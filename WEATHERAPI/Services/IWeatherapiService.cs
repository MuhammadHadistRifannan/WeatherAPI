using Microsoft.AspNetCore.SignalR;

namespace WeatherAPI.Services;

public interface IWeatherapiService
{
    public Task<string> GetWeatherAsync(string city);
}
