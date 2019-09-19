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
    public class CD_con_doccomimp
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtListar = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("con_doccomimp_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public void Select(int n_IdEmpresa)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()}
                                      };

            dtListar = xMiFuncion.StoreDTLLenar("con_doccomimp_select", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("con_doccomimp_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool Eliminar(int n_IdCargo)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool booOk = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdCargo.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("con_doccomimp_delete", arrParametros, mysConec) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                booOk = true;
            }

            return booOk;
        }
        public bool Insertar(BE_CON_DOCCOMIMP e_Percepcion)
        {
            bool booOk = false;

            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("con_doccomimp_insertar", e_Percepcion, mysConec, 0) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                booOk = true;
            }
            return booOk;
        }
        public bool Actualizar(BE_CON_DOCCOMIMP e_Percepcion)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("con_doccomimp_actualizar", e_Percepcion, mysConec, null) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                booOk = true;
            }
            return booOk;
        }
    }
}
