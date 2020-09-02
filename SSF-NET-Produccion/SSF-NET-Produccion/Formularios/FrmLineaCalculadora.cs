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

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmLineaCalculadora : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public OleDbConnection AccConec = new OleDbConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        BE_PRO_LINEA2017 entLinea = new BE_PRO_LINEA2017();
        List<BE_PRO_LINEADET2017> lstLineaDet = new List<BE_PRO_LINEADET2017>();

        // OBJETOS LOCALES
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_pro_lineas2017 objRegistro = new CN_pro_lineas2017();
        CN_pro_tareas objTareas = new CN_pro_tareas();
        CN_sun_unimed objUniMed = new CN_sun_unimed();
        //CN_pro_receta objReceta = new CN_pro_receta();


        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // DATATABLE LOCALES
        DataTable dtForm = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtListar = new DataTable();
        DataTable dtLineas = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtTarea = new DataTable();

        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        bool booAgregando = false;
        string[,] arrCabeceraDg1 = new string[10, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlexTar = new string[15, 5];

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;

        public FrmLineaCalculadora()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void FrmLineaCalculadora_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            //Tab1.SelectedIndex = 0;
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboPro, dtItems, "n_idpro", "c_despro");
        }
        void ConfigurarFormulario()
        {
            this.Height = 500;
            this.Width = 1042;

            Sizer1.Left = 1;
            Sizer1.Top = 42;
           
            //Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            //Tab_Posicionar(Tab1, 1, 42);
            //Tab1.SelectedIndex = 0;
            
            // FLEX GRID DE LAS TAREAS
            arrCabeceraFlexTar[0, 0] = "Tarea";
            arrCabeceraFlexTar[0, 1] = "200";
            arrCabeceraFlexTar[0, 2] = "C";
            arrCabeceraFlexTar[0, 3] = "";
            arrCabeceraFlexTar[0, 4] = "";

            arrCabeceraFlexTar[1, 0] = "Cantidad KLG.";
            arrCabeceraFlexTar[1, 1] = "60";
            arrCabeceraFlexTar[1, 2] = "N";
            arrCabeceraFlexTar[1, 3] = "";
            arrCabeceraFlexTar[1, 4] = "";

            arrCabeceraFlexTar[2, 0] = "Distorcion Maquina";
            arrCabeceraFlexTar[2, 1] = "60";
            arrCabeceraFlexTar[2, 2] = "N";
            arrCabeceraFlexTar[2, 3] = "";
            arrCabeceraFlexTar[2, 4] = "";

            arrCabeceraFlexTar[3, 0] = "Personal";
            arrCabeceraFlexTar[3, 1] = "60";
            arrCabeceraFlexTar[3, 2] = "N";
            arrCabeceraFlexTar[3, 3] = "0.00";
            arrCabeceraFlexTar[3, 4] = "";

            arrCabeceraFlexTar[4, 0] = "Tiempo";
            arrCabeceraFlexTar[4, 1] = "60";
            arrCabeceraFlexTar[4, 2] = "H";
            arrCabeceraFlexTar[4, 3] = "0";
            arrCabeceraFlexTar[4, 4] = "";

            arrCabeceraFlexTar[5, 0] = "KLG x Persona";
            arrCabeceraFlexTar[5, 1] = "60";
            arrCabeceraFlexTar[5, 2] = "N";
            arrCabeceraFlexTar[5, 3] = "";
            arrCabeceraFlexTar[5, 4] = "";

            arrCabeceraFlexTar[6, 0] = "Operarios Aparente";
            arrCabeceraFlexTar[6, 1] = "60";
            arrCabeceraFlexTar[6, 2] = "N";
            arrCabeceraFlexTar[6, 3] = "0";
            arrCabeceraFlexTar[6, 4] = "";

            arrCabeceraFlexTar[7, 0] = "Formula";
            arrCabeceraFlexTar[7, 1] = "60";
            arrCabeceraFlexTar[7, 2] = "N";
            arrCabeceraFlexTar[7, 3] = "0";
            arrCabeceraFlexTar[7, 4] = "";

            arrCabeceraFlexTar[8, 0] = "Operarios Reales";
            arrCabeceraFlexTar[8, 1] = "60";
            arrCabeceraFlexTar[8, 2] = "N";
            arrCabeceraFlexTar[8, 3] = "0";
            arrCabeceraFlexTar[8, 4] = "";

            arrCabeceraFlexTar[9, 0] = "Eficiencia Unitaria";
            arrCabeceraFlexTar[9, 1] = "60";
            arrCabeceraFlexTar[9, 2] = "N";
            arrCabeceraFlexTar[9, 3] = "0";
            arrCabeceraFlexTar[9, 4] = "";

            arrCabeceraFlexTar[10, 0] = "Eficiencia Total";
            arrCabeceraFlexTar[10, 1] = "60";
            arrCabeceraFlexTar[10, 2] = "N";
            arrCabeceraFlexTar[10, 3] = "0";
            arrCabeceraFlexTar[10, 4] = "";

            arrCabeceraFlexTar[11, 0] = "KG / HR";
            arrCabeceraFlexTar[11, 1] = "60";
            arrCabeceraFlexTar[11, 2] = "N";
            arrCabeceraFlexTar[11, 3] = "0";
            arrCabeceraFlexTar[11, 4] = "";

            arrCabeceraFlexTar[12, 0] = "COSTO EN LINEA S/F";
            arrCabeceraFlexTar[12, 1] = "60";
            arrCabeceraFlexTar[12, 2] = "N";
            arrCabeceraFlexTar[12, 3] = "0";
            arrCabeceraFlexTar[12, 4] = "";

            arrCabeceraFlexTar[13, 0] = "PORCENTAJE";
            arrCabeceraFlexTar[13, 1] = "0";
            arrCabeceraFlexTar[13, 2] = "N";
            arrCabeceraFlexTar[13, 3] = "0";
            arrCabeceraFlexTar[13, 4] = "";

            funFlex.FlexMostrarDatos(FgTarea, arrCabeceraFlexTar, dtListar, 2, false);

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            //CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;

            //objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            //objFormVis.ObtenerCabeceraLista(32, ref arrCabeceraDg1);
        }
        void DataTableCargar()
        {
            bool b_Result;

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(33);                             // INDICAMOS QUE ES EL FORMULARIO MAESTRO DE RECETAS

            //objMeses.mysConec = mysConec;
            //dtMeses = objMeses.Listar();

            objRegistro.mysConec = mysConec;
            b_Result = objRegistro.ListarProductosLinea(STU_SISTEMA.EMPRESAID);
            if (b_Result == true) 
            { 
                dtItems = objRegistro.dtProductos;
            }
            b_Result = objRegistro.ListarLineas (STU_SISTEMA.EMPRESAID);
            if (b_Result == true)
            {
                dtLineas = objRegistro.dtLineas;
            }

            objTareas.mysConec = mysConec;
            dtTarea = objTareas.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
        }
        void Tab_Dimensionar(C1.Win.C1Command.C1DockingTab dokTab, int intAlto, int intAncho)
        {
            Sizer1.Height = intAlto;
            Sizer1.Width = intAncho;
        }
        void Tab_Posicionar(C1.Win.C1Command.C1DockingTab dokTab, int intPosX, int intPosY)
        {
            Sizer1.Left = intPosX;
            Sizer1.Top = intPosY;
        }

        private void CboLin_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (Convert.ToInt32(CboLin.SelectedValue) == 0) { return; }

            lstLineaDet.Clear();
            objRegistro.TraerRegistro(Convert.ToInt32(CboLin.SelectedValue));
            entLinea = objRegistro.entLinea;
            lstLineaDet = objRegistro.lstLineDet;

            VerLinea(Convert.ToInt32(CboLin.SelectedValue), 0);
        }

        private void CboPro_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (Convert.ToInt32(CboPro.SelectedValue) == 0) { return; }
            DataTable dtResult = new DataTable();
            booAgregando = true;
            dtResult = funDatos.DataTableFiltrar(dtLineas, "n_idpro = " + Convert.ToInt32(CboPro.SelectedValue) + "");
            if (dtResult.Rows.Count != 0)
            {
                funDatos.ComboBoxCargarDataTable(CboLin, dtResult, "n_id", "c_deslin");
            }
            booAgregando = false;
        }

        private void Sizer1_Click(object sender, EventArgs e)
        {

        }
        void VerLinea(int n_idLinea, double n_CantidadProducir)
        {
            int n_row = 0;
            string c_dato = "";
            //double n_dato = 0;
            double n_por = 0;
            int n_fila = 2;
            double n_cankil = 0;
            double n_numperapa = 0;
            double n_formula = 0;
            double n_opereal = 0;
            double n_efiuni = 0;
            double n_efitot = 0;
            double n_costar = 0;

            FgTarea.Rows.Count = 2;

            if (n_CantidadProducir == 0)
            {
                TxtCanPro.Text = entLinea.n_can.ToString("0.00");
            }
            else
            {
                TxtCanPro.Text = n_CantidadProducir.ToString("0.00");
            }
            TxtEfiTot.Text = entLinea.n_efi.ToString("0.00");
            TxtNumOpe.Text = entLinea.n_numope.ToString("00");
            TxtPreLin.Text = entLinea.n_prelin.ToString("0.00");

            if (lstLineaDet.Count != 0)
            {
                for (n_row = 0; n_row <= lstLineaDet.Count - 1; n_row++)
                {
                    FgTarea.Rows.Count = FgTarea.Rows.Count + 1;
                    
                    c_dato = lstLineaDet[n_row].n_idtar.ToString();
                    c_dato = funDatos.DataTableBuscar(dtTarea, "n_id", "c_des", c_dato, "N").ToString();
                    FgTarea.SetData(n_fila, 1, c_dato);

                    n_por = (lstLineaDet[n_row].n_porefi / 100);
                    n_cankil = (Convert.ToDouble(TxtCanPro.Text) * n_por);
                    FgTarea.SetData(n_fila, 2, n_cankil.ToString("0.00"));                                      // CANTIDAD EN KILOGRAMOS

                    FgTarea.SetData(n_fila, 3, "0.00");                                                         // DISTORCION MAQUINA

                    FgTarea.SetData(n_fila, 4, lstLineaDet[n_row].n_numpertar.ToString("00"));                  // PERSONAL

                    FgTarea.SetData(n_fila, 5, "0");                                                            // TIEMPO
                    
                    FgTarea.SetData(n_fila, 6,  lstLineaDet[n_row].n_cankilper.ToString("0.00"));               // KLG POR PERSONA

                    n_numperapa = (n_cankil / lstLineaDet[n_row].n_cankilper);
                    FgTarea.SetData(n_fila, 7, n_numperapa.ToString("0.00"));                                   // OPERARIO APARENTE

                    n_formula = Math.Round(n_numperapa, 0);
                    FgTarea.SetData(n_fila, 8, n_formula.ToString("0"));                                        // FORMULA

                    // CALCULAMOS EL NUMERO DE PERSONAS REALES PARA REALIZAR LA TAREA
                    if (n_numperapa > n_formula)
                    {
                        n_opereal = n_numperapa + 1;
                    }
                    else
                    {
                        n_opereal = n_formula;
                    }
                    n_opereal = Math.Round(n_opereal, 0);
                    
                    FgTarea.SetData(n_fila, 9, n_opereal.ToString("0"));                                        // OPERARIOS REALES

                    n_efiuni = (n_numperapa / n_opereal);
                    FgTarea.SetData(n_fila, 10, n_efiuni.ToString("0.00"));                                     // EFICIENCIA UNITARIA

                    n_efitot = (n_efiuni * n_opereal);
                    FgTarea.SetData(n_fila, 11, n_efitot.ToString("0.00"));                                     // EFICIENCIA TOTAL

                    n_costar = (entLinea.n_prelin / lstLineaDet[n_row].n_cankilper);
                    FgTarea.SetData(n_fila, 13, n_costar.ToString("0.0000"));                                     // COSTO DE LA TAREA
                    n_fila = n_fila + 1;

                }
            }
            SumarColumnas();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void CmdCalLin_Click(object sender, EventArgs e)
        {
            if (TxtCanPro.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la cantidad a procesar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtCanPro.Focus();
                return;
            }

            if (TxtPreLin.Text == "")
            {
                MessageBox.Show("¡ No ha especificado le precio del producto para la linea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtPreLin.Focus();
                return;
            }
            VerLinea(Convert.ToInt32(CboLin.SelectedValue), Convert.ToDouble(TxtCanPro.Text));
        }
        void SumarColumnas()
        {
            TxtDato1.Text = funFlex.FlexSumarCol(FgTarea, 7, 2, FgTarea.Rows.Count - 1).ToString("0.00");
            TxtDato2.Text = funFlex.FlexSumarCol(FgTarea, 9, 2, FgTarea.Rows.Count - 1).ToString("0.00");
            double n_dato = 0;
            n_dato = Convert.ToDouble(funFlex.FlexSumarCol(FgTarea, 11, 2, FgTarea.Rows.Count - 1).ToString());                 // EFICIENCICIA TOTAL
            TxtDato3.Text = (n_dato / Convert.ToDouble(TxtDato2.Text)).ToString("0.00");

            n_dato = Convert.ToDouble(TxtCanPro.Text);
            n_dato = n_dato / Convert.ToDouble(TxtDato2.Text);
            TxtDato4.Text = n_dato.ToString("0.00"); 
            TxtDato5.Text = funFlex.FlexSumarCol(FgTarea, 13, 2, FgTarea.Rows.Count - 1).ToString("0.00"); 
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            objForm = null;
            objFormVis = null;
            objRegistro = null;
            objTareas = null;
            objUniMed = null;
            //objReceta = null;
            this.Close();
        }
    }
}
