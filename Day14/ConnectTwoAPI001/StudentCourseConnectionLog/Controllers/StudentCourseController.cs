using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentCourseConnectionLog.Models.Repository;

namespace StudentCourseConnectionLog.Controllers
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

        [HttpGet("GetAllLog")]
        public IActionResult GetAllLog()
        {
            return Ok(_repository.GetAllListForStudentAndCourse());
        }

        [HttpGet("GetLogWithIdBoth")]
        public IActionResult GetLogWithId()
        {


            return Ok();
        }

        [HttpPost("ConnectTwoDataInLog")]
        public IActionResult ConnectTwoDataInLog(int studentId, int courseId)
        {

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:9000/");
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    // New code:
            //    HttpResponseMessage response = await client.GetAsync("api/products/1");
            //    if (response.IsSuccessStatusCode)
            //    {
            //        Product product = await response.Content.ReadAsAsync > Product > ();
            //        Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price, product.Category);
            //    }
            //}

            return Ok();
        }
    }
}
