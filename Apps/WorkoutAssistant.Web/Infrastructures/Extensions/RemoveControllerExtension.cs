using Microsoft.AspNetCore.Mvc;

namespace WorkoutAssistant.Web.Infrastructures.Extensions;

public static class RemoveControllerExtension
{
    /// <summary>
    /// Remove Controller in string
    /// </summary>
    /// <param name="name"><see cref="string"/>The string you want remove controller from it</param>
    /// <returns><see cref="string"/>Return a string without controller in it</returns>
    public static string RemoveController(this string name)
    {
        return name.Replace(oldValue: nameof(Controller), newValue: string.Empty);
    }
}