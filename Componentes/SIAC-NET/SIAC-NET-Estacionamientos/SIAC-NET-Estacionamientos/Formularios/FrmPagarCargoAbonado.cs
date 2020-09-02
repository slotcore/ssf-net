using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Sistema;
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
using SIAC_Entidades.Contabilidad;
using System.Configuration;

namespace SIAC_NET_Estacionamientos.Formularios
{
    public partial class FrmPagarCargoAbonado : Form
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public string C_LOCAL;
        public string C_NUMEROPLACA;
        public int N_LOCAL;
        public int N_IDCAJERO;

        BE_VTA_VENTAS e_Documento = new BE_VTA_VENTAS();
        List<BE_VTA_VENTASDET> l_DocumentoDet = new List<BE_VTA_VENTASDET>();
        List<BE_VTA_VENTASDOC> l_DetDoc = new List<BE_VTA_VENTASDOC>();
        List<BE_VTA_VENTASOCT> l_DetOCT = new List<BE_VTA_VENTASOCT>();
        List<BE_VTA_VENTASDAT> l_DetDat = new List<BE_VTA_VENTASDAT>();
        List<BE_CON_DIARIO> l_Diario = new List<BE_CON_DIARIO>();

        SIAC_Objetos.Funciones objFunciones = new SIAC_Objetos.Funciones();
        CN_sys_empresalocal o_emploc = new CN_sys_empresalocal();
        CN_sun_tipmon objMoneda = new CN_sun_tipmon();
        CN_sun_tipcam ObjTC = new CN_sun_tipcam();
        Cls_NumeroLetra funLet = new Cls_NumeroLetra();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_est_tipocliente o_tipcli = new CN_est_tipocliente();
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
        int N_IDLIBRO = 0;

        DataTable dtLista = new DataTable();
        DataTable dtClientes = new DataTable();
        DataTable dtLocal = new DataTable();
        DataTable dtCajero = new DataTable();
        DataTable dtservicio = new DataTable();
        DataTable dtTipDocumento = new DataTable();
        DataTable dtMoneda = new DataTable();
        DataTable dtLocSet = new DataTable();
        DataTable dtTC = new DataTable();
        DataTable dttipcli = new DataTable();
        DataTable dtdocimp = new DataTable();
        DataTable dtpcite = new DataTable();
        DataTable dtimp = new DataTable();
        DataTable dtdoccuecon = new DataTable();

        // PARA ALMACENAR EL VALOR DE LA FACTURA
        double douPrecioTotal = 0;
        double douPrecioTotalSinIGV = 0;
        double douValorIGV = 0;
        string C_IDCAJERO;
        string C_IDLOCAL;
        int N_IDSERVICIODEFAULT = 0;
        int N_IDDOCUMENTODEFAULT = 0;
        string c_NUMSERFAC = "";
        string c_NUMSERBOL = "";
        string c_NUMSERTIC = "";
        int N_VISTAPREVIA = 0;                        // INDICA SI SE MOSTRARA UNA VISTA PREVIA DE LA IMPRESION 1 = SIN VISTA PREVIA;  2 = VER VISTA PREVIA
        int N_TIPOCOBRANZA = 0;

        string[,] arrCabecera1 = new string[11, 5];
        bool b_agregando = false;

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^-" + (char)8;
        string strCaracteres2 = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-" + (char)8;

        public FrmPagarCargoAbonado()
        {
            InitializeComponent();
        }

        private void FrmPagarCargoAbonado_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            DataTableCargar();
            ConfigurarForm();
            CargarDatos();
            MostrarDatos();
            booAgregando = false;
            
            FgReg.Focus();
        }
        void CargarDatos2()
        {
            DataTable dtRes = new DataTable();

            dtRes = funGen.DataTableFiltrar(dtClientes, "n_id = " + LbIdCliente.Text + "");

            if (dtRes.Rows.Count != 0)
            {
                TxtNumPla.Text = dtRes.Rows[0]["c_numpla"].ToString();
                TxtApeNom.Text = dtRes.Rows[0]["c_nom"].ToString();
                CboTipCliCli.SelectedValue = Convert.ToInt32(dtRes.Rows[0]["n_idtipcli"]);
                LbIdCliente.Text = dtRes.Rows[0]["n_id"].ToString();
                CboTipDoc.SelectedValue = Convert.ToInt32(dtRes.Rows[0]["n_tipdocfac"]);
                if (Convert.ToInt32(CboTipDoc.SelectedValue) == 90)
                {
                    N_IDLIBRO = 33;
                }
                else
                {
                    N_IDLIBRO = 14;
                }   

                if (Convert.ToInt32(CboTipDoc.SelectedValue) == 2) { TxtNumSer.Text = c_NUMSERFAC; }
                if (Convert.ToInt32(CboTipDoc.SelectedValue) == 4) { TxtNumSer.Text = c_NUMSERBOL; }
                if (Convert.ToInt32(CboTipDoc.SelectedValue) == 90) { TxtNumSer.Text = c_NUMSERTIC; }

                CN_est_cargos o_cargo = new CN_est_cargos(STU_SISTEMA);
                o_cargo.STU_SISTEMA = STU_SISTEMA;
                o_cargo.Consulta1(Convert.ToInt32(LbIdCliente.Text));
                dtRes = o_cargo.dtListar;
                o_cargo = null;

                if (dtRes.Rows.Count != 0)
                {
                    b_agregando = true;
                    funFlex.FlexMostrarDatos(FgReg, arrCabecera1, dtRes, 2, true);
                    HallarTotales();
                    b_agregando = false;
                }
            }

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            objTipDoc.mysConec = o_conec.mysConec;
            TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text);
            o_conec = null;
            //TxtAbono.Focus();
        }
        void CargarDatos()
        {
            DataTable dtRes = new DataTable();
           
            dtRes = funGen.DataTableFiltrar(dtClientes, "c_numpla = '" + C_NUMEROPLACA + "' AND n_idpla = "+ N_LOCAL.ToString() +"");

            if (dtRes.Rows.Count != 0)
            {
                TxtNumPla.Text = dtRes.Rows[0]["c_numpla"].ToString();
                TxtApeNom.Text = dtRes.Rows[0]["c_nom"].ToString();
                CboTipCliCli.SelectedValue = Convert.ToInt32(dtRes.Rows[0]["n_idtipcli"]);
                LbIdCliente.Text = dtRes.Rows[0]["n_id"].ToString();
                CboTipDoc.SelectedValue = Convert.ToInt32(dtRes.Rows[0]["n_tipdocfac"]);

                if (Convert.ToInt32(CboTipDoc.SelectedValue) == 2) { TxtNumSer.Text = c_NUMSERFAC; }
                if (Convert.ToInt32(CboTipDoc.SelectedValue) == 4) { TxtNumSer.Text = c_NUMSERBOL; }
                if (Convert.ToInt32(CboTipDoc.SelectedValue) == 90) { TxtNumSer.Text = c_NUMSERTIC; }

                CN_est_cargos o_cargo = new CN_est_cargos(STU_SISTEMA);
                o_cargo.STU_SISTEMA = STU_SISTEMA;
                o_cargo.Consulta1(Convert.ToInt32(LbIdCliente.Text));
                dtRes = o_cargo.dtListar;
                o_cargo = null;

                if (dtRes.Rows.Count != 0)
                {
                    b_agregando = true;
                    funFlex.FlexMostrarDatos(FgReg, arrCabecera1, dtRes, 2, true);
                    HallarTotales();
                    b_agregando = false;
                }
            }

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            objTipDoc.mysConec = o_conec.mysConec;
            TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text);
            o_conec = null;
        }
        void HallarTotales()
        {
            double n_valor = 0;
            int n_row = 0;
            string c_dato = "";

            for (n_row = 2; n_row <= FgReg.Rows.Count - 1; n_row++)
            {
                c_dato = FgReg.GetData(n_row, 7).ToString();
                if (c_dato == "True")
                {
                    n_valor = n_valor + Convert.ToDouble(FgReg.GetData(n_row, 6));
                }
            }
            LblImpPag.Text = n_valor.ToString("0.00");
            TxtImpPag.Text = n_valor.ToString("0.00");
        }
        void MostrarDatos()
        {

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

            CN_est_localsetup o_locset = new CN_est_localsetup(STU_SISTEMA);
            o_locset.STU_SISTEMA = STU_SISTEMA;
            dtLocSet = o_locset.Listar(STU_SISTEMA.EMPRESAID);
            o_locset = null;

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            o_emploc.mysConec = o_conec.mysConec;
            dtLocal = o_emploc.Listar(STU_SISTEMA.EMPRESAID, 0);

            objMoneda.mysConec = o_conec.mysConec;
            dtMoneda = objMoneda.Listar();                              // CARGAMOS TODAS MONEDAS

            o_tipcli.mysConec = o_conec.mysConec;
            o_tipcli.Listar();
            dttipcli = o_tipcli.dtListar;

            objTipDoc.mysConec = o_conec.mysConec;
            dtTipDocumento = objTipDoc.Listar_puntoventa();       // CARGAMOS TIPOS DE DOCUMENTO PARA VENTAS

            objMoneda.mysConec = o_conec.mysConec;
            dtMoneda = objMoneda.Listar();                              // CARGAMOS TODAS MONEDAS

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
            o_conec = null;
        }
        void ConfigurarForm()
        {
            string c_nomarc = @"C:\SSF-NET\estacionamiento.ini";
            //string N_IDEMPRESA = funGen.IniLeerSeccion(c_nomarc, "INFORMACION", "EMPRESA").ToString();
            C_IDLOCAL = funGen.IniLeerSeccion(c_nomarc, "INFORMACION", "LOCAL").ToString();
            C_IDCAJERO = funGen.IniLeerSeccion(c_nomarc, "INFORMACION", "CAJERO").ToString();

            this.Width = 727;
            this.Height = 525;

            arrCabecera1[0, 0] = "Tip Doc.";
            arrCabecera1[0, 1] = "35";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_tipdocdes";

            arrCabecera1[1, 0] = "Nº Documento";
            arrCabecera1[1, 1] = "115";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_numdoc";

            arrCabecera1[2, 0] = "M";
            arrCabecera1[2, 1] = "35";
            arrCabecera1[2, 2] = "C";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "c_mondes";
            
            arrCabecera1[3, 0] = "Servicio";
            arrCabecera1[3, 1] = "320";
            arrCabecera1[3, 2] = "C";
            arrCabecera1[3, 3] = "";
            arrCabecera1[3, 4] = "c_desser";

            arrCabecera1[4, 0] = "Imp. Bruto";
            arrCabecera1[4, 1] = "70";
            arrCabecera1[4, 2] = "D";
            arrCabecera1[4, 3] = "0.00";
            arrCabecera1[4, 4] = "n_imptot";

            arrCabecera1[5, 0] = "Saldo";
            arrCabecera1[5, 1] = "70";
            arrCabecera1[5, 2] = "D";
            arrCabecera1[5, 3] = "0.00";
            arrCabecera1[5, 4] = "n_impsal";

            arrCabecera1[6, 0] = "Pagar";
            arrCabecera1[6, 1] = "40";
            arrCabecera1[6, 2] = "B";
            arrCabecera1[6, 3] = "";
            arrCabecera1[6, 4] = "b_pagar";

            arrCabecera1[7, 0] = "n_idcar";
            arrCabecera1[7, 1] = "0";
            arrCabecera1[7, 2] = "N";
            arrCabecera1[7, 3] = "";
            arrCabecera1[7, 4] = "n_idcar";

            arrCabecera1[8, 0] = "n_iddoc";
            arrCabecera1[8, 1] = "0";
            arrCabecera1[8, 2] = "N";
            arrCabecera1[8, 3] = "";
            arrCabecera1[8, 4] = "n_id";

            arrCabecera1[9, 0] = "n_idser";
            arrCabecera1[9, 1] = "0";
            arrCabecera1[9, 2] = "N";
            arrCabecera1[9, 3] = "";
            arrCabecera1[9, 4] = "n_idser";

            arrCabecera1[10, 0] = "n_idunimed";
            arrCabecera1[10, 1] = "0";
            arrCabecera1[10, 2] = "N";
            arrCabecera1[10, 3] = "";
            arrCabecera1[10, 4] = "n_idunimed";

            funFlex.FlexMostrarDatos(FgReg, arrCabecera1, dtLista, 2, false);

            DataTable dtResul = new DataTable();
            dtResul = funGen.DataTableFiltrar(dtCajero, "n_idloc = " + N_LOCAL.ToString() + "");
            funGen.ComboBoxCargarDataTable(CboCajero, dtResul, "n_id", "c_cajapenom");
            CboCajero.SelectedValue = N_IDCAJERO;

            funGen.ComboBoxCargarDataTable(CboTipCliCli, dttipcli, "n_id", "c_des");
            
            funGen.ComboBoxCargarDataTable(CboTipDoc, dtTipDocumento, "n_id", "c_des");

            funGen.ComboBoxCargarDataTable(CboMoneda, dtMoneda, "n_id", "c_des");
            CboMoneda.SelectedValue = 115;

            TxtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LblTc.Text = objFunciones.ObtenerTC(dtTC, DateTime.Now.ToString("dd/MM/yyyy")).ToString("0.000");

            dtLocSet = funGen.DataTableFiltrar(dtLocSet, "n_idloc = " + C_IDLOCAL + "");
            N_IDSERVICIODEFAULT = Convert.ToInt32(dtLocSet.Rows[0]["n_idserdef"]);
            c_NUMSERFAC = dtLocSet.Rows[0]["c_numserfac"].ToString();
            c_NUMSERBOL = dtLocSet.Rows[0]["c_numserbol"].ToString();
            c_NUMSERTIC = dtLocSet.Rows[0]["c_numsertik"].ToString();
            N_VISTAPREVIA = Convert.ToInt32(dtLocSet.Rows[0]["n_vispre"]);
            N_IDDOCUMENTODEFAULT = Convert.ToInt32(dtLocSet.Rows[0]["n_iddocdef"]);
            N_TIPOCOBRANZA = Convert.ToInt32(dtLocSet.Rows[0]["n_idtipcob"]);

            CboTipDoc.SelectedValue = N_IDDOCUMENTODEFAULT;

            string c_nomarc2 = ConfigurationManager.AppSettings["PathIniFile"];
            //N_IDALMACEN = Convert.ToInt32(funDatos.IniLeerSeccion(c_nomarc, "SISTEMA", "ALMACEN").ToString());
            c_NUMSERFAC = funGen.IniLeerSeccion(c_nomarc2, "SISTEMA", "SERFAC").ToString();
            c_NUMSERBOL = funGen.IniLeerSeccion(c_nomarc2, "SISTEMA", "SERBOL").ToString();
            c_NUMSERTIC = funGen.IniLeerSeccion(c_nomarc2, "SISTEMA", "SERVAL").ToString();

            TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text);

            this.Text = "ESTACIONAMIENTOS - COBRANZA DE ABONADOS";
            douIGVTasa = 18;
            FgReg.AllowEditing = false;
        }

        private void FgReg_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (b_agregando == true) { return; }
            if (e.Col == 7)
            {
                HallarTotales();
            }
        }

        private void CmdCan_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CmdAce_Click(object sender, EventArgs e)
        {
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
            if (FgReg.Rows.Count == 2)
            {
                MessageBox.Show("¡ No hay cargos a cobrar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgReg.Focus();
                return;
            }
            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento que se emitira, debe definir el documento en el maestro de clientes !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboTipDoc.Focus();
                return;
            }
            if (Convert.ToDouble(TxtImpPag.Text) == 0)
            {
                MessageBox.Show("¡ El abonado no tiene cargos pendientes !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtImpPag.Focus();
                return;
            }

            CN_vta_ventas o_ventas = new CN_vta_ventas();
            string c_dato = "";
            AsignarEntidad();

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            o_ventas.mysConec = o_conec.mysConec;
            o_ventas.LstDetalle = l_DocumentoDet;
            o_ventas.LstDocumentos = l_DetDoc;
            o_ventas.LstDetalleOCT = l_DetOCT;
            o_ventas.LstDatos = l_DetDat;
            o_ventas.l_diario = l_Diario;
            o_ventas.STU_SISTEMA = STU_SISTEMA;

            int n_row = 0;
            int n_idcargo = 0;
            for (n_row = 0; n_row <= FgReg.Rows.Count - 1; n_row++)
            {
                c_dato = FgReg.GetData(n_row, 7).ToString();

                if (c_dato == "True")
                {
                    n_idcargo = Convert.ToInt32(FgReg.GetData(n_row, 9));
                }
            }
            if (o_ventas.Insertar2(e_Documento, n_idcargo) == true)
            {
                CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
                objRegistro.STU_SISTEMA = STU_SISTEMA;
                objRegistro.ImprimirComprobantePago(STU_SISTEMA.EMPRESAID, Convert.ToInt32(o_ventas.n_IdGenerado), "", 0, N_VISTAPREVIA,1);
                objRegistro = null;
            }
            else
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + o_ventas.IntErrorNumber.ToString() + " = " + o_ventas.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            CmdCan_Click(sender, e);
            o_conec = null;
        }
        void AsignarEntidad()
        {
            DataTable dtresul = new DataTable();
            int n_row = 2;
            string c_dato = "";
            string C_RECIBO = "";
            int N_SERVICIO = 0;
            int N_UNIMED = 0;
            string C_SERVICIO = "";
            string C_PERIODO = "";

            for (n_row = 0; n_row <= FgReg.Rows.Count - 1; n_row++)
            {
                c_dato = FgReg.GetData(n_row, 7).ToString();

                if (c_dato == "True")
                { 
                    C_RECIBO = FgReg.GetData(n_row, 2).ToString();
                    N_SERVICIO = Convert.ToInt32(FgReg.GetData(n_row, 10).ToString());
                    N_UNIMED = Convert.ToInt32(FgReg.GetData(n_row, 11).ToString());
                    //C_PERIODO = "DEL " + Convert.ToDateTime(FgReg.GetData(n_row, 4)).ToString("dd/MM/yy") + " AL " + Convert.ToDateTime(FgReg.GetData(n_row, 4)).AddDays(30).ToString("dd/MM/yy");

                    c_dato = FgReg.GetData(n_row, 4).ToString();
                    C_SERVICIO = c_dato;
                }
            }

            l_DocumentoDet.Clear();
            l_DetDoc.Clear();
            l_DetOCT.Clear();

            e_Documento.n_id = 0;
            e_Documento.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Documento.n_anotra = STU_SISTEMA.ANOTRABAJO;
            e_Documento.n_idmes = STU_SISTEMA.MESTRABAJO;
            e_Documento.n_idlib = N_IDLIBRO;
            e_Documento.c_numreg = "";
            e_Documento.n_idtippro = 23;
            e_Documento.n_idcli = Convert.ToInt32(LbIdCliente.Text);
            e_Documento.n_idpunvencli = 0;
            e_Documento.n_idtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);
            e_Documento.c_numser = TxtNumSer.Text;
            e_Documento.c_numdoc = TxtNumDoc.Text;
            if (e_Documento.n_idmes == 0)
            {
                e_Documento.d_fchreg = Convert.ToDateTime("01/01/" + e_Documento.n_anotra.ToString("0000"));
            }
            else
            {
                e_Documento.d_fchreg = Convert.ToDateTime("01/" + TxtFecha.Text.Substring(3, 2) + "/" + TxtFecha.Text.Substring(6, 4));
            }
            e_Documento.d_fchdoc = Convert.ToDateTime(TxtFecha.Text);
            e_Documento.d_fchven = Convert.ToDateTime(TxtFecha.Text);
            e_Documento.n_idconpag = 1;
            e_Documento.n_idmon = Convert.ToInt32(CboMoneda.SelectedValue);
            e_Documento.n_impbru = (Convert.ToDouble(TxtImpPag.Text) / ((douIGVTasa / 100) + 1));
            e_Documento.n_impbru2 = 0;
            e_Documento.n_impbru3 = 0;
            e_Documento.n_impinaf = 0;
            e_Documento.n_impigv = (Convert.ToDouble(TxtImpPag.Text) - (Convert.ToDouble(TxtImpPag.Text) / ((douIGVTasa / 100) + 1)));
            e_Documento.n_impisc = 0;
            e_Documento.n_impotr = 0;
            e_Documento.n_imptotven = Convert.ToDouble(TxtImpPag.Text);
            e_Documento.n_tc = Convert.ToDouble(LblTc.Text);
            e_Documento.n_impsal = Convert.ToDouble(TxtImpPag.Text);
            e_Documento.n_idven = 0;
            e_Documento.n_tasaigv = douIGVTasa;
            e_Documento.c_glosa = "COBRANZA DEL CARGO Nº " + C_RECIBO;
            e_Documento.n_impsubtot = (Convert.ToDouble(TxtImpPag.Text) / ((douIGVTasa / 100) + 1));
            e_Documento.n_pordsc = 0;
            e_Documento.n_idtipope = 1;

            e_Documento.n_idtipdocref = 0;
            e_Documento.n_iddocref = 0;

            e_Documento.c_serdocref = "";
            e_Documento.c_numdocref = "";

            string c_mon = "";
            if (Convert.ToDouble(CboMoneda.SelectedValue) == 115) { c_mon = "SOLES."; }
            if (Convert.ToDouble(CboMoneda.SelectedValue) == 151) { c_mon = "DOLARES AMERICANOS."; }
            e_Documento.c_numlet = funLet.Convertir(TxtImpPag.Text, true, c_mon);

            e_Documento.n_oriitem = 1;             // INDICAMOS QUE LA VENTA NO TIENE GUIA DE REMISION
            e_Documento.n_anulado = 0;
            e_Documento.c_motnc = "";

            e_Documento.n_idforpag = 1;            // INDICAMOS QUE LA FORMA DE PAGO ES EN EFECTIVO 
            e_Documento.n_idtarcre = 0;            // NO HAY TARJETA DE CREDITO

            // PREPARAMOS EL DETALLE DE LA VENTA
            BE_VTA_VENTASDET BE_Detalle = new BE_VTA_VENTASDET();

            BE_Detalle.n_idvta = e_Documento.n_id;
            BE_Detalle.n_canpro = 1;

            BE_Detalle.n_iditem = N_SERVICIO;

            //N_UNIMED = Convert.ToInt32(funGen.DataTableBuscar(dtservicio, "n_id", "n_idunimed", N_SERVICIO.ToString(), "N"));
            BE_Detalle.n_idunimed = N_UNIMED;
            BE_Detalle.n_preunibru = (Convert.ToDouble(TxtImpPag.Text) / ((douIGVTasa / 100) + 1));
            BE_Detalle.n_preuninet = (Convert.ToDouble(TxtImpPag.Text) / ((douIGVTasa / 100) + 1));
            BE_Detalle.n_imptot = (Convert.ToDouble(TxtImpPag.Text) / ((douIGVTasa / 100) + 1));
            BE_Detalle.c_desusu = C_SERVICIO;
            BE_Detalle.n_idtipven = 0;
            BE_Detalle.n_pordsc = 0;
            BE_Detalle.n_porigv = douIGVTasa;
            BE_Detalle.n_preuninetigv = Convert.ToDouble(TxtImpPag.Text);
            BE_Detalle.n_imptotigv = Convert.ToDouble(TxtImpPag.Text);
            BE_Detalle.n_idtipafeigv = 1;
            BE_Detalle.c_datadi = "";
            l_DocumentoDet.Add(BE_Detalle);

            l_DetOCT.Clear();
            BE_VTA_VENTASOCT entOC = new BE_VTA_VENTASOCT();

            ////  1001 - Total valor de venta - operaciones gravadas
            entOC.n_idvta = 0;
            entOC.n_idcon = 1;
            entOC.n_importe = (Convert.ToDouble(TxtImpPag.Text) / ((douIGVTasa / 100) + 1));
            l_DetOCT.Add(entOC);

            l_DetDat.Clear();
            BE_VTA_VENTASDAT entDat = new BE_VTA_VENTASDAT();
            entDat.n_idvta = 0;
            entDat.n_idcaj = Convert.ToInt32(CboCajero.SelectedValue);
            entDat.c_cajnom = STU_SISTEMA.USUARIOALIAS; //CboCajero.Text;
            entDat.n_idloc = Convert.ToInt32(N_LOCAL);
            entDat.c_locdes = C_LOCAL;
            entDat.h_horemi = DateTime.Now.ToString("HH:mm:ss");
            entDat.c_numpla = TxtNumPla.Text;
            entDat.c_horini = "";
            entDat.c_horfin = "";
            entDat.c_tiempousu = "";

            l_DetDat.Add(entDat);

            double n_valor = 0;
            string c_numasi = "";
            int n_idcueite = 0;
            int n_idcuedoc = 0;
            int n_idcueigv = 0;
            string c_abrtipdoc = "";
            DataTable dtResult = new DataTable();

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            CN_con_diario o_diario = new CN_con_diario();
            o_diario.mysConec = o_conec.mysConec;
            c_numasi = o_diario.ObtenerUltimoAsiento(STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 14,STU_SISTEMA.EMPRESAID);
            o_conec = null;


            // OBTENEMOS EL ID DE LA CUENTA CONTABLE DEL DOCUMENTO DE VENTA
            dtResult = funGen.DataTableFiltrar(dtdoccuecon, "n_idtipdoc = " + Convert.ToInt32(CboTipDoc.SelectedValue) + " AND n_idmon = " + Convert.ToInt32(CboMoneda.SelectedValue) + "");
            if (dtResult.Rows.Count == 0)
            {
                MessageBox.Show("¡ El tipo de documento seleccionado no tiene cuenta contable asignada, asignele una cuenta contable en el menu contabilidad !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            n_idcuedoc = Convert.ToInt32(dtResult.Rows[0]["n_idcueven"]);

            // OBTENEMOS EL ID DE LA CUENTA CONTABLE DEL IMPUESTO DE LA VENTA
            dtResult = funGen.DataTableFiltrar(dtdocimp, "n_idtipdoc = " + Convert.ToInt32(CboTipDoc.SelectedValue) + "");
            if (dtResult.Rows.Count == 0)
            {
                MessageBox.Show("¡ El tipo de documento seleccionado no tiene un impuesto asignado, asignele una cuenta contable en el menu contabilidad !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            n_idcueigv = Convert.ToInt32(dtResult.Rows[0]["n_idcueven"]);

            // OBTENEMOS EL ID DE LA CUENTA CONTABLE DEL SERVICIO
            dtResult = funGen.DataTableFiltrar(dtpcite, "n_iditem = " + N_SERVICIO + "");
            if (dtResult.Rows.Count == 0)
            {
                MessageBox.Show("¡ El item seleccionado no tiene cuenta contable asignada, asignele una cuenta contable en el menu contabilidad !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            n_idcueite = Convert.ToInt32(dtResult.Rows[0]["n_idpcven"]);

            // OBTENEMOS LA ABREVIATURA DEL TIPO DE DOUCMENTO
            dtResult = funGen.DataTableFiltrar(dtTipDocumento, "n_id = " + Convert.ToInt32(CboTipDoc.SelectedValue) + "");
            c_abrtipdoc = dtResult.Rows[0]["c_abr"].ToString();


            // ******************************************
            // CREAMOS LOS ASIENTOS CONTABLES DE LA VENTA
            l_Diario.Clear();

            // *****************
            // AGREGAMOS EL DEBE
            BE_CON_DIARIO ediario = new BE_CON_DIARIO();

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
                ediario.n_impdebsol = Convert.ToDouble(TxtImpPag.Text);
                ediario.n_imphabsol = 0;

                ediario.n_impdebdol = Convert.ToDouble(TxtImpPag.Text) / Convert.ToDouble(LblTc.Text);
                ediario.n_imphabdol = 0;
            }
            else
            {
                ediario.n_impdebsol = Convert.ToDouble(TxtImpPag.Text) * Convert.ToDouble(LblTc.Text);
                ediario.n_imphabsol = 0;

                ediario.n_impdebdol = Convert.ToDouble(TxtImpPag.Text);
                ediario.n_imphabdol = 0;
            }

            ediario.d_fchasi = Convert.ToDateTime(TxtFecha.Text);
            ediario.d_orifchdoc = Convert.ToDateTime(TxtFecha.Text);
            ediario.n_oriid = 0;
            ediario.n_oriidtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);
            ediario.n_oriidtipmon = Convert.ToInt32(CboMoneda.SelectedValue);
            ediario.c_orinumdoc = TxtNumSer.Text + "-" + TxtNumDoc.Text;
            ediario.c_origlo = "";
            ediario.c_oridestipmon = CboMoneda.Text;
            ediario.c_oridestipdoc = c_abrtipdoc;
            ediario.c_orinomcli = TxtApeNom.Text;
            ediario.c_orinumruc = "";

            l_Diario.Add(ediario);

            // *****************************
            // ESCRIBIMOS EL IGV DE LA VENTA
            if ((Convert.ToDouble(CboTipDoc.SelectedValue) == 2) || (Convert.ToDouble(CboTipDoc.SelectedValue) == 4))
            {
                BE_CON_DIARIO ediario2 = new BE_CON_DIARIO();
                n_valor = 0;
                n_valor = (Convert.ToDouble(TxtImpPag.Text) - (Convert.ToDouble(TxtImpPag.Text) / 1.18));

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

                ediario2.d_fchasi = Convert.ToDateTime(TxtFecha.Text);
                ediario2.d_orifchdoc = Convert.ToDateTime(TxtFecha.Text);
                ediario2.n_oriid = 0;
                ediario2.n_oriidtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);
                ediario2.n_oriidtipmon = Convert.ToInt32(CboMoneda.SelectedValue);
                ediario2.c_orinumdoc = TxtNumSer.Text + "-" + TxtNumDoc.Text;
                ediario2.c_origlo = "";
                ediario.c_oridestipmon = CboMoneda.Text;
                ediario.c_oridestipdoc = c_abrtipdoc;
                ediario.c_orinomcli = TxtApeNom.Text;
                ediario.c_orinumruc = "";

                l_Diario.Add(ediario2);
            }

            // *******************************
            // ESCRIBIMOS EL HABER DEL ASIENTO
            BE_CON_DIARIO ediario3 = new BE_CON_DIARIO();

            if ((Convert.ToDouble(CboTipDoc.SelectedValue) == 2) || (Convert.ToDouble(CboTipDoc.SelectedValue) == 4))
            {
                n_valor = (Convert.ToDouble(TxtImpPag.Text) / 1.18);
            }
            else
            {
                n_valor = Convert.ToDouble(TxtImpPag.Text);
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

            ediario3.d_fchasi = Convert.ToDateTime(TxtFecha.Text);
            ediario3.d_orifchdoc = Convert.ToDateTime(TxtFecha.Text);
            ediario3.n_oriid = 0;
            ediario3.n_oriidtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);
            ediario3.n_oriidtipmon = Convert.ToInt32(CboMoneda.SelectedValue);
            ediario3.c_orinumdoc = TxtNumSer.Text + "-" + TxtNumDoc.Text;
            ediario3.c_origlo = "";
            ediario.c_oridestipmon = CboMoneda.Text;
            ediario.c_oridestipdoc = c_abrtipdoc;
            ediario.c_orinomcli = TxtApeNom.Text;
            ediario.c_orinumruc = "";

            l_Diario.Add(ediario3);
        }

        private void FgReg_Enter(object sender, EventArgs e)
        {

        }

        private void FgReg_EnterCell(object sender, EventArgs e)
        {
            if (FgReg.Col == 7)
            {
                FgReg.AllowEditing = true;
            }
            else
            {
                FgReg.AllowEditing = false;
            }
        }
        private void CmdBusTra_Click(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();

            dtResul = funGen.DataTableFiltrar(dtClientes, "n_idpla = " + C_IDLOCAL + "");

            CN_est_clientes o_cliente = new CN_est_clientes(STU_SISTEMA);
            o_cliente.STU_SISTEMA = STU_SISTEMA; 
            dtResul = o_cliente.BuscarCliente(dtResul, "n_id", "");
            o_cliente = null;

            if (dtResul != null)
            {
                TxtNumPla.Text = "";
                TxtApeNom.Text = "";
                LbIdCliente.Text = "";

                if (dtResul.Rows.Count != 0)
                {
                    TxtNumPla.Text = dtResul.Rows[0]["c_numpla"].ToString();
                    TxtApeNom.Text = dtResul.Rows[0]["c_nom"].ToString();
                    LbIdCliente.Text = dtResul.Rows[0]["n_id"].ToString();
                    FgReg.Rows.Count = 2;
                    LblImpPag.Text = "";
                    TxtImpPag.Text = "";
                    CargarDatos2();
                    //TxtHorIni.Text = DateTime.Now.ToString("HH:mm:ss");

                    //if (Convert.ToInt32(dtResul.Rows[0]["n_idtipcli"]) == 2)
                    //{
                    //    CboSer.SelectedValue = Convert.ToInt32(dtResul.Rows[0]["n_idser"]);
                    //}

                    //DataTable dtResul2 = new DataTable();
                    //dtResul2 = funGen.DataTableFiltrar(dtLista, "(c_clinumpla = '" + TxtNumPla.Text + "') AND (n_iddocven = 0)");

                    //if (dtResul2.Rows.Count == 1)
                    //{
                    //    MessageBox.Show("¡ Ya existe un ingreso sin cerrar para la placa Nº " + TxtNumPla.Text + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    //    TxtNumPla.Text = "";
                    //    TxtCliente.Text = "";
                    //    LblIdCliente.Text = "";
                    //    TxtHorIni.Text = "";
                    //    TxtNumPla.Focus();
                    //    return;
                    //}
                }
            }
        }

        private void TxtNumPla_TextChanged(object sender, EventArgs e)
        {

        }

        private void CboTipDoc_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 2) { TxtNumSer.Text = c_NUMSERFAC; }
            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 4) { TxtNumSer.Text = c_NUMSERBOL; }
            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 90) { TxtNumSer.Text = c_NUMSERTIC; }

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            objTipDoc.mysConec = o_conec.mysConec;
            TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text);
            o_conec = null;

            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 90)
            {
                N_IDLIBRO = 33;
            }
            else
            {
                N_IDLIBRO = 14;
            }     
        }
    }
}
