using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Ventas;
using SIAC_Objetos.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIAC_Negocio.Tesoreria;

namespace SSF_NET_Ventas.Formularios
{
    public partial class FrmRepMarketing : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_TipoReporte;                                                   // 1 = PARETO DE CLIENTES;   2 = PARETO DE ITEMS
        

        DataTable dtTipExi = new DataTable();
        DataTable dtProv = new DataTable();
        DataTable dtItem = new DataTable();
        DataTable dtLista = new DataTable();
        DataTable dtEmp = new DataTable();

        CN_sys_empresa o_empresa = new CN_sys_empresa();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();

        string[,] arrCabecera1 = new string[5, 5];
        string[,] arrCabecera2 = new string[6, 5];
        public FrmRepMarketing()
        {
            InitializeComponent();
        }

        private void FrmRepMarketing_Load(object sender, EventArgs e)
        {
            CargarDT();
            ConfigurarFormulario();
        }
        void CargarDT()
        {
            o_empresa.mysConec = mysConec;
            o_empresa.Listar();
            dtEmp = o_empresa.dtLista;
            funDatos.ComboBoxCargarDataTable(CboTipPro, dtEmp, "n_id", "c_nomemp");
        }
        void ConfigurarFormulario()
        {
            this.Height = 600;
            this.Width = 1094;

            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;

           

            OptPen.Checked = true;

            TxtFchIni.Text = "01/01/" + STU_SISTEMA.ANOTRABAJO.ToString();
            CboTipPro.SelectedValue = STU_SISTEMA.EMPRESAID;
            FgDatos.Cols.Count = 5;

            funFlex.b_AlternarColor = true;
            
            if (n_TipoReporte == 1)
            {
                this.Text = "VENTAS - VENTAS X CLIENTE";
                Cabecera1();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, false);
            }
            if (n_TipoReporte == 2)
            {
                this.Text = "VENTAS - VENTAS X ITEMS";
                Cabecera2();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera2, dtLista, 2, false);
            }

        }
        void Cabecera1()
        {
            arrCabecera1[0, 0] = "Nº R.U.C.";
            arrCabecera1[0, 1] = "110";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_numdoc";

            arrCabecera1[1, 0] = "Cliente";
            arrCabecera1[1, 1] = "300";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_nombre";

            arrCabecera1[2, 0] = "Nº Doc. Emitidos";
            arrCabecera1[2, 1] = "70";
            arrCabecera1[2, 2] = "N";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "n_candoc";

            arrCabecera1[3, 0] = "Importe";
            arrCabecera1[3, 1] = "80";
            arrCabecera1[3, 2] = "D";
            arrCabecera1[3, 3] = "0.00";
            arrCabecera1[3, 4] = "n_impsol";

            arrCabecera1[4, 0] = "% Part.";
            arrCabecera1[4, 1] = "80";
            arrCabecera1[4, 2] = "D";
            arrCabecera1[4, 3] = "0.00";
            arrCabecera1[4, 4] = "n_por";
        }
        void Cabecera2()
        {
            arrCabecera2[0, 0] = "Cod. Producto";
            arrCabecera2[0, 1] = "110";
            arrCabecera2[0, 2] = "C";
            arrCabecera2[0, 3] = "";
            arrCabecera2[0, 4] = "c_codpro";

            arrCabecera2[1, 0] = "Producto / Mercaderia / Servicio";
            arrCabecera2[1, 1] = "300";
            arrCabecera2[1, 2] = "C";
            arrCabecera2[1, 3] = "";
            arrCabecera2[1, 4] = "c_despro";

            arrCabecera2[2, 0] = "Uni. Med.";
            arrCabecera2[2, 1] = "40";
            arrCabecera2[2, 2] = "C";
            arrCabecera2[2, 3] = "";
            arrCabecera2[2, 4] = "c_abrpre";

            arrCabecera2[3, 0] = "Cantidad";
            arrCabecera2[3, 1] = "80";
            arrCabecera2[3, 2] = "D";
            arrCabecera2[3, 3] = "0.00";
            arrCabecera2[3, 4] = "n_cantot";

            arrCabecera2[4, 0] = "Importe";
            arrCabecera2[4, 1] = "80";
            arrCabecera2[4, 2] = "D";
            arrCabecera2[4, 3] = "0.00";
            arrCabecera2[4, 4] = "n_imptot";

            arrCabecera2[5, 0] = "% Part.";
            arrCabecera2[5, 1] = "80";
            arrCabecera2[5, 2] = "D";
            arrCabecera2[5, 3] = "0.00";
            arrCabecera2[5, 4] = "n_por";
        }
        private void FrmCtaCtePedidos_Resize(object sender, EventArgs e)
        {
            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolBuscar_Click(object sender, EventArgs e)
        {
            EjecutarConsulta();
        }
        void EjecutarConsulta()
        {
            if (funFunciones.NulosC(TxtFchIni.Text) == "")
            {
                MessageBox.Show("¡ No ha indicado la fecha de inicio para la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }
            if (funFunciones.NulosC(TxtFchFin.Text) == "")
            {
                MessageBox.Show("¡ No ha indicado la fecha final para la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchFin.Focus();
                return;
            }
            if (OptPen.Checked == true)
            {
                if (Convert.ToInt32(CboTipPro.SelectedValue) == 0)
                { 
                    MessageBox.Show("¡ Debe de seleccionar una empresa !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    CboTipPro.Focus();
                    return;
                }
            }

            DateTime d_fchini = Convert.ToDateTime(TxtFchIni.Text);
            DateTime d_fchfin = Convert.ToDateTime(TxtFchFin.Text);

            if (d_fchini > d_fchfin)
            {
                MessageBox.Show("¡ La fecha de inicio no puede ser mayor a la fecha final de la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }

            //int m_tiprep = 0;
            CN_vta_ventas o_ventas = new CN_vta_ventas();
            o_ventas.mysConec = mysConec;
            o_ventas.STU_SISTEMA = STU_SISTEMA;

            if (n_TipoReporte == 1)
            { 
                o_ventas.VentasAnualesxCliente(Convert.ToInt32(CboTipPro.SelectedValue), TxtFchIni.Text, TxtFchFin.Text);
                dtLista = o_ventas.dtLista1;
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, true);
            }
            if (n_TipoReporte == 2)
            {
                o_ventas.VentasAnualesxItems(Convert.ToInt32(CboTipPro.SelectedValue), TxtFchIni.Text, TxtFchFin.Text);
                dtLista = o_ventas.dtLista1;
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera2, dtLista, 2, true);
            }
            MessageBox.Show("¡ Los datos se mostraron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            FgDatos.Focus();
        }
        void SetearCabecera1()
        {
            //funFlex.Flex_UniColumnas(FgDatos, 0, 1, 9, "DATOS DEL PEDIDO", 1);
            //funFlex.Flex_UniColumnas(FgDatos, 0, 10, 13, "DATOS DE FACTURACION", 1);
        }

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_nomarch = "";
            funFlex.ExportToExcel_NumFilaCabecera = 2;
            if (n_TipoReporte == 1)
            { 
                c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-PARETO-CLIENTES" + ".xls";
            }
            if (n_TipoReporte == 2)
            {
                c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-PARETO-ITEMS" + ".xls";
            }
            funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "VENTAS ACUMULADAS POR PERIODO", "DEL " + TxtFchIni.Text + "HASTA " + TxtFchIni.Text, c_nomarch);
        }

        private void OpTod_CheckedChanged(object sender, EventArgs e)
        {
            CboTipPro.SelectedValue = 0;
            CboTipPro.Enabled = false;
        }

        private void OptPen_CheckedChanged(object sender, EventArgs e)
        {
            CboTipPro.SelectedValue = 0;
            CboTipPro.Enabled = true;
        }

        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
