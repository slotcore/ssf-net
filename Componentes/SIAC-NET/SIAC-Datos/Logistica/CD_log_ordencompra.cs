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
    public class CD_log_ordencompra
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;

        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtCabecera = new DataTable();
        public DataTable dtDetalle = new DataTable();
        public DataTable dtOrdenPendientes = new DataTable();
        public DataTable dtListaOrdenReq = new DataTable();
        public DataTable dtLista = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();

        public BE_LOG_ORDENCOMPRA e_OC = new BE_LOG_ORDENCOMPRA();
        public List<BE_LOG_ORDENCOMPRADET> e_OCdet = new List<BE_LOG_ORDENCOMPRADET>();
        public void Listar(int n_idempresa, int n_idmes, int n_idano)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT64",n_idempresa.ToString()},
                                            {"n_anotra", "System.INT64",n_idano.ToString()},
                                            {"n_mestra", "System.INT64",n_idmes.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("log_ordencompra_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool TraerRegistro(Int64 n_IdRegistro)
        {
            bool b_Result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
            dtCabecera = xMiFuncion.StoreDTLLenar("log_ordencompra_obtenerregistro", arrParametros, mysConec);

            if (xMiFuncion.booOcurrioError == false)
            {
                string[,] arrParametrosUniMed = new string[1, 3] {
                                                {"n_idoc", "System.INT64", n_IdRegistro.ToString()}
                                          };
                dtDetalle = xMiFuncion.StoreDTLLenar("log_ordencompradet_listar", arrParametrosUniMed, mysConec);

                if (xMiFuncion.booOcurrioError == true)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    return b_Result;
                }
                b_Result = true;
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return b_Result;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool b_Result = false;

            // ELIMINAMOS EL DETALLE DEL REQUERIMIENTO
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idoc", "System.INT64", n_IdRegistro.ToString()}
                                      };

            xMiFuncion.ReAbrirConeccion(mysConec);
            b_Result = xMiFuncion.StoreEjecutar("log_ordencompradet_delete", arrParametros, mysConec);
            if (xMiFuncion.booOcurrioError == false)
            {
                string[,] arrParametros2 = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
                // ELIMINAMOS LA CABECERA DEL REQUERIMIENTO
                b_Result = xMiFuncion.StoreEjecutar("log_ordencompra_delete", arrParametros2, mysConec);
                if (xMiFuncion.booOcurrioError == true)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    return b_Result;
                }
                b_Result = true;
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return b_Result;
        }
        public bool Insertar(BE_LOG_ORDENCOMPRA e_OrdenCompra, List<BE_LOG_ORDENCOMPRADET> l_OrdenCompraDetalle)
        {
            bool booOk = false;
            int intFila = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            xMiFuncion.ReAbrirConeccion(mysConec);
            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("log_ordencompra_insertar", e_OrdenCompra, mysConec, 1) == true)
                {
                    for (intFila = 0; intFila <= l_OrdenCompraDetalle.Count - 1; intFila++)
                    {
                        e_OrdenCompra.n_id = Convert.ToInt16(xMiFuncion.intIdGenerado);
                        l_OrdenCompraDetalle[intFila].n_idoc = Convert.ToInt16(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("log_ordencompradet_insertar", l_OrdenCompraDetalle[intFila], mysConec, null) == true)
                        {
                            booOk = true;
                        }
                        else
                        {
                            // CONTROLAR EL ERROR
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            booOk = false;
                            return booOk;
                        }
                    }
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
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
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                //trans.Rollback();
                return booOk;
            }
        }
        public bool Actualizar(BE_LOG_ORDENCOMPRA e_OrdenCompra, List<BE_LOG_ORDENCOMPRADET> l_OrdenCompraDetalle)
        {
            bool booOk = false;
            int intFila = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            xMiFuncion.ReAbrirConeccion(mysConec);
            trans = mysConec.BeginTransaction();
            try
            {
                if (xMiFuncion.StoreEjecutar("log_ordencompra_actualizar", e_OrdenCompra, mysConec, null) == true)
                {
                    string[,] arrParametros = new string[1, 3] {
                                                                    {"n_idoc", "System.INT64", e_OrdenCompra.n_id.ToString()}
                                                               };
                    // BORRAMOS EL DETALLE
                    if (xMiFuncion.StoreEjecutar("log_ordencompradet_delete", arrParametros, mysConec) == true)
                    {
                        // SI LOS ITEMS SE ELIMINARON CON EXITO INSERTAMOS LOS NUEVOS ITEMS
                        for (intFila = 0; intFila <= l_OrdenCompraDetalle.Count - 1; intFila++)
                        {
                            l_OrdenCompraDetalle[intFila].n_idoc = Convert.ToInt16(e_OrdenCompra.n_id);
                            if (xMiFuncion.StoreEjecutar("log_ordencompradet_insertar", l_OrdenCompraDetalle[intFila], mysConec, null) == true)
                            {
                                booOk = true;
                            }
                            else
                            {
                                // CONTROLAR EL ERROR
                                b_OcurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                booOk = false;
                                return booOk;
                            }
                        }
                    }
                    else
                    {
                        // CONTROLAR EL ERROR
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        booOk = false;
                        return booOk;
                    }
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
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
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return booOk;
            }
        }

        public bool ConsultaPendientes(int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("log_ordencompra_consulta_pendientes", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
    }
}
