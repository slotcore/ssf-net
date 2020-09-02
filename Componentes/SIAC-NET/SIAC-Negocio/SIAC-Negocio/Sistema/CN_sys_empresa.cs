using MySql.Data.MySqlClient;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Sistema;
namespace SIAC_Negocio.Sistema
{
    public class CN_sys_empresa
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public BE_SYS_EMPRESA e_Empresa = new BE_SYS_EMPRESA();
        public DataTable dtFolderDocImpresion = new DataTable();
        public DataTable dtLista = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();
        
        public void Listar()
        {
            CD_sys_empresa miFun = new CD_sys_empresa();
            miFun.mysConec = mysConec;

            dtLista = miFun.Listar();
            if (dtLista == null)
            {
                dtLista = null;
                b_OcurrioError = miFun.booOcurrioError;
                c_ErrorMensaje = miFun.StrErrorMensaje;
                n_ErrorNumber = miFun.IntErrorNumber;
            }

            return;
        }
        public DataTable ListarEmpresasUsuario(int n_IdUsuario)
        {
            DataTable dtResul = new DataTable();

            CD_sys_empresa miFun = new CD_sys_empresa();
            miFun.mysConec = mysConec;

            dtResul = miFun.ListarEmpresasUsuario(n_IdUsuario);

            if (dtResul == null)
            {
                b_OcurrioError = miFun.booOcurrioError;
                c_ErrorMensaje = miFun.StrErrorMensaje;
                n_ErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public bool TraerRegistro(int n_IdEmpresa)
        {
            bool b_result = false;
            DataTable dtResult = new DataTable();
            CD_sys_empresa miFun = new CD_sys_empresa();
            Helper.Comunes.Funciones funFun = new Helper.Comunes.Funciones();
            miFun.mysConec = mysConec;
            dtResult = miFun.TraerRegistro(n_IdEmpresa);
            dtFolderDocImpresion = miFun.dtFolderDocImpresion;

            if (miFun.booOcurrioError == true)
            {
                return b_result;
            }
            e_Empresa.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
            e_Empresa.c_nomemp = dtResult.Rows[0]["c_nomemp"].ToString();
            e_Empresa.n_idtipdoc = Convert.ToInt32(dtResult.Rows[0]["n_idtipdoc"]);
            e_Empresa.c_numdoc = dtResult.Rows[0]["c_numdoc"].ToString();
            e_Empresa.c_nomcorto = dtResult.Rows[0]["c_nomcorto"].ToString();
            e_Empresa.n_activo = Convert.ToInt32(dtResult.Rows[0]["n_activo"]);
            e_Empresa.c_dir = dtResult.Rows[0]["c_dir"].ToString();
            e_Empresa.c_codtempus = dtResult.Rows[0]["c_codtempus"].ToString();
            e_Empresa.c_vtafearcdat = dtResult.Rows[0]["c_vtafearcdat"].ToString();
            e_Empresa.c_vtafearcenv = dtResult.Rows[0]["c_vtafearcenv"].ToString();
            e_Empresa.c_vtafearcrec = dtResult.Rows[0]["c_vtafearcrec"].ToString();
            e_Empresa.c_vtafealmenv = dtResult.Rows[0]["c_vtafealmenv"].ToString();
            e_Empresa.c_vtafealmrpt = dtResult.Rows[0]["c_vtafealmrpt"].ToString();
            e_Empresa.c_corctaven = dtResult.Rows[0]["c_corctaven"].ToString();
            e_Empresa.c_corctalog = dtResult.Rows[0]["c_corctalog"].ToString();
            e_Empresa.c_corctapro = dtResult.Rows[0]["c_corctapro"].ToString();
            e_Empresa.c_corctager = dtResult.Rows[0]["c_corctager"].ToString();
            e_Empresa.c_nomjefven = dtResult.Rows[0]["c_nomjefven"].ToString();
            e_Empresa.c_nomjeflog = dtResult.Rows[0]["c_nomjeflog"].ToString();

            e_Empresa.c_corrctapopcon = dtResult.Rows[0]["c_corrctapopcon"].ToString();
            e_Empresa.c_corrctapop = dtResult.Rows[0]["c_corrctapop"].ToString();

            e_Empresa.n_aplife = Convert.ToInt32(dtResult.Rows[0]["n_aplife"]);
            e_Empresa.c_folderimp = dtResult.Rows[0]["c_folderimp"].ToString();
            e_Empresa.n_idclipro = Convert.ToInt32(funFun.NulosN(dtResult.Rows[0]["n_idclipro"]));
            e_Empresa.n_idtipproven = Convert.ToInt32(funFun.NulosN(dtResult.Rows[0]["n_idtipproven"]));
            e_Empresa.c_verfe = dtResult.Rows[0]["c_verfe"].ToString();
            e_Empresa.c_logo = null;
            if (dtResult.Rows[0]["c_logo"] != DBNull.Value)
            { 
                e_Empresa.c_logo = (byte[])dtResult.Rows[0]["c_logo"];
            }
            b_result = true;
            return b_result;
        }
        public DataTable TraerRegistroBDMain(int n_IdEmpresa)
        {
            DataTable dtResult = new DataTable();
            CD_sys_empresa miFun = new CD_sys_empresa();

            miFun.mysConec = mysConec;
            dtResult = miFun.TraerRegistro(n_IdEmpresa);
            if (miFun.booOcurrioError == true)
            {
                //return b_result;
            }
            return dtResult;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            CD_sys_empresa miFun = new CD_sys_empresa();

            miFun.mysConec = mysConec;

            booResult = miFun.Eliminar(n_IdRegistro);
            if (booResult == false)
            {
                b_OcurrioError = false;
                c_ErrorMensaje = miFun.StrErrorMensaje;
                n_ErrorNumber = miFun.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_SYS_EMPRESA e_Empresa)
        {
            CD_sys_empresa miFun = new CD_sys_empresa();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_Empresa);

            b_OcurrioError = miFun.booOcurrioError;
            c_ErrorMensaje = miFun.StrErrorMensaje;
            n_ErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_SYS_EMPRESA e_Empresa)
        {
            CD_sys_empresa miFun = new CD_sys_empresa();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_Empresa);

            b_OcurrioError = miFun.booOcurrioError;
            c_ErrorMensaje = miFun.StrErrorMensaje;
            n_ErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
    }
}
