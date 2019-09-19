using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Produccion;
using SIAC_Objetos.Constantes;
using SIAC_Objetos.Sistema;
using SIAC_Entidades.Produccion;
using MySql.Data.MySqlClient;
using SIAC_Negocio.Almacen;

namespace SSF_NET_Produccion
{
    public class CLS_Recetas
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable dtTipExi;
        public DataTable dtItems;
        public DataTable dtUniMed;
        public BE_PRO_PRODUCTOSRECETAS e_Receta;
        public List<BE_PRO_PRODUCTOSRECETASINSUMOS> l_RecetaIns;
        public void VerRecetar()
        {
            Formularios.FrmVerRecetaInsumos MyForm = new Formularios.FrmVerRecetaInsumos();
            MyForm.dtTipExi = dtTipExi;
            MyForm.dtItems = dtItems;
            MyForm.dtUniMed = dtUniMed;
            MyForm.entReceta = e_Receta;
            MyForm.lstRecetasIns = l_RecetaIns;
            MyForm.ShowDialog();
            MyForm.Close();
            MyForm = null;
        }
    }
}
