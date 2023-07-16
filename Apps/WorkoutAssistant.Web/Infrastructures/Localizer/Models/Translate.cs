namespace WorkoutAssistant.Web.Infrastructures.Localizer.Models;

public record Translate
{
    public Translate()
    {
        Text = string.Empty;
        Description = string.Empty;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public string Text { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Description { get; set; }
}