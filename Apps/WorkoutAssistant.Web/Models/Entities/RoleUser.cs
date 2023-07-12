using System;
using WorkoutAssistant.Web.Infrastructures.Contracts;

namespace WorkoutAssistant.Web.Models.Entities;

public class RoleUser : IEntity<Guid>
{
    /// <summary>
    /// The primary key in table
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The foreign key for role table
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// The foreign key for user table
    /// </summary>
    public Guid UserId { get; set; }
}