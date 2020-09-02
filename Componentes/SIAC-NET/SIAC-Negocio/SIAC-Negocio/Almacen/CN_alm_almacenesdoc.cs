using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_Entidades;
using SIAC_Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIAC_Negocio.Almacen
{
    public class CN_alm_almacenesdoc
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable dtLista = new DataTable();
        public BE_ALM_ALMACENESDOC e_AlmDoc = new BE_ALM_ALMACENESDOC();

        public DataTable ListarEmpAlm(int n_IdEmpresa, int n_TipoMovimiento)
        {
            DataTable dtResult = new DataTable();
            CD_alm_almacenesdoc miFun = new CD_alm_almacenesdoc();
            miFun.mysConec = mysConec;
            miFun.ListarEmpAlm(n_IdEmpresa, n_TipoMovimiento);
            dtResult = miFun.dtLista;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return dtResult;
            }
            return dtResult;
        }
        public bool Listar(int n_TipoMovimiento)
        {
            bool b_result = false;
            CD_alm_almacenesdoc miFun = new CD_alm_almacenesdoc();
            miFun.mysConec = mysConec;
            miFun.Listar(n_TipoMovimiento);

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            dtLista = miFun.dtLista;
            b_result = true;
            return b_result;
        }
        public bool TraerRegistro(int n_IdRegistro)
        {
            bool b_result = false;
            DataTable dtResul = new DataTable();

            CD_alm_almacenesdoc miFun = new CD_alm_almacenesdoc();
            miFun.mysConec = mysConec;
            miFun.TraerRegistro(n_IdRegistro);
            dtResul = miFun.dtLista;

            if (miFun.b_OcurrioError == false)
            {
                e_AlmDoc.n_idemp = Convert.ToInt32(dtResul.Rows[0]["n_idemp"].ToString());
                e_AlmDoc.n_idalm = Convert.ToInt32(dtResul.Rows[0]["n_idalm"].ToString());
                e_AlmDoc.n_idloc = Convert.ToInt32(dtResul.Rows[0]["n_idloc"].ToString());
                e_AlmDoc.n_idtipdoc = Convert.ToInt32(dtResul.Rows[0]["n_idtipdoc"].ToString());
                e_AlmDoc.c_numser = dtResul.Rows[0]["c_numser"].ToString();
                e_AlmDoc.c_numdoc = "";
                e_AlmDoc.n_id = Convert.ToInt32(dtResul.Rows[0]["n_id"].ToString());
                b_result = true;
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
                
            }
            return b_result;
        }
        public bool Insertar(BE_ALM_ALMACENESDOC entAlmacenesDoc)
        {
            CD_alm_almacenesdoc miFun = new CD_alm_almacenesdoc();
            bool booOk = false;
            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entAlmacenesDoc);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_ALM_ALMACENESDOC entAlmacenesDoc)
        {
            CD_alm_almacenesdoc miFun = new CD_alm_almacenesdoc();
            bool booOk = false;
            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entAlmacenesDoc);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_alm_almacenesdoc miFun = new CD_alm_almacenesdoc();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_Id);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
