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
using SIAC_Negocio.Contabilidad;
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
using System.Configuration;

namespace SSF_NET_Ventas.Formularios
{
    public partial class FrmManVentas : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

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
        CN_sys_empresa objEmp = new CN_sys_empresa();
        CN_sun_fe_catalogo17 objCat17 = new CN_sun_fe_catalogo17();
        CN_sun_fe_catalogo51 objCat51 = new CN_sun_fe_catalogo51();
        CN_sun_fe_catalogo07 objCat07 = new CN_sun_fe_catalogo07();
        Cls_NumeroLetra funLet = new Cls_NumeroLetra();
        CN_vta_tipexivender o_tipexiven = new CN_vta_tipexivender();
        CN_mae_cliproitems o_itecli = new CN_mae_cliproitems();
        CN_con_pcitems o_pcite = new CN_con_pcitems();
        CN_con_pcitems o_pcitem = new CN_con_pcitems();
        const string c_numdecimp = "0.0000";

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
        public List<BE_VTA_VENTASOCT> LstDetOCT = new List<BE_VTA_VENTASOCT>();
        public List<BE_VTA_VENTASDOC> LstDetDoc = new List<BE_VTA_VENTASDOC>();

        BE_SYS_EMPRESA entEmpresa = new BE_SYS_EMPRESA();

        // DATATABLE LOCALES
        DataTable dtRegistros = new DataTable();
        DataTable dtCliPro = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtTipMon = new DataTable();
        DataTable dtTipDoc = new DataTable();
        DataTable dtTipDocAnu = new DataTable();
        DataTable dtTipDocExp = new DataTable();
        DataTable dtConPag = new DataTable();
        DataTable dtVendedor = new DataTable();
        DataTable dtTipoExis = new DataTable();
        DataTable dtPresentaItem = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtMoviDetalle = new DataTable();
        DataTable dtTC = new DataTable();
        DataTable dtAnex07 = new DataTable();
        DataTable dtCat17 = new DataTable();
        DataTable dtCat51 = new DataTable();
        DataTable dtTipDocRef = new DataTable();
        DataTable dtPrecios = new DataTable();
        DataTable dtitecli = new DataTable();
        DataTable dtpcite = new DataTable();
        DataTable dtitemctacon = new DataTable();

        // VARIABLES LOCALES
        int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        int N_TIPEXIVEN = 0;                                                            // ALMACENA EL TIPO DE ITEM QUE VENDERA LA EMPRESA
        double n_TASAIGV = 18;
        string[,] arrCabeceraDg1 = new string[18, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[9, 5];
        string[,] arrCabeceraFlex2 = new string[4, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strNumerovalidos2 = "1234567890" + (char)8;                                        // + (char)8;
        string strNumerovalidos3 = "1234567890EFB" + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^-" + (char)8;
        public FrmManVentas()
        {
            InitializeComponent();
        }

        private void FrmManVentas_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboMon, dtTipMon, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipOpe, dtCat51, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDocRef, dtTipDocRef, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtTipDoc, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDocAnu, dtTipDoc, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDocExp, dtTipDocExp, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboConPag, dtConPag, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipExi, dtTipoExis, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboVend, dtVendedor, "n_id", "c_apenom");       
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            //this.Height = 627;
            //this.Width = 1040;
            
            VerTool();
            //Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            //Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            arrCabeceraFlex1[0, 0] = "Producto";
            arrCabeceraFlex1[0, 1] = "285";
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Unidad Medida";
            arrCabeceraFlex1[1, 1] = "50";
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
            arrCabeceraFlex1[3, 3] = c_numdecimp;
            arrCabeceraFlex1[3, 4] = "c_itepredes";

            arrCabeceraFlex1[4, 0] = "Descuento";
            arrCabeceraFlex1[4, 1] = "73";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = c_numdecimp;
            arrCabeceraFlex1[4, 4] = "n_can";

            arrCabeceraFlex1[5, 0] = "Precio Neto";
            arrCabeceraFlex1[5, 1] = "80";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = c_numdecimp;
            arrCabeceraFlex1[5, 4] = "n_numlot";

            arrCabeceraFlex1[6, 0] = "Importe Total";
            arrCabeceraFlex1[6, 1] = "80";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = c_numdecimp;
            arrCabeceraFlex1[6, 4] = "n_numlot";

            arrCabeceraFlex1[7, 0] = "Tipo Venta";
            arrCabeceraFlex1[7, 1] = "50";
            arrCabeceraFlex1[7, 2] = "C";
            arrCabeceraFlex1[7, 3] = "";
            arrCabeceraFlex1[7, 4] = "n_numlot";

            arrCabeceraFlex1[8, 0] = "DescAdicional";
            arrCabeceraFlex1[8, 1] = "0";
            arrCabeceraFlex1[8, 2] = "C";
            arrCabeceraFlex1[8, 3] = "";
            arrCabeceraFlex1[8, 4] = "n_numlot";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);
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

            funFlex.FlexMostrarDatos(FgDoc, arrCabeceraFlex2, dtMoviDetalle, 2, false);

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
            LblTasIgv.Text = n_TASAIGV.ToString() + " %";
            CboTipExi.SelectedValue = N_TIPEXIVEN;
        }
        void DataTableCargar()
        {
            DataTable dtResul = new DataTable();
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 14);
            dtResul = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
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
            dtTipDocAnu = objTipDoc.Listar();
            dtTipDocExp = objTipDoc.Listar();
            dtTipDoc = funDatos.DataTableFiltrar(dtTipDoc, "n_id IN (2,4,5,11)");
            dtTipDocAnu = funDatos.DataTableFiltrar(dtTipDocAnu, "n_id IN (2,4,5,11)");
            dtTipDocExp = funDatos.DataTableFiltrar(dtTipDocExp, "n_id IN (2,4,5,11)");

            dtTipDocRef = objTipDoc.Listar();
            dtTipDocRef = funDatos.DataTableFiltrar(dtTipDocRef, "n_id IN (77,84)");

            objCondPag.mysConec = mysConec;
            dtConPag = objCondPag.Listar();

            o_tipexiven.mysConec = mysConec;
            string c_cad =  o_tipexiven.CadenaIN(STU_SISTEMA.EMPRESAID);

            objTipoExi.mysConec = mysConec;
            dtTipoExis = objTipoExi.Listar();
            //dtTipoExis = funDatos.DataTableFiltrar(dtTipoExis, "n_id IN (1,2,10, 20)");
            dtTipoExis = funDatos.DataTableFiltrar(dtTipoExis, "n_id IN (" + c_cad +")");

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

            objEmp.mysConec = mysConec;
            objEmp.TraerRegistro(STU_SISTEMA.EMPRESAID);
            entEmpresa = objEmp.e_Empresa;

            //objCat17.mysConec = mysConec;
            //objCat17.Listar();
            //dtCat17 = objCat17.dtLista;

            objCat51.mysConec = mysConec;
            objCat51.Listar();
            dtCat51 = objCat51.dtLista;

            o_pcite.mysConec = mysConec;
            o_pcite.Listar(STU_SISTEMA.EMPRESAID, 2);
            dtpcite = o_pcite.dtLista;

            CN_sys_empresa o_emp = new CN_sys_empresa();
            o_emp.mysConec = mysConec;
            o_emp.TraerRegistro(STU_SISTEMA.EMPRESAID);
            BE_SYS_EMPRESA e_empresa = new BE_SYS_EMPRESA();
            e_empresa = o_emp.e_Empresa;
            N_TIPEXIVEN = e_empresa.n_idtipproven;
            o_emp = null;
            e_empresa = null;

            o_pcitem.mysConec = mysConec;
            o_pcitem.Consulta2(STU_SISTEMA.EMPRESAID);
            dtitemctacon = o_pcitem.dtLista;
        }
        void ListarItems()
        {
            DataTable dtResult = new DataTable();

            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);

            dtResult = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
            LblNumReg.Text = (dtResult.Rows.Count).ToString();

            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtResult, true);

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
        void VerRegistro(int n_IdRegistro)
        {
            booAgregando = true;

            LstDetalle.Clear();
            LstDetDoc.Clear();

            objRegistros.mysConec = mysConec;
            BE_Registro = objRegistros.TraerRegistro(n_IdRegistro);
            LstDetalle = objRegistros.LstDetalle;
            LstDetDoc = objRegistros.LstDocumentos;

            LblNumAsi.Text = BE_Registro.c_numreg;
            CboTipOpe.SelectedValue = BE_Registro.n_idtipope;
            TxtFchEmiDoc.Text = BE_Registro.d_fchdoc.ToString();
            CboMon.SelectedValue = BE_Registro.n_idmon;
            //CboCliente.SelectedValue = BE_Registro.n_idcli;
            TxtNumRuc.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_numdoc", BE_Registro.n_idcli.ToString(), "N").ToString();
            LblidCliente.Text = BE_Registro.n_idcli.ToString();
            TxtCliente.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_nombre", BE_Registro.n_idcli.ToString(), "N").ToString();
            CboTipDoc.SelectedValue = BE_Registro.n_idtipdoc;
            LblTC.Text = BE_Registro.n_tc.ToString("0.000");          
            TxtNumSer.Text = BE_Registro.c_numser;
            TxtNumDoc.Text = BE_Registro.c_numdoc;
            CboConPag.SelectedValue = BE_Registro.n_idconpag;
            CboTipExi.SelectedValue = BE_Registro.n_idtippro;
            CboVend.SelectedValue = BE_Registro.n_idven;
            TxtFchVen.Text = BE_Registro.d_fchven.ToString();
            TxtGlosa.Text = BE_Registro.c_glosa;

            LblSubTot.Text = BE_Registro.n_impsubtot.ToString(c_numdecimp);
            TxtPorDes.Text = BE_Registro.n_pordsc.ToString("0.00");
            LblImpBru.Text = BE_Registro.n_impbru.ToString(c_numdecimp);
            LblImpIna.Text = BE_Registro.n_impinaf.ToString(c_numdecimp);
            LblImpIgv.Text = BE_Registro.n_impigv.ToString(c_numdecimp);
            LblImpIsc.Text = BE_Registro.n_impisc.ToString(c_numdecimp);
            LblImpTot.Text = BE_Registro.n_imptotven.ToString(c_numdecimp);

            CboTipDocRef.SelectedValue = BE_Registro.n_idtipdocref;
            LblIdDocRef.Text = BE_Registro.n_iddocref.ToString();
            //TxtNumDocRef.Text = BE_Registro.c_serdocref + "-" + BE_Registro.c_numdocref;
            TxtNumDocRef.Text = BE_Registro.c_numdocref;

            if (BE_Registro.n_oriitem == 1)
            {
                OptSinGuia.Checked = true;
                OptConGuia.Checked = false;
                OptOrdPed.Checked = false;
                OptOtros.Checked = false;
                CmdDelDoc.Enabled = false;
            }
            
            if (BE_Registro.n_oriitem == 2)
            {
                OptSinGuia.Checked = false;
                OptConGuia.Checked = true;
                OptOrdPed.Checked = false;
                OptOtros.Checked = false;
                CmdDelDoc.Enabled = true;
            }

            if (BE_Registro.n_oriitem == 3)
            {
                OptSinGuia.Checked = false;
                OptConGuia.Checked = false;
                OptOrdPed.Checked = true;
                OptOtros.Checked = false;
            }

            if (BE_Registro.n_oriitem == 4)
            {
                OptSinGuia.Checked = false;
                OptConGuia.Checked = false;
                OptOrdPed.Checked = false;
                OptOtros.Checked = true;
            }
            //LstDetDoc
            //funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);
            booAgregando = false;
            Mostrardetalle();
            MostrarDocumentos();
            //if (BE_Registro.n_oriitem == 2)
            //{
            //    FgItems.Rows.Count = LstDetalle.Count + 2;
            //}
        }
        void MostrarDocumentos()
        {
            int n_row = 0;
            string c_dato = "";
            DataTable dtResult = new DataTable();
            FgDoc.Rows.Count = 2;

            for (n_row = 0; n_row <= LstDetDoc.Count - 1; n_row++)
            {
                if (n_row == 0)
                {
                    c_dato = c_dato + LstDetDoc[n_row].n_iddoc.ToString();
                }
                else
                {
                    c_dato = c_dato + "," + LstDetDoc[n_row].n_iddoc.ToString();
                }
            }

            if (OptConGuia.Checked == true)
            { 
                CN_vta_guias objMov = new CN_vta_guias();
                objMov.mysConec = mysConec;
                objMov.Consulta2(c_dato);
                dtResult = objMov.dtLista;
            }
            if (OptOtros.Checked == true)
            {
                CN_vta_ventas objMov = new CN_vta_ventas();
                objMov.mysConec = mysConec;
                objMov.Consulta6(c_dato);
                dtResult = objMov.dtLista1;
            }

            if (dtResult != null)
            {
                booAgregando = true;
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
                    FgDoc.Rows.Count = FgDoc.Rows.Count + (10 - dtResult.Rows.Count);
                }
                booAgregando = false;
            }
        }
        void Mostrardetalle()
        {
            int n_fila = 2;
            int n_idIte;
            int n_idUniMed;
            DataTable DtFiltro = new DataTable();
            string strCadenaFiltro;
            string c_dato = "";
            double n_precio = 0;
            double n_canpro = 0;
            FgItems.Rows.Count = 2;
            booAgregando = true;
            foreach (BE_VTA_VENTASDET element in LstDetalle)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;

                //n_idTipExi = element.n_iditem;
                n_idIte = element.n_iditem;
                n_idUniMed = element.n_idunimed;

                //if (n_QueHace == 1) { n_precio = Convert.ToDouble(funDatos.DataTableBuscar(dtPrecios, "n_iditem", "n_preuninet", n_idIte.ToString(), "N"));}
                

                // MOSTRAMOS EL ITEM
                strCadenaFiltro = "n_id = " + n_idIte + "";
                DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                FgItems.SetData(n_fila, 1, DtFiltro.Rows[0]["c_despro"].ToString());

                // MOSTRAMOS LA UNIDAD DE MEDIDA
                strCadenaFiltro = "n_id = " + n_idUniMed + "";
                DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                FgItems.SetData(n_fila, 2, DtFiltro.Rows[0]["c_abrpre"].ToString());

                n_canpro = element.n_canpro;
                FgItems.SetData(n_fila, 3, n_canpro.ToString("0.00"));

                if (n_QueHace == 1)
                {
                    //FgItems.SetData(n_fila, 4, n_precio);
                    //FgItems.SetData(n_fila, 6, n_precio);

                    FgItems.SetData(n_fila, 4, element.n_preunibru);
                    FgItems.SetData(n_fila, 6, element.n_preuninet);
                }
                else
                {
                    FgItems.SetData(n_fila, 4, element.n_preunibru);
                    FgItems.SetData(n_fila, 6, element.n_preuninet);
                }
                
                FgItems.SetData(n_fila, 5, element.n_pordsc);
                FgItems.SetData(n_fila, 7, element.n_imptot);

                c_dato = element.n_idtipafeigv.ToString();
                c_dato = funDatos.DataTableBuscar(dtAnex07, "n_id", "c_codsun", c_dato, "C").ToString();
                FgItems.SetData(n_fila, 8, c_dato);

                if (element.n_idtipafeigv != 1)
                {
                    c_dato = funDatos.DataTableBuscar(dtAnex07, "n_id", "c_codsun", element.n_idtipafeigv.ToString(), "C").ToString();
                    FgItems.SetData(n_fila, 8, c_dato);
                }

                c_dato = element.c_datadi;
                FgItems.SetData(n_fila, 9, c_dato);

                Calcularfila(FgItems.Rows.Count-1);
                n_fila++;
            }

            if (LstDetalle.Count == 0)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;
            }
            SumarItems();
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
                        
            //LblTC.Text = objFunciones.ObtenerTC(dtTC, TxtFchEmiDoc.Text).ToString("0.000");
            CboTipOpe.SelectedValue = 1;
            FgDoc.Cols[1].ComboList = "...";
            FgItems.Cols[1].ComboList = "...";
            FgDoc.Rows.Count = 12;
            CboMon.SelectedValue = 115;
            CboTipDoc.SelectedValue = 2;
            CboTipExi.SelectedValue = 2;
            OptSinGuia.Checked = true;
            FgItems.Rows.Count = n_NumFilasDocumento;
            FgDoc.Rows.Count = 2;

            LblTC.Text = objFunciones.ObtenerTC(dtTC, TxtFchEmiDoc.Text).ToString("0.000");
            LstDetalle.Clear();
            LstDetOCT.Clear();
            CboTipExi.SelectedValue = N_TIPEXIVEN;
            if (entEmpresa.n_aplife == 2)
            {
                TxtNumSer.Text = "F001";
            }
            else
            {
                TxtNumSer.Text = "0001";
            }
            TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text);
            TxtNumRuc.Focus();
            booAgregando = false;
        }
        void Blanquea()
        {
            LblNumAsi.Text = "";
            TxtFchEmiDoc.Text = "";
            TxtFchEmiDoc.CustomFormat = "dd/MM/yyyy";
            TxtFchEmiDoc.Format = DateTimePickerFormat.Custom;
            TxtNumRuc.Text = "";
            LblidCliente.Text = "";
            TxtCliente.Text = "";
            CboMon.SelectedValue = 0;
            CboTipDocRef.SelectedValue = 0;
            CboTipDoc.SelectedValue = 0;
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            CboConPag.SelectedValue = 0;
            CboTipExi.SelectedValue = 0;
            CboVend.SelectedValue = 0;
            CboTipOpe.SelectedValue = 0;
            TxtGlosa.Text = "";
            LblSubTot.Text = "";
            TxtPorDes.Text = "";
            LblImpBru.Text = "";
            LblImpIna.Text = "";
            LblImpIgv.Text = "";
            LblImpIsc.Text = "";
            LblImpTot.Text = "";
            LblIdDocRef.Text = "";
            TxtNumDocRef.Text = "";

            OptSinGuia.Checked = true;
            OptConGuia.Checked = false;

            FgItems.Rows.Count = 2;

            LstDetalle.Clear();
            LstDetDoc.Clear();
            LstDetOCT.Clear();
            
            FgDoc.Rows.Count = 2;
        }
        void Bloquea()
        {
            TxtFchEmiDoc.Enabled = !TxtFchEmiDoc.Enabled;
            CboMon.Enabled = !CboMon.Enabled;
            CboTipDocRef.Enabled = !CboTipDocRef.Enabled;
            CboTipDoc.Enabled = !CboTipDoc.Enabled;
            TxtNumRuc.Enabled = !TxtNumRuc.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            //TxtNumDoc.ReadOnly = true;

            CboConPag.Enabled = !CboConPag.Enabled;
            CboTipExi.Enabled = !CboTipExi.Enabled;
            CboVend.Enabled = !CboVend.Enabled;
            TxtFchVen.Enabled = !TxtFchVen.Enabled;           
            TxtGlosa.Enabled = !TxtGlosa.Enabled;
            TxtPorDes.Enabled = !TxtPorDes.Enabled;
            CboTipOpe.Enabled = !CboTipOpe.Enabled;
            //TxtNumDocRef.Enabled = !TxtNumDocRef.Enabled;

            OptSinGuia.Enabled = !OptSinGuia.Enabled;
            OptConGuia.Enabled = !OptConGuia.Enabled;
            OptOrdPed.Enabled = !OptOrdPed.Enabled;
            OptOtros.Enabled = !OptOtros.Enabled;

            CmdBusCli.Enabled = !CmdBusCli.Enabled;
            CboBusDocRef.Enabled = !CboBusDocRef.Enabled;
            CmdDatAdi.Enabled = !CmdDatAdi.Enabled;
        }
        void VerTool()
        {
            if (entEmpresa.n_aplife == 1)
            {
                Toolbaja2.Visible = false;
                TooEnvSun.Visible = false;
                TooRecSun.Visible = false;
                TooCorreo.Visible = false;
            }
            else 
            {
                Toolbaja2.Visible = true;
                TooEnvSun.Visible = true;
                TooRecSun.Visible = true;
                TooCorreo.Visible = true;

                ToolEliminar2.Visible = false;
            }
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
                    Toolbaja2.Visible = false;
                    TooEnvSun.Visible = false;
                    TooRecSun.Visible = false;
                    TooCorreo.Visible = false;
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
                    Toolbaja2.Visible = true;
                    TooEnvSun.Visible = true;
                    TooRecSun.Visible = true;
                    TooCorreo.Visible = true;
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
            ToolImpoApertura.Enabled = !ToolImpoApertura.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar2.Enabled = !ToolEliminar2.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir.Enabled = !ToolImprimir.Enabled;
            //ToolExportar.Enabled = !ToolExportar.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
            Toolbaja2.Enabled = !Toolbaja2.Enabled;
            TooEnvSun.Enabled = !TooEnvSun.Enabled;
            TooRecSun.Enabled = !TooRecSun.Enabled;
            TooCorreo.Enabled = !TooCorreo.Enabled;
            ToolExportar.Enabled = !ToolExportar.Enabled;
            CmdRefDat.Enabled = !CmdRefDat.Enabled;
        }
        void Modificar()
        {
            if (DgLista.RowCount == 0)
            {
                MessageBox.Show("¡ No hay registros que modificar ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            int n_idpue = Convert.ToInt32(DgLista.Columns["n_idpue"].CellValue(DgLista.Row).ToString());
            int n_aceptado = Convert.ToInt32(DgLista.Columns[2].CellValue(DgLista.Row).ToString());
            int n_anulado = Convert.ToInt32(DgLista.Columns[1].CellValue(DgLista.Row).ToString());
            if (n_aceptado == 2)
            {
                MessageBox.Show("¡ No se puede modificar un documento aceptado" + objRegistros.StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (n_anulado == 1)
            {
                MessageBox.Show("¡ No se puede modificar un documento anulado " + objRegistros.StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

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

            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
  
            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            FgDoc.Cols[1].ComboList = "...";
            FgItems.Cols[1].ComboList = "...";
            Tab1.SelectedIndex = 1;

            //n_NumFilasDocumento
            //FgItems.Rows.Count = (n_NumFilasDocumento - FgItems.Rows.Count);
            CmdBusCli.Focus();
            TraePrecioCliente(Convert.ToInt32(LblidCliente.Text));
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

            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            int n_idpue = Convert.ToInt32(DgLista.Columns["n_idpue"].CellValue(DgLista.Row).ToString());
            
            if (n_idpue != 0)
            {
                MessageBox.Show("¡ No puede eliminar este documento, el documento fue generado en otro modulo ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }
            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                objRegistros.LstDocumentos = LstDetDoc;
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

            CmdDelDoc.Enabled = false;
            OptOtrPro.Enabled = false;
            OptDelPro.Enabled = false;
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

            objRegistros.LstDetalle = LstDetalle;    // ASIGNAMOS EL DETALLE DE LA GUIA
            objRegistros.LstDetalleOCT = LstDetOCT;
            objRegistros.LstDocumentos = LstDetDoc;
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
            BE_ListaReg.n_idtippro = Convert.ToInt32(CboTipExi.SelectedValue);
            BE_ListaReg.n_idcli = Convert.ToInt32(LblidCliente.Text);
            BE_ListaReg.n_idpunvencli=0;
            BE_ListaReg.n_idtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);
            BE_ListaReg.c_numser = TxtNumSer.Text;
            BE_ListaReg.c_numdoc = TxtNumDoc.Text;
            if (BE_ListaReg.n_idmes == 0)
            {
                BE_ListaReg.d_fchreg = Convert.ToDateTime("01/01/" + BE_ListaReg.n_anotra.ToString("0000"));
            }
            else
            { 
                BE_ListaReg.d_fchreg = Convert.ToDateTime("01/" + BE_ListaReg.n_idmes.ToString("00") + "/" + BE_ListaReg.n_anotra.ToString("0000"));
            }
            BE_ListaReg.d_fchdoc = Convert.ToDateTime(TxtFchEmiDoc.Text);
            BE_ListaReg.d_fchven = Convert.ToDateTime(TxtFchVen.Text);
            BE_ListaReg.n_idconpag = Convert.ToInt32(CboConPag.SelectedValue);
            BE_ListaReg.n_idmon = Convert.ToInt32(CboMon.SelectedValue);
            BE_ListaReg.n_impbru = Convert.ToDouble(LblImpBru.Text);
            BE_ListaReg.n_impbru2 = 0;
            BE_ListaReg.n_impbru3 = 0;
            BE_ListaReg.n_impinaf = Convert.ToDouble(LblImpIna.Text);
            BE_ListaReg.n_impigv =  Convert.ToDouble(LblImpIgv.Text);
            BE_ListaReg.n_impisc =  Convert.ToDouble(LblImpIsc.Text);
            BE_ListaReg.n_impotr = 0;
            BE_ListaReg.n_imptotven =  Convert.ToDouble(LblImpTot.Text);
            BE_ListaReg.n_tc  =  Convert.ToDouble(LblTC.Text);
            BE_ListaReg.n_impsal  =  Convert.ToDouble(LblImpTot.Text);
            BE_ListaReg.n_idven = Convert.ToInt32(CboVend.SelectedValue);
            BE_ListaReg.n_tasaigv = 18;
            BE_ListaReg.c_glosa = TxtGlosa.Text;
            BE_ListaReg.n_impsubtot = Convert.ToDouble(LblSubTot.Text);
            BE_ListaReg.n_pordsc = Convert.ToDouble(TxtPorDes.Text);
            BE_ListaReg.n_idtipope = Convert.ToInt32(CboTipOpe.SelectedValue);

            if (Convert.ToInt32(CboTipDocRef.SelectedValue) != 0)
            {
                BE_ListaReg.n_idtipdocref = Convert.ToInt32(CboTipDocRef.SelectedValue);
                BE_ListaReg.n_iddocref = Convert.ToInt32(funFunciones.NulosN(LblIdDocRef.Text).ToString());
                //BE_ListaReg.c_serdocref = TxtNumDocRef.Text.ToString().Substring(0,4);
                //BE_ListaReg.c_numdocref = TxtNumDocRef.Text.ToString().Substring(6, (TxtNumDocRef.Text.Length-6));
                BE_ListaReg.c_serdocref = "";
                BE_ListaReg.c_numdocref = TxtNumDocRef.Text;
            }
            string c_mon = "";
            if (Convert.ToDouble(CboMon.SelectedValue) == 115) { c_mon = "soles."; }
            if (Convert.ToDouble(CboMon.SelectedValue) == 151) { c_mon = "dolares americanos."; }
            BE_ListaReg.c_numlet = funLet.Convertir(LblImpTot.Text, true, c_mon);

            if (OptSinGuia.Checked == true)
            {
                BE_ListaReg.n_oriitem = 1;
            }
            if (OptConGuia.Checked == true)
            {
                BE_ListaReg.n_oriitem = 2;
            }
            if (OptOrdPed.Checked == true)
            {
                BE_ListaReg.n_oriitem = 3;
            }
            if (OptOtros.Checked == true)
            {
                BE_ListaReg.n_oriitem = 4;
            }   

            BE_ListaReg.n_anulado = 0;
            BE_ListaReg.c_motnc = "";
            BE_ListaReg.n_idforpag = 0;
            BE_ListaReg.n_idtarcre = 0;

            int n_fila = 0;
            DataTable DtFiltro = new DataTable();
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
                        BE_Detalle.n_idtipven = 0;
                        BE_Detalle.n_pordsc = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_fila, 5)));
                        BE_Detalle.n_porigv = n_TASAIGV;
                        string c_dato = FgItems.GetData(n_fila, 8).ToString();
                        c_dato = funDatos.DataTableBuscar(dtAnex07, "c_codsun", "n_id", c_dato, "C").ToString();
                        BE_Detalle.n_idtipafeigv = Convert.ToInt32(c_dato);
                        BE_Detalle.c_datadi = funFunciones.NulosC(FgItems.GetData(n_fila, 9)).ToString();
                        LstDetalleTmp.Add(BE_Detalle);
                    }
                }
                LstDetalle = LstDetalleTmp;
            }

            // CARGAMOS LA LISTA DE DOCUMENTOS
            if (FgDoc.Rows.Count > 2)
            {
                LstDetDoc.Clear();
                for (n_fila = 2; n_fila <= FgDoc.Rows.Count - 1; n_fila++)
                {
                    if (funFunciones.NulosC(FgDoc.GetData(n_fila, 2)).ToString() != "")
                    { 
                        BE_VTA_VENTASDOC BE_Detalle = new BE_VTA_VENTASDOC();
                        BE_Detalle.n_idvta = 0;
                        BE_Detalle.n_iddoc = Convert.ToInt32(FgDoc.GetData(n_fila, 3).ToString());
                        BE_Detalle.n_idtipdoc = Convert.ToInt32(FgDoc.GetData(n_fila, 4).ToString());
                        LstDetDoc.Add(BE_Detalle);
                    }
                }
            }

            LstDetOCT.Clear();
            BE_VTA_VENTASOCT entOC = new BE_VTA_VENTASOCT();

            ////  1001 - Total valor de venta - operaciones gravadas
            entOC.n_idvta = 0;
            entOC.n_idcon = 1;     
            entOC.n_importe = Convert.ToDouble(LblImpBru.Text);
            LstDetOCT.Add(entOC);

            ////  1003 - Total valor de venta - operaciones exoneradas
            if (Convert.ToDouble(LblImpIna.Text) != 0)
            {
                entOC = new BE_VTA_VENTASOCT();
                entOC.n_idvta = 0;
                entOC.n_idcon = 3;     
                entOC.n_importe = Convert.ToDouble(LblImpIna.Text);
                LstDetOCT.Add(entOC);
            }

            
            ////  1004 -  Total valor de venta – Operaciones gratuitas
            double n_valopegra = 0;
            for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
            {
                if (funFunciones.NulosC(FgItems.GetData(n_fila, 8)) == "21")
                { 
                    n_valopegra = n_valopegra + Convert.ToDouble(FgItems.GetData(n_fila, 6));
                }
            }
            
            if (n_valopegra != 0)
            {
                entOC = new BE_VTA_VENTASOCT();
                entOC.n_idvta = 0;
                entOC.n_idcon = 4;
                entOC.n_importe = n_valopegra;
                LstDetOCT.Add(entOC);
            }


            ////  2005 -  Total descuentos
            if (ObtenerDescuentos() != 0)
            {
                entOC = new BE_VTA_VENTASOCT();
                entOC.n_idvta = 0;
                entOC.n_idcon = 10;
                entOC.n_importe = ObtenerDescuentos();
                LstDetOCT.Add(entOC);
            }
        }
        double ObtenerDescuentos()
        {
            int n_row = 0;
            double n_totnet = 0;
            double n_totbru = 0;
            double n_valnet = 0;
            double n_valbru = 0;
            double n_can = 0;
            double n_valor = 0;
            double n_valor2 = 0;

            // OBTENEMOS EL DESCUENTO DE LOS ITEMS
            for (n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
            {
                if (Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, 5))) != 0)
                { 
                    n_can = Convert.ToDouble(FgItems.GetData(n_row, 3));
                    n_valbru = Convert.ToDouble(FgItems.GetData(n_row, 4));
                    n_valnet = Convert.ToDouble(FgItems.GetData(n_row, 6));
                
                    n_totbru = ((n_can * n_valbru));
                    n_totnet = ((n_can * n_valnet));

                    n_valor = n_valor + (n_totbru - n_totnet);
                }
            }
            
            // OPTENEMOS EL DESCUENTO TOTAL DEL DOCUMENTO
            n_valor2 = (Convert.ToDouble(LblSubTot.Text) - (Convert.ToDouble(LblImpBru.Text) + Convert.ToDouble(LblImpIna.Text)));

            n_valor = n_valor + n_valor2;
            return n_valor;
        }
        bool CamposOK()
        {
            bool booEstado = true;
            if (TxtFchEmiDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de emision del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (Convert.ToInt32(CboMon.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (Convert.ToDouble(funFunciones.NulosN(LblTC.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de cambio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (LblidCliente.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 0)
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
            if (TxtNumSer.Text.Length != 4)
            {
                MessageBox.Show("¡ El numero de serie debe contener solo 4 caracteres !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (n_QueHace == 1)
            {
                if (objRegistros.BuscarDocumento(Convert.ToInt32(LblidCliente.Text), Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text, TxtNumDoc.Text, STU_SISTEMA.EMPRESAID) == true)
                {
                    MessageBox.Show("¡ La " + CboTipDoc.Text.Trim() + " Nº " + TxtNumSer.Text + "-" + TxtNumDoc.Text + " ya existe, ingrese otro !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    TxtNumDoc.Focus();
                    return booEstado;
                }
            }
            else
            {
                BE_VTA_VENTAS e_ventas = new BE_VTA_VENTAS();
                int n_idreg = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
                e_ventas = objRegistros.TraerRegistro(n_idreg);
                
                string c_numdocori = e_ventas.c_numser + "-" + e_ventas.c_numdoc;
                string c_numdocnew = TxtNumSer.Text + "-" + TxtNumDoc.Text;
                if (c_numdocori != c_numdocnew)
                {
                    if (objRegistros.BuscarDocumento(Convert.ToInt32(LblidCliente.Text), Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text, TxtNumDoc.Text, STU_SISTEMA.EMPRESAID) == true)
                    {
                        MessageBox.Show("¡ La " + CboTipDoc.Text.Trim() + " Nº " + TxtNumSer.Text + "-" + TxtNumDoc.Text + " ya existe, ingrese otro !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        booEstado = false;
                        TxtNumDoc.Focus();
                        return booEstado;
                    }
                }
            }

            if (Convert.ToInt32(CboTipDocRef.SelectedValue) != 0)
            {
                if (Convert.ToInt32(funFunciones.NulosN(LblIdDocRef.Text)) != 0)
                {
                    if (TxtNumDocRef.Text == "")
                    {
                        MessageBox.Show("¡ No ha especificado el documento de referencia para esta venta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        booEstado = false;
                        return booEstado;
                    }
                }
                else
                {
                    MessageBox.Show("¡ No ha especificado el documento de referencia para esta venta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    return booEstado;
                }
            }
            if (Convert.ToInt32(CboConPag.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la condicion de pago !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (Convert.ToInt32(CboTipExi.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de existencia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (TxtFchVen.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de vencimiento del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            if (LblImpTot.Text == "")
            {
                MessageBox.Show("¡ El importe total del documento no puede ser 0 !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            int n_row = 0;
            DataTable dtres = new DataTable();
            string c_nomitem = "";
            string strCadenaFiltro = "";

            for (n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
            {
                c_nomitem = funFunciones.NulosC(FgItems.GetData(n_row, 1));
 
                // FILTRAMOS EL ITEM DE LA FILA PARA OBTENER EL ID  
                strCadenaFiltro = "c_despro = '" + c_nomitem + "'";
                dtres = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);

                if (dtres.Rows.Count != 0)
                { 
                    int n_idite = Convert.ToInt32(dtres.Rows[0]["n_id"].ToString());

                    if (n_idite != 0)
                    {
                        dtres = funDatos.DataTableFiltrar(dtpcite, "n_iditem = " + n_idite.ToString() + "");
                        if (dtres.Rows.Count == 0)
                        {
                            MessageBox.Show("¡ EL item " + FgItems.GetData(n_row, 1).ToString() + " no tiene cuenta contable asignada !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            booEstado = false;
                            return booEstado;
                        }
                    }
                }
            }


            return booEstado;
        }
        private void FrmManVentas_Activated(object sender, EventArgs e)
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
                    //else
                    //{
                    //    this.Close();
                    //}
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
                DataTable dtResul = new DataTable();
                objRegistros.mysConec = mysConec;
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 14);
                dtResul = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
                dtRegistros = dtResul;
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
        private void Tab1_SelectedIndexChanging(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;

            if (n_QueHace != 3) { return; }

            if (tc.SelectedIndex == 1)
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns[17].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }
        //private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        //{
        //    if (n_QueHace != 3) { return; }
            
        //    if (e.NewIndex == 1)
        //    {
        //        if (DgLista.RowCount == 0) { e.Cancel = true; return; }

        //        int intIdRegistro = Convert.ToInt32(DgLista.Columns[17].CellValue(DgLista.Row).ToString());

        //        if (n_QueHace != 1)
        //        {
        //            booAgregando = true;
        //            VerRegistro(intIdRegistro);
        //            booAgregando = false;
        //        }
        //    }
        //}
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            if (DgLista.Row == 0) { return; }
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[17].CellValue(DgLista.Row).ToString());
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
        private void FrmManVentas_Resize(object sender, EventArgs e)
        {
            //Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }
        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            //if (FgItems.Col == 1)
            //{
            //    //DataTable dtResult = new DataTable();
            //    string c_des = FgItems.GetData(FgItems.Row, 1).ToString();
            //    string c_tipven = "";
                
            //    // MOSTRAMOS EL TIPO DE VENTA
            //    string c_dato = funDatos.DataTableBuscar(dtAnex07, "n_id", "c_codsun", "1", "C").ToString();
            //    FgItems.SetData(e.Row, 8, c_dato);

            //    //c_tipven = funDatos.DataTableBuscar(dtItems, "c_despro", "n_idtipven", c_des, "C").ToString();
            //    //FgItems.SetData(e.Row, 8, c_tipven);
                
            //    // MOSTRAMOS LA UNIDAD DE MEDIDA DEL ITEM
            //    c_tipven = funDatos.DataTableBuscar(dtItems, "c_despro", "c_abrpre", c_des, "C").ToString();
            //    FgItems.SetData(e.Row, 2, c_tipven);
                
            //    // SELECCIONAMOS LA SIGUIENTE CELDA
            //    FgItems.Select(e.Row - 1, 2);
            //    return;
            //}
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
                booAgregando = true;
                double n_preuni = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(e.Row, 4)));
                FgItems.SetData(e.Row, 4, n_preuni.ToString(c_numdecimp));
                FgItems.Select(e.Row - 1, 5);
                Calcularfila(e.Row);
                booAgregando = false;
                return;
            }
            if (FgItems.Col == 5)
            {
                double n_dscto = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(e.Row, 5)));
                FgItems.SetData(e.Row, 5, n_dscto.ToString(c_numdecimp));
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
            n_dscto = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_Fila, 5)));
            n_valdsc = ((n_preuni / 100) * n_dscto);
            n_preneto = (n_preuni - n_valdsc);
            n_imptot = n_preneto * n_cantidad;

            //FgItems.SetData(n_Fila, 5, n_valdsc.ToString("0.00"));
            FgItems.SetData(n_Fila, 6, n_preneto.ToString(c_numdecimp));
            FgItems.SetData(n_Fila, 7, n_imptot.ToString(c_numdecimp));
            SumarItems();
        }
        private void SumarItems()
        {
            double n_impsub = 0;
            double n_impbru = 0;
            double n_impina = 0;
            double n_impigv = 0;
            double n_impisc = 0;
            double n_imptot = 0;
            double n_pordsc = 0;

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
                n_idtipven = Convert.ToInt32(funFunciones.NulosN(FgItems.GetData(n_row, 8)));
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
            }

            LblSubTot.Text = n_totimpbru.ToString(c_numdecimp);

            if (Convert.ToDouble(funFunciones.NulosN(TxtPorDes.Text)) != 0)
            {
                n_pordsc = Convert.ToDouble(TxtPorDes.Text);
                
                // CALCULAMOS EL BRUTO
                n_val = n_totimpbru * ((n_pordsc / 100) + 1);
                n_val = n_val - n_totimpbru;
                LblImpBru.Text = (n_totimpbru - n_val).ToString(c_numdecimp);

                // CALCULAMOS ELNUEVO INAFECTO
                n_val = n_totimpina * ((n_pordsc / 100) + 1);
                n_val = n_val - n_totimpina;
                LblImpIna.Text = (n_totimpina - n_val).ToString(c_numdecimp);

                // CALCULAMOS ELNUEVO IGV
                n_val = n_totimpigv * ((n_pordsc / 100) + 1);
                n_val = n_val - n_totimpigv;
                LblImpIgv.Text = (n_totimpigv - n_val).ToString(c_numdecimp);
            }
            else
            {
                TxtPorDes.Text = "0.00";
                LblImpIna.Text = n_totimpina.ToString(c_numdecimp);
                LblImpBru.Text = n_totimpbru.ToString(c_numdecimp);
                LblImpIgv.Text = n_totimpigv.ToString(c_numdecimp);
            }

            LblSubTot.Text = n_totsubtot.ToString(c_numdecimp);
            LblImpIsc.Text = n_totimpisc.ToString(c_numdecimp);

            n_val = Convert.ToDouble(LblImpBru.Text) + Convert.ToDouble(LblImpIna.Text) + Convert.ToDouble(LblImpIgv.Text);
            LblImpTot.Text = n_val.ToString(c_numdecimp);   // n_totimptot.ToString("0.00");
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
            //    //    // FILTRAMOS LOS ITEMS DEL TIPO DE PRODUCTO SELECCIONADO
            //    //    if (Convert.ToInt32(CboTipExi.SelectedValue) != 0)
            //    //    {
                       FgItems.AllowEditing = true;
            //    //        dtResul = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + CboTipExi.SelectedValue + "");
            //    //        funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 1);    // ITEMS
            //    //    }
            }
            if (FgItems.Col == 2)
            {
                //FgItems.AllowEditing = true;
                if (strDesTipPro != "")
                {
                    dtResul = funDatos.DataTableFiltrar(dtItems, "c_despro = '" + strDesTipPro + "'");
                    if (dtResul.Rows.Count != 0)
                    {
                        n_IdProducto = Convert.ToInt32(dtResul.Rows[0]["n_id"].ToString());
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
            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 0) 
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
                TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text);
            }
        }
        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text != "")
            {
                string strCad = "0000000000" + TxtNumDoc.Text;
                TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
            }
        }
        private void TxtNumSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                if (entEmpresa.n_aplife == 1)
                {
                    string strCad = "0000" + TxtNumSer.Text;

                    TxtNumSer.Text = strCad.Substring(strCad.Length - 4, 4);
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    SendKeys.Send("{TAB}");
                }

            }
            else
            {
                if (entEmpresa.n_aplife == 1)
                {
                    if (!strNumerovalidos2.Contains(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                }
                else 
                {
                    if (!strNumerovalidos3.Contains(e.KeyChar))
                    {
                        e.Handled = true;
                    }
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
        private void FgItems_KeyPress(object sender, KeyPressEventArgs e)
        {
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
            if (e.KeyCode.ToString() == "Insert")
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;
            }
            if (e.KeyCode.ToString() == "Delete")
            {
                if (FgItems.Rows.Count == 2) { return; }
                FgItems.RemoveItem(FgItems.Row);
            }
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
        void ImprimiDocumento()
        {
            CN_vta_ventas objAlm = new CN_vta_ventas();
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[17].CellValue(DgLista.Row).ToString());
            int intIdTipDocumento = Convert.ToInt32(DgLista.Columns[4].CellValue(DgLista.Row).ToString());
            int n_aceptado = Convert.ToInt32(DgLista.Columns[2].CellValue(DgLista.Row).ToString());
            int n_anulado = Convert.ToInt32(DgLista.Columns[1].CellValue(DgLista.Row).ToString());
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
        private void emitirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimiDocumento();
        }
        private void guiasDelMesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_vta_ventas objAlm = new CN_vta_ventas();
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[17].CellValue(DgLista.Row).ToString());
            objAlm.STU_SISTEMA = STU_SISTEMA;
            objAlm.ReportDocVentasMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
        }
        private void ToolImprimir_ButtonClick(object sender, EventArgs e)
        {
            ImprimiDocumento();
        }
        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            if (booAgregando == true) { return; }

            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtResul = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
            dtRegistros = dtResul;
            LblNumReg.Text = (dtRegistros.Rows.Count).ToString();
            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);

            ToolImpoApertura.Visible = false;
            if (Convert.ToInt32(CboMeses.SelectedValue) == 0) { ToolImpoApertura.Visible = true; }

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
        private void FgItems_Click(object sender, EventArgs e)
        {

        }
        private void TxtPorDes_TextChanged(object sender, EventArgs e)
        {

        }
        private void TxtPorDes_Validated(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (TxtPorDes.Text == "")
            {
                return;
            }
            TxtPorDes.Text = Convert.ToDouble(TxtPorDes.Text).ToString("0.00");
            SumarItems();
        }
        private void TxtPorDes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000" + TxtNumSer.Text;
                TxtPorDes.Text = funFunciones.NulosN(TxtPorDes.Text).ToString();
                TxtPorDes.Text = Convert.ToDouble(TxtPorDes.Text).ToString("0.00");
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
        private void CboConPag_SelectedValueChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            if (Convert.ToInt32(CboConPag.SelectedValue) == 0) { return; }

            int n_numdias = Convert.ToInt32( funDatos.DataTableBuscar(dtConPag, "n_id", "n_numdia", CboConPag.SelectedValue.ToString(), "N").ToString());
            DateTime d_fchven = Convert.ToDateTime(TxtFchEmiDoc.Text);
            d_fchven = d_fchven.AddDays(n_numdias);
            TxtFchVen.Value = d_fchven;
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
                if (Convert.ToInt32(CboTipExi.SelectedValue) != 0)
                {
                    n_idtipexi = Convert.ToInt32(CboTipExi.SelectedValue);
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
                        string c_iditem = dtResul.Rows[0]["n_id"].ToString();
                        c_dato = dtResul.Rows[0]["n_id"].ToString();
                        dtResul = funDatos.DataTableFiltrar(dtitemctacon, "n_idite = " + c_dato + "");
                        if (dtResul.Rows.Count != 0)
                        {
                            c_dato = dtResul.Rows[0]["n_idigvven"].ToString();
                            c_dato = funDatos.DataTableBuscar(dtAnex07, "n_id", "c_codsun", c_dato, "N").ToString();
                        }
                        else
                        {
                            c_dato = "1";
                            c_dato = funDatos.DataTableBuscar(dtAnex07, "n_id", "c_codsun", c_dato, "N").ToString();
                        }
                        FgItems.SetData(FgItems.Row, 8, c_dato);

                        dtResul = funDatos.DataTableFiltrar(dtitecli, "n_idite  = " + c_iditem + "");
                        if (dtResul.Rows.Count != 0)
                        {
                            FgItems.SetData(FgItems.Row, 4, dtResul.Rows[0]["n_prebru"].ToString());
                            FgItems.SetData(FgItems.Row, 5, c_numdecimp);
                            FgItems.SetData(FgItems.Row, 6, dtResul.Rows[0]["n_prebru"].ToString());
                        }
                        else
                        {
                            FgItems.SetData(FgItems.Row, 4, c_numdecimp);
                            FgItems.SetData(FgItems.Row, 5, c_numdecimp);
                            FgItems.SetData(FgItems.Row, 6, c_numdecimp);
                        }
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
        private void TooRecSun_Click(object sender, EventArgs e)
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
                            c_nomzip = c_nomzip+"\\R" + c_nomarch.ToString().Substring(2, 28) + ".zip";
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

            DataTable dtResul = new DataTable();
            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtResul = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
            dtRegistros = dtResul;

            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);

            DgLista.DataSource = dtRegistros;
        }
         private void enviarBajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n_aceptado = Convert.ToInt32(DgLista.Columns[2].CellValue(DgLista.Row).ToString());
            int n_anulado = Convert.ToInt32(DgLista.Columns[1].CellValue(DgLista.Row).ToString());
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
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns[17].CellValue(DgLista.Row).ToString());      // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            string c_numdoc = DgLista.Columns[14].CellValue(DgLista.Row).ToString();                    // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
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
            
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns[17].CellValue(DgLista.Row).ToString());      // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            string c_numdoc = DgLista.Columns[14].CellValue(DgLista.Row).ToString();                    // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            objRegistros.mysConec = mysConec;
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            if (objRegistros.LeerBajas(c_RutaSunRes, c_RutaSunEnv, c_RutaSunDat, c_RutaAlmRes, c_RutaAlmEnv, n_IdRegistro) == true)
            {
                // MessageBox.Show("¡ Se de dio de baja con exito al documento Nº " + c_numdoc + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                // ACTUALIZAMOS LA LISTA DE VENTAS
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO
                dtRegistros = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
                DgLista.DataSource = dtRegistros;
            }
            else
            {
                if (objRegistros.IntErrorNumber != 0)
                { 
                    MessageBox.Show("¡  No se pudo dar de baja a este documento por siguiente motivo :" + objRegistros.StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }
        private void enviarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string c_RutaArchPla = entEmpresa.c_vtafearcdat;
            string c_verfac = entEmpresa.c_verfe;
            objRegistros.mysConec = mysConec;

            objRegistros.STU_SISTEMA = STU_SISTEMA;
            if (objRegistros.GenerarArchivoPlano(STU_SISTEMA.EMPRESAID, 2, c_RutaArchPla, c_verfac) == true)
            {
                if (objRegistros.GenerarArchivoPlano(STU_SISTEMA.EMPRESAID, 4, c_RutaArchPla, c_verfac) == false)
                {
                    MessageBox.Show("¡ No se pudo generar los archivos por el siguiente motivo: " + objRegistros.StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    // ACTUALIZAMOS LA LISTA DE VENTAS
                    dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO
                    dtRegistros = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
                    DgLista.DataSource = dtRegistros;
                    MessageBox.Show("¡ Los archivos fueron enviados con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }
        private void limpiarNoProcesadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (objRegistros.CambiarEstadoEnviado(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
            {
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO
                dtRegistros = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
                DgLista.DataSource = dtRegistros;
                MessageBox.Show("¡ Lo documentos no enviados, ya estan listos para ser enviados nuevamente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        private void TooCorreo_Click(object sender, EventArgs e)
        {
            CN_vta_ventas objAlm = new CN_vta_ventas();
            Int32 intIdRegistro = Convert.ToInt32(DgLista.Columns[17].CellValue(DgLista.Row).ToString());
            int n_aceptado = Convert.ToInt32(DgLista.Columns[2].CellValue(DgLista.Row).ToString());
            int n_anulado = Convert.ToInt32(DgLista.Columns[1].CellValue(DgLista.Row).ToString());
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

            if (entEmpresa.n_aplife == 2) { b_esfacturaelectronica = true; }
            objAlm.STU_SISTEMA = STU_SISTEMA;
            objAlm.mysConec = mysConec;

            this.Cursor = Cursors.WaitCursor;
            if (objAlm.EnviarCorreo(intIdRegistro, b_esfacturaelectronica) == false)
            {
                MessageBox.Show("No se pudo enviar el correo por el siguiente motivo : " + objAlm.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            this.Cursor = Cursors.Default;
            DataTable dtResul = new DataTable();
            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtResul = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
            dtRegistros = dtResul;

            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);

            DgLista.DataSource = dtRegistros;
        }

        private void eliminarDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void anularDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnularRegistro();
        }
        bool AnularRegistro()
        {
            bool booResult = false;

            if (DgLista.RowCount == 0)
            {
                MessageBox.Show("¡ No hay registros que anular ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }

            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            int n_idpue = Convert.ToInt32(DgLista.Columns["n_idpue"].CellValue(DgLista.Row).ToString());

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
        private void CboTipDoc_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }
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

            TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text);
        }
        private void TxtNumSer_TextChanged(object sender, EventArgs e)
        {

        }
        void BuscarPedidos()
        {
            //string c_dato = "";
            //DataTable dtResul = new DataTable();
            //CN_vta_pedidocli objMov = new CN_vta_pedidocli();
            //objMov.mysConec = mysConec;

            //booAgregando = true;
            //c_dato = DocumentosAdicionados();

            //dtResul = objMov.BuscarPedidos(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblidCliente.Text), c_dato);

            string c_dato = "";
            DataTable dtResult = new DataTable();
            CN_vta_pedidocli o_Pedido = new CN_vta_pedidocli();
            o_Pedido.mysConec = mysConec;
            o_Pedido.SeleccionarPedidosCliente(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblidCliente.Text));
            dtResult = o_Pedido.dtLista;

            //if (dtResult != null)
            //{
            //    if (dtResult.Rows.Count != 0)
            //    {
            //        LblIdDocRef.Text = dtResult.Rows[0]["n_id"].ToString();
            //        TxtNumDocRef.Text = dtResult.Rows[0]["c_pednumdoc"].ToString();
            //    }
            //}

            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    c_dato = dtResult.Rows[0]["c_abr"].ToString();           // LMOSTRAMOS LA ABREVIATURA DEL TIPO DE DOCUMENTO
                    FgDoc.SetData(FgDoc.Row, 1, c_dato);

                    c_dato = dtResult.Rows[0]["c_pednumdoc"].ToString();        // MOSTRAMOS EL NUMERO DE DOCUMENTO
                    FgDoc.SetData(FgDoc.Row, 2, c_dato);

                    c_dato = dtResult.Rows[0]["n_id"].ToString();            // MOSTRAMOS EL ID DEL DOCUMENTO
                    FgDoc.SetData(FgDoc.Row, 3, c_dato);

                    c_dato = dtResult.Rows[0]["n_idtipodoc"].ToString();            // MOSTRAMOS EL ID DEL TIPO DE DOCUMENTO
                    FgDoc.SetData(FgDoc.Row, 4, c_dato);

                    AdicionarItemPedido(Convert.ToInt32(dtResult.Rows[0]["n_id"]));
                }
            }
            booAgregando = false;
        }
        void BuscarGuias()
        {
            string c_dato = "";
            DataTable dtResul = new DataTable();
            CN_vta_guias objMov = new CN_vta_guias();
            objMov.mysConec = mysConec;

            booAgregando = true;
            c_dato = DocumentosAdicionados();

            dtResul = objMov.BuscarGuias(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblidCliente.Text), c_dato);

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
            booAgregando = false;
        }
        void BuscarProForma()
        {
            string c_dato = "";
            DataTable dtResul = new DataTable();
            objRegistros.mysConec = mysConec;

            booAgregando = true;
            c_dato = DocumentosAdicionados();

            dtResul = objRegistros.BuscarProforma(STU_SISTEMA.EMPRESAID, c_dato);

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

                    AdicionarItemProforma(Convert.ToInt32(dtResul.Rows[0]["n_id"]));
                }
            }
            booAgregando = false;
        }
        private void FgDoc_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (LblidCliente.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre del cliente del bien o servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdBusCli.Focus();
                return;
            }
            if (FgDoc.Col == 1)
            {
                if (OptConGuia.Checked == true) { BuscarGuias(); }
                if (OptOrdPed.Checked == true) { BuscarPedidos(); }
                if (OptOtros.Checked == true) { BuscarProForma(); }
            }
        }
        void AdicionarItemProforma(int n_IdMovimiento)
        {
            List<BE_VTA_VENTASDET> l_ventadet = new List<BE_VTA_VENTASDET>();
            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdMovimiento);
            l_ventadet = objRegistros.LstDetalle;
            if (l_ventadet.Count != 0)
            {
                AdicionarListaProforma(l_ventadet);
            }
        }
        void AdicionarListaProforma(List<BE_VTA_VENTASDET> lstDetalle)
        {
            int n_row = 0;
            int n_fil = 0;
            DataTable dtres = new DataTable();

            for (n_row = 0; n_row <= lstDetalle.Count - 1; n_row++)
            {
                bool b_SeEncontro = false;
                for (n_fil = 0; n_fil <= LstDetalle.Count - 1; n_fil++)
                {
                    if (LstDetalle[n_fil].n_iditem == lstDetalle[n_row].n_iditem)
                    {
                        LstDetalle[n_fil].n_canpro = (LstDetalle[n_fil].n_canpro + lstDetalle[n_row].n_canpro);
                        b_SeEncontro = true;
                    }
                }
                if (b_SeEncontro == false)
                {
                    dtres = funDatos.DataTableFiltrar(dtitecli, "n_idite  = " + lstDetalle[n_row].n_iditem.ToString() + "");

                    BE_VTA_VENTASDET entVtaDet = new BE_VTA_VENTASDET();
                    entVtaDet.n_idvta = 0;
                    entVtaDet.n_iditem = lstDetalle[n_row].n_iditem;
                    entVtaDet.n_idunimed = lstDetalle[n_row].n_idunimed;
                    entVtaDet.n_canpro = lstDetalle[n_row].n_canpro;

                    entVtaDet.n_preunibru = lstDetalle[n_row].n_preunibru;

                    //if (dtres.Rows.Count != 0)
                    //{
                    //    entVtaDet.n_preunibru = Convert.ToDouble(dtres.Rows[0]["n_prebru"]);
                    //}
                    //else
                    //{
                    //    entVtaDet.n_preunibru = 0;
                    //}
                    entVtaDet.n_pordsc = 0;
                    entVtaDet.n_impdes = 0;
                    entVtaDet.n_imptot = 0;
                    entVtaDet.n_idtipafeigv = 1;

                    LstDetalle.Add(entVtaDet);
                }
            }
            Mostrardetalle();
        }
        void AdicionarItemPedido(int n_IdMovimiento)
        {
            CN_vta_pedidocli o_pedido = new CN_vta_pedidocli();
            BE_VTA_PEDIDOCLI e_pedido = new BE_VTA_PEDIDOCLI();
            List<BE_VTA_PEDIDOCLIDET> l_pedido = new List<BE_VTA_PEDIDOCLIDET>();
            o_pedido.mysConec = mysConec;
            o_pedido.TraerRegistro(n_IdMovimiento);
            l_pedido = o_pedido.lstPedDet;
            if (l_pedido.Count != 0)
            {
                AdicionarLista(l_pedido);
            }
        }
        void AdicionarLista(List<BE_VTA_PEDIDOCLIDET> lstDetalle)
        {
            int n_row = 0;
            int n_fil = 0;
            DataTable dtres = new DataTable();

            for (n_row = 0; n_row <= lstDetalle.Count - 1; n_row++)
            {
                //LstDetalle
                bool b_SeEncontro = false;
                for (n_fil = 0; n_fil <= LstDetalle.Count - 1; n_fil++)
                {
                    if (LstDetalle[n_fil].n_iditem == lstDetalle[n_row].n_idite)
                    {
                        //LstDetalle[n_fil].n_canpro = (LstDetalle[n_fil].n_canpro + lstDetalle[n_row].n_canpro);
                        LstDetalle[n_fil].n_canpro = (LstDetalle[n_fil].n_canpro + lstDetalle[n_row].n_can);
                        b_SeEncontro = true;
                    }
                }
                if (b_SeEncontro == false)
                {
                    dtres = funDatos.DataTableFiltrar(dtitecli, "n_idite  = " + lstDetalle[n_row].n_idite.ToString() + "");

                    BE_VTA_VENTASDET entVtaDet = new BE_VTA_VENTASDET();
                    entVtaDet.n_idvta = 0;
                    entVtaDet.n_iditem = lstDetalle[n_row].n_idite;
                    entVtaDet.n_idunimed = lstDetalle[n_row].n_idunimed;
                    entVtaDet.n_canpro = lstDetalle[n_row].n_can;

                    if (dtres.Rows.Count != 0)
                    {
                        entVtaDet.n_preunibru = Convert.ToDouble(dtres.Rows[0]["n_prebru"]);
                    }
                    else
                    {
                        entVtaDet.n_preunibru = 0;
                    }
                    entVtaDet.n_pordsc = 0;
                    entVtaDet.n_impdes = 0;
                    //entVtaDet.n_preunibru = 0;
                    entVtaDet.n_imptot = 0;
                    entVtaDet.n_idtipafeigv = 1;

                    DataTable dtResul = new DataTable();
                    string c_dato = entVtaDet.n_iditem.ToString();
                    dtResul = funDatos.DataTableFiltrar(dtitemctacon, "n_idite = " + c_dato + "");
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["n_idigvven"].ToString();
                        entVtaDet.n_idtipafeigv = Convert.ToInt32(c_dato);
                        c_dato = funDatos.DataTableBuscar(dtAnex07, "n_id", "c_codsun", c_dato, "N").ToString();
                    }
                    else
                    {
                        c_dato = "1";
                        entVtaDet.n_idtipafeigv = 1;
                        c_dato = funDatos.DataTableBuscar(dtAnex07, "n_id", "c_codsun", c_dato, "N").ToString();
                    }

                    LstDetalle.Add(entVtaDet);
                }
            }
            Mostrardetalle();
        }
        void AdicionarItem(int n_IdMovimiento)
        {
            //CN_alm_movimientos objMov = new CN_alm_movimientos();
            CN_vta_guias objMov = new CN_vta_guias();
            //BE_ALM_MOVIMIENTOS_CONSULTA entMov = new BE_ALM_MOVIMIENTOS_CONSULTA();
            BE_VTA_GUIAS entMov = new BE_VTA_GUIAS();
            List<BE_VTA_GUIASDET> lstDet = new List<BE_VTA_GUIASDET>();
            objMov.mysConec = mysConec;
            entMov = objMov.TraerRegistro(n_IdMovimiento);
            lstDet = objMov.LstDetalle;
            if (lstDet.Count != 0)
            {
                AdicionarLista(lstDet);
            }
        }
        void AdicionarLista(List<BE_VTA_GUIASDET> lstDetalle)
        {
            int n_row = 0;
            int n_fil = 0;
            DataTable dtres = new DataTable();

            for (n_row = 0; n_row <= lstDetalle.Count - 1; n_row++)
            {
                //LstDetalle
                bool b_SeEncontro = false;
                for (n_fil = 0; n_fil <= LstDetalle.Count - 1; n_fil++)
                {
                    if (LstDetalle[n_fil].n_iditem == lstDetalle[n_row].n_idite)
                    {
                        //LstDetalle[n_fil].n_canpro = (LstDetalle[n_fil].n_canpro + lstDetalle[n_row].n_canpro);
                        LstDetalle[n_fil].n_canpro = (LstDetalle[n_fil].n_canpro + lstDetalle[n_row].n_canpro);
                        b_SeEncontro = true;
                    }
                }
                if (b_SeEncontro == false)
                {
                    dtres = funDatos.DataTableFiltrar(dtitecli, "n_idite  = " + lstDetalle[n_row].n_idite.ToString() + "");
                    
                    BE_VTA_VENTASDET entVtaDet = new BE_VTA_VENTASDET();
                    entVtaDet.n_idvta = 0;
                    entVtaDet.n_iditem = lstDetalle[n_row].n_idite;
                    entVtaDet.n_idunimed = lstDetalle[n_row].n_idunimed;
                    entVtaDet.n_canpro = lstDetalle[n_row].n_candev;

                    if (dtres.Rows.Count != 0)
                    {
                        entVtaDet.n_preunibru = Convert.ToDouble(dtres.Rows[0]["n_prebru"]);
                    }
                    else
                    {
                        entVtaDet.n_preunibru = 0;
                    }
                    entVtaDet.n_pordsc = 0;
                    entVtaDet.n_impdes = 0;
                    //entVtaDet.n_preunibru = 0;
                    entVtaDet.n_imptot = 0;
                    entVtaDet.n_idtipafeigv = 1;

                    DataTable dtResul = new DataTable();
                    string c_dato = entVtaDet.n_iditem.ToString();
                    dtResul = funDatos.DataTableFiltrar(dtitemctacon, "n_idite = " + c_dato + "");
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["n_idigvven"].ToString();
                        entVtaDet.n_idtipafeigv = Convert.ToInt32(c_dato);
                        c_dato = funDatos.DataTableBuscar(dtAnex07, "n_id", "c_codsun", c_dato, "N").ToString();
                    }
                    else
                    {
                        c_dato = "1";
                        entVtaDet.n_idtipafeigv = 1;
                        c_dato = funDatos.DataTableBuscar(dtAnex07, "n_id", "c_codsun", c_dato, "N").ToString();
                    }
                    
                    LstDetalle.Add(entVtaDet);
                }
            }
            Mostrardetalle();
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
        private void FgDoc_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { FgDoc.AllowEditing = false; return; }

            FgDoc.AllowEditing = true;
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
                    LblidCliente.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtCliente.Text = dtResult.Rows[0]["c_nombre"].ToString();
                    CboConPag.SelectedValue = Convert.ToInt32(funFunciones.NulosN(dtResult.Rows[0]["n_idcondpag"]));
                    CboVend.SelectedValue = Convert.ToInt32(funFunciones.NulosN(dtResult.Rows[0]["n_idven"]));
                    TraePrecioCliente(Convert.ToInt32(LblidCliente.Text));
                }
                else
                {
                    TxtNumRuc.Text = "";
                    LblidCliente.Text = "";
                    TxtCliente.Text = "";
                    CboConPag.SelectedValue = 0;
                    CboVend.SelectedValue = 0;
                }
            }
        }
        void TraePrecioCliente(int n_IdCliente)
        {
            o_itecli.mysConec = mysConec;
            o_itecli.Listar(n_IdCliente);
            dtitecli = o_itecli.dtListar;
        }
        private void OptSinGuia_CheckedChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (OptSinGuia.Checked == true)
            {
                OptDelPro.Enabled = false;

                OptDelPro.Checked = false;
                OptOtrPro.Checked = false;

                FgDoc.Rows.Count = 2;
                FgItems.Rows.Count = 2;
                FgItems.Rows.Count = n_NumFilasDocumento;

                LstDetalle.Clear();
                LstDetDoc.Clear();
                LstDetOCT.Clear();

                CmdDelDoc.Enabled = false;
                CmdVerDoc.Enabled = false;
            }
        }
        private void OptConGuia_CheckedChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (OptConGuia.Checked == true)
            {
                OptDelPro.Enabled = true;
                
                OptDelPro.Checked = true;
                OptOtrPro.Checked = false;

                FgDoc.Rows.Count = 2;
                FgDoc.Rows.Count = 12;
                FgItems.Rows.Count = 2;
                FgItems.Rows.Count = n_NumFilasDocumento;

                LstDetalle.Clear();
                LstDetDoc.Clear();
                LstDetOCT.Clear();

                CmdDelDoc.Enabled = true;
                CmdVerDoc.Enabled = true;
            }
        }
        private void CmdDelDoc_Click(object sender, EventArgs e)
        {
            if (FgDoc.Rows.Count == 2) { return; }
            if (funFunciones.NulosC(FgDoc.GetData(FgDoc.Row, 2)).ToString() == "")
            {
                MessageBox.Show("¡ No hay documentos para eliminar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (OptConGuia.Checked == true) { QuitarGuias(); }
            if (OptOrdPed.Checked == true) { QuitarPedidos(); }

        }
        void QuitarGuias()
        {
            string c_dato = FgDoc.GetData(FgDoc.Row, 3).ToString();
            FgDoc.RemoveItem(FgDoc.Row);
            FgDoc.Rows.Count = FgDoc.Rows.Count + 1;

            CN_vta_guias objGuia = new CN_vta_guias();
            BE_VTA_GUIAS entMov = new BE_VTA_GUIAS();
            List<BE_VTA_GUIASDET> lstDet = new List<BE_VTA_GUIASDET>();
            objGuia.mysConec = mysConec;
            entMov = objGuia.TraerRegistro(Convert.ToInt32(c_dato));
            lstDet = objGuia.LstDetalle;
            if (lstDet.Count != 0)
            {
                QuitarLista(lstDet);
            }
        }
        void QuitarPedidos()
        {
            string c_dato = FgDoc.GetData(FgDoc.Row, 3).ToString();
            FgDoc.RemoveItem(FgDoc.Row);
            FgDoc.Rows.Count = FgDoc.Rows.Count + 1;

            CN_vta_pedidocli o_pedido = new CN_vta_pedidocli();
            BE_VTA_PEDIDOCLI e_pedido = new BE_VTA_PEDIDOCLI();
            List<BE_VTA_PEDIDOCLIDET> l_pedido = new List<BE_VTA_PEDIDOCLIDET>();
            o_pedido.mysConec = mysConec;
            o_pedido.TraerRegistro(Convert.ToInt32(c_dato));
            l_pedido = o_pedido.lstPedDet;
            if (l_pedido.Count != 0)
            {
                QuitarListaPedido(l_pedido);
            }
        }
        void QuitarListaPedido(List<BE_VTA_PEDIDOCLIDET> lstDet)
        {
            int n_row = 0;
            int n_fil = 0;

            for (n_row = 0; n_row <= lstDet.Count - 1; n_row++)
            {
                for (n_fil = 0; n_fil <= LstDetalle.Count - 1; n_fil++)
                {
                    if (LstDetalle[n_fil].n_iditem == lstDet[n_row].n_idite)
                    {
                        LstDetalle[n_fil].n_canpro = (LstDetalle[n_fil].n_canpro - lstDet[n_row].n_can);
                        if (LstDetalle[n_fil].n_canpro == 0)
                        {
                            LstDetalle.RemoveAt(n_fil);
                        }
                    }
                }
            }
            Mostrardetalle();
        }
        void QuitarLista(List<BE_VTA_GUIASDET> lstDet)
        {
            int n_row = 0;
            int n_fil = 0;

            for (n_row = 0; n_row <= lstDet.Count - 1; n_row++)
            {
                for (n_fil = 0; n_fil <= LstDetalle.Count - 1; n_fil++)
                {
                    if (LstDetalle[n_fil].n_iditem == lstDet[n_row].n_idite)
                    {
                        LstDetalle[n_fil].n_canpro = (LstDetalle[n_fil].n_canpro - lstDet[n_row].n_canpro);
                        if (LstDetalle[n_fil].n_canpro == 0)
                        {
                            LstDetalle.RemoveAt(n_fil);
                        }
                    }
                }
            }
            Mostrardetalle();
        }
        private void CboTipDocRef_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            { 
                CboTipDocRef.SelectedValue = 0;
                TxtNumDocRef.Text = "";
            }
        }
        private void FgDoc_Click(object sender, EventArgs e)
        {

        }
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            if (DgLista.RowCount == 0)
            {
                MessageBox.Show("¡ No hay datos para imprimir !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);                
            }

            string[] columnGridNames = { "c_numreg"
                    , "c_destipdoc"
                    , "c_numdoc"
                    , "d_fchdoc"
                    , "c_nomcli"
                    , "c_desmon"
                    , "n_tc"
                    , "n_impbru"
                    , "n_impinaf"
                    , "n_impigv"
                    , "n_imptotven"
                    , "n_impsal"
                    , "n_enviado"
                    , "n_aceptado"
                    , "n_anulado"};

            string[] columnHeaderNames = { "N° Registro"
                    , "T.D."
                    , "N° Documento"
                    , "Fch. Doc."
                    , "Cliente"
                    , "Mon."
                    , "T.C."
                    , "Imp. Bruto"
                    , "Imp. Inafecto"
                    , "I.G.V."
                    , "Importe Total"
                    , "Saldo"
                    , "Env."
                    , "Ace."
                    , "Anu."};

            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            funDbGrid.DG_ExporExcel(DgLista, saveFileDialog1, columnGridNames, columnHeaderNames);


            //string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-VTA-VENTAS-" + STU_SISTEMA.ANOTRABAJO.ToString() + Convert.ToInt32(CboMeses.SelectedValue).ToString("00") + ".xls";
            //DgLista.ExportTo(c_NomArchivo);
            //MessageBox.Show("! Se exporto con exito la informacion en el archivo " + c_NomArchivo + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        private void CmdGenAsi_Click(object sender, EventArgs e)
        {
            objRegistros.mysConec = mysConec;
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            if (objRegistros.RegeneraAsientos(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 14) == true)
            {
                MessageBox.Show("¡ Los asientos contables se crearon con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                ListarItems();
            }
        }
        private void CmdVerAsiento_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objConta = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objConta.mysConec = mysConec;
            objConta.STU_SISTEMA = STU_SISTEMA;
            objConta.VerAsiento(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue), 14, LblNumAsi.Text);
            objConta = null;
        }
        private void ToolHerramientas_KeyUp(object sender, KeyEventArgs e)
        {

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
                LblidCliente.Text = dtresul.Rows[0]["n_id"].ToString();
                CboConPag.SelectedValue = Convert.ToInt32(funFunciones.NulosN(dtresul.Rows[0]["n_idcondpag"]));
                CboVend.SelectedValue = Convert.ToInt32(funFunciones.NulosN(dtresul.Rows[0]["n_idven"]));
                TraePrecioCliente(Convert.ToInt32(LblidCliente.Text));
            }
            else
            {
                TxtNumRuc.Text = "";
                TxtCliente.Text = "";
                LblidCliente.Text = "";
                CboConPag.SelectedValue = 0;
                CboVend.SelectedValue = 0;
            }
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
            panel6.Visible = true;
        }
        private void emitirAnuladoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmitirAnulado();
        }
        private void CmdSalPan6_Click(object sender, EventArgs e)
        {
            Tab1.SelectedIndex = 0;
            Tab1.Enabled = true;
            ToolHerramientas.Enabled = true;
            panel6.Visible = false;
        }
        private void CmdAcePan6_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(CboTipDocAnu.SelectedValue) == 0)
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
            else
            { 
                string c_mes = TxtFchEmiOC.Text.Substring(3,2);
                if (c_mes != Convert.ToInt32(CboMeses.SelectedValue).ToString("00"))
                { 
                    MessageBox.Show("¡ La fecha de registro del documento no coincide con el periodo, el periodo actua es " + CboMeses.Text  + "! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    TxtFchEmiOC.Focus();
                    return;
                }
            }

            bool b_result = false;
            string c_fchreg = "01/"+ Convert.ToInt32(CboMeses.SelectedValue).ToString("00") +"/" + STU_SISTEMA.ANOTRABAJO.ToString("0000");           
            b_result = objRegistros.InsertarAnulado(STU_SISTEMA.EMPRESAID, 0, Convert.ToInt32(CboTipDocAnu.SelectedValue), TxtFchEmiOC.Text, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, TxtAnuNumSer.Text, TxtAnuNumDoc.Text, c_fchreg, Convert.ToDouble(LblTcAnulado.Text), 14);   
            if (b_result == false)
            {
                MessageBox.Show("¡ No se pudo emitir el documento anulado por el siguiente motivo :" + objRegistros.StrErrorMensaje + " ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtAnuNumSer.Focus();
                return;
            }
            else
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                DataTable dtResul = new DataTable();
                objRegistros.mysConec = mysConec;
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 14);
                dtResul = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
                dtRegistros = dtResul;
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();
            }
            CmdSalPan6_Click(sender, e);
        }
        private void TxtAnuNumSer_Validated(object sender, EventArgs e)
        {
            if (TxtAnuNumSer.Text != "")
            {
                string strCad = "0000" + TxtAnuNumSer.Text;

                if (entEmpresa.n_aplife == 1)
                {
                    TxtAnuNumSer.Text = "0" + strCad.Substring(strCad.Length - 3, 3);
                }
                else 
                { 
                    TxtAnuNumSer.Text = "F" + strCad.Substring(strCad.Length - 3, 3);
                }
                TxtAnuNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDocAnu.SelectedValue), TxtNumSer.Text);
            }
        }
        private void TxtAnuNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text != "")
            {
                string strCad = "0000000000" + TxtAnuNumDoc.Text;
                TxtAnuNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
            }
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

        private void CboBusDocRef_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            CN_vta_pedidocli o_Pedido = new CN_vta_pedidocli();
            o_Pedido.mysConec = mysConec;
            o_Pedido.SeleccionarPedidosCliente(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblidCliente.Text));
            dtResult = o_Pedido.dtLista;

            if (dtResult != null)
            { 
            if (dtResult.Rows.Count != 0)
                {
                    LblIdDocRef.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtNumDocRef.Text = dtResult.Rows[0]["c_pednumdoc"].ToString();
                }
            }
        }

        private void FrmManVentas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F11")
            {
                ToolEliminar2.Visible = !ToolEliminar2.Visible;
            }
        }

        private void CmdRefDat_Click(object sender, EventArgs e)
        {
            CargarDatosAdicionales();
            MessageBox.Show("¡ Los datos se actualizaron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void CmdCanPanAdi_Click(object sender, EventArgs e)
        {
            Tab1.SelectedIndex = 0;
            Tab1.Enabled = true;
            ToolHerramientas.Enabled = true;
            PanDatAdi.Visible = false;
        }

        private void CmdAceDatAdi_Click(object sender, EventArgs e)
        {
            //if (TxtDatAdi.Text != "")
            //{
                FgItems.SetData(FgItems.Row, 9, TxtDatAdi.Text);
            //}
            CmdCanPanAdi_Click(sender, e);
        }

        private void CmdDatAdi_Click(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(FgItems.GetData(FgItems.Row, 1)).ToString() == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre del item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            PanDatAdi.Left = ((this.Width - PanDatAdi.Width) / 2);
            PanDatAdi.Top = ((this.Height - PanDatAdi.Height) / 2);
            Tab1.SelectedIndex = 0;
            ToolHerramientas.Enabled = false;
            Tab1.Enabled = false;

            string c_dat = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 9)).ToString();
            
            TxtDatAdi.Text = c_dat;
            TxtDatAdi.Focus();
            PanDatAdi.Visible = true;
        }

        private void marcarComoProcesadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DgLista.RowCount == 0) { return; }
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            DataTable dtResul = new DataTable();
            objRegistros.mysConec = mysConec;
            objRegistros.ActualizarDocumentosNoGenerados(n_IdRegistro, 2);

            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtResul = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
            dtRegistros = dtResul;
            DgLista.DataSource = dtRegistros;
            LblNumReg.Text = (dtRegistros.Rows.Count).ToString();
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

        private void TxtAnuNumDoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtAnuNumDoc_MouseLeave(object sender, EventArgs e)
        {
            if (TxtAnuNumDoc.Text != "")
            {
                string strCad = "0000000000" + TxtAnuNumDoc.Text;
                TxtAnuNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
            }
        }

        private void TxtAnuNumSer_MouseLeave(object sender, EventArgs e)
        {
            if (TxtAnuNumSer.Text != "")
            {
                string strCad = "0000" + TxtAnuNumSer.Text;
                TxtAnuNumSer.Text = strCad.Substring(strCad.Length - 4, 4);
            }
        }

        private void marcarComoProcesadoYAceptadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DgLista.RowCount == 0) { return; }
            DataTable dtResul = new DataTable();
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            objRegistros.mysConec = mysConec;
            objRegistros.ActualizarDocumentosNoGenerados(n_IdRegistro, 2,2);
            
            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtResul = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
            dtRegistros = dtResul;
            DgLista.DataSource = dtRegistros;
            LblNumReg.Text = (dtRegistros.Rows.Count).ToString();
        }

        private void TxtNumRuc_TextChanged(object sender, EventArgs e)
        {

        }

        private void OptOtros_CheckedChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (OptOtros.Checked == true)
            {
                OptDelPro.Enabled = true;

                OptDelPro.Checked = true;
                OptOtrPro.Checked = false;

                FgDoc.Rows.Count = 2;
                FgDoc.Rows.Count = 12;
                FgItems.Rows.Count = 2;
                FgItems.Rows.Count = n_NumFilasDocumento;

                LstDetalle.Clear();
                LstDetDoc.Clear();
                LstDetOCT.Clear();

                CmdDelDoc.Enabled = true;
                CmdVerDoc.Enabled = true;
            }
        }

        private void ToolImpoApertura_Click(object sender, EventArgs e)
        {
            DialogResult Rpta = MessageBox.Show("Se importaran las compras pendientes del periodo anterior ¿ Desea importar los documentos pendiente de pago ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                objRegistros.mysConec = mysConec;
                if (objRegistros.ImportarApertura(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO - 1, 14) == true)
                {
                    DataTable dtResul = new DataTable();
                    dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO
                    dtResul = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
                    dtRegistros = dtResul;
                    DgLista.DataSource = dtRegistros;
                    MessageBox.Show("! Los datos se importaron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show("! No se pudieron importar los datos por el siguiente motivo: " + objRegistros.StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }

            }
        }

        private void OptOrdPed_CheckedChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (OptOrdPed.Checked == true)
            {
                OptDelPro.Enabled = true;

                OptDelPro.Checked = true;
                OptOtrPro.Checked = false;

                FgDoc.Rows.Count = 2;
                FgDoc.Rows.Count = 12;
                FgItems.Rows.Count = 2;
                FgItems.Rows.Count = n_NumFilasDocumento;

                LstDetalle.Clear();
                LstDetDoc.Clear();
                LstDetOCT.Clear();

                CmdDelDoc.Enabled = true;
                CmdVerDoc.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tab1.SelectedIndex = 0;
            Tab1.Enabled = true;
            ToolHerramientas.Enabled = true;
            panel5.Visible = false;
        }

        private void seleccionarEnviosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel5.Left = ((this.Width - panel5.Width) / 2);
            panel5.Top = ((this.Height - panel5.Height) / 2);
            Tab1.SelectedIndex = 0;
            ToolHerramientas.Enabled = false;
            Tab1.Enabled = false;
            TxtAnuNumSer.Text = "";
            TxtAnuNumDoc.Text = "";
            TxtAnuNumSer.Focus();
            TxtFchGenDoc.Text = "";
            //LblTcAnulado.Text = "";
            //LblTcAnulado.Text = objFunciones.ObtenerTC(dtTC, TxtFchEmiOC.Text).ToString("0.000");
            CboTipDocExp.SelectedValue = 2;
            panel5.Visible = true;
        }

        private void CmdExpArch_Click(object sender, EventArgs e)
        {
            string c_RutaArchPla = entEmpresa.c_vtafearcdat;
            string c_verfac = entEmpresa.c_verfe;
            objRegistros.mysConec = mysConec;

            objRegistros.STU_SISTEMA = STU_SISTEMA;
            if (objRegistros.GenerarArchivoPlanoPorDias(STU_SISTEMA.EMPRESAID, 2, TxtFchGenDoc.Text, c_RutaArchPla, c_verfac) == false)
            {
                MessageBox.Show("¡ No se pudo generar los archivos por el siguiente motivo: " + objRegistros.StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                // ACTUALIZAMOS LA LISTA DE VENTAS
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 14);    // CARGAMOS LOS DATOS DEL FORMULARIO
                dtRegistros = funDatos.DataTableFiltrar(dtRegistros, "((n_idtipdoc <> 8) AND (n_idtipdoc <> 9))");
                DgLista.DataSource = dtRegistros;
                MessageBox.Show("¡ Los archivos fueron enviados con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                button1_Click(sender, e);
            }
        }

        private void ponerEnObservacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Observar();
        }
        void Observar()
        {
            if (DgLista.RowCount == 0)
            {
                MessageBox.Show("¡ No hay registros que observar ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            string c_numser = "";
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            string c_numdoc = "";
            Genericas funGen = new Genericas();

            string c_nomarc2 = ConfigurationManager.AppSettings["PathIniFile"];
            //N_IDALMACEN = Convert.ToInt32(funDatos.IniLeerSeccion(c_nomarc, "SISTEMA", "ALMACEN").ToString());
            //c_NUMSERFAC = funGen.IniLeerSeccion(c_nomarc2, "SISTEMA", "SERFAC").ToString();
            //c_NUMSERBOL = funGen.IniLeerSeccion(c_nomarc2, "SISTEMA", "SERBOL").ToString();
            c_numser = funGen.IniLeerSeccion(c_nomarc2, "SISTEMA", "SERVAL").ToString();

            objRegistros.CambiarDocVentaAProforma(n_IdRegistro, STU_SISTEMA.EMPRESAID, 90, c_numser, ref c_numdoc);
            if (objRegistros.booOcurrioError == true)
            {
                MessageBox.Show("¡ No se pudo observar la venta por el siguiente motivo: " + objRegistros.StrErrorMensaje + " ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("¡ el documento se observo con exito, revisar el documento Nº " + c_numdoc + " ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();
            }
            return;

        }

        private void activarDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            objRegistros.ActivarDocumento(n_IdRegistro);
            if (objRegistros.booOcurrioError == false)
            {
                MessageBox.Show("¡ El registro se activo con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objRegistros.mysConec = mysConec;
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 14);
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();
            }
        }
    }
}
