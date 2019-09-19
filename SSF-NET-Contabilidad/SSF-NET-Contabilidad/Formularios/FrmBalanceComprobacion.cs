using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Contabilidad;
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


namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmBalanceComprobacion : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Libro;

        DataTable dtLib = new DataTable();
        DataTable dtMes = new DataTable();
        DataTable dtMes2 = new DataTable();
        DataTable dtTipMon = new DataTable();
        DataTable dtLista = new DataTable();

        // OBJETOS DE ACCESO A DATOS
        CN_con_diario objdia = new CN_con_diario();
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();
        CN_mae_meses objMes = new CN_mae_meses();
        CN_sun_tipmon objTipMon = new CN_sun_tipmon();
        CN_sun_libro objLibro = new CN_sun_libro();
        


        string[,] arrCabecera1 = new string[18, 5];
        string[,] arrCabeceraFlex1 = new string[2, 5];

        public FrmBalanceComprobacion()
        {
            InitializeComponent();
        }

        private void ToolBuscar_Click(object sender, EventArgs e)
        {
            EjecutarConsulta();
        }

        private void FrmBalanceComprobacion_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarDT();
            CboMon.SelectedValue = 115;
            CboMesIni.Focus();
        }
        void CargarDT()
        {
            objTipMon.mysConec = mysConec;
            dtTipMon = objTipMon.Listar();

            objLibro.mysConec = mysConec;
            objLibro.Listar();
            dtLib = objLibro.dtLista;

            objMes.mysConec = mysConec;
            dtMes = objMes.Listar();
            dtMes = funDatos.DataTableFiltrar(dtMes, "n_id IN(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)");
            dtMes2 = objMes.Listar();
            dtMes2 = funDatos.DataTableFiltrar(dtMes2, "n_id IN(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)");

            funDatos.ComboBoxCargarDataTable(CboMesIni, dtMes, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMesFin, dtMes2, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMon, dtTipMon, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboLib, dtLib, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 600;
            this.Width = 1094;

            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;

            this.Text = "CONTABILIDAD - BALANCE DE COMPROBACION";

            FgDatos.Cols.Count = 5;
            Cabecera1();
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, false);
            SetearCabecera1();
        }
        void SetearCabecera1()
        {
            //funFlex.Flex_UnirFilas(FgDatos,1 , 0, 1, "Nº Cuenta Contable", 70);
            //funFlex.Flex_UnirFilas(FgDatos, 2, 0, 1, "Descripcion", 300);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 3, 4, "Saldos Iniciales", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 5, 6, "Movimientos del Periodo", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 7, 8, "Sumas del Mayor", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 9, 10, "Saldos Al", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 11, 12, "Cuentas del Balance", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 13, 14, "Transferencias y Can.", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 15, 16, "Resutados por Naturaleza", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 17, 18, "Resultados por Funcion", 1);
        }
        void Cabecera1()
        {
            arrCabecera1[0, 0] = "Nº Cuenta";
            arrCabecera1[0, 1] = "70";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_cuecon";

            arrCabecera1[1, 0] = "Descripcion";
            arrCabecera1[1, 1] = "300";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_des";

            arrCabecera1[2, 0] = "Debe";
            arrCabecera1[2, 1] = "80";
            arrCabecera1[2, 2] = "D";
            arrCabecera1[2, 3] = "0.00";
            arrCabecera1[2, 4] = "n_salinideb";

            arrCabecera1[3, 0] = "Haber";
            arrCabecera1[3, 1] = "80";
            arrCabecera1[3, 2] = "D";
            arrCabecera1[3, 3] = "0.00";
            arrCabecera1[3, 4] = "n_salinihab";

            arrCabecera1[4, 0] = "Debe";
            arrCabecera1[4, 1] = "80";
            arrCabecera1[4, 2] = "D";
            arrCabecera1[4, 3] = "0.00";
            arrCabecera1[4, 4] = "n_movperdeb";

            arrCabecera1[5, 0] = "Haber";
            arrCabecera1[5, 1] = "80";
            arrCabecera1[5, 2] = "D";
            arrCabecera1[5, 3] = "0.00";
            arrCabecera1[5, 4] = "n_movperhab";

            arrCabecera1[6, 0] = "Debe";
            arrCabecera1[6, 1] = "80";
            arrCabecera1[6, 2] = "D";
            arrCabecera1[6, 3] = "0.00";
            arrCabecera1[6, 4] = "n_summaydeb";

            arrCabecera1[7, 0] = "Haber";
            arrCabecera1[7, 1] = "80";
            arrCabecera1[7, 2] = "D";
            arrCabecera1[7, 3] = "0.00";
            arrCabecera1[7, 4] = "n_summayhab";

            arrCabecera1[8, 0] = "Debe";
            arrCabecera1[8, 1] = "80";
            arrCabecera1[8, 2] = "D";
            arrCabecera1[8, 3] = "0.00";
            arrCabecera1[8, 4] = "n_salperdeb";

            arrCabecera1[9, 0] = "Haber";
            arrCabecera1[9, 1] = "80";
            arrCabecera1[9, 2] = "D";
            arrCabecera1[9, 3] = "0.00";
            arrCabecera1[9, 4] = "n_salperhab";

            arrCabecera1[10, 0] = "Debe";
            arrCabecera1[10, 1] = "80";
            arrCabecera1[10, 2] = "D";
            arrCabecera1[10, 3] = "0.00";
            arrCabecera1[10, 4] = "n_ctabaldeb";

            arrCabecera1[11, 0] = "Haber";
            arrCabecera1[11, 1] = "80";
            arrCabecera1[11, 2] = "D";
            arrCabecera1[11, 3] = "0.00";
            arrCabecera1[11, 4] = "n_ctabalhab";

            arrCabecera1[12, 0] = "Debe";
            arrCabecera1[12, 1] = "80";
            arrCabecera1[12, 2] = "D";
            arrCabecera1[12, 3] = "0.00";
            arrCabecera1[12, 4] = "n_ctatradeb";

            arrCabecera1[13, 0] = "Haber";
            arrCabecera1[13, 1] = "80";
            arrCabecera1[13, 2] = "D";
            arrCabecera1[13, 3] = "0.00";
            arrCabecera1[13, 4] = "n_ctatrahab";

            arrCabecera1[14, 0] = "Debe";
            arrCabecera1[14, 1] = "80";
            arrCabecera1[14, 2] = "D";
            arrCabecera1[14, 3] = "0.00";
            arrCabecera1[14, 4] = "n_ctanatdeb";

            arrCabecera1[15, 0] = "Haber";
            arrCabecera1[15, 1] = "80";
            arrCabecera1[15, 2] = "D";
            arrCabecera1[15, 3] = "0.00";
            arrCabecera1[15, 4] = "n_ctanathab";

            arrCabecera1[16, 0] = "Debe";
            arrCabecera1[16, 1] = "80";
            arrCabecera1[16, 2] = "D";
            arrCabecera1[16, 3] = "0.00";
            arrCabecera1[16, 4] = "n_ctafundeb";

            arrCabecera1[17, 0] = "Haber";
            arrCabecera1[17, 1] = "80";
            arrCabecera1[17, 2] = "D";
            arrCabecera1[17, 3] = "0.00";
            arrCabecera1[17, 4] = "n_ctafunhab";
        }
        private void FrmBalanceComprobacion_Resize(object sender, EventArgs e)
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
        void EjecutarConsulta()
        {
            if (Convert.ToInt16(CboMesIni.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha indicado el periodo inicial !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMesIni.Focus();
                return;
            }
            if (Convert.ToInt16(CboMesFin.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha indicado el periodo final !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMesFin.Focus();
                return;
            }

            if (Convert.ToInt16(CboMesIni.SelectedValue) > Convert.ToInt16(CboMesFin.SelectedValue))
            {
                MessageBox.Show("¡ El periodo de inicio no puede ser mayor al periodo final de la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMesIni.Focus();
                return;
            }
            
            objdia.mysConec = mysConec;
            objdia.BalanceComprobacion(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboMesIni.SelectedValue), Convert.ToInt16(CboMesFin.SelectedValue), Convert.ToInt16(CboLib.SelectedValue));
            dtLista = objdia.dtLista;
            
            funFlex.b_AlternarColor = false;
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, true);
            SetearCabecera1();
            PintarGrid();
            Totalizar();
            MessageBox.Show("¡ Los datos se mostraron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            FgDatos.Focus();
        }
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_cad = "( DE " + CboMesIni.Text + " A " + CboMesFin.Text + " )";
            string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-BALANCE_COMPROBACION-" + ".xls";
            funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "BALANCE COMPROBACION", c_cad, c_nomarch);
        }
        void PintarGrid()
        {
            funFlex.Flex_PintarCeldas(FgDatos, 2, 1, FgDatos.Rows.Count - 1, 2, Color.Black, Color.Beige);
            funFlex.Flex_PintarCeldas(FgDatos, 2, 5, FgDatos.Rows.Count - 1, 6, Color.Black, Color.Beige);
            funFlex.Flex_PintarCeldas(FgDatos, 2, 9, FgDatos.Rows.Count - 1, 10, Color.Black, Color.Beige);
            funFlex.Flex_PintarCeldas(FgDatos, 2, 13, FgDatos.Rows.Count - 1, 14, Color.Black, Color.Beige);
            funFlex.Flex_PintarCeldas(FgDatos, 2, 17, FgDatos.Rows.Count - 1, 18, Color.Black, Color.Beige);
        }
        void Totalizar()
        {
            double n_valor = 0;
            FgDatos.Rows.Count = FgDatos.Rows.Count + 1;

            FgDatos.SetData(FgDatos.Rows.Count - 1, 2, "T O T A L E S ==>");

            n_valor = funFlex.FlexSumarCol(FgDatos, 3, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 3, n_valor.ToString("0.00"));

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

            n_valor = funFlex.FlexSumarCol(FgDatos, 16, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 16, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 17, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 17, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 18, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 18, n_valor.ToString("0.00"));

            FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
            FgDatos.SetData(FgDatos.Rows.Count - 1, 2, "R E S U L T A D O S ==>");

            // SUMAS DEL SALDO
            double n_val1 = Convert.ToDouble(FgDatos.GetData(FgDatos.Rows.Count - 2, 9));
            double n_val2 = Convert.ToDouble(FgDatos.GetData(FgDatos.Rows.Count - 2, 10));
            double n_resu = (n_val1 - n_val2);
            if (n_resu < 0)
            {
                FgDatos.SetData(FgDatos.Rows.Count - 1, 9, n_resu.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 10, "0.00");
            }
            else
            {
                FgDatos.SetData(FgDatos.Rows.Count - 1, 9, "0.00");
                FgDatos.SetData(FgDatos.Rows.Count - 1, 10, n_resu.ToString("0.00"));
            }

            // SUMAS CUENTA DEL BALANCE
            n_val1 = Convert.ToDouble(FgDatos.GetData(FgDatos.Rows.Count - 2, 11));
            n_val2 = Convert.ToDouble(FgDatos.GetData(FgDatos.Rows.Count - 2, 12));
            n_resu = (n_val1 - n_val2);
            if (n_resu < 0)
            {
                FgDatos.SetData(FgDatos.Rows.Count - 1, 11, n_resu.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 12, "0.00");
            }
            else
            {
                FgDatos.SetData(FgDatos.Rows.Count - 1, 11, "0.00");
                FgDatos.SetData(FgDatos.Rows.Count - 1, 12, n_resu.ToString("0.00"));
            }

            // SUMAS CUENTA TRANSFERENCIA Y CANC
            n_val1 = Convert.ToDouble(FgDatos.GetData(FgDatos.Rows.Count - 2, 13));
            n_val2 = Convert.ToDouble(FgDatos.GetData(FgDatos.Rows.Count - 2, 14));
            n_resu = (n_val1 - n_val2);
            if (n_resu < 0)
            {
                FgDatos.SetData(FgDatos.Rows.Count - 1, 13, n_resu.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 14, "0.00");
            }
            else
            {
                FgDatos.SetData(FgDatos.Rows.Count - 1, 13, "0.00");
                FgDatos.SetData(FgDatos.Rows.Count - 1, 14, n_resu.ToString("0.00"));
            }

            // SUMAS CUENTA RESULTADOS POR NATURALEZA
            n_val1 = Convert.ToDouble(FgDatos.GetData(FgDatos.Rows.Count - 2, 15));
            n_val2 = Convert.ToDouble(FgDatos.GetData(FgDatos.Rows.Count - 2, 16));
            n_resu = (n_val1 - n_val2);
            if (n_resu < 0)
            {
                FgDatos.SetData(FgDatos.Rows.Count - 1, 15, n_resu.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 16, "0.00");
            }
            else
            {
                FgDatos.SetData(FgDatos.Rows.Count - 1, 15, "0.00");
                FgDatos.SetData(FgDatos.Rows.Count - 1, 16, n_resu.ToString("0.00"));
            }

            // SUMAS CUENTA RESULTADOS POR FUNCION
            n_val1 = Convert.ToDouble(FgDatos.GetData(FgDatos.Rows.Count - 2, 17));
            n_val2 = Convert.ToDouble(FgDatos.GetData(FgDatos.Rows.Count - 2, 18));
            n_resu = (n_val1 - n_val2);
            if (n_resu < 0)
            {
                FgDatos.SetData(FgDatos.Rows.Count - 1, 17, n_resu.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 18, "0.00");
            }
            else
            {
                FgDatos.SetData(FgDatos.Rows.Count - 1, 17, "0.00");
                FgDatos.SetData(FgDatos.Rows.Count - 1, 18, n_resu.ToString("0.00"));
            }


            FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
            FgDatos.SetData(FgDatos.Rows.Count - 1, 2, "S U M A S   I G U A L E S ==>");

            n_valor = funFlex.FlexSumarCol(FgDatos, 9, FgDatos.Rows.Count - 3, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 9, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 10, FgDatos.Rows.Count - 3, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 10, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 11, FgDatos.Rows.Count - 3, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 11, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 12, FgDatos.Rows.Count - 3, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 12, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 13, FgDatos.Rows.Count - 3, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 13, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 14, FgDatos.Rows.Count - 3, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 14, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 15, FgDatos.Rows.Count - 3, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 15, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 16, FgDatos.Rows.Count - 3, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 16, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 17, FgDatos.Rows.Count - 3, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 17, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 18, FgDatos.Rows.Count - 3, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 18, n_valor.ToString("0.00"));

            funFlex.Flex_PintarCeldas(FgDatos, FgDatos.Rows.Count - 3, 1, FgDatos.Rows.Count - 1, 18, Color.Black, Color.LightCoral);
        }

        private void FgDatos_DoubleClick(object sender, EventArgs e)
        {
            MostrarDetalle();
        }
        void MostrarDetalle()
        {
            string[,] arrCabeceraFlexFil = new string[16, 5];
            string[,] arrCabeceraFlexFix = new string[3, 4];
            DataTable dtResult = new DataTable();
            int n_row = 0;
            double n_valor = 0;

            objdia.mysConec = mysConec;
            string c_codcue = FgDatos.GetData(FgDatos.Row, 1).ToString();
            int n_IdCuenta = Convert.ToInt32(funFunciones.NulosN(funDatos.DataTableBuscar(dtLista, "c_cuecon", "n_idcue", c_codcue, "C")));

            objdia.BalanceDetalle(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboMesIni.SelectedValue), Convert.ToInt16(CboMesFin.SelectedValue), n_IdCuenta);

            if (objdia.b_OcurrioError == true)
            {
                return;
            }
            dtResult = objdia.dtLista;

            for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
            {
                if (n_row == 0)
                {
                    if (Convert.ToDouble(dtResult.Rows[n_row]["n_datimpdeb"]) != 0) { n_valor = Convert.ToDouble(dtResult.Rows[n_row]["n_datimpdeb"]); }
                    if (Convert.ToDouble(dtResult.Rows[n_row]["n_datimphab"]) != 0) { n_valor = Convert.ToDouble(dtResult.Rows[n_row]["n_datimphab"]); }
                    dtResult.Rows[n_row]["n_saldo"] = n_valor;
                }
                else
                {
                    if (Convert.ToDouble(dtResult.Rows[n_row]["n_datimpdeb"]) != 0)
                    {
                        //n_valor = Convert.ToDouble(dtResult.Rows[n_row]["datimpdeb"]) ;
                        dtResult.Rows[n_row]["n_saldo"] = (n_valor + Convert.ToDouble(dtResult.Rows[n_row]["n_datimpdeb"]));
                    }
                    if (Convert.ToDouble(dtResult.Rows[n_row]["n_datimphab"]) != 0)
                    {
                        //n_valor = Convert.ToDouble(dtResult.Rows[n_row]["datimphab"]) ;
                        dtResult.Rows[n_row]["n_saldo"] = (n_valor - Convert.ToDouble(dtResult.Rows[n_row]["n_datimphab"]));
                    }
                    n_valor = Convert.ToDouble(dtResult.Rows[n_row]["n_saldo"]);
                }

            }

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Nº Registro";
            arrCabeceraFlexFil[0, 1] = "60";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "";
            arrCabeceraFlexFil[0, 4] = "c_numasi";

            arrCabeceraFlexFil[1, 0] = "Glosa";
            arrCabeceraFlexFil[1, 1] = "200";
            arrCabeceraFlexFil[1, 2] = "C";
            arrCabeceraFlexFil[1, 3] = "";
            arrCabeceraFlexFil[1, 4] = "c_origlo";

            arrCabeceraFlexFil[2, 0] = "Libro";
            arrCabeceraFlexFil[2, 1] = "80";
            arrCabeceraFlexFil[2, 2] = "C";
            arrCabeceraFlexFil[2, 3] = "";
            arrCabeceraFlexFil[2, 4] = "c_des";

            arrCabeceraFlexFil[3, 0] = "Fch. Operacion";
            arrCabeceraFlexFil[3, 1] = "70";
            arrCabeceraFlexFil[3, 2] = "F";
            arrCabeceraFlexFil[3, 3] = "dd/MM/yyyy";
            arrCabeceraFlexFil[3, 4] = "d_orifchdoc";

            arrCabeceraFlexFil[4, 0] = "Nº Reg. Doc.";
            arrCabeceraFlexFil[4, 1] = "60";
            arrCabeceraFlexFil[4, 2] = "C";
            arrCabeceraFlexFil[4, 3] = "";
            arrCabeceraFlexFil[4, 4] = "c_numasi";

            arrCabeceraFlexFil[5, 0] = "T.D.";
            arrCabeceraFlexFil[5, 1] = "40";
            arrCabeceraFlexFil[5, 2] = "C";
            arrCabeceraFlexFil[5, 3] = "";
            arrCabeceraFlexFil[5, 4] = "c_docabredoc";

            arrCabeceraFlexFil[6, 0] = "Fch. Doc.";
            arrCabeceraFlexFil[6, 1] = "70";
            arrCabeceraFlexFil[6, 2] = "F";
            arrCabeceraFlexFil[6, 3] = "dd/MM/yyyy";
            arrCabeceraFlexFil[6, 4] = "d_orifchdoc";

            arrCabeceraFlexFil[7, 0] = "M.";
            arrCabeceraFlexFil[7, 1] = "40";
            arrCabeceraFlexFil[7, 2] = "C";
            arrCabeceraFlexFil[7, 3] = "";
            arrCabeceraFlexFil[7, 4] = "c_docmon";

            arrCabeceraFlexFil[8, 0] = "Nº Documento";
            arrCabeceraFlexFil[8, 1] = "110";
            arrCabeceraFlexFil[8, 2] = "C";
            arrCabeceraFlexFil[8, 3] = "";
            arrCabeceraFlexFil[8, 4] = "c_docnumdoc";

            arrCabeceraFlexFil[9, 0] = "Glosa";
            arrCabeceraFlexFil[9, 1] = "200";
            arrCabeceraFlexFil[9, 2] = "C";
            arrCabeceraFlexFil[9, 3] = "";
            arrCabeceraFlexFil[9, 4] = "c_origlo";

            arrCabeceraFlexFil[10, 0] = "Nº R.U.C. / D.N.I.";
            arrCabeceraFlexFil[10, 1] = "90";
            arrCabeceraFlexFil[10, 2] = "C";
            arrCabeceraFlexFil[10, 3] = "";
            arrCabeceraFlexFil[10, 4] = "c_docruc";

            arrCabeceraFlexFil[11, 0] = "Proveedor / Cliente";
            arrCabeceraFlexFil[11, 1] = "200";
            arrCabeceraFlexFil[11, 2] = "C";
            arrCabeceraFlexFil[11, 3] = "";
            arrCabeceraFlexFil[11, 4] = "c_doccli";

            arrCabeceraFlexFil[12, 0] = "T.C.";
            arrCabeceraFlexFil[12, 1] = "50";
            arrCabeceraFlexFil[12, 2] = "D";
            arrCabeceraFlexFil[12, 3] = "0.000";
            arrCabeceraFlexFil[12, 4] = "n_dattc";

            arrCabeceraFlexFil[13, 0] = "Debe";
            arrCabeceraFlexFil[13, 1] = "80";
            arrCabeceraFlexFil[13, 2] = "D";
            arrCabeceraFlexFil[13, 3] = "0.00";
            arrCabeceraFlexFil[13, 4] = "n_datimpdeb";

            arrCabeceraFlexFil[14, 0] = "Haber";
            arrCabeceraFlexFil[14, 1] = "80";
            arrCabeceraFlexFil[14, 2] = "D";
            arrCabeceraFlexFil[14, 3] = "0.00";
            arrCabeceraFlexFil[14, 4] = "n_datimphab";

            arrCabeceraFlexFil[15, 0] = "Saldo";
            arrCabeceraFlexFil[15, 1] = "80";
            arrCabeceraFlexFil[15, 2] = "D";
            arrCabeceraFlexFil[15, 3] = "0.00";
            arrCabeceraFlexFil[15, 4] = "n_saldo";


            arrCabeceraFlexFix[0, 0] = "0";
            arrCabeceraFlexFix[0, 1] = "1";
            arrCabeceraFlexFix[0, 2] = "4";
            arrCabeceraFlexFix[0, 3] = "DATOS DE LA OPERACION";

            arrCabeceraFlexFix[1, 0] = "0";
            arrCabeceraFlexFix[1, 1] = "5";
            arrCabeceraFlexFix[1, 2] = "12";
            arrCabeceraFlexFix[1, 3] = "DATOS DEL DOCUMENTO";

            arrCabeceraFlexFix[2, 0] = "0";
            arrCabeceraFlexFix[2, 1] = "13";
            arrCabeceraFlexFix[2, 2] = "16";
            arrCabeceraFlexFix[2, 3] = "DATOS DE LA OPERACION";

            funDatos.Filtrar_Titulo = "CONTABILIDAD - MAYOR DE LA CUENTA N° " + c_codcue;
            funDatos.MostrarDatos_NumFilasCabecera = 3;

            dtResult = funDatos.MostrarDatos(arrCabeceraFlexFil, dtResult, arrCabeceraFlexFix);            
        }

        private void CboLib_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                CboLib.SelectedValue = 0;
            }
        }
    }
}
