using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Tesoreria;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Logistica;
using SIAC_Entidades.Contabilidad;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Logistica;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Tesoreria;
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

namespace SSF_NET_Tesoreria.Formularios
{
    public partial class FrmManEgresos : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_tes_tesoreria objRegistros = new CN_tes_tesoreria();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_tes_destino objDes = new CN_tes_destino();
        CN_tes_origen objOri = new CN_tes_origen();
        CN_sun_tipmon objMon = new CN_sun_tipmon();
        CN_tes_mediopago objMedPag = new CN_tes_mediopago();
        CN_con_tc objTC = new CN_con_tc();
        CN_mae_clipro objCliPro = new CN_mae_clipro();
        //CN_vta_ventas objVentas = new CN_vta_ventas();
        CN_log_compras objCompras = new CN_log_compras();
        CN_tes_documentos objTesDoc = new CN_tes_documentos();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_mae_meses objMes = new CN_mae_meses();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        DatosMySql FunMysql = new DatosMySql();

        DataTable dtLista = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtPlaCue = new DataTable();
        DataTable dtMon = new DataTable();
        DataTable dtOri = new DataTable();
        DataTable dtDes = new DataTable();
        DataTable dtMedPag = new DataTable();
        DataTable dtTc = new DataTable();
        DataTable dtCliPro = new DataTable();
        DataTable dtTesDoc = new DataTable();
        DataTable dtTipDoc = new DataTable();
        DataTable dtMes = new DataTable();

        List<BE_TES_TESORERIAORI> l_TesOri = new List<BE_TES_TESORERIAORI>();
        List<BE_TES_TESORERIAORIDET> l_TesOriDet = new List<BE_TES_TESORERIAORIDET>();
        List<BE_TES_TESORERIADES> l_TesDes = new List<BE_TES_TESORERIADES>();
        List<BE_TES_TESORERIADESDET> l_TesDesDet = new List<BE_TES_TESORERIADESDET>();

        bool booAgregando;
        //bool b_editandovalor = false;

        BE_TES_TESORERIA e_Tesoreria = new BE_TES_TESORERIA();
        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[10, 5];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[6, 5];
        string[,] arrCabeceraFlex2 = new string[6, 5];
        string[,] arrCabeceraFlex3 = new string[15, 5];
        string[,] arrCabeceraFlex4 = new string[8, 5];
        bool booSeEjecuto = false;
        bool b_chequeanulado = false;
        bool b_Agregando = false;
        string c_Numerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string c_Caracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManEgresos()
        {
            InitializeComponent();
        }

        private void FrmManEgresos_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();
            //booAgregando = true;
            funDatos.ComboBoxCargarDataTable(CboMon, dtMon, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMes, "n_id", "c_des");
            //booAgregando = false;
        }
        void ConfigurarFormulario()
        {
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
            this.Text = dtForm.Rows[0]["c_titfor"].ToString();

            arrCabeceraFlex1[0, 0] = "Concepto";
            arrCabeceraFlex1[0, 1] = "420";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "";

            arrCabeceraFlex1[1, 0] = "TC";
            arrCabeceraFlex1[1, 1] = "40";
            arrCabeceraFlex1[1, 2] = "D";
            arrCabeceraFlex1[1, 3] = "0.000";
            arrCabeceraFlex1[1, 4] = "";

            arrCabeceraFlex1[2, 0] = "Importe S/.";
            arrCabeceraFlex1[2, 1] = "80";
            arrCabeceraFlex1[2, 2] = "D";
            arrCabeceraFlex1[2, 3] = "0.0000";
            arrCabeceraFlex1[2, 4] = "";

            arrCabeceraFlex1[3, 0] = "Importe US $";
            arrCabeceraFlex1[3, 1] = "80";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "0.000000";
            arrCabeceraFlex1[3, 4] = "";

            arrCabeceraFlex1[4, 0] = "IdConcepto";
            arrCabeceraFlex1[4, 1] = "0";
            arrCabeceraFlex1[4, 2] = "N";
            arrCabeceraFlex1[4, 3] = "";
            arrCabeceraFlex1[4, 4] = "";

            arrCabeceraFlex1[5, 0] = "Detalla";
            arrCabeceraFlex1[5, 1] = "0";
            arrCabeceraFlex1[5, 2] = "N";
            arrCabeceraFlex1[5, 3] = "";
            arrCabeceraFlex1[5, 4] = "";

            funFlex.FlexMostrarDatos(FgOriIng, arrCabeceraFlex1, dtLista, 2, false);
            FgOriIng.Rows.Count = 8;

            arrCabeceraFlex2[0, 0] = "Concepto";
            arrCabeceraFlex2[0, 1] = "420";
            arrCabeceraFlex2[0, 2] = "C";
            arrCabeceraFlex2[0, 3] = "";
            arrCabeceraFlex2[0, 4] = "";

            arrCabeceraFlex2[1, 0] = "TC";
            arrCabeceraFlex2[1, 1] = "40";
            arrCabeceraFlex2[1, 2] = "D";
            arrCabeceraFlex2[1, 3] = "0.000";
            arrCabeceraFlex2[1, 4] = "";

            arrCabeceraFlex2[2, 0] = "Importe S/.";
            arrCabeceraFlex2[2, 1] = "80";
            arrCabeceraFlex2[2, 2] = "D";
            arrCabeceraFlex2[2, 3] = "0.0000";
            arrCabeceraFlex2[2, 4] = "";

            arrCabeceraFlex2[3, 0] = "Importe US $";
            arrCabeceraFlex2[3, 1] = "80";
            arrCabeceraFlex2[3, 2] = "D";
            arrCabeceraFlex2[3, 3] = "0.000000";
            arrCabeceraFlex2[3, 4] = "";

            arrCabeceraFlex2[4, 0] = "IdConcepto";
            arrCabeceraFlex2[4, 1] = "0";
            arrCabeceraFlex2[4, 2] = "N";
            arrCabeceraFlex2[4, 3] = "";
            arrCabeceraFlex2[4, 4] = "";

            arrCabeceraFlex2[5, 0] = "Detalla";
            arrCabeceraFlex2[5, 1] = "0";
            arrCabeceraFlex2[5, 2] = "N";
            arrCabeceraFlex2[5, 3] = "";
            arrCabeceraFlex2[5, 4] = "";

            funFlex.FlexMostrarDatos(FgDesIng, arrCabeceraFlex2, dtLista, 2, false);
            FgDesIng.Rows.Count = 8;

            arrCabeceraFlex3[0, 0] = "Cliente";
            arrCabeceraFlex3[0, 1] = "200";
            arrCabeceraFlex3[0, 2] = "C";
            arrCabeceraFlex3[0, 3] = "";
            arrCabeceraFlex3[0, 4] = "";

            arrCabeceraFlex3[1, 0] = "T.D.";
            arrCabeceraFlex3[1, 1] = "30";
            arrCabeceraFlex3[1, 2] = "C";
            arrCabeceraFlex3[1, 3] = "";
            arrCabeceraFlex3[1, 4] = "";

            arrCabeceraFlex3[2, 0] = "Fch. Emi.";
            arrCabeceraFlex3[2, 1] = "70";
            arrCabeceraFlex3[2, 2] = "F";
            arrCabeceraFlex3[2, 3] = "dd/MM/yyyy";
            arrCabeceraFlex3[2, 4] = "";

            arrCabeceraFlex3[3, 0] = "Moneda";
            arrCabeceraFlex3[3, 1] = "40";
            arrCabeceraFlex3[3, 2] = "C";
            arrCabeceraFlex3[3, 3] = "";
            arrCabeceraFlex3[3, 4] = "";

            arrCabeceraFlex3[4, 0] = "Nº Documento";
            arrCabeceraFlex3[4, 1] = "100";
            arrCabeceraFlex3[4, 2] = "C";
            arrCabeceraFlex3[4, 3] = "0.00";
            arrCabeceraFlex3[4, 4] = "";

            arrCabeceraFlex3[5, 0] = "Importe";
            arrCabeceraFlex3[5, 1] = "75";
            arrCabeceraFlex3[5, 2] = "D";
            arrCabeceraFlex3[5, 3] = "0.00";
            arrCabeceraFlex3[5, 4] = "";

            arrCabeceraFlex3[6, 0] = "Saldo";
            arrCabeceraFlex3[6, 1] = "75";
            arrCabeceraFlex3[6, 2] = "D";
            arrCabeceraFlex3[6, 3] = "0.00";
            arrCabeceraFlex3[6, 4] = "";

            arrCabeceraFlex3[7, 0] = "Saldo M.A.";
            arrCabeceraFlex3[7, 1] = "75";
            arrCabeceraFlex3[7, 2] = "D";
            arrCabeceraFlex3[7, 3] = "0.00";
            arrCabeceraFlex3[7, 4] = "";

            arrCabeceraFlex3[8, 0] = "Acuenta";
            arrCabeceraFlex3[8, 1] = "75";
            arrCabeceraFlex3[8, 2] = "D";
            arrCabeceraFlex3[8, 3] = "0.00";
            arrCabeceraFlex3[8, 4] = "";

            arrCabeceraFlex3[9, 0] = "Nuevo Saldo";
            arrCabeceraFlex3[9, 1] = "75";
            arrCabeceraFlex3[9, 2] = "D";
            arrCabeceraFlex3[9, 3] = "0.00";
            arrCabeceraFlex3[9, 4] = "";

            arrCabeceraFlex3[10, 0] = "IdDoc";
            arrCabeceraFlex3[10, 1] = "0";
            arrCabeceraFlex3[10, 2] = "N";
            arrCabeceraFlex3[10, 3] = "0";
            arrCabeceraFlex3[10, 4] = "n_id";

            arrCabeceraFlex3[11, 0] = "IdMon";
            arrCabeceraFlex3[11, 1] = "0";
            arrCabeceraFlex3[11, 2] = "N";
            arrCabeceraFlex3[11, 3] = "0";
            arrCabeceraFlex3[11, 4] = "n_idmon";

            arrCabeceraFlex3[12, 0] = "nCorr";
            arrCabeceraFlex3[12, 1] = "0";
            arrCabeceraFlex3[12, 2] = "N";
            arrCabeceraFlex3[12, 3] = "0";
            arrCabeceraFlex3[12, 4] = "n_cor";

            arrCabeceraFlex3[13, 0] = "n_TipoDocumento";
            arrCabeceraFlex3[13, 1] = "0";
            arrCabeceraFlex3[13, 2] = "N";
            arrCabeceraFlex3[13, 3] = "0";
            arrCabeceraFlex3[13, 4] = "n_cor";

            arrCabeceraFlex3[14, 0] = "n_idlib";
            arrCabeceraFlex3[14, 1] = "0";
            arrCabeceraFlex3[14, 2] = "N";
            arrCabeceraFlex3[14, 3] = "0";
            arrCabeceraFlex3[14, 4] = "n_cor";

            funFlex.FlexMostrarDatos(fgDocCli, arrCabeceraFlex3, dtLista, 2, false);
            fgDocCli.Rows.Count = 8;


            //FgDocOri
            arrCabeceraFlex4[0, 0] = "Medio Pago";
            arrCabeceraFlex4[0, 1] = "400";
            arrCabeceraFlex4[0, 2] = "C";
            arrCabeceraFlex4[0, 3] = "";
            arrCabeceraFlex4[0, 4] = "";

            arrCabeceraFlex4[1, 0] = "T.D.";
            arrCabeceraFlex4[1, 1] = "50";
            arrCabeceraFlex4[1, 2] = "C";
            arrCabeceraFlex4[1, 3] = "";
            arrCabeceraFlex4[1, 4] = "";

            arrCabeceraFlex4[2, 0] = "Moneda";
            arrCabeceraFlex4[2, 1] = "40";
            arrCabeceraFlex4[2, 2] = "C";
            arrCabeceraFlex4[2, 3] = "";
            arrCabeceraFlex4[2, 4] = "";

            arrCabeceraFlex4[3, 0] = "Nº Documento";
            arrCabeceraFlex4[3, 1] = "100";
            arrCabeceraFlex4[3, 2] = "C";
            arrCabeceraFlex4[3, 3] = "";
            arrCabeceraFlex4[3, 4] = "";

            arrCabeceraFlex4[4, 0] = "Importe";
            arrCabeceraFlex4[4, 1] = "80";
            arrCabeceraFlex4[4, 2] = "D";
            arrCabeceraFlex4[4, 3] = "0.00";
            arrCabeceraFlex4[4, 4] = "";

            arrCabeceraFlex4[5, 0] = "n_IdTipMedPad";
            arrCabeceraFlex4[5, 1] = "0";
            arrCabeceraFlex4[5, 2] = "N";
            arrCabeceraFlex4[5, 3] = "0";
            arrCabeceraFlex4[5, 4] = "";

            arrCabeceraFlex4[6, 0] = "n_IdTipDoc";
            arrCabeceraFlex4[6, 1] = "0";
            arrCabeceraFlex4[6, 2] = "N";
            arrCabeceraFlex4[6, 3] = "0";
            arrCabeceraFlex4[6, 4] = "";

            arrCabeceraFlex4[7, 0] = "n_Corr";
            arrCabeceraFlex4[7, 1] = "0";
            arrCabeceraFlex4[7, 2] = "N";
            arrCabeceraFlex4[7, 3] = "0";
            arrCabeceraFlex4[7, 4] = "";

            funFlex.FlexMostrarDatos(FgDocOri, arrCabeceraFlex4, dtLista, 2, false);
            FgDocOri.Rows.Count = 8;
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 2);
            dtLista = objRegistros.dtLista;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(71, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(71);

            objMon.mysConec = mysConec;
            dtMon = objMon.Listar();

            objOri.mysConec = mysConec;
            objOri.Listar(STU_SISTEMA.EMPRESAID);
            dtOri = objOri.dtOrigen;
            dtOri = funDatos.DataTableFiltrar(dtOri, "n_tipo = 2");

            objDes.mysConec = mysConec;
            objDes.Listar(STU_SISTEMA.EMPRESAID);
            dtDes = objDes.dtDestino;
            dtDes = funDatos.DataTableFiltrar(dtDes, "n_tipo = 2");

            objMedPag.mysConec = mysConec;
            dtMedPag = objMedPag.Listar();

            objTC.mysConec = mysConec;
            objTC.Listar(0, 0);
            dtTc = objTC.dtLista;
            objTC = null;

            objCliPro.mysConec = mysConec;
            dtCliPro = objCliPro.ListarProveedor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objTesDoc.mysConec = mysConec;
            objTesDoc.Listar();
            dtTesDoc = objTesDoc.dtLista;

            objTipDoc.mysConec = mysConec;
            dtTipDoc = objTipDoc.Listar();

            objMes.mysConec = mysConec;
            dtMes = objMes.Listar();
        }
        void ListarItems()
        {
            // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
            objRegistros.mysConec = mysConec;
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue), 2);

            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);

            dtLista = objRegistros.dtLista;

            LblNumReg.Text = (dtLista.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtLista, true);
        }
        void VerRegistro(int n_IdRegistro)
        {
            //string c_dato = "";

            l_TesDes.Clear();
            l_TesDesDet.Clear();
            l_TesOri.Clear();
            l_TesOriDet.Clear();

            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdRegistro);
            e_Tesoreria = objRegistros.e_Tesoreria;
            l_TesDes = objRegistros.l_TesoreriaDes;
            l_TesDesDet = objRegistros.l_TesoreriaDesDet;
            l_TesOri = objRegistros.l_TesoreriaOri;
            l_TesOriDet = objRegistros.l_TesoreriaOriDet;

            TxtFchEmi.Text = Convert.ToDateTime(e_Tesoreria.d_fchope).ToString("dd/MM/yyyy");
            CboMon.SelectedValue = e_Tesoreria.n_idmon;
            TxtGlo.Text = e_Tesoreria.c_glo;
            LblTc.Text = e_Tesoreria.n_tc.ToString("0.000");
            LblNumAsi.Text = e_Tesoreria.c_numreg;
            booAgregando = true;
            FgOriIng.Rows.Count = 2;
            FgDesIng.Rows.Count = 2;

            MostarDetalle();
            booAgregando = false;
        }
        void Nuevo()
        {
            n_QueHace = 1;
            
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            Tab1.SelectedIndex = 1;
            FgOriIng.Cols[1].ComboList = "...";
            FgDesIng.Cols[1].ComboList = "...";

            FgOriIng.Rows.Count = 3;
            FgDesIng.Rows.Count = 3;
            CboMon.SelectedValue = 115;
            TxtFchEmi.Focus();
        }
        void Blanquea()
        {
            TxtFchEmi.Text = "";
            CboMon.SelectedValue = 0;
            TxtGlo.Text = "";
            LblTc.Text = "";
            LblNumAsi.Text = "";

            booAgregando = true;
            FgOriIng.Rows.Count = 2;
            FgDesIng.Rows.Count = 2;

            FgOriIng.Rows.Count = FgOriIng.Rows.Count + 1;
            FgDesIng.Rows.Count = FgDesIng.Rows.Count + 1;

            l_TesOri.Clear();
            l_TesOriDet.Clear();
            l_TesDes.Clear();
            l_TesDesDet.Clear();

            booAgregando = false;
            LblDebTotSol.Text = "";
            LblDebTotDol.Text = "";
            LblHabTotSol.Text = "";
            LblHabTotDol.Text = "";
        }
        void Bloquea()
        {
            TxtFchEmi.Enabled = !TxtFchEmi.Enabled;
            CboMon.Enabled = !CboMon.Enabled;
            TxtGlo.Enabled = !TxtGlo.Enabled;
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
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir.Enabled = !ToolImprimir.Enabled;
            ToolExportarExcel.Enabled = !ToolExportarExcel.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
        }
        void Modificar()
        {
            int n_dongen = Convert.ToInt32(DgLista.Columns["n_dongen"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            if (n_dongen == 2)
            {
                MessageBox.Show("El registro que desea modificar no fue generado en el modulo de egresos, no se puede modificar este registro", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            FgOriIng.Cols[1].ComboList = "...";
            FgDesIng.Cols[1].ComboList = "...";
            Tab1.SelectedIndex = 1;
            TxtFchEmi.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            int n_dongen = Convert.ToInt32(DgLista.Columns["n_dongen"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            if (n_dongen == 2)
            {
                MessageBox.Show("El registro que desea eliminar no fue generado en el modulo de egresos, no se puede eliminar este registro", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }
            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(n_IdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

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
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(e_Tesoreria, l_TesOri, l_TesOriDet, l_TesDes, l_TesDesDet, 2);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(e_Tesoreria, l_TesOri, l_TesOriDet, l_TesDes, l_TesDesDet, 2);
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
            if (n_QueHace == 1)
            {
                e_Tesoreria.n_id = 0;
            }
            else
            {
                e_Tesoreria.n_id = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            }

            e_Tesoreria.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Tesoreria.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_Tesoreria.n_mes = Convert.ToInt32(CboMeses.SelectedValue);
            e_Tesoreria.n_idlib = 1;
            e_Tesoreria.c_numreg = "";
            e_Tesoreria.d_fchope = Convert.ToDateTime(TxtFchEmi.Text);
            e_Tesoreria.n_idmon = Convert.ToInt32(CboMon.SelectedValue);
            e_Tesoreria.c_glo = TxtGlo.Text;
            e_Tesoreria.n_conciliado = 0;
            e_Tesoreria.n_tc = Convert.ToDouble(LblTc.Text);
            e_Tesoreria.n_tipreg = 2;
            e_Tesoreria.n_dongen = 1;
            e_Tesoreria.c_numreg = LblNumAsi.Text;
            l_TesOri.Clear();
            l_TesDes.Clear();

            for (n_row = 2; n_row <= FgOriIng.Rows.Count - 1; n_row++)
            {
                BE_TES_TESORERIAORI e_TesOri = new BE_TES_TESORERIAORI();
                e_TesOri.n_idtes = 0;
                e_TesOri.n_idori = Convert.ToInt32(FgOriIng.GetData(n_row, 5));
                if (Convert.ToInt32(CboMon.SelectedValue) == 115)
                {
                    e_TesOri.n_imp = Convert.ToDouble(FgOriIng.GetData(n_row, 3));
                }
                else
                {
                    e_TesOri.n_imp = Convert.ToDouble(FgOriIng.GetData(n_row, 4));
                }
                e_TesOri.n_idmod = 0;
                e_TesOri.n_idbcocta = 0;
                e_TesOri.n_tc = Convert.ToDouble(LblTc.Text);

                l_TesOri.Add(e_TesOri);
            }

            // *********************
            // CARGAMOS LOS DESTINOS
            for (n_row = 2; n_row <= FgDesIng.Rows.Count - 1; n_row++)
            {
                BE_TES_TESORERIADES e_TesDes = new BE_TES_TESORERIADES();
                e_TesDes.n_idtes = 0;
                e_TesDes.n_iddes = Convert.ToInt32(FgDesIng.GetData(n_row, 5));
                if (Convert.ToInt32(CboMon.SelectedValue) == 115)
                {
                    e_TesDes.n_imp = Convert.ToDouble(FgDesIng.GetData(n_row, 3));
                }
                else
                {
                    e_TesDes.n_imp = Convert.ToDouble(FgDesIng.GetData(n_row, 4));
                }
                e_TesDes.n_idmod = 0;
                e_TesDes.n_idbcocta = 0;
                e_TesDes.n_tc = Convert.ToDouble(LblTc.Text);

                l_TesDes.Add(e_TesDes);
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToDateTime(TxtFchEmi.Text).Month != Convert.ToInt32(CboMeses.SelectedValue))
            {
                MessageBox.Show("¡ La fecha de la operacion no coincide con el mes de registro !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchEmi.Focus();
                return booEstado;
            }
            if (TxtFchEmi.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de emision !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchEmi.Focus();
                return booEstado;
            }
            if (Convert.ToInt32(CboMon.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMon.Focus();
                return booEstado;
            }
            if (Convert.ToDouble(funFunciones.NulosN(LblTc.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de cambio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMon.Focus();
                return booEstado;
            }
            if (b_chequeanulado == false)
            { 
                //if (Convert.ToDouble(funFunciones.NulosN(LblDebTotSol.Text)) == 0)
                //{
                //    MessageBox.Show("¡ EL importe debe en soles no puede ser 0 !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //    booEstado = false;
                //    FgOriIng.Focus();
                //    return booEstado;
                //}
                //if (Convert.ToDouble(funFunciones.NulosN(LblHabTotSol.Text)) == 0)
                //{
                //    MessageBox.Show("¡ EL importe haber en soles no puede ser 0 !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //    booEstado = false;
                //    FgOriIng.Focus();
                //    return booEstado;
                //}
                //if (Convert.ToDouble(funFunciones.NulosN(LblDebTotDol.Text)) == 0)
                //{
                //    MessageBox.Show("¡ EL importe debe en dolares no puede ser 0 !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //    booEstado = false;
                //    FgOriIng.Focus();
                //    return booEstado;
                //}
                //if (Convert.ToDouble(funFunciones.NulosN(LblHabTotDol.Text)) == 0)
                //{
                //    MessageBox.Show("¡ EL importe haber en dolares no puede ser 0 !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //    booEstado = false;
                //    FgOriIng.Focus();
                //    return booEstado;
                //}

                if (Genericas.Round(Convert.ToDouble(LblDebTotSol.Text), 4) != Genericas.Round(Convert.ToDouble(LblHabTotSol.Text), 4))
                {
                    MessageBox.Show("¡ EL importe debe en soles no corresponder al importe haber en soles !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    FgOriIng.Focus();
                    return booEstado;
                }
                if (Genericas.Round(Convert.ToDouble(LblDebTotDol.Text), 4) != Genericas.Round(Convert.ToDouble(LblHabTotDol.Text), 4))
                {
                    MessageBox.Show("¡ EL importe debe en dolares no corresponder al importe haber en dolares !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    FgOriIng.Focus();
                    return booEstado;
                }
            }
            return booEstado;
        }

        private void FrmManEgresos_Activated(object sender, EventArgs e)
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
                // dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
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

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            VerRegistro(intIdRegistro);
        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            if (booAgregando == true) { return; }

            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue), 2);    // CARGAMOS LOS DATOS DEL FORMULARIO
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

        private void TxtFchEmi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
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
                    LblTc.Text = dtResul.Rows[0]["n_impven"].ToString();

                    //booAgregando = true;
                    //FgOriIng.Rows.Count = 2;
                    //FgDesIng.Rows.Count = 2;

                    //FgOriIng.Rows.Count = FgOriIng.Rows.Count + 1;
                    //FgDesIng.Rows.Count = FgDesIng.Rows.Count + 1;

                    //l_TesOri.Clear();
                    //l_TesOriDet.Clear();
                    //l_TesDes.Clear();
                    //l_TesDesDet.Clear();

                    //booAgregando = false;
                }
                else
                {
                    LblTc.Text = "";
                }
            }
        }

        private void CboMon_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CmdAddOri_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            FgOriIng.Rows.Count = FgOriIng.Rows.Count + 1;
        }

        private void CmdDelOri_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (FgOriIng.Rows.Count == 2) { return; }

            int n_row = 0;
            booAgregando = true;
            int n_idori = Convert.ToInt32(FgOriIng.GetData(FgOriIng.Row, 5));
            // ELIMINAMOS EL ORIGEN
            FgOriIng.RemoveItem(FgOriIng.Row);

            // ELIMINAMOS EL DETALLE DEL ORIGEN
            for (n_row = 0; n_row <= l_TesOriDet.Count - 1; n_row++)
            {
                if (l_TesOriDet[n_row].n_idori == n_idori)
                {
                    l_TesOriDet.RemoveAt(n_row);
                    n_row = n_row - 1;
                }
            }
            booAgregando = false ;
            SumarTotOrigen();
        }

        private void CmdAddDes_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            FgDesIng.Rows.Count = FgDesIng.Rows.Count + 1;
        }

        private void CmdDelDes_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (FgDesIng.Rows.Count == 2) { return; }

            booAgregando = true;
            int n_row = 0;
            int n_iddes = Convert.ToInt32(FgDesIng.GetData(FgDesIng.Row, 5));
            // ELIMINAMOS EL ORIGEN
            FgDesIng.RemoveItem(FgDesIng.Row);

            // ELIMINAMOS EL DETALLE DEL ORIGEN
            for (n_row = 0; n_row <= l_TesDesDet.Count - 1; n_row++)
            {
                if (l_TesDesDet[n_row].n_iddes == n_iddes)
                {
                    l_TesDesDet.RemoveAt(n_row);
                    n_row = n_row - 1;
                }
            }
            booAgregando = false;
            SumarTotDestino();
        }

        private void CmdVerAsiento_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objConta = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objConta.mysConec = mysConec;
            objConta.STU_SISTEMA = STU_SISTEMA;
            objConta.VerAsiento(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue), 1, LblNumAsi.Text);
            objConta = null;
        }

        private void FgOriIng_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (booAgregando == true) { return; }

            if (LblTc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el tipo de cambio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchEmi.Focus();
                return;
            }

            if (FgOriIng.Col == 1)
            {
                string c_dato = "";
                DataTable dtResul = new DataTable();

                b_Agregando = true;
                //c_dato = DocumentosAdicionados();
                dtResul = funDatos.DataTableFiltrar(dtOri, "n_idmon = " + Convert.ToInt32(CboMon.SelectedValue) + "");
                if (b_chequeanulado == true)
                {
                    dtResul = funDatos.DataTableFiltrar(dtResul, "n_idbanco >= 1");
                }

                dtResul = objOri.BuscarOrigen(dtResul);
                if (dtResul != null)
                {
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["c_des"].ToString();           // MOSTRAMOS LA DESCRIPCION DEL ORIGEN
                        FgOriIng.SetData(FgOriIng.Row, 1, c_dato);

                        c_dato = LblTc.Text;                                    // MOSTRAMOS LA DESCRIPCION DEL ORIGEN
                        FgOriIng.SetData(FgOriIng.Row, 2, c_dato);

                        c_dato = dtResul.Rows[0]["n_id"].ToString();            // MOSTRAMOS EL ID DEL ORIGEN
                        FgOriIng.SetData(FgOriIng.Row, 5, c_dato);

                        c_dato = dtResul.Rows[0]["n_detalla"].ToString();
                        FgOriIng.SetData(FgOriIng.Row, 6, c_dato);
                        if (c_dato == "1")
                        {
                            FgOriIng.Cols[3].ComboList = "...";
                            FgOriIng.Cols[4].ComboList = "...";
                        }
                        else
                        {
                            FgOriIng.Cols[3].ComboList = "";
                            FgOriIng.Cols[4].ComboList = "";
                        }
                    }
                }
                b_Agregando = false;
            }

            if ((FgOriIng.Col == 3) || (FgOriIng.Col == 4))
            {
                MostraPanel6();
            }
        }

        private void FgOriIng_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (booAgregando == true) { return; }
            booAgregando = true;
            if (FgOriIng.Col == 3)
            {
                double n_imp = Convert.ToDouble(funFunciones.NulosN(FgOriIng.GetData(FgOriIng.Row, 3)).ToString());
                double n_tc = Convert.ToDouble(LblTc.Text);
                n_imp = (n_imp / n_tc);
                FgOriIng.SetData(FgOriIng.Row, 4, n_imp.ToString("0.000000"));
            }
            if (FgOriIng.Col == 4)
            {
                double n_imp = Convert.ToDouble(funFunciones.NulosN(FgOriIng.GetData(FgOriIng.Row, 4)).ToString());
                double n_tc = Convert.ToDouble(LblTc.Text);
                n_imp = (n_imp * n_tc);
                FgOriIng.SetData(FgOriIng.Row, 3, n_imp.ToString("0.0000"));
            }
            SumarTotOrigen();
            booAgregando = false;
        }

        private void FgOriIng_EnterCell(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            if (LblTc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el tipo de cambio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchEmi.Focus();
                return;
            }

            string c_dato = "";

            if (FgOriIng.Col == 3)
            {
                if (Convert.ToInt32(CboMon.SelectedValue) == 115)
                {
                    c_dato = funFunciones.NulosC(FgOriIng.GetData(FgOriIng.Row, 5));
                    c_dato = funDatos.DataTableBuscar(dtOri, "n_id", "n_detalla", c_dato, "N").ToString();
                    if (c_dato == "1")
                    {
                        FgOriIng.Cols[3].ComboList = "...";
                        FgOriIng.AllowEditing = true;
                    }
                    else
                    {
                        FgOriIng.Cols[3].ComboList = "";
                        FgOriIng.AllowEditing = true;
                    }
                }
                else
                {
                    FgOriIng.Cols[3].ComboList = "";
                    FgOriIng.AllowEditing = false;
                }
                return;
            }

            if (FgOriIng.Col == 4)
            {
                if (Convert.ToInt32(CboMon.SelectedValue) == 151)
                {
                    c_dato = FgOriIng.GetData(FgOriIng.Row, 5).ToString();
                    c_dato = funDatos.DataTableBuscar(dtOri, "n_id", "n_detalla", c_dato, "N").ToString();
                    if (c_dato == "1")
                    {
                        FgOriIng.Cols[4].ComboList = "...";
                        FgOriIng.AllowEditing = true;
                    }
                    else
                    {
                        FgOriIng.Cols[4].ComboList = "";
                        FgOriIng.AllowEditing = true;
                    }
                }
                else
                {
                    FgOriIng.Cols[4].ComboList = "";
                    FgOriIng.AllowEditing = false;
                }
                return;
            }

            if (n_QueHace == 3) { FgOriIng.AllowEditing = false; return; }

            if (FgOriIng.Col == 1) { FgOriIng.AllowEditing = true; return; }
            if (FgOriIng.Col == 2) { FgOriIng.AllowEditing = false; return; }

            if (Convert.ToInt32(CboMon.SelectedValue) == 115)
            {
                if (FgOriIng.Col == 3) { FgOriIng.AllowEditing = true; return; }
                if (FgOriIng.Col == 4) { FgOriIng.AllowEditing = false; return; }
            }
            else
            {
                if (FgOriIng.Col == 3) { FgOriIng.AllowEditing = false; return; }
                if (FgOriIng.Col == 4) { FgOriIng.AllowEditing = true; return; }
            }


            //c_dato = dtResul.Rows[0]["n_detalla"].ToString();
            //        FgDesIng.SetData(FgDesIng.Row, 6, c_dato);
            //        if (c_dato == "1")
            //        {
            //            FgDesIng.Cols[3].ComboList = "...";
            //            FgDesIng.Cols[4].ComboList = "...";
            //        }
            //        else
            //        {
            //            FgDesIng.Cols[3].ComboList = "";
            //            FgDesIng.Cols[4].ComboList = "";
            //        }
        }

        private void FgOriIng_RowColChange(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            string c_dato = funFunciones.NulosC(FgOriIng.GetData(FgOriIng.Row, 6));

            if (c_dato == "1")
            {
                FgOriIng.Cols[3].ComboList = "...";
                FgOriIng.Cols[4].ComboList = "...";
            }
            else
            {
                //FgOriIng.Cols[3].ComboList = "";
                //FgOriIng.Cols[4].ComboList = "";
            }
        }

        private void FgDesIng_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (booAgregando == true) { return; }

            if (LblTc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el tipo de cambio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchEmi.Focus();
                return;
            }

            if (FgDesIng.Col == 1)
            {
                string c_dato = "";
                DataTable dtResul = new DataTable();

                b_Agregando = true;
                //c_dato = DocumentosAdicionados();
                dtResul = funDatos.DataTableFiltrar(dtDes, "n_idmon = " + Convert.ToInt32(CboMon.SelectedValue) + "");

                dtResul = objDes.BuscarDestino(dtResul);
                if (dtResul != null)
                {
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["c_des"].ToString();           // MOSTRAMOS LA DESCRIPCION DEL ORIGEN
                        FgDesIng.SetData(FgDesIng.Row, 1, c_dato);

                        c_dato = LblTc.Text;                                    // MOSTRAMOS LA DESCRIPCION DEL ORIGEN
                        FgDesIng.SetData(FgDesIng.Row, 2, c_dato);

                        c_dato = dtResul.Rows[0]["n_id"].ToString();            // MOSTRAMOS EL ID DEL ORIGEN
                        FgDesIng.SetData(FgDesIng.Row, 5, c_dato);

                        c_dato = dtResul.Rows[0]["n_detalla"].ToString();
                        FgDesIng.SetData(FgDesIng.Row, 6, c_dato);
                        if (c_dato == "1")
                        {
                            FgDesIng.Cols[3].ComboList = "...";
                            FgDesIng.Cols[4].ComboList = "...";
                        }
                        else
                        {
                            FgDesIng.Cols[3].ComboList = "";
                            FgDesIng.Cols[4].ComboList = "";
                        }
                    }
                }
                b_Agregando = false;
            }

            if ((FgDesIng.Col == 3) || (FgDesIng.Col == 4))
            {
                MostraPanel5();
            }
        }

        private void FgDesIng_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (booAgregando == true) { return; }

            booAgregando = true;
            if (FgDesIng.Col == 3)
            {
                double n_imp = Convert.ToDouble(funFunciones.NulosN(FgDesIng.GetData(FgDesIng.Row, 3)).ToString());
                double n_tc = Convert.ToDouble(LblTc.Text);
                n_imp = (n_imp / n_tc);
                FgDesIng.SetData(FgDesIng.Row, 4, n_imp.ToString("0.000000"));
            }
            if (FgDesIng.Col == 4)
            {
                double n_imp = Convert.ToDouble(funFunciones.NulosN(FgDesIng.GetData(FgDesIng.Row, 4)));
                double n_tc = Convert.ToDouble(LblTc.Text);
                n_imp = (n_imp * n_tc);
                FgDesIng.SetData(FgDesIng.Row, 3, n_imp.ToString("0.000000"));
            }
            SumarTotDestino();
            booAgregando = false;
        }

        private void FgDesIng_EnterCell(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            if (LblTc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el tipo de cambio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchEmi.Focus();
                return;
            }

            string c_dato;

            if (FgDesIng.Col == 3)
            {
                if (Convert.ToInt32(CboMon.SelectedValue) == 115)
                {
                    c_dato = funFunciones.NulosC(FgDesIng.GetData(FgDesIng.Row, 5)); // .ToString();
                    c_dato = funDatos.DataTableBuscar(dtDes, "n_id", "n_detalla", c_dato, "N").ToString();
                    if (c_dato == "1")
                    {
                        FgDesIng.Cols[3].ComboList = "...";
                        FgDesIng.AllowEditing = true;
                    }
                    else
                    {
                        FgDesIng.Cols[3].ComboList = "";
                        FgDesIng.AllowEditing = true;
                    }
                }
                else
                {
                    FgDesIng.Cols[3].ComboList = "";
                    FgDesIng.AllowEditing = false;
                }
                return;
            }

            if (FgDesIng.Col == 4)
            {
                if (Convert.ToInt32(CboMon.SelectedValue) == 151)
                {
                    c_dato = FgDesIng.GetData(FgDesIng.Row, 5).ToString();
                    c_dato = funDatos.DataTableBuscar(dtDes, "n_id", "n_detalla", c_dato, "N").ToString();
                    if (c_dato == "1")
                    {
                        FgDesIng.Cols[4].ComboList = "...";
                        FgDesIng.AllowEditing = true;
                    }
                    else
                    {
                        FgDesIng.Cols[4].ComboList = "";
                        FgDesIng.AllowEditing = true;
                    }
                }
                else
                {
                    FgDesIng.Cols[4].ComboList = "";
                    FgDesIng.AllowEditing = false;
                }
                return;
            }

            if (n_QueHace == 3) { FgDesIng.AllowEditing = false; return; }
            if (FgDesIng.Col == 1) { FgDesIng.AllowEditing = true; return; }
            if (FgDesIng.Col == 2) { FgDesIng.AllowEditing = false; return; }

            if (Convert.ToInt32(CboMon.SelectedValue) == 115)
            {
                if (FgDesIng.Col == 3) { FgDesIng.AllowEditing = true; return; }
                if (FgDesIng.Col == 4) { FgDesIng.AllowEditing = false; return; }
            }
            else
            {
                if (FgDesIng.Col == 3) { FgDesIng.AllowEditing = false; return; }
                if (FgDesIng.Col == 4) { FgDesIng.AllowEditing = true; return; }
            }
        }

        private void FgDesIng_RowColChange(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            string c_dato = funFunciones.NulosC(FgDesIng.GetData(FgDesIng.Row, 6));

            if (c_dato == "1")
            {
                FgDesIng.Cols[3].ComboList = "...";
                FgDesIng.Cols[4].ComboList = "...";
            }
            else
            {
                //FgDesIng.Cols[3].ComboList = "";
                //FgDesIng.Cols[4].ComboList = "";
            }
        }

        private void CmdBusCli_Click(object sender, EventArgs e)
        {
            objCliPro.mysConec = mysConec;
            DataTable dtResult = new DataTable();
            objCliPro.mysConec = mysConec;
            dtResult = objCliPro.BuscarCliPro(dtCliPro, 2, "n_id", "");
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    LblidCliente.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtCliente.Text = dtResult.Rows[0]["c_nombre"].ToString();
                }
                else
                {
                    LblidCliente.Text = "";
                    TxtCliente.Text = "";
                }
            }
        }

        private void CmdAddDoc_Click(object sender, EventArgs e)
        {
            //if (TxtCliente.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado el nombre del proveedor !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    CmdBusCli.Focus();
            //    return;
            //}

            string c_dato = "";
            int n_row = 0;
            DataTable dtResul = new DataTable();
            objCompras.mysConec = mysConec;
            if (LblidCliente.Text == "")
            {
                dtResul = objCompras.DocumentosConSaldo(STU_SISTEMA.EMPRESAID, 0);
            }
            if (LblidCliente.Text != "")
            { 
                dtResul = objCompras.DocumentosConSaldo(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblidCliente.Text));
            }
            booAgregando = true;
            if (dtResul != null)
            {
                if (dtResul.Rows.Count != 0)
                {
                    for (n_row = 0; n_row <= dtResul.Rows.Count - 1; n_row++)
                    {
                        fgDocCli.Rows.Count = fgDocCli.Rows.Count + 1;

                        c_dato = dtResul.Rows[n_row]["c_nombre"].ToString();
                        fgDocCli.SetData(fgDocCli.Rows.Count - 1, 1, c_dato);

                        c_dato = dtResul.Rows[n_row]["c_destipdoc"].ToString();
                        fgDocCli.SetData(fgDocCli.Rows.Count - 1, 2, c_dato);

                        c_dato = Convert.ToDateTime(dtResul.Rows[n_row]["d_fchdoc"]).ToString("dd/MM/yyyy");
                        fgDocCli.SetData(fgDocCli.Rows.Count - 1, 3, c_dato);

                        c_dato = dtResul.Rows[n_row]["c_desmon"].ToString();
                        fgDocCli.SetData(fgDocCli.Rows.Count - 1, 4, c_dato);

                        c_dato = dtResul.Rows[n_row]["c_numdoc"].ToString();
                        fgDocCli.SetData(fgDocCli.Rows.Count - 1, 5, c_dato);

                        c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_importe"]).ToString("0.00");
                        fgDocCli.SetData(fgDocCli.Rows.Count - 1, 6, c_dato);

                        c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_saldo"]).ToString("0.00");
                        fgDocCli.SetData(fgDocCli.Rows.Count - 1, 7, c_dato);

                        double n_valor = Convert.ToDouble(dtResul.Rows[n_row]["n_saldo"]);
                        double n_tc = Convert.ToDouble(LblTc.Text);
                        int n_idmon = Convert.ToInt32(dtResul.Rows[n_row]["n_idmon"]);

                        if (Convert.ToInt32(CboMon.SelectedValue) == 115)                       // SI ES SOLES
                        {
                            if (n_idmon == 115)
                            {
                                c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_saldo"]).ToString("0.00");
                                fgDocCli.SetData(fgDocCli.Rows.Count - 1, 8, c_dato);
                            }
                            else
                            {
                                c_dato = (n_valor * n_tc).ToString("0.00");
                                fgDocCli.SetData(fgDocCli.Rows.Count - 1, 8, c_dato);
                            }

                            fgDocCli.SetData(fgDocCli.Rows.Count - 1, 9, c_dato);              // MOSTRAMOS EL ACUENTA DEL DOCUMENTO POR DEFAULT

                        }
                        else                                                                   // SI ES DOLARES
                        {
                            if (n_idmon == 115)
                            {
                                c_dato = (n_valor / n_tc).ToString("0.00");
                                fgDocCli.SetData(fgDocCli.Rows.Count - 1, 8, c_dato);
                            }
                            else
                            {
                                c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_saldo"]).ToString("0.00");
                                fgDocCli.SetData(fgDocCli.Rows.Count - 1, 8, c_dato);
                            }

                            fgDocCli.SetData(fgDocCli.Rows.Count - 1, 9, c_dato);              // MOSTRAMOS EL ACUENTA DEL DOCUMENTO POR DEFAULT
                        }

                        c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_id"]).ToString();
                        fgDocCli.SetData(fgDocCli.Rows.Count - 1, 11, c_dato);

                        c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_idmon"]).ToString();
                        fgDocCli.SetData(fgDocCli.Rows.Count - 1, 12, c_dato);

                        c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_idtipdoc"]).ToString();
                        fgDocCli.SetData(fgDocCli.Rows.Count - 1, 14, c_dato);

                        c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_idlib"]).ToString();
                        fgDocCli.SetData(fgDocCli.Rows.Count - 1, 15, c_dato);
                    }
                }
                SumarColDocumentos();
                booAgregando = false;
            }
        }

        private void CmdDelDoc_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (fgDocCli.Rows.Count == 2) { return; }

            
            int n_fil = 0;
            int n_iddoc = Convert.ToInt32(fgDocCli.GetData(fgDocCli.Row, 11).ToString());
            for (n_fil = 0; n_fil <= l_TesDesDet.Count - 1; n_fil++)
            {
                if (l_TesDesDet[n_fil].n_iddoc == n_iddoc)
                {
                    l_TesDesDet.RemoveAt(n_fil);
                    break;
                }
            }
            fgDocCli.RemoveItem(fgDocCli.Row);
        }

        private void CmdAce_Click(object sender, EventArgs e)
        {
            int n_fil = 0;
            int n_filok = 0;
            for (n_fil = 2; n_fil <= fgDocCli.Rows.Count - 1; n_fil++)
            {
                if (fgDocCli.GetData(n_fil, 1).ToString() != "")
                {
                    if (fgDocCli.GetData(n_fil, 2).ToString() == "")
                    {
                        MessageBox.Show("¡ No ha especificado el tipo de documento en la fila " + n_fil.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    if (fgDocCli.GetData(n_fil, 3).ToString() == "")
                    {
                        MessageBox.Show("¡ No ha especificado la fecha de emision en la fila " + n_fil.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    if (fgDocCli.GetData(n_fil, 5).ToString() == "")
                    {
                        MessageBox.Show("¡ No ha especificado el numero de documento en la fila " + n_fil.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    if (Convert.ToDouble(fgDocCli.GetData(n_fil, 9)) == 0)
                    {
                        MessageBox.Show("¡ No ha especificado el el importe a cuenta en la fila " + n_fil.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    n_filok = n_filok + 1;
                }
            }

            if (n_filok == 0)
            {
                MessageBox.Show("¡ No se ha registrado ningun documento, debe de especificar al menos un documento para continuar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            int n_cor = 0;
            int n_corgrid;
            int n_idmod = Convert.ToInt32(funFunciones.NulosN(funDatos.DataTableBuscar(dtDes, "n_id", "n_idmod", LblIdDes.Text, "N")));

            for (n_fil = 2; n_fil <= fgDocCli.Rows.Count - 1; n_fil++)
            {
                n_corgrid = Convert.ToInt32(funFunciones.NulosN(fgDocCli.GetData(n_fil, 13)).ToString());
                if (BuscarEliminarDestinoDoc(Convert.ToInt32(LblIdDes.Text), n_corgrid) == true)
                {
                    n_cor = n_corgrid;
                }
                else
                {
                    n_cor = n_cor + 1;
                }

                BE_TES_TESORERIADESDET e_DesDet = new BE_TES_TESORERIADESDET();

                e_DesDet.n_idtes = 0;
                e_DesDet.n_iddes = Convert.ToInt32(LblIdDes.Text);
                e_DesDet.n_idtipper = 0;
                e_DesDet.n_idmod = n_idmod;
                e_DesDet.n_iddoc = Convert.ToInt32(fgDocCli.GetData(n_fil, 11)); ;
                e_DesDet.n_idper = 0;
                e_DesDet.n_idtipdoc = Convert.ToInt32(fgDocCli.GetData(n_fil, 14).ToString());
                e_DesDet.c_numser = fgDocCli.GetData(n_fil, 5).ToString().Substring(0, 4); ;
                e_DesDet.c_numdoc = fgDocCli.GetData(n_fil, 5).ToString().Substring(5, 10);
                e_DesDet.n_imp = Convert.ToDouble(fgDocCli.GetData(n_fil, 6));
                e_DesDet.n_sal = Convert.ToDouble(fgDocCli.GetData(n_fil, 7));
                e_DesDet.n_acuenta = Convert.ToDouble(fgDocCli.GetData(n_fil, 9));
                e_DesDet.d_fchdoc = Convert.ToDateTime(fgDocCli.GetData(n_fil, 3));
                e_DesDet.c_glo = "";
                e_DesDet.n_cor = n_cor;
                string c_dato = fgDocCli.GetData(n_fil, 4).ToString();

                e_DesDet.n_idmon = Convert.ToInt32(funDatos.DataTableBuscar(dtMon,"c_simbolo","n_id",c_dato,"C").ToString());
                e_DesDet.n_idlib = Convert.ToInt32(fgDocCli.GetData(n_fil, 15).ToString());
                //e_DesDet.n_idmedpag = Convert.ToInt32(fgDocCli.GetData(n_fil, 6).ToString());

                l_TesDesDet.Add(e_DesDet);
            }

            double n_valor = 0;
            for (n_fil = 0; n_fil <= l_TesDesDet.Count - 1; n_fil++)
            {
                n_valor = (n_valor + l_TesDesDet[n_fil].n_acuenta);
            }

            if (Convert.ToInt32(CboMon.SelectedValue) == 115)
            {
                FgDesIng.SetData(FgDesIng.Row, 3, n_valor.ToString("0.0000"));

                n_valor = (n_valor / Convert.ToDouble(LblTc.Text));
                FgDesIng.SetData(FgDesIng.Row, 4, n_valor.ToString("0.0000"));
            }
            else
            {
                FgDesIng.SetData(FgDesIng.Row, 4, n_valor.ToString("0.0000"));

                n_valor = (n_valor * Convert.ToDouble(LblTc.Text));
                FgDesIng.SetData(FgDesIng.Row, 3, n_valor.ToString("0.0000"));
            }

            CmdCan_Click(sender, e);
        }

        private void CmdCan_Click(object sender, EventArgs e)
        {
            CmdAddDoc.Enabled = false;
            CmdDelDoc.Enabled = false;
            CmdAce.Enabled = false;
            DescativarEntorno();
            panel5.Visible = false;
        }

        private void CmdAceOriDoc_Click(object sender, EventArgs e)
        {
            int n_fil = 0;
            int n_filok = 0;
            for (n_fil = 2; n_fil <= FgDocOri.Rows.Count - 1; n_fil++)
            {
                if (FgDocOri.GetData(n_fil, 1).ToString() != "")
                {
                    if (FgDocOri.GetData(n_fil, 2).ToString() == "")
                    {
                        MessageBox.Show("¡ No ha especificado el tipo de documento en la fila " + n_fil.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    if (FgDocOri.GetData(n_fil, 4).ToString() == "")
                    {
                        MessageBox.Show("¡ No ha especificado el numero de documento en la fila " + n_fil.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    if (b_chequeanulado == false)
                    { 
                        if (Convert.ToDouble(FgDocOri.GetData(n_fil, 5)) == 0)
                        {
                            MessageBox.Show("¡ No ha especificado el el importe en la fila " + n_fil.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            return;
                        }
                    }
                    n_filok = n_filok + 1;
                }
            }

            if (n_filok == 0)
            {
                MessageBox.Show("¡ No se ha registrado ningun documento, debe de especificar al menos un documento para continuar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            // ELIMINAMOS EL DOCUMENTO ACTUAL DE LA LISTA DE ORIGENDETALLE
            //for (n_fil = 2; n_fil <= l_TesOriDet.Count - 1; n_fil++)
            //{
            //    if l_TesOriDet[n_fil].n_idmedpag = 
            //}

            int n_cor = 0;
            int n_corgrid;
            for (n_fil = 2; n_fil <= FgDocOri.Rows.Count - 1; n_fil++)
            {
                n_corgrid = Convert.ToInt32(funFunciones.NulosN(FgDocOri.GetData(n_fil, 8)).ToString());
                
                if (BuscarEliminarOrigenDoc(Convert.ToInt32(LblIdOri.Text), n_corgrid) == true)
                {
                    n_cor = n_corgrid;
                }
                else
                {
                    n_cor = n_cor + 1;
                }

                BE_TES_TESORERIAORIDET e_OriDet = new BE_TES_TESORERIAORIDET();

                e_OriDet.n_idtes = 0;
                e_OriDet.n_idori = Convert.ToInt32(LblIdOri.Text);
                e_OriDet.n_idtipper = 0;
                e_OriDet.n_idmod = 0;
                e_OriDet.n_iddoc = 0;
                e_OriDet.n_idper = 0;
                e_OriDet.n_idtipdoc = Convert.ToInt32(FgDocOri.GetData(n_fil, 7).ToString());
                e_OriDet.c_numser = "";
                e_OriDet.c_numdoc = FgDocOri.GetData(n_fil, 4).ToString();
                e_OriDet.n_imp = 0;
                e_OriDet.n_sal = 0;
                e_OriDet.n_acuenta = Convert.ToDouble(FgDocOri.GetData(n_fil, 5).ToString());
                e_OriDet.d_fchdoc = Convert.ToDateTime(TxtFchEmi.Text);
                e_OriDet.c_glo = "";
                e_OriDet.n_cor = n_cor;
                e_OriDet.n_idmon = Convert.ToInt32(CboMon.SelectedValue);
                e_OriDet.n_idmedpag = Convert.ToInt32(FgDocOri.GetData(n_fil, 6).ToString());

                l_TesOriDet.Add(e_OriDet);
            }

            double n_valor = 0;
            for (n_fil = 0; n_fil <= l_TesOriDet.Count - 1; n_fil++)
            {
                n_valor = (n_valor + l_TesOriDet[n_fil].n_acuenta);
            }

            if (Convert.ToInt32(CboMon.SelectedValue) == 115)
            {
                FgOriIng.SetData(FgOriIng.Row, 3, n_valor.ToString("0.00"));

                n_valor = (n_valor / Convert.ToDouble(LblTc.Text));
                FgOriIng.SetData(FgOriIng.Row, 4, n_valor.ToString("0.00"));
            }
            else
            {
                FgOriIng.SetData(FgOriIng.Row, 4, n_valor.ToString("0.00"));

                n_valor = (n_valor * Convert.ToDouble(LblTc.Text));
                FgOriIng.SetData(FgOriIng.Row, 3, n_valor.ToString("0.00"));

                
            }

            CmdCanOriDoc_Click(sender, e);
        }

        private void CmdCanOriDoc_Click(object sender, EventArgs e)
        {
            CmdAceOriDoc.Enabled = false;
            DescativarEntorno();
            panel6.Visible = false;
        }

        private void FgDocOri_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (booAgregando == true) { return; }
            string c_dato = "";

            if (FgDocOri.Col == 1)
            {
                DataTable dtResul = new DataTable();
                b_Agregando = true;

                dtResul = objMedPag.BuscarMedioPago(dtMedPag);
                if (dtResul != null)
                {
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["c_des"].ToString();           // MOSTRAMOS LA DESCRIPCION DEL ORIGEN
                        FgDocOri.SetData(FgDocOri.Row, 1, c_dato);

                        c_dato = dtResul.Rows[0]["n_id"].ToString();           // MOSTRAMOS LA DESCRIPCION DEL ORIGEN
                        FgDocOri.SetData(FgDocOri.Row, 6, c_dato);
                    }
                }
            }

            if (FgDocOri.Col == 2)
            {
                DataTable dtResul = new DataTable();
                b_Agregando = true;

                dtResul = objTesDoc.BuscarDocumentos(dtTesDoc);
                if (dtResul != null)
                {
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["c_abr"].ToString();           // MOSTRAMOS LA DESCRIPCION DEL ORIGEN
                        FgDocOri.SetData(FgDocOri.Row, 2, c_dato);

                        c_dato = dtResul.Rows[0]["n_id"].ToString();           // MOSTRAMOS LA DESCRIPCION DEL ORIGEN
                        FgDocOri.SetData(FgDocOri.Row, 7, c_dato);
                    }
                }
            }
            b_Agregando = false;
        }

        private void FgDocOri_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (booAgregando == true) { return; }

            if (FgDocOri.Col == 4)
            { 
                string c_dato = funFunciones.NulosC(FgDocOri.GetData(FgDocOri.Row, 4));
                if (c_dato.Length > 10)
                {
                    MessageBox.Show("¡ El numero maximo de caracteres para el numero de documento no puede exceder los 10 digitos !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    FgDocOri.SetData(FgDocOri.Row, 4, "");
                    return;
                }
            }
            if (FgDocOri.Col == 5)
            {
                TxtOriTot.Text = funFlex.FlexSumarCol(FgDocOri, 5, 2, FgDocOri.Rows.Count - 1).ToString("0.00");
            }
        }
        void SumarTotDestino()
        {
            LblHabTotSol.Text = Genericas.Round(funFlex.FlexSumarCol(FgDesIng, 3, 2, FgDesIng.Rows.Count - 1), 4).ToString("0.0000");
            LblHabTotDol.Text = Genericas.Round(funFlex.FlexSumarCol(FgDesIng, 4, 2, FgDesIng.Rows.Count - 1), 4).ToString("0.0000");
        }
        void SumarTotOrigen()
        {
            LblDebTotSol.Text = Genericas.Round(funFlex.FlexSumarCol(FgOriIng, 3, 2, FgOriIng.Rows.Count - 1), 4).ToString("0.0000");
            LblDebTotDol.Text = Genericas.Round(funFlex.FlexSumarCol(FgOriIng, 4, 2, FgOriIng.Rows.Count - 1), 4).ToString("0.0000");
        }
        void SumarColDocumentos()
        {
            double n_Valor1 = 0;
            double n_Valor2 = 0;
            double n_Valor3 = 0;
            double n_Valor4 = 0;
            double n_Valor5 = 0;

            LblTot1.Text = "";
            LblTot2.Text = "";
            LblTot3.Text = "";
            LblTot4.Text = "";
            LblTot5.Text = "";

            n_Valor1 = funFlex.FlexSumarCol(fgDocCli, 6, 2, fgDocCli.Rows.Count - 1);
            n_Valor2 = funFlex.FlexSumarCol(fgDocCli, 7, 2, fgDocCli.Rows.Count - 1);
            n_Valor3 = funFlex.FlexSumarCol(fgDocCli, 8, 2, fgDocCli.Rows.Count - 1);
            n_Valor4 = funFlex.FlexSumarCol(fgDocCli, 9, 2, fgDocCli.Rows.Count - 1);
            n_Valor5 = funFlex.FlexSumarCol(fgDocCli, 10, 2, fgDocCli.Rows.Count - 1);

            LblTot1.Text = n_Valor1.ToString("0.00");
            LblTot2.Text = n_Valor2.ToString("0.00");
            LblTot3.Text = n_Valor3.ToString("0.00");
            LblTot4.Text = n_Valor4.ToString("0.00");
            LblTot5.Text = n_Valor5.ToString("0.00");
        }
        void DescativarEntorno()
        {
            ToolHerramientas.Enabled = !ToolHerramientas.Enabled;
            Tab1.Enabled = !Tab1.Enabled;
        }
        void MostraPanel5()
        {
            TxtCliente.Text = "";
            LblidCliente.Text = "";
            LblTot1.Text = "";
            LblTot2.Text = "";
            LblTot3.Text = "";
            LblTot4.Text = "";
            LblTot5.Text = "";

            fgDocCli.Rows.Count = 2;
            panel5.Left = ((this.Width - panel5.Width) / 2);
            panel5.Top = ((this.Height - panel5.Height) / 2);
            LblConcepto.Text = funFunciones.NulosC(FgDesIng.GetData(FgDesIng.Row, 1)).ToString();
            LblIdDes.Text = FgDesIng.GetData(FgDesIng.Row, 5).ToString();

            DescativarEntorno();
            if (n_QueHace != 3)
            { 
                CmdAddDoc.Enabled = true;
                CmdDelDoc.Enabled = true;
                CmdAce.Enabled = true;
            }

            int n_fil = 0;
            string c_dato = "";
            if (l_TesDesDet.Count != 0)                              // SI SE HA REGISTRADO DOCUMENTOS DE ORIGEN, LOS MOSTRAMOS EN EL DETALLE
            {
                fgDocCli.Rows.Count = 2;
                for (n_fil = 0; n_fil <= l_TesDesDet.Count - 1; n_fil++)
                {
                    fgDocCli.Rows.Count = fgDocCli.Rows.Count + 1;

                    CN_log_compras objcom = new CN_log_compras();
                    BE_LOG_COMPRAS e_compra = new BE_LOG_COMPRAS();
                    CN_con_regpercepcion o_Percep = new CN_con_regpercepcion();
                    BE_CON_REGPERCEPCION e_Percep = new BE_CON_REGPERCEPCION();

                    //if (Convert.ToInt32(l_TesDesDet[n_fil].n_idtipdoc) == 2)
                    //{ 
                    //    objcom.mysConec = mysConec;
                    //    objcom.TraerRegistro(l_TesDesDet[n_fil].n_iddoc);
                    //    e_compra = objcom.e_Compras;
                    //    c_dato = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_nombre", e_compra.n_idpro.ToString(), "N").ToString();
                    //}
                    //if (Convert.ToInt32(l_TesDesDet[n_fil].n_idtipdoc) == 86)
                    //{
                    //    o_Percep.mysConec = mysConec;
                    //    o_Percep.TraerRegistro(l_TesDesDet[n_fil].n_iddoc, 2);
                    //    e_Percep = o_Percep.e_Percepcion;
                    //    c_dato = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_nombre", e_Percep.n_idcli.ToString(), "N").ToString();
                    //}
                    if (Convert.ToInt32(l_TesDesDet[n_fil].n_idlib) == 8)
                    {
                        objcom.mysConec = mysConec;
                        objcom.TraerRegistro(l_TesDesDet[n_fil].n_iddoc);
                        e_compra = objcom.e_Compras;
                        c_dato = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_nombre", e_compra.n_idpro.ToString(), "N").ToString();
                    }

                    if (Convert.ToInt32(l_TesDesDet[n_fil].n_idtipdoc) == 86)
                    {
                        o_Percep.mysConec = mysConec;
                        o_Percep.TraerRegistro(l_TesDesDet[n_fil].n_iddoc, 2);
                        e_Percep = o_Percep.e_Percepcion;
                        c_dato = funDatos.DataTableBuscar(dtCliPro, "n_id", "c_nombre", e_Percep.n_idcli.ToString(), "N").ToString();
                    }
                    
                    fgDocCli.SetData(fgDocCli.Rows.Count - 1, 1, c_dato);

                    c_dato = funDatos.DataTableBuscar(dtTipDoc, "n_id", "c_abr", l_TesDesDet[n_fil].n_idtipdoc.ToString(), "N").ToString();
                    fgDocCli.SetData(fgDocCli.Rows.Count - 1, 2, c_dato);

                    c_dato = Convert.ToDateTime(l_TesDesDet[n_fil].d_fchdoc).ToString("dd/MM/yyyy");
                    fgDocCli.SetData(fgDocCli.Rows.Count - 1, 3, c_dato);

                    c_dato = funDatos.DataTableBuscar(dtMon, "n_id", "c_simbolo", l_TesDesDet[n_fil].n_idmon.ToString(), "N").ToString();
                    fgDocCli.SetData(fgDocCli.Rows.Count - 1, 4, c_dato);

                    c_dato = l_TesDesDet[n_fil].c_numser.ToString() + "-" + l_TesDesDet[n_fil].c_numdoc.ToString();
                    fgDocCli.SetData(fgDocCli.Rows.Count - 1, 5, c_dato);

                    c_dato = Convert.ToDouble(l_TesDesDet[n_fil].n_imp).ToString("0.00");
                    fgDocCli.SetData(fgDocCli.Rows.Count - 1, 6, c_dato);

                    c_dato = Convert.ToDouble(l_TesDesDet[n_fil].n_sal).ToString("0.00");
                    fgDocCli.SetData(fgDocCli.Rows.Count - 1, 7, c_dato);

                    if (Convert.ToInt32(CboMon.SelectedValue) == 115)
                    {
                        if (l_TesDesDet[n_fil].n_idmon == 115)
                        {
                            c_dato = Convert.ToDouble(l_TesDesDet[n_fil].n_sal).ToString("0.00");
                            fgDocCli.SetData(fgDocCli.Rows.Count - 1, 8, c_dato);
                        }
                        else
                        {
                            c_dato = (Convert.ToDouble(l_TesDesDet[n_fil].n_sal) * Convert.ToDouble(LblTc.Text)).ToString("0.00");
                            fgDocCli.SetData(fgDocCli.Rows.Count - 1, 8, c_dato);
                        }
                    }
                    else
                    {
                        if (l_TesDesDet[n_fil].n_idmon == 151)
                        {
                            c_dato = Convert.ToDouble(l_TesDesDet[n_fil].n_sal).ToString("0.00");
                            fgDocCli.SetData(fgDocCli.Rows.Count - 1, 8, c_dato);
                        }
                        else
                        {
                            c_dato = (Convert.ToDouble(l_TesDesDet[n_fil].n_sal) / Convert.ToDouble(LblTc.Text)).ToString("0.00");
                            fgDocCli.SetData(fgDocCli.Rows.Count - 1, 8, c_dato);
                        }
                    }

                    c_dato = Convert.ToDouble(l_TesDesDet[n_fil].n_acuenta).ToString("0.00");
                    fgDocCli.SetData(fgDocCli.Rows.Count - 1, 9, c_dato);

                    double n_newsaldo = 0;
                    double n_saldo = Convert.ToDouble(funFunciones.NulosN(fgDocCli.GetData(fgDocCli.Rows.Count - 1, 8)));
                    double n_acuenta = Convert.ToDouble(funFunciones.NulosN(fgDocCli.GetData(fgDocCli.Rows.Count - 1, 9))); 
                    n_newsaldo = (n_saldo - n_acuenta);

                    c_dato = n_newsaldo.ToString("0.00");
                    fgDocCli.SetData(fgDocCli.Rows.Count - 1, 10, c_dato);

                    c_dato = Convert.ToInt32(l_TesDesDet[n_fil].n_iddoc).ToString();
                    fgDocCli.SetData(fgDocCli.Rows.Count - 1, 11, c_dato);

                    c_dato = Convert.ToInt32(l_TesDesDet[n_fil].n_idmon).ToString();
                    fgDocCli.SetData(fgDocCli.Rows.Count - 1, 12, c_dato);

                    c_dato = Convert.ToInt32(l_TesDesDet[n_fil].n_cor).ToString();
                    fgDocCli.SetData(fgDocCli.Rows.Count - 1, 13, c_dato);

                    c_dato = Convert.ToInt32(l_TesDesDet[n_fil].n_idtipdoc).ToString();
                    fgDocCli.SetData(fgDocCli.Rows.Count - 1, 14, c_dato);

                    c_dato = Convert.ToInt32(l_TesDesDet[n_fil].n_idlib).ToString();
                    fgDocCli.SetData(fgDocCli.Rows.Count - 1, 15, c_dato);
                }

                SumarColDocumentos();
            }
            panel5.Visible = true;
        }
        void MostraPanel6()
        {
            label23.Text = "";
            LblIdOri.Text = "";
            TxtOriTot.Text = "";

            FgDocOri.Cols[1].ComboList = "...";
            FgDocOri.Cols[2].ComboList = "...";

            FgDocOri.Rows.Count = 2;
            FgDocOri.Rows.Count = FgDocOri.Rows.Count + 1;

            LblConceptoOri.Text = FgOriIng.GetData(FgOriIng.Row, 1).ToString();
            LblIdOri.Text = FgOriIng.GetData(FgOriIng.Row, 5).ToString();

            panel6.Left = ((this.Width - panel6.Width) / 2);
            panel6.Top = ((this.Height - panel6.Height) / 2);

            DescativarEntorno();
            if (n_QueHace != 3)
            {
                CmdAceOriDoc.Enabled = true;
            }

            int n_fil = 0;
            string c_dato = "";
            if (l_TesOriDet.Count != 0)                              // SI SE HA REGISTRADO DOCUMENTOS DE ORIGEN, LOS MOSTRAMOS EN EL DETALLE
            {
                FgDocOri.Rows.Count = 2;
                for (n_fil = 0; n_fil <= l_TesOriDet.Count - 1; n_fil++)
                {
                    FgDocOri.Rows.Count = FgDocOri.Rows.Count + 1;

                    c_dato = funDatos.DataTableBuscar(dtMedPag, "n_id", "c_des", l_TesOriDet[n_fil].n_idmedpag.ToString(), "N").ToString();
                    FgDocOri.SetData(FgDocOri.Rows.Count - 1, 1, c_dato);

                    c_dato = funDatos.DataTableBuscar(dtTesDoc, "n_id", "c_abr", l_TesOriDet[n_fil].n_idtipdoc.ToString(), "N").ToString();
                    FgDocOri.SetData(FgDocOri.Rows.Count - 1, 2, c_dato);

                    c_dato = funDatos.DataTableBuscar(dtMon, "n_id", "c_codsun", Convert.ToInt32(CboMon.SelectedValue).ToString(), "N").ToString();
                    FgDocOri.SetData(FgDocOri.Rows.Count - 1, 3, c_dato);

                    c_dato = l_TesOriDet[n_fil].c_numdoc;
                    FgDocOri.SetData(FgDocOri.Rows.Count - 1, 4, c_dato);

                    c_dato = l_TesOriDet[n_fil].n_acuenta.ToString("0.00");
                    FgDocOri.SetData(FgDocOri.Rows.Count - 1, 5, c_dato);

                    c_dato = l_TesOriDet[n_fil].n_idmedpag.ToString();
                    FgDocOri.SetData(FgDocOri.Rows.Count - 1, 6, c_dato);

                    c_dato = l_TesOriDet[n_fil].n_idtipdoc.ToString();
                    FgDocOri.SetData(FgDocOri.Rows.Count - 1, 7, c_dato);

                    c_dato = l_TesOriDet[n_fil].n_cor.ToString();
                    FgDocOri.SetData(FgDocOri.Rows.Count - 1, 8, c_dato);
                }
            }
            else
            {
                c_dato = funDatos.DataTableBuscar(dtMon, "n_id", "c_codsun", Convert.ToInt32(CboMon.SelectedValue).ToString(), "N").ToString();
                FgDocOri.SetData(FgDocOri.Rows.Count - 1, 3, c_dato);
            }

            panel6.Visible = true;
        }
        bool BuscarEliminarDestinoDoc(int n_IdDestino, int n_cor)
        {
            int n_fil = 0;
            bool b_seencontro = false;
            if (n_cor == 0) { return b_seencontro; }         // SI ES 0 QUIERE DECIR QUE RECIEN SE ESTA AGREGANDO EL DOCUMENTO DEL ORIGEN

            for (n_fil = 0; n_fil <= fgDocCli.Rows.Count - 1; n_fil++)
            {
                if (l_TesDesDet[n_fil].n_iddes == n_IdDestino)
                {
                    if (l_TesDesDet[n_fil].n_cor == n_cor)
                    {
                        l_TesDesDet.RemoveAt(n_fil);
                        b_seencontro = true;
                        return b_seencontro;
                    }
                }
            }
            return b_seencontro;
        }
        bool BuscarEliminarOrigenDoc(int n_IdOrigen, int n_cor)
        {
            int n_fil = 0;
            bool b_seencontro = false;
            if (n_cor == 0) { return b_seencontro; }         // SI ES 0 QUIERE DECIR QUE RECIEN SE ESTA AGREGANDO EL DOCUMENTO DEL ORIGEN
            int n_row = 0;

            for (n_fil = 1; n_fil <= FgDocOri.Rows.Count - 2; n_fil++)
            {
                if (l_TesOriDet[n_row].n_idori == n_IdOrigen)
                {
                    if (l_TesOriDet[n_row].n_cor == n_cor)
                    {
                        l_TesOriDet.RemoveAt(n_row);
                        b_seencontro = true;
                        return b_seencontro;
                    }
                }
                n_row = n_row + 1;
            }
            return b_seencontro;
        }
        void MostarDetalle()
        {
            int n_row = 0;
            double n_valor = 0;
            string c_dato = "";

            // MOSTRAMOS EL DEBE DE LA OPERACION
            for (n_row = 0; n_row <= l_TesOri.Count - 1; n_row++)
            {
                FgOriIng.Rows.Count = FgOriIng.Rows.Count + 1;

                c_dato = funDatos.DataTableBuscar(dtOri, "n_id", "c_des", l_TesOri[n_row].n_idori.ToString(), "N").ToString();
                FgOriIng.SetData(FgOriIng.Rows.Count - 1, 1, c_dato);

                c_dato = l_TesOri[n_row].n_tc.ToString("0.000");
                FgOriIng.SetData(FgOriIng.Rows.Count - 1, 2, c_dato);

                if (e_Tesoreria.n_idmon == 115)
                {
                    c_dato = l_TesOri[n_row].n_imp.ToString("0.0000");
                    FgOriIng.SetData(FgOriIng.Rows.Count - 1, 3, c_dato);

                    n_valor = l_TesOri[n_row].n_imp;
                    n_valor = (n_valor / l_TesOri[n_row].n_tc);
                    FgOriIng.SetData(FgOriIng.Rows.Count - 1, 4, n_valor.ToString("0.000000"));
                }
                else
                {
                    n_valor = l_TesOri[n_row].n_imp;
                    n_valor = (n_valor * l_TesOri[n_row].n_tc);
                    FgOriIng.SetData(FgOriIng.Rows.Count - 1, 3, n_valor.ToString("0.0000"));

                    c_dato = l_TesOri[n_row].n_imp.ToString("0.000000");
                    FgOriIng.SetData(FgOriIng.Rows.Count - 1, 4, c_dato);
                }

                c_dato = l_TesOri[n_row].n_idori.ToString();
                FgOriIng.SetData(FgOriIng.Rows.Count - 1, 5, c_dato);
            }

            // MOSTRAMOS EL HABER DE LA OPERACION
            for (n_row = 0; n_row <= l_TesDes.Count - 1; n_row++)
            {
                FgDesIng.Rows.Count = FgDesIng.Rows.Count + 1;

                c_dato = funDatos.DataTableBuscar(dtDes, "n_id", "c_des", l_TesDes[n_row].n_iddes.ToString(), "N").ToString();
                FgDesIng.SetData(FgDesIng.Rows.Count - 1, 1, c_dato);

                c_dato = l_TesDes[n_row].n_tc.ToString("0.000");
                FgDesIng.SetData(FgDesIng.Rows.Count - 1, 2, c_dato);

                if (e_Tesoreria.n_idmon == 115)
                {
                    c_dato = l_TesDes[n_row].n_imp.ToString("0.0000");
                    FgDesIng.SetData(FgDesIng.Rows.Count - 1, 3, c_dato);

                    n_valor = l_TesDes[n_row].n_imp;
                    n_valor = (n_valor / l_TesDes[n_row].n_tc);
                    FgDesIng.SetData(FgDesIng.Rows.Count - 1, 4, n_valor.ToString("0.000000"));
                }
                else
                {
                    n_valor = l_TesDes[n_row].n_imp;
                    n_valor = (n_valor * l_TesDes[n_row].n_tc);
                    FgDesIng.SetData(FgDesIng.Rows.Count - 1, 3, n_valor.ToString("0.0000"));

                    c_dato = l_TesDes[n_row].n_imp.ToString("0.000000");
                    FgDesIng.SetData(FgDesIng.Rows.Count - 1, 4, c_dato);
                }

                c_dato = l_TesDes[n_row].n_iddes.ToString();
                FgDesIng.SetData(FgDesIng.Rows.Count - 1, 5, c_dato);
            }
            
            LblDebTotSol.Text = Genericas.Round(funFlex.FlexSumarCol(FgOriIng, 3, 2, FgOriIng.Rows.Count - 1), 4).ToString("0.0000");
            LblDebTotDol.Text = Genericas.Round(funFlex.FlexSumarCol(FgOriIng, 4, 2, FgOriIng.Rows.Count - 1), 4).ToString("0.0000");
            LblHabTotSol.Text = Genericas.Round(funFlex.FlexSumarCol(FgDesIng, 3, 2, FgDesIng.Rows.Count - 1), 4).ToString("0.0000");
            LblHabTotDol.Text = Genericas.Round(funFlex.FlexSumarCol(FgDesIng, 4, 2, FgDesIng.Rows.Count - 1), 4).ToString("0.0000");
        }
        private void fgDocCli_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }
            
            if (fgDocCli.Col == 9)
            { 
                double n_newsaldo = 0;
                double n_saldo = Convert.ToDouble(funFunciones.NulosN(fgDocCli.GetData(fgDocCli.Row,8)));
                double n_acuenta = Convert.ToDouble(funFunciones.NulosN(fgDocCli.GetData(fgDocCli.Row, 9))); ;
                n_newsaldo = (n_saldo - n_acuenta);
                fgDocCli.SetData(fgDocCli.Row, 10, n_newsaldo.ToString("0.00"));
                SumarColDocumentos();

            }
        }
        private void fgDocCli_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            if (fgDocCli.Col == 9)
            {
                fgDocCli.AllowEditing = true;
            }
            else
            {
                fgDocCli.AllowEditing = false;
            }
        }
        private void fgDocCli_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 9)
            {
                if (!c_Numerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void FgDocOri_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            if (FgDocOri.Col == 3)
            {
                FgDocOri.AllowEditing = false;
            }
            else
            {
                FgDocOri.AllowEditing = true;
            }
        }
        private void FgDocOri_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            if (FgDocOri.Col == 4)
            {
                string c_dato = funFunciones.NulosC(FgDocOri.GetData(FgDocOri.Row, 4));
                if (c_dato.Length >= 10)
                {
                    MessageBox.Show("¡ El numero maximo de caracteres para el numero de documento no puede exceder los 10 digitos !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    FgDocOri.SetData(FgDocOri.Row, 4, "");
                    return;
                }
                //FgDocOri.AllowEditing = false;
            }
        }
        private void TxtFchEmi_ValueChanged(object sender, EventArgs e)
        {

        }
        private void ToolExportarExcel_Click(object sender, EventArgs e)
        {
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-TES-EGR-" + STU_SISTEMA.ANOTRABAJO.ToString() + Convert.ToInt32(CboMeses.SelectedValue).ToString("00") + ".xls";
            DgLista.ExportTo(c_NomArchivo);
            MessageBox.Show("! Se exporto con exito la informacion en el archivo " + c_NomArchivo + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        private void emitirChequeAnuladoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            b_chequeanulado = true;
            Nuevo();
        }
        private void ToolImprimir_ButtonClick(object sender, EventArgs e)
        {

        }
        private void crearMovimientoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            b_chequeanulado = false;
            Nuevo();
        }
        private void ToolNuevo_ButtonClick(object sender, EventArgs e)
        {
            b_chequeanulado = false;
            Nuevo();
        }
        private void emitirChequeAnuladoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            b_chequeanulado = true;
            Nuevo();
        }
        private void emitirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_tes_tesoreria objAlm = new CN_tes_tesoreria();

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[1].CellValue(DgLista.Row).ToString());
            objAlm.STU_SISTEMA = STU_SISTEMA;
            objAlm.mysConec = mysConec;
            objAlm.ImprimirDocumento(intIdRegistro, 1);
        }
        private void CmdGenAsi_Click(object sender, EventArgs e)
        {
            objRegistros.mysConec = mysConec;
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            if (objRegistros.RegeneraAsientos(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 1, 2) == true)
            {
                MessageBox.Show("¡ Los asientos contables se crearon con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                ListarItems();
            }
        }

        private void FgDesIng_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            //if (Convert.ToInt32(e.KeyChar) == 13)
            //{
            //    FgDesIng.AllowEditing = true;
            //}
        }

        private void FgDesIng_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 116)
            {
                //b_editandovalor = true;
                FgDesIng.AllowEditing = true;
            }
        }

        private void FgOriIng_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 116)
            {
                FgOriIng.AllowEditing = true;
            }
        }
    }
}
