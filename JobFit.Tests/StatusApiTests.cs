using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Jobfit.Tests;

public class StatusApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public StatusApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Status_Retorna200()
    {
        var resposta = await _client.GetAsync("/api/status");

        Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);
    }
}