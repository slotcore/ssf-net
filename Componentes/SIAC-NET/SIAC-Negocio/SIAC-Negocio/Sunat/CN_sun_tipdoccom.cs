using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Sunat;
using SIAC_DATOS.Sunat;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Sunat
{
    public class CN_sun_tipdoccom
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public BE_SUN_TIPDOCCOM e_DocCom = new BE_SUN_TIPDOCCOM();
        public DataTable ListarVista()
        {
            DataTable dtResul = new DataTable();

            CD_sun_tipdoccom miFun = new CD_sun_tipdoccom();
            miFun.mysConec = mysConec;

            dtResul = miFun.ListarVista();

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable Listar()
        {
            DataTable dtResul = new DataTable();

            CD_sun_tipdoccom miFun = new CD_sun_tipdoccom();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar();

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable TraerRegistro(int n_Idregistro)
        {
            DataTable dtResul = new DataTable();
            Helper.Comunes.Funciones funfunciones = new Helper.Comunes.Funciones();
            CD_sun_tipdoccom miFun = new CD_sun_tipdoccom();
            miFun.mysConec = mysConec;

            miFun.TraerRegistro(n_Idregistro);
            if (miFun.booOcurrioError == true)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtResul = miFun.dtLista;
                e_DocCom.n_id = Convert.ToInt32(dtResul.Rows[0]["n_id"]);
                e_DocCom.c_codsun = dtResul.Rows[0]["c_codsun"].ToString();
                e_DocCom.c_des = dtResul.Rows[0]["c_des"].ToString();
                e_DocCom.c_abr = dtResul.Rows[0]["c_abr"].ToString();
                e_DocCom.n_idtipo = Convert.ToInt32(dtResul.Rows[0]["n_idtipo"]);
                e_DocCom.n_essal = Convert.ToInt32(funfunciones.NulosN(dtResul.Rows[0]["n_essal"]));
                e_DocCom.n_esent = Convert.ToInt32(funfunciones.NulosN(dtResul.Rows[0]["n_esent"]));
                e_DocCom.n_seimp = Convert.ToInt32(dtResul.Rows[0]["n_seimp"]);
                e_DocCom.n_numfil = Convert.ToInt32(dtResul.Rows[0]["n_numfil"]);
                e_DocCom.n_activo = Convert.ToInt32(dtResul.Rows[0]["n_activo"]);
                //e_DocCom.c_preser = Convert.ToInt32(dtResul.Rows[0]["c_preser"]);
                e_DocCom.n_escom = Convert.ToInt32(dtResul.Rows[0]["n_escom"]);
            }

            return dtResul;
        }
        public DataTable Listar_puntoventa()
        {
            DataTable dtResul = new DataTable();

            CD_sun_tipdoccom miFun = new CD_sun_tipdoccom();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar_puntoventa();

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public string UltimoNumero(int n_idemp, int n_idtipdoc, string c_numser)
        {
            CD_sun_tipdoccom miFun = new CD_sun_tipdoccom();
            miFun.mysConec = mysConec;

            return miFun.UltimoNumero(n_idemp,n_idtipdoc, c_numser);
        }
        public bool Eliminar(int n_Idregistro)
        {
            bool b_result = false;
            CD_sun_tipdoccom miFun = new CD_sun_tipdoccom();

            miFun.mysConec = mysConec;
            b_result = miFun.Eliminar(n_Idregistro);
            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_result;
        }
        public bool Insertar(BE_SUN_TIPDOCCOM e_Documento)
        {
            bool b_result = false;
            CD_sun_tipdoccom miFun = new CD_sun_tipdoccom();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(e_Documento);
            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(BE_SUN_TIPDOCCOM e_Documento)
        {
            bool b_result = false;
            CD_sun_tipdoccom miFun = new CD_sun_tipdoccom();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(e_Documento);
            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_result;
        }
    }
}
