using BrewStation.Core.Models;
using BrewStation.Core.Time;
using System.Globalization;

namespace BrewStation.Core.Services;

public class BrewService : IBrewService
{
    private int _brewCount = 0;
    private readonly IClock _clock;

    public BrewService(IClock clock)
    {
        _clock = clock;
    }

    public BrewResult Brew()
    {
        var now = _clock.UtcNow;

        // April 1st → Teapot
        if (now.Month == 4 && now.Day == 1)    
        {
            return new BrewResult(BrewStatus.Teapot, null);
        }

        var count = Interlocked.Increment(ref _brewCount);

        // Every 5th call → Out of coffee
        if (count % 5 == 0)
        {
            return new BrewResult(BrewStatus.OutOfCoffee, null);
        }

        return new BrewResult(
            BrewStatus.Ok,
            new BrewCoffeeResponse
            {
                Message = "Your piping hot coffee is ready",
                Prepared = now.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture)
            });
    }
}

