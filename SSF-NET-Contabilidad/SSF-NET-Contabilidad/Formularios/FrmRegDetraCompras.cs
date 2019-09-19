using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using SIAC_Entidades.Logistica;
using SIAC_Entidades.Tesoreria;
using SIAC_Entidades.Ventas;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Logistica;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Sunat;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Tesoreria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmRegDetraCompras : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Tipo;                                                      // INDICA EL TIPO DE REGSTRO QUE SE ESTA GUARDANDO (1 = COMPRAS ;  2 = VENTAS)
        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_con_regdetracciones objRegistros = new CN_con_regdetracciones();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_con_detraccion objDet = new CN_con_detraccion();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_sun_tipmon objMon = new CN_sun_tipmon();
        CN_mae_clipro objPro = new CN_mae_clipro();
        CN_con_pc objpc = new CN_con_pc();
        CN_sun_tipope objtipope = new CN_sun_tipope();
        CN_tes_origen objOri = new CN_tes_origen();
        CN_tes_destino objDes = new CN_tes_destino();
        CN_tes_mediopago objMedPag = new CN_tes_mediopago();
        CN_sun_tipcam ObjTC = new CN_sun_tipcam();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();
        
        DataTable dtLista = new DataTable();
        DataTable dtPro = new DataTable();
        DataTable dtMon = new DataTable();
        DataTable dtMes = new DataTable();
        DataTable dtTipDoc = new DataTable();
        DataTable dtDet = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtCompras = new DataTable();
        DataTable dtMedPag = new DataTable();
        //DataTable dtCtaCon =new DataTable();
        DataTable dtOri = new DataTable();
        DataTable dtDes = new DataTable();
        DataTable dtTC = new DataTable();

        bool booAgregando;
        SIAC_Objetos.Funciones objFunciones = new SIAC_Objetos.Funciones();

        // ENTIDADES DE LA DETRACCION
        BE_CON_REGDETRACCIONES e_Detracciones = new BE_CON_REGDETRACCIONES();

        // ENTIDADES DE TESORERIA
        BE_TES_TESORERIA e_Tesoreria = new BE_TES_TESORERIA();
        List<BE_TES_TESORERIAORI> l_TesOri = new List<BE_TES_TESORERIAORI>();
        List<BE_TES_TESORERIADES> l_TesDes = new List<BE_TES_TESORERIADES>();
        List<BE_TES_TESORERIADESDET> l_TesDesDet = new List<BE_TES_TESORERIADESDET>();
        List<BE_TES_TESORERIAORIDET> l_TesOriDet = new List<BE_TES_TESORERIAORIDET>();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[13, 5];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL

        string c_Numerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string c_NumerovalidosFE = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZEF1234567890." + (char)8;                                        // + (char)8;
        string c_Caracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;

        bool booSeEjecuto = false;

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmRegDetraCompras()
        {
            InitializeComponent();
        }

        private void FrmRegDetraCompras_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void CargarCombos()
        {
            DataTableCargar();
            booAgregando = true;
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMes, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtTipDoc, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMon, dtMon, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboDet, dtDet, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipOpe, dtMedPag, "n_id", "c_des");
            //funDatos.ComboBoxCargarDataTable(CboDestino, dtDes, "n_id", "c_des");
            booAgregando = false;
        }
        void ConfigurarFormulario()
        {
            this.Height = 579;
            this.Width = 1015;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            //this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            if (n_Tipo == 1)
            {
                this.Text = "CONTABILIDAD - DETRACCIONES X COMPRAS";
            }
            if (n_Tipo == 2)
            {
                this.Text = "CONTABILIDAD - DETRACCIONES X VENTAS";
            }

            booAgregando = true;
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
            booAgregando = false;
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, n_Tipo);
            dtLista = objRegistros.dtLista;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(65, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(65);

            objMon.mysConec = mysConec;
            dtMon = objMon.Listar();

            objTipDoc.mysConec = mysConec;
            dtTipDoc = objTipDoc.Listar();

            objDet.mysConec = mysConec;
            objDet.Listar();
            dtDet = objDet.dtLista;

            objPro.mysConec = mysConec;

            if (n_Tipo == 1)
            { 
                dtPro = objPro.ListarProveedor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
            }
            if (n_Tipo == 2)
            {
                dtPro = objPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
            }

            objMeses.mysConec = mysConec;
            dtMes = objMeses.Listar();

            objOri.mysConec = mysConec;
            objOri.Listar(STU_SISTEMA.EMPRESAID);
            dtOri = objOri.dtOrigen;
            if (n_Tipo == 1) { dtOri = funDatos.DataTableFiltrar(dtOri, "n_tipo = 2"); }
            if (n_Tipo == 2) { dtOri = funDatos.DataTableFiltrar(dtOri, "n_tipo = 1"); }

            objDes.mysConec = mysConec;
            objDes.Listar(STU_SISTEMA.EMPRESAID);
            dtDes = objDes.dtDestino;
            if (n_Tipo == 1) { dtDes = funDatos.DataTableFiltrar(dtDes, "n_tipo = 2"); }
            if (n_Tipo == 2) { dtDes = funDatos.DataTableFiltrar(dtDes, "n_tipo = 1"); }

            objMedPag.mysConec = mysConec;
            dtMedPag = objMedPag.Listar();

            ObjTC.mysConec = mysConec;
            dtTC = ObjTC.Listartcano(151, STU_SISTEMA.ANOTRABAJO.ToString());
        }
        void ListarItems()
        {
            // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
            objRegistros.mysConec = mysConec;
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue), n_Tipo);
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
            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdRegistro);
            e_Detracciones = objRegistros.e_Detrac;

            booAgregando = true;

            if (n_Tipo == 1) { MostrarDatosDocCompra(e_Detracciones.n_iddoccom); }
            if (n_Tipo == 2) { MostrarDatosDocVentas(e_Detracciones.n_iddoccom); }

            LblIdDoc.Text = e_Detracciones.n_iddoccom.ToString();
            CboDet.SelectedValue = e_Detracciones.n_iddet;
            TxtTas.Text = e_Detracciones.n_por.ToString("0.00");
            TxtImpDet.Text = e_Detracciones.n_imp.ToString("0.00");
            TxtFchPag.Text = Convert.ToDateTime(e_Detracciones.d_fchpag).ToString("dd/MM/yyyy");
            TxtNumVou.Text = e_Detracciones.c_numdet;
            TxtGlo.Text = e_Detracciones.c_glosa;
            TxtTc2.Text = e_Detracciones.n_tc.ToString("0.000");

            LblIdDocCue.Text = e_Detracciones.n_idtesori.ToString("");
            if (LblIdDocCue.Text != "0")
            {
                TxtNumCta.Text = funDatos.DataTableBuscar(dtOri, "n_id", "c_cuecon", LblIdDocCue.Text, "N").ToString();
                LblDesCta.Text = funDatos.DataTableBuscar(dtOri, "n_id", "c_des", LblIdDocCue.Text, "N").ToString();
            }
            else
            {
                TxtNumCta.Text = "";
                LblDesCta.Text = "";
            }

            LblIdDestino.Text = e_Detracciones.n_iddes.ToString("");
            if (LblIdDestino.Text != "0")
            {
                TxtCtaDes.Text = funDatos.DataTableBuscar(dtDes, "n_id", "c_cuecon", LblIdDestino.Text, "N").ToString();
                LblDesDes.Text = funDatos.DataTableBuscar(dtDes, "n_id", "c_des", LblIdDestino.Text, "N").ToString();
            }
            else
            {
                TxtCtaDes.Text = "";
                LblDesDes.Text = "";
            }
            
            CboTipOpe.SelectedValue = e_Detracciones.n_idmedpag;

            if (e_Detracciones.n_aplipag == 1)
            {
                ChkAplPag.Checked = true;
            }
            else
            {
                ChkAplPag.Checked = false;
            }

            if (e_Detracciones.n_tippag == 1)
            {
                OptTipPagEfe.Checked = true;
                OptTipPagTra.Checked = false;
            }
            else
            {
                OptTipPagEfe.Checked = false;
                OptTipPagTra.Checked = true;
            }
            booAgregando = false;
        }
        void MostrarDatosDocVentas(int n_IdVenta)
        {
            BE_VTA_VENTAS entVen = new BE_VTA_VENTAS();
            CN_vta_ventas objVen = new CN_vta_ventas();
            objVen.mysConec = mysConec;
            entVen = objVen.TraerRegistro(n_IdVenta);
           
            if (objVen.booOcurrioError == false)
            {
                LblIdPro.Text = entVen.n_idcli.ToString();
                TxtPro.Text = funDatos.DataTableBuscar(dtPro, "n_id", "c_nombre", entVen.n_idcli.ToString(), "N").ToString();

                TxtNumDocCom.Text = entVen.c_numser + "-" + entVen.c_numdoc;
                CboTipDoc.SelectedValue = entVen.n_idtipdoc;
                TxtFchEmi.Text = entVen.d_fchdoc.ToString("dd/MM/yyyy");
                CboMon.SelectedValue = entVen.n_idmon;
                TxtTc.Text = entVen.n_tc.ToString("0.000");
                TxtImpDoc.Text = entVen.n_imptotven.ToString("0.00");
                if (entVen.n_idmon == 115)
                {
                    TxtImpExpMN.Text = entVen.n_imptotven.ToString("0.00");
                }
                else
                {
                    TxtImpExpMN.Text = (entVen.n_imptotven * entVen.n_tc).ToString("0.00");
                }
            }
        }
        void MostrarDatosDocCompra(int n_IdCompra)
        {
            BE_LOG_COMPRAS entCom = new BE_LOG_COMPRAS();
            CN_log_compras objLog = new CN_log_compras();
            objLog.mysConec = mysConec;
            if (objLog.TraerRegistro(n_IdCompra) == true)
            {
                entCom = objLog.e_Compras;
                                
                LblIdPro.Text = entCom.n_idpro.ToString();
                TxtPro.Text = funDatos.DataTableBuscar(dtPro, "n_id", "c_nombre", entCom.n_idpro.ToString(), "N").ToString();
                
                TxtNumDocCom.Text = entCom.c_numser + "-" + entCom.c_numdoc;
                CboTipDoc.SelectedValue = entCom.n_idtipdoc;
                TxtFchEmi.Text = entCom.d_fchdoc.ToString("dd/MM/yyyy");
                CboMon.SelectedValue = entCom.n_idmon;
                TxtTc.Text = entCom.n_tc.ToString("0.000");
                TxtImpDoc.Text = entCom.n_imptotcom.ToString("0.00");
                if (entCom.n_idmon == 115)
                {
                    TxtImpExpMN.Text = entCom.n_imptotcom.ToString("0.00");
                }
                else
                {
                    TxtImpExpMN.Text = (entCom.n_imptotcom * entCom.n_tc).ToString("0.00");
                }
            }
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
            OptTipPagEfe.Checked = true;
            TxtFchPag.Text = DateTime.Now.ToString();
            TxtTc2.Text = objFunciones.ObtenerTC(dtTC, TxtFchPag.Text).ToString("0.000");
            ChkAplPag.Checked = true;
            CmdBusCli.Focus();
        }
        void Blanquea()
        {
            TxtNumDocCom.Text = "";
            TxtTc.Text = "";
            TxtTc2.Text = "";
            TxtPro.Text = "";
            TxtImpDoc.Text = "";
            TxtImpExpMN.Text = "";
            TxtTas.Text = "";
            TxtImpDet.Text = "";

            funControl.dtpBlanquea(TxtFchPag);
            funControl.dtpBlanquea(TxtFchEmi);
            TxtNumVou.Text = "";
            TxtGlo.Text = "";

            LblIdDoc.Text = "";
            LblIdPro.Text = "";
            CboDet.SelectedValue = 0;
            //CboDestino.SelectedValue = 0;
            CboTipDoc.SelectedValue = 0;
            CboMon.SelectedValue = 0;

            CboTipOpe.SelectedValue = 0;
            TxtNumCta.Text = "";
            LblDesCta.Text = "";
            OptTipPagEfe.Checked = true;
            OptTipPagTra.Checked = false;
            LblIdDocCue.Text = "";

            LblDesDes.Text = "";
            LblIdDestino.Text = "";
            TxtCtaDes.Text = "";
        }
        void Bloquea()
        {
            CmdBusCli.Enabled = !CmdBusCli.Enabled;
            CmdBusDocCom.Enabled = !CmdBusDocCom.Enabled; 

            CboDet.Enabled = !CboDet.Enabled;
            //TxtTas.Enabled = !TxtTas.Enabled;
            TxtImpDet.Enabled = !TxtImpDet.Enabled;
            TxtFchPag.Enabled = !TxtFchPag.Enabled;
            TxtNumVou.Enabled = !TxtNumVou.Enabled;
            TxtGlo.Enabled = !TxtGlo.Enabled;

            CmdBusCta.Enabled = !CmdBusCta.Enabled;
            CboBusDes.Enabled = !CboBusDes.Enabled;
            //TxtNumCta.Enabled = !TxtNumCta.Enabled;
            //CboTipOpe.Enabled = !CboTipOpe.Enabled;

            OptTipPagTra.Enabled = !OptTipPagTra.Enabled;
            OptTipPagEfe.Enabled = !OptTipPagEfe.Enabled;
        }
        void MostrarEstadoMes(int n_IdEmpresa, int n_IdMes)
        {
            obj_fungen.mysConec = mysConec;
            if (obj_fungen.EstadoPeriodo(n_IdEmpresa, n_IdMes, 6) == true)
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
            ToolExportar.Enabled = !ToolExportar.Enabled;
            //ToolExportar.Enabled = !ToolExportar.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
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
            TxtTc.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int n_IdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            int n_IdTesoreria = Convert.ToInt16(funFunciones.NulosN(DgLista.Columns["n_idtes"].CellValue(DgLista.Row)));       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(n_IdRegistro, n_IdTesoreria) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

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

            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.e_Tesoreria = e_Tesoreria;
            objRegistros.l_TesOri = l_TesOri;
            objRegistros.l_TesDes = l_TesDes;
            objRegistros.l_TesDesDet = l_TesDesDet;
            objRegistros.l_TesOriDet = l_TesOriDet;

            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(e_Detracciones);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(e_Detracciones);
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
                e_Detracciones.n_id = 0;
            }
            else
            {
                e_Detracciones.n_id = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            }
                        
            e_Detracciones.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Detracciones.n_iddet = Convert.ToInt16(CboDet.SelectedValue);
            e_Detracciones.n_por = Convert.ToDouble(TxtTas.Text);
            e_Detracciones.n_iddoccom = Convert.ToInt16(LblIdDoc.Text);
            e_Detracciones.n_idmon = 115;
            e_Detracciones.n_tipmov = n_Tipo;   
            e_Detracciones.d_fchmov = Convert.ToDateTime(TxtFchPag.Text);
            e_Detracciones.c_glosa = TxtGlo.Text;
            e_Detracciones.n_imp = Convert.ToDouble(TxtImpDet.Text);
            e_Detracciones.c_numdet = TxtNumVou.Text;
            e_Detracciones.d_fchpag = Convert.ToDateTime(TxtFchPag.Text);
            e_Detracciones.n_idgrudet = 0;
            e_Detracciones.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_Detracciones.n_mes = Convert.ToInt16(CboMeses.SelectedValue);
            e_Detracciones.n_idlib = 33;
            e_Detracciones.c_numreg = "";
            e_Detracciones.n_tc = Convert.ToDouble(TxtTc2.Text);
            e_Detracciones.n_idtipdoc = 86;
            e_Detracciones.n_idtesori = Convert.ToInt16(LblIdDocCue.Text);
            e_Detracciones.n_idmedpag = Convert.ToInt16(CboTipOpe.SelectedValue);
            e_Detracciones.n_iddes = Convert.ToInt16(LblIdDestino.Text);

            if (OptTipPagEfe.Checked == true) { e_Detracciones.n_tippag = 1; }
            if (OptTipPagTra.Checked == true) { e_Detracciones.n_tippag = 2; } 
            
            if (ChkAplPag.Checked == true) { e_Detracciones.n_aplipag = 1; }
            if (ChkAplPag.Checked == false) { e_Detracciones.n_aplipag = 2; }
                
            // **********************************
            // ASIGNAMOS LOS DATOS PARA TESORERIA
            if (n_QueHace == 1)
            {
                e_Tesoreria.n_id = 0;
            }
            else
            {
                e_Tesoreria.n_id = e_Detracciones.n_idtes;
            }

            if (n_Tipo == 1) { AsignarEgreso(); }
            if (n_Tipo == 2) { AsignarIngreso(); }
        }
        void AsignarIngreso()
        {
            l_TesOri.Clear();
            l_TesDes.Clear();
            l_TesDesDet.Clear();
            l_TesOriDet.Clear();

            e_Tesoreria.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Tesoreria.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_Tesoreria.n_mes = Convert.ToInt16(CboMeses.SelectedValue);
            e_Tesoreria.n_idlib = 1;
            e_Tesoreria.c_numreg = "";
            e_Tesoreria.d_fchope = Convert.ToDateTime(TxtFchPag.Text);
            e_Tesoreria.n_idmon = Convert.ToInt16(CboMon.SelectedValue);
            e_Tesoreria.c_glo = "PAGO DETRACCION CLIENTE : " + TxtPro.Text + ",  " + CboTipDoc.Text + " Nº " + TxtNumDocCom.Text;
            e_Tesoreria.n_conciliado = 0;
            e_Tesoreria.n_tc = Convert.ToDouble(TxtTc2.Text);
            e_Tesoreria.n_tipreg = 1;
            e_Tesoreria.n_dongen = 2;                                     // INDICAMOS QUE EL REGISTRO SE GENERA FUERA DEL MODULO EGRESOS, INGRESO

            // *********************
            // CARGAMOS EL ORIGEN
            BE_TES_TESORERIAORI e_TesOri = new BE_TES_TESORERIAORI();
            int n_idmod = Convert.ToInt16(funFunciones.NulosN(funDatos.DataTableBuscar(dtOri, "n_id", "n_idmod", LblIdDocCue.Text, "N").ToString()));
            e_TesOri.n_idtes = 0;
            e_TesOri.n_idori = Convert.ToInt32(LblIdDocCue.Text);
            e_TesOri.n_imp = Convert.ToDouble(TxtImpDet.Text);
            e_TesOri.n_idmod = n_idmod;
            e_TesOri.n_idbcocta = 0;
            e_TesOri.n_tc = Convert.ToDouble(TxtTc.Text);
            l_TesOri.Add(e_TesOri);

            if (OptTipPagTra.Checked == true)
            {
                BE_TES_TESORERIAORIDET e_TesOriDet = new BE_TES_TESORERIAORIDET();
                //n_idmod = Convert.ToInt16(funDatos.DataTableBuscar(dtOri, "n_id", "n_idmod", LblIdDocCue.Text, "N"));

                e_TesOriDet.n_idtes = 0;
                e_TesOriDet.n_idori = Convert.ToInt32(LblIdDocCue.Text);                         // LE INDICAMOS QUE ES FACTURAS POR PAGAR
                e_TesOriDet.n_idtipper = 0;
                e_TesOriDet.n_idmod = n_idmod;
                e_TesOriDet.n_iddoc = Convert.ToInt32(LblIdDoc.Text);
                e_TesOriDet.n_idper = 0;
                e_TesOriDet.n_idtipdoc = 18;
                e_TesOriDet.c_numser = "";
                e_TesOriDet.c_numdoc = TxtNumVou.Text;
                e_TesOriDet.n_imp = 0;
                e_TesOriDet.n_sal = 0;
                e_TesOriDet.n_acuenta = Convert.ToDouble(TxtImpDet.Text); ;
                e_TesOriDet.d_fchdoc = Convert.ToDateTime(TxtFchPag.Text);
                e_TesOriDet.c_glo = "";
                e_TesOriDet.n_cor = 0;
                e_TesOriDet.n_idmon = 115; //Convert.ToInt16(CboMon.SelectedValue);
                e_TesOriDet.n_idmedpag = Convert.ToInt16(CboTipOpe.SelectedValue);
                
                l_TesOriDet.Add(e_TesOriDet);
            }

            // *********************
            // CARGAMOS LOS DESTINOS
            BE_TES_TESORERIADES e_TesDes = new BE_TES_TESORERIADES();
            n_idmod = Convert.ToInt16(funFunciones.NulosN(funDatos.DataTableBuscar(dtDes, "n_id", "n_idmod", "5", "N").ToString()));
            e_TesDes.n_idtes = 0;
            e_TesDes.n_iddes = Convert.ToInt16(LblIdDestino.Text);                             // LE INDICAMOS QUE ES FACTURAS POR PAGAR POR DEFAULT
            e_TesDes.n_imp = Convert.ToDouble(TxtImpDet.Text);
            e_TesDes.n_idmod = n_idmod;
            e_TesDes.n_idbcocta = 0;
            e_TesDes.n_tc = Convert.ToDouble(TxtTc.Text);
            l_TesDes.Add(e_TesDes);

            BE_TES_TESORERIADESDET e_TesDesDet = new BE_TES_TESORERIADESDET();
            //n_idmod = Convert.ToInt16(funDatos.DataTableBuscar(dtOri, "n_id", "n_idmod", LblIdDocCue.Text, "N"));

            e_TesDesDet.n_idtes = 0;
            e_TesDesDet.n_iddes = Convert.ToInt16(LblIdDestino.Text); ;                         // LE INDICAMOS QUE ES FACTURAS POR PAGAR
            e_TesDesDet.n_idtipper = 0;
            e_TesDesDet.n_idmod = n_idmod;
            e_TesDesDet.n_iddoc = Convert.ToInt32(LblIdDoc.Text);
            e_TesDesDet.n_idper = 0;
            e_TesDesDet.n_idtipdoc = Convert.ToInt16(CboTipDoc.SelectedValue);
            e_TesDesDet.c_numser = TxtNumDocCom.Text.Substring(0, 4);
            e_TesDesDet.c_numdoc = TxtNumDocCom.Text.Substring(5, 10);
            e_TesDesDet.n_imp = Convert.ToDouble(TxtImpDoc.Text);
            e_TesDesDet.n_sal = Convert.ToDouble(TxtImpDoc.Text);
            e_TesDesDet.n_acuenta = Convert.ToDouble(TxtImpDet.Text); ;
            e_TesDesDet.n_idori = 0;
            e_TesDesDet.d_fchdoc = Convert.ToDateTime(TxtFchEmi.Text);
            e_TesDesDet.c_glo = "";
            e_TesDesDet.n_cor = 0;
            e_TesDesDet.n_idmon = Convert.ToInt16(CboMon.SelectedValue);
            e_TesDesDet.n_idlib = 34;

            l_TesDesDet.Add(e_TesDesDet);
        }
        void AsignarEgreso()
        {
            l_TesOri.Clear();
            l_TesDes.Clear();
            l_TesDesDet.Clear();
            l_TesOriDet.Clear();

            e_Tesoreria.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Tesoreria.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_Tesoreria.n_mes = Convert.ToInt16(CboMeses.SelectedValue);
            e_Tesoreria.n_idlib = 1;
            e_Tesoreria.c_numreg = "";
            e_Tesoreria.d_fchope = Convert.ToDateTime(TxtFchPag.Text);
            e_Tesoreria.n_idmon = 115; //Convert.ToInt16(CboMon.SelectedValue);
            e_Tesoreria.c_glo = "PAGO DETRACCION PROVEEDOR : " + TxtPro.Text + ",  " + CboTipDoc.Text + " Nº " + TxtNumDocCom.Text;
            e_Tesoreria.n_conciliado = 0;
            e_Tesoreria.n_tc = Convert.ToDouble(TxtTc2.Text);
            e_Tesoreria.n_tipreg = 2;
            e_Tesoreria.n_dongen = 2;                                     // INDICAMOS QUE EL REGISTRO SE GENERA FUERA DEL MODULO EGRESOS, INGRESO

            // *********************
            // CARGAMOS EL ORIGEN
            BE_TES_TESORERIAORI e_TesOri = new BE_TES_TESORERIAORI();
            int n_idmod = Convert.ToInt16(funFunciones.NulosN(funDatos.DataTableBuscar(dtOri, "n_id", "n_idmod", LblIdDocCue.Text, "N").ToString()));
            e_TesOri.n_idtes = 0;
            e_TesOri.n_idori = Convert.ToInt32(LblIdDocCue.Text);
            e_TesOri.n_imp = Convert.ToDouble(TxtImpDet.Text);
            e_TesOri.n_idmod = n_idmod;
            e_TesOri.n_idbcocta = 0;
            e_TesOri.n_tc = Convert.ToDouble(TxtTc.Text);
            l_TesOri.Add(e_TesOri);

            if (OptTipPagTra.Checked == true)
            {
                BE_TES_TESORERIAORIDET e_TesOriDet = new BE_TES_TESORERIAORIDET();
                //n_idmod = Convert.ToInt16(funDatos.DataTableBuscar(dtOri, "n_id", "n_idmod", LblIdDocCue.Text, "N"));

                e_TesOriDet.n_idtes = 0;
                e_TesOriDet.n_idori = Convert.ToInt32(LblIdDocCue.Text);                         // LE INDICAMOS QUE ES FACTURAS POR PAGAR
                e_TesOriDet.n_idtipper = 0;
                e_TesOriDet.n_idmod = n_idmod;
                e_TesOriDet.n_iddoc = Convert.ToInt32(LblIdDoc.Text);
                e_TesOriDet.n_idper = 0;
                e_TesOriDet.n_idtipdoc = 18;
                e_TesOriDet.c_numser = "";
                e_TesOriDet.c_numdoc = TxtNumVou.Text;
                e_TesOriDet.n_imp = 0;
                e_TesOriDet.n_sal = 0;
                e_TesOriDet.n_acuenta = Convert.ToDouble(TxtImpDet.Text); ;
                e_TesOriDet.d_fchdoc = Convert.ToDateTime(TxtFchPag.Text);
                e_TesOriDet.c_glo = "";
                e_TesOriDet.n_cor = 0;
                e_TesOriDet.n_idmon = 115; //Convert.ToInt16(CboMon.SelectedValue);
                e_TesOriDet.n_idmedpag = Convert.ToInt16(CboTipOpe.SelectedValue);
                //e_TesOriDet.n_idlib = 0;

                l_TesOriDet.Add(e_TesOriDet);
            }

            // *********************
            // CARGAMOS LOS DESTINOS
            BE_TES_TESORERIADES e_TesDes = new BE_TES_TESORERIADES();
            n_idmod = Convert.ToInt16(funFunciones.NulosN(funDatos.DataTableBuscar(dtDes, "n_id", "n_idmod", "5", "N").ToString()));
            e_TesDes.n_idtes = 0;
            e_TesDes.n_iddes = Convert.ToInt16(LblIdDestino.Text);                             // LE INDICAMOS QUE ES FACTURAS POR PAGAR POR DEFAULT
            e_TesDes.n_imp = Convert.ToDouble(TxtImpDet.Text);
            e_TesDes.n_idmod = n_idmod;
            e_TesDes.n_idbcocta = 0;
            e_TesDes.n_tc = Convert.ToDouble(TxtTc.Text);
            l_TesDes.Add(e_TesDes);

            BE_TES_TESORERIADESDET e_TesDesDet = new BE_TES_TESORERIADESDET();
            //n_idmod = Convert.ToInt16(funDatos.DataTableBuscar(dtOri, "n_id", "n_idmod", LblIdDocCue.Text, "N"));

            e_TesDesDet.n_idtes = 0;
            e_TesDesDet.n_iddes = Convert.ToInt16(LblIdDestino.Text); ;                         // LE INDICAMOS QUE ES FACTURAS POR PAGAR
            e_TesDesDet.n_idtipper = 0;
            e_TesDesDet.n_idmod = n_idmod;
            e_TesDesDet.n_iddoc = Convert.ToInt32(LblIdDoc.Text);
            e_TesDesDet.n_idper = 0;
            e_TesDesDet.n_idtipdoc = Convert.ToInt16(CboTipDoc.SelectedValue);
            e_TesDesDet.c_numser = TxtNumDocCom.Text.Substring(0, 4);
            e_TesDesDet.c_numdoc = TxtNumDocCom.Text.Substring(5, 10);
            e_TesDesDet.n_imp = Convert.ToDouble(TxtImpDoc.Text);
            e_TesDesDet.n_sal = Convert.ToDouble(TxtImpDoc.Text);
            e_TesDesDet.n_acuenta = Convert.ToDouble(TxtImpDet.Text); ;
            e_TesDesDet.n_idori = 0;
            e_TesDesDet.d_fchdoc = Convert.ToDateTime(TxtFchEmi.Text);
            e_TesDesDet.c_glo = "";
            e_TesDesDet.n_cor = 0;
            e_TesDesDet.n_idmon = Convert.ToInt16(CboMon.SelectedValue);
            e_TesDesDet.n_idlib = 34;

            l_TesDesDet.Add(e_TesDesDet);
        }
        bool CamposOK()
        {
            bool booEstado = true;
            if (TxtPro.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre del proveedor !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtPro.Focus();
                return booEstado;
            }
            if (TxtNumDocCom.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el documento al que se le aplicara la detraccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumDocCom.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboDet.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de detraccion que se aplicara !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboDet.Focus();
                return booEstado;
            }
            if (TxtFchPag.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de pago de la detraccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchPag.Focus();
                return booEstado;
            }
            if (TxtNumVou.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de voucher de pago de la detraccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchPag.Focus();
                return booEstado;
            }
            if (Convert.ToDouble(funFunciones.NulosN(TxtTc2.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de cambio del pago de la detraccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtTc2.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(Convert.ToDateTime(TxtFchPag.Text).Month) != Convert.ToInt16(CboMeses.SelectedValue))
            {
                MessageBox.Show("¡ La fecha de pago no corresponde al mes de registro !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchEmi.Focus();
                return booEstado;
            }
            if (ChkAplPag.Checked == true)
            {
                if (Convert.ToInt16(funFunciones.NulosN(LblIdDocCue.Text)) == 0)
                {
                    MessageBox.Show("¡ No ha especificado el origen del abono !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    CmdBusCta.Focus();
                    return booEstado;
                }
                if (Convert.ToInt16(funFunciones.NulosN(LblIdDestino.Text)) == 0)
                {
                    MessageBox.Show("¡ No ha especificado el destino del abono !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    LblIdDestino.Focus();
                    return booEstado;
                }
            }
            else
            { 
                LblIdDocCue.Text = "0";
                LblIdDestino.Text = "0";
            }
            
            return booEstado;
        }
        private void FrmRegDetraCompras_Activated(object sender, EventArgs e)
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
                // dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
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
                dtResult = funDbGrid.DG_Filtrar(dtLista, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
        }
        private void FrmRegDetraCompras_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }

        private void CmdBusCli_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            objPro.mysConec = mysConec;
            dtResult = objPro.BuscarCliPro(dtPro, 1, "n_id", "");
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    LblIdPro.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtPro.Text = dtResult.Rows[0]["c_nombre"].ToString();
                                        
                    CN_log_compras objCom = new CN_log_compras();
                    objCom.mysConec = mysConec;
                    objCom.Consulta3(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblIdPro.Text));
                    dtCompras = objCom.dtLista;
                }
                else
                {
                    LblIdPro.Text = "";
                    TxtPro.Text = "";
                }
            }
        }
        private void CmdBusDocCom_Click(object sender, EventArgs e)
        {
            if (TxtPro.Text == "")
            {
                if (n_Tipo == 1)
                { 
                    MessageBox.Show("¡ No ha especificado el nombre del proveedor !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                if (n_Tipo == 2)
                {
                    MessageBox.Show("¡ No ha especificado el nombre del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                CmdBusCli.Focus();
                return;
            }

            DataTable dtResult = new DataTable();
            if (n_Tipo == 1)
            { 
                CN_log_compras objCom = new CN_log_compras();
                objCom.mysConec = mysConec;
                dtResult = objCom.BuscarDocumentoDetraccion(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblIdPro.Text));
            }
            
            if (n_Tipo == 2)
            {
                CN_vta_ventas objven = new CN_vta_ventas();
                objven.mysConec = mysConec;
                dtResult = objven.BuscarDocumentoDetraccion(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblIdPro.Text));
            }

            if (dtResult != null)
            {
                LblIdDoc.Text = dtResult.Rows[0]["n_id"].ToString();
                TxtNumDocCom.Text = dtResult.Rows[0]["c_numdoc"].ToString();

                if (n_Tipo == 1)
                {
                    MostrarDatosDocCompra(Convert.ToInt32(LblIdDoc.Text));
                }
                if (n_Tipo == 2)
                {
                    MostrarDatosDocVentas(Convert.ToInt32(LblIdDoc.Text));
                }
            }
            else
            {
                LblIdDoc.Text = "";
                TxtNumDocCom.Text = "";
            }
        }

        private void CboDet_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (Convert.ToInt32(CboDet.SelectedValue) == 0) { return; }

            if (LblIdPro.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre del proveedor !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboDet.SelectedValue = 0;
                CmdBusCli.Focus();
                return;
            }

            if (LblIdDoc.Text == "") 
            {
                MessageBox.Show("¡ No ha especificado el documento al que se aplicara la detraccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboDet.SelectedValue = 0;
                CmdBusDocCom.Focus();
                return;
            }

            double n_valor = 0;
            double n_tasa = 0;
            double n_detra = 0;

            n_valor = Convert.ToDouble(TxtImpExpMN.Text);
            n_tasa = Convert.ToDouble(funDatos.DataTableBuscar(dtDet, "n_id", "n_tasa", Convert.ToInt16(CboDet.SelectedValue).ToString(), "N").ToString());
            n_detra = (n_valor * (n_tasa / 100));

            TxtTas.Text = n_tasa.ToString("0.00");
            TxtImpDet.Text = n_detra.ToString("0.00");
        }

        private void TxtNumVou_TextChanged(object sender, EventArgs e)
        {

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

        private void TxtNumVou_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtPro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtNumDocCom_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtFchEmi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CboMon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtImpDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtImpExpMN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CboDet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtTas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtImpDet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                TxtImpDet.Text = Convert.ToDouble(TxtImpDet.Text).ToString("0.00");
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtFchPag_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            
            if (booAgregando == true) {return;}
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

        private void TxtFchEmi_ValueChanged(object sender, EventArgs e)
        {
            TxtFchEmi.CustomFormat = "dd/MM/yyyy";
            TxtFchEmi.Format = DateTimePickerFormat.Custom;
        }

        private void TxtFchPag_ValueChanged(object sender, EventArgs e)
        {
            TxtFchPag.CustomFormat = "dd/MM/yyyy";
            TxtFchPag.Format = DateTimePickerFormat.Custom;
        }

        private void CmdBusCta_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(CboMon.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda de la detraccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            string c_dato = "";
            DataTable dtResul = new DataTable();

            if (OptTipPagEfe.Checked == true)
            {
                //dtResul = funDatos.DataTableFiltrar(dtOri, "n_idmon = " + Convert.ToInt16(CboMon.SelectedValue) + " AND n_oridin = 1");
                dtResul = funDatos.DataTableFiltrar(dtOri, "n_idmon = 115 AND n_oridin = 1");
            }
            if (OptTipPagTra.Checked == true)
            {
                //dtResul = funDatos.DataTableFiltrar(dtOri, "n_idmon = " + Convert.ToInt16(CboMon.SelectedValue) + " AND n_oridin = 2");
                dtResul = funDatos.DataTableFiltrar(dtOri, "n_idmon = 115 AND n_oridin = 2");
            }

            dtResul = objOri.BuscarOrigen(dtResul);
            if (dtResul != null)
            {
                if (dtResul.Rows.Count != 0)
                {
                    c_dato = dtResul.Rows[0]["c_des"].ToString();           // MOSTRAMOS LA DESCRIPCION DEL ORIGEN
                   LblDesCta.Text = c_dato;

                    c_dato = dtResul.Rows[0]["n_id"].ToString();            // MOSTRAMOS EL ID DEL ORIGEN
                    LblIdDocCue.Text = c_dato;

                    c_dato = dtResul.Rows[0]["c_cuecon"].ToString();            // MOSTRAMOS EL NUMERO DE CUENTA
                    TxtNumCta.Text = c_dato;

                    CboTipOpe.SelectedValue = 0;
                    if (Convert.ToInt16(dtResul.Rows[0]["n_oridin"]) == 2)
                    {
                        CboTipOpe.Enabled = true;
                        CboTipOpe.SelectedValue = 1;
                    }
                    else
                    {
                        CboTipOpe.Enabled = false;
                    }
                }
            }
        }

        private void OptTipPagEfe_CheckedChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace != 3)
            { 
                if (OptTipPagEfe.Checked == true)
                {
                    CboTipOpe.Enabled = false;
                    CboTipOpe.SelectedValue = 0;
                    CboTipOpe.Enabled = false;
                }
            }
            if (OptTipPagEfe.Checked == true)
            {
                TxtNumCta.Text = "";
                LblDesCta.Text = "";
                LblIdDocCue.Text = "";
            }
        }

        private void OptTipPagTra_CheckedChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace != 3)
            {
                if (OptTipPagTra.Checked == true)
                {
                    CboTipOpe.Enabled = true;
                    CboTipOpe.SelectedValue = 3;
                    CboTipOpe.Enabled = true;
                }
            }
            if (OptTipPagTra.Checked == true)
            {
                TxtNumCta.Text = "";
                LblDesCta.Text = "";
                LblIdDocCue.Text = "";
            }
        }

        private void TxtFchPag_Validated(object sender, EventArgs e)
        {
            if (STU_SISTEMA.MESTRABAJO == 0)
            {
                //DataTable dttctem = new DataTable();
                //ObjTC.mysConec = mysConec;
                //int n_ano = Convert.ToDateTime(TxtFchEmiDoc.Text).Year;
                //dttctem = ObjTC.Listartcano(151, n_ano.ToString());
                //TxtTc2.Text = objFunciones.ObtenerTC(dttctem, TxtFchEmiDoc.Text).ToString("0.000");
            }
            else
            {
                TxtTc2.Text = objFunciones.ObtenerTC(dtTC, TxtFchPag.Text).ToString("0.000");
            }
        }

        private void ChkAplPag_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAplPag.Checked == false)
            {
                TxtNumCta.Text = "";
                LblDesCta.Text = "";
                LblIdDocCue.Text = "";
                CboTipOpe.SelectedValue = 0;

                OptTipPagEfe.Checked = false;
                OptTipPagTra.Checked = false;

                OptTipPagEfe.Enabled = false;
                OptTipPagTra.Enabled = false;

                CmdBusCta.Enabled = false;

                CboTipOpe.Enabled = false;
            }
            else
            {
                OptTipPagEfe.Checked = true;
                OptTipPagTra.Checked = false;

                OptTipPagEfe.Enabled = true;
                OptTipPagTra.Enabled = true;

                CmdBusCta.Enabled = true;

                //CboTipOpe.Enabled = true;
            }
        }
        private void CboBusDes_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(CboMon.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda de la detraccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            string c_dato = "";
            DataTable dtResul = new DataTable();

            if (OptTipPagEfe.Checked == true)
            {
                //dtResul = funDatos.DataTableFiltrar(dtDes, "n_idmon = " + Convert.ToInt16(CboMon.SelectedValue) + " ");
                dtResul = funDatos.DataTableFiltrar(dtDes, "n_idmon = 115");
            }
            if (OptTipPagTra.Checked == true)
            {
                //dtResul = funDatos.DataTableFiltrar(dtDes, "n_idmon = " + Convert.ToInt16(CboMon.SelectedValue) + " ");
                dtResul = funDatos.DataTableFiltrar(dtDes, "n_idmon = 115");
            }

            dtResul = objDes.BuscarDestino(dtResul);
            if (dtResul != null)
            {
                if (dtResul.Rows.Count != 0)
                {
                    c_dato = dtResul.Rows[0]["c_des"].ToString();           // MOSTRAMOS LA DESCRIPCION DEL ORIGEN
                    LblDesDes.Text = c_dato;

                    c_dato = dtResul.Rows[0]["n_id"].ToString();            // MOSTRAMOS EL ID DEL ORIGEN
                    LblIdDestino.Text = c_dato;

                    c_dato = dtResul.Rows[0]["c_cuecon"].ToString();            // MOSTRAMOS EL NUMERO DE CUENTA
                    TxtCtaDes.Text = c_dato;
                }
            }
        }

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_NomArchivo = "";
            if (n_Tipo == 1)
            {
                c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-CON-DETRACCION-COMPRAS-" + STU_SISTEMA.ANOTRABAJO.ToString() + Convert.ToInt16(CboMeses.SelectedValue).ToString("00") + ".xls";
            }
            else
            {
                c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-CON-DETRACCION-VENTAS-" + STU_SISTEMA.ANOTRABAJO.ToString() + Convert.ToInt16(CboMeses.SelectedValue).ToString("00") + ".xls";
            }
            DgLista.ExportTo(c_NomArchivo);
            MessageBox.Show("! Se exporto con exito la informacion en el archivo " + c_NomArchivo + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
    }
}
