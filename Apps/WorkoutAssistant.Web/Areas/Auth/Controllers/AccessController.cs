using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Web.Models.Configurations;

namespace WorkoutAssistant.Web.Areas.Auth.Controllers;

[Area(areaName: nameof(Named.Areas.Admin))]
public class AccessController : Controller
{
    public IActionResult Denied()
    {
        return Ok();
    }
}