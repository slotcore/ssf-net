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
    public class CD_mae_motivos
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable Listar(int n_IdEmpresa, int n_IdModulo)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16",n_IdEmpresa.ToString()},
                                            {"n_idmod", "System.INT16",n_IdModulo.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("mae_motivos_select", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public BE_MAE_MOTIVOS TraerRegistro(int n_IdRegistro)
        {
            BE_MAE_MOTIVOS e_motivo = new BE_MAE_MOTIVOS();
            DatosMySql xMiFuncion = new DatosMySql();
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("mae_motivos_obtenerregistro", arrParametros, mysConec);

            if (DtResultado.Rows.Count != 0)
            {
                e_motivo.n_id = Convert.ToInt16(DtResultado.Rows[0]["n_id"].ToString());
                e_motivo.n_idemp = Convert.ToInt16(DtResultado.Rows[0]["n_idemp"].ToString());
                e_motivo.c_des = DtResultado.Rows[0]["c_des"].ToString();
                e_motivo.n_idmod = Convert.ToInt16(DtResultado.Rows[0]["n_idmod"].ToString());
                e_motivo.n_activo = Convert.ToInt16(DtResultado.Rows[0]["n_activo"].ToString());
            }
            return e_motivo;
        }
        public bool Insertar(BE_MAE_MOTIVOS e_Motivos)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("mae_motivos_insertar", e_Motivos, mysConec, 1) == true)
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
        public bool Actualizar(BE_MAE_MOTIVOS e_Motivos)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("mae_motivos_actualizar", e_Motivos, mysConec, null) == true)
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
        public bool Eliminar(int n_Id)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_Id.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("mae_motivos_delete", arrParametros, mysConec);

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
