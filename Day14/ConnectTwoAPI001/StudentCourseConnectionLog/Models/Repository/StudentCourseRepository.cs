using StudentCourseConnectionLog.Models.DatabaseContext;
using StudentCourseConnectionLog.Models.DatabaseModels;

namespace StudentCourseConnectionLog.Models.Repository
{
    public class StudentCourseRepository : IStudentCourseRepository
    {
        private readonly DataContext _context;

        public StudentCourseRepository(DataContext context)
        {
            _context = context;
        }

        public StudentCourse AddStudentAndCourseInLog(int studentId, int courseId)
        {
            StudentCourse studentCourse = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId
            };

            var addNewLog = _context.StudentCourses.Add(studentCourse);
            _context.SaveChanges();

            return addNewLog.Entity;
        }

        public List<StudentCourse> GetAllListForStudentAndCourse()
        {
            return _context.StudentCourses.ToList();
        }

        public List<StudentCourse> GetLogByIdOfCourse(int courseId)
        {
            return _context.StudentCourses.Where(sc => sc.CourseId == courseId).ToList();
        }

        public List<StudentCourse> GetLogByIdOfStudent(int studentId)
        {
            return _context.StudentCourses.Where(sc => sc.StudentId == studentId).ToList();
        }

        public StudentCourse GetLogByIdOfStudentAndCourse(int studentId, int courseId)
        {
            return _context.StudentCourses.Where(SC => SC.StudentId == studentId && SC.CourseId == courseId).FirstOrDefault();
        }
    }
}
