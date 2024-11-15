using System.Text.Json.Serialization;
namespace MicroserviceСompositeSC.Models
{
    public class Provider
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
