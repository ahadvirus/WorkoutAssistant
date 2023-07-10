using WorkoutAssistant.Web.Infrastructures.Database.Contracts.Migrations;

namespace WorkoutAssistant.Web.Infrastructures.Database.Configurations.Users;

public class UserTableColumns : ITableColumnsDefinition
{
    public string Primary
    {
        get
        {
            return "Id";
        }
    }

    public string Username
    {
        get
        {
            return "Username";
        }
    }
    
    public string Password
    {
        get
        {
            return "Password";
        }
    }
}