using System;
using TimeTrackingApp.Interfaces;
using TimeTrackingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTrackingApp.Results;

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

        public Result<User> CreateUser(User users)
        {
            User user = new User {
                UserName = users.UserName,
                Email = users.Email
            };
            context.Add(user);
            context.SaveChanges();
            return Result.Ok(user);
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
        public Result<User> GetUserByUserName(string userName)
        {
            var user = context.Users.FirstOrDefault(user => user.UserName == userName);

            if (user is null)
                return Result.Fail<User>
                    (new ValidationError("Username", "Username Does Not Link To An Existing User."));

            return Result.Ok(user);
        }

        public Project[]? GetProjects(int userId)
        {
            var projects = context.Projects.Where(p => p.UserId == userId).ToArray();
            if (projects == null)
            {
                // Handle the case when the user is not found, e.g., log the information or throw an exception
                // For this example, we'll just log the information
                Console.WriteLine($"Project with user ID {userId} not found.");
            }
            return projects;
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateUser(User users)
        {
            User user = new User
            {
                UserName = users.UserName,
                Email = users.Email
            };
            context.Update(user);
            return Save();
        }
    }
}