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
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sunat;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;

namespace SSF_NET_Almacen.Formularios
{
    public partial class FrmKardexResumen2 : Form
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public bool b_esvalorizado;

        public string c_anostra;
        public DataTable dtItems = new DataTable();
        public int n_IdItem;
        public DataTable dtHistorico = new DataTable();
        
        // OBJETOS DE ACCESO A DATOS
        Cls_DBGrid o_grid = new Cls_DBGrid();
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        DataTable DtMovimientos = new DataTable();
        DataTable DtTipExi = new DataTable();
        DataTable dtmes = new DataTable();
        DataTable dtalm = new DataTable();

        CN_alm_movimientos obMov = new CN_alm_movimientos();
        CN_sun_tipexi objtipexi = new CN_sun_tipexi();
        CN_mae_meses objmes = new CN_mae_meses();
        CN_alm_almacenes o_alm = new CN_alm_almacenes();

        bool booSeEjecuto = false;
        string[,] arrCabeceraFlex1 = new string[9, 4];
        public FrmKardexResumen2()
        {
            InitializeComponent();
        }

        private void FrmKardexResumen2_Load(object sender, EventArgs e)
        {
            this.Text = "ALMACEN - KARDEX";
            CargarDatos();
            TxtAñoTra.Minimum = 2006;
            TxtAñoTra.Maximum = DateTime.Now.Year;
            TxtAñoTra.Value = DateTime.Now.Year;
            this.Width = 1086;
            this.Height = 623;
            Dimensionar(c1Sizer1, this.Height - 80, this.Width - 19);
            TxtFchIni.Text = "01/01/" + TxtAñoTra.Value.ToString();
            TxtFchFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        void CargarCombos()
        {
            funDatos.ComboBoxCargarDataTable(CboTipExi, DtTipExi, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboAlmacen, dtalm, "n_id", "c_des");
        }
        void CargarDatos()
        {
            objtipexi.mysConec = mysConec;
            DtTipExi = objtipexi.Listar();

            objmes.mysConec = mysConec;
            dtmes = objmes.Listar();

            o_alm.mysConec = mysConec;
            dtalm = o_alm.ListarNuevo(STU_SISTEMA.EMPRESAID, 1);

            CargarCombos();
        }
        void ConfigurarGrid()
        {
            arrCabeceraFlex1[0, 0] = "Tipo de Existencia";
            arrCabeceraFlex1[0, 1] = "150";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Codigo Producto";
            arrCabeceraFlex1[1, 1] = "110";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "c_codpro";

            arrCabeceraFlex1[2, 0] = "Descipcion Item";
            arrCabeceraFlex1[2, 1] = "300";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "c_despro";

            arrCabeceraFlex1[3, 0] = "Uni. Med";
            arrCabeceraFlex1[3, 1] = "40";
            arrCabeceraFlex1[3, 2] = "C";
            arrCabeceraFlex1[3, 3] = "c_abrepre";

            arrCabeceraFlex1[4, 0] = "Stock Inicial";
            arrCabeceraFlex1[4, 1] = "80";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "n_stkini";

            arrCabeceraFlex1[5, 0] = "Ingresos";
            arrCabeceraFlex1[5, 1] = "80";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "n_toting";

            arrCabeceraFlex1[6, 0] = "Salida";
            arrCabeceraFlex1[6, 1] = "80";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "n_totsal";

            arrCabeceraFlex1[7, 0] = "Saldo";
            arrCabeceraFlex1[7, 1] = "80";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "n_saldo";

            arrCabeceraFlex1[8, 0] = "ID";
            arrCabeceraFlex1[8, 1] = "0";
            arrCabeceraFlex1[8, 2] = "N";
            arrCabeceraFlex1[8, 3] = "n_id";

            o_grid.DG_FormatearGrid(DgLista, arrCabeceraFlex1, dtItems, true);
        }
        private void FrmKardexResumen2_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                ConfigurarGrid();
                booSeEjecuto = true;
            }
        }
        void MostrarDatos()
        {
            int n_idalm = Convert.ToInt32(CboAlmacen.SelectedValue);
            int n_tipexit = Convert.ToInt32(CboTipExi.SelectedValue);
            obMov.mysConec = mysConec;
            DtMovimientos = obMov.VerKardexResumido(STU_SISTEMA.EMPRESAID, Convert.ToInt32(TxtAñoTra.Value), TxtFchIni.Text, TxtFchFin.Text, n_tipexit, n_idalm);

            if (DtMovimientos != null)
            {
                if (DtMovimientos.Rows.Count != 0)
                {
                    o_grid.DG_FormatearGrid(DgLista, arrCabeceraFlex1, DtMovimientos, true);
                    MessageBox.Show("¡ Los datos se mostraron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show("¡ No hay movimientos en el periodo del tipo de existencia indicado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    CboTipExi.Focus();
                    return;
                }
            }
        }
        private void ToolSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ToolImprimir_Click(object sender, EventArgs e)
        {
            if (DgLista.RowCount == 0)
            {
                MessageBox.Show("¡ No hay datos para imprimir !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdBuscar.Focus();
                return;
            }

            CN_alm_movimientos objMov = new CN_alm_movimientos();

            objMov.STU_SISTEMA = STU_SISTEMA;
            objMov.ReportKardexResumen(TxtFchIni.Text, TxtFchFin.Text, Convert.ToInt32(CboTipExi.SelectedValue), Convert.ToInt32(CboAlmacen.SelectedValue));
        }
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void CmdBuscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(CboAlmacen.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el almacén !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboTipExi.Focus();
                return;
            }

            if (TxtFchIni.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de inicio de la busqueda !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }

            if (TxtFchFin.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha final de la busqueda !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchFin.Focus();
                return;
            }

            if (Convert.ToDateTime(TxtFchIni.Text) > Convert.ToDateTime(TxtFchFin.Text))
            {
                MessageBox.Show("¡ La fecha de inicio no puede ser mayor a la fecha final !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchFin.Focus();
                return;
            }

            MostrarDatos();
        }
        private void CmdVerTodKar_Click(object sender, EventArgs e)
        {
            //if (FgKar.Rows.Count == 2)
            //{
            //    MessageBox.Show("¡ No hay datos para imprimir !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    CmdBuscar.Focus();
            //    return;
            //}

            //CN_alm_movimientos objMov = new CN_alm_movimientos();
            //objMov.STU_SISTEMA = STU_SISTEMA;
            //objMov.ReporteKardexDetalle("2017", Convert.ToInt32(CboPer.SelectedValue).ToString("00"), 0, Convert.ToInt32(CboTipExi.SelectedValue));
        }
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            if (DgLista.RowCount == 0)
            {
                MessageBox.Show("¡ No hay datos para imprimir !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdBuscar.Focus();
                return;
            }

            string[] columnGridNames = { "c_tipexides"
                    , "c_codpro"
                    , "c_despro"
                    , "c_abrepre"
                    , "n_stkini"
                    , "n_toting"
                    , "n_totsal"
                    , "n_saldo"};

            string[] columnHeaderNames = { "Tipo de Existencia"
                    , "Codigo Producto"
                    , "Descripcion Item"
                    , "Uni. Med"
                    , "Stock Inicial"
                    , "Ingresos"
                    , "Salida"
                    , "Saldo"};

            funDbGrid.DG_ExporExcel(DgLista, saveFileDialog1, columnGridNames, columnHeaderNames);
        }
        private void DgLista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DataTable dtResult = new DataTable();

                string c_CadFiltro = funDbGrid.DG_LeerCondicionesFiltro(DgLista);
                dtResult = funDbGrid.DG_Filtrar(DtMovimientos, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
            }
        }
        private void CboAlmacen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                CboAlmacen.SelectedValue = 0;
            }
        }
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            if (DgLista.RowCount == 0) { return; }
            CmdVerKardex_Click(sender, e);
        }
        private void CmdVerKardex_Click(object sender, EventArgs e)
        {
            if (DgLista.RowCount == 2)
            {
                MessageBox.Show("¡ Ha especificado el item a mostrar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdBuscar.Focus();
                return;
            }
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            string c_producto = DgLista.Columns["c_despro"].CellValue(DgLista.Row).ToString();

            FrmKardexDetalle2 frm = new FrmKardexDetalle2();
            frm.b_esvalorizado = b_esvalorizado;
            frm.n_iditem = n_IdRegistro;
            frm.n_idtipexi = Convert.ToInt32(CboTipExi.SelectedValue);
            frm.c_fchini = TxtFchIni.Text;
            frm.c_fchfin = TxtFchFin.Text;
            frm.c_idano = STU_SISTEMA.ANOTRABAJO.ToString();
            frm.n_idalmacen = Convert.ToInt32(CboAlmacen.SelectedValue);
            frm.STU_SISTEMA = STU_SISTEMA;
            frm.mysConec = mysConec;
            frm.c_producto = c_producto;
            frm.Show();
        }
        void Dimensionar(C1.Win.C1Sizer.C1Sizer dokTab, int intAlto, int intAncho)
        {
            dokTab.Height = intAlto;
            dokTab.Width = intAncho;
        }

        private void FrmKardexResumen2_Resize(object sender, EventArgs e)
        {
            Dimensionar(c1Sizer1, this.Height-80, this.Width-19);
        }
    }
}
