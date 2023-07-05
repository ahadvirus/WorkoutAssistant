namespace WorkoutAssistant.Web.Infrastructures.Contracts.Connections;

public interface ISqlConnection
{
    /// <summary>
    /// Return a connection string of sql
    /// </summary>
    string ConnectionString { get; }
}