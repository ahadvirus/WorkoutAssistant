using System.Collections;
using System.Collections.Generic;

namespace WorkoutAssistant.Web.Infrastructures.Localizer.Models;

public class TranslateCollection : IEnumerable<KeyValuePair<string, Translate>>
{
    private IDictionary<string, Translate> Collection { get; }
    
    public TranslateCollection()
    {
        Collection = new Dictionary<string, Translate>();
    }

    public Translate? this[string key]
    {
        get
        {
            return Contains(key: key) ? Collection[key: key] : null;
        }
    }
    
    public void AddOrUpdate(string key, Translate value)
    {
        if (!Contains(key: key))
        {
            Collection.Add(key: key, value: value);
        }
        else
        {
            Collection[key: key] = value;
        }
    }

    public bool Contains(string key)
    {
        return Collection.ContainsKey(key: key);
    }
    
    public IEnumerator<KeyValuePair<string, Translate>> GetEnumerator()
    {
        return Collection.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}