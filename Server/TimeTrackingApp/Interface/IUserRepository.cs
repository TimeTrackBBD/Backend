using TimeTrackingApp.Model;
//using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Interface
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int userId);
        bool UserExists(int userId);
        bool CreateUser(User users); 
        bool UpdateUser(User users);
        bool DeleteUser(User users);
        bool Save();
    }
}
        
    