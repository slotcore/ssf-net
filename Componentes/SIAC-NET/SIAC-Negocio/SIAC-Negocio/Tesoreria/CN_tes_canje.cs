using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Sistema;
using SIAC_DATOS.Tesoreria;
using SIAC_DATOS.Ventas;
using SIAC_Entidades;
using SIAC_Entidades.Tesoreria;
using SIAC_Objetos.Constantes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using Helper.Comunes;


namespace SIAC_Negocio.Tesoreria
{
    public class CN_tes_canje
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public DataTable dtLista = new DataTable();
        public DataTable dtDocCompra = new DataTable();
        public DataTable dtDocVenta = new DataTable();

        public BE_TES_CANJE e_Canje = new BE_TES_CANJE();
        public List<BE_TES_CANJEDET> l_CanjeDet = new List<BE_TES_CANJEDET>();

        Funciones funFunciones = new Funciones();
        public void Listar(int n_IdEmpresa, int n_AñoTrabajo, int n_MesTrabajo)
        {
            CD_tes_canje miFun = new CD_tes_canje();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa, n_AñoTrabajo, n_MesTrabajo);
            dtLista = miFun.dtOrigen;

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            bool booResult;
            CD_tes_canje miFun = new CD_tes_canje();

            miFun.mysConec = mysConec;

            booResult = miFun.TraerRegistro(n_IdRegistro);
            dtResult = miFun.dtOrigen;
            dtDocCompra = miFun.dtDocCompra;
            dtDocVenta = miFun.dtDocVenta;
            if (dtResult.Rows.Count != 0)
            {
                e_Canje.n_idemp = Convert.ToInt32(dtResult.Rows[0]["n_idemp"]);
                e_Canje.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                e_Canje.n_idlib = Convert.ToInt32(dtResult.Rows[0]["n_idlib"]);
                e_Canje.n_ano = Convert.ToInt32(dtResult.Rows[0]["n_ano"]);
                e_Canje.n_mes = Convert.ToInt32(dtResult.Rows[0]["n_mes"]);
                e_Canje.d_fchreg = Convert.ToDateTime(dtResult.Rows[0]["d_fchreg"]);
                e_Canje.c_numreg = dtResult.Rows[0]["c_numreg"].ToString();
                e_Canje.c_numser = dtResult.Rows[0]["c_numser"].ToString();
                e_Canje.c_numdoc = dtResult.Rows[0]["c_numdoc"].ToString();
                e_Canje.d_fchemi = Convert.ToDateTime(dtResult.Rows[0]["d_fchemi"]);
                e_Canje.n_idpro = Convert.ToInt32(dtResult.Rows[0]["n_idpro"]);
                e_Canje.n_idcli = Convert.ToInt32(dtResult.Rows[0]["n_idcli"]);
                e_Canje.n_idmon = Convert.ToInt32(dtResult.Rows[0]["n_idmon"]);
                e_Canje.n_impcan = Convert.ToDouble(dtResult.Rows[0]["n_impcan"]);
                e_Canje.n_tc = Convert.ToDouble(dtResult.Rows[0]["n_tc"]);
            }
            if (booResult == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            CD_tes_canje miFun = new CD_tes_canje();

            miFun.mysConec = mysConec;

            booResult = miFun.Eliminar(n_IdRegistro);
            if (booResult == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_TES_CANJE e_Origen, List<BE_TES_CANJEDET> l_CanjeDocumentos)
        {
            CD_tes_canje miFun = new CD_tes_canje();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_Origen, l_CanjeDocumentos);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_TES_CANJE e_Origen, List<BE_TES_CANJEDET> l_CanjeDocumentos)
        {
            CD_tes_canje miFun = new CD_tes_canje();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_Origen, l_CanjeDocumentos);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
