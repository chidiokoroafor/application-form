using Newtonsoft.Json;

namespace ApplicationFormServer.Models
{
    public class Choice
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
