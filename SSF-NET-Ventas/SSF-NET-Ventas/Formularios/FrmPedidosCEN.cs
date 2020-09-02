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
using System.Xml;

namespace SSF_NET_Ventas.Formularios
{
    public partial class FrmPedidosCEN : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_vta_pedidocen objRegistros = new CN_vta_pedidocen();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_clipro objCliPro = new CN_mae_clipro();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_vta_punvencli objPunVen = new CN_vta_punvencli();
        CN_alm_inventariounimed ObjAlmUniMed = new CN_alm_inventariounimed();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_vta_itemcen objItemCen = new CN_vta_itemcen();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_VTA_PEDIDOCEN entRegistro = new BE_VTA_PEDIDOCEN();
        List<BE_VTA_PEDIDOCENDET> LstDetalle = new List<BE_VTA_PEDIDOCENDET>();   // CREAMOS UNA LISTA DEL TIPO BE_VTA_GUIASDET PARA ALMACENAR LOS ITEMS

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();
        DataTable dtCliPro = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtPunVen = new DataTable();
        DataTable dtPresentaItem = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtItemCen = new DataTable();

        // VARIABLES LOCALES
        int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[8, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[4, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^-" + (char)8;

        public FrmPedidosCEN()
        {
            InitializeComponent();
        }
        private void FrmPedidosCEN_Load(object sender, EventArgs e)
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

            dtPunVen = funDatos.DataTableOrdenar(dtPunVen, "c_des");
            funDatos.ComboBoxCargarDataTable(CboPunlle, dtPunVen, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboPunVen, dtPunVen, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 594;
            this.Width = 930;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            arrCabeceraFlex1[0, 0] = "Codigo";
            arrCabeceraFlex1[0, 1] = "100";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Descripcion";
            arrCabeceraFlex1[1, 1] = "400";
            arrCabeceraFlex1[1, 2] = "c";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Unidad Medida";
            arrCabeceraFlex1[2, 1] = "70";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_itepredes";

            arrCabeceraFlex1[3, 0] = "Cantidad";
            arrCabeceraFlex1[3, 1] = "80";
            arrCabeceraFlex1[3, 2] = "N";
            arrCabeceraFlex1[3, 3] = "0.000000";
            arrCabeceraFlex1[3, 4] = "n_can";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtItems, 2, false);
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
            objFormVis.ObtenerCabeceraLista(55, ref arrCabeceraDg1);

            objCliPro.mysConec = mysConec;
            dtCliPro = objCliPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(55);

            objItems.mysConec = mysConec;
            dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);                                        // CARGAMOS TODOS LOS DEPARTAMENTOS

            objItemCen.mysConec = mysConec;
            dtItemCen = objItemCen.Listar();

            objPunVen.mysConec = mysConec;
            dtPunVen = objPunVen.Listar();

            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();
        }
        void ListarItems()
        {
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
            dtLista = objRegistros.dtLista;
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
            string c_dato = "";
            int n_dato = 0;

            booAgregando = true;
            Blanquea();
            FgItems.Rows.Count = 2;
            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdRegistro);
            entRegistro = objRegistros.entRegistro;
            LstDetalle = objRegistros.LstDetalle;

            TxtFchDes.Text = "";
            TxtNumOrd.Text = entRegistro.c_numped;

            c_dato = entRegistro.c_codcli;
            c_dato = funDatos.DataTableBuscar(dtCliPro, "c_codcen", "c_nombre", c_dato, "C").ToString();
            TxtCliente.Text = c_dato;
            c_dato = entRegistro.c_codcli;
            c_dato = funDatos.DataTableBuscar(dtCliPro, "c_codcen", "n_id", c_dato, "C").ToString();
            LblIdCliente.Text = c_dato;

            c_dato = entRegistro.c_codpunven;
            //c_dato = funDatos.DataTableBuscar(dtPunVen, "c_codcen", "c_des", c_dato, "C").ToString();
            TxtPunVen.Text = c_dato;
            c_dato = entRegistro.c_codpunven;
            n_dato = Convert.ToInt32(funDatos.DataTableBuscar(dtPunVen, "c_codcen", "n_id", c_dato, "C"));
            CboPunVen.SelectedValue = n_dato;

            c_dato = entRegistro.c_codpunent;
            //c_dato = funDatos.DataTableBuscar(dtPunVen, "c_codcen", "c_des", c_dato, "C").ToString();
            TxtLugEnt.Text = c_dato;
            c_dato = entRegistro.c_codpunent;
            n_dato = Convert.ToInt32(funDatos.DataTableBuscar(dtPunVen, "c_codcen", "n_id", c_dato, "C"));
            CboPunlle.SelectedValue = n_dato;

            TcxtFchPed.Text = Convert.ToDateTime(entRegistro.d_fchemi).ToString("dd/MM/yyyy");
            TxtFchEnt.Text = Convert.ToDateTime(entRegistro.d_fchent).ToString("dd/MM/yyyy");
        
            double n_can = 0;
            string c_idite = "";
            foreach (BE_VTA_PEDIDOCENDET element in LstDetalle)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;

                c_dato = element.c_coditecen;
                n_can = element.n_canpro;

                FgItems.SetData(FgItems.Rows.Count - 1, 1, c_dato);

                c_idite = funDatos.DataTableBuscar(dtItemCen, "c_codcen", "n_iditem", c_dato, "C").ToString();
                c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", c_idite, "N").ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 2, c_dato);

                c_dato = funDatos.DataTableBuscar(dtPresentaItem, "n_idite", "c_abrpre", c_idite,"N").ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 3, c_dato);
                
                FgItems.SetData(FgItems.Rows.Count - 1, 4, n_can.ToString("0.00"));
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
            FgItems.Cols[2].ComboList = "...";
            FgItems.Focus();
        }
        void Blanquea()
        {
            TxtFchDes.Text = "";
            TxtNumOrd.Text = "";
            TxtCliente.Text = "";
            TcxtFchPed.Text = "";
            TxtFchEnt.Text = "";
            TxtPunVen.Text = "";
            TxtLugEnt.Text = "";
            CboPunlle.SelectedValue = 0;
            CboPunVen.SelectedValue = 0;
            TxtGlosa.Text = "";

            FgItems.Rows.Count = 2;
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;
        }
        void Bloquea()
        {
            TxtFchDes.Enabled = !TxtFchDes.Enabled;
            TxtNumOrd.Enabled = !TxtNumOrd.Enabled;
            TxtCliente.Enabled = !TxtCliente.Enabled;
            TcxtFchPed.Enabled = !TcxtFchPed.Enabled;
            TxtFchEnt.Enabled = !TxtFchEnt.Enabled;
            //TxtPunVen.Enabled = !TxtPunVen.Enabled;
            //TxtLugEnt.Enabled = !TxtLugEnt.Enabled;
            TxtGlosa.Enabled = !TxtGlosa.Enabled;

            CboPunlle.Enabled = !CboPunlle.Enabled;
            CboPunVen.Enabled = !CboPunVen.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolEditar.Enabled = !ToolEditar.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir.Enabled = !ToolImprimir.Enabled;
            ToolExportar.Enabled = !ToolExportar.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
        }
        void Modificar()
        {
            int n_despachado = Convert.ToInt32(DgLista.Columns["n_des"].CellValue(DgLista.Row).ToString());
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            if (n_despachado == 1)
            {
                MessageBox.Show("No se puede modificar un pedido despachado", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            
            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            FgItems.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            int n_despachado = Convert.ToInt32(DgLista.Columns["n_des"].CellValue(DgLista.Row).ToString());

            if (n_despachado == 1)
            {
                MessageBox.Show("No se puede eliminar un pedido despachado", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return false;
            }

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    //objRegistros.mysConec = mysConec;
                    //objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
                    //dtLista = objRegistros.dtLista;
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

            objRegistros.LstDetalle = LstDetalle;    // ASIGNAMOS EL DETALLE DE LA GUIA

            if (n_QueHace == 1)
            {
                //booResultado = objRegistros.Insertar(BE_ListaReg, LstGuiaDoc);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(entRegistro, LstDetalle);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.n_ErrorNumber.ToString() + " = " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_id = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());  
            
            entRegistro.n_idemp = STU_SISTEMA.EMPRESAID;
            entRegistro.n_id = n_id;
            entRegistro.c_codcli = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_codcen", LblIdCliente.Text, "N").ToString();
            entRegistro.c_codpunven = funDatos.DataTableBuscar(dtPunVen, "n_id", "c_codcen", CboPunVen.SelectedValue.ToString(), "N").ToString();
            entRegistro.c_codpunent = funDatos.DataTableBuscar(dtPunVen, "n_id", "c_codcen", CboPunlle.SelectedValue.ToString(), "N").ToString();
            entRegistro.d_fchemi = Convert.ToDateTime(TcxtFchPed.Text);
            entRegistro.d_fchent = Convert.ToDateTime(TxtFchEnt.Text);
            entRegistro.n_numite = ((FgItems.Rows.Count-1)-2);
            entRegistro.c_numped = TxtNumOrd.Text;
            entRegistro.d_fchdes = Convert.ToDateTime(TxtFchDes.Text);
            entRegistro.n_anotra = STU_SISTEMA.ANOTRABAJO;
            entRegistro.n_mestra = STU_SISTEMA.MESTRABAJO;
            entRegistro.n_idguides = 0;
            entRegistro.n_despachado = 1;           
        }
        bool CamposOK()
        {
            //int n_fila = 0;
            bool booEstado = true;

            //if (LblIdCliente.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado el cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    return booEstado;
            //}
            //if (TxtNumSer.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado el numero de serie !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    return booEstado;
            //}
            //if (TxtNumDoc.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado el numero de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    return booEstado;
            //}
            //if (TxtFchEmiDoc.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado la fecha de emision !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    return booEstado;
            //}
            //if (Convert.ToInt32(CboMotTras.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado el motivo de traslado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    return booEstado;
            //}
            //if (TxtOrdCom.Text != "")
            //{
            //    if (TxtFchEmiOC.Text == "")
            //    {
            //        MessageBox.Show("¡ No ha especificado la fecha de emision de la orden de compra !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //        booEstado = false;
            //        return booEstado;
            //    }
            //    if (TxtFchEntOC.Text == "")
            //    {
            //        MessageBox.Show("¡ No ha especificado la fecha de entrega de la orden de compra !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //        booEstado = false;
            //        return booEstado;
            //    }
            //}
            //if (Convert.ToInt32(CboTra.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado la empresa de tranporte !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    return booEstado;
            //}
            //if (Convert.ToInt32(CboCho.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado el nombe del chofer !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    return booEstado;
            //}
            //if (Convert.ToInt32(CboVeh.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado el vehiculo de transporte !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    return booEstado;
            //}

            //string c_dato = "";
            //for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
            //{
            //    if (funFunciones.NulosC(FgItems.GetData(n_fila, 2)).ToString() != "")
            //    {
            //        c_dato = FgItems.GetData(n_fila, 2).ToString();

            //        if (funFunciones.NulosC(FgItems.GetData(n_fila, 4)).ToString() == "")
            //        {
            //            MessageBox.Show("¡ No ha especificado la cantidad, para el item " + c_dato + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //            booEstado = false;
            //            return booEstado;
            //        }

            //        if (funFunciones.NulosC(FgItems.GetData(n_fila, 5)).ToString() == "")
            //        {
            //            MessageBox.Show("¡ No ha especificado el numero de lote, para el item " + c_dato + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //            booEstado = false;
            //            return booEstado;
            //        }
            //    }
            //}

            return booEstado;
        }
        private void FrmPedidosCEN_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (dtLista.Rows.Count == 0)
                {
                    DialogResult Rpta = MessageBox.Show("No se han encontrado registros, ¿ Desea importar pedidos ahora ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (DialogResult.Yes == Rpta)
                    {
                        objRegistros.STU_SISTEMA = STU_SISTEMA;
                        objRegistros.ProcesarCEN();
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
        private void ToolNuevo_Click(object sender, EventArgs e)
        {
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.ProcesarCEN();
            //CargarPedidos();
        }
        private void ToolModificar_Click(object sender, EventArgs e)
        {
            FrmPedidoCENGuias xFrmGui = new FrmPedidoCENGuias();
            xFrmGui.STU_SISTEMA = STU_SISTEMA;
            xFrmGui.mysConec = mysConec;
            xFrmGui.ShowDialog();
            ListarItems();
        }

        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            if (Grabar() == true)
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                //objRegistros.mysConec = mysConec;
                //objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
                //dtLista = objRegistros.dtLista;
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
            dtLista = null;

            objFormVis = null;

            this.Close();
        }
        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (n_QueHace != 3) { return; }

            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    VerRegistro(intIdRegistro);
                }
            }
        }
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
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
        private void FrmPedidosCEN_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        //void CargarPedidos()
        //{
        //    int n_numnarc = 0;
        //    int n_fila = 0;
        //    int n_row = 0;
        //    string[] c_ListaArchivos;
        //    string[,] c_ListaItems = new string[30, 2];
        //    string[,] c_ListaDatos = new string[5, 2]; 
        //    string c_nomarch = "";
        //    string c_rutapedido = "c:\\ssf-net\\pedidos";
        //    Helper.Cls_IO funIO = new Helper.Cls_IO();

        //    c_ListaArchivos = funIO.Dir_LeerDirectorio(c_rutapedido, "*.xml");
        //    n_numnarc = Convert.ToInt32(c_ListaArchivos.GetLongLength(0));

        //    if (n_numnarc == 0)
        //    {
        //        MessageBox.Show("No han encontrado bajas aceptadas por SUNAT", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        //        return;
        //    }

        //    for (n_fila = 0; n_fila <= n_numnarc - 1;n_fila++)
        //    {
        //        c_nomarch = c_ListaArchivos[n_fila];
        //        XmlDocument xDoc = new XmlDocument();    
        //        xDoc.Load(c_nomarch);
        //        XmlNodeList nodNodo = xDoc.GetElementsByTagName("body");
        //        XmlNodeList Lista = ((XmlElement)nodNodo[0]).GetElementsByTagName("lineItem");

        //        c_ListaDatos[0, 0] = "uniqueCreatorIdentification";          // NUMERO DE LA ORDEN DE COMPRA
        //        c_ListaDatos[0, 1] = "";

        //        c_ListaDatos[1, 0] = "movementDate";                         // FECHA DE EMISION
        //        c_ListaDatos[1, 1] = "";

        //        c_ListaDatos[2, 0] = "buyer";                                // NOMBRE DEL CLIENTE
        //        c_ListaDatos[2, 1] = "";

        //        c_ListaDatos[3, 0] = "shipParty";                            // PUNTO DE ENTREGA DEL CLIENTE
        //        c_ListaDatos[3, 1] = "";

        //        c_ListaDatos[4, 0] = "seller";                               // PUNTO DE VENTA DEL CLIENTE
        //        c_ListaDatos[4, 1] = "";

        //        XML_funciones funXML = new XML_funciones();
        //        if (funXML.XML_LeerNodo(ref c_ListaDatos, c_nomarch) == false)
        //        {
        //            MessageBox.Show("¡ No se pudo leer la orden de compra Nº " + c_ListaDatos[0, 1] + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        //            break;
        //            return;
        //        }

        //        n_row = 0;
        //        foreach (XmlElement nodo in Lista)
        //        {
        //            XmlNodeList c_codpro = nodo.GetElementsByTagName("itemIdentification");
        //            XmlNodeList c_canpro = nodo.GetElementsByTagName("requestedQuantity");

        //            c_ListaItems[n_row, 0] = c_codpro[0].InnerText;   // CODIGO DEL PRODUCTO
        //            c_ListaItems[n_row, 1] = c_canpro[0].InnerText;    // CANTIDAD DEL PRODUCTO
        //            n_row = n_row + 1;
        //        }

        //        PrepararEntidad(c_ListaDatos, c_ListaItems);
        //        if (objRegistros.Insertar(entRegistro, LstDetalle) == false)
        //        {
        //            MessageBox.Show("¡ No se pudo cargar la orden de compra Nº " + c_ListaDatos[0, 1] + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        //            return;
        //        }
        //        else
        //        {
        //            funIO.Fil_EliminarArchivo(c_nomarch);
        //        }
        //    }
        //    MessageBox.Show("¡ Los pedidos se importaron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        //    ListarItems();
        //}
        //void PrepararEntidad(string [,] c_Cabecera, string [,] c_Detalle)
        //{
        //    int n_row = 0;

        //    entRegistro.n_idemp = STU_SISTEMA.EMPRESAID;
        //    entRegistro.n_id = 0;
        //    entRegistro.c_codcli = "7750174001558";
        //    entRegistro.c_codpunven = c_Cabecera[4, 1].Substring(5,13);
        //    entRegistro.c_codpunent = c_Cabecera[3, 1].Substring(5, 13);
        //    entRegistro.d_fchemi = Convert.ToDateTime(c_Cabecera[1, 1]);
        //    entRegistro.d_fchent = Convert.ToDateTime(c_Cabecera[1, 1]);
        //    entRegistro.n_numite = 0;
        //    entRegistro.c_numped = c_Cabecera[0, 1];
        //    entRegistro.d_fchdes = DateTime.Now;
        //    entRegistro.n_anotra = STU_SISTEMA.ANOTRABAJO;
        //    entRegistro.n_mestra = STU_SISTEMA.MESTRABAJO;

        //    int n_numite = Convert.ToInt32(c_Detalle.GetLongLength(0));
        //    LstDetalle.Clear();
        //    for (n_row = 0; n_row <= n_numite - 1; n_row++)
        //    {
        //        BE_VTA_PEDIDOCENDET entDet = new BE_VTA_PEDIDOCENDET();

        //        if (funFunciones.NulosC(c_Detalle[n_row, 0]).ToString() != "")
        //        { 
        //            entDet.n_idped = 0;
        //            entDet.c_coditecen = c_Detalle[n_row, 0];
        //            entDet.n_canpro = Convert.ToDouble(c_Detalle[n_row,1]);
        //            entDet.c_codunimedcen = "";
        //            LstDetalle.Add(entDet);
        //        }
        //    }
        //}

        private void eliminarGuiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TooGroupEliminar_ButtonClick(object sender, EventArgs e)
        {

        }

        private void ToolEliminar_Click(object sender, EventArgs e)
        {
            
        }

        private void eliminarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void dehacerEnvioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DehacerPedido();
        }
        bool DehacerPedido()
        {
            bool booResult = false;
            
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            int n_despachado = Convert.ToInt32(DgLista.Columns["n_des"].CellValue(DgLista.Row).ToString());

            if (n_despachado == 0)
            {
                MessageBox.Show("No se puede ejecutar esta opcion el pedido no ha sido despachado", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return false;
            }

            DialogResult Rpta = MessageBox.Show("Esta seguro de dehacer el envio de este pedido", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.ActualizarGuiaDespacho(n_IdRegistro, 0, 1) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El envio se deshizo con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo deshacer el envio por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            return booResult;
        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            if (booAgregando == true) { return; }

            objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO);     // CARGAMOS LOS DATOS DEL FORMULARIO
            dtLista = objRegistros.dtLista;

            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);

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

        private void ToolEditar_Click(object sender, EventArgs e)
        {
            Modificar();
        }

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-VTA-PEDIDOSCEN-" + STU_SISTEMA.ANOTRABAJO.ToString() + Convert.ToInt32(CboMeses.SelectedValue).ToString("00") + ".xls";
            DgLista.ExportTo(c_NomArchivo);
            MessageBox.Show("! Se exporto con exito la informacion en el archivo " + c_NomArchivo + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void ToolImprimir_ButtonClick(object sender, EventArgs e)
        {

        }

        private void emitirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_vta_pedidocen o_ped = new CN_vta_pedidocen();
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());  
            o_ped.STU_SISTEMA = STU_SISTEMA;
            o_ped.ImprimirPedido(n_IdRegistro);
        }

        private void imprimirPedidosDelMesDetalladoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_vta_pedidocen o_ped = new CN_vta_pedidocen();
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            o_ped.STU_SISTEMA = STU_SISTEMA;
            o_ped.mysConec = mysConec;
            o_ped.ImprimirPedidoDet(n_IdRegistro);
        }
    }
}
