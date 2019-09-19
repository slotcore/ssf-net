using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Ventas;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Maestros;
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
    public partial class FrmVentasPeriodo : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Libro;
        public int n_Origen;                                                    // INDICA EL ORIGEN DE LA GUIA     1 = VENTAS ;   2 = TRASLADO ENTRE ALMACENES

        DataTable dtLocal = new DataTable();
        DataTable dtLista = new DataTable();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();
        CN_sys_empresalocal o_local = new CN_sys_empresalocal();

        string[,] arrCabecera1 = new string[12, 5];
        string[,] arrCabeceraFlex1 = new string[2, 5];
        public FrmVentasPeriodo()
        {
            InitializeComponent();
        }

        private void FrmVentasPeriodo_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarDT();
        }
        void CargarDT()
        {
            o_local.mysConec = mysConec;
            dtLocal = o_local.Listar(STU_SISTEMA.EMPRESAID, 0);

            funDatos.ComboBoxCargarDataTable(CboLocal, dtLocal, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 600;
            this.Width = 1094;

            Sz1.Left = 1;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;

            this.Text = "VENTAS - VENTAS ELECTRONICAS POR PERIODO";
            
            Cabecera1();
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, false);
            SetearCabecera();
        }
        void Cabecera1()
        {
            arrCabecera1[0, 0] = "Tipo Doc.";
            arrCabecera1[0, 1] = "40";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_abr";

            arrCabecera1[1, 0] = "Nº Documento";
            arrCabecera1[1, 1] = "120";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_numdoc";

            arrCabecera1[2, 0] = "Fch. Documento";
            arrCabecera1[2, 1] = "70";
            arrCabecera1[2, 2] = "F";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "d_fchdoc";

            arrCabecera1[3, 0] = "Tip. Doc. Ide.";
            arrCabecera1[3, 1] = "40";
            arrCabecera1[3, 2] = "C";
            arrCabecera1[3, 3] = "";
            arrCabecera1[3, 4] = "n_idtipdoc";

            arrCabecera1[4, 0] = "Nº Doc. Identidad";
            arrCabecera1[4, 1] = "80";
            arrCabecera1[4, 2] = "C";
            arrCabecera1[4, 3] = "";
            arrCabecera1[4, 4] = "c_numdoc1";

            arrCabecera1[5, 0] = "Cliente";
            arrCabecera1[5, 1] = "250";
            arrCabecera1[5, 2] = "C";
            arrCabecera1[5, 3] = "";
            arrCabecera1[5, 4] = "c_nombre";

            arrCabecera1[6, 0] = "Imp. Bruto";
            arrCabecera1[6, 1] = "70";
            arrCabecera1[6, 2] = "D";
            arrCabecera1[6, 3] = "";
            arrCabecera1[6, 4] = "n_impbru";

            arrCabecera1[7, 0] = "I.G.V.";
            arrCabecera1[7, 1] = "70";
            arrCabecera1[7, 2] = "D";
            arrCabecera1[7, 3] = "";
            arrCabecera1[7, 4] = "n_impigv";

            arrCabecera1[8, 0] = "Total";
            arrCabecera1[8, 1] = "70";
            arrCabecera1[8, 2] = "D";
            arrCabecera1[8, 3] = "";
            arrCabecera1[8, 4] = "n_imptotven";

            arrCabecera1[9, 0] = "Imp. Bruto";
            arrCabecera1[9, 1] = "70";
            arrCabecera1[9, 2] = "D";
            arrCabecera1[9, 3] = "";
            arrCabecera1[9, 4] = "n_impbru2";

            arrCabecera1[10, 0] = "I.G.V.";
            arrCabecera1[10, 1] = "70";
            arrCabecera1[10, 2] = "D";
            arrCabecera1[10, 3] = "";
            arrCabecera1[10, 4] = "n_impigv2";

            arrCabecera1[11, 0] = "Total";
            arrCabecera1[11, 1] = "70";
            arrCabecera1[11, 2] = "D";
            arrCabecera1[11, 3] = "";
            arrCabecera1[11, 4] = "n_imptotven2";
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
            int n_idlocal = 0;

            if (d_fchini > d_fchfin)
            {
                MessageBox.Show("¡ La fecha de inicio no puede ser mayor a la fecha final de la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }

            if (Convert.ToInt16(CboLocal.SelectedValue) != 0) { n_idlocal = Convert.ToInt16(CboLocal.SelectedValue); }

            CN_vta_ventas o_ventas = new CN_vta_ventas();
            o_ventas.mysConec = mysConec;
            o_ventas.STU_SISTEMA = STU_SISTEMA;

            o_ventas.Consulta28(STU_SISTEMA.EMPRESAID, TxtFchIni.Text, TxtFchFin.Text, n_idlocal);
            dtLista = o_ventas.dtLista1;

            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, true);
            SetearCabecera();
            //MostrarTotales();
            MessageBox.Show("¡ Los datos se mostraron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            FgDatos.Focus();
        }
        void MostrarTotales()
        {
            double n_valor = 0;

            FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
            FgDatos.SetData(FgDatos.Rows.Count - 1, 2, "TOTALES ==>");

            n_valor = funFlex.FlexSumarCol(FgDatos, 4, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 4, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 5, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 5, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 6, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 6, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 7, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 7, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 8, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 8, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 9, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 9, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 10, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 10, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 11, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 11, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 12, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 12, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 13, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 13, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 14, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 14, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 15, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 15, n_valor.ToString("0.00"));

            funFlex.Flex_PintarCeldas(FgDatos, FgDatos.Rows.Count - 1, 1, FgDatos.Rows.Count - 1, FgDatos.Cols.Count - 1, Color.Black, Color.PaleGreen);
            funFlex.Flex_PintarCeldas(FgDatos, 1, FgDatos.Cols.Count - 1, FgDatos.Rows.Count - 1, FgDatos.Cols.Count - 1, Color.Black, Color.Pink);
        }
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_cad = "DEL "+TxtFchIni.Text+" AL "+ TxtFchFin.Text;
            string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-VENTAPERIODO-" + ".xls";
            funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "VENTAS POR PERIODO", c_cad, c_nomarch);
        }
        private void FrmVentasPeriodo_Resize(object sender, EventArgs e)
        {
            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;
        }
        void SetearCabecera()
        {
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 7, 9, "DATO 1", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 10, 12, "DATO 2", 1);
        }

        private void CboLocal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete") { CboLocal.SelectedValue = 0; }
        }
    }
}
