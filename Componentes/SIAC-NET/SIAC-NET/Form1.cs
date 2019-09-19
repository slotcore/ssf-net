using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Entidades;
using SIAC_Negocio;
using SIAC_Objetos.Sistema;
using SIAC_NET_Almacen.Formularios;
using MetroFramework.Forms;

namespace SIAC_NET
{
    public partial class Form1 : MetroForm
    {
        public string striTituloSistema = "";
        MySqlConnection mysConectar = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_sISTEMA = new Sistema.STU_SISTEMA();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Helper.DatosMySql hlpFuncion = new Helper.DatosMySql();

            mysConectar = hlpFuncion.ObtenerConexion("localhost", "data01", "root", "12345678");

            if (mysConectar.State == ConnectionState.Open) 
            {
                MessageBox.Show(" Se conecto con exito", striTituloSistema, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("No se pudo abrir la BD ", striTituloSistema, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //mysConectar = Nothing;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SIAC_Negocio.Maestros.CN_mae_retencion Funciones = new SIAC_Negocio.Maestros.CN_mae_retencion();
            BE_MAE_RETENCION Retencion = new BE_MAE_RETENCION();

            Retencion.n_idret = 1;
            Retencion.c_des = "esto es una demo clase";
            Retencion.n_tas = 12.22;
            Retencion.n_idcueconcom = 1;
            Retencion.n_idcueconven = 1;

            Funciones.mysConec = mysConectar;
            if (Funciones.Insertar(Retencion) == true)
            {
                MessageBox.Show("El registro se guardo con exito");
            }
            else
            {
                MessageBox.Show(Funciones.StrErrorMensaje);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SIAC_Negocio.Maestros.CN_mae_retencion Funciones = new SIAC_Negocio.Maestros.CN_mae_retencion();
            BE_MAE_RETENCION Retencion = new BE_MAE_RETENCION();

            Retencion.n_idret = 4;
            Retencion.c_des = "44444444444444444";
            Retencion.n_tas = 444;
            Retencion.n_idcueconcom = 444;
            Retencion.n_idcueconven = 444;

            Funciones.mysConec = mysConectar;
            if (Funciones.Modificar(Retencion) == true)
            {
                MessageBox.Show("El registro se guardo con exito");
            }
            else
            {
                MessageBox.Show(Funciones.StrErrorMensaje);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SIAC_Negocio.Maestros.CN_mae_retencion Funciones = new SIAC_Negocio.Maestros.CN_mae_retencion();

            Funciones.mysConec = mysConectar;
            if (Funciones.Eliminar(2) == true)
            {
                MessageBox.Show("El registro se guardo con exito");
            }
            else
            {
                MessageBox.Show(Funciones.StrErrorMensaje);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable DtResult = new DataTable();
            SIAC_Negocio.Maestros.CN_mae_retencion Funciones = new SIAC_Negocio.Maestros.CN_mae_retencion();

            Funciones.mysConec = mysConectar;

            DtResult = Funciones.Listar();

            if (DtResult == null) {
                MessageBox.Show(Funciones.StrErrorMensaje);
            }
            else {
                dataGridView1.DataSource = DtResult;
                Funciones.CargarCombo(ref CboRetencion, DtResult);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);

            Program.STU_SISTEMA.ANOTRABAJO = 2015;
            Program.STU_SISTEMA.MESTRABAJO = 10;
            Program.STU_SISTEMA.EMPRESAID  = 1;
            Program.STU_SISTEMA.EMPRESANOMBRE = "EPS Soft";
            Program.STU_SISTEMA.TIPOCAMBIO = 2.783;
            Program.STU_SISTEMA.USUARIOALIAS = "kike";
            Program.STU_SISTEMA.USUARIOID = 1;
            Program.STU_SISTEMA.MONEDA = 1;

            //Helper.Comunes.INulosN ss = new Helper.Comunes.INulosN();
            //Helper.Comunes.Entero16 dd = new Helper.Comunes.Entero16();
            //dd.NulosN.
            //ss.NulosN(2);

            

      

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SIAC_Entidades.Ventas.BE_VTA_VENTAS entVentaCab = new SIAC_Entidades.Ventas.BE_VTA_VENTAS();
            List <SIAC_Entidades.Ventas.BE_VTA_VENTASDET> entVentaDetLista = new List<SIAC_Entidades.Ventas.BE_VTA_VENTASDET>();

            
            SIAC_Negocio.Ventas.CN_vta_ventas Funciones = new SIAC_Negocio.Ventas.CN_vta_ventas();
            SIAC_Objetos.Ventas.Ventas.STU_VTA_VENTAS objVentas;

            objVentas.entDocumento = entVentaCab;
            objVentas.entDocumentodetalle = entVentaDetLista;
            
            if (Funciones.Insertar(objVentas) == true)
            {
                MessageBox.Show("El registro se guardo con exito");
            }
            else
            {
                MessageBox.Show(Funciones.StrErrorMensaje);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SIAC_NET_Ventas.CLS_Ventas clsVentas = new  SIAC_NET_Ventas.CLS_Ventas();

            clsVentas.STU_SISTEMA = Program.STU_SISTEMA;
            clsVentas.mysConec = mysConectar;
            clsVentas.PuntoVenta();

            //FrmPuntoVenta xFrm = new FrmPuntoVenta();
            //xFrm.mysConec = mysConectar;
            //xFrm.STU_SISTEMA = Program.STU_SISTEMA;
            //xFrm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SIAC_NET_Almacen.CLS_Almacen clsItems = new SIAC_NET_Almacen.CLS_Almacen();

            clsItems.STU_SISTEMA = Program.STU_SISTEMA;
            clsItems.mysConec = mysConectar;
            clsItems.MantenimientoItems();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SIAC_NET_Almacen.CLS_Almacen clsItems = new SIAC_NET_Almacen.CLS_Almacen();

            clsItems.STU_SISTEMA = Program.STU_SISTEMA;
            clsItems.mysConec = mysConectar;
            clsItems.IngresoAlmacen();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SIAC_NET_Almacen.CLS_Almacen clsItems = new SIAC_NET_Almacen.CLS_Almacen();

            clsItems.STU_SISTEMA = Program.STU_SISTEMA;
            clsItems.mysConec = mysConectar;
            clsItems.SalidaAlmacen();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SIAC_NET_Compras.CLS_Compras clsCompras = new SIAC_NET_Compras.CLS_Compras();

            clsCompras.STU_SISTEMA = Program.STU_SISTEMA;
            clsCompras.mysConec = mysConectar;
            clsCompras.MantenimientoCompras();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SIAC_NET_Ventas.CLS_Ventas clsVentas = new SIAC_NET_Ventas.CLS_Ventas();

            clsVentas.STU_SISTEMA = Program.STU_SISTEMA;
            clsVentas.mysConec = mysConectar;
            clsVentas.PuntoVenta();
        }
    }
}
