using Newtonsoft.Json.Linq;
using System.Globalization;

namespace WeatherApp.Models;

internal class Rain
{
    public double H1 { get; }
    public double H3 { get; }

    public Rain(JToken rainData)
    {
        if (rainData != null)
        {
            if (rainData.SelectToken("3h") != null)
                H3 = double.Parse(rainData.SelectToken("3h").ToString(), CultureInfo.InvariantCulture);
            if (rainData.SelectToken("1h") != null)
                H1 = double.Parse(rainData.SelectToken("1h").ToString(), CultureInfo.InvariantCulture);
        }
    }
}
