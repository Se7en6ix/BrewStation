using BrewStation.Core.Models;

namespace BrewStation.Core.Services;

public record BrewResult(
    BrewStatus Status,
    BrewCoffeeResponse? Response);
