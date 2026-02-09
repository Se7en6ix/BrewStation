using BrewStation.Core.Time;

namespace BrewStation.Tests;

public class FakeClock : IClock
{
    public FakeClock(DateTimeOffset now)
    {
        UtcNow = now;
    }

    public DateTimeOffset UtcNow { get; }
}
