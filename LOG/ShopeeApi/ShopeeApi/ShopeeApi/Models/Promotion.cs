using System.Text.Json.Serialization;

namespace ShopeeApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Promotion
    {
        reduce30K = 30,
        reduce10K = 10,
        reduce20K = 20,
        reduce = 5,
        reduce40K = 40
    }
}