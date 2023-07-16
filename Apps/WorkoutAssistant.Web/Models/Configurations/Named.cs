namespace WorkoutAssistant.Web.Models.Configurations;

public record Named
{
    public Names.Areas Areas
    {
        get { return new Names.Areas(); }
    }
    
    public Names.View View
    {
        get { return new Names.View(); }
    }
    
    public Names.Routes Routes
    {
        get { return new Names.Routes(); }
    }
}