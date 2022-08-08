using CourseManagementSystem.Models.ModelClass;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentCourseConnectionLog.Models.DatabaseModels;
using StudentCourseConnectionLog.Models.Repository;
using StudentManagementSystem.Models.ModelClass;

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

        [HttpGet("GetLogWithStudentId/{studentId}")]
        public async Task<IActionResult> GetLogWithStudentId(int studentId)
        {
            var listDataConnectFromStudentId = _repository.GetLogByIdOfStudent(studentId);
            List<Course> listCourseConnectWithStudentId = new List<Course>();

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5063/");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var responseStudent = await httpClient.GetAsync($"/apigateway/GetStudentById/{studentId}");

            Student studentInformation = null;

            if (responseStudent.IsSuccessStatusCode)
            {
                studentInformation = JsonConvert.DeserializeObject<Student>(await responseStudent.Content.ReadAsStringAsync());
            }

            foreach (var value in listDataConnectFromStudentId)
            {
                var responseCourse = await httpClient.GetAsync($"/apigateway/GetCourseById/{value.CourseId}");

                if (responseCourse.IsSuccessStatusCode)
                {
                    Course courseInformation = JsonConvert.DeserializeObject<Course>(await responseCourse.Content.ReadAsStringAsync());
                    listCourseConnectWithStudentId.Add(courseInformation);
                }

            }

            if (studentInformation != null && listCourseConnectWithStudentId.Count > 0)
            {
                return Ok(new {
                    Student = studentInformation,
                    CourseConnectWithStudent = listCourseConnectWithStudentId
                }); 
            }


            httpClient.Dispose();

            return BadRequest(new
            {
                message = "Erros"
            });
        }

        [HttpGet("GetLogWithCourseId/{courseId}")]
        public async Task<IActionResult> GetLogWithCourseId(int courseId)
        {
            var listDataConnectFromCourseId = _repository.GetLogByIdOfCourse(courseId);
            List<Student> listCourseConnectWithStudentId = new List<Student>();

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5063/");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var responseCourse = await httpClient.GetAsync($"/apigateway/GetCourseById/{courseId}");

            Course courseInformation = null;

            if (responseCourse.IsSuccessStatusCode)
            {
                courseInformation = JsonConvert.DeserializeObject<Course>(await responseCourse.Content.ReadAsStringAsync());
            }

            foreach (var value in listDataConnectFromCourseId)
            {
                var responseStudent = await httpClient.GetAsync($"/apigateway/GetStudentById/{value.StudentId}");

                if (responseStudent.IsSuccessStatusCode)
                {
                    Student studentInformation = JsonConvert.DeserializeObject<Student>(await responseStudent.Content.ReadAsStringAsync());
                    listCourseConnectWithStudentId.Add(studentInformation);
                }

            }

            if (courseInformation != null && listCourseConnectWithStudentId.Count > 0)
            {
                return Ok(new
                {
                    Student = courseInformation,
                    CourseConnectWithStudent = listCourseConnectWithStudentId
                });
            }


            httpClient.Dispose();

            return BadRequest(new
            {
                message = "Erros"
            });
        }

        [HttpGet("GetLogWithIdBoth/{studentId}/{courseId}")]
        public async Task<IActionResult> GetLogWithId(int studentId, int courseId)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5063/");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var responseStudent = await httpClient.GetAsync($"/apigateway/GetStudentById/{studentId}");
            var responseCourse = await httpClient.GetAsync($"/apigateway/GetCourseById/{courseId}");

            if (responseStudent.IsSuccessStatusCode && responseCourse.IsSuccessStatusCode)
            {
                Student studentInformation = JsonConvert.DeserializeObject<Student>(await responseStudent.Content.ReadAsStringAsync());

                Course courseInformation = JsonConvert.DeserializeObject<Course>(await responseCourse.Content.ReadAsStringAsync());

                if (studentInformation != null && courseInformation != null)
                {
                    return Ok(new
                    {
                        Student = studentInformation,
                        Course = courseInformation
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        message = " OOp may be it has some errors"
                    });
                }
            }


            httpClient.Dispose();

            return BadRequest(new
            {
                message = "Erros"
            });
        }

        [HttpPost("ConnectTwoDataInLog")]
        public async Task<IActionResult> ConnectTwoDataInLog(StudentCourse studentCourse)
        {

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:5063/");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var responseCourse = await httpClient.GetAsync($"/apigateway/GetCourseById/{studentCourse.CourseId}");

                var responseStudent = await httpClient.GetAsync($"/apigateway/GetStudentById/{studentCourse.StudentId}");

                if (responseCourse.IsSuccessStatusCode && responseStudent.IsSuccessStatusCode)
                {
                    Student studentInformation = JsonConvert.DeserializeObject<Student>(await responseStudent.Content.ReadAsStringAsync());

                    Course courseInformation = JsonConvert.DeserializeObject<Course>(await responseCourse.Content.ReadAsStringAsync());

                    if (studentInformation != null && courseInformation != null)
                    {
                        var createdLog = _repository.AddStudentAndCourseInLog(studentInformation.Id, courseInformation.Id);

                        return Created("ConnectTwoTable",createdLog);
                    } else
                    {
                        return BadRequest(new
                        {
                            message = " OOp may be it has some errors"
                        });
                    }
                }
            }

            return BadRequest(new
            {
                message = " OOp may be it has some errors"
            });
        }
    }
}
