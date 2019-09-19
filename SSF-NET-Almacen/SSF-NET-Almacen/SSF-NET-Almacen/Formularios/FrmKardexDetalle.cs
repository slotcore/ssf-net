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
    public partial class FrmKardexDetalle : Form
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        public int n_iditem;
        public string c_idmes;
        public string c_idano;
        public string c_producto;
        public int n_idtipexi;
        public int n_idalmacen;

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        bool booSeEjecuto = false;
        string[,] arrCabeceraFlex1 = new string[11, 5];

        DataTable dtItems = new DataTable();
        DataTable DtTipExi = new DataTable();
        DataTable dtmes = new DataTable();

        CN_sun_tipexi objtipexi = new CN_sun_tipexi();
        CN_mae_meses objmes = new CN_mae_meses();

        public FrmKardexDetalle()
        {
            InitializeComponent();
        }
        private void CmdBuscar_Click(object sender, EventArgs e)
        {
        }
        void ConfigurarGrid()
        {
            FgKar.Rows.Count = 2;

            arrCabeceraFlex1[0, 0] = "FECHA";
            arrCabeceraFlex1[0, 1] = "80";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_destipexi";

            arrCabeceraFlex1[1, 0] = "TIPO DE DOCUMENTO";
            arrCabeceraFlex1[1, 1] = "200";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_codsuntipdoccom";

            arrCabeceraFlex1[2, 0] = "SERIE";
            arrCabeceraFlex1[2, 1] = "70";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_numser";

            arrCabeceraFlex1[3, 0] = "NUMERO";
            arrCabeceraFlex1[3, 1] = "120";
            arrCabeceraFlex1[3, 2] = "C";
            arrCabeceraFlex1[3, 3] = "";
            arrCabeceraFlex1[3, 4] = "c_numdoc";

            arrCabeceraFlex1[4, 0] = "TIPO DE OPERACION";
            arrCabeceraFlex1[4, 1] = "150";
            arrCabeceraFlex1[4, 2] = "C";
            arrCabeceraFlex1[4, 3] = "";
            arrCabeceraFlex1[4, 4] = "c_codsuntipope";

            arrCabeceraFlex1[5, 0] = "ENTRADAS";
            arrCabeceraFlex1[5, 1] = "80";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "n_ingreso";

            arrCabeceraFlex1[6, 0] = "SALIDAS";
            arrCabeceraFlex1[6, 1] = "80";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_salida";

            arrCabeceraFlex1[7, 0] = "SALDO FINAL";
            arrCabeceraFlex1[7, 1] = "80";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "";

            arrCabeceraFlex1[8, 0] = "ALMACEN";
            arrCabeceraFlex1[8, 1] = "150";
            arrCabeceraFlex1[8, 2] = "C";
            arrCabeceraFlex1[8, 3] = "";
            arrCabeceraFlex1[8, 4] = "c_desalm";

            arrCabeceraFlex1[9, 0] = "TIPO DOC. REFERENCIA";
            arrCabeceraFlex1[9, 1] = "150";
            arrCabeceraFlex1[9, 2] = "C";
            arrCabeceraFlex1[9, 3] = "";
            arrCabeceraFlex1[9, 4] = "c_desalm";

            arrCabeceraFlex1[10, 0] = "Nº DOC. REFERENCIA";
            arrCabeceraFlex1[10, 1] = "100";
            arrCabeceraFlex1[10, 2] = "C";
            arrCabeceraFlex1[10, 3] = "";
            arrCabeceraFlex1[10, 4] = "c_desalm";
            funFlex.FlexMostrarDatos(FgKar, arrCabeceraFlex1, dtItems, 2, false);
        }
        private void FrmKardexDetalle_Activated(object sender, EventArgs e)
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
            funDatos.ComboBoxCargarDataTable(CboPer, dtmes, "n_id", "c_des");
        }
        void CargarDatos()
        {
            objtipexi.mysConec = mysConec;
            DtTipExi = objtipexi.Listar();

            objmes.mysConec = mysConec;
            dtmes = objmes.Listar();

            CargarCombos();
        }
        private void FrmKardexDetalle_Load(object sender, EventArgs e)
        {
            this.Text = "ALMACEN - INGRESO DE INVENTARIO PERMANENTE EN UNIDADES FISICAS";
            CargarDatos();
        }
        void MostrarDatos()
        {
            //int n_row = 0;
            //DataTable dtResult = new DataTable();
            //double n_valor;
            //CN_alm_movimientos obMov = new CN_alm_movimientos();
            //double n_total = 0;
            //double n_toting = 0;
            //double n_totsal = 0;

            //txtnomite.Text = c_producto;
            //CboPer.SelectedValue = Convert.ToInt32(c_idmes);
            //CboTipExi.SelectedValue = n_idtipexi;
            //FgKar.Rows.Count = 2;
            //obMov.mysConec = mysConec;
            //dtResult = obMov.VerKardexDetallado(STU_SISTEMA.EMPRESAID, c_idano, c_idmes, n_iditem, n_idtipexi, n_idalmacen);

            //if (dtResult != null)
            //{
            //    if (dtResult.Rows.Count != 0)
            //    {
            //        for (n_row = 0; n_row <= (dtResult.Rows.Count - 1); n_row++)
            //        {
            //            FgKar.Rows.Count = (FgKar.Rows.Count + 1);
            //            FgKar.SetData(FgKar.Rows.Count - 1, 1, dtResult.Rows[n_row]["d_fchdoc"].ToString());
            //            FgKar.SetData(FgKar.Rows.Count - 1, 2, dtResult.Rows[n_row]["c_desdoccom"].ToString());
            //            FgKar.SetData(FgKar.Rows.Count - 1, 3, dtResult.Rows[n_row]["c_numser"].ToString());
            //            FgKar.SetData(FgKar.Rows.Count - 1, 4, dtResult.Rows[n_row]["c_numdoc"].ToString());
            //            FgKar.SetData(FgKar.Rows.Count - 1, 5, dtResult.Rows[n_row]["c_destipope"].ToString());

            //            n_valor = Convert.ToDouble(funFunciones.NulosN(dtResult.Rows[n_row]["n_ingreso"]));
            //            FgKar.SetData(FgKar.Rows.Count - 1, 6, n_valor.ToString("0.00"));
            //            n_toting = n_toting + n_valor;
            //            n_total = n_total + n_valor;

            //            n_valor = Convert.ToDouble(funFunciones.NulosN(dtResult.Rows[n_row]["n_salida"]));
            //            FgKar.SetData(FgKar.Rows.Count - 1, 7, n_valor.ToString("0.00"));
            //            n_totsal = n_totsal + n_valor;
            //            n_total = n_total - n_valor;
            //            FgKar.SetData(FgKar.Rows.Count - 1, 8, n_total.ToString("0.00"));
            //            FgKar.SetData(FgKar.Rows.Count - 1, 9, dtResult.Rows[n_row]["c_desalm"]);
            //            FgKar.SetData(FgKar.Rows.Count - 1, 10, dtResult.Rows[n_row]["c_docreftipdoc"]);
            //            FgKar.SetData(FgKar.Rows.Count - 1, 11, dtResult.Rows[n_row]["c_docrefnumdoc"]);
            //        }
            //        FgKar.Rows.Count = FgKar.Rows.Count + 1;

            //        FgKar.SetData(FgKar.Rows.Count - 1, 6, n_toting.ToString("0.00"));
            //        FgKar.SetData(FgKar.Rows.Count - 1, 7, n_totsal.ToString("0.00"));
            //        dtResult = null;
            //    }
            //    else
            //    {
            //        MessageBox.Show("¡ No hay movimientos en el periodo del tipo de existencia indicado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //        CboTipExi.Focus();
            //        return;
            //    }
            //}
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
            
            CN_alm_movimientos objMov = new CN_alm_movimientos();

            objMov.STU_SISTEMA = STU_SISTEMA;
            objMov.ReporteKardexDetalle(c_idano,c_idmes,n_iditem,n_idtipexi);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (FgKar.Rows.Count == 2)
            {
                MessageBox.Show("! No hay datos para exportar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            string c_titu2 = "MES - " + CboPer.Text; 
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-ALM-KARDET-" + DateTime.Now.ToString("ddMMyyyy") + ".xls";
            funFlex.ExportToExcel(FgKar, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "KARDEX RESUMIDO", c_titu2, c_NomArchivo);
        }
    }
}
