using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Mantenimiento;
using SIAC_Negocio.Mantenimiento;
using SIAC_Negocio.Sistema;
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

namespace SSF_NET_Mantenimiento.Formularios
{
    public partial class FrmManEquipos : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_man_equipos objRegistros = new CN_man_equipos();
        CN_sys_formulario objForm = new CN_sys_formulario();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_MAN_EQUIPOS entRegistro = new BE_MAN_EQUIPOS();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();
        DataTable dtForm = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[2, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        bool booSeEjecuto = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;

        public FrmManEquipos()
        {

            InitializeComponent();
        }
        private void FrmManEquipos_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void CargarCombos()
        {
            DataTableCargar();
            //funDatos.ComboBoxCargarDataTable(CboTipExi, dtTipExi, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 565;
            this.Width = 825;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            if (objRegistros.Listar(STU_SISTEMA.EMPRESAID) == true)
            {
                dtLista = objRegistros.dtLista;
            }

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(39, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(39);
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
            objRegistros.mysConec = mysConec;
            if (objRegistros.TraerRegistro(n_IdRegistro) == true)
            {
                entRegistro = objRegistros.entEquipos;
            }

            TxtDes.Text = entRegistro.c_des;
            TxtIdReg.Text = entRegistro.n_id.ToString();
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
            TxtDes.Focus();
        }
        void Blanquea()
        {
            TxtDes.Text = "";
            TxtIdReg.Text = "";
        }
        void Bloquea()
        {
            TxtDes.Enabled = !TxtDes.Enabled;
            //TxtIdReg.Enabled = !TxtIdReg.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir.Enabled = !ToolImprimir.Enabled;
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

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[1].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            TxtDes.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[1].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.ELiminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    if (objRegistros.Listar(STU_SISTEMA.EMPRESAID) == true)
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
                booResultado = objRegistros.Insertar(entRegistro);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(entRegistro);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.n_ErrorNumber.ToString() + " = " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            entRegistro.n_idemp = STU_SISTEMA.EMPRESAID;
            if (n_QueHace == 1)
            {
                entRegistro.n_id = 0;
            }
            else
            {
                entRegistro.n_id = entRegistro.n_id;
            }

            entRegistro.c_des = TxtDes.Text;
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (TxtDes.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la descripcion de la unidad de medida !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            return booEstado;
        }
        private void FrmManEquipos_Activated(object sender, EventArgs e)
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
                
                if (objRegistros.Listar(STU_SISTEMA.EMPRESAID) == true)
                {
                    dtLista = objRegistros.dtLista;
                }

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
            entRegistro = null;
            dtLista = null;
            objFormVis = null;
            this.Close();
        }

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[1].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            VerRegistro(intIdRegistro);
        }

        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (n_QueHace != 3) { return; }

            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns[1].CellValue(DgLista.Row).ToString());

//                int intIdRegistro = Convert.ToInt32(DgLista.Columns[1].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    VerRegistro(intIdRegistro);
                }
            }
        }

        private void TxtDes_KeyPress(object sender, KeyPressEventArgs e)
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

        private void DgLista_KeyUp(object sender, KeyEventArgs e)
        {
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
    }
}
