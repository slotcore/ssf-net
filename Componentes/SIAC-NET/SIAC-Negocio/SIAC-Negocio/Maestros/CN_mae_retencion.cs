using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Maestros;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Maestros
{
    public class CN_mae_retencion
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public bool Insertar(BE_MAE_RETENCION entRetencion)
        {
            bool booOk = false;
            CD_mae_retencion miFun = new CD_mae_retencion();
            miFun.mysConec = mysConec;
            if (booOk = miFun.Insertar(entRetencion) == true)
            {
                booOk = true;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booOk;
        }

        public bool Modificar(BE_MAE_RETENCION entRetencion)
        {
            bool booOk = false;
            CD_mae_retencion miFun = new CD_mae_retencion();
            miFun.mysConec = mysConec;
            if (booOk = miFun.Modificar(entRetencion) == true)
            {
                booOk = true;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booOk;
        }

        public bool Eliminar(int intRetencionId)
        {
            bool booOk = false;
            CD_mae_retencion miFun = new CD_mae_retencion();
            miFun.mysConec = mysConec;
            if (booOk = miFun.Eliminar(intRetencionId) == true)
            {
                booOk = true;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booOk;
        }

        public DataTable Listar()
        {
            DataTable dtResul = new DataTable();
            CD_mae_retencion miFun = new CD_mae_retencion();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar();

            if (dtResul == null) {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }

        public void CargarCombo(ref ComboBox CmbControl, DataTable DtTabla)
        {
            CD_mae_retencion miFun = new CD_mae_retencion();

            miFun.CargarCombo(ref CmbControl, DtTabla);

            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;
        }
    }
}
