using ShopeeApi.Models;
using System.Text.Json.Serialization;

namespace ShopeeApi.Dtos
{
    public class ResponseUserRegister
    {
        public string UserName { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role Role { get; set; }
    }
}