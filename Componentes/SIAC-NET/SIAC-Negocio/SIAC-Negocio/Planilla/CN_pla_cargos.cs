using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Planillas;
using SIAC_DATOS.Planilla;

namespace SIAC_Negocio.Planilla
{
    public class CN_pla_cargos
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_pla_cargos miFun = new CD_pla_cargos();
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
        public BE_PLA_CARGOS TraerRegistro(int n_IdRegistro)
        {
            BE_PLA_CARGOS entarea = new BE_PLA_CARGOS();
            DataTable dtresult = new DataTable();
            CD_pla_cargos miFun = new CD_pla_cargos();
            miFun.mysConec = mysConec;
            dtresult = miFun.TraerRegistro(n_IdRegistro);

            if (dtresult.Rows.Count != 0)
            {
                entarea.n_id = Convert.ToInt32(dtresult.Rows[0]["n_id"].ToString());
                entarea.n_idemp = Convert.ToInt32(dtresult.Rows[0]["n_idemp"].ToString());
                entarea.c_des = dtresult.Rows[0]["c_des"].ToString();
            }

            return entarea;
        }
        public bool Insertar(BE_PLA_CARGOS entClase)
        {
            BE_PLA_CARGOS entNuevoClase = new BE_PLA_CARGOS();
            CD_pla_cargos miFun = new CD_pla_cargos();
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
        public bool Actualizar(BE_PLA_CARGOS entClase)
        {
            BE_PLA_CARGOS entNuevoClase = new BE_PLA_CARGOS();
            CD_pla_cargos miFun = new CD_pla_cargos();
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
            CD_pla_cargos miFun = new CD_pla_cargos();
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
