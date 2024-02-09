using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bham_Events.Models
{
    public class RootObject
    {
        [JsonProperty("restaurants")]
        public Dictionary<string, Restaurant> Restaurants { get; set; }

        
        [JsonProperty("users")]
        public Dictionary<string, User> Users { get; set; }
    }
}
