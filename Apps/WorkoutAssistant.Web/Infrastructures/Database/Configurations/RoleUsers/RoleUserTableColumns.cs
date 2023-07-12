using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

namespace WorkoutAssistant.Web.Infrastructures.Database.Configurations.RoleUsers;

public class RoleUserTableColumns : ITableColumnsDefinition
{
    public string Primary
    {
        get
        {
            return "Id";
        }
    }

    public string RoleId
    {
        get
        {
            return "RoleId";
        }
    }
    
    public string UserId
    {
        get
        {
            return "UserId";
        }
    }
}