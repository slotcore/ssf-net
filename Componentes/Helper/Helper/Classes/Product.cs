using Newtonsoft.Json;
using System.Collections.Generic;

namespace Helper.Classes
{
    public class Product
    {
        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("companyId")]
        public int CompanyId { get; set; }

        [JsonProperty("company")]
        public Company Company { get; set; }
    }
}
