using TimeTrackingApp.Data;
using TimeTrackingApp.Interface;
using TimeTrackingApp.Model;
//using TimeTrackingApp.Dto;
using Microsoft.AspNetCore.Mvc;

namespace TimeTrackingApp.Repository
{
    public class UserProjectRepository : IUserProjectRepository

    {
        private readonly DataContext context;

        public UserProjectRepository(DataContext context)
        {
            this.context = context;
        }
        public bool UserProjectExists(int userProjectId)
        {
            return context.UserProject.Any(u => u.UserProjectId == userProjectId);
        }
        public bool CreateUserProject(UserProject userProject)
        {
            context.Add(userProject);
            return Save();
        }

        public bool DeleteUserProject(UserProject userProject)
        {

            context.Remove(userProject);
            return Save();
        }

        public ICollection<UserProject> GetUserProjects()
        {
            return context.UserProject.ToList();
        }
        public  UserProject GetUserProject(int userProjectId)
        {   
            return context.UserProject.Where(u => u.UserProjectId == userProjectId).FirstOrDefault();
        }
        public bool UpdateUserProject(UserProject userProject)
        {
            context.Update(userProject);
            return Save();
        }  
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0;
        }

    }
}