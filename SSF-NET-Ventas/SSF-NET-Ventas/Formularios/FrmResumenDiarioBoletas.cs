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
using SIAC_Entidades.Sistema;

namespace SSF_NET_Ventas.Formularios
{
    public partial class FrmResumenDiarioBoletas : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_TipoReg = 0;                                               // INDICA EL TIPO DE REGISTRO QUE SE MOSTRARA 1 = VENTAS ;  2 = COMPRAS
        // OBJETOS LOCALES
        CN_vta_boletaresumen objRegistros = new CN_vta_boletaresumen();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_clipro objCliPro = new CN_mae_clipro();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_empresalocal o_emploc = new CN_sys_empresalocal();
        CN_sun_tipdoccom o_doc = new CN_sun_tipdoccom();
        CN_mae_meses objMeses = new CN_mae_meses();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES

        CN_sys_empresa objEmp = new CN_sys_empresa();

        BE_SYS_EMPRESA entEmpresa = new BE_SYS_EMPRESA();
        BE_VTA_BOLETARESUMEN e_boleta = new BE_VTA_BOLETARESUMEN();
        List<BE_VTA_BOLETARESUMENDET> l_boletadet = new List<BE_VTA_BOLETARESUMENDET>();

        string[,] arrCabeceraFlex1 = new string[15, 5];
        string[,] arrCabeceraDg1 = new string[7, 4];
        bool booAgregando = false;
        bool booSeEjecuto = false;
        int n_QueHace = 3;
        // DATATABLE LOCALES
        DataTable dtRegistros = new DataTable();
        DataTable dtCliPro = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtDoc = new DataTable();
        DataTable dtLocal = new DataTable();
        DataTable dtMeses = new DataTable();
        public FrmResumenDiarioBoletas()
        {
            InitializeComponent();
        }
        void ConfigurarFormulario()
        {
            this.Width = 975;
            this.Height = 563;

            Tab1.Left = 1;
            Tab1.Top = 39;
            this.Text = "VENTAS - RESUMEN DE BOLETAS ELECTRONICAS";
            Tab_Dimensionar(Tab1, this.Height - 81, this.Width - 16);

            arrCabeceraFlex1[0, 0] = "Tip. Doc.";
            arrCabeceraFlex1[0, 1] = "35";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_abr";

            arrCabeceraFlex1[1, 0] = "Fch. Emi.";
            arrCabeceraFlex1[1, 1] = "70";
            arrCabeceraFlex1[1, 2] = "F";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_campo1";

            arrCabeceraFlex1[2, 0] = "Nº Documento";
            arrCabeceraFlex1[2, 1] = "100";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_campo4";

            arrCabeceraFlex1[3, 0] = "Doc. Ide. Cli";
            arrCabeceraFlex1[3, 1] = "100";
            arrCabeceraFlex1[3, 2] = "C";
            arrCabeceraFlex1[3, 3] = "";
            arrCabeceraFlex1[3, 4] = "c_campo6";

            arrCabeceraFlex1[4, 0] = "Cliente";
            arrCabeceraFlex1[4, 1] = "200";
            arrCabeceraFlex1[4, 2] = "C";
            arrCabeceraFlex1[4, 3] = "";
            arrCabeceraFlex1[4, 4] = "c_nombre";

            arrCabeceraFlex1[5, 0] = "M.";
            arrCabeceraFlex1[5, 1] = "30";
            arrCabeceraFlex1[5, 2] = "C";
            arrCabeceraFlex1[5, 3] = "";
            arrCabeceraFlex1[5, 4] = "c_campo7";

            arrCabeceraFlex1[6, 0] = "T. C.";
            arrCabeceraFlex1[6, 1] = "40";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.000";
            arrCabeceraFlex1[6, 4] = "n_tc";

            arrCabeceraFlex1[7, 0] = "Imp. Bruto";
            arrCabeceraFlex1[7, 1] = "60";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "c_campo8";

            arrCabeceraFlex1[8, 0] = "I.G.V.";
            arrCabeceraFlex1[8, 1] = "60";
            arrCabeceraFlex1[8, 2] = "D";
            arrCabeceraFlex1[8, 3] = "0.00";
            arrCabeceraFlex1[8, 4] = "c_campo14";

            arrCabeceraFlex1[9, 0] = "Total";
            arrCabeceraFlex1[9, 1] = "60";
            arrCabeceraFlex1[9, 2] = "D";
            arrCabeceraFlex1[9, 3] = "0.00";
            arrCabeceraFlex1[9, 4] = "c_campo16";

            arrCabeceraFlex1[10, 0] = "Cajero";
            arrCabeceraFlex1[10, 1] = "150";
            arrCabeceraFlex1[10, 2] = "C";
            arrCabeceraFlex1[10, 3] = "";
            arrCabeceraFlex1[10, 4] = "c_cajnom";

            arrCabeceraFlex1[11, 0] = "Dato 1";
            arrCabeceraFlex1[11, 1] = "60";
            arrCabeceraFlex1[11, 2] = "D";
            arrCabeceraFlex1[11, 3] = "";
            arrCabeceraFlex1[11, 4] = "n_newimpbru";

            arrCabeceraFlex1[12, 0] = "Dato 2";
            arrCabeceraFlex1[12, 1] = "60";
            arrCabeceraFlex1[12, 2] = "D";
            arrCabeceraFlex1[12, 3] = "";
            arrCabeceraFlex1[12, 4] = "n_newimpigv";

            arrCabeceraFlex1[13, 0] = "Dato 3";
            arrCabeceraFlex1[13, 1] = "60";
            arrCabeceraFlex1[13, 2] = "D";
            arrCabeceraFlex1[13, 3] = "";
            arrCabeceraFlex1[13, 4] = "n_newimptot";

            arrCabeceraFlex1[14, 0] = "Id Venta";
            arrCabeceraFlex1[14, 1] = "0";
            arrCabeceraFlex1[14, 2] = "N";
            arrCabeceraFlex1[14, 3] = "";
            arrCabeceraFlex1[14, 4] = "n_id";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtRegistros, 2, false);
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
            Tab1.SelectedIndex = 0;
        }
        private void CmdCargar_Click(object sender, EventArgs e)
        {
            
            if (Convert.ToInt16(CboDoc.SelectedValue) == 0) 
            {
                MessageBox.Show("¡ El ha especificado el tipo de documento a exportar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return; 
            }

            DataTable dtresul = new DataTable();
            CN_vta_ventas o_vetas = new CN_vta_ventas();
            o_vetas.mysConec = mysConec;
            o_vetas.Consulta25(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboDoc.SelectedValue), TxtFchEmiDoc.Text);
            dtresul = o_vetas.dtLista1;
            LblNumDoc.Text = dtresul.Rows.Count.ToString();
            double n_numpag = Math.Round((Convert.ToDouble(dtresul.Rows.Count) / 100),0);

            if ((((Convert.ToDouble(dtresul.Rows.Count) / 100)) - n_numpag) != 0) { n_numpag = n_numpag + 1; }
            TxtNumArchGen.Text = n_numpag.ToString("0");
            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtresul, 2, true);
        }

        private void FrmResumenDiarioBoletas_Load(object sender, EventArgs e)
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

            funDatos.ComboBoxCargarDataTable(CboDoc, dtDoc, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            dtRegistros = objRegistros.dtLista;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(95, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(95);

            o_emploc.mysConec = mysConec;
            dtLocal = o_emploc.Listar(STU_SISTEMA.EMPRESAID, 0);

            o_doc.mysConec = mysConec;
            dtDoc = o_doc.Listar();

            objEmp.mysConec = mysConec;
            objEmp.TraerRegistro(STU_SISTEMA.EMPRESAID);
            entEmpresa = objEmp.e_Empresa;

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();
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
                objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
                dtRegistros = objRegistros.dtLista;
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
        void Nuevo()
        {
            n_QueHace = 1;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            Tab1.SelectedIndex = 1;
            //funDatos.ComboBoxCargarDataTable(CboDoc, dtLocal, "n_id", "c_des");
            CboDoc.SelectedValue = 4;
            TxtFchDec.Focus();
        }
        void Blanquea()
        {
            CboDoc.SelectedValue = 0;
            FgItems.Rows.Count = 2;
        }
        void Bloquea()
        {
            // CboDoc.Enabled = !CboDoc.Enabled;
            TxtFchDec.Enabled = !TxtFchDec.Enabled;
            TxtFchEmiDoc.Enabled = !TxtFchEmiDoc.Enabled;

            CmdCargar.Enabled = !CmdCargar.Enabled;
            CmdDelDoc.Enabled = !CmdDelDoc.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir.Enabled = !ToolImprimir.Enabled;
            ToolExportar.Enabled = !ToolExportar.Enabled;
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
            CboDoc.Focus();
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
                    objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
                    dtRegistros = objRegistros.dtLista;
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
                booResultado = objRegistros.Insertar(e_boleta, l_boletadet);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(e_boleta, l_boletadet);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            

            e_boleta.n_idemp = STU_SISTEMA.EMPRESAID;
            if (n_QueHace == 1) { e_boleta.n_id = 0; }
            if (n_QueHace == 2) {
                int n_idreg = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
                e_boleta.n_id = n_idreg; 
            }

            e_boleta.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_boleta.n_mes = STU_SISTEMA.MESTRABAJO;
            e_boleta.d_fchdoc = Convert.ToDateTime(TxtFchDec.Text);
            e_boleta.d_fchven = Convert.ToDateTime(TxtFchEmiDoc.Text);
            e_boleta.n_idtipdoc = Convert.ToInt16(CboDoc.SelectedValue);
            e_boleta.n_numdoc = Convert.ToInt16( LblNumDoc.Text);
            e_boleta.n_importe = 0;
            e_boleta.n_numarc = Convert.ToInt16(TxtNumArchGen.Text);
            
            int n_row=0;
            int n_iddoc = 0;
            double n_impbru = 0;
            double n_impigv = 0;
            double n_imptot = 0;
            l_boletadet.Clear();

            for(n_row=2; n_row <= FgItems.Rows.Count - 1; n_row++)
            {
                BE_VTA_BOLETARESUMENDET e_boletadet = new BE_VTA_BOLETARESUMENDET();
                n_iddoc = Convert.ToInt32(FgItems.GetData(n_row,15));
                n_impbru = Convert.ToDouble(FgItems.GetData(n_row, 12));
                n_impigv = Convert.ToDouble(FgItems.GetData(n_row, 13));
                n_imptot = Convert.ToDouble(FgItems.GetData(n_row, 14));

                e_boletadet.n_idres = 0;
                e_boletadet.n_iddoc = n_iddoc;
                e_boletadet.n_impbru = n_impbru;
                e_boletadet.n_impigv = n_impigv;
                e_boletadet.n_imptot = n_imptot;
                l_boletadet.Add(e_boletadet);
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (FgItems.Rows.Count == 2)
            {
                MessageBox.Show("¡ No ha especificado los documentos que se enviaran a sunat !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CmdCargar.Focus();
                return booEstado;
            }

            if (Convert.ToInt16(CboDoc.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            return booEstado;
        }

        private void FrmResumenDiarioBoletas_Activated(object sender, EventArgs e)
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
        void ListarItems()
        {
            LblNumReg.Text = (dtRegistros.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtRegistros, true);
        }
        void VerRegistro(int n_IdRegistro)
        {
            DataTable dtdat = new DataTable();
            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdRegistro);
            e_boleta = objRegistros.e_Boletas;
            dtdat = objRegistros.dtBoletasdet;

            CboDoc.SelectedValue = e_boleta.n_idtipdoc;
            TxtFchDec.Text = Convert.ToDateTime(e_boleta.d_fchdoc).ToString("dd/MM/yyyy");
            TxtFchEmiDoc.Text = Convert.ToDateTime(e_boleta.d_fchven).ToString("dd/MM/yyyy");
            TxtNumArchGen.Text = e_boleta.n_numarc.ToString();
            LblNumDoc.Text = dtdat.Rows.Count.ToString();

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtdat, 2, true);
        }

        private void FrmResumenDiarioBoletas_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 81, this.Width - 18);
        }
        void Tab_Dimensionar(C1.Win.C1Command.C1DockingTab dokTab, int intAlto, int intAncho)
        {
            Tab1.Height = intAlto;
            Tab1.Width = intAncho;
        }

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            //VerRegistro(intIdRegistro);
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

        private void FgItems_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            FgItems.AllowEditing = false;

            //if ((FgItems.Col == 12) || (FgItems.Col == 13) || (FgItems.Col == 14))
            if (FgItems.Col == 14)
            {
                FgItems.AllowEditing = true;
            }
        }

        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {

        }

        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3) { return; }
            //if ((FgItems.Col == 12) || (FgItems.Col == 13) || (FgItems.Col == 14))
            if (FgItems.Col == 14)
            {
                double n_valor = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row,14)));
                double n_impbru = (n_valor / 1.18);
                double n_impigv = (n_valor - (n_valor / 1.18));

                FgItems.SetData(FgItems.Row, 12, n_impbru.ToString("0.00"));
                FgItems.SetData(FgItems.Row, 13, n_impigv.ToString("0.00"));
            }
        }
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            if (Tab1.SelectedIndex == 0)
            { 
                int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
                Tab1.SelectedIndex = 1;
                VerRegistro(intIdRegistro);
            }
            string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-BOL-" + Convert.ToDateTime(TxtFchEmiDoc.Text).ToString("dd-MM-yyyy") + ".xls";
            funFlex.ExportToExcel(FgItems, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "BOLETAS A DECLARAR", "DEL DIA" + Convert.ToDateTime(TxtFchEmiDoc.Text).ToString("dd-MM-yyyy"), c_nomarch);
        }

        private void ToolEnviar_Click(object sender, EventArgs e)
        {
            int n_idreg = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            int n_numarch = Convert.ToInt16(DgLista.Columns["n_numarc"].CellValue(DgLista.Row).ToString());
            string c_fchven = DgLista.Columns["d_fchven"].CellValue(DgLista.Row).ToString();
            string c_RutaArchPla = entEmpresa.c_vtafearcdat;
            CN_vta_boletaresumen o_bol = new CN_vta_boletaresumen();
            o_bol.STU_SISTEMA = STU_SISTEMA;
            o_bol.mysConec = mysConec;
            if (o_bol.GenerarBoletaResumen(n_idreg, c_fchven, n_numarch, c_RutaArchPla) == true)
            {
                MessageBox.Show("¡ Las boletas se exportaron con exito ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("¡ No se pudo enviar las boletas de venta por el siguiente motivo ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void CboMeses_SelectedIndexChanged(object sender, EventArgs e)
        {
             DataTable dtResul = new DataTable();
            if (booAgregando == true) { return; }

            //dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO
            //dtResul = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
            //dtRegistros = dtResul;

            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue));
            dtRegistros = objRegistros.dtLista;
            LblNumReg.Text = (dtRegistros.Rows.Count).ToString();
            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);

            //ToolImpoApertura.Visible = false;
            //if (Convert.ToInt16(CboMeses.SelectedValue) == 0) { ToolImpoApertura.Visible = true; }

            // MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
            DgLista.DataSource = dtRegistros;
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

        private void CmdDelDoc_Click(object sender, EventArgs e)
        {
            //int n_idvta = Convert.ToInt32(FgItems.GetData(FgItems.Row, 15));
            DialogResult Rpta = MessageBox.Show("¿ Esta seguro de eliminar el documento seleccionado ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                FgItems.RemoveItem(FgItems.Row);
                LblNumDoc.Text = (FgItems.Rows.Count - 2).ToString();
                MessageBox.Show("¡ Documento se elimino con exito ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
    }
}
