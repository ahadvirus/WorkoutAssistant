using System;
using WorkoutAssistant.Web.Infrastructures.Contracts;

namespace WorkoutAssistant.Web.Models.Entities;

public class Role : IEntity<Guid>
{
    /// <summary>
    /// Keep the primary key of table
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Keep the name of role
    /// </summary>
    public string Name { get; set; } = string.Empty;
}