using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using SIAC_Entidades.Logistica;
using SIAC_Entidades.Ventas;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Sistema;
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
    public partial class FrmRegRetenciones : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_con_regretencion objRegistros = new CN_con_regretencion();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_con_retencion objRet = new CN_con_retencion();
        CN_sun_tipmon objMon = new CN_sun_tipmon();
        CN_mae_clipro objPro = new CN_mae_clipro();
        CN_con_tc objTC = new CN_con_tc();
        CN_con_doccomimp o_docimp = new CN_con_doccomimp();
        CN_con_tipdoccomcue o_doc = new CN_con_tipdoccomcue();
        
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
        DataTable dtRet = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtCompras = new DataTable();
        DataTable dtTc = new DataTable();
        DataTable dtdocimp = new DataTable();
        DataTable dtdoc = new DataTable();

        bool booAgregando;
        int N_CTADOC = 0;
        int N_CTAIGV = 0;
        SIAC_Objetos.Funciones objFunciones = new SIAC_Objetos.Funciones();

        BE_CON_REGRETENCION e_Ret = new BE_CON_REGRETENCION();
        List<BE_CON_DIARIO> l_diario = new List<BE_CON_DIARIO>();
        List<BE_CON_REGRETENCIONDET> l_RetDet = new List<BE_CON_REGRETENCIONDET>();
        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[9, 5];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[10, 5];

        string c_Numerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string c_NumerovalidosFE = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZEF1234567890." + (char)8;                                        // + (char)8;
        string c_Caracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;

        bool booSeEjecuto = false;

        public FrmRegRetenciones()
        {
            InitializeComponent();
        }

        private void FrmRegRetenciones_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void CargarCombos()
        {

            DataTableCargar();
            booAgregando = true;
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMes, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipRet, dtRet, "n_id", "c_des");
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

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            booAgregando = true;
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
            booAgregando = false;

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

            arrCabeceraFlex1[4, 0] = "Imp. Total";
            arrCabeceraFlex1[4, 1] = "80";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.00";
            arrCabeceraFlex1[4, 4] = "";

            arrCabeceraFlex1[5, 0] = "Imp. Cobrado";
            arrCabeceraFlex1[5, 1] = "80";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "";

            arrCabeceraFlex1[6, 0] = "Imp. Cobrado S/.";
            arrCabeceraFlex1[6, 1] = "80";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "";

            arrCabeceraFlex1[7, 0] = "% Tasa Retencion";
            arrCabeceraFlex1[7, 1] = "70";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "";

            arrCabeceraFlex1[8, 0] = "Imp. Retenido";
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
            DataTable dtres = new DataTable();

        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            dtLista = objRegistros.dtLista;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(66, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(66);

            objMon.mysConec = mysConec;
            dtMon = objMon.Listar();

            objRet.mysConec = mysConec;
            objRet.Listar();
            dtRet = objRet.dtLista;

            objPro.mysConec = mysConec;
            dtPro = objPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objMeses.mysConec = mysConec;
            dtMes = objMeses.Listar();

            objTC.mysConec = mysConec;
            objTC.Listar(0, 0);
            dtTc = objTC.dtLista;
            objTC = null;
                        
            o_doc.mysConec = mysConec;
            o_doc.Listar(STU_SISTEMA.EMPRESAID);
            dtdoc = o_doc.dtLista;
            o_doc = null;

            o_docimp.mysConec = mysConec;
            dtdocimp = o_docimp.Listar(STU_SISTEMA.EMPRESAID);
            o_docimp = null;
        }
        void ListarItems()
        {
            // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
            objRegistros.mysConec = mysConec;
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue));
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
            int n_row =0;
            string c_dato = "";
            DataTable dtDet = new DataTable();
            DataTable dtres = new DataTable();

            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdRegistro);
            e_Ret = objRegistros.e_Retencion;
            dtDet = objRegistros.dtDetalle;

            booAgregando = true;

            LblIdPro.Text = e_Ret.n_idcli.ToString();
            TxtPro.Text = funDatos.DataTableBuscar(dtPro, "n_id", "c_nombre", LblIdPro.Text, "N").ToString();
            TxtTc.Text = e_Ret.n_tc.ToString("0.000");
            TxtFchEmi.Text = e_Ret.d_fchemi.ToString();
            TxtNumSer.Text = e_Ret.c_numser;
            TxtNumDoc.Text = e_Ret.c_numdoc;
            CboMon.SelectedValue = e_Ret.n_idmon;
            TxtTc.Text = e_Ret.n_tc.ToString("0.000");
            CboTipRet.SelectedValue = e_Ret.n_idret;
            TxtTasRet.Text = e_Ret.n_tas.ToString("0.00");
            TxtGlo.Text = e_Ret.c_glo;
            LblNumAsi.Text = e_Ret.c_numreg;

            // OBTENEMOS EL ID DE LA CUENTA DEL DOCUMENTO
            dtres = funDatos.DataTableFiltrar(dtdoc, "n_idtipdoc = 21 AND n_idmon = " + Convert.ToInt16(CboMon.SelectedValue).ToString() + " AND n_idemp = " + STU_SISTEMA.EMPRESAID.ToString() + "");
            if (dtres.Rows.Count != 0)
            {
                N_CTADOC = Convert.ToInt32(dtres.Rows[0]["n_idcueven"]);
            }


            // OBTENEMOS EL ID DE LA CUENTA DEL IGV
            dtres = funDatos.DataTableFiltrar(dtdocimp, "n_idtipdoc = 21 AND n_idemp = " + STU_SISTEMA.EMPRESAID.ToString() + "");
            if (dtres.Rows.Count != 0)
            {
                N_CTAIGV = Convert.ToInt32(dtres.Rows[0]["n_idcueven"]);
            }

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

                c_dato =Convert.ToDateTime(dtDet.Rows[n_row]["d_fchdoc"]).ToString("dd/MM/yyyy");
                FgItems.SetData(FgItems.Rows.Count - 1, 4, c_dato);

                c_dato = Convert.ToDouble(dtDet.Rows[n_row]["n_imptotven"]).ToString("0.00");
                FgItems.SetData(FgItems.Rows.Count - 1, 5, c_dato);

                c_dato = Convert.ToDouble(dtDet.Rows[n_row]["n_impcob"]).ToString("0.00");
                FgItems.SetData(FgItems.Rows.Count - 1, 6, c_dato);

                c_dato = Convert.ToDouble(dtDet.Rows[n_row]["n_impcob"]).ToString("0.00");
                FgItems.SetData(FgItems.Rows.Count - 1, 7, c_dato);

                c_dato = TxtTasRet.Text;
                FgItems.SetData(FgItems.Rows.Count - 1, 8, c_dato);

                c_dato = Convert.ToDouble(dtDet.Rows[n_row]["n_impret"]).ToString("0.00");
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

            DataTable dtres = new DataTable();
            // OBTENEMOS EL ID DE LA CUENTA DEL DOCUMENTO
            dtres = funDatos.DataTableFiltrar(dtdoc, "n_idtipdoc = 21 AND n_idmon = " + Convert.ToInt16(CboMon.SelectedValue).ToString() + " AND n_idemp = " + STU_SISTEMA.EMPRESAID.ToString() + "");
            if (dtres.Rows.Count != 0)
            {
                N_CTADOC = Convert.ToInt32(dtres.Rows[0]["n_idcueven"]);
            }


            // OBTENEMOS EL ID DE LA CUENTA DEL IGV
            dtres = funDatos.DataTableFiltrar(dtdocimp, "n_idtipdoc = 21 AND n_idemp = " + STU_SISTEMA.EMPRESAID.ToString() + "");
            if (dtres.Rows.Count != 0)
            {
                N_CTAIGV = Convert.ToInt32(dtres.Rows[0]["n_idcueven"]);
            }
            TxtTc.Focus();
        }
        void Blanquea()
        {
            TxtPro.Text = "";
            LblIdPro.Text = "";
            TxtFchEmi.Text = "";
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            CboMon.SelectedValue = 0;
            CboTipRet.SelectedValue = 0;
            TxtTasRet.Text = "";
            TxtGlo.Text = "";
            FgItems.Rows.Count = 2;
        }
        void Bloquea()
        {
            CmdBusCli.Enabled = !CmdBusCli.Enabled;

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
            if (obj_fungen.EstadoPeriodo(n_IdEmpresa, n_IdMes, 7) == true)
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
            DataTable dtres = new DataTable();
            // OBTENEMOS EL ID DE LA CUENTA DEL DOCUMENTO
            dtres = funDatos.DataTableFiltrar(dtdoc, "n_idtipdoc = 21 AND n_idmon = " + Convert.ToInt16(CboMon.SelectedValue).ToString() + " AND n_idemp = " + STU_SISTEMA.EMPRESAID.ToString() + "");
            if (dtres.Rows.Count != 0)
            {
                N_CTADOC = Convert.ToInt32(dtres.Rows[0]["n_idcueven"]);
            }


            // OBTENEMOS EL ID DE LA CUENTA DEL IGV
            dtres = funDatos.DataTableFiltrar(dtdocimp, "n_idtipdoc = 21 AND n_idemp = " + STU_SISTEMA.EMPRESAID.ToString() + "");
            if (dtres.Rows.Count != 0)
            {
                N_CTAIGV = Convert.ToInt32(dtres.Rows[0]["n_idcueven"]);
            }
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
            objRegistros.l_Diario = l_diario;
            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(e_Ret ,l_RetDet);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(e_Ret, l_RetDet);
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
                e_Ret.n_id = 0;
            }
            else
            {
                e_Ret.n_id = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            }

            e_Ret.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Ret.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_Ret.n_mes = Convert.ToInt16(CboMeses.SelectedValue);

            e_Ret.n_idlib = 17;
            e_Ret.n_idtipdoc = 21;
            e_Ret.n_idret = Convert.ToInt16(CboTipRet.SelectedValue);
            e_Ret.n_tas = Convert.ToDouble(TxtTasRet.Text);
            e_Ret.n_tip = 1;
            e_Ret.n_idcli = Convert.ToInt16(LblIdPro.Text);
            e_Ret.c_numser = TxtNumSer.Text;
            e_Ret.c_numdoc = TxtNumDoc.Text;
            e_Ret.d_fchemi = Convert.ToDateTime(TxtFchEmi.Text);
            e_Ret.d_fchreg = Convert.ToDateTime(TxtFchEmi.Text);
            e_Ret.n_idmon = Convert.ToInt16(CboMon.SelectedValue);
            e_Ret.n_tc = Convert.ToDouble(TxtTc.Text);
            e_Ret.n_impret = Convert.ToDouble(TxtTot2.Text);
            e_Ret.c_glo = TxtGlo.Text;

            l_RetDet.Clear();
            int n_row=0;
            for (n_row=2;n_row<=FgItems.Rows.Count-1;n_row++)
            {
                BE_CON_REGRETENCIONDET e_RetDet = new BE_CON_REGRETENCIONDET();
                
                e_RetDet.n_idret = 0;
                e_RetDet.n_iddoc = Convert.ToInt32(FgItems.GetData(n_row, 10));
                e_RetDet.n_impcob = Convert.ToDouble(FgItems.GetData(n_row, 7));
                e_RetDet.n_impret = Convert.ToDouble(FgItems.GetData(n_row, 9));

                l_RetDet.Add(e_RetDet);
            }

            // ******************
            // CREAMOS EL ASIENTO
            string c_NumAsi = "";
            int n_idret = 0;
            BE_CON_DIARIO e_diario1 = new BE_CON_DIARIO();
            l_diario.Clear();

            CN_con_diario o_diario = new CN_con_diario();
            o_diario.mysConec = mysConec;
            if (n_QueHace == 1)
            {
                n_idret = 0;
                c_NumAsi = o_diario.ObtenerUltimoAsiento(STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 17, STU_SISTEMA.EMPRESAID);
            }
            else
            {
                n_idret = Convert.ToInt16(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
                c_NumAsi = LblNumAsi.Text;
            }

            e_diario1.n_id = 0;
            e_diario1.n_idemp = STU_SISTEMA.EMPRESAID;
            e_diario1.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_diario1.n_mes = Convert.ToInt16(CboMeses.SelectedValue);
            e_diario1.n_lib = 17;
            e_diario1.c_numasi = c_NumAsi;
            e_diario1.n_idcue = N_CTAIGV;
            e_diario1.n_tc = Convert.ToDouble(TxtTc.Text);
            if (Convert.ToInt16(CboMon.SelectedValue) == 115)
            {
                e_diario1.n_impdebsol = Convert.ToDouble(TxtTot2.Text);
                e_diario1.n_imphabsol = 0;
                e_diario1.n_impdebdol = (Convert.ToDouble(TxtTot2.Text) / Convert.ToDouble(TxtTc.Text));
                e_diario1.n_imphabdol = 0;
            }
            else
            {
                e_diario1.n_impdebsol = (Convert.ToDouble(TxtTot2.Text) * Convert.ToDouble(TxtTc.Text));
                e_diario1.n_imphabsol = 0;
                e_diario1.n_impdebdol = Convert.ToDouble(TxtTot2.Text);
                e_diario1.n_imphabdol = 0;
            }
            e_diario1.d_fchasi = Convert.ToDateTime(TxtFchEmi.Text);
            e_diario1.d_orifchdoc = Convert.ToDateTime(TxtFchEmi.Text);
            e_diario1.n_oriid = n_idret;
            e_diario1.n_oriidtipdoc = 21;
            e_diario1.n_oriidtipmon = Convert.ToInt16(CboMon.SelectedValue);
            e_diario1.c_orinumdoc = TxtNumSer.Text+"-"+TxtNumDoc.Text;
            e_diario1.c_origlo = TxtGlo.Text;
            e_diario1.c_oridestipmon = Convert.ToString(funDatos.DataTableBuscar(dtMon, "n_id", "c_simbolo", Convert.ToInt16(CboMon.SelectedValue).ToString(), "N")).ToString();
            e_diario1.c_oridestipdoc = Convert.ToString(funDatos.DataTableBuscar(dtdoc, "n_idtipdoc", "c_abr", "21", "N")).ToString();
            e_diario1.c_orinomcli = TxtPro.Text;
            e_diario1.c_orinumruc = Convert.ToString(funDatos.DataTableBuscar(dtPro, "n_id", "c_numdoc", LblIdPro.Text, "N")).ToString(); 
            l_diario.Add(e_diario1);

            double n_valor = 0;
            for (n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
            {
                BE_CON_DIARIO e_diario2 = new BE_CON_DIARIO();
                e_diario2.n_id = 0;
                e_diario2.n_idemp = STU_SISTEMA.EMPRESAID;
                e_diario2.n_ano = STU_SISTEMA.ANOTRABAJO;
                e_diario2.n_mes = Convert.ToInt16(CboMeses.SelectedValue);
                e_diario2.n_lib = 17;
                e_diario2.c_numasi = c_NumAsi;
                e_diario2.n_idcue = N_CTADOC;
                e_diario2.n_tc = Convert.ToDouble(TxtTc.Text);

                n_valor = Convert.ToDouble(FgItems.GetData(n_row, 9));
                if (Convert.ToInt16(CboMon.SelectedValue) == 115)
                {
                    e_diario2.n_impdebsol = 0;
                    e_diario2.n_imphabsol = n_valor;
                    e_diario2.n_impdebdol = 0;
                    e_diario2.n_imphabdol = (n_valor / Convert.ToDouble(TxtTc.Text));
                }
                else
                {
                    e_diario2.n_impdebsol = 0;
                    e_diario2.n_imphabsol = (n_valor * Convert.ToDouble(TxtTc.Text));
                    e_diario2.n_impdebdol = 0;
                    e_diario2.n_imphabdol = n_valor;
                }
                e_diario2.d_fchasi = Convert.ToDateTime(TxtFchEmi.Text);

                DateTime c_fechadoc = Convert.ToDateTime(FgItems.GetData(n_row, 4));
                int n_iddocori = Convert.ToInt32(FgItems.GetData(n_row, 10));
                BE_VTA_VENTAS e_venta = new BE_VTA_VENTAS();
                CN_vta_ventas o_ventas = new CN_vta_ventas();
                o_ventas.mysConec = mysConec;
                e_venta =  o_ventas.TraerRegistro(n_iddocori);
                

                e_diario2.d_orifchdoc = e_venta.d_fchdoc;
                e_diario2.n_oriid = Convert.ToInt32(e_venta.n_id);
                e_diario2.n_oriidtipdoc = e_venta.n_idtipdoc;
                e_diario2.n_oriidtipmon = e_venta.n_idmon;
                e_diario2.c_orinumdoc = e_venta.c_numser + "-" + e_venta.c_numdoc;
                e_diario2.c_origlo = e_venta.c_glosa;
                e_diario2.c_oridestipmon = Convert.ToString(funDatos.DataTableBuscar(dtMon, "n_id", "c_simbolo", e_venta.n_idmon.ToString(), "N")).ToString();
                e_diario2.c_oridestipdoc = Convert.ToString(funDatos.DataTableBuscar(dtdoc, "n_idtipdoc", "c_abr", e_venta.n_idtipdoc.ToString(), "N")).ToString();
                e_diario2.c_orinomcli = Convert.ToString(funDatos.DataTableBuscar(dtPro, "n_id", "c_nombre", e_venta.n_idcli.ToString(), "N")).ToString(); 
                e_diario2.c_orinumruc = Convert.ToString(funDatos.DataTableBuscar(dtPro, "n_id", "c_numdoc", e_venta.n_idcli.ToString(), "N")).ToString(); 
                l_diario.Add(e_diario2);
            }

        }
        bool CamposOK()
        {
            bool booEstado = true;
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
        private void FrmRegRetenciones_Activated(object sender, EventArgs e)
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
        private void FrmRegRetenciones_Resize(object sender, EventArgs e)
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
                    LblIdPro.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtPro.Text = dtResult.Rows[0]["c_nombre"].ToString();
                }
                else
                {
                    LblIdPro.Text = "";
                    TxtPro.Text = "";
                }
            }
        }
        private void TxtFchEmi_Validated(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (TxtFchEmi.Text != "")
            {
                DataTable dtResul = new DataTable();
                dtResul = funDatos.DataTableFiltrar(dtTc, "d_fecha = '" + TxtFchEmi.Text + "'");
                if (dtResul.Rows.Count != 0)
                {
                    TxtTc.Text = dtResul.Rows[0]["n_impven"].ToString();
                }
            }
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
                string strCad = "0000" + TxtNumSer.Text;

                TxtNumSer.Text = strCad.Substring(strCad.Length - 4, 4);
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
                TxtTasRet.Text = Convert.ToDouble(funDatos.DataTableBuscar(dtRet, "n_id", "n_tasa", Convert.ToInt16(CboTipRet.SelectedValue).ToString(), "N")).ToString("0.00");
            }
            else
            {
                TxtTasRet.Text = "0.00";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (LblIdPro.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el nombre del cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdBusCli.Focus();
                return ;
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

            MostrarDocVenta();
        }
        void MostrarDocVenta()
        {
            string[,] arrCabeceraDg1 = new string[8, 5];
            CN_vta_ventas objVenta = new CN_vta_ventas();
            DataTable DtVentas = new DataTable();
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            DataTable dtItemFiltro = new DataTable();
   
            objVenta.mysConec = mysConec;
            DtVentas = objVenta.DocumentosRetencion(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblIdPro.Text));
            
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
            arrCabeceraDg1[4, 2] = "N";
            arrCabeceraDg1[4, 3] = "0.00";
            arrCabeceraDg1[4, 4] = "n_importe";

            arrCabeceraDg1[5, 0] = "Saldo";
            arrCabeceraDg1[5, 1] = "80";
            arrCabeceraDg1[5, 2] = "N";
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

                c_dato = Convert.ToDouble(dtResult.Rows[n_row]["n_importe"]).ToString("0.00");
                FgItems.SetData(FgItems.Rows.Count - 1, 5, c_dato);

                c_dato = Convert.ToDouble(dtResult.Rows[n_row]["n_saldo"]).ToString("0.00");
                FgItems.SetData(FgItems.Rows.Count - 1, 6, c_dato);

                c_dato = Convert.ToDouble(dtResult.Rows[n_row]["n_saldo"]).ToString("0.00");
                FgItems.SetData(FgItems.Rows.Count - 1, 7, c_dato);

                c_dato = TxtTasRet.Text;
                FgItems.SetData(FgItems.Rows.Count - 1, 8, c_dato);

                n_valor = Convert.ToDouble(dtResult.Rows[n_row]["n_saldo"]);
                n_valor = (n_valor * (Convert.ToDouble(TxtTasRet.Text)/100));
                FgItems.SetData(FgItems.Rows.Count - 1, 9, n_valor.ToString());

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
            double n_tot1 = funFlex.FlexSumarCol(FgItems, 7, 2, FgItems.Rows.Count - 1);
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
            objConta.VerAsiento(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboMeses.SelectedValue), 17, LblNumAsi.Text);
            objConta = null;
        }

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-CON-RETENCION-" + STU_SISTEMA.ANOTRABAJO.ToString() + Convert.ToInt16(CboMeses.SelectedValue).ToString("00") + ".xls";
            DgLista.ExportTo(c_NomArchivo);
            MessageBox.Show("! Se exporto con exito la informacion en el archivo " + c_NomArchivo + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
    }
}
