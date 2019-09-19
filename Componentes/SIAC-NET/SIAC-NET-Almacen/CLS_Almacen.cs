using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Constantes;
using SIAC_Objetos.Sistema;
using MySql.Data.MySqlClient;

namespace SIAC_NET_Almacen
{
    public class CLS_Almacen
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public MySqlConnection mysConec = new MySqlConnection();

        public void MantenimientoItems()
        {
            Formularios.FrmAlmacen3 FrmMantenimientoItems = new Formularios.FrmAlmacen3();
            FrmMantenimientoItems.mysConec = mysConec;
            FrmMantenimientoItems.STU_SISTEMA = STU_SISTEMA;
            FrmMantenimientoItems.ShowDialog();
        }
        public void IngresoAlmacen()
        {
            Formularios.FrmIngresoAlmacen3 FrmIngreso = new Formularios.FrmIngresoAlmacen3();
            FrmIngreso.mysConec = mysConec;
            FrmIngreso.STU_SISTEMA = STU_SISTEMA;
            FrmIngreso.ShowDialog();
        }
        public void SalidaAlmacen()
        {
            Formularios.FrmSalidaAlmacen3 FrmIngreso = new Formularios.FrmSalidaAlmacen3();
            FrmIngreso.mysConec = mysConec;
            FrmIngreso.STU_SISTEMA = STU_SISTEMA;
            FrmIngreso.ShowDialog();
        }
    }
}
