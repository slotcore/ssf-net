using Helper;
using MySql.Data.MySqlClient;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Planilla;
using SIAC_Objetos.Constantes;
using SIAC_Objetos.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SSF_NET_Planillas
{
    public class CLS_Planillas
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public MySqlConnection mysConec = new MySqlConnection();
        public SqlConnection sqlConec = new SqlConnection();

        public void ConsultaAsistencia()
        {
            Formularios.FrmConsultaAsistencia2 FrmFrmCon = new Formularios.FrmConsultaAsistencia2();
            FrmFrmCon.mysConec = mysConec;
            FrmFrmCon.STU_SISTEMA = STU_SISTEMA;
            FrmFrmCon.ShowDialog();
        }
        public void ConsultaCumpleanos()
        {
            Formularios.FrmRepCumpleaños FrmFrmCon = new Formularios.FrmRepCumpleaños();
            FrmFrmCon.mysConec = mysConec;
            FrmFrmCon.STU_SISTEMA = STU_SISTEMA;
            FrmFrmCon.ShowDialog();
        }
        public void ConsultaAsistenciaPersona()
        {
            Formularios.FrmRepAsistenciaPersona FrmFrmCon = new Formularios.FrmRepAsistenciaPersona();
            FrmFrmCon.mysConec = mysConec;
            FrmFrmCon.STU_SISTEMA = STU_SISTEMA;
            FrmFrmCon.ShowDialog();
        }
        public void ProcesarJornales()
        {
            Formularios.FrmProcesarDestajo frmForm = new Formularios.FrmProcesarDestajo();
            frmForm.mysConec = mysConec;
            frmForm.STU_SISTEMA = STU_SISTEMA;
            frmForm.Show();
        }
        public void ProcesarJornalesUni()
        {
            Formularios.FrmProcesarDestajoUni frmForm = new Formularios.FrmProcesarDestajoUni();
            frmForm.mysConec = mysConec;
            frmForm.STU_SISTEMA = STU_SISTEMA;
            frmForm.Show();
        }
        public void ManEmpleados()
        {
            Formularios.FrmManEmpleados frmForm = new Formularios.FrmManEmpleados();
            frmForm.mysConec = mysConec;
            frmForm.STU_SISTEMA = STU_SISTEMA;
            frmForm.ShowDialog();
        }
        //public void ImportarAsistencia()
        //{
        //    Formularios.FrmImportarAsistencia frmForm = new Formularios.FrmImportarAsistencia();
        //    frmForm.sqlCon = sqlConec;
        //    frmForm.mysConec = mysConec;
        //    frmForm.STU_SISTEMA = STU_SISTEMA;
        //    frmForm.ShowDialog();
        //}
        public void ReporteFormat1(string c_FechaInicio, string c_FechaFinal)
        {
            CN_pla_empleados FunPla = new CN_pla_empleados(STU_SISTEMA);
            //FunPla.mysConec = mysConec;
            FunPla.STU_SISTEMA = STU_SISTEMA;
            FunPla.ReporteFormat1("01/10/2018", "31/10/2018");
        }
        public void ReporteFormat2()
        {
            //Formularios.FrmFormato2 frmForm = new Formularios.FrmFormato2();
            //frmForm.mysConec = mysConec;
            //frmForm.STU_SISTEMA = STU_SISTEMA;
            //frmForm.ShowDialog();
        }
        public void MostrarAsistencia()
        {
            Formularios.FrmMarcacion frmForm = new Formularios.FrmMarcacion();
            frmForm.STU_SISTEMA = STU_SISTEMA;
            frmForm.ShowDialog();
        }
    }
}
