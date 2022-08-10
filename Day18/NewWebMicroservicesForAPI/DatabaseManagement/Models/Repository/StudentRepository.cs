using DatabaseManagement.Models.DatabaseContext;
using DatabaseManagement.Models.DatabaseModels;
using DatabaseManagement.Models.Dto.Request;
using DatabaseManagement.Models.Dto.Response;

namespace DatabaseManagement.Models.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public ResponseCreateStudent CreateStudent(RequestCreateStudent request)
        {
            Student studentInformation = new Student
            {
                StudentName = request.StudentName,
                StudentAddress = request.StudentAddress,
                StudentPhone = request.StudentPhone,
            };

            var createdStudent = _context.Students.Add(studentInformation);
            _context.SaveChanges();

            return new ResponseCreateStudent
            {
                StudentName = createdStudent.Entity.StudentName,
                StudentPhone = createdStudent.Entity.StudentPhone
            };
        }

        public List<ResponseCreateStudent> GetAllStudent()
        {
            List<ResponseCreateStudent> responseCreateStudents = new List<ResponseCreateStudent>();

            _context.Students.ToList().ForEach(s =>
            {
                ResponseCreateStudent student = new ResponseCreateStudent
                {
                    StudentName = s.StudentName,
                    StudentPhone = s.StudentPhone
                };

                responseCreateStudents.Add(student);
            });

            return responseCreateStudents;
        }
    }
}
