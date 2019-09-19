using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Planillas;

namespace SIAC_DATOS.Planilla
{
    public class CD_TEMPUS_MARCACIONES
    {
        public DataTable dtLista;
        //private DataTable dtRegistro;

        public bool b_OcurrioError;
        public string c_ErrorMensaje;
        public int n_ErrorNumber;

        public SqlConnection sqlConec = new SqlConnection();

        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        Helper.DatosSQL xFunSQl = new Helper.DatosSQL();

        public void TraerMarcacion(string c_FechaInicio, string c_FechaFinal)
        {
            DataTable dtResut = new DataTable();
            string c_Sql = "";

            c_Sql = "SELECT 0 As n_idmar, CONVERT(char(10), TEMPUS.MARCACIONES.FECHA, 120) AS d_fchmar, TEMPUS.MARCACIONES.NUMERO_TARJETA AS c_codemp, TEMPUS.MARCACIONES.HORATXT AS c_hormar" +
                " FROM TEMPUS.MARCACIONES " +
                " WHERE ((CONVERT(smalldatetime, CONVERT(char(10), FECHA, 103), 103) >= '" + c_FechaInicio + "') And " +
                " (       CONVERT(smalldatetime, CONVERT(char(10), FECHA, 103), 103) <= '" + c_FechaFinal + "')) " +
                " ORDER BY TEMPUS.MARCACIONES.FECHA, TEMPUS.MARCACIONES.NUMERO_TARJETA, TEMPUS.MARCACIONES.HORATXT";

            dtResut = xFunSQl.DtLLenar(c_Sql, sqlConec);

            if (xFunSQl.IntErrorNumber == 0)
            {
                dtLista = dtResut;   
            }
            else
            {
                b_OcurrioError = xFunSQl.booOcurrioError;
                c_ErrorMensaje = xFunSQl.StrErrorMensaje;
                n_ErrorNumber = xFunSQl.IntErrorNumber;
            }
        }
    }
}
