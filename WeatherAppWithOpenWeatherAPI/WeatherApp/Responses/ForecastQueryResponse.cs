using System.Linq;
using WeatherApp.Models;
using WeatherApp.TempUnits;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace WeatherApp.Responses;

internal class ForecastQueryResponse
{
    public bool ValidRequest { get; }
    public int Cnt { get; }
    public List<Forecast> ForecastList { get; } = new List<Forecast>();
    public List<Day> DayList { get; } = new List<Day>();

    public ForecastQueryResponse(string jsonResponse)
    {
        var jsonData = JObject.Parse(jsonResponse);

        if (jsonData.SelectToken("cod").ToString() == "200")
        {
            ValidRequest = true;
            Cnt = int.Parse(jsonData.SelectToken("cnt").ToString(), CultureInfo.InvariantCulture);

            foreach (JToken forecast in jsonData.SelectToken("list"))
                ForecastList.Add(new Forecast(forecast));

            ConvertForecastListToDayList();
        }
        else ValidRequest = false;
    }

    private void ConvertForecastListToDayList()
    {
        //json response will have a total of 40 timestamp with (2-8 ts/day)
        var dayTimestamp = ForecastList[0].DateTime.UnixToDateTime().ToString("dddd");
        List<double> temps = new List<double>();

        for (int i = 0; i < 40; i++)
        {
            if(ForecastList[i].DateTime.UnixToDateTime().ToString("dddd") == dayTimestamp)
            {
                temps.Add(ForecastList[i].Main.Temperature.KelvinCurrent);
            }
            else
            {
                string descript = ForecastList[i - 1].WeatherList[0].Description;
                int id = ForecastList[i - 1].WeatherList[0].ID;
                string icon = ForecastList[i - 1].WeatherList[0].Icon;
                string name = ForecastList[i - 1].DateTime.UnixToDateTime().ToString("dddd");
                var temperature = new TemperatureUnit((temps.Max() + temps.Min()) / 2, temps.Min(), temps.Max());
                var day = new Day(temperature, name, descript, id, icon);

                DayList.Add(day);
                temps.Clear();

                dayTimestamp = ForecastList[i].DateTime.UnixToDateTime().ToString("dddd");
            }
        }
    }
}
