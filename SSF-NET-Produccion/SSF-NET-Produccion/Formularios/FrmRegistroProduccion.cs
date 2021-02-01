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
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Produccion;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sunat;
using SIAC_Entidades.Produccion;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Almacen;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using System.Data.OleDb;
using C1.Win.C1FlexGrid;
using SIAC_Datos.Classes;
using SIAC_DATOS.Models.Produccion;
using Helper.Classes;
using Refit;
using Helper.Service;
using System.Configuration;

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmRegistroProduccion : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public OleDbConnection AccConec = new OleDbConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        BE_PRO_PRODUCCION entRegistro = new BE_PRO_PRODUCCION();
        List<BE_PRO_PRODUCCIONINS> lstInsumos = new List<BE_PRO_PRODUCCIONINS>();

        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_pro_produccion objRegistro = new CN_pro_produccion();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_sun_tipdoccom objTipDoc2 = new CN_sun_tipdoccom();
        CN_alm_inventario ObjItem = new CN_alm_inventario();
        CN_sun_unimed objUniMedSunat = new CN_sun_unimed();
        CN_pro_personal objPerPro = new CN_pro_personal();
        CN_pro_ordenproduccion objOrdenProduccion = new CN_pro_ordenproduccion();
        CN_mae_clipro o_cliente = new CN_mae_clipro(); 
        CN_alm_inventariounimed objUniMed = new CN_alm_inventariounimed();
        CN_pro_productosrecetas objRecetas = new CN_pro_productosrecetas();
        CN_pro_productosrecetaslineas objLineas = new CN_pro_productosrecetaslineas();
        CN_sun_tipexi objTipExi = new CN_sun_tipexi();
        CN_pro_productosrecetasinsumos objRecetasInsumo = new CN_pro_productosrecetasinsumos();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Convertir funCon = new Convertir();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles FunControl = new Cls_Controles();
        DatosMySql FunMysql = new DatosMySql();

        // DATATABLE LOCALES
        DataTable dtForm = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtListar = new DataTable();
        DataTable dtTipDoc = new DataTable();
        DataTable dtNumDocRef = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtPerPro = new DataTable();
        DataTable dtPrioridad = new DataTable();
        DataTable dtOrdenProduccion = new DataTable();
        DataTable dtTareas = new DataTable();
        DataTable dtUniMed = new DataTable();
        DataTable dtUniMedSunat = new DataTable();
        DataTable dtRecetas = new DataTable();
        DataTable dtLineas = new DataTable();
        DataTable dtTipExi = new DataTable();
        DataTable dtEquipos = new DataTable();
        DataTable dtSolMat = new DataTable();
        DataTable dtLinTar = new DataTable();
        DataTable dtRecetasInsumos = new DataTable();
        DataTable dtTipDoc2 = new DataTable();
        DataTable dtcliente = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                               // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[11, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraIns = new string[8, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strNumerovalidos2 = "1234567890-ABCDEF" + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;

        ObservableListSource<ProduccionNotaIng> ProduccionNotaIngs;

        public FrmRegistroProduccion()
        {
            InitializeComponent();
        }
        private void FrmRegistroProduccion_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            Tab1.SelectedIndex = 0;
            ConfigurarFormulario();
            booAgregando = false;
            ProduccionNotaIngs = new ObservableListSource<ProduccionNotaIng>();
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtTipDoc2, "n_id", "c_des");
            
            DataTable dtResul = new DataTable();
            funDatos.ComboBoxCargarDataTable(CboTipDocRef, dtTipDoc, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboUniMed, dtUniMed, "n_id", "c_despre");
            funDatos.ComboBoxCargarDataTable(CboSup, dtPerPro, "n_id", "c_apenom");

            funDatos.ComboBoxCargarDataTable(CboRec, dtRecetas, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboLin, dtLineas, "n_id", "c_deslin");
        }
        void ConfigurarFormulario()
        {
            this.Height = 587;
            this.Width = 965;         

            //Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            //Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;

            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            //FgInsumo
            arrCabeceraIns[0, 0] = "Tipo Item";
            arrCabeceraIns[0, 1] = "100";
            arrCabeceraIns[0, 2] = "C";
            arrCabeceraIns[0, 3] = "";
            arrCabeceraIns[0, 4] = "";

            arrCabeceraIns[1, 0] = "Insumo / Producto";
            arrCabeceraIns[1, 1] = "390";
            arrCabeceraIns[1, 2] = "C";
            arrCabeceraIns[1, 3] = "";
            arrCabeceraIns[1, 4] = "";

            arrCabeceraIns[2, 0] = "Uni. Med.";
            arrCabeceraIns[2, 1] = "50";
            arrCabeceraIns[2, 2] = "C";
            arrCabeceraIns[2, 3] = "";
            arrCabeceraIns[2, 4] = "";

            arrCabeceraIns[3, 0] = "Cantidad Entregada";
            arrCabeceraIns[3, 1] = "80";
            arrCabeceraIns[3, 2] = "D";
            arrCabeceraIns[3, 3] = "0.000000";
            arrCabeceraIns[3, 4] = "";

            arrCabeceraIns[4, 0] = "Cantidad Ultilizada";
            arrCabeceraIns[4, 1] = "80";
            arrCabeceraIns[4, 2] = "D";
            arrCabeceraIns[4, 3] = "0.000000";
            arrCabeceraIns[4, 4] = "";

            arrCabeceraIns[5, 0] = "Diferencia";
            arrCabeceraIns[5, 1] = "80";
            arrCabeceraIns[5, 2] = "D";
            arrCabeceraIns[5, 3] = "0.000000";
            arrCabeceraIns[5, 4] = "";

            arrCabeceraIns[6, 0] = "IdInsumo";
            arrCabeceraIns[6, 1] = "0";
            arrCabeceraIns[6, 2] = "N";
            arrCabeceraIns[6, 3] = "0";
            arrCabeceraIns[6, 4] = "";

            arrCabeceraIns[7, 0] = "Consu mido";
            arrCabeceraIns[7, 1] = "50";
            arrCabeceraIns[7, 2] = "B";
            arrCabeceraIns[7, 3] = "0";
            arrCabeceraIns[7, 4] = "";

            funFlex.FlexMostrarDatos(FgInsumo, arrCabeceraIns, dtListar, 2, false);

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(44, ref arrCabeceraDg1);
        }
        void DataTableCargar()
        {
            bool b_result = false;

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(44);                             // INDICAMOS QUE ES EL FORMULARIO MAESTRO DE RECETAS

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objRegistro.mysConec = mysConec;
            b_result  = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            if (b_result == true)
            {
                dtListar = objRegistro.dtListar;
            }

            ObjItem.mysConec = mysConec;
            dtItems = ObjItem.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);

            objUniMed.mysConec = mysConec;
            dtUniMed = objUniMed.Listar();

            objUniMedSunat.mysConec = mysConec;
            dtUniMedSunat = objUniMedSunat.Listar();

            objTipDoc.mysConec = mysConec;
            dtTipDoc = objTipDoc.Listar();
            dtTipDoc = funDatos.DataTableFiltrar(dtTipDoc, "n_id = 76");

            objTipDoc2.mysConec = mysConec;
            dtTipDoc2 = objTipDoc2.Listar();

            // CARGAMOS EL PERSONAL DE PRODUCCION
            objPerPro.mysConec = mysConec;
            if (objPerPro.Listar(STU_SISTEMA.EMPRESAID) == true)
            {
                dtPerPro = objPerPro.dtPersonal;
            }

            objOrdenProduccion.mysConec = mysConec;
            dtOrdenProduccion = objOrdenProduccion.Listar(STU_SISTEMA.EMPRESAID, 0, 0);

            //objtareas.mysConec = mysConec;
            //dtTareas = objtareas.Listar(STU_SISTEMA.EMPRESAID);

            objRecetas.mysConec = mysConec;
            b_result = objRecetas.ListarTodasRecetas(STU_SISTEMA.EMPRESAID);
            if (b_result == true)
            {
                dtRecetas = objRecetas.dtRecetas;
                    
            }
            objRecetasInsumo.mysConec = mysConec;
            b_result = objRecetasInsumo.ListarTodasLineas(STU_SISTEMA.EMPRESAID);
            if (b_result == true)
            {
                dtRecetasInsumos = objRecetasInsumo.dtInsumos;
            }

            objLineas.mysConec = mysConec;
            b_result = objLineas.ListarTodasLineas(STU_SISTEMA.EMPRESAID);
            if (b_result == true)
            {
                dtLineas = objLineas.dtLineas;
            }

            objTipExi.mysConec = mysConec;
            dtTipExi = objTipExi.Listar();

            objLineas.mysConec = mysConec;
            b_result = objLineas.ListarTodasLineas(STU_SISTEMA.EMPRESAID);


            o_cliente.mysConec = mysConec;
            dtcliente = o_cliente.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            //objSolMat.mysConec = mysConec;
            //b_result = objSolMat.Consulta3(STU_SISTEMA.EMPRESAID);
            //if (b_result == true)
            //{
            //    dtSolMat = objSolMat.dtLista;
            //}
            
            //objRecetas.mysConec = mysConec;
            //b_result = objRecetas.ListarTodasRecetas(STU_SISTEMA.EMPRESAID);
            //if (b_result == true)
            //{
            //    dtRecetas = objRecetas.dtRecetas;
            //}
        }
        void ListarItems()
        {
            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
            LblNumReg.Text = (dtListar.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtListar, true);
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
            string c_dato = "";
            Blanquea();
            DataTable dtResul = new DataTable();

            Tab02.SelectedIndex = 0;

            objRegistro.TraerRegistro(n_IdRegistro);
            entRegistro = objRegistro.EntProduccion;
            lstInsumos = objRegistro.LstInsumos;

            CboTipDoc.SelectedValue = entRegistro.n_idtipdoc;     // PARTE DE PRODUCION (VALOR POR DEFECTO DEL CAMPO)
            TxtFchReg.Text = Convert.ToDateTime(entRegistro.d_fchreg).ToString("dd/MM/yyyy");
            TxtNumSer.Text = entRegistro.c_numser;
            TxtNumDoc.Text = entRegistro.c_numdoc;
            CboTipDocRef.SelectedValue = entRegistro.n_docrefidtipdoc;
            TxtNumSerDocRef.Text = entRegistro.c_docrefnumser;
            TxtNumDocRef.Text = entRegistro.c_docrefnumdoc;

            c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", entRegistro.n_idpro.ToString(), "N").ToString();
            TxtPro.Text = c_dato;
            CboUniMed.SelectedValue = entRegistro.n_idunimed;
            CboSup.SelectedValue = entRegistro.n_idsup;
            TxtFchPro.Text = entRegistro.d_fchpro.ToString("dd/MM/yyyy");
            TxtHorIni.Text = entRegistro.c_horini;
            TxtHorFin.Text = entRegistro.c_horfin;
            TxtCanPro.Text = entRegistro.n_canpro.ToString("0.00");
            TxtNumLot.Text = entRegistro.c_numlot;
            TxtObs.Text = entRegistro.c_obs;
            TxtCanMatPri.Text = entRegistro.n_caningmp.ToString("0.00");
         
            LblIdOP.Text = entRegistro.n_docrefidtipdoc.ToString();
            LblIdIte.Text = entRegistro.n_idpro.ToString();

            DataTable dtResult = new DataTable();
            DataTable dtResLin = new DataTable();

            dtResult = funDatos.DataTableFiltrar(dtRecetas, "n_idpro = " + Convert.ToInt32(LblIdIte.Text) + "");
            funDatos.ComboBoxCargarDataTable(CboRec, dtResult, "n_id", "c_des");

            dtResLin = funDatos.DataTableFiltrar(dtLineas, "n_idpro = " + Convert.ToInt32(LblIdIte.Text) + "");
            funDatos.ComboBoxCargarDataTable(CboLin, dtResLin, "n_id", "c_deslin");

            LblIdReceta.Text = entRegistro.n_idrec.ToString();
            CboRec.SelectedValue = entRegistro.n_idrec;
            LblIdLinea.Text = entRegistro.n_idlin.ToString();
            CboLin.SelectedValue = entRegistro.n_idlin;
            CboSup.SelectedValue = entRegistro.n_idsup;

            TxtCanProducida.Text = entRegistro.n_canprorea.ToString("0.00");
            TxtCanPagar.Text = entRegistro.n_conmp.ToString("0.00");

            if (entRegistro.d_fchterpro != null)
            {
                TxtFchTerProd.Text = entRegistro.d_fchterpro.ToString();
            }
            else
            {
                FunControl.dtpBlanquea(TxtFchTerProd);
            }

            if (entRegistro.n_idestado == 2)
            {
                ChkCerrar.Checked = true;
            }
            else
            {
                ChkCerrar.Checked = false;
            }

            if (entRegistro.n_tipreg == 1) { OptTipPro1.Checked = true; }
            if (entRegistro.n_tipreg == 2) { OptTipPro2.Checked = true; }

            if (Convert.ToInt32(funFunciones.NulosN(entRegistro.n_idcli)) != 0)
            {
                DataTable dtres = new DataTable();
                dtres = funDatos.DataTableFiltrar(dtcliente, "n_id = " + entRegistro.n_idcli.ToString() + "");
                if (dtres.Rows.Count != 0)
                {
                    LblidCliente.Text = entRegistro.n_idcli.ToString();
                    TxtNumRuc.Text = dtres.Rows[0]["c_numdoc"].ToString();
                    TxtCliente.Text = dtres.Rows[0]["c_nombre"].ToString();
                }
            }
            if (Convert.ToInt32(funFunciones.NulosN(entRegistro.n_idped)) != 0)
            {
                BE_VTA_PEDIDOCLI e_pedido = new BE_VTA_PEDIDOCLI();
                CN_vta_pedidocli o_pedido = new CN_vta_pedidocli();
                o_pedido.mysConec = mysConec;
                if (o_pedido.TraerRegistro(entRegistro.n_idped) == true)
                {
                    e_pedido  = o_pedido.entPedCab;
                    LblIdDocRef.Text = entRegistro.n_idped.ToString();
                    TxtNumPed.Text = e_pedido.c_pednumdoc.ToString();
                }
                o_pedido = null;
            }
            
            if (entRegistro.n_tipreg == 1)
            {
                LblIdNotIng.Text = entRegistro.n_idnoting.ToString();
                BE_ALM_MOVIMIENTOS_CONSULTA e_mov = new BE_ALM_MOVIMIENTOS_CONSULTA();
                //BE_ALM_MOVIMIENTOS e_mov = new BE_ALM_MOVIMIENTOS ();
                CN_alm_movimientos objMov = new CN_alm_movimientos();
                objMov.mysConec = mysConec;

                if (entRegistro.n_idnoting != 0)
                {
                    e_mov = objMov.TraerRegistro(Convert.ToInt64(entRegistro.n_idnoting));
                    TxtNumNotIng.Text = e_mov.c_numser + "-" + e_mov.c_numdoc;
                    TxtPorRen.Text = entRegistro.n_renpor.ToString("0.00");
                    //TxtCanMatPri.Text = funFunciones.NulosN(e_mov.lst_items[0].n_can).ToString();
                    c_dato = funDatos.DataTableBuscar(dtItems,"n_id","c_despro",e_mov.lst_items[0].n_idite.ToString(),"N").ToString();
                    TxtMatPri.Text = c_dato;
                }
            }

            if (n_QueHace == 3)
            {
                CmdAddInsReal.Text = "Ver Consumo Real de Insumos";
            }
            else
            {
                CmdAddInsReal.Text = "Registrar Consumo Real de Insumos";
            }

            //Detalles
            ProduccionNotaIngs = ProduccionNotaIng.Fetch(n_IdRegistro);
            produccionNotaIngBindingSource.DataSource = ProduccionNotaIngs;
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
            booAgregando = false;

            // MOSTRAMOS EL ULTIMO NUMERO DE LA PROGRAMACION
            string c_numdoc = "";
            objTipDoc.mysConec = mysConec;
            
            CboTipDoc.SelectedValue = 73;                                        // PARTE DE PRODUCCION
            c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 73, "0001");
            TxtNumSer.Text = "0001";
            TxtNumDoc.Text = c_numdoc;
            OptTipPro1.Checked = true;            
            TxtFchReg.Focus();

            //Detalles
            ProduccionNotaIngs = new ObservableListSource<ProduccionNotaIng>();
            produccionNotaIngBindingSource.DataSource = ProduccionNotaIngs;
        }
        void Blanquea()
        {
            FgDoc.Rows.Count = 2;
            FgInsumos.Rows.Count = 2;
            FgTar.Rows.Count = 2;
            FgPer.Rows.Count = 2;

            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtFchReg.Text = "";
            TxtHorIni.Text = "";
            TxtHorFin.Text = "";
            TxtCanPro.Text = "";
            TxtNumLot.Text = "";
            TxtObs.Text = "";
            TxtPro.Text = "";
            TxtHorIni.Text = "";
            TxtHorFin.Text = "";

            TxtNumSerDocRef.Text = "";
            TxtNumDocRef.Text = "";

            TxtCanProducida.Text = "";

            TxtNumNotIng.Text = "";
            TxtPorRen.Text = "";
            LblIdNotIng.Text = "";

            FunControl.dtpBlanquea(TxtFchTerProd);

            LblIdOP.Text = "";
            LblIdIte.Text = "";
            LblIdReceta.Text = "";
            LblIdReceta.Text = "";
            LblIdLinea.Text = "";
            LblIdNotIng.Text = "";

            TxtMatPri.Text = "";
            TxtPorRen.Text = "";
            TxtCanMatPri.Text = "";

            CboTipDoc.SelectedValue = 0;
            CboTipDocRef.SelectedValue = 0;
            CboUniMed.SelectedValue = 0;
            CboSup.SelectedValue = 0;
            CboRec.SelectedValue = 0;
            CboLin.SelectedValue = 0;
            
            TxtNumPed.Text = "";
            LblIdDocRef.Text = "";
            TxtNumRuc.Text = "";
            TxtCliente.Text = "";
            LblidCliente.Text = "";

            DataTable dtResult = new DataTable();
            DataTable dtResLin = new DataTable();

            dtResult = funDatos.DataTableFiltrar(dtRecetas, "n_idpro = 99999");
            funDatos.ComboBoxCargarDataTable(CboRec, dtResult, "n_id", "c_des");

            dtResLin = funDatos.DataTableFiltrar(dtLineas, "n_idpro = 99999");
            funDatos.ComboBoxCargarDataTable(CboLin, dtResLin, "n_id", "c_deslin");

            lstInsumos.Clear();
        }
        void Bloquea()
        {
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtFchReg.Enabled = !TxtFchReg.Enabled;
            TxtCanPro.Enabled = !TxtCanPro.Enabled;
            TxtNumLot.Enabled = !TxtNumLot.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
            TxtPro.Enabled = !TxtPro.Enabled;
            TxtHorIni.Enabled = !TxtHorIni.Enabled;
            TxtHorFin.Enabled = !TxtHorFin.Enabled;

            TxtCanProducida.Enabled = !TxtCanProducida.Enabled;
            TxtFchTerProd.Enabled = !TxtFchTerProd.Enabled;
            TxtPorRen.Enabled = !TxtPorRen.Enabled;
            TxtCanMatPri.Enabled = !TxtCanMatPri.Enabled;

            OptTipPro1.Enabled = !OptTipPro1.Enabled;
            OptTipPro2.Enabled = !OptTipPro2.Enabled;

            CboSup.Enabled = !CboSup.Enabled;
            CboRec.Enabled = !CboRec.Enabled;
            CboLin.Enabled = !CboLin.Enabled;
            CboTipDocRef.Enabled = !CboTipDocRef.Enabled;

            CboBusDocRef.Enabled = !CboBusDocRef.Enabled;
            CmdBusCli.Enabled = !CmdBusCli.Enabled;

            CmdAddTar.Enabled = !CmdAddTar.Enabled;
            CmdDelTar.Enabled = !CmdDelTar.Enabled;
            CmdBusNotIng.Enabled = !CmdBusNotIng.Enabled;
            ChkCerrar.Enabled = !ChkCerrar.Enabled;
            
            //CmdAddInsReal.Enabled = !CmdAddInsReal.Enabled;
            CmdBusProd.Enabled = !CmdBusProd.Enabled;
            CmdBusOrdPro.Enabled = !CmdBusOrdPro.Enabled;

            //
            if (BtnAgregarDetalle.Enabled == ComponentFactory.Krypton.Toolkit.ButtonEnabled.False)
            {
                BtnAgregarDetalle.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;
            }
            else
            {
                BtnAgregarDetalle.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            }
            if (BtnQuitarDetalle.Enabled == ComponentFactory.Krypton.Toolkit.ButtonEnabled.False)
            {
                BtnQuitarDetalle.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;
            }
            else
            {
                BtnQuitarDetalle.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            }
        }
        void MostrarEstadoMes(int n_IdEmpresa, int n_IdMes)
        {
            obj_fungen.mysConec = mysConec;
            if (obj_fungen.EstadoPeriodo(n_IdEmpresa, n_IdMes, 12) == true)
            {
                ToolNuevo.Visible = false;
                //ToolModificar.Visible = false;
                TooModificar2.Visible = false;
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
                //ToolModificar.Visible = true;
                TooModificar2.Visible = true;
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
            TooModificar2.Enabled = !TooModificar2.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolMenuImprimir.Enabled = !ToolMenuImprimir.Enabled;
            //ToolExportar.Enabled = !ToolExportar.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
        }
        void Modificar()
        {
            booAgregando = true;
            int n_cerrado = Convert.ToInt32(DgLista.Columns[1].CellValue(DgLista.Row).ToString());

            if (n_cerrado == 1)
            {
                MessageBox.Show("¡ No se puede modificar una produccion cerrada !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[2].CellValue(DgLista.Row).ToString());


            VerRegistro(intIdRegistro);
            
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            Tab02.SelectedIndex = 0;
            TxtFchReg.Focus();
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[2].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            int n_cerrado = Convert.ToInt32(DgLista.Columns[1].CellValue(DgLista.Row).ToString());

            if (n_cerrado == 1)
            {
                MessageBox.Show("¡ No se puede eliminar una produccion cerrada !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return booResult;
            }
            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                CN_pro_solicitudamateriales objSolMat = new CN_pro_solicitudamateriales();
                CN_pro_solicitudtareas objSolTar = new CN_pro_solicitudtareas();

                objSolMat.mysConec = mysConec;
                objSolTar.mysConec = mysConec;

                if (objSolMat.BuscarProduccionEnSolicitud(intIdRegistro, STU_SISTEMA.EMPRESAID) == true)
                {
                    MessageBox.Show("¡ No se puede eliminar esta produccion porque esta referenciada a una solicitud de materiales !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return booResult;
                }
                else
                {
                    if (objSolTar.BuscarProduccionEnSolicitud(intIdRegistro, STU_SISTEMA.EMPRESAID) == true)
                    {
                        MessageBox.Show("¡ No se puede eliminar esta produccion porque esta referenciada a una solicitud de tareas !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return booResult;
                    }
                }

                if (objRegistro.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistro.mysConec = mysConec;
                    booResult = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
                    if (booResult == true)
                    {
                        dtListar = objRegistro.dtListar;
                        // MOSTRAMOS LOS DATOS EN LA GRILLA
                        ListarItems();
                    }
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistro.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (n_QueHace == 1)
            {
                booResultado = objRegistro.Insertar(ref entRegistro, lstInsumos);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistro.Actualizar(entRegistro, lstInsumos);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ No se pudo guardar el registro por el siguiente motivo ! " + objRegistro.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else {
                ListarItems();
            }
            //Se graban los detalles
            foreach (var produccionNotaIng in ProduccionNotaIngs)
            {
                produccionNotaIng.n_idproduccion = entRegistro.n_id;
                produccionNotaIng.Save();
            }
            //Se eliminan los detalles borrados
            foreach (var produccionNotaIng in ProduccionNotaIngs.GetRemoveItems())
            {
                produccionNotaIng.Delete();
            }


            return booResultado;
        }
        void AsignarEntidad()
        {
            entRegistro.n_idemp =  STU_SISTEMA.EMPRESAID;
            if (n_QueHace == 1)
            {
                entRegistro.n_id = 0;
            }
            else
            {
                entRegistro.n_id = entRegistro.n_id;
            }

            entRegistro.n_idtipdoc =Convert.ToInt32(CboTipDoc.SelectedValue);
            entRegistro.c_numser = TxtNumSer.Text ;
            entRegistro.c_numdoc = TxtNumDoc.Text;
            entRegistro.n_anotra =STU_SISTEMA.ANOTRABAJO;
            entRegistro.n_mestra = STU_SISTEMA.MESTRABAJO;
            entRegistro.n_idpro = Convert.ToInt32(LblIdIte.Text);
            entRegistro.n_idunimed = Convert.ToInt32(CboUniMed.SelectedValue);
            entRegistro.n_canpro = Convert.ToDouble(TxtCanPro.Text);
            entRegistro.n_idres = Convert.ToInt32(CboSup.SelectedValue);
            entRegistro.d_fchpro = Convert.ToDateTime(TxtFchPro.Text);
            entRegistro.c_horini = TxtHorIni.Text;
            entRegistro.c_horfin = TxtHorFin.Text;
            entRegistro.c_numlot = TxtNumLot.Text ;
            entRegistro.c_obs = TxtObs.Text;
            entRegistro.n_idrec = Convert.ToInt32(CboRec.SelectedValue);
            entRegistro.n_idlin = Convert.ToInt32(CboLin.SelectedValue);
            if (Convert.ToInt32(CboTipDocRef.SelectedValue) != 0)
            {
                entRegistro.n_docrefidtipdoc = Convert.ToInt32(CboTipDocRef.SelectedValue);
            }
            if (LblIdOP.Text != "")
            { 
                entRegistro.n_docrefid = Convert.ToInt32(LblIdOP.Text);
            }
            entRegistro.c_docrefnumser = TxtNumSerDocRef.Text;
            entRegistro.c_docrefnumdoc = TxtNumDocRef.Text;
            entRegistro.d_fchreg = Convert.ToDateTime(TxtFchReg.Text);
            entRegistro.n_idsup = Convert.ToInt32(CboSup.SelectedValue);
            entRegistro.n_canprorea = Convert.ToDouble(funFunciones.NulosN(TxtCanProducida.Text));
            entRegistro.n_caningmp = Convert.ToDouble(funFunciones.NulosN(TxtCanMatPri.Text));

            if (OptTipPro1.Checked == true) { entRegistro.n_tipreg = 1; }
            if (OptTipPro2.Checked == true) { entRegistro.n_tipreg = 2; }
            entRegistro.n_idnoting = 0;
            entRegistro.n_renpor = 0;

            entRegistro.n_idcli = Convert.ToInt32(funFunciones.NulosN(LblidCliente.Text));
            entRegistro.n_idped = Convert.ToInt32(funFunciones.NulosN(LblIdDocRef.Text));

            entRegistro.n_conmp  = Convert.ToDouble(funFunciones.NulosN(TxtCanPagar.Text));

            if (OptTipPro1.Checked == true)
            {
                entRegistro.n_idnoting = Convert.ToInt32(funFunciones.NulosN(LblIdNotIng.Text));
                entRegistro.n_renpor = Convert.ToDouble(funFunciones.NulosN(TxtPorRen.Text));
            }
            
            if (TxtFchTerProd.Text != " ")
            {
                entRegistro.d_fchterpro = Convert.ToDateTime(TxtFchTerProd.Text);
            }

            if (n_QueHace == 1)
            {
                entRegistro.n_idestado = 1;
                entRegistro.n_idestsol = 1;
                entRegistro.n_idesttar = 1;
            }
            else
            {
                if (ChkCerrar.Checked == true)
                {
                    entRegistro.n_idestado = 2;
                }
                else 
                {
                    entRegistro.n_idestado = 1;
                }
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (TxtNumSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie del programa de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumSer.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento del programa de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumDoc.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtFchReg.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de registro !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchReg.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt32(CboSup.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado en nombre del supervisor de la produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboSup.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtCanPro.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la cantidad del producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtCanPro.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtNumLot.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de lote de la produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumLot.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtHorIni.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la hora de inicio de la produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtHorIni.Focus();
                booEstado = false;
                return booEstado;
            }
            if (TxtHorFin.Text == "")
            {
                MessageBox.Show("¡ No ha especificado de termino de la produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtHorFin.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt32(CboRec.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado en nombre de la receta para elaborar este producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboRec.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt32(CboLin.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado en nombre de la linea para elaborar este producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboLin.Focus();
                booEstado = false;
                return booEstado;
            }

            if (OptTipPro1.Checked == true)
            {
                if (Convert.ToInt32(funFunciones.NulosN(LblIdNotIng.Text)) != 0)
                {
                    if (Convert.ToDouble(funFunciones.NulosN(TxtPorRen.Text)) == 0)
                    {
                        MessageBox.Show("¡ No ha especificado el porcentaje de rendimiento de la materia prima !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        TxtPorRen.Focus();
                        booEstado = false;
                        return booEstado;
                    }
                }

                //if (Convert.ToDouble(funFunciones.NulosN(TxtPorRen.Text)) != 0)
                //{
                //    if (Convert.ToInt32(funFunciones.NulosN(LblIdNotIng.Text)) == 0)
                //    {
                //        MessageBox.Show("¡ No ha especificado la nota de ingreso que involucra la materia prima !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //        CmdBusNotIng.Focus();
                //        booEstado = false;
                //        return booEstado;
                //    }
                //}
            }
            return booEstado;
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
            EliminarRegistro();
        }
        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            bool b_Result = false;

            if (Grabar() == true)
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objRegistro.mysConec = mysConec;
                b_Result = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
                if (b_Result == true)
                {
                    dtListar = objRegistro.dtListar;
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }

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
            objFormVis = null;

            this.Close();
        }
        private void FrmRegistroProduccion_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (dtListar.Rows.Count == 0)
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
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            if (DgLista.RowCount == 0) { return; }
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[2].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            VerRegistro(intIdRegistro);
            booAgregando = false;
        }
        private void DgLista_KeyPress(object sender, KeyPressEventArgs e)
        {
              if (e.KeyChar == 13)
            {
                DataTable dtResult = new DataTable();

                string c_CadFiltro = funDbGrid.DG_LeerCondicionesFiltro(DgLista);
                dtResult = funDbGrid.DG_Filtrar(dtListar, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
        }
        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
        }

        private void Tab1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;

            if (n_QueHace != 3) { return; }

            if (tc.SelectedIndex == 1)
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns[2].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }

        private void FrmRegistroProduccion_Resize(object sender, EventArgs e)
        {
            //Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
        }
        private void label12_Click(object sender, EventArgs e)
        {

        }
        private void CmdAddOP_Click(object sender, EventArgs e)
        {
            BuscarOrdenProduccion();
        }
        void BuscarOrdenProduccion()
        {
            string[,] arrCabeceraDg1 = new string[6, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            //string c_cadin = funFlex.Flex_CadenaIN(FgLisOrdPro, 6, 2);
            CN_pro_ordenproduccion objOrdProduccion = new CN_pro_ordenproduccion();

            DataTable dtOrdenProdPend = new DataTable();

            objOrdProduccion.mysConec = mysConec;
            if (objOrdProduccion.TraeOrdenProduccionPendientes(STU_SISTEMA.EMPRESAID, "") == false)
            {
                MessageBox.Show("No se pudieron traer las ordenes de produccion pendiente; por el siguiente motivo : " + objOrdProduccion.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            dtOrdenProdPend = objOrdProduccion.dtOrdenProdPendientes;

            arrCabeceraDg1[0, 0] = "Nº Orden Produccion";
            arrCabeceraDg1[0, 1] = "120";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_numdoc";

            arrCabeceraDg1[1, 0] = "Solicitante";
            arrCabeceraDg1[1, 1] = "300";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_resapenom";

            arrCabeceraDg1[2, 0] = "Fecha";
            arrCabeceraDg1[2, 1] = "80";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "d_fchemi";

            arrCabeceraDg1[3, 0] = "Prioridad";
            arrCabeceraDg1[3, 1] = "60";
            arrCabeceraDg1[3, 2] = "C";
            arrCabeceraDg1[3, 3] = "c_prides";

            arrCabeceraDg1[4, 0] = "Nº Items";
            arrCabeceraDg1[4, 1] = "60";
            arrCabeceraDg1[4, 2] = "N";
            arrCabeceraDg1[4, 3] = "n_numite";

            arrCabeceraDg1[5, 0] = "Id";
            arrCabeceraDg1[5, 1] = "0";
            arrCabeceraDg1[5, 2] = "N";
            arrCabeceraDg1[5, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            xFun.Buscar_CampoOrden = "c_numdoc";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtOrdenProdPend);

            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }

            List<BE_PRO_ORDENPRODUCCIONDET> lstOrdeProddet = new List<BE_PRO_ORDENPRODUCCIONDET>();

            objOrdProduccion.TraerRegistro(Convert.ToInt32(dtResult.Rows[0]["n_id"]));
            if (objOrdProduccion.IntErrorNumber == 0)
            {
                LblIdOP.Text = dtResult.Rows[0]["n_id"].ToString();
                TxtNumSerDocRef.Text = dtResult.Rows[0]["c_numdoc"].ToString().Substring(0, 4);
                TxtNumDocRef.Text = dtResult.Rows[0]["c_numdoc"].ToString().Substring(5, 10);
            }
            else
            {
                MessageBox.Show("No se pudieron traer las ordenes de produccion pendiente; por el siguiente motivo : " + objOrdProduccion.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
        }
        private void FgProducto_RowColChange(object sender, EventArgs e)
        {

        }
        private void label25_Click(object sender, EventArgs e)
        {

        }
        private void CmdBusOrdPro_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            BuscarOrdenProduccion();
        }
        private void CmdBusProd_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            if (Convert.ToInt32(CboTipDocRef.SelectedValue) == 0)
            {
                BuscarProducto();
            }
            else
            {
                MostrarProgramacion();
            }
        }
        void BuscarProducto()
        {
            string[,] arrCabeceraDg1 = new string[3, 4];
            DataTable dtResult = new DataTable();
            
            FgDoc.Rows.Count = 2;
            FgTar.Rows.Count = 2;
            FgInsumos.Rows.Count = 2;

            CN_alm_inventario objItems = new CN_alm_inventario();

            objItems.mysConec = mysConec;
            dtResult = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);

            dtResult = funDatos.DataTableFiltrar(dtResult, "n_idtipexi = 2");

            arrCabeceraDg1[0, 0] = "Producto";
            arrCabeceraDg1[0, 1] = "500";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_despro";

            arrCabeceraDg1[1, 0] = "Uni. Med.";
            arrCabeceraDg1[1, 1] = "60";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_abrpre";

            arrCabeceraDg1[2, 0] = "n_id";
            arrCabeceraDg1[2, 1] = "0";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            xFun.Buscar_CampoOrden = "c_despro";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }

            booAgregando = true;
            TxtPro.Text = dtResult.Rows[0]["c_despro"].ToString();
            LblIdIte.Text = dtResult.Rows[0]["n_id"].ToString();
            CboUniMed.SelectedValue = Convert.ToInt32(dtResult.Rows[0]["n_idunimed"]);
            TxtNumLot.Text = Convert.ToDateTime(TxtFchPro.Text).ToString("ddMMyyyy");

            dtResult = funDatos.DataTableFiltrar(dtRecetas, "n_idpro = " + Convert.ToInt32(LblIdIte.Text) + "");
            funDatos.ComboBoxCargarDataTable(CboRec, dtResult, "n_id", "c_des");

            DataTable dtResLin = new DataTable();
            dtResLin = funDatos.DataTableFiltrar(dtLineas, "n_idpro = " + Convert.ToInt32(LblIdIte.Text) + "");
            funDatos.ComboBoxCargarDataTable(CboLin, dtResLin, "n_id", "c_deslin");
            booAgregando = false;
        }
        void MostrarProgramacion()
        { 
            if (LblIdOP.Text == "") 
            {
                MessageBox.Show("¡ No ha especificado el numero de orden de produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return; 
            }

            string[,] arrCabeceraDg1 = new string[6, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            int n_row = 0;
            string c_FchEnt = "";

            FgDoc.Rows.Count = 2;
            FgTar.Rows.Count = 2;
            FgInsumos.Rows.Count = 2;

            CN_pro_ordenproduccion objOrdProduccion = new CN_pro_ordenproduccion();

            DataTable dtOrdenProdProdPend = new DataTable();

            objOrdProduccion.mysConec = mysConec;
            if (objOrdProduccion.TraeOrdenProduccionProductosPendientes(Convert.ToInt32(LblIdOP.Text)) == false)
            {
                MessageBox.Show("No se pudieron traer los productos de la orden de produccion Nº "+TxtNumSerDocRef.Text+"-"+TxtNumDocRef +" por el siguiente error : "+ objOrdProduccion.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            dtOrdenProdProdPend = objOrdProduccion.dtOrdenProdProdtPend;
            ///CALL pro_ordenproduccion_obtenerordprodprodpend(2)

            arrCabeceraDg1[0, 0] = "Cod. Producto";
            arrCabeceraDg1[0, 1] = "115";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_codpro";

            arrCabeceraDg1[1, 0] = "Producto";
            arrCabeceraDg1[1, 1] = "350";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_despro";

            arrCabeceraDg1[2, 0] = "Uni. Med";
            arrCabeceraDg1[2, 1] = "40";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "c_abrpre";

            arrCabeceraDg1[3, 0] = "Cantidad";
            arrCabeceraDg1[3, 1] = "70";
            arrCabeceraDg1[3, 2] = "D";
            arrCabeceraDg1[3, 3] = "n_can";

            arrCabeceraDg1[4, 0] = "Fch. Ent.";
            arrCabeceraDg1[4, 1] = "80";
            arrCabeceraDg1[4, 2] = "F";
            arrCabeceraDg1[4, 3] = "d_fchent";

            arrCabeceraDg1[5, 0] = "IdItem";
            arrCabeceraDg1[5, 1] = "0";
            arrCabeceraDg1[5, 2] = "C";
            arrCabeceraDg1[5, 3] = "n_idite";

            //arrCabeceraDg1[0, 0] = "T. D.";
            //arrCabeceraDg1[0, 1] = "350";
            //arrCabeceraDg1[0, 2] = "C";
            //arrCabeceraDg1[0, 3] = "c_prodes";

            //arrCabeceraDg1[1, 0] = "Uni. Med.";
            //arrCabeceraDg1[1, 1] = "60";
            //arrCabeceraDg1[1, 2] = "C";
            //arrCabeceraDg1[1, 3] = "c_unimeddes";

            //arrCabeceraDg1[2, 0] = "Cantidad";
            //arrCabeceraDg1[2, 1] = "80";
            //arrCabeceraDg1[2, 2] = "D";
            //arrCabeceraDg1[2, 3] = "n_can";

            //arrCabeceraDg1[3, 0] = "Receta";
            //arrCabeceraDg1[3, 1] = "80";
            //arrCabeceraDg1[3, 2] = "C";
            //arrCabeceraDg1[3, 3] = "";

            //arrCabeceraDg1[4, 0] = "Linea ";
            //arrCabeceraDg1[4, 1] = "80";
            //arrCabeceraDg1[4, 2] = "C";
            //arrCabeceraDg1[4, 3] = "n_numite";

            //arrCabeceraDg1[5, 0] = "Fch. Prod. Pro.";
            //arrCabeceraDg1[5, 1] = "80";
            //arrCabeceraDg1[5, 2] = "F";
            //arrCabeceraDg1[5, 3] = "d_fchpro";

            //arrCabeceraDg1[6, 0] = "n_idproducto";
            //arrCabeceraDg1[6, 1] = "0";
            //arrCabeceraDg1[6, 2] = "N";
            //arrCabeceraDg1[6, 3] = "n_idite";

            //arrCabeceraDg1[7, 0] = "n_idReceta";
            //arrCabeceraDg1[7, 1] = "0";
            //arrCabeceraDg1[7, 2] = "N";
            //arrCabeceraDg1[7, 3] = "n_idrec";

            //arrCabeceraDg1[8, 0] = "c_llave";
            //arrCabeceraDg1[8, 1] = "0";
            //arrCabeceraDg1[8, 2] = "C";
            //arrCabeceraDg1[8, 3] = "c_llave";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_idite";
            xFun.Buscar_CadFiltro = "";
            xFun.Buscar_CampoOrden = "c_despro";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtOrdenProdProdPend);

            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }

            if (objOrdProduccion.IntErrorNumber == 0)
            {
                TxtPro.Text = dtResult.Rows[0]["c_despro"].ToString();
                CboUniMed.SelectedValue = dtResult.Rows[0]["n_idunimed"].ToString();
               // TxtHorIni.Text = dtResult.Rows[0]["h_horini"].ToString();
                //TxtHorFin.Text = dtResult.Rows[0]["h_horfin"].ToString();
                TxtCanPro.Text = dtResult.Rows[0]["n_can"].ToString();
                //TxtNumLot.Text = dtResult.Rows[0][""].ToString();
                LblIdIte.Text  = dtResult.Rows[0]["n_idite"].ToString();
                //LblIdLinea.Text = dtResult.Rows[0]["n_idlin"].ToString();
                LblIdReceta.Text = dtResult.Rows[0]["n_idrec"].ToString();
                CboRec.SelectedValue = Convert.ToInt32(dtResult.Rows[0]["n_idrec"].ToString());
                //CboLin.SelectedValue = Convert.ToInt32(dtResult.Rows[0]["n_idlin"].ToString());
                //TxtFchPro.Text = Convert.ToDateTime(dtResult.Rows[0]["d_fchpro"]).ToString("dd/MM/yyyy");
                c_FchEnt = dtResult.Rows[0]["d_fchent"].ToString();

                TxtNumLot.Text = Convert.ToDateTime(dtResult.Rows[0]["d_fchent"]).ToString("ddMMyyyy");

                dtResult = funDatos.DataTableFiltrar(dtRecetas, "n_idpro = " + Convert.ToInt32(LblIdIte.Text) + "");
                funDatos.ComboBoxCargarDataTable(CboRec, dtResult, "n_id", "c_des");
                CboRec.SelectedValue = Convert.ToInt32(LblIdReceta.Text);

                DataTable dtResLin = new DataTable();
                dtResLin = funDatos.DataTableFiltrar(dtLineas, "n_idpro = " + Convert.ToInt32(LblIdIte.Text) + "");
                funDatos.ComboBoxCargarDataTable(CboLin, dtResLin, "n_id", "c_deslin");

                CN_pro_solicitudamateriales funSM = new CN_pro_solicitudamateriales ();
                CN_pro_produccion funProd = new CN_pro_produccion();

                funSM.mysConec = mysConec;
                funSM.Consulta2(Convert.ToInt32(LblIdOP.Text), Convert.ToInt32(LblIdIte.Text), Convert.ToInt32(LblIdReceta.Text), c_FchEnt);
                dtResult = funSM.dtLista;
                //lstInsumosDet.Clear();
                //lstTareasDet.Clear();
                //CargarListaInsumos(funSM.dtlstInsumos); 
                                
                booAgregando = true;
                for(n_row = 0; n_row <= dtResult.Rows.Count-1; n_row++)
                {
                    FgDoc.Rows.Count = FgDoc.Rows.Count + 1;
                    FgDoc.SetData(FgDoc.Rows.Count - 1, 1, dtResult.Rows[n_row]["c_tipdocdes"].ToString());
                    FgDoc.SetData(FgDoc.Rows.Count - 1, 2, dtResult.Rows[n_row]["c_numdoc"].ToString());
                    FgDoc.SetData(FgDoc.Rows.Count - 1, 3, dtResult.Rows[n_row]["d_fchent"].ToString());
                    FgDoc.SetData(FgDoc.Rows.Count - 1, 4, "0");
                    FgDoc.SetData(FgDoc.Rows.Count - 1, 5, dtResult.Rows[n_row]["n_id"].ToString());
                    FgDoc.SetData(FgDoc.Rows.Count - 1, 6, dtResult.Rows[n_row]["n_idtipdoc"].ToString());
                }
                if (dtResult.Rows.Count != 0)
                { 
                    //MostrarInsumos(Convert.ToInt32(dtResult.Rows[0]["n_id"].ToString()));
                }

                //// *********************************
                //// MOSTRAMOS LAS TAREAS DEL PRODUCTO
                //funProd.mysConec = mysConec;
                //funProd.TraerTareasLinea(Convert.ToInt32(LblIdIte.Text), Convert.ToInt32(LblIdReceta.Text), Convert.ToInt32(LblIdLinea.Text));
                //dtResult = funProd.dtListar;

                //for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                //{
                //    FgTar.Rows.Count = FgTar.Rows.Count + 1;
                //    FgTar.SetData(FgTar.Rows.Count - 1, 1, dtResult.Rows[n_row]["c_des"].ToString());
                //    FgTar.SetData(FgTar.Rows.Count - 1, 2, dtResult.Rows[n_row]["c_abr"].ToString());
                //    FgTar.SetData(FgTar.Rows.Count - 1, 5, dtResult.Rows[n_row]["n_numpertar"].ToString());
                //    FgTar.SetData(FgTar.Rows.Count - 1, 7, dtResult.Rows[n_row]["n_idtar"].ToString());
                //}

                booAgregando = false;
            }
            else
            {
                MessageBox.Show("No se pudieron traer las ordenes de produccion pendiente; por el siguiente motivo : " + objOrdProduccion.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
        }
        //void CargarListaInsumos(DataTable dtListaInsumos)
        //{ 
        //    int n_row = 0;

        //    //dtListaInsumos
        //    for (n_row = 0; n_row <= dtListaInsumos.Rows.Count - 1; n_row++)
        //    { 
        //        BE_PRO_PRODUCCIONINSUMOSDET entInsDet = new BE_PRO_PRODUCCIONINSUMOSDET();

        //        entInsDet.n_idpro = 0;
        //        entInsDet.n_idins = 0;
        //        entInsDet.n_idsolmat = Convert.ToInt32(dtListaInsumos.Rows[n_row]["n_idsol"]);
        //        entInsDet.n_idite = Convert.ToInt32(dtListaInsumos.Rows[n_row]["n_idite"]);
        //        entInsDet.n_idunimed = Convert.ToInt32(dtListaInsumos.Rows[n_row]["n_idunimed"]);
        //        entInsDet.n_canteo = Convert.ToDouble(dtListaInsumos.Rows[n_row]["n_canteo"]);
        //        entInsDet.n_canrea = Convert.ToDouble(dtListaInsumos.Rows[n_row]["n_canent"]);
        //        entInsDet.n_valdes = Convert.ToDouble(dtListaInsumos.Rows[n_row]["n_dife"]);
        //        entInsDet.n_pordes = Convert.ToDouble(dtListaInsumos.Rows[n_row]["n_porefi"]);
        //        entInsDet.n_valite = Convert.ToDouble(dtListaInsumos.Rows[n_row]["n_impval"]);

        //        lstInsumosDet.Add(entInsDet);
        //    }
        //}
        //void MostrarInsumos(int n_IdSolicitud)
        //{
        //    DataTable dtResult = new DataTable();
        //    int n_row = 0;
        //    double n_valor = 0;
        //    string c_dato = "";

        //    FgInsumos.Rows.Count = 2;

        //    if (lstInsumosDet.Count != 0)
        //    {
        //        for (n_row = 0; n_row <= lstInsumosDet.Count - 1; n_row++)
        //        {
        //            if (lstInsumosDet[n_row].n_idsolmat == n_IdSolicitud)
        //            { 
        //                FgInsumos.Rows.Count = FgInsumos.Rows.Count + 1;
        //                c_dato = funDatos.DataTableBuscar(dtItems,"n_id","c_despro",lstInsumosDet[n_row].n_idite.ToString(),"N").ToString();
        //                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 1, c_dato);

        //                c_dato = funDatos.DataTableBuscar(dtUniMedSunat, "n_id", "c_abr", lstInsumosDet[n_row].n_idunimed.ToString(), "N").ToString();
        //                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 2, c_dato);

        //                n_valor = Convert.ToDouble(lstInsumosDet[n_row].n_canteo);
        //                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 3, n_valor.ToString("0.000000"));

        //                n_valor = Convert.ToDouble(lstInsumosDet[n_row].n_canrea);
        //                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 4, n_valor.ToString("0.000000"));

        //                n_valor = Convert.ToDouble(lstInsumosDet[n_row].n_canteo - lstInsumosDet[n_row].n_canrea);
        //                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 5, n_valor.ToString("0.00"));
                       
        //                n_valor = Convert.ToDouble(lstInsumosDet[n_row].n_pordes);
        //                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 6, n_valor.ToString("0.00"));
                        
        //                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 7, lstInsumosDet[n_row].n_idite.ToString());
        //                FgInsumos.SetData(FgInsumos.Rows.Count - 1, 8, lstInsumosDet[n_row].n_idunimed.ToString());
        //            }
        //        }
        //    }
        //}
        private void CmdAddDoc_Click(object sender, EventArgs e)
        {

        }
        private void FgDoc_RowColChange(object sender, EventArgs e)
        {
            ////if (n_QueHace == 3) { return; }
            //if (booAgregando == true) { return; }
            //if (FgDoc.Rows.Count == 2) { return; }

            //int n_idsol = Convert.ToInt32(FgDoc.GetData(FgDoc.Row,5).ToString());
            //MostrarInsumos(n_idsol);
        }
        private void TxtObs_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtHorIni_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtHorFin_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtCanPro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                double n_val = Convert.ToDouble(funFunciones.NulosN(TxtCanPro.Text));
                TxtCanPro.Text = n_val.ToString("0.00");
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
        private void TxtNumLot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
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
        private void imprimirRecetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[2].CellValue(DgLista.Row).ToString());
            CN_pro_produccion FunSol = new CN_pro_produccion();
            FunSol.mysConec = mysConec;
            FunSol.STU_SISTEMA = STU_SISTEMA;
            FunSol.ReporteTareas(intIdRegistro);
        }
        private void CmdAddTar_Click(object sender, EventArgs e)
        {
            //if (n_QueHace == 3) { return; }
            
            //bool b_result = false;
            //int n_row = 0;
            //DataTable dtResult = new DataTable();
            //string c_Condicion = " NOT IN (";

            //for (n_row = 2; n_row <= FgTar.Rows.Count - 1; n_row++)
            //{
            //    if (funFunciones.NulosC(FgTar.GetData(n_row, 1)) != "")
            //    {
            //        if (n_row > 2) { c_Condicion = c_Condicion + ","; }
            //        c_Condicion = c_Condicion + FgTar.GetData(n_row, 7).ToString();
            //    }
            //}
            //c_Condicion = c_Condicion + ")";

            //int n_idite = Convert.ToInt32(LblIdIte.Text);
            //int n_idrec = Convert.ToInt32(LblIdReceta.Text);
            //int n_idlin = Convert.ToInt32(LblIdLinea.Text);
            //objLinTar.mysConec = mysConec;
            //b_result = objLinTar.Consulta1(n_idite, n_idrec, n_idlin, c_Condicion);
            //if (b_result == true)
            //{
            //    dtLinTar = objLinTar.dtLineasTar;
            //}

            //if (dtLinTar.Rows.Count == 0)
            //{
            //    MessageBox.Show("La linea no tiene tareas opcionales, modifique la linea para poder seleccionar una tarea opcional ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //}
            //else
            //{ 
            //    FgTar.Rows.Count = FgTar.Rows.Count + 1;
            //}
        }
        private void FgTar_EnterCell(object sender, EventArgs e)
        {
            //if (booAgregando == true) { return; }
            //if (FgTar.Rows.Count == 2) { return; }
            //if (n_QueHace == 3)
            //{
            //    FgTar.AllowEditing = false; return;
            //}
            //if (booAgregando == true) { return; }
                        
            //if (FgTar.Col == 1)
            //{
            //    funFlex.FlexColumnaCombo(FgTar, dtLinTar, "c_tardes", 1);
            //}
        }
        private void FgTar_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            //if (booAgregando == true) { return; }
            //if (FgTar.Rows.Count == 2) { return; }
            
            //string c_dato = "";
            
            //if (FgTar.Col == 1)
            //{
            //    c_dato = FgTar.GetData(FgTar.Row, 1).ToString();
            //    c_dato = funDatos.DataTableBuscar(dtTareas, "c_des", "n_id", c_dato, "C").ToString();
            //    FgTar.SetData(FgTar.Row, 7, c_dato);

            //}
        }
        private void CmdDelTar_Click(object sender, EventArgs e)
        {
            //if (booAgregando == true) { return; }
            //if (FgTar.Rows.Count == 2) { return; }

            //if (n_QueHace == 3) { FgTar.AllowEditing = false; return; }

            //FgTar.RemoveItem(FgTar.Row);
        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);

            objRegistro.mysConec = mysConec;

            if (objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
            {
                dtListar = objRegistro.dtListar;
            }

            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            ListarItems();

            if (dtListar.Rows.Count == 0)
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

        private void CboLin_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (booAgregando == true) { return; }
            //if (n_QueHace == 3) { return; }
            //if (Convert.ToInt32(CboRec.SelectedValue) == 0) 
            //{
            //    MessageBox.Show("Debe de seleccionar una receta para poder seleccionar una linea", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //    return;
            //}
            //// *********************************
            //// MOSTRAMOS LAS TAREAS DEL PRODUCTO
            //CN_pro_produccion funProd = new CN_pro_produccion();
            //DataTable dtResult = new DataTable();
            //int n_row = 0;

            //FgTar.Rows.Count = 2;
            //funProd.mysConec = mysConec;
            //funProd.TraerTareasLinea(Convert.ToInt32(LblIdIte.Text), Convert.ToInt32(CboRec.SelectedValue), Convert.ToInt32(CboLin.SelectedValue));
            //dtResult = funProd.dtListar;

            //for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
            //{
            //    FgTar.Rows.Count = FgTar.Rows.Count + 1;
            //    FgTar.SetData(FgTar.Rows.Count - 1, 1, dtResult.Rows[n_row]["c_des"].ToString());
            //    FgTar.SetData(FgTar.Rows.Count - 1, 2, dtResult.Rows[n_row]["c_abr"].ToString());
            //    FgTar.SetData(FgTar.Rows.Count - 1, 5, dtResult.Rows[n_row]["n_numpertar"].ToString());
            //    FgTar.SetData(FgTar.Rows.Count - 1, 7, dtResult.Rows[n_row]["n_idtar"].ToString());
            //}

        }

        private void TxtFchPro_Validated(object sender, EventArgs e)
        {
            if (TxtFchPro.Text == "") { return; }
            TxtNumLot.Text = Convert.ToDateTime(TxtFchPro.Text).ToString("ddMMyyyy");
        }

        private void CmdVerDoc_Click(object sender, EventArgs e)
        {
            ////if (FgLisPro.GetData(FgLisPro.Row, 10).ToString() == "SOLICITADO")
            ////{
            ////    MessageBox.Show("! Ya se emitio la solicitud de materiales para este producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            ////    return;
            ////}
            //BE_PRO_SOLICITUDMATERIALES entSolicitud = new BE_PRO_SOLICITUDMATERIALES();
            //List<BE_PRO_SOLICITUDMATERIALESDET> lstSolicitudDet = new List<BE_PRO_SOLICITUDMATERIALESDET>();

            //DataTable dtResult = new DataTable();
            //int n_row = 0;
            //int n_tipo = 0;
            //int n_idPro = Convert.ToInt32(LblIdIte.Text);                   // ID DEL PRODUCTO
            //int n_idRec = Convert.ToInt32(LblIdReceta);                     // ID DE OLA RECETA
            ////int n_idordpro = Convert.ToInt32(FgLisPro.GetData(FgLisPro.Row, 9).ToString());         // ID DE LA ORDEN DE PRODUCCION
            //int n_idres = Convert.ToInt32(CboSup.SelectedValue);            // ID DEL RESPONSABLE DE LA PRODUCCION
            //double n_canpro = Convert.ToDouble(TxtCanPro.Text);             // CANTIDAD DEL PRODUCTO
            //string c_fchent = TxtFchPro.Text;                               // FECHA DE ENTREGA        
            //string c_numdoc = "";

            //dtResult = funDatos.DataTableFiltrar(dtRecetas, "n_id = " + n_idRec + "");

            //    dtResult = funDatos.DataTableFiltrar(dtRecetasInsumos, "n_idrec = " + n_idRec + "");                                 // FILTRAMOS LOS INSUMOS DE LA RECETA
            //    dtResult = funDatos.DataTableFiltrar(dtResult, "n_idtipexi = " + dtTipExi.Rows[n_tipo]["n_id"].ToString() + "");     // FILTRAMOS LOS ITEMS DEL TIPO DE ITEM ACTUAL
            //    if (dtResult.Rows.Count != 0)                                                               // SI EXISTE ITEMS DE LA RECETA CON EL TIPO DE DE EXISTENCIA ACTUAL, IMPRIMIMOS UNA SOLICITUD
            //    {
            //        objTipDoc.mysConec = mysConec;
            //        c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 72, "0001");                   // MOSTRAMOS EL ULTIMO NUMERO DE LA PROGRAMACION

            //        // CREAMOS LA CABECERA DE LA SOLICITUD
            //        entSolicitud.n_idemp = STU_SISTEMA.EMPRESAID;
            //        entSolicitud.n_id = 0;
            //        entSolicitud.n_idtipdoc = 72;
            //        entSolicitud.c_numser = "0001";
            //        entSolicitud.c_numdoc = c_numdoc;
            //        entSolicitud.d_fchreg = DateTime.Now;
            //        entSolicitud.n_idsol = n_idres;                                                         // EL RESPONSABLE DE LA PRODUCCION SERA EL RESPONSABLE DE SOLICITAR LOS INSUMOS
            //        entSolicitud.n_idprogra = 0;
            //        entSolicitud.n_idordpro = 0;
            //        entSolicitud.n_idite = n_idPro;
            //        entSolicitud.n_idrec = n_idRec;
            //        entSolicitud.d_fchent = Convert.ToDateTime(c_fchent);
            //        entSolicitud.c_obs = "SOLICITUD GENERADA AUTOMATICAMENTE DEL PROGRAMA DE PRODUCCION";
            //        entSolicitud.n_anotra = STU_SISTEMA.ANOTRABAJO;
            //        entSolicitud.n_mestra = STU_SISTEMA.MESTRABAJO;
            //        entSolicitud.n_idalm = 0;
            //        entSolicitud.n_can = n_canpro;

            //        lstSolicitudDet.Clear();
            //        // CREAMOS EL DETALLE DE LA SOLICITUD               
            //        for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
            //        {
            //            BE_PRO_SOLICITUDMATERIALESDET entSolicitudDet = new BE_PRO_SOLICITUDMATERIALESDET();
            //            entSolicitudDet.n_idsol = 0;
            //            entSolicitudDet.n_idite = Convert.ToInt32(dtResult.Rows[n_row]["n_idite"]);
            //            entSolicitudDet.n_idunimed = Convert.ToInt32(dtResult.Rows[n_row]["n_idunimed"]);
            //            entSolicitudDet.n_canteo = Convert.ToDouble(dtResult.Rows[n_row]["n_can"]);
            //            entSolicitudDet.n_canent = Convert.ToDouble(dtResult.Rows[n_row]["n_can"]) * n_canpro;
            //            entSolicitudDet.c_numlot = "";
            //            entSolicitudDet.n_impval = 0;
            //            lstSolicitudDet.Add(entSolicitudDet);
            //        }

            //        CN_pro_solicitudamateriales xFunPro = new CN_pro_solicitudamateriales();
            //        xFunPro.mysConec = mysConec;
            //        if (xFunPro.Insertar(entSolicitud, lstSolicitudDet) == false)
            //        {
            //            MessageBox.Show("! No se pudo generar la solicituda de materuales pro el siguiente motivo : " + xFunPro.c_ErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //        }
            //    }
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click_1(object sender, EventArgs e)
        {

        }

        private void TxtFchTerProd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                TxtFchTerProd.Text = "";
                TxtFchTerProd.CustomFormat = " ";
                TxtFchTerProd.Format = DateTimePickerFormat.Custom;
            }
        }

        private void TxtFchTerProd_ValueChanged(object sender, EventArgs e)
        {
            TxtFchTerProd.CustomFormat = "dd/MM/yyyy";
            TxtFchTerProd.Format = DateTimePickerFormat.Custom;

        }
        private void CmdCanIns_Click(object sender, EventArgs e)
        {
            Tab1.Enabled = true;
            ToolHerramientas.Enabled = true;
            PnlInsumos.Visible = false;
        }
        private void CmdAddInsReal_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(funFunciones.NulosN(TxtCanProducida.Text)) == 0)
            {
                MessageBox.Show("! La cantidad producida no puede ser 0 !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtCanProducida.Focus();
                return;
            }

            //if (lstInsumos.Count == 0)
            //{
            //    CargarInsumos();
            //}
            CargarInsumos();
            MostrarInsumos();
            Tab1.Enabled = false;
            ToolHerramientas.Enabled = false;
            PnlInsumos.Left = ((this.Width - PnlInsumos.Width) / 2);
            PnlInsumos.Top = ((this.Height - PnlInsumos.Height) / 2);
            TxtProd2.Text = TxtPro.Text;
            TxtUniMed.Text = CboUniMed.Text;
            TxtCanProIns.Text = TxtCanProducida.Text;

            if (n_QueHace == 3)
            {
                CmdAceIns.Enabled = false;
            }
            else
            { 
                CmdAceIns.Enabled = true;
            }
            PnlInsumos.Visible = true;
        }
        void MostrarInsumos()
        {
            booAgregando = true;
            DataTable dtResul = new DataTable();
            int n_row = 0;
            string c_dato = "";

            FgInsumo.Rows.Count = 2;
            for (n_row = 0; n_row <= lstInsumos.Count - 1; n_row++)
            {
                FgInsumo.Rows.Count = FgInsumo.Rows.Count + 1;

                c_dato = lstInsumos[n_row].n_idins.ToString();
                c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "n_idtipexi", c_dato, "N").ToString();
                c_dato = funDatos.DataTableBuscar(dtTipExi, "n_id", "c_des", c_dato, "N").ToString();
                FgInsumo.SetData(FgInsumo.Rows.Count - 1, 1, c_dato);

                c_dato = lstInsumos[n_row].n_idins.ToString();
                c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", c_dato, "N").ToString();
                FgInsumo.SetData(FgInsumo.Rows.Count - 1, 2, c_dato);

                c_dato = lstInsumos[n_row].n_idunimed.ToString();
                c_dato = funDatos.DataTableBuscar(dtUniMedSunat, "n_id", "c_abr", c_dato, "N").ToString();
                FgInsumo.SetData(FgInsumo.Rows.Count - 1, 3, c_dato);

                c_dato = lstInsumos[n_row].n_canent.ToString("0.000000");
                FgInsumo.SetData(FgInsumo.Rows.Count - 1, 4, c_dato);

                c_dato = lstInsumos[n_row].n_canuti.ToString("0.000000");
                FgInsumo.SetData(FgInsumo.Rows.Count - 1, 5, c_dato);

                c_dato = (lstInsumos[n_row].n_canent - lstInsumos[n_row].n_canuti).ToString("0.000000");
                FgInsumo.SetData(FgInsumo.Rows.Count - 1, 6, c_dato);

                if (Convert.ToDouble(c_dato) < 0)
                {
                    funFlex.Flex_PintarCeldas(FgInsumo, FgInsumo.Rows.Count - 1, 6, FgInsumo.Rows.Count - 1, 6, Color.Red, Color.Transparent);
                }
                else
                {
                    if (Convert.ToDouble(c_dato) == 0)
                    {
                        funFlex.Flex_PintarCeldas(FgInsumo, FgInsumo.Rows.Count - 1, 6, FgInsumo.Rows.Count - 1, 6, Color.Black, Color.Transparent);
                    }
                    else
                    {
                        funFlex.Flex_PintarCeldas(FgInsumo, FgInsumo.Rows.Count - 1, 6, FgInsumo.Rows.Count - 1, 6, Color.Blue, Color.Transparent);
                    }
                }

                 
                c_dato = lstInsumos[n_row].n_idins.ToString();
                FgInsumo.SetData(FgInsumo.Rows.Count - 1, 7, c_dato);

                if (n_QueHace == 2)
                {
                    FgInsumo.SetData(FgInsumo.Rows.Count - 1, 8, 1);
                }
                else
                {
                    c_dato = lstInsumos[n_row].n_consumido.ToString();
                    FgInsumo.SetData(FgInsumo.Rows.Count - 1, 8, Convert.ToInt32(c_dato));
                }
            }
            booAgregando = false;
        }
        void CargarInsumos()
        {
            DataTable dtResul = new DataTable();
            DataTable dtSolMatRes = new DataTable();
            CN_pro_solicitudamateriales objSolMat = new CN_pro_solicitudamateriales();
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[2].CellValue(DgLista.Row).ToString());
            int n_row = 0;
            int n_fila = 0;
            bool b_encontro = false;
            List<BE_PRO_PRODUCCIONINS> lstInsumosReales = new List<BE_PRO_PRODUCCIONINS>();

            dtResul = funDatos.DataTableFiltrar(dtRecetasInsumos, "n_idrec = " + Convert.ToInt32(CboRec.SelectedValue).ToString() + "");

            objSolMat.mysConec = mysConec;

            if (objSolMat.ListarResumenSolMatGlobal(intIdRegistro, Convert.ToInt32(LblIdOP.Text), STU_SISTEMA.EMPRESAID) == true)
            {
                dtSolMatRes = objSolMat.dtListaMatEnt;
            }
            
            if (dtResul.Rows.Count != 0)
            {
                for (n_row = 0; n_row <= dtResul.Rows.Count - 1; n_row++)
                {
                    BE_PRO_PRODUCCIONINS entInsumo = new BE_PRO_PRODUCCIONINS();

                    entInsumo.n_idpro = intIdRegistro;
                    entInsumo.n_idins = Convert.ToInt32(dtResul.Rows[n_row]["n_idite"]);
                    entInsumo.n_canuti = 0;
                    entInsumo.n_idunimed = Convert.ToInt32(dtResul.Rows[n_row]["n_idunimed"]);

                    for (n_fila = 0; n_fila <= dtSolMatRes.Rows.Count - 1; n_fila++)
                    {
                        if (dtSolMatRes.Rows[n_fila]["n_idite"].ToString() == dtResul.Rows[n_row]["n_idite"].ToString())
                        {
                            entInsumo.n_canent = Convert.ToDouble(dtSolMatRes.Rows[n_fila]["n_cantot"]);
                            break;
                        }
                    }
                    
                    lstInsumosReales.Add(entInsumo);
                }
            }

            // ELIMINAMOSLOS INSUMOS QUE ESTEN GUARDADOS Y NO ESTEN EN LA NUEVA LISTA DE INSUMOS
            for (n_row = 0; n_row <= lstInsumos.Count - 1; n_row++)
            {
                b_encontro = false;
                for (n_fila = 0; n_fila <= lstInsumosReales.Count - 1; n_fila++)
                {
                    if (lstInsumos[n_row].n_idins == lstInsumosReales[n_fila].n_idins)
                    {
                        b_encontro = true;
                        break;
                    }
                }
                if (b_encontro == false)
                {
                    lstInsumos.RemoveAt(n_row);
                }
            }

            // ACTUALIZAMOS LOS INSUMOS PREVIAMENTE CARGADOS, SI FALTA U INSUMOS LO AGREGAMOS SI SOBRA LO ELIMINAMOS, SE TOMA COMO REAL LA SOLICITUDA DE MATERIAS
            // ACTUALIZAMOS O AGREGAMOS LOS INSUMOS REALES EN LOS INSUMOS GUARDADOS
            for (n_fila = 0; n_fila <= lstInsumosReales.Count - 1; n_fila++)
            { 
                for(n_row = 0; n_row <= lstInsumos.Count-1;n_row++)
                {
                    b_encontro = false;
                    if (lstInsumos[n_row].n_idins == lstInsumosReales[n_fila].n_idins)
                    {
                        b_encontro = true;
                        lstInsumos[n_row].n_canent = lstInsumosReales[n_fila].n_canent;
                        //lstInsumos[n_row].n_canuti = lstInsumosReales[n_fila].n_canuti;
                        break;
                    }
                }
                // SI NO ENCONTRO EL ITEM LOS AGREGAMOS
                if (b_encontro == false)
                { 
                    BE_PRO_PRODUCCIONINS enInsumos = new BE_PRO_PRODUCCIONINS();
                    enInsumos.n_canent = lstInsumosReales[n_fila].n_canent;
                    enInsumos.n_canuti = lstInsumosReales[n_fila].n_canuti;
                    enInsumos.n_dife = lstInsumosReales[n_fila].n_dife;
                    enInsumos.n_idins = lstInsumosReales[n_fila].n_idins;
                    enInsumos.n_idpro = lstInsumosReales[n_fila].n_idpro;
                    enInsumos.n_idunimed = lstInsumosReales[n_fila].n_idunimed;
                    lstInsumos.Add(enInsumos);
                }
            }
        }
        private void FgInsumo_EnterCell(object sender, EventArgs e)
        {
            if (booAgregando == true) {return;}
            if (n_QueHace == 3) { return; }

            FgInsumo.AllowEditing = false;
            if (FgInsumo.Col == 5)
            {
                FgInsumo.AllowEditing = true;
            }
            if (FgInsumo.Col == 8)
            {
                FgInsumo.AllowEditing = true;
            }
        }
        private void CmdAceIns_Click(object sender, EventArgs e)
        {
            int n_row = 0;
            int n_fila = 0;
            // VERIFICAMOS QUE TODOS LOS INSUMOS ESTEN CON LA CANTIDAD REAL
            for (n_row = 2; n_row <= FgInsumo.Rows.Count - 1; n_row++)
            {
                if (funFunciones.NulosC(FgInsumo.GetData(n_row, 5)) == "")
                {
                    MessageBox.Show("! No ha ingresado la cantidad real consumida para el insumos: "+  FgInsumo.GetData(n_row, 2).ToString() +" !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    FgInsumo.Focus();
                    return;
                }
            }

            for (n_row = 2; n_row <= FgInsumo.Rows.Count - 1; n_row++)
            {
                for (n_fila = 0; n_fila <= lstInsumos.Count - 1; n_fila++)
                {
                    int n_idins = Convert.ToInt32(FgInsumo.GetData(n_row, 7).ToString());
                    if (lstInsumos[n_fila].n_idins == n_idins)
                    {
                        lstInsumos[n_fila].n_canuti = Convert.ToDouble(FgInsumo.GetData(n_row, 5).ToString());
                        
                        if (FgInsumo.GetData(n_row, 8).ToString() == "True")
                        {
                            lstInsumos[n_fila].n_consumido = 1;
                        }
                        else
                        {
                            lstInsumos[n_fila].n_consumido = 0;
                        }

                        break;
                    }
                }
            }

            CmdCanIns_Click(sender, e);
        }
        private void FgInsumo_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }

            if (FgInsumo.Col == 5)
            {
                double n_dife = 0;
                n_dife = Convert.ToDouble(FgInsumo.GetData(FgInsumo.Row, 4)) - Convert.ToDouble(FgInsumo.GetData(FgInsumo.Row, 5));
                FgInsumo.SetData(FgInsumo.Row, 6, n_dife.ToString("0.000000"));
                if (n_dife < 0)
                {
                    funFlex.Flex_PintarCeldas(FgInsumo, FgInsumo.Row, 6, FgInsumo.Row, 6, Color.Red, Color.Transparent);
                }
                else
                {
                    if (n_dife == 0)
                    {
                        funFlex.Flex_PintarCeldas(FgInsumo, FgInsumo.Row, 6, FgInsumo.Row, 6, Color.Black, Color.Transparent);
                    }
                    else
                    {
                        funFlex.Flex_PintarCeldas(FgInsumo, FgInsumo.Row, 6, FgInsumo.Row, 6, Color.Blue, Color.Transparent);
                    }
                }
            }
        }
        private void TxtCanProducida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                double n_val = Convert.ToDouble(funFunciones.NulosN(TxtCanProducida.Text));
                TxtCanProducida.Text = n_val.ToString("0.00");
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
        private void ChkCerrar_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCerrar.Checked == true)
            {
                string c_Mensa = "";
                if (ValidarCierre(ref c_Mensa) == false)
                {
                    MessageBox.Show(c_Mensa, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    ChkCerrar.Checked = false;
                    return;
                }
            }
        }
        bool ValidarCierre(ref string c_Mensa)
        {
            int n_row = 0;
            bool b_result = false;
            string c_dato = "";

            if (Convert.ToDouble(TxtCanProducida.Text) == 0) 
            {
                c_Mensa = "No ha indicado la cantidad producida del producto";
                return b_result;
            }
            if (TxtFchTerProd.Text == " ")
            {
                c_Mensa = "No ha indicado la fecha de termino de la produccion";
                return b_result;
            }

            for (n_row = 0; n_row <= lstInsumos.Count - 1; n_row++)
            {
                if (lstInsumos[n_row].n_consumido == 1)
                { 
                    if (lstInsumos[n_row].n_canuti == 0)
                    {
                        c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", lstInsumos[n_row].n_idins.ToString(), "N").ToString();
                        c_Mensa = "No ha indicado la cantidad utilizada para el insumos: " + c_dato;
                        return b_result;
                    }
                }
            }
            b_result = true;            
            return b_result;
        }
        private void TxtCanPro_Validated(object sender, EventArgs e)
        {
            double n_valor = Convert.ToDouble(funFunciones.NulosN(TxtCanPro.Text));
            TxtCanPro.Text = n_valor.ToString("0.00");
        }
        private void TxtCanProducida_Validated(object sender, EventArgs e)
        {
            double n_valor = Convert.ToDouble(funFunciones.NulosN(TxtCanProducida.Text));
            TxtCanProducida.Text = n_valor.ToString("0.00");

            n_valor = Convert.ToDouble(funFunciones.NulosN(TxtCanProducida.Text)) / Convert.ToDouble(funFunciones.NulosN(TxtCanMatPri.Text));
            TxtPorRen.Text = (n_valor * 100).ToString("0.00");
        }
        private void modificarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modificar();
        }
        private void abrirProduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[2].CellValue(DgLista.Row).ToString());

            abrirProduccion(intIdRegistro);
        }
        void abrirProduccion(int n_IdProduccion)
        {
            bool b_Result = false;
            DialogResult Rpta = MessageBox.Show("Esta seguro de abrir el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistro.AbrirProduccion(n_IdProduccion) == true)
                {
                    MessageBox.Show("La produccion se abrio con exito", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    
                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistro.mysConec = mysConec;
                    b_Result = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
                    if (b_Result == true)
                    {
                        dtListar = objRegistro.dtListar;
                        // MOSTRAMOS LOS DATOS EN LA GRILLA
                        ListarItems();
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo abrir la produccion por el siguiente motivo; " + objRegistro.StrErrorMensaje + "", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }
        private void ToolValidar_Click(object sender, EventArgs e)
        {
        }

        private void CmdBusNotIng_Click(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            CN_alm_movimientos objMov = new CN_alm_movimientos();
            objMov.mysConec = mysConec;

            //if (OptDelPro.Checked == true) { dtResul = objMov.BuscarMovimientos(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblIdPro.Text), c_dato); }
            //if (OptOtrPro.Checked == true) { dtResul = objMov.BuscarMovimientos(STU_SISTEMA.EMPRESAID, 0, c_dato); }
            dtResul = objMov.BuscarMovimientosMP(STU_SISTEMA.EMPRESAID, 0, ""); 

            if (dtResul != null)
            {
                if (dtResul.Rows.Count != 0)
                {
                    LblIdNotIng.Text = dtResul.Rows[0]["n_id"].ToString();
                    TxtNumNotIng.Text = dtResul.Rows[0]["c_numdoc"].ToString();

                    //LblIdNotIng.Text = entRegistro.n_idnoting.ToString();
                    BE_ALM_MOVIMIENTOS_CONSULTA e_mov = new BE_ALM_MOVIMIENTOS_CONSULTA();
                    //BE_ALM_MOVIMIENTOS e_mov = new BE_ALM_MOVIMIENTOS ();
                    //CN_alm_movimientos objMov = new CN_alm_movimientos();
                    objMov.mysConec = mysConec;

                    e_mov = objMov.TraerRegistro(Convert.ToInt64(Convert.ToDouble(LblIdNotIng.Text)));
                    TxtNumNotIng.Text = e_mov.c_numser + "-" + e_mov.c_numdoc;
                    //TxtPorRen.Text = entRegistro.n_renpor.ToString("0.00");
                    TxtCanMatPri.Text = e_mov.lst_items[0].n_can.ToString("0.00");
                    string c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", e_mov.lst_items[0].n_idite.ToString(), "N").ToString();
                    TxtMatPri.Text = c_dato;
                }
            }
        }

        private void OptTipPro1_CheckedChanged(object sender, EventArgs e)
        {
            if (OptTipPro1.Checked == true)
            { 
                CmdBusNotIng.Enabled = true;
                TxtPorRen.Enabled = true;
            }
        }

        private void OptTipPro2_CheckedChanged(object sender, EventArgs e)
        {
            if (OptTipPro2.Checked == true)
            {
                CmdBusNotIng.Enabled = false;
                TxtPorRen.Enabled = false;
            }
        }

        private void TxtPorRen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                double n_val = Convert.ToDouble(funFunciones.NulosN(TxtPorRen.Text));
                TxtPorRen.Text = n_val.ToString("0.00");
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

        private void TxtNumNotIng_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtPorRen_Validated(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            double n_valor = Convert.ToDouble(funFunciones.NulosN(TxtPorRen.Text));
            TxtPorRen.Text = n_valor.ToString("0.00");

            double n_newval = (Convert.ToDouble(TxtCanMatPri.Text) * (n_valor / 100));
            TxtCanPagar.Text = n_newval.ToString("0.00");
        }

        private void TxtCanMatPri_Validated(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            double n_valor = Convert.ToDouble(funFunciones.NulosN(TxtCanProducida.Text)) / Convert.ToDouble(funFunciones.NulosN(TxtCanMatPri.Text));
            TxtPorRen.Text = (n_valor * 100).ToString("0.00");
            //double n_valor = Convert.ToDouble(funFunciones.NulosN(TxtPorRen.Text));
            //TxtPorRen.Text = n_valor.ToString("0.00");
            //double n_newval = (Convert.ToDouble(TxtCanMatPri.Text) * (n_valor / 100));
            //TxtCanPagar.Text = n_newval.ToString("0.00");
        }

        private void TxtPorRen_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtCanMatPri_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                double n_val = Convert.ToDouble(funFunciones.NulosN(TxtCanMatPri.Text));
                TxtCanMatPri.Text = n_val.ToString("0.00");
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
        private void CboCliente_SelectedValueChanged(object sender, EventArgs e)
        {
        }
        private void CboBusDocRef_Click(object sender, EventArgs e)
        {
            if (LblidCliente.Text == "") { return; }
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
                    TxtNumPed.Text = dtResult.Rows[0]["c_pednumdoc"].ToString();
                }
            }
        }
        private void CmdBusCli_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();

            CN_mae_clipro objCliPro = new CN_mae_clipro();
            objCliPro.mysConec = mysConec;
            dtResult = objCliPro.BuscarCliPro(dtcliente, 1, "n_id", "");
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtNumRuc.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                    LblidCliente.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtCliente.Text = dtResult.Rows[0]["c_nombre"].ToString();
                }
                else
                {
                    TxtNumRuc.Text = "";
                    LblidCliente.Text = "";
                    TxtCliente.Text = "";
                }
            }
        }

        private void CboRec_SelectedValueChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return;}
            if (booAgregando == true) { return; }
            DataTable dtResLin = new DataTable();
            int n_idrec = Convert.ToInt32(CboRec.SelectedValue);
            dtResLin = funDatos.DataTableFiltrar(dtLineas, "n_idpro = " + Convert.ToInt32(LblIdIte.Text) + " AND n_idrec = " + n_idrec + "");
            funDatos.ComboBoxCargarDataTable(CboLin, dtResLin, "n_id", "c_deslin");
            booAgregando = false;
        }

        private void BtnAgregarDetalle_Click(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            CN_alm_movimientos objMov = new CN_alm_movimientos();
            objMov.mysConec = mysConec;

            dtResul = objMov.BuscarMovimientosMP(STU_SISTEMA.EMPRESAID, 0, "");

            if (dtResul != null)
            {
                if (dtResul.Rows.Count != 0)
                {
                    ProduccionNotaIng produccionNotaIng = new ProduccionNotaIng();

                    produccionNotaIng.n_idnoting = Convert.ToInt32(dtResul.Rows[0]["n_id"]);

                    BE_ALM_MOVIMIENTOS_CONSULTA e_mov = new BE_ALM_MOVIMIENTOS_CONSULTA();
                    objMov.mysConec = mysConec;

                    e_mov = objMov.TraerRegistro(produccionNotaIng.n_idnoting);
                    produccionNotaIng.numnoting = e_mov.c_numser + "-" + e_mov.c_numdoc;
                    produccionNotaIng.cantidadmateriaprima = e_mov.lst_items[0].n_can;
                    produccionNotaIng.materiaprima = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", e_mov.lst_items[0].n_idite.ToString(), "N").ToString();

                    ProduccionNotaIngs.Add(produccionNotaIng);
                }
            }
        }

        private void BtnQuitarDetalle_Click(object sender, EventArgs e)
        {
            EliminarDetalle();

        }

        private void EliminarDetalle()
        {
            try
            {
                if (MessageBox.Show("¿Seguro desea eliminar el registro?", "Eliminar"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var viewModelDetail = (ProduccionNotaIng)produccionNotaIngBindingSource.Current;
                    ProduccionNotaIngs.RemoveItem(viewModelDetail);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrió un error al eliminar el registro, mensaje de error: {0}", ex.Message)
                    , "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSubirRegistro_Click(object sender, EventArgs e)
        {
            SubirRegistro();
        }

        public async void SubirRegistro()
        {
            try
            {
                int n_IdRegistro = Convert.ToInt32(DgLista.Columns[2].CellValue(DgLista.Row).ToString());

                objRegistro.TraerRegistro(n_IdRegistro);
                entRegistro = objRegistro.EntProduccion;

                ProductionOutput productionOutput = new ProductionOutput
                {
                    Code = entRegistro.n_id.ToString(),
                    Name = string.Format("{0}-{1}", entRegistro.c_numser, entRegistro.c_numdoc),
                    ProductionDate = entRegistro.d_fchpro,
                    CompanyId = 1,
                    EmployeeId = 2,
                    ProductionMethodId = 43
                };

                var productionService = RestService.For<IProductionService>(ConfigurationManager.AppSettings["ApiHostUrl"]);
                var productionOutputResult = await productionService.ProductionOutputCreate(productionOutput);

                MessageBox.Show(string.Format("¡Se subió el registro: {0} correctamente! ", productionOutputResult.Name), "Subir Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("¡Ocurrio un error: {0}! ", ex.Message), "Subir Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
