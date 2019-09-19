using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Constantes;
using SIAC_Objetos.Sistema;
using MySql.Data.MySqlClient;

namespace SSF_NET_Ventas
{
    public class CLS_Ventas
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public MySqlConnection mysConec = new MySqlConnection();
        public int n_IdLibro;
        public void MantenimientoCliPro(int n_tipo)
        {
            // n_tipo = 1  cliente
            // n_tipo = 2  proveedor
            Formularios.FrmManCliPro FrmForm = new Formularios.FrmManCliPro();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.n_EsCliPro = n_tipo;
            FrmForm.Show();
        }
        public void MantenimientoVendedor()
        {
            Formularios.FrmManVendedor FrmForm = new Formularios.FrmManVendedor();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.ShowDialog();
        }
        public void MantenimientoPuntoVenta(int n_TipoRegistro)
        {
            Formularios.FrmManPuntoVenta FrmForm = new Formularios.FrmManPuntoVenta();
            FrmForm.n_TipoReg = n_TipoRegistro;
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.ShowDialog();
        }
        public void MantenimientoEmpTra()
        {
            FrmManEmpTransporte FrmForm = new FrmManEmpTransporte();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.ShowDialog();
        }
        public void MantenimientoChofer()
        {
            Formularios.FrmManChoferes FrmForm = new Formularios.FrmManChoferes();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.ShowDialog();
        }
        public void MantenimientoVehiculo()
        {
            Formularios.FrmManVehiculo FrmForm = new Formularios.FrmManVehiculo();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.ShowDialog();
        }
        public void MantenimientoMotTraslado()
        {
            Formularios.FrmManMotTraslado FrmForm = new Formularios.FrmManMotTraslado();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.ShowDialog();
        }
        public void MantenimientoConceptoNC()
        {
            Formularios.FrmManConceptoNCND FrmForm = new Formularios.FrmManConceptoNCND();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.ShowDialog();
        }
        public void MantenimientoItemsCEN()
        {
            Formularios.FrmManItemsCEN FrmForm = new Formularios.FrmManItemsCEN();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.ShowDialog();
        }
        public void MantenimientoGuias()
        {
            Formularios.FrmManGuias FrmForm = new Formularios.FrmManGuias();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoGuiasPrecios()
        {
            Formularios.FrmManGuiasPrecios FrmForm = new Formularios.FrmManGuiasPrecios();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoVentas()
        {
            Formularios.FrmManVentas FrmForm = new Formularios.FrmManVentas();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void PuntoVenta()
        {
            Formularios.FrmPuntoVenta FrmForm = new Formularios.FrmPuntoVenta();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        
        public void MantenimientoVentasNoEmitidas()
        {
            Formularios.FrmManVentasNoEmitidas FrmForm = new Formularios.FrmManVentasNoEmitidas();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoNC(int n_TipoDocumento)
        {
            Formularios.FrmManNotaCredito FrmForm = new Formularios.FrmManNotaCredito();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.n_TIPODOCUMENTO = n_TipoDocumento;
            FrmForm.Show();
        }
        public void MantenimientoPedidosClientes()
        {
            Formularios.FrmManPedidos FrmForm = new Formularios.FrmManPedidos();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void VerHojaLiquidacion()
        {
            Formularios.FrmHojaLiquidacion frmForm = new Formularios.FrmHojaLiquidacion();
            frmForm.mysConec = mysConec;
            frmForm.STU_SISTEMA = STU_SISTEMA;
            frmForm.Show();
        }
        public void PedidosCEN()
        {
            Formularios.FrmPedidosCEN frmForm = new Formularios.FrmPedidosCEN();
            frmForm.mysConec = mysConec;
            frmForm.STU_SISTEMA = STU_SISTEMA;
            frmForm.Show();
        }
        public void ConsultaVentas()
        {
            Formularios.FrmConsultaVentas FrmForm = new Formularios.FrmConsultaVentas();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.n_Libro = n_IdLibro;
            FrmForm.Show();
        }
        public void ConsultaGuias()
        {
            Formularios.FrmVistaGuias FrmForm = new Formularios.FrmVistaGuias();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void VentasAnuales()
        {
            Formularios.FrmVentasAnuales FrmForm = new Formularios.FrmVentasAnuales();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void GuiasAnuales(int n_Origen)
        {
            Formularios.FrmGuiasAnuales FrmForm = new Formularios.FrmGuiasAnuales();
            FrmForm.mysConec = mysConec;
            FrmForm.n_Origen = n_Origen;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void CtaCtePedidos()
        {
            Formularios.FrmCtaCtePedidos FrmForm = new Formularios.FrmCtaCtePedidos();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void RepMarketing(int n_TipoReporte)
        {
            Formularios.FrmRepMarketing FrmForm = new Formularios.FrmRepMarketing();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.n_TipoReporte = n_TipoReporte;
            FrmForm.Show();
        }
        public void MantenimientoItemsVender()
        {
            Formularios.FrmManTipExitVender FrmForm = new Formularios.FrmManTipExitVender();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.ShowDialog();
        }
        public void MantenimientoClienteItems(int n_tipo)
        {
            Formularios.FrmClientePrecio FrmForm = new Formularios.FrmClientePrecio();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.n_EsCliPro = n_tipo;
            FrmForm.Show();
        }
        public void MantenimientoResumenDiario()
        {
            Formularios.FrmResumenDiarioBoletas FrmForm = new Formularios.FrmResumenDiarioBoletas();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void VentasPeriodo()
        {
            Formularios.FrmVentasPeriodo FrmForm = new Formularios.FrmVentasPeriodo();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
    }
}
