using WeatherApp.Models;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace WeatherApp.Responses;

internal class QueryResponse
{
    public bool ValidRequest { get; } //if "cod" == 200 true, other hand is false

    #region ValidRequest Info!
    /* 2xx - Success!
     * 200 - OK, the request was successful,
     * 201 - Created, the request was successful, and a resource was created,
     * 204 - No Content, the request was successful, but there is no additional
     * information to send in the response payload.
     */
    /* 4xx - Client Error!
     * 400 - Bad Request, the request could not be understood or was missing required
     * parameters,
     * 401 - zunauthorized, Authentication failed or user doesn't have permissions.
     */
    /*
     * 5xx - Server Error!
     * 500 - Internal Server Error, the server encountered an unexpected condition 
     * that prevented it from fulfiling request.
     */
    #endregion

    public Coordinates Coordinates { get; }
    public List<Weather> WeatherList { get; } = new List<Weather>();
    public string Base { get; }
    public Main Main { get; }
    public double Visibility { get; }
    public Wind Wind { get; }
    public Rain Rain { get; }
    public Snow Snow { get; }
    public Clouds Clouds { get; }
    public int DateTime { get; }
    public Sys Sys { get; }
    public int Timezone { get; }
    public int ID { get; }
    public string Name { get; }
    public int Cod { get; }

    public QueryResponse(string jsonResponse)
    {
        var jsonData = JObject.Parse(jsonResponse);

        if (jsonData.SelectToken("cod").ToString() == "200")
        {
            ValidRequest = true;
            Coordinates = new Coordinates(jsonData.SelectToken("coord"));

            foreach (JToken weather in jsonData.SelectToken("weather"))
                WeatherList.Add(new Weather(weather));

            Base = jsonData.SelectToken("base").ToString();
            Main = new Main(jsonData.SelectToken("main"));
            Visibility = double.Parse(jsonData.SelectToken("visibility").ToString(), CultureInfo.InvariantCulture);
            Wind = new Wind(jsonData.SelectToken("wind"));
            Rain = new Rain(jsonData.SelectToken("rain"));
            Snow = new Snow(jsonData.SelectToken("snow"));
            Clouds = new Clouds(jsonData.SelectToken("clouds"));
            Sys = new Sys(jsonData.SelectToken("sys"));
            DateTime = int.Parse(jsonData.SelectToken("dt").ToString(), CultureInfo.InvariantCulture);
            Timezone = int.Parse(jsonData.SelectToken("timezone").ToString(), CultureInfo.InvariantCulture);
            ID = int.Parse(jsonData.SelectToken("id").ToString(), CultureInfo.InvariantCulture);
            Name = jsonData.SelectToken("name").ToString();
            Cod = int.Parse(jsonData.SelectToken("cod").ToString(), CultureInfo.InvariantCulture);
        }
        else ValidRequest = false; //mean to jsonData.SelectToken("cod").ToString() != "200"
    }
}
