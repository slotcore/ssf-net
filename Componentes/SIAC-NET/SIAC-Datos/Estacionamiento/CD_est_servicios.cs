using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Estacionamiento;

namespace SIAC_DATOS.Estacionamiento
{
    public class CD_est_servicios
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public DataTable dtListar = new DataTable();
        
        MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public CD_est_servicios(string c_IPServidor, string c_NombreBD, string c_Usuario, string c_Passord, string c_Puerto)
        {
            MySqlConnection mysConectar = new MySqlConnection();
            Helper.DatosMySql hlpFuncion = new Helper.DatosMySql();

            mysConec = hlpFuncion.ObtenerConexion(c_IPServidor, c_NombreBD, c_Usuario, c_Passord, c_Puerto);

            if (hlpFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        ~CD_est_servicios()
        {
            mysConec.Close();
            mysConec = null; 
        }

        public void Listar(int n_IdEmpresa)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()}
                                      };

            dtListar = xMiFuncion.StoreDTLLenar("est_servicios_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool TraerRegistro(int n_IdRegistro)
        {
            bool booResult = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("est_servicios_obtenerregistro", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            booResult = true;
            return booResult;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("est_servicios_delete", arrParametros, mysConec);
            if (booResult == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_EST_SERVICIOS e_Servicio)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            booOk = xMiFuncion.StoreEjecutar("est_servicios_insertar", e_Servicio, mysConec, null);
            
            if (booOk == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Actualizar(BE_EST_SERVICIOS e_Servicio)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            booOk = xMiFuncion.StoreEjecutar("est_servicios_actualizar", e_Servicio, mysConec, null);

            if (booOk == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
    }
}
