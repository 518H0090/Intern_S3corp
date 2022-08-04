using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models.Dto;
using StudentManagementSystem.Models.ModelClass;
using StudentManagementSystem.Models.Repository;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _repository;

        public StudentController(IStudentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetAllStudent")]
        public ActionResult<List<Student>> GetAllStudent()
        {
            return _repository.GetAllStudent();
        }

        [HttpPost("CreateStudent")]
        public ActionResult<Student> CreateStudent(AddStudentDto student)
        {
            Student newStudent = new Student
            {
                StudentName = student.StudentName,
                StudentAddress = student.StudentAddress,
                StudentAge = student.StudentAge,
                StudentPhone = student.StudentPhone
            };

            var createdStudent = _repository.CreateStudent(newStudent);

            return Created("CreateStudent", createdStudent);
        }
    }
}
