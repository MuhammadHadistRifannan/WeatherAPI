using WeatherAPI.Services;
using WeatherAPI.Helper;
using System.Text.Json;
namespace WeatherAPI.API;

public class Weatherapi : IWeatherapiService
{
    readonly IConfiguration configuration;
    public Weatherapi(IConfiguration _configuration)
    {
        configuration = _configuration;
    }

    public async Task<string> GetWeatherAsync(string city)
    {
        var API = configuration.GetSection("API_KEY").Value;
        using (var client = new HttpClient())
        {
            try
            {
                var request =  await client.GetAsync($"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{city}?key={API}");
                var res =  await request.Content.ReadAsStringAsync();
                return res;
            }catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
