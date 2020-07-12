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
    public class CD_alm_almacenes
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        public DataTable Listar(int n_idempresa, int n_EsUnificado)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_idempresa.ToString()},
                                            {"n_esuni", "System.INT16", n_EsUnificado.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_almacenes_select", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public BE_ALM_ALMACENES_CONSULTA TraerRegistro(int n_IdRegistro)
        {
            BE_ALM_ALMACENES_CONSULTA EntAlmacenes = new BE_ALM_ALMACENES_CONSULTA();
            DataTable DtResultado = new DataTable();
            
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("alm_almacenes_obtenerregistro", arrParametros, mysConec);
 
            if (DtResultado.Rows.Count != 0)
            {
                EntAlmacenes.n_idemp = Convert.ToInt16(DtResultado.Rows[0]["n_idemp"].ToString());
                EntAlmacenes.n_id = Convert.ToInt16(DtResultado.Rows[0]["n_id"].ToString());
                EntAlmacenes.n_idlocal = Convert.ToInt16(DtResultado.Rows[0]["n_idlocal"].ToString());
                EntAlmacenes.n_idtipexi = Convert.ToInt16(DtResultado.Rows[0]["n_idtipexi"].ToString());
                EntAlmacenes.c_des = DtResultado.Rows[0]["c_des"].ToString();
                EntAlmacenes.c_desemploc = DtResultado.Rows[0]["c_desemploc"].ToString();
            }
            return EntAlmacenes;
        }
        public bool Insertar(BE_ALM_ALMACENES entAlmacenes)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("alm_almacenes_insertar", entAlmacenes, mysConec, 2) == true)
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
        public bool Actualizar(BE_ALM_ALMACENES entAlmacenes)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("alm_almacenes_actualizar", entAlmacenes, mysConec, null) == true)
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
        public bool Eliminar(int n_IdItem)
        {
            //alm_inventario_delete
            //DataTable DtResultado = new DataTable();
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdItem.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("alm_almacenes_delete", arrParametros, mysConec);

            if (booResult == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booResult;
        }
    }
}
