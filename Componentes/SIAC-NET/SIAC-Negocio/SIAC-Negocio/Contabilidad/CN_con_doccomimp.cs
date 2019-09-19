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
    public class CN_con_doccomimp
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtListar = new DataTable();
        public BE_CON_DOCCOMIMP e_DocImp = new BE_CON_DOCCOMIMP();

        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_con_doccomimp miFun = new CD_con_doccomimp();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmpresa);

            if (dtResul == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return dtResul;
        }
        public void Select(int n_IdEmpresa)
        {
            CD_con_doccomimp miFun = new CD_con_doccomimp();
            miFun.mysConec = mysConec;

            miFun.Select(n_IdEmpresa);
            dtListar = miFun.dtListar;

            if (miFun.n_ErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            CD_con_doccomimp miFun = new CD_con_doccomimp();
            miFun.mysConec = mysConec;

            miFun.TraerRegistro(n_IdRegistro);
            dtListar = miFun.dtListar;
            if (dtListar == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                e_DocImp.n_id = Convert.ToInt32(dtListar.Rows[0]["n_id"]);
                e_DocImp.n_idimp = Convert.ToInt32(dtListar.Rows[0]["n_idimp"]);
                e_DocImp.n_idtipdoc = Convert.ToInt32(dtListar.Rows[0]["n_idtipdoc"]);
                e_DocImp.n_idcuecom = Convert.ToInt32(dtListar.Rows[0]["n_idcuecom"]);
                e_DocImp.n_idcueven = Convert.ToInt32(dtListar.Rows[0]["n_idcueven"]);
                e_DocImp.n_portas = Convert.ToDouble(dtListar.Rows[0]["n_portas"]);
                e_DocImp.n_idemp = Convert.ToInt32(dtListar.Rows[0]["n_idemp"]);
            }
            return;
        }
        public bool Eliminar(int n_Idregistro)
        {
            bool b_result = false;
            CD_con_doccomimp miFun = new CD_con_doccomimp();

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
        public bool Insertar(BE_CON_DOCCOMIMP e_DocImp)
        {
            bool b_result = false;
            CD_con_doccomimp miFun = new CD_con_doccomimp();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(e_DocImp);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(BE_CON_DOCCOMIMP e_DocImp)
        {
            bool b_result = false;
            CD_con_doccomimp miFun = new CD_con_doccomimp();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(e_DocImp);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
    }
}
