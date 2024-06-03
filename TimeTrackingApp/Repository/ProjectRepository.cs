using TimeTrackingApp.Interfaces;
using TimeTrackingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace TimeTrackingApp.Repository
{
    public class ProjectRepository : IProjectRepository

    {
        private readonly TimeTrackDbContext context;

        public ProjectRepository(TimeTrackDbContext context)
        {
            this.context = context;
        }
        public bool ProjectExists(int projectId)
        {
            return context.Projects.Any(u => u.ProjectId == projectId);
        }
        public bool CreateProject(Project project)
        {
            context.Add(project);
            return Save();
        }

        public bool DeleteProject(Project project)
        {

            context.Remove(project);
            return Save();
        }

        public ICollection<Project> GetProjects()
        {
            return context.Projects.ToList();
        }
        public  Project GetProject(int projectId)
        {   
            return context.Projects.Where(u => u.ProjectId == projectId).FirstOrDefault();
        }
        public bool UpdateProject(Project project)
        {
            context.Update(project);
            return Save();
        }  
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0;
        }

    }
}