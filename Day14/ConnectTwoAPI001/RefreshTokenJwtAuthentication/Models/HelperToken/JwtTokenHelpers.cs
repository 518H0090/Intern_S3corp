using Microsoft.IdentityModel.Tokens;
using RefreshTokenJwtAuthentication.Models.DatabaseContext;
using RefreshTokenJwtAuthentication.Models.DatabaseModels;
using RefreshTokenJwtAuthentication.Models.Dto;
using RefreshTokenJwtAuthentication.Models.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RefreshTokenJwtAuthentication.Models.HelperToken
{
    public class JwtTokenHelpers
    {
        private readonly DataContext _context;
        private readonly IUserRepository _repository;
        public static readonly string secretKey = "Hieuro5122draco468255";

        public JwtTokenHelpers(DataContext context, IUserRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public TokenDto AuthTokens(User request)
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName == request.UserName).Id;

            string accessToken = GenerateAccesToken(request.UserName
                , request.Password,
                userId);

            var refreshToken = GenerateRefreshToken();
            TokenDto createTokenDto = setRefreshToken(refreshToken, accessToken, request.Id);

            return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenExpires = createTokenDto.RefreshTokenExpires
            };

            return createTokenDto;
        }

        public string GenerateAccesToken(string UserName, string Password, int userId)
        {
            if (UserName == null || Password == null || userId == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var symmetricKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity
                (
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                        new Claim(ClaimTypes.Name, UserName),
                        new Claim(ClaimTypes.Role, "User")
                    }
                ),
                Issuer = userId.ToString(),
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = credentials

            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return accessToken;
        }

        public string GenerateRefreshToken()
        {
            var byteArrays = new byte[64];
            using(var randomGenerator = RandomNumberGenerator.Create())
            {
                randomGenerator.GetBytes(byteArrays);

                var refreshToken = Convert.ToBase64String(byteArrays);

                return refreshToken;
            }
        }

        public TokenDto setRefreshToken(string refreshToken,string accesToken,int userId)
        {
            var findUser = _context.RefreshTokens.Where(rt => 
            rt.UserId == userId 
            && rt.ExpiresTime < DateTime.UtcNow);

            if (findUser == null)
            {
                return null;
            }

            var findRefreshTokenExistDeadExpires = _context.RefreshTokens
                .Where(rf => rf.ExpiresToken == refreshToken && rf.ExpiresTime > DateTime.UtcNow
                ).FirstOrDefault();

            if (findRefreshTokenExistDeadExpires != null)
            {
                var createNewRefreshToken = GenerateRefreshToken();
                setRefreshToken(createNewRefreshToken, accesToken, userId);
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            RefreshToken createRefreshToken = new RefreshToken
            {
                UserId = userId,
                AccessToken = tokenHandler.WriteToken(tokenHandler.ReadJwtToken(accesToken)),
                CreatedExpirates = DateTime.Now,
                ExpiresToken = refreshToken,
                ExpiresTime = DateTime.Now.AddMinutes(3),
            };

            _context.RefreshTokens.Add(createRefreshToken);
            _context.SaveChanges();

            return new TokenDto
            {
                AccessToken = createRefreshToken.AccessToken,
                RefreshToken = createRefreshToken.ExpiresToken,
                RefreshTokenExpires = createRefreshToken.ExpiresTime,
            };
        }

        //public JwtSecurityToken verifyValueFromToken(string accessToken)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var symmetricKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

        //    tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
        //    {
        //        IssuerSigningKey = symmetricKey,
        //        ValidateIssuerSigningKey = true,
        //        ValidateAudience = false,
        //        ValidateIssuer = false,
        //    }, out SecurityToken securityToken);

        //    return (JwtSecurityToken)securityToken;
        //}

        public int GetUserIdfromJwt(string accessToken)
        {
            if (accessToken == null)
            {
                return 0;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var getValue = tokenHandler.ReadJwtToken(accessToken).Claims.FirstOrDefault().Value;

            var userId = int.Parse(getValue);

            if (userId == null)
            {
                return 0;
            }

            return userId;
        }

        public TokenDto RenewToken(string accessToken)
        {
            var userId = GetUserIdfromJwt(accessToken);

            var findUser = _context.Users.Find(userId);

            if (findUser == null)
            {
                return null;
            }

            var newAccessToken = GenerateAccesToken(findUser.UserName, findUser.Password, findUser.Id);
            var refreshToken = GenerateRefreshToken();
            TokenDto createTokenDto = setRefreshToken(refreshToken, newAccessToken, userId);

            return createTokenDto;
        }
    }
}
