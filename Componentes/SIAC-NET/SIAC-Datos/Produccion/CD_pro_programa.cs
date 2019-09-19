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
    public class CD_pro_programa
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public DataTable dtPrograma = new DataTable();
        public DataTable dtProgramaDet = new DataTable();
        public DataTable dtProgramaDetPro = new DataTable();
        public DataTable dtProgramaDetProCron = new DataTable();
        public DataTable dtConsulta = new DataTable();
        //public DataTable dtProgramaDetProLin = new DataTable();
        //public DataTable dtProgramaDetProLindet = new DataTable();

        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtOrdenProdPendientes = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public bool Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            bool b_Result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT64",n_AnoTrabajo.ToString()},
                                            {"n_mestra", "System.INT64",n_MesTrabajo.ToString()},
                                      };

            dtPrograma = xMiFuncion.StoreDTLLenar("pro_programa_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtPrograma = null;
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
        public bool TraerRegistro(Int64 n_IdRegistro)
        {
            DataTable DtResultado = new DataTable();
            bool booResult = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
            dtPrograma = xMiFuncion.StoreDTLLenar("pro_programa_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtPrograma = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                string[,] arrParametros2 = new string[1, 3] {
                                                {"n_idpro", "System.INT64", n_IdRegistro.ToString()}
                                          };
                dtProgramaDet = xMiFuncion.StoreDTLLenar("pro_programadet_listar", arrParametros2, mysConec);
                if (xMiFuncion.IntErrorNumber != 0)
                {
                    dtProgramaDet = null;
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                }
                else
                {
                    string[,] arrParametros3 = new string[1, 3] {
                                                {"n_idpro", "System.INT64", n_IdRegistro.ToString()}
                                          };
                    dtProgramaDetPro = xMiFuncion.StoreDTLLenar("pro_programadetpro_listar", arrParametros3, mysConec);
                    if (xMiFuncion.IntErrorNumber != 0)
                    {
                        dtProgramaDetPro = null;
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                    }
                    else
                    {
                        dtProgramaDetProCron = xMiFuncion.StoreDTLLenar("pro_programadetprocron_listar", arrParametros2, mysConec);
                        if (xMiFuncion.IntErrorNumber != 0)
                        {
                            dtProgramaDetProCron = null;
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                        }
                        else
                        {
                            booResult = true;
                        }

                        //dtProgramaDetProLin = xMiFuncion.StoreDTLLenar("pro_programadetprolin_listar", arrParametros3, mysConec);
                        //if (xMiFuncion.IntErrorNumber != 0)
                        //{
                        //    dtProgramaDetProLin = null;
                        //    booOcurrioError = xMiFuncion.booOcurrioError;
                        //    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        //    IntErrorNumber = xMiFuncion.IntErrorNumber;
                        //}
                        //else
                        //{
                        //    dtProgramaDetProLindet = xMiFuncion.StoreDTLLenar("pro_programadetprolindet_listar", arrParametros3, mysConec);
                        //    if (xMiFuncion.IntErrorNumber != 0)
                        //    {
                        //        dtProgramaDetProLindet = null;
                        //        booOcurrioError = xMiFuncion.booOcurrioError;
                        //        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        //        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        //    }
                        //    else
                        //    {
                        //        booResult = true;
                        //    }
                        //}    
                    }
                }
            }

            return booResult;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;

            // PARAMETROS PARA ELIMINAR EL DETALLE DEL DETALLE PROGRAMA DE PRODUCCION
            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idpro", "System.INT64", n_IdRegistro.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("pro_programadetprocron_delete", arrParametros2, mysConec);
            if (booResult == true)                                                   // SI EL DETALLE SE ELIMINO CON EXITO, ELIMINAMOS LA CABECERA
            {
                booResult = xMiFuncion.StoreEjecutar("pro_programadetprocron_delete", arrParametros2, mysConec);
                if (booResult == true)                                                   // SI EL DETALLE SE ELIMINO CON EXITO, ELIMINAMOS LA CABECERA
                {
                    booResult = xMiFuncion.StoreEjecutar("pro_programadetpro_delete", arrParametros2, mysConec);
                    if (booResult == true)                                               // SI EL DETALLE SE ELIMINO CON EXITO, ELIMINAMOS LA CABECERA
                    {
                        booResult = xMiFuncion.StoreEjecutar("pro_programadet_delete", arrParametros2, mysConec);
                        if (booResult == true)                                           // SI EL DETALLE SE ELIMINO CON EXITO, ELIMINAMOS LA CABECERA
                        {
                            // PARAMETROS PARA ELIMINAR EL PROGRAMA DE PRODUCCION
                            string[,] arrParametros3 = new string[1, 3] {
                                                    {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                              };
                            booResult = xMiFuncion.StoreEjecutar("pro_programa_delete", arrParametros3, mysConec);
                            if (booResult == false)                                           
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
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_PRO_PROGRAMA entProgramaU, List<BE_PRO_PROGRAMADET> lstProgramaDetU, List<BE_PRO_PROGRAMADETPRO> lstProgramaDetProU,
                List<BE_PRO_PROGRAMADETPROCRON> lstProgramaDetProCronU)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_fila = 0;
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);

            try
            {
                // INSERTAMOS LA CABECERA DEL PROGRAMA DE PRODUCCION
                if (xMiFuncion.StoreEjecutar("pro_programa_insertar", entProgramaU, mysConec, 1) == true)
                {
                    entProgramaU.n_id = Convert.ToInt32(xMiFuncion.intIdGenerado);
                    // INSERTAMOS LOS DATOS DE LA TABLA pro_programadet
                    for (n_fila = 0; n_fila <= lstProgramaDetU.Count - 1; n_fila++)
                    {
                        lstProgramaDetU[n_fila].n_idpro = entProgramaU.n_id;
                        if (xMiFuncion.StoreEjecutar("pro_programadet_insertar", lstProgramaDetU[n_fila], mysConec, null) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            //break;
                            return booOk;
                        }
                       // booOk = true;
                    }

                    // INSERTAMOS LOS DATOS DE LA TABLA pro_programadetpro
                    for (n_fila = 0; n_fila <= lstProgramaDetProU.Count - 1; n_fila++)
                    {
                        lstProgramaDetProU[n_fila].n_idpro = entProgramaU.n_id;
                        if (xMiFuncion.StoreEjecutar("pro_programadetpro_insertar", lstProgramaDetProU[n_fila], mysConec, null) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            return booOk;
                            //break;
                        }
                            //booOk = true;
                    }

                    // INSERTAMOS EL CRONOGRAMA DE PRODUCCION
                    for (n_fila = 0; n_fila <= lstProgramaDetProCronU.Count - 1; n_fila++)
                    {
                        lstProgramaDetProCronU[n_fila].n_idpro = entProgramaU.n_id;
                        if (xMiFuncion.StoreEjecutar("pro_programadetprocron_insertar", lstProgramaDetProCronU[n_fila], mysConec, null) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            return booOk;
                        }
                    }
                    

                    //// INSERTAMOS LOS DATOS DE LA TABLA pro_programadetprolin
                    //if (booOk == true)
                    //{
                    //    for (n_fila = 0; n_fila <= lstProgramaDetProLinU.Count - 1; n_fila++)
                    //    {
                    //        lstProgramaDetProLinU[n_fila].n_idpro = entProgramaU.n_id;
                    //        if (xMiFuncion.StoreEjecutar("pro_programadetprolin_insertar", lstProgramaDetProLinU[n_fila], mysConec, null) == false)
                    //        {
                    //            booOcurrioError = xMiFuncion.booOcurrioError;
                    //            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    //            IntErrorNumber = xMiFuncion.IntErrorNumber;
                    //            break;
                    //        }
                    //        booOk = true;
                    //    }
                    //}

                    //// INSERTAMOS LOS DATOS DE LA TABLA pro_programadetprolindet
                    //if (booOk == true)
                    //{
                    //    for (n_fila = 0; n_fila <= lstProgramaDetProLinDetU.Count - 1; n_fila++)
                    //    {
                    //        lstProgramaDetProLinDetU[n_fila].n_idpro = entProgramaU.n_id;
                    //        if (xMiFuncion.StoreEjecutar("pro_programadetprolindet_insertar", lstProgramaDetProLinDetU[n_fila], mysConec, null) == false)
                    //        {
                    //            booOcurrioError = xMiFuncion.booOcurrioError;
                    //            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    //            IntErrorNumber = xMiFuncion.IntErrorNumber;
                    //            break;
                    //        }
                    //        booOk = true;
                    //    }
                    //}
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    return booOk;
            }
            booOk = true;
            trans.Commit();
            return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = true;
                StrErrorMensaje = exc.Message.ToString();
                IntErrorNumber = exc.HResult;
                trans.Rollback();
                return booOk;
            }
        }
        public bool Actualizar(BE_PRO_PROGRAMA entProgramaU, List<BE_PRO_PROGRAMADET> lstProgramaDetU, List<BE_PRO_PROGRAMADETPRO> lstProgramaDetProU,
                List<BE_PRO_PROGRAMADETPROCRON> lstProgramaDetProCronU)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_fila = 0;
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);

            try
            {
                // ELIMINAMOS EL DETALLE DE LA ORDEN DE PRODUCCION
                string[,] arrParametros2 = new string[1, 3] {
                                                {"n_idpro", "System.INT64", entProgramaU.n_id.ToString()}
                                          };
                if (xMiFuncion.StoreEjecutar("pro_programadetprocron_delete", arrParametros2, mysConec) == true)
                {
                    booOk = true;
                    if (xMiFuncion.StoreEjecutar("pro_programadetpro_delete", arrParametros2, mysConec) == true)
                    {
                        booOk = true;
                        if (xMiFuncion.StoreEjecutar("pro_programadet_delete", arrParametros2, mysConec) == false)
                        {
                            booOk = false;
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                        }
                    }
                    else 
                    {
                        booOk = false;
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                    }
                }
                else 
                {
                    booOk = false;
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                }

                if (booOk == true)  //SI NO HA HABIDO ERRORES ASL MOMENTO DE ELIMINAR EL REGISTRO GRABAMOS LOS DATOS
                {
                    // ACTUALIZAMOS LA CABECERA DEL PROGRAMA DE PRODUCCION
                    if (xMiFuncion.StoreEjecutar("pro_programa_actualizar", entProgramaU, mysConec, null) == true)
                    {
                        //entProgramaU.n_id = entProgramaU.n_id;
                        // INSERTAMOS LOS DATOS DE LA TABLA pro_programadet
                        for (n_fila = 0; n_fila <= lstProgramaDetU.Count - 1; n_fila++)
                        {
                            lstProgramaDetU[n_fila].n_idpro = entProgramaU.n_id;
                            if (xMiFuncion.StoreEjecutar("pro_programadet_insertar", lstProgramaDetU[n_fila], mysConec, null) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                break;
                            }
                            booOk = true;
                        }

                        // INSERTAMOS LOS DATOS DE LA TABLA pro_programadetpro
                        if (booOk == true)
                        {
                            for (n_fila = 0; n_fila <= lstProgramaDetProU.Count - 1; n_fila++)
                            {
                                lstProgramaDetProU[n_fila].n_idpro = entProgramaU.n_id;
                                if (xMiFuncion.StoreEjecutar("pro_programadetpro_insertar", lstProgramaDetProU[n_fila], mysConec, null) == false)
                                {
                                    booOcurrioError = xMiFuncion.booOcurrioError;
                                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                                    break;
                                }
                                booOk = true;
                            }
                        }

                        // INSERTAMOS EL CRONOGRAMA DE PRODUCCION
                        if (booOk == true)
                        {
                            for (n_fila = 0; n_fila <= lstProgramaDetProCronU.Count - 1; n_fila++)
                            {
                                lstProgramaDetProCronU[n_fila].n_idpro = entProgramaU.n_id;
                                if (xMiFuncion.StoreEjecutar("pro_programadetprocron_insertar", lstProgramaDetProCronU[n_fila], mysConec, null) == false)
                                {
                                    booOcurrioError = xMiFuncion.booOcurrioError;
                                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                                    break;
                                }
                                booOk = true;
                            }
                        }
                    }
                    else
                    {
                        booOk = false;
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                    }

                }

                trans.Commit();
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = true;
                StrErrorMensaje = exc.Message.ToString();
                IntErrorNumber = exc.HResult;
                trans.Rollback();
                return booOk;
            }



            //bool booOk = false;
            //DatosMySql xMiFuncion = new DatosMySql();
            //int n_row;

            //if (xMiFuncion.StoreEjecutar("pro_programa_actualizar", entProgramaU, mysConec, null) == true)
            //{
            //    #region ELIMINAMOS_TABLAS
            //    // ELIMINAMOS EL DETALLE DEL DETALLE DEL PROGRAMA DE PRODUCCION
            //    string[,] arrParametros2 = new string[1, 3] {
            //                                {"n_idpro", "System.INT64", entProgramaU.n_id.ToString()}
            //                          };

            //    // ELIMINAMOS EL DETALLE DE LAS LINEA DEL PROGRAMA DE PRODUCCION
            //    if (xMiFuncion.StoreEjecutar("pro_programadetprolindet_delete", arrParametros2, mysConec) == false)
            //    {
            //        booOcurrioError = xMiFuncion.booOcurrioError;
            //        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
            //        IntErrorNumber = xMiFuncion.IntErrorNumber;
            //        return booOk;
            //    }

            //    // ELIMINAMOS EL DETALLE DE LAS LINEA DEL PROGRAMA DE PRODUCCION
            //    if (xMiFuncion.StoreEjecutar("pro_programadetprolin_delete", arrParametros2, mysConec) == false)
            //    {
            //        booOcurrioError = xMiFuncion.booOcurrioError;
            //        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
            //        IntErrorNumber = xMiFuncion.IntErrorNumber;
            //        return booOk;
            //    }

            //    if (xMiFuncion.StoreEjecutar("pro_programadetpro_delete", arrParametros2, mysConec) == false)
            //    {
            //        booOcurrioError = xMiFuncion.booOcurrioError;
            //        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
            //        IntErrorNumber = xMiFuncion.IntErrorNumber;
            //        return booOk;
            //    }

            //    // ELIMINAMOS EL DETALLE DEL PROGRAMA DE PRODUCCION
            //    if (xMiFuncion.StoreEjecutar("pro_programadet_delete", arrParametros2, mysConec) == false)
            //    {
            //        booOcurrioError = xMiFuncion.booOcurrioError;
            //        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
            //        IntErrorNumber = xMiFuncion.IntErrorNumber;
            //        return booOk;
            //    }
            //    #endregion ELIMINAMOS_TABLAS
            //    // **************************************************************************************
            //    // CUANDO SE HAYAN ELIMINADO TODOS LOS DETALLES INSERTAMOS NUEVAMENTE LOS REGISTROS

            //    for (n_row = 0; n_row <= lstProgramaDetU.Count - 1; n_row++)
            //    {
            //        // INSERTAMOS EL NUEVO DETALLE
            //        lstProgramaDetU[n_row].n_idpro = entProgramaU.n_id;
            //        if (xMiFuncion.StoreEjecutar("pro_programadet_insertar", lstProgramaDetU[n_row], mysConec, null) == false)
            //        {
            //            booOcurrioError = xMiFuncion.booOcurrioError;
            //            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
            //            IntErrorNumber = xMiFuncion.IntErrorNumber;
            //            return booOk;
            //        }
            //    }

            //    // CUANDO SE HAYAN ELIMINADO TODOS LOS DETALLES INSERTAMOS NUEVAMENTE LOS NUEVOS DETALLES
            //    for (n_row = 0; n_row <= lstProgramaDetProU.Count - 1; n_row++)
            //    {
            //        // INSERTAMOS EL NUEVO DETALLE
            //        lstProgramaDetProU[n_row].n_idpro = entProgramaU.n_id;
            //        if (xMiFuncion.StoreEjecutar("pro_programadetpro_insertar", lstProgramaDetProU[n_row], mysConec, null) == false)
            //        {
            //            booOcurrioError = xMiFuncion.booOcurrioError;
            //            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
            //            IntErrorNumber = xMiFuncion.IntErrorNumber;
            //            return booOk;
            //        }
            //    }

            //    // CUANDO SE HAYAN ELIMINADO TODOS LOS DETALLES INSERTAMOS NUEVAMENTE LOS NUEVOS DETALLES DE LAS LINEAS
            //    for (n_row = 0; n_row <= lstProgramaDetProLinU.Count - 1; n_row++)
            //    {
            //        // INSERTAMOS EL NUEVO DETALLE
            //        lstProgramaDetProLinU[n_row].n_idpro = entProgramaU.n_id;
            //        if (xMiFuncion.StoreEjecutar("pro_programadetprolin_insertar", lstProgramaDetProLinU[n_row], mysConec, null) == false)
            //        {
            //            booOcurrioError = xMiFuncion.booOcurrioError;
            //            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
            //            IntErrorNumber = xMiFuncion.IntErrorNumber;
            //            return booOk;
            //        }
            //    }
            //    // CUANDO SE HAYAN ELIMINADO TODOS LOS DETALLES INSERTAMOS NUEVAMENTE LOS NUEVOS DETALLES DE LAS LINEAS
            //    for (n_row = 0; n_row <= lstProgramaDetProLinDetU.Count - 1; n_row++)
            //    {
            //        // INSERTAMOS EL NUEVO DETALLE
            //        lstProgramaDetProLinDetU[n_row].n_idpro = entProgramaU.n_id;
            //        if (xMiFuncion.StoreEjecutar("pro_programadetprodetlin_insertar", lstProgramaDetProLinDetU[n_row], mysConec, null) == false)
            //        {
            //            booOcurrioError = xMiFuncion.booOcurrioError;
            //            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
            //            IntErrorNumber = xMiFuncion.IntErrorNumber;
            //            return booOk;
            //        }
            //    }
            //}
            //else
            //{
            //    booOcurrioError = xMiFuncion.booOcurrioError;
            //    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
            //    IntErrorNumber = xMiFuncion.IntErrorNumber;
            //    return booOk;
            //}
            //booOk = true;
            //return booOk;
        }
        public bool Consulta1(int n_IdEmpresa, int n_IdPrograma)
        {
            bool b_Result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                            {"n_idpro", "System.INT64",n_IdPrograma.ToString()},
                                      };

            dtConsulta = xMiFuncion.StoreDTLLenar("pro_programa_consulta1", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtPrograma = null;
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
        public bool Consulta2(int n_IdEmpresa)
        {
            bool b_Result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                      };

            dtConsulta = xMiFuncion.StoreDTLLenar("pro_programa_consulta2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtPrograma = null;
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
        public bool ActualizarEstadoProductos(int n_idpro, int n_idordpro, int n_idite, string c_fchentrega, int n_estado)
        {
            bool b_result = false;

            string[,] arrParametros = new string[5, 3] {
                                            {"n_idpro", "System.INT32", n_idpro.ToString()},
                                            {"n_idordpro", "System.INT32", n_idordpro.ToString()},
                                            {"n_idite", "System.INT32", n_idite.ToString()},
                                            {"c_fchent", "System.STRING", c_fchentrega.ToString()},
                                            {"n_estado", "System.INT32", n_estado.ToString()},
                                      };

            b_result = xMiFuncion.StoreEjecutar("pro_programadetpro_actualizar1", arrParametros, mysConec);

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
