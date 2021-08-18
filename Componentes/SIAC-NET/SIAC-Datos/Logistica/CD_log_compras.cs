using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Logistica;
using SIAC_DATOS.Almacen;

namespace SIAC_DATOS.Logistica
{
    public class CD_log_compras
    {
        public bool b_ocurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();       
        public DataTable dtListaDet = new DataTable();
        public DataTable dtListaDoc = new DataTable();
        public DataTable dtRegistro = new DataTable();
        
        public int n_IdGenerado = 0;
        DatosMySql xMiFuncion = new DatosMySql();
        public bool BuscarDocumento(int n_IdProveedor, int n_IdTipoDocumento, string c_NumeroSerie, string c_NumeroDocumento, int n_IdEmpresa)
        { 
            bool b_result = false;

            string[,] arrParametros = new string[5, 3] {
                                            {"n_idpro", "System.INT32",n_IdProveedor.ToString()},
                                            {"c_numser", "System.STRING",c_NumeroSerie.ToString()},
                                            {"c_numdoc", "System.STRING",c_NumeroDocumento.ToString()},
                                            {"n_tipdoc", "System.INT32",n_IdTipoDocumento.ToString()},
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("log_compras_buscarfactura", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            
            return b_result;
        }
        public bool BuscarDocumento(int n_IdProveedor, int n_IdTipoDocumento, string c_NumeroSerie, string c_NumeroDocumento, int n_IdCompra, int n_IdEmpresa)
        {
            bool b_result = false;

            string[,] arrParametros = new string[6, 3] {
                                            {"n_idpro", "System.INT32",n_IdProveedor.ToString()},
                                            {"c_numser", "System.STRING",c_NumeroSerie.ToString()},
                                            {"c_numdoc", "System.STRING",c_NumeroDocumento.ToString()},
                                            {"n_tipdoc", "System.INT32",n_IdTipoDocumento.ToString()},
                                            {"n_idcom", "System.INT32",n_IdCompra.ToString()},
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("log_compras_buscarfactura2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;

            return b_result;
        }
        public bool Listar(int n_idempresa, int n_idmes, int n_idano, int n_IdTipoCompra)
        {
            bool b_result = false;

            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT64",n_idempresa.ToString()},
                                            {"n_anotra", "System.INT64",n_idano.ToString()},
                                            {"n_idmes", "System.INT64",n_idmes.ToString()},
                                            {"n_tipcom", "System.INT64",n_IdTipoCompra.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("log_compras_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            
            return b_result;
        }
        public bool Consulta1(int n_IdEmpresa, int n_IdProveedor)
        {
            bool b_result = false;

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idpro", "System.INT64",n_IdProveedor.ToString()},
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("log_compras_consulta1", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;

            return b_result;
        }
        public bool TraerRegistro(Int64 n_IdRegistro)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()},
                                      };

            dtRegistro = xMiFuncion.StoreDTLLenar("log_compras_obtenerregistro", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            else
            {
                string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idcom", "System.INT64", n_IdRegistro.ToString()},
                                      };

                dtListaDet = xMiFuncion.StoreDTLLenar("log_comprasdet_listar", arrParametros2, mysConec);

                if (xMiFuncion.IntErrorNumber != 0)
                {
                    b_ocurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    return b_result;
                }

                dtListaDoc = xMiFuncion.StoreDTLLenar("log_comprasdoc_listar", arrParametros2, mysConec);

                if (xMiFuncion.IntErrorNumber != 0)
                {
                    b_ocurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    return b_result;
                }
                
                b_result = true;
            }

            return b_result;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool b_result = false;
            MySqlTransaction trans;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idcom", "System.INT64", n_IdRegistro.ToString()}
                                      };

            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("log_comprasdoc_delete", arrParametros2, mysConec) == true)
                {
                    // TRAEMOS LOS DATOS DEL DOCUMENTO QUE SE ESTA ELIMINANDO
                    double n_valorinc = 0;
                    double n_salfac = 0;
                    //BE_VTA_VENTAS e_ventas = new BE_VTA_VENTAS();
                    DataTable dtres = new DataTable();
                    CD_log_compras o_compras = new CD_log_compras();
                    o_compras.mysConec = mysConec;
                    o_compras.TraerRegistro(Convert.ToInt32(n_IdRegistro));
                    dtres = o_compras.dtRegistro;
                    
                    n_valorinc = Convert.ToDouble(dtres.Rows[0]["n_imptotcom"]);                                        // EL IMPORTE ORIGINAL DE LA NOTA DE CREDITO

                    if (Convert.ToInt32(dtres.Rows[0]["n_idtipdoc"]) == 8)                                             //  SI ES NOTA DE CREDITO ACTUALIZAMOS EL SALDO DEL DOCUMENTO A SU SALDO ORIGINAL     
                    {
                        // TRAEMOS EL DOCUMENTO QUE SE NODIFICA
                        int n_iddoccompra = Convert.ToInt32(dtres.Rows[0]["n_iddocmod"]);                                   // OBTENEMOS EL ID DEL DOCUMENTO QUE SE MODIFICA
                        o_compras.TraerRegistro(n_iddoccompra);
                        dtres = o_compras.dtRegistro;
                        if (dtres.Rows.Count > 0)
                        {
                            n_salfac = Convert.ToDouble(dtres.Rows[0]["n_impsal"]) + n_valorinc;

                            string[,] arrParam1 = new string[3, 3] {
                                                {"n_idreg", "System.INT32", n_iddoccompra.ToString()},
                                                {"n_importe", "System.DOUBLE", n_salfac.ToString()},
                                                {"n_tipo", "System.INT32", "1"}
                                          };

                            if (xMiFuncion.StoreEjecutar("log_compras_actualizarsaldo", arrParam1, mysConec) == false)
                            {
                                b_ocurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return b_result;
                            }
                        }

                    }

                    if (xMiFuncion.StoreEjecutar("log_comprasdet_delete", arrParametros2, mysConec) == true)
                    {
                        if (xMiFuncion.StoreEjecutar("log_compras_delete", arrParametros, mysConec) == false)
                        {
                            b_ocurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return b_result;
                        }
                    }
                    else
                    {
                        b_ocurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return b_result;
                    }
                }
                else
                {
                    b_ocurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return b_result;
                }
                b_result = true;
                trans.Commit();
                return b_result;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_ocurrioError = true;
                c_ErrorMensaje = exc.Message.ToString();
                n_ErrorNumber = exc.HResult;
                trans.Rollback();
                return b_result;
            }
        }
        public bool Insertar(BE_LOG_COMPRAS entCabecera, List<BE_LOG_COMPRASDET> lstDetalle, List<BE_LOG_COMPRASDOC> lstDocumentos)
        {
            bool b_result = false;
            MySqlTransaction trans;
            int n_Fila = 0;
            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("log_compras_insertar", entCabecera, mysConec, 0) == true)
                {
                    // ACTUALIZAMOS EL SALDO DE LA FACTURA QUE SE MODIFICA
                    if (entCabecera.n_idtipdoc == 8)
                    {
                        //BE_LOG_COMPRAS e_compras = new BE_LOG_COMPRAS();
                        DataTable dtres = new DataTable();
                        CD_log_compras o_compras = new CD_log_compras();
                        double n_salfac = 0;

                        o_compras.mysConec = mysConec;

                        // TRAEMOS EL DOCUMENTO QUE SE NODIFICA
                        int n_iddocventa = entCabecera.n_iddocmod;                                   // OBTENEMOS EL ID DEL DOCUMENTO QUE SE MODIFICA
                        o_compras.TraerRegistro(n_iddocventa);
                        dtres = o_compras.dtRegistro;
                        n_salfac = Convert.ToDouble(dtres.Rows[0]["n_impsal"]);
                        n_salfac = n_salfac - entCabecera.n_imptotcom;
                        
                        string[,] arrParam1 = new string[3, 3] {
                                                {"n_idreg", "System.INT32", n_iddocventa.ToString()},
                                                {"n_importe", "System.DOUBLE", n_salfac.ToString()},
                                                {"n_tipo", "System.INT32", "2"}
                                          };

                        if (xMiFuncion.StoreEjecutar("log_compras_actualizarsaldo", arrParam1, mysConec) == false)
                        {
                            b_ocurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return b_result;
                        }
                    }

                    entCabecera.n_id = Convert.ToInt32(xMiFuncion.intIdGenerado);
                    // GRABAMOS EL DETALLE DE LA COMPRA
                    for (n_Fila = 0; n_Fila <= lstDetalle.Count - 1; n_Fila++)
                    {
                        lstDetalle[n_Fila].n_idcom = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("log_comprasdet_insertar", lstDetalle[n_Fila], mysConec, null) == false)
                        {
                            b_ocurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return b_result;
                        }
                    }

                    // GRABAMOS LOS DOUMENTOS ASIGNADOS A LA COMPRA
                    for (n_Fila = 0; n_Fila <= lstDocumentos.Count - 1; n_Fila++)
                    {
                        lstDocumentos[n_Fila].n_idcom = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("log_comprasdoc_insertar", lstDocumentos[n_Fila], mysConec, null) == false)
                        {
                            b_ocurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return b_result;
                        }
                    }

                    // ACTUALIZAMOS LOS DOCUMENTOS ASIGNADOS A LA COMPRA
                    CD_alm_movimientos objMov = new CD_alm_movimientos();
                    objMov.mysConec = mysConec;
                    for (n_Fila = 0; n_Fila <= lstDocumentos.Count - 1; n_Fila++)
                    {
                        if (objMov.ActualizarDocCompra(lstDocumentos[n_Fila].n_iddoc, entCabecera.n_id, 1) == false)
                        {
                            b_ocurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return b_result;
                        }
                    }
                }
                else
                {
                    b_ocurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return b_result;                
                }
                n_IdGenerado = entCabecera.n_id;
                b_result = true;
                trans.Commit();
                return b_result;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_ocurrioError = true;
                c_ErrorMensaje = exc.Message.ToString();
                n_ErrorNumber = exc.HResult;
                trans.Rollback();
                return b_result;
            }
        }
        public bool Actualizar(BE_LOG_COMPRAS entCabecera, List<BE_LOG_COMPRASDET> lstDetalle, List<BE_LOG_COMPRASDOC> lstDocumentos)
        {
            bool b_result = false;
            MySqlTransaction trans;
            int n_Fila = 0;
            trans = mysConec.BeginTransaction();

            try
            {
                if (entCabecera.n_idtipdoc == 8)
                {
                    DataTable dtresult = new DataTable();
                    CD_log_compras o_compras = new CD_log_compras();
                    double n_valorinc = 0;
                    double n_salfac = 0;

                    // TRAEMOS LOS DATOS DE LA NOTA DE CREDITO QUE SE ESTA ACTUALIZADO
                    o_compras.mysConec = mysConec;
                    o_compras.TraerRegistro(Convert.ToInt32(entCabecera.n_id));
                    dtresult = o_compras.dtRegistro;
                    n_valorinc = Convert.ToDouble(dtresult.Rows[0]["n_imptotcom"]);                                        // EL IMPORTE ORIGINAL DE LA NOTA DE CREDITO

                    // TRAEMOS EL DOCUMENTO QUE SE NODIFICA
                    int n_iddocventa = Convert.ToInt32(dtresult.Rows[0]["n_iddocmod"]);                                   // OBTENEMOS EL ID DEL DOCUMENTO QUE SE MODIFICA
                    o_compras.TraerRegistro(n_iddocventa);
                    dtresult = o_compras.dtRegistro;
                    n_salfac = Convert.ToDouble(dtresult.Rows[0]["n_impsal"]);
                    n_salfac = n_salfac + n_valorinc;

                    string[,] arrParam1 = new string[3, 3] {
                                                {"n_idreg", "System.INT32", n_iddocventa.ToString()},
                                                {"n_importe", "System.DOUBLE", n_salfac.ToString()},
                                                {"n_tipo", "System.DOUBLE", "1"}
                                          };

                    if (xMiFuncion.StoreEjecutar("log_compras_actualizarsaldo", arrParam1, mysConec) == false)
                    {
                        b_ocurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return b_result;
                    }
                }

                // ACTUALIZAMOS EL ESTADO DE LOS DOCUMENTOS ASIGNADOS A NO PROCESADO
                CD_alm_movimientos objMov = new CD_alm_movimientos();
                objMov.mysConec = mysConec;

                if (TraerRegistro(entCabecera.n_id) == true)
                {
                    if (dtListaDoc.Rows.Count != 0)
                    {
                        for (n_Fila = 0; n_Fila <= dtListaDoc.Rows.Count - 1; n_Fila++)
                        {
                            if (objMov.ActualizarDocCompra(Convert.ToInt32(dtListaDoc.Rows[n_Fila]["n_iddoc"]), 0, 0) == false)
                            {
                                b_ocurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return b_result;
                            }
                        }
                    }
                }

                if (xMiFuncion.StoreEjecutar("log_compras_actualizar", entCabecera, mysConec, null) == true)
                {
                    // ACTUALIZAMOS EL SALDO DE LA FACTURA QUE SE MODIFICA
                    if (entCabecera.n_idtipdoc == 8)
                    {
                        //BE_LOG_COMPRAS e_compras = new BE_LOG_COMPRAS();
                        DataTable dtres = new DataTable();
                        CD_log_compras o_compras = new CD_log_compras();
                        double n_salfac = 0;

                        o_compras.mysConec = mysConec;

                        // TRAEMOS EL DOCUMENTO QUE SE NODIFICA
                        int n_iddocventa = entCabecera.n_iddocmod;                                   // OBTENEMOS EL ID DEL DOCUMENTO QUE SE MODIFICA
                        o_compras.TraerRegistro(n_iddocventa);
                        dtres = o_compras.dtRegistro;
                        n_salfac = Convert.ToDouble(dtres.Rows[0]["n_impsal"]);
                        n_salfac = n_salfac - entCabecera.n_imptotcom;

                        string[,] arrParam1 = new string[3, 3] {
                                                {"n_idreg", "System.INT32", n_iddocventa.ToString()},
                                                {"n_importe", "System.DOUBLE", n_salfac.ToString()},
                                                {"n_tipo", "System.INT32", "2"}
                                          };

                        if (xMiFuncion.StoreEjecutar("log_compras_actualizarsaldo", arrParam1, mysConec) == false)
                        {
                            b_ocurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return b_result;
                        }
                    }

                    // ELIMINAMOS LOS ITEMS PREVIOS
                    string[,] arrParametros = new string[1, 3] {
                                                {"n_idcom", "System.INT64", entCabecera.n_id.ToString()}
                                          };
                    
                    // ADICIONAMOS EL DETALLE DE LA COMPRA
                    if (xMiFuncion.StoreEjecutar("log_comprasdet_delete", arrParametros, mysConec) == true)
                    {
                        // SI LOS ITEMS SE ELIMINARON CON EXITO INSERTAMOS LOS NUEVOS ITEMS
                        for (n_Fila = 0; n_Fila <= lstDetalle.Count - 1; n_Fila++)
                        {
                            lstDetalle[n_Fila].n_idcom = Convert.ToInt32(entCabecera.n_id);
                            if (xMiFuncion.StoreEjecutar("log_comprasdet_insertar", lstDetalle[n_Fila], mysConec, null) == false)
                            {
                                b_ocurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return b_result;
                            }
                        }
                    }
                    else
                    {
                        b_ocurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return b_result;
                    }

                    // ADICIONAMOS LOS DOCUMENTOS DE LA COMPRA
                    if (xMiFuncion.StoreEjecutar("log_comprasdoc_delete", arrParametros, mysConec) == true)
                    {
                        // SI LOS DOCUMENTOS DE LA COMPRA
                        for (n_Fila = 0; n_Fila <= lstDocumentos.Count - 1; n_Fila++)
                        {
                            lstDocumentos[n_Fila].n_idcom = Convert.ToInt32(entCabecera.n_id);
                            if (xMiFuncion.StoreEjecutar("log_comprasdoc_insertar", lstDocumentos[n_Fila], mysConec, null) == false)
                            {
                                b_ocurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return b_result;
                            }
                        }
                    }
                    else
                    {
                        b_ocurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return b_result;
                    }

                    // ACTUALIZAMOS LOS DOCUMENTOS ASIGNADOS A LA COMPRA
                    if (lstDocumentos.Count != 0)
                    { 
                        objMov.mysConec = mysConec;
                        for (n_Fila = 0; n_Fila <= lstDocumentos.Count - 1; n_Fila++)
                        {
                            if (objMov.ActualizarDocCompra(lstDocumentos[n_Fila].n_iddoc, entCabecera.n_id, 1) == false)
                            {
                                b_ocurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return b_result;
                            }
                        }
                    }

                    if (entCabecera.n_idtipdoc == 8)             // SI ES NOTA DE CREDITO
                    {

                    }
                }
                else
                {
                    b_ocurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return b_result;
                }

                b_result = true;
                trans.Commit();
                return b_result;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_ocurrioError = true;
                c_ErrorMensaje = exc.Message.ToString();
                n_ErrorNumber = exc.HResult;
                trans.Rollback();
                return b_result;
            }
        }
        public bool AsientoCab(int n_IdEmpresa, int n_IdCompra)
        {
            bool b_result = false;

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                            {"n_id", "System.INT64",n_IdCompra.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("log_compras_asientocab", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            else
            {
                dtListaDet = xMiFuncion.StoreDTLLenar("log_compras_asientodet", arrParametros, mysConec);
                if (xMiFuncion.IntErrorNumber != 0)
                {
                    b_ocurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    return b_result;
                }
            }
            b_result = true;

            return b_result;
        }
        public bool AgregarNumAsi(int n_IdRegistro, string c_NumeroRegistro)
        {
            bool b_Result = false;

            string[,] arrParametros = new string[2, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()},
                                            {"c_numasi", "System.STRING", c_NumeroRegistro.ToString()}
                                      };
            // BORRAMOS EL PADRE
            if (xMiFuncion.StoreEjecutar("log_compras_insertarnumasi", arrParametros, mysConec) == false)
            {
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return b_Result;
        }
        public bool Consulta2(int n_IdEmpresa, string c_FechaInicio, string c_FchFinal, int n_IdMoneda, int n_TipoFecha, int n_TipoSaldo, int n_IdLibro, int n_TipoConsulta)
        {
            bool b_result = false;

            string[,] arrParametros = new string[8, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FchFinal.ToString()},
                                            {"n_idmon", "System.INT32", n_IdMoneda.ToString()},
                                            {"n_tipfch", "System.INT32", n_TipoFecha.ToString()},
                                            {"n_tipsal", "System.INT32", n_TipoSaldo.ToString()},
                                            {"n_idlib", "System.INT32", n_IdLibro.ToString()},
                                            {"n_tipcon", "System.INT32", n_TipoConsulta.ToString()}

                                      };
            dtLista = xMiFuncion.StoreDTLLenar("log_compras_consulta2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool Consulta3(int n_IdEmpresa, int n_IdProveedor)
        {
            bool b_result = false;

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idpro", "System.INT64",n_IdProveedor.ToString()},
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("log_compras_consulta3", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;

            return b_result;
        }
        public DataTable DocumentosPercepcion(int n_IdEmpresa, int n_IdProveedor)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idpro", "System.INT16", n_IdProveedor.ToString()},
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("log_compras_consulta4", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Consulta5(int n_IdEmpresa, int n_IdCliente)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idcli", "System.INT16", n_IdCliente.ToString()}    
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("log_compras_consulta5", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Consulta9(int n_IdEmpresa, int n_AnoTrabajo, int n_IdLibro)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"n_idlib", "System.INT16", n_IdLibro.ToString()}    
                                      };
            	
            DtResultado = xMiFuncion.StoreDTLLenar("log_compras_consulta9", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Consulta8(int n_IdEmpresa, int n_AnoTrabajo, int n_IdLibro)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()},    
                                            {"n_idlib", "System.INT16", n_IdLibro.ToString()}    
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("log_compras_consulta8", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        // call log_compras_consulta8(2,2018)
        public void TieneCtaContable(string c_cadenaIn, int n_IdEmpresa)
        {
            string[,] arrParametros = new string[2, 3] {
                                            {"c_cadin", "System.STRING", c_cadenaIn},
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("log_compras_tienectacontable", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool ImportarApertura(int n_IdEmpresa, int n_AnoTrabajo, int n_IdLibro, string c_CadenaIN)
        {
            bool b_result = false;
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.STRING", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_idlib", "System.INT32", n_IdLibro.ToString()},
                                            {"c_cadin", "System.STRING", c_CadenaIN.ToString()}
                                      };

            xMiFuncion.StoreEjecutar("log_compras_apertura", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public void Consulta10(int n_IdEmpresa, int n_AnoTrabajo, int n_tipo)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_tipdat", "System.INT16", n_tipo.ToString()}    
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("log_compras_consulta10", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void Consulta14(int n_IdEmpresa, int n_TipoMoneda, int n_TipoImporte)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_tipmon", "System.INT16", n_TipoMoneda.ToString()},
                                            {"n_tipimp", "System.INT16", n_TipoImporte.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("log_compras_consulta14", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void Consulta17(int n_IdEmpresa, int n_AnoTrabajo, int n_TipoMoneda, int n_TipoImporte)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"n_tipmon", "System.INT16", n_TipoMoneda.ToString()},
                                            {"n_tipimp", "System.INT16", n_TipoImporte.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("los_compras_consulta17", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                b_ocurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
    }
}
