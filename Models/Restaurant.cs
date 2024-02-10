using Newtonsoft.Json;

namespace Bham_Events.Models
{
    public class Restaurant
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("menu")]
        public Menu Menu { get; set; }

        // Ignore this property when sending to Firebase
        [JsonIgnore]
        public string Id { get; set; }
    }
}
