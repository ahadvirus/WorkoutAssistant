namespace WorkoutAssistant.Web.Infrastructures.Localizer.Models;

public record JsonLocalizationOption
{
    public JsonLocalizationOption(string path)
    {
        Path = path;
    }
    
    public string Path { get; init; }
}