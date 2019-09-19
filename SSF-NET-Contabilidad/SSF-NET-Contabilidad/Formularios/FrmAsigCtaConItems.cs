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
using SIAC_Negocio.Contabilidad;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using SIAC_Entidades.Contabilidad;

namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmAsigCtaConItems : Form
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
        CN_con_pcitems objItems = new CN_con_pcitems();
        CN_alm_inventario o_Inventario = new CN_alm_inventario();
        CN_sun_unimed objUniMed = new CN_sun_unimed();
        CN_alm_inventarioimagen objImagen = new CN_alm_inventarioimagen();
        CN_con_pc objPlaCue = new CN_con_pc();
        CN_con_pcitems objPcItem = new CN_con_pcitems();
        CN_con_percepcion o_per = new CN_con_percepcion();
        CN_con_retencion o_ret = new CN_con_retencion();
        CN_con_detraccion o_det = new CN_con_detraccion();
        CN_sun_fe_catalogo07 o_cat7 = new CN_sun_fe_catalogo07();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_CON_PCITEMS e_Items = new BE_CON_PCITEMS();
        //List<BE_ALM_INVENTARIOIMAGEN> lstInventarioImagen = new List<BE_ALM_INVENTARIOIMAGEN>();
        //List<BE_ALM_INVENTARIOIMAGEN> lstImagen = new List<BE_ALM_INVENTARIOIMAGEN>();

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
        DataTable dtPlaCue = new DataTable();
        DataTable dtInventario = new DataTable();
        DataTable dtRet = new DataTable();
        DataTable dtPer = new DataTable();
        DataTable dtDet = new DataTable();
        DataTable dtigvcon = new DataTable();
        DataTable dtigvven = new DataTable();

        // VARIABLES LOCALES
        int n_Tipo = 2;                                                                 // INDICA QUE SE MOSTRARAN TODOS LOS ITEMS CON CUENTA CONTABLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        int n_MostrarActivos = 1;                                                       // INDICA SI SE MOSTRARAN LOS REGISTROS ACTIVOS
        string[,] arrCabeceraDg1 = new string[10, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[7, 5];
        string[,] arrCabeceraFlex2 = new string[3, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmAsigCtaConItems()
        {
            InitializeComponent();
        }

        private void FrmAsigCtaConItems_Load(object sender, EventArgs e)
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

        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            if (Grabar() == true)
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objItems.mysConec = mysConec;
                objItems.Listar(STU_SISTEMA.EMPRESAID, n_Tipo);
                dtItems = objItems.dtLista;
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
        void CerrarObjetos()
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
        }
        private void ToolCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }
        private void ToolSalir_Click(object sender, EventArgs e)
        {
            CerrarObjetos();
            this.Close();
        }
        private void c1DockingTabPage1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (e.NewIndex == 1)
            {
                if (n_QueHace != 1)
                {
                    int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }
        void ConfigurarFormulario()
        {
            this.Height = 613;
            this.Width = 980;

            Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 41);

            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "Detalle Registro";
            //ChkVerActivos.Checked = true;
            Tab1.SelectedIndex = 0;
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

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtItems.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtItems, true);
        }
        void VerRegistro(int n_IdRegistro)
        {
            Blanquea();
            DataTable dtresul = new DataTable();
            objItems.mysConec = mysConec;
            //e_Items = new BE_CON_PCITEMS();
            objItems.TraerRegistro(n_IdRegistro, STU_SISTEMA.EMPRESAID);
            e_Items = objItems.e_Items;

            dtresul = funDatos.DataTableFiltrar(dtInventario, "n_id = " + n_IdRegistro.ToString() + "");
            //dtresul = funDatos.DataTableFiltrar(dtItems, "n_iditem = " + n_IdRegistro.ToString() + "");
            if (dtresul.Rows.Count != 0)
            { 
                CboClase.SelectedValue = Convert.ToInt32(dtresul.Rows[0]["n_idclas"]);
                CboFamilia.SelectedValue = Convert.ToInt32(dtresul.Rows[0]["n_idfam"]);
                CboTipExis.SelectedValue = Convert.ToInt32(dtresul.Rows[0]["n_idtipexi"]);
                CboSubClase.SelectedValue = Convert.ToInt32(dtresul.Rows[0]["n_idsubclas"]);
                TxtCodigo.Text = dtresul.Rows[0]["c_codpro"].ToString();
                TxtDescripcion.Text = dtresul.Rows[0]["c_despro"].ToString();
                LblIdItem.Text = dtresul.Rows[0]["n_id"].ToString();
                //LblIdItem.Text = dtresul.Rows[0]["n_iditem"].ToString();
            }
            if (e_Items != null)
            { 
                dtresul = funDatos.DataTableFiltrar(dtPlaCue, "n_id = " + e_Items.n_idpc + "");
                if (dtresul.Rows.Count != 0)
                { 
                    TxtCtaCom.Text =dtresul.Rows[0]["c_cuecon"].ToString();
                    TxtCtaDesCom.Text=dtresul.Rows[0]["c_des"].ToString();
                    LblIdCtaCom.Text = dtresul.Rows[0]["n_id"].ToString();
                }

                dtresul = funDatos.DataTableFiltrar(dtPlaCue, "n_id = " + e_Items.n_idpcven + "");
                if (dtresul.Rows.Count != 0)
                {
                    TxtCtaVen.Text = dtresul.Rows[0]["c_cuecon"].ToString();
                    TxtCtaDesVen.Text = dtresul.Rows[0]["c_des"].ToString();
                    LblIdCtaVen.Text = dtresul.Rows[0]["n_id"].ToString();
                }

                CboDet.SelectedValue = e_Items.n_iddet;
                CboRet.SelectedValue = e_Items.n_idret;
                CboPer.SelectedValue = e_Items.n_idper;
                CboIsc.SelectedValue = e_Items.n_idisc;
                CboIgvCom.SelectedValue = e_Items.n_idigvcom;
                CboIgvVen.SelectedValue = e_Items.n_idigvven;
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt16(CboTipExis.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            //if (Convert.ToInt16(CboFamilia.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado la familia para el item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    return booEstado;
            //}

            //if (Convert.ToInt16(CboClase.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado la clase para el item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    return booEstado;
            //}

            //if (Convert.ToInt16(CboSubClase.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado la subclase para el item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    return booEstado;
            //}


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

            return booEstado;
        }
        void AsignarEntidad()
        {
            e_Items = new BE_CON_PCITEMS();
            e_Items.n_idemp = STU_SISTEMA.EMPRESAID;                                                           // POR DEFAULT LE ASIGNAMOS EL ID DE EMPRESA 0
            if (STU_SISTEMA.SYS_UNIBD == 0) { e_Items.n_idemp = STU_SISTEMA.EMPRESAID; }   // SI EL MAESTRO DE INVENTARIOS NO ES UNIFICADO LE ASIGNAMOS EL ID DE LA EMPRESA

            e_Items.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Items.n_idite = Convert.ToInt32(LblIdItem.Text);
            e_Items.n_idpc = Convert.ToInt32(funFunciones.NulosN(LblIdCtaCom.Text));
            e_Items.n_idpcven = Convert.ToInt32(funFunciones.NulosN(LblIdCtaVen.Text));
            e_Items.n_iddet = Convert.ToInt32(CboDet.SelectedValue);
            e_Items.n_idper = Convert.ToInt32(CboPer.SelectedValue);
            e_Items.n_idret =  Convert.ToInt32(CboRet.SelectedValue);
            e_Items.n_idigvcom = Convert.ToInt32(CboIgvCom.SelectedValue);
            e_Items.n_idigvven = Convert.ToInt32(CboIgvVen.SelectedValue);
            e_Items.n_idisc = Convert.ToInt32(CboIsc.SelectedValue);         
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
                booResultado = objItems.Insertar(e_Items);
            }

            if (n_QueHace == 2)
            {
                objItems.TraerRegistro(e_Items.n_idite, e_Items.n_idemp);
                DataTable dtresult = new DataTable();
                if (objItems.e_Items == null)
                {
                    booResultado = objItems.Insertar(e_Items);
                }
                else 
                {
                    booResultado = objItems.Actualizar(e_Items);
                }
            }
            return booResultado;
        }
        void DataTableCargar()
        {
            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(81, ref arrCabeceraDg1);

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

            objPcItem.mysConec = mysConec;
            objPcItem.Listar(STU_SISTEMA.EMPRESAID, n_Tipo);
            dtItems = objPcItem.dtLista;

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(81);

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(81, ref arrCabeceraDg1);

            objPlaCue.mysConec = mysConec;
            objPlaCue.Listar(STU_SISTEMA.EMPRESAID);
            dtPlaCue = objPlaCue.dtLista;

            o_Inventario.mysConec = mysConec;
            dtInventario = o_Inventario.Listar(STU_SISTEMA.EMPRESAID,1,1);

            o_per.mysConec = mysConec;
            o_per.Listar();
            dtPer = o_per.dtLista;

            o_ret.mysConec = mysConec;
            o_ret.Listar();
            dtRet = o_ret.dtLista;

            o_det.mysConec = mysConec;
            o_det.Listar();
            dtDet = o_det.dtLista;

            o_cat7.mysConec = mysConec;
            o_cat7.Listar();
            dtigvcon = o_cat7.dtLista;
            o_cat7.Listar();
            dtigvven = o_cat7.dtLista;
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboTipExis, dtTipoExis, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboFamilia, dtFamilia, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboClase, dtClase, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboSubClase, dtSubClase, "n_id", "c_des");

            funDatos.ComboBoxCargarDataTable(CboPer, dtPer, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboRet, dtRet, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboDet, dtDet, "n_id", "c_des");

            funDatos.ComboBoxCargarDataTable(CboIgvCom, dtigvcon, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboIgvVen, dtigvven, "n_id", "c_des");
            
        }
        void Blanquea()
        {
            TxtCodigo.Text = "";
            TxtDescripcion.Text = "";

            CboTipExis.SelectedValue = 0;
            CboFamilia.SelectedValue = 0;
            CboClase.SelectedValue = 0;
            CboSubClase.SelectedValue = 0;

            LblIdCtaCom.Text = "";
            LblIdCtaVen.Text = "";
            LblIdItem.Text = "";
            TxtCtaDesCom.Text = "";
            TxtCtaDesVen.Text = "";
            TxtCtaCom.Text = "";
            TxtCtaVen.Text ="";
            CboPer.SelectedValue = 0;
            CboRet.SelectedValue = 0;
            CboDet.SelectedValue = 0;
            CboIsc.SelectedValue = 0;
            CboIgvCom.SelectedValue = 0;
            CboIgvVen.SelectedValue = 0;
        }
        void Bloquea()
        {
            //TxtDescripcion.Enabled = !TxtDescripcion.Enabled;

            //CboTipExis.Enabled = !CboTipExis.Enabled;
            CboFamilia.Enabled = false;
            CboClase.Enabled = false;
            CboSubClase.Enabled = false;

            CmdBusCtaCom.Enabled = !CmdBusCtaCom.Enabled;
            CmdBusCtaVen.Enabled = !CmdBusCtaVen.Enabled;
            CboDet.Enabled = !CboDet.Enabled;
            CboRet.Enabled = !CboRet.Enabled;
            CboPer.Enabled = !CboPer.Enabled;
            CboIsc.Enabled = !CboIsc.Enabled;
            CboIgvCom.Enabled = !CboIgvCom.Enabled;
            CboIgvVen.Enabled = !CboIgvVen.Enabled;

            CmdBusIte.Enabled = !CmdBusIte.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            //ToolGruMod.Enabled = !ToolGruMod.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            //ToolGruEli.Enabled = !ToolGruEli.Enabled;
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

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

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
            booAgregando = false;
            //MostrarDatosItems();
            CboTipExis.Focus();
        }
        void MostrarDatosItems()
        {
            int n_idreg = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            DataTable dtresult = funDatos.DataTableFiltrar(dtItems, "n_iditem = " + n_idreg.ToString() + "");
            if (dtresult.Rows.Count != 0)
            {
                LblIdItem.Text = dtresult.Rows[0]["n_iditem"].ToString();
                TxtCodigo.Text = dtresult.Rows[0]["c_codpro"].ToString();
                CboTipExis.SelectedValue = dtresult.Rows[0]["n_idtipexi"].ToString();
                CboClase.SelectedValue = dtresult.Rows[0]["n_idclas"].ToString();
                CboFamilia.SelectedValue = dtresult.Rows[0]["n_idfam"].ToString();
                CboSubClase.SelectedValue = dtresult.Rows[0]["n_idsubclas"].ToString();
                TxtDescripcion.Text = dtresult.Rows[0]["c_despro"].ToString();
            }
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

        private void FrmAsigCtaConItems_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
        }

        private void FrmAsigCtaConItems_DoubleClick(object sender, EventArgs e)
        {
            if (DgLista.RowCount == 0) { return; }
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            VerRegistro(intIdRegistro);
            booAgregando = false;
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
                    objItems.Listar(STU_SISTEMA.EMPRESAID, n_Tipo);
                    dtItems = objItems.dtLista;
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objItems.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
            TxtCodigo.Enabled = false;
            DgLista.Focus();
        }

        private void FrmAsigCtaConItems_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                //if (n_Dedonde == 1)
                //{
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
                //}
                //else
                //{
                //    ToolNuevo.Visible = false;
                //    ToolModificar.Visible = false;
                //    ToolEliminar.Visible = false;
                //    toolStripSeparator1.Visible = false;
                //    toolStripSeparator2.Visible = false;
                //    ToolImprimir.Visible = false;
                //    ToolSalir.Visible = false;

                //    Nuevo();
                //    CboTipExis.Enabled = false;
                //    MostrarDatos();
                //    CboFamilia.Focus();
                //}
            }    
        }
        void MostrarDatos()
        {
            CboTipExis.SelectedValue = n_TipoExistencia;
        }

        private void ChkVerActivos_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkVerActivos.Checked == true)
            {
                n_Tipo = 1;
                ToolModificar.Visible = true;
                ToolNuevo.Visible = true;
            }
            else
            {
                n_Tipo = 2;
                ToolModificar.Visible = true;
                ToolNuevo.Visible = false;
            }

            objItems.mysConec = mysConec;
            objItems.Listar(STU_SISTEMA.EMPRESAID, n_Tipo);
            dtItems = objItems.dtLista;
            // MOSTRAMOS LOS DATOS EN LA GRILLA
            ListarItems();
        }

        private void ToolModificar_Click(object sender, EventArgs e)
        {
            Modificar();
        }

        private void CmdBusCtaCom_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            dtResult = objPlaCue.BuscarCuenta(dtPlaCue);
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtCtaCom.Text = dtResult.Rows[0]["c_cuecon"].ToString();
                    TxtCtaDesCom.Text = dtResult.Rows[0]["c_des"].ToString();
                    LblIdCtaCom.Text = dtResult.Rows[0]["n_id"].ToString();
                }
                else
                {
                    TxtCtaCom.Text = "";
                    TxtCtaDesCom.Text = "";
                    LblIdCtaCom.Text = "";
                }
            }
        }

        private void CmdBusCtaVen_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            dtResult = objPlaCue.BuscarCuenta(dtPlaCue);
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtCtaVen.Text = dtResult.Rows[0]["c_cuecon"].ToString();
                    TxtCtaDesVen.Text = dtResult.Rows[0]["c_des"].ToString();
                    LblIdCtaVen.Text = dtResult.Rows[0]["n_id"].ToString();
                }
                else
                {
                    TxtCtaCom.Text = "";
                    TxtCtaDesVen.Text = "";
                    LblIdCtaVen.Text = "";
                }
            }
        }

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            VerRegistro(intIdRegistro);
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

        private void CmdBusIte_Click(object sender, EventArgs e)
        {
            DataTable dtresul = new DataTable();
            CN_con_pcitems o_item = new CN_con_pcitems();
            o_item.mysConec = mysConec;
            o_item.STU_SISTEMA = STU_SISTEMA;
            dtresul = o_item.BuscarItemPendiente(STU_SISTEMA.EMPRESAID);
            if (dtresul == null) { return; }
            if (dtresul.Rows.Count != 0)
            {
                dtresul = funDatos.DataTableFiltrar(dtInventario, "n_id = " + dtresul.Rows[0]["n_id"] + "");
                if (dtresul.Rows.Count != 0)
                {
                    LblIdItem.Text = dtresul.Rows[0]["n_id"].ToString();
                    TxtCodigo.Text = dtresul.Rows[0]["c_codpro"].ToString();
                    CboTipExis.SelectedValue = dtresul.Rows[0]["n_idtipexi"].ToString();
                    CboClase.SelectedValue = dtresul.Rows[0]["n_idclas"].ToString();
                    CboFamilia.SelectedValue = dtresul.Rows[0]["n_idfam"].ToString();
                    CboSubClase.SelectedValue = dtresul.Rows[0]["n_idsubclas"].ToString();
                    TxtDescripcion.Text = dtresul.Rows[0]["c_despro"].ToString();
                }
            }
        }
    }
}

