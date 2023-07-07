using System;

namespace WorkoutAssistant.Web.Infrastructures.Database.Migrations.EntitiesPrimaryIndex;

public class UserIndexes
{
    /// <summary>
    /// Return primary key for mehdi user
    /// </summary>
    public Guid MehdiId
    {
        get
        {
            return Guid.Parse(input: "c0e60dd6-b518-40bb-b127-4b7d8fc06e19");
        }
    }

    /// <summary>
    /// Return primary key for ahad user
    /// </summary>
    public Guid AhadId
    {
        get
        {
            return Guid.Parse(input: "af1a4cec-6fb0-4288-9585-d85e1c1719ec");
        }
    }
}