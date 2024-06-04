using System.Text.Json.Serialization;

namespace TimeTrackingApp.Middleware;

[Serializable]
public class Response
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
}