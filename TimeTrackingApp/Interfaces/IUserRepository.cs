using  TimeTrackingApp.Models;

namespace TimeTrackingApp.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User? GetUser(int userId);
        Project[]? GetProjects(int userId);
        bool UserExists(int userId);
        bool CreateUser(User users); 
        bool UpdateUser(User users);
        bool DeleteUser(User users);
        bool Save();
    }
}
        
    