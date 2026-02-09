using BrewStation.Api.Extensions;
using BrewStation.Core.Services;
using BrewStation.Core.Time;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IClock, SystemClock>();
builder.Services.AddSingleton<IBrewService, BrewService>();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.MapOpenApi();
app.MapEndpoints();
app.UseScalarUi();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
