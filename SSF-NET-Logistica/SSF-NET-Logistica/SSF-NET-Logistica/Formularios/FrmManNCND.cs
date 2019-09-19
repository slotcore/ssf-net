using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Logistica;
using SIAC_Entidades.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Logistica;
using SIAC_Negocio.Almacen;
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
using SIAC_Negocio.Contabilidad;

namespace SSF_NET_Logistica.Formularios
{
    public partial class FrmManNCND : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_TipoNota = 0;

        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_log_compras objRegistros = new CN_log_compras();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sys_personalmodulo objpermod = new CN_sys_personalmodulo();
        CN_log_personal objLogPer = new CN_log_personal();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_alm_inventariounimed ObjAlmUniMed = new CN_alm_inventariounimed();
        CN_sun_tipexi objTipExi = new CN_sun_tipexi();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_sun_tipmon objMoneda = new CN_sun_tipmon();
        CN_sun_tipope objTipOpe = new CN_sun_tipope();
        CN_sun_fe_catalogo09 objCat09 = new CN_sun_fe_catalogo09();
        CN_sun_fe_catalogo10 objCat10 = new CN_sun_fe_catalogo10();
        CN_mae_condpago objCondPag = new CN_mae_condpago();
        CN_mae_clipro objPro = new CN_mae_clipro();
        CN_con_tc objTC = new CN_con_tc();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_LOG_COMPRAS e_Cabecera = new BE_LOG_COMPRAS();
        List<BE_LOG_COMPRASDET> l_Detalle = new List<BE_LOG_COMPRASDET>();
        List<BE_LOG_COMPRASDOC> l_Documentos = new List<BE_LOG_COMPRASDOC>();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO

        DataTable dtForm = new DataTable();
        DataTable dtUniMed = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtLogPer = new DataTable();
        DataTable dtTipDoc = new DataTable();
        DataTable dtTipExi = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtConPag = new DataTable();
        DataTable dtMon = new DataTable();
        DataTable dtTipOpe = new DataTable();
        DataTable dtPro = new DataTable();
        DataTable dtTipDocRef = new DataTable();
        DataTable dtMotNCND = new DataTable();
        DataTable dtTc = new DataTable();

        // VARIABLES LOCALES
        int n_NumFilasDocumento = 30;                                                    // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;
        Double n_porIGV = 0;                                                             // LAMACENA EL PORCENTAJE DEL IGV
        string[,] arrCabeceraDg1 = new string[11, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[6, 5];
        string[,] arrCabeceraFlex2 = new string[3, 5];

        bool b_SeEjecuto = false;
        bool b_Agregando = false;
        string c_Numerovalidos = "1234567890." + (char)8;                                                     // + (char)8;
        string c_NumerovalidosFE = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890." + (char)8;                        // + (char)8;
        string c_Caracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManNCND()
        {
            InitializeComponent();
        }

        private void FrmManNCND_Load(object sender, EventArgs e)
        {
            b_Agregando = true;
            CargarCombos();
            ConfigurarFormulario();
            b_Agregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboTipOpe, dtTipOpe, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMoneda, dtMon, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtTipDoc, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboConPag, dtConPag, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");

            funDatos.ComboBoxCargarDataTable(CboTipDocRef, dtTipDocRef, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMotivo, dtMotNCND, "n_id", "c_des");
            
        }
        void ConfigurarFormulario()
        {
            this.Height = 592;
            this.Width = 976;
            
            n_porIGV = 18;
            LblPorIGV.Text = n_porIGV.ToString("00") + "%";
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            arrCabeceraFlex1[0, 0] = "Descripcion del Servicio";
            arrCabeceraFlex1[0, 1] = "580";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Importe Afecto";
            arrCabeceraFlex1[1, 1] = "80";
            arrCabeceraFlex1[1, 2] = "D";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Importe Inafecto";
            arrCabeceraFlex1[2, 1] = "80";
            arrCabeceraFlex1[2, 2] = "D";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_itedes";

            arrCabeceraFlex1[3, 0] = "Importe Total";
            arrCabeceraFlex1[3, 1] = "80";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "0.00";
            arrCabeceraFlex1[3, 4] = "c_itepredes";

            arrCabeceraFlex1[4, 0] = "IdItem";
            arrCabeceraFlex1[4, 1] = "0";
            arrCabeceraFlex1[4, 2] = "N";
            arrCabeceraFlex1[4, 3] = "0.00";
            arrCabeceraFlex1[4, 4] = "n_can";

            arrCabeceraFlex1[5, 0] = "IdUnimed";
            arrCabeceraFlex1[5, 1] = "0";
            arrCabeceraFlex1[5, 2] = "N";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "n_can";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtLista, 2, false);
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;

            if (n_TipoNota == 1) { this.Text = "LOGISTICA - NOTA DE CREDITO"; }
            if (n_TipoNota == 2) {this.Text = "LOGISTICA - NOTA DE DEBITO";}

            //this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        void DataTableCargar()
        {
            ListarItems();                                                          // CARGAMOS LOS DATOS DEL FORMULARIO

            objFormVis.mysConec = mysConec;                                         // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(4, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                            // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(27);

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objItems.mysConec = mysConec;
            dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);                 // CARGAMOS TODOS LOS ITEMS
            dtItems = funDatos.DataTableFiltrar(dtItems, "n_idtipexi IN (1, 3, 4, 5, 6, 7, 8, 20, 21, 22, 23)");                            // FILTYRAMOS SOLO LOS SERVICIOS

            objTipExi.mysConec = mysConec;
            dtTipExi = objTipExi.Listar();

            objTipDoc.mysConec = mysConec;
            dtTipDoc = objTipDoc.Listar();
            dtTipDocRef = objTipDoc.Listar();
            dtTipDocRef = funDatos.DataTableFiltrar(dtTipDocRef, "n_id IN (2,3,4,5,8,9)");
            if (n_TipoNota == 1) { dtTipDocRef = funDatos.DataTableFiltrar(dtTipDocRef, "n_id IN (2,3,4,5,9)"); }
            if (n_TipoNota == 2) { dtTipDocRef = funDatos.DataTableFiltrar(dtTipDocRef, "n_id IN (2,3,4,5,8)"); }
            
            
            objCondPag.mysConec = mysConec;
            dtConPag = objCondPag.Listar();

            objMoneda.mysConec = mysConec;
            dtMon = objMoneda.Listar();

            objTipOpe.mysConec = mysConec;
            dtTipOpe = objTipOpe.Listar();

            ObjAlmUniMed.mysConec = mysConec;
            dtUniMed = ObjAlmUniMed.Listar();

            objLogPer.mysConec = mysConec;
            objLogPer.Listar(STU_SISTEMA.EMPRESAID);
            dtLogPer = objLogPer.dtPersonal;

            objPro.mysConec = mysConec;
            dtPro = objPro.ListarProveedor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
            
            if (n_TipoNota == 1)
            {
                objCat09.mysConec = mysConec;
                objCat09.Listar();
                dtMotNCND = objCat09.dtLista;
            }
            else
            {
                objCat10.mysConec = mysConec;
                objCat10.Listar();
                dtMotNCND = objCat10.dtLista;
            }

            objTC.mysConec = mysConec;
            objTC.Listar(0, 0);
            dtTc = objTC.dtLista;
            objTC = null;
        }
        void ListarItems()
        {
            objRegistros.mysConec = mysConec;
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 1);

            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);

            dtLista = objRegistros.dtLista;
            if (n_TipoNota == 1) { dtLista = funDatos.DataTableFiltrar(dtLista, "(n_idtipdoc = 8)"); }
            if (n_TipoNota == 2) { dtLista = funDatos.DataTableFiltrar(dtLista, "(n_idtipdoc = 9)"); }

            LblNumReg.Text = "0";
            LblNumReg.Text = funFunciones.NulosN((dtLista.Rows.Count)).ToString();
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

            b_Agregando = true;

            objRegistros.mysConec = mysConec;
            l_Detalle.Clear();
            l_Documentos.Clear();

            if (objRegistros.TraerRegistro(n_IdRegistro) == true)
            {
                e_Cabecera = objRegistros.e_Compras;
                l_Detalle = objRegistros.e_ComprasDet;
            }
            LblNumAsi.Text = e_Cabecera.c_numreg;
            TxtFchEmi.Text = e_Cabecera.d_fchdoc.ToString("dd/MM/yyyy");
            CboMoneda.SelectedValue = e_Cabecera.n_idmon;
            CboTipOpe.SelectedValue = e_Cabecera.n_idtipope;

            LblIdPro.Text = e_Cabecera.n_idpro.ToString();
            TxtNumRuc.Text = funDatos.DataTableBuscar(dtPro, "n_id", "c_numdoc", e_Cabecera.n_idpro.ToString(), "N").ToString();
            TxtProv.Text = funDatos.DataTableBuscar(dtPro, "n_id", "c_nombre", e_Cabecera.n_idpro.ToString(), "N").ToString();

            TxtTipCam.Text = e_Cabecera.n_tc.ToString("0.000");
            CboTipDoc.SelectedValue = e_Cabecera.n_idtipdoc;
            TxtNumSer.Text = e_Cabecera.c_numser;
            TxtNumDoc.Text = e_Cabecera.c_numdoc;
            CboConPag.SelectedValue = e_Cabecera.n_idconpag;
            TxtFchVen.Text = e_Cabecera.d_fchven.ToString("dd/MM/yyyy");
            TxtGlo.Text = e_Cabecera.c_glosa;
            CboTipOpe.SelectedValue = e_Cabecera.n_idtipope;

            //TxtBasImp1.Text = e_Cabecera.n_impbru.ToString("0.00");
            //TxtBasImp2.Text = e_Cabecera.n_impbru2.ToString("0.00");
            //TxtBasImp3.Text = e_Cabecera.n_impbru3.ToString("0.00");
            //TxtInafec.Text = e_Cabecera.n_impinaf.ToString("0.00");

            //TxtIgv1.Text = e_Cabecera.n_impigv.ToString("0.00"); ;
            //TxtIgv2.Text = e_Cabecera.n_impigv2.ToString("0.00"); ;
            //TxtIgv3.Text = e_Cabecera.n_impigv3.ToString("0.00"); ;
            TxtImpTot.Text = e_Cabecera.n_imptotcom.ToString("0.00");
            TxtImpuesto.Text = e_Cabecera.n_impigv.ToString("0.00");
            TxtImponible.Text = e_Cabecera.n_impbru.ToString("0.00"); 
                        
            if (n_TipoNota == 1) { CboMotivo.SelectedValue = e_Cabecera.n_idmotnc; }
            if (n_TipoNota == 2) { CboMotivo.SelectedValue = e_Cabecera.n_idmotnd; }

            CboTipDocRef.SelectedValue = e_Cabecera.n_idtipdocmod;
            LblIdDocref.Text = e_Cabecera.n_iddocmod.ToString();

            TxtNumDocRef.Text = "";
            if (e_Cabecera.n_iddocmod != 0)
            {
                BE_LOG_COMPRAS entCom = new BE_LOG_COMPRAS();
                CN_log_compras objCom = new CN_log_compras();
                objCom.mysConec = mysConec;
                if (objCom.TraerRegistro(e_Cabecera.n_iddocmod) == true)
                {
                    entCom = objCom.e_Compras;
                    TxtNumDocRef.Text = entCom.c_numser + "-" + entCom.c_numdoc;
                }
            }
            
            FgItems.Rows.Count = 2;
            for (n_row = 0; n_row <= l_Detalle.Count - 1; n_row++)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;

                c_dato = l_Detalle[n_row].n_iditem.ToString();                                 // DESCRIPCION DEL ITEM
                c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", c_dato, "N").ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 1, c_dato);

                
                if (Convert.ToDouble(l_Detalle[n_row].n_idtipafeigv) == 10)
                {
                    c_dato = l_Detalle[n_row].n_preunibru.ToString("0.000000");                    // PRECIO UNITARIO DEL ITEM
                    FgItems.SetData(FgItems.Rows.Count - 1, 2, c_dato);
                    FgItems.SetData(FgItems.Rows.Count - 1, 3, "");
                }
                
                if (Convert.ToDouble(l_Detalle[n_row].n_idtipafeigv) == 20)
                {
                    c_dato = l_Detalle[n_row].n_preunibru.ToString("0.000000");                    // PRECIO UNITARIO DEL ITEM
                    FgItems.SetData(FgItems.Rows.Count - 1, 2, "");
                    FgItems.SetData(FgItems.Rows.Count - 1, 3, c_dato);
                }

                c_dato = l_Detalle[n_row].n_imptot.ToString("0.00");                           // IMPORTE TOTAL DEL ITEM
                FgItems.SetData(FgItems.Rows.Count - 1, 4, c_dato);

                c_dato = l_Detalle[n_row].n_iditem.ToString("0.00");                           // ID DEL ITEM                    
                FgItems.SetData(FgItems.Rows.Count - 1, 5, c_dato);

                c_dato = l_Detalle[n_row].n_idunimed.ToString("0.00");                         // ID DE LA UNIDAD DE MEDIDA DEL ITEM
                FgItems.SetData(FgItems.Rows.Count - 1, 6, c_dato);
            }

            b_Agregando = false;
        }
        void Nuevo()
        {
            n_QueHace = 1;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            Tab1.SelectedIndex = 1;
            TxtFchEmi.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CboMoneda.SelectedValue = 115;
            CboTipOpe.SelectedValue = 1;
            CboConPag.SelectedValue = 1;
            if (n_TipoNota == 1) { CboTipDoc.SelectedValue = 8; }                           // SELECCIONAMOS LA NOTA DE CREDITO 
            if (n_TipoNota == 2) { CboTipDoc.SelectedValue = 9; }                           // SELECCIONAMOS LA NOTA DE DEBITO 
            TxtTipCam.Text = STU_SISTEMA.TIPOCAMBIO.ToString("0.0000");
            TxtNumSer.Text = "0001";
            FgItems.Cols[1].ComboList = "...";
            TxtNumRuc.Focus();
        }
        void Blanquea()
        {
            LblIdPro.Text = "";
            TxtNumRuc.Text = "";

            TxtFchEmi.Text = "";
            TxtFchEmi.CustomFormat = "dd/MM/yyyy";
            TxtFchEmi.Format = DateTimePickerFormat.Custom;

            CboMoneda.SelectedValue = 0;
            CboTipDoc.SelectedValue = 0;
            CboMotivo.SelectedValue = 0;
            CboTipDocRef.SelectedValue = 0;
            TxtProv.Text = "";
            //CboTipDoc.SelectedValue = 0;
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtTipCam.Text = "";
            CboConPag.SelectedValue = 0;
            TxtFchVen.Text = "";
            TxtFchVen.CustomFormat = "dd/MM/yyyy";
            TxtFchVen.Format = DateTimePickerFormat.Custom;
            TxtGlo.Text = "";

            LblNumReg.Text = "";
            LblNumAsi.Text = "";
            TxtNumDocRef.Text = "";

            TxtImpTot.Text = "";
            TxtImpuesto.Text = "";
            TxtImponible.Text = "";

            FgItems.Rows.Count = 2;
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;
        }
        void Bloquea()
        {
            TxtNumRuc.Enabled = !TxtNumRuc.Enabled;

            TxtFchEmi.Enabled = !TxtFchEmi.Enabled;
            CboMoneda.Enabled = !CboMoneda.Enabled;
            
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            CboConPag.Enabled = !CboConPag.Enabled;
            TxtFchVen.Enabled = !TxtFchVen.Enabled;
            TxtGlo.Enabled = !TxtGlo.Enabled;
            TxtTipCam.Enabled = !TxtTipCam.Enabled;
            CboTipOpe.Enabled = !CboTipOpe.Enabled;
            CboTipDocRef.Enabled = !CboTipDocRef.Enabled;
            CboMotivo.Enabled = !CboMotivo.Enabled;

            CmdBusPro.Enabled = !CmdBusPro.Enabled;
            CmdBusDocRef.Enabled = !CmdBusDocRef.Enabled;

            CmdDelItem.Enabled = !CmdDelItem.Enabled;
        }
        void MostrarEstadoMes(int n_IdEmpresa, int n_IdMes)
        {
            obj_fungen.mysConec = mysConec;
            if (obj_fungen.EstadoPeriodo(n_IdEmpresa, n_IdMes, 1) == true)
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
            ToolExportar.Enabled = !ToolExportar.Enabled;
        }
        void Modificar()
        {
            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            FgItems.Cols[1].ComboList = "...";
            TxtFchEmi.Focus();
        }
        bool EliminarRegistro()
        {
            bool b_Result = false;
            int n_IdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(n_IdRegistro) == true)
                {
                    b_Result = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            return b_Result;
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
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(e_Cabecera, l_Detalle, l_Documentos);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(e_Cabecera, l_Detalle, l_Documentos);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.n_ErrorNumber.ToString() + " = " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_row = 0;
            l_Detalle.Clear();
            l_Documentos.Clear();

            if (n_QueHace == 1) { e_Cabecera.n_id = 0; }
            if (n_QueHace == 2) { e_Cabecera.n_id = e_Cabecera.n_id; }
            e_Cabecera.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Cabecera.n_anotra = STU_SISTEMA.ANOTRABAJO;
            e_Cabecera.n_idmes = STU_SISTEMA.MESTRABAJO;
            e_Cabecera.n_idlib = 8;
            e_Cabecera.c_numreg  = LblNumAsi.Text;
            e_Cabecera.n_idtippro = 0;
            e_Cabecera.n_idpro = Convert.ToInt32(LblIdPro.Text);
            e_Cabecera.n_idtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);
            e_Cabecera.c_numser = TxtNumSer.Text;
            e_Cabecera.c_numdoc = TxtNumDoc.Text;
            e_Cabecera.d_fchdoc = Convert.ToDateTime(TxtFchEmi.Text);
            e_Cabecera.d_fchreg = Convert.ToDateTime(TxtFchEmi.Text);
            e_Cabecera.n_idconpag = Convert.ToInt32(CboConPag.SelectedValue);
            e_Cabecera.d_fchven = Convert.ToDateTime(TxtFchVen.Text);
            e_Cabecera.n_idmon = Convert.ToInt32(CboMoneda.SelectedValue);
            e_Cabecera.n_impbru = Convert.ToDouble(TxtImponible.Text);
            e_Cabecera.n_impbru2 = 0;
            e_Cabecera.n_impbru3 = 0;
            e_Cabecera.n_impinaf = 0;
            e_Cabecera.n_impigv = Convert.ToDouble(TxtImpuesto.Text); ;
            e_Cabecera.n_impigv2 = 0;
            e_Cabecera.n_impigv3 = 0;
            e_Cabecera.n_impisc = 0;
            e_Cabecera.n_impotr = 0;
            e_Cabecera.n_imptotcom = Convert.ToDouble(TxtImpTot.Text);
            e_Cabecera.n_tc = Convert.ToDouble(TxtTipCam.Text);
            e_Cabecera.n_impsal = Convert.ToDouble(TxtImpTot.Text);
            e_Cabecera.n_tasaigv = n_porIGV;
            e_Cabecera.c_glosa = TxtGlo.Text;
            e_Cabecera.n_estado = 1;
            e_Cabecera.n_idtipdocref = 0;
            e_Cabecera.n_iddocref = 0;
            e_Cabecera.n_idtipope = Convert.ToInt32(CboTipOpe.SelectedValue);
            e_Cabecera.n_idtipdocmod = Convert.ToInt32(CboTipDocRef.SelectedValue);
            e_Cabecera.n_iddocmod = Convert.ToInt32(LblIdDocref.Text);
            if (n_TipoNota == 1) {
                                    e_Cabecera.n_idmotnc = Convert.ToInt32(CboMotivo.SelectedValue);
                                    e_Cabecera.n_idmotnd = 0; 
                                 }
            if (n_TipoNota == 2) {
                                    e_Cabecera.n_idmotnc = 0; 
                                    e_Cabecera.n_idmotnd = Convert.ToInt32(CboMotivo.SelectedValue); 
                                 }
            
            e_Cabecera.n_tasa4ta = 0;
            e_Cabecera.n_imp4ta = 0;
            e_Cabecera.n_idtipcom = 1;                                                 // LE INDICAMOS QUE EL DOCUMENTO ES SIN DOCUMENTO DE REFERENCIA
            e_Cabecera.n_tipcom = 1;                                                   // INDICAMOS QUE ES COMPRA NACIONAL
            // CARGAMOS EL DETALLE DEL DOCUMENTO
            for (n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
            {
                BE_LOG_COMPRASDET e_Det = new BE_LOG_COMPRASDET();
                
                if (funFunciones.NulosC(FgItems.GetData(n_row, 1)).ToString() != "")
                {
                    e_Det.n_idcom = 0;
                    e_Det.n_iditem = Convert.ToInt32(FgItems.GetData(n_row, 5));                // ID DEL ITEM
                    e_Det.n_idunimed = Convert.ToInt32(FgItems.GetData(n_row, 6));              // ID DE LA UNIDAD DE MEDIDA
                    e_Det.n_canpro = 1;                                                         // CANTIDAD DEL PRODUCTO
                    if (Convert.ToDouble(FgItems.GetData(n_row, 2)) != 0)
                    { 
                        e_Det.n_preunibru = Convert.ToDouble(FgItems.GetData(n_row, 2));            // IMPORTE BRUTO DEL ITEM
                        e_Det.n_idtipafeigv = 10;
                        e_Det.n_preuni = Convert.ToDouble(FgItems.GetData(n_row, 2));               // PRECIO UNITARIO DESPUES DEL DESCUENTO
                        e_Det.n_imptot = Convert.ToDouble(FgItems.GetData(n_row, 2));               // IMPORTE TOTAL DEL ITEM DESPUES DEL DESCUENTO

                    }
                    if (Convert.ToDouble(FgItems.GetData(n_row, 3)) != 0)
                    {
                        e_Det.n_preunibru = Convert.ToDouble(FgItems.GetData(n_row, 3));            // IMPORTE BRUTO DEL ITEM
                        e_Det.n_idtipafeigv = 20;
                        e_Det.n_preuni = Convert.ToDouble(FgItems.GetData(n_row, 3));               // PRECIO UNITARIO DESPUES DEL DESCUENTO
                        e_Det.n_imptot = Convert.ToDouble(FgItems.GetData(n_row, 3));               // IMPORTE TOTAL DEL ITEM DESPUES DEL DESCUENTO

                    }
                    e_Det.n_idpordsc = 0;                                                       // PORCENTAJE DE DESCUENTO DEL ITEM
                    e_Det.n_impdsc = 0;                                                         // IMPORTE DEL PORCENTAJE DE DESCUENTO
                    //e_Det.n_preuni = Convert.ToDouble(FgItems.GetData(n_row, 2));               // PRECIO UNITARIO DESPUES DEL DESCUENTO
                    //e_Det.n_imptot = Convert.ToDouble(FgItems.GetData(n_row, 2));               // IMPORTE TOTAL DEL ITEM DESPUES DEL DESCUENTO

                    l_Detalle.Add(e_Det);
                }
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (TxtProv.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el proveedor del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtProv.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboTipDoc.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTipDoc.Focus();
                return booEstado;
            }
            if (TxtNumSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumSer.Focus();
                return booEstado;
            }
            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumDoc.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboConPag.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la condicion de pago del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboConPag.Focus();
                return booEstado;
            }
            if (TxtFchVen.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de vencimiento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchVen.Focus();
                return booEstado;
            }
            if (TxtImponible.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el detalle del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                FgItems.Focus();
                return booEstado;
            }
            if (TxtTipCam.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el tipo de cambio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtTipCam.Focus();
                return booEstado;
            }
            if (TxtImpTot.Text == "")
            {
                MessageBox.Show("¡ El importe total del documento no puede ser 0 !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtTipCam.Focus();
                return booEstado;
            }

            return booEstado;
        }
        private void ToolNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }
        private void ToolModificar_Click(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            objRegistros.TraerRegistro(intIdRegistro);
            e_Cabecera = objRegistros.e_Compras;
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
                ListarItems();
                DialogResult Rpta;
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();

                if (n_QueHace == 1)
                {
                    Rpta = MessageBox.Show("! El registro se agrego con exito ¡ ¿Desea agregar otro registro? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    Rpta = MessageBox.Show("! El registro se actualizo con exito ¡ ¿Desea agregar otro registro? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                }

                if (DialogResult.Yes == Rpta)
                {
                    Nuevo();
                    ActivarTool();           // LLAMAMOS DE NUEVO A ESTA FUNCION PORQUE SE LLAMA DENTRO DE LA FUNCION NUEVO
                    Bloquea();               // LLAMAMOS DE NUEVO A ESTA FUNCION PORQUE SE LLAMA DENTRO DE LA FUNCION NUEVO
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
            objRegistros = null;
            dtRegistro = null;
            dtLista = null;
            objFormVis = null;

            this.Close();
        }
        private void FrmManNCND_Activated(object sender, EventArgs e)
        {
            if (b_SeEjecuto == false)
            {
                b_SeEjecuto = true;
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
        private void FrmManNCND_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            VerRegistro(intIdRegistro);
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
                int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    VerRegistro(intIdRegistro);
                }
            }
        }
        private void CmdBusPro_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            objPro.mysConec = mysConec;
            dtResult = objPro.BuscarCliPro(dtPro, 1, "n_id", "");
            if (dtResult != null)
            { 
                if (dtResult.Rows.Count != 0)
                {
                    LblIdPro.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtProv.Text = dtResult.Rows[0]["c_nombre"].ToString();
                    TxtNumRuc.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                }
                else
                {
                    LblIdPro.Text = "";
                    TxtProv.Text = "";
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
        private void CboMoneda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboTipOpe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtProv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtTipCam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboTipDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtNumSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000" + TxtNumSer.Text;

                TxtNumSer.Text = strCad.Substring(strCad.Length - 4, 4);
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!c_NumerovalidosFE.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtNumSer_Validated(object sender, EventArgs e)
        {
            if (TxtNumSer.Text != "")
            {
                string strCad = "0000" + TxtNumSer.Text;
                TxtNumSer.Text = strCad.Substring(strCad.Length - 4, 4);
            }
        }
        private void TxtNumDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000000000" + TxtNumDoc.Text;

                TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!c_Numerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text != "")
            {
                string strCad = "0000000000" + TxtNumDoc.Text;
                TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
            }
        }
        private void CboConPag_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtFchVen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                TxtFchVen.CustomFormat = " ";
                TxtFchVen.Format = DateTimePickerFormat.Custom;

                SendKeys.Send("{TAB}");
            }
        }
        private void TxtGlo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!c_Caracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtImponible_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtImpuesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtImpTot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CmdBusDocRef_Click(object sender, EventArgs e)
        {
            if (TxtProv.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre del proveedor !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtProv.Focus();
                return;
            }
            if (Convert.ToInt32(CboTipDocRef.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento de referencia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboTipDocRef.Focus();
                return;
            }
            DataTable dtResult = new DataTable();
            objRegistros.mysConec = mysConec;
            dtResult = objRegistros.BuscarDocumentoProveedor(STU_SISTEMA.EMPRESAID,Convert.ToInt32(LblIdPro.Text));
            if (dtResult != null)
            { 
                if (dtResult.Rows.Count != 0)
                {
                    TxtNumDocRef.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                    LblIdDocref.Text = dtResult.Rows[0]["n_id"].ToString();
                }
                else
                {
                    TxtNumDocRef.Text = "";
                    LblIdDocref.Text = "";
                }
            }
        }

        private void FgItems_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgItems.Col == 1)
            {
                int n_idtipexi = 0;
                DataTable dtResul = new DataTable();
                string c_dato = "";

                b_Agregando = true;
                dtResul = objItems.BuscarItem("", "n_id", dtItems, n_idtipexi);
                if (dtResul != null)
                { 
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["c_despro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                        FgItems.SetData(FgItems.Row, 1, c_dato);

                        c_dato = dtResul.Rows[0]["n_id"].ToString();            // MOSTRAMOS EL ID DEL ITEM
                        FgItems.SetData(FgItems.Row, 5, c_dato);

                        c_dato = dtResul.Rows[0]["n_idunimed"].ToString();      // MOSTRAMOS LA UNIDAD DE MEDIDA DEL ITEM
                        FgItems.SetData(FgItems.Row, 6, c_dato);              
                    }
                }
                b_Agregando = false;
            }
        }

        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }
            DataTable dtResul = new DataTable();
            //string c_dato = "";
            if (b_Agregando == true) { return; }

            //if (FgItems.Col == 1)
            //{
            //    // FILTRAMOS LAS PRESENTACIONES DEL ITEM SELECCIONADO
            //    c_dato = FgItems.GetData(FgItems.Row, 9).ToString();                        // OBTENEMOS EL ID DEL ITEM
            //    dtResul = funDatos.DataTableFiltrar(dtUniMed, "n_idite = " + c_dato + "");
            //    funFlex.FlexColumnaCombo(FgItems, dtResul, "c_abrpre", 2);                  // UNIDADES DEL MEDIDA DEL ITEMS

            //    FgItems.AllowEditing = true;
            //    FgItems.Select(FgItems.Row - 1, 2);
            //    return;
            //}
            if ((FgItems.Col == 2) || (FgItems.Col == 3))
            {
                CalcularTotalFila();
                FgItems.Select(FgItems.Row - 1, 1);
                return;
            }
            if (FgItems.Col == 4)
            {
                FgItems.Select(FgItems.Row - 1, 3);
                FgItems.SetData(FgItems.Row + 1, 1, FgItems.GetData(FgItems.Row, 1));
                return;
            }           
        }
        void CalcularTotalFila()
        {
            int n_row =0;

            for (n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
            { 

                double n_impuniafe = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 2)));
                double n_impuniina = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 3)));
                double n_imptot = (n_impuniafe + n_impuniina);
                if (funFunciones.NulosC(FgItems.GetData(n_row, 1)) != "")
                { 
                    FgItems.SetData(n_row, 4, n_imptot.ToString("0.00"));
                }
            }

            double n_totafe = funFlex.FlexSumarCol(FgItems, 2, 2, FgItems.Rows.Count - 1);
            double n_totina = funFlex.FlexSumarCol(FgItems, 3, 2, FgItems.Rows.Count - 1);
            double n_impigv = ((n_totafe * ((n_porIGV / 100) + 1)) - n_totafe);
            double n_totbru = (n_totafe + n_totina);
            TxtImponible.Text = n_totbru.ToString("0.00");
            TxtImpuesto.Text = n_impigv.ToString("0.00");
            TxtImpTot.Text = (n_totbru + n_impigv).ToString("0.00");
        }
        private void FgItems_EnterCell(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count == 2) { return; }
            if (b_Agregando == true) { return; }

            DataTable dtResul = new DataTable();
            //int n_idtipproducto = 0;
            //string strDesTipPro = "";

            if ((FgItems.Col == 4) || (FgItems.Col == 5))
            {
                FgItems.AllowEditing = false;
            }
            else
            {
                FgItems.AllowEditing = true;
            }
            //if (FgItems.Col == 1)
            //{
            //    if (FgItems.Row >= 2)
            //    {
            //        if (funFunciones.NulosC(FgItems.GetData(FgItems.Row - 1, 1)) == "") { FgItems.AllowEditing = false; return; }
            //    }
            //    funFlex.FlexColumnaCombo(FgItems, dtTipoExis, "c_des", 1);
            //}

            //if (FgItems.Col == 2)
            //{
            //    // OBTNEMOS LA DESCRIPCION DEL TIPO DE PRODUCTO
            //    strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 1));

            //    if (strDesTipPro == "")
            //    {
            //        FgItems.AllowEditing = false;
            //        funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 2);
            //        return;
            //    }
            //    // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
            //    dtResul = funDatos.DataTableFiltrar(dtTipoExis, "c_des = '" + strDesTipPro + "'");
            //    if (dtResul.Rows.Count != 0)
            //    {
            //        n_idtipproducto = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());
            //    }
            //    FgItems.Cols[2].ComboList = "...";
            //}
            //if (FgItems.Col == 3)
            //{
            //    // OBTENEMOS LA DESCRIPCIO DEL ITEM
            //    strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 2));

            //    if (strDesTipPro == "")
            //    {
            //        FgItems.AllowEditing = false;
            //        funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 2);
            //        return;
            //    }

            //    // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
            //    dtResul = funDatos.DataTableFiltrar(dtItems, "c_despro = '" + strDesTipPro + "'");
            //    if (dtResul.Rows.Count != 0)
            //    {
            //        n_idtipproducto = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());

            //        // FILTRAMOS LAS PRESENTACIONES DEL ITEM SELECCIONADO
            //        dtResul = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_idtipproducto + "");
            //        funFlex.FlexColumnaCombo(FgItems, dtResul, "c_abrpre", 3);    // ITEMS
            //    }
            //}

            //if (FgItems.Col == 4)
            //{
            //    // OBTENEMOS LA DESCRIPCIO DEL ITEM
            //    strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 3));

            //    if (strDesTipPro == "")
            //    {
            //        FgItems.AllowEditing = false;
            //        return;
            //    }
            //}
            //FgItems.AllowEditing = true;
        }

        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 2)
            {
                if (!c_Caracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 3)
            {
                if (!c_Numerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void FgItems_RowColChange(object sender, EventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }

            if (b_Agregando == true) { return; }
        }

        private void CmdDelItem_Click(object sender, EventArgs e)
        {
            FgItems.RemoveItem(FgItems.Row);
            FgItems.Rows.Count = FgItems.Rows.Count + 1;
        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            if (b_Agregando == true) { return; }

            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            ListarItems();
            DgLista.DataSource = dtLista;
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

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-LOG-NCND-" + STU_SISTEMA.ANOTRABAJO.ToString() + Convert.ToInt16(CboMeses.SelectedValue).ToString("00") + ".xls";
            DgLista.ExportTo(c_NomArchivo);
            MessageBox.Show("! Se exporto con exito la informacion en el archivo " + c_NomArchivo + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void CmdVerAsiento_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objConta = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objConta.mysConec = mysConec;
            objConta.STU_SISTEMA = STU_SISTEMA;
            objConta.VerAsiento(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboMeses.SelectedValue), 8, LblNumAsi.Text);
            objConta = null;
        }

        private void TxtNumRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!c_Numerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void TxtNumRuc_Validated(object sender, EventArgs e)
        {
            DataTable dtresul = new DataTable();
            dtresul = funDatos.DataTableFiltrar(dtPro, "c_numdoc = '" + TxtNumRuc.Text + "'");

            if (dtresul.Rows.Count != 0)
            {
                TxtNumRuc.Text = dtresul.Rows[0]["c_numdoc"].ToString();
                TxtProv.Text = dtresul.Rows[0]["c_nombre"].ToString();
                LblIdPro.Text = dtresul.Rows[0]["n_id"].ToString();
            }
            else
            {
                TxtNumRuc.Text = "";
                TxtProv.Text = "";
                LblIdPro.Text = "";
            }
        }

        private void TxtFchEmi_Validated(object sender, EventArgs e)
        {
            if (b_Agregando == true) { return; }
            if (TxtFchEmi.Text != "")
            {
                DataTable dtResul = new DataTable();
                dtResul = funDatos.DataTableFiltrar(dtTc, "d_fecha = '" + TxtFchEmi.Text + "'");
                if (dtResul.Rows.Count != 0)
                {
                    TxtTipCam.Text = dtResul.Rows[0]["n_impven"].ToString();
                }
            }
        }

        private void CmdGenAsi_Click(object sender, EventArgs e)
        {
            objRegistros.mysConec = mysConec;
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            if (objRegistros.RegeneraAsientos(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 8, 1) == true)
            {
                MessageBox.Show("¡ Los asientos contables se crearon con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                ListarItems();
                DgLista.DataSource = dtLista;
            }
        }
    }
}
