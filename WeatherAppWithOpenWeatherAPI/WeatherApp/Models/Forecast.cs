using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace WeatherApp.Models;

internal class Forecast
{
    public int DateTime { get; }
    public Main Main { get; }
    public List<Weather> WeatherList { get; } = new List<Weather>();

    public Forecast(JToken forecastData)
    {
        if (forecastData != null)
        {
            DateTime = int.Parse(forecastData.SelectToken("dt").ToString(), CultureInfo.InvariantCulture);
            Main = new Main(forecastData.SelectToken("main"));

            foreach (JToken weather in forecastData.SelectToken("weather"))
                WeatherList.Add(new Weather(weather));
        }
    }
}
