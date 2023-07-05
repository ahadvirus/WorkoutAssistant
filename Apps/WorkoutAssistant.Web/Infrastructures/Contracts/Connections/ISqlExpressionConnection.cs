using FluentNHibernate.Cfg.Db;

namespace WorkoutAssistant.Web.Infrastructures.Contracts.Connections;

public interface ISqlExpressionConnection<T> where T : ConnectionStringBuilder
{
    /// <summary>
    /// Connection builder for NHibernate ORM
    /// </summary>
    /// <param name="connectionExpression"></param>
    void ConnectionExpression(T connectionExpression);
}