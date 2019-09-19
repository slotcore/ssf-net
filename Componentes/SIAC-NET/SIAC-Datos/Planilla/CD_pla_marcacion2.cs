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
    public class CD_pla_marcacion2
    {
        public bool b_OcurrioError;
        public string c_ErrorMensaje;
        public int n_ErrorNumber;

        private MySqlConnection mysConec;

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public CD_pla_marcacion2(string c_IPServidor, string c_NombreBD, string c_Usuario, string c_Passord, string c_Puerto)
        {
            MySqlConnection mysConectar = new MySqlConnection();
            Helper.DatosMySql hlpFuncion = new Helper.DatosMySql();

            mysConec = hlpFuncion.ObtenerConexion(c_IPServidor, c_NombreBD, c_Usuario, c_Passord, c_Puerto);

            if (hlpFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        ~CD_pla_marcacion2()
        {
            mysConec.Close();
            mysConec = null; 
        }
        public bool Insertar(List<BE_PLA_MARCACION2> l_Marcacion, string c_FechaInicio, string c_FechaFinal)
        {
            bool booOk = false;
            int n_row = 0;
            MySqlTransaction trans = null;

            try
            {
                trans = mysConec.BeginTransaction();

                for (n_row = 0; n_row <= l_Marcacion.Count - 1; n_row++)
                {
                    if (xMiFuncion.StoreEjecutar("pla_marcacion2_insertar", l_Marcacion[n_row], mysConec, 0) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }
                }
                booOk = true;
                trans.Commit();
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = true;
                c_ErrorMensaje = exc.Message.ToString();
                n_ErrorNumber = exc.HResult;
                trans.Rollback();
                return booOk;
            }
            return booOk;
        }
    }
}
