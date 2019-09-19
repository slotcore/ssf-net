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
    public class CN_sys_formulario
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable TraerRegistro(int intIdFormulario)
        {
            DataTable dtResult = new DataTable();
            CD_sys_formulario miFun = new CD_sys_formulario();

            miFun.mysConec = mysConec;
            dtResult = miFun.TraerRegistro(intIdFormulario);

            return dtResult;
        }
    }
}
