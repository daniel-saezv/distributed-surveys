using AuthService.Infra.Context;
using AuthService.Infra.Models;
using AuthService.Mappers;
using AuthService.Models.Login;
using AuthService.Models.Register;
using AuthService.Services.Jwt;
using AuthService.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService;

/// <summary>
/// Utility class for mapping endpoints in the Auth Service.
/// </summary>
public static class EndpointDefinition
{
    /// <summary>
    /// Maps the endpoints for user registration and login in the Auth Service.
    /// </summary>
    /// <param name="app"></param>
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapPost("/register", async Task<Results<Created<RegisterResponse>, ProblemHttpResult>> (
            RegisterRequest req,
            AuthDbContext db,
            IPasswordHasher<User> hasher) =>
        {
            var exists = await db.Users.AnyAsync(u => u.Username == req.Username);
            if (exists)
                return ProblemResults.AlreadyRegistered();

            var createdUser = await db.Users.AddAsync(req.ToUser(hasher));
            var response = createdUser.Entity.ToResponse();

            await db.SaveChangesAsync();

            return TypedResults.Created($"/users/{response.Id}", response);
        });

        app.MapPost("/login", async Task<Results<Ok<LoginResponse>, UnauthorizedHttpResult>> (
            LoginRequest req,
            AuthDbContext db,
            IPasswordHasher<User> hasher,
            IJwtTokenGenerator jwtGen) =>
        {
            var user = await db.Users.SingleOrDefaultAsync(u => u.Username == req.Username);
            if (user is null)
                return TypedResults.Unauthorized();

            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, req.Password);
            if (result is not PasswordVerificationResult.Success)
                return TypedResults.Unauthorized();

            var token = jwtGen.GenerateToken(user);
            return TypedResults.Ok(new LoginResponse(token));
        });

        return app;
    }
}