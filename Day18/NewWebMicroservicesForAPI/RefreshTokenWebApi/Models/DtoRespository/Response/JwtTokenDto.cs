namespace RefreshTokenWebApi.Models.DtoRespository.Response
{
    public class JwtTokenDto
    {
        public string AccessToken { set; get; }

        public string RefreshToken { set; get; }

        public DateTime RefreshTokenExpires { set; get; }
    }
}
