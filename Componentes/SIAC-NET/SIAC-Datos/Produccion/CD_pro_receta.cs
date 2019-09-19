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
    public class CD_pro_receta
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;

        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtRecCab = new DataTable();
        public DataTable dtRecIns = new DataTable();
        public DataTable dtRecTar = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        //public BE_PRO_RECETA entRecCab = new BE_PRO_RECETA();
        //public List<BE_PRO_RECETAINSUMO> lstRecIns = new List<BE_PRO_RECETAINSUMO>();
        //public List<BE_PRO_RECETATAREA> lstRecTar = new List<BE_PRO_RECETATAREA>();
        // ********************
        // ****  C R U D   ****
        // ********************
        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("pro_receta_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable ListarProductosConReceta(int n_IdEmpresa)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("pro_receta_productosconreceta", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable ListarRecetaLineas(int n_IdEmpresa)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("pro_receta_lineas", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public bool TraerRecetaProducto(Int64 n_IdProducto)
        {
            DataTable DtResultado = new DataTable();
            DataTable DtDetalleIns = new DataTable();
            DataTable DtDetalleTar = new DataTable();
            
            bool booResult = false;
            int n_row = 0;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idpro", "System.INT64", n_IdProducto.ToString()}
                                      };
            // TRAEMOS TODAS LAS RECETAS DEL PRODUCTO
            DtResultado = xMiFuncion.StoreDTLLenar("pro_receta_obtenerrecetaproducto", arrParametros, mysConec);


            string[,] arrParametrosUniMed = new string[1, 3] {
                                            {"n_idrec", "System.INT64", n_IdProducto.ToString()}
                                      };
            dtRecCab.Clear();
            dtRecIns.Clear();
            dtRecIns.Clear();

            for (n_row = 0; n_row <= DtResultado.Rows.Count - 1; n_row++)
            {
                arrParametrosUniMed[0, 2] = DtResultado.Rows[n_row]["n_id"].ToString();

                DtDetalleIns = xMiFuncion.StoreDTLLenar("pro_recetainsumo_listar", arrParametrosUniMed, mysConec);
                DtDetalleTar = xMiFuncion.StoreDTLLenar("pro_recetatarea_listar", arrParametrosUniMed, mysConec);

                if (DtDetalleIns.Rows.Count != 0)
                { 
                    dtRecIns = xFunGen.DataTableAgregarDataTable(DtDetalleIns, dtRecIns, "n_idrec = " + DtDetalleIns.Rows[0]["n_idrec"] + "");
                }
                if (DtDetalleTar.Rows.Count!=0)
                { 
                    dtRecTar = xFunGen.DataTableAgregarDataTable(DtDetalleTar, dtRecTar, "n_idrec = " + DtDetalleTar.Rows[0]["n_idrec"] + "");
                }
            }
            dtRecCab = DtResultado;
            booResult = true;

            return booResult;
        }
        public bool TraerRegistro(Int64 n_IdRegistro)
        {
            DataTable DtResultado = new DataTable();
            DataTable DtDetalleIns = new DataTable();
            DataTable DtDetalleTar = new DataTable();
            bool booResult = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("pro_receta_obtenerregistro", arrParametros, mysConec);

            string[,] arrParametrosUniMed = new string[1, 3] {
                                            {"n_idrec", "System.INT64", n_IdRegistro.ToString()}
                                      };
            DtDetalleIns = xMiFuncion.StoreDTLLenar("pro_recetainsumo_listar", arrParametrosUniMed, mysConec);
            DtDetalleTar = xMiFuncion.StoreDTLLenar("pro_recetatarea_listar", arrParametrosUniMed, mysConec);
            dtRecCab = DtResultado;
            dtRecIns = DtDetalleIns;
            dtRecTar = DtDetalleTar;
            booResult = true;

            return booResult;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;

            // PARAMETROS PARA ELIMINAR LA RECETA
            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
            // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idrec", "System.INT64", n_IdRegistro.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("pro_recetainsumo_delete", arrParametros, mysConec);
            if (booResult ==true)
            { 
                booResult = xMiFuncion.StoreEjecutar("pro_recetatarea_delete", arrParametros, mysConec);
                if (booResult == true)
                {
                    // ELIMINAMOS LA CABECERA DEL REQUERIMIENTO
                    booResult = xMiFuncion.StoreEjecutar("pro_receta_delete", arrParametros2, mysConec);
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

            return booResult;
        }
        public bool Insertar(List<BE_PRO_RECETA> lstReceta, List<BE_PRO_RECETAINSUMO> lstRecetaInsumos, List<BE_PRO_RECETATAREA> lstRecetaTarea)
        {
            bool booOk = false;
            int n_fila = 0;
            int n_filains = 0;
            int n_filatar = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();

            try
            {
                // GRABAMOS LAS RECETAS
                for (n_fila = 0; n_fila <= lstReceta.Count - 1; n_fila++)
                {
                    if (xMiFuncion.StoreEjecutar("pro_receta_insertar", lstReceta[n_fila], mysConec, 1) == true)
                    {
                        // GRABAMOS LOS INSUMOS
                        for (n_filains = 0; n_filains <= lstRecetaInsumos.Count - 1; n_filains++)
                        {
                            if (lstRecetaInsumos[n_filains].n_idrec == lstReceta[n_fila].n_id)
                            {
                                if (xMiFuncion.StoreEjecutar("pro_recetainsumo_insertar", lstRecetaInsumos[n_filains], mysConec, null) == false)
                                {
                                    // CONTROLAR EL ERROR
                                    booOcurrioError = xMiFuncion.booOcurrioError;
                                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                                    trans.Rollback();
                                    booOk = false;
                                    return booOk;
                                }
                            }
                        }

                        // GRABAMOS LAS TAREAS
                        for (n_filatar = 0; n_filatar <= lstRecetaTarea.Count - 1; n_filatar++)
                        {
                            if (lstRecetaTarea[n_filatar].n_idrec == lstReceta[n_fila].n_id)
                            {
                                if (xMiFuncion.StoreEjecutar("pro_recetatarea_insertar", lstRecetaTarea[n_filatar], mysConec, null) == false)
                                {
                                    // CONTROLAR EL ERROR
                                    booOcurrioError = xMiFuncion.booOcurrioError;
                                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                                    trans.Rollback();
                                    booOk = false;
                                    return booOk;
                                }
                            }
                        }
                    }
                    else
                    {
                        // CONTROLAR EL ERROR
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        booOk = false;
                        return booOk;
                    }
                }

                if (booOk == true)
                {
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }

                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return booOk;
            }
        }
        public bool Actualizar(List<BE_PRO_RECETA> lstReceta, List<BE_PRO_RECETAINSUMO> lstRecetaInsumos, List<BE_PRO_RECETATAREA> lstRecetaTarea)
        {
            bool booOk = false;
            int intFila = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;
            int n_fila = 0;
            int n_filains = 0;
            int n_filatar = 0;

            trans = mysConec.BeginTransaction();
            try
            {
                // ACTUALIZAMOS LAS RECETAS
                for (n_fila = 0; n_fila <= lstReceta.Count - 1; n_fila++)
                {
                    if (xMiFuncion.StoreEjecutar("pro_receta_actualizar", lstReceta[n_fila], mysConec, 1) == true)
                    {
                        string[,] arrParametros = new string[1, 3] {
                                                           {"n_idrec", "System.INT64", lstReceta[n_fila].n_id.ToString()}
                                                           };
                        // ELIMINAMOS LOS INSUMOS
                        if (xMiFuncion.StoreEjecutar("pro_recetainsumo_delete", arrParametros, mysConec) == true)
                        {
                            // ELIMINAMOS LAS TAREAS
                            if (xMiFuncion.StoreEjecutar("pro_recetatarea_delete", arrParametros, mysConec) == true)
                            {

                            }
                            else
                            {
                                // CONTROLAR EL ERROR
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                booOk = false;
                                return booOk;
                            }
                        }
                        else
                        {
                            // CONTROLAR EL ERROR
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            booOk = false;
                            return booOk;
                        }
                        
                        //**********************************************************************************************************************
                        // GRABAMOS LOS INSUMOS
                        for (n_filains = 0; n_filains <= lstRecetaInsumos.Count - 1; n_filains++)
                        {
                            if (lstRecetaInsumos[n_filains].n_idrec == lstReceta[n_fila].n_id)
                            {
                                if (xMiFuncion.StoreEjecutar("pro_recetainsumo_insertar", lstRecetaInsumos[n_filains], mysConec, null) == false)
                                {
                                    // CONTROLAR EL ERROR
                                    booOcurrioError = xMiFuncion.booOcurrioError;
                                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                                    trans.Rollback();
                                    booOk = false;
                                    return booOk;
                                }
                            }
                        }

                        // GRABAMOS LAS TAREAS
                        for (n_filatar = 0; n_filatar <= lstRecetaTarea.Count - 1; n_filatar++)
                        {
                            if (lstRecetaTarea[n_filatar].n_idrec == lstReceta[n_fila].n_id)
                            {
                                if (xMiFuncion.StoreEjecutar("pro_recetatarea_insertar", lstRecetaTarea[n_filatar], mysConec, null) == false)
                                {
                                    // CONTROLAR EL ERROR
                                    booOcurrioError = xMiFuncion.booOcurrioError;
                                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                                    trans.Rollback();
                                    booOk = false;
                                    return booOk;
                                }
                            }
                        }
                    }
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
    }
}
