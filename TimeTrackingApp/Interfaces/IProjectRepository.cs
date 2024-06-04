using TimeTrackingApp.Models;

namespace TimeTrackingApp.Interfaces
{
    public interface IProjectRepository
    {
        ICollection<Project> GetProjects();
        Project GetProject(int projectId);
        Tasks[] GetTasks(int projectId);
        bool ProjectExists(int projectId);
        bool CreateProject(Project project); 
        bool UpdateProject(Project project);
        bool DeleteProject(Project project);
        bool Save();
    }
}
        
    