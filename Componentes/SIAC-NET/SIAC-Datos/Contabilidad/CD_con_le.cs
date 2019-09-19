using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Contabilidad;
namespace SIAC_DATOS.Contabilidad
{
    public class CD_con_le
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        public void LE_81(int n_IdEmpresa,int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro)
        {
            string[,] arrParametros = new string[4, 3] {
                                        {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                        {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                        {"n_mes", "System.INT32", n_MesTrabajo.ToString()},
                                        {"n_idlib", "System.INT32", n_IdLibro.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_le_81", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void LE_82(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro)
        {
            string[,] arrParametros = new string[4, 3] {
                                        {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                        {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                        {"n_mes", "System.INT32", n_MesTrabajo.ToString()},
                                        {"n_idlib", "System.INT32", n_IdLibro.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_le_82", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void LE_141(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro)
        {
            string[,] arrParametros = new string[4, 3] {
                                        {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                        {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                        {"n_mes", "System.INT32", n_MesTrabajo.ToString()},
                                        {"n_idlib", "System.INT32", n_IdLibro.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_le_141", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
    }
}
