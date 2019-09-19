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
    public class CD_sys_formulariovista
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable ObtenerCabeceraLista(int intIdEmpresa)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idformulario", "System.INT32",Convert.ToString(intIdEmpresa)}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("sys_formulariovista_obtener_lista_x_formulario", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
           
            return DtResultado;
        }
    }
}
