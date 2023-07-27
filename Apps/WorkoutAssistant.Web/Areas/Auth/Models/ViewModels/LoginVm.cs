using System.ComponentModel.DataAnnotations;
using Mediator;
using WorkoutAssistant.Web.Areas.Auth.Models.DataTransfers;

namespace WorkoutAssistant.Web.Areas.Auth.Models.ViewModels;

public record LoginVm : AuthVm, IRequest<UserLoginInfoDto>
{
    /// <summary>
    /// 
    /// </summary>
    [Required] public override string Username { get; set; } = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    [Required] public override string Password { get; set; } = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    public bool RememberMe { get; set; } = false;
}