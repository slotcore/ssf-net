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
using SIAC_Negocio.Logistica;
using SIAC_Negocio.Ventas;
using SIAC_Entidades.Produccion;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Logistica;
using SIAC_Entidades.Ventas;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using System.Data.OleDb;
using SSF_NET_Produccion.ViewModels;
using SSF_NET_Planillas.Formularios;

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmOrdenProduccion : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public OleDbConnection AccConec = new OleDbConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        BE_PRO_ORDENPRODUCCION entOrdenProd = new BE_PRO_ORDENPRODUCCION();
        List<BE_PRO_ORDENPRODUCCIONDET> lstOrdenProdDet = new List<BE_PRO_ORDENPRODUCCIONDET>();
        
        // OBJETOS LOCALES
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_pro_ordenproduccion objRegistro = new CN_pro_ordenproduccion();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_alm_inventario ObjItem = new CN_alm_inventario();
        CN_mae_prioridad objPrioridad = new CN_mae_prioridad();
        CN_pro_ordenproduccion objLinea = new CN_pro_ordenproduccion();
        CN_log_ordenrequerimiento objRequerimiento = new CN_log_ordenrequerimiento();
        CN_vta_pedidocli objPedido = new CN_vta_pedidocli();
        CN_pro_productos objProductos = new CN_pro_productos();
        CN_pro_personal objPerPro = new CN_pro_personal();

        CN_pro_tareas objtareas = new CN_pro_tareas();
        //CN_sun_unimed objUniMed = new CN_sun_unimed();
        CN_alm_inventariounimed objUniMed = new CN_alm_inventariounimed();                                    // CARGAMOS LA UNIDAD DE MEDIDA DEL INVENTARIO PORQUE LA ORDEN DE REQUETRIMIENTO Y PEDIDO UTILIZAN ESA UNIDAD DE MEDIDA
        CN_pro_productosrecetas objReceta = new CN_pro_productosrecetas();
        
        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // DATATABLE LOCALES
        DataTable dtForm = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtListar = new DataTable();
        DataTable dtTipDoc = new DataTable();
        DataTable dtNumDocRef = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtPerPro = new DataTable();
        DataTable dtPrioridad = new DataTable();
        DataTable dtTareas = new DataTable();
        DataTable dtUniMed = new DataTable();
        DataTable dtReceta = new DataTable();
        DataTable dtLinea = new DataTable();
        DataTable dtTipoDocumento = new DataTable();
        DataTable dtRequerimiento = new DataTable();
        DataTable dtPedido = new DataTable();

        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[10, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlexLisPro = new string[8, 5];
        string[,] arrCabeceraFlexCroPro = new string[5, 5];
                
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmOrdenProduccion()
        {
            InitializeComponent();
        }
        private void label12_Click(object sender, EventArgs e)
        {
        }
        private void FrmOrdenProduccion_Load(object sender, EventArgs e)
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

            funDatos.ComboBoxCargarDataTable(CboTipDocRef, dtTipoDocumento, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboPri, dtPrioridad, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboRes, dtPerPro, "n_id", "c_apenom");
        }
        void ConfigurarFormulario()
        {
            //this.Height = 631;
            //this.Width = 945;
             
            //Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            //Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            // FLEX GRID DE LOS INSUMOS
            arrCabeceraFlexLisPro[0, 0] = "Producto";
            arrCabeceraFlexLisPro[0, 1] = "260";
            arrCabeceraFlexLisPro[0, 2] = "C";
            arrCabeceraFlexLisPro[0, 3] = "";
            arrCabeceraFlexLisPro[0, 4] = "c_despro";

            arrCabeceraFlexLisPro[1, 0] = "Receta";
            arrCabeceraFlexLisPro[1, 1] = "250";
            arrCabeceraFlexLisPro[1, 2] = "C";
            arrCabeceraFlexLisPro[1, 3] = "";
            arrCabeceraFlexLisPro[1, 4] = "c_codrec";

            arrCabeceraFlexLisPro[2, 0] = "Uni Med.";
            arrCabeceraFlexLisPro[2, 1] = "60";
            arrCabeceraFlexLisPro[2, 2] = "C";
            arrCabeceraFlexLisPro[2, 3] = "";
            arrCabeceraFlexLisPro[2, 4] = "c_unimed";

            arrCabeceraFlexLisPro[3, 0] = "Cantidad";
            arrCabeceraFlexLisPro[3, 1] = "70";
            arrCabeceraFlexLisPro[3, 2] = "80";
            arrCabeceraFlexLisPro[3, 3] = "";
            arrCabeceraFlexLisPro[3, 4] = "n_can";

            arrCabeceraFlexLisPro[4, 0] = "Fch. Entrega";
            arrCabeceraFlexLisPro[4, 1] = "70";
            arrCabeceraFlexLisPro[4, 2] = "F";
            arrCabeceraFlexLisPro[4, 3] = "";
            arrCabeceraFlexLisPro[4, 4] = "d_fchent";

            arrCabeceraFlexLisPro[5, 0] = "Nº Armadas";
            arrCabeceraFlexLisPro[5, 1] = "50";
            arrCabeceraFlexLisPro[5, 2] = "N";
            arrCabeceraFlexLisPro[5, 3] = "";
            arrCabeceraFlexLisPro[5, 4] = "n_numarm";

            arrCabeceraFlexLisPro[6, 0] = "id Producto";
            arrCabeceraFlexLisPro[6, 1] = "0";
            arrCabeceraFlexLisPro[6, 2] = "N";
            arrCabeceraFlexLisPro[6, 3] = "";
            arrCabeceraFlexLisPro[6, 4] = "n_idpro";

            arrCabeceraFlexLisPro[7, 0] = "id RECETA";
            arrCabeceraFlexLisPro[7, 1] = "0";
            arrCabeceraFlexLisPro[7, 2] = "N";
            arrCabeceraFlexLisPro[7, 3] = "";
            arrCabeceraFlexLisPro[7, 4] = "n_idrec";

            funFlex.FlexMostrarDatos(FgLisPro, arrCabeceraFlexLisPro, dtListar, 2, false);


            // FLEX GRID DE LAS TAREAS
            arrCabeceraFlexCroPro[0, 0] = "Producto";
            arrCabeceraFlexCroPro[0, 1] = "350";
            arrCabeceraFlexCroPro[0, 2] = "C";
            arrCabeceraFlexCroPro[0, 3] = "";
            arrCabeceraFlexCroPro[0, 4] = "";

            arrCabeceraFlexCroPro[1, 0] = "Uni. Med.";
            arrCabeceraFlexCroPro[1, 1] = "60";
            arrCabeceraFlexCroPro[1, 2] = "C";
            arrCabeceraFlexCroPro[1, 3] = "";
            arrCabeceraFlexCroPro[1, 4] = "";

            arrCabeceraFlexCroPro[2, 0] = "Cantidad";
            arrCabeceraFlexCroPro[2, 1] = "50";
            arrCabeceraFlexCroPro[2, 2] = "N";
            arrCabeceraFlexCroPro[2, 3] = "";
            arrCabeceraFlexCroPro[2, 4] = "";

            arrCabeceraFlexCroPro[3, 0] = "Fch. Produccion";
            arrCabeceraFlexCroPro[3, 1] = "65";
            arrCabeceraFlexCroPro[3, 2] = "F";
            arrCabeceraFlexCroPro[3, 3] = "";
            arrCabeceraFlexCroPro[3, 4] = "";

            arrCabeceraFlexCroPro[4, 0] = "Supervisor";
            arrCabeceraFlexCroPro[4, 1] = "200";
            arrCabeceraFlexCroPro[4, 2] = "C";
            arrCabeceraFlexCroPro[4, 3] = "";
            arrCabeceraFlexCroPro[4, 4] = "c_codrec";

            //funFlex.FlexMostrarDatos(fgCroPro, arrCabeceraFlexCroPro, dtListar, 2, false);


            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(36, ref arrCabeceraDg1);
        }
        void DataTableCargar()
        {
            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(36);                             // INDICAMOS QUE ES EL FORMULARIO MAESTRO DE RECETAS

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();
            
            objRegistro.mysConec = mysConec;
            dtListar = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);

            ObjItem.mysConec = mysConec;
            dtItems = ObjItem.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);

            objUniMed.mysConec = mysConec;
            dtUniMed = objUniMed.Listar();

            objPrioridad.mysConec = mysConec;
            dtPrioridad = objPrioridad.Listar(27);                              // LE PASAMOS EL ID DEL FORMULARIO
            
            objtareas.mysConec = mysConec;
            dtTareas = objtareas.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objReceta.mysConec = mysConec;
            if (objReceta.ListarTodasRecetas(STU_SISTEMA.EMPRESAID) == true)
            {
                dtReceta = objReceta.dtRecetas;
            }          
            
            objTipDoc.mysConec = mysConec;
            dtTipoDocumento = objTipDoc.Listar();

            DataTable dtResult = dtTipoDocumento;
            // FILTRANOS SOLO LOS DOCUMENTOS DE ORDEN DE REQUERIMIENTOM, PEDIDO DE CLIENTE
            dtTipoDocumento = dtResult.Select("n_id IN (75,77)").CopyToDataTable();
            
            // CARGAMOS EL PERSONAL DE PRODUCCION
            objPerPro.mysConec = mysConec;
            if (objPerPro.Listar(STU_SISTEMA.EMPRESAID) == true)
            {
                dtPerPro = objPerPro.dtPersonal;
            }

            // CARGAMOS LAS ORDENES DE REQUERIMIENTO
            objRequerimiento.mysConec = mysConec;
            if (objRequerimiento.ListarTodoRequerimientos(STU_SISTEMA.EMPRESAID) == true)
            {
                dtRequerimiento = objRequerimiento.dtListaOrdenReq;
            }

            // CARGAMOS LOS PEDIDOS DEL CLIENTE
            objPedido.mysConec = mysConec;
            if (objPedido.ListarTodoPedidos(STU_SISTEMA.EMPRESAID) == true)
            {
                dtPedido = objPedido.dtLisTodPedidos;
            }
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtListar.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtListar, true);
        }
        void Tab_Dimensionar(C1.Win.C1Command.C1DockingTab dokTab, int intAlto, int intAncho)
        {
            //Tab1.Height = intAlto;
            //Tab1.Width = intAncho;
        }
        void Tab_Posicionar(C1.Win.C1Command.C1DockingTab dokTab, int intPosX, int intPosY)
        {
            //dokTab.Left = intPosX;
            //dokTab.Top = intPosY;
        }
        void VerRegistro(int n_IdRegistro)
        {
            int n_Fila = 0;
            int n_filaflex = 2;
            string c_dato = "";
            DataTable dtResult = new DataTable();

            objRegistro.mysConec = mysConec;
            objRegistro.TraerRegistro(n_IdRegistro);
            entOrdenProd = objRegistro.entOrdenProd;
            lstOrdenProdDet = objRegistro.lstOrdenProdDet;

            entOrdenProd.n_idtipdoc = 76;
            TxtNumSer.Text = entOrdenProd.c_numser;
            TxtNumDoc.Text = entOrdenProd.c_numdoc;
            TxtFchEmi.Text = entOrdenProd.d_fchemi.ToString();
            
            CboTipDocRef.SelectedValue = entOrdenProd.n_idtipdocref;
            LblIdDocRef.Text = Convert.ToString(entOrdenProd.n_iddocref);

            if (entOrdenProd.n_idtipdocref == 75)
            { 
                dtResult = funDatos.DataTableFiltrar(dtRequerimiento, "n_id = " + entOrdenProd.n_iddocref.ToString() + " ");
                if (dtResult.Rows.Count != 0)
                {
                    TxtNumDocRef.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                    TxtNumSerDocRef.Text = dtResult.Rows[0]["c_numser"].ToString(); 
                }
            }
            if (entOrdenProd.n_idtipdocref == 77)
            {
                dtResult = funDatos.DataTableFiltrar(dtPedido, "n_id = " + entOrdenProd.n_iddocref.ToString() + " ");
                if (dtResult.Rows.Count != 0)
                {
                    TxtNumDocRef.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                    TxtNumSerDocRef.Text = dtResult.Rows[0]["c_numser"].ToString();
                }
            }

            CboRes.SelectedValue = entOrdenProd.n_idres;
            CboPri.SelectedValue = entOrdenProd.n_idpri;
            TxtFchEnt.Text = entOrdenProd.d_fchent.ToString();
            TxtObs.Text = entOrdenProd.c_obs;

            FgLisPro.Rows.Count = 2;
            for (n_Fila = 0; n_Fila <= (lstOrdenProdDet.Count - 1); n_Fila++)
            {
                FgLisPro.Rows.Count = FgLisPro.Rows.Count + 1;

                // MOSTRAMOS EL NOMBRE DEL ITEM
                c_dato = lstOrdenProdDet[n_Fila].n_idpro.ToString("");
                c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", c_dato, "N").ToString();
                FgLisPro.SetData(n_filaflex, 1, c_dato);

                c_dato = lstOrdenProdDet[n_Fila].n_idpro.ToString("");                                         // OBTENEMOS EL ID DEL PRODUCTO
                dtResult = funDatos.DataTableFiltrar(dtReceta, "(n_idpro = " + c_dato + ")");                  // FILTRAMOS TODAS LAS RECETAS DEL PRODUCTO
                dtResult = funDatos.DataTableFiltrar(dtResult, "(n_prirec = " + 1 + ")");                      // FILTRAMOS LA RECETA PRINCIPAL DEL PRODUCTO
                c_dato = dtResult.Rows[0]["n_id"].ToString();                                                  // OBTENEMOS EL ID DE LA RECETA
                FgLisPro.SetData(n_filaflex, 8, c_dato);                                                       // ESCRIBIMOS EL ID DE LA RECETA
                c_dato = funDatos.DataTableBuscar(dtResult, "n_id", "c_des", c_dato, "N").ToString();          // OBTENEMOS LA DESCRIPCION DE LA RECETA
                FgLisPro.SetData(n_filaflex, 2, c_dato);                                                       // POR DEFECTO MOSTRAMOS LA RECETA PRINCIPAL


                // MOSTRAMOS LA UNIDAD DE MEDIDA
                c_dato = lstOrdenProdDet[n_Fila].n_idunimed.ToString("");
                c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_despre", c_dato, "N").ToString();
                FgLisPro.SetData(n_filaflex, 3, c_dato);

                c_dato = lstOrdenProdDet[n_Fila].n_can.ToString("0.00");
                FgLisPro.SetData(n_filaflex, 4, c_dato);

                c_dato = Convert.ToString(lstOrdenProdDet[n_Fila].d_fchent);
                FgLisPro.SetData(n_filaflex, 5, c_dato);

                FgLisPro.SetData(n_filaflex, 6, lstOrdenProdDet[n_Fila].n_numarm);                                                            // POR DEFECTO GENERAMOS 1 ARMADA

                c_dato = lstOrdenProdDet[n_Fila].n_idpro.ToString();
                FgLisPro.SetData(n_filaflex, 7, c_dato);

                n_filaflex = n_filaflex + 1;
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

            // MOSTRAMOS EL ULTIMO NUMERO DE REQUERIMIENTO
            string c_numdoc = "";
            c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 76, "0001");
            TxtNumSer.Text = "0001";
            TxtNumDoc.Text = c_numdoc;

            TxtFchEmi.Focus();
        }
        void Blanquea()
        {
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtFchEmi.Text = "";
            CboTipDocRef.SelectedValue = 0;
            TxtNumDocRef.Text = "";
            TxtNumSerDocRef.Text = "";
            LblIdDocRef.Text = "";
            
            TxtFchEnt.Text = "";
            TxtFchEnt.CustomFormat = " ";
            TxtFchEnt.Format = DateTimePickerFormat.Custom;

            CboRes.SelectedValue = 0;
            CboPri.SelectedValue = 0;
            TxtObs.Text = "";
            FgLisPro.Rows.Count = 2;
        }
        void Bloquea()
        {
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtFchEmi.Enabled = !TxtFchEmi.Enabled;
            CboTipDocRef.Enabled = !CboTipDocRef.Enabled;
                        
            CmdBusDocRef.Text = "Buscar";
            //TxtNumDocRef.Enabled = !TxtNumDocRef.Enabled;
            //TxtNumSerDocRef.Enabled = !TxtNumSerDocRef.Enabled;
            TxtFchEnt.Enabled = !TxtFchEnt.Enabled;
            CboRes.Enabled = !CboRes.Enabled;
            CboPri.Enabled = !CboPri.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
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

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            TxtFchEmi.Focus();
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            objRegistro.mysConec = mysConec;
            objRegistro.TraerRegistro(intIdRegistro);
            entOrdenProd = objRegistro.entOrdenProd;

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistro.Eliminar(entOrdenProd) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistro.mysConec = mysConec;
                    dtListar = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
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
            CmdBusDocRef.Enabled = false;
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
                booResultado = objRegistro.Insertar(entOrdenProd, lstOrdenProdDet);
            }
            if (n_QueHace == 2)
            {
                booResultado = objRegistro.Actualizar(entOrdenProd, lstOrdenProdDet);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ No se pudo guardar el registro por el siguiente motivo ! " + objRegistro.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_fila = 0;
            string c_data = "";

            lstOrdenProdDet.Clear();

            entOrdenProd.n_idemp = STU_SISTEMA.EMPRESAID;
            if (n_QueHace == 1)
            {
                entOrdenProd.n_id = 0;
                entOrdenProd.n_idest = 1;                                 // EL REGISTRO SE INICIA COMO PENDIENTE
            }
            else
            {
                entOrdenProd.n_id = entOrdenProd.n_id;
            }
            
            entOrdenProd.n_idtipdoc = 76;
            entOrdenProd.c_numser = TxtNumSer.Text;
            entOrdenProd.c_numdoc = TxtNumDoc.Text;
            entOrdenProd.d_fchemi =  Convert.ToDateTime(TxtFchEmi.Text);
            entOrdenProd.n_anotra = STU_SISTEMA.ANOTRABAJO;
            entOrdenProd.n_mestra = STU_SISTEMA.MESTRABAJO;
            entOrdenProd.n_idres = Convert.ToInt16(CboRes.SelectedValue);
            entOrdenProd.n_idtipdocref =  Convert.ToInt16(CboTipDocRef.SelectedValue);
            entOrdenProd.n_iddocref  =  Convert.ToInt16(LblIdDocRef.Text);
            entOrdenProd.n_idpri =  Convert.ToInt16(CboPri.SelectedValue);
            entOrdenProd.c_obs = TxtObs.Text;
            entOrdenProd.d_fchent = Convert.ToDateTime(TxtFchEnt.Text);
            
            for (n_fila = 2; n_fila <= FgLisPro.Rows.Count-1; n_fila++)
            {
                BE_PRO_ORDENPRODUCCIONDET entDetalle = new BE_PRO_ORDENPRODUCCIONDET ();

                entDetalle.n_idord = 0;
                entDetalle.n_idpro  = Convert.ToInt32(FgLisPro.GetData(n_fila,7).ToString());
                entDetalle.n_idrec  = Convert.ToInt32(FgLisPro.GetData(n_fila,8).ToString());

                c_data = FgLisPro.GetData(n_fila,3).ToString();
                c_data  = Convert.ToString(funDatos.DataTableBuscar(dtUniMed,"c_despre","n_id",c_data,"C"));
                entDetalle.n_idunimed = Convert.ToInt16(c_data);

                entDetalle.n_can = Convert.ToDouble(FgLisPro.GetData(n_fila,4).ToString());
                entDetalle.n_numarm = Convert.ToInt32(FgLisPro.GetData(n_fila, 6).ToString());
                entDetalle.d_fchent = Convert.ToDateTime(FgLisPro.GetData(n_fila, 5).ToString());
                entDetalle.c_obs = "";
                lstOrdenProdDet.Add(entDetalle);
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (TxtNumSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie de la orden de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumSer.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento de la orden de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumDoc.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtFchEmi.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de emision !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchEmi.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboTipDocRef.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el documento de referencia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboTipDocRef.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtNumSerDocRef.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la serie del numero del documento de referencia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumSerDocRef.Focus();
                booEstado = false;
                return booEstado;
            }
            if (TxtNumDocRef.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero del documento de referencia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumSerDocRef.Focus();
                booEstado = false;
                return booEstado;
            }
            if (TxtFchEnt.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de la orden de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchEnt.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboRes.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre de la persona responsable de la produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboRes.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboPri.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la prioridad de la orden de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboPri.Focus();
                booEstado = false;
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
                objRegistro.mysConec = mysConec;
                dtListar = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
                
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
        private void FrmOrdenProduccion_Activated(object sender, EventArgs e)
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
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());
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
        private void Tab1_SelectedIndexChanging(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;

            if (n_QueHace != 3) { return; }

            if (tc.SelectedIndex == 1)
            {
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }
        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            //if (n_QueHace != 3) { return; }

            //if (e.NewIndex == 1)
            //{
            //    int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());

            //    if (n_QueHace != 1)
            //    {
            //        booAgregando = true;
            //        VerRegistro(intIdRegistro);
            //        booAgregando = false;
            //    }
            //}
        }
        private void FrmOrdenProduccion_Resize(object sender, EventArgs e)
        {
            //Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
        }
        private void CboTipDocRef_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true)
            {
                return;
            }

            if (Convert.ToInt16(CboTipDocRef.SelectedValue) == 75)
            {
                CmdBusDocRef.Enabled = true;
                CmdBusDocRef.Text = "Buscar Requerimientos";
            }
            if (Convert.ToInt16(CboTipDocRef.SelectedValue) == 77)
            {
                CmdBusDocRef.Enabled = true;
                CmdBusDocRef.Text = "Buscar Pedidos Cliente";
            }
            FgLisPro.Rows.Count = 2;
        }
        void BuscarRequerimiento()
        {
            string[,] arrCabeceraDg1 = new string[6, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            int n_Fila;
            int n_filaflex = 2;
            string c_dato;

            objRequerimiento.mysConec = mysConec;
            if ( objRequerimiento.ListarPendientes(STU_SISTEMA.EMPRESAID,"") == false)
            {
                MessageBox.Show("No se pudieron traer las ordenes de requerimientos pendiente; por el siguiente motivo : " + objRequerimiento.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
            dtRequerimiento = objRequerimiento.dtOrdReqPendiente;

            arrCabeceraDg1[0, 0] = "Nº Requerimiento";
            arrCabeceraDg1[0, 1] = "120";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_numordque";

            arrCabeceraDg1[1, 0] = "Solicitante";
            arrCabeceraDg1[1, 1] = "300";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_solapenom";

            arrCabeceraDg1[2, 0] = "Fecha Requerimiento";
            arrCabeceraDg1[2, 1] = "80";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "d_fchemi";

            arrCabeceraDg1[3, 0] = "Prioridad";
            arrCabeceraDg1[3, 1] = "60";
            arrCabeceraDg1[3, 2] = "C";
            arrCabeceraDg1[3, 3] = "c_despri";

            arrCabeceraDg1[4, 0] = "Nº Items";
            arrCabeceraDg1[4, 1] = "60";
            arrCabeceraDg1[4, 2] = "N";
            arrCabeceraDg1[4, 3] = "n_numite";

            arrCabeceraDg1[5, 0] = "Id";
            arrCabeceraDg1[5, 1] = "0";
            arrCabeceraDg1[5, 2] = "N";
            arrCabeceraDg1[5, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            xFun.Buscar_CampoOrden = "c_numordque";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtRequerimiento);

            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }

            TxtNumDocRef.Text = dtResult.Rows[0]["c_numdoc"].ToString();
            TxtNumSerDocRef.Text = dtResult.Rows[0]["c_numser"].ToString();

            LblIdDocRef.Text = dtResult.Rows[0]["n_id"].ToString();

            BE_LOG_ORDENREQUERIMIENTO entReque = new BE_LOG_ORDENREQUERIMIENTO();
            List<BE_LOG_ORDENREQUERIMIENTODET> lstRequeDet = new List<BE_LOG_ORDENREQUERIMIENTODET>();

            lstRequeDet.Clear();

            objRequerimiento.TraerRegistro(Convert.ToInt32(LblIdDocRef.Text));
            entReque = objRequerimiento.entReqCab;
            lstRequeDet = objRequerimiento.lstReqDet;

            booAgregando = true;
            FgLisPro.Rows.Count = 2;
            string c_nompro = "";

            for (n_Fila = 0; n_Fila <= (lstRequeDet.Count - 1); n_Fila++)
            {
                FgLisPro.Rows.Count = FgLisPro.Rows.Count + 1;

                // MOSTRAMOS EL NOMBRE DEL ITEM
                c_dato = lstRequeDet[n_Fila].n_idite.ToString("");
                c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", c_dato, "N").ToString();
                c_nompro = c_dato;
                FgLisPro.SetData(n_filaflex, 1, c_dato);
                
                c_dato = lstRequeDet[n_Fila].n_idite.ToString("");                                             // OBTENEMOS EL ID DEL PRODUCTO
                dtResult = funDatos.DataTableFiltrar(dtReceta, "(n_idpro = " + c_dato + ")");                  // FILTRAMOS TODAS LAS RECETAS DEL PRODUCTO
                dtResult = funDatos.DataTableFiltrar(dtResult, "(n_prirec = " + 1 + ")");                      // FILTRAMOS LA RECETA PRINCIPAL DEL PRODUCTO
                if (dtResult.Rows.Count == 0)
                {
                    MessageBox.Show("El producto: " + c_nompro + " no tiene una receta asignada", "Modulo Produccion", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
                else 
                { 
                    c_dato = dtResult.Rows[0]["n_id"].ToString();                                                  // OBTENEMOS EL ID DE LA RECETA
                    FgLisPro.SetData(n_filaflex, 8, c_dato);                                                       // ESCRIBIMOS EL ID DE LA RECETA
                    c_dato = funDatos.DataTableBuscar(dtResult, "n_id", "c_des", c_dato, "N").ToString();          // OBTENEMOS LA DESCRIPCION DE LA RECETA
                    FgLisPro.SetData(n_filaflex, 2, c_dato);                                                       // POR DEFECTO MOSTRAMOS LA RECETA PRINCIPAL
                }                                         

                // MOSTRAMOS LA UNIDAD DE MEDIDA
                c_dato = lstRequeDet[n_Fila].n_idunimed.ToString("");
                c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_despre", c_dato, "N").ToString();
                FgLisPro.SetData(n_filaflex, 3, c_dato);

                c_dato = lstRequeDet[n_Fila].n_can.ToString("0.00");
                FgLisPro.SetData(n_filaflex, 4, c_dato);

                c_dato = Convert.ToString(entReque.d_fchent);
                FgLisPro.SetData(n_filaflex, 5, c_dato);

                FgLisPro.SetData(n_filaflex, 6, 1);                                                            // POR DEFECTO GENERAMOS 1 ARMADA

                c_dato = lstRequeDet[n_Fila].n_idite.ToString();                                               // ID DEL PRODUCTO
                FgLisPro.SetData(n_filaflex, 7, c_dato);

                n_filaflex = n_filaflex + 1;
            }

            booAgregando = false;
        }
        void BuscarPedidoCliente()
        {
            string[,] arrCabeceraDg1 = new string[6, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            int n_Fila;
            int n_filaflex = 2;
            string c_dato;

            objPedido.mysConec = mysConec; 
            if (objPedido.ListarPedidosPendientes(STU_SISTEMA.EMPRESAID) == false)        // OOOJJJOOO actualizar el 0 se puso solo para que pase
            {
                MessageBox.Show("No se pudieron traer los pedidos pendiente, por el siguiente motivo : " + objRequerimiento.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
            dtRequerimiento = objPedido.dtLisPedPend;

            arrCabeceraDg1[0, 0] = "Nº Pedido";
            arrCabeceraDg1[0, 1] = "120";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_numdoc";

            arrCabeceraDg1[1, 0] = "Cliente";
            arrCabeceraDg1[1, 1] = "300";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_nombre";

            arrCabeceraDg1[2, 0] = "Fecha Reg.";
            arrCabeceraDg1[2, 1] = "70";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "d_fchped";

            arrCabeceraDg1[3, 0] = "Nº Orden Compra";
            arrCabeceraDg1[3, 1] = "100";
            arrCabeceraDg1[3, 2] = "N";
            arrCabeceraDg1[3, 3] = "c_numordcom";
            
            arrCabeceraDg1[4, 0] = "Fch. O.C.";
            arrCabeceraDg1[4, 1] = "70";
            arrCabeceraDg1[4, 2] = "F";
            arrCabeceraDg1[4, 3] = "d_fchped";

            arrCabeceraDg1[5, 0] = "Id";
            arrCabeceraDg1[5, 1] = "0";
            arrCabeceraDg1[5, 2] = "N";
            arrCabeceraDg1[5, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            xFun.Buscar_CampoOrden = "c_numdoc";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtRequerimiento);

            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }

            TxtNumDocRef.Text = dtResult.Rows[0]["c_numdoc"].ToString();
            TxtNumSerDocRef.Text = dtResult.Rows[0]["c_numser"].ToString();

            LblIdDocRef.Text = dtResult.Rows[0]["n_id"].ToString();

            BE_VTA_PEDIDOCLI entPedido = new BE_VTA_PEDIDOCLI();
            List<BE_VTA_PEDIDOCLIDET> lstPedidoDet = new List<BE_VTA_PEDIDOCLIDET>();

            lstPedidoDet.Clear();

            objPedido.TraerRegistro(Convert.ToInt32(LblIdDocRef.Text));
            entPedido = objPedido.entPedCab;
            lstPedidoDet = objPedido.lstPedDet;

            booAgregando = true;
            FgLisPro.Rows.Count = 2;

            List<OrdenProduccionDetalleItem> ordenProduccionDetalleItems 
                = new List<OrdenProduccionDetalleItem>();

            foreach (var pedidoDet in lstPedidoDet)
            {
                OrdenProduccionDetalleItem ordenProduccionDetalleItem 
                    = new OrdenProduccionDetalleItem();

                //producto
                ordenProduccionDetalleItem.n_idite = pedidoDet.n_idite;
                c_dato = pedidoDet.n_idite.ToString("");
                c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", c_dato, "N").ToString();
                ordenProduccionDetalleItem.c_despro = c_dato;
                //Receta
                c_dato = pedidoDet.n_idite.ToString("");
                dtResult = funDatos.DataTableFiltrar(dtReceta, "(n_idpro = " + c_dato + ")");
                dtResult = funDatos.DataTableFiltrar(dtResult, "(n_prirec = " + 1 + ")");
                ordenProduccionDetalleItem.n_idrec = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                c_dato = dtResult.Rows[0]["n_id"].ToString();
                c_dato = funDatos.DataTableBuscar(dtResult, "n_id", "c_des", c_dato, "N").ToString();
                ordenProduccionDetalleItem.c_desrec = c_dato;
                //Unidad
                ordenProduccionDetalleItem.n_idunimed = pedidoDet.n_idunimed;
                c_dato = pedidoDet.n_idunimed.ToString("");
                c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_despre", c_dato, "N").ToString();
                ordenProduccionDetalleItem.c_desunimed = c_dato;
                //cantidad
                ordenProduccionDetalleItem.n_can = pedidoDet.n_can;
                //fecha
                ordenProduccionDetalleItem.d_fchent = pedidoDet.d_fchent;
                //
                ordenProduccionDetalleItems.Add(ordenProduccionDetalleItem);
            }

            if (ordenProduccionDetalleItems.Count > 1)
            {
                FrmOrdenProduccionSelItem frmOrdenProduccionSelItem
                    = new FrmOrdenProduccionSelItem(ordenProduccionDetalleItems);
                frmOrdenProduccionSelItem.ShowDialog();
                if (frmOrdenProduccionSelItem.ordenProduccionDetalleItem != null)
                {
                    var ordenProduccionDetalleItem = frmOrdenProduccionSelItem.ordenProduccionDetalleItem;
                    FgLisPro.Rows.Count = FgLisPro.Rows.Count + 1;

                    // MOSTRAMOS EL NOMBRE DEL ITEM
                    FgLisPro.SetData(n_filaflex, 1, ordenProduccionDetalleItem.c_despro);
                    FgLisPro.SetData(n_filaflex, 8, ordenProduccionDetalleItem.n_idrec);
                    FgLisPro.SetData(n_filaflex, 2, ordenProduccionDetalleItem.c_desrec);                                                       // POR DEFECTO MOSTRAMOS LA RECETA PRINCIPAL

                    // MOSTRAMOS LA UNIDAD DE MEDIDA
                    FgLisPro.SetData(n_filaflex, 3, ordenProduccionDetalleItem.c_desunimed);

                    FgLisPro.SetData(n_filaflex, 4, ordenProduccionDetalleItem.n_can.ToString("0.00"));

                    FgLisPro.SetData(n_filaflex, 5, Convert.ToString(ordenProduccionDetalleItem.d_fchent));

                    FgLisPro.SetData(n_filaflex, 6, 1);

                    FgLisPro.SetData(n_filaflex, 7, ordenProduccionDetalleItem.n_idite.ToString());
                }
            }
            else
            {
                foreach (var ordenProduccionDetalleItem in ordenProduccionDetalleItems)
                {
                    FgLisPro.Rows.Count = FgLisPro.Rows.Count + 1;

                    // MOSTRAMOS EL NOMBRE DEL ITEM
                    FgLisPro.SetData(n_filaflex, 1, ordenProduccionDetalleItem.c_despro);
                    FgLisPro.SetData(n_filaflex, 8, ordenProduccionDetalleItem.n_idrec);
                    FgLisPro.SetData(n_filaflex, 2, ordenProduccionDetalleItem.c_desrec);                                                       // POR DEFECTO MOSTRAMOS LA RECETA PRINCIPAL

                    // MOSTRAMOS LA UNIDAD DE MEDIDA
                    FgLisPro.SetData(n_filaflex, 3, ordenProduccionDetalleItem.c_desunimed);

                    FgLisPro.SetData(n_filaflex, 4, ordenProduccionDetalleItem.n_can.ToString("0.00"));

                    FgLisPro.SetData(n_filaflex, 5, Convert.ToString(ordenProduccionDetalleItem.d_fchent));

                    FgLisPro.SetData(n_filaflex, 6, 1);

                    FgLisPro.SetData(n_filaflex, 7, ordenProduccionDetalleItem.n_idite.ToString());

                    n_filaflex += 1;
                }
            }

            booAgregando = false;
        }
        private void CmdBusDocRef_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(CboTipDocRef.SelectedValue) == 0)
            {
                MessageBox.Show("No ha especificado el tipo de documento de referencia " + objRequerimiento.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                CboTipDocRef.Focus();
                return;
            }
            if (Convert.ToInt16(CboTipDocRef.SelectedValue) == 75)
            {
                BuscarRequerimiento();
            }
            if (Convert.ToInt16(CboTipDocRef.SelectedValue) == 77)
            {
                BuscarPedidoCliente();
            }
        }
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        void CalcularArmadas()
        { 
        }
        private void FgLisPro_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgLisPro.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            if (FgLisPro.Col == 6)
            {
                //FgLisPro.Select(FgLisPro.Row - 1, 2);
                CalcularArmadas();
                return;
            }
            //if (FgItems.Col == 2)
            //{
            //    booAgregando = true;

            //    // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
            //    c_despro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 2));
            //    dtResul = funDatos.DataTableFiltrar(dtItems, "c_despro = '" + c_despro + "'");
            //    if (dtResul.Rows.Count != 0)
            //    {
            //        n_idpro = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());

            //        // FILTRAMOS LAS PRESENTACIONES DEL ITEM SELECCIONADO
            //        dtResul = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_idpro + " AND n_default = 1");
            //        funFlex.FlexColumnaCombo(FgItems, dtResul, "c_abrpre", 3);    // ITEMS
            //        if (dtResul.Rows.Count == 1)
            //        {
            //            FgItems.SetData(FgItems.Row, 3, dtResul.Rows[0]["c_abrpre"].ToString());

            //            CargarLotes(n_idpro);
            //        }
            //        else
            //        {
            //            FgItems.SetData(FgItems.Row, 3, "");
            //        }
            //    }
            //    FgItems.SetData(FgItems.Row, 7, DateTime.Now.ToString("HH:mm"));

            //    FgItems.Select(FgItems.Row - 1, 3);
            //    booAgregando = false;
            //    return;
            //}
        }
        private void FgLisPro_EnterCell(object sender, EventArgs e)
        {
            if (FgLisPro.Rows.Count == 2) { return; }
            if (n_QueHace == 3)
            {
                FgLisPro.AllowEditing = false; return;
            }
            if (booAgregando == true) { return; }

            DataTable dtResult = new DataTable();
            int n_idPro = 0;

            if (FgLisPro.Col == 1)
            {
                FgLisPro.AllowEditing = false;
                return;
            }

            if (FgLisPro.Col == 2)
            {
                n_idPro = Convert.ToInt16(FgLisPro.GetData(FgLisPro.Row,7));
                dtResult = funDatos.DataTableFiltrar(dtReceta,"n_idpro = "+ n_idPro.ToString() +"");
                funFlex.FlexColumnaCombo(FgLisPro, dtResult, "c_des", 2);
            }
            if (FgLisPro.Col == 3)
            {
                FgLisPro.AllowEditing = false;
                return;
            }
            if (FgLisPro.Col == 4)
            {
                FgLisPro.AllowEditing = false;
                return;
            }
            //if (FgLisPro.Col == 5)
            //{
            //    FgLisPro.AllowEditing = false;
            //    return;
            //}

            FgLisPro.AllowEditing = true;
        }
        private void FgLisPro_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 6)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void FgLisPro_RowColChange(object sender, EventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgLisPro.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }
        }
        private void CboMeses_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarItems();
        }
        private void TxtObs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtNumSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
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
        private void TxtNumSerDocRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
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
        private void TxtNumDocRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
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
        private void TxtNumSer_TextChanged(object sender, EventArgs e)
        {

        }
        private void TxtFchEmi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboTipDocRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtNumDocRef_TextChanged(object sender, EventArgs e)
        {

        }
        private void CboRes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboPri_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtFchEnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtFchEnt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                TxtFchEnt.Text = "";
                TxtFchEnt.CustomFormat = " ";
                TxtFchEnt.Format = DateTimePickerFormat.Custom;
            }
        }
        private void TxtFchEnt_ValueChanged(object sender, EventArgs e)
        {
            TxtFchEnt.CustomFormat = "dd/MM/yyyy";
            TxtFchEnt.Format = DateTimePickerFormat.Custom;
        }
        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            objRegistro.mysConec = mysConec;
            dtListar = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue));    // CARGAMOS LOS DATOS DEL FORMULARIO
            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            ListarItems();

            if (dtListar.Rows.Count == 0)
            {
                DialogResult Rpta = MessageBox.Show("No se han encontrado registros, ¿ Desea agregar uno ahora ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (DialogResult.Yes == Rpta)
                {
                    //Nuevo();
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

        private void CboMeses_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void TxtFchEmi_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CmdVerDoc_Click(object sender, EventArgs e)
        {

        }
    }
}
