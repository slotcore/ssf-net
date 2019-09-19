using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Estacionamiento;
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
    public partial class FrmGeneraCargos : Form
    {
        // VARIABLES PUBLICAS
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_empresalocal o_emploc = new CN_sys_empresalocal();
        CN_sun_tipdoccom o_tipdoc = new CN_sun_tipdoccom();
        CN_mae_meses objMeses = new CN_mae_meses();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_EST_CARGOS BE_Registro = new BE_EST_CARGOS();
        List<BE_EST_CARGOSCAB> l_carcab = new List<BE_EST_CARGOSCAB>();
        List<BE_EST_CARGOSDET> l_cardet = new List<BE_EST_CARGOSDET>();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO
        DataTable dtForm = new DataTable();
        DataTable dtLocal = new DataTable();
        DataTable dtSer = new DataTable();
        DataTable dtDoc = new DataTable();
        DataTable dtTipCon = new DataTable();
        DataTable dtcliente = new DataTable();
        DataTable dtMeses = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[7, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[12, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "EF1234567890" + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;

        public FrmGeneraCargos()
        {
            InitializeComponent();
        }

        private void FrmGeneraCargos_Load(object sender, EventArgs e)
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
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        void ConfigurarFormulario()
        {
            this.Height = 629;
            this.Width = 881;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();

            arrCabeceraFlex1[0, 0] = "Nº Doumento";
            arrCabeceraFlex1[0, 1] = "90";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_numpla";

            arrCabeceraFlex1[1, 0] = "Cliente";
            arrCabeceraFlex1[1, 1] = "300";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "n_id";

            arrCabeceraFlex1[2, 0] = "Tip. Doc.";
            arrCabeceraFlex1[2, 1] = "40";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "n_id";

            arrCabeceraFlex1[3, 0] = "Nº Documento";
            arrCabeceraFlex1[3, 1] = "100";
            arrCabeceraFlex1[3, 2] = "C";
            arrCabeceraFlex1[3, 3] = "";
            arrCabeceraFlex1[3, 4] = "n_id";

            arrCabeceraFlex1[4, 0] = "Imp. Bruto";
            arrCabeceraFlex1[4, 1] = "70";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.00";
            arrCabeceraFlex1[4, 4] = "n_id";

            arrCabeceraFlex1[5, 0] = "Imp. I.G.V.";
            arrCabeceraFlex1[5, 1] = "70";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "n_id";

            arrCabeceraFlex1[6, 0] = "Imp. Total";
            arrCabeceraFlex1[6, 1] = "70";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_id";

            arrCabeceraFlex1[7, 0] = "n_idcli";
            arrCabeceraFlex1[7, 1] = "0";
            arrCabeceraFlex1[7, 2] = "N";
            arrCabeceraFlex1[7, 3] = "";
            arrCabeceraFlex1[7, 4] = "n_id";

            arrCabeceraFlex1[8, 0] = "n_idtipdoc";
            arrCabeceraFlex1[8, 1] = "0";
            arrCabeceraFlex1[8, 2] = "N";
            arrCabeceraFlex1[8, 3] = "";
            arrCabeceraFlex1[8, 4] = "n_id";

            arrCabeceraFlex1[9, 0] = "n_idservicio";
            arrCabeceraFlex1[9, 1] = "0";
            arrCabeceraFlex1[9, 2] = "N";
            arrCabeceraFlex1[9, 3] = "";
            arrCabeceraFlex1[9, 4] = "n_id";

            arrCabeceraFlex1[10, 0] = "n_correlativo";
            arrCabeceraFlex1[10, 1] = "0";
            arrCabeceraFlex1[10, 2] = "N";
            arrCabeceraFlex1[10, 3] = "";
            arrCabeceraFlex1[10, 4] = "n_id";

            arrCabeceraFlex1[11, 0] = "Tip. Doc. Fac";
            arrCabeceraFlex1[11, 1] = "40";
            arrCabeceraFlex1[11, 2] = "C";
            arrCabeceraFlex1[11, 3] = "";
            arrCabeceraFlex1[11, 4] = "n_id";

            funFlex.FlexMostrarDatos(FgPlacas, arrCabeceraFlex1, dtLocal, 2, false);
            FgPlacas.AllowEditing = false;
        }
        void DataTableCargar()
        {
            CN_est_cargos objRegistros = new CN_est_cargos(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            dtLista = objRegistros.dtListar;
            objRegistros = null;

            CN_est_clientes o_cliente = new CN_est_clientes(STU_SISTEMA);
            o_cliente.STU_SISTEMA = STU_SISTEMA;
            o_cliente.Listar3(STU_SISTEMA.EMPRESAID);
            dtcliente = o_cliente.dtListar;
            o_cliente = null;

            CN_est_servicios o_ser = new CN_est_servicios(STU_SISTEMA);
            o_ser.STU_SISTEMA = STU_SISTEMA;
            dtSer = o_ser.Listar(STU_SISTEMA.EMPRESAID);
            o_ser = null;

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            objFormVis.mysConec = o_conec.mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(90, ref arrCabeceraDg1);

            objForm.mysConec = o_conec.mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(90);

            o_emploc.mysConec = o_conec.mysConec;
            dtLocal = o_emploc.Listar(STU_SISTEMA.EMPRESAID, 0);
                        
            o_tipdoc.mysConec = o_conec.mysConec;
            dtDoc = o_tipdoc.Listar();
            dtDoc = funDatos.DataTableFiltrar(dtDoc, "n_id IN(2, 4, 13, 83)");

            objMeses.mysConec = o_conec.mysConec;
            dtMeses = objMeses.Listar();
            o_conec = null;
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
            int n_row = 0;
            string c_dato = "";
            booAgregando = true;
            FgPlacas.Rows.Count = 2;

            l_carcab.Clear();
            l_cardet.Clear();

            CN_est_cargos objRegistros = new CN_est_cargos(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.TraerRegistro(n_IdRegistro);
            if (objRegistros.b_OcurrioError == false)
            {
                BE_Registro = objRegistros.e_Cargos;
                l_carcab = objRegistros.l_CargosCab;
                l_cardet = objRegistros.l_CargosDet;

                //BE_Registro.n_idemp
                //BE_Registro.n_id
                //BE_Registro.n_anotra
                //BE_Registro.n_mestra
                CboPla.SelectedValue = BE_Registro.n_idpla;
                TxtFchEmi.Text =  BE_Registro.d_fchemi.ToString("dd/MM/yyyy");
                TxtFchIni.Text = BE_Registro.d_fchini.ToString("dd/MM/yyyy");
                //BE_Registro.d_fchfin
                //BE_Registro.n_numsoc
                TxtImpBru.Text = BE_Registro.n_impbru.ToString("0.00");
                txtImpIgv.Text = BE_Registro.n_impigv.ToString("0.00");
                TxtImpTot.Text = BE_Registro.n_imptot.ToString("0.00");
                TxtObs.Text = BE_Registro.c_obs;
                LblNumRec.Text = BE_Registro.n_numrec.ToString();
            }
            objRegistros = null;
            MostrarDetalle();
        }

        void MostrarDetalle()
        {
            string c_dato = "";
            int n_row = 0;

            for (n_row = 0; n_row <= l_carcab.Count - 1; n_row++)
            {
                FgPlacas.Rows.Count = FgPlacas.Rows.Count + 1;

                c_dato = funDatos.DataTableBuscar(dtcliente, "n_id", "c_numdocide", l_carcab[n_row].n_idcli.ToString(), "N").ToString();
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 1, c_dato);

                c_dato = funDatos.DataTableBuscar(dtcliente, "n_id", "c_nom", l_carcab[n_row].n_idcli.ToString(), "N").ToString();
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 2, c_dato);

                //c_dato = funDatos.DataTableBuscar(dtcliente, "n_id", "n_idtipdoc", l_carcab[n_row].n_idcli.ToString(), "N").ToString();
                c_dato = funDatos.DataTableBuscar(dtDoc, "n_id", "c_abr", l_carcab[n_row].n_idtipdoc.ToString(), "N").ToString();
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 3, c_dato);

                c_dato = l_carcab[n_row].c_numser + "-" + l_carcab[n_row].c_numdoc;
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 4, c_dato);

                c_dato = l_carcab[n_row].n_impbru.ToString("0.00");
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 5, c_dato);

                c_dato = l_carcab[n_row].n_impigv.ToString("0.00");
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 6, c_dato);

                c_dato = l_carcab[n_row].n_imptot.ToString("0.00");
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 7, c_dato);

                c_dato = l_carcab[n_row].n_idcli.ToString();
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 8, c_dato);
                
                c_dato = l_carcab[n_row].n_idtipdoc.ToString();
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 9, c_dato);

                //c_dato = funDatos.DataTableBuscar(dtcliente, "n_id", "n_tipdocfac", l_carcab[n_row].n_idcli.ToString(), "N").ToString();
                c_dato = funDatos.DataTableBuscar(dtDoc, "n_id", "c_abr", l_carcab[n_row].n_idtipdocfac.ToString(), "N").ToString();
                FgPlacas.SetData(FgPlacas.Rows.Count - 1, 12, c_dato);
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
            booAgregando = false;
        }
        void Blanquea()
        {
            TxtFchEmi.Text = "";
            TxtFchIni.Text = "";
            TxtImpBru.Text = "";
            txtImpIgv.Text = "";
            TxtImpTot.Text = "";
            TxtObs.Text = "";
            CboPla.SelectedValue = 0;
            LblNumRec.Text = "";
            TxtObs.CharacterCasing = CharacterCasing.Upper;

            FgPlacas.Rows.Count = 2;
        }
        void Bloquea()
        {
            TxtFchEmi.Enabled = !TxtFchEmi.Enabled;
            TxtFchIni.Enabled = !TxtFchIni.Enabled;
            TxtImpBru.Enabled = !TxtImpBru.Enabled;
            txtImpIgv.Enabled = !txtImpIgv.Enabled;
            TxtImpTot.Enabled = !TxtImpTot.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
            CboPla.Enabled = !CboPla.Enabled;

            FgPlacas.Rows.Count = 2;

            CmdCargar.Enabled = !CmdCargar.Enabled;
            CmdDelPla.Enabled = !CmdDelPla.Enabled;
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
            FgPlacas.AllowEditing = true;
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            CN_est_cargos objRegistros = new CN_est_cargos(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            if (objRegistros.TieneCargosPagados(intIdRegistro) == true)
            {
                MessageBox.Show("¡ El cargo tiene documentos pagados, no se puede eliminar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult; 
            }


            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
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
            }
            objRegistros = null;
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

            CN_est_cargos objRegistros = new CN_est_cargos(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;

            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(BE_Registro, l_carcab, l_cardet);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(BE_Registro, l_carcab, l_cardet);
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
            l_carcab.Clear();
            l_cardet.Clear();
            
            if (n_QueHace == 1)
            {
                n_id = 0;
            }
            else
            {
                n_id = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            }

            BE_Registro.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_Registro.n_id = n_id;
            BE_Registro.n_idpla = Convert.ToInt16(CboPla.SelectedValue);
            BE_Registro.n_idano = STU_SISTEMA.ANOTRABAJO;
            BE_Registro.n_idmes = STU_SISTEMA.MESTRABAJO;
            BE_Registro.d_fchemi = Convert.ToDateTime(TxtFchEmi.Text);
            BE_Registro.d_fchini = Convert.ToDateTime(TxtFchIni.Text);
            BE_Registro.n_impbru = Convert.ToDouble(TxtImpBru.Text);
            BE_Registro.n_impigv = Convert.ToDouble(txtImpIgv.Text);
            BE_Registro.n_imptot = Convert.ToDouble(TxtImpTot.Text);
            BE_Registro.n_numrec = FgPlacas.Rows.Count - 2;
            BE_Registro.c_obs = TxtObs.Text;

            for (n_row = 2; n_row <= FgPlacas.Rows.Count - 1; n_row++)
            {
                BE_EST_CARGOSCAB e_carcab = new BE_EST_CARGOSCAB();

                e_carcab.n_idemp = STU_SISTEMA.EMPRESAID;
                e_carcab.n_idcar = n_id;

                c_dato = FgPlacas.GetData(n_row, 11).ToString();
                e_carcab.n_id = Convert.ToInt16(c_dato);

                e_carcab.n_idpla = Convert.ToInt16(CboPla.SelectedValue);
                
                
                e_carcab.n_idtipdoc = 83;
                
                c_dato = FgPlacas.GetData(n_row,4).ToString().Substring(0,4);
                e_carcab.c_numser = c_dato;

                c_dato = FgPlacas.GetData(n_row,4).ToString().Substring(5,10);
                e_carcab.c_numdoc = c_dato;

                e_carcab.d_fchemi = Convert.ToDateTime(TxtFchEmi.Text);
                
                c_dato = FgPlacas.GetData(n_row, 8).ToString();
                e_carcab.n_idcli = Convert.ToInt16(c_dato);

                c_dato = FgPlacas.GetData(n_row, 5).ToString();
                e_carcab.n_impbru = Convert.ToDouble(c_dato);

                c_dato = FgPlacas.GetData(n_row, 6).ToString();
                e_carcab.n_impigv = Convert.ToDouble(c_dato);

                c_dato = FgPlacas.GetData(n_row, 7).ToString();
                e_carcab.n_imptot = Convert.ToDouble(c_dato);

                e_carcab.n_impsal = Convert.ToDouble(c_dato);

                c_dato = FgPlacas.GetData(n_row, 9).ToString();
                e_carcab.n_idtipdocfac = Convert.ToInt16(c_dato);

                l_carcab.Add(e_carcab);
            }

            string c_fchcini = "";
            string c_fchcfin = "";
            DataTable dtres = new DataTable();

            for (n_row = 2; n_row <= FgPlacas.Rows.Count - 1; n_row++)
            {
                BE_EST_CARGOSDET e_cardet = new BE_EST_CARGOSDET();
                
                c_dato = FgPlacas.GetData(n_row, 8).ToString();

                dtres = funDatos.DataTableFiltrar(dtcliente, "n_id = " + c_dato + "");

                c_fchcini = Convert.ToDateTime(dtres.Rows[0]["d_fching"]).ToString("dd/MM/yyyy").Substring(0, 2) +"/"+  DateTime.Now.Month.ToString("00") +"/"+ DateTime.Now.Year.ToString("0000");
                if (c_fchcini == "31/11/2018") { c_fchcini = "30/11/2018"; }
                if (c_fchcini == "31/02/2019") { c_fchcini = "28/02/2019"; }
                if (c_fchcini == "30/02/2019") { c_fchcini = "28/02/2019"; }
                if (c_fchcini == "29/02/2019") { c_fchcini = "28/02/2019"; }
                if (c_fchcini == "31/04/2019") { c_fchcini = "30/04/2019"; }
                if (c_fchcini == "31/06/2019") { c_fchcini = "30/06/2019"; }
                if (c_fchcini == "31/09/2019") { c_fchcini = "30/09/2019"; }
                if (c_fchcini == "31/11/2019") { c_fchcini = "30/11/2019"; }
                //c_fchcini = Convert.ToDateTime(dtres.Rows[0]["d_fching"]).ToString("dd/MM/yyyy");
                c_fchcfin = Convert.ToDateTime(c_fchcini).AddDays(30).ToString("dd/MM/yyyy");
                e_cardet.n_idcar = n_id;

                c_dato = FgPlacas.GetData(n_row, 11).ToString();
                e_cardet.n_idcab = Convert.ToInt16(c_dato);

                c_dato = FgPlacas.GetData(n_row, 10).ToString();
                e_cardet.n_idser= Convert.ToInt16(c_dato);

                c_dato = funDatos.DataTableBuscar(dtSer, "n_id", "n_idunimed", c_dato, "N").ToString();
                e_cardet.n_idunimed = Convert.ToInt16(c_dato);

                c_dato = FgPlacas.GetData(n_row, 5).ToString();
                e_cardet.n_impbru = Convert.ToDouble(c_dato);

                c_dato = FgPlacas.GetData(n_row, 6).ToString();
                e_cardet.n_impigv = Convert.ToDouble(c_dato);

                c_dato = FgPlacas.GetData(n_row, 7).ToString();
                e_cardet.n_imptot = Convert.ToDouble(c_dato);

                e_cardet.c_desser = "CARGO ABONADO DEL: " + c_fchcini + " AL: " + c_fchcfin;
                l_cardet.Add(e_cardet);
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt16(CboPla.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre de laplaya !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboPla.Focus();
                return booEstado;
            }
            if (Convert.ToDouble(TxtImpTot.Text) == 0)
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
                CN_est_cargos objRegistros = new CN_est_cargos(STU_SISTEMA);
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
        private void FrmGeneraCargos_Activated(object sender, EventArgs e)
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
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }
        private void CmdCargar_Click(object sender, EventArgs e)
        {
            string c_dato = "";
            string c_numser = "0001";
            int n_row = 0;
            int n_numdoc = 0;
            int n_corr = 0;
            double n_imptot = 0;
            double n_impigv = 0;
            double n_impbru = 0;
            DataTable dtCli = new DataTable();
            CN_est_clientes o_Cliente = new CN_est_clientes(STU_SISTEMA);
            o_Cliente.STU_SISTEMA = STU_SISTEMA;
            o_Cliente.Consulta1(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboPla.SelectedValue));
            dtCli = o_Cliente.dtListar;

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            o_tipdoc.mysConec = o_conec.mysConec;
            string c_numdoc = o_tipdoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 83, "0001");
            n_numdoc = Convert.ToInt32(c_numdoc);
            o_conec = null;

            FgPlacas.Rows.Count = 2;
            //n_numdoc = 1;
            n_corr = 1;
            LblNumRec.Text = (dtCli.Rows.Count - 1).ToString();

            if (dtCli.Rows.Count != 0)
            { 
                for(n_row =0; n_row<=dtCli.Rows.Count-1;n_row++)
                {
                    FgPlacas.Rows.Count = FgPlacas.Rows.Count + 1;

                    c_dato = dtCli.Rows[n_row]["c_numdocide"].ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 1, c_dato);

                    c_dato = dtCli.Rows[n_row]["c_nom"].ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 2, c_dato);
                    
                    c_dato = funDatos.DataTableBuscar(dtDoc, "n_id", "c_abr", "83", "N").ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 3, c_dato);             

                    c_dato = c_numser + "-" + n_numdoc.ToString("0000000000");
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 4, c_dato);

                    n_impbru = 0; n_impigv = 0;
                    n_imptot = Convert.ToDouble(funFunciones.NulosN(dtCli.Rows[n_row]["n_importe"]));
                    if (n_imptot != 0)
                    { 
                        n_impbru = (n_imptot / 1.18);
                        n_impigv = (n_imptot - n_impbru);
                    }
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 5, n_impbru.ToString("0.00"));

                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 6, n_impigv.ToString("0.00"));

                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 7, n_imptot.ToString("0.00"));

                    c_dato = Convert.ToInt16(dtCli.Rows[n_row]["n_id"]).ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 8, c_dato);

                    c_dato = Convert.ToInt16(dtCli.Rows[n_row]["n_tipdocfac"]).ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 9, c_dato);

                    c_dato = Convert.ToInt16(dtCli.Rows[n_row]["n_idser"]).ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 10, c_dato);

                    c_dato = n_corr.ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 11, c_dato);

                    c_dato = dtCli.Rows[n_row]["c_docabr"].ToString();
                    FgPlacas.SetData(FgPlacas.Rows.Count - 1, 12, c_dato);

                    n_numdoc = n_numdoc + 1;
                    n_corr = n_corr+1;
                }
            }

            n_imptot = funFlex.FlexSumarCol(FgPlacas, 5, 2, FgPlacas.Rows.Count - 1);
            n_impigv = funFlex.FlexSumarCol(FgPlacas, 6, 2, FgPlacas.Rows.Count - 1);
            n_impbru = funFlex.FlexSumarCol(FgPlacas, 7, 2, FgPlacas.Rows.Count - 1);

            TxtImpBru.Text = n_imptot.ToString("0.00");
            txtImpIgv.Text = n_impigv.ToString("0.00");
            TxtImpTot.Text = n_impbru.ToString("0.00");
            o_Cliente = null;
        }
        private void FrmGeneraCargos_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            if (booAgregando == true) { return; }

            CN_est_cargos objRegistros = new CN_est_cargos(STU_SISTEMA);
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

        private void emitirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_est_cargos o_liq = new CN_est_cargos(STU_SISTEMA);
            int n_idreg = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString()); ;
            o_liq.STU_SISTEMA = STU_SISTEMA;
            o_liq.ImprimirCargo(n_idreg);
        }
    }
}
