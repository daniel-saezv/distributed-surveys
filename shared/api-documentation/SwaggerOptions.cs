namespace ApiDocumentation;
/// <summary>
/// Represents configuration options for Swagger API documentation.
/// </summary>
public class SwaggerOptions
{
    /// <summary>
    /// Gets or sets the title of the API documentation.
    /// </summary>
    public string Title { get; set; } = "API";

    /// <summary>
    /// Gets or sets the version of the API.
    /// </summary>
    public string Version { get; set; } = "v1";

    /// <summary>
    /// Gets or sets the description of the API.
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    /// Gets or sets the URL of the Swagger endpoint.
    /// </summary>
    public string EndpointUrl { get; set; } = "/swagger/v1/swagger.json";

    /// <summary>
    /// Gets or sets the display name of the Swagger endpoint.
    /// </summary>
    public string EndpointName { get; set; } = "API V1";
}
