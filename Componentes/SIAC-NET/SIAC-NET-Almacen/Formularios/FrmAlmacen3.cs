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
using DevComponents.DotNetBar;

namespace SIAC_NET_Almacen.Formularios
{
    public partial class FrmAlmacen3 : DevComponents.DotNetBar.Metro.MetroAppForm
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

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

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        DatosMySql funDatos = new DatosMySql();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_ALM_INVENTARIO_CONSULTA BE_Inventario = new BE_ALM_INVENTARIO_CONSULTA();

        // DATATABLE LOCALES
        DataTable dtUnidadMedida = new DataTable();
        DataTable dtMoneda = new DataTable();
        DataTable dtTipoExis = new DataTable();
        DataTable dtFamilia = new DataTable();
        DataTable dtClase = new DataTable();
        DataTable dtSubClase = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtForm = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[8, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[7, 5];
        string[,] arrCabeceraFlex2 = new string[2, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmAlmacen3()
        {
            InitializeComponent();
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
                objItems.mysConec = mysConec;
                dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID);
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
        void ConfigurarFormulario()
        {
            Tab_Dimensionar(Tab1, this.Height - 97, this.Width - 18);
            Tab_Posicionar(Tab1, 9, 74);

            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "Detalle Registro";

            arrCabeceraFlex1[0, 0] = "Unidad Medida";
            arrCabeceraFlex1[0, 1] = "50";
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_abrsun";

            arrCabeceraFlex1[1, 0] = "Descripcion";
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


            arrCabeceraFlex2[0, 0] = "Imagen";
            arrCabeceraFlex2[0, 1] = "250";
            arrCabeceraFlex2[0, 2] = "S";
            arrCabeceraFlex2[0, 3] = "";
            arrCabeceraFlex2[0, 4] = "c_abrsun";

            arrCabeceraFlex2[1, 0] = "IdImagen";
            arrCabeceraFlex2[1, 1] = "0";
            arrCabeceraFlex2[1, 2] = "S";
            arrCabeceraFlex2[1, 3] = "";
            arrCabeceraFlex2[1, 4] = "c_des";

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

            funFlex.FlexMostrarDatos(FgUniMed, arrCabeceraFlex1, BE_Inventario.lst_unidadmedida, 2, false);

            int n_fila = 2;
            foreach (BE_ALM_INVENTARIOUNIMED_CONSULTA element in BE_Inventario.lst_unidadmedida)
            {
                FgUniMed.Rows.Count = FgUniMed.Rows.Count + 1;
                FgUniMed.SetData(n_fila, 1, element.c_abrsun);
                FgUniMed.SetData(n_fila, 2, element.c_des);
                FgUniMed.SetData(n_fila, 3, element.c_desunimedbas);
                FgUniMed.SetData(n_fila, 4, element.n_canunimedbas);
                FgUniMed.SetData(n_fila, 5, element.n_default);
                FgUniMed.SetData(n_fila, 6, element.n_idunimed);
                FgUniMed.SetData(n_fila, 7, element.n_idunimedbas);

                FgUniMed.Cols[5].DataType = typeof(bool);
                n_fila++;
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt16(CboTipExis.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de existencia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
            BE_Inventario.n_idemp = STU_SISTEMA.EMPRESAID;
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

                    BE_UnidadMedida.c_dessun = FgUniMed.GetData(n_fila, 1).ToString();
                    BE_UnidadMedida.c_des = FgUniMed.GetData(n_fila, 2).ToString();
                    BE_UnidadMedida.c_desunimedbas = FgUniMed.GetData(n_fila, 3).ToString();
                    BE_UnidadMedida.n_canunimedbas = Convert.ToDouble(FgUniMed.GetData(n_fila, 4).ToString());
                    if (FgUniMed.GetData(n_fila, 5).ToString() == "False")
                    {
                        BE_UnidadMedida.n_default = 0;
                    }
                    else
                    {
                        BE_UnidadMedida.n_default = 1;
                    }

                    DataTable DtFiltro = new DataTable();
                    // FILTRAMOS LA UNIDAD DE MEDIDA POR LA ABREVIATURA SELECCIONADA PARA LA PRESENTACION 
                    string strCadenaFiltro = "c_abr = '" + FgUniMed.GetData(n_fila, 1).ToString() + "'";
                    DtFiltro = funDatos.DataTableFiltrar(dtUnidadMedida, strCadenaFiltro);
                    BE_UnidadMedida.n_idunimed = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                    // FILTRAMOS LA UNIDAD DE MEDIDA POR LA ABREVIATURA SELECCIONADA PARA LA UNIDAD DE MEDIDA BASICA
                    strCadenaFiltro = "c_abr = '" + FgUniMed.GetData(n_fila, 3).ToString() + "'";
                    DtFiltro = funDatos.DataTableFiltrar(dtUnidadMedida, strCadenaFiltro);
                    BE_UnidadMedida.n_idunimedbas = Convert.ToInt32(DtFiltro.Rows[0]["n_id"].ToString());

                    BE_UnidadMedida.n_idite = BE_Inventario.n_id;

                    LstUnidades.Add(BE_UnidadMedida);
                }
                BE_Inventario.lst_unidadmedida = LstUnidades;
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

            if (n_QueHace == 1)
            {
                booResultado = objItems.Insertar(BE_Inventario);
            }

            if (n_QueHace == 2)
            {
                booResultado = objItems.Actualizar(BE_Inventario);
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
            dtFamilia = objFamilia.Listar();                                // CARGAMOS TODAS LAS FAMILIAS

            objClase.mysConec = mysConec;
            dtClase = objClase.Listar();                                    // CARGAMOS TODAS LAS CLASES

            objSubClase.mysConec = mysConec;
            dtSubClase = objSubClase.Listar();                              // CARGAMOS TODAS LAS SUB CLASES

            objItems.mysConec = mysConec;
            dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID);

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

            CboTipExis.SelectedValue = 0;
            CboFamilia.SelectedValue = 0;
            CboClase.SelectedValue = 0;
            CboSubClase.SelectedValue = 0;
            CboMoneda.SelectedValue = 0;
        }
        void Bloquea()
        {
            TxtCodigo.Enabled = !TxtCodigo.Enabled;
            TxtDescripcion.Enabled = !TxtDescripcion.Enabled;
            TxtdescTecnica.Enabled = !TxtdescTecnica.Enabled;
            TxtCaracteristica.Enabled = !TxtCaracteristica.Enabled;
            TxtStockIni.Enabled = !TxtStockIni.Enabled;
            TxtStockActual.Enabled = !TxtStockActual.Enabled;
            TxtStockmMax.Enabled = !TxtStockmMax.Enabled;
            TxtStockMin.Enabled = !TxtStockMin.Enabled;
            TxtPreIni.Enabled = !TxtPreIni.Enabled;
            TxtPreAct.Enabled = !TxtPreAct.Enabled;

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
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
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
            TxtCodigo.Focus();
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
            booAgregando = false;
            TxtCodigo.Focus();
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
        private void FrmAlmacen2_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 91, this.Width - 18);
        }
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
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
                    dtItems = objItems.Listar(STU_SISTEMA.EMPRESAID);
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
            DgLista.Focus();
        }
        private void FgUniMed_RowColChange(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            funFlex.FlexColumnaCombo(FgUniMed, dtUnidadMedida, "c_abr", 1);
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
        }

        private void FrmAlmacen3_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 97, this.Width - 18);
        }
    }
}