using Newtonsoft.Json.Linq;
using System.Globalization;

namespace WeatherApp.Models;

internal class Snow
{
    public double H1 { get; }
    public double H3 { get; }

    public Snow(JToken snowData)
    {
        if (snowData != null)
        {
            if (snowData.SelectToken("3h") != null)
                H3 = double.Parse(snowData.SelectToken("3h").ToString(), CultureInfo.InvariantCulture);

            H1 = double.Parse(snowData.SelectToken("1h").ToString(), CultureInfo.InvariantCulture);
        }
    }
}
