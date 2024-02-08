using Newtonsoft.Json;

namespace Bham_Events.Models
{
    public class MenuItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("cost")]
        public double Cost { get; set; }

        public string Id { get; set; }


    }
}
