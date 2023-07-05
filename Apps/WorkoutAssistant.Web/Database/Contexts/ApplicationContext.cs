using Microsoft.EntityFrameworkCore;
using WorkoutAssistant.Web.Database.Connections;
using WorkoutAssistant.Web.Models.Entities;

namespace WorkoutAssistant.Web.Database.Contexts;

public class ApplicationContext : DbContext
{
    /// <summary>
    /// Connection string to connect database
    /// </summary>
    protected GymSqlConnection Connection { get; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options, GymSqlConnection connection) : base(options: options)
    {
        Connection = connection;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString: Connection.ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }

    /// <summary>
    /// Access to group table in database
    /// </summary>
    public DbSet<Group> Groups { get; set; }
}