using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Ventas;
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

namespace SSF_NET_Ventas.Formularios
{
    public partial class FrmManPedidos : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_vta_pedidocli objRegistros = new CN_vta_pedidocli();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_alm_inventariounimed ObjAlmUniMed = new CN_alm_inventariounimed();
        CN_sun_tipexi objTipoExi = new CN_sun_tipexi();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        //CN_mae_prioridad objprioridad = new CN_mae_prioridad();
        CN_mae_estados objestados = new CN_mae_estados();
        CN_mae_condpago objCondPag = new CN_mae_condpago();
        CN_mae_clipro objCli = new CN_mae_clipro();
        CN_vta_punvencli objPunVen = new CN_vta_punvencli();
        CN_vta_vendedor objVendedor = new CN_vta_vendedor();
        CN_sun_tipmon objMoneda = new CN_sun_tipmon();
        CN_mae_cliproitems o_itecli = new CN_mae_cliproitems();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        //BE_LOG_ORDENREQUERIMIENTO BE_Lista = new BE_LOG_ORDENREQUERIMIENTO();
        BE_VTA_PEDIDOCLI BE_Registro = new BE_VTA_PEDIDOCLI();
        List<BE_VTA_PEDIDOCLIDET> LstDetalle = new List<BE_VTA_PEDIDOCLIDET>();   // CREAMOS UNA LISTA DEL TIPO BE_VTA_GUIASDET PARA ALMACENAR LOS ITEMS

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO

        DataTable dtForm = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtMoviDetalle = new DataTable();
        DataTable dtPresentaItem = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtTipoDocumento = new DataTable();
        DataTable dtPunVen = new DataTable();
        DataTable dtCliente = new DataTable();
        DataTable dtTipoExis = new DataTable();
        //DataTable dtPrioridad = new DataTable();
        DataTable dtEstado = new DataTable();
        DataTable dtConVen = new DataTable();
        DataTable dtVen = new DataTable();
        DataTable dtMoneda = new DataTable();
        DataTable dtitecli = new DataTable();

        // VARIABLES LOCALES
        int n_idformulario = 38;
        int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[13, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[7, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^-" + (char)8;
        public FrmManPedidos()
        {
            InitializeComponent();
        }
        private void FrmManPedidos_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            //funDatos.ComboBoxCargarDataTable(CboCli, dtCliente, "n_id", "c_nombre");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDocPed, dtTipoDocumento, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboConPag, dtConVen, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboVen, dtVen, "n_id", "c_apenom");
            funDatos.ComboBoxCargarDataTable(CboMoneda, dtMoneda, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 634;
            this.Width = 991;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            arrCabeceraFlex1[0, 0] = "Codigo";
            arrCabeceraFlex1[0, 1] = "100";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Mercaderia / Producto / Servicio";
            arrCabeceraFlex1[1, 1] = "340";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Uni. Med.";
            arrCabeceraFlex1[2, 1] = "50";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_itepredes";

            arrCabeceraFlex1[3, 0] = "Cantidad";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "0.000000";
            arrCabeceraFlex1[3, 4] = "n_can";

            arrCabeceraFlex1[4, 0] = "Precio Unitario";
            arrCabeceraFlex1[4, 1] = "70";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.000000";
            arrCabeceraFlex1[4, 4] = "n_prebru";

            arrCabeceraFlex1[5, 0] = "Precio Bruto";
            arrCabeceraFlex1[5, 1] = "70";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.000000";
            arrCabeceraFlex1[5, 4] = "n_prebru";

            arrCabeceraFlex1[6, 0] = "Fecha Entrega";
            arrCabeceraFlex1[6, 1] = "80";
            arrCabeceraFlex1[6, 2] = "F";
            arrCabeceraFlex1[6, 3] = "dd/MM/yyyy";
            arrCabeceraFlex1[6, 4] = "n_prebru";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
            CboMoneda.SelectedValue = STU_SISTEMA.MONEDA;
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(n_idformulario, ref arrCabeceraDg1);
            
            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(38);

            objMoneda.mysConec = mysConec;
            dtMoneda = objMoneda.Listar();                              // CARGAMOS TODAS MONEDAS

            CargarDatosAdicionales();
        }
        void CargarDatosAdicionales()
        {
            objItems.mysConec = mysConec;
            dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);                                        // CARGAMOS TODOS LOS DEPARTAMENTOS
            dtItems = funDatos.DataTableFiltrar(dtItems, "n_idtipexi IN (1, 2, 23)");
            
            objCli.mysConec = mysConec;
            dtCliente = objCli.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objCondPag.mysConec = mysConec;
            dtConVen = objCondPag.Listar();

            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            objTipoExi.mysConec = mysConec;
            dtTipoExis = objTipoExi.Listar();

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objPunVen.mysConec = mysConec;
            dtPunVen = objPunVen.Listar();

            objVendedor.mysConec = mysConec;
            dtVen = objVendedor.Listar(STU_SISTEMA.EMPRESAID);

            objestados.mysConec = mysConec;
            dtEstado = objestados.Listar(27);

            objTipDoc.mysConec = mysConec;
            dtTipoDocumento = objTipDoc.Listar();
            dtTipoDocumento = funDatos.DataTableFiltrar(dtTipoDocumento, "n_id = 77");
        }
        void ListarItems()
        {
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
            booAgregando = true;

            objRegistros.mysConec = mysConec;
            LstDetalle.Clear();
            if (objRegistros.TraerRegistro(n_IdRegistro) == true)
            {
                BE_Registro = objRegistros.entPedCab;

                TxtNumSer.Text = BE_Registro.c_numser;
                TxtNumDoc.Text = BE_Registro.c_numdoc;
                //CboCli.SelectedValue = BE_Registro.n_idcli;
                LblIdCliente.Text = BE_Registro.n_idcli.ToString();
                TxtCliente.Text = funDatos.DataTableBuscar(dtCliente, "n_id", "c_nombre", BE_Registro.n_idcli.ToString(), "N").ToString();

                DataTable dtResult = new DataTable();
                dtResult = funDatos.DataTableFiltrar(dtPunVen, "n_idcli = " + Convert.ToInt32(LblIdCliente.Text) + "");
                if (dtResult.Rows.Count != 0)
                {
                    //CboPunVen.Enabled = true;
                    funDatos.ComboBoxCargarDataTable(CboPunVen, dtResult, "n_id", "c_des");
                    CboPunVen.SelectedValue = BE_Registro.n_idpunven;
                }
                
                CboTipDocPed.SelectedValue = BE_Registro.n_pedidtipdoc;
                TxtRefNumSer.Text = BE_Registro.c_pednumser;
                TxtRefNumDoc.Text = BE_Registro.c_pednumdoc;
                TxtFchPed.Text = BE_Registro.d_fchped.ToString();
                TxtFchReg.Text = BE_Registro.d_fchreg.ToString();
                CboConPag.SelectedValue = BE_Registro.n_idconpag;
                CboVen.SelectedValue = BE_Registro.n_idven;
                TxtObs.Text = BE_Registro.c_obs;
                TxtImpBru.Text = BE_Registro.n_impbru.ToString("0.00");
                TxtImpIgv.Text = BE_Registro.n_impigv.ToString("0.00");
                TxtImpTot.Text = BE_Registro.n_imptot.ToString("0.00");

                LblIdEstado.Text = BE_Registro.n_idestent.ToString();
                LblEstado.Text = funDatos.DataTableBuscar(dtEstado, "n_id", "c_des", BE_Registro.n_idestent.ToString(), "N").ToString();
                CboMoneda.SelectedValue = BE_Registro.n_idmon;
                LstDetalle = objRegistros.lstPedDet;

                TraePrecioCliente(Convert.ToInt32(LblIdCliente.Text));
                // MOSTRAMOS EL DETALLE DEL REQUERIMIENTO
                int n_fila = 2;
                int n_idIte;
                int n_idUniMed;
                //int n_idTipExi;
                double n_valor = 0;
                string strCadenaFiltro;
                DataTable DtFiltro = new DataTable();

                FgItems.Rows.Count = 2;

                foreach (BE_VTA_PEDIDOCLIDET element in LstDetalle)
                {
                    FgItems.Rows.Count = FgItems.Rows.Count + 1;

                    n_idIte = element.n_idite;
                    n_idUniMed = element.n_idunimed;

                    // MOSTRAMOS EL ITEM
                    strCadenaFiltro = "n_id = " + n_idIte + "";
                    DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                    FgItems.SetData(n_fila, 1, DtFiltro.Rows[0]["c_codpro"].ToString());
                    FgItems.SetData(n_fila, 2, DtFiltro.Rows[0]["c_despro"].ToString());

                    
                    // MOSTRAMOS LA UNIDAD DE MEDIDA
                    strCadenaFiltro = "n_idite = " + n_idIte + "";
                    DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                    FgItems.SetData(n_fila, 3, DtFiltro.Rows[0]["c_abrpre"].ToString());

                    FgItems.SetData(n_fila, 4, element.n_can);

                    FgItems.SetData(n_fila, 5, element.n_impbru);
                    
                    n_valor = element.n_impbru * element.n_can;
                    FgItems.SetData(n_fila, 6, n_valor);
                    FgItems.SetData(n_fila, 7, element.d_fchent.ToString("dd/MM/yyyy"));
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
            c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 77, "0001");
            TxtNumSer.Text = "0001";
            TxtNumDoc.Text = c_numdoc;
            TxtRefNumDoc.Text = c_numdoc;
            CboTipDocPed.SelectedValue = 77;
            CboMoneda.SelectedValue = 115;
            //FgItems.Cols[2].ComboList = "...";
            booAgregando = false;
            TxtFchPed.Text = DateTime.Now.ToString("dd/MM/yyyy");
            TxtFchPed.Focus();
        }
        void Blanquea()
        {
            LblIdCliente.Text = "";
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtCliente.Text = "";
            
            CboTipDocPed.SelectedValue = 0;           
            TxtRefNumSer.Text = "";
            TxtRefNumDoc.Text = "";

            TxtFchPed.Text = "";
            TxtFchPed.CustomFormat = " ";
            TxtFchPed.Format = DateTimePickerFormat.Custom;

            CboConPag.SelectedValue = 0;
            CboVen.SelectedValue = 0;
            TxtObs.Text = "";
            TxtImpBru.Text = "";
            TxtImpIgv.Text = "";
            TxtImpTot.Text = "";

            LblIdEstado.Text = "";
            LblEstado.Text = "";
            FgItems.Rows.Count = 2;
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;
        }
        void Bloquea()
        {
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            CmdBusCli.Enabled = !CmdBusCli.Enabled;
            CboPunVen.Enabled = !CboPunVen.Enabled;
            CboTipDocPed.Enabled = !CboTipDocPed.Enabled;
            TxtRefNumSer.Enabled = !TxtRefNumSer.Enabled;
            TxtRefNumDoc.Enabled = !TxtRefNumDoc.Enabled;
            TxtFchPed.Enabled = !TxtFchPed.Enabled;
            TxtFchReg.Enabled = !TxtFchReg.Enabled;
            CboConPag.Enabled = !CboConPag.Enabled;
            CboVen.Enabled = !CboVen.Enabled;
            CboMoneda.Enabled = !CboMoneda.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;

            CmdMosEnt.Enabled = !CmdMosEnt.Enabled;
            CmdAddItem.Enabled = !CmdAddItem.Enabled;
            CmdDelItem.Enabled = !CmdDelItem.Enabled;
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
            CmdRefDat.Enabled = !CmdRefDat.Enabled;
            ToolExportar.Enabled = !ToolExportar.Enabled;
            ToolProduccion.Enabled = !ToolProduccion.Enabled;
        }
        void Modificar()
        {
            int n_envpro = Convert.ToInt32(DgLista.Columns[3].CellValue(DgLista.Row).ToString());
            int n_entregado = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            if (n_envpro == 1)
            {
                MessageBox.Show("¡ No se puede modificar un pedido que ha sido enviado a produccion ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (n_entregado == 1)
            {
                MessageBox.Show("¡ No se puede modificar un pedido que ha sido entregado o despachado parcialmente ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            //FgItems.Cols[2].ComboList = "...";
            TxtFchPed.Focus();
            TraePrecioCliente(Convert.ToInt32(LblIdCliente.Text));
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int n_envpro = Convert.ToInt32(DgLista.Columns[3].CellValue(DgLista.Row).ToString());
            int n_entregado = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            if (n_envpro == 1)
            {
                MessageBox.Show("¡ No se puede eliminar un pedido que ha sido enviado a produccion ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }
            if (n_entregado == 1)
            {
                MessageBox.Show("¡ No se puede eliminar un pedido que ha sido entregado o despachado parcialmente ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {

            BE_Registro.n_idemp = STU_SISTEMA.EMPRESAID;

            if (n_QueHace == 1)
            {

                string c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 77, TxtNumSer.Text);
                //TxtNumSer.Text = "0001";
                TxtNumDoc.Text = c_numdoc;
                //TxtRefNumDoc.Text = c_numdoc;

                BE_Registro.n_id = 0;
            }
            else
            {
                BE_Registro.n_id = BE_Registro.n_id;
            }

            BE_Registro.n_idemp= STU_SISTEMA.EMPRESAID;
            //BE_Registro.n_id = 0;
            BE_Registro.n_anotra = STU_SISTEMA.ANOTRABAJO;
            BE_Registro.n_mestra = STU_SISTEMA.MESTRABAJO;
            BE_Registro.n_idcli = Convert.ToInt32(LblIdCliente.Text);
            BE_Registro.n_idpunven = Convert.ToInt32(CboPunVen.SelectedValue);
            BE_Registro.n_idtipodoc = 77;
            BE_Registro.c_numser = TxtNumSer.Text;
            BE_Registro.c_numdoc = TxtNumDoc.Text;
            BE_Registro.d_fchreg = Convert.ToDateTime(TxtFchReg.Text);
            BE_Registro.d_fchped = Convert.ToDateTime(TxtFchPed.Text);
            BE_Registro.d_fchent = Convert.ToDateTime(TxtFchPed.Text);
            BE_Registro.n_idconpag = Convert.ToInt32(CboConPag.SelectedValue);
            BE_Registro.n_idven = Convert.ToInt32(CboVen.SelectedValue);
            BE_Registro.c_obs = TxtObs.Text;
            BE_Registro.n_pedidtipdoc = Convert.ToInt32(CboTipDocPed.SelectedValue);
            BE_Registro.c_pednumser = ""; //TxtRefNumSer.Text;
            BE_Registro.c_pednumdoc = TxtRefNumDoc.Text;
            BE_Registro.n_impbru = Convert.ToDouble(TxtImpBru.Text);
            BE_Registro.n_impigv = Convert.ToDouble(TxtImpIgv.Text);
            BE_Registro.n_imptot = Convert.ToDouble(TxtImpTot.Text);
            BE_Registro.n_numite = FgItems.Rows.Count-2;
            //BE_Registro.n_mulent = 0;
            BE_Registro.n_idestent = 1;                             // NO ENTREGADO
            BE_Registro.n_idest = 1;                             // PENDIENTE DE PRODUCIR
            BE_Registro.n_idmon = Convert.ToInt32(CboMoneda.SelectedValue);

            int n_fila = 0;
            DataTable DtFiltro = new DataTable();
            string strCadenaFiltro = "";
            string c_nomitem = "";
            string c_presendes = "";
            List<BE_VTA_PEDIDOCLIDET> LstDetalleTmp = new List<BE_VTA_PEDIDOCLIDET>();

            if (FgItems.Rows.Count > 2)
            {
                for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
                {
                    if (funFunciones.NulosC(FgItems.GetData(n_fila, 2)) != "")
                    {
                        BE_VTA_PEDIDOCLIDET BE_Detalle = new BE_VTA_PEDIDOCLIDET();

                        c_nomitem = FgItems.GetData(n_fila, 2).ToString();
                        c_presendes = FgItems.GetData(n_fila, 3).ToString();

                        BE_Detalle.n_idped = 0;

                        // FILTRAMOS EL ITEM DE LA FILA PARA OBTENER EL ID  
                        strCadenaFiltro = "c_despro = '" + c_nomitem + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                        BE_Detalle.n_idite = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        // FILTRAMOS LA PRESENTACION PARA OBTENER SU ID
                        strCadenaFiltro = "c_abrpre = '" + c_presendes + "' AND n_idite = " + BE_Detalle.n_idite + "";
                        DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                        BE_Detalle.n_idunimed = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        BE_Detalle.n_can = Convert.ToDouble(FgItems.GetData(n_fila, 4).ToString());
                        BE_Detalle.n_impbru = Convert.ToDouble(FgItems.GetData(n_fila, 5).ToString());
                        BE_Detalle.n_impigv = 0;
                        BE_Detalle.n_imptot = 0;

                        if (funFunciones.NulosC(FgItems.GetData(n_fila, 7)) != "")
                        { 
                            BE_Detalle.d_fchent = Convert.ToDateTime(FgItems.GetData(n_fila, 7).ToString());;
                        }
                        BE_Detalle.n_entregado = 2;
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
            if (Convert.ToInt32(funFunciones.NulosN(LblIdCliente.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado el cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CmdBusCli.Focus();
                return booEstado;
            }
            //if (Convert.ToInt32(CboPunVen.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado el punto nombre del venta del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    CboPunVen.Focus();
            //    return booEstado;
            //}
            if (Convert.ToInt32(CboTipDocPed.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTipDocPed.Focus();
                return booEstado;
            }
            //if (TxtRefNumSer.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado el numero de serie del documento del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    TxtRefNumSer.Focus();
            //    return booEstado;
            //}
            if (TxtRefNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento del pedido del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtRefNumDoc.Focus();
                return booEstado;
            }
            if (TxtFchPed.Text == " ")
            {
                MessageBox.Show("¡ No ha especificado la fecha de pedido del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchPed.Focus();
                return booEstado;
            }
            //if (TxtFchReg.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado la fecha de entrega !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    TxtFchReg.Focus();
            //    return booEstado;
            //}
            if (Convert.ToInt32(CboConPag.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la condicion de pago del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboConPag.Focus();
                return booEstado;
            }
            if (Convert.ToInt32(CboVen.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre del vendedor !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboVen.Focus();
                return booEstado;
            }

            int n_fila = 0;
            if (FgItems.Rows.Count > 2)
            {
                for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
                {
                    if (funFunciones.NulosC(FgItems.GetData(n_fila, 2)) == "")
                    {
                        FgItems.RemoveItem(n_fila);
                        n_fila = n_fila - 1;
                    }
                }
            }

            if (FgItems.Rows.Count == 2)
            {
                MessageBox.Show("¡ El pedido no tiene ningun item, debe de ingresar al menos un item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtImpTot.Focus();
                return booEstado;
            }

            if (Convert.ToDouble(funFunciones.NulosN(TxtImpTot.Text)) == 0)
            {
                MessageBox.Show("¡ El pedido no ha sido valorizado, ingreso al menos un item y su respectivo precio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtImpTot.Focus();
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
                dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();

                DialogResult Rpta = MessageBox.Show("! El registro se agrego con exito ¡ ¿Desea agregar otro registro? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (DialogResult.Yes == Rpta)
                {
                    ActivarTool();
                    Bloquea();
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
            dtTipoDocumento = null;

            objTipoExi = null;
            dtTipoExis = null;

            objFormVis = null;
            objFormVis = null;

            this.Close();
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
        private void TxtRefNumDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000000000" + TxtRefNumDoc.Text;

                TxtRefNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
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
        private void TxtRefNumSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000" + TxtRefNumSer.Text;

                TxtRefNumSer.Text = strCad.Substring(strCad.Length - 4, 4);
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
        private void CboCli_SelectedValueChanged(object sender, EventArgs e)
        {

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
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            VerRegistro(intIdRegistro);
            booAgregando = false;
        }
        private void FrmManPedidos_Activated(object sender, EventArgs e)
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
        private void FgItems_RowColChange(object sender, EventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }
        }
        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if ((e.Col == 1) || (e.Col == 2))
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            if (e.Col == 4)
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            
            if ((e.Col == 5) || (e.Col == 6))
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
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

            //if (FgItems.Col == 1)
            //{
            //    if (FgItems.Row >= 2)
            //    {
            //        if (funFunciones.NulosC(FgItems.GetData(FgItems.Row - 1, 1)) == "") { FgItems.AllowEditing = false; return; }
            //    }
            //    funFlex.FlexColumnaCombo(FgItems, dtTipoExis, "c_des", 1);
            //}

            if (FgItems.Col == 2)
            {
                //// OBTNEMOS LA DESCRIPCION DEL TIPO DE PRODUCTO
                //strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 1));

                //if (strDesTipPro == "")
                //{
                //    FgItems.AllowEditing = false;
                //    funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 2);
                //    return;
                //}
                //// OBTENEMOS EL ID DEL TIPO DE PRODUCTO
                //dtResul = funDatos.DataTableFiltrar(dtTipoExis, "c_des = '" + strDesTipPro + "'");
                //if (dtResul.Rows.Count != 0)
                //{
                //    n_idtipproducto = Convert.ToInt32(dtResul.Rows[0]["n_id"].ToString());

                //    // FILTRAMOS LOS ITEMS DEL TIPO DE PRODUCTO SELECCIONADO
                //    dtResul = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + n_idtipproducto + "", "c_despro");
                //    funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 2);    // ITEMS
                //}

                n_idtipproducto = 2;

                //// FILTRAMOS LOS ITEMS DEL TIPO DE PRODUCTO SELECCIONADO
                //dtResul = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + n_idtipproducto + "", "c_despro");
                //funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 2);    // ITEMS

                //FgItems.Cols[2].ComboList = "...";

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
                FgItems.AllowEditing = true;
                return;
            }
            if (FgItems.Col == 6)
            {
                FgItems.AllowEditing = false;
                return;
            }

            FgItems.AllowEditing = true;
        }
        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            DataTable dtResul;
            string c_despro;
            int n_idpro;
            double n_Total = 0;
            string c_codigo = "";
            DataTable dtresult = new DataTable();
            int n_idite = 0;

            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            //if (FgItems.Col == 1)
            //{
            //    FgItems.Select(FgItems.Row - 1, 2);
            //    return;
            //}
            if (FgItems.Col == 1)
            {
                booAgregando = true;
                c_codigo = FgItems.GetData(FgItems.Row, 1).ToString();
                dtresult = funDatos.DataTableFiltrar(dtItems, "c_codpro = '" + c_codigo + "'");
                if (dtresult.Rows.Count != 0)
                {
                    FgItems.SetData(FgItems.Row, 1, dtresult.Rows[0]["c_codpro"].ToString());
                    FgItems.SetData(FgItems.Row, 2, dtresult.Rows[0]["c_despro"].ToString());
                    FgItems.SetData(FgItems.Row, 3, dtresult.Rows[0]["c_abrpre"].ToString());
                    FgItems.SetData(FgItems.Row, 7, TxtFchPed.Text);

                    n_idite = Convert.ToInt32(dtresult.Rows[0]["n_id"]);
                    dtresult = funDatos.DataTableFiltrar(dtitecli, "n_idite = " + n_idite.ToString() + "");
                    if (dtresult.Rows.Count != 0)
                    {
                        FgItems.SetData(FgItems.Row, 5, dtresult.Rows[0]["n_prebru"].ToString());
                        double n_valor = Convert.ToDouble(dtresult.Rows[0]["n_prebru"]) * Convert.ToDouble(FgItems.GetData(FgItems.Row, 4));
                        FgItems.SetData(FgItems.Row, 6, n_valor.ToString("0.00"));
                    }
                    else
                    {
                        FgItems.SetData(FgItems.Row, 5, "0.00");
                        FgItems.SetData(FgItems.Row, 6, "0.00");
                    }
                }
                else
                {
                    FgItems.SetData(FgItems.Row, 1, "");
                }

                FgItems.Select(FgItems.Row - 1, 4);
                booAgregando = false;
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

                        //CargarLotes(n_idpro);
                    }
                    else
                    {
                        FgItems.SetData(FgItems.Row, 3, "");
                    }
                }
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
                if (funFunciones.EsNumerico(funFunciones.NulosC(FgItems.GetData(FgItems.Row, 4).ToString())) == false)
                {
                    MessageBox.Show("¡ El dato ingresado no es un valor numerico !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booAgregando = true;
                    FgItems.SetData(FgItems.Row, 4, "");
                    booAgregando = false;
                    return;
                }
                booAgregando = true;
                n_Total = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row, 4)));
                FgItems.SetData(FgItems.Row, 4, n_Total.ToString("0.000000"));

                n_Total = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row, 4))) * Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row , 5)));
                FgItems.SetData(FgItems.Row, 6, n_Total.ToString("0.000000"));
                FgItems.Select(FgItems.Row - 1, 5);
                
                MostrarTotales();
                booAgregando = false;
                return;
            }
            if (FgItems.Col == 5)
            {
                if (funFunciones.EsNumerico(funFunciones.NulosC(FgItems.GetData(FgItems.Row, 5).ToString())) == false)
                {
                    MessageBox.Show("¡ El dato ingresado no es un valor numerico !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booAgregando = true;
                    FgItems.SetData(FgItems.Row, 5, "");
                    booAgregando = false;
                    return;
                }
                booAgregando = true;
                n_Total = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row, 5)));
                FgItems.SetData(FgItems.Row, 5, n_Total.ToString("0.000000"));

                n_Total = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row, 4))) * Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row, 5)));
                FgItems.SetData(FgItems.Row, 6, n_Total.ToString("0.000000"));
                FgItems.Select(FgItems.Row - 1 ,7);
                MostrarTotales();
                booAgregando = false;
                return;
            }

            if (FgItems.Col == 7)
            {
                if (funFunciones.EsFecha(funFunciones.NulosC(FgItems.GetData(FgItems.Row, 7).ToString())) == false)
                {
                    MessageBox.Show("¡ El dato ingresado no es un valor de fecha !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booAgregando = true;
                    FgItems.SetData(FgItems.Row, 7, "");
                    FgItems.Select(FgItems.Row - 1, 7);
                    booAgregando = false;
                    return;
                }
                if (funFunciones.NulosC(TxtFchPed.Text) == "")
                {
                    MessageBox.Show("¡ No ha ingresado la fecha de pedido de la orden de compra del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booAgregando = true;
                    FgItems.SetData(FgItems.Row, 7, "");
                    FgItems.Select(FgItems.Row - 1, 7);
                    TxtFchPed.Focus();
                    booAgregando = false;
                    return;
                }
                booAgregando = true;
                DateTime d_fch = Convert.ToDateTime(FgItems.GetData(FgItems.Row, 7).ToString());
                DateTime d_fch2 = Convert.ToDateTime(TxtFchPed.Text);

                if (d_fch < d_fch2)
                {
                    MessageBox.Show("¡ La fecha de entrega no puede ser menor a la fecha de pedido !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    FgItems.SetData(FgItems.Row, 7, "");
                    FgItems.Select(FgItems.Row - 1, 7);
                    booAgregando = false;
                    return;
                }

                FgItems.Select(FgItems.Row, 1);
                booAgregando = false;
            }
        }
        void MostrarTotales()
        {
            Double n_Total = 0;
            n_Total = funFlex.FlexSumarCol(FgItems, 6, 2, FgItems.Rows.Count - 1);
            TxtImpBru.Text = n_Total.ToString("0.00");
            n_Total = ((n_Total * 1.18) - n_Total);
            TxtImpIgv.Text = n_Total.ToString("0.00");
            n_Total = Convert.ToDouble(TxtImpBru.Text);
            n_Total = (n_Total * 1.18);
            TxtImpTot.Text = n_Total.ToString("0.00");
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
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void TxtNumSer_TextChanged(object sender, EventArgs e)
        {

        }
        private void TxtFchReg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboCli_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboPunVen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboTipDocPed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboConPag_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboVen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtFchPed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtFchPed_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                TxtFchPed.Text = "";
                TxtFchPed.CustomFormat = " ";
                TxtFchPed.Format = DateTimePickerFormat.Custom;
            }
        }
        private void TxtFchPed_ValueChanged(object sender, EventArgs e)
        {
            TxtFchPed.CustomFormat = "dd/MM/yyyy";
            TxtFchPed.Format = DateTimePickerFormat.Custom;
        }
        private void FrmManPedidos_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
        }
        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            objRegistros.mysConec = mysConec;
            dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO);    // CARGAMOS LOS DATOS DEL FORMULARIO
            
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
        private void CmdBusCli_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            objCli.mysConec = mysConec;
            dtResult = objCli.BuscarCliPro(dtCliente, 1, "n_id", "");
            if (dtResult != null)
            {
                CboPunVen.SelectedValue = 0;
                CboVen.SelectedValue = 0;

                if (dtResult.Rows.Count != 0)
                {
                    LblIdCliente.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtCliente.Text = dtResult.Rows[0]["c_nombre"].ToString();
                    CboVen.SelectedValue = Convert.ToInt32(funFunciones.NulosN(dtResult.Rows[0]["n_idven"]));
                    CboConPag.SelectedValue = Convert.ToInt32(funFunciones.NulosN(dtResult.Rows[0]["n_idcondpag"]));
                    dtResult = funDatos.DataTableFiltrar(dtPunVen, "n_idcli = " + Convert.ToInt32(LblIdCliente.Text) + "");
                    if (dtResult.Rows.Count != 0)
                    {
                        CboPunVen.Enabled = true;
                        funDatos.ComboBoxCargarDataTable(CboPunVen, dtResult, "n_id", "c_des");
                        
                        CboPunVen.SelectedValue = Convert.ToInt32(funFunciones.NulosN(dtResult.Rows[0]["n_id"]));
                    }
                    else
                    {
                        CboPunVen.Enabled = false;
                    }
                    TraePrecioCliente(Convert.ToInt32(LblIdCliente.Text));
                }
                else
                {
                    LblIdCliente.Text = "";
                    TxtCliente.Text = "";
                }
            }
        }
        void TraePrecioCliente(int n_IdCliente)
        { 
            o_itecli.mysConec = mysConec;
            o_itecli.Listar(n_IdCliente);
            dtitecli = o_itecli.dtListar;
        }
        private void FgItems_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            DataTable dtResult = new DataTable();
            string c_dato = "";

            if (e.Col == 2)
            {
                dtResult = objItems.BuscarItem("", "n_id", dtItems, 0);
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count != 0)
                    {
                        c_dato = dtResult.Rows[0]["c_despro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                        FgItems.SetData(e.Row, 2, c_dato);

                        c_dato = dtResult.Rows[0]["c_abrpre"].ToString();        // MOSTRAMOS LA PRESENTACION DEL ITEM
                        FgItems.SetData(e.Row, 3, c_dato);
                    }
                }
            }
        }
        //private void toolStripButton1_Click(object sender, EventArgs e)
        //{

        //}
        private void enviarAProudccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Rpta = MessageBox.Show("Desea enviar este pedido a produccion ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                int n_IdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
                objRegistros.mysConec = mysConec;
                objRegistros.ActualizarEnvioProduccion(n_IdRegistro, 1);
                dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
                ListarItems();
            }
        }
        private void quitarDeProduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Rpta = MessageBox.Show("Desea quitar este pedido de produccion ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                int n_IdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
                objRegistros.mysConec = mysConec;
                objRegistros.ActualizarEnvioProduccion(n_IdRegistro, 0);
                dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
                ListarItems();
            }
        }

        private void CmdMosEnt_Click(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            CN_vta_pedidocli objpedcli = new CN_vta_pedidocli();
            int n_idreg = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            objpedcli.mysConec = mysConec;
            objpedcli.SeleccionarEntregas(n_idreg, false);
        }

        private void emitirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_vta_pedidocli objAlm = new CN_vta_pedidocli();
            
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            objAlm.STU_SISTEMA = STU_SISTEMA;
            objAlm.mysConec = mysConec;
            objAlm.ImprimirDocumento(intIdRegistro, 1);
        }

        private void guiasDelMesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_vta_pedidocli objAlm = new CN_vta_pedidocli();

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            objAlm.STU_SISTEMA = STU_SISTEMA;
            objAlm.mysConec = mysConec;
            objAlm.ImprimirDocumento(intIdRegistro, 2);
        }

        private void CmdRefDat_Click(object sender, EventArgs e)
        {
            CargarDatosAdicionales();
            MessageBox.Show("¡ Los datos se actualizaron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-VTA-PEDIDOS-" + STU_SISTEMA.ANOTRABAJO.ToString() + Convert.ToInt32(CboMeses.SelectedValue).ToString("00") + ".xls";
            DgLista.ExportTo(c_NomArchivo);
            MessageBox.Show("! Se exporto con exito la informacion en el archivo " + c_NomArchivo + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void FgItems_KeyUp(object sender, KeyEventArgs e)
        {
            if (FgItems.Rows.Count == 2) { return; }
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }
            if (booAgregando == true) { return; }
            
            DataTable dtResul = new DataTable();
            string c_dato = "";
            int n_idite = 0;
            DataTable dtresult2 = new DataTable();

            if ((FgItems.Col == 1) || (FgItems.Col == 2))
            {
                if (e.KeyCode.ToString() == "F5")
                {
                    booAgregando = true;
                    dtResul = objItems.BuscarItem("", "n_id", dtItems, 0);
                    if (dtResul != null)
                    {
                        if (dtResul.Rows.Count != 0)
                        {
                            c_dato = dtResul.Rows[0]["c_codpro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                            FgItems.SetData(FgItems.Row, 1, c_dato);

                            c_dato = dtResul.Rows[0]["c_despro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                            FgItems.SetData(FgItems.Row, 2, c_dato);

                            c_dato = dtResul.Rows[0]["c_abrpre"].ToString();        // MOSTRAMOS LA PRESENTACION DEL ITEM
                            FgItems.SetData(FgItems.Row, 3, c_dato);

                            n_idite = Convert.ToInt32(dtResul.Rows[0]["n_id"]);
                            dtresult2 = funDatos.DataTableFiltrar(dtitecli, "n_idite = " + n_idite.ToString() + "");
                            if (dtresult2.Rows.Count != 0)
                            {
                                FgItems.SetData(FgItems.Row, 5, dtresult2.Rows[0]["n_prebru"].ToString());
                            }

                            FgItems.Select(FgItems.Row, 4);
                        }
                    }
                    booAgregando = false;
                }
            }
        }

        private void imprimirPedidoFormatoBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_vta_pedidocli objAlm = new CN_vta_pedidocli();

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            objAlm.STU_SISTEMA = STU_SISTEMA;
            objAlm.mysConec = mysConec;
            objAlm.ImprimirDocumento(intIdRegistro, 3);
        }
    }
}
