using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Tesoreria;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Almacen;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Logistica;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Tesoreria;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Sunat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSF_NET_Tesoreria.Formularios
{
    public partial class FrmCanjeDocumentos : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_tes_canje objRegistros = new CN_tes_canje();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sun_tipmon objMon = new CN_sun_tipmon();
        CN_sun_tipcam ObjTC = new CN_sun_tipcam();
        CN_mae_clipro objCliPro = new CN_mae_clipro();
        CN_log_compras objCompras = new CN_log_compras();
        CN_vta_ventas objVentas = new CN_vta_ventas();
        CN_tes_documentos objTesDoc = new CN_tes_documentos();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_mae_meses objMes = new CN_mae_meses();

        SIAC_Objetos.Funciones objFunciones = new SIAC_Objetos.Funciones();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        DatosMySql FunMysql = new DatosMySql();

        DataTable dtLista = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtMon = new DataTable();
        DataTable dtPro = new DataTable();
        DataTable dtCli = new DataTable();
        DataTable dtTesDoc = new DataTable();
        DataTable dtTipDoc = new DataTable();
        DataTable dtMes = new DataTable();
        DataTable dtTc = new DataTable();

        List<BE_TES_CANJEDET> l_CanjeDet = new List<BE_TES_CANJEDET>();
        BE_TES_CANJE e_Canje = new BE_TES_CANJE();
        
        bool booAgregando;
                
        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[8, 5];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[8, 5];
        string[,] arrCabeceraFlex2 = new string[12, 5];
        bool booSeEjecuto = false;

        bool b_Agregando = false;
        string c_Numerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string c_Caracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmCanjeDocumentos()
        {
            InitializeComponent();
        }

        private void FrmCanjeDocumentos_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();
            //booAgregando = true;
            funDatos.ComboBoxCargarDataTable(CboMon, dtMon, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMes, "n_id", "c_des");
            //booAgregando = false;
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
        void ConfigurarFormulario()
        {
            this.Height = 579;
            this.Width = 956;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
            this.Text = dtForm.Rows[0]["c_titfor"].ToString();

            arrCabeceraFlex1[0, 0] = "Tip. Doc.";
            arrCabeceraFlex1[0, 1] = "30";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "";

            arrCabeceraFlex1[1, 0] = "Nº Documento";
            arrCabeceraFlex1[1, 1] = "110";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "";

            arrCabeceraFlex1[2, 0] = "Fch. Emision";
            arrCabeceraFlex1[2, 1] = "70";
            arrCabeceraFlex1[2, 2] = "F";
            arrCabeceraFlex1[2, 3] = "dd/MM/yyyy";
            arrCabeceraFlex1[2, 4] = "";

            arrCabeceraFlex1[3, 0] = "Fch. Vencimiento";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "F";
            arrCabeceraFlex1[3, 3] = "dd/MM/yyyy";
            arrCabeceraFlex1[3, 4] = "";

            arrCabeceraFlex1[4, 0] = "Mon eda.";
            arrCabeceraFlex1[4, 1] = "30";
            arrCabeceraFlex1[4, 2] = "C";
            arrCabeceraFlex1[4, 3] = "";
            arrCabeceraFlex1[4, 4] = "";

            arrCabeceraFlex1[5, 0] = "Importe";
            arrCabeceraFlex1[5, 1] = "75";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "";

            arrCabeceraFlex1[6, 0] = "Saldo";
            arrCabeceraFlex1[6, 1] = "75";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "";

            arrCabeceraFlex1[7, 0] = "IdDocumento";
            arrCabeceraFlex1[7, 1] = "0";
            arrCabeceraFlex1[7, 2] = "N";
            arrCabeceraFlex1[7, 3] = "";
            arrCabeceraFlex1[7, 4] = "";

            funFlex.FlexMostrarDatos(FgDocCom, arrCabeceraFlex1, dtLista, 2, false);
            FgDocCom.Rows.Count = 2;

            arrCabeceraFlex2[0, 0] = "Tip. Doc.";
            arrCabeceraFlex2[0, 1] = "30";
            arrCabeceraFlex2[0, 2] = "C";
            arrCabeceraFlex2[0, 3] = "";
            arrCabeceraFlex2[0, 4] = "";

            arrCabeceraFlex2[1, 0] = "Nº Documento";
            arrCabeceraFlex2[1, 1] = "110";
            arrCabeceraFlex2[1, 2] = "C";
            arrCabeceraFlex2[1, 3] = "";
            arrCabeceraFlex2[1, 4] = "";

            arrCabeceraFlex2[2, 0] = "Fch. Emision";
            arrCabeceraFlex2[2, 1] = "70";
            arrCabeceraFlex2[2, 2] = "F";
            arrCabeceraFlex2[2, 3] = "dd/MM/yyyy";
            arrCabeceraFlex2[2, 4] = "";

            arrCabeceraFlex2[3, 0] = "Mon eda";
            arrCabeceraFlex2[3, 1] = "30";
            arrCabeceraFlex2[3, 2] = "C";
            arrCabeceraFlex2[3, 3] = "";
            arrCabeceraFlex2[3, 4] = "";

            arrCabeceraFlex2[4, 0] = "Importe";
            arrCabeceraFlex2[4, 1] = "75";
            arrCabeceraFlex2[4, 2] = "D";
            arrCabeceraFlex2[4, 3] = "0.00";
            arrCabeceraFlex2[4, 4] = "";

            arrCabeceraFlex2[5, 0] = "Saldo";
            arrCabeceraFlex2[5, 1] = "75";
            arrCabeceraFlex2[5, 2] = "D";
            arrCabeceraFlex2[5, 3] = "0.00";
            arrCabeceraFlex2[5, 4] = "";

            arrCabeceraFlex2[6, 0] = "Nº Documento";
            arrCabeceraFlex2[6, 1] = "110";
            arrCabeceraFlex2[6, 2] = "C";
            arrCabeceraFlex2[6, 3] = "";
            arrCabeceraFlex2[6, 4] = "";

            arrCabeceraFlex2[7, 0] = "Abono";
            arrCabeceraFlex2[7, 1] = "75";
            arrCabeceraFlex2[7, 2] = "D";
            arrCabeceraFlex2[7, 3] = "0.00";
            arrCabeceraFlex2[7, 4] = "";

            arrCabeceraFlex2[8, 0] = "Saldo";
            arrCabeceraFlex2[8, 1] = "75";
            arrCabeceraFlex2[8, 2] = "D";
            arrCabeceraFlex2[8, 3] = "0.00";
            arrCabeceraFlex2[8, 4] = "";

            arrCabeceraFlex2[9, 0] = "Saldo Doc.";
            arrCabeceraFlex2[9, 1] = "75";
            arrCabeceraFlex2[9, 2] = "D";
            arrCabeceraFlex2[9, 3] = "0.00";
            arrCabeceraFlex2[9, 4] = "";

            arrCabeceraFlex2[10, 0] = "IdDocCli";
            arrCabeceraFlex2[10, 1] = "0";
            arrCabeceraFlex2[10, 2] = "M";
            arrCabeceraFlex2[10, 3] = "30";
            arrCabeceraFlex2[10, 4] = "";

            arrCabeceraFlex2[11, 0] = "IdDocCanje";
            arrCabeceraFlex2[11, 1] = "0";
            arrCabeceraFlex2[11, 2] = "M";
            arrCabeceraFlex2[11, 3] = "0";
            arrCabeceraFlex2[11, 4] = "";

            funFlex.FlexMostrarDatos(FgDocVen, arrCabeceraFlex2, dtLista, 2, false);
            FgDocVen.Rows.Count = 2;

            funFlex.Flex_UniColumnas(FgDocVen, 0, 1, 6, "DOCUMENTOS EMITIDOS", 1);
            funFlex.Flex_UniColumnas(FgDocVen, 0, 7, 9, "CANJES", 1);
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            dtLista = objRegistros.dtLista;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(76, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(76);

            objMon.mysConec = mysConec;
            dtMon = objMon.Listar();

            ObjTC.mysConec = mysConec;
            dtTc = ObjTC.Listartcano(151, STU_SISTEMA.ANOTRABAJO.ToString());

            objCliPro.mysConec = mysConec;
            dtPro = objCliPro.ListarProveedor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
            dtCli = objCliPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objTesDoc.mysConec = mysConec;
            objTesDoc.Listar();
            dtTesDoc = objTesDoc.dtLista;

            objTipDoc.mysConec = mysConec;
            dtTipDoc = objTipDoc.Listar();

            objMes.mysConec = mysConec;
            dtMes = objMes.Listar();
        }
        private void FrmCanjeDocumentos_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (dtLista.Rows.Count == 0)
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
            // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
            objRegistros.mysConec = mysConec;
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue));

            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);

            dtLista = objRegistros.dtLista;

            LblNumReg.Text = (dtLista.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtLista, true);
        }
        void MostrarEstadoMes(int n_IdEmpresa, int n_IdMes)
        {
            obj_fungen.mysConec = mysConec;
            if (obj_fungen.EstadoPeriodo(n_IdEmpresa, n_IdMes, 5) == true)
            {
                ToolNuevo.Visible = false;
                ToolModificar.Visible = false;
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
                ToolModificar.Visible = true;
                ToolEliminar.Visible = true;

                ToolGrabar.Visible = true;
                ToolCancelar.Visible = true;

                toolStripSeparator1.Visible = true;
                toolStripSeparator2.Visible = true;

                PicClos1.Visible = false;
                PicClos2.Visible = false;
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
                // dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
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
        void AsignarEntidad()
        {
            int n_row = 0;
            if (n_QueHace == 1)
            {
                e_Canje.n_id = 0;
            }
            else
            {
                e_Canje.n_id = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            }

            e_Canje.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Canje.n_idlib = 1;
            e_Canje.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_Canje.n_mes = Convert.ToInt32(CboMeses.SelectedValue);
            e_Canje.d_fchreg = Convert.ToDateTime(TxtFchEmi.Text);
            e_Canje.c_numreg = "";
            e_Canje.c_numser = TxtNumSer.Text;
            e_Canje.c_numdoc = TxtNumDoc.Text;
            e_Canje.d_fchemi = Convert.ToDateTime(TxtFchEmi.Text);
            e_Canje.n_idpro = Convert.ToInt32(LblIdPro.Text);
            e_Canje.n_idcli = Convert.ToInt32(LblIdCli.Text);
            e_Canje.n_idmon = Convert.ToInt32(CboMon.SelectedValue);
            e_Canje.n_impcan = Convert.ToDouble(TxtImpCanje.Text);
            e_Canje.c_glosa = TxtGlo.Text;

            // GRABAMOS LOS DOCUMENTOS DEL CLIENTE
            for (n_row = 2; n_row <= FgDocVen.Rows.Count - 1; n_row++)
            {
                BE_TES_CANJEDET e_CanjeDet = new BE_TES_CANJEDET();

                e_CanjeDet.n_idcan = 0;
                e_CanjeDet.n_tipo = 1;
                e_CanjeDet.n_iddoc = Convert.ToInt32(FgDocVen.GetData(n_row,11));
                e_CanjeDet.n_impdoc = Convert.ToDouble(FgDocVen.GetData(n_row, 5)); 
                e_CanjeDet.n_saldo = Convert.ToDouble(FgDocVen.GetData(n_row, 6)); ;
                e_CanjeDet.n_caniddoc = Convert.ToInt32(FgDocVen.GetData(n_row, 12));
                e_CanjeDet.n_canimpdoc = Convert.ToDouble(FgDocVen.GetData(n_row, 8));
                e_CanjeDet.n_canimpsal = Convert.ToDouble(FgDocVen.GetData(n_row, 9)); 

                l_CanjeDet.Add(e_CanjeDet);
            }

            // GRABAMOS LOS DOCUMENTOS DEL PROVEEDOR
            for (n_row = 2; n_row <= FgDocCom.Rows.Count - 1; n_row++)
            {
                BE_TES_CANJEDET e_CanjeDet = new BE_TES_CANJEDET();

                e_CanjeDet.n_idcan = 0;
                e_CanjeDet.n_tipo = 2;
                e_CanjeDet.n_iddoc = Convert.ToInt32(FgDocCom.GetData(n_row, 8));
                e_CanjeDet.n_impdoc = Convert.ToDouble(FgDocCom.GetData(n_row, 6));
                e_CanjeDet.n_saldo = Convert.ToDouble(FgDocCom.GetData(n_row, 7)); ;
                e_CanjeDet.n_caniddoc = 0;
                e_CanjeDet.n_canimpdoc = 0;
                e_CanjeDet.n_canimpsal = 0;

                l_CanjeDet.Add(e_CanjeDet);
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (TxtFchEmi.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de proceso !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchEmi.Focus();
                return booEstado;
            }
            if (Convert.ToInt32(CboMon.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMon.Focus();
                return booEstado;
            }
            if (Convert.ToDouble(funFunciones.NulosN(LblTc.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de cambio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMon.Focus();
                return booEstado;
            }
            if (funFunciones.NulosC(TxtNumSer.Text) == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumSer.Focus();
                return booEstado;
            }
            if (funFunciones.NulosC(TxtNumDoc.Text) == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumDoc.Focus();
                return booEstado;
            }
            if (funFunciones.NulosC(LblIdPro.Text) == "")
            {
                MessageBox.Show("¡ No ha especificado el proveedor para realizar el canje !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumDoc.Focus();
                return booEstado;
            }
            if (funFunciones.NulosC(LblIdCli.Text) == "")
            {
                MessageBox.Show("¡ No ha especificado el cliente para realizar el canje !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumDoc.Focus();
                return booEstado;
            }
            if (Convert.ToDouble(funFunciones.NulosN(TxtImpCanje.Text)) == 0)
            {
                MessageBox.Show("¡ No ha realizado el canje, haga clic en el boton cancelar para realizar el canje !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtImpCanje.Focus();
                return booEstado;
            }
         
            return booEstado;
        }
        bool Grabar()
        {
            bool booResultado = false;
            if (CamposOK() == false)
            {
                return booResultado;
            }
            AsignarEntidad();
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(e_Canje, l_CanjeDet);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(e_Canje, l_CanjeDet);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.n_ErrorNumber.ToString() + " = " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        private void ToolCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            objRegistros = null;
            objFormVis = null;
            this.Close();
        }
        void Bloquea()
        {
            TxtFchEmi.Enabled = !TxtFchEmi.Enabled;
            CboMon.Enabled = !CboMon.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtNumRucCli.Enabled = !TxtNumRucCli.Enabled;
            TxtNumRucPro.Enabled = !TxtNumRucPro.Enabled;
            TxtGlo.Enabled = !TxtGlo.Enabled;

            CmdBusCli.Enabled = !CmdBusCli.Enabled;
            CmdBusPro.Enabled = !CmdBusPro.Enabled;
        }
        void Blanquea()
        {
            TxtFchEmi.Text = "";
            CboMon.SelectedValue = 0;
            TxtGlo.Text = "";
            LblTc.Text = "";
            LblNumAsi.Text = "";
            TxtNumRucPro.Text = "";
            TxtNomProv.Text = "";
            TxtNumRucCli.Text = "";
            TxtNomCli.Text = "";
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";

            LblIdPro.Text = "";
            LblIdCli.Text = "";

            booAgregando = true;
            FgDocCom.Rows.Count = 2;
            FgDocVen.Rows.Count = 2;

            FgDocCom.Rows.Count = FgDocCom.Rows.Count + 1;
            FgDocVen.Rows.Count = FgDocVen.Rows.Count + 1;

            l_CanjeDet.Clear();

            booAgregando = false;
            LblProTot1.Text = "";
            LblProTot2.Text = "";
            LblHabTotSol.Text = "";
            TxtImpCanje.Text = "";
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
        void Nuevo()
        {
            n_QueHace = 1;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            Tab1.SelectedIndex = 1;
            FgDocCom.AllowEditing = false;
            FgDocCom.Rows.Count = 2;
            FgDocVen.Rows.Count = 2;
            CboMon.SelectedValue = 115;
            TxtNumSer.Text = "0001";
            TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 87, TxtNumSer.Text);
            TxtFchEmi.Focus();
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
            FgDocCom.AllowEditing = false;
            TxtFchEmi.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(n_IdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            return booResult;
        }
        void VerRegistro(int n_IdRegistro)
        {
            string c_dato = "";
            DataTable dtCompras = new DataTable();
            DataTable dtVentas = new DataTable();
            l_CanjeDet.Clear();

            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdRegistro);
            e_Canje = objRegistros.e_Canje;
            //l_CanjeDet = objRegistros.l_CanjeDet;
            dtCompras = objRegistros.dtDocCompra;
            dtVentas = objRegistros.dtDocVenta;

            TxtFchEmi.Text = Convert.ToDateTime(e_Canje.d_fchemi).ToString("dd/MM/yyyy");
            CboMon.SelectedValue = e_Canje.n_idmon;
            TxtGlo.Text = e_Canje.c_glosa;
            LblTc.Text = e_Canje.n_tc.ToString("0.000");
            LblNumAsi.Text = e_Canje.c_numreg;
            TxtNumSer.Text = e_Canje.c_numser;
            TxtNumDoc.Text = e_Canje.c_numdoc;
            LblIdPro.Text = e_Canje.n_idpro.ToString();
            LblIdCli.Text = e_Canje.n_idcli.ToString();

            c_dato = funDatos.DataTableBuscar(dtPro, "n_id", "c_numdoc", e_Canje.n_idpro.ToString(), "N").ToString();
            TxtNumRucPro.Text = c_dato;

            c_dato = funDatos.DataTableBuscar(dtPro, "n_id", "c_nombre", e_Canje.n_idpro.ToString(), "N").ToString();
            TxtNomProv.Text = c_dato;

            c_dato = funDatos.DataTableBuscar(dtCli, "n_id", "c_numdoc", e_Canje.n_idcli.ToString(), "N").ToString();
            TxtNumRucCli.Text = c_dato;

            c_dato = funDatos.DataTableBuscar(dtCli, "n_id", "c_nombre", e_Canje.n_idcli.ToString(), "N").ToString();
            TxtNomCli.Text = c_dato;
            TxtImpCanje.Text = e_Canje.n_impcan.ToString("0.00");
            TxtGlo.Text = e_Canje.c_glosa;

            booAgregando = true;
            FgDocCom.Rows.Count = 2;
            FgDocVen.Rows.Count = 2;

            MostrarDetalle(dtCompras, dtVentas);

            SumarTotDocCli();
            SumarTotDocPro();

            booAgregando = false;
        }
        void MostrarDetalle(DataTable dtCompras, DataTable dtVentas)
        {
            int n_row = 0;
            string c_dato = "";
            FgDocCom.Rows.Count = 2;
            FgDocVen.Rows.Count = 2;

            //AGREGAMOS LOS DOCUMENTOS DEL PROVEEDOR
            for (n_row = 0; n_row <= dtCompras.Rows.Count - 1; n_row++)
            {
                FgDocCom.Rows.Count = FgDocCom.Rows.Count + 1;
                c_dato = dtCompras.Rows[n_row]["c_destipmon"].ToString();
                FgDocCom.SetData(FgDocCom.Rows.Count - 1, 1, c_dato);

                c_dato = dtCompras.Rows[n_row]["c_numdoc"].ToString();
                FgDocCom.SetData(FgDocCom.Rows.Count - 1, 2, c_dato);

                c_dato = dtCompras.Rows[n_row]["d_fchemi"].ToString();
                FgDocCom.SetData(FgDocCom.Rows.Count - 1, 3, c_dato);

                c_dato = dtCompras.Rows[n_row]["d_fchven"].ToString();
                FgDocCom.SetData(FgDocCom.Rows.Count - 1, 4, c_dato);

                c_dato = dtCompras.Rows[n_row]["c_desmon"].ToString();
                FgDocCom.SetData(FgDocCom.Rows.Count - 1, 5, c_dato);

                c_dato = Convert.ToDouble(dtCompras.Rows[n_row]["n_impdoc"]).ToString("0.00");
                FgDocCom.SetData(FgDocCom.Rows.Count - 1, 6, c_dato);

                c_dato = Convert.ToDouble(dtCompras.Rows[n_row]["n_saldo"]).ToString("0.00");
                FgDocCom.SetData(FgDocCom.Rows.Count - 1, 7, c_dato);

                c_dato = Convert.ToDouble(dtCompras.Rows[n_row]["n_iddoc"]).ToString();
                FgDocCom.SetData(FgDocCom.Rows.Count - 1, 8, c_dato);
            }

            //AGREGAMOS LOS DOCUMENTOS DEL CLIENTE
            for (n_row = 0; n_row <= dtVentas.Rows.Count - 1; n_row++)
            {
                FgDocVen.Rows.Count = FgDocVen.Rows.Count + 1;
                c_dato = dtVentas.Rows[n_row]["c_destipmon"].ToString();
                FgDocVen.SetData(FgDocVen.Rows.Count - 1, 1, c_dato);

                c_dato = dtVentas.Rows[n_row]["c_numdoc"].ToString();
                FgDocVen.SetData(FgDocVen.Rows.Count - 1, 2, c_dato);

                c_dato = dtVentas.Rows[n_row]["d_fchemi"].ToString();
                FgDocVen.SetData(FgDocVen.Rows.Count - 1, 3, c_dato);
                
                c_dato = dtVentas.Rows[n_row]["c_desmon"].ToString();
                FgDocVen.SetData(FgDocVen.Rows.Count - 1, 4, c_dato);

                c_dato = Convert.ToDouble(dtVentas.Rows[n_row]["n_impdoc"]).ToString("0.00");
                FgDocVen.SetData(FgDocVen.Rows.Count - 1, 5, c_dato);

                c_dato = Convert.ToDouble(dtVentas.Rows[n_row]["n_saldo"]).ToString("0.00");
                FgDocVen.SetData(FgDocVen.Rows.Count - 1, 6, c_dato);

                c_dato = dtVentas.Rows[n_row]["c_cannumdoc"].ToString();
                FgDocVen.SetData(FgDocVen.Rows.Count - 1, 7, c_dato);

                c_dato = Convert.ToDouble(dtVentas.Rows[n_row]["n_canimpdoc"]).ToString("0.00");
                FgDocVen.SetData(FgDocVen.Rows.Count - 1, 8, c_dato);

                c_dato = Convert.ToDouble(dtVentas.Rows[n_row]["n_canimpsal"]).ToString("0.00");
                FgDocVen.SetData(FgDocVen.Rows.Count - 1, 9, c_dato);

                c_dato = (Convert.ToDouble(dtVentas.Rows[n_row]["n_saldo"]) - Convert.ToDouble(dtVentas.Rows[n_row]["n_canimpdoc"])).ToString("0.00");
                FgDocVen.SetData(FgDocVen.Rows.Count - 1, 10, c_dato);

                c_dato = Convert.ToDouble(dtVentas.Rows[n_row]["n_iddoc"]).ToString();
                FgDocVen.SetData(FgDocVen.Rows.Count - 1, 11, c_dato);
            }
        }
        private void CmdBusPro_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            objCliPro.mysConec = mysConec;
            
            dtResult = objCliPro.BuscarCliPro(dtPro, 2, "n_id", "");
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtNumRucPro.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                    LblIdPro.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtNomProv.Text = dtResult.Rows[0]["c_nombre"].ToString();
                }
                else
                {
                    TxtNumRucPro.Text = "";
                    LblIdPro.Text = "";
                    TxtNomProv.Text = "";
                }
            }
        }
        private void CmdBusCli_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            objCliPro.mysConec = mysConec;

            dtResult = objCliPro.BuscarCliPro(dtCli, 1, "n_id", "");
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtNumRucCli.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                    LblIdCli.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtNomCli.Text = dtResult.Rows[0]["c_nombre"].ToString();
                }
                else
                {
                    TxtNumRucCli.Text = "";
                    LblIdCli.Text = "";
                    TxtNomCli.Text = "";
                }
            }
        }
        private void TxtNumRucPro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!c_Numerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtNumRucCli_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!c_Numerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtNumRucPro_Validated(object sender, EventArgs e)
        {

        }
        private void TxtNumRucCli_Validated(object sender, EventArgs e)
        {
            DataTable dtresul = new DataTable();
            dtresul = funDatos.DataTableFiltrar(dtCli, "c_numdoc = '" + TxtNumRucCli.Text + "'");

            if (dtresul.Rows.Count != 0)
            {
                TxtNumRucCli.Text = dtresul.Rows[0]["c_numdoc"].ToString();
                TxtNomCli.Text = dtresul.Rows[0]["c_nombre"].ToString();
                LblIdCli.Text = dtresul.Rows[0]["n_id"].ToString();
            }
            else
            {
                TxtNumRucCli.Text = "";
                TxtNomCli.Text = "";
                LblIdCli.Text = "";
                TxtNumRucCli.Focus();
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
                if (!c_Numerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtNumSer_Validated(object sender, EventArgs e)
        {
            //if (TxtNumSer.Text != "")
            //{
            //    string strCad = "0000" + TxtNumSer.Text;
            //    TxtNumSer.Text = strCad.Substring(strCad.Length - 4, 4);
            //}
            if (TxtNumSer.Text != "")
            {
                string strCad = "0000" + TxtNumSer.Text;

                //if (entEmpresa.n_aplife == 1)
                //{
                //    TxtNumSer.Text = "0" + strCad.Substring(strCad.Length - 3, 3);
                //}
                //else
                //{
                //    TxtNumSer.Text = "F" + strCad.Substring(strCad.Length - 3, 3);
                //}
                TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 87, TxtNumSer.Text);
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
                if (!c_Numerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text != "")
            {
                string strCad = "0000000000" + TxtNumDoc.Text;
                TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
            }
        }

        private void TxtFchEmi_Validated(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            LblTc.Text = objFunciones.ObtenerTC(dtTc, TxtFchEmi.Text).ToString("0.000");
        }

        private void CmdAddOri_Click(object sender, EventArgs e)
        {
            if (LblIdPro.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre del proveedor !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdBusPro.Focus();
                return;
            }

            string c_dato = "";
            int n_row = 0;
            DataTable dtResul = new DataTable();
            objCompras.mysConec = mysConec;
            dtResul = objCompras.DocumentosConSaldo(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblIdPro.Text));
            booAgregando = true;
            if (dtResul != null)
            {
                if (dtResul.Rows.Count != 0)
                {
                    for (n_row = 0; n_row <= dtResul.Rows.Count - 1; n_row++)
                    {
                        FgDocCom.Rows.Count = FgDocCom.Rows.Count + 1;

                        c_dato = dtResul.Rows[n_row]["c_destipdoc"].ToString();
                        FgDocCom.SetData(FgDocCom.Rows.Count - 1, 1, c_dato);

                        c_dato = dtResul.Rows[n_row]["c_numdoc"].ToString();
                        FgDocCom.SetData(FgDocCom.Rows.Count - 1, 2, c_dato);

                        c_dato = Convert.ToDateTime(dtResul.Rows[n_row]["d_fchdoc"]).ToString("dd/MM/yyyy");
                        FgDocCom.SetData(FgDocCom.Rows.Count - 1, 3, c_dato);

                        c_dato = dtResul.Rows[n_row]["c_desmon"].ToString();
                        FgDocCom.SetData(FgDocCom.Rows.Count - 1, 5, c_dato);

                        c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_importe"]).ToString("0.00");
                        FgDocCom.SetData(FgDocCom.Rows.Count - 1, 6, c_dato);

                        c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_saldo"]).ToString("0.00");
                        FgDocCom.SetData(FgDocCom.Rows.Count - 1, 7, c_dato);

                        c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_id"]).ToString();
                        FgDocCom.SetData(FgDocCom.Rows.Count - 1, 8, c_dato);
                    }
                }
                SumarTotDocPro();
            }
            booAgregando = false;
        }
        void SumarTotDocPro()
        {
            double n_imp1 = funFlex.FlexSumarCol(FgDocCom, 6, 2, FgDocCom.Rows.Count - 1);
            double n_imp2 = funFlex.FlexSumarCol(FgDocCom, 7, 2, FgDocCom.Rows.Count - 1);

            LblProTot1.Text = n_imp1.ToString("0.00");
            LblProTot2.Text = n_imp2.ToString("0.00");
        }
        private void CmdAddDes_Click(object sender, EventArgs e)
        {
            if (LblIdCli.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdBusCli.Focus();
                return;
            }

            string c_dato = "";
            int n_row = 0;
            DataTable dtResul = new DataTable();
            objVentas.mysConec = mysConec;
            dtResul = objVentas.DocumentosConSaldo(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblIdCli.Text));

            if (dtResul != null)
            {
                if (dtResul.Rows.Count != 0)
                {
                    for (n_row = 0; n_row <= dtResul.Rows.Count - 1; n_row++)
                    {
                        FgDocVen.Rows.Count = FgDocVen.Rows.Count + 1;

                        c_dato = dtResul.Rows[n_row]["c_destipdoc"].ToString();
                        FgDocVen.SetData(FgDocVen.Rows.Count - 1, 1, c_dato);

                        c_dato = dtResul.Rows[n_row]["c_numdoc"].ToString();
                        FgDocVen.SetData(FgDocVen.Rows.Count - 1, 2, c_dato);

                        c_dato = Convert.ToDateTime(dtResul.Rows[n_row]["d_fchdoc"]).ToString("dd/MM/yyyy");
                        FgDocVen.SetData(FgDocVen.Rows.Count - 1, 3, c_dato);

                        c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_importe"]).ToString("0.00");
                        FgDocVen.SetData(FgDocVen.Rows.Count - 1, 5, c_dato);

                        c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_saldo"]).ToString("0.00");
                        FgDocVen.SetData(FgDocVen.Rows.Count - 1, 6, c_dato);

                        c_dato = Convert.ToDouble(dtResul.Rows[n_row]["n_id"]).ToString();
                        FgDocVen.SetData(FgDocVen.Rows.Count - 1, 11, c_dato);
                    }
                }
                SumarTotDocCli();
            }
            LblHabTotSol.Text = funFlex.FlexSumarCol(FgDocVen, 6, 2, FgDocVen.Rows.Count - 1).ToString("0.00");
        }
        void SumarTotDocCli()
        {
            double n_imp1 = funFlex.FlexSumarCol(FgDocVen, 6, 2, FgDocVen.Rows.Count - 1);
            double n_imp2 = funFlex.FlexSumarCol(FgDocVen, 8, 2, FgDocVen.Rows.Count - 1);

            LblHabTotSol.Text = n_imp1.ToString("0.00");
            TxtImpCanje.Text = n_imp2.ToString("0.00");
        }
        private void CmdCanDoc_Click(object sender, EventArgs e)
        {
            if (FgDocCom.Rows.Count == 2)
            {
                MessageBox.Show("¡ No ha indicado los documentos del proveedor que se van a canjear !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdAddOri.Focus();
                return;
            }
            if (FgDocVen.Rows.Count == 2)
            {
                MessageBox.Show("¡ No ha indicado los documentos del cliente que se van a canjear !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdAddDes.Focus();
                return;
            }

            int n_row = 0;
            int n_rowcom = 0;
            double n_saldo = 0;
            double n_impdeuven = 0;
            double n_saldopag = 0;
            double n_impdocven = 0;
            int n_iddoccom = 0;
            for (n_row = 2; n_row <= FgDocVen.Rows.Count - 1; n_row++)
            {
                n_impdeuven = Convert.ToDouble(FgDocVen.GetData(n_row, 6));
                for (n_rowcom = 2; n_rowcom <= FgDocCom.Rows.Count - 1; n_rowcom++)
                {
                    n_iddoccom = Convert.ToInt32(FgDocCom.GetData(n_rowcom, 8).ToString());
                    n_impdocven = Convert.ToDouble(FgDocCom.GetData(n_rowcom, 6).ToString());
                    n_saldo = Convert.ToDouble(FgDocCom.GetData(n_rowcom, 6).ToString());
                    n_saldopag = n_impdeuven - n_saldo;
                    FgDocVen.SetData(n_row, 7, FgDocCom.GetData(n_rowcom, 2).ToString());
                    FgDocVen.SetData(n_row, 8, n_saldo.ToString());
                    FgDocVen.SetData(n_row, 9, (n_impdocven - n_saldo).ToString());
                    FgDocVen.SetData(n_row, 10, n_saldopag.ToString());
                    FgDocVen.SetData(n_row, 12, n_iddoccom.ToString());
                }
            }
            TxtImpCanje.Text = funFlex.FlexSumarCol(FgDocVen, 8, 2, FgDocVen.Rows.Count - 1).ToString("0.00");
        }
        private void TxtFchEmi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboMon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtNumRucCli_TextChanged(object sender, EventArgs e)
        {

        }
        private void TxtGlo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!c_Caracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
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
                dtResult = funDbGrid.DG_Filtrar(dtLista, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
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

        private void FgDocVen_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (booAgregando == true) { return; }
            if (e.Col == 5)
            {
                //double n_valor = 0;
                //FgDocVen.SetData(FgDocVen.Row, 5, n_valor.ToString("0.00"));
            }
            if (e.Col == 8)
            {
                double n_valor = Convert.ToDouble(FgDocVen.GetData(e.Row,6));
                double n_valor1 = Convert.ToDouble(FgDocVen.GetData(e.Row, 8));
                n_valor = (n_valor - n_valor1);
                FgDocVen.SetData(FgDocVen.Row, 9, n_valor.ToString("0.00"));
            }
            TxtImpCanje.Text = funFlex.FlexSumarCol(FgDocVen, 8, 2, FgDocVen.Rows.Count - 1).ToString("0.00");
        }

        private void FgDocVen_EnterCell(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (FgDocVen.Col == 8)
            {
                FgDocVen.AllowEditing = true;
            }
            else
            {
                FgDocVen.AllowEditing = false;
            }
        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            if (booAgregando == true) { return; }

            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue));    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtLista = objRegistros.dtLista;
            LblNumReg.Text = (dtLista.Rows.Count).ToString();
            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
            DgLista.DataSource = dtLista;
            if (dtLista.Rows.Count == 0)
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

        private void CmdDelOri_Click(object sender, EventArgs e)
        {
            if (FgDocCom.Rows.Count <= 2) { return; }
            if (booAgregando == true) { return; }

            FgDocCom.RemoveItem(FgDocCom.Row);

            SumarTotDocPro();
        }

        private void CmdDelDes_Click(object sender, EventArgs e)
        {
            if (FgDocVen.Rows.Count <= 2) { return; }
            if (booAgregando == true) { return; }

            FgDocVen.RemoveItem(FgDocVen.Row);

            
            SumarTotDocCli();
        }
    }
}
