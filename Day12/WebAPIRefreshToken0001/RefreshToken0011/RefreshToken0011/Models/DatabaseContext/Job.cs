using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RefreshToken0011.Models.DatabaseContext
{
    public class Job
    {
        [Key]
        public int JobId { set; get; }

        public string Name { set; get; }

        public string Description { set; get; }

        [JsonIgnore]
        public Character Character { set; get; }
    }
}
