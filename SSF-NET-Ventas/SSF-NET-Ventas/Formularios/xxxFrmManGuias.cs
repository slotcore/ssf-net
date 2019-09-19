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
    public partial class FrmManGuias : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_vta_guias objRegistros = new CN_vta_guias();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_clipro objCliPro = new CN_mae_clipro();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_sun_motivotraslado objMotTra = new CN_sun_motivotraslado();
        CN_vta_punvencli objPunVen = new CN_vta_punvencli();
        CN_vta_emptra objTrans = new CN_vta_emptra();
        CN_vta_chofer objChof = new CN_vta_chofer();
        CN_vta_vehiculo objVehi = new CN_vta_vehiculo();
        CN_alm_inventariounimed ObjAlmUniMed = new CN_alm_inventariounimed();
        CN_sun_tipexi objTipoExi = new CN_sun_tipexi();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_sun_tipdoccom objTipPed = new CN_sun_tipdoccom();
        CN_sys_empresalocal objEmlLoc = new CN_sys_empresalocal();
        
        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_VTA_GUIAS BE_ListaReg = new BE_VTA_GUIAS();
        BE_VTA_GUIAS BE_Registro = new BE_VTA_GUIAS();
        BE_VTA_GUIASDET BE_GuiaDetalle = new BE_VTA_GUIASDET();
        List<BE_VTA_GUIASDET> LstDetalle = new List<BE_VTA_GUIASDET>();   // CREAMOS UNA LISTA DEL TIPO BE_VTA_GUIASDET PARA ALMACENAR LOS ITEMS
        List<BE_VTA_GUIASDOC> LstGuiaDoc = new List<BE_VTA_GUIASDOC>();
        BE_VTA_GUIASDATOS e_GuiaDatos = new BE_VTA_GUIASDATOS();

        // DATATABLE LOCALES
        DataTable dtRegistros = new DataTable();
        DataTable dtCliPro = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtMotTra = new DataTable();
        DataTable dtPunVen = new DataTable();
        DataTable dtTrans = new DataTable();
        DataTable dtChof = new DataTable();
        DataTable dtVehi = new DataTable();
        DataTable dtMoviDetalle = new DataTable();
        DataTable dtTipoExis = new DataTable();
        DataTable dtPresentaItem = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtPunPar = new DataTable();
        DataTable dtTipDocAnu = new DataTable();
        DataTable dtTipPed = new DataTable();

        // VARIABLES LOCALES
        int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[11, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[5, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^-" + (char)8;
        public FrmManGuias()
        {
            InitializeComponent();
        }
        private void FrmManGuias_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();
                        
            funDatos.ComboBoxCargarDataTable(CboMotTras, dtMotTra, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboPunVen, dtPunVen, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboPunPar, dtPunPar, "n_id", "c_des");
            
            funDatos.ComboBoxCargarDataTable(CboTra, dtTrans, "n_id", "c_nombre");
            funDatos.ComboBoxCargarDataTable(CboCho, dtChof, "n_id", "c_nomcho");
            funDatos.ComboBoxCargarDataTable(CboVeh, dtVehi, "n_id", "c_marca");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipPed, dtTipPed, "n_id", "c_des");
            
            funDatos.ComboBoxCargarDataTable(CboTipDocAnu, dtTipDocAnu, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 640;
            this.Width = 930;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            arrCabeceraFlex1[0, 0] = "Tipo Item";
            arrCabeceraFlex1[0, 1] = "130";
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Item";
            arrCabeceraFlex1[1, 1] = "430";
            arrCabeceraFlex1[1, 2] = "S";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Uni. Med.";
            arrCabeceraFlex1[2, 1] = "70";
            arrCabeceraFlex1[2, 2] = "S";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_itepredes";

            arrCabeceraFlex1[3, 0] = "Cantidad";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "0.000000";
            arrCabeceraFlex1[3, 4] = "n_can";

            arrCabeceraFlex1[4, 0] = "Nº Lote";
            arrCabeceraFlex1[4, 1] = "100";
            arrCabeceraFlex1[4, 2] = "C";
            arrCabeceraFlex1[4, 3] = "";
            arrCabeceraFlex1[4, 4] = "n_numlot";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 1);

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(22, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(22);
            CargarDatosAdicionales();
        }
        void CargarDatosAdicionales()
        {
            objCliPro.mysConec = mysConec;
            dtCliPro = objCliPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objItems.mysConec = mysConec;
            dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);                                        // CARGAMOS TODOS LOS DEPARTAMENTOS
            dtItems = funDatos.DataTableFiltrar(dtItems, "n_idtipexi IN (1, 2, 10)");

            objMotTra.mysConec = mysConec;
            dtMotTra = objMotTra.Listar();

            objPunVen.mysConec = mysConec;
            dtPunVen = objPunVen.Listar();

            objEmlLoc.mysConec = mysConec;
            dtPunPar = objEmlLoc.Listar(STU_SISTEMA.EMPRESAID, 0);
            //dtPunPar = objEmlLoc.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objTrans.mysConec = mysConec;
            dtTrans = objTrans.Listar(STU_SISTEMA.EMPRESAID);

            objChof.mysConec = mysConec;
            dtChof = objChof.Listar(STU_SISTEMA.EMPRESAID);

            objVehi.mysConec = mysConec;
            dtVehi = objVehi.Listar(STU_SISTEMA.EMPRESAID);

            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            objTipoExi.mysConec = mysConec;
            dtTipoExis = objTipoExi.Listar();
            dtTipoExis = funDatos.DataTableFiltrar(dtTipoExis, "n_id IN (1, 2, 10)");

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objTipDoc.mysConec = mysConec;
            dtTipDocAnu = objTipDoc.Listar();
            dtTipDocAnu = funDatos.DataTableFiltrar(dtTipDocAnu, "n_id IN (10)");

            objTipPed.mysConec = mysConec;
            dtTipPed = objTipPed.Listar();
            dtTipPed = funDatos.DataTableFiltrar(dtTipPed, "n_id IN (77, 79)");
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtRegistros.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtRegistros, true);
            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
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
            Blanquea();
            objRegistros.mysConec = mysConec;
            BE_Registro = objRegistros.TraerRegistro(n_IdRegistro);
            LstDetalle = objRegistros.LstDetalle;
            e_GuiaDatos = objRegistros.e_GuiaDatos;  
       
            LblIdCliente.Text = BE_Registro.n_idcli.ToString();
            TxtCliente.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_nombre", BE_Registro.n_idcli.ToString(), "N").ToString();
            TxtNumRuc.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_numdoc", BE_Registro.n_idcli.ToString(), "N").ToString();
            TxtNumSer.Text = BE_Registro.c_numser;
            TxtNumDoc.Text = BE_Registro.c_numdoc;
            TxtFchEmiDoc.Text = BE_Registro.d_fchdoc.ToString();
            CboMotTras.SelectedValue = BE_Registro.n_idmottra;
            TxtOrdCom.Text = BE_Registro.c_numordcom;
            LblIdOC.Text = BE_Registro.n_iddocref.ToString();
            if (BE_Registro.d_fchpeddocref.ToString() == "")
            {
                TxtFchEmiOC.CustomFormat = " ";
                TxtFchEmiOC.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                TxtFchEmiOC.CustomFormat = "dd/MM/yyyy";
                TxtFchEmiOC.Format = DateTimePickerFormat.Custom;
                TxtFchEmiOC.Text = BE_Registro.d_fchpeddocref.ToString();
            }

            if (BE_Registro.d_fchentdocref.ToString() == "")
            {
                TxtFchEntOC.CustomFormat = " ";
                TxtFchEntOC.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                TxtFchEntOC.CustomFormat = "dd/MM/yyyy";
                TxtFchEntOC.Format = DateTimePickerFormat.Custom;
                TxtFchEntOC.Text = BE_Registro.d_fchentdocref.ToString();
            }

            CboPunVen.SelectedValue = BE_Registro.n_idpunvencli;
            TxtLugEnt.Text = BE_Registro.c_dirpunlle;
            TxtLugPar.Text = BE_Registro.c_dirpunpar;
            CboPunPar.SelectedValue = BE_Registro.n_idpunpar;
            CboTra.SelectedValue = BE_Registro.n_idemptra;
            CboCho.SelectedValue = BE_Registro.n_idcho;
            TxtNumBre.Text = funDatos.DataTableBuscar(dtChof, "n_id", "c_numbre", BE_Registro.n_idcho.ToString(), "N").ToString();
            CboVeh.SelectedValue = BE_Registro.n_idvehtra;
            TxtNumPla.Text = funDatos.DataTableBuscar(dtVehi, "n_id", "c_numpla", BE_Registro.n_idvehtra.ToString(), "N").ToString();

            CboTipPed.SelectedValue = BE_Registro.n_idtipdocref;

            if (BE_Registro.n_tipgui == 1) { OptSinDoc.Checked = true; }
            if (BE_Registro.n_tipgui == 2) { OptConDoc.Checked = true; }
            
            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);

            int n_fila = 2;
            int n_idTipExi;
            int n_idIte;
            int n_idUniMed;
            DataTable DtFiltro = new DataTable();
            string strCadenaFiltro;
            
            foreach (BE_VTA_GUIASDET element in LstDetalle)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;
               
                n_idTipExi = element.n_idtipexi;
                n_idIte = element.n_idite;
                n_idUniMed = element.n_idunimed;

                // MOSTRAMOS EL TPO DE EXIXTENCIA
                strCadenaFiltro = "n_id = " + n_idTipExi + "";
                DtFiltro = funDatos.DataTableFiltrar(dtTipoExis, strCadenaFiltro);
                FgItems.SetData(n_fila, 1, DtFiltro.Rows[0]["c_des"].ToString());

                // MOSTRAMOS EL ITEM
                strCadenaFiltro = "n_id = " + n_idIte + "";
                DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                FgItems.SetData(n_fila, 2, DtFiltro.Rows[0]["c_despro"].ToString());

                // MOSTRAMOS LA UNIDAD DE MEDIDA
                strCadenaFiltro = "n_idite = " + n_idIte + "";
                DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                FgItems.SetData(n_fila, 3, DtFiltro.Rows[0]["c_abrpre"].ToString());

                FgItems.SetData(n_fila, 4, element.n_canpro);
                FgItems.SetData(n_fila, 5, element.c_numlot);

                n_fila++;
            }
            if (LstDetalle.Count ==0)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;
            }

            booAgregando = false;
        }
        void Nuevo()
        {
            n_QueHace = 1;
            Tab1.TabPages[0].Enabled = false;
            booAgregando = true;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            Tab1.SelectedIndex = 1;
            TxtNumSer.Text = "0001";
            objTipDoc.mysConec = mysConec;
            TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 10, TxtNumSer.Text);
            FgItems.Cols[2].ComboList = "...";
            CboMotTras.SelectedValue = 1;

            int n_idpunpar = Convert.ToInt16(dtPunPar.Rows[0]["n_id"]);
            CboPunPar.SelectedValue = n_idpunpar;
            CmdBusOC.Enabled = false;
            CmdCarPedPen.Enabled = false;
            OptSinDoc.Checked = true;
            OptConDoc.Checked = false;
            booAgregando = false;
            TxtNumSer.Focus();
        }
        void Blanquea()
        {
            //CboCliente.SelectedValue = 0; 
            LblIdOC.Text = "";

            TxtNumRuc.Text = "";
            LblIdCliente.Text = "";
            TxtCliente.Text = "";
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtFchEmiDoc.Text = "";
            TxtFchEmiDoc.CustomFormat = "dd/MM/yyyy";
            TxtFchEmiDoc.Format = DateTimePickerFormat.Custom;

            CboMotTras.SelectedValue = 0;
            TxtOrdCom.Text = "";
            TxtFchEmiOC.Text = "";
            TxtFchEmiOC.CustomFormat = " ";
            TxtFchEmiOC.Format = DateTimePickerFormat.Custom;

            TxtFchEntOC.Text = "";
            TxtFchEntOC.CustomFormat = " ";
            TxtFchEntOC.Format = DateTimePickerFormat.Custom;

            CboPunVen.SelectedValue = 0;
            TxtLugEnt.Text = "";
            CboPunPar.SelectedValue = 0;
            TxtLugPar.Text = "";
            CboTra.SelectedValue = 0;
            CboCho.SelectedValue = 0;
            TxtNumBre.Text = "";
            CboVeh.SelectedValue = 0;
            TxtNumPla.Text = "";
            FgItems.Rows.Count = 2;
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;

            LstGuiaDoc.Clear();
            LstDetalle.Clear();
        }
        void Bloquea()
        {
            TxtNumRuc.Enabled = !TxtNumRuc.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtFchEmiDoc.Enabled = !TxtFchEmiDoc.Enabled;
            CboMotTras.Enabled = !CboMotTras.Enabled;
            //TxtOrdCom.Enabled = !TxtOrdCom.Enabled;
            //TxtFchEmiOC.Enabled = !TxtFchEmiOC.Enabled;
            //TxtFchEntOC.Enabled = !TxtFchEntOC.Enabled;
            CboPunVen.Enabled = !CboPunVen.Enabled;
            TxtLugEnt.Enabled = !TxtLugEnt.Enabled;
            CboTra.Enabled = !CboTra.Enabled;
            CboCho.Enabled = !CboCho.Enabled;
            TxtNumBre.Enabled = !TxtNumBre.Enabled;
            CboVeh.Enabled = !CboVeh.Enabled;
            TxtNumPla.Enabled = !TxtNumPla.Enabled;
            CboPunPar.Enabled = !CboPunPar.Enabled;

            CmdAddIte.Enabled = !CmdAddIte.Enabled;
            CmdDelIte.Enabled = !CmdDelIte.Enabled;
            CmdBusCli.Enabled = !CmdBusCli.Enabled;

            //CmdDatAdi.Enabled = !CmdDatAdi.Enabled;
        }
        void MostrarEstadoMes(int n_IdEmpresa, int n_IdMes)
        {
            obj_fungen.mysConec = mysConec;
            if (obj_fungen.EstadoPeriodo(n_IdEmpresa, n_IdMes, 9) == true)
            {
                ToolNuevo.Visible = false;
                ToolModificar.Visible = false;
                TooGroupEliminar.Visible = false;

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
                TooGroupEliminar.Visible = true;

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
            ToolExportar.Enabled = !ToolExportar.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;

            CmdRefDat.Enabled = !CmdRefDat.Enabled;
        }
        void Modificar()
        {
            if (DgLista.RowCount == 0)
            {
                MessageBox.Show("¡ No hay registros que modificar ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            FgItems.Cols[2].ComboList = "...";
            FgItems.Rows.Count = ((FgItems.Rows.Count - 2) + (n_NumFilasDocumento - (FgItems.Rows.Count - 2)));
            CmdBusCli.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            if (DgLista.RowCount == 0)
            {
                MessageBox.Show("¡ No hay registros que eliminar ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }

            int n_esfacturado = Convert.ToInt32(funFunciones.NulosN(funDatos.DataTableBuscar(dtRegistros, "n_id", "n_iddocven", intIdRegistro.ToString(), "N")));

            if (n_esfacturado != 0)
            {
                MessageBox.Show("¡ No se pudo eliminar esta guia, la guia ya fue facturada, elimine la factura e intente d e nuevo ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                BE_VTA_GUIAS e_Guia = new BE_VTA_GUIAS();
                objRegistros.mysConec = mysConec;
                e_Guia = objRegistros.TraerRegistro(intIdRegistro);
                if (objRegistros.Eliminar(intIdRegistro, e_Guia.n_tipori) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 1);
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

            objRegistros.LstDetalle = LstDetalle;    // ASIGNAMOS EL DETALLE DE LA GUIA

            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(BE_ListaReg, LstGuiaDoc, e_GuiaDatos);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(BE_ListaReg, LstGuiaDoc, e_GuiaDatos);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            BE_ListaReg.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_ListaReg.n_id = BE_Registro.n_id;
            BE_ListaReg.n_idcli = Convert.ToInt16(LblIdCliente.Text);
            BE_ListaReg.n_idtipdoc = 10;
            BE_ListaReg.c_numser = TxtNumSer.Text;
            BE_ListaReg.c_numdoc = TxtNumDoc.Text;
            BE_ListaReg.d_fchdoc = Convert.ToDateTime(TxtFchEmiDoc.Text);
            BE_ListaReg.n_idemptra = Convert.ToInt16(CboTra.SelectedValue);
            BE_ListaReg.n_idmottra = Convert.ToInt16(CboMotTras.SelectedValue);
            BE_ListaReg.c_numordcom = TxtOrdCom.Text;

            if (OptConDoc.Checked == true)
            {
                BE_ListaReg.n_idtipdocref = 1;
                BE_ListaReg.n_iddocref = Convert.ToInt32(LblIdOC.Text);
            }
            else
            {
                BE_ListaReg.n_idtipdocref = 0;
                BE_ListaReg.n_iddocref = 0;
            }

            if (TxtFchEmiOC.Text != " ")
            {
                BE_ListaReg.d_fchpeddocref = Convert.ToDateTime(TxtFchEmiOC.Text);
            }
            else
            {
                BE_ListaReg.d_fchpeddocref = null;
            }

            if (TxtFchEntOC.Text != " ")
            {
                BE_ListaReg.d_fchentdocref = Convert.ToDateTime(TxtFchEntOC.Text);
            }
            else
            {
                BE_ListaReg.d_fchentdocref = null;
            }
            
            BE_ListaReg.n_idpunvencli = Convert.ToInt16(CboPunVen.SelectedValue);
            BE_ListaReg.c_dirpunlle = TxtLugEnt.Text;
            BE_ListaReg.n_idpunlle = Convert.ToInt16(CboPunVen.SelectedValue);
            BE_ListaReg.c_dirpunpar = TxtLugPar.Text;
            BE_ListaReg.n_idpunpar = Convert.ToInt16(CboPunPar.SelectedValue);
            BE_ListaReg.n_idemptra = Convert.ToInt16(CboTra.SelectedValue);
            BE_ListaReg.n_idcho = Convert.ToInt16(CboCho.SelectedValue);
            BE_ListaReg.n_idvehtra = Convert.ToInt16(CboVeh.SelectedValue);
            BE_ListaReg.n_idano = STU_SISTEMA.ANOTRABAJO;
            BE_ListaReg.n_idmes = STU_SISTEMA.MESTRABAJO;
            if (OptSinDoc.Checked == true) { BE_ListaReg.n_tipgui = 1; }                      // INDICAMOS QUE ES DE TIPO 1 GUIA SIN DOCUMENTO
            if (OptConDoc.Checked == true) { BE_ListaReg.n_tipgui = 2; }                      // INDICAMOS QUE ES DE TIPO 2 GUIA CON DOCUMENTO
            BE_ListaReg.n_tipori = 1;
            BE_ListaReg.n_anulado = 1;

            BE_ListaReg.n_idtipdocref = Convert.ToInt16(CboTipPed.SelectedValue);
            BE_ListaReg.n_iddocref = Convert.ToInt32(LblIdOC.Text);
            BE_ListaReg.c_numdocref = TxtOrdCom.Text;
            BE_ListaReg.c_numordcom = TxtOrdCom.Text;
            BE_ListaReg.d_fchpeddocref = Convert.ToDateTime(TxtFchEmiOC.Text);
            BE_ListaReg.d_fchentdocref = Convert.ToDateTime(TxtFchEntOC.Text);

            int n_fila = 0;
            DataTable DtFiltro = new DataTable();
            string c_tipoexis = "";
            string strCadenaFiltro = "";
            string c_nomitem = "";
            string c_presendes = "";
            List<BE_VTA_GUIASDET> LstDetalleTmp = new List<BE_VTA_GUIASDET>();

            if (FgItems.Rows.Count > 2)
            {
                for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
                {
                    if (funFunciones.NulosC(FgItems.GetData(n_fila, 1)) != "")
                    {
                        BE_VTA_GUIASDET BE_Detalle = new BE_VTA_GUIASDET();

                        c_tipoexis = FgItems.GetData(n_fila, 1).ToString();
                        c_nomitem = FgItems.GetData(n_fila, 2).ToString();
                        c_presendes = FgItems.GetData(n_fila, 3).ToString();

                        BE_Detalle.n_idgui = BE_ListaReg.n_id;
                        BE_Detalle.n_canpro = Convert.ToDouble(FgItems.GetData(n_fila, 4).ToString());
                        if (funFunciones.NulosC(FgItems.GetData(n_fila, 5)) == "")
                        {
                            BE_Detalle.c_numlot = "";
                        }
                        else
                        {
                            BE_Detalle.c_numlot = FgItems.GetData(n_fila, 5).ToString();
                        }

                        // FILTRAMOS EL TIPO DE PRODUCTO PARA OBTENER SU id
                        strCadenaFiltro = "c_des = '" + c_tipoexis + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtTipoExis, strCadenaFiltro);
                        BE_Detalle.n_idtipexi = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        // FILTRAMOS EL ITEM DE LA FILA PARA OBTENER EL ID  
                        strCadenaFiltro = "c_despro = '" + c_nomitem + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                        BE_Detalle.n_idite = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());
                                            
                        // FILTRAMOS LA PRESENTACION PARA OBTENER SU ID
                        strCadenaFiltro = "c_abrpre = '" + c_presendes + "' AND n_idite = " + BE_Detalle.n_idite + "";
                        DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                        BE_Detalle.n_idunimed = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        LstDetalleTmp.Add(BE_Detalle);
                    }
                }
                LstDetalle = LstDetalleTmp;
            }
        }
        bool CamposOK()
        {
            int n_fila = 0;
            bool booEstado = true;

            if (LblIdCliente.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (TxtNumSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (TxtFchEmiDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de emision !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (Convert.ToInt16(CboMotTras.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el motivo de traslado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (TxtOrdCom.Text != "" )
            {
                if (TxtFchEmiOC.Text == "")
                {
                    MessageBox.Show("¡ No ha especificado la fecha de emision de la orden de compra !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    return booEstado;
                }
                if (TxtFchEntOC.Text == "")
                {
                    MessageBox.Show("¡ No ha especificado la fecha de entrega de la orden de compra !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    return booEstado;
                }
            }
            if (Convert.ToInt16(CboTra.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la empresa de tranporte !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (Convert.ToInt16(CboCho.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombe del chofer !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (Convert.ToInt16(CboVeh.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el vehiculo de transporte !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            string c_dato = "";
            for (n_fila=2; n_fila <= FgItems.Rows.Count-1; n_fila++)
            {
                if (funFunciones.NulosC(FgItems.GetData(n_fila, 2)).ToString() != "")
                {
                    c_dato = FgItems.GetData(n_fila, 2).ToString();

                    if (funFunciones.NulosC(FgItems.GetData(n_fila, 4)).ToString() == "")
                    {
                        MessageBox.Show("¡ No ha especificado la cantidad, para el item " + c_dato + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        booEstado = false;
                        return booEstado;
                    }

                    if (funFunciones.NulosC(FgItems.GetData(n_fila, 5)).ToString() == "")
                    {
                        MessageBox.Show("¡ No ha especificado el numero de lote, para el item " + c_dato + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        booEstado = false;
                        return booEstado;
                    }
                }
            }

            return booEstado;
        }
        private void FrmManGuias_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (dtRegistros.Rows.Count == 0)
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
            
        }
        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            if (Grabar() == true)
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objRegistros.mysConec = mysConec;
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 1);
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
            dtRegistros = null;

            objFormVis = null;

            this.Close();
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
                dtResult = funDbGrid.DG_Filtrar(dtRegistros, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
        }
        private void FrmManGuias_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
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
        private void TxtOrdCom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtLugEnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtNumBre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtNumPla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void FgItems_Click(object sender, EventArgs e)
        {

        }
        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
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
                FgItems.Select(FgItems.Row - 1, 3);
                return;
            }
            if (FgItems.Col == 3)
            {
                FgItems.Select(FgItems.Row - 1, 4);
                return;
            }
            if (FgItems.Col == 4)
            {
                FgItems.Select(FgItems.Row, 1);
                return;
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
                //// OBTNEMOS LA DESCRIPCION DEL TIPO DE PRODUCTO
                strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 1));
                FgItems.Cols[2].ComboList = "...";
       
                //// OBTENEMOS EL ID DEL TIPO DE PRODUCTO
                //dtResul = funDatos.DataTableFiltrar(dtTipoExis, "c_des = '" + strDesTipPro + "'");
                //if (dtResul.Rows.Count != 0)
                //{
                //    n_idtipproducto = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());

                //    // FILTRAMOS LOS ITEMS DEL TIPO DE PRODUCTO SELECCIONADO
                //    dtResul = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + n_idtipproducto + "");
                //    //funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 2);    // ITEMS
                //}
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
            FgItems.AllowEditing = true;
        }
        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 4)
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

            //if (FgItems.Row >= 2)
            //{
            //    if (funFunciones.NulosC(FgItems.GetData(FgItems.Row - 1, 1)) == "") { FgItems.AllowEditing = false; return; }
            //}

            //funFlex.FlexColumnaCombo(FgItems, dtTipoExis, "c_des", 1);    // TIPO DE PRODUCTO 
        }
        private void CboPunVen_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();

            if (CboPunVen.SelectedValue != null)
            {
                string strCadenaFiltro = "n_id = " + CboPunVen.SelectedValue.ToString() + "";
            
                DtFiltro = funDatos.DataTableFiltrar(dtPunVen, strCadenaFiltro);
                TxtLugEnt.Text = DtFiltro.Rows[0]["c_dir"].ToString();
            }
        }
        private void CboCho_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();

            if (CboCho.SelectedValue != null)
            {
                string strCadenaFiltro = "n_id = " + CboCho.SelectedValue.ToString() + "";

                DtFiltro = funDatos.DataTableFiltrar(dtChof, strCadenaFiltro);
                TxtNumBre.Text = DtFiltro.Rows[0]["c_numbre"].ToString();
            }
        }
        private void CboVeh_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();

            if (CboVeh.SelectedValue != null)
            {
                string strCadenaFiltro = "n_id = " + CboVeh.SelectedValue.ToString() + "";

                DtFiltro = funDatos.DataTableFiltrar(dtVehi, strCadenaFiltro);
                TxtNumPla.Text = DtFiltro.Rows[0]["c_numpla"].ToString();
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
        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text != "")
            {
                string strCad = "0000000000" + TxtNumDoc.Text;
                TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
            }
        }
        private void TxtFchEmiDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                TxtFchEmiOC.CustomFormat = " ";
                TxtFchEmiDoc.Format = DateTimePickerFormat.Custom;
            }
        }
        private void TxtOrdCom_Validated(object sender, EventArgs e)
        {
            if (TxtOrdCom.Text == "")
            {
                TxtFchEmiOC.CustomFormat = " ";
                TxtFchEmiDoc.Format = DateTimePickerFormat.Custom;

                TxtFchEntOC.CustomFormat = " ";
                TxtFchEntOC.Format = DateTimePickerFormat.Custom;

                FgItems.Rows.Count = 2;
            }
            else
            {
                TxtFchEmiOC.CustomFormat = "dd/MM/yyyy";
                TxtFchEmiDoc.Format = DateTimePickerFormat.Custom;

                TxtFchEntOC.CustomFormat = "dd/MM/yyyy";
                TxtFchEntOC.Format = DateTimePickerFormat.Custom;
            }
        }
        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 1);
            
            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            //MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);

            ListarItems();

            if (dtRegistros.Rows.Count == 0)
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
        private void ToolImprimir_ButtonClick(object sender, EventArgs e)
        {
            CN_vta_guias objAlm = new CN_vta_guias();
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            objAlm.STU_SISTEMA = STU_SISTEMA;
            objAlm.mysConec = mysConec;
            objAlm.ReportImprimirGuia(intIdRegistro); 
        }

        private void emitirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_vta_guias objAlm = new CN_vta_guias();
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            objAlm.STU_SISTEMA = STU_SISTEMA;
            objAlm.mysConec = mysConec;
            objAlm.ReportImprimirGuia(intIdRegistro);
        }
        private void guiasDelMesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_vta_guias objAlm = new CN_vta_guias();
            //int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            objAlm.STU_SISTEMA = STU_SISTEMA;
            objAlm.ReportGuiasMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void CboPunPar_SelectedValueChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (Convert.ToInt16(CboPunPar.SelectedValue) == 0) { return; }

            TxtLugPar.Text = funDatos.DataTableBuscar(dtPunPar, "n_id", "c_dir", Convert.ToInt16(CboPunPar.SelectedValue).ToString(), "N").ToString();
        }

        private void ToolHerramientas_Resize(object sender, EventArgs e)
        {

        }
        private void eliminarGuiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }
        private void anularRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            if (DgLista.RowCount == 0)
            {
                MessageBox.Show("¡ No hay registros que anular ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            int n_esfacturado = Convert.ToInt32(funFunciones.NulosN(funDatos.DataTableBuscar(dtRegistros, "n_id", "n_iddocven", intIdRegistro.ToString(), "N")));

            if (n_esfacturado != 0)
            {
                MessageBox.Show("¡ No se pudo anular esta guia, la guia ya fue facturada, elimine la factura e intente de nuevo ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            DialogResult Rpta = MessageBox.Show("Esta seguro de anular el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (DialogResult.Yes == Rpta)
            {
                objRegistros.mysConec = mysConec;
                if (objRegistros.CambiarEstado(intIdRegistro, 2) == true)
                {
                    objRegistros.mysConec = mysConec;
                    dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 1);
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo anular el registro por el siguiente motivo ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }
        private void guiasFacturadasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void guiasPendientesDeFacturacionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void CmdBusCli_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            objCliPro.mysConec = mysConec;
            dtResult = objCliPro.BuscarCliPro(dtCliPro, 1, "n_id", "");
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtNumRuc.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                    LblIdCliente.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtCliente.Text = dtResult.Rows[0]["c_nombre"].ToString();

                    dtResult = funDatos.DataTableFiltrar(dtPunVen, "n_idcli = "+ LblIdCliente.Text +"");
                    funDatos.ComboBoxCargarDataTable(CboPunVen, dtResult, "n_id", "c_des");
                }
                else
                {
                    TxtNumRuc.Text = "";
                    LblIdCliente.Text = "";
                    TxtCliente.Text = "";
                }
            }
        }
        private void FgItems_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgItems.Col == 2)
            {
                int n_idtipexi = 0;
                DataTable dtResul = new DataTable();
                string c_dato = "";

                booAgregando = true;

                // OBTNEMOS LA DESCRIPCION DEL TIPO DE PRODUCTO
                string c_DesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 1));

                if (c_DesTipPro == "")
                {
                    dtResul = dtItems;   // SI NO SELECCIONO UN TIPO DE PRODUCTO MOSTRAMOS TODOS LOS ITEMS
                }
                else
                {
                    dtResul = funDatos.DataTableFiltrar(dtTipoExis, "c_des = '" + c_DesTipPro + "'");
                    if (dtResul.Rows.Count != 0)
                    {
                        dtResul = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + dtResul.Rows[0]["n_id"].ToString() + "");
                    }
                }
                
                dtResul = objItems.BuscarItem("", "n_id", dtResul, n_idtipexi);
                if (dtResul != null)
                { 
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["c_despro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                        FgItems.SetData(FgItems.Row, 2, c_dato);

                        c_dato = dtResul.Rows[0]["c_abrpre"].ToString();        // MOSTRAMOS LA PRESENTACION DEL ITEM
                        FgItems.SetData(FgItems.Row, 3, c_dato);

                        //c_dato = dtResul.Rows[0]["n_id"].ToString();            // MOSTRAMOS EL ID DEL ITEM
                        //FgItems.SetData(FgItems.Row, 9, c_dato);

                        //c_dato = dtResul.Rows[0]["n_idunimed"].ToString();      // MOSTRAMOS LA UNIDAD DE MEDIDA DEL ITEM
                        //FgItems.SetData(FgItems.Row, 10, c_dato);
                    }
                }

                booAgregando = false;
            }
        }
        private void FgItems_KeyUp(object sender, KeyEventArgs e)
        {
            if (FgItems.Rows.Count == 2) { return; }
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }
            if (booAgregando == true) { return; }

            if (e.KeyCode.ToString() == "Delete")
            { 
                FgItems.RemoveItem(FgItems.Row);
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
            dtresul = funDatos.DataTableFiltrar(dtCliPro, "c_numdoc = '" + TxtNumRuc.Text + "'");

            if (dtresul.Rows.Count != 0)
            {
                TxtCliente.Text = dtresul.Rows[0]["c_nombre"].ToString();
                LblIdCliente.Text = dtresul.Rows[0]["n_id"].ToString();
            }
            else
            {
                TxtNumRuc.Text = "";
                TxtCliente.Text = "";
                LblIdCliente.Text = "";
                //TxtNumRuc.Focus();
            }
        }
        private void TxtAnuNumSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000" + TxtAnuNumSer.Text;

                TxtAnuNumSer.Text = strCad.Substring(strCad.Length - 4, 4);
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
        private void TxtAnuNumSer_Validated(object sender, EventArgs e)
        {
            if (TxtNumSer.Text != "")
            {
                string strCad = "0000" + TxtAnuNumSer.Text;

                TxtAnuNumSer.Text = "0" + strCad.Substring(strCad.Length - 3, 3);
                TxtAnuNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboTipDocAnu.SelectedValue), TxtNumSer.Text);
            }
        }
        private void TxtAnuNumDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000000000" + TxtAnuNumDoc.Text;

                TxtAnuNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
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
        private void TxtAnuNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text != "")
            {
                string strCad = "0000000000" + TxtAnuNumDoc.Text;
                TxtAnuNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
            }
        }

        private void CmdAcePan6_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(CboTipDocAnu.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtAnuNumSer.Focus();
                return;
            }
            if (TxtAnuNumSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie del documento ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtAnuNumSer.Focus();
                return;
            }
            if (TxtAnuNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero del documento ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtAnuNumDoc.Focus();
                return;
            }
            if (dateTimePicker1.Text == " ")
            {
                MessageBox.Show("¡ No ha especificado la fecha de emision del documento ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                dateTimePicker1.Focus();
                return;
            }

            bool b_result = false;
            string c_fchreg = "01/" + Convert.ToInt16(CboMeses.SelectedValue).ToString("00") + "/" + STU_SISTEMA.ANOTRABAJO.ToString("0000");
            b_result = objRegistros.InsertarAnulado(STU_SISTEMA.EMPRESAID, 0, Convert.ToInt16(CboTipDocAnu.SelectedValue), Convert.ToDateTime(dateTimePicker1.Text), Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, TxtAnuNumSer.Text, TxtAnuNumDoc.Text, 1);
            if (b_result == false)
            {
                MessageBox.Show("¡ No se pudo emitir el documento anulado por el siguiente motivo :" + objRegistros.StrErrorMensaje + " ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtAnuNumSer.Focus();
                return;
            }
            else
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objRegistros.mysConec = mysConec;
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 1);
                //DialogResult Rpta;
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();

                //// VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                //DataTable dtResul = new DataTable();
                //objRegistros.mysConec = mysConec;
                //dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
                //dtResul = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
                //dtRegistros = dtResul;
                //// MOSTRAMOS LOS DATOS EN LA GRILLA
                //ListarItems();
            }
            CmdSalPan6_Click(sender, e);
        }
        private void CmdSalPan6_Click(object sender, EventArgs e)
        {
            Tab1.SelectedIndex = 0;
            Tab1.Enabled = true;
            ToolHerramientas.Enabled = true;
            panel6.Visible = false;
        }
        private void emitirComoAnuladoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmitirAnulado();
        }
        void EmitirAnulado()
        {
            panel6.Left = ((this.Width - panel6.Width) / 2);
            panel6.Top = ((this.Height - panel6.Height) / 2);
            Tab1.SelectedIndex = 0;
            ToolHerramientas.Enabled = false;
            Tab1.Enabled = false;
            TxtAnuNumSer.Text = "";
            TxtAnuNumDoc.Text = "";
            TxtAnuNumSer.Focus();
            dateTimePicker1.Text = "";
            CboTipDocAnu.SelectedItem = 0;
            panel6.Visible = true;
        }
        private void OptConDoc_CheckedChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            CmdBusOC.Enabled = true;
            CmdCarPedPen.Enabled = true;
            CboTipPed.Enabled = true;
        }
        private void OptSinDoc_CheckedChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            OptSinDoc.Enabled = true;
            CboTipPed.Enabled = false;
            FgItems.Rows.Count = 2;
            CmdBusOC.Enabled = false;
            CmdCarPedPen.Enabled = false;
            LblIdOC.Text = "";
            TxtOrdCom.Text = "";

            TxtFchEmiOC.Text = "";
            TxtFchEmiOC.CustomFormat = " ";
            TxtFchEmiOC.Format = DateTimePickerFormat.Custom;

            TxtFchEntOC.Text = "";
            TxtFchEntOC.CustomFormat = " ";
            TxtFchEntOC.Format = DateTimePickerFormat.Custom;

            LstGuiaDoc.Clear();
        }
        private void CmdBusOC_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            if (Convert.ToInt16(funFunciones.NulosN(LblIdCliente.Text)) == 0)
            {
                MessageBox.Show("¡ Debe de indicar el nombre del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdBusCli.Focus();
                return;
            }

            DataTable dtResul = new DataTable();
            CN_vta_pedidocli objpedcli = new CN_vta_pedidocli();
            objpedcli.mysConec = mysConec;
            dtResul = objpedcli.PedidosConSaldo(STU_SISTEMA.EMPRESAID, Convert.ToInt16(LblIdCliente.Text));
                        
            if (dtResul != null)
            { 
                if (dtResul.Rows.Count != 0)
                {
                    TxtOrdCom.Text = dtResul.Rows[0]["c_numordcom"].ToString();
                    LblIdOC.Text = dtResul.Rows[0]["n_id"].ToString();

                    TxtFchEmiOC.Text = "";
                    TxtFchEmiOC.CustomFormat = "dd/MM/yyyy";
                    TxtFchEmiOC.Format = DateTimePickerFormat.Custom;

                    TxtFchEntOC.Text = "";
                    TxtFchEntOC.CustomFormat = "dd/MM/yyyy";
                    TxtFchEntOC.Format = DateTimePickerFormat.Custom;

                    TxtFchEmiOC.Text = Convert.ToDateTime(dtResul.Rows[0]["d_fchped"]).ToString("dd/MM/yyyy");
                    TxtFchEntOC.Text = Convert.ToDateTime(dtResul.Rows[0]["d_fchent"]).ToString("dd/MM/yyyy");

                    CmdCarPedPen_Click(sender, e);
                    //MostrarItem(Convert.ToInt32(LblIdOC.Text));
                }

                // AGREGAMOS LOS DOCUMENTOS ASIGNADOS A LA GUIA
                LstGuiaDoc.Clear();
                BE_VTA_GUIASDOC e_GuiaDoc = new BE_VTA_GUIASDOC();

                e_GuiaDoc.n_idgui = 0;
                e_GuiaDoc.n_iddoc = Convert.ToInt32(funFunciones.NulosN( LblIdOC.Text));
                e_GuiaDoc.n_idtipdoc = 77;
                e_GuiaDoc.c_numdoc = TxtOrdCom.Text;
                LstGuiaDoc.Add(e_GuiaDoc);
            }
        }
        void MostrarItem(int n_IdPedido)
        {
            int n_fila = 0;
            int n_row = 2;
            int n_idTipExi = 0;
            int n_idIte = 0;
            int n_idUniMed = 0;
            string strCadenaFiltro = "";
            DataTable DtFiltro = new DataTable();
            List<BE_VTA_PEDIDOCLIDET> lstPedDet = new List<BE_VTA_PEDIDOCLIDET>();
            DataTable dtresult = new DataTable();
            CN_vta_pedidocli objped = new CN_vta_pedidocli();
            objped.mysConec = mysConec;

            FgItems.Rows.Count = 2;
            if (objped.TraerRegistro(n_IdPedido) == true)
            {
                lstPedDet = objped.lstPedDet;
                booAgregando = true;

                for (n_fila = 0; n_fila <= lstPedDet.Count - 1; n_fila++ )
                {
                    FgItems.Rows.Count = FgItems.Rows.Count + 1;

                    n_idTipExi = 2;
                    n_idIte = lstPedDet[n_fila].n_idite;
                    n_idUniMed = lstPedDet[n_fila].n_idunimed;

                    // MOSTRAMOS EL TPO DE EXIXTENCIA
                    //strCadenaFiltro = "n_id = " + n_idTipExi + "";
                    //DtFiltro = funDatos.DataTableFiltrar(dtTipoExis, strCadenaFiltro);
                    FgItems.SetData(n_row, 1, DtFiltro.Rows[0]["c_codpro"].ToString());

                    // MOSTRAMOS EL ITEM
                    strCadenaFiltro = "n_id = " + n_idIte + "";
                    DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                    FgItems.SetData(n_row, 2, DtFiltro.Rows[0]["c_despro"].ToString());

                    // MOSTRAMOS LA UNIDAD DE MEDIDA
                    strCadenaFiltro = "n_idite = " + n_idIte + "";
                    DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                    FgItems.SetData(n_row, 3, DtFiltro.Rows[0]["c_abrpre"].ToString());

                    FgItems.SetData(n_row, 4, lstPedDet[n_fila].n_can);
                    //FgItems.SetData(n_fila, 5, element.c_numlot);

                    //n_fila++;
                    n_row++;
                }
                booAgregando = false;
            }
        }
        private void TxtOrdCom_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                TxtOrdCom.Text = "";
                
                TxtFchEmiOC.Text = "";
                TxtFchEmiOC.CustomFormat = " ";
                TxtFchEmiOC.Format = DateTimePickerFormat.Custom;

                TxtFchEntOC.Text = "";
                TxtFchEntOC.CustomFormat = " ";
                TxtFchEntOC.Format = DateTimePickerFormat.Custom;

                FgItems.Rows.Count = 2;
            }
        }
        private void CmdCarPedPen_Click(object sender, EventArgs e)
        {
            if ((LblIdOC.Text) == "")
            {
                MessageBox.Show("¡ No ha seleccionado la orden de compra del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdBusOC.Focus();
                return;
            }
            int n_fila = 0;
            int n_idTipExi = 0;
            int n_idIte =0;
            int n_idUniMed = 0;
            string strCadenaFiltro = "";
            DataTable DtFiltro = new DataTable();
            DataTable dtResul = new DataTable();
            CN_vta_pedidocli objpedcli = new CN_vta_pedidocli();
            objpedcli.mysConec = mysConec;
            objpedcli.SeleccionarEntregas(Convert.ToInt32(LblIdOC.Text), true);
            dtResul = objpedcli.dtLista;
            
            FgItems.Rows.Count = 2;
            booAgregando = true;
            if (dtResul != null)
            {
                if (dtResul.Rows.Count != 0)
                {
                    for (n_fila = 0; n_fila <= dtResul.Rows.Count - 1; n_fila++)
                    {
                        FgItems.Rows.Count = FgItems.Rows.Count + 1;

                        n_idTipExi = 2;
                        n_idIte = Convert.ToInt32(dtResul.Rows[n_fila]["n_idite"]);
                        n_idUniMed = Convert.ToInt32(dtResul.Rows[n_fila]["n_idunimed"]);

                        // MOSTRAMOS EL TPO DE EXIXTENCIA
                        strCadenaFiltro = "n_id = " + n_idTipExi + "";
                        DtFiltro = funDatos.DataTableFiltrar(dtTipoExis, strCadenaFiltro);
                        FgItems.SetData(FgItems.Rows.Count - 1, 1, DtFiltro.Rows[0]["c_des"].ToString());

                        // MOSTRAMOS EL ITEM
                        strCadenaFiltro = "n_id = " + n_idIte + "";
                        DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                        FgItems.SetData(FgItems.Rows.Count - 1, 2, DtFiltro.Rows[0]["c_despro"].ToString());

                        // MOSTRAMOS LA UNIDAD DE MEDIDA
                        strCadenaFiltro = "n_idite = " + n_idIte + "";
                        DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                        FgItems.SetData(FgItems.Rows.Count - 1, 3, DtFiltro.Rows[0]["c_abrpre"].ToString());

                        FgItems.SetData(FgItems.Rows.Count - 1, 4, Convert.ToDouble(dtResul.Rows[n_fila]["n_saldo"]).ToString("0.00"));
                        //FgItems.SetData(n_fila, 5, element.c_numlot);

                    }
                    
                }
            }
            booAgregando = false;
        }
        private void CmdDelIte_Click(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count != 0)
            {
                FgItems.RemoveItem(FgItems.Row);
            }
        }
        private void CmdAddIte_Click(object sender, EventArgs e)
        {
            if (OptSinDoc.Checked == true)
            {
                AgregarItem(1);
            }
            else
            {
                AgregarItem(2);
            }
        }
        void AgregarItem(int n_tipo)
        {
            if (OptConDoc.Checked==true)
            { 
                if (Convert.ToInt32(funFunciones.NulosN(LblIdOC.Text)) == 0)
                {
                    MessageBox.Show("¡ No ha seleccionado la orden de compra del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    CmdBusOC.Focus();
                    return;
                }
            }
            int n_idtipexi = 0;
            DataTable dtResul = new DataTable();
            DataTable dtResul2 = new DataTable();
            string c_dato = "";
            string c_cadin = "";

            booAgregando = true;

            if (n_tipo == 1)
            {
                dtResul = dtItems;
            }
            else
            {
                c_cadin = ObtenerItemOC(Convert.ToInt32(LblIdOC.Text));
                if (c_cadin != "")
                { 
                    dtResul = funDatos.DataTableFiltrar(dtItems,"n_id IN "+ c_cadin + "", "c_despro");
                }
            }
            dtResul = objItems.BuscarItem("", "n_id", dtResul, n_idtipexi);
            if (dtResul != null)
            {
                if (dtResul.Rows.Count != 0)
                {
                    FgItems.Rows.Count = FgItems.Rows.Count + 1;
                    c_dato = dtResul.Rows[0]["n_idtipexi"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                    dtResul2 = funDatos.DataTableFiltrar(dtTipoExis, "n_id ='" + c_dato + "'", "c_des");
                    c_dato = dtResul2.Rows[0]["c_des"].ToString();
                    FgItems.SetData(FgItems.Rows.Count - 1, 1, c_dato);

                    c_dato = dtResul.Rows[0]["c_despro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                    FgItems.SetData(FgItems.Rows.Count - 1, 2, c_dato);

                    c_dato = dtResul.Rows[0]["c_abrpre"].ToString();        // MOSTRAMOS LA PRESENTACION DEL ITEM
                    FgItems.SetData(FgItems.Rows.Count - 1, 3, c_dato);
                }
            }

            booAgregando = false;
        }
        string ObtenerItemOC(int n_idpedidocliente)
        {
            string c_cad = "";
            int n_fil = 0;
            DataTable dtresult = new DataTable();
            CN_vta_pedidocli objpedcli = new CN_vta_pedidocli();
            objpedcli.mysConec = mysConec;
            objpedcli.MostrarEntregas(Convert.ToInt32(LblIdOC.Text));
            dtresult = objpedcli.dtLista;
            dtresult = funDatos.DataTableFiltrar(dtresult, "n_saldo > 0");
            if (dtresult.Rows.Count != 0)
            { 
                c_cad = "(";
                for (n_fil = 0; n_fil <= dtresult.Rows.Count - 1; n_fil++)
                {
                    if (n_fil == 0)
                    { 
                    }
                    else
                    { 
                        c_cad = c_cad + ", ";
                    }
                    c_cad = c_cad + Convert.ToInt32(dtresult.Rows[n_fil]["n_idite"]);
                }
                c_cad = c_cad + ")";
            }
            return c_cad;
        }

        private void CmdRefDat_Click(object sender, EventArgs e)
        {
            CargarDatosAdicionales();
            MessageBox.Show("¡ Los datos se actualizaron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void CmdSalPan5_Click(object sender, EventArgs e)
        {
            Tab1.SelectedIndex = 0;
            Tab1.Enabled = true;
            ToolHerramientas.Enabled = true;
            panel5.Visible = false;
        }

        private void CmdAcePan5_Click(object sender, EventArgs e)
        {
            if (TxtDANumSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie para la factura ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtDANumSer.Focus();
                return;
            }
            if (TxtDANumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de la factura ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtDANumDoc.Focus();
                return;
            }

            if (e_GuiaDatos == null) { e_GuiaDatos = new BE_VTA_GUIASDATOS();}
            e_GuiaDatos.n_idgui = 0;
            e_GuiaDatos.c_facnumdoc = TxtDANumDoc.Text;
            e_GuiaDatos.c_facnumser = TxtDANumSer.Text;

            CmdSalPan5_Click(sender, e);
        }

        private void TxtDANumSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000" + TxtDANumSer.Text;

                TxtDANumSer.Text = strCad.Substring(strCad.Length - 4, 4);
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

        private void TxtDANumSer_Validated(object sender, EventArgs e)
        {
            if (TxtDANumSer.Text != "")
            {
                string strCad = "0000" + TxtDANumSer.Text;

                TxtDANumSer.Text = "0" + strCad.Substring(strCad.Length - 3, 3);
                //TxtDANumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboTipDocAnu.SelectedValue), TxtNumSer.Text);
            }
        }

        private void TxtDANumDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000000000" + TxtDANumDoc.Text;

                TxtDANumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
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

        private void TxtDANumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text != "")
            {
                string strCad = "0000000000" + TxtDANumDoc.Text;
                TxtDANumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
            }
        }

        private void CmdDatAdi_Click(object sender, EventArgs e)
        {
            panel5.Left = ((this.Width - panel5.Width) / 2);
            panel5.Top = ((this.Height - panel5.Height) / 2);
            //Tab1.SelectedIndex = 0;
            ToolHerramientas.Enabled = false;
            Tab1.Enabled = false;
            if (e_GuiaDatos == null)
            {
                TxtDANumSer.Text = "";
                TxtDANumDoc.Text = "";
            }
            else
            { 
                TxtDANumSer.Text = e_GuiaDatos.c_facnumser;
                TxtDANumDoc.Text = e_GuiaDatos.c_facnumdoc;
            }

            if (n_QueHace == 3)
            {
                CmdAcePan5.Enabled = false;
            }
            else 
            {
                CmdAcePan5.Enabled = true;
            }
            TxtDANumSer.Focus();
            panel5.Visible = true;
        }

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-VTA-GUIAS-" + STU_SISTEMA.ANOTRABAJO.ToString() + Convert.ToInt16(CboMeses.SelectedValue).ToString("00") + ".xls";
            DgLista.ExportTo(c_NomArchivo);
            MessageBox.Show("! Se exporto con exito la informacion en el archivo " + c_NomArchivo + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
    }
}
