using TimeTrackingApp.Model;
//using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Interface
{
    public interface IProjectRepository
    {
        ICollection<Project> GetProjects();
        Project GetProject(int projectId);
        bool ProjectExists(int projectId);
        bool CreateProject(Project project); 
        bool UpdateProject(Project project);
        bool DeleteProject(Project project);
        bool Save();
    }
}
        
    