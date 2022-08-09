using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefreshTokenJwtAuthentication.Models.DatabaseContext;
using RefreshTokenJwtAuthentication.Models.DatabaseModels;
using RefreshTokenJwtAuthentication.Models.Dto;
using RefreshTokenJwtAuthentication.Models.HelperToken;
using RefreshTokenJwtAuthentication.Models.Repository;

namespace RefreshTokenJwtAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtTokenHelpers _jwtTokenHelpers;
        private readonly IUserRepository _repository;

        public UserController(JwtTokenHelpers jwtTokenHelpers, IUserRepository repository)
        {
            _jwtTokenHelpers = jwtTokenHelpers;
            _repository = repository;
        }

        [HttpGet("GetAllUser")]
        public ActionResult<List<User>> GetAllUser()
        {
            return _repository.GetAllUser();
        }

        [HttpPost("CreateUser")]
        public ActionResult<User> CreateUser(CreateUserDto request)
        {
            User requestUser = new User
            {
                UserName = request.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            var createdUser = _repository.CreateUser(requestUser);

            return Created("CreateUser", createdUser);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDto request)
        {
            var userNameFind = _repository.GetUserByName(request.UserName);

            if (userNameFind == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, userNameFind.Password))
            {
                return null;
            }

            User loginUser = new User
            {
                UserName = request.UserName,
                Password = request.Password
            };

            var tokenGet = _jwtTokenHelpers.AuthTokens(loginUser);

            if (tokenGet == null)
            {
                return BadRequest(new
                {
                    message = "Oop may be we have some trouble with Login"
                });
            }

            var accessToken = tokenGet.AccessToken;
            var refreshToken = tokenGet.RefreshToken;

            Response.Cookies.Append("accessToken", accessToken, new CookieOptions
            {
                HttpOnly = true
            });

            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Expires = tokenGet.RefreshTokenExpires,
            });

            return Ok(tokenGet);
        }

        [HttpGet("GetUser")]
        public IActionResult FindUser()
        {
            var accessToken = Request.Cookies["accessToken"];
            var refreshToken = Request.Cookies["refreshToken"];

            if (accessToken == null)
            {
                return BadRequest(new
                {
                    message = "AccessToken doesn't Exist"
                });
            }

            if (refreshToken == null)
            {
                var generateNewToken = _jwtTokenHelpers.RenewToken(accessToken);

                var userId = _jwtTokenHelpers.GetUserIdfromJwt(generateNewToken.AccessToken);

                var findUser = _repository.GetUserById(userId);

                if (findUser == null)
                {
                    return BadRequest(new
                    {
                        message = "User Doesn't Exists"
                    });
                }

                Response.Cookies.Append("accessToken", generateNewToken.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                });

                Response.Cookies.Append("refreshToken", generateNewToken.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = generateNewToken.RefreshTokenExpires
                });


                return Ok(new
                {
                    FindUserAccess = findUser,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }

            var userIdAccess = _jwtTokenHelpers.GetUserIdfromJwt(accessToken);
            var findUserAccess = _repository.GetUserById(userIdAccess);

            if (findUserAccess == null)
            {
                return BadRequest(new
                {
                    message = "User Doesn't Exists"
                });
            }

            return Ok(new
            {
                FindUserAccess = findUserAccess,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}
