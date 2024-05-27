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

    public DbSet<Questions> Questions { get; set; } = default!;

    public DbSet<Answers> Answers { get; set; } = default!;
    
    }
    
}

