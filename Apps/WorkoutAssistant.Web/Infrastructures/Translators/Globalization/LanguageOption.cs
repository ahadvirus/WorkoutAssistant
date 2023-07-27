namespace WorkoutAssistant.Web.Infrastructures.Translators.Globalization;

public record LanguageOption
{
    public LanguageOption(string address)
    {
        Address = address;
    }
    
    public string Address { get; init; } = string.Empty;
}