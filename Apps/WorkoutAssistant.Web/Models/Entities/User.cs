using System;
using WorkoutAssistant.Web.Infrastructures.Contracts;

namespace WorkoutAssistant.Web.Models.Entities;

public class User : IEntity<Guid>
{
    /// <summary>
    /// Primary key of table
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Save username of user in it
    /// </summary>
    public string Username { get; set; } = string.Empty;
    
    /// <summary>
    /// Keeping the password of users in it
    /// </summary>
    public string Password { get; set; } = string.Empty;
}