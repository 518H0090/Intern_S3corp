namespace RefreshTokenJwtAuthentication.Models.DatabaseModels
{
    public class User
    {
        public int Id { set; get; }

        public string UserName { set; get; }

        public string Password { set; get; }
    }
}
