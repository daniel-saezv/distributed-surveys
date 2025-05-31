using ApiDocumentation;
var builder = WebApplication.CreateBuilder(args);
builder.AddSwaggerGenConfig(options =>
{
    options.Title = "Auth Service API";
    options.Version = "v1";
    options.Description = "API for the Auth Service.";
});
var app = builder.Build();
app.UseSwaggerUIConfig();

app.MapGet("/", () => "Hello World!");

app.Run();
