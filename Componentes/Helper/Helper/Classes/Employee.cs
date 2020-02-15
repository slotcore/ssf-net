using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Classes
{
    public class Employee
    {
        [JsonProperty("employeeId")]
        public int EmployeeId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("identityDocumentNumber")]
        public string IdentityDocumentNumber { get; set; }

        [JsonProperty("homeAddress")]
        public object HomeAddress { get; set; }

        [JsonProperty("emailAddress")]
        public object EmailAddress { get; set; }

        [JsonProperty("drivingLicenseNumber")]
        public object DrivingLicenseNumber { get; set; }

        [JsonProperty("licenseExpirationDate")]
        public DateTime LicenseExpirationDate { get; set; }

        [JsonProperty("identityDocumentId")]
        public int IdentityDocumentId { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }
    }
}
