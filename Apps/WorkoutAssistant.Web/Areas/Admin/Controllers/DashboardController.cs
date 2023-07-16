using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Web.Models.Configurations;

namespace WorkoutAssistant.Web.Areas.Admin.Controllers;

[Area(areaName: nameof(Named.Areas.Admin))]
public class DashboardController : Infrastructures.Web.Controllers.Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}