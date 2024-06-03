using TimeTrackingApp.Models;

namespace TimeTrackingApp.Interfaces
{
    public interface IPriorityRepository
    {
        ICollection<Priority> GetPriorities();
        Priority GetPriority(int priorityId);
        bool PriorityExists(int priorityId);
        bool CreatePriority(Priority priority); 
        bool UpdatePriority(Priority priority);
        bool DeletePriority(Priority priority);
        bool Save();
    }
}
        
    