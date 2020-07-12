using MySql.Data.MySqlClient;
using SIAC_DATOS.Maestros;
using SIAC_Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Negocio.Maestros
{
    public class CN_mae_aprobador
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_mae_aprobador miFun = new CD_mae_aprobador();
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
        public BE_MAE_APROBADOR TraerRegistro(int n_IdRegistro)
        {
            BE_MAE_APROBADOR entaprobador = new BE_MAE_APROBADOR();
            DataTable dtresult = new DataTable();
            CD_mae_aprobador miFun = new CD_mae_aprobador();
            miFun.mysConec = mysConec;
            dtresult = miFun.TraerRegistro(n_IdRegistro);

            if (dtresult.Rows.Count != 0)
            {
                entaprobador.n_id = Convert.ToInt32(dtresult.Rows[0]["n_id"].ToString());
                entaprobador.n_idemp = Convert.ToInt32(dtresult.Rows[0]["n_idemp"].ToString());
                entaprobador.n_idusu = Convert.ToInt32(dtresult.Rows[0]["n_idusu"].ToString());
                entaprobador.n_idfor = Convert.ToInt32(dtresult.Rows[0]["n_idfor"].ToString());
                entaprobador.n_idare = Convert.ToInt32(dtresult.Rows[0]["n_idare"].ToString());
                entaprobador.n_activo = Convert.ToInt32(dtresult.Rows[0]["n_activo"].ToString());
                entaprobador.desusuario = dtresult.Rows[0]["desusuario"].ToString();
                entaprobador.desarea = dtresult.Rows[0]["desarea"].ToString();
            }

            return entaprobador;
        }

        public DataTable ListarAprobadores(int n_idemp, int n_idusu, int n_idfor, int n_idare)
        {
            DataTable dtResul = new DataTable();

            CD_mae_aprobador miFun = new CD_mae_aprobador();
            miFun.mysConec = mysConec;

            dtResul = miFun.ListarAprobadores(n_idemp, n_idusu, n_idfor, n_idare);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public bool Insertar(BE_MAE_APROBADOR entClase)
        {
            BE_MAE_APROBADOR entNuevoClase = new BE_MAE_APROBADOR();
            CD_mae_aprobador miFun = new CD_mae_aprobador();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoClase.n_id = entClase.n_id;
            entNuevoClase.n_idemp = entClase.n_idemp;
            entNuevoClase.n_idusu = entClase.n_idusu;
            entNuevoClase.n_idfor = entClase.n_idfor;
            entNuevoClase.n_idare = entClase.n_idare;
            entNuevoClase.desusuario = entClase.desusuario;
            entNuevoClase.desarea = entClase.desarea;

            booOk = miFun.Insertar(entNuevoClase);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_MAE_APROBADOR entClase)
        {
            BE_MAE_APROBADOR entNuevoClase = new BE_MAE_APROBADOR();
            CD_mae_aprobador miFun = new CD_mae_aprobador();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoClase.n_id = entClase.n_id;
            entNuevoClase.n_idemp = entClase.n_idemp;
            entNuevoClase.n_idusu = entClase.n_idusu;
            entNuevoClase.n_idfor = entClase.n_idfor;
            entNuevoClase.n_idare = entClase.n_idare;
            entNuevoClase.desusuario = entClase.desusuario;
            entNuevoClase.desarea = entClase.desarea;

            booOk = miFun.Actualizar(entNuevoClase);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_mae_aprobador miFun = new CD_mae_aprobador();
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
