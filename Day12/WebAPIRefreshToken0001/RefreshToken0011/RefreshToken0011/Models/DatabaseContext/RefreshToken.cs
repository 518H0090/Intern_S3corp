namespace RefreshToken0011.Models.DatabaseContext
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public int UserId { set; get; }

        public string Token { set; get; }

        public DateTime ExpirationDate { set; get; } 
    }
}
