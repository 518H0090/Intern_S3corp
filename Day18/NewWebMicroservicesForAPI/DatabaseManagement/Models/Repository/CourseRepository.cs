using DatabaseManagement.Models.DatabaseContext;
using DatabaseManagement.Models.DatabaseModels;
using DatabaseManagement.Models.Dto.Request;
using DatabaseManagement.Models.Dto.Response;

namespace DatabaseManagement.Models.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public ResponseCreateCourse CreateCourse(RequestCreateCourse request)
        {
            Course courseCreated = new Course
            {
                CourseDescription = request.CourseDescription,
                CourseName = request.CourseName,
            };

            var createdCourse = _context.Courses.Add(courseCreated);
            _context.SaveChanges();

            return new ResponseCreateCourse
            {
                CourseName = createdCourse.Entity.CourseName,
                CourseDescription = createdCourse.Entity.CourseDescription
            };
        }

        public List<ResponseCreateCourse> GetAllCourse()
        {
            List<ResponseCreateCourse> courses = new List<ResponseCreateCourse>();

            _context.Courses.ToList().ForEach(c =>
            {
                ResponseCreateCourse course = new ResponseCreateCourse
                {
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription
                };

                courses.Add(course);
            });

            return courses;
        }
    }
}
