using TimeTrackingApp.Middleware;
using TimeTrackingApp.Models;
using TimeTrackingApp.Interfaces;
using TimeTrackingApp.Results;


namespace TimeTrackingApp.Converters;

public class UsersConverter
    {
    private readonly IUserRepository _userRepository;


    public UsersConverter( IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public Result<User> Convert(Response cognitoUser)
    {
        User user = new User
        {
            UserName = cognitoUser.UserName,
            Email = cognitoUser.Email
        };
        
        return Result.Ok(user);
    }
    
}