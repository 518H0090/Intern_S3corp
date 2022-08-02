using Microsoft.IdentityModel.Tokens;
using RefreshToken0011.Models.DatabaseContext;
using RefreshToken0011.Models.Dto;
using RefreshToken0011.Models.ModelDatabase;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace RefreshToken0011.Models.Helpers
{
    public class JwtTokenHelper
    {
        public static readonly string secureKey = "Secret Key for me";

        private readonly DataContext _context;

        public JwtTokenHelper(DataContext context)
        {
            _context = context;
        }

        //Jwt Token
        public string Generate(int id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(secureKey);

            var symmetricKey = new SymmetricSecurityKey(key);

            var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtHeader = new JwtHeader(credentials);

            var jwtPayload = new JwtPayload(id.ToString(),null, null ,null, DateTime.Now.AddMinutes(20));

            var jwtSecurityToken = new JwtSecurityToken(
                jwtHeader,jwtPayload
            );

            return tokenHandler.WriteToken(jwtSecurityToken);
        }

        public JwtSecurityToken VerifyToken(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(secureKey);

            tokenHandler.ValidateToken(jwt, new TokenValidationParameters {

                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false

                },out SecurityToken securityToken);

            return (JwtSecurityToken)securityToken;
        }

        
        //Refresh Token
        public string CreateRefreshTokens()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshTokens = Convert.ToBase64String(tokenBytes);

            var tokenIsInUser = _context.RefreshTokens.Any(_ => _.Token == refreshTokens);

            if (tokenIsInUser)
            {
                return CreateRefreshTokens();
            }

            return refreshTokens;
        }

        private void InsertRefreshToken(int userId, string refreshToken)
        {
            var newRefreshToken = new RefreshToken
            {
                UserId = userId,
                Token = refreshToken,
                ExpirationDate = DateTime.Now.AddDays(7)
            };

            _context.RefreshTokens.Add(newRefreshToken);
            _context.SaveChanges();
        }

        public TokenDto GetAuthTokens(User user)
        {
            var createUser = new User
            {
                UserName = user.UserName,
                Password = user.Password
            };


            if (createUser != null)
            {
                var accessToken = Generate(user.Id);
                var refreshToken = CreateRefreshTokens();

                InsertRefreshToken(user.Id, refreshToken);

                return new TokenDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
            }

            return null;
        }


        public TokenDto RenewTokens(string refreshToken)
        {
            var userRefreshToken = _context.RefreshTokens
            .Where(_ => _.Token == refreshToken
            && _.ExpirationDate >= DateTime.Now).FirstOrDefault();

            if (userRefreshToken == null)
            {
                return null;
            }

            var user = _context.Users
            .Where(u => u.Id == userRefreshToken.Id).FirstOrDefault();

            var newJwtToken = Generate(userRefreshToken.UserId);
            var newRefreshToken = CreateRefreshTokens();

          

            userRefreshToken.Token = newRefreshToken;
            userRefreshToken.ExpirationDate = DateTime.Now.AddDays(7);

           
            _context.SaveChanges();

            InsertRefreshToken(userRefreshToken.UserId, newRefreshToken);


            return new TokenDto
            {
                AccessToken = newJwtToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
