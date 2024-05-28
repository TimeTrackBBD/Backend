using TimeTrackingApp.Model;
//using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Interface
{
    public interface ITasksRepository
    {
        ICollection<Tasks> GetTasks();
        Tasks GetTask(int taskId);
        bool TaskExists(int taskId);
        bool CreateTask(Tasks task); 
        bool UpdateTask(Tasks task);
        bool DeleteTask(Tasks task);
        bool Save();
    }
}
        
    