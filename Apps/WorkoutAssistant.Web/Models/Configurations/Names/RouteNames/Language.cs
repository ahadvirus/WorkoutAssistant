namespace WorkoutAssistant.Web.Models.Configurations.Names.RouteNames;

public record Language
{
    public string Change
    {
        get
        {
            return string.Format(format: "{0}{1}", args: new object?[] { nameof(Change), nameof(Language) });
        }
    }
}