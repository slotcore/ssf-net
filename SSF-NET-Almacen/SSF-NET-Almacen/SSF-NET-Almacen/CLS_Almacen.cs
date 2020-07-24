using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Constantes;
using SIAC_Objetos.Sistema;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using SIAC_Negocio.Almacen;

namespace SSF_NET_Almacen
{
    public class CLS_Almacen
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public MySqlConnection mysConec = new MySqlConnection();
        public OleDbConnection AccConec = new OleDbConnection();
        public int n_DeDonde;                                                            // INDICA DESDE DONDE ESTA SIENDO INVOCADO EL FORMULARIO  (1 = MENU DEL SISTEMA;  2 = OTRO FORMULARIO)
        public int n_TipoExistencia;                                                     // INDICA EL AREA QUE ESTA INGRESANDO EL ITEM, ESTO ES PARA ASIGNAR EL TIPO DE ITEM QUE SE SELEECIONARA AUTOMATICAMENTE  
        public Int64 n_IdItemCreado;                                                      // INDICA EL ID DEL ITEM QUE SE HA CREADO, ESTE DATO SOLO SE DEVOLVERA CUANDO SE HAYA LLAMDO AL FORMULARIO DESDE OTRO FORMULARIO
        public void MantenimientoItems()
        {
            Formularios.FrmAlmacen3 FrmMantenimientoItems = new Formularios.FrmAlmacen3();
            FrmMantenimientoItems.mysConec = mysConec;
            FrmMantenimientoItems.STU_SISTEMA = STU_SISTEMA;
            FrmMantenimientoItems.n_Dedonde = n_DeDonde;
            FrmMantenimientoItems.n_TipoExistencia = n_TipoExistencia;
            FrmMantenimientoItems.ShowDialog();
            if (n_DeDonde == 2)
            { 
                n_IdItemCreado = FrmMantenimientoItems.n_IdItemCreado;
                FrmMantenimientoItems.Close();
            }
        }
        public void IngresoAlmacen()
        {
            Formularios.FrmIngresoAlmacen3 FrmIngreso = new Formularios.FrmIngresoAlmacen3();
            FrmIngreso.mysConec = mysConec;
            FrmIngreso.AccConec = AccConec;
            FrmIngreso.STU_SISTEMA = STU_SISTEMA;
            FrmIngreso.Show();
        }
        public void IngresoAlmacenMP()
        {
            Formularios.FrmIngresoAlmacenMP FrmIngreso = new Formularios.FrmIngresoAlmacenMP();
            FrmIngreso.mysConec = mysConec;
            FrmIngreso.AccConec = AccConec;
            FrmIngreso.STU_SISTEMA = STU_SISTEMA;
            FrmIngreso.Show();
        }
        public void SalidaAlmacen()
        {
            Formularios.FrmSalidaAlmacen3 FrmIngreso = new Formularios.FrmSalidaAlmacen3();
            FrmIngreso.mysConec = mysConec;
            FrmIngreso.AccConec = AccConec;
            FrmIngreso.STU_SISTEMA = STU_SISTEMA;
            FrmIngreso.Show();
        }
        public void SalidaAlmacenVarios()
        {
            Formularios.FrmMovMultAlmacen FrmMovMult = new Formularios.FrmMovMultAlmacen();
            FrmMovMult.mysConec = mysConec;
            FrmMovMult.AccConec = AccConec;
            FrmMovMult.STU_SISTEMA = STU_SISTEMA;
            FrmMovMult.Show();
        }
        public void TransferenciaAlmacenes()
        {
            Formularios.FrmTransferenciaAlmacen FrmTrans = new Formularios.FrmTransferenciaAlmacen();
            FrmTrans.mysConec = mysConec;
            FrmTrans.AccConec = AccConec;
            FrmTrans.STU_SISTEMA = STU_SISTEMA;
            FrmTrans.Show();
        }
        public void MantenimientoAlmacenes()
        {
            Formularios.FrmManAlmacenes FrmForm = new Formularios.FrmManAlmacenes();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoUnidadMedida()
        {
            Formularios.FrmManUniMed FrmForm = new Formularios.FrmManUniMed();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoTipoExistencia()
        {
            Formularios.FrmManTipExi FrmForm = new Formularios.FrmManTipExi();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoFamilia()
        {
            Formularios.FrmManFamilia FrmForm = new Formularios.FrmManFamilia();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoClase()
        {
            Formularios.FrmManClase FrmForm = new Formularios.FrmManClase();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoSubClase()
        {
            Formularios.FrmManSubClase FrmForm = new Formularios.FrmManSubClase();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoPersonal()
        {
            Formularios.FrmManPersonal FrmForm = new Formularios.FrmManPersonal();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void MantenimientoTipOpe()
        {
            Formularios.FrmManTipOpe FrmForm = new Formularios.FrmManTipOpe();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void ReporteMovimientosAlmacen()
        {
            Formularios.FrmRepMovAlm FrmForm = new Formularios.FrmRepMovAlm();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void ReporteItems()
        {
            Formularios.FrmRepItems FrmForm = new Formularios.FrmRepItems();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
        public void SaldosPorLotes()
        {
            CN_alm_movimientos objMov = new CN_alm_movimientos();
            objMov.STU_SISTEMA = STU_SISTEMA;
            objMov.ReportSaldoPorLotes(STU_SISTEMA.EMPRESAID);
        }
        public void SaldosPorLotesDet()
        {
            CN_alm_movimientos objMov = new CN_alm_movimientos();
            objMov.STU_SISTEMA = STU_SISTEMA;
            objMov.ReportSaldoPorLotesDet(STU_SISTEMA.EMPRESAID);
        }
        //public void KardexResumen()
        //{
        //    Formularios.FrmKardexResumen FrmForm = new Formularios.FrmKardexResumen();
        //    FrmForm.mysConec = mysConec;
        //    FrmForm.STU_SISTEMA = STU_SISTEMA;
        //    FrmForm.Show();
        //}
        public void KardexResumen(bool b_EsValorizado)
        {
            Formularios.FrmKardexResumen2 FrmForm = new Formularios.FrmKardexResumen2();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.b_esvalorizado = b_EsValorizado;
            FrmForm.Show();
        }
        public void MantenimientoDocAlmacen()
        {
            Formularios.FrmManAlmDocumentos FrmForm = new Formularios.FrmManAlmDocumentos();
            FrmForm.mysConec = mysConec;
            FrmForm.STU_SISTEMA = STU_SISTEMA;
            FrmForm.Show();
        }
    }
}
