using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Gestion;

namespace SIAC_DATOS.Gestion
{
    public class CD_ges_plancompras
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        
        public DataTable dtDataAnos;

        DatosMySql xMiFuncion = new DatosMySql();

        public void TraerDataAnos(int n_IdEmpresa)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("ges_plancompras_comprasanosdata", arrParametros, mysConec);
            dtDataAnos = DtResultado;
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
    }
}
