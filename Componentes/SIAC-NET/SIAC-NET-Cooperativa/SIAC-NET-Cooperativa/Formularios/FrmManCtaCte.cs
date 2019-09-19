using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Cooperativa;
using SIAC_Negocio.Cooperativa;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Ventas;
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
using C1.Win.C1FlexGrid;

namespace SIAC_NET_Cooperativa.Formularios
{
    public partial class FrmManCtaCte : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_coo_socios objRegistros = new CN_coo_socios();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_coo_tiposocio objTipSoc = new CN_coo_tiposocio();
        CN_coo_sociospuestos oboPuestoSocio = new CN_coo_sociospuestos();
        CN_coo_socios objSocios = new CN_coo_socios();
        CN_coo_cargoscab objCargosCab = new CN_coo_cargoscab();
        CN_coo_cargos objCargos = new CN_coo_cargos();
        CN_sun_tipdoccom objTipDocCom = new CN_sun_tipdoccom();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();
        Cls_NumeroLetra funLet = new Cls_NumeroLetra();

        // ENTIDADES LOCALES
        BE_COO_SOCIOS BE_ListaReg = new BE_COO_SOCIOS();
        BE_COO_SOCIOS BE_Registro = new BE_COO_SOCIOS();

        // DATATABLE LOCALES
        DataTable dtRegistros = new DataTable();
        DataTable dtTipDocIde = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtPuestoSocio = new DataTable();
        DataTable dtTipSoc = new DataTable();
        DataTable dtSocios = new DataTable();

        ToolTip myTool = new ToolTip();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[5, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex = new string[20, 5];
        bool booSeEjecuto = false;

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;

        public FrmManCtaCte()
        {
            InitializeComponent();
        }

        private void CmdMostrarDeuda_Click(object sender, EventArgs e)
        {
            int n_row = 0;
            DataTable dtResult = new DataTable();
            CN_coo_cargos objCargos = new CN_coo_cargos();
            string c_dato = "";

            TxtTotal.Text = "";
            TxtTotPag.Text = "";
            TxtSaldo.Text = "";
            FgDeuda.Rows.Count = 2;

            // MOSTRAMOS LA DEUDA DEL PUESTO
            DataTable dtCargosSocio = new DataTable();
            objCargos.mysConec = mysConec;
            objCargos.Consulta3(Convert.ToInt32(LblIdSoc.Text));
            dtCargosSocio = objCargos.dtLista;

            if (OptSolDeu.Checked == true)
            {
                dtResult = funDatos.DataTableFiltrar(dtCargosSocio, "n_detsal <> 0");           //  FILTRAMOS PARA SABER SI TIENE DOCUMENTOS PENDIENTES DE PAGO
            }
            else
            {
                dtResult = dtCargosSocio;
            }
            
            if (dtResult.Rows.Count == 0)
            {
                MessageBox.Show("¡ EL puesto indicado no tiene cargos generados  !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //TxtCodPue.Text = "";
                //TxtNomSoc.Text = "";
                //TxtSer.Text = "";
                //TxtTotal.Text = "";
                //FgDeuda.Rows.Count = 2;
                //LblIdPuesto.Text = "";
                //LblIdSoc.Text = "";
                //funControl.dtpBlanquea(TxtFchIng);
                return;
            }
              
            FgDeuda.Rows.Count = 2;
            if (objCargos.booOcurrioError == false)
            {
                dtResult = funDatos.DataTableOrdenar(dtResult, "n_carmestra");
                for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                {
                    FgDeuda.Rows.Count = FgDeuda.Rows.Count + 1;

                    c_dato = dtResult.Rows[n_row]["n_caranoemi"].ToString();
                    FgDeuda.SetData(FgDeuda.Rows.Count - 1, 1, c_dato);

                    c_dato = dtResult.Rows[n_row]["c_carmesdes"].ToString();
                    FgDeuda.SetData(FgDeuda.Rows.Count - 1, 2, c_dato);

                    c_dato = dtResult.Rows[n_row]["c_cartipdocdes"].ToString();
                    FgDeuda.SetData(FgDeuda.Rows.Count - 1, 3, c_dato);

                    c_dato = dtResult.Rows[n_row]["c_carnumdoc"].ToString();
                    FgDeuda.SetData(FgDeuda.Rows.Count - 1, 4, c_dato);

                    c_dato = dtResult.Rows[n_row]["c_socpuepuedes"].ToString();
                    FgDeuda.SetData(FgDeuda.Rows.Count - 1, 5, c_dato);

                    c_dato = dtResult.Rows[n_row]["c_detcondes"].ToString();
                    FgDeuda.SetData(FgDeuda.Rows.Count - 1, 6, c_dato);

                    c_dato = Convert.ToDouble(funFunciones.NulosN(dtResult.Rows[n_row]["n_detconimp"])).ToString("0.00");
                    FgDeuda.SetData(FgDeuda.Rows.Count - 1, 7, c_dato);

                    c_dato = Convert.ToDouble(funFunciones.NulosN(dtResult.Rows[n_row]["n_detsal"])).ToString("0.00");
                    FgDeuda.SetData(FgDeuda.Rows.Count - 1, 8, c_dato);

                    c_dato = dtResult.Rows[n_row]["c_pagfchdoc"].ToString();
                    if (c_dato == "")
                    {
                        FgDeuda.SetData(FgDeuda.Rows.Count - 1, 10, null);
                    }
                    else
                    {
                        FgDeuda.SetData(FgDeuda.Rows.Count - 1, 10, c_dato);
                    }

                    c_dato = dtResult.Rows[n_row]["c_pagdocpag"].ToString();
                    FgDeuda.SetData(FgDeuda.Rows.Count - 1, 11, c_dato);

                    c_dato = dtResult.Rows[n_row]["c_pagnumdoc"].ToString();
                    FgDeuda.SetData(FgDeuda.Rows.Count - 1, 12, c_dato);

                    //c_dato = dtResult.Rows[n_row]["n_impbru"].ToString();
                    //FgDeuda.SetData(FgDeuda.Rows.Count - 1, 13, c_dato);

                    //c_dato = dtResult.Rows[n_row]["n_impigv"].ToString();
                    //FgDeuda.SetData(FgDeuda.Rows.Count - 1, 14, c_dato);

                    c_dato = dtResult.Rows[n_row]["n_carid"].ToString();
                    FgDeuda.SetData(FgDeuda.Rows.Count - 1, 16, c_dato);

                    c_dato = dtResult.Rows[n_row]["n_caridpue"].ToString();
                    FgDeuda.SetData(FgDeuda.Rows.Count - 1, 17, c_dato);

                    c_dato = dtResult.Rows[n_row]["n_detidcon"].ToString();
                    FgDeuda.SetData(FgDeuda.Rows.Count - 1, 18, c_dato);

                    //******
                    c_dato = dtResult.Rows[n_row]["n_detiddocpag"].ToString();
                    FgDeuda.SetData(FgDeuda.Rows.Count - 1, 19, c_dato);

                    c_dato = dtResult.Rows[n_row]["n_detcor"].ToString();
                    FgDeuda.SetData(FgDeuda.Rows.Count - 1, 20, c_dato);
                    

                    if (Convert.ToDouble(funFunciones.NulosN(dtResult.Rows[n_row]["n_detsal"])) == 0)
                    {
                        PintarCelda(FgDeuda, FgDeuda.Rows.Count - 1, 1, FgDeuda.Rows.Count - 1, 12, Color.Blue);
                    }
                    else
                    {
                        PintarCelda(FgDeuda, FgDeuda.Rows.Count - 1, 1, FgDeuda.Rows.Count - 1, 12, Color.Red);
                    }
                }
            }

            CalcularTotales();
            GenerarNumero();
        }
        void PintarCelda(C1.Win.C1FlexGrid.C1FlexGrid Objeto, int n_Fila1, int n_Columna1, int n_Fila2, int n_Columna2, Color n_Color)
        {
            CellRange rg = Objeto.GetCellRange(n_Fila1, n_Columna1, n_Fila2, n_Columna2);
            rg.StyleNew.ForeColor = n_Color;
        }
        private void FrmManCtaCte_Load(object sender, EventArgs e)
        {
            DataTableCargar();
            ConfigurarFormulario();
        }
        void DataTableCargar()
        {
            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(52);

            oboPuestoSocio.mysConec = mysConec;
            oboPuestoSocio.Consulta1(STU_SISTEMA.EMPRESAID,1);
            dtPuestoSocio = oboPuestoSocio.dtPuestosSocios;

            objSocios.mysConec = mysConec;
            dtSocios = objSocios.Listar(STU_SISTEMA.EMPRESAID);

            objTipSoc.mysConec = mysConec;
            dtTipSoc = objTipSoc.Listar(STU_SISTEMA.EMPRESAID);
        }
        void ConfigurarFormulario()
        {
            this.Text = dtForm.Rows[0]["c_titfor"].ToString();

            this.Height = 559;
            this.Width = 1027;
            //1005; 555
            
            c1Sizer1.Width = this.Width - 19;
            c1Sizer1.Height = this.Height - 81;

            TxtCodPue.Text = "";
            TxtNomSoc.Text = "";
            TxtSer.Text = "";
            TxtTotal.Text = "";
            LblIdPuesto.Text = "";
            LblIdSoc.Text = "";
            funControl.dtpBlanquea(TxtFchIng);
            FgDeuda.Rows.Count = 2;

            OptSolDeu.Checked = true;

            arrCabeceraFlex[0, 0] = "Año";
            arrCabeceraFlex[0, 1] = "35";
            arrCabeceraFlex[0, 2] = "C";
            arrCabeceraFlex[0, 3] = "";
            arrCabeceraFlex[0, 4] = "";

            arrCabeceraFlex[1, 0] = "Mes Trabajo";
            arrCabeceraFlex[1, 1] = "95";
            arrCabeceraFlex[1, 2] = "C";
            arrCabeceraFlex[1, 3] = "";
            arrCabeceraFlex[1, 4] = "";

            arrCabeceraFlex[2, 0] = "Tip. Doc.";
            arrCabeceraFlex[2, 1] = "35";
            arrCabeceraFlex[2, 2] = "C";
            arrCabeceraFlex[2, 3] = "";
            arrCabeceraFlex[2, 4] = "";

            arrCabeceraFlex[3, 0] = "Nº Documento";
            arrCabeceraFlex[3, 1] = "100";
            arrCabeceraFlex[3, 2] = "C";
            arrCabeceraFlex[3, 3] = "";
            arrCabeceraFlex[3, 4] = "";

            arrCabeceraFlex[4, 0] = "Nº Puesto";
            arrCabeceraFlex[4, 1] = "50";
            arrCabeceraFlex[4, 2] = "C";
            arrCabeceraFlex[4, 3] = "";
            arrCabeceraFlex[4, 4] = "";

            arrCabeceraFlex[5, 0] = "Concepto";
            arrCabeceraFlex[5, 1] = "195";
            arrCabeceraFlex[5, 2] = "C";
            arrCabeceraFlex[5, 3] = "";
            arrCabeceraFlex[5, 4] = "";

            arrCabeceraFlex[6, 0] = "Importe";
            arrCabeceraFlex[6, 1] = "60";
            arrCabeceraFlex[6, 2] = "D";
            arrCabeceraFlex[6, 3] = "";
            arrCabeceraFlex[6, 4] = "";

            arrCabeceraFlex[7, 0] = "Saldo";
            arrCabeceraFlex[7, 1] = "60";
            arrCabeceraFlex[7, 2] = "D";
            arrCabeceraFlex[7, 3] = "";
            arrCabeceraFlex[7, 4] = "";

            arrCabeceraFlex[8, 0] = "Acuenta";
            arrCabeceraFlex[8, 1] = "60";
            arrCabeceraFlex[8, 2] = "D";
            arrCabeceraFlex[8, 3] = "";
            arrCabeceraFlex[8, 4] = "";

            arrCabeceraFlex[9, 0] = "Fch. Pago";
            arrCabeceraFlex[9, 1] = "60";
            arrCabeceraFlex[9, 2] = "F";
            arrCabeceraFlex[9, 3] = "dd/MM/yy";
            arrCabeceraFlex[9, 4] = "";

            arrCabeceraFlex[10, 0] = "Tip. Doc. Pago";
            arrCabeceraFlex[10, 1] = "50";
            arrCabeceraFlex[10, 2] = "C";
            arrCabeceraFlex[10, 3] = "";
            arrCabeceraFlex[10, 4] = "";

            arrCabeceraFlex[11, 0] = "Nº Doc. Pago";
            arrCabeceraFlex[11, 1] = "100";
            arrCabeceraFlex[11, 2] = "C";
            arrCabeceraFlex[11, 3] = "";
            arrCabeceraFlex[11, 4] = "";

            arrCabeceraFlex[12, 0] = "Pagar";
            arrCabeceraFlex[12, 1] = "50";
            arrCabeceraFlex[12, 2] = "B";
            arrCabeceraFlex[12, 3] = "";
            arrCabeceraFlex[12, 4] = "";

            arrCabeceraFlex[13, 0] = "Imp. Bruto";
            arrCabeceraFlex[13, 1] = "0";
            arrCabeceraFlex[13, 2] = "D";
            arrCabeceraFlex[13, 3] = "";
            arrCabeceraFlex[13, 4] = "";

            arrCabeceraFlex[14, 0] = "Importe IGV";
            arrCabeceraFlex[14, 1] = "0";
            arrCabeceraFlex[14, 2] = "D";
            arrCabeceraFlex[14, 3] = "";
            arrCabeceraFlex[14, 4] = "";

            arrCabeceraFlex[15, 0] = "idcar";
            arrCabeceraFlex[15, 1] = "0";
            arrCabeceraFlex[15, 2] = "N";
            arrCabeceraFlex[15, 3] = "";
            arrCabeceraFlex[15, 4] = "";

            arrCabeceraFlex[16, 0] = "idpue";
            arrCabeceraFlex[16, 1] = "0";
            arrCabeceraFlex[16, 2] = "N";
            arrCabeceraFlex[16, 3] = "";
            arrCabeceraFlex[16, 4] = "";

            arrCabeceraFlex[17, 0] = "idcon";
            arrCabeceraFlex[17, 1] = "0";
            arrCabeceraFlex[17, 2] = "N";
            arrCabeceraFlex[17, 3] = "";
            arrCabeceraFlex[17, 4] = "";

            arrCabeceraFlex[18, 0] = "idDocPago";
            arrCabeceraFlex[18, 1] = "0";
            arrCabeceraFlex[18, 2] = "N";
            arrCabeceraFlex[18, 3] = "";
            arrCabeceraFlex[18, 4] = "";

            arrCabeceraFlex[19, 0] = "correlativo";
            arrCabeceraFlex[19, 1] = "0";
            arrCabeceraFlex[19, 2] = "N";
            arrCabeceraFlex[19, 3] = "";
            arrCabeceraFlex[19, 4] = "";

            funFlex.FlexMostrarDatos(FgDeuda, arrCabeceraFlex, dtRegistros, 2, false);


            // Set up the delays for the ToolTip.
            myTool.AutoPopDelay = 5000;
            myTool.InitialDelay = 1000;
            myTool.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            myTool.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            myTool.SetToolTip(this.CmdMostrarDeuda, "Mostrar Deuda");
            myTool.SetToolTip(this.CmdGenPag, "Generar Pago");
            myTool.SetToolTip(this.CmdExtPag, "Extornar Pago");
            myTool.SetToolTip(this.CmdGenDeuda, "Generar Deuda");
            myTool.SetToolTip(this.CmdPagAde, "Pago Adelantado");
            myTool.SetToolTip(this.CmdDelDeu, "Eiminar Deuda");
            
        }

        private void FrmManCtaCte_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
            }
        }

        private void TxtCodPue_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtCodPue_Validated(object sender, EventArgs e)
        {
            if (TxtCodPue.Text == "") { return; }
            OptSolDeu.Checked = true;
            CmdMostrarDeuda_Click(sender, e);
        }
        void MostrarDatosSocio(int n_IdSocio)
        {
            DataTable dtResul = new DataTable();
            string c_dato = "";

            dtResul = funDatos.DataTableFiltrar(dtSocios, "n_id = " + n_IdSocio.ToString() + "");
            if (dtResul.Rows.Count != 0)
            {
                TxtNomSoc.Text = dtResul.Rows[0]["c_apenom"].ToString();

                c_dato = funDatos.DataTableBuscar(dtTipSoc, "n_id", "c_des", dtResul.Rows[0]["n_idtipsoc"].ToString(), "N").ToString();
                TxtSer.Text = c_dato;
                TxtFchIng.Text = "";//dtResul.Rows[0]["c_apenom"].ToString();
            }
        }

        private void TxtCodPue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                e.Handled = true;
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

        private void CmdProPen_Click(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            COD_Funciones xFunCoo = new COD_Funciones();
            xFunCoo.mysConec = mysConec;
            xFunCoo.STU_SISTEMA = STU_SISTEMA;
            dtResul = xFunCoo.BuscarSocios();
            if (funFunciones.Obj_ValidarError(xFunCoo.b_OcurrioError, xFunCoo.c_ErrorMensaje) == false) { return; }
            if (dtResul == null) { return; }
            TxtCodPue.Text = dtResul.Rows[0]["c_idenumdoc"].ToString();
            TxtNomSoc.Text = dtResul.Rows[0]["c_apenom"].ToString();
            TxtSer.Text = dtResul.Rows[0]["c_destipsoc"].ToString();
            LblIdSoc.Text = dtResul.Rows[0]["n_id"].ToString();

            CmdMostrarDeuda_Click(sender, e);
            GenerarNumero();
        }
        void GenerarNumero()
        {
            string c_dato = "";
            int n_idtipdoc = 0;

            c_dato = funDatos.DataTableBuscar(dtTipSoc, "c_des", "n_idtipdocfac", TxtSer.Text, "C").ToString();
            n_idtipdoc = Convert.ToInt32(c_dato);

            if (n_idtipdoc == 81) { LblDocGen.Text = "RECIBO DE COBRANZA"; }
            if (n_idtipdoc == 4) { LblDocGen.Text = "BOLETA DE VENTA"; }
            if (n_idtipdoc == 2) { LblDocGen.Text = "FACTURA"; }

            TxtNumSerGen.Text = "0001";
            objTipDocCom.mysConec = mysConec;
            TxtNumDocGen.Text = objTipDocCom.UltimoNumero(STU_SISTEMA.EMPRESAID, n_idtipdoc, TxtNumSerGen.Text);
        }
        private void FrmManCtaCte_Resize(object sender, EventArgs e)
        {
            c1Sizer1.Width = this.Width - 14;
            c1Sizer1.Height = this.Height - 78;
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            objRegistros = null;
            objFormVis = null;
            objTipSoc = null;
            oboPuestoSocio = null;
            objSocios = null;
            mysConec = null;
            this.Close();
        }

        private void CmdGenPag_Click(object sender, EventArgs e)
        {
            if (TxtNumDocGen.Text == "")
            {
                MessageBox.Show("¡ No ha indicado el numero de documento que se va a imprimir !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumDocGen.Focus();
                return;
            }

            int n_idtipdoc = 0;
            string c_numser = "";
            string c_numdoc = "";
            double n_imptc = 0;
            double n_tasaigv = 18;
            double n_imppagar = 0;
            double n_impigv = 0;
            double n_impbru = 0;
            double n_valor = 0;
            int n_row = 2;
            double n_IdGenerado = 0;
            string c_dato = "";

            for (n_row = 2; n_row <= (FgDeuda.Rows.Count - 1); n_row++)
            {
                if (funFunciones.NulosC(FgDeuda.GetData(n_row, 13)).ToString() == "True")
                {
                    n_valor = n_valor + 1;
                }
            }

            if (n_valor == 0)
            {
                MessageBox.Show("¡ No ha indicado que cargos son los que se van a pagar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            c_dato = funDatos.DataTableBuscar(dtTipSoc, "c_des", "n_idtipdocfac", TxtSer.Text, "C").ToString();
            n_idtipdoc = Convert.ToInt32(c_dato);
            c_numser = TxtNumSerGen.Text;
            c_numdoc = TxtNumDocGen.Text;
            //objTipDocCom.mysConec = mysConec;
            //c_numdoc = objTipDocCom.UltimoNumero(STU_SISTEMA.EMPRESAID, n_idtipdoc, c_numser);

            // ACUMULAMOS EL IMPORTE DE LO QUE SE VA A PAGAR (SOLO LO QUE ESTA CON CHECK)
            for (n_row = 2; n_row <= (FgDeuda.Rows.Count - 1); n_row++)
            {
                if (funFunciones.NulosC(FgDeuda.GetData(n_row, 13)).ToString() == "True")
                {
                    n_valor = Convert.ToDouble(FgDeuda.GetData(n_row, 9));
                    n_imppagar = n_imppagar + n_valor;
                }
            }

            CN_vta_ventas objVentas = new CN_vta_ventas();
            BE_VTA_VENTAS entVentas = new BE_VTA_VENTAS();
            List<BE_VTA_VENTASDET> lstVentasDet = new List<BE_VTA_VENTASDET>();

            entVentas.n_idemp = STU_SISTEMA.EMPRESAID;
            entVentas.n_id = 0;
            entVentas.n_anotra = STU_SISTEMA.ANOTRABAJO;
            entVentas.n_idmes = STU_SISTEMA.MESTRABAJO;
            entVentas.n_idlib = 2;
            entVentas.c_numreg = "";
            entVentas.n_idtippro = 23;
            entVentas.n_idcli = Convert.ToInt32(LblIdSoc.Text);
            entVentas.n_idpunvencli = 0;
            entVentas.n_idtipdoc = n_idtipdoc;
            entVentas.c_numser = c_numser;
            entVentas.c_numdoc = c_numdoc;
            entVentas.d_fchreg = Convert.ToDateTime("01/" + STU_SISTEMA.MESTRABAJO.ToString() + "/" + STU_SISTEMA.ANOTRABAJO.ToString());
            entVentas.d_fchdoc = DateTime.Now;
            entVentas.d_fchven = DateTime.Now;
            entVentas.n_idconpag = 1;                                   // INDICAMOS QUE ELPAGO ES AL CONTADO
            entVentas.n_idmon = 115;                                    // INDICAMOS QUE LA MONEDA ES SOLES
            if (TxtSer.Text == "INQUILINO (BOLETA)")
            {
                n_impbru = (n_imppagar / ((n_tasaigv / 100) + 1));
                n_impigv = (n_imppagar - n_impbru);
                //n_imppagar = (n_imppagar);
                entVentas.n_impinaf = 0;
                entVentas.n_impbru = n_impbru;
                entVentas.n_impigv = n_impigv;
                entVentas.n_imptotven = n_imppagar;
                entVentas.n_idtipven = 1;                               // INDICAMOS QUE LA VENTA ES AFECTA AL IGV
            }

            entVentas.n_impbru2 = 0;
            entVentas.n_impbru3 = 0;

            if (TxtSer.Text == "SOCIO")
            {
                entVentas.n_impbru = 0;
                entVentas.n_impinaf = n_imppagar;
                entVentas.n_impigv = 0;
                entVentas.n_imptotven = n_imppagar;
                entVentas.n_idtipven = 3;                               // INDICAMOS QUE LA VENTA ES INAAFECTA AL IGV
            }

            entVentas.n_impisc = 0;
            entVentas.n_impotr = 0;
            entVentas.n_tc = n_imptc;
            entVentas.n_impsal = 0;
            entVentas.n_idven = 0;
            entVentas.n_tasaigv = n_tasaigv;
            entVentas.c_glosa = "";
            entVentas.n_oriitem = 1;
            entVentas.n_anulado = 1;

            entVentas.n_idtipdocref = 0;
            entVentas.n_iddocref = 0;
            entVentas.c_serdocref = "";
            entVentas.c_numdocref = "";
            entVentas.n_idtipdes = 1;
            entVentas.n_impdes = 0;
            entVentas.c_nomcli = TxtNomSoc.Text;
            entVentas.c_dircli = "";
            entVentas.n_idpue = 0;                      // ESTE DATO YA NO SE USA AQUI, SE PASO A DETALLE PORQUE EL CLIENTE PUEDE PAGAR EN UN DOCUMENTO EL CONECPTO DE VARIOS PUESTO
            entVentas.n_idtipope = 1;                   // LE INDICAMOS QUE EL TIPO DE OPERACION ES 1 = VENTA NACIONAL POR DEFAULT


            string c_mon = "soles.";
            //if (Convert.ToDouble(CboMon.SelectedValue) == 115) { c_mon = "soles."; }
            //if (Convert.ToDouble(CboMon.SelectedValue) == 151) { c_mon = "dolares americanos."; }
            entVentas.c_numlet = funLet.Convertir(n_imppagar.ToString("0.00"), true, c_mon);

            //entVentas.c_numlet = funLet.Convertir(n_imppagar.ToString("0.00"), true);
            
            //string c_cadena = "";

            //// CARGAMOS LOS ITEMS DE LA VENTA
            //for (n_row = 2; n_row <= (FgDeuda.Rows.Count - 1); n_row++)
            //{
            //    if (funFunciones.NulosC(FgDeuda.GetData(n_row, 12)).ToString() == "True")
            //    {
            //        if (n_row > 2) { c_cadena = c_cadena + ","; }
            //        c_cadena = c_cadena + FgDeuda.GetData(n_row, 15).ToString();                   // ARMAMOS LA CADENA IN PARA TRAER EL DETALLE DEL DOCUMENTO
            //    }
            //}

            //DataTable dtDetalle = new DataTable();
            //objCargos.mysConec = mysConec;
            //objCargos.Consulta2(c_cadena);

            //if (objCargos.booOcurrioError == true)
            //{
            //    MessageBox.Show("¡ No se pudo realizar el pago de los cargos por el siguiente motivo :" + objCargos.StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    return;
            //}

            //dtDetalle = objCargos.dtLista;

            //for (n_row = 0; n_row <= (dtDetalle.Rows.Count - 1); n_row++)
            //{
            //    BE_VTA_VENTASDET entVtaDet = new BE_VTA_VENTASDET();

            //    entVtaDet.n_idvta = 0;
            //    entVtaDet.n_iditem = Convert.ToInt32(dtDetalle.Rows[n_row]["n_idcon"]);
            //    entVtaDet.c_desusu = dtDetalle.Rows[n_row]["c_descon"].ToString();
            //    entVtaDet.n_idunimed = 726;
            //    entVtaDet.n_canpro = 1;
            //    entVtaDet.n_preunibru = Convert.ToDouble(dtDetalle.Rows[n_row]["n_impbru"]);
            //    entVtaDet.n_impdes = 0;
            //    entVtaDet.n_preuninet = Convert.ToDouble(dtDetalle.Rows[n_row]["n_impbru"]);
            //    entVtaDet.n_imptot = Convert.ToDouble(dtDetalle.Rows[n_row]["n_imptotnet"]);

            //    lstVentasDet.Add(entVtaDet);
            //}

            for (n_row = 2; n_row <= (FgDeuda.Rows.Count - 1); n_row++)
            {
                if (funFunciones.NulosC(FgDeuda.GetData(n_row, 13)).ToString() == "True")
                { 
                    BE_VTA_VENTASDET entVtaDet = new BE_VTA_VENTASDET();

                    entVtaDet.n_idvta = 0;
                    entVtaDet.n_iditem = Convert.ToInt32(FgDeuda.GetData(n_row, 18));

                    c_dato = FgDeuda.GetData(n_row, 6).ToString() + "-" + FgDeuda.GetData(n_row, 1).ToString() + "-" + FgDeuda.GetData(n_row, 2).ToString() + "-" + FgDeuda.GetData(n_row, 5).ToString();
                    entVtaDet.c_desusu = c_dato;
                    entVtaDet.n_idunimed = 726;
                    entVtaDet.n_canpro = 1;

                    double n_valor2 = Convert.ToDouble(FgDeuda.GetData(n_row, 9));

                    n_valor2 = (n_valor2 / ((n_tasaigv / 100) + 1));
                    entVtaDet.n_preunibru = n_valor2;
                    entVtaDet.n_impdes = 0;
                    entVtaDet.n_preuninet = n_valor2;
                    entVtaDet.n_imptot = n_valor2;
                    entVtaDet.n_idpuesto = Convert.ToInt32(FgDeuda.GetData(n_row, 17));
                    lstVentasDet.Add(entVtaDet);
                }
            }

            objVentas.mysConec = mysConec;
            objVentas.LstDetalle = lstVentasDet;
            if (objVentas.Insertar(entVentas) == false)                             // GRABAMOS EL DOCUMENTO DE PAGO
            {
                MessageBox.Show("¡ No se pudo realizar el pago de los cargos por el siguiente motivo :" + objVentas.StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                n_IdGenerado = objVentas.n_IdGenerado;
                int n_idCargo = 0;
                int n_idPuesto = 0;
                int n_idConcepto = 0;
                int n_idSocio = 0;
                double n_impabo = 0;

                // ACTUALIZAMOS LOS CARGOS PAGADO
                for (n_row = 2; n_row <= (FgDeuda.Rows.Count - 1); n_row++)
                {
                    if (funFunciones.NulosC(FgDeuda.GetData(n_row, 13)).ToString() == "True")
                    {
                        n_idCargo = Convert.ToInt32(FgDeuda.GetData(n_row, 16).ToString());
                        n_idPuesto = Convert.ToInt32(FgDeuda.GetData(n_row, 17).ToString());
                        n_idConcepto = Convert.ToInt32(FgDeuda.GetData(n_row, 18).ToString());
                        n_idSocio = Convert.ToInt32(LblIdSoc.Text);
                        n_impabo = Convert.ToDouble(FgDeuda.GetData(n_row, 9));

                        objCargosCab.mysConec = mysConec;
                        objCargosCab.ActualizarCargo(n_idCargo, n_idSocio, n_idPuesto, n_idConcepto, n_IdGenerado, n_impabo);
                        if (objCargosCab.booOcurrioError == true)
                        {
                            MessageBox.Show("¡ ocurrio un error :" + objCargosCab.StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                    }
                }

                // MOSTRAMOS LA IMPRESION DE LA VENTA
                objVentas.STU_SISTEMA = STU_SISTEMA;
                objVentas.ReportImprimirDocumento(n_IdGenerado, n_idtipdoc,false,"", false);

                // MOSTRAMOS NUEVAMENTE TODA LA DEUDA DEL PUESTO
                CmdMostrarDeuda_Click(sender, e);
                
            }
        }

        private void FgDeuda_EnterCell(object sender, EventArgs e)
        {
            if (FgDeuda.Rows.Count == 2) { return; }
            FgDeuda.AllowEditing = false;

            if (FgDeuda.Col == 9)
            {
                double n_Saldo = Convert.ToDouble(FgDeuda.GetData(FgDeuda.Row, 8));
                if (n_Saldo > 0)
                {
                    string c_cheked = funFunciones.NulosC(FgDeuda.GetData(FgDeuda.Row, 13)).ToString();
                    if ((c_cheked == "False") || (c_cheked == ""))
                    {
                        FgDeuda.AllowEditing= false;
                    }
                    else
                    { 
                        FgDeuda.AllowEditing = true;
                    }
                }
            }
            if (FgDeuda.Col == 13)
            {
                double n_Saldo = Convert.ToDouble(FgDeuda.GetData(FgDeuda.Row, 8));
                if (n_Saldo > 0)
                {
                    FgDeuda.AllowEditing = true;
                }
            }
        }

        private void FgDeuda_CellChanged(object sender, RowColEventArgs e)
        {
            if (FgDeuda.Col == 13)
            {
                
                string c_cheked = funFunciones.NulosC(FgDeuda.GetData(FgDeuda.Row, 13)).ToString();
                if ((c_cheked == "False") || (c_cheked == ""))
                {
                    FgDeuda.SetData(FgDeuda.Row, 9, "0.00");
                }
                else
                {
                    double n_Saldo = Convert.ToDouble(FgDeuda.GetData(FgDeuda.Row, 8));
                    FgDeuda.SetData(FgDeuda.Row, 9, n_Saldo.ToString("0.00"));
                    if (n_Saldo == 0)
                    {
                        FgDeuda.SetData(FgDeuda.Row, 13, false);
                    }
                }
            }
            CalcularTotales();

        }
        void CalcularTotales()
        {
            if (FgDeuda.Rows.Count <= 2) { return; }
            int n_row = 0;
            double n_total = 0;
            double n_totpag = 0;
            double n_saldo = 0;
            double n_valor = 0;

            for (n_row = 2; n_row <= FgDeuda.Rows.Count - 1; n_row++)
            {
                n_valor = Convert.ToDouble(FgDeuda.GetData(n_row, 8));                              // OBTENEMOS EL SALDO DE LA DEUDA
                n_total = n_total + n_valor;

                if (funFunciones.NulosC(FgDeuda.GetData(n_row, 13)).ToString() == "True")
                {
                    n_valor = Convert.ToDouble(FgDeuda.GetData(n_row, 9));                           // OBTENEMOS EL IMPORTE A CUENTA
                    n_totpag = n_totpag + n_valor;
                }
            }

            TxtTotal.Text = n_total.ToString("0.00");
            TxtTotPag.Text = n_totpag.ToString("0.00");
            n_saldo = (n_total - n_totpag);
            TxtSaldo.Text = n_saldo.ToString("0.00");
        }
        private void CmdExtPag_Click(object sender, EventArgs e)
        {
            if (FgDeuda.GetData(FgDeuda.Row, 12) == "")
            {
                MessageBox.Show("¡ No se puede extornar este cargo, aun no ha sido pagado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el pago de este documento", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {

                int n_iddoc = Convert.ToInt32(FgDeuda.GetData(FgDeuda.Row, 19));

                CN_coo_cargoscab funCarCab = new CN_coo_cargoscab();
                funCarCab.mysConec = mysConec;
                if (funCarCab.ExtornarPago(n_iddoc, STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblIdSoc.Text)) == true)
                {
                    MessageBox.Show("¡ El pago se extorno con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    CmdMostrarDeuda_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("¡ El pago no se pudo extornar por el siguiente motivo: " + funCarCab.StrErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
             }
        }
        private void OptSolDeu_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void OptTod_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void CmdGenDeuda_Click(object sender, EventArgs e)
        {
            if (TxtCodPue.Text == "")
            {
                MessageBox.Show("¡ Debe de seleccionar un socio para poder agregarle cargos !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtCodPue.Focus();
                return;
            }

            FrmCrearDeuda xfrm = new FrmCrearDeuda();
            xfrm.C_NOMSOCIO = TxtNomSoc.Text;
            xfrm.C_SOCIONUMRUC = TxtCodPue.Text;
            xfrm.C_SOCIOTIPSOC = TxtSer.Text;
            xfrm.N_SOCIOTIPO = Convert.ToInt32(funDatos.DataTableBuscar(dtSocios, "n_id", "n_idtipsoc", LblIdSoc.Text, "N").ToString());
            xfrm.N_SOCIOID = Convert.ToInt32(LblIdSoc.Text);
            xfrm.mysConec = mysConec;
            xfrm.STU_SISTEMA = STU_SISTEMA;
            xfrm.N_TIPOOPERACION = 1;  // LE INDICAMOS AL FORMULARIO QUE SE VA A GENERAR DEUDA PENDIENTE
            xfrm.ShowDialog();
        }
        private void CmdPagAde_Click(object sender, EventArgs e)
        {
            if (TxtCodPue.Text == "")
            {
                MessageBox.Show("¡ Debe de seleccionar un socio para poder agregarle cargos !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtCodPue.Focus();
                return;
            }

            FrmCrearDeuda xfrm = new FrmCrearDeuda();
            xfrm.C_NOMSOCIO = TxtNomSoc.Text;
            xfrm.C_SOCIONUMRUC = TxtCodPue.Text;
            xfrm.C_SOCIOTIPSOC = TxtSer.Text;
            xfrm.N_SOCIOTIPO = Convert.ToInt32(funDatos.DataTableBuscar(dtSocios, "n_id", "n_idtipsoc", LblIdSoc.Text, "N").ToString());
            xfrm.N_SOCIOID = Convert.ToInt32(LblIdSoc.Text);
            xfrm.mysConec = mysConec;
            xfrm.STU_SISTEMA = STU_SISTEMA;
            xfrm.N_TIPOOPERACION = 2;  // LE INDICAMOS AL FORMULARIO QUE SE VA A GENERAR UNA PAGO ADELANTADO
            xfrm.ShowDialog();
        }

        private void CmdImpDeu_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void TxtNumDocGen_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtNumDocGen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                e.Handled = true;
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

        private void TxtNumDocGen_Validated(object sender, EventArgs e)
        {
            if (TxtNumDocGen.Text == "") { return; }

            string c_cad = "0000000000" + TxtNumDocGen.Text;
            TxtNumDocGen.Text =c_cad.Substring(c_cad.Length - 10, 10);
        }

        private void ToolImprimir_Click(object sender, EventArgs e)
        {
            CN_coo_cargos FunCar = new CN_coo_cargos();
            FunCar.mysConec = mysConec;
            FunCar.STU_SISTEMA = STU_SISTEMA;
            FunCar.ReportDeuda(Convert.ToInt32(LblIdSoc.Text));
        }

        private void CmdDelDeu_Click(object sender, EventArgs e)
        {
            string c_dato = FgDeuda.GetData(FgDeuda.Row, 4).ToString();
            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el documento Nº " + c_dato, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            { 
                int n_idcar = Convert.ToInt32(FgDeuda.GetData(FgDeuda.Row, 15));
                int n_idcordet =  Convert.ToInt32(FgDeuda.GetData(FgDeuda.Row, 19));
                objCargos.mysConec = mysConec;
                if (objCargos.EliminarConcepto(n_idcar, n_idcordet) == true)
                {
                    MessageBox.Show("¡ El concepto se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    CmdMostrarDeuda_Click(sender, e);
                }
            }
        }
    }
}
