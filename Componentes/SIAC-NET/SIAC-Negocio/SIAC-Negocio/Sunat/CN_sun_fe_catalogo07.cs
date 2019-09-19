using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Sunat;
using SIAC_DATOS.Sunat;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Sunat
{
    public class CN_sun_fe_catalogo07
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public bool Listar()
        {
            bool booResul = false;
            CD_sun_fe_catalogo07 miFun = new CD_sun_fe_catalogo07();
            miFun.mysConec = mysConec;

            if (miFun.Listar() == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                booResul = true;
                dtLista = miFun.dtLista;
            }

            return booResul;
        }
    }
}
