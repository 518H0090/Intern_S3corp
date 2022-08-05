using System.ComponentModel.DataAnnotations;

namespace RefreshTokenJwtAuthentication.Models.DatabaseModels
{
    public class RefreshToken
    {
        [Key]
        public int Id { set; get; }

        public int UserId { set; get; }

        public string AccessToken { set; get; }

        public string ExpiresToken { set; get; }

        public DateTime CreatedExpirates { set; get; }

        public DateTime ExpiresTime { set; get; }
    }
}
