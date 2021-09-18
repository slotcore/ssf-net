using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Classes.Facturador
{
    public class Cabecera
    {
        [JsonProperty("tipOperacion")]
        public string TipOperacion { get; set; }

        [JsonProperty("fecEmision")]
        public string FecEmision { get; set; }

        [JsonProperty("horEmision")]
        public string HorEmision { get; set; }

        [JsonProperty("fecVencimiento")]
        public string FecVencimiento { get; set; }

        [JsonProperty("codLocalEmisor")]
        public string CodLocalEmisor { get; set; }

        [JsonProperty("tipDocUsuario")]
        public string TipDocUsuario { get; set; }

        [JsonProperty("numDocUsuario")]
        public string NumDocUsuario { get; set; }

        [JsonProperty("rznSocialUsuario")]
        public string RznSocialUsuario { get; set; }

        [JsonProperty("tipMoneda")]
        public string TipMoneda { get; set; }

        [JsonProperty("sumTotTributos")]
        public string SumTotTributos { get; set; }

        [JsonProperty("sumTotValVenta")]
        public string SumTotValVenta { get; set; }

        [JsonProperty("sumPrecioVenta")]
        public string SumPrecioVenta { get; set; }

        [JsonProperty("sumDescTotal")]
        public string SumDescTotal { get; set; }

        [JsonProperty("sumOtrosCargos")]
        public string SumOtrosCargos { get; set; }

        [JsonProperty("sumTotalAnticipos")]
        public string SumTotalAnticipos { get; set; }

        [JsonProperty("sumImpVenta")]
        public string SumImpVenta { get; set; }

        [JsonProperty("ublVersionId")]
        public string UblVersionId { get; set; }

        [JsonProperty("customizationId")]
        public string CustomizationId { get; set; }
    }
}
