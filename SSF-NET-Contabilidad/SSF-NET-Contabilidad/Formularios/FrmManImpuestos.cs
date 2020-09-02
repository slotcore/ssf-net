using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Sistema;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmManImpuestos : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_con_impuestos objRegistros = new CN_con_impuestos();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_con_pc objPlaCue = new CN_con_pc();
        CN_sys_formulario objForm = new CN_sys_formulario();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        DataTable dtLista = new DataTable();
        DataTable dtPlaCue = new DataTable();
        DataTable dtForm = new DataTable();

        bool booAgregando = false;

        BE_CON_IMPUESTOS e_Impuestos = new BE_CON_IMPUESTOS();
        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[6, 5];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL

        bool booSeEjecuto = false;

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManImpuestos()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void FrmManImpuestos_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void CargarCombos()
        {
            DataTableCargar();
            booAgregando = true;
            //funDatos.ComboBoxCargarDataTable(CboModulo, dtModulo, "n_id", "c_des");

            booAgregando = false;
        }
        void ConfigurarFormulario()
        {
            this.Height = 579;
            this.Width = 955;

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
            objFormVis.ObtenerCabeceraLista(58, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(58);

            objPlaCue.mysConec = mysConec;
            objPlaCue.Listar(STU_SISTEMA.EMPRESAID);
            dtPlaCue = objPlaCue.dtLista;
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
            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdRegistro);
            e_Impuestos = objRegistros.e_Impuestos;

            TxtDes.Text = e_Impuestos.c_des;
            TxtAbr.Text = e_Impuestos.c_abr;
            TxtTasa.Text = e_Impuestos.n_tasa.ToString("0.00");

            string c_dato = funDatos.DataTableBuscar(dtPlaCue, "n_id", "c_cuecon", e_Impuestos.n_idcue.ToString(), "N").ToString();
            LblidCueCom.Text = e_Impuestos.n_idcue.ToString();
            if (c_dato == "0")
            {
                TxtCueCom.Text = "";
            }
            else
            {
                TxtCueCom.Text = c_dato;
            }

            c_dato = funDatos.DataTableBuscar(dtPlaCue, "n_id", "c_des", e_Impuestos.n_idcue.ToString(), "N").ToString();
            if (c_dato == "0")
            {
                LblCtaCom.Text = "";
            }
            else
            {
                LblCtaCom.Text = c_dato;
            }

            c_dato = funDatos.DataTableBuscar(dtPlaCue, "n_id", "c_cuecon", e_Impuestos.n_idcuevta.ToString(), "N").ToString();
            LblidCueVen.Text = e_Impuestos.n_idcuevta.ToString();

            if (c_dato == "0")
            {
                TxtCueVen.Text = "";
            }
            else
            {
                TxtCueVen.Text = c_dato;
            }
            c_dato = funDatos.DataTableBuscar(dtPlaCue, "n_id", "c_des", e_Impuestos.n_idcuevta.ToString(), "N").ToString();
            if (c_dato == "0")
            {
                LblCtaVen.Text = "";
            }
            else
            {
                LblCtaVen.Text = c_dato;
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
            TxtDes.Focus();
        }
        void Blanquea()
        {
            TxtDes.Text = "";
            TxtAbr.Text = "";
            TxtTasa.Text = "";

            LblidCueCom.Text = "";
            TxtCueCom.Text = "";
            LblCtaCom.Text = "";

            LblidCueVen.Text = "";
            TxtCueVen.Text = "";
            LblCtaVen.Text = "";
        }
        void Bloquea()
        {
            TxtDes.Enabled = !TxtDes.Enabled;
            TxtAbr.Enabled = !TxtAbr.Enabled;
            TxtTasa.Enabled = !TxtTasa.Enabled;

            CmdCtaDeb.Enabled = !CmdCtaDeb.Enabled;
            CmdCtaHab.Enabled = !CmdCtaHab.Enabled;
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
            TxtDes.Focus();
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
                booResultado = objRegistros.Insertar(e_Impuestos);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(e_Impuestos);
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
                e_Impuestos.n_id = 0;
            }
            else
            {
                e_Impuestos.n_id = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            }

            e_Impuestos.c_des = TxtDes.Text;
            e_Impuestos.c_abr = TxtAbr.Text;
            e_Impuestos.n_tasa = Convert.ToInt32(TxtTasa.Text);
            e_Impuestos.n_idcue = Convert.ToInt32(LblidCueCom.Text);
            e_Impuestos.n_idcuevta = Convert.ToInt32(LblidCueVen.Text);
        }
        bool CamposOK()
        {
            bool booEstado = true;
            if (TxtDes.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la descripcion del impuesto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtDes.Focus();
                return booEstado;
            }
            if (TxtAbr.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la abreviatura del impuesto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtAbr.Focus();
                return booEstado;
            }
            if (TxtTasa.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la tasa porcentual del impuesto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtTasa.Focus();
                return booEstado;
            }
            if (LblidCueCom.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la cuenta de compra para el impuesto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                LblidCueCom.Focus();
                return booEstado;
            }
            if (LblidCueVen.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la cuenta de venta para el impuesto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                LblidCueVen.Focus();
                return booEstado;
            }
            return booEstado;
        }

        private void FrmManImpuestos_Activated(object sender, EventArgs e)
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

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            VerRegistro(intIdRegistro);
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

        private void FrmManImpuestos_Resize(object sender, EventArgs e)
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

        private void TxtAbr_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CmdCtaDeb_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            dtResult = objPlaCue.BuscarCuenta(dtPlaCue);
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtCueCom.Text = dtResult.Rows[0]["c_cuecon"].ToString();
                    LblidCueCom.Text = dtResult.Rows[0]["n_id"].ToString();
                    LblCtaCom.Text = dtResult.Rows[0]["c_des"].ToString();
                }
                else
                {
                    TxtCueCom.Text = "";
                    LblidCueCom.Text = "";
                    LblCtaCom.Text = "";
                }
            }
        }

        private void CmdCtaHab_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            dtResult = objPlaCue.BuscarCuenta(dtPlaCue);
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtCueVen.Text = dtResult.Rows[0]["c_cuecon"].ToString();
                    LblidCueVen.Text = dtResult.Rows[0]["n_id"].ToString();
                    LblCtaVen.Text = dtResult.Rows[0]["c_des"].ToString();
                }
                else
                {
                    TxtCueVen.Text = "";
                    LblidCueVen.Text = "";
                    LblCtaVen.Text = "";
                }
            }
        }
    }
}
