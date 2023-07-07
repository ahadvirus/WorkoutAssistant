using System;

namespace WorkoutAssistant.Web.Infrastructures.Database.Migrations.EntitiesPrimaryIndex;

public class GroupIndexes
{
    /// <summary>
    /// Return primary key value for male crossfit exercises group
    /// </summary>
    public Guid MaleCrossFitIndex
    {
        get
        {
            return Guid.Parse(input: "a4cfdcbc-c5be-4411-a922-30fab3217c17");
        }
    }
    
    /// <summary>
    /// Return primary key value for female crossfit exercises group
    /// </summary>
    public Guid FemaleCrossFitIndex
    {
        get
        {
            return Guid.Parse(input: "8a74f654-1660-46b3-a1aa-ff8a5e64e415");
        }
    }
}