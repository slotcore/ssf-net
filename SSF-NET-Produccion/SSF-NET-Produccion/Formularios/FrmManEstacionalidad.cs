using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Produccion;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Produccion;
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

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmManEstacionalidad : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_pro_estacionalidad objRegistros = new CN_pro_estacionalidad();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_alm_inventario objItem = new CN_alm_inventario();
        CN_mae_estados objestado = new CN_mae_estados();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_PRO_ESTACIONALIDAD BE_Registro = new BE_PRO_ESTACIONALIDAD();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO
        DataTable dtItems = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtestado = new DataTable();

        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[16, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[2, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManEstacionalidad()
        {
            InitializeComponent();
        }
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void FrmManEstacionalidad_Load(object sender, EventArgs e)
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

            dtResult = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = 3", "c_despro");
            funDatos.ComboBoxCargarDataTable(CboMP, dtResult, "n_id", "c_despro");
        }
        void ConfigurarFormulario()
        {
            this.Height = 580;
            this.Width = 909;
            
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            arrCabeceraFlex1[0, 0] = "Periodo";
            arrCabeceraFlex1[0, 1] = "200";
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Valor";
            arrCabeceraFlex1[1, 1] = "150";
            arrCabeceraFlex1[1, 2] = "";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";
            
            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtLista, 2, false);

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(30, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                         // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(30);

            objItem.mysConec = mysConec;
            dtItems = objItem.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);

            objestado.mysConec = mysConec;
            dtestado = objestado.Listar(30);
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
            int n_fila = 0;
            string c_dato = "";            
            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdRegistro);
            
            FgItems.Rows.Count = 2;
            FgItems.Rows.Count = 14;
            if (objRegistros.booOcurrioError == false)
            {
                BE_Registro = objRegistros.entEstacion;
                CboMP.SelectedValue = BE_Registro.n_idite;

                FgItems.SetData(2, 1, "ENERO");
                c_dato = funDatos.DataTableBuscar(dtestado, "n_id", "c_des", BE_Registro.n_ene.ToString(), "N").ToString();
                FgItems.SetData(2, 2, c_dato);

                FgItems.SetData(3, 1, "FEBRERO");
                c_dato = funDatos.DataTableBuscar(dtestado, "n_id", "c_des", BE_Registro.n_feb.ToString(), "N").ToString();
                FgItems.SetData(3, 2, c_dato);

                FgItems.SetData(4, 1, "MARZO");
                c_dato = funDatos.DataTableBuscar(dtestado, "n_id", "c_des", BE_Registro.n_mar.ToString(), "N").ToString();
                FgItems.SetData(4, 2, c_dato);

                FgItems.SetData(5, 1, "ABRIL");
                c_dato = funDatos.DataTableBuscar(dtestado, "n_id", "c_des", BE_Registro.n_abr.ToString(), "N").ToString();
                FgItems.SetData(5, 2, c_dato);

                FgItems.SetData(6, 1, "MAYO");
                c_dato = funDatos.DataTableBuscar(dtestado, "n_id", "c_des", BE_Registro.n_may.ToString(), "N").ToString();
                FgItems.SetData(6, 2, c_dato);

                FgItems.SetData(7, 1, "JUNIO");
                c_dato = funDatos.DataTableBuscar(dtestado, "n_id", "c_des", BE_Registro.n_jun.ToString(), "N").ToString();
                FgItems.SetData(7, 2, c_dato);

                FgItems.SetData(8, 1, "JULIO");
                c_dato = funDatos.DataTableBuscar(dtestado, "n_id", "c_des", BE_Registro.n_jul.ToString(), "N").ToString();
                FgItems.SetData(8, 2, c_dato);

                FgItems.SetData(9, 1, "AGOSTO");
                c_dato = funDatos.DataTableBuscar(dtestado, "n_id", "c_des", BE_Registro.n_ago.ToString(), "N").ToString();
                FgItems.SetData(9, 2, c_dato);

                FgItems.SetData(10, 1, "SETIEMBRE");
                c_dato = funDatos.DataTableBuscar(dtestado, "n_id", "c_des", BE_Registro.n_set.ToString(), "N").ToString();
                FgItems.SetData(10, 2, c_dato);

                FgItems.SetData(11, 1, "OCTUBRE");
                c_dato = funDatos.DataTableBuscar(dtestado, "n_id", "c_des", BE_Registro.n_oct.ToString(), "N").ToString();
                FgItems.SetData(11, 2, c_dato);

                FgItems.SetData(12, 1, "NOVIEMBRE");
                c_dato = funDatos.DataTableBuscar(dtestado, "n_id", "c_des", BE_Registro.n_nov.ToString(), "N").ToString();
                FgItems.SetData(12, 2, c_dato);

                FgItems.SetData(13, 1, "DICIEMBRE");
                c_dato = funDatos.DataTableBuscar(dtestado, "n_id", "c_des", BE_Registro.n_dic.ToString(), "N").ToString();
                FgItems.SetData(13, 2, c_dato);

                n_fila = n_fila + 1;
            }
            booAgregando = false;
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
            ConfigurarFlex();
            CboMP.Focus();
        }
        void ConfigurarFlex()
        {
            booAgregando = true;
            FgItems.Rows.Count = 2;
            FgItems.Rows.Count = 14;

            FgItems.SetData(2, 1, "ENERO");
            FgItems.SetData(3, 1, "FEBRERO");
            FgItems.SetData(4, 1, "MARZO");
            FgItems.SetData(5, 1, "ABRIL");
            FgItems.SetData(6, 1, "MAYO");
            FgItems.SetData(7, 1, "JUNIO");
            FgItems.SetData(8, 1, "JULIO");
            FgItems.SetData(9, 1, "AGOSTO");
            FgItems.SetData(10, 1, "SETIEMBRE");
            FgItems.SetData(11, 1, "OCTUBRE");
            FgItems.SetData(12, 1, "NOVIEMBRE");
            FgItems.SetData(13, 1, "DICIEMBRE");
            booAgregando = false;
        }
        void Blanquea()
        {
            CboMP.SelectedValue = 0;
        }
        void Bloquea()
        {
            CboMP.Enabled = !CboMP.Enabled;
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
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[1].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            CboMP.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[9].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
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
                booResultado = objRegistros.Insertar(BE_Registro);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(BE_Registro);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            string c_dato;
            BE_Registro.n_idemp = STU_SISTEMA.EMPRESAID;
            if (n_QueHace == 1)
            { 
                BE_Registro.n_id = 0; 
            }
            else
            { 
                BE_Registro.n_id = BE_Registro.n_id; 
            }
            
            BE_Registro.n_idite = Convert.ToInt32(CboMP.SelectedValue);
            BE_Registro.n_idmp = 0;
            BE_Registro.c_des = CboMP.Text;

            c_dato = FgItems.GetData(2, 2).ToString();
            c_dato = funDatos.DataTableBuscar(dtestado, "c_des", "n_id", c_dato, "C").ToString();
            BE_Registro.n_ene = Convert.ToInt32(c_dato);

            c_dato = FgItems.GetData(3, 2).ToString();
            c_dato = funDatos.DataTableBuscar(dtestado, "c_des", "n_id", c_dato, "C").ToString();
            BE_Registro.n_feb = Convert.ToInt32(c_dato);

            c_dato = FgItems.GetData(4, 2).ToString();
            c_dato = funDatos.DataTableBuscar(dtestado, "c_des", "n_id", c_dato, "C").ToString();
            BE_Registro.n_mar = Convert.ToInt32(c_dato);

            c_dato = FgItems.GetData(5, 2).ToString();
            c_dato = funDatos.DataTableBuscar(dtestado, "c_des", "n_id", c_dato, "C").ToString();
            BE_Registro.n_abr = Convert.ToInt32(c_dato);

            c_dato = FgItems.GetData(6, 2).ToString();
            c_dato = funDatos.DataTableBuscar(dtestado, "c_des", "n_id", c_dato, "C").ToString();
            BE_Registro.n_may = Convert.ToInt32(c_dato);

            c_dato = FgItems.GetData(7, 2).ToString();
            c_dato = funDatos.DataTableBuscar(dtestado, "c_des", "n_id", c_dato, "C").ToString();
            BE_Registro.n_jun = Convert.ToInt32(c_dato);

            c_dato = FgItems.GetData(8, 2).ToString();
            c_dato = funDatos.DataTableBuscar(dtestado, "c_des", "n_id", c_dato, "C").ToString();
            BE_Registro.n_jul = Convert.ToInt32(c_dato);

            c_dato = FgItems.GetData(9, 2).ToString();
            c_dato = funDatos.DataTableBuscar(dtestado, "c_des", "n_id", c_dato, "C").ToString();
            BE_Registro.n_ago = Convert.ToInt32(c_dato);

            c_dato = FgItems.GetData(10, 2).ToString();
            c_dato = funDatos.DataTableBuscar(dtestado, "c_des", "n_id", c_dato, "C").ToString();
            BE_Registro.n_set = Convert.ToInt32(c_dato);

            c_dato = FgItems.GetData(11, 2).ToString();
            c_dato = funDatos.DataTableBuscar(dtestado, "c_des", "n_id", c_dato, "C").ToString();
            BE_Registro.n_oct = Convert.ToInt32(c_dato);

            c_dato = FgItems.GetData(12, 2).ToString();
            c_dato = funDatos.DataTableBuscar(dtestado, "c_des", "n_id", c_dato, "C").ToString();
            BE_Registro.n_nov = Convert.ToInt32(c_dato);

            c_dato = FgItems.GetData(13, 2).ToString();
            c_dato = funDatos.DataTableBuscar(dtestado, "c_des", "n_id", c_dato, "C").ToString();
            BE_Registro.n_dic = Convert.ToInt32(c_dato);

            //BE_Registro.n = Convert.ToInt32(CboMP.SelectedValue);
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt32(CboMP.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la unidad e medida de la tarea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMP.Focus();
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
                objRegistros.mysConec = mysConec;
                dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
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
        private void FrmManEstacionalidad_Activated(object sender, EventArgs e)
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
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[1].CellValue(DgLista.Row).ToString());
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
                int intIdRegistro = Convert.ToInt32(DgLista.Columns[1].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }

        private void FgItems_EnterCell(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count == 2) { return; }
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }
            if (booAgregando == true) { return; }

            DataTable dtResul = new DataTable();

            if (FgItems.Col == 2)
            {
                funFlex.FlexColumnaCombo(FgItems, dtestado, "c_des", 2);
            }

            FgItems.AllowEditing = true;
        }

        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 2)
            {
                FgItems.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            if (FgItems.Col == 1)
            {
                FgItems.Select(FgItems.Row - 1, 2);
                return;
            }
            if (FgItems.Col == 2)
            {
                FgItems.Select(FgItems.Row , 1);
                return;
            }
        }

        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 2)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void FgItems_RowColChange(object sender, EventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }
        }

    }
}
