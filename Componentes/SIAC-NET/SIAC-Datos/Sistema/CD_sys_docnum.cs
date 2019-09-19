using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Sistema
{
    public class CD_sys_docnum
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public string HallaNumeroDocumento(int intIdEmp, int intIdTipDoc, string strNumeroSerie, int intGrabarNumero)
        {
            string strNumero = "";
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT32",Convert.ToString(intIdEmp)},
                                            {"n_idtipdoc", "System.INT32",Convert.ToString(intIdTipDoc)},
                                            {"c_numser", "System.STRING",strNumeroSerie},
                                            {"n_granum", "System.INT32",Convert.ToString(intGrabarNumero)}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("sys_docnum_ObtenerNumeroDocumento", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                strNumero = "";
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                string douNumero = DtResultado.Rows[0]["c_numdoc"].ToString();
                strNumero = douNumero.ToString();
            }

            return strNumero;
        }
    }
}
