using System.Text.Json.Serialization;

namespace RefreshToken0011.Models.DatabaseContext
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Character> Characters { set; get; }
    }
}
