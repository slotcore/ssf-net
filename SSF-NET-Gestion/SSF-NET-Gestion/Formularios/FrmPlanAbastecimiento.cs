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
using SIAC_Negocio.Produccion;
using SIAC_Entidades.Gestion;
using SIAC_Entidades.Produccion;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using SSF_NET_Produccion;
using C1.Win.C1FlexGrid;
using SIAC_Negocio.Sunat;
using Newtonsoft.Json;

namespace SSF_NET_Gestion.Formularios
{
    public partial class FrmPlanAbastecimiento : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sun_tipexi objTipExi = new CN_sun_tipexi();
        CN_sun_unimed objUniMed = new CN_sun_unimed();
        CN_alm_inventario ObjItem = new CN_alm_inventario();

        CN_ges_planabastecimiento objCabecera = new CN_ges_planabastecimiento();                            // CABECERA DEL REGISTRO
        //CN_ges_planventasdet objDetalle = new CN_ges_planventasdet();                       // DETALLE DEL REGISTRO

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_GES_PLANABASTECIMIENTO BE_CABECERA = new BE_GES_PLANABASTECIMIENTO();
        List<BE_GES_PLANABASTECIMIENTODET> BE_DETALLE = new List<BE_GES_PLANABASTECIMIENTODET>();

        // DATATABLE LOCALES
        DataTable dtCabecera = new DataTable();                                        // CABECERA DEL REGISTRO
        DataTable dtDetalle = new DataTable();                                         // DETALLE DEL REGISTRO
        DataTable dtForm = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtRecetas = new DataTable();
        DataTable dtProducto = new DataTable();
        DataTable dtTipExi = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtUniMed = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[6, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[16, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890./" + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmPlanAbastecimiento()
        {
            InitializeComponent();
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            dtCabecera = null;
            dtDetalle = null;
            objFormVis = null;
            dtDetalle = null;
            this.Close();
        }
        void CargarCombos()
        {
            DataTableCargar();
            //funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMesIni, dtMeses, "n_id", "c_des");
        }
        void ConfigurarFlex()
        {
            arrCabeceraFlex1[0, 0] = "IdItem";
            arrCabeceraFlex1[0, 1] = "0";
            arrCabeceraFlex1[0, 2] = "N";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "n_iditem";

            arrCabeceraFlex1[1, 0] = "Producto";
            arrCabeceraFlex1[1, 1] = "300";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_despro";

            arrCabeceraFlex1[2, 0] = "Uni. Med.";
            arrCabeceraFlex1[2, 1] = "40";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_abrpre";

            arrCabeceraFlex1[3, 0] = "Enero";
            arrCabeceraFlex1[3, 1] = "80";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "0.00";
            arrCabeceraFlex1[3, 4] = "n_numlot";

            arrCabeceraFlex1[4, 0] = "Febrero";
            arrCabeceraFlex1[4, 1] = "80";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.00";
            arrCabeceraFlex1[4, 4] = "d_fchpro";

            arrCabeceraFlex1[5, 0] = "Marzo";
            arrCabeceraFlex1[5, 1] = "80";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "d_fchven";

            arrCabeceraFlex1[6, 0] = "Abril";
            arrCabeceraFlex1[6, 1] = "80";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "d_fchven";

            arrCabeceraFlex1[7, 0] = "Mayo";
            arrCabeceraFlex1[7, 1] = "80";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "d_fchven";

            arrCabeceraFlex1[8, 0] = "Junio";
            arrCabeceraFlex1[8, 1] = "80";
            arrCabeceraFlex1[8, 2] = "D";
            arrCabeceraFlex1[8, 3] = "0.00";
            arrCabeceraFlex1[8, 4] = "d_fchven";

            arrCabeceraFlex1[9, 0] = "Julio";
            arrCabeceraFlex1[9, 1] = "80";
            arrCabeceraFlex1[9, 2] = "D";
            arrCabeceraFlex1[9, 3] = "0.00";
            arrCabeceraFlex1[9, 4] = "d_fchven";

            arrCabeceraFlex1[10, 0] = "Agosto";
            arrCabeceraFlex1[10, 1] = "80";
            arrCabeceraFlex1[10, 2] = "D";
            arrCabeceraFlex1[10, 3] = "0.00";
            arrCabeceraFlex1[10, 4] = "d_fchven";

            arrCabeceraFlex1[11, 0] = "Setiembre";
            arrCabeceraFlex1[11, 1] = "80";
            arrCabeceraFlex1[11, 2] = "D";
            arrCabeceraFlex1[11, 3] = "0.00";
            arrCabeceraFlex1[11, 4] = "d_fchven";

            arrCabeceraFlex1[12, 0] = "Octubre";
            arrCabeceraFlex1[12, 1] = "80";
            arrCabeceraFlex1[12, 2] = "D";
            arrCabeceraFlex1[12, 3] = "0.00";
            arrCabeceraFlex1[12, 4] = "d_fchven";

            arrCabeceraFlex1[13, 0] = "Noviembre";
            arrCabeceraFlex1[13, 1] = "80";
            arrCabeceraFlex1[13, 2] = "D";
            arrCabeceraFlex1[13, 3] = "0.00";
            arrCabeceraFlex1[13, 4] = "d_fchven";

            arrCabeceraFlex1[14, 0] = "Diciembre";
            arrCabeceraFlex1[14, 1] = "80";
            arrCabeceraFlex1[14, 2] = "D";
            arrCabeceraFlex1[14, 3] = "0.00";
            arrCabeceraFlex1[14, 4] = "d_fchven";

            arrCabeceraFlex1[15, 0] = "Total";
            arrCabeceraFlex1[15, 1] = "80";
            arrCabeceraFlex1[15, 2] = "D";
            arrCabeceraFlex1[15, 3] = "0.00";
            arrCabeceraFlex1[15, 4] = "n_canpro";

            funFlex.FlexMostrarDatos(FgProd, arrCabeceraFlex1, dtDetalle, 2, false);
            funFlex.FlexMostrarDatos(FgInter, arrCabeceraFlex1, dtDetalle, 2, false);
            funFlex.FlexMostrarDatos(FgTodo, arrCabeceraFlex1, dtDetalle, 2, false);

            funFlex.FlexMostrarDatos(FgProdIns, arrCabeceraFlex1, dtDetalle, 2, false);
            funFlex.FlexMostrarDatos(FgInterIns, arrCabeceraFlex1, dtDetalle, 2, false);
            funFlex.FlexMostrarDatos(FgTodoIns, arrCabeceraFlex1, dtDetalle, 2, false);
        }       
        void ConfigurarFormulario()
        {
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            ConfigurarFlex();

            //FgProd.Rows.Count = FgProd.Rows.Count + n_NumFilasDocumento;                  // INDICAMOS EL NUMERO DE FILAS PARA EL DETALLE
            this.Text = dtForm.Rows[0]["c_titfor"].ToString() + " - Materia Prima";
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        void DataTableCargar()
        {
            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();
            dtMeses = funDatos.DataTableFiltrar(dtMeses, "n_id NOT IN (0,13)");

            objCabecera.mysConec = mysConec;
            objCabecera.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            dtCabecera = objCabecera.dtLista;

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(95);

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(95, ref arrCabeceraDg1);

            CN_pro_productos o_producto = new CN_pro_productos();
            o_producto.mysConec = mysConec;
            o_producto.Listar(0, 0);
            dtProducto = o_producto.dtListar;
            o_producto = null;

            CN_pro_productosrecetas o_receta = new CN_pro_productosrecetas();
            o_receta.mysConec = mysConec;
            o_receta.Consulta3();
            dtRecetas = o_receta.dtLista;
            o_receta = null;

            objTipExi.mysConec = mysConec;
            dtTipExi = objTipExi.Listar();

            objUniMed.mysConec = mysConec;
            dtUniMed = objUniMed.Listar();

            ObjItem.mysConec = mysConec;
            dtItems = ObjItem.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtCabecera.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtCabecera, true);
        }
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int n_idreg = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            VerRegistro(n_idreg);
            booAgregando = false;
        }
        void VerRegistro(int n_IdRegistro)
        {
            DataTable dtPro = new DataTable();
            DataTable dtInter = new DataTable();
            DataTable dtTodo = new DataTable();
            DataTable dtProIns = new DataTable();
            DataTable dtInterIns = new DataTable();
            DataTable dtTodoIns = new DataTable();

            CN_ges_planproduccion o_plapro = new CN_ges_planproduccion();
            BE_GES_PLANPRODUCCION e_plapro = new BE_GES_PLANPRODUCCION();
            DataTable dtres = new DataTable();

            FgProd.Rows.Count = 2;
            FgInter.Rows.Count = 2;
            FgTodo.Rows.Count = 2;

            FgProdIns.Rows.Count = 2;
            FgInterIns.Rows.Count = 2;
            FgTodoIns.Rows.Count = 2;

            objCabecera.mysConec = mysConec;
            objCabecera.TraerRegistro(n_IdRegistro);

            BE_CABECERA = objCabecera.e_Cabecera;
            dtPro = objCabecera.dtProducto;
            dtInter = objCabecera.dtIntermedio;
            dtTodo = objCabecera.dtTodo;
            dtProIns = objCabecera.dtProductoIns;
            dtInterIns = objCabecera.dtIntermedioIns;
            dtTodoIns = objCabecera.dtTodoIns;

            TxtDes.Text = BE_CABECERA.c_des;
            TxtFchIni.Text = Convert.ToDateTime(BE_CABECERA.d_fchini).ToString("dd/MM/yyyy");
            TxtFchFin.Text = Convert.ToDateTime(BE_CABECERA.d_fchfin).ToString("dd/MM/yyyy");
            LblIdPlaVen.Text = BE_CABECERA.n_idplapro.ToString();
            CboMesIni.SelectedValue = BE_CABECERA.n_mesini;

            o_plapro.mysConec = mysConec;
            o_plapro.TraerRegistro(BE_CABECERA.n_idplapro);
            e_plapro = o_plapro.e_Cabecera;
            TxtPlaVen.Text = e_plapro.c_des;
            o_plapro = null;

            llenarFlex(FgProd, dtPro, BE_CABECERA.n_mesini);
            llenarFlex(FgInter, dtInter, BE_CABECERA.n_mesini);
            llenarFlex(FgTodo, dtTodo, BE_CABECERA.n_mesini);

            llenarFlex(FgProdIns, dtProIns, BE_CABECERA.n_mesini);
            llenarFlex(FgInterIns, dtInterIns, BE_CABECERA.n_mesini);
            llenarFlex(FgTodoIns, dtTodoIns, BE_CABECERA.n_mesini);

            Tab2.SelectedIndex = 0;
        }
        void llenarFlex(C1.Win.C1FlexGrid.C1FlexGrid fg_Control, DataTable dtDatos, int n_MesInicio)
        {
            DataTable dtres = new DataTable();
            //DataTable dtmes = new DataTable();
            int n_col = 0;
            int n_col2 = 3;
            int n_mes = n_MesInicio;

            //dtmes = funDatos.DataTableFiltrar(dtMeses, "n_id NOT IN(0,13)");
            for (n_col = 1; n_col <= 12; n_col++)
            {
                dtres = funDatos.DataTableFiltrar(dtMeses, "n_id = " + n_mes.ToString() + "");
                arrCabeceraFlex1[n_col2, 0] = funFunciones.ConvertirTitulo(dtres.Rows[0]["c_des"].ToString());
                arrCabeceraFlex1[n_col2, 4] = funFunciones.ConvertirTitulo(dtres.Rows[0]["c_des"].ToString());
                n_mes = DateTime.Now.AddMonths(n_col).Month;
                n_col2 = n_col2 + 1;
            }
            funFlex.FlexMostrarDatos(fg_Control, arrCabeceraFlex1, dtDatos, 2, true);

            //DataTable dtres = dtDatos;
            //int n_fil = 0;
            //string c_dato = "";
            //double n_valor = 0;
            //for (n_fil = 0; n_fil <= dtres.Rows.Count - 1; n_fil++)
            //{
            //    fg_Control.Rows.Count = fg_Control.Rows.Count + 1;

            //    c_dato = dtDatos.Rows[n_fil]["n_iditem"].ToString();
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 1, c_dato);

            //    c_dato = dtDatos.Rows[n_fil]["c_despro"].ToString();
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 2, c_dato);

            //    c_dato = dtDatos.Rows[n_fil]["c_abrpre"].ToString();
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 3, c_dato);

            //    n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["January"]);
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 4, n_valor.ToString("0.00"));

            //    n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["February"]);
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 5, n_valor.ToString("0.00"));

            //    n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["March"]);
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 6, n_valor.ToString("0.00"));

            //    n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["April"]);
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 7, n_valor.ToString("0.00"));

            //    n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["May"]);
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 8, n_valor.ToString("0.00"));

            //    n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["June"]);
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 9, n_valor.ToString("0.00"));

            //    n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["July"]);
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 10, n_valor.ToString("0.00"));

            //    n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["August"]);
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 11, n_valor.ToString("0.00"));

            //    n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["September"]);
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 12, n_valor.ToString("0.00"));

            //    n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["October"]);
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 13, n_valor.ToString("0.00"));

            //    n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["Novenber"]);
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 14, n_valor.ToString("0.00"));

            //    n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["December"]);
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 15, n_valor.ToString("0.00"));

            //    n_valor = funFlex.FlexSumarRow(fg_Control, fg_Control.Rows.Count - 1, 4, 15);
            //    fg_Control.SetData(fg_Control.Rows.Count - 1, 16, n_valor.ToString("0.00"));
            //}
        }
        //private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        //{
        //    if (n_QueHace != 3) { return; }

        //    if (e.NewIndex == 1)
        //    {
        //        int n_idreg = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

        //        if (n_QueHace != 1)
        //        {
        //            booAgregando = true;
        //            VerRegistro(n_idreg);
        //            booAgregando = false;
        //        }
        //    }
        //}
        private void Tab1_SelectedIndexChanging(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;

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
        void Nuevo()
        {
            DataTable dtres = new DataTable();
            objCabecera.mysConec = mysConec;
            objCabecera.ListarActivos(STU_SISTEMA.EMPRESAID);
            dtres = objCabecera.dtLista;
            if (dtres.Rows.Count != 0)
            {
                MessageBox.Show("¡ Existe un plan de abastecimiento activo, debe de desactivar el plan activo para poder crear un nuevo plan !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
            Tab2.SelectedIndex = 0;
            FgProd.Rows.Count = 2;
            CboMesIni.SelectedValue = DateTime.Now.Month;
            MostrarMeses(Convert.ToInt32(CboMesIni.SelectedValue));
            booAgregando = false;
            TxtFchIni.Focus();
        }
        private void ToolNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }
        void Blanquea()
        {
            TxtFchIni.Text = "";
            TxtFchFin.Text = "";
            TxtPlaVen.Text = "";
            TxtDes.Text = "";
            LblIdPlaVen.Text = "";
            FgProd.Rows.Count = 2;
            FgInter.Rows.Count = 2;
            FgTodo.Rows.Count = 2;

            FgProdIns.Rows.Count = 2;
            FgInterIns.Rows.Count = 2;
            FgTodoIns.Rows.Count = 2;
        }
        void Bloquea()
        {
            TxtFchIni.Enabled = !TxtFchIni.Enabled;
            TxtFchFin.Enabled = !TxtFchFin.Enabled;
            TxtDes.Enabled = !TxtDes.Enabled;

            CmdAddValores.Enabled = !CmdAddValores.Enabled;
            CmdBusCli.Enabled = !CmdBusCli.Enabled;
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
            Tab2.SelectedIndex = 0;
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

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                objCabecera.mysConec = mysConec;
                if (objCabecera.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objCabecera.mysConec = mysConec;
                    objCabecera.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, 0);
                    dtCabecera = objCabecera.dtLista;
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objCabecera.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            return booResult;
        }
        private void ToolEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
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
                objCabecera.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, 0);
                dtCabecera = objCabecera.dtLista;
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
        bool Grabar()
        {
            bool booResultado = false;
            if (CamposOK() == false)
            {
                return booResultado;
            }
            AsignarEntidad();

            objCabecera.e_Cabecera = BE_CABECERA;
            objCabecera.l_DetPro = BE_DETALLE;
            if (n_QueHace == 1)
            {
                booResultado = objCabecera.Insertar();
            }

            if (n_QueHace == 2)
            {
                booResultado = objCabecera.Actualizar();
            }

            if (booResultado == false)
            {
                MessageBox.Show(" No se pudo guardar el registro por el siguiente motivo : " + objCabecera.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_fila = 0;
            int n_col = 0;
            int n_numele = 0;
            int n_numcol = 0;
            int n_id = 0;

            if (n_QueHace == 1) { n_id = 0; }
            if (n_QueHace == 2) { n_id = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString()); }

            BE_DETALLE.Clear();

            BE_CABECERA.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_CABECERA.n_id = n_id;
            BE_CABECERA.c_des = TxtDes.Text;
            BE_CABECERA.d_fchini = Convert.ToDateTime("01/" + Convert.ToInt32(CboMesIni.SelectedValue).ToString() + "/" +STU_SISTEMA.ANOTRABAJO.ToString());
            BE_CABECERA.d_fchfin = Convert.ToDateTime( BE_CABECERA.d_fchini.AddMonths(12).ToString("dd/MM/yyyy"));
            BE_CABECERA.n_mesini = Convert.ToInt32(CboMesIni.SelectedValue);
            BE_CABECERA.n_ano = Convert.ToDateTime(TxtFchIni.Text).Year;
            BE_CABECERA.n_idplapro = Convert.ToInt32(LblIdPlaVen.Text);
            BE_CABECERA.n_activo = 1;

            n_numele = FgProd.Rows.Count - 1;
            n_numcol = FgProd.Cols.Count - 2;
            for (n_fila = 2; n_fila <= n_numele; n_fila++)
            {
                for (n_col = 4; n_col <= n_numcol; n_col++)
                {
                    BE_GES_PLANABASTECIMIENTODET e_detalle = new BE_GES_PLANABASTECIMIENTODET();
                    e_detalle.n_idplaaba = n_id;
                    e_detalle.n_idite = Convert.ToInt32(FgProd.GetData(n_fila, 1));
                    e_detalle.n_idmes = Buscarmes(FgProd.GetData(1, n_col).ToString());
                    e_detalle.n_idtipite = 1;
                    e_detalle.n_can = Convert.ToDouble(FgProd.GetData(n_fila, n_col));
                    BE_DETALLE.Add(e_detalle);
                }
            }

            n_numele = FgInter.Rows.Count - 1;
            n_numcol = FgInter.Cols.Count - 2;
            for (n_fila = 2; n_fila <= n_numele; n_fila++)
            {
                for (n_col = 4; n_col <= n_numcol; n_col++)
                {
                    BE_GES_PLANABASTECIMIENTODET e_detalle = new BE_GES_PLANABASTECIMIENTODET();
                    e_detalle.n_idplaaba = n_id;
                    e_detalle.n_idite = Convert.ToInt32(FgInter.GetData(n_fila, 1));
                    e_detalle.n_idmes = Buscarmes(FgInter.GetData(1, n_col).ToString());
                    e_detalle.n_idtipite = 2;
                    e_detalle.n_can = Convert.ToDouble(FgInter.GetData(n_fila, n_col));
                    BE_DETALLE.Add(e_detalle);
                }
            }
        }
        int Buscarmes(string c_NomMes)
        {
            int n_nummes = Convert.ToInt32(funDatos.DataTableBuscar(dtMeses, "c_des", "n_id", c_NomMes, "C"));
            return n_nummes;
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (TxtFchIni.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de inicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchIni.Focus();
                return booEstado;
            }

            if (TxtFchFin.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de termino !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchFin.Focus();
                return booEstado;
            }

            if (TxtDes.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la descripcion del plan de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtDes.Focus();
                return booEstado;
            }

            if (FgProd.Rows.Count == 2)
            {
                MessageBox.Show("¡ No ha especificado los productos para plan de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            return booEstado;
        }
        private void DgLista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DataTable dtResult = new DataTable();

                string c_CadFiltro = funDbGrid.DG_LeerCondicionesFiltro(DgLista);
                dtResult = funDbGrid.DG_Filtrar(dtCabecera, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
        }
        private void FrmPlanAbastecimiento_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        private void FrmPlanAbastecimiento_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (dtCabecera.Rows.Count == 0)
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
        private void CmdBusCli_Click(object sender, EventArgs e)
        {
            DataTable dtres = new DataTable();
            CN_ges_planproduccion o_plan = new CN_ges_planproduccion();
            CN_ges_planventas o_planven = new CN_ges_planventas();

            o_plan.mysConec = mysConec;
            o_plan.BuscarPlanProduccionActivo(STU_SISTEMA.EMPRESAID);
            dtres = o_plan.dtLista;
            if (dtres == null) { return; }
            if (dtres.Rows.Count == 0) { return; }
            LblIdPlaVen.Text = Convert.ToInt32(dtres.Rows[0]["n_id"]).ToString();
            TxtPlaVen.Text = dtres.Rows[0]["c_des"].ToString();
            TxtFchIni.Text = Convert.ToDateTime(dtres.Rows[0]["d_fchini"]).ToString();
            TxtFchFin.Text = Convert.ToDateTime(dtres.Rows[0]["d_fchfin"]).ToString();
        }

        private void CmdAddValores_Click(object sender, EventArgs e)
        {
            DataTable dtpro = new DataTable();
            DataTable dtint = new DataTable();
            DataTable dttod = new DataTable();

            int n_idplapro = Convert.ToInt32(LblIdPlaVen.Text);
            CN_ges_planproduccion o_plapro = new CN_ges_planproduccion();
            o_plapro.mysConec = mysConec;

            o_plapro.TraerRegistro(n_idplapro);
            dtpro = o_plapro.dtProducto;
            dtint = o_plapro.dtIntermedio;
            dttod = o_plapro.dtTodo;
            o_plapro = null;

            llenarFlex(FgProd, dtpro, Convert.ToInt32(CboMesIni.SelectedValue));
            llenarFlex(FgInter, dtint, Convert.ToInt32(CboMesIni.SelectedValue));
            llenarFlex(FgTodo, dttod, Convert.ToInt32(CboMesIni.SelectedValue));
            Tab2.SelectedIndex = 0;
        }
        private void CmdVerRec_Click(object sender, EventArgs e)
        {
            int n_idpro = 0;
            int n_row = 0;
            int n_posrec = 9999;
            int n_idrec = 0;

            if (Tab2.SelectedIndex == 0) { n_idpro = Convert.ToInt32(FgProd.GetData(FgProd.Row, 1)); }
            if (Tab2.SelectedIndex == 1) { n_idpro = Convert.ToInt32(FgInter.GetData(FgInter.Row, 1)); }
            if (Tab2.SelectedIndex == 2) { n_idpro = Convert.ToInt32(FgTodo.GetData(FgTodo.Row, 1)); }

            List<BE_PRO_PRODUCTOSRECETAS> l_recetas = new List<BE_PRO_PRODUCTOSRECETAS>();
            List<BE_PRO_PRODUCTOSRECETASINSUMOS> l_recetasins = new List<BE_PRO_PRODUCTOSRECETASINSUMOS>();
            SSF_NET_Produccion.CLS_Recetas o_recetas = new SSF_NET_Produccion.CLS_Recetas();
            CN_pro_productos o_prod = new CN_pro_productos();

            o_prod.mysConec = mysConec;
            o_prod.TraerRegistro(n_idpro);

            l_recetas = o_prod.lstRecetas;
            l_recetasins = o_prod.lstRecetasIns;

            for (n_row = 0; n_row <= l_recetas.Count - 1; n_row++)
            {
                if (l_recetas[n_row].n_prirec == 1)
                {
                    n_posrec = n_row;
                    n_idrec = l_recetas[n_row].n_id;
                    break;
                }
            }

            for (n_row = 0; n_row <= l_recetasins.Count - 1; n_row++)
            {
                if (l_recetasins[n_row].n_idrec != n_idrec)
                {
                    l_recetasins.RemoveAt(n_row);
                    n_row = n_row - 1;
                }
            }

            if (n_posrec == 9999)
            {
                MessageBox.Show("¡ El producto seleccionado no tiene receta principal asignada !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            o_recetas.dtItems = dtItems;
            o_recetas.dtTipExi = dtTipExi;
            o_recetas.dtUniMed = dtUniMed;
            o_recetas.e_Receta = l_recetas[n_posrec];
            o_recetas.l_RecetaIns = l_recetasins;
            o_recetas.VerRecetar();
            o_recetas = null;
        }
        private void eliminarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }
        private void desactivarPlanDeAbastecimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DesactivarPlanAbastecimiento();
        }
        void DesactivarPlanAbastecimiento()
        {
            int n_idreg = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            objCabecera.mysConec = mysConec;
            objCabecera.CambiarEstadoPlanAbastecimiento(n_idreg, 2);
            if (objCabecera.b_OcurrioError == false)
            {
                objCabecera.mysConec = mysConec;
                objCabecera.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO,0);
                dtCabecera = objCabecera.dtLista;
                ListarItems();

                MessageBox.Show("¡ El plan de abastecimiento se desactivo con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("¡ No se pudo desactivar el plan de abastecimiento por el siguiente motivo:" + objCabecera.c_ErrorMensaje.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        private void CmdCompras_Click(object sender, EventArgs e)
        {
            string c_valores = "";
            FrmComprasPeriodo xFrm = new FrmComprasPeriodo();
            int n_id = 0;
            if (Tab2.SelectedIndex == 0) { n_id = Convert.ToInt32(FgProdIns.GetData(FgProdIns.Row, 1)); c_valores = funFlex.Flex_CadenaIN_Fila(FgProdIns, FgProdIns.Row, 4, 16); }
            if (Tab2.SelectedIndex == 1) { n_id = Convert.ToInt32(FgInterIns.GetData(FgInterIns.Row, 1)); c_valores = funFlex.Flex_CadenaIN_Fila(FgInterIns, FgInterIns.Row, 4, 16); }
            if (Tab2.SelectedIndex == 2) { n_id = Convert.ToInt32(FgTodoIns.GetData(FgTodoIns.Row, 1)); c_valores = funFlex.Flex_CadenaIN_Fila(FgTodoIns, FgTodoIns.Row, 4, 16); }

            xFrm.c_valores = c_valores;
            xFrm.n_IdItem = n_id;
            xFrm.mysConec = mysConec;
            xFrm.STU_SISTEMA = STU_SISTEMA;
            xFrm.dtItems = dtItems;
            xFrm.ShowDialog();
        }
        void MostrarMeses(int n_MesInicio)
        {
            DataTable dtres = new DataTable();
            int n_col = 0;
            int n_col2 = 3;
            int n_mes = n_MesInicio;

            for (n_col = 1; n_col <= 12; n_col++)
            {
                dtres = funDatos.DataTableFiltrar(dtMeses, "n_id = " + n_mes.ToString() + "");
                arrCabeceraFlex1[n_col2, 0] = funFunciones.ConvertirTitulo(dtres.Rows[0]["c_des"].ToString());
                arrCabeceraFlex1[n_col2, 4] = funFunciones.ConvertirTitulo(dtres.Rows[0]["c_des"].ToString());
                n_mes = DateTime.Now.AddMonths(n_col).Month;
                n_col2 = n_col2 + 1;
            }
            funFlex.FlexMostrarDatos(FgProd, arrCabeceraFlex1, dtres, 2, false);
            funFlex.FlexMostrarDatos(FgInter, arrCabeceraFlex1, dtres, 2, false);
            funFlex.FlexMostrarDatos(FgTodo, arrCabeceraFlex1, dtres, 2, false);

            funFlex.FlexMostrarDatos(FgProdIns, arrCabeceraFlex1, dtres, 2, false);
            funFlex.FlexMostrarDatos(FgInterIns, arrCabeceraFlex1, dtres, 2, false);
            funFlex.FlexMostrarDatos(FgTodoIns, arrCabeceraFlex1, dtres, 2, false);
        }
        private void CboMesIni_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            MostrarCabeceraMes();
        }
        void MostrarCabeceraMes()
        {
            int n_col = 0;
            int n_posArray = 3;
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
                if (n_nummes == 13)
                {
                    n_nummes = 1;
                }
            }
            funFlex.FlexMostrarDatos(FgProd, arrCabeceraFlex1, dtDetalle, 2, false);
            funFlex.FlexMostrarDatos(FgInter, arrCabeceraFlex1, dtDetalle, 2, false);
            funFlex.FlexMostrarDatos(FgTodo, arrCabeceraFlex1, dtDetalle, 2, false);
        }

        private void CmdVerEstacion_Click(object sender, EventArgs e)
        {
            int n_idite = 0;
            if (Tab2.SelectedTab.TabIndex == 0) { n_idite = Convert.ToInt32(FgProd.GetData(FgProd.Row, 1)); }
            if (Tab2.SelectedTab.TabIndex == 1) { n_idite = Convert.ToInt32(FgInter.GetData(FgInter.Row, 1)); }
            if (Tab2.SelectedTab.TabIndex == 2) { n_idite = Convert.ToInt32(FgTodo.GetData(FgTodo.Row, 1)); }

            int n_idMatPri = 0;
            DataTable dtRes = new DataTable();

            CLS_Produccion o_pro = new CLS_Produccion();
            dtRes = funDatos.DataTableFiltrar(dtProducto, "n_id = " + n_idite + "");
            n_idMatPri = Convert.ToInt32(funFunciones.NulosN(dtRes.Rows[0]["n_idmatpri"]));
            if (n_idMatPri == 0)
            {
                MessageBox.Show("¡ El producto no tiene materia prima como insumo principal !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            o_pro.mysConec = mysConec;
            o_pro.VerEstacionalidad(n_idMatPri);
            o_pro = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void BtnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

                objCabecera.mysConec = mysConec;
                objCabecera.TraerRegistro(n_IdRegistro);

                BE_CABECERA = objCabecera.e_Cabecera;
                
                //dtPro
                var results = objCabecera.dtProducto.AsEnumerable().Select(row => new
                {
                    c_codpro = row["c_codpro"],
                    c_despro = row["c_despro"],
                    c_abrpre = row["c_abrpre"],
                    Enero = row["Enero"],
                    Febrero = row["Febrero"],
                    Marzo = row["Marzo"],
                    Abril = row["Abril"],
                    Mayo = row["Mayo"],
                    Junio = row["Junio"],
                    Julio = row["Julio"],
                    Agosto = row["Agosto"],
                    Setiembre = row["Setiembre"],
                    Octubre = row["Octubre"],
                    Noviembre = row["Noviembre"],
                    Diciembre = row["Diciembre"],
                    n_canpro = row["n_canpro"]
                }).ToList();
                string jsonResults = JsonConvert.SerializeObject(results);
                DataTable dtPro = JsonConvert.DeserializeObject<DataTable>(jsonResults);
                dtPro.Columns[0].ColumnName = "Codigo";
                dtPro.Columns[1].ColumnName = "Descripcion";
                dtPro.Columns[2].ColumnName = "Unidad";
                dtPro.Columns[15].ColumnName = "Total";

                //dtInter
                var results_dtInter = objCabecera.dtIntermedio.AsEnumerable().Select(row => new
                {
                    c_codpro = row["c_codpro"],
                    c_despro = row["c_despro"],
                    c_abrpre = row["c_abrpre"],
                    Enero = row["Enero"],
                    Febrero = row["Febrero"],
                    Marzo = row["Marzo"],
                    Abril = row["Abril"],
                    Mayo = row["Mayo"],
                    Junio = row["Junio"],
                    Julio = row["Julio"],
                    Agosto = row["Agosto"],
                    Setiembre = row["Setiembre"],
                    Octubre = row["Octubre"],
                    Noviembre = row["Noviembre"],
                    Diciembre = row["Diciembre"],
                    n_canpro = row["n_canpro"]
                }).ToList();
                string jsonResults_dtInter = JsonConvert.SerializeObject(results_dtInter);
                DataTable dtInter = JsonConvert.DeserializeObject<DataTable>(jsonResults_dtInter);
                dtInter.Columns[0].ColumnName = "Codigo";
                dtInter.Columns[1].ColumnName = "Descripcion";
                dtInter.Columns[2].ColumnName = "Unidad";
                dtInter.Columns[15].ColumnName = "Total";

                //dtTodo
                var results_dtTodo = objCabecera.dtTodo.AsEnumerable().Select(row => new
                {
                    c_codpro = row["c_codpro"],
                    c_despro = row["c_despro"],
                    c_abrpre = row["c_abrpre"],
                    Enero = row["Enero"],
                    Febrero = row["Febrero"],
                    Marzo = row["Marzo"],
                    Abril = row["Abril"],
                    Mayo = row["Mayo"],
                    Junio = row["Junio"],
                    Julio = row["Julio"],
                    Agosto = row["Agosto"],
                    Setiembre = row["Setiembre"],
                    Octubre = row["Octubre"],
                    Noviembre = row["Noviembre"],
                    Diciembre = row["Diciembre"],
                    n_canpro = row["n_canpro"]
                }).ToList();
                string jsonResults_dtTodo = JsonConvert.SerializeObject(results_dtTodo);
                DataTable dtTodo = JsonConvert.DeserializeObject<DataTable>(jsonResults_dtTodo);
                dtTodo.Columns[0].ColumnName = "Codigo";
                dtTodo.Columns[1].ColumnName = "Descripcion";
                dtTodo.Columns[2].ColumnName = "Unidad";
                dtTodo.Columns[15].ColumnName = "Total";

                //dtProIns
                var results_dtProIns = objCabecera.dtProductoIns.AsEnumerable().Select(i => new
                    {
                        c_codpro = i["c_codpro"],
                        c_despro = i["c_despro"],
                        c_abrpre = i["c_abrpre"],
                        Enero = i["Enero"],
                        Febrero = i["Febrero"],
                        Marzo = i["Marzo"],
                        Abril = i["Abril"],
                        Mayo = i["Mayo"],
                        Junio = i["Junio"],
                        Julio = i["Julio"],
                        Agosto = i["Agosto"],
                        Setiembre = i["Setiembre"],
                        Octubre = i["Octubre"],
                        Noviembre = i["Noviembre"],
                        Diciembre = i["Diciembre"],
                        n_canpro = i["n_canpro"]
                    }).ToList();
                string jsonResults_dtProIns = JsonConvert.SerializeObject(results_dtProIns);
                DataTable dtProIns = JsonConvert.DeserializeObject<DataTable>(jsonResults_dtProIns);
                dtProIns.Columns[0].ColumnName = "Codigo";
                dtProIns.Columns[1].ColumnName = "Descripcion";
                dtProIns.Columns[2].ColumnName = "Unidad";
                dtProIns.Columns[15].ColumnName = "Total";

                //dtInterIns
                var results_dtInterIns = objCabecera.dtIntermedioIns.AsEnumerable().Select(i => new
                {
                    c_codpro = i["c_codpro"],
                    c_despro = i["c_despro"],
                    c_abrpre = i["c_abrpre"],
                    Enero = i["Enero"],
                    Febrero = i["Febrero"],
                    Marzo = i["Marzo"],
                    Abril = i["Abril"],
                    Mayo = i["Mayo"],
                    Junio = i["Junio"],
                    Julio = i["Julio"],
                    Agosto = i["Agosto"],
                    Setiembre = i["Setiembre"],
                    Octubre = i["Octubre"],
                    Noviembre = i["Noviembre"],
                    Diciembre = i["Diciembre"],
                    n_canpro = i["n_canpro"]
                }).ToList();
                string jsonResults_dtInterIns = JsonConvert.SerializeObject(results_dtInterIns);
                DataTable dtInterIns = JsonConvert.DeserializeObject<DataTable>(jsonResults_dtInterIns);
                dtInterIns.Columns[0].ColumnName = "Codigo";
                dtInterIns.Columns[1].ColumnName = "Descripcion";
                dtInterIns.Columns[2].ColumnName = "Unidad";
                dtInterIns.Columns[15].ColumnName = "Total";

                //dtTodoIns
                var results_dtTodoIns = objCabecera.dtTodoIns.AsEnumerable().Select(i => new
                {
                    c_codpro = i["c_codpro"],
                    c_despro = i["c_despro"],
                    c_abrpre = i["c_abrpre"],
                    Enero = i["Enero"],
                    Febrero = i["Febrero"],
                    Marzo = i["Marzo"],
                    Abril = i["Abril"],
                    Mayo = i["Mayo"],
                    Junio = i["Junio"],
                    Julio = i["Julio"],
                    Agosto = i["Agosto"],
                    Setiembre = i["Setiembre"],
                    Octubre = i["Octubre"],
                    Noviembre = i["Noviembre"],
                    Diciembre = i["Diciembre"],
                    n_canpro = i["n_canpro"]
                }).ToList();
                string jsonResults_dtTodoIns = JsonConvert.SerializeObject(results_dtTodoIns);
                DataTable dtTodoIns = JsonConvert.DeserializeObject<DataTable>(jsonResults_dtTodoIns);
                dtTodoIns.Columns[0].ColumnName = "Codigo";
                dtTodoIns.Columns[1].ColumnName = "Descripcion";
                dtTodoIns.Columns[2].ColumnName = "Unidad";
                dtTodoIns.Columns[15].ColumnName = "Total";

                List<DataTable> tables = new List<DataTable>();
                dtPro.TableName = "Terminados";
                tables.Add(dtPro);
                dtInter.TableName = "Intermedios";
                tables.Add(dtInter);
                dtTodo.TableName = "Todo";
                tables.Add(dtTodo);
                dtProIns.TableName = "Insumos Terminados";
                tables.Add(dtProIns);
                dtInterIns.TableName = "Insumos Intermedios";
                tables.Add(dtInterIns);
                dtTodoIns.TableName = "Insumos Todo";
                tables.Add(dtTodoIns);

                funDbGrid.DT_List_ExporExcel(tables);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error el exportar los registros: {0} ", ex.Message), "Exportar Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
