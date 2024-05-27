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
    
    }
    
}

