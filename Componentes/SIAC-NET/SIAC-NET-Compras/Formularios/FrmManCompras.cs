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
using SIAC_Entidades.Compras;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Compras;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using MetroFramework.Forms;

namespace SIAC_NET_Compras.Formularios
{
    public partial class FrmManCompras : MetroForm
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
        CN_mae_condpago ObjConPag = new CN_mae_condpago();

        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_com_compras objMovimientos = new CN_com_compras();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_sun_unimed objUniMed = new CN_sun_unimed();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sun_tipmon objMoneda = new CN_sun_tipmon();
        CN_sun_tipcam objTC = new CN_sun_tipcam();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        DatosMySql funDatos = new DatosMySql();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_COM_COMPRAS_CONSULTA BE_Movimiento = new BE_COM_COMPRAS_CONSULTA();

        // DATATABLE LOCALES
        DataTable dtProveedor = new DataTable();
        DataTable dtTipoDocumento = new DataTable();
        DataTable dtTipoExis = new DataTable();
        DataTable dtAlmacenes = new DataTable();
        DataTable dtConPag = new DataTable();
        DataTable dtMovimientos = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtMoviDetalle = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtPresentaItem = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtMoneda = new DataTable();
        DataTable dtTC = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[11, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[15, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        double intTasaIGV = 18;

        public FrmManCompras()
        {
            InitializeComponent();
        }
        private void FrmManCompras_Load(object sender, EventArgs e)
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
            funDatos.ComboBoxCargarDataTable(CboCondPag, dtConPag, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMoneda, dtMoneda, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            Tab_Dimensionar(Tab1, this.Height - 110, this.Width - 38);
            Tab_Posicionar(Tab1, 20, 95);

            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "Detalle Registro";

            arrCabeceraFlex1[0, 0] = "Item";
            arrCabeceraFlex1[0, 1] = "300";
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_despro";

            arrCabeceraFlex1[1, 0] = "Presentacion";
            arrCabeceraFlex1[1, 1] = "120";
            arrCabeceraFlex1[1, 2] = "S";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_desprepro";

            arrCabeceraFlex1[2, 0] = "Cantidad";
            arrCabeceraFlex1[2, 1] = "70";
            arrCabeceraFlex1[2, 2] = "N";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "n_canpro";

            arrCabeceraFlex1[3, 0] = "Imp. Afecto";
            arrCabeceraFlex1[3, 1] = "100";
            arrCabeceraFlex1[3, 2] = "N";
            arrCabeceraFlex1[3, 3] = "0.000000";
            arrCabeceraFlex1[3, 4] = "n_preuni";

            arrCabeceraFlex1[4, 0] = "Imp. No Afecto";
            arrCabeceraFlex1[4, 1] = "100";
            arrCabeceraFlex1[4, 2] = "N";
            arrCabeceraFlex1[4, 3] = "0.000000";
            arrCabeceraFlex1[4, 4] = "n_preuni";

            arrCabeceraFlex1[5, 0] = "Total";
            arrCabeceraFlex1[5, 1] = "100";
            arrCabeceraFlex1[5, 2] = "N";
            arrCabeceraFlex1[5, 3] = "0.000000";
            arrCabeceraFlex1[5, 4] = "n_imptot";

            arrCabeceraFlex1[6, 0] = "Importe 1";
            arrCabeceraFlex1[6, 1] = "0";
            arrCabeceraFlex1[6, 2] = "N";
            arrCabeceraFlex1[6, 3] = "0.000000";
            arrCabeceraFlex1[6, 4] = "n_imptot";

            arrCabeceraFlex1[7, 0] = "Importe 2";
            arrCabeceraFlex1[7, 1] = "0";
            arrCabeceraFlex1[7, 2] = "N";
            arrCabeceraFlex1[7, 3] = "0.000000";
            arrCabeceraFlex1[7, 4] = "n_imptot";

            arrCabeceraFlex1[8, 0] = "Importe 3";
            arrCabeceraFlex1[8, 1] = "0";
            arrCabeceraFlex1[8, 2] = "N";
            arrCabeceraFlex1[8, 3] = "0.000000";
            arrCabeceraFlex1[8, 4] = "n_imptot";

            arrCabeceraFlex1[9, 0] = "IGV 1";
            arrCabeceraFlex1[9, 1] = "0";
            arrCabeceraFlex1[9, 2] = "N";
            arrCabeceraFlex1[9, 3] = "0.000000";
            arrCabeceraFlex1[9, 4] = "n_imptot";

            arrCabeceraFlex1[10, 0] = "IGV 2";
            arrCabeceraFlex1[10, 1] = "0";
            arrCabeceraFlex1[10, 2] = "N";
            arrCabeceraFlex1[10, 3] = "0.000000";
            arrCabeceraFlex1[10, 4] = "n_imptot";

            arrCabeceraFlex1[11, 0] = "IGV 3";
            arrCabeceraFlex1[11, 1] = "0";
            arrCabeceraFlex1[11, 2] = "N";
            arrCabeceraFlex1[11, 3] = "0.000000";
            arrCabeceraFlex1[11, 4] = "n_imptot";

            arrCabeceraFlex1[12, 0] = "TipoCompra";
            arrCabeceraFlex1[12, 1] = "0";
            arrCabeceraFlex1[12, 2] = "N";
            arrCabeceraFlex1[12, 3] = "0.000000";
            arrCabeceraFlex1[12, 4] = "n_imptot";

            arrCabeceraFlex1[13, 0] = "IdProducto";
            arrCabeceraFlex1[13, 1] = "0";
            arrCabeceraFlex1[13, 2] = "N";
            arrCabeceraFlex1[13, 3] = "0.000000";
            arrCabeceraFlex1[13, 4] = "n_imptot";
            
            arrCabeceraFlex1[14, 0] = "IdPresentacion";
            arrCabeceraFlex1[14, 1] = "0";
            arrCabeceraFlex1[14, 2] = "N";
            arrCabeceraFlex1[14, 3] = "0.000000";
            arrCabeceraFlex1[14, 4] = "n_imptot";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtMoviDetalle, 4, false);

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

            ObjConPag.mysConec = mysConec;
            dtConPag = ObjConPag.Listar();                          // 

            objItems.mysConec = mysConec;
            dtItems = objItems.ListarTabla(STU_SISTEMA.EMPRESAID);

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objMoneda.mysConec = mysConec;
            dtMoneda = objMoneda.Listar();

            objTC.mysConec = mysConec;
            dtTC = objTC.ListarTodo(2);                                     // LE INDICAMOS QUE DEBE DE TRAER EL TIPO DE CAMBIO PARA LA MONEDA DOLARES

            objMovimientos.mysConec = mysConec;
            dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);

            ObjAlmUniMed.mysConec = mysConec;
            dtPresentaItem = ObjAlmUniMed.Listar();

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(2);

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(4, ref arrCabeceraDg1);
        }
        private void metroLabel11_Click(object sender, EventArgs e)
        {

        }
        private void FrmManCompras_Activated(object sender, EventArgs e)
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
            objMovimientos.mysConec = mysConec;
            BE_Movimiento = objMovimientos.TraerRegistro(n_IdRegistro);

            CboProveedor.SelectedValue = BE_Movimiento.n_idpro;
            TxtFchEmi.Text = BE_Movimiento.f_fchdoc.ToString();
            CboMoneda.SelectedValue = BE_Movimiento.n_idmon;
            CboTipDoc.SelectedValue = BE_Movimiento.n_idtipdoc;
            TxtNumDoc.Text = BE_Movimiento.c_numdoc;
            TxtNumSer.Text = BE_Movimiento.c_numser;
            CboTipPro.SelectedValue = BE_Movimiento.n_idtippro;
            CboCondPag.SelectedValue = BE_Movimiento.n_idconpag;
            TxtFchVen.Text = BE_Movimiento.f_fchven.ToString();
            TxtGlosa.Text = BE_Movimiento.c_glosa;

            LblTipCam.Text = BE_Movimiento.n_tc.ToString("0.00");

            TxtImp1.Text = BE_Movimiento.n_impbru.ToString("0.00");
            TxtImp2.Text = BE_Movimiento.n_impbru2.ToString("0.00");
            TxtImp3.Text = BE_Movimiento.n_impbru3.ToString("0.00");
            TxtImp4.Text = BE_Movimiento.n_impinaf.ToString("0.00");

            TxtIgv1.Text = BE_Movimiento.n_impigv.ToString("0.00");
            TxtIgv2.Text = BE_Movimiento.n_impigv2.ToString("0.00");
            TxtIgv3.Text = BE_Movimiento.n_impigv3.ToString("0.00");
            TxtOtrCarg.Text = BE_Movimiento.n_impotr.ToString("0.00");
            TxtISC.Text = BE_Movimiento.n_impisc.ToString("0.00");
            TxtTotal.Text = BE_Movimiento.n_imptotcom.ToString("0.00");

            intTasaIGV = BE_Movimiento.n_tasaigv;

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, BE_Movimiento.lst_items, 2, false);

            int n_fila = 2;
            foreach (BE_COM_COMPRASDET_CONSULTA element in BE_Movimiento.lst_items)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;
                FgItems.SetData(n_fila, 1, element.c_despro);
                FgItems.SetData(n_fila, 2, element.c_desprepro);
                FgItems.SetData(n_fila, 3, element.n_canpro.ToString("0.00"));
                FgItems.SetData(n_fila, 4, element.n_preuni.ToString("0.000000"));
                FgItems.SetData(n_fila, 6, element.n_imptot.ToString("0.00"));

                FgItems.SetData(n_fila, 13, element.n_idtipcom.ToString());
                FgItems.SetData(n_fila, 14, element.n_iditem.ToString());
                FgItems.SetData(n_fila, 15, element.n_idunimed.ToString());
                n_fila++;
            }
        }
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            VerRegistro(intIdRegistro);
            booAgregando = false;
        }
        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (e.NewIndex == 1)
            {
                if (n_QueHace !=1)
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
        }
        private void FrmManCompras_Resize(object sender, EventArgs e)
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
            LblTipCam.Text = MostrarTC(DateTime.Now).ToString("0.000");
            CboProveedor.Focus();
        }     
        double MostrarTC(DateTime FechaConsulta)
        {    
            DataTable dtResul = new DataTable();
            Double intValor = 0;
            dtResul = funDatos.DataTableFiltrar(dtTC, "f_fecha = '" + FechaConsulta.ToString().Substring(0,10) + "'");
            if (dtResul.Rows.Count != 0)
            {
                intValor= Convert.ToDouble(dtResul.Rows[0]["n_preven"]);
            }

            return intValor;
        }
        void Blanquea()
        {
            TxtFchEmi.Text = "";
            TxtFchVen.Text = "";
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtGlosa.Text = "";
            LblTipCam.Text = "";

            TxtImp1.Text = "";
            TxtImp2.Text = "";
            TxtImp3.Text = "";
            TxtImp4.Text = "";

            TxtIgv1.Text = "";
            TxtIgv2.Text = "";
            TxtIgv3.Text = "";
            TxtOtrCarg.Text = "";
            TxtISC.Text = "";
            TxtTotal.Text = "";

            CboTipPro.SelectedValue = 0;
            CboTipDoc.SelectedValue = 0;
            CboProveedor.SelectedValue = 0;
            CboCondPag.SelectedValue = 0;
            CboMoneda.SelectedValue = 0;
        }
        void Bloquea()
        {
            TxtFchEmi.Enabled = !TxtFchEmi.Enabled;
            TxtFchVen.Enabled = !TxtFchVen.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtGlosa.Enabled = !TxtGlosa.Enabled;
            
            CboTipPro.Enabled = !CboTipPro.Enabled;
            CboTipDoc.Enabled = !CboTipDoc.Enabled;
            CboProveedor.Enabled = !CboProveedor.Enabled;
            CboCondPag.Enabled = !CboCondPag.Enabled;
            CboMoneda.Enabled = !CboMoneda.Enabled;
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
            CboProveedor.Focus();
            HallarTotales();
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
                    dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
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
                dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO, STU_SISTEMA.ANOTRABAJO);
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();

                DialogResult Rpta = MessageBox.Show("! El registro se agrego con exito ¡ ¿Desea agregar otro registro? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (n_QueHace == 1)
                {
                    if (DialogResult.Yes == Rpta)
                    {
                        Blanquea();
                        FgItems.Rows.Count = 2;
                        return;
                    }
                    else
                    {
                        Cancelar();
                    }
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
            //HallarTotales();
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
        void HallarTotales()
        {
            int intFila=2;
            for(intFila = 2; intFila <= FgItems.Rows.Count-1; intFila++)
            {
                HallarTotalItem(intFila);
            }
        }
        void AsignarEntidad()
        {
            if (n_QueHace == 1)
            {
                BE_Movimiento.n_id = 0;
            }
            else
            {
                BE_Movimiento.n_id = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            }
            
            BE_Movimiento.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_Movimiento.n_anotra = STU_SISTEMA.ANOTRABAJO;
            BE_Movimiento.n_idmes = STU_SISTEMA.MESTRABAJO;
            BE_Movimiento.n_idlib = 8;                          // LIBRO DE COMPRAS
            BE_Movimiento.c_numreg = "";
            BE_Movimiento.n_idtippro = Convert.ToInt32(CboTipPro.SelectedValue);
            BE_Movimiento.n_idpro = Convert.ToInt32(CboProveedor.SelectedValue);
            BE_Movimiento.n_idtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue); 
            BE_Movimiento.c_numser = TxtNumSer.Text;
            BE_Movimiento.c_numdoc = TxtNumDoc.Text;
            BE_Movimiento.f_fchdoc = Convert.ToDateTime(TxtFchEmi.Text);
            BE_Movimiento.f_fchreg = Convert.ToDateTime(TxtFchEmi.Text);
            BE_Movimiento.n_idconpag = Convert.ToInt32(CboCondPag.SelectedValue); 
            BE_Movimiento.f_fchven = Convert.ToDateTime(TxtFchVen.Text);
            BE_Movimiento.n_idmon = Convert.ToInt32(CboMoneda.SelectedValue);
            BE_Movimiento.n_impbru = Convert.ToDouble(TxtImp1.Text);
            BE_Movimiento.n_impbru2 = Convert.ToDouble(TxtImp2.Text);
            BE_Movimiento.n_impbru3 = Convert.ToDouble(TxtImp3.Text);
            BE_Movimiento.n_impinaf = Convert.ToDouble(TxtImp4.Text);
            BE_Movimiento.n_impigv = Convert.ToDouble(TxtIgv1.Text);
            BE_Movimiento.n_impigv2 = Convert.ToDouble(TxtIgv2.Text);
            BE_Movimiento.n_impigv3 = Convert.ToDouble(TxtIgv3.Text);
            BE_Movimiento.n_impisc = Convert.ToDouble(funFunciones.NulosN(TxtISC.Text));
            BE_Movimiento.n_impotr = Convert.ToDouble(funFunciones.NulosN(TxtOtrCarg.Text));
            BE_Movimiento.n_imptotcom = Convert.ToDouble(TxtTotal.Text);
            BE_Movimiento.n_tc = Convert.ToDouble(LblTipCam.Text);
            BE_Movimiento.n_impsal = Convert.ToDouble(TxtTotal.Text);
            BE_Movimiento.n_tasaigv = intTasaIGV;
            BE_Movimiento.c_glosa = TxtGlosa.Text;
            BE_Movimiento.n_estado = 1;
            BE_Movimiento.n_idtipdocref = 0;
            BE_Movimiento.n_iddocref = 0;

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

            List<BE_COM_COMPRASDET_CONSULTA> LstDetalle = new List<BE_COM_COMPRASDET_CONSULTA>();

            if (FgItems.Rows.Count > 2)
            {
                for (n_fila = 2; n_fila <= FgItems.Rows.Count - 1; n_fila++)
                {
                    BE_COM_COMPRASDET_CONSULTA BE_Detalle = new BE_COM_COMPRASDET_CONSULTA();

                    BE_Detalle.n_idcom = 0;
                    BE_Detalle.n_iditem = Convert.ToInt16(FgItems.GetData(n_fila, 14).ToString());
                    BE_Detalle.n_idunimed = Convert.ToInt16(FgItems.GetData(n_fila, 15).ToString());
                    BE_Detalle.n_canpro = Convert.ToDouble(FgItems.GetData(n_fila, 3).ToString());
                    BE_Detalle.n_preunibru = 0;
                    BE_Detalle.n_idpordsc = 0;
                    BE_Detalle.n_impdsc = 0;
                    BE_Detalle.n_preuni = Convert.ToDouble(FgItems.GetData(n_fila, 4).ToString());
                    BE_Detalle.n_imptot = Convert.ToDouble(FgItems.GetData(n_fila, 6).ToString()); 

                    DataTable DtFiltro = new DataTable();

                    LstDetalle.Add(BE_Detalle);
                }
                BE_Movimiento.lst_items = LstDetalle;
            }
            booAgregando = false;
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (LblTipCam.Text == "0.000")
            {
                MessageBox.Show("¡ No ha especificado el tipo de cambio para el dia " + TxtFchEmi.Text + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchEmi.Focus();
                return booEstado;
            }

            if (Convert.ToInt16(CboProveedor.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre del proveedor !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboProveedor.Focus();
                return booEstado;
            }

            if (TxtFchEmi.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de emision del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchEmi.Focus();
                return booEstado;
            }

            if (Convert.ToInt16(CboMoneda.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda para este documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMoneda.Focus();
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
                TxtNumDoc.Focus();
                return booEstado;
            }

            if (Convert.ToInt16(CboCondPag.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la condicion del pago para el documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboCondPag.Focus();
                return booEstado;
            }

            if (TxtFchVen.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de vencimiento del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchVen.Focus();
                return booEstado;
            }

            if (TxtGlosa.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la glosa para el documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtGlosa.Focus();
                return booEstado;
            }
          
            if (FgItems.Rows.Count != 2)
            {
                int intFila;
                // VERIFICAMOS QUE LOS DATOS DE LAS PRESENTACIONES ESTEN COMPLETAS
                for (intFila = 2; intFila <= FgItems.Rows.Count - 1; intFila++)
                {
                    if (funFunciones.NulosC(FgItems.GetData(intFila, 1)) == "")
                    {
                        MessageBox.Show("¡ No ha especificado la descripcion del item en la fila " + (FgItems.Rows.Count - 2).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        booEstado = false;
                        return booEstado;
                    }

                    if (funFunciones.NulosC(FgItems.GetData(intFila, 2)) == "")
                    {
                        MessageBox.Show("¡ No ha especificado la presentacion del item en la fila " + (FgItems.Rows.Count - 2).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        booEstado = false;
                        return booEstado;
                    }
                    if (funFunciones.NulosC(FgItems.GetData(intFila, 3)) == "")
                    {
                        MessageBox.Show("¡ No ha especificado la cantidad del item que ingresara en la fila " + (FgItems.Rows.Count - 2).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        booEstado = false;
                        return booEstado;
                    }
                    
                    if (funFunciones.NulosC(FgItems.GetData(intFila, 6)) == "")
                    {
                        MessageBox.Show("¡ No ha especificado el precio para el item ingresado " + (FgItems.Rows.Count - 2).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
            
            // OBTNEMOS LA DESCRIPCION DEL TIPO DE PRODUCTO
            int n_idtipproducto = 0;
            DataTable dtResul = new DataTable();

            n_idtipproducto = Convert.ToInt16(CboTipPro.SelectedValue);

            // FILTRAMOS LOS ITEMS DEL TIPO DE PRODUCTO SELECCIONADO
            dtResul = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + n_idtipproducto + "");
            funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 1);    // ITEMS
        }
        private void CmdAddItem_Click(object sender, EventArgs e)
        {
            bool booAgregarUnidad = true;

            if (Convert.ToInt16(CboTipPro.SelectedValue)==0)
            {
                MessageBox.Show("No ha especificado el tipo de item", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboTipPro.Focus();
                return;
            }

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

            if (FgItems.Col ==1)
            {
                // OBTNEMOS LA DESCRIPCION DEL TIPO DE PRODUCTO
                n_idtipproducto = Convert.ToInt16(CboTipPro.SelectedValue);
                
                dtResul = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + n_idtipproducto + "");
                funFlex.FlexColumnaCombo(FgItems, dtResul, "c_despro", 1);    // ITEMS
            }

            if (FgItems.Col == 2)
            {
                // OBTENEMOS LA DESCRIPCIO DEL ITEM
                strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 1));

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
                funFlex.FlexColumnaCombo(FgItems, dtResul, "c_des", 2);    // ITEMS
            }

            if ((FgItems.Col == 3) || (FgItems.Col == 4)|| (FgItems.Col == 5))
            {
                FgItems.AllowEditing = true;
            }

            if (FgItems.Col == 6)
            {
                FgItems.AllowEditing = false;
                return;
            }

            FgItems.AllowEditing = true;
        }
        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if ((FgItems.Col == 3) || (FgItems.Col == 4) || (FgItems.Col == 5))
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
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
        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            objMovimientos.mysConec = mysConec;
            dtMovimientos = objMovimientos.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboMeses.SelectedValue), STU_SISTEMA.ANOTRABAJO);

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

            objMoneda = null;
            dtMoneda = null;

            ObjConPag = null;
            dtConPag = null;

            objMovimientos = null;
            dtMovimientos = null;

            objFormVis = null;
            objFormVis = null;

            this.Close();
        }
        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            double intDatoNumerico = 0;
            //double intValorIGV = 0;
            DataTable dtResult = new DataTable();

            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }
            if (booAgregando == true) { return; }

            if (e.Col == 1)
            {
                FgItems.SetData(e.Row, 2, "");
                // OBTENEMOS LA DESCRIPCIO DEL ITEM
                string strDesTipPro = "";
                

                strDesTipPro = funFunciones.NulosC(FgItems.GetData(FgItems.Row, 1));

                if (strDesTipPro == "")
                {
                    FgItems.AllowEditing = false;
                    funFlex.FlexColumnaCombo(FgItems, dtResult, "c_despro", 2);
                    return;
                }

                // OBTENEMOS EL ID DEL TIPO DE PRODUCTO
                dtResult = funDatos.DataTableFiltrar(dtItems, "c_despro = '" + strDesTipPro + "'");

                FgItems.SetData(FgItems.Row, 13, dtResult.Rows[0]["n_idtipcom"].ToString());   // OBTENEMOS EL TIPO DE COMPRA
                FgItems.SetData(FgItems.Row, 14, dtResult.Rows[0]["n_id"].ToString());         // OBTENEMOS EL ID DEL ITEM
            }
            
            if (FgItems.Col == 2)
            {
                int n_idtipproducto = 0;

                n_idtipproducto = Convert.ToInt16(FgItems.GetData(FgItems.Row, 14));

                // FILTRAMOS LAS PRESENTACIONES DEL ITEM SELECCIONADO Y LA PRESENTACION
                dtResult = funDatos.DataTableFiltrar(dtPresentaItem, "n_idite = " + n_idtipproducto + " AND c_des ='" + FgItems.GetData(FgItems.Row, 2) + "'");
                if (dtResult.Rows.Count != 0)
                {
                    FgItems.SetData(FgItems.Row, 15, dtResult.Rows[0]["n_id"].ToString());
                }
            }

            if (FgItems.Col == 3)
            {
                intDatoNumerico = Convert.ToDouble(FgItems.GetData(e.Row, e.Col).ToString());

                FgItems.SetData(e.Row, e.Col, intDatoNumerico.ToString("0.000000"));
            }

            if ((FgItems.Col == 4)|| (FgItems.Col == 5))
            {
                intDatoNumerico = Convert.ToDouble(FgItems.GetData(e.Row, e.Col).ToString());

                FgItems.SetData(e.Row, e.Col, intDatoNumerico.ToString("0.00"));
            }

            HallarTotalItem(e.Row);
        }
        void HallarTotalItem(int intFila)
        {
            // HALLAMOS EL TOTAL DEL ITEM
            double intImpAfe = Convert.ToDouble(FgItems.GetData(intFila,4));
            double intImpNoAfe = Convert.ToDouble(FgItems.GetData(intFila, 5));
            double intCantidad = Convert.ToDouble(FgItems.GetData(intFila, 3));
            double intTotal = 0;

            intTotal = ((intCantidad * intImpAfe) + (intCantidad * intImpNoAfe));
            FgItems.SetData(intFila, 6, intTotal.ToString("0.00"));

            // DISTRIBUIMOS EL IMPORTE Y EL IGV DEL ITEM
            double intValorIGV = 0;

            if (funFunciones.NulosC(FgItems.GetData(intFila, 13)) != "")
            {
                intTotal = (intImpAfe * intCantidad);
                intValorIGV = (intTotal * (intTasaIGV / 100));

                if (FgItems.GetData(intFila, 13).ToString() == "1")
                {
                    FgItems.SetData(intFila, 7, intTotal.ToString("0.00"));
                    FgItems.SetData(intFila, 10, intValorIGV.ToString("0.00"));
                }
                if (FgItems.GetData(intFila, 13).ToString() == "2")
                {
                    FgItems.SetData(intFila, 8, intTotal.ToString("0.00"));
                    FgItems.SetData(intFila, 11, intValorIGV.ToString("0.00"));
                }
                if (FgItems.GetData(intFila, 13).ToString() == "3")
                {
                    FgItems.SetData(intFila, 9, intTotal.ToString("0.00"));
                    FgItems.SetData(intFila, 12, intValorIGV.ToString("0.00"));
                }
            }

            // MOSTRAMOS LOS TOTALES DEL DOCUMENTO
            double intTot1 = 0;
            double intTot2 = 0;
            double intTot3 = 0;
            double intTot4 = 0;

            double intIGV1 = 0;
            double intIGV2 = 0;
            double intIGV3 = 0;

            double intTotDoc = 0;

            intTot1 = funFlex.FlexSumarCol(FgItems, 7, 2, FgItems.Rows.Count - 1);
            intTot2 = funFlex.FlexSumarCol(FgItems, 8, 2, FgItems.Rows.Count - 1);
            intTot3 = funFlex.FlexSumarCol(FgItems, 9, 2, FgItems.Rows.Count - 1);
            intTot4 = funFlex.FlexSumarCol(FgItems, 5, 2, FgItems.Rows.Count - 1);

            intIGV1 = funFlex.FlexSumarCol(FgItems, 10, 2, FgItems.Rows.Count - 1);
            intIGV2 = funFlex.FlexSumarCol(FgItems, 11, 2, FgItems.Rows.Count - 1);
            intIGV3 = funFlex.FlexSumarCol(FgItems, 12, 2, FgItems.Rows.Count - 1);

            TxtImp1.Text = intTot1.ToString("0.00");
            TxtImp2.Text = intTot2.ToString("0.00");
            TxtImp3.Text = intTot3.ToString("0.00");
            TxtImp4.Text = intTot4.ToString("0.00");

            TxtIgv1.Text = intIGV1.ToString("0.00");
            TxtIgv2.Text = intIGV2.ToString("0.00");
            TxtIgv3.Text = intIGV3.ToString("0.00");

            intTotDoc = (intTot1 + intTot2 + intTot3 + intTot4);  // SUMAMOS LOS IMPORTES DEL DOCUMENTO
            intTotDoc = intTotDoc + (intIGV1 + intIGV2 + intIGV3);
           
            TxtTotal.Text = intTotDoc.ToString("0.00");
        }
        private void FgItems_KeyUpEdit(object sender, C1.Win.C1FlexGrid.KeyEditEventArgs e)
        {
        }
        private void TxtFchEmi_ValueChanged(object sender, EventArgs e)
        {
            LblTipCam.Text = MostrarTC(Convert.ToDateTime(TxtFchEmi.Text)).ToString("0.000");
        }
        private void TxtNumSer_Click(object sender, EventArgs e)
        {

        }
        private void TxtNumSer_TextChanged(object sender, EventArgs e)
        {

        }
        private void TxtNumSer_Leave(object sender, EventArgs e)
        {
            if (TxtNumSer.Text != "")
            {
                string strCad = "0000" + TxtNumSer.Text;

                TxtNumSer.Text = strCad.Substring(strCad.Length - 4, 4);
                //SendKeys.Send("{TAB}");
            }
        }
        private void TxtNumDoc_Leave(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text != "")
            {
                string strCad = "0000000000" + TxtNumDoc.Text;

                TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
                //SendKeys.Send("{TAB}");
            }
        }
        private void CboCondPag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            int intNumDias;
            DateTime datFechaEmision = Convert.ToDateTime(TxtFchEmi.Text);
            DataTable dtResult = new DataTable();

            dtResult = funDatos.DataTableFiltrar(dtConPag, "n_id = " + Convert.ToInt32(CboCondPag.SelectedValue) + "");

            if (dtResult.Rows.Count!=0)
            {
                intNumDias = Convert.ToInt32(dtResult.Rows[0]["n_numdia"].ToString());
                TxtFchVen.Text = funFunciones.FechaSumarDias(datFechaEmision, intNumDias).ToString("dd/MM/yyyy");
            }
        }
        private void CmdAddTC_Click(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            string FechaConsulta = TxtFchEmi.Text;
            // SI NO EXISTE CARGAMOS NUEVAMENTE EL DATATABLE DE TC
            dtTC = objTC.ListarTodo(2);

            LblTipCam.Text = MostrarTC(Convert.ToDateTime(TxtFchEmi.Text)).ToString("0.000");
        }
        private void CmdAddPro_Click(object sender, EventArgs e)
        {

        }
    }
}
