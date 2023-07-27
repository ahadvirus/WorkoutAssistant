using Microsoft.AspNetCore.Mvc;

namespace WorkoutAssistant.Web.Areas.Admin.Controllers;

[Area(areaName: nameof(Models.Configurations.Named.Areas.Admin))]
public class DashboardController : Infrastructures.Web.Controllers.Controller
{
    // GET
    [Route(template: nameof(Models.Configurations.Named.Routes.Admin.Dashboard), Name = nameof(Models.Configurations.Named.Routes.Admin.Dashboard))]
    public IActionResult Index()
    {
        return View();
    }
}