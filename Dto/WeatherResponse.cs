using System.Text.Json.Serialization;

namespace McpServerSample.Dto;

public class WeatherResponse
{
    [JsonPropertyName("current_condition")]
    public CurrentCondition[]? CurrentCondition { get; set; }

    [JsonPropertyName("weather")]
    public WeatherDay[]? Weather { get; set; }

    [JsonPropertyName("nearest_area")]
    public NearestArea[]? NearestArea { get; set; }
}

public class CurrentCondition
{
    [JsonPropertyName("temp_C")]
    public string TempC { get; set; } = "";

    [JsonPropertyName("temp_F")]
    public string TempF { get; set; } = "";

    [JsonPropertyName("FeelsLikeC")]
    public string FeelsLikeC { get; set; } = "";

    [JsonPropertyName("FeelsLikeF")]
    public string FeelsLikeF { get; set; } = "";

    [JsonPropertyName("humidity")]
    public string Humidity { get; set; } = "";

    [JsonPropertyName("windspeedKmph")]
    public string WindspeedKmph { get; set; } = "";

    [JsonPropertyName("winddir16Point")]
    public string Winddir16Point { get; set; } = "";

    [JsonPropertyName("visibility")]
    public string Visibility { get; set; } = "";

    [JsonPropertyName("uvIndex")]
    public string UvIndex { get; set; } = "";

    [JsonPropertyName("observation_time")]
    public string ObservationTime { get; set; } = "";

    [JsonPropertyName("weatherDesc")]
    public WeatherDesc[]? WeatherDesc { get; set; }
}

public class WeatherDay
{
    [JsonPropertyName("date")]
    public string Date { get; set; } = "";

    [JsonPropertyName("maxtempC")]
    public string MaxtempC { get; set; } = "";

    [JsonPropertyName("maxtempF")]
    public string MaxtempF { get; set; } = "";

    [JsonPropertyName("mintempC")]
    public string MintempC { get; set; } = "";

    [JsonPropertyName("mintempF")]
    public string MintempF { get; set; } = "";

    [JsonPropertyName("avgtempC")]
    public string AvgtempC { get; set; } = "";

    [JsonPropertyName("avgtempF")]
    public string AvgtempF { get; set; } = "";

    [JsonPropertyName("uvIndex")]
    public string UvIndex { get; set; } = "";

    [JsonPropertyName("hourly")]
    public HourlyData[]? Hourly { get; set; }

    [JsonPropertyName("astronomy")]
    public Astronomy[]? Astronomy { get; set; }
}

public class HourlyData
{
    [JsonPropertyName("weatherDesc")]
    public WeatherDesc[]? WeatherDesc { get; set; }
}

public class WeatherDesc
{
    [JsonPropertyName("value")]
    public string Value { get; set; } = "";
}

public class NearestArea
{
    [JsonPropertyName("areaName")]
    public AreaName[]? AreaName { get; set; }
}

public class AreaName
{
    [JsonPropertyName("value")]
    public string Value { get; set; } = "";
}

public class Astronomy
{
    [JsonPropertyName("sunrise")]
    public string Sunrise { get; set; } = "";

    [JsonPropertyName("sunset")]
    public string Sunset { get; set; } = "";
}
