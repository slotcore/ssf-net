using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Estacionamiento;

namespace SIAC_DATOS.Estacionamiento
{
    public class CD_est_liquidacion
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public DataTable dtListar = new DataTable();
        public DataTable dtLiquidaDet = new DataTable();
        public DataTable dtListarDetalle = new DataTable();

        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        public void Listar(int n_IdEmpresa, int AnoTrabajo, int MesTrabajo)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT32", AnoTrabajo.ToString()},
                                            {"n_mestra", "System.INT32", MesTrabajo.ToString()}
                                      };

            dtListar = xMiFuncion.StoreDTLLenar("est_liquidacion_listar", arrParametros, mysConec);

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
            dtListar = xMiFuncion.StoreDTLLenar("est_liquidacion_obtenerregistro", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber == 0)
            {
                arrParametros[0, 0] = "n_idliq";
                dtLiquidaDet = xMiFuncion.StoreDTLLenar("est_liquidaciondet_listar", arrParametros, mysConec);
                if (xMiFuncion.IntErrorNumber == 0)
                {
                    dtListarDetalle = xMiFuncion.StoreDTLLenar("est_liquidacion_consulta2", arrParametros, mysConec);
                    if (xMiFuncion.IntErrorNumber != 0)
                    {
                        dtListar = null;
                        dtLiquidaDet = null;
                        dtListarDetalle = null;
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    }
                }
                else
                {
                    dtListar = null;
                    dtLiquidaDet = null;
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                }
            }
            else
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
            MySqlTransaction trans;
            bool b_Result = false;

            trans = mysConec.BeginTransaction();

            try
            {
                string[,] arrParametros = new string[1, 3] {
                                                {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                          };

                if (xMiFuncion.StoreEjecutar("est_liquidacion_delete", arrParametros, mysConec) == false)
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
                b_OcurrioError = true;
                c_ErrorMensaje = exc.Message.ToString();
                n_ErrorNumber = exc.HResult;
                trans.Rollback();
                return b_Result;
            }
        }
        public bool Insertar(BE_EST_LIQUIDACION e_Liquidacion, List<BE_EST_LIQUIDACIONDET> e_LiquidacionDet)
        {
            int n_row = 0;
            int n_idgen = 0;
            MySqlTransaction trans;
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            trans = mysConec.BeginTransaction();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_iddoc", "System.STRING", ""},
                                            {"n_valor", "System.STRING", "2"}
                                      };

            try
            {
                if (xMiFuncion.StoreEjecutar("est_liquidacion_insertar", e_Liquidacion, mysConec, 1) == true)
                {
                    n_idgen = Convert.ToInt32(xMiFuncion.intIdGenerado);
                    for (n_row = 0; n_row <= e_LiquidacionDet.Count - 1; n_row++)
                    {
                        e_LiquidacionDet[n_row].n_idliq = n_idgen;
                        if (xMiFuncion.StoreEjecutar("est_liquidaciondet_insertar", e_LiquidacionDet[n_row], mysConec, 2) == true)
                        {
                            arrParametros[0,2] = e_LiquidacionDet[n_row].n_iddocori.ToString();

                            if (e_LiquidacionDet[n_row].n_idtip == 1)
                            {
                                booOk = xMiFuncion.StoreEjecutar("est_movimientos_liquidar", arrParametros, mysConec);
                            }
                            else
                            {
                                booOk = xMiFuncion.StoreEjecutar("est_cargoscab_liquidar", arrParametros, mysConec);
                            }

                            if (booOk ==false)
                            {
                                b_OcurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                return booOk;
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
                trans.Commit();
                booOk = true;
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
        public bool Actualizar(BE_EST_LIQUIDACION e_Liquidacion, List<BE_EST_LIQUIDACIONDET> e_LiquidacionDet)
        {
            int n_row = 0;
            int n_fil = 0;
            MySqlTransaction trans;
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            trans = mysConec.BeginTransaction();

            try
            {
                string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", e_Liquidacion.n_id.ToString()}
                                      };

                if (xMiFuncion.StoreEjecutar("est_liquidacion_delete", arrParametros, mysConec) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }

                if (xMiFuncion.StoreEjecutar("est_liquidacion_actualizar", e_Liquidacion, mysConec, null) == true)
                {
                    for (n_row = 0; n_row <= e_LiquidacionDet.Count - 1; n_row++)
                    {
                        if (xMiFuncion.StoreEjecutar("est_liquidaciondet_insertar", e_LiquidacionDet[n_row], mysConec, 1) == false)
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
                trans.Commit();
                booOk = true;
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
        public void Consulta1(int n_IdEmpresa, int n_IdPlaya, int n_Idcajero, string c_Fecha)
        {
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_idpla", "System.INT32", n_IdPlaya.ToString()},
                                            {"n_idcaj", "System.INT32", n_Idcajero.ToString()},
                                            {"c_fchcob", "System.STRING", c_Fecha.ToString()}
                                      };

            dtListar = xMiFuncion.StoreDTLLenar("est_liquidacion_consulta1", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
    }
}
