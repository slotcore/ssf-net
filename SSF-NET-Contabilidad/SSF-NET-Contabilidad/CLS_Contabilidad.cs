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

namespace SSF_NET_Contabilidad
{
    public class CLS_Contabilidad
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public MySqlConnection mysConec = new MySqlConnection();
        public void MantenimientoTC()
        {
            Formularios.FrmManTC Frm = new Formularios.FrmManTC();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void MantenimientoPC()
        {
            Formularios.FrmManPC Frm = new Formularios.FrmManPC();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void MantenimientoImpuestos()
        {
            Formularios.FrmManImpuestos Frm = new Formularios.FrmManImpuestos();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void MantenimientoDetraccion()
        {
            Formularios.FrmMandetraccion Frm = new Formularios.FrmMandetraccion();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void MantenimientoRetencion()
        {
            Formularios.FrmManRetencion Frm = new Formularios.FrmManRetencion();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void MantenimientoPercepcion()
        {
            Formularios.FrmManPercepcion Frm = new Formularios.FrmManPercepcion();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void MantenimientoDocCom()
        {
            Formularios.FrmManTipDocCom Frm = new Formularios.FrmManTipDocCom();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void MantenimientoLibrosCon()
        {
            Formularios.FrmManTipDocCom Frm = new Formularios.FrmManTipDocCom();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void ProAsientosDiversos()
        {
            Formularios.FrmManAsientosDiv Frm = new Formularios.FrmManAsientosDiv();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void RegistroCompras(int n_IdLibro)
        {
            Formularios.FrmRegCompras Frm = new Formularios.FrmRegCompras();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.n_Libro = n_IdLibro;
            Frm.Show();
        }
        public void RegistroDetracciones(int n_TipoMovimiento)
        {
            Formularios.FrmRegDetraCompras Frm = new Formularios.FrmRegDetraCompras();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.n_Tipo = n_TipoMovimiento;
            Frm.Show();
        }
        public void RegistroPercepciones(int n_TipoRegistro)
        {
            Formularios.FrmregPercepciones Frm = new Formularios.FrmregPercepciones();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.n_TipoRegistro = n_TipoRegistro;
            Frm.Show();
        }
        public void Registroretenciones()
        {
            Formularios.FrmRegRetenciones Frm = new Formularios.FrmRegRetenciones();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void CostoProduccion()
        {
            Formularios.FrmCostoProduccion Frm = new Formularios.FrmCostoProduccion();
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void ConfiguracionValorizacion()
        {
            Formularios.FrmConfigVal Frm = new Formularios.FrmConfigVal();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void InventarioInicial()
        {
            Formularios.FrmInventarioIni Frm = new Formularios.FrmInventarioIni();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void VerAsiento(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, string c_NumeroAsiento)
        {
            Formularios.FrmVerAsiento Frm = new Formularios.FrmVerAsiento();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.n_IdEmpresa = n_IdEmpresa;
            Frm.n_AnoTrabajo = n_AnoTrabajo;
            Frm.n_MesTrabajo = n_MesTrabajo;
            Frm.n_IdLibro = n_IdLibro;
            Frm.c_NumAsiento = c_NumeroAsiento;
            Frm.Show();
        }
        public void CerrarMes()
        {
            Formularios.FrmCerrarModulo Frm = new Formularios.FrmCerrarModulo();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void Diario()
        {
            Formularios.FrmLibroDiario Frm = new Formularios.FrmLibroDiario();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            //Frm.n_Libro = n_IdLibro;
            Frm.Show();
        }
        public void Mayor()
        {
            Formularios.FrmLibroMayor Frm = new Formularios.FrmLibroMayor();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void AsignarCtaConItems()
        {
            Formularios.FrmAsigCtaConItems Frm = new Formularios.FrmAsigCtaConItems();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void BalanceComprobacion()
        {
            Formularios.FrmBalanceComprobacion Frm = new Formularios.FrmBalanceComprobacion();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void MantenimientoCtaConDoc()
        {
            Formularios.FrmManAsiCtaConDoc Frm = new Formularios.FrmManAsiCtaConDoc();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
        public void MantenimientoImpuestosDocumentos()
        {
            Formularios.FrmManImpuestosDocumentos Frm = new Formularios.FrmManImpuestosDocumentos();
            Frm.mysConec = mysConec;
            Frm.STU_SISTEMA = STU_SISTEMA;
            Frm.Show();
        }
    }
}
