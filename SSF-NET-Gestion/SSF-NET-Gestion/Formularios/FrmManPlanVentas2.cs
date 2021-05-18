using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Gestion;
using SIAC_Negocio.Almacen;
using SIAC_Entidades.Gestion;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;

namespace SSF_NET_Gestion.Formularios
{
    public partial class FrmManPlanVentas2 : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_alm_inventario o_inventa = new CN_alm_inventario();
        CN_ges_planventas objCabecera = new CN_ges_planventas();                            // CABECERA DEL REGISTRO

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        
        // ENTIDADES LOCALES
        BE_GES_PLANVENTAS BE_CABECERA = new BE_GES_PLANVENTAS();
        List<BE_GES_PLANVENTASDET> LS_DETALLE = new List<BE_GES_PLANVENTASDET>();
        List<BE_GES_PLANVENTASANOS> LS_DETALLEANOS = new List<BE_GES_PLANVENTASANOS>();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();                                        // CABECERA DEL REGISTRO
        DataTable dtCabecera = new DataTable();                                        // CABECERA DEL REGISTRO
        DataTable dtDetalle = new DataTable();                                         // DETALLE DEL REGISTRO
        DataTable dtForm = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtMeses1 = new DataTable();
        DataTable dtVenHis = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtVenHisDet = new DataTable();

        // VARIABLES LOCALES
        int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[5, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[17, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890./" + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        int n_idformulario = 25;                                                        // INDICA EL ID DEL FORMULARIO EN LA BD
        ToolTip o_tool = new ToolTip();

        public FrmManPlanVentas2()
        {
            InitializeComponent();
        }
        private void FrmManPlanVentas2_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        private void ToolSalir_Click(object sender, EventArgs e)
        {
            objCabecera = null;
            //objDetalle = null;
            dtCabecera = null;
            dtDetalle = null;
            objFormVis = null;
            dtDetalle = null;
            this.Close();
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboMesIni, dtMeses1, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");

            // LLENAMOS LA LISTA DE AÑOS
            LlenarAnosHistorico();
        }
        void LlenarAnosHistorico()
        {
            DataTable dtResul = new DataTable();
            int n_row = 0;
            // LLENAMOS LA LISTA DE AÑOS
            FgHisAno.Rows.Count = 1;
            objCabecera.TraerDataAnos(STU_SISTEMA.EMPRESAID);                   // TRAE LOS AÑOS DE TRABAJO

            dtResul = objCabecera.dtDataAnos;
            if (dtResul.Rows.Count != 0)
            {
                for (n_row = 0; n_row <= dtResul.Rows.Count - 1; n_row++)
                {
                    FgHisAno.Rows.Count = FgHisAno.Rows.Count + 1;
                    FgHisAno.SetData(FgHisAno.Rows.Count - 1, 1, dtResul.Rows[n_row]["n_anotra"].ToString());
                    FgHisAno.SetData(FgHisAno.Rows.Count - 1, 2, true);
                }
            }
        }
        void ConfigurarFormulario()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.Height = 583;
            //this.Width = 986;
            
            //Tab_Posicionar(Tab1, 1, 42);
            //Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            o_tool.AutoPopDelay = 5000;
            o_tool.InitialDelay = 0;
            o_tool.ReshowDelay = 100;
            // Force the ToolTip text to be displayed whether or not the form is active.
            o_tool.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            o_tool.SetToolTip(this.CmdAddValores, "Agregar promedios de los años seleccionados");
            o_tool.SetToolTip(this.Cmd_VerGra, "Importar plan de ventas anterior");

            FgItems.Rows.Count = 2;
            FgHisAno.AllowEditing = false;
            FgItems.AllowEditing = false;

            arrCabeceraFlex1[0, 0] = "Codigo";
            arrCabeceraFlex1[0, 1] = "100";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_codpro";

            arrCabeceraFlex1[1, 0] = "Descripcion";
            arrCabeceraFlex1[1, 1] = "350";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_despro";

            arrCabeceraFlex1[2, 0] = "Uni. Med";
            arrCabeceraFlex1[2, 1] = "50";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_abrpre";

            arrCabeceraFlex1[3, 0] = "Enero";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "0.00";
            arrCabeceraFlex1[3, 4] = "Enero";

            arrCabeceraFlex1[4, 0] = "Febrero";
            arrCabeceraFlex1[4, 1] = "70";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.00";
            arrCabeceraFlex1[4, 4] = "Febrero";

            arrCabeceraFlex1[5, 0] = "Marzo";
            arrCabeceraFlex1[5, 1] = "70";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "Marzo";

            arrCabeceraFlex1[6, 0] = "Abril";
            arrCabeceraFlex1[6, 1] = "70";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "Abril";

            arrCabeceraFlex1[7, 0] = "Mayo";
            arrCabeceraFlex1[7, 1] = "70";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "Mayo";

            arrCabeceraFlex1[8, 0] = "Junio";
            arrCabeceraFlex1[8, 1] = "70";
            arrCabeceraFlex1[8, 2] = "D";
            arrCabeceraFlex1[8, 3] = "0.00";
            arrCabeceraFlex1[8, 4] = "Junio";

            arrCabeceraFlex1[9, 0] = "Julio";
            arrCabeceraFlex1[9, 1] = "70";
            arrCabeceraFlex1[9, 2] = "D";
            arrCabeceraFlex1[9, 3] = "0.00";
            arrCabeceraFlex1[9, 4] = "Julio";

            arrCabeceraFlex1[10, 0] = "Agosto";
            arrCabeceraFlex1[10, 1] = "70";
            arrCabeceraFlex1[10, 2] = "D";
            arrCabeceraFlex1[10, 3] = "0.00";
            arrCabeceraFlex1[10, 4] = "Agosto";

            arrCabeceraFlex1[11, 0] = "Setiembre";
            arrCabeceraFlex1[11, 1] = "70";
            arrCabeceraFlex1[11, 2] = "D";
            arrCabeceraFlex1[11, 3] = "0.00";
            arrCabeceraFlex1[11, 4] = "Setiembre";

            arrCabeceraFlex1[12, 0] = "Octubre";
            arrCabeceraFlex1[12, 1] = "70";
            arrCabeceraFlex1[12, 2] = "D";
            arrCabeceraFlex1[12, 3] = "0.00";
            arrCabeceraFlex1[12, 4] = "Octubre";

            arrCabeceraFlex1[13, 0] = "Noviembre";
            arrCabeceraFlex1[13, 1] = "70";
            arrCabeceraFlex1[13, 2] = "D";
            arrCabeceraFlex1[13, 3] = "0.00";
            arrCabeceraFlex1[13, 4] = "Noviembre";

            arrCabeceraFlex1[14, 0] = "Diciembre";
            arrCabeceraFlex1[14, 1] = "70";
            arrCabeceraFlex1[14, 2] = "D";
            arrCabeceraFlex1[14, 3] = "0.00";
            arrCabeceraFlex1[14, 4] = "Diciembre";

            arrCabeceraFlex1[15, 0] = "Total";
            arrCabeceraFlex1[15, 1] = "70";
            arrCabeceraFlex1[15, 2] = "D";
            arrCabeceraFlex1[15, 3] = "0.00";
            arrCabeceraFlex1[15, 4] = "n_canpro";

            arrCabeceraFlex1[16, 0] = "n_iditem";
            arrCabeceraFlex1[16, 1] = "0";
            arrCabeceraFlex1[16, 2] = "N";
            arrCabeceraFlex1[16, 3] = "0.00";
            arrCabeceraFlex1[16, 4] = "n_iditem";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtLista, 2, false);
            FgItems.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            CboMesIni.SelectedValue = DateTime.Now.Month;
            //MostrarCabeceraMes();
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;                  // INDICAMOS EL NUMERO DE FILAS PARA EL DETALLE
            this.Text = dtForm.Rows[0]["c_titfor"].ToString() + " - Materia Prima";
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;

            FgHisAno.AllowEditing = true;
        }
        void DataTableCargar()
        {
            objCabecera.mysConec = mysConec;
            objCabecera.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO);
            dtLista = objCabecera.dtLista;

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();
            dtMeses1 = objMeses.Listar();

            //objCabecera.TraerItems(STU_SISTEMA.EMPRESAID);
            o_inventa.mysConec = mysConec;
            dtItems = o_inventa.Listar(0, 0, 1);
                        
            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(n_idformulario);

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(n_idformulario, ref arrCabeceraDg1);
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtLista.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtLista, true);
        }
        void Tab_Dimensionar(C1.Win.C1Command.C1DockingTab dokTab, int intAlto, int intAncho)
        {
            //Tab1.Height = intAlto;
            //Tab1.Width = intAncho;
            //FgItems.Width = Tab1.Width - 70;
            //FgItems.Height = Tab1.Height - 143;
        }
        void Tab_Posicionar(C1.Win.C1Command.C1DockingTab dokTab, int intPosX, int intPosY)
        {
            dokTab.Left = intPosX;
            dokTab.Top = intPosY;
        }
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            VerRegistro(intIdRegistro);
            booAgregando = false;
        }
        void VerRegistro(int n_IdRegistro)
        {
            int n_row = 0;
            DataTable dtresult = new DataTable();
            DataTable dtresultanos = new DataTable();
            DataTable dtOrdenMed = new DataTable();

            objCabecera.mysConec = mysConec;
            objCabecera.TraerRegistro(n_IdRegistro);
            objCabecera.OrdenMeses(n_IdRegistro);
            BE_CABECERA = objCabecera.entCabecera;

            dtresult = objCabecera.dtDetalle;
            dtresultanos = objCabecera.dtDetalleAnos;
            dtOrdenMed = objCabecera.dtOrdenMes;

            TxtFchCre.Text = BE_CABECERA.d_fchcre.ToString("dd/MM/yyyy");
            TxtDes.Text = BE_CABECERA.c_des.ToString();
            CboMesIni.SelectedValue = BE_CABECERA.n_idmesini;

            MostrarMeses(BE_CABECERA.n_idmesini);
            FgItems.Rows.Count = 2;
            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtresult, 2, true);
            LblNumRegistro.Text = (FgItems.Rows.Count - 2).ToString();
            FgHisAno.Rows.Count = 1;
            if (dtresultanos.Rows.Count != 0)
            {
                int n_fila = 0;

                for (n_fila = 0; n_fila <= dtresultanos.Rows.Count - 1; n_fila++)
                {
                    FgHisAno.Rows.Count = FgHisAno.Rows.Count + 1;
                    FgHisAno.SetData(FgHisAno.Rows.Count - 1, 1, dtresultanos.Rows[n_fila]["n_idano"].ToString());
                    FgHisAno.SetData(FgHisAno.Rows.Count - 1, 2, true);
                }
            }
            CargarVentasHistoricas();
        }
        private void FrmManPlanVentas2_Activated(object sender, EventArgs e)
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

        private void Tab1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;

            if (DgLista.RowCount == 0) { return; }
            if (n_QueHace != 3) { return; }

            if (tc.SelectedIndex == 1)
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }

        private void FrmManPlanVentas2_Resize(object sender, EventArgs e)
        {
            //Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
            //FgItems.Width = Tab1.Width - 70;
            //FgItems.Height = Tab1.Height - 143;
        }
        void Nuevo()
        {
            DataTable dtres = new DataTable();
            objCabecera.mysConec = mysConec;
            objCabecera.ListarActivos(STU_SISTEMA.EMPRESAID);
            dtres = objCabecera.dtLista;
            if (dtres.Rows.Count != 0)
            {
                MessageBox.Show("¡ Existe un plan de ventas activo, debe de desactivar el plan activo para poder crear un nuevo plan !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            booAgregando = true;
            n_QueHace = 1;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            Tab1.SelectedIndex = 1;
            FgItems.Rows.Count = 2;
            LlenarAnosHistorico();
            
            int n_row = 0;
            for (n_row = 0; n_row <= FgHisAno.Rows.Count - 1; n_row++)
            {
                if (funFunciones.NulosC(FgHisAno.GetData(n_row, 1)) == STU_SISTEMA.ANOTRABAJO.ToString())
                {
                    FgHisAno.SetData(n_row, 2, true);
                }
                else
                {
                    FgHisAno.SetData(n_row, 2, false);
                }
            }
            CboMesIni.SelectedValue =  DateTime.Now.Month;
            MostrarMeses(Convert.ToInt32(CboMesIni.SelectedValue));
            booAgregando = false;
            TxtFchIni.Focus();
        }
        void MostrarMeses(int n_MesInicio)
        {
            DataTable dtres = new DataTable();
            int n_col = 0;
            int n_col2 = 3;
            int n_mes = n_MesInicio;
            
            for (n_col = 1; n_col <= 12; n_col++)
            {
                dtres = funDatos.DataTableFiltrar( dtMeses,"n_id = " + n_mes.ToString()  + "");
                arrCabeceraFlex1[n_col2, 0] = funFunciones.ConvertirTitulo(dtres.Rows[0]["c_des"].ToString());
                arrCabeceraFlex1[n_col2, 4] = funFunciones.ConvertirTitulo(dtres.Rows[0]["c_des"].ToString());
                //n_mes = DateTime.Now.AddMonths(n_col).Month;
                n_mes = n_mes + 1;
                if (n_mes > 12) n_mes = 1;

                n_col2 = n_col2 + 1;
            }
            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtLista, 2, false);
        }
        void CargarVentasHistoricas()
        {
            string c_anohistoria = "";
            int n_fila;

            for (n_fila = 1; n_fila <= FgHisAno.Rows.Count - 1; n_fila++)
            {
                if (n_fila == 1)
                {
                    c_anohistoria = c_anohistoria + FgHisAno.GetData(n_fila, 1).ToString();
                }
                else 
                { 
                    c_anohistoria = c_anohistoria + ", " + FgHisAno.GetData(n_fila, 1).ToString();
                }
            }

            objCabecera.TraerVentaHistorica(STU_SISTEMA.EMPRESAID, 5, c_anohistoria);
            objCabecera.TraerVentaHistoricaDetalle(STU_SISTEMA.EMPRESAID, c_anohistoria);

            dtVenHis = objCabecera.dtVenHis;
            dtVenHisDet = objCabecera.dtDetalleAnos;
        }
        private void ToolNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }
        void Blanquea()
        {
            TxtFchIni.Text = "";
            TxtFchFin.Text = "";
            TxtDes.Text = "";
        }
        void Bloquea()
        {
            TxtFchIni.Enabled = !TxtFchIni.Enabled;
            TxtFchFin.Enabled = !TxtFchFin.Enabled;
            TxtDes.Enabled = !TxtDes.Enabled;

            CmdAddItem.Enabled = !CmdAddItem.Enabled;
            CmdDelItem.Enabled = !CmdDelItem.Enabled;
            CmdDelTod.Enabled = !CmdDelTod.Enabled;

            CmdAllItem.Enabled = !CmdAllItem.Enabled;
            CmdRetItem.Enabled = !CmdRetItem.Enabled;

            CmdAddValores.Enabled = !CmdAddValores.Enabled;
            Cmd_VerGra.Enabled = !Cmd_VerGra.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar2.Enabled = !ToolEliminar2.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir.Enabled = !ToolImprimir.Enabled;
            //ToolExportar.Enabled = !ToolExportar.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
        }
        void Modificar()
        {
            if (DgLista.RowCount == 0) { return; }
            booAgregando = true;
            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            TxtFchIni.Focus();
            booAgregando = false;
        }
        private void ToolModificar_Click(object sender, EventArgs e)
        {
            Modificar();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            objCabecera.mysConec = mysConec;
            objCabecera.TraerRegistro(intIdRegistro);
            BE_CABECERA = objCabecera.entCabecera;
            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {

                if (objCabecera.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objCabecera.mysConec = mysConec;
                    objCabecera.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO);
                    dtLista = objCabecera.dtLista;
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objCabecera.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            return booResult;
        }
        private void ToolEliminar_Click(object sender, EventArgs e)
        {
            
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
        private void ToolCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }
        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            if (Grabar() == true)
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objCabecera.mysConec = mysConec;
                objCabecera.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO);
                dtLista = objCabecera.dtLista;
                
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
        bool Grabar()
        {
            bool booResultado = false;
            if (CamposOK() == false)
            {
                return booResultado;
            }
            AsignarEntidad();

            objCabecera.entCabecera = BE_CABECERA;
            objCabecera.lstDetalle = LS_DETALLE;
            objCabecera.lstDetalleAnos = LS_DETALLEANOS;
            if (n_QueHace == 1)
            {
                booResultado = objCabecera.Insertar();
            }
            if (n_QueHace == 2)
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
                objCabecera.entCabecera.n_id = intIdRegistro;
                booResultado = objCabecera.Actualizar();
            }
            if (booResultado == false)
            {
                MessageBox.Show(" No se pudo guardar el registro por el siguiente motivo : " + objCabecera.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_iditem = 0;
            int n_idmes = 0;
            int n_fila = 0;
            int n_col = 0;
            int n_NumeroElementos = 0;
            //string c_des;
            DataTable dtresult = new DataTable();
            LS_DETALLEANOS.Clear();

            BE_CABECERA.n_idemp  = STU_SISTEMA.EMPRESAID;
            BE_CABECERA.n_id = 0;
            BE_CABECERA.n_idano = STU_SISTEMA.ANOTRABAJO;
            BE_CABECERA.n_idmes = STU_SISTEMA.MESTRABAJO;
            BE_CABECERA.d_fchcre = Convert.ToDateTime(TxtFchCre.Text);
            BE_CABECERA.c_des = TxtDes.Text;
            BE_CABECERA.n_idmesini = Convert.ToInt32(CboMesIni.SelectedValue);
            BE_CABECERA.n_activo = 1;

            // LIMPIAMOS LA LISTA
            if (LS_DETALLE != null)
            {
                n_NumeroElementos = Convert.ToInt32(LS_DETALLE.Count - 1);

                for (n_fila = 0; n_fila <= n_NumeroElementos; n_fila++)
                {
                    LS_DETALLE.RemoveAt(0);
                }
            }

            n_fila = 0;
            booAgregando = true;
            int n_orden = 0;

            if (FgItems.Rows.Count > 2)
            {
                for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
                {
                    n_orden = 1;

                    for (n_col = 4; n_col <= 15; n_col++)
                    { 
                        n_iditem = 0;
                        n_idmes = 0;

                        if (funFunciones.NulosC(FgItems.GetData(0, n_col).ToString()) == "Enero") { n_idmes = 1; }
                        if (funFunciones.NulosC(FgItems.GetData(0, n_col).ToString()) == "Febrero") { n_idmes = 2; }
                        if (funFunciones.NulosC(FgItems.GetData(0, n_col).ToString()) == "Marzo") { n_idmes = 3; }
                        if (funFunciones.NulosC(FgItems.GetData(0, n_col).ToString()) == "Abril") { n_idmes = 4; }
                        if (funFunciones.NulosC(FgItems.GetData(0, n_col).ToString()) == "Mayo") { n_idmes = 5; }
                        if (funFunciones.NulosC(FgItems.GetData(0, n_col).ToString()) == "Junio") { n_idmes = 6; }
                        if (funFunciones.NulosC(FgItems.GetData(0, n_col).ToString()) == "Julio") { n_idmes = 7; }
                        if (funFunciones.NulosC(FgItems.GetData(0, n_col).ToString()) == "Agosto") { n_idmes = 8; }
                        if (funFunciones.NulosC(FgItems.GetData(0, n_col).ToString()) == "Setiembre") { n_idmes = 9; }
                        if (funFunciones.NulosC(FgItems.GetData(0, n_col).ToString()) == "Octubre") { n_idmes = 10; }
                        if (funFunciones.NulosC(FgItems.GetData(0, n_col).ToString()) == "Noviembre") { n_idmes = 11; }
                        if (funFunciones.NulosC(FgItems.GetData(0, n_col).ToString()) == "Diciembre") { n_idmes = 12; }
                                                                                          
                        n_iditem = Convert.ToInt32(funFunciones.NulosN(FgItems.GetData(n_fila, 17).ToString()));
                        
                        BE_GES_PLANVENTASDET BE_Detalle = new BE_GES_PLANVENTASDET();
                        BE_Detalle.n_idplan = 0;
                        BE_Detalle.n_idite = n_iditem;
                        BE_Detalle.n_idmes = n_idmes;
                        BE_Detalle.n_canite = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_fila, n_col)));
                        BE_Detalle.n_orden = n_orden;
                            
                        LS_DETALLE.Add(BE_Detalle);
                        n_orden = n_orden + 1;
                    }
                }
            }

            if (FgHisAno.Rows.Count > 1)
            {
                for (n_fila = 1; n_fila <= FgHisAno.Rows.Count - 1; n_fila++)
                {
                    if (FgHisAno.GetData(n_fila, 2).ToString() == "True")
                    { 
                        BE_GES_PLANVENTASANOS BE_DetalleAnos = new BE_GES_PLANVENTASANOS();
                        BE_DetalleAnos.n_idplan = 0;
                        BE_DetalleAnos.n_idano = Convert.ToInt32(funFunciones.NulosN(FgHisAno.GetData(n_fila, 1)));
                        LS_DETALLEANOS.Add(BE_DetalleAnos);
                    }
                }
            }
            booAgregando = false;
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (TxtDes.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la descripcion del Plan de Ventas !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
                        
            if (FgItems.Rows.Count == 2)
            {
                MessageBox.Show("¡ No ha especificado ningun item para este plan de ventas !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            //int n_fila;
            //// VERIFICAMOS QUE LOS DATOS DE LAS PRESENTACIONES ESTEN COMPLETAS
            //for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
            //{
            //    if (funFunciones.NulosC(FgItems.GetData(n_fila, 18)) == "")
            //    {
            //        MessageBox.Show(" EL producto " + FgItems.GetData(n_fila ,6).ToString() + " no tiene datos de venta", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //        booEstado = false;
            //        return booEstado;
            //    }
            //}

            return booEstado;
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
        private void CmdAddItem_Click(object sender, EventArgs e)
        {
            DataTable dtresul = new DataTable();
            string c_dato = "";
            string c_cadin = funFlex.Flex_CadenaIN(FgItems, 17, 2);
            objCabecera.mysConec = mysConec;
            dtresul = objCabecera.BuscarItems(STU_SISTEMA.EMPRESAID, c_cadin);
            if (dtresul == null) { return; }

            if (dtresul.Rows.Count != 0)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;

                c_dato = dtresul.Rows[0]["c_codpro"].ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 1, c_dato);

                c_dato = dtresul.Rows[0]["c_despro"].ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 2, c_dato);

                c_dato = dtresul.Rows[0]["c_abrpre"].ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 3, c_dato);
                
                c_dato = Convert.ToInt32(dtresul.Rows[0]["n_iditem"]).ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 17, c_dato);

                LblNumRegistro.Text = (FgItems.Rows.Count - 2).ToString();
                FgItems.Select(FgItems.Rows.Count - 1, 1);
                FrmManPlanVentasHistorico xFrm = new FrmManPlanVentasHistorico();
                int n_id = Convert.ToInt32(dtresul.Rows[0]["n_iditem"]);
                xFrm.n_IdItem = n_id;
                xFrm.mysConec = mysConec;
                xFrm.STU_SISTEMA = STU_SISTEMA;
                xFrm.dtItems = dtItems;
                xFrm.ShowDialog();
                
                string[,] a_Datos = new string[1, 13];
                a_Datos = xFrm.a_Datos;
                bool b_cargado = xFrm.b_cargado;
                xFrm.Close();

                if (b_cargado == true)
                {
                    int n_row = 0;
                    int n_col = 0;
                    string c_mes = "";
                    for (n_col = 4; n_col <= 17; n_col++)
                    {
                        c_mes = FgItems.GetData(1, n_col).ToString();
                        for (n_row = 0; n_row <= 12; n_row++)
                        {
                            if (c_mes == a_Datos[n_row, 0])
                            {
                                FgItems.SetData(FgItems.Row, n_col, a_Datos[n_row, 1]);
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void FgItems_Click(object sender, EventArgs e)
        {

        }
        private void FgItems_DoubleClick(object sender, EventArgs e)
        {
            //int n_iditem;
            //DataTable dtresult = new DataTable();
            
            //dtresult = funDatos.DataTableFiltrar(dtItems,"c_despro= '" + FgItems.GetData(FgItems.Row,2).ToString() + "'");

            //if (dtresult.Rows.Count != 0)
            //{
            //    n_iditem = Convert.ToInt32(dtresult.Rows[0]["n_id"].ToString());
            //    FrmManPlanVentasHistorico FrmVista = new FrmManPlanVentasHistorico();
            //    FrmVista.dtHistorico = dtVenHisDet;
            //    FrmVista.dtItems = dtresult;
            //    FrmVista.n_IdItem = n_iditem;
            //    FrmVista.ShowDialog();
            //}
        }
        private void CmdAddValores_Click(object sender, EventArgs e)
        {
            int n_row = 0;
            int n_anotra = 0;
            int n_numsel = 0;
            for (n_row = 1; n_row <= FgHisAno.Rows.Count - 1; n_row++)
            {
                if (FgHisAno.GetData(n_row, 2).ToString() == "True")
                {
                    n_numsel = n_numsel + 1;
                }
            }

            if (n_numsel > 1)
            {
                MessageBox.Show(" Se ha seleccionado mas de 1 año de trabajo, solo debe de seleccionar 1", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgHisAno.Focus();
                return;
            }

            for (n_row = 1; n_row <= FgHisAno.Rows.Count - 1; n_row++)
            {
                if (FgHisAno.GetData(n_row, 2).ToString() == "True")
                {
                    n_anotra = Convert.ToInt32(FgHisAno.GetData(n_row, 1));
                }
            }

            DataTable dtresul = new DataTable();
            CN_ges_planventas o_plan = new CN_ges_planventas();

            o_plan.mysConec = mysConec;
            o_plan.VentasAnuales(STU_SISTEMA.EMPRESAID, n_anotra);
            dtresul = o_plan.dtLista;

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtresul, 2, true);
            LblNumRegistro.Text = (FgItems.Rows.Count - 2).ToString();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CboMesIni_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            MostrarCabeceraMes();
        }
        void MostrarCabeceraMes()
        {
            int n_col = 0;
            int n_posArray = 5;
            int n_nummes = Convert.ToInt32(CboMesIni.SelectedValue);
            DataTable dtResult = new DataTable();
            string c_nommes = "";

            for (n_col = 1; n_col <= 12; n_col++)
            {
                c_nommes = Convert.ToString(funDatos.DataTableBuscar(dtMeses, "n_id", "c_des", n_nummes.ToString(), "N"));
                arrCabeceraFlex1[n_posArray, 0] = c_nommes;
                arrCabeceraFlex1[n_posArray, 1] = "80";
                arrCabeceraFlex1[n_posArray, 2] = "N";
                arrCabeceraFlex1[n_posArray, 3] = "0.00";
                arrCabeceraFlex1[n_posArray, 4] = "";

                n_nummes = n_nummes + 1;
                n_posArray = n_posArray + 1;
                if (n_nummes > 12) n_nummes = 1;
            }
            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtDetalle, 2, false);
        }
        private void Cmd_VerGra_Click(object sender, EventArgs e)
        {
            DataTable dtres = new DataTable();
            CN_ges_planproduccion o_plan = new CN_ges_planproduccion();
            CN_ges_planventas o_planven = new CN_ges_planventas();

            o_plan.mysConec = mysConec;
            o_plan.BuscarPlanVentasActivo(STU_SISTEMA.EMPRESAID,2);
            dtres = o_plan.dtLista;
            if (dtres == null) { return; }
            if (dtres.Rows.Count == 0) { return; }

            int n_idplavenant = Convert.ToInt32(dtres.Rows[0]["n_id"]);
            int n_row = 0;

            CN_ges_planventas o_plaven = new CN_ges_planventas();
            o_plaven.mysConec = mysConec;
            o_plaven.TraerRegistro(n_idplavenant);
            dtres = o_plaven.dtDetalle;
            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtres, 2, true);

            //int n_col = 0;
            //bool b_sindato = false;
            //if (FgItems.Rows.Count == 2)
            //{
            //    MessageBox.Show("¡ No ha agregado ningun producto o servicio, debe agregar al menos 1 producto o servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    return;
            //}

            //for (n_col = 6; n_col <= 17; n_col++)
            //{
            //    if (FgItems.GetData(FgItems.Row, 6) == null)
            //    {
            //        b_sindato = false;
            //    }
            //    else
            //    { 
            //        if (Convert.ToDouble(FgItems.GetData(FgItems.Row, 6).ToString()) != 0)
            //        {
            //            b_sindato = true;
            //            break;
            //        }
            //    }
            //}

            //if (b_sindato == false)
            //{
            //    MessageBox.Show("¡ No hay valores que mostrar para el producto "+ FgItems.GetData(FgItems.Row,4).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    return;
            //}

            //string[,] arrCabeceraDg1 = new string[7, 4];
            //string[] c_TitulosY = new string[12];
            //double[,] C_ValoresX = new double[1, 12];
            //string[,] c_Series = new string[1,5];

            //c_Series[0,0]  = "2011";
            //c_Series[0,1]  = "2012";
            //c_Series[0,2]  = "2013";
            //c_Series[0,3]  = "2014";
            //c_Series[0,4]  = "2015";

            //c_TitulosY[0] = FgItems.GetData(0, 6).ToString().Substring(0, 3);
            //c_TitulosY[1] = FgItems.GetData(0, 7).ToString().Substring(0, 3);
            //c_TitulosY[2] = FgItems.GetData(0, 8).ToString().Substring(0, 3);
            //c_TitulosY[3] = FgItems.GetData(0, 9).ToString().Substring(0, 3);
            //c_TitulosY[4] = FgItems.GetData(0, 10).ToString().Substring(0, 3);
            //c_TitulosY[5] = FgItems.GetData(0, 11).ToString().Substring(0, 3);
            //c_TitulosY[6] = FgItems.GetData(0, 12).ToString().Substring(0, 3);
            //c_TitulosY[7] = FgItems.GetData(0, 13).ToString().Substring(0, 3);
            //c_TitulosY[8] = FgItems.GetData(0, 14).ToString().Substring(0, 3);
            //c_TitulosY[9] = FgItems.GetData(0, 15).ToString().Substring(0, 3);
            //c_TitulosY[10] = FgItems.GetData(0, 16).ToString().Substring(0, 3);
            //c_TitulosY[11] = FgItems.GetData(0, 17).ToString().Substring(0, 3);

            //C_ValoresX[0, 0] = Convert.ToDouble(FgItems.GetData(FgItems.Row,6));
            //C_ValoresX[0, 1] = Convert.ToDouble(FgItems.GetData(FgItems.Row, 7));
            //C_ValoresX[0, 2] = Convert.ToDouble(FgItems.GetData(FgItems.Row, 8));
            //C_ValoresX[0, 3] = Convert.ToDouble(FgItems.GetData(FgItems.Row, 9));
            //C_ValoresX[0, 4] = Convert.ToDouble(FgItems.GetData(FgItems.Row, 10));
            //C_ValoresX[0, 5] = Convert.ToDouble(FgItems.GetData(FgItems.Row, 11));
            //C_ValoresX[0, 6] = Convert.ToDouble(FgItems.GetData(FgItems.Row, 12));
            //C_ValoresX[0, 7] = Convert.ToDouble(FgItems.GetData(FgItems.Row, 13));
            //C_ValoresX[0, 8] = Convert.ToDouble(FgItems.GetData(FgItems.Row, 14));
            //C_ValoresX[0, 9] = Convert.ToDouble(FgItems.GetData(FgItems.Row, 15));
            //C_ValoresX[0, 10] = Convert.ToDouble(FgItems.GetData(FgItems.Row, 16));
            //C_ValoresX[0, 11] = Convert.ToDouble(FgItems.GetData(FgItems.Row, 17));

            //funDatos.Grafico_TituloGrafico = "PROMEDIO DE VENTAS - DEL 2011 AL 2016";
            //funDatos.Grafico_PieGrafico = "PRODUCTO : " + FgItems.GetData(FgItems.Row,4).ToString();
            //funDatos.Grafico_Series = c_Series;
            //funDatos.Grafico_TitulosY = c_TitulosY;
            //funDatos.Grafico_ValoresX = C_ValoresX;
            //funDatos.Grafico_Pie = true;
            //funDatos.Grafico_Cabecera = true;
            //funDatos.Grafico_Legenda = false;
            //funDatos.VerGrafico();
        }
        private void CmdDelItem_Click(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count == 2)
            {
                MessageBox.Show("¡ No ha agregado ningun producto o servicio, debe agregar al menos 1 producto o servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            FgItems.RemoveItem(FgItems.Row);
            LblNumRegistro.Text = (FgItems.Rows.Count - 2).ToString();
        }
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void CmdAllItem_Click(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count == 2)
            {
                MessageBox.Show("¡ No ha agregado ningun producto o servicio, debe agregar al menos 1 producto o servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            int n_fila;

            for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
            {
                if (FgItems.GetData(n_fila, 18) == null)
                {
                    FgItems.RemoveItem(n_fila);
                    n_fila = n_fila - 1;
                }
                else
                { 
                    if (FgItems.GetData(n_fila,18).ToString() == "")
                    { 
                        FgItems.RemoveItem(n_fila);
                    }
                }
            }
            MessageBox.Show("¡ Se eliminaron los items sin movimiento con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count == 2)
            {
                MessageBox.Show("¡ No ha hay datos para exportar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            //funFlex.ExportToExcel(FgItems, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, TxtDes.Text, "MES DE INICIO : "+ CboMeses.SelectedText );
        }
        private void CmdRetItem_Click(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count == 2) { return; }

            string c_dato = "";
            string[,] a_Datos = new string[13, 2];

            a_Datos[0, 0] = FgItems.GetData(1, 4).ToString();
            a_Datos[0, 1] = FgItems.GetData(FgItems.Row, 4).ToString();

            a_Datos[1, 0] = FgItems.GetData(1, 5).ToString();
            a_Datos[1, 1] = FgItems.GetData(FgItems.Row, 5).ToString();

            a_Datos[2, 0] = FgItems.GetData(1, 6).ToString();
            a_Datos[2, 1] = FgItems.GetData(FgItems.Row, 6).ToString();

            a_Datos[3, 0] = FgItems.GetData(1, 7).ToString();
            a_Datos[3, 1] = FgItems.GetData(FgItems.Row, 7).ToString();

            a_Datos[4, 0] = FgItems.GetData(1, 8).ToString();
            a_Datos[4, 1] = FgItems.GetData(FgItems.Row, 8).ToString();

            a_Datos[5, 0] = FgItems.GetData(1, 9).ToString();
            a_Datos[5, 1] = FgItems.GetData(FgItems.Row, 9).ToString();

            a_Datos[6, 0] = FgItems.GetData(1, 10).ToString();
            a_Datos[6, 1] = FgItems.GetData(FgItems.Row, 10).ToString();

            a_Datos[7, 0] = FgItems.GetData(1, 11).ToString();
            a_Datos[7, 1] = FgItems.GetData(FgItems.Row, 11).ToString();

            a_Datos[8, 0] = FgItems.GetData(1, 12).ToString();
            a_Datos[8, 1] = FgItems.GetData(FgItems.Row, 12).ToString();

            a_Datos[9, 0] = FgItems.GetData(1, 13).ToString();
            a_Datos[9, 1] = FgItems.GetData(FgItems.Row, 13).ToString();

            a_Datos[10, 0] = FgItems.GetData(1, 14).ToString();
            a_Datos[10, 1] = FgItems.GetData(FgItems.Row, 14).ToString();

            a_Datos[11, 0] = FgItems.GetData(1, 15).ToString();
            a_Datos[11, 1] = FgItems.GetData(FgItems.Row, 15).ToString();
            
            a_Datos[12, 0] = FgItems.GetData(1, 16).ToString();
            a_Datos[12, 1] = FgItems.GetData(FgItems.Row, 16).ToString();

            FrmManPlanVentasHistorico xFrm = new FrmManPlanVentasHistorico();
            int n_id = Convert.ToInt32(FgItems.GetData(FgItems.Row, 17));
            xFrm.n_IdItem = n_id;
            xFrm.mysConec = mysConec;
            xFrm.STU_SISTEMA = STU_SISTEMA;
            xFrm.dtItems = dtItems;
            xFrm.a_Datos = a_Datos;
            xFrm.ShowDialog();
            a_Datos = xFrm.a_Datos;
            bool b_cargado = xFrm.b_cargado;
            xFrm.Close();
            
            if (b_cargado ==true)
            {
                int n_row = 0;
                int n_col = 0;
                string c_mes = "";
                for (n_col = 4; n_col <= 17; n_col++)
                {
                    c_mes = FgItems.GetData(1, n_col).ToString();
                    for (n_row = 0; n_row <= 12; n_row++)
                    {
                        if (c_mes == a_Datos[n_row, 0])
                        { 
                            FgItems.SetData(FgItems.Row,n_col,a_Datos[n_row, 1]);
                            break;
                        }
                    }
                }
            }
        }
        string MostrarDato(string c_Dato, string[,] a_Datos)
        {
            string c_valor = "";
            if (c_Dato == "Enero") { c_valor = a_Datos[0, 0]; }
            if (c_Dato == "Febrero") { c_valor = a_Datos[0, 1]; }
            if (c_Dato == "Marzo") { c_valor = a_Datos[0, 2]; }
            if (c_Dato == "Abril") { c_valor = a_Datos[0, 3]; }
            if (c_Dato == "Mayo") { c_valor = a_Datos[0, 4]; }
            if (c_Dato == "Junio") { c_valor = a_Datos[0, 5]; }
            if (c_Dato == "Julio") { c_valor = a_Datos[0, 6]; }
            if (c_Dato == "Agosto") { c_valor = a_Datos[0, 7]; }
            if (c_Dato == "Setiembre") { c_valor = a_Datos[0, 8]; }
            if (c_Dato == "Octubre") { c_valor = a_Datos[0, 9]; }
            if (c_Dato == "Noviembre") { c_valor = a_Datos[0, 10]; }
            if (c_Dato == "Diciembre") { c_valor = a_Datos[0, 11]; }
            if (c_Dato == "Total") { c_valor = a_Datos[0, 12]; }
            return c_valor;
        }
        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            //DataTable dtResul = new DataTable();
            //if (booAgregando == true) { return; }

            //objCabecera.mysConec = mysConec;
            //objCabecera.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue));
            //dtLista = objCabecera.dtLista;

            //ListarItems();
            //LblNumReg.Text = (dtLista.Rows.Count).ToString();
            //STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            
            //DgLista.DataSource = dtLista;
            //if (dtLista.Rows.Count == 0)
            //{
            //    DialogResult Rpta = MessageBox.Show("No se han encontrado registros, ¿ Desea agregar uno ahora ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            //    if (DialogResult.Yes == Rpta)
            //    {
            //        Nuevo();
            //    }
            //    else
            //    {
            //        this.Close();
            //    }
            //}
            //else
            //{
            //    DgLista.Focus();
            //}
        }
        private void CmdDelTod_Click(object sender, EventArgs e)
        {
            FgItems.Rows.Count = 2;
            LblNumRegistro.Text = (FgItems.Rows.Count - 2).ToString();
        }
        private void FgHisAno_EnterCell(object sender, EventArgs e)
        {
            if (FgHisAno.Col == 1)
            {
                FgHisAno.AllowEditing = false;
            }
            if (FgHisAno.Col == 2)
            {
                FgHisAno.AllowEditing = true;
            }
        }
        private void FgHisAno_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            //if (booAgregando == true) { return; }
            //int n_row = 0;
            //int n_fila = e.Row;
            //for (n_row = 1; n_row <= FgHisAno.Rows.Count - 1; n_row++)
            //{
            //    e.Cancel = true;
            //    FgHisAno.SetData(n_row, 2, 0);
            //}

            ////e.Cancel = true;
            //FgHisAno.SetData(n_fila, 2, 1);
        }

        private void eliminarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void desactivarPlanVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DesactivarPlanVentas();
        }

        void ActivarPlanVentas()
        {
            int n_idreg = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            objCabecera.mysConec = mysConec;
            objCabecera.CambiarEstadoPlanVentas(n_idreg, 1);
            if (objCabecera.booOcurrioError == false)
            {
                objCabecera.mysConec = mysConec;
                objCabecera.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO);
                dtLista = objCabecera.dtLista;
                ListarItems();

                MessageBox.Show("¡ El plan de ventas se activó con éxito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("¡ No se pudo activar el plan de ventas por el siguiente motivo:" + objCabecera.StrErrorMensaje.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        void DesactivarPlanVentas()
        {
            int n_idreg = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            objCabecera.mysConec = mysConec;
            objCabecera.CambiarEstadoPlanVentas(n_idreg, 2);
            if (objCabecera.booOcurrioError == false)
            {
                objCabecera.mysConec = mysConec;
                objCabecera.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO);
                dtLista = objCabecera.dtLista;
                ListarItems();

                MessageBox.Show("¡ El plan de ventas se desactivo con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("¡ No se pudo desactivar el plan de ventas por el siguiente motivo:" + objCabecera.StrErrorMensaje.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void CmdAddValores_MouseHover(object sender, EventArgs e)
        {
        }

        private void Cmd_VerGra_MouseHover(object sender, EventArgs e)
        {
        }

        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            switch (e.Col)
            {
                case 1:
                    break;
                default:
                    if (!strNumerovalidos.Contains(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
            }

        }

        private void FgItems_EnterCell(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count <= 2) { return; }

            if (booAgregando == true) { return; }

            switch (FgItems.Col)
            {
                case 1:
                    FgItems.AllowEditing = false;
                    break;
                default:
                    FgItems.AllowEditing = true;
                    break;
            }
        }

        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (booAgregando == true) { return; }
            if (FgItems.Rows.Count > 2)
            {
                if ((e.Col >= 4) && (e.Col <= 15))
                {
                    double n_valor = funFlex.FlexSumarRow(FgItems, e.Row, 4, 15);
                    FgItems.SetData(e.Row, 16, n_valor.ToString("0.00"));
                }
                FgItems.Select(e.Row, e.Col);
            }
        }

        private void modificarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modificar();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ActivarPlanVentas();
        }
    }
}
