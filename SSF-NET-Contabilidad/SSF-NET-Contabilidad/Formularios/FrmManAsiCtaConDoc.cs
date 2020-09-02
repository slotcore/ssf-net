using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using SIAC_Entidades.Sunat;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Sistema;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmManAsiCtaConDoc : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_con_tipdoccomcue objRegistros = new CN_con_tipdoccomcue();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_con_pc objPlaCue = new CN_con_pc();
        CN_sun_tipdoccom o_TipDoc = new CN_sun_tipdoccom();
        CN_sun_tipmon  o_TipMon = new CN_sun_tipmon();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        
        DataTable dtLista = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtPlaCue = new DataTable();
        DataTable dtMon = new DataTable();
        DataTable dtDoc = new DataTable();

        bool booAgregando = false;

        BE_CON_TIPDOCCOMCUE e_DocComercial = new BE_CON_TIPDOCCOMCUE();
        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[7, 5];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL

        bool booSeEjecuto = false;

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManAsiCtaConDoc()
        {
            InitializeComponent();
        }

        private void FrmManAsiCtaConDoc_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void CargarCombos()
        {
            DataTableCargar();
            booAgregando = true;
            funDatos.ComboBoxCargarDataTable(CboDoc, dtDoc, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMon, dtMon, "n_id", "c_des");

            booAgregando = false;
        }
        void ConfigurarFormulario()
        {
            this.Height = 534;
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
            objRegistros.Listar(STU_SISTEMA.EMPRESAID);
            dtLista = objRegistros.dtLista;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(83, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(83);

            objPlaCue.mysConec = mysConec;
            objPlaCue.Listar(STU_SISTEMA.EMPRESAID);
            dtPlaCue = objPlaCue.dtLista;

            o_TipDoc.mysConec = mysConec;
            dtDoc = o_TipDoc.Listar();
                        
            o_TipMon.mysConec = mysConec;
            dtMon = o_TipMon.Listar();
        }
        void ListarItems()
        {
            // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
            objRegistros.mysConec = mysConec;
            objRegistros.Listar(STU_SISTEMA.EMPRESAID);
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
            DataTable dtresul = new DataTable();

            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdRegistro);
            e_DocComercial = objRegistros.e_Documento;

            CboDoc.SelectedValue = e_DocComercial.n_idtipdoc;
            CboMon.SelectedValue = e_DocComercial.n_idmon;

            LblIdCtaCom.Text = e_DocComercial.n_idcuecom.ToString();
            LblIdCtaVen.Text = e_DocComercial.n_idcueven.ToString();

            dtresul = funDatos.DataTableFiltrar(dtPlaCue, "n_id = " + e_DocComercial.n_idcuecom + "");
            if (dtresul.Rows.Count != 0)
            {
                TxtCtaCom.Text = dtresul.Rows[0]["c_cuecon"].ToString();
                TxtCtaDesCom.Text = dtresul.Rows[0]["c_des"].ToString();
                LblIdCtaCom.Text = dtresul.Rows[0]["n_id"].ToString();
            }

            dtresul = funDatos.DataTableFiltrar(dtPlaCue, "n_id = " + e_DocComercial.n_idcueven + "");
            if (dtresul.Rows.Count != 0)
            {
                TxtCtaVen.Text = dtresul.Rows[0]["c_cuecon"].ToString();
                TxtCtaDesVen.Text = dtresul.Rows[0]["c_des"].ToString();
                LblIdCtaVen.Text = dtresul.Rows[0]["n_id"].ToString();
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
            CboDoc.Focus();
        }
        void Blanquea()
        {
            CboDoc.SelectedValue = 0;
            CboMon.SelectedValue = 0;
            TxtCtaCom.Text = "";
            TxtCtaVen.Text = "";
            TxtCtaDesCom.Text = "";
            TxtCtaDesVen.Text = "";
            LblIdCtaCom.Text = "";
            LblIdCtaVen.Text = "";
        }
        void Bloquea()
        {
            CboDoc.Enabled = !CboDoc.Enabled;
            CboMon.Enabled = !CboMon.Enabled;
            TxtCtaCom.Enabled = !TxtCtaCom.Enabled;
            TxtCtaVen.Enabled = !TxtCtaVen.Enabled;

            CmdBusCtaCom.Enabled = !CmdBusCtaCom.Enabled;
            CmdBusCtaVen.Enabled = !CmdBusCtaVen.Enabled;
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

            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            CboDoc.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(n_IdRegistro) == true)
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

            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(e_DocComercial);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(e_DocComercial);
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
                e_DocComercial.n_id = 0;
            }
            else
            {
                e_DocComercial.n_id = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            }

            e_DocComercial.n_idtipdoc = Convert.ToInt32(CboDoc.SelectedValue);
            e_DocComercial.n_idmon = Convert.ToInt32(CboMon.SelectedValue);

            e_DocComercial.n_idcuecom = 0;
            e_DocComercial.n_idcueven = 0;

            if (LblIdCtaCom.Text != "") { e_DocComercial.n_idcuecom = Convert.ToInt32(LblIdCtaCom.Text); }
            if (LblIdCtaVen.Text != "") { e_DocComercial.n_idcueven = Convert.ToInt32(LblIdCtaVen.Text); }
            e_DocComercial.n_idemp = STU_SISTEMA.EMPRESAID;
        }
        bool CamposOK()
        {
            bool booEstado = true;
            if (Convert.ToInt32(CboDoc.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboDoc.Focus();
                return booEstado;
            }
            if (Convert.ToInt32(CboMon.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMon.Focus();
                return booEstado;
            }

            if ((TxtCtaCom.Text == "") && (TxtCtaVen.Text == ""))
            {
                MessageBox.Show("¡ No ha especificado la cuenta contable para compra o venta, debe de indicar al menos una cuenta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtCtaCom.Focus();
                return booEstado;
            }

            return booEstado;
        }

        private void FrmManAsiCtaConDoc_Activated(object sender, EventArgs e)
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
                int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    VerRegistro(intIdRegistro);
                }
            }
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
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            VerRegistro(intIdRegistro);
        }
        private void FrmManAsiCtaConDoc_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CmdBusCtaCom_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            dtResult = objPlaCue.BuscarCuenta(dtPlaCue);
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtCtaCom.Text = dtResult.Rows[0]["c_cuecon"].ToString();
                    TxtCtaDesCom.Text = dtResult.Rows[0]["c_des"].ToString();
                    LblIdCtaCom.Text = dtResult.Rows[0]["n_id"].ToString();
                }
                else
                {
                    TxtCtaCom.Text = "";
                    TxtCtaDesCom.Text = "";
                    LblIdCtaCom.Text = "";
                }
            }
        }
        private void CmdBusCtaVen_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            dtResult = objPlaCue.BuscarCuenta(dtPlaCue);
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtCtaVen.Text = dtResult.Rows[0]["c_cuecon"].ToString();
                    TxtCtaDesVen.Text = dtResult.Rows[0]["c_des"].ToString();
                    LblIdCtaVen.Text = dtResult.Rows[0]["n_id"].ToString();
                }
                else
                {
                    TxtCtaCom.Text = "";
                    TxtCtaDesVen.Text = "";
                    LblIdCtaVen.Text = "";
                }
            }
        }
    }
}
