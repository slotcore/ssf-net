using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Cooperativa;
using SIAC_Negocio.Cooperativa;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Maestros;
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

namespace SIAC_NET_Cooperativa.Formularios
{
    public partial class FrmManSocios : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_coo_socios objRegistros = new CN_coo_socios();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sun_tipdocide objTipDoc = new CN_sun_tipdocide();
        CN_sun_distritos objDis = new CN_sun_distritos();
        CN_mae_sexo objSexo = new CN_mae_sexo();
        CN_coo_tiposocio objTipSoc = new CN_coo_tiposocio();
        CN_sys_formulario objForm = new CN_sys_formulario();
        
        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles FunCont = new Cls_Controles();

        // ENTIDADES LOCALES
        BE_COO_SOCIOS BE_ListaReg = new BE_COO_SOCIOS();
        BE_COO_SOCIOS BE_Registro = new BE_COO_SOCIOS();

        // DATATABLE LOCALES
        DataTable dtRegistros = new DataTable();
        DataTable dtTipDocIde = new DataTable();
        DataTable dtDistrito = new DataTable();
        DataTable DtSexo = new DataTable();
        DataTable dtTipSoc = new DataTable();
        DataTable dtForm = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[5, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL

        bool booSeEjecuto = false;

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManSocios()
        {
            InitializeComponent();
        }

        private void FrmManSocios_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void CargarCombos()
        {
            DataTable dtResul = new DataTable();
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboTipDocIde, dtTipDocIde, "n_id", "c_des");

            dtResul = funDatos.DataTableFiltrar(dtDistrito, "((n_iddep = 14) AND (n_idpro = 1))");
            funDatos.ComboBoxCargarDataTable(CboDis, dtResul, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboSexo, DtSexo, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipSoc, dtTipSoc, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 587;
            this.Width = 930;
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";
            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
        }
        void DataTableCargar()
        {

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(43);

            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID);

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(43, ref arrCabeceraDg1);

            objTipDoc.mysConec = mysConec;
            dtTipDocIde = objTipDoc.Listar();

            objDis.mysConec = mysConec;
            dtDistrito = objDis.Listar();

            objSexo.mysConec = mysConec;
            DtSexo = objSexo.Listar();

            objTipSoc.mysConec = mysConec;
            dtTipSoc = objTipSoc.Listar(STU_SISTEMA.EMPRESAID);
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

            FunCont.dtpBlanquea(TxtFchIng);
            FunCont.dtpBlanquea(TxtFchNac);

            CboTipDocIde.SelectedValue = BE_Registro.n_ideidtipdoc;
            TxtNumDoc.Text = BE_Registro.c_idenumdoc;
            TxtApe1.Text = BE_Registro.c_ape1;
            TxtApe2.Text = BE_Registro.c_ape2;
            TxtNom1.Text = BE_Registro.c_nom1;
            TxtNom2.Text = BE_Registro.c_nom2;
            CboDis.SelectedValue = BE_Registro.n_iddis;
            TxtDir.Text = BE_Registro.c_dir;

            if (BE_Registro.d_fchnac != null)
            {
                TxtFchNac.Text = Convert.ToDateTime(BE_Registro.d_fchnac).ToString();
            }
            //if (Convert.ToDateTime(BE_Registro.d_fchnac).ToString().Substring(0,10) != "01/01/0001")
            //{
            //    TxtFchNac.Text = Convert.ToDateTime(BE_Registro.d_fchnac).ToString();
            //}

            if (BE_Registro.d_fching != null)
            {
                TxtFchIng.Text = Convert.ToDateTime(BE_Registro.d_fching).ToString();
            }
            //if (Convert.ToDateTime(BE_Registro.d_fching).ToString().Substring(0, 10) != "01/01/0001")
            //{
            //    TxtFchIng.Text = Convert.ToDateTime(BE_Registro.d_fching).ToString();
            //}
            
            CboSexo.SelectedValue = BE_Registro.n_idsex;
            TxtNumTel.Text = BE_Registro.c_numtel;
            TxtNumCel.Text = BE_Registro.c_numcel;
            TxtEmail.Text = BE_Registro.c_email;
            CboTipSoc.SelectedValue = BE_Registro.n_idtipsoc;
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
            CboTipDocIde.Focus();
        }
        void Blanquea()
        {
            CboTipDocIde.SelectedValue = 0;
            TxtNumDoc.Text = "";
            TxtApe1.Text = "";
            TxtApe2.Text = "";
            TxtNom1.Text = "";
            TxtNom2.Text = "";
            CboDis.SelectedValue = 0;
            TxtDir.Text = "";
            TxtFchNac.Text = "";
            TxtFchIng.Text = "";
            CboSexo.SelectedValue = 0;
            TxtNumTel.Text = "";
            TxtNumCel.Text = "";
            TxtEmail.Text = "";
            CboTipSoc.SelectedValue = 0;

            FunCont.dtpBlanquea(TxtFchIng);
            FunCont.dtpBlanquea(TxtFchNac);
        }
        void Bloquea()
        {
            CboTipDocIde.Enabled = !CboTipDocIde.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtApe1.Enabled = !TxtApe1.Enabled;
            TxtApe2.Enabled = !TxtApe2.Enabled;
            TxtNom1.Enabled = !TxtNom1.Enabled;
            TxtNom2.Enabled = !TxtNom2.Enabled;
            CboDis.Enabled = !CboDis.Enabled;
            TxtDir.Enabled = !TxtDir.Enabled;
            TxtFchNac.Enabled = !TxtFchNac.Enabled;
            TxtFchIng.Enabled = !TxtFchIng.Enabled;
            CboSexo.Enabled = !CboSexo.Enabled;
            TxtNumTel.Enabled = !TxtNumTel.Enabled;
            TxtNumCel.Enabled = !TxtNumCel.Enabled;
            TxtEmail.Enabled = !TxtEmail.Enabled;
            CboTipSoc.Enabled = !CboTipSoc.Enabled;
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

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            CboTipDocIde.Focus();
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

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            BE_ListaReg.n_idemp = STU_SISTEMA.EMPRESAID;
            if (n_QueHace == 1)
            {
                BE_ListaReg.n_id = 0;
            }
            else
            {
                BE_ListaReg.n_id = BE_Registro.n_id;
            }
            BE_ListaReg.n_ideidtipdoc = Convert.ToInt32(CboTipDocIde.SelectedValue);
            BE_ListaReg.c_idenumdoc = TxtNumDoc.Text;
            BE_ListaReg.c_ape1 = TxtApe1.Text;
            BE_ListaReg.c_ape2 = TxtApe2.Text;
            BE_ListaReg.c_nom1 = TxtNom1.Text;
            BE_ListaReg.c_nom2 = TxtNom2.Text;
            BE_ListaReg.c_apenom = TxtApe1.Text.Trim() + " " + TxtApe2.Text.Trim() + ", " + TxtNom1.Text.Trim() + TxtNom2.Text.Trim();
            BE_ListaReg.c_dir = TxtDir.Text;
            BE_ListaReg.n_iddis = Convert.ToInt32(CboDis.SelectedValue);
            if (TxtFchNac.Text != " ")
            {
                BE_ListaReg.d_fchnac = Convert.ToDateTime(TxtFchNac.Text);
            }
            else
            {
                BE_ListaReg.d_fchnac = null;
            }

            BE_ListaReg.c_numtel = TxtNumTel.Text;
            BE_ListaReg.c_numcel = TxtNumCel.Text ;
            BE_ListaReg.c_email = TxtEmail.Text;
            BE_ListaReg.n_idsex = Convert.ToInt32(CboSexo.SelectedValue);
            BE_ListaReg.n_idtipsoc = Convert.ToInt32(CboTipSoc.SelectedValue);
            if (TxtFchIng.Text != " ")
            {
                BE_ListaReg.d_fching = Convert.ToDateTime(TxtFchIng.Text);
            }
            else
            {
                BE_ListaReg.d_fching = null;
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt32(CboTipDocIde.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento de identidad !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTipDocIde.Focus();
                return booEstado;
            }

            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento de identidad !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumDoc.Focus();
                return booEstado;
            }

            if (TxtApe1.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el primer apellido del socio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtApe1.Focus();
                return booEstado;
            }

            if (TxtApe2.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el segundo apellido del socio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtApe2.Focus();
                return booEstado;
            }

            if (TxtNom1.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el primer nombre del socio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNom1.Focus();
                return booEstado;
            }

            //if (TxtNom2.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado el segundo nombre del socio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    TxtNom2.Focus();
            //    return booEstado;
            //}

            if (Convert.ToInt32(CboDis.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el distrito de residencia del socio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboDis.Focus();
                return booEstado;
            }

            if (TxtDir.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la direccion de residencia del socio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtDir.Focus();
                return booEstado;
            }

            if (Convert.ToInt32(CboTipSoc.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de socio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTipSoc.Focus();
                return booEstado;
            }
            return booEstado;
        }
        private void FrmManSocios_Activated(object sender, EventArgs e)
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
                int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    VerRegistro(intIdRegistro);
                }
            }
        }
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
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
        private void FrmManSocios_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 110, this.Width - 38);
        }
        private void TxtApe1_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtApe2_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtNom1_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtNom2_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtDir_KeyPress(object sender, KeyPressEventArgs e)
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
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void TxtFchNac_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                TxtFchNac.Text = "";
                TxtFchNac.CustomFormat = " ";
                TxtFchNac.Format = DateTimePickerFormat.Custom;
            }
        }

        private void TxtFchIng_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                TxtFchIng.Text = "";
                TxtFchIng.CustomFormat = " ";
                TxtFchIng.Format = DateTimePickerFormat.Custom;
            }
        }

        private void TxtFchIng_ValueChanged(object sender, EventArgs e)
        {
            TxtFchIng.CustomFormat = "dd/MM/yyyy";
            TxtFchIng.Format = DateTimePickerFormat.Custom;
        }

        private void TxtFchNac_ValueChanged(object sender, EventArgs e)
        {
            TxtFchNac.CustomFormat = "dd/MM/yyyy";
            TxtFchNac.Format = DateTimePickerFormat.Custom;
        }
    }
}
