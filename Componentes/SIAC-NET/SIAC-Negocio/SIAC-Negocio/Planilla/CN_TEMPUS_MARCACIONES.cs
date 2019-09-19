using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Planillas;
using SIAC_DATOS.Planilla;
using MySql.Data.MySqlClient;
using Helper;

namespace SIAC_Negocio.Planilla
{
    public class CN_TEMPUS_MARCACIONES
    {
        public DataTable dtLista;
        private DataTable _dtRegistro;

        //private BE_PLA_EMPLEADOS _entEmpleado;

        public bool b_OcurrioError;
        public string c_ErrorMensaje;
        public int n_ErrorNumber;

        public SqlConnection sqlConec = new SqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        Helper.Comunes.Funciones xFunciones = new Helper.Comunes.Funciones();
        Helper.DatosSQL xFunSql = new Helper.DatosSQL();

        public void TraerMarcacion(string c_FechaInicio, string c_FechaFinal)
        {
            CD_TEMPUS_MARCACIONES objMar = new CD_TEMPUS_MARCACIONES();
            objMar.sqlConec = sqlConec;
            objMar.TraerMarcacion(c_FechaInicio, c_FechaFinal);

            if (objMar.n_ErrorNumber == 0)
            {
                dtLista = objMar.dtLista;
            }
            else
            {
                b_OcurrioError = objMar.b_OcurrioError;
                c_ErrorMensaje = objMar.c_ErrorMensaje;
                n_ErrorNumber = objMar.n_ErrorNumber;
            }
        }
    }
}
