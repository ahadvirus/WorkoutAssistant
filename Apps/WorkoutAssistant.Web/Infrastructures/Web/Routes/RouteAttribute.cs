using System;

namespace WorkoutAssistant.Web.Infrastructures.Web.Routes;

/// <summary>
/// 
/// </summary>
public class RouteAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public long Version { get; }

    public RouteAttribute(long version)
    {
        Version = version;
    }
}