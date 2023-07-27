using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace WorkoutAssistant.Web.Infrastructures.Translators.Globalization;

public class LanguageCollection : IEnumerable<Language>
{
    private IDictionary<string, Language> Collection { get; }

    private LanguageOption Option { get; }

    private string FileExtension
    {
        get { return ".json"; }
    }

    private string Pattern
    {
        get { return "[a-z]{2}\\-[A-Z]{2}"; }
    }

    public LanguageCollection(LanguageOption option)
    {
        Option = option;
        Collection = new Dictionary<string, Language>();

        Initialization();
    }

    private void Initialization()
    {
        string fileAddress = Path.Combine(paths: new string[]
        {
            Option.Address,
            string.Format(
                format: "{0}s{1}",
                args: new object?[]
                {
                    nameof(Language).ToLower(),
                    FileExtension
                }
            )
        });

        if (File.Exists(path: fileAddress))
        {
            using (FileStream fileStream = new FileStream(path: fileAddress, access: FileAccess.Read,
                       mode: FileMode.Open, share: FileShare.Read))
            {
                using (StreamReader streamReader = new StreamReader(stream: fileStream))
                {
                    using (JsonTextReader jsonTextReader = new JsonTextReader(reader: streamReader))
                    {
                        while (jsonTextReader.Read())
                        {
                            if (fileStream.Position == 0 && jsonTextReader.TokenType != JsonToken.StartArray)
                            {
                                break;
                            }

                            do
                            {
                                jsonTextReader.Read();
                                if (jsonTextReader.Value != null && jsonTextReader.ValueType != null &&
                                    jsonTextReader.ValueType == typeof(string))
                                {
                                    Add(language: (string)jsonTextReader.Value);
                                }
                            } while (jsonTextReader.TokenType != JsonToken.EndArray);
                        }
                    }
                }
            }
        }
    }

    public Language? this[string language]
    {
        get
        {
            return ValidLanguageName(name: language)
                ? (Collection.TryGetValue(language, out Language? value) ? value : null)
                : null;
        }
    }

    public void Add(string language)
    {
        if (ValidLanguageName(name: language) && Collection.ContainsKey(key: language) == false)
        {
            CultureInfo? culture = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Where(predicate: culture => ValidLanguageName(name: culture.Name))
                .Where(predicate: culture =>
                    Collection.Select(selector: pair => pair.Key).Contains(value: culture.Name) == false)
                .FirstOrDefault(culture => culture.Name == language);

            if (culture != null)
            {
                Collection.Add(key: language, value: new Language(info: culture));
            }
        }
    }

    private bool ValidLanguageName(string name)
    {
        return Regex.IsMatch(input: name, pattern: Pattern, options: RegexOptions.IgnoreCase);
    }

    public IEnumerator<Language> GetEnumerator()
    {
        return Collection.Select(pair => pair.Value).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}