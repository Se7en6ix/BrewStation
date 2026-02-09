using BrewStation.Core.Services;

namespace BrewStation.Api.Endpoints;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/brew-coffee", (IBrewService brewService) =>
        {
            var result = brewService.Brew();

            return result.Status switch
            {
                BrewStatus.Ok => Results.Ok(result.Response),
                BrewStatus.OutOfCoffee => Results.StatusCode(503),
                BrewStatus.Teapot => Results.StatusCode(418),
                _ => Results.StatusCode(500)
            };
        });
    }
}