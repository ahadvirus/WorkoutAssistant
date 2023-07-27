using WorkoutAssistant.Web.Infrastructures.Web.Routes;

namespace WorkoutAssistant.Web.Models.Configurations.Names.RouteNames;

public record Language
{
    public Route ChangeLanguage
    {
        get
        {
            return new Route()
            {
                Path = "/language/change/{language}",
                Controller = nameof(Controllers.HomeController),
                Action = nameof(Controllers.HomeController.Index),
                Name = nameof(ChangeLanguage),
            };
        }
    }
}