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
using SIAC_Negocio.Planilla;
using SIAC_Entidades.Produccion;
using SIAC_Entidades.Almacen;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using System.Configuration;

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmSolicitudTareas : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_DeDonde = 0;                                               // INDICA DE DONDE SE ESTA INVOCANDO AL FORMULARIO 1 = DE PRODUCCION;   2 = DESDE PLANILLAS

        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_pro_solicitudtareas objRegistro = new CN_pro_solicitudtareas();

        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_alm_inventario ObjItem = new CN_alm_inventario();
        CN_sun_unimed objUniMedSunat = new CN_sun_unimed();
        CN_pro_personal objPerPro = new CN_pro_personal();
        CN_pro_ordenproduccion objOrdenProduccion = new CN_pro_ordenproduccion();
        CN_alm_inventariounimed objUniMed = new CN_alm_inventariounimed();
        CN_pro_productosrecetas objRecetas = new CN_pro_productosrecetas();
        CN_pro_productosrecetaslineas objLineas = new CN_pro_productosrecetaslineas();
        CN_pro_productosrecetaslineastareas objTareas = new CN_pro_productosrecetaslineastareas();
        CN_pro_produccion objProduccion = new CN_pro_produccion();
        CN_pro_tareas objTar = new CN_pro_tareas();
        CN_pro_productosrecetas objReceta = new CN_pro_productosrecetas();
        
        CN_pla_setup o_plaset = new CN_pla_setup();
        CN_sys_empresalocal o_Local = new CN_sys_empresalocal();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Convertir funCon = new Convertir();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();

        // DATATABLE LOCALES
        DataTable dtForm = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtLista = new DataTable();
        DataTable dtTipDoc = new DataTable();
        DataTable dtNumDocRef = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtPerPro = new DataTable();
        DataTable dtOrdenProduccion = new DataTable();
        DataTable dtUniMed = new DataTable();
        DataTable dtUniMedSunat = new DataTable();
        DataTable dtLineas = new DataTable();
        DataTable dtLineasTareas = new DataTable();
        DataTable dtProgConsulta = new DataTable();
        DataTable dtProgramaActual = new DataTable();
        DataTable dtProduccion = new DataTable();
        DataTable dtTareas = new DataTable();
        DataTable dtReceta = new DataTable();
        DataTable dtEmpleados = new DataTable();
        DataTable dtSetup = new DataTable();
        DataTable dtLocal = new DataTable();

        // ENTIDADES LOCALES
        BE_PRO_PRODUCCION BE_Produccion = new BE_PRO_PRODUCCION();
        BE_PRO_SOLICITUDTAREAS entTar = new BE_PRO_SOLICITUDTAREAS();
        List<BE_PRO_SOLICITUDTAREASCAB> lstTarCab = new List<BE_PRO_SOLICITUDTAREASCAB>();
        List<BE_PRO_SOLICITUDTAREASDET> lstTarDet = new List<BE_PRO_SOLICITUDTAREASDET>();
        
        // VARIABLES LOCALES
        string C_IDLOCAL = "";
        int n_QueHace = 3;                                                               // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[9, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[4, 5];
        string[,] arrCabeceraFlexTar = new string[12, 5];
        string[,] arrCabeceraFlexPer = new string[9, 5];

        int n_FilaTareaActual = 0;
        double n_PRECIOHORA = 0;
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmSolicitudTareas()
        {
            InitializeComponent();
        }
        private void FrmSolicitudTareas_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtTipDoc, "n_id", "c_des");

            funDatos.ComboBoxCargarDataTable(CboSol, dtPerPro, "n_id", "c_apenom");
            funDatos.ComboBoxCargarDataTable(CboLocal, dtLocal, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 650;
            this.Width = 1050;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();

            string c_nomarc = ConfigurationManager.AppSettings["PathIniFile"];
            C_IDLOCAL = funDatos.IniLeerSeccion(c_nomarc, "SISTEMA", "LOCAL").ToString();

            if (n_DeDonde == 2) { ToolVisar.Visible = true; }

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexTar[0, 0] = "Tarea";
            arrCabeceraFlexTar[0, 1] = "200";
            arrCabeceraFlexTar[0, 2] = "C";
            arrCabeceraFlexTar[0, 3] = "";
            arrCabeceraFlexTar[0, 4] = "";

            arrCabeceraFlexTar[1, 0] = "Fch. Proceso";
            arrCabeceraFlexTar[1, 1] = "70";
            arrCabeceraFlexTar[1, 2] = "F";
            arrCabeceraFlexTar[1, 3] = "";
            arrCabeceraFlexTar[1, 4] = "";

            arrCabeceraFlexTar[2, 0] = "Hora Inicio";
            arrCabeceraFlexTar[2, 1] = "50";
            arrCabeceraFlexTar[2, 2] = "H";
            arrCabeceraFlexTar[2, 3] = "HH:MM";
            arrCabeceraFlexTar[2, 4] = "";

            arrCabeceraFlexTar[3, 0] = "Hora Termino";
            arrCabeceraFlexTar[3, 1] = "50";
            arrCabeceraFlexTar[3, 2] = "H";
            arrCabeceraFlexTar[3, 3] = "HH:MM";
            arrCabeceraFlexTar[3, 4] = "";

            arrCabeceraFlexTar[4, 0] = "Nº Personas";
            arrCabeceraFlexTar[4, 1] = "70";
            arrCabeceraFlexTar[4, 2] = "N";
            arrCabeceraFlexTar[4, 3] = "";
            arrCabeceraFlexTar[4, 4] = "";

            arrCabeceraFlexTar[5, 0] = "Unidad Medida";
            arrCabeceraFlexTar[5, 1] = "60";
            arrCabeceraFlexTar[5, 2] = "C";
            arrCabeceraFlexTar[5, 3] = "";
            arrCabeceraFlexTar[5, 4] = "";

            arrCabeceraFlexTar[6, 0] = "Cantidad";
            arrCabeceraFlexTar[6, 1] = "70";
            arrCabeceraFlexTar[6, 2] = "D";
            arrCabeceraFlexTar[6, 3] = "";
            arrCabeceraFlexTar[6, 4] = "";

            arrCabeceraFlexTar[7, 0] = "Costo Tarea";
            arrCabeceraFlexTar[7, 1] = "70";
            arrCabeceraFlexTar[7, 2] = "D";
            arrCabeceraFlexTar[7, 3] = "";
            arrCabeceraFlexTar[7, 4] = "";

            arrCabeceraFlexTar[8, 0] = "Id Tarea";
            arrCabeceraFlexTar[8, 1] = "0";
            arrCabeceraFlexTar[8, 2] = "N";
            arrCabeceraFlexTar[8, 3] = "";
            arrCabeceraFlexTar[8, 4] = "";

            arrCabeceraFlexTar[9, 0] = "Imprimir";
            arrCabeceraFlexTar[9, 1] = "40";
            arrCabeceraFlexTar[9, 2] = "B";
            arrCabeceraFlexTar[9, 3] = "";
            arrCabeceraFlexTar[9, 4] = "";

            arrCabeceraFlexTar[10, 0] = "Orden";
            arrCabeceraFlexTar[10, 1] = "40";
            arrCabeceraFlexTar[10, 2] = "N";
            arrCabeceraFlexTar[10, 3] = "";
            arrCabeceraFlexTar[10, 4] = "";

            arrCabeceraFlexTar[11, 0] = "n_idreg";
            arrCabeceraFlexTar[11, 1] = "0";
            arrCabeceraFlexTar[11, 2] = "N";
            arrCabeceraFlexTar[11, 3] = "";
            arrCabeceraFlexTar[11, 4] = "";

            funFlex.FlexMostrarDatos(FgTar, arrCabeceraFlexTar, dtLista, 2, false);

            arrCabeceraFlexPer[0, 0] = "Nº";
            arrCabeceraFlexPer[0, 1] = "30";
            arrCabeceraFlexPer[0, 2] = "N";
            arrCabeceraFlexPer[0, 3] = "";
            arrCabeceraFlexPer[0, 4] = "";

            arrCabeceraFlexPer[1, 0] = "Persona";
            arrCabeceraFlexPer[1, 1] = "300";
            arrCabeceraFlexPer[1, 2] = "C";
            arrCabeceraFlexPer[1, 3] = "";
            arrCabeceraFlexPer[1, 4] = "";

            arrCabeceraFlexPer[2, 0] = "Hora Inicio";
            arrCabeceraFlexPer[2, 1] = "60";
            arrCabeceraFlexPer[2, 2] = "H";
            arrCabeceraFlexPer[2, 3] = "HH:MM";
            arrCabeceraFlexPer[2, 4] = "";

            arrCabeceraFlexPer[3, 0] = "Hora Termino";
            arrCabeceraFlexPer[3, 1] = "60";
            arrCabeceraFlexPer[3, 2] = "H";
            arrCabeceraFlexPer[3, 3] = "HH:MM";
            arrCabeceraFlexPer[3, 4] = "";

            arrCabeceraFlexPer[4, 0] = "Nº Envases";
            arrCabeceraFlexPer[4, 1] = "60";
            arrCabeceraFlexPer[4, 2] = "N";
            arrCabeceraFlexPer[4, 3] = "";
            arrCabeceraFlexPer[4, 4] = "";

            arrCabeceraFlexPer[5, 0] = "Peso Bruto";
            arrCabeceraFlexPer[5, 1] = "60";
            arrCabeceraFlexPer[5, 2] = "D";
            arrCabeceraFlexPer[5, 3] = "0.00";
            arrCabeceraFlexPer[5, 4] = "";

            arrCabeceraFlexPer[6, 0] = "Cantidad";
            arrCabeceraFlexPer[6, 1] = "70";
            arrCabeceraFlexPer[6, 2] = "D";
            arrCabeceraFlexPer[6, 3] = "";
            arrCabeceraFlexPer[6, 4] = "";

            arrCabeceraFlexPer[7, 0] = "id Persoma";
            arrCabeceraFlexPer[7, 1] = "0";
            arrCabeceraFlexPer[7, 2] = "N";
            arrCabeceraFlexPer[7, 3] = "";
            arrCabeceraFlexPer[7, 4] = "";

            arrCabeceraFlexPer[8, 0] = "Horas Decimal";
            arrCabeceraFlexPer[8, 1] = "0";
            arrCabeceraFlexPer[8, 2] = "D";
            arrCabeceraFlexPer[8, 3] = "";
            arrCabeceraFlexPer[8, 4] = "";

            funFlex.FlexMostrarDatos(FgPer, arrCabeceraFlexPer, dtLista, 2, false);

            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(45, ref arrCabeceraDg1);
        }
        void DataTableCargar()
        {
            bool b_result = false;

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(45);                             // INDICAMOS QUE ES EL FORMULARIO MAESTRO DE RECETAS

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objRegistro.mysConec = mysConec;
            b_result = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            if (b_result == true)
            {
                dtLista = objRegistro.dtLista;
            }

            ObjItem.mysConec = mysConec;
            dtItems = ObjItem.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);

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

            objLineas.mysConec = mysConec;
            objLineas.ListarTodasLineas(STU_SISTEMA.EMPRESAID);
            dtLineas = objLineas.dtLineas;

            objTareas.mysConec = mysConec;
            objTareas.ListarTodasTareas(STU_SISTEMA.EMPRESAID);
            dtLineasTareas = objTareas.dtLineasTar;

            objTar.mysConec = mysConec;
            dtTareas = objTar.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objReceta.mysConec = mysConec;
            objReceta.ListarTodasRecetas(STU_SISTEMA.EMPRESAID);
            dtReceta = objReceta.dtRecetas;

            CN_pla_empleados objEmpleado = new CN_pla_empleados(STU_SISTEMA);
            objEmpleado.STU_SISTEMA = STU_SISTEMA;
            objEmpleado.Listar(STU_SISTEMA.EMPRESAID);
            dtEmpleados = objEmpleado.dtLista;
            objEmpleado = null;

            o_plaset.mysConec = mysConec;
            o_plaset.Listar(STU_SISTEMA.EMPRESAID);
            dtSetup = o_plaset.dtLista;
            n_PRECIOHORA = Convert.ToDouble(dtSetup.Rows[0]["n_imppaghordes"]);

            o_Local.mysConec = mysConec;
            dtLocal = o_Local.Listar(STU_SISTEMA.EMPRESAID, 0);
        }
        void ListarItems()
        {
            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
            LblNumReg.Text = (dtLista.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtLista, true);
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
            string c_dato = "";

            DataTable dtResul = new DataTable();
            lstTarCab.Clear();
            lstTarDet.Clear();
            FgPer.Rows.Count = 2;
            FgTar.Rows.Count = 2;

            objRegistro.TraerRegistro(n_IdRegistro);
            entTar = objRegistro.entSolicitud;
            lstTarCab = objRegistro.lstSolicitudCab;
            lstTarDet = objRegistro.lstSolicitudDet;

            CboTipDoc.SelectedValue = entTar.n_idtipdoc;
            TxtFchReg.Text = Convert.ToDateTime(entTar.d_fchreg).ToString();
            TxtNumSer.Text = entTar.c_numser;
            TxtNumDoc.Text = entTar.c_numdoc;
            CboSol.SelectedValue = entTar.n_idsol;

            objProduccion.mysConec = mysConec;
            objProduccion.TraerRegistro(entTar.n_idpro);
            BE_Produccion = objProduccion.EntProduccion;

            TxtNumPro.Text = BE_Produccion.c_numser + "-" + BE_Produccion.c_numdoc;
            LblIdProduccion.Text = entTar.n_idpro.ToString();
            TxtDesPro.Text = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", BE_Produccion.n_idpro.ToString(), "N").ToString();
            LblIdProd.Text = BE_Produccion.n_idpro.ToString();
            TxtUniMed.Text = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_abrpre", BE_Produccion.n_idunimed.ToString(), "N").ToString();
            TxtCanPro2.Text = BE_Produccion.n_canpro.ToString("0.00");
            TxtFchPro.Text = BE_Produccion.d_fchpro.ToString("dd/MM/yyyy");
            TxtReceta.Text = funDatos.DataTableBuscar(dtReceta, "n_id", "c_des", BE_Produccion.n_idrec.ToString(), "N").ToString();
            LblIdReceta.Text = BE_Produccion.n_idrec.ToString();
            TxtObs.Text = BE_Produccion.c_obs;
            CboLocal.SelectedValue = Convert.ToInt32(entTar.n_idlocal);

            FgTar.Rows.Count = 2;
            for (n_row = 0; n_row <= lstTarCab.Count - 1; n_row++)
            {
                FgTar.Rows.Count = FgTar.Rows.Count + 1;

                c_dato = funDatos.DataTableBuscar(dtTareas, "n_id", "c_des", lstTarCab[n_row].n_idtar.ToString(), "N").ToString();
                FgTar.SetData(FgTar.Rows.Count - 1, 1, c_dato);

                c_dato = null;
                if (funFunciones.NulosC(lstTarCab[n_row].d_fchtra) != "")
                { 
                    c_dato = Convert.ToDateTime(lstTarCab[n_row].d_fchtra).ToString("dd/MM/yyyy");
                }
                FgTar.SetData(FgTar.Rows.Count - 1, 2, c_dato);

                c_dato = funFunciones.NulosC(lstTarCab[n_row].h_horini);
                FgTar.SetData(FgTar.Rows.Count - 1, 3, c_dato);

                c_dato = funFunciones.NulosC(lstTarCab[n_row].h_horfin);
                FgTar.SetData(FgTar.Rows.Count - 1, 4, c_dato);

                c_dato = funFunciones.NulosC(lstTarCab[n_row].n_numper);
                FgTar.SetData(FgTar.Rows.Count - 1, 5, c_dato);

                c_dato = funDatos.DataTableBuscar(dtTareas, "n_id", "c_unimedabr", lstTarCab[n_row].n_idtar.ToString(), "N").ToString();
                FgTar.SetData(FgTar.Rows.Count - 1, 6, c_dato);

                c_dato = funFunciones.NulosC(lstTarCab[n_row].n_can);
                FgTar.SetData(FgTar.Rows.Count - 1, 7, c_dato);

                c_dato = lstTarCab[n_row].n_idtar.ToString();
                FgTar.SetData(FgTar.Rows.Count - 1, 9, c_dato);

                c_dato = lstTarCab[n_row].n_ord.ToString();
                FgTar.SetData(FgTar.Rows.Count - 1, 11, c_dato);

                c_dato = lstTarCab[n_row].n_id.ToString();
                FgTar.SetData(FgTar.Rows.Count - 1, 12, c_dato);
            }
            if (FgTar.Rows.Count != 2)
            { 
                LblNomTar.Text = funFunciones.NulosC(FgTar.GetData(2, 1)).ToString();
                MostrarPersonas();
            }
        }
        void Nuevo()
        {
            n_QueHace = 1;
            booAgregando = true;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            Tab1.SelectedIndex = 1;
            CboTipDoc.SelectedValue = 82;
            TxtNumSer.Text = "0001";
            TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 82, "0001");
            OptProPen.Checked = true;

            OptProPen.Visible = true;
            OptProConTar.Visible = true;
            label20.Visible = true;
            CboLocal.SelectedValue = Convert.ToInt16(C_IDLOCAL);
            booAgregando = false;
            CboSol.Focus();
        }
        void Blanquea()
        {
            lstTarCab.Clear();
            FgTar.Rows.Count = 2;
            FgPer.Rows.Count = 2;

            CboTipDoc.SelectedValue = 0;
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            CboSol.SelectedValue = 0;
            TxtNumPro.Text = "";
            TxtDesPro.Text = "";
            TxtUniMed.Text = "";
            TxtCanPro2.Text = "";
            TxtNumSerOP.Text = "";
            TxtNumDocOP.Text = "";

            TxtObs.Text = "";

            lstTarCab.Clear();
            lstTarDet.Clear();
        }
        void Bloquea()
        {
            //CboTipDoc.Enabled = !CboTipDoc.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            CboSol.Enabled = !CboSol.Enabled;
            CboLocal.Enabled = !CboLocal.Enabled;
            TxtNumPro.Enabled = !TxtNumPro.Enabled;
            TxtDesPro.Enabled = !TxtDesPro.Enabled;
            TxtUniMed.Enabled = !TxtUniMed.Enabled;
            TxtCanPro2.Enabled = !TxtCanPro2.Enabled;
            TxtReceta.Enabled = !TxtCanPro2.Enabled;
            TxtNumSerOP.Enabled = !TxtNumSerOP.Enabled;
            TxtNumDocOP.Enabled = !TxtNumDocOP.Enabled;
            TxtFchOP.Enabled = !TxtFchOP.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
            CmdProPen.Enabled = !CmdProPen.Enabled;

            CmdAddPer.Enabled = !CmdAddPer.Enabled;
            CmdDelPer.Enabled = !CmdDelPer.Enabled;
            CmdRankin.Enabled = !CmdRankin.Enabled;
            CmdCalcHor.Enabled = !CmdCalcHor.Enabled;

            CmdAddTar.Enabled = !CmdAddTar.Enabled;
            CmdDelTar.Enabled = !CmdDelTar.Enabled;

            OptProPen.Enabled = !OptProPen.Enabled;
            OptProConTar.Enabled = !OptProConTar.Enabled;
        }
        void MostrarEstadoMes(int n_IdEmpresa, int n_IdMes)
        {
            obj_fungen.mysConec = mysConec;
            if (obj_fungen.EstadoPeriodo(n_IdEmpresa, n_IdMes, 12) == true)
            {
                ToolNuevo.Visible = false;
                ToolModificar.Visible = false;
                ToolEliminar.Visible = false;

                ToolGrabar.Visible = false;
                ToolCancelar.Visible = false;

                toolStripSeparator1.Visible = false;
                toolStripSeparator2.Visible = false;

                PicClos1.Visible = true;
                PicClos2.Visible = true;
            }
            else
            {
                ToolNuevo.Visible = true;
                ToolModificar.Visible = true;
                ToolEliminar.Visible = true;

                ToolGrabar.Visible = true;
                ToolCancelar.Visible = true;

                toolStripSeparator1.Visible = true;
                toolStripSeparator2.Visible = true;

                PicClos1.Visible = false;
                PicClos2.Visible = false;
            }
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir.Enabled = !ToolImprimir.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
        }
        void Modificar()
        {
            n_QueHace = 2;
            booAgregando = true;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            TxtNumPro.Focus();
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                objRegistro.mysConec = mysConec;
                if (objRegistro.ELiminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    objRegistro.mysConec = mysConec;
                    if (objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
                    {
                        dtLista = objRegistro.dtLista;
                    }
             
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
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

            OptProPen.Visible = false;
            OptProConTar.Visible = false;
            label20.Visible = false;
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
                booResultado = objRegistro.Insertar(entTar, lstTarCab, lstTarDet);
            }

            if (n_QueHace == 2)
            {
                //objRegistros.mysConec = mysConec;
                booResultado = objRegistro.Actualizar(entTar, lstTarCab, lstTarDet);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistro.n_ErrorNumber.ToString() + " = " + objRegistro.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_row = 0;
            int n_dato = 0;
            double n_valor = 0;
            int n_can = 0;
            string c_dato = "";
            int intIdRegistro = 0;

            if (n_QueHace == 1)
            {
                intIdRegistro = 0;
            }
            else
            {
                intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            }

            entTar.n_idemp = STU_SISTEMA.EMPRESAID;
            entTar.n_id = intIdRegistro;
            entTar.n_idmes = STU_SISTEMA.MESTRABAJO;
            entTar.n_anotra = STU_SISTEMA.ANOTRABAJO;
            entTar.n_idpro = Convert.ToInt32(LblIdProduccion.Text);
            entTar.n_idtipdoc = 82;
            entTar.c_numser = TxtNumSer.Text;
            entTar.c_numdoc = TxtNumDoc.Text;
            entTar.d_fchreg = Convert.ToDateTime(TxtFchReg.Text);
            entTar.n_idsol = Convert.ToInt32(CboSol.SelectedValue);
            entTar.n_idlocal = Convert.ToInt32(CboLocal.SelectedValue);
            //entTar.d_fchejetar
           
            lstTarCab.Clear();

            for (n_row = 2; n_row <= FgTar.Rows.Count - 1; n_row++)
            {
                BE_PRO_SOLICITUDTAREASCAB entTarCab = new BE_PRO_SOLICITUDTAREASCAB();

                entTarCab.n_idsol = intIdRegistro;
                if (n_QueHace == 1)
                {
                    entTarCab.n_id = 0;
                }
                else
                {
                    entTarCab.n_id = Convert.ToInt32(FgTar.GetData(n_row, 12));
                }

                n_dato = Convert.ToInt32(FgTar.GetData(n_row, 9));
                entTarCab.n_idtar = n_dato;

                n_valor = Convert.ToDouble(FgTar.GetData(n_row, 7));
                entTarCab.n_can = n_valor;

                c_dato = funFunciones.NulosC(FgTar.GetData(n_row, 2));
                if (c_dato != "")
                { 
                    entTarCab.d_fchtra = Convert.ToDateTime(c_dato);
                }

                c_dato = funFunciones.NulosC(FgTar.GetData(n_row, 3));
                if (c_dato != "")
                {
                    entTarCab.h_horini = c_dato;
                }

                c_dato = funFunciones.NulosC(FgTar.GetData(n_row, 4));
                if (c_dato != "")
                {
                    entTarCab.h_horfin = c_dato;
                }

                n_dato = Convert.ToInt32(FgTar.GetData(n_row, 5));
                entTarCab.n_numper = n_dato;

                n_valor = 0;
                entTarCab.n_costar = n_valor;

                n_can = Convert.ToInt32(FgTar.GetData(n_row, 11));
                entTarCab.n_ord  = n_can;

                CalcularCosto(FgTar.Row);
                lstTarCab.Add(entTarCab);
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;
            
            if (Convert.ToInt32(CboSol.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el solicitante de las tareas !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboSol.Focus();
                return booEstado;
            }

            if (TxtDesPro.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el producto a procesar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtDesPro.Focus();
                return booEstado;
            }

            if (Convert.ToInt32(CboLocal.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el local donde se realiza la produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboLocal.Focus();
                return booEstado;
            }

            int n_fila = 0;
            double n_cantidad = 0;
            double n_cantidaddet = 0;
            int n_idtar =0;
            string c_nomtar = "";
            
            for (n_fila = 2; n_fila <= FgTar.Rows.Count - 1; n_fila++)
            {
                n_cantidad = Convert.ToDouble(FgTar.GetData(n_fila, 7));
                n_idtar= Convert.ToInt16(FgTar.GetData(n_fila, 9));
                n_cantidaddet = CantidadDetalle(n_idtar);
                c_nomtar = FgTar.GetData(n_fila, 1).ToString();
                if (n_cantidad != n_cantidaddet)
                {
                    MessageBox.Show("¡ El importe de la tarea " + c_nomtar  + " no coincide con la suma e las labores realiadas por el personal indicado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    return booEstado;
                }
            }

            return booEstado;
        }
        double CantidadDetalle(int n_IdTarea)
        {
            //double n_valor = 0;
            int n_fila = 0;
            double n_cantidad = 0;
            int n_idtar = 0;
            for (n_fila = 0; n_fila <= lstTarDet.Count-1; n_fila++)
            {
                n_idtar = lstTarDet[n_fila].n_idsoltar;

                if (n_idtar == n_IdTarea)
                {
                    n_cantidad = n_cantidad + Math.Round(lstTarDet[n_fila].n_can,2);
                }
            }

            return Math.Round(n_cantidad,2);
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
            if (Grabar() == true)
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objRegistro.mysConec = mysConec;
                if (objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
                {
                    dtLista = objRegistro.dtLista;
                }

                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();

                DialogResult Rpta = MessageBox.Show("! El registro se agrego con exito ¡ ¿Desea agregar otro registro? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (DialogResult.Yes == Rpta)
                {
                    Cancelar();
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
            this.Close();
        }
        private void FrmSolicitudTareas_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (dtLista.Rows.Count == 0)
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
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
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
                dtResult = funDbGrid.DG_Filtrar(dtLista, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
        }
        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (n_QueHace != 3) { return; }

            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }
        private void FrmSolicitudTareas_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
        }
        private void c1Sizer1_Click(object sender, EventArgs e)
        {

        }
        private void CmdProPen_Click(object sender, EventArgs e)
        {
            string[,] arrCabeceraDg1 = new string[6, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            DataTable dtInsEnt = new DataTable();
            int n_row;
            double n_canpro = 0;
            string c_dato = "";
            booAgregando = true;
            objProduccion.mysConec = mysConec;
            if (OptProPen.Checked == true)
            { 
                if (objProduccion.ConsultaTareasPendientes(STU_SISTEMA.EMPRESAID) == true)
                {
                    dtProduccion = objProduccion.dtListar;
                }
            }
            if (OptProConTar.Checked == true)
            {
                if (objProduccion.ConsultaProduccionConTarea(STU_SISTEMA.EMPRESAID) == true)
                {
                    dtProduccion = objProduccion.dtListar;
                }
            }

            arrCabeceraDg1[0, 0] = "Fch. Produccion";
            arrCabeceraDg1[0, 1] = "80";
            arrCabeceraDg1[0, 2] = "F";
            arrCabeceraDg1[0, 3] = "d_fchpro";

            arrCabeceraDg1[1, 0] = "Nº Produccion";
            arrCabeceraDg1[1, 1] = "120";
            arrCabeceraDg1[1, 2] = "F";
            arrCabeceraDg1[1, 3] = "c_numdoc";

            arrCabeceraDg1[2, 0] = "Producto";
            arrCabeceraDg1[2, 1] = "400";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "c_despro";

            arrCabeceraDg1[3, 0] = "Uni. Med.";
            arrCabeceraDg1[3, 1] = "60";
            arrCabeceraDg1[3, 2] = "C";
            arrCabeceraDg1[3, 3] = "c_despre";

            arrCabeceraDg1[4, 0] = "Cantidad";
            arrCabeceraDg1[4, 1] = "80";
            arrCabeceraDg1[4, 2] = "N";
            arrCabeceraDg1[4, 3] = "n_canpro";

            arrCabeceraDg1[5, 0] = "Id";
            arrCabeceraDg1[5, 1] = "0";
            arrCabeceraDg1[5, 2] = "N";
            arrCabeceraDg1[5, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            xFun.Buscar_CampoOrden = "c_numdoc";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtProduccion);

            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }

            LblIdProduccion.Text = dtResult.Rows[0]["n_id"].ToString();
            TxtNumPro.Text = dtResult.Rows[0]["c_numdoc"].ToString();
            TxtDesPro.Text = dtResult.Rows[0]["c_despro"].ToString();
            TxtUniMed.Text = dtResult.Rows[0]["c_despre"].ToString();
            TxtCanPro2.Text = dtResult.Rows[0]["n_canpro"].ToString();
            n_canpro = Convert.ToDouble(TxtCanPro2.Text);

            TxtFchPro.Text = dtResult.Rows[0]["d_fchpro"].ToString();
            LblIdProd.Text = dtResult.Rows[0]["n_idpro"].ToString();
            TxtReceta.Text = dtResult.Rows[0]["c_recdes"].ToString();
            LblIdReceta.Text = dtResult.Rows[0]["n_recid"].ToString();

            FgTar.Rows.Count = 2;

            // TRAEMOS LAS TAREAS DE LA RECETA
            dtResult = funDatos.DataTableFiltrar(dtLineas, "n_idrec = " + LblIdReceta.Text + " and n_idpro = " + LblIdProd.Text + " AND n_act = 1");
            if (dtResult.Rows.Count != 0)
            {
                int n_idLinea = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                dtResult = funDatos.DataTableFiltrar(dtLineasTareas, "n_idlin = "+ n_idLinea +"");
                 if (dtResult.Rows.Count!=0)
                 {
                     dtResult = funDatos.DataTableOrdenar(dtResult, "n_ord");
                     for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                     {
                         FgTar.Rows.Count = FgTar.Rows.Count + 1;
                         c_dato = funDatos.DataTableBuscar(dtTareas, "n_id", "c_des", dtResult.Rows[n_row]["n_idtar"].ToString(), "N").ToString();
                         FgTar.SetData(FgTar.Rows.Count - 1, 1, c_dato);

                         c_dato = funDatos.DataTableBuscar(dtTareas, "n_id", "c_unimedabr", dtResult.Rows[n_row]["n_idtar"].ToString(), "N").ToString();
                         //c_dato = funDatos.DataTableBuscar(dtUniMedSunat, "n_id", "c_abr", c_dato, "N").ToString();
                         FgTar.SetData(FgTar.Rows.Count - 1, 6, c_dato);
                         FgTar.SetData(FgTar.Rows.Count - 1, 9, dtResult.Rows[n_row]["n_idtar"].ToString());
                         FgTar.SetData(FgTar.Rows.Count - 1, 11, dtResult.Rows[n_row]["n_ord"].ToString()); 
                     }
                 }
            }

            booAgregando = false;
        }
        private void ToolImprimir_ButtonClick(object sender, EventArgs e)
        {

        }
        private void emitirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tab1.SelectedIndex == 0)
            {
                Tab1.SelectedIndex = 1;
                MessageBox.Show("¡ Debe de seleccionar que tareas desea imprimir !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            int n_row = 0;
            string c_CadIn = "";
            int n_Idregistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            int n_numsel = 0;
            if (FgTar.Rows.Count != 0)
            {
                for (n_row = 2; n_row <= FgTar.Rows.Count - 1; n_row++)
                {
                    if (funFunciones.NulosC(FgTar.GetData(n_row, 10)) == "True")
                    {
                        n_numsel = n_numsel + 1;
                        if (n_numsel > 1) { c_CadIn = c_CadIn + ", "; }
                        c_CadIn = c_CadIn + FgTar.GetData(n_row, 9).ToString();
                    }
                }
            }

            if (c_CadIn == "")
            {
                MessageBox.Show("¡ Debe de seleccionar que tareas desea imprimir !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            
            CN_pro_solicitudtareas objTar = new CN_pro_solicitudtareas();
            objTar.STU_SISTEMA = STU_SISTEMA;
            objTar.mysConec = mysConec;
            objTar.ImprimirSolicitudTar(n_Idregistro, c_CadIn);
        }
        private void guiasDelMesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void DgLista_Click(object sender, EventArgs e)
        {
        }
        private void CmdAddPer_Click(object sender, EventArgs e)
        {
            string c_dato = FgTar.GetData(FgTar.Row, 1).ToString();

            if (funFunciones.NulosC(FgTar.GetData(FgTar.Row, 2)) == "")
            {
                MessageBox.Show("¡ No ha indicado la fecha de proceso de la tarea: " + c_dato + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (funFunciones.NulosC(FgTar.GetData(FgTar.Row, 3)) == "")
            {
                MessageBox.Show("¡ No ha indicado la hora de inicio de la tarea: " + c_dato + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (funFunciones.NulosC(FgTar.GetData(FgTar.Row, 4)) == "")
            {
                MessageBox.Show("¡ No ha indicado la hora de termino de la tarea: " + c_dato + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (funFunciones.NulosC(FgTar.GetData(FgTar.Row, 5)) == "")
            {
                MessageBox.Show("¡ No ha indicado el numero de personas para la tarea: " + c_dato + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            //if (funFunciones.NulosC(FgTar.GetData(FgTar.Row, 7)) == "")
            //{
            //    MessageBox.Show("¡ No ha indicado la cantidad total elaborada de la tarea: " + c_dato + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    return;
            //}
            //if (Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Row, 7))) == 0)
            //{
            //    MessageBox.Show("¡ No ha indicado la cantidad total elaborada de la tarea: " + c_dato + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    return;
            //}
            int n_numper = Convert.ToInt32(FgTar.GetData(FgTar.Row, 5).ToString());

            if (FgPer.Rows.Count - 2 >= n_numper)
            {
                MessageBox.Show("¡ El numero maximo de personas ya fue asignado para la tarea: " + c_dato + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            int n_Fila = 1;
            int n_row = 0;
            string c_fchini = FgTar.GetData(FgTar.Row, 3).ToString();
            string c_fchfin = FgTar.GetData(FgTar.Row, 4).ToString();
            double n_numhor = 0;
            int n_idtarea = Convert.ToInt32(FgTar.GetData(FgTar.Row, 9).ToString());
            string c_cadIN = funFlex.Flex_ObtenerDatosCol(FgPer,6);
            DataTable dtResult = new DataTable();
            string[,] arrCabeceraFlexFil = new string[4, 5];

            CN_pla_empleados objEmpleado = new CN_pla_empleados(STU_SISTEMA);
            objEmpleado.STU_SISTEMA = STU_SISTEMA;
            objEmpleado.Consulta1(STU_SISTEMA.EMPRESAID, c_cadIN);
            dtResult = objEmpleado.dtLista;
            objEmpleado = null;

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Apellidos y Nombres";
            arrCabeceraFlexFil[0, 1] = "400";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "c_apenom";

            arrCabeceraFlexFil[1, 0] = "Nº DNI";
            arrCabeceraFlexFil[1, 1] = "80";
            arrCabeceraFlexFil[1, 2] = "C";
            arrCabeceraFlexFil[1, 3] = "c_numdocide";

            arrCabeceraFlexFil[2, 0] = "Sel.";
            arrCabeceraFlexFil[2, 1] = "40";
            arrCabeceraFlexFil[2, 2] = "B";
            arrCabeceraFlexFil[2, 3] = "n_sel";

            arrCabeceraFlexFil[3, 0] = "ID";
            arrCabeceraFlexFil[3, 1] = "0";
            arrCabeceraFlexFil[3, 2] = "N";
            arrCabeceraFlexFil[3, 3] = "n_id";

            funDatos.Filtrar_CampoOrden = "c_apenom";
            funDatos.Filtrar_Titulo = "Filtro de Trabajadores";
            funDatos.Filtrar_ColumnaCheck = 3;
            dtResult = funDatos.Filtrar2(arrCabeceraFlexFil, dtResult);

            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    booAgregando = true;
                    double n_canpro = Convert.ToDouble(FgTar.GetData(FgTar.Row, 7)) / Convert.ToDouble(FgTar.GetData(FgTar.Row, 5));
                    if (n_numper <= (dtResult.Rows.Count - 1))
                    {
                        MessageBox.Show("¡ El numero de trabajadores seleccionado no puede ser mayor al numero de trabajadores indicado en la tarea: " + c_dato + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                    {
                        FgPer.Rows.Count = FgPer.Rows.Count + 1;

                        c_dato = n_Fila.ToString();
                        FgPer.SetData(FgPer.Rows.Count - 1, 1, c_dato);

                        c_dato = dtResult.Rows[n_row]["c_apenom"].ToString();
                        FgPer.SetData(FgPer.Rows.Count - 1, 2, c_dato);
                                                
                        FgPer.SetData(FgPer.Rows.Count - 1, 3, c_fchini);
                        
                        FgPer.SetData(FgPer.Rows.Count - 1, 4, c_fchfin);

                        FgPer.SetData(FgPer.Rows.Count - 1, 5, "1");
                        FgPer.SetData(FgPer.Rows.Count - 1, 6, n_canpro.ToString("0.00"));
                        FgPer.SetData(FgPer.Rows.Count - 1, 7, n_canpro.ToString("0.00"));

                        c_dato = dtResult.Rows[n_row]["n_id"].ToString();
                        FgPer.SetData(FgPer.Rows.Count - 1, 8, c_dato);

                        AgregarPersonaTarea(Convert.ToInt32(dtResult.Rows[n_row]["n_id"]), n_idtarea, c_fchini, c_fchfin, n_canpro);

                        n_numhor = funCon.HoraEnDecimal(funFunciones.HorasRestar(c_fchini, c_fchfin));
                        FgPer.SetData(FgPer.Rows.Count - 1, 9, n_numhor);

                        n_Fila = n_Fila + 1;
                    }

                    LblTotal.Text = funFlex.FlexSumarCol(FgPer, 7, 2, FgPer.Rows.Count - 1).ToString("0.00");
                    booAgregando = false;
                }
            }
        }
        void ActualizarPersonaTarea(int n_IdPersona, int n_Idtarea, string c_HoraInicio, string c_HoraTermino, double n_Cantidad, int n_NumeroEnvases, double n_PesoBruto)
        {
            int n_row = 0;

            for (n_row = 0; n_row <= lstTarDet.Count - 1; n_row++)
            {
                if (lstTarDet[n_row].n_idper == n_IdPersona)
                {
                    if (lstTarDet[n_row].n_idsoltar == n_Idtarea)
                    { 
                        lstTarDet[n_row].c_horini = c_HoraInicio;
                        lstTarDet[n_row].c_horter = c_HoraTermino;
                        lstTarDet[n_row].n_can = n_Cantidad;
                        lstTarDet[n_row].n_numenv = n_NumeroEnvases;
                        lstTarDet[n_row].n_pesbru = n_PesoBruto;
                    }
                }
            }
        }
        void AgregarPersonaTarea(int n_IdPersona, int n_Idtarea, string c_horini, string c_horfin, double n_Cantidad )
        { 
            BE_PRO_SOLICITUDTAREASDET entTarDet = new BE_PRO_SOLICITUDTAREASDET();
            
            entTarDet.n_idsol = 0;
            entTarDet.n_idsoltar = n_Idtarea;
            entTarDet.n_idper = n_IdPersona;
            entTarDet.n_can = n_Cantidad;
            entTarDet.c_obs = "";
            entTarDet.c_horini = c_horini;
            entTarDet.c_horter = c_horfin;
            entTarDet.c_numhortra = "";
            entTarDet.n_numhortra = 0;
            entTarDet.n_canmaxpro = 0;
            entTarDet.n_preunipro = 0;
            entTarDet.n_prehorpag = 0;
            entTarDet.n_imppaghrstra = 0;
            entTarDet.n_subsidio = 0;
            entTarDet.n_numenv = 1;
            entTarDet.n_pesbru = n_Cantidad;
            lstTarDet.Add(entTarDet);
        }
        private void FgTar_RowColChange(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (FgTar.Rows.Count <= 2) { return; }

            LblNomTar.Text = funFunciones.NulosC( FgTar.GetData(FgTar.Row, 1));
            
            MostrarPersonas();
            //CalcularCosto(FgTar.Row - 1);
        }
        void CalcularCosto(int n_fila)
        {
            if (n_fila < 2) { return; }
            Funciones o_funciones = new Funciones();
            Convertir o_convertir = new Convertir();
            List<BE_PRO_SOLICITUDTAREASDET> l_listar = new List<BE_PRO_SOLICITUDTAREASDET>();
            int n_row = 0;
            int n_idtarea = Convert.ToInt32(FgTar.GetData(n_fila, 9));
            string c_hora = "";

            l_listar = lstTarDet.Where(x => x.n_idsoltar == n_idtarea).ToList();

            if (l_listar.Count == 0) { return; }
            double n_maxpro = l_listar.Max(x => x.n_can);           // OBTENEMOS LA PRODUCCION MAX DEL GRUPO

            double n_prekiltar = (n_PRECIOHORA / n_maxpro);             // CALCULAMOS EL PRECIO DE TAREA POR KILO PARA ESTA TAREA

            for (n_row = 0; n_row <= lstTarDet.Count - 1; n_row++)
            {
                if (lstTarDet[n_row].n_idsoltar == n_idtarea)
                { 
                    string c_horini = lstTarDet[n_row].c_horini;
                    string c_horfin = lstTarDet[n_row].c_horter;

                    if ((c_horini != "") && (c_horfin != ""))
                    {
                        c_hora = o_funciones.HorasRestar(c_horini, c_horfin);

                        lstTarDet[n_row].n_preunipro = n_prekiltar;
                        lstTarDet[n_row].n_prehorpag = (n_prekiltar * lstTarDet[n_row].n_can);
                        lstTarDet[n_row].c_numhortra = c_hora.Substring(0,5);
                        lstTarDet[n_row].n_numhortra = o_convertir.HoraEnDecimal(c_hora);
                        lstTarDet[n_row].n_canmaxpro = n_maxpro;
                        lstTarDet[n_row].n_imppaghrstra = (lstTarDet[n_row].n_prehorpag * lstTarDet[n_row].n_numhortra);
                    }
                }
            }
        }
        void MostrarPersonas()
        {
            string c_dato = "";
            int n_row = 0;
            int n_num = 1;
            int n_idtar = Convert.ToInt32(FgTar.GetData(FgTar.Row, 9));
            double n_numhor = 0;
            string c_fchini = "";  //FgTar.GetData(FgTar.Row, 3).ToString();
            string c_fchfin = "";  //FgTar.GetData(FgTar.Row, 4).ToString();

            FgPer.Rows.Count = 2;
            booAgregando = true;
            for (n_row = 0; n_row <= lstTarDet.Count - 1; n_row++)
            {
                if (lstTarDet[n_row].n_idsoltar == n_idtar)
                {
                    FgPer.Rows.Count = FgPer.Rows.Count + 1;

                    c_dato = n_num.ToString();
                    FgPer.SetData(FgPer.Rows.Count - 1, 1, c_dato);

                    c_dato = funDatos.DataTableBuscar(dtEmpleados, "n_id", "c_apenom", lstTarDet[n_row].n_idper.ToString(), "N").ToString();
                    FgPer.SetData(FgPer.Rows.Count - 1, 2, c_dato);

                    c_dato = lstTarDet[n_row].c_horini.ToString();
                    FgPer.SetData(FgPer.Rows.Count - 1, 3, c_dato);

                    c_dato = lstTarDet[n_row].c_horter.ToString();
                    FgPer.SetData(FgPer.Rows.Count - 1, 4, c_dato);

                    c_dato = lstTarDet[n_row].n_numenv.ToString("0.00");
                    FgPer.SetData(FgPer.Rows.Count - 1, 5, c_dato);

                    c_dato = lstTarDet[n_row].n_pesbru.ToString("0.00");
                    FgPer.SetData(FgPer.Rows.Count - 1, 6, c_dato);

                    c_dato = lstTarDet[n_row].n_can.ToString("0.00");
                    FgPer.SetData(FgPer.Rows.Count - 1, 7, c_dato);

                    c_dato = lstTarDet[n_row].n_idper.ToString();
                    FgPer.SetData(FgPer.Rows.Count - 1, 8, c_dato);

                    c_fchini = FgPer.GetData(FgPer.Rows.Count - 1, 3).ToString();
                    c_fchfin = FgPer.GetData(FgPer.Rows.Count - 1, 4).ToString();

                    n_numhor = funCon.HoraEnDecimal(funFunciones.HorasRestar(c_fchini, c_fchfin));
                    FgPer.SetData(FgPer.Rows.Count - 1, 9, n_numhor);

                    n_num = n_num + 1;
                }
            }
            LblTotal.Text = funFlex.FlexSumarCol(FgPer, 7, 2, FgPer.Rows.Count - 1).ToString("0.00");
            booAgregando = false;
        }
        private void FgTar_EnterCell(object sender, EventArgs e)
        {
            FgTar.AllowEditing = false;
            if ((FgTar.Col == 1) || (FgTar.Col == 1) || (FgTar.Col == 8) || (FgTar.Col == 9) || (FgTar.Col == 11))
            {
                FgTar.AllowEditing = false;
            }
            else
            {
                if (n_QueHace == 3)
                {
                    if (FgTar.Col == 10)
                    {
                        FgTar.AllowEditing = true;
                    }
                    else
                    {
                        FgTar.AllowEditing = false; return;
                    }
                }
                else
                {
                    if (FgTar.Col == 10)
                    {
                        FgTar.AllowEditing = false; return;
                    }
                    else
                    { 
                        FgTar.AllowEditing = true;
                    }
                }
            }
        }
        private void FgTar_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 2)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 3)
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 4)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 5)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 6)
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 7)
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void CmdAddTar_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            FgTar.Rows.Count = FgTar.Rows.Count + 1;

            string c_dato = "";
            string[,] arrCabeceraDg1 = new string[3, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            DataTable dtInsEnt = new DataTable();
                       
            arrCabeceraDg1[0, 0] = "Tarea";
            arrCabeceraDg1[0, 1] = "450";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_des";

            arrCabeceraDg1[1, 0] = "Uni. Med.";
            arrCabeceraDg1[1, 1] = "40";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_unimedabr";

            arrCabeceraDg1[2, 0] = "Id";
            arrCabeceraDg1[2, 1] = "0";
            arrCabeceraDg1[2, 2] = "N";
            arrCabeceraDg1[2, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            xFun.Buscar_CampoOrden = "c_des";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtTareas);

            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }

            c_dato = dtResult.Rows[0]["c_des"].ToString();
            FgTar.SetData(FgTar.Rows.Count - 1, 1, c_dato);

            c_dato = TxtFchReg.Text;
            FgTar.SetData(FgTar.Rows.Count - 1, 2, c_dato);

            c_dato = dtResult.Rows[0]["c_unimedabr"].ToString();
            FgTar.SetData(FgTar.Rows.Count - 1, 6, c_dato);

            c_dato = dtResult.Rows[0]["n_id"].ToString();
            FgTar.SetData(FgTar.Rows.Count - 1, 9, c_dato);
        }
        private void CmdDelTar_Click(object sender, EventArgs e)
        {
            if (FgTar.Rows.Count - 1 == 2) { return; }
            FgTar.RemoveItem(FgTar.Row);
            int n_row =0;
            int n_idtar = Convert.ToInt32(FgTar.GetData(FgTar.Row, 9));

            // ELIMINAMOS LAS PERSONAS DE LA TAREA
            for (n_row = 0; n_row <= lstTarDet.Count - 1; n_row++)
            {
                if (lstTarDet[n_row].n_idsoltar == n_idtar)
                {
                    lstTarDet.RemoveAt(n_row);
                    n_row = n_row - 1;
                }
            }

            // ELIMINAMOS LA TAREA
            for (n_row = 0; n_row <= lstTarCab.Count - 1; n_row++)
            {
                if (lstTarCab[n_row].n_idtar == n_idtar)
                {
                    lstTarCab.RemoveAt(n_row);
                    n_row = n_row - 1;
                }
            }
        }
        private void FgPer_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (booAgregando == true) { return; }
            int n_idper = Convert.ToInt32(FgPer.GetData(FgPer.Row, 8));
            int n_idtar = Convert.ToInt32(FgTar.GetData(FgTar.Row, 9));
            string c_horini = funFunciones.NulosC(FgPer.GetData(FgPer.Row,3));
            string c_horfin = funFunciones.NulosC(FgPer.GetData(FgPer.Row,4));
            double n_can =Convert.ToDouble(FgPer.GetData(FgPer.Row,7));
            int n_numev = Convert.ToInt32(FgTar.GetData(FgTar.Row, 5));
            double n_prebru = Convert.ToDouble(FgPer.GetData(FgPer.Row, 6));
            double n_numkilenv = 0;
            double n_numhor =0;

            if ((e.Col == 3) || (e.Col == 4) || (e.Col == 5) || (e.Col == 6))
            {
                double n_pesnet = (n_prebru - (n_numev * n_numkilenv));
                n_can = n_pesnet;

                FgPer.SetData(FgPer.Row, 7, n_pesnet.ToString("0.00"));
                n_numev = Convert.ToInt32(FgPer.GetData(FgPer.Row,5));
                ActualizarPersonaTarea(n_idper, n_idtar, c_horini, c_horfin, n_can, n_numev, n_prebru);
                //LblTotal.Text = funFlex.FlexSumarCol(FgPer, 7, 2, FgPer.Rows.Count-1).ToString("0.00");
                
                n_numhor = funCon.HoraEnDecimal(funFunciones.HorasRestar(c_horini, c_horfin));
                FgPer.SetData(FgPer.Row, 9, n_numhor);
            }
            LblTotal.Text = funFlex.FlexSumarCol(FgPer, 7, 2, FgPer.Rows.Count - 1).ToString("0.00");
            FgTar.SetData(FgTar.Row, 7, LblTotal.Text);
        }
        private void FgPer_EnterCell(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if ((FgPer.Col == 3) || (FgPer.Col == 4) || (FgPer.Col == 5) || (FgPer.Col == 6))
            {
                FgPer.AllowEditing = true;
            }
            else
            {
                FgPer.AllowEditing = false;
            }
        }
        private void FgPer_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 3)
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 4)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 5)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void imprimirTareasConPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tab1.SelectedIndex == 0)
            {
                Tab1.SelectedIndex = 1;
                MessageBox.Show("¡ Debe de seleccionar que tareas desea imprimir !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            int n_row = 0;
            string c_CadIn = "";
            int n_Idregistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            int n_numsel = 0;
            if (FgTar.Rows.Count != 0)
            {
                for (n_row = 2; n_row <= FgTar.Rows.Count - 1; n_row++)
                {
                    if (funFunciones.NulosC(FgTar.GetData(n_row, 10)) == "True")
                    {
                        n_numsel = n_numsel + 1;
                        if (n_numsel > 1) { c_CadIn = c_CadIn + ", "; }
                        c_CadIn = c_CadIn + FgTar.GetData(n_row, 9).ToString();
                    }
                }
            }

            if (c_CadIn == "")
            {
                MessageBox.Show("¡ Debe de seleccionar que tareas desea imprimir !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            CN_pro_solicitudtareas objTar = new CN_pro_solicitudtareas();
            objTar.STU_SISTEMA = STU_SISTEMA;
            objTar.mysConec = mysConec;
            objTar.ImprimirSolicitudTarPer(n_Idregistro, c_CadIn);
        }
        private void CmdDelPer_Click(object sender, EventArgs e)
        {
            if (FgPer.Rows.Count - 1 == 1) { return; }
            int n_idtarea = Convert.ToInt32(FgTar.GetData(FgTar.Row, 9));
            int n_IdPersonal = Convert.ToInt16(FgPer.GetData(FgPer.Row, 8));
            FgPer.RemoveItem(FgPer.Row);
            EliminarEmpleado(n_IdPersonal, n_idtarea);
            LblTotal.Text = funFlex.FlexSumarCol(FgPer, 7, 2, FgPer.Rows.Count - 1).ToString("0.00");
        }
        void EliminarEmpleado(int n_IdPersonal, int n_IdTarea)
        {
            int n_row = 0;
            for(n_row = 0; n_row<=lstTarDet.Count-1; n_row++)
            {
                if (lstTarDet[n_row].n_idsoltar == n_IdTarea)
                {
                    if (lstTarDet[n_row].n_idper == n_IdPersonal)
                    {
                        lstTarDet.RemoveAt(n_row);
                    }
                }
            }
            return;
        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);

            objRegistro.mysConec = mysConec;
            //if (objRegistro.Consulta2(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
            if (objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
            {
                dtLista = objRegistro.dtLista;
            }

            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            ListarItems();

            if (dtLista.Rows.Count == 0)
            {
                DialogResult Rpta = MessageBox.Show("No se han encontrado registros, ¿ Desea agregar uno ahora ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (DialogResult.Yes == Rpta)
                {
                    //Nuevo();
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
        private void TxtNumSer_Validated(object sender, EventArgs e)
        {
            if (TxtNumSer.Text != "")
            {
                string strCad = "0000" + TxtNumSer.Text;
                TxtNumSer.Text = strCad.Substring(strCad.Length - 4, 4);
                TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 82, TxtNumSer.Text);
            }   
        }
        private void ToolVisar_Click(object sender, EventArgs e)
        {
            DataTable dtSele = new DataTable();
            string[,] arrSeleFlex1 = new string[8, 5];

            dtSele = funDatos.DataTableFiltrar(dtLista, "n_chkpla = 0");

            // FLEX GRID DE LOS TAREAS
            arrSeleFlex1[0, 0] = "Fch. Registro";
            arrSeleFlex1[0, 1] = "80";
            arrSeleFlex1[0, 2] = "F";
            arrSeleFlex1[0, 3] = "";
            arrSeleFlex1[0, 4] = "d_fchreg";

            arrSeleFlex1[1, 0] = "Nº Documento";
            arrSeleFlex1[1, 1] = "110";
            arrSeleFlex1[1, 2] = "C";
            arrSeleFlex1[1, 3] = "";
            arrSeleFlex1[1, 4] = "c_numdoc";

            arrSeleFlex1[2, 0] = "Local";
            arrSeleFlex1[2, 1] = "150";
            arrSeleFlex1[2, 2] = "C";
            arrSeleFlex1[2, 3] = "";
            arrSeleFlex1[2, 4] = "c_deslocal";

            arrSeleFlex1[3, 0] = "Nº Produccion";
            arrSeleFlex1[3, 1] = "110";
            arrSeleFlex1[3, 2] = "C";
            arrSeleFlex1[3, 3] = "";
            arrSeleFlex1[3, 4] = "c_numproduccion";

            arrSeleFlex1[4, 0] = "Producto";
            arrSeleFlex1[4, 1] = "240";
            arrSeleFlex1[4, 2] = "C";
            arrSeleFlex1[4, 3] = "";
            arrSeleFlex1[4, 4] = "c_despro";

            arrSeleFlex1[5, 0] = "Uni. Med.";
            arrSeleFlex1[5, 1] = "40";
            arrSeleFlex1[5, 2] = "C";
            arrSeleFlex1[5, 3] = "";
            arrSeleFlex1[5, 4] = "c_desunimed";

            arrSeleFlex1[6, 0] = "Chk Pla";
            arrSeleFlex1[6, 1] = "40";
            arrSeleFlex1[6, 2] = "B";
            arrSeleFlex1[6, 3] = "";
            arrSeleFlex1[6, 4] = "n_chkpla";

            arrSeleFlex1[7, 0] = "Id";
            arrSeleFlex1[7, 1] = "0";
            arrSeleFlex1[7, 2] = "N";
            arrSeleFlex1[7, 3] = "";
            arrSeleFlex1[7, 4] = "n_id";

            funFlex.FlexMostrarDatos(FgSele, arrSeleFlex1, dtSele, 2, true);

            ToolHerramientas.Enabled = false;
            Tab1.Enabled = false;
            PanSele.Left = ((this.Width - PanSele.Width) / 2);
            PanSele.Top = ((this.Height - PanSele.Height) / 2);
            PanSele.Visible = true;
            FgSele.Focus();
        }
        private void FgTar_BeforeRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            //if (booAgregando == true) { return; }
            //if (FgTar.Rows.Count <= 2) { return; }

            //CalcularCosto(FgTar.Row);
        }
        private void FgTar_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            if (booAgregando == true) { return; }
            if (FgTar.Rows.Count < 2) { return; }
            if (n_QueHace == 3) { return; }
            CalcularCosto(e.OldRange.BottomRow);
        }
        private void CmdAceSel_Click(object sender, EventArgs e)
        {
            int n_fil = 0;
            int n_IdReg = 0;

            for (n_fil = 2; n_fil <= FgSele.Rows.Count - 1; n_fil++)
            {
                if (FgSele.GetData(n_fil, 7).ToString() == "True")
                { 
                    n_IdReg = Convert.ToInt32(FgSele.GetData(n_fil,8).ToString());
                    objRegistro.MarcarRevisado(n_IdReg, 2);
                }
            }

            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            objRegistro.mysConec = mysConec;
            if (objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
            {
                dtLista = objRegistro.dtLista;
            }

            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            ListarItems();
            CmdCanSel_Click(sender, e);
        }
        private void CmdCanSel_Click(object sender, EventArgs e)
        {
            ToolHerramientas.Enabled = true;
            Tab1.Enabled = true;
            PanSele.Visible = false;
            DgLista.Focus();
        }

        private void FgSele_EnterCell(object sender, EventArgs e)
        {
            FgSele.AllowEditing = false;
            if (FgSele.Col == 7)
            {
                FgSele.AllowEditing = true;
            }
        }

        private void CmdRankin_Click(object sender, EventArgs e)
        {
            string c_dato = FgTar.GetData(FgTar.Row, 1).ToString();

            if (funFunciones.NulosC(FgTar.GetData(FgTar.Row, 2)) == "")
            {
                MessageBox.Show("¡ No ha indicado la fecha de proceso de la tarea: " + c_dato + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (funFunciones.NulosC(FgTar.GetData(FgTar.Row, 3)) == "")
            {
                MessageBox.Show("¡ No ha indicado la hora de inicio de la tarea: " + c_dato + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (funFunciones.NulosC(FgTar.GetData(FgTar.Row, 4)) == "")
            {
                MessageBox.Show("¡ No ha indicado la hora de termino de la tarea: " + c_dato + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (funFunciones.NulosC(FgTar.GetData(FgTar.Row, 5)) == "")
            {
                MessageBox.Show("¡ No ha indicado el numero de personas para la tarea: " + c_dato + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (funFunciones.NulosC(FgTar.GetData(FgTar.Row, 7)) == "")
            {
                MessageBox.Show("¡ No ha indicado la cantidad total elaborada de la tarea: " + c_dato + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Row, 7))) == 0)
            {
                MessageBox.Show("¡ No ha indicado la cantidad total elaborada de la tarea: " + c_dato + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            int n_numper = Convert.ToInt32(FgTar.GetData(FgTar.Row, 5).ToString());

            if (FgPer.Rows.Count - 2 >= n_numper)
            {
                MessageBox.Show("¡ El numero maximo de personas ya fue asignado para la tarea: " + c_dato + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            string[,] arrCabeceraFlexFil = new string[6, 5];
            int n_idtarea =  Convert.ToInt32(FgTar.GetData(FgTar.Row, 9).ToString());
            int n_row = 0;
            string c_destar = "";
            int n_Fila = 1;
            DataTable dtResult = new DataTable();
            string c_fchini = FgTar.GetData(FgTar.Row, 3).ToString();
            string c_fchfin = FgTar.GetData(FgTar.Row, 4).ToString();

            objRegistro.mysConec = mysConec;
            objRegistro.Consulta1(STU_SISTEMA.EMPRESAID, n_idtarea);
            dtResult = objRegistro.dtLista;

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Apellidos y Nombres";
            arrCabeceraFlexFil[0, 1] = "300";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "";
            arrCabeceraFlexFil[0, 4] = "c_traapenom";

            arrCabeceraFlexFil[1, 0] = "Nº DNI";
            arrCabeceraFlexFil[1, 1] = "80";
            arrCabeceraFlexFil[1, 2] = "C";
            arrCabeceraFlexFil[1, 3] = "";
            arrCabeceraFlexFil[1, 4] = "c_numdocide";

            arrCabeceraFlexFil[2, 0] = "Can. Producida";
            arrCabeceraFlexFil[2, 1] = "0";
            arrCabeceraFlexFil[2, 2] = "D";
            arrCabeceraFlexFil[2, 3] = "0.00";
            arrCabeceraFlexFil[2, 4] = "n_can";

            arrCabeceraFlexFil[3, 0] = "Can x Hora";
            arrCabeceraFlexFil[3, 1] = "80";
            arrCabeceraFlexFil[3, 2] = "D";
            arrCabeceraFlexFil[3, 3] = "0.00";
            arrCabeceraFlexFil[3, 4] = "n_prohor";

            arrCabeceraFlexFil[4, 0] = "Sel.";
            arrCabeceraFlexFil[4, 1] = "40";
            arrCabeceraFlexFil[4, 2] = "B";
            arrCabeceraFlexFil[4, 3] = "";
            arrCabeceraFlexFil[4, 4] = "n_sel";

            arrCabeceraFlexFil[5, 0] = "IdEmpleado";
            arrCabeceraFlexFil[5, 1] = "0";
            arrCabeceraFlexFil[5, 2] = "N";
            arrCabeceraFlexFil[5, 3] = "";
            arrCabeceraFlexFil[5, 4] = "n_idper";

            c_dato = FgTar.GetData(FgTar.Row, 1).ToString();

            funDatos.Filtrar_CampoOrden = "n_prohor DESC";
            funDatos.Filtrar_Titulo = "RANKING DE TRABAJADORES - " + c_dato;
            funDatos.Filtrar_CampoBusqueda = "n_idper";
            funDatos.Filtrar_ColumnaBusqueda = 6;
            funDatos.Filtrar_ColumnaCheck = 5;
            funDatos.Filtrar_AplicarFiltro = true;
            dtResult = funDatos.Filtrar(arrCabeceraFlexFil, dtResult);

            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    if (n_numper <= (dtResult.Rows.Count - 1))
                    {
                        MessageBox.Show("¡ El numero de trabajadores seleccionado no puede ser mayor al numero de trabajadores indicado en la tarea: " + c_dato + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                    {
                        FgPer.Rows.Count = FgPer.Rows.Count + 1;

                        c_dato = n_Fila.ToString();
                        FgPer.SetData(FgPer.Rows.Count - 1, 1, c_dato);

                        c_dato = dtResult.Rows[n_row]["c_traapenom"].ToString();
                        FgPer.SetData(FgPer.Rows.Count - 1, 2, c_dato);

                        FgPer.SetData(FgPer.Rows.Count - 1, 3, c_fchini);

                        FgPer.SetData(FgPer.Rows.Count - 1, 4, c_fchfin);

                        c_dato = dtResult.Rows[n_row]["n_idper"].ToString();
                        FgPer.SetData(FgPer.Rows.Count - 1, 6, c_dato);

                        double n_valor = Convert.ToDouble(FgPer.GetData(FgPer.Row, 7));
                        AgregarPersonaTarea(Convert.ToInt32(dtResult.Rows[n_row]["n_idper"]), n_idtarea, c_fchini, c_fchfin, n_valor);
                    }
                }
            }
        }

        private void Sc02_Click(object sender, EventArgs e)
        {

        }

        private void CmdCalcHor_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (FgPer.Rows.Count == 2) { return; }

            int n_row = 0;
            double n_tothor = funFlex.FlexSumarCol(FgPer,9,2,FgPer.Rows.Count - 1);
            double n_totkil = Convert.ToDouble(FgTar.GetData(FgTar.Row,7).ToString());
            double n_horper = 0;
            double n_canper = 0;
            for(n_row = 2; n_row <= FgPer.Rows.Count - 1; n_row++)
            {
                n_horper =Convert.ToDouble(FgPer.GetData(n_row,9).ToString());
                n_canper = n_totkil / n_tothor * n_horper;
                FgPer.SetData(n_row, 6, n_canper.ToString("0.00"));
                FgPer.SetData(n_row, 7, n_canper.ToString("0.00"));

                int n_idper = Convert.ToInt32(FgPer.GetData(n_row, 8));
                int n_idtar = Convert.ToInt32(FgTar.GetData(FgTar.Row, 9));
                string c_horini = funFunciones.NulosC(FgPer.GetData(n_row, 3));
                string c_horfin = funFunciones.NulosC(FgPer.GetData(n_row, 4));
                double n_can = Convert.ToDouble(FgPer.GetData(n_row, 7));
                int n_numev = Convert.ToInt32(FgTar.GetData(FgTar.Row, 5));
                double n_prebru = Convert.ToDouble(FgPer.GetData(n_row, 6));

                ActualizarPersonaTarea(n_idper, n_idtar, c_horini, c_horfin, n_can, n_numev, n_prebru);
            }
        }
    }
}
