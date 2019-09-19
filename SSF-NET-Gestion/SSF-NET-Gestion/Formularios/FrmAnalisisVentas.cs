using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Logistica;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Almacen;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Ventas;
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
using SIAC_Negocio.Gestion;

namespace SSF_NET_Gestion.Formularios
{
    public partial class FrmAnalisisVentas : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Libro;

        DataTable dtTipExi = new DataTable();
        DataTable dtProv = new DataTable();
        DataTable dtItem = new DataTable();
        DataTable dtLista = new DataTable();
        DataTable dttipexi = new DataTable();

        CN_mae_clipro objPro = new CN_mae_clipro();
        CN_alm_inventario objIte = new CN_alm_inventario();
        CN_sun_tipexi o_tipexi = new CN_sun_tipexi();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();
        //CN_mae_clipro objPro = new CN_mae_clipro();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_ges_planventas objCabecera = new CN_ges_planventas();                            // CABECERA DEL REGISTRO

        string[,] arrCabeceraFlex1 = new string[2, 5];
        string[,] arrCabeceraFlex2 = new string[2, 5];
        string[,] arrCabecera1 = new string[14, 5];
        string[,] arrCabecera1_1 = new string[6, 5];
        string[,] arrCabecera2 = new string[15, 5];
        string[,] arrCabecera3 = new string[17, 5];
        string[,] arrCabecera4 = new string[14, 5];

        string C_FORMATO1 = "#,##0.00";
        string C_FORMATO2 = "#,###";
        string C_FORMATONUMERICO = "#,##0.00";
        bool b_agregando = false;

        public FrmAnalisisVentas()
        {
            InitializeComponent();
        }
        private void FrmAnalisisVentas_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarDT();
        }
        void CargarDT()
        {
            objIte.mysConec = mysConec;
            dtItem = objIte.Listar(STU_SISTEMA.EMPRESAID, 1, 1);

            objPro.mysConec = mysConec;
            dtProv = objPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            o_tipexi.mysConec = mysConec;
            dttipexi = o_tipexi.Listar();

            funDatos.ComboBoxCargarDataTable(CboTipPro, dttipexi, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            DataTable dtResul = new DataTable();
            int n_row = 0;
            string c_dato = "";

            this.Height = 621;
            this.Width = 1095;
        
            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;

            this.Text = "GESTION - ANALISIS DE LAS VENTAS"; 

            OptTipFor1.Checked = true;

            if (OptTipFor1.Checked == true) { C_FORMATONUMERICO = C_FORMATO1; }
            if (OptTipFor2.Checked == true) { C_FORMATONUMERICO = C_FORMATO2; }

            arrCabeceraFlex1[0, 0] = "Provedor";
            arrCabeceraFlex1[0, 1] = "420";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Id";
            arrCabeceraFlex1[1, 1] = "0";
            arrCabeceraFlex1[1, 2] = "N";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";


            funFlex.FlexMostrarDatos(FgPro, arrCabeceraFlex1, dtLista, 1, false);

            arrCabeceraFlex2[0, 0] = "Item(s)";
            arrCabeceraFlex2[0, 1] = "420";
            arrCabeceraFlex2[0, 2] = "C";
            arrCabeceraFlex2[0, 3] = "";
            arrCabeceraFlex2[0, 4] = "c_tipexides";

            arrCabeceraFlex2[1, 0] = "Id";
            arrCabeceraFlex2[1, 1] = "0";
            arrCabeceraFlex2[1, 2] = "N";
            arrCabeceraFlex2[1, 3] = "";
            arrCabeceraFlex2[1, 4] = "c_itedes";

            funFlex.FlexMostrarDatos(FgItem, arrCabeceraFlex2, dtLista, 1, false);

            FgDatos.Cols.Count = 13;
            Cabecera1();
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, false);
            
            OptTip1.Checked = true;
            OptPer1.Checked = true;
            OptSel3.Checked = true;
            OptTipVal1.Checked = true;
            OptSelImp2.Checked = true;
            OptTipSel1.Checked = true;

            objCabecera.mysConec = mysConec;
            objCabecera.TraerDataAnos(STU_SISTEMA.EMPRESAID);                   // TRAE LOS AÑOS DE TRABAJO
            dtResul = objCabecera.dtDataAnos;
            FgHisAno.Rows.Count = 1;
            for (n_row = 0; n_row <= dtResul.Rows.Count - 1; n_row++)
            {
                FgHisAno.Rows.Count = FgHisAno.Rows.Count + 1;
                c_dato = dtResul.Rows[n_row]["n_anotra"].ToString();
                FgHisAno.SetData(FgHisAno.Rows.Count - 1, 1, c_dato);
                FgHisAno.SetData(FgHisAno.Rows.Count - 1, 2, true);
            }
        }
        void Cabecera1()
        {
            arrCabecera1[0, 0] = "Año";
            arrCabecera1[0, 1] = "100";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "n_anotra";

            arrCabecera1[1, 0] = "Enero";
            arrCabecera1[1, 1] = "80";
            arrCabecera1[1, 2] = "D";
            arrCabecera1[1, 3] = C_FORMATONUMERICO;
            arrCabecera1[1, 4] = "January";

            arrCabecera1[2, 0] = "Febrero";
            arrCabecera1[2, 1] = "80";
            arrCabecera1[2, 2] = "D";
            arrCabecera1[2, 3] = C_FORMATONUMERICO;
            arrCabecera1[2, 4] = "February";

            arrCabecera1[3, 0] = "Marzo";
            arrCabecera1[3, 1] = "80";
            arrCabecera1[3, 2] = "D";
            arrCabecera1[3, 3] = C_FORMATONUMERICO;
            arrCabecera1[3, 4] = "March";

            arrCabecera1[4, 0] = "Abril";
            arrCabecera1[4, 1] = "80";
            arrCabecera1[4, 2] = "D";
            arrCabecera1[4, 3] = C_FORMATONUMERICO;
            arrCabecera1[4, 4] = "April";

            arrCabecera1[5, 0] = "Mayo";
            arrCabecera1[5, 1] = "80";
            arrCabecera1[5, 2] = "D";
            arrCabecera1[5, 3] = C_FORMATONUMERICO;
            arrCabecera1[5, 4] = "May";

            arrCabecera1[6, 0] = "Junio";
            arrCabecera1[6, 1] = "80";
            arrCabecera1[6, 2] = "D";
            arrCabecera1[6, 3] = C_FORMATONUMERICO;
            arrCabecera1[6, 4] = "June";

            arrCabecera1[7, 0] = "Julio";
            arrCabecera1[7, 1] = "80";
            arrCabecera1[7, 2] = "D";
            arrCabecera1[7, 3] = C_FORMATONUMERICO;
            arrCabecera1[7, 4] = "July";

            arrCabecera1[8, 0] = "Agosto";
            arrCabecera1[8, 1] = "80";
            arrCabecera1[8, 2] = "D";
            arrCabecera1[8, 3] = C_FORMATONUMERICO;
            arrCabecera1[8, 4] = "August";

            arrCabecera1[9, 0] = "Setiembre";
            arrCabecera1[9, 1] = "80";
            arrCabecera1[9, 2] = "D";
            arrCabecera1[9, 3] = C_FORMATONUMERICO;
            arrCabecera1[9, 4] = "September";

            arrCabecera1[10, 0] = "Octubre";
            arrCabecera1[10, 1] = "80";
            arrCabecera1[10, 2] = "D";
            arrCabecera1[10, 3] = C_FORMATONUMERICO;
            arrCabecera1[10, 4] = "October";

            arrCabecera1[11, 0] = "Noviembre";
            arrCabecera1[11, 1] = "80";
            arrCabecera1[11, 2] = "D";
            arrCabecera1[11, 3] = C_FORMATONUMERICO;
            arrCabecera1[11, 4] = "November";

            arrCabecera1[12, 0] = "Diciembre";
            arrCabecera1[12, 1] = "80";
            arrCabecera1[12, 2] = "D";
            arrCabecera1[12, 3] = C_FORMATONUMERICO;
            arrCabecera1[12, 4] = "December";
            
            arrCabecera1[13, 0] = "Total";
            arrCabecera1[13, 1] = "80";
            arrCabecera1[13, 2] = "D";
            arrCabecera1[13, 3] = C_FORMATONUMERICO;
            arrCabecera1[13, 4] = "n_total";
        }
        void Cabecera1_1()
        {
            arrCabecera1_1[0, 0] = "Año";
            arrCabecera1_1[0, 1] = "100";
            arrCabecera1_1[0, 2] = "C";
            arrCabecera1_1[0, 3] = "";
            arrCabecera1_1[0, 4] = "c_numdoc";

            arrCabecera1_1[1, 0] = "Enero - Marzo";
            arrCabecera1_1[1, 1] = "100";
            arrCabecera1_1[1, 2] = "D";
            arrCabecera1_1[1, 3] = "0.00";
            arrCabecera1_1[1, 4] = "n_ene";

            arrCabecera1_1[2, 0] = "Abril - Junio";
            arrCabecera1_1[2, 1] = "100";
            arrCabecera1_1[2, 2] = "D";
            arrCabecera1_1[2, 3] = "0.00";
            arrCabecera1_1[2, 4] = "n_feb";

            arrCabecera1_1[3, 0] = "Julio - Setiembre";
            arrCabecera1_1[3, 1] = "100";
            arrCabecera1_1[3, 2] = "D";
            arrCabecera1_1[3, 3] = "0.00";
            arrCabecera1_1[3, 4] = "n_mar";

            arrCabecera1_1[4, 0] = "Octubre - Diciembre";
            arrCabecera1_1[4, 1] = "100";
            arrCabecera1_1[4, 2] = "D";
            arrCabecera1_1[4, 3] = "0.00";
            arrCabecera1_1[4, 4] = "n_abr";

            arrCabecera1_1[5, 0] = "Total";
            arrCabecera1_1[5, 1] = "80";
            arrCabecera1_1[5, 2] = "D";
            arrCabecera1_1[5, 3] = "0.00";
            arrCabecera1_1[5, 4] = "n_tot";
        }
        void Cabecera2()
        {
            arrCabecera2[0, 0] = "Nº R.U.C.";
            arrCabecera2[0, 1] = "110";
            arrCabecera2[0, 2] = "C";
            arrCabecera2[0, 3] = "";
            arrCabecera2[0, 4] = "c_numdoc";

            arrCabecera2[1, 0] = "Cliente";
            arrCabecera2[1, 1] = "300";
            arrCabecera2[1, 2] = "C";
            arrCabecera2[1, 3] = "";
            arrCabecera2[1, 4] = "c_nombre";

            arrCabecera2[2, 0] = "Enero";
            arrCabecera2[2, 1] = "80";
            arrCabecera2[2, 2] = "D";
            arrCabecera2[2, 3] = C_FORMATONUMERICO;
            arrCabecera2[2, 4] = "January";

            arrCabecera2[3, 0] = "Febrero";
            arrCabecera2[3, 1] = "80";
            arrCabecera2[3, 2] = "D";
            arrCabecera2[3, 3] = C_FORMATONUMERICO;
            arrCabecera2[3, 4] = "February";

            arrCabecera2[4, 0] = "Marzo";
            arrCabecera2[4, 1] = "80";
            arrCabecera2[4, 2] = "D";
            arrCabecera2[4, 3] = C_FORMATONUMERICO;
            arrCabecera2[4, 4] = "March";

            arrCabecera2[5, 0] = "Abril";
            arrCabecera2[5, 1] = "80";
            arrCabecera2[5, 2] = "D";
            arrCabecera2[5, 3] = C_FORMATONUMERICO;
            arrCabecera2[5, 4] = "April";

            arrCabecera2[6, 0] = "Mayo";
            arrCabecera2[6, 1] = "80";
            arrCabecera2[6, 2] = "D";
            arrCabecera2[6, 3] = C_FORMATONUMERICO;
            arrCabecera2[6, 4] = "May";

            arrCabecera2[7, 0] = "Junio";
            arrCabecera2[7, 1] = "80";
            arrCabecera2[7, 2] = "D";
            arrCabecera2[7, 3] = C_FORMATONUMERICO;
            arrCabecera2[7, 4] = "June";

            arrCabecera2[8, 0] = "Julio";
            arrCabecera2[8, 1] = "80";
            arrCabecera2[8, 2] = "D";
            arrCabecera2[8, 3] = C_FORMATONUMERICO;
            arrCabecera2[8, 4] = "July";

            arrCabecera2[9, 0] = "Agosto";
            arrCabecera2[9, 1] = "80";
            arrCabecera2[9, 2] = "D";
            arrCabecera2[9, 3] = C_FORMATONUMERICO;
            arrCabecera2[9, 4] = "August";

            arrCabecera2[10, 0] = "Setiembre";
            arrCabecera2[10, 1] = "80";
            arrCabecera2[10, 2] = "D";
            arrCabecera2[10, 3] = C_FORMATONUMERICO;
            arrCabecera2[10, 4] = "September";

            arrCabecera2[11, 0] = "Octubre";
            arrCabecera2[11, 1] = "80";
            arrCabecera2[11, 2] = "D";
            arrCabecera2[11, 3] = C_FORMATONUMERICO;
            arrCabecera2[11, 4] = "October";

            arrCabecera2[12, 0] = "Noviembre";
            arrCabecera2[12, 1] = "80";
            arrCabecera2[12, 2] = "D";
            arrCabecera2[12, 3] = C_FORMATONUMERICO;
            arrCabecera2[12, 4] = "November";

            arrCabecera2[13, 0] = "Diciembre";
            arrCabecera2[13, 1] = "80";
            arrCabecera2[13, 2] = "D";
            arrCabecera2[13, 3] = C_FORMATONUMERICO;
            arrCabecera2[13, 4] = "December";

            arrCabecera2[14, 0] = "Total";
            arrCabecera2[14, 1] = "80";
            arrCabecera2[14, 2] = "D";
            arrCabecera2[14, 3] = C_FORMATONUMERICO;
            arrCabecera2[14, 4] = "n_total";
        }
        void Cabecera3()
        {
            arrCabecera3[0, 0] = "Familia";
            arrCabecera3[0, 1] = "120";
            arrCabecera3[0, 2] = "C";
            arrCabecera3[0, 3] = "";
            arrCabecera3[0, 4] = "c_familia";

            arrCabecera3[1, 0] = "Cod. Producto";
            arrCabecera3[1, 1] = "100";
            arrCabecera3[1, 2] = "C";
            arrCabecera3[1, 3] = "";
            arrCabecera3[1, 4] = "c_codpro";

            arrCabecera3[2, 0] = "Producto";
            arrCabecera3[2, 1] = "300";
            arrCabecera3[2, 2] = "C";
            arrCabecera3[2, 3] = "";
            arrCabecera3[2, 4] = "c_despro";

            arrCabecera3[3, 0] = "Uni. Med";
            arrCabecera3[3, 1] = "40";
            arrCabecera3[3, 2] = "C";
            arrCabecera3[3, 3] = "";
            arrCabecera3[3, 4] = "c_unimed";

            arrCabecera3[4, 0] = "Enero";
            arrCabecera3[4, 1] = "80";
            arrCabecera3[4, 2] = "D";
            arrCabecera3[4, 3] = C_FORMATONUMERICO;
            arrCabecera3[4, 4] = "January";

            arrCabecera3[5, 0] = "Febrero";
            arrCabecera3[5, 1] = "80";
            arrCabecera3[5, 2] = "D";
            arrCabecera3[5, 3] = C_FORMATONUMERICO;
            arrCabecera3[5, 4] = "February";

            arrCabecera3[6, 0] = "Marzo";
            arrCabecera3[6, 1] = "80";
            arrCabecera3[6, 2] = "D";
            arrCabecera3[6, 3] = C_FORMATONUMERICO;
            arrCabecera3[6, 4] = "March";

            arrCabecera3[7, 0] = "Abril";
            arrCabecera3[7, 1] = "80";
            arrCabecera3[7, 2] = "D";
            arrCabecera3[7, 3] = C_FORMATONUMERICO;
            arrCabecera3[7, 4] = "April";

            arrCabecera3[8, 0] = "Mayo";
            arrCabecera3[8, 1] = "80";
            arrCabecera3[8, 2] = "D";
            arrCabecera3[8, 3] = C_FORMATONUMERICO;
            arrCabecera3[8, 4] = "May";

            arrCabecera3[9, 0] = "Junio";
            arrCabecera3[9, 1] = "80";
            arrCabecera3[9, 2] = "D";
            arrCabecera3[9, 3] = C_FORMATONUMERICO;
            arrCabecera3[9, 4] = "June";

            arrCabecera3[10, 0] = "Julio";
            arrCabecera3[10, 1] = "80";
            arrCabecera3[10, 2] = "D";
            arrCabecera3[10, 3] = C_FORMATONUMERICO;
            arrCabecera3[10, 4] = "July";

            arrCabecera3[11, 0] = "Agosto";
            arrCabecera3[11, 1] = "80";
            arrCabecera3[11, 2] = "D";
            arrCabecera3[11, 3] = C_FORMATONUMERICO;
            arrCabecera3[11, 4] = "August";

            arrCabecera3[12, 0] = "Setiembre";
            arrCabecera3[12, 1] = "80";
            arrCabecera3[12, 2] = "D";
            arrCabecera3[12, 3] = C_FORMATONUMERICO;
            arrCabecera3[12, 4] = "September";

            arrCabecera3[13, 0] = "Octubre";
            arrCabecera3[13, 1] = "80";
            arrCabecera3[13, 2] = "D";
            arrCabecera3[13, 3] = C_FORMATONUMERICO;
            arrCabecera3[13, 4] = "October";

            arrCabecera3[14, 0] = "Noviembre";
            arrCabecera3[14, 1] = "80";
            arrCabecera3[14, 2] = "D";
            arrCabecera3[14, 3] = C_FORMATONUMERICO;
            arrCabecera3[14, 4] = "November";

            arrCabecera3[15, 0] = "Diciembre";
            arrCabecera3[15, 1] = "80";
            arrCabecera3[15, 2] = "D";
            arrCabecera3[15, 3] = C_FORMATONUMERICO;
            arrCabecera3[15, 4] = "December";

            arrCabecera3[16, 0] = "Total";
            arrCabecera3[16, 1] = "80";
            arrCabecera3[16, 2] = "D";
            arrCabecera3[16, 3] = C_FORMATONUMERICO;
            arrCabecera3[16, 4] = "n_total";
        }
        void Cabecera4()
        {
            arrCabecera4[0, 0] = "Tipo Documento";
            arrCabecera4[0, 1] = "200";
            arrCabecera4[0, 2] = "C";
            arrCabecera4[0, 3] = "";
            arrCabecera4[0, 4] = "c_documento";

            arrCabecera4[1, 0] = "Enero";
            arrCabecera4[1, 1] = "80";
            arrCabecera4[1, 2] = "D";
            arrCabecera4[1, 3] = C_FORMATONUMERICO;
            arrCabecera4[1, 4] = "January";

            arrCabecera4[2, 0] = "Febrero";
            arrCabecera4[2, 1] = "80";
            arrCabecera4[2, 2] = "D";
            arrCabecera4[2, 3] = C_FORMATONUMERICO;
            arrCabecera4[2, 4] = "February";

            arrCabecera4[3, 0] = "Marzo";
            arrCabecera4[3, 1] = "80";
            arrCabecera4[3, 2] = "D";
            arrCabecera4[3, 3] = C_FORMATONUMERICO;
            arrCabecera4[3, 4] = "March";

            arrCabecera4[4, 0] = "Abril";
            arrCabecera4[4, 1] = "80";
            arrCabecera4[4, 2] = "D";
            arrCabecera4[4, 3] = C_FORMATONUMERICO;
            arrCabecera4[4, 4] = "April";

            arrCabecera4[5, 0] = "Mayo";
            arrCabecera4[5, 1] = "80";
            arrCabecera4[5, 2] = "D";
            arrCabecera4[5, 3] = C_FORMATONUMERICO;
            arrCabecera4[5, 4] = "May";

            arrCabecera4[6, 0] = "Junio";
            arrCabecera4[6, 1] = "80";
            arrCabecera4[6, 2] = "D";
            arrCabecera4[6, 3] = C_FORMATONUMERICO;
            arrCabecera4[6, 4] = "June";

            arrCabecera4[7, 0] = "Julio";
            arrCabecera4[7, 1] = "80";
            arrCabecera4[7, 2] = "D";
            arrCabecera4[7, 3] = C_FORMATONUMERICO;
            arrCabecera4[7, 4] = "July";

            arrCabecera4[8, 0] = "Agosto";
            arrCabecera4[8, 1] = "80";
            arrCabecera4[8, 2] = "D";
            arrCabecera4[8, 3] = C_FORMATONUMERICO;
            arrCabecera4[8, 4] = "August";

            arrCabecera4[9, 0] = "Setiembre";
            arrCabecera4[9, 1] = "80";
            arrCabecera4[9, 2] = "D";
            arrCabecera4[9, 3] = C_FORMATONUMERICO;
            arrCabecera4[9, 4] = "September";

            arrCabecera4[10, 0] = "Octubre";
            arrCabecera4[10, 1] = "80";
            arrCabecera4[10, 2] = "D";
            arrCabecera4[10, 3] = C_FORMATONUMERICO;
            arrCabecera4[10, 4] = "October";

            arrCabecera4[11, 0] = "Noviembre";
            arrCabecera4[11, 1] = "80";
            arrCabecera4[11, 2] = "D";
            arrCabecera4[11, 3] = C_FORMATONUMERICO;
            arrCabecera4[11, 4] = "November";

            arrCabecera4[12, 0] = "Diciembre";
            arrCabecera4[12, 1] = "80";
            arrCabecera4[12, 2] = "D";
            arrCabecera4[12, 3] = C_FORMATONUMERICO;
            arrCabecera4[12, 4] = "December";

            arrCabecera4[13, 0] = "Total";
            arrCabecera4[13, 1] = "80";
            arrCabecera4[13, 2] = "D";
            arrCabecera4[13, 3] = C_FORMATONUMERICO;
            arrCabecera4[13, 4] = "n_total";
        }
        private void FrmAnalisisVentas_Resize(object sender, EventArgs e)
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
            int n_tipcon = 0;
            int n_tipmon = 0;
            int n_tipimp = 0;
            int n_tipval = 0;
            // INDICAMOS LE TIPO DE CONSULTA
            if (OptTip1.Checked == true) { n_tipcon = 1; }
            if (OptTip2.Checked == true) { n_tipcon = 2; }
            if (OptTip3.Checked == true) { n_tipcon = 3; }
            if (OptTip4.Checked == true) { n_tipcon = 4; }

            // INDICAMOS EL TIPO DE MONEDA
            if (OptSel1.Checked == true) { n_tipmon = 1; }    // SOLO MONEDA NACIONAL    
            if (OptSel2.Checked == true) { n_tipmon = 2; }    // SOLO MONEDA EXTRANJERA
            if (OptSel3.Checked == true) { n_tipmon = 3; }    // TODO EN MONEDA NACIONAL
            if (OptSel4.Checked == true) { n_tipmon = 4; }    // TODO EN MONEDA EXTRANJERA

            // INDICAMOS EL TIPO DE IMPORTE A MOSTRAR
            if (OptSelImp1.Checked == true) { n_tipimp = 1; }   // IMPORTE INCLUIDO IGV
            if (OptSelImp2.Checked == true) { n_tipimp = 2; }   // IMPORTE SIN IGV
            if (OptSelImp3.Checked == true) { n_tipimp = 3; }   // SOLO EL IGV
            
            CN_vta_ventas funCom = new CN_vta_ventas();
            funCom.mysConec = mysConec;
            funCom.STU_SISTEMA = STU_SISTEMA;
  
            string c_CadINCli = funFlex.Flex_CadenaIN(FgPro, 2, 1);
            string c_CadINIte = funFlex.Flex_CadenaIN(FgItem, 2, 1); ;

            if (n_tipcon == 1)
            {
                Cabecera1();
                funCom.Consulta14(STU_SISTEMA.EMPRESAID, n_tipmon, n_tipimp);
                dtLista = funCom.dtLista1;
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, true);
                SumarColumnas();
            }
            if (n_tipcon == 2)
            {
                int n_row = 0;
                int n_anover = 0;
                for (n_row = 1; n_row <= FgHisAno.Rows.Count - 1; n_row++)
                {
                    if (FgHisAno.GetData(n_row, 2).ToString() == "True")
                    {
                        n_anover = Convert.ToInt16(FgHisAno.GetData(n_row, 1));
                    }
                }
                Cabecera2();
                funCom.Consulta17(STU_SISTEMA.EMPRESAID, n_anover, n_tipmon, n_tipimp);
                dtLista = funCom.dtLista1;
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera2, dtLista, 2, true);
                SumarColumnas2();
            }
            if (n_tipcon == 3)
            {
                int n_row = 0;
                int n_anover = 0;
                for (n_row = 1; n_row <= FgHisAno.Rows.Count - 1; n_row++)
                {
                    if (FgHisAno.GetData(n_row, 2).ToString() == "True")
                    {
                        n_anover = Convert.ToInt16(FgHisAno.GetData(n_row, 1));
                    }
                }
                Cabecera3();
                if (OptTipVal1.Checked == true) { n_tipval = 1; }
                if (OptTipVal2.Checked == true) { n_tipval = 2; }
                if (OptTipVal3.Checked == true) { n_tipval = 3; }

                if (n_tipval == 3)
                {
                    funCom.Consulta23(STU_SISTEMA.EMPRESAID, n_anover, n_tipmon, n_tipval);
                }
                else
                { 
                    funCom.Consulta18(STU_SISTEMA.EMPRESAID, n_anover, n_tipmon, n_tipval);
                }
                dtLista = funCom.dtLista1;
                
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera3, dtLista, 2, true);
                if (OptTipVal3.Checked != true)
                {
                    SumarColumnas3();
                }
                else
                {
                    FgDatos.Cols.Remove(FgDatos.Cols.Count - 1);
                }
            }
            if (n_tipcon == 4)
            {
                int n_row = 0;
                int n_anover = 0;
                for (n_row = 1; n_row <= FgHisAno.Rows.Count - 1; n_row++)
                {
                    if (FgHisAno.GetData(n_row, 2).ToString() == "True")
                    {
                        n_anover = Convert.ToInt16(FgHisAno.GetData(n_row, 1));
                    }
                }
                Cabecera4();
                funCom.Consulta19(STU_SISTEMA.EMPRESAID, n_anover, n_tipmon, n_tipimp);
                dtLista = funCom.dtLista1;
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera4, dtLista, 2, true);
                SumarColumnas();
            }
        }
        void SumarColumnas()
        {
            int n_col = 3;
            FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
            FgDatos.SetData(FgDatos.Rows.Count - 1, 1, "T O T A L E S  ==>");
            for (n_col = 2; n_col <= FgDatos.Cols.Count - 1; n_col++)
            {
                double n_valor = funFlex.FlexSumarCol(FgDatos, n_col, 2, FgDatos.Rows.Count - 2);
                FgDatos.SetData(FgDatos.Rows.Count - 1, n_col, n_valor.ToString("0.00"));
            }
            funFlex.Flex_PintarCeldas(FgDatos, FgDatos.Rows.Count - 1, 1, FgDatos.Rows.Count - 1, FgDatos.Cols.Count - 1, Color.Black,Color.Coral);
            funFlex.Flex_PintarCeldas(FgDatos, 2, FgDatos.Cols.Count - 1, FgDatos.Rows.Count - 2, FgDatos.Cols.Count - 1, Color.Black, Color.DarkSeaGreen);
        }
        void SumarColumnas2()
        {
            int n_col = 3;
            FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
            FgDatos.SetData(FgDatos.Rows.Count - 1, 1, "T O T A L E S  ==>");
            for (n_col = 3; n_col <= FgDatos.Cols.Count - 1; n_col++)
            {
                double n_valor = funFlex.FlexSumarCol(FgDatos, n_col, 2, FgDatos.Rows.Count - 2);
                FgDatos.SetData(FgDatos.Rows.Count - 1, n_col, n_valor.ToString("0.00"));
            }
            funFlex.Flex_PintarCeldas(FgDatos, FgDatos.Rows.Count - 1, 1, FgDatos.Rows.Count - 1, FgDatos.Cols.Count - 1, Color.Black, Color.Coral);
            funFlex.Flex_PintarCeldas(FgDatos, 2, FgDatos.Cols.Count - 1, FgDatos.Rows.Count - 2, FgDatos.Cols.Count - 1, Color.Black, Color.DarkSeaGreen);
        }
        void SumarColumnas3()
        {
            int n_col = 5;
            FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
            FgDatos.SetData(FgDatos.Rows.Count - 1, 3, "T O T A L E S  ==>");
            for (n_col = 5; n_col <= FgDatos.Cols.Count - 1; n_col++)
            {
                double n_valor = funFlex.FlexSumarCol(FgDatos, n_col, 2, FgDatos.Rows.Count - 2);
                FgDatos.SetData(FgDatos.Rows.Count - 1, n_col, n_valor.ToString("0.00"));
            }
            funFlex.Flex_PintarCeldas(FgDatos, FgDatos.Rows.Count - 1, 1, FgDatos.Rows.Count - 1, FgDatos.Cols.Count - 1, Color.Black, Color.Coral);
            funFlex.Flex_PintarCeldas(FgDatos, 2, FgDatos.Cols.Count - 1, FgDatos.Rows.Count - 2, FgDatos.Cols.Count - 1, Color.Black, Color.DarkSeaGreen);
        }
        void SetearCabecera1(string c_dato)
        {
            funFlex.Flex_UniColumnas(FgDatos, 0, 2, 13, c_dato, 1);
        }
        void SetearCabecera1_1(string c_dato)
        {
            funFlex.Flex_UniColumnas(FgDatos, 0, 2, 5, c_dato, 1);
        }
        void SetearCabecera2()
        {

            funFlex.Flex_UniColumnas(FgDatos, 0, 12, 13, "MONEDA NACIONAL", 1);
            funFlex.Flex_UniColumnas(FgDatos, 0, 14, 15, "MONEDA EXTRANJERA", 1);
            funFlex.Flex_UniColumnas(FgDatos, 0, 16, 18, "EXPRESADO EN MN", 1);
        }
        void SetearCabecera3()
        {
        }
        private void CmdAddPro_Click(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(FgPro.GetData(FgPro.Rows.Count - 1, 1)) == "")
            {
                MessageBox.Show("¡ No ha especificado un proveedor en la ultima fila !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgPro.Focus();
                return;
            }
            FgPro.Rows.Count = FgPro.Rows.Count + 1;
        }
        private void CmdAddIte_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(CboTipPro.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de existencia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                c1DockingTab1.SelectedIndex = 0;
                CboTipPro.Focus();
                return;
            }
            if (funFunciones.NulosC(FgItem.GetData(FgItem.Rows.Count - 1, 1)) == "")
            {
                MessageBox.Show("¡ No ha especificado un item en la ultima fila !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgItem.Focus();
                return;
            }
            FgItem.Rows.Count = FgItem.Rows.Count + 1;
        }
        private void CmdDelIte_Click(object sender, EventArgs e)
        {
            if (FgItem.Rows.Count == 1) { return; }
            FgItem.RemoveItem(FgItem.Row);
        }
        private void CmdDelPro_Click(object sender, EventArgs e)
        {
            if (FgPro.Rows.Count == 1) { return; }
            FgPro.RemoveItem(FgPro.Row);
        }
        private void FgPro_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgPro.Col == 1)
            {
                DataTable dtResult = new DataTable();
                string c_dato = "";
                objPro.mysConec = mysConec;
                dtResult = objPro.BuscarCliPro(dtProv, 1, "n_id", "");
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count != 0)
                    {
                        c_dato = dtResult.Rows[0]["c_nombre"].ToString();
                        FgPro.SetData(FgPro.Row, 1, c_dato);

                        c_dato = dtResult.Rows[0]["n_id"].ToString();
                        FgPro.SetData(FgPro.Row, 2, c_dato);

                        FgPro.Rows.Count = FgPro.Rows.Count + 1;
                    }
                }
            }
        }
        private void FgItem_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgItem.Col == 1)
            {
                if (Convert.ToInt16(CboTipPro.SelectedValue) == 0)
                {
                    MessageBox.Show("¡ No ha especificado el tipo de existencia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    c1DockingTab1.SelectedIndex = 0;
                    CboTipPro.Focus();
                    return;
                }

                DataTable dtResul = new DataTable();
                string c_dato = "";
                int n_idtippro = Convert.ToInt16(CboTipPro.SelectedValue);
                if (n_idtippro == 0)
                {
                    dtResul = objItems.BuscarItem("", "n_id", dtItem, n_idtippro);
                }
                else
                {
                    dtResul = objItems.BuscarItem("", "n_id", dtItem, n_idtippro);
                }

                if (dtResul != null)
                {
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["c_despro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                        FgItem.SetData(FgItem.Row, 1, c_dato);

                        c_dato = dtResul.Rows[0]["n_id"].ToString();            // MOSTRAMOS EL ID DEL ITEM
                        FgItem.SetData(FgItem.Row, 2, c_dato);

                        FgItem.Rows.Count = FgItem.Rows.Count + 1;
                    }
                }
            }
        }
        private void OptTip3_CheckedChanged(object sender, EventArgs e)
        {
            if (OptTip3.Checked == false) { return; }

            OptTipVal2.Enabled = true;
            OptTipVal3.Enabled = true;
            FgDatos.Cols.Count = 15;
            Cabecera3();
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera3, dtLista, 2, false);
            int n_row = 0;
            b_agregando = true;
            for (n_row = 1; n_row <= FgHisAno.Rows.Count - 1; n_row++)
            {
                FgHisAno.SetData(n_row, 2, "false");
            }
            FgHisAno.SetData(FgHisAno.Rows.Count - 1, 2, "true");
            b_agregando = false;
            if (OptTip3.Checked == true)
            {
                CmdAddPro.Enabled = true;
                CmdDelPro.Enabled = true;
                FgPro.Cols[1].ComboList = "...";
            }
            else
            {
                CmdAddPro.Enabled = false;
                CmdDelPro.Enabled = false;
                FgPro.Cols[1].ComboList = "";
            }
        }
        private void OptTip2_CheckedChanged(object sender, EventArgs e)
        {
            if (OptTip2.Checked ==false) { return; }
            OptTipVal1.Checked = true;
            OptTipVal2.Enabled = false;
            OptTipVal3.Enabled = false;
            FgDatos.Cols.Count = 15;
            Cabecera2();
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera2, dtLista, 2, false);
            int n_row = 0;
            b_agregando = true;
            for (n_row = 1; n_row <= FgHisAno.Rows.Count - 1; n_row++)
            {
                FgHisAno.SetData(n_row, 2, "false");
            }
            FgHisAno.SetData(FgHisAno.Rows.Count - 1, 2, "true");
            b_agregando = false;
            if (OptTip2.Checked == true)
            {
                CmdAddPro.Enabled = true;
                CmdDelPro.Enabled = true;
                FgPro.Cols[1].ComboList = "...";
            }
            else
            {
                CmdAddPro.Enabled = false;
                CmdDelPro.Enabled = false;
                FgPro.Cols[1].ComboList = "";
            }
        }
        private void OptPer1_CheckedChanged(object sender, EventArgs e)
        {
            if (OptPer1.Checked == true)
            {
                Cabecera1();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, false);
                SetearCabecera1("M  E   N  S  U  A  L");
            }
        }
        private void OptPer2_CheckedChanged(object sender, EventArgs e)
        {
            if (OptPer2.Checked == true)
            {
                Cabecera1_1();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera1_1, dtLista, 2, false);
                SetearCabecera1_1("T  R  I  M  E  S  T  R  A  L");
            }
        }

        private void OptTip1_CheckedChanged(object sender, EventArgs e)
        {
            if (OptTip1.Checked == false) { return; }
            OptTipVal1.Checked = true;
            OptTipVal2.Enabled = false;
            OptTipVal3.Enabled = false;
            FgDatos.Cols.Count = 13;
            Cabecera1();
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, false);
        }
        private void OptTipFor1_CheckedChanged(object sender, EventArgs e)
        {
            if (OptTipFor1.Checked==true)
            {
                C_FORMATONUMERICO = C_FORMATO1;
            }
        }
        private void OptTipFor2_CheckedChanged(object sender, EventArgs e)
        {
            if (OptTipFor2.Checked == true)
            {
                C_FORMATONUMERICO = C_FORMATO2;
            }
        }
        private void FgHisAno_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (b_agregando == true) { return; }
            if ((OptTip2.Checked == true) || (OptTip3.Checked == true) || (OptTip4.Checked == true))
            {
                int n_row=0;
                for (n_row = 1; n_row<= FgHisAno.Rows.Count-1;n_row++)
                {
                    if (n_row != FgHisAno.Row)
                    {
                        FgHisAno.SetData(n_row, 2, "false");
                    }
                    if (n_row == FgHisAno.Row)
                    {
                        FgHisAno.SetData(n_row, 2, "true");
                    }
                }
            }
        }
        private void OptTip4_CheckedChanged(object sender, EventArgs e)
        {
            if (OptTip4.Checked == false) { return; }
            OptTipVal1.Checked = true;
            OptTipVal2.Enabled = false;
            OptTipVal3.Enabled = false;
            FgDatos.Cols.Count = 15;
            Cabecera4();
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera4, dtLista, 2, false);
            int n_row = 0;
            b_agregando = true;
            for (n_row = 1; n_row <= FgHisAno.Rows.Count - 1; n_row++)
            {
                FgHisAno.SetData(n_row, 2, "false");
            }
            FgHisAno.SetData(FgHisAno.Rows.Count - 1, 2, "true");
            b_agregando = false;
            if (OptTip4.Checked == true)
            {
                CmdAddPro.Enabled = true;
                CmdDelPro.Enabled = true;
                FgPro.Cols[1].ComboList = "...";
            }
            else
            {
                CmdAddPro.Enabled = false;
                CmdDelPro.Enabled = false;
                FgPro.Cols[1].ComboList = "";
            }
        }
        private void OptTipVal1_CheckedChanged(object sender, EventArgs e)
        {
            if (OptTipVal1.Checked == false) { return; }
            groupBox9.Enabled = true;
        }
        private void OptTipVal2_CheckedChanged(object sender, EventArgs e)
        {
            if (OptTipVal2.Checked == false) { return; }
            groupBox9.Enabled = false;
        }
        private void OptTipVal3_CheckedChanged(object sender, EventArgs e)
        {
            if (OptTipVal3.Checked == false) { return; }
            groupBox9.Enabled = false;
        }
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            funFlex.ExportToExcel_NumFilaCabecera = 2;
            string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + "-VENTA_ANUAL-" + DateTime.Now.ToString("dd-mm-yyyy") + ".xls";
            funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "RESUMEN VENTAS DEL AÑO X PRODUCTOS", "TODO EL AÑO", c_nomarch);
        }
    }
}
