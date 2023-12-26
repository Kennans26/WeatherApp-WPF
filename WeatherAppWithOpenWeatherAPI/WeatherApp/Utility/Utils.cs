using System;
using System.Globalization;
using static WeatherApp.Models.Wind;

namespace WeatherApp;

public static class Utils
{
    public static string DirectionEnumToString(this DirectionEnum dir)
    {
        switch (dir)
        {
            case DirectionEnum.East:
                return "East";
            case DirectionEnum.East_North_East:
                return "East North-East";
            case DirectionEnum.East_South_East:
                return "East South-East";
            case DirectionEnum.North:
                return "North";
            case DirectionEnum.North_East:
                return "North East";
            case DirectionEnum.North_North_East:
                return "North North-East";
            case DirectionEnum.North_North_West:
                return "North North-West";
            case DirectionEnum.North_West:
                return "North West";
            case DirectionEnum.South:
                return "South";
            case DirectionEnum.South_East:
                return "South East";
            case DirectionEnum.South_South_East:
                return "South South-East";
            case DirectionEnum.South_South_West:
                return "South South-West";
            case DirectionEnum.South_West:
                return "South West";
            case DirectionEnum.West:
                return "West";
            case DirectionEnum.West_North_West:
                return "West North-West";
            case DirectionEnum.West_South_West:
                return "West South-West";
            case DirectionEnum.Unknown:
                return "Unknown";
            default:
                return "Unknown";
        }
    }

    public static string ToUppercaseWords(this string value)
    {
        char[] array = value.ToCharArray();
        //handle the first letter in the string
        if (array.Length >= 1)
        {
            if (char.IsLower(array[0]))
            {
                array[0] = char.ToUpper(array[0]);
            }
        }
        //scan through the letters, checking for spaces
        //uppercase the lowercase letters following spaces
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i - 1] == ' ')
            {
                if (char.IsLower(array[i]))
                {
                    array[i] = char.ToUpper(array[i]);
                }
            }
        }
        return new string(array);
    }

    public static DateTime UnixToDateTime(this int unixTime)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime;
    }

    public static string UnixToDayHour(this int unixTime)
    {
        var dt = UnixToDateTime(unixTime);

        return dt.ToString("dddd, hh:mm tt", CultureInfo.InvariantCulture);
    }

    public static string ToTimeOnly(this DateTime dt)
    {
        return dt.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture);
    }

    public static string ToTimeOnlyFromUTC(this int seconds, DateTime date)
    {
        int dateDifference = DateTime.Compare(date, date.ToUniversalTime());
        string sign = dateDifference < 0 ? "-" : dateDifference > 0 ? "+" : string.Empty;
        string timeonly = seconds != 0 ? $"{sign}{TimeSpan.FromSeconds(seconds).ToString(@"hh\hmm")}" : "0";

        return timeonly != "0" ? timeonly : "0";
    }

    //a method for getting weather icon for current result
    public static string GetWeatherIconName(int weatherID, string iconID)
    {
        var timePeriod = iconID.ToCharArray()[2]; //this is either d or n (day or night)
        var img = string.Empty;

        if (weatherID >= 200 && weatherID < 300) img = "storm.png";
        else if (weatherID >= 300 && weatherID < 500) img = "drizzle.png";
        else if (weatherID >= 500 && weatherID < 600) img = "rain.png";
        else if (weatherID >= 600 && weatherID < 700) img = "snow.png";
        else if (weatherID >= 700 && weatherID < 800) img = "atmosphere.png";
        else if (weatherID == 800) img = (timePeriod == 'd') ? "clear_day.png" : "clear_night.png";
        else if (weatherID == 801) img = (timePeriod == 'd') ? "few_clouds_day.png" : "few_clouds_night.png";
        else if (weatherID == 802 || weatherID == 803) img = (timePeriod == 'd') ? "few_clouds_day.png" : "few_clouds_night.png";
        else if (weatherID == 804) img = "overcast_clouds.png";
        else if (weatherID >= 900 && weatherID < 903) img = "extreme.png";
        else if (weatherID == 903) img = "cold.png";
        else if (weatherID == 904) img = "hot.png";
        else if (weatherID == 905 || weatherID >= 951) img = "windy.png";
        else if (weatherID == 906) img = "hail.png";

        return img;
    }
}
