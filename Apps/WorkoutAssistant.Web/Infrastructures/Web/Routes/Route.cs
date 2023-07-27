namespace WorkoutAssistant.Web.Infrastructures.Web.Routes;

/// <summary>
/// the object to 
/// </summary>
public record Route
{
    /// <summary>
    /// the path of route in application
    /// </summary>
    public string Path { get; set; } = string.Empty;
    
    /// <summary>
    /// the action will be called when route founded
    /// </summary>
    public string Action { get; set; } = string.Empty;
    
    /// <summary>
    /// the controller contain with the action
    /// </summary>
    public string Controller { get; set; } = string.Empty;
    
    /// <summary>
    /// the name of the route in system
    /// </summary>
    public string Name { get; set; } = string.Empty;
}