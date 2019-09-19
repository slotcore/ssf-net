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
    public partial class FrmCtaCtePedidos : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Libro;
        public int n_Origen;                                                    // INDICA EL ORIGEN DE LA GUIA     1 = VENTAS ;   2 = TRASLADO ENTRE ALMACENES

        DataTable dtTipExi = new DataTable();
        DataTable dtProv = new DataTable();
        DataTable dtItem = new DataTable();
        DataTable dtLista = new DataTable();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();

        string[,] arrCabecera1 = new string[13, 5];
        public FrmCtaCtePedidos()
        {
            InitializeComponent();
        }

        private void FrmCtaCtePedidos_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarDT();
        }
        void CargarDT()
        {
            //objPro.mysConec = mysConec;
            //dtProv = objPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
        }
        void ConfigurarFormulario()
        {
            this.Height = 600;
            this.Width = 1094;

            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;

            this.Text = "VENTAS - CUENTA CORRIENTE PEDIDOS";

            OptPen.Checked = true;

            TxtFchIni.Text = "01/01/" + STU_SISTEMA.ANOTRABAJO.ToString();

            FgDatos.Cols.Count = 5;
            Cabecera1();
            funFlex.b_AlternarColor = false;
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, false);
            SetearCabecera1();
        }
        void Cabecera1()
        {
            arrCabecera1[0, 0] = "Tip. Doc";
            arrCabecera1[0, 1] = "40";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_abr";

            arrCabecera1[1, 0] = "Nº Pedido";
            arrCabecera1[1, 1] = "100";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_pednumdoc";

            arrCabecera1[2, 0] = "Fch. Pedido";
            arrCabecera1[2, 1] = "70";
            arrCabecera1[2, 2] = "F";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "d_fchdoc";

            arrCabecera1[3, 0] = "Condicion";
            arrCabecera1[3, 1] = "100";
            arrCabecera1[3, 2] = "C";
            arrCabecera1[3, 3] = "";
            arrCabecera1[3, 4] = "c_conpagdes";

            arrCabecera1[4, 0] = "Producto";
            arrCabecera1[4, 1] = "300";
            arrCabecera1[4, 2] = "C";
            arrCabecera1[4, 3] = "";
            arrCabecera1[4, 4] = "c_despro";

            arrCabecera1[5, 0] = "Uni. Med";
            arrCabecera1[5, 1] = "50";
            arrCabecera1[5, 2] = "C";
            arrCabecera1[5, 3] = "";
            arrCabecera1[5, 4] = "c_abrpre";

            arrCabecera1[6, 0] = "Fch. Entrega";
            arrCabecera1[6, 1] = "70";
            arrCabecera1[6, 2] = "F";
            arrCabecera1[6, 3] = "";
            arrCabecera1[6, 4] = "d_fchent";

            arrCabecera1[7, 0] = "Cantidad";
            arrCabecera1[7, 1] = "70";
            arrCabecera1[7, 2] = "D";
            arrCabecera1[7, 3] = "";
            arrCabecera1[7, 4] = "n_can";

            arrCabecera1[8, 0] = "Cantidad Entregada";
            arrCabecera1[8, 1] = "70";
            arrCabecera1[8, 2] = "D";
            arrCabecera1[8, 3] = "";
            arrCabecera1[8, 4] = "n_canent";

            arrCabecera1[9, 0] = "T.D.";
            arrCabecera1[9, 1] = "40";
            arrCabecera1[9, 2] = "C";
            arrCabecera1[9, 3] = "";
            arrCabecera1[9, 4] = "c_factipdoc";

            arrCabecera1[10, 0] = "Nº Factura";
            arrCabecera1[10, 1] = "110";
            arrCabecera1[10, 2] = "C";
            arrCabecera1[10, 3] = "";
            arrCabecera1[10, 4] = "c_facnumdoc";

            arrCabecera1[11, 0] = "Fecha Doc.";
            arrCabecera1[11, 1] = "70";
            arrCabecera1[11, 2] = "F";
            arrCabecera1[11, 3] = "";
            arrCabecera1[11, 4] = "d_facfchdoc";

            arrCabecera1[12, 0] = "Clientes";
            arrCabecera1[12, 1] = "200";
            arrCabecera1[12, 2] = "C";
            arrCabecera1[12, 3] = "";
            arrCabecera1[12, 4] = "c_facnomcli";
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

            DateTime d_fchini = Convert.ToDateTime(TxtFchIni.Text);
            DateTime d_fchfin = Convert.ToDateTime(TxtFchFin.Text);

            if (d_fchini > d_fchfin)
            {
                MessageBox.Show("¡ La fecha de inicio no puede ser mayor a la fecha final de la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }

            int m_tiprep = 0;
            CN_vta_pedidocli o_ventas = new CN_vta_pedidocli();
            o_ventas.mysConec = mysConec;
            o_ventas.STU_SISTEMA = STU_SISTEMA;

            if (OpTod.Checked == true) { m_tiprep = 1; }
            if (OptPen.Checked == true) { m_tiprep = 2; }
            if (OptEnt.Checked == true) { m_tiprep = 3; }

            o_ventas.CtaCtePedidos(STU_SISTEMA.EMPRESAID, TxtFchIni.Text, TxtFchFin.Text, m_tiprep);
            dtLista = o_ventas.dtLista;

            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, true);
            SetearCabecera1();
            MessageBox.Show("¡ Los datos se mostraron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            FgDatos.Focus();
        }
        void SetearCabecera1()
        {
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 1, 9, "DATOS DEL PEDIDO", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 10, 13, "DATOS DE FACTURACION", 1);
        }
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_cad = "";
            string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-CTACTEPEDIDOS-" + ".xls";
            if (OpTod.Checked == true) { c_cad = "IMPORTES"; }
            if (OptPen.Checked == true) { c_cad = "CANTIDADES"; }
            funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "CUENTA CTE. PEDIDOS - CLIENTES", c_cad, c_nomarch);
        }
    }
}
