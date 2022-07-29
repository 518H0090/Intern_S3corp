using DemoJwtBasic001.Models.Dto;
using DemoJwtBasic001.Models.Interface;
using DemoJwtBasic001.Models.JwtHelper;
using DemoJwtBasic001.Models.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoJwtBasic001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _repository;
        private readonly JwtTokenHelper _jwtToken;

        public UserController(IUserRepository repository, JwtTokenHelper jwtToken)
        {
            _repository = repository;
            _jwtToken = jwtToken;
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterDto request)
        {
            User newUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            var createdUser = _repository.CreateUser(newUser);

            return Created("createUser",createdUser);
        }

        [HttpPost("Login")]
        public IActionResult LoginAccount(LoginDto request)
        {
            var userFindByEmail = _repository.GetUserByEmail(request.Email);

            if (userFindByEmail == null)
            {
                return NotFound();
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password,userFindByEmail.Password))
            {
                return BadRequest(new {message= "Not Valid"});
            }

            var jwt = _jwtToken.GenerateJwtToken(userFindByEmail.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                message = "Login Success"
            });
        }

        [HttpGet("FindUserLogin")]
        public IActionResult FindUser()
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt == null)
            {
                return NotFound();
            }

            int id = int.Parse(_jwtToken.Verify(jwt).Issuer);

            var userLogin = _repository.GetUserById(id);

            if (userLogin == null)
            {
                return NotFound();
            }

            return Ok(userLogin);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new
            {
                message = "Logout Success"
            });
        }
    }
}
