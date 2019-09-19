using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Estacionamiento;
using SIAC_Entidades.Maestros;

namespace SIAC_DATOS.Estacionamiento
{
    public class CD_est_conecta
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;

        public MySqlConnection mysConec = new MySqlConnection();
        
        public CD_est_conecta(string c_IPServidor, string c_NombreBD, string c_Usuario, string c_Passord, string c_Puerto)
        {
            MySqlConnection mysConectar = new MySqlConnection();
            Helper.DatosMySql hlpFuncion = new Helper.DatosMySql();

            do 
            {
                mysConec = hlpFuncion.ObtenerConexion(c_IPServidor, c_NombreBD, c_Usuario, c_Passord, c_Puerto);
            } while (mysConec == null);
                        

            if (hlpFuncion.booOcurrioError == true)
            {
                b_OcurrioError = hlpFuncion.booOcurrioError;
                c_ErrorMensaje = hlpFuncion.StrErrorMensaje;
                n_ErrorNumber = hlpFuncion.IntErrorNumber;
            }
        }
        ~CD_est_conecta()
        {
            if (mysConec.State == System.Data.ConnectionState.Closed)
            { 
                mysConec.Close();
            }
            mysConec = null;
        }
    }
}
