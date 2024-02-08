using Newtonsoft.Json;

namespace Bham_Events.Models
{
    public class User
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("likedItems")]
        public Dictionary<string, bool> LikedItems { get; set; }
    }
}
