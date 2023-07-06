using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using WorkoutAssistant.Web.Database.Connections;
using WorkoutAssistant.Web.Infrastructures.Contracts;

namespace WorkoutAssistant.Web.Infrastructures.Database.Migrations;

public class MigrationService : IMigratorService
{
    /// <summary>
    /// Connection string for current sql
    /// </summary>
    protected WorkoutSqlConnection Connection { get; }

    /// <summary>
    /// Connection to master database for ensure database exist
    /// </summary>
    protected MasterSqlConnection Master { get; }

    /// <summary>
    /// Migration Runner for provide for run
    /// </summary>
    protected IMigrationRunner Runner { get; }


    public MigrationService(MasterSqlConnection master, WorkoutSqlConnection connection, IMigrationRunner runner)
    {
        Master = master;
        Connection = connection;
        Runner = runner;
    }

    public async Task EnsureDatabaseAsync()
    {
        bool exists = false;

        await using (SqlConnection connection = new SqlConnection(Master.ConnectionString))
        {
            await using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [sys].[databases] WHERE [name] = @name";
                command.Parameters.Clear();
                command.Parameters.AddWithValue(parameterName: "name", value: Connection.Database);

                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        exists = true;
                    }
                }

                await connection.CloseAsync();

                if (!exists)
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = string.Format(format: "CREATE DATABASE {0}", args: new object?[] { Connection.Database });

                    await connection.OpenAsync();

                    await command.ExecuteNonQueryAsync();

                    await connection.CloseAsync();
                }
            }

        }
    }

    public async Task MigrateUpAsync()
    {
        await EnsureDatabaseAsync();

        if (Runner.HasMigrationsToApplyUp())
        {
            Runner.MigrateUp();
        }

    }

    public async Task MigrateDownAsync()
    {
        await EnsureDatabaseAsync();

        long version = Runner.MigrationLoader.LoadMigrations()
            .OrderByDescending(keySelector: pair => pair.Key)
            .Where(predicate: pair => !Runner.HasMigrationsToApplyUp(version: pair.Key))
            .Select(selector: pair => pair.Key)
            .FirstOrDefault();

        if (version != default)
        {
            Runner.MigrateDown(version: version);
        }
    }
}