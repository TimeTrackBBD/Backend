using Microsoft.AspNetCore.Mvc;
using TimeTrackingApp.Interfaces;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using System.Web;


namespace TimeTrackingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : Controller
    {
        private static readonly string clientId =  Environment.GetEnvironmentVariable("CLIENT_ID");
        private static readonly string redirectUri =  Environment.GetEnvironmentVariable("REDIRECT_URI");
        private static readonly string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
        private static readonly string tokenEndpoint = Environment.GetEnvironmentVariable("TOKEN_ENDPOINT");
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost()]
        [ProducesResponseType(200, Type = typeof(AccessTokenResponse))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] RequestBody body)
        {
            string code = body.code;
            if (code == null)
            {
                return StatusCode(400, new ErrorResponse
                {
                    Error = "Bad request",
                    Message = "No authorisation code was submitted in body"
                });
            }
            string redirectEncode = HttpUtility.UrlEncode(redirectUri);
            string tokenUrl = $"{tokenEndpoint}?client_id={clientId}&client_secret={clientSecret}&redirect_uri={redirectEncode}&grant_type=authorization_code&code={code}";
            string stringBody = $"";
            AuthResponse token;
            try
            {
                token = await AuthenticateUser(tokenUrl,stringBody);
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "Internal server error",
                    Message = "Server failed to hit the Cognito API at all"
                });
            }

            if (token == null)
            {
                return StatusCode(400, new ErrorResponse
                {
                    Error = "Bad request",
                    Message = "Server hit the Cognito API but the token returned was not saved. Code may be invalid"
                });
            }
            else
            {
                return StatusCode(200, token);
            }
        }

        private async Task<AuthResponse> AuthenticateUser(string tokenUrl, string stringBody)
        {
            
            HttpClient _httpClient = new HttpClient();
            if (tokenUrl == null)
            {
                return null;
            }
            HttpContent content = new StringContent(stringBody,System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
            HttpResponseMessage response = await _httpClient.PostAsync(tokenUrl, content);

            if (!response.IsSuccessStatusCode)
                return null;

            string userDetails = await response.Content.ReadAsStringAsync();

            AuthResponse token = JsonSerializer.Deserialize<AuthResponse>(userDetails);

            return token;
        }
    }

    [Serializable]
    public class RequestBody
    {
        public string code {  get; set; } 
    }

        [Serializable]
    public class AuthResponse
    {
        public string id_token { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }

    public class ErrorResponse
    {
        public string Error { get; set; }
        public string Message { get; set; }
    }

}



