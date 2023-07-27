namespace WorkoutAssistant.Web.Models.Configurations.Names;

/// <summary>
/// All sections exists in view
/// </summary>
public record Sections
{
    /// <summary>
    /// 
    /// </summary>
    public string Title
    {
        get
        {
            return nameof(Title);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public string Styles
    {
        get
        {
            return nameof(Styles);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public string Scripts
    {
        get
        {
            return nameof(Scripts);
        }
    }
}