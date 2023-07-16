using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Web.Models.Configurations;

namespace WorkoutAssistant.Web.Areas.Auth.Controllers;

[Area(areaName: nameof(Named.Areas.Auth))]
public class LoginController : Infrastructures.Web.Controllers.Controller
{
    // GET
    [Route(template: "/[action]/[controller]", Name = nameof(Named.Routes.Auth.Login))]
    public IActionResult Index()
    {
        return View();
    }
}