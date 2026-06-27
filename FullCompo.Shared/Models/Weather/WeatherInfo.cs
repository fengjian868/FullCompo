namespace FullCompo.Shared.Models.Weather;

public class WeatherInfo
{
    public DateTime UpdateTime { get; set; }
    public CurrentWeather Current { get; set; } = new();
    public List<DailyForecast> ForecastDaily { get; set; } = new();
    public List<HourlyForecast> ForecastHourly { get; set; } = new();
    public List<WeatherAlert> Alerts { get; set; } = new();
    public AirQuality? Aqi { get; set; }
}

public class CurrentWeather
{
    public int WeatherCode { get; set; } = 99;
    public string WeatherText { get; set; } = "未知";
    public TemperatureValue Temperature { get; set; } = new();
    public int Humidity { get; set; }
    public int? PrecipitationDistanceKm { get; set; }
    public string? WindDirection { get; set; }
    public int? WindLevel { get; set; }
    public int? Aqi { get; set; }
    public string? AqiLevel { get; set; }

    public double? ApparentTemperature { get; set; }
    public bool IsDay { get; set; }
    public double? Pressure { get; set; }
    public double? Uv { get; set; }
    public double? WindSpeed { get; set; }
    public int? WindDirectionDegree { get; set; }
    public string? WindDirectionText { get; set; }
}

public class TemperatureValue
{
    public double Value { get; set; }
    public string Unit { get; set; } = "°C";
    public override string ToString() => $"{Value}{Unit}";
}

public class DailyForecast
{
    public DateTime Date { get; set; }
    public int WeatherCodeDay { get; set; }
    public int WeatherCodeNight { get; set; }
    public TemperatureValue High { get; set; } = new();
    public TemperatureValue Low { get; set; } = new();
    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
    public double? UvIndexMax { get; set; }
}

public class HourlyForecast
{
    public DateTime Time { get; set; }
    public int WeatherCode { get; set; }
    public TemperatureValue Temperature { get; set; } = new();
    public double? UvIndex { get; set; }
    public double? Pressure { get; set; }
}

public class WeatherAlert
{
    public string Title { get; set; } = "";
    public string Type { get; set; } = "";
    public string Level { get; set; } = "";
    public string Content { get; set; } = "";
}

public class AirQuality
{
    public int Aqi { get; set; }
    public string Level { get; set; } = "";
    public string Description { get; set; } = "";
}
