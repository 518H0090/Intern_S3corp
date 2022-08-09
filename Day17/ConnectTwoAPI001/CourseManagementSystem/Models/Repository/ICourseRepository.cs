using CourseManagementSystem.Models.ModelClass;

namespace CourseManagementSystem.Models.Repository
{
    public interface ICourseRepository
    {
        Course CreateCourse(Course course);
        List<Course> GetAllCourse();
        Course GetCourseById(int id);
    }
}
