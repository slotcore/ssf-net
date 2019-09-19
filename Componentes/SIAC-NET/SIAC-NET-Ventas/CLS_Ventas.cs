using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Constantes;
using SIAC_Objetos.Ventas;
using SIAC_Objetos.Sistema;
using MySql.Data.MySqlClient;

namespace SIAC_NET_Ventas
{
    public class CLS_Ventas
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public MySqlConnection mysConec = new MySqlConnection();

        public void PuntoVenta()   
        {
            SIAC_NET_Ventas.Formularios.Form3 frmPuntoVenta = new SIAC_NET_Ventas.Formularios.Form3();
            frmPuntoVenta.mysConec = mysConec;
            frmPuntoVenta.STU_SISTEMA = STU_SISTEMA;
            frmPuntoVenta.ShowDialog();
        }
    }
}
