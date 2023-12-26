using Newtonsoft.Json.Linq;
using System.Globalization;

namespace WeatherApp.Models;

public class Wind
{
    public enum DirectionEnum
    {
        North,
        North_North_East,
        North_East,
        East_North_East,
        East,
        East_South_East,
        South_East,
        South_South_East,
        South,
        South_South_West,
        South_West,
        West_South_West,
        West,
        West_North_West,
        North_West,
        North_North_West,
        Unknown
    }

    public double SpeedKilometersPerHour { get; }
    public DirectionEnum Direction { get; }
    public double SpeedMetersPerSecond { get; }
    public double Degree { get; }
    public double Gust { get; }

    public Wind(JToken windData)
    {
        if (windData != null)
        {
            SpeedMetersPerSecond = double.Parse(windData.SelectToken("speed").ToString(), CultureInfo.InvariantCulture);
            SpeedKilometersPerHour = SpeedMetersPerSecond * 3.6;
            Degree = double.Parse(windData.SelectToken("deg").ToString(), CultureInfo.InvariantCulture);

            if (windData.SelectToken("gust") != null)
                Gust = double.Parse(windData.SelectToken("gust").ToString(), CultureInfo.InvariantCulture);
            
            Direction = AssignDirection(Degree);
        }
    }

    private DirectionEnum AssignDirection(double degree)
    {
        if (IsBetween(degree, 348.75, 360))
            return DirectionEnum.North;
        if (IsBetween(degree, 0, 11.25))
            return DirectionEnum.North;
        if (IsBetween(degree, 11.25, 33.75))
            return DirectionEnum.North_North_East;
        if (IsBetween(degree, 33.75, 56.25))
            return DirectionEnum.North_East;
        if (IsBetween(degree, 56.25, 78.75))
            return DirectionEnum.East_North_East;
        if (IsBetween(degree, 78.75, 101.25))
            return DirectionEnum.East;
        if (IsBetween(degree, 101.25, 123.75))
            return DirectionEnum.East_South_East;
        if (IsBetween(degree, 123.75, 146.25))
            return DirectionEnum.South_East;
        if (IsBetween(degree, 168.75, 191.25))
            return DirectionEnum.South;
        if (IsBetween(degree, 191.25, 213.75))
            return DirectionEnum.South_South_West;
        if (IsBetween(degree, 213.75, 236.25))
            return DirectionEnum.South_West;
        if (IsBetween(degree, 236.25, 258.75))
            return DirectionEnum.West_South_West;
        if (IsBetween(degree, 258.75, 281.25))
            return DirectionEnum.West;
        if (IsBetween(degree, 281.25, 303.75))
            return DirectionEnum.West_North_West;
        if (IsBetween(degree, 303.75, 326.25))
            return DirectionEnum.North_West;
        if (IsBetween(degree, 326.25, 348.75))
            return DirectionEnum.North_North_West;
        return DirectionEnum.Unknown;
    }

    private static bool IsBetween(double value, double min, double max)
    {
        return value >= min && value <= max;
    }
}
