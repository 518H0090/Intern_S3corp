using Microsoft.IdentityModel.Tokens;
using RefreshTokenWebApi.Models.DatabaseContext;
using RefreshTokenWebApi.Models.DatabaseModels;
using RefreshTokenWebApi.Models.DtoRespository.Request;
using RefreshTokenWebApi.Models.DtoRespository.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RefreshTokenWebApi.Models.JwtHelpers
{
    public class JwtRepository
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public JwtRepository(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string GenerateAccessToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration.GetValue<string>("JwtSettings:secretKey");

            var symmetricKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var claims = new ClaimsIdentity(
            
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim("Role", "User")
                }
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenSecurity = tokenHandler.CreateToken(tokenDescriptor);

            var accessToken = tokenHandler.WriteToken(tokenSecurity);

            return accessToken;
        }

        public string GenerateRefreshToken()
        {
            var arrayBytes = new byte[64];

            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(arrayBytes);

                return Convert.ToBase64String(arrayBytes);
            }
        }

        public JwtTokenDto SetRefreshToken(string accessToken, string refreshToken,string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var findUserExists = _context.RefreshTokens.Where(rt => rt.UserName == userName 
            && rt.ExpiresToken == refreshToken && rt.RefreshExpires > DateTime.Now).FirstOrDefault();

            if (findUserExists != null)
            {
                return new JwtTokenDto
                {
                    AccessToken = findUserExists.AccessToken,
                    RefreshToken = findUserExists.ExpiresToken,
                    RefreshTokenExpires = findUserExists.RefreshExpires
                };
            }

            refreshToken = GenerateRefreshToken();
            var checkRefreshTokenExists = _context.RefreshTokens
                .Where(rt => rt.ExpiresToken == refreshToken && rt.RefreshExpires > DateTime.Now
                    || rt.ExpiresToken == refreshToken && rt.RefreshExpires < DateTime.Now
                ).FirstOrDefault();

            if (checkRefreshTokenExists != null)
            {
                refreshToken = GenerateRefreshToken();
                SetRefreshToken(accessToken, refreshToken, userName);
            }

            RefreshToken newRefreshToken = new RefreshToken
            {
                AccessToken = accessToken,
                ExpiresToken = refreshToken,
                CreatedToken = DateTime.Now,
                RefreshExpires = DateTime.Now.AddMinutes(0.2),
                UserName = userName
            };

            var createdNewRefreshToken = _context.RefreshTokens.Add(newRefreshToken);
            _context.SaveChanges();

            JwtTokenDto generateToken = new JwtTokenDto
            {
                AccessToken = createdNewRefreshToken.Entity.AccessToken,
                RefreshToken = createdNewRefreshToken.Entity.ExpiresToken,
                RefreshTokenExpires = createdNewRefreshToken.Entity.RefreshExpires
            };

            return generateToken;
        }

        public JwtTokenDto GetAuthToken(ResponseFindUser request)
        {
            if (request == null)
            {
                return null;
            }

            var accessToken = GenerateAccessToken(request.UserName);
            var refreshToken = GenerateRefreshToken();

            var generateAllToken = SetRefreshToken(accessToken, refreshToken, request.UserName);

            if (generateAllToken == null)
            {
                return null;
            }

            return generateAllToken;
        }

        public string GetUserNameFromAccessToken(string accessToken)
        {
            if (accessToken == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var readToken = tokenHandler.ReadJwtToken(accessToken);

            var userName = readToken.Claims.FirstOrDefault().Value;

            if (userName == null)
            {
                return null;
            }

            return userName.ToString();
        }

        public JwtTokenDto RenewToken(string accessToken)
        {
            var findUserName = GetUserNameFromAccessToken(accessToken);

            if (findUserName == null)
            {
                return null;
            }

            var newAccessToken = GenerateAccessToken(findUserName);
            var newRefreshToken = GenerateRefreshToken();

            var tokenGenerate = SetRefreshToken(newAccessToken, newRefreshToken, findUserName);

            if (tokenGenerate == null)
            {
                return null;
            }

            return tokenGenerate;
        }
    }
}
