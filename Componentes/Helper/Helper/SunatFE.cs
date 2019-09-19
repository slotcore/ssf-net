using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.Services;

namespace Helper
{
    public class SunatFE
    {
        public XmlDocument DocXml;
        public DataTable dtOtrosConceptosFE;
        public DataTable dtDetalleFE;
        public MySql.Data.MySqlClient.MySqlConnection MyConeccion;
        
        public string C_RUTAARCHIVO;

        public string C_CONTRI_RUC;
        public string C_CONTRI_RAZSOC;
        public string C_CONTRI_NUMSERDOC;
        public string C_CONTRI_NUMCORDOC;

        DataTable dtDocumento = new DataTable();
        DataTable dtDocOrig = new DataTable();
        DataTable dtAtributos = new DataTable();
        DataTable dtDocItem= new DataTable();

        public string c_versionubl;
        public string c_custonubl;
        public string c_numfac;
        public string c_fchemi;
        public string c_tipdoc;
        public string c_codmon;
        public string c_numruc;
        public string c_nomemp;
        public string c_tipdocide;
        public string c_ubigeo;
        public string c_nomcalle;
        public string c_nomurbnanizacion;
        public string c_nomdepartamento;
        public string c_nomprovincia;
        public string c_nomdistrito;
        public string c_codigopais;
        public string c_numruccli;
        public string c_tipdocidecli;
        public string c_nomcli;
        public string c_impigv;
        public string c_codtipimp;
        public string c_nomtipimp;
        public string c_dattipimp;
        public string c_imptotdoc;
        public string c_implet;
        public string c_impletdato;
        public XmlDocument GenerarDocumento()
        {
            Helper.DatosMySql fudDat = new Helper.DatosMySql();
            Helper.Genericas funGen = new Helper.Genericas();
            int n_row = 0;
            
            string[,] arrAtributoNodo = new string[1, 2] {
                                            {"n_iddoc", "System.INT64"}
                                      };

            string[,] arrParametros = new string[1, 3] {
                                            {"n_iddoc", "System.INT64", "1"}
                                      };

            dtDocOrig = fudDat.StoreDTLLenar("sun_docelectronicos_consulta1", arrParametros, MyConeccion);
            dtDocumento = funGen.DataTableFiltrar(dtDocOrig, "n_id <= 74");
            dtDocItem = funGen.DataTableFiltrar(dtDocOrig, "n_id >= 75");
            dtAtributos = fudDat.StoreDTLLenar("sun_docelectronicosatri_consulta1", arrParametros, MyConeccion);

            XmlElement objNodoPadre;
            Helper.XML_funciones FunXml = new Helper.XML_funciones();
            string[,] arrAtributo = new string[10, 2] {
                                            {"xmlns", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"},
                                            {"xmlns:cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"},
                                            {"xmlns:cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"},
                                            {"xmlns:ccts", "urn:un:unece:uncefact:documentation:2"},
                                            {"xmlns:ds", "http://www.w3.org/2000/09/xmldsig#"},
                                            {"xmlns:ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2"},
                                            {"xmlns:qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2"},
                                            {"xmlns:sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1"},
                                            {"xmlns:udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2"},
                                            {"xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance"}
                                      };
            
            
            FunXml.DocAtributos = arrAtributo;         // CARGAMOS LOS ATRIBUTOS DEL DOCUMENTO
            FunXml.DocXml = DocXml;                    // PASAMOS LA VARIABLE XML
            DocXml = FunXml.CrearXML("Invoice");       // CREAMOS EL DOCUMENTO XML INDICANDO EL NOMBRE DE LA RAIZ
            objNodoPadre = FunXml.NodoPadre;
            CargarFE();
            AgregarNodo(1, dtDocumento, dtAtributos, objNodoPadre, "");

            //AGREGAMOS LOS ITEMS DE LA FACTURA ELECTRONICA
            for (n_row = 0; n_row <= dtDetalleFE.Rows.Count - 1; n_row++)
            {
                AgregarItems(1, dtDocItem, dtAtributos, objNodoPadre, "", n_row);
            }
            string c_tipcom = "01";
            string c_nomarch = C_CONTRI_RUC + "-" + c_tipcom + "-" + C_CONTRI_NUMSERDOC + "-" + C_CONTRI_NUMCORDOC + ".xml";
            FunXml.GrabarArchivo(DocXml, C_RUTAARCHIVO, c_nomarch);
            return DocXml;
        }

        public void Enviar()
        {
            /*  SERVICE REFERENCE-SPECIFIC CODE  
           ServiceReference1.ServiceNowSoapClient soapClient = new ServiceReference1.ServiceNowSoapClient();
           soapClient.ClientCredentials.UserName.UserName ="itil";
           soapClient.ClientCredentials.UserName.Password ="itil"; 
           
           ServiceReference1.insert insert = new ExampleWebServiceForWiki.ServiceReference1.insert();
           ServiceReference1.insertResponse response = new ExampleWebServiceForWiki.ServiceReference1.insertResponse();
           //   END OF SERVICE REFERENCE CODE    */

            //   WEB REFERENCE-SPECIFIC CODE
            //WebReference1.ServiceNow_incident soapClient = new ExampleWebServiceForWiki.WebReference1.ServiceNow_incident();
            //WS_FacturaE soapClient = new WS_FacturaE();
            
            
            
            //System.Net.ICredentials cred = new System.Net.NetworkCredential("itil", "itil");
            //soapClient.Credentials = cred;

            //WebReference1.insert insert = new WebReference1.insert();
            //WebReference1.insertResponse response = new WebReference1.insertResponse();
            ////   END OF WEB REFERENCE CODE

            //insert.category = "Category";
            //insert.comments = "Comments";
            //insert.short_description = "My short description";

            //try
            //{
            //    response = soapClient.insert(insert);
            //    this.richTextBoxResult.Text = "Incident Number: " + response.number + "\n";
            //    this.richTextBoxResult.Text += "Sys_id: " + response.sys_id;
            //}
            //catch (Exception error)
            //{
            //    this.richTextBoxResult.Text = error.Message;
            //}          
        }
        void AgregarItems(int n_IdNivel, DataTable dtDocumento, DataTable dtAtributos, XmlElement objNodoAnterior, string c_RutaArbol, int n_IdFilaDetalle)
        {
            DataTable dtResul = new DataTable();
            DataTable dtLista = new DataTable();
            DataTable dtNodoHijo = new DataTable();
            Helper.Comunes.Funciones Fun = new Helper.Comunes.Funciones();
            Helper.Genericas funGen = new Helper.Genericas();
            Helper.XML_funciones FunXml = new Helper.XML_funciones();
            XmlElement element1;
            int n_row = 0;
            string c_nomlista = "";
            string c_texto = "";
            string c_arbol = "";
            string c_prefi = "";
            string c_dirur = "";
            string c_valor = "";
            int n_tipnod = 0;
            int n_eslista = 0;
            string c_NewRutaArbol = "";
            string[,] arrAtributo = new string[1, 2] {
                                            {"n_iddoc", "System.INT64"}
                                      };

            dtResul = funGen.DataTableFiltrar(dtDocumento, "(n_numniv = " + n_IdNivel.ToString() + ") AND (c_arbol like '" + c_RutaArbol + "*')");              // FILTRAMOS NIVEL 6

            if (dtResul.Rows.Count != 0)
            {
                for (n_row = 0; n_row <= dtResul.Rows.Count - 1; n_row++)
                {
                    element1 = null;
                    c_texto = dtResul.Rows[n_row]["c_des"].ToString();
                    c_valor = dtResul.Rows[n_row]["c_valor"].ToString();
                    c_arbol = dtResul.Rows[n_row]["c_arbol"].ToString();
                    c_prefi = dtResul.Rows[n_row]["c_prefijo"].ToString();
                    c_dirur = dtResul.Rows[n_row]["c_dirurl"].ToString();
                    n_tipnod = Convert.ToInt16(dtResul.Rows[n_row]["n_tipo"]);
                    c_NewRutaArbol = dtResul.Rows[n_row]["c_arbol"].ToString();
                    n_eslista = Convert.ToInt16(Fun.NulosN(dtResul.Rows[n_row]["n_lista"]));
                    arrAtributo = LlenarArrayAtributos(Convert.ToInt32(dtResul.Rows[n_row]["n_id"]), dtAtributos);

                    FunXml.DocXml = DocXml;
                    if (n_tipnod == 1)
                    {
                        string c_campo = dtResul.Rows[n_row]["c_campo"].ToString();
                        c_valor = dtDetalleFE.Rows[n_IdFilaDetalle][c_campo].ToString();
                        FunXml.AgregarArchivo(c_texto, c_valor, objNodoAnterior, arrAtributo, c_prefi, c_dirur);
                    }
                    else
                    {
                        FunXml.DocXml = DocXml;

                        // PREGUNTAMOS SI EL NUEVO NODO ES UNA LISTA
                        if (n_eslista == 1)
                        {
                            // SI ES UNA LISTA ESCRIBIMOS LOS DATOS DEL ALISTA
                            c_nomlista = dtResul.Rows[n_row]["c_nomlista"].ToString();
                            int n_fil = 0;
                            int n_lisfil = 0;

                            dtLista = BucarDatosLista(c_nomlista);     // OBTENEMOS EL DATATABLE CORRECTO PARA ESCRIBIR LOS DATOS

                            string c_cond = "c_arbol like '" + c_arbol + "*' AND n_numniv > " + n_IdNivel.ToString() + " ";
                            dtNodoHijo = funGen.DataTableFiltrar(dtDocumento, c_cond);

                            for (n_lisfil = 0; n_lisfil <= dtLista.Rows.Count - 1; n_lisfil++)
                            {
                                // AGREGAMOS EL NODO PADRE
                                element1 = FunXml.AgregarCarpeta(c_texto, objNodoAnterior, arrAtributo, c_prefi, c_dirur);

                                for (n_fil = 0; n_fil <= dtNodoHijo.Rows.Count - 1; n_fil++)
                                {
                                    string c_textohijo = dtNodoHijo.Rows[n_fil]["c_des"].ToString();
                                    string c_valorhijo = dtNodoHijo.Rows[n_fil]["c_des"].ToString();       // OBTENEMOS EL NOMBRE DEL CAMPO
                                    //c_valorhijo = dtLista.Rows[n_lisfil][c_valorhijo].ToString();       // TRAEMOS EL DATO DE LA LISTA

                                    c_valorhijo = dtDetalleFE.Rows[n_IdFilaDetalle][c_valorhijo].ToString();

                                    FunXml.AgregarArchivo(c_textohijo, c_valorhijo, element1, arrAtributo, c_prefi, c_dirur);
                                }
                            }
                        }
                        else
                        {
                            element1 = FunXml.AgregarCarpeta(c_texto, objNodoAnterior, arrAtributo, c_prefi, c_dirur);

                            int n_NuevoNivel = n_IdNivel + 1;
                            AgregarItems(n_NuevoNivel, dtDocumento, dtAtributos, element1, c_NewRutaArbol, n_IdFilaDetalle);
                        }
                    }
                }
            }
        }
        void CargarFE()
        {
            AsignarDato("c_versionubl", c_versionubl);
            AsignarDato("c_custonubl", c_custonubl);
            AsignarDato("c_numfac",c_numfac);
            AsignarDato("c_fchemi",c_fchemi);
            AsignarDato("c_tipdoc",c_tipdoc);
            AsignarDato("c_codmon",c_codmon);
            AsignarDato("c_numruc",c_numruc);
            AsignarDato("c_nomemp",c_nomemp);
            AsignarDato("c_tipdocide",c_tipdocide);
            AsignarDato("c_ubigeo",c_ubigeo);
            AsignarDato("c_nomcalle",c_nomcalle);
            AsignarDato("c_nomurbnanizacion",c_nomurbnanizacion);
            AsignarDato("c_nomdepartamento",c_nomdepartamento);
            AsignarDato("c_nomprovincia",c_nomprovincia);
            AsignarDato("c_nomdistrito",c_nomdistrito);
            AsignarDato("c_codigopais",c_codigopais);
            AsignarDato("c_numruccli",c_numruccli);
            AsignarDato("c_tipdocidecli",c_tipdocidecli);
            AsignarDato("c_nomcli",c_nomcli);
            AsignarDato("c_impigv",c_impigv);
            AsignarDato("c_codtipimp",c_codtipimp);
            AsignarDato("c_nomtipimp",c_nomtipimp);
            AsignarDato("c_dattipimp",c_dattipimp);
            AsignarDato("c_imptotdoc", c_imptotdoc);
            AsignarDato("c_implet", c_implet);
            AsignarDato("c_impletdato", c_impletdato);
        }
        void AsignarDato(string c_NombreCampo, string c_NuevoValor)
        {
            int n_row = 0;
            string c_nomcam = "";
            for (n_row = 0; n_row <= dtDocumento.Rows.Count - 1; n_row++)
            {
                c_nomcam = dtDocumento.Rows[n_row]["c_campo"].ToString();
                if (c_nomcam == c_NombreCampo)
                {
                    dtDocumento.Rows[n_row]["c_valor"] = c_NuevoValor;
                }
            }
        }
        void AgregarNodo(int n_IdNivel, DataTable dtDocumento, DataTable dtAtributos, XmlElement objNodoAnterior, string c_RutaArbol)
        {
            DataTable dtResul = new DataTable();
            DataTable dtLista = new DataTable();
            DataTable dtNodoHijo = new DataTable();
            Helper.Comunes.Funciones Fun = new Helper.Comunes.Funciones();
            Helper.Genericas funGen = new Helper.Genericas();
            Helper.XML_funciones FunXml = new Helper.XML_funciones();
            XmlElement element1;
            int n_row = 0;
            string c_nomlista = "";
            string c_texto = "";
            string c_arbol = "";
            string c_prefi = "";
            string c_dirur = "";
            string c_valor = "";
            int n_tipnod = 0;
            int n_eslista = 0;
            string c_NewRutaArbol = "";
            string[,] arrAtributo = new string[1, 2] {
                                            {"n_iddoc", "System.INT64"}
                                      };

            dtResul = funGen.DataTableFiltrar(dtDocumento, "(n_numniv = " + n_IdNivel.ToString() + ") AND (c_arbol like '" + c_RutaArbol + "*')");              // FILTRAMOS NIVEL 6

            if (dtResul.Rows.Count != 0)
            {
                for (n_row = 0; n_row <= dtResul.Rows.Count - 1; n_row++)
                {
                    element1 = null;
                    c_texto = dtResul.Rows[n_row]["c_des"].ToString();
                    c_valor = dtResul.Rows[n_row]["c_valor"].ToString();
                    c_arbol = dtResul.Rows[n_row]["c_arbol"].ToString();
                    c_prefi = dtResul.Rows[n_row]["c_prefijo"].ToString();
                    c_dirur = dtResul.Rows[n_row]["c_dirurl"].ToString();
                    n_tipnod = Convert.ToInt16(dtResul.Rows[n_row]["n_tipo"]);
                    c_NewRutaArbol = dtResul.Rows[n_row]["c_arbol"].ToString();
                    n_eslista = Convert.ToInt16(Fun.NulosN(dtResul.Rows[n_row]["n_lista"]));
                    arrAtributo = LlenarArrayAtributos(Convert.ToInt32(dtResul.Rows[n_row]["n_id"]), dtAtributos);

                    FunXml.DocXml = DocXml;
                    if (n_tipnod == 1)
                    {
                        FunXml.AgregarArchivo(c_texto, c_valor, objNodoAnterior, arrAtributo, c_prefi, c_dirur);
                    }
                    else
                    {
                        FunXml.DocXml = DocXml;

                        // PREGUNTAMOS SI EL NUEVO NODO ES UNA LISTA
                        if (n_eslista == 1)
                        {
                            // SI ES UNA LISTA ESCRIBIMOS LOS DATOS DEL ALISTA
                            c_nomlista = dtResul.Rows[n_row]["c_nomlista"].ToString();
                            int n_fil = 0;
                            int n_lisfil = 0;

                            dtLista = BucarDatosLista(c_nomlista);     // OBTENEMOS EL DATATABLE CORRECTO PARA ESCRIBIR LOS DATOS

                            string c_cond = "c_arbol like '" + c_arbol + "*' AND n_numniv > " + n_IdNivel.ToString() + " ";
                            dtNodoHijo = funGen.DataTableFiltrar(dtDocumento, c_cond);

                            for (n_lisfil = 0; n_lisfil <= dtLista.Rows.Count - 1; n_lisfil++)
                            {
                                // AGREGAMOS EL NODO PADRE
                                element1 = FunXml.AgregarCarpeta(c_texto, objNodoAnterior, arrAtributo, c_prefi, c_dirur);
                                
                                for (n_fil = 0; n_fil <= dtNodoHijo.Rows.Count - 1; n_fil++)
                                {
                                    string c_textohijo = dtNodoHijo.Rows[n_fil]["c_des"].ToString();
                                    string c_valorhijo = dtNodoHijo.Rows[n_fil]["c_des"].ToString();       // OBTENEMOS EL NOMBRE DEL CAMPO
                                    c_valorhijo = dtLista.Rows[n_lisfil][c_valorhijo].ToString();       // TRAEMOS EL DATO DE LA LISTA
                                    FunXml.AgregarArchivo(c_textohijo, c_valorhijo, element1, arrAtributo, c_prefi, c_dirur);
                                }
                            }
                        }
                        else
                        {
                            element1 = FunXml.AgregarCarpeta(c_texto, objNodoAnterior, arrAtributo, c_prefi, c_dirur);

                            int n_NuevoNivel = n_IdNivel + 1;
                            AgregarNodo(n_NuevoNivel, dtDocumento, dtAtributos, element1, c_NewRutaArbol);
                        }
                    }
                    //int n_NuevoNivel = n_IdNivel + 1;
                    //AgregarNodo(n_NuevoNivel, dtDocumento, dtAtributos, element1, c_NewRutaArbol);
                }
            }
        }
        DataTable BucarDatosLista( string c_NombreLista)
        {
            DataTable dtResult = new DataTable();
            if (c_NombreLista == "dtOtrosConceptos")
            {
                dtResult = dtOtrosConceptosFE;
            }
            return dtResult;
        }
        string[,] LlenarArrayAtributos(int n_idNodo, DataTable dtAtributos)
        {
            int n_row = 0;
            string[,] arrAtributos = new string[1, 1];

            Helper.Genericas funGen = new Helper.Genericas();
            DataTable dtResult = new DataTable();
            dtResult = funGen.DataTableFiltrar(dtAtributos, "n_idnoddoc = " + n_idNodo.ToString() + "");

            if (dtResult.Rows.Count != 0)
            {
                string[,] arrAtributos2 = new string[2, dtResult.Rows.Count];

                for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                {
                    arrAtributos2[0, n_row] = dtResult.Rows[n_row]["c_nom"].ToString();
                    arrAtributos2[1, n_row] = dtResult.Rows[n_row]["c_valor"].ToString();
                }
                arrAtributos = arrAtributos2;
            }

            return arrAtributos;
        }
    }
}
