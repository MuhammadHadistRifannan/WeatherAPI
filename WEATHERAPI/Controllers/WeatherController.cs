using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Caching;
using WeatherAPI.API;
using WeatherAPI.Helper;
using WeatherAPI.Services;
using WeatherAPI.StandardHttp;
using System.Net.Sockets;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyApp.Namespace
{
    [Route("api/")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        readonly IWeatherapiService weatherapiService;
        readonly IRedisService redis;
        public WeatherController(IWeatherapiService _weatherservices , IRedisService _redisService)
        {
            weatherapiService = _weatherservices;
            redis = _redisService;
        }

        [HttpGet("weather/{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            if (redis.GetValue(city) == null)
            {
                try
                {
                    //return the object response 
                    var apiresponse = await weatherapiService.GetWeatherAsync(city);
                    //nunggu response ada data sebelum dikirim ke frontend
                    var finalresponse = ResponseStandard.CreateResponseOk(apiresponse);
                    //Serializing standard response 
                    var jsonData = System.Text.Json.JsonSerializer.Serialize(finalresponse);
                    await redis.SetValueAsync(city, jsonData);
                    return Ok(finalresponse);

                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }

            }

            //Getredis caching to improve the perform
            var cache = redis.GetValue(city);
            return Ok(cache);
        }
    }
}
