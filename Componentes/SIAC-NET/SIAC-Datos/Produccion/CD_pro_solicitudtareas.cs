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
    public class CD_pro_solicitudtareas
    {
        public  MySqlConnection mysConec;

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public bool b_OcurrioError;
        public string c_ErrorMensaje;
        public int n_ErrorNumber;

        public DataTable dtLista = new DataTable();
        public DataTable dtRegistro = new DataTable();
        public DataTable dtRegistroDet = new DataTable();
        public DataTable dtRegistroDetPer = new DataTable();
        
        public bool Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            bool b_result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_mestra", "System.INT32", n_MesTrabajo.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pro_solicitudtareas_select", arrParametros, mysConec);

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
            dtRegistro = xMiFuncion.StoreDTLLenar("pro_solicitudtareas_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber == 0)
            {
                string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idsol", "System.INT64", n_IdRegistro.ToString()}
                                      };
                dtRegistroDet = xMiFuncion.StoreDTLLenar("pro_solicitudtareascab_select", arrParametros2, mysConec);
                if (xMiFuncion.IntErrorNumber == 0)
                {
                    dtRegistroDetPer = xMiFuncion.StoreDTLLenar("pro_solicitudtareasdet_select", arrParametros2, mysConec);
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
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                }
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
            // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idsol", "System.INT64", n_IdRegistro.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("pro_solicitudtareasdet_delete", arrParametros, mysConec) == true)
            { 
                if (xMiFuncion.StoreEjecutar("pro_solicitudtareascab_delete", arrParametros, mysConec) == true)
                {
                    arrParametros = new string[1, 3] {
                                                {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                          };
                    if (xMiFuncion.StoreEjecutar("pro_solicitudtareas_delete", arrParametros, mysConec) == true)
                    {
                        booResult = true;
                    }
                    else
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    }
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                }
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_PRO_SOLICITUDTAREAS entSolicitud, List<BE_PRO_SOLICITUDTAREASCAB> LstSolicitudCab, List<BE_PRO_SOLICITUDTAREASDET> LstSolicituDet)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row = 0;
            int n_row3 = 0;

            if (xMiFuncion.StoreEjecutar("pro_solicitudtareas_insertar", entSolicitud, mysConec, 1) == true)
            {
                entSolicitud.n_id = Convert.ToInt32(xMiFuncion.intIdGenerado);
                for (n_row = 0; n_row <= LstSolicitudCab.Count - 1; n_row++)
                {
                    LstSolicitudCab[n_row].n_idsol = entSolicitud.n_id;
                    if (xMiFuncion.StoreEjecutar("pro_solicitudtareascab_insertar", LstSolicitudCab[n_row], mysConec, 1) == true)
                    {
                        // INSERTAMOS LAS PERSONAS ASIGNADAS PARA LA TAREA
                        for (n_row3 = 0; n_row3 <= LstSolicituDet.Count - 1; n_row3++)
                        {
                            if (LstSolicituDet[n_row3].n_idsoltar == LstSolicitudCab[n_row].n_idtar)
                            {
                                LstSolicituDet[n_row3].n_idsol = LstSolicitudCab[n_row].n_idsol;
                                if (xMiFuncion.StoreEjecutar("pro_solicitudtareasdet_insertar", LstSolicituDet[n_row3], mysConec, null) == false)
                                {
                                    b_OcurrioError = xMiFuncion.booOcurrioError;
                                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
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
                        break;
                    }
                    booOk = true;
                }
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Actualizar(BE_PRO_SOLICITUDTAREAS entSolicitud, List<BE_PRO_SOLICITUDTAREASCAB> LstSolicitudCab, List<BE_PRO_SOLICITUDTAREASDET> LstSolicituDet)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row = 0;
            int n_row2 = 0;
            int n_row3 = 0;
            bool b_actualizo = false;
            DataTable dtTareas = new DataTable();
            CD_pro_solicitudtareas funProSol = new CD_pro_solicitudtareas();

            xMiFuncion.ReAbrirConeccion(mysConec);
            funProSol.mysConec = mysConec;
            funProSol.TraerRegistro(entSolicitud.n_id);
            dtTareas = funProSol.dtRegistroDet;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idsol", "System.INT64", entSolicitud.n_id.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("pro_solicitudtareasdet_delete", arrParametros, mysConec) == true)
            {
                bool b_econtro = false;
                // ACTUALIZAMOS LA CABECERA DE LA SOLICITUD DE MATERIALES
                if (xMiFuncion.StoreEjecutar("pro_solicitudtareas_actualizar", entSolicitud, mysConec, null) == true)
                {
                    //ELIMINAMOS LAS TAREAS QUE HAYAN SIDO ELIMINADAS EN EL EL FORMULARIO
                    for (n_row = 0; n_row <= dtTareas.Rows.Count - 1; n_row++)
                    {
                        b_econtro = false;
                         for (n_row2 = 0; n_row2 <= LstSolicitudCab.Count - 1; n_row2++)
                         {
                             if (LstSolicitudCab[n_row2].n_idtar == Convert.ToInt32(dtTareas.Rows[n_row]["n_idtar"]))
                             {
                                 b_econtro = true;
                                 break;
                             }
                         }
                         if (b_econtro == false)
                         {
                             string[,] arrParametros3 = new string[2, 3] {
                                            {"n_idsol", "System.INT64", dtTareas.Rows[n_row]["n_idsol"].ToString()},
                                            {"n_idtar", "System.INT64", dtTareas.Rows[n_row]["n_idtar"].ToString()}
                                      };
                             if (xMiFuncion.StoreEjecutar("pro_solicitudtareascab_eliminartarea", arrParametros3, mysConec) == false)
                             {
                                 b_OcurrioError = xMiFuncion.booOcurrioError;
                                 c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                 n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                 return booOk;
                             }
                         }
                    }

                    // INSERTAMOS LAS TAREAS DE LA SOLICITUD DE TAREAS
                    for (n_row = 0; n_row <= LstSolicitudCab.Count - 1; n_row++)
                    {
                        // RECORREMOS LAS TAREAS ORIGINALES PARA VER SI HA OCURRIDO VARIACION
                        b_actualizo = false;                                                        // ESTA VARIABLE ES PARA SABER SI SE ACTUALIZA LA TAREA DE LA LISTA
                        for (n_row2 = 0; n_row2 <= dtTareas.Rows.Count - 1; n_row2++)
                        {
                            if (LstSolicitudCab[n_row].n_id == Convert.ToInt32(dtTareas.Rows[n_row2]["n_id"]))
                            {
                                // SI LOS ID COINCIDEN ACTUALIZAMOS LAS TAREA
                                if (xMiFuncion.StoreEjecutar("pro_solicitudtareascab_actualizar", LstSolicitudCab[n_row], mysConec, null) == true)
                                {
                                    for (n_row3 = 0; n_row3 <= LstSolicituDet.Count - 1; n_row3++)
                                    {
                                        if (LstSolicituDet[n_row3].n_idsoltar == LstSolicitudCab[n_row].n_idtar)
                                        {
                                            LstSolicituDet[n_row3].n_idsol = LstSolicitudCab[n_row].n_idsol;
                                            if (xMiFuncion.StoreEjecutar("pro_solicitudtareasdet_insertar", LstSolicituDet[n_row3], mysConec, null) == false)
                                            {
                                                b_OcurrioError = xMiFuncion.booOcurrioError;
                                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                                return booOk;
                                            }
                                        }
                                    }

                                    b_actualizo = true;
                                    break;
                                }
                                else
                                {
                                    b_OcurrioError = xMiFuncion.booOcurrioError;
                                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                    return booOk;
                                }
                            }
                        }

                        // SI NO SE PUDO ACTUALIZAR LA TAREA ES PORQUE SEGURAMENTE ES UNA TAREA NUEVA HAY QUE INSERTARLA
                        if (b_actualizo == false)
                        {
                            if (xMiFuncion.StoreEjecutar("pro_solicitudtareascab_insertar", LstSolicitudCab[n_row], mysConec, 1) == true)
                            {
                                for (n_row3 = 0; n_row3 <= LstSolicituDet.Count - 1; n_row3++)
                                {
                                    if (LstSolicituDet[n_row3].n_idsoltar == LstSolicitudCab[n_row].n_idtar)
                                    {
                                        LstSolicituDet[n_row3].n_idsol = LstSolicitudCab[n_row].n_idsol;
                                        if (xMiFuncion.StoreEjecutar("pro_solicitudtareasdet_insertar", LstSolicituDet[n_row3], mysConec, null) == false)
                                        {
                                            b_OcurrioError = xMiFuncion.booOcurrioError;
                                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                            return booOk;
                                        }
                                    }
                                }

                                b_actualizo = true;
                                break;
                            }
                        }
                    }

                    booOk = true;
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                }
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booOk;
        }
        public bool BuscarProduccionEnSolicitud(int n_idProduccion, int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idpro", "System.INT32", n_idProduccion.ToString()},
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pro_solicitudtareas_consulta1", arrParametros, mysConec);

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
        public bool MarcarRevisado(int n_IdSolicitudTarea, int n_Estado)
        {
            bool b_Result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idsoltar", "System.STRING", n_IdSolicitudTarea.ToString()},
                                            {"n_estado", "System.STRING", n_Estado.ToString()}
                                      };

            xMiFuncion.StoreEjecutar("pro_produccion_actualizarestadotareaplanilla", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_Result = true;
            }
            return b_Result;
        }
        public bool Consulta1(int n_IdEmpresa, int n_IdTarea)
        {
            bool b_result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_idtar", "System.INT32", n_IdTarea.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pro_solicitudtareas_promediotareas", arrParametros, mysConec);

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
        public bool Consulta3(int n_IdEmpresa, string c_FechaInicio, string c_FechaFinal, string c_CadenaIN)
        {
            bool b_result = false;
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FechaFinal.ToString()},
                                            {"c_cadinpro", "System.STRING", c_CadenaIN.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pro_solicitudtareas_consulta3", arrParametros, mysConec);

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
