# WeatherApp

WeatherApp is a desktop weather application built with C#, WPF, and the OpenWeatherMap API. It provides real-time weather information, a user-friendly interface, and customizable features.

## Overview

WeatherApp is designed to provide users with quick access to current weather conditions and forecasts. It leverages the OpenWeatherMap API to fetch accurate and up-to-date weather data. The application is built with C# using the WPF framework, offering a modern and intuitive user interface.

## Features

- **Current Weather:** View real-time weather conditions for a specified location.
- **Forecast:** Access detailed weather forecasts, including temperature, humidity, and wind speed.
- **Customization:** Personalize the app by adding multiple locations for quick weather updates.
- **User-friendly Interface:** Enjoy a clean and intuitive interface designed for a seamless user experience.

## Getting Started

### Prerequisites

- .NET Framework [minimum .NET 6.0 version]
- Visual Studio [2021/2022 or any + version]

### Installation
- Clone the repository to your local machine:

   ```bash
   git clone https://github.com/Kennans26/WeatherApp-WPF.git


## Usage
Ensure that you possess an active and compliant API key from OpenWeatherMap.

1. Navigate to the properties of "This PC" on your computer's desktop. Right-click on "This PC" and select "Properties."

2. In the "System Properties" window, click on "Advanced system settings" on the left-hand side.

3. Within the "System Properties" window, click on the "Environment Variables..." button.

4. In the "Environment Variables" window, locate and click on the "New..." button.

5. Enter the following details for the new environment variable:

   - **Variable name:** `OWAPI_Key`
   - **Variable value:** [Enter the API key you obtained from the website]

6. Click "OK" to save the new environment variable.

By following these steps, you will have successfully added a new environment variable named `OWAPI_Key` with the corresponding API key value, making it accessible for your WeatherApp application.

Now, you can run the WeatherApp, and it will use the API key you provided in the environment variable for fetching weather data.


## Contact

- **Email:** info.kennans26@gmail.com
- **LinkedIn:** [Kennans26 LinkedIn Profile](https://www.linkedin.com/in/kennans26)

## License

This project is licensed under the [MIT License](https://github.com/Kennans26/WeatherApp-WPF/blob/main/LICENSE).
