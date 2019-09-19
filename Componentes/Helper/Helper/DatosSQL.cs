using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Helper
{
    public class DatosSQL
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;

        public SqlConnection AbrirConeccion(string c_NomServidor, string c_BaseDatos, string c_Usuario, string c_Password)
        {
            SqlConnection Coneccion = new SqlConnection("Server=" + c_NomServidor + ";uid=" + c_Usuario + ";pwd=" + c_Password + ";database=" + c_BaseDatos + "");
            
            try
            {
                Coneccion.Open();
            }
            catch (Exception ex)
            {
                booOcurrioError = true;                  // INDICA QUE EXISTE ERROR
                IntErrorNumber = ex.HResult;             // INDICA EL NUMERO DE ERROR
                StrErrorMensaje = ex.Message;            // ENVIA EM MENSAJE DE ERROR
                return null;
            }
            return Coneccion;
        }
        public SqlConnection AbrirConeccion(string c_CadConeccion)
        {
            SqlConnection Coneccion = new SqlConnection(c_CadConeccion);

            try
            {
                Coneccion.Open();
            }
            catch (Exception ex)
            {
                booOcurrioError = true;                  // INDICA QUE EXISTE ERROR
                IntErrorNumber = ex.HResult;             // INDICA EL NUMERO DE ERROR
                StrErrorMensaje = ex.Message;            // ENVIA EM MENSAJE DE ERROR
                return null;
            }
            return Coneccion;
        }
        public DataTable DtLLenar(String c_Sql, SqlConnection xConeccion)
        {
            DataTable dtResult = new DataTable();

            SqlDataAdapter objDa = new SqlDataAdapter(c_Sql, xConeccion);

            try
            {
                objDa.Fill(dtResult);
                return dtResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = true;
                StrErrorMensaje = exc.Message;
                IntErrorNumber = exc.HResult;
                return dtResult;
            }
        }
    }
}
