using TimeTrackingApp.Data;
using TimeTrackingApp.Interface;
using TimeTrackingApp.Model;
//using TimeTrackingApp.Dto;
using Microsoft.AspNetCore.Mvc;

namespace TimeTrackingApp.Repository
{
    public class ProjectTaskRepository : IProjectTaskRepository

    {
        private readonly DataContext context;

        public ProjectTaskRepository(DataContext context)
        {
            this.context = context;
        }
        public bool ProjectTaskExists(int projectTaskId)
        {
            return context.ProjectTask.Any(u => u.ProjectTaskId == projectTaskId);
        }
        public bool CreateProjectTask(ProjectTask projectTask)
        {
            context.Add(projectTask);
            return Save();
        }

        public bool DeleteProjectTask(ProjectTask projectTask)
        {

            context.Remove(projectTask);
            return Save();
        }

        public ICollection<ProjectTask> GetProjectTasks()
        {
            return context.ProjectTask.ToList();
        }
        public  ProjectTask GetProjectTask(int projectTaskId)
        {   
            return context.ProjectTask.Where(u => u.ProjectTaskId == projectTaskId).FirstOrDefault();
        }
        public bool UpdateProjectTask(ProjectTask projectTask)
        {
            context.Update(projectTask);
            return Save();
        }  
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0;
        }

    }
}