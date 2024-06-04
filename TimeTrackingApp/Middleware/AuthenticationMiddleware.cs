using System.Text.Json;
using TimeTrackingApp.Repository;
using TimeTrackingApp.Models;
using TimeTrackingApp.Interfaces;
using TimeTrackingApp.Results;
using Microsoft.Extensions.Options;

namespace TimeTrackingApp.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AuthenticationService _authenticationService = new();

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserRepository usersRepository)
    {
        if (context.Request.Path == "/api/Auth" || context.Request.Method=="OPTIONS")
        {
            await _next(context);
            return;
        }

        // must have access token
        string accessToken = context.Request.Headers.Authorization;
        
        if (string.IsNullOrEmpty(accessToken))
        {
            await BuildErrorResponse(context, StatusCodes.Status401Unauthorized, new ValidationError("Authentication", "User Unauthorized, please set an access token"));
            return;
        }
        
        // must be a  access token with username and email
        Result<Response> cognitoUserResult = await _authenticationService.ValidateAndExtractUser(accessToken);
        
        if (cognitoUserResult.IsFailure)
        {
            await BuildErrorResponse(context, StatusCodes.Status401Unauthorized, new ValidationError("Authentication", "User Unauthorized"));
            return;
        }

        //Result<User> loggedInUserResult = usersConverter.Convert(cognitoUserResult.Value);

        //if (loggedInUserResult.IsFailure)
        //{
        //    await BuildErrorResponse(context, StatusCodes.Status400BadRequest, loggedInUserResult.ValidationErrors);
        //    context.Response.StatusCode = StatusCodes.Status400BadRequest;
        //    return;
        //}

        //User loggedInUser;

        //Result<User> dbUserResult = usersRepository.GetUser(loggedInUserResult.Value.EmailAddress);

        //if (dbUserResult.IsFailure)
        //{
        //    if (dbUserResult.ValidationErrors.Any(error =>
        //            error.ErrorMessage.Contains("Email Does Not Link To An Existing User")))
        //    {
        //        // create new user if not registered
        //        bool savedUserResult = usersRepository.CreateUser(loggedInUserResult.Value);

        //        if (savedUserResult==false)
        //        {
        //            await BuildErrorResponse(context, StatusCodes.Status500InternalServerError,new ValidationError("Failed to save user"));
        //            return;
        //        }

        //       // loggedInUser = savedUserResult.Value;
        //    }
        //    else
        //    {
        //        await BuildErrorResponse(context, StatusCodes.Status500InternalServerError, dbUserResult.ValidationErrors);
        //        return;
        //    }
        //}
        //else
        //{
        //    loggedInUser = dbUserResult.Value;
        //}

        //context.Items["loggedInUser"] = "1";

        await _next(context);
    }

    private static async Task BuildErrorResponse(HttpContext context, int statusCode, IEnumerable<ValidationError> errors)
    {
        context.Response.StatusCode = statusCode;
        var jsonResponse = JsonSerializer.Serialize(errors);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(jsonResponse);
    }

    private static async Task BuildErrorResponse(HttpContext context, int statusCode, ValidationError error)
    {
        await BuildErrorResponse(context, statusCode, new List<ValidationError> { error });
    }
}
