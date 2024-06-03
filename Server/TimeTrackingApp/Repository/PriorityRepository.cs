using TimeTrackingApp.Interfaces;
using TimeTrackingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace TimeTrackingApp.Repository
{
    public class PriorityRepository : IPriorityRepository

    {
        private readonly TimeTrackDbContext context;

        public PriorityRepository(TimeTrackDbContext context)
        {
            this.context = context;
        }
        public bool PriorityExists(int priorityId)
        {
            return context.Priorities.Any(u => u.PriorityId == priorityId);
        }

        public bool CreatePriority(Priority priority)
        {
            context.Add(priority);
            return Save();
        }

        public bool DeletePriority(Priority priority)
        {

            context.Remove(priority);
            return Save();
        }

        public ICollection<Priority> GetPriorities()
        {
            return context.Priorities.ToList();
        }

        public Priority GetPriority(int priorityId)
        {
            
            return context.Priorities.Where(u => u.PriorityId == priorityId).FirstOrDefault();
        }
        public bool UpdatePriority(Priority priority)
        {
            context.Update(priority);
            return Save();
        }  
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0;
        }

    }
}