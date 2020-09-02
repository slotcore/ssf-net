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
    public partial class FrmManTipDocCom : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sun_tipdoccom objRegistros = new CN_sun_tipdoccom();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        
        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        DataTable dtLista = new DataTable();
        DataTable dtForm = new DataTable();
        
        bool booAgregando = false;

        BE_SUN_TIPDOCCOM e_DocComercial = new BE_SUN_TIPDOCCOM();
        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[5, 5];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL

        bool booSeEjecuto = false;

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManTipDocCom()
        {
            InitializeComponent();
        }

        private void FrmManTipDocCom_Load(object sender, EventArgs e)
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
            dtLista = objRegistros.ListarVista();
            
            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(62, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(62);
        }
        void ListarItems()
        {
            // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
            objRegistros.mysConec = mysConec;
            dtLista = objRegistros.ListarVista();
            
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
            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdRegistro);
            e_DocComercial = objRegistros.e_DocCom;

            TxtCodSun.Text = e_DocComercial.c_codsun;
            TxtDes.Text = e_DocComercial.c_des;
            TxtAbr.Text = e_DocComercial.c_abr;
            TxtNumFil.Text = e_DocComercial.n_numfil.ToString();

            if (e_DocComercial.n_seimp == 1)
            {
                OptSiImp.Checked = true;
                OptNoImp.Checked = false;
            }
            else
            {
                OptSiImp.Checked = false;
                OptNoImp.Checked = true;
            }

            if (e_DocComercial.n_idtipo == 1)
            {
                OptAdm.Checked = false;
                OptCon.Checked = true;
            }
            else
            {
                OptAdm.Checked = true;
                OptCon.Checked = false;
            }

            if (e_DocComercial.n_esent == 1) { OptIng.Checked = true; OptSal.Checked = false; }
            if (e_DocComercial.n_essal == 1) { OptIng.Checked = false; OptSal.Checked = true; }

            if (e_DocComercial.n_activo == 1)
            {
                ChkAct.Checked = true;
            }
            else
            {
                ChkAct.Checked = false;
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
            TxtCodSun.Focus();
        }
        void Blanquea()
        {
            TxtDes.Text = "";
            TxtCodSun.Text = "";
            TxtAbr.Text = "";
            TxtNumFil.Text = "";
            OptSiImp.Checked = false;
            OptNoImp.Checked = false;
            OptAdm.Checked = false;
            OptCon.Checked = false;
            OptIng.Checked = false;
            OptSal.Checked = false;
            ChkAct.Checked = true;
        }
        void Bloquea()
        {
            TxtDes.Enabled = !TxtDes.Enabled;
            TxtCodSun.Enabled = !TxtCodSun.Enabled;
            TxtAbr.Enabled = !TxtAbr.Enabled;
            TxtNumFil.Enabled = !TxtNumFil.Enabled;
            OptSiImp.Enabled = !OptSiImp.Enabled;
            OptNoImp.Enabled = !OptNoImp.Enabled;
            OptAdm.Enabled = !OptAdm.Enabled;
            OptCon.Enabled = !OptCon.Enabled;
            OptIng.Enabled = !OptIng.Enabled;
            OptSal.Enabled = !OptSal.Enabled;
            ChkAct.Enabled = !ChkAct.Enabled;
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
            TxtCodSun.Focus();
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
                booResultado = objRegistros.Insertar(e_DocComercial);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(e_DocComercial);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
            
            e_DocComercial.c_codsun = TxtCodSun.Text;
            e_DocComercial.c_des = TxtDes.Text;
            e_DocComercial.c_abr = TxtAbr.Text;
            e_DocComercial.n_numfil = Convert.ToInt32(TxtNumFil.Text);

            if (OptAdm.Checked == true) { e_DocComercial.n_idtipo = 2;}
            if (OptCon.Checked == true) { e_DocComercial.n_idtipo = 1;}

            if (OptIng.Checked == true){  e_DocComercial.n_esent = 1;}
            if (OptSal.Checked == true){  e_DocComercial.n_essal = 1;}
                        
            if (OptSiImp.Checked ==true){e_DocComercial.n_seimp = 1;}
            if (OptNoImp.Checked ==true){e_DocComercial.n_seimp = 0;}

            if (ChkAct.Checked == true){e_DocComercial.n_activo = 1;}
            if (ChkAct.Checked == false) { e_DocComercial.n_activo = 2; }
            
            //e_DocComercial.c_preser
            //e_DocComercial.n_escom
        }
        bool CamposOK()
        {
            bool booEstado = true;
            if (TxtCodSun.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el codigo de la sunat paraeste documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtCodSun.Focus();
                return booEstado;
            }
            if (TxtDes.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la descripcion del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtDes.Focus();
                return booEstado;
            }
 
            if (TxtAbr.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la abrefviatura del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtAbr.Focus();
                return booEstado;
            }

            if ((OptSiImp.Checked == true) && (OptNoImp.Checked == true))
            {
                MessageBox.Show("¡ No ha especificado si el documento se imprimira por el sistema !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                OptSiImp.Focus();
                return booEstado;
            }

            if (OptSiImp.Checked == true)
            { 
                if (TxtNumFil.Text == "")
                {
                    MessageBox.Show("¡ No ha el numero de filas para el documento!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    OptSiImp.Focus();
                    return booEstado;
                }
            }
            
            if ((OptAdm.Checked == true) && (OptCon.Checked == true))
            {
                MessageBox.Show("¡ No ha especificado el tipo del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                OptAdm.Focus();
                return booEstado;
            }

            if ((OptIng.Checked == true) && (OptSal.Checked == true))
            {
                MessageBox.Show("¡ No ha especificado el tipo de movimiento que se realizara con el documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                OptIng.Focus();
                return booEstado;
            }
            return booEstado;
        }

        private void FrmManTipDocCom_Activated(object sender, EventArgs e)
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

        private void FrmManTipDocCom_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtNumFil_KeyPress(object sender, KeyPressEventArgs e)
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
