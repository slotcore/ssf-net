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
    public partial class FrmManCliPro : Form
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
        CN_mae_condpago o_conpag = new CN_mae_condpago();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_MAE_CLIPRO BE_ListaReg = new BE_MAE_CLIPRO();
        BE_MAE_CLIPRO BE_Registro = new BE_MAE_CLIPRO();

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
        DataTable dtconpag = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[7, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^-" + (char)8;
        public FrmManCliPro()
        {
            InitializeComponent();
        }

        private void FrmManCliPro_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboCatEmp, dtCatEmpresa, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipCon, dtTipCon, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboDocIde, dtDocIden, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipo, dtTipo, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboConPag, dtconpag, "n_id", "c_des");
            
            funDatos.ComboBoxCargarDataTable(CboDep, dtDep, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboPro, dtPro, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboDis, dtDis, "n_id", "c_des");
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
                                                 
            objCatEmpresa.mysConec = mysConec;
            dtCatEmpresa = objCatEmpresa.Listar();                          // CARGAMOS CATEGORIA DE EMPRESA
            
            objTipCon.mysConec = mysConec;
            dtTipCon = objTipCon.Listar();                                  // CARGAMOS TIPO DE CONTRIBUYENTE

            objDocIden.mysConec = mysConec;
            dtDocIden = objDocIden.Listar();                                // CARGAMOS DOCUMENTO DE IDENTIDAD
            
            objDep.mysConec = mysConec;
            dtDep = objDep.Listar();                                        // CARGAMOS TODOS LOS DEPARTAMENTOS
                    
            objPro.mysConec = mysConec;
            dtPro = objPro.Listar();                                        // CARGAMOS TODAS LAS PROVINCIAS
            
            objDis.mysConec = mysConec;
            dtDis = objDis.Listar();                                        // CARGAMOS TODOS LOS DISTRITOS

            objtipclipro.mysConec = mysConec;
            dtTipo = objtipclipro.Listar();
            if (n_EsCliPro == 1)
            {
                dtTipo = funDatos.DataTableFiltrar(dtTipo, "n_id IN (1,3)");
            }
            else
            {
                dtTipo = funDatos.DataTableFiltrar(dtTipo, "n_id IN (2,3)");
            }

            o_conpag.mysConec = mysConec;
            dtconpag = o_conpag.Listar();
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
            //booAgregando = true;
            objRegistros.mysConec = mysConec;
            BE_Registro = objRegistros.TraerRegistro(n_IdRegistro);

            CboCatEmp.SelectedValue = BE_Registro.n_idcatemp;
            CboDep.SelectedValue = BE_Registro.n_iddep;
            CboPro.SelectedValue = BE_Registro.n_idpro;
            CboDis.SelectedValue = BE_Registro.n_iddis;
            CboDocIde.SelectedValue = BE_Registro.n_idtipdoc;
            CboTipo.SelectedValue = BE_Registro.n_tipreg;
            CboConPag.SelectedValue = BE_Registro.n_idcondpag;
            CboTipCon.SelectedValue = BE_Registro.n_idtipcon;

            TxtApeMat.Text = BE_Registro.c_apecli2;
            TxtApePat.Text = BE_Registro.c_apecli1;
            TxtNumDocIde.Text = BE_Registro.c_numdoc;
            TxtDir.Text = BE_Registro.c_dir;
            TxtDirGir.Text = BE_Registro.c_letgirdir;
            TxtFax.Text = BE_Registro.c_fax;
            TxtGir.Text = BE_Registro.c_letnomgir;
            TxtGirTel.Text = BE_Registro.c_lettel;
            TxtMail.Text = BE_Registro.c_email;
            TxtNom1.Text = BE_Registro.c_nomcli1;
            TxtNom2.Text = BE_Registro.c_nomcli2;
            TxtNomCom.Text = BE_Registro.c_nomcon;
            TxtNomEmp.Text = BE_Registro.c_nombre;
            TxtNumDocGir.Text = BE_Registro.c_letnumdoc;
            TxtPagWeb.Text = BE_Registro.c_pagweb;
            TxtTel.Text = BE_Registro.c_tel;

            if (BE_Registro.n_ageret == 1)
            {
                ChkAgeRet.Checked = true;
            }
            else
            {
                ChkAgeRet.Checked = false;
            }
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
            if (n_EsCliPro == 1)
            {
                CboTipo.SelectedValue = 1;
            }
            else
            {
                CboTipo.SelectedValue = 2;
            }
            TxtNumDocIde.Focus();
        }
        void Blanquea()
        {
            CboCatEmp.SelectedValue =0;
            CboDep.SelectedValue = 0;
            CboDis.SelectedValue = 0;
            CboDocIde.SelectedValue = 0;
            CboPro.SelectedValue = 0;
            CboTipCon.SelectedValue = 0;
            CboTipo.SelectedValue = 0;
            CboConPag.SelectedValue = 0;
            TxtApeMat.Text = "";
            TxtApePat.Text = "";
            TxtNumDocIde.Text = "";
            TxtDir.Text = "";
            TxtDirGir.Text = "";
            TxtFax.Text = "";
            TxtGir.Text = "";
            TxtGirTel.Text = "";
            TxtMail.Text = "";
            TxtNom1.Text = "";
            TxtNom2.Text = "";
            TxtNomCom.Text = "";
            TxtNomEmp.Text = "";
            TxtNumDocGir.Text = "";
            TxtPagWeb.Text = "";
            TxtTel.Text = "";
            ChkAgeRet.Checked = false;
        }
        void Bloquea()
        {
            CboCatEmp.Enabled = !CboCatEmp.Enabled;
            CboDep.Enabled = !CboDep.Enabled;
            CboDis.Enabled = false;
            CboPro.Enabled = false;
            CboDocIde.Enabled = !CboDocIde.Enabled;
            CboTipo.Enabled = !CboTipo.Enabled;
            CboTipCon.Enabled = !CboTipCon.Enabled;
            CboConPag.Enabled = !CboConPag.Enabled;

            TxtApeMat.Enabled = false;
            TxtApePat.Enabled = false;
            TxtNom1.Enabled = false;
            TxtNom2.Enabled = false;

            TxtNumDocIde.Enabled = !TxtNumDocIde.Enabled;
            TxtDir.Enabled = !TxtDir.Enabled;
            TxtDirGir.Enabled = !TxtDirGir.Enabled;
            TxtFax.Enabled = !TxtFax.Enabled;
            TxtGir.Enabled = !TxtGir.Enabled;
            TxtGirTel.Enabled = !TxtGirTel.Enabled;
            TxtMail.Enabled = !TxtMail.Enabled;

            TxtNomCom.Enabled = !TxtNomCom.Enabled;
            //TxtNomEmp.Enabled = !TxtNomEmp.Enabled;
            TxtNumDocGir.Enabled = !TxtNumDocGir.Enabled;
            TxtPagWeb.Enabled = !TxtPagWeb.Enabled;
            TxtTel.Enabled = !TxtTel.Enabled;
            ChkAgeRet.Enabled = !ChkAgeRet.Enabled;
            CmdBusCli.Enabled = !CmdBusCli.Enabled;
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

            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            TxtNumDocIde.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

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
            bool booResultado = false;
            if (CamposOK() == false)
            {
                return booResultado;
            }
            AsignarEntidad();

            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(BE_ListaReg);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(BE_ListaReg);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            BE_ListaReg.n_idemp = 0;
            if (STU_SISTEMA.SYS_UNIBD == 0) { BE_ListaReg.n_idemp = STU_SISTEMA.EMPRESAID; }
            
            BE_ListaReg.n_idcatemp = Convert.ToInt32(CboCatEmp.SelectedValue);
            BE_ListaReg.n_idtipdoc = Convert.ToInt32(CboDocIde.SelectedValue);
            BE_ListaReg.n_idtipcon = Convert.ToInt32(CboTipCon.SelectedValue);
            BE_ListaReg.c_nombre = TxtNomEmp.Text;
            BE_ListaReg.c_apecli2 = TxtApeMat.Text;
            BE_ListaReg.c_apecli1 = TxtApePat.Text;
            BE_ListaReg.c_numdoc = TxtNumDocIde.Text;
            BE_ListaReg.c_nomcli1 = TxtNom1.Text;
            BE_ListaReg.c_nomcli2 = TxtNom2.Text;
            BE_ListaReg.c_dir = TxtDir.Text;
            BE_ListaReg.c_nomcon = TxtNomCom.Text;
            BE_ListaReg.n_idcondpag = Convert.ToInt32(CboConPag.SelectedValue);

            BE_ListaReg.c_letgirdir = TxtDirGir.Text;
            BE_ListaReg.c_fax = TxtFax.Text;
            BE_ListaReg.c_letnomgir = TxtGir.Text;
            BE_ListaReg.c_lettel = TxtGirTel.Text;
            BE_ListaReg.c_email = TxtMail.Text;     
            BE_ListaReg.c_letnumdoc = TxtNumDocGir.Text;
            BE_ListaReg.c_pagweb = TxtPagWeb.Text;
            BE_ListaReg.c_tel = TxtTel.Text;
            BE_ListaReg.n_iddep = Convert.ToInt32(CboDep.SelectedValue);
            BE_ListaReg.n_iddis = Convert.ToInt32(CboDis.SelectedValue);
            BE_ListaReg.n_idpro = Convert.ToInt32(CboPro.SelectedValue);
            BE_ListaReg.d_fchini = Convert.ToDateTime("01/01/2016");
            BE_ListaReg.c_codcen = "";
            BE_ListaReg.n_idven = 0;
            
            if (n_QueHace == 1)
            {
                BE_ListaReg.n_id = 0;
                BE_ListaReg.n_estado = 1;
            }
            else
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
                BE_ListaReg.n_id = intIdRegistro;
                //BE_ListaReg.n_estado = 1;
            }
            
            if (ChkAgeRet.Checked == false)
            {
                BE_ListaReg.n_ageret = 0;
            }
            else
            {
                BE_ListaReg.n_ageret = 1;
            }

            BE_ListaReg.n_tipreg = Convert.ToInt32(CboTipo.SelectedValue);   // INDICAMOS QUE ES CLIENTE
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt32(CboCatEmp.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la categoria de la empresa !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt32(CboTipCon.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de contribuyente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt32(CboDocIde.SelectedValue) == 0)
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

            if (TipoContribuyente(Convert.ToInt32(CboTipCon.SelectedValue))==1)
            {
                if (TxtApeMat.Text == "")
                {
                    MessageBox.Show("¡ No ha especificado el apellido materno del contribuyente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    return booEstado;
                }
            
                if (TxtApePat.Text == "")
                {
                    MessageBox.Show("¡ No ha especificado el apellido paterno del contribuyente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    return booEstado;
                }
                if (TxtNom1.Text == "")
                {
                    MessageBox.Show("¡ No ha especificado el primer nombre del contribuyente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    return booEstado;
                }
            }
            else
            {
                if (TxtNomEmp.Text == "")
                {
                    MessageBox.Show("¡ No ha especificado el nombre de la empresa !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    return booEstado;
                }
            }

            if (TxtNomCom.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre comercial de la empresa !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            if (TxtDir.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la direccion de la empresa !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                return booEstado;
            }

            DataTable dtresul = new DataTable();
            dtresul = objRegistros.Listar(2, 1);

            if (n_QueHace == 1)
            { 
                dtresul = funDatos.DataTableFiltrar(dtresul, "c_numdoc = '" + TxtNumDocIde.Text + "'");
                if (dtresul.Rows.Count > 0)
                {
                    MessageBox.Show("¡ El numero de documento de identidad ingresado ya existe, ingrese otro !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    return booEstado;
                }
            }
            return booEstado;
        }
        private void FrmManCliPro_Activated(object sender, EventArgs e)
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
                int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    VerRegistro(intIdRegistro);
                }
            }
        }
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
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

        private void FrmManCliPro_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 110, this.Width - 38);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void TxtDes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtNomEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtApePat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtApeMat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtNom1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtNom2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtNomCom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtDir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtPagWeb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtMail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtGir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtDirGir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtNumDocGir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtGirTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void CboDep_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();

            if (CboDep.SelectedValue != null)
            {
                booAgregando = true;
                string strCadenaFiltro = "n_iddep = " + CboDep.SelectedValue.ToString() + "";
                CboPro.Enabled = true;

                CboPro.SelectedValue = 0;
                CboDis.SelectedValue = 0;

                DtFiltro = funDatos.DataTableFiltrar(dtPro, strCadenaFiltro);
                funDatos.ComboBoxCargarDataTable(CboPro, DtFiltro, "n_id", "c_des");
                if (n_QueHace==3)
                {
                    CboPro.Enabled = false;
                    CboDis.Enabled = false;
                }
                else
                {
                    CboPro.Enabled = true;
                    CboDis.Enabled = false;
                }

                booAgregando = false;
            }
        }

        private void CboPro_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();

            if (CboPro.SelectedValue != null)
            {
                string strCadenaFiltro = "n_iddep = " + CboDep.SelectedValue.ToString() + " AND n_idpro = " + CboPro.SelectedValue.ToString() + "";
                CboDis.Enabled = true;

                CboDis.SelectedValue = 0;

                DtFiltro = funDatos.DataTableFiltrar(dtDis, strCadenaFiltro);
                funDatos.ComboBoxCargarDataTable(CboDis, DtFiltro, "n_id", "c_des");

                if (n_QueHace == 3)
                {
                    CboDis.Enabled = false;
                }
                else
                {
                    CboDis.Enabled = true;
                }
                
            }
        }

        private void CboTipCon_SelectedValueChanged(object sender, EventArgs e)
        {
            //dtTipCon
            if (booAgregando == true) { return; }

            DataTable DtFiltro = new DataTable();

            if (CboTipCon.SelectedValue != null)
            {
                string strCadenaFiltro = "n_id = " + CboTipCon.SelectedValue.ToString() + "";
                DtFiltro = funDatos.DataTableFiltrar(dtTipCon, strCadenaFiltro);

                if (n_QueHace == 3)
                {
                    TxtApePat.Enabled = false;
                    TxtApeMat.Enabled = false;
                    TxtNom1.Enabled = false;
                    TxtNom2.Enabled = false;
                    TxtNomEmp.Enabled = false;
                }
                else
                {
                    if (Convert.ToInt32(DtFiltro.Rows[0]["n_tipo"]) == 1)
                    {

                        TxtApePat.Enabled=true;
                        TxtApeMat.Enabled = true;
                        TxtNom1.Enabled = true;
                        TxtNom2.Enabled = true;
                        TxtNomEmp.Enabled = false;
                    }
                    else
                    {
                        TxtApePat.Enabled = false;
                        TxtApeMat.Enabled = false;
                        TxtNom1.Enabled = false;
                        TxtNom2.Enabled = false;
                        TxtNomEmp.Enabled = true;
                    }
                    TxtNomEmp.Text = "";
                    TxtNom1.Text = "";
                    TxtNom2.Text = "";
                    TxtApePat.Text = "";
                    TxtApeMat.Text = "";
                }

            }
        }
        int TipoContribuyente(int n_idTipoContribuyente)
        {
            int n_tipo = 0;
            DataTable DtFiltro = new DataTable();
            string strCadenaFiltro = "n_id = " + CboTipCon.SelectedValue.ToString() + "";
            DtFiltro = funDatos.DataTableFiltrar(dtTipCon, strCadenaFiltro);

            if (Convert.ToInt32(DtFiltro.Rows[0]["n_tipo"]) == 1)
            {
                n_tipo = 1;
            }
            else
            {
                n_tipo = 2;
            }
            return n_tipo;
        }

        private void TxtApePat_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtApePat_Validated(object sender, EventArgs e)
        {
            if (TxtApePat.Text != "")
            {
                TxtNomEmp.Text = TxtApePat.Text + " " + TxtApeMat.Text + ", " + TxtNom1.Text + " " + TxtNom2.Text;
                TxtNomCom.Text = TxtApePat.Text + " " + TxtApeMat.Text + ", " + TxtNom1.Text + " " + TxtNom2.Text;
            }
        }

        private void TxtApeMat_Validated(object sender, EventArgs e)
        {
            if (TxtApeMat.Text != "")
            {
                TxtNomEmp.Text = TxtApePat.Text + " " + TxtApeMat.Text + ", " + TxtNom1.Text + " " + TxtNom2.Text;
                TxtNomCom.Text = TxtApePat.Text + " " + TxtApeMat.Text + ", " + TxtNom1.Text + " " + TxtNom2.Text;
            }
        }

        private void TxtNom1_Validated(object sender, EventArgs e)
        {
            if (TxtNom1.Text != "")
            {
                TxtNomEmp.Text = TxtApePat.Text + " " + TxtApeMat.Text + ", " + TxtNom1.Text + " " + TxtNom2.Text;
                TxtNomCom.Text = TxtApePat.Text + " " + TxtApeMat.Text + ", " + TxtNom1.Text + " " + TxtNom2.Text;
            }
        }

        private void TxtNom2_Validated(object sender, EventArgs e)
        {
            if (TxtNom2.Text != "")
            {
                TxtNomEmp.Text = TxtApePat.Text + " " + TxtApeMat.Text + ", " + TxtNom1.Text + " " + TxtNom2.Text;
                TxtNomCom.Text = TxtApePat.Text + " " + TxtApeMat.Text + ", " + TxtNom1.Text + " " + TxtNom2.Text;
            }
        }

        private void TxtNomEmp_Validated(object sender, EventArgs e)
        {
            if (TxtNomEmp.Text != "")
            {
                TxtNomCom.Text = TxtNomEmp.Text;
            }
        }

        private void CboDep_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CmdBusCli_Click(object sender, EventArgs e)
        {
            string c_acteco = "";
            string c_direcc = "";
            Helper.Cls_ServiciosSunat xFun = new Helper.Cls_ServiciosSunat();
            xFun.ConsultarRUC();
            
            //string c_acteco = xFun.Contribuyente_ActEco;
            string c_condic = xFun.Contribuyente_Condicion;
            if (xFun.Contribuyente_Direccion.Length <= 100)
            {
                c_direcc = xFun.Contribuyente_Direccion;
            }
            else
            {
                c_direcc = xFun.Contribuyente_Direccion.Substring(0, 100);
            }
            
            string c_estado = xFun.Contribuyente_Estado;
            string c_fchiniact = xFun.Contribuyente_FchIniAct;
            string c_nombre = xFun.Contribuyente_Nombre;
            string c_numruc = xFun.Contribuyente_NumRUC;
            string c_tipcon = xFun.Contribuyente_TipCon;
            xFun = null;
            if (c_numruc != "")
            { 
                TxtNumDocIde.Text = c_numruc;
                TxtNomEmp.Text = c_nombre;
                TxtDir.Text = c_direcc;
                CboDocIde.SelectedValue = 4;

                int n_idtipcon = Convert.ToInt32(funDatos.DataTableBuscar(dtTipCon, "c_des", "n_id", c_tipcon, "C"));
                CboTipCon.SelectedValue = n_idtipcon;
                CboCatEmp.SelectedValue = 1;

                if ((n_idtipcon == 1) || (n_idtipcon == 2))
                {
                    string[] dato = new string[10];
                    dato = c_nombre.Split(' ');
                    int n_row = 1;

                    if (n_row <= dato.Length) { TxtApePat.Text = funFunciones.NulosC(dato[0]); n_row = n_row + 1; }
                    if (n_row <= dato.Length) { TxtApeMat.Text = funFunciones.NulosC(dato[1]); n_row = n_row + 1; }
                    if (n_row <= dato.Length) { TxtNom1.Text = funFunciones.NulosC(dato[2]); n_row = n_row + 1; }
                    if (n_row <= dato.Length) { TxtNom2.Text = funFunciones.NulosC(dato[3]); n_row = n_row + 1; }
                }
                TxtNomEmp.Text = c_nombre;
                TxtNomCom.Text = c_nombre;
            }
        }
    }
}
