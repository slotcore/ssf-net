using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Ventas;
using Helper.Comunes;

namespace SIAC_DATOS.Ventas
{
    public class CD_vta_guias
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public int n_IdGenerado = 0;

        public DataTable dtGuiasDoc = new DataTable();
        public DataTable dtGuiasDatos = new DataTable();
        public DataTable dtLista = new DataTable();

        public MySqlConnection mysConec = new MySqlConnection();
        public List<BE_VTA_GUIASDET> LstDetalle = new List<BE_VTA_GUIASDET>();

        Helper.Comunes.Funciones funBas = new Helper.Comunes.Funciones();
        Funciones funFunciones = new Funciones();

        DatosMySql xMiFuncion = new DatosMySql();
        public DataTable Listar(int n_idempresa, int n_idmes, int n_anotra, int n_TipoOrigen)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT16",n_idempresa.ToString()},
                                            {"n_idmes", "System.INT16",n_idmes.ToString()},
                                            {"n_idano", "System.INT16",n_anotra.ToString()},
                                            {"n_tipori", "System.INT16",n_TipoOrigen.ToString()}
                                      };
            
            DtResultado = xMiFuncion.StoreDTLLenar("vta_guias_select", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public BE_VTA_GUIAS TraerRegistro(int n_IdRegistro)
        {
            BE_VTA_GUIAS Ent_Guias = new BE_VTA_GUIAS();
            DatosMySql xMiFuncion = new DatosMySql();
            int n_fila;
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("vta_guias_obtenerregistro", arrParametros, mysConec);

            if (DtResultado.Rows.Count != 0)
            {
                Ent_Guias.n_idemp = Convert.ToInt16(DtResultado.Rows[0]["n_idemp"].ToString());

                Ent_Guias.n_idano = Convert.ToInt16(DtResultado.Rows[0]["n_idano"].ToString());
                Ent_Guias.n_idmes = Convert.ToInt16(DtResultado.Rows[0]["n_idmes"].ToString());

                Ent_Guias.n_id = Convert.ToInt16(DtResultado.Rows[0]["n_id"].ToString());
                Ent_Guias.n_idcli = Convert.ToInt16(DtResultado.Rows[0]["n_idcli"].ToString());
                Ent_Guias.n_idtipdoc = Convert.ToInt16(DtResultado.Rows[0]["n_idtipdoc"].ToString());
                Ent_Guias.c_numser = DtResultado.Rows[0]["c_numser"].ToString();
                Ent_Guias.c_numdoc = DtResultado.Rows[0]["c_numdoc"].ToString();
                Ent_Guias.d_fchdoc = Convert.ToDateTime(DtResultado.Rows[0]["d_fchdoc"].ToString());
                Ent_Guias.n_idemptra = Convert.ToInt16(DtResultado.Rows[0]["n_idemptra"].ToString());
                Ent_Guias.n_idmottra = Convert.ToInt16(DtResultado.Rows[0]["n_idmottra"].ToString());
                Ent_Guias.c_numordcom =DtResultado.Rows[0]["c_numordcom"].ToString();
                Ent_Guias.n_idtipdocref = Convert.ToInt32(DtResultado.Rows[0]["n_idtipdocref"]);
                Ent_Guias.n_iddocref =  Convert.ToInt32(DtResultado.Rows[0]["n_iddocref"]);
                Ent_Guias.c_numdocref = DtResultado.Rows[0]["c_numdocref"].ToString();
                if (funBas.NulosC(DtResultado.Rows[0]["d_fchpeddocref"])!="")
                {
                    Ent_Guias.d_fchpeddocref = Convert.ToDateTime(DtResultado.Rows[0]["d_fchpeddocref"].ToString());
                }
                else
                {
                    Ent_Guias.d_fchpeddocref = null;
                }

                if (funBas.NulosC(DtResultado.Rows[0]["d_fchentdocref"]) != "")
                {
                    Ent_Guias.d_fchentdocref = Convert.ToDateTime(DtResultado.Rows[0]["d_fchentdocref"].ToString());
                }
                else
                {
                    Ent_Guias.d_fchentdocref = null;
                }

                Ent_Guias.n_idpunvencli = Convert.ToInt16(DtResultado.Rows[0]["n_idpunvencli"].ToString());
                Ent_Guias.c_dirpunlle = DtResultado.Rows[0]["c_dirpunlle"].ToString();
                Ent_Guias.c_dirpunpar = DtResultado.Rows[0]["c_dirpunpar"].ToString();
                Ent_Guias.n_idemptra = Convert.ToInt16(DtResultado.Rows[0]["n_idemptra"].ToString());
                Ent_Guias.n_idcho = Convert.ToInt16(DtResultado.Rows[0]["n_idcho"].ToString());
                Ent_Guias.n_idvehtra = Convert.ToInt16(DtResultado.Rows[0]["n_idvehtra"].ToString());
                Ent_Guias.n_anulado = Convert.ToInt16(DtResultado.Rows[0]["n_anulado"].ToString());
                Ent_Guias.n_tipgui = Convert.ToInt16(DtResultado.Rows[0]["n_tipgui"]);

                Ent_Guias.n_idpunpar = Convert.ToInt16(DtResultado.Rows[0]["n_idpunpar"]);
                Ent_Guias.n_idpunlle = Convert.ToInt16(DtResultado.Rows[0]["n_idpunlle"]);

                Ent_Guias.n_chkalming = Convert.ToInt16(DtResultado.Rows[0]["n_chkalming"]);
                Ent_Guias.n_chkalmsal = Convert.ToInt16(DtResultado.Rows[0]["n_chkalmsal"]);
                Ent_Guias.n_idclides = Convert.ToInt16(funFunciones.NulosN(DtResultado.Rows[0]["n_idclides"]));
                Ent_Guias.n_aplotrpro = Convert.ToInt16(funFunciones.NulosN(DtResultado.Rows[0]["n_aplotrpro"]));
                Ent_Guias.n_tipori = Convert.ToInt16(funFunciones.NulosN(DtResultado.Rows[0]["n_tipori"]));
            }

            //arrParametros = "";
            arrParametros[0, 0] = "n_idgui";
            arrParametros[0, 1] = "System.INT16";
            arrParametros[0, 2] = n_IdRegistro.ToString();

            // OBTENEMOS EL DETALLE DE LA GUIA
            List<BE_VTA_GUIASDET> LstDetalleTMP = new List<BE_VTA_GUIASDET>();
            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            DtResultado = xMiFuncion.StoreDTLLenar("vta_guiasdet_obtenerregistro", arrParametros, mysConec);
            if (DtResultado.Rows.Count != 0)
            {
                for (n_fila = 0; n_fila <= DtResultado.Rows.Count - 1; n_fila++)
                {                
                    BE_VTA_GUIASDET BE_Detalle = new BE_VTA_GUIASDET();

                    BE_Detalle.n_idgui = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idgui"].ToString());
                    BE_Detalle.n_idite = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idite"].ToString());
                    BE_Detalle.n_idtipexi = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idtipexi"].ToString());
                    BE_Detalle.n_idunimed = Convert.ToInt16(DtResultado.Rows[n_fila]["n_idunimed"].ToString());
                    BE_Detalle.n_canpro = Convert.ToDouble(DtResultado.Rows[n_fila]["n_canpro"].ToString());
                    BE_Detalle.c_numlot = DtResultado.Rows[n_fila]["c_numlot"].ToString();

                    BE_Detalle.n_preuni = Convert.ToDouble(funFunciones.NulosN(DtResultado.Rows[n_fila]["n_preuni"]));
                    BE_Detalle.n_preven = Convert.ToDouble(funFunciones.NulosN(DtResultado.Rows[n_fila]["n_preven"]));
                    BE_Detalle.n_candev = Convert.ToDouble(funFunciones.NulosN(DtResultado.Rows[n_fila]["n_candev"]));
                    LstDetalleTMP.Add(BE_Detalle);
                }
            }
            LstDetalle = LstDetalleTMP;

            // TRAEMOS LOS DOCUMENTOS DE REFERENCIA DE LA GUIA
            dtGuiasDoc = xMiFuncion.StoreDTLLenar("vta_guiasdoc_select", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idgui", "System.INT16", n_IdRegistro.ToString()}
                                      };
            dtGuiasDatos = xMiFuncion.StoreDTLLenar("vta_guiasdatos_select", arrParametros2, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return Ent_Guias;
        }
        public bool Insertar(BE_VTA_GUIAS entGuias, List<BE_VTA_GUIASDOC> lstGuiasDoc, BE_VTA_GUIASDATOS e_GuiaDatos)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int intFila = 0;

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (xMiFuncion.StoreEjecutar("vta_guias_insertar", entGuias, mysConec, 1) == true)
            {
                n_IdGenerado = Convert.ToInt16(xMiFuncion.intIdGenerado);
                // INSERTAMOS EL DETALLE DE LA GUIA
                for (intFila = 0; intFila <= LstDetalle.Count - 1; intFila++)
                {
                    LstDetalle[intFila].n_idgui = Convert.ToInt16(xMiFuncion.intIdGenerado);
                    if (xMiFuncion.StoreEjecutar("vta_guiasdet_insertar", LstDetalle[intFila], mysConec, null) == true)
                    {
                        booOk = true;
                    }
                    else
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return booOk;
                    }
                }

                if (e_GuiaDatos != null)
                { 
                    //INSERTAMOS LOS DATOS ADICIONALES DE LA GUIA
                    e_GuiaDatos.n_idgui = n_IdGenerado;
                    if (xMiFuncion.StoreEjecutar("vta_guiasdatos_insertar", e_GuiaDatos, mysConec, null) == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return booOk;
                    }
                }

                // CERRAMOS EL PEDIDO EN CASO DE SER 0 EL SALDO DEL PEDIDO
                DataTable dtResult = new DataTable();
                int n_row = 0;
                bool b_result = false;
                double n_saldo = 0;
                string[,] arrParametros3 = new string[1, 3] {
                                            {"n_idped", "System.INT16", entGuias.n_iddocref.ToString()},
                                      };

                dtResult = xMiFuncion.StoreDTLLenar("vta_pedidocli_mostrarentregas", arrParametros3, mysConec);
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count != 0)
                    {

                        for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                        {
                            n_saldo = (n_saldo + (Convert.ToDouble(funFunciones.NulosN(dtResult.Rows[n_row]["n_can"])) - Convert.ToDouble(funFunciones.NulosN(dtResult.Rows[n_row]["n_canent"]))));
                        }
                        int n_estado = 0;
                        if (n_saldo <= 0) { n_estado = 2; }
                        if (n_saldo > 0)  { n_estado = 1; }

                            string[,] arrParametros4 = new string[2, 3] {
                                                                        {"n_id", "System.INT64", entGuias.n_iddocref.ToString()},
                                                                        {"n_idest", "System.INT16", n_estado.ToString()}
                                                                    };

                            b_result = xMiFuncion.StoreEjecutar("vta_pedidocli_actualizarestado", arrParametros4, mysConec);
                            if (b_result == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                return booOk;
                            }
                    }
                }

                // INSERTAMOS LOS DOCUMENTOS DE REFERENCIA
                for (intFila = 0; intFila <= lstGuiasDoc.Count - 1; intFila++)
                {
                    lstGuiasDoc[intFila].n_idgui = Convert.ToInt16(n_IdGenerado);

                    string[,] arrParametros = new string[5, 3] {
                                            {"n_idgui", "System.INT16",lstGuiasDoc[intFila].n_idgui.ToString()},
                                            {"n_idtipdoc", "System.INT16",lstGuiasDoc[intFila].n_idtipdoc.ToString()},
                                            {"c_numdoc", "System.STRING",lstGuiasDoc[intFila].c_numdoc},
                                            {"n_iddoc", "System.INT16",lstGuiasDoc[intFila].n_iddoc.ToString()},
                                            {"n_tipori", "System.INT16",entGuias.n_tipori.ToString()}
                                      };

                    if (xMiFuncion.StoreEjecutar("vta_guiasdoc_insertar", arrParametros, mysConec) == true)
                    //lstGuiasDoc[intFila].n_idgui = Convert.ToInt16(n_IdGenerado);
                    //if (xMiFuncion.StoreEjecutar("vta_guiasdoc_insertar", lstGuiasDoc[intFila], mysConec, null) == true)
                    {
                        booOk = true;
                    }
                    else
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return booOk;
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
        public bool Actualizar(BE_VTA_GUIAS entGuias, List<BE_VTA_GUIASDOC> lstGuiasDoc, BE_VTA_GUIASDATOS e_GuiaDatos)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int intFila = 0;

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (xMiFuncion.StoreEjecutar("vta_guias_actualizar", entGuias, mysConec, null) == true)
            {
                // ELIMINAMOS LOS ITEMS PREVIOS
                string[,] arrParametros = new string[1, 3] {
                                            {"n_idgui", "System.INT64", entGuias.n_id.ToString()}
                                      };
                // ELIMINAMOS LOS DOCUMENTOS RELACIONADOS A LA GUIA

                string[,] arrParametros2 = new string[2, 3] {
                                            {"n_idgui", "System.INT64", entGuias.n_id.ToString()},
                                            {"n_tipori", "System.INT64", entGuias.n_tipori.ToString()}
                                      };

                if (xMiFuncion.StoreEjecutar("vta_guiasdoc_delete", arrParametros2, mysConec) == true)
                {
                    if (xMiFuncion.StoreEjecutar("vta_guiasdet_delete", arrParametros, mysConec) == true)
                    {
                        // SI LOS ITEMS SE ELIMINARON CON EXITO INSERTAMOS LOS NUEVOS ITEMS
                        for (intFila = 0; intFila <= LstDetalle.Count - 1; intFila++)
                        {
                            // INSERTAMOS EL DETALLE DE LA GUIA
                            if (xMiFuncion.StoreEjecutar("vta_guiasdet_insertar", LstDetalle[intFila], mysConec, null) == true)
                            {
                                DataTable dtResult = new DataTable();
                                int n_row = 0;
                                bool b_result = false;
                                double n_saldo = 0;
                                string[,] arrParametros3 = new string[1, 3] {
                                            {"n_idemp", "System.INT16", entGuias.n_iddocref.ToString()},
                                      };

                                dtResult = xMiFuncion.StoreDTLLenar("vta_pedidocli_mostrarentregas", arrParametros3, mysConec);
                                if (dtResult != null)
                                {
                                    if (dtResult.Rows.Count != 0)
                                    { 

                                        for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                                        {
                                            n_saldo = (n_saldo + (Convert.ToDouble(dtResult.Rows[n_row]["n_can"]) - Convert.ToDouble(dtResult.Rows[n_row]["n_canent"])));
                                        }
                                        int n_estado = 0;
                                        if (n_saldo <= 0) { n_estado = 2; }
                                        if (n_saldo > 0) { n_estado = 1; }

                                            string[,] arrParametros4 = new string[2, 3] {
                                                                        {"n_id", "System.INT64", entGuias.n_iddocref.ToString()},
                                                                        {"n_idest", "System.INT64", n_estado.ToString()}
                                                                    };

                                            b_result = xMiFuncion.StoreEjecutar("vta_pedidocli_actualizarestado", arrParametros4, mysConec);
                                            if (b_result == false)
                                            {
                                                booOcurrioError = xMiFuncion.booOcurrioError;
                                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                                return booOk;
                                            }
                                        
                                    }
                                }

                                //booOk = true;
                            }
                            else
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                return booOk;
                            }
                        }

                        if (e_GuiaDatos != null)
                        { 
                            // ELIMINAMOS LOS DATOS ADICIONALES
                            string[,] arrParametros5 = new string[1, 3] {
                                                                            {"n_idgui", "System.INT64", entGuias.n_id.ToString()}
                                                                        };
                            if (xMiFuncion.StoreEjecutar("vta_guiasdatos_delete", arrParametros5, mysConec) == false)
                            { 
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                return booOk;
                            }

                            //INSERTAMOS LOS DATOS ADICIONALES DE LA GUIA
                            e_GuiaDatos.n_idgui = entGuias.n_id;
                            if (xMiFuncion.StoreEjecutar("vta_guiasdatos_insertar", e_GuiaDatos, mysConec, null) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                return booOk;
                            }
                        }
                        // INSERTAMOS LOS DOCUMENTOS DE REFERENCIA
                        for (intFila = 0; intFila <= lstGuiasDoc.Count - 1; intFila++)
                        {
                            lstGuiasDoc[intFila].n_idgui = entGuias.n_id;
                            if (xMiFuncion.StoreEjecutar("vta_guiasdoc_insertar", lstGuiasDoc[intFila], mysConec, null) == true)
                            {
                                booOk = true;
                            }
                            else
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                return booOk;
                            }
                        }
                    }
                }
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            booOk = true;
            return booOk;
        }
        public bool Eliminar(int n_IdItem, int n_TipoOrigen)
        {
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdItem.ToString()}
                                      };

            string[,] arrParametros2 = new string[2, 3] {
                                            {"n_idgui", "System.INT16", n_IdItem.ToString()},
                                            {"n_tipori", "System.INT16", n_TipoOrigen.ToString()}
                                      };

            string[,] arrParametros5 = new string[1, 3] {
                                            {"n_idgui", "System.INT16", n_IdItem.ToString()}
                                      };

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);


            // CERRAMOS EL PEDIDO EN CASO DE SER 0 EL SALDO DEL PEDIDO
            DataTable dtResult = new DataTable();
            int n_row = 0;
            bool b_result = false;
            double n_saldo = 0;
            BE_VTA_GUIAS e_guia = new BE_VTA_GUIAS();

            e_guia = TraerRegistro(n_IdItem);
            string[,] arrParametros3 = new string[1, 3] {
                                            {"n_idped", "System.INT16", e_guia.n_iddocref.ToString()},
                                      };

            dtResult = xMiFuncion.StoreDTLLenar("vta_pedidocli_mostrarentregas", arrParametros3, mysConec);
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {

                    for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                    {
                        n_saldo = (n_saldo + (Convert.ToDouble(funFunciones.NulosN(dtResult.Rows[n_row]["n_can"])) - Convert.ToDouble(funFunciones.NulosN(dtResult.Rows[n_row]["n_canent"]))));
                    }
                    int n_estado = 0;
                    if (n_saldo <= 0) { n_estado = 2; }
                    if (n_saldo > 0) { n_estado = 1; }

                        string[,] arrParametros4 = new string[2, 3] {
                                                                        {"n_id", "System.INT64", e_guia.n_iddocref.ToString()},
                                                                        {"n_idest", "System.INT64", n_estado.ToString()}
                                                                    };

                        b_result = xMiFuncion.StoreEjecutar("vta_pedidocli_actualizarestado", arrParametros4, mysConec);
                        if (b_result == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            return booResult;
                        }
                    
                }
            }

            if (xMiFuncion.StoreEjecutar("vta_guiasdatos_delete", arrParametros5, mysConec) == true)
            {
                if (xMiFuncion.StoreEjecutar("vta_guiasdoc_delete", arrParametros2, mysConec) == true)
                {
                    if (xMiFuncion.StoreEjecutar("vta_guiasdet_delete", arrParametros2, mysConec) == true)
                    {
                        if (xMiFuncion.StoreEjecutar("vta_guias_delete", arrParametros, mysConec) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                        }
                        booResult = true;
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
        public bool CambiarEstado(int n_IdGuia, int n_IdEstado)
        {
            bool booResult = false;
            int n_tipori = 0;
            BE_VTA_GUIAS e_guias = new BE_VTA_GUIAS();
            e_guias = TraerRegistro(n_IdGuia);

            n_tipori = e_guias.n_tipori;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idgui", "System.INT16", n_IdGuia.ToString()},
                                            {"n_idest", "System.INT16", n_IdEstado.ToString()}
                                      };

            string[,] arrParametros2 = new string[2, 3] {
                                            {"n_idgui", "System.INT16", n_IdGuia.ToString()},
                                            {"n_tipori", "System.INT16", n_tipori.ToString()}
                                      };

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (xMiFuncion.StoreEjecutar("vta_guiasdoc_delete", arrParametros2, mysConec) == true)
            {
                if (xMiFuncion.StoreEjecutar("vta_guiasdet_delete", arrParametros2, mysConec) == true)
                {
                    if (xMiFuncion.StoreEjecutar("vta_guias_anular", arrParametros, mysConec) == false)
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
        public void GuiasTranportistasPendiente(int n_IdEmpresa, int n_TipoMovimiento, int n_TipoOrigen, int n_AnoTrabajo)
        {
            //n_TipoMovimiento   1 == SALIDA     2 == ENTRADA
            //n_TipoOrigen       1 == VENTAS     2 == LOGISTICA
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idtip", "System.INT16", n_TipoMovimiento.ToString()},
                                            {"n_tipori", "System.INT16", n_TipoOrigen.ToString()},
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_guias_guiastransportista", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool ActualizarSalidaEstado(int n_IdGuia, int n_Estado)
        {
            bool b_result = false;

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idgui", "System.INT64", n_IdGuia.ToString()},
                                            {"n_idest", "System.INT64",n_Estado.ToString()}
                                      };

            b_result = xMiFuncion.StoreEjecutar("vta_guias_actualizarestadosalida", arrParametros, mysConec);

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
        public bool ActualizarEntradaEstado(int n_IdGuia, int n_Estado)
        {
            bool b_result = false;

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idgui", "System.INT64", n_IdGuia.ToString()},
                                            {"n_idest", "System.INT64",n_Estado.ToString()}
                                      };

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            b_result = xMiFuncion.StoreEjecutar("vta_guias_actualizarestadoentrada", arrParametros, mysConec);

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
        public void Consulta1(int n_idempresa, int n_IdCliente)
        {
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16",n_idempresa.ToString()},
                                            {"n_idcli", "System.INT16",n_IdCliente.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_guias_consulta3", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void Consulta2 (string c_CadenaIN)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"c_cadin", "System.STRING",c_CadenaIN.ToString()},
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("vta_guias_consulta4", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public bool ActualizarDocVenta(int n_IdGuia, int n_IdDocumentoVenta)
        {
            bool booResult = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idgui", "System.STRING", n_IdGuia.ToString()},
                                            {"n_iddocven", "System.STRING", n_IdDocumentoVenta.ToString()}
                                      };

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (xMiFuncion.StoreEjecutar("vta_guias_actualizardocven", arrParametros, mysConec) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return booResult;
            }
            booResult = true;
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

            if (xMiFuncion.StoreEjecutar("vta_guias_quitardocven", arrParametros, mysConec) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return booResult;
            }
            booResult = true;
            return booResult;
        }
        public bool InsertarAnulado(int n_IdEmpresa, int n_Id, int n_IdTipDoc, DateTime d_FchDoc, int n_IdMes, int n_IdAno, string c_NumSer, string c_NumDoc, int n_TipoMovimiento)
        {
            bool b_result = false;

            string[,] arrParametros = new string[9, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_id", "System.INT32", n_Id.ToString()},
                                            {"n_idtipdoc", "System.INT32", n_IdTipDoc.ToString()},
                                            {"d_fchdoc", "System.DATETIME", d_FchDoc.ToString()},
                                            {"n_idmes", "System.INT32", n_IdMes.ToString()},
                                            {"n_idano", "System.INT32", n_IdAno.ToString()},
                                            {"c_numser", "System.STRING", c_NumSer.ToString()},
                                            {"c_numdoc", "System.STRING", c_NumDoc.ToString()},
                                            {"n_tipori", "System.INT32", n_TipoMovimiento.ToString()},
                                      };

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            b_result = xMiFuncion.StoreEjecutar("vta_guias_insertaranulado", arrParametros, mysConec);

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
        public bool Consulta10(int n_idempresa, int n_TipoReporte, string c_FechaInicio, string c_FechaTermino, string c_CadenaIN)
        {
            bool b_resul = false;

            string[,] arrParametros = new string[5, 3] {
                                            {"n_idemp", "System.INT16", n_idempresa.ToString()},
                                            {"n_tiprep", "System.INT16", n_TipoReporte.ToString()},
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FechaTermino.ToString()},
                                            {"c_cadin", "System.STRING", c_CadenaIN.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_guias_consulta10", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return b_resul;
            }
            b_resul = true;
            return b_resul;
        }
        public void GuiasAnuales(int n_IdEmpresa, int n_Tipo, int n_AnoTrabajo, int n_OrigenGuia, string CadenaIn)
        {
            string[,] arrParametros = new string[5, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_tipo", "System.INT16", n_Tipo.ToString()},
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"n_origen", "System.INT16", n_OrigenGuia.ToString()},
                                            {"c_Cadin", "System.STRING", CadenaIn.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_guias_anualesunidades", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return;
            }
    
            return;
        }
    }
}
