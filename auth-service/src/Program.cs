using AuthService.Infra.Context;
using AuthService.Infra.Models;
using AuthService.Mappers;
using AuthService.Models.Register;
using Microsoft.AspNetCore.Identity;
using AuthService;
using AuthService.Infra;
using Microsoft.EntityFrameworkCore;
using ApiDocumentation;

var builder = WebApplication.CreateBuilder(args);
builder.AddSwaggerGenConfig(options =>
{
    options.Title = "Auth Service API";
    options.Version = "v1";
    options.Description = "API for the Auth Service.";
});

builder.Services.AddServices()
                .AddInfraServices(builder.Configuration);

var app = builder.Build();
app.UseSwaggerUIConfig();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
await context.Database.MigrateAsync();

#region Endpoint Definition

app.MapPost("/register", async (RegisterRequest req, AuthDbContext db, IPasswordHasher<User> hasher) =>
{
    var exists = await db.Users.AnyAsync(u => u.Username == req.Username);
    if (exists)
    {
        return Results.Problem(
            detail: "Username already exists.",
            statusCode: StatusCodes.Status409Conflict,
            title: "Conflict"
        );
    }

    var createdUser = await db.Users.AddAsync(req.ToUser(hasher));
    var response = createdUser.Entity.ToResponse();

    await db.SaveChangesAsync();

    return Results.Created($"/users/{response.Id}", response);
});
#endregion

app.Run();
