using System.Threading;
using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Web.Infrastructures.Extensions;
using WorkoutAssistant.Web.Models.Configurations;
using WorkoutAssistant.Web.Models.ViewModels;

namespace WorkoutAssistant.Web.Controllers;

public class LanguageController : Controller
{
    [Route(template: nameof(Named.Routes.Language.ChangeLanguage), Name = nameof(Named.Routes.Language.ChangeLanguage))]
    public IActionResult Index(
        [Bind(include: new string[]
        {
            nameof(ChangeLanguageVm.Language),
            nameof(ChangeLanguageVm.ReturnUrl)
        })] ChangeLanguageVm entry,
        CancellationToken cancellationToken
        )
    {
        string localUrl = string.IsNullOrEmpty(entry.ReturnUrl)
            ? nameof(Named.Routes.Home).GetRoutePathByName()
            : entry.ReturnUrl;
        
        return LocalRedirect(localUrl: localUrl);
    }
}