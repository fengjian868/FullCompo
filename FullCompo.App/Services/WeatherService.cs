using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using Avalonia.Threading;
using FullCompo.App.Helpers;
using FullCompo.Core.Abstractions.Services;
using FullCompo.Shared.Models.Weather;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FullCompo.App.Services;

public class WeatherService : IHostedService, IWeatherService
{
    private readonly IConfigService _configService;
    private readonly ILogger<WeatherService> _logger;
    private readonly HttpClient _httpClient = new();
    private readonly DispatcherTimer _updateTimer;

    public WeatherInfo? LastWeatherInfo { get; private set; }
    public bool IsRefreshing { get; private set; }

    public event EventHandler<WeatherInfo?>? WeatherUpdated;

    public WeatherService(IConfigService configService, ILogger<WeatherService> logger)
    {
        _configService = configService;
        _logger = logger;
        _updateTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMinutes(10)
        };
        _updateTimer.Tick += async (_, _) => await RefreshAsync();
    }

    public async Task RefreshAsync()
    {
        if (IsRefreshing) return;
        if (!_configService.AppSettings.WeatherEnabled) return;

        IsRefreshing = true;
        try
        {
            var (lat, lon) = GetCoordinates();

            var forecastUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat.ToString(CultureInfo.InvariantCulture)}&longitude={lon.ToString(CultureInfo.InvariantCulture)}&current=temperature_2m,relative_humidity_2m,apparent_temperature,is_day,precipitation,rain,showers,snowfall,weather_code,cloud_cover,pressure_msl,surface_pressure,wind_speed_10m,wind_direction_10m&hourly=temperature_2m,relative_humidity_2m,precipitation_probability,weather_code,surface_pressure,uv_index&daily=weather_code,temperature_2m_max,temperature_2m_min,sunrise,sunset,uv_index_max,precipitation_sum&timezone=auto&forecast_days=7";

            var forecastJson = await _httpClient.GetStringAsync(forecastUrl);
            var forecastDoc = JsonSerializer.Deserialize<JsonElement>(forecastJson);

            var aqiUrl = $"https://air-quality-api.open-meteo.com/v1/air-quality?latitude={lat.ToString(CultureInfo.InvariantCulture)}&longitude={lon.ToString(CultureInfo.InvariantCulture)}&current=us_aqi";
            int? aqiValue = null;
            try
            {
                var aqiJson = await _httpClient.GetStringAsync(aqiUrl);
                var aqiDoc = JsonSerializer.Deserialize<JsonElement>(aqiJson);
                if (aqiDoc.TryGetProperty("current", out var currentAqi) &&
                    currentAqi.TryGetProperty("us_aqi", out var aqiElement))
                {
                    aqiValue = aqiElement.GetInt32();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to fetch air quality");
            }

            LastWeatherInfo = ParseWeatherInfo(forecastDoc, aqiValue);
            LastWeatherInfo.UpdateTime = DateTime.Now;

            WeatherUpdated?.Invoke(this, LastWeatherInfo);
            _logger.LogInformation("Weather refreshed for {City}: {Weather} {Temp}",
                _configService.AppSettings.WeatherCityName,
                LastWeatherInfo.Current.WeatherText,
                LastWeatherInfo.Current.Temperature);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to refresh weather");
            AppLog.WriteException("WeatherService.RefreshAsync", ex);
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    private (double Lat, double Lon) GetCoordinates()
    {
        var lat = _configService.AppSettings.WeatherLatitude;
        var lon = _configService.AppSettings.WeatherLongitude;

        if (lat != 0 || lon != 0)
            return (lat, lon);

        // Default: Beijing
        return (39.9042, 116.4074);
    }

    private static WeatherInfo ParseWeatherInfo(JsonElement data, int? aqiValue)
    {
        var info = new WeatherInfo();

        try
        {
            var current = data.GetProperty("current");
            var weatherCode = current.GetProperty("weather_code").GetInt32();
            var isDay = current.GetProperty("is_day").GetInt32() == 1;

            current.TryGetProperty("apparent_temperature", out var appTemp);
            current.TryGetProperty("surface_pressure", out var pressure);
            current.TryGetProperty("wind_speed_10m", out var windSpeed);
            current.TryGetProperty("wind_direction_10m", out var windDir);

            info.Current = new CurrentWeather
            {
                WeatherCode = weatherCode,
                WeatherText = OpenMeteoWeatherCode.GetText(weatherCode),
                Temperature = new TemperatureValue
                {
                    Value = current.GetProperty("temperature_2m").GetDouble(),
                    Unit = "°C"
                },
                Humidity = current.GetProperty("relative_humidity_2m").GetInt32(),
                ApparentTemperature = appTemp.ValueKind == JsonValueKind.Number ? appTemp.GetDouble() : null,
                IsDay = isDay,
                Pressure = pressure.ValueKind == JsonValueKind.Number ? pressure.GetDouble() : null,
                Uv = null,
                WindSpeed = windSpeed.ValueKind == JsonValueKind.Number ? windSpeed.GetDouble() : null,
                WindDirectionDegree = windDir.ValueKind == JsonValueKind.Number ? windDir.GetInt32() : null,
                WindDirectionText = windDir.ValueKind == JsonValueKind.Number ? GetWindDirectionText(windDir.GetInt32()) : null,
                Aqi = aqiValue,
                AqiLevel = aqiValue.HasValue ? GetAqiLevel(aqiValue.Value) : null
            };

            if (data.TryGetProperty("daily", out var daily) && daily.TryGetProperty("time", out var dailyTimes))
            {
                var timeArray = dailyTimes.EnumerateArray().ToArray();
                var weatherCodeDay = daily.GetProperty("weather_code").EnumerateArray().ToArray();
                var maxTemps = daily.GetProperty("temperature_2m_max").EnumerateArray().ToArray();
                var minTemps = daily.GetProperty("temperature_2m_min").EnumerateArray().ToArray();
                var sunrises = daily.GetProperty("sunrise").EnumerateArray().ToArray();
                var sunsets = daily.GetProperty("sunset").EnumerateArray().ToArray();
                var uvMax = daily.TryGetProperty("uv_index_max", out var uvMaxEl) ? uvMaxEl.EnumerateArray().ToArray() : Array.Empty<JsonElement>();

                for (int i = 0; i < timeArray.Length; i++)
                {
                    var forecast = new DailyForecast
                    {
                        Date = DateTime.Parse(timeArray[i].GetString()!),
                        WeatherCodeDay = weatherCodeDay[i].GetInt32(),
                        WeatherCodeNight = weatherCodeDay[i].GetInt32(),
                        High = new TemperatureValue { Value = maxTemps[i].GetDouble(), Unit = "°C" },
                        Low = new TemperatureValue { Value = minTemps[i].GetDouble(), Unit = "°C" },
                        Sunrise = DateTime.Parse(sunrises[i].GetString()!),
                        Sunset = DateTime.Parse(sunsets[i].GetString()!)
                    };

                    if (i < uvMax.Length && uvMax[i].ValueKind == JsonValueKind.Number)
                        forecast.UvIndexMax = uvMax[i].GetDouble();

                    info.ForecastDaily.Add(forecast);
                }
            }

            if (data.TryGetProperty("hourly", out var hourly) && hourly.TryGetProperty("time", out var hourlyTimes))
            {
                var timeArray = hourlyTimes.EnumerateArray().ToArray();
                var hourlyCodes = hourly.GetProperty("weather_code").EnumerateArray().ToArray();
                var hourlyTemps = hourly.GetProperty("temperature_2m").EnumerateArray().ToArray();
                var hourlyPressure = hourly.TryGetProperty("surface_pressure", out var hourlyPressureEl) ? hourlyPressureEl.EnumerateArray().ToArray() : Array.Empty<JsonElement>();
                var hourlyUv = hourly.TryGetProperty("uv_index", out var hourlyUvEl) ? hourlyUvEl.EnumerateArray().ToArray() : Array.Empty<JsonElement>();

                for (int i = 0; i < timeArray.Length; i++)
                {
                    var forecast = new HourlyForecast
                    {
                        Time = DateTime.Parse(timeArray[i].GetString()!),
                        WeatherCode = hourlyCodes[i].GetInt32(),
                        Temperature = new TemperatureValue { Value = hourlyTemps[i].GetDouble(), Unit = "°C" }
                    };

                    if (i < hourlyPressure.Length && hourlyPressure[i].ValueKind == JsonValueKind.Number)
                        forecast.Pressure = hourlyPressure[i].GetDouble();
                    if (i < hourlyUv.Length && hourlyUv[i].ValueKind == JsonValueKind.Number)
                        forecast.UvIndex = hourlyUv[i].GetDouble();

                    info.ForecastHourly.Add(forecast);
                }
            }

            if (aqiValue.HasValue)
            {
                info.Aqi = new AirQuality
                {
                    Aqi = aqiValue.Value,
                    Level = GetAqiLevel(aqiValue.Value),
                    Description = GetAqiDescription(aqiValue.Value)
                };
            }
        }
        catch (Exception ex)
        {
            // ignored - return partial info
        }

        return info;
    }

    public async Task<List<CitySearchResult>> SearchCityAsync(string name)
    {
        var results = new List<CitySearchResult>();
        if (string.IsNullOrWhiteSpace(name)) return results;

        try
        {
            var uri = $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(name)}&count=10&language=zh&format=json";
            var json = await _httpClient.GetStringAsync(uri);
            var doc = JsonSerializer.Deserialize<JsonElement>(json);

            if (doc.TryGetProperty("results", out var cities))
            {
                foreach (var city in cities.EnumerateArray())
                {
                    results.Add(new CitySearchResult
                    {
                        CityId = city.TryGetProperty("id", out var id) ? id.GetInt64().ToString() : "",
                        Name = city.GetProperty("name").GetString() ?? "",
                        Province = city.TryGetProperty("admin1", out var admin1) ? admin1.GetString() : null,
                        District = city.TryGetProperty("admin2", out var admin2) ? admin2.GetString() : null,
                        Longitude = city.GetProperty("longitude").GetDouble(),
                        Latitude = city.GetProperty("latitude").GetDouble()
                    });
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to search city");
            AppLog.WriteException("WeatherService.SearchCityAsync", ex);
        }

        return results;
    }

    public string GetWeatherText(int code) => OpenMeteoWeatherCode.GetText(code);

    private static string GetWindDirectionText(int degree)
    {
        var directions = new[] { "北", "东北", "东", "东南", "南", "西南", "西", "西北" };
        var index = (int)Math.Round(degree / 45.0) % 8;
        return directions[index];
    }

    private static string GetAqiLevel(int aqi) => aqi switch
    {
        <= 50 => "优",
        <= 100 => "良",
        <= 150 => "轻度污染",
        <= 200 => "中度污染",
        <= 300 => "重度污染",
        _ => "严重污染"
    };

    private static string GetAqiDescription(int aqi) => GetAqiLevel(aqi);

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _updateTimer.Start();
        _ = RefreshAsync();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _updateTimer.Stop();
        return Task.CompletedTask;
    }
}
