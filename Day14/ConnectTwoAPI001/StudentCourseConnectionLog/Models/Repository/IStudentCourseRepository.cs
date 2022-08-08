using StudentCourseConnectionLog.Models.DatabaseModels;

namespace StudentCourseConnectionLog.Models.Repository
{
    public interface IStudentCourseRepository
    {
        List<StudentCourse> GetAllListForStudentAndCourse();
        StudentCourse AddStudentAndCourseInLog(int studentId, int courseId);
        StudentCourse GetLogByIdOfStudentAndCourse(int studentId, int courseId);
        List<StudentCourse> GetLogByIdOfStudent(int studentId);
        List<StudentCourse> GetLogByIdOfCourse(int courseId);
    }
}
