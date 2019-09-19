using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Sistema;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Objetos.Sistema;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SSF_NET.Formularios
{
    public partial class FrmEmpresaUsuario : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sys_empresa o_Empresa = new CN_sys_empresa();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sun_tipdocide o_DocIde = new CN_sun_tipdocide();
        CN_mae_clipro o_CliPro = new CN_mae_clipro();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        
        DataTable dtLista = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtDocIde = new DataTable();
        DataTable dtCliPro = new DataTable();
        bool booAgregando;

        BE_SYS_EMPRESA e_Empresa = new BE_SYS_EMPRESA();
        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[7, 5];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL

        bool b_SeEjecuto = false;

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmEmpresaUsuario()
        {
            InitializeComponent();
        }

        private void FrmEmpresaUsuario_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void CargarCombos()
        {
            DataTableCargar();
            booAgregando = true;
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtDocIde, "n_id", "c_des");

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
            o_Empresa.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            o_Empresa.Listar();
            dtLista = o_Empresa.dtLista;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(80, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(80);

            o_DocIde.mysConec = mysConec;
            dtDocIde = o_DocIde.Listar();

            o_CliPro.mysConec = mysConec;
            dtCliPro = o_CliPro.Listar(0, 1);
        }
        void ListarItems()
        {
            // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
            o_Empresa.mysConec = mysConec;
            o_Empresa.Listar();
            dtLista = o_Empresa.dtLista;

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
            //string c_dato = "";
            DataTable dtResul = new DataTable();

            o_Empresa.mysConec = mysConec;
            o_Empresa.TraerRegistro(n_IdRegistro);
            e_Empresa = o_Empresa.e_Empresa;

            dtResul = funDatos.DataTableFiltrar(dtCliPro, "n_id = " + e_Empresa.n_idclipro.ToString() + "");
            if (dtResul.Rows.Count != 0)
            { 
                LblIdCliPro.Text = dtResul.Rows[0]["n_id"].ToString();
                LblCliPro.Text = dtResul.Rows[0]["c_nombre"].ToString();
            }

            TxtEmp.Text = e_Empresa.c_nomemp;
            CboTipDoc.SelectedValue = e_Empresa.n_idtipdoc;
            TxtNumDoc.Text = e_Empresa.c_numdoc;
            txtNomCor.Text = e_Empresa.c_nomcorto;
            if (e_Empresa.n_activo == 1) { OptSiAct.Checked = true; }
            if (e_Empresa.n_activo == 2) { OptNoAct.Checked = true; }
            TxtDir.Text = e_Empresa.c_dir;

            TxtRutDat.Text = e_Empresa.c_vtafearcdat;
            TxtRutEnv.Text = e_Empresa.c_vtafearcenv;
            TxtRutRec.Text = e_Empresa.c_vtafearcrec;
            TxtRutEnv2.Text = e_Empresa.c_vtafealmenv;
            TxtRutRpta.Text = e_Empresa.c_vtafealmrpt;
            PicLogo.Image = funFunciones.Convertir_Bytes_Imagen(e_Empresa.c_logo);
            TxtCtaEmailVen.Text = e_Empresa.c_corctaven;
            TxtResVen.Text = e_Empresa.c_nomjefven;
            //e_Empresa.c_corctalog = "";
            //e_Empresa.c_corctapro = "";
            //e_Empresa.c_corctager = "";
            
            //e_Empresa.c_nomjeflog = "";

            TxtCuePOP.Text = e_Empresa.c_corrctapop;
            TxtCuePOPCon.Text = e_Empresa.c_corrctapopcon;
            
            if (e_Empresa.n_aplife == 2) { OptSi.Checked = true; }
            if (e_Empresa.n_aplife == 1) { OptNo.Checked = true; }

            TxtFoderImp.Text = e_Empresa.c_folderimp;

            //e_Empresa.c_nomemp = TxtEmp.Text;
            //e_Empresa.n_idtipdoc = Convert.ToInt16(CboTipDoc.SelectedValue);
            //e_Empresa.c_numdoc = TxtNumDoc.Text;
            //e_Empresa.c_nomcorto = txtNomCor.Text;

            //if (OptSiAct.Checked == true) { e_Empresa.n_activo = 1; }
            //if (OptNoAct.Checked == true) { e_Empresa.n_activo = 0; }

            //e_Empresa.c_dir = TxtDir.Text;
            //e_Empresa.c_codtempus = "";
            //e_Empresa.c_vtafearcdat = TxtRutDat.Text;
            //e_Empresa.c_vtafearcenv = TxtRutEnv.Text;
            //e_Empresa.c_vtafearcrec = TxtRutRec.Text;
            //e_Empresa.c_vtafealmenv = TxtRutEnv2.Text;
            //e_Empresa.c_vtafealmrpt = TxtRutRpta.Text;
            //e_Empresa.c_logo = funFunciones.Convertir_Imagen_Bytes(PicLogo.Image);
            //e_Empresa.c_corctaven = TxtCtaEmailVen.Text;
            //e_Empresa.c_corctalog = "";
            //e_Empresa.c_nomjefven = TxtResVen.Text;
            
            //e_Empresa.c_corrctapop = TxtCuePOP.Text;
            //e_Empresa.c_corrctapopcon = TxtCuePOPCon.Text;

            //if (OptSi.Checked == true) { e_Empresa.n_aplife = 2; }
            //if (OptNo.Checked == true) { e_Empresa.n_aplife = 1; }

            //e_Empresa.c_folderimp = TxtFoderImp.Text;
            //e_Empresa.n_idclipro = 0;
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
            OptNo.Checked = true;
            TxtEmp.Focus();
        }
        void Blanquea()
        {
            TxtEmp.Text = "";
            txtNomCor.Text = "";
            TxtDir.Text = "";
            CboTipDoc.SelectedValue = 0;
            TxtNumDoc.Text = "";

            TxtRutDat.Text = "";
            TxtRutEnv.Text = "";
            TxtRutRec.Text = "";
            TxtRutEnv2.Text = "";
            TxtCuePOP.Text = "";
            TxtCuePOPCon.Text = "";
            TxtCtaEmailVen.Text = "";
            TxtResVen.Text = "";
        }
        void Bloquea()
        {
            TxtEmp.Enabled = !TxtEmp.Enabled;
            txtNomCor.Enabled = !txtNomCor.Enabled;
            TxtDir.Enabled = !TxtDir.Enabled;
            CboTipDoc.Enabled = !CboTipDoc.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;

            //TxtRutDat.Enabled = !TxtRutDat.Enabled;
            //TxtRutEnv.Enabled = !TxtRutEnv.Enabled;
            //TxtRutRec.Enabled = !TxtRutRec.Enabled;
            //TxtRutEnv2.Enabled = !TxtRutEnv2.Enabled;
            TxtCuePOP.Enabled = !TxtCuePOP.Enabled;
            TxtCuePOPCon.Enabled = !TxtCuePOPCon.Enabled;
            TxtCtaEmailVen.Enabled = !TxtCtaEmailVen.Enabled;
            TxtResVen.Enabled = !TxtResVen.Enabled;

            CmdBus1.Enabled = !CmdBus1.Enabled;
            CmdBus2.Enabled = !CmdBus2.Enabled;
            CmdBus3.Enabled = !CmdBus3.Enabled;
            CmdBus4.Enabled = !CmdBus4.Enabled;
            CmdBus5.Enabled = !CmdBus5.Enabled;
            CmdBus6.Enabled = !CmdBus6.Enabled;
            CmdBusCliPro.Enabled = !CmdBusCliPro.Enabled;
            CmdBusImg.Enabled = !CmdBusImg.Enabled;

            OptSiAct.Enabled = !OptSiAct.Enabled;
            OptNoAct.Enabled = !OptNoAct.Enabled;

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

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            TxtEmp.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int n_IdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (o_Empresa.Eliminar(n_IdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + o_Empresa.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
                booResultado = o_Empresa.Insertar(e_Empresa);
            }

            if (n_QueHace == 2)
            {
                booResultado = o_Empresa.Actualizar(e_Empresa);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + o_Empresa.n_ErrorNumber.ToString() + " = " + o_Empresa.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            if (n_QueHace == 1)
            {
                e_Empresa.n_id = 0;
            }
            else
            {
                e_Empresa.n_id = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            }
            
            e_Empresa.c_nomemp = TxtEmp.Text;
            e_Empresa.n_idtipdoc = Convert.ToInt16(CboTipDoc.SelectedValue);
            e_Empresa.c_numdoc = TxtNumDoc.Text;
            e_Empresa.c_nomcorto = txtNomCor.Text;

            if (OptSiAct.Checked == true) { e_Empresa.n_activo = 1;}
            if (OptNoAct.Checked == true) { e_Empresa.n_activo = 0; }
            
            e_Empresa.c_dir = TxtDir.Text;
            e_Empresa.c_codtempus = "";
            e_Empresa.c_vtafearcdat = TxtRutDat.Text;
            e_Empresa.c_vtafearcenv = TxtRutEnv.Text;
            e_Empresa.c_vtafearcrec = TxtRutRec.Text;
            e_Empresa.c_vtafealmenv = TxtRutEnv2.Text;
            e_Empresa.c_vtafealmrpt = TxtRutRpta.Text;
            e_Empresa.c_logo = funFunciones.Convertir_Imagen_Bytes(PicLogo.Image);
            e_Empresa.c_corctaven = TxtCtaEmailVen.Text;
            e_Empresa.c_corctalog = "";
            e_Empresa.c_corctapro = "";
            e_Empresa.c_corctager = "";
            e_Empresa.c_nomjefven= TxtResVen.Text;
            e_Empresa.c_nomjeflog = "";
            e_Empresa.c_corrctapop= TxtCuePOP.Text;
            e_Empresa.c_corrctapopcon = TxtCuePOPCon.Text;
            
            if (OptSi.Checked == true) {e_Empresa.n_aplife = 2;}
            if (OptNo.Checked == true) {e_Empresa.n_aplife = 1;}
            
            e_Empresa.c_folderimp = TxtFoderImp.Text;
            e_Empresa.n_idclipro = 0;
        }
        bool CamposOK()
        {
            bool booEstado = true;
            if (TxtEmp.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la descripcion de la detraccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtEmp.Focus();
                return booEstado;
            }
            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la tasa de la detraccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumDoc.Focus();
                return booEstado;
            }
            if (TxtCuePOP.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el importe base para aplicar la detraccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtCuePOP.Focus();
                return booEstado;
            }
            return booEstado;
        }

        private void FrmEmpresaUsuario_Activated(object sender, EventArgs e)
        {
            if (b_SeEjecuto == false)
            {
                b_SeEjecuto = true;
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
                o_Empresa.mysConec = mysConec;
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
            o_Empresa = null;
            objFormVis = null;
            this.Close();
        }

        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (n_QueHace != 3) { return; }

            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    VerRegistro(intIdRegistro);
                }
            }
        }

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            VerRegistro(intIdRegistro);
        }

        private void Tab1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void FrmEmpresaUsuario_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }

        private void CmdBus1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                TxtRutDat.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void CmdBus2_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                TxtRutEnv.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void CmdBus3_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                TxtRutRec.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void CmdBus4_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                TxtRutEnv2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void CmdBus5_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                TxtFoderImp.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        private void CmdBusCliPro_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            o_CliPro.mysConec = mysConec;
            dtResult = o_CliPro.BuscarCliPro(dtCliPro, 1, "n_id", "");
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    LblIdCliPro.Text = dtResult.Rows[0]["n_id"].ToString();
                    LblCliPro.Text = dtResult.Rows[0]["c_nombre"].ToString();
                }
                else
                {
                    LblIdCliPro.Text = "";
                    LblCliPro.Text = "";
                }
            }
        }
        private void CmdBus6_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                TxtRutRpta.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        private void CmdBusImg_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string imagen = openFileDialog1.FileName;
                    PicLogo.Image = Image.FromFile(imagen);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("El archivo seleccionado no es un tipo de imagen válido");
            }
        }
    }
}
