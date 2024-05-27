using TimeTrackingApp.Data;
using TimeTrackingApp.Interface;
using TimeTrackingApp.Model;
//using TimeTrackingApp.Dto;
using Microsoft.AspNetCore.Mvc;

namespace TimeTrackingApp.Repository
{
    public class UserRepository : IUserRepository

    {
        private readonly DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }
        public bool UserExists(int userId)
        {
            return context.User.Any(u => u.UserId == userId);
        }

        public bool CreateUser(User user)
        {
            context.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {

            context.Remove(user);
            return Save();
        }

        public ICollection<User> GetUsers()
        {
            return context.User.ToList();
        }

        public User GetUser(int userId)
        {
            
            return context.User.Where(u => u.UserId == userId).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateUser(User user)
        {
            context.Update(user);
            return Save();
        }
    }
}