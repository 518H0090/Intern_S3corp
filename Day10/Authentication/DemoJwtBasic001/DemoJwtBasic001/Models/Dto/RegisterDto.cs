namespace DemoJwtBasic001.Models.Dto
{
    public class RegisterDto
    {

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { set; get; } = string.Empty;
    }
}
