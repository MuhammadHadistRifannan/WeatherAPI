using System.Text.Json;
using Newtonsoft.Json;

namespace WeatherAPI.Helper;

public class JsonHelper
{
    public static dynamic FromStringToJson(string value)
    {
        return JsonConvert.DeserializeObject<dynamic>(value)!;
    }
}   
