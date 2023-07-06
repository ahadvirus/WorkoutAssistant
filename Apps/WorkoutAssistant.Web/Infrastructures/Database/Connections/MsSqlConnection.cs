using Microsoft.Data.SqlClient;
using WorkoutAssistant.Web.Infrastructures.Contracts.Connections;

namespace WorkoutAssistant.Web.Infrastructures.Database.Connections;

public abstract class MsSqlConnection : ISqlConnection
{
    /// <summary>
    /// 
    /// </summary>
    public string Server { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string Database { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string Password { get; set; } = string.Empty;

    public string ConnectionString
    {
        get
        {
            return (new SqlConnectionStringBuilder()
                {
                    DataSource = Server,
                    InitialCatalog = Database,
                    UserID = Username,
                    Password = Password,
                    IntegratedSecurity = true,
                    TrustServerCertificate = true
                })
                .ConnectionString;
        }
    }
}