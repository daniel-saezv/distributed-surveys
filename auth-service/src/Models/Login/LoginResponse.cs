namespace AuthService.Models.Login;

/// <summary>
/// Represents a response containing the authentication token and its expiration details.
/// </summary>
/// <param name="Token"></param>
/// <param name="TokenType"></param>
public record LoginResponse(
    string Token,
    string TokenType = "Bearer");