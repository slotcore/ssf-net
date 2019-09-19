using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Sunat;
using SIAC_Entidades.Maestros;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Ventas;
using SIAC_Objetos.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSF_NET_Ventas.Formularios
{
    public partial class FrmClientePrecio : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_EsCliPro;                                                       // INDICA SE ES CLENTE O PROVEEDOR (1 = CLIENTE   ; 2 = PROVEEDOR) 
        // OBJETOS LOCALES
        CN_mae_clipro objRegistros = new CN_mae_clipro();
        CN_mae_tipoclipro objtipclipro = new CN_mae_tipoclipro();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sun_distritos objDis = new CN_sun_distritos();
        CN_sun_provincia objPro = new CN_sun_provincia();
        CN_sun_departamentos objDep = new CN_sun_departamentos();
        CN_sun_catempresa objCatEmpresa = new CN_sun_catempresa();
        CN_sun_tipcontribuyente objTipCon = new CN_sun_tipcontribuyente();
        CN_sun_tipdocide objDocIden = new CN_sun_tipdocide();
        CN_sys_formulario objForm = new CN_sys_formulario();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_MAE_CLIPRO BE_ListaReg = new BE_MAE_CLIPRO();
        BE_MAE_CLIPRO BE_Registro = new BE_MAE_CLIPRO();

        List<BE_MAE_CLIPROITEMS> l_Items = new List<BE_MAE_CLIPROITEMS>();

        // DATATABLE LOCALES
        DataTable dtRegistros = new DataTable();
        DataTable dtLocal = new DataTable();
        DataTable dtCatEmpresa = new DataTable();
        DataTable dtTipCon = new DataTable();
        DataTable dtDocIden = new DataTable();
        DataTable dtDep = new DataTable();
        DataTable dtPro = new DataTable();
        DataTable dtDis = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtCliPro = new DataTable();
        DataTable dtTipo = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[7, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex2 = new string[7, 5];  
        
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^-" + (char)8;
        public FrmClientePrecio()
        {
            InitializeComponent();
        }

        private void FrmClientePrecio_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboDocIde, dtDocIden, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 602;
            this.Width = 832;
                        
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            this.Text = "CLIENTES - LISTA DE ITEMS Y PRECIOS"; //dtForm.Rows[0]["c_titfor"].ToString();

            arrCabeceraFlex2[0, 0] = "Codigo";
            arrCabeceraFlex2[0, 1] = "80";
            arrCabeceraFlex2[0, 2] = "C";
            arrCabeceraFlex2[0, 3] = "";
            arrCabeceraFlex2[0, 4] = "c_tipexides";

            arrCabeceraFlex2[1, 0] = "Mercaderia / Producto / Bien o Servicio";
            arrCabeceraFlex2[1, 1] = "300";
            arrCabeceraFlex2[1, 2] = "C";
            arrCabeceraFlex2[1, 3] = "";
            arrCabeceraFlex2[1, 4] = "c_tipexides";

            arrCabeceraFlex2[2, 0] = "Uni. Med.";
            arrCabeceraFlex2[2, 1] = "40";
            arrCabeceraFlex2[2, 2] = "C";
            arrCabeceraFlex2[2, 3] = "";
            arrCabeceraFlex2[2, 4] = "c_tipexides";

            arrCabeceraFlex2[3, 0] = "P. Bruto";
            arrCabeceraFlex2[3, 1] = "70";
            arrCabeceraFlex2[3, 2] = "D";
            arrCabeceraFlex2[3, 3] = "0.00";
            arrCabeceraFlex2[3, 4] = "c_tipexides";

            arrCabeceraFlex2[4, 0] = "Tasa IGV";
            arrCabeceraFlex2[4, 1] = "60";
            arrCabeceraFlex2[4, 2] = "D";
            arrCabeceraFlex2[4, 3] = "0.00";
            arrCabeceraFlex2[4, 4] = "c_tipexides";

            arrCabeceraFlex2[5, 0] = "P. Neto";
            arrCabeceraFlex2[5, 1] = "70";
            arrCabeceraFlex2[5, 2] = "D";
            arrCabeceraFlex2[5, 3] = "0.00";
            arrCabeceraFlex2[5, 4] = "c_tipexides";

            arrCabeceraFlex2[6, 0] = "Id Producto";
            arrCabeceraFlex2[6, 1] = "0";
            arrCabeceraFlex2[6, 2] = "N";
            arrCabeceraFlex2[6, 3] = "";
            arrCabeceraFlex2[6, 4] = "c_tipexides";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex2, dtRegistros, 2, false);
        }
        void DataTableCargar()
        {
            objForm.mysConec = mysConec;                                         // CARGAMOS LOS DATOS DEL FORMULARIO
            objFormVis.mysConec = mysConec;
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            if (n_EsCliPro == 1)
            {
                dtRegistros = objRegistros.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
                objFormVis.ObtenerCabeceraLista(12, ref arrCabeceraDg1);         // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista - CLIENTES
                dtForm = objForm.TraerRegistro(12);
            }
            else
            {
                dtRegistros = objRegistros.ListarProveedor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
                objFormVis.ObtenerCabeceraLista(13, ref arrCabeceraDg1);         // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista - PROVEEDORES
                dtForm = objForm.TraerRegistro(13);
            }

            objDocIden.mysConec = mysConec;
            dtDocIden = objDocIden.Listar();                                // CARGAMOS DOCUMENTO DE IDENTIDAD
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtRegistros.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtRegistros, true);
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
            int n_fil = 0;
            string c_dato = "";
            DataTable dtresult = new DataTable();
            int n_idcli = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            CN_mae_cliproitems o_items = new CN_mae_cliproitems();

            o_items.mysConec = mysConec;
            o_items.Listar(n_idcli);
            dtresult = o_items.dtListar;

            objRegistros.mysConec = mysConec;
            BE_Registro = objRegistros.TraerRegistro(n_IdRegistro);

            CboDocIde.SelectedValue = BE_Registro.n_idtipdoc;
            TxtNumDocIde.Text = BE_Registro.c_numdoc;
            TxtNomEmp.Text = BE_Registro.c_nombre;

            FgItems.Rows.Count = 2;
            for (n_fil = 0; n_fil <= dtresult.Rows.Count - 1; n_fil++)
            {
                FgItems.Rows.Count = FgItems.Rows.Count+1;
                
                c_dato = dtresult.Rows[n_fil]["c_codpro"].ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 1, c_dato);
                
                c_dato = dtresult.Rows[n_fil]["c_despro"].ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 2, c_dato);
                
                c_dato = dtresult.Rows[n_fil]["c_abrpre"].ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 3, c_dato);
                
                c_dato = Convert.ToDouble(dtresult.Rows[n_fil]["n_prebru"]).ToString("0.00000");
                FgItems.SetData(FgItems.Rows.Count - 1, 4, c_dato);

                c_dato = Convert.ToDouble(dtresult.Rows[n_fil]["n_tasigv"]).ToString("0.00");
                FgItems.SetData(FgItems.Rows.Count - 1, 5, c_dato);

                c_dato = Convert.ToDouble(dtresult.Rows[n_fil]["n_prenet"]).ToString("0.000000");
                FgItems.SetData(FgItems.Rows.Count - 1, 6, c_dato);

                c_dato = Convert.ToInt32(dtresult.Rows[n_fil]["n_idite"]).ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 7, c_dato);
            }
            
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
            TxtNumDocIde.Focus();
        }
        void Blanquea()
        {
            FgItems.Rows.Count = 2;
            CboDocIde.SelectedValue = 0;
            TxtNumDocIde.Text = "";
            TxtNomEmp.Text = "";
        }
        void Bloquea()
        {
            CboDocIde.Enabled = !CboDocIde.Enabled;
            TxtNumDocIde.Enabled = !TxtNumDocIde.Enabled;
            TxtNomEmp.Enabled = !TxtNomEmp.Enabled;

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
            //ToolExportar.Enabled = !ToolExportar.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
        }
        void Modificar()
        {
            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            TxtNumDocIde.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    if (n_EsCliPro == 1)
                    {
                        dtRegistros = objRegistros.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
                    }
                    else
                    {
                        dtRegistros = objRegistros.ListarProveedor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
                    }

                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
            CN_mae_cliproitems o_items = new CN_mae_cliproitems();
            bool booResultado = false;
            if (CamposOK() == false)
            {
                return booResultado;
            }
            AsignarEntidad();
            o_items.mysConec = mysConec;

            if (n_QueHace == 1)
            {
                booResultado = o_items.Insertar(l_Items);
            }

            if (n_QueHace == 2)
            {
                booResultado = o_items.Insertar(l_Items);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            if (FgItems.Rows.Count == 2)
            {
                MessageBox.Show("¡ No hay items que guardar, debe de agregar almenos un item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            int n_fil = 0;
            int n_idcli = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR
            

            l_Items.Clear();

            for (n_fil = 2; n_fil <= FgItems.Rows.Count - 1; n_fil++)
            {
                BE_MAE_CLIPROITEMS e_Items = new BE_MAE_CLIPROITEMS();
                e_Items.n_idcli = n_idcli;
  
                e_Items.n_idite = Convert.ToInt32(FgItems.GetData(n_fil, 7));
                e_Items.n_prebru = Convert.ToDouble(FgItems.GetData(n_fil, 4));
                e_Items.n_prenet = Convert.ToDouble(FgItems.GetData(n_fil, 6));
                e_Items.n_tasigv = Convert.ToDouble(FgItems.GetData(n_fil, 5));

                l_Items.Add(e_Items);
            }

        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt16(CboDocIde.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento de identidad del contribuyente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (TxtNumDocIde.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero del documento de identidad !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }
            return booEstado;
        }

        private void FrmClientePrecio_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (dtRegistros.Rows.Count == 0)
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
                objRegistros.mysConec = mysConec;

                if (n_EsCliPro == 1)
                {
                    dtRegistros = objRegistros.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
                }
                else
                {
                    dtRegistros = objRegistros.ListarProveedor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
                }

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
            dtRegistros = null;

            objFormVis = null;

            this.Close();
        }

        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (n_QueHace != 3) { return; }

            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    VerRegistro(intIdRegistro);
                }
            }
        }

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            VerRegistro(intIdRegistro);
        }

        private void DgLista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DataTable dtResult = new DataTable();

                string c_CadFiltro = funDbGrid.DG_LeerCondicionesFiltro(DgLista);
                dtResult = funDbGrid.DG_Filtrar(dtRegistros, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
        }

        private void FrmClientePrecio_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 110, this.Width - 38);
        }

        private void CmdAddItem_Click(object sender, EventArgs e)
        {
            int n_row = 0;
            DataTable dtResult = new DataTable();
            CN_alm_inventario o_item = new CN_alm_inventario();
            string c_dato = "";

            o_item.mysConec = mysConec;
            string c_cadIN = funFlex.Flex_ObtenerDatosCol(FgItems, 7);
            dtResult = o_item.FiltrarSelccionarItems(STU_SISTEMA.EMPRESAID, c_cadIN);
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                    {
                        FgItems.Rows.Count = FgItems.Rows.Count + 1;
                        c_dato = dtResult.Rows[n_row]["c_codpro"].ToString();
                        FgItems.SetData(FgItems.Rows.Count - 1, 1, c_dato);

                        c_dato = dtResult.Rows[n_row]["c_despro"].ToString();
                        FgItems.SetData(FgItems.Rows.Count - 1, 2, c_dato);

                        c_dato = dtResult.Rows[n_row]["c_abrpre"].ToString();
                        FgItems.SetData(FgItems.Rows.Count - 1, 3, c_dato);
                        FgItems.SetData(FgItems.Rows.Count - 1, 4, "0.00");
                        c_dato = "18.00";
                        FgItems.SetData(FgItems.Rows.Count - 1, 5, c_dato);
                        FgItems.SetData(FgItems.Rows.Count - 1, 6, "0.00");
                        c_dato = Convert.ToInt32(dtResult.Rows[n_row]["n_id"]).ToString();
                        FgItems.SetData(FgItems.Rows.Count - 1, 7, c_dato);
                    }
                }
            }
        }

        private void CmdDelItem_Click(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count == 2) { return; }
            
            FgItems.RemoveItem(FgItems.Row);
        }

        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3) { return; }

            double n_valor = 0;
            double n_precio = 0;
            double n_tasigv = 0;

            if (FgItems.Col == 4)
            {
                n_valor = Convert.ToDouble(FgItems.GetData(e.Row, 4));
                n_tasigv= Convert.ToDouble(FgItems.GetData(e.Row, 5));
                n_precio = n_valor * ((n_tasigv / 100) + 1);

                FgItems.SetData(e.Row, 6, n_precio.ToString("0.00"));
            }
            if (FgItems.Col == 6)
            {
                n_valor = Convert.ToDouble(FgItems.GetData(e.Row, 6));
                n_tasigv = Convert.ToDouble(FgItems.GetData(e.Row, 5));
                n_precio = n_valor / ((n_tasigv / 100) + 1);

                FgItems.SetData(e.Row, 4, n_precio.ToString("0.00"));
            }
        }

        private void FgItems_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            
            if ((FgItems.Col == 4) || (FgItems.Col == 6))
            {
                FgItems.AllowEditing = true;
            }
            else
            {
                FgItems.AllowEditing = false;
            }
        }

        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (n_QueHace == 3) { return; }

            if ((e.Col == 5) || (e.Col == 5))
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ToolExcel_Click(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            VerRegistro(intIdRegistro);
            string c_archivo = STU_SISTEMA.EMPRESARUC + "-PRECIOS.xls";
            funFlex.ExportToExcel(FgItems, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "LISTA DE PRECIOS DEL CLIENTE", TxtNomEmp.Text, c_archivo);
        }
    }
}
