using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Contabilidad;
using SIAC_DATOS.Logistica;
using SIAC_DATOS.Ventas;
using SIAC_DATOS.Tesoreria;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using Helper;

namespace SIAC_Negocio.Contabilidad
{
    public class CN_con_pcitems
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;

        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public DataTable dtResumen = new DataTable();
        public BE_CON_PCITEMS e_Items = new BE_CON_PCITEMS();
        
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public string c_NewNumAsiento;

        Helper.Comunes.Funciones funFunciones = new Helper.Comunes.Funciones();
        public void Listar(int n_IdEmpresa, int n_Tipo)
        {
            CD_con_pcitems miFun = new CD_con_pcitems();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa, n_Tipo);
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return;
        }
        public void TraerRegistro(int n_IdRegistro, int n_IdEmpresa)
        {
            CD_con_pcitems miFun = new CD_con_pcitems();
            miFun.mysConec = mysConec;
            e_Items = new BE_CON_PCITEMS();
            miFun.TraerRegistro(n_IdRegistro, n_IdEmpresa);
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                e_Items = null;
                if (dtLista.Rows.Count != 0)
                { 
                    e_Items = new BE_CON_PCITEMS();
                    e_Items.n_idemp = Convert.ToInt32(dtLista.Rows[0]["n_idemp"]);
                    e_Items.n_idite = Convert.ToInt32(dtLista.Rows[0]["n_idite"]);
                    e_Items.n_idpc = Convert.ToInt32(funFunciones.NulosN(dtLista.Rows[0]["n_idpc"]));
                    e_Items.n_idpcven = Convert.ToInt32(funFunciones.NulosN(dtLista.Rows[0]["n_idpcven"]));
                    e_Items.n_iddet = Convert.ToInt32(funFunciones.NulosN(dtLista.Rows[0]["n_iddet"]));
                    e_Items.n_idper = Convert.ToInt32(funFunciones.NulosN(dtLista.Rows[0]["n_idper"]));
                    e_Items.n_idret = Convert.ToInt32(funFunciones.NulosN(dtLista.Rows[0]["n_idret"]));
                    e_Items.n_idigvcom = Convert.ToInt32(funFunciones.NulosN(dtLista.Rows[0]["n_idigvcom"]));
                    e_Items.n_idigvven = Convert.ToInt32(funFunciones.NulosN(dtLista.Rows[0]["n_idigvven"]));
                    e_Items.n_idisc = Convert.ToInt32(funFunciones.NulosN(dtLista.Rows[0]["n_idisc"]));
                }
            }
            return;
        }
        public bool Eliminar(int n_Idregistro)
        {
            bool b_result = false;
            CD_con_pcitems miFun = new CD_con_pcitems();

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
        public bool Insertar(BE_CON_PCITEMS e_Items)
        {
            bool b_result = false;
            CD_con_pcitems miFun = new CD_con_pcitems();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(e_Items);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(BE_CON_PCITEMS e_Items)
        {
            bool b_result = false;
            CD_con_pcitems miFun = new CD_con_pcitems();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(e_Items);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public void ListarItemCtaventa(int n_IdEmpresa)
        {
            CD_con_pcitems miFun = new CD_con_pcitems();
            miFun.mysConec = mysConec;

            miFun.ListarItemCtaventa(n_IdEmpresa);
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return;
        }
        public DataTable BuscarItemPendiente(int n_IdEmpresa)
        {
            string[,] arrCabeceraDg1 = new string[4, 4];
            DataTable dtResult = new DataTable();
            //DataTable dtresulpre = new DataTable();
            CD_con_pcitems o_items = new CD_con_pcitems();
            Helper.Genericas funDatos = new Helper.Genericas();

            o_items.mysConec = mysConec;
            o_items.Consulta1(n_IdEmpresa);
            dtResult = o_items.dtLista;

            arrCabeceraDg1[0, 0] = "Descripcion";
            arrCabeceraDg1[0, 1] = "400";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_despro";

            arrCabeceraDg1[1, 0] = "Codigo";
            arrCabeceraDg1[1, 1] = "110";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_codpro";

            arrCabeceraDg1[2, 0] = "Uni. Med";
            arrCabeceraDg1[2, 1] = "50";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "c_abrpre";

            arrCabeceraDg1[3, 0] = "Id";
            arrCabeceraDg1[3, 1] = "0";
            arrCabeceraDg1[3, 2] = "N";
            arrCabeceraDg1[3, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "c_despro";
            xFun.Buscar_CadFiltro = "";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            return dtResult;
        }
        public void Consulta2(int n_IdEmpresa)
        {
            CD_con_pcitems miFun = new CD_con_pcitems();
            miFun.mysConec = mysConec;

            miFun.Consulta2(n_IdEmpresa);
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return;
        }
    }
}
