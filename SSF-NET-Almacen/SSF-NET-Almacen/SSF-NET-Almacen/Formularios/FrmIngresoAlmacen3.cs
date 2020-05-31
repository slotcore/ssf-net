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
using SIAC_Entidades.Sistema;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Produccion;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using System.Data.OleDb;
using System.Configuration;

namespace SSF_NET_Almacen.Formularios
{
    public partial class FrmIngresoAlmacen3 : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public OleDbConnection AccConec = new OleDbConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_sun_tipexi objTipoExi = new CN_sun_tipexi();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_mae_clipro ObjPro = new CN_mae_clipro();
        CN_alm_almacenes ObjAlm = new CN_alm_almacenes();
        CN_alm_inventariounimed ObjAlmUniMed = new CN_alm_inventariounimed();
        CN_alm_responsable ObjRes = new CN_alm_responsable();
        CN_alm_almacenesdoc ObjAlmDoc = new CN_alm_almacenesdoc();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_empresa objEmp = new CN_sys_empresa();
        CN_alm_movimientos objMovimientos = new CN_alm_movimientos();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_sun_unimed objUniMed = new CN_sun_unimed();
        CN_sun_tipope objTipOpe = new CN_sun_tipope();
        CN_mae_meses objMeses = new CN_mae_meses();
        //CN_ACC_pro_producciondet objProDet = new CN_ACC_pro_producciondet();
        CN_pro_revision objRev = new CN_pro_revision();
        CN_sys_setup o_set = new CN_sys_setup();
        
        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_ALM_MOVIMIENTOS_CONSULTA BE_Movimiento = new BE_ALM_MOVIMIENTOS_CONSULTA();
        BE_SYS_EMPRESA e_empresa = new BE_SYS_EMPRESA();

        // DATATABLE LOCALES
        DataTable dtProveedor = new DataTable();
        DataTable dtTipoDocumento = new DataTable();
        DataTable dtTipoDocumento2 = new DataTable();
        DataTable dtTipoExis = new DataTable();
        DataTable dtAlmacenes = new DataTable();
        DataTable dtResponsables = new DataTable();
        DataTable dtMovimientos = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtMoviDetalle = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtPresentaItem = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtDocAlm = new DataTable();
        DataTable dtTipOpe = new DataTable();
        DataTable DtProdDet = new DataTable();
        DataTable dtsetup = new DataTable();

        // VARIABLES LOCALES
        int N_IDALMACEN = 0;
        int N_INGTRAZABALIDAD = 0;                                                       // LE INDICAMOS AL MODULO QUE TIENE QUE PEDIR DATOS DE TRAZABILIDAD
        int N_INGPRECIO = 0;
        int n_NumFilasDocumento = 30;                                                    // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                               // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[10, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[9, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890./" + (char)8;
        string strNumerovalidos2 = "EFGR1234567890" + (char)8;       // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmIngresoAlmacen3()
        {
            InitializeComponent();
        }
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void FrmIngresoAlmacen3_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        private void ToolSalir_Click(object sender, EventArgs e)
        {
            objTipoExi = null;
            dtTipoExis = null;

            objTipDoc = null;
            dtTipoDocumento = null;

            objTipoExi = null;
            dtTipoExis = null;

            ObjPro = null;
            dtProveedor = null;

            ObjAlm = null;
            dtAlmacenes = null;

            ObjRes = null;
            dtResponsables = null;

            objMovimientos = null;
            dtMovimientos = null;

            objFormVis = null;
            objFormVis = null;

            this.Close();
        }
        void CargarCombos()
        {
            DataTable dtResult = new DataTable();
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboTipOpe, dtTipoExis, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtTipoDocumento, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboAlmacen, dtAlmacenes, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboResponsable, dtResponsables, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipOpe, dtTipOpe, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");

            //dtResult = funDatos.DataTableFiltrar(dtTipoDocumento2, "n_id IN (74,10,32)");
            dtResult = funDatos.DataTableFiltrar(dtTipoDocumento2, "n_id IN (74,10,32)");
            funDatos.ComboBoxCargarDataTable(CboDocRef, dtResult, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            //this.Height = 582;
            //this.Width = 967;
            
            string c_nomarc = ConfigurationManager.AppSettings["PathIniFile"];
            //string c_idlocal = funDatos.IniLeerSeccion(c_nomarc, "SISTEMA", "LOCAL").ToString();
            N_IDALMACEN = Convert.ToInt16(funDatos.IniLeerSeccion(c_nomarc, "SISTEMA", "ALMACEN").ToString());
            //CboAlmacen.SelectedValue = Convert.ToInt16(c_idalmacen);

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

            //Tab_Posicionar(Tab1, 1, 41);
            //Tab_Dimensionar(Tab1, this.Height - 81, this.Width - 18);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            SetearFlex();

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;                  // INDICAMOS EL NUMERO DE FILAS PARA EL DETALLE
            this.Text = dtForm.Rows[0]["c_titfor"].ToString() + " - Otros Items";
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        void SetearFlex()
        {
            arrCabeceraFlex1[0, 0] = "Codigo";
            arrCabeceraFlex1[0, 1] = "110";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Mercaderia / Producto / Servicio";
            if (N_INGTRAZABALIDAD == 1) { arrCabeceraFlex1[1, 1] = "250"; }
            if (N_INGTRAZABALIDAD == 0) { arrCabeceraFlex1[1, 1] = "400"; }
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Uni. Med.";
            arrCabeceraFlex1[2, 1] = "40";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_itepredes";

            arrCabeceraFlex1[3, 0] = "Cantidad";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "0.000000";
            arrCabeceraFlex1[3, 4] = "n_can";

            arrCabeceraFlex1[4, 0] = "Precio Unitario";
            if (N_INGPRECIO == 1) { arrCabeceraFlex1[4, 1] = "70"; }
            if (N_INGPRECIO == 0) { arrCabeceraFlex1[4, 1] = "0"; }
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.000000";
            arrCabeceraFlex1[4, 4] = "n_numlot";

            arrCabeceraFlex1[5, 0] = "Precio Total";
            if (N_INGPRECIO == 1) { arrCabeceraFlex1[5, 1] = "70"; }
            if (N_INGPRECIO == 0) { arrCabeceraFlex1[5, 1] = "0"; }
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "d_fchpro";

            arrCabeceraFlex1[6, 0] = "Nº Lote";
            if (N_INGTRAZABALIDAD == 1) { arrCabeceraFlex1[6, 1] = "80"; }
            if (N_INGTRAZABALIDAD == 0) { arrCabeceraFlex1[6, 1] = "0"; }
            arrCabeceraFlex1[6, 2] = "C";
            arrCabeceraFlex1[6, 3] = "";
            arrCabeceraFlex1[6, 4] = "c_numlot";

            arrCabeceraFlex1[7, 0] = "Fch. Produccion";
            if (N_INGTRAZABALIDAD == 1) { arrCabeceraFlex1[7, 1] = "70"; }
            if (N_INGTRAZABALIDAD == 0) { arrCabeceraFlex1[7, 1] = "0"; }
            arrCabeceraFlex1[7, 2] = "F";
            arrCabeceraFlex1[7, 3] = "dd/MM/yyyy";
            arrCabeceraFlex1[7, 4] = "c_marca";

            arrCabeceraFlex1[8, 0] = "Producto Conforme";
            arrCabeceraFlex1[8, 1] = "55";
            arrCabeceraFlex1[8, 2] = "B";
            arrCabeceraFlex1[8, 3] = "";
            arrCabeceraFlex1[8, 4] = "c_conf";
        }
        void DataTableCargar()
        {
            ObjPro.mysConec = mysConec;
            dtProveedor = ObjPro.ListarProveedor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);                                         //

            objTipDoc.mysConec = mysConec;
            dtTipoDocumento = objTipDoc.Listar();                                           // 
            dtTipoDocumento2 = objTipDoc.Listar(); 

            objTipoExi.mysConec = mysConec;
            dtTipoExis = objTipoExi.Listar();                                               //  

            o_set.mysConec = mysConec;
            dtsetup = o_set.TraerRegistro(); 

            ObjAlm.mysConec = mysConec;
            dtAlmacenes = ObjAlm.ListarNuevo(STU_SISTEMA.EMPRESAID, Convert.ToInt16(funFunciones.NulosN(dtsetup.Rows[0]["n_almunialmacenes"])));                             // 

            ObjRes.mysConec = mysConec;
            dtResponsables = ObjRes.Listar(STU_SISTEMA.EMPRESAID);                          // 

            objItems.mysConec = mysConec;
            dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objMovimientos.mysConec = mysConec;
            dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 1, 2);

            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            objTipOpe.mysConec = mysConec;
            dtTipOpe = objTipOpe.ListarParaMovimientos(1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(2);

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(2, ref arrCabeceraDg1);

            objEmp.mysConec = mysConec;
            objEmp.TraerRegistro(STU_SISTEMA.EMPRESAID);
            e_empresa = objEmp.e_Empresa;

            ObjAlmDoc.mysConec = mysConec;
            dtDocAlm = ObjAlmDoc.ListarEmpAlm(STU_SISTEMA.EMPRESAID, 1);               // 1 FILTRAREMOS SOLO LOS ALMACENES QUE PERTENESCAN A LA EMPRESA Y QUE GENEREN MOVIMIENTO DE INGRESO A ALMACEN   

            o_set.mysConec = mysConec;
            dtsetup = o_set.TraerRegistro(); 
        }
        private void FrmIngresoAlmacen3_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (dtMovimientos.Rows.Count == 0)
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
            LblNumReg.Text = (dtMovimientos.Rows.Count).ToString();
            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtMovimientos, true);
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
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            if (dtAlmacenes.Rows.Count == 0) { return; }
            if (DgLista.RowCount  ==0 ) {return;}
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[9].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            VerRegistro(intIdRegistro);
            booAgregando = false;
        }
        void VerRegistro(Int64 n_IdRegistro)
        {
            objMovimientos.mysConec = mysConec;
            BE_Movimiento = objMovimientos.TraerRegistro(n_IdRegistro);

            txtFchIng.Text = BE_Movimiento.d_fching.ToString();
            TxtFchDoc.Text = BE_Movimiento.d_fchdoc.ToString();
            
            TxtNumDoc.Text = BE_Movimiento.c_numdoc;
            TxtNumSer.Text = BE_Movimiento.c_numser;
            
            //CboProveedor.SelectedValue = BE_Movimiento.n_idclipro;
            LblIdPro.Text = BE_Movimiento.n_idclipro.ToString();
            TxtNumRuc.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_numdoc", BE_Movimiento.n_idclipro.ToString(), "N").ToString();
            TxtProv.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_nombre", BE_Movimiento.n_idclipro.ToString(), "N").ToString();

            CboAlmacen.SelectedValue = BE_Movimiento.n_idalm;
            TxtObs.Text = BE_Movimiento.c_obs;
            CboTipOpe.SelectedValue = BE_Movimiento.n_idtipope;

            CboDocRef.SelectedValue = BE_Movimiento.n_docrefidtipdoc;
            LblIdDocRef.Text = BE_Movimiento.n_docrefiddocref.ToString();
            TxtNumDocRef.Text = BE_Movimiento.c_docrefnumdoc;
            TxtSerDocRef.Text = BE_Movimiento.c_docrefnumser;

            int n_idalm = Convert.ToInt16(CboAlmacen.SelectedValue);
            // CARGAMOS LOS TIPOS DE DODUMENTO
            DataTable dtResul = funDatos.DataTableFiltrar(dtDocAlm, "n_idalm = " + n_idalm.ToString() + "");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtResul, "n_idtipdoc", "c_desdoc");
            CboTipDoc.SelectedValue = BE_Movimiento.n_idtipdoc;

            // MOSTRAMOS LOS RESPONSABLES DEL ALMACEN
            dtResul = funDatos.DataTableFiltrar(dtResponsables, "n_idalm = " + n_idalm.ToString() + "");
            funDatos.ComboBoxCargarDataTable(CboResponsable, dtResul, "n_id", "c_apenom");
            CboResponsable.SelectedValue = BE_Movimiento.n_perid;

            //funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, BE_Movimiento.lst_items, 2, false);
            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);
            int n_fila = 2;
            foreach (BE_ALM_MOVIMIENTOSDET_CONSULTA element in BE_Movimiento.lst_items)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;

                dtResul = funDatos.DataTableFiltrar(dtItems, "n_id = " + element.n_idite.ToString() + "");
                if (dtResul.Rows.Count != 0)
                {
                    FgItems.SetData(n_fila, 1, dtResul.Rows[0]["c_codpro"].ToString());
                    //FgItems.SetData(n_fila, 1, element.c_tipexides);
                }
                FgItems.SetData(n_fila, 2, element.c_itedes);
                FgItems.SetData(n_fila, 3, element.c_itepredes);
                FgItems.SetData(n_fila, 4, element.n_can);
                FgItems.SetData(n_fila, 5, element.n_preuni);
                FgItems.SetData(n_fila, 6, element.n_pretot);
                FgItems.SetData(n_fila, 7, element.c_numlot);
                FgItems.SetData(n_fila, 8, element.d_fchpro);

                if (element.n_estpro == 1)
                {
                    FgItems.SetData(n_fila, 9, true);
                }
                else
                {
                    FgItems.SetData(n_fila, 9,false);
                }
                n_fila++;
            }
        }
        private void Tab1_SelectedIndexChanging(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;

            if (n_QueHace != 3) { return; }

            if (tc.SelectedIndex == 1)
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns[9].CellValue(DgLista.Row).ToString());

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
            //    Int64 intIdRegistro = Convert.ToInt64(DgLista.Columns[9].CellValue(DgLista.Row).ToString());

            //    if (n_QueHace != 1)
            //    {
            //        booAgregando = true;
            //        VerRegistro(intIdRegistro);
            //        booAgregando = false;
            //    }
            //}
        }
        private void FrmIngresoAlmacen3_Resize(object sender, EventArgs e)
        {
            //Tab_Dimensionar(Tab1, this.Height - 81, this.Width - 18);
        }
        private void ToolNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
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

            if (Convert.ToInt16(CboMeses.SelectedValue) == 0)
            {
                txtFchIng.Text = "01/01/" + STU_SISTEMA.ANOTRABAJO.ToString();
                TxtFchDoc.Text = "01/01/" + STU_SISTEMA.ANOTRABAJO.ToString();
                TxtFchDoc.Enabled = false;
                txtFchIng.Enabled = false;
                CboAlmacen.Focus();
            }
            else
            {
                TxtFchDoc.Enabled = true;
                txtFchIng.Enabled = true;
                txtFchIng.Focus();
            }
            //FgItems.Cols[2].ComboList = "...";
            CboTipOpe.SelectedValue = 2;
            int n_idalm = Convert.ToInt16(dtAlmacenes.Rows[0]["n_id"]);
            CboAlmacen.SelectedValue = n_idalm;

            int n_idres = 0;
            if (dtResponsables.Rows.Count != 0)
            { 
                n_idres = Convert.ToInt16(dtResponsables.Rows[0]["n_id"]);
            }
            CboResponsable.SelectedValue = n_idres;
            CboAlmacen.SelectedValue = Convert.ToInt16(N_IDALMACEN);
            txtFchIng.Focus();
        }
        void Blanquea()
        {
            txtFchIng.Text = "";
            TxtFchDoc.Text = "";
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";

            TxtNumRuc.Text = "";
            TxtProv.Text = "";
            LblIdPro.Text = "";
            
            TxtObs.Text = "";
            TxtNumDocRef.Text = "";
            TxtSerDocRef.Text = "";

            CboTipOpe.SelectedValue = 0;
            CboTipDoc.SelectedValue = 0;
            CboAlmacen.SelectedValue = 0;
            CboResponsable.SelectedValue = 0;
            CboDocRef.SelectedValue = 0;

            CmdBusDocRef.Visible = false;
            LblIdDocRef.Text = "";
        }
        void Bloquea()
        {
            txtFchIng.Enabled = !txtFchIng.Enabled;
            TxtFchDoc.Enabled = !TxtFchDoc.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
            
            CboTipOpe.Enabled = !CboTipOpe.Enabled;
            CboTipDoc.Enabled = !CboTipDoc.Enabled;

            TxtNumRuc.Enabled = !TxtNumRuc.Enabled;
            CmdBusPro.Enabled = !CmdBusPro.Enabled;

            CboAlmacen.Enabled = !CboAlmacen.Enabled;
            CboResponsable.Enabled = !CboResponsable.Enabled;
            CboDocRef.Enabled = !CboDocRef.Enabled;
            
            CmdAddItem.Enabled = !CmdAddItem.Enabled;
            CmdDelItem.Enabled = !CmdDelItem.Enabled;
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
        private void ToolModificar_Click(object sender, EventArgs e)
        {
            Modificar();
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
            if (Convert.ToInt16(CboMeses.SelectedValue) == 0)
            {
                txtFchIng.Text = "01/01/" + STU_SISTEMA.ANOTRABAJO.ToString();
                TxtFchDoc.Text = "01/01/" + STU_SISTEMA.ANOTRABAJO.ToString();
                TxtFchDoc.Enabled = false;
                txtFchIng.Enabled = false;
                CboAlmacen.Focus();
            }
            else
            {
                TxtFchDoc.Enabled = true;
                txtFchIng.Enabled = true;
                txtFchIng.Focus();
            }
            //FgItems.Cols[2].ComboList = "...";

            txtFchIng.Focus();
            booAgregando = false;
        }
        private void ToolEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[9].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            objMovimientos.mysConec = mysConec;
            BE_Movimiento = objMovimientos.TraerRegistro(intIdRegistro);

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                //objMovimientos.AccConec = AccConec;
                if (objMovimientos.Eliminar(intIdRegistro, BE_Movimiento, 2) == true)         // INDICAMOS 2 = ENTRADA
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objMovimientos.mysConec = mysConec;
                    dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 1, 2);
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objMovimientos.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            return booResult;
        }
        private void ToolCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
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
        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            if (Grabar() == true)
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objMovimientos.mysConec = mysConec;
                dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 1, 2);
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
            //objMovimientos.AccConec = AccConec;

            if (n_QueHace == 1)
            {
                if (objMovimientos.DocumentoExiste(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text, TxtNumDoc.Text, 1) == true)
                {
                    MessageBox.Show(" El numero de documento " + TxtNumSer.Text + "-" + TxtNumDoc.Text + " ya existe, ingrese otro ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return booResultado;
                }
                booResultado = objMovimientos.Insertar(BE_Movimiento, 2);   // INDICAMOS 2 = ENTRADA
            }

            if (n_QueHace == 2)
            {
                booResultado = objMovimientos.Actualizar(BE_Movimiento);
            }

            if (booResultado == false)
            {
                MessageBox.Show(" No se pudo guardar el registro por el siguiente motivo: " + objMovimientos.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_fila = 0;
            int n_NumeroElementos = 0;

            // ************************************************
            // ASIGNAMOS LOS DATOS DE LA CABECERA DEL DOCUMENTO
            BE_Movimiento.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_Movimiento.n_idtipmov = 1;                                                 // 1 = INGRESO ; 2 = SALIDA
            
            //BE_Movimiento.n_idclipro = Convert.ToInt16(CboProveedor.SelectedValue);
            BE_Movimiento.n_idclipro = Convert.ToInt16(LblIdPro.Text);

            BE_Movimiento.d_fchdoc = Convert.ToDateTime(TxtFchDoc.Text);
            BE_Movimiento.d_fching = Convert.ToDateTime(txtFchIng.Text);
            BE_Movimiento.n_idtipdoc = Convert.ToInt16(CboTipDoc.SelectedValue);
            BE_Movimiento.c_numser = TxtNumSer.Text;
            BE_Movimiento.c_numdoc = TxtNumDoc.Text;
            BE_Movimiento.n_idalm = Convert.ToInt16(CboAlmacen.SelectedValue);
            BE_Movimiento.n_anotra = STU_SISTEMA.ANOTRABAJO;
            BE_Movimiento.n_idmes = STU_SISTEMA.MESTRABAJO;
            BE_Movimiento.c_obs = TxtObs.Text; 
            BE_Movimiento.n_idtipope = Convert.ToInt16(CboTipOpe.SelectedValue);
            BE_Movimiento.n_perid = Convert.ToInt16(CboResponsable.SelectedValue);
            BE_Movimiento.c_docrefnumdoc = TxtNumDocRef.Text;
            BE_Movimiento.c_docrefnumser = TxtSerDocRef.Text;
            BE_Movimiento.n_tipite = 2;

            BE_Movimiento.n_docrefiddocref = null;
            if (LblIdDocRef.Text != "")
            { 
                BE_Movimiento.n_docrefiddocref = Convert.ToInt32(LblIdDocRef.Text);
            }

            BE_Movimiento.n_docrefidtipdoc = null;
            if (Convert.ToInt16(CboDocRef.SelectedValue) != 0)
            {
                BE_Movimiento.n_docrefidtipdoc = Convert.ToInt16(CboDocRef.SelectedValue);
            }
            
            // **********************************
            // ASIGNAMOS EL DETALLE DEL DOCUMENTO
            if (BE_Movimiento.lst_items != null)
            {
                n_NumeroElementos = Convert.ToInt16(BE_Movimiento.lst_items.Count - 1);

                for (n_fila = 0; n_fila <= n_NumeroElementos; n_fila++)
                {
                    BE_Movimiento.lst_items.RemoveAt(0);
                }
            }

            n_fila = 0;
            booAgregando = true;

            List<BE_ALM_MOVIMIENTOSDET_CONSULTA> LstDetalle = new List<BE_ALM_MOVIMIENTOSDET_CONSULTA>();

            if (FgItems.Rows.Count > 2)
            {
                for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
                {
                    if (funFunciones.NulosC(FgItems.GetData(n_fila, 1)) != "")
                    {
                        BE_ALM_MOVIMIENTOSDET_CONSULTA BE_Detalle = new BE_ALM_MOVIMIENTOSDET_CONSULTA();

                        BE_Detalle.n_idmov = 0;
                        BE_Detalle.n_can = Convert.ToDouble(FgItems.GetData(n_fila, 4).ToString());

                        BE_Detalle.c_tipexides = FgItems.GetData(n_fila, 1).ToString();
                        BE_Detalle.c_itedes = FgItems.GetData(n_fila, 2).ToString();
                        BE_Detalle.c_itepredes = FgItems.GetData(n_fila, 3).ToString();
                        BE_Detalle.c_numlot = funFunciones.NulosC(FgItems.GetData(n_fila, 7));

                        BE_Detalle.d_fchpro = null;
                        if (funFunciones.NulosC(FgItems.GetData(n_fila, 8)) != "")
                        {
                            BE_Detalle.d_fchpro = Convert.ToDateTime(FgItems.GetData(n_fila, 8));
                        }

                        BE_Detalle.d_fchven = null;
                        //if (funFunciones.NulosC(FgItems.GetData(n_fila, 7)) != "")
                        //{
                        //    BE_Detalle.d_fchven = Convert.ToDateTime(FgItems.GetData(n_fila, 7));
                        //}

                        DataTable DtFiltro = new DataTable();

                        // FILTRAMOS EL ITEM DE LA FILA PARA OBTENER EL ID  
                        string strCadenaFiltro = "c_despro = '" + BE_Detalle.c_itedes + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                        BE_Detalle.n_idite = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        // FILTRAMOS LA PRESENTACION PARA OBTENER SU ID
                        strCadenaFiltro = "c_abrpre = '" + BE_Detalle.c_itepredes + "' AND n_idite = " + BE_Detalle.n_idite + "";
                        DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                        BE_Detalle.n_idpre = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        // FILTRAMOS EL TIPO DE PRODUCTO PARA OBTENER SU id
                        strCadenaFiltro = "n_id = '" + BE_Detalle.n_idite.ToString() + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                        if (DtFiltro.Rows.Count != 0)
                        { 
                            BE_Detalle.n_idtippro = Convert.ToInt32(DtFiltro.Rows[0]["n_idtipexi"].ToString());
                        }

                        BE_Detalle.n_preuni = Convert.ToDouble(FgItems.GetData(n_fila, 5));
                        BE_Detalle.n_pretot = Convert.ToDouble(FgItems.GetData(n_fila, 6));

                        BE_Detalle.n_iddep = null;
                        BE_Detalle.n_idpro = null;
                        BE_Detalle.n_iddis = null;
                        BE_Detalle.c_desori = "";
                        BE_Detalle.c_marca = funFunciones.NulosC(FgItems.GetData(n_fila, 8));
                        if (funFunciones.NulosC( FgItems.GetData(n_fila, 9)) == "True")
                        { 
                            BE_Detalle.n_estpro = 1;
                        }
                        else
                        {
                            BE_Detalle.n_estpro = 0;
                        }
                        LstDetalle.Add(BE_Detalle);
                    }
                }
                BE_Movimiento.lst_items = LstDetalle;
            }
            booAgregando = false;
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (txtFchIng.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de ingreso !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (TxtFchDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboTipDoc.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (TxtNumSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboTipOpe.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de operacion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(funFunciones.NulosN(LblIdPro.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre del proveedor !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboAlmacen.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el almacen de destino !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
                            MessageBox.Show("¡ No ha especificado la descripcion del item en la fila Nº " + (intFila - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
                        }

                        if (funFunciones.NulosC(FgItems.GetData(intFila, 3)) == "")
                        {
                            MessageBox.Show("¡ No ha especificado la presentacion del item en la fila Nº " + (intFila - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
                        }
                        if (funFunciones.NulosC(FgItems.GetData(intFila, 4)) == "")
                        {
                            MessageBox.Show("¡ No ha especificado la cantidad del item en la fila Nº " + (intFila - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
                        }

                        if (N_INGTRAZABALIDAD == 1)
                        {
                            if (funFunciones.NulosC(FgItems.GetData(intFila, 7)) == "")
                            {
                                MessageBox.Show("¡ No ha especificado el numero de lote del item en la fila Nº " + (intFila - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                booEstado = false;
                                return booEstado;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("¡ No ha especificado ningun item para este ingreso !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
                dtResult = funDbGrid.DG_Filtrar(dtMovimientos, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
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
        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            string c_despro;
            int n_idpro;
            string c_codigo = "";
            DataTable dtresult = new DataTable();
            int n_idite = 0;
            double n_valor = 0;

            //if (FgItems.Col == 1)
            //{
            //    FgItems.Select(e.Row - 1 , 2);
            //    return;
            //}
            if (FgItems.Col == 1)
            {
                booAgregando = true;
                c_codigo = FgItems.GetData(FgItems.Row, 1).ToString();
                dtresult = funDatos.DataTableFiltrar(dtItems, "c_codpro = '" + c_codigo + "'");
                if (dtresult.Rows.Count != 0)
                {
                    FgItems.SetData(FgItems.Row, 1, dtresult.Rows[0]["c_codpro"].ToString());
                    FgItems.SetData(FgItems.Row, 2, dtresult.Rows[0]["c_despro"].ToString());
                    FgItems.SetData(FgItems.Row, 3, dtresult.Rows[0]["c_abrpre"].ToString());

                    FgItems.SetData(FgItems.Row, 9, 1);

                    //n_idite = Convert.ToInt32(dtresult.Rows[0]["n_id"]);
                    //dtresult = funDatos.DataTableFiltrar(dtitecli, "n_idite = " + n_idite.ToString() + "");
                    //if (dtresult.Rows.Count != 0)
                    //{
                    //    FgItems.SetData(FgItems.Row, 5, dtresult.Rows[0]["n_prebru"].ToString());
                    //}
                    FgItems.Select(FgItems.Row - 1, 4);
                }
                else
                {
                    FgItems.SetData(FgItems.Row, 1, "");
                    FgItems.Select(FgItems.Row - 1, 1);
                }

                booAgregando = false;
                return;
            }

            if (FgItems.Col == 2)
            {
                booAgregando = true;

                // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
                c_despro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 2));
                dtresult = funDatos.DataTableFiltrar(dtItems, "c_despro = '" + c_despro + "'");
                if (dtresult.Rows.Count != 0)
                {
                    n_idpro = Convert.ToInt16(dtresult.Rows[0]["n_id"].ToString());

                    // FILTRAMOS LAS PRESENTACIONES DEL ITEM SELECCIONADO
                    dtresult = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_idpro + " AND n_default = 1");
                    funFlex.FlexColumnaCombo(FgItems, dtresult, "c_abrpre", 3);    // ITEMS
                    if (dtresult.Rows.Count == 1)
                    {
                        FgItems.SetData(FgItems.Row, 3, dtresult.Rows[0]["c_abrpre"].ToString());

                        //CargarLotes(n_idpro);
                    }
                    else
                    {
                        FgItems.SetData(FgItems.Row, 3, "");
                    }
                }
                FgItems.Select(FgItems.Row - 1, 3);
                booAgregando = false;
                return;
            }
            //if (FgItems.Col == 2)
            //{
            //    MostrarUnidadMedida(e.Row, FgItems.GetData(e.Row, 2).ToString());
            //    FgItems.Select(e.Row - 1 , 3);
            //    return;
            //}
            if (FgItems.Col == 3)
            {
                FgItems.Select(e.Row - 1, 4);
                return;
            }
            if (FgItems.Col == 4)
            {
                n_valor = Convert.ToDouble(FgItems.GetData(e.Row, 4)) * Convert.ToDouble(FgItems.GetData(e.Row, 5));
                FgItems.SetData(e.Row, 6, n_valor.ToString("0.00"));
                FgItems.Select(e.Row - 1, 5);
                return;
            }
            if (FgItems.Col == 5)
            {
                n_valor = Convert.ToDouble(FgItems.GetData(e.Row, 4)) * Convert.ToDouble(FgItems.GetData(e.Row, 5));
                FgItems.SetData(e.Row, 6, n_valor.ToString("0.00"));
                //FgItems.SetData(FgItems.Row, 5, FgItems.GetData(FgItems.Row, 5).ToString().ToUpper());
                FgItems.Select(e.Row, 1);
                return;
            }
            //if (FgItems.Col == 6)
            //{
            //    if (funFunciones.EsFecha(FgItems.GetData(FgItems.Row , 6).ToString()) == false)
            //    {
            //        MessageBox.Show("¡ La fecha ingresada no es valida !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //        FgItems.SetData(FgItems.Row, 6, "");
            //        return;
            //    }
            //    FgItems.Select(e.Row - 1 , 7);
            //    return;
            //}
            //if (FgItems.Col == 7)
            //{
            //    if (funFunciones.EsFecha(FgItems.GetData(FgItems.Row , 7).ToString()) == false)
            //    {
            //        MessageBox.Show("¡ La fecha ingresada no es valida !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //        FgItems.SetData(FgItems.Row, 7, "");
            //        return;
            //    }
            //    FgItems.Select(e.Row - 1, 8);
            //    return;
            //}
            //if (FgItems.Col == 8)
            //{
            //    FgItems.SetData(FgItems.Row, 5, FgItems.GetData(FgItems.Row, 5).ToString().ToUpper());
            //    FgItems.Select(e.Row, 1);
            //    return;
            //}
        }
        private void CmdAddItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(CboTipDoc.SelectedValue) == 0)
            {
                MessageBox.Show("No se ha indicado el tipo de documento", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboTipDoc.Focus();
                return;
            }

            bool booAgregarUnidad = true;

            if (Convert.ToString(FgItems.GetData(FgItems.Rows.Count - 1, 1)) == "") booAgregarUnidad = false;
            if (Convert.ToString(FgItems.GetData(FgItems.Rows.Count - 1, 2)) == "") booAgregarUnidad = false;
            if (Convert.ToString(FgItems.GetData(FgItems.Rows.Count - 1, 3)) == "") booAgregarUnidad = false;
            if (Convert.ToString(FgItems.GetData(FgItems.Rows.Count - 1, 4)) == "") booAgregarUnidad = false;

            if (booAgregarUnidad == true)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;
                FgItems.Focus();
            }
            else
            {
                MessageBox.Show("No puede agregar mas items hasta que no haya completado el item anterior", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        private void CmdDelItem_Click(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count > 2)
            {
                FgItems.RemoveItem(FgItems.Row);
                FgItems.Focus();
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
            int n_idtipproducto = 0;
            string strDesTipPro = "";
            FgItems.AllowEditing = false;

            if (FgItems.Col == 1)
            {
                FgItems.AllowEditing = true;
                if (FgItems.Row >= 2)
                {
                    if (funFunciones.NulosC(FgItems.GetData(FgItems.Row - 1, 1)) == "") { FgItems.AllowEditing = false; return; }
                }
                //funFlex.FlexColumnaCombo(FgItems, dtTipoExis, "c_des", 1);
            }

            //if (FgItems.Col == 2)
            //{
            //    // OBTNEMOS LA DESCRIPCION DEL TIPO DE PRODUCTO
            //    strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 1));

            //    if (strDesTipPro == "")
            //    {
            //        FgItems.AllowEditing = false;
            //        funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 2);
            //        return;
            //    }
            //    // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
            //    dtResul = funDatos.DataTableFiltrar(dtTipoExis, "c_des = '" + strDesTipPro + "'");
            //    if (dtResul.Rows.Count != 0)
            //    {
            //        n_idtipproducto = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());

            //        // FILTRAMOS LOS ITEMS DEL TIPO DE PRODUCTO SELECCIONADO
            //        dtResul = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + n_idtipproducto + "","c_despro");
            //        funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 2);    // ITEMS
            //    }
            //}
            if (FgItems.Col == 2)
            {
                FgItems.AllowEditing = true;
            }
            if (FgItems.Col == 3)
            {
                // OBTENEMOS LA DESCRIPCIO DEL ITEM
                strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 2));

                if (strDesTipPro == "")
                {
                    FgItems.AllowEditing = false;
                    funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 2);
                    return;
                }

                // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
                dtResul = funDatos.DataTableFiltrar(dtItems, "c_despro = '" + strDesTipPro + "'");
                if (dtResul.Rows.Count != 0)
                {
                    n_idtipproducto = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());

                    // FILTRAMOS LAS PRESENTACIONES DEL ITEM SELECCIONADO
                    dtResul = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_idtipproducto + "");
                    funFlex.FlexColumnaCombo(FgItems, dtResul, "c_abrpre", 3);    // ITEMS
                }
            }

            if (FgItems.Col == 4)
            {
                // OBTENEMOS LA DESCRIPCIO DEL ITEM
                strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 3));
                FgItems.AllowEditing = true;

                if (strDesTipPro == "")
                {
                    FgItems.AllowEditing = false;
                    return;
                }
            }
            if (FgItems.Col == 5)
            {
                // OBTENEMOS LA DESCRIPCIO DEL ITEM
                strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 3));
                FgItems.AllowEditing = true;

                if (strDesTipPro == "")
                {
                    FgItems.AllowEditing = false;
                    return;
                }
            }

            if (FgItems.Col == 6)
            {

                //FgItems.Cols[6].Format ="dd/MM/yyyy";
            }
            if (FgItems.Col == 7)
            {
                FgItems.AllowEditing = true;
            }
            if (FgItems.Col == 8)
            {
                FgItems.AllowEditing = true;
                FgItems.Cols[7].Format = "dd/MM/yyyy";
            }

            //FgItems.AllowEditing = true;
        }
        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 4)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 5)
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 6)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 7)
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 8)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtNumDoc_TextChanged(object sender, EventArgs e)
        {

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
        private void CboAlmacen_SelectedValueChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }
            int n_idalm = Convert.ToInt16(CboAlmacen.SelectedValue);
            DataTable dtResul = funDatos.DataTableFiltrar(dtDocAlm, "n_idalm = " + n_idalm.ToString() + "");

            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtResul, "n_idtipdoc", "c_desdoc");

            // MOSTRAMOS LOS RESPONSABLES DEL ALMACEN
            dtResul = funDatos.DataTableFiltrar(dtResponsables, "n_idalm = " + n_idalm.ToString() + "");
            funDatos.ComboBoxCargarDataTable(CboResponsable, dtResul, "n_id", "c_apenom");
        }
        private void CboTipDoc_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            int n_IdTipDoc = Convert.ToInt16(CboTipDoc.SelectedValue);
            DataTable dtResul = funDatos.DataTableFiltrar(dtDocAlm, "n_idtipdoc = " + n_IdTipDoc.ToString() + "");
            booAgregando = true;
            
            if (Convert.ToInt16(dtResul.Rows[0]["n_numfil"].ToString()) != 0)
            {
                if (n_QueHace == 1)
                {
                    FgItems.Rows.Count = 2;
                    FgItems.Rows.Count = FgItems.Rows.Count + Convert.ToInt16(dtResul.Rows[0]["n_numfil"].ToString());                  // INDICAMOS EL NUMERO DE FILAS PARA EL DETALLE
                }
            }
            else
            {
                FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;
            }
            TxtNumSer.Focus();
            booAgregando = false;
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
        private void CboAlmacen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboTipDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboProveedor_KeyPress(object sender, KeyPressEventArgs e)
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
        private void panel1_Paint(object sender, PaintEventArgs e)
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
                if (!strNumerovalidos2.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void c1Sizer1_Click(object sender, EventArgs e)
        {

        }
        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            if (Convert.ToInt16(CboMeses.SelectedValue) == 0)
            {
                N_INGPRECIO = 1;
                SetearFlex();
            }
            objMovimientos.mysConec = mysConec;
            dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 1, 2);    // CARGAMOS LOS DATOS DEL FORMULARIO
            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            ListarItems();

            if (dtMovimientos.Rows.Count == 0)
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
        private void TxtSerDocRef_Validated(object sender, EventArgs e)
        {
            if (TxtSerDocRef.Text == "") { return; }

            string strCad = "0000" + TxtSerDocRef.Text;
            TxtSerDocRef.Text = strCad.Substring(strCad.Length - 4, 4);
        }
        private void TxtNumDocRef_Validated(object sender, EventArgs e)
        {
            if (TxtNumDocRef.Text == "") { return; }

            string strCad = "0000000000" + TxtNumDoc.Text;
            TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
        }
        private void TxtSerDocRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000" + TxtSerDocRef.Text;

                TxtSerDocRef.Text = strCad.Substring(strCad.Length - 4, 4);
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
                string strCad = "0000000000" + TxtNumDocRef.Text;

                TxtNumDocRef.Text = strCad.Substring(strCad.Length - 10, 10);
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
        private void TxtNumSer_Validated(object sender, EventArgs e)
        {
            if (TxtNumSer.Text == "") { return; }

            if (TxtNumSer.Text == "") { return; }

            string strCad = "0000" + TxtNumSer.Text;
            TxtNumSer.Text = strCad.Substring(strCad.Length - 4, 4);

            string c_numdoc = "";
            c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboTipDoc.SelectedValue), TxtNumSer.Text);
            TxtNumDoc.Text = c_numdoc;
        }

        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text == "") { return; }

            string strCad = "0000000000" + TxtNumDoc.Text;
            TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
        }

        private void CboDocRef_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            TxtNumDocRef.Enabled = true;
            TxtSerDocRef.Enabled = true;
            CmdBusDocRef.Visible = false;

            CmdAddItem.Enabled = true;
            CmdDelItem.Enabled = true;

            if (Convert.ToInt32(CboDocRef.SelectedValue) == 74)   // PARTE DE CONFORMIDAD
            {
                booAgregando = true;
                TxtNumDocRef.Text = "";
                TxtSerDocRef.Text = "";
                TxtNumDocRef.Enabled = false;
                TxtSerDocRef.Enabled = false;
                CmdBusDocRef.Text = "Parte de Conformidad.";
                CmdBusDocRef.Visible = true;

                //CmdAddItem.Enabled = false;
                //CmdDelItem.Enabled = false;
                //CboTipOpe.SelectedValue = 19;
                booAgregando = false;
            }

            if (Convert.ToInt32(CboDocRef.SelectedValue) == 32)   // PARTE DE CONFORMIDAD
            {
                //TxtNumDocRef.Text = "";
                //TxtSerDocRef.Text = "";
                //TxtNumDocRef.Enabled = false;
                //TxtSerDocRef.Enabled = false;
                CmdBusDocRef.Text = "Guia Transporte";
                CmdBusDocRef.Visible = true;

                if (STU_SISTEMA.EMPRESAID == 1) { LblIdPro.Text = "1711"; }
                if (STU_SISTEMA.EMPRESAID == 2) { LblIdPro.Text = "7032"; }

                TxtNumRuc.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_numdoc", LblIdPro.Text, "N").ToString();
                TxtProv.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_nombre", LblIdPro.Text, "N").ToString();

                TxtNumRuc.Enabled = false;
                CmdBusPro.Enabled = false;

                //CmdAddItem.Enabled = false;
                //CmdDelItem.Enabled = false;
                //CboTipOpe.SelectedValue = 21;
                //CboTipDoc.SelectedValue = 49;
                //TxtNumSer.Text = "0001";
                //TxtNumSer_Validated(sender, e);
                CmdBusDocRef.Focus();
            }
            TxtSerDocRef.Focus();
        }
        private void CmdBusDocRef_Click(object sender, EventArgs e)
        {
            //if (Convert.ToInt16(CboTipDoc.SelectedValue) == 0)
            //{
            //    MessageBox.Show("No se ha indicado el tipo de documento", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    CboTipDoc.Focus();
            //    return;
            //}
            if (Convert.ToInt16(CboDocRef.SelectedValue) == 74)
            {
                MostrarParteConformidad();
            }
            if (Convert.ToInt16(CboDocRef.SelectedValue) == 32)
            {
                MostrarGuiaTransportista();
            }

            booAgregando = false;
        }
        void MostrarParteConformidad()
        {
            string[,] arrCabeceraDg1 = new string[9, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            int n_Fila;
            int n_iditem;
            int n_filaflex;
            //double n_saldorequerido = 0;

            FgItems.Rows.Count = 2;
            FgItems.Rows.Count = n_NumFilasDocumento;
            //objProDet.AccConec = AccConec;
            objRev.mysConec = mysConec;
            DtProdDet = objRev.ConsultaRevisionPendientes(STU_SISTEMA.EMPRESAID);

            arrCabeceraDg1[0, 0] = "Nº Parte Conformidad";
            arrCabeceraDg1[0, 1] = "110";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_numdocvis";

            arrCabeceraDg1[1, 0] = "Producto";
            arrCabeceraDg1[1, 1] = "100";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_despro";

            arrCabeceraDg1[2, 0] = "Uni. Med.";
            arrCabeceraDg1[2, 1] = "40";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "c_abrpre";

            arrCabeceraDg1[3, 0] = "Can. Producida";
            arrCabeceraDg1[3, 1] = "70";
            arrCabeceraDg1[3, 2] = "N";
            arrCabeceraDg1[3, 3] = "n_canpro";

            arrCabeceraDg1[4, 0] = "Can. Prod. Conforme";
            arrCabeceraDg1[4, 1] = "70";
            arrCabeceraDg1[4, 2] = "N";
            arrCabeceraDg1[4, 3] = "n_canprocon";

            arrCabeceraDg1[5, 0] = "Can. Prod. No Conforme";
            arrCabeceraDg1[5, 1] = "80";
            arrCabeceraDg1[5, 2] = "N";
            arrCabeceraDg1[5, 3] = "n_canpronocon";

            arrCabeceraDg1[6, 0] = "Fecha Revision";
            arrCabeceraDg1[6, 1] = "80";
            arrCabeceraDg1[6, 2] = "F";
            arrCabeceraDg1[6, 3] = "d_fchfin";

            arrCabeceraDg1[7, 0] = "Persona Encargada";
            arrCabeceraDg1[7, 1] = "120";
            arrCabeceraDg1[7, 2] = "C";
            arrCabeceraDg1[7, 3] = "c_perenc";

            
            arrCabeceraDg1[8, 0] = "n_Id";
            arrCabeceraDg1[8, 1] = "0";
            arrCabeceraDg1[8, 2] = "N";
            arrCabeceraDg1[8, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CampoOrden = "d_fchfin";
            xFun.Buscar_CadFiltro = "";
            dtResult = xFun.Buscar(arrCabeceraDg1, DtProdDet);

            if (dtResult != null)
            {
                if (dtResult.Rows.Count == 0) { return; }

                TxtNumSer.Text = dtResult.Rows[0]["c_numser"].ToString();
                TxtNumDoc.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                CboTipDoc.SelectedValue = Convert.ToInt16(CboDocRef.SelectedValue);
                TxtNumDocRef.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                TxtSerDocRef.Text = dtResult.Rows[0]["c_numser"].ToString();
                LblIdDocRef.Text = dtResult.Rows[0]["n_id"].ToString();
                txtFchIng.Text = Convert.ToDateTime(dtResult.Rows[0]["d_fchrev"]).ToString("dd/MM/yyyy");
                TxtFchDoc.Text = Convert.ToDateTime(dtResult.Rows[0]["d_fchrev"]).ToString("dd/MM/yyyy");

                //STU_SISTEMA.
                LblIdPro.Text = e_empresa.n_idclipro.ToString();
                TxtNumRuc.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_numdoc", LblIdPro.Text, "N").ToString();
                TxtProv.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_nombre", LblIdPro.Text, "N").ToString();

                n_filaflex = 2;
                booAgregando = true;
                for (n_Fila = 0; n_Fila <= (dtResult.Rows.Count - 1); n_Fila++)
                {
                    n_iditem = Convert.ToInt32(dtResult.Rows[n_Fila]["n_idite"].ToString());

                    FgItems.SetData(n_filaflex, 1, "PRODUCTOS TERMINADOS");
                    FgItems.SetData(n_filaflex, 2, dtResult.Rows[n_Fila]["c_despro"].ToString());

                    // MOSTRAMOS LA PRESENTACION DEL ITEM
                    dtresulpre = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_iditem + " AND n_default = 1");
                    if (dtresulpre.Rows.Count == 1)
                    {
                        FgItems.SetData(n_filaflex, 3, dtresulpre.Rows[0]["c_abrpre"].ToString());
                    }
                    else
                    {
                        FgItems.SetData(n_filaflex, 3, "");
                    }

                    FgItems.SetData(n_filaflex, 4, Convert.ToDouble(dtResult.Rows[n_Fila]["n_canprocon"]).ToString("0.000000"));
                    FgItems.SetData(n_filaflex, 7, dtResult.Rows[n_Fila]["c_numlot"].ToString());
                    FgItems.SetData(n_filaflex, 8, Convert.ToDateTime(dtResult.Rows[n_Fila]["d_fchrev"]).ToString("dd/MM/yy"));

                    FgItems.SetData(n_filaflex, 9, true);

                    // SI LA CANTIDAD DE PRODUCTO NO CONFORME ES MAYOR A 0, MOSTRAMOS COMO PRODUCTO NO CONFORME
                    if (Convert.ToDouble(dtResult.Rows[n_Fila]["n_canpronocon"]) != 0)
                    {
                        n_filaflex = n_filaflex + 1;

                        // **********************
                        // PRODUCTOS NO CONFORMES
                        FgItems.SetData(n_filaflex, 1, "PRODUCTOS TERMINADOS");
                        FgItems.SetData(n_filaflex, 2, dtResult.Rows[n_Fila]["c_despro"].ToString());

                        // MOSTRAMOS LA PRESENTACION DEL ITEM
                        dtresulpre = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_iditem + " AND n_default = 1");
                        if (dtresulpre.Rows.Count == 1)
                        {
                            FgItems.SetData(n_filaflex, 3, dtresulpre.Rows[0]["c_abrpre"].ToString());
                        }
                        else
                        {
                            FgItems.SetData(n_filaflex, 3, "");
                        }

                        FgItems.SetData(n_filaflex, 4, Convert.ToDouble(dtResult.Rows[n_Fila]["n_canpronocon"]).ToString("0.000000"));
                        FgItems.SetData(n_filaflex, 7, dtResult.Rows[n_Fila]["c_numlot"].ToString());
                        FgItems.SetData(n_filaflex, 8, Convert.ToDateTime(dtResult.Rows[n_Fila]["d_fchrev"]).ToString("dd/MM/yy"));

                        FgItems.SetData(n_filaflex, 9, false);
                        n_filaflex = n_filaflex + 1;
                    }
                }
            }
        }
        void MostrarGuiaTransportista()
        {
            int n_Fila = 0;
            int n_filaflex = 2;
            int n_iditem = 0;
            double n_Cantidad = 0;
            DataTable dtresult = new DataTable();
            DataTable DtSolMat = new DataTable();
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            DataTable dtItemFiltro = new DataTable();
            CN_vta_guias o_guia = new CN_vta_guias();
           
            o_guia.mysConec = mysConec;
            dtResult = o_guia.BuscarGuiasPendientesAlmacen(STU_SISTEMA.EMPRESAID, 2, 2, STU_SISTEMA.ANOTRABAJO);
            int n_idguia;
       
            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }

            n_idguia = Convert.ToInt16(dtResult.Rows[0]["n_id"].ToString());
            LblIdDocRef.Text = n_idguia.ToString();
            TxtSerDocRef.Text = dtResult.Rows[0]["c_numser"].ToString();
            TxtNumDocRef.Text = dtResult.Rows[0]["c_numdoc1"].ToString();

            //CboTipOpe.SelectedValue = Convert.ToInt16(dtResult.Rows[0]["n_idmottra"]);
            CboTipDoc.SelectedValue = Convert.ToInt16(dtResult.Rows[0]["n_idtipdoc"]);
            TxtNumSer.Text = dtResult.Rows[0]["c_numser"].ToString();
            TxtNumDoc.Text = dtResult.Rows[0]["c_numdoc1"].ToString();

            txtFchIng.Value = Convert.ToDateTime(dtResult.Rows[0]["d_fchdoc"]);
            TxtFchDoc.Value = Convert.ToDateTime(dtResult.Rows[0]["d_fchdoc"]);

            BE_VTA_GUIAS entGuia = new BE_VTA_GUIAS();
            List<BE_VTA_GUIASDET> lstGuiasDet = new List<BE_VTA_GUIASDET>();

            entGuia = o_guia.TraerRegistro(n_idguia);
            lstGuiasDet = o_guia.LstDetalle;

            //dtResult = DtDetalleTMP;
            n_filaflex = 2;
            FgItems.Rows.Count = 2;

            booAgregando = true;
            for (n_Fila = 0; n_Fila <= (lstGuiasDet.Count - 1); n_Fila++)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;
                n_iditem = Convert.ToInt32(lstGuiasDet[n_Fila].n_idite);

                dtItemFiltro = funDatos.DataTableFiltrar(dtItems, "n_id = " + n_iditem + "");

                if (dtItemFiltro.Rows.Count != 0)
                {
                    // MOSTRAMOS LA DESCRIPCION DEL ITEM
                    FgItems.SetData(n_filaflex, 2, dtItemFiltro.Rows[0]["c_despro"].ToString());

                    // MOSTRAMOS LA DESCRIPCION DEL TIPO DE ITEM
                    dtItemFiltro = funDatos.DataTableFiltrar(dtTipoExis, "n_id = " + Convert.ToInt16(dtItemFiltro.Rows[0]["n_idtipexi"]) + "");
                    FgItems.SetData(n_filaflex, 1, dtItemFiltro.Rows[0]["c_des"].ToString());
                }

                //MOSTRAMOS LA PRESENTACION DEL ITEM
                dtresulpre = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_iditem + " AND n_default = 1");
                if (dtresulpre.Rows.Count == 1)
                {
                    FgItems.SetData(n_filaflex, 3, dtresulpre.Rows[0]["c_abrpre"].ToString());
                }
                else
                {
                    FgItems.SetData(n_filaflex, 3, "");
                }
                n_Cantidad = Convert.ToDouble(lstGuiasDet[n_Fila].n_canpro);
                FgItems.SetData(n_filaflex, 4, n_Cantidad.ToString("0.000000"));
                //FgItems.SetData(n_filaflex, 6, n_Cantidad.ToString("0.000000"));
                //FgItems.SetData(n_filaflex, 7, DateTime.Now.ToString("HH:mm"));
                n_filaflex = n_filaflex + 1;
            }

            booAgregando = false;
        }
        private void ToolImprimir_Click(object sender, EventArgs e)
        {
        }
        void MostrarUnidadMedida(int n_Fila, string c_DesItem)
        {
            string c_despro;
            DataTable dtResul = new DataTable();
            int n_idpro;

            // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
            booAgregando = true;
            c_despro = c_DesItem;
            dtResul = funDatos.DataTableFiltrar(dtItems, "c_despro = '" + c_despro + "'");
            if (dtResul.Rows.Count != 0)
            {
                n_idpro = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());

                // FILTRAMOS LAS PRESENTACIONES DEL ITEM SELECCIONADO
                dtResul = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_idpro + " AND n_default = 1");
                funFlex.FlexColumnaCombo(FgItems, dtResul, "c_abrpre", 3);    // ITEMS
                if (dtResul.Rows.Count == 1)
                {
                    FgItems.SetData(FgItems.Row, 3, dtResul.Rows[0]["c_abrpre"].ToString());
                }
                else
                {
                    FgItems.SetData(FgItems.Row, 3, "");
                }
            }
            booAgregando = false;
            //FgItems.SetData(FgItems.Row, 7, DateTime.Now.ToString("HH:mm"));
        }
        private void ToolManFun_Click(object sender, EventArgs e)
        {
            Helper.Cls_VisorHTML VerHtml = new Helper.Cls_VisorHTML();
            VerHtml.c_NombreArchivo = "j:\\SSF-NET\\archivos\\documentacion\\Procesos\\proceso01.html";
            VerHtml.c_TituloForm = "Manual Funciones - Almacen";
            VerHtml.VerHtml();
        }
        private void CboTipOpe_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            TxtNumRuc.Text = "";
            TxtProv.Text = "";
            LblIdPro.Text = "";

            CboTipDoc.SelectedValue = 0;
            CboDocRef.SelectedValue = 0;

            if (Convert.ToInt32(CboTipOpe.SelectedValue) == 19)
            {
                if (STU_SISTEMA.EMPRESAID == 1) { LblIdPro.Text = "1711"; }
                if (STU_SISTEMA.EMPRESAID == 2) { LblIdPro.Text = "7032"; }

                TxtNumRuc.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_numdoc", LblIdPro.Text, "N").ToString();
                TxtProv.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_nombre", LblIdPro.Text, "N").ToString();

                TxtNumRuc.Enabled = false;
                CmdBusPro.Enabled = false;

                CboTipDoc.SelectedValue = 73;
                CboDocRef.SelectedValue = 74;

                CboResponsable.Focus();
            }

            if (Convert.ToInt32(CboTipOpe.SelectedValue) == 21)
            {
                if (STU_SISTEMA.EMPRESAID == 1) { LblIdPro.Text = "1711"; }
                if (STU_SISTEMA.EMPRESAID == 2) { LblIdPro.Text = "7032"; }

                TxtNumRuc.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_numdoc", LblIdPro.Text, "N").ToString();
                TxtProv.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_nombre", LblIdPro.Text, "N").ToString();

                TxtNumRuc.Enabled = false;
                CmdBusPro.Enabled = false;

                CboTipDoc.SelectedValue = 10;
                CboDocRef.SelectedValue = 32;

                CboResponsable.Focus();
            }
        }

        private void CmdBusPro_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            ObjPro.mysConec = mysConec;
            dtResult = ObjPro.BuscarCliPro(dtProveedor, 1, "n_id", "");

            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    LblIdPro.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtNumRuc.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                    TxtProv.Text = dtResult.Rows[0]["c_nombre"].ToString();
                }
                else
                {
                    LblIdPro.Text = "";
                    TxtProv.Text = "";
                    TxtNumRuc.Text = "";
                }
            }
        }

        private void TxtNumRuc_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtNumRuc_Validated(object sender, EventArgs e)
        {
            DataTable dtresul = new DataTable();
            dtresul = funDatos.DataTableFiltrar(dtProveedor, "c_numdoc = '" + TxtNumRuc.Text + "'");

            if (dtresul.Rows.Count != 0)
            {
                TxtNumRuc.Text = dtresul.Rows[0]["c_numdoc"].ToString();
                TxtProv.Text = dtresul.Rows[0]["c_nombre"].ToString();
                LblIdPro.Text = dtresul.Rows[0]["n_id"].ToString();
            }
            else
            {
                TxtNumRuc.Text = "";
                TxtProv.Text = "";
                LblIdPro.Text = "";
            }
        }

        private void FgItems_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgItems.Col == 2)
            {
                int n_idtipexi = 3;
                DataTable dtResul = new DataTable();
                string c_dato = "";

                // OBTNEMOS LA DESCRIPCION DEL TIPO DE PRODUCTO
                string strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 1));
                if (strDesTipPro == "")
                {
                    n_idtipexi = 0;
                }
    
                // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
                dtResul = funDatos.DataTableFiltrar(dtTipoExis, "c_des = '" + strDesTipPro + "'");
                if (dtResul.Rows.Count != 0)
                {
                    n_idtipexi = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());
                }

                booAgregando = true;
                dtResul = objItems.BuscarItem("", "n_id", dtItems, n_idtipexi);
                if (dtResul != null)
                {
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["c_despro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                        FgItems.SetData(FgItems.Row, 2, c_dato);


                        c_dato = dtResul.Rows[0]["n_id"].ToString();        // MOSTRAMOS LA PRESENTACION DEL ITEM
                        c_dato = funDatos.DataTableBuscar(dtPresentaItem, "n_idite", "c_abrpre", c_dato, "N").ToString();

                        FgItems.SetData(FgItems.Row, 3, c_dato);
                    }
                }
                booAgregando = false;
            }
        }

        private void FgItems_KeyUp(object sender, KeyEventArgs e)
        {
            if (FgItems.Rows.Count == 2) { return; }
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }
            if (booAgregando == true) { return; }

            DataTable dtResul = new DataTable();
            string c_dato = "";
            int n_idite = 0;
            DataTable dtresult2 = new DataTable();

            if ((FgItems.Col == 1) || (FgItems.Col == 2))
            {
                if (e.KeyCode.ToString() == "F5")
                {
                    booAgregando = true;
                    dtResul = objItems.BuscarItem("", "n_id", dtItems, 0);
                    if (dtResul != null)
                    {
                        if (dtResul.Rows.Count != 0)
                        {
                            c_dato = dtResul.Rows[0]["c_codpro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                            FgItems.SetData(FgItems.Row, 1, c_dato);

                            c_dato = dtResul.Rows[0]["c_despro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                            FgItems.SetData(FgItems.Row, 2, c_dato);

                            c_dato = dtResul.Rows[0]["c_abrpre"].ToString();        // MOSTRAMOS LA PRESENTACION DEL ITEM
                            FgItems.SetData(FgItems.Row, 3, c_dato);

                            //n_idite = Convert.ToInt32(dtResul.Rows[0]["n_id"]);
                            //dtresult2 = funDatos.DataTableFiltrar(dtitecli, "n_idite = " + n_idite.ToString() + "");
                            //if (dtresult2.Rows.Count != 0)
                            //{
                            //    FgItems.SetData(FgItems.Row, 5, dtresult2.Rows[0]["n_prebru"].ToString());
                            //}

                            FgItems.Select(FgItems.Row, 4);
                        }
                    }
                    booAgregando = false;
                }
            }
        }
        private void notaIngresoFormatBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_alm_movimientos objAlm = new CN_alm_movimientos();
            objAlm.STU_SISTEMA = STU_SISTEMA;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            objAlm.ReportImprimirNotaIngreso(intIdRegistro, 2);
        }
        private void notaIngresoFormatAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_alm_movimientos objAlm = new CN_alm_movimientos();
            objAlm.STU_SISTEMA = STU_SISTEMA;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            objAlm.ReportImprimirNotaIngreso(intIdRegistro, 1);
        }
    }
}
