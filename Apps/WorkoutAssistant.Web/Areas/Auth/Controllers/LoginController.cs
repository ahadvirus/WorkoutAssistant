using Microsoft.AspNetCore.Mvc;

namespace WorkoutAssistant.Web.Areas.Auth.Controllers;

public class LoginController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}