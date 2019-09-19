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
    public class CD_pro_rendimiento
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public DataTable dtRegistro = new DataTable();
        public DataTable dtListar = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        public bool Listar(int n_IdEmpresa, int n_EsUnificado)
        {
            bool booResult = false;
            
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()},
                                            {"n_esuni", "System.INT16", n_EsUnificado.ToString()}
                                      };

            dtListar = xMiFuncion.StoreDTLLenar("pro_rendimiento_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            booResult = true;

            return booResult;
        }
        public bool TraerRegistro(Int64 n_IdRegistro)
        {
            DataTable DtResultado = new DataTable();
            bool booResult = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("pro_rendimiento_obtenerregistro", arrParametros, mysConec);

            dtRegistro = DtResultado;
            booResult = true;

            return booResult;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("pro_rendimiento_delete", arrParametros, mysConec);
            if (booResult == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_PRO_RENDIMIENTO entRendimiento)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("pro_rendimiento_insertar", entRendimiento, mysConec, 1) == true)
            {
                booOk = true;
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Actualizar(BE_PRO_RENDIMIENTO entRendimiento)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("pro_rendimiento_actualizar", entRendimiento, mysConec, null) == true)
            {
                booOk = true;
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
    }
}
