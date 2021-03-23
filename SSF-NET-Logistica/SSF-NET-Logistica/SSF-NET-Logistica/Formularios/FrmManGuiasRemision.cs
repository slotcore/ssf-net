using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Logistica;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Logistica;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Ventas;
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
    public partial class FrmManGuiasRemision : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_vta_guias objRegistros = new CN_vta_guias();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_clipro objCliPro = new CN_mae_clipro();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_sun_motivotraslado objMotTra = new CN_sun_motivotraslado();
        CN_vta_punvencli objPunVen = new CN_vta_punvencli();
        CN_vta_emptra objTrans = new CN_vta_emptra();
        CN_vta_chofer objChof = new CN_vta_chofer();
        CN_vta_vehiculo objVehi = new CN_vta_vehiculo();
        CN_alm_inventariounimed ObjAlmUniMed = new CN_alm_inventariounimed();
        CN_sun_tipexi objTipoExi = new CN_sun_tipexi();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_log_ordenrequerimiento objOrdReq = new CN_log_ordenrequerimiento();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        
        
        // ENTIDADES LOCALES
        BE_VTA_GUIAS BE_ListaReg = new BE_VTA_GUIAS();
        BE_VTA_GUIAS BE_Registro = new BE_VTA_GUIAS();
        BE_VTA_GUIASDET BE_GuiaDetalle = new BE_VTA_GUIASDET();
        BE_VTA_GUIASDATOS e_GuiaDatos = new BE_VTA_GUIASDATOS();

        List<BE_VTA_GUIASDET> LstDetalle = new List<BE_VTA_GUIASDET>();   // CREAMOS UNA LISTA DEL TIPO BE_VTA_GUIASDET PARA ALMACENAR LOS ITEMS
        List<BE_VTA_GUIASDOC> LstGuiaDoc = new List<BE_VTA_GUIASDOC>();
        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO

        DataTable dtCliPro = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtMotTra = new DataTable();
        DataTable dtPunVen = new DataTable();
        DataTable dtPunPar = new DataTable();
        DataTable dtTrans = new DataTable();
        DataTable dtChof = new DataTable();
        DataTable dtVehi = new DataTable();
        DataTable dtMoviDetalle = new DataTable();
        DataTable dtTipoExis = new DataTable();
        DataTable dtPresentaItem = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtTipoDocumento = new DataTable();
        DataTable dtOrdReq = new DataTable();
        
        // VARIABLES LOCALES
        int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[9, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[6, 5];
        string[,] arrCabeceraFlex2 = new string[5, 5];
        string[,] arrCabeceraFlex3 = new string[2, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManGuiasRemision()
        {
            InitializeComponent();
        }

        private void FrmManGuiasRemision_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            //funDatos.ComboBoxCargarDataTable(CboCliente, dtCliPro, "n_id", "c_nombre");
            funDatos.ComboBoxCargarDataTable(CboMotTras, dtMotTra, "n_id", "c_des");

            funDatos.ComboBoxCargarDataTable(CboPunVen, dtPunVen, "n_id", "c_des");           
            funDatos.ComboBoxCargarDataTable(CboPunPar, dtPunPar, "n_id", "c_des");

            funDatos.ComboBoxCargarDataTable(CboTra, dtTrans, "n_id", "c_nombre");
            funDatos.ComboBoxCargarDataTable(CboCho, dtChof, "n_id", "c_nomcho");
            funDatos.ComboBoxCargarDataTable(CboVeh, dtVehi, "n_id", "c_marca");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            arrCabeceraFlex1[0, 0] = "Tipo Item";
            arrCabeceraFlex1[0, 1] = "150";
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Item";
            arrCabeceraFlex1[1, 1] = "350";
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
            arrCabeceraFlex1[4, 1] = "100";
            arrCabeceraFlex1[4, 2] = "C";
            arrCabeceraFlex1[4, 3] = "";
            arrCabeceraFlex1[4, 4] = "n_numlot";

            arrCabeceraFlex1[5, 0] = "N_IdDocumento";
            arrCabeceraFlex1[5, 1] = "0";
            arrCabeceraFlex1[5, 2] = "C";
            arrCabeceraFlex1[5, 3] = "";
            arrCabeceraFlex1[5, 4] = "n_numlot";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);

            arrCabeceraFlex2[0, 0] = "Tip. Doc.";
            arrCabeceraFlex2[0, 1] = "30";
            arrCabeceraFlex2[0, 2] = "C";
            arrCabeceraFlex2[0, 3] = "";
            arrCabeceraFlex2[0, 4] = "c_tipexides";

            arrCabeceraFlex2[1, 0] = "Nº Doc.";
            arrCabeceraFlex2[1, 1] = "100";
            arrCabeceraFlex2[1, 2] = "S";
            arrCabeceraFlex2[1, 3] = "";
            arrCabeceraFlex2[1, 4] = "c_itedes";

            arrCabeceraFlex2[2, 0] = "Fch. Ped.";
            arrCabeceraFlex2[2, 1] = "70";
            arrCabeceraFlex2[2, 2] = "F";
            arrCabeceraFlex2[2, 3] = "";
            arrCabeceraFlex2[2, 4] = "c_itepredes";

            arrCabeceraFlex2[3, 0] = "ID";
            arrCabeceraFlex2[3, 1] = "0";
            arrCabeceraFlex2[3, 2] = "N";
            arrCabeceraFlex2[3, 3] = "0.000000";
            arrCabeceraFlex2[3, 4] = "n_can";

            arrCabeceraFlex2[4, 0] = "IDTIPDOC";
            arrCabeceraFlex2[4, 1] = "0";
            arrCabeceraFlex2[4, 2] = "N";
            arrCabeceraFlex2[4, 3] = "0.000000";
            arrCabeceraFlex2[4, 4] = "n_can";

            funFlex.FlexMostrarDatos(FgDoc, arrCabeceraFlex2, dtMoviDetalle, 2, false);


            arrCabeceraFlex3[0, 0] = "Nº Lote";
            arrCabeceraFlex3[0, 1] = "120";
            arrCabeceraFlex3[0, 2] = "C";
            arrCabeceraFlex3[0, 3] = "";
            arrCabeceraFlex3[0, 4] = "c_tipexides";

            arrCabeceraFlex3[1, 0] = "Cantidad";
            arrCabeceraFlex3[1, 1] = "80";
            arrCabeceraFlex3[1, 2] = "N";
            arrCabeceraFlex3[1, 3] = "";
            arrCabeceraFlex3[1, 4] = "c_itedes";

            funFlex.FlexMostrarDatos(FgLotes, arrCabeceraFlex3, dtMoviDetalle, 2, false);


            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 2);

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(54, ref arrCabeceraDg1);

            objCliPro.mysConec = mysConec;
            dtCliPro = objCliPro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(54);

            objItems.mysConec = mysConec;
            dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);                                        // CARGAMOS TODOS LOS ITEMS

            objMotTra.mysConec = mysConec;
            dtMotTra = objMotTra.Listar();

            objPunVen.mysConec = mysConec;
            dtPunVen = objPunVen.Listar();
            dtPunPar = objPunVen.Listar();

            objTrans.mysConec = mysConec;
            dtTrans = objTrans.Listar(STU_SISTEMA.EMPRESAID);

            objChof.mysConec = mysConec;
            dtChof = objChof.Listar(STU_SISTEMA.EMPRESAID);

            objVehi.mysConec = mysConec;
            dtVehi = objVehi.Listar(STU_SISTEMA.EMPRESAID);

            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            objTipoExi.mysConec = mysConec;
            dtTipoExis = objTipoExi.Listar();

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objTipDoc.mysConec = mysConec;
            dtTipoDocumento = objTipDoc.Listar();

            objOrdReq.mysConec = mysConec;
            objOrdReq.TraerTodo(STU_SISTEMA.EMPRESAID);
            dtOrdReq = objOrdReq.dtLista;
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtLista.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtLista, true);
        }
        void VerRegistro(int n_IdRegistro)
        {
            booAgregando = true;
            LstGuiaDoc.Clear();

            objRegistros.mysConec = mysConec;
            BE_Registro = objRegistros.TraerRegistro(n_IdRegistro);
            LstDetalle = objRegistros.LstDetalle;
            LstGuiaDoc = objRegistros.LstGuiasDoc;

            if (BE_Registro.n_idcli == 7032)
            {
                ChkOtroCliente.Checked = false;
            }
            else
            {
                ChkOtroCliente.Checked = true;
            }

            LblIdCliente2.Text = BE_Registro.n_idclides.ToString();
            TxtNumRuc2.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_numdoc", BE_Registro.n_idclides.ToString(), "N").ToString();
            TxtCliente2.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_nombre", BE_Registro.n_idclides.ToString(), "N").ToString();

            LblIdCliente.Text = BE_Registro.n_idcli.ToString();
            TxtNumRuc.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_numdoc", BE_Registro.n_idcli.ToString(), "N").ToString();
            TxtCliente.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_nombre", BE_Registro.n_idcli.ToString(), "N").ToString();

            TxtNumSer.Text = BE_Registro.c_numser;
            TxtNumDoc.Text = BE_Registro.c_numdoc;
            TxtFchEmiDoc.Text = BE_Registro.d_fchdoc.ToString();
            CboMotTras.SelectedValue = BE_Registro.n_idmottra;
            TxtNumDocRef.Text = BE_Registro.c_numordcom;

            LblIdDocRef.Text = BE_Registro.n_iddocref.ToString();               // ID DEL DOCUMENTO DEL REFERENCIA, VER TABLA(log_ordenrequerimiento o tabla ped_pedidos)
            TxtNumDocRef.Text = funFunciones.NulosC(BE_Registro.c_numdocref);                                             // NUMERO DEL DOCUMENTO DE REFERENCIA
            
            if (BE_Registro.d_fchpeddocref.ToString() == "")
            {
                TxtFchEmiOC.CustomFormat = " ";
                TxtFchEmiOC.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                TxtFchEmiOC.CustomFormat = "dd/MM/yyyy";
                TxtFchEmiOC.Format = DateTimePickerFormat.Custom;
                TxtFchEmiOC.Text = BE_Registro.d_fchpeddocref.ToString();
            }

            if (BE_Registro.d_fchentdocref.ToString() == "")
            {
                TxtFchEntOC.CustomFormat = " ";
                TxtFchEntOC.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                TxtFchEntOC.CustomFormat = "dd/MM/yyyy";
                TxtFchEntOC.Format = DateTimePickerFormat.Custom;
                TxtFchEntOC.Text = BE_Registro.d_fchentdocref.ToString();
            }
            DataTable dtResult = new DataTable();

            dtResult = funDatos.DataTableFiltrar(dtPunPar, "n_idcli = " + Convert.ToInt32(LblIdCliente.Text) + "");
            funDatos.ComboBoxCargarDataTable(CboPunPar, dtResult, "n_id", "c_des");
            CboPunPar.SelectedValue = BE_Registro.n_idpunpar;             // ID DEL PUNTO DE PARTIDA

            dtResult = funDatos.DataTableFiltrar(dtPunVen, "n_idcli = " + Convert.ToInt32(LblIdCliente2.Text) + "");
            funDatos.ComboBoxCargarDataTable(CboPunVen, dtResult, "n_id", "c_des");
            CboPunVen.SelectedValue = BE_Registro.n_idpunlle;             // ID DEL PUNTO DE LLEGADA

            TxtLugEnt.Text = BE_Registro.c_dirpunlle;
            TxtLugPar.Text = BE_Registro.c_dirpunpar;

            //LblIdCliente2.Text = BE_Registro.n_idclides.ToString();
            //TxtNumRuc2.Text = 

            
            CboTra.SelectedValue = BE_Registro.n_idemptra;
            CboCho.SelectedValue = BE_Registro.n_idcho;

            if (BE_Registro.n_tipgui == 1)
            {
                OptSinDoc.Checked = true;
                OptConDoc.Checked = false;
            }
            if (BE_Registro.n_tipgui == 2)
            {
                OptSinDoc.Checked = false;
                OptConDoc.Checked = true;
            }

            if (BE_Registro.n_aplotrpro == 0)
            {
                ChkOtroCliente.Checked = false;
            }
            else
            {
                ChkOtroCliente.Checked = true;
            }

            // MOSTRAMOS EL BREVETE DEL CONDUCTOR
            DataTable DtFiltro = new DataTable();
            string strCadenaFiltro = "n_id = " + CboCho.SelectedValue.ToString() + "";

            DtFiltro = funDatos.DataTableFiltrar(dtChof, strCadenaFiltro);
            TxtNumBre.Text = DtFiltro.Rows[0]["c_numbre"].ToString();
            
            CboVeh.SelectedValue = BE_Registro.n_idvehtra;

            strCadenaFiltro = "n_id = " + CboVeh.SelectedValue.ToString() + "";
            DtFiltro = funDatos.DataTableFiltrar(dtVehi, strCadenaFiltro);
            TxtNumPla.Text = DtFiltro.Rows[0]["c_numpla"].ToString();

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);

            int n_fila = 2;
            int n_idTipExi;
            int n_idIte;
            int n_idUniMed;
            string c_dato = "";    
            foreach (BE_VTA_GUIASDET element in LstDetalle)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;

                n_idTipExi = element.n_idtipexi;
                n_idIte = element.n_idite;
                n_idUniMed = element.n_idunimed;

                // MOSTRAMOS EL TPO DE EXIXTENCIA
                strCadenaFiltro = "n_id = " + n_idTipExi + "";
                DtFiltro = funDatos.DataTableFiltrar(dtTipoExis, strCadenaFiltro);
                FgItems.SetData(n_fila, 1, DtFiltro.Rows[0]["c_des"].ToString());

                // MOSTRAMOS EL ITEM
                strCadenaFiltro = "n_id = " + n_idIte + "";
                DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                FgItems.SetData(n_fila, 2, DtFiltro.Rows[0]["c_despro"].ToString());

                // MOSTRAMOS LA UNIDAD DE MEDIDA
                strCadenaFiltro = "n_idite = " + n_idIte + "";
                DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                FgItems.SetData(n_fila, 3, DtFiltro.Rows[0]["c_abrpre"].ToString());

                FgItems.SetData(n_fila, 4, element.n_canpro);
                FgItems.SetData(n_fila, 5, element.c_numlot);

                FgItems.SetData(n_fila, 6, element.n_iddocref.ToString());

                n_fila++;
            }
            if (LstDetalle.Count == 0)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;
            }

            // MOSTRAMOS LOS DOCUMENTOS QUE COMPONEN LA GUIA
            FgDoc.Rows.Count = 2;

            for (n_fila = 0; n_fila <= LstGuiaDoc.Count - 1; n_fila++)
            {
                FgDoc.Rows.Count = FgDoc.Rows.Count + 1;

                c_dato = LstGuiaDoc[n_fila].n_iddoc.ToString();
                c_dato = funDatos.DataTableBuscar(dtOrdReq, "n_id", "n_idtipdoc", c_dato, "N").ToString();      // OBTENEMOS EL ID DEL TIPO DE DOCUMENTO
                FgDoc.SetData(FgDoc.Rows.Count - 1, 5, c_dato);                                                 // MOSTRAMOS EL ID DEL TIPO DE DOCUMENTO    

                c_dato = funDatos.DataTableBuscar(dtTipoDocumento,"n_id","c_abr",c_dato,"N").ToString();        // ABREVIATURA DEL TIPO DE DOCUMENTO
                FgDoc.SetData(FgDoc.Rows.Count - 1, 1, c_dato);

                c_dato =  LstGuiaDoc[n_fila].c_numdoc;                                                          // NUMERO DE DOCUMENTO
                FgDoc.SetData(FgDoc.Rows.Count-1, 2, c_dato);

                c_dato = LstGuiaDoc[n_fila].n_iddoc.ToString();
                c_dato = funDatos.DataTableBuscar(dtOrdReq, "n_id", "d_fchemi", c_dato, "N").ToString();        // FECHA DE DOCUMENTOI
                FgDoc.SetData(FgDoc.Rows.Count - 1, 3, c_dato);

                c_dato = LstGuiaDoc[n_fila].n_iddoc.ToString();                                                            // ID DEL TIPO DEL DOCUMENTO
                FgDoc.SetData(FgDoc.Rows.Count - 1, 4, c_dato);
            }

            booAgregando = false;
        }
        void Nuevo()
        {
            DataTable dtResult = new DataTable();
            n_QueHace = 1;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            // CARGAMOS LOS PUNTOS DE LLEGADA SEGUN EL ORIGEN DE LOS ITEMS
            LblIdCliente.Text = "7032";
            TxtCliente.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_nombre", LblIdCliente.Text, "N").ToString();
            TxtNumRuc.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_numdoc", LblIdCliente.Text, "N").ToString();

            //dtResult = funDatos.DataTableFiltrar(dtPunVen, "n_idcli = " + Convert.ToInt32(LblIdCliente.Text) + "");
            //funDatos.ComboBoxCargarDataTable(CboPunVen, dtResult, "n_id", "c_des");

            dtResult = funDatos.DataTableFiltrar(dtPunPar, "n_idcli = " + Convert.ToInt32(LblIdCliente.Text) + "");
            funDatos.ComboBoxCargarDataTable(CboPunPar, dtResult, "n_id", "c_des");

            // CARGAMOS LOS PUNTOS DE LLEGADA SEGUN EL DESTINO DE LOS ITEMS
            LblIdCliente2.Text = "7032";
            TxtCliente2.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_nombre", LblIdCliente2.Text, "N").ToString();
            TxtNumRuc2.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_numdoc", LblIdCliente2.Text, "N").ToString();

            dtResult = funDatos.DataTableFiltrar(dtPunVen, "n_idcli = " + Convert.ToInt32(LblIdCliente2.Text) + "");
            funDatos.ComboBoxCargarDataTable(CboPunVen, dtResult, "n_id", "c_des");
            booAgregando = false;
            OptSinDoc.Checked = true;
            TxtNumSer.Focus();
        }
        void Blanquea()
        {
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";

            TxtNumRuc.Text = "";
            LblIdCliente.Text = "";
            TxtCliente.Text = "";

            TxtFchEmiDoc.Text = "";
            TxtFchEmiDoc.CustomFormat = "dd/MM/yyyy";
            TxtFchEmiDoc.Format = DateTimePickerFormat.Custom;

            CboMotTras.SelectedValue = 0;
            TxtNumDocRef.Text = "";

            TxtFchEmiOC.Text = "";
            TxtFchEmiOC.CustomFormat = " ";
            TxtFchEmiOC.Format = DateTimePickerFormat.Custom;

            TxtFchEntOC.Text = "";
            TxtFchEntOC.CustomFormat = " ";
            TxtFchEntOC.Format = DateTimePickerFormat.Custom;

            CboPunVen.SelectedValue = 0;

            TxtLugEnt.Text = "";
            CboTra.SelectedValue = 0;
            CboCho.SelectedValue = 0;
            TxtNumBre.Text = "";
            CboVeh.SelectedValue = 0;
            TxtNumPla.Text = "";

            ChkOtroCliente.Checked = false;

            TxtNumDocRef.Text = "";
            FgItems.Rows.Count = 2;
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;

            FgDoc.Rows.Count = 2;
            LstDetalle.Clear();
            LstGuiaDoc.Clear();
        }
        void Bloquea()
        {
            //TxtNumRuc.Enabled = !TxtNumRuc.Enabled;
            //CmdBusCli.Enabled = !CmdBusCli.Enabled;

            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtFchEmiDoc.Enabled = !TxtFchEmiDoc.Enabled;
            CboMotTras.Enabled = !CboMotTras.Enabled;
            CboPunVen.Enabled = !CboPunVen.Enabled;
            CboPunPar.Enabled = !CboPunPar.Enabled;

            //TxtLugEnt.Enabled = !TxtLugEnt.Enabled;
            CboTra.Enabled = !CboTra.Enabled;
            CboCho.Enabled = !CboCho.Enabled;
            TxtNumBre.Enabled = !TxtNumBre.Enabled;
            CboVeh.Enabled = !CboVeh.Enabled;
            TxtNumPla.Enabled = !TxtNumPla.Enabled;

            OptSinDoc.Enabled = !OptSinDoc.Enabled;
            OptConDoc.Enabled = !OptConDoc.Enabled;

            CmdAddItem.Enabled = !CmdAddItem.Enabled;
            CmdDelItem.Enabled = !CmdDelItem.Enabled;
            CmdAddNewItem.Enabled = !CmdAddNewItem.Enabled;

            ChkOtroCliente.Enabled = !ChkOtroCliente.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            toolStripSplitButton2.Enabled = !toolStripSplitButton2.Enabled;
            toolStripSplitButton1.Enabled = !toolStripSplitButton1.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir.Enabled = !ToolImprimir.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
        }
        void Modificar()
        {
            DataTable dtResult = new DataTable();
            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            booAgregando = true;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            booAgregando = false;
            TxtNumRuc.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            BE_Registro = objRegistros.TraerRegistro(intIdRegistro);
            if (BE_Registro.n_anulado == 2)
            {
                MessageBox.Show("¡ No se puede eliminar un registro anulado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }
            if (BE_Registro.n_chkalming == 2)
            {
                MessageBox.Show("¡ No se puede eliminar un registro que tiene ingreso a almacen !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }
            if (BE_Registro.n_chkalmsal == 2)
            {
                MessageBox.Show("¡ No se puede eliminar un registro que tiene salida de almacen !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                BE_VTA_GUIAS e_Guia = new BE_VTA_GUIAS();
                objRegistros.mysConec = mysConec;
                e_Guia = objRegistros.TraerRegistro(intIdRegistro);

                if (objRegistros.Eliminar(intIdRegistro, e_Guia.n_tipori) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 2);
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
            CmdBusCli.Enabled = false;
            TxtNumRuc.Enabled = false;
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

            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(BE_ListaReg, LstGuiaDoc, e_GuiaDatos);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(BE_ListaReg, LstGuiaDoc, e_GuiaDatos);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            BE_ListaReg.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_ListaReg.n_id = BE_Registro.n_id;
            BE_ListaReg.n_idano = STU_SISTEMA.ANOTRABAJO;
            BE_ListaReg.n_idmes = Convert.ToInt32(CboMeses.SelectedValue);
            BE_ListaReg.n_idcli = Convert.ToInt32(LblIdCliente.Text);
            //BE_ListaReg.n_idcli = Convert.ToInt32(LblIdCliente2.Text);
            BE_ListaReg.n_idtipdoc = 10;
            BE_ListaReg.c_numser = TxtNumSer.Text;
            BE_ListaReg.c_numdoc = TxtNumDoc.Text;
            BE_ListaReg.d_fchdoc = Convert.ToDateTime(TxtFchEmiDoc.Text);
            BE_ListaReg.n_idemptra = Convert.ToInt32(CboTra.SelectedValue);
            BE_ListaReg.n_idmottra = Convert.ToInt32(CboMotTras.SelectedValue);
            BE_ListaReg.c_numdocref = TxtNumDocRef.Text;
            BE_ListaReg.c_dirpunpar = TxtLugPar.Text;
            BE_ListaReg.c_dirpunlle = TxtLugEnt.Text;
            BE_ListaReg.n_idemptra = Convert.ToInt32(CboTra.SelectedValue);
            BE_ListaReg.n_idcho = Convert.ToInt32(CboCho.SelectedValue);
            BE_ListaReg.n_idvehtra = Convert.ToInt32(CboVeh.SelectedValue);
            BE_ListaReg.n_anulado = 0;
            BE_ListaReg.n_idpunpar = Convert.ToInt32(CboPunPar.SelectedValue);
            BE_ListaReg.n_idpunlle = Convert.ToInt32(CboPunVen.SelectedValue);

            if (OptSinDoc.Checked == true)
            { 
                BE_ListaReg.n_tipgui = 1;
            }
            if (OptConDoc.Checked == true)
            {
                BE_ListaReg.n_tipgui = 2;
            }

            //if (n_QueHace == 1)
            //{
            //    BE_Registro.n_chkalmsal = 1;
            //    BE_Registro.n_chkalming = 1;
            //}
            BE_ListaReg.n_idpunvencli = Convert.ToInt32(CboPunVen.SelectedValue);
            BE_ListaReg.n_tipori = 2;
            BE_ListaReg.n_idclides = Convert.ToInt32(LblIdCliente2.Text);

            if (ChkOtroCliente.Checked == true)
            {
                BE_ListaReg.n_aplotrpro = 1;
            }
            else
            {
                BE_ListaReg.n_aplotrpro = 0;
            }

            int n_fila = 0;
            DataTable DtFiltro = new DataTable();
            string c_tipoexis = "";
            string strCadenaFiltro = "";
            string c_nomitem = "";
            string c_presendes = "";
            List<BE_VTA_GUIASDET> LstDetalleTmp = new List<BE_VTA_GUIASDET>();

            if (FgItems.Rows.Count > 2)
            {
                for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
                {
                    if (funFunciones.NulosC(FgItems.GetData(n_fila, 2)) != "")
                    {
                        BE_VTA_GUIASDET BE_Detalle = new BE_VTA_GUIASDET();

                        c_tipoexis = FgItems.GetData(n_fila, 1).ToString();
                        c_nomitem = FgItems.GetData(n_fila, 2).ToString();
                        c_presendes = FgItems.GetData(n_fila, 3).ToString();

                        BE_Detalle.n_idgui = BE_ListaReg.n_id;
                        BE_Detalle.n_canpro = Convert.ToDouble(FgItems.GetData(n_fila, 4).ToString());
                        if (funFunciones.NulosC(FgItems.GetData(n_fila, 5)) == "")
                        {
                            BE_Detalle.c_numlot = "";
                        }
                        else
                        {
                            BE_Detalle.c_numlot = FgItems.GetData(n_fila, 5).ToString();
                        }

                        // FILTRAMOS EL TIPO DE PRODUCTO PARA OBTENER SU id
                        strCadenaFiltro = "c_des = '" + c_tipoexis + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtTipoExis, strCadenaFiltro);
                        BE_Detalle.n_idtipexi = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        // FILTRAMOS EL ITEM DE LA FILA PARA OBTENER EL ID  
                        strCadenaFiltro = "c_despro = '" + c_nomitem + "'";
                        DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                        BE_Detalle.n_idite = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        // FILTRAMOS LA PRESENTACION PARA OBTENER SU ID
                        strCadenaFiltro = "c_abrpre = '" + c_presendes + "' AND n_idite = " + BE_Detalle.n_idite + "";
                        DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                        BE_Detalle.n_idunimed = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                        if (OptSinDoc.Checked == true)
                        {
                            BE_Detalle.n_iddocref = 0;
                        }
                        else 
                        {
                            string c_dato = FgItems.GetData(n_fila, 6).ToString();                                            // GARDAMOS EL ID DEL DOCUMENTO AL QUE PERTENECE EL ITEM
                            BE_Detalle.n_iddocref = Convert.ToInt32(c_dato);
                        }
                        
                        LstDetalleTmp.Add(BE_Detalle);
                    }
                }
                LstDetalle = LstDetalleTmp;
            }

            if (OptConDoc.Checked == true)
            { 
                if (FgDoc.Rows.Count != 2)
                {
                    LstGuiaDoc.Clear();
                    for (n_fila = 2; n_fila <= FgDoc.Rows.Count - 1; n_fila++)
                    {
                        BE_VTA_GUIASDOC entguidoc = new BE_VTA_GUIASDOC();

                        entguidoc.n_idgui = 0;
                        entguidoc.n_idtipdoc = Convert.ToInt32(FgDoc.GetData(n_fila, 5));
                        entguidoc.n_iddoc = Convert.ToInt32(FgDoc.GetData(n_fila, 4));
                        entguidoc.c_numdoc = FgDoc.GetData(n_fila, 2).ToString();

                        LstGuiaDoc.Add(entguidoc);
                    }
                }
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt32(LblIdCliente.Text) == 0)
            {
                MessageBox.Show("¡ No ha especificado el cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumRuc.Focus();
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
            if (TxtFchEmiDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de emision !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchEmiDoc.Focus();
                return booEstado;
            }
            if (Convert.ToInt32(CboMotTras.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el motivo de traslado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMotTras.Focus();
                return booEstado;
            }
            if (Convert.ToInt32(CboTra.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la empresa de tranporte !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTra.Focus();
                return booEstado;
            }
            if (Convert.ToInt32(CboCho.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombe del chofer !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboCho.Focus();
                return booEstado;
            }
            if (Convert.ToInt32(CboVeh.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el vehiculo de transporte !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboVeh.Focus();
                return booEstado;
            }

            if (Convert.ToInt32(CboPunVen.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el punto de llegada de la mercaderia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboPunVen.Focus();
                return booEstado;
            }

            if (Convert.ToInt32(CboPunVen.SelectedValue) == Convert.ToInt32(CboPunPar.SelectedValue))
            {
                MessageBox.Show("¡ El punto de partida no puede ser igual al punto de llegada !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboPunPar.Focus();
                return booEstado;
            }

            return booEstado;
        }
        private void FrmManGuiasRemision_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
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

        private void ToolNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }
        private void ToolModificar_Click(object sender, EventArgs e)
        {

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
                dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 2);
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
            dtRegistro = null;
            dtLista = null;
            objFormVis = null;

            this.Close();
        }
        private void Tab1_SelectedIndexChanging(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;

            if (n_QueHace != 3) { return; }

            if (tc.SelectedIndex == 1)
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

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
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
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
        private void TxtNumBre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtNumPla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
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
                FgItems.Select(FgItems.Row, 2);
                return;
            }
            if (FgItems.Col == 2)
            {
                FgItems.Cols[2].ComboList = "...";
                FgItems.AllowEditing = true;
                FgItems.Select(FgItems.Row, 3);
                return;
            }
            if (FgItems.Col == 3)
            {
                FgItems.Select(FgItems.Row, 4);
                return;
            }
            if (FgItems.Col == 4)
            {
                FgItems.Select(FgItems.Row-1, 5);
                return;
            }
            if (FgItems.Col == 5)
            {
                FgItems.Select(FgItems.Row, 1);
                FgItems.SetData(FgItems.Row-1, 1, FgItems.GetData(FgItems.Row-1,1));
                return;
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
                // OBTNEMOS LA DESCRIPCION DEL TIPO DE PRODUCTO
                strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 1));

                if (strDesTipPro == "")
                {
                    FgItems.AllowEditing = false;
                    funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 2);
                    return;
                }
                // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
                dtResul = funDatos.DataTableFiltrar(dtTipoExis, "c_des = '" + strDesTipPro + "'");
                if (dtResul.Rows.Count != 0)
                {
                    n_idtipproducto = Convert.ToInt32(dtResul.Rows[0]["n_id"].ToString());
                }
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
                    n_idtipproducto = Convert.ToInt32(dtResul.Rows[0]["n_id"].ToString());

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
            FgItems.AllowEditing = true;
        }
        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 2)
            {
                if (!strCaracteres.Contains(e.KeyChar))
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
        private void CboCho_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();

            if (CboCho.SelectedValue != null)
            {
                string strCadenaFiltro = "n_id = " + CboCho.SelectedValue.ToString() + "";

                DtFiltro = funDatos.DataTableFiltrar(dtChof, strCadenaFiltro);
                TxtNumBre.Text = DtFiltro.Rows[0]["c_numbre"].ToString();
            }
        }
        private void CboVeh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();

            if (CboVeh.SelectedValue != null)
            {
                string strCadenaFiltro = "n_id = " + CboVeh.SelectedValue.ToString() + "";

                DtFiltro = funDatos.DataTableFiltrar(dtVehi, strCadenaFiltro);
                TxtNumPla.Text = DtFiltro.Rows[0]["c_numpla"].ToString();
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
        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text != "")
            {
                string strCad = "0000000000" + TxtNumDoc.Text;
                TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
            }
        }
        private void TxtFchEmiDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                TxtFchEmiOC.CustomFormat = " ";
                TxtFchEmiDoc.Format = DateTimePickerFormat.Custom;
            }
        }
        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 2);

            ListarItems();

            if (dtLista.Rows.Count == 0)
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
        private void ToolImprimir_ButtonClick(object sender, EventArgs e)
        {
            CN_vta_guias objAlm = new CN_vta_guias();
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            objAlm.STU_SISTEMA = STU_SISTEMA;
            objAlm.mysConec = mysConec;
            objAlm.ReportImprimirGuia(intIdRegistro); 
        }
        private void emitirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_vta_guias objAlm = new CN_vta_guias();
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            objAlm.STU_SISTEMA = STU_SISTEMA;
            objAlm.mysConec = mysConec;
            objAlm.ReportImprimirGuia(intIdRegistro);
        }
        private void guiasDelMesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_vta_guias objAlm = new CN_vta_guias();
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            objAlm.STU_SISTEMA = STU_SISTEMA;
            objAlm.ReportGuiasMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
        }
        private void CmdBusDoc_Click(object sender, EventArgs e)
        {

        }
        private void CboCho_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //private void CboCliente_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //if (booAgregando == true) { return; }

        //    //if (Convert.ToInt32(CboCliente.SelectedValue) !=0 )
        //    //    { 
        //    //    DataTable dtfiltrar = new DataTable();
        //    //    dtfiltrar = funDatos.DataTableFiltrar(dtCliPro, "n_id = "+ Convert.ToInt32(CboCliente.SelectedValue) +"");
        //    //    if (dtfiltrar.Rows.Count != 0)
        //    //    {
        //    //        DataTable dtResult = new DataTable();

        //    //        dtResult = funDatos.DataTableFiltrar(dtPunPar, "n_idcli = " + Convert.ToInt32(CboCliente.SelectedValue) + "");
        //    //        if (dtResult.Rows.Count != 0)
        //    //        {
        //    //            funDatos.ComboBoxCargarDataTable(CboPunPar, dtResult, "n_id", "c_des");
        //    //        }
        //    //        else
        //    //        {
        //    //            dtResult = funDatos.DataTableFiltrar(dtPunPar, "n_idcli = 999999");
        //    //            funDatos.ComboBoxCargarDataTable(CboPunPar, dtResult, "n_id", "c_des");
        //    //            CboPunPar.SelectedValue = 0;
        //    //            TxtLugPar.Text = "";
        //    //        }
        //    //    }
        //    //}
        //}
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void CmdDelItem_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            FgItems.RemoveItem(FgItems.Row);
        }
        private void CmdAddItem_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            FgItems.Rows.Count = FgItems.Rows.Count + 1;
        }
        private void FgItems_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgItems.Col == 2)
            {
                BuscarItem("" , 0);
            }
        }
        void BuscarItem(string c_CadenaBuscar, int n_IdCampoBuscar)
        {
            string[,] arrCabeceraDg1 = new string[4, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            int n_iditem;
            int n_idtipexi = 0;
            string c_tipoitem = "";

            // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
            c_tipoitem = FgItems.GetData(FgItems.Row, 1).ToString();
            dtResult = funDatos.DataTableFiltrar(dtTipoExis, "c_des = '" + c_tipoitem + "'");
            if (dtResult.Rows.Count != 0)
            {
                n_idtipexi = Convert.ToInt32(dtResult.Rows[0]["n_id"].ToString());
            }
            dtResult = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + n_idtipexi + "");

            arrCabeceraDg1[0, 0] = "Codigo";
            arrCabeceraDg1[0, 1] = "120";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_codpro";

            arrCabeceraDg1[1, 0] = "Descripcion";
            arrCabeceraDg1[1, 1] = "500";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_despro";

            arrCabeceraDg1[2, 0] = "Uni. Med.";
            arrCabeceraDg1[2, 1] = "50";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "c_abrpre";

            arrCabeceraDg1[3, 0] = "Id";
            arrCabeceraDg1[3, 1] = "0";
            arrCabeceraDg1[3, 2] = "N";
            arrCabeceraDg1[3, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }
            
            n_iditem = Convert.ToInt32(dtResult.Rows[0]["n_id"].ToString());
            FgItems.SetData(FgItems.Row, 2, dtResult.Rows[0]["c_despro"].ToString());

            //MOSTRAMOS LA PRESENTACION DEL ITEM
            dtresulpre = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_iditem + " AND n_default = 1");
            if (dtresulpre.Rows.Count == 1)
            {
                FgItems.SetData(FgItems.Row, 3, dtresulpre.Rows[0]["c_abrpre"].ToString());
            }
            else
            {
                FgItems.SetData(FgItems.Row, 3, "");
            }
        }
        private void CmdAddNewItem_Click(object sender, EventArgs e)
        {
            DataTable dtresult = new DataTable();
            string c_tipoexistencia = "";
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = mysConec;
            objForm.STU_SISTEMA = STU_SISTEMA;
            objForm.n_DeDonde = 2;                        // INDICAMOS QUE EL FORMULARIO ES LLAMADO DESDE OTRO FORMULARIO
            objForm.n_TipoExistencia = 7;
            objForm.MantenimientoItems();

            Int64 n_idnewitem = objForm.n_IdItemCreado;

            // CARGAMOS NUEVMENTE LOS ITEMS 
            objItems.mysConec = mysConec;
            dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);

            // CARGAMOS NUEVAMENTE LA UNIDAD DE MEDIDA
            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            dtresult = funDatos.DataTableFiltrar(dtItems, "n_id = " + n_idnewitem + " ");

            if (dtresult.Rows.Count != 0)
            {
                booAgregando = true;
                FgItems.SetData(FgItems.Row, 1, c_tipoexistencia);
                FgItems.SetData(FgItems.Row, 2, dtresult.Rows[0]["c_despro"].ToString());

                // FILTRAMOS LAS PRESENTACIONES DEL ITEM SELECCIONADO
                dtresult = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_idnewitem + "");

                funFlex.FlexColumnaCombo(FgItems, dtresult, "c_abrpre", 3);    // ITEMS
                FgItems.SetData(FgItems.Row, 3, dtresult.Rows[0]["c_abrpre"].ToString());
                booAgregando = false;
            }
        }
        private void TxtNumSer_TextChanged(object sender, EventArgs e)
        {

        }
        private void CmdAddDoc_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            string c_dato = "";
            string c_cadIN = funFlex.Flex_ObtenerDatosCol(FgDoc,4);
            string[,] arrCabeceraDg1 = new string[5, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            DataTable dtReq = new DataTable();
            int n_Fila;
            int n_row;

            CN_log_ordenrequerimiento funLog = new CN_log_ordenrequerimiento();
            List<BE_LOG_ORDENREQUERIMIENTODET> LstDetReq = new List<BE_LOG_ORDENREQUERIMIENTODET>();

            funLog.mysConec = mysConec;
            funLog.ListarPendientes(STU_SISTEMA.EMPRESAID, c_cadIN);
            dtReq = funLog.dtOrdReqPendiente;

            arrCabeceraDg1[0, 0] = "Nº Orden Requerimiento";
            arrCabeceraDg1[0, 1] = "120";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_numordque";

            arrCabeceraDg1[1, 0] = "Fch. Emision";
            arrCabeceraDg1[1, 1] = "80";
            arrCabeceraDg1[1, 2] = "F";
            arrCabeceraDg1[1, 3] = "d_fchemi";

            arrCabeceraDg1[2, 0] = "Solicitante";
            arrCabeceraDg1[2, 1] = "250";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "c_solapenom";

            arrCabeceraDg1[3, 0] = "Prioridad";
            arrCabeceraDg1[3, 1] = "60";
            arrCabeceraDg1[3, 2] = "C";
            arrCabeceraDg1[3, 3] = "c_despri";

            arrCabeceraDg1[4, 0] = "Id";
            arrCabeceraDg1[4, 1] = "0";
            arrCabeceraDg1[4, 2] = "N";
            arrCabeceraDg1[4, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            xFun.Buscar_CampoOrden = "c_numordque";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtReq);

            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }
            
            if (dtResult.Rows.Count != 0)
            {
                booAgregando = true;
                
                for (n_Fila = 0; n_Fila <= (dtResult.Rows.Count - 1); n_Fila++)
                {
                    FgDoc.Rows.Count = FgDoc.Rows.Count + 1;

                    c_dato = dtResult.Rows[n_Fila]["c_doccomdes"].ToString();
                    FgDoc.SetData(FgDoc.Rows.Count - 1, 1, c_dato);

                    c_dato = dtResult.Rows[n_Fila]["c_numser"].ToString() + "-" + dtResult.Rows[n_Fila]["c_numdoc"].ToString();
                    FgDoc.SetData(FgDoc.Rows.Count - 1, 2, c_dato);

                    c_dato = dtResult.Rows[n_Fila]["d_fchemi"].ToString();
                    FgDoc.SetData(FgDoc.Rows.Count - 1, 3, c_dato);

                    c_dato = dtResult.Rows[n_Fila]["n_id"].ToString();
                    FgDoc.SetData(FgDoc.Rows.Count - 1, 4, c_dato);

                    c_dato = dtResult.Rows[n_Fila]["n_idtipdoc"].ToString();
                    FgDoc.SetData(FgDoc.Rows.Count - 1, 5, c_dato);

                    int n_idReg = Convert.ToInt32(dtResult.Rows[n_Fila]["n_id"]);
                    funLog.TraerRegistro(n_idReg);
                    LstDetReq = funLog.lstReqDet;

                    for (n_row = 0; n_row <= (LstDetReq.Count - 1); n_row++)
                    {
                        FgItems.Rows.Count = FgItems.Rows.Count + 1;

                        c_dato = LstDetReq[n_row].n_idite.ToString();                                                    // ID DEL ITEM
                        c_dato = funDatos.DataTableBuscar(dtItems,"n_id","c_destipexi",c_dato,"N").ToString();           // OBTENEMOS EL ID DEL TIPO DE ITEM
                        FgItems.SetData(FgItems.Rows.Count - 1, 1, c_dato);

                        c_dato = LstDetReq[n_row].n_idite.ToString();                                                    // ID DEL ITEM
                        c_dato = funDatos.DataTableBuscar(dtItems,"n_id","c_despro",c_dato,"N").ToString();              // OBTENEMOS LA DESCRIPCION DEL ITEMS
                        FgItems.SetData(FgItems.Rows.Count - 1, 2, c_dato);

                        c_dato = LstDetReq[n_row].n_idite.ToString();                                                   // ID DEL ITEM
                        c_dato = funDatos.DataTableBuscar(dtItems,"n_id","c_abrpre",c_dato,"N").ToString();             // OBTENEMOS EL ID DE LA UNIDAD DE MEDIDA
                        FgItems.SetData(FgItems.Rows.Count - 1, 3, c_dato);

                        c_dato = LstDetReq[n_row].n_can.ToString();                                                     // CANTIDAD DEL ITEM
                        FgItems.SetData(FgItems.Rows.Count - 1, 4, c_dato);

                        c_dato = LstDetReq[n_row].n_idreq.ToString();                                                   // ID DEL REQUERIMIENTO
                        FgItems.SetData(FgItems.Rows.Count - 1, 6, c_dato);
                    }
                }
                booAgregando = false;
            }
        }
        private void OptSinDoc_CheckedChanged(object sender, EventArgs e)
        {
            FgDoc.Rows.Count = 2;

            if (n_QueHace != 3)
            {
                CmdAddDoc.Enabled = false;
                CmdDelDoc.Enabled = false;
                CmdAddLot.Enabled = false;

                CmdAddItem.Enabled = true;
                CmdDelItem.Enabled = true;
                CmdAddNewItem.Enabled = true;
            }
            FgItems.Rows.Count = n_NumFilasDocumento;
        }
        private void OptConDoc_CheckedChanged(object sender, EventArgs e)
        {
            FgDoc.Rows.Count = 2;
            if (n_QueHace != 3)
            {
                CmdAddDoc.Enabled = true;
                CmdDelDoc.Enabled = true;
                CmdAddLot.Enabled = true;

                CmdAddItem.Enabled = false;
                CmdDelItem.Enabled = false;
                CmdAddNewItem.Enabled = false;
            }
            FgItems.Rows.Count = 2;
        }
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void FgDoc_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { FgDoc.AllowEditing = false; return; }
        }
        private void CmdDelDoc_Click(object sender, EventArgs e)
        {
            if (FgDoc.Rows.Count == 2) { return; }
            int n_iddocumento = Convert.ToInt32(FgDoc.GetData(FgDoc.Row,4));
            FgDoc.RemoveItem(FgDoc.Row);
            EliminarItemsLista(n_iddocumento);
        }
        void EliminarItemsLista(int n_DocumentoReferencia)
        { 
            int n_row = 0;
            for (n_row = 2; n_row <= FgItems.Rows.Count-1; n_row++)
            {
                FgItems.RemoveItem(n_row);
            }
        }
        private void ChkOtroCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }
            DataTable dtResult = new DataTable();

            if (ChkOtroCliente.Checked == true)
            {
                LblIdCliente.Text = "7032";
                TxtNumRuc.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_numdoc", LblIdCliente.Text, "N").ToString();
                TxtCliente.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_nombre", LblIdCliente.Text, "N").ToString();

                LblIdCliente2.Text = "";
                TxtNumRuc2.Text = "";
                TxtCliente2.Text = "";

                CmdBusCli.Enabled = true;
                TxtNumRuc.Enabled = true;

                CmdBusCli2.Enabled = true;
                TxtNumRuc2.Enabled = true;
                
                dtResult = funDatos.DataTableFiltrar(dtPunPar, "n_idcli = 7032");
                funDatos.ComboBoxCargarDataTable(CboPunPar, dtResult, "n_id", "c_des");
                CboPunPar.SelectedValue = 0;

                dtResult = funDatos.DataTableFiltrar(dtPunVen, "n_idcli = 0");
                funDatos.ComboBoxCargarDataTable(CboPunVen, dtResult, "n_id", "c_des");
                CboPunVen.SelectedValue = 0;

                TxtLugPar.Text = "";
                TxtLugEnt.Text = "";
            }
            else
            {
                dtResult = dtPunPar;
                LblIdCliente.Text = "7032";
                LblIdCliente2.Text = "7032";
                CmdBusCli.Enabled = false;
                TxtNumRuc.Enabled = false;

                CmdBusCli2.Enabled = false;
                TxtNumRuc2.Enabled = false;

                TxtNumRuc.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_numdoc", LblIdCliente.Text, "N").ToString();
                TxtCliente.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_nombre", LblIdCliente.Text, "N").ToString();

                TxtNumRuc2.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_numdoc", LblIdCliente2.Text, "N").ToString();
                TxtCliente2.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_nombre", LblIdCliente2.Text, "N").ToString();

                dtResult = funDatos.DataTableFiltrar(dtPunPar, "n_idcli = " + Convert.ToInt32(LblIdCliente.Text) + "");
                funDatos.ComboBoxCargarDataTable(CboPunPar, dtResult, "n_id", "c_des");
                TxtLugPar.Text = "";
                CboPunPar.SelectedValue = 0;

                dtResult = funDatos.DataTableFiltrar(dtPunVen, "n_idcli = " + Convert.ToInt32(LblIdCliente2.Text) + "");
                funDatos.ComboBoxCargarDataTable(CboPunVen, dtResult, "n_id", "c_des");
                TxtLugEnt.Text = "";
                CboPunVen.SelectedValue = 0;
            }
        }

        private void CboPunVen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            DataTable dtResult = new DataTable();

            dtResult = funDatos.DataTableFiltrar(dtPunVen, "n_id = " + Convert.ToInt32(CboPunVen.SelectedValue) + "");
            if (dtResult.Rows.Count != 0)
            {
                TxtLugEnt.Text = dtResult.Rows[0]["c_dir"].ToString();
            }
            else
            {
                TxtLugEnt.Text = "";
            }
        }

        private void CboPunPar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            DataTable dtResult = new DataTable();

            dtResult = funDatos.DataTableFiltrar(dtPunPar, "n_id = " + Convert.ToInt32(CboPunPar.SelectedValue) + "");
            if (dtResult.Rows.Count != 0)
            {
                TxtLugPar.Text = dtResult.Rows[0]["c_dir"].ToString();
            }
            else
            {
                TxtLugPar.Text = "";
            }
        }

        private void CmdAddLot_Click(object sender, EventArgs e)
        {
            Tab1.Enabled = false;
            ToolHerramientas.Enabled = false;

            panel5.Left = ((this.Width - panel5.Width) / 2);
            panel5.Top = ((this.Height - panel5.Height) / 2);
            FgLotes.Rows.Count = 10;
            LblProducto.Text = FgItems.GetData(FgItems.Row, 2).ToString();
            panel5.Visible = true;
        }

        private void CmdCanLot_Click(object sender, EventArgs e)
        {
            Tab1.Enabled = true;
            ToolHerramientas.Enabled = true;
            FgLotes.Rows.Count = 2;
            panel5.Visible = false;
        }

        private void FgLotes_EnterCell(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count == 2) { return; }
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }
            if (booAgregando == true) { return; }


            if (FgItems.Col == 1)
            {
                FgItems.AllowEditing = true;
            }

            if (FgItems.Col == 2)
            {
                FgItems.AllowEditing = true;
            }
        }
        private void FgLotes_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
             if (e.Col == 1)
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 2)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void CmdAceLot_Click(object sender, EventArgs e)
        {
            int n_row = 0;
            double n_can = 0;
            double n_canpro = Convert.ToDouble(FgItems.GetData(FgItems.Row, 4));
            string c_tippro = FgItems.GetData(FgItems.Row, 1).ToString();
            string c_despro = FgItems.GetData(FgItems.Row, 2).ToString();
            string c_unimed = FgItems.GetData(FgItems.Row, 3).ToString();
            int n_iddoc = Convert.ToInt32(FgDoc.GetData(FgDoc.Row, 4));

            string c_numlot = "";
            n_can = Convert.ToDouble(funFlex.FlexSumarCol(FgLotes, 2, 2, FgLotes.Rows.Count-1));

            //for (n_row = 2; n_row <= FgLotes.Rows.Count - 1; n_row++)
            //{
            //    c_numlot = FgLotes.GetData(n_row, 1).ToString();
            //    n_can = Convert.ToDouble(FgLotes.GetData(n_row, 2));
            //    if ((c_numlot == "") && (n_can == 0))
            //    {
                    
            //        return;
            //    }
            //}
            if (n_canpro != n_can)
            {
                MessageBox.Show("¡ La cantidad ingresada de lotes no coincide con la cantidad solicitada en la orden de requerimiento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgLotes.Focus();
                return;
            }
            else
            {
                booAgregando = true;
                // ELIMINAMOS EL ITEM DE LA LISTA DE ITEMS
                FgItems.RemoveItem(FgItems.Row);

                // AGREGAMOS LOS LOTES
                for (n_row = 2; n_row <= FgLotes.Rows.Count - 1; n_row++)
                {
                    c_numlot = funFunciones.NulosC(FgLotes.GetData(n_row, 1));
                    n_can = Convert.ToDouble(funFunciones.NulosN(FgLotes.GetData(n_row, 2)));
                    if ((c_numlot != "") && (n_can != 0))
                    {
                        FgItems.Rows.Count = FgItems.Rows.Count + 1;
                        FgItems.SetData(FgItems.Rows.Count - 1, 1, c_tippro);
                        FgItems.SetData(FgItems.Rows.Count - 1, 2, c_despro);
                        FgItems.SetData(FgItems.Rows.Count - 1, 3, c_unimed);
                        
                        FgItems.SetData(FgItems.Rows.Count - 1, 4, n_can.ToString("0.00"));
                        FgItems.SetData(FgItems.Rows.Count - 1, 5, c_numlot);
                        
                        FgItems.SetData(FgItems.Rows.Count - 1, 6, n_iddoc);
                    }
                }
                booAgregando = false;
            }
            CmdCanLot_Click(sender, e);
        }

        private void eliminarGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void anularGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnularGuia();
        }
        bool AnularGuia()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            
            BE_Registro = objRegistros.TraerRegistro(intIdRegistro);
            if (BE_Registro.n_anulado == 2)
            {
                MessageBox.Show("¡ No se puede anular este registro ya se encuentra anulado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }
            if (BE_Registro.n_chkalming == 2)
            {
                MessageBox.Show("¡ No se puede anular un registro que tiene ingreso a almacen !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }
            if (BE_Registro.n_chkalmsal == 2)
            {
                MessageBox.Show("¡ No se puede anular un registro que tiene salida de almacen !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }

            DialogResult Rpta = MessageBox.Show("Esta seguro de anular el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.CambiarEstado(intIdRegistro, 2) == true)                // CAMBIAMOS EL ESTADO A ANULADO
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se anulo con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 2);
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

        private void modificarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            BE_Registro = objRegistros.TraerRegistro(intIdRegistro);
            if (BE_Registro.n_anulado == 2)
            {
                MessageBox.Show("¡ No se puede modificar un registro anulado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            if (BE_Registro.n_chkalming == 2)
            {
                MessageBox.Show("¡ No se puede modificar un registro que tiene ingreso a almacen !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (BE_Registro.n_chkalmsal == 2)
            {
                MessageBox.Show("¡ No se puede modificar un registro que tiene salida de almacen !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            Modificar();
        }
        bool ActivarGuia()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de activar el registro anulado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.CambiarEstado(intIdRegistro, 1) == true)                // CAMBIAMOS EL ESTADO A ANULADO
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se activo con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 2);
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo activar el registro por el siguiente motivo ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            return booResult;
        }

        private void activarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivarGuia();
        }

        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e)
        {

        }

        private void emitirGuiaComoAnuladaToolStripMenuItem_Click(object sender, EventArgs e)
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
            //TxtFchEmiOC.CustomFormat = "dd/MM/yyyy";
            //TxtFchEmiOC.Format = DateTimePickerFormat.Custom;
            panel6.Visible = true;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Tab1.SelectedIndex = 0;
            Tab1.Enabled = true;
            ToolHerramientas.Enabled = true;           
            panel6.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
                MessageBox.Show("¡ No ha especificado la fecha de emision de la guia ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchEmiOC.Focus();
                return;
            }

            bool b_result = false;
            b_result = objRegistros.InsertarAnulado(STU_SISTEMA.EMPRESAID, 0, 10, Convert.ToDateTime(TxtFchEmiOC.Text), Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, TxtAnuNumSer.Text, TxtAnuNumDoc.Text, 2);    // 2 = INDICA QUE LA GUIA ES DE LOSGISTICA
            if (b_result == false)
            {
                MessageBox.Show("¡ No se pudo emitir la guia anulada por el siguiente motivo :" + objRegistros.StrErrorMensaje + " ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtAnuNumSer.Focus();
                return;
            }
            else 
            {
                MessageBox.Show("¡ la guia anulada se genero con exito ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                
                dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 2);
                ListarItems();
            }
            button1_Click_1(sender, e);
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

        private void TxtFchEmiOC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                TxtFchEmiOC.CustomFormat = " ";
                TxtFchEmiDoc.Format = DateTimePickerFormat.Custom;
            }
        }

        private void TxtFchEmiOC_ValueChanged(object sender, EventArgs e)
        {

        }

        private void TxtFchEmiOC_Enter(object sender, EventArgs e)
        {
            //if (n_QueHace != 3) { return; }

            //TxtFchEntOC.CustomFormat = "dd/MM/yyyy";
            //TxtFchEntOC.Format = DateTimePickerFormat.Custom;
        }

        private void TxtFchEmiOC_MouseMove(object sender, MouseEventArgs e)
        {
            //if (n_QueHace != 3) { return; }
            //TxtFchEntOC.CustomFormat = "dd/MM/yyyy";
            //TxtFchEntOC.Format = DateTimePickerFormat.Custom;
            //TxtFchEntOC.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void CmdBusCli_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            objCliPro.mysConec = mysConec;

            if (ChkOtroCliente.Checked == true) { dtResult = funDatos.DataTableFiltrar(dtCliPro, "n_id <> 7032"); }

            dtResult = objCliPro.BuscarCliPro(dtResult, 1, "n_id", "");
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtNumRuc.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                    LblIdCliente.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtCliente.Text = dtResult.Rows[0]["c_nombre"].ToString();

                    dtResult = funDatos.DataTableFiltrar(dtPunPar, "n_idcli = " + LblIdCliente.Text + "");
                    funDatos.ComboBoxCargarDataTable(CboPunPar, dtResult, "n_id", "c_des");
                    CboPunPar.SelectedValue = 0;
                    TxtLugPar.Text = "";
                    if (LblIdCliente.Text != "7032")
                    {
                        dtResult = funDatos.DataTableFiltrar(dtCliPro, "n_id = 7032");
                        TxtNumRuc2.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                        LblIdCliente2.Text = dtResult.Rows[0]["n_id"].ToString();
                        TxtCliente2.Text = dtResult.Rows[0]["c_nombre"].ToString();

                        dtResult = funDatos.DataTableFiltrar(dtPunPar, "n_idcli = 7032");
                        funDatos.ComboBoxCargarDataTable(CboPunVen, dtResult, "n_id", "c_des");
                        CboPunVen.SelectedValue = 0;
                        TxtLugEnt.Text = "";
                    }
                }
                else
                {
                    TxtNumRuc.Text = "";
                    LblIdCliente.Text = "";
                    TxtCliente.Text = "";
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
            dtresul = funDatos.DataTableFiltrar(dtCliPro, "c_numdoc = '" + TxtNumRuc.Text + "'");

            if (dtresul.Rows.Count != 0)
            {
                TxtNumRuc.Text = dtresul.Rows[0]["c_numdoc"].ToString();
                TxtCliente.Text = dtresul.Rows[0]["c_nombre"].ToString();
                LblIdCliente.Text = dtresul.Rows[0]["n_id"].ToString();

                
                if (Convert.ToInt32(LblIdCliente.Text) != 0)
                {
                    DataTable dtfiltrar = new DataTable();
                    dtfiltrar = funDatos.DataTableFiltrar(dtCliPro, "n_id = " + Convert.ToInt32(LblIdCliente.Text) + "");
                    if (dtfiltrar.Rows.Count != 0)
                    {
                        DataTable dtResult = new DataTable();

                        dtResult = funDatos.DataTableFiltrar(dtPunPar, "n_idcli = " + Convert.ToInt32(LblIdCliente.Text) + "");
                        if (dtResult.Rows.Count != 0)
                        {
                            funDatos.ComboBoxCargarDataTable(CboPunPar, dtResult, "n_id", "c_des");
                        }
                        else
                        {
                            dtResult = funDatos.DataTableFiltrar(dtPunPar, "n_idcli = 999999");
                            funDatos.ComboBoxCargarDataTable(CboPunPar, dtResult, "n_id", "c_des");
                            CboPunPar.SelectedValue = 0;
                            TxtLugPar.Text = "";
                        }
                    }
                }
            }
            else
            {
                TxtNumRuc.Text = "";
                TxtCliente.Text = "";
                LblIdCliente.Text = "";
                TxtNumRuc.Focus();
            }
        }

        private void CmdBusCli2_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            objCliPro.mysConec = mysConec;
            if (ChkOtroCliente.Checked == true) { dtResult = funDatos.DataTableFiltrar(dtCliPro, "n_id <> 7032"); }

            dtResult = objCliPro.BuscarCliPro(dtResult, 1, "n_id", "");
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtNumRuc2.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                    LblIdCliente2.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtCliente2.Text = dtResult.Rows[0]["c_nombre"].ToString();

                    dtResult = funDatos.DataTableFiltrar(dtPunPar, "n_idcli = " + LblIdCliente2.Text + "");
                    funDatos.ComboBoxCargarDataTable(CboPunVen, dtResult, "n_id", "c_des");
                    CboPunVen.SelectedValue = 0;
                    TxtLugEnt.Text = "";

                    if (LblIdCliente2.Text != "7032")
                    {
                        dtResult = funDatos.DataTableFiltrar(dtCliPro, "n_id = 7032");
                        TxtNumRuc.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                        LblIdCliente.Text = dtResult.Rows[0]["n_id"].ToString();
                        TxtCliente.Text = dtResult.Rows[0]["c_nombre"].ToString();

                        dtResult = funDatos.DataTableFiltrar(dtPunPar, "n_idcli = 7032");
                        funDatos.ComboBoxCargarDataTable(CboPunPar, dtResult, "n_id", "c_des");
                        CboPunPar.SelectedValue = 0;
                        TxtLugPar.Text = "";
                    }
                }
                else
                {
                    TxtNumRuc2.Text = "";
                    LblIdCliente2.Text = "";
                    TxtCliente2.Text = "";
                }
            }
        }

        private void CboPunPar_SelectedValueChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) {return;}
            if (booAgregando == true) { return; }

            DataTable dtResult = new DataTable();
            dtResult = funDatos.DataTableFiltrar(dtPunPar, "n_idcli = " + LblIdCliente.Text + "");

            TxtLugPar.Text = funDatos.DataTableBuscar(dtResult, "n_id", "c_dir", Convert.ToInt32(CboPunPar.SelectedValue).ToString(), "N").ToString();
        }

        private void CboPunVen_SelectedValueChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            DataTable dtResult = new DataTable();
            dtResult = funDatos.DataTableFiltrar(dtPunVen, "n_idcli = " + LblIdCliente2.Text + "");

            TxtLugEnt.Text = funDatos.DataTableBuscar(dtResult, "n_id", "c_dir", Convert.ToInt32(CboPunVen.SelectedValue).ToString(), "N").ToString();
        }
    }
}
