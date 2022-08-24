using System.ComponentModel.DataAnnotations;

namespace TestDockerForWebApi
{
    public class Student
    {
        [Key]
        public int Id { set; get; }

        [Required]
        public string StudentName { set; get; }

        [Required]
        public string StudentClass { set; get; }
    }
}
