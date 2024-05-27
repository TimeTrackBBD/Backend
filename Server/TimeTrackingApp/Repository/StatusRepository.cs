using TimeTrackingApp.Data;
using TimeTrackingApp.Interface;
using TimeTrackingApp.Model;
//using TimeTrackingApp.Dto;
using Microsoft.AspNetCore.Mvc;

namespace TimeTrackingApp.Repository
{
    public class StatusRepository : IStatusRepository

    {
        private readonly DataContext context;

        public StatusRepository(DataContext context)
        {
            this.context = context;
        }
        public bool StatusExists(int statusId)
        {
            return context.Status.Any(u => u.StatusId == statusId);
        }

        public bool CreateStatus(Status status)
        {
            context.Add(status);
            return Save();
        }

        public bool DeleteStatus(Status status)
        {

            context.Remove(status);
            return Save();
        }

        public ICollection<Status> GetStatuses()
        {
            return context.Status.ToList();
        }

        public Status GetStatus(int statusId)
        {
            
            return context.Status.Where(u => u.StatusId == statusId).FirstOrDefault();
        }
        public bool UpdateStatus(Status status)
        {
            context.Update(status);
            return Save();
        }

   
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0;
        }

    }
}