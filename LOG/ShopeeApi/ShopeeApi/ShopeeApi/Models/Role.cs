using System.Text.Json.Serialization;

namespace ShopeeApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Admin = 928399,
        User = 468255
    }
}