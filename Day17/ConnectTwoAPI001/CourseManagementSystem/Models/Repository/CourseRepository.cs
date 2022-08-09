using CourseManagementSystem.Models.DatabaseContext;
using CourseManagementSystem.Models.ModelClass;

namespace CourseManagementSystem.Models.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public Course CreateCourse(Course course)
        {
            var createdCourse = _context.Courses.Add(course);
            _context.SaveChanges();
            return createdCourse.Entity;
        }

        public List<Course> GetAllCourse()
        {
            return _context.Courses.ToList();
        }

        public Course GetCourseById(int id)
        {
            return _context.Courses.Find(id);
        }
    }
}
