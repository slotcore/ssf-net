using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Classes.Facturador
{
    public class VariablesGlobale
    {
        [JsonProperty("tipVariableGlobal")]
        public string TipVariableGlobal { get; set; }

        [JsonProperty("codTipoVariableGlobal")]
        public string CodTipoVariableGlobal { get; set; }

        [JsonProperty("porVariableGlobal")]
        public string PorVariableGlobal { get; set; }

        [JsonProperty("monMontoVariableGlobal")]
        public string MonMontoVariableGlobal { get; set; }

        [JsonProperty("mtoVariableGlobal")]
        public string MtoVariableGlobal { get; set; }

        [JsonProperty("monBaseImponibleVariableGlobal")]
        public string MonBaseImponibleVariableGlobal { get; set; }

        [JsonProperty("mtoBaseImpVariableGlobal")]
        public string MtoBaseImpVariableGlobal { get; set; }
    }
}
