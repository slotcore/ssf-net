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
    public class CD_est_otrocargoscab
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public int n_idcargogenerado = 0;
        public DataTable dtListar = new DataTable();
        public DataTable dtCargoCab = new DataTable();
        public DataTable dtCargoDet = new DataTable();

        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
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

                if (xMiFuncion.StoreEjecutar("est_otrocargoscab_delete", arrParametros, mysConec) == true)
                {
                    arrParametros[0, 0] = "n_idcar";

                    if (xMiFuncion.StoreEjecutar("est_otrocargosdet_delete", arrParametros, mysConec) == false)
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
        public bool Insertar(BE_EST_OTROCARGOSCAB e_CargosCabecera, List<BE_EST_OTROCARGOSDET> l_CargosDetalle)
        {
            int n_row = 0;
            int n_idgen = 0;
            MySqlTransaction trans;
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("est_otrocargoscab_insertar", e_CargosCabecera, mysConec, 1) == true)
                {
                    n_idgen = Convert.ToInt32(xMiFuncion.intIdGenerado);
                    for (n_row = 0; n_row <= l_CargosDetalle.Count - 1; n_row++)
                    {
                        l_CargosDetalle[n_row].n_idcar = n_idgen;
                        if (xMiFuncion.StoreEjecutar("est_otrocargoscabdet_insertar", l_CargosDetalle[n_row], mysConec, 2) == true)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }
                    n_idcargogenerado = n_idgen;
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
        public bool Insertar(BE_EST_OTROCARGOSCAB e_CargosCabecera, List<BE_EST_OTROCARGOSDET> l_CargosDetalle, int DeDonde)
        {
            int n_row = 0;
            int n_idgen = 0;
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            try
            {
                if (xMiFuncion.StoreEjecutar("est_otrocargoscab_insertar", e_CargosCabecera, mysConec, 1) == true)
                {
                    n_idgen = Convert.ToInt32(xMiFuncion.intIdGenerado);
                    n_idcargogenerado = n_idgen;
                    for (n_row = 0; n_row <= l_CargosDetalle.Count - 1; n_row++)
                    {
                        l_CargosDetalle[n_row].n_idcar = n_idgen;
                        if (xMiFuncion.StoreEjecutar("est_otrocargoscabdet_insertar", l_CargosDetalle[n_row], mysConec, null) == true)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            return booOk;
                        }
                    }
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    return booOk;
                }
                booOk = true;
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = true;
                c_ErrorMensaje = exc.Message.ToString();
                n_ErrorNumber = exc.HResult;
                return booOk;
            }
        }
        public bool Actualizar(BE_EST_OTROCARGOSCAB e_CargosCabecera, List<BE_EST_OTROCARGOSDET> l_CargosDetalle)
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
                                            {"n_id", "System.INT32", e_CargosCabecera.n_id.ToString()}
                                      };

                if (xMiFuncion.StoreEjecutar("est_otrocargoscab_delete", arrParametros, mysConec) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }

                if (xMiFuncion.StoreEjecutar("est_otrocargoscab_actualizar", e_CargosCabecera, mysConec, null) == true)
                {
                    for (n_row = 0; n_row <= l_CargosDetalle.Count - 1; n_row++)
                    {
                        if (xMiFuncion.StoreEjecutar("est_otrocargosdet_insertar", l_CargosDetalle[n_row], mysConec, 1) == false)
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
        public bool ActualizarDocVenta(int n_IdCargo, int n_IdDocumentoPago, string c_FechaPago)
        {
            bool b_Result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"n_id", "System.INT16", n_IdCargo.ToString()},
                                            {"n_iddocpag", "System.INT16", n_IdDocumentoPago.ToString()},
                                            {"c_fchpag", "System.STRING", c_FechaPago.ToString().Substring(0,10)}
                                      };

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (xMiFuncion.StoreEjecutar("est_otrocargoscab_actualizar_a_pagado", arrParametros, mysConec) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_Result;
            }
            b_Result = true;
            return b_Result;
        }
    }
}
