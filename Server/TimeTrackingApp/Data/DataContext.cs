using Microsoft.EntityFrameworkCore;
using TimeTrackingApp.Model;

namespace TimeTrackingApp.Data
{

    public class DataContext: DbContext
    {

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    
    }
    public DbSet<User> User { get; set; } = default!;

    public DbSet<Status> Status { get; set; } = default!;

    public DbSet<Priority> Priority { get; set; } = default!;

    public DbSet<Project> Project { get; set; } = default!;

    public DbSet<Tasks> Tasks { get; set; } = default!;
    public DbSet<UserProject> UserProject { get; set; } = default!;
    public DbSet<ProjectTask> ProjectTask { get; set; } = default!;
    public DbSet<TimeEntry> TimeEntry { get; set; } = default!;
    }
    
}

