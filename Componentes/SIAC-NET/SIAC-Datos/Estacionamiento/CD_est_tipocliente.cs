using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Estacionamiento;

namespace SIAC_DATOS.Estacionamiento
{
    public class CD_est_tipocliente
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public DataTable dtListar = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public void Listar()
        {
            string[,] arrParametros = new string[0, 3] {
                                      };

            dtListar = xMiFuncion.StoreDTLLenar("est_tipocliente_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
    }
}
