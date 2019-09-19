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
    public class CD_pro_lineas2017
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        
        public DataTable dtProductos = new DataTable();
        public DataTable dtLineas = new DataTable();
        public DataTable dtCab = new DataTable();
        public DataTable dtDet = new DataTable();

        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        public bool ListarProductosLinea(int n_IdEmpresa)
        {
            bool booResult = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()}
                                      };
            dtProductos = xMiFuncion.StoreDTLLenar("pro_linea2017_productos_con_lineas", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber == 0)
            {
                booResult = true;
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;
        }
        public bool ListarLineas(int n_IdEmpresa)
        {
            bool booResult = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()}
                                      };
            dtLineas = xMiFuncion.StoreDTLLenar("pro_linea2017_lineas", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber == 0)
            {
                booResult = true;
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;
        }
        public bool TraerRegistro(int n_IdLinea)
        {
            bool booResult = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32",n_IdLinea.ToString()}
                                      };
            xMiFuncion.IntErrorNumber = 0;
            dtCab = xMiFuncion.StoreDTLLenar("pro_linea2017_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber == 0)
            {
                string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idlin", "System.INT32",n_IdLinea.ToString()}
                                      };
                xMiFuncion.IntErrorNumber = 0;
                dtDet = xMiFuncion.StoreDTLLenar("pro_lineadet2017_listar", arrParametros2, mysConec);
                if (xMiFuncion.IntErrorNumber == 0)
                {
                    booResult = true;
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                }
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;
        }
    }
}
