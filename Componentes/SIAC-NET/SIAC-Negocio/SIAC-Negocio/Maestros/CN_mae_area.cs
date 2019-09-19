using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Maestros;
using SIAC_DATOS.Maestros;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Maestros
{
    public class CN_mae_area
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_mae_area miFun = new CD_mae_area();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmpresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_MAE_AREA TraerRegistro(int n_IdRegistro)
        {
            BE_MAE_AREA entarea = new BE_MAE_AREA();
            DataTable dtresult = new DataTable();
            CD_mae_area miFun = new CD_mae_area();
            miFun.mysConec = mysConec;
            dtresult = miFun.TraerRegistro(n_IdRegistro);

            if (dtresult.Rows.Count != 0)
            {
                entarea.n_id = Convert.ToInt16(dtresult.Rows[0]["n_id"].ToString());
                entarea.n_idemp = Convert.ToInt16(dtresult.Rows[0]["n_idemp"].ToString());
                entarea.c_des = dtresult.Rows[0]["c_des"].ToString();
            }

            return entarea;
        }
        public bool Insertar(BE_MAE_AREA entClase)
        {
            BE_MAE_AREA entNuevoClase = new BE_MAE_AREA();
            CD_mae_area miFun = new CD_mae_area();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoClase.n_idemp = entClase.n_idemp;
            entNuevoClase.n_id = entClase.n_id;
            entNuevoClase.c_des = entClase.c_des;

            booOk = miFun.Insertar(entNuevoClase);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_MAE_AREA entClase)
        {
            BE_MAE_AREA entNuevoClase = new BE_MAE_AREA();
            CD_mae_area miFun = new CD_mae_area();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoClase.n_idemp = entClase.n_idemp;
            entNuevoClase.n_id = entClase.n_id;
            entNuevoClase.c_des = entClase.c_des;

            booOk = miFun.Actualizar(entNuevoClase);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_mae_area miFun = new CD_mae_area();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_Id);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
    }
}
