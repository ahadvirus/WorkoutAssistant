namespace WorkoutAssistant.Web.Areas.Auth.Models.ViewModels;

public record AuthVm
{
    public virtual string Username { get; set; } = string.Empty;

    public virtual string Password { get; set; } = string.Empty;
}