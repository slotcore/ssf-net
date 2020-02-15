using Newtonsoft.Json;
using System;

namespace Helper.Classes
{
    public class ProductionOutput
    {
        [JsonProperty("productionOutputId")]
        public int ProductionOutputId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("productionDate")]
        public DateTime ProductionDate { get; set; }

        [JsonProperty("companyId")]
        public int CompanyId { get; set; }

        [JsonProperty("company")]
        public Company Company { get; set; }

        [JsonProperty("employeeId")]
        public int EmployeeId { get; set; }

        [JsonProperty("employee")]
        public Employee Employee { get; set; }

        [JsonProperty("productionMethodId")]
        public int ProductionMethodId { get; set; }

        [JsonProperty("productionMethod")]
        public ProductionMethod ProductionMethod { get; set; }
    }
}
