using TimeTrackingApp.Model;
//using TimeTrackingApp.Dto;

namespace TimeTrackingApp.Interface
{
    public interface ITimeEntryRepository
    {
        ICollection<TimeEntry> GetTimeEntrys();
        TimeEntry GetTimeEntry(int timeEntryId);
        bool TimeEntryExists(int timeEntryId);
        bool CreateTimeEntry(TimeEntry timeEntry); 
        bool UpdateTimeEntry(TimeEntry timeEntry);
        bool DeleteTimeEntry(TimeEntry timeEntry);
        bool Save();
    }
}
        
    