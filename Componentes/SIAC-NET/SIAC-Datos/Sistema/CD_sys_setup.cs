using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
using MySql.Data.MySqlClient;
using System.Data;

namespace SIAC_DATOS.Sistema
{
    public class CD_sys_setup
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();


        //public DataTable TraerRegistro(int n_IdEmpresa)
        //{
        //    DataTable DtResultado = new DataTable();
        //    DatosMySql xMiFuncion = new DatosMySql();
        //    string[,] arrParametros = new string[1, 3] {
        //                                    {"n_idemp", "System.STRING", n_IdEmpresa.ToString()}
        //                              };

        //    DtResultado = xMiFuncion.StoreDTLLenar("sys_setup_obtenerregistro", arrParametros, mysConec);
        //    if (xMiFuncion.IntErrorNumber != 0)
        //    {
        //        booOcurrioError = xMiFuncion.booOcurrioError;
        //        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
        //        IntErrorNumber = xMiFuncion.IntErrorNumber;
        //    }

        //    return DtResultado;
        //}
        public DataTable TraerRegistro()
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[0, 3] {                                            
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("sys_setup_obtenerregistro", arrParametros, mysConec);
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
