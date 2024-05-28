using TimeTrackingApp.Model;
//using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Interface
{
    public interface IUserProjectRepository
    {
        ICollection<UserProject> GetUserProjects();
        UserProject GetUserProject(int userProjectId);
        bool UserProjectExists(int userProjectId);
        bool CreateUserProject(UserProject userProject); 
        bool UpdateUserProject(UserProject userProject);
        bool DeleteUserProject(UserProject userProject);
        bool Save();
    }
}
        
    