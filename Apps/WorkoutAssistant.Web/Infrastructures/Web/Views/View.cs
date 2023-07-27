using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using WorkoutAssistant.Web.Models.Configurations;

namespace WorkoutAssistant.Web.Infrastructures.Web.Views;

public abstract class View<T> : RazorPage<T>
{
    protected Named Named
    {
        get
        {
            return new Named();
        }
    }
    
    [RazorInject]
    public IViewLocalizer ViewLocalizer { get; set; }
}