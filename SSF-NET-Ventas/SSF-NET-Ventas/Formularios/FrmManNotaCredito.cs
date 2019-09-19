using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Sistema;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Sunat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SSF_NET_Ventas.Formularios
{
    public partial class FrmManNotaCredito : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_TIPODOCUMENTO;

        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_vta_ventas objRegistros = new CN_vta_ventas();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sun_tipmon objTipMon = new CN_sun_tipmon();
        CN_mae_clipro objCliPro = new CN_mae_clipro();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_mae_condpago objCondPag = new CN_mae_condpago();
        CN_vta_vendedor objVendedor = new CN_vta_vendedor();
        CN_alm_inventariounimed ObjAlmUniMed = new CN_alm_inventariounimed();
        CN_sun_tipexi objTipoExi = new CN_sun_tipexi();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sun_tipcam ObjTC = new CN_sun_tipcam();
        CN_sun_fe_catalogo07 objCat07 = new CN_sun_fe_catalogo07();
        CN_sun_fe_catalogo09 objCat09 = new CN_sun_fe_catalogo09();
        CN_sun_fe_catalogo10 objCat10 = new CN_sun_fe_catalogo10();
        CN_sys_empresa objEmp = new CN_sys_empresa();
        Cls_NumeroLetra funLet = new Cls_NumeroLetra();
        CN_sun_fe_catalogo17 objCat51 = new CN_sun_fe_catalogo17();

        SIAC_Objetos.Funciones objFunciones = new SIAC_Objetos.Funciones();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_VTA_VENTAS BE_ListaReg = new BE_VTA_VENTAS();
        BE_VTA_VENTAS BE_Registro = new BE_VTA_VENTAS();
        BE_VTA_VENTASDET BE_VentaDetalle = new BE_VTA_VENTASDET();
        List<BE_VTA_VENTASDET> LstDetalle = new List<BE_VTA_VENTASDET>();   // CREAMOS UNA LISTA DEL TIPO BE_VTA_GUIASDET PARA ALMACENAR LOS ITEMS
        List<BE_VTA_VENTASNCDOC> LstDocNC = new List<BE_VTA_VENTASNCDOC>();
        BE_SYS_EMPRESA entEmpresa = new BE_SYS_EMPRESA();

        // DATATABLE LOCALES
        DataTable dtRegistros = new DataTable();
        DataTable dtCliPro = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtTipMon = new DataTable();
        DataTable dtTipDoc = new DataTable();
        DataTable dtTipDocAnu = new DataTable();
        DataTable dtConPag = new DataTable();
        DataTable dtVendedor = new DataTable();
        DataTable dtTipoExis = new DataTable();
        DataTable dtPresentaItem = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtMoviDetalle = new DataTable();
        DataTable dtTC = new DataTable();
        DataTable dtCat09 = new DataTable();
        DataTable dtCat10 = new DataTable();
        DataTable dtDocMod = new DataTable();
        DataTable dtAnex07 = new DataTable();
        DataTable dtCat51 = new DataTable();
        DataTable dtDocNC = new DataTable();

        // VARIABLES LOCALES
        int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        double n_TASAIGV = 18;
        string[,] arrCabeceraDg1 = new string[18, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex3 = new string[6, 5];
        string[,] arrCabeceraFlex1 = new string[8, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strNumerovalidos2 = "1234567890.EF" + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^-" + (char)8;
        public FrmManNotaCredito()
        {
            InitializeComponent();
        }
        private void FrmManNotaCredito_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTable dtResul = new DataTable();

            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboMon, dtTipMon, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipOpe, dtCat51, "n_id", "c_des");
            //funDatos.ComboBoxCargarDataTable(CboCliente, dtCliPro, "n_id", "c_nombre");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtTipDoc, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMot, dtConPag, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipExi, dtTipoExis, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDocMod, dtVendedor, "n_id", "c_apenom");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");

            if (n_TIPODOCUMENTO == 1)
            {
                dtResul = funDatos.DataTableFiltrar(dtDocMod, "n_id IN (2, 4, 9)");
                funDatos.ComboBoxCargarDataTable(CboTipDocMod, dtResul, "n_id", "c_des");
                funDatos.ComboBoxCargarDataTable(CboMot, dtCat09, "n_id", "c_des");

                dtResul = funDatos.DataTableFiltrar(dtTipDocAnu, "n_id IN (8)");
            }
            if (n_TIPODOCUMENTO == 2)
            {
                dtResul = funDatos.DataTableFiltrar(dtDocMod, "n_id IN (2, 4, 8)");
                funDatos.ComboBoxCargarDataTable(CboTipDocMod, dtResul, "n_id", "c_des");
                funDatos.ComboBoxCargarDataTable(CboMot, dtCat10, "n_id", "c_des");

                dtResul = funDatos.DataTableFiltrar(dtTipDocAnu, "n_id IN (9)");
            }

            funDatos.ComboBoxCargarDataTable(CboTipDocAnu, dtResul, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 627;
            this.Width = 1040;

            VerTool();
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            arrCabeceraFlex1[0, 0] = "Producto";
            arrCabeceraFlex1[0, 1] = "498";
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Unidad Medida";
            arrCabeceraFlex1[1, 1] = "60";
            arrCabeceraFlex1[1, 2] = "S";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_desunimed";

            arrCabeceraFlex1[2, 0] = "Cantidad";
            arrCabeceraFlex1[2, 1] = "60";
            arrCabeceraFlex1[2, 2] = "D";
            arrCabeceraFlex1[2, 3] = "0.00";
            arrCabeceraFlex1[2, 4] = "c_itedes";

            arrCabeceraFlex1[3, 0] = "Precio Unitario";
            arrCabeceraFlex1[3, 1] = "80";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "0.000000";
            arrCabeceraFlex1[3, 4] = "c_itepredes";

            arrCabeceraFlex1[4, 0] = "Descuento";
            arrCabeceraFlex1[4, 1] = "0";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.000000";
            arrCabeceraFlex1[4, 4] = "n_can";

            arrCabeceraFlex1[5, 0] = "Precio Neto";
            arrCabeceraFlex1[5, 1] = "0";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.000000";
            arrCabeceraFlex1[5, 4] = "n_numlot";

            arrCabeceraFlex1[6, 0] = "Importe Total";
            arrCabeceraFlex1[6, 1] = "80";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_numlot";

            arrCabeceraFlex1[7, 0] = "Tipo Venta";
            arrCabeceraFlex1[7, 1] = "50";
            arrCabeceraFlex1[7, 2] = "C";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "n_numlot";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;

            arrCabeceraFlex3[0, 0] = "T.D.";
            arrCabeceraFlex3[0, 1] = "35";
            arrCabeceraFlex3[0, 2] = "C";
            arrCabeceraFlex3[0, 3] = "";
            arrCabeceraFlex3[0, 4] = "c_abr";

            arrCabeceraFlex3[1, 0] = "Nº Documento";
            arrCabeceraFlex3[1, 1] = "110";
            arrCabeceraFlex3[1, 2] = "C";
            arrCabeceraFlex3[1, 3] = "";
            arrCabeceraFlex3[1, 4] = "c_numdoc";

            arrCabeceraFlex3[2, 0] = "Importe";
            arrCabeceraFlex3[2, 1] = "70";
            arrCabeceraFlex3[2, 2] = "D";
            arrCabeceraFlex3[2, 3] = "0.00";
            arrCabeceraFlex3[2, 4] = "c_numdoc";

            arrCabeceraFlex3[3, 0] = "Aplicar";
            arrCabeceraFlex3[3, 1] = "70";
            arrCabeceraFlex3[3, 2] = "D";
            arrCabeceraFlex3[3, 3] = "0.00";
            arrCabeceraFlex3[3, 4] = "c_numdoc";

            arrCabeceraFlex3[4, 0] = "Saldo";
            arrCabeceraFlex3[4, 1] = "70";
            arrCabeceraFlex3[4, 2] = "D";
            arrCabeceraFlex3[4, 3] = "0.00";
            arrCabeceraFlex3[4, 4] = "c_numdoc";

            arrCabeceraFlex3[5, 0] = "n_iddocumento";
            arrCabeceraFlex3[5, 1] = "0";
            arrCabeceraFlex3[5, 2] = "N";
            arrCabeceraFlex3[5, 3] = "";
            arrCabeceraFlex3[5, 4] = "c_desunimed";

            funFlex.FlexMostrarDatos(FgDocCredito, arrCabeceraFlex3, dtMoviDetalle, 1, false);
            

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
            LblTasIgv.Text = n_TASAIGV.ToString() + " %";

            if (n_TIPODOCUMENTO == 1)
            {
                // SI EL TIPO DE DOCUMENTO ES 1 = NOTA DE CREDITO
                this.Text = "VENTAS - MANTENIMIENTO NOTAS DE CREDITO";
            }
            else
            {
                // SI EL TIPO DE DOCUMENTO ES 1 = NOTA DE CREDITO
                this.Text = "VENTAS - MANTENIMIENTO NOTAS DE DEBITO";
            }
        }
        void DataTableCargar()
        {
            DataTable dtResul = new DataTable();
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO

            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 14);
            if (n_TIPODOCUMENTO == 1)
            {
                dtResul = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc = 8)");
            }
            if (n_TIPODOCUMENTO == 2)
            {
                dtResul = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc = 9)");
            }
            
            dtRegistros = dtResul;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(23, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(23);

            CargarDatosAdicionales();
        }
        void CargarDatosAdicionales()
        { 
            objCliPro.mysConec = mysConec;
            dtCliPro = objCliPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objItems.mysConec = mysConec;
            dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);                                        // CARGAMOS TODOS LOS DEPARTAMENTOS

            objTipMon.mysConec = mysConec;
            dtTipMon = objTipMon.Listar();

            objTipDoc.mysConec = mysConec;
            dtTipDoc = objTipDoc.Listar();
            dtDocMod = objTipDoc.Listar();
            dtTipDocAnu = objTipDoc.Listar();

            objCondPag.mysConec = mysConec;
            dtConPag = objCondPag.Listar();

            objTipoExi.mysConec = mysConec;
            dtTipoExis = objTipoExi.Listar();

            objVendedor.mysConec = mysConec;
            dtVendedor = objVendedor.Listar(STU_SISTEMA.EMPRESAID);

            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            ObjTC.mysConec = mysConec;
            dtTC = ObjTC.Listartcano(151, STU_SISTEMA.ANOTRABAJO.ToString());

            objCat07.mysConec = mysConec;
            objCat07.Listar();
            dtAnex07 = objCat07.dtLista;

            objCat09.mysConec = mysConec;
            objCat09.Listar();
            dtCat09 = objCat09.dtLista;

            objCat10.mysConec = mysConec;
            objCat10.Listar();
            dtCat10 = objCat10.dtLista;

            objEmp.mysConec = mysConec;
            objEmp.TraerRegistro(STU_SISTEMA.EMPRESAID);
            entEmpresa = objEmp.e_Empresa;

            objCat51.mysConec = mysConec;
            objCat51.Listar();
            dtCat51 = objCat51.dtLista;
        }
        void VerTool()
        {
            if (entEmpresa.n_aplife == 1)
            {
                ToolEnviar.Visible = false;
                TooRecArc.Visible = false;
                ToolCorreo.Visible = false;
                ToolBaja.Visible = false;
                ToolEliminar2.Visible = true;
            }
            else
            {
                ToolEnviar.Visible = true;
                TooRecArc.Visible = true;
                ToolCorreo.Visible = true;
                ToolBaja.Visible = true;

                ToolEliminar2.Visible = false;
            }
        }
        void ListarItems()
        {
            DataTable dtResul = new DataTable();

            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);

            if (n_TIPODOCUMENTO == 1)
            {
                dtResul = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc = 8)");
            }
            if (n_TIPODOCUMENTO == 2)
            {
                dtResul = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc = 9)");
            }
            dtRegistros = dtResul;
            LblNumReg.Text = (dtRegistros.Rows.Count).ToString();

            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtRegistros, true);

            if (entEmpresa.n_aplife == 1)
            {
                DgLista.Splits[0].DisplayColumns[16].Width = 30;
                DgLista.Splits[0].DisplayColumns[15].Visible = false;
                DgLista.Splits[0].DisplayColumns[14].Visible = false;
                DgLista.Splits[0].DisplayColumns[5].Width = 260;
            }
            else
            {
                DgLista.Splits[0].DisplayColumns[16].Width = 30;
                DgLista.Splits[0].DisplayColumns[15].Visible = true;
                DgLista.Splits[0].DisplayColumns[14].Visible = true;

                DgLista.Splits[0].DisplayColumns[5].Width = 200;
            }
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
            DataTable dtResul = new DataTable();
            booAgregando = true;

            objRegistros.mysConec = mysConec;
            BE_Registro = objRegistros.TraerRegistro(n_IdRegistro);
            LstDetalle = objRegistros.LstDetalle;

            objRegistros.Consulta27(n_IdRegistro);
            dtDocNC = objRegistros.dtLista1;

            LblNumAsi.Text = BE_Registro.c_numreg;
            TxtFchEmiDoc.Text = BE_Registro.d_fchdoc.ToString();
            CboMon.SelectedValue = BE_Registro.n_idmon;
            //CboCliente.SelectedValue = BE_Registro.n_idcli;
            LblIdCliente.Text = BE_Registro.n_idcli.ToString();
            TxtNumRuc.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_numdoc", BE_Registro.n_idcli.ToString(), "N").ToString();
            TxtCliente.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_nombre", BE_Registro.n_idcli.ToString(), "N").ToString();

            CboTipDoc.SelectedValue = BE_Registro.n_idtipdoc;
            LblTC.Text = BE_Registro.n_tc.ToString("0.000");
            TxtNumSer.Text = BE_Registro.c_numser;
            TxtNumDoc.Text = BE_Registro.c_numdoc;
            CboMot.SelectedValue = BE_Registro.n_idtipmot;
            CboTipExi.SelectedValue = BE_Registro.n_idtippro;
            CboTipOpe.SelectedValue = BE_Registro.n_idtipope;

            CboTipDocMod.SelectedValue = BE_Registro.n_idtipdocmod;
            TxtFchVen.Text = BE_Registro.d_fchven.ToString();
            TxtGlosa.Text = BE_Registro.c_glosa;
            TxtSust.Text = BE_Registro.c_motnc;
            //LblSubTot.Text = BE_Registro.n_impsubtot.ToString("0.00");
            //TxtPorDes.Text = BE_Registro.n_pordsc.ToString("0.00");
            LblImpBru.Text = BE_Registro.n_impbru.ToString("0.00");
            LblImpIna.Text = BE_Registro.n_impinaf.ToString("0.00");
            LblImpIgv.Text = BE_Registro.n_impigv.ToString("0.00");
            LblImpIsc.Text = BE_Registro.n_impisc.ToString("0.00");
            LblImpTot.Text = BE_Registro.n_imptotven.ToString("0.00");

            //if (BE_Registro.n_oriitem == 1)
            //{
            //    OptSinGuia.Checked = true;
            //    OptConGuia.Checked = false;
            //    OptOrdPed.Checked = false;
            //}

            //if (BE_Registro.n_oriitem == 2)
            //{
            //    OptSinGuia.Checked = false;
            //    OptConGuia.Checked = true;
            //    OptOrdPed.Checked = false;
            //}

            //if (BE_Registro.n_oriitem == 2)
            //{
            //    OptSinGuia.Checked = false;
            //    OptConGuia.Checked = false;
            //    OptOrdPed.Checked = true;
            //}

            // MOSTRAMOS EL DOCUMENTO QUE MODIFICA
            dtResul = objRegistros.Consulta5(STU_SISTEMA.EMPRESAID, BE_Registro.n_iddocmod);
            if (objRegistros.booOcurrioError != true)
            {
                funDatos.ComboBoxCargarDataTable(CboNumDocMod, dtResul, "n_id", "c_numdoc");
                CboNumDocMod.SelectedValue = BE_Registro.n_iddocmod;
            }

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);

            int n_fila = 2;
            int n_idIte;
            int n_idUniMed;
            DataTable DtFiltro = new DataTable();
            string strCadenaFiltro;

            foreach (BE_VTA_VENTASDET element in LstDetalle)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;

                //n_idTipExi = element.n_iditem;
                n_idIte = element.n_iditem;
                n_idUniMed = element.n_idunimed;


                // MOSTRAMOS EL ITEM
                strCadenaFiltro = "n_id = " + n_idIte + "";
                DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                FgItems.SetData(n_fila, 1, DtFiltro.Rows[0]["c_despro"].ToString());

                // MOSTRAMOS LA UNIDAD DE MEDIDA
                strCadenaFiltro = "n_id = " + n_idUniMed + "";
                DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                FgItems.SetData(n_fila, 2, DtFiltro.Rows[0]["c_abrpre"].ToString());

                FgItems.SetData(n_fila, 3, element.n_canpro);
                FgItems.SetData(n_fila, 4, element.n_preunibru);
                FgItems.SetData(n_fila, 5, element.n_pordsc);
                FgItems.SetData(n_fila, 6, element.n_preuninet);
                FgItems.SetData(n_fila, 7, element.n_imptot);
                FgItems.SetData(n_fila, 8, element.n_idtipven);

                //BE_Detalle.n_idtipven = Convert.ToInt32(FgItems.GetData(n_fila, 9).ToString());
                n_fila++;
            }
            if (LstDetalle.Count == 0)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;
            }

            string c_dato = "";
            FgDocCredito.Rows.Count = 1;
            if (dtDocNC.Rows.Count != 0)
            {
                for (n_fila = 0; n_fila <= dtDocNC.Rows.Count - 1; n_fila++)
                {
                    FgDocCredito.Rows.Count = FgDocCredito.Rows.Count + 1;

                    c_dato = dtDocNC.Rows[n_fila]["c_abr"].ToString();
                    FgDocCredito.SetData(FgDocCredito.Rows.Count - 1, 1, c_dato);

                    c_dato = dtDocNC.Rows[n_fila]["c_numdoc"].ToString();
                    FgDocCredito.SetData(FgDocCredito.Rows.Count - 1, 2, c_dato);

                    c_dato = Convert.ToDouble(dtDocNC.Rows[n_fila]["n_saldo"]).ToString("0.00");
                    FgDocCredito.SetData(FgDocCredito.Rows.Count - 1, 3, c_dato);

                    c_dato = Convert.ToDouble(dtDocNC.Rows[n_fila]["n_acuenta"]).ToString("0.00");
                    FgDocCredito.SetData(FgDocCredito.Rows.Count - 1, 4, c_dato);

                    c_dato = Convert.ToDouble(dtDocNC.Rows[n_fila]["n_nuevosaldo"]).ToString("0.00");
                    FgDocCredito.SetData(FgDocCredito.Rows.Count - 1, 5, c_dato);

                    c_dato = Convert.ToInt32(dtDocNC.Rows[n_fila]["n_iddoc"]).ToString();
                    FgDocCredito.SetData(FgDocCredito.Rows.Count - 1, 6, c_dato);
                }
            }
            booAgregando = false;
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
            
            if (n_TIPODOCUMENTO == 1)
            { 
                CboTipDoc.SelectedValue = 8;
            }
            if (n_TIPODOCUMENTO == 2)
            {
                CboTipDoc.SelectedValue = 9;
            }

            MostrarNumeroSerie();
            LblTC.Text = STU_SISTEMA.TIPOCAMBIO.ToString("0.0000");
            CboMon.SelectedValue = 115;
            CboTipOpe.SelectedValue = 1;
            FgItems.Cols[1].ComboList = "...";
            FgDocCredito.Cols[2].ComboList = "...";
            
            objRegistros.mysConec = mysConec;
            objRegistros.Consulta27(9999999);
            dtDocNC = objRegistros.dtLista1;

            TxtNumRuc.Focus();
            booAgregando = false;
        }
        void Blanquea()
        {
            LblNumAsi.Text = "";
            TxtFchEmiDoc.Text = "";
            TxtFchEmiDoc.CustomFormat = "dd/MM/yyyy";
            TxtFchEmiDoc.Format = DateTimePickerFormat.Custom;
            CboMon.SelectedValue = 0;

            TxtCliente.Text = "";
            LblIdCliente.Text = "";
            TxtNumRuc.Text = "";

            //CboCliente.SelectedValue = 0;
            CboTipDoc.SelectedValue = 0;
            CboTipOpe.SelectedValue = 0;
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            CboMot.SelectedValue = 0;
            CboTipExi.SelectedValue = 0;
            CboTipDocMod.SelectedValue = 0;
            //TxtFchVen.Text = " ";
            //TxtFchVen.CustomFormat = "dd/MM/yyyy";
            //TxtFchVen.Format = DateTimePickerFormat.Custom;
            TxtGlosa.Text = "";
            TxtSust.Text = "";
            //LblSubTot.Text = "";
            //TxtPorDes.Text = "";
            LblImpBru.Text = "";
            LblImpIna.Text = "";
            LblImpIgv.Text = "";
            LblImpIsc.Text = "";
            LblImpTot.Text = "";

            FgItems.Rows.Count = 2;
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;
        }
        void Bloquea()
        {
            TxtFchEmiDoc.Enabled = !TxtFchEmiDoc.Enabled;
            CboMon.Enabled = !CboMon.Enabled;

            TxtCliente.Text = "";
            LblIdCliente.Text = "";
            TxtNumRuc.Enabled = !TxtNumRuc.Enabled;
            CmdBusCli.Enabled = !CmdBusCli.Enabled;

            //CboCliente.Enabled = !CboCliente.Enabled;
            CboTipOpe.Enabled = !CboTipOpe.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            CboMot.Enabled = !CboMot.Enabled;
            CboTipExi.Enabled = !CboTipExi.Enabled;
            CboTipDocMod.Enabled = !CboTipDocMod.Enabled;
            CboNumDocMod.Enabled = !CboNumDocMod.Enabled;
            TxtFchVen.Enabled = !TxtFchVen.Enabled;
            TxtGlosa.Enabled = !TxtGlosa.Enabled;
            TxtSust.Enabled = !TxtSust.Enabled;
            //TxtPorDes.Enabled = !TxtPorDes.Enabled;
        }
        void MostrarEstadoMes(int n_IdEmpresa, int n_IdMes)
        {
            obj_fungen.mysConec = mysConec;
            if (obj_fungen.EstadoPeriodo(n_IdEmpresa, n_IdMes, 2) == true)
            {
                ToolNuevo.Visible = false;
                ToolModificar.Visible = false;

                ToolEliminar2.Visible = false;

                ToolGrabar.Visible = false;
                ToolCancelar.Visible = false;

                if (entEmpresa.n_aplife == 2)
                {
                    ToolBaja.Visible = false;
                    ToolEnviar.Visible = false;
                    TooRecArc.Visible = false;
                    ToolCorreo.Visible = false;
                }
                toolStripSeparator1.Visible = false;
                toolStripSeparator2.Visible = false;

                PicClos1.Visible = true;
                PicClos2.Visible = true;
            }
            else
            {
                ToolNuevo.Visible = true;
                ToolModificar.Visible = true;

                if (entEmpresa.n_aplife == 1)
                {
                    ToolEliminar2.Visible = true;
                }

                ToolGrabar.Visible = true;
                ToolCancelar.Visible = true;

                if (entEmpresa.n_aplife == 2)
                {
                    ToolBaja.Visible = true;
                    ToolEnviar.Visible = true;
                    TooRecArc.Visible = true;
                    ToolCorreo.Visible = true;
                }

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
            ToolEliminar2.Enabled = !ToolEliminar2.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir.Enabled = !ToolImprimir.Enabled;
            //ToolExportar.Enabled = !ToolExportar.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
            ToolBaja.Enabled = !ToolBaja.Enabled;
            ToolEnviar.Enabled = !ToolEnviar.Enabled;
            TooRecArc.Enabled = !TooRecArc.Enabled;
            ToolCorreo.Enabled = !ToolCorreo.Enabled;
            CmdRefDat.Enabled = !CmdRefDat.Enabled;
        }
        void Modificar()
        {
            if (DgLista.RowCount == 0)
            {
                MessageBox.Show("¡ No hay registros que modificar ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            int n_idpue = Convert.ToInt16(DgLista.Columns["n_idpue"].CellValue(DgLista.Row).ToString());

            if (n_idpue != 0)
            {
                MessageBox.Show("¡ No puede modificar este documento, el documento fue generado en otro modulo ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
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

            //n_NumFilasDocumento
            FgItems.Rows.Count = (n_NumFilasDocumento - FgItems.Rows.Count);
            //CboCliente.Focus();
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;

            if (DgLista.RowCount == 0)
            {
                MessageBox.Show("¡ No hay registros que eliminar ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }

            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            int n_idpue = Convert.ToInt16(DgLista.Columns["n_idpue"].CellValue(DgLista.Row).ToString());

            if (n_idpue != 0)
            {
                MessageBox.Show("¡ No puede eliminar este documento, el documento fue generado en otro modulo ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }
            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 14);
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
            CboTipDocMod.Enabled = false;
            DgLista.Focus();
        }
        bool Grabar()
        {
            bool booResultado = false;
            AsignarEntidad();
            if (CamposOK() == false)
            {
                return booResultado;
            }

            objRegistros.LstDetalle = LstDetalle;    // ASIGNAMOS EL DETALLE DE LA GUIA
            objRegistros.LstDocumentosNC = LstDocNC;
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.b_GenerarAsiento = true;
            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(BE_ListaReg);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(BE_ListaReg);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            BE_ListaReg.n_id = BE_Registro.n_id;
            BE_ListaReg.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_ListaReg.n_anotra = STU_SISTEMA.ANOTRABAJO;
            BE_ListaReg.n_idmes = STU_SISTEMA.MESTRABAJO;
            BE_ListaReg.n_idlib = 14;

            BE_ListaReg.c_numreg = LblNumAsi.Text;
            BE_ListaReg.n_idtippro = Convert.ToInt16(CboTipExi.SelectedValue);
            BE_ListaReg.n_idcli = Convert.ToInt32(LblIdCliente.Text);
            BE_ListaReg.n_idpunvencli = 0;
            BE_ListaReg.n_idtipdoc = Convert.ToInt16(CboTipDoc.SelectedValue);
            BE_ListaReg.c_numser = TxtNumSer.Text;
            BE_ListaReg.c_numdoc = TxtNumDoc.Text;
            BE_ListaReg.d_fchreg = Convert.ToDateTime("01/" + BE_ListaReg.n_idmes.ToString("00") + "/" + BE_ListaReg.n_anotra.ToString("0000"));
            BE_ListaReg.d_fchdoc = Convert.ToDateTime(TxtFchEmiDoc.Text);
            BE_ListaReg.d_fchven = Convert.ToDateTime(TxtFchVen.Text);
            BE_ListaReg.n_idconpag = Convert.ToInt16(CboMot.SelectedValue);
            BE_ListaReg.n_idmon = Convert.ToInt16(CboMon.SelectedValue);
            BE_ListaReg.n_impbru = Convert.ToDouble(LblImpBru.Text);
            BE_ListaReg.n_impbru2 = 0;
            BE_ListaReg.n_impbru3 = 0;
            BE_ListaReg.n_impinaf = Convert.ToDouble(LblImpIna.Text);
            BE_ListaReg.n_impigv = Convert.ToDouble(LblImpIgv.Text);
            BE_ListaReg.n_impisc = Convert.ToDouble(LblImpIsc.Text);
            BE_ListaReg.n_impotr = 0;
            BE_ListaReg.n_imptotven = Convert.ToDouble(LblImpTot.Text);
            BE_ListaReg.n_impsubtot = (Convert.ToDouble(LblImpBru.Text) + Convert.ToDouble(LblImpIna.Text));
            BE_ListaReg.n_tc = Convert.ToDouble(LblTC.Text);
            BE_ListaReg.n_impsal = Convert.ToDouble(LblImpTot.Text);
            BE_ListaReg.n_idven = Convert.ToInt16(CboTipDocMod.SelectedValue);
            BE_ListaReg.n_tasaigv = 18;
            BE_ListaReg.c_glosa = TxtGlosa.Text;
            BE_ListaReg.c_motnc = TxtSust.Text;
            //BE_ListaReg.n_impsubtot = 0;
            BE_ListaReg.n_pordsc = 0;

            BE_ListaReg.n_idtipdocmod = Convert.ToInt32(CboTipDocMod.SelectedValue);
            BE_ListaReg.n_iddocmod = Convert.ToInt32(CboNumDocMod.SelectedValue);
            BE_ListaReg.n_idtipmot = Convert.ToInt32(CboMot.SelectedValue);

            string c_mon = "";
            if (Convert.ToDouble(CboMon.SelectedValue) == 115) { c_mon = "soles."; }
            if (Convert.ToDouble(CboMon.SelectedValue) == 151) { c_mon = "dolares americanos."; }
            BE_ListaReg.c_numlet = funLet.Convertir(LblImpTot.Text, true, c_mon);
            //BE_ListaReg.c_numlet = funLet.Convertir(LblImpTot.Text, true);

            BE_ListaReg.n_idtipope = Convert.ToInt32(CboTipOpe.SelectedValue);
            //BE_ListaReg.n_idtipdocmod = Convert.ToInt32(CboTipDocMod.SelectedValue);
            BE_ListaReg.n_iddocref = 0;
            //if (OptSinGuia.Checked == true)
            //{
            //    BE_ListaReg.n_oriitem = 1;
            //}
            //if (OptConGuia.Checked == true)
            //{
            //    BE_ListaReg.n_oriitem = 2;
            //}
            //if (OptOrdPed.Checked == true)
            //{
            //    BE_ListaReg.n_oriitem = 3;
            //}
            BE_ListaReg.n_anulado = 0;

            int n_fila = 0;
            DataTable DtFiltro = new DataTable();
            //string c_tipoexis = "";
            string strCadenaFiltro = "";
            string c_nomitem = "";
            string c_presendes = "";
            List<BE_VTA_VENTASDET> LstDetalleTmp = new List<BE_VTA_VENTASDET>();

            if (FgItems.Rows.Count > 2)
            {
                for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
                {
                    if (funFunciones.NulosC(FgItems.GetData(n_fila, 1)) != "")
                    {
                        BE_VTA_VENTASDET BE_Detalle = new BE_VTA_VENTASDET();

                        c_nomitem = FgItems.GetData(n_fila, 1).ToString();
                        c_presendes = FgItems.GetData(n_fila, 2).ToString();

                        BE_Detalle.n_idvta = BE_ListaReg.n_id;
                        BE_Detalle.n_canpro = Convert.ToDouble(FgItems.GetData(n_fila, 3).ToString());

                        // FILTRAMOS EL ITEM DE LA FILA PARA OBTENER EL ID  
                        strCadenaFiltro = "c_despro = '" + c_nomitem + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                        BE_Detalle.n_iditem = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        // FILTRAMOS LA PRESENTACION PARA OBTENER SU ID
                        strCadenaFiltro = "c_abrpre = '" + c_presendes + "' AND n_idite = " + BE_Detalle.n_iditem + "";
                        DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                        BE_Detalle.n_idunimed = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        BE_Detalle.n_preunibru = Convert.ToDouble(FgItems.GetData(n_fila, 4).ToString());
                        BE_Detalle.n_preuninet = Convert.ToDouble(FgItems.GetData(n_fila, 6).ToString());
                        BE_Detalle.n_imptot = Convert.ToDouble(FgItems.GetData(n_fila, 7).ToString());
                        BE_Detalle.n_idtipven = Convert.ToInt32(FgItems.GetData(n_fila, 8).ToString());
                        BE_Detalle.n_pordsc = Convert.ToDouble(FgItems.GetData(n_fila, 5).ToString());
                        BE_Detalle.n_porigv = n_TASAIGV;
                        string c_dato = FgItems.GetData(n_fila, 8).ToString();
                        c_dato = funDatos.DataTableBuscar(dtAnex07, "c_codsun", "n_id", c_dato, "C").ToString();
                        BE_Detalle.n_idtipafeigv = Convert.ToInt16(c_dato);
                        BE_Detalle.n_idtipafeisc = 1;
                        BE_Detalle.c_datadi = "";
                        LstDetalleTmp.Add(BE_Detalle);
                    }
                }
                LstDetalle = LstDetalleTmp;
            }

            LstDocNC.Clear();
            if (dtDocNC.Rows.Count != 0) 
            {
                for (n_fila = 0; n_fila <= dtDocNC.Rows.Count - 1; n_fila++)
                {
                    BE_VTA_VENTASNCDOC e_docnc = new BE_VTA_VENTASNCDOC();
                    e_docnc.n_idvta = 0;
                    e_docnc.n_iddoc = Convert.ToInt32(dtDocNC.Rows[n_fila]["n_iddoc"]);
                    e_docnc.n_saldo = Convert.ToDouble(dtDocNC.Rows[n_fila]["n_saldo"]);
                    e_docnc.n_acuenta = Convert.ToDouble(dtDocNC.Rows[n_fila]["n_acuenta"]);
                    e_docnc.n_nuevosaldo = Convert.ToDouble(dtDocNC.Rows[n_fila]["n_nuevosaldo"]);

                    LstDocNC.Add(e_docnc);
                }
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;
            if (TxtFchEmiDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de emision del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchEmiDoc.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboMon.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMon.Focus();
                return booEstado;
            }
            if (Convert.ToDouble(funFunciones.NulosN(LblTC.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de cambio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (Convert.ToInt16(LblIdCliente.Text) == 0)
            {
                MessageBox.Show("¡ No ha especificado el cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CmdBusCli.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboTipDoc.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTipDoc.Focus();
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
                return booEstado;
            }

            if (Convert.ToInt16(CboMot.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la condicion de pago !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMot.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboTipExi.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de existencia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTipExi.Focus();
                return booEstado;
            }

            if (TxtFchVen.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de vencimiento del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchVen.Focus();
                return booEstado;
            }
            if (LblImpTot.Text == "")
            {
                MessageBox.Show("¡ El importe total del documento no puede ser 0 !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (TxtSust.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el motivo de emision del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtSust.Focus();
                return booEstado;
            }
            if (LstDocNC.Count == 0)
            {
                MessageBox.Show("¡ No ha especificado los documentos a los que afectara la nota de credito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CmdAddDoc.Focus();
                return booEstado;
            }
            return booEstado;
        }
        private void FrmManNotaCredito_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (dtRegistros.Rows.Count == 0)
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
            
        }
        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            if (Grabar() == true)
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objRegistros.mysConec = mysConec;
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 14);

                DialogResult Rpta;
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
        private void ToolSalir_Click(object sender, EventArgs e)
        {
            objRegistros = null;
            dtRegistros = null;

            objFormVis = null;

            this.Close();
        }
        private void Tab1_TabIndexChanged(object sender, EventArgs e)
        {

        }
        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (n_QueHace != 3) { return; }

            if (e.NewIndex == 1)
            {
                if (dtRegistros.Rows.Count == 0) { e.Cancel = true; return; }
                int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    VerRegistro(intIdRegistro);
                }
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
                dtResult = funDbGrid.DG_Filtrar(dtRegistros, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
        }
        private void FrmManNotaCredito_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            //Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
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
        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            if (FgItems.Col == 1)
            {
                //DataTable dtResult = new DataTable();
                string c_des = FgItems.GetData(FgItems.Row, 1).ToString();
                string c_tipven = "";

                // MOSTRAMOS EL TIPO DE VENTA
                string c_dato = funDatos.DataTableBuscar(dtAnex07, "n_id", "c_codsun", "1", "C").ToString();
                FgItems.SetData(e.Row, 8, c_dato);

                //c_tipven = funDatos.DataTableBuscar(dtItems, "c_despro", "n_idtipven", c_des, "C").ToString();
                //FgItems.SetData(e.Row, 8, c_tipven);

                // MOSTRAMOS LA UNIDAD DE MEDIDA DEL ITEM
                c_tipven = funDatos.DataTableBuscar(dtItems, "c_despro", "c_abrpre", c_des, "C").ToString();
                FgItems.SetData(e.Row, 2, c_tipven);

                // SELECCIONAMOS LA SIGUIENTE CELDA
                FgItems.Select(e.Row - 1, 2);
                return;
            }
            if (FgItems.Col == 2)
            {
                FgItems.Select(e.Row - 1, 3);
                return;
            }
            if (FgItems.Col == 3)
            {
                double n_cantidad = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(e.Row, 3)));
                FgItems.SetData(e.Row, 3, n_cantidad.ToString("0.00"));
                FgItems.Select(e.Row - 1, 4);
                Calcularfila(e.Row);
                return;
            }
            if (FgItems.Col == 4)
            {
                double n_preuni = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(e.Row, 4)));
                FgItems.SetData(e.Row, 4, n_preuni.ToString("0.000000"));
                FgItems.Select(e.Row - 1, 5);
                Calcularfila(e.Row);
                return;
            }
            if (FgItems.Col == 5)
            {
                double n_dscto = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(e.Row, 5)));
                FgItems.SetData(e.Row, 5, n_dscto.ToString("0.00"));
                FgItems.Select(e.Row, 1);
                Calcularfila(e.Row);
                return;
            }
            if (FgItems.Col == 8)
            {
                booAgregando = true;
                string c_dato = FgItems.GetData(FgItems.Row, 8).ToString();
                c_dato = funDatos.DataTableBuscar(dtAnex07, "c_des", "c_codsun", c_dato, "C").ToString();
                FgItems.SetData(e.Row, 8, c_dato);
                Calcularfila(e.Row);
                booAgregando = false;
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
            int n_IdProducto;
            string strDesTipPro = "";

            // OBTENEMOS LA DESCRIPCION DEL ITEM
            strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 1));

            FgItems.AllowEditing = false;

            if (FgItems.Col == 1)
            {
                // FILTRAMOS LOS ITEMS DEL TIPO DE PRODUCTO SELECCIONADO
                //if (Convert.ToInt16(CboTipExi.SelectedValue) != 0)
                //{
                    FgItems.AllowEditing = true;
                //    dtResul = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + CboTipExi.SelectedValue + "");
                //    funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 1);    // ITEMS
               // }
            }
            if (FgItems.Col == 2)
            {
                if (strDesTipPro != "")
                {
                    dtResul = funDatos.DataTableFiltrar(dtItems, "c_despro = '" + strDesTipPro + "'");
                    if (dtResul.Rows.Count != 0)
                    {
                        n_IdProducto = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());
                        FgItems.AllowEditing = true;
                        dtResul = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_IdProducto + "");
                        funFlex.FlexColumnaCombo(FgItems, dtResul, "c_abrpre", 2);    // ITEMS
                    }
                }
            }
            if (FgItems.Col == 3)
            {
                if (strDesTipPro != "")
                {
                    FgItems.AllowEditing = true;
                }
            }
            if (FgItems.Col == 4)
            {
                if (strDesTipPro != "")
                {
                    FgItems.AllowEditing = true;
                }
            }
            if (FgItems.Col == 5)
            {
                if (strDesTipPro != "")
                {
                    FgItems.AllowEditing = true;
                }
            }
            if (FgItems.Col == 8)
            {
                FgItems.AllowEditing = true;
                funFlex.FlexColumnaCombo(FgItems, dtAnex07, "c_des", 8);
            }
        }
        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 3)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            if (e.Col == 4)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void FgItems_KeyUp(object sender, KeyEventArgs e)
        {
            if (FgItems.Col == 1)
            {
                if (e.KeyCode.ToString() == "Delete")
                {
                    booAgregando = true;
                    FgItems.SetData(FgItems.Row, 1, "");
                    FgItems.SetData(FgItems.Row, 2, "");
                    FgItems.SetData(FgItems.Row, 3, "");
                    FgItems.SetData(FgItems.Row, 4, "");
                    FgItems.SetData(FgItems.Row, 5, "");
                    FgItems.SetData(FgItems.Row, 6, "");
                    FgItems.SetData(FgItems.Row, 7, "");

                    Calcularfila(FgItems.Row);
                    booAgregando = false;
                }
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
        private void TxtNumSer_Validated(object sender, EventArgs e)
        {
            if (Convert.ToInt16(CboTipDoc.SelectedValue) == 0) 
            {
                MessageBox.Show("No ha especificado el tipo de documento", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumSer.Text = "";
                TxtNumDoc.Text = "";
                CboTipDoc.Focus();
                return; 
            }
            if (TxtNumSer.Text != "")
            {
                if (entEmpresa.n_aplife == 1)
                {
                    string strCad = "0000" + TxtNumSer.Text;
                    if (TxtNumSer.Text.Length != 4)
                    {
                        TxtNumSer.Text = "0" + strCad.Substring(strCad.Length - 3, 3);
                    }
                }
                else
                {
                    //if (CboTipDoc.SelectedValue == "4")
                    //{
                    //    TxtNumSer.Text = "B" + strCad.Substring(strCad.Length - 3, 3);
                    //}
                    //else
                    //{ 
                    //    TxtNumSer.Text = "F" + strCad.Substring(strCad.Length - 3, 3);
                    //}
                }
                TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboTipDoc.SelectedValue), TxtNumSer.Text);
            }
            //if (TxtNumSer.Text != "")
            //{
            //    string strCad = "0000" + TxtNumSer.Text;

            //    TxtNumSer.Text = "" + strCad.Substring(strCad.Length - 4, 4);
            //    TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboTipDoc.SelectedValue), TxtNumSer.Text);
            //}
        }
        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            //if (Convert.ToInt16(CboTipDoc.SelectedValue) == 0) { return; }
            //if (TxtNumSer.Text != "")
            //{
            //    string strCad = "0000" + TxtNumSer.Text;

            //    if (entEmpresa.n_aplife == 1)
            //    {
            //        if (TxtNumSer.Text.Length != 4)
            //        {
            //            TxtNumSer.Text = "0" + strCad.Substring(strCad.Length - 3, 3);
            //        }
            //    }
            //    else
            //    {
            //        TxtNumSer.Text = "F" + strCad.Substring(strCad.Length - 3, 3);
            //    }
            //    TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboTipDoc.SelectedValue), TxtNumSer.Text);
            //}
            if (TxtNumDoc.Text != "")
            {
                string strCad = "0000000000" + TxtNumDoc.Text;
                TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
            }
        }
        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();

            if (booAgregando == true) { return; }

            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO
            if (n_TIPODOCUMENTO == 1)
            { 
                dtResul = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc = 8)");
            }
            if (n_TIPODOCUMENTO == 2)
            {
                dtResul = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc = 9)");
            }
            dtRegistros = dtResul;

            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            
            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);

            DgLista.DataSource = dtRegistros;
            if (dtRegistros.Rows.Count == 0)
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
        private void Calcularfila(int n_Fila)
        {
            double n_preneto = 0;
            double n_imptot = 0;
            double n_cantidad = 0;
            double n_preuni = 0;
            double n_dscto = 0;
            double n_valdsc = 0;
            n_cantidad = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_Fila, 3)));
            n_preuni = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_Fila, 4)));
            n_dscto = 0;//Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_Fila, 5)));
            n_valdsc = ((n_preuni / 100) * n_dscto);
            n_preneto = (n_preuni - n_valdsc);
            n_imptot = n_preneto * n_cantidad;

            //FgItems.SetData(n_Fila, 5, n_valdsc.ToString("0.00"));
            FgItems.SetData(n_Fila, 6, n_preneto.ToString("0.000000"));
            FgItems.SetData(n_Fila, 7, n_imptot.ToString("0.00"));
            SumarItems();
        }
        private void SumarItems()
        {
            double n_impsub = 0;
            double n_impbru = 0;
            double n_impina = 0;
            double n_impigv = 0;
            double n_impisc = 0;
            //double n_imptot = 0;
            //double n_pordsc = 0;

            double n_totsubtot = 0;
            double n_totimpbru = 0;
            double n_totimpina = 0;
            double n_totimpigv = 0;
            double n_totimpisc = 0;
            double n_totimptot = 0;

            double n_tasaIGV = 18;
            int n_row = 0;
            int n_idtipven = 0;
            double n_val = 0;

            for (n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
            {
                n_idtipven = Convert.ToInt16(funFunciones.NulosN(FgItems.GetData(n_row, 8)));
                n_impsub = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 7)));

                if (n_idtipven != 21)
                {
                    if ((n_idtipven == 20) || (n_idtipven == 30))
                    {
                        n_impbru = 0;
                        n_impina = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 7)));
                        n_impigv = 0;
                    }
                    else
                    {
                        n_impina = 0;
                        n_impbru = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 7)));
                        n_impigv = ((n_impbru * ((n_tasaIGV / 100) + 1)) - n_impbru);
                    }

                    n_impisc = 0;
                    //n_imptot = (n_impbru * ((n_tasaIGV / 100) + 1));
                    n_totsubtot = n_totsubtot + n_impsub;
                    n_totimpbru = n_totimpbru + n_impbru;
                    n_totimpina = n_totimpina + n_impina;
                    n_totimpigv = n_totimpigv + n_impigv;
                    n_totimpisc = n_totimpisc + n_impisc;
                    n_totimptot = (n_totimpbru + n_totimpina + n_totimpigv);
                }
                //if (n_idtipven == 3)
                //{
                //    n_impbru = 0;
                //    n_impina = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 7)));
                //    n_impigv = 0;
                //}
                //else
                //{
                //    n_impina = 0;
                //    n_impbru = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 7)));
                //    n_impigv = ((n_impbru * ((n_tasaIGV / 100) + 1)) - n_impbru);
                //}

                //n_impisc = 0;
                ////n_imptot = (n_impbru * ((n_tasaIGV / 100) + 1));
                //n_totsubtot = n_totsubtot + n_impsub;
                //n_totimpbru = n_totimpbru + n_impbru;
                //n_totimpina = n_totimpina + n_impina;
                //n_totimpigv = n_totimpigv + n_impigv;
                //n_totimpisc = n_totimpisc + n_impisc;
                //n_totimptot = (n_totimpbru + n_totimpina + n_totimpigv);
            }

            //LblSubTot.Text = n_totimpbru.ToString("0.00");

            //if (Convert.ToDouble(funFunciones.NulosN(TxtPorDes.Text)) != 0)
            //{
            //    n_pordsc = 0; //Convert.ToDouble(TxtPorDes.Text);

            //    // CALCULAMOS EL BRUTO
            //    n_val = n_totimpbru * ((n_pordsc / 100) + 1);
            //    n_val = n_val - n_totimpbru;
            //    LblImpBru.Text = (n_totimpbru - n_val).ToString("0.00");

            //    // CALCULAMOS ELNUEVO INAFECTO
            //    n_val = n_totimpina * ((n_pordsc / 100) + 1);
            //    n_val = n_val - n_totimpina;
            //    LblImpIna.Text = (n_totimpina - n_val).ToString("0.00");

            //    // CALCULAMOS ELNUEVO IGV
            //    n_val = n_totimpigv * ((n_pordsc / 100) + 1);
            //    n_val = n_val - n_totimpigv;
            //    LblImpIgv.Text = (n_totimpigv - n_val).ToString("0.00");
            //}
            //else
            //{
                //TxtPorDes.Text = "0.00";
                LblImpIna.Text = n_totimpina.ToString("0.00");
                LblImpBru.Text = n_totimpbru.ToString("0.00");
                LblImpIgv.Text = n_totimpigv.ToString("0.00");
            //}

            //LblSubTot.Text = n_totsubtot.ToString("0.00");
            LblImpIsc.Text = n_totimpisc.ToString("0.00");

            n_val = Convert.ToDouble(LblImpBru.Text) + Convert.ToDouble(LblImpIna.Text) + Convert.ToDouble(LblImpIgv.Text);
            LblImpTot.Text = n_val.ToString("0.00");   // n_totimptot.ToString("0.00");
        }
        private void CboTipDocMod_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (Convert.ToInt16(LblIdCliente.Text) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (CboTipDocMod.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento que se va a modificar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            DataTable dtResul = new DataTable();
            dtResul = objRegistros.Consulta4(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDocMod.SelectedValue), Convert.ToInt32(LblIdCliente.Text));
            if (objRegistros.booOcurrioError == true)
            {
                return;
            }
            if (dtResul.Rows.Count == 0)
            {
                MessageBox.Show("¡ El cliente no tiene "+ CboTipDocMod.Text + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboNumDocMod.Enabled = false;
                CboNumDocMod.Items.Clear();
                return;
            }
            CboNumDocMod.Enabled = true;
            booAgregando = true;
            funDatos.ComboBoxCargarDataTable(CboNumDocMod, dtResul, "n_id", "c_numdoc");
            booAgregando = false;
        }
        private void TooEnvArc_Click(object sender, EventArgs e)
        {

        }
        private void enviarBajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n_aceptado = Convert.ToInt32(DgLista.Columns["n_aceptado"].CellValue(DgLista.Row).ToString());
            int n_anulado = Convert.ToInt32(DgLista.Columns["n_anulado"].CellValue(DgLista.Row).ToString());
            if (n_aceptado == 0)
            {
                MessageBox.Show("¡ No se puede dar de baja a un documento que no ha sido aceptado " + objRegistros.StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (n_anulado == 1)
            {
                MessageBox.Show("¡ Este documento ya fue dado de baja " + objRegistros.StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            string c_RutaArchPla = entEmpresa.c_vtafearcdat;
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());      // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            string c_numdoc = DgLista.Columns["c_numdoc"].CellValue(DgLista.Row).ToString();                    // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            DialogResult n_rpta = 0;

            objRegistros.mysConec = mysConec;
            objRegistros.STU_SISTEMA = STU_SISTEMA;

            n_rpta = MessageBox.Show("¿ Esta seguro de dar de baja al documento " + c_numdoc + " ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (n_rpta == DialogResult.No) { return; }

            if (objRegistros.GenerarArchivoBaja(STU_SISTEMA.EMPRESAID, n_IdRegistro, c_RutaArchPla) == false)
            {
                MessageBox.Show("¡ No se pudo generar el archivo de baja por el siguiente motivo: " + objRegistros.StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("¡ El archivo de baja para el documento " + c_numdoc + " fue enviado con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        private void recibirConfirmacionBajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string c_RutaSunEnv = entEmpresa.c_vtafearcenv;
            string c_RutaSunRes = entEmpresa.c_vtafearcrec;
            string c_RutaSunDat = entEmpresa.c_vtafearcdat;
            string c_RutaAlmEnv = entEmpresa.c_vtafealmenv;
            string c_RutaAlmRes = entEmpresa.c_vtafealmrpt;

            int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());      // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            string c_numdoc = DgLista.Columns["c_numdoc"].CellValue(DgLista.Row).ToString();                    // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            objRegistros.mysConec = mysConec;
            if (objRegistros.LeerBajas(c_RutaSunRes, c_RutaSunEnv, c_RutaSunDat, c_RutaAlmRes, c_RutaAlmEnv, n_IdRegistro) == true)
            {
                MessageBox.Show("¡ Se de dio de baja con exito al documento Nº " + c_numdoc + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                // ACTUALIZAMOS LA LISTA DE VENTAS
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO
                if (n_TIPODOCUMENTO == 1)
                { 
                    dtRegistros = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc == 8)");
                }
                if (n_TIPODOCUMENTO == 2)
                {
                    dtRegistros = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc == 9)");
                }

                DgLista.DataSource = dtRegistros;
            }
        }
        private void enviarArchivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string c_RutaArchPla = entEmpresa.c_vtafearcdat;
            objRegistros.mysConec = mysConec;
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            string c_verfac = entEmpresa.c_verfe;
            int n_tipdoc = 0;
            if (n_TIPODOCUMENTO == 1) { n_tipdoc = 8; }
            if (n_TIPODOCUMENTO == 2) { n_tipdoc = 9; }

            if (objRegistros.GenerarArchivoPlano(STU_SISTEMA.EMPRESAID, n_tipdoc, c_RutaArchPla, c_verfac) == false)
            {
                MessageBox.Show("¡ No se pudo generar los archivos por el siguiente motivo: " + objRegistros.StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO

                if (n_TIPODOCUMENTO == 1)
                { 
                    dtRegistros = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc = 8)");
                }
                if (n_TIPODOCUMENTO == 2)
                {
                    dtRegistros = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc = 9)");
                }

                DgLista.DataSource = dtRegistros;
                MessageBox.Show("¡ Los archivos fueron enviados cone exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        private void limpiarNoProcesadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (objRegistros.CambiarEstadoEnviado(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
            {
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO
                if (n_TIPODOCUMENTO == 1)
                {
                    dtRegistros = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc = 8)");
                }
                if (n_TIPODOCUMENTO == 2)
                {
                    dtRegistros = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc = 9)");
                }
                
                DgLista.DataSource = dtRegistros;
                MessageBox.Show("¡ Lo documentos no enviados, ya estan listos para ser enviados nuevamente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        private void TooRecArc_Click(object sender, EventArgs e)
        {
            Cls_IO funIo = new Cls_IO();
            string c_RutaArch = entEmpresa.c_vtafearcdat;
            string c_RutaEnv = entEmpresa.c_vtafearcenv;
            string c_RutaRes = entEmpresa.c_vtafearcrec;

            string c_RutaArchResp = entEmpresa.c_vtafealmrpt;
            string c_RutaArchEnv = entEmpresa.c_vtafealmenv;
            string[] c_ListaArchivos;
            int n_numnarc = 0;
            int n_row = 0;
            string c_nomzip = "";

            // CARGAMOS LOS ANTIGUOS ARCHIVOS XML
            c_ListaArchivos = funIo.Dir_LeerDirectorio(c_RutaRes, "*.xml");
            n_numnarc = Convert.ToInt32(c_ListaArchivos.GetLongLength(0));

            if (n_numnarc != 0)
            {
                // ELIMINAMOS LOS ANTIGUOS XML CARGADOS
                for (n_row = 0; n_row <= (n_numnarc - 1); n_row++)
                {
                    if (funIo.Fil_EliminarArchivo(c_ListaArchivos[n_row]) == false)
                    {
                        MessageBox.Show("¡ No se puede eliminar el archivo " + c_ListaArchivos[n_row] + " por el siguiente motivo: " + funIo.c_err_mensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
            }

            // CARGAMOS TODOS LOS ARCHIVOS ZIP DE RESPUESTA
            c_ListaArchivos = funIo.Dir_LeerDirectorio(c_RutaRes, "*.zip");
            if (funIo.n_err_numero != 0)
            {
                MessageBox.Show("¡ No se pudo leer la carpeta " + c_RutaArch + " por el siguiente motivo: " + funIo.c_err_mensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            n_numnarc = Convert.ToInt32(c_ListaArchivos.GetLongLength(0));

            if (n_numnarc != 0)
            {
                // EXTRAEMOS TODOS LOS XML DE LOS ARCHIVOS ZIP
                for (n_row = 0; n_row <= (n_numnarc - 1); n_row++)
                {
                    if (funIo.Zip_Descomprimir(c_ListaArchivos[n_row], c_RutaRes, "xml") == false)
                    {
                        MessageBox.Show("¡ No se pudo desempaquetar el siguiente archivo: " + c_ListaArchivos[n_row].ToString() + " por el siguiente motivo: " + funIo.c_err_mensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
            }

            // CARGAMOS LOS NUEVOS ARCHIVOS XML
            c_ListaArchivos = funIo.Dir_LeerDirectorio(c_RutaRes, "*.xml");
            n_numnarc = Convert.ToInt32(c_ListaArchivos.GetLongLength(0));

            if (n_numnarc != 0)
            {
                // LEEMOS LOS ARCHIVOS CARGADOS
                for (n_row = 0; n_row <= (n_numnarc - 1); n_row++)
                {
                    string[,] arrInf = new string[4, 2];

                    // PREGUNTAMOS SI EL ARCHIVO FUE APROBADO POR LA SUNAT
                    if (objRegistros.EstaAprobado(c_ListaArchivos[n_row], ref arrInf) == true)
                    {
                        // ACTUALIZAMOS EL ESTADO DEL DOCUMENTO A N_ACEPTADO = 2
                        string c_nomarch = Path.GetFileName(c_ListaArchivos[n_row]);
                        string c_tipdoc = c_nomarch.ToString().Substring(14, 2);
                        int n_tipdoc = Convert.ToInt32(funDatos.DataTableBuscar(dtTipDoc, "c_codsun", "n_id", c_tipdoc, "C"));

                        objRegistros.STU_SISTEMA = STU_SISTEMA;
                        if (objRegistros.ActualizarDocumento(c_ListaArchivos[n_row], arrInf, n_tipdoc) == false)
                        {
                            // SI NO SE PUDO ACTUALIZAR ELIMINAMOS EL ARCHIVO
                            funIo.Fil_EliminarArchivo(c_ListaArchivos[n_row]);
                        }
                        else
                        {
                            c_nomzip = Path.GetDirectoryName(c_ListaArchivos[n_row]);
                            c_nomzip = c_nomzip + "\\R" + c_nomarch.ToString().Substring(2, 28) + ".zip";
                            // COPIAMOS LOS ARCHIVOS XML A LA CARPETA DE RESPUESTA DEL SISTEMA
                            if (funIo.Fil_CopiarArchivo(c_nomzip, c_RutaArchResp) == false)
                            {
                                MessageBox.Show("¡ No se puede copiar el siguiente archivo: " + c_ListaArchivos[n_row].ToString() + " por el siguiente motivo: " + funIo.c_err_mensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                return;
                            }
                            else
                            {
                                // SI YA ESTA COPIADO EL ARCHIVO ZIP LO BORRAMOS
                                funIo.Fil_EliminarArchivo(c_nomzip);

                                // BORRAMOS EL ARCHIVO XML EXTRAIDO
                                funIo.Fil_EliminarArchivo(c_ListaArchivos[n_row]);

                                c_nomarch = c_nomarch.ToString().Substring(2, 28) + ".zip";
                                c_nomarch = c_RutaEnv + "\\" + c_nomarch;
                                // COPIAMOS EL ARCHIVO XML ENVIADO A LA SUNAT
                                if (funIo.Fil_CopiarArchivo(c_nomarch, c_RutaArchEnv) == false)
                                {
                                    MessageBox.Show("¡ No se puede copiar el siguiente archivo: " + c_nomarch + " por el siguiente motivo: " + funIo.c_err_mensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                    return;
                                }
                                else
                                {
                                    if (funIo.Fil_EliminarArchivo(c_nomarch) == false)
                                    {
                                        MessageBox.Show("¡ No se puede eliminar el siguiente archivo: " + c_nomarch + " por el siguiente motivo: " + funIo.c_err_mensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                        return;
                                    }
                                    string c_nom = Path.GetFileName(c_nomarch).Substring(0, 28) + ".cab";
                                    funIo.Fil_EliminarArchivo(c_RutaArch + "\\" + c_nom);

                                    c_nom = Path.GetFileName(c_nomarch).Substring(0, 28) + ".not";
                                    funIo.Fil_EliminarArchivo(c_RutaArch + "\\" + c_nom);

                                    c_nom = Path.GetFileName(c_nomarch).Substring(0, 28) + ".det";
                                    funIo.Fil_EliminarArchivo(c_RutaArch + "\\" + c_nom);

                                    c_nom = Path.GetFileName(c_nomarch).Substring(0, 28) + ".ley";
                                    funIo.Fil_EliminarArchivo(c_RutaArch + "\\" + c_nom);

                                    c_nom = Path.GetFileName(c_nomarch).Substring(0, 28) + ".rel";
                                    funIo.Fil_EliminarArchivo(c_RutaArch + "\\" + c_nom);

                                    c_nom = Path.GetFileName(c_nomarch).Substring(0, 28) + ".tri";
                                    funIo.Fil_EliminarArchivo(c_RutaArch + "\\" + c_nom);
                                }
                            }
                        }
                    }
                    else
                    {
                        // SI EL ARCHIVO NO ESTA APROBADO LO ELIMINAMOS
                        if (funIo.Fil_EliminarArchivo(c_ListaArchivos[n_row]) == false)
                        {
                            MessageBox.Show("¡ No se puede eliminar el siguiente archivo: " + c_ListaArchivos[n_row].ToString() + " por el siguiente motivo: " + funIo.c_err_mensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            return;
                        }
                    }
                }
            }

            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO

            if (n_TIPODOCUMENTO == 1)
            {
                dtRegistros = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc = 8)");
            }
            if (n_TIPODOCUMENTO == 2)
            {
                dtRegistros = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc = 9)");
            }

            DgLista.DataSource = dtRegistros;
            MessageBox.Show("¡ Los archivos fueron enviados cone exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        private void FgItems_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }
            if (booAgregando == true) { return; }

            if (FgItems.Col == 1)
            {
                int n_idtipexi = 0;
                DataTable dtResul = new DataTable();
                string c_dato = "";

                booAgregando = true;
                dtResul = dtItems;
                if (Convert.ToInt16(CboTipExi.SelectedValue) != 0)
                {
                    n_idtipexi = Convert.ToInt16(CboTipExi.SelectedValue);
                    dtResul = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + n_idtipexi + "");
                }

                dtResul = objItems.BuscarItem("", "n_id", dtResul, n_idtipexi);
                if (dtResul != null)
                {
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["c_despro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                        FgItems.SetData(FgItems.Row, 1, c_dato);

                        c_dato = dtResul.Rows[0]["c_abrpre"].ToString();        // MOSTRAMOS LA PRESENTACION DEL ITEM
                        FgItems.SetData(FgItems.Row, 2, c_dato);

                        // MOSTRAMOS EL TIPO DE VENTA
                        c_dato = funDatos.DataTableBuscar(dtAnex07, "n_id", "c_codsun", "1", "C").ToString();
                        FgItems.SetData(FgItems.Row, 8, c_dato);
                    }
                }

                booAgregando = false;
            }

            if (FgItems.Col == 8)
            {
                string c_dato = FgItems.GetData(FgItems.Row, 8).ToString();
                c_dato = funDatos.DataTableBuscar(dtAnex07, "c_des", "c_codsun", c_dato, "C").ToString();
                FgItems.SetData(FgItems.Row, 8, c_dato);
            }
        }
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void ToolCorreo_Click(object sender, EventArgs e)
        {
            CN_vta_ventas objAlm = new CN_vta_ventas();
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            int n_aceptado = Convert.ToInt16(DgLista.Columns["n_aceptado"].CellValue(DgLista.Row).ToString());
            int n_anulado = Convert.ToInt16(DgLista.Columns["n_anulado"].CellValue(DgLista.Row).ToString());
            bool b_esfacturaelectronica = false;
            if (n_anulado == 1)
            {
                MessageBox.Show("No se puede enviar x correo un documento anulado", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (n_aceptado == 0)
            {
                MessageBox.Show("No se puede enviar x correo un documento que no ha sido aceptado por la sunat", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            objAlm.STU_SISTEMA = STU_SISTEMA;
            objAlm.mysConec = mysConec;
            if (entEmpresa.n_aplife == 1) { b_esfacturaelectronica = false; }
            if (entEmpresa.n_aplife == 2) { b_esfacturaelectronica = true;}
            if (objAlm.EnviarCorreo(intIdRegistro, b_esfacturaelectronica) == false)
            {
                MessageBox.Show("No se pudo enviar el correo por el siguiente motivo : " + objAlm.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        private void emitirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimiDocumento();
        }
        void ImprimiDocumento()
        {
            CN_vta_ventas objAlm = new CN_vta_ventas();
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[17].CellValue(DgLista.Row).ToString());
            int intIdTipDocumento = Convert.ToInt16(DgLista.Columns[4].CellValue(DgLista.Row).ToString());
            int n_aceptado = Convert.ToInt16(DgLista.Columns[2].CellValue(DgLista.Row).ToString());
            int n_anulado = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());
            bool b_esfacturaelectronica = false;
            if (n_anulado == 1)
            {
                MessageBox.Show("No se puede imprimir un documento anulado", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (entEmpresa.n_aplife == 2)       // PREGUNTAMOS SI LA EMPRESA ESTA AFECTA A FACTURA ELECTRONICA    2 = APLICA FACTURA ELECTRONICA
            {
                b_esfacturaelectronica = true;
                //if (n_aceptado == 0)
                //{
                //    MessageBox.Show("No se puede imprimir un documento que no ha sido aceptado por la sunat", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //    return;
                //}
            }
            objAlm.STU_SISTEMA = STU_SISTEMA;
            objAlm.mysConec = mysConec;
            objAlm.ReportImprimirDocumento(intIdRegistro, intIdTipDocumento, false, "", b_esfacturaelectronica);
        }
        private void eliminarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }
        bool AnularRegistro()
        {
            bool booResult = false;
            if (DgLista.RowCount == 0)
            {
                MessageBox.Show("¡ No hay registros que anular ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }

            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            int n_idpue = Convert.ToInt16(DgLista.Columns["n_idpue"].CellValue(DgLista.Row).ToString());

            if (n_idpue != 0)
            {
                MessageBox.Show("¡ No puede anular este documento, el documento fue generado en otro modulo ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }
            DialogResult Rpta = MessageBox.Show("Esta seguro de anular el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Anular(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se anulo con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 14);
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo anular el registro por el siguiente motivo ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            return booResult;
        }
        private void anularRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnularRegistro();
        }
        private void ToolImprimir_ButtonClick(object sender, EventArgs e)
        {
            ImprimiDocumento();
        }
        private void CboTipDoc_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }
            MostrarNumeroSerie();
            
        }
        void MostrarNumeroSerie()
        {
            if (entEmpresa.n_aplife == 2)
            {
                string c_prefdoc = funDatos.DataTableBuscar(dtTipDoc, "n_id", "c_preser", CboTipDoc.SelectedValue.ToString(), "N").ToString();
                if (c_prefdoc.Length == 1) { TxtNumSer.Text = c_prefdoc.Trim() + "001"; }
                if (c_prefdoc.Length == 2) { TxtNumSer.Text = c_prefdoc.Trim() + "01"; }
            }
            else
            {
                TxtNumSer.Text = "0001";
            }

            TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboTipDoc.SelectedValue), TxtNumSer.Text);
        }
        private void CmdVerAsiento_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objConta = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objConta.mysConec = mysConec;
            objConta.STU_SISTEMA = STU_SISTEMA;
            objConta.VerAsiento(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboMeses.SelectedValue), 14, LblNumAsi.Text);
            objConta = null;
        }
        private void FrmManNotaCredito_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F11")
            {
                ToolEliminar2.Visible = !ToolEliminar2.Visible;
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
            dtresul = funDatos.DataTableFiltrar(dtCliPro, "c_numdoc = '" + TxtNumRuc.Text + "'");

            if (dtresul.Rows.Count != 0)
            {
                TxtNumRuc.Text = dtresul.Rows[0]["c_numdoc"].ToString();
                TxtCliente.Text = dtresul.Rows[0]["c_nombre"].ToString();
                LblIdCliente.Text = dtresul.Rows[0]["n_id"].ToString();
            }
            else
            {
                TxtNumRuc.Text = "";
                TxtCliente.Text = "";
                LblIdCliente.Text = "";
                //TxtNumRuc.Focus();
            }
        }
        private void CmdBusCli_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            objCliPro.mysConec = mysConec;
            dtResult = objCliPro.BuscarCliPro(dtCliPro, 1, "n_id", "");
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtNumRuc.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                    LblIdCliente.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtCliente.Text = dtResult.Rows[0]["c_nombre"].ToString();
                }
                else
                {
                    TxtNumRuc.Text = "";
                    LblIdCliente.Text = "";
                    TxtCliente.Text = "";
                }
            }
        }
        private void CmdAcePan6_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(CboTipDocAnu.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtAnuNumSer.Focus();
                return;
            }
            if (TxtAnuNumSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie del documento ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtAnuNumSer.Focus();
                return;
            }
            if (TxtAnuNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero del documento ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtAnuNumDoc.Focus();
                return;
            }
            if (TxtFchEmiOC.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de emision del documento ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchEmiOC.Focus();
                return;
            }

            bool b_result = false;
            string c_fchreg = "01/" + Convert.ToInt16(CboMeses.SelectedValue).ToString("00") + "/" + STU_SISTEMA.ANOTRABAJO.ToString("0000");
            b_result = objRegistros.InsertarAnulado(STU_SISTEMA.EMPRESAID, 0, Convert.ToInt16(CboTipDocAnu.SelectedValue), TxtFchEmiOC.Text, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, TxtAnuNumSer.Text, TxtAnuNumDoc.Text, c_fchreg, Convert.ToDouble(LblTcAnulado.Text), 14);
            if (b_result == false)
            {
                MessageBox.Show("¡ No se pudo emitir el documento anulado por el siguiente motivo :" + objRegistros.StrErrorMensaje + " ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtAnuNumSer.Focus();
                return;
            }
            else
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objRegistros.mysConec = mysConec;
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 14);

                //DialogResult Rpta;
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();
            }
            CmdSalPan6_Click(sender, e);
        }
        private void CmdSalPan6_Click(object sender, EventArgs e)
        {
            Tab1.SelectedIndex = 0;
            Tab1.Enabled = true;
            ToolHerramientas.Enabled = true;
            panel6.Visible = false;
        }
        private void TxtAnuNumSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000" + TxtAnuNumSer.Text;

                TxtAnuNumSer.Text = strCad.Substring(strCad.Length - 4, 4);
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
        private void TxtAnuNumSer_Validated(object sender, EventArgs e)
        {
            if (TxtAnuNumSer.Text != "")
            {
                string strCad = "0000" + TxtAnuNumSer.Text;

                if (entEmpresa.n_aplife == 2)
                { 
                    TxtAnuNumSer.Text = "F" + strCad.Substring(strCad.Length - 3, 3);
                }
                if (entEmpresa.n_aplife == 1)
                {
                    TxtAnuNumSer.Text = "0" + strCad.Substring(strCad.Length - 3, 3);
                }
                TxtAnuNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboTipDocAnu.SelectedValue), TxtNumSer.Text);
            }
        }
        private void TxtAnuNumDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000000000" + TxtAnuNumDoc.Text;

                TxtAnuNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
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
        private void TxtAnuNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtAnuNumDoc.Text != "")
            {
                string strCad = "0000000000" + TxtAnuNumDoc.Text;
                TxtAnuNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
            }
        }
        private void emitirComoAnuladoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmitirAnulado();
        }
        void EmitirAnulado()
        {
            panel6.Left = ((this.Width - panel6.Width) / 2);
            panel6.Top = ((this.Height - panel6.Height) / 2);
            Tab1.SelectedIndex = 0;
            ToolHerramientas.Enabled = false;
            Tab1.Enabled = false;
            TxtAnuNumSer.Text = "";
            TxtAnuNumDoc.Text = "";
            TxtAnuNumSer.Focus();
            TxtFchEmiOC.Text = "";
            LblTcAnulado.Text = "";
            LblTcAnulado.Text = objFunciones.ObtenerTC(dtTC, TxtFchEmiOC.Text).ToString("0.000");
            if (n_TIPODOCUMENTO == 1)
            {
                CboTipDocAnu.SelectedItem = 0;
            }
            else
            {
                CboTipDocAnu.SelectedItem = 0;
            }
            panel6.Visible = true;
        }
        private void CmdRefDat_Click(object sender, EventArgs e)
        {
            CargarDatosAdicionales();
            MessageBox.Show("¡ Los datos se actualizaron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        private void TxtFchEmiOC_Validated(object sender, EventArgs e)
        {
            if (STU_SISTEMA.MESTRABAJO == 0)
            {
                DataTable dttctem = new DataTable();
                ObjTC.mysConec = mysConec;
                int n_ano = Convert.ToDateTime(TxtFchEmiOC.Text).Year;
                dttctem = ObjTC.Listartcano(151, n_ano.ToString());
                LblTcAnulado.Text = objFunciones.ObtenerTC(dttctem, TxtFchEmiOC.Text).ToString("0.000");
            }
            else
            {
                LblTcAnulado.Text = objFunciones.ObtenerTC(dtTC, TxtFchEmiOC.Text).ToString("0.000");
            }
        }
        private void TxtFchEmiOC_ValueChanged(object sender, EventArgs e)
        {
            if (STU_SISTEMA.MESTRABAJO == 0)
            {
                DataTable dttctem = new DataTable();
                ObjTC.mysConec = mysConec;
                int n_ano = Convert.ToDateTime(TxtFchEmiOC.Text).Year;
                dttctem = ObjTC.Listartcano(151, n_ano.ToString());
                LblTcAnulado.Text = objFunciones.ObtenerTC(dttctem, TxtFchEmiOC.Text).ToString("0.000");
            }
            else
            {
                LblTcAnulado.Text = objFunciones.ObtenerTC(dtTC, TxtFchEmiOC.Text).ToString("0.000");
            }
        }
        private void TxtSust_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtGlosa_KeyPress(object sender, KeyPressEventArgs e)
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
        private void marcarComoProcesadoYAceptadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DgLista.RowCount == 0) { return; }
            DataTable dtResul = new DataTable();
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            objRegistros.mysConec = mysConec;
            objRegistros.ActualizarDocumentosNoGenerados(n_IdRegistro, 2, 2);

            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO

            if (n_TIPODOCUMENTO == 1)
            {
                dtResul = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc = 8)");
            }
            if (n_TIPODOCUMENTO == 2)
            {
                dtResul = funDatos.DataTableFiltrar(dtRegistros, "(n_idtipdoc = 9)");
            }
            //dtResul = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");

            dtRegistros = dtResul;
            DgLista.DataSource = dtRegistros;
            LblNumReg.Text = (dtRegistros.Rows.Count).ToString();
        }
        private void TxtFchEmiDoc_Validated(object sender, EventArgs e)
        {
            if (STU_SISTEMA.MESTRABAJO == 0)
            {
                DataTable dttctem = new DataTable();
                ObjTC.mysConec = mysConec;
                int n_ano = Convert.ToDateTime(TxtFchEmiDoc.Text).Year;
                dttctem = ObjTC.Listartcano(151, n_ano.ToString());
                LblTC.Text = objFunciones.ObtenerTC(dttctem, TxtFchEmiDoc.Text).ToString("0.000");
            }
            else
            {
                LblTC.Text = objFunciones.ObtenerTC(dtTC, TxtFchEmiDoc.Text).ToString("0.000");
            }
        }
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            CN_vta_ventas o_venta = new CN_vta_ventas();
            o_venta.mysConec = mysConec;
            o_venta.ConsultaNotaCredito(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboMeses.SelectedValue));
            o_venta = null;
        }
        private void ChkVarDoc_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkVarDoc.Checked == true)
            {
                TxtSust.Width = 376;
                TxtGlosa.Width = 376;
                CboNumDocMod.Visible = false;
                label8.Visible = false;
                //509, 181
                FgDocCredito.Left  = 509;
                FgDocCredito.Top = 134;
                FgDocCredito.Rows.Count = 1;
                FgDocCredito.AllowEditing = true;
                FgDocCredito.Visible = true;
                FgDocCredito.Rows.Count = FgDocCredito.Rows.Count + 1;
                FgDocCredito.Cols[2].ComboList = "...";
            }
            else
            {
                TxtSust.Width = 753;
                TxtGlosa.Width = 753;
                CboNumDocMod.Visible = true;
                label8.Visible = true;
                FgDocCredito.Visible = false;
            }
        }
        private void FgDocCredito_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }
            if (booAgregando == true) { return; }

            if (FgItems.Col == 1)
            {
                if (LblIdCliente.Text == "") { return; }

                BuscarDocumentos();
                booAgregando = false;
            }
        }
        void BuscarDocumentos()
        {
            DataTable dtResul = new DataTable();
            string c_dato = "";

            dtResul = objRegistros.BuscarDocumentoParaNotaCredito(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblIdCliente.Text), "");

            if (dtResul != null)
            {
                if (dtResul.Rows.Count != 0)
                {
                    FgDocCredito.Rows.Count = FgDocCredito.Rows.Count + 1;
                    c_dato = dtResul.Rows[0]["c_abr"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                    FgDocCredito.SetData(FgDocCredito.Rows.Count-1, 1, c_dato);

                    c_dato = dtResul.Rows[0]["c_numdoc"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                    FgDocCredito.SetData(FgDocCredito.Rows.Count - 1, 2, c_dato);

                    c_dato = dtResul.Rows[0]["n_imptotven"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                    FgDocCredito.SetData(FgDocCredito.Rows.Count - 1, 3, Convert.ToDouble(c_dato).ToString("0.00"));

                    c_dato = dtResul.Rows[0]["n_id"].ToString();        // MOSTRAMOS LA PRESENTACION DEL ITEM
                    FgDocCredito.SetData(FgDocCredito.Rows.Count - 1, 6, c_dato);

                    DataRow row;
                    row = dtDocNC.NewRow();

                    row["c_numdoc"] = FgDocCredito.GetData(FgDocCredito.Rows.Count - 1, 2).ToString();
                    row["n_saldo"] = Convert.ToDouble(FgDocCredito.GetData(FgDocCredito.Rows.Count - 1, 3));
                    row["n_acuenta"] = 0;
                    row["n_nuevosaldo"] = Convert.ToDouble(FgDocCredito.GetData(FgDocCredito.Rows.Count - 1, 3));
                    row["n_iddoc"] = Convert.ToInt32(FgDocCredito.GetData(FgDocCredito.Rows.Count - 1, 6)); 
                    row["c_abr"] = FgDocCredito.GetData(FgDocCredito.Rows.Count - 1, 1).ToString();
                                       
                    dtDocNC.Rows.Add(row);
                }
            }
        }
        private void FgDocCredito_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (booAgregando == true) { return; }
            if (e.Col == 4)
            {
                int n_iddoc = Convert.ToInt32(FgDocCredito.GetData(FgDocCredito.Row, 6));
                double n_saldo = 0;
                double n_imp = Convert.ToDouble(FgDocCredito.GetData(FgDocCredito.Row, 3).ToString());
                double n_debita = Convert.ToDouble(FgDocCredito.GetData(FgDocCredito.Row, 4).ToString());
                FgDocCredito.SetData(FgDocCredito.Row, 5, (n_imp - n_debita).ToString("0.00"));

                int n_row = 0;
                for (n_row = 0; n_row <= dtDocNC.Rows.Count - 1; n_row++)
                {
                    if (Convert.ToInt32(funFunciones.NulosN(dtDocNC.Rows[n_row]["n_iddoc"])) == n_iddoc)
                    {
                        n_saldo = (n_imp - n_debita);
                        dtDocNC.Rows[n_row]["n_acuenta"] = n_debita;
                        dtDocNC.Rows[n_row]["n_nuevosaldo"] = n_saldo;
                    }
                }
            }
        }
        private void FgDocCredito_EnterCell(object sender, EventArgs e)
        {
            FgDocCredito.AllowEditing = false;

            if ((FgDocCredito.Col == 2) || (FgDocCredito.Col == 4))
            {
                FgDocCredito.AllowEditing = true;
            }
        }
        private void CmdDelDoc_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (FgDocCredito.Rows.Count == 1) { return; }
            int n_iddoc = Convert.ToInt32(FgDocCredito.GetData(FgDocCredito.Row, 6));
            FgDocCredito.Rows.Remove(FgDocCredito.Row);

            funDatos.DataTanbleEliminarRegistro(ref dtDocNC, "n_iddoc", n_iddoc.ToString(), "N");
        }
        private void CmdAddDoc_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            BuscarDocumentos();
        }
        private void CboNumDocMod_SelectedValueChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            FgDocCredito.Rows.Count = 1;
            dtDocNC.Clear();

            if (Convert.ToInt32(CboNumDocMod.SelectedValue) != 0)
            {
                string c_dato = "";
                BE_VTA_VENTAS e_venta = new BE_VTA_VENTAS();
                int n_iddoc = Convert.ToInt32(CboNumDocMod.SelectedValue);
                e_venta = objRegistros.TraerRegistro(n_iddoc);

                FgDocCredito.Rows.Count = FgDocCredito.Rows.Count + 1;
                c_dato = funDatos.DataTableBuscar(dtTipDoc, "n_id", "c_abr", e_venta.n_idtipdoc.ToString(),"N").ToString();
                 
                FgDocCredito.SetData(FgDocCredito.Rows.Count - 1, 1, c_dato);

                c_dato = e_venta.c_numser+"-"+e_venta.c_numdoc;        // NUMERO DEL DOCUMENTO
                FgDocCredito.SetData(FgDocCredito.Rows.Count - 1, 2, c_dato);

                c_dato = e_venta.n_imptotven.ToString("0.00");        // IMPORTE DEL DOCUMENTO
                FgDocCredito.SetData(FgDocCredito.Rows.Count - 1, 3, c_dato);

                c_dato = Convert.ToDouble(funFunciones.NulosN(LblImpTot.Text)).ToString("0.00");        // ACUENTA PARA EL DOCUMENTO
                FgDocCredito.SetData(FgDocCredito.Rows.Count - 1, 4, c_dato);

                c_dato = (e_venta.n_imptotven - Convert.ToDouble(funFunciones.NulosN(LblImpTot.Text))).ToString("0.00");        // NUEVO SALDO DEL DOCUMENTO AFECTADO
                FgDocCredito.SetData(FgDocCredito.Rows.Count - 1, 5, c_dato);

                c_dato = e_venta.n_id.ToString();        // MOSTRAMOS LA PRESENTACION DEL ITEM
                FgDocCredito.SetData(FgDocCredito.Rows.Count - 1, 6, c_dato);

                DataRow row;
                row = dtDocNC.NewRow();

                row["c_numdoc"] = e_venta.c_numser + "-" + e_venta.c_numdoc;
                row["n_saldo"] = e_venta.n_imptotven;
                row["n_acuenta"] = Convert.ToDouble(funFunciones.NulosN(LblImpTot.Text));
                row["n_nuevosaldo"] = (e_venta.n_imptotven - Convert.ToDouble(funFunciones.NulosN(LblImpTot.Text)));
                row["n_iddoc"] = e_venta.n_id;
                row["c_abr"] = funDatos.DataTableBuscar(dtTipDoc, "n_id", "c_abr", e_venta.n_idtipdoc.ToString(), "N").ToString();

                dtDocNC.Rows.Add(row);
            }
        }
        private void CboNumDocMod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void CmdVerAfec_Click(object sender, EventArgs e)
        {
            ToolHerramientas.Enabled = false;
            Tab1.Enabled = false;
            panel5.Left = ((this.Width - panel5.Width) / 2);
            panel5.Top = ((this.Height-panel5.Height)/2);
            panel5.Visible = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ToolHerramientas.Enabled = true;
            Tab1.Enabled = true;
            panel5.Visible = false;
        }
    }
}
