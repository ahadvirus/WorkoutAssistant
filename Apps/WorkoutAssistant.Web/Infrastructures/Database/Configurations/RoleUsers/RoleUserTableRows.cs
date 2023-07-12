using System;

namespace WorkoutAssistant.Web.Infrastructures.Database.Configurations.RoleUsers;

public class RoleUserTableRows
{
    public Guid MehdiIdToAdminId
    {
        get
        {
            return Guid.Parse(input: "b8682364-28ab-411c-959d-61d9a9fae454");
        }
    }
    
    public Guid AhadIdToAdminId
    {
        get
        {
            return Guid.Parse(input: "d7f0acd1-c654-45ee-b0e2-df585197e9e5");
        }
    }
}