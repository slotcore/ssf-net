using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Constantes;
using SIAC_Objetos.Sistema;
using MySql.Data.MySqlClient;
using SIAC_Negocio.Cooperativa;

namespace SIAC_NET_Cooperativa
{
    public class CLS_Cooperativa
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public MySqlConnection mysConec = new MySqlConnection();
        public void MantenimientoPuestos()
        {
            Formularios.FrmManPuestos Frm = new Formularios.FrmManPuestos();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void MantenimientoTipoPuestos()
        {
            Formularios.FrmManTipoPuesto Frm = new Formularios.FrmManTipoPuesto();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void MantenimientoSocios()
        {
            Formularios.FrmManSocios Frm = new Formularios.FrmManSocios();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void GenerarCargoMensual()
        {
            Formularios.FrmGenCargos Frm = new Formularios.FrmGenCargos();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void MantenimientoSociosPuestos()
        {
            Formularios.FrmManSociosPuestos Frm = new Formularios.FrmManSociosPuestos();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void MantenimientoConceptos()
        {
            Formularios.FrmManConceptos2 Frm = new Formularios.FrmManConceptos2();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void RegistroServicios()
        {
            Formularios.FrmRegServicios Frm = new Formularios.FrmRegServicios();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void VerCtaCtePuestos()
        {
            Formularios.FrmCtaCtePuestos Frm = new Formularios.FrmCtaCtePuestos();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void VerCtaCte()
        {
            Formularios.FrmManCtaCte Frm = new Formularios.FrmManCtaCte();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void PadronSocios(int n_IdEstadoSocio)
        {
            CN_coo_socios obsoc = new CN_coo_socios();
            obsoc.mysConec = mysConec;
            obsoc.STU_SISTEMA = STU_SISTEMA;
            obsoc.ReportePadronSocios(n_IdEstadoSocio);
        }
        public void VerReporteDeudaSocios()
        {
            CN_coo_cargos obsoc = new CN_coo_cargos();
            obsoc.mysConec = mysConec;
            obsoc.STU_SISTEMA = STU_SISTEMA;
            obsoc.VerReporteDeudaSocios();
        }
    }
}
