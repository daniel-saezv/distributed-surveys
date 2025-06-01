namespace AuthService.Models.Login;
/// <summary>
/// Represents a request to log in a user.
/// </summary>
/// <param name="Username"></param>
/// <param name="Password"></param>
public record LoginRequest(string Username, string Password);