using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Maestros;

namespace SIAC_DATOS.Planilla
{
    public class CD_TEMPUS_marcacion
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable ListarIngresos(int n_AnoTrabajo, int n_MesTrabajo, string c_CodEmpresa)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[3, 3] {
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"n_mestra", "System.INT16", n_MesTrabajo.ToString()},
                                            {"c_idemp", "System.STRING", c_CodEmpresa.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("tempus_marcacion_ingreso", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return DtResultado;
        }
        public DataTable ListarSalidas(int n_AnoTrabajo, int n_MesTrabajo, string c_CodEmpresa)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[3, 3] {
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"n_mestra", "System.INT16", n_MesTrabajo.ToString()},
                                            {"c_idemp", "System.STRING", c_CodEmpresa.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("tempus_marcacion_salida", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return DtResultado;
        }
        public DataTable ListarPersonal(string c_CodEmpresa)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"c_codemp", "System.STRING", c_CodEmpresa.ToString()},
                                          
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("tempus_personal", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return DtResultado;
        }
        public DataTable AnosTrabajo()
        { 
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[0, 3] {
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("tempus_anostrabajo", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return DtResultado;
        }
        public DataTable Empresas()
        { 
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[0, 3] {
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("tempus_empresas", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return DtResultado;
        }
    }
}
