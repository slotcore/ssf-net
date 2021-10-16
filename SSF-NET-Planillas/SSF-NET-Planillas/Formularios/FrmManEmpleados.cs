using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Planillas;
using SIAC_Entidades.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Planilla;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIAC_Datos.Models.Planillas;
using SIAC_Datos.Classes;
using SIAC_DATOS.Data;
using Helper.Classes;
using Helper.Service;
using Refit;
using System.Configuration;
using Helper.Classes.Login;

namespace SSF_NET_Planillas.Formularios
{
    public partial class FrmManEmpleados : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_mae_meses objMeses = new CN_mae_meses();
        //CN_pla_empleados objRegistros = new CN_pla_empleados();
        CN_sun_tipdocide objDocIde = new CN_sun_tipdocide();
        CN_mae_sexo objSex = new CN_mae_sexo();

        CN_sun_departamentos objResDep = new CN_sun_departamentos();
        CN_sun_distritos objResDis = new CN_sun_distritos();
        CN_sun_provincia objResPro = new CN_sun_provincia();

        CN_sun_departamentos objNacDep = new CN_sun_departamentos();
        CN_sun_distritos objNacDis = new CN_sun_distritos();
        CN_sun_provincia objNacPro = new CN_sun_provincia();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Helper.Cls_Controles funcon = new Helper.Cls_Controles();
        // ENTIDADES LOCALES
        BE_PLA_EMPLEADOS entRegistro = new BE_PLA_EMPLEADOS();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO

        DataTable dtForm = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtDocIde = new DataTable();
        DataTable dtSex = new DataTable();

        DataTable dtResDep = new DataTable();
        DataTable dtResDis = new DataTable();
        DataTable dtResPro = new DataTable();

        DataTable dtNacDep = new DataTable();
        DataTable dtNacDis = new DataTable();
        DataTable dtNacPro = new DataTable();

        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[15, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[4, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        const int id_Formulario = 96;

        bool flagFiltro = false;
        bool flagActivos = true;

        private ObservableListSource<PeriodoLaboral> PeriodoLaborals;

        public FrmManEmpleados()
        {
            InitializeComponent();
        }
        public FrmManEmpleados(bool mostrarActivos)
        {
            InitializeComponent();
            flagFiltro = true;
            flagActivos = mostrarActivos;
        }
        private void FrmManEmpleados_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtDocIde, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboSex, dtSex, "n_id", "c_des");

            funDatos.ComboBoxCargarDataTable(CboResDep, dtResDep, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboResPro, dtResPro, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboResDis, dtResDis, "n_id", "c_des");

            funDatos.ComboBoxCargarDataTable(CboNacDep, dtNacDep, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboNacPro, dtNacPro, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboNacDis, dtNacDis, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            //this.Height = 576;
            //this.Width = 873;

            //Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            //Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            //CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        void DataTableCargar()
        {
            //objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            CN_pla_empleados objRegistros = new CN_pla_empleados(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            if (flagFiltro)
            {
                if (flagActivos)
                {
                    if (objRegistros.ListarDetalladoActivo(STU_SISTEMA.EMPRESAID) == true)
                    {
                        dtLista = objRegistros.dtLista;
                    }
                }
                else
                {
                    if (objRegistros.ListarDetalladoInactivo(STU_SISTEMA.EMPRESAID) == true)
                    {
                        dtLista = objRegistros.dtLista;
                    }
                }
            }
            else
            {
                if (objRegistros.ListarDetallado(STU_SISTEMA.EMPRESAID) == true)
                {
                    dtLista = objRegistros.dtLista;
                }
            }
            objRegistros = null;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(id_Formulario, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(id_Formulario);

            objDocIde.mysConec = mysConec;
            dtDocIde = objDocIde.Listar();

            objSex.mysConec = mysConec;
            dtSex = objSex.Listar();

            objResDep.mysConec = mysConec;
            dtResDep = objResDep.Listar();

            objResDis.mysConec = mysConec;
            dtResDis = objResDis.Listar();

            objResPro.mysConec = mysConec;
            dtResPro = objResPro.Listar();
            
            objNacDep.mysConec = mysConec;
            dtNacDep = objNacDep.Listar();

            objResDis.mysConec = mysConec;
            dtNacDis = objResDis.Listar();

            objNacPro.mysConec = mysConec;
            dtNacPro = objNacPro.Listar();
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtLista.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtLista, true);
        }
        void Tab_Dimensionar(C1.Win.C1Command.C1DockingTab dokTab, int intAlto, int intAncho)
        {
            //Tab1.Height = intAlto;
            //Tab1.Width = intAncho;
        }
        void Tab_Posicionar(C1.Win.C1Command.C1DockingTab dokTab, int intPosX, int intPosY)
        {
            //dokTab.Left = intPosX;
            //dokTab.Top = intPosY;
        }
        void VerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            booAgregando = true;

            Tab2.SelectedIndex = 0;

            funDatos.ComboBoxCargarDataTable(CboResPro, dtResPro, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboResDis, dtResDis, "n_id", "c_des");

            funDatos.ComboBoxCargarDataTable(CboNacPro, dtNacPro, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboNacDis, dtNacDis, "n_id", "c_des");


            //objRegistros.mysConec = mysConec;
            CN_pla_empleados objRegistros = new CN_pla_empleados(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.TraerRegistro(n_IdRegistro);
            
            ChkAsigFam.Checked = false;
            ChkDes.Checked = false;
            ChkDestacado.Checked = false;

            if (objRegistros.b_OcurrioError == false)
            {
                entRegistro = objRegistros.entEmpleado;
                CboTipDoc.SelectedValue = entRegistro.n_idtipdocide;
                TxtNumDoc.Text = entRegistro.c_numdocide;
                TxtApePat.Text = entRegistro.c_ape1;
                TxtApeMat.Text = entRegistro.c_ape2;
                TxtNom1.Text = entRegistro.c_nom1;
                TxtNom2.Text = entRegistro.c_nom2;
                CboSex.SelectedValue = entRegistro.n_idsex;
                TxtDir.Text = entRegistro.c_dir;

                CboResDep.SelectedValue = entRegistro.n_idresdep;

                dtResult = funDatos.DataTableFiltrar(dtResPro, "n_iddep =" + Convert.ToInt32(CboResDep.SelectedValue) + "");
                funDatos.ComboBoxCargarDataTable(CboResPro, dtResult, "n_id", "c_des");
                CboResPro.SelectedValue = entRegistro.n_idrespro;
                if (n_QueHace != 3) 
                { 
                    if (entRegistro.n_idrespro != 0)
                    { CboResPro.Enabled = true; CboResDis.Enabled = true; }
                    else
                    { CboResPro.Enabled = false; }
                }

                dtResult = funDatos.DataTableFiltrar(dtResDis, "n_iddep =" + Convert.ToInt32(CboResDep.SelectedValue) + "");
                dtResult = funDatos.DataTableFiltrar(dtResult, "n_idpro =" + Convert.ToInt32(CboResPro.SelectedValue) + "");
                funDatos.ComboBoxCargarDataTable(CboResDis, dtResult, "n_id", "c_des");
                CboResDis.SelectedValue = entRegistro.n_idresdis;

                //if (n_QueHace != 3) 
                //{
                //    if (entRegistro.n_idresdis != 0)
                //    { CboResDis.Enabled = true; }
                //    else
                //    { CboResDis.Enabled = false; }
                //}

                TxtTel.Text = entRegistro.c_numtel;
                TxtMail.Text = entRegistro.c_email;

                CboNacDep.SelectedValue = entRegistro.n_idnacdep;
                if (n_QueHace != 3)
                {
                    if (entRegistro.n_idnacdep != 0)
                    { CboNacDep.Enabled = true; CboNacPro.Enabled = true; }
                    else
                    { CboNacPro.Enabled = false; }
                }

                dtResult = funDatos.DataTableFiltrar(dtNacPro, "n_iddep =" + Convert.ToInt32(CboNacDep.SelectedValue) + "");
                funDatos.ComboBoxCargarDataTable(CboNacPro, dtResult, "n_id", "c_des");
                CboNacPro.SelectedValue = entRegistro.n_idnacpro;
                if (n_QueHace != 3) 
                {
                    if (entRegistro.n_idnacpro != 0)
                    { CboNacPro.Enabled = true; CboNacDis.Enabled = true; }
                    else
                    { CboNacPro.Enabled = false; }
                }

                dtResult = funDatos.DataTableFiltrar(dtNacDis, "n_iddep =" + Convert.ToInt32(CboNacDep.SelectedValue) + "");
                dtResult = funDatos.DataTableFiltrar(dtResult, "n_idpro =" + Convert.ToInt32(CboNacPro.SelectedValue) + "");
                funDatos.ComboBoxCargarDataTable(CboNacDis, dtResult, "n_id", "c_des");
                CboNacDis.SelectedValue = entRegistro.n_idnacdis;
                //if (n_QueHace != 3) 
                //{
                //    if (entRegistro.n_idnacdis != 0)
                //    { CboNacDis.Enabled = true; }
                //    else
                //    { CboNacDis.Enabled = false; }
                //}

                CboNacDis.SelectedValue = entRegistro.n_idnacdis;
                TxtFchNac.Text = Convert.ToDateTime(entRegistro.d_fchnac).ToString("dd/MM/yyyy");
                TxtBas.Text = Convert.ToDouble(entRegistro.n_suebas).ToString("0.00");
                TxtImpHorNor.Text = Convert.ToDouble(entRegistro.n_imphornor).ToString("0.00");
                TxtImpHorExt.Text = Convert.ToDouble(entRegistro.n_imphorext).ToString("0.00");
                TxtNumEsa.Text = entRegistro.c_numesa;

                if (funFunciones.NulosC(entRegistro.d_fching) != "")
                { 
                    TxtFchIng.Text = Convert.ToDateTime(entRegistro.d_fching).ToString("dd/MM/yyyy");
                }

                if (funFunciones.NulosC(entRegistro.d_fchbaj) != "")
                {
                    TxtFchBaj2.CustomFormat = "dd/MM/yyyy";
                    TxtFchBaj2.Format = DateTimePickerFormat.Custom;
                    TxtFchBaj2.Text = Convert.ToDateTime(entRegistro.d_fchbaj).ToString("dd/MM/yyyy");
                }
                else
                {
                    funcon.dtpBlanquea(TxtFchBaj2);
                }

                if (entRegistro.n_asigfam == 1)
                {
                    ChkAsigFam.Checked = true;
                }
                if (entRegistro.n_destajo == 1)
                {
                    ChkDes.Checked = true;
                }
                if (entRegistro.n_destacado == 2)
                {
                    ChkDestacado.Checked = true;
                }
                //Carga detalles de periodo laboral
                PeriodoLaborals = ApplicationDbContext.PeriodoLaborals(n_IdRegistro);
                periodoLaboralBindingSource.DataSource = PeriodoLaborals;
            }
            objRegistros = null;
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
            Tab2.SelectedIndex = 0;
            CboTipDoc.Focus();
            PeriodoLaborals = new ObservableListSource<PeriodoLaboral>();
            periodoLaboralBindingSource.DataSource = PeriodoLaborals;
        }
        void Blanquea()
        {
            CboTipDoc.SelectedValue = 0;
            TxtNumDoc.Text = "";
            TxtApePat.Text = "";
            TxtApeMat.Text = "";
            TxtNom1.Text = "";
            TxtNom2.Text = "";
            CboSex.SelectedValue = 0;
            TxtDir.Text = "";
            CboResDep.SelectedValue = 0;
            CboResPro.SelectedValue = 0;
            CboResDis.SelectedValue = 0;
            TxtTel.Text = "";
            TxtMail.Text = "";
            CboNacDep.SelectedValue = 0;
            CboNacPro.SelectedValue = 0;
            CboNacDis.SelectedValue = 0;

            TxtFchNac.Text = "";

            TxtBas.Text = "";
            TxtImpHorExt.Text = "";
            TxtImpHorNor.Text = "";
            TxtFchIng.Text = "";
            TxtFchBaj2.Text = "";
            funcon.dtpBlanquea(TxtFchBaj2);

            ChkAsigFam.Checked = false;
            ChkDes.Checked = false;
            ChkDestacado.Checked = false;
        }
        void Bloquea()
        {
            CboTipDoc.Enabled = !CboTipDoc.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtApePat.Enabled = !TxtApePat.Enabled;
            TxtApeMat.Enabled = !TxtApeMat.Enabled;
            TxtNom1.Enabled = !TxtNom1.Enabled;
            TxtNom2.Enabled = !TxtNom2.Enabled;
            CboSex.Enabled = !CboSex.Enabled;
            TxtDir.Enabled = !TxtDir.Enabled;
            CboResDep.Enabled = !CboResDep.Enabled;
            //CboResPro.Enabled = !CboResPro.Enabled;
            //CboResDis.Enabled = !CboResDis.Enabled;
            TxtTel.Enabled = !TxtTel.Enabled;
            TxtMail.Enabled = !TxtMail.Enabled;
            CboNacDep.Enabled = !CboNacDep.Enabled;
            //CboNacPro.Enabled = !CboNacPro.Enabled;
            //CboNacDis.Enabled = !CboNacDis.Enabled;

            TxtFchIng.Enabled = !TxtFchIng.Enabled;
            TxtFchNac.Enabled = !TxtFchNac.Enabled;
            TxtNumEsa.Enabled = !TxtNumEsa.Enabled;

            TxtBas.Enabled = !TxtBas.Enabled;
            TxtImpHorExt.Enabled = !TxtImpHorExt.Enabled;
            TxtImpHorNor.Enabled = !TxtImpHorNor.Enabled;

            ChkAsigFam.Enabled = !ChkAsigFam.Enabled;
            ChkDes.Enabled = !ChkDes.Enabled;
            ChkDestacado.Enabled = !ChkDestacado.Enabled;
            //
            if (BtnAgregarPeriodo.Enabled == ComponentFactory.Krypton.Toolkit.ButtonEnabled.False)
            {
                BtnAgregarPeriodo.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;
            }
            else
            {
                BtnAgregarPeriodo.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            }
            if (BtnModificarPeriodo.Enabled == ComponentFactory.Krypton.Toolkit.ButtonEnabled.False)
            {
                BtnModificarPeriodo.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;
            }
            else
            {
                BtnModificarPeriodo.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            }
            if (BtnQuitarPeriodo.Enabled == ComponentFactory.Krypton.Toolkit.ButtonEnabled.False)
            {
                BtnQuitarPeriodo.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;
            }
            else
            {
                BtnQuitarPeriodo.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            }
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar2.Enabled = !ToolModificar2.Enabled;
            ToolEliminar2.Enabled = !ToolEliminar2.Enabled;
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

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);

            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            Tab2.SelectedIndex = 0;
            TxtNumDoc.Focus();
        }

        public async void SubirEmpleado()
        {
            try
            {
                int n_IdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

                CN_pla_empleados objRegistros = new CN_pla_empleados(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;
                objRegistros.TraerRegistro(n_IdRegistro);

                if (objRegistros.b_OcurrioError)
                {
                    throw new Exception("Error!!");
                }

                entRegistro = objRegistros.entEmpleado;
                var employee = new Employee
                {
                    IdentityDocumentId = 1,
                    Code = entRegistro.n_id.ToString(),
                    IdentityDocumentNumber = entRegistro.c_numdocide,
                    LastName = string.Format("{0} {1}", entRegistro.c_ape1, entRegistro.c_ape2),
                    FirstName = string.Format("{0} {1}", entRegistro.c_nom1, entRegistro.c_nom2),
                    HomeAddress = entRegistro.c_dir,
                    EmailAddress = entRegistro.c_email
                };

                Login login = new Login
                {
                    UserName = ConfigurationManager.AppSettings["ApiHostUser"],
                    Password = ConfigurationManager.AppSettings["ApiHostPassword"]
                };
                var accountService = RestService.For<IAccountService>(ConfigurationManager.AppSettings["ApiHostUrl"]);
                var tokenResult = await accountService.Login(login);

                var masterService = RestService.For<IMasterService>(ConfigurationManager.AppSettings["ApiHostUrl"]);
                var token = string.Format("Bearer {0}", tokenResult.Token);
                var employeeResult = await masterService.EmployeeCreate(employee, token);

                MessageBox.Show(string.Format("¡Se subió el empleado: {0} correctamente! ", employeeResult.FullName), "Subir Empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("¡Ocurrio un error: {0}! ", ex.Message), "Subir Empleado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                CN_pla_empleados objRegistros = new CN_pla_empleados(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;
                if (objRegistros.ELiminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    //objRegistros.mysConec = mysConec;
                    objRegistros.Listar(STU_SISTEMA.EMPRESAID);
                    if (objRegistros.b_OcurrioError == false)
                    {
                        dtLista = objRegistros.dtLista;
                    }
                    
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
            CboResDis.Enabled = false;
            CboResPro.Enabled = false;

            CboNacDis.Enabled = false;
            CboNacPro.Enabled = false;

            LblTitulo2.Text = "Detalle del Registro";
            Tab1.TabPages[0].Enabled = true;
            Tab1.SelectedIndex = 0;
            DgLista.Focus();
        }
        bool Grabar()
        {
            bool booResultado = false;
            CN_pla_empleados objRegistros = new CN_pla_empleados(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;

            if (CamposOK() == false)
            {
                return booResultado;
            }
            AsignarEntidad();

            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(entRegistro);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(entRegistro);
            }

            //Se graban los detalles
            foreach (var periodoLaboral in PeriodoLaborals)
            {
                periodoLaboral.n_idempleado = entRegistro.n_id;
                periodoLaboral.Save();
            }
            //Se eliminan los detalles borrados
            foreach (var periodoLaboral in PeriodoLaborals.GetRemoveItems())
            {
                periodoLaboral.Delete();
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
            if (n_QueHace == 2)
            {
                n_id = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            }

            entRegistro.n_idemp = STU_SISTEMA.EMPRESAID;
            entRegistro.n_id = n_id;
            entRegistro.n_idtipdocide = Convert.ToInt32(CboTipDoc.SelectedValue);
            entRegistro.c_numdocide = TxtNumDoc.Text;
            entRegistro.c_ape1 = TxtApePat.Text;
            entRegistro.c_ape2 = TxtApeMat.Text;
            entRegistro.c_nom1 = TxtNom1.Text;
            entRegistro.c_nom2 = TxtNom2.Text;
            entRegistro.d_fchnac = Convert.ToDateTime(TxtFchNac.Text);
            entRegistro.n_idsex = Convert.ToInt32(CboSex.SelectedValue);
            entRegistro.c_numtel = TxtTel.Text;
            entRegistro.c_numesa = TxtNumEsa.Text;
            entRegistro.d_fching = TxtFchIng.Value;
            
            if (ChkAsigFam.Checked == true)
            {
                entRegistro.n_asigfam = 1;
            }
            else
            {
                entRegistro.n_asigfam = 2;
            }
            
            entRegistro.n_suebas = Convert.ToDouble(funFunciones.NulosN(TxtBas.Text));
            //entRegistro.n_bon
            entRegistro.n_imphornor = Convert.ToDouble(funFunciones.NulosN(TxtImpHorNor.Text));
            entRegistro.n_imphorext = Convert.ToDouble(funFunciones.NulosN(TxtImpHorExt.Text));
            //entRegistro.n_activo

            if (ChkDes.Checked == true)
            {
                entRegistro.n_destajo= 1;
            }
            else
            {
                entRegistro.n_destajo= 2;
            }

            if (ChkDestacado.Checked == true)
            {
                entRegistro.n_destacado = 2;
            }
            else
            {
                entRegistro.n_destacado = 1;
            }
            
            entRegistro.c_dir = TxtDir.Text;
            entRegistro.c_email = TxtMail.Text;
            entRegistro.n_idnacpro = Convert.ToInt32(CboNacPro.SelectedValue);
            entRegistro.n_idnacdep = Convert.ToInt32(CboNacDep.SelectedValue);
            entRegistro.n_idnacdis = Convert.ToInt32(CboNacDis.SelectedValue);
            entRegistro.n_idrespro = Convert.ToInt32(CboResPro.SelectedValue);
            entRegistro.n_idresdep = Convert.ToInt32(CboResDep.SelectedValue);
            entRegistro.n_idresdis = Convert.ToInt32(CboResDis.SelectedValue);
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento de identidad del empleado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumDoc.Focus();
                return booEstado;
            }
            if (TxtDir.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la direccion del empleado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtDir.Focus();
                return booEstado;
            }
            if (TxtApePat.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el apellido paterno del empleado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtApePat.Focus();
                return booEstado;
            }
            if (TxtApeMat.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el apellido materno del empleado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtApeMat.Focus();
                return booEstado;
            }
            if (TxtNom1.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre del empleado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNom1.Focus();
                return booEstado;
            }
            if (Convert.ToInt32(CboSex.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el sexo del empleado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboSex.Focus();
                return booEstado;
            }
            //if (TxtTel.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado el numero de telefono del empleado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    TxtTel.Focus();
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
                //objRegistros.mysConec = mysConec;
                CN_pla_empleados objRegistros = new CN_pla_empleados(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;
                if (flagFiltro)
                {
                    if (flagActivos)
                    {
                        if (objRegistros.ListarDetalladoActivo(STU_SISTEMA.EMPRESAID) == true)
                        {
                            dtLista = objRegistros.dtLista;
                        }
                    }
                    else
                    {
                        if (objRegistros.ListarDetalladoInactivo(STU_SISTEMA.EMPRESAID) == true)
                        {
                            dtLista = objRegistros.dtLista;
                        }
                    }
                }
                else
                {
                    if (objRegistros.Listar(STU_SISTEMA.EMPRESAID) == true)
                    {
                        dtLista = objRegistros.dtLista;
                    }
                }
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
        private void FrmManEmpleados_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();
                if (flagFiltro)
                {
                    if (flagActivos)
                    {
                        this.Text = "PLANILLAS - MAESTRO DE EMPLEADOS - ACTIVOS";
                    }
                    else
                    {
                        this.Text = "PLANILLAS - MAESTRO DE EMPLEADOS - INACTIVOS";
                    }
                }

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
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
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
        private void Tab1_SelectedIndexChanging(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;

            if (n_QueHace != 3) { return; }

            if (tc.SelectedIndex == 1)
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
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void label12_Click(object sender, EventArgs e)
        {

        }
        private void label15_Click(object sender, EventArgs e)
        {

        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void CboTipDoc_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtApePat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtApeMat_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtNom1_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtNom2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CboSex_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtDir_KeyPress(object sender, KeyPressEventArgs e)
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
        private void CboResDep_KeyPress(object sender, KeyPressEventArgs e)
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
        private void CboResPro_KeyPress(object sender, KeyPressEventArgs e)
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
        private void CboResDis_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtTel_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtMail_KeyPress(object sender, KeyPressEventArgs e)
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
        private void CboNacDep_KeyPress(object sender, KeyPressEventArgs e)
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
        private void CboNacPro_KeyPress(object sender, KeyPressEventArgs e)
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
        private void CboNacDis_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtFchNac_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtBas_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtImpHorNor_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtImpHorExt_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtNumEsa_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtFchIng_KeyPress(object sender, KeyPressEventArgs e)
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
        private void FrmManEmpleados_Resize(object sender, EventArgs e)
        {
            //Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
        }
        private void CboResDep_SelectedValueChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            DataTable dtResult = new DataTable();
            booAgregando = true;
            dtResult = funDatos.DataTableFiltrar(dtResPro, "n_iddep =" + Convert.ToInt32(CboResDep.SelectedValue) + "");

            funDatos.ComboBoxCargarDataTable(CboResPro, dtResult, "n_id", "c_des");
            CboResPro.Enabled = true;
            booAgregando = false;
        }
        private void CboResPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }
            DataTable dtResult = new DataTable();
            booAgregando = true;
            dtResult = funDatos.DataTableFiltrar(dtResDis, "n_iddep =" + Convert.ToInt32(CboResDep.SelectedValue) + "");

            dtResult = funDatos.DataTableFiltrar(dtResult, "n_idpro =" + Convert.ToInt32(CboResPro.SelectedValue) + "");

            funDatos.ComboBoxCargarDataTable(CboResDis, dtResult, "n_id", "c_des");
            CboResDis.Enabled = true;
            booAgregando = false;
        }
        private void CboNacDep_SelectedValueChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            DataTable dtResult = new DataTable();
            booAgregando = true;
            dtResult = funDatos.DataTableFiltrar(dtNacPro, "n_iddep =" + Convert.ToInt32(CboNacDep.SelectedValue) + "");

            funDatos.ComboBoxCargarDataTable(CboNacPro, dtResult, "n_id", "c_des");
            CboNacPro.Enabled = true;
            booAgregando = false;
        }
        private void CboNacPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }
            DataTable dtResult = new DataTable();
            booAgregando = true;
            dtResult = funDatos.DataTableFiltrar(dtNacDis, "n_iddep =" + Convert.ToInt32(CboNacDep.SelectedValue) + "");

            dtResult = funDatos.DataTableFiltrar(dtResult, "n_idpro =" + Convert.ToInt32(CboNacPro.SelectedValue) + "");

            funDatos.ComboBoxCargarDataTable(CboNacDis, dtResult, "n_id", "c_des");
            CboNacDis.Enabled = true;
            booAgregando = false;
        }
        private void emitirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_pla_empleados xFunPla = new CN_pla_empleados(STU_SISTEMA);
            xFunPla.STU_SISTEMA = STU_SISTEMA;
            xFunPla.Reporte1();
        }
        private void guiasDelMesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSelMesCumple xFrm = new FrmSelMesCumple();
            xFrm.STU_SISTEMA = STU_SISTEMA;
            xFrm.mysConec = mysConec;
            xFrm.Show();
        }

        private void eliminarPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void desactivarPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DesactivarRegistro();
        }
        bool DesactivarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de desactivar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                CN_pla_empleados objRegistros = new CN_pla_empleados(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;
                // DESACTIVAMOS EL EMPLEADO
                if (objRegistros.Desactivar(intIdRegistro, 2) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se desactivo con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    //objRegistros.mysConec = mysConec;
                    if (flagFiltro)
                    {
                        if (flagActivos)
                        {
                            objRegistros.ListarDetalladoActivo(STU_SISTEMA.EMPRESAID);
                        }
                        else
                        {
                            objRegistros.ListarDetalladoInactivo(STU_SISTEMA.EMPRESAID);
                        }
                    }
                    else
                    {
                        objRegistros.Listar(STU_SISTEMA.EMPRESAID);
                    }

                    if (objRegistros.b_OcurrioError == false)
                    {
                        dtLista = objRegistros.dtLista;
                    }

                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo desactivar el registro por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                objRegistros = null;
            }
            return booResult;
        }

        private void ToolModificar2_ButtonClick(object sender, EventArgs e)
        {
            Modificar();
        }

        private void modficarEmpleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modificar();
        }

        private void activarEmpleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivarRegistro();
        }
        bool ActivarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de activar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                CN_pla_empleados objRegistros = new CN_pla_empleados(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;

                // DESACTIVAMOS EL EMPLEADO
                if (objRegistros.Desactivar(intIdRegistro, 1) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se activo con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    //objRegistros.mysConec = mysConec;
                    objRegistros.Listar(STU_SISTEMA.EMPRESAID);
                    if (objRegistros.b_OcurrioError == false)
                    {
                        dtLista = objRegistros.dtLista;
                    }

                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo activar el registro por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                objRegistros = null;
            }
            return booResult;
        }
        private void empleadosActivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //objRegistros.mysConec = mysConec;
            CN_pla_empleados objRegistros = new CN_pla_empleados(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.ListaActivos();
            objRegistros = null;
        }
        private void CmdCan_Click(object sender, EventArgs e)
        {
            Pan1.Visible = false;
            Pan1.Refresh();
        }
        private void darDeBajaEmpleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pan1.Left = ((this.Width - Pan1.Width) / 2);
            Pan1.Top = ((this.Height - Pan1.Height) / 2);

            BE_PLA_EMPLEADOS e_emp = new BE_PLA_EMPLEADOS();
            CN_pla_empleados objRegistros = new CN_pla_empleados(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            int n_idreg = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            objRegistros.TraerRegistro(n_idreg);
            e_emp = objRegistros.entEmpleado;
            objRegistros = null;

            TxtApeNom2.Text = e_emp.c_ape1 + " " + e_emp.c_ape2 + ", " + e_emp.c_nom1 + " " + e_emp.c_nom2;
            TxtFchIng2.Text = Convert.ToDateTime(e_emp.d_fching).ToString("dd/MM/yyyy");
            TxtFchBaj.Text = DateTime.Now.ToString("dd/MM/yyyy");
            Pan1.Visible = true;
            Pan1.Refresh();
        }
        private void CmdAce_Click(object sender, EventArgs e)
        {
            int n_idreg = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            DialogResult Rpta = MessageBox.Show("¿ Desea dar de baja al empleado ? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                CN_pla_empleados objRegistros = new CN_pla_empleados(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;
                //objRegistros.mysConec = mysConec;
                if (objRegistros.DarBajaEmpleado(n_idreg, TxtFchBaj.Text) == true)
                {
                    MessageBox.Show("¡ El empleado se dio de baja con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    //objRegistros.mysConec = mysConec;

                    if (flagFiltro)
                    {
                        if (flagActivos)
                        {
                            objRegistros.ListarDetalladoActivo(STU_SISTEMA.EMPRESAID);
                        }
                        else
                        {
                            objRegistros.ListarDetalladoInactivo(STU_SISTEMA.EMPRESAID);
                        }
                    }
                    else
                    {
                        objRegistros.Listar(STU_SISTEMA.EMPRESAID);
                    }

                    if (objRegistros.b_OcurrioError == false)
                    {
                        dtLista = objRegistros.dtLista;
                    }

                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                objRegistros = null;
                CmdCan_Click(sender, e);
                return;
            }
        }

        private void CmdAddPer_Click(object sender, EventArgs e)
        {
            //if (n_QueHace == 3) { return; }

            //FgTar.Rows.Count = FgTar.Rows.Count + 1;

            //string c_dato = "";
            //string[,] arrCabeceraDg1 = new string[3, 4];
            //DataTable dtResult = new DataTable();
            //DataTable dtresulpre = new DataTable();
            //DataTable dtInsEnt = new DataTable();

            //arrCabeceraDg1[0, 0] = "Tarea";
            //arrCabeceraDg1[0, 1] = "450";
            //arrCabeceraDg1[0, 2] = "C";
            //arrCabeceraDg1[0, 3] = "c_des";

            //arrCabeceraDg1[1, 0] = "Uni. Med.";
            //arrCabeceraDg1[1, 1] = "40";
            //arrCabeceraDg1[1, 2] = "C";
            //arrCabeceraDg1[1, 3] = "c_unimedabr";

            //arrCabeceraDg1[2, 0] = "Id";
            //arrCabeceraDg1[2, 1] = "0";
            //arrCabeceraDg1[2, 2] = "N";
            //arrCabeceraDg1[2, 3] = "n_id";

            //Genericas xFun = new Genericas();
            //xFun.Buscar_CampoBusqueda = "n_id";
            //xFun.Buscar_CadFiltro = "";
            //xFun.Buscar_CampoOrden = "c_des";
            //dtResult = xFun.Buscar(arrCabeceraDg1, dtTareas);

            //if (dtResult == null) { return; }
            //if (dtResult.Rows.Count == 0) { return; }

            //c_dato = dtResult.Rows[0]["c_des"].ToString();
            //FgTar.SetData(FgTar.Rows.Count - 1, 1, c_dato);

            //c_dato = TxtFchReg.Text;
            //FgTar.SetData(FgTar.Rows.Count - 1, 2, c_dato);

            //c_dato = dtResult.Rows[0]["c_unimedabr"].ToString();
            //FgTar.SetData(FgTar.Rows.Count - 1, 6, c_dato);

            //c_dato = dtResult.Rows[0]["n_id"].ToString();
            //FgTar.SetData(FgTar.Rows.Count - 1, 9, c_dato);
        }

        private void BtnAgregarPeriodo_Click(object sender, EventArgs e)
        {
            AgregarDetalle();
        }

        private void BtnModificarPeriodo_Click(object sender, EventArgs e)
        {
            ModificarDetalle();
        }

        private void BtnQuitarPeriodo_Click(object sender, EventArgs e)
        {
            EliminarDetalle();
        }

        private void AgregarDetalle()
        {
            try
            {
                var frm = new FrmManEmpleadoPeriodo(PeriodoLaborals);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrió un error al agregar, mensaje de error: {0}", ex.Message)
                    , "Agregar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModificarDetalle()
        {
            try
            {
                var viewModel = (PeriodoLaboral)periodoLaboralBindingSource.Current;
                var frm = new FrmManEmpleadoPeriodo(viewModel, PeriodoLaborals);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrió un error al modificar, mensaje de error: {0}", ex.Message)
                    , "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarDetalle()
        {
            try
            {
                if (MessageBox.Show("¿Seguro desea eliminar el registro?", "Eliminar"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var viewModelDetail = (PeriodoLaboral)periodoLaboralBindingSource.Current;
                    PeriodoLaborals.RemoveItem(viewModelDetail);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrió un error al eliminar el registro, mensaje de error: {0}", ex.Message)
                    , "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSubirRegistro_Click(object sender, EventArgs e)
        {
            SubirEmpleado();
        }
    }
}
