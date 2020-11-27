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
using System.Collections.Generic;
using SIAC_DATOS.Models.Almacen;
using SIAC_DATOS.Models.Sunat;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using SIAC_DATOS.Models.Sistema;

namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmInventarioIni : Form
    {
        // VARIABLES PUBLICAS
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // ENTIDADES LOCALES
        InventarioInicial m_InventarioInicial = new InventarioInicial();

        // VARIABLES LOCALES
        // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        int n_QueHace = 3;
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;
        string strNumerovalidos2 = "EFGR1234567890" + (char)8;
        int n_idformulario = 101;
        
        public FrmInventarioIni()
        {
            InitializeComponent();
        }

        private void FrmConfigVal_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }

        void CargarCombos()
        {
            DataTableCargar();

            CboAlmacen.DataSource = Almacen.FetchList();
            CboAlmacen.DisplayMember = "c_des";
            CboAlmacen.ValueMember = "n_id";

            CboMoneda.DataSource = TipoMoneda.FetchList();
            CboMoneda.DisplayMember = "c_des";
            CboMoneda.ValueMember = "n_id";
        }

        void ConfigurarFormulario()
        {
            Formulario formulario = Formulario.Fetch(n_idformulario);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";
            this.Text = formulario.c_titfor;
        }

        void DataTableCargar()
        {
        }

        void ListarItems()
        {
            inventarioInicialBindingSource.DataSource = InventarioInicial.FetchList(STU_SISTEMA.EMPRESAID);
            LblNumReg.Text = (inventarioInicialBindingSource.Count).ToString();
        }

        void VerRegistro()
        {
            try
            {
                m_InventarioInicial = (InventarioInicial)inventarioInicialBindingSource.Current;

                CboAlmacen.SelectedValue = m_InventarioInicial.n_idalm;
                CboMoneda.SelectedValue = m_InventarioInicial.n_idmon;
                TxtDescripcion.Text = m_InventarioInicial.c_des;
                TxtNumDoc.Text = m_InventarioInicial.c_numdoc;
                TxtNumSer.Text = m_InventarioInicial.c_numser;
                DtpFchVigencia.Value = m_InventarioInicial.d_fchvig;

                inventarioInicialDetBindingSource.DataSource = m_InventarioInicial.InventarioInicialDets;
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
            CboAlmacen.SelectedValue = 0;
            CboMoneda.SelectedValue = 0;
            TxtDescripcion.Text = "";
            TxtNumDoc.Text = "";
            TxtNumSer.Text = "";
            DtpFchVigencia.Text = "";
            m_InventarioInicial = new InventarioInicial();
            inventarioInicialDetBindingSource.DataSource = m_InventarioInicial.InventarioInicialDets;
        }

        void Bloquea()
        {
            CboMoneda.Enabled = !CboMoneda.Enabled;
            CboAlmacen.Enabled = !CboAlmacen.Enabled;
            TxtDescripcion.Enabled = !TxtDescripcion.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            DtpFchVigencia.Enabled = !DtpFchVigencia.Enabled;

            if (BtnCargarExcel.Enabled == ComponentFactory.Krypton.Toolkit.ButtonEnabled.True)
                BtnCargarExcel.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            else
                BtnCargarExcel.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;

            if (BtnAgregarItem.Enabled == ComponentFactory.Krypton.Toolkit.ButtonEnabled.True)
                BtnAgregarItem.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            else
                BtnAgregarItem.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;

            if (BtnEliminarItem.Enabled == ComponentFactory.Krypton.Toolkit.ButtonEnabled.True)
                BtnEliminarItem.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            else
                BtnEliminarItem.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;
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
            CboAlmacen.Focus();
            booAgregando = false;
        }

        private void EliminarRegistro()
        {
            try
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns[9].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

                m_InventarioInicial = InventarioInicial.Fetch(intIdRegistro);

                DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (DialogResult.Yes == Rpta)
                {
                    //objMovimientos.AccConec = AccConec;
                    m_InventarioInicial.Delete();
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
                m_InventarioInicial.IsNew = true;
                m_InventarioInicial.IsOld = true;
            }

            if (n_QueHace == 2)
            {
                m_InventarioInicial.IsNew = false;
                m_InventarioInicial.IsOld = true;
            }
            m_InventarioInicial.Save();
        }

        void AsignarEntidad()
        {
            m_InventarioInicial.n_idemp = STU_SISTEMA.EMPRESAID;
            m_InventarioInicial.c_des = TxtDescripcion.Text;
            m_InventarioInicial.c_numdoc = TxtNumDoc.Text;
            m_InventarioInicial.c_numser = TxtNumSer.Text;
            m_InventarioInicial.d_fchvig = DtpFchVigencia.Value;
            m_InventarioInicial.n_idmon = Convert.ToInt32(CboMoneda.SelectedValue);
            m_InventarioInicial.n_idalm = Convert.ToInt32(CboAlmacen.SelectedValue);
        }

        private void ValidarCampos()
        {
            bool booEstado = true;

            if (Convert.ToInt32(CboMoneda.SelectedValue) == 0)
            {
                throw new Exception("¡ No ha especificado la moneda !");
            }

            if (Convert.ToInt32(CboAlmacen.SelectedValue) == 0)
            {
                throw new Exception("¡ No ha especificado el almacén !");
            }

            if (TxtDescripcion.Text == "")
            {
                throw new Exception("¡ No ha especificado la descripción !");
            }

            if (TxtNumSer.Text == "")
            {
                throw new Exception("¡ No ha especificado el numero de serie !");
            }

            if (TxtNumDoc.Text == "")
            {
                throw new Exception("¡ No ha especificado el numero de documento !");
            }

            if (inventarioInicialDetBindingSource.Count == 0)
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

                if (inventarioInicialBindingSource.Count == 0)
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

        private void TxtObs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void ToolImprimir_Click(object sender, EventArgs e)
        {
        }

        private void ToolManFun_Click(object sender, EventArgs e)
        {            
        }

        private void BtnCargarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var filePath = string.Empty;
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Archivos Excel (*.xlsx)|*.xlsx|Archivos Excel 2007 (*.xls)|*.xls";
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {

                        int contador = 0;
                        try
                        {
                            ISheet sheet;
                            filePath = openFileDialog.FileName;
                            var fileExt = Path.GetExtension(filePath);

                            if (fileExt.ToLower() == ".xls")
                            {
                                HSSFWorkbook hssfwb;
                                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                                {
                                    hssfwb = new HSSFWorkbook(file);
                                }

                                sheet = hssfwb.GetSheetAt(0);
                            }
                            else //.xlsx extension
                            {
                                XSSFWorkbook hssfwb;
                                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                                {
                                    hssfwb = new XSSFWorkbook(file);
                                }

                                sheet = hssfwb.GetSheetAt(0);
                            }

                            for (int m_fila = 1; m_fila <= sheet.LastRowNum; m_fila++)
                            {
                                if (sheet.GetRow(m_fila) != null)
                                {
                                    // Se busca el item
                                    var fila = sheet.GetRow(m_fila);
                                    var descripcionItem = fila.GetCell(1).ToString().Trim();
                                    var unidadItem = fila.GetCell(2).ToString().Trim();
                                    Inventario inventario = Inventario.TraerPorDescripcion(descripcionItem);

                                    if (inventario != null)
                                    {
                                        InventarioUnimed inventarioUnimed = inventario.InventarioUnimeds
                                            .Where(o => o.c_abrpre == unidadItem).FirstOrDefault();
                                        if (inventarioUnimed != null)
                                        {
                                            double cantidad = Convert.ToDouble(fila.GetCell(3).ToString());
                                            double costoUnit = Convert.ToDouble(fila.GetCell(4).ToString());

                                            var inventarioInicialDet = m_InventarioInicial.InventarioInicialDets
                                                .Where(o => o.n_idite == inventario.n_id).FirstOrDefault();

                                            if (inventarioInicialDet != null)
                                            {
                                                inventarioInicialDet.n_idunimed = inventarioUnimed.n_id;
                                                inventarioInicialDet.c_desunimed = inventarioUnimed.c_abrpre;
                                                inventarioInicialDet.n_can = cantidad;
                                                inventarioInicialDet.n_costounit = costoUnit;
                                            }
                                            else
                                            {
                                                m_InventarioInicial.InventarioInicialDets.Add(new InventarioInicialDet
                                                {
                                                    n_idite = inventario.n_id,
                                                    n_idunimed = inventarioUnimed.n_idunimedbas,
                                                    c_codite = inventario.c_codpro,
                                                    c_desite = inventario.c_despro,
                                                    c_desunimed = inventarioUnimed.c_abrpre,
                                                    n_can = cantidad,
                                                    n_costounit = costoUnit
                                                });
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Se generó un error en la fila: {0}, detalle del error: {1}", contador, ex.Message));
                        }
                    }
                }

                MessageBox.Show("Excel cargado correctamente!!", "Cargar Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cargar Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtNumSer_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtNumSer.Text)) return;

            string strCad = "0000" + TxtNumSer.Text;
            TxtNumSer.Text = strCad.Substring(strCad.Length - 4, 4);

            TxtNumDoc.Text = TipoDocumento.UltimoNumero(STU_SISTEMA.EMPRESAID, 97, TxtNumSer.Text);
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
                if (!strNumerovalidos2.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text == "") { return; }

            string strCad = "0000000000" + TxtNumDoc.Text;
            TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
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
    }
}
