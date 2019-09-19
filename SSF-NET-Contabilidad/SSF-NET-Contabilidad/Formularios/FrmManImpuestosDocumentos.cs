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
    public partial class FrmManImpuestosDocumentos : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_con_doccomimp objRegistros = new CN_con_doccomimp();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_con_pc objPlaCue = new CN_con_pc();
        CN_sun_tipdoccom o_TipDoc = new CN_sun_tipdoccom();
        CN_con_impuestos o_imp = new CN_con_impuestos();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        DataTable dtLista = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtPlaCue = new DataTable();
        DataTable dtImp = new DataTable();
        DataTable dtDoc = new DataTable();

        bool booAgregando = false;

        BE_CON_DOCCOMIMP e_ImpDoc = new BE_CON_DOCCOMIMP();
        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[6, 5];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL

        bool booSeEjecuto = false;

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManImpuestosDocumentos()
        {
            InitializeComponent();
        }

        private void FrmManIMpuestosDocumentos_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void CargarCombos()
        {
            DataTableCargar();
            booAgregando = true;
            funDatos.ComboBoxCargarDataTable(CboDoc, dtDoc, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboImp, dtImp, "n_id", "c_des");

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
            dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID);

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(93, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(93);

            objPlaCue.mysConec = mysConec;
            objPlaCue.Listar(STU_SISTEMA.EMPRESAID);
            dtPlaCue = objPlaCue.dtLista;

            o_TipDoc.mysConec = mysConec;
            dtDoc = o_TipDoc.Listar();

            o_imp.mysConec = mysConec;
            o_imp.Listar(STU_SISTEMA.EMPRESAID);
            dtImp = o_imp.dtLista;
        }
        void ListarItems()
        {
            // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
            objRegistros.mysConec = mysConec;
            objRegistros.Select(STU_SISTEMA.EMPRESAID);
            dtLista = objRegistros.dtListar;

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
            Blanquea();
            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdRegistro);
            e_ImpDoc = objRegistros.e_DocImp;

            CboImp.SelectedValue = e_ImpDoc.n_idimp;
            CboDoc.SelectedValue = e_ImpDoc.n_idtipdoc;
            TxtTasa.Text = e_ImpDoc.n_portas.ToString("0.00");
            

            LblIdCtaCom.Text = e_ImpDoc.n_idcuecom.ToString();
            LblIdCtaVen.Text = e_ImpDoc.n_idcueven.ToString();

            dtresul = funDatos.DataTableFiltrar(dtPlaCue, "n_id = " + e_ImpDoc.n_idcuecom + "");
            if (dtresul.Rows.Count != 0)
            {
                TxtCtaCom.Text = dtresul.Rows[0]["c_cuecon"].ToString();
                TxtCtaDesCom.Text = dtresul.Rows[0]["c_des"].ToString();
                LblIdCtaCom.Text = dtresul.Rows[0]["n_id"].ToString();
            }

            dtresul = funDatos.DataTableFiltrar(dtPlaCue, "n_id = " + e_ImpDoc.n_idcueven + "");
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
            CboImp.SelectedValue = 0;
            TxtTasa.Text = "";
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
            CboImp.Enabled = !CboImp.Enabled;
            TxtTasa.Enabled = !TxtTasa.Enabled;
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

            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            CboDoc.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int n_IdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

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
                booResultado = objRegistros.Insertar(e_ImpDoc);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(e_ImpDoc);
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
                e_ImpDoc.n_id = 0;
            }
            else
            {
                e_ImpDoc.n_id = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            }

            e_ImpDoc.n_idtipdoc = Convert.ToInt16(CboDoc.SelectedValue);
            e_ImpDoc.n_idimp = Convert.ToInt16(CboImp.SelectedValue);
            e_ImpDoc.n_portas = Convert.ToDouble(TxtTasa.Text);
            e_ImpDoc.n_idemp = STU_SISTEMA.EMPRESAID;

            e_ImpDoc.n_idcuecom = 0;
            e_ImpDoc.n_idcueven = 0;

            if (Convert.ToInt16(funFunciones.NulosN(LblIdCtaCom.Text)) != 0) { e_ImpDoc.n_idcuecom = Convert.ToInt16(funFunciones.NulosN(LblIdCtaCom.Text)); }
            if (Convert.ToInt16(funFunciones.NulosN(LblIdCtaVen.Text)) != 0) { e_ImpDoc.n_idcueven = Convert.ToInt16(funFunciones.NulosN(LblIdCtaVen.Text)); }
        }
        bool CamposOK()
        {
            bool booEstado = true;
            if (Convert.ToInt16(CboDoc.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboDoc.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboImp.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el impuesto para el documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboImp.Focus();
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

        private void FrmManIMpuestosDocumentos_Activated(object sender, EventArgs e)
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
                int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

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
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            VerRegistro(intIdRegistro);
        }

        private void FrmManIMpuestosDocumentos_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }

        private void TxtTasa_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtCtaCom_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtCtaVen_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtCtaCom_KeyUp(object sender, KeyEventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (e.KeyCode.ToString() == "Delete")
            {
                TxtCtaCom.Text = "";
                TxtCtaDesCom.Text = "";
                LblIdCtaCom.Text = "";
            }
        }

        private void TxtCtaVen_KeyUp(object sender, KeyEventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (e.KeyCode.ToString() == "Delete")
            {
                TxtCtaVen.Text = "";
                TxtCtaDesVen.Text = "";
                LblIdCtaVen.Text = "";
            }
        }
    }
}
