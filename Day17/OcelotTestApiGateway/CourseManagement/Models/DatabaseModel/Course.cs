using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Models.DatabaseModel
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        public string CourseName { set; get; }

        public string CourseDescription { set; get; }
    }
}
