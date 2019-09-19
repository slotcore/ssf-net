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
    public partial class FrmSolicitudMateriales2 : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        BE_PRO_SOLICITUDMATERIALES entSolicitud = new BE_PRO_SOLICITUDMATERIALES();
        List<BE_PRO_SOLICITUDMATERIALESDET> lstSolicitudDet = new List<BE_PRO_SOLICITUDMATERIALESDET>();

        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_pro_solicitudamateriales objRegistro = new CN_pro_solicitudamateriales();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_alm_inventario ObjItem = new CN_alm_inventario();
        CN_sun_unimed objUniMedSunat = new CN_sun_unimed();
        CN_pro_personal objPerPro = new CN_pro_personal();

        CN_pro_ordenproduccion objOrdenProduccion = new CN_pro_ordenproduccion();
        CN_alm_inventariounimed objUniMed = new CN_alm_inventariounimed();
        CN_pro_productosrecetas objRecetas = new CN_pro_productosrecetas();
        CN_pro_productosrecetasinsumos objRecetasInsumo = new CN_pro_productosrecetasinsumos();
        CN_pro_produccion objProduccion = new CN_pro_produccion();

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
        DataTable dtListar = new DataTable();
        DataTable dtTipDoc = new DataTable();
        DataTable dtNumDocRef = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtPerPro = new DataTable();
        DataTable dtOrdenProduccion = new DataTable();
        DataTable dtUniMed = new DataTable();
        DataTable dtUniMedSunat = new DataTable();
        DataTable dtRecetas = new DataTable();
        DataTable dtRecetasInsumos = new DataTable();
        DataTable dtProgConsulta = new DataTable();
        DataTable dtProgramaActual = new DataTable();
        DataTable dtProduccion = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                                // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[9, 4];                                     // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlexIns = new string[8, 5];
        string[,] arrCabeceraFlexLisPro = new string[11, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;

        public FrmSolicitudMateriales2()
        {
            InitializeComponent();
        }

        private void FrmSolicitudMateriales2_Load(object sender, EventArgs e)
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

            arrCabeceraFlexIns[7, 0] = "Seleccion";
            arrCabeceraFlexIns[7, 1] = "60";
            arrCabeceraFlexIns[7, 2] = "B";
            arrCabeceraFlexIns[7, 3] = "";
            arrCabeceraFlexIns[7, 4] = "";

            funFlex.FlexMostrarDatos(FgInsumos, arrCabeceraFlexIns, dtListar, 2, false);
            
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
        }
        void ListarItems()
        {
            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
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

            DataTable dtResult = new DataTable();
            dtResult = funDatos.DataTableFiltrar(dtUniMed, "n_idite = " + entSolicitud.n_idite + " AND n_default = 1");
            TxtCanPro2.Text = entSolicitud.n_can.ToString("0.00");
            TxtObs.Text = entSolicitud.c_obs;

            MostrarDatosProducto(entSolicitud.n_idpro);

            TxtNumSerOP.Text = "";
            TxtNumDocOP.Text = "";
            funControl.dtpText(TxtFchOP, "");
           
            // MOSTRAMOS EL DETALLE DE LA SOLICITUD
            string c_dato;
            Double n_dato;
            int n_valor;

            FgInsumos.Rows.Count = 2;

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

                if (n_QueHace != 3) 
                {
                    n_dato = (lstSolicitudDet[n_row].n_canteo * Convert.ToDouble(TxtCanPro2.Text));
                }
                else
                {
                    n_dato = lstSolicitudDet[n_row].n_canent;
                }
                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 4, n_dato.ToString("0.000000"));                                  // CANTIDAD ENTREGADA DE LA RECETA

                c_dato = lstSolicitudDet[n_row].c_numlot.ToString();
                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 5, c_dato);                                                       // NUMERO DE LOTE

                n_valor = lstSolicitudDet[n_row].n_idite;
                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 6, n_valor);                                                      // ID DEL ITEM

                n_valor = lstSolicitudDet[n_row].n_idunimed;
                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 7, n_valor);                                                      // ID DE LA UNIDAD DE MEDIDA
            }
        }
        void MostrarDatosProducto(int n_idProduccion)
        { 
            objProduccion.mysConec = mysConec;
            BE_PRO_PRODUCCION entProd = new BE_PRO_PRODUCCION();
            if (objProduccion.TraerRegistro(n_idProduccion) == true)
            {
                entProd = objProduccion.EntProduccion;
            }
            LblIdProduccion.Text = entProd.n_id.ToString();
            TxtNumPro.Text = entProd.c_numser + "-" + entProd.c_numdoc;

            string c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", entProd.n_idpro.ToString(), "N").ToString();
            TxtDesPro.Text = c_dato;

            c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_despre", entProd.n_idunimed.ToString(), "N").ToString();
            TxtUniMed.Text = c_dato;
            TxtCanPro2.Text = entProd.n_canpro.ToString("0.00");

            funControl.dtpText(TxtFchPro, entProd.d_fchpro.ToString());
            LblIdProd.Text = entProd.n_idpro.ToString();

            c_dato = funDatos.DataTableBuscar(dtRecetas, "n_id", "c_des", entProd.n_idrec.ToString(), "N").ToString();
            TxtReceta.Text = c_dato;

            LblIdRec.Text = entProd.n_idrec.ToString();
        }
        void Nuevo()
        {
            booAgregando = true;
            n_QueHace = 1;
            ActivarTool();
            Tab1.TabPages[0].Enabled = false;
            Tab1.SelectedIndex = 1;
            Bloquea();
            Blanquea();
            booAgregando = false;
            CboTipDoc.SelectedValue = 72;
            TxtNumSer.Text = "0001";
            TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 72, "0001");
            OptBusProducto.Checked = true;
            OptBusProducto.Visible = true;
            OptBusProcesado.Visible = true;
            
            CboSol.Focus();
        }
        void Blanquea()
        {
            lstSolicitudDet.Clear();
            FgInsumos.Rows.Count = 2;

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

            funControl.dtpBlanquea(TxtFchOP);           

            TxtObs.Text = "";
        }
        void Bloquea()
        {
            //CboTipDoc.Enabled = !CboTipDoc.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            CboSol.Enabled = !CboSol.Enabled;
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

            CmdSelTod.Enabled = !CmdSelTod.Enabled;
            CmdDelSel.Enabled = !CmdDelSel.Enabled;
            CmdSel.Enabled = !CmdSel.Enabled;
            CmdAddIte.Enabled = !CmdAddIte.Enabled;
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
            ToolMenuImprimir.Enabled = !ToolMenuImprimir.Enabled;
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

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            TxtFchReg.Focus();
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

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
            OptBusProducto.Visible = false;
            OptBusProcesado.Visible = false;
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
                booResultado = objRegistro.Insertar(entSolicitud, lstSolicitudDet);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistro.Actualizar(entSolicitud, lstSolicitudDet);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ No se pudo guardar el registro por el siguiente motivo ! " + objRegistro.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_fila = 0;
            int n_dato = 0;
            double n_valor = 0;
            string c_dato;

            entSolicitud.n_idemp = STU_SISTEMA.EMPRESAID;
            if (n_QueHace == 1)
            {
                entSolicitud.n_id = 0;
            }
            else
            {
                entSolicitud.n_id = entSolicitud.n_id;
            }

            entSolicitud.n_idemp = STU_SISTEMA.EMPRESAID;
            entSolicitud.n_idtipdoc = 72;
            entSolicitud.c_numser = TxtNumSer.Text;
            entSolicitud.c_numdoc = TxtNumDoc.Text;
            entSolicitud.d_fchreg = Convert.ToDateTime(TxtFchReg.Text);
            entSolicitud.n_idsol = Convert.ToInt32(CboSol.SelectedValue);
            entSolicitud.n_idprogra = 0;
            entSolicitud.n_idordpro = 0;
            entSolicitud.n_idite = Convert.ToInt32(LblIdProd.Text);
            entSolicitud.n_idrec = Convert.ToInt32(LblIdRec.Text);
            entSolicitud.c_obs = TxtObs.Text;
            entSolicitud.n_anotra = STU_SISTEMA.ANOTRABAJO;
            entSolicitud.n_mestra = STU_SISTEMA.MESTRABAJO;
            entSolicitud.n_idpro = Convert.ToInt32(LblIdProduccion.Text);
            entSolicitud.n_can = Convert.ToDouble(TxtCanPro2.Text);
            lstSolicitudDet.Clear();

            // CARGAMOS LA LISTA DE ORDENES DE PRODUCCION
            for (n_fila = 2; n_fila <= (FgInsumos.Rows.Count - 1); n_fila++)
            {
                if (funFunciones.NulosC(FgInsumos.GetData(n_fila, 8)) == "True")
                { 
                    BE_PRO_SOLICITUDMATERIALESDET entSoliDet = new BE_PRO_SOLICITUDMATERIALESDET();
                
                    entSoliDet.n_idsol = 0;                                      // ID DE LA SOLICITUD

                    n_dato = Convert.ToInt32(FgInsumos.GetData(n_fila, 6));
                    entSoliDet.n_idite = n_dato;                                 // ID DEL ITEM

                    n_dato = Convert.ToInt32(FgInsumos.GetData(n_fila, 7));      // ID DE LA UNIDAD DE MEDIDA
                    entSoliDet.n_idunimed = n_dato;

                    n_valor = Convert.ToDouble(FgInsumos.GetData(n_fila, 3));
                    entSoliDet.n_canteo = n_valor;                               // CANTIDAD TEORICA DE LA RECETA

                    n_valor = Convert.ToDouble(FgInsumos.GetData(n_fila, 4));
                    entSoliDet.n_canent = n_valor;                               // CANTIDAD ENTREGADA

                    c_dato = funFunciones.NulosC(FgInsumos.GetData(n_fila, 5)).ToString();
                    entSoliDet.c_numlot = c_dato;                                // NUMERO DE LOTE

                    entSoliDet.n_impval = 0;                                     // VALOR DEL INSUMO

                    lstSolicitudDet.Add(entSoliDet);
                }
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

            if (TxtNumPro.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumPro.Focus();
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
                // CHEQUEAMOS QUE SE HAYAN SELECIONADO INSUMOS
                int n_numsel = 0;
                for (n_fila = 2; n_fila <= FgInsumos.Rows.Count - 1; n_fila++)
                {
                    if (funFunciones.NulosC(FgInsumos.GetData(n_fila, 8)) == "True")
                    {
                        n_numsel = n_numsel + 1;
                    }
                }
                if (n_numsel == 0)
                {
                    MessageBox.Show("¡ No ha indicado los items que se solicitaran con esta solicitud ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    FgInsumos.Focus();
                    booEstado = false;
                    return booEstado;
                }
                // CHEQUEAMOS QUE LOS INSUMOS TENGAN CATIDADES
                for (n_fila = 2; n_fila <= FgInsumos.Rows.Count - 1; n_fila++)
                {
                    if (Convert.ToDouble(funFunciones.NulosN(FgInsumos.GetData(n_fila, 4))) == 0)
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
        private void FrmSolicitudMateriales2_Activated(object sender, EventArgs e)
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
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }
        private void FrmSolicitudMateriales2_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
        }
        private void imprimirRecetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Imprimir();
        }
        void Imprimir()
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            CN_pro_solicitudamateriales FunSol = new CN_pro_solicitudamateriales();
            FunSol.mysConec = mysConec;
            FunSol.STU_SISTEMA = STU_SISTEMA;
            FunSol.ImprimirSolicitudMat(intIdRegistro);
        }

        private void ToolMenuImprimir_ButtonClick(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void CmdProPen_Click(object sender, EventArgs e)
        {
            string[,] arrCabeceraDg1 = new string[6, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            DataTable dtInsEnt = new DataTable();
            int n_row;
            int n_fila;
            double n_canpro = 0;

            objProduccion.mysConec = mysConec;
            if (OptBusProducto.Checked == true)
            {
                if (objProduccion.ConsultaSolMatPendientes(STU_SISTEMA.EMPRESAID) == true)
                {
                    dtProduccion = objProduccion.dtListar;
                }
            }
            else
            {
                if (objProduccion.ConsultaSolMatProcesadas(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
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
            LblIdRec.Text = dtResult.Rows[0]["n_recid"].ToString();
            string c_nidpro = dtResult.Rows[0]["n_idpro"].ToString();
            dtResult = funDatos.DataTableFiltrar(dtRecetasInsumos, "n_idrec = " + LblIdRec.Text + " AND n_idpro = " + c_nidpro + "");                                 // FILTRAMOS LOS INSUMOS DE LA RECETA

            if (OptBusProducto.Checked == true)
            {
                objRegistro.Consulta4(Convert.ToInt32(LblIdProduccion.Text));
            }
            else
            {
                objRegistro.Consulta4(999999);
            }
            dtInsEnt = objRegistro.dtLista;
                        
            // CREAMOS EL DETALLE DE LA SOLICITUD               
            int n_idinsEnt = 0;                   // ID DEL INSUMO ENTREGADO
            int n_idinsCar = 0;                   // ID DEL INSUMO CARGADO
            bool b_encontrado = false;
            lstSolicitudDet.Clear();
            for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
            {
                b_encontrado = false;
                n_idinsCar = Convert.ToInt32(dtResult.Rows[n_row]["n_idite"]);

                for (n_fila = 0; n_fila <= dtInsEnt.Rows.Count - 1; n_fila++)
                {
                    n_idinsEnt = Convert.ToInt32(dtInsEnt.Rows[n_fila]["n_idite"]);
                    if (n_idinsCar == n_idinsEnt)
                    {
                        b_encontrado = true;
                        break;
                    }
                }
                if (b_encontrado == false)
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
            }

            MostrarIsumos();
            booAgregando = false;
        }
        void MostrarIsumos()
        {
            string c_dato;
            Double n_dato;
            int n_valor;
            int n_row = 0;
            booAgregando = true;
            FgInsumos.Rows.Count = 2;
            
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
            booAgregando = false;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CmdSelTod_Click(object sender, EventArgs e)
        {
            int n_fila = 0;

            for (n_fila = 2; n_fila <= FgInsumos.Rows.Count - 1; n_fila++)
            {
                FgInsumos.SetData(n_fila, 8, false);
            }
        }
        private void CmdDelSel_Click(object sender, EventArgs e)
        {
            int n_fila = 0;

            for (n_fila = 2; n_fila <= FgInsumos.Rows.Count - 1; n_fila++)
            {
                if (funFunciones.NulosC(FgInsumos.GetData(n_fila, 8)) == "True")
                { 
                    FgInsumos.RemoveItem(n_fila);
                    n_fila = n_fila - 1;
                }
            }
        }
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

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
        private void CmdSel_Click(object sender, EventArgs e)
        {
            int n_fila = 0;

            for (n_fila = 2; n_fila <= FgInsumos.Rows.Count - 1; n_fila++)
            {
                FgInsumos.SetData(n_fila, 8, true);
            }
        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);

            objRegistro.mysConec = mysConec;
            
            if ( objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO)==true)
            {
                dtListar = objRegistro.dtLista;
            }

            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            ListarItems();

            if (dtListar.Rows.Count == 0)
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

        private void CmdAddIte_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            
            string c_dato = "";
            string[,] arrCabeceraDg1 = new string[3, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            DataTable dtInsEnt = new DataTable();

            arrCabeceraDg1[0, 0] = "Item";
            arrCabeceraDg1[0, 1] = "500";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_despro";

            arrCabeceraDg1[1, 0] = "Codigo";
            arrCabeceraDg1[1, 1] = "120";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_codpro";

            arrCabeceraDg1[2, 0] = "Id";
            arrCabeceraDg1[2, 1] = "0";
            arrCabeceraDg1[2, 2] = "N";
            arrCabeceraDg1[2, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            xFun.Buscar_CampoOrden = "c_despro";
            dtResult = funDatos.DataTableFiltrar(dtItems, "n_idtipexi IN (2, 3, 4, 6)");
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }

            FgInsumos.Rows.Count = FgInsumos.Rows.Count + 1;

            c_dato = dtResult.Rows[0]["c_despro"].ToString();
            FgInsumos.SetData(FgInsumos.Rows.Count - 1, 1, c_dato);

            c_dato = dtResult.Rows[0]["n_id"].ToString();
            c_dato = funDatos.DataTableBuscar(dtUniMed, "n_idite", "c_abrpre", c_dato, "N").ToString();
            FgInsumos.SetData(FgInsumos.Rows.Count - 1, 2, c_dato);

            FgInsumos.SetData(FgInsumos.Rows.Count - 1, 3, "0.000000");

            c_dato = dtResult.Rows[0]["n_id"].ToString();
            FgInsumos.SetData(FgInsumos.Rows.Count - 1, 6, c_dato);

            c_dato = dtResult.Rows[0]["n_id"].ToString();
            c_dato = funDatos.DataTableBuscar(dtUniMed, "n_idite", "n_idunimedbas", c_dato, "N").ToString();
            FgInsumos.SetData(FgInsumos.Rows.Count - 1, 7, c_dato);
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

        private void FgInsumos_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {

        }

        private void FgInsumos_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            FgInsumos.AllowEditing = false;

            //if (OptBusProcesado.Checked == true)
            //{              
                if (FgInsumos.Col == 4)
                {
                    if (funFunciones.NulosC(FgInsumos.GetData(FgInsumos.Row, 8)) == "True")
                    {
                        FgInsumos.AllowEditing = true;
                    }                    
                }
                if (FgInsumos.Col == 8)
                {
                    FgInsumos.AllowEditing = true;
                }
            //}
        }

        private void OptBusProducto_CheckedChanged(object sender, EventArgs e)
        {
            //CmdSel.Enabled = false;
            //CmdSelTod.Enabled = false;
            //CmdDelSel.Enabled = false;
        }

        private void OptBusProcesado_CheckedChanged(object sender, EventArgs e)
        {
            //CmdSel.Enabled = true;
            //CmdSelTod.Enabled = true;
            //CmdDelSel.Enabled = true;
        }
    }
}
