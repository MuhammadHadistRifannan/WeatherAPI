using WeatherAPI.API;
using WeatherAPI.Services;
using WeatherAPI.Caching;
using Microsoft.AspNetCore.RateLimiting;
using System.Reflection.Metadata.Ecma335;

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

//Adding Rate Limiter 
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.PermitLimit = 5;
        opt.QueueLimit = 0;
        opt.Window = TimeSpan.FromSeconds(10);
    });
    options.OnRejected = async (context, cancellationToken) =>
    {
        
        // Mengatur tipe konten dan status (status 429 sudah diatur di atas)
        context.HttpContext.Response.ContentType = "application/json";
        
        // Membuat pesan JSON kustom
        var message = new 
        {
            Status = 429,
            Title = "Too Many Requests",
            Detail = $"Anda telah melebihi batas permintaan. Coba lagi nanti"
        };

        // Menulis respons
        await context.HttpContext.Response.WriteAsJsonAsync(message, cancellationToken);
    };

});

var app = builder.Build();

app.MapControllers();
app.UseRateLimiter();
app.Run();