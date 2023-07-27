using System;
using System.Threading;
using System.Threading.Tasks;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Web.Areas.Auth.Models.DataTransfers;
using WorkoutAssistant.Web.Areas.Auth.Models.ViewModels;

namespace WorkoutAssistant.Web.Areas.Auth.Controllers;

[Area(areaName: nameof(Web.Models.Configurations.Named.Areas.Auth))]
public class LoginController : Infrastructures.Web.Controllers.Controller
{
    // GET
    [Route(template: nameof(Web.Models.Configurations.Named.Routes.Auth.Login),
        Name = nameof(Web.Models.Configurations.Named.Routes.Auth.Login))]
    public IActionResult Index()
    {
        return View();
    }

    [Route(template: nameof(Web.Models.Configurations.Named.Routes.Auth.Login))]
    public async Task<IActionResult> Index(
        [Bind(include: new string[]
        {
            nameof(LoginVm.Username),
            nameof(LoginVm.Password),
            nameof(LoginVm.RememberMe)
        })] LoginVm entry,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        IActionResult result = View(entry);

        try
        {
            UserLoginInfoDto info = await mediator.Send(request: entry, cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            //
        }
        
        return result;
    }
}