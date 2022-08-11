using DatabaseManagement.Models.Dto.Request;
using DatabaseManagement.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NlogExtensions;

namespace DatabaseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCourseController : ControllerBase
    {
        private readonly IStudentCourseRepository _repository;
        private readonly ILoggerManager _logger;

        public StudentCourseController(IStudentCourseRepository repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Recorded two id from Student and Course
        /// </summary>
        /// <param name="request">StudentId and CourseId</param>
        /// <returns>Recorded</returns>
        [HttpPost("RecordJoinStudentCourse")]
        public IActionResult JoinStudentCourse(RequestJoinStudentName request)
        {
            var recordedJoin = _repository.JoinStudentCourse(request);

            if (recordedJoin == null)
            {
                _logger.LogError("Can't Join Two tables");
                return BadRequest(new
                {
                    message = "Can't Join two tables"
                });
            }

            _logger.LogInfo("Recorded Succesful join between Student and Course");

            return Ok(recordedJoin);
        }

        /// <summary>
        /// Return All Joined Id with two tables (Student and Course)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllRecorded")]
        public IActionResult GetAllRecord()
        {
            var allRecored = _repository.GetAllRecord();

            if (allRecored == null)
            {
                _logger.LogError("Can't Get All Record");

                return BadRequest(new
                {
                    message = "Can't  Query Recorded"
                });
            }

            _logger.LogInfo("Get All Record from tables");

            return Ok(allRecored);
        }
    }
}
