using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopeeApi.Datas;
using ShopeeApi.Dtos;
using ShopeeApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ShopeeApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserRepository(DataContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> AuthenLogin(RequestUserLogin request)
        {
            var serviceResponse = new ServiceResponse<string>();

            User findCharacter = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);

            if (findCharacter == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Not Found User";
            }
            else if (!VerifyPassword(request.Password, findCharacter.PasswordSalt, findCharacter.PasswordHash))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Not Validate Password";
            }
            else
            {
                serviceResponse.Data = GenerateJwtToken(findCharacter);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseUserRegister>> Register(RequestUserRegister request)
        {
            var serviceResponse = new ServiceResponse<ResponseUserRegister>();

            if (ExistsUser(request.UserName) || !request.Password.SequenceEqual(request.RePassword))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User Exists or Invaliad Password Compare";
            }
            else
            {
                HashPassword(request.Password, out byte[] passwordSalt, out byte[] passwordHash);

                User newUser = new User
                {
                    UserName = request.UserName,
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash,
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                var responseRegister = _mapper.Map<ResponseUserRegister>(newUser);

                serviceResponse.Data = responseRegister;
            }

            return serviceResponse;
        }

        public string GenerateJwtToken(User request)
        {
            string secretKey = _configuration.GetSection("JwtSettings:SecretKey").Value;

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, request.Id.ToString()),
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.Role, request.Role.ToString()),
            };

            SymmetricSecurityKey symmetric = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(secretKey));

            SigningCredentials credentials = new SigningCredentials(symmetric, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(1)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        private bool ExistsUser(string requestUsername)
        {
            bool checkUserExists = _context.Users.Any(u => u.UserName == requestUsername);

            if (checkUserExists)
            {
                return true;
            }

            return false;
        }

        private void HashPassword(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.Unicode.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                byte[] passwordHashCheck = hmac.ComputeHash(Encoding.Unicode.GetBytes(password));

                return passwordHashCheck.SequenceEqual(passwordHash);
            }
        }

        public async Task<ServiceResponse<ResponseViewUser>> ViewUserInfo(string jwtToken)
        {
            ServiceResponse<ResponseViewUser> serviceResponse = new ServiceResponse<ResponseViewUser>();

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.ReadJwtToken(jwtToken);

            var userName = token.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name || u.Type == "unique_name").Value;

            //var findUserName = await _context.Users
            //    .FirstOrDefaultAsync(u => u.UserName == userName);

            //if (findUserName != null)
            //{
            //serviceResponse.Data = _mapper.Map<ResponseViewUser>(findUserName);
            serviceResponse.Message = userName;
            //}

            //else
            //{
            //    serviceResponse.Message = "Please Check Your Authentication";
            //    serviceResponse.Success = false;
            //}

            return serviceResponse;
        }
    }
}