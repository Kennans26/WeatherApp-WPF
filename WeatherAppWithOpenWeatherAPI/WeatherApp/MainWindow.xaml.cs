using System;
using System.Windows;
using WeatherApp.Models;
using WeatherApp.Clients;
using WeatherApp.Responses;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using WeatherApp.UserControls;
using System.Windows.Media.Imaging;
using static WeatherApp.Tools.Enums;

namespace WeatherApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private QueryResponse currentResult;
    private ForecastQueryResponse forecastResult;

    public MainWindow()
    {
        InitializeComponent();

        txtAPIStatus.Foreground = new SolidColorBrush(Colors.Red);
        txtAPIStatus.Text = "Offline";
        txtSearch.Focus();
    }

    private void button_Click(object sender, RoutedEventArgs e)
    {
        var btn = (Button)sender;

        switch (btn.Name)
        {
            case "btnCelsius":
                if (currentResult == null)
                    return;
                if (btnCelsius.Style == FindResource("tempButton"))
                {
                    btnCelsius.Style = (Style)FindResource("activeTempButton");
                    btnFahrenheit.Style = (Style)FindResource("tempButton");
                    btnKelvin.Style = (Style)FindResource("tempButton");

                    tbCurrentTemp.Text = Math.Round(currentResult.Main.Temperature.CelsiusCurrent).ToString();
                    tbTempUnit.Text = "°C";
                    ChangeTempUnit(TempUnit.Celsius, "°C");
                }
                break;
            case "btnFahrenheit":
                if (currentResult == null)
                    return;
                if (btnFahrenheit.Style == FindResource("tempButton"))
                {
                    btnFahrenheit.Style = (Style)FindResource("activeTempButton");
                    btnCelsius.Style = (Style)FindResource("tempButton");
                    btnKelvin.Style = (Style)FindResource("tempButton");

                    tbCurrentTemp.Text = Math.Round(currentResult.Main.Temperature.FahrenheitCurrent).ToString();
                    tbTempUnit.Text = "°F";
                    ChangeTempUnit(TempUnit.Fahrenheit, "°F");
                }
                break;
            case "btnKelvin":
                if (currentResult == null)
                    return;
                if (btnKelvin.Style == FindResource("tempButton"))
                {
                    btnKelvin.Style = (Style)FindResource("activeTempButton");
                    btnCelsius.Style = (Style)FindResource("tempButton");
                    btnFahrenheit.Style = (Style)FindResource("tempButton");

                    tbCurrentTemp.Text = Math.Round(currentResult.Main.Temperature.KelvinCurrent).ToString();
                    tbTempUnit.Text = "K";
                    ChangeTempUnit(TempUnit.Kelvin, "K");
                }
                break;
            case "btnMinimize": //for minimize the current window
                WindowState = WindowState.Minimized;
                break;
            case "btnRefresh":
                if (currentResult != null)
                {
                    var client = new OpenWeatherAPIClient();
                    currentResult = client.Query(currentResult.Name);
                    if (currentResult.ValidRequest)
                        FillWeatherData();
                }
                break;
            case "btnClose": //for close (exit) the current window
                Close();
                Environment.Exit(0);
                break;
            case "btnReload":
                break;
        }
    }


    private void textSearch_MouseDown(object sender, MouseButtonEventArgs e)
    {
        txtSearch.Focus();
    }

    private void txtSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && txtSearch.Text.Length > 0)
        {
            var client = new OpenWeatherAPIClient(); //apiKey + httpClient + (disposed)
            currentResult = client.Query(txtSearch.Text);
            if (currentResult != null)
            {
                forecastResult = client.Query(currentResult.Coordinates.Longitude.ToString(), currentResult.Coordinates.Latitude.ToString());
                FillWeatherData();
                FillWeatherForecast();
            }
        }
    }

    private void textSearch_MouseEnter(object sender, MouseEventArgs e)
    {
        textSearch.Visibility = Visibility.Collapsed;
    }

    private void textSearch_MouseLeave(object sender, MouseEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSearch.Text))
        {
            textSearch.Visibility = Visibility.Visible;
        }
    }

    private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        textSearch.Visibility = !string.IsNullOrEmpty(txtSearch.Text) && txtSearch.Text.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }


    private void FillWeatherData()
    {
        imgWeaher.Source = new BitmapImage(new Uri(@$"/Images/{Utils.GetWeatherIconName(currentResult.ID, currentResult.WeatherList[0].Icon)}", UriKind.Relative));
        txtAPIStatus.Foreground = new SolidColorBrush(Colors.Green);
        txtAPIStatus.Text = "Online";
        
        double temp = btnCelsius.Style == FindResource("activeTempButton") ? currentResult.Main.Temperature.CelsiusCurrent :
            btnFahrenheit.Style == FindResource("activeTempButton") ? currentResult.Main.Temperature.FahrenheitCurrent :
            currentResult.Main.Temperature.KelvinCurrent;

        tbCurrentTemp.Text = Math.Round(temp).ToString();

        tbTempUnit.Text = btnCelsius.Style == FindResource("activeTempButton") ? "°C" :
            btnFahrenheit.Style == FindResource("activeTempButton") ? "°F" : "K";

        tbDayHour.Text = currentResult.DateTime.UnixToDayHour();
        tbDateFetch.Text = currentResult.DateTime.UnixToDateTime().ToString("dd.MM.yyyy");
        tbDescription.Text = currentResult.WeatherList[0].Description.ToUppercaseWords();

        tbVisibility.Text = Math.Round((currentResult.Visibility / 1000), 2).ToString();
        tbHumidityPercent.Text = currentResult.Main.Humidity.ToString();
        
        tbWindSpeed.Text = Math.Round(currentResult.Wind.SpeedKilometersPerHour, 2).ToString();
        tbWindDirection.Text = currentResult.Wind.Direction.DirectionEnumToString();
        
        tbShiftFromUTC.Text = currentResult.Timezone.ToTimeOnlyFromUTC(currentResult.DateTime.UnixToDateTime());
        tbUTC.Text = currentResult.DateTime.UnixToDateTime().ToUniversalTime().ToString("dd.MM.yyyy hh:mm tt");

        tbLongitude.Text = currentResult.Coordinates.Longitude.ToString();
        tbLatitude.Text = currentResult.Coordinates.Latitude.ToString();

        tbSunrise.Text = currentResult.Sys.Sunrise.ToString("hh:mm:ss tt");
        tbSunset.Text = currentResult.Sys.Sunset.ToString("hh:mm:ss tt");

        tbCity.Text = $"{currentResult.Name}, {currentResult.Sys.Country}";
    }

    private void FillWeatherForecast()
    {
        spForecast.Children.Clear();

        foreach (Day day in forecastResult.DayList)
        {
            spForecast.Children.Add(new CardDay()
            {
                Day = day.Name == currentResult.DateTime.UnixToDateTime().ToString("dddd") ? "Today" : day.Name,
                Source = new BitmapImage(new Uri(@$"/Images/{Utils.GetWeatherIconName(day.ID, day.Icon)}", UriKind.Relative)),
                Detail = day.Description.ToUppercaseWords(),

                MinTemp = btnCelsius.Style == FindResource("activeTempButton") ? $"{Math.Round(day.Temperature.CelsiusMinimum)}°C" :
                          btnFahrenheit.Style == FindResource("activeTempButton") ? $"{Math.Round(day.Temperature.FahrenheitMinimum)}°F" :
                          $"{Math.Round(day.Temperature.KelvinMinimum)}K",

                MaxTemp = btnCelsius.Style == FindResource("activeTempButton") ? $"{Math.Round(day.Temperature.CelsiusMaximum)}°C" :
                          btnFahrenheit.Style == FindResource("activeTempButton") ? $"{Math.Round(day.Temperature.FahrenheitMaximum)}°F" :
                          $"{Math.Round(day.Temperature.KelvinMaximum)}K",
            });
        }
    }


    private void ChangeTempUnit(TempUnit unit, string sign)
    {
        int i = 0;

        foreach (CardDay day in spForecast.Children)
        {
            string minTemp = unit == TempUnit.Celsius ? $"{Math.Round(forecastResult.DayList[i].Temperature.CelsiusMinimum)}{sign}" :
                unit == TempUnit.Fahrenheit ? $"{Math.Round(forecastResult.DayList[i].Temperature.FahrenheitMinimum)}{sign}" :
                $"{Math.Round(forecastResult.DayList[i].Temperature.KelvinMinimum)}{sign}";

            string maxTemp = unit == TempUnit.Celsius ? $"{Math.Round(forecastResult.DayList[i].Temperature.CelsiusMaximum)}{sign}" :
                unit == TempUnit.Fahrenheit ? $"{Math.Round(forecastResult.DayList[i].Temperature.FahrenheitMaximum)}{sign}" :
                $"{Math.Round(forecastResult.DayList[i].Temperature.KelvinMaximum)}{sign}";

            day.MinTemp = minTemp;
            day.MaxTemp = maxTemp;

            i++;
        }
    }
}
