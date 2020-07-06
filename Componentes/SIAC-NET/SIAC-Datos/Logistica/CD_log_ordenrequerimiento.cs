using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Logistica;

namespace SIAC_DATOS.Logistica
{
    public class CD_log_ordenrequerimiento
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;

        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtCabecera = new DataTable();
        public DataTable dtDetalle = new DataTable();
        public DataTable dtOrdenPendientes = new DataTable();
        public DataTable dtListaOrdenReq = new DataTable();
        public DataTable dtLista = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();

        public BE_LOG_ORDENREQUERIMIENTO entReqCab = new BE_LOG_ORDENREQUERIMIENTO();
        public List<BE_LOG_ORDENREQUERIMIENTODET> lstReqDet = new List<BE_LOG_ORDENREQUERIMIENTODET>();

        public DataTable ConsultaRequerimientoPendientes(int n_idempresa)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_idempresa.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("log_ordenrequerimiento_consulta1", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }

        public bool ListarTodoRequerimientos(int n_IdEmpresa)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()}
                                      };

            dtListaOrdenReq = xMiFuncion.StoreDTLLenar("log_ordenrequerimiento_listartodo", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else 
            {
                b_result = true;
            }
            return b_result;
        }


        public bool ListarRequerimientosPorArea(int n_IdEmpresa, int n_IdAreaDest)
        {
            bool b_result = false;

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                            {"n_idareadest", "System.INT64",n_IdAreaDest.ToString()}
                                      };

            dtListaOrdenReq = xMiFuncion.StoreDTLLenar("log_ordenrequerimiento_listarporarea", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool ActualizarEstadoRequerimiento(int n_IdRegistro, int n_IdEstado)
        { 
            bool b_result = false;

            string[,] arrParametros = new string[2, 3] {
                                            {"n_id", "System.INT64",n_IdRegistro.ToString()},
                                            {"n_idest", "System.INT64",n_IdEstado.ToString()}
                                      };
            
            b_result = xMiFuncion.StoreEjecutar("log_ordenrequerimiento_actualizarestado", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }

            return b_result;  
        }
        public bool ListarPendientes(int n_IdEmpresa, string c_CadenaIN)
        {
            bool b_result = false;

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()},
                                            {"c_cadin", "System.STRING", c_CadenaIN.ToString()}
                                      };

            dtOrdenPendientes = xMiFuncion.StoreDTLLenar("log_ordenrequerimiento_listarpendientes", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }

            return b_result;
        }
        public bool TraerTodo(int n_IdEmpresa)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("log_ordenrequerimiento_traertodo", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }

            return b_result;
        }
        
        // ********************
        // ****  C R U D   ****
        // ********************
        public DataTable Listar(int n_idempresa, int n_idmes, int n_idano)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT64",n_idempresa.ToString()},
                                            {"n_anotra", "System.INT64",n_idano.ToString()},
                                            {"n_mestra", "System.INT64",n_idmes.ToString()},
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("log_ordenrequerimiento_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }

        public DataTable ListarPorArea(int n_idempresa, int n_idmes, int n_idano, int n_idareadest)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT64",n_idempresa.ToString()},
                                            {"n_anotra", "System.INT64",n_idano.ToString()},
                                            {"n_mestra", "System.INT64",n_idmes.ToString()},
                                            {"n_idareadest", "System.INT64",n_idareadest.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("log_ordenrequerimiento_listar_todo_porarea", arrParametros, mysConec);

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
            DataTable DtDetalle = new DataTable();
            bool booResult = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("log_ordenrequerimiento_obtenerregistro", arrParametros, mysConec);

            string[,] arrParametrosUniMed = new string[1, 3] {
                                            {"n_idreq", "System.INT64", n_IdRegistro.ToString()}
                                      };
            DtDetalle = xMiFuncion.StoreDTLLenar("log_ordenrequerimientodet_listar", arrParametrosUniMed, mysConec);

            dtCabecera = DtResultado;
            dtDetalle = DtDetalle;
            booResult = true;

            return booResult;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;

            // ELIMINAMOS EL DETALLE DEL REQUERIMIENTO
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("log_ordenrequerimientodet_delete", arrParametros, mysConec);

            // ELIMINAMOS LA CABECERA DEL REQUERIMIENTO
            booResult = xMiFuncion.StoreEjecutar("log_ordenrequerimiento_delete", arrParametros, mysConec);

            if (booResult == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booResult;
        }
        public bool Insertar(BE_LOG_ORDENREQUERIMIENTO entCabecera, List<BE_LOG_ORDENREQUERIMIENTODET> lstDetalle)
        {
            bool booOk = false;
            int intFila = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("log_ordenrequerimiento_insertar", entCabecera, mysConec, 1) == true)
                {
                    for (intFila = 0; intFila <= lstDetalle.Count - 1; intFila++)
                    {
                        entCabecera.n_id = Convert.ToInt16(xMiFuncion.intIdGenerado);
                        lstDetalle[intFila].n_idreq = Convert.ToInt16(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("log_ordenrequerimientodet_insertar", lstDetalle[intFila], mysConec, null) == true)
                        {
                            booOk = true;
                        }
                        else
                        {
                            // CONTROLAR EL ERROR
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            booOk = false;
                            return booOk;
                        }
                    }
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    booOk = false;
                    return booOk;
                }

                if (booOk == true)
                {
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }

                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                //trans.Rollback();
                return booOk;
            }
        }
        public bool Actualizar(BE_LOG_ORDENREQUERIMIENTO entCabecera, List<BE_LOG_ORDENREQUERIMIENTODET> lstDetalle)
        {
            bool booOk = false;
            int intFila = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();
            try
            {
                if (xMiFuncion.StoreEjecutar("log_ordenrequerimiento_actualizar", entCabecera, mysConec, null) == true)
                {
                    string[,] arrParametros = new string[1, 3] {
                                                                    {"n_idreq", "System.INT64", entCabecera.n_id.ToString()}
                                                               };
                    // BORRAMOS EL DETALLE
                    if (xMiFuncion.StoreEjecutar("log_ordenrequerimientodet_delete", arrParametros, mysConec) == true)
                    {
                        // SI LOS ITEMS SE ELIMINARON CON EXITO INSERTAMOS LOS NUEVOS ITEMS
                        for (intFila = 0; intFila <= lstDetalle.Count - 1; intFila++)
                        {
                            lstDetalle[intFila].n_idreq = Convert.ToInt16(entCabecera.n_id);
                            if (xMiFuncion.StoreEjecutar("log_ordenrequerimientodet_insertar", lstDetalle[intFila], mysConec, null) == true)
                            {
                                booOk = true;
                            }
                            else
                            {
                                // CONTROLAR EL ERROR
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                booOk = false;
                                return booOk;
                            }
                        }
                    }
                    else
                    {
                        // CONTROLAR EL ERROR
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        booOk = false;
                        return booOk;
                    }
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    booOk = false;
                    return booOk;
                }
                trans.Commit();
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return booOk;
            }
        }
    }
}
