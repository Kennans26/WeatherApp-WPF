using WeatherApp.TempUnits;

namespace WeatherApp.Models;

internal class Day
{
    public TemperatureUnit Temperature { get; }
    public string Name { get; }
    public string Description { get; }
    public int ID { get; }
    public string Icon { get; }

    public Day(TemperatureUnit temperature, string name, string description, int id, string icon)
    {
        Temperature = temperature;
        Name = name;
        Description = description;
        ID = id;
        Icon = icon;
    }
}
