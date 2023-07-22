using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Localization;
using WorkoutAssistant.Web.Infrastructures.Localizer.Models;

namespace WorkoutAssistant.Web.Infrastructures.Localizer;

public class JsonLocalizer : IStringLocalizer
{
    private string Address { get; }

    private LanguageCollection Collection { get; }

    public JsonLocalizer(string address, LanguageCollection collection)
    {
        Address = address;
        Collection = collection;
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        /*
        using (FileStream fileStream = new FileStream(path: Address, mode: FileMode.Open, access: FileAccess.Read,
                   share: FileShare.Read))
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
                            yield return new LocalizedString(name: key, value: value.Text, resourceNotFound: false);
                        }
                    }
                }
            }
        }
        */

        throw new System.NotImplementedException(message: string.Format(format: "Create in {0}",
            args: new object?[] { nameof(JsonLocalizer) }));
    }

    public LocalizedString this[string name]
    {
        get
        {
            string value = GetValue(key: name);

            return new LocalizedString(
                name: name,
                value: string.IsNullOrEmpty(value) ? name : value,
                resourceNotFound: string.IsNullOrEmpty(value)
            );
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            LocalizedString value = this[name: name];

            return value.ResourceNotFound
                ? value
                : new LocalizedString(
                    name: value.Name,
                    value: string.Format(format: value.Value, args: arguments),
                    resourceNotFound: value.ResourceNotFound
                );
        }
    }

    private string GetValue(string key)
    {
        string result = string.Empty;

        TranslateCollection? translates = Collection[language: GetCurrentCulture()];

        if (translates != null)
        {
            Translate? translate = translates[key: key];

            if (translate != null)
            {
                result = translate.Text;
            }
        }

        return result;
    }

    private string GetCurrentCulture()
    {
        return Thread.CurrentThread.CurrentCulture.Name;
    }
}