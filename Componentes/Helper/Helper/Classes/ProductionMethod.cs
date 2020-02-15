using Newtonsoft.Json;

namespace Helper.Classes
{
    public class ProductionMethod
    {

        [JsonProperty("productionMethodId")]
        public int ProductionMethodId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("product")]
        public Product Product { get; set; }
    }
}
