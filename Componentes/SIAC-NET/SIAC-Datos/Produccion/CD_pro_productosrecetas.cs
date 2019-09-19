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
    public class CD_pro_productosrecetas
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;

        public DataTable dtRecetas = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public bool Listar(Int64 n_IdRegistro)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idpro", "System.INT64", n_IdRegistro.ToString()}
                                      };
            dtRecetas = xMiFuncion.StoreDTLLenar("pro_productosrecetas_listar", arrParametros, mysConec);

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
        public bool ListarTodasRecetas(int n_IdEmpresa)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()}
                                      };
            dtRecetas = xMiFuncion.StoreDTLLenar("pro_productosrecetas_listartodas", arrParametros, mysConec);

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
        public void Consulta1(string c_ListaId)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"c_iditem", "System.STRING", c_ListaId.ToString()}
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("pro_productosrecetas_consulta2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void Consulta3()
        {
            string[,] arrParametros = new string[0, 3] {
                                            
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("pro_productosrecetas_consulta3", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        

    }
}
