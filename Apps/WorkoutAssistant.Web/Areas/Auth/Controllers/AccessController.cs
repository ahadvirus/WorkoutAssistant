using Microsoft.AspNetCore.Mvc;

namespace WorkoutAssistant.Web.Areas.Auth.Controllers;

public class AccessController : Controller
{
    public IActionResult Denied()
    {
        return Ok();
    }
}