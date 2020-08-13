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
        CN_alm_almacenes ObjAlm = new CN_alm_almacenes();
        CN_alm_inventariounimed ObjAlmUniMed = new CN_alm_inventariounimed();
        CN_alm_personal ObjRes = new CN_alm_personal();
        CN_alm_almacenesdoc ObjAlmDoc = new CN_alm_almacenesdoc();
        CN_alm_inventariolotes ObjLotes = new CN_alm_inventariolotes();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_empresa funsys = new CN_sys_empresa();
        CN_con_costoproduccion objCostoProduccion = new CN_con_costoproduccion();
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
        BE_CON_COSTOPRODUCCION BE_CostoProduccion = new BE_CON_COSTOPRODUCCION();
        BE_SYS_EMPRESA entEmp = new BE_SYS_EMPRESA();

        // DATATABLE LOCALES
        DataTable dtConfiguracionCosto = new DataTable();
        DataTable dtAlmacenesDestino = new DataTable();
        DataTable dtTipoExis = new DataTable();
        DataTable dtResponsables = new DataTable();
        DataTable dtCostoProduccion = new DataTable();
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
        string[,] arrCabeceraDg1 = new string[10, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[11, 5];
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
            funDatos.ComboBoxCargarDataTable(CboConfiguracion, dtConfiguracionCosto, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboResponsable, dtResponsables, "n_id", "destra");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
        }

        void ConfigurarFormulario()
        {
            Tab1.SelectedIndex = 0;
            string c_nomarc = ConfigurationManager.AppSettings["PathIniFile"];
            N_IDALMACEN = Convert.ToInt16(funDatos.IniLeerSeccion(c_nomarc, "SISTEMA", "ALMACEN").ToString());

            if (dtsetup.Rows.Count != 0)
            {
                if (Convert.ToInt16(dtsetup.Rows[0]["n_almtrazabilidad"]) == 1)
                {
                    N_INGTRAZABALIDAD = 1;
                }
                if (Convert.ToInt16(dtsetup.Rows[0]["n_guipedpre"]) == 1)
                {
                    N_INGPRECIO = 1;
                }
            }

            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            arrCabeceraFlex1[0, 0] = "Parte Producción";
            arrCabeceraFlex1[0, 1] = "100";
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Item";
            arrCabeceraFlex1[1, 1] = "280";
            arrCabeceraFlex1[1, 2] = "S";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Uni. Med.";
            arrCabeceraFlex1[2, 1] = "40";
            arrCabeceraFlex1[2, 2] = "S";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_itepredes";

            arrCabeceraFlex1[3, 0] = "Receta";
            arrCabeceraFlex1[3, 1] = "75";
            arrCabeceraFlex1[3, 2] = "C";
            arrCabeceraFlex1[3, 3] = "";
            arrCabeceraFlex1[3, 4] = "n_numlot";

            arrCabeceraFlex1[4, 0] = "Factor Dist.";
            arrCabeceraFlex1[4, 1] = "70";
            arrCabeceraFlex1[4, 2] = "C";
            arrCabeceraFlex1[4, 3] = "";
            arrCabeceraFlex1[4, 4] = "n_can";

            arrCabeceraFlex1[5, 0] = "Cantidad";
            arrCabeceraFlex1[5, 1] = "70";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "n_can";

            arrCabeceraFlex1[6, 0] = "Costo MP";
            arrCabeceraFlex1[6, 1] = "70";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_can";

            arrCabeceraFlex1[7, 0] = "Costo MOD";
            arrCabeceraFlex1[7, 1] = "70";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "n_can";

            arrCabeceraFlex1[8, 0] = "Costo Primo";
            arrCabeceraFlex1[8, 1] = "70";
            arrCabeceraFlex1[8, 2] = "D";
            arrCabeceraFlex1[8, 3] = "0.00";
            arrCabeceraFlex1[8, 4] = "n_can";

            arrCabeceraFlex1[9, 0] = "Costo CIF";
            arrCabeceraFlex1[9, 1] = "70";
            arrCabeceraFlex1[9, 2] = "D";
            arrCabeceraFlex1[9, 3] = "0.00";
            arrCabeceraFlex1[9, 4] = "n_can";

            arrCabeceraFlex1[10, 0] = "Costo Total";
            arrCabeceraFlex1[10, 1] = "70";
            arrCabeceraFlex1[10, 2] = "D";
            arrCabeceraFlex1[10, 3] = "0.00";
            arrCabeceraFlex1[10, 4] = "n_can";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);

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

            ObjAlm.mysConec = mysConec;
            dtConfiguracionCosto = ObjAlm.ListarNuevo(STU_SISTEMA.EMPRESAID, Convert.ToInt16(funFunciones.NulosN(dtsetup.Rows[0]["n_almunialmacenes"])));                             // 
            dtAlmacenesDestino = ObjAlm.ListarNuevo(STU_SISTEMA.EMPRESAID, Convert.ToInt16(funFunciones.NulosN(dtsetup.Rows[0]["n_almunialmacenes"])));                             // 

            ObjRes.mysConec = mysConec;
            dtResponsables = ObjRes.ListarPorCargo(STU_SISTEMA.EMPRESAID, 2);                          // 

            objItems.mysConec = mysConec;
            dtItems = objItems.ListarTabla(STU_SISTEMA.EMPRESAID);

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objCostoProduccion.mysConec = mysConec;
            dtCostoProduccion = objCostoProduccion.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);

            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(99);

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(99, ref arrCabeceraDg1);

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
            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
            LblNumReg.Text = (dtCostoProduccion.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtCostoProduccion, true);
        }

        void VerRegistro(int n_IdRegistro)
        {
            objCostoProduccion.mysConec = mysConec;
            BE_CostoProduccion = objCostoProduccion.TraerRegistro(n_IdRegistro);

            TxtNumDoc.Text = BE_CostoProduccion.c_numdoc;
            TxtNumSer.Text = BE_CostoProduccion.c_numser;            
            CboConfiguracion.SelectedValue = BE_CostoProduccion.n_idconfigval;
            TxtObs.Text = BE_CostoProduccion.c_obs;            
            CboResponsable.SelectedValue = BE_CostoProduccion.n_idresp;

            bECONCOSTOPRODUCCIONDETBindingSource.DataSource = BE_CostoProduccion.lst_items;
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
            FgItems.Rows.Count = 2;
            booAgregando = false;
            FgItems.Cols[2].ComboList = "...";
            CboConfiguracion.SelectedValue = Convert.ToInt16(N_IDALMACEN);
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
        }
        void MostrarEstadoMes(int n_IdEmpresa, int n_IdMes)
        {
            obj_fungen.mysConec = mysConec;
            if (obj_fungen.EstadoPeriodo(n_IdEmpresa, n_IdMes, 3) == true)
            {
                ToolNuevo.Visible = false;
                ToolModificar.Visible = false;
                ToolEliminar.Visible = false;

                ToolGrabar.Visible = false;
                ToolCancelar.Visible = false;

                toolStripSeparator1.Visible = false;
                toolStripSeparator2.Visible = false;

                PicClos1.Visible = true;
                PicClos2.Visible = true;
            }
            else
            {
                ToolNuevo.Visible = true;
                ToolModificar.Visible = true;
                ToolEliminar.Visible = true;

                ToolGrabar.Visible = true;
                ToolCancelar.Visible = true;

                toolStripSeparator1.Visible = true;
                toolStripSeparator2.Visible = true;

                PicClos1.Visible = false;
                PicClos2.Visible = false;
            }
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

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[9].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            CboMeses.Focus();
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[9].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            objCostoProduccion.mysConec = mysConec;
            BE_CostoProduccion = objCostoProduccion.TraerRegistro(intIdRegistro);

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                //objMovimientos.AccConec = AccConec;
                if (objCostoProduccion.Eliminar(intIdRegistro) == true)     // INDICAMOS 1 = SALIDA
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objCostoProduccion.mysConec = mysConec;
                    dtCostoProduccion = objCostoProduccion.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objCostoProduccion.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
                if (objCostoProduccion.DocumentoExiste(STU_SISTEMA.EMPRESAID, TxtNumSer.Text, TxtNumDoc.Text) == true)
                {
                    MessageBox.Show(" El numero de documento " + TxtNumSer.Text + "-" + TxtNumDoc.Text + " ya existe, ingrese otro ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return booResultado;
                }
                booResultado = objCostoProduccion.Insertar(BE_CostoProduccion);
            }

            if (n_QueHace == 2)
            {
                //objMovimientos.AccConec = AccConec;
                booResultado = objCostoProduccion.Actualizar(BE_CostoProduccion);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {
            BE_CostoProduccion = new BE_CON_COSTOPRODUCCION();
            BE_CostoProduccion.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_CostoProduccion.c_numser = TxtNumSer.Text;
            BE_CostoProduccion.c_numdoc = TxtNumDoc.Text;
            BE_CostoProduccion.n_idconfigval = Convert.ToInt16(CboConfiguracion.SelectedValue);
            BE_CostoProduccion.n_anotra = STU_SISTEMA.ANOTRABAJO;
            BE_CostoProduccion.n_idmes = STU_SISTEMA.MESTRABAJO;
            BE_CostoProduccion.c_obs = TxtObs.Text; ;            
            BE_CostoProduccion.n_idresp =  Convert.ToInt16(CboResponsable.SelectedValue);
            booAgregando = true;

            if (FgItems.Rows.Count > 2)
            {
                int n_fila;
                for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
                {
                    if (funFunciones.NulosC(FgItems.GetData(n_fila, 1)) != "")
                    {
                        BE_CON_COSTOPRODUCCIONDET BE_Detalle = new BE_CON_COSTOPRODUCCIONDET();

                        BE_Detalle.n_idcostoprod = 0;
                        BE_Detalle.n_can = Convert.ToDouble(FgItems.GetData(n_fila, 6));
                        BE_Detalle.n_costomp = Convert.ToDouble(FgItems.GetData(n_fila, 7));
                        BE_Detalle.n_costomod = Convert.ToDouble(FgItems.GetData(n_fila, 8));
                        BE_Detalle.n_costocif = Convert.ToDouble(FgItems.GetData(n_fila, 10));
                        BE_Detalle.n_idparteprod = Convert.ToInt32(FgItems.GetData(n_fila, 12));
                        BE_Detalle.n_idmov = Convert.ToInt32(FgItems.GetData(n_fila, 13));

                        BE_CostoProduccion.lst_items.Add(BE_Detalle);
                    }
                }
            }
            booAgregando = false;
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

                if (dtCostoProduccion.Rows.Count == 0)
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
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objCostoProduccion.mysConec = mysConec;
                dtCostoProduccion = objCostoProduccion.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
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

            ObjAlm = null;
            dtConfiguracionCosto = null;

            ObjRes = null;
            dtResponsables = null;

            objCostoProduccion = null;
            dtCostoProduccion = null;

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
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[9].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[9].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            VerRegistro(intIdRegistro);
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
            if (e.KeyChar == 13)
            {
                DataTable dtResult = new DataTable();

                string c_CadFiltro = funDbGrid.DG_LeerCondicionesFiltro(DgLista);
                dtResult = funDbGrid.DG_Filtrar(dtCostoProduccion, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
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
            BE_CostoProduccion.lst_items = objCostoProduccion.ListarPartesdeProduccion(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
            bECONCOSTOPRODUCCIONDETBindingSource.DataSource = BE_CostoProduccion.lst_items;
            FgItems.DataSource = bECONCOSTOPRODUCCIONDETBindingSource.DataSource;
        }

        private void BtnProcesarMP_Click(object sender, EventArgs e)
        {

        }


        //private void ProcesarMp()
        //{
        //    foreach (BE_CON_COSTOPRODUCCIONDET costOrdenProduccionViewModel in BE_CostoProduccion.lst_items)
        //    {
        //        ProcesaInsumos(costoProduccion, fechaInicioDateTimePicker.Value, fechaFinDateTimePicker.Value);
        //        ProcesaIntermedios(ViewModel.FechaInicio, ViewModel.FechaFin);
        //        ProcesaTerminados(ViewModel.FechaInicio, ViewModel.FechaFin);
        //        CosteaMercaderia(costoProduccion, costOrdenProduccionViewModel.CodigoMercaderia, ViewModel.FechaInicio, ViewModel.FechaFin);
        //    }
        //}


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
    }
}
