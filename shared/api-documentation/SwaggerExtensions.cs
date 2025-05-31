using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ApiDocumentation;

/// <summary>
/// Provides extension methods for configuring Swagger/OpenAPI documentation in ASP.NET Core applications.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Adds and configures Swagger/OpenAPI generation services to the application's service collection.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> to add the Swagger services to.</param>
    /// <param name="configure">An optional action to configure <see cref="SwaggerOptions"/>.</param>
    /// <returns>The <see cref="WebApplicationBuilder"/> instance for chaining.</returns>
    public static WebApplicationBuilder AddSwaggerGenConfig(this WebApplicationBuilder builder, Action<SwaggerOptions>? configure = null)
    {
        var options = new SwaggerOptions();
        configure?.Invoke(options);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(genOptions =>
        {
            genOptions.SwaggerDoc(options.Version, new()
            {
                Title = options.Title,
                Version = options.Version,
                Description = options.Description
            });
        });

        builder.Services.AddSingleton(options);

        return builder;
    }

    /// <summary>
    /// Configures the application to use Swagger and Swagger UI middleware with the specified options.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> to configure.</param>
    /// <returns>The <see cref="WebApplication"/> instance for chaining.</returns>
    public static WebApplication UseSwaggerUIConfig(this WebApplication app)
    {
        var options = app.Services.GetRequiredService<SwaggerOptions>();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint(options.EndpointUrl, options.EndpointName);
        });

        return app;
    }
}
