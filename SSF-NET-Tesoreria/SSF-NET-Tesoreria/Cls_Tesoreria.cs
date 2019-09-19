using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Constantes;
using SIAC_Objetos.Sistema;
using MySql.Data.MySqlClient;
using SIAC_Negocio.Ventas;

namespace SSF_NET_Tesoreria
{
    public class Cls_Tesoreria
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public MySqlConnection mysConec = new MySqlConnection();
        
        public void MantenimientoIngresos()
        {
            Formularios.FrmManIngresos FrmMan = new Formularios.FrmManIngresos();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void MantenimientoEgresos()
        {
            Formularios.FrmManEgresos FrmMan = new Formularios.FrmManEgresos();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void MantenimientoBancos()
        {
            Formularios.FrmManBancos FrmMan = new Formularios.FrmManBancos();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void MantenimientoCuentaBancos()
        {
            Formularios.FrmManCueBanco FrmMan = new Formularios.FrmManCueBanco();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void MantenimientoMedioPago()
        {
            Formularios.FrmManMedioPago FrmMan = new Formularios.FrmManMedioPago();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void MantenimientoDocumentos()
        {
            Formularios.FrmManDocumentos FrmMan = new Formularios.FrmManDocumentos();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void MantenimientoOrigen(int n_TipoOrigen)
        {
            //n_TipoOrigen   1 = INGRESO   ;   2  = EGRESO
            Formularios.FrmManOrigen FrmMan = new Formularios.FrmManOrigen();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.n_tipo = n_TipoOrigen;
            FrmMan.Show();
        }
        public void MantenimientoDestino(int n_TipoOrigen)
        {
            //n_TipoOrigen   1 = INGRESO   ;   2  = EGRESO
            Formularios.FrmManDestino FrmMan = new Formularios.FrmManDestino();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.n_tipo = n_TipoOrigen;
            FrmMan.Show();
        }
        public void CanjeDocumentos()
        {
            //n_TipoOrigen   1 = INGRESO   ;   2  = EGRESO
            Formularios.FrmCanjeDocumentos FrmCanje = new Formularios.FrmCanjeDocumentos();
            FrmCanje.mysConec = mysConec;
            FrmCanje.STU_SISTEMA = STU_SISTEMA;
            FrmCanje.Show();
        }
        public void VerCtaCteClientes()
        {
            //n_TipoOrigen   1 = INGRESO   ;   2  = EGRESO
            Formularios.FrmCtaCteCliente FrmCtaCteCli = new Formularios.FrmCtaCteCliente();
            FrmCtaCteCli.mysConec = mysConec;
            FrmCtaCteCli.STU_SISTEMA = STU_SISTEMA;
            FrmCtaCteCli.Show();
        }
        public void VerCtaCteProveedor()
        {
            //n_TipoOrigen   1 = INGRESO   ;   2  = EGRESO
            Formularios.FrmCtaCteProveedor FrmCtaCteCli = new Formularios.FrmCtaCteProveedor();
            FrmCtaCteCli.mysConec = mysConec;
            FrmCtaCteCli.STU_SISTEMA = STU_SISTEMA;
            FrmCtaCteCli.Show();
        }
        public void ConciliacionBancarua()
        {
            Formularios.FrmConciliacionBanco FrmMan = new Formularios.FrmConciliacionBanco();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void EmisionLetras()
        {
            Formularios.FrmManLetras FrmMan = new Formularios.FrmManLetras();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
    }
}
