using System;
using System.Collections.Generic;

namespace WorkoutAssistant.Web.Areas.Auth.Models.DataTransfers;

public record UserLoginInfoDto
{
    public Guid Guid { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public IEnumerable<string> Role { get; set; } = new string[] { };
}