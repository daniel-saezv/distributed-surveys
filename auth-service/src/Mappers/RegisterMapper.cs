using AuthService.Infra.Models;
using AuthService.Models.Register;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Mappers;

/// <summary>
/// Provides extension methods for mapping <see cref="User"/> entities to response models.
/// </summary>
public static class RegisterMapper
{
    /// <summary>
    /// Maps a <see cref="RegisterRequest"/> to a <see cref="User"/> entity.
    /// </summary>
    /// <param name="req"></param>
    /// <param name="hasher"></param>
    /// <returns></returns>
    public static User ToUser(this RegisterRequest req, IPasswordHasher<User> hasher)
    {
        var user = new User { Username = req.Username };
        user.PasswordHash = hasher.HashPassword(user, req.Password);
        return user;
    }

    /// <summary>
    /// Maps a <see cref="User"/> entity to a <see cref="RegisterResponse"/> object.
    /// </summary>
    /// <param name="user">The user entity to map.</param>
    /// <returns>A <see cref="RegisterResponse"/> containing the user's ID and username.</returns>
    public static RegisterResponse ToResponse(this User user) => new(user.Id, user.Username);
}