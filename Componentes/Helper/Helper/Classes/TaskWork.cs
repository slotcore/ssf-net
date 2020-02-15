using Newtonsoft.Json;

namespace Helper.Classes
{
    public class TaskWork
    {

        [JsonProperty("taskWorkId")]
        public int TaskWorkId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
