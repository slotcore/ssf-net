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
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using System.Data.OleDb;

namespace SSF_NET_Almacen.Formularios
{
    public partial class FrmIngresoAlmacenMP : Form
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
        CN_alm_movimientos objMovimientos = new CN_alm_movimientos();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_sun_unimed objUniMed = new CN_sun_unimed();
        CN_sun_tipope objTipOpe = new CN_sun_tipope();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sun_distritos objDis = new CN_sun_distritos();
        CN_sun_provincia objPro = new CN_sun_provincia();
        CN_sun_departamentos objDep = new CN_sun_departamentos();
        CN_alm_inventariolotes ObjAItemLotes = new CN_alm_inventariolotes();
        CN_sys_setup o_set = new CN_sys_setup();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_ALM_MOVIMIENTOS_CONSULTA BE_Movimiento = new BE_ALM_MOVIMIENTOS_CONSULTA();

        // DATATABLE LOCALES
        DataTable dtProveedor = new DataTable();
        DataTable dtTipoDocumento = new DataTable();
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
        DataTable dtDep = new DataTable();
        DataTable dtPro = new DataTable();
        DataTable dtDis = new DataTable();
        DataTable dtsetup = new DataTable();

        // VARIABLES LOCALES
        int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[10, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[10, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890./" + (char)8;                                        // + (char)8;
        string strNumerovalidos3 = "EFGR1234567890" + (char)8;       // + (char)8;
        string strNumerovalidos2 = "1234567890ABCDEFGHIJKLMNÑOPQRSTUVWXYZ" + (char)8; // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmIngresoAlmacenMP()
        {
            InitializeComponent();
        }
        private void FrmIngresoAlmacenMP_Load(object sender, EventArgs e)
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
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboAlmacen,dtAlmacenes, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipOpe, dtTipoExis, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtTipoDocumento, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipOpe, dtTipOpe, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboResponsable, dtResponsables, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboDocRef, dtTipoDocumento, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 587;
            this.Width = 945;
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            arrCabeceraFlex1[0, 0] = "Tipo Item";
            arrCabeceraFlex1[0, 1] = "0";
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Item";
            arrCabeceraFlex1[1, 1] = "180";
            arrCabeceraFlex1[1, 2] = "S";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Uni. Med.";
            arrCabeceraFlex1[2, 1] = "50";
            arrCabeceraFlex1[2, 2] = "S";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_itepredes";

            arrCabeceraFlex1[3, 0] = "Cantidad";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "0.000000";
            arrCabeceraFlex1[3, 4] = "n_can";

            arrCabeceraFlex1[4, 0] = "Nº Lote";
            arrCabeceraFlex1[4, 1] = "90";
            arrCabeceraFlex1[4, 2] = "C";
            arrCabeceraFlex1[4, 3] = "";
            arrCabeceraFlex1[4, 4] = "n_numlot";

            arrCabeceraFlex1[5, 0] = "Departamento";
            arrCabeceraFlex1[5, 1] = "90";
            arrCabeceraFlex1[5, 2] = "C";
            arrCabeceraFlex1[5, 3] = "";
            arrCabeceraFlex1[5, 4] = "d_fchpro";

            arrCabeceraFlex1[6, 0] = "Provincia";
            arrCabeceraFlex1[6, 1] = "80";
            arrCabeceraFlex1[6, 2] = "C";
            arrCabeceraFlex1[6, 3] = "";
            arrCabeceraFlex1[6, 4] = "d_fchven";

            arrCabeceraFlex1[7, 0] = "Distrito";
            arrCabeceraFlex1[7, 1] = "80";
            arrCabeceraFlex1[7, 2] = "C";
            arrCabeceraFlex1[7, 3] = "";
            arrCabeceraFlex1[7, 4] = "d_fchven";

            arrCabeceraFlex1[8, 0] = "Fundo / Hacienda / Lugar Origen";
            arrCabeceraFlex1[8, 1] = "120";
            arrCabeceraFlex1[8, 2] = "C";
            arrCabeceraFlex1[8, 3] = "dd/mm/yyyy";
            arrCabeceraFlex1[8, 4] = "d_fchven";

            arrCabeceraFlex1[9, 0] = "Hora Recepcion";
            arrCabeceraFlex1[9, 1] = "70";
            arrCabeceraFlex1[9, 2] = "H";
            arrCabeceraFlex1[9, 3] = "hh:mm";
            arrCabeceraFlex1[9, 4] = "d_fchven";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;                  // INDICAMOS EL NUMERO DE FILAS PARA EL DETALLE
            this.Text = dtForm.Rows[0]["c_titfor"].ToString() + " - Materia Prima"; 
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        void DataTableCargar()
        {
            ObjPro.mysConec = mysConec;
            dtProveedor = ObjPro.ListarProveedor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);                                         //

            objTipDoc.mysConec = mysConec;
            dtTipoDocumento = objTipDoc.Listar();                                           // 

            objTipoExi.mysConec = mysConec;
            dtTipoExis = objTipoExi.Listar();                                               //  

            o_set.mysConec = mysConec;
            dtsetup = o_set.TraerRegistro(); 

            ObjAlm.mysConec = mysConec;
            dtAlmacenes = ObjAlm.ListarNuevo(STU_SISTEMA.EMPRESAID, Convert.ToInt16(funFunciones.NulosN(dtsetup.Rows[0]["n_almunialmacenes"])));                             // 

            ObjRes.mysConec = mysConec;
            dtResponsables = ObjRes.Listar(STU_SISTEMA.EMPRESAID);  

            objItems.mysConec = mysConec;
            dtItems = objItems.ListarTabla(STU_SISTEMA.EMPRESAID);

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objMovimientos.mysConec = mysConec;
            dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 1, 1);

            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            objTipOpe.mysConec = mysConec;
            dtTipOpe = objTipOpe.ListarParaMovimientos(1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(2);

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(2, ref arrCabeceraDg1);

            ObjAlmDoc.mysConec = mysConec;
            dtDocAlm = ObjAlmDoc.ListarEmpAlm(STU_SISTEMA.EMPRESAID, 1);    // 1 FILTRAREMOS SOLO LOS ALMACENES QUE PERTENESCAN A LA EMPRESA Y QUE GENEREN MOVIMIENTO DE INGRESO A ALMACEN 

            objDep.mysConec = mysConec;
            dtDep = objDep.Listar();                                        // CARGAMOS TODOS LOS DEPARTAMENTOS

            objPro.mysConec = mysConec;
            dtPro = objPro.Listar();                                        // CARGAMOS TODAS LAS PROVINCIAS

            objDis.mysConec = mysConec;
            dtDis = objDis.Listar();                                        // CARGAMOS TODOS LOS DISTRITOS
        }
        void ListarItems()
        {
            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
            LblNumReg.Text = (dtMovimientos.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtMovimientos, true);
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
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
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

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, BE_Movimiento.lst_items, 2, false);

            int n_fila = 2;
            foreach (BE_ALM_MOVIMIENTOSDET_CONSULTA element in BE_Movimiento.lst_items)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;
                FgItems.SetData(n_fila, 1, element.c_tipexides);
                FgItems.SetData(n_fila, 2, element.c_itedes);
                FgItems.SetData(n_fila, 3, element.c_itepredes);
                FgItems.SetData(n_fila, 4, element.n_can);
                FgItems.SetData(n_fila, 5, element.c_numlot);

                string strCadenaFiltro;
                DataTable DtFiltro = new DataTable();
                string c_dato;

                strCadenaFiltro = "n_id = '" + element.n_iddep + "'";
                DtFiltro = funDatos.DataTableFiltrar(dtDep, strCadenaFiltro);
                c_dato = "";
                if (DtFiltro.Rows.Count != 0)
                {
                    c_dato = DtFiltro.Rows[0]["c_des"].ToString();
                }
                FgItems.SetData(n_fila, 6, c_dato);

                strCadenaFiltro = "n_iddep = " + element.n_iddep + "  AND n_id = " + element.n_idpro + "";
                DtFiltro = funDatos.DataTableFiltrar(dtPro, strCadenaFiltro);
                c_dato = "";
                if (DtFiltro.Rows.Count != 0)
                {
                    c_dato = DtFiltro.Rows[0]["c_des"].ToString();
                }
                FgItems.SetData(n_fila, 7, c_dato);

                strCadenaFiltro = "n_iddep = " + element.n_iddep + " AND n_idpro = " + element.n_idpro + " AND n_id = '" + element.n_iddis + "'";
                DtFiltro = funDatos.DataTableFiltrar(dtDis, strCadenaFiltro);
                c_dato = "";
                if (DtFiltro.Rows.Count != 0)
                {
                    c_dato = DtFiltro.Rows[0]["c_des"].ToString();
                }
                FgItems.SetData(n_fila, 8, c_dato);

                FgItems.SetData(n_fila, 9, element.c_desori);
                FgItems.SetData(n_fila, 10, funFunciones.f_Hora(element.h_horing));
               
                n_fila++;
            }
        }
        private void FrmIngresoAlmacenMP_Activated(object sender, EventArgs e)
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
        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (n_QueHace != 3) { return; }

            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }
        private void FrmIngresoAlmacenMP_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 97, this.Width - 18);
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
            FgItems.Cols[2].ComboList = "...";
            CboTipOpe.SelectedValue = 2;
            int n_idalm = Convert.ToInt16(dtAlmacenes.Rows[0]["n_id"]);
            CboAlmacen.SelectedValue = n_idalm;

            int n_idres = Convert.ToInt16(dtResponsables.Rows[0]["n_id"]);
            CboResponsable.SelectedValue = n_idres;
            
            txtFchIng.Focus();
        }
        void Blanquea()
        {
            txtFchIng.Text = "";
            TxtFchDoc.Text = "";
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtObs.Text = "";
            TxtNumDocRef.Text = "";
            TxtSerDocRef.Text = "";
            LblIdPro.Text = "";
            TxtNumRuc.Text = "";
            TxtProv.Text = "";

            CboTipOpe.SelectedValue = 0;
            CboTipDoc.SelectedValue = 0;
            
            CboAlmacen.SelectedValue = 0;
            CboDocRef.SelectedValue = 0;
            CboResponsable.SelectedValue = 0;
        }
        void Bloquea()
        {
            txtFchIng.Enabled = !txtFchIng.Enabled;
            TxtFchDoc.Enabled = !TxtFchDoc.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
            TxtNumDocRef.Enabled = !TxtNumDocRef.Enabled;
            TxtSerDocRef.Enabled = !TxtSerDocRef.Enabled;

            TxtNumRuc.Enabled = !TxtNumRuc.Enabled;
            CmdBusPro.Enabled = !CmdBusPro.Enabled;

            CboTipOpe.Enabled = !CboTipOpe.Enabled;
            CboTipDoc.Enabled = !CboTipDoc.Enabled;
            
            CboAlmacen.Enabled = !CboAlmacen.Enabled;
            CboDocRef.Enabled = !CboDocRef.Enabled;
            CboResponsable.Enabled = !CboResponsable.Enabled;
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
            //ToolImprimir.Enabled = !ToolImprimir.Enabled;
            ToolImprimir2.Enabled = !ToolImprimir2.Enabled;
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
            string c_numfac = DgLista.Columns["c_facnumdoc"].CellValue(DgLista.Row).ToString();                   // OBTENEMOS EL DOCUMENTO DE FACTURACION

            if (c_numfac != "")
            {
                MessageBox.Show("! No se puede modificar un ingreso de almacen que ya fue facturado, factura asignada es : " + c_numfac + " ¡", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
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
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            //string c_numfac = funFunciones.NulosC(DgLista.Columns["c_facnumdoc"].CellValue(DgLista.Row)).ToString();                   // OBTENEMOS EL DOCUMENTO DE FACTURACION

            //if (c_numfac != "")
            //{
            //    MessageBox.Show("! No se puede eliminar un ingreso de almacen que ya fue facturado¡", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    return booResult;
            //}
                        
            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            objMovimientos.mysConec = mysConec;
            BE_Movimiento = objMovimientos.TraerRegistro(intIdRegistro);

            if (DialogResult.Yes == Rpta)
            {
                //objMovimientos.AccConec = AccConec;
                if (objMovimientos.Eliminar(intIdRegistro, BE_Movimiento, 2) == true)   // INDICAMOS 2 = ENTRADA
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objMovimientos.mysConec = mysConec;
                    dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 1, 1);
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
                dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 1, 1);
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
                if (objMovimientos.DocumentoExiste(STU_SISTEMA.EMPRESAID,Convert.ToInt32(CboTipDoc.SelectedValue),TxtNumSer.Text, TxtNumDoc.Text, 1) == true)
                {
                    MessageBox.Show(" El numero de documento " + TxtNumSer.Text +"-"+TxtNumDoc.Text  + " ya existe, ingrese otro ", "",  MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return booResultado;
                }
                booResultado = objMovimientos.Insertar(BE_Movimiento, 2);    // INDICAMOS 2 = ENTRADA
            }

            if (n_QueHace == 2)
            {
                booResultado = objMovimientos.Actualizar(BE_Movimiento);
            }

            if (booResultado == false)
            {
                MessageBox.Show(" No se pudo guardar el registro por el siguiente motivo : " + objMovimientos.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {
            //BE_Movimiento.n_id;
            BE_Movimiento.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_Movimiento.n_idtipmov = 1;                            // 1 = INGRESO ; 2 = SALIDA
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
            BE_Movimiento.c_docrefnumdoc = TxtNumDocRef.Text;
            BE_Movimiento.c_docrefnumser = TxtSerDocRef.Text;
            BE_Movimiento.n_docrefidtipdoc = Convert.ToInt16(CboDocRef.SelectedValue);
            BE_Movimiento.n_tipite = 1;
            BE_Movimiento.n_perid = Convert.ToInt16(CboResponsable.SelectedValue);

            int n_fila = 0;
            int n_NumeroElementos = 0;

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

            string strCadenaFiltro;
            DataTable DtFiltro = new DataTable();
            int n_iddep;
            int n_idpro;
            int n_iddis;
            List<BE_ALM_MOVIMIENTOSDET_CONSULTA> LstDetalle = new List<BE_ALM_MOVIMIENTOSDET_CONSULTA>();

            if (FgItems.Rows.Count > 2)
            {
                for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
                {
                    if (funFunciones.NulosC(FgItems.GetData(n_fila, 2)) != "")
                    {
                        BE_ALM_MOVIMIENTOSDET_CONSULTA BE_Detalle = new BE_ALM_MOVIMIENTOSDET_CONSULTA();

                        BE_Detalle.n_idmov = 0;
                        BE_Detalle.n_can = Convert.ToDouble(FgItems.GetData(n_fila, 4).ToString());

                        //BE_Detalle.c_tipexides = FgItems.GetData(n_fila, 1).ToString();
                        BE_Detalle.c_itedes = FgItems.GetData(n_fila, 2).ToString();
                        BE_Detalle.c_itepredes = FgItems.GetData(n_fila, 3).ToString();
                        BE_Detalle.c_numlot = funFunciones.NulosC(FgItems.GetData(n_fila, 5));

                        if (funFunciones.NulosC(FgItems.GetData(n_fila, 6)) != "")
                        {
                            strCadenaFiltro = "c_des = '" + FgItems.GetData(n_fila, 6) + "'";
                            DtFiltro = funDatos.DataTableFiltrar(dtDep, strCadenaFiltro);
                            n_iddep = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());
                        }
                        else
                        {
                            n_iddep = 0;
                        }
                        
                        if (funFunciones.NulosC(FgItems.GetData(n_fila, 7)) != "")
                        {                     
                        strCadenaFiltro = "c_des = '" + FgItems.GetData(n_fila, 7) + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtPro, strCadenaFiltro);
                        n_idpro = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());
                        }
                        else
                        {
                            n_idpro = 0;
                        }                    
                        
                        if (funFunciones.NulosC(FgItems.GetData(n_fila, 7)) != "")
                        {
                            strCadenaFiltro = "c_des = '" + FgItems.GetData(n_fila, 8) + "'";
                            DtFiltro = funDatos.DataTableFiltrar(dtDis, strCadenaFiltro);
                            n_iddis = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());
                        }
                        else
                        {
                            n_iddis = 0;
                        }

                        BE_Detalle.n_iddep = n_iddep;
                        BE_Detalle.n_idpro = n_idpro;
                        BE_Detalle.n_iddis = n_iddis;
                        BE_Detalle.c_desori = funFunciones.NulosC(FgItems.GetData(n_fila, 9));

                        // FILTRAMOS EL ITEM DE LA FILA PARA OBTENER EL ID  
                        strCadenaFiltro = "c_despro = '" + BE_Detalle.c_itedes + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                        BE_Detalle.n_idite = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        // FILTRAMOS LA PRESENTACION PARA OBTENER SU ID
                        strCadenaFiltro = "c_abrpre = '" + BE_Detalle.c_itepredes + "' AND n_idite = " + BE_Detalle.n_idite + "";
                        DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                        BE_Detalle.n_idpre = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        // TIPO DE PRODUCTO SIEMPRE SERA MATERIA PRIMA
                        BE_Detalle.n_idtippro = 3;

                        BE_Detalle.d_fchpro = Convert.ToDateTime(txtFchIng.Text);                      // ASIGNAMOS LA FECHA DE PRODUCCION L AFECHA DE INGRESO A ALMACEN

                        int n_numdias;

                        strCadenaFiltro = "n_id = " + BE_Detalle.n_idite + "";
                        
                        DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                        n_numdias = Convert.ToInt32(DtFiltro.Rows[0]["n_numdiavid"].ToString());
                        DateTime d_fchven = funFunciones.FechaSumarDias(Convert.ToDateTime(txtFchIng.Text), n_numdias);  // CALCULAMOS LA FECHA DE VENCIMIETO EN FUNCION AL TIEMPO DE VIDA DEL ITEM
                        BE_Detalle.d_fchven = d_fchven;

                        //BE_Detalle.h_horing = Convert.ToDateTime(txtFchIng.Text + " " + FgItems.GetData(n_fila, 10));
                        BE_Detalle.h_horing = FgItems.GetData(n_fila, 10).ToString();
                        BE_Detalle.c_marca = "";
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

            if (Convert.ToInt16(LblIdPro.Text) == 0)
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
                    if (funFunciones.NulosC(FgItems.GetData(intFila, 2)) != "")
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
                        if (funFunciones.NulosC(FgItems.GetData(intFila, 4)) == "")
                        {
                            MessageBox.Show("¡ No ha especificado la cantidad del item en la fila " + (FgItems.Row - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
                        }
                        if (funFunciones.NulosC(FgItems.GetData(intFila, 5)) == "")
                        {
                            MessageBox.Show("¡ No ha especificado el numero de lote del item en la fila " + (FgItems.Row - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
                        }
                        if (funFunciones.NulosC(FgItems.GetData(intFila, 10)) == "")
                        {
                            MessageBox.Show("¡ No ha especificado la hora de ingreso del item en la fila " + (FgItems.Row - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
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
        void GenerarNumeroLote(int n_Fila, string c_DesItem)
        {
            DataTable dtResul = new DataTable();
            DataTable dtLotes = new DataTable();
            int n_idproducto;
            string n_numlot;
            booAgregando = true;
            // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
            dtResul = funDatos.DataTableFiltrar(dtItems, "c_despro = '" + c_DesItem + "'");
            if (dtResul.Rows.Count != 0)
            {
                n_idproducto = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());

                // FILTRAMOS EL ITEM SELECCIONADO
                dtResul = funDatos.DataTableFiltrar(dtItems, "n_id = " + n_idproducto + "");

                // TRAEMOS LOS LOTES REGISTRADOS EN EL DIA
                ObjAItemLotes.mysConec = mysConec;
                dtLotes = ObjAItemLotes.TraerLotesItem(STU_SISTEMA.EMPRESAID, n_idproducto, txtFchIng.Text);
                if (dtLotes.Rows.Count != 0)
                {
                    string c_numlote = "";
                    c_numlote = dtLotes.Rows[0]["c_numlot"].ToString();
                    int n_poscaracter = c_numlote.IndexOf("-");
                    int n_result = Convert.ToInt16(c_numlote.Substring(n_poscaracter + 1, 2));

                    n_result = n_result + 1;
                    n_numlot = dtResul.Rows[0]["c_prelot"].ToString() + Convert.ToDateTime(txtFchIng.Text).ToString("ddMMyy") + "-" + n_result.ToString("00");
                    FgItems.SetData(n_Fila, 5, n_numlot); 
                }
                else
                {
                    n_numlot = dtResul.Rows[0]["c_prelot"].ToString() + Convert.ToDateTime(txtFchIng.Text).ToString("ddMMyy") + "-01";
                    FgItems.SetData(n_Fila, 5, n_numlot);                                                       // NUMERO DE LOTE GENERADO
                }
            }
            booAgregando = false;
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

            if (FgItems.Col == 1)
            {
                FgItems.Select(e.Row - 1, 2);
                return;
            }
            if (FgItems.Col == 2)
            {
                MostrarUnidadMedida(e.Row, FgItems.GetData(e.Row, 2).ToString());
                GenerarNumeroLote(e.Row, FgItems.GetData(e.Row ,2).ToString());
                FgItems.Select(e.Row - 1, 3);
                return;
            }
            if (FgItems.Col == 3)
            {
                FgItems.Select(e.Row - 1, 4);
                return;
            }
            if (FgItems.Col == 4)
            {
                FgItems.Select(e.Row - 1, 5);
                return;
            }
            if (FgItems.Col == 5)
            {
                FgItems.SetData(FgItems.Row, 5, FgItems.GetData(FgItems.Row, 5).ToString().ToUpper());
                FgItems.Select(e.Row - 1, 6);
                return;
            }
            if (FgItems.Col == 6)
            {
                booAgregando = true;
                FgItems.SetData(e.Row, 7, "");
                FgItems.SetData(e.Row, 8, "");
                
                FgItems.Select(e.Row - 1, 7);
                booAgregando = false;
                return;
            }
            if (FgItems.Col == 7)
            {
                booAgregando = true;
                FgItems.SetData(e.Row, 8, "");
                FgItems.Select(e.Row - 1, 8);
                booAgregando = false;
                return;
            }
            if (FgItems.Col == 8)
            {
                FgItems.Select(e.Row - 1, 9);
                return;
            }
            if (FgItems.Col == 9)
            {
                FgItems.Select(e.Row - 1, 10);
                return;
            }
            if (FgItems.Col == 10)
            {
                FgItems.Select(e.Row, 2);
                return;
            }
        }
        private void CmdAddItem_Click(object sender, EventArgs e)
        {
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
            if (FgItems.Row <= 1) { return; }

            DataTable dtResul = new DataTable();
            DataTable DtFiltro = new DataTable();
            int n_idtipproducto = 0;
            string strDesTipPro = "";

            if (FgItems.Col == 1)
            {
                if (FgItems.Row >= 2)
                {
                    if (funFunciones.NulosC(FgItems.GetData(FgItems.Row - 1, 1)) == "") { FgItems.AllowEditing = false; return; }
                }
                funFlex.FlexColumnaCombo(FgItems, dtTipoExis, "c_des", 1);
            }

            if (FgItems.Col == 2)
            {
                FgItems.AllowEditing = true;
                FgItems.Cols[2].ComboList = "...";
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

                if (strDesTipPro == "")
                {
                    FgItems.AllowEditing = false;
                    return;
                }
            }
            if (FgItems.Col == 6)
            {
                funFlex.FlexColumnaCombo(FgItems, dtDep, "c_des", 6);
            }
            if (FgItems.Col == 7)
            {
                if (funFunciones.NulosC(FgItems.GetData(FgItems.Row, 6)) == "")
                {
                    FgItems.AllowEditing = false;
                }
                else
                {
                    int n_iddep;
                    string strCadenaFiltro = "c_des = '" + FgItems.GetData(FgItems.Row, 6) + "'";
                    DtFiltro = funDatos.DataTableFiltrar(dtDep, strCadenaFiltro);
                    n_iddep = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                    dtResul = funDatos.DataTableFiltrar(dtPro, "n_iddep = " + n_iddep.ToString() + "");
                    funFlex.FlexColumnaCombo(FgItems, dtResul, "c_des", 7);
                }
            }
            if (FgItems.Col == 8)
            {
                if (funFunciones.NulosC(FgItems.GetData(FgItems.Row, 7)) == "")
                {
                    FgItems.AllowEditing = false;
                }
                else
                {
                    int n_iddep;
                    string strCadenaFiltro = "c_des = '" + FgItems.GetData(FgItems.Row, 6) + "'";
                    DtFiltro = funDatos.DataTableFiltrar(dtDep, strCadenaFiltro);
                    n_iddep = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                    int n_idpro;
                    strCadenaFiltro = "c_des = '" + FgItems.GetData(FgItems.Row, 7) + "'";
                    DtFiltro = funDatos.DataTableFiltrar(dtPro, strCadenaFiltro);
                    n_idpro = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                    dtResul = funDatos.DataTableFiltrar(dtDis, "n_iddep = " + n_iddep.ToString() + " AND n_idpro = " + n_idpro.ToString() + "");
                    funFlex.FlexColumnaCombo(FgItems, dtResul, "c_des", 8);
                }              
            }
            FgItems.AllowEditing = true;
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
            if (e.Col == 9)
            {
                if (!strCaracteres.Contains(e.KeyChar))
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
            CboTipOpe.Focus();
        }
        private void CboTipDoc_SelectedValueChanged(object sender, EventArgs e)
        {

        }
        private void CboTipDoc_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            int n_IdTipDoc = Convert.ToInt16(CboTipDoc.SelectedValue);
            DataTable dtResul = funDatos.DataTableFiltrar(dtDocAlm, "n_idtipdoc = " + n_IdTipDoc.ToString() + "");
            booAgregando = true;
            FgItems.Rows.Count = 2;

            if (dtResul.Rows.Count != 0)
            { 
                if (Convert.ToInt16(dtResul.Rows[0]["n_numfil"].ToString()) != 0)
                {
                    FgItems.Rows.Count = FgItems.Rows.Count + Convert.ToInt16(dtResul.Rows[0]["n_numfil"].ToString());                  // INDICAMOS EL NUMERO DE FILAS PARA EL DETALLE
                }
                else
                {
                    FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;
                }
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
                if (!strNumerovalidos3.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void FgItems_ValidateEdit(object sender, C1.Win.C1FlexGrid.ValidateEditEventArgs e)
        {

        }
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            objMovimientos.mysConec = mysConec;
            dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 1, 1);    // CARGAMOS LOS DATOS DEL FORMULARIO
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

        private void CboMeses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TxtNumSer_Validated(object sender, EventArgs e)
        {
            if (TxtNumSer.Text == "") { return; }

            string strCad = "0000" + TxtNumSer.Text;
            string c_numdoc = "";

            TxtNumSer.Text = strCad.Substring(strCad.Length - 4, 4);
            c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboTipDoc.SelectedValue), TxtNumSer.Text);
            TxtNumDoc.Text = c_numdoc;
        }
        private void label8_Click(object sender, EventArgs e)
        {

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
        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text == "") { return; }

            string strCad = "0000000000" + TxtNumDoc.Text;

            TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
        }

        private void TxtNumDocRef_Validated(object sender, EventArgs e)
        {
            if (TxtNumDocRef.Text == "") { return; }

            string strCad = "0000000000" + TxtNumDocRef.Text;

            TxtNumDocRef.Text = strCad.Substring(strCad.Length - 10, 10);
        }

        private void TxtSerDocRef_Validated(object sender, EventArgs e)
        {
            if (TxtSerDocRef.Text == "") { return; }

            string strCad = "0000" + TxtSerDocRef.Text;

            TxtSerDocRef.Text = strCad.Substring(strCad.Length - 4, 4);
        }

        private void ToolImprimir_Click(object sender, EventArgs e)
        {
            CN_alm_movimientos objAlm = new CN_alm_movimientos();
            objAlm.STU_SISTEMA = STU_SISTEMA;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            objAlm.ReportImprimirNotaIngreso(intIdRegistro, 1);
        }

        private void CboDocRef_SelectedValueChanged(object sender, EventArgs e)
        {
            TxtSerDocRef.Focus();
        }

        private void ToolManFun_Click(object sender, EventArgs e)
        {
            Helper.Cls_VisorHTML VerHtml = new Helper.Cls_VisorHTML();
            VerHtml.c_NombreArchivo = "j:\\SSF-NET\\archivos\\documentacion\\Procesos\\proceso01.html";
            VerHtml.c_TituloForm = "Manual Funciones - Almacen";
            VerHtml.VerHtml();
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
                //TxtNumRuc.Focus();
            }
        }
        private void FgItems_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgItems.Col == 2)
            {
                int n_idtipexi = 3;
                DataTable dtResul = new DataTable();
                string c_dato = "";

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

                        //c_dato = dtResul.Rows[0]["n_id"].ToString();            // MOSTRAMOS EL ID DEL ITEM
                        //FgItems.SetData(FgItems.Row, 9, c_dato);

                        //c_dato = dtResul.Rows[0]["n_idunimed"].ToString();      // MOSTRAMOS LA UNIDAD DE MEDIDA DEL ITEM
                        //FgItems.SetData(FgItems.Row, 10, c_dato);
                    }
                }
                booAgregando = false;
            }
        }
        private void TxtNumRuc_TextChanged(object sender, EventArgs e)
        {

        }
        private void TxtProv_TextChanged(object sender, EventArgs e)
        {

        }

        private void LblIdPro_Click(object sender, EventArgs e)
        {

        }
        private void imgresosSinDocDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_alm_movimientos o_mov = new CN_alm_movimientos();
            o_mov.mysConec = mysConec;
            o_mov.STU_SISTEMA = STU_SISTEMA;
            o_mov.ReportConsulta9(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 2);
        }
        private void ingresosConDocDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_alm_movimientos o_mov = new CN_alm_movimientos();
            o_mov.mysConec = mysConec;
            o_mov.STU_SISTEMA = STU_SISTEMA;
            o_mov.ReportConsulta9(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 1);
        }
        private void ingresosSinDocDeProduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_alm_movimientos o_mov = new CN_alm_movimientos();
            o_mov.mysConec = mysConec;
            o_mov.STU_SISTEMA = STU_SISTEMA;
            o_mov.ReportConsulta10(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 2);
        }
        private void ingresosSinDocDeProduccionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CN_alm_movimientos o_mov = new CN_alm_movimientos();
            o_mov.mysConec = mysConec;
            o_mov.STU_SISTEMA = STU_SISTEMA;
            o_mov.ReportConsulta10(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 1);
        }
    }
}
