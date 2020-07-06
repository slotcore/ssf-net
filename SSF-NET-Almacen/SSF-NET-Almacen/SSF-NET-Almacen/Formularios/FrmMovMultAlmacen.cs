﻿using System;
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
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Almacen;
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

namespace SSF_NET_Almacen.Formularios
{
    public partial class FrmMovMultAlmacen : Form
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
        CN_alm_inventariolotes ObjLotes = new CN_alm_inventariolotes();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_personaladjunto objPerAdj = new CN_sys_personaladjunto();
        CN_sys_empresa funsys = new CN_sys_empresa();
        CN_alm_movimientos objMovimientos = new CN_alm_movimientos();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_sun_unimed objUniMed = new CN_sun_unimed();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sun_tipope objTipOpe = new CN_sun_tipope();
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
        List<BE_ALM_MOVIMIENTOS_CONSULTA> BE_l_Movimiento = new List<BE_ALM_MOVIMIENTOS_CONSULTA>();
        BE_ALM_MOVIMIENTOS_CONSULTA BE_Movimiento = new BE_ALM_MOVIMIENTOS_CONSULTA();
        BE_SYS_EMPRESA entEmp = new BE_SYS_EMPRESA();

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
        DataTable dtPerAdj = new DataTable();
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
        //string[,] arrCabeceraDg1 = new string[10, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[10, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;
        int n_ItemProducido;                                                            // ESTA VARIABLE ALMACENARA EL ID DEL ITEM PRODUCIDO CUANDO SE SELECCIONE UNA ORDEN DE PRODUCCION
        
        public FrmMovMultAlmacen()
        {
            InitializeComponent();
        }

        private void FrmSalidaAlmacen3_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            Nuevo();
            booAgregando = false;
        }

        void CargarCombos()
        {
            DataTable dtResul = new DataTable();
            DataTableCargar();
            
            dtResul = funDatos.DataTableFiltrar(dtTipoDocumento2, "n_id IN (2, 5, 4, 72, 75, 10, 32, 88, 92)");
            funDatos.ComboBoxCargarDataTable(CboDocRef, dtResul, "n_id", "c_des");            
            funDatos.ComboBoxCargarDataTable(CboTipOpe, dtTipOpe, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboSolicitante, dtPerAdj, "n_id", "c_apenom");
        }

        void ConfigurarFormulario()
        {
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

            arrCabeceraFlex1[0, 0] = "Tipo Item";
            arrCabeceraFlex1[0, 1] = "100";
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Item";
            if (N_INGTRAZABALIDAD == 1) { arrCabeceraFlex1[1, 1] = "280"; }
            if (N_INGTRAZABALIDAD == 0) { arrCabeceraFlex1[1, 1] = "400"; }
            arrCabeceraFlex1[1, 2] = "S";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Uni. Med.";
            arrCabeceraFlex1[2, 1] = "40";
            arrCabeceraFlex1[2, 2] = "S";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_itepredes";

            arrCabeceraFlex1[3, 0] = "Nº Lote";
            if (N_INGTRAZABALIDAD == 1) { arrCabeceraFlex1[3, 1] = "80"; }
            if (N_INGTRAZABALIDAD == 0) { arrCabeceraFlex1[3, 1] = "0"; }
            arrCabeceraFlex1[3, 2] = "C";
            arrCabeceraFlex1[3, 3] = "";
            arrCabeceraFlex1[3, 4] = "n_numlot";

            arrCabeceraFlex1[4, 0] = "Cantidad en Almacen";
            arrCabeceraFlex1[4, 1] = "0";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.000000";
            arrCabeceraFlex1[4, 4] = "n_can";

            arrCabeceraFlex1[5, 0] = "Cantidad Requerida";
            arrCabeceraFlex1[5, 1] = "70";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.000000";
            arrCabeceraFlex1[5, 4] = "n_can";

            arrCabeceraFlex1[6, 0] = "Precio Unitario";
            if (N_INGPRECIO == 1) { arrCabeceraFlex1[6, 1] = "70"; }
            if (N_INGPRECIO == 0) { arrCabeceraFlex1[6, 1] = "0"; }
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_can";

            arrCabeceraFlex1[7, 0] = "Imp. Total";
            if (N_INGPRECIO == 1) { arrCabeceraFlex1[7, 1] = "70"; }
            if (N_INGPRECIO == 0) { arrCabeceraFlex1[7, 1] = "0"; }
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "n_can";

            arrCabeceraFlex1[8, 0] = "Hora Entrega";
            arrCabeceraFlex1[8, 1] = "60";
            arrCabeceraFlex1[8, 2] = "H";
            arrCabeceraFlex1[8, 3] = "HH:mm";
            arrCabeceraFlex1[8, 4] = "n_can";

            arrCabeceraFlex1[9, 0] = "Almacén";
            arrCabeceraFlex1[9, 1] = "250";
            arrCabeceraFlex1[9, 2] = "S";
            arrCabeceraFlex1[9, 3] = "";
            arrCabeceraFlex1[9, 4] = "c_almdes";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);
        }

        void DataTableCargar()
        {
            ObjPro.mysConec = mysConec;
            dtProveedor = ObjPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);                                         //

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
            dtItems = objItems.ListarTabla(STU_SISTEMA.EMPRESAID);

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objMovimientos.mysConec = mysConec;
            dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 2, 0);  // EL PARAMETROS N_IDTIPITEM = 0  MOSTRARA TODAS LAS SALIDAS

            objTipOpe.mysConec = mysConec;
            dtTipOpe = objTipOpe.ListarParaMovimientos(2);

            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            objPerAdj.mysConec = mysConec;
            dtPerAdj = objPerAdj.Listar(STU_SISTEMA.EMPRESAID);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(3);

            ObjAlmDoc.mysConec = mysConec;
            dtDocAlm = ObjAlmDoc.ListarEmpAlm(STU_SISTEMA.EMPRESAID, 2);               // 1 FILTRAREMOS SOLO LOS ALMACENES QUE PERTENESCAN A LA EMPRESA Y QUE GENEREN MOVIMIENTO DE INGRESO A ALMACEN 

            objlotes.mysConec = mysConec;
            dtLotes = objlotes.TraerLotesConSaldo(STU_SISTEMA.EMPRESAID);
            
            funsys.mysConec = mysConec;
            funsys.TraerRegistro(STU_SISTEMA.EMPRESAID);
            entEmp = funsys.e_Empresa;
        }

        void Nuevo()
        {
            booAgregando = true;
            n_QueHace = 1;
            Blanquea();
            Bloquea();
            FgItems.Rows.Count = 2;
            booAgregando = false;
            FgItems.Cols[2].ComboList = "...";
            txtFchIng.Focus();
        }

        void Blanquea()
        {
            txtFchIng.Text = "";
            TxtFchDoc.Text = "";
            TxtObs.Text = "";
            TxtSerDocRef.Text = "";
            TxtNumDocRef.Text = "";

            TxtNumRuc.Text = "";
            LblIdPro.Text = "";
            TxtProv.Text = "";
            
            CboSolicitante.SelectedValue = 0;
            CboTipOpe.SelectedValue = 0;
            CboSolicitante.SelectedValue = 0;
            CboDocRef.SelectedValue = 0;

            CmdBusDocRef.Visible = false;
            LblIdDocRef.Text = "";
        }

        void Bloquea()
        {
            txtFchIng.Enabled = !txtFchIng.Enabled;
            TxtFchDoc.Enabled = !TxtFchDoc.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
            TxtSerDocRef.Enabled = !TxtSerDocRef.Enabled;
            TxtNumDocRef.Enabled = !TxtNumDocRef.Enabled;

            TxtNumRuc.Enabled = !TxtNumRuc.Enabled;
            TxtProv.Enabled = !TxtProv.Enabled;
            CmdBusPro.Enabled = !CmdBusPro.Enabled;

            CboTipOpe.Enabled = !CboTipOpe.Enabled;
            CboSolicitante.Enabled = !CboSolicitante.Enabled;
            CboDocRef.Enabled = !CboDocRef.Enabled;

            CmdAddItem.Enabled = !CmdAddItem.Enabled;
            CmdDelItem.Enabled = !CmdDelItem.Enabled;
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
                objMovimientos.n_ItemProducido = n_ItemProducido;
                booResultado = objMovimientos.InsertarLista(BE_l_Movimiento, 1);    // INDICAMOS 1 = SALIDA
            }
            return booResultado;
        }

        void AsignarEntidad()
        {
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

            List<BE_ALM_MOVIMIENTOSDET_CONSULTA> LstDetalle = new List<BE_ALM_MOVIMIENTOSDET_CONSULTA>();
            BE_l_Movimiento.Clear();
            if (FgItems.Rows.Count > 2)
            {
                for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
                {
                    if (funFunciones.NulosC(FgItems.GetData(n_fila, 1)) != "")
                    {
                        DataTable DtFiltro = new DataTable();

                        string strCadenaFiltro = "c_des = '" + FgItems.GetData(n_fila, 10).ToString() + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtAlmacenes, strCadenaFiltro);
                        int n_idalm = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        BE_ALM_MOVIMIENTOS_CONSULTA BE_Movimiento = BE_l_Movimiento
                                                                    .Where(m => m.n_idalm == n_idalm)
                                                                    .FirstOrDefault();
                        bool m_AlmacenNuevo = false;

                        if (BE_Movimiento == null)
                        {
                            m_AlmacenNuevo = true;
                            BE_Movimiento = new BE_ALM_MOVIMIENTOS_CONSULTA();
                            BE_Movimiento.n_idemp = STU_SISTEMA.EMPRESAID;
                            BE_Movimiento.n_idtipmov = 2;
                            BE_Movimiento.n_idclipro = Convert.ToInt16(LblIdPro.Text);
                            BE_Movimiento.d_fchdoc = Convert.ToDateTime(TxtFchDoc.Text);
                            BE_Movimiento.d_fching = Convert.ToDateTime(txtFchIng.Text);
                            BE_Movimiento.n_idtipdoc = 50;
                            BE_Movimiento.c_numser = "001";
                            BE_Movimiento.c_numdoc = objTipDoc.UltimoNumero(BE_Movimiento.n_idemp
                                , BE_Movimiento.n_idtipdoc
                                , BE_Movimiento.c_numser);
                            BE_Movimiento.n_idalm = n_idalm;
                            BE_Movimiento.n_anotra = STU_SISTEMA.ANOTRABAJO;
                            BE_Movimiento.n_idmes = STU_SISTEMA.MESTRABAJO;
                            BE_Movimiento.c_obs = TxtObs.Text; ;
                            BE_Movimiento.n_idtipope = Convert.ToInt16(CboTipOpe.SelectedValue);
                            BE_Movimiento.n_perid = Convert.ToInt16(CboSolicitante.SelectedValue);
                            BE_Movimiento.n_docrefidtipdoc = Convert.ToInt16(CboDocRef.SelectedValue);
                            BE_Movimiento.c_docrefnumser = TxtSerDocRef.Text;
                            BE_Movimiento.c_docrefnumdoc = TxtNumDocRef.Text;

                            BE_Movimiento.n_docrefiddocref = null;
                            if (LblIdDocRef.Text != "")
                            {
                                BE_Movimiento.n_docrefiddocref = Convert.ToInt32(LblIdDocRef.Text);
                            }

                        }
                        
                        BE_ALM_MOVIMIENTOSDET_CONSULTA BE_Detalle = new BE_ALM_MOVIMIENTOSDET_CONSULTA();

                        BE_Detalle.n_idmov = 0;
                        BE_Detalle.n_can = Convert.ToDouble(FgItems.GetData(n_fila, 6).ToString());

                        BE_Detalle.c_tipexides = FgItems.GetData(n_fila, 1).ToString();
                        BE_Detalle.c_itedes = FgItems.GetData(n_fila, 2).ToString();
                        BE_Detalle.c_itepredes = FgItems.GetData(n_fila, 3).ToString();
                        BE_Detalle.c_numlot = funFunciones.NulosC(FgItems.GetData(n_fila, 4));


                        // FILTRAMOS EL ITEM DE LA FILA PARA OBTENER EL ID  
                        strCadenaFiltro = "c_despro = '" + BE_Detalle.c_itedes + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                        BE_Detalle.n_idite = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        // FILTRAMOS LA PRESENTACION PARA OBTENER SU ID
                        strCadenaFiltro = "c_abrpre = '" + BE_Detalle.c_itepredes + "' AND n_idite = " + BE_Detalle.n_idite + "";
                        DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                        BE_Detalle.n_idpre = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        // FILTRAMOS EL TIPO DE PRODUCTO PARA OBTENER SU id
                        strCadenaFiltro = "c_des = '" + BE_Detalle.c_tipexides + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtTipoExis, strCadenaFiltro);
                        BE_Detalle.n_idtippro = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());
                        BE_Detalle.h_horsal = funFunciones.NulosC(FgItems.GetData(n_fila, 9));
                        BE_Detalle.n_preuni = Convert.ToDouble(FgItems.GetData(n_fila, 7));
                        BE_Detalle.n_pretot = Convert.ToDouble(FgItems.GetData(n_fila, 8));

                        BE_Movimiento.lst_items.Add(BE_Detalle);

                        if (m_AlmacenNuevo)
                        {
                            BE_l_Movimiento.Add(BE_Movimiento);
                        }
                    }
                }
            }
            booAgregando = false;
        }

        bool CamposOK()
        {
            bool booEstado = true;

            if (txtFchIng.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de ingreso !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                txtFchIng.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtFchDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchDoc.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(LblIdPro.Text) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre del proveedor !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumRuc.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboTipOpe.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de operacion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboTipOpe.Focus();
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
                        if (funFunciones.NulosC(FgItems.GetData(intFila, 10)) == "")
                        {
                            MessageBox.Show("¡ No ha especificado el almacén del item en la fila " + (FgItems.Row - 1).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
                        }
                    }
                }

                // VERIFICAMOS QUE LA CANTIDAD DE INSUMOS REPARTIDOS EN LOS LOTES SEA IGUAL AL PEDIDO ORIGINAL
                int n_idite = 0;
                int n_iditedet = 0;
                double n_can = 0;
                double n_candet = 0;
                int n_row = 0;
                string c_dato = "";
                string c_dato2 = "";

                for (intFila = 0; intFila <= DtDetalleTMP.Rows.Count - 1; intFila++)
                {
                    n_idite = Convert.ToInt32(DtDetalleTMP.Rows[intFila]["iditem"]);
                    c_dato2 = DtDetalleTMP.Rows[intFila]["descripcion"].ToString();
                    if (Convert.ToInt16(CboDocRef.SelectedValue) == 73)
                    {
                        n_can = Convert.ToDouble(DtDetalleTMP.Rows[intFila]["canutil"]);
                    }
                    if (Convert.ToInt16(CboDocRef.SelectedValue) == 10)
                    {
                        n_can = Convert.ToDouble(DtDetalleTMP.Rows[intFila]["canpro"]);
                    }
                    
                    n_candet =  0;
                    for (n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
                    { 
                        c_dato = FgItems.GetData(n_row,2).ToString();
                        n_iditedet = Convert.ToInt32(funDatos.DataTableBuscar(dtItems, "c_despro", "n_id", c_dato, "C").ToString());
                        if (n_iditedet == n_idite)
                        {
                            n_candet = n_candet + Convert.ToDouble(FgItems.GetData(n_row, 6).ToString());
                            break;
                        }
                    }
                    if (Math.Round(n_candet,6) != Math.Round(n_can,6))
                    {
                        MessageBox.Show("¡ La cantidad del item " + c_dato2 + " no coincide con la cantidad original !\n" + "Cant. Original = " + n_can.ToString("0.000000") + " Cant. Registrada = " + n_candet.ToString("0.000000"), "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        booEstado = false;
                        return booEstado;
                    }
                }
            }
            else
            {
                MessageBox.Show("¡ No ha especificado ningun item para este ingreso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            return booEstado;
        }     
        
        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            if (Grabar() == true)
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objMovimientos.mysConec = mysConec;
                dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 2, 0);
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                Blanquea();
                MessageBox.Show("! El registro se agregó con exito ¡ ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 4)
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
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
            if (FgItems.Col == 5)
            {
                FgItems.AllowEditing = false;
                return;
            }


            if (FgItems.Col == 10)
            {
                if (FgItems.Row >= 2)
                {
                    if (funFunciones.NulosC(FgItems.GetData(FgItems.Row - 1, 1)) == "") { FgItems.AllowEditing = false; return; }
                }
                funFlex.FlexColumnaCombo(FgItems, dtAlmacenes, "c_des", 10);
            }

            FgItems.AllowEditing = true;
        }

        private void CmdDelItem_Click(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count > 2)
            {
                FgItems.RemoveItem(FgItems.Row);
                FgItems.Focus();
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

        private void FgItems_RowColChange(object sender, EventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }
        }

        void CargarLotes(int n_iditem)
        {   
            DataTable dtResul = new DataTable();
            int n_row;
            int n_filadt;

            objlotes.mysConec = mysConec;
            dtLotes = objlotes.TraerLotesConSaldo(STU_SISTEMA.EMPRESAID);

            dtResul = funDatos.DataTableFiltrar(dtLotes, "n_idite = " + n_iditem + "");

            FgItems.SetData(FgItems.Row, 4, "");
            FgItems.SetData(FgItems.Row, 5, "");

            if (dtResul.Rows.Count != 0)
            {
                // RECORREMOS LOS ITEMS CARGADOS PARA ACTUALIZAR EL SALDO DE LOS LOTES
                for (n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
                {
                    string c_numlote;
                    double n_cantidad;

                    c_numlote = funFunciones.NulosC(FgItems.GetData(n_row, 4));
                    n_cantidad = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 6)));

                    if (c_numlote != "")
                    {
                        for (n_filadt = 0; n_filadt <= dtResul.Rows.Count - 1; n_filadt++)
                        {
                            if (dtResul.Rows[n_filadt]["c_numlot"].ToString() == c_numlote)
                            {
                                dtResul.Rows[n_filadt]["n_saldo"] = (Convert.ToDouble(dtResul.Rows[n_filadt]["n_saldo"]) - n_cantidad);
                                break;
                            }
                        }
                    }
                }
                dtResul = funDatos.DataTableFiltrar(dtResul, "n_saldo <> 0");
                funFlex.FlexColumnaCombo(FgItems, dtResul, "c_numlot", 4);

                if (dtResul.Rows.Count != 0)
                {
                    FgItems.SetData(FgItems.Row, 4, funFunciones.NulosC(dtResul.Rows[0]["c_numlot"].ToString()));
                    FgItems.SetData(FgItems.Row, 5, Convert.ToDouble(funFunciones.NulosN(dtResul.Rows[0]["n_saldo"].ToString())).ToString("0.000000"));
                }
            }
            else
            {
                FgItems.SetData(FgItems.Row, 4, "SIN LOTE");
                FgItems.SetData(FgItems.Row, 5, "0.000000");
            }
        }

        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            DataTable dtResul;
            string c_despro;
            int n_idpro;

            if (n_QueHace == 3)
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
                booAgregando = true;

                // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
                c_despro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 2));
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
                        
                        CargarLotes(n_idpro);
                    }
                    else
                    {
                        FgItems.SetData(FgItems.Row, 3, "");
                    }
                }
                FgItems.SetData(FgItems.Row, 7, DateTime.Now.ToString("HH:mm"));

                FgItems.Select(FgItems.Row - 1, 3);
                booAgregando = false;
                return;
            }
            if (FgItems.Col == 3)
            {
                FgItems.Select(FgItems.Row - 1, 4);
                return;
            }
            if (FgItems.Col == 4)
            {
                FgItems.Select(FgItems.Row - 1, 5);
                return;
            }
            if (FgItems.Col == 5)
            {
                FgItems.Select(FgItems.Row - 1, 6);
                return;
            }
            if (FgItems.Col == 6)
            {
                double n_saldo = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row,5)));
                double n_cansol = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row,6)));
                string c_numlote = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 4));

                //if (c_numlote != "")
                //{
                //    if (n_cansol > n_saldo)
                //    {
                //        //FgItems.SetData(FgItems.Row, 6, "");
                //        MessageBox.Show("La cantidad solicitada excede la capacidad del lote, ingrese una cantidad menor o igual al lote", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //    }
                //    else
                //    {
                //        FgItems.SetData(FgItems.Row, 6, n_cansol.ToString("0.000000"));
                //    }
                //}
                //else
                //{
                //    FgItems.SetData(FgItems.Row, 6, n_cansol.ToString("0.000000"));
                //}
                return;
            }
            if (FgItems.Col == 7)
            {
                FgItems.Select(FgItems.Row, 1);
                return;
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

        private void TxtSerDocRef_Validated(object sender, EventArgs e)
        {
            if (TxtSerDocRef.Text == "") { return; }

            string strCad = "0000" + TxtSerDocRef.Text;
            TxtSerDocRef.Text = strCad.Substring(strCad.Length - 4, 4);
        }

        private void TxtNumDocRef_Validated(object sender, EventArgs e)
        {
            if (TxtNumDocRef.Text == "") { return; }

            string strCad = "0000000000" + TxtNumDocRef.Text;
            TxtNumDocRef.Text = strCad.Substring(strCad.Length - 10, 10);
        }

        private void CboDocRef_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            //if (Convert.ToInt16(CboAlmacen.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha indicado el almacen", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    CboDocRef.SelectedValue = 0;
            //    return;
            //}
            TxtNumDocRef.Enabled = true;
            TxtSerDocRef.Enabled = true;
            CmdBusDocRef.Visible = false;

            CmdAddItem.Enabled = true;
            CmdDelItem.Enabled = true;
            CboTipOpe.SelectedValue = 0;
            if (Convert.ToInt32(CboDocRef.SelectedValue) == 2)
            {
                TxtNumDocRef.Text = "";
                TxtSerDocRef.Text = "";
                TxtNumDocRef.Enabled = false;
                TxtSerDocRef.Enabled = false;
                //CmdBusDocRef.Text = "FACTURA";
                CmdBusDocRef.Visible = true;

                CmdAddItem.Enabled = false;
                CmdDelItem.Enabled = false;
                booAgregando = true;
                CboTipOpe.SelectedValue = 1;
                booAgregando = false;
            }

            if (Convert.ToInt32(CboDocRef.SelectedValue) == 10)
            {
                TxtNumDocRef.Text = "";
                TxtSerDocRef.Text = "";
                TxtNumDocRef.Enabled = false;
                TxtSerDocRef.Enabled = false;
                //CmdBusDocRef.Text = "GUIA REMISION";
                CmdBusDocRef.Visible = true;

                CmdAddItem.Enabled = false;
                CmdDelItem.Enabled = false;
                CboTipOpe.SelectedValue = 1;
            }

            if (Convert.ToInt32(CboDocRef.SelectedValue) == 72) 
            {
                TxtNumDocRef.Text = "";
                TxtSerDocRef.Text = "";
                TxtNumDocRef.Enabled = false;
                TxtSerDocRef.Enabled = false;
                CmdBusDocRef.Visible = true;
                //CmdBusDocRef.Text = "Solicitud Mat.";
                CmdBusDocRef.Visible = true;

                CmdAddItem.Enabled = false;
                CmdDelItem.Enabled = false;
                booAgregando = true;
                CboTipOpe.SelectedValue = 10;
                //CboTipDoc.SelectedValue = 72;
                booAgregando = false; ;
            }

            if (Convert.ToInt32(CboDocRef.SelectedValue) == 75)
            {
                TxtNumDocRef.Text = "";
                TxtSerDocRef.Text = "";
                TxtNumDocRef.Enabled = false;
                TxtSerDocRef.Enabled = false;
                CmdBusDocRef.Visible = true;
                //CmdBusDocRef.Text = "Orden de Req.";
                CmdBusDocRef.Visible = true;

                //CmdAddItem.Enabled = false;
                //CmdDelItem.Enabled = false;
                booAgregando = true;
                CboTipOpe.SelectedValue = 10;
                //CboTipDoc.SelectedValue = 75;
                booAgregando = false; ;
            }

            if (Convert.ToInt32(CboDocRef.SelectedValue) == 92)
            {
                TxtNumDocRef.Text = "";
                TxtSerDocRef.Text = "";
                TxtNumDocRef.Enabled = false;
                TxtSerDocRef.Enabled = false;
                CmdBusDocRef.Visible = true;
                //CmdBusDocRef.Text = "Solicitud Adic. Mat.";
                CmdBusDocRef.Visible = true;

                CmdAddItem.Enabled = false;
                CmdDelItem.Enabled = false;
                booAgregando = true;
                CboTipOpe.SelectedValue = 10;
                //CboTipDoc.SelectedValue = 92;
                booAgregando = false; ;
            }

            if (Convert.ToInt32(CboDocRef.SelectedValue) == 73)
            {
                TxtNumDocRef.Text = "";
                TxtSerDocRef.Text = "";
                TxtNumDocRef.Enabled = false;
                TxtSerDocRef.Enabled = false;
                //CmdBusDocRef.Text = "Parte Produccion";
                CmdBusDocRef.Visible = true;

                CmdAddItem.Enabled = false;
                CmdDelItem.Enabled = false;
                CboTipOpe.SelectedValue = 10;
            }
            
            if (Convert.ToInt32(CboDocRef.SelectedValue) == 32)
            {
                TxtNumDocRef.Text = "";
                TxtSerDocRef.Text = "";
                TxtNumDocRef.Enabled = false;
                TxtSerDocRef.Enabled = false;
                //CmdBusDocRef.Text = "Guia Transporte";
                CmdBusDocRef.Visible = true;

                CmdAddItem.Enabled = false;
                CmdDelItem.Enabled = false;
                CboTipOpe.SelectedValue = 11;
            }
            if (Convert.ToInt32(CboDocRef.SelectedValue) == 88)
            {
                TxtNumDocRef.Text = "";
                TxtSerDocRef.Text = "";
                TxtNumDocRef.Enabled = false;
                TxtSerDocRef.Enabled = false;
                //CmdBusDocRef.Text = "Proforma";
                CmdBusDocRef.Visible = true;

                CmdAddItem.Enabled = false;
                CmdDelItem.Enabled = false;
                booAgregando = true;
                CboTipOpe.SelectedValue = 1;
                booAgregando = false;
            }
            TxtSerDocRef.Focus();
        }

        void MostrarParteProduccion()
        {
            string[,] arrCabeceraDg1 = new string[6, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            int n_Fila;
            int n_iditem;
            int n_filaflex;
            double n_Cantidad = 0;
            int n_IdReceta;

            objProPro.AccConec = AccConec;
            DtSolMat = objProPro.ProduccionSinSalidaItem();

            arrCabeceraDg1[0, 0] = "Nº Parte Produccion";
            arrCabeceraDg1[0, 1] = "80";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "numparte";

            arrCabeceraDg1[1, 0] = "Producto";
            arrCabeceraDg1[1, 1] = "350";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "prodes";

            arrCabeceraDg1[2, 0] = "Responsable";
            arrCabeceraDg1[2, 1] = "250";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "c_nomenc";

            arrCabeceraDg1[3, 0] = "Cantidad Producidas";
            arrCabeceraDg1[3, 1] = "80";
            arrCabeceraDg1[3, 2] = "N";
            arrCabeceraDg1[3, 3] = "cantidad";

            arrCabeceraDg1[4, 0] = "Fch. Produccion";
            arrCabeceraDg1[4, 1] = "80";
            arrCabeceraDg1[4, 2] = "F";
            arrCabeceraDg1[4, 3] = "dia";

            arrCabeceraDg1[5, 0] = "Id";
            arrCabeceraDg1[5, 1] = "0";
            arrCabeceraDg1[5, 2] = "C";
            arrCabeceraDg1[5, 3] = "c_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "c_id";
            xFun.Buscar_CadFiltro = "";
            xFun.Buscar_CampoOrden = "dia";
            dtResult = xFun.Buscar(arrCabeceraDg1, DtSolMat);

            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }

            n_ItemProducido = Convert.ToInt16(dtResult.Rows[0]["iditem"].ToString());
            n_IdReceta = Convert.ToInt16(dtResult.Rows[0]["idrec"].ToString());
            TxtNumDocRef.Text = dtResult.Rows[0]["numparte"].ToString();
            TxtSerDocRef.Text = "0001";
            LblIdDocRef.Text = dtResult.Rows[0]["idpro"].ToString();

            CN_ACC_pro_producciondet objProdDetalle = new CN_ACC_pro_producciondet();
            objProdDetalle.AccConec = AccConec;
            DataTable dtItemFiltro = new DataTable();
            objProdDetalle.n_ItemProducido = n_ItemProducido;
            DtDetalleTMP = objProdDetalle.ProduccionInsumos(Convert.ToInt16(LblIdDocRef.Text), n_IdReceta);
            dtResult = DtDetalleTMP;
            n_filaflex = 2;
            FgItems.Rows.Count = 2;
           
            booAgregando = true;
            for (n_Fila = 0; n_Fila <= (dtResult.Rows.Count - 1); n_Fila++)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;
                n_iditem = Convert.ToInt32(dtResult.Rows[n_Fila]["iditem"].ToString());
                
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
                n_Cantidad = Convert.ToDouble(dtResult.Rows[n_Fila]["canutil"]);
                FgItems.SetData(n_filaflex, 4, "");
                FgItems.SetData(n_filaflex, 6, n_Cantidad.ToString("0.000000"));
                FgItems.SetData(n_filaflex, 7, DateTime.Now.ToString("HH:mm"));
                n_filaflex = n_filaflex + 1;
            }

            booAgregando = false;
        }

        void MostrarGuiasPendientes(int n_TipoOrigen)
        {
            // n_TipoOrigen  = INDICA DE DONDE VIENE LA GUIA 1 =VENTAS ;  2 = LOGISTICA
            int n_Fila = 0;
            int n_filaflex = 2;
            int n_iditem = 0;
            double n_Cantidad = 0;
            int n_idguia;
            string c_dato = "";
            DataTable dtresult = new DataTable();
            DataTable DtSolMat = new DataTable();
            DataTable dtresulpre = new DataTable();
            DataTable dtItemFiltro = new DataTable();
            CN_vta_guias o_guia = new CN_vta_guias();

            o_guia.mysConec = mysConec;
            dtresult = o_guia.BuscarGuiasPendientesAlmacen(STU_SISTEMA.EMPRESAID, 1, n_TipoOrigen, STU_SISTEMA.ANOTRABAJO);

            if (dtresult == null) { return; }
            if (dtresult.Rows.Count == 0) { return; }

            n_idguia = Convert.ToInt16(dtresult.Rows[0]["n_id"].ToString());

            BE_VTA_GUIAS entGuia = new BE_VTA_GUIAS();
            List<BE_VTA_GUIASDET> lstGuiasDet = new List<BE_VTA_GUIASDET>();
            booAgregando = true;
            entGuia = o_guia.TraerRegistro(n_idguia);

            LblIdPro.Text = entGuia.n_idcli.ToString();
            TxtNumRuc.Text = funDatos.DataTableBuscar(dtProveedor,"n_id","c_numdoc", entGuia.n_idcli.ToString(),"N").ToString();
            TxtProv.Text =  funDatos.DataTableBuscar(dtProveedor,"n_id","c_nombre", entGuia.n_idcli.ToString(),"N").ToString();
            //TxtNumDoc.Text = entGuia.c_numdoc;
            //TxtNumSer.Text = entGuia.c_numser;
            TxtNumDocRef.Text = entGuia.c_numdoc;
            TxtSerDocRef.Text = entGuia.c_numser;
            LblIdDocRef.Text = entGuia.n_id.ToString();

            //CboTipDoc.SelectedValue = entGuia.n_idtipdoc;
            TxtFchDoc.Text = entGuia.d_fchdoc.ToString("dd/MM/yyyy");
            txtFchIng.Text = entGuia.d_fchdoc.ToString("dd/MM/yyyy");
            //CboTipOpe.SelectedValue = 11;
            //CboDocRef.SelectedValue = entGuia.n_idtipdoc;

            lstGuiasDet = o_guia.LstDetalle;

            n_filaflex = 2;
            FgItems.Rows.Count = 2;

           
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

                c_dato = lstGuiasDet[n_Fila].c_numlot;
                FgItems.SetData(n_filaflex, 4, c_dato);

                n_Cantidad = Convert.ToDouble(lstGuiasDet[n_Fila].n_candev);
                FgItems.SetData(n_filaflex, 5, "0.00");
                FgItems.SetData(n_filaflex, 6, n_Cantidad.ToString("0.000000"));
                
                FgItems.SetData(n_filaflex, 7,lstGuiasDet[n_Fila].n_preuni.ToString("0.00"));
                FgItems.SetData(n_filaflex, 8, lstGuiasDet[n_Fila].n_preuni * n_Cantidad);
                FgItems.SetData(n_filaflex, 9, "08:00");
                n_filaflex = n_filaflex + 1;
            }
            
            booAgregando = false;
            CboSolicitante.Focus();
        }

        void MostrarOrdenRequerimiento()
        {
            int n_Fila = 0;
            int n_filaflex = 2;
            int n_iditem = 0;
            double n_Cantidad = 0;
            DataTable dtresult = new DataTable();
            CN_log_ordenrequerimiento o_pro = new CN_log_ordenrequerimiento();
            List<BE_LOG_ORDENREQUERIMIENTODET> l_reqcabdet = new List<BE_LOG_ORDENREQUERIMIENTODET>();
            BE_LOG_ORDENREQUERIMIENTO e_reqcab = new BE_LOG_ORDENREQUERIMIENTO();
            o_pro.mysConec = mysConec;
            dtresult = o_pro.OrdenesListarPorArea(STU_SISTEMA.EMPRESAID, 12);

            FgItems.Rows.Count = 2;
            if (dtresult != null)
            {
                if (dtresult.Rows.Count != 0)
                {
                    booAgregando = true;

                    o_pro.TraerRegistro(Convert.ToInt32(dtresult.Rows[0]["n_id"]));
                    e_reqcab = o_pro.entReqCab;

                    LblIdPro.Text = entEmp.n_idclipro.ToString();
                    TxtNumRuc.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_numdoc", entEmp.n_idclipro.ToString(), "N").ToString();
                    TxtProv.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_nombre", entEmp.n_idclipro.ToString(), "N").ToString();

                    //TxtNumDoc.Text = e_reqcab.c_numdoc;
                    //TxtNumSer.Text = e_reqcab.c_numser;
                    TxtNumDocRef.Text = e_reqcab.c_numdoc;
                    TxtSerDocRef.Text = e_reqcab.c_numser;
                    LblIdDocRef.Text = e_reqcab.n_id.ToString();

                    //CboTipDoc.SelectedValue = e_reqcab.n_idtipdoc;
                    TxtFchDoc.Text = e_reqcab.d_fchent.Value.ToString("dd/MM/yyyy");
                    //txtFchIng.Text = e_reqcab.d_fchent.Value.ToString("dd/MM/yyyy");
                    //CboTipOpe.SelectedValue = 10;
                    //CboTipDoc.SelectedValue = 72;
                    l_reqcabdet = o_pro.lstReqDet;
                    CmdBusPro.Enabled = false;

                    if (l_reqcabdet.Count != 0)
                    {
                        for (n_Fila = 0; n_Fila <= (l_reqcabdet.Count - 1); n_Fila++)
                        {
                            FgItems.Rows.Count = FgItems.Rows.Count + 1;
                            n_iditem = l_reqcabdet[n_Fila].n_idite;

                            dtresult = funDatos.DataTableFiltrar(dtItems, "n_id = " + n_iditem + "");

                            if (dtresult.Rows.Count != 0)
                            {
                                // MOSTRAMOS LA DESCRIPCION DEL ITEM
                                FgItems.SetData(n_filaflex, 2, dtresult.Rows[0]["c_despro"].ToString());

                                // MOSTRAMOS LA DESCRIPCION DEL TIPO DE ITEM
                                dtresult = funDatos.DataTableFiltrar(dtTipoExis, "n_id = " + Convert.ToInt16(dtresult.Rows[0]["n_idtipexi"]) + "");
                                FgItems.SetData(n_filaflex, 1, dtresult.Rows[0]["c_des"].ToString());
                            }

                            //MOSTRAMOS LA PRESENTACION DEL ITEM
                            dtresult = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_iditem + " AND n_default = 1");
                            if (dtresult.Rows.Count == 1)
                            {
                                FgItems.SetData(n_filaflex, 3, dtresult.Rows[0]["c_abrpre"].ToString());
                            }
                            else
                            {
                                FgItems.SetData(n_filaflex, 3, "");
                            }
                            n_Cantidad = Convert.ToDouble(l_reqcabdet[n_Fila].n_can);
                            FgItems.SetData(n_filaflex, 4, "");
                            FgItems.SetData(n_filaflex, 6, n_Cantidad.ToString("0.000000"));
                            FgItems.SetData(n_filaflex, 7, DateTime.Now.ToString("HH:mm"));
                            n_filaflex = n_filaflex + 1;
                        }

                        booAgregando = false;
                    }
                }
            }
        }

        void MostrarSolicitudMateriales()
        {
            int n_Fila = 0;
            int n_filaflex = 2;
            int n_iditem = 0;
            double n_Cantidad = 0;
            DataTable dtresult = new DataTable();
            CN_pro_solicitudamateriales o_pro = new CN_pro_solicitudamateriales();
            List<BE_PRO_SOLICITUDMATERIALESDET> l_solmatdet = new List<BE_PRO_SOLICITUDMATERIALESDET>();
            BE_PRO_SOLICITUDMATERIALES e_solmat = new BE_PRO_SOLICITUDMATERIALES();
            o_pro.mysConec = mysConec;
            dtresult = o_pro.SolicitudPendienteJalar(STU_SISTEMA.EMPRESAID);

            FgItems.Rows.Count = 2;
            if (dtresult != null)
            {
                if (dtresult.Rows.Count != 0)
                {
                    booAgregando = true;

                    o_pro.TraerRegistro(Convert.ToInt32(dtresult.Rows[0]["n_id"]));
                    e_solmat = o_pro.entSolicitud;

                    LblIdPro.Text = entEmp.n_idclipro.ToString();
                    TxtNumRuc.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_numdoc", entEmp.n_idclipro.ToString(), "N").ToString();
                    TxtProv.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_nombre", entEmp.n_idclipro.ToString(), "N").ToString();

                    TxtNumDocRef.Text = e_solmat.c_numdoc;
                    TxtSerDocRef.Text = e_solmat.c_numser;
                    LblIdDocRef.Text = e_solmat.n_id.ToString();

                    //CboTipDoc.SelectedValue = e_solmat.n_idtipdoc;
                    TxtFchDoc.Text = e_solmat.d_fchreg.ToString("dd/MM/yyyy");
                    txtFchIng.Text = e_solmat.d_fchreg.ToString("dd/MM/yyyy");
                    CboTipOpe.SelectedValue = 10;
                    //CboTipDoc.SelectedValue = 72;
                    l_solmatdet = o_pro.lstSolicitudDet;
                    CmdBusPro.Enabled = false;

                    if (l_solmatdet.Count != 0)
                    {
                        for (n_Fila = 0; n_Fila <= (l_solmatdet.Count - 1); n_Fila++)
                        {
                            FgItems.Rows.Count = FgItems.Rows.Count + 1;
                            n_iditem = l_solmatdet[n_Fila].n_idite;

                            dtresult = funDatos.DataTableFiltrar(dtItems, "n_id = " + n_iditem + "");

                            if (dtresult.Rows.Count != 0)
                            {
                                // MOSTRAMOS LA DESCRIPCION DEL ITEM
                                FgItems.SetData(n_filaflex, 2, dtresult.Rows[0]["c_despro"].ToString());

                                // MOSTRAMOS LA DESCRIPCION DEL TIPO DE ITEM
                                dtresult = funDatos.DataTableFiltrar(dtTipoExis, "n_id = " + Convert.ToInt16(dtresult.Rows[0]["n_idtipexi"]) + "");
                                FgItems.SetData(n_filaflex, 1, dtresult.Rows[0]["c_des"].ToString());
                            }

                            //MOSTRAMOS LA PRESENTACION DEL ITEM
                            dtresult = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_iditem + " AND n_default = 1");
                            if (dtresult.Rows.Count == 1)
                            {
                                FgItems.SetData(n_filaflex, 3, dtresult.Rows[0]["c_abrpre"].ToString());
                            }
                            else
                            {
                                FgItems.SetData(n_filaflex, 3, "");
                            }
                            n_Cantidad = Convert.ToDouble(l_solmatdet[n_Fila].n_canent);
                            FgItems.SetData(n_filaflex, 4, "");
                            FgItems.SetData(n_filaflex, 6, n_Cantidad.ToString("0.000000"));
                            FgItems.SetData(n_filaflex, 7, DateTime.Now.ToString("HH:mm"));
                            n_filaflex = n_filaflex + 1;
                        }

                        booAgregando = false;
                    }
                }
            }
        }

        void MostrarSolicitudMaterialesAdicional()
        {
            int n_Fila = 0;
            int n_filaflex = 2;
            int n_iditem = 0;
            double n_Cantidad = 0;
            DataTable dtresult = new DataTable();
            CN_pro_solicitudamateriales o_pro = new CN_pro_solicitudamateriales();
            List<BE_PRO_SOLICITUDMATERIALESDET> l_solmatdet = new List<BE_PRO_SOLICITUDMATERIALESDET>();
            BE_PRO_SOLICITUDMATERIALES e_solmat = new BE_PRO_SOLICITUDMATERIALES();
            o_pro.mysConec = mysConec;
            dtresult = o_pro.SolicitudPendienteJalarAdicionales(STU_SISTEMA.EMPRESAID);

            FgItems.Rows.Count = 2;
            if (dtresult != null)
            {
                if (dtresult.Rows.Count != 0)
                {
                    booAgregando = true;

                    o_pro.TraerRegistro(Convert.ToInt32(dtresult.Rows[0]["n_id"]));
                    e_solmat = o_pro.entSolicitud;

                    LblIdPro.Text = entEmp.n_idclipro.ToString();
                    TxtNumRuc.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_numdoc", entEmp.n_idclipro.ToString(), "N").ToString();
                    TxtProv.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_nombre", entEmp.n_idclipro.ToString(), "N").ToString();

                    TxtNumDocRef.Text = e_solmat.c_numdoc;
                    TxtSerDocRef.Text = e_solmat.c_numser;
                    LblIdDocRef.Text = e_solmat.n_id.ToString();

                    //CboTipDoc.SelectedValue = e_solmat.n_idtipdoc;
                    TxtFchDoc.Text = e_solmat.d_fchreg.ToString("dd/MM/yyyy");
                    //txtFchIng.Text = e_solmat.d_fchreg.ToString("dd/MM/yyyy");
                    CboTipOpe.SelectedValue = 10;
                    //CboTipDoc.SelectedValue = 72;
                    l_solmatdet = o_pro.lstSolicitudDet;
                    CmdBusPro.Enabled = false;

                    if (l_solmatdet.Count != 0)
                    {
                        for (n_Fila = 0; n_Fila <= (l_solmatdet.Count - 1); n_Fila++)
                        {
                            FgItems.Rows.Count = FgItems.Rows.Count + 1;
                            n_iditem = l_solmatdet[n_Fila].n_idite;

                            dtresult = funDatos.DataTableFiltrar(dtItems, "n_id = " + n_iditem + "");

                            if (dtresult.Rows.Count != 0)
                            {
                                // MOSTRAMOS LA DESCRIPCION DEL ITEM
                                FgItems.SetData(n_filaflex, 2, dtresult.Rows[0]["c_despro"].ToString());

                                // MOSTRAMOS LA DESCRIPCION DEL TIPO DE ITEM
                                dtresult = funDatos.DataTableFiltrar(dtTipoExis, "n_id = " + Convert.ToInt16(dtresult.Rows[0]["n_idtipexi"]) + "");
                                FgItems.SetData(n_filaflex, 1, dtresult.Rows[0]["c_des"].ToString());
                            }

                            //MOSTRAMOS LA PRESENTACION DEL ITEM
                            dtresult = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_iditem + " AND n_default = 1");
                            if (dtresult.Rows.Count == 1)
                            {
                                FgItems.SetData(n_filaflex, 3, dtresult.Rows[0]["c_abrpre"].ToString());
                            }
                            else
                            {
                                FgItems.SetData(n_filaflex, 3, "");
                            }
                            n_Cantidad = Convert.ToDouble(l_solmatdet[n_Fila].n_canent);
                            FgItems.SetData(n_filaflex, 4, "");
                            FgItems.SetData(n_filaflex, 6, n_Cantidad.ToString("0.000000"));
                            FgItems.SetData(n_filaflex, 7, DateTime.Now.ToString("HH:mm"));
                            n_filaflex = n_filaflex + 1;
                        }

                        booAgregando = false;
                    }
                }
            }
        }

        void MostrarProformas()
        {
            int n_Fila = 0;
            int n_filaflex = 2;
            int n_iditem = 0;
            double n_Cantidad = 0;
            DataTable dtresult = new DataTable();
            CN_vta_ventas o_ven = new CN_vta_ventas();
            List<BE_VTA_VENTASDET> l_vendet = new List<BE_VTA_VENTASDET>();
            BE_VTA_VENTAS e_ven = new BE_VTA_VENTAS();
            o_ven.mysConec = mysConec;
            dtresult = o_ven.ProformasPendJalAlmacen(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO);

            FgItems.Rows.Count = 2;
            if (dtresult != null)
            {
                if (dtresult.Rows.Count != 0)
                {
                    booAgregando = true;

                    e_ven = o_ven.TraerRegistro(Convert.ToInt32(dtresult.Rows[0]["n_id"]));

                    LblIdPro.Text = e_ven.n_idcli.ToString();
                    TxtNumRuc.Text = dtresult.Rows[0]["c_clinumdoc"].ToString();
                    TxtProv.Text = dtresult.Rows[0]["c_clinom"].ToString();
                    TxtNumDocRef.Text = e_ven.c_numdoc;
                    TxtSerDocRef.Text = e_ven.c_numser;
                    LblIdDocRef.Text = e_ven.n_id.ToString();

                    //CboTipDoc.SelectedValue = e_ven.n_idtipdoc;
                    TxtFchDoc.Text = e_ven.d_fchdoc.ToString("dd/MM/yyyy");
                    txtFchIng.Text = e_ven.d_fchdoc.ToString("dd/MM/yyyy");
                    CboTipOpe.SelectedValue = 1;

                    l_vendet = o_ven.LstDetalle;


                    if (l_vendet.Count != 0)
                    {
                        for (n_Fila = 0; n_Fila <= (l_vendet.Count - 1); n_Fila++)
                        {
                            FgItems.Rows.Count = FgItems.Rows.Count + 1;
                            n_iditem = l_vendet[n_Fila].n_iditem;

                            dtresult = funDatos.DataTableFiltrar(dtItems, "n_id = " + n_iditem + "");

                            if (dtresult.Rows.Count != 0)
                            {
                                // MOSTRAMOS LA DESCRIPCION DEL ITEM
                                FgItems.SetData(n_filaflex, 2, dtresult.Rows[0]["c_despro"].ToString());

                                // MOSTRAMOS LA DESCRIPCION DEL TIPO DE ITEM
                                dtresult = funDatos.DataTableFiltrar(dtTipoExis, "n_id = " + Convert.ToInt16(dtresult.Rows[0]["n_idtipexi"]) + "");
                                FgItems.SetData(n_filaflex, 1, dtresult.Rows[0]["c_des"].ToString());
                            }

                            //MOSTRAMOS LA PRESENTACION DEL ITEM
                            dtresult = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_iditem + " AND n_default = 1");
                            if (dtresult.Rows.Count == 1)
                            {
                                FgItems.SetData(n_filaflex, 3, dtresult.Rows[0]["c_abrpre"].ToString());
                            }
                            else
                            {
                                FgItems.SetData(n_filaflex, 3, "");
                            }
                            n_Cantidad = Convert.ToDouble(l_vendet[n_Fila].n_canpro);
                            FgItems.SetData(n_filaflex, 4, "");
                            FgItems.SetData(n_filaflex, 6, n_Cantidad.ToString("0.000000"));
                            FgItems.SetData(n_filaflex, 9, DateTime.Now.ToString("HH:mm"));
                            n_filaflex = n_filaflex + 1;
                        }

                        booAgregando = false;
                    }
                }
            }
        }

        void MostrarFacturas()
        {
            int n_Fila = 0;
            int n_filaflex = 2;
            int n_iditem = 0;
            double n_Cantidad = 0;
            DataTable dtresult = new DataTable();
            CN_vta_ventas o_ven = new CN_vta_ventas();
            List<BE_VTA_VENTASDET> l_vendet = new List<BE_VTA_VENTASDET>();
            BE_VTA_VENTAS e_ven = new BE_VTA_VENTAS();
            o_ven.mysConec = mysConec;
            dtresult = o_ven.DocumentosPendJalAlmacen(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO);

            FgItems.Rows.Count = 2;
            if (dtresult != null)
            { 
                if (dtresult.Rows.Count != 0)
                {
                    booAgregando = true;

                    e_ven = o_ven.TraerRegistro(Convert.ToInt32(dtresult.Rows[0]["n_id"]));

                    LblIdPro.Text = e_ven.n_idcli.ToString();
                    TxtNumRuc.Text = dtresult.Rows[0]["c_clinumdoc"].ToString();
                    TxtProv.Text = dtresult.Rows[0]["c_clinom"].ToString();
                    TxtNumDocRef.Text = e_ven.c_numdoc;
                    TxtSerDocRef.Text = e_ven.c_numser;
                    LblIdDocRef.Text = e_ven.n_id.ToString();

                    //CboTipDoc.SelectedValue = e_ven.n_idtipdoc;
                    TxtFchDoc.Text = e_ven.d_fchdoc.ToString("dd/MM/yyyy");
                    txtFchIng.Text = e_ven.d_fchdoc.ToString("dd/MM/yyyy");
                    CboTipOpe.SelectedValue = 1;
                    
                    l_vendet = o_ven.LstDetalle;
                    

                    if (l_vendet.Count != 0)
                    {
                        for (n_Fila = 0; n_Fila <= (l_vendet.Count - 1); n_Fila++)
                        {
                            FgItems.Rows.Count = FgItems.Rows.Count + 1;
                            n_iditem = l_vendet[n_Fila].n_iditem;

                            dtresult = funDatos.DataTableFiltrar(dtItems, "n_id = " + n_iditem + "");

                            if (dtresult.Rows.Count != 0)
                            {
                                // MOSTRAMOS LA DESCRIPCION DEL ITEM
                                FgItems.SetData(n_filaflex, 2, dtresult.Rows[0]["c_despro"].ToString());

                                // MOSTRAMOS LA DESCRIPCION DEL TIPO DE ITEM
                                dtresult = funDatos.DataTableFiltrar(dtTipoExis, "n_id = " + Convert.ToInt16(dtresult.Rows[0]["n_idtipexi"]) + "");
                                FgItems.SetData(n_filaflex, 1, dtresult.Rows[0]["c_des"].ToString());
                            }

                            //MOSTRAMOS LA PRESENTACION DEL ITEM
                            dtresult = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_iditem + " AND n_default = 1");
                            if (dtresult.Rows.Count == 1)
                            {
                                FgItems.SetData(n_filaflex, 3, dtresult.Rows[0]["c_abrpre"].ToString());
                            }
                            else
                            {
                                FgItems.SetData(n_filaflex, 3, "");
                            }
                            n_Cantidad = Convert.ToDouble(l_vendet[n_Fila].n_canpro);
                            FgItems.SetData(n_filaflex, 4, "");
                            FgItems.SetData(n_filaflex, 6, n_Cantidad.ToString("0.000000"));
                            FgItems.SetData(n_filaflex, 9, DateTime.Now.ToString("HH:mm"));
                            n_filaflex = n_filaflex + 1;
                        }

                        booAgregando = false;
                    }
                }
            }
        }

        private void CmdBusDocRef_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(CboDocRef.SelectedValue) == 73)
            {
                MostrarParteProduccion();
            }
            if (Convert.ToInt16(CboDocRef.SelectedValue) == 10)
            {
                MostrarGuiasPendientes(1); // INDICAMOS QUE MUESTRE SOLO GUIAS DE VENTA
            }
            if (Convert.ToInt16(CboDocRef.SelectedValue) == 32)
            {
                MostrarGuiasPendientes(2); // INDICAMOS QUE MUESTRE SOLO GUIAS DE LOGISTICA
            }
            if ((Convert.ToInt16(CboDocRef.SelectedValue) == 2) || (Convert.ToInt16(CboDocRef.SelectedValue) == 4))
            {
                MostrarFacturas();    // MOSTRAMOS LOS DOCUMENTOS DE VENTA QUE NO TENGAN GUIA ADJUNTA
            }
            if (Convert.ToInt16(CboDocRef.SelectedValue) == 72)
            {
                MostrarSolicitudMateriales();
            }
            if (Convert.ToInt16(CboDocRef.SelectedValue) == 75)
            {
                MostrarOrdenRequerimiento();
            }
            if (Convert.ToInt16(CboDocRef.SelectedValue) == 92)
            {
                MostrarSolicitudMaterialesAdicional();
            }
            if (Convert.ToInt16(CboDocRef.SelectedValue) == 88)
            {
                MostrarProformas();    // MOSTRAMOS LOS DOCUMENTOS DE VENTA QUE NO TENGAN GUIA ADJUNTA
            }
        }

        private void CboTipOpe_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            //CboProveedor.Enabled = true;
            CmdBusPro.Enabled = true;
            TxtNumRuc.Enabled = true;
            //CboProveedor.SelectedValue = 0;
            //CmdAddLot.Visible = false;
            //CmdDelLot.Visible = false;

            //TxtNumSer.Text = "0001";
            //TxtNumDoc.Text = "";
            if (Convert.ToInt16(CboTipOpe.SelectedValue) == 1)                                                                  // SALIDA POR VENTA
            {
                //CboTipDoc.SelectedValue = 50;
                CboDocRef.SelectedValue = 10;
                //CmdAddLot.Visible = true;
                //CmdDelLot.Visible = true;
                //CboProveedor.Focus();
                TxtNumRuc.Focus();
            }

            if (Convert.ToInt16(CboTipOpe.SelectedValue) == 10)                                                                 // SALIDA A PRODUCCION
            {
                if (STU_SISTEMA.EMPRESAID == 1) { LblIdPro.Text = "1711"; }
                if (STU_SISTEMA.EMPRESAID == 2) { LblIdPro.Text = "7032"; }

                TxtNumRuc.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_numdoc", LblIdPro.Text, "N").ToString();
                TxtProv.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_nombre", LblIdPro.Text, "N").ToString();

                //CboProveedor.Enabled = false;
                CmdBusPro.Enabled = false;
                TxtNumRuc.Enabled = false;

                //CboTipDoc.SelectedValue = 50;
                CboDocRef.SelectedValue = 73;                
                CboSolicitante.Focus();
            }

            if (Convert.ToInt16(CboTipOpe.SelectedValue) == 11)                                                                 // SALIDA A PRODUCCION
            {
                if (STU_SISTEMA.EMPRESAID == 1) {  LblIdPro.Text  = "1711"; }
                if (STU_SISTEMA.EMPRESAID == 2) {  LblIdPro.Text = "7032"; }

                TxtNumRuc.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_numdoc", LblIdPro.Text, "N").ToString();
                TxtProv.Text = funDatos.DataTableBuscar(dtProveedor, "n_id", "c_nombre", LblIdPro.Text, "N").ToString();

                //CboProveedor.Enabled = false;
                CmdBusPro.Enabled = false;
                TxtNumRuc.Enabled = false;

                //CboTipDoc.SelectedValue = 50;
                CboDocRef.SelectedValue = 32;
                CboSolicitante.Focus();
            }

            //MostarNumeroDocumento(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboTipDoc.SelectedValue), TxtNumSer.Text);
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
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            if (FgItems.Col == 5)
            {
                FgItems.Select(FgItems.Row - 1, 2);
                return;
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

    }
}