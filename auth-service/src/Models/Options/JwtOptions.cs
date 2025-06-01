namespace AuthService.Models.Options;
/// <summary>
/// Options for configuring JWT authentication.
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// The issuer of the JWT tokens, typically the name of your application or service.
    /// </summary>
    public required string Issuer { get; init; }
    /// <summary>
    /// The audience for which the JWT tokens are intended, usually the client application or service that will consume the tokens.
    /// </summary>
    public required string Audience { get; init; }
    /// <summary>
    /// The secret key used to sign the JWT tokens. This should be kept secure and not exposed publicly.
    /// </summary>
    public required string Key { get; init; }
    /// <summary>
    /// The expiration time for the JWT tokens in minutes. This determines how long the token is valid before it needs to be refreshed or reissued.
    /// </summary>
    public required int ExpirationMinutes { get; init; }
    /// <summary>
    /// The name of the JWT authentication scheme.
    /// </summary>
    public static readonly string SectionName = "Jwt";
};