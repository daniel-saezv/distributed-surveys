# Shared ApiDocumentation Library

This library provides shared abstractions and extension methods for configuring API documentation (Swagger/OpenAPI) across all distributed services in this repository.

## Technologies

- .NET 9

## Usage

Add a reference to the `Shared.ApiDocumentation` library in your service. Use the provided extension methods to configure Swagger in your API project.

### Example

```csharp
// In your Program.cs

using ApiDocumentation;

var builder = WebApplication.CreateBuilder(args);

// Optionally configure Swagger options
builder.AddSwaggerGenConfig(options =>
{
    options.Title = "My API";
    options.Version = "v1";
    options.Description = "API documentation for My Service";
    options.EndpointUrl = "/swagger/v1/swagger.json";
    options.EndpointName = "My API V1";
});

var app = builder.Build();

app.UseSwaggerUIConfig();

app.Run();
```

- `AddSwaggerGenConfig` registers and configures Swagger/OpenAPI generation using customizable options.
- `UseSwaggerUIConfig` enables Swagger middleware and UI using the configured options.