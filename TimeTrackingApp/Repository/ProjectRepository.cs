using TimeTrackingApp.Interfaces;
using TimeTrackingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTrackingApp.Results;

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

            Project projectCleaned = new Project
            {
                ProjectName = project.ProjectName,
                Description = project.Description,
                UserId = project.UserId
            };
            context.Add(projectCleaned);
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

        public Tasks[] GetTasks(int projectId)
        {   
            var tasks = context.Tasks.Where(p => p.ProjectId == projectId).ToArray();
            if (tasks == null)
            {
                // Handle the case when the user is not found, e.g., log the information or throw an exception
                // For this example, we'll just log the information
                Console.WriteLine($"Task with project ID {projectId} not found.");
            }
            return tasks;        
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