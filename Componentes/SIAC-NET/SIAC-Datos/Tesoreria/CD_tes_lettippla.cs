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
    public class CD_tes_lettippla
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public DataTable dtLista = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        public DataTable Listar()
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[0, 3] {
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("tes_lettippla_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public bool TraerRegistro(int n_IdRegistro)
        {
            DataTable DtResultado = new DataTable();
            bool b_Result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("tes_lettippla_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.booOcurrioError == true)
            {
                dtLista = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_Result;
            }
            b_Result = true;
            return b_Result;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool b_Result = false;
            MySqlTransaction trans = null;

            trans = mysConec.BeginTransaction();
            xMiFuncion.ReAbrirConeccion(mysConec);
            try
            {
                // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
                string[,] arrParametros = new string[1, 3] {
                                                {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                          };

                b_Result = xMiFuncion.StoreEjecutar("tes_lettippla_delete", arrParametros, mysConec);
                if (b_Result == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return b_Result;
                }
                b_Result = true;
                trans.Commit();
                return b_Result;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return b_Result;
            }
        }
        public bool Insertar(BE_TES_LETTIPPLA e_TipoPlazo)
        {
            bool b_Ok = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans = null;

            trans = mysConec.BeginTransaction();
            xMiFuncion.ReAbrirConeccion(mysConec);
            try
            {
                if (xMiFuncion.StoreEjecutar("tes_lettippla_insertar", e_TipoPlazo, mysConec, 1) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return b_Ok;
                }

                b_Ok = true;
                trans.Commit();
                return b_Ok;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return b_Ok;
            }
        }
        public bool Actualizar(BE_TES_LETTIPPLA e_TipoPlazo)
        {
            bool b_Ok = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans = null;

            trans = mysConec.BeginTransaction();
            xMiFuncion.ReAbrirConeccion(mysConec);
            try
            {
                if (xMiFuncion.StoreEjecutar("tes_lettippla_actualizar", e_TipoPlazo, mysConec, null) == false)
                 {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return b_Ok;
                }
               
                b_Ok = true;
                trans.Commit();
                return b_Ok;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return b_Ok;
            }
        }
    }
}
