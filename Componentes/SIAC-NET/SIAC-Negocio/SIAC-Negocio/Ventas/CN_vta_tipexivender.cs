using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Ventas;
using SIAC_DATOS.Ventas;
using MySql.Data.MySqlClient;
using Helper;
namespace SIAC_Negocio.Ventas
{
    public class CN_vta_tipexivender
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        
        CD_vta_tipexivender miFun = new CD_vta_tipexivender();
        Genericas o_fungen = new Genericas();
        public void Listar(int n_IdEmpresa)
        {
            miFun.mysConec = mysConec;
            miFun.Listar(n_IdEmpresa);

            if (miFun.b_OcurrioError == true)
            {
                dtLista = null;
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                dtLista = miFun.dtLista;
            }

            return;
        }
        public string CadenaIN(int n_IdEmpresa)
        {
            string c_cad = "";
            miFun.mysConec = mysConec;
            miFun.Listar(n_IdEmpresa);

            if (miFun.b_OcurrioError == true)
            {
                dtLista = null;
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                dtLista = miFun.dtLista;
                c_cad = o_fungen.DataTableCadenaIN(dtLista, "n_idtipexi");
            }
            return c_cad;
        }
        public BE_VTA_TIPEXIVENDER TraerRegistro(int n_IdEmpresa, int n_IdTipoExistenciaVender)
        {
            BE_VTA_TIPEXIVENDER e_obj = new BE_VTA_TIPEXIVENDER();
            DataTable dtresul = new DataTable();
            miFun.mysConec = mysConec;
            miFun.TraerRegistro(n_IdEmpresa, n_IdTipoExistenciaVender);
            dtresul = miFun.dtLista;

            if (dtresul.Rows.Count != 9)
            {
                e_obj.n_idemp = Convert.ToInt32(dtresul.Rows[0]["n_idemp"]);
                e_obj.n_idtipexi = Convert.ToInt32(dtresul.Rows[0]["n_idtipexi"]);
            }
            return e_obj;
        }
        public bool Insertar(BE_VTA_TIPEXIVENDER e_TipoExistenciaVender)
        {
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_TipoExistenciaVender);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_VTA_TIPEXIVENDER e_TipoExistenciaVender)
        {
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_TipoExistenciaVender);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_IdEmpresa, int n_IdTipoExistencia)
        {
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Eliminar(n_IdEmpresa, n_IdTipoExistencia);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
