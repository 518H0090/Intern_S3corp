using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefreshToken0011.Models.DatabaseContext;
using RefreshToken0011.Models.Dto;
using RefreshToken0011.Models.Helpers;
using RefreshToken0011.Models.Interface;

namespace RefreshToken0011.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository _repository;
        private readonly JwtTokenHelper _tokenHelper;

        public JobController(IJobRepository repository, JwtTokenHelper tokenHelper)
        {
            _repository = repository;
            _tokenHelper = tokenHelper;
        }

        [HttpGet("ReturnUserId")]
        public int GetUserId()
        {
            var jwt = Request.Cookies["AccessToken"];

            if (jwt == null)
            {
                return 0;
            }

            var token = _tokenHelper.VerifyToken(jwt);

            var userId = int.Parse(token.Issuer);

            return userId;
        }

        [HttpGet("GetAllJob")]
        public ActionResult<List<Job>> GetAllJob()
        {
            var userId = GetUserId();

            if (userId == 0)
            {
                return NotFound(new
                {
                    message = "Not Found User Login"
                });
            }

            return _repository.GetAllJob();
        }

        [HttpPost("AddNewJob")]
        public ActionResult<Job> CreateJob(CreatejobDto job)
        {
            var userId = GetUserId();

            if (userId == 0)
            {
                return NotFound(new
                {
                    message = "Not Found User Login"
                });
            }

            Job createJob = new Job
            {
                Name = job.Name,
                Description = job.Description
            };

            var createdNewJob = _repository.CreateJob(createJob);

            return Created("CreateNewJob",createdNewJob);
        }

        [HttpPost("EditJob")]
        public ActionResult<Job> UpdateJob(UpdateJobDto job)
        {
            var userId = GetUserId();

            if (userId == 0)
            {
                return NotFound(new
                {
                    message = "Not Found User Login"
                });
            }

            Job updateJob = new Job
            {
                JobId = job.JobId,
                Name = job.Name,
                Description = job.Description
            };

            var updatedNewJob = _repository.EditJob(updateJob);

            if (updatedNewJob == null)
            {
                return BadRequest(new
                {
                    message = "Oop maybe we can't update this job"
                });
            }

            return Ok(new
            {
                message = "Update Success",
                JobUpdate = updatedNewJob
            });
        }

        [HttpPost("DeleteJob/{id}")]
        public ActionResult<Job> DeleteJob(int id)
        {
            var userId = GetUserId();

            if (userId == 0)
            {
                return NotFound(new
                {
                    message = "Not Found User Login"
                });
            }

            var deletedJob = _repository.DeleteJob(id);

            if (deletedJob == null)
            {
                return BadRequest(new
                {
                    message = "Oops we can't delete this job"
                });
            }

            return Ok(new
            {
                message = "Delete Success",
                DeletedJob = deletedJob
            });
        }

        [HttpPost("DeleteAllJob")]
        public ActionResult<List<Job>> DeleteAllJob()
        {
            var userId = GetUserId();

            if (userId == 0)
            {
                return NotFound(new
                {
                    message = "Not Found User Login"
                });
            }

            var deletedALLJob = _repository.DeleteAllJob();

            if (deletedALLJob == null)
            {
                return BadRequest(new
                {
                    message = "Oops we can't delete this job"
                });
            }

            return Ok(new
            {
                message = "Delete Success",
                DeletedAllJob = deletedALLJob
            });
        }


    }
}
