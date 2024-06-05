using  TimeTrackingApp.Models;
using TimeTrackingApp.Results;

namespace TimeTrackingApp.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User? GetUser(int userId);
        Result<User> GetUserByUserName(string userName);
        Project[]? GetProjects(int userId);
        bool UserExists(int userId);
        Result<User> CreateUser(User users); 
        bool UpdateUser(User users);
        bool DeleteUser(User users);
        bool Save();
    }
}
        
    