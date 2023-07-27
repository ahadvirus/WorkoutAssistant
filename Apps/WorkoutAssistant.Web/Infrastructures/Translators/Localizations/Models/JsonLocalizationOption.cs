namespace WorkoutAssistant.Web.Infrastructures.Translators.Localizations.Models;

public record JsonLocalizationOption
{
    public JsonLocalizationOption(string path)
    {
        Path = path;
    }
    
    public string Path { get; init; }
}