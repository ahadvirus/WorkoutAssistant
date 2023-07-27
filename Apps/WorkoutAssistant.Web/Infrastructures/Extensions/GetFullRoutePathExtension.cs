using System;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace WorkoutAssistant.Web.Infrastructures.Extensions;

public static class GetFullRoutePathExtension
{
    public static string GetFullRoutePath(this HttpContext context)
    {
        string result = context.Request.Path;

        if (context.Request.Query.Any())
        {
            result = string.Format(format: "{0}?{1}", args: new object?[]
            {
                result,
                string.Join(
                    separator: "&",
                    values: context.Request.Query.Select(selector: pair =>
                        string.Format(
                            format: "{0}={1}",
                            args: new object?[] { pair.Key, HttpUtility.UrlEncode(str: pair.Value) }
                        )
                    )
                )
            });
        }

        return result;
    }
}