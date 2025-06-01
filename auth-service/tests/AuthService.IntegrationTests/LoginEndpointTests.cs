using System.Net;
using System.Net.Http.Json;
using AuthService.Models.Login;

namespace AuthService.IntegrationTests;

public class LoginEndpointTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenUserDoesNotExist()
    {
        var loginRequest = new LoginRequest("nouser", "nopass");
        var response = await _client.PostAsJsonAsync("/login", loginRequest);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenPasswordIsIncorrect()
    {
        var loginRequest = new LoginRequest("test", "wrongpass");
        var response = await _client.PostAsJsonAsync("/login", loginRequest);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Login_ReturnsOk_WhenCredentialsAreCorrect()
    {
        var loginRequest = new LoginRequest("test", "test");
        var response = await _client.PostAsJsonAsync("/login", loginRequest);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(loginResponse);
        Assert.NotEmpty(loginResponse.Token);
        Assert.NotEmpty(loginResponse.TokenType);
    }
}
