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
using SIAC_Negocio.Produccion;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Mantenimiento;
using SIAC_Entidades.Produccion;
using SIAC_Entidades.Almacen;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using System.Data.OleDb;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Diagnostics;

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmManProductos : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public OleDbConnection AccConec = new OleDbConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        BE_PRO_PRODUCTOS entRegistro = new BE_PRO_PRODUCTOS();
        List<BE_PRO_PRODUCTOSRECETAS> lstRecetas = new List<BE_PRO_PRODUCTOSRECETAS>();
        List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstRecetasIns = new List<BE_PRO_PRODUCTOSRECETASINSUMOS>();
        List<BE_PRO_PRODUCTOSRECETASLINEAS> lstLineas = new List<BE_PRO_PRODUCTOSRECETASLINEAS>();
        List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS> lstLineasTar = new List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS>();
        List<BE_PRO_PRODUCTOSUBILOC> lstUbiLoc = new List<BE_PRO_PRODUCTOSUBILOC>();
        List<BE_PRO_PRODUCTOSUBILOCALM> lstUbiLocAlm = new List<BE_PRO_PRODUCTOSUBILOCALM>();

        // OBJETOS LOCALES
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_pro_productos objRegistro = new CN_pro_productos();
        CN_pro_tareas objTareas = new CN_pro_tareas();
        CN_sun_unimed objUniMed = new CN_sun_unimed();
        CN_mae_familia objFam = new CN_mae_familia();
        CN_mae_clase objCla = new CN_mae_clase();
        CN_mae_subclase objSubCla = new CN_mae_subclase();
        CN_pro_tipoproducto objTipPro = new CN_pro_tipoproducto();
        CN_alm_inventario ObjItem = new CN_alm_inventario();
        CN_sun_tipexi objTipExi = new CN_sun_tipexi();
        CN_sys_empresalocal  objLocal = new CN_sys_empresalocal();
        CN_alm_almacenes objAlmacen = new CN_alm_almacenes();
        CN_sun_unimed objUniMedSunat = new CN_sun_unimed();
        CN_man_equipos objEquipo = new CN_man_equipos();

        // DATATABLE LOCALES
        DataTable dtListar = new DataTable();
        DataTable dtTareas = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtUniMed = new DataTable();
        DataTable dtReceta = new DataTable();
        DataTable dtLineas = new DataTable();
        DataTable dtFan = new DataTable();
        DataTable dtCla = new DataTable();
        DataTable dtSubCla = new DataTable();
        DataTable dtTipPro = new DataTable();
        DataTable dtTipExi = new DataTable();
        DataTable dtEquipos = new DataTable();
        DataTable dtLocal = new DataTable();
        DataTable dtAlmacen = new DataTable();
        DataTable dtUniMedSunat = new DataTable();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Convertir funCon = new Convertir();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        DatosMySql FunMysql = new DatosMySql();
                
        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                                // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        //int n_IdProducto;
        
        string[,] arrCabeceraDg1 = new string[8, 4];                                      // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlexRec = new string[8, 5];
        string[,] arrCabeceraFlexLin = new string[11, 5];
        string[,] arrCabeceraFlexLoc = new string[2, 5];
        string[,] arrCabeceraFlexAlm = new string[4, 5];
        
        int n_IdRec = 29999;
        int n_IdLinea = 19999;
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;

        public FrmManProductos()
        {
            InitializeComponent();
        }
        private void label10_Click(object sender, EventArgs e)
        {

        }
        private void FrmManProductos_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            Tab1.SelectedIndex = 0;
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboFam, dtFan, "n_id", "c_des");
            //funDatos.ComboBoxCargarDataTable(CboCla, dtCla, "n_id", "c_des");
            //funDatos.ComboBoxCargarDataTable(CboSubCla, dtSubCla, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboUniMed, dtUniMed, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipPro, dtTipPro, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            //this.Height = 635;
            //this.Width = 1061;
            
            //Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            //Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;

            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            // FLEX GRID DE LAS RECETAS
            arrCabeceraFlexRec[0, 0] = "Codigo";
            arrCabeceraFlexRec[0, 1] = "70";
            arrCabeceraFlexRec[0, 2] = "C";
            arrCabeceraFlexRec[0, 3] = "";
            arrCabeceraFlexRec[0, 4] = "c_codrec";

            arrCabeceraFlexRec[1, 0] = "Descripcion";
            arrCabeceraFlexRec[1, 1] = "350";
            arrCabeceraFlexRec[1, 2] = "C";
            arrCabeceraFlexRec[1, 3] = "";
            arrCabeceraFlexRec[1, 4] = "c_des";

            arrCabeceraFlexRec[2, 0] = "Uni. Med.";
            arrCabeceraFlexRec[2, 1] = "60";
            arrCabeceraFlexRec[2, 2] = "C";
            arrCabeceraFlexRec[2, 3] = "";
            arrCabeceraFlexRec[2, 4] = "c_itepredes";

            arrCabeceraFlexRec[3, 0] = "Cantidad";
            arrCabeceraFlexRec[3, 1] = "60";
            arrCabeceraFlexRec[3, 2] = "N";
            arrCabeceraFlexRec[3, 3] = "0.00";
            arrCabeceraFlexRec[3, 4] = "n_can";

            arrCabeceraFlexRec[4, 0] = "Prio ridad";
            arrCabeceraFlexRec[4, 1] = "40";
            arrCabeceraFlexRec[4, 2] = "N";
            arrCabeceraFlexRec[4, 3] = "0";
            arrCabeceraFlexRec[4, 4] = "n_prirec";

            arrCabeceraFlexRec[5, 0] = "Observaciones";
            arrCabeceraFlexRec[5, 1] = "150";
            arrCabeceraFlexRec[5, 2] = "C";
            arrCabeceraFlexRec[5, 3] = "";
            arrCabeceraFlexRec[5, 4] = "c_obs";

            arrCabeceraFlexRec[6, 0] = "IdReceta";
            arrCabeceraFlexRec[6, 1] = "0";
            arrCabeceraFlexRec[6, 2] = "N";
            arrCabeceraFlexRec[6, 3] = "0";
            arrCabeceraFlexRec[6, 4] = "n_id";

            arrCabeceraFlexRec[7, 0] = "Activo";
            arrCabeceraFlexRec[7, 1] = "40";
            arrCabeceraFlexRec[7, 2] = "B";
            arrCabeceraFlexRec[7, 3] = "";
            arrCabeceraFlexRec[7, 4] = "n_act";

            funFlex.FlexMostrarDatos(FgRec, arrCabeceraFlexRec, dtListar, 2, false);

            // FLEX GRID DE LAS LINEAS
            arrCabeceraFlexLin[0, 0] = "Descripcion";
            arrCabeceraFlexLin[0, 1] = "240";
            arrCabeceraFlexLin[0, 2] = "C";
            arrCabeceraFlexLin[0, 3] = "";
            arrCabeceraFlexLin[0, 4] = "c_codrec";

            arrCabeceraFlexLin[1, 0] = "Uni. Med.";
            arrCabeceraFlexLin[1, 1] = "60";
            arrCabeceraFlexLin[1, 2] = "C";
            arrCabeceraFlexLin[1, 3] = "";
            arrCabeceraFlexLin[1, 4] = "c_codrec";

            arrCabeceraFlexLin[2, 0] = "Insumo / Producto Principal";
            arrCabeceraFlexLin[2, 1] = "190";
            arrCabeceraFlexLin[2, 2] = "C";
            arrCabeceraFlexLin[2, 3] = "";
            arrCabeceraFlexLin[2, 4] = "c_codrec";

            arrCabeceraFlexLin[3, 0] = "Tiempo Linea";
            arrCabeceraFlexLin[3, 1] = "55";
            arrCabeceraFlexLin[3, 2] = "H";
            arrCabeceraFlexLin[3, 3] = "";
            arrCabeceraFlexLin[3, 4] = "c_codrec";

            arrCabeceraFlexLin[4, 0] = "Cantidad x Hora";
            arrCabeceraFlexLin[4, 1] = "55";
            arrCabeceraFlexLin[4, 2] = "N";
            arrCabeceraFlexLin[4, 3] = "";
            arrCabeceraFlexLin[4, 4] = "c_codrec";

            arrCabeceraFlexLin[5, 0] = "Operarios";
            arrCabeceraFlexLin[5, 1] = "60";
            arrCabeceraFlexLin[5, 2] = "N";
            arrCabeceraFlexLin[5, 3] = "";
            arrCabeceraFlexLin[5, 4] = "c_codrec";

            arrCabeceraFlexLin[6, 0] = "Eficiencia";
            arrCabeceraFlexLin[6, 1] = "60";
            arrCabeceraFlexLin[6, 2] = "N";
            arrCabeceraFlexLin[6, 3] = "";
            arrCabeceraFlexLin[6, 4] = "c_codrec";

            arrCabeceraFlexLin[7, 0] = "Activo";
            arrCabeceraFlexLin[7, 1] = "40";
            arrCabeceraFlexLin[7, 2] = "B";
            arrCabeceraFlexLin[7, 3] = "";
            arrCabeceraFlexLin[7, 4] = "c_codrec";

            arrCabeceraFlexLin[8, 0] = "IDRECETA";
            arrCabeceraFlexLin[8, 1] = "0";
            arrCabeceraFlexLin[8, 2] = "N";
            arrCabeceraFlexLin[8, 3] = "";
            arrCabeceraFlexLin[8, 4] = "c_codrec";

            arrCabeceraFlexLin[9, 0] = "IdLinea";
            arrCabeceraFlexLin[9, 1] = "0";
            arrCabeceraFlexLin[9, 2] = "N";
            arrCabeceraFlexLin[9, 3] = "";
            arrCabeceraFlexLin[9, 4] = "c_codrec";

            arrCabeceraFlexLin[10, 0] = "Precio Hora Tarea";
            arrCabeceraFlexLin[10, 1] = "70";
            arrCabeceraFlexLin[10, 2] = "D";
            arrCabeceraFlexLin[10, 3] = "";
            arrCabeceraFlexLin[10, 4] = "c_codrec";

            funFlex.FlexMostrarDatos(FgLineas, arrCabeceraFlexLin, dtListar, 2, false);
            
            arrCabeceraFlexLoc[0, 0] = "Local";
            arrCabeceraFlexLoc[0, 1] = "340";
            arrCabeceraFlexLoc[0, 2] = "C";
            arrCabeceraFlexLoc[0, 3] = "";
            arrCabeceraFlexLoc[0, 4] = "c_codrec";

            arrCabeceraFlexLoc[1, 0] = "Id Local";
            arrCabeceraFlexLoc[1, 1] = "0";
            arrCabeceraFlexLoc[1, 2] = "N";
            arrCabeceraFlexLoc[1, 3] = "";
            arrCabeceraFlexLoc[1, 4] = "c_codrec";

            funFlex.FlexMostrarDatos(FgLocal, arrCabeceraFlexLoc, dtListar, 2, false);
                        
            arrCabeceraFlexAlm[0, 0] = "Almacen";
            arrCabeceraFlexAlm[0, 1] = "280";
            arrCabeceraFlexAlm[0, 2] = "C";
            arrCabeceraFlexAlm[0, 3] = "";
            arrCabeceraFlexAlm[0, 4] = "c_codrec";

            arrCabeceraFlexAlm[1, 0] = "Capacidad Minima";
            arrCabeceraFlexAlm[1, 1] = "80";
            arrCabeceraFlexAlm[1, 2] = "N";
            arrCabeceraFlexAlm[1, 3] = "";
            arrCabeceraFlexAlm[1, 4] = "c_codrec";

            arrCabeceraFlexAlm[2, 0] = "Capacidad Maxima";
            arrCabeceraFlexAlm[2, 1] = "80";
            arrCabeceraFlexAlm[2, 2] = "N";
            arrCabeceraFlexAlm[2, 3] = "";
            arrCabeceraFlexAlm[2, 4] = "c_codrec";

            arrCabeceraFlexAlm[3, 0] = "Id Almacen";
            arrCabeceraFlexAlm[3, 1] = "0";
            arrCabeceraFlexAlm[3, 2] = "N";
            arrCabeceraFlexAlm[3, 3] = "";
            arrCabeceraFlexAlm[3, 4] = "c_codrec";

            funFlex.FlexMostrarDatos(FgAlmacenes, arrCabeceraFlexAlm, dtListar, 2, false);
            
            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(35, ref arrCabeceraDg1);
        }
        void DataTableCargar()
        {
            objRegistro.mysConec = mysConec;
            if (objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD) == true)
            {
                dtListar = objRegistro.dtListar;
            }
            
            ObjItem.mysConec = mysConec;
            dtItems = ObjItem.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);

            objTareas.mysConec = mysConec;
            dtTareas = objTareas.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objUniMed.mysConec = mysConec;
            dtUniMed = objUniMed.Listar();

            objFam.mysConec = mysConec;
            dtFan = objFam.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
            dtFan = funDatos.DataTableFiltrar(dtFan, "(n_idtipexi = 2)");

            objCla.mysConec = mysConec;
            dtCla = objCla.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objSubCla.mysConec = mysConec;
            dtSubCla = objSubCla.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objTipPro.mysConec = mysConec;
            if (objTipPro.Listar(STU_SISTEMA.EMPRESAID) == true)
            {
                dtTipPro = objTipPro.dtLista;
            }

            objLocal.mysConec = mysConec;
            dtLocal = objLocal.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objAlmacen.mysConec = mysConec;
            dtAlmacen = objAlmacen.ListarNuevo(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objTipExi.mysConec = mysConec;
            dtTipExi = objTipExi.Listar();

            objForm.mysConec = mysConec;                                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(35);                                             // INDICAMOS QUE ES EL FORMULARIO MAESTRO DE RECETAS

            objEquipo.mysConec = mysConec;
            if (objEquipo.Listar(STU_SISTEMA.EMPRESAID) == true)
            {
                dtEquipos = objEquipo.dtLista;
            }
            

            objUniMedSunat.mysConec = mysConec;
            dtUniMedSunat = objUniMedSunat.Listar();
        }
        void ListarItems()
        {
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
        void VerRegistro(int n_IdProducto)
        {
            int n_row = 0;
            int n_fila = 2;
            string c_dato = "";
            lstRecetas.Clear();
            lstLineas.Clear();
            lstRecetasIns.Clear();
            lstLineasTar.Clear();
            DataTable dtResul = new DataTable();

            Blanquea();
            FgRec.Rows.Count = 2;
            FgLineas.Rows.Count = 2;
            FgLocal.Rows.Count = 2;
            FgAlmacenes.Rows.Count = 2;
            objRegistro.mysConec = mysConec;

            if (objRegistro.TraerRegistro(n_IdProducto)==true)
            {
                entRegistro = objRegistro.entRegistro;
                TxtCodPro.Text = entRegistro.c_cod;
                TxtProducto.Text = entRegistro.c_despro;
                TxtObs.Text = entRegistro.c_obs;
                CboFam.SelectedValue = entRegistro.n_idfam;

                if (entRegistro.n_idcla != 0)
                {
                    dtResul = funDatos.DataTableFiltrar(dtCla, "(n_idtipexi = 2) AND (n_idfam = " + CboFam.SelectedValue.ToString() + " )");
                    funDatos.ComboBoxCargarDataTable(CboCla, dtResul, "n_id", "c_des");
                    CboCla.SelectedValue = entRegistro.n_idcla;

                    if (entRegistro.n_idsubcla != 0)
                    {
                        dtResul = funDatos.DataTableFiltrar(dtSubCla, "(n_idtipexi = 2)");
                        dtResul = funDatos.DataTableFiltrar(dtResul, "(n_idfam = " + CboFam.SelectedValue.ToString() + " ) AND (n_idcla = " + CboCla.SelectedValue.ToString() + ")");
                        funDatos.ComboBoxCargarDataTable(CboSubCla, dtResul, "n_id", "c_des");
                    }
                }
                else
                {
                    dtResul = funDatos.DataTableFiltrar(dtCla, "(n_idtipexi = 2) AND (n_idfam = " + CboFam.SelectedValue.ToString() + " )");
                    funDatos.ComboBoxCargarDataTable(CboCla, dtResul, "n_id", "c_des");
                }

                CboSubCla.SelectedValue = entRegistro.n_idsubcla;
                CboUniMed.SelectedValue = entRegistro.n_idunimed;
                CboTipPro.SelectedValue = entRegistro.n_idtip;

                if (entRegistro.n_act == 1)
                {
                    ChkAct.Checked = true;
                    LblEst.ForeColor = Color.Navy;
                    LblEst.Text = "ACTIVO";
                }
                else
                {
                    ChkAct.Checked = false;
                    LblEst.ForeColor = Color.Red;
                    LblEst.Text = "NO ACTIVO";
                }

                lstRecetas = objRegistro.lstRecetas;
                lstLineas = objRegistro.lstLineas;
                lstRecetasIns = objRegistro.lstRecetasIns;
                lstLineasTar = objRegistro.lstLineasTar;
                lstUbiLoc = objRegistro.lstUbiLoc;
                lstUbiLocAlm = objRegistro.lstUbiLocAlm;

                // ********************* 
                // MOSTRAMOS LAS RECETAS
                for (n_row = 0; n_row <= lstRecetas.Count - 1; n_row++)
                {
                    FgRec.Rows.Count = FgRec.Rows.Count + 1;
                    FgRec.SetData(n_fila, 1, lstRecetas[n_row].c_codrec);
                    FgRec.SetData(n_fila, 2, lstRecetas[n_row].c_des);

                    c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_abr", lstRecetas[n_row].n_idunimed.ToString(), "N").ToString();
                    FgRec.SetData(n_fila, 3, c_dato);
                    FgRec.SetData(n_fila, 4, lstRecetas[n_row].n_can.ToString("0.00"));
                    FgRec.SetData(n_fila, 5, lstRecetas[n_row].n_prirec);
                    FgRec.SetData(n_fila, 6, lstRecetas[n_row].c_obs);
                    FgRec.SetData(n_fila, 7, lstRecetas[n_row].n_id);                              // ID DE LA RECETA
                    FgRec.SetData(n_fila, 8, lstRecetas[n_row].n_act);                             // INDICA SI LA RECETA ESTA ACTIVA
                    n_fila = n_fila + 1;
                }
                if (lstRecetas.Count != 0)
                { 
                    MostrarLineas(lstRecetas[0].n_id, lstRecetas[0].c_des);
                }
                MostrarLocalUbicacion();
                if (lstUbiLoc.Count != 0)
                { 
                    MostrarLocalAlmacenUbicacion(lstUbiLoc[0].n_idloc);
                }
            }
        }
        void MostrarLocalUbicacion()
        {
            int n_row = 0;
            int n_fila = 2;
            string c_dato = "";
            FgLocal.Rows.Count = 2;
                        
            n_fila = 2;
            for (n_row = 0; n_row <= lstUbiLoc.Count - 1; n_row++)
            {
                FgLocal.Rows.Count = FgLocal.Rows.Count + 1;
                c_dato = funDatos.DataTableBuscar(dtLocal, "n_id", "c_des", lstUbiLoc[n_row].n_idloc.ToString(), "N").ToString();
                FgLocal.SetData(n_fila, 1, c_dato);
                FgLocal.SetData(n_fila, 2, lstUbiLoc[n_row].n_idloc);
                n_fila = n_fila + 1;
            }
        }
        void MostrarLocalAlmacenUbicacion(int n_IdLocal)
        {
            int n_row = 0;
            int n_fila = 2;
            string c_dato = "";
            FgAlmacenes.Rows.Count = 2;

            n_fila = 2;
            for (n_row = 0; n_row <= lstUbiLocAlm.Count - 1; n_row++)
            {
                if (lstUbiLocAlm[n_row].n_idloc == n_IdLocal)
                {
                    FgAlmacenes.Rows.Count = FgAlmacenes.Rows.Count + 1;
                    c_dato = funDatos.DataTableBuscar(dtAlmacen, "n_id", "c_des", lstUbiLocAlm[n_row].n_idalm.ToString(), "N").ToString();
                    FgAlmacenes.SetData(n_fila, 1, c_dato);
                    FgAlmacenes.SetData(n_fila, 2, lstUbiLocAlm[n_row].n_canalmmin);
                    FgAlmacenes.SetData(n_fila, 3, lstUbiLocAlm[n_row].n_canalmmax);
                    FgAlmacenes.SetData(n_fila, 4, lstUbiLocAlm[n_row].n_idalm);
                    n_fila = n_fila + 1;
                }
            }
        }
        void MostrarLineas(int n_IdReceta, string c_DesReceta)
        {
            int n_row = 0;
            int n_fila = 2;
            string c_dato = "";
            FgLineas.Rows.Count = 2;
            
            LblReceta.Text = c_DesReceta;
            // ********************* 
            // MOSTRAMOS LAS LINEAS
            booAgregando = true;
            n_fila = 2;
            for (n_row = 0; n_row <= lstLineas.Count - 1; n_row++)
            {
                if (lstLineas[n_row].n_idrec == n_IdReceta)
                {
                    FgLineas.Rows.Count = FgLineas.Rows.Count + 1;

                    FgLineas.SetData(n_fila, 1, funFunciones.NulosC( lstLineas[n_row].c_deslin));

                    if (Convert.ToInt32(funFunciones.NulosN(lstLineas[n_row].n_idunimed)) != 0)
                    { 
                        c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_abr", lstLineas[n_row].n_idunimed.ToString(), "N").ToString();
                        FgLineas.SetData(n_fila, 2, c_dato);
                    }

                    if (Convert.ToInt32(funFunciones.NulosN(lstLineas[n_row].n_idite)) != 0)
                    {
                        c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", lstLineas[n_row].n_idite.ToString(), "N").ToString();
                        FgLineas.SetData(n_fila, 3, c_dato);
                    }

                    c_dato = funCon.DecimalEnHoras(lstLineas[n_row].n_tiepro);              // CONVERTIRMOS EL TIEMPO DE DECIMALES A FORMATO HORA PARA MOSTRAR EN EL GRID
                    FgLineas.SetData(n_fila, 4, c_dato);

                    FgLineas.SetData(n_fila, 5, lstLineas[n_row].n_can.ToString("0.00"));
                    FgLineas.SetData(n_fila, 6, lstLineas[n_row].n_numope.ToString("0.00"));
                    FgLineas.SetData(n_fila, 7, lstLineas[n_row].n_efi.ToString("0.00"));
                    if (lstLineas[n_row].n_act == 1)
                    {
                        FgLineas.SetData(n_fila, 8, "1");
                    }
                    else
                    {
                        FgLineas.SetData(n_fila, 8, "0");
                    }
                    
                    FgLineas.SetData(n_fila, 9, lstLineas[n_row].n_idrec);
                    FgLineas.SetData(n_fila, 10, lstLineas[n_row].n_id);
                    FgLineas.SetData(n_fila, 11, lstLineas[n_row].n_prehorjor.ToString("0.00"));
                    n_fila = n_fila + 1;
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
            booAgregando = false;
            ChkAct.Checked = true;
            TxtCodPro.Focus();

            lstRecetas.Clear();
            lstRecetasIns.Clear();
            lstLineas.Clear();
            lstLineasTar.Clear();

            CmdVerRec.Text = "Modificar Receta";
            CmdVerLin.Text = "Modificar Linea";
            n_IdRec = 29999;
        }
        void Blanquea()
        {
            TxtCodPro.Text = "";
            TxtProducto.Text = "";
            CboFam.SelectedValue = 0;
            CboCla.SelectedValue = 0;
            CboSubCla.SelectedValue = 0;
            CboUniMed.SelectedValue = 0;
            CboTipPro.SelectedValue = 0;
            TxtCodPro.Text = "";
            LblReceta.Text = "";
            FgRec.Rows.Count = 2;
            FgLineas.Rows.Count = 2;
        }
        void Bloquea()
        {
            TxtProducto.Enabled = !TxtProducto.Enabled;
            CboFam.Enabled = !CboFam.Enabled;
            CboCla.Enabled = !CboCla.Enabled;
            CboSubCla.Enabled = !CboSubCla.Enabled;
            CboUniMed.Enabled = !CboUniMed.Enabled;
            CboTipPro.Enabled = !CboTipPro.Enabled;
            TxtCodPro.Enabled = !TxtCodPro.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
            ChkAct.Enabled = !ChkAct.Enabled;

            CmdAddRec.Enabled = !CmdAddRec.Enabled;
            CmdDelRec.Enabled = !CmdDelRec.Enabled;

            AmdAddLin.Enabled = !AmdAddLin.Enabled;
            CmdDelLin.Enabled = !CmdDelLin.Enabled;

            CmdAddAlm.Enabled = !CmdAddAlm.Enabled;
            CmdEdiAlm.Enabled = !CmdEdiAlm.Enabled;

            CmdAddLoc.Enabled = !CmdAddLoc.Enabled;
            CmdEdiLoc.Enabled = !CmdEdiLoc.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
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
            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            FgLineas.Focus();
            booAgregando = false;
            n_IdRec = 29999;

            CmdVerRec.Text  =  "Modificar Receta";
            CmdVerLin.Text = "Modificar Linea";
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DE LA RECETA PARA ELIMINAR LAS LINEAS

            //objRegistro.mysConec = mysConec;
            //objRegistro.obternerlineas(STU_SISTEMA.EMPRESAID, intIdRegistro);

            //if (objRegistro.booOcurrioError == false)
            //{
            //    lstLineas = objRegistro.lstLineas;
            //    lstLineasDet = objRegistro.lstLineasDet;
            //}

            //DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar las lineas de la receta seleccionada", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            //if (DialogResult.Yes == Rpta)
            //{
            //    if (objRegistro.Eliminar(lstLineas) == true)
            //    {
            //        booResult = true;
            //        MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            //        // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
            //        objRegistro.mysConec = mysConec;
            //        dtListar = objReceta.ListarRecetaLineas(STU_SISTEMA.EMPRESAID);
            //        // MOSTRAMOS LOS DATOS EN LA GRILLA
            //        ListarItems();
            //    }
            //    else
            //    {
            //        MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistro.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    }
            //}
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
            CmdVerRec.Text = "Ver Receta";
            CmdVerLin.Text = "Ver Linea";
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
                booResultado = objRegistro.Insertar(entRegistro, lstRecetas, lstRecetasIns, lstLineas, lstLineasTar);
            }
            
            if (n_QueHace == 2)
            {
                ////var copia = (source as IEnumerable<string>).ToList();
                //BE_PRO_PRODUCTOS entRecU = new BE_PRO_PRODUCTOS();
                //List<BE_PRO_PRODUCTOSRECETAS> lstRecetasU = new List<BE_PRO_PRODUCTOSRECETAS>();
                //List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstRecetasInsU = new List<BE_PRO_PRODUCTOSRECETASINSUMOS>();
                //List<BE_PRO_PRODUCTOSRECETASLINEAS> lstLineasU = new List<BE_PRO_PRODUCTOSRECETASLINEAS>();
                //List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS> lstLineasTarU = new List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS>();
                //List<BE_PRO_PRODUCTOSUBILOC> lstUbiLocU = new List<BE_PRO_PRODUCTOSUBILOC>();
                //List<BE_PRO_PRODUCTOSUBILOCALM> lstUbiLocAlmU = new List<BE_PRO_PRODUCTOSUBILOCALM>();

                //entRecU = entRegistro;
                //entRecU = entRegistro. ;
                //entRecU = entRegistro.Clone() as BE_PRO_PRODUCTOS;
                //lstRecetasU = (lstRecetas as IEnumerable<BE_PRO_PRODUCTOSRECETAS>).ToList();
                //lstRecetasInsU = (lstRecetasIns as IEnumerable<BE_PRO_PRODUCTOSRECETASINSUMOS>).ToList();
                //lstLineasU = (lstLineas as IEnumerable<BE_PRO_PRODUCTOSRECETASLINEAS>).ToList();
                //lstLineasTarU = (lstLineasTar as IEnumerable<BE_PRO_PRODUCTOSRECETASLINEASTAREAS>).ToList();
                
                //booResultado = objRegistro.Actualizar(entRecU, lstRecetasU, lstRecetasInsU, lstLineasU, lstLineasTarU);
                CN_pro_productos objReg = new CN_pro_productos();
                objReg.mysConec = mysConec;
                objReg.TraerRegistro(entRegistro.n_id);
                
                objRegistro.lstRecetas = objReg.lstRecetas;
                objRegistro.lstRecetasIns = objReg.lstRecetasIns;
                objRegistro.lstLineas = objReg.lstLineas;
                objRegistro.lstLineasTar = objReg.lstLineasTar;

                booResultado = objRegistro.Actualizar(entRegistro, lstRecetas, lstRecetasIns, lstLineas, lstLineasTar);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ No se pudo guardar el registro por el siguiente motivo ! " + objRegistro.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {
            entRegistro.n_idemp = 0;
            if (STU_SISTEMA.SYS_UNIBD == 0) { entRegistro.n_idemp = STU_SISTEMA.EMPRESAID; }
                        
            if (n_QueHace == 1)
            {
                entRegistro.n_id = 0;
            }
            entRegistro.c_cod = TxtCodPro.Text ;
            entRegistro.c_despro = TxtProducto.Text;
            entRegistro.n_idunimed = Convert.ToInt32(CboUniMed.SelectedValue);
            entRegistro.n_idfam = Convert.ToInt32(CboFam.SelectedValue);
            entRegistro.n_idcla= Convert.ToInt32(CboCla.SelectedValue);
            entRegistro.n_idsubcla = Convert.ToInt32(CboSubCla.SelectedValue);
            entRegistro.n_idtip = 2;                                                // EL TIPO DE EXISTENCIA SIEMPRE SERA 2 = PRODUCTO
            entRegistro.c_obs = TxtObs.Text;
                        
            if (ChkAct.Checked == true)
            {
                entRegistro.n_act = 1;                                              // INDICAMOS QUE ES ACTIVO
            }
            else
            {
                entRegistro.n_act = 2;                                              // INDICAMOS QUE ES NO ACTIVO
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            //if (TxtCodPro.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado el codigo del producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    TxtCodPro.Focus();
            //    booEstado = false;
            //    return booEstado;
            //}
            if (TxtProducto.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre del producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtProducto.Focus();
                booEstado = false;
                return booEstado;
            }
            if (Convert.ToInt32(CboFam.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la familia del producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboFam.Focus();
                booEstado = false;
                return booEstado;
            }
            if (Convert.ToInt32(CboCla.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la clase del producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboCla.Focus();
                booEstado = false;
                return booEstado;
            }
            if (Convert.ToInt32(CboSubCla.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la sub clase del producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboSubCla.Focus();
                booEstado = false;
                return booEstado;
            }
            if (Convert.ToInt32(CboUniMed.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la unidad de medida del producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboUniMed.Focus();
                booEstado = false;
                return booEstado;
            }

            //if (TxtObs.Text == "")
            //{
            //    MessageBox.Show("¡ No ha especificado la observacion del producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    TxtObs.Focus();
            //    booEstado = false;
            //    return booEstado;
            //}
           
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
            MessageBox.Show("Opcion no disponible", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //EliminarRegistro();
        }
        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            if (Grabar() == true)
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objRegistro.mysConec = mysConec;

                objRegistro.mysConec = mysConec;
                if (objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD) == true)
                {
                    dtListar = objRegistro.dtListar;
                }

                //dtListar = objRegistro.ListarRecetaLineas(STU_SISTEMA.EMPRESAID);
                //// MOSTRAMOS LOS DATOS EN LA GRILLA
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
            //objReceta = null;
            objRegistro = null;
            ObjItem = null;
            objTareas = null;
            objTipExi = null;
            objUniMed = null;
            objFormVis = null;
            objForm = null;

            this.Close();
        }
        private void FrmManProductos_Activated(object sender, EventArgs e)
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
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            //VerRegistro(intIdRegistro);
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
        private void FrmManProductos_Resize(object sender, EventArgs e)
        {
            //Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
        }
        private void FgRec_RowColChange(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            FgLineas.Rows.Count = 2;

            int n_fila = 0;
            int m_idrec = Convert.ToInt32(funFunciones.NulosN(FgRec.GetData(FgRec.Row, 7)));
            int n_numlin = 0 ;

            // VERIFICAMOS SI LA RECETA TIENE LINEAS
            for (n_fila = 0; n_fila <= lstLineas.Count - 1; n_fila++)
            {
                if (lstLineas[n_fila].n_idrec == m_idrec)
                {
                    n_numlin = n_numlin + 1;
                }
            }
            if (n_numlin == 0) { return; }
            
            MostrarLineas(Convert.ToInt32(FgRec.GetData(FgRec.Row,7).ToString()), FgRec.GetData(FgRec.Row,2).ToString());
        }
        private void CmdVerRec_Click(object sender, EventArgs e)
        {
            if (FgRec.Rows.Count == 2)
            {
                MessageBox.Show("No se han encontrado recetas ¡ Debe de agregar una como minimo!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            else
            {
                int n_row = 0;
                int n_index = 0;
                int n_idre = Convert.ToInt32(FgRec.GetData(FgRec.Row, 7).ToString());

                for (n_row = 0; n_row <= lstRecetas.Count-1; n_row++)
                {
                    if (lstRecetas[n_row].n_id == n_idre)
                    {
                        n_index = n_row;
                        break;
                    }
                }

                // FILTRAMOS SOLO LOS INSUMOS DE LA RECETA  ACTUAL
                List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstTemp = new List<BE_PRO_PRODUCTOSRECETASINSUMOS>();
                BE_PRO_PRODUCTOSRECETASINSUMOS entTemp = new BE_PRO_PRODUCTOSRECETASINSUMOS();
                                
                for (n_row = 0; n_row <= lstRecetasIns.Count-1; n_row++)
                {
                    if (lstRecetasIns[n_row].n_idrec == n_idre)
                    {
                        entTemp = lstRecetasIns[n_row];

                        lstTemp.Add(entTemp);
                    }
                }

                FrmVerRecetaInsumos MyForm = new FrmVerRecetaInsumos();
                MyForm.dtTipExi = dtTipExi;
                MyForm.dtItems = dtItems;
                MyForm.dtUniMed = dtUniMed;
                MyForm.entReceta = lstRecetas[n_index];
                MyForm.lstRecetasIns = lstTemp;

                if (CmdVerRec.Text != "Ver Receta")
                {
                    MyForm.b_Modificar = true;
                }
                MyForm.ShowDialog(); 

                if (CmdVerRec.Text != "Ver Receta")
                {
                    lstTemp = MyForm.lstRecetasIns;
                    AgregarNuevosItemsLista(lstTemp);
                }
                
                MyForm.Close();
                MyForm = null;
            }
        }
        void AgregarNuevosItemsLista(List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstOrigen)
        { 
            int n_row = 0;
            int n_rowdet = 0;
            bool b_SeEncontro = false;

            if (lstOrigen.Count == 0) { return; }

            // BUSCAMOS SI SE HA ELIMINADO ALGUN INSUMOS
            for (n_row = 0; n_row <= lstRecetasIns.Count - 1; n_row++)
            {
                if (lstRecetasIns[n_row].n_idrec == lstOrigen[0].n_idrec)
                {
                    b_SeEncontro = false;
                    for (n_rowdet = 0; n_rowdet <= lstOrigen.Count - 1; n_rowdet++)
                    {
                        if ((lstOrigen[n_rowdet].n_idpro == lstRecetasIns[n_row].n_idpro) &&
                            (lstOrigen[n_rowdet].n_idrec == lstRecetasIns[n_row].n_idrec) &&
                            (lstOrigen[n_rowdet].n_idite == lstRecetasIns[n_row].n_idite))
                        {
                            b_SeEncontro = true;
                            break;
                        }
                    }

                    if (b_SeEncontro == false)
                    {
                        lstRecetasIns.RemoveAt(n_row);
                    }
                }
            }


            for (n_row = 0; n_row <= lstOrigen.Count - 1; n_row++) 
            {
                b_SeEncontro = false;
                for (n_rowdet = 0; n_rowdet <= lstRecetasIns.Count - 1; n_rowdet++)
                {
                    if ((lstOrigen[n_row].n_idpro == lstRecetasIns[n_rowdet].n_idpro) && (lstOrigen[n_row].n_idrec == lstRecetasIns[n_rowdet].n_idrec) && (lstOrigen[n_row].n_idite == lstRecetasIns[n_rowdet].n_idite))
                    {
                        lstRecetasIns[n_rowdet].n_idpro = lstOrigen[n_row].n_idpro;
                        lstRecetasIns[n_rowdet].n_idrec = lstOrigen[n_row].n_idrec;
                        lstRecetasIns[n_rowdet].n_idite = lstOrigen[n_row].n_idite;
                        lstRecetasIns[n_rowdet].n_idunimed = lstOrigen[n_row].n_idunimed;
                        lstRecetasIns[n_rowdet].n_can = lstOrigen[n_row].n_can;
                        lstRecetasIns[n_rowdet].n_inspri = lstOrigen[n_row].n_inspri;
                        b_SeEncontro = true;
                        break;
                    }
                }

                if (b_SeEncontro == false)
                {
                    BE_PRO_PRODUCTOSRECETASINSUMOS entIns = new BE_PRO_PRODUCTOSRECETASINSUMOS();
                    entIns.n_idpro = lstOrigen[n_row].n_idpro;
                    entIns.n_idrec = lstOrigen[n_row].n_idrec;
                    entIns.n_idite = lstOrigen[n_row].n_idite;
                    entIns.n_idunimed = lstOrigen[n_row].n_idunimed;
                    entIns.n_can = lstOrigen[n_row].n_can;
                    entIns.n_inspri = lstOrigen[n_row].n_inspri;
                    lstRecetasIns.Add(entIns);
                    //break;
                }
            }   
        }
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }
        private void CmdVerLin_Click(object sender, EventArgs e)
        {
            if (FgLineas.Rows.Count == 2)
            {
                MessageBox.Show("No se han encontrado tareas para esta linea ¡ Debe de agregar una como minimo!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            if (funFunciones.NulosC(FgLineas.GetData(FgLineas.Row, 4)) == "")
            {
                MessageBox.Show("No ha indicado el tiempo de la linea", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            if (Convert.ToDouble(funFunciones.NulosN(FgLineas.GetData(FgLineas.Row, 5))) == 0)
            {
                MessageBox.Show("No ha indicado la cantidad de kilos a procesar", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            if (Convert.ToDouble(funFunciones.NulosN(FgLineas.GetData(FgLineas.Row, 11))) == 0)
            {
                MessageBox.Show("No ha indicado el precio por hora de la linea", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            int n_row = 0;
                int n_index = 0;
                int n_idLin = Convert.ToInt32(FgLineas.GetData(FgLineas.Row, 10).ToString());

                for (n_row = 0; n_row <= lstLineas.Count - 1; n_row++)
                {
                    if (lstLineas[n_row].n_id == n_idLin)
                    {
                        n_index = n_row;
                        break;
                    }
                }

                // FILTRAMOS SOLO LAS TAREAS DE LA LINEA  ACTUAL
                List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS> lstTemp = new List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS>();
                BE_PRO_PRODUCTOSRECETASLINEASTAREAS entTemp = new BE_PRO_PRODUCTOSRECETASLINEASTAREAS();
                
                for (n_row = 0; n_row <= lstLineasTar.Count - 1; n_row++)
                {
                    if (lstLineasTar[n_row].n_idlin == n_idLin)
                    {
                        entTemp = lstLineasTar[n_row];

                        lstTemp.Add(entTemp);
                    }
                }

                FrmVerLineasTareas MyForm = new FrmVerLineasTareas();
                MyForm.dtTipExi = dtTipExi;
                MyForm.dtItems = dtItems;
                MyForm.dtUniMed = dtUniMed;
                MyForm.entLinea = lstLineas[n_index];
                MyForm.lstLineaTar = lstTemp;
                MyForm.dtTareas = dtTareas;
                MyForm.dtEquipos = dtEquipos;

                if (CmdVerLin.Text != "Ver Linea")
                {
                    MyForm.b_Modificar = true;
                }
                MyForm.ShowDialog();

                if (CmdVerLin.Text != "Ver Linea")
                {
                    BE_PRO_PRODUCTOSRECETASLINEAS entLinTmp = new BE_PRO_PRODUCTOSRECETASLINEAS();
                    entLinTmp = MyForm.entLinea;
                    lstTemp = MyForm.lstLineaTar;
                    // ACTUALIZAMOS LOS DATOS DE LA LINEA
                    lstLineas[n_index].n_idpro = entLinTmp.n_idpro;
                    lstLineas[n_index].n_idrec = entLinTmp.n_idrec;
                    lstLineas[n_index].n_id = entLinTmp.n_id;
                    lstLineas[n_index].c_codlin = entLinTmp.c_codlin;
                    lstLineas[n_index].c_deslin = entLinTmp.c_deslin;
                    lstLineas[n_index].n_idunimed = entLinTmp.n_idunimed;
                    lstLineas[n_index].n_idite = entLinTmp.n_idite;
                    lstLineas[n_index].n_can = entLinTmp.n_can;
                    lstLineas[n_index].n_numope = entLinTmp.n_numope;
                    lstLineas[n_index].n_efi = entLinTmp.n_efi;
                    lstLineas[n_index].n_tiepro = entLinTmp.n_tiepro;
                    lstLineas[n_index].n_prehorjor = entLinTmp.n_prehorjor;
                    lstLineas[n_index].n_act = entLinTmp.n_act;
                    lstLineas[n_index].c_obs = entLinTmp.c_obs;

                    FgLineas.SetData(FgLineas.Row,6,lstLineas[n_index].n_numope);
                    FgLineas.SetData(FgLineas.Row, 7, lstLineas[n_index].n_efi);
                    AgregarNuevasTareasLista(lstTemp);
                }
                MyForm.Close();
                MyForm = null;
            
        }
        void AgregarNuevasTareasLista(List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS> lstOrigen)
        {
            int n_row = 0;
            int n_rowdet = 0;
            bool b_SeEncontro = false;

            //for (n_row = 0; n_row <= lstLineasTar.Count - 1; n_row++)
            //{
            //    if ((lstOrigen[n_row].n_idpro == lstLineasTar[n_rowdet].n_idpro) && (lstOrigen[n_row].n_idrec == lstLineasTar[n_rowdet].n_idrec) &&
            //            (lstOrigen[n_row].n_idlin == lstLineasTar[n_rowdet].n_idlin))
            //    {
            //        lstLineasTar.RemoveAt(n_row);
            //        n_row = n_row - 1;
            //    }
            //}
            for (n_row = 0; n_row <= lstLineasTar.Count - 1; n_row++)
            {
                for (n_rowdet = 0; n_rowdet <= lstOrigen.Count - 1; n_rowdet++)
                {
                    if ((lstOrigen[n_rowdet].n_idpro == lstLineasTar[n_row].n_idpro) && (lstOrigen[n_rowdet].n_idrec == lstLineasTar[n_row].n_idrec) &&
                            (lstOrigen[n_rowdet].n_idlin == lstLineasTar[n_row].n_idlin))
                    {
                        lstLineasTar.RemoveAt(n_row);

                        if (lstLineasTar.Count == 0)
                        { break; }
                        else
                        {
                            if (n_row > 0) { n_row = n_row - 1; }
                             
                        }
                    }
                }
            }

            for (n_row = 0; n_row <= lstOrigen.Count - 1; n_row++)
            {
                b_SeEncontro = false;
                for (n_rowdet = 0; n_rowdet <= lstLineasTar.Count - 1; n_rowdet++)
                {
                    if ((lstOrigen[n_row].n_idpro == lstLineasTar[n_rowdet].n_idpro) && (lstOrigen[n_row].n_idrec == lstLineasTar[n_rowdet].n_idrec) &&
                        (lstOrigen[n_row].n_idlin == lstLineasTar[n_rowdet].n_idlin) && (lstOrigen[n_row].n_idtar == lstLineasTar[n_rowdet].n_idtar))
                    {
                        lstLineasTar[n_rowdet].n_idpro= lstOrigen[n_row].n_idpro;
                        lstLineasTar[n_rowdet].n_idrec = lstOrigen[n_row].n_idrec;
                        lstLineasTar[n_rowdet].n_idlin = lstOrigen[n_row].n_idlin;
                        lstLineasTar[n_rowdet].n_idtar = lstOrigen[n_row].n_idtar;
                        lstLineasTar[n_rowdet].n_porefi = lstOrigen[n_row].n_porefi;
                        lstLineasTar[n_rowdet].n_cankilpro = lstOrigen[n_row].n_cankilpro;
                        lstLineasTar[n_rowdet].n_numpertar = lstOrigen[n_row].n_numpertar;
                        lstLineasTar[n_rowdet].n_idequipo = lstOrigen[n_row].n_idequipo;
                        lstLineasTar[n_rowdet].n_numpertarequ = lstOrigen[n_row].n_numpertarequ;
                        lstLineasTar[n_rowdet].n_capkilporper = lstOrigen[n_row].n_capkilporper;
                        lstLineasTar[n_rowdet].n_capkilporhorlin = lstOrigen[n_row].n_capkilporhorlin;
                        lstLineasTar[n_rowdet].n_capkilporlintietra = lstOrigen[n_row].n_capkilporlintietra;
                        lstLineasTar[n_rowdet].n_numpercal = lstOrigen[n_row].n_numpercal;
                        lstLineasTar[n_rowdet].n_totprotietra = lstOrigen[n_row].n_totprotietra;
                        lstLineasTar[n_rowdet].n_porefiuni = lstOrigen[n_row].n_porefiuni;
                        lstLineasTar[n_rowdet].n_porefitot = lstOrigen[n_row].n_porefitot;
                        lstLineasTar[n_rowdet].n_kghper = lstOrigen[n_row].n_kghper;
                        lstLineasTar[n_rowdet].n_costar = lstOrigen[n_row].n_costar;
                        lstLineasTar[n_rowdet].n_ord = lstOrigen[n_row].n_ord;
                        b_SeEncontro = true;
                        break;
                    }
                }

                if (b_SeEncontro == false)
                {
                    BE_PRO_PRODUCTOSRECETASLINEASTAREAS entTar = new BE_PRO_PRODUCTOSRECETASLINEASTAREAS();

                    entTar.n_idpro = lstOrigen[n_row].n_idpro;
                    entTar.n_idrec = lstOrigen[n_row].n_idrec;
                    entTar.n_idlin = lstOrigen[n_row].n_idlin;
                    entTar.n_idtar = lstOrigen[n_row].n_idtar;
                    entTar.n_porefi = lstOrigen[n_row].n_porefi;
                    entTar.n_cankilpro = lstOrigen[n_row].n_cankilpro;
                    entTar.n_numpertar = lstOrigen[n_row].n_numpertar;
                    entTar.n_idequipo = lstOrigen[n_row].n_idequipo;
                    entTar.n_numpertarequ = lstOrigen[n_row].n_numpertarequ;
                    entTar.n_capkilporper = lstOrigen[n_row].n_capkilporper;
                    entTar.n_capkilporhorlin = lstOrigen[n_row].n_capkilporhorlin;
                    entTar.n_capkilporlintietra = lstOrigen[n_row].n_capkilporlintietra;
                    entTar.n_numpercal = lstOrigen[n_row].n_numpercal;
                    entTar.n_totprotietra = lstOrigen[n_row].n_totprotietra;
                    entTar.n_porefiuni = lstOrigen[n_row].n_porefiuni;
                    entTar.n_porefitot = lstOrigen[n_row].n_porefitot;
                    entTar.n_kghper = lstOrigen[n_row].n_kghper;
                    entTar.n_costar = lstOrigen[n_row].n_costar;
                    entTar.n_ord = lstOrigen[n_row].n_ord;

                    lstLineasTar.Add(entTar);
                }
            }  
        }
        private void FgLocal_RowColChange(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            MostrarLocalAlmacenUbicacion(Convert.ToInt32(FgLocal.GetData(FgLocal.Row,2)));
        }
        private void AmdAddLin_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (FgLineas.Rows.Count != 2)
            {
                if (funFunciones.NulosC(FgLineas.GetData(FgLineas.Row, 1)) == "")
                {
                    MessageBox.Show("¡ No se ha especificado el nombre de la linea de la ultima linea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (funFunciones.NulosC(FgLineas.GetData(FgLineas.Row, 2)) == "")
                {
                    MessageBox.Show("¡ No se ha especificado la unidad de medida de la ultima linea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (funFunciones.NulosC(FgLineas.GetData(FgLineas.Row, 3)) == "")
                {
                    MessageBox.Show("¡ o se ha especificado el insumo principal de la linea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (funFunciones.NulosC(FgLineas.GetData(FgLineas.Row, 4)) == "")
                {
                    MessageBox.Show("¡ No ha especificado el tiempo de la linea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (funFunciones.NulosC(FgLineas.GetData(FgLineas.Row, 5)) == "")
                {
                    MessageBox.Show("¡ No se ha especificado la cantidad por hora a procesar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            booAgregando = true;
            FgLineas.Rows.Count = FgLineas.Rows.Count + 1;
            n_IdLinea = n_IdLinea + 1;
            BE_PRO_PRODUCTOSRECETASLINEAS entLin = new BE_PRO_PRODUCTOSRECETASLINEAS();

            entLin.n_idpro = entRegistro.n_id;
            entLin.n_idrec = Convert.ToInt32(FgRec.GetData(FgRec.Row,7));
            entLin.n_id = n_IdLinea;
            lstLineas.Add(entLin);

            FgLineas.SetData(FgLineas.Rows.Count - 1, 9, entLin.n_idrec.ToString());
            FgLineas.SetData(FgLineas.Rows.Count - 1, 10, entLin.n_id.ToString());
            if (FgLineas.Rows.Count - 1 == 2)
            {
                FgLineas.SetData(FgLineas.Rows.Count - 1, 8,"True");
            }
            FgLineas.Select(FgLineas.Rows.Count - 1, 1);
            booAgregando = false;
        }
        private void CmdAddRec_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (FgRec.Rows.Count > 2) 
            {
                if (funFunciones.NulosC(FgRec.GetData(FgRec.Rows.Count-1, 1)) == "")
                {
                    MessageBox.Show("¡ No se ha especificado el codigo de la receta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (funFunciones.NulosC(FgRec.GetData(FgRec.Rows.Count - 1, 2)) == "")
                {
                    MessageBox.Show("¡ No se ha especificado la descripcion de la receta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (funFunciones.NulosC(FgRec.GetData(FgRec.Rows.Count - 1, 3)) == "")
                {
                    MessageBox.Show("¡ No se ha especificado la unidad de medida de la receta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (funFunciones.NulosC(FgRec.GetData(FgRec.Rows.Count - 1, 4)) == "")
                {
                    MessageBox.Show("¡ No se ha especificado la cantidad de la receta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (funFunciones.NulosC(FgRec.GetData(FgRec.Rows.Count - 1, 5)) == "")
                {
                    MessageBox.Show("¡ No se ha especificado la prioridad de la rceta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            booAgregando = true;
            BE_PRO_PRODUCTOSRECETAS entRec = new BE_PRO_PRODUCTOSRECETAS();
            
            FgRec.Rows.Count = FgRec.Rows.Count + 1;
            FgLineas.Rows.Count = 2;
            n_IdRec = n_IdRec + 1;
            entRec.n_id = n_IdRec;
            lstRecetas.Add(entRec);

            FgRec.SetData(FgRec.Rows.Count-1, 7, n_IdRec.ToString());
            FgRec.Select(FgRec.Rows.Count - 1, 1);
            booAgregando = false;
        }
        private void CmdDelLin_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            int n_row = 0;
            int n_fila = 0;
            int n_index = 0;
            bool b_encontro = false;

            int m_idlin = Convert.ToInt32(FgRec.GetData(FgRec.Row, 7));

            for (n_row = 2; n_row <= FgLineas.Rows.Count - 1; n_row++)
            {
                for (n_fila = 0; n_fila <= lstLineas.Count-1; n_fila++)
                {
                    if (m_idlin == lstLineas[n_fila].n_id)
                    {
                        n_index = n_fila;
                        b_encontro = true;
                        break;
                    }   
                }
                if (b_encontro == true) { break; }
            }

            lstLineas.RemoveAt(n_index);
            FgLineas.RemoveItem(FgLineas.Row);
        }
        private void CmdDelRec_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            int n_row = 0;
            int n_fila = 0;
            int n_index = 0;
            bool b_encontro = false;

            int m_idrec = Convert.ToInt32(FgRec.GetData(FgRec.Row, 7));

            for (n_row = 2; n_row <= FgRec.Rows.Count - 1; n_row++)
            {
                for (n_fila = 0; n_fila <= lstRecetas.Count - 1; n_fila++)
                {
                    if ( m_idrec == lstRecetas[n_fila].n_id)
                    {
                        n_index = n_fila;
                        b_encontro = true;
                        break;
                    }
                }

                if (b_encontro == true) { break; }
            }

            lstRecetas.RemoveAt(n_index);
            FgRec.RemoveItem(FgRec.Row);
        }
        private void CmdAddLoc_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            FgLocal.Rows.Count = FgLocal.Rows.Count + 1;
        }
        private void CmdAddAlm_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            FgAlmacenes.Rows.Count = FgAlmacenes.Rows.Count + 1;
        }
        private void CmdEdiLoc_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            FgLocal.RemoveItem(FgLocal.Row);
        }
        private void CmdEdiAlm_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            FgAlmacenes.RemoveItem(FgAlmacenes.Row);
        }

        private void FgRec_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            //DataTable dtResul;
            //string c_despro;
            //int n_idpro;
            int n_IdReceta = 0;
            int n_IdUniMed = 0;
            int n_fila =0;
            int n_Indice = 0;

            if (n_QueHace == 3)
            {
                FgRec.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            n_IdReceta = Convert.ToInt32(FgRec.GetData(FgRec.Row, 7));

            for (n_fila = 0; n_fila <= lstRecetas.Count-1; n_fila++)
            {
                if (lstRecetas[n_fila].n_id == n_IdReceta)
                {
                    n_Indice = n_fila;
                    break;
                }
            }
            
            if (FgRec.Col == 1)                     // CODIGO DE LA RECETA
            {
                lstRecetas[n_Indice].c_codrec = funFunciones.NulosC(FgRec.GetData(FgRec.Row, 1).ToString());
            }

            if (FgRec.Col == 2)                     // DESCRIPCION DE LA RECETA
            {
                
                lstRecetas[n_Indice].c_des = FgRec.GetData(FgRec.Row, 2).ToString();
            }

            if (FgRec.Col == 3)                     // UNIDAD DE MEDIDA
            {
                n_IdUniMed = 0;
                string c_dato = "";
                c_dato = FgRec.GetData(FgRec.Row, 3).ToString();
                c_dato = funDatos.DataTableBuscar(dtUniMed, "c_abr", "n_id", c_dato, "C").ToString();
                n_IdUniMed = Convert.ToInt32(c_dato);
                lstRecetas[n_Indice].n_idunimed = n_IdUniMed;
            }

            if (FgRec.Col == 4)                     // CANTIDAD
            {
                double n_Valor = 0;
                n_Valor = Convert.ToDouble(FgRec.GetData(FgRec.Row, 4));
                FgRec.SetData(FgRec.Row, 4, n_Valor.ToString("0.00"));
                lstRecetas[n_Indice].n_can = Convert.ToDouble(FgRec.GetData(FgRec.Row, 4).ToString());
            }

            if (FgRec.Col == 5)                     // PRIORIDAD
            {
                lstRecetas[n_Indice].n_prirec = Convert.ToInt32(FgRec.GetData(FgRec.Row, 5).ToString());
            }

            if (FgRec.Col == 6)                     // OBSERVACIONES
            {
                lstRecetas[n_Indice].c_obs = FgRec.GetData(FgRec.Row, 6).ToString();
            }

            if (FgRec.Col == 8)                     // ACTIVO
            {
                booAgregando = true;
                int n_act = Convert.ToInt32(funFunciones.NulosN(FgRec.GetData(FgRec.Row, 8)));
                lstRecetas[n_Indice].n_act = n_act;
                if (n_act == 1)
                {
                    FgRec.SetData(FgRec.Row, 8, "true");
                }
                else
                {
                    FgRec.SetData(FgRec.Row, 8, "false");
                }

                booAgregando = false;
            }
        }

        private void FgRec_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgRec.AllowEditing = false; return;
            }
            if (FgRec.Rows.Count == 2) { return; }
            
            if (booAgregando == true) { return; }

            DataTable dtResul = new DataTable();

            if (FgRec.Col == 1)                     // CODIGO DE LA RECETA
            {
            }

            if (FgRec.Col == 2)                     // DESCRIPCION DE LA RECETA
            {
            }

            if (FgRec.Col == 3)                     // UNIDAD DE MEDIDA
            {

                funFlex.FlexColumnaCombo(FgRec, dtUniMed, "c_abr", 3);    // ITEMS
            }

            if (FgRec.Col == 4)                     // CANTIDAD
            {
            }

            if (FgRec.Col == 5)                     // PRIORIDAD
            {
            }

            if (FgRec.Col == 6)                     // OBSERVACIONES
            {
            }

            if (FgRec.Col == 7)                     // ACTIVO
            {
            }

            FgRec.AllowEditing = true;
        }

        private void FgRec_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if ((e.Col == 1) || (e.Col == 2) || (e.Col == 6))
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            
            if ((e.Col == 4) || (e.Col == 5))
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void FgLineas_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            //DataTable dtResul;
            //string c_despro;
            //int n_idpro;
            int n_IdReceta = 0;
            int n_IdUniMed = 0;
            int n_fila = 0;
            int n_Indice = 0;
            int n_IdLin = 0;

            if (n_QueHace == 3)
            {
                FgLineas.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            if (FgLineas.Rows.Count == 2) { return; }

            n_IdReceta = Convert.ToInt32(FgLineas.GetData(FgLineas.Row, 9));
            n_IdLin = Convert.ToInt32(FgLineas.GetData(FgLineas.Row, 10));

            for (n_fila = 0; n_fila <= lstLineas.Count - 1; n_fila++)
            {
                if (lstLineas[n_fila].n_idrec == n_IdReceta)
                {
                    if (lstLineas[n_fila].n_id == n_IdLin)
                    { 
                        n_Indice = n_fila;
                        break;
                    }
                }
            }

            if (FgLineas.Col == 1)                     // DESCRIPCION DE LA LINEA
            {
                lstLineas[n_Indice].c_deslin = funFunciones.NulosC(FgLineas.GetData(FgLineas.Row, 1));
            }

            if (FgLineas.Col == 2)                     // UNIDAD DE MEDIDA
            {
                n_IdUniMed = 0;
                string c_dato = "";
                c_dato = funFunciones.NulosC(FgLineas.GetData(FgLineas.Row, 2));
                if (c_dato != "")
                {
                    c_dato = funDatos.DataTableBuscar(dtUniMed, "c_abr", "n_id", c_dato, "C").ToString();
                    n_IdUniMed = Convert.ToInt32(c_dato);
                    lstLineas[n_Indice].n_idunimed = n_IdUniMed;
                }
            }

            if (FgLineas.Col == 3)                     // INSUMO O PRODUCTO PRINCIPAL   
            {
                int n_IdIns = 0;
                string c_dato = "";
                c_dato = funFunciones.NulosC(FgLineas.GetData(FgLineas.Row, 3));
                if (c_dato != "")
                { 
                    c_dato = funDatos.DataTableBuscar(dtItems, "c_despro", "n_id", c_dato, "C").ToString();
                    n_IdIns = Convert.ToInt32(c_dato);
                    lstLineas[n_Indice].n_idite = n_IdIns;
                }
            }

            if (FgLineas.Col == 4)                     // TIEMPO DE LA LINEA
            {
                string c_valor = "";
                double n_Valor = 0;
                if (funFunciones.EsHora(FgLineas.GetData(FgLineas.Row, 4).ToString()) == false)
                {
                    MessageBox.Show("¡ La hora ingresada no es valida !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booAgregando = true;
                    FgLineas.SetData(FgLineas.Row, 4, "");
                    booAgregando = false;
                    return;
                }

                if (funFunciones.NulosC(FgLineas.GetData(FgLineas.Row, 4)) != "")
                {
                    c_valor = FgLineas.GetData(FgLineas.Row, 4).ToString() + ":00";
                    n_Valor = funCon.HoraEnDecimal(c_valor);
                    lstLineas[n_Indice].n_tiepro = n_Valor;
                }
                else
                {
                    lstLineas[n_Indice].n_tiepro = 0;
                }
            }

            if (FgLineas.Col == 5)                     // CANTIDAD POR HORA
            {
                if (funFunciones.EsNumerico(FgLineas.GetData(FgLineas.Row, 5).ToString()) == false)
                {
                    MessageBox.Show("¡ El valor ingresado no es un valor numerico !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booAgregando = true;
                    FgLineas.SetData(FgLineas.Row, 5, "");
                    booAgregando = false;
                    return;
                }
                lstLineas[n_Indice].n_can = Convert.ToDouble(funFunciones.NulosN(FgLineas.GetData(FgLineas.Row, 5)));
            }

            if (FgLineas.Col == 6)                     // NUMERO DE OPERARIOS
            {
                lstLineas[n_Indice].n_numope = Convert.ToDouble(funFunciones.NulosN(FgLineas.GetData(FgLineas.Row, 6)));
            }

            if (FgLineas.Col == 7)                     // EFICIENCIA
            {
                lstLineas[n_Indice].n_efi = Convert.ToDouble(funFunciones.NulosN( FgLineas.GetData(FgLineas.Row, 7)));
            }

            if (FgLineas.Col == 8)                     // ACTIVO
            {
                booAgregando = true;

                //ActualizarCkeck(Convert.ToInt32(funFunciones.NulosN(FgLineas.GetData(FgLineas.Row, 10))));
                int n_act = Convert.ToInt32(funFunciones.NulosN(FgLineas.GetData(FgLineas.Row, 8)));
                lstLineas[n_Indice].n_act = n_act;
                if (n_act == 1)
                {
                    FgLineas.SetData(FgLineas.Row, 8, "true");
                }
                else
                {
                    FgLineas.SetData(FgLineas.Row, 8, "false");
                }
                booAgregando = false;
            }
            if (FgLineas.Col == 11)                     // PRECIO HORA JORNAL
            {
                if (funFunciones.EsNumerico(FgLineas.GetData(FgLineas.Row, 11).ToString()) == false)
                {
                    MessageBox.Show("¡ El valor ingresado no es un valor numerico !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booAgregando = true;
                    FgLineas.SetData(FgLineas.Row, 11, "");
                    booAgregando = false;
                    return;
                }
                lstLineas[n_Indice].n_prehorjor = Convert.ToDouble(funFunciones.NulosN(FgLineas.GetData(FgLineas.Row, 11)));
            }
        }
        void ActualizarCkeck(int n_IdReceta)
        {
            int n_fila = 0;
            // LIMPRIAMOS EL CAMPO N_IDACT DE LA LISTA
            for (n_fila = 0; n_fila <= lstLineas.Count - 1; n_fila++)
            {
                if (lstLineas[n_fila].n_idrec == n_IdReceta) 
                {
                    lstLineas[n_fila].n_act = 0;
                }
            }

            // LIMPIAMOS LA COLUMNA ACTIVO DE LA FLEX LINEAS
            for (n_fila = 2; n_fila <= FgLineas.Rows.Count - 1; n_fila++)
            {
                FgLineas.SetData(n_fila, 8, "0");
            }
        }
        private void FgLineas_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgRec.AllowEditing = false; return;
            }
            if (FgRec.Rows.Count == 2) { return; }

            if (booAgregando == true) { return; }

            DataTable dtResul = new DataTable();

            if (FgLineas.Col == 1)                     // DESCRIPCION DE LA LINEA
            {
                FgLineas.AllowEditing = true;
            }

            if (FgLineas.Col == 2)                     // UNIDAD DE MEDIDA DE LA LINEA
            {
                //FgLineas.AllowEditing = true;
                funFlex.FlexColumnaCombo(FgLineas, dtUniMed, "c_abr", 2);    // ITEMS
            }

            if (FgLineas.Col == 3)                     // PRODUCTO, INSUMO O MATERIA PRIMA PRINCIPAL
            {
                //FgLineas.AllowEditing = true;
                DataTable dtres = new DataTable();
                dtres = funDatos.DataTableFiltrar(dtItems, "n_idtipexi IN (2, 3, 6)");
                funFlex.FlexColumnaCombo(FgLineas, dtres, "c_despro", 3);    // ITEMS
                FgLineas.AllowEditing = true;
            }

            if (FgLineas.Col == 4)                     // TIEMPO DE LA LINEA
            {
                FgLineas.AllowEditing = true;
            }

            if (FgLineas.Col == 5)                     // CANTIDAD POR HORA DE LA LINEA
            {
                FgLineas.AllowEditing = true;
            }

            if (FgLineas.Col == 6)                     // OPERARIOS
            {
                FgLineas.AllowEditing = false;
            }

            if (FgLineas.Col == 7)                     // EFICIENCIA
            {
                FgLineas.AllowEditing = false;
            }
            if (FgLineas.Col == 8)                     //ACTIVO
            {
                FgLineas.AllowEditing = true;
            }
            if (FgLineas.Col == 11)                     //ACTIVO
            {
                FgLineas.AllowEditing = true;
            }
        }
        private void FgLineas_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if ((e.Col == 1))
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            if ((e.Col == 4) || (e.Col == 5) || (e.Col == 6) || (e.Col == 7) || (e.Col == 8) || (e.Col == 11))
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void ChkAct_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAct.Checked == true)
            {
                LblEst.ForeColor=Color.Navy;
                LblEst.Text = "ACTIVO";
            }
            else
            {
                LblEst.ForeColor = Color.Red; 
                LblEst.Text = "NO ACTIVO";
            }
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

        private void CboFam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CboCla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CboSubCla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CboUniMed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtProducto_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtCodPro_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtProducto_TextChanged(object sender, EventArgs e)
        {

        }

        private void CboFam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }

            booAgregando = true;
            DataTable dtResul = new DataTable();
            dtResul = funDatos.DataTableFiltrar(dtCla, "(n_idtipexi = 2) AND (n_idfam = " + CboFam.SelectedValue.ToString() + " )");
            funDatos.ComboBoxCargarDataTable(CboCla, dtResul, "n_id", "c_des");
            booAgregando = false;
        }

        private void CboCla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }

            DataTable dtResul = new DataTable();
            dtResul = funDatos.DataTableFiltrar(dtSubCla, "(n_idtipexi = 2)");
            dtResul = funDatos.DataTableFiltrar(dtResul, "(n_idfam = " + CboFam.SelectedValue.ToString() + " ) AND (n_idcla = " + CboCla.SelectedValue.ToString() + ")");
            funDatos.ComboBoxCargarDataTable(CboSubCla, dtResul, "n_id", "c_des");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-PRO-PRODUCTOS" + STU_SISTEMA.ANOTRABAJO.ToString() + Convert.ToInt32(CboMeses.SelectedValue).ToString("00") + ".xls";
            DgLista.ExportTo(c_NomArchivo);
            MessageBox.Show("! Se exporto con exito la informacion en el archivo " + c_NomArchivo + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void Tab1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;

            if (n_QueHace != 3) { return; }

            if (tc.SelectedIndex == 1)
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }

        private void ExportarExcelButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (FgRec.Rows.Count == 2)
                {
                    MessageBox.Show("No se han encontrado recetas ¡ Debe de agregar una como minimo!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                else
                {
                    int n_row = 0;
                    int n_index = 0;
                    int n_idre = Convert.ToInt32(FgRec.GetData(FgRec.Row, 7).ToString());

                    for (n_row = 0; n_row <= lstRecetas.Count - 1; n_row++)
                    {
                        if (lstRecetas[n_row].n_id == n_idre)
                        {
                            n_index = n_row;
                            break;
                        }
                    }

                    // FILTRAMOS SOLO LOS INSUMOS DE LA RECETA  ACTUAL
                    List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstTemp = new List<BE_PRO_PRODUCTOSRECETASINSUMOS>();
                    BE_PRO_PRODUCTOSRECETASINSUMOS entTemp = new BE_PRO_PRODUCTOSRECETASINSUMOS();

                    for (n_row = 0; n_row <= lstRecetasIns.Count - 1; n_row++)
                    {
                        if (lstRecetasIns[n_row].n_idrec == n_idre)
                        {
                            entTemp = lstRecetasIns[n_row];

                            lstTemp.Add(entTemp);
                        }
                    }


                    SaveFileDialog objCuadroDialogo = new SaveFileDialog();
                    objCuadroDialogo.Filter = "MS Excel (*.xlsx) |*.xlsx;*.xlsx|(*.xlsx) |*.xlsx|(*.*) |*.*";

                    if (objCuadroDialogo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ExportarAExcel(objCuadroDialogo.FileName, lstRecetas[n_index], lstTemp);

                        MessageBox.Show("El archivo se exportó correctamente", "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        string filename = "Excel.exe";

                        Process proc = new Process();
                        proc.EnableRaisingEvents = false;
                        proc.StartInfo.FileName = filename;
                        proc.StartInfo.Arguments = objCuadroDialogo.FileName;
                        proc.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrio un error: {0}", ex.Message), "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportarAExcel(string pFilePath
            , BE_PRO_PRODUCTOSRECETAS entReceta
            , List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstRecetasIns)
        {
            try
            {
                if (entReceta != null && lstRecetasIns.Count > 0)
                {
                    IWorkbook workbook = null;
                    ISheet worksheet = null;

                    using (FileStream stream = new FileStream(pFilePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        string Ext = System.IO.Path.GetExtension(pFilePath); //<-Extension del archivo
                        switch (Ext.ToLower())
                        {
                            case ".xls":
                                HSSFWorkbook workbookH = new HSSFWorkbook();
                                NPOI.HPSF.DocumentSummaryInformation dsi = NPOI.HPSF.PropertySetFactory.CreateDocumentSummaryInformation();
                                dsi.Company = "Cutcsa"; dsi.Manager = "Departamento Informatico";
                                workbookH.DocumentSummaryInformation = dsi;
                                workbook = workbookH;
                                break;

                            case ".xlsx": workbook = new XSSFWorkbook(); break;
                        }

                        worksheet = workbook.CreateSheet("Reporte de Receta"); //<-Usa el nombre de la tabla como nombre de la Hoja

                        //Fuente negita
                        var font = workbook.CreateFont();
                        font.FontHeightInPoints = 11;
                        font.FontName = "Calibri";
                        font.Boldweight = (short)FontBoldWeight.Bold;

                        //CREAR EN LA PRIMERA FILA LOS TITULOS DE LAS COLUMNAS
                        int iRow = 0;
                        IRow fila = worksheet.CreateRow(iRow);
                        ICell cell = fila.CreateCell(0, CellType.String);
                        cell.SetCellValue("RECETA");
                        cell.CellStyle = workbook.CreateCellStyle();
                        cell.CellStyle.SetFont(font);

                        cell = fila.CreateCell(1, CellType.String);
                        cell.SetCellValue(string.Format("{0}-{1}", entReceta.c_codrec, entReceta.c_des));
                        iRow += 2;

                        fila = worksheet.CreateRow(iRow);
                        // Columnas de la tabla
                        cell = fila.CreateCell(0, CellType.String);
                        cell.SetCellValue("Código");
                        cell.CellStyle = workbook.CreateCellStyle();
                        cell.CellStyle.SetFont(font);

                        cell = fila.CreateCell(1, CellType.String);
                        cell.SetCellValue("Descripción");
                        cell.CellStyle = workbook.CreateCellStyle();
                        cell.CellStyle.SetFont(font);

                        cell = fila.CreateCell(2, CellType.String);
                        cell.SetCellValue("Cantidad");
                        cell.CellStyle = workbook.CreateCellStyle();
                        cell.CellStyle.SetFont(font);

                        cell = fila.CreateCell(3, CellType.String);
                        cell.SetCellValue("Unidad");
                        cell.CellStyle = workbook.CreateCellStyle();
                        cell.CellStyle.SetFont(font);
                        iRow++;

                        foreach (BE_PRO_PRODUCTOSRECETASINSUMOS RecetasIns in lstRecetasIns)
                        {
                            // Valores de la tabla
                            fila = worksheet.CreateRow(iRow);

                            // CODIGO DEL PRODUCTO
                            var c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_codpro", RecetasIns.n_idite.ToString(), "N").ToString();
                            cell = fila.CreateCell(0, CellType.String);
                            cell.SetCellValue(c_dato);

                            // DESCRIPCION DEL PRODUCTO
                            c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", RecetasIns.n_idite.ToString(), "N").ToString();
                            cell = fila.CreateCell(1, CellType.String);
                            cell.SetCellValue(c_dato);

                            // CANTIDAD
                            cell = fila.CreateCell(2, CellType.String);
                            cell.SetCellValue(RecetasIns.n_can);

                            // UNIDAD DE MEDIDA DEL PRODUCTO
                            c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_abr", RecetasIns.n_idunimed.ToString(), "N").ToString();
                            cell = fila.CreateCell(3, CellType.String);
                            cell.SetCellValue(c_dato);

                            iRow++;
                        }
                        
                        //Ancho de las columnas
                        worksheet.AutoSizeColumn(0);
                        worksheet.AutoSizeColumn(1);
                        worksheet.AutoSizeColumn(2);
                        worksheet.AutoSizeColumn(3);
                        
                        workbook.Write(stream);
                        stream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
