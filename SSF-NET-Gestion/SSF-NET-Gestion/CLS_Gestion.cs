using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Constantes;
using SIAC_Objetos.Sistema;
using MySql.Data.MySqlClient;

namespace SSF_NET_Gestion
{
    public class CLS_Gestion
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public MySqlConnection mysConec = new MySqlConnection();

        public void MantenimientoPlanVentas()
        {
            Formularios.FrmManPlanVentas2 FrmPlaVen = new Formularios.FrmManPlanVentas2();
            FrmPlaVen.mysConec = mysConec;
            FrmPlaVen.STU_SISTEMA = STU_SISTEMA;
            FrmPlaVen.Show();
        }
        public void MantenimientoPlanProduccion()
        {
            Formularios.FrmManPlanProduccion FrmPlaPro = new Formularios.FrmManPlanProduccion();
            FrmPlaPro.mysConec = mysConec;
            FrmPlaPro.STU_SISTEMA = STU_SISTEMA;
            FrmPlaPro.Show();
        }
        public void MantenimientoPlanAbastecimiento()
        {
            Formularios.FrmPlanAbastecimiento FrmPlaAba = new Formularios.FrmPlanAbastecimiento();
            FrmPlaAba.mysConec = mysConec;
            FrmPlaAba.STU_SISTEMA = STU_SISTEMA;
            FrmPlaAba.Show();
        }
        public void AnalizarCompras()
        {
            Formularios.FrmAnalisisCompra xFrm = new Formularios.FrmAnalisisCompra();
            xFrm.mysConec = mysConec;
            xFrm.STU_SISTEMA = STU_SISTEMA;
            xFrm.n_Libro = 8;
            xFrm.ShowDialog();
        }
        public void AnalizarProduccion()
        {
            Formularios.FrmAnalisisProduccion xFrm = new Formularios.FrmAnalisisProduccion();
            xFrm.mysConec = mysConec;
            xFrm.STU_SISTEMA = STU_SISTEMA;
            xFrm.ShowDialog();
        }
        public void AnalizarVentas()
        {
            Formularios.FrmAnalisisVentas xFrm = new Formularios.FrmAnalisisVentas();
            xFrm.mysConec = mysConec;
            xFrm.STU_SISTEMA = STU_SISTEMA;
            xFrm.ShowDialog();
        }
        public void ProduccionUnificado()
        {
            Formularios.FrmConsultarProduccion FrmPlaAba = new Formularios.FrmConsultarProduccion();
            FrmPlaAba.mysConec = mysConec;
            FrmPlaAba.STU_SISTEMA = STU_SISTEMA;
            FrmPlaAba.Show();
        }
        public void PlanVentasUnificado()
        {
            Formularios.FrmPlanVentasUni FrmPlaVen = new Formularios.FrmPlanVentasUni();
            FrmPlaVen.mysConec = mysConec;
            FrmPlaVen.STU_SISTEMA = STU_SISTEMA;
            FrmPlaVen.Show();
        }
        public void PlanProduccionUnificado()
        {
            Formularios.FrmPlanProduccionUnificado FrmPlaPro = new Formularios.FrmPlanProduccionUnificado();
            FrmPlaPro.mysConec = mysConec;
            FrmPlaPro.STU_SISTEMA = STU_SISTEMA;
            FrmPlaPro.Show();
        }
        public void PlanAbastecimientoUnificado()
        {
            Formularios.FrmPlanAbastecimientoUni FrmPlaPro = new Formularios.FrmPlanAbastecimientoUni();
            FrmPlaPro.mysConec = mysConec;
            FrmPlaPro.STU_SISTEMA = STU_SISTEMA;
            FrmPlaPro.Show();
        }
    }
}
