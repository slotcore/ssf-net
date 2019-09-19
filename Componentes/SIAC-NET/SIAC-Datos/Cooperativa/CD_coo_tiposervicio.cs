using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Cooperativa;

namespace SIAC_DATOS.Cooperativa
{
    public class CD_coo_tiposervicio
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable dtTipSer = new DataTable();
        public DataTable dtLista = new DataTable();
        public void Listar(int n_IdEmpresa)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[0, 3] {  };

            dtLista = xMiFuncion.StoreDTLLenar("coo_tiposervicio_select", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DatosMySql xMiFuncion = new DatosMySql();

            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            dtTipSer = xMiFuncion.StoreDTLLenar("coo_tiposervicio_obtenerregistro", arrParametros, mysConec);

            if (xMiFuncion.booOcurrioError == true)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool Insertar(BE_COO_TIPOSERVICIO entTipoServicio)
        {
            bool booOk = false;

            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("coo_tiposervicio_insertar", entTipoServicio, mysConec, 1) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                booOk = true;
            }
            return booOk;
        }
        public bool Actualizar(BE_COO_TIPOSERVICIO entTipoServicio)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("coo_tiposervicio_actualizar", entTipoServicio, mysConec, null) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                booOk = true;
            }
            return booOk;
        }
        public bool Eliminar(int n_IdCargo)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool booOk = false;

            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdCargo.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("coo_tiposervicio_delete", arrParametros2, mysConec) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                booOk = true;
            }

            return booOk;
        }
    }
}
