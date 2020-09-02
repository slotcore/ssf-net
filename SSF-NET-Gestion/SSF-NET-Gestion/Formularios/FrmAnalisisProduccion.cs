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
using SIAC_Negocio.Produccion;
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

namespace SSF_NET_Gestion.Formularios
{
    public partial class FrmAnalisisProduccion : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Libro;

        DataTable dtTipExi = new DataTable();
        DataTable dtProv = new DataTable();
        DataTable dtItem = new DataTable();
        DataTable dtInsumo = new DataTable();
        DataTable dtLista = new DataTable();
        DataTable dttipexi = new DataTable();
        DataTable dtanos = new DataTable();
        DataTable dtmes = new DataTable();

        CN_pro_produccion o_produccion = new CN_pro_produccion();
        CN_mae_clipro objPro = new CN_mae_clipro();
        CN_alm_inventario objIte = new CN_alm_inventario();
        CN_sun_tipexi o_tipexi = new CN_sun_tipexi();
        CN_mae_meses o_mes = new CN_mae_meses();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();
        //CN_mae_clipro objPro = new CN_mae_clipro();
        CN_alm_inventario objItems = new CN_alm_inventario();

        string C_FORMATO1 = "#,##0.00";
        string C_FORMATO2 = "#,###";
        string C_FORMATONUMERICO = "#,##0.00";

        string[,] arrCabeceraFlex1 = new string[2, 5];
        string[,] arrCabeceraFlex2 = new string[2, 5];
        string[,] arrCabeceraFlex3 = new string[2, 5];
        string[,] arrCabeceraFlex4 = new string[3, 5];

        string[,] arrCabecera1 = new string[16, 5];
        string[,] arrCabecera1_1 = new string[6, 5];
        string[,] arrCabecera2 = new string[18, 5];
        string[,] arrCabecera3 = new string[8, 5];

        public FrmAnalisisProduccion()
        {
            InitializeComponent();
        }

        private void FrmAnalisisProduccion_Load(object sender, EventArgs e)
        {
            CargarDT();
            ConfigurarFormulario();
            SetearForm();
        }
        void SetearForm()
        {
            int n_row = 0;
            OptTip1.Checked = true;
            OptRes1.Checked = true;
            OptPer1.Checked = true;
            OptTipVal2.Checked = true;
            OptTipPre1.Checked = true;

            //SELECCIONAMOS EL AÑO DE TRABAJO ACTUAL
            for (n_row = 0; n_row <= FgAno.Rows.Count - 1; n_row++)
            {
                if (FgAno.GetData(n_row, 1).ToString() == STU_SISTEMA.ANOTRABAJO.ToString())
                {
                    FgAno.SetData(n_row, 2, true);
                }
            }
            //SELECCIONAMOS EL AÑO DE TRABAJO ACTUAL
            for (n_row = 0; n_row <= FgMes.Rows.Count - 1; n_row++)
            {
                if (Convert.ToInt32(FgMes.GetData(n_row, 3).ToString()) <= Convert.ToInt32(STU_SISTEMA.MESTRABAJO))
                {
                    FgMes.SetData(n_row, 2, true);
                }
            }
        }
        void CargarDT()
        {
            o_produccion.mysConec = mysConec;
            o_produccion.ConsultarAnos(STU_SISTEMA.EMPRESAID);
            dtanos = o_produccion.dtListar;

            objIte.mysConec = mysConec;
            dtItem = objIte.Listar(STU_SISTEMA.EMPRESAID, 1, 1);
            dtInsumo = objIte.Listar(STU_SISTEMA.EMPRESAID, 1, 1);
            dtItem = funDatos.DataTableFiltrar(dtItem, "n_idtipexi = 2");
            dtInsumo = funDatos.DataTableFiltrar(dtInsumo, "n_idtipexi IN (3, 4, 5, 6)");

            objPro.mysConec = mysConec;
            dtProv = objPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            o_tipexi.mysConec = mysConec;
            dttipexi = o_tipexi.Listar();

            o_mes.mysConec = mysConec;
            dtmes = o_mes.Listar();
            dtmes = funDatos.DataTableFiltrar(dtmes, "n_id IN (1,2,3,4,5,6,7,8,9,10,11,12)");
        }
        void ConfigurarFormulario()
        {
            this.Height = 600;
            this.Width = 1010;

            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;

            this.Text = "GESTION - ANALISIS DE PRODUCCION"; 

            arrCabeceraFlex1[0, 0] = "Producto";
            arrCabeceraFlex1[0, 1] = "405";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Id";
            arrCabeceraFlex1[1, 1] = "0";
            arrCabeceraFlex1[1, 2] = "N";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            funFlex.FlexMostrarDatos(FgPro, arrCabeceraFlex1, dtLista, 1, false);
            FgPro.AllowEditing = true;
            FgPro.Cols[1].ComboList = "...";

            arrCabeceraFlex2[0, 0] = "Insumo / Materia Prima";
            arrCabeceraFlex2[0, 1] = "405";
            arrCabeceraFlex2[0, 2] = "C";
            arrCabeceraFlex2[0, 3] = "";
            arrCabeceraFlex2[0, 4] = "c_tipexides";

            arrCabeceraFlex2[1, 0] = "Id";
            arrCabeceraFlex2[1, 1] = "0";
            arrCabeceraFlex2[1, 2] = "N";
            arrCabeceraFlex2[1, 3] = "";
            arrCabeceraFlex2[1, 4] = "c_itedes";

            funFlex.FlexMostrarDatos(FgItem, arrCabeceraFlex2, dtLista, 1, false);
            FgItem.AllowEditing = true;
            FgItem.Cols[1].ComboList = "...";

            arrCabeceraFlex3[0, 0] = "Año";
            arrCabeceraFlex3[0, 1] = "40";
            arrCabeceraFlex3[0, 2] = "N";
            arrCabeceraFlex3[0, 3] = "";
            arrCabeceraFlex3[0, 4] = "n_anotra";

            arrCabeceraFlex3[1, 0] = "Sel";
            arrCabeceraFlex3[1, 1] = "28";
            arrCabeceraFlex3[1, 2] = "B";
            arrCabeceraFlex3[1, 3] = "";
            arrCabeceraFlex3[1, 4] = "";

            funFlex.FlexMostrarDatos(FgAno, arrCabeceraFlex3, dtanos, 1, true);
            FgAno.Rows.Fixed = 0;
            FgAno.RemoveItem(0);

            arrCabeceraFlex4[0, 0] = "Mes";
            arrCabeceraFlex4[0, 1] = "80";
            arrCabeceraFlex4[0, 2] = "C";
            arrCabeceraFlex4[0, 3] = "";
            arrCabeceraFlex4[0, 4] = "c_des";

            arrCabeceraFlex4[1, 0] = "Sel";
            arrCabeceraFlex4[1, 1] = "30";
            arrCabeceraFlex4[1, 2] = "B";
            arrCabeceraFlex4[1, 3] = "";
            arrCabeceraFlex4[1, 4] = "";

            arrCabeceraFlex4[2, 0] = "Id";
            arrCabeceraFlex4[2, 1] = "0";
            arrCabeceraFlex4[2, 2] = "N";
            arrCabeceraFlex4[2, 3] = "";
            arrCabeceraFlex4[2, 4] = "n_id";

            funFlex.FlexMostrarDatos(FgMes, arrCabeceraFlex4, dtmes, 1, true);
            FgMes.Rows.Fixed = 0;
            FgMes.RemoveItem(0);

            FgDatos.Cols.Count = 13;
            Cabecera1();
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, false);
            SetearCabecera1(STU_SISTEMA.ANOTRABAJO.ToString());
        }
        void Cabecera1()
        {
            arrCabecera1[0, 0] = "Tipo Producto";
            arrCabecera1[0, 1] = "80";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_tippro";

            arrCabecera1[1, 0] = "Producto";
            arrCabecera1[1, 1] = "350";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_despro";

            arrCabecera1[2, 0] = "Uni. Med.";
            arrCabecera1[2, 1] = "40";
            arrCabecera1[2, 2] = "C";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "c_abrpre";

            arrCabecera1[3, 0] = "Enero";
            arrCabecera1[3, 1] = "80";
            arrCabecera1[3, 2] = "D";
            arrCabecera1[3, 3] = C_FORMATONUMERICO;
            arrCabecera1[3, 4] = "January";

            arrCabecera1[4, 0] = "Febrero";
            arrCabecera1[4, 1] = "80";
            arrCabecera1[4, 2] = "D";
            arrCabecera1[4, 3] = C_FORMATONUMERICO;
            arrCabecera1[4, 4] = "February";

            arrCabecera1[5, 0] = "Marzo";
            arrCabecera1[5, 1] = "80";
            arrCabecera1[5, 2] = "D";
            arrCabecera1[5, 3] = C_FORMATONUMERICO;
            arrCabecera1[5, 4] = "March";

            arrCabecera1[6, 0] = "Abril";
            arrCabecera1[6, 1] = "80";
            arrCabecera1[6, 2] = "D";
            arrCabecera1[6, 3] = C_FORMATONUMERICO;
            arrCabecera1[6, 4] = "April";

            arrCabecera1[7, 0] = "Mayo";
            arrCabecera1[7, 1] = "80";
            arrCabecera1[7, 2] = "D";
            arrCabecera1[7, 3] = C_FORMATONUMERICO;
            arrCabecera1[7, 4] = "May";

            arrCabecera1[8, 0] = "Junio";
            arrCabecera1[8, 1] = "80";
            arrCabecera1[8, 2] = "D";
            arrCabecera1[8, 3] = C_FORMATONUMERICO;
            arrCabecera1[8, 4] = "June";

            arrCabecera1[9, 0] = "julio";
            arrCabecera1[9, 1] = "80";
            arrCabecera1[9, 2] = "D";
            arrCabecera1[9, 3] = C_FORMATONUMERICO;
            arrCabecera1[9, 4] = "July";

            arrCabecera1[10, 0] = "Agosto";
            arrCabecera1[10, 1] = "80";
            arrCabecera1[10, 2] = "D";
            arrCabecera1[10, 3] = C_FORMATONUMERICO;
            arrCabecera1[10, 4] = "August";

            arrCabecera1[11, 0] = "Setiembre";
            arrCabecera1[11, 1] = "80";
            arrCabecera1[11, 2] = "D";
            arrCabecera1[11, 3] = C_FORMATONUMERICO;
            arrCabecera1[11, 4] = "September";

            arrCabecera1[12, 0] = "Octubre";
            arrCabecera1[12, 1] = "80";
            arrCabecera1[12, 2] = "D";
            arrCabecera1[12, 3] = C_FORMATONUMERICO;
            arrCabecera1[12, 4] = "October";

            arrCabecera1[13, 0] = "Noviembre";
            arrCabecera1[13, 1] = "80";
            arrCabecera1[13, 2] = "D";
            arrCabecera1[13, 3] = C_FORMATONUMERICO;
            arrCabecera1[13, 4] = "November";

            arrCabecera1[14, 0] = "Diciembre";
            arrCabecera1[14, 1] = "80";
            arrCabecera1[14, 2] = "D";
            arrCabecera1[14, 3] = C_FORMATONUMERICO;
            arrCabecera1[14, 4] = "December";
            arrCabecera1[14, 0] = "Diciembre";

            arrCabecera1[15, 0] = "Total";
            arrCabecera1[15, 1] = "80";
            arrCabecera1[15, 2] = "D";
            arrCabecera1[15, 3] = C_FORMATONUMERICO;
            arrCabecera1[15, 4] = "n_canpro";
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
            arrCabecera2[0, 0] = "Nº Registro";
            arrCabecera2[0, 1] = "80";
            arrCabecera2[0, 2] = "C";
            arrCabecera2[0, 3] = "";
            arrCabecera2[0, 4] = "c_numreg";

            arrCabecera2[1, 0] = "Cliente";
            arrCabecera2[1, 1] = "300";
            arrCabecera2[1, 2] = "C";
            arrCabecera2[1, 3] = "";
            arrCabecera2[1, 4] = "c_cliente";

            arrCabecera2[2, 0] = "T.D.";
            arrCabecera2[2, 1] = "40";
            arrCabecera2[2, 2] = "C";
            arrCabecera2[2, 3] = "";
            arrCabecera2[2, 4] = "c_destipdoc";

            arrCabecera2[3, 0] = "Nº Documento";
            arrCabecera2[3, 1] = "100";
            arrCabecera2[3, 2] = "C";
            arrCabecera2[3, 3] = "";
            arrCabecera2[3, 4] = "c_numdoc";

            arrCabecera2[4, 0] = "Fecha Documento";
            arrCabecera2[4, 1] = "70";
            arrCabecera2[4, 2] = "F";
            arrCabecera2[4, 3] = "dd/MM/yyyy";
            arrCabecera2[4, 4] = "d_fchdoc";

            arrCabecera2[5, 0] = "Fecha Vencimiento";
            arrCabecera2[5, 1] = "70";
            arrCabecera2[5, 2] = "F";
            arrCabecera2[5, 3] = "dd/MM/yyyy";
            arrCabecera2[5, 4] = "d_fchven";

            arrCabecera2[6, 0] = "Condicion de Pago";
            arrCabecera2[6, 1] = "80";
            arrCabecera2[6, 2] = "C";
            arrCabecera2[6, 3] = "";
            arrCabecera2[6, 4] = "c_desconpag";

            arrCabecera2[7, 0] = "Dias atrazo";
            arrCabecera2[7, 1] = "60";
            arrCabecera2[7, 2] = "N";
            arrCabecera2[7, 3] = "";
            arrCabecera2[7, 4] = "n_diaatrazo";

            arrCabecera2[8, 0] = "Glosa";
            arrCabecera2[8, 1] = "100";
            arrCabecera2[8, 2] = "D";
            arrCabecera2[8, 3] = "";
            arrCabecera2[8, 4] = "c_glosa";

            arrCabecera2[9, 0] = "Moneda";
            arrCabecera2[9, 1] = "60";
            arrCabecera2[9, 2] = "C";
            arrCabecera2[9, 3] = "";
            arrCabecera2[9, 4] = "c_desmon";

            arrCabecera2[10, 0] = "Tipo Cambio";
            arrCabecera2[10, 1] = "60";
            arrCabecera2[10, 2] = "D";
            arrCabecera2[10, 3] = "0.000";
            arrCabecera2[10, 4] = "n_tc";

            arrCabecera2[11, 0] = "Importe";
            arrCabecera2[11, 1] = "80";
            arrCabecera2[11, 2] = "D";
            arrCabecera2[11, 3] = "0.00";
            arrCabecera2[11, 4] = "n_impsol";

            arrCabecera2[12, 0] = "Saldo";
            arrCabecera2[12, 1] = "80";
            arrCabecera2[12, 2] = "D";
            arrCabecera2[12, 3] = "0.00";
            arrCabecera2[12, 4] = "n_impsalsol";

            arrCabecera2[13, 0] = "Importe";
            arrCabecera2[13, 1] = "80";
            arrCabecera2[13, 2] = "D";
            arrCabecera2[13, 3] = "0.00";
            arrCabecera2[13, 4] = "n_impdol";

            arrCabecera2[14, 0] = "Saldo";
            arrCabecera2[14, 1] = "80";
            arrCabecera2[14, 2] = "D";
            arrCabecera2[14, 3] = "0.00";
            arrCabecera2[14, 4] = "n_impsaldol";

            arrCabecera2[15, 0] = "Total";
            arrCabecera2[15, 1] = "80";
            arrCabecera2[15, 2] = "D";
            arrCabecera2[15, 3] = "0.00";
            arrCabecera2[15, 4] = "n_imptotsol";

            arrCabecera2[16, 0] = "Abono";
            arrCabecera2[16, 1] = "80";
            arrCabecera2[16, 2] = "D";
            arrCabecera2[16, 3] = "0.00";
            arrCabecera2[16, 4] = "n_impabototsol";

            arrCabecera2[17, 0] = "Saldo";
            arrCabecera2[17, 1] = "80";
            arrCabecera2[17, 2] = "D";
            arrCabecera2[17, 3] = "0.00";
            arrCabecera2[17, 4] = "n_impsaltotol";
        }
        void Cabecera3()
        {
            arrCabecera3[0, 0] = "Cliente";
            arrCabecera3[0, 1] = "300";
            arrCabecera3[0, 2] = "C";
            arrCabecera3[0, 3] = "";
            arrCabecera3[0, 4] = "c_nombre";

            arrCabecera3[1, 0] = "Codigo";
            arrCabecera3[1, 1] = "120";
            arrCabecera3[1, 2] = "C";
            arrCabecera3[1, 3] = "";
            arrCabecera3[1, 4] = "c_codpro";

            arrCabecera3[2, 0] = "Producto";
            arrCabecera3[2, 1] = "350";
            arrCabecera3[2, 2] = "C";
            arrCabecera3[2, 3] = "";
            arrCabecera3[2, 4] = "c_despro";

            arrCabecera3[3, 0] = "Uni. Med.";
            arrCabecera3[3, 1] = "40";
            arrCabecera3[3, 2] = "C";
            arrCabecera3[3, 3] = "";
            arrCabecera3[3, 4] = "c_desunimed";

            arrCabecera3[4, 0] = "Cantidad";
            arrCabecera3[4, 1] = "80";
            arrCabecera3[4, 2] = "D";
            arrCabecera3[4, 3] = "0.00";
            arrCabecera3[4, 4] = "n_totcan";

            arrCabecera3[5, 0] = "Importe Soles";
            arrCabecera3[5, 1] = "80";
            arrCabecera3[5, 2] = "D";
            arrCabecera3[5, 3] = "0.00";
            arrCabecera3[5, 4] = "n_impsol";

            arrCabecera3[6, 0] = "Importe Dolares";
            arrCabecera3[6, 1] = "80";
            arrCabecera3[6, 2] = "D";
            arrCabecera3[6, 3] = "0.00";
            arrCabecera3[6, 4] = "n_impdol";

            arrCabecera3[7, 0] = "Importe Exp. Soles";
            arrCabecera3[7, 1] = "80";
            arrCabecera3[7, 2] = "D";
            arrCabecera3[7, 3] = "0.00";
            arrCabecera3[7, 4] = "n_impexpsol";
        }

        private void FrmAnalisisProduccion_Resize(object sender, EventArgs e)
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
            int n_idmon = 0;
            int n_tipfch = 0;
            int n_tipsal = 0;

            //if (funFunciones.NulosC(TxtFchIni.Text) == "")
            //{
            //    MessageBox.Show("¡ No ha indicado la fecha de inicio para la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    TxtFchIni.Focus();
            //    return;
            //}
            //if (funFunciones.NulosC(TxtFchFin.Text) == "")
            //{
            //    MessageBox.Show("¡ No ha indicado la fecha final para la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    TxtFchFin.Focus();
            //    return;
            //}

            //DateTime d_fchini = Convert.ToDateTime(TxtFchIni.Text);
            //DateTime d_fchfin = Convert.ToDateTime(TxtFchFin.Text);

            //if (d_fchini > d_fchfin)
            //{
            //    MessageBox.Show("¡ La fecha de inicio no puede ser mayor a la fecha final de la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    TxtFchIni.Focus();
            //    return;
            //}


            //if (OptTod.Checked == true) { n_idmon = 0; }
            //if (OptSol.Checked == true) { n_idmon = 115; }
            //if (OptDol.Checked == true) { n_idmon = 151; }

            //if (OptFchEmi.Checked == true) { n_tipfch = 1; }
            //if (OptFchVen.Checked == true) { n_tipfch = 2; }
            //if (OptFchReg.Checked == true) { n_tipfch = 3; }

            //if (OptTodDoc.Checked == true) { n_tipsal = 1; }
            //if (OptPen.Checked == true) { n_tipsal = 2; }
            //if (OptPag.Checked == true) { n_tipsal = 3; }

            //int n_tipoconsulta = 0;
            //CN_vta_ventas funCom = new CN_vta_ventas();
            Cabecera1();
            o_produccion.mysConec = mysConec;
            o_produccion.STU_SISTEMA = STU_SISTEMA;
            //if (OptRes.Checked == true) { n_tipoconsulta = 1; }
            //if (OptDet.Checked == true) { n_tipoconsulta = 2; }
            //if (OptResDet.Checked == true) { n_tipoconsulta = 3; }

            string c_CadINAno = funFlex.Flex_CadenaIN(FgAno, 1, 0, 2);
            //string c_CadINIte = funFlex.Flex_CadenaIN(FgItem, 2, 1); ;

            o_produccion.Consulta7(STU_SISTEMA.EMPRESAID, c_CadINAno);
            dtLista = o_produccion.dtListar;
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, true);
            //SetearCabecera1(c_CadINAno);
        }
        void SetearCabecera1(string c_dato)
        {
            funFlex.Flex_UniColumnas(FgDatos, 0, 3, 14, c_dato, 1);
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
                MessageBox.Show("¡ No ha especificado un producto en la ultima fila !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgPro.Focus();
                return;
            }
            FgPro.Rows.Count = FgPro.Rows.Count + 1;
        }

        private void CmdAddIte_Click(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(CboTipPro.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado el tipo de existencia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    c1DockingTab1.SelectedIndex = 0;
            //    //CboTipPro.Focus();
            //    return;
            //}
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
                objIte.mysConec = mysConec;
                dtResult = objIte.BuscarItem("", "c_despro", dtItem, 0);
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count != 0)
                    {
                        c_dato = dtResult.Rows[0]["c_despro"].ToString();
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
                DataTable dtResul = new DataTable();
                string c_dato = "";
                
                dtResul = objItems.BuscarItem("", "n_id", dtInsumo, 0);

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

        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void OptTipPre1_CheckedChanged(object sender, EventArgs e)
        {
            if (OptTipPre1.Checked == true)
            {
                C_FORMATONUMERICO = C_FORMATO1;
            }
        }

        private void OptTipPre2_CheckedChanged(object sender, EventArgs e)
        {
            if (OptTipPre2.Checked == true)
            {
                C_FORMATONUMERICO = C_FORMATO2;
            }
        }

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            funFlex.ExportToExcel_NumFilaCabecera = 2;
            string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + "-PRODUCCION_ANUAL-" + DateTime.Now.ToString("dd-mm-yyyy") + ".xls";
            funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "RESUMEN PRODUCCION DEL AÑO X PRODUCTOS", "TODO EL AÑO", c_nomarch);
        }
    }
}
