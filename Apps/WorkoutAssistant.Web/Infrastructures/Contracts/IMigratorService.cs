using System.Threading.Tasks;

namespace WorkoutAssistant.Web.Infrastructures.Contracts;

public interface IMigratorService
{
    /// <summary>
    /// Checking the database exists in sql server
    /// </summary>
    /// <returns></returns>
    Task EnsureDatabaseAsync();

    /// <summary>
    /// Applied the migration not applied yet on sql server
    /// </summary>
    /// <returns></returns>
    Task MigrateUpAsync();

    /// <summary>
    /// Rollback last migration applied in database
    /// </summary>
    /// <returns></returns>
    Task MigrateDownAsync();
}