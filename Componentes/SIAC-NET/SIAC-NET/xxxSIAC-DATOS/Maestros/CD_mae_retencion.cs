using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SIAC_Entidades;
using Helper;

namespace SIAC_DATOS.Maestros
{
    public class CD_mae_retencion
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public bool Insertar(BE_MAE_RETENCION entRetencion)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            if (xMiFuncion.StoreEjecutar("mae_retencion_insert", entRetencion, mysConec) == true)
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
        public bool Modificar(BE_MAE_RETENCION entRetencion)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            if (xMiFuncion.StoreEjecutar("mae_retencion_update", entRetencion, mysConec) == true)
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
        public bool Eliminar(int intRetencionId)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                                            {"n_idret", "SYSTEM.INT16", intRetencionId.ToString()}
                                                       };

            if (xMiFuncion.StoreEjecutar("mae_retencion_delete", arrParametros, mysConec) == true)
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
        public DataTable Listar()
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"", "",""}
                                      };           

            DtResultado = xMiFuncion.StoreDTLLenar("mae_retencion_select", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public void CargarCombo(ref ComboBox CmbControl, DataTable DtTabla)
        {
            DatosMySql xMiFuncion = new DatosMySql();

            xMiFuncion.ComboBoxCargarDataTable(CmbControl, DtTabla, "n_id", "c_des");
        }
    }
}
