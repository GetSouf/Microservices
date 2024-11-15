using System.Text.Json.Serialization;
namespace MicroserviceСompositeSC.Models
{
    public class Product
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("rating")]
        public float Rating { get; set; }
        [JsonPropertyName("categoryId")]
        public long CategoryId { get; set; }
        [JsonPropertyName("price")]
        public float Price { get; set; }
        [JsonPropertyName("providerId")]
        public long ProviderId { get; set; }
    }
}

