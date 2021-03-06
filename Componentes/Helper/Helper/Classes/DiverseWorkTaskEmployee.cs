﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Classes
{
    public class DiverseWorkTaskEmployee
    {
        [JsonProperty("diverseWorkTaskEmployeeId")]
        public int DiverseWorkTaskEmployeeId { get; set; }

        [JsonProperty("startDateTime")]
        public DateTime StartDateTime { get; set; }

        [JsonProperty("endDateTime")]
        public DateTime EndDateTime { get; set; }

        [JsonProperty("qty")]
        public double Qty { get; set; }

        [JsonProperty("diverseWorkTaskId")]
        public int DiverseWorkTaskId { get; set; }

        [JsonProperty("employeeId")]
        public int EmployeeId { get; set; }

        [JsonProperty("employee")]
        public Employee Employee { get; set; }
    }
}
