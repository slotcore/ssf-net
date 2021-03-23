using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Produccion;
using SIAC_Objetos.Constantes;
using SIAC_Objetos.Sistema;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using SIAC_Negocio.Almacen;

namespace SSF_NET_Produccion
{
    public class CLS_Produccion
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public MySqlConnection mysConec = new MySqlConnection();
        public OleDbConnection AccConec = new OleDbConnection();
        public void MantenimientoRevision()
        {
            Formularios.FrmRevision FrmMan = new Formularios.FrmRevision();
            FrmMan.mysConec = mysConec;
            //FrmMan.AccConec = AccConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
         public void MantenimientoTareas()
        {
            Formularios.FrmMantarea FrmMan = new Formularios.FrmMantarea();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void MantenimientoEstacionalidad()
        {
            Formularios.FrmManEstacionalidad FrmMan = new Formularios.FrmManEstacionalidad();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void MantenimientoOrdenProduccion()
        {
            Formularios.FrmOrdenProduccion FrmMan = new Formularios.FrmOrdenProduccion();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void CalculadoraLineas()
        {
            Formularios.FrmLineaCalculadora FrmMan = new Formularios.FrmLineaCalculadora();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void MantenimientoRendimiento()
        {
            Formularios.FrmManRendimiento FrmMan = new Formularios.FrmManRendimiento();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void MantenimientoProgramaProduccion()
        {
            Formularios.FrmCronograma FrmMan = new Formularios.FrmCronograma();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void CronogramaProduccion()
        {
            Formularios.FrmProgramacionProd FrmMan = new Formularios.FrmProgramacionProd();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void MantenimientoProductos()
        {
            Formularios.FrmManProductos FrmMan = new Formularios.FrmManProductos();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void RegistroProduccion()
        {
            Formularios.FrmRegistroProduccion FrmMan = new Formularios.FrmRegistroProduccion();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void SolicitudMateriales()
        {
            Formularios.FrmSolicitudMateriales2 FrmMan = new Formularios.FrmSolicitudMateriales2();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void SolicitudTareas(int n_DeDonde)
        {
            Formularios.FrmSolicitudTareas FrmMan = new Formularios.FrmSolicitudTareas();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.n_DeDonde = n_DeDonde;
            FrmMan.Show();
        }
        public void SolicitudTareasDiversas(int n_DeDonde)
        {
            Formularios.FrmSolicitudTareasDiversas FrmMan = new Formularios.FrmSolicitudTareasDiversas();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.n_DeDonde = n_DeDonde;
            FrmMan.Show();
        }
        public void ConsultaProduccion()
        {
            Formularios.FrmConsultaProduccion FrmMan = new Formularios.FrmConsultaProduccion();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void ConsultarTareas()
        {
            Formularios.FrmConsultaTareas FrmMan = new Formularios.FrmConsultaTareas();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.Show();
        }
        public void ConsultaRevisionesNoJaladasAlmacen(int n_idemp)
        {
            CN_pro_produccion o_pro = new CN_pro_produccion();
            o_pro.mysConec = mysConec;
            o_pro.Consulta9(n_idemp);
        }
        public void ConsultaProduccionNoTerminada(int n_idemp)
        {
            CN_pro_produccion o_pro = new CN_pro_produccion();
            o_pro.mysConec = mysConec;
            o_pro.Consulta10(n_idemp);
        }
        public void VerEstacionalidad(int n_IdItem)
        {
            Formularios.FrmVerEstacionalidad xFrm = new Formularios.FrmVerEstacionalidad();
            xFrm.mysConec = mysConec;
            xFrm.STU_SISTEMA = STU_SISTEMA;
            xFrm.n_IdItem = n_IdItem;
            xFrm.ShowDialog();
        }  
    }
}
