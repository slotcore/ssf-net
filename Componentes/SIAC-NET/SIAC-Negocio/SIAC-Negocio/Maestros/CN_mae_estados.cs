using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Sunat;
using SIAC_DATOS.Maestros;
using SIAC_DATOS.Sunat;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Maestros
{
    public class CN_mae_estados
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar(int n_IdFormulario)
        {
            DataTable dtResul = new DataTable();

            CD_mae_estados miFun = new CD_mae_estados();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdFormulario);

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
