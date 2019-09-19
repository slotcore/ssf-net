using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Almacen;

namespace SIAC_DATOS.Almacen
{
    public class CD_alm_movimientos
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtMovimiento = new DataTable();
        public DataTable dtMovimientoDet = new DataTable();
        public DataTable dtResumenKardex = new DataTable();
        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        public DataTable Listar(int n_idempresa, int n_idmes, int n_idano, int n_idtipmov, int n_idtipitem)
        {
            DataTable DtResultado = new DataTable();

            xMiFuncion.ReAbrirConeccion(mysConec);
            string[,] arrParametros = new string[5, 3] {
                                            {"n_idemp", "System.INT64",n_idempresa.ToString()},
                                            {"n_anotra", "System.INT64",n_idano.ToString()},
                                            {"n_idmes", "System.INT64",n_idmes.ToString()},
                                            {"n_idtipmov", "System.INT64",n_idtipmov.ToString()},
                                            {"n_tipite", "System.INT64",n_idtipitem.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_movimientos_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Consulta6(int n_IdEmpresa)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_movimientos_consulta6", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Consulta11(int n_IdEmpresa,int n_IdClienteProveedor)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()},
                                            {"n_idclipro", "System.INT64", n_IdClienteProveedor.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_movimientos_consulta11", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Consulta7(string c_CadenaIN)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"c_cadin", "System.STRING",c_CadenaIN.ToString()},
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_movimientos_consulta7", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Consulta8(int n_IdEmpresa, int n_IdProveedor)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idpro", "System.INT16", n_IdProveedor.ToString()},
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_movimientos_consulta8", arrParametros, mysConec);

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
            DtResultado = xMiFuncion.StoreDTLLenar("alm_movimientos_obtenerregistro", arrParametros, mysConec);

            string[,] arrParametrosUniMed = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
            DtDetalle = xMiFuncion.StoreDTLLenar("alm_movimientosdet_listar", arrParametrosUniMed, mysConec);

            dtMovimiento = DtResultado;
            dtMovimientoDet = DtDetalle;
            booResult = true;

            return booResult;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            xMiFuncion.ReAbrirConeccion(mysConec);
            // ELIMINAMOS EL LOTE CREADO
            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_iddocmov", "System.INT64", n_IdRegistro.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("alm_inventariolotes_delete", arrParametros2, mysConec);

            // ELIMINAMOS EL DETALLE DEL INGRESO
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("alm_movimientosdet_delete", arrParametros, mysConec);

            // ELIMINAMOS LA CABECERA DEL INGRESO
            booResult = xMiFuncion.StoreEjecutar("alm_movimientos_delete", arrParametros, mysConec);
            
            if (booResult == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;

            }

            return booResult;
        }
        public bool Insertar(BE_ALM_MOVIMIENTOS entCabecera, List<BE_ALM_MOVIMIENTOSDET> lstDetalle, List<BE_ALM_INVENTARIOLOTE> lstLote)
        {
            bool booOk = false;
            int intFila = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            xMiFuncion.ReAbrirConeccion(mysConec);
            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("alm_movimientos_insertar", entCabecera, mysConec, 0) == true)
                {
                    for (intFila = 0; intFila <= lstDetalle.Count - 1; intFila++)
                    {
                        entCabecera.n_id = Convert.ToInt16(xMiFuncion.intIdGenerado);
                        lstDetalle[intFila].n_idmov = Convert.ToInt16(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("alm_movimientosdet_insertar", lstDetalle[intFila], mysConec, null) == true)
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

                        // AGREGAMOS LOS LOTES
                        lstLote[intFila].n_iddocmov = Convert.ToInt16(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("alm_inventariolotes_insertar", lstLote[intFila], mysConec, null) == true) 
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
                trans.Rollback();
                return booOk;
            }
        }
        public bool Actualizar(BE_ALM_MOVIMIENTOS entCabecera, List<BE_ALM_MOVIMIENTOSDET> lstDetalle, List<BE_ALM_INVENTARIOLOTE> lstLote)
        {
            bool booOk = false;
            int intFila = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            xMiFuncion.ReAbrirConeccion(mysConec);
            trans = mysConec.BeginTransaction();
            try
            {
                if (xMiFuncion.StoreEjecutar("alm_movimientos_actualizar", entCabecera, mysConec, null) == true)
                {
                    // ELIMINAMOS LOS ITEMS PREVIOS
                    string[,] arrParametrosLote = new string[2, 3] {
                                                {"n_idemp", "System.INT64", entCabecera.n_idemp.ToString()},
                                                {"n_iddocmov", "System.INT64", entCabecera.n_id.ToString()}
                                          };
                    // BORRAMOS EL MOVIMIENTOS DE LOS LOTES
                    if (xMiFuncion.StoreEjecutar("alm_inventariolotes_delete", arrParametrosLote, mysConec) == true)
                    {
                        string[,] arrParametros = new string[1, 3] {
                                                {"n_id", "System.INT64", entCabecera.n_id.ToString()}
                                          };

                        // BORRAMOS EL DETALLE
                        if (xMiFuncion.StoreEjecutar("alm_movimientosdet_delete", arrParametros, mysConec) == true)
                        {
                            // SI LOS ITEMS SE ELIMINARON CON EXITO INSERTAMOS LOS NUEVOS ITEMS
                            for (intFila = 0; intFila <= lstDetalle.Count - 1; intFila++)
                            {
                                //lstDetalle[intFila].n_idmov = Convert.ToInt16(xMiFuncion.intIdGenerado);
                                if (xMiFuncion.StoreEjecutar("alm_movimientosdet_insertar", lstDetalle[intFila], mysConec, null) == true)
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

                                // AGREGAMOS LOS LOTES
                                //lstLote[intFila].n_iddocmov = Convert.ToInt16(xMiFuncion.intIdGenerado);
                                if (xMiFuncion.StoreEjecutar("alm_inventariolotes_insertar", lstLote[intFila], mysConec, null) == true)
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
                    // CONTROLAR EL ERROR
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    booOk = false;
                    return booOk;
                }
                booOk = true;
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
        public bool ActualizarDocCompra(int n_IdMovimiento, int n_IdDocCompra,int n_Estado)
        {
            bool b_result = false;
            string[,] arrParametrosLote = new string[3, 3] {
                                                {"n_idmov", "System.INT32", n_IdMovimiento.ToString()},
                                                {"n_iddoccom", "System.INT32", n_IdDocCompra.ToString()},
                                                {"n_estado", "System.INT32", n_Estado.ToString()}
                                          };
            if (xMiFuncion.StoreEjecutar("alm_movimientos_actualizardoccom", arrParametrosLote, mysConec) == false)
            {
                // CONTROLAR EL ERROR
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool DocumentoExiste(int n_IdEmpresa, int n_IdTipoDocumento, string c_NumSerie, string c_NumDocumento, int n_TipoMovimiento)
        {
            bool b_result = false;

            string[,] arrParametros = new string[5, 3] {
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()},
                                            {"n_idtipdoc", "System.INT32",n_IdTipoDocumento.ToString()},
                                            {"c_numser", "System.STRING",c_NumSerie.ToString()},
                                            {"c_numdoc", "System.STRING",c_NumDocumento.ToString()},
                                            {"n_idtipdoc", "System.STRING",n_TipoMovimiento.ToString()}
                                      };

            dtMovimiento = xMiFuncion.StoreDTLLenar("alm_movimientos_existenumerodocumento", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtMovimiento = null;
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
        //public bool VerKardexResumido(int n_IdEmpresa, int n_AnoConsulta, int n_PeriodoConsulta, int n_TipoExistencia, int n_IdAlmacen)
        //{
        //    DataTable DtResultado = new DataTable();
        //    DataTable DtDetalle = new DataTable();
        //    bool booResult = false;

        //    string[,] arrParametros = new string[5, 3] {
        //                                    {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
        //                                    {"n_anocon", "System.INT32", n_AnoConsulta.ToString()},
        //                                    {"n_percon", "System.INT32", n_PeriodoConsulta.ToString()},
        //                                    {"n_idtipexi", "System.INT32", n_TipoExistencia.ToString()},
        //                                    {"n_idalm", "System.INT32", n_IdAlmacen.ToString()}
        //                              };

        //    DtResultado = xMiFuncion.StoreDTLLenar("alm_kardex_resumen", arrParametros, mysConec);

        //    dtResumenKardex = DtResultado;
        //    booResult = true;

        //    return booResult;
        //}
        public bool VerKardexResumido(int n_IdEmpresa, int n_AnoConsulta, string c_FechaInicio, string c_FechaFinal, int n_TipoExistencia, int n_IdAlmacen)
        {
            DataTable DtResultado = new DataTable();
            DataTable DtDetalle = new DataTable();
            bool booResult = false;

            string[,] arrParametros = new string[6, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_anocon", "System.INT32", n_AnoConsulta.ToString()},
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_Fchfin", "System.STRING", c_FechaFinal.ToString()},
                                            {"n_idtipexi", "System.INT32", n_TipoExistencia.ToString()},
                                            {"n_idalm", "System.INT32", n_IdAlmacen.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_kardex_resumen_2", arrParametros, mysConec);

            dtResumenKardex = DtResultado;
            booResult = true;

            return booResult;
        }
        //public bool VerKardexDetallado(int n_IdEmpresa, string c_ano, string c_mes, int n_iddelproducto, int n_idtipoexistencia, int n_IdAlmacen)
        //{
        //    DataTable DtResultado = new DataTable();
        //    DataTable DtDetalle = new DataTable();
        //    bool booResult = false;

        //    string[,] arrParametros = new string[6, 3] {
        //                                    {"n_idemp", "System.INT64", n_IdEmpresa.ToString()},
        //                                    {"c_ano", "System.STRING", c_ano},
        //                                    {"c_mes", "System.STRING", c_mes},
        //                                    {"n_idpro", "System.INT32", n_iddelproducto.ToString()},
        //                                    {"n_idtipexi", "System.INT32", n_idtipoexistencia.ToString()},
        //                                    {"n_idalm", "System.INT32", n_IdAlmacen.ToString()},
        //                              };
        //    DtResultado = xMiFuncion.StoreDTLLenar("alm_kardex_detalle", arrParametros, mysConec);

        //    dtResumenKardex = DtResultado;
        //    booResult = true;

        //    return booResult;
        //}
        public bool VerKardexDetallado(int n_IdEmpresa, string c_Ano, string c_FechaInicio, string c_FechaFinal, int n_IdProducto, int n_IdAlmacen)
        {
            DataTable DtResultado = new DataTable();
            DataTable DtDetalle = new DataTable();
            bool booResult = false;

            string[,] arrParametros = new string[6, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()},
                                            {"n_anocon", "System.STRING", c_Ano},
                                            {"c_fchini", "System.STRING", c_FechaInicio},
                                            {"c_fchfin", "System.STRING", c_FechaFinal},
                                            {"n_idexi", "System.INT32", n_IdProducto.ToString()},
                                            {"n_idalm", "System.INT32", n_IdAlmacen.ToString()},
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_kardex_detalle_2", arrParametros, mysConec);

            dtResumenKardex = DtResultado;
            booResult = true;

            return booResult;
        }
        public DataTable VerMovimientosAlmacen(int n_IdEmpresa, int n_IdAlmacen, int n_TipoOperacion, int n_IdTipExistencia, int n_IdItem, int n_TipoMovimiento, string c_FchInicio, string c_FchFin)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[8, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_idtipmov", "System.INT32", n_TipoMovimiento.ToString()},
                                            {"n_idalm", "System.INT32", n_IdAlmacen.ToString()},
                                            {"n_idtipope", "System.INT32", n_TipoOperacion.ToString()},
                                            {"n_idtipexi", "System.INT32", n_IdTipExistencia.ToString()},
                                            {"n_idite", "System.INT32", n_IdItem.ToString()},
                                            {"c_fchini", "System.STRING", c_FchInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FchFin.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_movimientos_consulta5", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
    }
}
