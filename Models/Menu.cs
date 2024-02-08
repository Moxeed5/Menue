using Newtonsoft.Json;

namespace Bham_Events.Models
{
    public class Menu
    {
        [JsonProperty("menuItems")]
        public List<MenuItem> MenuItems { get; set; }

        // If you store an ID for menus themselves in Firebase
        public string Id { get; set; }
    }
}
