using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace Helper
{
    public class Cls_Api
    {
        public string ApiPost(string c_Direccion, object o_Entidad)
        {
            string ResultadoCadena = String.Empty;

            JsonSerializerSettings ConfigJson = new JsonSerializerSettings(); //Evitar que se serialicen los nodos cuyo valor sea nulo (Los ignora).
            ConfigJson.NullValueHandling = NullValueHandling.Ignore;
            byte[] Datos = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(o_Entidad, Formatting.None, ConfigJson));

            HttpWebRequest WReq = (HttpWebRequest)HttpWebRequest.Create(c_Direccion);
            WReq.ContentType = "application/json; charset=UTF-8";
            WReq.ContentLength = Datos.Length;
            WReq.Method = "POST";
            WReq.GetRequestStream().Write(Datos, 0, Datos.Length);

            HttpWebResponse res = (HttpWebResponse)WReq.GetResponse();
            Encoding Codificacion = ASCIIEncoding.UTF8;

            StreamReader SReader = new StreamReader(res.GetResponseStream(), Codificacion);
            ResultadoCadena = SReader.ReadToEnd();
            return ResultadoCadena;
        }
    }
}
