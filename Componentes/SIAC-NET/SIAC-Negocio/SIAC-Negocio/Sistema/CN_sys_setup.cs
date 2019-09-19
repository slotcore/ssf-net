using System;
using MySql.Data.MySqlClient;
using SIAC_DATOS.Sistema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SIAC_Negocio.Sistema
{
    public class CN_sys_setup
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        //public DataTable TraerRegistro(int n_IdEmpresa)
        //{
        //    DataTable dtResult = new DataTable();

        //    CD_sys_setup miFun = new CD_sys_setup();
        //    miFun.mysConec = mysConec;

        //    dtResult = miFun.TraerRegistro(n_IdEmpresa);

        //    if (miFun.IntErrorNumber != 0)
        //    {
        //        booOcurrioError = miFun.booOcurrioError;
        //        StrErrorMensaje = miFun.StrErrorMensaje;
        //        IntErrorNumber = miFun.IntErrorNumber;
        //    }

        //    return dtResult;
        //}
        public DataTable TraerRegistro()
        {
            DataTable dtResult = new DataTable();

            CD_sys_setup miFun = new CD_sys_setup();
            miFun.mysConec = mysConec;

            dtResult = miFun.TraerRegistro();

            if (miFun.IntErrorNumber != 0)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResult;
        }
    }
}
