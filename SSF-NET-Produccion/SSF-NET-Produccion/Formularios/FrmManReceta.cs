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
using SIAC_Negocio.Produccion;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sunat;
using SIAC_Entidades.Produccion;
using SIAC_Entidades.Almacen;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using System.Data.OleDb;

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmManReceta : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public OleDbConnection AccConec = new OleDbConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        List<BE_PRO_RECETA> lstReceta = new List<BE_PRO_RECETA>();
        List<BE_PRO_RECETAINSUMO> lstRecetaInsumo = new List<BE_PRO_RECETAINSUMO>();
        List<BE_PRO_RECETATAREA> lstRecetaTarea = new List<BE_PRO_RECETATAREA>();

        // OBJETOS LOCALES
        CN_pro_receta objRegistro = new CN_pro_receta();
        CN_alm_inventario ObjItem = new CN_alm_inventario();
        CN_pro_tareas objTareas = new CN_pro_tareas();
        CN_sun_tipexi objTipExi = new CN_sun_tipexi();
        CN_sun_unimed objUniMed = new CN_sun_unimed();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_mae_meses objMeses = new CN_mae_meses();
        

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // DATATABLE LOCALES
        DataTable dtListar = new DataTable();
        DataTable dtTareas = new DataTable();
        DataTable dtResponsables = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtUniMed = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtTipExi = new DataTable();
        DataTable dtReceta = new DataTable();

        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[10, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlexIns = new string[5, 5];
        string[,] arrCabeceraFlexTar = new string[6, 5];
        string[,] arrCabeceraFlexRec = new string[8, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManReceta()
        {
            InitializeComponent();
        }

        private void FrmManReceta_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            Tab1.SelectedIndex = 0;
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();
            //funDatos.ComboBoxCargarDataTable(CboUniMed, dtPerAdj, "n_id", "c_apenom");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 605;
            this.Width = 931;
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;

            panel5.BackColor = Control.DefaultBackColor;
            panel6.BackColor = Control.DefaultBackColor;

            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            // FLEX GRID DE LOS INSUMOS
            arrCabeceraFlexIns[0, 0] = "Tipo Existencia";
            arrCabeceraFlexIns[0, 1] = "110";
            arrCabeceraFlexIns[0, 2] = "C";
            arrCabeceraFlexIns[0, 3] = "";
            arrCabeceraFlexIns[0, 4] = "c_codrec";

            arrCabeceraFlexIns[1, 0] = "Codigo";
            arrCabeceraFlexIns[1, 1] = "120";
            arrCabeceraFlexIns[1, 2] = "C";
            arrCabeceraFlexIns[1, 3] = "";
            arrCabeceraFlexIns[1, 4] = "c_codrec";

            arrCabeceraFlexIns[2, 0] = "Descripcion";
            arrCabeceraFlexIns[2, 1] = "345";
            arrCabeceraFlexIns[2, 2] = "C";
            arrCabeceraFlexIns[2, 3] = "";
            arrCabeceraFlexIns[2, 4] = "c_codrec";

            arrCabeceraFlexIns[3, 0] = "Uni. Med.";
            arrCabeceraFlexIns[3, 1] = "40";
            arrCabeceraFlexIns[3, 2] = "C";
            arrCabeceraFlexIns[3, 3] = "";
            arrCabeceraFlexIns[3, 4] = "c_codrec";

            arrCabeceraFlexIns[4, 0] = "Cantidad Teorica";
            arrCabeceraFlexIns[4, 1] = "75";
            arrCabeceraFlexIns[4, 2] = "N";
            arrCabeceraFlexIns[4, 3] = "";
            arrCabeceraFlexIns[4, 4] = "c_codrec";

            funFlex.FlexMostrarDatos(FgInsumos, arrCabeceraFlexIns, dtListar, 2, false);

            // FLEX GRID DE LAS TAREAS
            arrCabeceraFlexTar[0, 0] = "Codigo";
            arrCabeceraFlexTar[0, 1] = "120";
            arrCabeceraFlexTar[0, 2] = "C";
            arrCabeceraFlexTar[0, 3] = "";
            arrCabeceraFlexTar[0, 4] = "c_codrec";

            arrCabeceraFlexTar[1, 0] = "Descripcion";
            arrCabeceraFlexTar[1, 1] = "360";
            arrCabeceraFlexTar[1, 2] = "C";
            arrCabeceraFlexTar[1, 3] = "";
            arrCabeceraFlexTar[1, 4] = "c_codrec";

            arrCabeceraFlexTar[2, 0] = "Uni. Med.";
            arrCabeceraFlexTar[2, 1] = "40";
            arrCabeceraFlexTar[2, 2] = "C";
            arrCabeceraFlexTar[2, 3] = "";
            arrCabeceraFlexTar[2, 4] = "c_codrec";

            arrCabeceraFlexTar[3, 0] = "Arranca";
            arrCabeceraFlexTar[3, 1] = "60";
            arrCabeceraFlexTar[3, 2] = "H";
            arrCabeceraFlexTar[3, 3] = "";
            arrCabeceraFlexTar[3, 4] = "c_codrec";

            arrCabeceraFlexTar[4, 0] = "Costo";
            arrCabeceraFlexTar[4, 1] = "70";
            arrCabeceraFlexTar[4, 2] = "N";
            arrCabeceraFlexTar[4, 3] = "";
            arrCabeceraFlexTar[4, 4] = "c_codrec";

            arrCabeceraFlexTar[5, 0] = "Orden";
            arrCabeceraFlexTar[5, 1] = "40";
            arrCabeceraFlexTar[5, 2] = "N";
            arrCabeceraFlexTar[5, 3] = "";
            arrCabeceraFlexTar[5, 4] = "c_codrec";

            funFlex.FlexMostrarDatos(FgTarea, arrCabeceraFlexTar, dtListar, 2, false);
            
            // FLEX GRID DE LAS RECETAS
            arrCabeceraFlexRec[0, 0] = "Codigo";
            arrCabeceraFlexRec[0, 1] = "80";
            arrCabeceraFlexRec[0, 2] = "S";
            arrCabeceraFlexRec[0, 3] = "";
            arrCabeceraFlexRec[0, 4] = "c_codrec";

            arrCabeceraFlexRec[1, 0] = "Descripcion";
            arrCabeceraFlexRec[1, 1] = "280";
            arrCabeceraFlexRec[1, 2] = "C";
            arrCabeceraFlexRec[1, 3] = "";
            arrCabeceraFlexRec[1, 4] = "c_des";

            arrCabeceraFlexRec[2, 0] = "Uni. Med.";
            arrCabeceraFlexRec[2, 1] = "40";
            arrCabeceraFlexRec[2, 2] = "C";
            arrCabeceraFlexRec[2, 3] = "";
            arrCabeceraFlexRec[2, 4] = "c_itepredes";

            arrCabeceraFlexRec[3, 0] = "Cantidad";
            arrCabeceraFlexRec[3, 1] = "60";
            arrCabeceraFlexRec[3, 2] = "N";
            arrCabeceraFlexRec[3, 3] = "0.00";
            arrCabeceraFlexRec[3, 4] = "n_can";

            arrCabeceraFlexRec[4, 0] = "Prio ridad";
            arrCabeceraFlexRec[4, 1] = "40";
            arrCabeceraFlexRec[4, 2] = "N";
            arrCabeceraFlexRec[4, 3] = "0";
            arrCabeceraFlexRec[4, 4] = "n_prirec";

            arrCabeceraFlexRec[5, 0] = "Observaciones";
            arrCabeceraFlexRec[5, 1] = "150";
            arrCabeceraFlexRec[5, 2] = "C";
            arrCabeceraFlexRec[5, 3] = "";
            arrCabeceraFlexRec[5, 4] = "c_obs";

            arrCabeceraFlexRec[6, 0] = "IdReceta";
            arrCabeceraFlexRec[6, 1] = "0";
            arrCabeceraFlexRec[6, 2] = "N";
            arrCabeceraFlexRec[6, 3] = "0";
            arrCabeceraFlexRec[6, 4] = "n_id";

            arrCabeceraFlexRec[7, 0] = "Activo";
            arrCabeceraFlexRec[7, 1] = "40";
            arrCabeceraFlexRec[7, 2] = "B";
            arrCabeceraFlexRec[7, 3] = "";
            arrCabeceraFlexRec[7, 4] = "n_act";

            funFlex.FlexMostrarDatos(FgReceta, arrCabeceraFlexRec, dtListar, 2, false);

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(28, ref arrCabeceraDg1);
        }
        void DataTableCargar()
        {
            objRegistro.mysConec = mysConec;
            dtListar = objRegistro.ListarProductosConReceta(STU_SISTEMA.EMPRESAID);

            ObjItem.mysConec = mysConec;
            dtItems = ObjItem.Listar(STU_SISTEMA.EMPRESAID);

            objTareas.mysConec = mysConec;
            dtTareas = objTareas.Listar(STU_SISTEMA.EMPRESAID);

            objUniMed.mysConec = mysConec;
            dtUniMed = objUniMed.Listar();

            objTipExi.mysConec = mysConec;
            dtTipExi = objTipExi.Listar();

            //ObjReceta.AccConec = AccConec;
            //dtReceta = ObjReceta.ListarRecetas();                          // 

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(28);                             // INDICAMOS QUE ES EL FORMULARIO MAESTRO DE RECETAS
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtListar.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtListar, true);
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
        void VerRegistro(Int64 n_IdRegistro)
        {
            int n_row = 0;
            int n_fila = 0;
            string c_dato = "";
            DataTable dtResul = new DataTable();

            objRegistro.mysConec = mysConec;
            objRegistro.TraerRecetaProducto(n_IdRegistro);

            dtResul = funDatos.DataTableFiltrar(dtListar, "n_id = " + n_IdRegistro.ToString() + "");
            lstReceta = objRegistro.lstReceta;
            lstRecetaInsumo =objRegistro.lstRecetaInsumo;
            lstRecetaTarea = objRegistro.lstRecetaTarea;

            // MOSTRAMOS LOS DATOS DEL PRODUCTO
            TxtCodPro.Text = dtResul.Rows[0]["c_procod"].ToString();
            TxtProducto.Text = dtResul.Rows[0]["c_prodes"].ToString();
            TxtUniMed.Text = dtResul.Rows[0]["c_unimedabr"].ToString();
            // MOSTRAMOS LAS RECETAS DEL PRODUCTO
            FgReceta.Rows.Count = 2;
            n_fila = 2;
            if (lstReceta.Count != 0)
            { 
                for (n_row = 0; n_row <= lstReceta.Count - 1; n_row++)
                {
                    FgReceta.Rows.Count = FgReceta.Rows.Count + 1;
                    FgReceta.SetData(n_fila, 1, lstReceta[n_row].c_codrec);
                    FgReceta.SetData(n_fila, 2, lstReceta[n_row].c_des);
                
                    c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_abr", lstReceta[n_row].n_idunimed.ToString(), "N").ToString();
                    FgReceta.SetData(n_fila, 3, c_dato);
                    FgReceta.SetData(n_fila, 4, lstReceta[n_row].n_can.ToString("0.00"));
                    FgReceta.SetData(n_fila, 5, lstReceta[n_row].n_prirec);
                    FgReceta.SetData(n_fila, 6, lstReceta[n_row].c_obs);
                    FgReceta.SetData(n_fila, 7, lstReceta[n_row].n_id);                              // ID DE LA RECETA
                    FgReceta.SetData(n_fila, 8, lstReceta[n_row].n_act);                             // INDICA SI LA RECETA ESTA ACTIVA
                    n_fila = n_fila + 1;
                }
                MostrarDatosReceta(lstReceta[0].n_id);                                               // MOSTRAMOS LOS DATOS DE LA RECETA
            }

            Tab2.SelectedIndex = 0;
        }
        void MostrarDatosReceta(int n_idreceta)
        {
            int n_row = 0;
            int n_fila = 0;
            string c_dato = "";                     
            
            FgInsumos.Rows.Count = 2;
            FgTarea.Rows.Count = 2;

            lblNomReceta1.Text = FgReceta.GetData(FgReceta.Row, 2).ToString();
            lblNomReceta2.Text = FgReceta.GetData(FgReceta.Row, 2).ToString();

            // MOSTRAMOS LOS INSUMOS DE LA PRIMERA RECETA
            n_fila = 2;
            for (n_row = 0; n_row <= lstRecetaInsumo.Count - 1; n_row++)
            {
                if (lstRecetaInsumo[n_row].n_idrec == n_idreceta)
                {
                    FgInsumos.Rows.Count = FgInsumos.Rows.Count + 1;

                    // DESCRIPCION DEL TIPO DE ITEM
                    c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "n_idtipexi", lstRecetaInsumo[n_row].n_idite.ToString(), "N").ToString();
                    c_dato = funDatos.DataTableBuscar(dtTipExi, "n_id", "c_des", c_dato, "N").ToString();
                    FgInsumos.SetData(n_fila, 1, c_dato);

                    // CODIGO DEL PRODUCTO
                    c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_codpro", lstRecetaInsumo[n_row].n_idite.ToString(), "N").ToString();
                    FgInsumos.SetData(n_fila, 2, c_dato);

                    // DESCRIPCION DEL ITEM
                    c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", lstRecetaInsumo[n_row].n_idite.ToString(), "N").ToString();
                    FgInsumos.SetData(n_fila, 3, c_dato);

                    // UNIDAD DE MEDIDA
                    c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_abr", lstRecetaInsumo[n_row].n_idunimed.ToString(), "N").ToString();
                    FgInsumos.SetData(n_fila, 4, c_dato);

                    // CANTIDAD DEL ITEM
                    FgInsumos.SetData(n_fila, 5, lstRecetaInsumo[n_row].n_can.ToString("0.00000000"));
                    n_fila = n_fila + 1;
                }
            }

            // MOSTRAMOS LAS TAREAS DE LA RECETA
            n_fila = 2;
            for (n_row = 0; n_row <= lstRecetaTarea.Count - 1; n_row++)
            {
                if (lstRecetaTarea[n_row].n_idrec == n_idreceta)
                {
                    FgTarea.Rows.Count = FgTarea.Rows.Count + 1;

                    // CODIGO DE LA TAREA
                    c_dato = funDatos.DataTableBuscar(dtTareas, "n_id", "c_cod", lstRecetaTarea[n_row].n_idtar.ToString(), "N").ToString();
                    FgTarea.SetData(n_fila, 1, c_dato);

                    // DECSRIPCION DE LA TAREA
                    c_dato = funDatos.DataTableBuscar(dtTareas, "n_id", "c_des", lstRecetaTarea[n_row].n_idtar.ToString(), "N").ToString();
                    FgTarea.SetData(n_fila, 2, c_dato);

                    // DESCRIPCION DE LA UNIDAD DE MEDIDA
                    c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_abr", lstRecetaTarea[n_row].n_idunimed.ToString(), "N").ToString();
                    FgTarea.SetData(n_fila, 3, c_dato);

                    // CANTIDAD DE LA TAREA
                    FgTarea.SetData(n_fila, 4, lstRecetaTarea[n_row].n_can.ToString("0.000000"));

                    // CANTIDAD DEL ITEM
                    FgTarea.SetData(n_fila, 5, lstRecetaTarea[n_row].n_ord.ToString());

                    // OBSERVACIONES
                    //FgInsumos.SetData(n_fila, 4, lstRecetaTarea[n_row].c.ToString("0.00"));

                    n_fila = n_fila + 1;
                }
            }
            Tab2.SelectedIndex = 0;
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
                        
            CmdAddRec.Focus();
        }
        void Blanquea()
        {
        }
        void Bloquea()
        {
            //CboProd.Enabled = !CboProd.Enabled;
            //TxtReceta.Enabled = !TxtReceta.Enabled;
            //TxtNumLot.Enabled = !TxtNumLot.Enabled;
            //TxtUniMed.Enabled = !TxtUniMed.Enabled;
            //TxtCantidad.Enabled = !TxtCantidad.Enabled;
            //TxtFchIni.Enabled = !TxtFchIni.Enabled;
            //TxtFchFin.Enabled = !TxtFchFin.Enabled;
            //TxtHorIni.Enabled = !TxtHorIni.Enabled;
            //TxtHorFin.Enabled = !TxtHorFin.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolMenuImprimir.Enabled = !ToolMenuImprimir.Enabled;
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
            FgReceta.Focus();
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            //int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            //objRegistro.mysConec = mysConec;
            //BE_Revision = objRegistro.TraerRegistro(intIdRegistro);

            //DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            //if (DialogResult.Yes == Rpta)
            //{
            //    objRegistro.AccConec = AccConec;
            //    if (objRegistro.Eliminar(BE_Revision) == true)
            //    {
            //        booResult = true;
            //        MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            //        // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
            //        objRegistro.mysConec = mysConec;
            //        dtMovimientos = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
            //        // MOSTRAMOS LOS DATOS EN LA GRILLA
            //        ListarItems();
            //    }
            //    else
            //    {
            //        MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistro.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    }
            //}
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
                objRegistro.AccConec = AccConec;
                booResultado = objRegistro.Insertar(lstReceta, lstRecetaInsumo, lstRecetaTarea);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistro.Actualizar(lstReceta, lstRecetaInsumo, lstRecetaTarea);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ No se pudo guardar el registro por el siguiente motivo ! " + objRegistro.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {

            //BE_Revision.n_idemp = STU_SISTEMA.EMPRESAID;
            //BE_Revision.n_id = 0;
            //BE_Revision.n_idtipdoc = 74;
            //BE_Revision.c_numser = TxtNumSer.Text;
            //BE_Revision.c_numdoc = TxtCodPro.Text;
            //BE_Revision.n_idtipdocref = Convert.ToInt16(CboTipDoc.SelectedValue);
            //BE_Revision.n_iddocref = Convert.ToInt32(LblIdProducion.Text);
            //BE_Revision.c_numdocref = TxtProducto.Text;
            //BE_Revision.n_idpro = Convert.ToInt32(LblIdProducion.Text);
            ////BE_Revision.c_numdoc = TxtNumParteProd.Text;
            //BE_Revision.n_idpro = Convert.ToInt32(LblIdProducion.Text);
            //BE_Revision.n_idite = Convert.ToInt16(CboProducto.SelectedValue);
            //BE_Revision.n_idrec = Convert.ToInt16(CboReceta.SelectedValue);
            //BE_Revision.c_numlot = TxtNumLot.Text;
            //BE_Revision.n_canpro = Convert.ToDouble(funFunciones.NulosN(TxtCantidad.Text));
            //BE_Revision.n_idunimed = Convert.ToInt16(CboUniMed.SelectedValue);
            //BE_Revision.d_fchini = Convert.ToDateTime(TxtFchIni.Text);
            //BE_Revision.d_fchfin = Convert.ToDateTime(TxtFchFin.Text);
            //BE_Revision.h_horini = TxtHorIni.Text;
            //BE_Revision.h_horfin = TxtHorFin.Text;
            //BE_Revision.n_canprocon = Convert.ToDouble(funFunciones.NulosN(TxtCanProCon.Text));
            //BE_Revision.n_canpronocon = Convert.ToDouble(funFunciones.NulosN(TxtCanProNoCon.Text));
            //BE_Revision.c_obsprocon = TxtObsProCon.Text;
            //BE_Revision.c_obspronocon = TxtObsProNoCon.Text;
            //BE_Revision.n_idperrev = Convert.ToInt16(CboUniMed.SelectedValue);
            //BE_Revision.d_fchrev = Convert.ToDateTime(TxtFchRev.Text);
            //BE_Revision.h_horrev = TxtHorRev.Text;
        }
        bool CamposOK()
        {
            bool booEstado = true;

            //if (Convert.ToDouble(TxtCanProCon.Text) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado la cantidad de producto conforme !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    TxtCanProCon.Focus();
            //    booEstado = false;
            //    return booEstado;
            //}

            //if (Convert.ToDouble(funFunciones.NulosN(TxtCanProNoCon.Text)) != 0)
            //{
            //    if (TxtObsProNoCon.Text == "")
            //    {
            //        MessageBox.Show("¡ No ha especificado la observacion de producto no conforme !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //        TxtObsProNoCon.Focus();
            //        booEstado = false;
            //        return booEstado;
            //    }
            //}

            //if (Convert.ToInt16(CboUniMed.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado el nombre de la persona que realizo la revision del producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    CboUniMed.Focus();
            //    booEstado = false;
            //    return booEstado;
            //}

            //if (TxtFchRev.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado la fecha de revision !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    TxtCanProNoCon.Focus();
            //    booEstado = false;
            //    return booEstado;
            //}

            //if (TxtHorRev.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado la hora de revision !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    TxtObsProNoCon.Focus();
            //    booEstado = false;
            //    return booEstado;
            //}
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
                objRegistro.mysConec = mysConec;
                dtListar = objRegistro.Listar(STU_SISTEMA.EMPRESAID);
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
            objFormVis = null;

            this.Close();
        }

        private void FrmManReceta_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (dtListar.Rows.Count == 0)
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

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[5].CellValue(DgLista.Row).ToString());
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
                dtResult = funDbGrid.DG_Filtrar(dtListar, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
        }

        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (n_QueHace != 3) { return; }

            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[5].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }

        private void FrmManReceta_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
        }

        private void FgReceta_RowColChange(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            MostrarDatosReceta(Convert.ToInt16(FgReceta.GetData(FgReceta.Row, 7)));
        }

        private void toolmenuimpreceta_Click(object sender, EventArgs e)
        {

        }

        private void imprimirRecetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_pro_receta obtReceta = new CN_pro_receta();
            int n_idproducto;
            
            n_idproducto = Convert.ToInt16(DgLista.Columns[5].CellValue(DgLista.Row).ToString());
            obtReceta.STU_SISTEMA = STU_SISTEMA;
            obtReceta.ReporteRecetas(n_idproducto);
        }
    }
}
