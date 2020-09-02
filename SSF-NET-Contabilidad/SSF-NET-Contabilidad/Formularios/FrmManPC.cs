using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
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

namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmManPC : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        DatosMySql FunMysql = new DatosMySql();
        CN_con_pc objRegistros = new CN_con_pc();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_con_pcdestinos objDestinos = new CN_con_pcdestinos();
        CN_sys_modulos objModulos = new CN_sys_modulos();
        CN_sys_formulario objForm = new CN_sys_formulario();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        DataTable dtLista = new DataTable();
        DataTable dtModulo = new DataTable();
        DataTable dtDestino = new DataTable();
        DataTable dtDestino2 = new DataTable();
        DataTable dtForm = new DataTable();

        bool booAgregando = false;

        BE_CON_PC e_Pc = new BE_CON_PC();
        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[8, 5];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL

        bool booSeEjecuto = false;

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManPC()
        {
            InitializeComponent();
        }

        private void FrmManPC_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void CargarCombos()
        {
            DataTableCargar();
            booAgregando = true;
            funDatos.ComboBoxCargarDataTable(CboDis1, dtDestino, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboDis2, dtDestino2, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboModulo, dtModulo, "n_id", "c_des");
            
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
            objFormVis.ObtenerCabeceraLista(57, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(57);

            objDestinos.mysConec = mysConec;
            objDestinos.Listar();
            dtDestino = objDestinos.dtLista;

            objDestinos.Listar();
            dtDestino2 = objDestinos.dtLista;

            objModulos.mysConec = mysConec;
            objModulos.Listar();
            dtModulo = objModulos.dtLista;
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
            e_Pc = objRegistros.e_Pc;


            TxtCueCon.Text = e_Pc.c_cuecon;
            TxtDes.Text = e_Pc.c_des;

            string c_dato = funDatos.DataTableBuscar(dtLista, "n_id", "c_cuecon", e_Pc.n_ctadesdeb.ToString(), "N").ToString();
            LblidCueDesDeb.Text = e_Pc.n_ctadesdeb.ToString();
            if (c_dato == "0") 
            {
                TxtCueDesDeb.Text = "";
            }
            else
            { 
                TxtCueDesDeb.Text = c_dato;
            }

            c_dato = funDatos.DataTableBuscar(dtLista, "n_id", "c_des", e_Pc.n_ctadesdeb.ToString(), "N").ToString();
            if (c_dato == "0")
            {
                LblCtaDesDeb.Text = "";
            }
            else
            {
                LblCtaDesDeb.Text = c_dato;
            }
            
            c_dato = funDatos.DataTableBuscar(dtLista, "n_id", "c_cuecon", e_Pc.n_ctadeshab.ToString(), "N").ToString();
            LblidCueDesHab.Text = e_Pc.n_ctadeshab.ToString();

            if (c_dato == "0")
            {
                TxtCueDesHab.Text = "";
            }
            else
            { 
                TxtCueDesHab.Text = c_dato;
            }
            c_dato = funDatos.DataTableBuscar(dtLista, "n_id", "c_des", e_Pc.n_ctadeshab.ToString(), "N").ToString();
            if (c_dato == "0")
            {
                LblCtaDesHab.Text = "";
            }
            else
            { 
                LblCtaDesHab.Text = c_dato;
            }

            CboDis1.SelectedValue = e_Pc.n_iddes;
            CboDis2.SelectedValue = e_Pc.n_iddes2;

            if (e_Pc.c_tipsal == "D")
            {
                OptDebe.Checked = true;
                OptHaber.Checked = false;
            }
            else
            {
                OptDebe.Checked = false;
                OptHaber.Checked = true;            
            }

            if (e_Pc.n_desctabal == 0) { ChkAct.Checked = false; ChkPas.Checked = false; }
            if (e_Pc.n_desctabal == 1) { ChkAct.Checked = true; ChkPas.Checked = false; }
            if (e_Pc.n_desctabal == 2) { ChkAct.Checked = false; ChkPas.Checked = true; }

            if (e_Pc.n_documentar == 0) { OptSi.Checked = false; OptNo.Checked = true; }
            if (e_Pc.n_documentar == 1) { OptSi.Checked = true; OptNo.Checked = false; }

            CboModulo.SelectedValue = e_Pc.n_idmodulo;
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
            TxtCueCon.Focus();
        }
        void Blanquea()
        {
            TxtCueCon.Text = "";
            TxtDes.Text = "";
            TxtCueDesDeb.Text = "";
            TxtCueDesHab.Text = "";

            LblidCueDesDeb.Text = "";
            LblidCueDesHab.Text = "";

            CboDis1.SelectedValue = 0;
            CboDis2.SelectedValue = 0;
            CboModulo.SelectedValue = 0;

            OptDebe.Checked = false;
            OptHaber.Checked = false;

            ChkAct.Checked = false; 
            ChkPas.Checked = false;

            OptSi.Checked = false; 
            OptNo.Checked = false;

            LblCtaDesDeb.Text = "";
            LblCtaDesHab.Text = "";
        }
        void Bloquea()
        {
            TxtCueCon.Enabled = !TxtCueCon.Enabled;
            TxtDes.Enabled = !TxtDes.Enabled;
            //TxtCueDesDeb.Enabled = !TxtCueDesDeb.Enabled;
            //TxtCueDesHab.Enabled = !TxtCueDesHab.Enabled;

            CboDis1.Enabled = !CboDis1.Enabled;
            CboDis2.Enabled = !CboDis2.Enabled;
            CboModulo.Enabled = !CboModulo.Enabled;

            OptDebe.Enabled = !OptDebe.Enabled;
            OptHaber.Enabled = !OptHaber.Enabled;

            ChkAct.Enabled = !ChkAct.Enabled;
            ChkPas.Enabled = !ChkPas.Enabled;

            OptSi.Enabled = !OptSi.Enabled;
            OptNo.Enabled = !OptNo.Enabled;

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
            TxtCueCon.Focus();
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
            mysConec = FunMysql.ReAbrirConeccion(mysConec);
            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(e_Pc);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(e_Pc);
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
                e_Pc.n_id = 0;
            }
            else
            {
                e_Pc.n_id = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            }

            e_Pc.c_cuecon = TxtCueCon.Text;
            e_Pc.c_des = TxtDes.Text;
            if (Convert.ToInt32(funFunciones.NulosN(LblidCueDesDeb.Text)) != 0)
            { 
                e_Pc.n_ctadesdeb = Convert.ToInt32(LblidCueDesDeb.Text);
            }
            if (Convert.ToInt32(funFunciones.NulosN(LblidCueDesHab.Text)) != 0)
            { 
                e_Pc.n_ctadeshab = Convert.ToInt32(LblidCueDesHab.Text);
            }
            e_Pc.n_iddes = Convert.ToInt32(CboDis1.SelectedValue);
            e_Pc.n_iddes2= Convert.ToInt32(CboDis2.SelectedValue);
            //e_Pc.n_iddes3
            //e_Pc.n_tipo
            if (OptDebe.Checked == true){ e_Pc.c_tipsal = "D";}
            if (OptHaber.Checked == true){ e_Pc.c_tipsal = "H";}

            //e_Pc.n_dissegsal
            if (OptSi.Checked == true) { e_Pc.n_documentar = 1; }
            if (OptNo.Checked == true) { e_Pc.n_documentar = 0; }
            //
            e_Pc.n_idmodulo = Convert.ToInt32(CboModulo.SelectedValue);
            
            if ((ChkAct.Checked == false) && (ChkPas.Checked == false)) { e_Pc.n_desctabal = 0; }
            if (ChkAct.Checked == true) { e_Pc.n_desctabal = 1; }
            if (ChkPas.Checked == true) { e_Pc.n_desctabal = 2; }
            e_Pc.n_idemp = STU_SISTEMA.EMPRESAID;
        }
        bool CamposOK()
        {
            bool booEstado = true;
            if (TxtCueCon.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de la cuenta contable !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtCueCon.Focus();
                return booEstado;
            }
            if (TxtDes.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la descripcion de la cuenta contable !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtDes.Focus();
                return booEstado;
            }
            if (n_QueHace != 2)
            { 
                if (objRegistros.BuscarNumeroCuenta(TxtCueCon.Text, STU_SISTEMA.EMPRESAID) == true)
                {
                    MessageBox.Show("¡ El numero de cuenta contable ingresado ya existe, ingrese otro!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    TxtCueCon.Focus();
                    return booEstado;
                }
            }
            return booEstado;
        }

        private void FrmManPC_Activated(object sender, EventArgs e)
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

        private void FrmManPC_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }

        private void TxtCueCon_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtCueDesDeb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtCueDesHab_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CboDis1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CboDis2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CmdBusPro_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            dtResult = objRegistros.BuscarCuenta(dtLista);
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtCueDesDeb.Text = dtResult.Rows[0]["c_cuecon"].ToString();
                    LblidCueDesDeb.Text = dtResult.Rows[0]["n_id"].ToString();
                    LblCtaDesDeb.Text = dtResult.Rows[0]["c_des"].ToString();
                }
                else
                {
                    TxtCueDesDeb.Text = "";
                    LblidCueDesDeb.Text = "";
                    LblCtaDesDeb.Text = "";
                }
            }
        }

        private void CmdCtaHab_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            dtResult = objRegistros.BuscarCuenta(dtLista);
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtCueDesHab.Text = dtResult.Rows[0]["c_cuecon"].ToString();
                    LblidCueDesHab.Text = dtResult.Rows[0]["n_id"].ToString();
                    LblCtaDesHab.Text = dtResult.Rows[0]["c_des"].ToString();
                }
                else
                {
                    TxtCueDesHab.Text = "";
                    LblidCueDesHab.Text = "";
                    LblCtaDesHab.Text = "";
                }
            }
        }
    }
}
