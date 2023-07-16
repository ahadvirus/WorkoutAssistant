using System.Collections;
using System.Collections.Generic;

namespace WorkoutAssistant.Web.Infrastructures.Localizer.Models;

public class LanguageCollection : IEnumerable<KeyValuePair<string, TranslateCollection>>
{
    private IDictionary<string, TranslateCollection> Collection { get; }

    public LanguageCollection()
    {
        Collection = new Dictionary<string, TranslateCollection>();
    }

    public TranslateCollection? Add(string language)
    {
        TranslateCollection? result = null;

        if (!Contains(language: language))
        {
            result = new TranslateCollection();
            Collection.Add(key: language, value: result);
        }

        return result;
    }

    public TranslateCollection? this[string language]
    {
        get
        {
            return Contains(language: language) ? Collection[key: language] : null;
        }
    }

    public bool Contains(string language)
    {
        return Collection.ContainsKey(key: language);
    }

    public IEnumerator<KeyValuePair<string, TranslateCollection>> GetEnumerator()
    {
        return Collection.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}