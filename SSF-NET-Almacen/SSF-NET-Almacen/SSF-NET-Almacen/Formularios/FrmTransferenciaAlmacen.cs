using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Produccion;
using SIAC_Entidades.Sistema;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Produccion;
using SIAC_Negocio.Sunat;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using System.Data.OleDb;
using System.Configuration;
using SIAC_Entidades.Logistica;
using SIAC_Negocio.Logistica;

namespace SSF_NET_Almacen.Formularios
{
    public partial class FrmTransferenciaAlmacen : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public OleDbConnection AccConec = new OleDbConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_sun_tipexi objTipoExi = new CN_sun_tipexi();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_alm_almacenes ObjAlm = new CN_alm_almacenes();
        CN_alm_inventariounimed ObjAlmUniMed = new CN_alm_inventariounimed();
        CN_alm_personal ObjRes = new CN_alm_personal();
        CN_alm_almacenesdoc ObjAlmDoc = new CN_alm_almacenesdoc();
        CN_alm_inventariolotes ObjLotes = new CN_alm_inventariolotes();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_empresa funsys = new CN_sys_empresa();
        CN_alm_transferencias objTransferencias = new CN_alm_transferencias();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_sun_unimed objUniMed = new CN_sun_unimed();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_alm_inventariolotes objlotes = new CN_alm_inventariolotes();
        CN_ACC_pro_solicitudmat objSolMar = new CN_ACC_pro_solicitudmat();
        CN_ACC_pro_producciondet objProPro = new CN_ACC_pro_producciondet();
        CN_sys_setup o_set = new CN_sys_setup();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_ALM_TRANSFERENCIAS BE_Transferencia = new BE_ALM_TRANSFERENCIAS();
        BE_SYS_EMPRESA entEmp = new BE_SYS_EMPRESA();

        // DATATABLE LOCALES
        DataTable dtAlmacenesOrigen = new DataTable();
        DataTable dtAlmacenesDestino = new DataTable();
        DataTable dtTipoExis = new DataTable();
        DataTable dtResponsables = new DataTable();
        DataTable dtTransferencias = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtMoviDetalle = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtPresentaItem = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtDocAlm = new DataTable();
        DataTable dtLotes = new DataTable();
        DataTable DtSolMat = new DataTable();
        DataTable DtDetalleTMP = new DataTable();                                       // EN ESTE DATA TABLE SE ALMACENA EL DETALLE DE LOS DOCUMENTOS JALADOS
        DataTable dtsetup = new DataTable();

        // VARIABLES LOCALES
        int N_IDALMACEN = 0;
        int N_IDLOCAL = 0;
        int N_INGTRAZABALIDAD = 0;                                                       // LE INDICAMOS AL MODULO QUE TIENE QUE PEDIR DATOS DE TRAZABILIDAD
        int N_INGPRECIO = 0;
        int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[10, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[9, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;
        int n_ItemProducido;                                                            // ESTA VARIABLE ALMACENARA EL ID DEL ITEM PRODUCIDO CUANDO SE SELECCIONE UNA ORDEN DE PRODUCCION
        
        public FrmTransferenciaAlmacen()
        {
            InitializeComponent();
        }

        private void FrmSalidaAlmacen3_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }

        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboAlmacenOrigen, dtAlmacenesOrigen, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboAlmacenDestino, dtAlmacenesDestino, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboResponsable, dtResponsables, "n_id", "destra");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
        }

        void ConfigurarFormulario()
        {
            Tab1.SelectedIndex = 0;
            string c_nomarc = ConfigurationManager.AppSettings["PathIniFile"];
            N_IDALMACEN = Convert.ToInt32(funDatos.IniLeerSeccion(c_nomarc, "SISTEMA", "ALMACEN").ToString());

            if (dtsetup.Rows.Count != 0)
            {
                if (Convert.ToInt32(dtsetup.Rows[0]["n_almtrazabilidad"]) == 1)
                {
                    N_INGTRAZABALIDAD = 1;
                }
                if (Convert.ToInt32(dtsetup.Rows[0]["n_guipedpre"]) == 1)
                {
                    N_INGPRECIO = 1;
                }
            }

            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            arrCabeceraFlex1[0, 0] = "Tipo Item";
            arrCabeceraFlex1[0, 1] = "100";
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Item";
            if (N_INGTRAZABALIDAD == 1) { arrCabeceraFlex1[1, 1] = "280"; }
            if (N_INGTRAZABALIDAD == 0) { arrCabeceraFlex1[1, 1] = "400"; }
            //arrCabeceraFlex1[1, 1] = "280";
            arrCabeceraFlex1[1, 2] = "S";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Uni. Med.";
            arrCabeceraFlex1[2, 1] = "40";
            arrCabeceraFlex1[2, 2] = "S";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_itepredes";

            arrCabeceraFlex1[3, 0] = "Nº Lote";
            if (N_INGTRAZABALIDAD == 1) { arrCabeceraFlex1[3, 1] = "80"; }
            if (N_INGTRAZABALIDAD == 0) { arrCabeceraFlex1[3, 1] = "0"; }
            //arrCabeceraFlex1[3, 1] = "75";
            arrCabeceraFlex1[3, 2] = "C";
            arrCabeceraFlex1[3, 3] = "";
            arrCabeceraFlex1[3, 4] = "n_numlot";

            arrCabeceraFlex1[4, 0] = "Cantidad en Almacen";
            arrCabeceraFlex1[4, 1] = "70";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.000000";
            arrCabeceraFlex1[4, 4] = "n_can";

            arrCabeceraFlex1[5, 0] = "Cantidad Requerida";
            arrCabeceraFlex1[5, 1] = "70";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.000000";
            arrCabeceraFlex1[5, 4] = "n_can";

            arrCabeceraFlex1[6, 0] = "Precio Unitario";
            if (N_INGPRECIO == 1) { arrCabeceraFlex1[6, 1] = "70"; }
            if (N_INGPRECIO == 0) { arrCabeceraFlex1[6, 1] = "0"; }
            //arrCabeceraFlex1[6, 1] = "70";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_can";

            arrCabeceraFlex1[7, 0] = "Imp. Total";
            if (N_INGPRECIO == 1) { arrCabeceraFlex1[7, 1] = "70"; }
            if (N_INGPRECIO == 0) { arrCabeceraFlex1[7, 1] = "0"; }
            //arrCabeceraFlex1[7, 1] = "70";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "n_can";

            arrCabeceraFlex1[8, 0] = "Hora Entrega";
            arrCabeceraFlex1[8, 1] = "60";
            arrCabeceraFlex1[8, 2] = "H";
            arrCabeceraFlex1[8, 3] = "HH:mm";
            arrCabeceraFlex1[8, 4] = "n_can";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }

        void DataTableCargar()
        {            
            o_set.mysConec = mysConec;
            dtsetup = o_set.TraerRegistro();

            objTipDoc.mysConec = mysConec;

            objTipoExi.mysConec = mysConec;
            dtTipoExis = objTipoExi.Listar();

            ObjAlm.mysConec = mysConec;
            dtAlmacenesOrigen = ObjAlm.ListarNuevo(STU_SISTEMA.EMPRESAID, Convert.ToInt32(funFunciones.NulosN(dtsetup.Rows[0]["n_almunialmacenes"])));                             // 
            dtAlmacenesDestino = ObjAlm.ListarNuevo(STU_SISTEMA.EMPRESAID, Convert.ToInt32(funFunciones.NulosN(dtsetup.Rows[0]["n_almunialmacenes"])));                             // 

            ObjRes.mysConec = mysConec;
            dtResponsables = ObjRes.ListarPorCargo(STU_SISTEMA.EMPRESAID, 2);                          // 

            objItems.mysConec = mysConec;
            dtItems = objItems.ListarTabla(STU_SISTEMA.EMPRESAID);

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objTransferencias.mysConec = mysConec;
            dtTransferencias = objTransferencias.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);

            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(99);

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(99, ref arrCabeceraDg1);

            ObjAlmDoc.mysConec = mysConec;
            dtDocAlm = ObjAlmDoc.ListarEmpAlm(STU_SISTEMA.EMPRESAID, 2);               // 1 FILTRAREMOS SOLO LOS ALMACENES QUE PERTENESCAN A LA EMPRESA Y QUE GENEREN MOVIMIENTO DE INGRESO A ALMACEN 

            objlotes.mysConec = mysConec;
            dtLotes = objlotes.TraerLotesConSaldo(STU_SISTEMA.EMPRESAID);
            
            funsys.mysConec = mysConec;
            funsys.TraerRegistro(STU_SISTEMA.EMPRESAID);
            entEmp = funsys.e_Empresa;
        }

        void ListarItems()
        {
            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
            LblNumReg.Text = (dtTransferencias.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtTransferencias, true);
        }

        void VerRegistro(int n_IdRegistro)
        {
            objTransferencias.mysConec = mysConec;
            BE_Transferencia = objTransferencias.TraerRegistro(n_IdRegistro);

            txtFchIng.Text = BE_Transferencia.d_fching.ToString();
            TxtFchDoc.Text = BE_Transferencia.d_fchdoc.ToString();
            TxtNumDoc.Text = BE_Transferencia.c_numdoc;
            TxtNumSer.Text = BE_Transferencia.c_numser;            
            CboAlmacenOrigen.SelectedValue = BE_Transferencia.n_idalmorig;
            CboAlmacenDestino.SelectedValue = BE_Transferencia.n_idalmdest;
            TxtObs.Text = BE_Transferencia.c_obs;            
            CboResponsable.SelectedValue = BE_Transferencia.n_idresp;
            
            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, BE_Transferencia.lst_items, 2, false);

            int n_fila = 2;
            foreach (BE_ALM_TRANSFERENCIASDET element in BE_Transferencia.lst_items)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;
                FgItems.SetData(n_fila, 1, element.c_tipexides);
                FgItems.SetData(n_fila, 2, element.c_itedes);
                FgItems.SetData(n_fila, 3, element.c_itepredes);
                FgItems.SetData(n_fila, 4, element.c_numlot);
                FgItems.SetData(n_fila, 6, element.n_can);
                FgItems.SetData(n_fila, 7, element.n_preuni);
                FgItems.SetData(n_fila, 8, element.n_pretot);
                FgItems.SetData(n_fila, 9, element.h_horsal);

                n_fila++;
            }
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
            FgItems.Rows.Count = 2;
            booAgregando = false;
            FgItems.Cols[2].ComboList = "...";
            CboAlmacenOrigen.SelectedValue = Convert.ToInt32(N_IDALMACEN);
            txtFchIng.Focus();
        }
        void Blanquea()
        {
            txtFchIng.Text = "";
            TxtFchDoc.Text = "";
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtObs.Text = "";
            
            CboAlmacenDestino.SelectedValue = 0;            
            CboAlmacenOrigen.SelectedValue = 0;
            CboResponsable.SelectedValue = 0;
            
        }
        void Bloquea()
        {
            txtFchIng.Enabled = !txtFchIng.Enabled;
            TxtFchDoc.Enabled = !TxtFchDoc.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
            CboAlmacenDestino.Enabled = !CboAlmacenDestino.Enabled;            
            CboAlmacenOrigen.Enabled = !CboAlmacenOrigen.Enabled;
            CboResponsable.Enabled = !CboResponsable.Enabled;
            
            CmdAddItem.Enabled = !CmdAddItem.Enabled;
            CmdDelItem.Enabled = !CmdDelItem.Enabled;
        }
        void MostrarEstadoMes(int n_IdEmpresa, int n_IdMes)
        {
            obj_fungen.mysConec = mysConec;
            if (obj_fungen.EstadoPeriodo(n_IdEmpresa, n_IdMes, 3) == true)
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
            ToolManFun.Enabled = !ToolManFun.Enabled;
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

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[9].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            txtFchIng.Focus();
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[9].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            objTransferencias.mysConec = mysConec;
            BE_Transferencia = objTransferencias.TraerRegistro(intIdRegistro);

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                //objMovimientos.AccConec = AccConec;
                if (objTransferencias.Eliminar(intIdRegistro) == true)     // INDICAMOS 1 = SALIDA
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objTransferencias.mysConec = mysConec;
                    dtTransferencias = objTransferencias.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objTransferencias.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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

            if (n_QueHace == 1)
            {
                if (objTransferencias.DocumentoExiste(STU_SISTEMA.EMPRESAID, TxtNumSer.Text, TxtNumDoc.Text) == true)
                {
                    MessageBox.Show(" El numero de documento " + TxtNumSer.Text + "-" + TxtNumDoc.Text + " ya existe, ingrese otro ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return booResultado;
                }
                booResultado = objTransferencias.Insertar(BE_Transferencia);
            }

            if (n_QueHace == 2)
            {
                //objMovimientos.AccConec = AccConec;
                booResultado = objTransferencias.Actualizar(BE_Transferencia);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {
            BE_Transferencia = new BE_ALM_TRANSFERENCIAS();
            BE_Transferencia.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_Transferencia.d_fchdoc = Convert.ToDateTime(TxtFchDoc.Text);
            BE_Transferencia.d_fching = Convert.ToDateTime(txtFchIng.Text);
            BE_Transferencia.c_numser = TxtNumSer.Text;
            BE_Transferencia.c_numdoc = TxtNumDoc.Text;
            BE_Transferencia.n_idalmorig = Convert.ToInt32(CboAlmacenOrigen.SelectedValue);
            BE_Transferencia.n_idalmdest = Convert.ToInt32(CboAlmacenDestino.SelectedValue);
            BE_Transferencia.n_anotra = STU_SISTEMA.ANOTRABAJO;
            BE_Transferencia.n_idmes = STU_SISTEMA.MESTRABAJO;
            BE_Transferencia.c_obs = TxtObs.Text; ;            
            BE_Transferencia.n_idresp =  Convert.ToInt32(CboResponsable.SelectedValue);
            booAgregando = true;

            if (FgItems.Rows.Count > 2)
            {
                int n_fila;
                for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
                {
                    if (funFunciones.NulosC(FgItems.GetData(n_fila, 1)) != "")
                    {
                        BE_ALM_TRANSFERENCIASDET BE_Detalle = new BE_ALM_TRANSFERENCIASDET();

                        BE_Detalle.n_idtrans = 0;
                        BE_Detalle.n_can = Convert.ToDouble(FgItems.GetData(n_fila, 6).ToString());

                        string c_tipexides = FgItems.GetData(n_fila, 1).ToString();
                        string c_itedes = FgItems.GetData(n_fila, 2).ToString();
                        string c_itepredes = FgItems.GetData(n_fila, 3).ToString();
                        BE_Detalle.c_numlot = funFunciones.NulosC(FgItems.GetData(n_fila, 4));

                        DataTable DtFiltro = new DataTable();

                        // FILTRAMOS EL ITEM DE LA FILA PARA OBTENER EL ID  
                        string strCadenaFiltro = "c_despro = '" + c_itedes + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                        BE_Detalle.n_idite = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        // FILTRAMOS LA PRESENTACION PARA OBTENER SU ID
                        strCadenaFiltro = "c_abrpre = '" + c_itepredes + "' AND n_idite = " + BE_Detalle.n_idite + "";
                        DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                        BE_Detalle.n_idpre = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        // FILTRAMOS EL TIPO DE PRODUCTO PARA OBTENER SU id
                        strCadenaFiltro = "c_des = '" + c_tipexides + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtTipoExis, strCadenaFiltro);
                        BE_Detalle.n_idtippro = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());
                        BE_Detalle.h_horsal = funFunciones.NulosC(FgItems.GetData(n_fila, 9));
                        BE_Detalle.n_preuni = Convert.ToDouble(FgItems.GetData(n_fila, 7));
                        BE_Detalle.n_pretot = Convert.ToDouble(FgItems.GetData(n_fila, 8));

                        BE_Transferencia.lst_items.Add(BE_Detalle);
                    }
                }
            }
            booAgregando = false;
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (txtFchIng.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de ingreso !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                txtFchIng.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtFchDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchDoc.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtNumSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumSer.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumDoc.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt32(CboAlmacenOrigen.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el almacen de Origen !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboAlmacenOrigen.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt32(CboAlmacenDestino.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el almacen de Destino !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboAlmacenDestino.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt32(CboAlmacenDestino.SelectedValue) == Convert.ToInt32(CboAlmacenOrigen.SelectedValue))
            {
                MessageBox.Show("¡ Almacén origen y destino no pueden ser iguales !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboAlmacenDestino.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt32(Convert.ToDateTime(TxtFchDoc.Text).ToString("MM")) != Convert.ToInt32(CboMeses.SelectedValue))
            {
                MessageBox.Show("¡ La fecha del documento de salida no coincide con el mes de trabajo !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchDoc.Focus();
                booEstado = false;
                return booEstado;
            }

            if (FgItems.Rows.Count != 2)
            {
                int intFila;
                // VERIFICAMOS QUE LOS DATOS DE LAS PRESENTACIONES ESTEN COMPLETAS
                for (intFila = 2; intFila <= FgItems.Rows.Count - 1; intFila++)
                {
                    if (funFunciones.NulosC(FgItems.GetData(intFila, 1)) != "")
                    {
                        if (funFunciones.NulosC(FgItems.GetData(intFila, 2)) == "")
                        {
                            MessageBox.Show("¡ No ha especificado la descripcion del item en la fila " + (FgItems.Row - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
                        }
                        if (funFunciones.NulosC(FgItems.GetData(intFila, 3)) == "")
                        {
                            MessageBox.Show("¡ No ha especificado la presentacion del item en la fila " + (FgItems.Row - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
                        }
                        if (N_INGTRAZABALIDAD == 1)
                        { 
                            if (funFunciones.NulosC(FgItems.GetData(intFila, 4)) == "")
                            {
                                MessageBox.Show("¡ No ha especificado el numero de lote, en la fila " + (FgItems.Row - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                booEstado = false;
                                return booEstado;
                            }
                        }
                        if (funFunciones.NulosC(FgItems.GetData(intFila, 6)) == "")
                        {
                            MessageBox.Show("¡ No ha especificado la cantidad del item que ingresara en la fila " + (FgItems.Row - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
                        }
                        if (funFunciones.NulosC(FgItems.GetData(intFila, 9)) == "")
                        {
                            MessageBox.Show("¡ No ha especificado la hora de salida del item en la fila " + (FgItems.Row - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("¡ No ha especificado ningun item para este ingreso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            return booEstado;
        }

        private void FrmSalidaAlmacen3_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (dtTransferencias.Rows.Count == 0)
                {
                    DialogResult Rpta = MessageBox.Show("No se han encontrado registros, ¿ Desea agregar uno ahora ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (DialogResult.Yes == Rpta)
                    {
                        Nuevo();
                    }
                }
                else
                {
                    DgLista.Focus();
                }
            }  
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
                objTransferencias.mysConec = mysConec;
                dtTransferencias = objTransferencias.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();

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
            objTipoExi = null;
            dtTipoExis = null;

            objTipDoc = null;

            objTipoExi = null;
            dtTipoExis = null;

            ObjAlm = null;
            dtAlmacenesOrigen = null;

            ObjRes = null;
            dtResponsables = null;

            objTransferencias = null;
            dtTransferencias = null;

            objFormVis = null;
            objFormVis = null;

            this.Close();
        }

        private void Tab1_SelectedIndexChanging(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;

            if (n_QueHace != 3) { return; }

            if (tc.SelectedIndex == 1)
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns[9].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[9].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            VerRegistro(intIdRegistro);
            booAgregando = false;
        }

        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 4)
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
 
        }

        private void FgItems_EnterCell(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count == 2) { return; }
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }
            if (booAgregando == true) { return; }

            DataTable dtResul = new DataTable();
            int n_idtipproducto = 0;
            string strDesTipPro = "";

            if (FgItems.Col == 1)
            {
                if (FgItems.Row >= 2)
                {
                    if (funFunciones.NulosC(FgItems.GetData(FgItems.Row - 1, 1)) == "") { FgItems.AllowEditing = false; return; }
                }
                funFlex.FlexColumnaCombo(FgItems, dtTipoExis, "c_des", 1);
            }

            if (FgItems.Col == 2)
            {
                FgItems.AllowEditing = true;
            }

            if (FgItems.Col == 3)
            {
                // OBTENEMOS LA DESCRIPCIO DEL ITEM
                strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 2));

                if (strDesTipPro == "")
                {
                    FgItems.AllowEditing = false;
                    funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 2);
                    return;
                }

                // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
                dtResul = funDatos.DataTableFiltrar(dtItems, "c_despro = '" + strDesTipPro + "'");
                if (dtResul.Rows.Count != 0)
                {
                    n_idtipproducto = Convert.ToInt32(dtResul.Rows[0]["n_id"].ToString());

                    // FILTRAMOS LAS PRESENTACIONES DEL ITEM SELECCIONADO
                    dtResul = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_idtipproducto + "");
                    funFlex.FlexColumnaCombo(FgItems, dtResul, "c_abrpre", 3);    // ITEMS
                }
            }

            if (FgItems.Col == 4)
            {
                // OBTENEMOS LA DESCRIPCIO DEL ITEM
                strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 3));

                if (strDesTipPro == "")
                {
                    FgItems.AllowEditing = false;
                    return;
                }
            }
            if (FgItems.Col == 5)
            {
                FgItems.AllowEditing = false;
                return;
            }
 
            FgItems.AllowEditing = true;
        }

        private void CmdDelItem_Click(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count > 2)
            {
                FgItems.RemoveItem(FgItems.Row);
                FgItems.Focus();
            }
        }

        private void CmdAddItem_Click(object sender, EventArgs e)
        {
            bool booAgregarUnidad = true;

            if (Convert.ToString(FgItems.GetData(FgItems.Rows.Count - 1, 1)) == "") booAgregarUnidad = false;
            if (Convert.ToString(FgItems.GetData(FgItems.Rows.Count - 1, 2)) == "") booAgregarUnidad = false;
            if (Convert.ToString(FgItems.GetData(FgItems.Rows.Count - 1, 3)) == "") booAgregarUnidad = false;
            if (Convert.ToString(FgItems.GetData(FgItems.Rows.Count - 1, 4)) == "") booAgregarUnidad = false;


            if (booAgregarUnidad == true)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;
                FgItems.Focus();
            }
            else
            {
                MessageBox.Show("No puede agregar mas items hasta que no haya completado el item anterior", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void FgItems_RowColChange(object sender, EventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }
        }

        private void DgLista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DataTable dtResult = new DataTable();

                string c_CadFiltro = funDbGrid.DG_LeerCondicionesFiltro(DgLista);
                dtResult = funDbGrid.DG_Filtrar(dtTransferencias, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
        }

        private void FrmSalidaAlmacen3_Resize(object sender, EventArgs e)
        {
            //Tab_Dimensionar(Tab1, this.Height - 81, this.Width - 18);
        }

        private void TxtNumSer_TextChanged(object sender, EventArgs e)
        {
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
                string strCad = "0000000000" + TxtNumDoc.Text;

                TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
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

        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            DataTable dtResul;
            string c_despro;
            int n_idpro;

            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            if (FgItems.Col == 1)
            {
                FgItems.Select(FgItems.Row - 1, 2);
                return;
            }
            if (FgItems.Col == 2)
            {
                booAgregando = true;

                // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
                c_despro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 2));
                dtResul = funDatos.DataTableFiltrar(dtItems, "c_despro = '" + c_despro + "'");
                if (dtResul.Rows.Count != 0)
                {
                    n_idpro = Convert.ToInt32(dtResul.Rows[0]["n_id"].ToString());

                    // FILTRAMOS LAS PRESENTACIONES DEL ITEM SELECCIONADO
                    dtResul = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_idpro + " AND n_default = 1");
                    funFlex.FlexColumnaCombo(FgItems, dtResul, "c_abrpre", 3);    // ITEMS
                    if (dtResul.Rows.Count == 1)
                    {
                        FgItems.SetData(FgItems.Row, 3, dtResul.Rows[0]["c_abrpre"].ToString());
                    }
                    else
                    {
                        FgItems.SetData(FgItems.Row, 3, "");
                    }
                }
                FgItems.SetData(FgItems.Row, 7, DateTime.Now.ToString("HH:mm"));

                FgItems.Select(FgItems.Row - 1, 3);
                booAgregando = false;
                return;
            }
            if (FgItems.Col == 3)
            {
                FgItems.Select(FgItems.Row - 1, 4);
                return;
            }
            if (FgItems.Col == 4)
            {
                FgItems.Select(FgItems.Row - 1, 5);
                return;
            }
            if (FgItems.Col == 5)
            {
                FgItems.Select(FgItems.Row - 1, 6);
                return;
            }
            if (FgItems.Col == 6)
            {
                double n_saldo = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row,5)));
                double n_cansol = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row,6)));
                string c_numlote = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 4));

                //if (c_numlote != "")
                //{
                //    if (n_cansol > n_saldo)
                //    {
                //        //FgItems.SetData(FgItems.Row, 6, "");
                //        MessageBox.Show("La cantidad solicitada excede la capacidad del lote, ingrese una cantidad menor o igual al lote", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //    }
                //    else
                //    {
                //        FgItems.SetData(FgItems.Row, 6, n_cansol.ToString("0.000000"));
                //    }
                //}
                //else
                //{
                //    FgItems.SetData(FgItems.Row, 6, n_cansol.ToString("0.000000"));
                //}
                return;
            }
            if (FgItems.Col == 7)
            {
                FgItems.Select(FgItems.Row, 1);
                return;
            }
        }

        private void txtFchIng_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtFchDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtObs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            objTransferencias.mysConec = mysConec;
            dtTransferencias = objTransferencias.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO);
            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            ListarItems();

            if (dtTransferencias.Rows.Count == 0)
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
            if (TxtNumSer.Text == "") { return; }

            string c_numdoc = "";
            c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 95, TxtNumSer.Text);
            TxtNumDoc.Text = c_numdoc;
        }

        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text == "") { return; }

            string strCad = "0000000000" + TxtNumDoc.Text;
            TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
        }

        private void ToolImprimir_Click(object sender, EventArgs e)
        {
        }

        private void ToolManFun_Click(object sender, EventArgs e)
        {            
        }

        private void FgItems_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgItems.Col == 2)
            {
                int n_idtipexi = 3;
                DataTable dtResul = new DataTable();
                string c_dato = "";

                // OBTNEMOS LA DESCRIPCION DEL TIPO DE PRODUCTO
                string strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 1));
                if (strDesTipPro == "")
                {
                    n_idtipexi = 0;
                }

                // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
                dtResul = funDatos.DataTableFiltrar(dtTipoExis, "c_des = '" + strDesTipPro + "'");
                if (dtResul.Rows.Count != 0)
                {
                    n_idtipexi = Convert.ToInt32(dtResul.Rows[0]["n_id"].ToString());
                }

                booAgregando = true;
                dtResul = objItems.BuscarItem("", "n_id", dtItems, n_idtipexi);
                if (dtResul != null)
                {
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["c_despro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                        FgItems.SetData(FgItems.Row, 2, c_dato);

                        c_dato = dtResul.Rows[0]["n_id"].ToString();        // MOSTRAMOS LA PRESENTACION DEL ITEM
                        c_dato = funDatos.DataTableBuscar(dtPresentaItem, "n_idite", "c_abrpre", c_dato, "N").ToString();

                        FgItems.SetData(FgItems.Row, 3, c_dato);
                    }
                }
                booAgregando = false;
            }
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            if (FgItems.Col == 5)
            {
                FgItems.Select(FgItems.Row - 1, 2);
                return;
            }
        }

        private void CboResponsable_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }
            if (Convert.ToInt32(CboResponsable.SelectedValue) == 0) { return; }
            
            TxtNumSer.Text = "0001";
            TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 95, TxtNumSer.Text);
            CboAlmacenOrigen.Focus();
            booAgregando = false;
        }
    }
}
