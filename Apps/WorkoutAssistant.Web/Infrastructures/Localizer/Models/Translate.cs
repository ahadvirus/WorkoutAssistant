using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkoutAssistant.Web.Infrastructures.Localizer.Models;

public class Translate : INotifyPropertyChanged
{
    public Translate(string key)
    {
        _text = string.Empty;
        _description = string.Empty;
        Key = key;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Key { get; }

    private string _text;

    /// <summary>
    /// 
    /// </summary>
    public string Text
    {
        get { return _text; }
        set { SetField(field: ref _text, value: value, propertyName: nameof(Text)); }
    }

    private string? _description;

    /// <summary>
    /// 
    /// </summary>
    public string? Description
    {
        get { return _description; }
        set { SetField(field: ref _description, value: value, propertyName: nameof(Description)); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(sender: this, e: new PropertyChangedEventArgs(propertyName));
    }

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(x: field, y: value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }
}