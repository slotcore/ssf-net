using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Negocio.Almacen
{
    public class CN_alm_personal
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_alm_personal miFun = new CD_alm_personal();
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

        public BE_ALM_PERSONAL TraerRegistro(int n_IdRegistro)
        {
            BE_ALM_PERSONAL entaprobador = new BE_ALM_PERSONAL();
            DataTable dtresult = new DataTable();
            CD_alm_personal miFun = new CD_alm_personal();
            miFun.mysConec = mysConec;
            dtresult = miFun.TraerRegistro(n_IdRegistro);

            if (dtresult.Rows.Count != 0)
            {
                entaprobador.n_id = Convert.ToInt32(dtresult.Rows[0]["n_id"].ToString());
                entaprobador.n_idemp = Convert.ToInt32(dtresult.Rows[0]["n_idemp"].ToString());
                entaprobador.n_idtra = Convert.ToInt32(dtresult.Rows[0]["n_idtra"].ToString());
                entaprobador.n_idcargo = Convert.ToInt32(dtresult.Rows[0]["n_idcargo"].ToString());
                entaprobador.n_activo = Convert.ToInt32(dtresult.Rows[0]["n_activo"].ToString());
                entaprobador.destra = dtresult.Rows[0]["destra"].ToString();
                entaprobador.descargo = dtresult.Rows[0]["descargo"].ToString();
            }

            return entaprobador;
        }

        public DataTable ListarPorCargo(int n_idemp, int n_idcargo)
        {
            DataTable dtResul = new DataTable();

            CD_alm_personal miFun = new CD_alm_personal();
            miFun.mysConec = mysConec;

            dtResul = miFun.ListarPorCargo(n_idemp, n_idcargo);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }

        public bool Insertar(BE_ALM_PERSONAL entClase)
        {
            BE_ALM_PERSONAL entNuevoClase = new BE_ALM_PERSONAL();
            CD_alm_personal miFun = new CD_alm_personal();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoClase.n_id = entClase.n_id;
            entNuevoClase.n_idemp = entClase.n_idemp;
            entNuevoClase.n_idtra = entClase.n_idtra;
            entNuevoClase.n_idcargo = entClase.n_idcargo;
            entNuevoClase.destra = entClase.destra;
            entNuevoClase.descargo = entClase.descargo;

            booOk = miFun.Insertar(entNuevoClase);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }

        public bool Actualizar(BE_ALM_PERSONAL entClase)
        {
            BE_ALM_PERSONAL entNuevoClase = new BE_ALM_PERSONAL();
            CD_alm_personal miFun = new CD_alm_personal();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoClase.n_id = entClase.n_id;
            entNuevoClase.n_idemp = entClase.n_idemp;
            entNuevoClase.n_idtra = entClase.n_idtra;
            entNuevoClase.n_idcargo = entClase.n_idcargo;
            entNuevoClase.destra = entClase.destra;
            entNuevoClase.descargo = entClase.descargo;

            booOk = miFun.Actualizar(entNuevoClase);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }

        public bool Eliminar(int n_Id)
        {
            CD_alm_personal miFun = new CD_alm_personal();
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
