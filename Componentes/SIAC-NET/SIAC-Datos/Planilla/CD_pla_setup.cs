using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Planillas;

namespace SIAC_DATOS.Planilla
{
    public class CD_pla_setup
    {
        public DataTable dtRegistro;

        public bool b_OcurrioError;
        public string c_ErrorMensaje;
        public int n_ErrorNumber;

        public MySqlConnection mysConec;

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public DataTable dtLista = new DataTable();       

        public bool Listar(int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()},
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("pla_setup_select", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        
    }
}
