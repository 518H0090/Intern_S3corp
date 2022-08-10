using System.Text.Json.Serialization;

namespace DatabaseManagement.Models.DatabaseModels
{
    public class StudentCourse
    {
        public Student Student { set; get; }
        public int StudentId { set; get; }

        public Course Course { set; get; }
        public int CourseId { set; get; }
    }
}
