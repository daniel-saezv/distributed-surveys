namespace AuthService.Models.Register;
/// <summary>
/// Represents a request to register a new user.
/// </summary>
/// <param name="Username">The username of the user to register.</param>
/// <param name="Password">The password of the user to register.</param>
public record RegisterRequest(string Username, string Password);