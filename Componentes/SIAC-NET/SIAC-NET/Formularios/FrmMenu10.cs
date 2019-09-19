using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SIAC_Negocio.Sistema;
using MySql.Data.MySqlClient;

namespace SIAC_NET.Formularios
{
    public partial class FrmMenu10 : DevComponents.DotNetBar.Metro.MetroAppForm
    {
        //public MySqlConnection mysConeccion = new MySqlConnection();
        public FrmMenu10()
        {
            InitializeComponent();
        }

        private void cajaYBancosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void maestroDeItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {


            SIAC_NET_Almacen.CLS_Almacen objForm  = new SIAC_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoItems();
        }

        private void metroStatusBar1_ItemClick(object sender, EventArgs e)
        {

        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {

        }

        private void ingresosAlmacenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Almacen.CLS_Almacen objForm = new SIAC_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.IngresoAlmacen();
        }

        private void salidasAlmacenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Almacen.CLS_Almacen objForm = new SIAC_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.SalidaAlmacen();
        }

        private void registroDeVentasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SIAC_NET_Ventas.CLS_Ventas objForm = new SIAC_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.PuntoVenta();
        }

        private void FrmMenu10_Load(object sender, EventArgs e)
        {
            Program.STU_SISTEMA.ANOTRABAJO = 2015;
            Program.STU_SISTEMA.EMPRESAID = 1;
            Program.STU_SISTEMA.EMPRESANOMBRE = "MI EMPRESA";
            Program.STU_SISTEMA.MESTRABAJO = 1;
            Program.STU_SISTEMA.USUARIOALIAS = "ADMIN";
            Program.STU_SISTEMA.USUARIOID = 1;
            Program.STU_SISTEMA.MONEDA = 1;                           // MONEDA POR DEFECTO SOLES 
        }

        private void TooBut3_Click(object sender, EventArgs e)
        {
            //Form7 MiForm = new Form7();
            ////MiForm.mysConeccion = mysConeccion;
            //MiForm.ShowDialog();
            string c_NomArchivo = "";
            string c_Ruta = "";

            c_NomArchivo = "RPT_ComPunVen.rpt";
            c_Ruta = @"C:\siac-net\reportes\ventas\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "PUNTO DE VENTA - IMPRESION DE COMPROBANTES";
            xVisor.c_PathRep = c_Ruta;
            xVisor.VerCrystal();
        }

        private void TooBut4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}