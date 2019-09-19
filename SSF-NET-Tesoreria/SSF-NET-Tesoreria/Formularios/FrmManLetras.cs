using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Tesoreria;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Tesoreria;
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
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Ventas;
using SIAC_Entidades.Contabilidad;
using SIAC_Entidades.Ventas;
using SIAC_Negocio.Contabilidad;

namespace SSF_NET_Tesoreria.Formularios
{
    public partial class FrmManLetras : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_tes_letras objRegistros = new CN_tes_letras();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sun_tipdocide o_TipDoc = new CN_sun_tipdocide();
        CN_sun_tipmon o_Moneda= new CN_sun_tipmon();
        CN_tes_lettippla o_TipPla = new CN_tes_lettippla();
        CN_mae_clipro o_CliPro = new CN_mae_clipro();
        CN_sun_tipcam o_TC = new CN_sun_tipcam();
        CN_vta_ventas o_Ventas = new CN_vta_ventas();
        CN_tes_cuentabanco o_CtaBan = new CN_tes_cuentabanco();
        CN_tes_bancos e_bancos = new CN_tes_bancos();
        CN_mae_meses objMes = new CN_mae_meses();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_con_tipdoccomcue o_TipDocComCtaCon = new CN_con_tipdoccomcue();

        // OBJETOS DE ACCESO A DATOS
        Cls_NumeroLetra funLet = new Cls_NumeroLetra();
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        SIAC_Objetos.Funciones objFunciones = new SIAC_Objetos.Funciones();

        // ENTIDADES LOCALES
        BE_TES_LETRAS e_letras = new BE_TES_LETRAS();
        List<BE_TES_LETRASDET> l_letdet = new List<BE_TES_LETRASDET>();
        List<BE_TES_LETRASDOC> l_letdoc = new List<BE_TES_LETRASDOC>();
        List<BE_CON_DIARIO> l_diario = new List<BE_CON_DIARIO>();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO

        DataTable dtForm = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtTipDoc = new DataTable();
        DataTable dtMoneda = new DataTable();
        DataTable dtTipPla = new DataTable();
        DataTable dtCliPro = new DataTable();
        DataTable dtTC = new DataTable();
        DataTable dtCueBan = new DataTable();
        DataTable dtBancos = new DataTable();
        DataTable dtMes = new DataTable();
        DataTable dtTipDocCom = new DataTable();
        DataTable dtTipDocComCtaCon = new DataTable();

        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                 // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int N_IDCTALET = 0;
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        bool booAgregando = false;
        string[,] arrCabeceraDg1 = new string[9, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[10, 5];
        string[,] arrCabeceraFlex2 = new string[12, 5];

        bool b_SeEjecuto = false;
        bool b_Agregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManLetras()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void TxtNumRuc_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtAbr_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void TxtCod_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmManLetras_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void CargarCombos()
        {
            DataTableCargar();
            booAgregando = true;
            funDatos.ComboBoxCargarDataTable(CboDocIde, dtTipDoc, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMoneda, dtMoneda, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipInt, dtTipPla, "n_id", "c_des");

            funDatos.ComboBoxCargarDataTable(CboBanco, dtBancos, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboNumCue, dtCueBan, "n_id", "c_numcue");

            funDatos.ComboBoxCargarDataTable(CboMeses, dtMes, "n_id", "c_des");
            booAgregando = false;
        }
        void ConfigurarFormulario()
        {
            booAgregando = true;
            this.Height = 639;
            this.Width = 850;
 
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;

            arrCabeceraFlex1[0, 0] = "Nº Reg.";
            arrCabeceraFlex1[0, 1] = "0";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "";

            arrCabeceraFlex1[1, 0] = "Nº Documento";
            arrCabeceraFlex1[1, 1] = "110";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "";

            arrCabeceraFlex1[2, 0] = "T.D.";
            arrCabeceraFlex1[2, 1] = "30";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "";

            arrCabeceraFlex1[3, 0] = "Fch. Emision";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "F";
            arrCabeceraFlex1[3, 3] = "dd/MM/yyyy";
            arrCabeceraFlex1[3, 4] = "";

            arrCabeceraFlex1[4, 0] = "M";
            arrCabeceraFlex1[4, 1] = "30";
            arrCabeceraFlex1[4, 2] = "C";
            arrCabeceraFlex1[4, 3] = "";
            arrCabeceraFlex1[4, 4] = "";

            arrCabeceraFlex1[5, 0] = "Cliente";
            arrCabeceraFlex1[5, 1] = "200";
            arrCabeceraFlex1[5, 2] = "C";
            arrCabeceraFlex1[5, 3] = "";
            arrCabeceraFlex1[5, 4] = "";

            arrCabeceraFlex1[6, 0] = "Importe";
            arrCabeceraFlex1[6, 1] = "70";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "";

            arrCabeceraFlex1[7, 0] = "Saldo S/";
            arrCabeceraFlex1[7, 1] = "70";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "";

            arrCabeceraFlex1[8, 0] = "Saldo $";
            arrCabeceraFlex1[8, 1] = "70";
            arrCabeceraFlex1[8, 2] = "D";
            arrCabeceraFlex1[8, 3] = "0.00";
            arrCabeceraFlex1[8, 4] = "";

            arrCabeceraFlex1[9, 0] = "idDoc";
            arrCabeceraFlex1[9, 1] = "0";
            arrCabeceraFlex1[9, 2] = "N";
            arrCabeceraFlex1[9, 3] = "";
            arrCabeceraFlex1[9, 4] = "";

            funFlex.FlexMostrarDatos(FgDocFin, arrCabeceraFlex1, dtLista, 2, false);
            

            arrCabeceraFlex2[0, 0] = "Nº Letra";
            arrCabeceraFlex2[0, 1] = "70";
            arrCabeceraFlex2[0, 2] = "C";
            arrCabeceraFlex2[0, 3] = "";
            arrCabeceraFlex2[0, 4] = "";

            arrCabeceraFlex2[1, 0] = "Plazo";
            arrCabeceraFlex2[1, 1] = "40";
            arrCabeceraFlex2[1, 2] = "C";
            arrCabeceraFlex2[1, 3] = "";
            arrCabeceraFlex2[1, 4] = "";

            arrCabeceraFlex2[2, 0] = "Capital";
            arrCabeceraFlex2[2, 1] = "80";
            arrCabeceraFlex2[2, 2] = "D";
            arrCabeceraFlex2[2, 3] = "0.00";
            arrCabeceraFlex2[2, 4] = "";

            arrCabeceraFlex2[3, 0] = "Interes";
            arrCabeceraFlex2[3, 1] = "80";
            arrCabeceraFlex2[3, 2] = "D";
            arrCabeceraFlex2[3, 3] = "0.00";
            arrCabeceraFlex2[3, 4] = "";

            arrCabeceraFlex2[4, 0] = "Portes";
            arrCabeceraFlex2[4, 1] = "80";
            arrCabeceraFlex2[4, 2] = "D";
            arrCabeceraFlex2[4, 3] = "0.00";
            arrCabeceraFlex2[4, 4] = "";

            arrCabeceraFlex2[5, 0] = "I.G.V.";
            arrCabeceraFlex2[5, 1] = "80";
            arrCabeceraFlex2[5, 2] = "D";
            arrCabeceraFlex2[5, 3] = "0.00";
            arrCabeceraFlex2[5, 4] = "";

            arrCabeceraFlex2[6, 0] = "Total";
            arrCabeceraFlex2[6, 1] = "80";
            arrCabeceraFlex2[6, 2] = "D";
            arrCabeceraFlex2[6, 3] = "0.00";
            arrCabeceraFlex2[6, 4] = "";

            arrCabeceraFlex2[7, 0] = "Fch. Vencimiento";
            arrCabeceraFlex2[7, 1] = "70";
            arrCabeceraFlex2[7, 2] = "F";
            arrCabeceraFlex2[7, 3] = "dd/MM/yyyy";
            arrCabeceraFlex2[7, 4] = "";

            arrCabeceraFlex2[8, 0] = "Banco";
            arrCabeceraFlex2[8, 1] = "0";
            arrCabeceraFlex2[8, 2] = "C";
            arrCabeceraFlex2[8, 3] = "";
            arrCabeceraFlex2[8, 4] = "";

            arrCabeceraFlex2[9, 0] = "Nº Cuenta";
            arrCabeceraFlex2[9, 1] = "0";
            arrCabeceraFlex2[9, 2] = "C";
            arrCabeceraFlex2[9, 3] = "";
            arrCabeceraFlex2[9, 4] = "";

            arrCabeceraFlex2[10, 0] = "idLetra";
            arrCabeceraFlex2[10, 1] = "0";
            arrCabeceraFlex2[10, 2] = "N";
            arrCabeceraFlex2[10, 3] = "";
            arrCabeceraFlex2[10, 4] = "";

            arrCabeceraFlex2[11, 0] = "id Cuenta";
            arrCabeceraFlex2[11, 1] = "0";
            arrCabeceraFlex2[11, 2] = "N";
            arrCabeceraFlex2[11, 3] = "";
            arrCabeceraFlex2[11, 4] = "";

            funFlex.FlexMostrarDatos(FgLet, arrCabeceraFlex2, dtLista, 2, false);

            booAgregando = false;

            N_IDCTALET = Convert.ToInt32(funDatos.DataTableBuscar(dtTipDoc, "n_id", "n_idcueven", "92", "N"));
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            dtLista = objRegistros.dtLista;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(92, ref arrCabeceraDg1);
            
            objForm.mysConec = mysConec;                                         // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(92);

            o_TipDoc.mysConec = mysConec;
            dtTipDoc = o_TipDoc.Listar();

            o_Moneda.mysConec = mysConec;
            dtMoneda = o_Moneda.Listar();

            o_TipPla.mysConec = mysConec;
            o_TipPla.Listar();
            dtTipPla = o_TipPla.dtLista;

            o_CliPro.mysConec = mysConec;
            dtCliPro = o_CliPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            o_TC.mysConec = mysConec;
            dtTC = o_TC.Listartcano(151, STU_SISTEMA.ANOTRABAJO.ToString());

            e_bancos.mysConec = mysConec;
            dtBancos = e_bancos.Listar();

            o_CtaBan.mysConec = mysConec;
            dtCueBan = o_CtaBan.Listar(STU_SISTEMA.EMPRESAID);

            objMes.mysConec = mysConec;
            dtMes = objMes.Listar();

            objTipDoc.mysConec = mysConec;
            dtTipDocCom = objTipDoc.Listar();

            o_TipDocComCtaCon.mysConec = mysConec;
            o_TipDocComCtaCon.Listar(STU_SISTEMA.EMPRESAID);
            dtTipDocComCtaCon = o_TipDocComCtaCon.dtLista;
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtLista.Rows.Count).ToString();
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
            DataTable dtResult = new DataTable();
            DataTable dtDoc = new DataTable();
            int n_row = 0;
            double n_valor = 0;

            l_letdet.Clear();
            l_letdoc.Clear();
            FgLet.Rows.Count = 2;
            FgDocFin.Rows.Count = 2;
            objRegistros.mysConec = mysConec;

            objRegistros.TraerRegistro(n_IdRegistro);
            if (objRegistros.b_OcurrioError == false)
            {
                e_letras = objRegistros.e_Letras;
                l_letdet = objRegistros.l_LetDet;
                l_letdoc = objRegistros.l_LetDoc;
                dtDoc = objRegistros.dtListaDoc;     

                LblidCliente.Text = e_letras.n_idclipro.ToString();
                TxtNumRuc.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_numdoc", e_letras.n_idclipro.ToString(), "N").ToString();
                TxtCliente.Text = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_nombre", e_letras.n_idclipro.ToString(), "N").ToString();
                
                TxtGirA.Text = e_letras.c_girnom;
                TxtDirGir.Text = e_letras.c_girdir;
                TxtNumTel.Text = e_letras.c_girtel;
                CboDocIde.SelectedValue=0;
                TxtNumDocIde.Text = e_letras.c_girnumdoc;
                                
                TxtFchEmi.Text  = Convert.ToDateTime(e_letras.d_fchreg).ToString("dd/MM/yyyy");
                TxtFchIni.Text  = Convert.ToDateTime(e_letras.d_fchini).ToString("dd/MM/yyyy");
                
                //e_letras.n_tiplet                                          // TIPO DE LETRA 1 = CLIENTE;   2 = PROVEEDORES
                TxtInterval.Text = e_letras.n_numdiaint.ToString();
                CboMoneda.SelectedValue = Convert.ToInt16(e_letras.n_idmon);
                TxtNumLet.Text = e_letras.n_numlet.ToString();
                TxtImpFin.Text = Convert.ToDouble(e_letras.n_impcap).ToString("0.00");
                CboTipInt.SelectedValue = Convert.ToInt16(e_letras.n_idtipint);
                TxtTasa.Text = e_letras.n_portas.ToString("0.00");
                //TxtGlo.Text = e_letras.c_glosa;
                TxtDiaPla.Text = e_letras.n_numdiapla.ToString();
                LblTc.Text = e_letras.n_tc.ToString("0.00");
                TxtPortes.Text = Convert.ToDouble(e_letras.n_imppor).ToString("0.00");

                CboBanco.SelectedValue = Convert.ToInt16(e_letras.n_idban);
                CboNumCue.SelectedValue = Convert.ToInt16(e_letras.n_idcueban);
            }

            for (n_row = 0; n_row <= l_letdet.Count - 1; n_row++)
            {
                FgLet.Rows.Count = FgLet.Rows.Count + 1;

                FgLet.SetData(FgLet.Rows.Count - 1, 1, l_letdet[n_row].c_numlet);
                FgLet.SetData(FgLet.Rows.Count - 1, 2, l_letdet[n_row].n_diapla.ToString());
                FgLet.SetData(FgLet.Rows.Count - 1, 3, l_letdet[n_row].n_impcap.ToString("0.00"));
                FgLet.SetData(FgLet.Rows.Count - 1, 4, l_letdet[n_row].n_impint.ToString("0.00"));
                FgLet.SetData(FgLet.Rows.Count - 1, 5, l_letdet[n_row].n_imppor.ToString("0.00"));
                FgLet.SetData(FgLet.Rows.Count - 1, 6, "0.00");
                FgLet.SetData(FgLet.Rows.Count - 1, 7, l_letdet[n_row].n_implet.ToString("0.00"));
                FgLet.SetData(FgLet.Rows.Count - 1, 8, Convert.ToDateTime(l_letdet[n_row].d_fchven).ToString("dd/MM/yyyy"));
                FgLet.SetData(FgLet.Rows.Count - 1, 11, l_letdet[n_row].n_id);
            }

            for (n_row = 0; n_row <= dtDoc.Rows.Count - 1; n_row++)
            {
                FgDocFin.Rows.Count = FgDocFin.Rows.Count + 1;
                FgDocFin.SetData(FgDocFin.Rows.Count - 1, 2, dtDoc.Rows[n_row]["c_numdoc"].ToString());
                FgDocFin.SetData(FgDocFin.Rows.Count - 1, 3, dtDoc.Rows[n_row]["c_abr"].ToString());
                FgDocFin.SetData(FgDocFin.Rows.Count - 1, 4, Convert.ToDateTime(dtDoc.Rows[n_row]["d_fchdoc"]).ToString("dd/MM/yyyy"));
                FgDocFin.SetData(FgDocFin.Rows.Count - 1, 5, dtDoc.Rows[n_row]["c_simbolo"].ToString());
                FgDocFin.SetData(FgDocFin.Rows.Count - 1, 6, dtDoc.Rows[n_row]["c_nombre"].ToString());
                FgDocFin.SetData(FgDocFin.Rows.Count - 1, 7, Convert.ToDouble(dtDoc.Rows[n_row]["n_imptotven"]).ToString("0.00"));

                if (Convert.ToInt16(dtDoc.Rows[n_row]["n_idmon"]) == 115)
                {
                    FgDocFin.SetData(FgDocFin.Rows.Count - 1, 8, Convert.ToDouble(dtDoc.Rows[n_row]["n_impfin"]).ToString("0.00"));
                    
                    n_valor = Convert.ToDouble(dtDoc.Rows[n_row]["n_impfin"]);
                    n_valor = n_valor / Convert.ToDouble(LblTc.Text);
                    FgDocFin.SetData(FgDocFin.Rows.Count - 1, 9, n_valor.ToString("0.00"));
                }
                else
                {
                    FgDocFin.SetData(FgDocFin.Rows.Count - 1, 9, Convert.ToDouble(dtDoc.Rows[n_row]["n_impfin"]).ToString("0.00"));
                    n_valor = Convert.ToDouble(dtDoc.Rows[n_row]["n_impfin"]);
                    n_valor = n_valor * Convert.ToDouble(LblTc.Text);
                    FgDocFin.SetData(FgDocFin.Rows.Count - 1, 8, n_valor.ToString("0.00"));
                }
                FgDocFin.SetData(FgDocFin.Rows.Count - 1, 10, Convert.ToInt32(dtDoc.Rows[n_row]["n_iddoc"]).ToString());
            }

            TxtImpSol.Text = funFlex.FlexSumarCol(FgDocFin, 8, 2, FgDocFin.Rows.Count - 1).ToString("0.00");
            TxtImpDol.Text = funFlex.FlexSumarCol(FgDocFin, 9, 2, FgDocFin.Rows.Count - 1).ToString("0.00");

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
            Tab02.SelectedIndex = 0;
            TxtInterval.Text = "30";
            CboTipInt.SelectedValue = 2;
            booAgregando = false;
            LblTc.Text = objFunciones.ObtenerTC(dtTC, DateTime.Now.ToString("dd/MM/yyyy")).ToString("0.000");
            CboMoneda.SelectedValue = 115;
            TxtPortes.Text = "0.00";
            TxtTasa.Text = "0.00";
            TxtDiaPla.Text = "30";
            TxtNumLet.Text = "1";
            TxtNumRuc.Focus();
        }
        void Blanquea()
        {
            TxtNumRuc.Text = "";
            TxtCliente.Text = "";
            LblidCliente.Text = "";
            TxtGirA.Text = "";
            TxtDirGir.Text = "";
            CboDocIde.SelectedValue=0;
            TxtNumDocIde.Text = "";
            TxtNumTel.Text = "";
            CboMoneda.SelectedValue=0;
            LblTc.Text = "";
            CboTipInt.SelectedValue = 0;
            TxtTasa.Text = "";
            TxtPortes.Text = "";
            TxtImpFin.Text = "";
            TxtDiaPla.Text = "";
            TxtInterval.Text = "";
            TxtNumLet.Text = "";

            CboBanco.SelectedValue = 0;
            CboNumCue.SelectedValue = 0;

            TxtImpSol.Text = "";
            TxtImpDol.Text = "";
            TxtTotSol1.Text = "";
            TxtTotSol2.Text = "";
            TxtTotSol3.Text = "";
            TxtTotSol4.Text = "";
            TxtTotSol5.Text = "";

            TxtTotDol1.Text = "";
            TxtTotDol2.Text = "";
            TxtTotDol3.Text = "";
            TxtTotDol4.Text = "";
            TxtTotDol5.Text = "";

            FgDocFin.Rows.Count = 2;
            FgLet.Rows.Count = 2;

            l_letdet.Clear();
            l_letdoc.Clear();
        }
        void Bloquea()
        {
            TxtNumRuc.Enabled = !TxtNumRuc.Enabled;
            //TxtCliente.Enabled = TxtCliente.Enabled;
            //LblidCliente.Enabled = LblidCliente.Enabled;
            //TxtGirA.Enabled = TxtGirA.Enabled;
            //TxtDirGir.Enabled = TxtDirGir.Enabled;
            //CboDocIde.Enabled = CboDocIde.Enabled;
            //TxtNumDocIde.Enabled = TxtNumDocIde.Enabled;
            CboBanco.Enabled = !CboBanco.Enabled;
            CboNumCue.Enabled = !CboNumCue.Enabled;

            TxtFchEmi.Enabled = !TxtFchEmi.Enabled;
            TxtFchIni.Enabled = !TxtFchIni.Enabled;
            CboMoneda.Enabled = !CboMoneda.Enabled;
            //LblTc.Enabled = LblTc.Enabled;
            CboTipInt.Enabled = !CboTipInt.Enabled;
            TxtTasa.Enabled = !TxtTasa.Enabled;
            TxtPortes.Enabled = !TxtPortes.Enabled;
            TxtImpFin.Enabled = !TxtImpFin.Enabled;
            TxtDiaPla.Enabled = !TxtDiaPla.Enabled;
            TxtInterval.Enabled = !TxtInterval.Enabled;
            TxtNumLet.Enabled = !TxtNumLet.Enabled;


            CmdBusCli.Enabled = !CmdBusCli.Enabled;
            CmdAddDoc.Enabled = !CmdAddDoc.Enabled;
            CmdDelDoc.Enabled = !CmdDelDoc.Enabled;
            CmdGenLet.Enabled = !CmdGenLet.Enabled;
            CmdAddLet.Enabled = !CmdAddLet.Enabled;
            CmdDelLet.Enabled = !CmdDelLet.Enabled;

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
        }
        void Modificar()
        {
            n_QueHace = 2;
            booAgregando = true;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            Tab02.SelectedIndex = 0;
            booAgregando = false;
            TxtNumRuc.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboMeses.SelectedValue));
                    dtLista = objRegistros.dtLista;
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
            objRegistros.l_diario = l_diario;
            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(e_letras, l_letdet, l_letdoc);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(e_letras, l_letdet, l_letdoc);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.n_ErrorNumber.ToString() + " = " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            if (n_QueHace == 1) {e_letras.n_id = 0;}
            if (n_QueHace == 2) { e_letras.n_id = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString()); }

            e_letras.n_idemp = STU_SISTEMA.EMPRESAID;
            e_letras.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_letras.n_mes = Convert.ToInt16( CboMeses.SelectedValue);
            e_letras.n_idlib = 999;

            l_letdet.Clear();
            l_letdoc.Clear();

            e_letras.c_numreg ="";
            e_letras.n_tiplet = 1;
            e_letras.d_fchreg = Convert.ToDateTime(TxtFchEmi.Text);
            e_letras.d_fchini = Convert.ToDateTime(TxtFchIni.Text);
            e_letras.n_idclipro = Convert.ToInt32(LblidCliente.Text);
            e_letras.n_idmon= Convert.ToInt16(CboMoneda.SelectedValue);
            e_letras.n_numlet = Convert.ToInt32(TxtNumLet.Text);
            e_letras.n_impcap = Convert.ToDouble(TxtImpFin.Text);
            e_letras.n_idtipint= Convert.ToInt32(CboTipInt.SelectedValue);
            e_letras.n_portas = Convert.ToDouble(TxtTasa.Text);
            //e_letras.c_glosa= Convert.ToInt32(TxtNumLet.Text);
            e_letras.n_numdiaint= Convert.ToInt16(TxtInterval.Text);
            e_letras.n_tc = Convert.ToDouble(LblTc.Text);
            e_letras.c_girnumdoc = TxtNumDocIde.Text;
            e_letras.c_girnom = TxtGirA.Text;
            e_letras.c_girtel = TxtNumTel.Text;
            e_letras.c_girdir = TxtDirGir.Text;
            e_letras.n_imppor = Convert.ToDouble(TxtPortes.Text);
            e_letras.n_numdiapla = Convert.ToInt16(TxtDiaPla.Text);
            e_letras.n_idban = Convert.ToInt16(CboBanco.SelectedValue);
            e_letras.n_idcueban = Convert.ToInt16(CboNumCue.SelectedValue);

            int n_row = 0;
            int N_IDCTADOC = 0;
            double n_valor = 0;

            for(n_row = 2; n_row<=FgLet.Rows.Count-1;n_row++)
            {
                BE_TES_LETRASDET e_letdet = new BE_TES_LETRASDET();

                e_letdet.n_idlet = 0;
                e_letdet.n_id = 0;
                e_letdet.c_numser = "0001";
                e_letdet.c_numlet = FgLet.GetData(n_row,1).ToString();
                e_letdet.d_fchemi = Convert.ToDateTime(TxtFchIni.Text);
                e_letdet.d_fchven = Convert.ToDateTime(FgLet.GetData(n_row, 8).ToString());
                e_letdet.n_impcap = Convert.ToDouble(FgLet.GetData(n_row, 3).ToString());
                e_letdet.n_imppor = 0;
                e_letdet.n_impint = 0;
                e_letdet.n_implet = Convert.ToDouble(FgLet.GetData(n_row, 7).ToString());
                e_letdet.n_diapla = Convert.ToInt32(FgLet.GetData(n_row, 2).ToString());
                e_letdet.n_impsal = Convert.ToDouble(FgLet.GetData(n_row, 7).ToString());

                string c_mon = "";
                if (Convert.ToDouble(CboMoneda.SelectedValue) == 115) { c_mon = "soles."; }
                if (Convert.ToDouble(CboMoneda.SelectedValue) == 151) { c_mon = "dolares americanos."; }

                e_letdet.c_imptex = funLet.Convertir(e_letdet.n_implet.ToString(), true, c_mon);

                l_letdet.Add(e_letdet);
            }

            for(n_row = 2; n_row<=FgDocFin.Rows.Count-1;n_row++)
            {
                BE_TES_LETRASDOC e_letdoc = new BE_TES_LETRASDOC();

                e_letdoc.n_idlet =0;
                e_letdoc.n_iddoc =Convert.ToInt32(FgDocFin.GetData(n_row,10).ToString());

                if (Convert.ToInt16(CboMoneda.SelectedValue) == 115)
                {
                    e_letdoc.n_impfin = Convert.ToDouble(FgDocFin.GetData(n_row, 8).ToString());
                }
                else
                {
                    e_letdoc.n_impfin = Convert.ToDouble(FgDocFin.GetData(n_row, 9).ToString());
                }

                l_letdoc.Add(e_letdoc);
            }

            n_row = 0;

            string c_NumAsi = "";
            CN_con_diario o_diario = new CN_con_diario();
            o_diario.mysConec = mysConec;
            if (n_QueHace == 1)
            {
                //n_idret = 0;
                c_NumAsi = o_diario.ObtenerUltimoAsiento(STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 35, STU_SISTEMA.EMPRESAID);
            }
            else
            {
                //n_idret = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
                c_NumAsi = LblNumAsi.Text;
            }

            // ESCRIBIMOS EL DEBE
            for (n_row = 2; n_row <= FgLet.Rows.Count - 1; n_row++)
            { 
                BE_CON_DIARIO e_diario1 = new BE_CON_DIARIO();
                e_diario1.n_id = 0;
                e_diario1.n_idemp = STU_SISTEMA.EMPRESAID;
                e_diario1.n_ano = STU_SISTEMA.ANOTRABAJO;
                e_diario1.n_mes = Convert.ToInt16(CboMeses.SelectedValue);
                e_diario1.n_lib = 35;
                e_diario1.c_numasi = "";
                e_diario1.n_idcue = N_IDCTALET;
                e_diario1.n_tc = Convert.ToDouble(LblTc.Text);

                n_valor = Convert.ToDouble(FgLet.GetData(n_row, 7));
                if (Convert.ToInt16(CboMoneda.SelectedValue) == 115)
                {
                    e_diario1.n_impdebsol = n_valor;
                    e_diario1.n_imphabsol = 0;
                    e_diario1.n_impdebdol = n_valor / Convert.ToDouble(LblTc.Text);
                    e_diario1.n_imphabdol = 0;
                }
                else
                {
                    e_diario1.n_impdebsol = n_valor * Convert.ToDouble(LblTc.Text);
                    e_diario1.n_imphabsol = 0;
                    e_diario1.n_impdebdol = n_valor;
                    e_diario1.n_imphabdol = 0;
                }
                e_diario1.d_fchasi = Convert.ToDateTime(TxtFchEmi.Text);
                e_diario1.d_orifchdoc = Convert.ToDateTime(TxtFchEmi.Text);
                e_diario1.n_oriid = 0;
                e_diario1.n_oriidtipdoc = 92;
                e_diario1.n_oriidtipmon = Convert.ToInt16(CboMoneda.SelectedValue);
                e_diario1.c_orinumdoc = FgLet.GetData(n_row, 1).ToString();
                e_diario1.c_origlo = "";
                e_diario1.c_oridestipmon = Convert.ToString(funDatos.DataTableBuscar(dtMoneda, "n_id", "c_simbolo", Convert.ToInt16(CboMoneda.SelectedValue).ToString(), "N")).ToString();
                e_diario1.c_oridestipdoc = Convert.ToString(funDatos.DataTableBuscar(dtTipDocCom, "n_id", "c_abr", "92", "N")).ToString();
                e_diario1.c_orinomcli = TxtCliente.Text;
                e_diario1.c_orinumruc = TxtNumRuc.Text;
                l_diario.Add(e_diario1);
            }

            // ESCRIBIMOS EL HABER
            for (n_row = 2; n_row <= FgDocFin.Rows.Count - 1; n_row++)
            {
                N_IDCTADOC = Convert.ToInt32(funDatos.DataTableBuscar(dtTipDocComCtaCon, "n_idtipdoc", "n_idcueven", FgDocFin.GetData(n_row, 10).ToString(), "N"));
                if (Convert.ToInt16(CboMoneda.SelectedValue) == 115) { n_valor = Convert.ToDouble(FgDocFin.GetData(n_row, 8)); }
                if (Convert.ToInt16(CboMoneda.SelectedValue) == 151) { n_valor = Convert.ToDouble(FgDocFin.GetData(n_row, 9)); }
                
                BE_CON_DIARIO e_diario2 = new BE_CON_DIARIO();
                e_diario2.n_id = 0;
                e_diario2.n_idemp = STU_SISTEMA.EMPRESAID;
                e_diario2.n_ano = STU_SISTEMA.ANOTRABAJO;
                e_diario2.n_mes = Convert.ToInt16(CboMeses.SelectedValue);
                e_diario2.n_lib = 35;
                e_diario2.c_numasi = "";
                e_diario2.n_idcue = N_IDCTADOC;
                e_diario2.n_tc = Convert.ToDouble(LblTc.Text);

                n_valor = Convert.ToDouble(FgLet.GetData(n_row, 7));
                if (Convert.ToInt16(CboMoneda.SelectedValue) == 115)
                {
                    e_diario2.n_impdebsol = 0;
                    e_diario2.n_imphabsol = n_valor;
                    e_diario2.n_impdebdol = 0;
                    e_diario2.n_imphabdol = n_valor / Convert.ToDouble(LblTc.Text);;
                }
                else
                {
                    e_diario2.n_impdebsol = 0;
                    e_diario2.n_imphabsol = n_valor * Convert.ToDouble(LblTc.Text);
                    e_diario2.n_impdebdol = 0;
                    e_diario2.n_imphabdol = n_valor;
                }
                e_diario2.d_fchasi = Convert.ToDateTime(TxtFchEmi.Text);
                CN_vta_ventas o_ven = new CN_vta_ventas();
                BE_VTA_VENTAS e_ven = new BE_VTA_VENTAS();

                o_ven.mysConec = mysConec;
                e_ven = o_ven.TraerRegistro(Convert.ToInt32(FgDocFin.GetData(n_row, 10)));

                e_diario2.d_orifchdoc = Convert.ToDateTime(FgDocFin.GetData(n_row, 4));
                e_diario2.n_oriid = Convert.ToInt32(e_ven.n_id);
                e_diario2.n_oriidtipdoc = e_ven.n_idtipdoc;
                e_diario2.n_oriidtipmon = e_ven.n_idmon;
                e_diario2.c_orinumdoc = e_ven.c_numser + "-" + e_ven.c_numdoc;
                e_diario2.c_origlo = e_ven.c_glosa;
                e_diario2.c_oridestipmon = Convert.ToString(funDatos.DataTableBuscar(dtMoneda, "n_id", "c_simbolo", e_ven.n_idmon.ToString(), "N")).ToString();
                e_diario2.c_oridestipdoc = Convert.ToString(funDatos.DataTableBuscar(dtTipDocCom, "n_id", "c_abr", e_ven.n_idtipdoc.ToString(), "N")).ToString();
                e_diario2.c_orinomcli = TxtCliente.Text;
                e_diario2.c_orinumruc = TxtNumRuc.Text;
                l_diario.Add(e_diario2);
            }

        }
        bool CamposOK()
        {
            bool booEstado = true;
            
            if (TxtNumRuc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de ruc del cliente!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumRuc.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboMoneda.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMoneda.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboTipInt.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de interes !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTipInt.Focus();
                return booEstado;
            }
            //if (Convert.ToDouble(TxtTasa.Text) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado la tasa de interes !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    TxtTasa.Focus();
            //    return booEstado;
            //}
            //if (Convert.ToDouble(TxtPortes.Text) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado los portes de la letra !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    TxtTasa.Focus();
            //    return booEstado;
            //}
            if (Convert.ToDouble(TxtImpFin.Text) == 0)
            {
                MessageBox.Show("¡ No ha especificado el importe a financiar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtImpFin.Focus();
                return booEstado;
            }
            //if (Convert.ToInt16(TxtDiaPla.Text) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado los dias de plazo para la emision de la letra !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    TxtDiaPla.Focus();
            //    return booEstado;
            //}
            if (Convert.ToDouble(TxtInterval.Text) == 0)
            {
                MessageBox.Show("¡ No ha especificado los intervalos para cada letra !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtInterval.Focus();
                return booEstado;
            }
            if (Convert.ToDouble(TxtNumLet.Text) == 0)
            {
                MessageBox.Show("¡ No ha especificado el numero de letras a emitir !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumLet.Focus();
                return booEstado;
            }
            if (FgDocFin.Rows.Count == 2)
            {
                MessageBox.Show("¡ No ha indicado los documentos a canjear !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CmdAddDoc.Focus();
                return booEstado;
            }

            if (FgLet.Rows.Count == 2)
            {
                MessageBox.Show("¡ No ha generado las letras, haga clic en el boton generar de la pestaña letras generadas !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CmdGenLet.Focus();
                return booEstado;
            }

            double n_valor = funFlex.FlexSumarCol(FgLet, 7, 2, FgLet.Rows.Count-1);
            if (Convert.ToDouble(TxtImpFin.Text) != n_valor)
            {
                MessageBox.Show("¡ El importe a financiar no corresponde con el importe de la letras generadas !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtImpFin.Focus();
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
                objRegistros.mysConec = mysConec;
                objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboMeses.SelectedValue));
                dtLista = objRegistros.dtLista;
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
            this.Close();
        }

        private void FrmManLetras_Activated(object sender, EventArgs e)
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

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            b_Agregando = true;
            VerRegistro(intIdRegistro);
            b_Agregando = false;
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
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    b_Agregando = true;
                    VerRegistro(intIdRegistro);
                    b_Agregando = false;
                }
            }
        }

        private void CmdBusCli_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            o_CliPro.mysConec = mysConec;
            dtResult = o_CliPro.BuscarCliPro(dtCliPro, 1, "n_id", "");
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtNumRuc.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                    LblidCliente.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtCliente.Text = dtResult.Rows[0]["c_nombre"].ToString();
                    
                    TxtGirA.Text = dtResult.Rows[0]["c_letnomgir"].ToString();
                    TxtDirGir.Text = dtResult.Rows[0]["c_letgirdir"].ToString();
                    //CboDocIde.SelectedValue = Convert.ToInt16(funFunciones.NulosN(dtResult.Rows[0]["n_lettipdocide"]));
                    TxtNumDocIde.Text = dtResult.Rows[0]["c_letnumdoc"].ToString();
                    TxtNumTel.Text = dtResult.Rows[0]["c_lettel"].ToString(); 
                }
                else
                {
                    TxtNumRuc.Text = "";
                    LblidCliente.Text = "";
                    TxtCliente.Text = "";
                    
                    TxtGirA.Text = "";
                    TxtDirGir.Text = "";
                    CboDocIde.SelectedValue = 0;
                    TxtNumDocIde.Text = "";
                    TxtNumTel.Text = "";
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
                LblidCliente.Text = dtresul.Rows[0]["n_id"].ToString();

                TxtGirA.Text = dtresul.Rows[0]["c_letnomgir"].ToString();
                TxtDirGir.Text = dtresul.Rows[0]["c_letgirdir"].ToString();
                //CboDocIde.SelectedValue = Convert.ToInt16(dtresul.Rows[0]["n_lettipdocide"].ToString());
                TxtNumDocIde.Text = dtresul.Rows[0]["c_letnumdoc"].ToString();
                TxtNumTel.Text = dtresul.Rows[0]["c_lettel"].ToString(); 
            }
            else
            {
                TxtNumRuc.Text = "";
                LblidCliente.Text = "";
                TxtCliente.Text = "";

                TxtGirA.Text = "";
                TxtDirGir.Text = "";
                CboDocIde.SelectedValue = 0;
                TxtNumDocIde.Text = "";
                TxtNumTel.Text = "";
            }
        }

        private void TxtNumDocIde_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtTasa_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtPortes_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtImpFin_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtDiaPla_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtInterval_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtNumLet_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtFchEmi_Validated(object sender, EventArgs e)
        {
            if (STU_SISTEMA.MESTRABAJO == 0)
            {
                DataTable dttctem = new DataTable();
                o_TC.mysConec = mysConec;
                int n_ano = Convert.ToDateTime(TxtFchEmi.Text).Year;
                dttctem = o_TC.Listartcano(151, n_ano.ToString());
                LblTc.Text = objFunciones.ObtenerTC(dttctem, TxtFchEmi.Text).ToString("0.000");
            }
            else
            {
                LblTc.Text = objFunciones.ObtenerTC(dtTC, TxtFchEmi.Text).ToString("0.000");
            }
        }

        private void CmdAddDoc_Click(object sender, EventArgs e)
        {
            BuscarDocVentaConSaldo();
        }
        void BuscarDocVentaConSaldo()
        {
            if (Convert.ToInt32(funFunciones.NulosN(LblidCliente.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumRuc.Focus();
                return;
            }
            if (Convert.ToInt32(CboMoneda.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMoneda.Focus();
                return;
            }
            if (Convert.ToDouble(funFunciones.NulosN(LblTc.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de cambio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchEmi.Focus();
                return;
            }

            DataTable dtResul = new DataTable();
            int n_row = 0;
            string c_dato = "";
            int n_idmon = Convert.ToInt16(CboMoneda.SelectedValue);
            //double n_valor = 0;
            double n_tc = Convert.ToDouble(funFunciones.NulosN(LblTc.Text));
            o_Ventas.mysConec = mysConec;
            dtResul = o_Ventas.DocumentosConSaldo(STU_SISTEMA.EMPRESAID, Convert.ToInt16(LblidCliente.Text));

            if (dtResul != null)
            {
                if (dtResul.Rows.Count != 0)
                {
                    //FgDocFin.Rows.Count = 2;
                    for (n_row = 0; n_row <= dtResul.Rows.Count - 1; n_row++)
                    {
                        FgDocFin.Rows.Count = FgDocFin.Rows.Count + 1;

                        c_dato = dtResul.Rows[n_row]["c_numdoc"].ToString();
                        FgDocFin.SetData(FgDocFin.Rows.Count - 1, 2, c_dato);

                        c_dato = dtResul.Rows[n_row]["c_destipdoc"].ToString();
                        FgDocFin.SetData(FgDocFin.Rows.Count - 1, 3, c_dato);

                        c_dato = Convert.ToDateTime(dtResul.Rows[n_row]["d_fchdoc"]).ToString("dd/MM/yyyy");
                        FgDocFin.SetData(FgDocFin.Rows.Count - 1, 4, c_dato);


                        c_dato = dtResul.Rows[n_row]["c_desmon"].ToString();
                        FgDocFin.SetData(FgDocFin.Rows.Count - 1, 5, c_dato);
                        
                        c_dato = dtResul.Rows[n_row]["c_nombre"].ToString();
                        FgDocFin.SetData(FgDocFin.Rows.Count - 1, 6, c_dato);
                        
                        c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_importe"]).ToString("0.00");
                        FgDocFin.SetData(FgDocFin.Rows.Count - 1, 7, c_dato);

                        double n_valor = Convert.ToDouble(dtResul.Rows[n_row]["n_saldo"]);

                        if (n_idmon == 115)                                                     // SI ES SOLES
                        {
                            c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_saldo"]).ToString("0.00");
                            FgDocFin.SetData(FgDocFin.Rows.Count - 1, 8, c_dato);
                            
                            c_dato = (n_valor / n_tc).ToString("0.00");
                            FgDocFin.SetData(FgDocFin.Rows.Count - 1, 9, c_dato);
                        }
                        else                                                                    // SI ES DOLARES
                        {
                            c_dato = (n_valor * n_tc).ToString("0.00");
                            FgDocFin.SetData(FgDocFin.Rows.Count - 1, 8, c_dato);

                            c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_saldo"]).ToString("0.00");
                            FgDocFin.SetData(FgDocFin.Rows.Count - 1, 9, c_dato);
                        }
  
                        c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_id"]).ToString();
                        FgDocFin.SetData(FgDocFin.Rows.Count - 1, 10, c_dato);

                        //c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_idmon"]).ToString();
                        //FgDocFin.SetData(FgDocFin.Rows.Count - 1, 11, c_dato);

                        //c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_idtipdoc"]).ToString();
                        //FgDocFin.SetData(FgDocFin.Rows.Count - 1, 12, c_dato);

                        //c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_idlib"]).ToString();
                        //FgDocFin.SetData(FgDocFin.Rows.Count - 1, 13, c_dato);
                    }
                }
                SumarColDocumentos();
            }
        }
        void SumarColDocumentos()
        {
            TxtImpSol.Text = funFlex.FlexSumarCol(FgDocFin, 8, 2, FgDocFin.Rows.Count - 1).ToString("0.00");
            TxtImpDol.Text = funFlex.FlexSumarCol(FgDocFin, 9, 2, FgDocFin.Rows.Count - 1).ToString("0.00") ;
        }
        private void CmdDelDoc_Click(object sender, EventArgs e)
        {
            if (FgDocFin.Rows.Count == 2) { return; }
            FgDocFin.RemoveItem(FgDocFin.Row);
            SumarColDocumentos();
        }
        private void TxtFchEmi_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtFchIni_KeyPress(object sender, KeyPressEventArgs e)
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
        private void CboMoneda_KeyPress(object sender, KeyPressEventArgs e)
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
        private void CboTipInt_KeyPress(object sender, KeyPressEventArgs e)
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
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void TxtFchIni_ValueChanged(object sender, EventArgs e)
        {

        }
        private void TxtFchIni_Validated(object sender, EventArgs e)
        {
            if (STU_SISTEMA.MESTRABAJO == 0)
            {
                DataTable dttctem = new DataTable();
                o_TC.mysConec = mysConec;
                int n_ano = Convert.ToDateTime(TxtFchIni.Text).Year;
                dttctem = o_TC.Listartcano(151, n_ano.ToString());
                LblTc.Text = objFunciones.ObtenerTC(dttctem, TxtFchIni.Text).ToString("0.000");
            }
            else
            {
                LblTc.Text = objFunciones.ObtenerTC(dtTC, TxtFchIni.Text).ToString("0.000");
            }
        }
        private void TxtTasa_Validated(object sender, EventArgs e)
        {
            if (TxtTasa.Text == "") {return;}
            TxtTasa.Text =Convert.ToDouble(funFunciones.NulosN(TxtTasa.Text)).ToString("0.00");
        }
        private void TxtPortes_Validated(object sender, EventArgs e)
        {
            if (TxtPortes.Text == "") { return; }
            TxtPortes.Text = Convert.ToDouble(funFunciones.NulosN(TxtPortes.Text)).ToString("0.00");
        }
        private void TxtImpFin_Validated(object sender, EventArgs e)
        {
            if (TxtImpFin.Text == "") { return; }
            TxtImpFin.Text = Convert.ToDouble(funFunciones.NulosN(TxtImpFin.Text)).ToString("0.00");
        }
        private void TxtDiaPla_Validated(object sender, EventArgs e)
        {
            if (TxtDiaPla.Text == "") { return; }
            TxtDiaPla.Text = Convert.ToDouble(funFunciones.NulosN(TxtDiaPla.Text)).ToString("");
        }
        private void TxtInterval_Validated(object sender, EventArgs e)
        {
            if (TxtInterval.Text == "") { return; }
            TxtInterval.Text = Convert.ToDouble(funFunciones.NulosN(TxtInterval.Text)).ToString("");
        }
        private void TxtNumLet_Validated(object sender, EventArgs e)
        {
            if (TxtNumLet.Text == "") { return; }
            TxtNumLet.Text = Convert.ToDouble(funFunciones.NulosN(TxtNumLet.Text)).ToString("");
        }

        private void CmdGenLet_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(funFunciones.NulosN(TxtImpFin.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado el importe a financiar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtImpFin.Focus();
                return;
            }
            if (Convert.ToDouble(funFunciones.NulosN(TxtDiaPla.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado los dias de plazo !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtDiaPla.Focus();
                return;
            }
            if (Convert.ToDouble(funFunciones.NulosN(TxtInterval.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado el intervalo de dias entra cada letra !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtInterval.Focus();
                return;
            }
            if (Convert.ToDouble(funFunciones.NulosN(TxtNumLet.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado el numero de letras a emitir !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumLet.Focus();
                return;
            }
            
            GenerarLetras();
        }
        void GenerarLetras()
        {
            int n_row = 0;
            double n_mumdiapla = Convert.ToDouble(TxtDiaPla.Text);
            double n_intervalos = Convert.ToDouble(TxtInterval.Text);
            double n_numlet = Convert.ToDouble(TxtNumLet.Text);
            DateTime d_fchini = Convert.ToDateTime(TxtFchIni.Text).AddDays(n_mumdiapla);
            string c_numdoc = "";
            int n_numdoc = 0;
            double n_capital = Convert.ToDouble(TxtImpSol.Text);
            double n_cuota = (n_capital/n_numlet);
            
            FgLet.Rows.Count = 2;
            double n_plazo = n_intervalos;
            objTipDoc.mysConec = mysConec;
            n_numdoc = Convert.ToInt32(objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 92, "0001"));

            for (n_row = 0; n_row <= n_numlet - 1; n_row++)
            { 
                FgLet.Rows.Count = FgLet.Rows.Count+1;

                FgLet.SetData(FgLet.Rows.Count - 1, 1, n_numdoc.ToString("00000000"));
                FgLet.SetData(FgLet.Rows.Count-1, 2, n_plazo.ToString());
                FgLet.SetData(FgLet.Rows.Count-1, 3, n_cuota.ToString("0.00"));
                FgLet.SetData(FgLet.Rows.Count-1, 4, "0.00");
                FgLet.SetData(FgLet.Rows.Count-1, 5, "0.00");
                FgLet.SetData(FgLet.Rows.Count-1, 6, "0.00");
                FgLet.SetData(FgLet.Rows.Count-1, 7, n_cuota.ToString("0.00"));
                FgLet.SetData(FgLet.Rows.Count-1, 8, d_fchini.AddDays(n_plazo).ToString("dd/MM/yyyy"));

                n_plazo = n_plazo+n_intervalos;
                n_numdoc = n_numdoc + 1;
            }

            //today.AddDays(36)
        }

        private void CboMoneda_SelectedValueChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }
            funDatos.ComboBoxCargarDataTable(CboBanco, dtBancos, "n_id", "c_des");

            DataTable dtresult = new DataTable();
            dtresult = funDatos.DataTableFiltrar(dtCueBan, "n_idmon =" + Convert.ToInt16(CboMoneda.SelectedValue).ToString() + "");
            if (dtresult.Rows.Count != 0)
            { 
                funDatos.ComboBoxCargarDataTable(CboNumCue, dtCueBan, "n_id", "c_numcue");
            }
        }

        private void CboBanco_SelectedValueChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            
            DataTable dtresult = new DataTable();
            dtresult = funDatos.DataTableFiltrar(dtCueBan, "n_idban =" + Convert.ToInt16(CboBanco.SelectedValue).ToString() + " AND  n_idmon = " + Convert.ToInt16(CboMoneda.SelectedValue).ToString() + "");
            if (dtresult.Rows.Count != 0)
            {
                funDatos.ComboBoxCargarDataTable(CboNumCue, dtresult, "n_id", "c_numcue");
            }

            //MostrarCuentaBanco();
        }
        void MostrarCuentaBanco()
        {
            //CboCueBan = null;
            if (Convert.ToInt16(CboBanco.SelectedValue) == 0) { return; }
            if (Convert.ToInt16(CboMoneda.SelectedValue) == 0) { return; }

            DataTable dtResul = new DataTable();
            int n_idmon = Convert.ToInt16(CboMoneda.SelectedValue);
            int n_idban = Convert.ToInt16(CboBanco.SelectedValue);
            dtResul = funDatos.DataTableFiltrar(dtCueBan, "(n_idmon = " + n_idmon.ToString() + " AND n_idban = " + n_idban.ToString() + ")");
            funDatos.ComboBoxCargarDataTable(CboNumCue, dtResul, "n_id", "c_numcue");
            //funDatos.ComboBoxCargarDataTable(CboMoneda, dtMon, "n_id", "c_des");
        }

        private void TxtDiaPla_TextChanged(object sender, EventArgs e)
        {

        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            if (booAgregando == true) { return; }

            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue));    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtLista = objRegistros.dtLista;
            LblNumReg.Text = (dtLista.Rows.Count).ToString();
            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
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
        void MostrarEstadoMes(int n_IdEmpresa, int n_IdMes)
        {
            obj_fungen.mysConec = mysConec;
            if (obj_fungen.EstadoPeriodo(n_IdEmpresa, n_IdMes, 5) == true)
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

        private void emitirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_tes_letras o_tes = new CN_tes_letras();
            o_tes.STU_SISTEMA = STU_SISTEMA;
            int n_IdReg = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            o_tes.ReportImprimirLetras(n_IdReg);
        }

        private void FgDocFin_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3) { return; }
            //if (b_Agregando == true) { return; }
            if (FgDocFin.Col == 7)
            {
                double n_valor = Convert.ToDouble(FgDocFin.GetData(FgDocFin.Row, 7));
                FgDocFin.SetData(FgDocFin.Row, 8, n_valor.ToString());

                n_valor = (n_valor/ Convert.ToDouble(LblTc.Text));
                FgDocFin.SetData(FgDocFin.Row, 9, n_valor.ToString());
            }
        }

        private void FgDocFin_Enter(object sender, EventArgs e)
        {
            
        }

        private void FgDocFin_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            //if (b_Agregando == true) { return; }

            if (FgDocFin.Col == 7)
            {
                FgDocFin.AllowEditing = true;
            }
            else
            {
                FgDocFin.AllowEditing = false;
            }
        }

        private void CmdDelLet_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            FgLet.RemoveItem(FgLet.Row);
        }
    }
}
