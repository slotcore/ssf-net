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
using SIAC_Entidades.Produccion;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
//using System.Data.OleDb;

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmRevision : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        //public OleDbConnection AccConec = new OleDbConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        BE_PRO_REVISION BE_Revision = new BE_PRO_REVISION();

        // OBJETOS LOCALES
        CN_pro_revision objRegistro = new CN_pro_revision();
        CN_alm_inventario ObjItem = new CN_alm_inventario();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_sys_personaladjunto objPerAdj = new CN_sys_personaladjunto();
        CN_alm_inventariounimed objUniMed = new CN_alm_inventariounimed();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_pro_productosrecetas objReceta = new CN_pro_productosrecetas();

        //CN_ACC_pro_producciondet ObjReceta = new CN_ACC_pro_producciondet();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // DATATABLE LOCALES
        DataTable dtMovimientos = new DataTable();
        DataTable dtTipoDocumento = new DataTable();
        DataTable dtResponsables = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtPresentaItem = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtPerAdj = new DataTable();
        DataTable dtReceta = new DataTable();
        
        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[12, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmRevision()
        {
            InitializeComponent();
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label21_Click(object sender, EventArgs e)
        {

        }
        private void FrmRevision_Load(object sender, EventArgs e)
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
            funDatos.ComboBoxCargarDataTable(CboProducto, dtItems, "n_id", "c_despro");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtTipoDocumento, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboReceta, dtReceta, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboPerRes, dtPerAdj, "n_id", "c_apenom");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 587;
            this.Width = 930;
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
            
            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(24, ref arrCabeceraDg1);
        }
        void DataTableCargar()
        {
            objRegistro.mysConec = mysConec;
            dtMovimientos = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);

            ObjItem.mysConec = mysConec;
            dtItems = ObjItem.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);

            objTipDoc.mysConec = mysConec;
            dtTipoDocumento = objTipDoc.Listar();

            objUniMed.mysConec = mysConec;
            dtPresentaItem = objUniMed.Listar();

            objPerAdj.mysConec = mysConec;
            dtPerAdj = objPerAdj.Listar(STU_SISTEMA.EMPRESAID);

            objReceta.mysConec = mysConec;
            objReceta.ListarTodasRecetas(0);
            dtReceta = objReceta.dtRecetas;
            //ObjReceta.AccConec = AccConec;
            //dtReceta = ObjReceta.ListarRecetas();                          // 

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();
          
            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(24);
        }
        void ListarItems()
        {
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
        void VerRegistro(Int64 n_IdRegistro)
        {
            DataTable dtresulpre = new DataTable();

            objRegistro.mysConec = mysConec;
            BE_Revision = objRegistro.TraerRegistro(n_IdRegistro);

            TxtNumSer.Text = BE_Revision.c_numser;
            TxtNumDoc.Text = BE_Revision.c_numdoc;

            LblIdProducion.Text = BE_Revision.n_idpro.ToString();
            CboProducto.SelectedValue = BE_Revision.n_idite;
            CboReceta.SelectedValue = BE_Revision.n_idrec;
            CboTipDoc.SelectedValue = BE_Revision.n_idtipdoc;
            
            // MOSTRAMOS LA PRESENTACION DEL ITEM
            dtresulpre = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + BE_Revision.n_idite + " AND n_default = 1");
            funDatos.ComboBoxCargarDataTable(CboUniMed, dtresulpre, "n_id", "c_despre");
            if (dtresulpre.Rows.Count != 0)
            {
                CboUniMed.SelectedValue = Convert.ToInt32(dtresulpre.Rows[0]["n_id"].ToString());
            }
            
            TxtNumParteProd.Text = BE_Revision.c_numdocref;
            TxtNumLot.Text = BE_Revision.c_numlot.ToString();
            TxtCantidad.Text = BE_Revision.n_canpro.ToString();
            TxtFchIni.Text = BE_Revision.d_fchini.ToString();
            TxtFchFin.Text = BE_Revision.d_fchfin.ToString();
            TxtHorIni.Text = BE_Revision.h_horini.ToString();
            TxtHorFin.Text = BE_Revision.h_horfin.ToString();
            TxtCanProCon.Text = BE_Revision.n_canprocon.ToString();
            TxtObsProCon.Text = BE_Revision.c_obsprocon.ToString();
            TxtCanProNoCon.Text = BE_Revision.n_canpronocon.ToString();
            TxtObsProNoCon.Text = BE_Revision.c_obspronocon.ToString();
            CboPerRes.SelectedValue = BE_Revision.n_idperrev;
            TxtFchRev.Text = BE_Revision.d_fchrev.ToString();
            TxtHorRev.Text = BE_Revision.h_horrev.ToString();
            LblIdProducion.Text = BE_Revision.n_iddocref.ToString();
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

            TxtNumSer.Text = "0001";

            string c_numdoc = "";
            c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 74, TxtNumSer.Text);
            TxtNumDoc.Text = c_numdoc;
            CboTipDoc.SelectedValue = 73;
            CmdBusParProd.Focus();
        }
        void Blanquea()
        {
            CboProducto.SelectedValue = 0;
            TxtNumLot.Text = "";
            CboTipDoc.SelectedValue = 0;
            CboReceta.SelectedValue = 0;
            CboUniMed.SelectedValue = 0;
            TxtCantidad.Text = "";
            TxtFchIni.Text = "";
            TxtFchFin.Text = "";
            TxtHorIni.Text = "";
            TxtHorFin.Text = "";
            TxtCanProCon.Text = "";
            TxtObsProCon.Text = "";
            TxtCanProNoCon.Text = "";
            TxtObsProNoCon.Text = "";
            CboPerRes.SelectedValue = 0;
            TxtFchRev.Text = "";
            TxtHorRev.Text = "";
        }
        void Bloquea()
        {
            //CboProd.Enabled = !CboProd.Enabled;
            //TxtReceta.Enabled = !TxtReceta.Enabled;
            //TxtNumLot.Enabled = !TxtNumLot.Enabled;
            //TxtUniMed.Enabled = !TxtUniMed.Enabled;
            //TxtCantidad.Enabled = !TxtCantidad.Enabled;
            //TxtFchIni.Enabled = !TxtFchIni.Enabled;
            //TxtFchFin.Enabled = !TxtFchFin.Enabled;
            //TxtHorIni.Enabled = !TxtHorIni.Enabled;
            //TxtHorFin.Enabled = !TxtHorFin.Enabled;
            CmdBusParProd.Enabled = !CmdBusParProd.Enabled;
            TxtCanProCon.Enabled = !TxtCanProCon.Enabled;
            TxtObsProCon.Enabled = !TxtObsProCon.Enabled;
            TxtCanProNoCon.Enabled = !TxtCanProNoCon.Enabled;
            TxtObsProNoCon.Enabled = !TxtObsProNoCon.Enabled;
            CboPerRes.Enabled = !CboPerRes.Enabled;
            TxtFchRev.Enabled = !TxtFchRev.Enabled;
            TxtHorRev.Enabled = !TxtHorRev.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir2.Enabled = !ToolImprimir2.Enabled;
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
            CboProducto.Focus();
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            objRegistro.mysConec = mysConec;
            BE_Revision = objRegistro.TraerRegistro(n_IdRegistro);

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                //objRegistro.AccConec = AccConec;
                if (objRegistro.Eliminar(n_IdRegistro, BE_Revision.n_idpro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistro.mysConec = mysConec;
                    dtMovimientos = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
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
            objRegistro.mysConec = mysConec;
            if (n_QueHace == 1)
            {
                booResultado = objRegistro.Insertar(BE_Revision);
            }

            if (n_QueHace == 2)
            {
                BE_Revision.n_id = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
                booResultado = objRegistro.Actualizar(BE_Revision);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ No se pudo guardar el registro por el siguiente motivo ! " + objRegistro.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {

            BE_Revision.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_Revision.n_id = 0;
            BE_Revision.n_idtipdoc = 74;
            BE_Revision.c_numser = TxtNumSer.Text;
            BE_Revision.c_numdoc = TxtNumDoc.Text;
            BE_Revision.n_idtipdocref = Convert.ToInt32(CboTipDoc.SelectedValue);
            BE_Revision.n_iddocref = Convert.ToInt32(LblIdProducion.Text);
            BE_Revision.c_numdocref = TxtNumParteProd.Text;
            BE_Revision.n_idpro = Convert.ToInt32(LblIdProducion.Text);
            //BE_Revision.c_numdoc = TxtNumParteProd.Text;
            BE_Revision.n_idpro = Convert.ToInt32(LblIdProducion.Text);
            BE_Revision.n_idite = Convert.ToInt32(CboProducto.SelectedValue);
            BE_Revision.n_idrec = Convert.ToInt32(CboReceta.SelectedValue);
            BE_Revision.c_numlot = TxtNumLot.Text;
            BE_Revision.n_canpro = Convert.ToDouble(funFunciones.NulosN(TxtCantidad.Text));
            BE_Revision.n_idunimed = Convert.ToInt32(CboUniMed.SelectedValue);
            BE_Revision.d_fchini = Convert.ToDateTime(TxtFchIni.Text);
            BE_Revision.d_fchfin = Convert.ToDateTime(TxtFchFin.Text);
            BE_Revision.h_horini = TxtHorIni.Text;
            BE_Revision.h_horfin = TxtHorFin.Text;
            BE_Revision.n_canprocon = Convert.ToDouble(funFunciones.NulosN(TxtCanProCon.Text));
            BE_Revision.n_canpronocon = Convert.ToDouble(funFunciones.NulosN(TxtCanProNoCon.Text));
            BE_Revision.c_obsprocon = TxtObsProCon.Text;
            BE_Revision.c_obspronocon = TxtObsProNoCon.Text;
            BE_Revision.n_idperrev = Convert.ToInt32(CboPerRes.SelectedValue);
            BE_Revision.d_fchrev = Convert.ToDateTime(TxtFchRev.Text);
            BE_Revision.h_horrev = TxtHorRev.Text;
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToDouble(TxtCanProCon.Text) == 0)
            {
                MessageBox.Show("¡ No ha especificado la cantidad de producto conforme !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtCanProCon.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToDouble(funFunciones.NulosN(TxtCanProNoCon.Text)) != 0)
            {
                if (TxtObsProNoCon.Text == "")
                {
                    MessageBox.Show("¡ No ha especificado la observacion de producto no conforme !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    TxtObsProNoCon.Focus();
                    booEstado = false;
                    return booEstado;
                }
            }

            if (Convert.ToInt32(CboPerRes.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre de la persona que realizo la revision del producto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboPerRes.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtFchRev.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de revision !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtCanProNoCon.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtHorRev.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la hora de revision !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtObsProNoCon.Focus();
                booEstado = false;
                return booEstado;
            }
            return booEstado;
        }
        private void FrmRevision_Activated(object sender, EventArgs e)
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
                objRegistro.mysConec = mysConec;
                dtMovimientos = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
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
            objFormVis = null;

            this.Close();
        }
        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (n_QueHace != 3) { return; }

            if (e.NewIndex == 1)
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
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
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
                dtResult = funDbGrid.DG_Filtrar(dtMovimientos, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
        }
        private void FrmRevision_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 110, this.Width - 38);
        }
        private void CmdBusParProd_Click(object sender, EventArgs e)
        {
            string[,] arrCabeceraDg1 = new string[6, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            string c_preflote = "";
            int n_Fila;
            int n_iditem;
            double n_valor;
            DataTable DtProdDet = new DataTable();
            CN_pro_produccion objProDet = new CN_pro_produccion();
            objProDet.mysConec = mysConec;
            objProDet.ProductosTerminados(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue));
            DtProdDet = objProDet.dtListar;

            arrCabeceraDg1[0, 0] = "Nº Parte Producion";
            arrCabeceraDg1[0, 1] = "120";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_numdoc";

            arrCabeceraDg1[1, 0] = "Fch. Produccion";
            arrCabeceraDg1[1, 1] = "80";
            arrCabeceraDg1[1, 2] = "F";
            arrCabeceraDg1[1, 3] = "d_fchpro";

            arrCabeceraDg1[2, 0] = "Producto";
            arrCabeceraDg1[2, 1] = "300";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "c_despro";

            arrCabeceraDg1[3, 0] = "Cantidad";
            arrCabeceraDg1[3, 1] = "80";
            arrCabeceraDg1[3, 2] = "N";
            arrCabeceraDg1[3, 3] = "n_canpro";

            arrCabeceraDg1[4, 0] = "Responsable";
            arrCabeceraDg1[4, 1] = "200";
            arrCabeceraDg1[4, 2] = "C";
            arrCabeceraDg1[4, 3] = "c_apenom";
            
            arrCabeceraDg1[5, 0] = "Id";
            arrCabeceraDg1[5, 1] = "0";
            arrCabeceraDg1[5, 2] = "C";
            arrCabeceraDg1[5, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "c_numdoc";
            xFun.Buscar_CadFiltro = "";
            dtResult = xFun.Buscar(arrCabeceraDg1, DtProdDet);

            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }
                     
            booAgregando = true;
            for (n_Fila = 0; n_Fila <= (dtResult.Rows.Count - 1); n_Fila++)
            {
                n_iditem = Convert.ToInt32(dtResult.Rows[n_Fila]["n_idpro"].ToString());
                LblIdProducion.Text = dtResult.Rows[n_Fila]["n_id"].ToString();

                CboReceta.SelectedValue = dtResult.Rows[n_Fila]["n_idrec"].ToString();
                CboProducto.SelectedValue = dtResult.Rows[n_Fila]["n_idpro"].ToString();  ///

                CboTipDoc.SelectedValue = dtResult.Rows[n_Fila]["n_idtipdoc"].ToString();
                TxtNumParteProd.Text = dtResult.Rows[n_Fila]["c_numdoc"].ToString(); ///
                n_valor = Convert.ToDouble(dtResult.Rows[n_Fila]["n_canprorea"].ToString());
                TxtCantidad.Text = n_valor.ToString("0.00");
                TxtNumLot.Text = dtResult.Rows[n_Fila]["c_numlot"].ToString();
                TxtHorIni.Text = Convert.ToDateTime(dtResult.Rows[n_Fila]["c_horini"]).ToString("HH:mm");
                TxtHorFin.Text = Convert.ToDateTime(dtResult.Rows[n_Fila]["c_horfin"]).ToString("HH:mm"); //dtResult.Rows[n_Fila]["horfin"].ToString();
                TxtFchIni.Text = dtResult.Rows[n_Fila]["d_fchpro"].ToString();
                TxtFchFin.Text = dtResult.Rows[n_Fila]["d_fchterpro"].ToString();

                // GENERAMOS EL NUMERO DE LOTE
                dtresulpre = funDatos.DataTableFiltrar(dtItems, "n_id = " + n_iditem + "");
                if (dtresulpre.Rows.Count == 0)
                {
                    MessageBox.Show("¡ No se encuentra el producto indicado, posiblemente haya sido eliminado o dado de baja  !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    TxtNumLot.Text = "";
                    return;
                }
                else 
                { 
                    c_preflote =funFunciones.NulosC(dtresulpre.Rows[0]["c_prelot"]);
                    TxtNumLot.Text = c_preflote + Convert.ToDateTime(dtResult.Rows[n_Fila]["d_fchterpro"]).ToString("ddMMyyyy");
                }
                // MOSTRAMOS LA PRESENTACION DEL ITEM
                dtresulpre = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_iditem + " AND n_default = 1");
                funDatos.ComboBoxCargarDataTable(CboUniMed, dtresulpre, "n_id", "c_despre");
                if (dtresulpre.Rows.Count != 0)
                {
                    CboUniMed.SelectedValue =Convert.ToInt32( dtresulpre.Rows[0]["n_id"]);
                }

                TxtFchRev.Text = dtResult.Rows[n_Fila]["d_fchterpro"].ToString();
                TxtCanProCon.Text = n_valor.ToString("0.00");
                TxtCanProNoCon.Text = "0";
            }
            booAgregando = false;
        }
        private void TxtCanProCon_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtCanProNoCon_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtHorRev_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void TxtNumParteProd_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtNumLot_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtCantidad_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtObsProCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //else
            //{
            //    if (!strCaracteres.Contains(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
            //}
        }
        private void TxtObsProNoCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            //else
            //{
            //    //if (!strCaracteres.Contains(e.KeyChar))
            //    //{
            //        e.Handled = true;
            //    //}
            //}
        }
        private void TxtCanProCon_Validated(object sender, EventArgs e)
        {
            if (TxtCanProCon.Text == "") { return; }

            double n_canprocon = Convert.ToDouble(TxtCanProCon.Text);
            double n_cantidad = Convert.ToDouble(TxtCantidad.Text);
            if (n_canprocon > n_cantidad)
            {
                MessageBox.Show(" La cantidad del producto conforme no puede ser mayor a la cantidad producida", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtCanProCon.Text = "0.00";
                return;
            }
            TxtCanProCon.Text = n_canprocon.ToString("0.00");
        }
        private void TxtCanProNoCon_Validated(object sender, EventArgs e)
        {
            if (TxtCanProNoCon.Text == "") { return; }

            double n_canprocon = Convert.ToDouble(TxtCanProCon.Text);
            if (n_canprocon == 0) 
            {
                MessageBox.Show(" Debe de indicar la cantidad de producto conforme ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return; 
            }

            double n_cantidad = Convert.ToDouble(TxtCantidad.Text);
            double n_canpronocon = Convert.ToDouble(TxtCanProNoCon.Text);
            if ((n_canprocon + n_canpronocon) > n_cantidad)
            {
                MessageBox.Show(" La cantidad del producto conforme + producto no conforme no puede ser mayor a la cantidad producida ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtCanProNoCon.Text = "0.00";
                return;
            }
            TxtCanProNoCon.Text = n_canpronocon.ToString("0.00");
        }
        private void TxtHorRev_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            objRegistro.mysConec = mysConec;
            dtMovimientos = objRegistro.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO);  // EL PARAMETROS N_IDTIPITEM = 0  MOSTRARA TODAS LAS SALIDAS
            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
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

        private void ToolImprimir_Click(object sender, EventArgs e)
        {

        }
        private void productosNoConformesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_pro_revision FunRev = new CN_pro_revision();
            FunRev.mysConec = mysConec;
            FunRev.STU_SISTEMA = STU_SISTEMA;
            FunRev.ReporteProdNoConforme(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO,2);
        }

        private void imprimirParteDeProduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_pro_revision FunRev = new CN_pro_revision();
            int n_registro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString()); 
            FunRev.mysConec = mysConec;
            FunRev.STU_SISTEMA = STU_SISTEMA;
            FunRev.ParteProduccion(n_registro);
        }

        private void productosConformesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CN_pro_revision FunRev = new CN_pro_revision();
            FunRev.mysConec = mysConec;
            FunRev.STU_SISTEMA = STU_SISTEMA;
            FunRev.ReporteProdNoConforme(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 1);
        }
    }
}
