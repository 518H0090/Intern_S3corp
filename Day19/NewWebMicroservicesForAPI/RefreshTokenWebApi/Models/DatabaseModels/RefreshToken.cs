namespace RefreshTokenWebApi.Models.DatabaseModels
{
    public class RefreshToken
    {
        public int Id { set; get; }

        public string UserName { set; get; }

        public string AccessToken { set; get; }

        public string ExpiresToken { set; get; }

        public DateTime CreatedToken { set; get; } = DateTime.UtcNow;

        public DateTime RefreshExpires { set; get; } = DateTime.UtcNow.AddMinutes(1);
    }
}
