using TimeTrackingApp.Model;
//using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Interface
{
    public interface IStatusRepository
    {
        ICollection<Status> GetStatuses();
        Status GetStatus(int statusId);
        bool StatusExists(int statusId);
        bool CreateStatus(Status status); 
        bool UpdateStatus(Status status);
        bool DeleteStatus(Status status);
        bool Save();
    }
}
        
    