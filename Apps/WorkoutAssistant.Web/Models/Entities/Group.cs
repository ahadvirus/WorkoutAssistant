using System;
using WorkoutAssistant.Web.Infrastructures.Contracts;

namespace WorkoutAssistant.Web.Models.Entities;

public class Group : IEntity<Guid>
{
    /// <summary>
    /// The primary key of table
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Name of the group exercise
    /// </summary>
    public string Name { get; set; } = string.Empty;
}