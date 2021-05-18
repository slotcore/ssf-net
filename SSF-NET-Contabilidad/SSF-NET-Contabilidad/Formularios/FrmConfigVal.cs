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
using SIAC_Negocio.Contabilidad;

namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmConfigVal : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_sys_formulario objForm = new CN_sys_formulario();

        // OBJETOS DE ACCESO A DATOS
        Funciones funFunciones = new Funciones();

        // ENTIDADES LOCALES
        ConfigVal m_ConfigVal = new ConfigVal();

        // DATATABLE LOCALES
        DataTable dtForm = new DataTable();

        // VARIABLES LOCALES
        // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        int n_QueHace = 3;
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;
        int n_idformulario = 102;
        
        public FrmConfigVal()
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

            var MetVals = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("CP", "Costo Promedio")
            };
            CboMetVal.DataSource = MetVals;
            CboMetVal.DisplayMember = "value";
            CboMetVal.ValueMember = "key";

            var TipDists = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("PT", "Productos Terminados")
            };
            CboTipDist.DataSource = TipDists;
            CboTipDist.DisplayMember = "value";
            CboTipDist.ValueMember = "key";

            var FactDists = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("CNT", "Cantidad")
            };
            CboFactDist.DataSource = FactDists;
            CboFactDist.DisplayMember = "value";
            CboFactDist.ValueMember = "key";
        }

        void ConfigurarFormulario()
        {
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";
            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
        }

        void DataTableCargar()
        {
            objTipDoc.mysConec = mysConec;

            // CARGAMOS LOS DATOS DEL FORMULARIO
            objForm.mysConec = mysConec;
            dtForm = objForm.TraerRegistro(n_idformulario);
        }

        void ListarItems()
        {
            configValBindingSource.DataSource = ConfigVal.FetchList(STU_SISTEMA.EMPRESAID);
            LblNumReg.Text = (configValBindingSource.Count).ToString();
        }

        void VerRegistro()
        {
            try
            {
                int n_id = ((ConfigVal)configValBindingSource.Current).n_id;
                m_ConfigVal = ConfigVal.Fetch(n_id);

                CboMetVal.SelectedValue = m_ConfigVal.c_metval;
                CboTipDist.SelectedValue = m_ConfigVal.c_tipdist;
                CboFactDist.SelectedValue = m_ConfigVal.c_factdist;
                TxtDescripcion.Text = m_ConfigVal.c_des;
                TxtObs.Text = m_ConfigVal.c_obs;

                configValCuesBindingSource.DataSource = m_ConfigVal.ConfigValCues;
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
            TxtDescripcion.Text = "";
            TxtObs.Text = "";            
            CboMetVal.SelectedValue = 0;
            CboTipDist.SelectedValue = 0;
            CboFactDist.SelectedValue = 0;
            m_ConfigVal = new ConfigVal();
            configValCuesBindingSource.DataSource = m_ConfigVal.ConfigValCues;
        }

        void Bloquea()
        {
            TxtDescripcion.Enabled = !TxtDescripcion.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
            CboFactDist.Enabled = !CboFactDist.Enabled;
            CboTipDist.Enabled = !CboTipDist.Enabled;
            CboMetVal.Enabled = !CboMetVal.Enabled;

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
            CboMetVal.Focus();
            booAgregando = false;
        }

        private void EliminarRegistro()
        {
            try
            {
                int intIdRegistro = ((ConfigVal)configValBindingSource.Current).n_id;

                m_ConfigVal = ConfigVal.Fetch(intIdRegistro);

                DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (DialogResult.Yes == Rpta)
                {
                    //objMovimientos.AccConec = AccConec;
                    m_ConfigVal.Delete();
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

            //if (n_QueHace == 1)
            //{
            //    m_ConfigVal.IsNew = true;
            //}

            //if (n_QueHace == 2)
            //{
            //    m_ConfigVal.IsOld = true;
            //}
            m_ConfigVal.Save();
        }

        void AsignarEntidad()
        {
            m_ConfigVal.n_idemp = STU_SISTEMA.EMPRESAID;
            m_ConfigVal.c_des = TxtDescripcion.Text;
            m_ConfigVal.c_obs = TxtObs.Text;
            m_ConfigVal.c_factdist = CboFactDist.SelectedValue.ToString();
            m_ConfigVal.c_metval = CboMetVal.SelectedValue.ToString();
            m_ConfigVal.c_tipdist = CboTipDist.SelectedValue.ToString();
        }

        private void ValidarCampos()
        {
            if (TxtDescripcion.Text == "")
            {
                throw new Exception("¡ No ha especificado la descripción !");
            }

            if (m_ConfigVal.ConfigValCues.Count == 0)
            {
                throw new Exception("¡ No ha especificado ninguna cuenta para este proceso!");
            }
        }

        private void FrmCostoProduccion_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (configValBindingSource.Count == 0)
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

                DialogResult Rpta = MessageBox.Show("! El registro se agregó con éxito ¡ ¿Desea agregar otro registro? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

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
            objTipDoc = null;
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

        private void BtnAgregarCuenta_Click(object sender, EventArgs e)
        {
            CN_con_pc objPlaCue = new CN_con_pc();
            DataTable dtPlaCue = new DataTable();

            objPlaCue.mysConec = mysConec;
            objPlaCue.Listar(STU_SISTEMA.EMPRESAID);
            dtPlaCue = objPlaCue.dtLista;

            DataTable dtResult = objPlaCue.BuscarCuenta(dtPlaCue);
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    int idCuenta = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                    ConfigValCue configValCue = m_ConfigVal.ConfigValCues
                        .Where(o => o.n_idcue == idCuenta).FirstOrDefault();

                    if (configValCue != null)
                    {
                        throw new Exception("La cuenta seleccionada ya se encuentra agregada");
                    }
                    else
                    {
                        m_ConfigVal.ConfigValCues.Add(new ConfigValCue
                        {
                            n_idcue = idCuenta,
                            c_descue = dtResult.Rows[0]["c_des"].ToString()
                        });
                    }
                }
            }
        }

        private void BtnEliminarCuenta_Click(object sender, EventArgs e)
        {
            var configValCue = (ConfigValCue)configValCuesBindingSource.Current;
            m_ConfigVal.ConfigValCues.RemoveItem(configValCue);
        }
    }
}
