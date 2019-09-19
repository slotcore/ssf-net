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
    public class CD_est_cargos
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public DataTable dtListar = new DataTable();
        public DataTable dtCargoCab = new DataTable();
        public DataTable dtCargoDet = new DataTable();

        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        public void Listar(int n_IdEmpresa, int AnoTrabajo, int MesTrabajo)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32", AnoTrabajo.ToString()},
                                            {"n_mes", "System.INT32", MesTrabajo.ToString()}
                                      };

            dtListar = xMiFuncion.StoreDTLLenar("est_cargos_listar", arrParametros, mysConec);

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
            dtListar = xMiFuncion.StoreDTLLenar("est_cargos_obtenerregistro", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber == 0)
            {
                arrParametros[0, 0] = "n_idcar";
                dtCargoCab = xMiFuncion.StoreDTLLenar("est_cargoscab_listar", arrParametros, mysConec);
                if (xMiFuncion.IntErrorNumber == 0)
                {
                    dtCargoDet = xMiFuncion.StoreDTLLenar("est_cargosdet_listar", arrParametros, mysConec);
                    if (xMiFuncion.IntErrorNumber != 0)
                    {
                        dtListar = null;
                        dtCargoCab = null;
                        dtCargoDet = null;
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    }
                }
                else
                {
                    dtListar = null;
                    dtCargoCab = null;
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

                if (xMiFuncion.StoreEjecutar("est_cargos_delete", arrParametros, mysConec) == true)
                {
                    arrParametros[0, 0] = "n_idcar";

                    if (xMiFuncion.StoreEjecutar("est_cargoscab_delete", arrParametros, mysConec) == true)
                    {
                        if (xMiFuncion.StoreEjecutar("est_cargosdet_delete", arrParametros, mysConec) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return b_Result;
                        }
                    }
                    else
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return b_Result;
                    }
                }
                else
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
        public bool Insertar(BE_EST_CARGOS e_Cargos, List<BE_EST_CARGOSCAB> l_CargosCabecera, List<BE_EST_CARGOSDET> l_CargosDetalle)
        {
            int n_row = 0;
            int n_fil = 0;
            int n_idgen = 0;
            int n_idcab = 0;
            MySqlTransaction trans;
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("est_cargos_insertar", e_Cargos, mysConec, 1) == true)
                {
                    n_idgen = Convert.ToInt32( xMiFuncion.intIdGenerado);
                    for (n_row = 0; n_row <= l_CargosCabecera.Count - 1; n_row++)
                    {
                        l_CargosCabecera[n_row].n_idcar = n_idgen;
                        if (xMiFuncion.StoreEjecutar("est_cargoscab_insertar", l_CargosCabecera[n_row], mysConec, 2) == true)
                        {
                            n_idcab = Convert.ToInt32(xMiFuncion.intIdGenerado);
                            for (n_fil = 0; n_fil <= l_CargosDetalle.Count - 1; n_fil++)
                            {
                                if (l_CargosDetalle[n_fil].n_idcab == l_CargosCabecera[n_row].n_id)
                                {
                                    l_CargosDetalle[n_fil].n_idcar = n_idgen;
                                    l_CargosDetalle[n_fil].n_idcab = n_idcab;
                                    if (xMiFuncion.StoreEjecutar("est_cargosdet_insertar", l_CargosDetalle[n_fil], mysConec, null) == false)
                                    {
                                        b_OcurrioError = xMiFuncion.booOcurrioError;
                                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                        trans.Rollback();
                                        return booOk;                                    
                                    }
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
        public bool Actualizar(BE_EST_CARGOS e_Cargos, List<BE_EST_CARGOSCAB> l_CargosCabecera, List<BE_EST_CARGOSDET> l_CargosDetalle)
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
                                            {"n_id", "System.INT32", e_Cargos.n_id.ToString()}
                                      };

                if (xMiFuncion.StoreEjecutar("est_cargoscab_delete", arrParametros, mysConec) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }

                if (xMiFuncion.StoreEjecutar("est_cargos_actualizar", e_Cargos, mysConec, null) == true)
                {
                    for (n_row = 0; n_row <= l_CargosCabecera.Count - 1; n_row++)
                    {
                        if (xMiFuncion.StoreEjecutar("est_cargoscab_insertar", l_CargosCabecera[n_row], mysConec, 1) == true)
                        {
                            for (n_fil = 0; n_fil <= l_CargosDetalle.Count - 1; n_row++)
                            {
                                if (l_CargosDetalle[n_fil].n_idcab == l_CargosCabecera[n_row].n_id)
                                {
                                    if (xMiFuncion.StoreEjecutar("est_cargosdet_insertar", l_CargosCabecera[n_row], mysConec, 1) == false)
                                    {
                                        b_OcurrioError = xMiFuncion.booOcurrioError;
                                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                        trans.Rollback();
                                        return booOk;
                                    }
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
        public void Consulta1(int n_IdCliente)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idcli", "System.INT32", n_IdCliente.ToString()}
                                      };

            dtListar = xMiFuncion.StoreDTLLenar("est_cargoscab_consulta1", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void Consulta2(int n_IdCliente)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idcli", "System.INT32", n_IdCliente.ToString()}
                                      };

            dtListar = xMiFuncion.StoreDTLLenar("est_cargoscab_consulta2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListar = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool CambiarEstadoCargo(int n_IdCargo, int n_IdDocumentoPago, string c_FechaPago)
        {
            bool b_Result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"n_id", "System.INT16", n_IdCargo.ToString()},
                                            {"n_iddocpag", "System.INT16", n_IdDocumentoPago.ToString()},
                                            {"c_fchpag", "System.STRING", c_FechaPago.ToString()}
                                      };

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (xMiFuncion.StoreEjecutar("est_cargoscab_actualizar_a_pagado", arrParametros, mysConec) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_Result;
            }
            
            b_Result = true;
            return b_Result;
        }

        public void Consulta3(int n_IdCargo)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idcar", "System.INT32", n_IdCargo.ToString()}
                                      };

            dtListar = xMiFuncion.StoreDTLLenar("est_cargos_consulta1", arrParametros, mysConec);

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
