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
    public class CD_TEMPUS_mostrar
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtRevision = new DataTable();
        public DataTable dtMovimientoDet = new DataTable();
        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        public bool Eliminar(string c_codempresa)
        {
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"c_idemp", "System.STRING", c_codempresa.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("tempus_mostrar_delete", arrParametros, mysConec);

            if (booResult == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booResult;
        }
        public bool Insertar(BE_TEMPUS_MOSTRAR entCabecera)
        {
            bool booResult = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();

            try
            {
                booResult = xMiFuncion.StoreEjecutar("tempus_mostrar_insertar", entCabecera, mysConec, 1);

                if (booResult == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booResult;
                }

                trans.Commit();
                return booResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return booResult;
            }
        }
    }
}
