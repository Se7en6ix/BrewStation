using BrewStation.Core.Time;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;

namespace BrewStation.IntegrationTests;

public class BrewServiceEndpointTests
{
    [Fact]
    public async Task GET_BrewCoffee_Returns_200_With_Response()
    {
        var factory = CreateFactory(new FakeClock(
            new DateTimeOffset(2024, 2, 3, 10, 0, 0, TimeSpan.Zero)));

        var client = factory.CreateClient();

        var response = await client.GetAsync("/brew-coffee");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();
        Assert.Contains("Your piping hot coffee is ready", body);
        Assert.Contains("2024-02-03T10:00:00", body);
    }

    [Fact]
    public async Task Every_Fifth_Request_Returns_503()
    {
        var factory = CreateFactory(new FakeClock(DateTimeOffset.Now));
        var client = factory.CreateClient();

        for (int i = 1; i <= 4; i++)
        {
            var ok = await client.GetAsync("/brew-coffee");
            Assert.Equal(HttpStatusCode.OK, ok.StatusCode);
        }

        var fifth = await client.GetAsync("/brew-coffee");
        Assert.Equal(HttpStatusCode.ServiceUnavailable, fifth.StatusCode);
        Assert.Equal(string.Empty, await fifth.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task April_First_Returns_418()
    {
        var factory = CreateFactory(new FakeClock(
            new DateTimeOffset(2024, 4, 1, 9, 0, 0, TimeSpan.Zero)));

        var client = factory.CreateClient();

        var response = await client.GetAsync("/brew-coffee");

        Assert.Equal((HttpStatusCode)418, response.StatusCode);
    }

    private static WebApplicationFactory<Program> CreateFactory(IClock clock)
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Replace the existing IClock registration with our fake clock
                    services.Replace(ServiceDescriptor.Singleton(clock));
                });
            });
    }

}