using System;
using System.Data;
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
using SIAC_Negocio.Produccion;
using SIAC_Entidades.Produccion;
using SSF_NET_Produccion.Formularios;

namespace SSF_NET_Produccion
{
    class Cls_Funciones
    {
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable dtTipExi = new DataTable();
        public DataTable dtItems = new DataTable();
        public DataTable dtUniMedSunat = new DataTable();

        public void VerReceta(int n_IdProducto, int n_IdReceta)
        {
            int n_row = 0;
            int n_index = 0;
            CN_pro_productos objPro = new CN_pro_productos();
            List<BE_PRO_PRODUCTOSRECETAS> lstRecetas = new List<BE_PRO_PRODUCTOSRECETAS>();
            List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstRecetasIns = new List<BE_PRO_PRODUCTOSRECETASINSUMOS>();

            objPro.mysConec = mysConec;
            if (objPro.TraerRegistro(n_IdProducto) == true)
            {
                lstRecetas = objPro.lstRecetas;
                lstRecetasIns = objPro.lstRecetasIns;
            }

            for (n_row = 0; n_row <= lstRecetas.Count - 1; n_row++)
            {
                if (lstRecetas[n_row].n_id == n_IdReceta)
                {
                    n_index = n_row;
                    break;
                }
            }

            // FILTRAMOS SOLO LOS INSUMOS DE LA RECETA  ACTUAL
            List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstTemp = new List<BE_PRO_PRODUCTOSRECETASINSUMOS>();
            BE_PRO_PRODUCTOSRECETASINSUMOS entTemp = new BE_PRO_PRODUCTOSRECETASINSUMOS();

            for (n_row = 0; n_row <= lstRecetasIns.Count - 1; n_row++)
            {
                if (lstRecetasIns[n_row].n_idrec == n_IdReceta)
                {
                    entTemp = lstRecetasIns[n_row];

                    lstTemp.Add(entTemp);
                }
            }

            FrmVerRecetaInsumos MyForm = new FrmVerRecetaInsumos();
            MyForm.dtTipExi = dtTipExi;
            MyForm.dtItems = dtItems;
            MyForm.dtUniMed = dtUniMedSunat;
            MyForm.entReceta = lstRecetas[n_index];
            MyForm.lstRecetasIns = lstTemp;
            MyForm.ShowDialog();
        }
    }
}
