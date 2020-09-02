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
    public partial class FrmConsultarProduccion : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sys_empresa o_empresa = new CN_sys_empresa();
        CN_pro_produccion o_produccion = new CN_pro_produccion();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // DATATABLE LOCALES
        DataTable dtEmpresa = new DataTable();
        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[6, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[8, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890./" + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmConsultarProduccion()
        {
            InitializeComponent();
        }
        private void FrmConsultarProduccion_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        private void FrmConsultarProduccion_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                MostrarInfo();
            }
        }
        void MostrarInfo()
        {
            CN_pro_produccion o_proint = new CN_pro_produccion();
            CN_pro_produccion o_proter = new CN_pro_produccion();

            DataTable dtInt = new DataTable();
            DataTable dtTer = new DataTable();
            int n_row = 0;

            for (n_row =0; n_row<= dtEmpresa.Rows.Count - 1; n_row++)
            {
                if (n_row == 0) { TabEmpTP1.Text = dtEmpresa.Rows[n_row]["c_nomcorto"].ToString(); TabEmpTP1.TabVisible = true; }
                if (n_row == 1) { TabEmpTP2.Text = dtEmpresa.Rows[n_row]["c_nomcorto"].ToString(); TabEmpTP2.TabVisible = true; }
                if (n_row == 2) { TabEmpTP3.Text = dtEmpresa.Rows[n_row]["c_nomcorto"].ToString(); TabEmpTP3.TabVisible = true; }
                if (n_row == 3) { TabEmpTP4.Text = dtEmpresa.Rows[n_row]["c_nomcorto"].ToString(); TabEmpTP4.TabVisible = true; }
                if (n_row == 4) { TabEmpTP5.Text = dtEmpresa.Rows[n_row]["c_nomcorto"].ToString(); TabEmpTP5.TabVisible = true; }
                if (n_row == 5) { TabEmpTP6.Text = dtEmpresa.Rows[n_row]["c_nomcorto"].ToString(); TabEmpTP6.TabVisible = true; }
                if (n_row == 6) { TabEmpTP7.Text = dtEmpresa.Rows[n_row]["c_nomcorto"].ToString(); TabEmpTP7.TabVisible = true; }
                if (n_row == 7) { TabEmpTP8.Text = dtEmpresa.Rows[n_row]["c_nomcorto"].ToString(); TabEmpTP8.TabVisible = true; }

                o_proint.mysConec = mysConec;
                o_proter.mysConec = mysConec;
                
                o_proint.Consulta12(Convert.ToInt32(dtEmpresa.Rows[n_row]["n_id"]), STU_SISTEMA.ANOTRABAJO,1);  // CARGAMOS PRODUCTOS INTERMEDIOS
                o_proter.Consulta12(Convert.ToInt32(dtEmpresa.Rows[n_row]["n_id"]), STU_SISTEMA.ANOTRABAJO,2);  // CARGAMOS PRODUCTOS TERMINADOS
                dtInt = o_proint.dtListar;
                dtTer = o_proter.dtListar;

                funFlex.b_AlternarColor = false;
                if (n_row == 0) 
                {
                    funFlex.FlexMostrarDatos(FgProEmp1, arrCabeceraFlex1, dtTer, 2, true);
                    funFlex.FlexMostrarDatos(FgIntEmp1, arrCabeceraFlex1, dtInt, 2, true);
                }
                if (n_row == 3)
                {
                    funFlex.FlexMostrarDatos(FgProEmp4, arrCabeceraFlex1, dtTer, 2, true);
                    funFlex.FlexMostrarDatos(FgIntEmp4, arrCabeceraFlex1, dtInt, 2, true);
                }
            }
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
            this.Text = "GESTION - Consulta de Produccion Unificado";
            this.Height = 600;
            this.Width = 1000;
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;

            TabEmpTP1.TabVisible = false;
            TabEmpTP2.TabVisible = false;
            TabEmpTP3.TabVisible = false;
            TabEmpTP4.TabVisible = false;
            TabEmpTP5.TabVisible = false;
            TabEmpTP6.TabVisible = false;
            TabEmpTP7.TabVisible = false;
            TabEmpTP8.TabVisible = false;

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

            arrCabeceraFlex1[3, 0] = "Programado";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "";
            arrCabeceraFlex1[3, 4] = "n_totprogra";

            arrCabeceraFlex1[4, 0] = "Stock Ini.";
            arrCabeceraFlex1[4, 1] = "80";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.00";
            arrCabeceraFlex1[4, 4] = "n_stkini";

            arrCabeceraFlex1[5, 0] = "Total Producido";
            arrCabeceraFlex1[5, 1] = "80";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "n_totpro";

            arrCabeceraFlex1[6, 0] = "Stock Actual";
            arrCabeceraFlex1[6, 1] = "80";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_total";

            arrCabeceraFlex1[7, 0] = "Por Producir";
            arrCabeceraFlex1[7, 1] = "80";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "n_porpro";

            funFlex.b_AlternarColor = false;
            funFlex.FlexMostrarDatos(FgProEmp1, arrCabeceraFlex1, dtEmpresa, 2, false);
            funFlex.FlexMostrarDatos(FgIntEmp1, arrCabeceraFlex1, dtEmpresa, 2, false);

            funFlex.FlexMostrarDatos(FgProEmp2, arrCabeceraFlex1, dtEmpresa, 2, false);
            funFlex.FlexMostrarDatos(FgIntEmp2, arrCabeceraFlex1, dtEmpresa, 2, false);

            funFlex.FlexMostrarDatos(FgProEmp3, arrCabeceraFlex1, dtEmpresa, 2, false);
            funFlex.FlexMostrarDatos(FgIntEmp3, arrCabeceraFlex1, dtEmpresa, 2, false);

            funFlex.FlexMostrarDatos(FgProEmp4, arrCabeceraFlex1, dtEmpresa, 2, false);
            funFlex.FlexMostrarDatos(FgIntEmp4, arrCabeceraFlex1, dtEmpresa, 2, false);

            funFlex.FlexMostrarDatos(FgProEmp5, arrCabeceraFlex1, dtEmpresa, 2, false);
            funFlex.FlexMostrarDatos(FgIntEmp5, arrCabeceraFlex1, dtEmpresa, 2, false);

            funFlex.FlexMostrarDatos(FgProEmp6, arrCabeceraFlex1, dtEmpresa, 2, false);
            funFlex.FlexMostrarDatos(FgIntEmp6, arrCabeceraFlex1, dtEmpresa, 2, false);

            funFlex.FlexMostrarDatos(FgProEmp7, arrCabeceraFlex1, dtEmpresa, 2, false);
            funFlex.FlexMostrarDatos(FgIntEmp7, arrCabeceraFlex1, dtEmpresa, 2, false);

            funFlex.FlexMostrarDatos(FgProEmp8, arrCabeceraFlex1, dtEmpresa, 2, false);
            funFlex.FlexMostrarDatos(FgIntEmp8, arrCabeceraFlex1, dtEmpresa, 2, false);
        }
        private void ToolSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmConsultarProduccion_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }
    }
}
