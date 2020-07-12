using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Produccion;
namespace SIAC_DATOS.Produccion
{
    public class CD_pro_personal
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable dtPersonal = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();

        public bool Listar(int n_IdEmpresa)
        {
            bool b_Result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                      };

            dtPersonal = xMiFuncion.StoreDTLLenar("pro_personal_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_Result = true;
            }

            return b_Result;
        }

        public bool ListarPorCargo(int n_IdEmpresa, int n_IdCargo)
        {
            bool b_Result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                            {"n_idcargo", "System.INT64",n_IdCargo.ToString()},
                                      };

            dtPersonal = xMiFuncion.StoreDTLLenar("pro_personal_listar_Por_Cargo", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_Result = true;
            }

            return b_Result;
        }

        public bool ObtenerEmpleado(int n_IdPersonal)
        {
            bool b_Result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdPersonal.ToString()},
                                      };

            dtPersonal = xMiFuncion.StoreDTLLenar("pro_personal_obtener_empleado", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_Result = true;
            }

            return b_Result;
        }
    }
}
