using StudentManagementSystem.Models.DatabaseContext;
using StudentManagementSystem.Models.ModelClass;

namespace StudentManagementSystem.Models.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public Student CreateStudent(Student student)
        {
            var createdStudent = _context.Students.Add(student);
            _context.SaveChanges();
            return createdStudent.Entity;
        }

        public List<Student> GetAllStudent()
        {
            return _context.Students.ToList();
        }

        public Student GetStudentById(int id)
        {
            return _context.Students.Find(id);
        }
    }
}
