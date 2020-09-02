using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Sistema;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Contabilidad;
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
using System.IO;

namespace SSF_NET_Ventas.Formularios
{
    public partial class FrmPuntoVenta : Form
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        BE_VTA_VENTAS e_Documento = new BE_VTA_VENTAS();
        List <BE_VTA_VENTASDET> l_DocumentoDet = new List<BE_VTA_VENTASDET>();
        List <BE_VTA_VENTASDOC> l_DetDoc = new List<BE_VTA_VENTASDOC>();
        List<BE_VTA_VENTASOCT> l_DetOCT = new List<BE_VTA_VENTASOCT>();

        CN_mae_clipro objcli = new CN_mae_clipro();
        CN_alm_inventario objAlmacen = new CN_alm_inventario();
        CN_alm_inventariounimed objAlmacenUniMed = new CN_alm_inventariounimed();
        CN_mae_clipro objClientes = new CN_mae_clipro();
        CN_sun_tipmon objMoneda = new CN_sun_tipmon();
        CN_sun_tipdoccom objTipDocumento = new CN_sun_tipdoccom();
        //CN_sys_docnum objNumeroDoc = new CN_sys_docnum();
        CN_vta_ventas objVentas = new CN_vta_ventas();
        CN_sun_tipcam ObjTC = new CN_sun_tipcam();
        SIAC_Objetos.Funciones objFunciones = new SIAC_Objetos.Funciones();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        Cls_NumeroLetra funLet = new Cls_NumeroLetra();
        CN_sun_tipcontribuyente objTipCon = new CN_sun_tipcontribuyente();
        CN_con_pcitems objPcIte = new CN_con_pcitems();

        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        DatosMySql funDatos = new DatosMySql();
        Genericas funGen = new Genericas();

        int intFilasCantidad = 0;
        double douIGVTasa = 0;
        bool booAgregando = false;
        string strTituloFormulario = "";

        DataTable dtItems = new DataTable();
        DataTable dtClientes = new DataTable();
        DataTable dtUnidadMedida = new DataTable();
        DataTable dtMoneda = new DataTable();
        DataTable dtTipDocumento = new DataTable();
        DataTable dtTC = new DataTable();
        DataTable dtTipCon = new DataTable();
        DataTable dtPrecios = new DataTable();
        DataTable dtCtaConVen = new DataTable();

        // PARA ALMACENAR EL VALOR DE LA FACTURA
        double douPrecioTotal = 0;
        double douPrecioTotalSinIGV = 0;
        double douValorIGV = 0;

        public FrmPuntoVenta()
        {
            InitializeComponent();
        }

        private void TooNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            
            LblNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDocumento.SelectedValue), LblSerDoc.Text);
            if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 2) { TxtNumRuc.Focus(); }
            if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 4) { TxtNomCli.Focus(); }
        }
        void AsignarEntidad()
        {
            l_DocumentoDet.Clear();
            l_DetDoc.Clear();
            l_DetOCT.Clear();

            e_Documento.n_id = 0;
            e_Documento.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Documento.n_anotra = STU_SISTEMA.ANOTRABAJO;
            e_Documento.n_idmes = STU_SISTEMA.MESTRABAJO;
            e_Documento.n_idlib = 14;
            e_Documento.c_numreg = "";
            e_Documento.n_idtippro = 2;
            e_Documento.n_idcli = Convert.ToInt32(LblIdCliente.Text);
            e_Documento.n_idpunvencli = 0;
            e_Documento.n_idtipdoc = Convert.ToInt32(CboTipDocumento.SelectedValue);
            e_Documento.c_numser = LblSerDoc.Text;
            e_Documento.c_numdoc = LblNumDoc.Text;
            if (e_Documento.n_idmes == 0)
            {
                e_Documento.d_fchreg = Convert.ToDateTime("01/01/" + e_Documento.n_anotra.ToString("0000"));
            }
            else
            {
                e_Documento.d_fchreg = Convert.ToDateTime("01/" + LblFchEmi.Text.Substring(3, 2) + "/" + LblFchEmi.Text.Substring(6, 4));
            }
            e_Documento.d_fchdoc = Convert.ToDateTime(LblFchEmi.Text);
            e_Documento.d_fchven = Convert.ToDateTime(LblFchEmi.Text);
            e_Documento.n_idconpag = 1;
            e_Documento.n_idmon = Convert.ToInt32(CboMoneda.SelectedValue);
            e_Documento.n_impbru = Convert.ToDouble(LblImpBru.Text);
            e_Documento.n_impbru2 = 0;
            e_Documento.n_impbru3 = 0;
            e_Documento.n_impinaf = 0;
            e_Documento.n_impigv = Convert.ToDouble(LblIgv.Text);
            e_Documento.n_impisc = 0;
            e_Documento.n_impotr = 0;
            e_Documento.n_imptotven = Convert.ToDouble(LblTotal.Text);
            e_Documento.n_tc = Convert.ToDouble(LblTipCam.Text);
            e_Documento.n_impsal = Convert.ToDouble(LblTotal.Text);
            e_Documento.n_idven = 0;
            e_Documento.n_tasaigv = douIGVTasa;
            e_Documento.c_glosa = "VENTA EN MOSTRADOR DEL DIA " + LblFchEmi.Text;
            e_Documento.n_impsubtot = Convert.ToDouble(LblImpBru.Text);
            e_Documento.n_pordsc = 0;
            e_Documento.n_idtipope = 1;

            e_Documento.n_idtipdocref = 0;
            e_Documento.n_iddocref = 0;

            e_Documento.c_serdocref = "";
            e_Documento.c_numdocref = "";

            string c_mon = "";
            if (Convert.ToDouble(CboMoneda.SelectedValue) == 115) { c_mon = "soles."; }
            if (Convert.ToDouble(CboMoneda.SelectedValue) == 151) { c_mon = "dolares americanos."; }
            e_Documento.c_numlet = funLet.Convertir(LblTotal.Text, true, c_mon);

            e_Documento.n_oriitem = 1;             // INDICAMOS QUE LA VENTA NO TIENE GUIA DE REMISION
            e_Documento.n_anulado = 0;
            e_Documento.c_motnc = "";

            if (OptForPag1.Checked == true) { e_Documento.n_idforpag = 1; }
            if (OptForPag2.Checked == true) { e_Documento.n_idforpag = 2; }

            if (OptTarCre1.Checked == true) { e_Documento.n_idtarcre = 1; }
            if (OptTarCre1.Checked == true) { e_Documento.n_idtarcre = 2; }
            if (OptTarCre1.Checked == true) { e_Documento.n_idtarcre = 3; }

            int n_fila = 0;
            DataTable DtFiltro = new DataTable();
            string c_nomitem = "";
            string c_presendes = "";
            double n_valor = 0;

            if (FgDetalle.Rows.Count > 2)
            {
                for (n_fila = 1; n_fila <= FgDetalle.Rows.Count - 1; n_fila++)
                {
                    if (funFunciones.NulosC(FgDetalle.GetData(n_fila, 1)) != "")
                    {
                        BE_VTA_VENTASDET BE_Detalle = new BE_VTA_VENTASDET();

                        c_nomitem = FgDetalle.GetData(n_fila, 1).ToString();
                        c_presendes = FgDetalle.GetData(n_fila, 2).ToString();

                        BE_Detalle.n_idvta = e_Documento.n_id;
                        BE_Detalle.n_canpro = Convert.ToDouble(FgDetalle.GetData(n_fila, 3).ToString());

                        BE_Detalle.n_iditem = Convert.ToInt32(FgDetalle.GetData(n_fila, 6).ToString());
                        BE_Detalle.n_idunimed = Convert.ToInt32(FgDetalle.GetData(n_fila, 7).ToString());

                        //n_valor = Convert.ToDouble(FgDetalle.GetData(n_fila, 4).ToString());
                        //n_valor = n_valor / ((douIGVTasa / 100) + 1);
                        BE_Detalle.n_preunibru = Convert.ToDouble(FgDetalle.GetData(n_fila, 8).ToString());
                        BE_Detalle.n_preuninet = Convert.ToDouble(FgDetalle.GetData(n_fila, 8).ToString());
                        BE_Detalle.n_imptot = Convert.ToDouble(FgDetalle.GetData(n_fila, 9).ToString());
                        BE_Detalle.n_idtipven = 0;
                        BE_Detalle.n_pordsc = 0;
                        BE_Detalle.n_porigv = douIGVTasa;
                        //string c_dato = FgDetalle.GetData(n_fila, 8).ToString();
                        //c_dato = funDatos.DataTableBuscar(dtAnex07, "c_codsun", "n_id", c_dato, "C").ToString();
                        BE_Detalle.n_preuninetigv = Convert.ToDouble(FgDetalle.GetData(n_fila, 4).ToString());
                        BE_Detalle.n_imptotigv = Convert.ToDouble(FgDetalle.GetData(n_fila, 5).ToString());
                        BE_Detalle.n_idtipafeigv = 1;
                        BE_Detalle.c_datadi = funFunciones.NulosC(FgDetalle.GetData(n_fila, 9)).ToString();
                        l_DocumentoDet.Add(BE_Detalle);
                    }
                }
            }

            l_DetOCT.Clear();
            BE_VTA_VENTASOCT entOC = new BE_VTA_VENTASOCT();

            ////  1001 - Total valor de venta - operaciones gravadas
            entOC.n_idvta = 0;
            entOC.n_idcon = 1;     
            entOC.n_importe = Convert.ToDouble(LblImpBru.Text);
            l_DetOCT.Add(entOC);
        }
        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            GrabarVenta();
        }
        void GrabarVenta()
        {
            if (Convert.ToDouble(funFunciones.NulosN(LblTotal.Text)) == 0)                              // SI EL TOTAL DEL DOCUMENTO ES 0
            {
                funFunciones.MensajeMostrarError("El valor del documento no puede ser 0", strTituloFormulario);
                return;
            }

            if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 2)                // SI EL TIPO DE DOCUMENTO ES FACTURA
            {
                if (funFunciones.NulosC(TxtNumRuc.Text) == "")
                {
                    funFunciones.MensajeMostrarError("No ha indicado el numero de ruc para este documento", strTituloFormulario);
                    return;
                }
            }
            if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 4)                // SI EL TIPO DE DOCUMENTO ES BOLETA
            {
                if (funFunciones.NulosC(TxtNomCli.Text) == "")
                {
                    funFunciones.MensajeMostrarError("No ha indicado el nombre del cliente para este documento", strTituloFormulario);
                    return;
                }
            }

            if (Grabar() == true)
            {
                funFunciones.MensajeMostrarAviso("El documento se guardo con exito", strTituloFormulario);
                LimpiarControles();
                LblNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDocumento.SelectedValue), LblSerDoc.Text);
                if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 2) { TxtNumRuc.Focus(); }
                if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 4) { TxtNomCli.Focus(); }
            }
        }
        void Imprimir(int n_IdVenta, int n_IdTipoDocumento)
        {
            CN_vta_ventas objAlm = new CN_vta_ventas();
            objAlm.STU_SISTEMA = STU_SISTEMA;
            objAlm.mysConec = mysConec;
            objAlm.ReportImprimirDocumentoPV(n_IdVenta, n_IdTipoDocumento, false, "", true);
        }
        private void FrmPuntoVenta_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            DataTableCargar();
            
            funGen.ComboBoxCargarDataTable(CboMoneda, dtMoneda, "n_id", "c_des");
            CboMoneda.SelectedValue = STU_SISTEMA.MONEDA;

            funGen.ComboBoxCargarDataTable(CboTipDocumento, dtTipDocumento, "n_id", "c_des");
            booAgregando = false;

            LblSerDoc.Text = "";
            LblNumDoc.Text = "";

            LblSerDoc.Text = "0001";
            objTipDoc.mysConec = mysConec;

            LblFchEmi.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LblTipCam.Text = objFunciones.ObtenerTC(dtTC, LblFchEmi.Text).ToString("0.000");

            CboMoneda.SelectedIndex = 0;
            CboTipDocumento.SelectedIndex = 0;

            if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 2)
            {
                TxtNumRuc.ReadOnly = false;
            }
            else
            {
                TxtNumRuc.ReadOnly = true;
            }

            this.Text = "VENTAS - PUNTO DE VENTA";
            LimpiarControles();
            intFilasCantidad = 20;
            douIGVTasa = 18;
            FgDetalle.Rows.Count = intFilasCantidad;
            FgDetalle.Cols[6].Width = 40;                            // ID DEL ITEM  
            FgDetalle.Cols[7].Width = 40;                            // ID DE LA UNIDAD DE MEDIDA
            FgDetalle.Cols[8].Width = 40;                            // PRECIO UNITARIO SIN IGV
            FgDetalle.Cols[9].Width = 40;                            // PRECIO TOTAL SIN IGV
            LblIGVTasa.Text = douIGVTasa.ToString() + "%";
            FgDetalle.Cols[1].ComboList = "...";
            OptForPag1.Checked = true;
            TxtNumRuc.Focus();
        }
        private void TxtNumRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendKeys.Send("{Tab}");
            }

            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                //permitir teclas de control como retroceso
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                }
            }
        }
        private void TxtNomCli_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendKeys.Send("{Tab}");
            }
        }
        private void TxtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendKeys.Send("{Tab}");
            }
        }
        private void TxtNumRuc_Validated(object sender, EventArgs e)
        {
            if (TxtNumRuc.Text != "")
            {
                DataTable DtCliente2 = new DataTable();
                DtCliente2 = funGen.DataTableFiltrar(dtClientes, "c_numdoc = '" + TxtNumRuc.Text + "'");

                if ((DtCliente2 != null) && (DtCliente2.Rows.Count != 0))
                {
                    TxtNomCli.Text = DtCliente2.Rows[0]["c_nombre"].ToString();
                    TxtDireccion.Text = DtCliente2.Rows[0]["c_dir"].ToString();
                    LblIdCliente.Text = DtCliente2.Rows[0]["n_id"].ToString();
                    FgDetalle.Focus();
                    FgDetalle.Select(1, 1);
                }
                else
                {
                    TxtNumRuc.Text = "";
                    LblIdCliente.Text = "";
                    TxtNumRuc.Focus();
                    TxtNomCli.Text = "";
                    TxtDireccion.Text = "";
                }
            }
        }
        void LimpiarControles()
        {
            TxtNumRuc.Text = "";
            TxtNomCli.Text = "";
            TxtDireccion.Text = "";
            LblImpBru.Text = "";
            LblIgv.Text = "";
            LblTotal.Text = "";
            LblIdCliente.Text = "";
            booAgregando = true;
            FgDetalle.Rows.Count = 1;
            FgDetalle.Rows.Count = intFilasCantidad + 1;
            booAgregando = false;
            OptForPag1.Checked = true;
        }
        void DataTableCargar()
        {
            objAlmacen.mysConec = mysConec;
            dtItems = objAlmacen.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD, 1);         // CARGAMOS TODOS LOS ITEMAS
            dtItems = funGen.DataTableFiltrar(dtItems, "n_idtipexi IN (1,2)");

            objClientes.mysConec = mysConec;
            dtClientes = objClientes.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);                   // CARGAMOS TODOS LOS CLIENTES

            objAlmacenUniMed.mysConec = mysConec;
            dtUnidadMedida = objAlmacenUniMed.Listar();                 // CARGAMOS TODAS LAS UNIDADES DE MEDIDA

            objMoneda.mysConec = mysConec;
            dtMoneda = objMoneda.Listar();                              // CARGAMOS TODAS MONEDAS

            objTipDocumento.mysConec = mysConec;
            dtTipDocumento = objTipDocumento.Listar_puntoventa();       // CARGAMOS TIPOS DE DOCUMENTO PARA VENTAS
            
            ObjTC.mysConec = mysConec;
            dtTC = ObjTC.Listartcano(151, STU_SISTEMA.ANOTRABAJO.ToString());

            objTipCon.mysConec = mysConec;
            dtTipCon = objTipCon.Listar();                                  // CARGAMOS TIPO DE CONTRIBUYENTE

            objPcIte.mysConec = mysConec;
            objPcIte.ListarItemCtaventa(STU_SISTEMA.EMPRESAID);
            dtCtaConVen = objPcIte.dtLista;
        }
        private void FgDetalle_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (booAgregando == true) { return; }
            DataTable dtUniMed = new DataTable();
            DataTable dtResult = new DataTable();
            int intProductoId = 0;
            int intUniMedId = 0;

            if (e.Col == 1)             // SI SE HA SELECCIONADO ALGUN ITEM DE LA COLUMNA 1
            {
                string strItemDescripcion = FgDetalle.GetData(e.Row, 1).ToString();
                intProductoId = Convert.ToInt32(funGen.DataTableBuscar(dtItems, "c_despro", "n_id", strItemDescripcion, "C"));    // OBTENEMOS EL ID DEL ITEM SELECCIONADO

                FgDetalle.SetData(e.Row, 6, intProductoId);
                dtUniMed = funGen.DataTableFiltrar(dtUnidadMedida, "n_idite = " + intProductoId + "");                            //  FILTRAMOS POR ID DEL PRODUCTO
                if (dtUniMed.Rows.Count != 0) { funFlex.FlexColumnaCombo(FgDetalle, dtUniMed, "c_abrpre", 2); }

                dtUniMed = funGen.DataTableFiltrar(dtUniMed, "n_default = 1");                                                    //  FILTRAMOS LA UNIDAD DE MEDIDA POR DEFECTO

                FgDetalle.SetData(e.Row, 2, dtUniMed.Rows[0]["c_abrpre"].ToString());                                               // ESTABLECEMOS LA ABREVIATURA DE LA UNIDAD DE MEDIDA 
                FgDetalle.SetData(e.Row, 7, dtUniMed.Rows[0]["n_id"].ToString());                                                   // ESTABLECEMOS EL ID DE LA UNIDAD DE MEDIDA 
                FgDetalle.SetData(e.Row, 6, intProductoId.ToString());                                                             // ESTABLECEMOS EL ID DE LA EXISTENCIA

                double doupreuniigv = Convert.ToDouble(dtUniMed.Rows[0]["n_preuniigv"].ToString());
                double doupreuni = Convert.ToDouble(dtUniMed.Rows[0]["n_preuni"].ToString());
                FgDetalle.SetData(e.Row, 4, doupreuniigv.ToString("0.000000"));                                                     // ESTABLECEMOS EL PRECIO CON IGV DEL ITEM
                FgDetalle.SetData(e.Row, 8, doupreuni.ToString("0.000000"));                                                        // ESTABLECEMOS EL PRECIO SIN IGV DEL ITEM

                CalcularFila(e.Row);
                //FgDetalle.Select(FgDetalle.Row - 1, 3);
            }

            if (e.Col == 2)             // SI SE HA SELECCIONADO ALGUN ITEM DE LA COLUMNA 2
            {
                intProductoId = Convert.ToInt32(FgDetalle.GetData(e.Row, 6));
                string strUnidadMedidaDescripcion = FgDetalle.GetData(e.Row, 2).ToString();

                dtUniMed = funGen.DataTableFiltrar(dtUnidadMedida, "n_idite = " + intProductoId + ""); //  FILTRAMOS LA UNIDAD DE MEDIDA POR DEFECTO
                intUniMedId = Convert.ToInt32(funGen.DataTableBuscar(dtUniMed, "c_abrpre", "n_id", strUnidadMedidaDescripcion, "C"));
                //intUniMedId = Convert.ToInt32(funDatos.DataTableBuscar(dtUnidadMedida, "c_abrpre", "n_id", intProductoId.ToString(), "N"));
                FgDetalle.SetData(e.Row, 7, intUniMedId);

                dtUniMed = funGen.DataTableFiltrar(dtUniMed, "n_id = " + intUniMedId + " AND n_idite = " + intProductoId + "");   //  FILTRAMOS LA UNIDAD DE MEDIDA POR DEFECTO

                double doupreuniigv = Convert.ToDouble(dtUniMed.Rows[0]["n_preuniigv"].ToString());
                double doupreuni = Convert.ToDouble(dtUniMed.Rows[0]["n_preuni"].ToString());

                FgDetalle.SetData(e.Row, 2, dtUniMed.Rows[0]["c_abrpre"].ToString());                                               // ESTABLECEMOS LA ABREVIATURA DE LA UNIDAD DE MEDIDA 
                FgDetalle.SetData(e.Row, 7, dtUniMed.Rows[0]["n_id"].ToString());                                                   // ESTABLECEMOS EL ID DE LA UNIDAD DE MEDIDA 
                FgDetalle.SetData(e.Row, 4, doupreuniigv.ToString("0.000000"));                                                     // ESTABLECEMOS EL PRECIO CON IGV DEL ITEM
                FgDetalle.SetData(e.Row, 8, doupreuni.ToString("0.000000"));                                                        // ESTABLECEMOS EL PRECIO SIN IGV DEL ITEM

                CalcularFila(e.Row);
                FgDetalle.Select(FgDetalle.Row - 1, 3);
                return;
            }

            if ((e.Col == 3) || (e.Col == 4))
            {
                //if (e.Col == 4)
                //{
                double douPreSninIGV = 0;
                double douTotPreSinIGV = 0;

                if (funFunciones.NulosC(FgDetalle.GetData(FgDetalle.Row, 4)) != "")
                {

                    douPreSninIGV = (Convert.ToDouble(FgDetalle.GetData(FgDetalle.Row, 4)) / (((douIGVTasa) / 100) + 1));
                    douTotPreSinIGV = (douPreSninIGV * Convert.ToDouble(FgDetalle.GetData(FgDetalle.Row, 3)));
                    FgDetalle.SetData(FgDetalle.Row, 8, douPreSninIGV.ToString("0.000000"));
                    FgDetalle.SetData(FgDetalle.Row, 9, douTotPreSinIGV.ToString("0.000000"));
                    CalcularFila(e.Row);
                }

                if (e.Col == 3)
                {
                    FgDetalle.Select(FgDetalle.Row - 1, 4);
                    return;
                }
                if (e.Col == 4)
                {
                    FgDetalle.Select(e.Row, 1);
                    return;
                }
            }
        }
        void CalcularFila(int intFila)
        {
            double douCantidad = Convert.ToDouble(FgDetalle.GetData(FgDetalle.Row, 3));
            double douPreuniIGV = Convert.ToDouble(FgDetalle.GetData(FgDetalle.Row, 4));
            double douPreuni = Convert.ToDouble(FgDetalle.GetData(FgDetalle.Row, 9));

            double douPrecioTotalIGV = douCantidad * douPreuniIGV;
            double douPrecioTotal = douCantidad * douPreuni;

            FgDetalle.SetData(intFila, 5, douPrecioTotalIGV.ToString("0.00"));
            //FgDetalle.SetData(intFila, 9, douPrecioTotal.ToString("0.00"));
            SumarTotales();
        }
        void SumarTotales()
        {
            douPrecioTotal = 0;
            douPrecioTotal = funFlex.FlexSumarCol(FgDetalle, 5, 1, FgDetalle.Rows.Count - 1);
            douPrecioTotalSinIGV = funFlex.FlexSumarCol(FgDetalle, 9, 1, FgDetalle.Rows.Count - 1);

            LblImpBru.Text = douPrecioTotalSinIGV.ToString("0.00");
            LblTotal.Text = douPrecioTotal.ToString("0.00");
            douValorIGV = (douPrecioTotal - douPrecioTotalSinIGV);
            LblIgv.Text = douValorIGV.ToString("0.00");
        }
        private void FgDetalle_RowColChange(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            DataTable dtUniMed = new DataTable();
            int intProductoId = 0;

            if (FgDetalle.Col == 2)
            {
                intProductoId = Convert.ToInt32(FgDetalle.GetData(FgDetalle.Row, 6));                         // OBTENEMOS EL ID DEL ITEM SELECCIONADO
                dtUniMed = funGen.DataTableFiltrar(dtUnidadMedida, "n_idite = " + intProductoId + "");
                funFlex.FlexColumnaCombo(FgDetalle, dtUniMed, "c_abrpre", 2);
            }
        }
        bool Grabar()
        {
            bool booOk = false;
            int intFila = 0;
            int intClienteId = 0;
            int intIdClienteDefault = 1;
            int intIdTipProd = 3;
            string strNumeroDocumento = "";
            int intIdVendedor = 0;
            if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 2) { intClienteId = Convert.ToInt32(LblIdCliente.Text); }
            if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 4) { intClienteId = intIdClienteDefault; }
            if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 13) { intClienteId = intIdClienteDefault; }

            //OBTENEMOS EL NUEVO NUMERO DE DOCUMENTO ANTES DE GUARDAR LA OPERACION
            AsignarEntidad();
            strNumeroDocumento = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDocumento.SelectedValue), LblSerDoc.Text);
                        
            objVentas.mysConec = mysConec;
            objVentas.LstDetalle = l_DocumentoDet;    // ASIGNAMOS EL DETALLE DE LA GUIA
            objVentas.LstDetalleOCT = l_DetOCT;
            objVentas.LstDocumentos = l_DetDoc;
            objVentas.STU_SISTEMA = STU_SISTEMA;
            if (objVentas.Insertar(e_Documento) == true)
            {
                Imprimir(Convert.ToInt32(e_Documento.n_id), e_Documento.n_idtipdoc);
                booOk = true;
            }

            return booOk;
        }
        private void FgDetalle_EnterCell(object sender, EventArgs e)
        {
            if (FgDetalle.Col == 5)
            {
                FgDetalle.AllowEditing = false;
            }
            else
            {
                FgDetalle.AllowEditing = true;
            }
        }
        private void CboTipDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            LblTipDocumento.Text = CboTipDocumento.Text;
            LblNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDocumento.SelectedValue), LblSerDoc.Text);
            TxtNumRuc.Focus();
        }

        private void FgDetalle_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            //if (n_QueHace == 3)
            //{
            //    FgDetalle.AllowEditing = false; return;
            //}
            //if (booAgregando == true) { return; }


            if (FgDetalle.Col == 1)
            {
                int n_idtipexi = 0;
                DataTable dtResul = new DataTable();
                string c_dato = "";

                booAgregando = true;
                dtResul = dtItems;
                //if (Convert.ToInt32(CboTipExi.SelectedValue) != 0)
                //{
                //    n_idtipexi = Convert.ToInt32(CboTipExi.SelectedValue);
                //    dtResul = funGen.DataTableFiltrar(dtItems, "n_idtipexi = " + n_idtipexi + "");
                //}
                
                dtResul = objAlmacen.BuscarItemPuntoVenta("", "n_id", dtResul, n_idtipexi);
                if (dtResul != null)
                {
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["n_id"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                        FgDetalle.SetData(e.Row, 6, c_dato);

                        int n_idctacon = Convert.ToInt32(funGen.DataTableBuscar(dtCtaConVen, "n_idite", "n_idpcven", c_dato, "N"));
                        if (n_idctacon == 0)
                        {
                            funFunciones.MensajeMostrarAviso("El item seleccionado no tiene cuenta contable asignada", strTituloFormulario);
                            return;
                        }

                        booAgregando = true;
                        c_dato = dtResul.Rows[0]["c_despro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                        FgDetalle.SetData(e.Row, 1, c_dato);



                        c_dato = dtResul.Rows[0]["n_idunimed"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                        FgDetalle.SetData(e.Row, 7, c_dato);

                        c_dato = dtResul.Rows[0]["c_abrpre"].ToString();        // MOSTRAMOS LA PRESENTACION DEL ITEM
                        FgDetalle.SetData(e.Row, 2, c_dato);

                        c_dato = dtResul.Rows[0]["n_preven"].ToString();        // MOSTRAMOS LA PRESENTACION DEL ITEM
                        FgDetalle.SetData(e.Row, 4, c_dato);

                        booAgregando = false;
                        //// MOSTRAMOS EL TIPO DE VENTA
                        //c_dato = funGen.DataTableBuscar(dtAnex07, "n_id", "c_codsun", "1", "C").ToString();
                        //FgDetalle.SetData(FgDetalle.Row, 8, c_dato);
                    }
                }

                booAgregando = false;
                FgDetalle.Select(e.Row, 3);
            }
        }

        private void ToolCliRapido_Click(object sender, EventArgs e)
        {
            AgregarRuc();
        }
        void AgregarRuc()
        {
            string c_acteco = "";
            Helper.Cls_ServiciosSunat xFun = new Helper.Cls_ServiciosSunat();
            xFun.ConsultarRUC();

            string c_condic = xFun.Contribuyente_Condicion;
            string c_direcc = xFun.Contribuyente_Direccion;
            string c_estado = xFun.Contribuyente_Estado;
            string c_fchiniact = xFun.Contribuyente_FchIniAct;
            string c_nombre = xFun.Contribuyente_Nombre;
            string c_numruc = xFun.Contribuyente_NumRUC;
            string c_tipcon = xFun.Contribuyente_TipCon;
            string c_apepat = "";
            string c_apemat = "";
            string c_nom1 = "";
            string c_nom2 = "";
            xFun = null;

            if (c_numruc != "")
            {
                int n_idtipcon = Convert.ToInt32(funGen.DataTableBuscar(dtTipCon, "c_des", "n_id", c_tipcon, "C"));

                if ((n_idtipcon == 1) || (n_idtipcon == 2))
                {
                    string[] dato = new string[10];
                    dato = c_nombre.Split(' ');
                    int n_row = 1;

                    if (n_row <= dato.Length) { c_apepat = funFunciones.NulosC(dato[0]); n_row = n_row + 1; }
                    if (n_row <= dato.Length) { c_apemat = funFunciones.NulosC(dato[1]); n_row = n_row + 1; }
                    if (n_row <= dato.Length) { c_nom1 = funFunciones.NulosC(dato[2]); n_row = n_row + 1; }
                    if (n_row <= dato.Length) { c_nom2 = funFunciones.NulosC(dato[3]); n_row = n_row + 1; }
                }
                BE_MAE_CLIPRO e_cli = new BE_MAE_CLIPRO();

                e_cli.n_idemp = 0;
                e_cli.n_id = 0;
                e_cli.n_idcatemp = 1;
                e_cli.n_idtipcon = n_idtipcon;
                e_cli.n_idtipdoc = 4;
                e_cli.c_numdoc = c_numruc;
                e_cli.c_nombre = c_nombre;
                e_cli.c_nomcli1 = c_nom1;
                e_cli.c_nomcli2 = c_nom2;
                e_cli.c_apecli1 = c_apepat;
                e_cli.c_apecli2 = c_apemat;
                e_cli.c_dir = c_direcc.Substring(1, 100);
                e_cli.c_tel = "";
                e_cli.c_fax = "";
                e_cli.c_nomcon = c_nombre;
                e_cli.c_email = "";
                e_cli.c_pagweb = "";
                e_cli.n_estado = 1;
                e_cli.n_iddep = 0;
                e_cli.n_idpro = 0;
                e_cli.n_iddis = 0;
                e_cli.n_ageret = 0;
                e_cli.c_codcen = "";
                e_cli.n_idven = 0;
                e_cli.n_idcondpag = 0;
                e_cli.c_letnomgir = "";
                e_cli.c_letgirdir = "";
                e_cli.c_letnumdoc = "";
                e_cli.c_lettel = "";
                e_cli.n_tipreg = 0;
                e_cli.d_fchini = DateTime.Now;
                
                objcli.mysConec = mysConec;
                if (objcli.Insertar(e_cli) == true)
                {
                    LblIdCliente.Text = objcli.n_IdGenerado.ToString();
                    TxtNumRuc.Text = c_numruc;
                    TxtNomCli.Text = c_nombre;
                    TxtDireccion.Text = c_direcc;
                }
            }
        }
        private void FrmPuntoVenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MostrarAyuda();
            }
            if (e.KeyCode.ToString() == "F2")
            {
                GrabarVenta();
            }
            if (e.KeyCode.ToString() == "F3")
            {
                LimpiarControles();
                LblNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDocumento.SelectedValue), LblSerDoc.Text);
                if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 2) { TxtNumRuc.Focus(); }
                if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 4) { TxtNomCli.Focus(); }
            }
            if (e.KeyCode.ToString() == "F4")
            {
                AgregarRuc();
            }
            if (e.KeyCode.ToString() == "F5")
            {
                BuscarCLientes();
            }
            if (e.KeyCode.ToString() == "F6")
            {
                int n_num = 0;

                n_num = Convert.ToInt32(CboTipDocumento.Items.Count);
                if (Convert.ToInt32(CboTipDocumento.SelectedIndex) < n_num - 1)
                {
                    CboTipDocumento.SelectedIndex = CboTipDocumento.SelectedIndex + 1;
                }
                else
                {
                    CboTipDocumento.SelectedIndex = 0;
                }
            }
            if (e.KeyCode.ToString() == "F7")
            {
                if (OptForPag1.Checked == true) { OptForPag2.Checked = true; OptTarCre1.Checked = true; return; }
                if (OptForPag2.Checked == true) { OptForPag1.Checked = true; return; }
            }
            if (e.KeyCode.ToString() == "F8")
            {
                if (OptTarCre1.Checked == true) { OptTarCre2.Checked = true; return; }
                if (OptTarCre2.Checked == true) { OptTarCre3.Checked = true; return; }
                if (OptTarCre3.Checked == true) { OptTarCre1.Checked = true; return; }
                
            }
            if (e.KeyCode.ToString() == "F8")
            {
                this.Close();
            }
        }
        private void CmdBusCli_Click(object sender, EventArgs e)
        {
            BuscarCLientes();
        }
        void BuscarCLientes()
        {
            DataTable dtResult = new DataTable();
            objClientes.mysConec = mysConec;
            dtResult = objClientes.BuscarCliPro(dtClientes, 1, "n_id", "");
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    TxtNumRuc.Text = dtResult.Rows[0]["c_numdoc"].ToString();
                    LblIdCliente.Text = dtResult.Rows[0]["n_id"].ToString();
                    TxtNomCli.Text = dtResult.Rows[0]["c_nombre"].ToString();
                    TxtDireccion.Text = dtResult.Rows[0]["c_dir"].ToString();

                    //dtPrecios = objVentas.UltimoPrecioCliente(STU_SISTEMA.EMPRESAID, Convert.ToInt32(LblIdCliente.Text));
                    FgDetalle.Focus();
                    FgDetalle.Select(1, 1);
                }
                else
                {
                    TxtNumRuc.Text = "";
                    LblIdCliente.Text = "";
                    TxtNomCli.Text = "";
                    TxtDireccion.Text = "";
                }
            }
        }
        private void OptForPag2_CheckedChanged(object sender, EventArgs e)
        {
            if (OptForPag2.Checked == true)
            {
                OptTarCre1.Enabled = true;
                OptTarCre2.Enabled = true;
                OptTarCre3.Enabled = true;
            }
        }
        private void OptForPag1_CheckedChanged(object sender, EventArgs e)
        {
            if (OptForPag1.Checked == true)
            {
                OptTarCre1.Enabled = false;
                OptTarCre2.Enabled = false;
                OptTarCre3.Enabled = false;
            }
        }
        private void CmdSalHel_Click(object sender, EventArgs e)
        {
            c1Sizer1.Enabled = true;
            PanHelp.Visible = false;
            TxtNumRuc.Focus();
        }
        private void ToolAyuda_Click(object sender, EventArgs e)
        {
            MostrarAyuda();
        }
        void MostrarAyuda()
        {
            PanHelp.Left = ((this.Width - PanHelp.Width) / 2);
            PanHelp.Top = ((this.Height - PanHelp.Height) / 2);
            c1Sizer1.Enabled = false;
            PanHelp.Visible = true;
        }
        private void FrmPuntoVenta_Activated(object sender, EventArgs e)
        {
            if (Convert.ToDouble(LblTipCam.Text) == 0)
            {
                funFunciones.MensajeMostrarError("No hay tipo de cambio, registre el tipo de cambio para poder continuar", strTituloFormulario);
                this.Close();
            }

            TxtNumRuc.Focus();
        }
        private void FgDetalle_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.KeyChar.ToString() == "F2")
            {
                GrabarVenta();
            }
        }
        private void ToolSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
