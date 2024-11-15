using System.Text.Json.Serialization;
namespace MicroserviceСompositeSC.Models;
public class RatingOfCategory
{
    [JsonPropertyName("categoryName")]
    public string CategoryName { get; set; }
    [JsonPropertyName("averageRating")]
    public double AverageRating { get; set; }
}
