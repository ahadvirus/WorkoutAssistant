using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WorkoutAssistant.Web.Infrastructures.Web.Routes;

public class RouteCollection : IEnumerable<Route>
{
    /// <summary>
    /// the collection to keep all routes exists in system
    /// </summary>
    private IDictionary<string, Route> Collection { get; }

    /// <summary>
    /// the assembly will be search for routes
    /// </summary>
    private Assembly Assembly { get; }

    private RouteCollection(Assembly assembly)
    {
        Assembly = assembly;
        Collection = new Dictionary<string, Route>();
        Accumulate();
    }

    /// <summary>
    /// return the route by passing route name
    /// </summary>
    /// <param name="name"><see cref="string"/> the name ask for route information</param>
    public Route? this[string name]
    {
        get { return Collection.TryGetValue(key: name, value: out Route? value) ? value : null; }
    }

    /// <summary>
    /// function to search assembly and grab all routes exists in the assembly
    /// </summary>
    public void Accumulate()
    {
        Collection.Clear();

        IEnumerable<Type> types = Assembly.GetTypes()
            .Where(predicate: type => type.IsClass && type.GetCustomAttributes()
                .Any(predicate: attribute => attribute.GetType() == typeof(RouteAttribute)))
            .OrderBy(keySelector: type => type.GetCustomAttributes()
                .Where(attribute => attribute.GetType() == typeof(RouteAttribute))
                .Select(selector: attribute => ((RouteAttribute)attribute).Version)
                .First()
            );
        foreach (Type type in types)
        {
            ConstructorInfo? constructorInfo = type.GetConstructors().FirstOrDefault();

            if (constructorInfo == null || constructorInfo.GetParameters().Length != 0)
            {
                continue;
            }

            object entry = constructorInfo.Invoke(parameters: null);

            foreach (Route route in Grab(entry: entry))
            {
                if (Collection.ContainsKey(key: route.Name))
                {
                    Collection.Add(key: route.Name, value: route);
                }
                else
                {
                    Collection[key: route.Name] = route;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    private IEnumerable<Route> Grab(object entry)
    {
        List<Route> routes = new List<Route>();

        foreach (PropertyInfo property in entry.GetType().GetProperties())
        {
            object? result = property.GetValue(obj: entry);
            if (result != null)
            {
                if (result is Route route)
                {
                    routes.Add(item: route);
                }
                else
                {
                    routes.AddRange(collection: Grab(entry: result));
                }
            }
        }

        return routes;
    }

    public IEnumerator<Route> GetEnumerator()
    {
        return Collection.Select(pair => pair.Value).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private static RouteCollection? _instance;

    public static RouteCollection GetInstance(Assembly? assembly = null)
    {
        if (_instance == null && assembly == null)
        {
            throw new Exception(message: string.Empty);
        }

        if (_instance == null && assembly != null)
        {
            _instance = new RouteCollection(assembly: assembly);
        }

        return _instance ?? throw new Exception(message: string.Empty);
    }
}