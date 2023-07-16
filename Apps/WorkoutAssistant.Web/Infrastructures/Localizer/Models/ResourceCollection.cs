using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace WorkoutAssistant.Web.Infrastructures.Localizer.Models;

public class ResourceCollection : IEnumerable<KeyValuePair<string, LanguageCollection>>
{
    private IDictionary<string, LanguageCollection> Collection { get; }

    private JsonLocalizationOption Options { get; }

    private string SearchPattern
    {
        get { return "*.json"; }
    }

    private SearchOption SearchOption
    {
        get { return SearchOption.AllDirectories; }
    }

    public ResourceCollection(JsonLocalizationOption options)
    {
        Options = options;
        Collection = new Dictionary<string, LanguageCollection>();

        Initialization();
    }

    private void Initialization()
    {
        IEnumerable<string> files = Directory.EnumerateFiles(
            path: Options.Path,
            searchPattern: SearchPattern,
            searchOption: SearchOption
        );

        foreach (string file in files)
        {
            FileInfo fileInfo = new FileInfo(fileName: file);

            string resource = !string.IsNullOrEmpty(value: fileInfo.DirectoryName)
                ? fileInfo.DirectoryName.Replace(
                    oldValue: Options.Path,
                    newValue: string.Empty
                )
                : file.Replace(
                        oldValue: string.Format(format: "{0}{1}",
                            args: new object?[] { fileInfo.Name, fileInfo.Extension }),
                        newValue: string.Empty
                    )
                    .Replace(
                        oldValue: Options.Path,
                        newValue: string.Empty
                    );

            resource = resource.StartsWith(value: Path.DirectorySeparatorChar)
                ? resource.Substring(startIndex: 1)
                : resource;

            resource = resource.Replace(
                oldChar: Path.DirectorySeparatorChar,
                newChar: Type.Delimiter
            );


            LanguageCollection? languages =
                Contains(resource: resource) ? this[resource: resource] : Add(resource: resource);

            if (languages != null)
            {
                TranslateCollection? translates = languages.Contains(language: fileInfo.Name)
                    ? languages[language: fileInfo.Name]
                    : languages.Add(language: fileInfo.Name);
                if (translates != null)
                {
                    using (FileStream fileStream = new FileStream(path: file, access: FileAccess.Read,
                               mode: FileMode.Open, share: FileShare.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(stream: fileStream))
                        {
                            using (JsonTextReader jsonReader = new JsonTextReader(reader: streamReader))
                            {
                                int count = 1;
                                while (jsonReader.Read())
                                {
                                    if (jsonReader.TokenType != JsonToken.PropertyName)
                                    {
                                        continue;
                                    }

                                    count += 1;

                                    string key = jsonReader.Value != null
                                        ? (string)jsonReader.Value
                                        : string.Format(format: "undefined-{0}", args: new object?[] { count });

                                    jsonReader.Read();

                                    Translate? value = JsonSerializer.Create().Deserialize<Translate>(jsonReader);

                                    if (value != null)
                                    {
                                        translates.AddOrUpdate(key: key, value: value);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public LanguageCollection? this[string resource]
    {
        get { return Contains(resource: resource) ? Collection[key: resource] : null; }
    }

    public LanguageCollection? Add(string resource)
    {
        LanguageCollection? result = null;

        if (!Contains(resource: resource))
        {
            result = new LanguageCollection();
            Collection.Add(key: resource, value: result);
        }

        return result;
    }

    public bool Contains(string resource)
    {
        return Collection.ContainsKey(key: resource);
    }

    public IEnumerator<KeyValuePair<string, LanguageCollection>> GetEnumerator()
    {
        return Collection.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}