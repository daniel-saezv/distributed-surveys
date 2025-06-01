using AuthService.Infra.Context;
using AuthService;
using AuthService.Infra;
using Microsoft.EntityFrameworkCore;
using ApiDocumentation;
using AuthService.Middlewares;
using AuthService.Models.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddExceptionHandler<DatabaseExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddOptions<JwtOptions>()
    .BindConfiguration(JwtOptions.SectionName)
    .ValidateDataAnnotations()
    .ValidateOnStart();
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

if (!app.Environment.IsEnvironment("Testing"))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    await db.Database.MigrateAsync();
}

app.MapEndpoints()
    .Run();
    
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public partial class Program { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
