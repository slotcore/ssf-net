using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Sistema;
using SIAC_Entidades.Contabilidad;
using SIAC_Entidades.Estacionamiento;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Estacionamiento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace SIAC_NET_Estacionamientos.Formularios
{
    public partial class FrmRegistroMovimientos : Form
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public bool b_GENERARASIENTO = false;                                  // INDICA SI SE GENERARA EL ASIENTO CONTABLE DE LA OPERACION

        BE_VTA_VENTAS e_Documento = new BE_VTA_VENTAS();
        List<BE_VTA_VENTASDET> l_DocumentoDet = new List<BE_VTA_VENTASDET>();
        List<BE_VTA_VENTASDOC> l_DetDoc = new List<BE_VTA_VENTASDOC>();
        List<BE_VTA_VENTASOCT> l_DetOCT = new List<BE_VTA_VENTASOCT>();
        List<BE_VTA_VENTASDAT> l_DetDat = new List<BE_VTA_VENTASDAT>();
        List<BE_CON_DIARIO> l_Diario = new List<BE_CON_DIARIO>();
        List<BE_EST_MOVIMIENTOSDET> l_movdet = new List<BE_EST_MOVIMIENTOSDET>();

        CN_mae_clipro objcli = new CN_mae_clipro();
        CN_sys_empresalocal o_locemp = new CN_sys_empresalocal();
        CN_sys_setup o_setup = new CN_sys_setup();
        SIAC_Objetos.Funciones objFunciones = new SIAC_Objetos.Funciones();
        CN_sys_empresalocal o_emploc = new CN_sys_empresalocal();
        CN_sun_tipdoccom objTipDocumento = new CN_sun_tipdoccom();
        CN_sun_tipmon objMoneda = new CN_sun_tipmon();
        CN_alm_inventariounimed objAlmUniMed = new CN_alm_inventariounimed();
        CN_sun_tipcam ObjTC = new CN_sun_tipcam();
        Cls_NumeroLetra funLet = new Cls_NumeroLetra();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_con_doccomimp o_docimp = new CN_con_doccomimp();
        CN_con_pcitems o_pcitem = new CN_con_pcitems();
        CN_con_impuestos o_imp = new CN_con_impuestos();
        CN_con_tipdoccomcue o_doccuecon = new CN_con_tipdoccomcue();

        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        DatosMySql funDatos = new DatosMySql();
        Genericas funGen = new Genericas();
        Helper.Comunes.Convertir funcon = new Helper.Comunes.Convertir();

        int intFilasCantidad = 0;
        double douIGVTasa = 0;
        bool booAgregando = false;
        string strTituloFormulario = "";
        string c_DatoEscan = "";

        DataTable dtLista = new DataTable();
        DataTable dtClientes = new DataTable();
        DataTable dtLocal = new DataTable();
        DataTable dtCajero = new DataTable();
        DataTable dtservicio = new DataTable();
        DataTable dtservicio2 = new DataTable();
        DataTable dtTipDocumento = new DataTable();
        DataTable dtMoneda = new DataTable();
        DataTable dtUnidadMedida = new DataTable();
        DataTable dtLocSet = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtTC = new DataTable();
        DataTable dtdocimp = new DataTable();
        DataTable dtpcite = new DataTable();
        DataTable dtimp = new DataTable();
        DataTable dtdoccuecon = new DataTable();
        DataTable dtsetup = new DataTable();

        // PARA ALMACENAR EL VALOR DE LA FACTURA
        double douPrecioTotal = 0;
        double douPrecioTotalSinIGV = 0;
        double douValorIGV = 0;
        string C_IDCAJERO;
        string C_IDLOCAL;
        int N_IDSERVICIODEFAULT = 0;
        int N_IDDOCUMENTODEFAULT = 0;
        int N_IMPRIMIRVALE = 0;                    // INDICA SI EL VALE SERA IMPRIMIDO POR LA PLAYA
        int N_VISTAPREVIA = 0;                     // INDICA SI SE MOSTRARA UNA VISTA PREVIA DE LA IMPRESION 1 = SIN VISTA PREVIA;  2 = VER VISTA PREVIA
        string c_NUMSERFAC = "";
        string c_NUMSERBOL = "";
        string c_NUMSERTIC = "";
        int N_TIPOCOBRANZA = 0;
        int N_TIPOVISTA = 1;                       //  1 = SOLO MUESTRA LOS MOVIMIENTOS PENDIENTES FINALIZACION :  2 = MUESTRA TODO LOSMOVIMIENTOS
        int N_TOLERA = 0;
        int N_APLFRA = 0;
        int N_IDSERHOR = 0;
        int N_IDSERABONADO = 0;
        double N_NUMHORTRA = 0;
        double N_DIFHOR = 0;
        double N_NUMHORADI = 0;
        string[,] arrCabecera1 = new string[13, 5];

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^-" + (char)8;
        string strCaracteres2 = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-" + (char)8;
        public FrmRegistroMovimientos()
        {
            InitializeComponent();
        }

        private void TooNuevo_Click(object sender, EventArgs e)
        {
            TxtNumPla.Text = "";
            LblIdCliente.Text = "";
            TxtCliente.Text = "";
            
            CboSer.SelectedValue = N_IDSERVICIODEFAULT;
            
            TxtHorIni.Text = "";
            TxtHorFin.Text = "";

            toolStrip1.Enabled = false;
            c1Sizer2.Enabled = false;
            Pan1.Left = ((this.Width - Pan1.Width) / 2);
            Pan1.Top = ((this.Height - Pan1.Height) / 2);
            Pan1.Visible = true;
            TxtNumPla.CharacterCasing = CharacterCasing.Upper;
            TxtNumPla.Focus();
        }
        private void FrmRegistroMovimientos_Load(object sender, EventArgs e)
        {
            //this.TxtPlaBus.TxtPlaBus.LostFocus += new EventHandler(TxtPlaBus_LostFocus);
            booAgregando = true;
            DataTableCargar();
            ConfigurarForm();
            CargarDatos();
            booAgregando = false;

            FgReg.Focus();
        }
        void CargarDatos()
        {
            TxtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
            objRegistro.STU_SISTEMA = STU_SISTEMA;
            objRegistro.Listar(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboLocal.SelectedValue), TxtFecha.Text);
            dtLista = objRegistro.dtListar;
            objRegistro = null;
            if (N_TIPOVISTA == 1)
            {
                dtLista = funGen.DataTableFiltrar(dtLista, "n_finalizado = 1");
            }
            dtLista = funGen.DataTableOrdenar(dtLista, "c_horini DESC");
        }
        void DataTableCargar()
        {
            CN_est_clientes o_cliente = new CN_est_clientes(STU_SISTEMA);
            o_cliente.STU_SISTEMA = STU_SISTEMA;
            o_cliente.Listar();                   // CARGAMOS TODOS LOS CLIENTES
            dtClientes = o_cliente.dtListar;
            o_cliente = null;

            CN_est_cajeros o_cajero = new CN_est_cajeros(STU_SISTEMA);
            o_cajero.STU_SISTEMA = STU_SISTEMA;
            o_cajero.Listar(STU_SISTEMA.EMPRESAID);
            dtCajero = o_cajero.dtListar;
            o_cajero = null;

            CN_est_servicios o_servicio = new CN_est_servicios(STU_SISTEMA);
            o_servicio.STU_SISTEMA = STU_SISTEMA;
            o_servicio.Listar(STU_SISTEMA.EMPRESAID);
            dtservicio = o_servicio.dtListar;
            o_servicio = null;

            CN_est_servicios o_servicio2 = new CN_est_servicios(STU_SISTEMA);
            o_servicio2.STU_SISTEMA = STU_SISTEMA;
            o_servicio2.Listar(STU_SISTEMA.EMPRESAID);
            dtservicio2 = o_servicio2.dtListar;
            o_servicio2 = null;

            CN_est_localsetup o_locset = new CN_est_localsetup(STU_SISTEMA);
            o_locset.STU_SISTEMA = STU_SISTEMA;
            dtLocSet = o_locset.Listar(STU_SISTEMA.EMPRESAID);
            o_locset = null;

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            o_emploc.mysConec = o_conec.mysConec;
            dtLocal = o_emploc.Listar(STU_SISTEMA.EMPRESAID, 0);
            
            objTipDocumento.mysConec = o_conec.mysConec;
            dtTipDocumento = objTipDocumento.Listar_puntoventa();       // CARGAMOS TIPOS DE DOCUMENTO PARA VENTAS

            objMoneda.mysConec = o_conec.mysConec;
            dtMoneda = objMoneda.Listar();                              // CARGAMOS TODAS MONEDAS

            objAlmUniMed.mysConec = o_conec.mysConec;
            dtUnidadMedida = objAlmUniMed.Listar();                     // CARGAMOS TODAS LAS UNIDADES DE MEDIDA

            ObjTC.mysConec = o_conec.mysConec;
            dtTC = ObjTC.Listartcano(151, STU_SISTEMA.ANOTRABAJO.ToString());

            o_docimp.mysConec = o_conec.mysConec;
            dtdocimp = o_docimp.Listar(STU_SISTEMA.EMPRESAID);

            o_pcitem.mysConec = o_conec.mysConec;
            o_pcitem.Listar(STU_SISTEMA.EMPRESAID, 2);
            dtpcite = o_pcitem.dtLista;

            o_imp.mysConec = o_conec.mysConec;
            o_imp.Listar(STU_SISTEMA.EMPRESAID);
            dtimp = o_imp.dtLista;

            o_doccuecon.mysConec = o_conec.mysConec;
            o_doccuecon.Listar(STU_SISTEMA.EMPRESAID);
            dtdoccuecon = o_doccuecon.dtLista;

            o_setup.mysConec = o_conec.mysConec;
            dtsetup = o_setup.TraerRegistro();
            if (Convert.ToInt32(dtsetup.Rows[0]["n_genasiconven"]) == 1)
            {
                b_GENERARASIENTO = false;
            }
            else
            {
                b_GENERARASIENTO = true;
            }
            o_setup = null;
            o_conec = null;
        }
        void ConfigurarForm()
        {
            string c_nomarc = @"C:\SSF-NET\estacionamiento.ini";
            //string N_IDEMPRESA = funGen.IniLeerSeccion(c_nomarc, "INFORMACION", "EMPRESA").ToString();
            C_IDLOCAL = funGen.IniLeerSeccion(c_nomarc, "INFORMACION", "LOCAL").ToString();
            C_IDCAJERO = funGen.IniLeerSeccion(c_nomarc, "INFORMACION", "CAJERO").ToString();
            N_IMPRIMIRVALE = Convert.ToInt32(funGen.IniLeerSeccion(c_nomarc, "INFORMACION", "IMPRIMIRVALE").ToString());

            this.Width = 857;
            this.Height = 623;
            
            if (N_TIPOVISTA == 1)
            {
                CmdVer.Text = "Ver Todos Ingresos";
            }
            else 
            {
                CmdVer.Text = "Ver Solo Pendientes";
            }
            
            arrCabecera1[0, 0] = "Nº Placa";
            arrCabecera1[0, 1] = "60";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_clinumpla";

            arrCabecera1[1, 0] = "Cliente";
            arrCabecera1[1, 1] = "240";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_clides";

            arrCabecera1[2, 0] = "Hora Ingreso";
            arrCabecera1[2, 1] = "60";
            arrCabecera1[2, 2] = "C";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "c_horini";

            arrCabecera1[3, 0] = "Hora Salida";
            arrCabecera1[3, 1] = "60";
            arrCabecera1[3, 2] = "C";
            arrCabecera1[3, 3] = "";
            arrCabecera1[3, 4] = "c_horfin";

            arrCabecera1[4, 0] = "Tiempo";
            arrCabecera1[4, 1] = "60";
            arrCabecera1[4, 2] = "C";
            arrCabecera1[4, 3] = "";
            arrCabecera1[4, 4] = "c_tieuit";

            arrCabecera1[5, 0] = "Importe";
            arrCabecera1[5, 1] = "60";
            arrCabecera1[5, 2] = "D";
            arrCabecera1[5, 3] = "0.00";
            arrCabecera1[5, 4] = "n_importe";

            arrCabecera1[6, 0] = "Tip. Doc.";
            arrCabecera1[6, 1] = "40";
            arrCabecera1[6, 2] = "C";
            arrCabecera1[6, 3] = "";
            arrCabecera1[6, 4] = "c_doctipdoc";

            arrCabecera1[7, 0] = "Nº Documento";
            arrCabecera1[7, 1] = "110";
            arrCabecera1[7, 2] = "C";
            arrCabecera1[7, 3] = "";
            arrCabecera1[7, 4] = "c_numdoc";

            arrCabecera1[8, 0] = "Id";
            arrCabecera1[8, 1] = "0";
            arrCabecera1[8, 2] = "N";
            arrCabecera1[8, 3] = "";
            arrCabecera1[8, 4] = "n_id";

            arrCabecera1[9, 0] = "Id Doc. venta";
            arrCabecera1[9, 1] = "0";
            arrCabecera1[9, 2] = "N";
            arrCabecera1[9, 3] = "";
            arrCabecera1[9, 4] = "n_iddocven";

            arrCabecera1[10, 0] = "Tipo Cliente";
            arrCabecera1[10, 1] = "70";
            arrCabecera1[10, 2] = "C";
            arrCabecera1[10, 3] = "";
            arrCabecera1[10, 4] = "c_tipclides";

            arrCabecera1[11, 0] = "Id Tipo Doc. Venta";
            arrCabecera1[11, 1] = "0";
            arrCabecera1[11, 2] = "N";
            arrCabecera1[11, 3] = "";
            arrCabecera1[11, 4] = "n_idtipdoc";

            arrCabecera1[12, 0] = "Fecha";
            arrCabecera1[12, 1] = "0";
            arrCabecera1[12, 2] = "F";
            arrCabecera1[12, 3] = "dd/MM/yyyy";
            arrCabecera1[12, 4] = "d_fchdoc";

            booAgregando = true;
            funFlex.FlexMostrarDatos(FgReg, arrCabecera1, dtLista, 2, true);
            FgReg.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

            funGen.ComboBoxCargarDataTable(CboLocal, dtLocal, "n_id", "c_des");
            CboLocal.SelectedValue = Convert.ToInt32(C_IDLOCAL);

            dtservicio = funGen.DataTableFiltrar(dtservicio, "n_idpla = " + Convert.ToInt32(CboLocal.SelectedValue) + "");
            funGen.ComboBoxCargarDataTable(CboSer, dtservicio, "n_id", "c_des");

            dtservicio2 = funGen.DataTableFiltrar(dtservicio2, "n_idpla = " + Convert.ToInt32(CboLocal.SelectedValue) + "");
            funGen.ComboBoxCargarDataTable(CboServ2, dtservicio2, "n_id", "c_des");
            
            DataTable dtResul = new DataTable();
            dtResul = funGen.DataTableFiltrar(dtCajero, "n_idloc = " + Convert.ToInt32(CboLocal.SelectedValue).ToString() + "");
            
            int n_row = 0;
            bool b_seencontro = false;
            int n_idusu = 0;
            for (n_row = 0; n_row <= dtResul.Rows.Count - 1; n_row++)
            {
                n_idusu = Convert.ToInt32(dtResul.Rows[n_row]["n_idusu"]);
                if (STU_SISTEMA.USUARIOID == n_idusu)
                {
                    C_IDCAJERO = dtResul.Rows[n_row]["n_id"].ToString();
                    b_seencontro = true;
                    break;
                }
            }

            if (b_seencontro == true)
            {
                ToolAnular.Visible = false;
                funGen.ComboBoxCargarDataTable(CboCajero, dtResul, "n_id", "c_cajapenom");
                CboCajero.SelectedValue = Convert.ToInt32(C_IDCAJERO);
            }
            else
            {
                if (STU_SISTEMA.USUARIOPERFIL != 10)
                {
                    TooNuevo.Visible = false;
                    ToolCliRapido.Visible = false;
                    ToolGrabar.Visible = false;
                    toolStripButton2.Visible = false;
                    ToolAnular.Visible = true;

                    CboLocal.Enabled = true;
                }
                //MessageBox.Show("¡ El usuario actual no pertenece a la playa" + CboLocal.Text + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //this.Close();
            }

            funGen.ComboBoxCargarDataTable(CboTipDoc, dtTipDocumento, "n_id", "c_des");
            funGen.ComboBoxCargarDataTable(CboMoneda, dtMoneda, "n_id", "c_des");
            CboMoneda.SelectedValue = 115;
            
            TxtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LblTc.Text = objFunciones.ObtenerTC(dtTC, DateTime.Now.ToString("dd/MM/yyyy")).ToString("0.000");
            if (Convert.ToDouble(LblTc.Text) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de cambio, defina el tipo cambio del dia para poder continuar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.Close();
            }

            string c_nomarc2 = ConfigurationManager.AppSettings["PathIniFile"];
            //N_IDALMACEN = Convert.ToInt32(funDatos.IniLeerSeccion(c_nomarc, "SISTEMA", "ALMACEN").ToString());
            c_NUMSERFAC = funGen.IniLeerSeccion(c_nomarc2, "SISTEMA", "SERFAC").ToString();
            c_NUMSERBOL = funGen.IniLeerSeccion(c_nomarc2, "SISTEMA", "SERBOL").ToString();
            c_NUMSERTIC = funGen.IniLeerSeccion(c_nomarc2, "SISTEMA", "SERVAL").ToString();

            dtLocSet = funGen.DataTableFiltrar(dtLocSet, "n_idloc = " + C_IDLOCAL + "");
            N_IDSERVICIODEFAULT = 0;
            if (dtLocSet.Rows.Count != 0)
            { 
                N_IDSERVICIODEFAULT = Convert.ToInt32(dtLocSet.Rows[0]["n_idserdef"]);
            }

            N_IDDOCUMENTODEFAULT = 0;
            if (dtLocSet.Rows.Count != 0)
            { 
                N_IDDOCUMENTODEFAULT = Convert.ToInt32(dtLocSet.Rows[0]["n_iddocdef"]);
            }

            N_VISTAPREVIA = 1;
            N_TIPOCOBRANZA = 1;
            N_TOLERA = 0;
            N_IDSERHOR = 0;
            if (dtLocSet.Rows.Count !=0)
            {
                N_VISTAPREVIA = Convert.ToInt32(dtLocSet.Rows[0]["n_vispre"]);
                N_TIPOCOBRANZA = Convert.ToInt32(dtLocSet.Rows[0]["n_idtipcob"]);
                N_TOLERA = Convert.ToInt32(dtLocSet.Rows[0]["n_tolmin"]);
                N_IDSERHOR = Convert.ToInt32(dtLocSet.Rows[0]["n_idserhor"]);
            }

            if (N_IDSERHOR == 0)
            {
                MessageBox.Show("¡ No ha definido el servicio x hora por default para esta playa, defina un servicio x default en el menu Setup Playa!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //this.Close();            
            }
            if (N_IDSERVICIODEFAULT != 0) { CboServ2.SelectedValue = N_IDSERVICIODEFAULT; }
            if (N_IDDOCUMENTODEFAULT != 0) { CboTipDoc.SelectedValue = N_IDDOCUMENTODEFAULT; }

            this.Text = "CONTROL ESTACIONAMIENTOS - REGISTRO DE OPERACIONES  v 5.23";
            douIGVTasa = 18;
            FgReg.AllowEditing = false;
            booAgregando = false;
        }
        private void CmdCan_Click(object sender, EventArgs e)
        {
            CmdAce.Visible = true;
            CmdImp.Visible = false;
            toolStrip1.Enabled = true;
            c1Sizer2.Enabled = true;
            FgReg.Focus();
            Pan1.Visible = false;

            TxtPlaBus.Text = "";
            TxtPlaBus.Focus();
            TxtPlaBus.BackColor = Color.YellowGreen;
        }
        private void CmdBusTra_Click(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            CargarDatos();
            dtResul = funGen.DataTableFiltrar(dtClientes, "n_idpla = " + C_IDLOCAL + "");
            LblTipCliente.Text = "";

            CN_est_clientes o_cliente = new CN_est_clientes(STU_SISTEMA);
            o_cliente.STU_SISTEMA = STU_SISTEMA;
            dtResul = o_cliente.BuscarCliente(dtResul, "n_id", "");
            o_cliente = null;
            
            if (dtResul != null)
            {
                N_IDSERABONADO = Convert.ToInt32(dtResul.Rows[0]["n_idser"]);
                TxtNumPla.Text = "";
                TxtCliente.Text = "";
                LblIdCliente.Text = "";

                if (dtResul.Rows.Count != 0)
                {
                    TxtNumPla.Text = dtResul.Rows[0]["c_numpla"].ToString();
                    TxtCliente.Text = dtResul.Rows[0]["c_nom"].ToString();
                    LblIdCliente.Text = dtResul.Rows[0]["n_id"].ToString();
                    TxtHorIni.Text = DateTime.Now.ToString("HH:mm:ss");
                    LblTipCliente.Text = dtResul.Rows[0]["n_idtipcli"].ToString();
                    if (LblTipCliente.Text == "2") { CboSer.Enabled = false; }

                    if (Convert.ToInt32(dtResul.Rows[0]["n_idtipcli"]) == 2)
                    {
                        CboSer.SelectedValue = Convert.ToInt32(dtResul.Rows[0]["n_idser"]);
                    }

                    DataTable dtResul2 = new DataTable();
                    dtResul2 = funGen.DataTableFiltrar(dtLista, "(c_clinumpla = '" + TxtNumPla.Text + "') AND (n_iddocven = 0)");

                    if (dtResul2.Rows.Count == 1)
                    {
                        MessageBox.Show("¡ Ya existe un ingreso sin cerrar para la placa Nº " + TxtNumPla.Text + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                        TxtNumPla.Text = "";
                        TxtCliente.Text = "";
                        LblIdCliente.Text = "";
                        TxtHorIni.Text = "";
                        TxtNumPla.Focus();
                        return;
                    }
                }
            }
        }
        private void CmdAce_Click(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(TxtNumPla.Text) == "") 
            {
                MessageBox.Show("¡ No ha especificado el cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumPla.Focus();
                return; 
            }
            if (Convert.ToInt32(CboSer.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboSer.Focus();
                return;
            }

            Grabar();
            TieneDeuda(Convert.ToInt32(LblIdCliente.Text));
            CmdCan_Click(sender, e);
        }
        void TieneDeuda(int n_IdAbonado)
        {
            CN_est_cargos o_cargo = new CN_est_cargos(STU_SISTEMA);
            o_cargo.STU_SISTEMA = STU_SISTEMA;
            if (o_cargo.AbonadoTieneDeuda(n_IdAbonado) == true)
            {
                MessageBox.Show("¡ El abonado tiene deuda pendiente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            o_cargo = null;
        }
        bool Modificar()
        {
            bool b_ok = false;
            BE_EST_MOVIMIENTOS e_Movimiento = new BE_EST_MOVIMIENTOS();

            e_Movimiento.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Movimiento.n_idloc = Convert.ToInt32(CboLocal.SelectedValue);
            e_Movimiento.n_id =  Convert.ToInt32(LblIdReg.Text);
            e_Movimiento.n_idcaj = Convert.ToInt32(CboCajero.SelectedValue);
            e_Movimiento.n_idcli = Convert.ToInt32(LblIdCliente2.Text);
            //e_Movimiento.d_fchdoc = Convert.ToDateTime(TxtFecha.Text);
            e_Movimiento.d_fchdoc = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            e_Movimiento.c_horini = TxtHorIni2.Text.Substring(11, 8);
            e_Movimiento.c_horfin = TxtHorFin2.Text.Substring(11, 8);
            e_Movimiento.c_tieuit = txtTieUti.Text;
            e_Movimiento.n_tieuti = funcon.HoraEnDecimal(txtTieUti.Text);
            e_Movimiento.n_idser = Convert.ToInt32(CboServ2.SelectedValue);
            e_Movimiento.c_numpla = TxtNumPla2.Text;
            e_Movimiento.n_finalizado = 2;
            bool b_Pagar = false;
            //TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text);
            
            if (funFunciones.NulosC(TxtNumDoc.Text) == "") { b_ok = false; return b_ok; }
            
            string c_dato = funGen.DataTableBuscar(dtClientes, "n_id", "n_idtipcli", LblIdCliente2.Text, "N").ToString();
            if ((c_dato == "1") || (c_dato == "0"))
            {
                e_Movimiento.n_impser = Convert.ToDouble(TxtImporte2.Text);
                e_Movimiento.n_importe = Convert.ToDouble(funFunciones.NulosN(TxtTotPag.Text));
                e_Movimiento.n_idtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);
                e_Movimiento.c_numdoc = TxtNumSer.Text + "-" + TxtNumDoc.Text;
                e_Movimiento.n_iddocven = 0;
                e_Movimiento.n_idtipcli = 1;
                b_Pagar = true;
            }
            else
            {
                e_Movimiento.n_impser = 0;
                e_Movimiento.n_importe = 0;
                e_Movimiento.n_idtipdoc = 0;
                e_Movimiento.c_numdoc = "";
                e_Movimiento.n_iddocven = 0;
                e_Movimiento.n_idtipcli = 2;
            }
            if (AsignarEntidad() == false)     // SI OCURRIO ALGUN ERROR 
            {
                b_ok = false;
                return b_ok;
            }

            CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
            objRegistro.STU_SISTEMA = STU_SISTEMA;

            objRegistro.e_Documento = e_Documento;
            objRegistro.l_DocumentoDet = l_DocumentoDet;
            objRegistro.l_DetDoc = l_DetDoc;
            objRegistro.l_DetOCT = l_DetOCT;
            objRegistro.l_DetDat = l_DetDat;
            objRegistro.l_Diario = l_Diario;
            objRegistro.l_movdet = l_movdet;

            DataTable dtRes = new DataTable();

            dtRes = funGen.DataTableFiltrar(dtservicio, "n_id = " + Convert.ToInt32(CboServ2.SelectedValue).ToString() + "");

            if (dtRes.Rows.Count != 0)
            {
                objRegistro.b_GENERARASIENTO = b_GENERARASIENTO;
                if (objRegistro.Actualizar(e_Movimiento, b_Pagar) == true)
                {
                    //MessageBox.Show("¡ El servicio se finalizo con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    if (b_Pagar == true)
                    { 
                        //int n_id = Convert.ToInt32(FgReg.GetData(FgReg.Row, 9));
                        //c_dato = TxtNumPla2.Text + "|" + TxtCliente2.Text + "|" + DateTime.Now.ToString("dd/MM/yyyy") + "|" + TxtHorIni2.Text + "|" + TxtHorFin2.Text + "|" + CboLocal.Text + "|" + CboCajero.Text + "|" + TxtNumDoc.Text + "-" + TxtNumDoc.Text + "|" + TxtTotPag.Text + "|" + CboTipDoc.Text;
                        //objRegistro.STU_SISTEMA = STU_SISTEMA;
                        //objRegistro.ImprimirComprobantePago(STU_SISTEMA.EMPRESAID, Convert.ToInt32(objRegistro.e_Documento.n_id), c_dato, Convert.ToInt32(CboTipDoc.SelectedValue), N_VISTAPREVIA);
                        objRegistro.STU_SISTEMA = STU_SISTEMA;

                        if (Convert.ToInt32(CboTipDoc.SelectedValue) == 90)
                        {
                            if (N_IMPRIMIRVALE == 1)
                            {
                                objRegistro.ImprimirComprobantePago(STU_SISTEMA.EMPRESAID, Convert.ToInt32(objRegistro.e_Documento.n_id), "", 0, N_VISTAPREVIA, 1);
                            }
                            else
                            { 
                                objRegistro.ImprimirComprobantePago(STU_SISTEMA.EMPRESAID, Convert.ToInt32(objRegistro.e_Documento.n_id), "", 0, 2, 1);
                            }
                        }
                        else
                        {  
                            objRegistro.ImprimirComprobantePago(STU_SISTEMA.EMPRESAID, Convert.ToInt32(objRegistro.e_Documento.n_id), "", 0, N_VISTAPREVIA, 1);
                        }
                    }
                    CargarDatos();
                    funFlex.FlexMostrarDatos(FgReg, arrCabecera1, dtLista, 2, true);
                    PintarFilas();
                    this.Focus();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo guardar el registro por el siguiente motivo : " + objRegistro.c_ErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return b_ok;                
                }
            }
            objRegistro = null;
            b_ok = true;
            return b_ok;
        }
        bool AsignarEntidad()
        {
            bool b_ok = false;

            DataTable dtresul = new DataTable();

            l_DocumentoDet.Clear();
            l_DetDoc.Clear();
            l_DetOCT.Clear();

            e_Documento.n_id = 0;
            e_Documento.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Documento.n_anotra = STU_SISTEMA.ANOTRABAJO;
            e_Documento.n_idmes = STU_SISTEMA.MESTRABAJO;
            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 90)
            {
                e_Documento.n_idlib = 33;
            }
            else
            { 
                e_Documento.n_idlib = 14;
            }
            e_Documento.c_numreg = "";
            e_Documento.n_idtippro = 23;
            e_Documento.n_idcli = Convert.ToInt32(LblIdCliente2.Text);
            e_Documento.n_idpunvencli = 0;
            e_Documento.n_idtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);
            e_Documento.c_numser = TxtNumSer.Text;
            e_Documento.c_numdoc = TxtNumDoc.Text;
            //e_Documento.c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text);

            if (e_Documento.c_numdoc == "")            // SI EL NUMERO DE DOCUMENTO ESTA EN BLANCO RETORNA FALSO SE CANCELA EL GUARDADO
            {
                b_ok = false;
            }
            TxtNumDoc.Text = e_Documento.c_numdoc;

            if (e_Documento.n_idmes == 0)
            {
                e_Documento.d_fchreg = Convert.ToDateTime("01/01/" + e_Documento.n_anotra.ToString("0000"));
            }
            else
            {
                //e_Documento.d_fchreg = Convert.ToDateTime("01/" + TxtFecha.Text.Substring(3, 2) + "/" + TxtFecha.Text.Substring(6, 4));
                string c_fecha = DateTime.Now.ToString("dd/MM/yyyy");
                e_Documento.d_fchreg = Convert.ToDateTime("01/" + c_fecha.Substring(3, 2) + "/" + c_fecha.Substring(6, 4));
            }
            //e_Documento.d_fchdoc = Convert.ToDateTime(TxtFecha.Text);
            //e_Documento.d_fchven = Convert.ToDateTime(TxtFecha.Text);
            e_Documento.d_fchdoc = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            e_Documento.d_fchven = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            e_Documento.n_idconpag = 1;
            e_Documento.n_idmon = Convert.ToInt32(CboMoneda.SelectedValue);
            e_Documento.n_impbru = (Convert.ToDouble(funFunciones.NulosN(TxtTotPag.Text)) / ((douIGVTasa / 100) + 1));
            e_Documento.n_impbru2 = 0;
            e_Documento.n_impbru3 = 0;
            e_Documento.n_impinaf = 0;
            e_Documento.n_impigv = (Convert.ToDouble(funFunciones.NulosN(TxtTotPag.Text)) - (Convert.ToDouble(funFunciones.NulosN(TxtTotPag.Text)) / ((douIGVTasa / 100) + 1)));
            e_Documento.n_impisc = 0;
            e_Documento.n_impotr = 0;
            e_Documento.n_imptotven = Convert.ToDouble(funFunciones.NulosN(TxtTotPag.Text));
            e_Documento.n_tc = Convert.ToDouble(LblTc.Text);
            e_Documento.n_impsal = Convert.ToDouble(funFunciones.NulosN(TxtTotPag.Text));
            e_Documento.n_idven = 0;
            e_Documento.n_tasaigv = douIGVTasa;
            //e_Documento.c_glosa = "ALQUILER DE ESTACIONAMIENTO DEL DIA " + TxtFecha.Text;
            e_Documento.c_glosa = "ALQUILER DE ESTACIONAMIENTO DEL DIA " + DateTime.Now.ToString("dd/MM/yyyy");
            e_Documento.n_impsubtot = (Convert.ToDouble(funFunciones.NulosN(TxtTotPag.Text)) / ((douIGVTasa / 100) + 1));
            e_Documento.n_pordsc = 0;
            e_Documento.n_idtipope = 1;

            e_Documento.n_idtipdocref = 0;
            e_Documento.n_iddocref = 0;

            e_Documento.c_serdocref = "";
            e_Documento.c_numdocref = "";

            string c_mon = "";
            if (Convert.ToDouble(CboMoneda.SelectedValue) == 115) { c_mon = "soles."; }
            if (Convert.ToDouble(CboMoneda.SelectedValue) == 151) { c_mon = "dolares americanos."; }
            e_Documento.c_numlet = funLet.Convertir(TxtTotPag.Text, true, c_mon);

            e_Documento.n_oriitem = 1;             // INDICAMOS QUE LA VENTA NO TIENE GUIA DE REMISION
            e_Documento.n_anulado = 0;
            e_Documento.c_motnc = "";

            e_Documento.n_idforpag = 1;            // INDICAMOS QUE LA FORMA DE PAGO ES EN EFECTIVO 
            e_Documento.n_idtarcre = 0;            // NO HAY TARJETA DE CREDITO

            // PREPARAMOS EL DETALLE DE LA VENTA
            int n_row=0;
            l_DocumentoDet.Clear();
            for (n_row = 0; n_row <= l_movdet.Count - 1; n_row++)
            { 
                BE_VTA_VENTASDET BE_Detalle = new BE_VTA_VENTASDET();

                BE_Detalle.n_idvta = e_Documento.n_id;
                BE_Detalle.n_canpro = l_movdet[n_row].n_can;

                //BE_Detalle.n_iditem = Convert.ToInt32(CboServ2.SelectedValue);
                BE_Detalle.n_iditem = l_movdet[n_row].n_idser;
                        
                dtresul = funGen.DataTableFiltrar(dtUnidadMedida, "n_idite = " + BE_Detalle.n_iditem.ToString() + "");
                BE_Detalle.n_idunimed = 0;
                if (dtresul.Rows.Count!=0)
                {
                    BE_Detalle.n_idunimed = Convert.ToInt32(dtresul.Rows[0]["n_id"]);
                }

                if (n_row == 0)
                {
                    //BE_Detalle.c_desusu = CboServ2.Text + " P Nº " + TxtNumPla2.Text + " HI : " + TxtHorIni2.Text + "   HF : " + TxtHorFin2.Text;
                    BE_Detalle.c_desusu = CboServ2.Text;
                }
                else
                { 
                    dtresul= funGen.DataTableFiltrar(dtservicio,"n_id = " + N_IDSERHOR.ToString() + "");
                    if (dtresul.Rows.Count!=0)
                    {
                        BE_Detalle.c_desusu = dtresul.Rows[0]["c_des"].ToString() + " X " + l_movdet[n_row].n_can.ToString("00") + " HORAS";
                    }
                }

                //BE_Detalle.n_preunibru = (Convert.ToDouble(TxtImporte2.Text) / ((douIGVTasa / 100) + 1));
                BE_Detalle.n_preunibru = l_movdet[n_row].n_preuni;
                //BE_Detalle.n_preuninet = (Convert.ToDouble(TxtImporte2.Text) / ((douIGVTasa / 100) + 1));
                BE_Detalle.n_preuninet = l_movdet[n_row].n_preuni;
                //BE_Detalle.n_imptot = (Convert.ToDouble(TxtTotPag.Text) / ((douIGVTasa / 100) + 1));
                BE_Detalle.n_imptot = l_movdet[n_row].n_preuni;
                BE_Detalle.n_idtipven = 0;
                BE_Detalle.n_pordsc = 0;
                BE_Detalle.n_porigv = douIGVTasa;
                //BE_Detalle.n_preuninetigv = Convert.ToDouble(TxtImporte2.Text);
                BE_Detalle.n_preuninetigv = l_movdet[n_row].n_imptot;
                //BE_Detalle.n_imptotigv = Convert.ToDouble(TxtTotPag.Text);
                BE_Detalle.n_imptotigv = l_movdet[n_row].n_imptot;
                BE_Detalle.n_idtipafeigv = 1;
                BE_Detalle.c_datadi = "";
                l_DocumentoDet.Add(BE_Detalle);
            }


            l_DetOCT.Clear();
            BE_VTA_VENTASOCT entOC = new BE_VTA_VENTASOCT();

            ////  1001 - Total valor de venta - operaciones gravadas
            entOC.n_idvta = 0;
            entOC.n_idcon = 1;
            entOC.n_importe = (Convert.ToDouble(TxtImporte2.Text) / ((douIGVTasa / 100) + 1));
            l_DetOCT.Add(entOC);

            l_DetDat.Clear();
            BE_VTA_VENTASDAT entDat = new BE_VTA_VENTASDAT();
            entDat.n_idvta = 0;
            entDat.n_idcaj = Convert.ToInt32(CboCajero.SelectedValue);
            entDat.c_cajnom = STU_SISTEMA.USUARIOALIAS; //CboCajero.Text;
            entDat.n_idloc = Convert.ToInt32(CboLocal.SelectedValue);
            entDat.c_locdes = CboLocal.Text;
            entDat.h_horemi = DateTime.Now.ToString("HH:mm:ss");
            entDat.c_numpla = TxtNumPla2.Text;
            entDat.c_horini = TxtHorIni2.Text;
            entDat.c_horfin= TxtHorFin2.Text;
            entDat.c_tiempousu = txtTieUti.Text;

            l_DetDat.Add(entDat);


            // ******************************************
            // CREAMOS LOS ASIENTOS CONTABLES DE LA VENTA
            string c_numasi = "";
            int n_idcuedoc = 0;
            int n_idcueigv = 0;
            int n_idcueite = 0;

            string c_tipdoc = "";
            string c_tipmon = "";
            string c_numruc = "";
            double n_valor = 0;

            dtresul =  funGen.DataTableFiltrar(dtMoneda,"n_id = "+ Convert.ToInt32(CboMoneda.SelectedValue) +"");
            if (dtresul.Rows.Count != 0)
            {
                c_tipmon = dtresul.Rows[0]["c_codsun"].ToString();
            }

            dtresul =  funGen.DataTableFiltrar(dtTipDocumento,"n_id = "+ Convert.ToInt32(CboTipDoc.SelectedValue) +"");
            if (dtresul.Rows.Count != 0)
            {
                c_tipdoc = dtresul.Rows[0]["c_abr"].ToString();
            }

            dtresul =  funGen.DataTableFiltrar(dtClientes,"n_id = "+ Convert.ToInt32(CboTipDoc.SelectedValue) +"");
            if (dtresul.Rows.Count != 0)
            {
                c_numruc = dtresul.Rows[0]["c_numdocide"].ToString();
            }

            dtresul = funGen.DataTableFiltrar(dtdoccuecon, "n_idtipdoc = " + Convert.ToInt32(CboTipDoc.SelectedValue) + " AND n_idmon = " + Convert.ToInt32(CboMoneda.SelectedValue) + "");
            if (dtresul.Rows.Count != 0)
            {
                n_idcuedoc = Convert.ToInt32(dtresul.Rows[0]["n_idcueven"]);
            }

            dtresul = funGen.DataTableFiltrar(dtdocimp, "n_idtipdoc = " + Convert.ToInt32(CboTipDoc.SelectedValue) + " ");
            if (dtresul.Rows.Count != 0)
            {
                n_idcueigv = Convert.ToInt32(dtresul.Rows[0]["n_idcueven"]);
            }

            dtresul = funGen.DataTableFiltrar(dtpcite, "n_iditem = " + Convert.ToInt32(CboServ2.SelectedValue) + "");
            if (dtresul.Rows.Count != 0)
            {
                n_idcueite = Convert.ToInt32(dtresul.Rows[0]["n_idpcven"]);
            }



            l_Diario.Clear();       
            BE_CON_DIARIO ediario = new BE_CON_DIARIO();

            //dtresul =  funGen.DataTableFiltrar(dtMoneda,"n_id = "+ Convert.ToInt32(CboMoneda.SelectedValue) +"");
            //if (dtresul.Rows.Count != 0) {n_idcue = Convert.ToInt32(dtresul.Rows[0][""]);}

            ediario.n_id = 0;
            ediario.n_idemp = STU_SISTEMA.EMPRESAID;
            ediario.n_ano = STU_SISTEMA.ANOTRABAJO;
            ediario.n_mes = STU_SISTEMA.MESTRABAJO;
            ediario.n_lib = 14;
            ediario.c_numasi = c_numasi;
            ediario.n_idcue = n_idcuedoc;
            ediario.n_tc = Convert.ToDouble(LblTc.Text);

            if (Convert.ToInt32(CboMoneda.SelectedValue) == 115)
            {
                ediario.n_impdebsol = Convert.ToDouble(funFunciones.NulosN(TxtTotPag.Text));
                ediario.n_imphabsol = 0;

                ediario.n_impdebdol = Convert.ToDouble(funFunciones.NulosN(TxtTotPag.Text)) / Convert.ToDouble(LblTc.Text);
                ediario.n_imphabdol = 0;
            }
            else
            {
                ediario.n_impdebsol = Convert.ToDouble(funFunciones.NulosN(TxtTotPag.Text)) * Convert.ToDouble(LblTc.Text);
                ediario.n_imphabsol = 0;

                ediario.n_impdebdol = Convert.ToDouble(funFunciones.NulosN(TxtTotPag.Text));
                ediario.n_imphabdol = 0;
            }
            
            //ediario.d_fchasi = Convert.ToDateTime(TxtFecha.Text);
            //ediario.d_orifchdoc = Convert.ToDateTime(TxtFecha.Text);
            ediario.d_fchasi = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            ediario.d_orifchdoc = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            ediario.n_oriid = 0;
            ediario.n_oriidtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);
            ediario.n_oriidtipmon = Convert.ToInt32(CboMoneda.SelectedValue);
            ediario.c_orinumdoc = TxtNumSer.Text +"-" + TxtNumDoc.Text;
            ediario.c_origlo = "";
            ediario.c_oridestipmon = c_tipmon;
            ediario.c_oridestipdoc = c_tipdoc;
            ediario.c_orinomcli = TxtCliente2.Text;
            ediario.c_orinumruc = c_numruc;

            l_Diario.Add(ediario);

            // *****************************
            // ESCRIBIMOS EL IGV DE LA VENTA
            if ((Convert.ToDouble(CboTipDoc.SelectedValue) == 2) || (Convert.ToDouble(CboTipDoc.SelectedValue) == 4))
            {
                BE_CON_DIARIO ediario2 = new BE_CON_DIARIO();
               
                n_valor = (Convert.ToDouble(funFunciones.NulosN(TxtTotPag.Text)) - (Convert.ToDouble(funFunciones.NulosN(TxtTotPag.Text)) / 1.18));

                ediario2.n_id = 0;
                ediario2.n_idemp = STU_SISTEMA.EMPRESAID;
                ediario2.n_ano = STU_SISTEMA.ANOTRABAJO;
                ediario2.n_mes = STU_SISTEMA.MESTRABAJO;
                ediario2.n_lib = 14;
                ediario2.c_numasi = c_numasi;
                ediario2.n_idcue = n_idcueigv;
                ediario2.n_tc = Convert.ToDouble(LblTc.Text);

                if (Convert.ToInt32(CboMoneda.SelectedValue) == 115)
                {
                    ediario2.n_impdebsol = 0;
                    ediario2.n_imphabsol = n_valor;

                    ediario2.n_impdebdol = 0;
                    ediario2.n_imphabdol = n_valor / Convert.ToDouble(LblTc.Text);
                }
                else
                {
                    ediario2.n_impdebsol = 0;
                    ediario2.n_imphabsol = n_valor * Convert.ToDouble(LblTc.Text);

                    ediario2.n_impdebdol = 0;
                    ediario2.n_imphabdol = n_valor;
                }

                //ediario2.d_fchasi = Convert.ToDateTime(TxtFecha.Text);
                //ediario2.d_orifchdoc = Convert.ToDateTime(TxtFecha.Text);
                ediario2.d_fchasi = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                ediario2.d_orifchdoc = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));

                ediario2.n_oriid = 0;
                ediario2.n_oriidtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);
                ediario2.n_oriidtipmon = Convert.ToInt32(CboMoneda.SelectedValue);
                ediario2.c_orinumdoc = TxtNumSer.Text + "-" + TxtNumDoc.Text;
                ediario2.c_origlo = "";
                ediario2.c_oridestipmon = c_tipmon;
                ediario2.c_oridestipdoc = c_tipdoc;
                ediario2.c_orinomcli = TxtCliente2.Text;
                ediario2.c_orinumruc = c_numruc;

                l_Diario.Add(ediario2);
            }

            // *******************************
            // ESCRIBIMOS EL HABER DEL ASIENTO
            BE_CON_DIARIO ediario3 = new BE_CON_DIARIO();

            if ((Convert.ToDouble(CboTipDoc.SelectedValue) == 2) || (Convert.ToDouble(CboTipDoc.SelectedValue) == 4))
            {
                n_valor = (Convert.ToDouble(funFunciones.NulosN(TxtTotPag.Text)) / 1.18);
            }
            else
            {
                n_valor = Convert.ToDouble(funFunciones.NulosN(TxtTotPag.Text));
            }
            

            ediario3.n_id = 0;
            ediario3.n_idemp = STU_SISTEMA.EMPRESAID;
            ediario3.n_ano = STU_SISTEMA.ANOTRABAJO;
            ediario3.n_mes = STU_SISTEMA.MESTRABAJO;
            ediario3.n_lib = 14;
            ediario3.c_numasi = c_numasi;
            ediario3.n_idcue = n_idcueite;
            ediario3.n_tc = Convert.ToDouble(LblTc.Text);

            if (Convert.ToInt32(CboMoneda.SelectedValue) == 115)
            {
                ediario3.n_impdebsol = 0;
                ediario3.n_imphabsol = n_valor;

                ediario3.n_impdebdol = 0;
                ediario3.n_imphabdol = n_valor / Convert.ToDouble(LblTc.Text);
            }
            else
            {
                ediario3.n_impdebsol = 0;
                ediario3.n_imphabsol = n_valor * Convert.ToDouble(LblTc.Text);

                ediario3.n_impdebdol = 0;
                ediario3.n_imphabdol = n_valor;
            }

            //ediario3.d_fchasi = Convert.ToDateTime(TxtFecha.Text);
            //ediario3.d_orifchdoc = Convert.ToDateTime(TxtFecha.Text);
            ediario3.d_fchasi = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            ediario3.d_orifchdoc = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            ediario3.n_oriid = 0;
            ediario3.n_oriidtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);
            ediario3.n_oriidtipmon = Convert.ToInt32(CboMoneda.SelectedValue);
            ediario3.c_orinumdoc = TxtNumSer.Text + "-" + TxtNumDoc.Text;
            ediario3.c_origlo = "";
            ediario3.c_oridestipmon = c_tipmon;
            ediario3.c_oridestipdoc = c_tipdoc;
            ediario3.c_orinomcli = TxtCliente2.Text;
            ediario3.c_orinumruc = c_numruc;

            l_Diario.Add(ediario3);
            b_ok = true;
            return b_ok;
        }
        void Grabar()
        { 
            BE_EST_MOVIMIENTOS e_Movimiento = new BE_EST_MOVIMIENTOS();
            DataTable dtresul = new DataTable();
            string c_dato = "";

            e_Movimiento.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Movimiento.n_idloc = Convert.ToInt32(CboLocal.SelectedValue);
            e_Movimiento.n_id = 0;
            e_Movimiento.n_idcaj = Convert.ToInt32(CboCajero.SelectedValue);
            e_Movimiento.n_idcli = Convert.ToInt32(LblIdCliente.Text);
            //e_Movimiento.d_fchdoc = Convert.ToDateTime(TxtFecha.Text);
            e_Movimiento.d_fchdoc = Convert.ToDateTime(DateTime.Now);
            e_Movimiento.c_horini = TxtHorIni.Text;
            e_Movimiento.c_horfin = "";
            e_Movimiento.c_tieuit = "";
            e_Movimiento.n_tieuti = 0;
            e_Movimiento.n_idser = Convert.ToInt32(CboSer.SelectedValue);
            e_Movimiento.n_importe = 0;
            e_Movimiento.c_numpla = TxtNumPla.Text;
            e_Movimiento.n_finalizado = 1;

            c_dato = funGen.DataTableBuscar(dtClientes, "n_id", "n_idtipcli", LblIdCliente.Text, "N").ToString();
            if (c_dato == "0") { c_dato = "1"; }
            e_Movimiento.n_idtipcli = Convert.ToInt32(c_dato);

            dtresul = funGen.DataTableFiltrar(dtservicio,"n_id = " + Convert.ToInt32(CboSer.SelectedValue) + "");
            e_Movimiento.n_impser = 0;
            if (dtservicio.Rows.Count!=0)
            {
                e_Movimiento.n_impser = Convert.ToDouble(dtresul.Rows[0]["n_imptot"]);
            }

            e_Movimiento.n_idtipdoc = 0;
            e_Movimiento.c_numdoc = "";
            e_Movimiento.n_iddocven = 0;

            CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
            objRegistro.STU_SISTEMA = STU_SISTEMA;

            if (objRegistro.Insertar(e_Movimiento) == true)
            {
                //MessageBox.Show("¡ El servicio se guardo con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                //c_dato = TxtNumPla.Text + "|" + TxtCliente.Text + "|" + DateTime.Now.ToString("dd/MM/yyyy") + "|" + TxtHorIni.Text + "|" + CboLocal.Text + "|" + CboCajero.Text;//
                objRegistro.STU_SISTEMA = STU_SISTEMA;

                if (e_Movimiento.n_idtipcli == 1) 
                {
                    objRegistro.ImprimirTicket(objRegistro.n_IdRegistro, "", N_VISTAPREVIA);
                }

                CargarDatos();
                funFlex.FlexMostrarDatos(FgReg, arrCabecera1, dtLista, 2, true);
                PintarFilas();
            }
            else 
            {
                MessageBox.Show("¡ No se pudo guardar el registro por el siguiente motivo : " + objRegistro.c_ErrorMensaje  + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            objRegistro = null;
        }
        private void TxtNumPla_TextChanged(object sender, EventArgs e)
        {

        }
        private void TxtNumPla_Validated(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(TxtNumPla.Text).ToString() == "") { return; }
            LblTipCliente.Text = "";
            CargarDatos();
            DataTable dtResul = new DataTable();
            dtResul = funGen.DataTableFiltrar(dtLista, "(c_clinumpla = '" + TxtNumPla.Text + "') AND (n_finalizado = 1)");

            if (dtResul.Rows.Count == 1)
            {
                if (dtResul.Rows[0]["c_clides"].ToString() != "ANULADO")
                { 
                    MessageBox.Show("¡ Ya existe un ingreso sin cerrar para la placa Nº " + TxtNumPla.Text + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    TxtNumPla.Text = "";
                    TxtNumPla.Focus();
                    return;
                }
            }
            CboSer.Enabled = true;
            DataTable dtresul = new DataTable();
            dtresul = funGen.DataTableFiltrar(dtClientes, "c_numpla = '" + TxtNumPla.Text + "' AND n_idpla = " + C_IDLOCAL + "");

            if (dtresul.Rows.Count != 0)
            {
                TxtNumPla.Text = dtresul.Rows[0]["c_numpla"].ToString();
                TxtCliente.Text = dtresul.Rows[0]["c_nom"].ToString();
                LblIdCliente.Text = dtresul.Rows[0]["n_id"].ToString();
                TxtHorIni.Text = DateTime.Now.ToString("HH:mm:ss");
                N_IDSERABONADO = Convert.ToInt32(dtresul.Rows[0]["n_idser"]);
                if (Convert.ToInt32(dtresul.Rows[0]["n_idtipcli"]) == 2)
                {
                    CboSer.SelectedValue = Convert.ToInt32(dtresul.Rows[0]["n_idser"]);
                    CboSer.Enabled = false;
                }
            }
            else
            {
                //MessageBox.Show("¡ El numero de placa no es un abonado, se le asignara como cliente regular  !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboSer.SelectedValue = N_IDSERHOR;
                CboSer.Enabled = true;
                TxtCliente.Text = "DIVERSOS";   // LE ASIGNAMOS EL CODIGO DEL CLIENTE REGUKLAR
                LblIdCliente.Text = "1";
                TxtHorIni.Text = DateTime.Now.ToString("HH:mm:ss");
                CmdAce.Focus();
            }
        }
        private void TxtNumPla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strCaracteres2.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtHorIni_KeyPress(object sender, KeyPressEventArgs e)
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
        private void FrmRegistroMovimientos_Activated(object sender, EventArgs e)
        {
            funFlex.FlexMostrarDatos(FgReg, arrCabecera1, dtLista, 2, true);
            PintarFilas();
            TxtPlaBus.Focus();
        }
        private void ToolSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            if (FgReg.Rows.Count == 2)
            {
                MessageBox.Show("¡ No hay ingresos para finalizar, debe de agregar al menos un ingreso !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgReg.Focus();
                return;
            }
            if (funFunciones.NulosC(FgReg.GetData(FgReg.Row, 2)) == "ANULADO")
            {
                MessageBox.Show("¡ El ingreso fue anulado no se puede cobrar un ingreso anulado, seleccione otro!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgReg.Focus();
                return;
            }
            if (funFunciones.NulosC(FgReg.GetData(FgReg.Row, 8)) != "")
            {
                MessageBox.Show("¡ El ingreso ya fue cerrado, seleccione otro!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgReg.Focus();
                return;
            }
            int n_id = Convert.ToInt32(FgReg.GetData(FgReg.Row, 9).ToString());
            string c_plca = FgReg.GetData(FgReg.Row, 1).ToString();
            int n_row = 0;
            bool b_econtro = false;

            CargarDatos();
            funFlex.FlexMostrarDatos(FgReg, arrCabecera1, dtLista, 2, true);
            PintarFilas();

            for (n_row = 2; n_row <= FgReg.Rows.Count - 1; n_row++)
            {
                if (n_id == Convert.ToInt32(FgReg.GetData(n_row, 9).ToString()))
                {
                    b_econtro = true;
                }
            }

            if (b_econtro == false)
            {
                MessageBox.Show("¡ El ingreso seleccionado ya fue cancelado, verifique que haya sido cancelado haciendo clic en el boton Ver Todos los Ingresos !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            toolStrip1.Enabled = false;
            c1Sizer2.Enabled = false;
            Pan2.Left = ((this.Width - Pan2.Width) / 2);
            Pan2.Top = ((this.Height - Pan2.Height) / 2);
            
            BlanqueaPanel2();
            
            MostrarDetalle(n_id, c_plca);
            Pan2.Visible = true;
            TxtAbono.Focus();
        }
        void BlanqueaPanel2()
        {
            TxtNumPla2.Text = "";
            TxtCliente2.Text = "";
            //CboServ2.SelectedValue = 0;
            TxtImporte2.Text = "";
            TxtHorIni2.Text = "";
            TxtHorFin2.Text = "";
            txtTieUti.Text = "";
            TxtTotPag.Text = "";
            //CboTipDoc.SelectedValue = 0;
            CboServ2.SelectedValue = N_IDSERVICIODEFAULT;
            CboTipDoc.SelectedValue = N_IDDOCUMENTODEFAULT;
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtAbono.Text = "";
            TxtVuelto.Text = "";
        }
        void MostrarDetalle(int n_Idregistro, string c_plca)
        {
            int N_APLTOLFRA = 0;
            BE_EST_MOVIMIENTOS e_Movimiento = new BE_EST_MOVIMIENTOS();
            double n_tiempo = 0;
            N_NUMHORADI = 0;

            CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
            objRegistro.STU_SISTEMA = STU_SISTEMA;

            objRegistro.TraerRegistro(n_Idregistro);
            e_Movimiento = objRegistro.e_Movimiento;
            if (objRegistro.b_OcurrioError == false)
            {
                LblIdCliente2.Text = e_Movimiento.n_idcli.ToString();
                TxtNumPla2.Text = c_plca;     // funGen.DataTableBuscar(dtClientes, "n_id", "c_numpla", LblIdCliente2.Text, "N").ToString();
                LblIdReg.Text = e_Movimiento.n_id.ToString();
                if (LblIdCliente2.Text == "1")
                {
                    CmdAddPer.Visible = true;
                    CmdAddEmp.Visible = true;
                    TxtCliente2.Text = "DIVERSOS";
                }
                else
                {
                    TxtCliente2.Text = funGen.DataTableBuscar(dtClientes, "n_id", "c_nom", LblIdCliente2.Text, "N").ToString();
                }
                CboServ2.SelectedValue = e_Movimiento.n_idser;
                TxtHorIni2.Text = e_Movimiento.d_fchdoc.ToString("dd/MM/yyyy") + " " + e_Movimiento.c_horini;
                TxtHorFin2.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                TxtImporte2.Text = e_Movimiento.n_impser.ToString("0.00");
                //txtTieUti.Text = funFunciones.HorasRestar(TxtHorIni2.Text, TxtHorFin2.Text);

                txtTieUti.Text = funFunciones.Horas_IntervaloDias(TxtHorIni2.Text, TxtHorFin2.Text);
                txtTieUti.Text = (txtTieUti.Text.Substring(0,5)+":00");
                string c_tolera = "00:"+ N_TOLERA.ToString("00") + ":00";
                double n_tolera = funcon.HoraEnDecimal(c_tolera);
                n_tiempo = funcon.HoraEnDecimal(txtTieUti.Text.Substring(0,5)+":00");  //  -funcon.HoraEnDecimal(c_tolera);
                                
                DataTable dtRes = new DataTable();
                dtRes = funGen.DataTableFiltrar(dtservicio, "n_id = " + Convert.ToInt32(CboServ2.SelectedValue).ToString() + "");

                l_movdet.Clear();
                BE_EST_MOVIMIENTOSDET e_movdet = new BE_EST_MOVIMIENTOSDET();
                e_movdet.n_idmov = 0;
                e_movdet.n_idser = Convert.ToInt32(CboServ2.SelectedValue);
                e_movdet.n_impbru = 0;
                e_movdet.n_impigv = 0;
                e_movdet.n_imptot = 0;
                e_movdet.n_idunimed = 0;
                e_movdet.n_can = 1;
                e_movdet.n_preuni = (Convert.ToDouble(TxtImporte2.Text) /1.18);
                l_movdet.Add(e_movdet);

                N_APLFRA = Convert.ToInt32(dtRes.Rows[0]["n_aplfra"]);
                N_APLTOLFRA = Convert.ToInt32(dtRes.Rows[0]["n_apltolmedpag"]);
                int n_idtipcal = Convert.ToInt32(dtRes.Rows[0]["n_idcal"]);
                double N_HORASER = Convert.ToInt32(funFunciones.NulosN(dtRes.Rows[0]["n_numhorser"]));                               // ALMACENA LAS HORAS DEL SERVICIO
                double n_valserhor = 0;

                // TRAEMOS LOS DATOS DEL SERVICIO POR HORA ESTO SOLO PARA SUMARLE A LOS SERVICIOS QUE SEAN POR LAPSO DE TIEMPO
                DataTable dtResHor = new DataTable();
                dtResHor = funGen.DataTableFiltrar(dtservicio, "n_id = " + N_IDSERHOR.ToString() + "");
                if (dtResHor.Rows.Count != 0) { n_valserhor = Convert.ToDouble(dtResHor.Rows[0]["n_imptot"]); }

                string c_horini = funFunciones.NulosC(dtRes.Rows[0]["c_horini"]);
                string c_horfin = funFunciones.NulosC(dtRes.Rows[0]["c_horfin"]);
                int n_AplicaMedioPago = Convert.ToInt32(funFunciones.NulosN(dtRes.Rows[0]["n_aplfra"]));

                if (n_idtipcal == 1)     // INDICA QUE EL VALOR DEL SERVICIO SE CALCULAR EN FUNCION A LA HORA TRANSCURRIDA POR EL PRECIO
                {
                    double n_numhorser = 0;
                    double n_valadi = 0;

                    dtRes = funGen.DataTableFiltrar(dtservicio, "n_id = " + Convert.ToInt32(CboServ2.SelectedValue).ToString() + "");

                    if (dtRes.Rows.Count != 0)
                    {
                        if (Convert.ToInt32(dtRes.Rows[0]["n_pagser"]) == 2)    // PREGUNTAMOS SI EL SERVICIO SE PAGA
                        {
                            if ((c_horini != "") && (c_horfin != ""))                             // EL SERVICIO TIENE UNA HORA DE INICIO Y UNA HORA DE TERMINO
                            {
                                c_horini = dtRes.Rows[0]["c_horini"].ToString() + ":00";
                                c_horfin = dtRes.Rows[0]["c_horfin"].ToString() + ":00";

                                string[] ret = CalcularDiaNoche(c_horini, c_horfin, TxtHorFin2.Text, TxtHorIni2.Text.Substring(0, 10), n_tolera, n_AplicaMedioPago);
                                //string[] ret = CalcularDiaNoche("19:00", "09:00", "29/01/2019 09:09", "28/01/2019", n_tolera, n_AplicaMedioPago);
                                txtTieUti.Text = funcon.DecimalEnHoras(Convert.ToDouble(ret[2].ToString()));

                                if (Convert.ToInt32(ret[0]) == 1) 
                                { 
                                    // ESCRIBIMOS LOS VALORES DEL SERVICIO EN EL DETALLE
                                    l_movdet[0].n_idser = Convert.ToInt32(dtRes.Rows[0]["n_id"]);
                                    l_movdet[0].n_imptot = Convert.ToDouble(dtRes.Rows[0]["n_imptot"]);
                                    l_movdet[0].n_impigv = Convert.ToDouble(dtRes.Rows[0]["n_imptot"]) - (Convert.ToDouble(dtRes.Rows[0]["n_imptot"]) / 1.18);
                                    l_movdet[0].n_impbru = (Convert.ToDouble(dtRes.Rows[0]["n_imptot"]) / 1.18);
                                    l_movdet[0].n_can = 1;
                                    l_movdet[0].n_idunimed = 0;
                                    l_movdet[0].n_preuni = (Convert.ToDouble(dtRes.Rows[0]["n_imptot"]) / 1.18);
                                    TxtTotPag.Text = Convert.ToDouble(dtRes.Rows[0]["n_imptot"]).ToString();
                                    //txtTieUti.Text = funcon.DecimalEnHoras(Convert.ToDouble(ret[0].ToString()));
                                }
                                if (Convert.ToDouble(ret[1]) >= n_tolera)                         // SI HAY TIEMPO ADICIONAL LO AGREGAMOS ALA LISTA DETALLE
                                {
                                    N_NUMHORADI = Convert.ToDouble(ret[1]);

                                    if (N_TIPOCOBRANZA == 3)
                                    {
                                        n_valadi = HallarPrecio(Convert.ToDouble(ret[2]), n_valserhor);
                                    }
                                    else
                                    { 
                                        n_valadi = (n_valserhor * N_NUMHORADI);
                                    }
                                    BE_EST_MOVIMIENTOSDET e_movdet2 = new BE_EST_MOVIMIENTOSDET();
                                    e_movdet2.n_idmov = 0;
                                    e_movdet2.n_idser = N_IDSERHOR;
                                    e_movdet2.n_impbru = ((n_valadi) / 1.18);
                                    e_movdet2.n_impigv = (n_valadi - ((n_valadi) / 1.18));
                                    e_movdet2.n_imptot = (n_valadi);
                                    e_movdet2.n_idunimed = 0;
                                    e_movdet2.n_can = N_NUMHORADI;
                                    e_movdet2.n_preuni = (n_valserhor / 1.18);
                                    l_movdet.Add(e_movdet2);

                                    TxtTotPag.Text = (Convert.ToDouble(TxtTotPag.Text) + e_movdet2.n_imptot).ToString("0.00");
                                    //txtTieUti.Text = funcon.DecimalEnHoras(Convert.ToDouble(ret[2].ToString()));
                                    //HallarPrecio(n_tiempo, Convert.ToDouble(TxtImporte2.Text)).ToString("0.00");
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(funFunciones.NulosN(dtRes.Rows[0]["n_numhorser"])) != 0)
                                {
                                    c_horini = TxtHorIni2.Text;
                                    c_horfin = Convert.ToDateTime(c_horini).AddHours(N_HORASER).ToString("dd/MM/yyyy HH:mm:ss");

                                    string[] ret = HallarTiempoTar24(c_horini, c_horfin, TxtHorFin2.Text, n_tolera, n_AplicaMedioPago, N_HORASER);

                                    if (Convert.ToInt32(ret[0]) == 1)
                                    {
                                        // ESCRIBIMOS LOS VALORES DEL SERVICIO EN EL DETALLE
                                        l_movdet[0].n_idser = Convert.ToInt32(dtRes.Rows[0]["n_id"]);
                                        l_movdet[0].n_imptot = Convert.ToDouble(dtRes.Rows[0]["n_imptot"]);
                                        l_movdet[0].n_impigv = Convert.ToDouble(dtRes.Rows[0]["n_imptot"]) - (Convert.ToDouble(dtRes.Rows[0]["n_imptot"]) / 1.18);
                                        l_movdet[0].n_impbru = (Convert.ToDouble(dtRes.Rows[0]["n_imptot"]) / 1.18);
                                        l_movdet[0].n_can = 1;
                                        l_movdet[0].n_idunimed = 0;
                                        l_movdet[0].n_preuni = (Convert.ToDouble(dtRes.Rows[0]["n_imptot"]) / 1.18);
                                        TxtTotPag.Text = Convert.ToDouble(dtRes.Rows[0]["n_imptot"]).ToString();
                                    }
                                    if (Convert.ToDouble(ret[1]) >= n_tolera)                         // SI HAY TIEMPO ADICIONAL LO AGREGAMOS ALA LISTA DETALLE
                                    {
                                        N_NUMHORADI = Convert.ToDouble(ret[1]);
                                        n_valadi = (n_valserhor * N_NUMHORADI);
                                        BE_EST_MOVIMIENTOSDET e_movdet2 = new BE_EST_MOVIMIENTOSDET();
                                        e_movdet2.n_idmov = 0;
                                        e_movdet2.n_idser = N_IDSERHOR;
                                        e_movdet2.n_impbru = ((n_valadi) / 1.18);
                                        e_movdet2.n_impigv = (n_valadi - ((n_valadi) / 1.18));
                                        e_movdet2.n_imptot = (n_valadi);
                                        e_movdet2.n_idunimed = 0;
                                        e_movdet2.n_can = N_NUMHORADI;
                                        e_movdet2.n_preuni = (n_valserhor / 1.18);
                                        l_movdet.Add(e_movdet2);

                                        TxtTotPag.Text = (Convert.ToDouble(TxtTotPag.Text) + e_movdet2.n_imptot).ToString("0.00");
                                    }
                                }
                                else
                                { 
                                    n_numhorser = 0;
                                }
                            }

                            //if (Convert.ToDateTime(TxtHorIni2.Text).Day == Convert.ToDateTime(TxtHorFin2.Text).Day)
                            //{
                            //    if (c_horfin == "")
                            //    {
                            //        N_NUMHORTRA = 0;
                            //    }
                            //    else
                            //    {
                            //        if (Convert.ToDateTime(TxtHorFin2.Text.Substring(11, 8)) < Convert.ToDateTime(c_horfin))
                            //        {
                            //            N_NUMHORTRA = 0;
                            //        }
                            //        else
                            //        {
                            //            N_NUMHORTRA = funcon.HoraEnDecimal(funFunciones.HorasRestar(c_horfin, TxtHorFin2.Text.Substring(11, 8)));
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    if (n_numhorser > n_tiempo)
                            //    {
                            //        if (funcon.HoraEnDecimal(TxtHorFin2.Text.Substring(11, 8)) < funcon.HoraEnDecimal(c_horfin))
                            //        {
                            //            N_NUMHORTRA = 0;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        TimeSpan result;

                            //        //if (c_horfin == "")
                            //        //{
                            //        if (n_numhorser == 24)
                            //        {
                            //            c_horfin = Convert.ToDateTime(TxtHorIni2.Text).AddHours(24).ToString();
                            //            if (Convert.ToDateTime(TxtHorFin2.Text) <= Convert.ToDateTime(c_horfin))
                            //            {
                            //                N_NUMHORTRA = 0;
                            //            }
                            //            else
                            //            {
                            //                // CALCULAMOS EL TIEMPO TRANSCURRIDO ENTRE LA FECHA DE TERMINO DEL SERVICIO Y LA HORA DE SALIDA DEL VEHICULO
                            //                result = Convert.ToDateTime(TxtHorFin2.Text).Subtract(Convert.ToDateTime(c_horfin));  //fecha2.Subtract(fecha1);
                            //                N_NUMHORTRA = result.TotalHours;
                            //            }
                            //        }
                            //        else
                            //        {
                            //            c_horfin = TxtHorFin2.Text.Substring(11, 8);

                            //            if (funcon.HoraEnDecimal(c_horfin) <= 9)
                            //            {
                            //                // CALCULAMOS EL TIEMPO TRANSCURRIDO ENTRE LA FECHA DE TERMINO DEL SERVICIO Y LA HORA DE SALIDA DEL VEHICULO
                            //                result = Convert.ToDateTime(TxtHorFin2.Text).Subtract(Convert.ToDateTime(TxtHorIni2.Text.Substring(0, 10) + " " + c_horfin).AddDays(1));  //fecha2.Subtract(fecha1);
                            //            }
                            //            else
                            //            {
                            //                result = Convert.ToDateTime(TxtHorFin2.Text).Subtract(Convert.ToDateTime(TxtHorIni2.Text.Substring(0, 10) + " " + c_horfin));  //fecha2.Subtract(fecha1);
                            //            }
                            //            N_NUMHORTRA = result.TotalHours;
                            //        }
                            //        //}
                            //    }
                            //}

                            //if (N_NUMHORTRA > 0)
                            //{
                            //    N_NUMHORTRA = (N_NUMHORTRA - n_tolera);          // DESCONTAMOS EL TIEMPO DE TOLERANCIA DE LA PLAYA
                            //}
                            //txtTieUti.Text = funcon.DecimalEnHoras(N_NUMHORTRA);
                            //N_NUMHORADI = N_NUMHORTRA;                       // ESTE DATOS ES QUE SE GUARDARA AL MOMENTO DE ESCRIBIR LA BOLETA EN EL DETALLE  
                            //double n_preciotar = 0;

                            //if (N_NUMHORTRA > 0)
                            //{
                            //    c_tolera = funcon.DecimalEnHoras(N_NUMHORTRA);
                            //    DialogResult Rpta = MessageBox.Show("El servicio a finalizo a las " + c_horfin + " , ha transucurrido " + c_tolera + " horas desde la finalizacion del servicio, Se cargara el valor adicional de " + c_tolera + " horas de servicio de estacionamiento", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                            //    DataTable dtResHor = new DataTable();
                            //    dtResHor = funGen.DataTableFiltrar(dtservicio, "n_id = " + N_IDSERHOR.ToString() + "");
                            //    if (dtResHor.Rows.Count != 0)
                            //    {
                            //        n_valadi = Convert.ToDouble(dtResHor.Rows[0]["n_imptot"]);
                            //        n_preciotar = n_valadi;
                            //        if (N_TIPOCOBRANZA == 1)
                            //        {
                            //            n_valadi = (N_NUMHORTRA * n_valadi);
                            //        }

                            //        if (N_TIPOCOBRANZA == 2)   // HORA Y FRACCION
                            //        {
                            //            int n_sumar = 0;
                            //            double n_minutos = 0;
                            //            double n_numhor = 0;
                            //            // COBRANZA POR HORA FRACCCION
                            //            n_numhor = int.Parse(N_NUMHORTRA.ToString().Split('.')[0]);

                            //            n_minutos = (n_tiempo - n_numhor);
                            //            if (n_minutos == 0)
                            //            {
                            //                n_sumar = 0;
                            //            }
                            //            else
                            //            {
                            //                n_sumar = 1;
                            //            }
                            //            n_numhor = n_numhor + n_sumar;
                            //            N_NUMHORADI = n_numhor;
                            //            n_valadi = (n_valadi * n_numhor);
                            //        }
                            //    }

                            //    BE_EST_MOVIMIENTOSDET e_movdet2 = new BE_EST_MOVIMIENTOSDET();
                            //    e_movdet2.n_idmov = 0;
                            //    e_movdet2.n_idser = N_IDSERHOR;
                            //    e_movdet2.n_impbru = ((n_valadi) / 1.18);
                            //    e_movdet2.n_impigv = (n_valadi - ((n_valadi) / 1.18));
                            //    e_movdet2.n_imptot = (n_valadi);
                            //    e_movdet2.n_idunimed = 0;
                            //    e_movdet2.n_can = N_NUMHORADI;
                            //    e_movdet2.n_preuni = (n_preciotar / 1.18);
                            //    l_movdet.Add(e_movdet2);
                            //}
                        }
                        if (Convert.ToInt32(dtRes.Rows[0]["n_pagser"]) == 1)    // PREGUNTAMOS SI EL SERVICIO NO SE PAGA
                        {
                            l_movdet[0].n_imptot = 0;
                            l_movdet[0].n_impigv = 0;
                            l_movdet[0].n_impbru = 0;
                        }
                    }
                    //TxtTotPag.Text = (n_valserori + n_valadi).ToString("0.00"); 
                }
                else
                {
                    if (n_tiempo >= n_tolera)
                    {
                        if (n_tiempo < n_tolera)
                        {
                            n_tiempo = 1;
                        }
                        else
                        {
                            if (N_TIPOCOBRANZA == 3)
                            {
                                if (N_APLTOLFRA == 2)
                                {
                                    n_tiempo = HallarTiempo(n_tiempo, n_tolera, n_AplicaMedioPago);
                                }
                                else
                                {
                                    n_tiempo = (n_tiempo - n_tolera);
                                }
                            }
                            if (N_TIPOCOBRANZA == 2)
                            { 
                                n_tiempo = (n_tiempo - n_tolera);
                                if (n_tiempo == 0)
                                {
                                    n_tiempo = 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        n_tiempo = n_tiempo;
                        if (n_tiempo == 0)
                        {
                            n_tiempo = 1;
                        }
                    }
                    
                    if (n_tiempo <= 0)
                    {

                        TxtTotPag.Text = (n_tiempo * Convert.ToDouble(TxtImporte2.Text)).ToString("0.00");
                    }
                    else
                    { 
                        if (N_TIPOCOBRANZA == 1)    // COBRANZA POR MINUTO
                        {
                            TxtTotPag.Text = (Convert.ToDouble(TxtImporte2.Text) * n_tiempo).ToString("0.00");
                        }

                        if (N_TIPOCOBRANZA == 2)   // HORA Y FRACCION
                        {
                            int n_sumar = 0;
                            double n_minutos = 0;
                            double n_numhor = 0;
                            // COBRANZA POR HORA FRACCCION
                            n_numhor = int.Parse(n_tiempo.ToString().Split('.')[0]);
                        
                            n_minutos = (n_tiempo - n_numhor);
                            if (n_minutos == 0)
                            {
                                n_sumar = 0;
                            }
                            else
                            {
                                n_sumar = 1;
                            }
                            n_numhor = n_numhor + n_sumar;
                            TxtTotPag.Text = (Convert.ToDouble(TxtImporte2.Text) * n_numhor).ToString("0.00");
                        }

                        if (N_TIPOCOBRANZA == 3)   // FRACCIONARA EN HORA Y MEDIA HORA PARA REALIZAR LA COBRANZA
                        {
                            if (N_APLFRA == 2)
                            {
                                TxtTotPag.Text = HallarPrecio(n_tiempo, Convert.ToDouble(TxtImporte2.Text)).ToString("0.00");
                            }
                            if (N_APLFRA == 1)
                            {
                                int n_sumar = 0;
                                double n_minutos = 0;
                                double n_numhor = 0;

                                n_numhor = int.Parse(n_tiempo.ToString().Split('.')[0]);

                                n_minutos = (n_tiempo - n_numhor);
                                if (n_minutos == 0)
                                {
                                    n_sumar = 0;
                                }
                                else
                                {
                                    n_sumar = 1;
                                }
                                n_numhor = n_numhor + n_sumar;
                                TxtTotPag.Text = (Convert.ToDouble(TxtImporte2.Text) * n_numhor).ToString("0.00");
                            }
                        }
                    }
                    // ESCRIBIMOS LOS VALORES DEL SERVICIO EN EL DETALLE
                    l_movdet[0].n_imptot = Convert.ToDouble(TxtTotPag.Text);
                    l_movdet[0].n_impigv = Convert.ToDouble(TxtTotPag.Text) - (Convert.ToDouble(TxtTotPag.Text) / 1.18);
                    l_movdet[0].n_impigv = (Convert.ToDouble(TxtTotPag.Text) / 1.18);
                }
                
                CboTipDoc.SelectedValue = N_IDDOCUMENTODEFAULT;
                if (N_IDDOCUMENTODEFAULT == 2) { TxtNumSer.Text = c_NUMSERFAC; }
                if (N_IDDOCUMENTODEFAULT == 4) { TxtNumSer.Text = c_NUMSERBOL; }
                if (N_IDDOCUMENTODEFAULT == 20) { TxtNumSer.Text = c_NUMSERTIC; }

                string c_dato = funGen.DataTableBuscar(dtClientes, "n_id", "n_idtipcli", LblIdCliente2.Text, "N").ToString();
                if (c_dato == "2")
                {
                    CboTipDoc.SelectedValue = 0;
                    TxtNumSer.Text = "";
                    TxtNumDoc.Text = "";
                    TxtAbono.Enabled = false;
                }
                else
                {
                    TxtAbono.Enabled = true;

                    CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
                    objTipDoc.mysConec = o_conec.mysConec;
                    TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text);
                    o_conec = null;
                    TxtAbono.Focus();
                }
            }
            else
            { 
            }
            objRegistro = null;
        }
        public string[] HallarTiempoTar24(string c_FchHorIni, string c_FchHorFin, string c_FchHorTerser, double n_Tolerancia, int n_AplicaMedioPago, double n_TiempoServicio)
        {
            double n_valor = 0;
            TimeSpan result;

            double n_numhoradi = 0;
            double n_numhoradireal = 0;

            result = Convert.ToDateTime(c_FchHorTerser).Subtract(Convert.ToDateTime(c_FchHorFin));  //fecha2.Subtract(fecha1);
            n_valor = 1;

            if (result.TotalHours >= n_Tolerancia)
            { 
                n_numhoradi = result.TotalHours;
                n_numhoradireal = result.TotalHours;
                n_numhoradi = HallarTiempo(n_numhoradi, n_Tolerancia, n_AplicaMedioPago);
            }
            string[] ret = { n_valor.ToString(), n_numhoradi.ToString(), n_numhoradireal.ToString() };
            return ret;
        }
        public string[] CalcularDiaNoche(string c_HorIni, string c_HorFin, string c_DiaHorTerminoServicio, string c_DiaInicio, double n_Tolerancia, int n_AplicaMedioPago)
        {
            // n_AplicaMedioPago = INDICA SI EL SERVCIIO SE COBRARA LA MITAD DE SU VALOR  1 = NO APLICA MEDIO PAGO;    2 = APLICA MEDIO PAGO

            Helper.Comunes.Convertir funcon2 = new Helper.Comunes.Convertir();
            Funciones funFunciones2 = new Funciones();
            TimeSpan result;
            int n_valor = 0;                                // ALMACENA EL VANLOR DEL SERVICIO CUANDO SE CALCULE
            int n_turno = 0;                                // INDICA EL TURNO  1 = DIA         2= NOCHE
            double n_horaini = 0;                           // PARA DETERMINAR LA HORA DE INICIO DEL SERVICIO
            double n_numhoradi = 0;
            double n_numhoradireal = 0;
            n_horaini = funcon2.HoraEnDecimal(funFunciones2.HorasRestar("00:00", c_HorIni));

            if (n_horaini <= 12) { n_turno = 1; }
            else { n_turno = 2; }

            if (n_turno == 1)                  // SI ES TURNO DE DIA
            {
                if (Convert.ToDateTime(c_DiaHorTerminoServicio) <= Convert.ToDateTime(c_DiaInicio + " "+ c_HorFin))
                {
                    n_valor = 1;
                }
                else
                {
                    n_valor = 1;
                    result = Convert.ToDateTime(c_DiaHorTerminoServicio).Subtract(Convert.ToDateTime(c_DiaInicio + " " + c_HorFin));  //fecha2.Subtract(fecha1);
                    n_numhoradi = result.TotalHours;
                    n_numhoradireal = result.TotalHours;
                    n_numhoradi = HallarTiempo(n_numhoradi, n_Tolerancia, n_AplicaMedioPago);
                }
            }
            if (n_turno == 2)
            {
                string c_aa = "";
                //if (c_DiaInicio == DateTime.Now.ToString("dd/MM/yyyy"))
                //{
                //    c_aa = Convert.ToDateTime(c_DiaInicio).AddDays(0).ToString("dd/MM/yyyy") + " " + c_HorFin;
                //    //c_aa = DateTime.Now.ToString("dd/MM/yyyy") + " " + c_HorFin;
                //}
                //else
                //{
                //    c_aa = Convert.ToDateTime(c_DiaInicio).AddDays(1).ToString("dd/MM/yyyy") + " " + c_HorFin;
                //}
                c_aa = Convert.ToDateTime(c_DiaInicio).AddDays(1).ToString("dd/MM/yyyy") + " " + c_HorFin;          // CALCULAMOS EL EL DIA Y HORA DE TERMINO DEL SERVICIO

                if (Convert.ToDateTime(c_DiaHorTerminoServicio) <= Convert.ToDateTime(c_aa))
                {
                    n_valor = 1;
                }
                else
                {
                    n_valor = 1;
                    result = Convert.ToDateTime(c_DiaHorTerminoServicio).Subtract(Convert.ToDateTime(c_aa));  //fecha2.Subtract(fecha1);
                    n_numhoradi = result.TotalHours;
                    n_numhoradireal = result.TotalHours;
                    n_numhoradi = HallarTiempo(n_numhoradi, n_Tolerancia, n_AplicaMedioPago);
                }
            }

            string[] ret = { n_valor.ToString(), n_numhoradi.ToString(), n_numhoradireal.ToString() };
            return ret;
        }

        public double HallarPrecio(double n_Valor, double n_Precio)
        {
            double n_val = 0;

            string numStr = n_Valor.ToString("0.00");
            int n_entero = int.Parse(numStr.Split('.')[0]);
            float n_decimal = float.Parse("0," + numStr.Split('.')[1]);

            n_val = n_entero * n_Precio;

            if (n_decimal <= 50)
            {
                double n_valmit = 0;
                double n_mitadprecio = (n_Precio / 2);
                numStr = n_mitadprecio.ToString("0.00");
                int n_ent = int.Parse(numStr.Split('.')[0]);

                if (Convert.ToInt32(n_decimal) == 0)
                {
                    //n_val = n_val;
                    //if ((n_mitadprecio - n_ent) == 0)
                    //{
                    //    n_valmit = n_mitadprecio;
                    //}
                    //else
                    //{
                    //    n_valmit = n_mitadprecio + 0.5;
                    //}
                    //n_val = (n_val + n_mitadprecio);
                    n_val =n_val+ n_valmit;
                }
                else
                {
                    if ((n_mitadprecio - n_ent) == 0)
                    {
                        n_valmit = n_mitadprecio;
                    }
                    else
                    {
                        n_valmit = n_mitadprecio + 0.5;
                    }
                    //n_val = (n_val + n_mitadprecio);
                    n_val = n_val + n_valmit;
                }
                //if ((n_mitadprecio - n_ent) == 0)
                //{
                //    n_valmit = n_mitadprecio;
                //}
                //else
                //{
                //    n_valmit = n_mitadprecio + 0.5;
                //}
                ////n_val = (n_val + n_mitadprecio);
                //n_val = n_val + n_valmit;
            }
            else
            {
                n_val = n_val + n_Precio;
            }

            return n_val;
        }
        public double HallarTiempo(double n_tiempo, double n_tolerancia, int n_AplicaMedioPago)
        {
            // n_AplicaMedioPago = INDICA SI EL SERVCIIO SE COBRARA LA MITAD DE SU VALOR  1 = NO APLICA MEDIO PAGO;    2 = APLICA MEDIO PAGO
            double n_newtiempo = 0;
            int n_entero = DecimalHallarEnteros(n_tiempo,1);
            int n_decimal = DecimalHallarEnteros(n_tiempo, 2); 
            if (n_entero == 0)
            {
               if (n_decimal <= 50)
               {
                    if (n_AplicaMedioPago == 2) { n_newtiempo = n_entero + 0.5; }
                    if (n_AplicaMedioPago == 1) { n_newtiempo = n_entero + 1; } 
                  //n_newtiempo  = 0.5;
               }
               else
               {
                  n_newtiempo  = 1;
               }
            }
            else
            {
	            if (n_tolerancia != 0)	                             // si toleracia es <>  0 se aplica tolerancia
	            {
                    if (n_tiempo > (n_entero + n_tolerancia))        // si el tiempo es mayor al entero + tolerancia le sumamos medio tiempo
		            {
			            if (n_decimal <= 50)
			            {
                            if (n_AplicaMedioPago == 2) { n_newtiempo = n_entero + 0.5; }
                            if (n_AplicaMedioPago == 1) { n_newtiempo = n_entero + 1; } 
			            }
			            else
			            {
                            n_newtiempo = n_entero+ 1;
			            }
		            }		
		            else
		            {
                        if (n_decimal == 0)
                        {
                            n_newtiempo = n_entero;
                            //n_newtiempo = 0;
                        }
                        else
                        {
                            if ((n_tiempo -n_entero) < n_tolerancia)
                            {
                                n_newtiempo = n_entero;   
                            }
                            else
                            { 
    		                    if (n_decimal <= 50)
			                    { 
				                    n_newtiempo  = n_entero + 0.5;
			                    }	   	
				                    else
			                    {
				                    n_newtiempo  = n_entero + 1;
			                    }
                            }
                        }
		            }		
	            }
	            else
	            {
		            if (n_decimal <= 50)
		            { 
			            n_newtiempo  = n_entero + 0.5;
		            }	   	
			        else
		            {
			            n_newtiempo  = n_entero + 1;
		            }	
	            }
            }

            return n_newtiempo;
        }
        int DecimalHallarEnteros(double n_valor, int n_tipo)
        {
            // n_tipo = 1  DEVUELVE LA PARTE ENTERA
            // n_tipo = 2  DEVUELVE LA PARTE DECIMAL
            int n_newvalor = 0;
            string numStr = n_valor.ToString("0.00");

            if (n_tipo == 1) { n_newvalor = int.Parse(numStr.Split('.')[0]); }
            if (n_tipo == 2) { n_newvalor = int.Parse(numStr.Split('.')[1]); }

            return n_newvalor;
        }
        private void TxtAbono_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtNumSer_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtNumDoc_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtNumPla2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtCliente2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboServ2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtHorIni2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtHorFin2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtImporte2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboTipDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int n_row = 0;
            bool b_econtro = false;
            int n_id = Convert.ToInt32( LblIdReg.Text);
            CargarDatos();
            funFlex.FlexMostrarDatos(FgReg, arrCabecera1, dtLista, 2, true);
            PintarFilas();

            for (n_row = 2; n_row <= FgReg.Rows.Count - 1; n_row++)
            {
                if (n_id == Convert.ToInt32(FgReg.GetData(n_row, 9).ToString()))
                {
                    //if (FgReg.GetData(n_row, 8).ToString() != "")
                    //{ 
                        b_econtro = true;
                    //}
                }
            }

            if (b_econtro == false)     // SI NO LO ENCUENTRA QUIERE DECIR QUE YA ESTA PAGADO
            {
                MessageBox.Show("¡ El ingreso seleccionado ya fue cancelado, verifique que haya sido cancelado haciendo clic en el boton Ver Todos los Ingresos !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdCan2_Click(sender, e);
                return;
            }

            if (funFunciones.NulosC(TxtNumSer.Text) == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumSer.Focus();
                return;
            }
            if (funFunciones.NulosC(TxtNumDoc.Text) == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumDoc.Focus();
                return;
            }
            if (funFunciones.NulosC(TxtNumPla2.Text) == "")
            {
                MessageBox.Show("¡ No ha especificado el cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumPla.Focus();
                return;
            }
            
            if (Convert.ToInt32(CboServ2.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboSer.Focus();
                return;
            }
            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento que se va a generar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdAddEmp.Focus();
                return;
            }

            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 2)
            {
                if (TxtCliente2.Text == "DIVERSOS")
                { 
                    MessageBox.Show("¡ Debe de ingresar el numero de R.U.C. y el nombre de la razon social para generar este documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    CmdAddEmp.Focus();
                    return;
                }
            }

            if (Modificar() == false)
            {
                MessageBox.Show("¡ No se pudo guardar la operacion, intente de nuevo !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            CmdCan2_Click(sender, e);
        }
        private void CmdCan2_Click(object sender, EventArgs e)
        {
            CmdAddPer.Visible = false;
            CmdAddEmp.Visible = false;

            button2.Visible = true;
            CmdImp2.Visible = false;
            toolStrip1.Enabled = true;
            c1Sizer2.Enabled = true;
            Pan2.Visible = false;

            TxtPlaBus.Text = "";
            TxtPlaBus.Focus();
            TxtPlaBus.BackColor = Color.YellowGreen;
        }
        private void TxtAbono_Validated(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(TxtAbono.Text) == "") { return; }

            TxtVuelto.Text = (Convert.ToDouble(TxtAbono.Text) - Convert.ToDouble(TxtTotPag.Text)).ToString("0.00");
        }
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        void MostrarPago()
        {
            int n_id = Convert.ToInt32(FgReg.GetData(FgReg.Row, 9));
            DataTable dtresul = new DataTable();
            dtresul = funGen.DataTableFiltrar(dtLista, "n_id = " + n_id + "");
            if (dtresul.Rows.Count != 0)
            {
                if (dtresul.Rows[0]["n_finalizado"].ToString() == "1")
                { 
                    MessageBox.Show("¡ El movimiento no ha sido finalizado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
            }

            toolStrip1.Enabled = false;
            c1Sizer2.Enabled = false;
            Pan2.Left = ((this.Width - Pan2.Width) / 2);
            Pan2.Top = ((this.Height - Pan2.Height) / 2);
            Pan2.Visible = true;
            TxtNumPla.Focus();
            
            BE_EST_MOVIMIENTOS e_Movi = new BE_EST_MOVIMIENTOS();
            CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
            objRegistro.STU_SISTEMA = STU_SISTEMA;
            objRegistro.TraerRegistro(n_id);
            e_Movi = objRegistro.e_Movimiento;
            objRegistro = null;

            DataTable dtRes = new DataTable();
            dtRes = funGen.DataTableFiltrar(dtClientes, "n_id = " + e_Movi.n_idcli.ToString() + "");
            if (dtRes.Rows.Count != 0)
            {
                TxtCliente2.Text = dtRes.Rows[0]["c_nom"].ToString();
            }
            CboServ2.SelectedValue = e_Movi.n_idser;
            LblIdCliente2.Text = e_Movi.n_idcli.ToString();
            TxtHorIni2.Text = e_Movi.c_horini;
            TxtHorFin2.Text = e_Movi.c_horfin;
            txtTieUti.Text = e_Movi.c_tieuit;
            TxtNumPla2.Text = e_Movi.c_numpla;
            TxtImporte2.Text = e_Movi.n_impser.ToString("0.00");
            TxtTotPag.Text = e_Movi.n_importe.ToString("0.00");
            CboTipDoc.SelectedValue = e_Movi.n_idtipdoc;
            if (e_Movi.c_numdoc != "")
            { 
                TxtNumSer.Text = e_Movi.c_numdoc.Substring(0,4);
                TxtNumDoc.Text = e_Movi.c_numdoc.Substring(5, 10);
            }
            button2.Visible = false;
            CmdImp2.Left = 387;
            CmdImp2.Top = 287;
            CmdImp2.Visible = true;
        }
        void MostrarIngreso()
        {
            toolStrip1.Enabled = false;
            c1Sizer2.Enabled = false;
            Pan1.Left = ((this.Width - Pan1.Width) / 2);
            Pan1.Top = ((this.Height - Pan1.Height) / 2);
            Pan1.Visible = true;
            TxtNumPla.Focus();
            int n_id = Convert.ToInt32(FgReg.GetData(FgReg.Row, 9));
            BE_EST_MOVIMIENTOS e_Movi = new BE_EST_MOVIMIENTOS();

            CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
            objRegistro.STU_SISTEMA = STU_SISTEMA;
            objRegistro.TraerRegistro(n_id);
            e_Movi = objRegistro.e_Movimiento;
            objRegistro = null;
            
            DataTable dtRes = new DataTable();
            dtRes = funGen.DataTableFiltrar(dtClientes, "n_id = " + e_Movi.n_idcli.ToString() + "");
            if (dtRes.Rows.Count !=0)
            {
                TxtCliente.Text = dtRes.Rows[0]["c_nom"].ToString();
            }
            CboSer.SelectedValue = e_Movi.n_idser;
            LblIdCliente.Text = e_Movi.n_idcli.ToString();
            TxtHorIni.Text = e_Movi.c_horini;
            TxtNumPla.Text = e_Movi.c_numpla;
            CmdAce.Visible = false;
            CmdImp.Left = 201;
            CmdImp.Top = 153;
            CmdImp.Visible = true;
        }
        private void CmdImp_Click(object sender, EventArgs e)
        {
            int n_id = Convert.ToInt32(FgReg.GetData(FgReg.Row, 9));
            string c_dato = TxtNumPla.Text + "|" + TxtCliente.Text + "|" + DateTime.Now.ToString("dd/MM/yyyy") + "|" + TxtHorIni.Text + "|" + CboLocal.SelectedText + "|" + CboCajero.SelectedText;
            
            CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
            objRegistro.STU_SISTEMA = STU_SISTEMA;
            objRegistro.STU_SISTEMA = STU_SISTEMA;
            objRegistro.ImprimirTicket(n_id, c_dato, N_VISTAPREVIA);
            objRegistro = null;
            CmdCan_Click(sender, e);
        }
        private void FrmRegistroMovimientos_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void FrmRegistroMovimientos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData.ToString() == "Escape")
            {
                TxtPlaBus.Text = "";
                TxtPlaBus.Focus();
                TxtPlaBus.BackColor = Color.YellowGreen;
            }
            if (e.KeyData.ToString() == "F3")
            {
                TooNuevo_Click(sender, e);

                TxtPlaBus.Text = "";
                TxtPlaBus.Focus();
                TxtPlaBus.BackColor = Color.YellowGreen;
            }
            if (e.KeyData.ToString() == "F5")
            {
                ToolCliRapido_Click(sender, e);

                TxtPlaBus.Text = "";
                TxtPlaBus.Focus();
                TxtPlaBus.BackColor = Color.YellowGreen;
            }
            if (e.KeyData.ToString() == "F6")
            {
                int n_idreg = Convert.ToInt32(FgReg.GetData(FgReg.Row, 9));
                //string c_numpla = FgReg.GetData(FgReg.Row, 1).ToString();
                //string c_cliente = FgReg.GetData(FgReg.Row, 2).ToString();
                //string c_horini = FgReg.GetData(FgReg.Row, 3).ToString();
                //string c_dato = c_numpla + "|" + c_cliente + "|" + DateTime.Now.ToString("dd/MM/yyyy") + "|" + c_horini + "|" + CboLocal.Text + "|" + CboCajero.Text;

                CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
                objRegistro.STU_SISTEMA = STU_SISTEMA;
                objRegistro.ImprimirTicket(n_idreg, "", 2);
                objRegistro = null;

                TxtPlaBus.Text = "";
                TxtPlaBus.Focus();
                TxtPlaBus.BackColor = Color.YellowGreen;

                //CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
                //objRegistro.STU_SISTEMA = STU_SISTEMA;
                //objRegistro.ImprimirTicket(n_idreg, c_dato, N_VISTAPREVIA);
                //objRegistro = null;
            }
            if (e.KeyData.ToString() == "F7")
            {
                if (funFunciones.NulosC(FgReg.GetData(FgReg.Row, 8)) == "")
                {
                    MessageBox.Show("¡ El ingreso aun no ha sido cancelado, no se puede imprimir documento de venta", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    FgReg.Focus();
                    return;
                }
                int n_idreg = Convert.ToInt32(FgReg.GetData(FgReg.Row, 10));
                CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
                objRegistro.STU_SISTEMA = STU_SISTEMA;
                objRegistro.ImprimirComprobantePago(STU_SISTEMA.EMPRESAID, n_idreg, "", 0, 2,1);
                objRegistro = null;

                TxtPlaBus.Text = "";
                TxtPlaBus.Focus();
                TxtPlaBus.BackColor = Color.YellowGreen;
                //int n_idreg = Convert.ToInt32(FgReg.GetData(FgReg.Row, 10));
                //string c_numpla = FgReg.GetData(FgReg.Row, 1).ToString();
                //string c_cliente = FgReg.GetData(FgReg.Row, 2).ToString();
                //string c_horini = FgReg.GetData(FgReg.Row, 3).ToString();
                //string c_horfin = FgReg.GetData(FgReg.Row, 4).ToString();
                //string c_importe = FgReg.GetData(FgReg.Row, 6).ToString();
                //string c_tipdoc = FgReg.GetData(FgReg.Row, 7).ToString(); 
                //string c_idtipdoc = FgReg.GetData(FgReg.Row, 12).ToString();
                //string c_numdoc = FgReg.GetData(FgReg.Row, 8).ToString();
                //string c_dato = c_numpla + "|" + c_cliente + "|" + DateTime.Now.ToString("dd/MM/yyyy") + "|" + c_horini + "|" + c_horfin + "|" + CboLocal.Text + "|" + CboCajero.Text + "|" + c_numdoc + "|" + c_importe + "|" + c_tipdoc;

                //CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
                //objRegistro.STU_SISTEMA = STU_SISTEMA;
                //objRegistro.ImprimirComprobantePago(STU_SISTEMA.EMPRESAID, n_idreg, c_dato, Convert.ToInt32(c_idtipdoc), N_VISTAPREVIA);
                //objRegistro = null;
            }
            if (e.KeyData.ToString() == "F8")
            {
                FrmPrintCargos ofrm = new FrmPrintCargos();
                ofrm.STU_SISTEMA = STU_SISTEMA;
                ofrm.N_IDPLAYA = Convert.ToInt32(CboLocal.SelectedValue);
                ofrm.N_IDCAJERO = Convert.ToInt32(CboCajero.SelectedValue);
                ofrm.N_IDTIPDOC = Convert.ToInt32(CboCajero.SelectedValue);
                ofrm.C_CAJERO = CboCajero.Text;
                ofrm.C_LOCAL = CboLocal.Text;
                ofrm.ShowDialog();

                TxtPlaBus.Text = "";
                TxtPlaBus.Focus();
                TxtPlaBus.BackColor = Color.YellowGreen;
            }
        }
        private void CmdImp2_Click(object sender, EventArgs e)
        {
            int n_idvta = Convert.ToInt32(FgReg.GetData(FgReg.Row, 10));
            string c_dato = TxtNumPla2.Text + "|" + TxtCliente2.Text + "|" + DateTime.Now.ToString("dd/MM/yyyy") + "|" + TxtHorIni2.Text + "|" + TxtHorFin2.Text + "|" + CboLocal.Text + "|" + CboCajero.Text + "|" + TxtNumSer.Text + "-" + TxtNumDoc.Text + "|" + TxtImporte2.Text + "|" + CboTipDoc.Text;

            CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
            objRegistro.STU_SISTEMA = STU_SISTEMA;
            objRegistro.ImprimirComprobantePago(STU_SISTEMA.EMPRESAID, n_idvta, c_dato, Convert.ToInt32(CboTipDoc.SelectedValue), N_VISTAPREVIA, 1);
            objRegistro = null;
            CmdCan2_Click(sender, e);
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string c_dato = "";
            //if (FgReg.Rows.Count == 2) { return; }
            if (FgReg.Rows.Count != 2)
            { 
                c_dato = FgReg.GetData(FgReg.Row, 1).ToString();
            }
            FrmPagarCargoAbonado xFrm = new FrmPagarCargoAbonado();
            xFrm.STU_SISTEMA = STU_SISTEMA;
            xFrm.C_NUMEROPLACA = c_dato;
            xFrm.N_LOCAL = Convert.ToInt32(CboLocal.SelectedValue);
            xFrm.C_LOCAL = CboLocal.Text;
            xFrm.N_IDCAJERO = Convert.ToInt32(CboCajero.SelectedValue);
            xFrm.ShowDialog();

            TxtPlaBus.Text = "";
            TxtPlaBus.Focus();
            TxtPlaBus.BackColor = Color.YellowGreen;
            this.Focus();
        }
        private void CboTipDoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void CmdVer_Click(object sender, EventArgs e)
        {
            if (N_TIPOVISTA == 1)
            {
                N_TIPOVISTA = 2;
                CmdVer.Text = "Ver Solo Pendientes";
            }
            else
            {
                N_TIPOVISTA = 1;
                CmdVer.Text = "Ver Todos Ingresos";
            }
            CargarDatos();
            funFlex.FlexMostrarDatos(FgReg, arrCabecera1, dtLista, 2, true);
            PintarFilas();
        }
        void PintarFilas()
        {
            int n_row = 2;
            for (n_row = 2; n_row <= FgReg.Rows.Count-1; n_row++)
            {
                //if (TxtFecha.Text != FgReg.GetData(n_row, 13).ToString().Substring(0, 10))
                if (DateTime.Now.ToString("dd/MM/yyyy") != FgReg.GetData(n_row, 13).ToString().Substring(0, 10))
                {
                    funFlex.Flex_PintarCeldas(FgReg, n_row, 1, n_row, 12, Color.Blue);
                }
                else
                {
                    funFlex.Flex_PintarCeldas(FgReg, n_row, 1, n_row, 12, Color.Black);
                }
            }
        }
        private void ToolCliRapido_Click(object sender, EventArgs e)
        {
            string[,] arrCabeceraDg1 = new string[3, 4];
            DataTable dtResult = new DataTable();
            Helper.Genericas funDatos = new Helper.Genericas();
            N_TIPOVISTA = 1;
            CargarDatos();
            dtResult = dtLista;

            arrCabeceraDg1[0, 0] = "Nº Placa";
            arrCabeceraDg1[0, 1] = "100";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_clinumpla";

            arrCabeceraDg1[1, 0] = "Nombre Abonado";
            arrCabeceraDg1[1, 1] = "600";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_clides";

            arrCabeceraDg1[2, 0] = "Id";
            arrCabeceraDg1[2, 1] = "0";
            arrCabeceraDg1[2, 2] = "N";
            arrCabeceraDg1[2, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "c_clinumpla";
            xFun.Buscar_CadFiltro = "";
            xFun.Buscar_CampoOrden = "c_clinumpla";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            if (dtResult != null)
            { 
                if (dtResult.Rows.Count != 0)
                { 
                    toolStrip1.Enabled = false;
                    c1Sizer2.Enabled = false;
                    Pan2.Left = ((this.Width - Pan2.Width) / 2);
                    Pan2.Top = ((this.Height - Pan2.Height) / 2);

                    int n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                    BlanqueaPanel2();
                    string c_plca = dtResult.Rows[0]["c_clinumpla"].ToString();
                    MostrarDetalle(n_id, c_plca);
                    Pan2.Visible = true;
                    button2.Focus();
                }
            }
            TxtPlaBus.Text = "";
            TxtPlaBus.Focus();
            TxtPlaBus.BackColor = Color.YellowGreen;
        }

        private void CmdAddPer_Click(object sender, EventArgs e)
        {
            Formularios.FrmManClientes FrmMan = new Formularios.FrmManClientes();
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.n_TipoCli = 1;
            FrmMan.n_DeDonde = 2;
            FrmMan.c_numplaca = TxtNumPla2.Text;
            FrmMan.n_tipserv = Convert.ToInt32(CboServ2.SelectedValue);
            FrmMan.n_idpla = Convert.ToInt32(CboLocal.SelectedValue);
            FrmMan.ShowDialog();
            if (FrmMan.N_CLIENTE_ID != 0)
            { 
                LblIdCliente2.Text = FrmMan.N_CLIENTE_ID.ToString();
                TxtCliente2.Text = FrmMan.C_CLIENTE_NOM;
                CboTipDoc.SelectedValue = 4;
                RecargarCliente();
            }
            FrmMan = null;
        }
        void RecargarCliente()
        {
            CN_est_clientes o_cliente = new CN_est_clientes(STU_SISTEMA);
            o_cliente.STU_SISTEMA = STU_SISTEMA;
            o_cliente.Listar();                   // CARGAMOS TODOS LOS CLIENTES
            dtClientes = o_cliente.dtListar;
            o_cliente = null;
        }
        private void CmdAddEmp_Click(object sender, EventArgs e)
        {
            Formularios.FrmManClientes FrmMan = new Formularios.FrmManClientes();
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.n_TipoCli = 2;
            FrmMan.n_DeDonde = 2;
            FrmMan.c_numplaca = TxtNumPla2.Text;
            FrmMan.n_tipserv = Convert.ToInt32(CboServ2.SelectedValue);
            FrmMan.n_idpla = Convert.ToInt32(CboLocal.SelectedValue);
            FrmMan.c_numplaca = TxtNumPla2.Text;
            FrmMan.ShowDialog();
            if (FrmMan.N_CLIENTE_ID != 0)
            {
                LblIdCliente2.Text = FrmMan.N_CLIENTE_ID.ToString();
                TxtCliente2.Text = FrmMan.C_CLIENTE_NOM;
                CboTipDoc.SelectedValue = 2;
                RecargarCliente();
            }
            FrmMan = null;

        }
        private void CboTipDoc_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            //if (Convert.ToInt32(CboTipDoc.Items.Count) == 0) { return; }
            //if (CboTipDoc.Text == "") { return; }
            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 0) { return; }
            TxtAbono.Enabled = true;

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            objTipDoc.mysConec = o_conec.mysConec;
            
            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 2) { TxtNumSer.Text = c_NUMSERFAC; }
            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 4) { TxtNumSer.Text = c_NUMSERBOL; }
            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 90) { TxtNumSer.Text = c_NUMSERTIC; }
            TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text);
            o_conec = null;
            TxtAbono.Focus();
        }
        private void CboLocal_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            CargarDatos();
            funFlex.FlexMostrarDatos(FgReg, arrCabecera1, dtLista, 2, true);
        }
        private void ToolAnular_Click(object sender, EventArgs e)
        {
            if (FgReg.Rows.Count == 2)
            {
                MessageBox.Show("¡ No hay movimiento para eliminar", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgReg.Focus();
                return;
            }

            int n_idmov = Convert.ToInt32(FgReg.GetData(FgReg.Row, 9));
            int n_iddocven = Convert.ToInt32(FgReg.GetData(FgReg.Row, 10));

            DialogResult Rpta = MessageBox.Show("Esta seguro de anular el movimiento seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                Anular(n_idmov, n_iddocven);
            }

            TxtPlaBus.Text = "";
            TxtPlaBus.Focus();
            TxtPlaBus.BackColor = Color.YellowGreen;
        }
        void Anular(int n_IdMovimiento, int n_IdDocumentoventa)
        {
            CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
            objRegistro.STU_SISTEMA = STU_SISTEMA;
            if (objRegistro.Anular(n_IdMovimiento, n_IdDocumentoventa) == false)
            {
                MessageBox.Show("El registro no se pudo anular por el siguiente notivo : " + objRegistro.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("El registro se anulo con exito", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CargarDatos();
                funFlex.FlexMostrarDatos(FgReg, arrCabecera1, dtLista, 2, true);
            }
            objRegistro = null;
        }
        private void FrmRegistroMovimientos_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void CboSer_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            DataTable dtRes = new DataTable();
            dtRes = funGen.DataTableFiltrar(dtservicio, "n_id = " + Convert.ToInt32(CboSer.SelectedValue).ToString() + "");
           
            if (dtRes.Rows.Count != 0)
            {
                //if (LblTipCliente.Text != "2")
                //{
                //    if (Convert.ToInt32(CboSer.SelectedValue) == N_IDSERABONADO)
                //    {
                //        MessageBox.Show("¡ El servicio no aplica para este tipo de cliente !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //        CboSer.SelectedValue = 0;
                //        CboSer.Focus();
                //        return;
                //    }
                //}

                string c_horini = funFunciones.NulosC(dtRes.Rows[0]["c_horini"]);
                string c_horfin = funFunciones.NulosC(dtRes.Rows[0]["c_horfin"]);

                if (c_horini != "")
                {
                    if (SaberSiEsDiaNoche(c_horini, c_horfin,DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")) == false)
                    { 
                        MessageBox.Show("¡ No se puede registrar este servicio la hora del servicio inicia a las " + c_horini + ", seleccione otro servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        CboSer.SelectedValue = 0;
                        CboSer.Focus();
                        return;
                    }
                }
            }
        }
        bool SaberSiEsDiaNoche(string c_HorIni, string c_HorFin, string c_DiaHoraRegistro)
        {
            bool b_procede = false;
            int n_turno = 0;                                // INDICA EL TURNO  1 = DIA         2= NOCHE
            double n_horaini = 0;                           // PARA DETERMINAR LA HORA DE INICIO DEL SERVICIO
            Funciones funFunciones2 = new Funciones();
            Helper.Comunes.Convertir funcon2 = new Helper.Comunes.Convertir();

            n_horaini = funcon2.HoraEnDecimal(funFunciones2.HorasRestar("00:00", c_HorIni));

            if (n_horaini <= 12) { n_turno = 1; }
            else { n_turno = 2; }

            if (n_turno == 1)
            {
                string c_horreg = c_DiaHoraRegistro.ToString().Substring(11, 8);
                double n_horreg = funcon2.HoraEnDecimal(c_horreg);
                double n_horini = funcon2.HoraEnDecimal(c_HorIni);
                double n_horfin = funcon2.HoraEnDecimal(c_HorFin);

                if (n_horreg >= n_horini)
                {
                    if (n_horreg <= n_horfin)
                    {
                        b_procede = true;
                    }
                }
            }
            if (n_turno == 2)
            {
                string c_horreg = c_DiaHoraRegistro.ToString().Substring(11, 8);
                double n_horreg = funcon2.HoraEnDecimal(c_horreg);
                double n_horini = funcon2.HoraEnDecimal(c_HorIni);
                double n_horfin = funcon2.HoraEnDecimal(c_HorFin);
                                
                if (n_horreg >= n_horini)                             // SI LA HORA DE INICIO ES MAYOR O IGUAL A LA HORA DE INICIO
                {
                    double n_horini2 = funcon2.HoraEnDecimal(c_DiaHoraRegistro.Substring(11, 8));
                    if ((n_horini2 >= n_horini) && (n_horini2 <= funcon2.HoraEnDecimal("23:59")))
                    {
                        b_procede = true;
                    }
                    if (b_procede == false)
                    {
                        if ((n_horini2 >= 0) && (n_horini2 <= n_horfin))
                        {
                            b_procede = true;
                        }
                    }
                }
                if (b_procede == false)           // ANALIZAMOS EL SIGUIENTE TURNO DE LA NOCHE
                {
                    double n_horini2 = funcon2.HoraEnDecimal(c_DiaHoraRegistro.Substring(11, 8));
                    if ((n_horini2 >= 0) && (n_horini2 <= n_horfin))
                    {
                        b_procede = true;
                    }
                }
            }
            return b_procede;
        }
        private void ToolAyuda_Click(object sender, EventArgs e)
        {
            CargarDatos();
            MessageBox.Show("¡ Los datos se actualizaron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            
            TxtPlaBus.Text = "";
            TxtPlaBus.Focus();
            TxtPlaBus.BackColor = Color.YellowGreen;
        }

        private void ToolAnulaPago_Click(object sender, EventArgs e)
        {
            if (FgReg.Rows.Count == 2)
            {
                MessageBox.Show("¡ No hay ingresos para finalizar, debe de agregar al menos un ingreso !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgReg.Focus();
                return;
            }
            if (funFunciones.NulosC(FgReg.GetData(FgReg.Row, 2)) == "ANULADO")
            {
                MessageBox.Show("¡ El ingreso fue anulado no se puede anular el pago, seleccione otro!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgReg.Focus();
                return;
            }
            if (funFunciones.NulosC(FgReg.GetData(FgReg.Row, 8)) == "")
            {
                MessageBox.Show("¡ El ingreso aun no ha sido pagado, seleccione otro!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgReg.Focus();
                return;
            }
            int n_idmov = Convert.ToInt32(FgReg.GetData(FgReg.Row, 9).ToString());
            int n_iddocven = Convert.ToInt32(FgReg.GetData(FgReg.Row, 10).ToString());
            CN_est_movimientos o_mov = new CN_est_movimientos(STU_SISTEMA);
            if (o_mov.AnularPago(n_idmov, n_iddocven, STU_SISTEMA.EMPRESAID) == true)
            {
                MessageBox.Show("¡ El pago se extorno con exito, ya puede volver a cancelar el documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CargarDatos();
                funFlex.FlexMostrarDatos(FgReg, arrCabecera1, dtLista, 2, true);
            }
            else
            {
                MessageBox.Show("¡ No se pudo extornar el pago por el siguiente motivo:" + o_mov.c_ErrorMensaje.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            TxtPlaBus.Text = "";
            TxtPlaBus.Focus();
            TxtPlaBus.BackColor = Color.YellowGreen;
        }

        private void TxtPlaBus_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void TxtPlaBus_Validated(object sender, EventArgs e)
        {   
        }

        private void TxtPlaBus_KeyDown(object sender, KeyEventArgs e)
        {
        }
        private void TxtPlaBus_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar != (char)13)
            //{
            //    c_DatoEscan = c_DatoEscan + TxtPlaBus.Text;
            //}

            if (e.KeyChar == (char)13)
            {
                //c_DatoEscan  = c_DatoEscan + TxtPlaBus.Text;
                TxtPlaBus.Text = TxtPlaBus.Text.Replace("'", "-");
                DataTable dtResult = new DataTable();
                CargarDatos();
                dtResult = dtLista;
                dtResult = funGen.DataTableFiltrar(dtResult, "c_clinumpla = '" + TxtPlaBus.Text + "'");

                if (dtResult.Rows.Count != 0)
                {

                    toolStrip1.Enabled = false;
                    c1Sizer2.Enabled = false;
                    Pan2.Left = ((this.Width - Pan2.Width) / 2);
                    Pan2.Top = ((this.Height - Pan2.Height) / 2);

                    int n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                    BlanqueaPanel2();
                    string c_plca = dtResult.Rows[0]["c_clinumpla"].ToString();
                    MostrarDetalle(n_id, c_plca);
                    Pan2.Visible = true;
                    button2.Focus();
                }
                //c_DatoEscan = "";
                TxtPlaBus.Text = "";
                TxtPlaBus.Focus();
                TxtPlaBus.BackColor = Color.YellowGreen;
            }
        }

        private void TxtPlaBus_Enter(object sender, EventArgs e)
        {
            
        }

        private void TxtPlaBus_Leave(object sender, EventArgs e)
        {
            if (TxtPlaBus.Focused == true)
            {
                TxtPlaBus.BackColor = Color.YellowGreen;
            }
            else
            {
                TxtPlaBus.BackColor = Color.White;
            }
        }

        private void TxtPlaBus_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void TxtPlaBus_MouseClick(object sender, MouseEventArgs e)
        {
            TxtPlaBus.BackColor = Color.YellowGreen;
        }
    }
}
