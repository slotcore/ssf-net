using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Produccion;
using SIAC_Entidades.Sistema;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Produccion;
using SIAC_Negocio.Sunat;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using System.Data.OleDb;
using System.Configuration;
using SIAC_Entidades.Logistica;
using SIAC_Negocio.Logistica;
using SIAC_Entidades.Contabilidad;
using SIAC_DATOS.Models.Contabilidad;

namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmCostoProduccion : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public OleDbConnection AccConec = new OleDbConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_sun_tipexi objTipoExi = new CN_sun_tipexi();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_alm_inventariounimed ObjAlmUniMed = new CN_alm_inventariounimed();
        CN_alm_almacenesdoc ObjAlmDoc = new CN_alm_almacenesdoc();
        CN_alm_inventariolotes ObjLotes = new CN_alm_inventariolotes();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_empresa funsys = new CN_sys_empresa();
        //CN_con_costoproduccion objCostoProduccion = new CN_con_costoproduccion();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_sun_unimed objUniMed = new CN_sun_unimed();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_alm_inventariolotes objlotes = new CN_alm_inventariolotes();
        CN_ACC_pro_solicitudmat objSolMar = new CN_ACC_pro_solicitudmat();
        CN_ACC_pro_producciondet objProPro = new CN_ACC_pro_producciondet();
        CN_sys_setup o_set = new CN_sys_setup();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        CostoProduccion m_CostoProduccion = new CostoProduccion();
        BE_SYS_EMPRESA entEmp = new BE_SYS_EMPRESA();

        // DATATABLE LOCALES
        DataTable dtTipoExis = new DataTable();
        //DataTable dtCostoProduccion = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtMoviDetalle = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtPresentaItem = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtDocAlm = new DataTable();
        DataTable dtLotes = new DataTable();
        DataTable DtSolMat = new DataTable();
        DataTable DtDetalleTMP = new DataTable();                                       // EN ESTE DATA TABLE SE ALMACENA EL DETALLE DE LOS DOCUMENTOS JALADOS
        DataTable dtsetup = new DataTable();

        // VARIABLES LOCALES
        int N_IDALMACEN = 0;
        int N_IDLOCAL = 0;
        int N_INGTRAZABALIDAD = 0;                                                       // LE INDICAMOS AL MODULO QUE TIENE QUE PEDIR DATOS DE TRAZABILIDAD
        int N_INGPRECIO = 0;
        int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        //string[,] arrCabeceraDg1 = new string[10, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        //string[,] arrCabeceraFlex1 = new string[11, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;
        int n_ItemProducido;                                                            // ESTA VARIABLE ALMACENARA EL ID DEL ITEM PRODUCIDO CUANDO SE SELECCIONE UNA ORDEN DE PRODUCCION
        
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
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");

            CboResponsable.DataSource = PersonalContabilidad.FetchList(STU_SISTEMA.EMPRESAID, 2);
            CboResponsable.DisplayMember = "c_destra";
            CboResponsable.ValueMember = "n_idtra";

            CboConfiguracion.DataSource = ConfiguracionValorizacion.FetchList(STU_SISTEMA.EMPRESAID);
            CboConfiguracion.DisplayMember = "c_des";
            CboConfiguracion.ValueMember = "n_id";
        }

        void ConfigurarFormulario()
        {
            Tab1.SelectedIndex = 0;
            string c_nomarc = ConfigurationManager.AppSettings["PathIniFile"];
            N_IDALMACEN = Convert.ToInt32(funDatos.IniLeerSeccion(c_nomarc, "SISTEMA", "ALMACEN").ToString());

            if (dtsetup.Rows.Count != 0)
            {
                if (Convert.ToInt32(dtsetup.Rows[0]["n_almtrazabilidad"]) == 1)
                {
                    N_INGTRAZABALIDAD = 1;
                }
                if (Convert.ToInt32(dtsetup.Rows[0]["n_guipedpre"]) == 1)
                {
                    N_INGPRECIO = 1;
                }
            }

            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }

        void DataTableCargar()
        {            
            o_set.mysConec = mysConec;
            dtsetup = o_set.TraerRegistro();

            objTipDoc.mysConec = mysConec;

            objTipoExi.mysConec = mysConec;
            dtTipoExis = objTipoExi.Listar();

            objItems.mysConec = mysConec;
            dtItems = objItems.ListarTabla(STU_SISTEMA.EMPRESAID);

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();
                        
            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(99);

            ObjAlmDoc.mysConec = mysConec;
            dtDocAlm = ObjAlmDoc.ListarEmpAlm(STU_SISTEMA.EMPRESAID, 2);               // 1 FILTRAREMOS SOLO LOS ALMACENES QUE PERTENESCAN A LA EMPRESA Y QUE GENEREN MOVIMIENTO DE INGRESO A ALMACEN 

            objlotes.mysConec = mysConec;
            dtLotes = objlotes.TraerLotesConSaldo(STU_SISTEMA.EMPRESAID);
            
            funsys.mysConec = mysConec;
            funsys.TraerRegistro(STU_SISTEMA.EMPRESAID);
            entEmp = funsys.e_Empresa;
        }

        void ListarItems()
        {
            costoProduccionBindingSource.DataSource = CostoProduccion.FetchList(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO);
            LblNumReg.Text = (costoProduccionBindingSource.Count).ToString();
        }

        void VerRegistro()
        {
            m_CostoProduccion = (CostoProduccion)costoProduccionBindingSource.Current;
            //m_CostoProduccion = CostoProduccion.Fetch(n_IdRegistro);

            TxtNumDoc.Text = m_CostoProduccion.c_numdoc;
            TxtNumSer.Text = m_CostoProduccion.c_numser;            
            CboConfiguracion.SelectedValue = m_CostoProduccion.n_idconfigval;
            TxtObs.Text = m_CostoProduccion.c_obs;            
            CboResponsable.SelectedValue = m_CostoProduccion.n_idresp;

            CostoProduccionDetBindingSource.DataSource = m_CostoProduccion.CostoProduccionDets;
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
            //FgItems.Rows.Count = 2;
            booAgregando = false;
            FgItems.Cols[2].ComboList = "...";
            CboConfiguracion.SelectedValue = Convert.ToInt32(N_IDALMACEN);
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
                if (CostoProduccion.DocumentoExiste(STU_SISTEMA.EMPRESAID, TxtNumSer.Text, TxtNumDoc.Text) == true)
                {
                    MessageBox.Show(" El numero de documento " + TxtNumSer.Text + "-" + TxtNumDoc.Text + " ya existe, ingrese otro ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return booResultado;
                }
                m_CostoProduccion.IsNew = true;
            }

            if (n_QueHace == 2)
            {
                m_CostoProduccion.IsOld = true;
            }
            m_CostoProduccion.Save();
            return booResultado;
        }
        void AsignarEntidad()
        {
            m_CostoProduccion.n_idemp = STU_SISTEMA.EMPRESAID;
            m_CostoProduccion.c_numser = TxtNumSer.Text;
            m_CostoProduccion.c_numdoc = TxtNumDoc.Text;
            m_CostoProduccion.n_idconfigval = Convert.ToInt32(CboConfiguracion.SelectedValue);
            m_CostoProduccion.n_anotra = STU_SISTEMA.ANOTRABAJO;
            m_CostoProduccion.n_idmes = STU_SISTEMA.MESTRABAJO;
            m_CostoProduccion.c_obs = TxtObs.Text; ;            
            m_CostoProduccion.n_idresp =  Convert.ToInt32(CboResponsable.SelectedValue);
        }

        bool CamposOK()
        {
            bool booEstado = true;

            if (TxtNumSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumSer.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumDoc.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt32(CboConfiguracion.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la configuración de valorización !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboConfiguracion.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt32(CboResponsable.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el reponsable !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboConfiguracion.Focus();
                booEstado = false;
                return booEstado;
            }

            if (FgItems.Rows.Count != 2)
            {
                int intFila;
                // VERIFICAMOS QUE LOS DATOS DE LAS PRESENTACIONES ESTEN COMPLETAS
                for (intFila = 2; intFila <= FgItems.Rows.Count - 1; intFila++)
                {
                    if (funFunciones.NulosC(FgItems.GetData(intFila, 1)) != "")
                    {
                        if (funFunciones.NulosC(FgItems.GetData(intFila, 2)) == "")
                        {
                            MessageBox.Show("¡ No ha especificado la descripcion del item en la fila " + (FgItems.Row - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
                        }
                        if (funFunciones.NulosC(FgItems.GetData(intFila, 3)) == "")
                        {
                            MessageBox.Show("¡ No ha especificado la presentacion del item en la fila " + (FgItems.Row - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
                        }
                        if (N_INGTRAZABALIDAD == 1)
                        { 
                            if (funFunciones.NulosC(FgItems.GetData(intFila, 4)) == "")
                            {
                                MessageBox.Show("¡ No ha especificado el numero de lote, en la fila " + (FgItems.Row - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                booEstado = false;
                                return booEstado;
                            }
                        }
                        if (funFunciones.NulosC(FgItems.GetData(intFila, 6)) == "")
                        {
                            MessageBox.Show("¡ No ha especificado la cantidad del item que ingresara en la fila " + (FgItems.Row - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
                        }
                        if (funFunciones.NulosC(FgItems.GetData(intFila, 9)) == "")
                        {
                            MessageBox.Show("¡ No ha especificado la hora de salida del item en la fila " + (FgItems.Row - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("¡ No ha especificado ningun item para este proceso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            return booEstado;
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
            if (Grabar() == true)
            {
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
            objTipoExi = null;
            dtTipoExis = null;

            objTipDoc = null;

            objTipoExi = null;
            dtTipoExis = null;

            objFormVis = null;
            objFormVis = null;

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
            if (TxtNumSer.Text == "") { return; }

            string c_numdoc = "";
            c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 95, TxtNumSer.Text);
            TxtNumDoc.Text = c_numdoc;
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
            TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 95, TxtNumSer.Text);
            CboConfiguracion.Focus();
            booAgregando = false;
        }

        private void BtnBuscarParte_Click(object sender, EventArgs e)
        {
            try
            {
                CargarCabecera();
                Cursor.Current = Cursors.WaitCursor;
                m_CostoProduccion.ListarPartesdeProduccion(m_CostoProduccion.n_idemp
                    , m_CostoProduccion.n_anotra
                    , m_CostoProduccion.n_idmes);

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

        private void BtnProcesarMP_Click(object sender, EventArgs e)
        {
            try
            {
                CargarCabecera();
                Cursor.Current = Cursors.WaitCursor;
                m_CostoProduccion.ProcesarMp(STU_SISTEMA.EMPRESAID
                    , m_CostoProduccion.d_fchini
                    , m_CostoProduccion.d_fchfin);

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
        }


        //private void ProcesaInsumos(Common.Models.Contabilidad.CostoProduccion costoProduccion, DateTime fechaInicio, DateTime fechaFin)
        //{
        //    using (var context = new ApplicationDbContext())
        //    {
        //        var tipoMercaderia = context.TipoMercaderias
        //            .Where(o => o.Codigo == "INS")
        //            .FirstOrDefault();
        //        var mercaderias = (from k in context.KardexMovimientos
        //                           where k.Mercaderia.TipoMercaderiaId == tipoMercaderia.TipoMercaderiaId
        //                               && k.Fecha >= fechaInicio && k.Fecha <= fechaFin
        //                           group k by k.MercaderiaId into g
        //                           select g.FirstOrDefault().Mercaderia).ToList();

        //        foreach (var mercaderia in mercaderias)
        //        {
        //            CosteaMercaderia(costoProduccion, mercaderia.MercaderiaId, fechaInicio, fechaFin);
        //        }
        //    }
        //}

        private void ProcesaIntermedios(DateTime fechaInicio, DateTime fechaFin)
        {
            throw new NotImplementedException();
        }

        private void ProcesaTerminados(DateTime fechaInicio, DateTime fechaFin)
        {
            throw new NotImplementedException();
        }

        private void FgItems_SelChange(object sender, EventArgs e)
        {
            if (booAgregando) return;
            CostoProduccionDet costoProduccionDet = (CostoProduccionDet)CostoProduccionDetBindingSource.Current;
            if (costoProduccionDet != null)
            {
                CostoProduccionDetInsBindingSource.DataSource = costoProduccionDet.CostoProduccionDetInss;
                CostoProduccionDetModBindingSource.DataSource = costoProduccionDet.CostoProduccionDetMods;
            }
        }
    }
}
