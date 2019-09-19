using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Constantes;
using SIAC_Objetos.Sistema;
using MySql.Data.MySqlClient;

namespace SSF_NET_Logistica
{
    public class CLS_Logistica
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public MySqlConnection mysConec = new MySqlConnection();
        public int n_IdLibro;
        public void MantenimientoGuias()
        {
            Formularios.FrmManGuiasRemision FrmForm = new Formularios.FrmManGuiasRemision();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoOR()
        {
            Formularios.FrmManOrdenRequerimiento FrmForm = new Formularios.FrmManOrdenRequerimiento();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoCompras()
        {
            Formularios.FrmManCompras2 FrmForm = new Formularios.FrmManCompras2();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoLiquidacionCompra()
        {
            Formularios.FrmManLiquidacionCompra FrmForm = new Formularios.FrmManLiquidacionCompra();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoImportacion()
        {
            Formularios.FrmManComprasImportacion FrmForm = new Formularios.FrmManComprasImportacion();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoRenta4ta()
        {
            Formularios.FrmManRenta4ta FrmForm = new Formularios.FrmManRenta4ta();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoNCND(int n_TipoNota)
        {
            // 1 = NOTA DE CREDITO;   2 = NOTA DE DEBITO
            Formularios.FrmManNCND FrmForm = new Formularios.FrmManNCND();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.n_TipoNota = n_TipoNota;             // INDICAMOS QUE TIPO DE NOTA MOSTRARA
            FrmForm.Show();
        }
         public void ConsultaCompras()
        {
            Formularios.FrmConsultaCompras FrmForm = new Formularios.FrmConsultaCompras();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.n_Libro = n_IdLibro;
            FrmForm.Show();
        }
         public void MantenimientoOC()
         {
             Formularios.FrmOrdenCompra FrmForm = new Formularios.FrmOrdenCompra();
             FrmForm.mysConec = mysConec;
             FrmForm.STU_SISTEMA = STU_SISTEMA;
             FrmForm.Show();
         }
         public void MantenimientoMotivos()
         {
             Formularios.FrmManMotivos FrmForm = new Formularios.FrmManMotivos();
             FrmForm.mysConec = mysConec;
             FrmForm.STU_SISTEMA = STU_SISTEMA;
             FrmForm.Show();
         }
    }
}
