using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Ventas;

namespace SIAC_DATOS.Ventas
{
    public class CD_vta_pedidocli
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;

        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtCabecera = new DataTable();
        public DataTable dtDetalle = new DataTable();
        public DataTable dtLisPedPend = new DataTable();
        public DataTable dtLisTodPedidos = new DataTable();
        public DataTable dtLista = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Genericas funDatos = new Genericas();

        public BE_VTA_PEDIDOCLI entPedCab = new BE_VTA_PEDIDOCLI();
        public List<BE_VTA_PEDIDOCLIDET> lstPedDet = new List<BE_VTA_PEDIDOCLIDET>();
        public bool ListarPedidosPendientes(int n_IdEmpresa, int n_IdCliente)
        {
            bool b_result = false;
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16",n_IdEmpresa.ToString()},
                                            {"n_idcli", "System.INT16",n_IdCliente.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("vta_pedidocli_listarpendientescliente", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            { 
                dtLisPedPend = DtResultado;
                b_result = true;
            }

            return b_result;
        }
        public bool ListarPedidosPendientes(int n_IdEmpresa)
        {
            bool b_result = false;
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT16",n_IdEmpresa.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("vta_pedidocli_listarpendientescliente2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                dtLisPedPend = DtResultado;
                b_result = true;
            }

            return b_result;
        }
        public bool ListarTodoPedidos(int n_idempresa)
        {
            bool b_result = false;
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_idempresa.ToString()},
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("vta_pedidocli_listartodospedidos", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                dtLisTodPedidos = DtResultado;
                b_result = true;
            }

            return b_result;
        }
        public bool ActualizarEstadoPedido(int n_IdRegistro, int n_IdEstado)
        {
            bool b_result = false;

            string[,] arrParametros = new string[2, 3] {
                                            {"n_id", "System.INT64",n_IdRegistro.ToString()},
                                            {"n_idest", "System.INT64",n_IdEstado.ToString()}
                                      };

            b_result = xMiFuncion.StoreEjecutar("vta_pedidocli_actualizarestado", arrParametros, mysConec);

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

            DtResultado = xMiFuncion.StoreDTLLenar("vta_pedidocli_listar", arrParametros, mysConec);

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

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("vta_pedidocli_obtenerregistro", arrParametros, mysConec);

            string[,] arrParametrosUniMed = new string[1, 3] {
                                            {"n_idped", "System.INT64", n_IdRegistro.ToString()}
                                      };
            DtDetalle = xMiFuncion.StoreDTLLenar("vta_pedidoclidet_listar", arrParametrosUniMed, mysConec);

            dtCabecera = DtResultado;
            dtDetalle = DtDetalle;
            booResult = true;

            return booResult;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            MySqlTransaction trans;
            
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };

            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idped", "System.INT64", n_IdRegistro.ToString()}
                                      };

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            trans = mysConec.BeginTransaction();

            try
            {
                // ELIMINAMOS EL DETALLE DEL REQUERIMIENTO
                booResult = xMiFuncion.StoreEjecutar("vta_pedidoclidet_delete", arrParametros2, mysConec);
                if (booResult == true)
                {
                    // ELIMINAMOS LA CABECERA DEL REQUERIMIENTO
                    booResult = xMiFuncion.StoreEjecutar("vta_pedidocli_delete", arrParametros, mysConec);

                    if (booResult == false)
                    {
                        trans.Rollback();
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return booResult;
                    }
                }
                else
                {
                    trans.Rollback();
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
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
        public bool Insertar(BE_VTA_PEDIDOCLI entCabecera, List<BE_VTA_PEDIDOCLIDET> lstDetalle)
        {
            bool booOk = false;
            int intFila = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("vta_pedidocli_insertar", entCabecera, mysConec, 1) == true)
                {
                    for (intFila = 0; intFila <= lstDetalle.Count - 1; intFila++)
                    {
                        entCabecera.n_id = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        lstDetalle[intFila].n_idped = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("vta_pedidoclidet_insertar", lstDetalle[intFila], mysConec, null) == true)
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
        public bool Actualizar(BE_VTA_PEDIDOCLI entCabecera, List<BE_VTA_PEDIDOCLIDET> lstDetalle)
        {
            bool booOk = false;
            int intFila = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            trans = mysConec.BeginTransaction();
            try
            {
                if (xMiFuncion.StoreEjecutar("vta_pedidocli_actualizar", entCabecera, mysConec, null) == true)
                {
                    string[,] arrParametros = new string[1, 3] {
                                                                    {"n_idped", "System.INT64", entCabecera.n_id.ToString()}
                                                               };
                    // BORRAMOS EL DETALLE
                    if (xMiFuncion.StoreEjecutar("vta_pedidoclidet_delete", arrParametros, mysConec) == true)
                    {
                        // SI LOS ITEMS SE ELIMINARON CON EXITO INSERTAMOS LOS NUEVOS ITEMS
                        for (intFila = 0; intFila <= lstDetalle.Count - 1; intFila++)
                        {
                            lstDetalle[intFila].n_idped = Convert.ToInt32(entCabecera.n_id);
                            if (xMiFuncion.StoreEjecutar("vta_pedidoclidet_insertar", lstDetalle[intFila], mysConec, null) == true)
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
        public bool ActualizarEnvioProduccion(int n_IdPedido, int IdEstado)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_id", "System.INT32", n_IdPedido.ToString()},
                                            {"n_estado", "System.INT32", IdEstado.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("vta_pedidocli_actualizarenvprod", arrParametros, mysConec) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return booOk;
            }
            booOk = true;
            return booOk;
        }
        public void MostrarEntregas(int n_IdPedidoCliente)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idped", "System.INT32", n_IdPedidoCliente.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_pedidocli_mostrarentregas", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void PedidosCliente(int n_IdEmpresa, int n_IdCliente)
        {
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_idcli", "System.INT32", n_IdCliente.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_pedidocli_consulta2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void CtaCtePedidos(int n_IdEmpresa, string c_FechaInicio, string c_FchFInal, int n_Tipo)
        {
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FchFInal.ToString()},
                                            {"n_tipo", "System.INT32", n_Tipo.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_pedidocli_ctacte", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
    }
}