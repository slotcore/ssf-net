using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Logistica;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Almacen;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Logistica;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Sunat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmLibroDiario : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Libro;

        DataTable dtTipExi = new DataTable();
        DataTable dtProv = new DataTable();
        DataTable dtItem = new DataTable();
        DataTable dtLista = new DataTable();
        DataTable dtResumen = new DataTable();

        CN_sun_tipmon objMon = new CN_sun_tipmon();
        CN_mae_meses objMes = new CN_mae_meses();
        CN_sun_libro o_Libro = new CN_sun_libro();

        DataTable dtMon = new DataTable();
        DataTable dtPerIni = new DataTable();
        DataTable dtPerFin = new DataTable();
        DataTable dtLibro = new DataTable();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();

        string[,] arrCabeceraFlex1 = new string[2, 5];
        string[,] arrCabeceraFlex2 = new string[2, 5];
        string[,] arrCabecera1 = new string[15, 5];
        string[,] arrCabResumen1 = new string[4, 5];
        public FrmLibroDiario()
        {
            InitializeComponent();
        }

        private void FrmLibroDiario_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarDT();
        }
        void CargarDT()
        {
            objMon.mysConec = mysConec;
            dtMon = objMon.Listar();

            o_Libro.mysConec =mysConec;
            o_Libro.Listar();
            dtLibro = o_Libro.dtLista;

            objMes.mysConec = mysConec;
            dtPerIni = objMes.Listar();
            dtPerIni = funDatos.DataTableFiltrar(dtPerIni, "n_id NOT IN(0,13)");

            dtPerFin = objMes.Listar();
            dtPerFin = funDatos.DataTableFiltrar(dtPerFin, "n_id NOT IN(0,13)");

            funDatos.ComboBoxCargarDataTable(CboPerIni, dtPerIni, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboPerFin, dtPerFin, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMon, dtMon, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboLibro, dtLibro, "n_id", "c_des");

            CboMon.SelectedValue = 115;       // LA MONEDA POR EFECTO ES SOLES
            //CboPerIni.SelectedValue = DateTime.Now.Month;
        }
        void ConfigurarFormulario()
        {
            this.Height = 600;
            this.Width = 1010;

            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;

            LblNumReg.Text = "";

            OptLibro.Checked = true;

            Tab02.SelectedIndex = 0;

            Cabecera1();
            this.Text = "CONTABILIDAD - LIBRO DIARIO";
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, false);
            funFlex.FlexMostrarDatos(FgRes, arrCabResumen1, dtResumen, 3, false);
            Configurarcabecera1();
        }
        void Cabecera1()
        {
            arrCabecera1[0, 0] = "Nº Reg. Doc.";
            arrCabecera1[0, 1] = "80";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_numasi2";

            arrCabecera1[1, 0] = "T.D.";
            arrCabecera1[1, 1] = "30";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_abr";

            arrCabecera1[2, 0] = "Fch. Doc.";
            arrCabecera1[2, 1] = "70";
            arrCabecera1[2, 2] = "F";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "d_orifchdoc";

            arrCabecera1[3, 0] = "Nº Documento";
            arrCabecera1[3, 1] = "110";
            arrCabecera1[3, 2] = "C";
            arrCabecera1[3, 3] = "";
            arrCabecera1[3, 4] = "c_orinumdoc";

            arrCabecera1[4, 0] = "Glosa";
            arrCabecera1[4, 1] = "100";
            arrCabecera1[4, 2] = "C";
            arrCabecera1[4, 3] = "";
            arrCabecera1[4, 4] = "c_origlo";

            arrCabecera1[5, 0] = "Nº RUC / DNI";
            arrCabecera1[5, 1] = "80";
            arrCabecera1[5, 2] = "C";
            arrCabecera1[5, 3] = "";
            arrCabecera1[5, 4] = "c_orinumruc";

            arrCabecera1[6, 0] = "Proveedor / Cliente / Otros";
            arrCabecera1[6, 1] = "150";
            arrCabecera1[6, 2] = "C";
            arrCabecera1[6, 3] = "";
            arrCabecera1[6, 4] = "c_orinomcli";

            arrCabecera1[7, 0] = "Nº Cuenta";
            arrCabecera1[7, 1] = "80";
            arrCabecera1[7, 2] = "C";
            arrCabecera1[7, 3] = "";
            arrCabecera1[7, 4] = "c_cuecon";

            arrCabecera1[8, 0] = "Nombre de la Cuenta";
            arrCabecera1[8, 1] = "200";
            arrCabecera1[8, 2] = "C";
            arrCabecera1[8, 3] = "";
            arrCabecera1[8, 4] = "c_des";

            arrCabecera1[9, 0] = "M.";
            arrCabecera1[9, 1] = "30";
            arrCabecera1[9, 2] = "C";
            arrCabecera1[9, 3] = "";
            arrCabecera1[9, 4] = "c_simbolo";

            arrCabecera1[10, 0] = "T.C.";
            arrCabecera1[10, 1] = "40";
            arrCabecera1[10, 2] = "D";
            arrCabecera1[10, 3] = "0.000";
            arrCabecera1[10, 4] = "n_tc";

            arrCabecera1[11, 0] = "Debe";
            arrCabecera1[11, 1] = "80";
            arrCabecera1[11, 2] = "D";
            arrCabecera1[11, 3] = "0.00";
            arrCabecera1[11, 4] = "n_impdebsol";

            arrCabecera1[12, 0] = "Haber";
            arrCabecera1[12, 1] = "80";
            arrCabecera1[12, 2] = "D";
            arrCabecera1[12, 3] = "0.00";
            arrCabecera1[12, 4] = "n_imphabsol";

            arrCabecera1[13, 0] = "n_idlib";
            arrCabecera1[13, 1] = "0";
            arrCabecera1[13, 2] = "N";
            arrCabecera1[13, 3] = "";
            arrCabecera1[13, 4] = "n_lin";

            arrCabecera1[14, 0] = "n_orden";
            arrCabecera1[14, 1] = "0";
            arrCabecera1[14, 2] = "N";
            arrCabecera1[14, 3] = "";
            arrCabecera1[14, 4] = "n_orden";


            arrCabResumen1[0, 0] = "Nº Cuenta";
            arrCabResumen1[0, 1] = "80";
            arrCabResumen1[0, 2] = "C";
            arrCabResumen1[0, 3] = "";
            arrCabResumen1[0, 4] = "c_cuecon";

            arrCabResumen1[1, 0] = "Nombre Cuenta Contable";
            arrCabResumen1[1, 1] = "300";
            arrCabResumen1[1, 2] = "C";
            arrCabResumen1[1, 3] = "";
            arrCabResumen1[1, 4] = "c_des";

            arrCabResumen1[2, 0] = "Deb";
            arrCabResumen1[2, 1] = "80";
            arrCabResumen1[2, 2] = "D";
            arrCabResumen1[2, 3] = "";
            arrCabResumen1[2, 4] = "n_impdebsol";

            arrCabResumen1[3, 0] = "Haber";
            arrCabResumen1[3, 1] = "80";
            arrCabResumen1[3, 2] = "D";
            arrCabResumen1[3, 3] = "";
            arrCabResumen1[3, 4] = "n_imphabsol";
        }
        private void FrmLibroDiario_Resize(object sender, EventArgs e)
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
            if (Convert.ToInt16(CboPerIni.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el periodo de inicio a consultar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboPerIni.Focus();
                return;
            }
            if (Convert.ToInt16(CboPerFin.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el periodo final a consultar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboPerFin.Focus();
                return;
            }
            if (Convert.ToInt16(CboPerIni.SelectedValue) > Convert.ToInt16(CboPerFin.SelectedValue))
            {
                MessageBox.Show("¡ El periodo de inicio no puede ser mayor al periodo final !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboPerFin.Focus();
                return;
            }
            if (OptLibro.Checked == true)
            {
                if (Convert.ToInt16(CboLibro.SelectedValue) == 0)
                {
                    MessageBox.Show("¡ No ha seleccionado el libro a consultar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    CboPro.Focus();
                    return;
                }
            }

            Tab02.SelectedIndex = 0;
            CN_con_diario funCom = new CN_con_diario();
            funCom.mysConec = mysConec;
            funCom.STU_SISTEMA = STU_SISTEMA;

            bool b_Result = false;
            b_Result = funCom.ConsultaDiario(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboLibro.SelectedValue), Convert.ToInt16(CboPerIni.SelectedValue), Convert.ToInt16(CboPerFin.SelectedValue));
            
            if (b_Result == true)
            {
                dtLista = funCom.dtLista;
                dtResumen = funCom.dtResumen;
                if (dtLista.Rows.Count != 0)
                {
                    funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, true);
                    funFlex.FlexMostrarDatos(FgRes, arrCabResumen1, dtResumen, 3, true);
                    HalarTotalresumen();
                }
                else
                {
                    MessageBox.Show("¡ No hay registros en el periodo indicado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }
        void HalarTotalresumen()
        {
            double n_val1 = funFlex.FlexSumarCol(FgRes, 3, 3, FgRes.Rows.Count-1);
            double n_val2 = funFlex.FlexSumarCol(FgRes, 4, 3, FgRes.Rows.Count - 1);

            FgRes.Rows.Count = FgRes.Rows.Count + 1;
            FgRes.SetData(FgRes.Rows.Count - 1, 2, "TOTAL ==>");
            FgRes.SetData(FgRes.Rows.Count - 1, 3, n_val1.ToString("0.00"));
            FgRes.SetData(FgRes.Rows.Count - 1, 4, n_val2.ToString("0.00"));
        }
        void Configurarcabecera1()
        {
            // CABECERA DETALLE
            //funFlex.Flex_UniColumnas(FgDatos, 0, 2, 5, "COMPROBANTE DE PAGO", 1);
            //funFlex.Flex_UniColumnas(FgDatos, 0, 6, 8, "INFORMACION DEL PROVEEDOR", 1);
            //funFlex.Flex_UniColumnas(FgDatos, 0, 9, 11, "COMPROBANTE DE PAGO", 1);
            //funFlex.Flex_UniColumnas(FgDatos, 0, 12, 14, "BASE IMPONIBLE", 1);

            //funFlex.Flex_UniColumnas(FgDatos, 0, 18, 21, "IMPUESTO GENERAL A LA VENTAS", 1);
            //funFlex.Flex_UniColumnas(FgDatos, 0, 25, 28, "CONSTANCIA DE DEPOSITO SPOT", 1);
            //funFlex.Flex_UniColumnas(FgDatos, 0, 29, 36, "REFERENCIA DEL COMPROBANTE DE PAGO O DOCUMENTO ORIGINAL", 1);

            //// CABECERA RSUMEN
            //funFlex.Flex_UniColumnas(FgRes, 0, 1, 2, "TIPO DE DOCUMENTO", 1);
            //funFlex.Flex_UniColumnas(FgRes, 0, 3, 5, "BASE IMPONIBLE", 1);
            //funFlex.Flex_UniColumnas(FgRes, 0, 9, 11, "COMPROBANTE DE PAGO", 1);
        }
        void Configurarcabecera2()
        {
        }
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            if (Tab02.SelectedIndex == 0)
            {
                funFlex.ExportToExcel_NumFilaCabecera = 3;
                string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-LIBRO_DIARIO-" + Convert.ToInt16(CboPerIni.SelectedValue).ToString("00") + "-AL-" + Convert.ToInt16(CboPerIni.SelectedValue).ToString("00") + ".xls";
                funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "LIBRO DIARIO - " + CboLibro.Text.ToUpper(), "DE " + CboPerIni.Text.ToUpper() + " A " + CboPerFin.Text.ToUpper(), c_nomarch);
            }
            else
            {
                funFlex.ExportToExcel_NumFilaCabecera = 3;
                string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-LIBRO_DIARIO-" + Convert.ToInt16(CboPerIni.SelectedValue).ToString("00") + "-AL-" + Convert.ToInt16(CboPerIni.SelectedValue).ToString("00") + ".xls";
                funFlex.ExportToExcel(FgRes, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "RESUMEN LIBRO DIARIO - " + CboLibro.Text.ToUpper(), "DE " + CboPerIni.Text.ToUpper() + " A " + CboPerFin.Text.ToUpper(), c_nomarch);
            }
        }
        private void TooGenLE_Click(object sender, EventArgs e)
        {
            bool b_result = false;
            CN_con_le objLE = new CN_con_le();
            objLE.mysConec = mysConec;
            objLE.STU_SISTEMA = STU_SISTEMA;
            if (n_Libro == 8)
            {
                b_result = objLE.LE_81(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboPerIni.SelectedValue));
            }
            if (n_Libro == 14)
            {
                b_result = objLE.LE_141(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboPerIni.SelectedValue));
            }

            if (b_result == true)
            {
                MessageBox.Show("¡ El libro electronico se genero con exito en la siguiente ruta :" + objLE.c_RutaSalida + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void ToolImprimir_Click(object sender, EventArgs e)
        {

        }

        private void ToolFilter_Click(object sender, EventArgs e)
        {
            CN_con_diario o_dia = new CN_con_diario();
            o_dia.mysConec= mysConec;
            o_dia.VerDescuadrados(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboPerIni.SelectedValue), Convert.ToInt16(CboLibro.SelectedValue));
        }
    }
}
