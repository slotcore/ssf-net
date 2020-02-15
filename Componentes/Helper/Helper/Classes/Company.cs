using Newtonsoft.Json;
using System.Collections.Generic;

namespace Helper.Classes
{
    public class Company
    {
        [JsonProperty("companyId")]
        public int CompanyId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("identityDocumentNumber")]
        public string IdentityDocumentNumber { get; set; }

        [JsonProperty("homeAddress")]
        public string HomeAddress { get; set; }

        [JsonProperty("emailAddress")]
        public object EmailAddress { get; set; }

        [JsonProperty("identityDocumentId")]
        public int IdentityDocumentId { get; set; }

        [JsonProperty("identityDocument")]
        public object IdentityDocument { get; set; }
    }
}
