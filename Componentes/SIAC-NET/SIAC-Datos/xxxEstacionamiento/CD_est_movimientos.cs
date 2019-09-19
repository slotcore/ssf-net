using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Estacionamiento;
using SIAC_Entidades.Ventas;

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

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public BE_VTA_VENTAS e_Documento = new BE_VTA_VENTAS();
        public List<BE_VTA_VENTASDET> l_DocumentoDet = new List<BE_VTA_VENTASDET>();
        public List<BE_VTA_VENTASDOC> l_DetDoc = new List<BE_VTA_VENTASDOC>();
        public List<BE_VTA_VENTASOCT> l_DetOCT = new List<BE_VTA_VENTASOCT>();

        public void Listar(int n_IdEmpresa, int n_idLocal, string c_FechaMovimientos)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_idloc", "System.INT32", n_idLocal.ToString()},
                                            {"c_dia", "System.STRING", c_FechaMovimientos.ToString()}
                                      };

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
        public bool Insertar(BE_EST_MOVIMIENTOS e_Movimiento)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;
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
            int intFila = 0;
            MySqlTransaction trans;
            int n_IdGenerado = 0;

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            trans = mysConec.BeginTransaction();

            try
            {
                e_Movimiento.n_iddocven = 0;
                if (b_GenerarCargo == true)
                {
                    if (xMiFuncion.StoreEjecutar("vta_ventas_insertar", e_Documento, mysConec, 0) == true)
                    {
                        // AGREGAMOS EL DETALLE DE LA VENTA
                        n_IdGenerado = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        e_Documento.n_id = Convert.ToInt64(n_IdGenerado);
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
                if (xMiFuncion.StoreEjecutar("est_movimientos_actualizar", e_Movimiento, mysConec, null) == false)
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
