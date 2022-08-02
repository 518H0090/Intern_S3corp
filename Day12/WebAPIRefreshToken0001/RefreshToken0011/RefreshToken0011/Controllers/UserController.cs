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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        private readonly JwtTokenHelper _TokenHelper;

        public UserController(IUserRepository repository, JwtTokenHelper tokenHelper)
        {
            _repository = repository;
            _TokenHelper = tokenHelper;
        }

        [HttpPost("RegisterUser")]
        public ActionResult<User> Register(RegisterDto user)
        {
            var userCreate = new User
            {
                UserName = user.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };

            var createUser = _repository.CreateUser(userCreate);

            return Created("CreateUser", createUser);
        }

        [HttpGet("GetAllUser")]
        public ActionResult<List<User>> GetAllUser()
        {
            var allUser = _repository.GetAllUser();

            if (allUser.Count < 0 )
            {
                return NotFound(new {message = "No User Storage in System"});
            }

            return Ok(_repository.GetAllUser());
        }

        [HttpPost("Login")]
        public ActionResult<User> Login(LoginDto request)
        {
            var userLogin = _repository.GetUserByUserName(request.UserName);

            if (userLogin == null)
            {
                return NotFound(new {message = " USer Not found"});
            }

            if(!BCrypt.Net.BCrypt.Verify(request.Password, userLogin.Password))
            {
                return NotFound(new { message = " Password Is Not" });
            }

            var jwt = _TokenHelper.GetAuthTokens(userLogin);

            var accessToken = jwt.AccessToken.ToString();
            var refreshToken = jwt.RefreshToken.ToString();

            Response.Cookies.Append("AccessToken", accessToken, new CookieOptions
            {
                HttpOnly = true
            });

            Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true
            });

            var token = new TokenDto
            {
                AccessToken = jwt.AccessToken,
                RefreshToken = jwt.RefreshToken
            };

            return Ok(new
            {
                Message = "Login Success",
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken
            });
        }

        [HttpGet("FindUser")]
        public ActionResult<User> FindUser()
        {
            var jwt = Request.Cookies["AccessToken"];
            var refreshToken = Request.Cookies["RefreshToken"];

            if (jwt == null)
            {
                return NotFound(new
                {
                    message = "Token Doesn't Exists"
                });
            }

            RenewTokens(refreshToken);

            var token = _TokenHelper.VerifyToken(jwt);

            var userId = int.Parse(token.Issuer);

            var accountFind = _repository.GetUserByUserId(userId);

            if (refreshToken == null || accountFind == null)
            {
                return NotFound(new
                {
                    message = "User Doesn't Exists Or Not Exist Refresh Token",
                });
            }

            return Ok(new
            {
                AccountInfor = accountFind.ToString(),
                AccessToken = jwt,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("Logout")]
        public ActionResult Logout()
        {
            Response.Cookies.Delete("AccessToken");
            Response.Cookies.Delete("RefreshToken");

            return Ok(new
            {
                message = "Logout Success"
            });
        }


        [HttpPost("NewRefreshToken")]
        public IActionResult RenewTokens(string refreshToken)
        {
            var tokens = _TokenHelper.RenewTokens(refreshToken);
            if (tokens == null)
            {
                return ValidationProblem("Invalid Refresh Token");
            }

            Response.Cookies.Append("RefreshToken", tokens.RefreshToken, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(tokens);
        }

    }
}
