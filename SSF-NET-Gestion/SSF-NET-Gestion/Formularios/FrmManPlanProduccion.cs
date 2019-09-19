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

namespace SSF_NET_Gestion.Formularios
{
    public partial class FrmManPlanProduccion : Form
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
        CN_pro_estacionalidad objEst = new CN_pro_estacionalidad();
        CN_ges_planproduccion objCabecera = new CN_ges_planproduccion();                            // CABECERA DEL REGISTRO
        //CN_ges_planventasdet objDetalle = new CN_ges_planventasdet();                       // DETALLE DEL REGISTRO

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_GES_PLANPRODUCCION BE_CABECERA = new BE_GES_PLANPRODUCCION();
        List<BE_GES_PLANPRODUCCIONDET> BE_DETALLE = new List<BE_GES_PLANPRODUCCIONDET>();

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
        DataTable dtEstacion = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[6, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[16, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890./" + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManPlanProduccion()
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
        }
        void ConfigurarFormulario()
        {
            this.Height = 600;
            this.Width = 1000;
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
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

            objCabecera.mysConec = mysConec;
            objCabecera.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            dtCabecera = objCabecera.dtLista;

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(94);

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(94, ref arrCabeceraDg1);

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

            objEst.mysConec = mysConec;
            dtEstacion = objEst.Listar(0, 0);
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtCabecera.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtCabecera, true);
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
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int n_idreg = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
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
            CN_ges_planventas o_plaven = new CN_ges_planventas();
            DataTable dtres = new DataTable();

            FgProd.Rows.Count = 2;
            FgInter.Rows.Count = 2;
            FgTodo.Rows.Count = 2;

            objCabecera.mysConec = mysConec;
            objCabecera.TraerRegistro(n_IdRegistro);

            BE_CABECERA = objCabecera.e_Cabecera;
            dtPro = objCabecera.dtProducto;
            dtInter = objCabecera.dtIntermedio;
            dtTodo = objCabecera.dtTodo;

            TxtDes.Text = BE_CABECERA.c_des;
            TxtFchIni.Text = Convert.ToDateTime(BE_CABECERA.d_fchini).ToString("dd/MM/yyyy");
            TxtFchFin.Text = Convert.ToDateTime(BE_CABECERA.d_fchfin).ToString("dd/MM/yyyy");
            LblIdPlaVen.Text = BE_CABECERA.n_idplaven.ToString();
            CboMesIni.SelectedValue = BE_CABECERA.n_mesini;

            o_plaven.mysConec = mysConec;
            o_plaven.TraerRegistro(BE_CABECERA.n_idplaven);
            dtres = o_plaven.dtCabecera;
            TxtPlaVen.Text = dtres.Rows[0]["c_des"].ToString();
            o_plaven = null;

            MostrarMeses(BE_CABECERA.n_mesini);
            FgProd.Rows.Count = 2;
            FgInter.Rows.Count = 2;
            FgTodo.Rows.Count = 2;

            funFlex.FlexMostrarDatos(FgProd, arrCabeceraFlex1, dtPro, 2, true);
            funFlex.FlexMostrarDatos(FgInter, arrCabeceraFlex1, dtInter, 2, true);
            funFlex.FlexMostrarDatos(FgTodo, arrCabeceraFlex1, dtTodo, 2, true);
            
            //llenarFlex(FgProd, dtPro);
            //llenarFlex(FgInter, dtInter);
            //llenarFlex(FgTodo, dtTodo);
            Tab2.SelectedIndex = 0;
        }
        //void llenarFlex(C1.Win.C1FlexGrid.C1FlexGrid fg_Control, DataTable dtDatos)
        //{
        //    DataTable dtres = dtDatos;
        //    int n_fil = 0;
        //    string c_dato = "";
        //    double n_valor = 0;
        //    for (n_fil = 0; n_fil <= dtres.Rows.Count - 1; n_fil++)
        //    {
        //        fg_Control.Rows.Count = fg_Control.Rows.Count + 1;

        //        c_dato = dtDatos.Rows[n_fil]["n_iditem"].ToString();
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 1, c_dato);

        //        c_dato = dtDatos.Rows[n_fil]["c_despro"].ToString();
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 2, c_dato);

        //        c_dato = dtDatos.Rows[n_fil]["c_abrpre"].ToString();
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 3, c_dato);

        //        n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["January"]);
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 4, n_valor.ToString("0.00"));

        //        n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["February"]);
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 5, n_valor.ToString("0.00"));

        //        n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["March"]);
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 6, n_valor.ToString("0.00"));

        //        n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["April"]);
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 7, n_valor.ToString("0.00"));

        //        n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["May"]);
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 8, n_valor.ToString("0.00"));

        //        n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["June"]);
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 9, n_valor.ToString("0.00"));

        //        n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["July"]);
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 10, n_valor.ToString("0.00"));

        //        n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["August"]);
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 11, n_valor.ToString("0.00"));

        //        n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["September"]);
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 12, n_valor.ToString("0.00"));

        //        n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["October"]);
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 13, n_valor.ToString("0.00"));

        //        n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["Novenber"]);
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 14, n_valor.ToString("0.00"));

        //        n_valor = Convert.ToDouble(dtDatos.Rows[n_fil]["December"]);
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 15, n_valor.ToString("0.00"));

        //        n_valor = funFlex.FlexSumarRow(fg_Control, fg_Control.Rows.Count - 1, 4, 15);
        //        fg_Control.SetData(fg_Control.Rows.Count - 1, 16, n_valor.ToString("0.00"));
        //    }
        //}
        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (n_QueHace != 3) { return; }

            if (e.NewIndex == 1)
            {
                int n_idreg = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(n_idreg);
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
                MessageBox.Show("¡ Existe un plan de produccion activo, debe de desactivar el plan activo para poder crear un nuevo plan !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
            MostrarMeses(Convert.ToInt16(CboMesIni.SelectedValue));
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

            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

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
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

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
            if (n_QueHace == 2) { n_id = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString()); }

            BE_DETALLE.Clear();

            BE_CABECERA.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_CABECERA.n_id = n_id;
            BE_CABECERA.c_des = TxtDes.Text;
            BE_CABECERA.d_fchini = Convert.ToDateTime("01/" + Convert.ToInt16(CboMesIni.SelectedValue).ToString() + "/" + STU_SISTEMA.ANOTRABAJO.ToString());             
            BE_CABECERA.d_fchfin = BE_CABECERA.d_fchini.AddMonths(12);            
            BE_CABECERA.n_mesini = Convert.ToInt16(CboMesIni.SelectedValue);
            BE_CABECERA.n_ano= Convert.ToDateTime(TxtFchIni.Text).Year;
            BE_CABECERA.n_idplaven = Convert.ToInt32(LblIdPlaVen.Text);
            BE_CABECERA.n_activo = 1;

            n_numele = FgProd.Rows.Count-1;
            n_numcol = FgProd.Cols.Count-2;
            for (n_fila = 2; n_fila <= n_numele; n_fila++)
            {
                for (n_col = 4; n_col <= n_numcol; n_col++)
                {
                    BE_GES_PLANPRODUCCIONDET e_detalle = new BE_GES_PLANPRODUCCIONDET();
                    e_detalle.n_idplapro = n_id;
                    e_detalle.n_idite = Convert.ToInt32(FgProd.GetData(n_fila,1));
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
                    BE_GES_PLANPRODUCCIONDET e_detalle = new BE_GES_PLANPRODUCCIONDET();
                    e_detalle.n_idplapro = n_id;
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

        private void FrmManPlanProduccion_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }

        private void FrmManPlanProduccion_Activated(object sender, EventArgs e)
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
        private void CmdAddValores_Click(object sender, EventArgs e)
        {
            if (LblIdPlaVen.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el plan de ventas !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtPlaVen.Focus();
                return;
            }

            CN_ges_planventas o_planven = new CN_ges_planventas();
            DataTable dtres = new DataTable();

            int n_idreg = Convert.ToInt16(LblIdPlaVen.Text);
            o_planven.mysConec = mysConec;
            o_planven.Consulta2(n_idreg);
            dtres = o_planven.dtLista;
            MostrarPlanVentas(dtres, Convert.ToInt16(CboMesIni.SelectedValue));
            HallarIntermedios2();
        }
        void HallarIntermedios2()
        {
            int n_vuelta = 0;
            DataTable dtresul = new DataTable();
            DataTable dtInter = new DataTable();
            string c_dato = funFlex.Flex_CadenaIN(FgProd, 1, 2);
            CN_pro_productosrecetas o_receta = new CN_pro_productosrecetas();
            o_receta.mysConec = mysConec;
            o_receta.Consulta1(c_dato);
            dtresul = o_receta.dtLista;
            dtInter = funDatos.DataTableAgregarDataTable(dtresul, dtInter, "n_vuelta = 99");
            n_vuelta = 1;
            ProcesarIntermedios(FgProd, ref dtInter, n_vuelta);
            MostrarIntermedios(dtInter, n_vuelta);
            
            do
            {
                dtresul = funDatos.DataTableFiltrar(dtInter, "n_vuelta = "+ n_vuelta.ToString() +"");
                n_vuelta = n_vuelta + 1;
                if (dtresul.Rows.Count != 0)
                {
                    ProcesarIntermedios(FgInter, ref dtInter, n_vuelta);
                    MostrarIntermedios(dtInter, n_vuelta);
                }
            } while (dtresul.Rows.Count != 0);
            dtInter = funDatos.DataTableFiltrar(dtInter, "n_idtipexi = 2");
            PintarIntermedios(dtInter);
        }
        void PintarIntermedios(DataTable dtDatos)
        {
            int n_row = 0;
            int n_fil = 0;
            string c_dato = "";
            int n_idpro = 0;
            bool b_encontro = false;
            DataTable dtRes = dtDatos;
            FgInter.Rows.Count = 2;
            double n_valor = 0;
            for (n_row = 0; n_row <= dtRes.Rows.Count - 1; n_row++)
            {
                //n_idpro = Convert.ToInt32(dtRes.Rows[n_row]["n_idpro"]);
                n_idpro = Convert.ToInt32(dtRes.Rows[n_row]["n_idite"]);
                b_encontro = false;
                for (n_fil = 2; n_fil <= FgInter.Rows.Count - 1; n_fil++)
                {
                    if (n_idpro == Convert.ToInt32(FgInter.GetData(n_fil, 1).ToString()))
                    {
                        n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 4)) + Convert.ToDouble(dtRes.Rows[n_row]["n_01"]);
                        FgInter.SetData(FgInter.Rows.Count - 1, 4, n_valor.ToString("0.00"));

                        n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 5)) + Convert.ToDouble(dtRes.Rows[n_row]["n_02"]);
                        FgInter.SetData(FgInter.Rows.Count - 1, 5, n_valor.ToString("0.00"));

                        n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 6)) + Convert.ToDouble(dtRes.Rows[n_row]["n_03"]);
                        FgInter.SetData(FgInter.Rows.Count - 1, 6, n_valor.ToString("0.00"));

                        n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 7)) + Convert.ToDouble(dtRes.Rows[n_row]["n_04"]);
                        FgInter.SetData(FgInter.Rows.Count - 1, 7, n_valor.ToString("0.00"));

                        n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 8)) + Convert.ToDouble(dtRes.Rows[n_row]["n_05"]);
                        FgInter.SetData(FgInter.Rows.Count - 1, 8, n_valor.ToString("0.00"));

                        n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 9)) + Convert.ToDouble(dtRes.Rows[n_row]["n_06"]);
                        FgInter.SetData(FgInter.Rows.Count - 1, 9, n_valor.ToString("0.00"));

                        n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 10)) + Convert.ToDouble(dtRes.Rows[n_row]["n_07"]);
                        FgInter.SetData(FgInter.Rows.Count - 1, 10, n_valor.ToString("0.00"));

                        n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 11)) + Convert.ToDouble(dtRes.Rows[n_row]["n_08"]);
                        FgInter.SetData(FgInter.Rows.Count - 1, 11, n_valor.ToString("0.00"));

                        n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 12)) + Convert.ToDouble(dtRes.Rows[n_row]["n_09"]);
                        FgInter.SetData(FgInter.Rows.Count - 1, 12, n_valor.ToString("0.00"));

                        n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 13)) + Convert.ToDouble(dtRes.Rows[n_row]["n_10"]);
                        FgInter.SetData(FgInter.Rows.Count - 1, 13, n_valor.ToString("0.00"));

                        n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 14)) + Convert.ToDouble(dtRes.Rows[n_row]["n_11"]);
                        FgInter.SetData(FgInter.Rows.Count - 1, 14, n_valor.ToString("0.00"));

                        n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 15)) + Convert.ToDouble(dtRes.Rows[n_row]["n_12"]);
                        FgInter.SetData(FgInter.Rows.Count - 1, 15, n_valor.ToString("0.00"));

                        n_valor = funFlex.FlexSumarRow(FgInter, FgInter.Rows.Count - 1, 4, 15);
                        FgInter.SetData(FgInter.Rows.Count - 1, 16, n_valor.ToString("0.00"));
                        b_encontro = true;
                    }
                }

                if (b_encontro == false)
                {
                    FgInter.Rows.Count = FgInter.Rows.Count + 1;

                    //c_dato = dtRes.Rows[n_row]["n_idpro"].ToString();
                    c_dato = dtRes.Rows[n_row]["n_idite"].ToString();
                    FgInter.SetData(FgInter.Rows.Count - 1, 1, c_dato);

                    c_dato = Convert.ToString(funDatos.DataTableBuscar(dtProducto, "n_id", "c_despro", c_dato, "N"));
                    FgInter.SetData(FgInter.Rows.Count - 1, 2, c_dato);

                    c_dato = dtRes.Rows[n_row]["c_abr"].ToString();
                    FgInter.SetData(FgInter.Rows.Count - 1, 3, c_dato);

                    n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 4)) + Convert.ToDouble(dtRes.Rows[n_row]["n_01"]);
                    FgInter.SetData(FgInter.Rows.Count - 1, 4, n_valor.ToString("0.00"));

                    n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 5)) + Convert.ToDouble(dtRes.Rows[n_row]["n_02"]);
                    FgInter.SetData(FgInter.Rows.Count - 1, 5, n_valor.ToString("0.00"));

                    n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 6)) + Convert.ToDouble(dtRes.Rows[n_row]["n_03"]);
                    FgInter.SetData(FgInter.Rows.Count - 1, 6, n_valor.ToString("0.00"));

                    n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 7)) + Convert.ToDouble(dtRes.Rows[n_row]["n_04"]);
                    FgInter.SetData(FgInter.Rows.Count - 1, 7, n_valor.ToString("0.00"));

                    n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 8)) + Convert.ToDouble(dtRes.Rows[n_row]["n_05"]);
                    FgInter.SetData(FgInter.Rows.Count - 1, 8, n_valor.ToString("0.00"));

                    n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 9)) + Convert.ToDouble(dtRes.Rows[n_row]["n_06"]);
                    FgInter.SetData(FgInter.Rows.Count - 1, 9, n_valor.ToString("0.00"));

                    n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 10)) + Convert.ToDouble(dtRes.Rows[n_row]["n_07"]);
                    FgInter.SetData(FgInter.Rows.Count - 1, 10, n_valor.ToString("0.00"));

                    n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 11)) + Convert.ToDouble(dtRes.Rows[n_row]["n_08"]);
                    FgInter.SetData(FgInter.Rows.Count - 1, 11, n_valor.ToString("0.00"));

                    n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 12)) + Convert.ToDouble(dtRes.Rows[n_row]["n_09"]);
                    FgInter.SetData(FgInter.Rows.Count - 1, 12, n_valor.ToString("0.00"));

                    n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 13)) + Convert.ToDouble(dtRes.Rows[n_row]["n_10"]);
                    FgInter.SetData(FgInter.Rows.Count - 1, 13, n_valor.ToString("0.00"));

                    n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 14)) + Convert.ToDouble(dtRes.Rows[n_row]["n_11"]);
                    FgInter.SetData(FgInter.Rows.Count - 1, 14, n_valor.ToString("0.00"));

                    n_valor = Convert.ToDouble(FgInter.GetData(n_fil, 15)) + Convert.ToDouble(dtRes.Rows[n_row]["n_12"]);
                    FgInter.SetData(FgInter.Rows.Count - 1, 15, n_valor.ToString("0.00"));

                    n_valor = funFlex.FlexSumarRow(FgInter, FgInter.Rows.Count - 1, 4, 15);
                    FgInter.SetData(FgInter.Rows.Count - 1, 16, n_valor.ToString("0.00"));
                }
            }

            int n_idmp = 0;
            double n_totpro = 0;
            double n_canmes = 0;
            int n_nummesabu = 0;
            // ACOMODAMOS DE ACUERDO A LA ESTACIONALIDAD
            for (n_fil = 2; n_fil <= FgInter.Rows.Count - 1; n_fil++)
            {
                n_idpro = Convert.ToInt32(funFunciones.NulosN(FgInter.GetData(n_fil, 1)));
                n_totpro = Convert.ToDouble(funFunciones.NulosN(FgInter.GetData(n_fil, 16)));
                // BUSCAMOS EL PRODUCTO EN LA TABLA DE PRODUCTOS PARA OBTENER LA MATERIA PRIMA DEL PRODUCTO
                dtRes = funDatos.DataTableFiltrar(dtProducto,"n_id = "+ n_idpro.ToString() +"");
                if (dtRes.Rows.Count != 0)
                {
                    n_idmp = Convert.ToInt32(funFunciones.NulosN(dtRes.Rows[0]["n_idmatpri"]));
                    if (n_idmp != 0)
                    { 
                        // BUSCAMOS SI LA MATERIA PRIMA DEL PRODUCTO TIENE ESTACIONALIDAD
                        dtRes = funDatos.DataTableFiltrar(dtEstacion, "n_idite = " + n_idmp.ToString() + "");
                        if (dtRes.Rows.Count != 0)
                        { 
                            n_nummesabu = NumMesAbundancia(dtRes);
                            n_canmes = (n_totpro/n_nummesabu);
                            // MOSTRAMOS LOS NUEVOS DATOS CALCULADOS DE ACUERDO A LA ESTACIONALIDAD
                            MostrarEstacionalidad(n_fil, n_canmes, dtRes);
                        }
                    }
                }
            }
        }
        void MostrarEstacionalidad(int n_Fila, double n_CantidadMensual, DataTable dtEstacionalidad)
        {
            FgInter.SetData(n_Fila, 4, "0.00");
            FgInter.SetData(n_Fila, 5, "0.00");
            FgInter.SetData(n_Fila, 6, "0.00");
            FgInter.SetData(n_Fila, 7, "0.00");
            FgInter.SetData(n_Fila, 8, "0.00");
            FgInter.SetData(n_Fila, 9, "0.00");
            FgInter.SetData(n_Fila, 10, "0.00");
            FgInter.SetData(n_Fila, 11, "0.00");
            FgInter.SetData(n_Fila, 12, "0.00");
            FgInter.SetData(n_Fila, 13, "0.00");
            FgInter.SetData(n_Fila, 14, "0.00");
            FgInter.SetData(n_Fila, 15, "0.00");

            int n_col = 0;
            int n_fgcol = 0;
            string c_mes = "";
            int n_valest = 0;

            for (n_col = 2; n_col <= dtEstacionalidad.Columns.Count - 1; n_col++)
            {
                c_mes = dtEstacionalidad.Columns[n_col].ColumnName;
                n_valest = Convert.ToInt16(dtEstacionalidad.Rows[0][n_col]);

                for (n_fgcol = 4; n_fgcol <= 15; n_fgcol++)
                {
                    if (c_mes.ToUpper() == FgInter.GetData(1, n_fgcol).ToString().ToUpper())
                    {
                        if (n_valest == 5)
                        {
                            FgInter.SetData(n_Fila, n_fgcol, n_CantidadMensual.ToString());
                        }
                        else
                        {
                            FgInter.SetData(n_Fila, n_fgcol, "");
                        }
                        break;
                    }
                    //else
                    //{
                    //    FgInter.SetData(n_Fila, n_fgcol, "0.00");
                    //    break;
                    //}
                }
            }
        }
        int NumMesAbundancia(DataTable dtEstacionalidad)
        {
            int n_nummes = 0;
            if (Convert.ToInt16(funFunciones.NulosN(dtEstacionalidad.Rows[0]["enero"])) == 5) { n_nummes = n_nummes + 1; }
            if (Convert.ToInt16(funFunciones.NulosN(dtEstacionalidad.Rows[0]["febrero"])) == 5) { n_nummes = n_nummes + 1; }
            if (Convert.ToInt16(funFunciones.NulosN(dtEstacionalidad.Rows[0]["marzo"])) == 5) { n_nummes = n_nummes + 1; }
            if (Convert.ToInt16(funFunciones.NulosN(dtEstacionalidad.Rows[0]["abril"])) == 5) { n_nummes = n_nummes + 1; }
            if (Convert.ToInt16(funFunciones.NulosN(dtEstacionalidad.Rows[0]["mayo"])) == 5) { n_nummes = n_nummes + 1; }
            if (Convert.ToInt16(funFunciones.NulosN(dtEstacionalidad.Rows[0]["junio"])) == 5) { n_nummes = n_nummes + 1; }
            if (Convert.ToInt16(funFunciones.NulosN(dtEstacionalidad.Rows[0]["julio"])) == 5) { n_nummes = n_nummes + 1; }
            if (Convert.ToInt16(funFunciones.NulosN(dtEstacionalidad.Rows[0]["agosto"])) == 5) { n_nummes = n_nummes + 1; }
            if (Convert.ToInt16(funFunciones.NulosN(dtEstacionalidad.Rows[0]["setiembre"])) == 5) { n_nummes = n_nummes + 1; }
            if (Convert.ToInt16(funFunciones.NulosN(dtEstacionalidad.Rows[0]["octubre"])) == 5) { n_nummes = n_nummes + 1; }
            if (Convert.ToInt16(funFunciones.NulosN(dtEstacionalidad.Rows[0]["noviembre"])) == 5) { n_nummes = n_nummes + 1; }
            if (Convert.ToInt16(funFunciones.NulosN(dtEstacionalidad.Rows[0]["diciembre"])) == 5) { n_nummes = n_nummes + 1; }
            return n_nummes;
        }
        void MostrarIntermedios(DataTable dtDatos, int n_Vuelta)
        {
            int n_row = 0;
            string c_dato = "";
            DataTable dtRes = funDatos.DataTableFiltrar(dtDatos, "n_vuelta = " + n_Vuelta.ToString() + "");
            FgInter.Rows.Count = 2;

            for (n_row = 0; n_row <= dtRes.Rows.Count - 1; n_row++)
            {
                FgInter.Rows.Count = FgInter.Rows.Count + 1;

                c_dato = dtRes.Rows[n_row]["n_idite"].ToString();
                FgInter.SetData(FgInter.Rows.Count - 1, 1, c_dato);

                c_dato = dtRes.Rows[n_row]["c_despro"].ToString();
                FgInter.SetData(FgInter.Rows.Count - 1, 2, c_dato);

                c_dato = dtRes.Rows[n_row]["c_abr"].ToString();
                FgInter.SetData(FgInter.Rows.Count - 1, 3, c_dato);

                c_dato = Convert.ToDouble(dtRes.Rows[n_row]["n_01"]).ToString("0.00");
                FgInter.SetData(FgInter.Rows.Count - 1, 4, c_dato);

                c_dato = Convert.ToDouble(dtRes.Rows[n_row]["n_02"]).ToString("0.00");
                FgInter.SetData(FgInter.Rows.Count - 1, 5, c_dato);

                c_dato = Convert.ToDouble(dtRes.Rows[n_row]["n_03"]).ToString("0.00");
                FgInter.SetData(FgInter.Rows.Count - 1, 6, c_dato);

                c_dato = Convert.ToDouble(dtRes.Rows[n_row]["n_04"]).ToString("0.00");
                FgInter.SetData(FgInter.Rows.Count - 1, 7, c_dato);

                c_dato = Convert.ToDouble(dtRes.Rows[n_row]["n_05"]).ToString("0.00");
                FgInter.SetData(FgInter.Rows.Count - 1, 8, c_dato);

                c_dato = Convert.ToDouble(dtRes.Rows[n_row]["n_06"]).ToString("0.00");
                FgInter.SetData(FgInter.Rows.Count - 1, 9, c_dato);

                c_dato = Convert.ToDouble(dtRes.Rows[n_row]["n_07"]).ToString("0.00");
                FgInter.SetData(FgInter.Rows.Count - 1, 10, c_dato);

                c_dato = Convert.ToDouble(dtRes.Rows[n_row]["n_08"]).ToString("0.00");
                FgInter.SetData(FgInter.Rows.Count - 1, 11, c_dato);

                c_dato = Convert.ToDouble(dtRes.Rows[n_row]["n_09"]).ToString("0.00");
                FgInter.SetData(FgInter.Rows.Count - 1, 12, c_dato);

                c_dato = Convert.ToDouble(dtRes.Rows[n_row]["n_10"]).ToString("0.00");
                FgInter.SetData(FgInter.Rows.Count - 1, 13, c_dato);

                c_dato = Convert.ToDouble(dtRes.Rows[n_row]["n_11"]).ToString("0.00");
                FgInter.SetData(FgInter.Rows.Count - 1, 14, c_dato);

                c_dato = Convert.ToDouble(dtRes.Rows[n_row]["n_12"]).ToString("0.00");
                FgInter.SetData(FgInter.Rows.Count - 1, 15, c_dato);
            }
        }
        void ProcesarIntermedios(C1.Win.C1FlexGrid.C1FlexGrid fg_Control, ref DataTable dtInter, int n_vuelta)
        {
            DataTable dtres = new DataTable();
            int n_row = 0;
            int n_fil = 0;
            int n_idpro = 0;
            double n_canins = 0;
            for (n_row = 2; n_row <= fg_Control.Rows.Count - 1; n_row++)
            {
                n_idpro = Convert.ToInt32(fg_Control.GetData(n_row, 1));
                dtres = funDatos.DataTableFiltrar(dtRecetas,"n_idpro = "+ n_idpro.ToString() + "");
                if (dtres.Rows.Count!=0)
                {
                    for (n_fil = 0; n_fil <= dtres.Rows.Count - 1; n_fil++)
                    { 
                        DataRow dtrow = dtInter.NewRow();
                        dtrow["n_idpro"] = n_idpro;
                        //dtrow["n_idpro"] = Convert.ToInt32(dtres.Rows[n_fil]["n_idite"]); 
                        dtrow["n_idite"] = Convert.ToInt32(dtres.Rows[n_fil]["n_idite"]);
                        //dtrow["n_idite"] = 0;
                        dtrow["n_idunimed"] = Convert.ToInt32(dtres.Rows[n_fil]["n_idunimed"]);
                        dtrow["n_idtipexi"] = Convert.ToInt32(dtres.Rows[n_fil]["n_idtipexi"]);
                        dtrow["c_despro"] = dtres.Rows[n_fil]["c_despro"].ToString();
                        dtrow["c_codpro"] = dtres.Rows[n_fil]["c_codpro"].ToString();
                        dtrow["c_abr"] = dtres.Rows[n_fil]["c_abr"].ToString();

                        n_canins = Convert.ToDouble(dtres.Rows[n_fil]["n_can"]);
                        dtrow["n_can"] = n_canins;
                        dtrow["n_01"] = Convert.ToDouble(fg_Control.GetData(n_row, 4)) * n_canins;
                        dtrow["n_02"] = Convert.ToDouble(fg_Control.GetData(n_row, 5)) * n_canins;
                        dtrow["n_03"] = Convert.ToDouble(fg_Control.GetData(n_row, 6)) * n_canins;
                        dtrow["n_04"] = Convert.ToDouble(fg_Control.GetData(n_row, 7)) * n_canins;
                        dtrow["n_05"] = Convert.ToDouble(fg_Control.GetData(n_row, 8)) * n_canins;
                        dtrow["n_06"] = Convert.ToDouble(fg_Control.GetData(n_row, 9)) * n_canins;
                        dtrow["n_07"] = Convert.ToDouble(fg_Control.GetData(n_row, 10)) * n_canins;
                        dtrow["n_08"] = Convert.ToDouble(fg_Control.GetData(n_row, 11)) * n_canins;
                        dtrow["n_09"] = Convert.ToDouble(fg_Control.GetData(n_row, 12)) * n_canins;
                        dtrow["n_10"] = Convert.ToDouble(fg_Control.GetData(n_row, 13)) * n_canins;
                        dtrow["n_11"] = Convert.ToDouble(fg_Control.GetData(n_row, 14)) * n_canins;
                        dtrow["n_12"] = Convert.ToDouble(fg_Control.GetData(n_row, 15)) * n_canins;
                        dtrow["n_vuelta"] = n_vuelta;
                        dtInter.Rows.Add(dtrow);
                    }
                }
            }
        }
        void MostrarPlanVentas(DataTable dtDatos, int n_MesInicio)
        {
            FgProd.Rows.Count = 2;
            DataTable dtres = new DataTable();
            DataTable dtmes = new DataTable();
            int n_col = 0;
            int n_col2 = 3;
            int n_mes = n_MesInicio;

            dtmes = funDatos.DataTableFiltrar(dtMeses, "n_id NOT IN(0,13)");
            for (n_col = 1; n_col <= 12; n_col++)
            {
                dtres = funDatos.DataTableFiltrar(dtmes, "n_id = " + n_mes.ToString() + "");
                arrCabeceraFlex1[n_col2, 0] = funFunciones.ConvertirTitulo(dtres.Rows[0]["c_des"].ToString());
                arrCabeceraFlex1[n_col2, 4] = funFunciones.ConvertirTitulo(dtres.Rows[0]["c_des"].ToString());
                n_mes = DateTime.Now.AddMonths(n_col).Month;
                n_col2 = n_col2 + 1;
            }
            funFlex.FlexMostrarDatos(FgProd, arrCabeceraFlex1, dtDatos, 2, true);
        }
        private void CmdBusCli_Click(object sender, EventArgs e)
        {
            DataTable dtres = new DataTable();
            CN_ges_planproduccion o_plan = new CN_ges_planproduccion();
            CN_ges_planventas o_planven = new CN_ges_planventas();

            o_plan.mysConec = mysConec;
            o_plan.BuscarPlanVentasActivo(STU_SISTEMA.EMPRESAID, 1);
            dtres = o_plan.dtLista;
            if (dtres == null) { return; }
            if (dtres.Rows.Count == 0) { return; }
            LblIdPlaVen.Text = Convert.ToInt16(dtres.Rows[0]["n_id"]).ToString();
            TxtPlaVen.Text = dtres.Rows[0]["c_des"].ToString();
            MostrarMeses(Convert.ToInt16(dtres.Rows[0]["n_idmesini"]));
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
        private void FrmManPlanProduccion_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }
        private void CmdVerEstaciona_Click(object sender, EventArgs e)
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
            o_pro.mysConec = mysConec;
            o_pro.VerEstacionalidad(n_idMatPri);
            o_pro = null;
        }
        private void eliminarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }
        private void desactivarPlanDeProduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DesactivarPlanVentas();
        }
        void DesactivarPlanVentas()
        {
            int n_idreg = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            objCabecera.mysConec = mysConec;
            objCabecera.CambiarEstadoPlanProduccion(n_idreg, 2);
            if (objCabecera.b_OcurrioError == false)
            {
                objCabecera.mysConec = mysConec;
                objCabecera.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, 0);
                dtCabecera = objCabecera.dtLista;
                ListarItems();

                MessageBox.Show("¡ El plan de produccion se desactivo con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("¡ No se pudo desactivar el plan de produccion por el siguiente motivo:" + objCabecera.c_ErrorMensaje.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
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
            int n_nummes = Convert.ToInt16(CboMesIni.SelectedValue);
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
    }
}

