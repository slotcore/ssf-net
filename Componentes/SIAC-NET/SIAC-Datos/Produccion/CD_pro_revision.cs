using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Produccion;

namespace SIAC_DATOS.Produccion
{
    public class CD_pro_revision
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtRevision = new DataTable();
        public DataTable dtMovimientoDet = new DataTable();
        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        public DataTable Listar(int n_idempresa, int n_idmes, int n_idano)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT64",n_idempresa.ToString()},
                                            {"n_anotra", "System.INT64",n_idano.ToString()},
                                            {"n_idmes", "System.INT64",n_idmes.ToString()}                                                                                      
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("pro_revision_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public bool TraerRegistro(Int64 n_IdRegistro)
        {
            DataTable DtResultado = new DataTable();
            bool booResult = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("pro_revision_obtenerregistro", arrParametros, mysConec);
            dtRevision = DtResultado;
            booResult = true;

            return booResult;
        }
        public bool Eliminar(int n_IdRegistro, int n_IdProduccion)
        {
            bool booResult = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()},
                                            {"n_idpro", "System.INT32", n_IdProduccion.ToString()},
                                      };

            booResult = xMiFuncion.StoreEjecutar("pro_revision_delete", arrParametros, mysConec);

            if (booResult == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booResult;
        }
        public bool Insertar(BE_PRO_REVISION entCabecera)
        {
            bool booResult = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();

            try
            {
                booResult = xMiFuncion.StoreEjecutar("pro_revision_insertar", entCabecera, mysConec, 1);
                
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
        public bool Actualizar(BE_PRO_REVISION entCabecera)
        {
            bool booResult = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();
            try
            {
                booResult = xMiFuncion.StoreEjecutar("pro_revision_actualizar", entCabecera, mysConec, null);

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
        public DataTable ConsultaRevisionPendientes(int n_idempresa)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_idempresa.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("pro_revision_consulta1", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public bool ActualizarEstado(int ?n_IdRegistro, int n_Estado)
        {
            bool booResult = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();
            try
            {
                string[,] arrParametros = new string[2, 3] {
                                            {"n_idreg", "System.INT64",n_IdRegistro.ToString()},
                                            {"n_estado", "System.INT64",n_Estado.ToString()}
                                      };
                booResult = xMiFuncion.StoreEjecutar("pro_revision_actualizarestado", arrParametros, mysConec);

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
