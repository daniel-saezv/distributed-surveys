using ApiDocumentation;

var builder = WebApplication.CreateBuilder(args);

builder.AddSwaggerGenConfig(options =>
{
    options.Title = "Analysis Worker API";
    options.Version = "v1";
    options.Description = "API for the Analysis Worker service.";
});

var app = builder.Build();

app.UseSwaggerUIConfig();

app.MapGet("/", () => "Hello World!");

app.Run();
