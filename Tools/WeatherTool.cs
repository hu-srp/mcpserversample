using McpServerSample.Dto;
using ModelContextProtocol.Server;
using Serilog;
using System.ComponentModel;
using System.Text.Json;

namespace McpServerSample.Tools;

[McpServerToolType]
public sealed class WeatherTool
{
    private readonly IHttpClientFactory _httpClientFactory;

    public WeatherTool(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [McpServerTool, Description("Query weather information for a specific city")]
    public async Task<string> GetWeather([Description("City name (e.g., Beijing, Shanghai, Zhengzhou)")] string city)
    {

        try
        {
            var httpClient = _httpClientFactory.CreateClient("weather");
            var url = $"https://wttr.in/{Uri.EscapeDataString(city)}?format=j1";
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return $"Failed to fetch weather data for '{city}'. Status code: {response.StatusCode}";
            }

            var json = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<WeatherResponse>(json);

            if (weatherData?.CurrentCondition == null || weatherData.CurrentCondition.Length == 0)
            {
                return $"Could not parse weather data for '{city}'.";
            }

            var current = weatherData.CurrentCondition[0];
            var area = weatherData.NearestArea?[0];
            var locationName = area?.AreaName?[0].Value ?? city;

            var result = $"""
                Weather for {locationName}:
                - Condition: {current.WeatherDesc?[0].Value}
                - Temperature: {current.TempC}°C ({current.TempF}°F)
                - Feels Like: {current.FeelsLikeC}°C ({current.FeelsLikeF}°F)
                - Humidity: {current.Humidity}%
                - Wind: {current.WindspeedKmph} km/h ({current.Winddir16Point})
                - Visibility: {current.Visibility} km
                - UV Index: {current.UvIndex}
                - Local Time: {current.ObservationTime}
                """;

            Log.Information("Weather query for {City}: {Condition}, {Temperature}°C", city, current.WeatherDesc?[0].Value, current.TempC);
            return result;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error fetching weather for '{City}'", city);
            return $"Error fetching weather for '{city}': {ex.Message}";
        }
    }

    [McpServerTool, Description("Get weather forecast for a city")]
    public async Task<string> GetWeatherForecast(
        [Description("City name (e.g., Beijing, Shanghai, Zhengzhou)")] string city,
        [Description("Number of days to forecast (1-3)")] int days = 3)
    {

        try
        {
            days = Math.Clamp(days, 1, 3);
            var httpClient = _httpClientFactory.CreateClient("weather");
            var url = $"https://wttr.in/{Uri.EscapeDataString(city)}?format=j1";
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return $"Failed to fetch weather forecast for '{city}'. Status code: {response.StatusCode}";
            }

            var json = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<WeatherResponse>(json);

            if (weatherData?.Weather == null || weatherData.Weather.Length == 0)
            {
                return $"Could not parse weather forecast for '{city}'.";
            }

            var area = weatherData.NearestArea?[0];
            var locationName = area?.AreaName?[0].Value ?? city;
            var results = new List<string> { $"Weather Forecast for {locationName}:\n" };

            for (int i = 0; i < Math.Min(days, weatherData.Weather.Length); i++)
            {
                var day = weatherData.Weather[i];
                var forecast = $"""
                    Day {i + 1} ({day.Date}):
                    - Max Temp: {day.MaxtempC}°C ({day.MaxtempF}°F)
                    - Min Temp: {day.MintempC}°C ({day.MintempF}°F)
                    - Avg Temp: {day.AvgtempC}°C ({day.AvgtempF}°F)
                    - Condition: {day.Hourly?[0]?.WeatherDesc?[0].Value ?? "N/A"}
                    - UV Index: {day.UvIndex}
                    - Sunrise: {day.Astronomy?[0].Sunrise}
                    - Sunset: {day.Astronomy?[0].Sunset}
                    """;
                results.Add(forecast);
            }

            var forecastResult = string.Join("\n", results);
            Log.Information("Weather forecast query for {City}: {Days} days", city, days);
            return forecastResult;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error fetching weather forecast for '{City}'", city);
            return $"Error fetching weather forecast for '{city}': {ex.Message}";
        }
    }
}