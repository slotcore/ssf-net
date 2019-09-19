using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Almacen;

namespace SIAC_DATOS.Almacen
{
    public class CD_alm_almacenesdoc
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable dtLista = new DataTable();

        // ***********************************************************************
        // n_TipoMovimiento = 1  FILTRARA SOLO LOS DOCUMENTOS QUE SEAN DE INGRESO 
        // n_TipoMovimiento = 2  FILTRARA SOLO LOS DOCUMENTOS QUE SEA DE SALIDA
        // ***********************************************************************
        public bool ListarEmpAlm(int n_IdEmpresa, int n_TipoMovimiento)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool b_result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "SYSTEM.INT16", n_IdEmpresa.ToString()},
                                            {"n_tipmov", "SYSTEM.INT16", n_TipoMovimiento.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("alm_almacenesdoc_listar2", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool Listar(int n_TipoMovimiento)
        {
            bool b_result = false;
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"n_tipmov", "SYSTEM.INT16", n_TipoMovimiento.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("alm_almacenesdoc_listar", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool TraerRegistro(int n_IdRegistro)
        {
            bool b_result = false;
            DatosMySql xMiFuncion = new DatosMySql();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("alm_almacenesdoc_obtenerregistro", arrParametros, mysConec);

            if (xMiFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool Insertar(BE_ALM_ALMACENESDOC entAlmacenesDoc)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("alm_almacenesdoc_insertar", entAlmacenesDoc, mysConec, 6) == true)
            {
                booOk = true;
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Actualizar(BE_ALM_ALMACENESDOC entAlmacenesDoc)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("alm_almacenesdoc_actualizar", entAlmacenesDoc, mysConec, null) == true)
            {
                booOk = true;
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Eliminar(int n_IdItem)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdItem.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("alm_almacenesdoc_delete", arrParametros, mysConec);

            if (booResult == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booResult;
        }
    }
}
