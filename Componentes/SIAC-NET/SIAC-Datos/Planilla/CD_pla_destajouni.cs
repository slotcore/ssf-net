using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Planillas;

namespace SIAC_DATOS.Planilla
{
    public class CD_pla_destajouni
    {
        public DataTable dtLista;
        public DataTable dtRegistro;

        public bool b_OcurrioError;
        public string c_ErrorMensaje;
        public int n_ErrorNumber;

        public MySqlConnection mysConec;

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public DataTable dtPago = new DataTable();
        public DataTable dtHora = new DataTable();
        public DataTable dtPlanillas = new DataTable();

        public bool Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            bool b_result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT32",n_AnoTrabajo.ToString()},
                                            {"n_mestra", "System.INT32",n_MesTrabajo.ToString()}
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("pla_destajouni_select", arrParametros, mysConec);

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
        public bool TraerRegistro(Int64 n_IdRegistro)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
            dtRegistro = xMiFuncion.StoreDTLLenar("pla_destajouni_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber == 0)
            {
                string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idpla", "System.INT64", n_IdRegistro.ToString()}
                                      };
                // CARGAMOS EL PAGO EN IMPORTE
                dtPago = xMiFuncion.StoreDTLLenar("pla_destajouni_pagoimporte", arrParametros2, mysConec);
                if (xMiFuncion.IntErrorNumber != 0)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    return b_result;
                }

                // CRAGAMOS LAS HORAS TRABAJADAS
                dtHora = xMiFuncion.StoreDTLLenar("pla_destajouni_pagohora", arrParametros2, mysConec);
                if (xMiFuncion.IntErrorNumber != 0)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    return b_result;
                }

                // CRAGAMOS LAS PLANILLAS ORIGINALES PROCESDADAS
                dtPlanillas = xMiFuncion.StoreDTLLenar("pla_destajounipla_listar", arrParametros2, mysConec);
                if (xMiFuncion.IntErrorNumber != 0)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    return b_result;
                }
                
                b_result = true;
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return b_result;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            MySqlTransaction trans;

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            trans = mysConec.BeginTransaction();

            try
            {
                // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
                string[,] arrParametros = new string[1, 3] {
                                                {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                          };

                if (xMiFuncion.StoreEjecutar("pla_destajouni_delete", arrParametros, mysConec) == true)
                {
                    booResult = true;
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booResult;
                }
                trans.Commit();
                return booResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return booResult;
            }
        }
        public bool Insertar(BE_PLA_DESTAJOUNI e_Cargos, string c_ListaPlanilas, List<BE_PLA_DESTAJOUNIPLA> l_CargosPlanillas)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row = 0;
            double n_idgen = 0;
            MySqlTransaction trans;

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("pla_destajouni_insertar", e_Cargos, mysConec, 1) == true)
                {
                    n_idgen = xMiFuncion.intIdGenerado;

                    string[,] arrParametros = new string[2, 3] {
                                                {"n_idpla", "System.INT32", Convert.ToInt32(n_idgen).ToString()},
                                                {"c_inidpla", "System.STRING", c_ListaPlanilas},
                                          };
                    	
                    if (xMiFuncion.StoreEjecutar("pla_destajounidet_insertar", arrParametros, mysConec) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }

                    for (n_row = 0; n_row <= l_CargosPlanillas.Count - 1; n_row++)
                    {
                        l_CargosPlanillas[n_row].n_idpla = Convert.ToInt32(n_idgen);
                        string[,] arrParametros2 = new string[2, 3] {
                                                    {"n_idpla", "System.INT32", l_CargosPlanillas[n_row].n_idpla.ToString()},
                                                    {"n_idplaori", "System.INT32", l_CargosPlanillas[n_row].n_idplaori.ToString()},
                                              };

                        if (xMiFuncion.StoreEjecutar("pla_destajounipla_insertar", arrParametros2, mysConec) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }
                    booOk = true;
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
        public bool Actualizar(BE_PLA_DESTAJOUNI e_Cargos, string c_ListaPlanilas, List<BE_PLA_DESTAJOUNIPLA> l_CargosPlanillas)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row = 0;
            double n_idgen = 0;
            MySqlTransaction trans;

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            trans = mysConec.BeginTransaction();
            try
            {
                if (xMiFuncion.StoreEjecutar("pla_destajouni_actualizar", e_Cargos, mysConec, null) == true)
                {
                    // ELIMINAMOS LA PLANILLA CALCULADA
                    string[,] arrParametros2 = new string[1, 3] {
                                                {"n_idpla", "System.INT32", e_Cargos.n_id.ToString()}
                                        };

                    if (xMiFuncion.StoreEjecutar("pla_destajounidet_delete", arrParametros2, mysConec) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }

                    if (xMiFuncion.StoreEjecutar("pla_destajounipla_delete", arrParametros2, mysConec) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }

                    n_idgen = e_Cargos.n_id;

                    string[,] arrParametros = new string[2, 3] {
                                                {"n_idpla", "System.INT32", Convert.ToInt32(n_idgen).ToString()},
                                                {"c_inidpla", "System.STRING", c_ListaPlanilas},
                                          };

                    if (xMiFuncion.StoreEjecutar("pla_destajounidet_insertar", arrParametros, mysConec) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }

                    for (n_row = 0; n_row <= l_CargosPlanillas.Count - 1; n_row++)
                    {
                        l_CargosPlanillas[n_row].n_idpla = Convert.ToInt32(n_idgen);

                        string[,] arrParametros3 = new string[2, 3] {
                                                    {"n_idpla", "System.INT32", l_CargosPlanillas[n_row].n_idpla.ToString()},
                                                    {"n_idplaori", "System.INT32", l_CargosPlanillas[n_row].n_idplaori.ToString()},
                                              };
                        if (xMiFuncion.StoreEjecutar("pla_destajounipla_insertar", arrParametros3, mysConec) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }
                    booOk = true;
                    booOk = true;
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
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
        public bool VerTareasDia(int n_idPlanilla, int n_IdEmpleado, string c_FechaConsulta)
        {
            bool b_result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idpla", "System.INT32",n_idPlanilla.ToString()},
                                            {"n_idper", "System.INT32",n_IdEmpleado.ToString()},
                                            {"c_fchcon", "System.STRING",c_FechaConsulta}
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("pla_destajouni_verdia", arrParametros, mysConec);

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
