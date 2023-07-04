using System;

namespace WorkoutAssistant.Web.Models.Entities;

public class Group
{
    /// <summary>
    /// The primary key of table
    /// </summary>
    public virtual Guid Id { get; set; }
    
    /// <summary>
    /// Name of the group exercise
    /// </summary>
    public virtual string Name { get; set; } = string.Empty;
}