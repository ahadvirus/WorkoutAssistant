namespace WorkoutAssistant.Web.Models.Configurations.Names;

/// <summary>
/// Represent all area names exist in application
/// </summary>
public class Areas
{
    /// <summary>
    /// Return name of admin area
    /// </summary>
    public string Admin
    {
        get
        {
            return nameof(Admin);
        }
    }

    /// <summary>
    /// Return name of auth area
    /// </summary>
    public string Auth { get; set; }
}