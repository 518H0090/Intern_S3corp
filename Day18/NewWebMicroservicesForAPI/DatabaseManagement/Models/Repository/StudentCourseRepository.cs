using DatabaseManagement.Models.DatabaseContext;
using DatabaseManagement.Models.DatabaseModels;
using DatabaseManagement.Models.Dto.Request;
using DatabaseManagement.Models.Dto.Response;
using Microsoft.EntityFrameworkCore;

namespace DatabaseManagement.Models.Repository
{
    public class StudentCourseRepository : IStudentCourseRepository
    {
        private readonly DataContext _context;

        public StudentCourseRepository(DataContext context)
        {
            _context = context;
        }

        public ResponseJoinStudentCourse JoinStudentCourse(RequestJoinStudentName request)
        {
            var studentCheck = _context.Students.Find(request.StudentId);

            var courseCheck = _context.Courses.Find(request.CourseId);

            if (studentCheck == null || courseCheck == null)
            {
                return null;
            }

            StudentCourse studentCourse = new StudentCourse
            {
                StudentId = request.StudentId,
                CourseId = request.CourseId
            };

            var recordedJoin = _context.StudentCourses.Add(studentCourse);
            _context.SaveChanges();

            return new ResponseJoinStudentCourse
            {
                CourseId = request.CourseId,
                StudentId = request.StudentId
            };
        }

        public List<ResponseJoinStudentCourse> GetAllRecord()
        {
            List<ResponseJoinStudentCourse> studentCourses = new List<ResponseJoinStudentCourse>();

            _context.StudentCourses.Include(sc => sc.Student).Include(sc => sc.Course).ToList().ForEach(sc =>
            {
                ResponseJoinStudentCourse studentCourse = new ResponseJoinStudentCourse
                {
                    StudentId = sc.StudentId,
                    CourseId = sc.CourseId
                };

                studentCourses.Add(studentCourse);
            });

            return studentCourses;
        }
        
    }
}
