using StudentCourseConnectionLog.Models.DatabaseModels;

namespace CourseManagementSystem.Models.ModelClass
{
    public class Course
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public string CourseDescription { set; get; }

    }
}
