using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Classes
{
    public class DiverseWorkTask
    {
        [JsonProperty("diverseWorkTaskId")]
        public int DiverseWorkTaskId { get; set; }

        [JsonProperty("startDateTime")]
        public DateTime StartDateTime { get; set; }

        [JsonProperty("endDateTime")]
        public DateTime EndDateTime { get; set; }

        [JsonProperty("qty")]
        public double Qty { get; set; }

        [JsonProperty("diverseWorkId")]
        public int DiverseWorkId { get; set; }

        [JsonProperty("taskWorkId")]
        public int TaskWorkId { get; set; }

        [JsonProperty("taskWork")]
        public TaskWork TaskWork { get; set; }

        [JsonProperty("diverseWorkTaskEmployees")]
        public List<DiverseWorkTaskEmployee> DiverseWorkTaskEmployees { get; set; }
    }
}
