using Microsoft.AspNetCore.Mvc;

namespace WorkoutAssistant.Web.Areas.Admin.Controllers;

public class DashboardController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}