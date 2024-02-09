using Newtonsoft.Json;

namespace Bham_Events.Models
{
    public class Restaurant
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        // Directly include the Menu object instead of MenuId
        [JsonProperty("menu")]
        public Menu Menu { get; set; }

        // Id is typically assigned after fetching the data from Firebase
        // It doesn't need a JsonProperty because it's not part of the stored JSON
        public string Id { get; set; }
    }
}
