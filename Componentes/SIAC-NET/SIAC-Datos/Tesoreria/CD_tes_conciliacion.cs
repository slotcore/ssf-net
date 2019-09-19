using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Tesoreria;

namespace SIAC_DATOS.Tesoreria
{
    public class CD_tes_conciliacion
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public DataTable dtTarea = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable dtLista = new DataTable();
        public DataTable dtListaDet = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        public void Listar(int n_IdEmpresa, int n_AñoTrabajo, int n_MesTrabajo)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT32", n_AñoTrabajo.ToString()},
                                            {"n_mestra", "System.INT32", n_MesTrabajo.ToString()}
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            dtLista = xMiFuncion.StoreDTLLenar("tes_conciliacion_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
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
            dtLista = xMiFuncion.StoreDTLLenar("tes_conciliacion_obtenerregistro", arrParametros, mysConec);

            if (xMiFuncion.booOcurrioError == false)
            {
                string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idcon", "System.INT32", n_IdRegistro.ToString()}
                                      };
                dtListaDet = xMiFuncion.StoreDTLLenar("tes_conciliaciondet_listar", arrParametros2, mysConec);
                if (xMiFuncion.booOcurrioError == true)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    return booResult;
                }
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return booResult;
            }
            booResult = true;

            return booResult;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);

            try
            {
                string[,] arrParametros2 = new string[1, 3] {
                                                    {"n_idcon", "System.INT32", n_IdRegistro.ToString()}
                                              };
                if (xMiFuncion.StoreEjecutar("tes_conciliaciondet_delete", arrParametros2, mysConec) == true)
                {
                    // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
                    string[,] arrParametros = new string[1, 3] {
                                                    {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                              };

                    if (xMiFuncion.StoreEjecutar("tes_conciliacion_delete", arrParametros, mysConec) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booResult;
                    }
                }
                else 
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booResult;
                }

                booResult = true;
                trans.Commit();
                return booResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = true;
                c_ErrorMensaje = exc.Message.ToString();
                n_ErrorNumber = exc.HResult;
                trans.Rollback();
                return booResult;
            }
        }
        public bool Insertar(BE_TES_CONCILIACION e_Conciliacion, List<BE_TES_CONCILIACIONDET> l_ConcialiacionDet)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row = 0;
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);

            try
            {
                if (xMiFuncion.StoreEjecutar("tes_conciliacion_insertar", e_Conciliacion, mysConec, 1) == true)
                {
                    for (n_row = 0; n_row <= l_ConcialiacionDet.Count - 1; n_row++)
                    {
                        l_ConcialiacionDet[n_row].n_idcon = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("tes_conciliaciondet_insertar", l_ConcialiacionDet[n_row], mysConec, null) == false)
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
                b_OcurrioError = true;
                c_ErrorMensaje = exc.Message.ToString();
                n_ErrorNumber = exc.HResult;
                trans.Rollback();
                return booOk;
            }
        }
        public bool Actualizar(BE_TES_CONCILIACION e_Conciliacion, List<BE_TES_CONCILIACIONDET> l_ConcialiacionDet)
        {
            bool booOk = false;
            int n_row = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            trans = mysConec.BeginTransaction();
            

            try
            {
                string[,] arrParametros = new string[1, 3] {
                                                {"n_idcon", "System.INT32", e_Conciliacion.n_id.ToString()}
                                          };

               // booResult = xMiFuncion.StoreEjecutar("tes_conciliacion_delete", arrParametros, mysConec);

                if (xMiFuncion.StoreEjecutar("tes_conciliaciondet_delete", arrParametros, mysConec) == true)
                { 
                    if (xMiFuncion.StoreEjecutar("tes_conciliacion_actualizar", e_Conciliacion, mysConec, null) == true)
                    {
                        for (n_row = 0; n_row <= l_ConcialiacionDet.Count - 1; n_row++)
                        {
                            l_ConcialiacionDet[n_row].n_idcon = e_Conciliacion.n_id;
                            if (xMiFuncion.StoreEjecutar("tes_conciliaciondet_insertar", l_ConcialiacionDet[n_row], mysConec, null) == false)
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
                        trans.Commit();
                        return booOk;
                    }
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Commit();
                    return booOk;
                }
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
        public void TraerParaConciliacion(int n_IdEmpresa, int n_MesTrabajo, int n_IdBanco, int n_AnoTrabajo)
        {
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_idmes", "System.INT32", n_MesTrabajo.ToString()},
                                            {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_idbanco", "System.INT32", n_IdBanco.ToString()}
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            dtLista = xMiFuncion.StoreDTLLenar("tes_conciliacion_traermovimientos", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool TraerPenidentesOtroMes(int n_IdEmpresa, int n_IdCuentaBanco, string c_FechaConsulta)
        {
            bool booResult = false;

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_idcue", "System.INT32", n_IdCuentaBanco.ToString()},
                                            {"c_fchini", "System.STRING", c_FechaConsulta.ToString()}
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            dtLista = xMiFuncion.StoreDTLLenar("tes_conciliacion_pendientesotromes", arrParametros, mysConec);

            if (xMiFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return booResult;
            }
            booResult = true;

            return booResult;
        }
    }
}
