using BrewStation.Core.Services;
using BrewStation.Core.Time;

namespace BrewStation.Tests;

public class BrewServiceTests
{
    [Fact]
    public void Returns_Teapot_On_April_First()
    {
        var clock = new FakeClock(new DateTimeOffset(2024, 4, 1, 10, 0, 0, TimeSpan.Zero));
        var service = new BrewService(clock);

        var result = service.Brew();

        Assert.Equal(BrewStatus.Teapot, result.Status);
    }

    [Fact]
    public void Every_Fifth_Call_Is_Out_Of_Coffee()
    {
        var clock = new FakeClock(DateTimeOffset.Now);
        var service = new BrewService(clock);

        for (int i = 1; i <= 4; i++)
        {
            Assert.Equal(BrewStatus.Ok, service.Brew().Status);
        }

        Assert.Equal(BrewStatus.OutOfCoffee, service.Brew().Status);
    }

    [Fact]
    public void Successful_Brew_Returns_Response()
    {
        var clock = new FakeClock(DateTimeOffset.Now);
        var service = new BrewService(clock);

        var result = service.Brew();

        Assert.Equal(BrewStatus.Ok, result.Status);
        Assert.NotNull(result.Response);
        Assert.Equal("Your piping hot coffee is ready", result.Response!.Message);
    }
}

public class FakeClock : IClock
{
    public FakeClock(DateTimeOffset now)
    {
        UtcNow = now;
    }

    public DateTimeOffset UtcNow { get; }
}
