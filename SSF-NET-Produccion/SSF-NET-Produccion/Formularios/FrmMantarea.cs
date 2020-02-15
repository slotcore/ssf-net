using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Produccion;
using SIAC_Entidades.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Produccion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper.Classes;
using Helper.Service;
using Refit;
using System.Configuration;

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmMantarea : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_pro_tareas objRegistros = new CN_pro_tareas();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sun_unimed objUniMed = new CN_sun_unimed();
        CN_mae_meses objMeses = new CN_mae_meses();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_PRO_TAREA BE_Registro = new BE_PRO_TAREA();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO

        DataTable dtForm = new DataTable();
        DataTable dtUniMed = new DataTable();
        DataTable dtMeses = new DataTable();

        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                 // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[7, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[4, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmMantarea()
        {
            InitializeComponent();
        }
        private void FrmMantarea_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            //funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboUniMed, dtUniMed, "n_id", "c_des");
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
            //CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(29, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(29);

            objUniMed.mysConec = mysConec;
            dtUniMed = objUniMed.Listar();
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
            
            objRegistros.TraerRegistro(n_IdRegistro);
            if (objRegistros.booOcurrioError == false)
            {
                BE_Registro = objRegistros.entTareas;
                TxtCod.Text = BE_Registro.c_cod;
                TxtDes.Text = BE_Registro.c_des;
                TxtAbr.Text = BE_Registro.c_abr;
                CboUniMed.SelectedValue = BE_Registro.n_idunimed;
                TxtPre.Text = BE_Registro.n_pre.ToString("0.00");
                TxtObs.Text = BE_Registro.c_obs;

                if (BE_Registro.n_div == 1)
                {
                    OptSi.Checked = true;
                    OptNo.Checked = false;
                }
                else
                {
                    OptSi.Checked = false;
                    OptNo.Checked = true;
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
            TxtCod.Focus();
        }
        void Blanquea()
        {
            TxtCod.Text = "";
            TxtDes.Text = "";
            TxtAbr.Text = "";
            CboUniMed.SelectedValue = 0;
            TxtPre.Text = "";
            TxtObs.Text = "";
        }
        void Bloquea()
        {
            TxtCod.Enabled = !TxtCod.Enabled;
            TxtDes.Enabled = !TxtDes.Enabled;
            TxtAbr.Enabled = !TxtAbr.Enabled;
            CboUniMed.Enabled = !CboUniMed.Enabled;
            TxtPre.Enabled = !TxtPre.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;

            OptSi.Enabled = !OptSi.Enabled;
            OptNo.Enabled = !OptNo.Enabled;
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

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            TxtCod.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
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
                booResultado = objRegistros.Insertar(BE_Registro);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(BE_Registro);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            BE_Registro.n_idemp = 0;                                                            
            if (STU_SISTEMA.SYS_UNIBD == 0) { BE_Registro.n_idemp = STU_SISTEMA.EMPRESAID; }

            BE_Registro.c_cod = TxtCod.Text;
            BE_Registro.c_des = TxtDes.Text;
            BE_Registro.c_abr = TxtAbr.Text;
            BE_Registro.n_idunimed = Convert.ToInt16(CboUniMed.SelectedValue);
            BE_Registro.n_pre = Convert.ToDouble(TxtPre.Text);
            BE_Registro.c_obs = TxtObs.Text;

            if (OptSi.Checked == true)
            {
                BE_Registro.n_div = 1;
            }
            if (OptNo.Checked == true)
            {
                BE_Registro.n_div = 0;
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (TxtCod.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el codigo de la tarea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtCod.Focus();
                return booEstado;
            }
            if (TxtDes.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la descripcion de la tarea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtDes.Focus();
                return booEstado;
            }
            if (TxtAbr.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la abreviatura de la tarea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtAbr.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboUniMed.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la unidad e medida de la tarea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboUniMed.Focus();
                return booEstado;
            }
            if (TxtPre.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el precio de la tarea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtPre.Focus();
                return booEstado;
            }

            if ((OptSi.Checked == false) && (OptNo.Checked == false))
            {
                MessageBox.Show("¡ No ha especificado si la nueva tarea e diversa o no !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                OptSi.Focus();
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
                dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
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
            objUniMed = null;
            objFormVis = null;
            this.Close();
        }

        private void FrmMantarea_Activated(object sender, EventArgs e)
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Tab1_Click(object sender, EventArgs e)
        {

        }

        private void btnSubirRegistro_Click(object sender, EventArgs e)
        {
            SubirTarea();
        }

        public async void SubirTarea()
        {
            try
            {
                int n_IdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
                objRegistros.mysConec = mysConec;
                objRegistros.TraerRegistro(n_IdRegistro);

                if (objRegistros.booOcurrioError)
                {
                    throw new Exception("Error!!");
                }

                BE_Registro = objRegistros.entTareas;
                TaskWork taskWork = new TaskWork
                {
                    Code = BE_Registro.n_id.ToString(),
                    Name = BE_Registro.c_des,
                    Description = BE_Registro.c_abr
                };

                var humanResourceService = RestService.For<IHumanResourceService>(ConfigurationManager.AppSettings["ApiHostUrl"]);
                var taskWorkResult = await humanResourceService.TaskWorkCreate(taskWork);

                MessageBox.Show(string.Format("¡Se subió el empleado: {0} correctamente! ", taskWorkResult.Name), "Subir Empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("¡Ocurrio un error: {0}! ", ex.Message), "Subir Empleado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
