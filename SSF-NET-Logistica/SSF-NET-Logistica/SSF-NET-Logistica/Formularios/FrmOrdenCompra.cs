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

namespace SSF_NET_Logistica.Formularios
{
    public partial class FrmOrdenCompra : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_log_ordencompra objRegistros = new CN_log_ordencompra();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_alm_inventariounimed ObjAlmUniMed = new CN_alm_inventariounimed();
        CN_sun_tipexi objTipoExi = new CN_sun_tipexi();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_mae_prioridad objprioridad = new CN_mae_prioridad();
        CN_mae_estados objestados = new CN_mae_estados();
        CN_sys_empresalocal objlocal = new CN_sys_empresalocal();
        CN_mae_area objarea = new CN_mae_area();
        CN_sys_personalmodulo objpermod = new CN_sys_personalmodulo();
        CN_mae_motivos objmotivos = new CN_mae_motivos();
        CN_log_personal objLogPer = new CN_log_personal();
        CN_sun_tipmon o_Moneda = new CN_sun_tipmon();
        CN_mae_clipro objPro = new CN_mae_clipro();
        CN_mae_condpago o_ConPag = new CN_mae_condpago();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_LOG_ORDENCOMPRA BE_Lista = new BE_LOG_ORDENCOMPRA();
        BE_LOG_ORDENCOMPRA BE_Registro = new BE_LOG_ORDENCOMPRA();
        //BE_LOG_ORDENREQUERIMIENTODET BE_Detalle = new BE_LOG_ORDENREQUERIMIENTODET();
        List<BE_LOG_ORDENCOMPRADET> LstDetalle = new List<BE_LOG_ORDENCOMPRADET>();   // CREAMOS UNA LISTA DEL TIPO BE_VTA_GUIASDET PARA ALMACENAR LOS ITEMS

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO

        DataTable dtForm = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtMoviDetalle = new DataTable();
        DataTable dtPresentaItem = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtTipoDocumento = new DataTable();
        DataTable dtSolicitante = new DataTable();
        DataTable dtArea = new DataTable();
        DataTable dtTipoExis = new DataTable();
        DataTable dtPrioridad = new DataTable();
        DataTable dtEstado = new DataTable();
        DataTable dtLocal = new DataTable();
        DataTable dtmotivo = new DataTable();
        DataTable dtLogPer = new DataTable();
        DataTable dtMoneda = new DataTable();
        DataTable dtPro = new DataTable();
        DataTable dtConPag = new DataTable();

        // VARIABLES LOCALES
        int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[13, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[7, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmOrdenCompra()
        {
            InitializeComponent();
        }

        private void FrmOrdenCompra_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboArea, dtArea, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboSolicitante, dtLogPer, "n_id", "c_apenom");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboConPag, dtConPag, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboLocal, dtLocal, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMotivo, dtmotivo, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMoneda,dtMoneda, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 595;
            this.Width = 952;
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            arrCabeceraFlex1[0, 0] = "Tipo Item";
            arrCabeceraFlex1[0, 1] = "140";
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Item";
            arrCabeceraFlex1[1, 1] = "370";
            arrCabeceraFlex1[1, 2] = "S";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Uni. Med.";
            arrCabeceraFlex1[2, 1] = "35";
            arrCabeceraFlex1[2, 2] = "S";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_itepredes";

            arrCabeceraFlex1[3, 0] = "Cantidad";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "0.00";
            arrCabeceraFlex1[3, 4] = "n_can";

            arrCabeceraFlex1[4, 0] = "P. Unit. Afecto";
            arrCabeceraFlex1[4, 1] = "75";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.000000";
            arrCabeceraFlex1[4, 4] = "n_can";

            arrCabeceraFlex1[5, 0] = "P. Unit. Inafecto";
            arrCabeceraFlex1[5, 1] = "75";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.000000";
            arrCabeceraFlex1[5, 4] = "n_can";

            arrCabeceraFlex1[6, 0] = "Total";
            arrCabeceraFlex1[6, 1] = "75";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_can";


            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
            dtLista = objRegistros.dtLista;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(78, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(78);

            objItems.mysConec = mysConec;
            dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);                                        // CARGAMOS TODOS LOS DEPARTAMENTOS

            objarea.mysConec = mysConec;
            dtArea = objarea.Listar(STU_SISTEMA.EMPRESAID);

            objprioridad.mysConec = mysConec;
            dtPrioridad = objprioridad.Listar(78);                          // CARGAMOS LAS PRIORIDADES DEL FORMULARIO

            objlocal.mysConec = mysConec;
            dtLocal = objlocal.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            objTipoExi.mysConec = mysConec;
            dtTipoExis = objTipoExi.Listar();

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            o_Moneda.mysConec = mysConec;
            dtMoneda = o_Moneda.Listar();
            dtMoneda = funDatos.DataTableFiltrar(dtMoneda, "n_id IN (115,151)");

            objLogPer.mysConec = mysConec;
            objLogPer.Listar(STU_SISTEMA.EMPRESAID);
            dtLogPer = objLogPer.dtPersonal;

            objmotivos.mysConec = mysConec;
            dtmotivo = objmotivos.Listar(STU_SISTEMA.EMPRESAID, 2);

            objestados.mysConec = mysConec;
            dtEstado = objestados.Listar(27);

            objTipDoc.mysConec = mysConec;
            dtTipoDocumento = objTipDoc.Listar();
            dtTipoDocumento = funDatos.DataTableFiltrar(dtTipoDocumento, "n_essal = 1 AND n_idtipo = 2");

            objPro.mysConec = mysConec;
            dtPro = objPro.ListarProveedor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            o_ConPag.mysConec = mysConec;
            dtConPag = o_ConPag.Listar();
        }
        void ListarItems()
        {
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
            booAgregando = true;

            objRegistros.mysConec = mysConec;
            LstDetalle.Clear();
            if (objRegistros.TraerRegistro(n_IdRegistro) == true)
            {
                BE_Registro = objRegistros.e_OC;

                TxtNumSer.Text = BE_Registro.c_numser;
                TxtNumDoc.Text = BE_Registro.c_numdoc;

                if (BE_Registro.d_fchemi.ToString() == "")
                {
                    TxtFchEmiDoc.CustomFormat = " ";
                    TxtFchEmiDoc.Format = DateTimePickerFormat.Custom;
                }
                else
                {
                    TxtFchEmiDoc.CustomFormat = "dd/MM/yyyy";
                    TxtFchEmiDoc.Format = DateTimePickerFormat.Custom;
                    TxtFchEmiDoc.Text = BE_Registro.d_fchemi.ToString();
                }

                LblIdPro.Text = BE_Registro.n_idpro.ToString();
                TxtNumRuc.Text = funDatos.DataTableBuscar(dtPro, "n_id", "c_numdoc", BE_Registro.n_idpro.ToString(), "N").ToString();
                TxtProv.Text = funDatos.DataTableBuscar(dtPro, "n_id", "c_nombre", BE_Registro.n_idpro.ToString(), "N").ToString();

                CboLocal.SelectedValue = BE_Registro.n_idloc;
                CboSolicitante.SelectedValue = BE_Registro.n_idpersol;
                CboArea.SelectedValue = BE_Registro.n_idare;
                CboConPag.SelectedValue = BE_Registro.n_idconpag;
                CboMoneda.SelectedValue = BE_Registro.n_idmon;
                if (BE_Registro.d_fchent.ToString() == "")
                {
                    TxtFchEnt.CustomFormat = " ";
                    TxtFchEnt.Format = DateTimePickerFormat.Custom;
                }
                else
                {
                    TxtFchEnt.CustomFormat = "dd/MM/yyyy";
                    TxtFchEnt.Format = DateTimePickerFormat.Custom;
                    TxtFchEnt.Text = BE_Registro.d_fchent.ToString();
                }
                CboMotivo.SelectedValue = BE_Registro.n_idmot;
                TxtObs.Text = BE_Registro.c_obs.ToString();

                LblIdEstado.Text = BE_Registro.n_idest.ToString();
                LblEstado.Text = funDatos.DataTableBuscar(dtEstado, "n_id", "c_des", BE_Registro.n_idest.ToString(), "N").ToString();

                TxtImpBru.Text = BE_Registro.n_impbru.ToString("0.00");
                TxtInafec.Text = BE_Registro.n_impina.ToString("0.00");
                TxtIgv.Text = BE_Registro.n_impigv.ToString("0.00");
                TxtTot.Text = BE_Registro.n_imptot.ToString("0.00");

                //Funciones funFunciones = new Funciones();
                //Genericas funDatos = new Genericas();

                LstDetalle = objRegistros.l_OCDet;

                // MOSTRAMOS EL DETALLE DEL REQUERIMIENTO
                int n_fila = 2;
                int n_idIte;
                int n_idUniMed;
                int n_idTipExi;
                string strCadenaFiltro;
                DataTable DtFiltro = new DataTable();

                FgItems.Rows.Count = 2;

                foreach (BE_LOG_ORDENCOMPRADET element in LstDetalle)
                {
                    FgItems.Rows.Count = FgItems.Rows.Count + 1;

                    n_idIte = element.n_idite;
                    n_idUniMed = element.n_idunimed;

                    // MOSTRAMOS EL ITEM
                    strCadenaFiltro = "n_id = " + n_idIte + "";
                    DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                    FgItems.SetData(n_fila, 2, DtFiltro.Rows[0]["c_despro"].ToString());

                    // MOSTRAMOS EL TPO DE EXIXTENCIA
                    n_idTipExi = Convert.ToInt16(DtFiltro.Rows[0]["n_idtipexi"].ToString());
                    strCadenaFiltro = "n_id = " + n_idTipExi + "";
                    DtFiltro = funDatos.DataTableFiltrar(dtTipoExis, strCadenaFiltro);
                    FgItems.SetData(n_fila, 1, DtFiltro.Rows[0]["c_des"].ToString());

                    // MOSTRAMOS LA UNIDAD DE MEDIDA
                    strCadenaFiltro = "n_idite = " + n_idIte + "";
                    DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                    FgItems.SetData(n_fila, 3, DtFiltro.Rows[0]["c_abrpre"].ToString());

                    FgItems.SetData(n_fila, 4, element.n_can.ToString("0.00"));

                    if (element.n_idtipafeigv == 1)
                    {
                        FgItems.SetData(n_fila, 5, element.n_preuni.ToString("0.000000"));
                    }
                    else
                    {
                        FgItems.SetData(n_fila, 6, element.n_preuni.ToString("0.000000"));
                    }
                    FgItems.SetData(n_fila, 7, element.n_imptot.ToString("0.00"));

                    n_fila = n_fila + 1;
                }
            }
            booAgregando = false;
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

            // MOSTRAMOS EL ULTIMO NUMERO DE REQUERIMIENTO
            string c_numdoc = "";
            c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 84, "0001");
            TxtNumSer.Text = "0001";
            TxtNumDoc.Text = c_numdoc;

            TxtFchEmiDoc.Focus();
        }
        void Blanquea()
        {
            LblEstado.Text = "";
            LblIdPro.Text = "";
            TxtNumRuc.Text = "";
            TxtProv.Text = "";

            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtFchEmiDoc.Text = "";
            TxtFchEmiDoc.CustomFormat = "";
            TxtFchEmiDoc.Format = DateTimePickerFormat.Custom;

            CboLocal.SelectedValue = 0;
            CboSolicitante.SelectedValue = 0;
            CboArea.SelectedValue = 0;
            CboConPag.SelectedValue = 0;
            TxtFchEnt.Text = "";
            TxtFchEnt.CustomFormat = "dd/MM/yyyy";
            TxtFchEnt.Format = DateTimePickerFormat.Custom;
            CboMotivo.SelectedValue = 0;
            TxtObs.Text = "";
            LblIdEstado.Text = "";
            FgItems.Rows.Count = 2;
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;

            TxtTot.Text = "";
            TxtInafec.Text = "";
            TxtIgv.Text = "";
            TxtImpBru.Text = "";
        }
        void Bloquea()
        {
            //TxtNumSer.Enabled = !TxtNumSer.Enabled;
            //TxtNumDoc.Enabled = !TxtNumDoc.Enabled;

            TxtNumRuc.Enabled = !TxtNumRuc.Enabled;
            TxtFchEmiDoc.Enabled = !TxtFchEmiDoc.Enabled;
            CboSolicitante.Enabled = !CboSolicitante.Enabled;
            CboLocal.Enabled = !CboLocal.Enabled;
            CboArea.Enabled = !CboArea.Enabled;
            CboConPag.Enabled = !CboConPag.Enabled;
            TxtFchEnt.Enabled = !TxtFchEnt.Enabled;
            CboMotivo.Enabled = !CboMotivo.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
            CboMoneda.Enabled = !CboMoneda.Enabled;

            CmdBusPro.Enabled = !CmdBusPro.Enabled;
            CmdAddItem.Enabled = !CmdAddItem.Enabled;
            CmdDelItem.Enabled = !CmdDelItem.Enabled;
            CmdAddNewItem.Enabled = !CmdAddNewItem.Enabled;
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
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            TxtFchEmiDoc.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
                    dtLista = objRegistros.dtLista;
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
                booResultado = objRegistros.Insertar(BE_Registro, LstDetalle);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(BE_Registro, LstDetalle);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.n_ErrorNumber.ToString() + " = " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            if (n_QueHace == 1)
            {
                BE_Registro.n_id = 0;
            }
            else
            {
                BE_Registro.n_id = BE_Registro.n_id;
            }

            BE_Registro.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_Registro.n_idtipdoc = 84;
            BE_Registro.c_numser = TxtNumSer.Text;
            BE_Registro.c_numdoc = TxtNumDoc.Text;
            BE_Registro.n_anotra = STU_SISTEMA.ANOTRABAJO;
            BE_Registro.n_mestra = Convert.ToInt16(CboMeses.SelectedValue);
            BE_Registro.d_fchemi= Convert.ToDateTime(TxtFchEmiDoc.Text);
            BE_Registro.d_fchent = Convert.ToDateTime(TxtFchEnt.Text);
            BE_Registro.n_idloc = Convert.ToInt16(CboLocal.SelectedValue);
            BE_Registro.n_idare = Convert.ToInt16(CboArea.SelectedValue);
            BE_Registro.n_idpersol = Convert.ToInt16(CboSolicitante.SelectedValue);
            BE_Registro.n_idpri = 1;
            BE_Registro.c_obs = TxtObs.Text;
            BE_Registro.n_idest = 1;
            BE_Registro.n_idmot = Convert.ToInt16(CboMotivo.SelectedValue);
            BE_Registro.n_idpro = Convert.ToInt16(LblIdPro.Text);
            BE_Registro.n_idmon = Convert.ToInt16(CboMoneda.SelectedValue);
            BE_Registro.n_impbru = Convert.ToDouble(funFunciones.NulosN(TxtImpBru.Text));
            BE_Registro.n_impigv = Convert.ToDouble(funFunciones.NulosN(TxtIgv.Text));
            BE_Registro.n_imptot = Convert.ToDouble(funFunciones.NulosN(TxtTot.Text));
            BE_Registro.n_idconpag = Convert.ToInt16(CboConPag.SelectedValue);
            BE_Registro.n_impina = Convert.ToDouble(funFunciones.NulosN(TxtInafec.Text));

            int n_fila = 0;
            DataTable DtFiltro = new DataTable();
            string strCadenaFiltro = "";
            string c_nomitem = "";
            string c_presendes = "";
            List<BE_LOG_ORDENCOMPRADET> LstDetalleTmp = new List<BE_LOG_ORDENCOMPRADET>();

            if (FgItems.Rows.Count > 2)
            {
                for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
                {
                    if (funFunciones.NulosC(FgItems.GetData(n_fila, 1)) != "")
                    {
                        BE_LOG_ORDENCOMPRADET BE_Detalle = new BE_LOG_ORDENCOMPRADET();

                        c_nomitem = FgItems.GetData(n_fila, 2).ToString();
                        c_presendes = FgItems.GetData(n_fila, 3).ToString();

                        BE_Detalle.n_can = Convert.ToDouble(FgItems.GetData(n_fila, 4).ToString());

                        // FILTRAMOS EL ITEM DE LA FILA PARA OBTENER EL ID  
                        strCadenaFiltro = "c_despro = '" + c_nomitem + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                        BE_Detalle.n_idite = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        // FILTRAMOS LA PRESENTACION PARA OBTENER SU ID
                        strCadenaFiltro = "c_abrpre = '" + c_presendes + "' AND n_idite = " + BE_Detalle.n_idite + "";
                        DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                        BE_Detalle.n_idunimed = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        if (Convert.ToDouble(FgItems.GetData(n_fila, 5).ToString())!=0)
                        {
                            BE_Detalle.n_preuni = Convert.ToDouble(FgItems.GetData(n_fila, 5).ToString());
                            BE_Detalle.n_idtipafeigv = 1;
                        }

                        if (Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_fila, 6))) != 0)
                        {
                            BE_Detalle.n_preuni = Convert.ToDouble(FgItems.GetData(n_fila, 6).ToString());
                            BE_Detalle.n_idtipafeigv = 9;
                        }
                        BE_Detalle.n_imptot = Convert.ToDouble(FgItems.GetData(n_fila, 7).ToString());

                       

                        LstDetalleTmp.Add(BE_Detalle);
                    }
                }
                LstDetalle = LstDetalleTmp;
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

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
            if (Convert.ToInt16(CboLocal.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el local que genera el requerimiento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboLocal.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboSolicitante.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el solicitante !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboSolicitante.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboArea.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el area !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboArea.Focus();
                return booEstado;
            }
            if (TxtFchEnt.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de entrega !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchEnt.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboConPag.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la condicion de pago !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboConPag.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboMotivo.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el motivo del requerimiento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMotivo.Focus();
                return booEstado;
            }
            if (LblIdPro.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el proveedor para la orden de compra !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumRuc.Focus();
                return booEstado;
            }
            if (Convert.ToDouble(funFunciones.NulosN(TxtTot.Text)) == 0)
            {
                MessageBox.Show("¡ La orden de compra debe de tener un item como minimo !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumRuc.Focus();
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
                objRegistros.mysConec = mysConec;
                objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
                dtLista = objRegistros.dtLista;
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

        private void emitirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());
            CN_log_ordencompra objLog = new CN_log_ordencompra();
            objLog.STU_SISTEMA = STU_SISTEMA;
            objLog.ReportImprimirOrdCompra(intIdRegistro);
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            objTipoExi = null;
            dtTipoExis = null;

            objTipDoc = null;
            dtTipoDocumento = null;

            objTipoExi = null;
            dtTipoExis = null;

            objFormVis = null;
            objFormVis = null;

            this.Close();
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
                    n_idpro = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());

                    // FILTRAMOS LAS PRESENTACIONES DEL ITEM SELECCIONADO
                    dtResul = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_idpro + " AND n_default = 1");
                    funFlex.FlexColumnaCombo(FgItems, dtResul, "c_abrpre", 3);    // ITEMS
                    if (dtResul.Rows.Count == 1)
                    {
                        FgItems.SetData(FgItems.Row, 3, dtResul.Rows[0]["c_abrpre"].ToString());

                        //CargarLotes(n_idpro);
                    }
                    else
                    {
                        FgItems.SetData(FgItems.Row, 3, "");
                    }
                }
                //FgItems.SetData(FgItems.Row, 7, DateTime.Now.ToString("HH:mm"));

                FgItems.Select(FgItems.Row - 1, 3);
                booAgregando = false;
                return;
            }
            if (FgItems.Col == 3)
            {
                FgItems.Select(FgItems.Row - 1, 4);
                return;
            }
            //if (FgItems.Col == 4)
            //{
            //    FgItems.Select(FgItems.Row, 1);
            //    SumarTotales();
            //    return;
            //}
            if ((FgItems.Col == 5) || (FgItems.Col == 4))
            {
                FgItems.SetData(FgItems.Row, 6, "0.00");
                double n_can = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row,4)));
                double n_preuni = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row, 5)));
                double n_imptot = (n_can * n_preuni);
                SumarTotales();
                FgItems.SetData(FgItems.Row, 7, n_imptot.ToString("0.00"));
                FgItems.Select(FgItems.Row, 1);
                return;
            }
            if (FgItems.Col == 6)
            {
                FgItems.SetData(FgItems.Row, 5, "0.00");
                double n_can = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row, 4)));
                double n_preuni = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row, 6)));
                double n_imptot = (n_can * n_preuni);
                SumarTotales();
                FgItems.SetData(FgItems.Row, 7, n_imptot.ToString("0.00"));
                FgItems.Select(FgItems.Row, 1);
                return;
            }
        }
        void SumarTotales()
        {
            int n_row = 0;
            double n_impafe = 0;
            double n_impina = 0;
            double n_impigv = 0;
            double n_imptot = 0;

            double n_valor = 0;
            double n_cant = 0;

            for (n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
            {
                n_cant = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 4)));

                n_valor = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 5)));
                n_impafe = n_impafe + (n_valor * n_cant);

                n_valor = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 6)));
                n_impina = n_impina + (n_valor * n_cant);

                if (Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 5))) != 0)
                { 
                    n_impigv =  ((n_impafe * 1.18) - n_impafe);
                }

                n_imptot = (n_impigv + n_impafe + n_impina);
            }

            TxtImpBru.Text =n_impafe.ToString("0.00");
            TxtInafec.Text = n_impina.ToString("0.00");
            TxtIgv.Text = n_impigv.ToString("0.00");
            TxtTot.Text = n_imptot.ToString("0.00");
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
                // OBTNEMOS LA DESCRIPCION DEL TIPO DE PRODUCTO
                strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 1));

                if (strDesTipPro == "")
                {
                    FgItems.AllowEditing = false;
                    funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 2);
                    return;
                }
                // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
                dtResul = funDatos.DataTableFiltrar(dtTipoExis, "c_des = '" + strDesTipPro + "'");
                if (dtResul.Rows.Count != 0)
                {
                    n_idtipproducto = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());

                    // FILTRAMOS LOS ITEMS DEL TIPO DE PRODUCTO SELECCIONADO
                    dtResul = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + n_idtipproducto + "", "c_despro");
                    funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 2);    // ITEMS
                }
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
                    n_idtipproducto = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());

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
                // OBTENEMOS LA DESCRIPCIO DEL ITEM
                strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 3));

                if (strDesTipPro == "")
                {
                    FgItems.AllowEditing = false;
                    return;
                }
            }
            if (FgItems.Col == 6)
            {
                // OBTENEMOS LA DESCRIPCIO DEL ITEM
                strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 3));

                if (strDesTipPro == "")
                {
                    FgItems.AllowEditing = false;
                    return;
                }
            }
            if (FgItems.Col == 7)
            {
                FgItems.AllowEditing = false;
                return;
            }

            FgItems.AllowEditing = true;
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
            else
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
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

            if (booAgregando == true) { return; }
        }

        private void FrmOrdenCompra_Activated(object sender, EventArgs e)
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
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());
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
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }

        private void CmdAddItem_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            FgItems.Rows.Count = FgItems.Rows.Count + 1;
        }

        private void CmdDelItem_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            FgItems.RemoveItem(FgItems.Row);
        }

        private void CmdAddNewItem_Click(object sender, EventArgs e)
        {
            DataTable dtresult = new DataTable();
            string c_tipoexistencia = "";
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = mysConec;
            objForm.STU_SISTEMA = STU_SISTEMA;
            objForm.n_DeDonde = 2;                        // INDICAMOS QUE EL FORMULARIO ES LLAMADO DESDE OTRO FORMULARIO
            objForm.n_TipoExistencia = 7;
            objForm.MantenimientoItems();

            Int64 n_idnewitem = objForm.n_IdItemCreado;

            // CARGAMOS NUEVMENTE LOS ITEMS 
            objItems.mysConec = mysConec;
            dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);

            // CARGAMOS NUEVAMENTE LA UNIDAD DE MEDIDA
            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            dtresult = funDatos.DataTableFiltrar(dtItems, "n_id = " + n_idnewitem + " ");

            if (dtresult.Rows.Count != 0)
            {
                booAgregando = true;
                FgItems.SetData(FgItems.Row, 1, c_tipoexistencia);
                FgItems.SetData(FgItems.Row, 2, dtresult.Rows[0]["c_despro"].ToString());

                // FILTRAMOS LAS PRESENTACIONES DEL ITEM SELECCIONADO
                dtresult = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_idnewitem + "");

                funFlex.FlexColumnaCombo(FgItems, dtresult, "c_abrpre", 3);    // ITEMS
                FgItems.SetData(FgItems.Row, 3, dtresult.Rows[0]["c_abrpre"].ToString());
                booAgregando = false;
            }
        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO);
            dtLista = objRegistros.dtLista;
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

        private void TxtNumRuc_KeyPress(object sender, KeyPressEventArgs e)
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
                //TxtNumRuc.Focus();
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
                    TxtNumRuc.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                    TxtProv.Text = dtResult.Rows[0]["c_nombre"].ToString();
                }
                else
                {
                    LblIdPro.Text = "";
                    TxtProv.Text = "";
                    TxtNumRuc.Text = "";
                }
            }
        }
    }
}
