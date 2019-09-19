using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.OleDb;

namespace Helper
{
    public class DatosAcces
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;

        public OleDbConnection AbrirConeccion(string c_CadenaConeccion)
        {
            OleDbConnection xConeccion = new OleDbConnection();

            try 
            { 
                xConeccion.ConnectionString = c_CadenaConeccion;
                xConeccion.Open();

                return xConeccion;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS LA CONECCION CERRADA
                booOcurrioError = true;
                StrErrorMensaje = exc.Message;
                IntErrorNumber = exc.HResult;
                return xConeccion;
            }
        }

        public DataTable DtLLenar(String c_Sql, OleDbConnection xConeccion)
        {
            DataTable dtResult = new DataTable();
            OleDbDataAdapter objDa = new OleDbDataAdapter(c_Sql, xConeccion);

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

        //public Boolean EjecutarSQL(string c_Sql, OleDbConnection xConeccion, OleDbTransaction ObjTransaccion)
        //{
        //    OleDbCommand xCmd = new OleDbCommand(c_Sql, xConeccion);

        //    xCmd.Transaction = ObjTransaccion;
        //    xCmd.ExecuteNonQuery();

        //    return true;
        //}
        public Boolean EjecutarSQL(string c_Sql, OleDbConnection xConeccion)
        {
            OleDbCommand xCmd = new OleDbCommand(c_Sql, xConeccion);
            bool booResult = false;

            try 
            { 
                xCmd.ExecuteNonQuery();
                booResult = true;
                return booResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = true;
                StrErrorMensaje = exc.Message;
                IntErrorNumber = exc.HResult;
                return booResult;
            }
        }
        public double HallaCodigoTabla(string c_Tabla, string c_CampoId, OleDbConnection xConeccion)
        {
            DataTable DtResult = new DataTable();
            string c_Sql;
            double n_Valor;
            Comunes.Funciones objFun = new Comunes.Funciones();

            c_Sql = "SELECT MAX(" + c_CampoId + ") AS miid FROM " + c_Tabla + "";
            DtResult = DtLLenar(c_Sql, xConeccion);

            if (DtResult.Rows.Count == 1)
            {
                n_Valor = Convert.ToDouble(objFun.NulosN(Convert.ToDouble(DtResult.Rows[0]["miid"].ToString()))) + 1;
            }
            else
            {
                n_Valor = 1;
            }
            return n_Valor;
        }
    }
}
