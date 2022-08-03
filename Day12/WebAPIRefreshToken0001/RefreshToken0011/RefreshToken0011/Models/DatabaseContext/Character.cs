using System.Text.Json.Serialization;

namespace RefreshToken0011.Models.DatabaseContext
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int level { set; get; } = 1;

        [JsonIgnore]
        public User User { set; get; }
        public int UserId { set; get; }

        [JsonIgnore]
        public Job Job { set; get; }
        
        public int JobWorkId { set; get; }
    }
}
