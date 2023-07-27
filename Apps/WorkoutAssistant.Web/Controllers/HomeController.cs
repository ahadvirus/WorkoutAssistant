using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using WorkoutAssistant.Web.Database.Contexts;
using WorkoutAssistant.Web.Infrastructures.Web.Views;
using WorkoutAssistant.Web.Models;
using WorkoutAssistant.Web.Models.Configurations;
using WorkoutAssistant.Web.Models.Entities;

namespace WorkoutAssistant.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IStringLocalizer<HomeController> _localizer;

    public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer)
    {
        _logger = logger;
        _localizer = localizer;
    }

    [Route(template: nameof(Named.Routes.Home), Name = nameof(Named.Routes.Home))]
    public IActionResult Index()
    {
        return View(); // Ok(_localizer[name: "Message"]);
    }

    public async Task<ActionResult<IEnumerable<Group>>> Groups(
        [FromServices] ApplicationContext context,
        CancellationToken cancellationToken
    )
    {
        return Ok(value: await context.Groups.ToListAsync(cancellationToken: cancellationToken));
    }

    public async Task<ActionResult<IEnumerable<Group>>> Roles(
        [FromServices] ApplicationContext context,
        CancellationToken cancellationToken
    )
    {
        return Ok(value: await context.Roles.ToListAsync(cancellationToken: cancellationToken));
    }

    public async Task<ActionResult<IEnumerable<Group>>> Users(
        [FromServices] ApplicationContext context,
        CancellationToken cancellationToken
    )
    {
        return Ok(value: await context.Users.ToListAsync(cancellationToken: cancellationToken));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}