using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Planilla;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
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

namespace SSF_NET_Ventas.Formularios
{
    public partial class FrmManPuntoVenta : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_TipoReg = 0;                                               // INDICA EL TIPO DE REGISTRO QUE SE MOSTRARA 1 = VENTAS ;  2 = COMPRAS
        // OBJETOS LOCALES
        CN_vta_punvencli objRegistros = new CN_vta_punvencli();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_clipro objCliPro = new CN_mae_clipro();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sun_distritos objDis = new CN_sun_distritos();
        CN_sun_provincia objPro = new CN_sun_provincia();
        CN_sun_departamentos objDep = new CN_sun_departamentos();
        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_VTA_PUNVENCLI BE_ListaReg = new BE_VTA_PUNVENCLI();
        BE_VTA_PUNVENCLI BE_Registro = new BE_VTA_PUNVENCLI();

        // DATATABLE LOCALES
        DataTable dtRegistros = new DataTable();
        DataTable dtCliPro = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtDep = new DataTable();
        DataTable dtPro = new DataTable();
        DataTable dtDis = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[5, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^-" + (char)8;
        public FrmManPuntoVenta()
        {
            InitializeComponent();
        }

        private void FrmManPuntoVenta_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            DataTable dtResult = new DataTable();

            funDatos.ComboBoxCargarDataTable(CboCliente, dtCliPro, "n_id", "c_nombre");

            funDatos.ComboBoxCargarDataTable(CboDep, dtDep, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboPro, dtPro, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboDis, dtDis, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 587;
            this.Width = 930;
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            if (n_TipoReg == 1)
            {
                this.Text = dtForm.Rows[0]["c_titfor"].ToString() + " - CLIENTES";
            }
            else
            {
                this.Text = dtForm.Rows[0]["c_titfor"].ToString()+" - PROVEEDORES";
            }
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            if (n_TipoReg == 1) { dtRegistros = objRegistros.Listar2(1); }
            if (n_TipoReg == 2) { dtRegistros = objRegistros.Listar2(2); }

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(15, ref arrCabeceraDg1);

            objCliPro.mysConec = mysConec;
            if (n_TipoReg == 1)
            {
                dtCliPro = objCliPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
            }
            else
            {
                dtCliPro = objCliPro.ListarProveedor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
            }

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(15);

            objDep.mysConec = mysConec;
            dtDep = objDep.Listar();                                        // CARGAMOS TODOS LOS DEPARTAMENTOS

            objPro.mysConec = mysConec;
            dtPro = objPro.Listar();                                        // CARGAMOS TODAS LAS PROVINCIAS

            objDis.mysConec = mysConec;
            dtDis = objDis.Listar();                                        // CARGAMOS TODOS LOS DISTRITOS
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtRegistros.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtRegistros, true);
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
            BE_Registro = objRegistros.TraerRegistro(n_IdRegistro);

            CboCliente.SelectedValue = BE_Registro.n_idcli;
            TxtCodCEN.Text = BE_Registro.c_codcen.ToString();
            TxtDescripcion.Text = BE_Registro.c_des.ToString();
            TxtDir.Text = BE_Registro.c_dir.ToString();
            CboDep.SelectedValue = Convert.ToInt16(BE_Registro.n_iddep);
            CboPro.SelectedValue = Convert.ToInt16(BE_Registro.n_idpro);
            CboDis.SelectedValue = Convert.ToInt16(BE_Registro.n_iddis);
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
            CboCliente.Focus();
        }
        void Blanquea()
        {
            CboCliente.SelectedValue = 0;
            TxtDescripcion.Text = "";
            TxtCodCEN.Text = "";
            TxtDir.Text = "";
            CboDep.SelectedValue = 0;
            CboPro.SelectedValue = 0;
            CboDis.SelectedValue = 0;
        }
        void Bloquea()
        {
            CboCliente.Enabled = !CboCliente.Enabled;
            TxtCodCEN.Enabled = !TxtCodCEN.Enabled;
            TxtDescripcion.Enabled = !TxtDescripcion.Enabled;
            TxtDir.Enabled = !TxtDir.Enabled;
            CboDep.Enabled = !CboDep.Enabled;
            CboDis.Enabled = !CboDis.Enabled;
            CboPro.Enabled = !CboPro.Enabled;
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
            CboCliente.Focus();
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
                    dtRegistros = objRegistros.Listar();
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

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            BE_ListaReg.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_ListaReg.n_idcli = Convert.ToInt16(CboCliente.SelectedValue);
            BE_ListaReg.n_id = BE_Registro.n_id;
            BE_ListaReg.c_codcen = TxtCodCEN.Text;
            BE_ListaReg.c_des = TxtDescripcion.Text;
            BE_ListaReg.c_dir = TxtDir.Text;
            BE_ListaReg.n_iddep = Convert.ToInt16(CboDep.SelectedValue);
            BE_ListaReg.n_iddis = Convert.ToInt16(CboDis.SelectedValue);
            BE_ListaReg.n_idpro = Convert.ToInt16(CboPro.SelectedValue);
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt16(CboCliente.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (TxtCodCEN.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el codigo CEN del punto de venta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (TxtDescripcion.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre del punto de venta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (TxtDir.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la direccion del punto de venta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (Convert.ToInt16(CboDep.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el departamento de ubicacion del punto de venta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            } 
            if (Convert.ToInt16(CboPro.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la provincia de ubicacion del punto de venta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (Convert.ToInt16(CboDis.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el distrito de ubicacion del punto de venta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            return booEstado;
        }

        private void FrmManPuntoVenta_Activated(object sender, EventArgs e)
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
                dtRegistros = objRegistros.Listar();
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
            dtRegistros = null;

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

        private void DgLista_DoubleClick(object sender, EventArgs e)
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

        private void FrmManPuntoVenta_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 110, this.Width - 38);
        }

        private void CboDep_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();

            if (CboDep.SelectedValue != null)
            {
                booAgregando = true;
                string strCadenaFiltro = "n_iddep = " + CboDep.SelectedValue.ToString() + "";
                CboPro.Enabled = true;

                CboPro.SelectedValue = 0;
                CboDis.SelectedValue = 0;

                DtFiltro = funDatos.DataTableFiltrar(dtPro, strCadenaFiltro);
                funDatos.ComboBoxCargarDataTable(CboPro, DtFiltro, "n_id", "c_des");
                if (n_QueHace == 3)
                {
                    CboPro.Enabled = false;
                    CboDis.Enabled = false;
                }
                else
                {
                    CboPro.Enabled = true;
                    CboDis.Enabled = false;
                }

                booAgregando = false;
            }
        }

        private void CboPro_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();

            if (CboPro.SelectedValue != null)
            {
                string strCadenaFiltro = "n_iddep = " + CboDep.SelectedValue.ToString() + " AND n_idpro = " + CboPro.SelectedValue.ToString() + "";
                CboDis.Enabled = true;

                CboDis.SelectedValue = 0;

                DtFiltro = funDatos.DataTableFiltrar(dtDis, strCadenaFiltro);
                funDatos.ComboBoxCargarDataTable(CboDis, DtFiltro, "n_id", "c_des");

                if (n_QueHace == 3)
                {
                    CboDis.Enabled = false;
                }
                else
                {
                    CboDis.Enabled = true;
                }
            }
        }

        private void TxtCodCEN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtDir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}
