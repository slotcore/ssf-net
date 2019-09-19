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

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmManSolicitudMateriales : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        BE_PRO_SOLICITUDMATERIALES entSolicitud = new BE_PRO_SOLICITUDMATERIALES();
        List<BE_PRO_SOLICITUDMATERIALESDET> lstSolicitudDet = new List<BE_PRO_SOLICITUDMATERIALESDET>();

        // OBJETOS LOCALES
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_pro_solicitudamateriales objRegistro = new CN_pro_solicitudamateriales();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_alm_inventario ObjItem = new CN_alm_inventario();
        CN_sun_unimed objUniMedSunat = new CN_sun_unimed();
        CN_pro_personal objPerPro = new CN_pro_personal();

        CN_pro_ordenproduccion objOrdenProduccion = new CN_pro_ordenproduccion();
        CN_pro_tareas objtareas = new CN_pro_tareas();
        CN_alm_inventariounimed objUniMed = new CN_alm_inventariounimed();
        CN_pro_productosrecetas objRecetas = new CN_pro_productosrecetas();
        CN_pro_productosrecetasinsumos objRecetasInsumo = new CN_pro_productosrecetasinsumos();
        CN_pro_productosrecetaslineas objLineas = new CN_pro_productosrecetaslineas();
        CN_sun_tipexi objTipExi = new CN_sun_tipexi();
        CN_pro_programa objPrograma = new CN_pro_programa();

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
        DataTable dtRecetasInsumos = new DataTable();
        DataTable dtLineas = new DataTable();
        DataTable dtTipExi = new DataTable();
        DataTable dtEquipos = new DataTable();
        DataTable dtProgConsulta = new DataTable();
        DataTable dtProgramaActual = new DataTable();

        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                                // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[10, 4];                                     // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlexIns = new string[8, 5];
        string[,] arrCabeceraFlexLisPro = new string[11, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;

        public FrmManSolicitudMateriales()
        {
            InitializeComponent();
        }
        private void label12_Click(object sender, EventArgs e)
        {
        }
        private void label19_Click(object sender, EventArgs e)
        {
        }
        private void FrmManSolicitudMateriales_Load(object sender, EventArgs e)
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
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtTipDoc, "n_id", "c_des");

            funDatos.ComboBoxCargarDataTable(CboPrograma, dtProgConsulta, "n_id", "c_programanumdoc");
            funDatos.ComboBoxCargarDataTable(CboItem, dtItems, "n_id", "c_despro");
            funDatos.ComboBoxCargarDataTable(CboUniMed, dtUniMed, "n_id", "c_despre");
            funDatos.ComboBoxCargarDataTable(CboReceta, dtRecetas, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboSol, dtPerPro, "n_id", "c_apenom");
            
        }
        void ConfigurarFormulario()
        {
            this.Height = 663;
            this.Width = 990;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;

            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            arrCabeceraFlexIns[0, 0] = "Insumo / Producto";
            arrCabeceraFlexIns[0, 1] = "400";
            arrCabeceraFlexIns[0, 2] = "C";
            arrCabeceraFlexIns[0, 3] = "";
            arrCabeceraFlexIns[0, 4] = "";

            arrCabeceraFlexIns[1, 0] = "Unidad Medida";
            arrCabeceraFlexIns[1, 1] = "60";
            arrCabeceraFlexIns[1, 2] = "C";
            arrCabeceraFlexIns[1, 3] = "";
            arrCabeceraFlexIns[1, 4] = "";

            arrCabeceraFlexIns[2, 0] = "Cantidad Teorica";
            arrCabeceraFlexIns[2, 1] = "80";
            arrCabeceraFlexIns[2, 2] = "D";
            arrCabeceraFlexIns[2, 3] = "0.000000";
            arrCabeceraFlexIns[2, 4] = "";

            arrCabeceraFlexIns[3, 0] = "Cantidad Requerida";
            arrCabeceraFlexIns[3, 1] = "80";
            arrCabeceraFlexIns[3, 2] = "D";
            arrCabeceraFlexIns[3, 3] = "0.000000";
            arrCabeceraFlexIns[3, 4] = "";

            arrCabeceraFlexIns[4, 0] = "Nº Lote";
            arrCabeceraFlexIns[4, 1] = "90";
            arrCabeceraFlexIns[4, 2] = "C";
            arrCabeceraFlexIns[4, 3] = "";
            arrCabeceraFlexIns[4, 4] = "";

            arrCabeceraFlexIns[5, 0] = "Id Item";
            arrCabeceraFlexIns[5, 1] = "0";
            arrCabeceraFlexIns[5, 2] = "N";
            arrCabeceraFlexIns[5, 3] = "";
            arrCabeceraFlexIns[5, 4] = "";

            arrCabeceraFlexIns[6, 0] = "Id Uni Med";
            arrCabeceraFlexIns[6, 1] = "0";
            arrCabeceraFlexIns[6, 2] = "N";
            arrCabeceraFlexIns[6, 3] = "";
            arrCabeceraFlexIns[6, 4] = "";

            funFlex.FlexMostrarDatos(FgInsumos, arrCabeceraFlexIns, dtListar, 2, false);

            arrCabeceraFlexLisPro[0, 0] = "Producto";
            arrCabeceraFlexLisPro[0, 1] = "350";
            arrCabeceraFlexLisPro[0, 2] = "C";
            arrCabeceraFlexLisPro[0, 3] = "";
            arrCabeceraFlexLisPro[0, 4] = "";

            arrCabeceraFlexLisPro[1, 0] = "Unidad Medida";
            arrCabeceraFlexLisPro[1, 1] = "60";
            arrCabeceraFlexLisPro[1, 2] = "C";
            arrCabeceraFlexLisPro[1, 3] = "";
            arrCabeceraFlexLisPro[1, 4] = "";

            arrCabeceraFlexLisPro[2, 0] = "Receta";
            arrCabeceraFlexLisPro[2, 1] = "100";
            arrCabeceraFlexLisPro[2, 2] = "C";
            arrCabeceraFlexLisPro[2, 3] = "";
            arrCabeceraFlexLisPro[2, 4] = "";

            arrCabeceraFlexLisPro[3, 0] = "Cantidad";
            arrCabeceraFlexLisPro[3, 1] = "80";
            arrCabeceraFlexLisPro[3, 2] = "D";
            arrCabeceraFlexLisPro[3, 3] = "0.00";
            arrCabeceraFlexLisPro[3, 4] = "";

            arrCabeceraFlexLisPro[4, 0] = "Fch. Entrega";
            arrCabeceraFlexLisPro[4, 1] = "80";
            arrCabeceraFlexLisPro[4, 2] = "F";
            arrCabeceraFlexLisPro[4, 3] = "dd/MM/yyyy";
            arrCabeceraFlexLisPro[4, 4] = "";

            arrCabeceraFlexLisPro[5, 0] = "Fch. Produccion";
            arrCabeceraFlexLisPro[5, 1] = "80";
            arrCabeceraFlexLisPro[5, 2] = "F";
            arrCabeceraFlexLisPro[5, 3] = "dd/MM/yyyy";
            arrCabeceraFlexLisPro[5, 4] = "";

            arrCabeceraFlexLisPro[6, 0] = "IdItem";
            arrCabeceraFlexLisPro[6, 1] = "0";
            arrCabeceraFlexLisPro[6, 2] = "N";
            arrCabeceraFlexLisPro[6, 3] = "0";
            arrCabeceraFlexLisPro[6, 4] = "";

            arrCabeceraFlexLisPro[7, 0] = "IdReceta";
            arrCabeceraFlexLisPro[7, 1] = "0";
            arrCabeceraFlexLisPro[7, 2] = "N";
            arrCabeceraFlexLisPro[7, 3] = "0";
            arrCabeceraFlexLisPro[7, 4] = "";
            
            arrCabeceraFlexLisPro[8, 0] = "IdOrdenProduccion";
            arrCabeceraFlexLisPro[8, 1] = "0";
            arrCabeceraFlexLisPro[8, 2] = "N";
            arrCabeceraFlexLisPro[8, 3] = "0";
            arrCabeceraFlexLisPro[8, 4] = "";

            arrCabeceraFlexLisPro[9, 0] = "Nº Solicitud Manteriales";
            arrCabeceraFlexLisPro[9, 1] = "100";
            arrCabeceraFlexLisPro[9, 2] = "C";
            arrCabeceraFlexLisPro[9, 3] = "";
            arrCabeceraFlexLisPro[9, 4] = "";

            arrCabeceraFlexLisPro[10, 0] = "ID RESPONSABLE";
            arrCabeceraFlexLisPro[10, 1] = "0";
            arrCabeceraFlexLisPro[10, 2] = "N";
            arrCabeceraFlexLisPro[10, 3] = "0";
            arrCabeceraFlexLisPro[10, 4] = "";

            funFlex.FlexMostrarDatos(FgLisPro, arrCabeceraFlexLisPro, dtListar, 2, false);
            
            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(40, ref arrCabeceraDg1);
        }
        void DataTableCargar()
        {
            bool b_result = false;

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(40);                             // INDICAMOS QUE ES EL FORMULARIO MAESTRO DE RECETAS

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objRegistro.mysConec = mysConec;
            b_result = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            if (b_result == true)
            {
                dtListar = objRegistro.dtLista;
            }

            ObjItem.mysConec = mysConec;
            dtItems = ObjItem.Listar(STU_SISTEMA.EMPRESAID);

            objUniMed.mysConec = mysConec;
            dtUniMed = objUniMed.Listar();

            objUniMedSunat.mysConec = mysConec;
            dtUniMedSunat = objUniMedSunat.Listar();

            objTipDoc.mysConec = mysConec;
            dtTipDoc = objTipDoc.Listar();

            // CARGAMOS EL PERSONAL DE PRODUCCION
            objPerPro.mysConec = mysConec;
            if (objPerPro.Listar(STU_SISTEMA.EMPRESAID) == true)
            {
                dtPerPro = objPerPro.dtPersonal;
            }

            objOrdenProduccion.mysConec = mysConec;
            dtOrdenProduccion = objOrdenProduccion.Listar(STU_SISTEMA.EMPRESAID, 0, 0);

            objtareas.mysConec = mysConec;
            dtTareas = objtareas.Listar(STU_SISTEMA.EMPRESAID);

            objRecetas.mysConec = mysConec;
            b_result = objRecetas.ListarTodasRecetas(STU_SISTEMA.EMPRESAID);
            if (b_result == true)
            {
                dtRecetas = objRecetas.dtRecetas;
            }

            objRecetasInsumo.mysConec = mysConec;
            b_result = objRecetasInsumo.ListarTodasLineas(STU_SISTEMA.EMPRESAID);
            if (b_result == true)
            {
                dtRecetasInsumos = objRecetasInsumo.dtInsumos;
            }

            objTipExi.mysConec = mysConec;
            dtTipExi = objTipExi.Listar();

            objPrograma.mysConec = mysConec;
            if (objPrograma.Consulta2(STU_SISTEMA.EMPRESAID) == true)
            {
                dtProgConsulta = objPrograma.dtConsulta;
            }
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
            
            DataTable dtResul = new DataTable();
            lstSolicitudDet.Clear();

            objRegistro.mysConec = mysConec;
            objRegistro.TraerRegistro(n_IdRegistro);

            entSolicitud = objRegistro.entSolicitud;
            lstSolicitudDet = objRegistro.lstSolicitudDet;

            CboTipDoc.SelectedValue = entSolicitud.n_idtipdoc;
            TxtFchReg.Text = entSolicitud.d_fchreg.ToString("dd/MM/yyyy");
            TxtNumSer.Text = entSolicitud.c_numser;
            TxtNumDoc.Text = entSolicitud.c_numdoc;
            CboSol.SelectedValue = entSolicitud.n_idsol;
            CboItem.SelectedValue = entSolicitud.n_idite;

            DataTable dtResult = new DataTable();

            dtResult = funDatos.DataTableFiltrar(dtUniMed, "n_idite = " + entSolicitud.n_idite + " AND n_default = 1");
            CboUniMed.SelectedValue = dtResult.Rows[0]["n_id"];
            TxtCanPro2.Text = entSolicitud.n_can.ToString("0.00");
            CboReceta.SelectedValue = entSolicitud.n_idrec;

            TxtNumSerOP.Text ="";
            TxtNumDocOP.Text ="";
            TxtFchOP.Text ="";
            TxtNumSerPro.Text ="";
            TxtNumDocPro.Text ="";
            TxtFcgProg.Text ="";
            TxtHorIni.Text ="";
            TxtHorFin.Text ="";
            TxtObs.Text ="";

            // MOSTRAMOS EL DETALLE DE LA SOLICITUD
            string c_dato;
            Double n_dato;
            int n_valor;
            int n_fila;

            FgInsumos.Rows.Count = 2;
            n_fila = 2;
            for (n_row = 0; n_row <= lstSolicitudDet.Count - 1; n_row++)
            {
                FgInsumos.Rows.Count = FgInsumos.Rows.Count + 1;

                // MOSTRAMOS EL NOMBRE DEL ITEM
                c_dato = lstSolicitudDet[n_row].n_idite.ToString("");
                c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", c_dato, "N").ToString();
                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 1, c_dato);                                                       // NOMBRE DEL PRODUCTO

                c_dato = lstSolicitudDet[n_row].n_idunimed.ToString("");
                c_dato = funDatos.DataTableBuscar(dtUniMedSunat, "n_id", "c_abr", c_dato, "N").ToString();
                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 2, c_dato);                                                       // UNIDAD DE MEDIDA

                n_dato = lstSolicitudDet[n_row].n_canteo;
                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 3, n_dato.ToString("0.000000"));                                  // CANTIDAD TEORICA DE LA RECETA

                n_dato = lstSolicitudDet[n_row].n_canent;
                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 4, n_dato.ToString("0.000000"));                                  // CANTIDAD ENTREGADA DE LA RECETA

                c_dato = lstSolicitudDet[n_row].c_numlot.ToString();
                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 5, c_dato);                                                       // NUMERO DE LOTE

                n_valor = lstSolicitudDet[n_row].n_idite;
                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 6, n_valor);                                                      // ID DEL ITEM

                n_valor = lstSolicitudDet[n_row].n_idunimed;
                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 7, n_valor);                                                      // ID DE LA UNIDAD DE MEDIDA
            }
        }
        void Nuevo()
        {
            booAgregando = true;
            n_QueHace = 1;
            Tab1.SelectedIndex = 0;
            //Tab1.TabPages[0].Enabled = false;

            booAgregando = false;

            PanelSol.Visible = true;
            Tab1.Enabled = false;
            ToolHerramientas.Enabled = false;

            PanelSol.Left = ((this.Width - PanelSol.Width) / 2);
            PanelSol.Top = ((this.Height - PanelSol.Height) / 2);

            FgLisPro.Rows.Count = 2;
            CboPrograma.SelectedValue = 0;
        }
        void Blanquea()
        {
            lstSolicitudDet.Clear();
            FgInsumos.Rows.Count = 2;

            CboTipDoc.SelectedValue = 0;
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            CboSol.SelectedValue = 0;
            CboItem.SelectedValue = 0;
            CboUniMed.SelectedValue = 0;
            TxtCanPro2.Text = "";
            CboReceta.SelectedValue = 0;
            TxtNumSerOP.Text = "";
            TxtNumDocOP.Text = "";
            TxtFchOP.Text = "";
            TxtNumSerPro.Text = "";
            TxtNumDocPro.Text = "";
            TxtFcgProg.Text = "";
            TxtHorIni.Text = "";
            TxtHorFin.Text = "";
            TxtObs.Text = "";
        }
        void Bloquea()
        {
            CboTipDoc.Enabled = !CboTipDoc.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            CboSol.Enabled = !CboSol.Enabled;
            CboItem.Enabled = !CboItem.Enabled;
            CboUniMed.Enabled = !CboUniMed.Enabled;
            TxtCanPro2.Enabled = !TxtCanPro2.Enabled;
            CboReceta.Enabled = !CboReceta.Enabled;
            TxtNumSerOP.Enabled = !TxtNumSerOP.Enabled;
            TxtNumDocOP.Enabled = !TxtNumDocOP.Enabled;
            TxtFchOP.Enabled = !TxtFchOP.Enabled;
            TxtNumSerPro.Enabled = !TxtNumSerPro.Enabled;
            TxtNumDocPro.Enabled = !TxtNumDocPro.Enabled;
            TxtFcgProg.Enabled = !TxtFcgProg.Enabled;
            TxtHorIni.Enabled = !TxtHorIni.Enabled;
            TxtHorFin.Enabled = !TxtHorFin.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
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

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[2].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            TxtFchReg.Focus();
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[2].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistro.ELiminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistro.mysConec = mysConec;
                    booResult = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
                    if (booResult == true)
                    {
                        dtListar = objRegistro.dtLista;
                        // MOSTRAMOS LOS DATOS EN LA GRILLA
                        ListarItems();
                    }
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistro.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
            //if (CamposOK() == false)
            //{
            //    return booResultado;
            //}
            //AsignarEntidad();

            if (n_QueHace == 1)
            {
                booResultado = objRegistro.Insertar(entSolicitud, lstSolicitudDet);
            }

            if (n_QueHace == 2)
            {
                // booResultado = objRegistro.Actualizar(entPrograma, lstProgramaDet, lstProgramaedetpro);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ No se pudo guardar el registro por el siguiente motivo ! " + objRegistro.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_fila =0;
            int n_dato =0;
            double n_valor = 0;
            string c_dato;

            entSolicitud.n_idemp =  STU_SISTEMA.EMPRESAID;
            if (n_QueHace == 1)
            {
                entSolicitud.n_id = 0;
            }
            else
            {
                entSolicitud.n_id = entSolicitud.n_id;
            }

            entSolicitud.n_idtipdoc = 72;
            entSolicitud.c_numser = TxtNumSer.Text;
            entSolicitud.c_numdoc = TxtNumDoc.Text;
            entSolicitud.d_fchreg = Convert.ToDateTime(TxtFchReg.Text);
            entSolicitud.n_idsol = Convert.ToInt32(CboSol.SelectedValue);
            entSolicitud.n_idprogra = Convert.ToInt32(LblIdProgramaProduccion.Text);
            entSolicitud.n_idordpro = Convert.ToInt32(LblIdOrdenProduccion.Text);
            entSolicitud.n_idite = Convert.ToInt32(CboItem.SelectedValue);
            entSolicitud.n_idrec= Convert.ToInt32(CboReceta.SelectedValue);
            entSolicitud.c_obs = TxtObs.Text;
            entSolicitud.n_anotra = STU_SISTEMA.ANOTRABAJO;
            entSolicitud.n_mestra = STU_SISTEMA.MESTRABAJO;
                        
            lstSolicitudDet.Clear();

            // CARGAMOS LA LISTA DE ORDENES DE PRODUCCION
            for(n_fila = 2; n_fila <= (FgInsumos.Rows.Count - 1); n_fila++)
            {
                BE_PRO_SOLICITUDMATERIALESDET entSoliDet = new BE_PRO_SOLICITUDMATERIALESDET();

                entSoliDet.n_idsol = 0;                                      // ID DE LA SOLICITUD
                
                n_dato = Convert.ToInt32(FgInsumos.GetData(n_fila, 6));
                entSoliDet.n_idite = n_dato;                                 // ID DEL ITEM

                n_dato = Convert.ToInt32(FgInsumos.GetData(n_fila, 8));      // ID DE LA UNIDAD DE MEDIDA
                entSoliDet.n_idunimed = n_dato;
                
                n_valor = Convert.ToDouble(FgInsumos.GetData(n_fila, 4));
                entSoliDet.n_canteo = n_valor;                               // CANTIDAD TEORICA DE LA RECETA

                n_valor = Convert.ToDouble(FgInsumos.GetData(n_fila, 5));
                entSoliDet.n_canent = n_valor;                               // CANTIDAD ENTREGADA

                c_dato = FgInsumos.GetData(n_fila, 6).ToString();
                entSoliDet.c_numlot = c_dato;                                // NUMERO DE LOTE

                entSoliDet.n_impval = 0;                                     // VALOR DEL INSUMO

                lstSolicitudDet.Add(entSoliDet);
            }           
        }
        bool CamposOK()
        {
            bool booEstado = true;
            int n_fila = 0;

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

            if (TxtFchReg.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de registro !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchReg.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboSol.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado en nombre del solicitante de los insumos !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboSol.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboItem.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboItem.Focus();
                booEstado = false;
                return booEstado;
            }

            if (FgInsumos.Rows.Count == 2)
            {
                MessageBox.Show("¡ La solicitud de materiales debe de tener como minimo un insumo !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgInsumos.Focus();
                booEstado = false;
                return booEstado;
            }
            else
            {
                for (n_fila = 2; n_fila <= FgInsumos.Rows.Count - 1; n_fila++)
                {
                    if (Convert.ToDouble(funFunciones.NulosN(FgInsumos.GetData(n_fila, 5))) == 0)
                    {
                        MessageBox.Show("¡ Debe de indicar la cantidad de insumo entregada en la fila Nº" + n_fila.ToString() + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        FgInsumos.Focus();
                        booEstado = false;
                        return booEstado;
                    }
                }
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
                    dtListar = objRegistro.dtLista;
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
        private void FrmManSolicitudMateriales_Activated(object sender, EventArgs e)
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
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[2].CellValue(DgLista.Row).ToString());
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
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[2].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }
        private void FrmManSolicitudMateriales_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
        }
        private void CmdBusPro_Click(object sender, EventArgs e)
        {

        }
        private void CboPrograma_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (Convert.ToInt32(CboPrograma.SelectedValue) == 0)
            {
                return;
            }
            booAgregando = true;
            DataTable dtResult = new DataTable();
            
            FgLisPro.Rows.Count = 2;

            dtResult = funDatos.DataTableFiltrar(dtProgConsulta, "n_id = " + Convert.ToInt32(CboPrograma.SelectedValue).ToString() + "");
            if (dtResult.Rows.Count != 0)
            {
                TxtFchIni.Text = Convert.ToDateTime(dtResult.Rows[0]["d_fchini"]).ToString("dd/MM/yyyy");
                TxtFchFin.Text = Convert.ToDateTime(dtResult.Rows[0]["d_fchfin"]).ToString("dd/MM/yyyy");
            }

            TxtFchProduccion.Text = "";
            TxtFchProduccion.CustomFormat = " ";

            TxtFchProduccion.Format = DateTimePickerFormat.Custom;

            objPrograma.mysConec = mysConec;
            if (objPrograma.Consulta1(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboPrograma.SelectedValue)) == true)
            {
                dtProgramaActual = objPrograma.dtConsulta;
                MostrarProductos(dtProgramaActual);
            }
            booAgregando = false;

        }
        void MostrarProductos(DataTable dtResult)
        {
            int n_row = 0;

            if (dtResult.Rows.Count != 0)
            {
                for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                {
                    FgLisPro.Rows.Count = FgLisPro.Rows.Count + 1;
                    FgLisPro.SetData(FgLisPro.Rows.Count - 1, 1, dtResult.Rows[n_row]["c_despro"]);
                    //FgLisPro.SetData(FgLisPro.Rows.Count - 1, 2, dtResult.Rows[0]["c_despro"]);
                    //FgLisPro.SetData(FgLisPro.Rows.Count - 1, 3, dtResult.Rows[0]["c_despro"]);
                    FgLisPro.SetData(FgLisPro.Rows.Count - 1, 4, Convert.ToDouble(dtResult.Rows[n_row]["n_can"]).ToString("0.00"));
                    FgLisPro.SetData(FgLisPro.Rows.Count - 1, 5, Convert.ToDateTime(dtResult.Rows[n_row]["d_fchent"]).ToString("dd/MM/yyyy"));
                    FgLisPro.SetData(FgLisPro.Rows.Count - 1, 6, Convert.ToDateTime(dtResult.Rows[n_row]["d_fchpro"]).ToString("dd/MM/yyyy"));

                    FgLisPro.SetData(FgLisPro.Rows.Count - 1, 7, dtResult.Rows[n_row]["n_idite"]);
                    FgLisPro.SetData(FgLisPro.Rows.Count - 1, 8, dtResult.Rows[n_row]["n_idrec"]);
                    FgLisPro.SetData(FgLisPro.Rows.Count - 1, 9, dtResult.Rows[n_row]["n_ordproid"]);
                    FgLisPro.SetData(FgLisPro.Rows.Count - 1, 10, dtResult.Rows[n_row]["c_estado"]);

                    FgLisPro.SetData(FgLisPro.Rows.Count - 1, 11, dtResult.Rows[n_row]["n_idres"]);
                }
            }
        }
        private void TxtFchProduccion_Validated(object sender, EventArgs e)
        {
            //DataTable dtResult = new DataTable();

            //FgLisPro.Rows.Count = 2;

            //if (TxtFchProduccion.Text == " ")
            //{
            //    MostrarProductos(dtProgramaActual);
            //    return;
            //}
            //else 
            //{
            //    dtResult = funDatos.DataTableFiltrar(dtProgramaActual, "d_fchpro = '" + TxtFchProduccion.Text + "'");
            //    if (dtResult.Rows.Count != 0)
            //    {
            //        MostrarProductos(dtResult);
            //    }
            //}
            
            
            //objPrograma.mysConec = mysConec;

            //if (objPrograma.Consulta1(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboPrograma.SelectedValue)) == true)
            //{
            //    dtResult = objPrograma.dtConsulta;
            //    if (dtResult.Rows.Count != 0)
            //    {
            //        dtResult = funDatos.DataTableFiltrar(dtResult, "d_fchpro = '" + TxtFchProduccion.Text + "'");
            //        if (dtResult.Rows.Count != 0)
            //        {
            //            MostrarProductos(dtResult);
            //        }
            //    }
            //}
        }
        private void TxtFchProduccion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")                            //  SI BLANQUEAMOS LAS FECHA MOSTRAMOS TODOS LOS PRODEUCTOS
            {
                TxtFchProduccion.Text = "";
                TxtFchProduccion.CustomFormat = " ";
                
                TxtFchProduccion.Format = DateTimePickerFormat.Custom;

                MostrarProductos(dtProgramaActual);
            }
        }
        private void TxtFchProduccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtFchIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtFchFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboPrograma_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtFchProduccion_ValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            TxtFchProduccion.CustomFormat = "dd/MM/yyyy";
            TxtFchProduccion.Format = DateTimePickerFormat.Custom;

            DataTable dtResult = new DataTable();

            FgLisPro.Rows.Count = 2;

            if (TxtFchProduccion.Text == " ")
            {
                MostrarProductos(dtProgramaActual);
                return;
            }
            else
            {
                dtResult = funDatos.DataTableFiltrar(dtProgramaActual, "d_fchpro = '" + TxtFchProduccion.Text + "'");
                if (dtResult.Rows.Count != 0)
                {
                    MostrarProductos(dtResult);
                }
            }
        }
        private void CmdCan_Click(object sender, EventArgs e)
        {
            n_QueHace = 3;
            PanelSol.Visible = false;
            Tab1.Enabled = true;
            ToolHerramientas.Enabled = true;
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
                int n_idPro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 7).ToString());
                int n_idRec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 8).ToString());

                //VerReceta(n_idPro, n_idRec);
                SSF_NET_Produccion.Cls_Funciones funPro = new SSF_NET_Produccion.Cls_Funciones();
                funPro.dtItems = dtItems;
                funPro.dtTipExi = dtTipExi;
                funPro.dtUniMedSunat = dtUniMedSunat;
                funPro.mysConec = mysConec;
                funPro.VerReceta(n_idPro, n_idRec);
            }
        }
        private void CmdGra_Click(object sender, EventArgs e)
        {
            if (FgLisPro.GetData(FgLisPro.Row, 10).ToString() == "SOLICITADO")
            {
                MessageBox.Show("! Ya se emitio la solicitud de materiales para este producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            DataTable dtResult = new DataTable();
            int n_row = 0;
            int n_tipo = 0;
            int n_idPro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 7).ToString());            // ID DEL PRODUCTO
            int n_idRec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 8).ToString());            // ID DE OLA RECETA
            int n_idordpro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 9).ToString());         // ID DE LA ORDEN DE PRODUCCION
            int n_idres = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 11).ToString());           // ID DEL RESPONSABLE DE LA PRODUCCION
            double n_canpro = Convert.ToDouble(FgLisPro.GetData(FgLisPro.Row, 4));                  // CANTIDAD DEL PRODUCTO
            string c_fchent = FgLisPro.GetData(FgLisPro.Row, 5).ToString();                         // FECHA DE ENTREGA        
            string c_numdoc = "";

            dtResult = funDatos.DataTableFiltrar(dtRecetas, "n_id = " + n_idRec + "");

            for (n_tipo = 0; n_tipo <= dtTipExi.Rows.Count - 1; n_tipo++)                                   // RECORREMOS EL TIPO DE EXISTENCIA
            {
                dtResult = funDatos.DataTableFiltrar(dtRecetasInsumos, "n_idrec = " + n_idRec + "");                                 // FILTRAMOS LOS INSUMOS DE LA RECETA
                dtResult = funDatos.DataTableFiltrar(dtResult, "n_idtipexi = " + dtTipExi.Rows[n_tipo]["n_id"].ToString() + "");     // FILTRAMOS LOS ITEMS DEL TIPO DE ITEM ACTUAL
                if (dtResult.Rows.Count != 0)                                                               // SI EXISTE ITEMS DE LA RECETA CON EL TIPO DE DE EXISTENCIA ACTUAL, IMPRIMIMOS UNA SOLICITUD
                { 
                    objTipDoc.mysConec = mysConec;
                    c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 72, "0001");                   // MOSTRAMOS EL ULTIMO NUMERO DE LA PROGRAMACION

                    // CREAMOS LA CABECERA DE LA SOLICITUD
                    entSolicitud.n_idemp = STU_SISTEMA.EMPRESAID;
		            entSolicitud.n_id = 0;
		            entSolicitud.n_idtipdoc = 72;
		            entSolicitud.c_numser = "0001";
                    entSolicitud.c_numdoc = c_numdoc;
		            entSolicitud.d_fchreg= DateTime.Now;
                    entSolicitud.n_idsol = n_idres;                                                         // EL RESPONSABLE DE LA PRODUCCION SERA EL RESPONSABLE DE SOLICITAR LOS INSUMOS
		            entSolicitud.n_idprogra = Convert.ToInt32(CboPrograma.SelectedValue);
		            entSolicitud.n_idordpro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row,9));
		            entSolicitud.n_idite = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row,7));
		            entSolicitud.n_idrec = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row,8));
                    entSolicitud.d_fchent = Convert.ToDateTime(c_fchent);
		            entSolicitud.c_obs = "SOLICITUD GENERADA AUTOMATICAMENTE DEL PROGRAMA DE PRODUCCION";
		            entSolicitud.n_anotra = STU_SISTEMA.ANOTRABAJO;
		            entSolicitud.n_mestra = STU_SISTEMA.MESTRABAJO;
                    entSolicitud.n_idalm = 0;
                    entSolicitud.n_can = n_canpro;

                    lstSolicitudDet.Clear();
                    // CREAMOS EL DETALLE DE LA SOLICITUD               
                    for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                    {
                        BE_PRO_SOLICITUDMATERIALESDET entSolicitudDet = new BE_PRO_SOLICITUDMATERIALESDET();
                        entSolicitudDet.n_idsol = 0;
                        entSolicitudDet.n_idite = Convert.ToInt32(dtResult.Rows[n_row]["n_idite"]);
                        entSolicitudDet.n_idunimed = Convert.ToInt32(dtResult.Rows[n_row]["n_idunimed"]);
                        entSolicitudDet.n_canteo = Convert.ToDouble(dtResult.Rows[n_row]["n_can"]);
                        entSolicitudDet.n_canent = Convert.ToDouble(dtResult.Rows[n_row]["n_can"]) * n_canpro;
                        entSolicitudDet.c_numlot = "";
                        entSolicitudDet.n_impval = 0;
                        lstSolicitudDet.Add(entSolicitudDet);
                    }
                    
                    Grabar();
                }
            }

            bool b_Result = false;
            string c_producto = FgLisPro.GetData(FgLisPro.Row, 1).ToString();
            MessageBox.Show("! La solicitud de materiales para el producto : " + c_producto + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            CmdCan_Click(sender, e);

            // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
            objRegistro.mysConec = mysConec;
            b_Result = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            if (b_Result == true)
            {
                dtListar = objRegistro.dtLista;
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();
            }
            return;
        }
        private void CmdCerrar_Click(object sender, EventArgs e)
        {
            PanelSol.Visible = false;
            Tab1.Enabled = true;
            ToolHerramientas.Enabled = true;
        }
        private void imprimirRecetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Imprimir();
        }
        void Imprimir()
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[2].CellValue(DgLista.Row).ToString());
            CN_pro_solicitudamateriales FunSol = new CN_pro_solicitudamateriales();
            FunSol.mysConec = mysConec;
            FunSol.STU_SISTEMA = STU_SISTEMA;
            FunSol.ImprimirSolicitudMat(intIdRegistro);
        }
        private void ToolMenuImprimir_ButtonClick(object sender, EventArgs e)
        {
            Imprimir();
        }
    }
}
