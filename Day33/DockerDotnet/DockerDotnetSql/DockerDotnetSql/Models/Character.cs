using System.ComponentModel.DataAnnotations;

namespace DockerDotnetSql.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string CharacterName { get; set; }

        [Required]
        public string CharacterJob { set; get; }

        [Range(1, 100)]
        public int JobLevel { get; set; } = 1;
    }
}
