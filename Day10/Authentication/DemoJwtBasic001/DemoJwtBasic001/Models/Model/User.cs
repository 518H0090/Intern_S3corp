using System.Text.Json.Serialization;

namespace DemoJwtBasic001.Models.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [JsonIgnore]
        public string Password { set; get; } = string.Empty;
    }
}
