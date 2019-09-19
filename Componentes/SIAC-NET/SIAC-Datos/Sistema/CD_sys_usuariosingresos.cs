using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
using MySql.Data.MySqlClient;
using System.Data;
using SIAC_Entidades.Sistema;

namespace SIAC_DATOS.Sistema
{
    public class CD_sys_usuariosingresos
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public bool Insertar(BE_SYS_USUARIOSINGRESOS ent_Ingresos)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("sys_usuariosingreso_insertar", ent_Ingresos, mysConec, 0) == true)
            {
                booOk = true;
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
    }
}
