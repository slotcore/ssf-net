using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Maestros;
using SIAC_DATOS.Sistema;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Sistema
{
    public class CN_sys_personaladjunto
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_sys_personaladjunto miFun = new CD_sys_personaladjunto();
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
