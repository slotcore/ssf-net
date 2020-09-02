using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Tesoreria;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Tesoreria;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Sunat;
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
    public partial class FrmManOrigen : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_tipo;                                                          // INDICA EL TIPO DE REGISTRO QUE SE MOSTRARA (1 = INGRESO ; 2 = SALIDA)
        // OBJETOS LOCALES
        CN_tes_origen objRegistros = new CN_tes_origen();
        CN_tes_bancos e_bancos = new CN_tes_bancos();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_con_pc objPlaCue = new CN_con_pc();
        CN_sun_tipmon objMon = new CN_sun_tipmon();
        CN_tes_cuentabanco o_CtaBan = new CN_tes_cuentabanco();
        CN_sys_modulos objMod = new CN_sys_modulos();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_TES_ORIGEN BE_Registro = new BE_TES_ORIGEN();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO

        DataTable dtForm = new DataTable();
        DataTable dtUniMed = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtPlaCue = new DataTable();
        DataTable dtMon = new DataTable();
        DataTable dtModulo = new DataTable();
        DataTable dtBancos = new DataTable();
        DataTable dtCueBan = new DataTable();

        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                 // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[6, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[4, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManOrigen()
        {
            InitializeComponent();
        }

        private void FrmManOrigen_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboMoneda, dtMon, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboModulo, dtModulo, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboBanco, dtBancos, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboCueBan, dtCueBan, "n_id", "c_numcue");
        }
        void ConfigurarFormulario()
        {
            this.Height = 500;
            this.Width = 719;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            if (n_tipo == 1)
            {
                this.Text = dtForm.Rows[0]["c_titfor"].ToString() + " - INGRESOS";
            }
            else
            {
                this.Text = dtForm.Rows[0]["c_titfor"].ToString() + " - EGRESOS";
            }
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            objRegistros.ListarVista(STU_SISTEMA.EMPRESAID);
            dtLista = objRegistros.dtOrigen;
            if (n_tipo == 1) { dtLista = funDatos.DataTableFiltrar(dtLista, "n_tipo = 1"); }
            if (n_tipo == 2) { dtLista = funDatos.DataTableFiltrar(dtLista, "n_tipo= 2"); }

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(74, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(74);

            objPlaCue.mysConec = mysConec;
            objPlaCue.Listar(STU_SISTEMA.EMPRESAID);
            dtPlaCue = objPlaCue.dtLista;

            objMon.mysConec = mysConec;
            dtMon = objMon.Listar();

            objMod.mysConec = mysConec;
            objMod.Listar();
            dtModulo = objMod.dtLista;

            e_bancos.mysConec = mysConec;
            dtBancos = e_bancos.Listar();

            o_CtaBan.mysConec = mysConec;
            dtCueBan = o_CtaBan.Listar(STU_SISTEMA.EMPRESAID);
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
            if (objRegistros.b_OcurrioError == false)
            {
                BE_Registro = objRegistros.e_Origen;

                LblidCueCom.Text = BE_Registro.n_idcue.ToString();
                TxtCueCom.Text = funDatos.DataTableBuscar(dtPlaCue, "n_id", "c_cuecon", LblidCueCom.Text, "N").ToString();
                TxtCtaCom.Text = funDatos.DataTableBuscar(dtPlaCue, "n_id", "c_des", LblidCueCom.Text, "N").ToString();
                CboModulo.SelectedValue = BE_Registro.n_idmod;
                CboMoneda.SelectedValue = BE_Registro.n_idmon;
                CboBanco.SelectedValue = BE_Registro.n_idbanco;
                //if (n_QueHace == 2) { MostrarCuentaBanco(); }
                MostrarCuentaBanco();
                CboCueBan.SelectedValue = BE_Registro.n_idcueban;
                if (BE_Registro.n_tipo == 1)
                {
                    OptIng.Checked = true;
                }
                else
                {
                    OptEgr.Checked = true;
                }

                if (BE_Registro.n_detalla == 0)
                {
                    OptNoDet.Checked = true;
                }
                else
                {
                    OptSiDet.Checked = true;
                }

                if (BE_Registro.n_oridin == 1)
                {
                    OptEfe.Checked = true;
                }
                else
                {
                    OptBan.Checked = true;
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
            TxtCtaCom.Focus();
            if (n_tipo == 1)
            {
                OptIng.Checked = true;
                OptEgr.Checked = false;
            }
            else
            {
                OptIng.Checked = false;
                OptEgr.Checked = true;
            }
            CmdCtaDeb.Focus();
        }
        void Blanquea()
        {
            TxtCtaCom.Text = "";
            TxtCueCom.Text = "";
            LblidCueCom.Text = "";
            CboModulo.SelectedValue = 0;
            CboMoneda.SelectedValue = 0;
            CboBanco.SelectedValue = 0;
            CboCueBan.SelectedValue = 0;
            OptNoDet.Checked = false;
            OptSiDet.Checked = false;

            OptIng.Checked = false;
            OptEgr.Checked = false;
        }
        void Bloquea()
        {
            TxtCtaCom.Enabled = !TxtCtaCom.Enabled;
            CboModulo.Enabled = !CboModulo.Enabled;
            CboMoneda.Enabled = !CboMoneda.Enabled;
            CboBanco.Enabled = !CboBanco.Enabled;
            CmdCtaDeb.Enabled = !CmdCtaDeb.Enabled;
            CboCueBan.Enabled = !CboCueBan.Enabled; 
            //OptIng.Enabled = !OptIng.Enabled;
            //OptEgr.Enabled = !OptEgr.Enabled;
            OptNoDet.Enabled = !OptNoDet.Enabled;
            OptSiDet.Enabled = !OptSiDet.Enabled;

            OptBan.Enabled = !OptBan.Enabled;
            OptEfe.Enabled = !OptEfe.Enabled;
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
            booAgregando = true;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            CmdCtaDeb.Focus();
            booAgregando = false;
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
                    objRegistros.ListarVista(STU_SISTEMA.EMPRESAID);
                    dtLista = objRegistros.dtOrigen;
                    if (n_tipo == 1) { dtLista = funDatos.DataTableFiltrar(dtLista, "n_tipo = 1"); }
                    if (n_tipo == 2) { dtLista = funDatos.DataTableFiltrar(dtLista, "n_tipo = 2"); }
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

            return booResultado;
        }
        void AsignarEntidad()
        {
            BE_Registro.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_Registro.n_idcue = Convert.ToInt32(LblidCueCom.Text);
            BE_Registro.n_idmod = Convert.ToInt32(CboModulo.SelectedValue);
            BE_Registro.n_idmon = Convert.ToInt32(CboMoneda.SelectedValue);
            BE_Registro.n_idcueban = Convert.ToInt32(CboCueBan.SelectedValue);
            BE_Registro.n_idemp = STU_SISTEMA.EMPRESAID;

            if (n_tipo == 1)
            {
                BE_Registro.n_tipo = 1;
            }
            else
            {
                BE_Registro.n_tipo = 2;
            }
            if (OptEgr.Checked == true) { BE_Registro.n_tipo = 2; }
            if (OptIng.Checked == true) { BE_Registro.n_tipo = 1; }

            if (OptSiDet.Checked == true) { BE_Registro.n_detalla = 1; }
            if (OptNoDet.Checked == true) { BE_Registro.n_detalla = 0; }

            if (OptEfe.Checked == true) { BE_Registro.n_oridin = 1; }
            if (OptBan.Checked == true) { BE_Registro.n_oridin = 2; }

            BE_Registro.n_idbanco = 0;
            if (Convert.ToInt32(CboBanco.SelectedValue) != 0) { BE_Registro.n_idbanco = Convert.ToInt32(CboBanco.SelectedValue); }
        }
        bool CamposOK()
        {
            bool booEstado = false;

            if (TxtCtaCom.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la cuenta contable para este origen !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtCtaCom.Focus();
                return booEstado;
            }
            if (Convert.ToInt32(CboMoneda.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda del origen !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMoneda.Focus();
                return booEstado;
            }
            if ((OptEgr.Checked == false) && (OptIng.Checked == false))
            {
                MessageBox.Show("¡ No ha especificado si el origen es de ingreso o egreso !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                OptEgr.Focus();
                return booEstado;
            }
            if ((OptSiDet.Checked == false) && (OptNoDet.Checked == false))
            {
                MessageBox.Show("¡ No ha especificado si se detallara el origen !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                OptSiDet.Focus();
                return booEstado;
            }
            if (Convert.ToInt32(CboBanco.SelectedValue) != 0)
            {
                if (Convert.ToInt32(CboCueBan.SelectedValue) == 0)
                {
                    MessageBox.Show("¡ No ha especificado el numero de cuenta bancario para esta cuenta contable !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    CboCueBan.Focus();
                    return booEstado;
                } 
            }
            booEstado = true;
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
                objRegistros.ListarVista(STU_SISTEMA.EMPRESAID);
                dtLista = objRegistros.dtOrigen;
                if (n_tipo == 1) { dtLista = funDatos.DataTableFiltrar(dtLista, "n_tipo = 1"); }
                if (n_tipo == 2) { dtLista = funDatos.DataTableFiltrar(dtLista, "n_tipo = 2"); }
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
        private void FrmManOrigen_Activated(object sender, EventArgs e)
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
                    TxtCtaCom.Text = dtResult.Rows[0]["c_des"].ToString();
                }
                else
                {
                    TxtCueCom.Text = "";
                    LblidCueCom.Text = "";
                    TxtCtaCom.Text = "";
                }
            }
        }

        private void CboBanco_SelectedValueChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }
            MostrarCuentaBanco();
        }
        void MostrarCuentaBanco()
        {
            //CboCueBan = null;
            if (Convert.ToInt32(CboBanco.SelectedValue) == 0) { return; }
            if (Convert.ToInt32(CboMoneda.SelectedValue) == 0) { return; }

            DataTable dtResul = new DataTable();
            int n_idmon = Convert.ToInt32(CboMoneda.SelectedValue);
            int n_idban = Convert.ToInt32(CboBanco.SelectedValue);
            dtResul = funDatos.DataTableFiltrar(dtCueBan, "(n_idmon = " + n_idmon.ToString() + " AND n_idban = " + n_idban.ToString() + ")");
            funDatos.ComboBoxCargarDataTable(CboCueBan, dtResul, "n_id", "c_numcue");
            //funDatos.ComboBoxCargarDataTable(CboMoneda, dtMon, "n_id", "c_des");
        }

        private void CboMoneda_SelectedValueChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }
            MostrarCuentaBanco();
        }
    }
}
