using FluentNHibernate.Cfg.Db;
using WorkoutAssistant.Web.Infrastructures.Contracts.Connections;
using WorkoutAssistant.Web.Infrastructures.Database.Connections;

namespace WorkoutAssistant.Web.Database.Connections;

public class WorkoutSqlConnection : MsSqlConnection, ISqlExpressionConnection<MsSqlConnectionStringBuilder>
{
    public void ConnectionExpression(MsSqlConnectionStringBuilder connectionExpression)
    {
        connectionExpression
            .Server(server: Server)
            .Database(database: Database)
            .Username(username: Username)
            .Password(password: Password)
            .TrustedConnection();
    }
}