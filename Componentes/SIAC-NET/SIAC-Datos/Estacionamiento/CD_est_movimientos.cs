using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Estacionamiento;
using SIAC_Entidades.Contabilidad;
using SIAC_Entidades.Ventas;
using SIAC_DATOS.Contabilidad;

namespace SIAC_DATOS.Estacionamiento
{
    public class CD_est_movimientos
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public DataTable dtListar = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();
        public int n_IdRegistro = 0;
        public bool b_GENERARASIENTO = false;                                             // INDICA SI SE GENERARA EL ASIENTO CONTABLE DE LA OPERACION

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public BE_VTA_VENTAS e_Documento = new BE_VTA_VENTAS();
        public List<BE_VTA_VENTASDET> l_DocumentoDet = new List<BE_VTA_VENTASDET>();
        public List<BE_VTA_VENTASDOC> l_DetDoc = new List<BE_VTA_VENTASDOC>();
        public List<BE_VTA_VENTASOCT> l_DetOCT = new List<BE_VTA_VENTASOCT>();
        public List<BE_VTA_VENTASDAT> l_DetDat = new List<BE_VTA_VENTASDAT>();
        public List<BE_CON_DIARIO> l_Diario = new List<BE_CON_DIARIO>();

        public List<BE_EST_MOVIMIENTOSDET> l_movdet = new List<BE_EST_MOVIMIENTOSDET>();
        public CD_est_movimientos(string c_IPServidor, string c_NombreBD, string c_Usuario, string c_Passord, string c_Puerto)
        {
            MySqlConnection mysConectar = new MySqlConnection();
            Helper.DatosMySql hlpFuncion = new Helper.DatosMySql();

            mysConec = hlpFuncion.ObtenerConexion(c_IPServidor, c_NombreBD, c_Usuario, c_Passord, c_Puerto);

            if (hlpFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        ~CD_est_movimientos()
        {
            mysConec.Close();

            mysConec = null;
            xMiFuncion = null;
            xFun = null;
            xFunGen = null;
            e_Documento = null;
            l_DocumentoDet = null;
            l_DetDoc = null;
            l_DetOCT = null;
            l_DetDat = null;
            l_Diario = null;
        }
        public void Listar(int n_IdEmpresa, int n_idLocal, string c_FechaMovimientos)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_idloc", "System.INT32", n_idLocal.ToString()},
                                            {"c_dia", "System.STRING", c_FechaMovimientos.ToString()}
                                      };
            //mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            dtListar = xMiFuncion.StoreDTLLenar("est_movimientos_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool TraerRegistro(int n_IdRegistro)
        {
            bool booResult = false;
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("est_movimientos_obtenerregistro", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            booResult = true;
            return booResult;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("est_movimientos_delete", arrParametros, mysConec);
            if (booResult == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;
        }
        public bool AnularPago(int n_IdRegistro, int n_IdDocVenta, int n_IdEmpresa)
        {
            bool booResult = false;
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idmov", "System.INT32", n_IdRegistro.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("est_movimientos_anularpago", arrParametros, mysConec);
            if (booResult == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return booResult;
            }
            else
            {
                string[,] arrParametros2 = new string[2, 3] {
                                            {"n_iddoc", "System.INT32", n_IdDocVenta.ToString()},
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()}
                                      };

                booResult = xMiFuncion.StoreEjecutar("vta_ventas_anulardocumento", arrParametros2, mysConec);
                if (booResult == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                }
            }
            return booResult;
        }
        public bool Anular(int n_IdRegistro, int n_IdDocumentoVenta)
        {
            bool booResult = false;
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            string[,] arrParametros = new string[2, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()},
                                            {"n_idven", "System.INT32", n_IdDocumentoVenta.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("est_movimientos_anular", arrParametros, mysConec);
            if (booResult == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_EST_MOVIMIENTOS e_Movimiento)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            n_IdRegistro = 0;
            trans = mysConec.BeginTransaction();

            try
            { 
                booOk = xMiFuncion.StoreEjecutar("est_movimientos_insertar", e_Movimiento, mysConec, 2);
                if (booOk == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }
                n_IdRegistro = Convert.ToInt32(xMiFuncion.intIdGenerado);
                trans.Commit();
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = true;
                c_ErrorMensaje = exc.Message.ToString();
                n_ErrorNumber = exc.HResult;
                trans.Rollback();
                return booOk;
            }
        }
        public bool Actualizar(BE_EST_MOVIMIENTOS e_Movimiento, bool b_GenerarCargo)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            CD_con_diario funCon = new CD_con_diario();
            int intFila = 0;
            MySqlTransaction trans;
            int n_IdGenerado = 0;
            DataTable dtresul = new DataTable();
            string c_numasi = "0000";

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            trans = mysConec.BeginTransaction();
            funCon.mysConec = mysConec;

            try
            {
                e_Movimiento.n_iddocven = 0;
                if (b_GenerarCargo == true)
                {
                    if (b_GENERARASIENTO == true)
                    { 
                        // OBTENEMOS EL ULTIMO ASIENTO
                        funCon.ObtenerUltimoAsiento(e_Documento.n_anotra, e_Documento.n_idmes, 14, e_Documento.n_idemp);
                        dtresul = funCon.dtLista;
                        c_numasi = Convert.ToInt16(dtresul.Rows[0]["c_newnumero"]).ToString("0000");
                    }

                    e_Documento.c_numreg = c_numasi;

                    if (xMiFuncion.StoreEjecutar("vta_ventas_insertar", e_Documento, mysConec, 0) == true)
                    {
                        // AGREGAMOS EL DETALLE DE LA VENTA
                        n_IdGenerado = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        e_Documento.n_id = Convert.ToInt64(n_IdGenerado);
                                               
                        for (intFila = 0; intFila <= l_Diario.Count - 1; intFila++)
                        {
                            l_Diario[intFila].n_oriid = n_IdGenerado;
                            l_Diario[intFila].c_numasi = c_numasi;
                        }
                                                
                        for (intFila = 0; intFila <= l_DocumentoDet.Count - 1; intFila++)
                        {
                            l_DocumentoDet[intFila].n_idvta = n_IdGenerado;
                            if (xMiFuncion.StoreEjecutar("vta_ventasdet_insertar", l_DocumentoDet[intFila], mysConec, null) == false)
                            {
                                b_OcurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }

                        // AGREGAMOS LOS DOCUMENTOS DE LA VENTA
                        for (intFila = 0; intFila <= l_DetDoc.Count - 1; intFila++)
                        {
                            l_DetDoc[intFila].n_idvta = Convert.ToInt32(n_IdGenerado);
                            if (xMiFuncion.StoreEjecutar("vta_ventasdoc_insertar", l_DetDoc[intFila], mysConec, null) == false)
                            {
                                b_OcurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }

                        // ESTO ES PARA LA FACTURACION ELECTRONICA
                        for (intFila = 0; intFila <= l_DetOCT.Count - 1; intFila++)
                        {
                            l_DetOCT[intFila].n_idvta = n_IdGenerado;
                            if (xMiFuncion.StoreEjecutar("vta_ventasoct_insertar", l_DetOCT[intFila], mysConec, null) == false)
                            {
                                b_OcurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }

                        // PARA OTROS DATOS DE LA FACTURA
                        for (intFila = 0; intFila <= l_DetDat.Count - 1; intFila++)
                        {
                            l_DetDat[intFila].n_idvta = n_IdGenerado;
                            if (xMiFuncion.StoreEjecutar("vta_ventasdat_insertar", l_DetDat[intFila], mysConec, null) == false)
                            {
                                b_OcurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }
                        if (b_GENERARASIENTO == true)
                        { 
                            funCon.b_DesdeOtraCapa = true;
                            funCon.mysConec = mysConec;
                            if (funCon.Insertar(l_Diario)==false)
                            {
                                b_OcurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }
                        e_Movimiento.n_iddocven = n_IdGenerado;
                    }
                    else
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }
                }

                // ACTUALIZAMOS EL MOVIMIENTO EN LA TABLA MOVIMIENTOS DEL MODULO DE ESTACIONAMIENTO
                if (xMiFuncion.StoreEjecutar("est_movimientos_actualizar", e_Movimiento, mysConec, null) == true)
                {
                    int n_row = 0;
                    for (n_row = 0; n_row <= l_movdet.Count - 1; n_row++)
                    {
                        l_movdet[n_row].n_idmov = e_Movimiento.n_id;
                        booOk = xMiFuncion.StoreEjecutar("est_movimientosdet_insertar", l_movdet[n_row], mysConec, null);
                        if (booOk == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
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
                    return booOk;
                }

                booOk = true;

                trans.Commit();
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = true;
                c_ErrorMensaje = exc.Message.ToString();
                n_ErrorNumber = exc.HResult;
                trans.Rollback();
                return booOk;
            }
        }
        public void Consulta2(int n_IdEmpresa, int n_IdPlaya, int n_IdCaja, string c_FcehaInicio, string c_FechaFinal)
        {
            string[,] arrParametros = new string[5, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_idpla", "System.INT32", n_IdPlaya.ToString()},
                                            {"n_idcaj", "System.INT32", n_IdCaja.ToString()},
                                            {"c_fchini", "System.STRING", c_FcehaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FechaFinal.ToString()}
                                      };

            dtListar = xMiFuncion.StoreDTLLenar("est_movimientos_consulta2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void ImprimirVenta(int n_IdEmpresa, int n_Idventa)
        {
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_idvta", "System.INT32", n_Idventa.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("vta_ventas_imprimedoc", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void ImprimirTicket(int n_IdRegistro)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("est_movimientos_consulta1", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        //public bool Actualizar(BE_EST_MOVIMIENTOS e_Movimiento)
        //{
        //    bool booOk = false;
        //    DatosMySql xMiFuncion = new DatosMySql();

        //    booOk = xMiFuncion.StoreEjecutar("est_movimientos_actualizar", e_Movimiento, mysConec, null);

        //    if (booOk == false)
        //    {
        //        b_OcurrioError = xMiFuncion.booOcurrioError;
        //        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
        //        n_ErrorNumber = xMiFuncion.IntErrorNumber;
        //    }

        //    return booOk;
        //}
    }
}
