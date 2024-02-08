using Newtonsoft.Json;

namespace Bham_Events.Models
{
    public class Restaurant
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("menuId")]
        public string MenuId { get; set; }

        public string Id { get; set; }
    }
}
