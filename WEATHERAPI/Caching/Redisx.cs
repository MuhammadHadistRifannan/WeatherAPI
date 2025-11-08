using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using StackExchange.Redis;
using WeatherAPI.Services;

namespace WeatherAPI.Caching;

public class Redisx : IRedisService
{
    readonly IDistributedCache redis;
    public Redisx(IDistributedCache _cache)
    {
        redis = _cache;
    }
    public void SetValue(string key, string value)
    {
        redis.SetString(key, value);
    }
    
    public async Task SetValueAsync(string key , string apiresponse)
    {
        await redis.SetStringAsync(key ,apiresponse, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });
    }
    
    public string GetValue(string key)
    {
        return redis.GetString(key)!;
    }
}
