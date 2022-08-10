using DatabaseManagement.Models.Dto.Request;
using DatabaseManagement.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseManagement.Controllers
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

        /// <summary>
        /// Create Student
        /// </summary>
        /// <param name="request">StudentName, Address , Phone</param>
        /// <returns>Information for craete student</returns>
        [HttpPost("CreateStudent")]
        public IActionResult CreateStudent(RequestCreateStudent request)
        {
            var createdStudent = _repository.CreateStudent(request);

            if (createdStudent == null)
            {
                return BadRequest(new
                {
                    message = "Can't Create Student"
                });
            }

            return Created("CreateStudent", createdStudent);
        }

        /// <summary>
        /// Return All Student
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllStudent")]
        public IActionResult GetAllStudent()
        {
            return Ok(_repository.GetAllStudent());
        }
    }
}
