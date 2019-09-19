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
    public partial class FrmManProgramarProduccion : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public OleDbConnection AccConec = new OleDbConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        BE_PRO_PROGRAMA entPrograma = new BE_PRO_PROGRAMA();
        List<BE_PRO_PROGRAMADET> lstProgramadet = new List<BE_PRO_PROGRAMADET>();
        List<BE_PRO_PROGRAMADETPRO> lstProgramadetpro = new List<BE_PRO_PROGRAMADETPRO>();
        List<BE_PRO_PROGRAMADETPROLIN> lstProgramadetprolin = new List<BE_PRO_PROGRAMADETPROLIN>();
        List<BE_PRO_PROGRAMADETPROLINDET> lstProgramadetprolindet = new List<BE_PRO_PROGRAMADETPROLINDET>();

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
        CN_pro_productosrecetaslineas objLineas = new CN_pro_productosrecetaslineas();
        CN_sun_tipexi objTipExi = new CN_sun_tipexi();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Convertir funCon = new Convertir();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

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
        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                                // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[10, 4];                                     // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlexLisPro = new string[16, 5];
        string[,] arrCabeceraFlexLisOrdPro = new string[6, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManProgramarProduccion()
        {
            InitializeComponent();
        }
        private void FrmManProgramarProduccion_Load(object sender, EventArgs e)
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
            arrCabeceraFlexLisOrdPro[0, 1] = "100";
            arrCabeceraFlexLisOrdPro[0, 2] = "C";
            arrCabeceraFlexLisOrdPro[0, 3] = "";
            arrCabeceraFlexLisOrdPro[0, 4] = "";

            arrCabeceraFlexLisOrdPro[1, 0] = "Fch. Emnision";
            arrCabeceraFlexLisOrdPro[1, 1] = "70";
            arrCabeceraFlexLisOrdPro[1, 2] = "F";
            arrCabeceraFlexLisOrdPro[1, 3] = "";
            arrCabeceraFlexLisOrdPro[1, 4] = "";
            
            arrCabeceraFlexLisOrdPro[2, 0] = "Responsable";
            arrCabeceraFlexLisOrdPro[2, 1] = "350";
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
            arrCabeceraFlexLisPro[3, 2] = "80";
            arrCabeceraFlexLisPro[3, 3] = "";
            arrCabeceraFlexLisPro[3, 4] = "";

            arrCabeceraFlexLisPro[4, 0] = "Cantidad";
            arrCabeceraFlexLisPro[4, 1] = "60";
            arrCabeceraFlexLisPro[4, 2] = "N";
            arrCabeceraFlexLisPro[4, 3] = "";
            arrCabeceraFlexLisPro[4, 4] = "";

            arrCabeceraFlexLisPro[5, 0] = "Fch. Produccion";
            arrCabeceraFlexLisPro[5, 1] = "70";
            arrCabeceraFlexLisPro[5, 2] = "F";
            arrCabeceraFlexLisPro[5, 3] = "";
            arrCabeceraFlexLisPro[5, 4] = "";

            arrCabeceraFlexLisPro[6, 0] = "Hora Produccion";
            arrCabeceraFlexLisPro[6, 1] = "50";
            arrCabeceraFlexLisPro[6, 2] = "H";
            arrCabeceraFlexLisPro[6, 3] = "";
            arrCabeceraFlexLisPro[6, 4] = "";

            arrCabeceraFlexLisPro[7, 0] = "Encargado";
            arrCabeceraFlexLisPro[7, 1] = "200";
            arrCabeceraFlexLisPro[7, 2] = "C";
            arrCabeceraFlexLisPro[7, 3] = "";
            arrCabeceraFlexLisPro[7, 4] = "";

            arrCabeceraFlexLisPro[8, 0] = "IdRec";
            arrCabeceraFlexLisPro[8, 1] = "0";
            arrCabeceraFlexLisPro[8, 2] = "N";
            arrCabeceraFlexLisPro[8, 3] = "";
            arrCabeceraFlexLisPro[8, 4] = "";

            arrCabeceraFlexLisPro[9, 0] = "idLin";
            arrCabeceraFlexLisPro[9, 1] = "0";
            arrCabeceraFlexLisPro[9, 2] = "N";
            arrCabeceraFlexLisPro[9, 3] = "";
            arrCabeceraFlexLisPro[9, 4] = "";

            arrCabeceraFlexLisPro[10, 0] = "idPro";
            arrCabeceraFlexLisPro[10, 1] = "0";
            arrCabeceraFlexLisPro[10, 2] = "N";
            arrCabeceraFlexLisPro[10, 3] = "";
            arrCabeceraFlexLisPro[10, 4] = "";

            arrCabeceraFlexLisPro[11, 0] = "idRes";
            arrCabeceraFlexLisPro[11, 1] = "0";
            arrCabeceraFlexLisPro[11, 2] = "N";
            arrCabeceraFlexLisPro[11, 3] = "";
            arrCabeceraFlexLisPro[11, 4] = "";

            arrCabeceraFlexLisPro[12, 0] = "Nº Trabajadores";
            arrCabeceraFlexLisPro[12, 1] = "60";
            arrCabeceraFlexLisPro[12, 2] = "N";
            arrCabeceraFlexLisPro[12, 3] = "";
            arrCabeceraFlexLisPro[12, 4] = "";

            arrCabeceraFlexLisPro[13, 0] = "Nº Horas";
            arrCabeceraFlexLisPro[13, 1] = "60";
            arrCabeceraFlexLisPro[13, 2] = "H";
            arrCabeceraFlexLisPro[13, 3] = "";
            arrCabeceraFlexLisPro[13, 4] = "";

            arrCabeceraFlexLisPro[14, 0] = "Hora Termino";
            arrCabeceraFlexLisPro[14, 1] = "60";
            arrCabeceraFlexLisPro[14, 2] = "H";
            arrCabeceraFlexLisPro[14, 3] = "";
            arrCabeceraFlexLisPro[14, 4] = "";

            arrCabeceraFlexLisPro[15, 0] = "Id Orden Produccion";
            arrCabeceraFlexLisPro[15, 1] = "0";
            arrCabeceraFlexLisPro[15, 2] = "N";
            arrCabeceraFlexLisPro[15, 3] = "";
            arrCabeceraFlexLisPro[15, 4] = "";

            funFlex.FlexMostrarDatos(FgLisPro, arrCabeceraFlexLisPro, dtListar, 2, false);          

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
            b_result  = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            if (b_result == true)
            {
                dtListar = objRegistro.dtListar;
            }
            ObjItem.mysConec = mysConec;
            dtItems = ObjItem.Listar(STU_SISTEMA.EMPRESAID);

            objUniMed.mysConec = mysConec;
            dtUniMed = objUniMed.Listar();

            objUniMedSunat.mysConec = mysConec;
            dtUniMedSunat = objUniMedSunat.Listar();

            objPrioridad.mysConec = mysConec;
            dtPrioridad = objPrioridad.Listar(27);

            objtareas.mysConec = mysConec;
            dtTareas = objtareas.Listar(STU_SISTEMA.EMPRESAID);

            objOrdenProduccion.mysConec = mysConec;
            dtOrdenProduccion = objOrdenProduccion.Listar(STU_SISTEMA.EMPRESAID, 0, 0);

            // CARGAMOS EL PERSONAL DE PRODUCCION
            objPerPro.mysConec = mysConec;
            if (objPerPro.Listar(STU_SISTEMA.EMPRESAID) == true)
            {
                dtPerPro = objPerPro.dtPersonal;
            }

            objRecetas.mysConec = mysConec;
            b_result =  objRecetas.ListarTodasRecetas(STU_SISTEMA.EMPRESAID);
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

            ObjItem.mysConec = mysConec;
            dtEquipos = ObjItem.Listar(STU_SISTEMA.EMPRESAID);
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
            lstProgramadetprolin.Clear();
            lstProgramadetprolindet.Clear();

            objRegistro.mysConec = mysConec;
            objRegistro.TraerRegistro(n_IdRegistro);

            entPrograma = objRegistro.entPrograma;
            lstProgramadet = objRegistro.lstProgramaDet;
            lstProgramadetpro = objRegistro.lstProgramaDetPro;
            lstProgramadetprolin = objRegistro.lstprogramadetprolin;
            lstProgramadetprolindet = objRegistro.lstprogramadetprolindet;
            
            TxtNumSer.Text = entPrograma.c_numser;
            TxtNumDoc.Text = entPrograma.c_numdoc;
            TxtFchEmi.Text = entPrograma.d_fchemi.ToString();
            CboRes.SelectedValue = entPrograma.n_idpro;
            TxtFchIni.Text  = entPrograma.d_fchini.ToString("dd/MM/yyyy");
            TxtFchFin.Text = entPrograma.d_fchfin.ToString("dd/MM/yyyy");
            TxtObs.Text = entPrograma.c_obs;
            
            // ***********************************
            // MOSTRAMOS LAS ORDENES DE PRODUCCION
            FgLisOrdPro.Rows.Count = 2;
            n_fila = 2;
            for (n_row = 0; n_row <= lstProgramadet.Count - 1; n_row++)
            {
                FgLisOrdPro.Rows.Count = FgLisOrdPro.Rows.Count + 1;

                dtResul = funDatos.DataTableFiltrar(dtOrdenProduccion, "n_id = " + lstProgramadet[n_row].n_idordpro + " ");
                if (dtResul.Rows.Count != 0)
                { c_dato = dtResul.Rows[0]["c_numser"].ToString() + "-" + dtResul.Rows[0]["c_numdoc"].ToString(); }
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
            CmdCalcLin.Text = "Modificar Linea";
            Tab1.SelectedIndex = 1;
            booAgregando = false;

            // MOSTRAMOS EL ULTIMO NUMERO DE LA PROGRAMACION
            string c_numdoc = "";
            objTipDoc.mysConec = mysConec;
            c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 78, "0001");
            TxtNumSer.Text = "0001";
            TxtNumDoc.Text = c_numdoc;

            TxtFchEmi.Focus();
        }
        void Blanquea()
        {
            FgLisOrdPro.AllowEditing = false;
            lstProgramadet.Clear();
            lstProgramadetpro.Clear();
            lstProgramadetprolin.Clear();
            lstProgramadetprolindet.Clear();
       
            FgLisOrdPro.Rows.Count = 2;
            FgLisPro.Rows.Count = 2;

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
            TxtFchIni.Enabled = !TxtFchIni.Enabled;
            TxtFchFin.Enabled = !TxtFchFin.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;

            CmdAddOP.Enabled = !CmdAddOP.Enabled;
            CmdDelOP.Enabled = !CmdDelOP.Enabled;
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

            CmdCalcLin.Text = "Modificar Linea";
            Tab1.SelectedIndex = 1;
            TxtFchEmi.Focus();
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[4].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            objRegistro.mysConec = mysConec;
            objRegistro.TraerRegistro(intIdRegistro);

            entPrograma = objRegistro.entPrograma;
            lstProgramadet = objRegistro.lstProgramaDet;
            lstProgramadetpro = objRegistro.lstProgramaDetPro;
            lstProgramadetprolin = objRegistro.lstprogramadetprolin;
            lstProgramadetprolindet = objRegistro.lstprogramadetprolindet;
            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistro.Eliminar(entPrograma, lstProgramadet) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistro.mysConec = mysConec;
                    booResult = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
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
            CmdCalcLin.Text = "Ver Linea";
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

            if (n_QueHace == 1)
            {
                booResultado = objRegistro.Insertar(entPrograma, lstProgramadet, lstProgramadetpro, lstProgramadetprolin, lstProgramadetprolindet);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistro.Actualizar(entPrograma, lstProgramadet, lstProgramadetpro, lstProgramadetprolin, lstProgramadetprolindet);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ No se pudo guardar el registro por el siguiente motivo ! " + objRegistro.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {
            //int n_fila =0;

            entPrograma.n_idemp =  STU_SISTEMA.EMPRESAID;
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
            entPrograma.d_fchini =  Convert.ToDateTime(TxtFchIni.Text);
            entPrograma.d_fchfin = Convert.ToDateTime(TxtFchFin.Text);
            entPrograma.d_fchemi =  Convert.ToDateTime(TxtFchEmi.Text);
            entPrograma.n_idpro = Convert.ToInt16(CboRes.SelectedValue);
            entPrograma.c_obs = TxtObs.Text;
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

                if (lstProgramadetpro[n_row].d_fchpro.ToString() == "")
                {
                    MessageBox.Show("¡ No ha especificado la fecha de produccion para el item : " + c_nonitem + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    FgLisPro.Focus();
                    booEstado = false;
                    return booEstado;
                }
                if (lstProgramadetpro[n_row].h_horini.ToString() == "")
                {
                    MessageBox.Show("¡ No ha especificado la hora de inicio de produccion para el item : " + c_nonitem + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    FgLisPro.Focus();
                    booEstado = false;
                    return booEstado;
                }
                if (lstProgramadetpro[n_row].n_idres == 0)
                {
                    MessageBox.Show("¡ No ha especificado el responsable de produccion para el item : " + c_nonitem + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    FgLisPro.Focus();
                    booEstado = false;
                    return booEstado;
                }
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
        private void FrmManProgramarProduccion_Activated(object sender, EventArgs e)
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

            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[4].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }
        private void FrmManProgramarProduccion_Resize(object sender, EventArgs e)
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
        void BuscarOrdenProduccion()
        {
            string[,] arrCabeceraDg1 = new string[6, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            int n_row;

            CN_pro_ordenproduccion objOrdProduccion = new CN_pro_ordenproduccion();
            
            DataTable dtOrdenProdPend = new DataTable();

            objOrdProduccion.mysConec = mysConec;
            if (objOrdProduccion.TraeOrdenProduccionPendientes(STU_SISTEMA.EMPRESAID) == false)
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
                entTmpProd.d_fchpro = null;
                entTmpProd.h_horini = "";
                entTmpProd.h_horfin = "";
                entTmpProd.n_idres = 0;

                BuscarRecetaInsPrincipal(entTmpProd.n_idite, entTmpProd.n_idrec, entTmpProd.n_idlin, entTmpProd.n_can, ref n_idinspri, ref n_caninspri);

                lstProgramadetpro.Add(entTmpProd);
            }
            #endregion CARGAR_PRODUCTOS                                                                        

            // CARGAMOS LAS TAREAS DE LAS RECETAS lstProgramaedetprolindet
            #region CARGAR_TAREAS
            CN_pro_productos objPro = new CN_pro_productos();
            int n_row2 = 0;
            List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS> lstLineasTar = new List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS>();

            for (n_row = 0; n_row <= lstOrdeProddet.Count - 1; n_row++)                                        // RECORREMOS LA LISTA DE PRODUCTOS
            {
                // TRAEMOS LOS DATOS DE LA RECETA
                objPro.mysConec = mysConec;
                if (objPro.TraerRegistro(lstOrdeProddet[n_row].n_idpro) == true)
                {
                    lstLineasTar = objPro.lstLineasTar;                                                             // CARGAMOS LAS TAREAS DE LAS RECETAS
                }

                for (n_row2 = 0; n_row2 <= lstLineasTar.Count - 1; n_row2++)
                {
                    BE_PRO_PROGRAMADETPROLINDET entLineaTarea = new BE_PRO_PROGRAMADETPROLINDET();

                    entLineaTarea.n_idite = lstLineasTar[n_row2].n_idpro;
                    entLineaTarea.n_idrec = lstLineasTar[n_row2].n_idrec;
                    entLineaTarea.n_idlin = lstLineasTar[n_row2].n_idlin;
                    entLineaTarea.n_idtar = lstLineasTar[n_row2].n_idtar;
                    entLineaTarea.n_porefi = lstLineasTar[n_row2].n_porefi;
                    entLineaTarea.n_idordpro = lstOrdeProddet[n_row].n_idord;
                    entLineaTarea.n_cankilpro = lstLineasTar[n_row2].n_cankilpro;
                    entLineaTarea.n_numpertar = lstLineasTar[n_row2].n_numpertar;
                    entLineaTarea.n_idequipo = lstLineasTar[n_row2].n_idequipo;
                    entLineaTarea.n_numpertarequ = lstLineasTar[n_row2].n_numpertarequ;
                    entLineaTarea.n_capkilporper = lstLineasTar[n_row2].n_capkilporper;
                    entLineaTarea.n_capkilporhorlin = lstLineasTar[n_row2].n_capkilporhorlin;
                    entLineaTarea.n_capkilporlintietra = lstLineasTar[n_row2].n_capkilporlintietra;
                    entLineaTarea.n_numpercal = lstLineasTar[n_row2].n_numpercal;
                    entLineaTarea.n_totprotietra = lstLineasTar[n_row2].n_totprotietra;
                    entLineaTarea.n_porefiuni = lstLineasTar[n_row2].n_porefiuni;
                    entLineaTarea.n_porefitot = lstLineasTar[n_row2].n_porefitot;
                    entLineaTarea.n_costar = lstLineasTar[n_row2].n_costar;
                    entLineaTarea.n_ord = lstLineasTar[n_row2].n_ord;

                    lstProgramadetprolindet.Add(entLineaTarea);
                }
            }
            #endregion CARGAR_TAREAS

            booAgregando = true;
            MostrarOrdenProduccion(entOrdeProd);                                                    // MUESTRA LA ORDEN DE PRODUCCION EN EL GRID FgLisOrdPro
            MostrarProductosOrdenProduccion(Convert.ToInt32(dtOrdenProdPend.Rows[0]["n_id"]));          // MUESTRA EL DETALLE DE LA ORDEN DE PRODUCCION
            booAgregando = false;
        }
        void MostrarProductosOrdenProduccion(int IdOrdenProduccion)
        { 
            int n_row = 0;
            string c_dato = "";
            int n_filaflex = 0;
            int n_idpro = 0;
            int n_idrec = 0;

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
                    FgLisPro.SetData(n_filaflex, 11, c_dato);                                                      // ID DEL PRODUCTO

                    c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", c_dato, "N").ToString();
                    FgLisPro.SetData(n_filaflex, 1, c_dato);                                                       // NOMBRE DEL PRODUCTO

                    c_dato = lstProgramadetpro[n_row].n_idrec.ToString("");
                    n_idrec = lstProgramadetpro[n_row].n_idrec;
                    c_dato = funDatos.DataTableBuscar(dtRecetas, "n_id", "c_codrec", c_dato, "N").ToString();
                    FgLisPro.SetData(n_filaflex, 2, c_dato);                                                       // CODIGO DE LA RECETA        

                    c_dato = lstProgramadetpro[n_row].n_idrec.ToString("");
                    FgLisPro.SetData(n_filaflex, 9, c_dato);                                                       // ID DE LA RECETA

                    c_dato = lstProgramadetpro[n_row].n_idlin.ToString("");

                    c_dato = funDatos.DataTableBuscar(dtLineas, "n_id", "c_codlin", c_dato, "N").ToString();
                    FgLisPro.SetData(n_filaflex, 3, c_dato);                                                       // CODIGO DE LA LINEA

                    c_dato = lstProgramadetpro[n_row].n_idlin.ToString("");
                    FgLisPro.SetData(n_filaflex, 10, c_dato);                                                      // ID DE LA LINEA

                    c_dato = lstProgramadetpro[n_row].n_idunimed.ToString("");
                    c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_despre", c_dato, "N").ToString();
                    FgLisPro.SetData(n_filaflex, 4, c_dato);                                                       // UNIDAD DE MEDIDA

                    c_dato = lstProgramadetpro[n_row].n_can.ToString("0.00");
                    FgLisPro.SetData(n_filaflex, 5, c_dato);                                                       // CANTIDAD A PRODUCCIR

                    if (lstProgramadetpro[n_row].d_fchpro != null)
                    {
                        c_dato = Convert.ToDateTime(lstProgramadetpro[n_row].d_fchpro).ToString("dd/MM/yyyy");
                        FgLisPro.SetData(n_filaflex, 6, c_dato);                                                   // FECHA DE PRODUCCION
                    }
                    else
                    {
                        FgLisPro.SetData(n_filaflex, 6, "");                                                       // FECHA DE PRODUCCION
                    }

                    if (funFunciones.NulosC(lstProgramadetpro[n_row].h_horini) != "")
                    { c_dato = lstProgramadetpro[n_row].h_horini.ToString(); }
                    else
                    { c_dato = ""; }
                    FgLisPro.SetData(n_filaflex, 7, c_dato);                                                       // HORA DE PRODUCCION

                    if (Convert.ToInt32(funFunciones.NulosN(lstProgramadetpro[n_row].n_idres)) != 0)
                    {
                        c_dato = lstProgramadetpro[n_row].n_idres.ToString("");
                        FgLisPro.SetData(n_filaflex, 12, c_dato);                                                  // ID DEL RESPONSABLE

                        c_dato = funDatos.DataTableBuscar(dtPerPro, "n_id", "c_apenom", c_dato, "N").ToString();
                    }
                    else
                    {
                        c_dato = "";
                        FgLisPro.SetData(n_filaflex, 12, "0");
                    }
                    FgLisPro.SetData(n_filaflex, 8, c_dato);                                                        // NOMBRE DEL RESPONSABLE

                    c_dato = funFunciones.NulosC(lstProgramadetpro[n_row].h_horfin);
                    FgLisPro.SetData(n_filaflex, 15, c_dato);                                                       // HORA DE TERMINO


                    c_dato = funFunciones.NulosC(lstProgramadetpro[n_row].n_idordpro);
                    FgLisPro.SetData(n_filaflex, 16, c_dato);                                                       // ID DE LA ORDEN DE PRODUCCION

                    //double n_horini = 0;
                    double n_tiepro = 0;

                    // **************************************
                    // CARGAMOS LOS DATOS DE LA LINEA ACTYIVA

                    int n_rowlin = 0;
                    for(n_rowlin = 0; n_rowlin <= lstProgramadetprolin.Count - 1 ;n_rowlin++)
                    {
                        if ((lstProgramadetprolin[n_rowlin].n_idite == lstProgramadetpro[n_row].n_idite) && (lstProgramadetprolin[n_rowlin].n_idlin == lstProgramadetpro[n_row].n_idlin)
                            && (lstProgramadetprolin[n_rowlin].n_idrec == lstProgramadetpro[n_row].n_idrec))
                        {
                            c_dato = lstProgramadetprolin[n_rowlin].n_numope.ToString();
                            FgLisPro.SetData(n_filaflex, 13, c_dato);                                                       // NUMERO DE OPERARIOS

                            if (lstProgramadetprolin[n_rowlin].n_tiepro == 0)
                            {
                                c_dato = "";
                            }
                            else
                            {
                                n_tiepro = lstProgramadetprolin[n_rowlin].n_tiepro;
                                c_dato = funCon.DecimalEnHoras(lstProgramadetprolin[n_rowlin].n_tiepro);
                            }
                            FgLisPro.SetData(n_filaflex, 14, c_dato);                                                       // TIEMPO DE PRODUCCION
                        }
                    }
                    n_filaflex = n_filaflex + 1;
                }
            }
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
        private void CmdDelOP_Click(object sender, EventArgs e)
        {
            if (FgLisOrdPro.Rows.Count == 2)
            {
                return;
            }
            int n_idOP = Convert.ToInt32(FgLisOrdPro.GetData(FgLisOrdPro.Row, 6));
            EliminarProductos(n_idOP);
            FgLisOrdPro.RemoveItem(FgLisOrdPro.Row);
            FgLisPro.Rows.Count = 2;
        }
        void EliminarProductos(int n_IdOrdenProduccion)
        { 
            int n_row = 0;
           
            for (n_row = 0; n_row <= lstProgramadetprolindet.Count; n_row++)
            {
                if (lstProgramadetprolindet[n_row].n_idordpro == n_IdOrdenProduccion)
                {
                    lstProgramadetprolindet.RemoveAt(n_row);
                    n_row = n_row - 1;
                    if (lstProgramadetprolindet.Count == 0) { break; }
                }
            }

            for (n_row = 0; n_row <= lstProgramadetprolin.Count; n_row++)
            {
                if (lstProgramadetprolin[n_row].n_idordpro == n_IdOrdenProduccion)
                {
                    lstProgramadetprolin.RemoveAt(n_row);
                    n_row = n_row - 1;
                    if (lstProgramadetprolin.Count == 0) { break; }
                }
            }

            for (n_row = 0; n_row <= lstProgramadetpro.Count; n_row++)
            {
                if (lstProgramadetpro[n_row].n_idordpro == n_IdOrdenProduccion)
                {
                    lstProgramadetpro.RemoveAt(n_row);
                    n_row = n_row - 1;
                    if (lstProgramadetpro.Count == 0) { break; }
                }
            }

            for (n_row = 0; n_row <= lstProgramadet.Count; n_row++)
            {
                if (lstProgramadet[n_row].n_idordpro == n_IdOrdenProduccion)
                {
                    lstProgramadet.RemoveAt(n_row);
                    n_row = n_row - 1;
                    if (lstProgramadet.Count == 0) { break; }
                }
            }
        }
        private void TxtFchEnt_ValueChanged(object sender, EventArgs e)
        {

        }
        private void FgLisPro_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgLisPro.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            DataTable dtResul;
            int n_row = 0;
            string c_dato = "";
            int n_idpro = 0;
            int n_idordpro = 0;
            int n_idrec = 0;
            int n_idres = 0;
            int n_idlin = 0;

            n_idpro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 11).ToString());                               // OBTENEMOS EL ID DEL PRODUCTO
            n_idordpro = Convert.ToInt32(FgLisOrdPro.GetData(FgLisOrdPro.Row, 6).ToString());                       // OBTENEMOS EL ID DE LA ORDEN DE PRODUCCION

            if (FgLisPro.Col == 2)
            {
                // OBTENEMOS EL ID DE LA RECETA
                c_dato = FgLisPro.GetData(FgLisPro.Row, 11).ToString();                                             // ID DEL PRODUCTO     
                dtResul = funDatos.DataTableFiltrar(dtRecetas, "n_idpro = " + c_dato + "");                         // OBTENEMOS LAS RECETAS DEL PRODUCTO
                c_dato = FgLisPro.GetData(FgLisPro.Row, 2).ToString();                                              // oOBTENEMOS EL CODIGO DE LA RECETA        
                dtResul = funDatos.DataTableFiltrar(dtResul, "c_codrec = '" + c_dato + "'");
                n_idrec = Convert.ToInt32(dtResul.Rows[0]["n_id"]);
                FgLisPro.SetData(FgLisPro.Row, 9, n_idrec);                                                         // ESCRIBIMOS EL ID DE LA RECETA

                // SI CAMBIA LA RECETA ACTALIZAMOS LA LISTA CON EL NUEVO DATO
                
                for (n_row = 0; n_row <= lstProgramadetpro.Count-1; n_row++)
                {
                    if (lstProgramadetpro[n_row].n_idordpro == n_idordpro)                                         // SI ES EL NUMERO DE ORDEN DE PRODUCCION
                    {
                        if (lstProgramadetpro[n_row].n_idite == n_idpro)                                           // SI EL PRODUCTO                
                        {
                            n_idrec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 9).ToString());                // OBTENEMOS EL ID DE LA RECETA
                            lstProgramadetpro[n_row].n_idrec = n_idrec;                                            // CAMBIAMOS POR LA RECETA SELECCIONADA
                        }
                    }
                }
                
                // SE CAMBIO LA RECETA SE TIENE QUE ACTUALIZAR LA NUEVA LINEA, YA QUE CADA RECETA ESTA AMARRADA A UNA LINEA
                dtResul = funDatos.DataTableFiltrar(dtLineas,"n_idrec = "+ n_idrec +"");                            // FILTRAMOS LA LINEA DE LA RECETA
                if (dtResul.Rows.Count != 0)
                {
                    n_idlin = Convert.ToInt32(dtResul.Rows[0]["n_id"]);
                    FgLisPro.SetData(FgLisPro.Row, 3, dtResul.Rows[0]["c_codlin"].ToString());                      // ACTUALIZAMOS EL CODIGO DE LA LINEA
                    FgLisPro.SetData(FgLisPro.Row, 10, n_idlin.ToString());                                         // ACTUALIZAMOS EL ID DE LA LINEA
                }
                else
                {
                    FgLisPro.SetData(FgLisPro.Row, 3, "");                                                          // ACTUALIZAMOS EL CODIGO DE LA RECETA
                    FgLisPro.SetData(FgLisPro.Row, 10, "");                                                         // ACTUALIZAMOS EL ID DE LA LINEA
                }

                // CAMBIAMOS LA LINEA EN LA LISTA
                for (n_row = 0; n_row <= lstProgramadetpro.Count - 1; n_row++)
                {
                    if (lstProgramadetpro[n_row].n_idordpro == n_idordpro)                                         // SI ES EL NUMERO DE ORDEN DE PRODUCCION
                    {
                        if (lstProgramadetpro[n_row].n_idite == n_idpro)                                           // SI EL PRODUCTO                
                        {
                            n_idrec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 9).ToString());                // OBTENEMOS EL ID DE LA RECETA
                            lstProgramadetpro[n_row].n_idlin = n_idlin;                                             // CAMBIAMOS POR LA RECETA SELECCIONADA
                        }
                    }
                }
                return;
            }

            if (FgLisPro.Col == 3)
            {
                FgLisPro.Select(FgLisPro.Row - 1, 4);
                return;
            }

            if (FgLisPro.Col == 6)
            {
                DateTime d_fchorden = Convert.ToDateTime(TxtFchIni.Text);

                DateTime d_fchini = Convert.ToDateTime(TxtFchIni.Text);
                DateTime d_fchfin = Convert.ToDateTime(TxtFchFin.Text);
                DateTime d_fchpro = Convert.ToDateTime(FgLisPro.GetData(FgLisPro.Row, 6));

                if (d_fchpro < d_fchini)
                {
                    booAgregando = true;
                    FgLisPro.SetData(FgLisPro.Row, 6,"");
                    booAgregando = false;
                    MessageBox.Show("! La fecha de produccion no puede ser menor a la fecha de inicio  !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }

                if (d_fchpro > d_fchfin)
                {
                    booAgregando = true;
                    FgLisPro.SetData(FgLisPro.Row, 6, "");
                    booAgregando = false;
                    MessageBox.Show("! La fecha de produccion no puede ser mayor a la fecha de termino  !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                // CAMBIAMOS LA FECHA DE INICIO
                for (n_row = 0; n_row <= lstProgramadetpro.Count - 1; n_row++)
                {
                    if (lstProgramadetpro[n_row].n_idordpro == n_idordpro)                                             // SI ES EL NUMERO DE ORDEN DE PRODUCCION
                    {
                        if (lstProgramadetpro[n_row].n_idite == n_idpro)                                               // SI EL PRODUCTO                
                        {
                            c_dato = FgLisPro.GetData(FgLisPro.Row, 6).ToString();                                      // OBTENEMOS EL ID DE LA RECETA
                            lstProgramadetpro[n_row].d_fchpro = Convert.ToDateTime(c_dato);                            // CAMBIAMOS POR LA RECETA SELECCIONADA
                        }
                    }
                }
                return;
            }

            if (FgLisPro.Col == 7)
            {
                // CAMBIAMOS LA HORA DE INICIO
                for (n_row = 0; n_row <= lstProgramadetpro.Count - 1; n_row++)
                {
                    if (lstProgramadetpro[n_row].n_idordpro == n_idordpro)                                             // SI ES EL NUMERO DE ORDEN DE PRODUCCION
                    {
                        if (lstProgramadetpro[n_row].n_idite == n_idpro)                                               // SI EL PRODUCTO                
                        {
                            c_dato = FgLisPro.GetData(FgLisPro.Row, 7).ToString();                                      // OBTENEMOS EL ID DE LA RECETA
                            lstProgramadetpro[n_row].h_horini = c_dato;                                                // CAMBIAMOS POR LA RECETA SELECCIONADA
                        }
                    }
                }
                return;
            }

            if (FgLisPro.Col == 8)
            {
                c_dato = FgLisPro.GetData(FgLisPro.Row, 8).ToString();                                                  // OBTENEMOS EL NOMBRE DEL RESPONSABLE
                dtResul = funDatos.DataTableFiltrar(dtPerPro, "c_apenom = '" + c_dato + "'");
                if (dtResul.Rows.Count != 0)
                {
                    n_idres = Convert.ToInt32(dtResul.Rows[0]["n_id"]);
                    FgLisPro.SetData(FgLisPro.Row, 12, n_idres);

                    // CAMBIAMOS EL ID DEL PERSONAL EN LA LISTA
                    for (n_row = 0; n_row <= lstProgramadetpro.Count - 1; n_row++)
                    {
                        if (lstProgramadetpro[n_row].n_idordpro == n_idordpro)                                         // SI ES EL NUMERO DE ORDEN DE PRODUCCION
                        {
                            if (lstProgramadetpro[n_row].n_idite == n_idpro)                                           // SI EL PRODUCTO                
                            {
                                n_idrec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 9).ToString());                // OBTENEMOS EL ID DE LA RECETA
                                lstProgramadetpro[n_row].n_idres = n_idres;                                            // CAMBIAMOS POR LA RECETA SELECCIONADA
                            }
                        }
                    }
                }
                else
                {
                    FgLisPro.SetData(FgLisPro.Row, 12, ""); 
                }
                return;
            }
        }
        private void FgLisPro_EnterCell(object sender, EventArgs e)
        {
            if (FgLisPro.Rows.Count == 2) { return; }
            if (n_QueHace == 3)
            {
                FgLisPro.AllowEditing = false; return;
            }
            if (booAgregando == true) { return; }

            DataTable dtResul = new DataTable();
            string c_dato = "";

            if (FgLisPro.Col == 2)
            {
                if (FgLisPro.Row >= 2)
                {
                    // SI LA COLUMNA DE PRODUCTO ESTA VACIA NO MUESTRA NADA
                    if (funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row - 1, 1)) == "") { FgLisPro.AllowEditing = false; return; }
                }
                c_dato = FgLisPro.GetData(FgLisPro.Row, 11).ToString();                                                         // ID DEL PRODUCTO     
                dtResul = funDatos.DataTableFiltrar(dtRecetas, "n_idpro = " + c_dato + "");

                funFlex.FlexColumnaCombo(FgLisPro, dtResul, "c_codrec", 2);
            }

            if (FgLisPro.Col == 3)
            {
                if (FgLisPro.Row >= 2)
                {
                    // SI LA COLUMNA DE RECETA ESTA VACIA NO MUESTRA NADA
                    if (funFunciones.NulosC(FgLisPro.GetData(FgLisPro.Row - 1, 2)) == "") { FgLisPro.AllowEditing = false; return; }
                }
                
                c_dato = FgLisPro.GetData(FgLisPro.Row, 9).ToString();                                                             // ID DE LA RECETA
                dtResul = funDatos.DataTableFiltrar(dtLineas, "n_idrec = " + c_dato + "");
                funFlex.FlexColumnaCombo(FgLisPro, dtResul, "c_codlin", 3);
            }

            if (FgLisPro.Col == 8)
            {
                funFlex.FlexColumnaCombo(FgLisPro, dtPerPro, "c_apenom", 8);
            }

            if ((FgLisPro.Col == 1) || (FgLisPro.Col == 4) || (FgLisPro.Col == 5) || (FgLisPro.Col == 13) || (FgLisPro.Col == 14) || (FgLisPro.Col == 15))
            {
                FgLisPro.AllowEditing = false;
                return;
            }
            FgLisPro.AllowEditing = true;
        }
        private void FgLisPro_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 4)
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void FgLisPro_RowColChange(object sender, EventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgLisPro.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }
        }
        private void FgLisOrdPro_RowColChange(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (FgLisOrdPro.Rows.Count == 2) {return;}

            booAgregando = true;
            MostrarProductosOrdenProduccion(Convert.ToInt32(FgLisOrdPro.GetData(FgLisOrdPro.Row, 6)));
            booAgregando = false;
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
                int n_idRec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 9).ToString());
                int n_idPro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 11).ToString());

                VerReceta(n_idPro, n_idRec);
            }
        }


        //************************************************************************
        //**        CAMBIAR POR CLASE CREADA
        //************************************************************************
        void VerReceta(int n_IdProducto,  int n_IdReceta)
        {
            int n_row = 0;
            int n_index = 0;
            CN_pro_productos objPro = new CN_pro_productos();
            List<BE_PRO_PRODUCTOSRECETAS> lstRecetas = new List<BE_PRO_PRODUCTOSRECETAS>();
            List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstRecetasIns = new List<BE_PRO_PRODUCTOSRECETASINSUMOS>();

            objPro.mysConec = mysConec;
            if (objPro.TraerRegistro(n_IdProducto) == true)
            {
                lstRecetas = objPro.lstRecetas;
                lstRecetasIns = objPro.lstRecetasIns;
            }

            for (n_row = 0; n_row <= lstRecetas.Count - 1; n_row++)
            {
                if (lstRecetas[n_row].n_id == n_IdReceta)
                {
                    n_index = n_row;
                    break;
                }
            }

            // FILTRAMOS SOLO LOS INSUMOS DE LA RECETA  ACTUAL
            List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstTemp = new List<BE_PRO_PRODUCTOSRECETASINSUMOS>();
            BE_PRO_PRODUCTOSRECETASINSUMOS entTemp = new BE_PRO_PRODUCTOSRECETASINSUMOS();

            for (n_row = 0; n_row <= lstRecetasIns.Count - 1; n_row++)
            {
                if (lstRecetasIns[n_row].n_idrec == n_IdReceta)
                {
                    entTemp = lstRecetasIns[n_row];

                    lstTemp.Add(entTemp);
                }
            }

            FrmVerRecetaInsumos MyForm = new FrmVerRecetaInsumos();
            MyForm.dtTipExi = dtTipExi;
            MyForm.dtItems = dtItems;
            MyForm.dtUniMed = dtUniMedSunat;
            MyForm.entReceta = lstRecetas[n_index];
            MyForm.lstRecetasIns = lstTemp;
            MyForm.ShowDialog();
        }
        private void CmdVerLin_Click(object sender, EventArgs e)
        {
            if (FgLisPro.Rows.Count == 2)
            {
                MessageBox.Show("! No se han encontrado productos ¡ Debe de agregar uno como minimo !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            int n_idRec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 9).ToString());
            int n_idLin = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 10).ToString());
            int n_idPro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 11).ToString());
            int n_idOrdPro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 16).ToString()); 
            double n_canPro = Convert.ToDouble(FgLisPro.GetData(FgLisPro.Row, 5).ToString());

            VerLineas(n_idOrdPro, n_idPro, n_idRec, n_idLin, false, n_canPro);
        }
        void VerLineas(int n_OrdenProduccion, int n_IdProducto,  int n_IdReceta, int n_IdLinea, bool b_Calcular, double n_CanProProducir)
        {
            int n_row = 0;
            int n_row2 = 0;
            int n_index = 0;
            double n_CanInsPri = 0;
            int n_IdInsPri = 0;
            int n_rowlin = 0;
            bool b_seencontro = false;
            #region DATOS_RECETA
            //DATOS DE LA RECETA
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

            BuscarRecetaInsPrincipal(n_IdProducto, n_IdReceta, n_IdLinea, n_CanProProducir, ref n_IdInsPri, ref n_CanInsPri);

            // OBTENEMOS EL INDICE DE LA LINEA PARA ENVIARLE AL VISOR DE LINEAS
            for (n_row = 0; n_row <= lstLineas.Count - 1; n_row++)
            {
                if (lstLineas[n_row].n_id == n_IdLinea)
                {
                    n_index = n_row;
                    break;
                }
            }

            // ACTUALIZAMOS LOS DATOS DE LA LINEA CON LOS DATOS DE LA LISTA DE LINEAS
            for (n_row = 0; n_row <= lstLineas.Count-1; n_row++)
            {
                for (n_row2 = 0; n_row2 <= lstProgramadetpro.Count-1; n_row2++)
                {
                    if ((lstProgramadetpro[n_row2].n_idite == n_IdProducto) && (lstProgramadetpro[n_row2].n_idrec == n_IdReceta) && (lstProgramadetpro[n_row2].n_idlin == n_IdLinea))
                    {
                        // BUSCAMOS LOS DATOS QUE FALTAN EL LA LISTA DE LINEA
                        for (n_rowlin = 0; n_rowlin <= lstProgramadetprolin.Count-1; n_rowlin++)
                        {
                            if (lstProgramadetprolin[n_rowlin].n_idite == lstProgramadetpro[n_row2].n_idite)
                            {
                                lstLineas[n_row].n_tiepro = lstProgramadetprolin[n_rowlin].n_tiepro;                  // JALAMOS DE lstProgramadetprolin
                                lstLineas[n_row].n_numope = lstProgramadetprolin[n_rowlin].n_numope;                  // JALAMOS DE lstProgramadetprolin
                                lstLineas[n_row].n_efi = lstProgramadetprolin[n_rowlin].n_porefi;                     // JALAMOS DE lstProgramadetprolin
                            }
                        }

                        lstLineas[n_row].n_can = lstProgramadetpro[n_row2].n_can;                               // JALAMOS DE lstProgramadetpro
                    }
                }
            }
            
            bool b_TieneTareas = false;
            List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS> lstTemp = new List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS>();
            // PREGUNTAMOS SI EL DETALLE DE LAS TAREAS TIENE DATOS
            if (lstProgramadetprolindet.Count != 0)
            {
                for (n_row = 0; n_row <= lstProgramadetprolindet.Count - 1; n_row++)
                {
                    if (lstProgramadetprolindet[n_row].n_idite == n_IdProducto)
                    {
                        if (lstProgramadetprolindet[n_row].n_idrec == n_IdReceta)
                        {
                            if (lstProgramadetprolindet[n_row].n_idlin == n_IdLinea)
                            {
                                BE_PRO_PRODUCTOSRECETASLINEASTAREAS entTemp = new BE_PRO_PRODUCTOSRECETASLINEASTAREAS();

                                entTemp.n_idpro = lstProgramadetprolindet[n_row].n_idite;
                                entTemp.n_idrec = lstProgramadetprolindet[n_row].n_idrec;
                                entTemp.n_idlin = lstProgramadetprolindet[n_row].n_idlin;
                                entTemp.n_idtar = lstProgramadetprolindet[n_row].n_idtar;
                                entTemp.n_porefi = lstProgramadetprolindet[n_row].n_porefi;
                                entTemp.n_cankilpro = lstProgramadetprolindet[n_row].n_cankilpro;
                                entTemp.n_numpertar = lstProgramadetprolindet[n_row].n_numpertar;
                                entTemp.n_idequipo = lstProgramadetprolindet[n_row].n_idequipo;
                                entTemp.n_canequi = lstProgramadetprolindet[n_row].n_canequi;
                                entTemp.n_numpertarequ = lstProgramadetprolindet[n_row].n_numpertarequ;
                                entTemp.n_capkilporper = lstProgramadetprolindet[n_row].n_capkilporper;
                                entTemp.n_capkilporhorlin = lstProgramadetprolindet[n_row].n_capkilporhorlin;
                                entTemp.n_capkilporlintietra = lstProgramadetprolindet[n_row].n_capkilporlintietra;
                                entTemp.n_numpercal = lstProgramadetprolindet[n_row].n_numpercal;
                                entTemp.n_totprotietra = lstProgramadetprolindet[n_row].n_totprotietra;
                                entTemp.n_porefiuni = lstProgramadetprolindet[n_row].n_porefiuni;
                                entTemp.n_porefitot = lstProgramadetprolindet[n_row].n_porefitot;
                                entTemp.n_costar = lstProgramadetprolindet[n_row].n_costar;
                                entTemp.n_ord = lstProgramadetprolindet[n_row].n_ord;
                                                                    
                                lstTemp.Add(entTemp);
                                b_TieneTareas = true;
                            }
                        }
                    }
                }

                // SI NO SE ENCONTRO TAREAS CARGADAS LAS AGREGAMOS PARA QUE PUEDA HACER EL CALCULO
                if (b_TieneTareas == false)
                {
                    for (n_row = 0; n_row <= lstLineasTar.Count - 1; n_row++)
                    {               
                        if (lstLineasTar[n_row].n_idlin == n_IdLinea)
                        {
                            BE_PRO_PRODUCTOSRECETASLINEASTAREAS entTemp = new BE_PRO_PRODUCTOSRECETASLINEASTAREAS();
                            entTemp = lstLineasTar[n_row];
                            lstTemp.Add(entTemp);
                        }
                    }
                }
            }
            else
            { 
                // FILTRAMOS SOLO LAS TAREAS DE LA LINEA  ACTUAL
                for (n_row = 0; n_row <= lstLineasTar.Count - 1; n_row++)
                {               
                    if (lstLineasTar[n_row].n_idlin == n_IdLinea)
                    {
                        BE_PRO_PRODUCTOSRECETASLINEASTAREAS entTemp = new BE_PRO_PRODUCTOSRECETASLINEASTAREAS();
                        entTemp = lstLineasTar[n_row];
                        lstTemp.Add(entTemp);
                    }
                }
            }
            #endregion DATOS_RECETA
            booAgregando = true;

            FrmVerLineasTareas MyForm = new FrmVerLineasTareas();
            MyForm.dtTipExi = dtTipExi;
            MyForm.dtItems = dtItems;
            MyForm.dtUniMed = dtUniMed;
            MyForm.entLinea = lstLineas[n_index];
            MyForm.lstLineaTar = lstTemp;
            MyForm.dtTareas = dtTareas;
            MyForm.dtEquipos = dtEquipos;
            MyForm.n_NewcanProPri = n_CanInsPri;                                    // LE PASAMOS LA CANTIDAD DEL INSUMOS PRINCIPAL DEL PRODUCTO A PROGRAMAR

            if (CmdCalcLin.Text != "Ver Linea")
            {
                MyForm.b_Modificar = true;
            }
            MyForm.ShowDialog();                                                        // LLAMAMOS AL FORMNULARIO QUE MUESTRA LA LINEA
            if (MyForm.b_Aceptar == true)
            { 
                if (CmdVerLin.Text != "Ver Linea")
                {
                    #region insertar_lineas_y_tareas
                    // ACTUALIZAMOS LOS DATOS DE LA LINEA ACTUAL
                    BE_PRO_PRODUCTOSRECETASLINEAS entLinTmp = new BE_PRO_PRODUCTOSRECETASLINEAS();
                    entLinTmp = MyForm.entLinea;
                    lstTemp = MyForm.lstLineaTar;

                    if (lstProgramadetprolin.Count == 0)
                    {
                        BE_PRO_PROGRAMADETPROLIN entLinea = new BE_PRO_PROGRAMADETPROLIN();
                                        
                        entLinea.n_idpro = entPrograma.n_id;
                        entLinea.n_idordpro = n_OrdenProduccion;
                        entLinea.n_idite = entLinTmp.n_idpro;
                        entLinea.n_idrec = entLinTmp.n_idrec;
                        entLinea.n_idlin = entLinTmp.n_id;
                        //lstProgramadetprolin[n_index].n_idinspri = entLinTmp.n_idinspri;
                        entLinea.n_caninspri = entLinTmp.n_can;
                        entLinea.n_numope = Convert.ToInt32(entLinTmp.n_numope);
                        entLinea.n_porefi = entLinTmp.n_efi;
                        entLinea.n_tiepro = entLinTmp.n_tiepro;
                        entLinea.n_prehorjor = entLinTmp.n_prehorjor;
                    
                        lstProgramadetprolin.Add(entLinea);
                    }
                    else
                    {
                        b_seencontro = false;
                        // ACTUALIZAMOS LOS DATOS DE LA LINEA
                        for (n_row = 0; n_row <= lstProgramadetprolin.Count - 1; n_row++)
                        {
                            if ((lstProgramadetprolin[n_row].n_idlin == entLinTmp.n_id) && (lstProgramadetprolin[n_row].n_idrec == entLinTmp.n_idrec) &&
                                (lstProgramadetprolin[n_row].n_idite == entLinTmp.n_idpro))
                            {
                                //lstProgramadetprolin[n_row].n_idpro = entLinTmp.n_idpro;
                                //lstProgramadetprolin[n_index].n_idordpro = entLinTmp.n_idordpro;
                                lstProgramadetprolin[n_row].n_idite = entLinTmp.n_idpro;
                                lstProgramadetprolin[n_row].n_idrec = entLinTmp.n_idrec;
                                lstProgramadetprolin[n_row].n_idlin = entLinTmp.n_id;
                                //lstProgramadetprolin[n_index].n_idinspri = entLinTmp.n_idinspri;
                                lstProgramadetprolin[n_row].n_caninspri = entLinTmp.n_can;
                                lstProgramadetprolin[n_row].n_numope = Convert.ToInt32(entLinTmp.n_numope);
                                lstProgramadetprolin[n_row].n_porefi = entLinTmp.n_efi;
                                lstProgramadetprolin[n_row].n_tiepro = entLinTmp.n_tiepro;
                                lstProgramadetprolin[n_row].n_prehorjor = entLinTmp.n_prehorjor;
                                b_seencontro = true;
                                break;
                            }
                        }
                        if (b_seencontro == false)
                        {
                            BE_PRO_PROGRAMADETPROLIN entLinea = new BE_PRO_PROGRAMADETPROLIN();

                            entLinea.n_idpro = entPrograma.n_id;
                            entLinea.n_idordpro = n_OrdenProduccion;
                            entLinea.n_idite = entLinTmp.n_idpro;
                            entLinea.n_idrec = entLinTmp.n_idrec;
                            entLinea.n_idlin = entLinTmp.n_id;
                            //lstProgramadetprolin[n_index].n_idinspri = entLinTmp.n_idinspri;
                            entLinea.n_caninspri = entLinTmp.n_can;
                            entLinea.n_numope = Convert.ToInt32(entLinTmp.n_numope);
                            entLinea.n_porefi = entLinTmp.n_efi;
                            entLinea.n_tiepro = entLinTmp.n_tiepro;
                            entLinea.n_prehorjor = entLinTmp.n_prehorjor;

                            lstProgramadetprolin.Add(entLinea);
                        }
                    }  

                    // ACTUALIZAMOS  LOS DATOS TRAIDOS EN LA FILA ACTUAL DE LOS PRODUCTOS
                    FgLisPro.SetData(FgLisPro.Row, 13, entLinTmp.n_numope.ToString("00"));
                    FgLisPro.SetData(FgLisPro.Row, 14, funCon.DecimalEnHoras(entLinTmp.n_tiepro));

                    double n_horini = funCon.HoraEnDecimal(FgLisPro.GetData(FgLisPro.Row, 7).ToString()+":00");
                    //n_horini = Convert.ToDouble(FgLisPro.GetData(FgLisPro.Row, 7));
                    double n_horfin = entLinTmp.n_tiepro;

                    n_horini = n_horini + n_horfin;
                    FgLisPro.SetData(FgLisPro.Row, 15, funCon.DecimalEnHoras(n_horini));

                    //ACTUALIZAMOS LA HORA DE TERMINO EN LA LISTA lstProgramadetpro
                    for (n_rowlin = 0; n_rowlin <= lstProgramadetpro.Count - 1; n_rowlin++)
                    {
                        if ((lstProgramadetpro[n_rowlin].n_idite == n_IdProducto) && (lstProgramadetpro[n_rowlin].n_idrec == n_IdReceta) &&
                            (lstProgramadetpro[n_rowlin].n_idlin == n_IdLinea))
                        {
                            lstProgramadetpro[n_rowlin].h_horfin = FgLisPro.GetData(FgLisPro.Row, 15).ToString();
                            break;
                        }
                    }

                    // ACTUALIZAMOS EL DETALLE DE LA LINEA ACTUAL
                    if (lstProgramadetprolindet.Count == 0)
                    {
                        // SI LA LISTA DE TAREAS ESTA VACIA INSERTAMOS LAS TAREAS CALCULADA
                        for (n_rowlin = 0; n_rowlin <= lstTemp.Count - 1; n_rowlin++)
                        {
                            BE_PRO_PROGRAMADETPROLINDET entLinDet = new BE_PRO_PROGRAMADETPROLINDET();
                            entLinDet.n_idpro = 0;
                            entLinDet.n_idordpro = n_OrdenProduccion;
                            entLinDet.n_idite = n_IdProducto;
                            entLinDet.n_idrec = n_IdReceta;
                            entLinDet.n_idlin = n_IdLinea;
                            entLinDet.n_idtar = lstTemp[n_rowlin].n_idtar;
                            entLinDet.n_porefi = lstTemp[n_rowlin].n_porefi;
                            entLinDet.n_cankilpro = lstTemp[n_rowlin].n_cankilpro;
                            entLinDet.n_numpertar = lstTemp[n_rowlin].n_numpertar;
                            entLinDet.n_idequipo = lstTemp[n_rowlin].n_idequipo;
                            entLinDet.n_canequi = lstTemp[n_rowlin].n_canequi;
                            entLinDet.n_numpertarequ = lstTemp[n_rowlin].n_numpertarequ;
                            entLinDet.n_capkilporper = lstTemp[n_rowlin].n_capkilporper;
                            entLinDet.n_capkilporhorlin = lstTemp[n_rowlin].n_capkilporhorlin;
                            entLinDet.n_capkilporlintietra = lstTemp[n_rowlin].n_capkilporlintietra;
                            entLinDet.n_numpercal = lstTemp[n_rowlin].n_numpercal;
                            entLinDet.n_totprotietra = lstTemp[n_rowlin].n_totprotietra;
                            entLinDet.n_porefiuni = lstTemp[n_rowlin].n_porefiuni;
                            entLinDet.n_porefitot = lstTemp[n_rowlin].n_porefitot;
                            entLinDet.n_costar = lstTemp[n_rowlin].n_costar;
                            entLinDet.n_ord = lstTemp[n_rowlin].n_ord;

                            lstProgramadetprolindet.Add(entLinDet);
                        }
                    }
                    else
                    {
                        // RECORREMOS LA LISTA DE TAREAS RETORNADA DE LA CALCULADARA
                        for (n_row2 = 0; n_row2 <= lstTemp.Count - 1; n_row2++)
                        {
                            b_seencontro = false;
                            // RECORREMOS LA LISA DE TAREAS  PARA BUSCAR SI LA TAREA YA EXISTE
                            for (n_row = 0; n_row <= lstProgramadetprolindet.Count - 1; n_row++)
                            {
                                //  PREGUNTAMOS SI EXISTE
                                if ((lstProgramadetprolindet[n_row].n_idite == lstTemp[n_row2].n_idpro) &&
                                (lstProgramadetprolindet[n_row].n_idordpro == n_OrdenProduccion) &&
                                (lstProgramadetprolindet[n_row].n_idlin == lstTemp[n_row2].n_idlin) &&
                                (lstProgramadetprolindet[n_row].n_idrec == lstTemp[n_row2].n_idrec) &&
                                (lstProgramadetprolindet[n_row].n_idtar == lstTemp[n_row2].n_idtar))
                                {
                                    // SI EXISTE LO ACTUALIZAMOS
                                    lstProgramadetprolindet[n_row].n_idtar = lstTemp[n_row2].n_idtar;
                                    lstProgramadetprolindet[n_row].n_porefi = lstTemp[n_row2].n_porefi;
                                    lstProgramadetprolindet[n_row].n_cankilpro = lstTemp[n_row2].n_cankilpro;
                                    lstProgramadetprolindet[n_row].n_numpertar = lstTemp[n_row2].n_numpertar;
                                    lstProgramadetprolindet[n_row].n_idequipo = lstTemp[n_row2].n_idequipo;
                                    lstProgramadetprolindet[n_row].n_canequi = lstTemp[n_row2].n_canequi;
                                    lstProgramadetprolindet[n_row].n_numpertarequ = lstTemp[n_row2].n_numpertarequ;
                                    lstProgramadetprolindet[n_row].n_capkilporper = lstTemp[n_row2].n_capkilporper;
                                    lstProgramadetprolindet[n_row].n_capkilporhorlin = lstTemp[n_row2].n_capkilporhorlin;
                                    lstProgramadetprolindet[n_row].n_capkilporlintietra = lstTemp[n_row2].n_capkilporlintietra;
                                    lstProgramadetprolindet[n_row].n_numpercal = lstTemp[n_row2].n_numpercal;
                                    lstProgramadetprolindet[n_row].n_totprotietra = lstTemp[n_row2].n_totprotietra;
                                    lstProgramadetprolindet[n_row].n_porefiuni = lstTemp[n_row2].n_porefiuni;
                                    lstProgramadetprolindet[n_row].n_porefitot = lstTemp[n_row2].n_porefitot;
                                    lstProgramadetprolindet[n_row].n_costar = lstTemp[n_row2].n_costar;
                                    lstProgramadetprolindet[n_row].n_ord = lstTemp[n_row2].n_ord;

                                    b_seencontro = true;
                                    break;
                                }
                            }

                            if (b_seencontro == false)                                             // SI NO LO ENCONTRAMOS LO INSERTAMOS
                            {
                                BE_PRO_PROGRAMADETPROLINDET entLinDet = new BE_PRO_PROGRAMADETPROLINDET();
                                entLinDet.n_idpro = 0;
                                entLinDet.n_idordpro = n_OrdenProduccion;
                                entLinDet.n_idite = n_IdProducto;
                                entLinDet.n_idrec = n_IdReceta;
                                entLinDet.n_idlin = n_IdLinea;
                                entLinDet.n_idtar = lstTemp[n_row2].n_idtar;
                                entLinDet.n_porefi = lstTemp[n_row2].n_porefi;
                                entLinDet.n_cankilpro = lstTemp[n_row2].n_cankilpro;
                                entLinDet.n_numpertar = lstTemp[n_row2].n_numpertar;
                                entLinDet.n_idequipo = lstTemp[n_row2].n_idequipo;
                                entLinDet.n_canequi = lstTemp[n_row2].n_canequi;
                                entLinDet.n_numpertarequ = lstTemp[n_row2].n_numpertarequ;
                                entLinDet.n_capkilporper = lstTemp[n_row2].n_capkilporper;
                                entLinDet.n_capkilporhorlin = lstTemp[n_row2].n_capkilporhorlin;
                                entLinDet.n_capkilporlintietra = lstTemp[n_row2].n_capkilporlintietra;
                                entLinDet.n_numpercal = lstTemp[n_row2].n_numpercal;
                                entLinDet.n_totprotietra = lstTemp[n_row2].n_totprotietra;
                                entLinDet.n_porefiuni = lstTemp[n_row2].n_porefiuni;
                                entLinDet.n_porefitot = lstTemp[n_row2].n_porefitot;
                                entLinDet.n_costar = lstTemp[n_row2].n_costar;
                                entLinDet.n_ord = lstTemp[n_row2].n_ord;

                                lstProgramadetprolindet.Add(entLinDet);
                            }
                        }
                    }
                }
                #endregion insertar_lineas_y_tareas
            }
            MyForm.Close();
            MyForm = null;
            
            booAgregando = false;
        }
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void CboMeses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            objRegistro.mysConec = mysConec;
            bool booResult = false;

            booResult = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboMeses.SelectedValue));
            if (booResult == true)
            {
                dtListar = objRegistro.dtListar;
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();
            }
        }
        private void CmdCalcLin_Click(object sender, EventArgs e)
        {
            if (FgLisPro.Rows.Count == 2)
            {
                MessageBox.Show("! No hay productos para visualizar la linea!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if  (Convert.ToString(FgLisPro.GetData(FgLisPro.Row, 7)) == "")
            {
                MessageBox.Show("! No ha especificado la hora de inicio de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            int n_idRec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 9).ToString());
            int n_idLin = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 10).ToString());
            int n_idPro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 11).ToString());
            int n_idOrdPro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 16).ToString());
            double n_canPro = Convert.ToDouble(FgLisPro.GetData(FgLisPro.Row, 5).ToString());

            VerLineas(n_idOrdPro, n_idPro, n_idRec, n_idLin, true, n_canPro);
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
        private void TxtFchIni_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                TxtFchIni.Text = "";
                TxtFchIni.CustomFormat = " ";
                TxtFchIni.Format = DateTimePickerFormat.Custom;
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
        private void TxtFchIni_ValueChanged(object sender, EventArgs e)
        {
            TxtFchIni.CustomFormat = "dd/MM/yyyy";
            TxtFchIni.Format = DateTimePickerFormat.Custom;
        }
        private void TxtFchFin_ValueChanged(object sender, EventArgs e)
        {
            TxtFchFin.CustomFormat = "dd/MM/yyyy";
            TxtFchFin.Format = DateTimePickerFormat.Custom;
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
    }
}
