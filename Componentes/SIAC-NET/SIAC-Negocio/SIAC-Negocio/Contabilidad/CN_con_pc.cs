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
    public class CN_con_pc
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public BE_CON_PC e_Pc = new BE_CON_PC();
        public void Listar(int n_IdEmpresa)
        {
            CD_con_pc miFun = new CD_con_pc();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa);
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return;
        }
        public bool BuscarNumeroCuenta(string c_NumeroCuenta, int n_IdEmpresa)
        {
            bool b_result = false;
            CD_con_pc miFun = new CD_con_pc();
            miFun.mysConec = mysConec;

            miFun.BuscarNumeroCuenta(c_NumeroCuenta, n_IdEmpresa);
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                if (dtLista.Rows.Count != 0)
                {
                    b_result = true;
                }
            }
            return b_result;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            CD_con_pc miFun = new CD_con_pc();
            miFun.mysConec = mysConec;

            miFun.TraerRegistro(n_IdRegistro);
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                e_Pc.n_id = Convert.ToInt32(dtLista.Rows[0]["n_id"]);
                e_Pc.c_cuecon = dtLista.Rows[0]["c_cuecon"].ToString();
                e_Pc.c_des = dtLista.Rows[0]["c_des"].ToString();
                e_Pc.n_ctadesdeb = Convert.ToInt32(dtLista.Rows[0]["n_ctadesdeb"]);
                e_Pc.n_ctadeshab = Convert.ToInt32(dtLista.Rows[0]["n_ctadeshab"]);
                e_Pc.n_iddes = Convert.ToInt32(dtLista.Rows[0]["n_iddes"]);
                e_Pc.n_iddes2 = Convert.ToInt32(dtLista.Rows[0]["n_iddes2"]);
                e_Pc.n_iddes3 = Convert.ToInt32(dtLista.Rows[0]["n_iddes3"]);
                e_Pc.n_tipo = Convert.ToInt32(dtLista.Rows[0]["n_tipo"]);
                e_Pc.c_tipsal = dtLista.Rows[0]["c_tipsal"].ToString();
                //e_Pc.n_dissegsal = Convert.ToInt32(dtLista.Rows[0]["n_dissegsal"]);
                e_Pc.n_documentar = Convert.ToInt32(dtLista.Rows[0]["n_documentar"]);
                e_Pc.n_idmodulo = Convert.ToInt32(dtLista.Rows[0]["n_idmodulo"]);
                e_Pc.n_desctabal = Convert.ToInt32(dtLista.Rows[0]["n_desctabal"]);
            }
            return;
        }
        public bool Eliminar(int n_Idregistro)
        {
            bool b_result = false;
            CD_con_pc miFun = new CD_con_pc();

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
        public bool Insertar(BE_CON_PC e_Cuenta)
        {
            bool b_result = false;
            CD_con_pc miFun = new CD_con_pc();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(e_Cuenta);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(BE_CON_PC e_Cuenta)
        {
            bool b_result = false;
            CD_con_pc miFun = new CD_con_pc();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(e_Cuenta);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public DataTable BuscarCuenta(DataTable DtPlanCuenta)
        {
            string[,] arrCabeceraDg1 = new string[4, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            Helper.Genericas funDatos = new Helper.Genericas();

            dtResult = DtPlanCuenta;
            //dtResult = funDatos.DataTableFiltrar(DtCLiPro, "n_idtipreg = " + n_TipoRegistro + "");

            arrCabeceraDg1[0, 0] = "Nº Cuenta";
            arrCabeceraDg1[0, 1] = "80";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_cuecon";

            arrCabeceraDg1[1, 0] = "Descripcion";
            arrCabeceraDg1[1, 1] = "300";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_des";

            arrCabeceraDg1[2, 0] = "Divicionaria";
            arrCabeceraDg1[2, 1] = "40";
            arrCabeceraDg1[2, 2] = "B";
            arrCabeceraDg1[2, 3] = "n_tipo";

            arrCabeceraDg1[3, 0] = "Id";
            arrCabeceraDg1[3, 1] = "0";
            arrCabeceraDg1[3, 2] = "N";
            arrCabeceraDg1[3, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "c_cuecon";
            xFun.Buscar_CadFiltro = "";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            return dtResult;
        }
    }
}
