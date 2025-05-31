using System.Text.RegularExpressions;

namespace Validation;

/// <summary>
/// Provides reusable, compiled regular expressions for validation.
/// </summary>
public static partial class RegexPatterns
{
    /// <summary>
    /// Matches only alphanumeric characters and underscores.
    /// </summary>
    [GeneratedRegex(@"^[a-zA-Z0-9_]+$", RegexOptions.Compiled)]
    public static partial Regex AlphanumericUnderscore();

    /// <summary>
    /// Matches a valid email address.
    /// </summary>
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled)]
    public static partial Regex Email();
}