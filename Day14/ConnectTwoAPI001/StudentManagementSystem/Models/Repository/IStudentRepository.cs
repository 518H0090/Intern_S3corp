using StudentManagementSystem.Models.ModelClass;

namespace StudentManagementSystem.Models.Repository
{
    public interface IStudentRepository
    {
        Student CreateStudent(Student student);
        List<Student> GetAllStudent();
    }
}
