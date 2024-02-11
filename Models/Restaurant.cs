using Newtonsoft.Json;

namespace Bham_Events.Models
{
    public class Restaurant
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        // Make Menu nullable and do not initialize it by default.
        // This way, a Menu must be explicitly added, avoiding automatic creation of empty menus.
        [JsonProperty("menu", NullValueHandling = NullValueHandling.Ignore)]
        public Menu? Menu { get; set; }

        // Ignore this property when sending to Firebase
        [JsonIgnore]
        public string Id { get; set; }
    }
}
