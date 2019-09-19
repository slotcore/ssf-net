using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Gestion;

namespace SIAC_DATOS.Gestion
{
    public class CD_ges_planproduccion
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();

        //private List<BE_GES_PLANPRODUCCIONDET> l_Detalle;
        //private BE_GES_PLANPRODUCCION e_Cabecera;
        public DataTable dtLista;
        public DataTable dtLista2;
        public DataTable dtCabecera;
        public DataTable dtProducto;
        public DataTable dtIntermedio;
        public DataTable dtTodo;

        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT16",n_IdEmpresa.ToString()}
                                            //{"n_idano", "System.INT16",n_AnoTrabajo.ToString()},
                                            //{"n_idmes", "System.INT16",n_MesTrabajo.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("ges_planproduccion_select", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idreg", "System.INT16", n_IdRegistro.ToString()}
                                      };
            string[,] arrParametros2 = new string[2, 3] {
                                            {"n_idpla", "System.INT16", n_IdRegistro.ToString()},
                                            {"n_tipexi", "System.INT16", "1"}
                                      };
            try
            {
                dtCabecera = xMiFuncion.StoreDTLLenar("ges_planproduccion_obtenerregistro", arrParametros, mysConec);
                dtProducto = xMiFuncion.StoreDTLLenar("ges_planproducciondet_obtenerregistroprod", arrParametros2, mysConec);

                arrParametros2[1, 2] = "2";
                dtIntermedio = xMiFuncion.StoreDTLLenar("ges_planproducciondet_obtenerregistroprod", arrParametros2, mysConec);

                arrParametros2[1, 2] = "0";
                dtTodo = xMiFuncion.StoreDTLLenar("ges_planproducciondet_obtenerregistroprod", arrParametros2, mysConec);
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return;
            }
        }
        public bool Insertar(BE_GES_PLANPRODUCCION e_Cabecera, List<BE_GES_PLANPRODUCCIONDET> l_Detalle)
        {
            int n_fila = 0;
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("ges_planproduccion_insertar", e_Cabecera, mysConec, 1) == true)
                {
                    for (n_fila = 0; n_fila <= l_Detalle.Count - 1; n_fila++)
                    {
                        // AGREGAMOS LOS ITEMS
                        l_Detalle[n_fila].n_idplapro = Convert.ToInt16(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("ges_planproducciondet_insertar", l_Detalle[n_fila], mysConec, null) == true)
                        {
                            booOk = true;
                        }
                        else
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                        }
                    }
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                }
                trans.Commit();
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return booOk;
            }
        }
        public bool Actualizar(BE_GES_PLANPRODUCCION e_Cabecera, List<BE_GES_PLANPRODUCCIONDET> l_Detalle)
        {
            int n_fila = 0;
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();
            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idplan", "System.INT16", e_Cabecera.n_id.ToString()}
                                       };
            try
            {
                // ELIMINAMOS EL DETALLE
                if (xMiFuncion.StoreEjecutar("ges_planproducciondet_delete", arrParametros2, mysConec) == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }

                if (xMiFuncion.StoreEjecutar("ges_planproduccion_actualizar", e_Cabecera, mysConec, null) == true)
                {
                    for (n_fila = 0; n_fila <= l_Detalle.Count - 1; n_fila++)
                    {
                        // AGREGAMOS LOS ITEMS
                        l_Detalle[n_fila].n_idplapro = e_Cabecera.n_id;
                        if (xMiFuncion.StoreEjecutar("ges_planproducciondet_insertar", l_Detalle[n_fila], mysConec, null) == true)
                        {
                            booOk = true;
                        }
                        else
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }
                trans.Commit();
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return booOk;
            }
        }
        public bool Eliminar(int n_IdItem)
        {
            bool booResult = false;
            MySqlTransaction trans;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idpla", "System.INT16", n_IdItem.ToString()}
                                      };
            trans = mysConec.BeginTransaction();

            try
            {
                booResult = xMiFuncion.StoreEjecutar("ges_planproduccion_delete", arrParametros, mysConec);
                if (booResult == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
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
        public void Consulta1(int n_IdEmpresa)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()}
                                      };
            try
            {
                dtLista = xMiFuncion.StoreDTLLenar("ges_planproduccion_consulta1", arrParametros, mysConec);
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return;
            }
        }
        public void CambiarEstadoPlanProduccion(int n_IdPlanVenta, int n_Estado)
        {
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idplapro", "System.INT16", n_IdPlanVenta.ToString()},
                                            {"n_estado", "System.INT16", n_Estado.ToString()}
                                      };

            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();
            try
            {
                if (xMiFuncion.StoreEjecutar("ges_planproduccion_cambiarestado", arrParametros, mysConec, null) == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return;
                }
                trans.Commit();
                return;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return;
            }
        }
        public void ListarActivos(int n_IdEmpresa)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()}
                                      };
            try
            {
                dtLista = xMiFuncion.StoreDTLLenar("ges_planproduccion_listaractivos", arrParametros, mysConec);
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return;
            }
        }
        public void PlanProduccionUnificado()
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_tippro", "System.INT16", "2"}
                                      };
            try
            {
                dtLista = xMiFuncion.StoreDTLLenar("ges_planproduccion_unificado", arrParametros, mysConec);
                if (xMiFuncion.booOcurrioError == true)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    return;
                }

                arrParametros[0, 2] = "1";                                                                           // PRODUCTOS INTERMEDIOS
                dtLista2 = xMiFuncion.StoreDTLLenar("ges_planproduccion_unificado", arrParametros, mysConec);
                if (xMiFuncion.booOcurrioError == true)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    return;
                }
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return;
            }
        }
    }
}
