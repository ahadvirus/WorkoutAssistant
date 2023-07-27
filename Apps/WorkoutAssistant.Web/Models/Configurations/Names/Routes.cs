using WorkoutAssistant.Web.Infrastructures.Web.Routes;
using WorkoutAssistant.Web.Models.Configurations.Names.RouteNames;

namespace WorkoutAssistant.Web.Models.Configurations.Names;

[Route(version: 20230726124623)]
public record Routes
{
    public Auth Auth
    {
        get { return new Auth(); }
    }
    
    public Admin Admin
    {
        get { return new Admin(); }
    }

    public Language Language
    {
        get { return new Language(); }
    }

    public Route Home
    {
        get
        {
            return new Route()
            {
                Path = "/",
                Controller = nameof(Controllers.HomeController),
                Action = nameof(Controllers.HomeController.Index),
                Name = nameof(Home),
            };
        }
    }
}