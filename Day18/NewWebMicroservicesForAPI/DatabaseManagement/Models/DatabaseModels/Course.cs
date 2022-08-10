namespace DatabaseManagement.Models.DatabaseModels
{
    public class Course
    {
        public int Id { set; get; }

        public string CourseName { set; get; }

        public string CourseDescription { set; get; }

        public List<StudentCourse> StudentCourses { set; get; }
    }
}
