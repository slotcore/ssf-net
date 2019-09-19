using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Tesoreria;
namespace SIAC_DATOS.Tesoreria
{
    public class CD_tes_documentos
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public DataTable dtDocumentos = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public DataTable dtLista = new DataTable();
        public void Listar()
        {
            string[,] arrParametros = new string[0, 3] {
                                      };

            dtDocumentos = xMiFuncion.StoreDTLLenar("tes_documentos_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        public bool TraerRegistro(int n_IdRegistro)
        {
            DataTable DtResultado = new DataTable();
            bool booResult = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("tes_documentos_obtenerregistro", arrParametros, mysConec);

            dtLista = DtResultado;
            booResult = true;

            return booResult;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("tes_documentos_delete", arrParametros, mysConec);
            if (booResult == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_TES_DOCUMENTOS e_Documentos)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("tes_documentos_insertar", e_Documentos, mysConec, 0) == true)
            {
                booOk = true;
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Actualizar(BE_TES_DOCUMENTOS e_Documentos)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("tes_documentos_actualizar", e_Documentos, mysConec, null) == true)
            {
                booOk = true;
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
    }
}
