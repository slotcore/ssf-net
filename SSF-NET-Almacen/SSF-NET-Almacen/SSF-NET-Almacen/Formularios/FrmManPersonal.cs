using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Ventas;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Planilla;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Ventas;
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

namespace SSF_NET_Almacen.Formularios
{
    public partial class FrmManPersonal : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_alm_personal objRegistros = new CN_alm_personal();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_pla_cargos objCargos = new CN_pla_cargos();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_ALM_PERSONAL BE_ListaReg = new BE_ALM_PERSONAL();
        BE_ALM_PERSONAL BE_Registro = new BE_ALM_PERSONAL();

        // DATATABLE LOCALES
        DataTable dtForm = new DataTable();
        DataTable dtRegistros = new DataTable();
        DataTable dtLocal = new DataTable();
        DataTable dtCargos = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[3, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        const int id_Formulario = 98;

        bool booSeEjecuto = false;
    
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        
        public FrmManPersonal()
        {
            InitializeComponent();
        }

        private void FrrmManAlmacenes_Load(object sender, System.EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }

        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboCargo, dtCargos, "n_id", "c_des");
        }

        void ConfigurarFormulario()
        {
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
        }

        void DataTableCargar()
        {
            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(id_Formulario, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(id_Formulario);

            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID);

            objCargos.mysConec = mysConec;
            dtCargos = objCargos.Listar(STU_SISTEMA.EMPRESAID);                               //  CARGAMOS TODOS LOS TIPOS DE ITEM
        }

        void ListarItems()
        {
            LblNumReg.Text = (dtRegistros.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtRegistros, true);
        }

        void VerRegistro(int n_IdRegistro)
        {
            objRegistros.mysConec = mysConec;
            BE_Registro = objRegistros.TraerRegistro(n_IdRegistro);

            CboCargo.SelectedValue = BE_Registro.n_idcargo;
            TxtTra.Text = BE_Registro.destra;
            LblIdTra.Text = BE_Registro.n_idtra.ToString();
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
            CboCargo.Focus();
        }

        void Blanquea()
        {
            CboCargo.SelectedValue = 0;
            TxtTra.Text = string.Empty;
            LblIdTra.Text = string.Empty;
        }

        void Bloquea()
        {
            CboCargo.Enabled = !CboCargo.Enabled;
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

            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            CboCargo.Focus();
        }

        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID);
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
                booResultado = objRegistros.Insertar(BE_ListaReg);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(BE_ListaReg);
            }

            if (booResultado==false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }

        void AsignarEntidad()
        {
            BE_ListaReg.n_id = BE_Registro.n_id;
            BE_ListaReg.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_ListaReg.n_idcargo = Convert.ToInt16(CboCargo.SelectedValue);
            BE_ListaReg.n_idtra = Convert.ToInt32(LblIdTra.Text);
        }

        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt16(CboCargo.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el usuario !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (string.IsNullOrEmpty(LblIdTra.Text))
            {
                MessageBox.Show("¡ No ha especificado el Empleado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            return booEstado;
        }

        private void FrmManAprobador_Activated(object sender, System.EventArgs e)
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

        private void ToolNuevo_Click(object sender, System.EventArgs e)
        {
            Nuevo();
        }

        private void ToolModificar_Click(object sender, System.EventArgs e)
        {
            Modificar();
        }

        private void ToolEliminar_Click(object sender, System.EventArgs e)
        {
            EliminarRegistro();
        }

        private void ToolGrabar_Click(object sender, System.EventArgs e)
        {
            if (Grabar() == true)
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objRegistros.mysConec = mysConec;
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID);
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

        private void ToolCancelar_Click(object sender, System.EventArgs e)
        {
            Cancelar();
        }

        private void ToolSalir_Click(object sender, System.EventArgs e)
        {
            objRegistros = null;
            dtRegistros = null;

            objFormVis = null;

            this.Close();
        }

        private void Tab1_SelectedIndexChanging(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;

            if (n_QueHace != 3) { return; }

            if (tc.SelectedIndex == 1)
            {
                int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    VerRegistro(intIdRegistro);
                }
            }
        }

        private void DgLista_DoubleClick(object sender, System.EventArgs e)
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

        private void CmdBusTra_Click(object sender, EventArgs e)
        {
            BuscarTrabajador();
        }

        void BuscarTrabajador()
        {
            try
            {
                string[,] arrCabeceraDg1 = new string[2, 4];
                DataTable dtResult = new DataTable();

                CN_pla_empleados o_emp = new CN_pla_empleados(STU_SISTEMA);
                o_emp.Listar(STU_SISTEMA.EMPRESAID);
                dtResult = o_emp.dtLista;

                dtResult = funDatos.DataTableFiltrar(dtResult, "n_activo = 1");

                arrCabeceraDg1[0, 0] = "Empleado";
                arrCabeceraDg1[0, 1] = "500";
                arrCabeceraDg1[0, 2] = "C";
                arrCabeceraDg1[0, 3] = "c_apenom";

                arrCabeceraDg1[1, 0] = "n_id";
                arrCabeceraDg1[1, 1] = "0";
                arrCabeceraDg1[1, 2] = "C";
                arrCabeceraDg1[1, 3] = "n_id";

                Genericas xFun = new Genericas();
                xFun.Buscar_CampoBusqueda = "n_id";
                xFun.Buscar_CadFiltro = "";
                xFun.Buscar_CampoOrden = "c_apenom";
                dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

                if (dtResult == null) { return; }
                if (dtResult.Rows.Count == 0) { return; }

                TxtTra.Text = dtResult.Rows[0]["c_apenom"].ToString();
                LblIdTra.Text = dtResult.Rows[0]["n_id"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al buscar Trabajador", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
