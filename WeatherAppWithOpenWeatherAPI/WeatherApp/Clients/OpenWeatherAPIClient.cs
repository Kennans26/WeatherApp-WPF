using System;
using System.Windows;
using System.Net.Http;
using WeatherApp.Responses;
using System.Threading.Tasks;

namespace WeatherApp.Clients;

internal class OpenWeatherAPIClient : IDisposable
{
    private bool disposed;

    private readonly string apiKey;
    private readonly HttpClient httpClient;

    public OpenWeatherAPIClient()
    {
        apiKey = GetVariable(); //getting a apiKey on Environment Variables...
        httpClient = new HttpClient();
    }

    #region About Uri
    /*
     * As a result, 'Uri' objects represent web addresses created to point to an API
     * or other resource. These objects are a class provided by .NET to manage
     * and create components of the URI.
     */
    #endregion
    private Uri GenerateRequestUrl(string queryString) => new Uri($"http://api.openweathermap.org/data/2.5/weather?appid={apiKey}&q={queryString}");
    private Uri GenerateRequestUrl(string longitude, string latitude) => new Uri($"http://api.openweathermap.org/data/2.5/forecast?appid={apiKey}&lon={longitude}&lat={latitude}");


    private string GetVariable()
    {
        string? value = System.Environment.GetEnvironmentVariable("OWAPI_Key", EnvironmentVariableTarget.User);
        return !string.IsNullOrEmpty(value) ? value : string.Empty;
    }


    private async Task<QueryResponse?> QueryAsync(string queryString)
    {
        try
        {
            var jsonResponse = await httpClient.GetStringAsync(GenerateRequestUrl(queryString)).ConfigureAwait(false);
            var query = new QueryResponse(jsonResponse); //creating a new QueryResponse class's instance in here
            return query.ValidRequest ? query : null;
        }
        catch (HttpRequestException)
        {
            MessageBox.Show("Unable to process your request. Please double check the city name or ensure that you are connected to the internet.", "Invalid Request", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        return null;
    }
    private async Task<ForecastQueryResponse?> QueryAsync(string longitude, string latitude)
    {
        try
        {
            var jsonResponse = await httpClient.GetStringAsync(GenerateRequestUrl(longitude, latitude)).ConfigureAwait(false);
            var query = new ForecastQueryResponse(jsonResponse);
            return query.ValidRequest ? query : null;
        }
        catch (HttpRequestException)
        {
            MessageBox.Show("Unable to process your request. Please double check the city name or ensure that you are connected to the internet.", "Invalid Request", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        return null;
    }

    public QueryResponse? Query(string queryString)
    {
        return Task.Run(async () => await QueryAsync(queryString).ConfigureAwait(false)).ConfigureAwait(false).GetAwaiter().GetResult();
    }
    public ForecastQueryResponse? Query(string longitude, string latitude)
    {
        return Task.Run(async () => await QueryAsync(longitude, latitude).ConfigureAwait(false)).ConfigureAwait(false).GetAwaiter().GetResult();
    }


    public void Dispose()
    {
        //dispose of unmanaged resources.
        Dispose(true);
        //suppress finalization.
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }

        if (disposing)
        {
            //dispose managed state (managed objects)
        }

        //free unmanaged resources (unmanaged objects) and override a finalizer below
        //set large fields to null

        httpClient.Dispose();
        disposed = true;
    }
}
