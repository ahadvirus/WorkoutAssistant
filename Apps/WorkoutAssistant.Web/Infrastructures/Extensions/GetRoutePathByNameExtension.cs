using WorkoutAssistant.Web.Infrastructures.Web.Routes;

namespace WorkoutAssistant.Web.Infrastructures.Extensions;

public static class GetRoutePathByNameExtension
{
    public static string GetRoutePathByName(this string name)
    {
        RouteCollection collection = RouteCollection.GetInstance(typeof(GetRoutePathByNameExtension).Assembly);

        Route? route = collection[name: name];

        return route == null ? string.Empty : route.Path;
    }
}