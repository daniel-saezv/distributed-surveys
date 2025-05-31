namespace AuthService.Models.Register;
/// <summary>
/// Represents the response returned after a successful user registration.
/// </summary>
/// <param name="Id">The unique identifier of the newly registered user.</param>
/// <param name="Username">The username of the newly registered user.</param>
public record RegisterResponse(int Id, string Username);