using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
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
using MetroFramework.Forms;


namespace SIAC_NET_Ventas.Formularios
{
    public partial class FrmPuntoVenta2 : MetroForm
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
        //Ventas.STU_VTA_VENTAS STUVENTAS = new Ventas.STU_VTA_VENTAS();

        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        DatosMySql funDatos = new DatosMySql();

        int intFilasCantidad = 0;
        double douIGVTasa = 0;
        //bool booAgregando = false;
        //string strTituloFormulario = "";

        DataTable dtItems = new DataTable();
        DataTable dtClientes = new DataTable();
        DataTable dtUnidadMedida = new DataTable();
        DataTable dtMoneda = new DataTable();
        DataTable dtTipDocumento = new DataTable();

        public FrmPuntoVenta2()
        {
            InitializeComponent();
        }

        private void FrmPuntoVenta2_Load(object sender, EventArgs e)
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

            LblSerDoc.Text = "0001";
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
            intFilasCantidad = 20;
            douIGVTasa = 18;
            FgDetalle.Rows.Count = intFilasCantidad;
            FgDetalle.Cols[6].Width = 0;                            // ID DEL ITEM  
            FgDetalle.Cols[7].Width = 0;                            // ID DE LA UNIDAD DE MEDIDA
            FgDetalle.Cols[8].Width = 0;                            // PRECIO UNITARIO SIN IGV
            FgDetalle.Cols[9].Width = 0;                            // PRECIO TOTAL SIN IGV
            LblIGVTasa.Text = douIGVTasa.ToString() + "%";

            LblTipCam.Text = STU_SISTEMA.TIPOCAMBIO.ToString("0.000");
        }
        void DataTableCargar()
        {
            objAlmacen.mysConec = mysConec;
            dtItems = objAlmacen.Listar(STU_SISTEMA.EMPRESAID);         // CARGAMOS TODOS LOS ITEMAS

            objClientes.mysConec = mysConec;
            dtClientes = objClientes.ListarCliente();  // CARGAMOS TODOS LOS CLIENTES

            objAlmacenUniMed.mysConec = mysConec;
            dtUnidadMedida = objAlmacenUniMed.Listar();       // CARGAMOS TODAS LAS UNIDADES DE MEDIDA

            objMoneda.mysConec = mysConec;
            dtMoneda = objMoneda.Listar();       // CARGAMOS TODAS MONEDAS

            objTipDocumento.mysConec = mysConec;
            dtTipDocumento = objTipDocumento.Listar_puntoventa();       // CARGAMOS TIPOS DE DOCUMENTO PARA VENTAS
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
    }
}
