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
    public partial class FrmRegCompras : Form
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
        CN_mae_clipro objPro = new CN_mae_clipro();
        CN_mae_meses objMes = new CN_mae_meses();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();

        DataTable dtMon = new DataTable();
        DataTable dtPro = new DataTable();
        DataTable dtMes = new DataTable();
        DataTable dtTipDoc = new DataTable();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();

        string[,] arrCabeceraFlex1 = new string[2, 5];
        string[,] arrCabeceraFlex2 = new string[2, 5];
        string[,] arrCabecera1 = new string[36, 5];
        string[,] arrCabecera2 = new string[15, 5];
        string[,] arrCabecera3 = new string[24, 5];

        string[,] arrCabResumen1 = new string[13, 5];
        string[,] arrCabResumen2 = new string[13, 5];
        string[,] arrCabResumen3 = new string[9, 5];

        public FrmRegCompras()
        {
            InitializeComponent();
        }
        private void FrmRegCompras_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarDT();
        }
        void CargarDT()
        {
            objMon.mysConec = mysConec;
            dtMon = objMon.Listar();

            objTipDoc.mysConec = mysConec;
            dtTipDoc = objTipDoc.Listar();
            dtTipDoc = funDatos.DataTableFiltrar(dtTipDoc, "n_escom = 1 AND n_idtipo = 1");
            
            objPro.mysConec = mysConec;
            dtPro = objPro.ListarProveedor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objMes.mysConec = mysConec;
            dtMes = objMes.Listar();
            dtMes = funDatos.DataTableFiltrar(dtMes, "n_id NOT IN(0,13)");

            funDatos.ComboBoxCargarDataTable(CboMes, dtMes, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMon, dtMon, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboPro, dtPro, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtTipDoc, "n_id", "c_des");

            CboMon.SelectedValue = 115;       // LA MONEDA POR EFECTO ES SOLES
            CboMes.SelectedValue = DateTime.Now.Month;
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

            OptVis1.Checked = true;
            OptSor3.Checked = true;
            OptAll.Checked = true;

            Tab02.SelectedIndex = 0;

            if (n_Libro == 8)
            {
                Cabecera1();
                this.Text = "CONTABILIDAD - REGISTRO DE COMPRAS";
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, false);
                funFlex.FlexMostrarDatos(FgRes, arrCabResumen1, dtResumen, 3, false);
                Configurarcabecera1();
            }
            if (n_Libro == 32)
            {
                Cabecera2();
                this.Text = "CONTABILIDAD - REGISTRO RENTA 4ta CATEGORIA";
                OptVis2.Enabled = false;
                OptVis3.Enabled = false;
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera2, dtLista, 3, false);
                Tab02.TabPages[1].Visible = false;
                Tab02.TabPages[1].Enabled = false;
                TooGenLE.Visible = false;
            }
            if (n_Libro == 14)
            {
                Cabecera3();
                this.Text = "CONTABILIDAD - REGISTRO DE VENTAS";
                OptVis2.Enabled = false;
                OptVis3.Enabled = false;
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera3, dtLista, 3, false);
                funFlex.FlexMostrarDatos(FgRes, arrCabResumen3, dtResumen, 3, false);
                Configurarcabecera3();
            }
        }
        void Cabecera1()
        {
            arrCabecera1[0, 0] = "Nº Registro";
            arrCabecera1[0, 1] = "60";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_numreg";

            arrCabecera1[1, 0] = "Fecha Emision";
            arrCabecera1[1, 1] = "70";
            arrCabecera1[1, 2] = "F";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "d_fchdoc";

            arrCabecera1[2, 0] = "Tipo CP";
            arrCabecera1[2, 1] = "40";
            arrCabecera1[2, 2] = "C";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "c_codsun";

            arrCabecera1[3, 0] = "Nº Serie";
            arrCabecera1[3, 1] = "50";
            arrCabecera1[3, 2] = "C";
            arrCabecera1[3, 3] = "";
            arrCabecera1[3, 4] = "c_numser";

            arrCabecera1[4, 0] = "Nº CP";
            arrCabecera1[4, 1] = "80";
            arrCabecera1[4, 2] = "C";
            arrCabecera1[4, 3] = "";
            arrCabecera1[4, 4] = "c_numdoc";

            arrCabecera1[5, 0] = "Tip. Doc. Iden.";
            arrCabecera1[5, 1] = "40";
            arrCabecera1[5, 2] = "C";
            arrCabecera1[5, 3] = "";
            arrCabecera1[5, 4] = "c_docidecodsun";

            arrCabecera1[6, 0] = "Nº Doc. Identidad";
            arrCabecera1[6, 1] = "80";
            arrCabecera1[6, 2] = "C";
            arrCabecera1[6, 3] = "";
            arrCabecera1[6, 4] = "c_idenumdoc";

            arrCabecera1[7, 0] = "Apellidos y Nombres / Razon Social";
            arrCabecera1[7, 1] = "200";
            arrCabecera1[7, 2] = "C";
            arrCabecera1[7, 3] = "";
            arrCabecera1[7, 4] = "c_nombre";

            arrCabecera1[8, 0] = "Glosa";
            arrCabecera1[8, 1] = "200";
            arrCabecera1[8, 2] = "C";
            arrCabecera1[8, 3] = "";
            arrCabecera1[8, 4] = "c_glosa";

            arrCabecera1[9, 0] = "T.C.";
            arrCabecera1[9, 1] = "40";
            arrCabecera1[9, 2] = "D";
            arrCabecera1[9, 3] = "0.000";
            arrCabecera1[9, 4] = "n_tc";

            arrCabecera1[10, 0] = "M.";
            arrCabecera1[10, 1] = "40";
            arrCabecera1[10, 2] = "C";
            arrCabecera1[10, 3] = "";
            arrCabecera1[10, 4] = "c_codsun2";

            arrCabecera1[11, 0] = "Base 1 Vta Grav Exc";
            arrCabecera1[11, 1] = "70";
            arrCabecera1[11, 2] = "D";
            arrCabecera1[11, 3] = "";
            arrCabecera1[11, 4] = "n_impbru";

            arrCabecera1[12, 0] = "Base 2 Vta No Grav";
            arrCabecera1[12, 1] = "70";
            arrCabecera1[12, 2] = "D";
            arrCabecera1[12, 3] = "";
            arrCabecera1[12, 4] = "n_impbru2";

            arrCabecera1[13, 0] = "Base 3 Vta No Grav";
            arrCabecera1[13, 1] = "70";
            arrCabecera1[13, 2] = "D";
            arrCabecera1[13, 3] = "";
            arrCabecera1[13, 4] = "n_impbru3";

            arrCabecera1[14, 0] = "Valor Adq. No Grav.";
            arrCabecera1[14, 1] = "70";
            arrCabecera1[14, 2] = "D";
            arrCabecera1[14, 3] = "";
            arrCabecera1[14, 4] = "n_impinaf";

            arrCabecera1[15, 0] = "Desc Obtenidos";
            arrCabecera1[15, 1] = "70";
            arrCabecera1[15, 2] = "D";
            arrCabecera1[15, 3] = "";
            arrCabecera1[15, 4] = "n_dscimp";

            arrCabecera1[16, 0] = "I.S.C.";
            arrCabecera1[16, 1] = "70";
            arrCabecera1[16, 2] = "D";
            arrCabecera1[16, 3] = "";
            arrCabecera1[16, 4] = "n_impisc";

            arrCabecera1[17, 0] = "Tasa IGV(%)";
            arrCabecera1[17, 1] = "40";
            arrCabecera1[17, 2] = "D";
            arrCabecera1[17, 3] = "";
            arrCabecera1[17, 4] = "n_tasaigv";

            arrCabecera1[18, 0] = "IGV Base 1";
            arrCabecera1[18, 1] = "60";
            arrCabecera1[18, 2] = "D";
            arrCabecera1[18, 3] = "";
            arrCabecera1[18, 4] = "n_impigv";

            arrCabecera1[19, 0] = "IGV Base 2";
            arrCabecera1[19, 1] = "60";
            arrCabecera1[19, 2] = "D";
            arrCabecera1[19, 3] = "";
            arrCabecera1[19, 4] = "n_impigv2";

            arrCabecera1[20, 0] = "IGV Base 3";
            arrCabecera1[20, 1] = "60";
            arrCabecera1[20, 2] = "D";
            arrCabecera1[20, 3] = "";
            arrCabecera1[20, 4] = "n_impigv3";

            arrCabecera1[21, 0] = "Otros Cargos";
            arrCabecera1[21, 1] = "70";
            arrCabecera1[21, 2] = "D";
            arrCabecera1[21, 3] = "";
            arrCabecera1[21, 4] = "n_impotr";

            arrCabecera1[22, 0] = "Importe Total";
            arrCabecera1[22, 1] = "70";
            arrCabecera1[22, 2] = "D";
            arrCabecera1[22, 3] = "";
            arrCabecera1[22, 4] = "n_imptotcom";

            arrCabecera1[23, 0] = "CP y NC y ND Sujetos a Retencion";
            arrCabecera1[23, 1] = "70";
            arrCabecera1[23, 2] = "C";
            arrCabecera1[23, 3] = "";
            arrCabecera1[23, 4] = "";

            arrCabecera1[24, 0] = "Nº Documento";
            arrCabecera1[24, 1] = "80";
            arrCabecera1[24, 2] = "C";
            arrCabecera1[24, 3] = "";
            arrCabecera1[24, 4] = "c_detranumdoc";

            arrCabecera1[25, 0] = "Fecha";
            arrCabecera1[25, 1] = "70";
            arrCabecera1[25, 2] = "F";
            arrCabecera1[25, 3] = "";
            arrCabecera1[25, 4] = "c_detrafchdoc";

            arrCabecera1[26, 0] = "Importe";
            arrCabecera1[26, 1] = "70";
            arrCabecera1[26, 2] = "D";
            arrCabecera1[26, 3] = "0.00";
            arrCabecera1[26, 4] = "c_detraimp";

            arrCabecera1[27, 0] = "Glosa";
            arrCabecera1[27, 1] = "70";
            arrCabecera1[27, 2] = "C";
            arrCabecera1[27, 3] = "";
            arrCabecera1[27, 4] = "c_detraglosa";

            arrCabecera1[28, 0] = "Registro";
            arrCabecera1[28, 1] = "70";
            arrCabecera1[28, 2] = "C";
            arrCabecera1[28, 3] = "";
            arrCabecera1[28, 4] = "";

            arrCabecera1[29, 0] = "Fecha CP";
            arrCabecera1[29, 1] = "70";
            arrCabecera1[29, 2] = "F";
            arrCabecera1[29, 3] = "";
            arrCabecera1[29, 4] = "d_docreffchdoc";

            arrCabecera1[30, 0] = "Tipo CP";
            arrCabecera1[30, 1] = "40";
            arrCabecera1[30, 2] = "C";
            arrCabecera1[30, 3] = "";
            arrCabecera1[30, 4] = "c_docreftipdoc";

            arrCabecera1[31, 0] = "T.D.";
            arrCabecera1[31, 1] = "40";
            arrCabecera1[31, 2] = "C";
            arrCabecera1[31, 3] = "";
            arrCabecera1[31, 4] = "c_docreftipdocabr";

            arrCabecera1[32, 0] = "Nº Serie";
            arrCabecera1[32, 1] = "40";
            arrCabecera1[32, 2] = "C";
            arrCabecera1[32, 3] = "";
            arrCabecera1[32, 4] = "c_docrefnumser";

            arrCabecera1[33, 0] = "Nº CP";
            arrCabecera1[33, 1] = "80";
            arrCabecera1[33, 2] = "C";
            arrCabecera1[33, 3] = "";
            arrCabecera1[33, 4] = "c_docrefnumdoc";

            arrCabecera1[34, 0] = "M";
            arrCabecera1[34, 1] = "40";
            arrCabecera1[34, 2] = "C";
            arrCabecera1[34, 3] = "";
            arrCabecera1[34, 4] = "c_docrefmon";

            arrCabecera1[35, 0] = "Importe";
            arrCabecera1[35, 1] = "80";
            arrCabecera1[35, 2] = "D";
            arrCabecera1[35, 3] = "0.00";
            arrCabecera1[35, 4] = "c_docrefimp";


            // CONFIGURAMOS LA CABECERA DEL RESUMEN

            arrCabResumen1[0, 0] = "Descripcion";
            arrCabResumen1[0, 1] = "300";
            arrCabResumen1[0, 2] = "C";
            arrCabResumen1[0, 3] = "";
            arrCabResumen1[0, 4] = "c_des";

            arrCabResumen1[1, 0] = "Abrev";
            arrCabResumen1[1, 1] = "40";
            arrCabResumen1[1, 2] = "C";
            arrCabResumen1[1, 3] = "";
            arrCabResumen1[1, 4] = "c_abr";

            arrCabResumen1[2, 0] = "Base 1 Vta. Grav. Exc.";
            arrCabResumen1[2, 1] = "80";
            arrCabResumen1[2, 2] = "D";
            arrCabResumen1[2, 3] = "0.00";
            arrCabResumen1[2, 4] = "n_impbru";

            arrCabResumen1[3, 0] = "Base 2 Vta. Grav. Exc.";
            arrCabResumen1[3, 1] = "80";
            arrCabResumen1[3, 2] = "D";
            arrCabResumen1[3, 3] = "0.00";
            arrCabResumen1[3, 4] = "n_impbru2";

            arrCabResumen1[4, 0] = "Base 3 Vta. Grav. Exc.";
            arrCabResumen1[4, 1] = "80";
            arrCabResumen1[4, 2] = "N";
            arrCabResumen1[4, 3] = "0.00";
            arrCabResumen1[4, 4] = "n_impbru3";

            arrCabResumen1[5, 0] = "Valor Adq. no Gravados";
            arrCabResumen1[5, 1] = "80";
            arrCabResumen1[5, 2] = "D";
            arrCabResumen1[5, 3] = "0.00";
            arrCabResumen1[5, 4] = "n_impinaf";

            arrCabResumen1[6, 0] = "Desc. Obtenido";
            arrCabResumen1[6, 1] = "80";
            arrCabResumen1[6, 2] = "D";
            arrCabResumen1[6, 3] = "0.00";
            arrCabResumen1[6, 4] = "n_imptotdscobt";

            arrCabResumen1[7, 0] = "I.S.C.";
            arrCabResumen1[7, 1] = "80";
            arrCabResumen1[7, 2] = "D";
            arrCabResumen1[7, 3] = "0.00";
            arrCabResumen1[7, 4] = "n_impisc";

            arrCabResumen1[8, 0] = "IGV Base 1";
            arrCabResumen1[8, 1] = "80";
            arrCabResumen1[8, 2] = "D";
            arrCabResumen1[8, 3] = "0.00";
            arrCabResumen1[8, 4] = "n_impigv";

            arrCabResumen1[9, 0] = "IGV Base 2";
            arrCabResumen1[9, 1] = "80";
            arrCabResumen1[9, 2] = "D";
            arrCabResumen1[9, 3] = "0.00";
            arrCabResumen1[9, 4] = "n_impigv2";

            arrCabResumen1[10, 0] = "IGV Base 3";
            arrCabResumen1[10, 1] = "80";
            arrCabResumen1[10, 2] = "D";
            arrCabResumen1[10, 3] = "0.00";
            arrCabResumen1[10, 4] = "n_impigv3";

            arrCabResumen1[11, 0] = "Otros Cargos";
            arrCabResumen1[11, 1] = "80";
            arrCabResumen1[11, 2] = "D";
            arrCabResumen1[11, 3] = "0.00";
            arrCabResumen1[11, 4] = "n_impotr";

            arrCabResumen1[12, 0] = "Importe Total";
            arrCabResumen1[12, 1] = "80";
            arrCabResumen1[12, 2] = "D";
            arrCabResumen1[12, 3] = "0.00";
            arrCabResumen1[12, 4] = "n_imptotcom";
        }
        void Cabecera2()
        {
            arrCabecera2[0, 0] = "Nº Registro";
            arrCabecera2[0, 1] = "60";
            arrCabecera2[0, 2] = "C";
            arrCabecera2[0, 3] = "";
            arrCabecera2[0, 4] = "c_numreg";

            arrCabecera2[1, 0] = "Fch. Documento";
            arrCabecera2[1, 1] = "70";
            arrCabecera2[1, 2] = "F";
            arrCabecera2[1, 3] = "";
            arrCabecera2[1, 4] = "d_fchdoc";

            arrCabecera2[2, 0] = "Fch. Vencimiento";
            arrCabecera2[2, 1] = "70";
            arrCabecera2[2, 2] = "F";
            arrCabecera2[2, 3] = "";
            arrCabecera2[2, 4] = "d_fchven";

            arrCabecera2[3, 0] = "Nº Serie";
            arrCabecera2[3, 1] = "50";
            arrCabecera2[3, 2] = "C";
            arrCabecera2[3, 3] = "";
            arrCabecera2[3, 4] = "c_numser";

            arrCabecera2[4, 0] = "Nº Documento";
            arrCabecera2[4, 1] = "80";
            arrCabecera2[4, 2] = "C";
            arrCabecera2[4, 3] = "";
            arrCabecera2[4, 4] = "c_numdoc";

            arrCabecera2[5, 0] = "Tip. Doc. Idem.";
            arrCabecera2[5, 1] = "50";
            arrCabecera2[5, 2] = "C";
            arrCabecera2[5, 3] = "";
            arrCabecera2[5, 4] = "c_tipdoc";

            arrCabecera2[6, 0] = "Nº Doc. Ident.";
            arrCabecera2[6, 1] = "90";
            arrCabecera2[6, 2] = "C";
            arrCabecera2[6, 3] = "";
            arrCabecera2[6, 4] = "c_numdoc1";

            arrCabecera2[7, 0] = "Proveedor";
            arrCabecera2[7, 1] = "200";
            arrCabecera2[7, 2] = "C";
            arrCabecera2[7, 3] = "";
            arrCabecera2[7, 4] = "c_nombre";

            arrCabecera2[8, 0] = "Glosa";
            arrCabecera2[8, 1] = "200";
            arrCabecera2[8, 2] = "C";
            arrCabecera2[8, 3] = "";
            arrCabecera2[8, 4] = "c_glosa";

            arrCabecera2[9, 0] = "T.C.";
            arrCabecera2[9, 1] = "40";
            arrCabecera2[9, 2] = "D";
            arrCabecera2[9, 3] = "0.000";
            arrCabecera2[9, 4] = "n_tc";

            arrCabecera2[10, 0] = "M";
            arrCabecera2[10, 1] = "40";
            arrCabecera2[10, 2] = "C";
            arrCabecera2[10, 3] = "";
            arrCabecera2[10, 4] = "c_desmon";

            arrCabecera2[11, 0] = "Total Honorarios";
            arrCabecera2[11, 1] = "80";
            arrCabecera2[11, 2] = "D";
            arrCabecera2[11, 3] = "";
            arrCabecera2[11, 4] = "n_imptothon";

            arrCabecera2[12, 0] = "Imp. Rta 4ta Cat";
            arrCabecera2[12, 1] = "80";
            arrCabecera2[12, 2] = "D";
            arrCabecera2[12, 3] = "";
            arrCabecera2[12, 4] = "n_impret4ta";

            arrCabecera2[13, 0] = "Otras Retenciones";
            arrCabecera2[13, 1] = "70";
            arrCabecera2[13, 2] = "D";
            arrCabecera2[13, 3] = "";
            arrCabecera2[13, 4] = "n_impotrret";

            arrCabecera2[14, 0] = "Total Neto Pagado";
            arrCabecera2[14, 1] = "80";
            arrCabecera2[14, 2] = "D";
            arrCabecera2[14, 3] = "";
            arrCabecera2[14, 4] = "n_impnetpag";

        }
        void Cabecera3()
        {
            arrCabecera3[0, 0] = "Nº Registro";
            arrCabecera3[0, 1] = "60";
            arrCabecera3[0, 2] = "C";
            arrCabecera3[0, 3] = "";
            arrCabecera3[0, 4] = "c_numreg";

            arrCabecera3[1, 0] = "Fch. Documento";
            arrCabecera3[1, 1] = "70";
            arrCabecera3[1, 2] = "F";
            arrCabecera3[1, 3] = "";
            arrCabecera3[1, 4] = "d_fchdoc";

            arrCabecera3[2, 0] = "Fch. Vencimiento";
            arrCabecera3[2, 1] = "70";
            arrCabecera3[2, 2] = "F";
            arrCabecera3[2, 3] = "";
            arrCabecera3[2, 4] = "d_fchven";

            arrCabecera3[3, 0] = "TCP";
            arrCabecera3[3, 1] = "40";
            arrCabecera3[3, 2] = "C";
            arrCabecera3[3, 3] = "";
            arrCabecera3[3, 4] = "c_codsun";

            arrCabecera3[4, 0] = "Nº Serie";
            arrCabecera3[4, 1] = "50";
            arrCabecera3[4, 2] = "C";
            arrCabecera3[4, 3] = "";
            arrCabecera3[4, 4] = "c_numser";

            arrCabecera3[5, 0] = "Nº CP";
            arrCabecera3[5, 1] = "70";
            arrCabecera3[5, 2] = "C";
            arrCabecera3[5, 3] = "";
            arrCabecera3[5, 4] = "c_numdoc";

            arrCabecera3[6, 0] = "Tipo Doc. Idem";
            arrCabecera3[6, 1] = "50";
            arrCabecera3[6, 2] = "C";
            arrCabecera3[6, 3] = "";
            arrCabecera3[6, 4] = "c_codsun1";

            arrCabecera3[7, 0] = "Nº de Doc. Iden.";
            arrCabecera3[7, 1] = "90";
            arrCabecera3[7, 2] = "C";
            arrCabecera3[7, 3] = "";
            arrCabecera3[7, 4] = "c_numdoc1";

            arrCabecera3[8, 0] = "Apellidos y Nombres / Razon Social";
            arrCabecera3[8, 1] = "200";
            arrCabecera3[8, 2] = "C";
            arrCabecera3[8, 3] = "";
            arrCabecera3[8, 4] = "c_nombre";

            arrCabecera3[9, 0] = "T.C.";
            arrCabecera3[9, 1] = "40";
            arrCabecera3[9, 2] = "D";
            arrCabecera3[9, 3] = "0.000";
            arrCabecera3[9, 4] = "n_tc";

            arrCabecera3[10, 0] = "M";
            arrCabecera3[10, 1] = "40";
            arrCabecera3[10, 2] = "C";
            arrCabecera3[10, 3] = "";
            arrCabecera3[10, 4] = "c_codsun2";

            arrCabecera3[11, 0] = "Valor de la Export.";
            arrCabecera3[11, 1] = "80";
            arrCabecera3[11, 2] = "D";
            arrCabecera3[11, 3] = "";
            arrCabecera3[11, 4] = "n_impbru";

            arrCabecera3[12, 0] = "Base Imp. Oper. Grav";
            arrCabecera3[12, 1] = "80";
            arrCabecera3[12, 2] = "D";
            arrCabecera3[12, 3] = "";
            arrCabecera3[12, 4] = "n_impbru2";

            arrCabecera3[13, 0] = "Operaciones Exoneradas";
            arrCabecera3[13, 1] = "70";
            arrCabecera3[13, 2] = "D";
            arrCabecera3[13, 3] = "";
            arrCabecera3[13, 4] = "n_impbru3";

            arrCabecera3[14, 0] = "Operaciones Inafectas";
            arrCabecera3[14, 1] = "80";
            arrCabecera3[14, 2] = "D";
            arrCabecera3[14, 3] = "";
            arrCabecera3[14, 4] = "n_impinaf";

            arrCabecera3[15, 0] = "I.S.C.";
            arrCabecera3[15, 1] = "80";
            arrCabecera3[15, 2] = "D";
            arrCabecera3[15, 3] = "";
            arrCabecera3[15, 4] = "n_impisc";

            arrCabecera3[16, 0] = "Tasa IGV(%)";
            arrCabecera3[16, 1] = "80";
            arrCabecera3[16, 2] = "D";
            arrCabecera3[16, 3] = "";
            arrCabecera3[16, 4] = "n_tasaigv";

            arrCabecera3[17, 0] = "I.G.V. e I.M.P.";
            arrCabecera3[17, 1] = "80";
            arrCabecera3[17, 2] = "D";
            arrCabecera3[17, 3] = "";
            arrCabecera3[17, 4] = "n_impigv";

            arrCabecera3[18, 0] = "Otros Trib. o Cargos";
            arrCabecera3[18, 1] = "80";
            arrCabecera3[18, 2] = "D";
            arrCabecera3[18, 3] = "";
            arrCabecera3[18, 4] = "n_impotr";

            arrCabecera3[19, 0] = "Importe Total";
            arrCabecera3[19, 1] = "80";
            arrCabecera3[19, 2] = "D";
            arrCabecera3[19, 3] = "";
            arrCabecera3[19, 4] = "n_imptotven";

            arrCabecera3[20, 0] = "Fch. Doc.";
            arrCabecera3[20, 1] = "80";
            arrCabecera3[20, 2] = "F";
            arrCabecera3[20, 3] = "";
            arrCabecera3[20, 4] = "d_fchdoc1";

            arrCabecera3[21, 0] = "TCP";
            arrCabecera3[21, 1] = "50";
            arrCabecera3[21, 2] = "C";
            arrCabecera3[21, 3] = "";
            arrCabecera3[21, 4] = "c_codsun3";

            arrCabecera3[22, 0] = "Nº Serie";
            arrCabecera3[22, 1] = "70";
            arrCabecera3[22, 2] = "C";
            arrCabecera3[22, 3] = "";
            arrCabecera3[22, 4] = "c_numser1";

            arrCabecera3[23, 0] = "Nº CP";
            arrCabecera3[23, 1] = "80";
            arrCabecera3[23, 2] = "C";
            arrCabecera3[23, 3] = "";
            arrCabecera3[23, 4] = "c_numdoc2";

            // CONFIGURAMOS LA CABECERA DEL RESUMEN

            arrCabResumen3[0, 0] = "Descripcion";
            arrCabResumen3[0, 1] = "300";
            arrCabResumen3[0, 2] = "C";
            arrCabResumen3[0, 3] = "";
            arrCabResumen3[0, 4] = "c_des";

            arrCabResumen3[1, 0] = "Abrev";
            arrCabResumen3[1, 1] = "40";
            arrCabResumen3[1, 2] = "C";
            arrCabResumen3[1, 3] = "";
            arrCabResumen3[1, 4] = "c_abr";

            arrCabResumen3[2, 0] = "Valor de la Exportacion";
            arrCabResumen3[2, 1] = "80";
            arrCabResumen3[2, 2] = "D";
            arrCabResumen3[2, 3] = "0.00";
            arrCabResumen3[2, 4] = "n_valexp";

            arrCabResumen3[3, 0] = "Base Impon. Oper. Grav";
            arrCabResumen3[3, 1] = "80";
            arrCabResumen3[3, 2] = "D";
            arrCabResumen3[3, 3] = "0.00";
            arrCabResumen3[3, 4] = "n_impbru";

            arrCabResumen3[4, 0] = "Operaciones Inafectas";
            arrCabResumen3[4, 1] = "80";
            arrCabResumen3[4, 2] = "D";
            arrCabResumen3[4, 3] = "0.00";
            arrCabResumen3[4, 4] = "n_impinaf";

            arrCabResumen3[5, 0] = "I.G.V.  e I.M.P.";
            arrCabResumen3[5, 1] = "80";
            arrCabResumen3[5, 2] = "D";
            arrCabResumen3[5, 3] = "0.00";
            arrCabResumen3[5, 4] = "n_impigv";

            arrCabResumen3[6, 0] = "I.S.C.";
            arrCabResumen3[6, 1] = "80";
            arrCabResumen3[6, 2] = "D";
            arrCabResumen3[6, 3] = "0.00";
            arrCabResumen3[6, 4] = "n_impisc";

            arrCabResumen3[7, 0] = "Otros Trib. o Cargos";
            arrCabResumen3[7, 1] = "80";
            arrCabResumen3[7, 2] = "D";
            arrCabResumen3[7, 3] = "0.00";
            arrCabResumen3[7, 4] = "n_impotr";

            arrCabResumen3[8, 0] = "Importe Total";
            arrCabResumen3[8, 1] = "80";
            arrCabResumen3[8, 2] = "D";
            arrCabResumen3[8, 3] = "0.00";
            arrCabResumen3[8, 4] = "n_imptotven";

        }
        private void FrmRegCompras_Resize(object sender, EventArgs e)
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
            if (Convert.ToInt32(CboMes.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el periodo a consultar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMes.Focus();
                return;
            }

            if (OptSel.Checked == true)
            {
                if (Convert.ToInt32(CboPro.SelectedValue) == 0)
                {
                    MessageBox.Show("¡ No ha seleccionado el proveedor !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    CboPro.Focus();
                    return;
                }
            }

            Tab02.SelectedIndex = 0;
            //int n_tipord = 0;
            CN_con_diario funCom = new CN_con_diario();
            funCom.mysConec = mysConec;
            funCom.STU_SISTEMA = STU_SISTEMA;
            
            bool b_Result = false;
            if (n_Libro == 8)
            {
                b_Result = funCom.RegistroCompras(STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMes.SelectedValue), n_Libro,  STU_SISTEMA.EMPRESAID);
            }
            if (n_Libro == 32)
            {
                b_Result = funCom.RegistroHonorarios(STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMes.SelectedValue), n_Libro, STU_SISTEMA.EMPRESAID);
            }
            if (n_Libro == 14)
            {
                b_Result = funCom.RegistroVentas(STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMes.SelectedValue), n_Libro, STU_SISTEMA.EMPRESAID);
            }

            if (b_Result ==true)
            {
                dtLista = funCom.dtLista;
                dtResumen = funCom.dtResumen;
                if (dtLista.Rows.Count != 0)
                {
                    Ordenar();
                }
                else
                {
                    MessageBox.Show("¡ No hay registros en el periodo indicado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }
        void Ordenar()
        {
            string c_orden = "";
            if (dtLista == null) { return; }
            if (dtLista.Rows.Count == 0) { return; }

            if (OptSor1.Checked == true) { dtLista = funDatos.DataTableOrdenar(dtLista, "d_fchdoc"); c_orden = "fecha de documento"; }
            if (OptSor2.Checked == true) { dtLista = funDatos.DataTableOrdenar(dtLista, "c_numdocord"); c_orden = "numero de documento"; }
            if (OptSor3.Checked == true) { dtLista = funDatos.DataTableOrdenar(dtLista, "c_numreg"); c_orden = "numero de registro"; }
            if (OptSor4.Checked == true) { dtLista = funDatos.DataTableOrdenar(dtLista, "c_fchdocnumdocord");  c_orden = "fecha de documento y numero de registro"; }

            if (n_Libro == 8) 
            { 
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, true);
                funFlex.FlexMostrarDatos(FgRes, arrCabResumen1, dtResumen, 3, true);
                Configurarcabecera1();
            }
            if (n_Libro == 32) { funFlex.FlexMostrarDatos(FgDatos, arrCabecera2, dtLista, 3, true); }
            if (n_Libro == 14) 
            { 
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera3, dtLista, 3, true);
                funFlex.FlexMostrarDatos(FgRes, arrCabResumen3, dtResumen, 3, true);
                Configurarcabecera3();
            }
            LblNumReg.Text = dtLista.Rows.Count.ToString();
            SumarColumnas();
            MessageBox.Show("¡ Se ordeno por " + c_orden + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        void Configurarcabecera1()
        { 
            // CABECERA DETALLE
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 2, 5, "COMPROBANTE DE PAGO", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 6, 8, "INFORMACION DEL PROVEEDOR", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 9, 11, "COMPROBANTE DE PAGO", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 12, 14, "BASE IMPONIBLE", 1);

            funFlex.Flex_FixUniColumnas(FgDatos, 0, 18, 21, "IMPUESTO GENERAL A LA VENTAS", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 25, 28, "CONSTANCIA DE DEPOSITO SPOT", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 29, 36, "REFERENCIA DEL COMPROBANTE DE PAGO O DOCUMENTO ORIGINAL", 1);

            // CABECERA RSUMEN
            funFlex.Flex_FixUniColumnas(FgRes, 0, 1, 2, "TIPO DE DOCUMENTO", 1);
            funFlex.Flex_FixUniColumnas(FgRes, 0, 3, 5, "BASE IMPONIBLE", 1);
            funFlex.Flex_FixUniColumnas(FgRes, 0, 9, 11, "COMPROBANTE DE PAGO", 1);
        }
        void Configurarcabecera2()
        {
        }
        void Configurarcabecera3()
        {
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 2, 6, "COMPROBANTE DE PAGO", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 7, 9, "INFORMACION DEL CLIENTE", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 21, 24, "REFERENCIA DEL COMPROBANTE DE PAGO", 1);

            funFlex.Flex_FixUniColumnas(FgRes, 0, 1, 2, "TIPO DE DOCUMENTO", 1);
        }
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            funFlex.ExportToExcel_NumFilaCabecera = 3;
            if (Tab02.SelectedIndex == 0)
            { 
                if (n_Libro == 8)
                { 
                    string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-08-COMPRAS-DETALLADO" + ".xls";
                    funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "REGISTRO DE COMPRAS - DETALLADO", "MES DE " + CboMes.Text.ToUpper(), c_nomarch);
                }
                if (n_Libro == 32)
                {
                    string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-32-HONORARIOS-DETALLADO" + ".xls";
                    funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "REGISTRO DE 4TA CATEGORIA - DETALLADO", "MES DE " + CboMes.Text.ToUpper(), c_nomarch);
                }
                if (n_Libro == 14)
                {
                    string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-14-VENTAS-DETALLADO" + ".xls";
                    funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "REGISTRO DE VENTAS - DETALLADO", "MES DE " + CboMes.Text.ToUpper(), c_nomarch);
                }
            }
            if (Tab02.SelectedIndex == 1)
            {
                if (n_Libro == 8)
                {
                    string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-08-COMPRAS-RESUMEN" + ".xls";
                    funFlex.ExportToExcel(FgRes, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "REGISTRO DE COMPRAS - RESUMEN", "MES DE " + CboMes.Text.ToUpper(), c_nomarch);
                }
                if (n_Libro == 32)
                {
                    string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-32-HONORARIOS-RESUMEN" + ".xls";
                    funFlex.ExportToExcel(FgRes, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "REGISTRO DE 4TA CATEGORIA - RESUMEN", "MES DE " + CboMes.Text.ToUpper(), c_nomarch);
                }
                if (n_Libro == 14)
                {
                    string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-14-VENTAS-RESUMEN" + ".xls";
                    funFlex.ExportToExcel(FgRes, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "REGISTRO DE VENTAS - RESUMEN", "MES DE " + CboMes.Text.ToUpper(), c_nomarch);
                }
            }
        }
        private void OptSor1_CheckedChanged(object sender, EventArgs e)
        {
            if (OptSor1.Checked == true) { Ordenar(); }
        }
        private void OptSor2_CheckedChanged(object sender, EventArgs e)
        {
            if (OptSor2.Checked == true) { Ordenar(); }
        }
        private void OptSor3_CheckedChanged(object sender, EventArgs e)
        {
            if (OptSor3.Checked == true) { Ordenar(); }
        }
        private void OptSor4_CheckedChanged(object sender, EventArgs e)
        {
            if (OptSor4.Checked == true) { Ordenar(); }
        }
        void SumarColumnas()
        {
            double n_tot1 = 0;
            double n_tot2 = 0;
            double n_tot3 = 0;
            double n_tot4 = 0;
            double n_tot5 = 0;
            double n_tot6 = 0;
            double n_tot7 = 0;
            double n_tot8 = 0;
            double n_tot9 = 0;
            double n_tot10 = 0;
            double n_tot11 = 0;
            double n_tot12 = 0;
            int n_row = 0;

            if (n_Libro == 8)
            {
                for (n_row = 3; n_row <= FgDatos.Rows.Count - 1; n_row++)
                {
                    if (FgDatos.GetData(n_row, 12).ToString() == "07")
                    {
                        //n_tot1 = n_tot1 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 12)));
                        //n_tot2 = n_tot2 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 13)));
                        //n_tot3 = n_tot3 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 14)));
                        //n_tot4 = n_tot4 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 15)));
                        //n_tot5 = n_tot5 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 16)));
                        //n_tot6 = n_tot6 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 17)));
                        //n_tot7 = n_tot7 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 18)));
                        //n_tot8 = n_tot8 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 19)));
                        //n_tot9 = n_tot9 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 20)));
                        //n_tot10 = n_tot10 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 21)));
                        //n_tot11 = n_tot11 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 22)));
                        //n_tot12 = n_tot12 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 23)));

                        n_tot1 = n_tot1 + (Convert.ToDouble(FgDatos.GetData(n_row, 12)));
                        n_tot2 = n_tot2 + (Convert.ToDouble(FgDatos.GetData(n_row, 13)));
                        n_tot3 = n_tot3 + (Convert.ToDouble(FgDatos.GetData(n_row, 14)));
                        n_tot4 = n_tot4 + (Convert.ToDouble(FgDatos.GetData(n_row, 15)));
                        n_tot5 = n_tot5 + (Convert.ToDouble(FgDatos.GetData(n_row, 16)));
                        n_tot6 = n_tot6 + (Convert.ToDouble(FgDatos.GetData(n_row, 17)));
                        n_tot7 = n_tot7 + (Convert.ToDouble(FgDatos.GetData(n_row, 18)));
                        n_tot8 = n_tot8 + (Convert.ToDouble(FgDatos.GetData(n_row, 19)));
                        n_tot9 = n_tot9 + (Convert.ToDouble(FgDatos.GetData(n_row, 20)));
                        n_tot10 = n_tot10 + (Convert.ToDouble(FgDatos.GetData(n_row, 21)));
                        n_tot11 = n_tot11 + (Convert.ToDouble(FgDatos.GetData(n_row, 22)));
                        n_tot12 = n_tot12 + (Convert.ToDouble(FgDatos.GetData(n_row, 23)));
                    }
                    else
                    { 
                        n_tot1 = n_tot1 + Convert.ToDouble(FgDatos.GetData(n_row, 12));
                        n_tot2 = n_tot2 + Convert.ToDouble(FgDatos.GetData(n_row, 13));
                        n_tot3 = n_tot3 + Convert.ToDouble(FgDatos.GetData(n_row, 14));
                        n_tot4 = n_tot4 + Convert.ToDouble(FgDatos.GetData(n_row, 15));
                        n_tot5 = n_tot5 + Convert.ToDouble(FgDatos.GetData(n_row, 16));
                        n_tot6 = n_tot6 + Convert.ToDouble(FgDatos.GetData(n_row, 17));
                        n_tot7 = n_tot7 + Convert.ToDouble(FgDatos.GetData(n_row, 18));
                        n_tot8 = n_tot8 + Convert.ToDouble(FgDatos.GetData(n_row, 19));
                        n_tot9 = n_tot9 + Convert.ToDouble(FgDatos.GetData(n_row, 20));
                        n_tot10 = n_tot10 + Convert.ToDouble(FgDatos.GetData(n_row, 21));
                        n_tot11 = n_tot11 + Convert.ToDouble(FgDatos.GetData(n_row, 22));
                        n_tot12 = n_tot12 + Convert.ToDouble(FgDatos.GetData(n_row, 23));
                    }
                }

                FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
                FgDatos.SetData(FgDatos.Rows.Count - 1, 9, "TOTAL ==>");

                FgDatos.SetData(FgDatos.Rows.Count - 1, 12, n_tot1.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 13, n_tot2.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 14, n_tot3.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 15, n_tot4.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 16, n_tot5.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 17, n_tot6.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 18, n_tot7.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 19, n_tot8.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 20, n_tot9.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 21, n_tot10.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 22, n_tot11.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 23, n_tot12.ToString("0.00"));
                              
                //// *****************
                //// TOTAL DEL RESUMEN
                n_tot1 = 0;
                n_tot2 = 0;
                n_tot3 = 0;
                n_tot4 = 0;
                n_tot5 = 0;
                n_tot6 = 0;
                n_tot7 = 0;
                n_tot8 = 0;
                n_tot8 = 0;
                n_tot10 = 0;
                n_tot11 = 0;
                n_tot12 = 0;

                for (n_row = 3; n_row <= FgRes.Rows.Count - 1; n_row++)
                {
                    if (FgDatos.GetData(n_row, 12).ToString() == "NC")
                    {
                        //n_tot1 = n_tot1 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 3)));
                        //n_tot2 = n_tot2 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 4)));
                        //n_tot3 = n_tot3 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 5)));
                        //n_tot4 = n_tot4 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 6)));
                        //n_tot5 = n_tot5 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 7)));
                        //n_tot6 = n_tot6 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 8)));
                        //n_tot7 = n_tot7 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 9)));
                        //n_tot8 = n_tot8 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 10)));
                        //n_tot9 = n_tot9 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 11)));
                        //n_tot10 = n_tot10 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 12)));
                        //n_tot11 = n_tot11 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 13)));

                        n_tot1 = n_tot1 + (Convert.ToDouble(FgRes.GetData(n_row, 3)));
                        n_tot2 = n_tot2 + (Convert.ToDouble(FgRes.GetData(n_row, 4)));
                        n_tot3 = n_tot3 + (Convert.ToDouble(FgRes.GetData(n_row, 5)));
                        n_tot4 = n_tot4 + (Convert.ToDouble(FgRes.GetData(n_row, 6)));
                        n_tot5 = n_tot5 + (Convert.ToDouble(FgRes.GetData(n_row, 7)));
                        n_tot6 = n_tot6 + (Convert.ToDouble(FgRes.GetData(n_row, 8)));
                        n_tot7 = n_tot7 + (Convert.ToDouble(FgRes.GetData(n_row, 9)));
                        n_tot8 = n_tot8 + (Convert.ToDouble(FgRes.GetData(n_row, 10)));
                        n_tot9 = n_tot9 + (Convert.ToDouble(FgRes.GetData(n_row, 11)));
                        n_tot10 = n_tot10 + (Convert.ToDouble(FgRes.GetData(n_row, 12)));
                        n_tot11 = n_tot11 + (Convert.ToDouble(FgRes.GetData(n_row, 13)));
                    }
                    else
                    {
                        n_tot1 = n_tot1 + Convert.ToDouble(FgRes.GetData(n_row, 3));
                        n_tot2 = n_tot2 + Convert.ToDouble(FgRes.GetData(n_row, 4));
                        n_tot3 = n_tot3 + Convert.ToDouble(FgRes.GetData(n_row, 5));
                        n_tot4 = n_tot4 + Convert.ToDouble(FgRes.GetData(n_row, 6));
                        n_tot5 = n_tot5 + Convert.ToDouble(FgRes.GetData(n_row, 7));
                        n_tot6 = n_tot6 + Convert.ToDouble(FgRes.GetData(n_row, 8));
                        n_tot7 = n_tot7 + Convert.ToDouble(FgRes.GetData(n_row, 9));
                        n_tot8 = n_tot8 + Convert.ToDouble(FgRes.GetData(n_row, 10));
                        n_tot9 = n_tot9 + Convert.ToDouble(FgRes.GetData(n_row, 11));
                        n_tot10 = n_tot10 + Convert.ToDouble(FgRes.GetData(n_row, 12));
                        n_tot11 = n_tot11 + Convert.ToDouble(FgRes.GetData(n_row, 13));
                        
                    }
                }

                FgRes.Rows.Count = FgRes.Rows.Count + 1;
                FgRes.SetData(FgRes.Rows.Count - 1, 1, "TOTAL ==>");
                FgRes.SetData(FgRes.Rows.Count - 1, 3, n_tot1.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 4, n_tot2.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 5, n_tot3.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 6, n_tot4.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 7, n_tot5.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 8, n_tot6.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 9, n_tot7.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 10, n_tot8.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 11, n_tot9.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 12, n_tot10.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 13, n_tot11.ToString("0.00"));        
            }
            
            if (n_Libro == 32)
            {
                n_tot1 = funFlex.FlexSumarCol(FgDatos, 12, 3, FgDatos.Rows.Count - 1);
                n_tot2 = funFlex.FlexSumarCol(FgDatos, 13, 3, FgDatos.Rows.Count - 1);
                n_tot3 = funFlex.FlexSumarCol(FgDatos, 14, 3, FgDatos.Rows.Count - 1);
                n_tot4 = funFlex.FlexSumarCol(FgDatos, 15, 3, FgDatos.Rows.Count - 1);

                FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
                FgDatos.SetData(FgDatos.Rows.Count - 1, 9, "TOTAL ==>");
                FgDatos.SetData(FgDatos.Rows.Count - 1, 12, n_tot1.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 13, n_tot2.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 14, n_tot3.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 15, n_tot4.ToString("0.00"));
            }

            if (n_Libro == 14)
            {       
                for (n_row = 3; n_row <= FgDatos.Rows.Count - 1; n_row++)
                {
                    if (FgDatos.GetData(n_row, 4).ToString() == "07")
                    {
                        //n_tot1 = n_tot1 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 12)));
                        //n_tot2 = n_tot2 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 13)));
                        //n_tot3 = n_tot3 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 14)));
                        //n_tot4 = n_tot4 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 15)));
                        //n_tot5 = n_tot5 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 16)));
                        //n_tot6 = n_tot6 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 18)));
                        //n_tot7 = n_tot7 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 19)));
                        //n_tot8 = n_tot8 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 20)));

                        n_tot1 = n_tot1 + (Convert.ToDouble(FgDatos.GetData(n_row, 12)));
                        n_tot2 = n_tot2 + (Convert.ToDouble(FgDatos.GetData(n_row, 13)));
                        n_tot3 = n_tot3 + (Convert.ToDouble(FgDatos.GetData(n_row, 14)));
                        n_tot4 = n_tot4 + (Convert.ToDouble(FgDatos.GetData(n_row, 15)));
                        n_tot5 = n_tot5 + (Convert.ToDouble(FgDatos.GetData(n_row, 16)));
                        n_tot6 = n_tot6 + (Convert.ToDouble(FgDatos.GetData(n_row, 18)));
                        n_tot7 = n_tot7 + (Convert.ToDouble(FgDatos.GetData(n_row, 19)));
                        n_tot8 = n_tot8 + (Convert.ToDouble(FgDatos.GetData(n_row, 20)));
                    }
                    else
                    {
                        n_tot1 = n_tot1 + Convert.ToDouble(FgDatos.GetData(n_row, 12));
                        n_tot2 = n_tot2 + Convert.ToDouble(FgDatos.GetData(n_row, 13));
                        n_tot3 = n_tot3 + Convert.ToDouble(FgDatos.GetData(n_row, 14));
                        n_tot4 = n_tot4 + Convert.ToDouble(FgDatos.GetData(n_row, 15));
                        n_tot5 = n_tot5 + Convert.ToDouble(FgDatos.GetData(n_row, 16));
                        n_tot6 = n_tot6 + Convert.ToDouble(FgDatos.GetData(n_row, 18));
                        n_tot7 = n_tot7 + Convert.ToDouble(FgDatos.GetData(n_row, 19));
                        n_tot8 = n_tot8 + Convert.ToDouble(FgDatos.GetData(n_row, 20));
                    }
                }
                                
                FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
                FgDatos.SetData(FgDatos.Rows.Count - 1, 9, "TOTAL ==>");
                FgDatos.SetData(FgDatos.Rows.Count - 1, 12, n_tot1.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 13, n_tot2.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 14, n_tot3.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 15, n_tot4.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 16, n_tot5.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 18, n_tot6.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 19, n_tot7.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 20, n_tot8.ToString("0.00"));


                // *****************
                // TOTAL DEL RESUMEN
                n_tot1 = 0;
                n_tot2 = 0;
                n_tot3 = 0;
                n_tot4 = 0;
                n_tot5 = 0;
                n_tot6 = 0;
                n_tot7 = 0;
                n_tot8 = 0;

                for (n_row = 3; n_row <= FgRes.Rows.Count - 1; n_row++)
                {
                    if (FgRes.GetData(n_row, 2).ToString() == "NC")
                    {
                        //n_tot1 = n_tot1 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 3)));
                        //n_tot2 = n_tot2 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 4)));
                        //n_tot3 = n_tot3 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 5)));
                        //n_tot4 = n_tot4 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 6)));
                        //n_tot5 = n_tot5 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 7)));
                        //n_tot6 = n_tot6 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 8)));
                        //n_tot7 = n_tot7 + funFunciones.PostivoNegativo(Convert.ToDouble(FgRes.GetData(n_row, 9)));

                        n_tot1 = n_tot1 + (Convert.ToDouble(FgRes.GetData(n_row, 3)));
                        n_tot2 = n_tot2 + (Convert.ToDouble(FgRes.GetData(n_row, 4)));
                        n_tot3 = n_tot3 + (Convert.ToDouble(FgRes.GetData(n_row, 5)));
                        n_tot4 = n_tot4 + (Convert.ToDouble(FgRes.GetData(n_row, 6)));
                        n_tot5 = n_tot5 + (Convert.ToDouble(FgRes.GetData(n_row, 7)));
                        n_tot6 = n_tot6 + (Convert.ToDouble(FgRes.GetData(n_row, 8)));
                        n_tot7 = n_tot7 + (Convert.ToDouble(FgRes.GetData(n_row, 9)));
                       
                    }
                    else
                    {
                        n_tot1 = n_tot1 + Convert.ToDouble(FgRes.GetData(n_row, 3));
                        n_tot2 = n_tot2 + Convert.ToDouble(FgRes.GetData(n_row, 4));
                        n_tot3 = n_tot3 + Convert.ToDouble(FgRes.GetData(n_row, 5));
                        n_tot4 = n_tot4 + Convert.ToDouble(FgRes.GetData(n_row, 6));
                        n_tot5 = n_tot5 + Convert.ToDouble(FgRes.GetData(n_row, 7));
                        n_tot6 = n_tot6 + Convert.ToDouble(FgRes.GetData(n_row, 8));
                        n_tot7 = n_tot7 + Convert.ToDouble(FgRes.GetData(n_row, 9));
                        //n_tot8 = n_tot8 + Convert.ToDouble(FgRes.GetData(n_row, 10));
                    }
                }

                FgRes.Rows.Count = FgRes.Rows.Count + 1;
                FgRes.SetData(FgRes.Rows.Count - 1, 1, "TOTAL ==>");
                FgRes.SetData(FgRes.Rows.Count - 1, 3, n_tot1.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 4, n_tot2.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 5, n_tot3.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 6, n_tot4.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 7, n_tot5.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 8, n_tot6.ToString("0.00"));
                FgRes.SetData(FgRes.Rows.Count - 1, 9, n_tot7.ToString("0.00"));
                //FgRes.SetData(FgRes.Rows.Count - 1, 10, n_tot8.ToString("0.00"));
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
                b_result = objLE.LE_81(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMes.SelectedValue));
            }
            if (n_Libro == 14)
            {
                b_result = objLE.LE_141(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMes.SelectedValue));
            }

            if (b_result == true)
            {
                MessageBox.Show("¡ El libro electronico se genero con exito en la siguiente ruta :" + objLE.c_RutaSalida + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
    }
}
