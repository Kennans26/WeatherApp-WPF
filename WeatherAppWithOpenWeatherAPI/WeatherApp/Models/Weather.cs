using Newtonsoft.Json.Linq;
using System.Globalization;

namespace WeatherApp.Models;

internal class Weather
{
    public int ID { get; }
    public string Main { get; }
    public string Description { get; }
    public string Icon { get; }

    public Weather(JToken weatherData)
    {
        if (weatherData != null)
        {
            ID = int.Parse(weatherData.SelectToken("id").ToString(), CultureInfo.InvariantCulture);
            Main = weatherData.SelectToken("main").ToString();
            Description = weatherData.SelectToken("description").ToString();
            Icon = weatherData.SelectToken("icon").ToString();
        }
    }
}
