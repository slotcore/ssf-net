using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Produccion;
using MySql.Data.MySqlClient;
using Helper.Comunes;
using Helper;
using SIAC_Negocio.Planilla;
using System.Reflection;
using System.Deployment.Application;
using System.Configuration;

namespace SSF_NET.Formularios
{
    public partial class FrmMenu10 : Form
    {
        CN_mae_meses objMeses = new CN_mae_meses();
        DataTable dtMes = new DataTable();
        int second = 0;

        Helper.Genericas miFun = new Helper.Genericas();

        public FrmMenu10()
        {
            InitializeComponent();

            // Verificar actualizacion
            Timer Clock = new Timer();
            Clock.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["ServiceInterval"]);
            Clock.Start();
            Clock.Tick += new EventHandler(Timer_Tick);
        }
        private void FrmMenu10_Load(object sender, EventArgs e)
        {
            //string name = Assembly.GetEntryAssembly().FullName;
            objMeses.mysConec = Program.mysConeccion;
            dtMes = objMeses.Listar();

            this.Text = string.Format("{0} - {1}", Program.STU_SISTEMA.SYS_NOMBRE, Program.STU_SISTEMA.SYS_VESION);
            Program.STU_SISTEMA.MONEDA = 1;                           // MONEDA POR DEFECTO SOLES 

            CN_sys_perfil o_perfil= new CN_sys_perfil();
            o_perfil.mysConec = Program.mysConeccion;
            o_perfil.ActivarOpcionesPerfil(Program.STU_SISTEMA.USUARIOPERFIL, menuStrip1);

            toolStripStatusLabel2.Text = "";
            toolStripStatusLabel4.Text = "";
            toolStripStatusLabel6.Text = "";
            toolStripStatusLabel8.Text = "";

            if (Program.STU_SISTEMA.EMPRESAID == 0)
            {
                Program.mysConeccion.Close();
                Program.AccConeccion.Close();
                MessageBox.Show("No ha seleccionado ninguna Empresa", "SSF Soft", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.Close();
                Application.Exit();
            }
            else
            {
                toolStripStatusLabel2.Text = Program.STU_SISTEMA.EMPRESANOMBRE;
                toolStripStatusLabel4.Text = Program.STU_SISTEMA.ANOTRABAJO.ToString();

                string c_Dato = miFun.DataTableBuscar(dtMes, "n_id", "c_des", Program.STU_SISTEMA.MESTRABAJO.ToString(), "N").ToString();
                toolStripStatusLabel6.Text = c_Dato;
                toolStripStatusLabel8.Text = Program.STU_SISTEMA.USUARIOALIAS; 
            }

            TsBarraActualizacion.Visible = false;
            TsModOffline.Visible = false;
        }

        public void Timer_Tick(object sender, EventArgs eArgs)
        {
            try
            {
                if (!TsBarraActualizacion.Visible)
                {
                    if (VerificarNuevaVersion())
                    {
                        TsBarraActualizacion.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                TsModOffline.Visible = true;
                TsLabelMensajeOffline.Text = ex.Message;
            }
        }

        private void TooBut4_Click(object sender, EventArgs e)
        {
            Program.GuardarIngreso(2);
            Application.Exit();

            //DataTable dtResult = new DataTable();
            //string[,] arrCabeceraFlexFil = new string[4, 4];
            //Genericas funDatos = new Genericas();
            //string c_cadIN = "";
            
            //CN_pla_empleados objEmpleado = new CN_pla_empleados(Program.STU_SISTEMA);
            //objEmpleado.STU_SISTEMA = Program.STU_SISTEMA;
            //objEmpleado.Consulta1(Program.STU_SISTEMA.EMPRESAID, c_cadIN);
            //dtResult = objEmpleado.dtLista;
            //objEmpleado = null;

            //// FLEX GRID DE LOS TAREAS
            //arrCabeceraFlexFil[0, 0] = "Apellidos y Nombres";
            //arrCabeceraFlexFil[0, 1] = "300";
            //arrCabeceraFlexFil[0, 2] = "C";
            //arrCabeceraFlexFil[0, 3] = "c_apenom";

            //arrCabeceraFlexFil[1, 0] = "Nº DNI";
            //arrCabeceraFlexFil[1, 1] = "80";
            //arrCabeceraFlexFil[1, 2] = "C";
            //arrCabeceraFlexFil[1, 3] = "c_numdocide";

            //arrCabeceraFlexFil[2, 0] = "Sel.";
            //arrCabeceraFlexFil[2, 1] = "40";
            //arrCabeceraFlexFil[2, 2] = "B";
            //arrCabeceraFlexFil[2, 3] = "n_sel";

            //arrCabeceraFlexFil[3, 0] = "ID";
            //arrCabeceraFlexFil[3, 1] = "0";
            //arrCabeceraFlexFil[3, 2] = "N";
            //arrCabeceraFlexFil[3, 3] = "n_id";

            //funDatos.Filtrar_CampoOrden = "c_apenom";
            //funDatos.Filtrar_Titulo = "Filtro de Trabajadores";
            //funDatos.Filtrar_ColumnaCheck = 3;
            //dtResult = funDatos.Filtrar2(arrCabeceraFlexFil, dtResult);
            //if (dtResult != null)
            //{ 
            //    if (dtResult.Rows.Count != 0)
            //    {
            //    }
            //}
        }
        private void aprobarOrdeDeRequerimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void maestroDeItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.n_DeDonde = 1;                           // INDICAMOS QUE EL FORMULARIO ES INVOCADO DESDE EL MENU
            objForm.MantenimientoItems();
        }
        private void ingresosAlmacenToolStripMenuItem_Click(object sender, EventArgs e)
        { 

        }
        private void salidasAlmacenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.AccConec = Program.AccConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.SalidaAlmacen();
        }
        private void TooBut1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();

            MySqlConnection mysConTMP = new MySqlConnection();
            //mysConTMP = Program.Conectar("data04");
            FrmSelEmpresa MiForm = new FrmSelEmpresa();
            MiForm.n_VienedelMenu = 1;
            MiForm.mysConec = Program.mysConeccion;
            MiForm.ShowDialog();

            toolStripStatusLabel2.Text = Program.STU_SISTEMA.EMPRESANOMBRE;
            toolStripStatusLabel4.Text = Program.STU_SISTEMA.ANOTRABAJO.ToString();

            string c_Dato = miFun.DataTableBuscar(dtMes, "n_id", "c_des", Program.STU_SISTEMA.MESTRABAJO.ToString(), "N").ToString();
            toolStripStatusLabel6.Text = c_Dato;
            toolStripStatusLabel8.Text = Program.STU_SISTEMA.USUARIOALIAS;
        }
        private void FrmMenu10_Activated(object sender, EventArgs e)
        {
            if (Program.STU_SISTEMA.EMPRESAID == 0)
            {
                Application.Exit();
            }
            else
            {
                toolStripStatusLabel2.Text = Program.STU_SISTEMA.EMPRESANOMBRE;
                toolStripStatusLabel4.Text = Program.STU_SISTEMA.ANOTRABAJO.ToString();

                string c_Dato = miFun.DataTableBuscar(dtMes, "n_id", "c_des", Program.STU_SISTEMA.MESTRABAJO.ToString(), "N").ToString();
                toolStripStatusLabel6.Text = c_Dato;
                toolStripStatusLabel8.Text = Program.STU_SISTEMA.USUARIOALIAS;
                VerMenuEstacionamiento();
            }
        }
        void VerMenuEstacionamiento()
        {
            if (Program.STU_SISTEMA.USUARIOPERFIL == 10)
            {
                
                almacenToolStripMenuItem.Visible = false;
                comprasToolStripMenuItem.Visible = false;
                ventasToolStripMenuItem.Visible = false;
                TesoreriaToolStripMenuItem.Visible = false;
                contabilidadToolStripMenuItem1.Visible = false;
                produccionToolStripMenuItem.Visible = false;
                contabilidadToolStripMenuItem.Visible = false;
                gestionToolStripMenuItem.Visible = false;
                mantenimientoToolStripMenuItem.Visible = false;
                calidadToolStripMenuItem.Visible = false;
                marketingToolStripMenuItem.Visible = false;
                maestrosToolStripMenuItem.Visible = false;

                maestroDeServiciosToolStripMenuItem.Enabled = false;
                maestroDeOperadoresDeCajaToolStripMenuItem.Enabled = false;
                maestoDeClientesToolStripMenuItem.Enabled = true;
                maestroPlayasToolStripMenuItem.Enabled = false;
                setipPlayaToolStripMenuItem.Enabled = false;
                configurarPlayaToolStripMenuItem.Enabled = false;
                controlDePlayaToolStripMenuItem.Enabled = true;
                generacionCargosToolStripMenuItem.Enabled = false;
                liquidacionCajaToolStripMenuItem.Enabled = true;
            }
            else
            {
                almacenToolStripMenuItem.Visible = true;
                comprasToolStripMenuItem.Visible = true;
                ventasToolStripMenuItem.Visible = true;
                TesoreriaToolStripMenuItem.Visible = true;
                contabilidadToolStripMenuItem1.Visible = true;
                produccionToolStripMenuItem.Visible = true;
                contabilidadToolStripMenuItem.Visible = true;
                //gestionToolStripMenuItem.Visible = true;
                //mantenimientoToolStripMenuItem.Visible = true;
                //calidadToolStripMenuItem.Visible = true;
                //marketingToolStripMenuItem.Visible = true;
                //maestrosToolStripMenuItem.Visible = true;

                maestroDeServiciosToolStripMenuItem.Enabled = true;
                maestroDeOperadoresDeCajaToolStripMenuItem.Enabled = true;
                maestoDeClientesToolStripMenuItem.Enabled = true;
                maestroPlayasToolStripMenuItem.Enabled = true;
                setipPlayaToolStripMenuItem.Enabled = true;
                configurarPlayaToolStripMenuItem.Enabled = true;
                controlDePlayaToolStripMenuItem.Enabled = true;
                generacionCargosToolStripMenuItem.Enabled = true;
                liquidacionCajaToolStripMenuItem.Enabled = true;
            }

        }
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {

        }
        private void toolStripStatusLabel5_Click(object sender, EventArgs e)
        {

        }
        private void maestroDeAlmacenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoAlmacenes();
        }
        private void maestroDeUnidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoUnidadMedida();
        }
        private void maestroTipoDeItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoTipoExistencia();
        }
        private void maestroDeFamiliasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoFamilia();
        }
        private void maestroDeClasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoClase();
        }
        private void maestroDeSubClaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoSubClase();
        }
        private void maestroTipoOperacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoTipOpe();
        }
        private void TooBut3_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoTC();

            //string c_NomArchivo = "";
            //string c_Ruta = "";

            //c_NomArchivo = "RptMocvimiento.rpt";
            //c_Ruta = @"C:\ssf-net\reportes\almacen\" + c_NomArchivo;

            //Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            //xVisor.b_VisPrev = true;
            //xVisor.c_Titulo = "PUNTO DE VENTA - IMPRESION DE COMPROBANTES";
            //xVisor.c_PathRep = c_Ruta;
            //xVisor.VerCrystal();
        }
        private void consultaIngresosSalidasDeAlmacenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ReporteMovimientosAlmacen();
        }
        private void reportesDeInventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ReporteItems();
        }
        private void maestroDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoCliPro(1);
        }
        private void maestrosToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }
        private void maestroDeVendedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoVendedor();
        }
        private void puntosDeVentaDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoPuntoVenta(1);
        }
        private void maestroEmpresasTransporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoEmpTra();
        }
        private void maestroDeChoferesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoChofer();
        }
        private void maestroDeUnidadesDeTransporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoVehiculo();
        }
        private void maestroMotivosDeTrasladoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoMotTraslado();
        }
        private void conceptosNCNDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoConceptoNC();
        }
        private void mestroDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoItemsCEN();
        }
        private void guiasDeRemisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            //objForm.MantenimientoGuias();
            objForm.MantenimientoGuiasPrecios();
        }
        private void registroDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoVentas();
        }
        private void otrosItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.AccConec = Program.AccConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.IngresoAlmacen();
        }
        private void materiaPrimaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.AccConec = Program.AccConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.IngresoAlmacenMP();
        }
        private void consultaDeLotesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void saldosPorLoteResumidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objCls = new SSF_NET_Almacen.CLS_Almacen();
            objCls.mysConec = Program.mysConeccion;
            objCls.STU_SISTEMA = Program.STU_SISTEMA;
            objCls.SaldosPorLotes();
        }
        private void saldosPorLoteDetalladoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objCls = new SSF_NET_Almacen.CLS_Almacen();
            objCls.mysConec = Program.mysConeccion;
            objCls.STU_SISTEMA = Program.STU_SISTEMA;
            objCls.SaldosPorLotesDet();
        }
        private void revisionDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.AccConec = Program.AccConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoRevision();
        }
        private void planDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Gestion.CLS_Gestion o_Ges = new SSF_NET_Gestion.CLS_Gestion();
            o_Ges.mysConec = Program.mysConeccion;
            o_Ges.STU_SISTEMA = Program.STU_SISTEMA;
            o_Ges.MantenimientoPlanVentas();
        }
        private void consultaDeAsistenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void kardexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.AccConec = Program.AccConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.KardexResumen(false);
        }
        private void marcacionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void consultaDeAsistenciaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Planillas.CLS_Planillas objForm = new SSF_NET_Planillas.CLS_Planillas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MostrarAsistencia();
        }
        private void reporteDeCumpleañosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Planillas.CLS_Planillas objForm = new SSF_NET_Planillas.CLS_Planillas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ConsultaCumpleanos();
        }
        private void consultaAsistenciaPorPersonaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Planillas.CLS_Planillas objForm = new SSF_NET_Planillas.CLS_Planillas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ConsultaAsistenciaPersona();
        }
        private void registroGuiaDeRemisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoGuias();
        }
        private void consultaDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.n_IdLibro = 14;
            objForm.ConsultaVentas();
        }
        private void registroDeComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void ordenDeRequerimientoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoOR(28);
        }
        private void ordenDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void tareasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoTareas();
        }
        private void estacionalidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoEstacionalidad();
        }
        private void ordenDeProducionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoOrdenProduccion();
        }
        private void consultaDeProduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ConsultaProduccion();
        }
        private void calcularLineasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.CalculadoraLineas();
        }
        private void redimientosMateriaPrimaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoRendimiento();
        }
        private void programarProduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoProgramaProduccion();
        }
        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoProductos();
        }
        private void registroProduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.RegistroProduccion();
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            

            
        }
        private void pedidosClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {

        }
        private void generarPlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void procesarJornalesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void equiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Mantenimiento.CLS_Mantenimiento objForm = new SSF_NET_Mantenimiento.CLS_Mantenimiento();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoEquipos();
        }
        private void maestroDeEquiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Mantenimiento.CLS_Mantenimiento objForm = new SSF_NET_Mantenimiento.CLS_Mantenimiento();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoEquipos();
        }
        private void solicitudDeMaterialesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void maestroPuestosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Cooperativa.CLS_Cooperativa objFrm = new SIAC_NET_Cooperativa.CLS_Cooperativa();
            objFrm.mysConec = Program.mysConeccion;
            objFrm.STU_SISTEMA = Program.STU_SISTEMA;
            objFrm.MantenimientoPuestos();
        }
        private void maestroTipoPuestosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Cooperativa.CLS_Cooperativa objFrm = new SIAC_NET_Cooperativa.CLS_Cooperativa();
            objFrm.mysConec = Program.mysConeccion;
            objFrm.STU_SISTEMA = Program.STU_SISTEMA;
            objFrm.MantenimientoTipoPuestos();
        }
        private void asociadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Cooperativa.CLS_Cooperativa objFrm = new SIAC_NET_Cooperativa.CLS_Cooperativa();
            objFrm.mysConec = Program.mysConeccion;
            objFrm.STU_SISTEMA = Program.STU_SISTEMA;
            objFrm.MantenimientoSocios();
        }
        private void generacionCargosMensualesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Cooperativa.CLS_Cooperativa objFrm = new SIAC_NET_Cooperativa.CLS_Cooperativa();
            objFrm.mysConec = Program.mysConeccion;
            objFrm.STU_SISTEMA = Program.STU_SISTEMA;
            objFrm.GenerarCargoMensual();
        }
        private void gestionDePuestosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Cooperativa.CLS_Cooperativa objFrm = new SIAC_NET_Cooperativa.CLS_Cooperativa();
            objFrm.mysConec = Program.mysConeccion;
            objFrm.STU_SISTEMA = Program.STU_SISTEMA;
            objFrm.MantenimientoSociosPuestos();
        }
        private void maestroDeConceptosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Cooperativa.CLS_Cooperativa objFrm = new SIAC_NET_Cooperativa.CLS_Cooperativa();
            objFrm.mysConec = Program.mysConeccion;
            objFrm.STU_SISTEMA = Program.STU_SISTEMA;
            objFrm.MantenimientoConceptos();
        }
        private void registrarTareasProduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void solicitudDeMaterialesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.SolicitudMateriales();
        }
        private void registroTareasProduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
 
        }
        private void importacionDeAsistenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SSF_NET_Planillas.CLS_Planillas objForm = new SSF_NET_Planillas.CLS_Planillas();
            //objForm.mysConec = Program.mysConeccion;
            //objForm.sqlConec = Program.sqlConeccion;
            //objForm.STU_SISTEMA = Program.STU_SISTEMA;
            //objForm.ImportarAsistencia();
        }
        private void cargarServiciosAAsociadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Cooperativa.CLS_Cooperativa objFrm = new SIAC_NET_Cooperativa.CLS_Cooperativa();
            objFrm.mysConec = Program.mysConeccion;
            objFrm.STU_SISTEMA = Program.STU_SISTEMA;
            objFrm.RegistroServicios();
        }
        private void padronDeAsociadosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void ordenDeRequerimientoiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoOR();
        }
        private void ordenDeRequerimientoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoOR();
        }
        private void ordenDeRequerimientoToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoOR();
        }
        private void registroDe4taCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void consultaDeComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.n_IdLibro = 8;
            objForm.ConsultaCompras();
        }
        private void consultaDe4taCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.n_IdLibro = 32;
            objForm.ConsultaCompras();
        }
        private void registroDeVentasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.PuntoVenta();
        }
        private void activosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Cooperativa.CLS_Cooperativa obj = new SIAC_NET_Cooperativa.CLS_Cooperativa();
            obj.mysConec = Program.mysConeccion;
            obj.STU_SISTEMA = Program.STU_SISTEMA;
            obj.PadronSocios(1);  // SOCIOS ACTIVOS
        }
        private void noActivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Cooperativa.CLS_Cooperativa obj = new SIAC_NET_Cooperativa.CLS_Cooperativa();
            obj.mysConec = Program.mysConeccion;
            obj.STU_SISTEMA = Program.STU_SISTEMA;
            obj.PadronSocios(2); // SOCIOS NO ACTIVOS
        }
        private void porAsociadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Cooperativa.CLS_Cooperativa objFrm = new SIAC_NET_Cooperativa.CLS_Cooperativa();
            objFrm.mysConec = Program.mysConeccion;
            objFrm.STU_SISTEMA = Program.STU_SISTEMA;
            objFrm.VerCtaCte();
        }
        private void hojaLiquidacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objVen = new SSF_NET_Ventas.CLS_Ventas();
            objVen.mysConec = Program.mysConeccion;
            objVen.STU_SISTEMA = Program.STU_SISTEMA;
            objVen.VerHojaLiquidacion();
        }
        private void notasDeCreditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            
            objForm.MantenimientoNC(1);  // INDICAMOS QUE ES NOTA DE CREDITO
        }
        private void notasDebitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoNC(2);  // INDICAMOS QUE ES NOTA DE debito
        }
        private void registroComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void registroDe4taCategoriaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoRenta4ta();
        }
        private void reporteDeudaDeSociosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Cooperativa.CLS_Cooperativa obj = new SIAC_NET_Cooperativa.CLS_Cooperativa();
            obj.mysConec = Program.mysConeccion;
            obj.STU_SISTEMA = Program.STU_SISTEMA;
            obj.VerReporteDeudaSocios(); 
        }
        private void notaDeDebitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoNCND(2);                  // LE INDICAMOS QUE DEBE MOSTRAR NOTA DE DEBITO
        }
        private void notaDeCreditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoNCND(1);                  // LE INDICAMOS QUE DEBE MOSTRAR NOTA DE CREDITO
        }
        private void importarPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.PedidosCEN();
        }
        private void planDeCuentasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoPC();
        }
        private void tipoDeCambioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoTC();
        }
        private void detraccionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoDetraccion();
        }
        private void retencionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoRetencion();
        }
        private void percepcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoPercepcion();
        }
        private void librosContablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoLibrosCon();
        }
        private void dopcumentosContablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoDocCom();
        }
        private void impuestosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoImpuestos();
        }
        private void asientosDiversosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ProAsientosDiversos();
        }
        private void registroDeComprasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.RegistroCompras(8);
        }
        private void registroRta4taCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.RegistroCompras(32);
        }
        private void registroDeVentasToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.RegistroCompras(14);
        }
        private void registroDeDetracionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void registroDeRetencionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.Registroretenciones();
        }
        private void registroDePercepcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void comprasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.RegistroPercepciones(2);   // LE INDICAMOS QUE ES PERCEPCION DE COMPRAS
        }
        private void ventasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.RegistroPercepciones(1);   // LE INDICAMOS QUE ES PERCEPCION DE VENTAS
        }
        private void percepcionVentaInternaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.RegistroPercepciones(1);   // LE INDICAMOS QUE ES PERCEPCION DE VENTAS
        }
        private void pdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.RegistroPercepciones(2);   // LE INDICAMOS QUE ES PERCEPCION DE VENTAS
        }
        private void ingresosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Tesoreria.Cls_Tesoreria objForm = new SSF_NET_Tesoreria.Cls_Tesoreria();
            objForm.mysConec =Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoIngresos();
        }
        private void egresosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Tesoreria.Cls_Tesoreria objForm = new SSF_NET_Tesoreria.Cls_Tesoreria();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoEgresos();
        }
        private void maestroDeBancosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Tesoreria.Cls_Tesoreria objForm = new SSF_NET_Tesoreria.Cls_Tesoreria();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoBancos();
        }
        private void mediosDePagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Tesoreria.Cls_Tesoreria objForm = new SSF_NET_Tesoreria.Cls_Tesoreria();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoMedioPago();
        }
        private void maestroDeSeriresXAlmacenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoDocAlmacen();
        }
        private void documentosDeCajaYBancosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Tesoreria.Cls_Tesoreria objForm = new SSF_NET_Tesoreria.Cls_Tesoreria();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoDocumentos();
        }
        private void origenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void destinoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void ingresosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Tesoreria.Cls_Tesoreria objForm = new SSF_NET_Tesoreria.Cls_Tesoreria();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoOrigen(1);
        }
        private void egresosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Tesoreria.Cls_Tesoreria objForm = new SSF_NET_Tesoreria.Cls_Tesoreria();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoOrigen(2);
        }
        private void ingresoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Tesoreria.Cls_Tesoreria objForm = new SSF_NET_Tesoreria.Cls_Tesoreria();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoDestino(1);
        }
        private void destinoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Tesoreria.Cls_Tesoreria objForm = new SSF_NET_Tesoreria.Cls_Tesoreria();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoDestino(2);
        }
        private void asignarCtaContableADocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoCtaConDoc();
        }
        private void cerrarMesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.CerrarMes();
        }
        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoCliPro(2);
        }
        private void puntoVentaClienteProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoPuntoVenta(2);
        }
        private void consultaDeDespachoxPorGuiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ConsultaGuias();
        }
        private void cajeDeDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Tesoreria.Cls_Tesoreria objForm = new SSF_NET_Tesoreria.Cls_Tesoreria();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.CanjeDocumentos();
        }
        private void emisionGuiasTransporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoGuias();
        }
        private void analisisClienteProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Tesoreria.Cls_Tesoreria objForm = new SSF_NET_Tesoreria.Cls_Tesoreria();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.VerCtaCteClientes();
        }
        private void ctaCteDelProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Tesoreria.Cls_Tesoreria objForm = new SSF_NET_Tesoreria.Cls_Tesoreria();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.VerCtaCteProveedor();
        }

        private void cuentasDeBancosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Tesoreria.Cls_Tesoreria objForm = new SSF_NET_Tesoreria.Cls_Tesoreria();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoCuentaBancos();
        }

        private void tareasDeProduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.SolicitudTareas(1);
        }

        private void tareasDiversasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.SolicitudTareasDiversas(1);
        }

        private void jornalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Planillas.CLS_Planillas objForm = new SSF_NET_Planillas.CLS_Planillas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ProcesarJornales();
        }
        private void tareasDeProduccionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.SolicitudTareas(2);
        }
        private void tareasDiversasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.SolicitudTareasDiversas(2);
        }

        private void libroDiarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.Diario();
        }

        private void proformaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoVentasNoEmitidas();
        }

        private void compraNacionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoCompras();
        }

        private void importacionDeBienesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoImportacion();
        }

        private void ventasToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.VentasAnuales();
        }

        private void guiasRemisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.GuiasAnuales(1);                              // INDICAMOS QUE LAS GUIAS QUE MOSTRARA SON POR VENTA
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoPedidosClientes();
        }

        private void ctaCtePedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.CtaCtePedidos();
        }

        private void ordenDeCompraToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoOC();
        }

        private void comprasToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.RegistroDetracciones(1);
        }

        private void ventasToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.RegistroDetracciones(2);
        }

        private void conciliacionBancariaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Tesoreria.Cls_Tesoreria objForm = new SSF_NET_Tesoreria.Cls_Tesoreria();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ConciliacionBancarua();
        }

        private void jornalesUnificadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Planillas.CLS_Planillas objForm = new SSF_NET_Planillas.CLS_Planillas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ProcesarJornalesUni();
        }

        private void letrasAClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Tesoreria.Cls_Tesoreria objForm = new SSF_NET_Tesoreria.Cls_Tesoreria();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.EmisionLetras();
        }

        private void paretoClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.RepMarketing(1);
        }

        private void paretoItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.RepMarketing(2);
        }

        private void asignarUsuariosAEmpresasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mantenimientoDeEmpresasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmEmpresaUsuario frmEmp = new FrmEmpresaUsuario();
            frmEmp.mysConec = Program.mysConeccion;
            frmEmp.STU_SISTEMA = Program.STU_SISTEMA;
            frmEmp.Show();
        }

        private void asignarCtaContableAItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.AsignarCtaConItems();
        }

        private void centroDeCostosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void asignarTipoExistenciaParaVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoItemsVender();
        }
        private void balanceComprobacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.BalanceComprobacion();
        }
        private void lbroMayorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.Mayor();
        }
        private void TooBut5_Click(object sender, EventArgs e)
        {
            Helper.Cls_ServiciosSunat xFun = new Helper.Cls_ServiciosSunat();
            xFun.ConsultarRUC();
            xFun = null;
        }
        private void analisisDeComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Gestion.CLS_Gestion o_Ges = new SSF_NET_Gestion.CLS_Gestion();
            o_Ges.mysConec = Program.mysConeccion;
            o_Ges.STU_SISTEMA = Program.STU_SISTEMA;
            o_Ges.AnalizarCompras();
        }
        private void comercialToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void liqidacionDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoLiquidacionCompra();
        }
        private void consultaDeTareaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ConsultarTareas();
        }
        private void planDeProduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Gestion.CLS_Gestion o_Ges = new SSF_NET_Gestion.CLS_Gestion();
            o_Ges.mysConec = Program.mysConeccion;
            o_Ges.STU_SISTEMA = Program.STU_SISTEMA;
            o_Ges.MantenimientoPlanProduccion();
        }
        private void consultasToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
        private void consultaDeTareasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ConsultarTareas();
        }
        private void lineasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void analisisDeProduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Gestion.CLS_Gestion o_Ges = new SSF_NET_Gestion.CLS_Gestion();
            o_Ges.mysConec = Program.mysConeccion;
            o_Ges.STU_SISTEMA = Program.STU_SISTEMA;
            o_Ges.AnalizarProduccion();
        }
        private void maestroDeServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Estacionamientos.CLS_Estacionamiento xFun = new SIAC_NET_Estacionamientos.CLS_Estacionamiento();
            xFun.STU_SISTEMA = Program.STU_SISTEMA;
            xFun.MantenimientoServicios();
        }
        private void maestroDeOperadoresDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Estacionamientos.CLS_Estacionamiento xFun = new SIAC_NET_Estacionamientos.CLS_Estacionamiento();
            xFun.STU_SISTEMA = Program.STU_SISTEMA;
            xFun.MantenimientoCajeros();
        }
        private void configurarPlayaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Estacionamientos.CLS_Estacionamiento xFun = new SIAC_NET_Estacionamientos.CLS_Estacionamiento();
            xFun.STU_SISTEMA = Program.STU_SISTEMA;
            xFun.ConfigurarPuntoVenta();
        }
        private void controlDePlayaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState=FormWindowState.Minimized;
            SIAC_NET_Estacionamientos.CLS_Estacionamiento xFun = new SIAC_NET_Estacionamientos.CLS_Estacionamiento();
            xFun.STU_SISTEMA = Program.STU_SISTEMA;
            xFun.Movimientos();
            if (xFun.b_Expandir == true) { this.WindowState = FormWindowState.Normal; }
        }
        private void setipPlayaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Estacionamientos.CLS_Estacionamiento xFun = new SIAC_NET_Estacionamientos.CLS_Estacionamiento();
            xFun.STU_SISTEMA = Program.STU_SISTEMA;
            xFun.PuntoVentaSetup();
        }
        private void maestroDeMotivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoMotivos();
        }

        private void generacionCargosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Estacionamientos.CLS_Estacionamiento xFun = new SIAC_NET_Estacionamientos.CLS_Estacionamiento();
            xFun.STU_SISTEMA = Program.STU_SISTEMA;
            xFun.GenerarCargos();
        }

        private void formato1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Planillas.CLS_Planillas objForm = new SSF_NET_Planillas.CLS_Planillas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ReporteFormat1("01/10/2018","31/10/2018");
        }

        private void maestoDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Estacionamientos.CLS_Estacionamiento xFun = new SIAC_NET_Estacionamientos.CLS_Estacionamiento();
            xFun.STU_SISTEMA = Program.STU_SISTEMA;
            xFun.MantenimientoClientes();
        }

        private void liquidacionCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SIAC_NET_Estacionamientos.CLS_Estacionamiento xFun = new SIAC_NET_Estacionamientos.CLS_Estacionamiento();
            xFun.STU_SISTEMA = Program.STU_SISTEMA;
            xFun.GenerarLiquidacion();
        }

        private void analisarVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Gestion.CLS_Gestion o_Ges = new SSF_NET_Gestion.CLS_Gestion();
            o_Ges.mysConec = Program.mysConeccion;
            o_Ges.STU_SISTEMA = Program.STU_SISTEMA;
            o_Ges.AnalizarVentas();
        }

        private void formato2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Planillas.CLS_Planillas objForm = new SSF_NET_Planillas.CLS_Planillas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ReporteFormat2();
        }

        private void maestroPlayasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cLientesItemsParaVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoClienteItems(1);
        }

        private void asignarImpuestosADocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoImpuestosDocumentos();
        }

        private void parteDeProduccionSinVisarXAlmacenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ConsultaRevisionesNoJaladasAlmacen(Program.STU_SISTEMA.EMPRESAID);
        }

        private void producionPendientesDeCierreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ConsultaProduccionNoTerminada(Program.STU_SISTEMA.EMPRESAID);
        }
        void EnviarAuditoria()
        {
            CN_pro_produccion o_pro = new CN_pro_produccion();
            o_pro.mysConec = Program.mysConeccion;
            o_pro.STU_SISTEMA = Program.STU_SISTEMA;
            o_pro.Consulta10_A(Program.STU_SISTEMA.EMPRESAID);
            o_pro.Consulta9_A(Program.STU_SISTEMA.EMPRESAID);
            o_pro = null;
        }
        void EnviarAlmacen()
        { 
        }
        void EnviarParteProduccion()
        {
            
        }

        private void TooBut2_Click(object sender, EventArgs e)
        {
            //EnviarAuditoria();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //second = second + 1;
            //TimeSpan span1 = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));

            //if (span1 <= TimeSpan.Parse("12:00:00"))
            //{
            //    if ((span1 >= TimeSpan.Parse("11:00:00")) && (span1 <= TimeSpan.Parse("11:10:00")))
            //    {
            //        EnviarAuditoria();
            //        timer1.Stop();
            //        //MessageBox.Show("Exiting timer...");
            //    }
            //}
            //if (span1 > TimeSpan.Parse("12:00:00"))
            //{
            //    if ((span1 >= TimeSpan.Parse("16:00:00")) && (span1 <= TimeSpan.Parse("16:10:00")))
            //    {
            //        EnviarAuditoria();
            //        timer1.Stop();
            //        //MessageBox.Show("Exiting timer...");
            //    }
            //}
        }
        private void mantenimientoDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManUsuarios xFrm = new FrmManUsuarios();
            xFrm.STU_SISTEMA = Program.STU_SISTEMA;
            xFrm.mysConec = Program.mysConeccion;
            xFrm.ShowDialog();
        }
        private void resumenDiarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoResumenDiario();
        }
        private void planDeAbastecimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Gestion.CLS_Gestion o_Ges = new SSF_NET_Gestion.CLS_Gestion();
            o_Ges.mysConec = Program.mysConeccion;
            o_Ges.STU_SISTEMA = Program.STU_SISTEMA;
            o_Ges.MantenimientoPlanAbastecimiento();
        }

        private void produccionTotalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Gestion.CLS_Gestion o_Ges = new SSF_NET_Gestion.CLS_Gestion();
            o_Ges.mysConec = Program.mysConeccion;
            o_Ges.STU_SISTEMA = Program.STU_SISTEMA;
            o_Ges.ProduccionUnificado();
        }

        private void planDeVentasUnificadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Gestion.CLS_Gestion o_Ges = new SSF_NET_Gestion.CLS_Gestion();
            o_Ges.mysConec = Program.mysConeccion;
            o_Ges.STU_SISTEMA = Program.STU_SISTEMA;
            o_Ges.PlanVentasUnificado();
        }

        private void planDeProduccionUnificadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Gestion.CLS_Gestion o_Ges = new SSF_NET_Gestion.CLS_Gestion();
            o_Ges.mysConec = Program.mysConeccion;
            o_Ges.STU_SISTEMA = Program.STU_SISTEMA;
            o_Ges.PlanProduccionUnificado();
        }

        private void planDeAbastecimientoUnificadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Gestion.CLS_Gestion o_Ges = new SSF_NET_Gestion.CLS_Gestion();
            o_Ges.mysConec = Program.mysConeccion;
            o_Ges.STU_SISTEMA = Program.STU_SISTEMA;
            o_Ges.PlanAbastecimientoUnificado();
        }

        private void kardexValorizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.AccConec = Program.AccConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.KardexResumen(true);
        }

        private void ventasPorPeriodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Ventas.CLS_Ventas objForm = new SSF_NET_Ventas.CLS_Ventas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.VentasPeriodo();
        }

        private void activosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SSF_NET_Planillas.CLS_Planillas objForm = new SSF_NET_Planillas.CLS_Planillas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ManEmpleadosActivos();
        }

        private void inactivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Planillas.CLS_Planillas objForm = new SSF_NET_Planillas.CLS_Planillas();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ManEmpleadosInactivos();
        }

        private void ordenDeRequerimientoToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoOR(12);
        }

        private void salidasAlmacenVariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.AccConec = Program.AccConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.SalidaAlmacenVarios();
        }

        private void aprobadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Logistica.CLS_Logistica objForm = new SSF_NET_Logistica.CLS_Logistica();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoAprobadores();
        }

        private void maestroDePersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.MantenimientoPersonal();
        }

        private void transferenciasEntreAlmacenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Almacen.CLS_Almacen objForm = new SSF_NET_Almacen.CLS_Almacen();
            objForm.mysConec = Program.mysConeccion;
            objForm.AccConec = Program.AccConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.TransferenciaAlmacenes();
        }

        private void toolStripMenuItem43_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ConsultaRevisionesNoJaladasAlmacen(Program.STU_SISTEMA.EMPRESAID);
        }

        private void toolStripMenuItem44_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ConsultaProduccionNoTerminada(Program.STU_SISTEMA.EMPRESAID);
        }

        private void costoDeProducciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.CostoProduccion();
        }

        private void configuraciónValorizaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.ConfiguracionValorizacion();
        }

        private void inventarioInicialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Contabilidad.CLS_Contabilidad objForm = new SSF_NET_Contabilidad.CLS_Contabilidad();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.InventarioInicial();
        }

        private void LnkLabelReiniciar_Click(object sender, EventArgs e)
        {
            ReiniciarAplicativo();
        }

        private bool VerificarNuevaVersion()
        {
            bool nuevaVersion = false;
            TsModOffline.Visible = false;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                UpdateCheckInfo info;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    throw new Exception("La nueva versión de la aplicación no se puede descargar en este momento. \n\nComprueba tu conexión de red o vuelve a intentarlo más tarde. Error: " + dde.Message);
                }
                catch (InvalidDeploymentException ide)
                {
                    throw new Exception("No se puede buscar una nueva versión de la aplicación. La implementación de ClickOnce está dañada. Vuelva a implementar la aplicación y vuelva a intentarlo. Error: " + ide.Message);
                }
                catch (InvalidOperationException ioe)
                {
                    throw new Exception("Esta aplicación no se puede actualizar. Probablemente no sea una aplicación ClickOnce. Error: " + ioe.Message);
                }

                if (info.UpdateAvailable)
                {
                    nuevaVersion = true;
                }
            }

            return nuevaVersion;
        }

        private void ReiniciarAplicativo()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                LnkLabelReiniciar.Text = "Actualizando por favor espere...";
                LnkLabelReiniciar.Enabled = false;
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                ad.Update();
                //Application.ExitThread();
                Application.Restart();
            }
            catch (DeploymentDownloadException dde)
            {
                MessageBox.Show("No se puede instalar la última versión de la aplicación. \n\nPor favor comprueba tu conexión de red o vuelve a intentarlo más tarde. Error: " + dde);
                return;
            }
            finally 
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void cronogramaDeProduccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SSF_NET_Produccion.CLS_Produccion objForm = new SSF_NET_Produccion.CLS_Produccion();
            objForm.mysConec = Program.mysConeccion;
            objForm.STU_SISTEMA = Program.STU_SISTEMA;
            objForm.CronogramaProduccion();
        }

        private void procesarPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
