using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Contabilidad;
using SIAC_DATOS.Sunat;

namespace SIAC_DATOS.Contabilidad
{
    public class CD_con_costoproduccion
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtMovimiento = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();

        private BE_CON_COSTOPRODUCCION CargarCabecera(DataRow drCab)
        {
            BE_CON_COSTOPRODUCCION entCab = new BE_CON_COSTOPRODUCCION();

            entCab.n_id = Convert.ToInt32(drCab["n_id"].ToString());
            entCab.n_idemp = Convert.ToInt32(drCab["n_idemp"].ToString());
            entCab.n_anotra = Convert.ToInt32(drCab["n_ano"].ToString());
            entCab.n_idmes = Convert.ToInt32(drCab["n_mes"].ToString());
            entCab.n_idconfigval = Convert.ToInt32(drCab["n_idconfigval"].ToString());
            entCab.n_idresp = Convert.ToInt32(drCab["n_idresp"].ToString());
            entCab.c_numser = drCab["c_numser"].ToString();
            entCab.c_numdoc = drCab["c_numdoc"].ToString();
            entCab.c_obs = drCab["c_obs"].ToString();
            entCab.n_costomod = Convert.ToDouble(drCab["n_costomod"].ToString());
            entCab.n_costoCif = Convert.ToDouble(drCab["n_costoCif"].ToString());
            entCab.c_desconfigval = drCab["c_desconfigval"].ToString();
            entCab.c_desresp = drCab["c_desresp"].ToString();

            return entCab;
        }

        private BE_CON_COSTOPRODUCCIONDET CargarDetalle(DataRow drDet)
        {
            BE_CON_COSTOPRODUCCIONDET entDet = new BE_CON_COSTOPRODUCCIONDET();

            entDet.n_id = Convert.ToInt32(drDet["n_id"].ToString());
            entDet.n_idcostoprod = Convert.ToInt32(drDet["n_idcostoprod"].ToString());
            entDet.n_idparteprod = Convert.ToInt32(drDet["n_idparteprod"].ToString());
            entDet.n_idmov = Convert.ToInt32(drDet["n_idmov"].ToString());
            entDet.n_factdist = Convert.ToDouble(drDet["n_factdist"].ToString());
            entDet.n_costomp = Convert.ToDouble(drDet["n_costomp"]);
            entDet.n_costomod = Convert.ToDouble(drDet["n_costomod"]);
            entDet.n_costocif = Convert.ToDouble(drDet["n_costocif"].ToString());
            entDet.n_can = Convert.ToDouble(drDet["n_cantidad"].ToString());
            entDet.n_idprod = Convert.ToInt32(drDet["n_idprod"].ToString());
            entDet.c_desparteprod = drDet["c_desparteprod"].ToString();
            entDet.n_idrec = Convert.ToInt32(drDet["n_idrec"].ToString());
            entDet.c_desprod = drDet["c_desprod"].ToString();
            entDet.c_desrec = drDet["c_desrec"].ToString();
            entDet.c_desunimed = drDet["c_desunimed"].ToString();

            return entDet;
        }

        public DataTable Listar(int n_idempresa, int n_idmes, int n_idano)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16", n_idempresa.ToString()},
                                            {"n_idmes", "System.INT16", n_idmes.ToString()},
                                            {"n_anotra", "System.INT16", n_idano.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_transferencias_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }

        public List<BE_CON_COSTOPRODUCCIONDET> ListarPartesdeProduccion(int n_idempresa, int n_idmes, int n_idano)
        {
            DataTable DtResultado = new DataTable();
            List<BE_CON_COSTOPRODUCCIONDET> lst_items = new List<BE_CON_COSTOPRODUCCIONDET>();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16", n_idempresa.ToString()},
                                            {"n_idmes", "System.INT16", n_idmes.ToString()},
                                            {"n_anotra", "System.INT16", n_idano.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_transferencias_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                throw new Exception(xMiFuncion.StrErrorMensaje);
            }

            if (DtResultado != null)
            {
                foreach (DataRow drDet in DtResultado.Rows)
                {
                    BE_CON_COSTOPRODUCCIONDET Detalle = new BE_CON_COSTOPRODUCCIONDET();
                    Detalle = CargarDetalle(drDet);
                    lst_items.Add(Detalle);
                }
            }

            return lst_items;
        }

        public BE_CON_COSTOPRODUCCION TraerRegistro(int n_IdRegistro)
        {
            BE_CON_COSTOPRODUCCION entCab = new BE_CON_COSTOPRODUCCION();
            DataTable DtResultado = new DataTable();
            DataTable DtDetalle = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("alm_transferencias_obtenerregistro", arrParametros, mysConec);
 
            if (DtResultado.Rows.Count != 0)
            {
                entCab = CargarCabecera(DtResultado.Rows[0]);

                string[,] arrParametrosDet = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
                DtDetalle = xMiFuncion.StoreDTLLenar("alm_transferenciasdet_listar", arrParametrosDet, mysConec);
                if (DtDetalle.Rows.Count != 0)
                {
                    foreach (DataRow dr in DtDetalle.Rows)
                    {
                        BE_CON_COSTOPRODUCCIONDET Detalle = new BE_CON_COSTOPRODUCCIONDET();
                        Detalle = CargarDetalle(dr);
                        entCab.lst_items.Add(Detalle);
                    }
                }

            }
            return entCab;
        }

        public bool Insertar(BE_CON_COSTOPRODUCCION entCabecera)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;
            xMiFuncion.ReAbrirConeccion(mysConec);
            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("alm_transferencias_insertar", entCabecera, mysConec, 0) == true)
                {
                    foreach (BE_CON_COSTOPRODUCCIONDET entTransDet in entCabecera.lst_items)
                    {
                        entTransDet.n_idcostoprod = Convert.ToInt16(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("alm_transferenciasdet_insertar", entTransDet, mysConec, 0) == true)
                        {
                            booOk = true;
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
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    booOk = false;
                }

                if (booOk == true)
                {
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOk = false;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
            }

            return booOk;
        }

        public bool Actualizar(BE_CON_COSTOPRODUCCION entCabecera)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            xMiFuncion.ReAbrirConeccion(mysConec);
            trans = mysConec.BeginTransaction();

            if (xMiFuncion.StoreEjecutar("alm_transferencias_actualizar", entCabecera, mysConec, null) == true)
            {
                string[,] arrParametros = new string[1, 3] {
                                                {"n_id", "System.INT64", entCabecera.n_id.ToString()}
                                          };

                // BORRAMOS EL DETALLE
                if (xMiFuncion.StoreEjecutar("alm_transferenciasdet_delete", arrParametros, mysConec) == true)
                {
                    // SI LOS ITEMS SE ELIMINARON CON EXITO INSERTAMOS LOS NUEVOS ITEMS
                    foreach (BE_CON_COSTOPRODUCCIONDET entDet in entCabecera.lst_items)
                    {
                        //entDet.n_idmov = Convert.ToInt16(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("alm_transferenciasdet_insertar", entDet, mysConec, null) == true)
                        {
                            booOk = true;
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
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Eliminar(int n_IdItem)
        {
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdItem.ToString()}
                                      };
            
            // ELIMINAMOS DETALLES
            booResult = xMiFuncion.StoreEjecutar("alm_transferenciasdet_delete", arrParametros, mysConec);
            // ELIMINAMOS CABECERA
            booResult = xMiFuncion.StoreEjecutar("alm_transferencias_delete", arrParametros, mysConec);

            if (booResult == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booResult;
        }

        public bool DocumentoExiste(int n_IdEmpresa, string c_NumSerie, string c_NumDocumento)
        {
            DataTable DtResultado = new DataTable();
            bool b_result = false;

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()},
                                            {"c_numser", "System.STRING",c_NumSerie.ToString()},
                                            {"c_numdoc", "System.STRING",c_NumDocumento.ToString()}
                                      };

            dtMovimiento = xMiFuncion.StoreDTLLenar("alm_movimientos_existenumerodocumento", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtMovimiento = null;
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
