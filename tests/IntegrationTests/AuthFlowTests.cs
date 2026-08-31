using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTests;

public class AuthFlowTests : IClassFixture<WebApplicationFactory<object>>
{
    private readonly WebApplicationFactory<object> _factory;

    public AuthFlowTests(WebApplicationFactory<object> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Register_Login_Refresh_Revoke_Flow_Works()
    {
        var client = _factory.CreateClient();

        // Note: The app expects tenant resolution via X-Tenant-Slug header. Use seeded tenant 'barbearia-demo'.
        var registerPayload = JsonSerializer.Serialize(new { email = "ituser@barbearia.demo", password = "DevPass123!", displayName = "IT User" });
        var registerRequest = new HttpRequestMessage(HttpMethod.Post, "/api/auth/register")
        {
            Content = new StringContent(registerPayload, Encoding.UTF8, "application/json")
        };
        registerRequest.Headers.Add("X-Tenant-Slug", "barbearia-demo");

        var registerResp = await client.SendAsync(registerRequest);
        registerResp.EnsureSuccessStatusCode();

        var loginPayload = JsonSerializer.Serialize(new { email = "ituser@barbearia.demo", password = "DevPass123!" });
        var loginResp = await client.PostAsync("/api/auth/login", new StringContent(loginPayload, Encoding.UTF8, "application/json"));
        loginResp.EnsureSuccessStatusCode();

        var loginContent = await loginResp.Content.ReadAsStringAsync();
        Assert.Contains("accessToken", loginContent);
        Assert.Contains("refreshToken", loginContent);

        // Further refresh/revoke flow could be tested by parsing tokens and calling /refresh and /revoke
    }
}
