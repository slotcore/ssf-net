using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Maestros;

namespace SIAC_DATOS.Maestros
{
    public class CD_mae_subclase
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar(int n_IdEmpresa, int n_EsUnificado)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_esuni", "System.INT16", n_EsUnificado.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("mae_subclase_select", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public BE_MAE_SUBCLASE TraerRegistro(int n_IdRegistro)
        {
            BE_MAE_SUBCLASE Ent_Clase = new BE_MAE_SUBCLASE();
            DatosMySql xMiFuncion = new DatosMySql();

            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("mae_subclase_obtenerregistro", arrParametros, mysConec);

            if (DtResultado.Rows.Count != 0)
            {
                Ent_Clase.n_id = Convert.ToInt32(DtResultado.Rows[0]["n_id"].ToString());
                Ent_Clase.n_idemp = Convert.ToInt32(DtResultado.Rows[0]["n_idemp"].ToString());
                Ent_Clase.n_idtipexi = Convert.ToInt32(DtResultado.Rows[0]["n_idtipexi"].ToString());
                Ent_Clase.n_idfam = Convert.ToInt32(DtResultado.Rows[0]["n_idfam"].ToString());
                Ent_Clase.n_idcla = Convert.ToInt32(DtResultado.Rows[0]["n_idcla"].ToString());
                Ent_Clase.c_des = DtResultado.Rows[0]["c_des"].ToString();
                Ent_Clase.c_pre = DtResultado.Rows[0]["c_pre"].ToString();
            }
            return Ent_Clase;
        }
        public bool Insertar(BE_MAE_SUBCLASE entSubClase)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("mae_subclase_insertar", entSubClase, mysConec, 4) == true)
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
        public bool Actualizar(BE_MAE_SUBCLASE entSubClase)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("mae_subclase_actualizar", entSubClase, mysConec, null) == true)
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
        public bool Eliminar(int n_Id)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_Id.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("mae_subclase_delete", arrParametros, mysConec);

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
