namespace BrewStation.Core.Time;

public class SystemClock : IClock
{
    public DateTimeOffset UtcNow => DateTimeOffset.Now;
}
