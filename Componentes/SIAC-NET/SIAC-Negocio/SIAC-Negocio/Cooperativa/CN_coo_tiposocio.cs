using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Cooperativa;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Cooperativa;

namespace SIAC_Negocio.Cooperativa
{
    public class CN_coo_tiposocio
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_coo_tiposocio miFun = new CD_coo_tiposocio();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmpresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
    }
}
