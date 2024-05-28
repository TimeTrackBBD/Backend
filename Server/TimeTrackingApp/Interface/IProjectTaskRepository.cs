using TimeTrackingApp.Model;
//using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Interface
{
    public interface IProjectTaskRepository
    {
        ICollection<ProjectTask> GetProjectTasks();
        ProjectTask GetProjectTask(int projectTaskId);
        bool ProjectTaskExists(int projectTaskId);
        bool CreateProjectTask(ProjectTask projectTask); 
        bool UpdateProjectTask(ProjectTask projectTask);
        bool DeleteProjectTask(ProjectTask projectTask);
        bool Save();
    }
}
        
    