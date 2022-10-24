using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopeeApi.Dtos;
using ShopeeApi.Models;
using ShopeeApi.Repository;

namespace ShopeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("ViewRole")]
        public async Task<IActionResult> ViewRole()
        {
            //return Ok(JsonConvert.SerializeObject(new {Role.Admin, Role.User}, new Newtonsoft.Json.Converters.StringEnumConverter()));
            return Ok(JsonConvert.SerializeObject(Enum.GetNames(typeof(Role)).ToList()
                , new Newtonsoft.Json.Converters.StringEnumConverter()));
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser(RequestUserRegister request)
        {
            var registerCheck = await _repository.Register(request);

            if (registerCheck.Data == null)
            {
                return BadRequest(new
                {
                    Success = registerCheck.Success,
                    Message = registerCheck.Message
                });
            }

            return Created("CreateUser", registerCheck.Data);
        }

        [HttpPost]
        [Route("AuthenLogin")]
        public async Task<IActionResult> AuthenLogin(RequestUserLogin request)
        {
            var authenUser = await _repository.AuthenLogin(request);

            if (authenUser.Data == null)
            {
                return BadRequest(new
                {
                    Success = authenUser.Success,
                    Message = authenUser.Message
                });
            }

            Response.Cookies.Append("JwtToken", authenUser.Data, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(1)
            });

            return Ok(authenUser);
        }

        [HttpGet]
        [Route("ViewUserInfo")]
        public async Task<IActionResult> ViewUserInfo()
        {
            var jwtTokenFromCookies = Request.Cookies["JwtToken"];

            var checkUserInfo = await _repository.ViewUserInfo(jwtTokenFromCookies);

            if (checkUserInfo.Data == null)
            {
                return BadRequest(new
                {
                    Success = checkUserInfo.Success,
                    Message = checkUserInfo.Message
                });
            }

            return Ok(checkUserInfo.Data);
        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("JwtToken");
            return Ok("Logout");
        }
    }
}