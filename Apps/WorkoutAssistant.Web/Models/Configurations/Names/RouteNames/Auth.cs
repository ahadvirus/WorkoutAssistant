using WorkoutAssistant.Web.Infrastructures.Web.Routes;

namespace WorkoutAssistant.Web.Models.Configurations.Names.RouteNames;

/// <summary>
/// the all routes define in auth area
/// </summary>
public record Auth
{
    /// <summary>
    /// 
    /// </summary>
    public Route Login
    {
        get
        {
            return new Route()
            {
                Path = "/auth/login",
                Controller = nameof(Controllers.HomeController),
                Action = nameof(Controllers.HomeController.Index),
                Name = nameof(Login)
            };
        }
    }
}