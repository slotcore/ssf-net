using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Estacionamiento;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Estacionamiento;
using SIAC_Negocio.Planilla;
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

namespace SIAC_NET_Estacionamientos.Formularios
{
    public partial class FrmManCajeros : Form
    {
        // VARIABLES PUBLICAS
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_empresalocal o_emploc = new CN_sys_empresalocal();
        CN_sys_usuarios o_usu = new CN_sys_usuarios();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_EST_CAJEROS BE_Registro = new BE_EST_CAJEROS();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO
        DataTable dtemp = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtLocal = new DataTable();
        DataTable dtusuario = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[3, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[4, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManCajeros()
        {
            InitializeComponent();
        }

        private void FrmManCajeros_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboLocal, dtLocal, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 500;
            this.Width = 719;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
        }
        void DataTableCargar()
        {
            CN_est_cajeros objRegistros = new CN_est_cajeros(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.Listar(STU_SISTEMA.EMPRESAID);
            dtLista = objRegistros.dtListar;
            objRegistros = null;

            CN_pla_empleados o_emp = new CN_pla_empleados(STU_SISTEMA);
            o_emp.STU_SISTEMA = STU_SISTEMA;
            o_emp.Listar(STU_SISTEMA.EMPRESAID);
            dtemp = o_emp.dtLista;
            o_emp = null;

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            objFormVis.mysConec = o_conec.mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(85, ref arrCabeceraDg1);

            objForm.mysConec = o_conec.mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(85);

            o_emploc.mysConec = o_conec.mysConec;
            dtLocal = o_emploc.Listar(STU_SISTEMA.EMPRESAID, 0);
            
            o_usu.mysConec = o_conec.mysConec;
            o_usu.Consulta1(0);
            dtusuario = o_usu.dtLista;
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
            booAgregando = true;

            CN_est_cajeros objRegistros = new CN_est_cajeros(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            
            objRegistros.TraerRegistro(n_IdRegistro);
            if (objRegistros.b_OcurrioError == false)
            {
                BE_Registro = objRegistros.e_Cajero;

                CboLocal.SelectedValue = BE_Registro.n_idloc;
                LblidTrab.Text = BE_Registro.n_idtra.ToString();
                TxtTrabajador.Text = funDatos.DataTableBuscar(dtemp, "n_id", "c_apenom", LblidTrab.Text, "N").ToString();
                LblIdIsuario.Text = BE_Registro.n_idusu.ToString();
                TxtUsuario.Text = funDatos.DataTableBuscar(dtusuario, "n_id", "c_usuario", LblIdIsuario.Text, "N").ToString();
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
            CboLocal.Focus();
        }
        void Blanquea()
        {
            CboLocal.SelectedValue = 0;
            TxtTrabajador.Text = "";
            LblidTrab.Text = "";
            TxtUsuario.Text = "";
            LblIdIsuario.Text = "";
        }
        void Bloquea()
        {
            CboLocal.Enabled = !CboLocal.Enabled;
            CmdBusUsu.Enabled = !CmdBusUsu.Enabled;
            CmdBusTra.Enabled = !CmdBusTra.Enabled;  
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

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            CboLocal.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                CN_est_cajeros objRegistros = new CN_est_cajeros(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.Listar(STU_SISTEMA.EMPRESAID);
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

            CN_est_cajeros objRegistros = new CN_est_cajeros(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(BE_Registro);
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

            if (n_QueHace == 1)
            {
                n_id = 0;
            }
            else
            {
                n_id = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            }
            BE_Registro.n_id = n_id;
            BE_Registro.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_Registro.n_idloc = Convert.ToInt32(CboLocal.SelectedValue);
            BE_Registro.n_idtra = Convert.ToInt32(LblidTrab.Text);
            BE_Registro.n_idusu = Convert.ToInt32(LblIdIsuario.Text);
            
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt32(CboLocal.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre del Local !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboLocal.Focus();
                return booEstado;
            }
            if (LblidTrab.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre del trabajador !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtTrabajador.Focus();
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
                CN_est_cajeros objRegistros = new CN_est_cajeros(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;
                objRegistros.Listar(STU_SISTEMA.EMPRESAID);
                dtLista = objRegistros.dtListar;
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
                objRegistros = null;
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
        private void FrmManCajeros_Activated(object sender, EventArgs e)
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
        private void CmdBusTra_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            string[,] arrCabeceraFlexFil = new string[3, 4];

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Apellidos y Nombres";
            arrCabeceraFlexFil[0, 1] = "300";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "c_apenom";

            arrCabeceraFlexFil[1, 0] = "Nº DNI";
            arrCabeceraFlexFil[1, 1] = "80";
            arrCabeceraFlexFil[1, 2] = "C";
            arrCabeceraFlexFil[1, 3] = "c_numdocide";

            arrCabeceraFlexFil[2, 0] = "ID";
            arrCabeceraFlexFil[2, 1] = "0";
            arrCabeceraFlexFil[2, 2] = "N";
            arrCabeceraFlexFil[2, 3] = "n_id";

            funDatos.Buscar_CampoBusqueda = "n_id";
            funDatos.Buscar_CadFiltro = "";
            dtResult = funDatos.Buscar(arrCabeceraFlexFil, dtemp);
            if (dtResult != null)
            { 
                if (dtResult.Rows.Count != 0)
                {
                    LblidTrab.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtTrabajador.Text = dtResult.Rows[0]["c_apenom"].ToString();
                }
            }
        }
        private void CmdBusUsu_Click(object sender, EventArgs e)
        {
            DataTable dtRes = new DataTable();
            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            CN_sys_usuarios o_usu = new CN_sys_usuarios();
            o_usu.mysConec = o_conec.mysConec;
            o_usu.BuscarUsuario(0);
            dtRes = o_usu.dtLista;
            o_conec = null;
            o_usu = null;
            if (dtRes != null)
            {
                if (dtRes.Rows.Count != 0)
                {
                    LblIdIsuario.Text = dtRes.Rows[0]["n_id"].ToString();
                    TxtUsuario.Text = dtRes.Rows[0]["c_usuario"].ToString();
                }
            }
        }
    }
}
