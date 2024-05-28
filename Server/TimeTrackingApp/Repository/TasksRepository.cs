using TimeTrackingApp.Data;
using TimeTrackingApp.Interface;
using TimeTrackingApp.Model;
//using TimeTrackingApp.Dto;
using Microsoft.AspNetCore.Mvc;

namespace TimeTrackingApp.Repository
{
    public class TasksRepository : ITasksRepository

    {
        private readonly DataContext context;

        public TasksRepository(DataContext context)
        {
            this.context = context;
        }
        public bool TaskExists(int taskId)
        {
            return context.Tasks.Any(u => u.TaskID == taskId);
        }
        public bool CreateTask(Tasks task)
        {
            context.Add(task);
            return Save();
        }

        public bool DeleteTask(Tasks task)
        {

            context.Remove(task);
            return Save();
        }

        public ICollection<Tasks> GetTasks()
        {
            return context.Tasks.ToList();
        }
        public  Tasks GetTask(int taskId)
        {   
            return context.Tasks.Where(u => u.TaskID == taskId).FirstOrDefault();
        }
        public bool UpdateTask(Tasks task)
        {
            context.Update(task);
            return Save();
        }  
        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0;
        }

    }
}