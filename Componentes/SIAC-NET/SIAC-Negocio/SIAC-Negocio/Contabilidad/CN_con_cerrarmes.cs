using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Contabilidad;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using Helper;

namespace SIAC_Negocio.Contabilidad
{
    public class CN_con_cerrarmes
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public BE_CON_PERCEPCION e_Percepcion = new BE_CON_PERCEPCION();
        public void Listar(int n_IdEmpresa)
        {
            CD_con_cerrarmes miFun = new CD_con_cerrarmes();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa);
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return;
        }
        public bool Actualizar(List<BE_CON_CERRARMES> l_CerrarMes)
        {
            bool b_result = false;
            CD_con_cerrarmes miFun = new CD_con_cerrarmes();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(l_CerrarMes);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
    }
}
