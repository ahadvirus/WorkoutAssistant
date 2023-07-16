using System.Collections.Generic;
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
        get { throw new System.NotImplementedException(); }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get { throw new System.NotImplementedException(); }
    }
}