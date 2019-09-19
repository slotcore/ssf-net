using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Sunat
{
    public class CD_sun_fe_catalogo07
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;

        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public bool Listar()
        {
            bool dtResul = false;
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"", "",""}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("sun_fe_catalogo07_listar", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            dtResul = true;
            return dtResul;
        }
    }
}
