using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Almacen;

namespace SIAC_DATOS.Almacen
{
    public class CD_alm_inventariolotes
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        public bool Insertar(BE_ALM_INVENTARIOLOTE entLotes)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("alm_inventariolotes_insertar", entLotes, mysConec, 1) == true)
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
        public DataTable TraerLotesConSaldo(int n_idemp, int n_iditem)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT64",n_idemp.ToString()},
                                            {"n_idite", "System.INT16",n_iditem.ToString()}                              
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_inventariolotes_traerlotesconsaldo", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable TraerLotesConSaldo(int n_idemp)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_idemp.ToString()}                              
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_inventariolotes_traerlotesconsaldo2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable TraerLotesItem(int n_idemp, int n_iditem, string c_FechaIngreso)
        { 
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT64", n_idemp.ToString()},
                                            {"n_idite", "System.INT16", n_iditem.ToString()},
                                            {"c_fching", "System.STRING", c_FechaIngreso.ToString()}                              
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_inventariolotes_listaritem", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
    }
}
