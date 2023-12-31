﻿namespace WorkoutAssistant.Web.Models.Configurations.Names;

/// <summary>
/// Define all static names shared between views
/// </summary>
public record View
{
    /// <summary>
    /// Represent all title in views
    /// </summary>
    public string Title
    {
        get
        {
            return nameof(Title);
        }
    }
}