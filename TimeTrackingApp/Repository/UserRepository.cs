using System;
using TimeTrackingApp.Interfaces;
using TimeTrackingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace TimeTrackingApp.Repository
{
    public class UserRepository : IUserRepository

    {
        private readonly TimeTrackDbContext context;

        public UserRepository(TimeTrackDbContext context)
        {
            this.context = context;
        }
        public bool UserExists(int userId)
        {
            return context.Users.Any(u => u.UserId == userId);
        }

        public bool CreateUser(User users)
        {
            context.Add(users);
            return Save();
        }

        public bool DeleteUser(User users)
        {

            context.Remove(users);
            return Save();
        }

        public ICollection<User> GetUsers()
        {
            return context.Users.ToList();
        }

        public User? GetUser(int userId)
        {
            var user = context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                // Handle the case when the user is not found, e.g., log the information or throw an exception
                // For this example, we'll just log the information
                Console.WriteLine($"User with ID {userId} not found.");
            }
            return user;
        }


        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateUser(User users)
        {
            context.Update(users);
            return Save();
        }
    }
}