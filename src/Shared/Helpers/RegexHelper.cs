
using System.Text.RegularExpressions;

namespace Shared.Helpers;

public static partial class RegexHelper
{
    [GeneratedRegex(@"[^a-z0-9]+")]
    private static partial Regex NormalizedRegex();

    public static string Normalize(string text)
    {
        text = text.Trim();
        text = text.ToLowerInvariant();
        text = NormalizedRegex().Replace(text, " ");
        return text;
    }
}
