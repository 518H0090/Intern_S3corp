namespace DatabaseManagement.Models.DatabaseModels
{
    public class Student
    {
        public int Id { get; set; }

        public string StudentName { set; get; }

        public string StudentAddress { set; get; }

        public int StudentPhone { set; get; }

        public List<StudentCourse> StudentCourses { set; get; }
    }
}
