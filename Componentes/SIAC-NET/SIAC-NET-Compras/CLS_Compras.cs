using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Constantes;
using SIAC_Objetos.Sistema;
using MySql.Data.MySqlClient;


namespace SIAC_NET_Compras
{
    public class CLS_Compras
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public MySqlConnection mysConec = new MySqlConnection();

        public void MantenimientoCompras()
        {
            Formularios.FrmManCompras FrmMantenimientoItems = new Formularios.FrmManCompras();
            FrmMantenimientoItems.mysConec = mysConec;
            FrmMantenimientoItems.STU_SISTEMA = STU_SISTEMA;
            FrmMantenimientoItems.ShowDialog();
        }
    }
}
