using ShopeeApi.Models;
using System.Text.Json.Serialization;

namespace ShopeeApi.Dtos
{
    public class ResponseGetRestaurantWithFood
    {
        public int RsId { get; set; }

        public string RsTitle { set; get; } = string.Empty;

        public string RsProvince { set; get; } = string.Empty;

        public string RsAddress { set; get; } = string.Empty;

        public string RsType { set; get; } = string.Empty;

        public string RsImageUrl { set; get; } = string.Empty;

        public float RsStar { set; get; }

        public int RsReviews { set; get; }

        public string RsOpenTime { set; get; } = string.Empty;

        public string RsPrinceRange { set; get; } = string.Empty;

        public bool RsRefeLike { set; get; } = false;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Promotion RsPromotion { set; get; } = Promotion.reduce;

        public IEnumerable<ResponseGetFood> Foods { set; get; }
    }
}
