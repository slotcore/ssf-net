using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Estacionamiento;
using SIAC_Entidades.Tesoreria;
using SIAC_Negocio.Estacionamiento;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Planilla;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Tesoreria;
using SIAC_Objetos.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIAC_NET_Estacionamientos.Formularios
{
    public partial class FrmManLiquidacion : Form
    {
        // VARIABLES PUBLICAS
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_empresalocal o_emploc = new CN_sys_empresalocal();
        CN_sun_tipdoccom o_tipdoc = new CN_sun_tipdoccom();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sun_tipcam ObjTC = new CN_sun_tipcam();
        SIAC_Objetos.Funciones objFunciones = new SIAC_Objetos.Funciones();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_EST_LIQUIDACION BE_Registro = new BE_EST_LIQUIDACION();
        List<BE_EST_LIQUIDACIONDET> l_LiquidaDet = new List<BE_EST_LIQUIDACIONDET>();
        BE_TES_TESORERIA e_tes = new BE_TES_TESORERIA();
        List<BE_TES_TESORERIADES> l_tesdes = new List<BE_TES_TESORERIADES>();
        List<BE_TES_TESORERIADESDET> l_tesdesdet = new List<BE_TES_TESORERIADESDET>();
        List<BE_TES_TESORERIAORI> l_tesori = new List<BE_TES_TESORERIAORI>();
        List<BE_TES_TESORERIAORIDET> l_tesoridet = new List<BE_TES_TESORERIAORIDET>();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO
        DataTable dtForm = new DataTable();
        DataTable dtLocal = new DataTable();
        DataTable dtSer = new DataTable();
        DataTable dtDoc = new DataTable();
        DataTable dtTipCon = new DataTable();
        DataTable dtcliente = new DataTable();
        DataTable dtCajero = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtTC = new DataTable();
        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[10, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[13, 5];

        string C_IDLOCAL = "";
        string C_IDCAJERO = "";
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "EF1234567890" + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;

        public FrmManLiquidacion()
        {
            InitializeComponent();
        }

        private void FrmManLiquidacion_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboPla, dtLocal, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboCajero, dtCajero, "n_id", "c_cajapenom");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        void ConfigurarFormulario()
        {
            this.Height = 629;
            this.Width = 910;
                        
            string c_nomarc = @"C:\SSF-NET\estacionamiento.ini";
            C_IDLOCAL = funDatos.IniLeerSeccion(c_nomarc, "INFORMACION", "LOCAL").ToString();
            C_IDCAJERO = funDatos.IniLeerSeccion(c_nomarc, "INFORMACION", "CAJERO").ToString();

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();

            arrCabeceraFlex1[0, 0] = "Nº Doc. Identidad";
            arrCabeceraFlex1[0, 1] = "90";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_numpla";

            arrCabeceraFlex1[1, 0] = "Cliente";
            arrCabeceraFlex1[1, 1] = "200";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "n_id";

            arrCabeceraFlex1[2, 0] = "Tip. Doc.";
            arrCabeceraFlex1[2, 1] = "30";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "n_id";

            arrCabeceraFlex1[3, 0] = "Nº Documento";
            arrCabeceraFlex1[3, 1] = "100";
            arrCabeceraFlex1[3, 2] = "C";
            arrCabeceraFlex1[3, 3] = "";
            arrCabeceraFlex1[3, 4] = "n_id";

            arrCabeceraFlex1[4, 0] = "Imp. Bruto";
            arrCabeceraFlex1[4, 1] = "65";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.00";
            arrCabeceraFlex1[4, 4] = "n_id";

            arrCabeceraFlex1[5, 0] = "Imp. I.G.V.";
            arrCabeceraFlex1[5, 1] = "65";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "n_id";

            arrCabeceraFlex1[6, 0] = "Imp. Total";
            arrCabeceraFlex1[6, 1] = "65";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_id";

            arrCabeceraFlex1[7, 0] = "n_iddoc";
            arrCabeceraFlex1[7, 1] = "0";
            arrCabeceraFlex1[7, 2] = "N";
            arrCabeceraFlex1[7, 3] = "";
            arrCabeceraFlex1[7, 4] = "n_id";

            arrCabeceraFlex1[8, 0] = "n_idtipdoc";
            arrCabeceraFlex1[8, 1] = "0";
            arrCabeceraFlex1[8, 2] = "N";
            arrCabeceraFlex1[8, 3] = "";
            arrCabeceraFlex1[8, 4] = "n_id";

            arrCabeceraFlex1[9, 0] = "Tipo Operacion";
            arrCabeceraFlex1[9, 1] = "0";
            arrCabeceraFlex1[9, 2] = "N";
            arrCabeceraFlex1[9, 3] = "";
            arrCabeceraFlex1[9, 4] = "n_id";

            arrCabeceraFlex1[10, 0] = "Id Documento Origen";
            arrCabeceraFlex1[10, 1] = "0";
            arrCabeceraFlex1[10, 2] = "N";
            arrCabeceraFlex1[10, 3] = "";
            arrCabeceraFlex1[10, 4] = "n_id";

            arrCabeceraFlex1[11, 0] = "Servicio";
            arrCabeceraFlex1[11, 1] = "200";
            arrCabeceraFlex1[11, 2] = "C";
            arrCabeceraFlex1[11, 3] = "";
            arrCabeceraFlex1[11, 4] = "c_desser";

            arrCabeceraFlex1[12, 0] = "Fecha Doc";
            arrCabeceraFlex1[12, 1] = "0";
            arrCabeceraFlex1[12, 2] = "F";
            arrCabeceraFlex1[12, 3] = "";
            arrCabeceraFlex1[12, 4] = "d_fecha";

            funFlex.FlexMostrarDatos(FgPlacas, arrCabeceraFlex1, dtLocal, 2, false);
            FgPlacas.AllowEditing = false;

            DataTable dtResul = new DataTable();
            dtResul = funDatos.DataTableFiltrar(dtCajero, "n_idloc = " + C_IDLOCAL + "");

            int n_row = 0;
            bool b_seencontro = false;
            int n_idusu = 0;
            for (n_row = 0; n_row <= dtResul.Rows.Count - 1; n_row++)
            {
                n_idusu = Convert.ToInt16(dtResul.Rows[n_row]["n_idusu"]);
                
                if (STU_SISTEMA.USUARIOID == n_idusu)
                {
                    C_IDCAJERO = Convert.ToInt16(dtResul.Rows[n_row]["n_id"]).ToString();
                    b_seencontro = true;
                    break;
                }
            }

            if (b_seencontro == true)
            {
                CboPla.Enabled = false;
                CboCajero.SelectedValue = Convert.ToInt16(C_IDCAJERO);
            }
            else
            {
                CboPla.Enabled = true;
                CboCajero.Enabled = true;
                //MessageBox.Show("¡ El usuario actual no pertenece a esta playa !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //this.Close();
            }
        }
        void DataTableCargar()
        {
            CN_est_liquidacion objRegistros = new CN_est_liquidacion(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            dtLista = objRegistros.dtListar;
            objRegistros = null;

            CN_est_clientes o_cliente = new CN_est_clientes(STU_SISTEMA);
            o_cliente.STU_SISTEMA = STU_SISTEMA;
            o_cliente.Listar3(STU_SISTEMA.EMPRESAID);
            dtcliente = o_cliente.dtListar;
            o_cliente = null;

            CN_est_cajeros o_cajero = new CN_est_cajeros(STU_SISTEMA);
            o_cajero.STU_SISTEMA = STU_SISTEMA;
            o_cajero.Listar(STU_SISTEMA.EMPRESAID);
            dtCajero = o_cajero.dtListar;
            o_cajero = null;
            
            CN_est_servicios o_ser = new CN_est_servicios(STU_SISTEMA);
            o_ser.STU_SISTEMA = STU_SISTEMA;
            dtSer = o_ser.Listar(STU_SISTEMA.EMPRESAID);
            dtSer = null;

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            objFormVis.mysConec = o_conec.mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(91, ref arrCabeceraDg1);

            objForm.mysConec = o_conec.mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(91);

            o_emploc.mysConec = o_conec.mysConec;
            dtLocal = o_emploc.Listar(STU_SISTEMA.EMPRESAID, 0);
            
            o_tipdoc.mysConec = o_conec.mysConec;
            dtDoc = o_tipdoc.Listar();
            dtDoc = funDatos.DataTableFiltrar(dtDoc, "n_id IN(2, 4, 13, 83)");

            ObjTC.mysConec = o_conec.mysConec;
            dtTC = ObjTC.Listartcano(151, STU_SISTEMA.ANOTRABAJO.ToString());

            objMeses.mysConec = o_conec.mysConec;
            dtMeses = objMeses.Listar();
            o_conec = null;
        }
        void ListarItems()
        {
            if (STU_SISTEMA.USUARIOPERFIL == 10)
            {
                //int n_idpla = Convert.ToInt16(funDatos.DataTableBuscar(dtCajero, "n_idusu", "n_idloc", STU_SISTEMA.USUARIOID.ToString(), "N"));

                dtLista = funDatos.DataTableFiltrar(dtLista, "n_idpla = " + C_IDLOCAL + "");
            }

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
            DataTable dt_Detalle = new DataTable();
            booAgregando = true;
            FgPlacas.Rows.Count = 2;

            l_LiquidaDet.Clear();
            CN_est_liquidacion objRegistros = new CN_est_liquidacion(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;

            objRegistros.TraerRegistro(n_IdRegistro);
            if (objRegistros.b_OcurrioError == false)
            {
                BE_Registro = objRegistros.e_Liquidacion;
                l_LiquidaDet = objRegistros.l_LiquidacionDet;
                dt_Detalle = objRegistros.dtListarDet;

                TxtNumSer.Text = BE_Registro.c_numser;
                TxtNumDoc.Text = BE_Registro.c_numdoc;
                CboPla.SelectedValue = BE_Registro.n_idpla;
                CboCajero.SelectedValue = BE_Registro.n_idcaj;
                TxtFchEmi.Text = BE_Registro.d_fchemi.ToString("dd/MM/yyyy");
                TxtFchIni.Text = BE_Registro.d_fchini.ToString("dd/MM/yyyy");
                TxtFchFin.Text = BE_Registro.d_fchfin.ToString("dd/MM/yyyy");
                TxtImpTot.Text = BE_Registro.n_importe.ToString("0.00");
                TxtObs.Text = BE_Registro.c_obs;
                LblNumRec.Text = BE_Registro.n_numdoccob.ToString();
                TxtHorIni.Text = BE_Registro.h_horliq.ToString();

                if (BE_Registro.n_tipo == 1) { OptTipo1.Checked = true; }
                if (BE_Registro.n_tipo == 2) { OptTipo2.Checked = true; }
            }
            objRegistros = null;
            MostrarDetalle(dt_Detalle);
        }

        void MostrarDetalle(DataTable dt_Detalle)
        {
            string c_dato = "";
            int n_row = 0;

            for (n_row = 0; n_row <= dt_Detalle.Rows.Count - 1; n_row++)
            {
                FgPlacas.Rows.Count = FgPlacas.Rows.Count + 1;

                c_dato = dt_Detalle.Rows[n_row]["c_clinumdoc"].ToString();
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 1, c_dato);

                c_dato = dt_Detalle.Rows[n_row]["c_clinom"].ToString();
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 2, c_dato);

                c_dato = dt_Detalle.Rows[n_row]["c_tipdocabr"].ToString();
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 3, c_dato);

                c_dato = dt_Detalle.Rows[n_row]["c_numdoc"].ToString();
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 4, c_dato);

                c_dato = Convert.ToDouble(dt_Detalle.Rows[n_row]["n_impbru"]).ToString("0.00");
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 5, c_dato);

                c_dato = Convert.ToDouble(dt_Detalle.Rows[n_row]["n_impigv"]).ToString("0.00");
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 6, c_dato);

                c_dato = Convert.ToDouble(dt_Detalle.Rows[n_row]["n_imptotven"]).ToString("0.00");
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 7, c_dato);

                c_dato = Convert.ToInt32(dt_Detalle.Rows[n_row]["n_idven"]).ToString();
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 8, c_dato);

                c_dato = Convert.ToInt32(dt_Detalle.Rows[n_row]["n_idtipdoc"]).ToString();
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 9, c_dato);

                c_dato =dt_Detalle.Rows[n_row]["c_despro"].ToString();
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 12, c_dato);

                c_dato = Convert.ToDateTime(dt_Detalle.Rows[n_row]["d_fchdoc"]).ToString("dd/MM/yyyy");
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 13, c_dato);
            }
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
            FgPlacas.AllowEditing = true;
            CboPla.SelectedValue = Convert.ToInt16(C_IDLOCAL);
            CboCajero.SelectedValue = Convert.ToInt16(C_IDCAJERO);
            TxtNumSer.Text = "0001";

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            o_tipdoc =new CN_sun_tipdoccom();
            o_tipdoc.mysConec = o_conec.mysConec;
            TxtNumDoc.Text = o_tipdoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 91, TxtNumSer.Text);
            booAgregando = false;
            o_conec = null;
            o_tipdoc = null;
        }
        void Blanquea()
        {
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtFchEmi.Text = "";
            TxtFchIni.Text = "";
            TxtFchFin.Text = "";
            TxtImpBru.Text = "";
            txtImpIgv.Text = "";
            TxtImpTot.Text = "";
            TxtObs.Text = "";
            CboPla.SelectedValue = 0;
            LblNumRec.Text = "";
            TxtObs.CharacterCasing = CharacterCasing.Upper;
            TxtHorIni.Text = DateTime.Now.ToString("HH:mm:ss");
            FgPlacas.Rows.Count = 2;
            OptTipo1.Checked = true;
        }
        void Bloquea()
        {
            TxtFchEmi.Enabled = !TxtFchEmi.Enabled;
            TxtFchIni.Enabled = !TxtFchIni.Enabled;
            TxtFchFin.Enabled = !TxtFchFin.Enabled;
            TxtImpBru.Enabled = !TxtImpBru.Enabled;
            txtImpIgv.Enabled = !txtImpIgv.Enabled;
            TxtImpTot.Enabled = !TxtImpTot.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;

            OptTipo1.Enabled = !OptTipo1.Enabled;
            OptTipo2.Enabled = !OptTipo2.Enabled;
            //CboPla.Enabled = !CboPla.Enabled;
            //CboCajero.Enabled = !CboCajero.Enabled;
            FgPlacas.Rows.Count = 2;

            CmdCargar.Enabled = !CmdCargar.Enabled;
            CmdDelPla.Enabled = !CmdDelPla.Enabled;
            TxtFchFin.Text = DateTime.Now.ToString("HH:mm:ss");
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
        }
        void Modificar()
        {
            n_QueHace = 2;
            booAgregando = true;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());
            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            FgPlacas.AllowEditing = true;
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                CN_est_liquidacion objRegistros = new CN_est_liquidacion(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
                    dtLista = objRegistros.dtListar;
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                objRegistros = null;
            }
            return booResult;
        }
        void Cancelar()
        {
            n_QueHace = 3;
            ActivarTool();
            Bloquea();
            FgPlacas.AllowEditing = false;
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

            CN_est_liquidacion objRegistros = new CN_est_liquidacion(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.e_tes = e_tes;
            objRegistros.l_tesdes = l_tesdes;
            objRegistros.l_tesdesdet = l_tesdesdet;
            objRegistros.l_tesori = l_tesori;
            objRegistros.l_tesoridet = l_tesoridet;
            
            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(BE_Registro, l_LiquidaDet);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(BE_Registro, l_LiquidaDet);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.n_ErrorNumber.ToString() + " = " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            objRegistros = null;
            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_id = 0;
            int n_row = 0;
            string c_dato = "";
            l_LiquidaDet.Clear();
            l_tesori.Clear();
            l_tesoridet.Clear();
            l_tesdes.Clear();
            l_tesdesdet.Clear();

            if (n_QueHace == 1)
            {
                n_id = 0;
            }
            else
            {
                n_id = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());
            }

            BE_Registro.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_Registro.n_id = n_id;
            BE_Registro.c_numser = TxtNumSer.Text;
            BE_Registro.c_numdoc = TxtNumDoc.Text;
            BE_Registro.n_idtipdoc = 91;
            BE_Registro.n_idpla = Convert.ToInt16(CboPla.SelectedValue);
            BE_Registro.n_idcaj = Convert.ToInt16(CboCajero.SelectedValue);
            BE_Registro.n_ano = STU_SISTEMA.ANOTRABAJO;
            BE_Registro.n_mes = STU_SISTEMA.MESTRABAJO;
            BE_Registro.d_fchemi = Convert.ToDateTime(TxtFchEmi.Text);
            BE_Registro.d_fchini = Convert.ToDateTime(TxtFchIni.Text);
            BE_Registro.d_fchfin = Convert.ToDateTime(TxtFchFin.Text);
            BE_Registro.n_importe = Convert.ToDouble(TxtImpTot.Text);
            BE_Registro.n_numdoccob = FgPlacas.Rows.Count - 2;
            BE_Registro.c_obs = TxtObs.Text;
            BE_Registro.h_horliq = TxtHorIni.Text;
            
            if (OptTipo1.Checked == true) { BE_Registro.n_tipo = 1; }
            if (OptTipo2.Checked == true) { BE_Registro.n_tipo = 2; }  
            for (n_row = 2; n_row <= FgPlacas.Rows.Count - 1; n_row++)
            {
                BE_EST_LIQUIDACIONDET e_LiquidaDet = new BE_EST_LIQUIDACIONDET();

                e_LiquidaDet.n_idliq = 0;

                c_dato = FgPlacas.GetData(n_row, 8).ToString();
                e_LiquidaDet.n_idven = Convert.ToInt32(c_dato);

                c_dato = FgPlacas.GetData(n_row, 7).ToString();
                e_LiquidaDet.n_impcob = Convert.ToDouble(c_dato);

                c_dato = FgPlacas.GetData(n_row, 10).ToString();
                e_LiquidaDet.n_idtip = Convert.ToInt32(c_dato);

                c_dato = FgPlacas.GetData(n_row, 11).ToString();
                e_LiquidaDet.n_iddocori = Convert.ToInt32(c_dato);
                l_LiquidaDet.Add(e_LiquidaDet);
            }
            PrepararTesoreria();
        }
        void PrepararTesoreria()
        {
            int n_row = 0;
            int n_cor = 0;
            double N_TC = 0;
            N_TC = Convert.ToDouble( objFunciones.ObtenerTC(dtTC, TxtFchFin.Text));

            e_tes.n_idemp = STU_SISTEMA.EMPRESAID;
            e_tes.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_tes.n_mes = STU_SISTEMA.MESTRABAJO;
            e_tes.n_idlib = 1;      // INDICAMOS QUE ES EL LIBRO DE CAJA Y BACOS
            e_tes.n_id = 0;         // EL ID SE GENERARA AL MOMENTO DE GUARDAR EL REGISTRO
            e_tes.c_numreg = "";    // EL NUMERO DE ASIENTO SE GENERARA AL MOMENTO DE CREAR LOS ASIWNTOS CONTABLES
            e_tes.d_fchope = Convert.ToDateTime(TxtFchFin.Text);
            e_tes.n_idmon = 115;
            e_tes.c_glo = "LIQUIDACION DE CAJA DEL "+TxtFchFin.Text+" CAJERO :"+CboCajero.Text+" PLAYA :"+CboPla.Text;
            e_tes.n_conciliado=0;
            e_tes.n_tc = N_TC;
            e_tes.n_tipreg = 1;   // INDICAMOS QUE ES UN REFGISTRO DE INGRESO
            e_tes.n_dongen = 2;   // LE INDICAMOS QUE EL REGISTRO SE SE GENERO POR EL MODULO DE TESORERIA


            BE_TES_TESORERIAORI e_tesori = new BE_TES_TESORERIAORI();

            e_tesori.n_idtes =0;
            e_tesori.n_idori = 116;
            e_tesori.n_imp = Convert.ToDouble(TxtImpTot.Text);
            e_tesori.n_idmod =0;
            e_tesori.n_idbcocta = 0;
            e_tesori.n_tc = N_TC;
            //e_tesori.n_conciliado = 0;
            l_tesori.Add(e_tesori);

            BE_TES_TESORERIADES e_tesdes = new BE_TES_TESORERIADES();

            e_tesdes.n_idtes = 0;
            e_tesdes.n_iddes = 256;
            e_tesdes.n_imp = Convert.ToDouble(TxtImpTot.Text);
            e_tesdes.n_idmod = 0;
            e_tesdes.n_idbcocta = 0;
            e_tesdes.n_tc = N_TC;
            l_tesdes.Add(e_tesdes);

            n_cor = 1;
            for (n_row = 2; n_row <= FgPlacas.Rows.Count - 1; n_row++)
            {
                BE_TES_TESORERIADESDET e_tesdetdet = new BE_TES_TESORERIADESDET();
                e_tesdetdet.n_idtes = 0;
                e_tesdetdet.n_iddes = 256;
                e_tesdetdet.n_idtipper = 0;
                e_tesdetdet.n_idmod = 2;
                e_tesdetdet.n_iddoc = Convert.ToInt32(FgPlacas.GetData(n_row,8));
                e_tesdetdet.n_idper = 0;
                e_tesdetdet.n_idtipdoc = Convert.ToInt32(FgPlacas.GetData(n_row,9));
                e_tesdetdet.c_numser =  FgPlacas.GetData(n_row,4).ToString().Substring(0, 4);
                e_tesdetdet.c_numdoc =  FgPlacas.GetData(n_row,4).ToString().Substring(5, 10);
                e_tesdetdet.n_imp =  Convert.ToDouble(FgPlacas.GetData(n_row,7));
                e_tesdetdet.n_sal =  Convert.ToDouble(FgPlacas.GetData(n_row,7));
                e_tesdetdet.n_acuenta =  Convert.ToDouble(FgPlacas.GetData(n_row,7));
                e_tesdetdet.n_idori = 0;
                e_tesdetdet.d_fchdoc = Convert.ToDateTime(FgPlacas.GetData(n_row,13));
                e_tesdetdet.c_glo = "";
                e_tesdetdet.n_cor = n_cor;
                e_tesdetdet.n_idmon = 115;           // LE INDICAMOS QUES LA MONEDA ES SOLES
                e_tesdetdet.n_idlib = 14;            // LE INDICAMOS QUE ES LIBRO DE VENTAS

                l_tesdesdet.Add(e_tesdetdet);

                n_cor = n_cor + 1;
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt16(CboPla.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre de la playa !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboPla.Focus();
                return booEstado;
            }
            if (Convert.ToDouble(funFunciones.NulosN(TxtImpTot.Text)) == 0)
            {
                MessageBox.Show("¡ No hay cargos gerenrados para guardar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CmdCargar.Focus();
                return booEstado;
            }


            //if (TxtBol.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado el numero de serie para la boleta de este local !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    TxtBol.Focus();
            //    return booEstado;
            //}
            //if (TxtTik.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado el numero de serie para el ticket de este local !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    TxtTik.Focus();
            //    return booEstado;
            //}
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
                CN_est_liquidacion objRegistros = new CN_est_liquidacion(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;
                objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
                dtLista = objRegistros.dtListar;
                objRegistros = null;
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
            objFormVis = null;
            this.Close();
        }
        private void FrmManLiquidacion_Activated(object sender, EventArgs e)
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
            //booAgregando = true;
            //VerRegistro(intIdRegistro);
            //booAgregando = false;
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
        private void FrmManLiquidacion_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }
        private void CboPla_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            DataTable dtResul = new DataTable();
            dtResul = funDatos.DataTableFiltrar(dtCajero, "n_idloc = " + Convert.ToInt16(CboPla.SelectedValue).ToString() + "");
            funDatos.ComboBoxCargarDataTable(CboCajero, dtResul, "n_id", "c_cajapenom");
            CboCajero.SelectedValue = Convert.ToInt16(C_IDCAJERO);
        }
        private void CmdCargar_Click(object sender, EventArgs e)
        {
            string c_dato = "";
            string c_numser = "0001";
            int n_row = 0;
            int n_numdoc = 0;
            int n_corr = 0;
            int n_tipo = 0;
            double n_imptot = 0;
            double n_impigv = 0;
            double n_impbru = 0;
            DataTable dtdocumento = new DataTable();
            CN_est_liquidacion o_Liquidacion = new CN_est_liquidacion(STU_SISTEMA);
            o_Liquidacion.STU_SISTEMA = STU_SISTEMA;
            FgPlacas.Rows.Count = 2;
            if (OptTipo1.Checked == true) { n_tipo = 1; }
            if (OptTipo2.Checked == true) { n_tipo = 2; }
            o_Liquidacion.Consulta1(STU_SISTEMA.EMPRESAID, Convert.ToInt16(C_IDLOCAL), Convert.ToInt16(C_IDCAJERO), TxtFchIni.Text, TxtFchFin.Text, n_tipo);
            dtdocumento = o_Liquidacion.dtListar;
            o_Liquidacion = null;

            if (dtdocumento.Rows.Count == 0)
            {
                MessageBox.Show("¡ No hay movimiento para liquidar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                o_Liquidacion = null;
                return;
            }
            
            FgPlacas.Rows.Count = 2;
            //n_numdoc = 1;
            n_corr = 1;
            LblNumRec.Text = (dtdocumento.Rows.Count).ToString();

            if (dtdocumento.Rows.Count != 0)
            {
                for (n_row = 0; n_row <= dtdocumento.Rows.Count - 1; n_row++)
                {
                    FgPlacas.Rows.Count = FgPlacas.Rows.Count + 1;

                    c_dato = dtdocumento.Rows[n_row]["c_clinumdocide"].ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 1, c_dato);

                    c_dato = dtdocumento.Rows[n_row]["c_clinom"].ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 2, c_dato);

                    c_dato = dtdocumento.Rows[n_row]["c_tipdocdes"].ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 3, c_dato);

                    c_dato = dtdocumento.Rows[n_row]["c_numdoc"].ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 4, c_dato);

                    c_dato = Convert.ToDouble(dtdocumento.Rows[n_row]["n_impbru"]).ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 5, c_dato);

                    c_dato =  Convert.ToDouble(dtdocumento.Rows[n_row]["n_impigv"]).ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 6, c_dato);

                    c_dato =  Convert.ToDouble(dtdocumento.Rows[n_row]["n_imptotven"]).ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 7, c_dato);

                    c_dato = Convert.ToInt32(dtdocumento.Rows[n_row]["n_venid"]).ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 8, c_dato);

                    c_dato = Convert.ToInt16(dtdocumento.Rows[n_row]["n_idtipdoc"]).ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 9, c_dato);

                    c_dato = Convert.ToInt16(dtdocumento.Rows[n_row]["n_tipo"]).ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 10, c_dato);

                    c_dato = Convert.ToInt32(dtdocumento.Rows[n_row]["n_iddocori"]).ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 11, c_dato);

                    c_dato = funFunciones.NulosC(dtdocumento.Rows[n_row]["c_desser"]).ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 12, c_dato);

                    c_dato = Convert.ToDateTime(dtdocumento.Rows[n_row]["d_fchdoc"]).ToString("dd/MM/yyyy");
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 13, c_dato);

                    n_numdoc = n_numdoc + 1;
                    n_corr = n_corr + 1;
                }
            }

            n_imptot = funFlex.FlexSumarCol(FgPlacas, 5, 2, FgPlacas.Rows.Count - 1);
            n_impigv = funFlex.FlexSumarCol(FgPlacas, 6, 2, FgPlacas.Rows.Count - 1);
            n_impbru = funFlex.FlexSumarCol(FgPlacas, 7, 2, FgPlacas.Rows.Count - 1);

            TxtImpBru.Text = n_imptot.ToString("0.00");
            txtImpIgv.Text = n_impigv.ToString("0.00");
            TxtImpTot.Text = n_impbru.ToString("0.00");
        }
        private void emitirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_est_liquidacion o_liq = new CN_est_liquidacion(STU_SISTEMA);
            o_liq.STU_SISTEMA = STU_SISTEMA;
            int n_idreg = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString()); ;
            o_liq.STU_SISTEMA = STU_SISTEMA;
            o_liq.ImprimirLiquidacion(n_idreg,1);
            o_liq = null;
        }
        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            if (booAgregando == true) { return; }

            CN_est_liquidacion objRegistros = new CN_est_liquidacion(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue));
            dtLista = objRegistros.dtListar;
            objRegistros = null;
            LblNumReg.Text = (dtLista.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtLista, true);

            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);

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
        private void OptTipo1_CheckedChanged(object sender, EventArgs e)
        {
            FgPlacas.Rows.Count = 2;
        }
        private void OptTipo2_CheckedChanged(object sender, EventArgs e)
        {
            FgPlacas.Rows.Count = 2;
        }
        private void imprimirLiquidacionFormatoBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_est_liquidacion o_liq = new CN_est_liquidacion(STU_SISTEMA);
            o_liq.STU_SISTEMA = STU_SISTEMA;

            int n_idreg = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString()); ;
            o_liq.STU_SISTEMA = STU_SISTEMA;
            o_liq.ImprimirLiquidacion(n_idreg, 2);
            o_liq = null;
        }

        private void imprimirLiquidacionXDiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_est_liquidacion o_liq = new CN_est_liquidacion(STU_SISTEMA);
            o_liq.STU_SISTEMA = STU_SISTEMA;
            int n_idpla = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());    // ID DE LA PLAYA
            string c_fecha = DgLista.Columns[4].CellValue(DgLista.Row).ToString().Substring(0,10); 
            o_liq.STU_SISTEMA = STU_SISTEMA;
            o_liq.ImprimirLiquidacionDia(STU_SISTEMA.EMPRESAID, n_idpla, c_fecha);
            o_liq = null;
        }

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            string c_numliq = DgLista.Columns["c_liqnumdoc"].CellValue(DgLista.Row).ToString();
            string c_nompla = DgLista.Columns["c_plades"].CellValue(DgLista.Row).ToString();
            Tab1.SelectedIndex = 1;
            VerRegistro(intIdRegistro);
            string c_archivo = STU_SISTEMA.EMPRESARUC + "-LIQ" + c_numliq + ".xls";
            funFlex.ExportToExcel(FgPlacas, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "LIQUIDACION DE COBRANZA", c_nompla, c_archivo);
        }
    }
}
