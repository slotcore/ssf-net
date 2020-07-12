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
using SIAC_Negocio.Produccion;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sunat;
using SIAC_Entidades.Produccion;
using SIAC_Entidades.Almacen;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using System.Data.OleDb;
using C1.Win.C1FlexGrid;

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmCronograma : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public OleDbConnection AccConec = new OleDbConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        BE_PRO_PROGRAMA entPrograma = new BE_PRO_PROGRAMA();
        List<BE_PRO_PROGRAMADET> lstProgramadet = new List<BE_PRO_PROGRAMADET>();
        List<BE_PRO_PROGRAMADETPRO> lstProgramadetpro = new List<BE_PRO_PROGRAMADETPRO>();
        List<BE_PRO_PROGRAMADETPROCRON> lstProgramadetprocron = new List<BE_PRO_PROGRAMADETPROCRON>();

        // OBJETOS LOCALES
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_pro_programa objRegistro = new CN_pro_programa();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_alm_inventario ObjItem = new CN_alm_inventario();
        CN_mae_prioridad objPrioridad = new CN_mae_prioridad();
        CN_pro_ordenproduccion objOrdenProduccion = new CN_pro_ordenproduccion();
        CN_pro_personal objPerPro = new CN_pro_personal();
        CN_pro_tareas objtareas = new CN_pro_tareas();
        CN_alm_inventariounimed objUniMed = new CN_alm_inventariounimed();
        CN_sun_unimed objUniMedSunat = new CN_sun_unimed();
        CN_pro_productosrecetas objRecetas = new CN_pro_productosrecetas();
        CN_pro_productosrecetasinsumos objRecetasInsumos = new CN_pro_productosrecetasinsumos();
        CN_pro_productosrecetaslineas objLineas = new CN_pro_productosrecetaslineas();
        CN_sun_tipexi objTipExi = new CN_sun_tipexi();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Convertir funCon = new Convertir();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        DatosMySql FunMysql = new DatosMySql();

        // DATATABLE LOCALES
        DataTable dtForm = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtListar = new DataTable();
        DataTable dtTipDoc = new DataTable();
        DataTable dtNumDocRef = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtPerPro = new DataTable();
        DataTable dtPrioridad = new DataTable();
        DataTable dtOrdenProduccion = new DataTable();
        DataTable dtTareas = new DataTable();
        DataTable dtUniMed = new DataTable();
        DataTable dtUniMedSunat = new DataTable();
        DataTable dtRecetas = new DataTable();
        DataTable dtLineas = new DataTable();
        DataTable dtTipExi = new DataTable();
        DataTable dtEquipos = new DataTable();
        DataTable dtInsumos = new DataTable();
        DataTable dtStock = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                                // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[10, 4];                                     // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlexLisPro = new string[14, 5];
        string[,] arrCabeceraFlexLisOrdPro = new string[6, 5];
        string[,] arrCabeceraFlexCrono = new string[9, 5];
        string[,] arrCabeceraFlexInt = new string[7, 5];
        string[,] arrCabeceraFlexIns = new string[7, 5];
        string[,] arrCabeceraFlexPer = new string[7, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmCronograma()
        {
            InitializeComponent();
        }
        private void FrmCronograma_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            Tab1.SelectedIndex = 0;
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboRes, dtPerPro, "n_id", "c_apenom");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 651;
            this.Width = 1214;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;

            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            arrCabeceraFlexLisOrdPro[0, 0] = "Nº Orden Produccion";
            arrCabeceraFlexLisOrdPro[0, 1] = "110";
            arrCabeceraFlexLisOrdPro[0, 2] = "C";
            arrCabeceraFlexLisOrdPro[0, 3] = "";
            arrCabeceraFlexLisOrdPro[0, 4] = "";

            arrCabeceraFlexLisOrdPro[1, 0] = "Fch. Emnision";
            arrCabeceraFlexLisOrdPro[1, 1] = "70";
            arrCabeceraFlexLisOrdPro[1, 2] = "F";
            arrCabeceraFlexLisOrdPro[1, 3] = "dd/MM/yyyy";
            arrCabeceraFlexLisOrdPro[1, 4] = "";

            arrCabeceraFlexLisOrdPro[2, 0] = "Responsable";
            arrCabeceraFlexLisOrdPro[2, 1] = "300";
            arrCabeceraFlexLisOrdPro[2, 2] = "C";
            arrCabeceraFlexLisOrdPro[2, 3] = "";
            arrCabeceraFlexLisOrdPro[2, 4] = "";

            arrCabeceraFlexLisOrdPro[3, 0] = "Nº Items";
            arrCabeceraFlexLisOrdPro[3, 1] = "40";
            arrCabeceraFlexLisOrdPro[3, 2] = "N";
            arrCabeceraFlexLisOrdPro[3, 3] = "";
            arrCabeceraFlexLisOrdPro[3, 4] = "";

            arrCabeceraFlexLisOrdPro[4, 0] = "Prioridad";
            arrCabeceraFlexLisOrdPro[4, 1] = "60";
            arrCabeceraFlexLisOrdPro[4, 2] = "C";
            arrCabeceraFlexLisOrdPro[4, 3] = "";
            arrCabeceraFlexLisOrdPro[4, 4] = "";

            arrCabeceraFlexLisOrdPro[5, 0] = "Id";
            arrCabeceraFlexLisOrdPro[5, 1] = "0";
            arrCabeceraFlexLisOrdPro[5, 2] = "N";
            arrCabeceraFlexLisOrdPro[5, 3] = "";
            arrCabeceraFlexLisOrdPro[5, 4] = "";
            
            funFlex.FlexMostrarDatos(FgLisOrdPro, arrCabeceraFlexLisOrdPro, dtListar, 2, false);

            // FLEX GRID LOS PRODUCTOS DE LA LAS ORDENES DE PRODUCCION
            arrCabeceraFlexLisPro[0, 0] = "Producto";
            arrCabeceraFlexLisPro[0, 1] = "300";
            arrCabeceraFlexLisPro[0, 2] = "C";
            arrCabeceraFlexLisPro[0, 3] = "";
            arrCabeceraFlexLisPro[0, 4] = "";

            arrCabeceraFlexLisPro[1, 0] = "Receta";
            arrCabeceraFlexLisPro[1, 1] = "100";
            arrCabeceraFlexLisPro[1, 2] = "C";
            arrCabeceraFlexLisPro[1, 3] = "";
            arrCabeceraFlexLisPro[1, 4] = "";

            arrCabeceraFlexLisPro[2, 0] = "Linea";
            arrCabeceraFlexLisPro[2, 1] = "100";
            arrCabeceraFlexLisPro[2, 2] = "C";
            arrCabeceraFlexLisPro[2, 3] = "";
            arrCabeceraFlexLisPro[2, 4] = "";

            arrCabeceraFlexLisPro[3, 0] = "Uni. Med";
            arrCabeceraFlexLisPro[3, 1] = "50";
            arrCabeceraFlexLisPro[3, 2] = "C";
            arrCabeceraFlexLisPro[3, 3] = "";
            arrCabeceraFlexLisPro[3, 4] = "";

            arrCabeceraFlexLisPro[4, 0] = "Cantidad";
            arrCabeceraFlexLisPro[4, 1] = "60";
            arrCabeceraFlexLisPro[4, 2] = "D";
            arrCabeceraFlexLisPro[4, 3] = "0.00";
            arrCabeceraFlexLisPro[4, 4] = "";

            arrCabeceraFlexLisPro[5, 0] = "Fch. Entrega";
            arrCabeceraFlexLisPro[5, 1] = "70";
            arrCabeceraFlexLisPro[5, 2] = "F";
            arrCabeceraFlexLisPro[5, 3] = "dd/MM/yyyy";
            arrCabeceraFlexLisPro[5, 4] = "";

            arrCabeceraFlexLisPro[6, 0] = "IdRec";
            arrCabeceraFlexLisPro[6, 1] = "0";
            arrCabeceraFlexLisPro[6, 2] = "N";
            arrCabeceraFlexLisPro[6, 3] = "";
            arrCabeceraFlexLisPro[6, 4] = "";

            arrCabeceraFlexLisPro[7, 0] = "idLin";
            arrCabeceraFlexLisPro[7, 1] = "0";
            arrCabeceraFlexLisPro[7, 2] = "N";
            arrCabeceraFlexLisPro[7, 3] = "";
            arrCabeceraFlexLisPro[7, 4] = "";

            arrCabeceraFlexLisPro[8, 0] = "idPro";
            arrCabeceraFlexLisPro[8, 1] = "0";
            arrCabeceraFlexLisPro[8, 2] = "N";
            arrCabeceraFlexLisPro[8, 3] = "";
            arrCabeceraFlexLisPro[8, 4] = "";

            arrCabeceraFlexLisPro[9, 0] = "idRes";
            arrCabeceraFlexLisPro[9, 1] = "0";
            arrCabeceraFlexLisPro[9, 2] = "N";
            arrCabeceraFlexLisPro[9, 3] = "";
            arrCabeceraFlexLisPro[9, 4] = "";

            arrCabeceraFlexLisPro[10, 0] = "Id Orden Produccion";
            arrCabeceraFlexLisPro[10, 1] = "0";
            arrCabeceraFlexLisPro[10, 2] = "N";
            arrCabeceraFlexLisPro[10, 3] = "";
            arrCabeceraFlexLisPro[10, 4] = "";

            arrCabeceraFlexLisPro[11, 0] = "Fch. Produccion";
            arrCabeceraFlexLisPro[11, 1] = "70";
            arrCabeceraFlexLisPro[11, 2] = "F";
            arrCabeceraFlexLisPro[11, 3] = "dd/MM/yyyy";
            arrCabeceraFlexLisPro[11, 4] = "";

            arrCabeceraFlexLisPro[12, 0] = "Fch. Entrega";                     // aqui se almacena la fecha de entrega , la otra puede ser camciada por el usuario y cuando se valide se actualizara esta
            arrCabeceraFlexLisPro[12, 1] = "0";
            arrCabeceraFlexLisPro[12, 2] = "F";
            arrCabeceraFlexLisPro[12, 3] = "dd/MM/yyyy";
            arrCabeceraFlexLisPro[12, 4] = "";

            arrCabeceraFlexLisPro[13, 0] = "Responsable Produccion";
            arrCabeceraFlexLisPro[13, 1] = "300";
            arrCabeceraFlexLisPro[13, 2] = "C";
            arrCabeceraFlexLisPro[13, 3] = "";
            arrCabeceraFlexLisPro[13, 4] = "";

            funFlex.FlexMostrarDatos(FgLisPro, arrCabeceraFlexLisPro, dtListar, 2, false);
            FgLisPro.Cols[1].ComboList = "...";
            FgLisPro.Cols[2].ComboList = "...";
            FgLisPro.Cols[3].ComboList = "...";

            arrCabeceraFlexCrono[0, 0] = "Producto";
            arrCabeceraFlexCrono[0, 1] = "300";
            arrCabeceraFlexCrono[0, 2] = "C";
            arrCabeceraFlexCrono[0, 3] = "";
            arrCabeceraFlexCrono[0, 4] = "";

            arrCabeceraFlexCrono[1, 0] = "Uni. Med.";
            arrCabeceraFlexCrono[1, 1] = "50";
            arrCabeceraFlexCrono[1, 2] = "C";
            arrCabeceraFlexCrono[1, 3] = "";
            arrCabeceraFlexCrono[1, 4] = "";

            arrCabeceraFlexCrono[2, 0] = "Fch. Entrega";
            arrCabeceraFlexCrono[2, 1] = "70";
            arrCabeceraFlexCrono[2, 2] = "F";
            arrCabeceraFlexCrono[2, 3] = "dd/MM/yyyy";
            arrCabeceraFlexCrono[2, 4] = "";

            arrCabeceraFlexCrono[3, 0] = "Stock Actual";
            arrCabeceraFlexCrono[3, 1] = "70";
            arrCabeceraFlexCrono[3, 2] = "D";
            arrCabeceraFlexCrono[3, 3] = "0.00";
            arrCabeceraFlexCrono[3, 4] = "";

            arrCabeceraFlexCrono[4, 0] = "Cantidad";
            arrCabeceraFlexCrono[4, 1] = "70";
            arrCabeceraFlexCrono[4, 2] = "D";
            arrCabeceraFlexCrono[4, 3] = "0.00";
            arrCabeceraFlexCrono[4, 4] = "";

            arrCabeceraFlexCrono[5, 0] = "Cant Producir";
            arrCabeceraFlexCrono[5, 1] = "70";
            arrCabeceraFlexCrono[5, 2] = "D";
            arrCabeceraFlexCrono[5, 3] = "0.00";
            arrCabeceraFlexCrono[5, 4] = "";

            arrCabeceraFlexCrono[6, 0] = "Id Producto";
            arrCabeceraFlexCrono[6, 1] = "0";
            arrCabeceraFlexCrono[6, 2] = "N";
            arrCabeceraFlexCrono[6, 3] = "";
            arrCabeceraFlexCrono[6, 4] = "";

            arrCabeceraFlexCrono[7, 0] = "Id Receta";
            arrCabeceraFlexCrono[7, 1] = "0";
            arrCabeceraFlexCrono[7, 2] = "N";
            arrCabeceraFlexCrono[7, 3] = "";
            arrCabeceraFlexCrono[7, 4] = "";

            arrCabeceraFlexCrono[8, 0] = "Id OrdenProduccion";
            arrCabeceraFlexCrono[8, 1] = "0";
            arrCabeceraFlexCrono[8, 2] = "N";
            arrCabeceraFlexCrono[8, 3] = "";
            arrCabeceraFlexCrono[8, 4] = "";
            funFlex.FlexMostrarDatos(FgCrono, arrCabeceraFlexCrono, dtListar, 2, false);
            

            arrCabeceraFlexInt[0, 0] = "Producto";
            arrCabeceraFlexInt[0, 1] = "300";
            arrCabeceraFlexInt[0, 2] = "C";
            arrCabeceraFlexInt[0, 3] = "";
            arrCabeceraFlexInt[0, 4] = "";

            arrCabeceraFlexInt[1, 0] = "Uni. Med.";
            arrCabeceraFlexInt[1, 1] = "50";
            arrCabeceraFlexInt[1, 2] = "C";
            arrCabeceraFlexInt[1, 3] = "";
            arrCabeceraFlexInt[1, 4] = "";

            arrCabeceraFlexInt[2, 0] = "Fecha Uso";
            arrCabeceraFlexInt[2, 1] = "70";
            arrCabeceraFlexInt[2, 2] = "F";
            arrCabeceraFlexInt[2, 3] = "dd/MM/yyyy";
            arrCabeceraFlexInt[2, 4] = "";

            arrCabeceraFlexInt[3, 0] = "Stock Actual";
            arrCabeceraFlexInt[3, 1] = "70";
            arrCabeceraFlexInt[3, 2] = "D";
            arrCabeceraFlexInt[3, 3] = "0.00";
            arrCabeceraFlexInt[3, 4] = "";

            arrCabeceraFlexInt[4, 0] = "Cantidad Requerida";
            arrCabeceraFlexInt[4, 1] = "70";
            arrCabeceraFlexInt[4, 2] = "D";
            arrCabeceraFlexInt[4, 3] = "0.00";
            arrCabeceraFlexInt[4, 4] = "";

            arrCabeceraFlexInt[5, 0] = "Saldo";
            arrCabeceraFlexInt[5, 1] = "70";
            arrCabeceraFlexInt[5, 2] = "D";
            arrCabeceraFlexInt[5, 3] = "0.00";
            arrCabeceraFlexInt[5, 4] = "";

            arrCabeceraFlexInt[6, 0] = "Id Producto";
            arrCabeceraFlexInt[6, 1] = "0";
            arrCabeceraFlexInt[6, 2] = "N";
            arrCabeceraFlexInt[6, 3] = "";
            arrCabeceraFlexInt[6, 4] = "";

            funFlex.FlexMostrarDatos(FgInt, arrCabeceraFlexInt, dtListar, 2, false);

            arrCabeceraFlexIns[0, 0] = "Producto";
            arrCabeceraFlexIns[0, 1] = "300";
            arrCabeceraFlexIns[0, 2] = "C";
            arrCabeceraFlexIns[0, 3] = "";
            arrCabeceraFlexIns[0, 4] = "";

            arrCabeceraFlexIns[1, 0] = "Uni. Med.";
            arrCabeceraFlexIns[1, 1] = "50";
            arrCabeceraFlexIns[1, 2] = "C";
            arrCabeceraFlexIns[1, 3] = "";
            arrCabeceraFlexIns[1, 4] = "";

            arrCabeceraFlexIns[2, 0] = "Fecha Uso";
            arrCabeceraFlexIns[2, 1] = "70";
            arrCabeceraFlexIns[2, 2] = "F";
            arrCabeceraFlexIns[2, 3] = "dd/MM/yyyy";
            arrCabeceraFlexIns[2, 4] = "";

            arrCabeceraFlexIns[3, 0] = "Stock Actual";
            arrCabeceraFlexIns[3, 1] = "70";
            arrCabeceraFlexIns[3, 2] = "D";
            arrCabeceraFlexIns[3, 3] = "0.00";
            arrCabeceraFlexIns[3, 4] = "";

            arrCabeceraFlexIns[4, 0] = "Cantidad Requerida";
            arrCabeceraFlexIns[4, 1] = "70";
            arrCabeceraFlexIns[4, 2] = "D";
            arrCabeceraFlexIns[4, 3] = "0.00";
            arrCabeceraFlexIns[4, 4] = "";

            arrCabeceraFlexIns[5, 0] = "Saldo";
            arrCabeceraFlexIns[5, 1] = "70";
            arrCabeceraFlexIns[5, 2] = "D";
            arrCabeceraFlexIns[5, 3] = "0.00";
            arrCabeceraFlexIns[5, 4] = "";

            arrCabeceraFlexIns[6, 0] = "Id Producto";
            arrCabeceraFlexIns[6, 1] = "0";
            arrCabeceraFlexIns[6, 2] = "N";
            arrCabeceraFlexIns[6, 3] = "";
            arrCabeceraFlexIns[6, 4] = "";

            funFlex.FlexMostrarDatos(FgIns, arrCabeceraFlexIns, dtListar, 2, false);


            arrCabeceraFlexPer[0, 0] = "Producto";
            arrCabeceraFlexPer[0, 1] = "300";
            arrCabeceraFlexPer[0, 2] = "C";
            arrCabeceraFlexPer[0, 3] = "";
            arrCabeceraFlexPer[0, 4] = "";

            arrCabeceraFlexPer[1, 0] = "Uni. Med.";
            arrCabeceraFlexPer[1, 1] = "50";
            arrCabeceraFlexPer[1, 2] = "C";
            arrCabeceraFlexPer[1, 3] = "";
            arrCabeceraFlexPer[1, 4] = "";

            arrCabeceraFlexPer[2, 0] = "Cantidad Hora";
            arrCabeceraFlexPer[2, 1] = "70";
            arrCabeceraFlexPer[2, 2] = "D";
            arrCabeceraFlexPer[2, 3] = "0.00";
            arrCabeceraFlexPer[2, 4] = "";

            arrCabeceraFlexPer[3, 0] = "Cantidad Requerida";
            arrCabeceraFlexPer[3, 1] = "70";
            arrCabeceraFlexPer[3, 2] = "D";
            arrCabeceraFlexPer[3, 3] = "0.00";
            arrCabeceraFlexPer[3, 4] = "";

            arrCabeceraFlexPer[4, 0] = "Nº Dia";
            arrCabeceraFlexPer[4, 1] = "70";
            arrCabeceraFlexPer[4, 2] = "N";
            arrCabeceraFlexPer[4, 3] = "";
            arrCabeceraFlexPer[4, 4] = "";

            arrCabeceraFlexPer[5, 0] = "Dia Inicio";
            arrCabeceraFlexPer[5, 1] = "70";
            arrCabeceraFlexPer[5, 2] = "F";
            arrCabeceraFlexPer[5, 3] = "dd/MM/yyyy";
            arrCabeceraFlexPer[5, 4] = "";

            arrCabeceraFlexPer[6, 0] = "Id Producto";
            arrCabeceraFlexPer[6, 1] = "0";
            arrCabeceraFlexPer[6, 2] = "N";
            arrCabeceraFlexPer[6, 3] = "";
            arrCabeceraFlexPer[6, 4] = "";

            funFlex.FlexMostrarDatos(FgPer, arrCabeceraFlexPer, dtListar, 2, false);

            FgLisOrdPro.AllowEditing = false;
            FgLisPro.AllowEditing = false;
            FgCrono.AllowEditing = false;
            FgInt.AllowEditing = false;
            FgIns.AllowEditing = false;
            FgPer.AllowEditing = false;
            
            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(37, ref arrCabeceraDg1);
        }
        void DataTableCargar()
        {
            bool b_result = false;

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(37);                             // INDICAMOS QUE ES EL FORMULARIO MAESTRO DE RECETAS

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objRegistro.mysConec = mysConec;
            b_result = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            if (b_result == true)
            {
                dtListar = objRegistro.dtListar;
            }
            ObjItem.mysConec = mysConec;
            dtItems = ObjItem.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);
            dtStock = ObjItem.Stock(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO);

            objUniMed.mysConec = mysConec;
            dtUniMed = objUniMed.Listar();

            objUniMedSunat.mysConec = mysConec;
            dtUniMedSunat = objUniMedSunat.Listar();

            objPrioridad.mysConec = mysConec;
            dtPrioridad = objPrioridad.Listar(27);

            objtareas.mysConec = mysConec;
            dtTareas = objtareas.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objOrdenProduccion.mysConec = mysConec;
            dtOrdenProduccion = objOrdenProduccion.Listar(STU_SISTEMA.EMPRESAID);

            // CARGAMOS EL PERSONAL DE PRODUCCION
            objPerPro.mysConec = mysConec;
            if (objPerPro.ListarPorCargo(STU_SISTEMA.EMPRESAID, 4) == true)
            {
                dtPerPro = objPerPro.dtPersonal;
            }

            objRecetas.mysConec = mysConec;
            b_result = objRecetas.ListarTodasRecetas(STU_SISTEMA.EMPRESAID);
            if (b_result == true)
            {
                dtRecetas = objRecetas.dtRecetas;
            }

            objLineas.mysConec = mysConec;
            b_result = objLineas.ListarTodasLineas(STU_SISTEMA.EMPRESAID);
            if (b_result == true)
            {
                dtLineas = objLineas.dtLineas;
            }

            objTipExi.mysConec = mysConec;
            dtTipExi = objTipExi.Listar();

            objRecetasInsumos.mysConec = mysConec;
            b_result = objRecetasInsumos.ListarTodasLineas(STU_SISTEMA.EMPRESAID);
            {
                dtInsumos = objRecetasInsumos.dtInsumos;
            }

            ObjItem.mysConec = mysConec;
            dtEquipos = ObjItem.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);
            dtEquipos = funDatos.DataTableFiltrar(dtEquipos, "n_idtipexi = 20");            // FILTRAMOS SOLOS LOS EQUIPOS
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtListar.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtListar, true);
        }
        void Tab_Dimensionar(C1.Win.C1Command.C1DockingTab dokTab, int intAlto, int intAncho)
        {
            Tab1.Height = intAlto;
            Tab1.Width = intAncho;
        }
        void Tab_Posicionar(C1.Win.C1Command.C1DockingTab dokTab, int intPosX, int intPosY)
        {
            dokTab.Left = intPosX;
            dokTab.Top = intPosY;
        }
        void VerRegistro(int n_IdRegistro)
        {
            int n_row = 0;
            int n_fila = 0;
            string c_dato = "";
            DataTable dtResul = new DataTable();
            lstProgramadet.Clear();
            lstProgramadetpro.Clear();
            lstProgramadetprocron.Clear();

            objRegistro.mysConec = mysConec;
            objRegistro.TraerRegistro(n_IdRegistro);

            entPrograma = objRegistro.entPrograma;
            lstProgramadet = objRegistro.lstProgramaDet;
            lstProgramadetpro = objRegistro.lstProgramaDetPro;
            lstProgramadetprocron = objRegistro.lstProgramaDetProCron;

            TxtNumSer.Text = entPrograma.c_numser;
            TxtNumDoc.Text = entPrograma.c_numdoc;
            TxtFchEmi.Text = entPrograma.d_fchemi.ToString();
            CboRes.SelectedValue = entPrograma.n_idpro;
            TxtFchIni.Text = entPrograma.d_fchini.ToString("dd/MM/yyyy");
            TxtFchFin.Text = entPrograma.d_fchfin.ToString("dd/MM/yyyy");
            TxtObs.Text = entPrograma.c_obs;

            TxtNumHorDia.Text = entPrograma.n_numhordia.ToString("");
            TxtNumDias.Text = entPrograma.n_numdiapro.ToString("");
            OptFchPro.Checked = true;

            // ***********************************
            // MOSTRAMOS LAS ORDENES DE PRODUCCION
            FgLisOrdPro.Rows.Count = 2;
            n_fila = 2;
            for (n_row = 0; n_row <= lstProgramadet.Count - 1; n_row++)
            {
                FgLisOrdPro.Rows.Count = FgLisOrdPro.Rows.Count + 1;

                dtResul = funDatos.DataTableFiltrar(dtOrdenProduccion, "n_id = " + lstProgramadet[n_row].n_idordpro + " ");
                if (dtResul.Rows.Count != 0)
                //{ c_dato = dtResul.Rows[0]["c_numser"].ToString() + "-" + dtResul.Rows[0]["c_numdoc"].ToString(); }
                { c_dato =  dtResul.Rows[0]["c_numdoc"].ToString(); }
                else
                { c_dato = ""; }
                FgLisOrdPro.SetData(n_fila, 1, c_dato);                                                         // NUMERO DE ORDEN DE PRODUCCION

                c_dato = Convert.ToDateTime(dtResul.Rows[0]["d_fchemi"]).ToString("dd/MM/yyyy");
                FgLisOrdPro.SetData(n_fila, 2, c_dato);                                                         // FECHA DE EMISION

                DataTable dtResul2 = new DataTable();
                dtResul2 = funDatos.DataTableFiltrar(dtPerPro, "n_id = " + dtResul.Rows[0]["n_idres"].ToString() + " ");
                c_dato = dtResul2.Rows[0]["c_apenom"].ToString();
                FgLisOrdPro.SetData(n_fila, 3, c_dato);                                                         // NOMBRE DEL RESPONSABLE                                         

                c_dato = "0";
                FgLisOrdPro.SetData(n_fila, 4, c_dato);                                                         // NUMERO DE ITEMS

                dtResul2 = funDatos.DataTableFiltrar(dtPrioridad, "n_id = " + dtResul.Rows[0]["n_idpri"].ToString() + " ");
                c_dato = dtResul2.Rows[0]["c_des"].ToString();
                FgLisOrdPro.SetData(n_fila, 5, c_dato);

                c_dato = lstProgramadet[n_row].n_idordpro.ToString();
                FgLisOrdPro.SetData(n_fila, 6, c_dato);
                n_fila = n_fila + 1;
            }
            MostrarProductosOrdenProduccion(Convert.ToInt32(lstProgramadet[0].n_idordpro));
            booAgregando = false;
            CreaCronograma();
            VerCronograma();

            TraerIntermedios2();
            TraerInsumos2();
            MostrarPersonal();

            FgCrono.Cols.Frozen = 9;
            FgInt.Cols.Frozen = 7;
            FgIns.Cols.Frozen = 7;
        }
        void Nuevo()
        {
            booAgregando = true;
            n_QueHace = 1;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            
            Tab1.SelectedIndex = 1;
            Tab2.SelectedIndex = 0;

            OptFchEnt.Checked = true;
            booAgregando = false;

            // MOSTRAMOS EL ULTIMO NUMERO DE LA PROGRAMACION
            string c_numdoc = "";
            objTipDoc.mysConec = mysConec;
            c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 78, "0001");
            TxtNumSer.Text = "0001";
            TxtNumDoc.Text = c_numdoc;
            TxtNumHorDia.Text = "8";
            TxtNumDias.Text = "5";
            TxtFchIni.Text = "01/" + STU_SISTEMA.MESTRABAJO.ToString("00") + "/" + STU_SISTEMA.ANOTRABAJO.ToString("0000");

            int dias = DateTime.DaysInMonth(STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            TxtFchFin.Text = dias.ToString("00") + "/" + STU_SISTEMA.MESTRABAJO.ToString("00") + "/" + STU_SISTEMA.ANOTRABAJO.ToString("0000");

            FgLisPro.AllowEditing = true;
            TxtFchEmi.Focus();
        }
        void Blanquea()
        {
            FgLisOrdPro.AllowEditing = false;
            lstProgramadet.Clear();
            lstProgramadetpro.Clear();
            lstProgramadetprocron.Clear();

            FgLisOrdPro.Rows.Count = 2;
            FgLisPro.Rows.Count = 2;
            FgCrono.Cols.Count = 8;
            FgCrono.Rows.Count = 2;

            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtFchEmi.Text = "";
            CboRes.SelectedValue = 0;

            TxtFchIni.Text = "";
            TxtFchIni.CustomFormat = " ";
            TxtFchIni.Format = DateTimePickerFormat.Custom;

            TxtFchFin.Text = "";
            TxtFchFin.CustomFormat = " ";
            TxtFchFin.Format = DateTimePickerFormat.Custom;

            TxtObs.Text = "";
        }
        void Bloquea()
        {
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtFchEmi.Enabled = !TxtFchEmi.Enabled;
            CboRes.Enabled = !CboRes.Enabled;
            //TxtFchIni.Enabled = !TxtFchIni.Enabled;
            //TxtFchFin.Enabled = !TxtFchFin.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
            TxtNumDias.Enabled = !TxtNumDias.Enabled;
            TxtNumHorDia.Enabled = !TxtNumHorDia.Enabled;

            CmdAddOP.Enabled = !CmdAddOP.Enabled;
            CmdDelOP.Enabled = !CmdDelOP.Enabled;

            CmdAddPro.Enabled = !CmdAddPro.Enabled;
            CmdDelPro.Enabled = !CmdDelPro.Enabled;

            CmdProcesar.Enabled = !CmdProcesar.Enabled;
            CmdRep.Enabled = !CmdRep.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolMenuImprimir.Enabled = !ToolMenuImprimir.Enabled;
            //ToolExportar.Enabled = !ToolExportar.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
        }
        void Modificar()
        {
            booAgregando = true;
            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[4].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";

            Tab1.SelectedIndex = 1;
            Tab2.SelectedIndex = 0;
            TxtFchEmi.Focus();

            FgLisPro.AllowEditing = true;
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[4].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistro.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistro.mysConec = mysConec;
                    booResult = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
                    if (booResult == true)
                    {
                        dtListar = objRegistro.dtListar;
                        // MOSTRAMOS LOS DATOS EN LA GRILLA
                        ListarItems();
                    }
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistro.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            return booResult;
        }
        void Cancelar()
        {
            n_QueHace = 3;
            ActivarTool();
            Bloquea();
            LblTitulo2.Text = "Detalle del Registro";
            
            Tab1.TabPages[0].Enabled = true;
            Tab1.SelectedIndex = 0;
            DgLista.Focus();
        }
        bool Grabar()
        {
            bool booResultado = false;
            if (CamposOK() == false)
            {
                return booResultado;
            }
            AsignarEntidad();

            mysConec = FunMysql.ReAbrirConeccion(mysConec);
            
            if (n_QueHace == 1)
            {
                booResultado = objRegistro.Insertar(entPrograma, lstProgramadet, lstProgramadetpro, lstProgramadetprocron);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistro.Actualizar(entPrograma, lstProgramadet, lstProgramadetpro, lstProgramadetprocron);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ No se pudo guardar el registro por el siguiente motivo ! " + objRegistro.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {
            entPrograma.n_idemp = STU_SISTEMA.EMPRESAID;
            if (n_QueHace == 1)
            {
                entPrograma.n_id = 0;
            }
            else
            {
                entPrograma.n_id = entPrograma.n_id;
            }

            entPrograma.n_idtipdoc = 78;
            entPrograma.c_numser = TxtNumSer.Text;
            entPrograma.c_numdoc = TxtNumDoc.Text;
            entPrograma.n_anotra = STU_SISTEMA.ANOTRABAJO;
            entPrograma.n_mestra = STU_SISTEMA.MESTRABAJO;
            entPrograma.n_idpro = Convert.ToInt32(CboRes.SelectedValue);
            entPrograma.n_idtipdoc = 78;
            entPrograma.d_fchini = Convert.ToDateTime(TxtFchIni.Text);
            entPrograma.d_fchfin = Convert.ToDateTime(TxtFchFin.Text);
            entPrograma.d_fchemi = Convert.ToDateTime(TxtFchEmi.Text);
            entPrograma.n_idpro = Convert.ToInt16(CboRes.SelectedValue);
            entPrograma.c_obs = TxtObs.Text;
            entPrograma.n_numdiapro = Convert.ToInt32(TxtNumDias.Text);
            entPrograma.n_numhordia = Convert.ToInt32(TxtNumHorDia.Text);
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (TxtNumSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie del programa de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumSer.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento del programa de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumDoc.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtFchEmi.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de emision !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchEmi.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtFchIni.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de inicio del programa de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                booEstado = false;
                return booEstado;
            }
            if (TxtFchFin.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha final del programa de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboRes.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado en nombre del programador de la produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboRes.Focus();
                booEstado = false;
                return booEstado;
            }

            // VERIFICAMOS QUE SE HAYA AGREGADO ORDENES DE PRODUCCION

            if (lstProgramadet.Count == 0)
            {
                MessageBox.Show("¡ No ha programado ninguna Orden de Produccion, debe de programar al menos una orden", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgLisOrdPro.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToDateTime(TxtFchFin.Text) < Convert.ToDateTime(TxtFchIni.Text))
            {
                MessageBox.Show("¡ La fecha de termino no puede ser menor a la fecha de inicio", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                booEstado = false;
                return booEstado;
            }
            // VERIFICAMOS SI LOS PRODUCTOS A PRODUCIR TIENEN TODOS LOS DATOS NECESARIOS
            int n_row = 0;
            string c_nonitem = "";

            for (n_row = 0; n_row <= lstProgramadetpro.Count - 1; n_row++)
            {
                c_nonitem = Convert.ToString(funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", lstProgramadetpro[n_row].n_idite.ToString(), "N"));

                //if (lstProgramadetpro[n_row].d_fchpro.ToString() == "")
                //{
                //    MessageBox.Show("¡ No ha especificado la fecha de produccion para el item : " + c_nonitem + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //    FgLisPro.Focus();
                //    booEstado = false;
                //    return booEstado;
                //}
                //if (lstProgramadetpro[n_row].h_horini.ToString() == "")
                //{
                //    MessageBox.Show("¡ No ha especificado la hora de inicio de produccion para el item : " + c_nonitem + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //    FgLisPro.Focus();
                //    booEstado = false;
                //    return booEstado;
                //}
                //if (lstProgramadetpro[n_row].n_idres == 0)
                //{
                //    MessageBox.Show("¡ No ha especificado el responsable de produccion para el item : " + c_nonitem + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //    FgLisPro.Focus();
                //    booEstado = false;
                //    return booEstado;
                //}
                //if (lstProgramadetpro[n_row].n_numope == 0)
                //{
                //    MessageBox.Show("¡ No ha especificado el numero de operarios que inmtervienen en la produccion del item : " + c_nonitem + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //    FgLisPro.Focus();
                //    booEstado = false;
                //    return booEstado;
                //}
            }

            return booEstado;
        }
        private void ToolNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }
        private void ToolModificar_Click(object sender, EventArgs e)
        {
            Modificar();
        }
        private void ToolEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }
        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            bool b_Result = false;

            if (Grabar() == true)
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objRegistro.mysConec = mysConec;
                b_Result = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
                if (b_Result == true)
                {
                    dtListar = objRegistro.dtListar;
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }

                DialogResult Rpta = MessageBox.Show("! El registro se agrego con exito ¡ ¿Desea agregar otro registro? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (DialogResult.Yes == Rpta)
                {
                    Nuevo();
                    return;
                }
                else
                {
                    Cancelar();
                }
            }
        }
        private void ToolCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }
        private void ToolSalir_Click(object sender, EventArgs e)
        {
            objFormVis = null;
            objFormVis = null;

            this.Close();
        }
        private void FrmCronograma_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (dtListar.Rows.Count == 0)
                {
                    DialogResult Rpta = MessageBox.Show("No se han encontrado registros, ¿ Desea agregar uno ahora ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (DialogResult.Yes == Rpta)
                    {
                        Nuevo();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    DgLista.Focus();
                }
            }  
        }
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[4].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            VerRegistro(intIdRegistro);
            booAgregando = false;
        }
        private void DgLista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DataTable dtResult = new DataTable();

                string c_CadFiltro = funDbGrid.DG_LeerCondicionesFiltro(DgLista);
                dtResult = funDbGrid.DG_Filtrar(dtListar, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
        }
        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (n_QueHace != 3) { return; }

            if (DgLista.RowCount == 0) { return; }
            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[4].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    Tab2.SelectedIndex = 0;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }
        private void Tab1_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
        }
        private void TxtNumSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtNumDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtFchEmi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboRes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtFchIni_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                TxtFchIni.Text = "";
                TxtFchIni.CustomFormat = " ";
                TxtFchIni.Format = DateTimePickerFormat.Custom;
            }
        }
        private void TxtFchIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtFchIni_ValueChanged(object sender, EventArgs e)
        {
            TxtFchIni.CustomFormat = "dd/MM/yyyy";
            TxtFchIni.Format = DateTimePickerFormat.Custom;
            if (TxtFchIni.Text != "")
            {
                CreaCronograma();
            }
        }
        private void TxtFchFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtFchFin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                TxtFchFin.Text = "";
                TxtFchFin.CustomFormat = " ";
                TxtFchFin.Format = DateTimePickerFormat.Custom;
            }
        }
        private void TxtFchFin_ValueChanged(object sender, EventArgs e)
        {
            TxtFchFin.CustomFormat = "dd/MM/yyyy";
            TxtFchFin.Format = DateTimePickerFormat.Custom;

            if (TxtFchFin.Text != "")
            {
                CreaCronograma();
            }
       }
        private void TxtObs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        void CreaCronograma()
        {
            if (booAgregando == true) { return; }
            if (TxtFchIni.Text == " ") { return; }
            if (TxtFchFin.Text == " ") { return; }

            if (Convert.ToDateTime(TxtFchIni.Text) > Convert.ToDateTime(TxtFchFin.Text))
            {
                MessageBox.Show("La fecha final no puede ser menor o igual a la fecha de inicio", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }
            int n_col = 0;
            int n_numdia = 0;
            DateTime d_fecha = Convert.ToDateTime(TxtFchIni.Text);

            string c_fchini = "";
            c_fchini = d_fecha.AddDays(-7).ToString("dd/MM/yyyy");
            d_fecha = Convert.ToDateTime(c_fchini);
            n_numdia = funFunciones.Fecha_Intervalo(c_fchini, TxtFchFin.Text);
            funFlex.FlexMostrarDatos(FgCrono, arrCabeceraFlexCrono, dtListar, 2, false);
            FgCrono.Cols.Count = 10;

            FgCrono.Rows[0].Height = 33;
            FgCrono.Rows[1].Height = 33;
            for (n_col = 1; n_col <= n_numdia; n_col++)
            {
                FgCrono.Cols.Count = FgCrono.Cols.Count + 1;
                FgCrono.Cols[FgCrono.Cols.Count - 1].DataType = typeof(Double);
                //FgCrono.Cols[FgCrono.Cols.Count - 1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                //FgCrono.Cols[FgCrono.Cols.Count - 1].Format = "0.00";

                funFlex.Flex_FixUnirFilas(FgCrono, FgCrono.Cols.Count - 1, 0, 1, d_fecha.ToString("dd/MM/yyyy"),50);

                CellRange rg = FgCrono.GetCellRange(0, FgCrono.Cols.Count - 1, 1, FgCrono.Cols.Count - 1);
                rg.StyleNew.TextDirection =TextDirectionEnum.Up;

                if (d_fecha.ToString("dddd") == "domingo")
                {
                    rg.StyleNew.BackColor = Color.Gray;
                }
                d_fecha = d_fecha.AddDays(1);
            }

            // FLEX DE INTERMEDIOS
            FgInt.Rows[0].Height = 33;
            FgInt.Rows[1].Height = 33;

            FgInt.Cols.Count = 8;
            d_fecha = Convert.ToDateTime(c_fchini);

            for (n_col = 1; n_col <= n_numdia; n_col++)
            {
                FgInt.Cols.Count = FgInt.Cols.Count + 1;
                FgInt.Cols[FgInt.Cols.Count - 1].DataType = typeof(Double);
                //FgInt.Cols[FgInt.Cols.Count - 1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                //FgInt.Cols[FgInt.Cols.Count - 1].Format = "0.00";

                funFlex.Flex_FixUnirFilas(FgInt, FgInt.Cols.Count - 1, 0, 1, d_fecha.ToString("dd/MM/yyyy"), 60);

                CellRange rg = FgInt.GetCellRange(0, FgInt.Cols.Count - 1, 1, FgInt.Cols.Count - 1);
                rg.StyleNew.TextDirection = TextDirectionEnum.Up;

                if (d_fecha.ToString("dddd") == "domingo")
                {
                    rg.StyleNew.BackColor = Color.Gray;
                }
                d_fecha = d_fecha.AddDays(1);
            }

            // FLEX DE INSUMOS
            FgIns.Rows[0].Height = 33;
            FgIns.Rows[1].Height = 33;

            FgIns.Cols.Count = 8;
            d_fecha = Convert.ToDateTime(c_fchini);

            for (n_col = 1; n_col <= n_numdia; n_col++)
            {
                FgIns.Cols.Count = FgIns.Cols.Count + 1;
                FgIns.Cols[FgIns.Cols.Count - 1].DataType = typeof(Double);
                //FgIns.Cols[FgIns.Cols.Count - 1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                //FgIns.Cols[FgIns.Cols.Count - 1].Format = "0.00";

                funFlex.Flex_FixUnirFilas(FgIns, FgIns.Cols.Count - 1, 0, 1, d_fecha.ToString("dd/MM/yyyy"), 60);

                CellRange rg = FgIns.GetCellRange(0, FgIns.Cols.Count - 1, 1, FgIns.Cols.Count - 1);
                rg.StyleNew.TextDirection = TextDirectionEnum.Up;

                if (d_fecha.ToString("dddd") == "domingo")
                {
                    rg.StyleNew.BackColor = Color.Gray;
                }
                d_fecha = d_fecha.AddDays(1);
            }

            // FLEX DE PERSONAL
            FgPer.Rows[0].Height = 33;
            FgPer.Rows[1].Height = 33;

            FgPer.Cols.Count = 8;
            d_fecha = Convert.ToDateTime(c_fchini);
            booAgregando = true;
            for (n_col = 1; n_col <= n_numdia; n_col++)
            {
                FgPer.Cols.Count = FgPer.Cols.Count + 1;
                FgPer.Cols[FgPer.Cols.Count - 1].DataType = typeof(Int32);
                //FgPer.Cols[FgPer.Cols.Count - 1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                funFlex.Flex_UnirFilas(FgPer, FgPer.Cols.Count - 1, 0, 1, d_fecha.ToString("dd/MM/yyyy"), 30);

                CellRange rg = FgPer.GetCellRange(0, FgPer.Cols.Count - 1, 1, FgPer.Cols.Count - 1);
                rg.StyleNew.TextDirection = TextDirectionEnum.Up;

                if (d_fecha.ToString("dddd") == "domingo")
                {
                    rg.StyleNew.BackColor = Color.Gray;
                }
                d_fecha = d_fecha.AddDays(1);
            }
            booAgregando = false;
        }
        private void FrmCronograma_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
        }
        private void CmdAddOP_Click(object sender, EventArgs e)
        {
            if (TxtFchIni.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de inicio del programa de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }
            if (TxtFchFin.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha final del programa de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }
            BuscarOrdenProduccion();
        }
        void BuscarOrdenProduccion()
        {
            string[,] arrCabeceraDg1 = new string[6, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            int n_row;
            string c_cadin = funFlex.Flex_CadenaIN(FgLisOrdPro,6,2);
            
            CN_pro_ordenproduccion objOrdProduccion = new CN_pro_ordenproduccion();

            DataTable dtOrdenProdPend = new DataTable();

            objOrdProduccion.mysConec = mysConec;
            if (objOrdProduccion.TraeOrdenProduccionPendientes(STU_SISTEMA.EMPRESAID, c_cadin) == false)
            {
                MessageBox.Show("No se pudieron traer las ordenes de produccion pendiente; por el siguiente motivo : " + objOrdProduccion.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            dtOrdenProdPend = objOrdProduccion.dtOrdenProdPendientes;

            arrCabeceraDg1[0, 0] = "Nº Orden Produccion";
            arrCabeceraDg1[0, 1] = "120";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_numdoc";

            arrCabeceraDg1[1, 0] = "Solicitante";
            arrCabeceraDg1[1, 1] = "300";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_resapenom";

            arrCabeceraDg1[2, 0] = "Fecha";
            arrCabeceraDg1[2, 1] = "80";
            arrCabeceraDg1[2, 2] = "F";
            arrCabeceraDg1[2, 3] = "d_fchemi";

            arrCabeceraDg1[3, 0] = "Prioridad";
            arrCabeceraDg1[3, 1] = "60";
            arrCabeceraDg1[3, 2] = "C";
            arrCabeceraDg1[3, 3] = "c_prides";

            arrCabeceraDg1[4, 0] = "Nº Items";
            arrCabeceraDg1[4, 1] = "60";
            arrCabeceraDg1[4, 2] = "N";
            arrCabeceraDg1[4, 3] = "n_numite";

            arrCabeceraDg1[5, 0] = "Id";
            arrCabeceraDg1[5, 1] = "0";
            arrCabeceraDg1[5, 2] = "N";
            arrCabeceraDg1[5, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            xFun.Buscar_CampoOrden = "c_numdoc";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtOrdenProdPend);

            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }

            List<BE_PRO_ORDENPRODUCCIONDET> lstOrdeProddet = new List<BE_PRO_ORDENPRODUCCIONDET>();
            BE_PRO_ORDENPRODUCCION entOrdeProd = new BE_PRO_ORDENPRODUCCION();

            objOrdProduccion.TraerRegistro(Convert.ToInt32(dtResult.Rows[0]["n_id"]));
            if (objOrdProduccion.IntErrorNumber == 0)
            {
                entOrdeProd = objOrdProduccion.entOrdenProd;
                lstOrdeProddet = objOrdProduccion.lstOrdenProdDet;                                       // OBTENEMOS EL DETALLE DE LA ORDEN DE PRODUCCION SELECCIONADA
            }
            else
            {
                MessageBox.Show("No se pudieron traer las ordenes de produccion pendiente; por el siguiente motivo : " + objOrdProduccion.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            // CARGAMOS LA ORDEN DE PRODUCCION
            BE_PRO_PROGRAMADET entTmp = new BE_PRO_PROGRAMADET();

            entTmp.n_idpro = 0;
            entTmp.n_idordpro = entOrdeProd.n_id;
            lstProgramadet.Add(entTmp);                                                             // AGREGAMOS LA ORDEN DE PRODUCCION SELECCIONADA 

            int n_idinspri = 0;                                                                     // PARA ALMACENAR EL INSUMO PRINCIPAL
            double n_caninspri = 0;                                                                 // PARA ALMACENAR LA CANTIDAD DEL INSUMO PRINCIPAL 
            int n_numdias = Convert.ToInt16(TxtNumDias.Text);

            // LLENAMIOS LA LISTA lstProgramaedetpro CON LOS PRODUCTOS A PRODUCIR
            #region CARGAR_PRODUCTOS
            // CARGAMOS LOS PRODUCTOS A PRODUCIR DE LA ORDEN DE PRODUCCION SELECCIONADA
            for (n_row = 0; n_row <= lstOrdeProddet.Count - 1; n_row++)
            {
                BE_PRO_PROGRAMADETPRO entTmpProd = new BE_PRO_PROGRAMADETPRO();

                entTmpProd.n_idpro = 0;
                entTmpProd.n_idordpro = lstOrdeProddet[n_row].n_idord;                              // ID DE LA ORDEN DE PRODUCCION SELECCIONADA
                entTmpProd.n_idite = lstOrdeProddet[n_row].n_idpro;
                entTmpProd.n_idrec = lstOrdeProddet[n_row].n_idrec;

                // ASIGNAMOS LA LINEA POR DEFECTO DE LA RECETA
                dtResult = funDatos.DataTableFiltrar(dtLineas, "n_idrec = " + lstOrdeProddet[n_row].n_idrec + "");
                if (dtResult.Rows.Count != 0)
                {
                    entTmpProd.n_idlin = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                }
                else
                {
                    entTmpProd.n_idlin = 0;
                }

                entTmpProd.n_idunimed = lstOrdeProddet[n_row].n_idunimed;
                entTmpProd.n_can = lstOrdeProddet[n_row].n_can;
                entTmpProd.d_fchent = lstOrdeProddet[n_row].d_fchent;


                entTmpProd.d_fchpro = Convert.ToDateTime(funFunciones.FechaRestarDias(lstOrdeProddet[n_row].d_fchent.ToString(), n_numdias));                                    
                entTmpProd.h_horini = "";
                entTmpProd.h_horfin = "";
                entTmpProd.n_idres = 0;

                BuscarRecetaInsPrincipal(entTmpProd.n_idite, entTmpProd.n_idrec, entTmpProd.n_idlin, entTmpProd.n_can, ref n_idinspri, ref n_caninspri);

                lstProgramadetpro.Add(entTmpProd);
            }
            #endregion CARGAR_PRODUCTOS         

            booAgregando = true;
            MostrarOrdenProduccion(entOrdeProd);                                                    // MUESTRA LA ORDEN DE PRODUCCION EN EL GRID FgLisOrdPro
            
            MostrarProductosOrdenProduccion(Convert.ToInt32(entOrdeProd.n_id));                     // MUESTRA EL DETALLE DE LA ORDEN DE PRODUCCION
            booAgregando = false;
        }
        void MostrarProductosOrdenProduccion(int IdOrdenProduccion)
        {
            int n_row = 0;
            string c_dato = "";
            int n_filaflex = 0;
            int n_idpro = 0;
            int n_idrec = 0;
            booAgregando = true;
            FgLisPro.Rows.Count = 2;
            n_filaflex = 2;

            for (n_row = 0; n_row <= lstProgramadetpro.Count - 1; n_row++)
            {
                if (lstProgramadetpro[n_row].n_idordpro == IdOrdenProduccion)
                {
                    FgLisPro.Rows.Count = FgLisPro.Rows.Count + 1;

                    // MOSTRAMOS EL NOMBRE DEL ITEM
                    c_dato = lstProgramadetpro[n_row].n_idite.ToString("");
                    n_idpro = lstProgramadetpro[n_row].n_idite;
                    FgLisPro.SetData(n_filaflex, 9, c_dato);                                                      // ID DEL PRODUCTO

                    c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", c_dato, "N").ToString();
                    FgLisPro.SetData(n_filaflex, 1, c_dato);                                                       // NOMBRE DEL PRODUCTO

                    c_dato = lstProgramadetpro[n_row].n_idrec.ToString("");
                    n_idrec = lstProgramadetpro[n_row].n_idrec;
                    c_dato = funDatos.DataTableBuscar(dtRecetas, "n_id", "c_codrec", c_dato, "N").ToString();
                    FgLisPro.SetData(n_filaflex, 2, c_dato);                                                       // CODIGO DE LA RECETA        

                    c_dato = lstProgramadetpro[n_row].n_idlin.ToString("");

                    c_dato = funDatos.DataTableBuscar(dtLineas, "n_id", "c_codlin", c_dato, "N").ToString();
                    FgLisPro.SetData(n_filaflex, 3, c_dato);                                                       // CODIGO DE LA LINEA

                    c_dato = lstProgramadetpro[n_row].n_idlin.ToString("");
                    FgLisPro.SetData(n_filaflex, 8, c_dato);                                                      // ID DE LA LINEA

                    c_dato = lstProgramadetpro[n_row].n_idunimed.ToString("");
                    c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_despre", c_dato, "N").ToString();
                    FgLisPro.SetData(n_filaflex, 4, c_dato);                                                       // UNIDAD DE MEDIDA

                    c_dato = lstProgramadetpro[n_row].n_can.ToString("0.00");
                    FgLisPro.SetData(n_filaflex, 5, c_dato);                                                       // CANTIDAD A PRODUCCIR

                    if (lstProgramadetpro[n_row].d_fchent != null)
                    {
                        c_dato = Convert.ToDateTime(lstProgramadetpro[n_row].d_fchent).ToString("dd/MM/yyyy");
                        FgLisPro.SetData(n_filaflex, 6, c_dato);                                                   // FECHA DE PRODUCCION
                        
                        c_dato = Convert.ToDateTime(lstProgramadetpro[n_row].d_fchpro).ToString("dd/MM/yyyy");
                        FgLisPro.SetData(n_filaflex, 13, c_dato);  
                    }
                    else
                    {
                        FgLisPro.SetData(n_filaflex, 6, "");                                                       // FECHA DE ENTREGA
                        FgLisPro.SetData(n_filaflex, 13, "");  
                    }

                    c_dato = lstProgramadetpro[n_row].n_idrec.ToString("");
                    FgLisPro.SetData(n_filaflex, 7, c_dato);                                                       // ID DE LA RECETA

                    c_dato = lstProgramadetpro[n_row].n_idordpro.ToString("");
                    FgLisPro.SetData(n_filaflex, 11, c_dato);                                                       // ID DEL ITEM

                    //c_dato = lstProgramadetpro[n_row].n_idordpro.ToString("");
                    //FgLisPro.SetData(n_filaflex, 10, c_dato);                                                       // ID DEL ITEM
                    //if (funFunciones.NulosC(lstProgramadetpro[n_row].h_horini) != "")
                    //{ c_dato = lstProgramadetpro[n_row].h_horini.ToString(); }
                    //else
                    //{ c_dato = ""; }
                    //FgLisPro.SetData(n_filaflex, 7, c_dato);                                                       // HORA DE PRODUCCION

                    if (lstProgramadetpro[n_row].d_fchpro != null)
                    {
                        c_dato = Convert.ToDateTime(lstProgramadetpro[n_row].d_fchpro).ToString("dd/MM/yyyy");
                        FgLisPro.SetData(n_filaflex, 12, c_dato);                                                   // FECHA DE PRODUCCION
                    }
                    else
                    {
                        FgLisPro.SetData(n_filaflex, 12, "");                                                       // FECHA DE PRODUCCION
                    }

                    c_dato = lstProgramadetpro[n_row].n_idres.ToString("");
                    c_dato = funDatos.DataTableBuscar(dtPerPro, "n_id", "c_apenom", c_dato, "N").ToString();
                    FgLisPro.SetData(n_filaflex, 14, c_dato);                                                       // UNIDAD DE MEDIDA

                    n_filaflex = n_filaflex + 1;
                }
            }
            booAgregando = false;
        }
        void MostrarOrdenProduccion(BE_PRO_ORDENPRODUCCION entOrdenProduccion)
        {
            string c_dato = "";
            // ***********************************
            // AGREGAMOS LAS ORDENES DE PRODUCCION

            FgLisOrdPro.Rows.Count = FgLisOrdPro.Rows.Count + 1;

            c_dato = entOrdenProduccion.c_numser + "-" + entOrdenProduccion.c_numdoc;
            FgLisOrdPro.SetData(FgLisOrdPro.Rows.Count - 1, 1, c_dato);                                  // NUMERO DE LA ORDEN DE PRODUCCION

            c_dato = entOrdenProduccion.d_fchemi.ToString("dd/MM/yyyy");
            FgLisOrdPro.SetData(FgLisOrdPro.Rows.Count - 1, 2, c_dato);                                  // FECHA DE EMISION DE LA ORDEN DE PRODUCCION

            c_dato = entOrdenProduccion.n_idres.ToString();
            c_dato = funDatos.DataTableBuscar(dtPerPro, "n_id", "c_apenom", c_dato, "N").ToString();
            FgLisOrdPro.SetData(FgLisOrdPro.Rows.Count - 1, 3, c_dato);                                  // RESPONSABLE DE LA ORDEN DE PRODUCCION

            c_dato = "0";
            FgLisOrdPro.SetData(FgLisOrdPro.Rows.Count - 1, 4, c_dato);                                  // NUMERO DE ITEM DE LA ORDEN DE PRODUCCION

            c_dato = entOrdenProduccion.n_idpri.ToString();
            c_dato = funDatos.DataTableBuscar(dtPrioridad, "n_id", "c_des", c_dato, "N").ToString();
            FgLisOrdPro.SetData(FgLisOrdPro.Rows.Count - 1, 5, c_dato);                                  // PRIORIDAD DE LA ORDEN DE PRODUCCION

            c_dato = entOrdenProduccion.n_id.ToString();
            FgLisOrdPro.SetData(FgLisOrdPro.Rows.Count - 1, 6, c_dato);                                  // ID DE LA ORDEN DE PRODUCCION
        }
        void BuscarRecetaInsPrincipal(int n_IdProducto, int n_IdReceta, int n_IdLinea, Double n_CantidadProducir, ref int n_IdProductoPrincipal, ref double n_CanProductoPrincipal)
        {
            int n_row = 0;
            int n_index = 0;

            CN_pro_productos objPro = new CN_pro_productos();

            List<BE_PRO_PRODUCTOSRECETAS> lstRecetas = new List<BE_PRO_PRODUCTOSRECETAS>();
            List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstRecetasIns = new List<BE_PRO_PRODUCTOSRECETASINSUMOS>();
            List<BE_PRO_PRODUCTOSRECETASLINEAS> lstLineas = new List<BE_PRO_PRODUCTOSRECETASLINEAS>();
            List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS> lstLineasTar = new List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS>();

            objPro.mysConec = mysConec;
            // TRAEMOS LOS DATOS DE LA RECETA
            if (objPro.TraerRegistro(n_IdProducto) == true)
            {
                lstRecetas = objPro.lstRecetas;                                      // CARGAMOS LAS RECETAS DEL PRODUCTO
                lstLineas = objPro.lstLineas;                                        // CARGAMOS LAS LINEAS DEL PRODUCTO
                lstRecetasIns = objPro.lstRecetasIns;                                // CARGAMOS LA LISTA DE INSUMOS DE LA RECETA
                lstLineasTar = objPro.lstLineasTar;                                  // CARGAMOS LAS TAREAS DE LAS RECETAS
            }

            // OBTENEMOS EL INDICE DE LA LINEA
            for (n_row = 0; n_row <= lstLineas.Count - 1; n_row++)
            {
                if (lstLineas[n_row].n_id == n_IdLinea)
                {
                    n_index = n_row;
                    break;
                }
            }

            // BUSCAMOS EL ID DEL INSUMO PRINCIPAL EN LA RECETA
            for (n_row = 0; n_row <= lstRecetasIns.Count - 1; n_row++)
            {
                if (lstRecetasIns[n_row].n_idrec == n_IdReceta)
                {
                    if (lstRecetasIns[n_row].n_inspri == 1)
                    {
                        n_IdProductoPrincipal = lstRecetasIns[n_row].n_idite;
                        n_CanProductoPrincipal = (lstRecetasIns[n_row].n_can * n_CantidadProducir);
                        break;
                    }
                }
            }
        }
        private void FgLisOrdPro_RowColChange(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (FgLisOrdPro.Rows.Count == 2) { return; }

            booAgregando = true;
            MostrarProductosOrdenProduccion(Convert.ToInt32(FgLisOrdPro.GetData(FgLisOrdPro.Row, 6)));
            booAgregando = false;
        }
        private void CmdProcesar_Click(object sender, EventArgs e)
        {

            if (TxtFchIni.Text == " ")
            {
                MessageBox.Show("¡ No ha especificado la fecha de inicio !","Cronograma Produccion", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }
            if (TxtFchFin.Text == " ")
            {
                MessageBox.Show("¡ No ha especificado la fecha de termino !", "Cronograma Produccion", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                TxtFchFin.Focus();
                return;
            }

            if (FgLisOrdPro.Rows.Count == 2)
            {
                MessageBox.Show("¡ No ha especificado las ordenes de produccion a programar !", "Cronograma Produccion", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                FgLisOrdPro.Focus();
                return;
            }
            if (lstProgramadetpro.Count == 0)
            {
                MessageBox.Show("¡ No hay productos a programar, asegurese que las ordenes de compra selecciondas cuente al menos con un producto !", "Cronograma Produccion", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                FgLisOrdPro.Focus();
                return;
            }

            int n_row = 0;
            string c_dato = "";
            for (n_row = 0; n_row <= lstProgramadetpro.Count - 1; n_row++)
            {
                if (lstProgramadetpro[n_row].d_fchent < Convert.ToDateTime(TxtFchIni.Text))
                {
                    Tab2.SelectedIndex = 0;
                    c_dato = lstProgramadetpro[n_row].n_idite.ToString("");
                    c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", c_dato, "N").ToString();

                    MessageBox.Show("¡ la fecha de entrega del item : " + c_dato + ", no esta en el rango del cronograma de produccion, asignele una fceha dentro del rango del cronograma de produccion", "Cronograma Produccion", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    FgLisPro.Focus();
                    return;
                }
            }
            VerCronograma();
        }
        void VerCronograma()
        {
            int n_row = 0;
            int n_col = 0;
            string c_dato = "";
            double n_cantidad = 0;
            double n_stock = 0;
            double n_canpro = 0;
            int n_numdiapro = 0;
            DateTime d_fecha;
            int n_rowbus = 0;
            bool b_SeEncontro = false;
            int n_rowactual = 0;
            double n_canact = 0;

            FgCrono.Rows.Count = 2;

            for (n_row = 0; n_row <= lstProgramadetpro.Count - 1; n_row++)
            {
                b_SeEncontro = false;
                for (n_rowbus = 2; n_rowbus <= FgCrono.Rows.Count - 1; n_rowbus++)
                {
                    if (Convert.ToInt32(FgCrono.GetData(n_rowbus, 7)) == lstProgramadetpro[n_row].n_idite)
                    {
                        b_SeEncontro = true;
                        n_rowactual = n_rowbus;
                        break;
                    }
                }
                if (b_SeEncontro == false)
                {
                    FgCrono.Rows.Count = FgCrono.Rows.Count + 1;
                    n_rowactual = FgCrono.Rows.Count - 1;
                }

                c_dato = lstProgramadetpro[n_row].n_idite.ToString("");
                c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", c_dato, "N").ToString();
                FgCrono.SetData(n_rowactual, 1, c_dato);                                           // DESCRIPCION DEL PRODUCTO

                c_dato = lstProgramadetpro[n_row].n_idunimed.ToString("");
                c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_despre", c_dato, "N").ToString();
                FgCrono.SetData(n_rowactual, 2, c_dato);                                          // UNIDAD DE MEDIDA

                c_dato = lstProgramadetpro[n_row].d_fchent.ToString();
                FgCrono.SetData(n_rowactual, 3, c_dato);                                          // FECHA DE ENTREGA

                n_stock = Convert.ToDouble(funDatos.DataTableBuscar(dtStock, "n_id", "n_stock", lstProgramadetpro[n_row].n_idite.ToString(), "N"));
                FgCrono.SetData(n_rowactual, 4, n_stock.ToString("0.00"));                        // STOCK ACTUAL

                n_cantidad = Convert.ToDouble(FgCrono.GetData(n_rowactual, 5)) + lstProgramadetpro[n_row].n_can;

                FgCrono.SetData(n_rowactual, 5, n_cantidad.ToString("0.00"));                     // CANTIDAD REQUERIDA
                n_cantidad = lstProgramadetpro[n_row].n_can;

                n_canpro = Convert.ToDouble(FgCrono.GetData(n_rowactual, 5));
                n_canpro = (n_stock - n_canpro);
                FgCrono.SetData(n_rowactual, 6, n_canpro.ToString("0.00"));                       // CANTIDAD A PROCESAR

                c_dato = lstProgramadetpro[n_row].n_idite.ToString("");
                FgCrono.SetData(n_rowactual, 7, c_dato);                                          // ID DEL PRODUCTO

                c_dato = lstProgramadetpro[n_row].n_idrec.ToString("");
                FgCrono.SetData(n_rowactual, 8, c_dato);                                          // ID DE LA RECETA

                c_dato = lstProgramadetpro[n_row].n_idordpro.ToString("");
                FgCrono.SetData(n_rowactual, 9, c_dato);                                          // ID DE LA ORDEN DE PRODUCCION

                if (OptFchPro.Checked == true)
                {
                    d_fecha = Convert.ToDateTime(lstProgramadetpro[n_row].d_fchpro);
                }
                else
                {
                    d_fecha = Convert.ToDateTime(lstProgramadetpro[n_row].d_fchent);
                }
                c_dato = d_fecha.ToString("dd/MM/yyyy");

                n_numdiapro =  Convert.ToInt16(TxtNumDias.Text);

                CellRange rg;
                for (n_col = 10; n_col <= FgCrono.Cols.Count - 1; n_col++)
                {
                    if (funFunciones.NulosC(FgCrono.GetData(1, n_col)).ToString() == c_dato)
                    {
                        n_canact = Convert.ToDouble(funFunciones.NulosN(FgCrono.GetData(n_rowactual, n_col)));
                        FgCrono.Cols[n_col].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                        FgCrono.Cols[n_col].Format = "0.00";

                        n_canact = n_canact + n_cantidad;
                        FgCrono.SetData(n_rowactual, n_col, n_canact.ToString("0.00"));
                        rg = FgCrono.GetCellRange(n_rowactual, n_col, n_rowactual, n_col);

                        if (OptFchPro.Checked == true)
                        {
                            rg.StyleNew.BackColor = Color.Brown;
                            rg.StyleNew.ForeColor = Color.Yellow;
                        }
                        else
                        {
                            rg.StyleNew.BackColor = Color.Green;
                            rg.StyleNew.ForeColor = Color.White;
                        }
                        break;
                    }
                }

                BE_PRO_PROGRAMADETPROCRON entCrono = new BE_PRO_PROGRAMADETPROCRON();

                entCrono.n_idpro = 0;
                entCrono.n_idordpro = Convert.ToInt32(FgCrono.GetData(n_rowactual, 9).ToString());
                entCrono.n_idproducto = Convert.ToInt32(FgCrono.GetData(n_rowactual, 7).ToString());
                entCrono.d_fchpro = Convert.ToDateTime(c_dato).AddDays(-n_numdiapro);
                entCrono.d_fchent = Convert.ToDateTime(c_dato);
                entCrono.n_can = Convert.ToDouble(FgCrono.GetData(n_rowactual, 5).ToString());
                lstProgramadetprocron.Add(entCrono);
            }
            TraerIntermedios2();
            TraerInsumos2();
            MostrarPersonal();

            FgCrono.Cols.Frozen = 9;
            FgInt.Cols.Frozen = 7;
            FgIns.Cols.Frozen = 7;
        }
        int BuscarInterFlex(int n_IdInsumo, int n_IdUniMed)
        {
            int n_result = 0;
            int n_row = 0;
            int n_rowActual = 0;
            string c_Descripcion;
            string c_UniMedida;
            double n_valor = 0;

            for (n_row = 2; n_row <= FgInt.Rows.Count - 1; n_row++)             // BUSCAMOS LA FILA DEL INSUMO EN CASO DE QUE HAYA SIDO AGREGADO
            {
                if (Convert.ToInt32(FgInt.GetData(n_row, 7)) == n_IdInsumo)
                {
                    n_rowActual = n_row;
                    break;
                }
            }
            if (n_rowActual == 0)                                              // SI NO SE HA ENCONTRADO EL ITEM EN LA LISTA 
            {
                // AGREGAMOS UNA FILA PARA AGREGAR EL ITEMS
                FgInt.Rows.Count = FgInt.Rows.Count + 1;
                n_result = FgInt.Rows.Count - 1;

                c_Descripcion = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", n_IdInsumo.ToString(), "N").ToString();
                c_UniMedida = funDatos.DataTableBuscar(dtUniMedSunat, "n_id", "c_abr", n_IdUniMed.ToString(), "N").ToString();

                FgInt.SetData(n_result, 1, c_Descripcion);
                FgInt.SetData(n_result, 2, c_UniMedida);

                n_valor = Convert.ToDouble(funDatos.DataTableBuscar(dtStock, "n_id", "n_stock", n_IdInsumo.ToString(), "N"));
                FgInt.SetData(n_result, 4, n_valor.ToString("0.00"));                                                              // STOCK ACTUAL
                FgInt.SetData(n_result, 7, n_IdInsumo.ToString());
            }
            else
            {
                // OBTENEMOS LA FILA DONDE SE ENCUENTRA EL ITEM
                n_result = n_rowActual;
            }
            return n_result;
        }
        int BuscarInsumoFlex(int n_IdInsumo, int n_IdUniMed)
        {
            int n_result = 0;
            int n_row = 0;
            int n_rowActual = 0;
            string c_Descripcion;
            string c_UniMedida;
            double n_valor = 0;

            for (n_row = 2; n_row <= FgIns.Rows.Count - 1; n_row++)            // BUSCAMOS LA FILA DEL INSUMO EN CASO DE QUE HAYA SIDO AGREGADO
            {
                if (Convert.ToInt32(FgIns.GetData(n_row, 7)) == n_IdInsumo)
                {
                    n_rowActual = n_row;
                    break;
                }
            }
            if (n_rowActual == 0)                                              // SI NO SE HA ENCONTRADO EL ITEM EN LA LISTA 
            {
                // AGREGAMOS UNA FILA PARA AGREGAR EL ITEMS
                FgIns.Rows.Count = FgIns.Rows.Count + 1;
                n_result = FgIns.Rows.Count - 1;

                c_Descripcion = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", n_IdInsumo.ToString(), "N").ToString();
                c_UniMedida = funDatos.DataTableBuscar(dtUniMedSunat, "n_id", "c_abr", n_IdUniMed.ToString(), "N").ToString();

                FgIns.SetData(n_result, 1, c_Descripcion);
                FgIns.SetData(n_result, 2, c_UniMedida);

                n_valor = Convert.ToDouble(funDatos.DataTableBuscar(dtStock, "n_id", "n_stock", n_IdInsumo.ToString(), "N"));
                FgIns.SetData(n_result, 4, n_valor.ToString("0.00"));                                                              // STOCK ACTUAL
                FgIns.SetData(n_result, 7, n_IdInsumo.ToString());
            }
            else
            {
                // OBTENEMOS LA FILA DONDE SE ENCUENTRA EL ITEM
                n_result = n_rowActual;
            }
            return n_result;
        }
        void MostrarPersonal()
        {
            int n_row = 0;
            int n_col = 0;
            double n_canhor = 0;
            int n_idproducto = 0;
            int n_numdias = 26;
            double n_canreq = 0;
            double n_numper = 0;
            int n_numhordia = Convert.ToInt32(TxtNumHorDia.Text);
            booAgregando = true;  
            FgPer.Rows.Count = 2;
                  
            for (n_row = 2; n_row <= FgInt.Rows.Count - 1; n_row++)
            {
                FgPer.Rows.Count = FgPer.Rows.Count + 1;
                FgPer.SetData(FgPer.Rows.Count - 1, 1, FgInt.GetData(n_row, 1).ToString());
                FgPer.SetData(FgPer.Rows.Count - 1, 2, FgInt.GetData(n_row, 2).ToString());


                FgPer.SetData(FgPer.Rows.Count - 1, 4, FgInt.GetData(n_row, 5).ToString());             // CANTIDAD REQUERIDA
                FgPer.SetData(FgPer.Rows.Count - 1, 7, FgInt.GetData(n_row, 7).ToString());

                n_idproducto = Convert.ToInt32(FgPer.GetData(FgPer.Rows.Count - 1, 7));

                n_canreq = Convert.ToDouble(FgPer.GetData(FgPer.Rows.Count - 1, 4));
                n_canhor = Convert.ToDouble(funDatos.DataTableBuscar(dtItems, "n_id", "n_kilhor", n_idproducto.ToString(), "N").ToString());
                
                FgPer.SetData(FgPer.Rows.Count - 1, 3, n_canhor.ToString("0.00"));
                FgPer.SetData(FgPer.Rows.Count - 1, 5, n_numdias.ToString("0"));
                FgPer.SetData(FgPer.Rows.Count - 1, 6, TxtFchIni.Text);

                if (n_canhor != 0)
                {
                    int n_colini = 0;
                    int n_dia = 1;
                    string c_dato;
                    DateTime d_dia = Convert.ToDateTime(TxtFchIni.Text);
                    n_numper = (((n_canreq / n_canhor) / n_numhordia) / n_numdias);

                    for (n_col = 9; n_col <= FgPer.Cols.Count - 1; n_col++)
                    {
                        c_dato = d_dia.ToString("dd/MM/yyyy");
                        if (FgPer.GetData(1, n_col).ToString().Trim() == c_dato.Trim())
                        {
                            n_colini = n_col;
                            break;
                        }
                    }

                    for (n_col = n_colini; n_col <= FgPer.Cols.Count - 1; n_col++)
                    {
                        c_dato = FgPer.GetData(1, n_col).ToString();
                        c_dato = Convert.ToDateTime(c_dato).ToString("dddd");
                        if (c_dato.Trim() !="domingo")
                            { 
                            FgPer.SetData(n_row, n_col, n_numper.ToString("0"));
                            n_dia = n_dia + 1;
                            if (n_dia > n_numdias)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            booAgregando = false;
        }
        void TraerIntermedios2()
        {
            int n_row = 0;
            int n_rowrec = 0;
            int n_idpro = 0;
            int n_idrec = 0;
            int n_idinsumo = 0;
            double n_canprod = 0;
            string c_dato = "";
            int n_idunimed = 0;
            double n_canins = 0;
            DataTable dtResul = new DataTable();

            FgIns.Rows.Count = 2;                                            // INICIALIZAMOS EL FLEX DE LOS ISNUMOS

            for (n_row = 2; n_row <= FgCrono.Rows.Count - 1; n_row++)        // RECORREMOS EL FLEX DEL CRONOGRAMA
            {
                n_idpro = Convert.ToInt32(FgCrono.GetData(n_row, 7));        // ID DEL PRODUCTO
                n_idrec = Convert.ToInt32(FgCrono.GetData(n_row, 8));        // ID DE LA RECETA

                dtResul = funDatos.DataTableFiltrar(dtInsumos, "(n_idpro = " + n_idpro + ")");              // FILTRAMOS LAS RECETAS DEL PRODUCTO
                dtResul = funDatos.DataTableFiltrar(dtResul, "(n_idrec = " + n_idrec + ")");                // FILTRAMOS LA RECETA DEL PRODUCTO

                if (dtResul.Rows.Count != 0)                                                                // SI EL PRODUCTO TIENE RECETAS
                {
                    for (n_rowrec = 0; n_rowrec <= dtResul.Rows.Count - 1; n_rowrec++)
                    {
                        n_idinsumo = Convert.ToInt32(dtResul.Rows[n_rowrec]["n_idite"].ToString());         // OBTENEMOS ID DEL INSUMO
                        n_idunimed = Convert.ToInt32(dtResul.Rows[n_rowrec]["n_idunimed"].ToString());      // OBTENEMOS ID DE LA UNIDAD DE MEDIDA
                        n_canins = Convert.ToDouble(dtResul.Rows[n_rowrec]["n_can"].ToString());            // OBTENEMOS ID DE LA UNIDAD DE MEDIDA
                        c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "n_idtipexi", n_idinsumo.ToString(), "N").ToString();   // TIPO DE INSUMO
                        //if (n_idinsumo == 458) 
                        //{
                        //    c_dato = "#";
                        //}
                        if (c_dato == "2")                                   //ESCRIBIMOS EL DATO EN EL FLEX INSUMOS
                        {
                            int n_filins = 0;
                            int n_col = 0;
                            int n_colins = 0;
                            double n_canpro = 0;
                            string c_fecha = "";
                            double n_totcel = 0;

                            n_filins = BuscarInterFlex(n_idinsumo, n_idunimed);                                           // OBTENEMOSL A FILA DE INSUMO EL EL FLEX DE INSUMOS

                            for (n_col = 10; n_col <= FgCrono.Cols.Count - 1; n_col++)
                            {
                                if (funFunciones.NulosC(FgCrono.GetData(n_row, n_col)) != "")
                                { 
                                    if (Convert.ToDouble(FgCrono.GetData(n_row, n_col)) != 0)
                                    {
                                        n_canprod = Convert.ToDouble(FgCrono.GetData(n_row, n_col));               // CANTIDAD A PRODUCIR DEL PRODUCTO
                                        c_fecha = FgCrono.GetData(1, n_col).ToString();                            // FECHA DE ENTREGA DEL PRODUCTO
                                        if (OptFchEnt.Checked == true)                                             // SI ESTA SELECCIONADO FECHA DE ENTREGA, BUSCAMOS LA FE HA DE PRODUCCION
                                        {
                                            c_fecha = ObtenerFechaProduccion(n_idpro, c_fecha, n_canprod);             // FECHA DE PRODUCCION
                                        }

                                        // RECORREMOS LAS COLUMNAS DE LOS INSUMOS Y BUSCAMOS LA FECHA QUE INDICA EL FLEX DEL CRONOGRAMA
                                        for (n_colins = 8; n_colins <= FgInt.Cols.Count - 1; n_colins++)
                                        {
                                            if (FgInt.GetData(1, n_colins).ToString() == c_fecha)    // SI ENCONTRAMOS LA FECHA
                                            {
                                                n_totcel = Convert.ToDouble(FgInt.GetData(n_filins, n_colins));
                                                n_canpro = (n_canins * n_canprod);
                                                FgInt.Cols[n_colins].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                                                FgInt.Cols[n_colins].Format = "0.00";
                                                FgInt.SetData(n_filins, n_colins, n_canpro.ToString("0.000000"));
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            double n_stock = 0;
            for (n_row = 2; n_row <= FgInt.Rows.Count - 1; n_row++)
            {
                // CALCULAMOS LA CANTIDAD A CONSUMIR DE ISNUMOS
                n_canins = funFlex.FlexSumarRow(FgInt, n_row, 9, FgInt.Cols.Count - 1);
                FgInt.SetData(n_row, 5, n_canins.ToString("0.00"));

                // CALCULAMOS LA DIFERENCIA A PRODUCIR
                n_stock = Convert.ToDouble(FgInt.GetData(n_row, 4));
                n_stock = (n_stock - n_canins);
                FgInt.SetData(n_row, 6, n_stock.ToString("0.00"));
            }
        }
        string ObtenerFechaProduccion(int n_IdItem, string c_FechaEntrega, double n_CantidadProducir)
        {
            int n_row =0;
            string c_FchPro ="";

            for (n_row = 0; n_row <= lstProgramadetpro.Count - 1; n_row++)
            {
                if ((lstProgramadetpro[n_row].n_idite == n_IdItem) && (lstProgramadetpro[n_row].d_fchent.ToString().Substring(0, 10) == c_FechaEntrega)
                    && (lstProgramadetpro[n_row].n_can == n_CantidadProducir))
                {
                    c_FchPro = lstProgramadetpro[n_row].d_fchpro.ToString().Substring(0, 10);
                    break;
                }
            }

            return c_FchPro;
        }
        void TraerInsumos2()
        {
            int n_row = 0;
            int n_rowrec = 0;
            int n_idpro = 0;
            int n_idrec = 0;
            int n_idinsumo = 0;
            double n_canprod = 0;
            string c_dato = "";
            int n_idunimed = 0;
            double n_canins = 0;
            DataTable dtResul = new DataTable();

            FgIns.Rows.Count = 2;                                            // INICIALIZAMOS EL FLEX DE LOS ISNUMOS

            for (n_row = 2; n_row <= FgCrono.Rows.Count - 1; n_row++)        // RECORREMOS EL FLEX DEL CRONOGRAMA
            {
                n_idpro = Convert.ToInt32(FgCrono.GetData(n_row, 7));        // ID DEL PRODUCTO
                n_idrec = Convert.ToInt32(FgCrono.GetData(n_row, 8));        // ID DE LA RECETA
                
                dtResul = funDatos.DataTableFiltrar(dtInsumos, "(n_idpro = " + n_idpro + ")");         // FILTRAMOS LAS RECETAS DEL PRODUCTO
                dtResul = funDatos.DataTableFiltrar(dtResul, "(n_idrec = " + n_idrec + ")");           // FILTRAMOS LA RECETA DEL PRODUCTO
                
                if (dtResul.Rows.Count != 0)                                                           // SI EL PRODUCTO TIENE RECETAS
                { 
                    for (n_rowrec = 0; n_rowrec <= dtResul.Rows.Count - 1; n_rowrec++) 
                    {
                        n_idinsumo = Convert.ToInt32(dtResul.Rows[n_rowrec]["n_idite"].ToString());        // OBTENEMOS ID DEL INSUMO
                        n_idunimed = Convert.ToInt32(dtResul.Rows[n_rowrec]["n_idunimed"].ToString());     // OBTENEMOS ID DE LA UNIDAD DE MEDIDA
                        n_canins = Convert.ToDouble(dtResul.Rows[n_rowrec]["n_can"].ToString());     // OBTENEMOS ID DE LA UNIDAD DE MEDIDA
                        c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "n_idtipexi", n_idinsumo.ToString(), "N").ToString();   // TIPO DE INSUMO
                        
                        if (c_dato != "2")              //ESCRIBIMOS EL DATO EN EL FLEX INSUMOS
                        {
                            int n_filins = 0;
                            int n_col = 0;
                            int n_colins = 0;
                            double n_canpro = 0;
                            string c_fecha ="";
                            double n_totcel = 0;

                            n_filins = BuscarInsumoFlex(n_idinsumo, n_idunimed);                                           // OBTENEMOSL A FILA DE INSUMO EL EL FLEX DE INSUMOS

                            for (n_col = 10; n_col <= FgCrono.Cols.Count - 1; n_col++)
                            {
                                if (funFunciones.NulosC(FgCrono.GetData(n_row, n_col)) != "PRODUCCION")
                                { 
                                    if (Convert.ToDouble(FgCrono.GetData(n_row, n_col)) != 0)
                                    {
                                        n_canprod = Convert.ToDouble(FgCrono.GetData(n_row, n_col));               // CANTIDAD A PRODUCIR DEL PRODUCTO
                                        c_fecha = FgCrono.GetData(1, n_col).ToString();                            // FECHA DE ENTREGA DEL PRODUCTO
                                        if (OptFchEnt.Checked == true)                                             // SI ESTA SELECCIONADO FECHA DE ENTREGA, BUSCAMOS LA FE HA DE PRODUCCION
                                        {
                                            c_fecha = ObtenerFechaProduccion(n_idpro, c_fecha, n_canprod);             // FECHA DE PRODUCCION
                                        }

                                        // RECORREMOS LAS COLUMNAS DE LOS INSUMOS Y BUSCAMOS LA FECHA QUE INDICA EL FLEX DEL CRONOGRAMA
                                        for (n_colins = 8; n_colins <= FgIns.Cols.Count - 1; n_colins++)
                                        {
                                            if (FgIns.GetData(1,n_colins).ToString() == c_fecha)    // SI ENCONTRAMOS LA FECHA
                                            {
                                                n_totcel = Convert.ToDouble(FgIns.GetData(n_filins, n_colins));
                                                n_canpro = ((n_canins * n_canprod)+n_totcel);
                                                FgIns.Cols[n_colins].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                                                FgIns.Cols[n_colins].Format = "0.00";
                                                FgIns.SetData(n_filins, n_colins, n_canpro.ToString("0.000000"));
                                                
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            double n_stock = 0;
            for (n_row = 2; n_row <= FgIns.Rows.Count - 1; n_row++)
            {
                // CALCULAMOS LA CANTIDAD A CONSUMIR DE ISNUMOS
                n_canins = funFlex.FlexSumarRow(FgIns, n_row, 9, FgIns.Cols.Count - 1);
                FgIns.SetData(n_row, 5, n_canins.ToString("0.00"));

                // CALCULAMOS LA DIFERENCIA A PRODUCIR
                n_stock = Convert.ToDouble(FgIns.GetData(n_row, 4));
                n_stock = (n_stock - n_canins);
                FgIns.SetData(n_row, 6, n_stock.ToString("0.00"));
            }
        }
        private void CmdRep_Click(object sender, EventArgs e)
        {
            if (FgCrono.Col <= 7) 
            {
                MessageBox.Show("¡ La columna seleccionada no es valida !", "Cronograma Produccion", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return; 
            }
            DateTime dFchProduccion = Convert.ToDateTime(FgCrono.GetData(1, FgCrono.Col));
            DateTime dFchEntrega ;
            
            if (FgCrono.Rows.Count == 2) { return; }
            int n_col = 0;
            int n_numdiapro = 5;
            CellRange rg;

            for (n_col = 10; n_col <= FgCrono.Cols.Count - 1; n_col++)
            {
                if (funFunciones.NulosC(FgCrono.GetData(FgCrono.Row, n_col)) == "ENTREGA")
                {
                    dFchEntrega = Convert.ToDateTime(FgCrono.GetData(1, n_col));

                    if (dFchProduccion >= dFchEntrega)
                    {
                        MessageBox.Show("¡ La fecha de produccion no puede ser menor o igual a la fecha de entrega !", "Cronograma Produccion", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    FgCrono.SetData(FgCrono.Row, n_col - n_numdiapro, "");
                    rg = FgCrono.GetCellRange(FgCrono.Row, n_col - n_numdiapro, FgCrono.Row, n_col - n_numdiapro);
                    rg.StyleNew.BackColor = Color.Transparent;
                }
            }

            FgCrono.SetData(FgCrono.Row, FgCrono.Col, "PRODUCCION");

            rg = FgCrono.GetCellRange(FgCrono.Row, FgCrono.Col, FgCrono.Row, FgCrono.Col);
            rg.StyleNew.BackColor = Color.Brown;
            rg.StyleNew.ForeColor = Color.Yellow;
        }
        private void CmdExpIns_Click(object sender, EventArgs e)
        {
            if (FgIns.Rows.Count == 2)
            {
                MessageBox.Show("! No hay datos para exportar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-PRO-PROGRAMA-2-" + DateTime.Now.ToString("ddMMyyyy") + ".xls";
            string c_titu2 = "FECHA INICIO  : " + TxtFchIni.Text + "        FECHA FINAL : " + TxtFchFin.Text;

            funFlex.ExportToExcel(FgIns, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "PROGRAMA PRODUCCION - INSUMOS REQUERIDOS", c_titu2, c_NomArchivo);
        }
        private void CmdExpInt_Click(object sender, EventArgs e)
        {
            if (FgInt.Rows.Count == 2)
            {
                MessageBox.Show("! No hay datos para exportar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-PRO-PROGRAMA-3-" + DateTime.Now.ToString("ddMMyyyy") + ".xls";
            string c_titu2 = "FECHA INICIO  : " + TxtFchIni.Text + "        FECHA FINAL : " + TxtFchFin.Text;

            funFlex.ExportToExcel(FgInt, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "PROGRAMA PRODUCCION - INTERMEDIOS REQUERIDOS", c_titu2, c_NomArchivo);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (FgCrono.Rows.Count == 2)
            {
                MessageBox.Show("! No hay datos para exportar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            string c_titu2 = "";
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-PRO-PROGRAMA-1-" + DateTime.Now.ToString("ddMMyyyy") + ".xls";

            if (OptFchPro.Checked == true)
            {
                c_titu2 = "FECHA INICIO  : " + TxtFchIni.Text + "        FECHA FINAL : " + TxtFchFin.Text + "    (FECHA DE PRODUCCION)";
            }
            else
            {
                c_titu2 = "FECHA INICIO  : " + TxtFchIni.Text + "        FECHA FINAL : " + TxtFchFin.Text + "    (FECHA DE ENTREGA)";
            }

            funFlex.ExportToExcel(FgCrono, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "PROGRAMA PRODUCCION - CRONOGRAMA PRODUCCION", c_titu2, c_NomArchivo);
        }
        private void CmdVerRec_Click(object sender, EventArgs e)
        {
            if (FgLisPro.Rows.Count == 2)
            {
                MessageBox.Show("! No se han encontrado productos ¡ Debe de agregar uno como minimo !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            else
            {
                int n_idRec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 7).ToString());
                int n_idPro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 9).ToString());

                SSF_NET_Produccion.Cls_Funciones funPro = new SSF_NET_Produccion.Cls_Funciones();
                funPro.dtItems = dtItems;
                funPro.dtTipExi = dtTipExi;
                funPro.dtUniMedSunat = dtUniMedSunat;
                funPro.mysConec = mysConec;
                funPro.VerReceta(n_idPro, n_idRec);
            }
        }
        private void CmdCalcLin_Click(object sender, EventArgs e)
        {

        }
        private void CmdVerRec2_Click(object sender, EventArgs e)
        {
            if (FgCrono.Rows.Count == 2)
            {
                MessageBox.Show("! No se han encontrado productos ¡ Debe de agregar uno como minimo !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            else
            {
                int n_idPro = Convert.ToInt32(FgCrono.GetData(FgCrono.Row, 7).ToString());
                int n_idRec = Convert.ToInt32(FgCrono.GetData(FgCrono.Row, 8).ToString());

                //VerReceta(n_idPro, n_idRec);
                SSF_NET_Produccion.Cls_Funciones funPro = new SSF_NET_Produccion.Cls_Funciones();
                funPro.dtItems = dtItems;
                funPro.dtTipExi = dtTipExi;
                funPro.dtUniMedSunat = dtUniMedSunat;
                funPro.mysConec = mysConec;
                funPro.VerReceta(n_idPro, n_idRec);
            }
        }
        private void CmdVerLin_Click(object sender, EventArgs e)
        {
            //if (FgLisPro.Rows.Count == 2)
            //{
            //    MessageBox.Show("! No hay productos para visualizar la linea!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    return;
            //}
            //if (Convert.ToString(FgLisPro.GetData(FgLisPro.Row, 7)) == "")
            //{
            //    MessageBox.Show("! No ha especificado la hora de inicio de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    return;
            //}

            //int n_idRec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 9).ToString());
            //int n_idLin = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 10).ToString());
            //int n_idPro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 11).ToString());
            //int n_idOrdPro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 16).ToString());
            //double n_canPro = Convert.ToDouble(FgLisPro.GetData(FgLisPro.Row, 5).ToString());

            //VerLineas(n_idOrdPro, n_idPro, n_idRec, n_idLin, true, n_canPro);
        }
        private void CmdExpReqPer_Click(object sender, EventArgs e)
        {
            if (FgPer.Rows.Count == 2) 
            {
                MessageBox.Show("! No hay datos para exportar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-PRO-PROGRAMA-4-" + DateTime.Now.ToString("ddMMyyyy") + ".xls";
            string c_titu2 = "FECHA INICIO  : " + TxtFchIni.Text + "        FECHA FINAL : " + TxtFchFin.Text;

            funFlex.ExportToExcel(FgPer, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "PROGRAMA PRODUCCION - PERSONAL REQUERIDOS", c_titu2, c_NomArchivo);
        }
        private void FgPer_CellChanged(object sender, RowColEventArgs e)
        {
            if (booAgregando == true)      { return; }
            if ((FgPer.Col == 5) || (FgPer.Col == 6))
            {
                int n_col = 0;
                string c_dato = "";
                int n_colini = 0;
                DateTime d_dia = Convert.ToDateTime(FgPer.GetData(FgPer.Row, 6).ToString().Trim()); ;
                double n_numper = 0;
                int n_dia = 0;
                int n_numdias = Convert.ToInt32( FgPer.GetData(FgPer.Row, 5));
                int n_numhordia = Convert.ToInt32(TxtNumHorDia.Text);
                double n_canhor = Convert.ToDouble( FgPer.GetData(FgPer.Row, 3));
                double n_canreq = Convert.ToDouble(FgPer.GetData(FgPer.Row, 4));

                n_numper = (((n_canreq / n_canhor) / n_numhordia) / n_numdias);

                booAgregando = true;
                for (n_col = 9; n_col <= FgPer.Cols.Count - 1; n_col++)
                {
                    FgPer.SetData(FgPer.Row, n_col, "");
                }
                booAgregando = false;
                for (n_col = 9; n_col <= FgPer.Cols.Count - 1; n_col++)
                {
                    c_dato = d_dia.ToString("dd/MM/yyyy");
                    if (FgPer.GetData(1, n_col).ToString().Trim() == c_dato.Trim())
                    {
                        n_colini = n_col;
                        break;
                    }
                }

                c_dato = "";
                booAgregando = true;
                for (n_col = n_colini; n_col <= FgPer.Cols.Count - 1; n_col++)
                {
                    c_dato = FgPer.GetData(1, n_col).ToString();
                    c_dato = Convert.ToDateTime(c_dato).ToString("dddd");
                    if (c_dato.Trim() != "domingo")
                    {
                        
                        FgPer.SetData(FgPer.Row, n_col, n_numper.ToString("0"));
                        

                        n_dia = n_dia + 1;
                        if (n_dia >= n_numdias)
                        {
                            break;
                        }
                    }
                }
                booAgregando = false;
            }
        }
        private void FgPer_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }
            
            if (FgPer.GetData(FgPer.Row,3).ToString().Trim() == "0.00")
            {
                FgPer.AllowEditing = false;
                return;
            }
            if ((FgPer.Col == 5) || (FgPer.Col == 6))
            {
                FgPer.AllowEditing = true;
            }
            else 
            {
                FgPer.AllowEditing = false;
            }
        }
        private void FgPer_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            if ((FgPer.Col == 5) || (FgPer.Col == 6))
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void CboMeses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            bool b_Result = false;
            objRegistro.mysConec = mysConec;

            b_Result = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue));
            if (b_Result == true)
            {
                STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
                dtListar = objRegistro.dtListar;
                ListarItems();

                if (dtListar.Rows.Count == 0)
                {
                    DialogResult Rpta = MessageBox.Show("No se han encontrado registros, ¿ Desea agregar uno ahora ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (DialogResult.Yes == Rpta)
                    {
                        Nuevo();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    DgLista.Focus();
                }
            }
        }
        private void OptFchPro_CheckedChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (OptFchPro.Checked == false) { return; }
            MostrarProductosOrdenProduccion(Convert.ToInt32(lstProgramadet[0].n_idordpro));
            booAgregando = false;
            CreaCronograma();
            VerCronograma();
            
            TraerIntermedios2();
            TraerInsumos2();
            MostrarPersonal();

            FgCrono.Cols.Frozen = 9;
            FgInt.Cols.Frozen = 7;
            FgIns.Cols.Frozen = 7;
        }
        private void OptFchEnt_CheckedChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (OptFchEnt.Checked == false) { return; }
            MostrarProductosOrdenProduccion(Convert.ToInt32(lstProgramadet[0].n_idordpro));
            booAgregando = false;
            CreaCronograma();
            VerCronograma();
            
            TraerIntermedios2();
            TraerInsumos2();
            MostrarPersonal();

            FgCrono.Cols.Frozen = 9;
            FgInt.Cols.Frozen = 7;
            FgIns.Cols.Frozen = 7;
        }
        private void TxtNumSer_TextChanged(object sender, EventArgs e)
        {

        }
        private void TxtNumDias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void FgLisPro_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
        }

        private void FgLisPro_CellChanged(object sender, RowColEventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }
            int n_row = 0;

            if (FgLisPro.Col == 5)
            {
                int n_IdItem = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 9).ToString());
                int n_IdOrdPro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 11).ToString());
                int n_IdPro = entPrograma.n_id;
                int n_IdRec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 7).ToString());
                int n_IdLin = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 8).ToString());
                //double n_can = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 5).ToString());
                double n_Cantidad = Convert.ToDouble(funFunciones.NulosN(FgLisPro.GetData(FgLisPro.Row, 5)));
                string c_fchent = "";
                string c_fchpro = funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row, 12)).Substring(0,10);

                if (funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row, 13)) != "")
                {
                    c_fchent = funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row, 13).ToString()).Substring(0, 10);
                }

                for (n_row = 0; n_row <= lstProgramadetpro.Count - 1; n_row++)
                {
                    //if ((lstProgramadetpro[n_row].n_idite == n_IdItem) && (lstProgramadetpro[n_row].n_idrec == n_IdRec) &&
                    //   (lstProgramadetpro[n_row].n_idlin == n_IdLin) && (lstProgramadetpro[n_row].n_can == n_Cantidad) && (Convert.ToString(lstProgramadetpro[n_row].d_fchent).Substring(0, 10) == c_fchent))
                    //if ((lstProgramadetpro[n_row].n_idite == n_IdItem) && (lstProgramadetpro[n_row].n_idrec == n_IdRec) &&
                    //   (lstProgramadetpro[n_row].n_idlin == n_IdLin) && (Convert.ToString(lstProgramadetpro[n_row].d_fchent).Substring(0, 10) == c_fchent))
                    
                    if ((lstProgramadetpro[n_row].n_idite == n_IdItem) && (lstProgramadetpro[n_row].n_idrec == n_IdRec) &&
                        (lstProgramadetpro[n_row].n_idlin == n_IdLin) && (lstProgramadetpro[n_row].n_idordpro == n_IdOrdPro) &&
                        (Convert.ToString(lstProgramadetpro[n_row].d_fchpro).Substring(0, 10) == c_fchpro))
                    {
                        lstProgramadetpro[n_row].n_can = n_Cantidad;
                    }
                }
            }

            if (FgLisPro.Col == 6)
            {
                int n_numdias = Convert.ToInt32(TxtNumDias.Text);

                string c_newfchent = FgLisPro.GetData(FgLisPro.Row, 6).ToString();

                DateTime d_fecha = Convert.ToDateTime(c_newfchent);

                string c_newfchpro = d_fecha.AddDays(-n_numdias).ToString("dd/MM/yyyy"); //FgLisPro.GetData(FgLisPro.Row, 12).ToString();

                string c_fchent = "";
                if (funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row, 13)) != "")
                {
                    c_fchent = funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row, 13).ToString()).Substring(0, 10);
                }
                int n_IdItem = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 9).ToString());
                int n_IdRec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 7).ToString());
                int n_IdLin = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 8).ToString());
                int n_IdOrdPro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 11).ToString());
                double n_Cantidad = Convert.ToDouble(funFunciones.NulosN(FgLisPro.GetData(FgLisPro.Row, 5)));
                int n_IdPro = entPrograma.n_id;

                for (n_row = 0; n_row <= lstProgramadetpro.Count - 1; n_row++)
                {
                    //if ((lstProgramadetpro[n_row].n_idite == n_IdItem) && (lstProgramadetpro[n_row].n_idrec == n_IdRec) &&
                    //    (lstProgramadetpro[n_row].n_idlin == n_IdLin) && (lstProgramadetpro[n_row].n_can == n_Cantidad) && (Convert.ToString(lstProgramadetpro[n_row].d_fchent).Substring(0, 10) == c_fchent))
                    if ((lstProgramadetpro[n_row].n_idite == n_IdItem) && (lstProgramadetpro[n_row].n_idrec == n_IdRec) &&
                        (lstProgramadetpro[n_row].n_idlin == n_IdLin) && (Convert.ToString(lstProgramadetpro[n_row].d_fchent).Substring(0, 10) == c_fchent) &&
                        (lstProgramadetpro[n_row].n_idordpro == n_IdOrdPro))
                    {
                        lstProgramadetpro[n_row].d_fchent = Convert.ToDateTime(c_newfchent);
                        lstProgramadetpro[n_row].d_fchpro = Convert.ToDateTime(c_newfchpro);

                        // actualizamos la nueva fecha de entrega
                        FgLisPro.SetData(FgLisPro.Row, 12, c_newfchpro);
                        FgLisPro.SetData(FgLisPro.Row, 13, c_newfchent);
                    }
                }
            }
            if (FgLisPro.Col == 12)
            {
                int n_IdItem = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 9).ToString());
                int n_IdOrdPro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 11).ToString());
                int n_IdPro = entPrograma.n_id;
                int n_IdRec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 7).ToString());
                int n_IdLin = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 8).ToString());
                string c_fchent = ""; //funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row, 13)).Substring(0, 10);
                string c_fchproold = funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row, 13)).Substring(0, 10);

                if (funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row, 12)) == "") {return;}
                string c_fchpro = funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row, 12)).Substring(0, 10);

                // BUSCAMOS SI EXISTE UNA PRODUCCION IGUAL EN LA MISMA FECHA
                for (n_row = 0; n_row <= lstProgramadetpro.Count - 1; n_row++)
                {
                    if ((lstProgramadetpro[n_row].n_idite == n_IdItem) && (lstProgramadetpro[n_row].n_idrec == n_IdRec) &&
                       (lstProgramadetpro[n_row].n_idlin == n_IdLin) && (lstProgramadetpro[n_row].n_idordpro == n_IdOrdPro) &&
                       (Convert.ToString(lstProgramadetpro[n_row].d_fchpro).Substring(0, 10) == c_fchpro))
                    {
                        MessageBox.Show("! Ya se ha programado una produccion de este producto para el dia  " + c_fchpro + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        c_fchpro = c_fchproold;
                        FgLisPro.SetData(FgLisPro.Row, 12, c_fchproold);
                        break;
                    }
                }
                for (n_row = 0; n_row <= lstProgramadetpro.Count - 1; n_row++)
                {
                    if ((lstProgramadetpro[n_row].n_idite == n_IdItem) && (lstProgramadetpro[n_row].n_idrec == n_IdRec) &&
                       (lstProgramadetpro[n_row].n_idlin == n_IdLin) && (lstProgramadetpro[n_row].n_idordpro == n_IdOrdPro) &&
                       (Convert.ToString(lstProgramadetpro[n_row].d_fchpro).Substring(0, 10) == c_fchproold))
                    {
                        lstProgramadetpro[n_row].d_fchpro = Convert.ToDateTime(c_fchpro);
                    }
                }
            }
            if (FgLisPro.Col == 14)
            {
                int n_IdItem = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 9).ToString());
                int n_IdOrdPro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 11).ToString());
                int n_IdPro = entPrograma.n_id;
                int n_IdRec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 7).ToString());
                int n_IdLin = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 8).ToString());
                double n_can = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 5).ToString());
                string c_fchent = "";
                if (funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row, 13)) != "")
                {
                    c_fchent = funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row, 13).ToString()).Substring(0, 10);
                }

                for (n_row = 0; n_row <= lstProgramadetpro.Count - 1; n_row++)
                {
                    //if ((lstProgramadetpro[n_row].n_idite == n_IdItem) && (lstProgramadetpro[n_row].n_idordpro == n_IdOrdPro) &&
                    //    (lstProgramadetpro[n_row].n_idpro == n_IdPro) && (Convert.ToString(lstProgramadetpro[n_row].d_fchent).Substring(0, 10) == c_fchent))
                    if ((lstProgramadetpro[n_row].n_idite == n_IdItem) && (lstProgramadetpro[n_row].n_idrec == n_IdRec) &&
                        (lstProgramadetpro[n_row].n_idlin == n_IdLin) && (Convert.ToString(lstProgramadetpro[n_row].d_fchent).Substring(0, 10) == c_fchent))                    
                    {
                        string c_res = FgLisPro.GetData(FgLisPro.Row, 14).ToString();
                        int n_idres = Convert.ToInt32(funDatos.DataTableBuscar(dtPerPro, "c_apenom", "n_id", c_res, "C"));
                        lstProgramadetpro[n_row].n_idres = n_idres;
                        break;
                    }
                }
            }
        }

        void EnviarCorreo(string c_nomArchivo, string[] c_Destinatarios, string c_Asunto, string c_cuerpo)
        {
            Helper.Correo miCorreo = new Helper.Correo();
            //string[] c_Destinatarios = { "lrincon@agro-vado.com", "supervisor01@hotmail.com" };
            string[] c_ListaArchivos = { @c_nomArchivo };

            miCorreo.c_AsuntoCorreo = c_Asunto;
            miCorreo.c_CorreoRecibeCopia = "sistemas@agro-vado.com";
            miCorreo.c_CuentaOrigen = "pcp@agro-vado.com";
            miCorreo.c_CuerpoCorreo = c_cuerpo;
            miCorreo.c_Destinatarios = c_Destinatarios;
            miCorreo.c_CuentaContraseña = "Agrovado087701";
            miCorreo.c_DominioCuenta = "mail.agro-vado.com";
            miCorreo.c_ListaArchivos = c_ListaArchivos;
            miCorreo.EnviarCorreo();
        }

        private void CmdEnvCorrPrograma_Click(object sender, EventArgs e)
        {
            if (FgCrono.Rows.Count == 2)
            {
                MessageBox.Show("! No hay datos para enviar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            button3_Click(sender, e);

            string c_NomArchivo = @"C:\ssf-net\"+STU_SISTEMA.EMPRESARUC + "-PRO-PROGRAMA-1-" + DateTime.Now.ToString("ddMMyyyy") + ".xls";
            string[] c_Destinatarios = { "compras02@agro-vado.com", "almacen2@agro-vado.com", "ventas@agro-vado.com", "almacen2@agro-vado.com", "almacen@agro-vado.com", "epollongo@hotmail.com" };
            string c_Asunto = "Programa de produccion del " + TxtFchIni.Text + " al " + TxtFchFin.Text;
            string c_Cuerpo = "Srs buenos dias, le envio el programa de produccion para el presente periodo, cualquier cambio comunicar a la brevedad posible" + "\r\n" +
                "Atentamente" + "\r\n" +
                " " + "\r\n" +
                CboRes.Text + "\r\n" +
                "PCP";

            EnviarCorreo(c_NomArchivo, c_Destinatarios, c_Asunto, c_Cuerpo);
        }

        private void CmdEnvCorrIns_Click(object sender, EventArgs e)
        {
            if (FgCrono.Rows.Count == 2)
            {
                MessageBox.Show("! No hay datos para enviar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            CmdExpIns_Click(sender, e);

            string c_NomArchivo = @"C:\ssf-net\" + STU_SISTEMA.EMPRESARUC + "-PRO-PROGRAMA-2-" + DateTime.Now.ToString("ddMMyyyy") + ".xls";
            string[] c_Destinatarios = { "compras02@agro-vado.com", "almacen2@agro-vado.com", "ventas@agro-vado.com", "almacen2@agro-vado.com", "almacen@agro-vado.com", "epollongo@hotmail.com" };
            string c_Asunto = "Programa de produccion del " + TxtFchIni.Text + " al " + TxtFchFin.Text + "  -   Requerimiento de insumos";
            string c_Cuerpo = "Srs buenos dias, le envio el requerimiento de insumos para la produccion del presente periodo, cualquier cambio comunicar a la brevedad posible" + "\r\n" +
                "Atentamente" + "\r\n" +
                " " + "\r\n" +
                 CboRes.Text + "\r\n" +
                "PCP";

            EnviarCorreo(c_NomArchivo, c_Destinatarios, c_Asunto, c_Cuerpo);
        }

        private void CmdEnvCorrInter_Click(object sender, EventArgs e)
        {
            if (FgCrono.Rows.Count == 2)
            {
                MessageBox.Show("! No hay datos para enviar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            CmdExpInt_Click(sender, e);

            string c_NomArchivo = @"C:\ssf-net\" + STU_SISTEMA.EMPRESARUC + "-PRO-PROGRAMA-3-" + DateTime.Now.ToString("ddMMyyyy") + ".xls";
            string[] c_Destinatarios = { "compras02@agro-vado.com", "almacen2@agro-vado.com", "ventas@agro-vado.com", "almacen2@agro-vado.com", "almacen@agro-vado.com", "epollongo@hotmail.com" };
            string c_Asunto = "Programa de produccion del " + TxtFchIni.Text + " al " + TxtFchFin.Text + "  -   Requerimiento de productos intermedios";
            string c_Cuerpo = "Srs buenos dias, le envio el requerimiento de productos intermedios para la produccion del presente periodo, cualquier cambio comunicar a la brevedad posible" + "\r\n" +
                "Atentamente" + "\r\n" +
                " " + "\r\n" +
                CboRes.Text + "\r\n" +
                "PCP";

            EnviarCorreo(c_NomArchivo, c_Destinatarios, c_Asunto, c_Cuerpo);
        }

        private void CmdEnvCorrPer_Click(object sender, EventArgs e)
        {
            if (FgCrono.Rows.Count == 2)
            {
                MessageBox.Show("! No hay datos para enviar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            CmdExpReqPer_Click(sender, e);

            string c_NomArchivo = @"C:\ssf-net\" + STU_SISTEMA.EMPRESARUC + "-PRO-PROGRAMA-4-" + DateTime.Now.ToString("ddMMyyyy") + ".xls";
            string[] c_Destinatarios = { "almacen2@agro-vado.com", "ventas@agro-vado.com", "almacen2@agro-vado.com", "almacen@agro-vado.com", "epollongo@hotmail.com" };
            string c_Asunto = "Programa de produccion del " + TxtFchIni.Text + " al " + TxtFchFin.Text + "  -   Requerimiento de personal";
            string c_Cuerpo = "Srs buenos dias, le envio el requerimiento de personal para la elaboracion de productos intermedios del presente periodo, cualquier cambio comunicar a la brevedad posible" + "\r\n" +
                "Atentamente" + "\r\n" +
                " " + "\r\n" +
                CboRes.Text + "\r\n" +
                "PCP";

            EnviarCorreo(c_NomArchivo, c_Destinatarios, c_Asunto, c_Cuerpo);
        }

        private void CmdDelPro_Click(object sender, EventArgs e)
        {
            if (FgLisPro.Rows.Count == 2) { return; }

            int n_row = 0;
            int n_IdItem = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 9).ToString());
            int n_IdOrdPro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 11).ToString());
            int n_idrec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 7).ToString());
            int n_idlin = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 8).ToString());
            int n_IdPro = entPrograma.n_id;
            double n_can = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 5).ToString());
            string c_fchent = "";
            string c_fchproold = Convert.ToDateTime(FgLisPro.GetData(FgLisPro.Row, 13)).ToString("dd/MM/yyyy");
            //if (funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row, 13)) != "")
            //{
            //    c_fchent = funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row, 13).ToString()).Substring(0, 10);
            //}

            for (n_row = 0; n_row <= lstProgramadetpro.Count - 1; n_row++)
            {
                if ((lstProgramadetpro[n_row].n_idite == n_IdItem) && (lstProgramadetpro[n_row].n_idordpro == n_IdOrdPro) &&
                    (lstProgramadetpro[n_row].n_idrec == n_idrec) && (lstProgramadetpro[n_row].n_idlin == n_idlin) &&
                    (lstProgramadetpro[n_row].d_fchpro == Convert.ToDateTime(c_fchproold)))
                    
                    //(Convert.ToString(lstProgramadetpro[n_row].d_fchent).Substring(0, 10) == c_fchent))
                {
                    //string c_res = FgLisPro.GetData(FgLisPro.Row, 14).ToString();
                    //int n_idres = Convert.ToInt32(funDatos.DataTableBuscar(dtPerPro, "c_apenom", "n_id", c_res, "C"));
                    //lstProgramadetpro[n_row].n_idres = n_idres;
                    //break;

                    lstProgramadetpro.RemoveAt(n_row);
                    break;
                }
            }

            FgLisPro.Rows.Remove(FgLisPro.Row);
        }

        private void CmdAddPro_Click(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row,1)) == "") { return; }

            FgLisPro.Rows.Count = FgLisPro.Rows.Count + 1;
        }

        private void FgLisPro_CellButtonClick(object sender, RowColEventArgs e)
        {
            if (FgLisPro.Col == 1)
            {
                BuscarProducto();
            }

            if (FgLisPro.Col == 2)
            {
            }

            if (FgLisPro.Col == 3)
            {
            }
        }
        void BuscarProducto()
        {
            string[,] arrCabeceraDg1 = new string[3, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();

            booAgregando = true;
            dtResult = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = 2");
            arrCabeceraDg1[0, 0] = "Producto";
            arrCabeceraDg1[0, 1] = "600";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_despro";

            arrCabeceraDg1[1, 0] = "Unidad Medida";
            arrCabeceraDg1[1, 1] = "80";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_abrpre";

            arrCabeceraDg1[2, 0] = "Id";
            arrCabeceraDg1[2, 1] = "0";
            arrCabeceraDg1[2, 2] = "N";
            arrCabeceraDg1[2, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "c_despro";
            xFun.Buscar_CadFiltro = "";
            xFun.Buscar_CampoOrden = "c_despro";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }

            FgLisPro.SetData(FgLisPro.Row, 1, dtResult.Rows[0]["c_despro"]);
            FgLisPro.SetData(FgLisPro.Row, 4, dtResult.Rows[0]["c_despre"]);

            DateTime d_fchpro = DateTime.Now;
            int n_idrec = 0;
            int n_idlin = 0;
            int n_idpro = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
            int n_idunimed = Convert.ToInt32(dtResult.Rows[0]["n_idunimed"]);

            FgLisPro.SetData(FgLisPro.Row, 9, dtResult.Rows[0]["n_id"]);
            FgLisPro.SetData(FgLisPro.Row, 6, DateTime.Now.ToString("dd/MM/yyyy"));              // FECHA DE ENTREGA

            FgLisPro.SetData(FgLisPro.Row, 13, DateTime.Now.ToString("dd/MM/yyyy"));             // FECHA DE ENTREGA
            
            FgLisPro.SetData(FgLisPro.Row, 12, d_fchpro.AddDays(Convert.ToInt32(TxtNumDias.Text)));             // FECHA DE PRODUCCION

            FgLisPro.SetData(FgLisPro.Row, 11, FgLisOrdPro.GetData(FgLisOrdPro.Row, 6));         // ORDEN DE PRODUCCION

            //BUSCAMOS LA RECETA DEL PRODUCTO
            dtResult = funDatos.DataTableFiltrar(dtRecetas, "n_idpro = " + n_idpro + " and n_prirec = 1");

            if (dtResult.Rows.Count != 0)
            {
                n_idrec = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                FgLisPro.SetData(FgLisPro.Row, 2, dtResult.Rows[0]["c_codrec"]);
                FgLisPro.SetData(FgLisPro.Row, 7, dtResult.Rows[0]["n_id"]);
            }
            //BUSCAMOS LA LINEA DEL PRODUCTO
            dtResult = funDatos.DataTableFiltrar(dtLineas, "n_idpro = " + n_idpro + " and n_idrec = "  +n_idrec +"");

            if (dtResult.Rows.Count != 0)
            {
                n_idlin = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                FgLisPro.SetData(FgLisPro.Row, 3, dtResult.Rows[0]["c_codlin"]);
                FgLisPro.SetData(FgLisPro.Row, 8, dtResult.Rows[0]["n_id"]);
            }

            BE_PRO_PROGRAMADETPRO entPro = new BE_PRO_PROGRAMADETPRO();
            
            entPro.n_idite = n_idpro;
            entPro.n_idrec = n_idrec;
            entPro.n_idlin = n_idlin;
            entPro.n_can = 0;
            entPro.n_idunimed = 0;
            entPro.n_idres = 0;
            entPro.d_fchent = DateTime.Now;
            entPro.d_fchpro = d_fchpro.AddDays(Convert.ToInt32(TxtNumDias.Text));
            entPro.h_horfin = "";
            entPro.h_horini = "";
            entPro.n_idordpro = Convert.ToInt32(FgLisOrdPro.GetData(FgLisOrdPro.Row, 6));
            entPro.n_can = 0;
            entPro.n_idpro = entPrograma.n_id;
            entPro.n_idunimed = n_idunimed;

            lstProgramadetpro.Add(entPro);

            booAgregando = false;
        }

        private void FgLisPro_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgLisPro.AllowEditing = false;
                return;
            }
            else
            {
                FgLisPro.AllowEditing = true;
            }

            if (FgLisPro.Col == 4)
            {
                FgLisPro.AllowEditing = false;
            }

            if (FgLisPro.Col == 14)
            {
                FgLisPro.AllowEditing = true;
                funFlex.FlexColumnaCombo(FgLisPro, dtPerPro, "c_apenom", 14);
                
            }
        }

        private void FgLisOrdPro_CellChanged(object sender, RowColEventArgs e)
        {

        }

        private void CmdVerOP_Click(object sender, EventArgs e)
        {

        }

        private void CmdDelOP_Click(object sender, EventArgs e)
        {
            if (FgLisOrdPro.Rows.Count == 2)
            {
                int n_row = 0;
                for (n_row = 0; n_row <= lstProgramadet.Count - 1; n_row++)
                { 

                }
                FgLisOrdPro.RemoveItem(FgLisOrdPro.Row);
            }
        }
    }
}
