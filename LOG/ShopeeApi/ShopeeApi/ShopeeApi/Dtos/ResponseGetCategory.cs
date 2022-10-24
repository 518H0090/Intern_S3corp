namespace ShopeeApi.Dtos
{
    public class ResponseGetCategory
    {
        public int CategoryId { get; set; }

        public string CategoryName { set; get; } = string.Empty;

        public string CategoryTag { set; get; } = string.Empty;
    }
}