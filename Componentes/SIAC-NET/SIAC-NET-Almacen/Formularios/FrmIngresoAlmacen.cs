using System;
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
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using MetroFramework.Forms;

namespace SIAC_NET_Almacen.Formularios
{
    public partial class FrmIngresoAlmacen : MetroForm
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sun_tipexi objTipoExi = new CN_sun_tipexi();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_mae_clipro ObjPro = new CN_mae_clipro();
        CN_mae_almacenes ObjAlm = new CN_mae_almacenes();
        CN_alm_inventariounimed ObjAlmUniMed = new CN_alm_inventariounimed();
        CN_alm_responsable ObjRes = new CN_alm_responsable();

        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_alm_movimientos objMovimientos = new CN_alm_movimientos();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_sun_unimed objUniMed = new CN_sun_unimed();
        CN_mae_meses objMeses = new CN_mae_meses();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        DatosMySql funDatos = new DatosMySql();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_ALM_MOVIMIENTOS_CONSULTA BE_Movimiento = new BE_ALM_MOVIMIENTOS_CONSULTA();

        // DATATABLE LOCALES
        DataTable dtProveedor = new DataTable();
        DataTable dtTipoDocumento = new DataTable();
        DataTable dtTipoExis = new DataTable();
        DataTable dtAlmacenes = new DataTable();
        DataTable dtResponsables = new DataTable();
        DataTable dtMovimientos = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtMoviDetalle = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtPresentaItem = new DataTable();
        DataTable dtMeses = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[7, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[4, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmIngresoAlmacen()
        {
            InitializeComponent();
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
        private void FrmIngresoAlmacen_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboTipPro, dtTipoExis, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtTipoDocumento, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboProveedor, dtProveedor, "n_id", "c_nombre");
            funDatos.ComboBoxCargarDataTable(CboAlmacen, dtAlmacenes, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboResponsable, dtResponsables, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            Tab_Dimensionar(Tab1, this.Height - 110, this.Width - 38);
            Tab_Posicionar(Tab1, 20, 95);

            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "Detalle Registro";

            arrCabeceraFlex1[0, 0] = "Tipo Item";
            arrCabeceraFlex1[0, 1] = "100";
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Item";
            arrCabeceraFlex1[1, 1] = "400";
            arrCabeceraFlex1[1, 2] = "S";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Presentacion";
            arrCabeceraFlex1[2, 1] = "170";
            arrCabeceraFlex1[2, 2] = "S";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_itepredes";

            arrCabeceraFlex1[3, 0] = "Cantidad";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "N";
            arrCabeceraFlex1[3, 3] = "0.000000";
            arrCabeceraFlex1[3, 4] = "n_can";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 2, false);
            
            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        void DataTableCargar()
        {
            ObjPro.mysConec = mysConec;
            dtProveedor = ObjPro.ListarProveedor();                                         //

            objTipDoc.mysConec = mysConec;
            dtTipoDocumento = objTipDoc.Listar();                                           // 

            objTipoExi.mysConec = mysConec;
            dtTipoExis = objTipoExi.Listar();                                               //  

            ObjAlm.mysConec = mysConec;
            dtAlmacenes = ObjAlm.Listar(STU_SISTEMA.EMPRESAID);                             // 

            ObjRes.mysConec = mysConec;
            dtResponsables = ObjRes.Listar(STU_SISTEMA.EMPRESAID);                          // 

            objItems.mysConec = mysConec;
            dtItems = objItems.ListarTabla(STU_SISTEMA.EMPRESAID);

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objMovimientos.mysConec = mysConec;
            dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO, 1);

            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(2);

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(2, ref arrCabeceraDg1);
        }
        private void FrmIngresoAlmacen_Activated(object sender, EventArgs e)
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
        private void DgLista_Click(object sender, EventArgs e)
        {

        }
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            VerRegistro(intIdRegistro);
            booAgregando = false;
        }
        void VerRegistro(Int64 n_IdRegistro)
        {
            objMovimientos.mysConec = mysConec;
            BE_Movimiento = objMovimientos.TraerRegistro(n_IdRegistro);

            txtFchIng.Text = BE_Movimiento.d_fching.ToString();
            TxtFchDoc.Text = BE_Movimiento.d_fchdoc.ToString();
            CboTipDoc.SelectedValue = BE_Movimiento.n_idtipdoc;
            TxtNumDoc.Text = BE_Movimiento.c_numdoc;
            TxtNumSer.Text = BE_Movimiento.c_numser;
            CboProveedor.SelectedValue = BE_Movimiento.n_idclipro;
            CboAlmacen.SelectedValue = BE_Movimiento.n_idalm;
            TxtObs.Text = BE_Movimiento.c_obs;

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, BE_Movimiento.lst_items, 2, false);

            int n_fila = 2;
            foreach (BE_ALM_MOVIMIENTOSDET_CONSULTA element in BE_Movimiento.lst_items)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;
                FgItems.SetData(n_fila, 1, element.c_tipexides);
                FgItems.SetData(n_fila, 2, element.c_itedes);
                FgItems.SetData(n_fila, 3, element.c_itepredes);
                FgItems.SetData(n_fila, 4, element.n_can);

                n_fila++;
            }
        }
        private void Tab1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }
        private void FrmIngresoAlmacen_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 110, this.Width - 38);
        }
        private void ToolNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
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
            FgItems.Rows.Count = 2;
            booAgregando = false;
            txtFchIng.Focus();
        }
        void Blanquea()
        {
            txtFchIng.Text = "";
            TxtFchDoc.Text = "";
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtObs.Text = "";

            CboTipPro.SelectedValue = 0;
            CboTipDoc.SelectedValue = 0;
            CboProveedor.SelectedValue = 0;
            CboAlmacen.SelectedValue = 0;
            CboResponsable.SelectedValue = 0;
        }
        void Bloquea()
        {
            txtFchIng.Enabled = !txtFchIng.Enabled;
            TxtFchDoc.Enabled = !TxtFchDoc.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;

            CboTipPro.Enabled = !CboTipPro.Enabled;
            CboTipDoc.Enabled = !CboTipDoc.Enabled;
            CboProveedor.Enabled = !CboProveedor.Enabled;
            CboAlmacen.Enabled = !CboAlmacen.Enabled;
            CboResponsable.Enabled = !CboResponsable.Enabled;

            CmdAddItem.Enabled = !CmdAddItem.Enabled;
            CmdDelItem.Enabled = !CmdDelItem.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir.Enabled = !ToolImprimir.Enabled;
            ToolExportar.Enabled = !ToolExportar.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
        }
        private void ToolModificar_Click(object sender, EventArgs e)
        {
            Modificar();
        }
        void Modificar()
        {
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
            txtFchIng.Focus();
            booAgregando = false;
        }
        private void ToolEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objMovimientos.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objMovimientos.mysConec = mysConec;
                    dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO,1);
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objMovimientos.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            return booResult;
        }
        private void ToolCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
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
        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            if (Grabar() == true)
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objMovimientos.mysConec = mysConec;
                dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO,1);
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
                booResultado = objMovimientos.Insertar(BE_Movimiento);
            }

            if (n_QueHace == 2)
            {
                booResultado = objMovimientos.Actualizar(BE_Movimiento);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {
            //BE_Movimiento.lst_items;

            //BE_Movimiento.n_id;
            BE_Movimiento.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_Movimiento.n_idtipmov = 1;                            // 1 = INGRESO ; 2 = SALIDA
            BE_Movimiento.n_idclipro = Convert.ToInt16(CboProveedor.SelectedValue);
            BE_Movimiento.d_fchdoc = Convert.ToDateTime(TxtFchDoc.Text);
            BE_Movimiento.d_fching = Convert.ToDateTime(txtFchIng.Text);
            BE_Movimiento.n_idtipdoc = Convert.ToInt16(CboTipDoc.SelectedValue);
            BE_Movimiento.c_numser = TxtNumSer.Text;
            BE_Movimiento.c_numdoc = TxtNumDoc.Text;
            BE_Movimiento.n_idalm = Convert.ToInt16(CboAlmacen.SelectedValue);
            BE_Movimiento.n_anotra = STU_SISTEMA.ANOTRABAJO;
            BE_Movimiento.n_idmes = STU_SISTEMA.MESTRABAJO;
            BE_Movimiento.c_obs = TxtObs.Text; ;

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

            if (FgItems.Rows.Count > 2)
            {
                for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
                {
                    BE_ALM_MOVIMIENTOSDET_CONSULTA BE_Detalle = new BE_ALM_MOVIMIENTOSDET_CONSULTA();

                    BE_Detalle.n_idmov = 0;
                    BE_Detalle.n_can = Convert.ToDouble(FgItems.GetData(n_fila, 4).ToString()); 

                    BE_Detalle.c_tipexides = FgItems.GetData(n_fila, 1).ToString();
                    BE_Detalle.c_itedes = FgItems.GetData(n_fila, 2).ToString(); 
                    BE_Detalle.c_itepredes = FgItems.GetData(n_fila, 3).ToString();
                    
                    DataTable DtFiltro = new DataTable();
                    
                    // FILTRAMOS EL ITEM DE LA FILA PARA OBTENER EL ID  
                    string strCadenaFiltro = "c_despro = '" + BE_Detalle.c_itedes + "'";
                    DtFiltro = funDatos.DataTableFiltrar(dtItems, strCadenaFiltro);
                    BE_Detalle.n_idite = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                    // FILTRAMOS LA PRESENTACION PARA OBTENER SU ID
                    strCadenaFiltro = "c_des = '" + BE_Detalle.c_itepredes + "' AND n_idite = " + BE_Detalle.n_idite + "";
                    DtFiltro = funDatos.DataTableFiltrar(dtPresentaItem, strCadenaFiltro);
                    BE_Detalle.n_idpre = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                    // FILTRAMOS EL TIPO DE PRODUCTO PARA OBTENER SU id
                    strCadenaFiltro = "c_des = '" + BE_Detalle.c_tipexides + "'";
                    DtFiltro = funDatos.DataTableFiltrar(dtTipoExis, strCadenaFiltro);
                    BE_Detalle.n_idtippro = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                    LstDetalle.Add(BE_Detalle);
                }
                BE_Movimiento.lst_items = LstDetalle;
            }
            booAgregando = false;
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (txtFchIng.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de ingreso !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (TxtFchDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            //if (Convert.ToInt16(CboTipPro.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado el tipo de existencia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    return booEstado;
            //}

            if (Convert.ToInt16(CboTipDoc.SelectedValue) == 0)
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

            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboProveedor.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre del proveedor !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboAlmacen.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el almacen de destino !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            //if (Convert.ToInt16(CboResponsable.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado el nombre del responsable !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    return booEstado;
            //}

            if (FgItems.Rows.Count != 2)
            {
                int intFila;
                // VERIFICAMOS QUE LOS DATOS DE LAS PRESENTACIONES ESTEN COMPLETAS
                for (intFila = 2; intFila <= FgItems.Rows.Count - 1; intFila++)
                {
                    if (funFunciones.NulosC(FgItems.GetData(intFila, 2)) == "")
                    {
                        MessageBox.Show("¡ No ha especificado la descripcion del item en la fila " + (FgItems.Rows.Count - 2).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        booEstado = false;
                        return booEstado;
                    }

                    if (funFunciones.NulosC(FgItems.GetData(intFila, 3)) == "")
                    {
                        MessageBox.Show("¡ No ha especificado la presentacion del item en la fila " + (FgItems.Rows.Count - 2).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        booEstado = false;
                        return booEstado;
                    }
                    if (funFunciones.NulosC(FgItems.GetData(intFila, 4)) == "")
                    {
                        MessageBox.Show("¡ No ha especificado la cantidad del item que ingresara en la fila " + (FgItems.Rows.Count - 2).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
        private void FgItems_RowColChange(object sender, EventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }
            
            funFlex.FlexColumnaCombo(FgItems, dtTipoExis, "c_des", 1);    // TIPO DE PRODUCTO       
        }
        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
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
                //FgItems.Cols[5].DataType = typeof(bool);
                //FgItems.SetData(FgItems.Rows.Count - 1, 5, false);
                FgItems.Focus();
            }
            else
            {
                MessageBox.Show("No puede agregar mas items hasta que no haya completado el item anterior", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        private void CmdDelItem_Click(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count > 2)
            {
                FgItems.RemoveItem(FgItems.Row);
                FgItems.Focus();
            }
        }
        private void FgItems_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3) 
            {    
                FgItems.AllowEditing = false; return;
            }
            if (booAgregando == true) { return; }
            
            DataTable dtResul = new DataTable();
            int n_idtipproducto = 0;
            string strDesTipPro = "";

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
                n_idtipproducto = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());

                // FILTRAMOS LOS ITEMS DEL TIPO DE PRODUCTO SELECCIONADO
                dtResul = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + n_idtipproducto + "");
                funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 2);    // ITEMS
                
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
                n_idtipproducto = Convert.ToInt16(dtResul.Rows[0]["n_id"].ToString());

                // FILTRAMOS LAS PRESENTACIONES DEL ITEM SELECCIONADO
                dtResul = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_idtipproducto + "");
                funFlex.FlexColumnaCombo(FgItems, dtResul, "c_des", 3);    // ITEMS
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
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }
        private void FgItems_Click(object sender, EventArgs e)
        {
        }
        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 4)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtNumSer_TextChanged(object sender, EventArgs e)
        {

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
        private void TxtNumDoc_TextChanged(object sender, EventArgs e)
        {

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

        private void ToolImprimir_Click(object sender, EventArgs e)
        {

        }

        private void ToolExportar_Click(object sender, EventArgs e)
        {

        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            objMovimientos.mysConec = mysConec;
            dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO, 1);

            ListarItems();

            if (dtMovimientos.Rows.Count == 0)
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

        private void CboMeses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
