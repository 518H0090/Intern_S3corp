using DatabaseManagement.Models.Dto.Request;
using DatabaseManagement.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _repository;

        public CourseController(ICourseRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("CreateCourse")]
        public IActionResult CreateCourse(RequestCreateCourse request)
        {
            var createdCourse = _repository.CreateCourse(request);

            if (createdCourse == null)
            {
                return BadRequest(new
                {
                    message = "Can't Create Course"
                });
            }

            return Created("Create Course", createdCourse);
        }

        [HttpGet("GetAllCourses")]
        public IActionResult GetAllCourse()
        {
            return Ok(_repository.GetAllCourse());
        }
    }
}
