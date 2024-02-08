using Newtonsoft.Json;

namespace Bham_Events.Models
{
    public class RootObject
    {
        [JsonProperty("restaurants")]
        public Dictionary<string, Restaurant> Restaurants { get; set; }

        [JsonProperty("menus")]
        public Dictionary<string, Dictionary<string, MenuItem>> MenuItems { get; set; }

        [JsonProperty("menus")]
        public Dictionary<string, Dictionary<string, Menu>> Menus { get; set; }

        [JsonProperty("users")]
        public Dictionary<string, User> Users { get; set; }
    }
}
