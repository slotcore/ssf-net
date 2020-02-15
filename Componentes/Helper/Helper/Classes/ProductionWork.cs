using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Helper.Classes
{
    public class ProductionWork
    {
        [JsonProperty("productionWorkId")]
        public int ProductionWorkId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("startDateTime")]
        public DateTime StartDateTime { get; set; }

        [JsonProperty("endDateTime")]
        public DateTime EndDateTime { get; set; }

        [JsonProperty("companyId")]
        public int CompanyId { get; set; }

        [JsonProperty("company")]
        public Company Company { get; set; }

        [JsonProperty("productionOutputId")]
        public int ProductionOutputId { get; set; }

        [JsonProperty("productionOutput")]
        public ProductionOutput ProductionOutput { get; set; }

        [JsonProperty("productionWorkTasks")]
        public List<ProductionWorkTask> ProductionWorkTasks { get; set; }
    }
}
