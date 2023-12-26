using Newtonsoft.Json.Linq;
using System.Globalization;

namespace WeatherApp.Models;

internal class Coordinates
{
    public double Longitude { get; }
    public double Latitude { get; }

    public Coordinates(JToken coordinateData)
    {
        if (coordinateData != null)
        {
            Longitude = double.Parse(coordinateData.SelectToken("lon").ToString(), CultureInfo.InvariantCulture);
            Latitude = double.Parse(coordinateData.SelectToken("lat").ToString(), CultureInfo.InvariantCulture);
        }
    }
}
