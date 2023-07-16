using WorkoutAssistant.Web.Models.Configurations.Names.RouteNames;

namespace WorkoutAssistant.Web.Models.Configurations.Names;

public record Routes
{
    public Auth Auth
    {
        get
        {
            return new Auth();
        }
    }
    
    public Language Language
    {
        get
        {
            return new Language();
        }
    }

}