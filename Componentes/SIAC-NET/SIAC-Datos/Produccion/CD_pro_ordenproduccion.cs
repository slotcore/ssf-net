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
    public class CD_pro_ordenproduccion
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public DataTable dtOrdenProd = new DataTable();
        public DataTable dtOrdenProdDet = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtOrdenProdPendientes  = new DataTable();
        public DataTable dtOrdProdProdtPends = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
       
        public DataTable Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            DataTable dtResult = new DataTable();
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT64",n_AnoTrabajo.ToString()},
                                            {"n_mestra", "System.INT64",n_MesTrabajo.ToString()},
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            dtResult = xMiFuncion.StoreDTLLenar("pro_ordenproduccion_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtOrdenProd = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return dtResult;
        }
        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResult = new DataTable();
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()}
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            dtResult = xMiFuncion.StoreDTLLenar("pro_ordenproduccion_listar2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtOrdenProd = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return dtResult;
        }
        public bool TraeOrdenProduccionProductosPendientes(int n_IdOrdenProduccion)
        {
            bool b_Result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idordpro", "System.INT64", n_IdOrdenProduccion.ToString()},
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            dtOrdProdProdtPends = xMiFuncion.StoreDTLLenar("pro_ordenproduccion_obtenerordprodprodpend", arrParametros, mysConec);

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
        public bool TraeOrdenProduccionPendientes(int n_IdEmpresa, string c_CadenaIN)
        {
            bool b_Result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()},
                                            {"c_cadin", "System.STRING", c_CadenaIN},
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            dtOrdenProdPendientes = xMiFuncion.StoreDTLLenar("pro_ordenproduccion_obtenerordenproduccionpendientes", arrParametros, mysConec);

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
        public bool TraerRegistro(Int64 n_IdRegistro)
        {
            DataTable DtResultado = new DataTable();
            bool booResult = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            dtOrdenProd = xMiFuncion.StoreDTLLenar("pro_ordenproduccion_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber !=0)
            {
                dtOrdenProd = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            { 
                string[,] arrParametros2 = new string[1, 3] {
                                                {"n_idord", "System.INT64", n_IdRegistro.ToString()}
                                          };
                mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
                dtOrdenProdDet = xMiFuncion.StoreDTLLenar("pro_ordenproducciondet_listar", arrParametros2, mysConec);
                if (xMiFuncion.IntErrorNumber != 0)
                {
                    dtOrdenProdDet = null;
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                }
                else
                {
                    booResult = true;
                }
            }
            
            return booResult;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            
            // PARAMETROS PARA ELIMINAR EL DETALLE DE LA ORDEN DE PRODUCCION
            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idord", "System.INT64", n_IdRegistro.ToString()}
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            booResult = xMiFuncion.StoreEjecutar("pro_ordenproducciondet_delete", arrParametros2, mysConec);

            if (booResult == true)                                               // SI EL DETALLE SE ELIMINO CON EXITO, ELIMINAMOS LA CABECERA
            { 
                // PARAMETROS PARA ELIMINAR LA CABECERA DE LA ORDEN DE PRODUCCION
                string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
                mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
                booResult = xMiFuncion.StoreEjecutar("pro_ordenproduccion_delete", arrParametros, mysConec);
            }

            if (booResult == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_PRO_ORDENPRODUCCION entOrdenProduccion, List<BE_PRO_ORDENPRODUCCIONDET> lstOrdenProduccionDetalle)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_fila = 0;

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            // INSERTAMOS LA CABECERA DE LA ORDEN DE PRODUCCION
            if (xMiFuncion.StoreEjecutar("pro_ordenproduccion_insertar", entOrdenProduccion, mysConec, 1) == true)
            {
                for (n_fila = 0; n_fila <= lstOrdenProduccionDetalle.Count - 1; n_fila++)
                {
                    mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
                    // INSERTAMOS EL DETALLE DE LA ORDEN DE PRODUCCION
                    lstOrdenProduccionDetalle[n_fila].n_idord = Convert.ToInt32(xMiFuncion.intIdGenerado);
                    if (xMiFuncion.StoreEjecutar("pro_ordenproducciondet_insertar", lstOrdenProduccionDetalle[n_fila], mysConec, null) == true)
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

            return booOk;
        }
        public bool Actualizar(BE_PRO_ORDENPRODUCCION entOrdenProduccion, List<BE_PRO_ORDENPRODUCCIONDET> lstOrdenProduccionDetalle)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row;
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            if (xMiFuncion.StoreEjecutar("pro_ordenproduccion_actualizar", entOrdenProduccion, mysConec, null) == true)
            {
                mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
                // ELIMINAMOS EL DETALLE DE LA ORDEN DE PRODUCCION
                string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idord", "System.INT64", entOrdenProduccion.n_id.ToString()}
                                      };
                if (xMiFuncion.StoreEjecutar("pro_ordenproducciondet_delete", arrParametros2, mysConec) == true)
                {
                    for (n_row = 0; n_row <= lstOrdenProduccionDetalle.Count - 1; n_row++)
                    {
                        mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
                        // INSERTAMOS EL NUEVO DETALLE
                        lstOrdenProduccionDetalle[n_row].n_idord = entOrdenProduccion.n_id;
                        if (xMiFuncion.StoreEjecutar("pro_ordenproducciondet_insertar", lstOrdenProduccionDetalle[n_row], mysConec, null) == true)
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
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool ActualizarEstadoOrdenProduccion(int n_IdRegistro, int n_IdEstado)
        {
            bool b_result = false;

            string[,] arrParametros = new string[2, 3] {
                                            {"n_id", "System.INT64",n_IdRegistro.ToString()},
                                            {"n_idest", "System.INT64",n_IdEstado.ToString()}
                                      };
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            b_result = xMiFuncion.StoreEjecutar("pro_ordenproduccion_actualizarestado", arrParametros, mysConec);

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
