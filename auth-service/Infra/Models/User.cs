namespace AuthService.Infra.Models;
/// <summary>
/// Represents a user entity in the authentication service.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string Username { get; set; } = default!;

    /// <summary>
    /// Gets or sets the hashed password of the user.
    /// </summary>
    public string PasswordHash { get; set; } = default!;
}