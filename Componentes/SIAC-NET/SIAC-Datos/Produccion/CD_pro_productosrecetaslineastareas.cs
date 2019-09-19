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
    public class CD_pro_productosrecetaslineastareas
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;

        public DataTable dtLineasTar = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        public bool Listar(Int64 n_IdProducto)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idpro", "System.INT64", n_IdProducto.ToString()}
                                      };
            dtLineasTar = xMiFuncion.StoreDTLLenar("pro_productosrecetaslineastareas_listar", arrParametros, mysConec);

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
        public bool ListarTodasTareas(Int64 n_IdEmpresa)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()}
                                      };
            dtLineasTar = xMiFuncion.StoreDTLLenar("pro_productosrecetaslineastareas_listartareas", arrParametros, mysConec);

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
        
        public bool Consulta1(int n_IdProducto, int n_IdReceta, int n_idLinea, string c_Condicion)
        { 
            bool b_result = false;

            string[,] arrParametros = new string[4, 3] {
                                            {"n_idpro", "System.INT32", n_IdProducto.ToString()},
                                            {"n_idrec", "System.INT32", n_IdReceta.ToString()},
                                            {"n_idlin", "System.INT32", n_idLinea.ToString()},
                                            {"c_cond", "System.STRING", c_Condicion.ToString()}
                                      };
            dtLineasTar = xMiFuncion.StoreDTLLenar("pro_productosrecetaslineastareas_consulta1", arrParametros, mysConec);

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
