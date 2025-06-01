using AuthService.Infra.Context;
using AuthService.Infra.Models;
using AuthService.Mappers;
using AuthService.Models.Register;
using Microsoft.AspNetCore.Identity;
using AuthService;
using AuthService.Infra;
using Microsoft.EntityFrameworkCore;
using ApiDocumentation;
using AuthService.Middlewares;
using AuthService.Models.Login;
using AuthService.Services.Jwt;
using Microsoft.AspNetCore.Http.HttpResults;
using AuthService.Utils;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddExceptionHandler<DatabaseExceptionHandler>();
builder.Services.AddProblemDetails();
builder.AddSwaggerGenConfig(options =>
{
    options.Title = "Auth Service API";
    options.Version = "v1";
    options.Description = "API for the Auth Service.";
});

builder.Services.AddServices()
                .AddInfraServices(builder.Configuration);

var app = builder.Build();
app.UseExceptionHandler();
app.UseSwaggerUIConfig();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
await context.Database.MigrateAsync();

#region Endpoint Definition

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
#endregion

app.Run();
