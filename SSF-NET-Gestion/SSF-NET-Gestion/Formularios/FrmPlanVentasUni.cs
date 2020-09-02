using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Gestion;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Produccion;
using SIAC_Entidades.Gestion;
using SIAC_Entidades.Produccion;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using SSF_NET_Produccion;
using C1.Win.C1FlexGrid;
using SIAC_Negocio.Sunat;

namespace SSF_NET_Gestion.Formularios
{
    public partial class FrmPlanVentasUni : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sys_empresa o_empresa = new CN_sys_empresa();
        CN_pro_produccion o_produccion = new CN_pro_produccion();
        CN_mae_meses objMeses = new CN_mae_meses();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // DATATABLE LOCALES
        DataTable dtEmpresa = new DataTable();
        DataTable dtMeses = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[6, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[16, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890./" + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmPlanVentasUni()
        {
            InitializeComponent();
        }

        private void FrmPlanVentasUni_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }

        private void FrmPlanVentasUni_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                MostrarInfo();
            }
        }
        void MostrarInfo()
        {
            int n_row = 0;
            DataTable dtres = new DataTable();
            CN_ges_planventas o_plaven = new CN_ges_planventas();
            o_plaven.mysConec = mysConec;
            FgProEmp1.Rows.Count = 2;
            o_plaven.PlanVentasUnificado();                     // CARGAMOS PLAN DE VENTAS UNFIFIADO
            dtres = o_plaven.dtLista;

            if (o_plaven.booOcurrioError == false)
            {
                funFlex.b_AlternarColor = true;
                MostrarCabeceraMes(Convert.ToInt32(dtres.Rows[0]["n_idmesini"]), dtres);

                //funFlex.FlexMostrarDatos(FgProEmp1, arrCabeceraFlex1, dtres, 2, true);
            }
            else 
            {
                MessageBox.Show("¡ No se puede mostrar el plan de ventas unificado por el siguiente motivo: "+ o_plaven.StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            o_plaven = null;
        }
        void MostrarCabeceraMes(int n_MesInicio, DataTable dtDatos)
        {
            int n_col = 0;
            int n_posArray = 3;
            int n_nummes = n_MesInicio;
            DataTable dtResult = new DataTable();
            string c_nommes = "";

            for (n_col = 1; n_col <= 12; n_col++)
            {
                c_nommes = Convert.ToString(funDatos.DataTableBuscar(dtMeses, "n_id", "c_des", n_nummes.ToString(), "N"));
                arrCabeceraFlex1[n_posArray, 0] = c_nommes.ToLower();
                arrCabeceraFlex1[n_posArray, 1] = "70";
                arrCabeceraFlex1[n_posArray, 2] = "D";
                arrCabeceraFlex1[n_posArray, 3] = "0.00";
                arrCabeceraFlex1[n_posArray, 4] = c_nommes.ToLower();

                n_nummes = n_nummes + 1;
                n_posArray = n_posArray + 1;
                if (n_nummes == 13)
                {
                    n_nummes = 1;
                }
            }
            funFlex.FlexMostrarDatos(FgProEmp1, arrCabeceraFlex1, dtDatos, 2, true);
        }
        void Tab_Dimensionar(C1.Win.C1Command.C1DockingTab dokTab, int intAlto, int intAncho)
        {
            Tab1.Height = intAlto;
            Tab1.Width = intAncho;
        }
        void Tab_Posicionar(C1.Win.C1Command.C1DockingTab dokTab, int intPosX, int intPosY)
        {
            dokTab.Left = intPosX;
            dokTab.Top = intPosY;
        }
        void ConfigurarFormulario()
        {
            this.Text = "GESTION - Plan de Ventas Unificado";
            this.Height = 600;
            this.Width = 1000;
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;

            //TabEmpTP1.TabVisible = false;
            ConfigurarFlex();
        }
        void CargarCombos()
        {
            DataTableCargar();
        }
        void DataTableCargar()
        {
            o_empresa.mysConec = mysConec;
            o_empresa.Listar();
            dtEmpresa = o_empresa.dtLista;

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();
        }
        void ConfigurarFlex()
        {
            arrCabeceraFlex1[0, 0] = "IdItem";
            arrCabeceraFlex1[0, 1] = "0";
            arrCabeceraFlex1[0, 2] = "N";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "n_idpro";

            arrCabeceraFlex1[1, 0] = "Producto";
            arrCabeceraFlex1[1, 1] = "300";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_despro";

            arrCabeceraFlex1[2, 0] = "Uni. Med";
            arrCabeceraFlex1[2, 1] = "40";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_abrpre";

            arrCabeceraFlex1[3, 0] = "Enero";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "";
            arrCabeceraFlex1[3, 4] = "n_ene";

            arrCabeceraFlex1[4, 0] = "Febrero";
            arrCabeceraFlex1[4, 1] = "70";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.00";
            arrCabeceraFlex1[4, 4] = "n_feb";

            arrCabeceraFlex1[5, 0] = "Marzo";
            arrCabeceraFlex1[5, 1] = "70";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "n_mar";

            arrCabeceraFlex1[6, 0] = "Abril";
            arrCabeceraFlex1[6, 1] = "70";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_abr";

            arrCabeceraFlex1[7, 0] = "Mayo";
            arrCabeceraFlex1[7, 1] = "70";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "n_may";

            arrCabeceraFlex1[8, 0] = "Junio";
            arrCabeceraFlex1[8, 1] = "70";
            arrCabeceraFlex1[8, 2] = "D";
            arrCabeceraFlex1[8, 3] = "0.00";
            arrCabeceraFlex1[8, 4] = "n_jun";

            arrCabeceraFlex1[9, 0] = "Julio";
            arrCabeceraFlex1[9, 1] = "70";
            arrCabeceraFlex1[9, 2] = "D";
            arrCabeceraFlex1[9, 3] = "0.00";
            arrCabeceraFlex1[9, 4] = "n_jul";

            arrCabeceraFlex1[10, 0] = "Agosto";
            arrCabeceraFlex1[10, 1] = "70";
            arrCabeceraFlex1[10, 2] = "D";
            arrCabeceraFlex1[10, 3] = "0.00";
            arrCabeceraFlex1[10, 4] = "n_ago";

            arrCabeceraFlex1[11, 0] = "Setiembre";
            arrCabeceraFlex1[11, 1] = "70";
            arrCabeceraFlex1[11, 2] = "D";
            arrCabeceraFlex1[11, 3] = "0.00";
            arrCabeceraFlex1[11, 4] = "n_set";

            arrCabeceraFlex1[12, 0] = "Octubre";
            arrCabeceraFlex1[12, 1] = "70";
            arrCabeceraFlex1[12, 2] = "D";
            arrCabeceraFlex1[12, 3] = "0.00";
            arrCabeceraFlex1[12, 4] = "n_oct";

            arrCabeceraFlex1[13, 0] = "Noviembre";
            arrCabeceraFlex1[13, 1] = "70";
            arrCabeceraFlex1[13, 2] = "D";
            arrCabeceraFlex1[13, 3] = "0.00";
            arrCabeceraFlex1[13, 4] = "n_nov";

            arrCabeceraFlex1[14, 0] = "Diciembre";
            arrCabeceraFlex1[14, 1] = "70";
            arrCabeceraFlex1[14, 2] = "D";
            arrCabeceraFlex1[14, 3] = "0.00";
            arrCabeceraFlex1[14, 4] = "n_dic";

            arrCabeceraFlex1[15, 0] = "Total";
            arrCabeceraFlex1[15, 1] = "70";
            arrCabeceraFlex1[15, 2] = "D";
            arrCabeceraFlex1[15, 3] = "0.00";
            arrCabeceraFlex1[15, 4] = "n_canpro";

            funFlex.b_AlternarColor = false;
            funFlex.FlexMostrarDatos(FgProEmp1, arrCabeceraFlex1, dtEmpresa, 2, false);
        }
        private void ToolSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmPlanVentasUni_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }

        private void ToolAbastecimiento_Click(object sender, EventArgs e)
        {

        }
    }
}
