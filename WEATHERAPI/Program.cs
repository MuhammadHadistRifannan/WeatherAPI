using WeatherAPI.API;
using WeatherAPI.Services;
using WeatherAPI.Caching;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDistributedRedisCache(options =>
{
    options.InstanceName = "weather-redis";
    options.Configuration = builder.Configuration.GetSection("redis").Value;
});

builder.Services.AddScoped<IWeatherapiService, Weatherapi>();
builder.Services.AddScoped<IRedisService, Redisx>();


var app = builder.Build();


app.MapControllers();
app.Run();