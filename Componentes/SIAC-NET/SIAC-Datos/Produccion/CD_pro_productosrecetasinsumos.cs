using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Produccion;

namespace SIAC_DATOS.Produccion
{
    public class CD_pro_productosrecetasinsumos
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;

        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtInsumos = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        public bool Listar(Int64 n_idProducto)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idpro", "System.INT64", n_idProducto.ToString()}
                                      };
            dtInsumos = xMiFuncion.StoreDTLLenar("pro_productosrecetasinsumos_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool ListarTodo(int n_IdEmpresa)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()}
                                      };
            dtInsumos = xMiFuncion.StoreDTLLenar("pro_productosrecetasinsumos_listartodo", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
    }
}
