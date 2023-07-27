using WorkoutAssistant.Web.Infrastructures.Web.Routes;

namespace WorkoutAssistant.Web.Models.Configurations.Names.RouteNames;

public record Admin
{
    public Route Dashboard
    {
        get
        {
            return new Route()
            {
                Path = "/admin/dashboard",
                Controller = nameof(Web.Areas.Admin.Controllers.DashboardController),
                Action = nameof(Web.Areas.Admin.Controllers.DashboardController.Index),
                Name = nameof(Dashboard)
            };
        }
    }
}