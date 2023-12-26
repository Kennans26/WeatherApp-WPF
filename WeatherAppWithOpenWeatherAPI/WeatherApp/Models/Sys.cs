using System;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace WeatherApp.Models;

internal class Sys
{
    public int Type { get; }
    public int ID { get; }
    public double Message { get; }
    public string Country { get; }
    public DateTime Sunrise { get; }
    public DateTime Sunset { get; }

    public Sys(JToken sysData)
    {
        if (sysData != null)
        {
            if (sysData.SelectToken("type") != null)
                Type = int.Parse(sysData.SelectToken("type").ToString(), CultureInfo.InvariantCulture);
            if (sysData.SelectToken("id") != null)
                ID = int.Parse(sysData.SelectToken("id").ToString(), CultureInfo.InvariantCulture);
            if (sysData.SelectToken("message") != null)
                Message = int.Parse(sysData.SelectToken("message").ToString(), CultureInfo.InvariantCulture);

            Country = sysData.SelectToken("country").ToString();
            Sunrise = UnixToDateTime(double.Parse(sysData.SelectToken("sunrise").ToString(), CultureInfo.InvariantCulture));
            Sunset = UnixToDateTime(double.Parse(sysData.SelectToken("sunset").ToString(), CultureInfo.InvariantCulture));
        }
    }

    private static DateTime UnixToDateTime(double unixTime)
    {
        DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return dt.AddSeconds(unixTime).ToLocalTime();
    }
}
