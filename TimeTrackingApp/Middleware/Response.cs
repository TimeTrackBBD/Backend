using System.Text.Json.Serialization;

namespace TimeTrackingApp.Middleware;

[Serializable]
public class Response
{
    [JsonPropertyName("username")]
    public string UserName { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
}