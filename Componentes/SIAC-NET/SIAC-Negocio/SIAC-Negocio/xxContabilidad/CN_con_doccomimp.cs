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
using SIAC_Entidades.Cooperativa;

namespace SIAC_Negocio.Contabilidad
{
    public class CN_con_doccomimp
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable Listar()
        {
            DataTable dtResul = new DataTable();

            CD_con_doccomimp miFun = new CD_con_doccomimp();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar();

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
