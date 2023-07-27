using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Web.Infrastructures.Translators.Localizations.Models;
using WorkoutAssistant.Web.Models.Configurations;

namespace WorkoutAssistant.Web.Controllers;

public class ChangeController : Controller
{
    private ResourceCollection Resources { get; }

    public ChangeController(ResourceCollection resources)
    {
        Resources = resources;
    }

    // GET
    //[Route("/[controller]")]
    public IActionResult Index()
    {
        LanguageCollection? languages = Resources[resource: "Controllers.HomeController"];

        if (languages != null)
        {
            TranslateCollection? translates = languages[language: "en-US"];

            if (translates != null)
            {
                Translate? translate = translates[key: "Message"];
                
                if (translate != null)
                {
                    translate.Text = "Change by code in change route";
                }
            }
        }
        
        return RedirectToRoute(nameof(Named.Routes.Home));
    }
}