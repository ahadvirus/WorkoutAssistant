using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
                string language = fileInfo.Name.Replace(oldValue: fileInfo.Extension, newValue: string.Empty);
                TranslateCollection? translates = languages.Contains(language: language)
                    ? languages[language: language]
                    : languages.Add(language: language);
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
                                    
                                    /*
                                    do
                                    {
                                        jsonReader.Read();
                                        Debug.WriteLine(message: string.Format(format: " === {0} ====== {1} ===", args: new object?[] {jsonReader.Value, jsonReader.TokenType}));
                                    } while (jsonReader.TokenType != JsonToken.EndObject);
                                    */

                                    JObject jsonObject = JObject.Load(reader: jsonReader);

                                    Translate value = new Translate(key: key)
                                    {
                                        Text = (string)jsonObject[propertyName: nameof(Translate.Text)]!,
                                        Description = (string?)jsonObject[propertyName: nameof(Translate.Description)]
                                    };

                                    translates.AddOrUpdate(key: key, value: value);
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
            result = new LanguageCollection(resource: resource, address: Options.Path);
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