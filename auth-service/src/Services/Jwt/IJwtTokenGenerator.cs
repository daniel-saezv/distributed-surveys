using AuthService.Infra.Models;

namespace AuthService.Services.Jwt;

/// <summary>
/// Interface for generating JWT tokens.
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    string GenerateToken(User user);
}