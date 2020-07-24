using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Almacen;
using SIAC_DATOS.Sunat;

namespace SIAC_DATOS.Almacen
{
    public class CD_alm_transferencias
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtMovimiento = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();

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
        public BE_ALM_TRANSFERENCIAS TraerRegistro(int n_IdRegistro)
        {
            BE_ALM_TRANSFERENCIAS EntAlmacenes = new BE_ALM_TRANSFERENCIAS();
            DataTable DtResultado = new DataTable();
            DataTable DtDetalle = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("alm_transferencias_obtenerregistro", arrParametros, mysConec);
 
            if (DtResultado.Rows.Count != 0)
            {
                EntAlmacenes.n_idemp = Convert.ToInt32(DtResultado.Rows[0]["n_idemp"].ToString());
                EntAlmacenes.n_id = Convert.ToInt32(DtResultado.Rows[0]["n_id"].ToString());
                EntAlmacenes.d_fchdoc = Convert.ToDateTime(DtResultado.Rows[0]["d_fchdoc"].ToString());
                EntAlmacenes.d_fching = Convert.ToDateTime(DtResultado.Rows[0]["d_fching"].ToString());
                EntAlmacenes.c_numser = DtResultado.Rows[0]["c_numser"].ToString();
                EntAlmacenes.c_numdoc = DtResultado.Rows[0]["c_numdoc"].ToString();
                EntAlmacenes.n_idalmorig = Convert.ToInt32(DtResultado.Rows[0]["n_idalmorig"].ToString());
                EntAlmacenes.n_idalmdest = Convert.ToInt32(DtResultado.Rows[0]["n_idalmdest"].ToString());
                EntAlmacenes.n_anotra = Convert.ToInt32(DtResultado.Rows[0]["n_anotra"].ToString());
                EntAlmacenes.n_idmes = Convert.ToInt32(DtResultado.Rows[0]["n_idmes"].ToString());
                EntAlmacenes.c_obs = DtResultado.Rows[0]["c_obs"].ToString();
                EntAlmacenes.n_idresp = Convert.ToInt32(DtResultado.Rows[0]["n_idresp"].ToString());
                EntAlmacenes.c_alm_origdes = DtResultado.Rows[0]["c_alm_origdes"].ToString();
                EntAlmacenes.c_alm_destdes = DtResultado.Rows[0]["c_alm_destdes"].ToString();
                EntAlmacenes.c_numdocvis = DtResultado.Rows[0]["c_numdocvis"].ToString();
                EntAlmacenes.c_respdes = DtResultado.Rows[0]["c_respdes"].ToString();

                string[,] arrParametrosDet = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
                DtDetalle = xMiFuncion.StoreDTLLenar("alm_transferenciasdet_listar", arrParametrosDet, mysConec);
                if (DtDetalle.Rows.Count != 0)
                {
                    foreach (DataRow dr in DtDetalle.Rows)
                    {
                        BE_ALM_TRANSFERENCIASDET Detalle = new BE_ALM_TRANSFERENCIASDET();

                        Detalle.n_idtrans = Convert.ToInt16(dr["n_idtrans"].ToString());
                        Detalle.n_idite = Convert.ToInt16(dr["n_idite"].ToString());
                        Detalle.n_idpre = Convert.ToInt16(dr["n_idpre"].ToString());
                        Detalle.n_can = Convert.ToDouble(dr["n_can"].ToString());
                        Detalle.n_preuni = Convert.ToDouble(dr["n_preuni"]);
                        Detalle.n_pretot = Convert.ToDouble(dr["n_pretot"]);
                        Detalle.n_idalm = Convert.ToInt16(dr["n_idalm"].ToString());
                        Detalle.c_numlot = dr["c_numlot"].ToString();
                        Detalle.c_itedes = dr["c_itedes"].ToString();
                        Detalle.c_itepredes = dr["c_itepredes"].ToString();
                        Detalle.c_tipexides = dr["c_tipexides"].ToString();
                        //Detalle.d_fchpro = Convert.ToDateTime(dr["d_fchpro"]);
                        //Detalle.d_fchven = Convert.ToDateTime(dr["d_fchven"]);
                        //Detalle.n_iddep = Convert.ToInt16(dr["n_iddep"].ToString());
                        //Detalle.n_idpro = Convert.ToInt16(dr["n_idpro"].ToString());
                        //Detalle.n_iddis = Convert.ToInt16(dr["n_iddis"].ToString());
                        Detalle.c_desori = dr["c_desori"].ToString();
                        Detalle.c_marca = dr["c_marca"].ToString();
                        //Detalle.h_horing = dr["h_horing"].ToString();
                        Detalle.h_horsal = dr["h_horsal"].ToString();
                        Detalle.n_estpro = Convert.ToInt16(dr["n_estpro"]);
                        EntAlmacenes.lst_items.Add(Detalle);
                    }
                }

            }
            return EntAlmacenes;
        }

        public bool Insertar(BE_ALM_TRANSFERENCIAS entCabecera)
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
                    foreach (BE_ALM_TRANSFERENCIASDET entTransDet in entCabecera.lst_items)
                    {
                        entTransDet.n_idtrans = Convert.ToInt16(xMiFuncion.intIdGenerado);
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
                    //Movimientos de almacen
                    if (InsertarMovimiento(entCabecera, 1))
                    {
                        if (InsertarMovimiento(entCabecera, 2))
                        {
                            booOk = true;
                        }
                        else
                        {
                            trans.Rollback();
                            booOk = false;
                            return booOk;
                        }
                    }
                    else
                    {
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


        public bool InsertarMovimiento(BE_ALM_TRANSFERENCIAS entTransferencia, int n_TipoMovimiento)
        {
            bool booOk = false;
            BE_ALM_MOVIMIENTOS entCabecera = new BE_ALM_MOVIMIENTOS();
            List<BE_ALM_MOVIMIENTOSDET> lstDetalle = new List<BE_ALM_MOVIMIENTOSDET>();
            List<BE_ALM_INVENTARIOLOTE> lstLote = new List<BE_ALM_INVENTARIOLOTE>();

            CD_alm_movimientos miFun = new CD_alm_movimientos();
            miFun.mysConec = mysConec;

            int n_idtipdocingmov = 49;
            int n_idtipdocsalmov = 50;
            int n_idtipdocrefmov = 95;
            int n_idtipopeingmov = 21;
            int n_idtipopesalmov = 11;

            CD_sun_tipdoccom miFun_sun_tipdoccom = new CD_sun_tipdoccom();
            miFun_sun_tipdoccom.mysConec = mysConec;
            string c_numdocmov = string.Empty;

            if (n_TipoMovimiento == 1)
            {
                c_numdocmov = miFun_sun_tipdoccom.UltimoNumero(entTransferencia.n_idemp
                    , n_idtipdocingmov
                    , entTransferencia.c_numser);
            }
            else
            {
                c_numdocmov = miFun_sun_tipdoccom.UltimoNumero(entTransferencia.n_idemp
                    , n_idtipopesalmov
                    , entTransferencia.c_numser);
            }

            entCabecera.n_id = entTransferencia.n_id;
            entCabecera.n_idemp = entTransferencia.n_idemp;
            entCabecera.n_idtipmov = n_TipoMovimiento;
            entCabecera.n_idclipro = 7032;
            entCabecera.d_fchdoc = entTransferencia.d_fchdoc;
            entCabecera.d_fching = entTransferencia.d_fching;
            if (n_TipoMovimiento == 1)
            {
                entCabecera.n_idtipdoc = n_idtipdocingmov;
            }
            else
            {
                entCabecera.n_idtipdoc = n_idtipdocsalmov;
            }
            entCabecera.c_numser = entTransferencia.c_numser;
            entCabecera.c_numdoc = c_numdocmov;
            if (n_TipoMovimiento == 1)
            {
                entCabecera.n_idalm = entTransferencia.n_idalmdest;
            }
            else
            {
                entCabecera.n_idalm = entTransferencia.n_idalmorig;
            }
            entCabecera.n_anotra = entTransferencia.n_anotra;
            entCabecera.n_idmes = entTransferencia.n_idmes;
            entCabecera.c_obs = entTransferencia.c_obs;
            if (n_TipoMovimiento == 1)
            {
                entCabecera.n_idtipope = n_idtipopeingmov;
            }
            else
            {
                entCabecera.n_idtipope = n_idtipopesalmov;
            }
            entCabecera.n_tipite = 2;
            entCabecera.n_docrefidtipdoc = n_idtipdocrefmov;
            entCabecera.c_docrefnumser = entTransferencia.c_numser;
            entCabecera.c_docrefnumdoc = entTransferencia.c_numdoc;
            entCabecera.n_docrefiddocref = entTransferencia.n_id;
            entCabecera.n_perid = entTransferencia.n_idresp;

            foreach (BE_ALM_TRANSFERENCIASDET element in entTransferencia.lst_items)
            {
                BE_ALM_MOVIMIENTOSDET_CONSULTA entNewDetalle = new BE_ALM_MOVIMIENTOSDET_CONSULTA();
                BE_ALM_INVENTARIOLOTE entNewLote = new BE_ALM_INVENTARIOLOTE();

                entNewDetalle.n_idmov = entTransferencia.n_id;
                entNewDetalle.n_idite = element.n_idite;
                entNewDetalle.n_idpre = element.n_idpre;
                entNewDetalle.n_can = element.n_can;
                entNewDetalle.n_idalm = element.n_idalm;
                entNewDetalle.c_numlot = element.c_numlot;
                entNewDetalle.n_idtippro = element.n_idtippro;
                entNewDetalle.d_fchpro = null;
                entNewDetalle.d_fchven = null;
                entNewDetalle.n_iddep = element.n_iddep;
                entNewDetalle.n_idpro = element.n_idpro;
                entNewDetalle.n_iddis = element.n_iddis;
                entNewDetalle.c_desori = element.c_desori;
                entNewDetalle.c_marca = element.c_marca;
                entNewDetalle.n_preuni = element.n_preuni;
                entNewDetalle.n_pretot = element.n_pretot;
                entNewDetalle.h_horing = "";
                entNewDetalle.h_horsal = element.h_horsal;
                entNewDetalle.n_estpro = 1;
                lstDetalle.Add(entNewDetalle);

                // AGREGAMOS LOS LOTES
                entNewLote.n_idemp = entTransferencia.n_idemp;
                entNewLote.n_idite = element.n_idite;
                entNewLote.n_iddocmov = 0;
                entNewLote.d_fchmov = entTransferencia.d_fching;
                entNewLote.c_numlot = element.c_numlot;
                entNewLote.n_idunimed = 0;

                entNewLote.n_caning = 0;
                entNewLote.n_cansal = 0;
                if (entCabecera.n_idtipmov == 1) { entNewLote.n_caning = element.n_can; }
                if (entCabecera.n_idtipmov == 2) { entNewLote.n_cansal = element.n_can; }
                entNewDetalle.d_fchven = null;
                entNewLote.n_iddep = element.n_iddep;
                entNewLote.n_idpro = element.n_idpro;
                entNewLote.n_iddis = element.n_iddis;
                entNewLote.c_oriite = element.c_desori;
                entNewLote.h_horing = "";
                entNewLote.h_horsal = element.h_horsal;
                entNewLote.n_estpro = element.n_estpro;
                lstLote.Add(entNewLote);
            }
            if (miFun.Insertar_off(entCabecera, lstDetalle, lstLote) == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return booOk;
            }

            booOk = true;
            return booOk;
        }

        public bool Actualizar(BE_ALM_TRANSFERENCIAS entCabecera)
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
                    foreach (BE_ALM_TRANSFERENCIASDET entDet in entCabecera.lst_items)
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
