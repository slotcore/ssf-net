using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Estacionamiento;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Estacionamiento;
using SIAC_Negocio.Planilla;
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
using SIAC_Entidades.Ventas;
using SIAC_Negocio.Ventas;
using System.Configuration;

namespace SIAC_NET_Estacionamientos.Formularios
{
    public partial class FrmManClientes : Form
    {
        // VARIABLES PUBLICAS
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_DeDonde = 0;                                                      // 1 = DESDE EL MENU DEL SISTEMA:    2 = DESDE OTRO FORMULARIO
        public int n_TipoCli = 0;                                                      // 1 = INDICA QUE SE REGISTRARA UN CLIENTE CON DNI;   2 = SE REGISTRARA UN CLIENTE CON RUC
        public string c_numplaca = "";                                                 // ALMACENA EL NUMERO DE PLACA PAR GUARADARLO                
        public int n_tipserv = 0;                                                      // ID DEL TIPO DE  SERVICIO
        public int n_idpla = 0;                                                        // ID DE LA PLAYA
        public int N_CLIENTE_ID = 0;
        public string C_CLIENTE_NOM = "";
        public string C_CLIENTE_NUMDOC = "";
        public int N_IDCAJERO = 0;
        public string C_CAJERO = "";
        public bool b_GENERARASIENTO;                                                  // INDICA SI SE GENERARA ASIENTO CONTABLE CUANDO SE REGISTRE LA VENTA                                             

        int N_IDCAJERO2 = 0;
        string C_CAJERO2 = "";

        public int n_idcargogenerado = 0;                                                        // ESTA  VARIABLE ALMACENA EL ID DEL CARGO GENERADO EN LA TABLA EST_OTROCARGOSCAB

        string c_NUMSERFAC = "";
        string c_NUMSERBOL = "";
        string c_NUMSERTIC = "";
        int  N_VISTAPREVIA = 1;
        string C_LOCAL = "";

        // OBJETOS LOCALES
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_empresalocal o_emploc = new CN_sys_empresalocal();
        CN_sys_setup o_setup = new CN_sys_setup();
        CN_sun_tipdoccom o_tipdoc = new CN_sun_tipdoccom();
        CN_sun_tipcontribuyente objTipCon = new CN_sun_tipcontribuyente();
        CN_sun_tipdocide objDocIden = new CN_sun_tipdocide();
        CN_sun_distritos objDis = new CN_sun_distritos();
        CN_sun_provincia objPro = new CN_sun_provincia();
        CN_sun_departamentos objDep = new CN_sun_departamentos();
        CN_est_tipocliente obj_TipCli = new CN_est_tipocliente();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_sun_tipdoccom objTipDoc2 = new CN_sun_tipdoccom();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_NumeroLetra funLet = new Cls_NumeroLetra();
        Genericas funGen = new Genericas();

        // ENTIDADES LOCALES
        BE_EST_CLIENTES BE_Registro = new BE_EST_CLIENTES();
        List<BE_EST_CLIENTESPLACAS> l_clipla = new List<BE_EST_CLIENTESPLACAS>();
        BE_EST_OTROCARGOSCAB e_carcab = new BE_EST_OTROCARGOSCAB();
        List<BE_EST_OTROCARGOSDET> l_cardet = new List<BE_EST_OTROCARGOSDET>();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO
        DataTable dtForm = new DataTable();
        DataTable dtLocal = new DataTable();
        DataTable dtSer = new DataTable();
        DataTable dtSer2 = new DataTable();
        DataTable dtDoc = new DataTable();
        DataTable dtTipCon = new DataTable();
        DataTable dtDocIden = new DataTable();
        DataTable dtDep = new DataTable();
        DataTable dtPro = new DataTable();
        DataTable dtDis = new DataTable();
        DataTable dtTipCli = new DataTable();
        DataTable dtPlacas = new DataTable();
        DataTable dtCajero = new DataTable();
        DataTable dtLocSet = new DataTable();
        DataTable dtDocGen = new DataTable();
        DataTable dtsetup = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[9, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[2, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "EF1234567890." + (char)8;                                        // + (char)8;
        string strNumerovalidos2 = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;

        public FrmManClientes()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void FrmManClientes_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboTipCon, dtTipCon, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtDocIden, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDocExp, dtDocGen, "n_id", "c_des");
            
            funDatos.ComboBoxCargarDataTable(CboSer, dtSer, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboServ2, dtSer2, "n_id", "c_des");
            
            funDatos.ComboBoxCargarDataTable(CboTipDocVen, dtDoc, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboLoc, dtLocal, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipCli, dtTipCli, "n_id", "c_des");

            funDatos.ComboBoxCargarDataTable(CboDep, dtDep, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboPro, dtPro, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboDis, dtDis, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 626;
            this.Width = 881;
            
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();

            arrCabeceraFlex1[0, 0] = "Nº Placa";
            arrCabeceraFlex1[0, 1] = "80";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_numpla";

            arrCabeceraFlex1[1, 0] = "n_id";
            arrCabeceraFlex1[1, 1] = "0";
            arrCabeceraFlex1[1, 2] = "N";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "n_id";

            funFlex.FlexMostrarDatos(FgPlacas, arrCabeceraFlex1, dtPlacas, 1, false);

            string c_nomarc = @"C:\SSF-NET\estacionamiento.ini";
            n_idpla = Convert.ToInt16(funDatos.IniLeerSeccion(c_nomarc, "INFORMACION", "LOCAL").ToString());

            //dtLocSet = funGen.DataTableFiltrar(dtLocSet, "n_idloc = " + n_idpla.ToString() + "");
            ////N_IDSERVICIODEFAULT = Convert.ToInt16(dtLocSet.Rows[0]["n_idserdef"]);
            //c_NUMSERFAC = dtLocSet.Rows[0]["c_numserfac"].ToString();
            //c_NUMSERBOL = dtLocSet.Rows[0]["c_numserbol"].ToString();
            //c_NUMSERTIC = dtLocSet.Rows[0]["c_numsertik"].ToString();
            
            string c_nomarc2 = ConfigurationManager.AppSettings["PathIniFile"];
            c_NUMSERFAC = funGen.IniLeerSeccion(c_nomarc2, "SISTEMA", "SERFAC").ToString();
            c_NUMSERBOL = funGen.IniLeerSeccion(c_nomarc2, "SISTEMA", "SERBOL").ToString();
            c_NUMSERTIC = funGen.IniLeerSeccion(c_nomarc2, "SISTEMA", "SERVAL").ToString();

            //N_IDDOCUMENTODEFAULT = Convert.ToInt16(dtLocSet.Rows[0]["n_iddocdef"]);
            N_VISTAPREVIA = Convert.ToInt16(dtLocSet.Rows[0]["n_vispre"]);
            C_LOCAL = dtLocSet.Rows[0]["c_locdes"].ToString();

            DataTable dtresult = new DataTable();

            dtresult = funDatos.DataTableFiltrar(dtCajero, "n_idusu = " + N_IDCAJERO + "");
            if (dtresult.Rows.Count != 0)
            { 
                N_IDCAJERO2 = Convert.ToInt16(dtresult.Rows[0]["n_id"]);
                C_CAJERO2 = dtresult.Rows[0]["c_cajapenom"].ToString();
            }
            //entDat.n_idcaj = N_IDCAJERO;
            //entDat.c_cajnom = C_CAJERO;
        }
        void DataTableCargar()
        {
            CN_est_clientes objRegistros = new CN_est_clientes(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.Listar2();
            dtLista = objRegistros.dtListar;
            dtPlacas = objRegistros.dtListarPlacas;
            objRegistros = null;

            CN_est_servicios o_ser = new CN_est_servicios(STU_SISTEMA);
            o_ser.STU_SISTEMA = STU_SISTEMA;
            dtSer = o_ser.Listar(STU_SISTEMA.EMPRESAID);
            o_ser = null;

            CN_est_servicios o_ser2 = new CN_est_servicios(STU_SISTEMA);
            o_ser2.STU_SISTEMA = STU_SISTEMA;
            dtSer2 = o_ser2.Listar(STU_SISTEMA.EMPRESAID);
            o_ser2 = null;

            CN_est_cajeros o_caj = new CN_est_cajeros(STU_SISTEMA);
            o_caj.STU_SISTEMA = STU_SISTEMA;
            o_caj.Listar(STU_SISTEMA.EMPRESAID);
            dtCajero = o_caj.dtListar;
            o_caj = null;

            CN_est_localsetup o_locset = new CN_est_localsetup(STU_SISTEMA);
            o_locset.STU_SISTEMA = STU_SISTEMA;
            dtLocSet = o_locset.Listar(STU_SISTEMA.EMPRESAID);
            o_locset = null;

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            objFormVis.mysConec = o_conec.mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(88, ref arrCabeceraDg1);

            objForm.mysConec = o_conec.mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(88);

            o_emploc.mysConec = o_conec.mysConec;
            dtLocal = o_emploc.Listar(STU_SISTEMA.EMPRESAID, 0);

            objDocIden.mysConec = o_conec.mysConec;
            dtDocIden = objDocIden.Listar();                                // CARGAMOS DOCUMENTO DE IDENTIDAD

            objTipCon.mysConec = o_conec.mysConec;
            dtTipCon = objTipCon.Listar();                                  // CARGAMOS TIPO DE CONTRIBUYENTE

            o_tipdoc.mysConec = o_conec.mysConec;
            dtDoc = o_tipdoc.Listar();
            dtDoc = funDatos.DataTableFiltrar(dtDoc, "n_id IN(2, 4, 90)");

            objTipDoc2.mysConec = o_conec.mysConec;
            dtDocGen = objTipDoc2.Listar();
            dtDocGen = funDatos.DataTableFiltrar(dtDocGen, "n_id IN(2, 4, 90)");
             
            objDep.mysConec = o_conec.mysConec;
            dtDep = objDep.Listar();                                        // CARGAMOS TODOS LOS DEPARTAMENTOS

            objPro.mysConec = o_conec.mysConec;
            dtPro = objPro.Listar();                                        // CARGAMOS TODAS LAS PROVINCIAS

            objDis.mysConec = o_conec.mysConec;
            dtDis = objDis.Listar();                                        // CARGAMOS TODOS LOS DISTRITOS

            obj_TipCli.mysConec = o_conec.mysConec;
            obj_TipCli.Listar();
            dtTipCli = obj_TipCli.dtListar;
            
            o_setup.mysConec=o_conec.mysConec;
            dtsetup = o_setup.TraerRegistro();
            if (Convert.ToInt16(dtsetup.Rows[0]["n_genasiconven"]) == 1)
            {
                b_GENERARASIENTO = false;
            }
            else 
            {
                b_GENERARASIENTO = true;
            }
            
            o_setup = null;
            o_conec = null;
        }
        void ListarItems()
        {
            if (STU_SISTEMA.USUARIOPERFIL == 10)
            {
                //DataTable dtres = new DataTable();
                //dtres = funDatos.DataTableFiltrar(dtres)
                //    n_idpla
                //int n_idpla = Convert.ToInt16(funDatos.DataTableBuscar(dtCajero, "n_idusu", "n_idloc", STU_SISTEMA.USUARIOID.ToString(), "N"));

                dtLista = funDatos.DataTableFiltrar(dtLista, "n_idpla = "+ n_idpla.ToString() +"");
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
            DataTable DtFiltro = new DataTable();
            DataTable dtRes = new DataTable();
            int n_row = 0;
            string c_dato = "";
            booAgregando = true;
            FgPlacas.Rows.Count = 1;

            CN_est_clientes objRegistros = new CN_est_clientes(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;

            objRegistros.TraerRegistro(n_IdRegistro);
            if (objRegistros.b_OcurrioError == false)
            {
                string strCadenaFiltro = "n_iddep = " + BE_Registro.n_iddep.ToString() + "";
                DtFiltro = funDatos.DataTableFiltrar(dtPro, strCadenaFiltro);
                funDatos.ComboBoxCargarDataTable(CboPro, DtFiltro, "n_id", "c_des");

                strCadenaFiltro = "n_iddep = " + BE_Registro.n_iddep.ToString() + " AND n_idpro = " + BE_Registro.n_idpro.ToString() + "";
                DtFiltro = funDatos.DataTableFiltrar(dtDis, strCadenaFiltro);
                funDatos.ComboBoxCargarDataTable(CboDis, DtFiltro, "n_id", "c_des");

                BE_Registro = objRegistros.e_Cliente;
                dtPlacas = objRegistros.dtListarPlacas;

                CboTipCon.SelectedValue = BE_Registro.n_idtipcon;
                CboLoc.SelectedValue = BE_Registro.n_idpla;
                
                // CboSer.Enabled = true;
                
                dtRes = funDatos.DataTableFiltrar(dtSer, "n_idpla = " + Convert.ToInt16(CboLoc.SelectedValue) + "");
                funDatos.ComboBoxCargarDataTable(CboSer, dtRes, "n_id", "c_des");

                CboTipDoc.SelectedValue = BE_Registro.n_idtipdocide;
                TxtNumDoc.MaxLength = 15;
                if (Convert.ToInt16(CboTipDoc.SelectedValue) == 2) { TxtNumDoc.MaxLength = 8; }
                if (Convert.ToInt16(CboTipDoc.SelectedValue) == 4) { TxtNumDoc.MaxLength = 11; }

                TxtNumDoc.Text = BE_Registro.c_numdocide;
                TxtNomEmp.Text =BE_Registro.c_nom;
                TxtApe1.Text =BE_Registro.c_ape1;
                TxtApe2.Text =BE_Registro.c_ape2;
                TxtNom1.Text =BE_Registro.c_nom1;
                TxtNom2.Text =BE_Registro.c_nom2;
                TxtDir.Text =BE_Registro.c_dir;
                CboDep.SelectedValue = BE_Registro.n_iddep;
                CboPro.SelectedValue = BE_Registro.n_idpro;
                CboDis.SelectedValue = BE_Registro.n_iddis;
                TxtNumTel.Text = BE_Registro.c_numtel;
                CboSer.SelectedValue = BE_Registro.n_idser;
                CboTipDocVen.SelectedValue = BE_Registro.n_tipdocfac;
                CboTipCli.SelectedValue = BE_Registro.n_idtipcli;
                TxtImporte.Text = BE_Registro.n_importe.ToString("0.00");
                TxtFchIng.Text = BE_Registro.d_fching.ToString("dd/MM/yyyy");
                if (dtPlacas.Rows.Count != 0)
                {
                    for (n_row = 0; n_row <= dtPlacas.Rows.Count - 1; n_row++)
                    {
                        FgPlacas.Rows.Count = FgPlacas.Rows.Count + 1;
                        c_dato = dtPlacas.Rows[n_row]["c_numpla"].ToString();
                        FgPlacas.SetData(FgPlacas.Rows.Count - 1, 1, c_dato);
                    }
                }
            }
            objRegistros = null;
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
            l_clipla.Clear();
            CboTipCon.Focus();
            FgPlacas.AllowEditing = true;
            TxtFchIng.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CboTipCon.SelectedValue = 1;
            CboTipDoc.SelectedValue = 2;
            TxtNumDoc.MaxLength = 15;
            if (Convert.ToInt16(CboTipDoc.SelectedValue) == 2) { TxtNumDoc.MaxLength = 8; }
            if (Convert.ToInt16(CboTipDoc.SelectedValue) == 4) { TxtNumDoc.MaxLength = 11; }

            CboLoc.SelectedValue = n_idpla;
            CboSer.Enabled = true;
            DataTable dtRes = new DataTable();
            dtRes = funDatos.DataTableFiltrar(dtSer, "n_idpla = " + Convert.ToInt16(CboLoc.SelectedValue) + "");
            funDatos.ComboBoxCargarDataTable(CboSer, dtRes, "n_id", "c_des");
            TxtNomEmp.Enabled = false;
            TxtNumDoc.Focus();
            booAgregando = false;
        }
        void Blanquea()
        {
            CboTipCon.SelectedValue = 0;
            CboTipDoc.SelectedValue = 0;
            TxtNumDoc.Text = "";
            TxtNomEmp.Text = "";
            TxtApe1.Text = "";
            TxtApe2.Text = "";
            TxtNom1.Text = "";
            TxtNom2.Text = "";
            TxtDir.Text = "";
            CboDep.SelectedValue = 0;
            CboPro.SelectedValue = 0;
            CboDis.SelectedValue = 0;
            TxtNumTel.Text = "";
            CboSer.SelectedValue = 0;
            CboTipDocVen.SelectedValue = 0;
            TxtImporte.Text = "";
            CboLoc.SelectedValue = 0;
            CboTipCli.SelectedValue = 0;

            TxtNomEmp.CharacterCasing = CharacterCasing.Upper;
            TxtApe1.CharacterCasing = CharacterCasing.Upper;
            TxtApe2.CharacterCasing = CharacterCasing.Upper;
            TxtNom1.CharacterCasing = CharacterCasing.Upper;
            TxtNom2.CharacterCasing = CharacterCasing.Upper;
            TxtDir.CharacterCasing = CharacterCasing.Upper;

            FgPlacas.Rows.Count = 1;
        }
        void Bloquea()
        {
            CboTipCon.Enabled = !CboTipCon.Enabled;
            CboTipDoc.Enabled = !CboTipDoc.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtNomEmp.Enabled = !TxtNomEmp.Enabled;
            TxtApe1.Enabled = !TxtApe1.Enabled;
            TxtApe2.Enabled = !TxtApe2.Enabled;
            TxtNom1.Enabled = !TxtNom1.Enabled;
            TxtNom2.Enabled = !TxtNom2.Enabled;
            TxtDir.Enabled = !TxtDir.Enabled;
            CboDep.Enabled = !CboDep.Enabled;
            CboPro.Enabled = !CboPro.Enabled;
            CboDis.Enabled = !CboDis.Enabled;
            TxtNumTel.Enabled = !TxtNumTel.Enabled;
            CboSer.Enabled = !CboSer.Enabled;
            CboTipDocVen.Enabled = !CboTipDocVen.Enabled;
            CboLoc.Enabled = !CboLoc.Enabled;
            CboTipCli.Enabled = !CboTipCli.Enabled;
            TxtImporte.Enabled = !TxtImporte.Enabled;

            CmdAddPla.Enabled = !CmdAddPla.Enabled;
            CmdDelPla.Enabled = !CmdDelPla.Enabled;

            CmdBusCli.Enabled = !CmdBusCli.Enabled;

            TxtFchIng.Enabled = !TxtFchIng.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar2.Enabled = !ToolEliminar2.Enabled;
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
            l_clipla.Clear();
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            CboTipCon.Focus();
            FgPlacas.AllowEditing = true;
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                CN_est_clientes objRegistros = new CN_est_clientes(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    //objRegistros.mysConec = mysConec;
                    objRegistros.Listar2();
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

            CN_est_clientes objRegistros = new CN_est_clientes(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.l_ClientePlaca = l_clipla;
            objRegistros.e_carcab = e_carcab;
            objRegistros.l_cardet = l_cardet;

            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(BE_Registro);
                n_idcargogenerado = objRegistros.n_idcargogenerado;
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(BE_Registro);
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
            BE_Registro.n_idtipcon = Convert.ToInt16(CboTipCon.SelectedValue);
            BE_Registro.n_idpla = Convert.ToInt16(CboLoc.SelectedValue);
            BE_Registro.n_idtipdocide = Convert.ToInt16(CboTipDoc.SelectedValue);
            BE_Registro.c_numdocide = TxtNumDoc.Text;
            BE_Registro.c_nom = TxtNomEmp.Text;
            BE_Registro.c_ape1 = TxtApe1.Text;
            BE_Registro.c_ape2 = TxtApe2.Text;
            BE_Registro.c_nom1 = TxtNom1.Text;
            BE_Registro.c_nom2 = TxtNom2.Text;
            BE_Registro.c_dir = TxtDir.Text;
            BE_Registro.n_iddep = Convert.ToInt16(CboDep.SelectedValue);
            BE_Registro.n_idpro = Convert.ToInt16(CboPro.SelectedValue);
            BE_Registro.n_iddis = Convert.ToInt16(CboDis.SelectedValue);
            BE_Registro.c_numtel = TxtNumTel.Text;
            BE_Registro.n_idser = Convert.ToInt16(CboSer.SelectedValue);
            BE_Registro.n_tipdocfac = Convert.ToInt16(CboTipDocVen.SelectedValue);
            BE_Registro.n_idtipcli = Convert.ToInt16(CboTipCli.SelectedValue);
            BE_Registro.n_importe = Convert.ToDouble(funFunciones.NulosN(TxtImporte.Text));
            BE_Registro.d_fching = Convert.ToDateTime(TxtFchIng.Text);

            l_clipla.Clear();
            for(n_row =1; n_row<= FgPlacas.Rows.Count-1;n_row++)
            {
                BE_EST_CLIENTESPLACAS e_clipla = new BE_EST_CLIENTESPLACAS();

                c_dato = funFunciones.NulosC( FgPlacas.GetData(n_row,1));
                if (c_dato != "")
                { 
                    e_clipla.n_idcli = n_id;
                    e_clipla.c_numpla  =c_dato;
                    l_clipla.Add(e_clipla);
                }
            }

            l_cardet.Clear();
            
            e_carcab.n_idemp = STU_SISTEMA.EMPRESAID;
            e_carcab.n_id = 0;
            e_carcab.n_idpla =  Convert.ToInt16(CboLoc.SelectedValue);
            e_carcab.n_idtipdoc = 83;
            e_carcab.c_numser = "0001";

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            o_tipdoc = new CN_sun_tipdoccom();
            o_tipdoc.mysConec = o_conec.mysConec;
            string c_numdoc = o_tipdoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 83, "0001");
            o_conec = null;
            o_tipdoc = null;

            e_carcab.c_numdoc = c_numdoc;
            e_carcab.d_fchemi = DateTime.Now;
            e_carcab.n_idcli = 0;
            e_carcab.n_impbru = (Convert.ToDouble(funFunciones.NulosN(TxtImporte.Text)) / 1.18);
            e_carcab.n_impigv = (Convert.ToDouble(funFunciones.NulosN(TxtImporte.Text)) / 1.18) - Convert.ToDouble(funFunciones.NulosN(TxtImporte.Text));
            e_carcab.n_imptot = Convert.ToDouble(funFunciones.NulosN(TxtImporte.Text));
            e_carcab.n_impsal = Convert.ToDouble(funFunciones.NulosN(TxtImporte.Text));
            e_carcab.n_idtipdocfac = Convert.ToInt16(CboTipDocVen.SelectedValue);
            e_carcab.n_iddocven = 0;
            e_carcab.d_fchpag = DateTime.Now;
            e_carcab.n_pagado = 2;
            e_carcab.n_liquidado = 1;

            BE_EST_OTROCARGOSDET e_cardet = new BE_EST_OTROCARGOSDET();
            e_cardet.n_idcar = 0;
            e_cardet.n_idser = Convert.ToInt16(CboSer.SelectedValue);
            e_cardet.n_idunimed = 0;
            e_cardet.n_impbru = (Convert.ToDouble(funFunciones.NulosN(TxtImporte.Text)) / 1.18);
            e_cardet.n_impigv = (Convert.ToDouble(funFunciones.NulosN(TxtImporte.Text)) / 1.18) - Convert.ToDouble(funFunciones.NulosN(TxtImporte.Text));
            e_cardet.n_imptot =  Convert.ToDouble(funFunciones.NulosN(TxtImporte.Text));
            e_cardet.c_desser = "COBRANZA AL ABONADO DEL DIA " + Convert.ToDateTime(TxtFchIng.Text) + " AL " + Convert.ToDateTime(TxtFchIng.Text).AddDays(30).ToString().Substring(0,10);
            l_cardet.Add(e_cardet);
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (funFunciones.NulosC(TxtNomEmp.Text) == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNomEmp.Focus();
                return booEstado;
            }
            if (funFunciones.NulosC(TxtNumDoc.Text) == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento de identidad del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumDoc.Focus();
                return booEstado;
            }
            if (funFunciones.NulosC(TxtDir.Text) == "")
            {
                MessageBox.Show("¡ No ha especificado la direccion del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtDir.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboTipCli.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboLoc.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboLoc.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre de la playa !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboLoc.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboSer.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el servicio que se le asignara al cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboSer.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboTipDocVen.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el documento de cobranza para el cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTipDocVen.Focus();
                return booEstado;
            }

            if (Convert.ToInt16(CboTipCli.SelectedValue) == 2)
            { 
                if (Convert.ToDouble(funFunciones.NulosN(TxtImporte.Text)) == 0)
                {
                    MessageBox.Show("¡ No ha especificado el importe del servicio que se cobrara al abonado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    TxtImporte.Focus();
                    return booEstado;
                }
            }

            //if ((OptTipCob1.Checked == false) && (OptTipCob2.Checked == false))
            //{
            //    MessageBox.Show("¡ No ha especificado el tipo de cobranza para este local !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    OptTipCob1.Focus();
            //    return booEstado;
            //}



            //if (TxtFac.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado el numero de serie para la factura de este local !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    TxtFac.Focus();
            //    return booEstado;
            //}
            //if (TxtBol.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado el numero de serie para la boleta de este local !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    TxtBol.Focus();
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
            
        }
        private void ToolEliminar_Click(object sender, EventArgs e)
        {
            
        }
        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            if (Grabar() == true)
            {
                if (n_QueHace == 1)
                { 
                    if (Convert.ToInt16(CboTipCli.SelectedValue) == 2)
                    { 
                        //DialogResult Rpta2 = MessageBox.Show("! Desea generar el cargo del mes para el abonado ? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        //if (DialogResult.Yes == Rpta2)
                        //{
                        //    GenerarCargo(TxtFchIng.Text, Convert.ToDouble(TxtImporte.Text), BE_Registro.n_id);
                        //}
                        MessageBox.Show("¡ Se va a generar un cargo para el asociado !","SIAC - ESTACIONAMIENTO", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        GenerarCargo(TxtFchIng.Text, Convert.ToDouble(TxtImporte.Text), BE_Registro.n_id);
                    }
                }

                if (n_DeDonde == 1)
                {
                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    CN_est_clientes objRegistros = new CN_est_clientes(STU_SISTEMA);
                    objRegistros.STU_SISTEMA = STU_SISTEMA;
                    objRegistros.Listar2();
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
                else
                {
                    N_CLIENTE_ID = BE_Registro.n_id;
                    C_CLIENTE_NOM = BE_Registro.c_nom;
                    C_CLIENTE_NUMDOC = BE_Registro.c_numdocide;
                    this.Hide();
                }
            }
        }
        void GenerarCargo(string c_FechaIngreso, double n_Importe, int n_IdCliente)
        {
            BE_VTA_VENTAS e_Documento = new BE_VTA_VENTAS();
            List<BE_VTA_VENTASDET> l_DocumentoDet = new List<BE_VTA_VENTASDET>();
            List<BE_VTA_VENTASDOC> l_DetDoc = new List<BE_VTA_VENTASDOC>();
            List<BE_VTA_VENTASOCT> l_DetOCT = new List<BE_VTA_VENTASOCT>();
            List<BE_VTA_VENTASDAT> l_DetDat = new List<BE_VTA_VENTASDAT>();
            double douIGVTasa = 18;
            double n_tc = 3.653;

            l_DocumentoDet.Clear();
            l_DetDoc.Clear();
            l_DetOCT.Clear();

            e_Documento.n_id = 0;
            e_Documento.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Documento.n_anotra = STU_SISTEMA.ANOTRABAJO;
            e_Documento.n_idmes = STU_SISTEMA.MESTRABAJO;
            e_Documento.n_idlib = 14;
            e_Documento.c_numreg = "";
            e_Documento.n_idtippro = 23;
            e_Documento.n_idcli = n_IdCliente;
            e_Documento.n_idpunvencli = 0;
            e_Documento.n_idtipdoc = Convert.ToInt16(CboTipDocVen.SelectedValue);

            if (Convert.ToInt16(CboTipDocVen.SelectedValue) == 2) { e_Documento.c_numser = c_NUMSERFAC; }
            if (Convert.ToInt16(CboTipDocVen.SelectedValue) == 4) { e_Documento.c_numser = c_NUMSERBOL; }
            if (Convert.ToInt16(CboTipDocVen.SelectedValue) == 90) { e_Documento.c_numser = c_NUMSERTIC; }

            //string c_NUMSERFAC = "";
            //string c_NUMSERBOL = "";
            //string c_NUMSERTIC = "";
            //e_Documento.c_numdoc = "0000000001";

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            objTipDoc = new CN_sun_tipdoccom();
            objTipDoc.mysConec = o_conec.mysConec;
            e_Documento.c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboTipDocVen.SelectedValue), e_Documento.c_numser);
            o_conec = null;
            objTipDoc = null;
            
            if (e_Documento.n_idmes == 0)
            {
                e_Documento.d_fchreg = Convert.ToDateTime("01/01/" + e_Documento.n_anotra.ToString("0000"));
            }
            else
            {
                e_Documento.d_fchreg = Convert.ToDateTime("01/" + c_FechaIngreso.Substring(3, 2) + "/" + c_FechaIngreso.Substring(6, 4));
            }
            e_Documento.d_fchdoc = DateTime.Now;
            e_Documento.d_fchven = DateTime.Now; 
            e_Documento.n_idconpag = 1;
            e_Documento.n_idmon = 115;
            e_Documento.n_impbru = (Convert.ToDouble(n_Importe) / ((douIGVTasa / 100) + 1));
            e_Documento.n_impbru2 = 0;
            e_Documento.n_impbru3 = 0;
            e_Documento.n_impinaf = 0;
            e_Documento.n_impigv = (n_Importe - (n_Importe / ((douIGVTasa / 100) + 1)));
            e_Documento.n_impisc = 0;
            e_Documento.n_impotr = 0;
            e_Documento.n_imptotven = n_Importe;
            e_Documento.n_tc = n_tc;
            e_Documento.n_impsal = n_Importe;
            e_Documento.n_idven = 0;
            e_Documento.n_tasaigv = douIGVTasa;
            e_Documento.c_glosa = "COBRANZA ABONADO NUEVO";
            e_Documento.n_impsubtot = (n_Importe / ((douIGVTasa / 100) + 1));
            e_Documento.n_pordsc = 0;
            e_Documento.n_idtipope = 1;

            e_Documento.n_idtipdocref = 0;
            e_Documento.n_iddocref = 0;

            e_Documento.c_serdocref = "";
            e_Documento.c_numdocref = "";

            string c_mon = "SOLES.";
            int N_UNIMED = 26;
            //if (Convert.ToDouble(CboMoneda.SelectedValue) == 115) { c_mon = "SOLES."; }
            //if (Convert.ToDouble(CboMoneda.SelectedValue) == 151) { c_mon = "DOLARES AMERICANOS."; }
            e_Documento.c_numlet = funLet.Convertir(n_Importe.ToString(), true, c_mon);

            e_Documento.n_oriitem = 1;             // INDICAMOS QUE LA VENTA NO TIENE GUIA DE REMISION
            e_Documento.n_anulado = 0;
            e_Documento.c_motnc = "";

            e_Documento.n_idforpag = 1;            // INDICAMOS QUE LA FORMA DE PAGO ES EN EFECTIVO 
            e_Documento.n_idtarcre = 0;            // NO HAY TARJETA DE CREDITO

            // PREPARAMOS EL DETALLE DE LA VENTA
            BE_VTA_VENTASDET BE_Detalle = new BE_VTA_VENTASDET();
            int N_SERVICIO = Convert.ToInt16(CboSer.SelectedValue);
            string C_SERVICIO = CboSer.Text;

            BE_Detalle.n_idvta = e_Documento.n_id;
            BE_Detalle.n_canpro = 1;

            BE_Detalle.n_iditem = N_SERVICIO;

            BE_Detalle.n_idunimed = N_UNIMED;
            BE_Detalle.n_preunibru = (n_Importe / ((douIGVTasa / 100) + 1));
            BE_Detalle.n_preuninet = (n_Importe / ((douIGVTasa / 100) + 1));
            BE_Detalle.n_imptot = (n_Importe / ((douIGVTasa / 100) + 1)); 

            DateTime d_fchfin = Convert.ToDateTime(TxtFchIng.Text);
            d_fchfin  = d_fchfin.AddDays(30);

            BE_Detalle.c_desusu = "CARGO DEL ABONADO DEL : " + TxtFchIng.Text + " AL : " + d_fchfin.ToString("dd/MM/yyyy");
            BE_Detalle.n_idtipven = 0;
            BE_Detalle.n_pordsc = 0;
            BE_Detalle.n_porigv = douIGVTasa;
            BE_Detalle.n_preuninetigv = n_Importe;
            BE_Detalle.n_imptotigv = n_Importe;
            BE_Detalle.n_idtipafeigv = 1;
            BE_Detalle.c_datadi = "";
            l_DocumentoDet.Add(BE_Detalle);

            l_DetOCT.Clear();
            BE_VTA_VENTASOCT entOC = new BE_VTA_VENTASOCT();

            ////  1001 - Total valor de venta - operaciones gravadas
            entOC.n_idvta = 0;
            entOC.n_idcon = 1;
            entOC.n_importe = (n_Importe / ((douIGVTasa / 100) + 1));
            l_DetOCT.Add(entOC);

            l_DetDat.Clear();
            BE_VTA_VENTASDAT entDat = new BE_VTA_VENTASDAT();
            entDat.n_idvta = 0;
            //entDat.n_idcaj = N_IDCAJERO;
            entDat.n_idcaj = N_IDCAJERO2;
            //entDat.c_cajnom = C_CAJERO;
            entDat.c_cajnom = STU_SISTEMA.USUARIOALIAS;
            entDat.n_idloc = Convert.ToInt16(CboLoc.SelectedValue);
            entDat.c_locdes = CboLoc.Text;
            entDat.h_horemi = DateTime.Now.ToString("HH:mm:ss");
            if (FgPlacas.Rows.Count == 1)
            {
                entDat.c_numpla = "";
            }
            else
            { 
                entDat.c_numpla = FgPlacas.GetData(1,1).ToString();
            }
            entDat.c_horini = "";
            entDat.c_horfin = "";
            entDat.c_tiempousu = "";
            l_DetDat.Add(entDat);

            CN_est_conecta o_conec2 = new CN_est_conecta(STU_SISTEMA);
            CN_vta_ventas o_venta = new CN_vta_ventas();
            o_venta.mysConec = o_conec2.mysConec;
            o_conec2 = null;

            o_venta.LstDetalle = l_DocumentoDet;
            o_venta.LstDocumentos = l_DetDoc;
            o_venta.LstDetalleOCT = l_DetOCT;
            o_venta.LstDatos = l_DetDat;
            o_venta.STU_SISTEMA = STU_SISTEMA;
            o_venta.b_GenerarAsiento = b_GENERARASIENTO;
            o_venta.Insertar(e_Documento);

            if (o_venta.booOcurrioError == false)
            {
                CN_est_conecta o_conec3 = new CN_est_conecta(STU_SISTEMA);
                CN_est_otrocargoscab o_carcab = new CN_est_otrocargoscab();
                o_carcab.mysConec = o_conec3.mysConec;
                o_carcab.ActualizarDocVenta(n_idcargogenerado, Convert.ToInt32(o_venta.n_IdGenerado), DateTime.Now.ToString());

                CN_est_movimientos o_movi = new CN_est_movimientos(STU_SISTEMA);
                o_movi.STU_SISTEMA = STU_SISTEMA;
                string c_dato = "";
                //string c_dato = TxtNomEmp.Text + "|" + DateTime.Now.ToString("dd/MM/yyyy") + "|" + C_LOCAL + "|" + C_CAJERO + e_Documento.c_numser + "-" + e_Documento.c_numdoc + "|" + e_Documento.n_imptotven.ToString("0.00") + "|" + CboTipDoc.Text;
                o_movi.ImprimirComprobantePago(STU_SISTEMA.EMPRESAID, Convert.ToInt32(o_venta.n_IdGenerado), c_dato, 0, N_VISTAPREVIA, 1);
                o_movi = null;
                o_conec3 = null;
            }
        }
        private void ToolCancelar_Click(object sender, EventArgs e)
        {
            if (n_DeDonde == 1)
            { 
                Cancelar();
            }
            if (n_DeDonde == 2)
            {
                this.Close();
            }
        }
        private void ToolSalir_Click(object sender, EventArgs e)
        {
            objFormVis = null;
            this.Close();
        }
        private void FrmManClientes_Activated(object sender, EventArgs e)
        {
            //if (n_DeDonde == 2) { return; }
            if (booSeEjecuto == false)
            {
                if (n_DeDonde == 1)
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
                else
                {
                    if (n_QueHace == 1) { return; }
                    Nuevo();
                    if (n_TipoCli == 1)
                    {
                        CboTipCon.SelectedValue = 1;
                        //CboTipDoc.SelectedValue = 2;
                        CboTipDocVen.SelectedValue = 4;
                    }
                    if (n_TipoCli == 2)
                    {
                        CboTipCon.SelectedValue = 7;
                        //CboTipDoc.SelectedValue = 4;
                        CboTipDocVen.SelectedValue = 2;
                    }

                    CboTipCli.SelectedValue = 1;
                    CboLoc.SelectedValue = n_idpla;
                    CboSer.SelectedValue = n_tipserv;

                    FgPlacas.Rows.Count = FgPlacas.Rows.Count + 1;
                    FgPlacas.SetData(1, 1, c_numplaca);
                    TxtNumDoc.Focus();
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
        private void CboTipCon_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtNumDoc_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtNomEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtApe1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtApe2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtNom1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtNom2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtDir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CboDep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboPro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboDis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtNumTel_KeyPress(object sender, KeyPressEventArgs e)
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
        private void CboSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboTipDocVen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strNumerovalidos2.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void CboDep_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();

            if (CboDep.SelectedValue != null)
            {
                booAgregando = true;
                string strCadenaFiltro = "n_iddep = " + CboDep.SelectedValue.ToString() + "";
                CboPro.Enabled = true;

                CboPro.SelectedValue = 0;
                CboDis.SelectedValue = 0;

                DtFiltro = funDatos.DataTableFiltrar(dtPro, strCadenaFiltro);
                funDatos.ComboBoxCargarDataTable(CboPro, DtFiltro, "n_id", "c_des");
                if (n_QueHace == 3)
                {
                    CboPro.Enabled = false;
                    CboDis.Enabled = false;
                }
                else
                {
                    CboPro.Enabled = true;
                    CboDis.Enabled = false;
                }

                booAgregando = false;
            }
        }
        private void CboPro_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();

            if (CboPro.SelectedValue != null)
            {
                string strCadenaFiltro = "n_iddep = " + CboDep.SelectedValue.ToString() + " AND n_idpro = " + CboPro.SelectedValue.ToString() + "";
                CboDis.Enabled = true;

                CboDis.SelectedValue = 0;

                DtFiltro = funDatos.DataTableFiltrar(dtDis, strCadenaFiltro);
                funDatos.ComboBoxCargarDataTable(CboDis, DtFiltro, "n_id", "c_des");

                if (n_QueHace == 3)
                {
                    CboDis.Enabled = false;
                }
                else
                {
                    CboDis.Enabled = true;
                }

            }
        }
        private void CboLoc_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            CboSer.Enabled = true;
            DataTable dtRes = new DataTable();
            dtRes = funDatos.DataTableFiltrar(dtSer, "n_idpla = " + Convert.ToInt16(CboLoc.SelectedValue) + "");
            funDatos.ComboBoxCargarDataTable(CboSer, dtRes, "n_id", "c_des");
        }
        private void CboTipCon_SelectedValueChanged(object sender, EventArgs e)
        {
            //dtTipCon
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();

            if (CboTipCon.SelectedValue != null)
            {
                string strCadenaFiltro = "n_id = " + CboTipCon.SelectedValue.ToString() + "";
                DtFiltro = funDatos.DataTableFiltrar(dtTipCon, strCadenaFiltro);

                if (n_QueHace == 3)
                {
                    TxtApe1.Enabled = false;
                    TxtApe2.Enabled = false;
                    TxtNom1.Enabled = false;
                    TxtNom2.Enabled = false;
                    TxtNomEmp.Enabled = false;
                }
                else
                {
                    if (Convert.ToInt16(DtFiltro.Rows[0]["n_tipo"]) == 1)
                    {

                        TxtApe1.Enabled = true;
                        TxtApe2.Enabled = true;
                        TxtNom1.Enabled = true;
                        TxtNom2.Enabled = true;
                        TxtNomEmp.Enabled = false;
                    }
                    else
                    {
                        TxtApe1.Enabled = false;
                        TxtApe2.Enabled = false;
                        TxtNom1.Enabled = false;
                        TxtNom2.Enabled = false;
                        TxtNomEmp.Enabled = true;
                    }
                    TxtNomEmp.Text = "";
                    TxtNom1.Text = "";
                    TxtNom2.Text = "";
                    TxtApe1.Text = "";
                    TxtApe2.Text = "";
                }

            }
        }
        private void TxtApe1_Validated(object sender, EventArgs e)
        {
            if (TxtApe1.Text != "")
            {
                TxtNomEmp.Text = TxtApe1.Text + " " + TxtApe2.Text + ", " + TxtNom1.Text + " " + TxtNom2.Text;
            }
        }
        private void TxtApe2_Validated(object sender, EventArgs e)
        {
            if (TxtApe1.Text != "")
            {
                TxtNomEmp.Text = TxtApe1.Text + " " + TxtApe2.Text + ", " + TxtNom1.Text + " " + TxtNom2.Text;
            }
        }
        private void TxtNom1_Validated(object sender, EventArgs e)
        {
            if (TxtApe1.Text != "")
            {
                TxtNomEmp.Text = TxtApe1.Text + " " + TxtApe2.Text + ", " + TxtNom1.Text + " " + TxtNom2.Text;
            }
        }
        private void TxtNom2_Validated(object sender, EventArgs e)
        {
            if (TxtApe1.Text != "")
            {
                TxtNomEmp.Text = TxtApe1.Text + " " + TxtApe2.Text + ", " + TxtNom1.Text + " " + TxtNom2.Text;
            }
        }

        private void CmdDelPla_Click(object sender, EventArgs e)
        {
            if (FgPlacas.Rows.Count == 1) { return; }
            FgPlacas.RemoveItem(FgPlacas.Row);
        }

        private void CmdAddPla_Click(object sender, EventArgs e)
        {
            string c_dato = funFunciones.NulosC(FgPlacas.GetData(FgPlacas.Rows.Count-1,1));
            if (c_dato == "") { return; }

            FgPlacas.Rows.Count = FgPlacas.Rows.Count + 1;
        }

        private void CmdBusCli_Click(object sender, EventArgs e)
        {
            string c_acteco = "";
            Helper.Cls_ServiciosSunat xFun = new Helper.Cls_ServiciosSunat();
            xFun.ConsultarRUC();

            //string c_acteco = xFun.Contribuyente_ActEco;
            string c_condic = xFun.Contribuyente_Condicion;
            string c_direcc = "";
            if ((xFun.Contribuyente_Direccion.Length != 0))
            {
                if ((xFun.Contribuyente_Direccion.Length != 1))
                { 
                    c_direcc = xFun.Contribuyente_Direccion.Substring(0,100);
                }
            }
            string c_estado = xFun.Contribuyente_Estado;
            string c_fchiniact = xFun.Contribuyente_FchIniAct;
            string c_nombre = xFun.Contribuyente_Nombre;
            string c_numruc = xFun.Contribuyente_NumRUC;
            string c_tipcon = xFun.Contribuyente_TipCon;
            xFun = null;
            if (c_numruc != "")
            {
                TxtNumDoc.Text = c_numruc;
                TxtNomEmp.Text = c_nombre;
                TxtDir.Text = c_direcc;
                CboTipDoc.SelectedValue = 4;

                int n_idtipcon = Convert.ToInt16(funDatos.DataTableBuscar(dtTipCon, "c_des", "n_id", c_tipcon, "C"));
                CboTipCon.SelectedValue = n_idtipcon;
                //CboCatEmp.SelectedValue = 1;

                if ((n_idtipcon == 1) || (n_idtipcon == 2))
                {
                    string[] dato = new string[10];
                    dato = c_nombre.Split(' ');
                    int n_row = 1;

                    if (n_row <= dato.Length) { TxtApe1.Text = funFunciones.NulosC(dato[0]); n_row = n_row + 1; }
                    if (n_row <= dato.Length) { TxtApe2.Text = funFunciones.NulosC(dato[1]); n_row = n_row + 1; }
                    if (n_row <= dato.Length) { TxtNom1.Text = funFunciones.NulosC(dato[2]); n_row = n_row + 1; }
                    if (n_row <= dato.Length) { TxtNom2.Text = funFunciones.NulosC(dato[3]); n_row = n_row + 1; }
                }
                TxtNomEmp.Text = c_nombre;
                //TxtNomCom.Text = c_nombre;
            }
            else
            {
                booSeEjecuto = true;
            }
        }

        private void emitirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_est_clientes o_cli = new CN_est_clientes(STU_SISTEMA);
            o_cli.STU_SISTEMA = STU_SISTEMA;
            int n_id = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            int n_idpla = Convert.ToInt32(funDatos.DataTableBuscar(dtLista,"n_id","n_idpla",n_id.ToString(),"N"));
            o_cli.ReporteClientes(STU_SISTEMA.EMPRESAID, n_idpla);
        }

        private void guiasDelMesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_est_clientes o_cli = new CN_est_clientes(STU_SISTEMA);
            o_cli.STU_SISTEMA = STU_SISTEMA;
            o_cli.ReporteClientes(STU_SISTEMA.EMPRESAID, 0);
        }

        private void eliminarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void anularClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnularCliente();
        }
        bool AnularCliente()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de anular el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                CN_est_clientes objRegistros = new CN_est_clientes(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;
                if (objRegistros.Anular(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se anulo con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    //objRegistros.mysConec = mysConec;
                    objRegistros.Listar2();
                    dtLista = objRegistros.dtListar;
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo anular el registro por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                objRegistros = null;
            }
            return booResult;
        }

        private void modificarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modificar();
        }

        private void activarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivarRegistro();

        }
        bool ActivarRegistro()
        {
            bool booResult = false;
            CN_est_clientes objRegistros = new CN_est_clientes(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            int n_IdRegistro = objRegistros.BuscarNoActivos(STU_SISTEMA.EMPRESAID, n_idpla);  // Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            if (n_IdRegistro == 0) { return booResult; }

            DialogResult Rpta = MessageBox.Show("Esta seguro de activar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Activar(n_IdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se activo con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    //objRegistros.mysConec = mysConec;
                    objRegistros.Listar2();
                    dtLista = objRegistros.dtListar;
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo activar el registro por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            objRegistros = null;
            return booResult;
        }

        private void CboTipDoc_SelectedValueChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            TxtNumDoc.MaxLength = 15;
            if (Convert.ToInt16(CboTipDoc.SelectedValue) == 2) { TxtNumDoc.MaxLength = 8; }
            if (Convert.ToInt16(CboTipDoc.SelectedValue) == 4) { TxtNumDoc.MaxLength = 11; }
        }

        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }
            if (funFunciones.NulosC(TxtNumDoc.Text) == "") { return; }

            CN_est_clientes o_cli = new CN_est_clientes(STU_SISTEMA);
            o_cli.STU_SISTEMA = STU_SISTEMA;
            if (o_cli.ExisteDocIdentidad(STU_SISTEMA.EMPRESAID, n_idpla, TxtNumDoc.Text, Convert.ToInt16(CboTipDoc.SelectedValue)) == true)
            {
                TxtNumDoc.Text = "";
                MessageBox.Show("¡ El numero de documento de identidad ya existe, ingrese otro!", "SIAC - ESTACIONAMIENTOS", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumDoc.Focus();
                return;
            }
        }

        private void ToolModificar2_ButtonClick(object sender, EventArgs e)
        {

        }

        private void FgPlacas_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }
            if (FgPlacas.Rows.Count == 1) {return;}

            string c_NumPlaca = funFunciones.NulosC(FgPlacas.GetData(FgPlacas.Row,1));
            if (funFunciones.NulosC(c_NumPlaca) == "") { return; }

            CN_est_clientes o_cli = new CN_est_clientes(STU_SISTEMA);
            o_cli.STU_SISTEMA = STU_SISTEMA;
            
            if (o_cli.ExisteNumPla(STU_SISTEMA.EMPRESAID, n_idpla, c_NumPlaca ) == true)
            {
                FgPlacas.SetData(FgPlacas.Row, 1, "");
                MessageBox.Show("¡ El numero de placa ingresado ya existe, ingrese otro!", "SIAC - ESTACIONAMIENTOS", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgPlacas.Focus();
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tab1.SelectedIndex = 0;
            Tab1.Enabled = true;
            ToolHerramientas.Enabled = true;
            panel5.Visible = false;
        }

        private void ToolGenCargo_Click(object sender, EventArgs e)
        {
            Tab1.SelectedIndex = 0;
            Tab1.Enabled = true;
            ToolHerramientas.Enabled = true;
            panel5.Left = ((this.Width - panel5.Width) / 2);
            panel5.Top = ((this.Height - panel5.Height) / 2);
            panel5.Visible = true;
            TxtNomCli2.Text = DgLista.Columns[6].CellValue(DgLista.Row).ToString();
            TxtFchIni.Text = DateTime.Now.ToString("dd/MM/yyyy");
            TxtFchFin.Text = Convert.ToDateTime(TxtFchIni.Text).AddDays(30).ToString().Substring(0, 10);
            DataTable dtRes = new DataTable();
            dtRes = funDatos.DataTableFiltrar(dtSer2, "n_idpla = " + Convert.ToInt16(n_idpla) + "");
            funDatos.ComboBoxCargarDataTable(CboServ2, dtRes, "n_id", "c_des");

            CboTipDocExp.Focus();
        }

        private void CmdExpArch_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(CboServ2.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el servicio que se va a crear !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboServ2.Focus();
                return;
            }

            if (Convert.ToInt16(CboTipDocExp.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el documento de cobranza para el cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboTipDocExp.Focus();
                return;
            }
            if (Convert.ToDouble(funFunciones.NulosN(TxtImporte2.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado el importe del servicio que se cobrara al abonado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtImporte2.Focus();
                return;
            }

            int n_IdReg = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            GenerarCargo2(TxtFchIni.Text, TxtFchFin.Text, Convert.ToDouble(TxtImporte2.Text), n_IdReg);
            MessageBox.Show("¡ El cargo se creo con exito !", "SIAC - ESTACIONAMIENTOS", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            button1_Click(sender, e);
        }
        void GenerarCargo2(string c_FechaInicio, string c_FechaFin, double n_Importe, int n_IdCliente)
        {
            // ******************************************
            // CARGAMOS LAS ENTIDADES DE LOS OTROS CARGOS
            l_cardet.Clear();

            e_carcab.n_idemp = STU_SISTEMA.EMPRESAID;
            e_carcab.n_id = 0;
            e_carcab.n_idpla = n_idpla;    ///Convert.ToInt16(CboLoc.SelectedValue);
            e_carcab.n_idtipdoc = 83;
            e_carcab.c_numser = "0001";

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            o_tipdoc = new CN_sun_tipdoccom();
            o_tipdoc.mysConec = o_conec.mysConec;
            string c_numdoc = o_tipdoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 83, "0001");
            o_conec = null;
            o_tipdoc = null;

            e_carcab.c_numdoc = c_numdoc;
            e_carcab.d_fchemi = DateTime.Now;
            e_carcab.n_idcli = 0;
            e_carcab.n_impbru = (Convert.ToDouble(funFunciones.NulosN(TxtImporte2.Text)) / 1.18);
            e_carcab.n_impigv = (Convert.ToDouble(funFunciones.NulosN(TxtImporte2.Text)) - (Convert.ToDouble(funFunciones.NulosN(TxtImporte2.Text)) / 1.18));
            e_carcab.n_imptot = Convert.ToDouble(funFunciones.NulosN(TxtImporte2.Text));
            e_carcab.n_impsal = Convert.ToDouble(funFunciones.NulosN(TxtImporte2.Text));
            e_carcab.n_idtipdocfac = Convert.ToInt16(CboTipDocExp.SelectedValue);
            e_carcab.n_iddocven = 0;
            e_carcab.d_fchpag = DateTime.Now;
            e_carcab.n_pagado = 2;
            e_carcab.n_liquidado = 1;

            BE_EST_OTROCARGOSDET e_cardet = new BE_EST_OTROCARGOSDET();
            e_cardet.n_idcar = 0;
            e_cardet.n_idser = Convert.ToInt16(CboSer.SelectedValue);
            e_cardet.n_idunimed = 0;
            e_cardet.n_impbru = (Convert.ToDouble(funFunciones.NulosN(TxtImporte2.Text)) / 1.18);
            e_cardet.n_impigv = (Convert.ToDouble(funFunciones.NulosN(TxtImporte2.Text)) - (Convert.ToDouble(funFunciones.NulosN(TxtImporte2.Text)) / 1.18));
            e_cardet.n_imptot = Convert.ToDouble(funFunciones.NulosN(TxtImporte2.Text));
            e_cardet.c_desser = "COBRANZA AL ABONADO DEL DIA " + c_FechaInicio + " AL " + c_FechaFin;
            l_cardet.Add(e_cardet);

            // **********************************
            // CARGAMOS LAS ENTIDADES DE LA VENTA
            BE_VTA_VENTAS e_Documento = new BE_VTA_VENTAS();
            List<BE_VTA_VENTASDET> l_DocumentoDet = new List<BE_VTA_VENTASDET>();
            List<BE_VTA_VENTASDOC> l_DetDoc = new List<BE_VTA_VENTASDOC>();
            List<BE_VTA_VENTASOCT> l_DetOCT = new List<BE_VTA_VENTASOCT>();
            List<BE_VTA_VENTASDAT> l_DetDat = new List<BE_VTA_VENTASDAT>();
            double douIGVTasa = 18;
            double n_tc = 3.653;

            l_DocumentoDet.Clear();
            l_DetDoc.Clear();
            l_DetOCT.Clear();

            e_Documento.n_id = 0;
            e_Documento.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Documento.n_anotra = STU_SISTEMA.ANOTRABAJO;
            e_Documento.n_idmes = STU_SISTEMA.MESTRABAJO;
            e_Documento.n_idlib = 14;
            e_Documento.c_numreg = "";
            e_Documento.n_idtippro = 23;
            e_Documento.n_idcli = n_IdCliente;
            e_Documento.n_idpunvencli = 0;
            e_Documento.n_idtipdoc = Convert.ToInt16(CboTipDocExp.SelectedValue);

            if (Convert.ToInt16(CboTipDocExp.SelectedValue) == 2) { e_Documento.c_numser = c_NUMSERFAC; }
            if (Convert.ToInt16(CboTipDocExp.SelectedValue) == 4) { e_Documento.c_numser = c_NUMSERBOL; }
            if (Convert.ToInt16(CboTipDocExp.SelectedValue) == 90) { e_Documento.c_numser = c_NUMSERTIC; }

            //CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            //objTipDoc = new CN_sun_tipdoccom();
            //objTipDoc.mysConec = o_conec.mysConec;
            e_Documento.c_numdoc = TxtNumDoc2.Text;
            //o_conec = null;
            //objTipDoc = null;

            if (e_Documento.n_idmes == 0)
            {
                e_Documento.d_fchreg = Convert.ToDateTime("01/01/" + e_Documento.n_anotra.ToString("0000"));
            }
            else
            {
                e_Documento.d_fchreg = Convert.ToDateTime("01/" + c_FechaInicio.Substring(3, 2) + "/" + c_FechaInicio.Substring(6, 4));
            }
            e_Documento.d_fchdoc = DateTime.Now;
            e_Documento.d_fchven = DateTime.Now;
            e_Documento.n_idconpag = 1;
            e_Documento.n_idmon = 115;
            e_Documento.n_impbru = (Convert.ToDouble(n_Importe) / ((douIGVTasa / 100) + 1));
            e_Documento.n_impbru2 = 0;
            e_Documento.n_impbru3 = 0;
            e_Documento.n_impinaf = 0;
            e_Documento.n_impigv = (n_Importe - (n_Importe / ((douIGVTasa / 100) + 1)));
            e_Documento.n_impisc = 0;
            e_Documento.n_impotr = 0;
            e_Documento.n_imptotven = n_Importe;
            e_Documento.n_tc = n_tc;
            e_Documento.n_impsal = n_Importe;
            e_Documento.n_idven = 0;
            e_Documento.n_tasaigv = douIGVTasa;
            e_Documento.c_glosa = "GENERACION DE CARGO A ABONADO";
            e_Documento.n_impsubtot = (n_Importe / ((douIGVTasa / 100) + 1));
            e_Documento.n_pordsc = 0;
            e_Documento.n_idtipope = 1;

            e_Documento.n_idtipdocref = 0;
            e_Documento.n_iddocref = 0;

            e_Documento.c_serdocref = "";
            e_Documento.c_numdocref = "";

            string c_mon = "SOLES.";
            int N_UNIMED = 26;
            //if (Convert.ToDouble(CboMoneda.SelectedValue) == 115) { c_mon = "SOLES."; }
            //if (Convert.ToDouble(CboMoneda.SelectedValue) == 151) { c_mon = "DOLARES AMERICANOS."; }
            e_Documento.c_numlet = funLet.Convertir(n_Importe.ToString(), true, c_mon);

            e_Documento.n_oriitem = 1;             // INDICAMOS QUE LA VENTA NO TIENE GUIA DE REMISION
            e_Documento.n_anulado = 0;
            e_Documento.c_motnc = "";

            e_Documento.n_idforpag = 1;            // INDICAMOS QUE LA FORMA DE PAGO ES EN EFECTIVO 
            e_Documento.n_idtarcre = 0;            // NO HAY TARJETA DE CREDITO

            // PREPARAMOS EL DETALLE DE LA VENTA
            BE_VTA_VENTASDET BE_Detalle = new BE_VTA_VENTASDET();
            int N_SERVICIO = Convert.ToInt16(CboSer.SelectedValue);
            string C_SERVICIO = CboSer.Text;

            BE_Detalle.n_idvta = e_Documento.n_id;
            BE_Detalle.n_canpro = 1;

            BE_Detalle.n_iditem = N_SERVICIO;

            BE_Detalle.n_idunimed = N_UNIMED;
            BE_Detalle.n_preunibru = (n_Importe / ((douIGVTasa / 100) + 1));
            BE_Detalle.n_preuninet = (n_Importe / ((douIGVTasa / 100) + 1));
            BE_Detalle.n_imptot = (n_Importe / ((douIGVTasa / 100) + 1));

            DateTime d_fchfin = Convert.ToDateTime(c_FechaFin);
            //DateTime d_fchfin = Convert.ToDateTime(TxtFchIng.Text);
            //d_fchfin = d_fchfin.AddDays(30);


            BE_Detalle.c_desusu = "CARGO DEL ABONADO DEL : " + c_FechaInicio + " AL : " + c_FechaFin;
            BE_Detalle.n_idtipven = 0;
            BE_Detalle.n_pordsc = 0;
            BE_Detalle.n_porigv = douIGVTasa;
            BE_Detalle.n_preuninetigv = n_Importe;
            BE_Detalle.n_imptotigv = n_Importe;
            BE_Detalle.n_idtipafeigv = 1;
            BE_Detalle.c_datadi = "";
            l_DocumentoDet.Add(BE_Detalle);

            l_DetOCT.Clear();
            BE_VTA_VENTASOCT entOC = new BE_VTA_VENTASOCT();

            ////  1001 - Total valor de venta - operaciones gravadas
            entOC.n_idvta = 0;
            entOC.n_idcon = 1;
            entOC.n_importe = (n_Importe / ((douIGVTasa / 100) + 1));
            l_DetOCT.Add(entOC);

            l_DetDat.Clear();
            BE_VTA_VENTASDAT entDat = new BE_VTA_VENTASDAT();
            entDat.n_idvta = 0;
            //entDat.n_idcaj = N_IDCAJERO;
            entDat.n_idcaj = N_IDCAJERO2;
            //entDat.c_cajnom = C_CAJERO;
            entDat.c_cajnom = STU_SISTEMA.USUARIOALIAS;
            entDat.n_idloc = Convert.ToInt16(CboLoc.SelectedValue);
            entDat.c_locdes = CboLoc.Text;
            entDat.h_horemi = DateTime.Now.ToString("HH:mm:ss");
            if (FgPlacas.Rows.Count == 1)
            {
                entDat.c_numpla = "";
            }
            else
            {
                entDat.c_numpla = FgPlacas.GetData(1, 1).ToString();
            }
            entDat.c_horini = "";
            entDat.c_horfin = "";
            entDat.c_tiempousu = "";
            l_DetDat.Add(entDat);

            CN_est_conecta o_conec2 = new CN_est_conecta(STU_SISTEMA);
            CN_vta_ventas o_venta = new CN_vta_ventas();
            CN_est_otrocargoscab o_otrocab = new CN_est_otrocargoscab();
            o_venta.mysConec = o_conec2.mysConec;
            o_otrocab.mysConec = o_conec2.mysConec;
            o_conec2 = null;

            // GRABAMOS EL NUEV CARGO
            //o_otrocab.mysConec = o_conec2.mysConec;
            e_carcab.n_idcli = n_IdCliente;
            if (o_otrocab.Insertar(e_carcab, l_cardet) == true)
            {
                n_idcargogenerado = o_otrocab.n_idcargogenerado;
            }
            else
            {
                MessageBox.Show("Ocurrio un error no se puede generar el cargo por el siguente motivo : " + o_otrocab.c_ErrorMensaje, "SIAC - ESTACIONAMIENTOS", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            // GRABAMOS LA VENTA
            o_venta.LstDetalle = l_DocumentoDet;
            o_venta.LstDocumentos = l_DetDoc;
            o_venta.LstDetalleOCT = l_DetOCT;
            o_venta.LstDatos = l_DetDat;
            o_venta.STU_SISTEMA = STU_SISTEMA;
            o_venta.Insertar(e_Documento);

            if (o_venta.booOcurrioError == false)
            {
                CN_est_conecta o_conec3 = new CN_est_conecta(STU_SISTEMA);
                CN_est_otrocargoscab o_carcab = new CN_est_otrocargoscab();
                o_carcab.mysConec = o_conec3.mysConec;
                o_carcab.ActualizarDocVenta(n_idcargogenerado, Convert.ToInt32(o_venta.n_IdGenerado), DateTime.Now.ToString());

                CN_est_movimientos o_movi = new CN_est_movimientos(STU_SISTEMA);
                o_movi.STU_SISTEMA = STU_SISTEMA;
                string c_dato = "";
                //string c_dato = TxtNomEmp.Text + "|" + DateTime.Now.ToString("dd/MM/yyyy") + "|" + C_LOCAL + "|" + C_CAJERO + e_Documento.c_numser + "-" + e_Documento.c_numdoc + "|" + e_Documento.n_imptotven.ToString("0.00") + "|" + CboTipDoc.Text;
                o_movi.ImprimirComprobantePago(STU_SISTEMA.EMPRESAID, Convert.ToInt32(o_venta.n_IdGenerado), c_dato, 0, N_VISTAPREVIA, 1);
                o_movi = null;
                o_conec3 = null;
            }
        }

        private void TxtImporte2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strNumerovalidos2.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void CboTipDocExp_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (Convert.ToInt16(CboTipDocExp.SelectedValue) == 0) { return; }

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            objTipDoc = new CN_sun_tipdoccom();
            objTipDoc.mysConec = o_conec.mysConec;

            if (Convert.ToInt16(CboTipDocExp.SelectedValue) == 2) { TxtNumSer.Text = c_NUMSERFAC; }
            if (Convert.ToInt16(CboTipDocExp.SelectedValue) == 4) { TxtNumSer.Text = c_NUMSERBOL; }
            if (Convert.ToInt16(CboTipDocExp.SelectedValue) == 90) { TxtNumSer.Text = c_NUMSERTIC; }
            TxtNumDoc2.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboTipDocExp.SelectedValue), TxtNumSer.Text);
            o_conec = null;
        }

        private void CboLoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
