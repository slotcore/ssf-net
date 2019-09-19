using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Estacionamiento;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Estacionamiento;
using SIAC_Negocio.Planilla;
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

namespace SIAC_NET_Estacionamientos.Formularios
{
    public partial class FrmSetupPlaya : Form
    {
        // VARIABLES PUBLICAS
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_empresalocal o_emploc = new CN_sys_empresalocal();
        CN_sun_tipdoccom o_tipdoc = new CN_sun_tipdoccom();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_EST_LOCALSETUP BE_Registro = new BE_EST_LOCALSETUP();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO
        DataTable dtemp = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtLocal = new DataTable(); 
        DataTable dtSer = new DataTable();
        DataTable dtDoc = new DataTable();
        DataTable dtSerPla = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[8, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[4, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "EF1234567890" + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;

        public FrmSetupPlaya()
        {
            InitializeComponent();
        }
        private void FrmSetupPlaya_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboLocal, dtLocal, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboDocDef, dtDoc, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboSerDef, dtSer, "n_id", "c_des");
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
        }
        void DataTableCargar()
        {
            CN_est_localsetup objRegistros = new CN_est_localsetup(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;                                   
            objRegistros.Listar(STU_SISTEMA.EMPRESAID);
            dtLista = objRegistros.dtListar;
            objRegistros = null;

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            objFormVis.mysConec = o_conec.mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(87, ref arrCabeceraDg1);

            objForm.mysConec = o_conec.mysConec;                                        // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(87);

            o_emploc.mysConec = o_conec.mysConec;
            dtLocal = o_emploc.Listar(STU_SISTEMA.EMPRESAID, 0);

            o_tipdoc.mysConec = o_conec.mysConec;
            dtDoc = o_tipdoc.Listar();
            dtDoc = funDatos.DataTableFiltrar(dtDoc, "n_id IN(2, 4, 13)");

            CN_est_servicios o_ser = new CN_est_servicios(STU_SISTEMA);
            o_ser.STU_SISTEMA = STU_SISTEMA;
            dtSer = o_ser.Listar(STU_SISTEMA.EMPRESAID);
            o_ser = null;

            CN_est_servicios o_serpla = new CN_est_servicios(STU_SISTEMA);
            o_serpla.STU_SISTEMA = STU_SISTEMA;
            dtSerPla = o_serpla.Listar(STU_SISTEMA.EMPRESAID);
            o_serpla = null;
           
            CN_pla_empleados o_emp = new CN_pla_empleados(STU_SISTEMA);
            o_emp.STU_SISTEMA = STU_SISTEMA;
            o_emp.Listar(STU_SISTEMA.EMPRESAID);
            dtemp = o_emp.dtLista;
            o_emp = null;
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

            CN_est_localsetup objRegistros = new CN_est_localsetup(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;                                   

            objRegistros.TraerRegistro(n_IdRegistro);
            if (objRegistros.b_OcurrioError == false)
            {
                BE_Registro = objRegistros.e_Local;
              
                CboLocal.SelectedValue = BE_Registro.n_idloc;
                
                if (BE_Registro.n_idtipcob == 1) { OptTipCob1.Checked = true; }
                if (BE_Registro.n_idtipcob == 2) { OptTipCob2.Checked = true; }
                if (BE_Registro.n_idtipcob == 3) { OptTipCob3.Checked = true; }
                TxtFac.Text = BE_Registro.c_numserfac;
                TxtBol.Text = BE_Registro.c_numserbol;
                TxtTik.Text = BE_Registro.c_numsertik;
                TxtTol.Text = BE_Registro.n_tolmin.ToString();
                CboDocDef.SelectedValue = BE_Registro.n_iddocdef;
                CboSerCobAdi.SelectedValue = BE_Registro.n_idserhor;

                DataTable dtRes = new DataTable();
                DataTable dtRes1 = new DataTable();
                dtRes = funDatos.DataTableFiltrar(dtSer, "n_idpla = " + Convert.ToInt16(CboLocal.SelectedValue) + "");
                funDatos.ComboBoxCargarDataTable(CboSerDef, dtRes, "n_id", "c_des");
                CboSerDef.SelectedValue = BE_Registro.n_idserdef;

                dtRes1 = funDatos.DataTableFiltrar(dtSer, "n_idpla = " + Convert.ToInt16(CboLocal.SelectedValue) + "");
                funDatos.ComboBoxCargarDataTable(CboSerCobAdi, dtRes1, "n_id", "c_des");
                CboSerCobAdi.SelectedValue = BE_Registro.n_idserhor;

                if (BE_Registro.n_vispre == 1) {ChkVisPre.Checked = false;}
                if (BE_Registro.n_vispre == 2) {ChkVisPre.Checked = true;}
            }
            objRegistros = null;
        }
        void Nuevo()
        {
            n_QueHace = 1;
            Tab1.TabPages[0].Enabled = false;
            booAgregando = true;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            Tab1.SelectedIndex = 1;
            CboSerDef.Enabled = false;
            OptTipCob2.Checked = true;
            dtSerPla = funDatos.DataTableFiltrar(dtSerPla, "n_idpla = " + Convert.ToInt16(CboLocal.SelectedValue) + "");
            funDatos.ComboBoxCargarDataTable(CboSerCobAdi, dtSerPla, "n_id", "c_des");
            CboLocal.Focus();
            booAgregando = false;
        }
        void Blanquea()
        {
            CboLocal.SelectedValue = 0;
            OptTipCob1.Checked = false;
            OptTipCob2.Checked = false;
            CboDocDef.SelectedValue = 0;
            CboSerDef.SelectedValue = 0;
            TxtFac.Text = "";
            TxtBol.Text = "";
            TxtTik.Text = "";
            TxtTol.Text = "";
            ChkVisPre.Checked = false;
        }
        void Bloquea()
        {
            CboLocal.Enabled = !CboLocal.Enabled;
            OptTipCob1.Enabled = !OptTipCob1.Enabled;
            OptTipCob2.Enabled = !OptTipCob2.Enabled;
            OptTipCob3.Enabled = !OptTipCob3.Enabled;

            CboDocDef.Enabled = !CboDocDef.Enabled;
            CboSerDef.Enabled = !CboSerDef.Enabled;
            CboSerCobAdi.Enabled = !CboSerCobAdi.Enabled;

            TxtFac.Enabled = !TxtFac.Enabled;
            TxtBol.Enabled = !TxtBol.Enabled;
            TxtTik.Enabled = !TxtTik.Enabled;

            TxtTol.Enabled = !TxtTol.Enabled;
            ChkVisPre.Enabled = !ChkVisPre.Enabled;
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

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            VerRegistro(intIdRegistro);       
            CboSerDef.Enabled = true;
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;

            //dtSerPla = funDatos.DataTableFiltrar(dtSerPla, "n_idpla = " + Convert.ToInt16(CboLocal.SelectedValue) + "");
            //funDatos.ComboBoxCargarDataTable(CboSerCobAdi, dtSerPla, "n_id", "c_des");
            CboLocal.Focus();
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                CN_est_localsetup objRegistros = new CN_est_localsetup(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;   
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.Listar(STU_SISTEMA.EMPRESAID);
                    dtLista = objRegistros.dtListar;
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                objRegistros = null;
            }
            return booResult;
        }
        void Cancelar()
        {
            n_QueHace = 3;
            ActivarTool();
            CboSerDef.Enabled = false;
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

            CN_est_localsetup objRegistros = new CN_est_localsetup(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;   
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
            objRegistros = null;
            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_id = 0;

            //if (n_QueHace == 1)
            //{
            //    n_id = 0;
            //}
            //else
            //{
            //    n_id = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            //}
            
            BE_Registro.n_idloc = Convert.ToInt16(CboLocal.SelectedValue);
            if (OptTipCob1.Checked == true) { BE_Registro.n_idtipcob = 1; }
            if (OptTipCob2.Checked == true) { BE_Registro.n_idtipcob = 2; }
            if (OptTipCob3.Checked == true) { BE_Registro.n_idtipcob = 3; }

            BE_Registro.c_numserfac = TxtFac.Text;
            BE_Registro.c_numserbol = TxtBol.Text;
            BE_Registro.c_numsertik = TxtTik.Text;
            BE_Registro.n_iddocdef = Convert.ToInt16(CboDocDef.SelectedValue);
            BE_Registro.n_idserdef = Convert.ToInt16(CboSerDef.SelectedValue);
            BE_Registro.n_idserhor = Convert.ToInt16(CboSerCobAdi.SelectedValue);
            BE_Registro.n_tolmin = Convert.ToInt16(TxtTol.Text);

            if (ChkVisPre.Checked == false) { BE_Registro.n_vispre = 1; }
            if (ChkVisPre.Checked == true) { BE_Registro.n_vispre = 2; }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt16(CboLocal.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre del Local !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboLocal.Focus();
                return booEstado;
            }
            if ((OptTipCob1.Checked == false) && (OptTipCob2.Checked == false) && (OptTipCob3.Checked == false))
            {
                MessageBox.Show("¡ No ha especificado el tipo de cobranza para este local !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                OptTipCob1.Focus();
                return booEstado;
            }
            
            if (Convert.ToInt16(CboDocDef.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el documento por defecto para este local !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboDocDef.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboSerDef.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el servicio por defecto para este local !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboSerDef.Focus();
                return booEstado;
            }
            if (TxtFac.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie para la factura de este local !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFac.Focus();
                return booEstado;
            }
            if (TxtBol.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie para la boleta de este local !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtBol.Focus();
                return booEstado;
            }
            if (TxtTik.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie para el ticket de este local !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtTik.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboSerCobAdi.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el servicio para el cobro adicional de horas !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboSerCobAdi.Focus();
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
                CN_est_localsetup objRegistros = new CN_est_localsetup(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;   
                objRegistros.Listar(STU_SISTEMA.EMPRESAID);
                dtLista = objRegistros.dtListar;
                objRegistros = null;
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
        private void FrmSetupPlaya_Activated(object sender, EventArgs e)
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
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
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
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void CboLocal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CboLocal_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            
            CboSerDef.Enabled = true;
            DataTable dtRes = new DataTable();
            dtRes = funDatos.DataTableFiltrar(dtSer, "n_idpla = " + Convert.ToInt16(CboLocal.SelectedValue) + "");
            funDatos.ComboBoxCargarDataTable(CboSerDef, dtRes, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboSerCobAdi, dtSerPla, "n_id", "c_des");
        }

        private void CboLocal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CboDocDef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }

        }

        private void CboSerDef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }

        }

        private void TxtFac_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtBol_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtTik_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtFac_Validated(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            string strCad = "0000" + TxtFac.Text;
            if (TxtFac.Text.Length != 4)
            {
                TxtFac.Text = "0" + strCad.Substring(strCad.Length - 3, 3);
            }
        }

        private void TxtBol_Validated(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            string strCad = "0000" + TxtBol.Text;
            if (TxtBol.Text.Length != 4)
            {
                TxtBol.Text = "0" + strCad.Substring(strCad.Length - 3, 3);
            }
        }

        private void TxtTik_Validated(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            string strCad = "0000" + TxtTik.Text;
            if (TxtTik.Text.Length != 4)
            {
                TxtTik.Text = "0" + strCad.Substring(strCad.Length - 3, 3);
            }
        }

        private void FrmSetupPlaya_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }

        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        } 
    }
}
