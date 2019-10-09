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
    public partial class FrmKardexDetalle2 : Form
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        public int n_iditem;
        public string c_fchini;
        public string c_fchfin;
        public string c_idano;
        public string c_producto;
        public int n_idtipexi;
        public int n_idalmacen;
        public bool b_esvalorizado;

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        int n_anccol = 70;
        bool booSeEjecuto = false;
        string[,] arrCabeceraFlex1 = new string[18, 5];

        DataTable dtItems = new DataTable();
        DataTable DtTipExi = new DataTable();
        DataTable dtmes = new DataTable();

        CN_sun_tipexi objtipexi = new CN_sun_tipexi();
        CN_mae_meses objmes = new CN_mae_meses();
        public FrmKardexDetalle2()
        {
            InitializeComponent();
        }
        void ConfigurarGrid()
        {
            FgKar.Rows.Count = 2;

            arrCabeceraFlex1[0, 0] = "Fecha";
            arrCabeceraFlex1[0, 1] = "75";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "d_fchdoc";

            arrCabeceraFlex1[1, 0] = "Tipo Documento";
            arrCabeceraFlex1[1, 1] = "200";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_desdoccom";

            arrCabeceraFlex1[2, 0] = "Serie";
            arrCabeceraFlex1[2, 1] = "50";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_numser";

            arrCabeceraFlex1[3, 0] = "Numero";
            arrCabeceraFlex1[3, 1] = "100";
            arrCabeceraFlex1[3, 2] = "C";
            arrCabeceraFlex1[3, 3] = "";
            arrCabeceraFlex1[3, 4] = "c_numdoc";

            arrCabeceraFlex1[4, 0] = "Tipo Operacion";
            arrCabeceraFlex1[4, 1] = "150";
            arrCabeceraFlex1[4, 2] = "C";
            arrCabeceraFlex1[4, 3] = "";
            arrCabeceraFlex1[4, 4] = "c_destipope";

            arrCabeceraFlex1[5, 0] = "Cantidad";
            arrCabeceraFlex1[5, 1] = "70";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "n_ingresocan";

            arrCabeceraFlex1[6, 0] = "Costo Unitario";
            arrCabeceraFlex1[6, 1] = n_anccol.ToString();
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_ingresopre";

            arrCabeceraFlex1[7, 0] = "Costo Total";
            arrCabeceraFlex1[7, 1] = n_anccol.ToString();
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "n_ingresocos";

            arrCabeceraFlex1[8, 0] = "Cantidad";
            arrCabeceraFlex1[8, 1] = "70";
            arrCabeceraFlex1[8, 2] = "D";
            arrCabeceraFlex1[8, 3] = "0.00";
            arrCabeceraFlex1[8, 4] = "n_salidacan";

            arrCabeceraFlex1[9, 0] = "Costo Unitario";
            arrCabeceraFlex1[9, 1] = n_anccol.ToString();
            arrCabeceraFlex1[9, 2] = "D";
            arrCabeceraFlex1[9, 3] = "0.00";
            arrCabeceraFlex1[9, 4] = "n_salidapre";

            arrCabeceraFlex1[10, 0] = "Costo Total";
            arrCabeceraFlex1[10, 1] = n_anccol.ToString();
            arrCabeceraFlex1[10, 2] = "D";
            arrCabeceraFlex1[10, 3] = "0.00";
            arrCabeceraFlex1[10, 4] = "n_salidacos";

            arrCabeceraFlex1[11, 0] = "Cantidad";
            arrCabeceraFlex1[11, 1] = "80";
            arrCabeceraFlex1[11, 2] = "D";
            arrCabeceraFlex1[11, 3] = "0.00";
            arrCabeceraFlex1[11, 4] = "n_saldocan";

            arrCabeceraFlex1[12, 0] = "Costo Unitario";
            arrCabeceraFlex1[12, 1] = n_anccol.ToString();
            arrCabeceraFlex1[12, 2] = "D";
            arrCabeceraFlex1[12, 3] = "0.00";
            arrCabeceraFlex1[12, 4] = "n_saldopre";

            arrCabeceraFlex1[13, 0] = "Costo Total";
            arrCabeceraFlex1[13, 1] = n_anccol.ToString();
            arrCabeceraFlex1[13, 2] = "D";
            arrCabeceraFlex1[13, 3] = "0.00";
            arrCabeceraFlex1[13, 4] = "n_saldocos";
            
            arrCabeceraFlex1[14, 0] = "Almacen";
            arrCabeceraFlex1[14, 1] = "150";
            arrCabeceraFlex1[14, 2] = "C";
            arrCabeceraFlex1[14, 3] = "";
            arrCabeceraFlex1[14, 4] = "c_desalm";

            arrCabeceraFlex1[15, 0] = "Tipo Doc. Referencia";
            arrCabeceraFlex1[15, 1] = "150";
            arrCabeceraFlex1[15, 2] = "C";
            arrCabeceraFlex1[15, 3] = "";
            arrCabeceraFlex1[15, 4] = "c_docreftipdoc";

            arrCabeceraFlex1[16, 0] = "Nº Doc. Referencia";
            arrCabeceraFlex1[16, 1] = "100";
            arrCabeceraFlex1[16, 2] = "C";
            arrCabeceraFlex1[16, 3] = "";
            arrCabeceraFlex1[16, 4] = "c_docrefnumdoc";
            funFlex.FlexMostrarDatos(FgKar, arrCabeceraFlex1, dtItems, 2, false);

            arrCabeceraFlex1[17, 0] = "Proveedor";
            arrCabeceraFlex1[17, 1] = "150";
            arrCabeceraFlex1[17, 2] = "C";
            arrCabeceraFlex1[17, 3] = "";
            arrCabeceraFlex1[17, 4] = "c_desnompro";

            funFlex.Flex_FixUniColumnas(FgKar, 0, 6, 8, "ENTRADAS", 1);
            funFlex.Flex_FixUniColumnas(FgKar, 0, 9, 11, "SALIDAS", 1);
            funFlex.Flex_FixUniColumnas(FgKar, 0, 12, 14, "SALDO FINAL", 1);
        }
        private void FrmKardexDetalle2_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                ConfigurarGrid();
                booSeEjecuto = true;
                MostrarDatos();
            }
        }
        void CargarCombos()
        {
            funDatos.ComboBoxCargarDataTable(CboTipExi, DtTipExi, "n_id", "c_des");
        }
        void CargarDatos()
        {
            objtipexi.mysConec = mysConec;
            DtTipExi = objtipexi.Listar();

            objmes.mysConec = mysConec;
            dtmes = objmes.Listar();

            CargarCombos();
        }
        private void FrmKardexDetalle2_Load(object sender, EventArgs e)
        {
            this.Width = 1222;
            this.Height = 614;

            TxtFchIni.Text = c_fchini;
            TxtFchFin.Text = c_fchfin;
            if (b_esvalorizado == false) { this.Text = "ALMACEN - INGRESO DE INVENTARIO PERMANENTE EN UNIDADES FISICAS"; }
            if (b_esvalorizado == true) { this.Text = "ALMACEN - INGRESO DE INVENTARIO PERMANENTE EN UNIDADES FISICAS VALORIZADA"; }
            c1Sizer1.Left = 2;
            c1Sizer1.Top = 39;
            Dimensionar(c1Sizer1, this.Height, this.Width);
            if (b_esvalorizado == true)
            {
                n_anccol = 70;
            }
            else
            {
                n_anccol = 0;
            }
            Dimensionar(c1Sizer1, this.Height - 80, this.Width - 20);
            CargarDatos();
        }
        void MostrarDatos()
        {
            DataTable dtResult = new DataTable();
            CN_alm_movimientos obMov = new CN_alm_movimientos();

            txtnomite.Text = c_producto;
            CboTipExi.SelectedValue = n_idtipexi;
            FgKar.Rows.Count = 2;
            obMov.mysConec = mysConec;
            dtResult = obMov.VerKardexDetallado(STU_SISTEMA.EMPRESAID, c_idano, TxtFchIni.Text, TxtFchFin.Text, n_iditem, n_idalmacen);

            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    funFlex.FlexMostrarDatos(FgKar, arrCabeceraFlex1, dtResult, 2, true);

                    funFlex.Flex_FixUniColumnas(FgKar, 0, 6, 8, "ENTRADAS", 1);
                    funFlex.Flex_FixUniColumnas(FgKar, 0, 9, 11, "SALIDAS", 1);
                    funFlex.Flex_FixUniColumnas(FgKar, 0, 12, 14, "SALDO FINAL", 1);
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
            if (FgKar.Rows.Count == 2)
            {
                MessageBox.Show("¡ No hay datos para imprimir !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdBuscar.Focus();
                return;
            }

            //CN_alm_movimientos objMov = new CN_alm_movimientos();

            //objMov.STU_SISTEMA = STU_SISTEMA;
            //objMov.ReporteKardexDetalle(c_idano, c_idmes, n_iditem, n_idtipexi);
        }
        private void toolExportar_Click(object sender, EventArgs e)
        {
            if (FgKar.Rows.Count == 2)
            {
                MessageBox.Show("! No hay datos para exportar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            string c_titu1 = "";
            if (b_esvalorizado == false) { c_titu1 = "KARDEX - "+ txtnomite.Text; }
            if (b_esvalorizado == true) { c_titu1 = "KARDEX VALORIZADO - " + txtnomite.Text; }
            string c_titu2 = "DEL " + TxtFchIni.Text+" AL " + TxtFchFin.Text;
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-ALM-KARDET-" + DateTime.Now.ToString("ddMMyyyy") + ".xls";
            funFlex.ExportToExcel(FgKar, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, c_titu1, c_titu2, c_NomArchivo);
        }
        private void FrmKardexDetalle2_Resize(object sender, EventArgs e)
        {
            Dimensionar(c1Sizer1, this.Height - 80, this.Width - 20);
        }
        void Dimensionar(C1.Win.C1Sizer.C1Sizer dokTab, int intAlto, int intAncho)
        {
            dokTab.Height = intAlto;
            dokTab.Width = intAncho;
        }
        private void c1Sizer1_Click(object sender, EventArgs e)
        {

        }
        private void CmdBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
