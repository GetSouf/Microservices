using System.Text.Json.Serialization;

namespace MicroserviceСompositeSC.Models
{
    public class NumberOfProducts
    {
        [JsonPropertyName("categoryName")]
        public string CategoryName { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}