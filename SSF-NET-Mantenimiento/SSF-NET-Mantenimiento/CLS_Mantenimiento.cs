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

namespace SSF_NET_Mantenimiento
{
    public class CLS_Mantenimiento
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public MySqlConnection mysConec = new MySqlConnection();
        public OleDbConnection AccConec = new OleDbConnection();
        public void MantenimientoEquipos()
        {
            Formularios.FrmManEquipos FrmMan = new Formularios.FrmManEquipos();
            FrmMan.mysConec = mysConec;
            FrmMan.STU_SISTEMA = STU_SISTEMA;
            FrmMan.ShowDialog();
        }
    }
}
