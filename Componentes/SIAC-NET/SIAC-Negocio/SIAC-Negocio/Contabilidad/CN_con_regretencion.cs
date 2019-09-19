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
    public class CN_con_regretencion
    {
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public DataTable dtDetalle = new DataTable();

        public BE_CON_REGRETENCION e_Retencion = new BE_CON_REGRETENCION();
        public List<BE_CON_REGRETENCIONDET> l_RetencionDet = new List<BE_CON_REGRETENCIONDET>();
        public List<BE_CON_DIARIO> l_Diario = new List<BE_CON_DIARIO>();
        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            CD_con_regretencion miFun = new CD_con_regretencion();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo);
            dtLista = miFun.dtLista;
            
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            CD_con_regretencion miFun = new CD_con_regretencion();
            miFun.mysConec = mysConec;

            miFun.TraerRegistro(n_IdRegistro);
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
                e_Retencion.n_idemp = Convert.ToInt32(dtLista.Rows[0]["n_idemp"]);
                e_Retencion.n_ano = Convert.ToInt32(dtLista.Rows[0]["n_ano"]);
                e_Retencion.n_mes = Convert.ToInt32(dtLista.Rows[0]["n_mes"]);
                e_Retencion.c_numreg = dtLista.Rows[0]["c_numreg"].ToString();
                e_Retencion.n_id = Convert.ToInt32(dtLista.Rows[0]["n_id"]);
                e_Retencion.n_idlib = Convert.ToInt32(dtLista.Rows[0]["n_idlib"]);
                e_Retencion.n_idtipdoc = Convert.ToInt32(dtLista.Rows[0]["n_idtipdoc"]);
                e_Retencion.n_idret = Convert.ToInt32(dtLista.Rows[0]["n_idret"]);
                e_Retencion.n_tas = Convert.ToDouble(dtLista.Rows[0]["n_tas"]);
                e_Retencion.n_tip = Convert.ToInt32(dtLista.Rows[0]["n_tip"]);
                e_Retencion.n_idcli = Convert.ToInt32(dtLista.Rows[0]["n_idcli"]);
                e_Retencion.c_numser = dtLista.Rows[0]["c_numser"].ToString();
                e_Retencion.c_numdoc = dtLista.Rows[0]["c_numdoc"].ToString();
                e_Retencion.d_fchemi = Convert.ToDateTime(dtLista.Rows[0]["d_fchemi"]);
                e_Retencion.d_fchreg = Convert.ToDateTime(dtLista.Rows[0]["d_fchreg"]);
                e_Retencion.n_idmon = Convert.ToInt32(dtLista.Rows[0]["n_idmon"]);
                e_Retencion.n_impret = Convert.ToDouble(dtLista.Rows[0]["n_impret"]);
                e_Retencion.c_glo = dtLista.Rows[0]["c_glo"].ToString();
                e_Retencion.n_tc = Convert.ToDouble(dtLista.Rows[0]["n_tc"]);
            }
            return;
        }
        public bool Eliminar(int n_Idregistro)
        {
            bool b_result = false;
            CD_con_regretencion miFun = new CD_con_regretencion();

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
        public bool Insertar(BE_CON_REGRETENCION e_Retencion, List<BE_CON_REGRETENCIONDET> l_RetencionDetalle)
        {
            bool b_result = false;
            CD_con_regretencion miFun = new CD_con_regretencion();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(e_Retencion, l_RetencionDetalle);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                //string c_NumAsi = "";
                //CN_con_diario funCon = new CN_con_diario();
                //funCon.mysConec = mysConec;
                //funCon.STU_SISTEMA = STU_SISTEMA;
                
                //funCon.GenerarAsientoRetencion(STU_SISTEMA.EMPRESAID, e_Retencion.n_id, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 17, "");
                //c_NumAsi = funCon.c_NewNumAsiento;
                //miFun.AgregarNumAsi(e_Retencion.n_id, c_NumAsi);
            }
            b_result = true;
            return b_result;
        }
        public bool Actualizar(BE_CON_REGRETENCION e_Retencion, List<BE_CON_REGRETENCIONDET> l_RetencionDetalle)
        {
            bool b_result = false;
            CD_con_regretencion miFun = new CD_con_regretencion();

            miFun.mysConec = mysConec;
            miFun.l_Diario = l_Diario;
            b_result = miFun.Actualizar(e_Retencion, l_RetencionDetalle);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                //string c_NumAsi = e_Retencion.c_numreg;
                //CN_con_diario funCon = new CN_con_diario();
                //funCon.mysConec = mysConec;
                //funCon.STU_SISTEMA = STU_SISTEMA;

                //funCon.GenerarAsientoRetencion(STU_SISTEMA.EMPRESAID, e_Retencion.n_id, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 17, c_NumAsi);
            }
            return b_result;
        }
    }
}
