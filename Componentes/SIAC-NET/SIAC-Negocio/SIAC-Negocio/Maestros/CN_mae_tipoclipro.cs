using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Maestros;
using SIAC_DATOS.Maestros;
using MySql.Data.MySqlClient;


namespace SIAC_Negocio.Maestros
{
    public class CN_mae_tipoclipro
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar()
        {
            DataTable dtResul = new DataTable();

            CD_mae_tipoclipro miFun = new CD_mae_tipoclipro();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar();

            if (dtResul == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return dtResul;
        }
    }
}
