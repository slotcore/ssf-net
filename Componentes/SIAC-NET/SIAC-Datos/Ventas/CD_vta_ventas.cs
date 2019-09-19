using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Contabilidad;

namespace SIAC_DATOS.Ventas
{
    public class CD_vta_ventas
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public List<BE_VTA_VENTASDET> LstDetalle = new List<BE_VTA_VENTASDET>();
        public List<BE_VTA_VENTASDAT> LstDatos = new List<BE_VTA_VENTASDAT>();
        public List<BE_VTA_VENTASOCT> LstDetalleOCT = new List<BE_VTA_VENTASOCT>();
        public List<BE_VTA_VENTASDOC> LstDocumentos = new List<BE_VTA_VENTASDOC>();
        public List<BE_VTA_VENTASNCDOC> LstDocumentosNC = new List<BE_VTA_VENTASNCDOC>();
        public List<BE_CON_DIARIO> l_diario = new List<BE_CON_DIARIO>();

        public DataTable dtLista1 = new DataTable();
        public DataTable dtLista2 = new DataTable();
        public DataTable dtLista3 = new DataTable();
        public DataTable dtLista4 = new DataTable();
        public DataTable dtLista5 = new DataTable();

        public double n_IdGenerado = 0;
        Helper.Comunes.Funciones funBas = new Helper.Comunes.Funciones();

        DatosMySql xMiFuncion = new DatosMySql();

        public DataTable Listar(int n_idempresa, int n_idmes, int n_anotra, int n_IdLibro)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16",n_idempresa.ToString()},
                                            {"n_idmes", "System.INT16",n_idmes.ToString()},
                                            {"n_idano", "System.INT16",n_anotra.ToString()}
                                      };
            if (n_IdLibro == 14)
            { 
                DtResultado = xMiFuncion.StoreDTLLenar("vta_ventas_select", arrParametros, mysConec);
            }
            if (n_IdLibro == 33)
            {
                DtResultado = xMiFuncion.StoreDTLLenar("vta_ventasnoemitidas_select", arrParametros, mysConec);
            }

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public BE_VTA_VENTAS TraerRegistro(int n_IdRegistro)
        {
            BE_VTA_VENTAS Ent_Ventas = new BE_VTA_VENTAS();
            DatosMySql xMiFuncion = new DatosMySql();
            int n_fila;
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("vta_ventas_obtenerregistro", arrParametros, mysConec);

            if (DtResultado.Rows.Count != 0)
            {
                Ent_Ventas.n_id = Convert.ToInt32(DtResultado.Rows[0]["n_id"].ToString());
                Ent_Ventas.n_idemp = Convert.ToInt16(DtResultado.Rows[0]["n_idemp"].ToString());
                Ent_Ventas.n_anotra = Convert.ToInt16(DtResultado.Rows[0]["n_anotra"].ToString());
                Ent_Ventas.n_idmes = Convert.ToInt16(DtResultado.Rows[0]["n_idmes"].ToString());
                Ent_Ventas.n_idlib = Convert.ToInt16(DtResultado.Rows[0]["n_idlib"].ToString());
                Ent_Ventas.c_numreg = DtResultado.Rows[0]["c_numreg"].ToString();
                Ent_Ventas.n_idtippro = Convert.ToInt16(funBas.NulosN(DtResultado.Rows[0]["n_idtippro"]));
                Ent_Ventas.n_idcli = Convert.ToInt16(DtResultado.Rows[0]["n_idcli"].ToString());
                Ent_Ventas.n_idtipdoc = Convert.ToInt16(DtResultado.Rows[0]["n_idtipdoc"].ToString());
                Ent_Ventas.c_numser = DtResultado.Rows[0]["c_numser"].ToString();
                Ent_Ventas.c_numdoc = DtResultado.Rows[0]["c_numdoc"].ToString();
                Ent_Ventas.d_fchreg = Convert.ToDateTime(DtResultado.Rows[0]["d_fchreg"].ToString());
                Ent_Ventas.d_fchdoc = Convert.ToDateTime(DtResultado.Rows[0]["d_fchdoc"].ToString());
                Ent_Ventas.d_fchven = Convert.ToDateTime(DtResultado.Rows[0]["d_fchven"].ToString());
                Ent_Ventas.n_idconpag = Convert.ToInt16(DtResultado.Rows[0]["n_idconpag"].ToString());
                Ent_Ventas.n_idmon = Convert.ToInt16(DtResultado.Rows[0]["n_idmon"].ToString());
                Ent_Ventas.n_impbru = Convert.ToDouble(DtResultado.Rows[0]["n_impbru"].ToString());
                Ent_Ventas.n_impbru2 = Convert.ToDouble(DtResultado.Rows[0]["n_impbru2"].ToString());
                Ent_Ventas.n_impbru3 = Convert.ToDouble(DtResultado.Rows[0]["n_impbru3"].ToString());
                Ent_Ventas.n_impinaf = Convert.ToDouble(DtResultado.Rows[0]["n_impinaf"].ToString());
                Ent_Ventas.n_impigv = Convert.ToDouble(DtResultado.Rows[0]["n_impigv"].ToString());
                Ent_Ventas.n_impisc = Convert.ToDouble(DtResultado.Rows[0]["n_impisc"].ToString());
                Ent_Ventas.n_impotr = Convert.ToDouble(funBas.NulosN(DtResultado.Rows[0]["n_impotr"]));
                Ent_Ventas.n_imptotven = Convert.ToDouble(DtResultado.Rows[0]["n_imptotven"].ToString());
                Ent_Ventas.n_tc = Convert.ToDouble(funBas.NulosN(DtResultado.Rows[0]["n_tc"]));
                Ent_Ventas.n_impsal = Convert.ToDouble(DtResultado.Rows[0]["n_impsal"].ToString());
                Ent_Ventas.n_idven = Convert.ToInt16(DtResultado.Rows[0]["n_idven"].ToString());
                Ent_Ventas.n_tasaigv = Convert.ToDouble(DtResultado.Rows[0]["n_tasaigv"].ToString());
                Ent_Ventas.c_glosa = DtResultado.Rows[0]["c_glosa"].ToString();
                Ent_Ventas.n_anulado = Convert.ToInt16(DtResultado.Rows[0]["n_anulado"].ToString());
                Ent_Ventas.n_oriitem = Convert.ToInt16(DtResultado.Rows[0]["n_oriitem"].ToString());

                Ent_Ventas.n_impsubtot = Convert.ToDouble(funBas.NulosN(DtResultado.Rows[0]["n_impsubtot"]));
                Ent_Ventas.n_pordsc = Convert.ToDouble(funBas.NulosN(DtResultado.Rows[0]["n_pordsc"]));

                Ent_Ventas.n_idtipdocmod = Convert.ToInt32(funBas.NulosN(DtResultado.Rows[0]["n_idtipdocmod"]));
                Ent_Ventas.n_iddocmod = Convert.ToInt32(funBas.NulosN(DtResultado.Rows[0]["n_iddocmod"]));
                Ent_Ventas.n_idtipmot = Convert.ToInt32(funBas.NulosN(DtResultado.Rows[0]["n_idtipmot"]));
                Ent_Ventas.n_idtipope = Convert.ToInt32(funBas.NulosN(DtResultado.Rows[0]["n_idtipope"]));

                Ent_Ventas.n_idtipdocref = Convert.ToInt16(DtResultado.Rows[0]["n_idtipdocref"].ToString());
                Ent_Ventas.n_iddocref = Convert.ToInt16(DtResultado.Rows[0]["n_iddocref"].ToString());
                Ent_Ventas.c_numdocref = DtResultado.Rows[0]["c_numdocref"].ToString();
                Ent_Ventas.c_serdocref = DtResultado.Rows[0]["c_serdocref"].ToString();
                Ent_Ventas.c_motnc = DtResultado.Rows[0]["c_motnc"].ToString();
                Ent_Ventas.n_iddocven = Convert.ToInt32(funBas.NulosN(DtResultado.Rows[0]["n_iddocven"]));
            }

            arrParametros[0, 0] = "n_idvta";
            arrParametros[0, 1] = "System.INT16";
            arrParametros[0, 2] = n_IdRegistro.ToString();

            // OBTENEMOS EL DETALLE DE LA VENTA
            List<BE_VTA_VENTASDET> LstDetalleTMP = new List<BE_VTA_VENTASDET>();

            DtResultado = xMiFuncion.StoreDTLLenar("vta_ventasdet_obtenerregistro", arrParametros, mysConec);
            if (DtResultado.Rows.Count != 0)
            {
                for (n_fila = 0; n_fila <= DtResultado.Rows.Count - 1; n_fila++)
                {
                    BE_VTA_VENTASDET BE_Detalle = new BE_VTA_VENTASDET();

                    BE_Detalle.n_idvta = Convert.ToInt32(DtResultado.Rows[n_fila]["n_idvta"].ToString());
                    BE_Detalle.n_iditem = Convert.ToInt16(DtResultado.Rows[n_fila]["n_iditem"].ToString());
                    BE_Detalle.c_desusu = DtResultado.Rows[n_fila]["c_desusu"].ToString();
                    BE_Detalle.n_idunimed = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idunimed"].ToString());
                    BE_Detalle.n_canpro = Convert.ToDouble(DtResultado.Rows[n_fila]["n_canpro"].ToString());
                    BE_Detalle.n_preunibru = Convert.ToDouble(DtResultado.Rows[n_fila]["n_preunibru"].ToString());
                    BE_Detalle.n_impdes = Convert.ToDouble(DtResultado.Rows[n_fila]["n_impdes"].ToString());
                    BE_Detalle.n_preuninet = Convert.ToDouble(DtResultado.Rows[n_fila]["n_preuninet"].ToString());
                    BE_Detalle.n_imptot = Convert.ToDouble(DtResultado.Rows[n_fila]["n_imptot"].ToString());
                    BE_Detalle.n_pordsc = Convert.ToDouble(funBas.NulosN(DtResultado.Rows[n_fila]["n_pordsc"]));
                    BE_Detalle.n_idtipven = Convert.ToInt16(funBas.NulosN(DtResultado.Rows[n_fila]["n_idtipven"]));
                    BE_Detalle.n_idtipafeigv = Convert.ToInt16(funBas.NulosN(DtResultado.Rows[n_fila]["n_idtipafeigv"]));
                    BE_Detalle.c_datadi = funBas.NulosC(DtResultado.Rows[n_fila]["c_datadi"]).ToString();
                    BE_Detalle.n_preuninetigv = Convert.ToDouble(funBas.NulosN(DtResultado.Rows[n_fila]["n_preuninetigv"]));
                    BE_Detalle.n_imptotigv = Convert.ToDouble(funBas.NulosN(DtResultado.Rows[n_fila]["n_imptotigv"]));
                    LstDetalleTMP.Add(BE_Detalle);
                }
            }
            LstDetalle = LstDetalleTMP;

            // ALACENAMOS LOS DOCUMENTO ASIGNADO ALA FACTURA
            LstDocumentos.Clear();
            DtResultado = xMiFuncion.StoreDTLLenar("vta_ventasdoc_obtenerregistro", arrParametros, mysConec);
            if (DtResultado.Rows.Count != 0)
            {
                for (n_fila = 0; n_fila <= DtResultado.Rows.Count - 1; n_fila++)
                {
                    BE_VTA_VENTASDOC BE_Ddocumento = new BE_VTA_VENTASDOC();

                    BE_Ddocumento.n_idvta = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idvta"].ToString());
                    BE_Ddocumento.n_idtipdoc = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idtipdoc"].ToString());
                    BE_Ddocumento.n_iddoc = Convert.ToInt16(DtResultado.Rows[n_fila]["n_iddoc"].ToString());
                    LstDocumentos.Add(BE_Ddocumento);
                }
            }
                        
            return Ent_Ventas;
        }
        public bool Insertar(BE_VTA_VENTAS entGuias)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int intFila = 0;
            MySqlTransaction trans = null;

            try
            {
                trans = mysConec.BeginTransaction();

                if (xMiFuncion.StoreEjecutar("vta_ventas_insertar", entGuias, mysConec, 0) == true)
                {
                    // ACTUALIZAMOS EL SALDO DE LA FACTURA QUE SE MODIFICA
                    if (entGuias.n_idtipdoc == 8)
                    {
                        for (intFila = 0; intFila <= LstDocumentosNC.Count - 1; intFila++)
                        {
                            BE_VTA_VENTAS e_ventas = new BE_VTA_VENTAS();
                            CD_vta_ventas o_venta = new CD_vta_ventas();
                            double n_salfac = 0;

                            o_venta.mysConec = mysConec;

                            // TRAEMOS EL DOCUMENTO QUE SE NODIFICA
                            //int n_iddocventa = entGuias.n_iddocmod;                                   // OBTENEMOS EL ID DEL DOCUMENTO QUE SE MODIFICA
                            int n_iddocventa = LstDocumentosNC[intFila].n_iddoc;                                   // OBTENEMOS EL ID DEL DOCUMENTO QUE SE MODIFICA
                            e_ventas = o_venta.TraerRegistro(n_iddocventa);
                            n_salfac = e_ventas.n_impsal - entGuias.n_imptotven;

                            string[,] arrParam1 = new string[2, 3] {
                                                {"n_id", "System.INT32", n_iddocventa.ToString()},
                                                {"n_saldo", "System.DOUBLE", n_salfac.ToString()}
                                          };

                            if (xMiFuncion.StoreEjecutar("vta_ventas_actualizarsaldo", arrParam1, mysConec) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }
                    }

                    // AGREGAMOS EL DETALLE DE LA VENTA
                    n_IdGenerado = xMiFuncion.intIdGenerado;
                    entGuias.n_id = Convert.ToInt64(n_IdGenerado);
                    for (intFila = 0; intFila <= LstDetalle.Count - 1; intFila++)
                    {
                        LstDetalle[intFila].n_idvta = Convert.ToInt64(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("vta_ventasdet_insertar", LstDetalle[intFila], mysConec, null) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }

                    }

                    // AGREGAMOS LOS DOCUMENTOS DE LA VENTA
                    for (intFila = 0; intFila <= LstDocumentos.Count - 1; intFila++)
                    {
                        LstDocumentos[intFila].n_idvta = Convert.ToInt64(n_IdGenerado);
                        if (xMiFuncion.StoreEjecutar("vta_ventasdoc_insertar", LstDocumentos[intFila], mysConec, null) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }

                    if (entGuias.n_oriitem == 2)
                    {
                        // ACTUALIZAMOS LAS GUIAS DE REMISION CON EL DOCUMENTO DE VENTA
                        for (intFila = 0; intFila <= LstDocumentos.Count - 1; intFila++)
                        {
                            string[,] arrParametros3 = new string[2, 3] {
                                                    {"n_idgui", "System.INT64",LstDocumentos[intFila].n_iddoc.ToString()},
                                                    {"n_iddocven", "System.INT64", n_IdGenerado.ToString()}
                                              };

                            if (xMiFuncion.StoreEjecutar("vta_guias_Actualizardocven", arrParametros3, mysConec) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }
                    }
                    if (entGuias.n_oriitem == 4)
                    {
                        // ACTUALIZAMOS LAS GUIAS DE REMISION CON EL DOCUMENTO DE VENTA
                        for (intFila = 0; intFila <= LstDocumentos.Count - 1; intFila++)
                        {
                            string[,] arrParametros3 = new string[2, 3] {
                                                    {"n_idpro", "System.INT64",LstDocumentos[intFila].n_iddoc.ToString()},
                                                    {"n_iddocven", "System.INT64", n_IdGenerado.ToString()}
                                              };

                            if (xMiFuncion.StoreEjecutar("vta_ventas_actualizardocven", arrParametros3, mysConec) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }
                    }
                    // ESTO ES PARA LA FACTURACION ELECTRONICA
                    for (intFila = 0; intFila <= LstDetalleOCT.Count - 1; intFila++)
                    {
                        LstDetalleOCT[intFila].n_idvta = Convert.ToInt64(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("vta_ventasoct_insertar", LstDetalleOCT[intFila], mysConec, null) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }

                    // AGREGAMOS LOS DATOS EXTRAS DE LA VENTA
                    for (intFila = 0; intFila <= LstDatos.Count - 1; intFila++)
                    {
                        LstDatos[intFila].n_idvta = Convert.ToInt64(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("vta_ventasdat_insertar", LstDatos[intFila], mysConec, null) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }

                    if  (entGuias.n_idtipdoc == 8)
                    {
                        // AGREGAMOS LOS DOCUMETOS QUE MODIFICA LA NOTA DE CREDITO
                        for (intFila = 0; intFila <= LstDocumentosNC.Count - 1; intFila++)
                        {
                            LstDocumentosNC[intFila].n_idvta = Convert.ToInt64(xMiFuncion.intIdGenerado);
                            if (xMiFuncion.StoreEjecutar("vta_ventasncdoc_insertar", LstDocumentosNC[intFila], mysConec, null) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }
                    }
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
        public bool Insertar2(BE_VTA_VENTAS entGuias, int n_IdCargo)
        {
            // SE LLAMARA A ESTE METODO CUANDO VENA DEL MODULO DE ESTACIONAMIENTO
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int intFila = 0;
            MySqlTransaction trans = null;

            //mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            try
            {
                trans = mysConec.BeginTransaction();
                entGuias.c_numreg = l_diario[0].c_numasi;
                if (xMiFuncion.StoreEjecutar("vta_ventas_insertar", entGuias, mysConec, 0) == true)
                {
                    // ACTUALIZAMOS EL SALDO DE LA FACTURA QUE SE MODIFICA
                    if (entGuias.n_idtipdoc == 8)
                    {
                        BE_VTA_VENTAS e_ventas = new BE_VTA_VENTAS();
                        CD_vta_ventas o_venta = new CD_vta_ventas();
                        double n_salfac = 0;

                        o_venta.mysConec = mysConec;

                        // TRAEMOS EL DOCUMENTO QUE SE NODIFICA
                        int n_iddocventa = entGuias.n_iddocmod;                                   // OBTENEMOS EL ID DEL DOCUMENTO QUE SE MODIFICA
                        e_ventas = o_venta.TraerRegistro(n_iddocventa);
                        n_salfac = e_ventas.n_impsal - entGuias.n_imptotven;

                        string[,] arrParam1 = new string[2, 3] {
                                                {"n_id", "System.INT32", n_iddocventa.ToString()},
                                                {"n_saldo", "System.DOUBLE", n_salfac.ToString()}
                                          };

                        if (xMiFuncion.StoreEjecutar("vta_ventas_actualizarsaldo", arrParam1, mysConec) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }

                    // AGREGAMOS EL DETALLE DE LA VENTA
                    n_IdGenerado = xMiFuncion.intIdGenerado;
                    entGuias.n_id = Convert.ToInt64(n_IdGenerado);
                    for (intFila = 0; intFila <= LstDetalle.Count - 1; intFila++)
                    {
                        LstDetalle[intFila].n_idvta = Convert.ToInt64(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("vta_ventasdet_insertar", LstDetalle[intFila], mysConec, null) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }

                    }

                    //// AGREGAMOS LOS DOCUMENTOS DE LA VENTA
                    //for (intFila = 0; intFila <= LstDocumentos.Count - 1; intFila++)
                    //{
                    //    LstDocumentos[intFila].n_idvta = Convert.ToInt32(n_IdGenerado);
                    //    if (xMiFuncion.StoreEjecutar("vta_ventasdoc_insertar", LstDocumentos[intFila], mysConec, null) == false)
                    //    {
                    //        booOcurrioError = xMiFuncion.booOcurrioError;
                    //        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    //        IntErrorNumber = xMiFuncion.IntErrorNumber;
                    //        trans.Rollback();
                    //        return booOk;
                    //    }
                    //}

                    //if (entGuias.n_oriitem == 2)
                    //{ 
                    //    // ACTUALIZAMOS LAS GUIAS DE REMISION CON EL DOCUMENTO DE VENTA
                    //    for (intFila = 0; intFila <= LstDocumentos.Count - 1; intFila++)
                    //    {
                    //        string[,] arrParametros3 = new string[2, 3] {
                    //                                {"n_idgui", "System.INT64",LstDocumentos[intFila].n_iddoc.ToString()},
                    //                                {"n_iddocven", "System.INT64", n_IdGenerado.ToString()}
                    //                          };

                    //        if (xMiFuncion.StoreEjecutar("vta_guias_Actualizardocven", arrParametros3, mysConec) == false)
                    //        {
                    //            booOcurrioError = xMiFuncion.booOcurrioError;
                    //            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    //            IntErrorNumber = xMiFuncion.IntErrorNumber;
                    //            trans.Rollback();
                    //            return booOk;
                    //        }
                    //    }
                    //}
                    //if (entGuias.n_oriitem == 4)
                    //{
                    //    // ACTUALIZAMOS LAS GUIAS DE REMISION CON EL DOCUMENTO DE VENTA
                    //    for (intFila = 0; intFila <= LstDocumentos.Count - 1; intFila++)
                    //    {
                    //        string[,] arrParametros3 = new string[2, 3] {
                    //                                {"n_idpro", "System.INT64",LstDocumentos[intFila].n_iddoc.ToString()},
                    //                                {"n_iddocven", "System.INT64", n_IdGenerado.ToString()}
                    //                          };

                    //        if (xMiFuncion.StoreEjecutar("vta_ventas_actualizardocven", arrParametros3, mysConec) == false)
                    //        {
                    //            booOcurrioError = xMiFuncion.booOcurrioError;
                    //            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    //            IntErrorNumber = xMiFuncion.IntErrorNumber;
                    //            trans.Rollback();
                    //            return booOk;
                    //        }
                    //    }
                    //}

                    // ESTO ES PARA LA FACTURACION ELECTRONICA
                    for (intFila = 0; intFila <= LstDetalleOCT.Count - 1; intFila++)
                    {
                        LstDetalleOCT[intFila].n_idvta = Convert.ToInt64(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("vta_ventasoct_insertar", LstDetalleOCT[intFila], mysConec, null) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }

                    // AGREGAMOS LOS DATOS EXTRAS DE LA VENTA
                    for (intFila = 0; intFila <= LstDatos.Count - 1; intFila++)
                    {
                        LstDatos[intFila].n_idvta = Convert.ToInt64(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("vta_ventasdat_insertar", LstDatos[intFila], mysConec, null) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }

                    // GRABAMOS EL DIARIO
                    for (intFila = 0; intFila <= l_diario.Count - 1; intFila++)
                    {
                        l_diario[intFila].n_oriid = Convert.ToInt32(n_IdGenerado);
                        if (xMiFuncion.StoreEjecutar("con_diario_insertar", l_diario[intFila], mysConec, 0) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }

                    // CASMBIAMOS EL ESTADO DEL CARGO
                    string[,] arrParametros = new string[3, 3] {
                                            {"n_id", "System.INT16", n_IdCargo.ToString()},
                                            {"n_iddocpag", "System.INT16", n_IdGenerado.ToString()},
                                            {"c_fchpag", "System.STRING", entGuias.d_fchdoc.ToString("dd/MM/yyyy")}
                                      };

                    DatosMySql FunMysql = new DatosMySql();
                    mysConec = FunMysql.ReAbrirConeccion(mysConec);

                    if (xMiFuncion.StoreEjecutar("est_cargoscab_actualizar_a_pagado", arrParametros, mysConec) == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }
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
        public bool Actualizar(BE_VTA_VENTAS entGuias, List<BE_VTA_VENTASDOC> lstDocumentoOriginal)
        {
            List<BE_VTA_VENTASDOC> lstDocTMP = new List<BE_VTA_VENTASDOC>();
            DataTable dtresul = new DataTable();
            CD_vta_guias objguias = new CD_vta_guias();
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int intFila = 0;
            MySqlTransaction trans;
            string c_cadenaIN = "";
            
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);

            trans = mysConec.BeginTransaction();
            try
            {
                if (entGuias.n_idtipdoc == 8)
                {
                    Consulta27(entGuias.n_id);
                    dtresul = dtLista1;
                    for (intFila = 0; intFila <= dtresul.Rows.Count - 1; intFila++)
                    { 
                        BE_VTA_VENTAS e_ventas = new BE_VTA_VENTAS();
                        CD_vta_ventas o_venta = new CD_vta_ventas();
                        double n_valorinc = 0;
                        double n_salfac = 0;

                        o_venta.mysConec = mysConec;
                        // TRAEMOS LOS DATOS DE LA NOTA DE CREDITO QUE SE ESTA ACTUALIZADO
                        //e_ventas = o_venta.TraerRegistro(Convert.ToInt32(entGuias.n_id));
                        //n_valorinc = e_ventas.n_imptotven;                                        // EL IMPORTE ORIGINAL DE LA NOTA DE CREDITO

                        // TRAEMOS EL DOCUMENTO QUE SE NODIFICA
                        int n_iddocventa = Convert.ToInt32(dtresul.Rows[intFila]["n_iddoc"]);                                     // OBTENEMOS EL ID DEL DOCUMENTO QUE SE MODIFICA
                        e_ventas = o_venta.TraerRegistro(n_iddocventa);
                        n_valorinc = Convert.ToDouble(dtresul.Rows[intFila]["n_acuenta"]);                                        // EL IMPORTE ORIGINAL DE LA NOTA DE CREDITO
                        n_salfac = e_ventas.n_impsal + n_valorinc;

                        string[,] arrParam1 = new string[2, 3] {
                                                    {"n_id", "System.INT32", n_iddocventa.ToString()},
                                                    {"n_saldo", "System.DOUBLE", n_salfac.ToString()}
                                              };

                        if (xMiFuncion.StoreEjecutar("vta_ventas_actualizarsaldo", arrParam1, mysConec) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }
                }

                // ARMAMOS LA CADENA IN PARA ACTUALIZAR LAS GUIAS
                lstDocTMP = lstDocumentoOriginal;
                for (intFila = 0; intFila <= lstDocTMP.Count - 1; intFila++)
                {
                    if (intFila == 0) { c_cadenaIN = c_cadenaIN + lstDocTMP[intFila].n_iddoc.ToString(); }
                    if (intFila >= 1) { c_cadenaIN = c_cadenaIN + "," + lstDocTMP[intFila].n_iddoc.ToString(); }
                }

                objguias.mysConec = mysConec;
                if (objguias.QuitarDocVenta(c_cadenaIN) == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }
                
                if (xMiFuncion.StoreEjecutar("vta_ventas_actualizar", entGuias, mysConec, null) == true)
                {
                    // ACTUALIZAMOS EL ESTADO DE LA FACTURA QUE SE MODIFICA
                    if (entGuias.n_idtipdoc == 8)
                    {
                        BE_VTA_VENTAS e_ventas = new BE_VTA_VENTAS();
                        CD_vta_ventas o_venta = new CD_vta_ventas();
                        double n_salfac = 0;

                        o_venta.mysConec = mysConec;

                        //// TRAEMOS LOS DATOS DE LA NOTA DE CREDITO QUE SE ESTA ACTUALIZADO
                        //e_ventas = o_venta.TraerRegistro(Convert.ToInt32(entGuias.n_id));
                        //n_valorinc = e_ventas.n_imptotven;                                        // EL IMPORTE ORIGINAL DE LA NOTA DE CREDITO

                        // TRAEMOS EL DOCUMENTO QUE SE NODIFICA

                        for (intFila = 0; intFila <= LstDocumentosNC.Count - 1; intFila++)
                        {
                            int n_iddocventa = LstDocumentosNC[intFila].n_iddoc;
                            e_ventas = o_venta.TraerRegistro(n_iddocventa);
                            n_salfac = e_ventas.n_impsal - LstDocumentosNC[intFila].n_acuenta;

                            string[,] arrParam1 = new string[2, 3] {
                                                {"n_id", "System.INT32", n_iddocventa.ToString()},
                                                {"n_saldo", "System.DOUBLE", n_salfac.ToString()}
                                          };

                            if (xMiFuncion.StoreEjecutar("vta_ventas_actualizarsaldo", arrParam1, mysConec) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }


                        //int n_iddocventa = entGuias.n_iddocmod;                                   // OBTENEMOS EL ID DEL DOCUMENTO QUE SE MODIFICA
                        //e_ventas = o_venta.TraerRegistro(n_iddocventa);
                        //n_salfac = e_ventas.n_impsal - entGuias.n_imptotven;

                        //string[,] arrParam1 = new string[2, 3] {
                        //                        {"n_id", "System.INT32", n_iddocventa.ToString()},
                        //                        {"n_saldo", "System.DOUBLE", n_salfac.ToString()}
                        //                  };

                        //if (xMiFuncion.StoreEjecutar("vta_ventas_actualizarsaldo", arrParam1, mysConec) == false)
                        //{
                        //    booOcurrioError = xMiFuncion.booOcurrioError;
                        //    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        //    IntErrorNumber = xMiFuncion.IntErrorNumber;
                        //    trans.Rollback();
                        //    return booOk;
                        //}
                    }

                    // ELIMINAMOS LOS ITEMS PREVIOS
                    string[,] arrParametros = new string[1, 3] {
                                                {"n_idvta", "System.INT64", entGuias.n_id.ToString()}
                                          };

                    if (xMiFuncion.StoreEjecutar("vta_ventasoct_delete", arrParametros, mysConec) == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }
                    
                    if (xMiFuncion.StoreEjecutar("vta_ventasdoc_delete", arrParametros, mysConec) == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }

                    if (xMiFuncion.StoreEjecutar("vta_ventasdat_delete", arrParametros, mysConec) == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }

                    if (xMiFuncion.StoreEjecutar("vta_ventasncdoc_delete", arrParametros, mysConec) == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }

                    if (xMiFuncion.StoreEjecutar("vta_ventasdet_delete", arrParametros, mysConec) == true)
                    {
                        // SI LOS ITEMS SE ELIMINARON CON EXITO INSERTAMOS LOS NUEVOS ITEMS
                        for (intFila = 0; intFila <= LstDetalle.Count - 1; intFila++)
                        {
                            LstDetalle[intFila].n_idvta = entGuias.n_id;
                            if (xMiFuncion.StoreEjecutar("vta_ventasdet_insertar", LstDetalle[intFila], mysConec, null) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }

                        // AGREGAMOS LOS DOCUMENTOS DE LA VENTA
                        for (intFila = 0; intFila <= LstDocumentos.Count - 1; intFila++)
                        {
                            LstDocumentos[intFila].n_idvta = Convert.ToInt32(entGuias.n_id);
                            if (xMiFuncion.StoreEjecutar("vta_ventasdoc_insertar", LstDocumentos[intFila], mysConec, null) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }

                        // ACTUALIZAMOS LAS GUIAS DE REMISION CON EL DOCUMENTO DE VENTA
                        for (intFila = 0; intFila <= LstDocumentos.Count - 1; intFila++)
                        {
                            string[,] arrParametros3 = new string[2, 3] {
                                                {"n_idgui", "System.INT64",LstDocumentos[intFila].n_iddoc.ToString()},
                                                {"n_iddocven", "System.INT64", entGuias.n_id.ToString()}
                                          };

                            if (xMiFuncion.StoreEjecutar("vta_guias_Actualizardocven", arrParametros3, mysConec) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }

                        // ESTO ES PARA LA FACTURACION ELECTRONICA
                        for (intFila = 0; intFila <= LstDetalleOCT.Count - 1; intFila++)
                        {
                            LstDetalleOCT[intFila].n_idvta = entGuias.n_id;
                            if (xMiFuncion.StoreEjecutar("vta_ventasoct_insertar", LstDetalleOCT[intFila], mysConec, null) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }

                        // AGREGAMOS LOS DATOS EXTRAS DE LA VENTA
                        for (intFila = 0; intFila <= LstDatos.Count - 1; intFila++)
                        {
                            LstDatos[intFila].n_idvta = entGuias.n_id;
                            if (xMiFuncion.StoreEjecutar("vta_ventasdat_insertar", LstDatos[intFila], mysConec, null) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }

                        if (entGuias.n_idtipdoc == 8)
                        {
                            // AGREGAMOS LOS DOCUMETOS QUE MODIFICA LA NOTA DE CREDITO
                            for (intFila = 0; intFila <= LstDocumentosNC.Count - 1; intFila++)
                            {
                                LstDocumentosNC[intFila].n_idvta = entGuias.n_id;
                                if (xMiFuncion.StoreEjecutar("vta_ventasncdoc_insertar", LstDocumentosNC[intFila], mysConec, null) == false)
                                {
                                    booOcurrioError = xMiFuncion.booOcurrioError;
                                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                                    trans.Rollback();
                                    return booOk;
                                }
                            }
                        }
                        booOk = true;
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
                booOcurrioError = true;
                StrErrorMensaje = exc.Message.ToString();
                IntErrorNumber = exc.HResult;
                trans.Rollback();
                return booOk;
            }
        }
        public bool Eliminar(int n_IdItem, List<BE_VTA_VENTASDOC> lstDocumentoOriginal)
        {
            bool booResult = false;
            int intFila = 0;
            string[,] arrParametros = new string[1, 3];
            List<BE_VTA_VENTASDOC> lstDocTMP = new List<BE_VTA_VENTASDOC>();
            BE_VTA_VENTAS e_Ventas = new BE_VTA_VENTAS();
            MySqlTransaction trans;

            e_Ventas =  TraerRegistro(n_IdItem);               // TRAEMOS LOS DATOS DE LA VENTA
            
            arrParametros[0, 0] = "n_idvta";
            arrParametros[0, 1] = "System.INT16";
            arrParametros[0, 2] = n_IdItem.ToString();

            // ARMAMOS LA CADENA IN PARA ACTUALIZAR LAS GUIAS
            lstDocTMP = lstDocumentoOriginal;
            string c_cadenaIN = "";
            for (intFila = 0; intFila <= lstDocTMP.Count - 1; intFila++)
            {
                if (intFila == 0) { c_cadenaIN = c_cadenaIN + lstDocTMP[intFila].n_iddoc.ToString(); }
                if (intFila >= 1) { c_cadenaIN = c_cadenaIN + "," + lstDocTMP[intFila].n_iddoc.ToString(); }
            }
            DataTable dtresul = new DataTable();
            CD_vta_guias objguias = new CD_vta_guias();
            objguias.mysConec = mysConec;

            trans = mysConec.BeginTransaction();
            try
            {
                // TRAEMOS LOS DATOS DEL DOCUMENTO QUE SE ESTA ELIMINANDO
                double n_valorinc = 0;
                double n_salfac = 0;
                BE_VTA_VENTAS e_ventas = new BE_VTA_VENTAS();
                CD_vta_ventas o_venta = new CD_vta_ventas();
                o_venta.mysConec = mysConec;
                e_ventas = o_venta.TraerRegistro(Convert.ToInt32(n_IdItem));
                n_valorinc = e_ventas.n_imptotven;                                        // EL IMPORTE ORIGINAL DE LA NOTA DE CREDITO

                if (e_ventas.n_idtipdoc == 8)                                             //  SI ES NOTA DE CREDITO ACTUALIZAMOS EL SALDO DEL DOCUMENTO A SU SALDO ORIGINAL     
                {
                    // TRAEMOS EL DOCUMENTO QUE SE NODIFICA
                    int n_iddocventa = e_ventas.n_iddocmod;                                   // OBTENEMOS EL ID DEL DOCUMENTO QUE SE MODIFICA
                    e_ventas = o_venta.TraerRegistro(n_iddocventa);
                    n_salfac = e_ventas.n_impsal + n_valorinc;

                    string[,] arrParam1 = new string[2, 3] {
                                                {"n_id", "System.INT32", n_iddocventa.ToString()},
                                                {"n_saldo", "System.DOUBLE", n_salfac.ToString()}
                                          };

                    if (xMiFuncion.StoreEjecutar("vta_ventas_actualizarsaldo", arrParam1, mysConec) == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booResult;
                    }

                }

                if (e_ventas.n_oriitem == 2)                                 // ACTUALIZAMOS EL ID DEL DOCUMENTO DE VENTA DE LAS GUIAS
                { 
                    if (c_cadenaIN != "")
                    {
                        if (objguias.QuitarDocVenta(c_cadenaIN) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            return booResult;
                        }
                    }
                }

                if (e_ventas.n_oriitem == 4)                                 // ACTUALIZAMOS EL ID DEL DOCUMENTO DE VENTA DE LA PROFORMA
                {
                    if (c_cadenaIN != "")
                    {
                        if (QuitarDocVenta(c_cadenaIN) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            return booResult;
                        }
                    }
                }
                
                // BORRAMOS EL HIJO
                if (xMiFuncion.StoreEjecutar("vta_ventasdoc_delete", arrParametros, mysConec) == true)
                {
                    if (xMiFuncion.StoreEjecutar("vta_ventasoct_delete", arrParametros, mysConec) == true)
                    {
                        if (xMiFuncion.StoreEjecutar("vta_ventasdet_delete", arrParametros, mysConec) == true)
                        {
                            arrParametros[0, 0] = "n_id";
                            arrParametros[0, 1] = "System.INT16";
                            arrParametros[0, 2] = n_IdItem.ToString();

                            // BORRAMOS EL PADRE
                            booResult = xMiFuncion.StoreEjecutar("vta_ventas_delete", arrParametros, mysConec);

                            if (booResult == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booResult;
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
                    }
                    else
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booResult;
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
                // ELIMINAMOS EL DIARIO DE LA VENTA
                string[,] arrParDelDiario = new string[5, 3] {
                                                {"n_lib", "System.INT16", e_Ventas.n_idlib.ToString()},
                                                {"n_ano", "System.INT16", e_Ventas.n_anotra.ToString()},
                                                {"n_mes", "System.INT16", e_Ventas.n_idmes.ToString()},    
                                                {"c_numasi", "System.STRING", e_Ventas.c_numreg.ToString()},    
                                                {"n_idemp", "System.INT16", e_Ventas.n_idemp.ToString()}
                                          };
                booResult = xMiFuncion.StoreEjecutar("con_diario_delete", arrParDelDiario, mysConec);
                if (booResult == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                }

                trans.Commit();
                return booResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                //trans.Rollback();
                return booResult;
            }
        }
        public bool Anular(int n_IdVenta, List<BE_VTA_VENTASDOC> lstDocumentoOriginal)
        {
            bool booResult = false;
            MySqlTransaction trans;
            BE_VTA_VENTAS e_Ventas = new BE_VTA_VENTAS();
            string[,] arrParametros = new string[1, 3];
            List<BE_VTA_VENTASDOC> lstDocTMP = new List<BE_VTA_VENTASDOC>();
            int intFila = 0;
            arrParametros[0, 0] = "n_idvta";
            arrParametros[0, 1] = "System.INT16";
            arrParametros[0, 2] = n_IdVenta.ToString();

            // ARMAMOS LA CADENA IN PARA ACTUALIZAR LAS GUIAS
            lstDocTMP = lstDocumentoOriginal;
            string c_cadenaIN = "";
            for (intFila = 0; intFila <= lstDocTMP.Count - 1; intFila++)
            {
                if (intFila == 0) { c_cadenaIN = c_cadenaIN + lstDocTMP[intFila].n_iddoc.ToString(); }
                if (intFila >= 1) { c_cadenaIN = c_cadenaIN + "," + lstDocTMP[intFila].n_iddoc.ToString(); }
            }
            DataTable dtresul = new DataTable();
            CD_vta_guias objguias = new CD_vta_guias();
            objguias.mysConec = mysConec;

            trans = mysConec.BeginTransaction();
            try
            {
                e_Ventas = TraerRegistro(n_IdVenta);

                if (e_Ventas.n_oriitem == 2)
                { 
                    // ACTUALIZAMOS EL ESTADO DE LAS GUIAS
                    if (c_cadenaIN != "")
                    {
                        if (objguias.QuitarDocVenta(c_cadenaIN) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            return booResult;
                        }
                    }
                }
                
                if (e_Ventas.n_oriitem == 4)
                {
                    // SI NO TIENE GUIAS TALVEZ TENGA PROFORMAS, LIBERAMOS LAS PROFORMAS QUE ESTE FACTURANDO
                    QuitarDocVenta2(n_IdVenta.ToString());
                }

                // BORRAMOS EL HIJO
                if (xMiFuncion.StoreEjecutar("vta_ventasdet_delete", arrParametros, mysConec) == true)
                {
                    arrParametros[0, 0] = "n_id";
                    arrParametros[0, 1] = "System.INT16";
                    arrParametros[0, 2] = n_IdVenta.ToString();

                    // BORRAMOS EL PADRE
                    booResult = xMiFuncion.StoreEjecutar("vta_ventas_anular", arrParametros, mysConec);

                    if (booResult == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                    }
                }

                string[,] arrParDelDiario = new string[7, 3] {
                                                {"n_lib", "System.INT16", e_Ventas.n_idlib.ToString()},
                                                {"n_ano", "System.INT16", e_Ventas.n_anotra.ToString()},
                                                {"n_mes", "System.INT16", e_Ventas.n_idmes.ToString()},    
                                                {"c_numasi", "System.STRING", e_Ventas.c_numreg.ToString()},    
                                                {"n_idemp", "System.INT16", e_Ventas.n_idemp.ToString()},
                                                {"n_iddoc", "System.INT16", e_Ventas.n_id.ToString()},
                                                {"n_idtipdoc", "System.INT16", e_Ventas.n_idtipdoc.ToString()}
                                           };
                booResult = xMiFuncion.StoreEjecutar("con_diario_anular", arrParDelDiario, mysConec);
                if (booResult == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                }

                trans.Commit();
                return booResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                //trans.Rollback();
                return booResult;
            }
        }
        public bool CambiarEstadoEnviado(int n_IdEmpresa, int n_IdAno, int n_IdMes)
        {
            bool booResult = false;

            string[,] arrParametros = new string[3, 3];

            arrParametros[0, 0] = "n_idemp";
            arrParametros[0, 1] = "System.INT32";
            arrParametros[0, 2] = n_IdEmpresa.ToString();

            arrParametros[1, 0] = "n_idano";
            arrParametros[1, 1] = "System.INT32";
            arrParametros[1, 2] = n_IdAno.ToString();

            arrParametros[2, 0] = "n_idmes";
            arrParametros[2, 1] = "System.INT32";
            arrParametros[2, 2] = n_IdMes.ToString();

            if (xMiFuncion.StoreEjecutar("vta_venta_desmarcarnoaceptados", arrParametros, mysConec) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            booResult = true;
            return booResult;
        }
        public bool AnularDocumento(int n_IdDocumento, int n_IdEmpresa)
        {
            bool booResult = false;

            string[,] arrParametros = new string[1, 3];

            arrParametros[0, 0] = "n_idvta";
            arrParametros[0, 1] = "System.INT16";
            arrParametros[0, 2] = n_IdDocumento.ToString();

            // BORRAMOS EL HIJO
            if (xMiFuncion.StoreEjecutar("vta_ventasdet_delete", arrParametros, mysConec) == true)
            {
                string[,] arrParametros2 = new string[2, 3];

                arrParametros2[0, 0] = "n_iddoc";
                arrParametros2[0, 1] = "System.INT16";
                arrParametros2[0, 2] = n_IdDocumento.ToString();

                arrParametros2[1, 0] = "n_idemp";
                arrParametros2[1, 1] = "System.INT16";
                arrParametros2[1, 2] = n_IdEmpresa.ToString();

                // BORRAMOS EL PADRE
                booResult = xMiFuncion.StoreEjecutar("vta_ventas_anulardocumento", arrParametros2, mysConec);

                if (booResult == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                }
            }
            return booResult;
        }
        public bool QuitarDocVenta(string c_CadenaIN)
        {
            if (c_CadenaIN == "") { return true; }
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"c_cadin", "System.STRING", c_CadenaIN.ToString()}
                                      };

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (xMiFuncion.StoreEjecutar("vta_ventas_quitardocven", arrParametros, mysConec) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return booResult;
            }
            booResult = true;
            return booResult;
        }
        public bool QuitarDocVenta2(string c_CadenaIN)
        {
            if (c_CadenaIN == "") { return true; }
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"c_cadin", "System.STRING", c_CadenaIN.ToString()}
                                      };

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (xMiFuncion.StoreEjecutar("vta_ventas_quitardocven2", arrParametros, mysConec) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return booResult;
            }
            booResult = true;
            return booResult;
        }
        public bool Consulta2(int n_IdEmpresa, string c_FechaInicio, string c_FchFinal, int n_IdMoneda, int n_TipoFecha, int n_TipoSaldo, int n_IdLibro, int n_TipoConsulta, string c_CadINCliente, string c_CadINItem)
        {
            bool b_result = false;

            string[,] arrParametros = new string[10, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FchFinal.ToString()},
                                            {"n_idmon", "System.INT32", n_IdMoneda.ToString()},
                                            {"n_tipfch", "System.INT32", n_TipoFecha.ToString()},
                                            {"n_tipsal", "System.INT32",n_TipoSaldo.ToString()},
                                            {"n_idlib", "System.INT32",n_IdLibro.ToString()},
                                            {"n_tipcon", "System.INT32",n_TipoConsulta.ToString()},
                                            {"c_cadincli", "System.STRING",c_CadINCliente.ToString()},
                                            {"c_cadinite", "System.STRING",c_CadINItem.ToString()}
                                      };
            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta9", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public DataTable Consulta3(int n_IdPuesto)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idpue", "System.INT16", n_IdPuesto.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("vta_ventas_consulta3", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable CodigoBarra(int n_IdEmpresa, double n_IdVenta)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idvta", "System.INT16", n_IdVenta.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("vta_ventas_codigobarra", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Consulta4(int n_IdEmpresa, int n_idTipoDocumento, int IdCliente)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idtipdoc", "System.INT16", n_idTipoDocumento.ToString()},
                                            {"n_idcli", "System.INT16", IdCliente.ToString()}  
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("vta_ventas_consulta4", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Consulta5(int n_IdEmpresa, int n_IdDocumento)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_id", "System.INT16", n_IdDocumento.ToString()}    
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("vta_ventas_consulta5", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Consulta8(int n_IdEmpresa, int n_IdCliente)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idcli", "System.INT16", n_IdCliente.ToString()}    
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("vta_ventas_consulta8", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Consulta9(int n_IdEmpresa, int n_AnoTrabajo)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("vta_ventas_consulta10", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public bool Consulta10(int n_IdEmpresa, int n_IdCliente)
        {
            bool b_result = false;

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idcli", "System.INT64",n_IdCliente.ToString()},
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta11", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;

            return b_result;
        }
        public bool CargarVentasPendEnvio(int n_idempresa, int n_IdTipoDocumento, int n_IdMes)
        {
            DataTable DtResultado = new DataTable();
            bool b_Result = false;

            string[,] arrVtaPar = new string[3, 3] {
                                            {"n_idemp", "System.INT64", n_idempresa.ToString()},
                                            {"n_idtipdoc", "System.INT32", n_IdTipoDocumento.ToString()},
                                            {"n_idmes", "System.INT32", n_IdMes.ToString()}
                                      };
            if ((n_IdTipoDocumento == 2)|| (n_IdTipoDocumento == 4))
            { 
                dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_cab", arrVtaPar, mysConec);
            }

            if ((n_IdTipoDocumento == 8) || (n_IdTipoDocumento == 9))
            { 
                dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_cab_ncnd", arrVtaPar, mysConec);
            }

            if (xMiFuncion.IntErrorNumber == 0)
            {
                dtLista2 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_det", arrVtaPar, mysConec);
                if (xMiFuncion.IntErrorNumber == 0)
                {
                    dtLista3 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_ley", arrVtaPar, mysConec);
                    if (xMiFuncion.IntErrorNumber == 0)
                    {
                        dtLista4 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_rel", arrVtaPar, mysConec);
                        if (xMiFuncion.IntErrorNumber == 0)
                        {
                            dtLista5 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_tri", arrVtaPar, mysConec);
                            if (xMiFuncion.IntErrorNumber != 0)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                return b_Result;
                            }
                        }
                        else
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            return b_Result;
                        }
                    }
                    else
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return b_Result;
                    }
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    return b_Result;
                }
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return b_Result;
            }
            b_Result = true;
            return b_Result;
        }
        public bool CargarVentasPendEnvioV12(int n_idempresa, int n_IdTipoDocumento, int n_IdMes)
        {
            DataTable DtResultado = new DataTable();
            bool b_Result = false;

            string[,] arrVtaPar = new string[3, 3] {
                                            {"n_idemp", "System.INT64", n_idempresa.ToString()},
                                            {"n_idtipdoc", "System.INT32", n_IdTipoDocumento.ToString()},
                                            {"n_idmes", "System.INT32", n_IdMes.ToString()}
                                      };
            if ((n_IdTipoDocumento == 2) || (n_IdTipoDocumento == 4))
            {
                dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_cab_v12", arrVtaPar, mysConec);
            }

            if ((n_IdTipoDocumento == 8) || (n_IdTipoDocumento == 9))
            {
                dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_cab_ncnd_v12", arrVtaPar, mysConec);
            }

            if (xMiFuncion.IntErrorNumber == 0)
            {
                dtLista2 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_det_v12", arrVtaPar, mysConec);
                if (xMiFuncion.IntErrorNumber == 0)
                {
                    dtLista3 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_ley_v12", arrVtaPar, mysConec);
                    if (xMiFuncion.IntErrorNumber == 0)
                    {
                        dtLista4 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_rel_v12", arrVtaPar, mysConec);
                        if (xMiFuncion.IntErrorNumber == 0)
                        {
                            dtLista5 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_tri_v12", arrVtaPar, mysConec);
                            if (xMiFuncion.IntErrorNumber != 0)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                return b_Result;
                            }
                        }
                        else
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            return b_Result;
                        }
                    }
                    else
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return b_Result;
                    }
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    return b_Result;
                }
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return b_Result;
            }
            b_Result = true;
            return b_Result;
        }
        public bool CargarVentasPendEnvioV12PorDias(int n_idempresa, int n_IdTipoDocumento, string c_FechaEmisionDocumento)
        {
            DataTable DtResultado = new DataTable();
            bool b_Result = false;

            string[,] arrVtaPar = new string[3, 3] {
                                            {"n_idemp", "System.INT64", n_idempresa.ToString()},
                                            {"n_idtipdoc", "System.INT32", n_IdTipoDocumento.ToString()},
                                            {"c_fchemi", "System.STRING", c_FechaEmisionDocumento.ToString()},
                                            
                                      };
            if ((n_IdTipoDocumento == 2) || (n_IdTipoDocumento == 4))
            {
                dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_cab_v12xdia", arrVtaPar, mysConec);
            }

            if ((n_IdTipoDocumento == 8) || (n_IdTipoDocumento == 9))
            {
                dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_cab_ncnd_v12xdia", arrVtaPar, mysConec);
            }

            if (xMiFuncion.IntErrorNumber == 0)
            {
                dtLista2 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_det_v12xdia", arrVtaPar, mysConec);
                if (xMiFuncion.IntErrorNumber == 0)
                {
                    dtLista3 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_ley_v12xdia", arrVtaPar, mysConec);
                    if (xMiFuncion.IntErrorNumber == 0)
                    {
                        dtLista4 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_rel_v12xdia", arrVtaPar, mysConec);
                        if (xMiFuncion.IntErrorNumber == 0)
                        {
                            dtLista5 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_tri_v12xdia", arrVtaPar, mysConec);
                            if (xMiFuncion.IntErrorNumber != 0)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                return b_Result;
                            }
                        }
                        else
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            return b_Result;
                        }
                    }
                    else
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return b_Result;
                    }
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    return b_Result;
                }
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return b_Result;
            }
            b_Result = true;
            return b_Result;
        }
        public bool ActualizarEstadoEnvio(int n_IdVenta, int IdEstado)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idvta", "System.INT64", n_IdVenta.ToString()},
                                            {"n_idest", "System.INT64", IdEstado.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("vta_ventas_actualizarestadoenvio", arrParametros, mysConec) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return booOk;
            }
            booOk = true;
            return booOk;
        }
        public bool ActualizarEstadoRecep(int n_IdEmpresa, int n_IdTipDocumento, string c_NumeroSerie, string c_NumeroDocumento, 
                                        string c_FechaAutorizacion, string c_HoraAutorizacion, string c_NombreArchivo)
        {
            bool b_result = false;

            string[,] arrParametros = new string[7, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idtipdoc", "System.INT16", n_IdTipDocumento.ToString()},    
                                            {"c_numser", "System.STRING", c_NumeroSerie.ToString()},    
                                            {"c_numdoc", "System.STRING", c_NumeroDocumento.ToString()},    
                                            {"c_fchaut", "System.STRING", c_FechaAutorizacion.ToString()},    
                                            {"c_horaut", "System.STRING", c_HoraAutorizacion.ToString()},    
                                            {"c_nomarchxml", "System.STRING", c_NombreArchivo.ToString()}    
                                      };

            if (xMiFuncion.StoreEjecutar("vta_ventas_actualizarestadorec", arrParametros, mysConec) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool TraerParaBaja(int n_IdEmpresa, int n_IdVenta)
        {
            bool b_result = false;

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idvta", "System.INT16", n_IdVenta.ToString()}
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_sunarchpla_baj", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
            
        }
        public bool AsientoCab(int n_IdEmpresa, int n_IdCompra)
        {
            bool b_result = false;
            xMiFuncion.IntErrorNumber =0;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                            {"n_id", "System.INT64",n_IdCompra.ToString()}
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_asientocab", arrParametros, mysConec);
            
            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            else
            {
                dtLista2 = xMiFuncion.StoreDTLLenar("vta_ventas_asientodet", arrParametros, mysConec);
                if (xMiFuncion.IntErrorNumber != 0)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
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
            if (xMiFuncion.StoreEjecutar("vta_ventas_insertarnumasi", arrParametros, mysConec) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return b_Result;
        }
        public DataTable DocumentosRetencion(int n_IdEmpresa, int n_IdCliente)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idcli", "System.INT16", n_IdCliente.ToString()},
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("vta_ventas_consulta6", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable DocumentosPercepcion(int n_IdEmpresa, int n_IdProveedor)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idcli", "System.INT16", n_IdProveedor.ToString()},
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("vta_ventas_consulta7", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public bool InsertarAnulado(int n_IdEmpresa, int n_Id, int n_IdTipDoc, string c_FchDocumento, int n_IdMes, int n_IdAno, string c_NumSer, string c_NumDoc, string c_FechaRegistro, double n_TipoCambio, int n_IdLibro)
        {
            bool b_result = false;
            n_IdGenerado = 0;
            string[,] arrParametros = new string[11, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_id", "System.INT64", n_Id.ToString()},
                                            {"n_idtipdoc", "System.INT32", n_IdTipDoc.ToString()},
                                            {"c_fchdoc", "System.STRING", c_FchDocumento},
                                            {"n_idmes", "System.INT32", n_IdMes.ToString()},
                                            {"n_idano", "System.INT32", n_IdAno.ToString()},
                                            {"c_numser", "System.STRING", c_NumSer.ToString()},
                                            {"c_numdoc", "System.STRING", c_NumDoc.ToString()},
                                            {"c_fchreg", "System.STRING", c_FechaRegistro},
                                            {"n_tc", "System.DOUBLE", n_TipoCambio.ToString()},
                                            {"n_idlib", "System.DOUBLE", n_IdLibro.ToString()}
                                      };

            b_result = xMiFuncion.StoreEjecutar("vta_ventas_insertaranulado", arrParametros, mysConec, 1);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                n_IdGenerado = xMiFuncion.intIdGenerado;
                b_result = true;
            }

            return b_result;
        }
        public void VentasAnuales(int n_IdEmpresa, int n_Tipo, int n_AnoTrabajo, string c_CadenaIN)
        {
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_tipo", "System.INT16", n_Tipo.ToString()},
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"c_cadin", "System.STRING", c_CadenaIN},
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_anualesunidades", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void VentasAnualesxCliente(int n_IdEmpresa, string c_FechaInicio, string c_FechaTermino)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FechaTermino.ToString()}
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta12", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void VentasAnualesxItems(int n_IdEmpresa, string c_FechaInicio, string c_FechaTermino)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FechaTermino.ToString()}
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta13", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool ActualizarEstadoNoEnviados(int n_Idregistro, int n_Estado)
        {
            bool b_result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_id", "System.INT32", n_Idregistro.ToString()},
                                            {"n_estado", "System.INT16", n_Estado.ToString()}
                                      };
            xMiFuncion.ReAbrirConeccion(mysConec);
            b_result = xMiFuncion.StoreEjecutar("vta_ventas_actualizarestadoaceptado", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return b_result;
        }
        public bool ActualizarEstadoNoEnviados(int n_Idregistro, int n_EstadoEnviado, int EsstadoAceptado)
        {
            bool b_result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"n_id", "System.INT32", n_Idregistro.ToString()},
                                            {"n_estenv", "System.INT16", n_EstadoEnviado.ToString()},
                                            {"n_estace", "System.INT16", EsstadoAceptado.ToString()}
                                      };
            xMiFuncion.ReAbrirConeccion(mysConec);
            b_result = xMiFuncion.StoreEjecutar("vta_ventas_actualizarestadoaceptado2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return b_result;
        }

        public DataTable UltimoPrecioCliente(int n_Idempresa, int n_IdCliente)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_Idempresa.ToString()},
                                            {"n_idcli", "System.INT16", n_IdCliente.ToString()}    
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("vta_ventas_ultimoprecio", arrParametros, mysConec);
            

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public void Consulta14(int n_IdEmpresa, int n_TipoMoneda, int n_TipoImporte)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_tipmon", "System.INT16", n_TipoMoneda.ToString()},
                                            {"n_tipimp", "System.INT16", n_TipoImporte.ToString()}
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta14", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
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

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta17", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void Consulta18(int n_IdEmpresa, int n_AnoTrabajo, int n_TipoMoneda, int n_TipoImporte)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"n_tipmon", "System.INT16", n_TipoMoneda.ToString()},
                                            {"n_tipval", "System.INT16", n_TipoImporte.ToString()}
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta18", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void Consulta19(int n_IdEmpresa, int n_AnoTrabajo, int n_TipoMoneda, int n_TipoImporte)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"n_tipmon", "System.INT16", n_TipoMoneda.ToString()},
                                            {"n_tipimp", "System.INT16", n_TipoImporte.ToString()}
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta19", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        //public void Consulta1(int n_idempresa)
        //{
        //    string[,] arrParametros = new string[1, 3] {
        //                                    {"n_idemp", "System.INT16",n_idempresa.ToString()}
        //                              };

        //    dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta15", arrParametros, mysConec);

        //    if (xMiFuncion.IntErrorNumber != 0)
        //    {
        //        dtLista1 = null;
        //        booOcurrioError = xMiFuncion.booOcurrioError;
        //        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
        //        IntErrorNumber = xMiFuncion.IntErrorNumber;
        //    }

        //    return;
        //}
        public void Consulta20(int n_idempresa)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT16",n_idempresa.ToString()}
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta20", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void Consulta6(string c_CadenaIN)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"c_cadin", "System.STRING",c_CadenaIN.ToString()},
                                      };
            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta16", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void BuscarDocumento(int n_IdCliente, int n_IdTipoDocumento, string c_NumeroSerie, string c_NumeroDocumento, int n_IdEmpresa)
        {
            string[,] arrParametros = new string[5, 3] {
                                            {"n_idcli", "System.INT32",n_IdCliente.ToString()},
                                            {"n_idtipdoc", "System.INT32",n_IdTipoDocumento.ToString()},
                                            {"c_numser", "System.STRING",c_NumeroSerie.ToString()},
                                            {"c_numdoc", "System.STRING",c_NumeroDocumento.ToString()},
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()}
                                      };
            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_buscardocumentos", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void NotaCreditoConsultaDetalle(int n_IdEmpresa, int n_IdAno, int n_IdMes)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32",n_IdAno.ToString()},
                                            {"n_mes", "System.STRING",n_IdMes.ToString()}
                                      };
            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta15", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public bool ImportarApertura(int n_IdEmpresa, int n_AnoTrabajo, int n_IdLibro)
        {
            bool b_result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.STRING", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_idlib", "System.INT32", n_IdLibro.ToString()}
                                      };

            xMiFuncion.StoreEjecutar("vta_ventas_apertura", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public DataTable Consulta21(int n_IdEmpresa, int n_AnoTrabajo)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("vta_ventas_consulta21", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public void Consulta22(int n_IdEmpresa, int n_IdLocal, string c_FechaVenta)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idloc", "System.INT16", n_IdLocal.ToString()},
                                            {"c_fchven", "System.STRING", c_FechaVenta.ToString()}
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta22", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return ;
        }
        public void Consulta23(int n_IdEmpresa, int n_AnoTrabajo, int n_TipoMoneda, int n_TipoImporte)
        {
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"n_tipmon", "System.INT16", n_TipoMoneda.ToString()},
                                            {"n_tipimp", "System.INT16", n_TipoImporte.ToString()}
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta23", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void Consulta25(int n_IdEmpresa, int n_IdTipDoc, string c_FechaVenta)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idtipdoc", "System.INT16", n_IdTipDoc.ToString()},
                                            {"c_fchdoc", "System.STRING", c_FechaVenta}
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta25", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void Consulta26(int n_IdEmpresa, int n_IdCliente, string c_CadenaIn)
        {
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idcli", "System.INT16", n_IdCliente.ToString()},
                                            
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta24", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void Consulta27(Int64 n_IdVenta)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idvta", "System.INT16", n_IdVenta.ToString()}
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta25", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void CambiarDocVentaAProforma(int n_IdDocumento, int n_IdTipDocConvertir, string c_NumeroSerie, string c_NumeroDocumento)
        {
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idvta", "System.INT16", n_IdDocumento.ToString()},
                                            {"n_idtipdoccon", "System.INT16", n_IdTipDocConvertir.ToString()},
                                            {"c_numser", "System.STRING",c_NumeroSerie},
                                            {"c_numdoc", "System.STRING",c_NumeroDocumento}
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_convertirproforma", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void Consulta28(int n_IdEmpresa, string c_FechaInicio, string c_FechaFinal, int n_IdLocal)
        {
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FechaFinal.ToString()},
                                            {"n_idlocal", "System.STRING", n_IdLocal.ToString()}
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("vta_ventas_consulta26", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public bool ActivarDocumento(int n_Idregistro)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idvta", "System.INT32", n_Idregistro.ToString()}
                                      };
            xMiFuncion.ReAbrirConeccion(mysConec);
            b_result = xMiFuncion.StoreEjecutar("vta_ventas_activardocumento", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return b_result;
        }
    }
}
