using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace WorkoutAssistant.Web.Infrastructures.Translators.Localizations.Models;

public class TranslateCollection : IEnumerable<KeyValuePair<string, Translate>>
{
    /// <summary>
    /// 
    /// </summary>
    private IDictionary<string, Translate> Collection { get; }

    /// <summary>
    /// 
    /// </summary>
    private string Language { get; }

    /// <summary>
    /// 
    /// </summary>
    private string Address { get; }

    private string Extension
    {
        get { return "json"; }
    }

    public TranslateCollection(string language, string address)
    {
        Language = language;
        Address = address;
        Collection = new Dictionary<string, Translate>();
    }

    public Translate? this[string key]
    {
        get { return Contains(key: key) ? Collection[key: key] : null; }
    }

    public void AddOrUpdate(string key, Translate value)
    {
        value.PropertyChanged += ValueOnPropertyChanged;

        if (!Contains(key: key))
        {
            Collection.Add(key: key, value: value);
        }
        else
        {
            Collection[key: key] = value;
        }
    }

    private void ValueOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender != null && sender is Translate translate)
        {
            if (!string.IsNullOrEmpty(e.PropertyName))
            {
                PropertyInfo? propertyInfo = sender.GetType().GetProperty(e.PropertyName);
                if (propertyInfo == null)
                {
                    return;
                }

                object? newValue = propertyInfo.GetValue(obj: sender);

                if (newValue == null)
                {
                    return;
                }

                using (FileStream fileStream = new FileStream(
                           path: Path.Combine(
                               paths: new string[]
                               {
                                   Address,
                                   string.Format(
                                       format: "{0}.{1}",
                                       args: new object?[] { Language, Extension }
                                   )
                               }
                           ),
                           access: FileAccess.ReadWrite,
                           mode: FileMode.Open,
                           share: FileShare.ReadWrite
                       ))
                {
                    ValueWriter status = ValueWriter.None;
                    //int objectCount = 0;

                    using (StreamReader streamReader = new StreamReader(stream: fileStream))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(stream: fileStream))
                        {
                            using (JsonTextReader jsonReader = new JsonTextReader(reader: streamReader))
                            {
                                using (JsonTextWriter jsonWriter = new JsonTextWriter(textWriter: streamWriter))
                                {
                                    jsonWriter.Formatting = Formatting.Indented;
                                    while (jsonReader.Read())
                                    {
                                        if (jsonReader.Value == null)
                                        {
                                            JsonToken readerToken = jsonReader.TokenType;
                                            /*
                                            if (jsonReader.TokenType == JsonToken.StartObject)
                                            {
                                                objectCount += 1;
                                            }

                                            if (jsonReader.TokenType == JsonToken.EndObject)
                                            {
                                                if (objectCount == 1)
                                                {
                                                    readerToken = JsonToken.None;
                                                }

                                                objectCount -= 1;
                                            }
                                            */
                                            jsonWriter.WriteToken(token: readerToken);
                                            continue;
                                        }

                                        if ((int)status < 3 && jsonReader.TokenType == JsonToken.PropertyName)
                                        {
                                            if (status == ValueWriter.None)
                                            {
                                                status =
                                                    ((string)jsonReader.Value).ToLower().Equals(translate.Key.ToLower())
                                                        ? ValueWriter.Parent
                                                        : ValueWriter.None;
                                            }
                                            else
                                            {
                                                status = ((string)jsonReader.Value).ToLower()
                                                    .Equals(e.PropertyName.ToLower())
                                                        ? ValueWriter.Found
                                                        : ValueWriter.Parent;
                                            }
                                        }

                                        object? token = status switch
                                        {
                                            ValueWriter.None => jsonReader.Value,
                                            ValueWriter.Parent => jsonReader.Value,
                                            ValueWriter.Found => jsonReader.Value,
                                            ValueWriter.Replace => newValue,
                                            ValueWriter.Complete => jsonReader.Value,
                                            _ => throw new ArgumentOutOfRangeException()
                                        };

                                        jsonWriter.WriteToken(token: jsonReader.TokenType, value: token);

                                        if (status == ValueWriter.Replace)
                                        {
                                            status = ValueWriter.Complete;
                                        }

                                        if (status == ValueWriter.Found)
                                        {
                                            status = ValueWriter.Replace;
                                        }
                                    }

                                    fileStream.SetLength(0);
                                    //jsonWriter.AutoCompleteOnClose = false;
                                    //streamWriter.Flush();
                                    //streamWriter.Write(jsonWriter);
                                }
                            }
                        }
                    }
                }
            }
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