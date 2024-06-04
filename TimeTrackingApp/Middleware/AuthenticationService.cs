using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using TimeTrackingApp.Results;
namespace TimeTrackingApp.Middleware;

public class AuthenticationService
{

    public async Task<Result<Response>> ValidateAndExtractUser(string accessToken)
    {
        string url = $"https://time-track.auth.eu-west-1.amazoncognito.com/oauth2/userInfo";
        HttpClient _httpClient = new HttpClient();
        if (accessToken.StartsWith("Bearer "))
        {
            accessToken = accessToken.Split(' ')[1];
        }
        if (accessToken == null )
        {
            return Result.Fail<Response>(new ValidationError("Access token is not set."));
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return Result.Fail<Response>(new ValidationError("Could Not Authenticate Client With Cognito."));
        
        string userDetails = await response.Content.ReadAsStringAsync();
        
        var user = JsonSerializer.Deserialize<Response>(userDetails);
        
        // check inside as well.
        if (user is null)
            return Result.Fail<Response>(new ValidationError("Could Not Authenticate Client With Cognito."));

        return Result.Ok(user);
    }
}