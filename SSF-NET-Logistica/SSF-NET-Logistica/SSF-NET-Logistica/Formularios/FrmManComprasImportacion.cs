using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Logistica;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Almacen;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Logistica;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
//using SIAC_Objetos.Funciones;
using SIAC_Negocio.Sunat;
using SSF_NET_Contabilidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSF_NET_Logistica.Formularios
{
    public partial class FrmManComprasImportacion : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_log_compras objRegistros = new CN_log_compras();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sys_personalmodulo objpermod = new CN_sys_personalmodulo();
        CN_log_personal objLogPer = new CN_log_personal();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_alm_inventariounimed ObjAlmUniMed = new CN_alm_inventariounimed();
        CN_sun_tipexi objTipExi = new CN_sun_tipexi();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_sun_tipmon objMoneda = new CN_sun_tipmon();
        CN_sun_tipope objTipOpe = new CN_sun_tipope();
        CN_mae_condpago objCondPag = new CN_mae_condpago();
        CN_mae_clipro objPro = new CN_mae_clipro();
        CN_alm_movimientos objMov = new CN_alm_movimientos();
        CN_con_tc objTC = new CN_con_tc();
        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_LOG_COMPRAS e_Cabecera = new BE_LOG_COMPRAS();
        List<BE_LOG_COMPRASDET> l_Detalle = new List<BE_LOG_COMPRASDET>();
        List<BE_LOG_COMPRASDOC> l_Documentos = new List<BE_LOG_COMPRASDOC>();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO

        DataTable dtForm = new DataTable();
        DataTable dtUniMed = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtLogPer = new DataTable();
        DataTable dtTipDoc = new DataTable();
        DataTable dtTipExi = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtConPag = new DataTable();
        DataTable dtMon = new DataTable();
        DataTable dtTipOpe = new DataTable();
        DataTable dtPro = new DataTable();
        DataTable dtMov = new DataTable();
        DataTable dtTc = new DataTable();

        // VARIABLES LOCALES
        int n_NumFilasDocumento = 30;                                                    // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;
        Double n_porigv = 0;                                                             // LAMACENA EL PORCENTAJE DEL IGV
        string[,] arrCabeceraDg1 = new string[11, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[10, 5];
        string[,] arrCabeceraFlex2 = new string[4, 5];

        bool b_SeEjecuto = false;
        bool b_Agregando = false;
        string c_Numerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string c_NumerovalidosFE = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZEF1234567890." + (char)8;                                        // + (char)8;
        string c_Caracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
         
        public FrmManComprasImportacion()
        {
            InitializeComponent();
        }

        private void FrmManComprasImportacion_Load(object sender, EventArgs e)
        {
            b_Agregando = true;
            CargarCombos();
            ConfigurarFormulario();
            b_Agregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboTipOpe, dtTipOpe, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMoneda, dtMon, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtTipDoc, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboConPag, dtConPag, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipExi, dtTipExi, "n_id", "c_des");

        }
        void ConfigurarFormulario()
        {
            this.Height = 636;
            this.Width = 976;

            n_porigv = 18;
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            arrCabeceraFlex1[0, 0] = "Descripcion";
            arrCabeceraFlex1[0, 1] = "250";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Uni. Med.";
            arrCabeceraFlex1[1, 1] = "45";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Cantidad";
            arrCabeceraFlex1[2, 1] = "70";
            arrCabeceraFlex1[2, 2] = "D";
            arrCabeceraFlex1[2, 3] = "0.000000";
            arrCabeceraFlex1[2, 4] = "c_itepredes";

            arrCabeceraFlex1[3, 0] = "Imp. Afecto";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "0.000000";
            arrCabeceraFlex1[3, 4] = "n_can";

            arrCabeceraFlex1[4, 0] = "Imp. No Afecto";
            arrCabeceraFlex1[4, 1] = "70";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.000000";
            arrCabeceraFlex1[4, 4] = "n_can";

            arrCabeceraFlex1[5, 0] = "% Dscto";
            arrCabeceraFlex1[5, 1] = "40";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "n_can";

            arrCabeceraFlex1[6, 0] = "Nuevo Pre. Unitario";
            arrCabeceraFlex1[6, 1] = "70";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.000000";
            arrCabeceraFlex1[6, 4] = "n_can";

            arrCabeceraFlex1[7, 0] = "Total";
            arrCabeceraFlex1[7, 1] = "70";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.000000";
            arrCabeceraFlex1[7, 4] = "n_can";

            arrCabeceraFlex1[8, 0] = "IdItem";
            arrCabeceraFlex1[8, 1] = "0";
            arrCabeceraFlex1[8, 2] = "N";
            arrCabeceraFlex1[8, 3] = "0.00";
            arrCabeceraFlex1[8, 4] = "n_can";

            arrCabeceraFlex1[9, 0] = "IdUnimed";
            arrCabeceraFlex1[9, 1] = "0";
            arrCabeceraFlex1[9, 2] = "N";
            arrCabeceraFlex1[9, 3] = "0.00";
            arrCabeceraFlex1[9, 4] = "n_can";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtLista, 2, false);
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;


            arrCabeceraFlex2[0, 0] = "Tip. Doc.";
            arrCabeceraFlex2[0, 1] = "40";
            arrCabeceraFlex2[0, 2] = "C";
            arrCabeceraFlex2[0, 3] = "";
            arrCabeceraFlex2[0, 4] = "c_tipexides";

            arrCabeceraFlex2[1, 0] = "Nº Documento";
            arrCabeceraFlex2[1, 1] = "120";
            arrCabeceraFlex2[1, 2] = "C";
            arrCabeceraFlex2[1, 3] = "";
            arrCabeceraFlex2[1, 4] = "c_tipexides";

            arrCabeceraFlex2[2, 0] = "Id Documento";
            arrCabeceraFlex2[2, 1] = "0";
            arrCabeceraFlex2[2, 2] = "N";
            arrCabeceraFlex2[2, 3] = "";
            arrCabeceraFlex2[2, 4] = "c_tipexides";

            arrCabeceraFlex2[3, 0] = "Id Tipo Documento";
            arrCabeceraFlex2[3, 1] = "0";
            arrCabeceraFlex2[3, 2] = "N";
            arrCabeceraFlex2[3, 3] = "";
            arrCabeceraFlex2[3, 4] = "c_tipexides";

            funFlex.FlexMostrarDatos(FgDoc, arrCabeceraFlex2, dtLista, 2, false);

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        void DataTableCargar()
        {
            ListarItems();                                    // CARGAMOS LOS DATOS DEL FORMULARIO

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(4, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(4);

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objItems.mysConec = mysConec;
            dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);                                        // CARGAMOS TODOS LOS DEPARTAMENTOS

            objTipExi.mysConec = mysConec;
            dtTipExi = objTipExi.Listar();

            objTipDoc.mysConec = mysConec;
            dtTipDoc = objTipDoc.Listar();
            dtTipDoc = funDatos.DataTableFiltrar(dtTipDoc, "n_escom = 1 AND n_idtipo = 1");

            objCondPag.mysConec = mysConec;
            dtConPag = objCondPag.Listar();

            objMoneda.mysConec = mysConec;
            dtMon = objMoneda.Listar();

            objTipOpe.mysConec = mysConec;
            dtTipOpe = objTipOpe.Listar();
            dtTipOpe = funDatos.DataTableFiltrar(dtTipOpe, "n_tipmov = 1");

            ObjAlmUniMed.mysConec = mysConec;
            dtUniMed = ObjAlmUniMed.Listar();

            objLogPer.mysConec = mysConec;
            objLogPer.Listar(STU_SISTEMA.EMPRESAID);
            dtLogPer = objLogPer.dtPersonal;

            objPro.mysConec = mysConec;
            dtPro = objPro.ListarProveedor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objMov.mysConec = mysConec;
            dtMov = objMov.Consulta6(STU_SISTEMA.EMPRESAID);

            objTC.mysConec = mysConec;
            objTC.Listar(0, 0);
            dtTc = objTC.dtLista;
            objTC = null;
        }
        void ListarItems()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            if (objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 2) == true)
            {
                dtLista = objRegistros.dtLista;
                dtLista = funDatos.DataTableFiltrar(dtLista, "(n_idtipdoc IN(2))");
            }

            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);

            LblNumReg.Text = "0";
            LblNumReg.Text = funFunciones.NulosN((dtLista.Rows.Count)).ToString();
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
            b_Agregando = true;

            objRegistros.mysConec = mysConec;
            l_Detalle.Clear();
            l_Documentos.Clear();

            if (objRegistros.TraerRegistro(n_IdRegistro) == true)
            {
                e_Cabecera = objRegistros.e_Compras;
                l_Detalle = objRegistros.e_ComprasDet;
                l_Documentos = objRegistros.e_ComprasDoc;
            }
            LblNumAsi.Text = e_Cabecera.c_numreg;
            TxtFchEmi.Text = e_Cabecera.d_fchdoc.ToString("dd/MM/yyyy");
            CboMoneda.SelectedValue = e_Cabecera.n_idmon;
            CboTipOpe.SelectedValue = e_Cabecera.n_idtipope;

            LblIdPro.Text = e_Cabecera.n_idpro.ToString();
            TxtNumRuc.Text = funDatos.DataTableBuscar(dtPro, "n_id", "c_numdoc", e_Cabecera.n_idpro.ToString(), "N").ToString();
            TxtProv.Text = funDatos.DataTableBuscar(dtPro, "n_id", "c_nombre", e_Cabecera.n_idpro.ToString(), "N").ToString();

            TxtTipCam.Text = e_Cabecera.n_tc.ToString("0.000");
            CboTipDoc.SelectedValue = e_Cabecera.n_idtipdoc;
            TxtNumSer.Text = e_Cabecera.c_numser;
            TxtNumDoc.Text = e_Cabecera.c_numdoc;
            CboConPag.SelectedValue = e_Cabecera.n_idconpag;
            TxtFchVen.Text = e_Cabecera.d_fchven.ToString("dd/MM/yyyy");
            TxtGlo.Text = e_Cabecera.c_glosa;
            CboTipOpe.SelectedValue = e_Cabecera.n_idtipope;

            TxtBasImp1.Text = e_Cabecera.n_impbru.ToString("0.00");
            TxtBasImp2.Text = e_Cabecera.n_impbru2.ToString("0.00");
            TxtBasImp3.Text = e_Cabecera.n_impbru3.ToString("0.00");
            TxtInafec.Text = e_Cabecera.n_impinaf.ToString("0.00");

            TxtIgv1.Text = e_Cabecera.n_impigv.ToString("0.00"); ;
            TxtIgv2.Text = e_Cabecera.n_impigv2.ToString("0.00"); ;
            TxtIgv3.Text = e_Cabecera.n_impigv3.ToString("0.00"); ;
            TxtOtros.Text = e_Cabecera.n_impotr.ToString("0.00"); ;
            TxtIsc.Text = e_Cabecera.n_impisc.ToString("0.00"); ;
            TxtTotal.Text = e_Cabecera.n_imptotcom.ToString("0.00"); ;

            CboTipExi.SelectedValue = e_Cabecera.n_idtippro;

            if (e_Cabecera.n_idtipcom == 1)
            {
                OptSinDoc.Checked = true;
                OptConDoc.Checked = false;
            }
            else
            {
                OptSinDoc.Checked = false;
                OptConDoc.Checked = true;
            }

            //// VOLVEMOS A CARGAR LAS VARIABLES PORQUE SON LIMPIADAS EN EL CHEC CON DOCUMENTO
            //l_Detalle = objRegistros.e_ComprasDet;
            //l_Documentos = objRegistros.e_ComprasDoc;

            b_Agregando = false;
            Mostrardetalle();
            MostrarDocumentos();
            //FgItems.Rows.Count = 2;
            //for (n_row=0; n_row<= l_Detalle.Count-1; n_row++)
            //{
            //    FgItems.Rows.Count = FgItems.Rows.Count + 1;

            //    c_dato = l_Detalle[n_row].n_iditem.ToString();                                 // DESCRIPCION DEL ITEM
            //    c_dato = funDatos.DataTableBuscar(dtItems,"n_id", "c_despro",c_dato,"N").ToString();
            //    FgItems.SetData(FgItems.Rows.Count-1, 1, c_dato);

            //    c_dato = l_Detalle[n_row].n_idunimed.ToString();                               // UNIDAD DE MEDIDA DEL ITEM                 
            //    c_dato = funDatos.DataTableBuscar(dtUniMed,"n_id","c_abrpre",c_dato, "N").ToString();
            //    FgItems.SetData(FgItems.Rows.Count-1, 2, c_dato);

            //    c_dato = l_Detalle[n_row].n_canpro.ToString("0.00");                           // CANTIDAD DEL ITEM 
            //    FgItems.SetData(FgItems.Rows.Count-1, 3, c_dato);

            //    c_dato = l_Detalle[n_row].n_preunibru.ToString("0.000000");                    // PRECIO UNITARIO DEL ITEM
            //    FgItems.SetData(FgItems.Rows.Count-1, 4, c_dato);

            //    //c_dato = l_Detalle[n_row].N.ToString("0.00");                                // 
            //    //FgItems.SetData(FgItems.Rows.Count-1, 5, c_dato);                                

            //    FgItems.SetData(FgItems.Rows.Count-1, 6, "");                                  // PORCENTAJE DE DESCUENTO

            //    c_dato = l_Detalle[n_row].n_preuni.ToString("0.00");                           // PRECIO UNITARIO DEL PRODUCTO CON DESCUENTO
            //    FgItems.SetData(FgItems.Rows.Count-1, 7, c_dato);

            //    c_dato = l_Detalle[n_row].n_imptot.ToString("0.00");                           // IMPORTE TOTAL DEL ITEM
            //    FgItems.SetData(FgItems.Rows.Count-1, 8, c_dato);

            //    c_dato = l_Detalle[n_row].n_iditem.ToString("0.00");                           // ID DEL ITEM                    
            //    FgItems.SetData(FgItems.Rows.Count-1, 9, c_dato);

            //    c_dato = l_Detalle[n_row].n_idunimed.ToString("0.00");                         // ID DE LA UNIDAD DE MEDIDA DEL ITEM
            //    FgItems.SetData(FgItems.Rows.Count - 1, 10, c_dato);
            //}

            //if (n_QueHace == 2)
            //{ 
            //    int n_numfiladi = (n_NumFilasDocumento - l_Detalle.Count);

            //    for (n_row = 0; n_row <= n_numfiladi; n_row++)
            //    {
            //        FgItems.Rows.Count = FgItems.Rows.Count + 1;
            //    }
            //}
        }
        void MostrarDocumentos()
        {
            int n_row = 0;
            string c_dato = "";
            DataTable dtResult = new DataTable();
            FgDoc.Rows.Count = 2;

            for (n_row = 0; n_row <= l_Documentos.Count - 1; n_row++)
            {
                if (n_row == 0)
                {
                    c_dato = c_dato + l_Documentos[n_row].n_iddoc.ToString();
                }
                else
                {
                    c_dato = c_dato + "," + l_Documentos[n_row].n_iddoc.ToString();
                }
            }

            CN_alm_movimientos objMov = new CN_alm_movimientos();
            objMov.mysConec = mysConec;
            dtResult = objMov.Consulta7(c_dato);

            if (dtResult != null)
            {
                b_Agregando = true;
                for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                {
                    FgDoc.Rows.Count = FgDoc.Rows.Count + 1;

                    c_dato = dtResult.Rows[n_row]["c_abr"].ToString();
                    FgDoc.SetData(FgDoc.Rows.Count - 1, 1, c_dato);

                    c_dato = dtResult.Rows[n_row]["c_numdoc"].ToString();
                    FgDoc.SetData(FgDoc.Rows.Count - 1, 2, c_dato);

                    c_dato = dtResult.Rows[n_row]["n_id"].ToString();
                    FgDoc.SetData(FgDoc.Rows.Count - 1, 3, c_dato);

                    c_dato = dtResult.Rows[n_row]["n_idtipdoc"].ToString();
                    FgDoc.SetData(FgDoc.Rows.Count - 1, 4, c_dato);
                }
                if (n_QueHace != 3)
                {
                    FgDoc.Rows.Count = FgDoc.Rows.Count + (10 - dtResult.Rows.Count - 1);
                }
                b_Agregando = false;
            }
        }
        void Mostrardetalle()
        {
            int n_row = 0;
            string c_dato = "";

            b_Agregando = true;
            FgItems.Rows.Count = 2;
            for (n_row = 0; n_row <= l_Detalle.Count - 1; n_row++)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;

                c_dato = l_Detalle[n_row].n_iditem.ToString();                                 // DESCRIPCION DEL ITEM
                c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", c_dato, "N").ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 1, c_dato);

                c_dato = l_Detalle[n_row].n_idunimed.ToString();                               // UNIDAD DE MEDIDA DEL ITEM                 
                c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_abrpre", c_dato, "N").ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 2, c_dato);

                c_dato = l_Detalle[n_row].n_canpro.ToString("0.00");                           // CANTIDAD DEL ITEM 
                FgItems.SetData(FgItems.Rows.Count - 1, 3, c_dato);

                c_dato = l_Detalle[n_row].n_preunibru.ToString("0.000000");                    // PRECIO UNITARIO DEL ITEM
                FgItems.SetData(FgItems.Rows.Count - 1, 4, c_dato);

                //c_dato = l_Detalle[n_row].N.ToString("0.00");                                // 
                //FgItems.SetData(FgItems.Rows.Count-1, 5, c_dato);                                

                FgItems.SetData(FgItems.Rows.Count - 1, 6, "");                                  // PORCENTAJE DE DESCUENTO

                c_dato = l_Detalle[n_row].n_preuni.ToString("0.00");                           // PRECIO UNITARIO DEL PRODUCTO CON DESCUENTO
                FgItems.SetData(FgItems.Rows.Count - 1, 7, c_dato);

                c_dato = l_Detalle[n_row].n_imptot.ToString("0.00");                           // IMPORTE TOTAL DEL ITEM
                FgItems.SetData(FgItems.Rows.Count - 1, 8, c_dato);

                c_dato = l_Detalle[n_row].n_iditem.ToString("0.00");                           // ID DEL ITEM                    
                FgItems.SetData(FgItems.Rows.Count - 1, 9, c_dato);

                c_dato = l_Detalle[n_row].n_idunimed.ToString("0.00");                         // ID DE LA UNIDAD DE MEDIDA DEL ITEM
                FgItems.SetData(FgItems.Rows.Count - 1, 10, c_dato);
            }

            if (n_QueHace == 2)
            {
                int n_numfiladi = (n_NumFilasDocumento - l_Detalle.Count);

                for (n_row = 0; n_row <= n_numfiladi; n_row++)
                {
                    FgItems.Rows.Count = FgItems.Rows.Count + 1;
                }
            }
            b_Agregando = false;
        }
        void Nuevo()
        {
            b_Agregando = true;
            n_QueHace = 1;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            Tab1.SelectedIndex = 1;
            TxtFchEmi.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CboMoneda.SelectedValue = 115;
            CboTipOpe.SelectedValue = 2;
            FgItems.Cols[1].ComboList = "...";
            FgDoc.Cols[1].ComboList = "...";
            OptSinDoc.Checked = true;
            FgDoc.Rows.Count = 2;
            l_Detalle.Clear();
            l_Documentos.Clear();
            b_Agregando = false;
            CboConPag.SelectedValue = 1;
            CboTipExi.SelectedValue = 3;
            TxtTipCam.Text = STU_SISTEMA.TIPOCAMBIO.ToString("0.0000");
            TxtNumRuc.Focus();
        }
        void Blanquea()
        {
            LblIdPro.Text = "";

            TxtFchEmi.Text = "";
            TxtFchEmi.CustomFormat = "dd/MM/yyyy";
            TxtFchEmi.Format = DateTimePickerFormat.Custom;

            LblNumAsi.Text = "";
            CboMoneda.SelectedValue = 0;
            CboTipDoc.SelectedValue = 0;

            TxtProv.Text = "";
            TxtNumRuc.Text = "";
            LblIdPro.Text = "";

            CboTipDoc.SelectedValue = 0;
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtTipCam.Text = "";
            CboConPag.SelectedValue = 0;
            TxtFchVen.Text = "";
            TxtFchVen.CustomFormat = "dd/MM/yyyy";
            TxtFchVen.Format = DateTimePickerFormat.Custom;
            TxtGlo.Text = "";
            CboTipExi.SelectedValue = 0;

            OptSinDoc.Checked = true;
            OptConDoc.Checked = false;

            TxtBasImp1.Text = "";
            TxtBasImp2.Text = "";
            TxtBasImp3.Text = "";
            TxtInafec.Text = "";
            TxtIgv1.Text = "";
            TxtIgv2.Text = "";
            TxtIgv3.Text = "";
            TxtOtros.Text = "";
            TxtIsc.Text = "";
            TxtTotal.Text = "";

            FgItems.Rows.Count = 2;
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;

            FgDoc.Rows.Count = 2;
            FgDoc.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;
        }

        void Bloquea()
        {
            TxtFchEmi.Enabled = !TxtFchEmi.Enabled;
            CboMoneda.Enabled = !CboMoneda.Enabled;
            CboTipDoc.Enabled = !CboTipDoc.Enabled;
            //TxtProv.Enabled = !TxtProv.Enabled;
            TxtNumRuc.Enabled = !TxtNumRuc.Enabled;

            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            CboConPag.Enabled = !CboConPag.Enabled;
            TxtFchVen.Enabled = !TxtFchVen.Enabled;
            TxtGlo.Enabled = !TxtGlo.Enabled;
            OptSinDoc.Enabled = !OptSinDoc.Enabled;
            OptConDoc.Enabled = !OptConDoc.Enabled;
            TxtTipCam.Enabled = !TxtTipCam.Enabled;
            CboTipOpe.Enabled = !CboTipOpe.Enabled;
            CboTipExi.Enabled = !CboTipExi.Enabled;

            CmdBusPro.Enabled = !CmdBusPro.Enabled;

            CmdVerDoc.Enabled = !CmdVerDoc.Enabled;
            CmdDelDoc.Enabled = !CmdDelDoc.Enabled;
            CmdDelItem.Enabled = !CmdDelItem.Enabled;
            CmdAddNewItem.Enabled = !CmdAddNewItem.Enabled;

            CmdVerAsiento.Enabled = !CmdVerAsiento.Enabled;
        }
        void MostrarEstadoMes(int n_IdEmpresa, int n_IdMes)
        {
            obj_fungen.mysConec = mysConec;
            if (obj_fungen.EstadoPeriodo(n_IdEmpresa, n_IdMes, 1) == true)
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
            ToolSalir.Enabled = !ToolSalir.Enabled;
            ToolExportar.Enabled = !ToolExportar.Enabled;
        }
        void Modificar()
        {
            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            FgItems.Cols[1].ComboList = "...";
            FgDoc.Cols[1].ComboList = "...";
            TxtFchEmi.Focus();
        }
        bool EliminarRegistro()
        {
            bool b_Result = false;
            int n_IdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(n_IdRegistro) == true)
                {
                    b_Result = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            return b_Result;
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
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(e_Cabecera, l_Detalle, l_Documentos);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(e_Cabecera, l_Detalle, l_Documentos);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.n_ErrorNumber.ToString() + " = " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_row = 0;
            l_Detalle.Clear();
            l_Documentos.Clear();

            if (n_QueHace == 1) { e_Cabecera.n_id = 0; }
            if (n_QueHace == 2) { e_Cabecera.n_id = e_Cabecera.n_id; }
            e_Cabecera.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Cabecera.n_anotra = STU_SISTEMA.ANOTRABAJO;
            e_Cabecera.n_idmes = STU_SISTEMA.MESTRABAJO;
            e_Cabecera.n_idlib = 8;
            e_Cabecera.c_numreg = LblNumAsi.Text;
            e_Cabecera.n_idtippro = Convert.ToInt32(CboTipExi.SelectedValue);
            e_Cabecera.n_idpro = Convert.ToInt32(LblIdPro.Text);
            e_Cabecera.n_idtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);
            e_Cabecera.c_numser = TxtNumSer.Text;
            e_Cabecera.c_numdoc = TxtNumDoc.Text;
            e_Cabecera.d_fchdoc = Convert.ToDateTime(TxtFchEmi.Text);
            e_Cabecera.d_fchreg = Convert.ToDateTime(TxtFchEmi.Text);
            e_Cabecera.n_idconpag = Convert.ToInt32(CboConPag.SelectedValue);
            e_Cabecera.d_fchven = Convert.ToDateTime(TxtFchVen.Text);
            e_Cabecera.n_idmon = Convert.ToInt32(CboMoneda.SelectedValue);
            e_Cabecera.n_impbru = Convert.ToDouble(TxtBasImp1.Text);
            e_Cabecera.n_impbru2 = Convert.ToDouble(TxtBasImp2.Text);
            e_Cabecera.n_impbru3 = Convert.ToDouble(TxtBasImp2.Text);
            e_Cabecera.n_impinaf = Convert.ToDouble(TxtInafec.Text);
            e_Cabecera.n_impigv = Convert.ToDouble(TxtIgv1.Text);
            e_Cabecera.n_impigv2 = Convert.ToDouble(TxtIgv2.Text);
            e_Cabecera.n_impigv3 = Convert.ToDouble(TxtIgv3.Text);
            e_Cabecera.n_impisc = Convert.ToDouble(TxtIsc.Text);
            e_Cabecera.n_impotr = Convert.ToDouble(TxtOtros.Text);
            e_Cabecera.n_imptotcom = Convert.ToDouble(TxtTotal.Text);
            e_Cabecera.n_tc = Convert.ToDouble(TxtTipCam.Text);
            e_Cabecera.n_impsal = Convert.ToDouble(TxtTotal.Text);
            e_Cabecera.n_tasaigv = n_porigv;
            e_Cabecera.c_glosa = TxtGlo.Text;
            e_Cabecera.n_estado = 1;
            e_Cabecera.n_idtipdocref = 0;
            e_Cabecera.n_iddocref = 0;
            e_Cabecera.n_idtipope = Convert.ToInt32(CboTipOpe.SelectedValue);
            e_Cabecera.n_tipcom = 2;                                           // INDICAMOS QUE ES UNA COMPRA POR IMPORTACION DE BIENES
            if (OptSinDoc.Checked == true) { e_Cabecera.n_idtipcom = 1; }
            if (OptConDoc.Checked == true) { e_Cabecera.n_idtipcom = 2; }

            // CARGAMOS EL DETALLE DEL DOCUMENTO
            for (n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
            {
                BE_LOG_COMPRASDET e_Det = new BE_LOG_COMPRASDET();

                if (funFunciones.NulosC(FgItems.GetData(n_row, 1)).ToString() != "")
                {
                    e_Det.n_idcom = 0;
                    e_Det.n_iditem = Convert.ToInt32(FgItems.GetData(n_row, 9));              // ID DEL ITEM
                    e_Det.n_idunimed = Convert.ToInt32(FgItems.GetData(n_row, 10));          // ID DE LA UNIDAD DE MEDIDA
                    e_Det.n_canpro = Convert.ToDouble(FgItems.GetData(n_row, 3));            // CANTIDAD DEL PRODUCTO
                    e_Det.n_preunibru = Convert.ToDouble(FgItems.GetData(n_row, 4));         // IMPORTE BRUTO DEL ITEM
                    e_Det.n_idpordsc = Convert.ToDouble(FgItems.GetData(n_row, 5));           // PORCENTAJE DE DESCUENTO DEL ITEM
                    e_Det.n_impdsc = Convert.ToDouble(FgItems.GetData(n_row, 6));             // IMPORTE DEL PORCENTAJE DE DESCUENTO
                    e_Det.n_preuni = Convert.ToDouble(FgItems.GetData(n_row, 7));             // PRECIO UNITARIO DESPUES DEL DESCUENTO
                    e_Det.n_imptot = Convert.ToDouble(FgItems.GetData(n_row, 8));             // IMPORTE TOTAL DEL ITEM DESPUES DEL DESCUENTO

                    l_Detalle.Add(e_Det);
                }
            }

            // CARGAMOS LA LISTA DE DOCUMENTOS ASIGNADOS 
            for (n_row = 2; n_row <= FgDoc.Rows.Count - 1; n_row++)
            {
                if (funFunciones.NulosC(FgDoc.GetData(n_row, 2)) != "")
                {
                    BE_LOG_COMPRASDOC e_Det = new BE_LOG_COMPRASDOC();

                    e_Det.n_idcom = 0;                                                      // ID DE LA COMPRA
                    e_Det.n_idtipdoc = Convert.ToInt32(FgDoc.GetData(n_row, 4));            // ID DEL TIPO DE DOCUMENTO
                    e_Det.n_iddoc = Convert.ToInt32(FgDoc.GetData(n_row, 3));               // ID DEL DOCUMENTO

                    l_Documentos.Add(e_Det);
                }
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (TxtProv.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el proveedor del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtProv.Focus();
                return booEstado;
            }
            if (TxtNumSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumSer.Focus();
                return booEstado;
            }
            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumDoc.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboTipDoc.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTipDoc.Focus();
                return booEstado;
            }
            if (n_QueHace == 1)
            {
                if (objRegistros.BuscarDocumento(Convert.ToInt32(LblIdPro.Text), Convert.ToInt16(CboTipDoc.SelectedValue), TxtNumSer.Text, TxtNumDoc.Text,STU_SISTEMA.EMPRESAID) == true)
                {
                    MessageBox.Show("¡ La " + CboTipDoc.Text.Trim() + " Nº " + TxtNumSer.Text + "-" + TxtNumDoc.Text + " ya existe, ingrese otro !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    TxtNumDoc.Focus();
                    return booEstado;
                }
            }
            if (Convert.ToInt16(CboConPag.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la condicion de pago del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboConPag.Focus();
                return booEstado;
            }
            if (TxtFchVen.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de vencimiento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchVen.Focus();
                return booEstado;
            }
            if (TxtTotal.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el detalle del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                FgItems.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboTipOpe.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de operacion para la compra !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTipOpe.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboTipExi.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de existencia para la compra !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTipExi.Focus();
                return booEstado;
            }

            if (Convert.ToDateTime(TxtFchEmi.Text) > Convert.ToDateTime(TxtFchVen.Text))
            {
                MessageBox.Show("¡ La fecha de emision del documento no puede ser mayor a la fecha de vencimiento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchVen.Focus();
                return booEstado;
            }
            return booEstado;
        }

        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text != "")
            {
                string strCad = "0000000000" + TxtNumDoc.Text;
                TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
            }
        }

        private void TxtNumSer_Validated(object sender, EventArgs e)
        {
            if (TxtNumSer.Text != "")
            {
                string strCad = "0000" + TxtNumSer.Text;
                TxtNumSer.Text = strCad.Substring(strCad.Length - 4, 4);
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
                if (!c_NumerovalidosFE.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
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
                if (!c_NumerovalidosFE.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void TxtFchEmi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CboMoneda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CboTipOpe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtProv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtTipCam_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CboConPag_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtGlo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!c_Caracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void TxtBasImp1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtBasImp2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtBasImp3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtInafec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtIgv1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtIgv2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtIgv3_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtIgv3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtOtros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtIsc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            VerRegistro(intIdRegistro);
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
                int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    VerRegistro(intIdRegistro);
                }
            }
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            objRegistros = null;
            dtRegistro = null;
            dtLista = null;
            objFormVis = null;

            this.Close();
        }

        private void ToolNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void ToolModificar_Click(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            objRegistros.TraerRegistro(intIdRegistro);
            e_Cabecera = objRegistros.e_Compras;
            Modificar();
        }

        private void ToolEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            DialogResult Rpta;
            if (Grabar() == true)
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();

                if (n_QueHace == 1)
                {
                    Rpta = MessageBox.Show("! El registro se agrego con exito ¡ ¿Desea agregar otro registro? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    Rpta = MessageBox.Show("! El registro se actualizo con exito ¡ ¿Desea agregar otro registro? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                }

                if (DialogResult.Yes == Rpta)
                {
                    Nuevo();
                    ActivarTool();           // LLAMAMOS DE NUEVO A ESTA FUNCION PORQUE SE LLAMA DENTRO DE LA FUNCION NUEVO
                    Bloquea();               // LLAMAMOS DE NUEVO A ESTA FUNCION PORQUE SE LLAMA DENTRO DE LA FUNCION NUEVO
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

        private void FrmManComprasImportacion_Activated(object sender, EventArgs e)
        {
            if (b_SeEjecuto == false)
            {
                b_SeEjecuto = true;
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

        private void FrmManComprasImportacion_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }

        private void FgItems_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgItems.Col == 1)
            {
                int n_idtipexi = 0;
                DataTable dtResul = new DataTable();
                string c_dato = "";

                b_Agregando = true;
                dtResul = objItems.BuscarItem("", "n_id", dtItems, n_idtipexi);
                if (dtResul != null)
                {
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["c_despro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                        FgItems.SetData(FgItems.Row, 1, c_dato);

                        c_dato = dtResul.Rows[0]["c_abrpre"].ToString();        // MOSTRAMOS LA PRESENTACION DEL ITEM
                        FgItems.SetData(FgItems.Row, 2, c_dato);

                        c_dato = dtResul.Rows[0]["n_id"].ToString();            // MOSTRAMOS EL ID DEL ITEM
                        FgItems.SetData(FgItems.Row, 9, c_dato);

                        c_dato = dtResul.Rows[0]["n_idunimed"].ToString();      // MOSTRAMOS LA UNIDAD DE MEDIDA DEL ITEM
                        FgItems.SetData(FgItems.Row, 10, c_dato);
                    }
                }
                b_Agregando = false;
            }
        }

        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }
            DataTable dtResul = new DataTable();
            string c_dato = "";
            if (b_Agregando == true) { return; }

            if (FgItems.Col == 1)
            {
                // FILTRAMOS LAS PRESENTACIONES DEL ITEM SELECCIONADO
                c_dato = FgItems.GetData(FgItems.Row, 9).ToString();                        // OBTENEMOS EL ID DEL ITEM
                dtResul = funDatos.DataTableFiltrar(dtUniMed, "n_idite = " + c_dato + "");
                funFlex.FlexColumnaCombo(FgItems, dtResul, "c_abrpre", 2);                  // UNIDADES DEL MEDIDA DEL ITEMS

                FgItems.AllowEditing = true;
                FgItems.Select(FgItems.Row - 1, 2);
                return;
            }
            if (FgItems.Col == 2)
            {
                FgItems.Select(FgItems.Row - 1, 3);
                return;
            }
            if (FgItems.Col == 3)
            {
                CalcularTotalFila();
                FgItems.Select(FgItems.Row - 1, 4);

                return;
            }
            if (FgItems.Col == 4)
            {
                CalcularTotalFila();
                FgItems.Select(FgItems.Row - 1, 5);

                return;
            }
            if (FgItems.Col == 5)
            {
                CalcularTotalFila();
                FgItems.Select(FgItems.Row - 1, 6);

                return;
            }
            if (FgItems.Col == 6)
            {
                CalcularTotalFila();
                FgItems.Select(FgItems.Row - 1, 7);

                return;
            }
            if (FgItems.Col == 7)
            {
                CalcularTotalFila();
                FgItems.Select(FgItems.Row - 1, 8);
                return;
            }
            if (FgItems.Col == 8)
            {
                FgItems.Select(FgItems.Row - 1, 1);
                FgItems.SetData(FgItems.Row + 1, 1, FgItems.GetData(FgItems.Row, 1));
                return;
            }
        }
        void CalcularTotalFila()
        {
            b_Agregando = true;
            double n_preuniafe = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row, 4)));
            double n_preuniinafe = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row, 5))); ;
            double n_can = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row, 3)));
            double n_pordsc = 0;
            double n_prenet = 0;
            double n_pretot = 0;
            double n_pretotina = 0;
            double n_pretotite = 0;
            double n_valor = 0;
            double n_valor2 = 0;
            int n_row = 0;

            n_prenet = (n_preuniafe + n_preuniinafe) * ((n_pordsc / 100) + 1);
            n_pretot = (n_preuniafe * n_can);
            n_pretotina = (n_preuniinafe * n_can);
            n_pretotite = n_pretot + n_pretotina;

            FgItems.SetData(FgItems.Row, 7, n_prenet.ToString("0.000000"));
            FgItems.SetData(FgItems.Row, 8, n_pretotite.ToString("0.000000"));



            double n_impafe = 0;
            double n_impinafe = 0;
            double n_totafe = 0;
            double n_totinafe = 0;

            for (n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
            {

                n_impafe = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 4)));
                n_impinafe = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 5)));
                n_can = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 3)));
                n_valor = (n_valor + (n_impafe * n_can));
                n_valor2 = (n_valor2 + (n_impinafe * n_can));
            }
            n_totafe = n_valor;
            n_totinafe = n_valor2;

            TxtBasImp1.Text = Math.Round(n_totafe, 2).ToString("0.00");
            TxtBasImp2.Text = "0.00";
            TxtBasImp3.Text = "0.00";
            TxtInafec.Text = Math.Round(n_totinafe, 2).ToString("0.00");

            double n_igv = 0;
            double n_igv2 = 0;
            double n_igv3 = 0;
            n_igv = ((Convert.ToDouble(TxtBasImp1.Text) * ((n_porigv / 100) + 1)) - Convert.ToDouble(TxtBasImp1.Text));
            //TxtIgv1.Text = ((Convert.ToDouble(TxtBasImp1.Text) * ((n_porigv / 100) + 1)) - Convert.ToDouble(TxtBasImp1.Text)).ToString("0.00");
            TxtIgv1.Text = Math.Round(n_igv, 2).ToString("0.00");

            n_igv2 = (Convert.ToDouble(TxtBasImp2.Text) * ((n_porigv / 100) + 1));
            //TxtIgv2.Text = (Convert.ToDouble(TxtBasImp2.Text) * ((n_porigv / 100) + 1)).ToString("0.00");
            TxtIgv2.Text = Math.Round(n_igv2, 2).ToString("0.00");

            n_igv = (Convert.ToDouble(TxtBasImp3.Text) * ((n_porigv / 100) + 1));
            //TxtIgv3.Text = (Convert.ToDouble(TxtBasImp3.Text) * ((n_porigv / 100) + 1)).ToString("0.00");
            TxtIgv3.Text = Math.Round(n_igv3, 2).ToString("0.00");

            TxtOtros.Text = "0.00";
            TxtIsc.Text = "0.00";

            double n_total = Convert.ToDouble(TxtBasImp1.Text) + Convert.ToDouble(TxtBasImp2.Text) + Convert.ToDouble(TxtBasImp3.Text) + Convert.ToDouble(TxtInafec.Text);
            n_total = (n_total + Convert.ToDouble(TxtIgv1.Text) + Convert.ToDouble(TxtIgv2.Text) + Convert.ToDouble(TxtIgv3.Text) + Convert.ToDouble(TxtOtros.Text) + Convert.ToDouble(TxtIsc.Text));
            TxtTotal.Text = n_total.ToString();
            b_Agregando = false;
        }

        private void FgItems_EnterCell(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count == 2) { return; }
            if (b_Agregando == true) { return; }

            DataTable dtResul = new DataTable();
            //int n_idtipproducto = 0;
            //string strDesTipPro = "";

            if ((FgItems.Col == 7) || (FgItems.Col == 8))
            {
                FgItems.AllowEditing = false;
            }
            else
            {
                FgItems.AllowEditing = true;
            }
            //if (FgItems.Col == 1)
            //{
            //    if (FgItems.Row >= 2)
            //    {
            //        if (funFunciones.NulosC(FgItems.GetData(FgItems.Row - 1, 1)) == "") { FgItems.AllowEditing = false; return; }
            //    }
            //    funFlex.FlexColumnaCombo(FgItems, dtTipoExis, "c_des", 1);
            //}

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
            //    }
            //    FgItems.Cols[2].ComboList = "...";
            //}
            //if (FgItems.Col == 3)
            //{
            //    // OBTENEMOS LA DESCRIPCIO DEL ITEM
            //    strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 2));

            //    if (strDesTipPro == "")
            //    {
            //        FgItems.AllowEditing = false;
            //        funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 2);
            //        return;
            //    }

            //    // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
            //    dtResul = funDatos.DataTableFiltrar(dtItems, "c_despro = '" + strDesTipPro + "'");
            //    if (dtResul.Rows.Count != 0)
            //    {
            //        n_idtipproducto = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());

            //        // FILTRAMOS LAS PRESENTACIONES DEL ITEM SELECCIONADO
            //        dtResul = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_idtipproducto + "");
            //        funFlex.FlexColumnaCombo(FgItems, dtResul, "c_abrpre", 3);    // ITEMS
            //    }
            //}

            //if (FgItems.Col == 4)
            //{
            //    // OBTENEMOS LA DESCRIPCIO DEL ITEM
            //    strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 3));

            //    if (strDesTipPro == "")
            //    {
            //        FgItems.AllowEditing = false;
            //        return;
            //    }
            //}
            FgItems.AllowEditing = true;
        }

        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 2)
            {
                if (!c_Caracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 4)
            {
                if (!c_Numerovalidos.Contains(e.KeyChar))
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

            if (b_Agregando == true) { return; }
        }

        private void CmdBusPro_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            objPro.mysConec = mysConec;
            dtResult = objPro.BuscarCliPro(dtPro, 1, "n_id", "");
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

        private void OptConDoc_CheckedChanged(object sender, EventArgs e)
        {
            if (b_Agregando == true) { return; }
            if (OptConDoc.Checked == true)
            {
                OptDelPro.Enabled = true;
                OptOtrPro.Enabled = true;

                CmdDelItem.Enabled = false;
                CmdAddNewItem.Enabled = false;

                CmdDelDoc.Enabled = true;
                CmdVerDoc.Enabled = true;

                FgItems.Rows.Count = 2;
                FgDoc.Rows.Count = 11;
                l_Detalle.Clear();
                l_Documentos.Clear();
            }
        }

        private void OptSinDoc_CheckedChanged(object sender, EventArgs e)
        {
            if (b_Agregando == true) { return; }
            if (OptSinDoc.Checked == true)
            {
                OptDelPro.Enabled = false;
                OptOtrPro.Enabled = false;

                CmdDelItem.Enabled = true;
                CmdAddNewItem.Enabled = true;

                CmdDelDoc.Enabled = false;
                CmdVerDoc.Enabled = false;

                FgItems.Rows.Count = 2;
                FgItems.Rows.Count = n_NumFilasDocumento;
                FgDoc.Rows.Count = 2;
            }
        }

        private void CmdDelItem_Click(object sender, EventArgs e)
        {
            FgItems.RemoveItem(FgItems.Row);
            FgItems.Rows.Count = FgItems.Rows.Count + 1;
        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            if (b_Agregando == true) { return; }

            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            ListarItems();
            DgLista.DataSource = dtLista;

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

        private void FgDoc_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (LblIdPro.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el proveedor del bien o servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdBusPro.Focus();
                return;
            }
            if (FgDoc.Col == 1)
            {
                string c_dato = "";
                DataTable dtResul = new DataTable();
                CN_alm_movimientos objMov = new CN_alm_movimientos();
                objMov.mysConec = mysConec;

                b_Agregando = true;
                c_dato = DocumentosAdicionados();

                if (OptDelPro.Checked == true) { dtResul = objMov.BuscarMovimientos(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblIdPro.Text), c_dato); }
                if (OptOtrPro.Checked == true) { dtResul = objMov.BuscarMovimientos(STU_SISTEMA.EMPRESAID, 0, c_dato); }

                if (dtResul != null)
                {
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["c_abr"].ToString();           // LMOSTRAMOS LA ABREVIATURA DEL TIPO DE DOCUMENTO
                        FgDoc.SetData(FgDoc.Row, 1, c_dato);

                        c_dato = dtResul.Rows[0]["c_numdoc"].ToString();        // MOSTRAMOS EL NUMERO DE DOCUMENTO
                        FgDoc.SetData(FgDoc.Row, 2, c_dato);

                        c_dato = dtResul.Rows[0]["n_id"].ToString();            // MOSTRAMOS EL ID DEL DOCUMENTO
                        FgDoc.SetData(FgDoc.Row, 3, c_dato);

                        c_dato = dtResul.Rows[0]["n_idtipdoc"].ToString();            // MOSTRAMOS EL ID DEL TIPO DE DOCUMENTO
                        FgDoc.SetData(FgDoc.Row, 4, c_dato);

                        AdicionarItem(Convert.ToInt32(dtResul.Rows[0]["n_id"]));
                    }
                }
                b_Agregando = false;
            }
        }
        string DocumentosAdicionados()
        {
            int n_row = 0;
            string c_cadena = "";
            for (n_row = 2; n_row <= FgDoc.Rows.Count - 1; n_row++)
            {
                if (funFunciones.NulosC(FgDoc.GetData(n_row, 3)) != "")
                {
                    if (n_row == 2)
                    {
                        c_cadena = c_cadena + funFunciones.NulosC(FgDoc.GetData(n_row, 3));
                    }
                    else
                    {
                        c_cadena = c_cadena + "," + funFunciones.NulosC(FgDoc.GetData(n_row, 3));
                    }
                }
            }
            return c_cadena;
        }
        void AdicionarItem(int n_IdMovimiento)
        {
            CN_alm_movimientos objMov = new CN_alm_movimientos();
            BE_ALM_MOVIMIENTOS_CONSULTA entMov = new BE_ALM_MOVIMIENTOS_CONSULTA();

            objMov.mysConec = mysConec;
            entMov = objMov.TraerRegistro(n_IdMovimiento);
            if (entMov.lst_items.Count != 0)
            {
                AdicionarLista(entMov.lst_items);
            }
        }
        void AdicionarLista(List<BE_ALM_MOVIMIENTOSDET_CONSULTA> lstDetalle)
        {
            int n_row = 0;
            int n_fil = 0;
            string c_dato = "";

            for (n_row = 0; n_row <= lstDetalle.Count - 1; n_row++)
            {
                bool b_SeEncontro = false;
                for (n_fil = 0; n_fil <= l_Detalle.Count - 1; n_fil++)
                {
                    if (l_Detalle[n_fil].n_iditem == lstDetalle[n_row].n_idite)
                    {
                        l_Detalle[n_fil].n_canpro = (l_Detalle[n_fil].n_canpro + lstDetalle[n_row].n_can);
                        b_SeEncontro = true;
                    }
                }
                if (b_SeEncontro == false)
                {
                    BE_LOG_COMPRASDET entComDet = new BE_LOG_COMPRASDET();
                    entComDet.n_idcom = 0;
                    entComDet.n_iditem = lstDetalle[n_row].n_idite;

                    c_dato = funDatos.DataTableBuscar(dtUniMed, "n_idite", "n_id", lstDetalle[n_row].n_idite.ToString(), "N").ToString();

                    entComDet.n_idunimed = Convert.ToInt32(c_dato);
                    entComDet.n_canpro = lstDetalle[n_row].n_can;
                    entComDet.n_preuni = 0;
                    entComDet.n_idpordsc = 0;
                    entComDet.n_impdsc = 0;
                    entComDet.n_preuni = 0;
                    entComDet.n_imptot = 0;

                    l_Detalle.Add(entComDet);
                }
            }
            Mostrardetalle();
        }

        private void CmdDelDoc_Click(object sender, EventArgs e)
        {
            int n_idmovimiento = Convert.ToInt32(FgDoc.GetData(FgDoc.Row, 3));
            if (n_idmovimiento == 0) { return; }

            FgDoc.RemoveItem(FgDoc.Row);
            EliminarItem(n_idmovimiento);
            FgDoc.Rows.Count = FgDoc.Rows.Count + 1;
        }
        void EliminarItem(int n_IdMovimiento)
        {
            CN_alm_movimientos objMov = new CN_alm_movimientos();
            BE_ALM_MOVIMIENTOS_CONSULTA entMov = new BE_ALM_MOVIMIENTOS_CONSULTA();

            objMov.mysConec = mysConec;
            entMov = objMov.TraerRegistro(n_IdMovimiento);
            if (entMov.lst_items.Count != 0)
            {
                EliminarLista(entMov.lst_items);
            }
        }
        void EliminarLista(List<BE_ALM_MOVIMIENTOSDET_CONSULTA> lstDetalle)
        {
            int n_row = 0;
            int n_fil = 0;

            for (n_row = 0; n_row <= lstDetalle.Count - 1; n_row++)
            {
                for (n_fil = 0; n_fil <= l_Detalle.Count - 1; n_fil++)
                {
                    if (l_Detalle[n_fil].n_iditem == lstDetalle[n_row].n_idite)
                    {
                        l_Detalle[n_fil].n_canpro = (l_Detalle[n_fil].n_canpro - lstDetalle[n_row].n_can);
                    }
                }
            }
            Mostrardetalle();
        }

        private void FgDoc_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { FgDoc.AllowEditing = false; return; }

            FgDoc.AllowEditing = true;
        }

        private void CboTipExi_SelectedValueChanged(object sender, EventArgs e)
        {
            if (b_Agregando == true) { return; }
            int n_idtipexi = Convert.ToInt16(CboTipExi.SelectedValue);

            if ((n_idtipexi == 1) || (n_idtipexi == 3) || (n_idtipexi == 6))
            {
                OptSinDoc.Enabled = false;
                OptConDoc.Enabled = true;
                OptConDoc.Checked = true;
                OptDelPro.Checked = true;
            }
            else
            {
                OptSinDoc.Enabled = true;
                OptConDoc.Enabled = true;
            }

        }

        private void TxtFchEmi_Validated(object sender, EventArgs e)
        {
            if (b_Agregando == true) { return; }
            if (TxtFchEmi.Text != "")
            {
                DataTable dtResul = new DataTable();
                dtResul = funDatos.DataTableFiltrar(dtTc, "d_fecha = '" + TxtFchEmi.Text + "'");
                if (dtResul.Rows.Count != 0)
                {
                    TxtTipCam.Text = dtResul.Rows[0]["n_impven"].ToString();
                }
            }
        }

        private void CboConPag_SelectedValueChanged(object sender, EventArgs e)
        {
            if (b_Agregando == true) { return; }
            DataTable dtResul = new DataTable();

            DateTime dtfch = Convert.ToDateTime(TxtFchEmi.Text);
            dtResul = funDatos.DataTableFiltrar(dtConPag, "n_id = " + Convert.ToInt16(CboConPag.SelectedValue) + "");
            if (dtResul.Rows.Count != 0)
            {
                int n_numdias = Convert.ToInt32(dtResul.Rows[0]["n_numdia"]);
                TxtFchVen.Text = dtfch.AddDays(n_numdias).ToString("dd/MM/yyyy");
            }
        }

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-LOG-COM-" + STU_SISTEMA.ANOTRABAJO.ToString() + Convert.ToInt16(CboMeses.SelectedValue).ToString("00") + ".xls";
            DgLista.ExportTo(c_NomArchivo);
            MessageBox.Show("! Se exporto con exito la informacion en el archivo " + c_NomArchivo + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            objRegistros.mysConec = mysConec;
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            if (objRegistros.RegeneraAsientos(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 8,2) == true)
            {
                MessageBox.Show("¡ Los asientos contables se crearon con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                ListarItems();
                DgLista.DataSource = dtLista;
            }
        }

        private void CmdVerAsiento_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objConta = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objConta.mysConec = mysConec;
            objConta.STU_SISTEMA = STU_SISTEMA;
            objConta.VerAsiento(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboMeses.SelectedValue), 8, LblNumAsi.Text);
            objConta = null;
        }

        private void TxtNumRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!c_Numerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void TxtNumRuc_Validated(object sender, EventArgs e)
        {
            DataTable dtresul = new DataTable();
            dtresul = funDatos.DataTableFiltrar(dtPro, "c_numdoc = '" + TxtNumRuc.Text + "'");

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

        private void FgDoc_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {

                        //rightClickMenuStrip.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position
                    }
                    break;
            }
        }
    }
}
