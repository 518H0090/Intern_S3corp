using DatabaseManagement.Models.Dto.Request;
using DatabaseManagement.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCourseController : ControllerBase
    {
        private readonly IStudentCourseRepository _repository;

        public StudentCourseController(IStudentCourseRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("RecordJoinStudentCourse")]
        public IActionResult JoinStudentCourse(RequestJoinStudentName request)
        {
            var recordedJoin = _repository.JoinStudentCourse(request);

            if (recordedJoin == null)
            {
                return BadRequest(new
                {
                    message = "Can't Join two tables"
                });
            }

            return Ok(recordedJoin);
        }

        [HttpGet("GetAllRecorded")]
        public IActionResult GetAllRecord()
        {
            var allRecored = _repository.GetAllRecord();

            if (allRecored == null)
            {
                return BadRequest(new
                {
                    message = "Can't  Query Recorded"
                });
            }

            return Ok(allRecored);
        }
    }
}
