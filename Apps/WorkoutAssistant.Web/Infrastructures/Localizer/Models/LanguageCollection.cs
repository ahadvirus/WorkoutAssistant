using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace WorkoutAssistant.Web.Infrastructures.Localizer.Models;

public class LanguageCollection : IEnumerable<KeyValuePair<string, TranslateCollection>>
{
    /// <summary>
    /// 
    /// </summary>
    private IDictionary<string, TranslateCollection> Collection { get; }

    /// <summary>
    /// 
    /// </summary>
    private string Resource { get; }

    /// <summary>
    /// 
    /// </summary>
    private string Address { get; }

    public LanguageCollection(string resource, string address)
    {
        Resource = resource;
        Address = address;
        Collection = new Dictionary<string, TranslateCollection>();
    }

    public TranslateCollection? Add(string language)
    {
        TranslateCollection? result = null;

        if (!Contains(language: language))
        {
            result = new TranslateCollection(
                language: language,
                address: Path.Combine(paths: new string[]
                    { Address, Resource.Replace(oldChar: Type.Delimiter, newChar: Path.DirectorySeparatorChar) })
            );
            Collection.Add(key: language, value: result);
        }

        return result;
    }

    public TranslateCollection? this[string language]
    {
        get { return Contains(language: language) ? Collection[key: language] : null; }
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