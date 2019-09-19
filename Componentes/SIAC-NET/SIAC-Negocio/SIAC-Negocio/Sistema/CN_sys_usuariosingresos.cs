using System;
using MySql.Data.MySqlClient;
using SIAC_DATOS.Sistema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SIAC_Entidades.Sistema;

namespace SIAC_Negocio.Sistema
{
    public class CN_sys_usuariosingresos
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public bool Insertar(BE_SYS_USUARIOSINGRESOS ent_Ingresos)
        {
            DataTable dtResult = new DataTable();
            bool b_result = false;

            CD_sys_usuariosingresos miFun = new CD_sys_usuariosingresos();
            miFun.mysConec = mysConec;

            if (miFun.Insertar(ent_Ingresos) == true) 
            {
                b_result = true;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            b_result = true;
            return b_result;
        }
    }
}
