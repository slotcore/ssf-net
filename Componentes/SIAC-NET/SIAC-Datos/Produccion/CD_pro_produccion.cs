using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Produccion;
namespace SIAC_DATOS.Produccion
{
    public class CD_pro_produccion
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;

        public DataTable dtListar = new DataTable();
        
        public DataTable dtRegistro = new DataTable();
        public DataTable dtInsumos = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        //public bool ProcesarJornales(string c_FechaInicio, string c_FehaTermino)
        //{ 
        //    bool b_Result = false;
        //    string[,] arrParametros = new string[2, 3] {
        //                                    {"c_fchini", "System.STRING",c_FechaInicio.ToString()},
        //                                    {"c_fchfin", "System.STRING",c_FehaTermino.ToString()}
        //                              };

        //    dtJornales = xMiFuncion.StoreDTLLenar("pro_produccion_jornalsemanal", arrParametros, mysConec);

        //    if (xMiFuncion.IntErrorNumber != 0)
        //    {
        //        dtJornales = null;
        //        booOcurrioError = xMiFuncion.booOcurrioError;
        //        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
        //        IntErrorNumber = xMiFuncion.IntErrorNumber;
        //    }
        //    else
        //    {
        //        b_Result = true;
        //    }
        //    return b_Result;
        //}
        //public bool VerTareasDiaPersonal(int IdEmpresa, int n_IdPersona, string c_DiaTrabajo)
        //{
        //    bool b_Result = false;
        //    string[,] arrParametros = new string[3, 3] {
        //                                    {"n_idemp", "System.INT32", IdEmpresa.ToString()},
        //                                    {"n_idper", "System.INT32", n_IdPersona.ToString()},
        //                                    {"c_diatra", "System.STRING", c_DiaTrabajo.ToString()}
        //                              };

        //    dtTareasDiaPersona = xMiFuncion.StoreDTLLenar("pro_produccion_vertareapersona", arrParametros, mysConec);

        //    if (xMiFuncion.IntErrorNumber != 0)
        //    {
        //        dtJornales = null;
        //        booOcurrioError = xMiFuncion.booOcurrioError;
        //        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
        //        IntErrorNumber = xMiFuncion.IntErrorNumber;
        //    }
        //    else
        //    {
        //        b_Result = true;
        //    }
        //    return b_Result;
        //}
        public bool TraerRegistro(Int64 n_IdRegistro)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };

            dtRegistro = xMiFuncion.StoreDTLLenar("pro_produccion_obtenerregistro", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber == 0)
            {
                string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idpro", "System.INT64", n_IdRegistro.ToString()}
                                      };
                dtInsumos = xMiFuncion.StoreDTLLenar("pro_produccionins_listar", arrParametros2, mysConec);

                if (xMiFuncion.IntErrorNumber == 0)
                {
                    b_result = true;
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                }
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return b_result;
        }
        public bool Listar(int n_IdEmpresa, int n_AnoTra, int n_MesTra)
        {
            bool b_result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT64",n_AnoTra.ToString()},
                                            {"n_mestra", "System.INT64",n_MesTra.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_produccion_listar", arrParametros, mysConec);

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
        //public bool TraerTareasLinea(int n_IdProducto, int n_IdReceta, int n_IdLinea)
        //{ 
        //    bool b_result = false;
        //    string[,] arrParametros = new string[3, 3] {
        //                                    {"n_idpro", "System.INT64",n_IdProducto.ToString()},
        //                                    {"n_idrec", "System.INT64",n_IdReceta.ToString()},
        //                                    {"n_idlin", "System.INT64",n_IdLinea.ToString()}
        //                              };
        //    dtListar = xMiFuncion.StoreDTLLenar("pro_productosrecetaslineastareas_counsulta1", arrParametros, mysConec);

        //    if (xMiFuncion.IntErrorNumber != 0)
        //    {
        //        booOcurrioError = xMiFuncion.booOcurrioError;
        //        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
        //        IntErrorNumber = xMiFuncion.IntErrorNumber;
        //    }
        //    else
        //    {
        //        b_result = true;
        //    }
        //    return b_result;
        //}
        public bool Eliminar(int n_IdProduccion)
        {
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdProduccion.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("pro_produccion_delete", arrParametros, mysConec) == true)
            {
                booResult = true;
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;            
        }
        public bool Insertar(ref BE_PRO_PRODUCCION entProduccion, List<BE_PRO_PRODUCCIONINS> LstInsumos)
        {
            bool booResult = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;
            int n_row = 0;
            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("pro_produccion_insertar", entProduccion, mysConec, 1) == true)
                {
                    entProduccion.n_id = Convert.ToInt32(xMiFuncion.intIdGenerado);
                    for (n_row = 0; n_row <= LstInsumos.Count - 1;n_row++)
                    {
                        BE_PRO_PRODUCCIONINS entProIns = new BE_PRO_PRODUCCIONINS();

                        entProIns = LstInsumos[n_row];
                        entProIns.n_idpro = entProduccion.n_id;
                        if (xMiFuncion.StoreEjecutar("pro_produccionins_insertar", entProIns, mysConec, null) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booResult;
                        }
                        booResult = true;
                    }
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
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
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return booResult;
            }
        }
        public bool Actualizar(BE_PRO_PRODUCCION entProduccion, List<BE_PRO_PRODUCCIONINS> LstInsumos)
        {
            bool booResult = false;
            int n_row = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();
            try
            {
                string[,] arrParametros = new string[1, 3] {
                                            {"n_idpro", "System.INT64",entProduccion.n_id.ToString()}
                                      };
                // BORRAMOS EL DETALLE DE LOS INSUMOS

                if (xMiFuncion.StoreEjecutar("pro_produccionins_delete", arrParametros, mysConec) == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booResult;
                }

                if (xMiFuncion.StoreEjecutar("pro_produccion_actualizar", entProduccion, mysConec, null) == true)
                {
                    for (n_row = 0; n_row <= LstInsumos.Count - 1; n_row++)
                    {
                        BE_PRO_PRODUCCIONINS entProIns = new BE_PRO_PRODUCCIONINS();

                        entProIns = LstInsumos[n_row];
                        entProIns.n_idpro = entProduccion.n_id;
                        if (xMiFuncion.StoreEjecutar("pro_produccionins_insertar", entProIns, mysConec, null) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booResult;
                        }
                        booResult = true;
                    }
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
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
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return booResult;
            }
        }
        public bool Consulta1(int n_IdEmpresa, string c_FechaInicio, string c_FechaFInal, string c_CadInPro, string c_CadInIns)
        {
            bool b_result = false;
            string[,] arrParametros = new string[5, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                            {"c_fchini", "System.STRING",c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING",c_FechaFInal.ToString()},
                                            {"c_cadinpro", "System.STRING",c_CadInPro.ToString()},
                                            {"c_cadinins", "System.STRING",c_CadInIns.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_produccion_consulta1", arrParametros, mysConec);

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
        public bool Consulta4(int n_IdEmpresa, string c_FechaInicio, string c_FechaFInal, string c_CadInPro, string c_CadInIns)
        {
            bool b_result = false;
            string[,] arrParametros = new string[5, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                            {"c_fchini", "System.STRING",c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING",c_FechaFInal.ToString()},
                                            {"c_cadinpro", "System.STRING",c_CadInPro.ToString()},
                                            {"c_cadinins", "System.STRING",c_CadInIns.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_produccion_consulta4", arrParametros, mysConec);

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
        public bool Consulta2(int n_IdEmpresa, int n_AnoTra, int n_MesTra)
        {
            bool b_result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT64",n_AnoTra.ToString()},
                                            {"n_mestra", "System.INT64",n_MesTra.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_produccion_consulta2", arrParametros, mysConec);

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
        public bool ConsultaSolMatPendientes(int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_produccion_pendientes_solmat", arrParametros, mysConec);

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
        public bool ConsultaSolMatProcesadas(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            bool b_result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT64", n_AnoTrabajo.ToString()},
                                            {"n_mestra", "System.INT64", n_MesTrabajo.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_produccion_procesada_solmat", arrParametros, mysConec);

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
        public bool ConsultaTareasPendientes(int n_IdEmpresa)
        {
            // *******************************************************************
            // ESTA FUNCION DEVUELVE LAS PRODUCCIONES QUE NO TIENEN TAREA IMPRESAS

            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_produccion_pendientes_tareas", arrParametros, mysConec);

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
        public bool ConsultaProduccionConTarea(int n_IdEmpresa)
        {
            // *******************************************************************
            // ESTA FUNCION DEVUELVE LAS PRODUCCIONES QUE SI TIENEN TAREA IMPRESAS

            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_produccion_emitidas_tareas", arrParametros, mysConec);

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
        public bool ActualizarEstadoSolicitud(int n_IdProduccion, int n_Estado)
        {
            bool b_Result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idpro", "System.STRING", n_IdProduccion.ToString()},
                                            {"n_estado", "System.STRING", n_Estado.ToString()}
                                      };

            xMiFuncion.StoreEjecutar("pro_produccion_actualizarestadosolicitud", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_Result = true;
            }
            return b_Result;
        }
        public bool ActualizarEstadoTarea(int n_IdProduccion, int n_Estado)
        {
            bool b_Result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idpro", "System.STRING", n_IdProduccion.ToString()},
                                            {"n_idest", "System.STRING", n_Estado.ToString()}
                                      };

            xMiFuncion.StoreEjecutar("pro_produccion_actualizarestadotarea", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_Result = true;
            }
            return b_Result;
        }
        public bool ProductosTerminados(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            bool b_result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT64",n_AnoTrabajo.ToString()},
                                            {"n_mes", "System.INT64",n_MesTrabajo.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_produccion_consulta3", arrParametros, mysConec);

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
        public bool AbrirProduccion(int n_IdProduccion)
        {
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdProduccion.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("pro_produccion_abrirproduccion", arrParametros, mysConec) == true)
            {
                booResult = true;
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult; 
        }

        public bool ConsultaProduccionPendienteLiquidar(int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()},
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_produccion_consulta5", arrParametros, mysConec);

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
        public bool ConsultarAnos(int n_IdEmpresa)
        { 
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()},
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_produccion_mostraraños", arrParametros, mysConec);

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
        public bool Consulta7(int n_IdEmpresa, string c_AnoTrabajo)
        {
            bool b_result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()},
                                            {"c_anotra", "System.STRING", c_AnoTrabajo.ToString()},
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_produccion_consulta6", arrParametros, mysConec);

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
        public bool Consulta9(int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_produccion_consulta9", arrParametros, mysConec);

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
        public bool Consulta10(int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_produccion_consulta10", arrParametros, mysConec);

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
        public bool Consulta12(int n_IdEmpresa, int n_AnoTrabajo, int n_TipoProducto)
        {
            bool b_result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_tipexi", "System.INT32", n_TipoProducto.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_produccion_consulta12", arrParametros, mysConec);

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
    }
}

