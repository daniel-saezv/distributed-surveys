using AuthService.Infra.Context;
using AuthService;
using AuthService.Infra;
using Microsoft.EntityFrameworkCore;
using ApiDocumentation;
using AuthService.Middlewares;
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

app.MapEndpoints()
    .Run();
