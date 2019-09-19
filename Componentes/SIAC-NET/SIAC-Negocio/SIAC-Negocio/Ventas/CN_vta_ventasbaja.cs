using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Ventas;
using SIAC_DATOS.Ventas;
using MySql.Data.MySqlClient;
namespace SIAC_Negocio.Ventas
{
    public class CN_vta_ventasbaja
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();

        public string Correlativo(int n_IdEmpresa)
        {
            DataTable dtLista = new DataTable();
            CD_vta_ventasbaja miFun = new CD_vta_ventasbaja();
            string c_corr = "";
            bool b_result = false;

            miFun.mysConec = mysConec;
            b_result = miFun.Correlativo(n_IdEmpresa);
            if (b_result == true)
            {
                dtLista = miFun.dtLista;
                c_corr = dtLista.Rows[0]["c_newnumero"].ToString();
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return c_corr;
        }
        public bool BuscarArchivo(int n_IdEmpresa, string c_NombreArchivo)
        {
            CD_vta_ventasbaja miFun = new CD_vta_ventasbaja();
            bool b_result = false;

            miFun.mysConec = mysConec;
            b_result = miFun.BuscarArchivo(n_IdEmpresa, c_NombreArchivo);
            if (b_result == true)
            {
                dtLista = miFun.dtLista;
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return b_result;
        }
        public bool Insertar(BE_VTA_VENTASBAJA e_VentaBaja)
        {
            CD_vta_ventasbaja miFun = new CD_vta_ventasbaja();
            bool b_result = false;

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(e_VentaBaja);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return b_result;
        }
    }
}
