using System.Globalization;

namespace WorkoutAssistant.Web.Infrastructures.Translators.Globalization;

public record Language
{
    public Language(CultureInfo info)
    {
        Name = info.NativeName;
        Code = info.Name;
        IsRightToLeft = info.TextInfo.IsRightToLeft;
        CultureInfo = info;
    }
    
    public string Name { get; init; }
    public string Code { get; init; }
    public bool IsRightToLeft { get; init; }
    public CultureInfo CultureInfo { get; init; }
}