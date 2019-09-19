using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar;
using System.Diagnostics;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Metro.ColorTables;
using SIAC_Entidades.Ventas;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Objetos.Constantes;
using SIAC_Objetos.Ventas;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;

namespace SIAC_NET
{
    public partial class FrmPuntoVenta : Form
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        CN_alm_inventario objAlmacen = new CN_alm_inventario();
        CN_alm_inventariounimed objAlmacenUniMed = new CN_alm_inventariounimed();
        CN_mae_clipro objClientes = new CN_mae_clipro();
        CN_sun_tipmon objMoneda = new CN_sun_tipmon();
        CN_sun_tipdoccom objTipDocumento = new CN_sun_tipdoccom();
        CN_sys_docnum objNumeroDoc = new CN_sys_docnum();
        CN_vta_ventas objVentas = new CN_vta_ventas();
        //Constantes.SYS_DOCNUM objCons = new Constantes.SYS_DOCNUM();
        Ventas.STU_VTA_VENTAS STUVENTAS = new Ventas.STU_VTA_VENTAS();                           
      
        FlexGrid funFlex = new FlexGrid();
        Funciones funFunciones = new Funciones();
        DatosMySql funDatos = new DatosMySql();
        
        int intFilasCantidad = 0;
        double douIGVTasa = 0;
        bool booAgregando = false;
        string strTituloFormulario;

        DataTable dtItems = new DataTable();
        DataTable dtClientes = new DataTable();
        DataTable dtUnidadMedida = new DataTable();
        DataTable dtMoneda = new DataTable();
        DataTable dtTipDocumento = new DataTable();

        public FrmPuntoVenta()
        {
            InitializeComponent();

        }
        private void C1Sizer1_Click(object sender, EventArgs e)
        {
        }
        private void metroTabPanel2_Click(object sender, EventArgs e)
        {

        }
        private void metroAppButton1_Click(object sender, EventArgs e)
        {

        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void FrmPuntoVenta_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            DataTableCargar();
            
            funFlex.FlexColumnaCombo(FgDetalle, dtItems, "c_despro", 1);
            
            funDatos.ComboBoxCargarDataTable(CboMoneda, dtMoneda, "n_id", "c_des");
            CboMoneda.SelectedValue = STU_SISTEMA.MONEDA;

            funDatos.ComboBoxCargarDataTable(CboTipDocumento, dtTipDocumento, "n_id", "c_des");            
            booAgregando = false;
            CboTipDocumento.SelectedValue = 4;

            LblSerDoc.Text = "";
            LblNumDoc.Text = "";

            LblSerDoc.Text  = "0001";
            objNumeroDoc.mysConec = mysConec;

            LblNumDoc.Text = objNumeroDoc.HallaNumeroDocumento(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDocumento.SelectedValue), LblSerDoc.Text, Constantes.SYS_DOCNUM.NO_GRABAR_NUMERO_GENERADO);

            if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 2)
            {
                TxtNumRuc.ReadOnly = false;
            }
            else
            {
                TxtNumRuc.ReadOnly = true;
            }

            this.Text = "SIAC - Punto de Venta";
            LimpiarControles();
            TxtNumRuc.Focus();
            intFilasCantidad  = 20;
            douIGVTasa = 18;
            FgDetalle.Rows.Count = intFilasCantidad;
            FgDetalle.Cols[6].Width = 0;                            // ID DEL ITEM  
            FgDetalle.Cols[7].Width = 0;                            // ID DE LA UNIDAD DE MEDIDA
            FgDetalle.Cols[8].Width = 0;                            // PRECIO UNITARIO SIN IGV
            FgDetalle.Cols[9].Width = 0;                            // PRECIO TOTAL SIN IGV
            LblIGVTasa.Text = douIGVTasa.ToString() + "%";
            
            LblTipCam.Text = STU_SISTEMA.TIPOCAMBIO.ToString("0.000");
        }

        private void metroStatusBar1_ItemClick(object sender, EventArgs e)
        {

        }
        private void metroTabItem1_Click(object sender, EventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void TxtNumRuc_TextChanged(object sender, EventArgs e)
        {
   
        }
        private void TxtNumRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) {
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
                objClientes.mysConec = mysConec;

                DtCliente2 = objClientes.ClienteBuscar(TxtNumRuc.Text);
                if ((DtCliente2 != null) && (DtCliente2.Rows.Count != 0))
                {
                    TxtNomCli.Text = DtCliente2.Rows[0]["c_nombre"].ToString();
                    TxtDireccion.Text = DtCliente2.Rows[0]["c_dir"].ToString();
                }
                else
                {
                    funFunciones.MensajeMostrarAviso(Mensajes.CRUD_BUSCAR.ToString(),"");
                    TxtNumRuc.Text = "";
                    TxtNumRuc.Focus();
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
        }
        void DataTableCargar()
        {
            objAlmacen.mysConec = mysConec;
            dtItems = objAlmacen.Listar();         // CARGAMOS TODOS LOS ITEMAS

            objClientes.mysConec = mysConec;
            dtClientes = objClientes.ClienteListar();  // CARGAMOS TODOS LOS CLIENTES

            objAlmacenUniMed.mysConec = mysConec;
            dtUnidadMedida = objAlmacenUniMed.Listar();       // CARGAMOS TODAS LAS UNIDADES DE MEDIDA

            objMoneda.mysConec = mysConec;
            dtMoneda = objMoneda.Listar();       // CARGAMOS TODAS MONEDAS

            objTipDocumento.mysConec = mysConec;
            dtTipDocumento = objTipDocumento.Listar_puntoventa();       // CARGAMOS TIPOS DE DOCUMENTO PARA VENTAS
        }
        private void FgDetalle_Click(object sender, EventArgs e)
        {

        }
        private void FgDetalle_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            DataTable dtUniMed = new DataTable();
            int intProductoId = 0;
            int intUniMedId = 0;
            
            if (e.Col == 1)             // SI SE HA SELECCIONADO ALGUN ITEM DE LA COLUMNA 1
            { 
                string strItemDescripcion = FgDetalle.GetData(e.Row, 1).ToString();
                intProductoId = Convert.ToInt32(funDatos.DataTableBuscar(dtItems, "c_despro", "n_id", strItemDescripcion, "C"));    // OBTENEMOS EL ID DEL ITEM SELECCIONADO
    
                FgDetalle.SetData(e.Row, 6, intProductoId);
                dtUniMed = funDatos.DataTableFiltrar(dtUnidadMedida, "n_idite = " + intProductoId + "");                            //  FILTRAMOS POR ID DEL PRODUCTO
                if (dtUniMed.Rows.Count != 0) { funFlex.FlexColumnaCombo(FgDetalle, dtUniMed, "c_abr", 2);  }

                dtUniMed = funDatos.DataTableFiltrar(dtUniMed, "n_default = 1");                                                    //  FILTRAMOS LA UNIDAD DE MEDIDA POR DEFECTO

                FgDetalle.SetData(e.Row, 2, dtUniMed.Rows[0]["c_abr"].ToString());                                                  // ESTABLECEMOS LA ABREVIATURA DE LA UNIDAD DE MEDIDA 
                FgDetalle.SetData(e.Row, 7, dtUniMed.Rows[0]["n_idunimed"].ToString());                                             // ESTABLECEMOS EL ID DE LA UNIDAD DE MEDIDA 

                double doupreuniigv = Convert.ToDouble(dtUniMed.Rows[0]["n_preuniigv"].ToString());
                double doupreuni = Convert.ToDouble(dtUniMed.Rows[0]["n_preuni"].ToString());
                FgDetalle.SetData(e.Row, 4, doupreuniigv.ToString("0.000000"));                                                     // ESTABLECEMOS EL PRECIO CON IGV DEL ITEM
                FgDetalle.SetData(e.Row, 8, doupreuni.ToString("0.000000"));                                                        // ESTABLECEMOS EL PRECIO SIN IGV DEL ITEM

                CalcularFila(e.Row);
            }

            if (e.Col == 2)             // SI SE HA SELECCIONADO ALGUN ITEM DE LA COLUMNA 2
            {
                intProductoId = Convert.ToInt32(FgDetalle.GetData(e.Row, 6));
                string strUnidadMedidaDescripcion =  FgDetalle.GetData(e.Row, 2).ToString();
                intUniMedId = Convert.ToInt32(funDatos.DataTableBuscar(dtUnidadMedida, "c_abr", "n_idunimed", strUnidadMedidaDescripcion, "C"));
                FgDetalle.SetData(e.Row, 7, intUniMedId);

                dtUniMed = funDatos.DataTableFiltrar(dtUnidadMedida, "n_idunimed = " + intUniMedId + " AND n_idite = " + intProductoId + ""); //  FILTRAMOS LA UNIDAD DE MEDIDA POR DEFECTO

                double doupreuniigv = Convert.ToDouble(dtUniMed.Rows[0]["n_preuniigv"].ToString());
                double doupreuni = Convert.ToDouble(dtUniMed.Rows[0]["n_preuni"].ToString());

                FgDetalle.SetData(e.Row, 2, dtUniMed.Rows[0]["c_abr"].ToString());                                                  // ESTABLECEMOS LA ABREVIATURA DE LA UNIDAD DE MEDIDA 
                FgDetalle.SetData(e.Row, 7, dtUniMed.Rows[0]["n_idunimed"].ToString());                                             // ESTABLECEMOS EL ID DE LA UNIDAD DE MEDIDA 
                FgDetalle.SetData(e.Row, 4, doupreuniigv.ToString("0.000000"));                                                     // ESTABLECEMOS EL PRECIO CON IGV DEL ITEM
                FgDetalle.SetData(e.Row, 8, doupreuni.ToString("0.000000"));                                                        // ESTABLECEMOS EL PRECIO SIN IGV DEL ITEM

                CalcularFila(e.Row);
            }

            if (e.Col == 3)
            {
                CalcularFila(e.Row);
            }
        }
        void CalcularFila(int intFila)
        {
            double douCantidad = Convert.ToDouble(FgDetalle.GetData(FgDetalle.Row, 3));
            double douPreuniIGV = Convert.ToDouble(FgDetalle.GetData(FgDetalle.Row, 4));
            double douPreuni = Convert.ToDouble(FgDetalle.GetData(FgDetalle.Row, 8));

            double douPrecioTotalIGV = douCantidad * douPreuniIGV;
            double douPrecioTotal = douCantidad * douPreuni;

            FgDetalle.SetData(intFila, 5, douPrecioTotalIGV.ToString("0.00"));
            FgDetalle.SetData(intFila, 9, douPrecioTotal.ToString("0.00"));
            SumarTotales();
        }
        void SumarTotales()
        {
            double douPrecioTotal = 0;
            double douPrecioTotalSinIGV = 0;
            double douValorIGV = 0;
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
                dtUniMed = funDatos.DataTableFiltrar(dtUnidadMedida, "n_idite = " + intProductoId + "");
                funFlex.FlexColumnaCombo(FgDetalle, dtUniMed, "c_abr", 2);
            }
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (Grabar() == false) 
            {
                MessageBox.Show("");
            }

            CN_vta_ventas objVentas = new CN_vta_ventas();

            if (objVentas.Insertar(STUVENTAS) == true)
            {
                MessageBox.Show("Se Grabo con exito");
            }
            else
            {
                MessageBox.Show("No se pudo guardar");
            }
        }
        bool Grabar()
        {
            bool booOk = false;
            int intFila = 0;
            
            BE_VTA_VENTAS entDocumento = new BE_VTA_VENTAS() ;
            BE_VTA_VENTASDET entDocumentoDet = new BE_VTA_VENTASDET();

            entDocumento.n_idven = 1;
            entDocumento.n_idemp = STU_SISTEMA.EMPRESAID;
            entDocumento.n_anotra = STU_SISTEMA.ANOTRABAJO;
            entDocumento.n_idmes = STU_SISTEMA.MESTRABAJO;
            entDocumento.n_idlib = 2;
            entDocumento.c_numreg = "0001";
            entDocumento.n_idtippro = 3;
            entDocumento.n_idcli = 1;
            //if (funFunciones.NulosC(LblIdCliente.Text) != "") 
            //{
            //    entDocumento.n_idcli =Convert.ToInt32(LblIdCliente.Text);
            //}
            entDocumento.n_idpunvencli = 0;
            entDocumento.n_idtipdoc = Convert.ToInt32(CboTipDocumento.SelectedValue);
            entDocumento.c_numser = LblSerDoc.Text;

            //OBTENEMOS EL NUEVO NUMERO DE DOCUMENTO ANTES DE GUARDAR LA OPERACION
            objNumeroDoc.mysConec = mysConec;
            entDocumento.c_numdoc = objNumeroDoc.HallaNumeroDocumento(entDocumento.n_idemp, entDocumento.n_idtipdoc, entDocumento.c_numser, Constantes.SYS_DOCNUM.GRABAR_NUMERO_GENERADO);

            entDocumento.c_numdoc = LblNumDoc.Text;
            entDocumento.f_fchreg = DateTime.Now;
            entDocumento.f_fchdoc = DateTime.Now;
            entDocumento.f_fchven = DateTime.Now;
            entDocumento.n_idconpag = 1;                                     // CONDICION DE PAGO (1 = CONTADO)
            entDocumento.n_idmon = Convert.ToInt32(CboMoneda.SelectedValue);                
            entDocumento.n_impbru = Convert.ToDouble(LblImpBru.Text);
            entDocumento.n_impbru2 = 0;
            entDocumento.n_impbru3 = 0;
            entDocumento.n_impinaf = 0;
            entDocumento.n_impigv = Convert.ToDouble(LblIgv.Text);
            entDocumento.n_impisc = 0;
            entDocumento.n_impotr = 0;
            entDocumento.n_imptotven = Convert.ToDouble(LblTotal.Text);  
            entDocumento.n_tc = Convert.ToDouble(LblTipCam.Text);
            entDocumento.n_impsal = Convert.ToDouble(LblTotal.Text);  
            entDocumento.n_idven = 0;
            entDocumento.n_tasaigv = douIGVTasa;
            entDocumento.c_glosa = "PUNTO DE VENTA";
            entDocumento.n_oriitem = 1;                                       // (1 = directo; 2 = Guia de Remision;  3 = Cotizacion)
            entDocumento.n_estado = 1;                                        // (0 = anulado ; 1 = activo)
            entDocumento.n_idtipven = 1;                                      // TODO LO QUE SE VENDA AQUI SERA VENTA GRAVADA
            entDocumento.n_idtipdocref = 0;
            entDocumento.n_iddocref = 0;
            entDocumento.n_idtipdes = 1;                                      // SE APLICA EL TIPO DE DESCUENTO PORCENTAJE
            entDocumento.n_impdes = 0;                                        // IMPORTE DEL DESCUENTO OBTENIDO
            entDocumento.c_nomcli = TxtNomCli.Text;
            entDocumento.c_dircli = TxtDireccion.Text;

            STUVENTAS.entDocumento = entDocumento;

            //List <BE_VTA_VENTASDET> objListaDetalle =  BE_VTA_VENTASDET();
            //List<Author> AuthorList = new List<Author>();
            List<BE_VTA_VENTASDET> objListaDetalle = new List<BE_VTA_VENTASDET>();

            for (intFila = 1; intFila <= FgDetalle.Rows.Count-1; intFila++)
            {
                BE_VTA_VENTASDET objDetalle = new BE_VTA_VENTASDET();
                //string strCadena = FgDetalle.GetData(intFila, 1).ToString();

                if (FgDetalle.GetData(intFila, 1) != null)
                { 
                    objDetalle.n_iditem = Convert.ToInt32(FgDetalle.GetData(intFila,6));
                    objDetalle.c_desusu = FgDetalle.GetData(intFila, 1).ToString();
                    objDetalle.n_idunimed = Convert.ToInt32(FgDetalle.GetData(intFila, 7));
                    objDetalle.n_canpro = Convert.ToDouble(FgDetalle.GetData(intFila, 3));
                    objDetalle.n_preunibru = Convert.ToDouble(FgDetalle.GetData(intFila, 8));
                    objDetalle.n_impdes = 0; //Convert.ToDouble(FgDetalle.GetData(intFila, 3));
                    objDetalle.n_preuninet = Convert.ToDouble(FgDetalle.GetData(intFila, 8));
                    objDetalle.n_imptot = Convert.ToDouble(FgDetalle.GetData(intFila, 9));

                    objListaDetalle.Add(objDetalle);
                }
            }

            STUVENTAS.entDocumentodetalle = objListaDetalle;

            objVentas.mysConec = mysConec;
            if (objVentas.Insertar(STUVENTAS) == true)
            {
                booOk = true;
            }


            return booOk;
        }
        private void metroShell1_Click(object sender, EventArgs e)
        {

        }
        private void OptBoleta_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CmdTraerCotiza_Click(object sender, EventArgs e)
        {

        }
        private void TooNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();

            objNumeroDoc.mysConec = mysConec;
            LblNumDoc.Text = objNumeroDoc.HallaNumeroDocumento(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDocumento.SelectedValue), LblSerDoc.Text, Constantes.SYS_DOCNUM.NO_GRABAR_NUMERO_GENERADO);
            if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 2) { TxtNumRuc.Focus(); }
            if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 4) { TxtNomCli.Focus(); }
        }
        private void ToolCliRapido_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 4)
            {
                LimpiarControles();

                TxtNomCli.Text = "VENTA EN MOSTRADOR";
                TxtDireccion.Text =  "VENTA EN MOSTRADOR";
            }
            else
            {
                funFunciones.MensajeMostrarAviso("Esta funcion no aplica para este tipo de documento", strTituloFormulario);
            }
        }
        private void CboTipDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }

            LblTipDocumento.Text = CboTipDocumento.Text;

            if (Convert.ToInt32(CboTipDocumento.SelectedValue) == 2)
            {
                TxtNumRuc.ReadOnly = false;
                TxtNumRuc.Focus();
            }
            else
            {
                TxtNumRuc.ReadOnly = true;
                TxtNomCli.Focus();
            }
        }
        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            if (Grabar() == true)
            {
                funFunciones.MensajeMostrarAviso("El documento se guardo con exito", strTituloFormulario);
                TooNuevo_Click(sender, e);
            }
        }
    }
}
