using Newtonsoft.Json.Linq;
using System.Globalization;

namespace WeatherApp.Models;

internal class Clouds
{
    public double All { get; }

    public Clouds(JToken cloudData)
	{
		if(cloudData != null)
			All = double.Parse(cloudData.SelectToken("all").ToString(), CultureInfo.InvariantCulture);
	}
}
