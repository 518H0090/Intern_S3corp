using CourseManagementSystem.Models.Dto;
using CourseManagementSystem.Models.ModelClass;
using CourseManagementSystem.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.Controllers
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

        [HttpGet("GetAllCourse")]
        public ActionResult<List<Course>> GetAllCourse()
        {
            return Ok(_repository.GetAllCourse());
        }

        [HttpGet("GetCourseById/{id}")]
        public ActionResult<Course> GetCourseById(int id)
        {
            return Ok(_repository.GetCourseById(id));
        }

        [HttpPost("CreateCourse")]
        public ActionResult<Course> CreateCourse(CreateCourse course)
        {
            Course newCourse = new Course
            {
                CourseName = course.CourseName,
                CourseDescription = course.CourseDescription
            };

            var createdCourse = _repository.CreateCourse(newCourse);

            return Created("CreateCourse", createdCourse);
        }
    }
}
