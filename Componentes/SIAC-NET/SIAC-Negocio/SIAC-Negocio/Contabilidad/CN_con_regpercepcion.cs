using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Contabilidad;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using Helper;

namespace SIAC_Negocio.Contabilidad
{
    public class CN_con_regpercepcion
    {
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public DataTable dtDetalle = new DataTable();

        public BE_CON_REGPERCEPCION e_Percepcion = new BE_CON_REGPERCEPCION();
        public List<BE_CON_REGPERCEPCIONDET> l_PercepcionDet = new List<BE_CON_REGPERCEPCIONDET>();
        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo, int n_TipoRegistro)
        {
            CD_con_regpercepcion miFun = new CD_con_regpercepcion();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo, n_TipoRegistro);
            dtLista = miFun.dtLista;

            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return;
        }
        public void TraerRegistro(int n_IdRegistro, int n_TipoRegistro)
        {
            CD_con_regpercepcion miFun = new CD_con_regpercepcion();
            miFun.mysConec = mysConec;

            miFun.TraerRegistro(n_IdRegistro, n_TipoRegistro);
            dtLista = miFun.dtLista;
            dtDetalle = miFun.dtDetalle;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                e_Percepcion.n_idemp = Convert.ToInt32(dtLista.Rows[0]["n_idemp"]);
                e_Percepcion.n_ano = Convert.ToInt32(dtLista.Rows[0]["n_ano"]);
                e_Percepcion.n_mes = Convert.ToInt32(dtLista.Rows[0]["n_mes"]);
                e_Percepcion.c_numreg = dtLista.Rows[0]["c_numreg"].ToString();
                e_Percepcion.n_id = Convert.ToInt32(dtLista.Rows[0]["n_id"]);
                e_Percepcion.n_idlib = Convert.ToInt32(dtLista.Rows[0]["n_idlib"]);
                e_Percepcion.n_idtipdoc = Convert.ToInt32(dtLista.Rows[0]["n_idtipdoc"]);
                e_Percepcion.n_idper = Convert.ToInt32(dtLista.Rows[0]["n_idper"]);
                e_Percepcion.n_tas = Convert.ToDouble(dtLista.Rows[0]["n_tas"]);
                e_Percepcion.n_tip = Convert.ToInt32(dtLista.Rows[0]["n_tip"]);
                e_Percepcion.n_idcli = Convert.ToInt32(dtLista.Rows[0]["n_idcli"]);
                e_Percepcion.c_numser = dtLista.Rows[0]["c_numser"].ToString();
                e_Percepcion.c_numdoc = dtLista.Rows[0]["c_numdoc"].ToString();
                e_Percepcion.d_fchemi = Convert.ToDateTime(dtLista.Rows[0]["d_fchemi"]);
                e_Percepcion.d_fchreg = Convert.ToDateTime(dtLista.Rows[0]["d_fchreg"]);
                e_Percepcion.n_idmon = Convert.ToInt32(dtLista.Rows[0]["n_idmon"]);
                e_Percepcion.n_impper = Convert.ToDouble(dtLista.Rows[0]["n_impper"]);
                e_Percepcion.c_glo = dtLista.Rows[0]["c_glo"].ToString();
                e_Percepcion.n_tc = Convert.ToDouble(dtLista.Rows[0]["n_tc"]);
                //e_Percepcion.n_saldo = Convert.ToDouble(dtLista.Rows[0]["n_saldo"]);
            }
            return;
        }
        public bool Eliminar(int n_Idregistro)
        {
            bool b_result = false;
            CD_con_regpercepcion miFun = new CD_con_regpercepcion();

            miFun.mysConec = mysConec;
            b_result = miFun.Eliminar(n_Idregistro);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Insertar(BE_CON_REGPERCEPCION e_Percepcion, List<BE_CON_REGPERCEPCIONDET> l_PercepcionDetalle, int n_TipoRegistro)
        {
            bool b_result = false;
            CD_con_regpercepcion miFun = new CD_con_regpercepcion();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(e_Percepcion, l_PercepcionDetalle);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                string c_NumAsi = "";
                CN_con_diario funCon = new CN_con_diario();
                funCon.mysConec = mysConec;
                funCon.STU_SISTEMA = STU_SISTEMA;

                funCon.GenerarAsientoPercepcion(STU_SISTEMA.EMPRESAID, e_Percepcion.n_id, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 16, "", n_TipoRegistro);
                c_NumAsi = funCon.c_NewNumAsiento;
                miFun.AgregarNumAsi(e_Percepcion.n_id, c_NumAsi);
            }
            b_result = true;
            return b_result;
        }
        public bool Actualizar(BE_CON_REGPERCEPCION e_Percepcion, List<BE_CON_REGPERCEPCIONDET> l_PercepcionDetalle, int n_TipoRegistro)
        {
            bool b_result = false;
            CD_con_regpercepcion miFun = new CD_con_regpercepcion();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(e_Percepcion, l_PercepcionDetalle);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                string c_NumAsi = e_Percepcion.c_numreg;
                CN_con_diario funCon = new CN_con_diario();
                funCon.mysConec = mysConec;
                funCon.STU_SISTEMA = STU_SISTEMA;

                funCon.GenerarAsientoPercepcion(STU_SISTEMA.EMPRESAID, e_Percepcion.n_id, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 16, c_NumAsi, n_TipoRegistro);
            }
            return b_result;
        }
    }
}
