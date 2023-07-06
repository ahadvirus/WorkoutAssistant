using Microsoft.EntityFrameworkCore;
using WorkoutAssistant.Web.Database.Configurations;
using WorkoutAssistant.Web.Database.Connections;
using WorkoutAssistant.Web.Models.Entities;

namespace WorkoutAssistant.Web.Database.Contexts;

public class ApplicationContext : DbContext
{
    /// <summary>
    /// Connection string to connect database
    /// </summary>
    protected WorkoutSqlConnection Connection { get; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options, WorkoutSqlConnection connection) : base(
        options: options)
    {
        Connection = connection;

        #region InitialTables

        Groups = Set<Group>();

        #endregion
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString: Connection.ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }

    #region DefineTables

    /// <summary>
    /// Access to group table in database
    /// </summary>
    public DbSet<Group> Groups { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Configurations

        modelBuilder.ApplyConfiguration(new GroupConfiguration());

        #endregion
        
        base.OnModelCreating(modelBuilder);
    }
}