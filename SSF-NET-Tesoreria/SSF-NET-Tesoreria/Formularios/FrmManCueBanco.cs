using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Tesoreria;
using SIAC_Negocio.Sistema;
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

namespace SSF_NET_Tesoreria.Formularios
{
    public partial class FrmManCueBanco : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_tes_cuentabanco objRegistros = new CN_tes_cuentabanco();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sun_tipmon o_TipMon = new CN_sun_tipmon();
        CN_tes_bancos o_Bancos = new CN_tes_bancos();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_TES_CUENTABANCO BE_Registro = new BE_TES_CUENTABANCO();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO

        DataTable dtForm = new DataTable();
        DataTable dtMoneda = new DataTable();
        DataTable dtBancos = new DataTable();
        DataTable dtMeses = new DataTable();

        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                 // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[4, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[4, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManCueBanco()
        {
            InitializeComponent();
        }
        private void FrmManCueBanco_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboBanco, dtBancos, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMoneda, dtMoneda, "n_id", "c_des");
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
            dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID);

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(77, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(77);

            o_TipMon.mysConec = mysConec;
            dtMoneda = o_TipMon.Listar();

            o_Bancos.mysConec = mysConec;
            dtBancos = o_Bancos.Listar();
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
                BE_Registro = objRegistros.entBancos;
                CboBanco.SelectedValue = BE_Registro.n_idban;
                CboMoneda.SelectedValue = BE_Registro.n_idmon;
                TxtNumCue.Text = BE_Registro.c_numcue;
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
            CboBanco.Focus();
        }
        void Blanquea()
        {
            CboBanco.SelectedValue = 0;
            CboMoneda.SelectedValue = 0;
            TxtNumCue.Text = "";
        }
        void Bloquea()
        {
            CboBanco.Enabled = !CboBanco.Enabled;
            CboMoneda.Enabled = !CboMoneda.Enabled;
            TxtNumCue.Enabled = !TxtNumCue.Enabled;
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
            CboBanco.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID);
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
            BE_Registro.n_idban = Convert.ToInt32(CboBanco.SelectedValue);
            BE_Registro.c_numcue = TxtNumCue.Text;
            BE_Registro.n_idmon = Convert.ToInt32(CboMoneda.SelectedValue);
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt32(CboBanco.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre del banco !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboBanco.Focus();
                return booEstado;
            }
            if (Convert.ToInt32(CboMoneda.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de moneda !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMoneda.Focus();
                return booEstado;
            }
            if (TxtNumCue.Text == "")
            {
                MessageBox.Show("¡ No ha especificado numero de cuenta bancario !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumCue.Focus();
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
                dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID);
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
        private void FrmManCueBanco_Activated(object sender, EventArgs e)
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
    }
}
