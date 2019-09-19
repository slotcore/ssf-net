using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using SIAC_Entidades.Logistica;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Logistica;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Sunat;
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

namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmregPercepciones : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_TipoRegistro;                                                  // INDICA QUE TIPO DE REGISTRO SE MOSTRARA (1 = PERCEPCION VENTAS; 2 = PERCEPCION COMPRAS)
        
        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_con_regpercepcion objRegistros = new CN_con_regpercepcion();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_con_percepcion objPer = new CN_con_percepcion();
        CN_sun_tipmon objMon = new CN_sun_tipmon();
        CN_mae_clipro objPro = new CN_mae_clipro();
        CN_con_tc objTC = new CN_con_tc();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();

        DataTable dtLista = new DataTable();
        DataTable dtPro = new DataTable();
        DataTable dtMon = new DataTable();
        DataTable dtMes = new DataTable();
        DataTable dtPer = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtCompras = new DataTable();
        DataTable dtTc = new DataTable();

        bool booAgregando;
        SIAC_Objetos.Funciones objFunciones = new SIAC_Objetos.Funciones();

        BE_CON_REGPERCEPCION e_Per = new BE_CON_REGPERCEPCION();
        List<BE_CON_REGPERCEPCIONDET> l_PerDet = new List<BE_CON_REGPERCEPCIONDET>();
        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[10, 5];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[10, 5];

        string c_Numerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string c_NumerovalidosFE = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZEF1234567890." + (char)8;                                        // + (char)8;
        string c_Caracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;

        bool booSeEjecuto = false;
        public FrmregPercepciones()
        {
            InitializeComponent();
        }

        private void FrmregPercepciones_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void CargarCombos()
        {

            DataTableCargar();
            booAgregando = true;
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMes, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipRet, dtPer, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMon, dtMon, "n_id", "c_des");

            booAgregando = false;
        }
        void ConfigurarFormulario()
        {
            this.Height = 579;
            this.Width = 955;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            
            booAgregando = true;
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
            booAgregando = false;

            if (n_TipoRegistro == 1)
            {
                label4.Text = "Cliente";
                this.Text = dtForm.Rows[0]["c_titfor"].ToString()+" - VENTAS";
            }
            else
            {
                label4.Text = "Proveedor";
                this.Text = dtForm.Rows[0]["c_titfor"].ToString() + " - COMPRAS";
            }

            arrCabeceraFlex1[0, 0] = "Nº Documento";
            arrCabeceraFlex1[0, 1] = "120";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "";

            arrCabeceraFlex1[1, 0] = "M";
            arrCabeceraFlex1[1, 1] = "40";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "";

            arrCabeceraFlex1[2, 0] = "T.D.";
            arrCabeceraFlex1[2, 1] = "60";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "0.000000";
            arrCabeceraFlex1[2, 4] = "";

            arrCabeceraFlex1[3, 0] = "Fch. Emision";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "F";
            arrCabeceraFlex1[3, 3] = "dd/MM/yyyy";
            arrCabeceraFlex1[3, 4] = "";

            arrCabeceraFlex1[4, 0] = "Importe US$";
            arrCabeceraFlex1[4, 1] = "80";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.00";
            arrCabeceraFlex1[4, 4] = "";

            arrCabeceraFlex1[5, 0] = "Importe S/.";
            arrCabeceraFlex1[5, 1] = "80";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "";

            arrCabeceraFlex1[6, 0] = "";
            arrCabeceraFlex1[6, 1] = "0";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "";

            arrCabeceraFlex1[7, 0] = "% Tasa Percepcion";
            arrCabeceraFlex1[7, 1] = "70";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "";

            arrCabeceraFlex1[8, 0] = "Importe Percepcion";
            arrCabeceraFlex1[8, 1] = "80";
            arrCabeceraFlex1[8, 2] = "D";
            arrCabeceraFlex1[8, 3] = "0.00";
            arrCabeceraFlex1[8, 4] = "";

            arrCabeceraFlex1[9, 0] = "IdDocumento";
            arrCabeceraFlex1[9, 1] = "0";
            arrCabeceraFlex1[9, 2] = "N";
            arrCabeceraFlex1[9, 3] = "";
            arrCabeceraFlex1[9, 4] = "";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtLista, 2, false);
            FgItems.Rows.Count = 10;

        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, n_TipoRegistro);
            dtLista = objRegistros.dtLista;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(67, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(67);

            objMon.mysConec = mysConec;
            dtMon = objMon.Listar();

            objPer.mysConec = mysConec;
            objPer.Listar();
            dtPer = objPer.dtLista;

            objPro.mysConec = mysConec;
            if (n_TipoRegistro == 1)
            {
                dtPro = objPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
            }
            else
            {
                dtPro = objPro.ListarProveedor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
            }
            
            objMeses.mysConec = mysConec;
            dtMes = objMeses.Listar();

            objTC.mysConec = mysConec;
            objTC.Listar(0, 0);
            dtTc = objTC.dtLista;
            objTC = null;
        }
        void ListarItems()
        {
            // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
            objRegistros.mysConec = mysConec;
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue), n_TipoRegistro);
            dtLista = objRegistros.dtLista;

            LblNumReg.Text = (dtLista.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtLista, true);
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
            int n_row = 0;
            string c_dato = "";
            DataTable dtDet = new DataTable();
            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdRegistro, n_TipoRegistro);
            e_Per = objRegistros.e_Percepcion;
            dtDet = objRegistros.dtDetalle;

            booAgregando = true;

            LblIdPro.Text = e_Per.n_idcli.ToString();
            TxtNumRuc.Text = funDatos.DataTableBuscar(dtPro, "n_id", "c_numdoc", LblIdPro.Text, "N").ToString();
            TxtPro.Text = funDatos.DataTableBuscar(dtPro, "n_id", "c_nombre", LblIdPro.Text, "N").ToString();
            TxtTc.Text = e_Per.n_tc.ToString("0.000");
            TxtFchEmi.Text = e_Per.d_fchemi.ToString();
            TxtNumSer.Text = e_Per.c_numser;
            TxtNumDoc.Text = e_Per.c_numdoc;
            TxtTc.Text = e_Per.n_tc.ToString("0.000");
            CboMon.SelectedValue = e_Per.n_idmon;
            CboTipRet.SelectedValue = e_Per.n_idper;
            TxtTasRet.Text = e_Per.n_tas.ToString("0.00");
            TxtGlo.Text = e_Per.c_glo;
            LblNumAsi.Text = e_Per.c_numreg;

            FgItems.Rows.Count = 2;
            for (n_row = 0; n_row <= dtDet.Rows.Count - 1; n_row++)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;

                c_dato = dtDet.Rows[n_row]["c_numdoc"].ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 1, c_dato);

                c_dato = dtDet.Rows[n_row]["c_desmon"].ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 2, c_dato);

                c_dato = dtDet.Rows[n_row]["c_docdes"].ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 3, c_dato);

                c_dato = Convert.ToDateTime(dtDet.Rows[n_row]["d_fchdoc"]).ToString("dd/MM/yyyy");
                FgItems.SetData(FgItems.Rows.Count - 1, 4, c_dato);

                if (e_Per.n_idmon == 151)
                {
                    c_dato = Convert.ToDouble(dtDet.Rows[n_row]["n_impdoc"]).ToString("0.00"); ;
                    FgItems.SetData(FgItems.Rows.Count - 1, 5, c_dato);

                    c_dato = (Convert.ToDouble(dtDet.Rows[n_row]["n_impdoc"]) * e_Per.n_tc).ToString("0.00");
                    FgItems.SetData(FgItems.Rows.Count - 1, 6, c_dato);
                }
                else
                {
                    c_dato = "0.00";
                    FgItems.SetData(FgItems.Rows.Count - 1, 5, c_dato);

                    c_dato = Convert.ToDouble(dtDet.Rows[n_row]["n_impdoc"]).ToString("0.00");
                    FgItems.SetData(FgItems.Rows.Count - 1, 6, c_dato);
                }

                //c_dato = Convert.ToDouble(dtDet.Rows[n_row]["n_impcob"]).ToString("0.00");
                //FgItems.SetData(FgItems.Rows.Count - 1, 7, c_dato);

                c_dato = TxtTasRet.Text;
                FgItems.SetData(FgItems.Rows.Count - 1, 8, c_dato);

                c_dato = Convert.ToDouble(dtDet.Rows[n_row]["n_impper"]).ToString("0.00");
                FgItems.SetData(FgItems.Rows.Count - 1, 9, c_dato);

                c_dato = dtDet.Rows[n_row]["n_iddoc"].ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 10, c_dato);
            }
            booAgregando = false;

            SumarColumnas();
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
            CboMon.SelectedValue = 115;
            TxtTc.Text = STU_SISTEMA.TIPOCAMBIO.ToString("0.000");
            TxtNumSer.Text = "E001";
            TxtNumRuc.Focus();
        }
        void Blanquea()
        {
            TxtPro.Text = "";
            TxtTc.Text = "";
            TxtNumRuc.Text = "";
            LblIdPro.Text = "";
            TxtFchEmi.Text = "";
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            CboMon.SelectedValue = 0;
            CboTipRet.SelectedValue = 0;
            TxtTasRet.Text = "";
            TxtGlo.Text = "";
            FgItems.Rows.Count = 2;
            TxtTot1.Text = "";
            TxtTot2.Text = "";
        }
        void Bloquea()
        {
            CmdBusCli.Enabled = !CmdBusCli.Enabled;

            TxtNumRuc.Enabled = !TxtNumRuc.Enabled;
            TxtFchEmi.Enabled = !TxtFchEmi.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            CboMon.Enabled = !CboMon.Enabled;
            CboTipRet.Enabled = !CboTipRet.Enabled;
            //TxtTasRet.Enabled = !TxtTasRet.Enabled;
            TxtGlo.Enabled = !TxtGlo.Enabled;

            CmdAddDoc.Enabled = !CmdAddDoc.Enabled;
            CmdDelDoc.Enabled = !CmdDelDoc.Enabled;
        }
        void MostrarEstadoMes(int n_IdEmpresa, int n_IdMes)
        {
            obj_fungen.mysConec = mysConec;
            if (obj_fungen.EstadoPeriodo(n_IdEmpresa, n_IdMes, 8) == true)
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
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir.Enabled = !ToolImprimir.Enabled;
            ToolExportar.Enabled = !ToolExportar.Enabled;
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
            TxtTc.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int n_IdRegistro = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(n_IdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    Tab1.SelectedIndex = 0;
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(e_Per, l_PerDet, n_TipoRegistro);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(e_Per, l_PerDet, n_TipoRegistro);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.n_ErrorNumber.ToString() + " = " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            if (n_QueHace == 1)
            {
                e_Per.n_id = 0;
            }
            else
            {
                e_Per.n_id = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            }

            e_Per.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Per.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_Per.n_mes = Convert.ToInt16(CboMeses.SelectedValue);

            e_Per.n_idlib = 16;
            e_Per.n_idtipdoc = 86;
            e_Per.n_idper = Convert.ToInt16(CboTipRet.SelectedValue);
            e_Per.n_tas = Convert.ToDouble(TxtTasRet.Text);
            e_Per.n_tip = 1;
            e_Per.n_idcli = Convert.ToInt16(LblIdPro.Text);
            e_Per.c_numser = TxtNumSer.Text;
            e_Per.c_numdoc = TxtNumDoc.Text;
            e_Per.d_fchemi = Convert.ToDateTime(TxtFchEmi.Text);
            e_Per.d_fchreg = Convert.ToDateTime(TxtFchEmi.Text);
            e_Per.n_idmon = Convert.ToInt16(CboMon.SelectedValue);
            e_Per.n_impper = Convert.ToDouble(TxtTot2.Text);
            e_Per.c_glo = TxtGlo.Text;
            e_Per.n_tc = Convert.ToDouble(TxtTc.Text);
            e_Per.n_tipreg = n_TipoRegistro;

            l_PerDet.Clear();
            int n_row = 0;
            for (n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
            {
                BE_CON_REGPERCEPCIONDET e_PerDet = new BE_CON_REGPERCEPCIONDET();

                e_PerDet.n_idper = 0;
                e_PerDet.n_iddoc = Convert.ToInt32(FgItems.GetData(n_row, 10));
                e_PerDet.n_impdoc = Convert.ToDouble(FgItems.GetData(n_row, 6));
                e_PerDet.n_impper = Convert.ToDouble(FgItems.GetData(n_row, 9));

                l_PerDet.Add(e_PerDet);
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (TxtNumSer.Text.Length != 4)
            {
                MessageBox.Show("¡ El numero de digitos para el numero de serie no es valido !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumSer.Focus();
                return booEstado;
            }
            if (TxtNumDoc.Text.Length != 10)
            {
                MessageBox.Show("¡ El numero de digitos para el numero de documento no es valido !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumDoc.Focus();
                return booEstado;
            }

            if (TxtPro.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre del proveedor !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtPro.Focus();
                return booEstado;
            }
            if (TxtNumSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie de la retencion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumSer.Focus();
                return booEstado;
            }
            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento de la retencion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumDoc.Focus();
                return booEstado;
            }

            if (Convert.ToInt16(CboMon.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda de la retencion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMon.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboTipRet.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de retencion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTipRet.Focus();
                return booEstado;
            }

            if (Convert.ToDouble(TxtTot2.Text) == 0)
            {
                MessageBox.Show("¡ El importe de la retencion no puede ser 0 !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtTot2.Focus();
                return booEstado;
            }
            if (Convert.ToDouble(TxtTc.Text) == 0)
            {
                MessageBox.Show("¡ El tipo de cambio no puede ser 0 !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtTc.Focus();
                return booEstado;
            }
            return booEstado;
        }
        private void FrmregPercepciones_Activated(object sender, EventArgs e)
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
                dtResult = funDbGrid.DG_Filtrar(dtLista, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
        }
        private void FrmregPercepciones_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }
        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();

            if (booAgregando == true) { return; }
            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            ListarItems();
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
        private void CmdBusCli_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            objPro.mysConec = mysConec;
            dtResult = objPro.BuscarCliPro(dtPro, 2, "n_id", "");
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtNumRuc.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                    LblIdPro.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtPro.Text = dtResult.Rows[0]["c_nombre"].ToString();
                }
                else
                {
                    TxtNumRuc.Text = "";
                    LblIdPro.Text = "";
                    TxtPro.Text = "";
                }
            }
        }
        private void TxtFchEmi_Validated(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            //if (TxtFchEmi.Text != "")
            //{
                DataTable dtResul = new DataTable();
                dtResul = funDatos.DataTableFiltrar(dtTc, "d_fecha = '" + TxtFchEmi.Text + "'");
                if (dtResul.Rows.Count != 0)
                {
                    TxtTc.Text = dtResul.Rows[0]["n_impven"].ToString();
                }
                else
                {
                    TxtTc.Text = "";
                }
            //}
        }
        private void TxtPro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtFchEmi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtTc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtNumSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                //string strCad = "000" + TxtNumSer.Text;

                //TxtNumSer.Text = "E" + strCad.Substring(strCad.Length - 3, 3);
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!c_NumerovalidosFE.Contains(e.KeyChar))
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
                if (!c_Numerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void CboMon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboTipRet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtTasRet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
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
        private void CboTipRet_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (Convert.ToInt16(CboTipRet.SelectedValue) != 0)
            {
                TxtTasRet.Text = Convert.ToDouble(funDatos.DataTableBuscar(dtPer, "n_id", "n_tasa", Convert.ToInt16(CboTipRet.SelectedValue).ToString(), "N")).ToString("0.00");
            }
            else
            {
                TxtTasRet.Text = "0.00";
            }
        }
        private void CmdAddDoc_Click(object sender, EventArgs e)
        {
            if (LblIdPro.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdBusCli.Focus();
                return;
            }
            if (Convert.ToInt16(CboTipRet.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de retencion que se aplicara !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboTipRet.Focus();
                return;
            }
            if (Convert.ToInt16(CboMon.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMon.Focus();
                return;
            }
            if (TxtTc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el tipo de cambio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtTc.Focus();
                return;
            }
            MostrarDocumentos();
        }
        void MostrarDocumentos()
        {
            string[,] arrCabeceraDg1 = new string[8, 5];
            
            DataTable DtVentas = new DataTable();
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            DataTable dtItemFiltro = new DataTable();

            
            if (n_TipoRegistro == 1)
            {
                CN_vta_ventas objVenta = new CN_vta_ventas();
                objVenta.mysConec = mysConec;
                DtVentas = objVenta.DocumentosPercepcion(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblIdPro.Text));
            }
            else
            {
                CN_log_compras objCompra = new CN_log_compras();
                objCompra.mysConec = mysConec;
                DtVentas = objCompra.DocumentosPercepcion(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblIdPro.Text));
            }

            arrCabeceraDg1[0, 0] = "T.D.";
            arrCabeceraDg1[0, 1] = "40";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "";
            arrCabeceraDg1[0, 4] = "c_tipdoc";

            arrCabeceraDg1[1, 0] = "Nº Documento";
            arrCabeceraDg1[1, 1] = "100";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "";
            arrCabeceraDg1[1, 4] = "c_numdoc";

            arrCabeceraDg1[2, 0] = "Moneda";
            arrCabeceraDg1[2, 1] = "40";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "";
            arrCabeceraDg1[2, 4] = "c_desmon";

            arrCabeceraDg1[3, 0] = "Fch. Emision";
            arrCabeceraDg1[3, 1] = "70";
            arrCabeceraDg1[3, 2] = "F";
            arrCabeceraDg1[3, 3] = "";
            arrCabeceraDg1[3, 4] = "d_fchemi";

            arrCabeceraDg1[4, 0] = "Importe";
            arrCabeceraDg1[4, 1] = "80";
            arrCabeceraDg1[4, 2] = "D";
            arrCabeceraDg1[4, 3] = "0.00";
            arrCabeceraDg1[4, 4] = "n_importe";

            arrCabeceraDg1[5, 0] = "Saldo";
            arrCabeceraDg1[5, 1] = "80";
            arrCabeceraDg1[5, 2] = "D";
            arrCabeceraDg1[5, 3] = "0.00";
            arrCabeceraDg1[5, 4] = "n_saldo";

            arrCabeceraDg1[6, 0] = "Sel";
            arrCabeceraDg1[6, 1] = "40";
            arrCabeceraDg1[6, 2] = "B";
            arrCabeceraDg1[6, 3] = "0.00";
            arrCabeceraDg1[6, 4] = "n_sel";

            arrCabeceraDg1[7, 0] = "IdDoc";
            arrCabeceraDg1[7, 1] = "0";
            arrCabeceraDg1[7, 2] = "N";
            arrCabeceraDg1[7, 3] = "";
            arrCabeceraDg1[7, 4] = "n_id";

            Genericas xFun = new Genericas();

            xFun.Filtrar_CampoBusqueda = "n_id";
            xFun.Filtrar_CampoOrden = "c_numdoc";
            xFun.Filtrar_ColumnaBusqueda = 8;
            xFun.Filtrar_ColumnaCheck = 7;
            dtResult = xFun.Filtrar(arrCabeceraDg1, DtVentas);

            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }

            int n_row = 0;
            string c_dato = "";
            double n_valor = 0;

            booAgregando = true;
            for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;
                c_dato = dtResult.Rows[n_row]["c_numdoc"].ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 1, c_dato);

                c_dato = dtResult.Rows[n_row]["c_desmon"].ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 2, c_dato);

                c_dato = dtResult.Rows[n_row]["c_tipdoc"].ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 3, c_dato);

                c_dato = Convert.ToDateTime(dtResult.Rows[n_row]["d_fchemi"]).ToString("dd/MM/yyyy");
                FgItems.SetData(FgItems.Rows.Count - 1, 4, c_dato);

                c_dato = dtResult.Rows[n_row]["n_idmon"].ToString();
                if (c_dato == "151")
                {
                    c_dato = Convert.ToDouble(dtResult.Rows[n_row]["n_importe"]).ToString("0.00");
                    FgItems.SetData(FgItems.Rows.Count - 1, 5, c_dato);

                    FgItems.SetData(FgItems.Rows.Count - 1, 6, "0.00");
                }
                else
                {
                    c_dato = "0.00";
                    FgItems.SetData(FgItems.Rows.Count - 1, 5, c_dato);

                    c_dato = Convert.ToDouble(dtResult.Rows[n_row]["n_importe"]).ToString("0.00");
                    FgItems.SetData(FgItems.Rows.Count - 1, 6, c_dato);

                    //c_dato = (Convert.ToDouble(c_dato) * Convert.ToDouble(TxtTc.Text)).ToString("0.00");
                    //FgItems.SetData(FgItems.Rows.Count - 1, 6, c_dato);

                    n_valor = Convert.ToDouble(dtResult.Rows[n_row]["n_importe"]);
                    n_valor = ((n_valor * ((Convert.ToDouble(TxtTasRet.Text) / 100) + 1)) - n_valor);

                    FgItems.SetData(FgItems.Rows.Count - 1, 9, n_valor.ToString());
                }
                
                //c_dato = Convert.ToDouble(dtResult.Rows[n_row]["n_saldo"]).ToString("0.00");
                //FgItems.SetData(FgItems.Rows.Count - 1, 6, c_dato);

                //c_dato = Convert.ToDouble(dtResult.Rows[n_row]["n_saldo"]).ToString("0.00");
                //FgItems.SetData(FgItems.Rows.Count - 1, 7, c_dato);

                c_dato = TxtTasRet.Text;
                FgItems.SetData(FgItems.Rows.Count - 1, 8, c_dato);

                c_dato = Convert.ToDouble(dtResult.Rows[n_row]["n_id"]).ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 10, c_dato);
            }
            booAgregando = false;
            SumarColumnas();
        }
        private void FgItems_EnterCell(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }

            if ((FgItems.Col == 7) || (FgItems.Col == 6))
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
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }

            if ((e.Col == 7) || (e.Col == 6))
            {
                if (!c_Numerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }

            if (e.Col == 6)
            {
                string c_valor = FgItems.GetData(FgItems.Row, 6).ToString();
                FgItems.SetData(FgItems.Row, 7, Convert.ToDouble(c_valor).ToString("0.00"));
                Calcular(FgItems.Row);
            }
            if (e.Col == 7)
            {
                string c_valor = FgItems.GetData(FgItems.Row, 7).ToString();
                FgItems.SetData(FgItems.Row, 6, Convert.ToDouble(c_valor).ToString("0.00"));
                Calcular(FgItems.Row);
            }
            SumarColumnas();
        }
        void Calcular(int n_row)
        {
            double n_valor = Convert.ToDouble(FgItems.GetData(n_row, 7).ToString());
            double n_tasa = Convert.ToDouble(TxtTasRet.Text);
            n_valor = (n_valor * (n_tasa / 100));
            FgItems.SetData(n_row, 9, n_valor.ToString("0.00"));

        }
        void SumarColumnas()
        {
            double n_tot1 = funFlex.FlexSumarCol(FgItems, 6, 2, FgItems.Rows.Count - 1);
            double n_tot2 = funFlex.FlexSumarCol(FgItems, 9, 2, FgItems.Rows.Count - 1);

            TxtTot1.Text = n_tot1.ToString("0.00");
            TxtTot2.Text = n_tot2.ToString("0.00");
        }
        private void CmdDelDoc_Click(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count == 2) { return; }

            FgItems.RemoveItem(FgItems.Row);
        }

        private void CmdVerAsiento_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objConta = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objConta.mysConec = mysConec;
            objConta.STU_SISTEMA = STU_SISTEMA;
            objConta.VerAsiento(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboMeses.SelectedValue), 16, LblNumAsi.Text);
            objConta = null;
        }
        private void TxtNumRuc_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtNumRuc_Validated(object sender, EventArgs e)
        {
            DataTable dtresul = new DataTable();
            dtresul = funDatos.DataTableFiltrar(dtPro, "c_numdoc = '" + TxtNumRuc.Text + "'");

            if (dtresul.Rows.Count != 0)
            {
                TxtNumRuc.Text = dtresul.Rows[0]["c_numdoc"].ToString();
                TxtPro.Text = dtresul.Rows[0]["c_nombre"].ToString();
                LblIdPro.Text = dtresul.Rows[0]["n_id"].ToString();
            }
            else
            {
                TxtNumRuc.Text = "";
                TxtPro.Text = "";
                LblIdPro.Text = "";
                //TxtNumRuc.Focus();
            }
        }
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_NomArchivo = "";
            if (n_TipoRegistro == 1)
            {
                c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-CON-PERCEPCIONES-VENTAS-" + STU_SISTEMA.ANOTRABAJO.ToString() + Convert.ToInt16(CboMeses.SelectedValue).ToString("00") + ".xls";
            }
            else
            {
                c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-CON-PERCEPCIONES-COMPRAS-" + STU_SISTEMA.ANOTRABAJO.ToString() + Convert.ToInt16(CboMeses.SelectedValue).ToString("00") + ".xls";
            }
            DgLista.ExportTo(c_NomArchivo);
            MessageBox.Show("! Se exporto con exito la informacion en el archivo " + c_NomArchivo + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text != "")
            {
                string strCad = "0000000000" + TxtNumDoc.Text;
                TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
            }
        }
    }
}
