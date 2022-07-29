
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace DemoJwtBasic001.Models.JwtHelper
{
    public class JwtTokenHelper
    {
        private readonly string secureKey = "this is a security key";

        public string GenerateJwtToken(int id)
        {
            //Set up Header
            var symmetricKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
            var headers = new JwtHeader(credentials);

            //Pay load
            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));
            
            //Signature
            var securityToken = new JwtSecurityToken(headers,payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var keyEncode = System.Text.Encoding.ASCII.GetBytes(secureKey);
            handler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(keyEncode),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            },out SecurityToken validateSecurityToken);

            return (JwtSecurityToken)validateSecurityToken;
        }

    }
  
}
