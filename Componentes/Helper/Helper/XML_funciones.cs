using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System;

namespace Helper
{
    public class XML_funciones
    {
        public XmlDocument DocXml;
        public string[,] DocAtributos;
        public XmlElement NodoPadre;
        public XmlDocument CrearXML(string c_NombreRoot)
        {
            //version="1.0" encoding="ISO-8859-1" standalone="no"
            int n_row = 0;
            int n_NumeroElementos = Convert.ToInt32(DocAtributos.GetLongLength(0)) - 1;
            string c_version = "1.0";
            string c_encondi = "ISO-8859-1";
            string c_standal = "no";
            // DECLARAMOS EL DOCUMENTO XML
            XmlDeclaration xmlDeclaration = DocXml.CreateXmlDeclaration(c_version, c_encondi, c_standal);

            XmlElement root = DocXml.DocumentElement;
            DocXml.InsertBefore(xmlDeclaration, root);

            // CREAMOS EL NODO PRINCIPAL
            XmlElement element1 = DocXml.CreateElement(string.Empty, c_NombreRoot, string.Empty);
            DocXml.AppendChild(element1);

            // AGREGAMOS LOS ATRIBUTOS DEL DOCUMENTO (LAS REFRENCIAS A LOS XSD)
            for (n_row = 0; n_row <= n_NumeroElementos; n_row++)
            {
                element1.SetAttribute(DocAtributos[n_row, 0], DocAtributos[n_row, 1]);
            }
            NodoPadre = element1;
            return DocXml;
        }
        public void GrabarArchivo(XmlDocument objDocumento, string c_RutaSalida, string c_NombreArchivo)
        {
            objDocumento.Save(c_RutaSalida + "\\" + c_NombreArchivo);
        }
        public XmlElement AgregarCarpeta(string c_Nombre, XmlElement ObjCarpetaPadre, string[,] arrAtributos, string c_Prefijo, string c_DireccionURL)
        {
            int n_row = 0;
            int n_NumeroElementos = Convert.ToInt32(arrAtributos.GetLongLength(0)) - 1;
            XmlElement objElemento;
            Helper.Comunes.Funciones funFun = new Helper.Comunes.Funciones();

            String prefix = c_Prefijo;
            String testNamespace = c_DireccionURL;

            if (funFun.NulosC(prefix) == "")
            {
                objElemento = DocXml.CreateElement(string.Empty, c_Nombre, string.Empty);
            }
            else
            {
                objElemento = DocXml.CreateElement(prefix, c_Nombre, testNamespace);
            }
            if (n_NumeroElementos != 0)
            {
                // RECORREMOS EL ARRAY DE ATRIBUTOS
                for (n_row = 0; n_row <= n_NumeroElementos - 1; n_row++)
                {
                    objElemento.SetAttribute(arrAtributos[0, n_row], arrAtributos[1, n_row]);
                }
            }
            ObjCarpetaPadre.AppendChild(objElemento);
            return objElemento;
        }
        public void AgregarArchivo(string c_Nombre, string c_Caption, XmlElement ObjCarpetaActual, string[,] arrAtributos, string c_Prefijo, string c_DireccionURL)
        {
            int n_row = 0;
            int n_NumeroElementos = Convert.ToInt32(arrAtributos.GetLongLength(0)) - 1;

            //XmlElement element6 = DocXml.CreateElement(string.Empty, c_Nombre, string.Empty);
            XmlElement element6 = DocXml.CreateElement(c_Prefijo, c_Nombre, c_DireccionURL);

            if (n_NumeroElementos != 0)
            {
                // RECORREMOS EL ARRAY DE ATRIBUTOS
                for (n_row = 0; n_row <= n_NumeroElementos - 1; n_row++)
                {
                    element6.SetAttribute(arrAtributos[0, n_row], arrAtributos[1, n_row]);
                }
            }
            XmlText text1 = DocXml.CreateTextNode(c_Caption);
            element6.AppendChild(text1);
            ObjCarpetaActual.AppendChild(element6);
        }
        public bool XML_LeerNodo(ref string[,] c_ListaNodos, string c_NombreArchivo)
        {
            bool b_Result = false;
            int n_row = 0;
            XmlDocument doc = new XmlDocument();
            doc.Load(c_NombreArchivo);

            int n_numnodos = Convert.ToInt32(c_ListaNodos.GetLongLength(0)) - 1;
            for (n_row = 0; n_row <= n_numnodos; n_row++)
            {
                string c_nodo = c_ListaNodos[n_row, 0];
                XmlNodeList elemList = doc.GetElementsByTagName(c_nodo);
                for (int i = 0; i < elemList.Count; i++)
                {
                    //Console.WriteLine(elemList[i].InnerXml);
                    c_ListaNodos[n_row, 1] = elemList[i].InnerXml;
                }
            }
            b_Result = true;
            return b_Result;
        }
    }
}
