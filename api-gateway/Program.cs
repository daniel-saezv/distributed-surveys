using ApiDocumentation;
var builder = WebApplication.CreateBuilder(args);
builder.AddSwaggerGenConfig(options =>
{
    options.Title = "API Gateway";
    options.Version = "v1";
    options.Description = "API for the API Gateway service.";
});
var app = builder.Build();
app.UseSwaggerUIConfig();

app.MapGet("/", () => "Hello World!");

app.Run();
