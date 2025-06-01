using AuthService.Infra.Models;
using AuthService.Services.Jwt;
using Microsoft.AspNetCore.Identity;

namespace AuthService;

/// <summary>
/// Extension methods for configuring services in the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the necessary services for the AuthService to the service collection.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>()
                       .AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
    }
}