using BrewStation.Core.Time;

namespace BrewStation.IntegrationTests;

public class FakeClock : IClock
{
    public FakeClock(DateTimeOffset now)
    {
        UtcNow = now;
    }

    public DateTimeOffset UtcNow { get; }
}
