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

namespace SIAC_NET_Estacionamientos
{
    public class CLS_Estacionamiento
    {
        public bool b_Expandir = false;

        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public void MantenimientoCajeros()
        {
            Formularios.FrmManCajeros FrmMan = new Formularios.FrmManCajeros();
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void MantenimientoServicios()
        {
            Formularios.FrmManServicios FrmMan = new Formularios.FrmManServicios();
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void Movimientos()
        {
            Formularios.FrmRegistroMovimientos FrmMan = new Formularios.FrmRegistroMovimientos();
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.ShowDialog();
            b_Expandir = true;
        }
        public void ConfigurarPuntoVenta()
        {
            Formularios.FrmConfigurarPunto FrmMan = new Formularios.FrmConfigurarPunto();
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void PuntoVentaSetup()
        {
            Formularios.FrmSetupPlaya FrmMan = new Formularios.FrmSetupPlaya();
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void MantenimientoClientes()
        {
            Formularios.FrmManClientes FrmMan = new Formularios.FrmManClientes();
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.n_DeDonde = 1;
            FrmMan.N_IDCAJERO = STU_SISTEMA.USUARIOID;
            FrmMan.C_CAJERO = STU_SISTEMA.USUARIOALIAS;
            FrmMan.Show();
        }
        public void GenerarCargos()
        {
            Formularios.FrmGeneraCargos FrmMan = new Formularios.FrmGeneraCargos();
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void GenerarLiquidacion()
        {
            Formularios.FrmManLiquidacion FrmMan = new Formularios.FrmManLiquidacion();
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
    }
}
