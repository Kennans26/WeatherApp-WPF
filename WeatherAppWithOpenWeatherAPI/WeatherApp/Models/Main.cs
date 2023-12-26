using Newtonsoft.Json.Linq;
using System.Globalization;
using WeatherApp.TempUnits;

namespace WeatherApp.Models;

internal class Main
{
    public TemperatureUnit Temperature { get; }
    public double Pressure { get; }
    public double Humidity { get; }
    public double SeaLevel { get; }
    public double GroundLevel { get; }

    public Main(JToken mainData)
    {
        if (mainData != null)
        {
            Temperature = new TemperatureUnit(
                double.Parse(mainData.SelectToken("temp").ToString(), CultureInfo.InvariantCulture),
                double.Parse(mainData.SelectToken("temp_min").ToString(), CultureInfo.InvariantCulture),
                double.Parse(mainData.SelectToken("temp_max").ToString(), CultureInfo.InvariantCulture));

            Pressure = double.Parse(mainData.SelectToken("pressure").ToString(), CultureInfo.InvariantCulture);
            Humidity = double.Parse(mainData.SelectToken("humidity").ToString(), CultureInfo.InvariantCulture);

            if (mainData.SelectToken("sea_level") != null)
                SeaLevel = double.Parse(mainData.SelectToken("sea_level").ToString(), CultureInfo.InvariantCulture);
            if (mainData.SelectToken("grnd_level") != null)
                GroundLevel = double.Parse(mainData.SelectToken("grnd_level").ToString(), CultureInfo.InvariantCulture);
        }
    }
}
