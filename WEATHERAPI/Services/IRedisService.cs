namespace WeatherAPI.Services;

public interface IRedisService
{
    public void SetValue(string key, string value);
    public Task SetValueAsync(string key, string value);
    public string GetValue(string key);
}
