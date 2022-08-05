namespace RefreshTokenJwtAuthentication.Models.Dto
{
    public class TokenDto
    {
        public string AccessToken { set; get; }
        
        public string RefreshToken { set; get; }

        public DateTime RefreshTokenExpires { set; get; }
    }
}
