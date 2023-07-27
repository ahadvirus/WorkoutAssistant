using System;
using Microsoft.Extensions.Localization;
using WorkoutAssistant.Web.Infrastructures.Translators.Localizations.Models;

namespace WorkoutAssistant.Web.Infrastructures.Translators.Localizations;

public class JsonLocalizerFactory : IStringLocalizerFactory
{
    private JsonLocalizationOption Options { get; }
    private ResourceCollection Collection { get; }

    public JsonLocalizerFactory(JsonLocalizationOption options, ResourceCollection collection)
    {
        Options = options;
        Collection = collection;
    }

    public IStringLocalizer Create(Type resourceSource)
    {
        return Create(baseName:
            string.IsNullOrEmpty(value: resourceSource.FullName)
                ? string.Empty
                : resourceSource.FullName.Replace(
                    oldValue: resourceSource.Module.Name.Replace(
                        oldValue: System.IO.Path.GetExtension(path: resourceSource.Module.FullyQualifiedName),
                        newValue: string.Empty
                    ),
                    newValue: string.Empty
                )
        );
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return Create(baseName: baseName.Replace(oldValue: location, newValue: string.Empty));
    }

    private IStringLocalizer Create(string baseName)
    {
        baseName = baseName.StartsWith(value: Type.Delimiter) ? baseName.Substring(startIndex: 1) : baseName;

        LanguageCollection? collection = Collection.Contains(baseName)
            ? Collection[baseName]
            : Collection.Add(resource: baseName);

        baseName = baseName.Replace(
            oldChar: Type.Delimiter,
            newChar: System.IO.Path.DirectorySeparatorChar
        );

        return new JsonLocalizer(
            address: System.IO.Path.Combine(
                paths: new string[]
                {
                    Options.Path,
                    baseName
                }
            ),
            collection: collection ?? throw new Exception(message: string.Empty)
        );
    }
}