using Newtonsoft.Json;

namespace Bham_Events.Models
{
    public class MenuItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        // Id is assigned when a new MenuItem is added to Firebase
        public string Id { get; set; }
    }
}
