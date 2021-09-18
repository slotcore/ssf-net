using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Classes.Facturador
{
    public class Comprobante
    {
        [JsonProperty("cabecera")]
        public Cabecera Cabecera { get; set; }

        [JsonProperty("variablesGlobales")]
        public List<VariablesGlobale> VariablesGlobales { get; set; }

        [JsonProperty("detalle")]
        public List<Detalle> Detalle { get; set; }

        [JsonProperty("leyendas")]
        public List<Leyenda> Leyendas { get; set; }

        [JsonProperty("adicionalDetalle")]
        public List<AdicionalDetalle> AdicionalDetalle { get; set; }

        [JsonProperty("tributos")]
        public List<Tributo> Tributos { get; set; }
    }
}
