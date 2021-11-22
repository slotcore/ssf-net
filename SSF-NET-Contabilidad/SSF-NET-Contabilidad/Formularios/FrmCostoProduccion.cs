using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using System.Data.OleDb;
using SIAC_DATOS.Models.Contabilidad;
using SIAC_DATOS.Models.Maestros;
using SIAC_DATOS.Models.Sunat;
using SIAC_DATOS.Models.Sistema;
using System.Collections.Generic;
using SIAC_DATOS.Classes.Contabilidad;
using SIAC_DATOS.Models.Almacen;

namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmCostoProduccion : Form
    {
        // VARIABLES PUBLICAS
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // ENTIDADES LOCALES
        CostoProduccion m_CostoProduccion = new CostoProduccion();

        // VARIABLES LOCALES
        // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        int n_QueHace = 3;
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;
        int n_idformulario = 103;
        
        public FrmCostoProduccion()
        {
            InitializeComponent();
        }

        private void FrmSalidaAlmacen3_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }

        void CargarCombos()
        {
            DataTableCargar();

            CboMeses.DataSource = Mes.FetchList();
            CboMeses.DisplayMember = "c_des";
            CboMeses.ValueMember = "n_id";

            CboResponsable.DataSource = PersonalContabilidad.FetchList(STU_SISTEMA.EMPRESAID, 2);
            CboResponsable.DisplayMember = "c_destra";
            CboResponsable.ValueMember = "n_idtra";

            CboConfiguracion.DataSource = ConfigVal.FetchList(STU_SISTEMA.EMPRESAID);
            CboConfiguracion.DisplayMember = "c_des";
            CboConfiguracion.ValueMember = "n_id";
        }

        void ConfigurarFormulario()
        {
            Formulario formulario = Formulario.Fetch(n_idformulario);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";
            this.Text = formulario.c_titfor;
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }

        void DataTableCargar()
        {
        }

        void ListarItems()
        {
            costoProduccionBindingSource.DataSource = CostoProduccion.FetchList(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO);
            LblNumReg.Text = (costoProduccionBindingSource.Count).ToString();
        }

        void VerRegistro()
        {
            try
            {
                int n_id = ((CostoProduccion)costoProduccionBindingSource.Current).n_id;
                m_CostoProduccion = CostoProduccion.Fetch(n_id);

                TxtNumDoc.Text = m_CostoProduccion.c_numdoc;
                TxtNumSer.Text = m_CostoProduccion.c_numser;
                CboConfiguracion.SelectedValue = m_CostoProduccion.n_idconfigval;
                TxtObs.Text = m_CostoProduccion.c_obs;
                CboResponsable.SelectedValue = m_CostoProduccion.n_idresp;
                CboMeses.SelectedValue = m_CostoProduccion.n_idmes;

                CostoProduccionDetBindingSource.DataSource = m_CostoProduccion.CostoProduccionDets;
            }
            catch (Exception ex)
            {
                MessageBox.Show("¡ Ocurrió un error ! " + ex.Message, "Ver Registro", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        void Nuevo()
        {
            booAgregando = true;
            n_QueHace = 1;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            Tab1.SelectedIndex = 1;
            booAgregando = false;
        }

        void Blanquea()
        {
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtObs.Text = "";
            
            CboConfiguracion.SelectedValue = 0;
            CboResponsable.SelectedValue = 0;
            
        }

        void Bloquea()
        {
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
            CboConfiguracion.Enabled = !CboConfiguracion.Enabled;
            CboResponsable.Enabled = !CboResponsable.Enabled;
            CboMeses.Enabled = !CboMeses.Enabled;

            if (BtnBuscarParte.Enabled == ComponentFactory.Krypton.Toolkit.ButtonEnabled.True)
                BtnBuscarParte.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            else
                BtnBuscarParte.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;

            if (BtnProcesarMP.Enabled == ComponentFactory.Krypton.Toolkit.ButtonEnabled.True)
                BtnProcesarMP.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            else
                BtnProcesarMP.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;

            if (BtnProcesarModCif.Enabled == ComponentFactory.Krypton.Toolkit.ButtonEnabled.True)
                BtnProcesarModCif.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            else
                BtnProcesarModCif.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;

            if (BtnAgregarCuenta.Enabled == ComponentFactory.Krypton.Toolkit.ButtonEnabled.True)
                BtnAgregarCuenta.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            else
                BtnAgregarCuenta.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;

            if (BtnEliminarCuenta.Enabled == ComponentFactory.Krypton.Toolkit.ButtonEnabled.True)
                BtnEliminarCuenta.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            else
                BtnEliminarCuenta.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;
        }

        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir.Enabled = !ToolImprimir.Enabled;
            ToolManFun.Enabled = !ToolManFun.Enabled;
            //ToolExportar.Enabled = !ToolExportar.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
        }

        void Modificar()
        {
            booAgregando = true;
            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            //int intIdRegistro = Convert.ToInt32(DgLista.Columns[9].CellValue(DgLista.Row).ToString());

            VerRegistro();
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            CboMeses.Focus();
            booAgregando = false;
        }

        private void EliminarRegistro()
        {
            try
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns[9].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

                m_CostoProduccion = CostoProduccion.Fetch(intIdRegistro);

                DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (DialogResult.Yes == Rpta)
                {
                    //objMovimientos.AccConec = AccConec;
                    m_CostoProduccion.Delete();
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
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

        private void Grabar()
        {
            ValidarCampos();
            AsignarEntidad();

            if (n_QueHace == 1)
            {
                if (CostoProduccion.DocumentoExiste(STU_SISTEMA.EMPRESAID, TxtNumSer.Text, TxtNumDoc.Text) == true)
                {
                    throw new Exception(" El numero de documento " + TxtNumSer.Text + "-" + TxtNumDoc.Text + " ya existe, ingrese otro ");
                }

                m_CostoProduccion.IsNew = true;
            }

            if (n_QueHace == 2)
            {
                m_CostoProduccion.IsOld = true;
            }

            m_CostoProduccion.Save();
        }

        void AsignarEntidad()
        {
            m_CostoProduccion.n_idemp = STU_SISTEMA.EMPRESAID;
            m_CostoProduccion.c_numser = TxtNumSer.Text;
            m_CostoProduccion.c_numdoc = TxtNumDoc.Text;
            m_CostoProduccion.n_idconfigval = Convert.ToInt32(CboConfiguracion.SelectedValue);
            m_CostoProduccion.n_anotra = STU_SISTEMA.ANOTRABAJO;
            m_CostoProduccion.n_idmes = Convert.ToInt32(CboMeses.SelectedValue.ToString());
            m_CostoProduccion.c_obs = TxtObs.Text; ;            
            m_CostoProduccion.n_idresp =  Convert.ToInt32(CboResponsable.SelectedValue);
        }

        private void ValidarCampos()
        {
            if (TxtNumSer.Text == "")
            {
                throw new Exception("¡ No ha especificado el numero de serie !");
            }

            if (TxtNumDoc.Text == "")
            {
                throw new Exception("¡ No ha especificado el numero de documento !");
            }

            if (Convert.ToInt32(CboConfiguracion.SelectedValue) == 0)
            {
                throw new Exception("¡ No ha especificado la configuración de valorización !");
            }

            if (Convert.ToInt32(CboResponsable.SelectedValue) == 0)
            {
                throw new Exception("¡ No ha especificado el reponsable !");
            }

            if (CostoProduccionDetBindingSource.Count == 0)
            {
                throw new Exception("¡ No ha especificado ningun item para este proceso!");
            }
        }

        private void FrmCostoProduccion_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (costoProduccionBindingSource.Count == 0)
                {
                    DialogResult Rpta = MessageBox.Show("No se han encontrado registros, ¿ Desea agregar uno ahora ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (DialogResult.Yes == Rpta)
                    {
                        Nuevo();
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
            try
            {
                Grabar();
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();

                DialogResult Rpta = MessageBox.Show("! El registro se grabó con éxito ¡ ¿Desea agregar otro registro? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

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
            catch (Exception ex)
            {
                MessageBox.Show("¡ No se pudo grabar el registro por el siguiente motivo ! " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToolCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Tab1_SelectedIndexChanging(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;

            if (n_QueHace != 3) { return; }

            if (tc.SelectedIndex == 1)
            {
                //int intIdRegistro = Convert.ToInt32(DgLista.Columns[9].CellValue(DgLista.Row).ToString());
                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro();
                    booAgregando = false;
                }
            }
        }

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            //int intIdRegistro = Convert.ToInt32(DgLista.Columns[9].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            VerRegistro();
            booAgregando = false;
        }

        private void CmdDelItem_Click(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count > 2)
            {
                FgItems.RemoveItem(FgItems.Row);
                FgItems.Focus();
            }
        }

        private void DgLista_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    DataTable dtResult = new DataTable();

            //    string c_CadFiltro = funDbGrid.DG_LeerCondicionesFiltro(DgLista);
            //    dtResult = funDbGrid.DG_Filtrar(dtCostoProduccion, c_CadFiltro, DgLista);
            //    DgLista.DataSource = dtResult;
            //    LblNumReg.Text = (dtResult.Rows.Count).ToString();
            //}
        }

        private void TxtNumSer_TextChanged(object sender, EventArgs e)
        {
        }

        private void TxtNumSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000" + TxtNumSer.Text;

                TxtNumSer.Text = strCad.Substring(strCad.Length - 4, 4);
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

        private void TxtNumDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000000000" + TxtNumDoc.Text;

                TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
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

        private void txtFchIng_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtFchDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtObs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtNumSer_Validated(object sender, EventArgs e)
        {
            if (TxtNumSer.Text == "") return;
            TxtNumDoc.Text = TipoDocumento.UltimoNumero(STU_SISTEMA.EMPRESAID, 95, TxtNumSer.Text);
        }

        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text == "") { return; }

            string strCad = "0000000000" + TxtNumDoc.Text;
            TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
        }

        private void ToolImprimir_Click(object sender, EventArgs e)
        {
        }

        private void ToolManFun_Click(object sender, EventArgs e)
        {            
        }

        private void CboResponsable_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }
            if (Convert.ToInt32(CboResponsable.SelectedValue) == 0) { return; }
            
            TxtNumSer.Text = "0001";
            TxtNumDoc.Text = TipoDocumento.UltimoNumero(STU_SISTEMA.EMPRESAID, 95, TxtNumSer.Text);
            CboConfiguracion.Focus();
            booAgregando = false;
        }

        private void BtnBuscarParte_Click(object sender, EventArgs e)
        {
            try
            {
                CargarCabecera();
                Cursor.Current = Cursors.WaitCursor;

                //CostoProduccionDetBindingSource.Clear();
                //costoProduccionCuesBindingSource.Clear();
                //costoProduccionDetInsBindingSource.Clear();
                //CostoProduccionDetModBindingSource.Clear();
                //costoProduccionDetCifBindingSource.Clear();

                costoProduccionDetInsBindingSource.DataSource = null;
                CostoProduccionDetModBindingSource.DataSource = null;
                costoProduccionDetCifBindingSource.DataSource = null;

                //Se buscan los partes del periodo
                m_CostoProduccion.ListarPartesdeProduccion(m_CostoProduccion.n_idemp
                    , m_CostoProduccion.n_anotra
                    , m_CostoProduccion.n_idmes);

                //Se distribuye Mod
                m_CostoProduccion.ProcesarMod();

                CostoProduccionDetBindingSource.DataSource = m_CostoProduccion.CostoProduccionDets;

                MessageBox.Show("Partes de producción listado correctamente"
                    , "Listar Partes"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrio un error al buscar partes de producción, error: {0}", ex.Message)
                    , "Buscar Partes"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void BtnProcesarModCif_Click(object sender, EventArgs e)
        {
            try
            {
                CargarCabecera();
                Cursor.Current = Cursors.WaitCursor;
                m_CostoProduccion.ProcesarCif();

                costoProduccionCuesBindingSource.DataSource = m_CostoProduccion.CostoProduccionCues;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrio un error al procesar MP, error: {0}", ex.Message)
                    , "Procesar MP"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void BtnProcesarMP_Click(object sender, EventArgs e)
        {
            try
            {
                CargarCabecera();
                Cursor.Current = Cursors.WaitCursor;
                m_CostoProduccion.ProcesarMp();

                if (m_CostoProduccion.CostoProduccionErrors.Count > 0)
                {
                    MessageBox.Show(string.Format("Se han producido errores al procesar MP, por favor revisar y corregir")
                        , "Procesar MP"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Error);

                    using (FrmCostoProduccionError xForm = new FrmCostoProduccionError(m_CostoProduccion.CostoProduccionErrors))
                    {
                        xForm.ShowDialog(this);
                    }
                }
                else
                {
                    MessageBox.Show("MP procesado correctamente"
                        , "Procesar MP"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrio un error al procesar MP, error: {0}", ex.Message)
                    , "Procesar MP"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void CargarCabecera()
        {
            m_CostoProduccion.n_idemp = STU_SISTEMA.EMPRESAID;
            m_CostoProduccion.n_anotra = STU_SISTEMA.ANOTRABAJO;
            m_CostoProduccion.n_idmes = Convert.ToInt32(CboMeses.SelectedValue.ToString());
            m_CostoProduccion.n_idconfigval = Convert.ToInt32(CboConfiguracion.SelectedValue.ToString());
        }

        private void FgItems_SelChange(object sender, EventArgs e)
        {
            if (booAgregando) return;
            CostoProduccionDet costoProduccionDet = (CostoProduccionDet)CostoProduccionDetBindingSource.Current;

            if (costoProduccionDet != null)
            {
                costoProduccionDet.IsValid = true;
                costoProduccionDetInsBindingSource.DataSource = costoProduccionDet.CostoProduccionDetInss;
                CostoProduccionDetModBindingSource.DataSource = costoProduccionDet.CostoProduccionDetMods;
                costoProduccionDetCifBindingSource.DataSource = costoProduccionDet.CostoProduccionDetCifs;
            }
        }

        private void BtnVerErroresMP_Click(object sender, EventArgs e)
        {
            if(m_CostoProduccion.CostoProduccionErrors == null)
            {
                m_CostoProduccion.CostoProduccionErrors 
                    = new SIAC_Datos.Classes.ObservableListSource<SIAC_DATOS.Classes.Contabilidad.CostoProduccionError>();
            }
            if (m_CostoProduccion.CostoProduccionErrors.Count > 0)
            {
                using (FrmCostoProduccionError xForm = new FrmCostoProduccionError(m_CostoProduccion.CostoProduccionErrors))
                {
                    xForm.ShowDialog(this);
                }
            }
            else
            {
                MessageBox.Show("No se encontraron errores en el proceso MP"
                    , "Ver errores MP"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Information);
            }
        }

        private void BtnVerDetalleCostoInsumo_Click(object sender, EventArgs e)
        {
            try
            {
                CostoProduccionDetIns costoProduccionDetIns = (CostoProduccionDetIns)costoProduccionDetInsBindingSource.Current;
                Movimiento movimiento = Movimiento.Fetch(costoProduccionDetIns.n_idmov);
                var costoProduccionInsumoDetalles = CostoProduccion.ObtieneCostoDetalleInsumo(m_CostoProduccion.n_idemp, 
                                                        costoProduccionDetIns.n_idite, 
                                                        costoProduccionDetIns.n_can, 
                                                        movimiento.d_fching);

                if (costoProduccionInsumoDetalles.Count > 0)
                {
                    using (FrmCostoProduccionInsumoDetalle xForm = new FrmCostoProduccionInsumoDetalle(costoProduccionInsumoDetalles))
                    {
                        xForm.ShowDialog(this);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron detalles en el insumo"
                        , "Costo Insumo Detalle"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrio un error al ver detalle, error: {0}", ex.Message)
                    , "Procesar MP"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
            }
        }
    }
}
