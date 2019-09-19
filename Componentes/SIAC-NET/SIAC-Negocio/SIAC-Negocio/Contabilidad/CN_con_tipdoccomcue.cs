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
using SIAC_Entidades.Cooperativa;
using SIAC_Entidades.Contabilidad;
using Helper.Comunes;

namespace SIAC_Negocio.Contabilidad
{
    public class CN_con_tipdoccomcue
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;

        Funciones funFunciones = new Funciones();

        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public BE_CON_TIPDOCCOMCUE e_Documento = new BE_CON_TIPDOCCOMCUE();
        public void Listar(int n_IdEmpresa)
        {
            CD_con_tipdoccomcue miFun = new CD_con_tipdoccomcue();
            miFun.mysConec = mysConec;
            miFun.Listar(n_IdEmpresa);
            dtLista = miFun.dtLista;

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            CD_con_tipdoccomcue miFun = new CD_con_tipdoccomcue();
            miFun.mysConec = mysConec;
            miFun.TraerRegistro(n_IdRegistro);
            dtLista = miFun.dtLista;

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                e_Documento.n_id = Convert.ToInt32(dtLista.Rows[0]["n_id"]);
                e_Documento.n_idtipdoc = Convert.ToInt32(dtLista.Rows[0]["n_idtipdoc"]);
                e_Documento.n_idmon = Convert.ToInt32(dtLista.Rows[0]["n_idmon"]);
                e_Documento.n_idcuecom = Convert.ToInt32(funFunciones.NulosN(dtLista.Rows[0]["n_idcuecom"]));
                e_Documento.n_idcueven = Convert.ToInt32(funFunciones.NulosN(dtLista.Rows[0]["n_idcueven"]));
                e_Documento.n_idemp = Convert.ToInt32(dtLista.Rows[0]["n_idemp"]);
            }
            return;
        }
        public bool Eliminar(int n_Idregistro)
        {
            bool b_result = false;
            CD_con_tipdoccomcue miFun = new CD_con_tipdoccomcue();

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
        public bool Insertar(BE_CON_TIPDOCCOMCUE e_DocumentoCta)
        {
            bool b_result = false;
            CD_con_tipdoccomcue miFun = new CD_con_tipdoccomcue();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(e_DocumentoCta);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(BE_CON_TIPDOCCOMCUE e_DocumentoCta)
        {
            bool b_result = false;
            CD_con_tipdoccomcue miFun = new CD_con_tipdoccomcue();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(e_DocumentoCta);
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
