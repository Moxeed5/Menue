using Newtonsoft.Json;

namespace Bham_Events.Models
{
    public class Menu
    {
        [JsonProperty("menuItems")]
        public List<MenuItem> MenuItems { get; set; }
    }
}
