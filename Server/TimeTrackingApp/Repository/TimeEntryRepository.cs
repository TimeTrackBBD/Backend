using TimeTrackingApp.Data;
using TimeTrackingApp.Interface;
using TimeTrackingApp.Model;
//using TimeTrackingApp.Dto;
using Microsoft.AspNetCore.Mvc;

namespace TimeTrackingApp.Repository
{
    public class TimeEntryRepository : ITimeEntryRepository

    {
        private readonly DataContext context;

        public TimeEntryRepository(DataContext context)
        {
            this.context = context;
        }
        public bool TimeEntryExists(int timeEntryId)
        {
            return context.TimeEntry.Any(u => u.TimeEntryId == timeEntryId);
        }
        public bool CreateTimeEntry(TimeEntry timeEntry)
        {
            context.Add(timeEntry);
            return Save();
        }

        public bool DeleteTimeEntry(TimeEntry timeEntry)
        {

            context.Remove(timeEntry);
            return Save();
        }

        public ICollection<TimeEntry> GetTimeEntrys()
        {
            return context.TimeEntry.ToList();
        }
        public  TimeEntry GetTimeEntry(int timeEntryId)
        {   
            return context.TimeEntry.Where(u => u.TimeEntryId == timeEntryId).FirstOrDefault();
        }
        public bool UpdateTimeEntry(TimeEntry timeEntry)
        {
            context.Update(timeEntry);
            return Save();
        }  
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0;
        }

    }
}