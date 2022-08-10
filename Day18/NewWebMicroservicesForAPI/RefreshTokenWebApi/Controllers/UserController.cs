using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefreshTokenWebApi.Models.DtoRespository.Request;
using RefreshTokenWebApi.Models.JwtHelpers;
using RefreshTokenWebApi.Models.Repository;

namespace RefreshTokenWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly JwtRepository _jwtRepository;

        public UserController(IUserRepository repository, JwtRepository jwtRepository)
        {
            _repository = repository;
            _jwtRepository = jwtRepository;
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="request">UserName and Password</param>
        /// <returns>UserName and Password</returns>
        [HttpPost("CreateUser")]
        public IActionResult CreateUser(UserCreate request)
        {
            var createdUser = _repository.CreateUser(request);

            if (createdUser == null)
            {
                return BadRequest(new
                {
                    message = "Can't Create User"
                });
            }

            return Created("CreateNewUser",createdUser);
        }

        /// <summary>
        /// Login and Generate Jwt token
        /// </summary>
        /// <param name="request">Username and Password</param>
        /// <returns>AccessToken and RefreshToken</returns>
        [HttpPost("Login")]
        public IActionResult LoginInSystem(UserLogin request)
        {
            var findUser = _repository.FindUser(request);

            if (findUser == null)
            {
                return BadRequest(new
                {
                    message = "User of Password invalid"
                });
            }

            var tokenGenerate = _jwtRepository.GetAuthToken(findUser);

            if (tokenGenerate == null)
            {
                return BadRequest(new
                {
                    message = "Can't Generate Token"
                });
            }

            Response.Cookies.Append("accessToken", tokenGenerate.AccessToken, new CookieOptions
            {
                HttpOnly = true
            });

            Response.Cookies.Append("refreshToken", tokenGenerate.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Expires = tokenGenerate.RefreshTokenExpires
            });

            return Ok(tokenGenerate);
        }

        /// <summary>
        /// Check Expires of Refresh Token and generate new token if time is expired.
        /// </summary>
        /// <returns>Refresh token and access token</returns>
        [HttpGet("CheckLogin")]
        public IActionResult CheckLogin()
        {
            var accessToken = Request.Cookies["accessToken"];
            var refreshToken = Request.Cookies["refreshToken"];

            if (accessToken == null)
            {
                return BadRequest(new
                {
                    message = "No Value Can't Query"
                });
            }

            if (refreshToken == null)
            {
                var generateNewToken = _jwtRepository.RenewToken(accessToken);

                if (generateNewToken == null)
                {
                    return BadRequest(new
                    {
                        message = "No Value Can't Query"
                    });
                }

                Response.Cookies.Append("accessToken", generateNewToken.AccessToken, new CookieOptions
                {
                    HttpOnly = true
                });

                Response.Cookies.Append("refreshToken", generateNewToken.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = generateNewToken.RefreshTokenExpires
                });

                return Ok(generateNewToken);
            }

            return Ok(new
            {
                accessToken = accessToken,
                refreshToken = refreshToken
            });
        }
    }
}
