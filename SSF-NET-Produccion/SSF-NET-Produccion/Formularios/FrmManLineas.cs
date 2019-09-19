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
    public partial class FrmManLineas : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public OleDbConnection AccConec = new OleDbConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        List<BE_PRO_LINEAS> lstLineas = new List<BE_PRO_LINEAS>();
        List<BE_PRO_LINEASDET> lstLineasDet = new List<BE_PRO_LINEASDET>();
        
        // OBJETOS LOCALES
        CN_pro_receta objReceta = new CN_pro_receta();
        CN_pro_lineas objRegistro = new CN_pro_lineas();
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
        DataTable dtItems = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtUniMed = new DataTable();
        DataTable dtReceta = new DataTable();
        DataTable dtLineas = new DataTable();

        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        int n_IdProducto;
        string[,] arrCabeceraDg1 = new string[9, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlexLin = new string[9, 5];
        string[,] arrCabeceraFlexLinDet = new string[15, 5];
        
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManLineas()
        {
            InitializeComponent();
        }

        private void FrmManLineas_Load(object sender, EventArgs e)
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
            //funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 614;
            this.Width = 932;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            // FLEX GRID DE LOS INSUMOS
            arrCabeceraFlexLin[0, 0] = "Descripcion";
            arrCabeceraFlexLin[0, 1] = "200";
            arrCabeceraFlexLin[0, 2] = "C";
            arrCabeceraFlexLin[0, 3] = "";
            arrCabeceraFlexLin[0, 4] = "c_codrec";

            arrCabeceraFlexLin[1, 0] = "Uni. Med.";
            arrCabeceraFlexLin[1, 1] = "60";
            arrCabeceraFlexLin[1, 2] = "C";
            arrCabeceraFlexLin[1, 3] = "";
            arrCabeceraFlexLin[1, 4] = "c_codrec";

            arrCabeceraFlexLin[2, 0] = "Materia Prima";
            arrCabeceraFlexLin[2, 1] = "100";
            arrCabeceraFlexLin[2, 2] = "C";
            arrCabeceraFlexLin[2, 3] = "";
            arrCabeceraFlexLin[2, 4] = "c_codrec";

            arrCabeceraFlexLin[3, 0] = "Uni. / Hora";
            arrCabeceraFlexLin[3, 1] = "60";
            arrCabeceraFlexLin[3, 2] = "C";
            arrCabeceraFlexLin[3, 3] = "";
            arrCabeceraFlexLin[3, 4] = "c_codrec";

            arrCabeceraFlexLin[4, 0] = "Operarios";
            arrCabeceraFlexLin[4, 1] = "60";
            arrCabeceraFlexLin[4, 2] = "N";
            arrCabeceraFlexLin[4, 3] = "";
            arrCabeceraFlexLin[4, 4] = "c_codrec";

            arrCabeceraFlexLin[5, 0] = "Eficiencia";
            arrCabeceraFlexLin[5, 1] = "60";
            arrCabeceraFlexLin[5, 2] = "N";
            arrCabeceraFlexLin[5, 3] = "";
            arrCabeceraFlexLin[5, 4] = "c_codrec";

            arrCabeceraFlexLin[6, 0] = "Activo";
            arrCabeceraFlexLin[6, 1] = "60";
            arrCabeceraFlexLin[6, 2] = "N";
            arrCabeceraFlexLin[6, 3] = "";
            arrCabeceraFlexLin[6, 4] = "c_codrec";

            arrCabeceraFlexLin[7, 0] = "ID";
            arrCabeceraFlexLin[7, 1] = "0";
            arrCabeceraFlexLin[7, 2] = "N";
            arrCabeceraFlexLin[7, 3] = "";
            arrCabeceraFlexLin[7, 4] = "c_codrec";

            funFlex.FlexMostrarDatos(FgLineas, arrCabeceraFlexLin, dtListar, 2, false);

            // FLEX GRID DE LAS TAREAS
            arrCabeceraFlexLinDet[0, 0] = "Orden";
            arrCabeceraFlexLinDet[0, 1] = "40";
            arrCabeceraFlexLinDet[0, 2] = "C";
            arrCabeceraFlexLinDet[0, 3] = "";
            arrCabeceraFlexLinDet[0, 4] = "c_codrec";

            arrCabeceraFlexLinDet[1, 0] = "Tarea";
            arrCabeceraFlexLinDet[1, 1] = "200";
            arrCabeceraFlexLinDet[1, 2] = "C";
            arrCabeceraFlexLinDet[1, 3] = "";
            arrCabeceraFlexLinDet[1, 4] = "c_codrec";

            arrCabeceraFlexLinDet[2, 0] = "Rdmto.";
            arrCabeceraFlexLinDet[2, 1] = "60";
            arrCabeceraFlexLinDet[2, 2] = "C";
            arrCabeceraFlexLinDet[2, 3] = "";
            arrCabeceraFlexLinDet[2, 4] = "c_codrec";

            arrCabeceraFlexLinDet[3, 0] = "Prod. Proc.";
            arrCabeceraFlexLinDet[3, 1] = "60";
            arrCabeceraFlexLinDet[3, 2] = "H";
            arrCabeceraFlexLinDet[3, 3] = "";
            arrCabeceraFlexLinDet[3, 4] = "c_codrec";

            arrCabeceraFlexLinDet[4, 0] = "Unidad / Hora";
            arrCabeceraFlexLinDet[4, 1] = "60";
            arrCabeceraFlexLinDet[4, 2] = "N";
            arrCabeceraFlexLinDet[4, 3] = "";
            arrCabeceraFlexLinDet[4, 4] = "c_codrec";

            arrCabeceraFlexLinDet[5, 0] = "Personal Ideal";
            arrCabeceraFlexLinDet[5, 1] = "60";
            arrCabeceraFlexLinDet[5, 2] = "N";
            arrCabeceraFlexLinDet[5, 3] = "";
            arrCabeceraFlexLinDet[5, 4] = "c_codrec";

            arrCabeceraFlexLinDet[6, 0] = "Prioridad";
            arrCabeceraFlexLinDet[6, 1] = "40";
            arrCabeceraFlexLinDet[6, 2] = "N";
            arrCabeceraFlexLinDet[6, 3] = "";
            arrCabeceraFlexLinDet[6, 4] = "c_codrec";

            arrCabeceraFlexLinDet[7, 0] = "Pers. / Linea";
            arrCabeceraFlexLinDet[7, 1] = "60";
            arrCabeceraFlexLinDet[7, 2] = "N";
            arrCabeceraFlexLinDet[7, 3] = "";
            arrCabeceraFlexLinDet[7, 4] = "c_codrec";

            arrCabeceraFlexLinDet[8 , 0] = "Efic. / Pers.";
            arrCabeceraFlexLinDet[8, 1] = "60";
            arrCabeceraFlexLinDet[8, 2] = "N";
            arrCabeceraFlexLinDet[8, 3] = "";
            arrCabeceraFlexLinDet[8, 4] = "c_codrec";

            arrCabeceraFlexLinDet[9, 0] = "Efic. / Tarea";
            arrCabeceraFlexLinDet[9, 1] = "60";
            arrCabeceraFlexLinDet[9, 2] = "N";
            arrCabeceraFlexLinDet[9, 3] = "";
            arrCabeceraFlexLinDet[9, 4] = "c_codrec";
            
            arrCabeceraFlexLinDet[10, 0] = "Avance Real";
            arrCabeceraFlexLinDet[10, 1] = "60";
            arrCabeceraFlexLinDet[10, 2] = "N";
            arrCabeceraFlexLinDet[10, 3] = "";
            arrCabeceraFlexLinDet[10, 4] = "c_codrec";

            arrCabeceraFlexLinDet[11, 0] = "Duracion Tarea";
            arrCabeceraFlexLinDet[11, 1] = "60";
            arrCabeceraFlexLinDet[11, 2] = "N";
            arrCabeceraFlexLinDet[11, 3] = "";
            arrCabeceraFlexLinDet[11, 4] = "c_codrec";

            arrCabeceraFlexLinDet[12, 0] = "Desvalance";
            arrCabeceraFlexLinDet[12, 1] = "60";
            arrCabeceraFlexLinDet[12, 2] = "N";
            arrCabeceraFlexLinDet[12, 3] = "";
            arrCabeceraFlexLinDet[12, 4] = "c_codrec";

            arrCabeceraFlexLinDet[13, 0] = "Intervalo";
            arrCabeceraFlexLinDet[13, 1] = "60";
            arrCabeceraFlexLinDet[13, 2] = "N";
            arrCabeceraFlexLinDet[13, 3] = "";
            arrCabeceraFlexLinDet[13, 4] = "c_codrec";

            arrCabeceraFlexLinDet[14, 0] = "Duracion Real";
            arrCabeceraFlexLinDet[14, 1] = "60";
            arrCabeceraFlexLinDet[14, 2] = "N";
            arrCabeceraFlexLinDet[14, 3] = "";
            arrCabeceraFlexLinDet[14, 4] = "c_codrec";

            funFlex.FlexMostrarDatos(FgLineasDet, arrCabeceraFlexLinDet, dtListar, 2, false);

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(31, ref arrCabeceraDg1);
        }
        void DataTableCargar()
        {
            objReceta.mysConec = mysConec;
            dtListar = objReceta.ListarRecetaLineas(STU_SISTEMA.EMPRESAID);

            //objRegistro.mysConec = mysConec;
            //dtListar = objRegistro.Listar(STU_SISTEMA.EMPRESAID);

            ObjItem.mysConec = mysConec;
            dtItems = ObjItem.Listar(STU_SISTEMA.EMPRESAID);

            objTareas.mysConec = mysConec;
            dtTareas = objTareas.Listar(STU_SISTEMA.EMPRESAID);

            objUniMed.mysConec = mysConec;
            dtUniMed = objUniMed.Listar();

            //objTipExi.mysConec = mysConec;
            //dtTipExi = objTipExi.Listar();

            objReceta.mysConec = mysConec;
            dtReceta = objReceta.Listar(STU_SISTEMA.EMPRESAID);                          // 

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(31);                             // INDICAMOS QUE ES EL FORMULARIO MAESTRO DE RECETAS
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
        void VerRegistro(int n_IdEmpresa, int n_IdReceta)
        {
            int n_row = 0;
            int n_fila = 0;
            string c_dato = "";
            DataTable dtResul = new DataTable();

            lstLineas.Clear();
            lstLineasDet.Clear();
            n_fila = 2;
            FgLineas.Rows.Count = 2;
            FgLineasDet.Rows.Count = 2;


            c_dato = funDatos.DataTableBuscar(dtReceta, "n_id", "c_codrec", n_IdReceta.ToString(), "N").ToString();
            TxtCodPro.Text = c_dato;

            c_dato = funDatos.DataTableBuscar(dtReceta, "n_id", "c_des", n_IdReceta.ToString(), "N").ToString();
            TxtProducto.Text = c_dato;

            objRegistro.mysConec = mysConec;
            objRegistro.obternerlineas(n_IdEmpresa, n_IdReceta);

            if (objRegistro.booOcurrioError == false)
            {
                lstLineas = objRegistro.lstLineas;
                lstLineasDet = objRegistro.lstLineasDet;

                if (lstLineas.Count == 0)
                { 
                    MessageBox.Show("La receta seleccionada no tiene lineas de produccion asignda", "", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                }
                else 
                { 
                    for (n_row = 0; n_row <= lstLineas.Count - 1; n_row++)
                    {
                        FgLineas.Rows.Count = FgLineas.Rows.Count + 1;

                        FgLineas.SetData(n_fila, 1, lstLineas[n_row].c_des.ToString());

                        c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_abr", lstLineas[n_row].n_idunimed.ToString(), "N").ToString();
                        FgLineas.SetData(n_fila, 2, c_dato);

                        FgLineas.SetData(n_fila, 3, lstLineas[n_row].n_can.ToString("0.00"));
                        FgLineas.SetData(n_fila, 4, lstLineas[n_row].n_kghor.ToString("0.00"));
                        FgLineas.SetData(n_fila, 5, lstLineas[n_row].n_numope.ToString("0.00"));
                        FgLineas.SetData(n_fila, 6, lstLineas[n_row].n_efi.ToString("0.00"));
                        FgLineas.SetData(n_fila, 7, lstLineas[n_row].n_activo);
                        FgLineas.SetData(n_fila, 8, lstLineas[n_row].n_id);
                        n_fila = n_fila + 1;
                    }
                    if (lstLineas.Count != 0)
                    { 
                        MostrarDatosLinea(lstLineas[0].n_id, 2);
                    }
                }
            }
        }
        void MostrarDatosLinea(int n_IdLinea, int n_IdFilaFgLineas)
        {
            int n_row = 0;
            int n_fila = 0;
            string c_dato = "";
            
            FgLineasDet.Rows.Count = 2;

            TxtNomRec.Text = FgLineas.GetData(n_IdFilaFgLineas, 1).ToString();

            // MOSTRAMOS LOS INSUMOS DE LA PRIMERA RECETA
            n_fila = 2;
            for (n_row = 0; n_row <= lstLineasDet.Count - 1; n_row++)
            {
                if (lstLineasDet[n_row].n_idlin == n_IdLinea)
                {
                    FgLineasDet.Rows.Count = FgLineasDet.Rows.Count + 1;

                    FgLineasDet.SetData(n_fila, 1, lstLineasDet[n_row].n_ord);

                    // DESCRIPCION DE LA TAREA
                    c_dato = funDatos.DataTableBuscar(dtTareas, "n_id", "c_des", lstLineasDet[n_row].n_idtar.ToString(), "N").ToString();
                    FgLineasDet.SetData(n_fila, 2, c_dato);

                    FgLineasDet.SetData(n_fila, 3, lstLineasDet[n_row].n_rdt.ToString("0.00"));
                    FgLineasDet.SetData(n_fila, 4, lstLineasDet[n_row].n_canpro.ToString("0.00"));
                    FgLineasDet.SetData(n_fila, 5, lstLineasDet[n_row].n_kghor.ToString("0.00"));
                    FgLineasDet.SetData(n_fila, 6, lstLineasDet[n_row].n_numopeide.ToString("0.00"));
                    FgLineasDet.SetData(n_fila, 7, lstLineasDet[n_row].n_pri.ToString("0.00"));
                    FgLineasDet.SetData(n_fila, 8, lstLineasDet[n_row].n_numope.ToString("0.00"));
                    FgLineasDet.SetData(n_fila, 9, lstLineasDet[n_row].n_efiper.ToString("0.00"));
                    FgLineasDet.SetData(n_fila, 10, lstLineasDet[n_row].n_efitar.ToString("0.00"));

                    FgLineasDet.SetData(n_fila, 11, lstLineasDet[n_row].n_canrea.ToString("0.00"));
                    FgLineasDet.SetData(n_fila, 12, lstLineasDet[n_row].n_durtarrea.ToString("0.00"));
                    FgLineasDet.SetData(n_fila, 13, lstLineasDet[n_row].n_desval.ToString("0.00"));
                    FgLineasDet.SetData(n_fila, 14, lstLineasDet[n_row].n_interv.ToString("0.00"));
                    FgLineasDet.SetData(n_fila, 15, lstLineasDet[n_row].n_durtarrea.ToString("0.00"));

                    n_fila = n_fila + 1;
                }
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

            FgLineas.Focus();
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

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(STU_SISTEMA.EMPRESAID, intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            FgLineas.Focus();
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DE LA RECETA PARA ELIMINAR LAS LINEAS

            objRegistro.mysConec = mysConec;
            objRegistro.obternerlineas(STU_SISTEMA.EMPRESAID, intIdRegistro);

            if (objRegistro.booOcurrioError == false)
            {
                lstLineas = objRegistro.lstLineas;
                lstLineasDet = objRegistro.lstLineasDet;
            }

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar las lineas de la receta seleccionada", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistro.Eliminar(lstLineas) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistro.mysConec = mysConec;
                    dtListar = objReceta.ListarRecetaLineas(STU_SISTEMA.EMPRESAID);
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistro.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
                //booResultado = objRegistro.Insertar(lstReceta, lstRecetaInsumo, lstRecetaTarea);
            }

            if (n_QueHace == 2)
            {
                //booResultado = objRegistro.Actualizar(lstReceta, lstRecetaInsumo, lstRecetaTarea);
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
                dtListar = objReceta.ListarRecetaLineas(STU_SISTEMA.EMPRESAID);
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
            objReceta = null;
            objRegistro = null;
            ObjItem = null;
            objTareas = null;
            objTipExi = null;
            objUniMed = null;
            objFormVis = null;
            objForm = null;
            objMeses = null;

            this.Close();
        }

        private void FrmManLineas_Activated(object sender, EventArgs e)
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

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            VerRegistro(STU_SISTEMA.EMPRESAID, intIdRegistro);
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
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
                
                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(STU_SISTEMA.EMPRESAID, intIdRegistro);
                    booAgregando = false;
                }
            }
        }

        private void FrmManLineas_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void FgLineas_RowColChange(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            MostrarDatosLinea(Convert.ToInt16(FgLineas.GetData(FgLineas.Row, 8)), FgLineas.Row);
        }

        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
