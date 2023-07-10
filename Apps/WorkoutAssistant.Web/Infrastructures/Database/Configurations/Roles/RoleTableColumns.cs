using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

namespace WorkoutAssistant.Web.Infrastructures.Database.Configurations.Roles;

public class RoleTableColumns : ITableColumnsDefinition
{
    public string Primary
    {
        get
        {
            return "Id";
        }
    }

    public string Name
    {
        get
        {
            return "Name";
        }
    }
}