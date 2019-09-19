using MySql.Data.MySqlClient;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Negocio.Sistema
{
    public class CN_sys_docnum
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        
        public string HallaNumeroDocumento(int intIdEmp, int intIdTipDoc, string strNumeroSerie, int intGrabarNumero)
        {
            string strNumeroDocumento;
            CD_sys_docnum miFun = new CD_sys_docnum();
            miFun.mysConec = mysConec;

            strNumeroDocumento = miFun.HallaNumeroDocumento(intIdEmp, intIdTipDoc, strNumeroSerie, intGrabarNumero);

            if (strNumeroDocumento == "")
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return strNumeroDocumento;
        }
    }
}
