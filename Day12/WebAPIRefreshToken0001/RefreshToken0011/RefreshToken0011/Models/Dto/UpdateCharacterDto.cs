namespace RefreshToken0011.Models.Dto
{
    public class UpdateCharacterDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int level { set; get; } = 1;

        public int JobId { set; get; }
    }
}
