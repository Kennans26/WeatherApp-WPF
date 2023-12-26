using System;

namespace WeatherApp.TempUnits
{
    internal class TemperatureUnit
    {
        public double CelsiusCurrent { get; }
        public double FahrenheitCurrent { get; }
        public double KelvinCurrent { get; }
        public double CelsiusMinimum { get; }
        public double CelsiusMaximum { get; }
        public double FahrenheitMinimum { get; }
        public double FahrenheitMaximum { get; }
        public double KelvinMinimum { get; }
        public double KelvinMaximum { get; }

        public TemperatureUnit(double temp, double min, double max)
        {
            KelvinCurrent = temp;
            KelvinMinimum = min;
            KelvinMaximum = max;

            CelsiusCurrent = KelvinToCelsius(KelvinCurrent);
            CelsiusMinimum = KelvinToCelsius(KelvinMinimum);
            CelsiusMaximum = KelvinToCelsius(KelvinMaximum);

            FahrenheitCurrent = CelsiusToFahranheit(CelsiusCurrent);
            FahrenheitMinimum = CelsiusToFahranheit(CelsiusMinimum);
            FahrenheitMaximum = CelsiusToFahranheit(CelsiusMaximum);
        }

        private static double CelsiusToFahranheit(double celsius)
        {
            return Math.Round(((9.0 / 5.0) * celsius) + 32, 3);
        }

        private static double KelvinToCelsius(double kelvin)
        {
            return Math.Round(kelvin - 273.15, 3);
        }
    }
}
