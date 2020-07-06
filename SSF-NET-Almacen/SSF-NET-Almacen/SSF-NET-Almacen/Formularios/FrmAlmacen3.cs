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

namespace SSF_NET_Almacen.Formularios
{
    public partial class FrmAlmacen3 : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Dedonde;                                                           // INDICA DESDE DONDE ESTA SIENDO INVOCADO EL FORMULARIO  (1 = MENU DEL SISTEMA;  2 = OTRO FORMULARIO)
        public int n_TipoExistencia;                                                    // INDICA EL AREA QUE ESTA INGRESANDO EL ITEM, ESTO ES PARA ASIGNAR EL TIPO DE ITEM QUE SE SELEECIONARA AUTOMATICAMENTE       
        public Int64 n_IdItemCreado;                                                      // INDICA EL ID DEL ITEM QUE SE HA CREADO, ESTE DATO SOLO SE DEVOLVERA CUANDO SE HAYA LLAMDO AL FORMULARIO DESDE OTRO FORMULARIO
        // OBJETOS LOCALES
        CN_sun_tipmon objMoneda = new CN_sun_tipmon();
        CN_alm_inventariounimed objAlmacenUniMed = new CN_alm_inventariounimed();
        CN_sun_tipexi objTipoExi = new CN_sun_tipexi();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_mae_familia objFamilia = new CN_mae_familia();
        CN_mae_clase objClase = new CN_mae_clase();
        CN_mae_subclase objSubClase = new CN_mae_subclase();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_sun_unimed objUniMed = new CN_sun_unimed();
        CN_alm_inventarioimagen objImagen = new CN_alm_inventarioimagen();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_ALM_INVENTARIO_CONSULTA BE_Inventario = new BE_ALM_INVENTARIO_CONSULTA();
        List<BE_ALM_INVENTARIOIMAGEN> lstInventarioImagen = new List<BE_ALM_INVENTARIOIMAGEN>();
        List<BE_ALM_INVENTARIOIMAGEN> lstImagen = new List<BE_ALM_INVENTARIOIMAGEN>();
       
        // DATATABLE LOCALES
        DataTable dtUnidadMedida = new DataTable();
        DataTable dtMoneda = new DataTable();
        DataTable dtTipoExis = new DataTable();
        DataTable dtFamilia = new DataTable();
        DataTable dtClase = new DataTable();
        DataTable dtSubClase = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtImagen = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        int n_MostrarActivos = 1;                                                       // INDICA SI SE MOSTRARAN LOS REGISTROS ACTIVOS
        string[,] arrCabeceraDg1 = new string[8, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[7, 5];
        string[,] arrCabeceraFlex2 = new string[3, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmAlmacen3()
        {
            InitializeComponent();
        }

        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void FrmAlmacen3_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
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
                objItems.mysConec = mysConec;
                dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, n_MostrarActivos);
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
                    if (n_Dedonde == 1)
                    {
                        Cancelar();
                    }
                    else
                    {
                        objUniMed = null;
                        dtUnidadMedida = null;

                        objMoneda = null;
                        dtMoneda = null;

                        objTipoExi = null;
                        dtTipoExis = null;

                        objFamilia = null;
                        dtFamilia = null;

                        objClase = null;
                        dtClase = null;

                        objSubClase = null;
                        dtSubClase = null;

                        objItems = null;
                        dtItems = null;

                        objFormVis = null;
                        objFormVis = null;

                        this.Hide();
                    }
                    
                }
            }
        }

        private void ToolCancelar_Click(object sender, EventArgs e)
        {
            if (n_Dedonde == 1)
            {
                Cancelar();
            }
            else
            {
                objUniMed = null;
                dtUnidadMedida = null;

                objMoneda = null;
                dtMoneda = null;

                objTipoExi = null;
                dtTipoExis = null;

                objFamilia = null;
                dtFamilia = null;

                objClase = null;
                dtClase = null;

                objSubClase = null;
                dtSubClase = null;

                objItems = null;
                dtItems = null;

                objFormVis = null;
                objFormVis = null;
                
                this.Hide();
            }
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            objUniMed = null;
            dtUnidadMedida = null;

            objMoneda = null;
            dtMoneda = null;

            objTipoExi = null;
            dtTipoExis = null;

            objFamilia = null;
            dtFamilia = null;

            objClase = null;
            dtClase = null;

            objSubClase = null;
            dtSubClase = null;

            objItems = null;
            dtItems = null;

            objFormVis = null;
            objFormVis = null;

            this.Close();
        }
        private void Tab1_SelectedIndexChanging(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;

            if (DgLista.RowCount == 0) { return; }

            if (tc.SelectedIndex == 1)
            {
                if (n_QueHace != 1)
                {
                    int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }
        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            //if (DgLista.RowCount == 0) { return; }
            //if (e.NewIndex == 1)
            //{
            //    if (n_QueHace != 1)
            //    {
            //        int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            //        booAgregando = true;
            //        VerRegistro(intIdRegistro);
            //        booAgregando = false;
            //    }
            //}
        }
        void ConfigurarFormulario()
        {
            this.Height = 613;
            this.Width = 980;

            //Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
            //Tab_Posicionar(Tab1, 1, 41);

            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "Detalle Registro";
            ChkVerActivos.Checked = true;

            arrCabeceraFlex1[0, 0] = "Pres. Abrv";
            arrCabeceraFlex1[0, 1] = "50";
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_abrsun";

            arrCabeceraFlex1[1, 0] = "Pres. Descripcion";
            arrCabeceraFlex1[1, 1] = "230";
            arrCabeceraFlex1[1, 2] = "S";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_des";

            arrCabeceraFlex1[2, 0] = "Uni. Med. basica";
            arrCabeceraFlex1[2, 1] = "70";
            arrCabeceraFlex1[2, 2] = "S";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_desunimedbas";

            arrCabeceraFlex1[3, 0] = "Can. Uni. Med. basica";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "N";
            arrCabeceraFlex1[3, 3] = "0.000000";
            arrCabeceraFlex1[3, 4] = "n_canunimedbas";

            arrCabeceraFlex1[4, 0] = "Uni. Med. Principal";
            arrCabeceraFlex1[4, 1] = "60";
            arrCabeceraFlex1[4, 2] = "N";
            arrCabeceraFlex1[4, 3] = "0";
            arrCabeceraFlex1[4, 4] = "n_default";

            arrCabeceraFlex1[5, 0] = "Id Uni. Med.";
            arrCabeceraFlex1[5, 1] = "0";
            arrCabeceraFlex1[5, 2] = "N";
            arrCabeceraFlex1[5, 3] = "0";
            arrCabeceraFlex1[5, 4] = "n_idunimed";

            arrCabeceraFlex1[6, 0] = "Id Uni. Med. Bas.";
            arrCabeceraFlex1[6, 1] = "0";
            arrCabeceraFlex1[6, 2] = "N";
            arrCabeceraFlex1[6, 3] = "0";
            arrCabeceraFlex1[6, 4] = "n_idunimedbas";


            arrCabeceraFlex2[0, 0] = "descripcion";
            arrCabeceraFlex2[0, 1] = "250";
            arrCabeceraFlex2[0, 2] = "C";
            arrCabeceraFlex2[0, 3] = "";
            arrCabeceraFlex2[0, 4] = "c_abrsun";

            arrCabeceraFlex2[1, 0] = "archivo";
            arrCabeceraFlex2[1, 1] = "0";
            arrCabeceraFlex2[1, 2] = "C";
            arrCabeceraFlex2[1, 3] = "";
            arrCabeceraFlex2[1, 4] = "c_des";

            arrCabeceraFlex2[2, 0] = "n_id";
            arrCabeceraFlex2[2, 1] = "0";
            arrCabeceraFlex2[2, 2] = "n";
            arrCabeceraFlex2[2, 3] = "";
            arrCabeceraFlex2[2, 4] = "c_des";

            funFlex.FlexMostrarDatos(FgUniMed, arrCabeceraFlex1, dtUnidadMedida, 2, false);
            funFlex.FlexMostrarDatos(FgImagen, arrCabeceraFlex2, dtUnidadMedida, 2, false);

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtItems.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtItems, true);
        }
        void VerRegistro(Int64 n_IdRegistro)
        {
            objItems.mysConec = mysConec;
            BE_Inventario = objItems.TraerRegistro(n_IdRegistro);
            objImagen.mysConec = mysConec;
            lstImagen = objImagen.Listar(BE_Inventario.n_id);

            TxtCodigo.Text = BE_Inventario.c_codpro;
            TxtDescripcion.Text = BE_Inventario.c_despro;
            TxtdescTecnica.Text = BE_Inventario.c_destec;
            TxtCaracteristica.Text = BE_Inventario.c_descar;
            TxtStockIni.Text = BE_Inventario.n_stkini.ToString("0.00");
            TxtStockActual.Text = BE_Inventario.n_stkact.ToString("0.00");
            TxtStockmMax.Text = BE_Inventario.n_stkmax.ToString("0.00");
            TxtStockMin.Text = BE_Inventario.n_stkmin.ToString("0.00");
            TxtPreIni.Text = BE_Inventario.n_preini.ToString("0.00");
            TxtPreAct.Text = BE_Inventario.n_preuni.ToString("0.00");

            CboTipExis.SelectedValue = BE_Inventario.n_idtipexi;
            CboFamilia.SelectedValue = BE_Inventario.n_idfam;
            CboClase.SelectedValue = BE_Inventario.n_idclas;
            CboSubClase.SelectedValue = BE_Inventario.n_idsubclas;
            CboMoneda.SelectedValue = BE_Inventario.n_idmon;
            TxtTiemVid.Text = BE_Inventario.n_numdiavid.ToString();
            TxtLotPref.Text = BE_Inventario.c_prelot.ToString();
            pictureBox1.Image = null;

            funFlex.FlexMostrarDatos(FgUniMed, arrCabeceraFlex1, BE_Inventario.lst_unidadmedida, 2, false);

            int n_fila = 2;
            foreach (BE_ALM_INVENTARIOUNIMED_CONSULTA element in BE_Inventario.lst_unidadmedida)
            {
                FgUniMed.Rows.Count = FgUniMed.Rows.Count + 1;
                FgUniMed.SetData(n_fila, 1, element.c_abrpre);
                FgUniMed.SetData(n_fila, 2, element.c_despre);
                FgUniMed.SetData(n_fila, 4, element.n_canunimedbas);
                FgUniMed.SetData(n_fila, 5, element.n_default);
                FgUniMed.SetData(n_fila, 6, element.n_id);
                FgUniMed.SetData(n_fila, 7, element.n_idunimedbas);

                DataTable DtFiltro = new DataTable();
                string strCadenaFiltro;
                strCadenaFiltro = "n_id = " + element.n_idunimedbas + "";
                DtFiltro = funDatos.DataTableFiltrar(dtUnidadMedida, strCadenaFiltro);
                FgUniMed.SetData(n_fila, 3, DtFiltro.Rows[0]["c_abr"].ToString());

                FgUniMed.Cols[5].DataType = typeof(bool);
                n_fila++;
            }

            // ESCRIBIMOS LAS IMAGENES GUARDADAS
            n_fila = 2;
            FgImagen.Rows.Count = 2;

            if (lstImagen.Count != 0)
            { 
                foreach (BE_ALM_INVENTARIOIMAGEN element in lstImagen)
                {
                    FgImagen.Rows.Count = FgImagen.Rows.Count + 1;
                    FgImagen.SetData(n_fila, 1, element.c_des);
                    FgImagen.SetData(n_fila, 2, STU_SISTEMA.RUTAFOTOITEMS + element.c_nomfil);
                    FgImagen.SetData(n_fila, 3, element.n_id);
                    //FgImagen.SetData(n_fila, 5, element.n_idite);            

                    n_fila++;
                }
                pictureBox1.Image = Image.FromFile(FgImagen.GetData(2,2).ToString());
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;
            if (ChkGenCod.Checked == true)
            {
                TxtCodigo.Text = GeneraCodigoProducto();
            }
            else
            {
                if (TxtCodigo.Text == "")
                {
                    MessageBox.Show("¡ No ha especificado el codigo del item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    return booEstado;
                }
            }

            if (Convert.ToInt16(CboTipExis.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboFamilia.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la familia para el item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboClase.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la clase para el item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboSubClase.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la subclase para el item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboMoneda.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda para el item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (TxtDescripcion.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la descripcion del item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (TxtCodigo.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el codigo del item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (FgUniMed.Rows.Count != 2)
            {
                int intFila;
                // VERIFICAMOS QUE LOS DATOS DE LAS PRESENTACIONES ESTEN COMPLETAS
                for (intFila = 2; intFila <= FgUniMed.Rows.Count - 1; intFila++)
                {
                    if (funFunciones.NulosC(FgUniMed.GetData(intFila, 1)) == "")
                    {
                        MessageBox.Show("¡ No ha especificado la unidad de medida de la presentacion en la fila " + (FgUniMed.Rows.Count - 2).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        booEstado = false;
                        return booEstado;
                    }
                    if (funFunciones.NulosC(FgUniMed.GetData(intFila, 2)) == "")
                    {
                        MessageBox.Show("¡ No ha especificado la descripcion de la presentacion en la fila " + (FgUniMed.Rows.Count - 2).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        booEstado = false;
                        return booEstado;
                    }

                    if (funFunciones.NulosC(FgUniMed.GetData(intFila, 3)) == "")
                    {
                        MessageBox.Show("¡ No ha especificado la unidad de medida base de la presentacion en la fila " + (FgUniMed.Rows.Count - 2).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        booEstado = false;
                        return booEstado;
                    }
                    if (funFunciones.NulosC(FgUniMed.GetData(intFila, 4)) == "")
                    {
                        MessageBox.Show("¡ No ha especificado la cantidad de la unidad de medida base de la presentacion en la fila " + (FgUniMed.Rows.Count - 2).ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        booEstado = false;
                        return booEstado;
                    }
                }

                bool booEligioPrincipal = false;

                // VERIFICAMOS SI SE HA ASIGNADO ALGUNA DE LAS PRESENTACIONES COMO PRINCIPAL
                for (intFila = 2; intFila <= FgUniMed.Rows.Count - 1; intFila++)
                {
                    if (Convert.ToInt16(FgUniMed.GetData(intFila, 5)) == 1)
                    {
                        booEligioPrincipal = true;
                    }
                }
                if (booEligioPrincipal == false)
                {
                    MessageBox.Show("¡ Debe de indicar cual sera la presentacion principal del item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    return booEstado;
                }
            }
            else
            {
                MessageBox.Show("¡ No ha especificado las presentaciones para el item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            return booEstado;
        }
        void AsignarEntidad()
        {
            BE_Inventario.n_idemp = 0;                                                           // POR DEFAULT LE ASIGNAMOS EL ID DE EMPRESA 0
            if (STU_SISTEMA.SYS_UNIBD == 0) { BE_Inventario.n_idemp = STU_SISTEMA.EMPRESAID; }   // SI EL MAESTRO DE INVENTARIOS NO ES UNIFICADO LE ASIGNAMOS EL ID DE LA EMPRESA

            BE_Inventario.c_codpro = TxtCodigo.Text;
            BE_Inventario.c_despro = TxtDescripcion.Text;
            BE_Inventario.c_destec = TxtdescTecnica.Text;
            BE_Inventario.c_descar = TxtCaracteristica.Text;

            BE_Inventario.n_stkini = Convert.ToDouble(funFunciones.NulosN(TxtStockIni.Text));
            BE_Inventario.n_stkact = Convert.ToDouble(funFunciones.NulosN(TxtStockActual.Text));
            BE_Inventario.n_stkmax = Convert.ToDouble(funFunciones.NulosN(TxtStockmMax.Text));
            BE_Inventario.n_stkmin = Convert.ToDouble(funFunciones.NulosN(TxtStockMin.Text));
            BE_Inventario.n_preini = Convert.ToDouble(funFunciones.NulosN(TxtPreIni.Text));
            BE_Inventario.n_preuni = Convert.ToDouble(funFunciones.NulosN(TxtPreAct.Text));

            BE_Inventario.n_idtipexi = Convert.ToInt16(CboTipExis.SelectedValue);
            BE_Inventario.n_idfam = Convert.ToInt16(CboFamilia.SelectedValue);
            BE_Inventario.n_idclas = Convert.ToInt16(CboClase.SelectedValue);
            BE_Inventario.n_idsubclas = Convert.ToInt16(CboSubClase.SelectedValue);
            BE_Inventario.n_idmon = Convert.ToInt16(CboMoneda.SelectedValue);
            BE_Inventario.n_numdiavid = Convert.ToInt16(funFunciones.NulosN(TxtTiemVid.Text));
            BE_Inventario.c_prelot = funFunciones.NulosC(TxtLotPref.Text);

            int n_fila = 0;
            int n_NumeroElementos = 0;

            if (BE_Inventario.lst_unidadmedida != null)
            {
                n_NumeroElementos = Convert.ToInt16(BE_Inventario.lst_unidadmedida.Count - 1);

                for (n_fila = 0; n_fila <= n_NumeroElementos; n_fila++)
                {
                    BE_Inventario.lst_unidadmedida.RemoveAt(0);
                }
            }

            n_fila = 0;
            booAgregando = true;

            List<BE_ALM_INVENTARIOUNIMED_CONSULTA> LstUnidades = new List<BE_ALM_INVENTARIOUNIMED_CONSULTA>();

            if (FgUniMed.Rows.Count > 2)
            {
                for (n_fila = 2; n_fila <= FgUniMed.Rows.Count - 1; n_fila++)
                {
                    BE_ALM_INVENTARIOUNIMED_CONSULTA BE_UnidadMedida = new BE_ALM_INVENTARIOUNIMED_CONSULTA();

                    BE_UnidadMedida.n_idite = 0;
                    BE_UnidadMedida.n_id = Convert.ToInt16(funFunciones.NulosN(FgUniMed.GetData(n_fila, 6)));
                    BE_UnidadMedida.c_despre = FgUniMed.GetData(n_fila, 2).ToString();
                    BE_UnidadMedida.c_abrpre = FgUniMed.GetData(n_fila, 1).ToString();

                    // FILTRAMOS LA UNIDAD DE MEDIDA POR LA ABREVIATURA SELECCIONADA PARA LA UNIDAD DE MEDIDA BASICA
                    DataTable DtFiltro = new DataTable();
                    string strCadenaFiltro;
                    strCadenaFiltro = "c_abr = '" + FgUniMed.GetData(n_fila, 3).ToString() + "'";
                    DtFiltro = funDatos.DataTableFiltrar(dtUnidadMedida, strCadenaFiltro);
                    BE_UnidadMedida.n_idunimedbas = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());
                    BE_UnidadMedida.n_canunimedbas = Convert.ToDouble(FgUniMed.GetData(n_fila, 4).ToString());

                    if (FgUniMed.GetData(n_fila, 5).ToString() == "False")
                    {
                        BE_UnidadMedida.n_default = 0;
                    }
                    else
                    {
                        BE_UnidadMedida.n_default = 1;
                    }
                    BE_UnidadMedida.n_preuni = 0;
                    BE_UnidadMedida.n_preuniigv = 0;

                    LstUnidades.Add(BE_UnidadMedida);
                }
                BE_Inventario.lst_unidadmedida = LstUnidades;
            }

            // CARGAMOS LAS IMAGENES
            if (FgImagen.Rows.Count > 2)
            {
                for (n_fila = 2; n_fila <= FgImagen.Rows.Count - 1; n_fila++)
                {
                    BE_ALM_INVENTARIOIMAGEN entInventarioImagen = new BE_ALM_INVENTARIOIMAGEN();
                    entInventarioImagen.n_idite = 0;
                    entInventarioImagen.n_id = Convert.ToInt16(funFunciones.NulosN(FgImagen.GetData(n_fila, 3)));
                    entInventarioImagen.c_des = FgImagen.GetData(n_fila, 1).ToString();
                    entInventarioImagen.c_nomfil = FgImagen.GetData(n_fila, 2).ToString();

                    lstInventarioImagen.Add(entInventarioImagen);
                }
            }
            booAgregando = false;
        }
        bool Grabar()
        {
            bool booResultado = false;
            if (CamposOK() == false)
            {
                return booResultado;
            }
            AsignarEntidad();
            objItems.STU_SISTEMA = STU_SISTEMA;
            if (n_QueHace == 1)
            {
                booResultado = objItems.Insertar(ref BE_Inventario);
                n_IdItemCreado = BE_Inventario.n_id;
            }

            if (n_QueHace == 2)
            {
                booResultado = objItems.Actualizar(BE_Inventario,lstInventarioImagen);
            }
            return booResultado;
        }
        void DataTableCargar()
        {
            objUniMed.mysConec = mysConec;
            dtUnidadMedida = objUniMed.Listar();

            objMoneda.mysConec = mysConec;
            dtMoneda = objMoneda.Listar();                                  // CARGAMOS TODAS MONEDAS

            objTipoExi.mysConec = mysConec;
            dtTipoExis = objTipoExi.Listar();                               //  CARGAMOS TODOS LOS TIPOS DE ITEM

            objFamilia.mysConec = mysConec;
            dtFamilia = objFamilia.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);                                // CARGAMOS TODAS LAS FAMILIAS

            objClase.mysConec = mysConec;
            dtClase = objClase.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);                                    // CARGAMOS TODAS LAS CLASES

            objSubClase.mysConec = mysConec;
            dtSubClase = objSubClase.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);                              // CARGAMOS TODAS LAS SUB CLASES

            objItems.mysConec = mysConec;
            dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, n_MostrarActivos);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(1);

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(1, ref arrCabeceraDg1);
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboTipExis, dtTipoExis, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboFamilia, dtFamilia, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboClase, dtClase, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboSubClase, dtSubClase, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMoneda, dtMoneda, "n_id", "c_des");

            CboMoneda.SelectedValue = STU_SISTEMA.MONEDA;
        }
        void Blanquea()
        {
            TxtCodigo.Text = "";
            TxtDescripcion.Text = "";
            TxtdescTecnica.Text = "";
            TxtCaracteristica.Text = "";
            TxtStockIni.Text = "";
            TxtStockActual.Text = "";
            TxtStockmMax.Text = "";
            TxtStockMin.Text = "";
            TxtPreIni.Text = "";
            TxtPreAct.Text = "";
            TxtTiemVid.Text = "";
            TxtLotPref.Text = "";

            CboTipExis.SelectedValue = 0;
            CboFamilia.SelectedValue = 0;
            CboClase.SelectedValue = 0;
            CboSubClase.SelectedValue = 0;
            CboMoneda.SelectedValue = 0;
        }
        void Bloquea()
        {
            //TxtCodigo.Enabled = !TxtCodigo.Enabled;
            TxtDescripcion.Enabled = !TxtDescripcion.Enabled;
            TxtdescTecnica.Enabled = !TxtdescTecnica.Enabled;
            TxtCaracteristica.Enabled = !TxtCaracteristica.Enabled;
            TxtStockIni.Enabled = !TxtStockIni.Enabled;
            TxtStockActual.Enabled = !TxtStockActual.Enabled;
            TxtStockmMax.Enabled = !TxtStockmMax.Enabled;
            TxtStockMin.Enabled = !TxtStockMin.Enabled;
            TxtPreIni.Enabled = !TxtPreIni.Enabled;
            TxtPreAct.Enabled = !TxtPreAct.Enabled;
            TxtTiemVid.Enabled = !TxtTiemVid.Enabled;
            TxtLotPref.Enabled = !TxtLotPref.Enabled;

            CboTipExis.Enabled = !CboTipExis.Enabled;
            CboFamilia.Enabled = false;
            CboClase.Enabled = false;
            CboSubClase.Enabled = false;
            CboMoneda.Enabled = !CboMoneda.Enabled;

            CmdAddUni.Enabled = !CmdAddUni.Enabled;
            CmdDelUni.Enabled = !CmdDelUni.Enabled;

            CmdAddImg.Enabled = !CmdAddImg.Enabled;
            CmdDelImg.Enabled = !CmdDelImg.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            //ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolGruMod.Enabled = !ToolGruMod.Enabled;
            //ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGruEli.Enabled = !ToolGruEli.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir.Enabled = !ToolImprimir.Enabled;
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

            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            CboTipExis.Focus();
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
            FgUniMed.Rows.Count = 2;
            ChkGenCod.Checked = true;
            booAgregando = false;
            
            CboTipExis.Focus();
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

        private void FrmAlmacen3_Resize(object sender, EventArgs e)
        {          
            //Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
        }

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            if (DgLista.RowCount == 0) { return; }
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            VerRegistro(intIdRegistro);
            booAgregando = false;
        }

        private void TxtCodigo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtdescTecnica_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtCaracteristica_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtStockIni_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtStockMin_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtPreIni_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtStockActual_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtStockmMax_KeyPress(object sender, KeyPressEventArgs e)
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
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objItems.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objItems.mysConec = mysConec;
                    dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, n_MostrarActivos);
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objItems.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            return booResult;
        }

        private void TxtPreAct_KeyPress(object sender, KeyPressEventArgs e)
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
        void Cancelar()
        {
            n_QueHace = 3;
            ActivarTool();
            Bloquea();
            LblTitulo2.Text = "Detalle del Registro";
            Tab1.TabPages[0].Enabled = true;
            Tab1.SelectedIndex = 0;
            TxtCodigo.Enabled = false;
            DgLista.Focus();
        }

        private void FgUniMed_RowColChange(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }
            //funFlex.FlexColumnaCombo(FgUniMed, dtUnidadMedida, "c_abr", 1);
            funFlex.FlexColumnaCombo(FgUniMed, dtUnidadMedida, "c_abr", 3);
        }

        private void CmdAddUni_Click(object sender, EventArgs e)
        {
            bool booAgregarUnidad = true;

            if (Convert.ToString(FgUniMed.GetData(FgUniMed.Rows.Count - 1, 1)) == "") booAgregarUnidad = false;
            if (Convert.ToString(FgUniMed.GetData(FgUniMed.Rows.Count - 1, 2)) == "") booAgregarUnidad = false;
            if (Convert.ToString(FgUniMed.GetData(FgUniMed.Rows.Count - 1, 3)) == "") booAgregarUnidad = false;
            if (Convert.ToString(FgUniMed.GetData(FgUniMed.Rows.Count - 1, 4)) == "") booAgregarUnidad = false;
            if (Convert.ToString(FgUniMed.GetData(FgUniMed.Rows.Count - 1, 5)) == "") booAgregarUnidad = false;

            if (booAgregarUnidad == true)
            {
                FgUniMed.Rows.Count = FgUniMed.Rows.Count + 1;
                FgUniMed.Cols[5].DataType = typeof(bool);
                FgUniMed.SetData(FgUniMed.Rows.Count - 1, 5, false);
                FgUniMed.Focus();
            }
            else
            {
                MessageBox.Show("No puede agregar mas unidades hasta que no haya completado los datos de la unidad anterior", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void CmdDelUni_Click(object sender, EventArgs e)
        {
            if (FgUniMed.Rows.Count > 2)
            {
                FgUniMed.RemoveItem(FgUniMed.Row);
                FgUniMed.Focus();
            }
        }

        private void FgUniMed_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (e.Col == 3)
            {
                if (FgUniMed.GetData(e.Row, 1).ToString() == FgUniMed.GetData(e.Row, 3).ToString())
                {
                    FgUniMed.SetData(e.Row, 3, "");
                    MessageBox.Show("La unidad basica no puede ser igual a la unidad de presentacion", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void FgUniMed_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 4)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void CboTipExis_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();
            string strCadenaFiltro = "n_idtipexi = " + CboTipExis.SelectedValue.ToString() + "";
            CboFamilia.Enabled = true;

            CboFamilia.SelectedValue = 0;
            CboClase.SelectedValue = 0;
            CboSubClase.SelectedValue = 0;

            DtFiltro = funDatos.DataTableFiltrar(dtFamilia, strCadenaFiltro);
            funDatos.ComboBoxCargarDataTable(CboFamilia, DtFiltro, "n_id", "c_des");
            
            CboFamilia.Enabled = true;
            CboClase.Enabled = false;
            CboSubClase.Enabled = false;
        }

        string GeneraCodigoProducto()
        {
            DataTable dtFiltrar = new DataTable();
            string c_CadenaFiltro;
            string c_preftipexi;
            string c_preffam;
            string c_prefcla;
            string c_prefsubcla;
            string c_numero;
            string c_codpro;

            c_numero = "";

            if (ChkGenCod.Checked == false)
            {
                c_codpro = "";
            }
            else
            {
                c_CadenaFiltro = "n_id = " + CboTipExis.SelectedValue.ToString() + "";
                dtFiltrar = funDatos.DataTableFiltrar(dtTipoExis, c_CadenaFiltro);
                c_preftipexi = dtFiltrar.Rows[0]["c_pre"].ToString();

                c_CadenaFiltro = "n_id = " + CboFamilia.SelectedValue.ToString() + "";
                dtFiltrar = funDatos.DataTableFiltrar(dtFamilia, c_CadenaFiltro);
                c_preffam = dtFiltrar.Rows[0]["c_pre"].ToString();

                c_CadenaFiltro = "n_id = " + CboClase.SelectedValue.ToString() + "";
                dtFiltrar = funDatos.DataTableFiltrar(dtClase, c_CadenaFiltro);
                c_prefcla = dtFiltrar.Rows[0]["c_pre"].ToString();

                c_CadenaFiltro = "n_id = " + CboSubClase.SelectedValue.ToString() + "";
                dtFiltrar = funDatos.DataTableFiltrar(dtSubClase, c_CadenaFiltro);
                c_prefsubcla = dtFiltrar.Rows[0]["c_pre"].ToString();

                dtFiltrar = objItems.ObtenerCodigo(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboTipExis.SelectedValue), Convert.ToInt16(CboFamilia.SelectedValue), Convert.ToInt16(CboClase.SelectedValue), Convert.ToInt16(CboSubClase.SelectedValue));

                if (dtFiltrar.Rows.Count != 0)
                {
                    if (Convert.ToInt16(dtFiltrar.Rows[0]["c_numite"]) != 0)
                    {
                        c_numero = "000" + (Convert.ToInt16(dtFiltrar.Rows[0]["c_numite"]) + 1).ToString();
                        c_numero = c_numero.Substring(c_numero.Length - 3, 3);
                    }
                    else
                    {
                        c_codpro = "001";
                    }
                }
                else
                {
                    c_numero = "001";
                    //c_codpro = "001";
                }

                //if (dtFiltrar.Rows[0]["c_numite"].ToString() != "")
                //{
                //    if (Convert.ToInt16(dtFiltrar.Rows[0]["c_numite"]) != 0)
                //    {
                //        c_numero = "000" + (Convert.ToInt16(dtFiltrar.Rows[0]["c_numite"]) + 1).ToString();
                //        c_numero = c_numero.Substring(c_numero.Length - 3, 3);
                //    }
                //    else
                //    {
                //        c_codpro = "001";
                //    }
                //}
                //else
                //{
                //    c_codpro = "001";
                //}
                c_codpro = c_preftipexi + c_preffam + c_prefcla + c_prefsubcla + c_numero;
            }

            return c_codpro;
        }

        private void CboFamilia_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();

            if (CboFamilia.SelectedValue != null)
            {
                string strCadenaFiltro = "n_idfam = " + CboFamilia.SelectedValue.ToString() + "";
                CboClase.Enabled = true;

                CboClase.SelectedValue = 0;
                CboSubClase.SelectedValue = 0;

                DtFiltro = funDatos.DataTableFiltrar(dtClase, strCadenaFiltro);
                funDatos.ComboBoxCargarDataTable(CboClase, DtFiltro, "n_id", "c_des");

                CboClase.Enabled = true;
                CboSubClase.Enabled = false;
            }
        }

        private void CboClase_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();

            if (CboClase.SelectedValue != null)
            {
                string strCadenaFiltro = "n_idcla = " + CboClase.SelectedValue.ToString() + "";
                CboSubClase.Enabled = true;

                CboSubClase.SelectedValue = 0;

                DtFiltro = funDatos.DataTableFiltrar(dtSubClase, strCadenaFiltro);
                funDatos.ComboBoxCargarDataTable(CboSubClase, DtFiltro, "n_id", "c_des");

                CboSubClase.Enabled = true;
            }
        }
        private void DgLista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DataTable dtResult = new DataTable();

                string c_CadFiltro = funDbGrid.DG_LeerCondicionesFiltro(DgLista);
                dtResult = funDbGrid.DG_Filtrar(dtItems, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
        }
        private void FrmAlmacen3_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (n_Dedonde == 1)
                {
                    if (dtItems.Rows.Count == 0)
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
                else
                {
                    ToolNuevo.Visible = false;
                    ToolModificar.Visible = false;
                    ToolEliminar.Visible = false;
                    toolStripSeparator1.Visible = false;
                    toolStripSeparator2.Visible = false;
                    ToolImprimir.Visible = false;
                    ToolSalir.Visible = false;
       
                    Nuevo();
                    //CboTipExis.Enabled = false;
                    MostrarDatos();
                    CboTipExis.Focus();
                }
            }    
        }
        void MostrarDatos()
        {
            CboTipExis.SelectedValue = n_TipoExistencia;
            CboMoneda.SelectedValue = 1;
            
            TxtStockIni.Text = "0.00";
            TxtStockActual.Text = "0.00";
            TxtStockMin.Text = "0.00";
            TxtStockmMax.Text = "0.00";
            TxtPreIni.Text = "0.00";
            TxtPreAct.Text = "0.00";
            FgUniMed.Rows.Count = FgUniMed.Rows.Count + 1;
            FgUniMed.SetData(2, 1, "UNI.");
            FgUniMed.SetData(2, 2, "UNIDAD");
            FgUniMed.SetData(2, 3, "NIU");
            FgUniMed.SetData(2, 4, "1");
            FgUniMed.Cols[5].DataType = typeof(bool);
            FgUniMed.SetData(2, 5, 1);
            FgUniMed.SetData(2, 6, 1);                                 // ID DE LA UNIDAD DEL PRODUCTO
            FgUniMed.SetData(2, 7, 58);                                // ID DE LA UNIDAD DE MEDIDA BASICA
        }
        private void FgUniMed_Click(object sender, EventArgs e)
        {

        }
        private void label20_Click(object sender, EventArgs e)
        {

        }
        private void TxtTiemVid_KeyPress(object sender, KeyPressEventArgs e)
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
        private void ChkGenCod_CheckedChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            if (ChkGenCod.Checked == false)     
            {
                TxtCodigo.Text = "";
                TxtCodigo.Enabled = true;
            }
            else
            {
                TxtCodigo.Text = "";
                TxtCodigo.Enabled = false;
                TxtCodigo.Text = GeneraCodigoProducto();
            }
        }
        private void TxtStockIni_TextChanged(object sender, EventArgs e)
        {

        }
        private void CmdAddImg_Click(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(FgImagen.GetData(1, 1)) == "") { return; }

            string c_nonarchivo = "";
            string c_rutanonarchivo = "";

            //Definimos los filtros de archivos a permitir, en este caso imagenes
            FDImagenes.Filter = "Bitmap files (*.bmp)|*.bmp|Gif files (*.gif)|*.gif|JGP files (*.jpg)|*.jpg|All (*.*)|*.* |PNG (*.patito)|*.png ";
            ///Establece que filtro se mostrará por deceto en este caso, 3=jpg
            FDImagenes.FilterIndex = 3;
            ///Esto aparece en el Nombre del archivo a seleccionar, se puede quitar no es fundamental
            FDImagenes.FileName = "Seleccione una imagen";

            //El titulo de la Ventana....
            FDImagenes.Title = "Imagenes de Items";

            //El directorio que por deceto habrirá, para cada contrapleca del Path colocar \\ C:\\Fotitos\\Wizard y así sucesivamente
            FDImagenes.InitialDirectory = "c:\\";

            /// Evalúa que si al aparecer el cuadro de dialogo la persona presionó Ok
            if (FDImagenes.ShowDialog() == DialogResult.OK)
            {
                /// Si esto se cumple, capturamos la propiedad File Name y la guardamos en la variable Garrobito
                c_nonarchivo = FDImagenes.SafeFileName;
                c_rutanonarchivo = FDImagenes.FileName;
                FgImagen.Rows.Count = FgImagen.Rows.Count + 1;

                FgImagen.SetData(FgImagen.Rows.Count - 1, 1, c_nonarchivo);
                FgImagen.SetData(FgImagen.Rows.Count - 1, 2, c_rutanonarchivo);
                //Por ultimo se la asignamos al PictureBox
                pictureBox1.Image = Image.FromFile(@c_rutanonarchivo);
                FgImagen.Select(FgImagen.Rows.Count - 1, 2);
            }
        }
        private void FgImagen_RowColChange(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            string c_rutanonarchivo = "";

            c_rutanonarchivo = funFunciones.NulosC(FgImagen.GetData(FgImagen.Row, 2));
            if (c_rutanonarchivo == "") { return; }
            if (funFunciones.ArchivoExiste(c_rutanonarchivo) == true)
            {
                pictureBox1.Image = Image.FromFile(c_rutanonarchivo);
            }
            else 
            {
                pictureBox1.Image = null;
            }
        }

        private void CmdDelImg_Click(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(FgImagen.GetData(1, 1)) == "") { return; }

            DialogResult Rpta = MessageBox.Show("¿ Esta seguro de eliminar la imagen seleccionada ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                FgImagen.RemoveItem(FgImagen.Row);
            }
        }

        private void FgUniMed_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3) 
            { 
                FgUniMed.AllowEditing = false; 
            }
            else
            { 
                FgUniMed.AllowEditing = true;
            }
        }

        private void ChkVerActivos_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkVerActivos.Checked == true)
            {
                n_MostrarActivos = 1;
            }
            else
            {
                n_MostrarActivos = 0;
            }

            objItems.mysConec = mysConec;
            dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, n_MostrarActivos);
            // MOSTRAMOS LOS DATOS EN LA GRILLA
            ListarItems();

        }

        private void modificarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modificar();
        }

        private void activarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivarRegistro(1);
        }
        void ActivarRegistro(int n_Estado)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            objItems.mysConec = mysConec;
            if (objItems.Activar(intIdRegistro, n_Estado) == false)
            {
                MessageBox.Show("No se pudo activar el registro por el siguiente motivo: " + objItems.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else 
            {
                objItems.mysConec = mysConec;
                dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, n_MostrarActivos);
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();
                MessageBox.Show("El registro se activo con exito","", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

        }
        private void eliminarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opcion no disponible", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //EliminarRegistro();
        }

        private void descativarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivarRegistro(0);
        }
    }
}
