using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Helper.Classes
{
    public class ProductionWorkTask
    {

        [JsonProperty("productionWorkTaskId")]
        public int ProductionWorkTaskId { get; set; }

        [JsonProperty("startDateTime")]
        public DateTime StartDateTime { get; set; }

        [JsonProperty("endDateTime")]
        public DateTime EndDateTime { get; set; }

        [JsonProperty("qty")]
        public double Qty { get; set; }

        [JsonProperty("productionWorkId")]
        public int ProductionWorkId { get; set; }

        [JsonProperty("taskWorkId")]
        public int TaskWorkId { get; set; }

        [JsonProperty("taskWork")]
        public TaskWork TaskWork { get; set; }

        [JsonProperty("productionWorkTaskEmployees")]
        public List<ProductionWorkTaskEmployee> ProductionWorkTaskEmployees { get; set; }
    }
}
