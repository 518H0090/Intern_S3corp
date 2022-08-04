using CourseManagementSystem.Models.DatabaseContext;

namespace CourseManagementSystem.Models.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            _context = context;
        }
    }
}
