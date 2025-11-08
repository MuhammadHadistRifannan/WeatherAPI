using System.Text.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherAPI.StandardHttp;

public class ResponseStandard
{
    public static object CreateResponseOk(string _data)
    {
        var response = new
        {
            status_code = 200,
            message = "Data has been transfer successfully",
            data = JsonDocument.Parse(_data)
        };

        return response;
    }
}
