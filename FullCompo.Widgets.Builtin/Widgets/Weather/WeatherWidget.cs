using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using FullCompo.Core.Abstractions.Services;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;
using FullCompo.Shared.Models.Weather;

namespace FullCompo.Widgets.Builtin.Widgets.Weather;

public class WeatherWidget : WidgetBase
{
    public override string Id => "builtin.weather";
    public override string Name => "天气";
    public override string Description => "显示天气信息，支持多种视图";
    public override bool HasCustomBackground => true;

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小方", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中横条", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 200, Height = 100 },
        new WidgetSize { Id = "large-hbar", Name = "大横条", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 160 }
    };

    private IConfigService? _configService;

    public override void OnActivated(WidgetContext context)
    {
        _configService = context.GetService<IConfigService>();
        base.OnActivated(context);
    }

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("viewMode", WeatherViewMode.Current.ToString());
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var weatherService = context.GetService<IWeatherService>();
        var viewMode = ParseViewMode(context.Settings.GetValue("viewMode", WeatherViewMode.Current.ToString()));

        var info = weatherService?.LastWeatherInfo;

        if (info == null)
        {
            return CreatePlaceholder("未获取天气");
        }

        return viewMode switch
        {
            WeatherViewMode.Forecast => CreateForecastView(context, info),
            WeatherViewMode.Temperature => CreateTemperatureView(context, info),
            WeatherViewMode.UV => CreateUvView(context, info),
            WeatherViewMode.SunriseSunset => CreateSunriseSunsetView(context, info),
            WeatherViewMode.Pressure => CreatePressureView(context, info),
            WeatherViewMode.AirQuality => CreateAirQualityView(context, info),
            _ => CreateCurrentView(context, info)
        };
    }

    public override Control? CreateSettingsView(WidgetSettings settings)
    {
        var current = ParseViewMode(settings.GetValue("viewMode", WeatherViewMode.Current.ToString()));

        var panel = new StackPanel { Spacing = 8 };
        panel.Children.Add(new TextBlock { Text = "视图模式" });

        var combo = new ComboBox();
        foreach (WeatherViewMode mode in Enum.GetValues<WeatherViewMode>())
        {
            combo.Items.Add(mode.ToString());
        }
        combo.SelectedItem = current.ToString();
        combo.SelectionChanged += (_, _) =>
        {
            if (combo.SelectedItem is string selected)
                settings.SetValue("viewMode", selected);
        };

        panel.Children.Add(combo);
        return panel;
    }

    private static WeatherViewMode ParseViewMode(string? value)
    {
        if (Enum.TryParse<WeatherViewMode>(value, out var mode))
            return mode;
        return WeatherViewMode.Current;
    }

    private static Control CreateCard(Control content, IBrush background)
    {
        return new Border
        {
            Background = background,
            CornerRadius = new CornerRadius(20),
            Padding = new Thickness(14),
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            Child = content
        };
    }

    private Control CreateCurrentView(WidgetContext context, WeatherInfo info)
    {
        var isLarge = context.CurrentSize.Rows >= 2;
        var isWide = context.CurrentSize.Columns >= 2 && context.CurrentSize.Rows == 1;

        var current = info.Current;
        var (start, end) = OpenMeteoWeatherCode.GetGradient(current.WeatherCode, current.IsDay);
        var background = CreateGradientBrush(start, end);

        var cityText = new TextBlock
        {
            FontSize = isLarge ? 15 : 12,
            FontWeight = FontWeight.SemiBold,
            Foreground = Brushes.White,
            TextTrimming = TextTrimming.CharacterEllipsis,
            VerticalAlignment = VerticalAlignment.Center
        };

        var iconText = new TextBlock
        {
            FontSize = isLarge ? 56 : isWide ? 40 : 32,
            Foreground = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            Text = OpenMeteoWeatherCode.GetIcon(current.WeatherCode, current.IsDay)
        };

        var tempText = new TextBlock
        {
            FontSize = isLarge ? 40 : isWide ? 32 : 24,
            FontWeight = FontWeight.Light,
            Foreground = Brushes.White,
            VerticalAlignment = VerticalAlignment.Top
        };

        var unitText = new TextBlock
        {
            FontSize = isLarge ? 18 : 12,
            Foreground = Brushes.White,
            Opacity = 0.85,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(2, isLarge ? 6 : 2, 0, 0)
        };

        var conditionText = new TextBlock
        {
            FontSize = isLarge ? 14 : isWide ? 13 : 11,
            Foreground = Brushes.White,
            Opacity = 0.9,
            TextTrimming = TextTrimming.CharacterEllipsis,
            VerticalAlignment = VerticalAlignment.Center
        };

        var tipText = new TextBlock
        {
            FontSize = isLarge ? 12 : isWide ? 11 : 10,
            Foreground = Brushes.White,
            Opacity = 0.8,
            TextWrapping = TextWrapping.Wrap,
            TextTrimming = TextTrimming.CharacterEllipsis,
            MaxLines = 2,
            VerticalAlignment = VerticalAlignment.Center
        };

        void Update()
        {
            cityText.Text = _configService?.AppSettings.WeatherCityName ?? "";
            tempText.Text = ((int)current.Temperature.Value).ToString();
            unitText.Text = current.Temperature.Unit;
            conditionText.Text = current.WeatherText;
            tipText.Text = BuildCurrentTip(current);
        }

        Update();

        Grid contentGrid;
        if (isWide)
        {
            contentGrid = new Grid
            {
                ColumnDefinitions = new ColumnDefinitions("Auto,*,Auto"),
                RowDefinitions = new RowDefinitions("Auto,*")
            };

            var headerPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Spacing = 6
            };
            headerPanel.Children.Add(cityText);
            Grid.SetColumnSpan(headerPanel, 2);
            contentGrid.Children.Add(headerPanel);

            var iconPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Spacing = 8,
                VerticalAlignment = VerticalAlignment.Center
            };
            iconPanel.Children.Add(iconText);
            var tempPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center
            };
            tempPanel.Children.Add(tempText);
            tempPanel.Children.Add(unitText);
            iconPanel.Children.Add(tempPanel);
            Grid.SetRow(iconPanel, 1);
            contentGrid.Children.Add(iconPanel);

            var infoPanel = new StackPanel
            {
                Spacing = 2,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            infoPanel.Children.Add(conditionText);
            infoPanel.Children.Add(tipText);
            Grid.SetRow(infoPanel, 1);
            Grid.SetColumn(infoPanel, 1);
            Grid.SetColumnSpan(infoPanel, 2);
            contentGrid.Children.Add(infoPanel);
        }
        else
        {
            contentGrid = new Grid
            {
                RowDefinitions = new RowDefinitions("Auto,*,Auto,Auto")
            };
            contentGrid.Children.Add(cityText);

            var iconPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Spacing = 6,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            iconPanel.Children.Add(iconText);
            var tempPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center
            };
            tempPanel.Children.Add(tempText);
            tempPanel.Children.Add(unitText);
            iconPanel.Children.Add(tempPanel);
            Grid.SetRow(iconPanel, 1);
            contentGrid.Children.Add(iconPanel);

            Grid.SetRow(conditionText, 2);
            contentGrid.Children.Add(conditionText);

            Grid.SetRow(tipText, 3);
            contentGrid.Children.Add(tipText);
        }

        return CreateCard(contentGrid, background);
    }

    private Control CreateForecastView(WidgetContext context, WeatherInfo info)
    {
        var (start, end) = OpenMeteoWeatherCode.GetGradient(info.Current.WeatherCode, info.Current.IsDay);
        var background = CreateGradientBrush(start, end);

        var panel = new StackPanel
        {
            Spacing = 4,
            VerticalAlignment = VerticalAlignment.Stretch
        };

        panel.Children.Add(new TextBlock
        {
            Text = "未来预报",
            FontSize = 14,
            FontWeight = FontWeight.SemiBold,
            Foreground = Brushes.White
        });

        var list = new StackPanel { Spacing = 2 };
        foreach (var day in info.ForecastDaily.Take(context.CurrentSize.Rows >= 2 ? 5 : 3))
        {
            var row = new Grid
            {
                ColumnDefinitions = new ColumnDefinitions("Auto,*,Auto,Auto")
            };
            row.Children.Add(new TextBlock { Text = day.Date.ToString("MM/dd"), Foreground = Brushes.White, FontSize = 11, VerticalAlignment = VerticalAlignment.Center });
            var icon = new TextBlock { Text = OpenMeteoWeatherCode.GetIcon(day.WeatherCodeDay), FontSize = 14, HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetColumn(icon, 1);
            row.Children.Add(icon);
            var temp = new TextBlock { Text = $"{day.Low.Value:F0}° / {day.High.Value:F0}°", Foreground = Brushes.White, FontSize = 11, HorizontalAlignment = HorizontalAlignment.Right };
            Grid.SetColumn(temp, 2);
            Grid.SetColumnSpan(temp, 2);
            row.Children.Add(temp);
            list.Children.Add(row);
        }

        panel.Children.Add(list);
        return CreateCard(panel, background);
    }

    private Control CreateTemperatureView(WidgetContext context, WeatherInfo info)
    {
        var (start, end) = OpenMeteoWeatherCode.GetGradient(info.Current.WeatherCode, info.Current.IsDay);
        var background = CreateGradientBrush(start, end);

        var panel = new StackPanel
        {
            Spacing = 4,
            VerticalAlignment = VerticalAlignment.Stretch
        };

        panel.Children.Add(new TextBlock
        {
            Text = "24 小时温度",
            FontSize = 14,
            FontWeight = FontWeight.SemiBold,
            Foreground = Brushes.White
        });

        var list = new StackPanel { Spacing = 2 };
        var now = DateTime.Now;
        foreach (var hour in info.ForecastHourly.Where(h => h.Time >= now).Take(context.CurrentSize.Rows >= 2 ? 12 : 6))
        {
            var row = new Grid
            {
                ColumnDefinitions = new ColumnDefinitions("Auto,*,Auto")
            };
            row.Children.Add(new TextBlock { Text = hour.Time.ToString("HH:mm"), Foreground = Brushes.White, FontSize = 11 });
            var icon = new TextBlock { Text = OpenMeteoWeatherCode.GetIcon(hour.WeatherCode), FontSize = 12, HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetColumn(icon, 1);
            row.Children.Add(icon);
            var temp = new TextBlock { Text = $"{hour.Temperature.Value:F0}°", Foreground = Brushes.White, FontSize = 11, HorizontalAlignment = HorizontalAlignment.Right };
            Grid.SetColumn(temp, 2);
            row.Children.Add(temp);
            list.Children.Add(row);
        }

        panel.Children.Add(list);
        return CreateCard(panel, background);
    }

    private Control CreateUvView(WidgetContext context, WeatherInfo info)
    {
        var (start, end) = OpenMeteoWeatherCode.GetGradient(info.Current.WeatherCode, info.Current.IsDay);
        var background = CreateGradientBrush(start, end);

        var panel = new StackPanel
        {
            Spacing = 4,
            VerticalAlignment = VerticalAlignment.Stretch
        };

        panel.Children.Add(new TextBlock
        {
            Text = "紫外线指数",
            FontSize = 14,
            FontWeight = FontWeight.SemiBold,
            Foreground = Brushes.White
        });

        var currentUv = info.ForecastHourly
            .Where(h => h.Time <= DateTime.Now)
            .OrderByDescending(h => h.Time)
            .FirstOrDefault(h => h.UvIndex.HasValue)?.UvIndex;

        panel.Children.Add(new TextBlock
        {
            Text = currentUv.HasValue ? $"当前 UV {currentUv.Value:F1}" : "当前无数据",
            FontSize = 20,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.White
        });

        var list = new StackPanel { Spacing = 2 };
        foreach (var day in info.ForecastDaily.Take(context.CurrentSize.Rows >= 2 ? 5 : 3))
        {
            if (!day.UvIndexMax.HasValue) continue;
            list.Children.Add(new TextBlock
            {
                Text = $"{day.Date:MM/dd} 最高 {day.UvIndexMax.Value:F1}",
                Foreground = Brushes.White,
                FontSize = 11
            });
        }

        panel.Children.Add(list);
        return CreateCard(panel, background);
    }

    private Control CreateSunriseSunsetView(WidgetContext context, WeatherInfo info)
    {
        var (start, end) = OpenMeteoWeatherCode.GetGradient(info.Current.WeatherCode, info.Current.IsDay);
        var background = CreateGradientBrush(start, end);

        var today = info.ForecastDaily.FirstOrDefault();

        var panel = new StackPanel
        {
            Spacing = 6,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        panel.Children.Add(new TextBlock
        {
            Text = "日出日落",
            FontSize = 14,
            FontWeight = FontWeight.SemiBold,
            Foreground = Brushes.White,
            HorizontalAlignment = HorizontalAlignment.Center
        });

        if (today != null)
        {
            panel.Children.Add(new TextBlock
            {
                Text = $"🌅 日出 {today.Sunrise:HH:mm}",
                FontSize = 16,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            });
            panel.Children.Add(new TextBlock
            {
                Text = $"🌇 日落 {today.Sunset:HH:mm}",
                FontSize = 16,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            });
        }
        else
        {
            panel.Children.Add(new TextBlock { Text = "无数据", Foreground = Brushes.White });
        }

        return CreateCard(panel, background);
    }

    private Control CreatePressureView(WidgetContext context, WeatherInfo info)
    {
        var (start, end) = OpenMeteoWeatherCode.GetGradient(info.Current.WeatherCode, info.Current.IsDay);
        var background = CreateGradientBrush(start, end);

        var panel = new StackPanel
        {
            Spacing = 4,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        panel.Children.Add(new TextBlock
        {
            Text = "气压",
            FontSize = 14,
            FontWeight = FontWeight.SemiBold,
            Foreground = Brushes.White,
            HorizontalAlignment = HorizontalAlignment.Center
        });

        panel.Children.Add(new TextBlock
        {
            Text = info.Current.Pressure.HasValue ? $"{info.Current.Pressure.Value:F0} hPa" : "--",
            FontSize = 28,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.White,
            HorizontalAlignment = HorizontalAlignment.Center
        });

        var currentHour = info.ForecastHourly
            .Where(h => h.Time <= DateTime.Now)
            .OrderByDescending(h => h.Time)
            .FirstOrDefault(h => h.Pressure.HasValue);

        if (currentHour != null)
        {
            var prevHour = info.ForecastHourly
                .Where(h => h.Time < currentHour.Time && h.Pressure.HasValue)
                .OrderByDescending(h => h.Time)
                .FirstOrDefault();

            if (prevHour != null)
            {
                var diff = currentHour.Pressure!.Value - prevHour.Pressure!.Value;
                panel.Children.Add(new TextBlock
                {
                    Text = $"{(diff >= 0 ? "↑" : "↓")} {Math.Abs(diff):F1} hPa",
                    FontSize = 12,
                    Foreground = Brushes.White,
                    Opacity = 0.85,
                    HorizontalAlignment = HorizontalAlignment.Center
                });
            }
        }

        return CreateCard(panel, background);
    }

    private Control CreateAirQualityView(WidgetContext context, WeatherInfo info)
    {
        var (start, end) = ("#FF5B8BD4", "#FF3A4B5C");
        var background = CreateGradientBrush(start, end);

        var panel = new StackPanel
        {
            Spacing = 4,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        panel.Children.Add(new TextBlock
        {
            Text = "空气质量",
            FontSize = 14,
            FontWeight = FontWeight.SemiBold,
            Foreground = Brushes.White,
            HorizontalAlignment = HorizontalAlignment.Center
        });

        if (info.Aqi != null)
        {
            panel.Children.Add(new TextBlock
            {
                Text = $"AQI {info.Aqi.Aqi}",
                FontSize = 32,
                FontWeight = FontWeight.Bold,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            });
            panel.Children.Add(new TextBlock
            {
                Text = info.Aqi.Level,
                FontSize = 14,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            });
        }
        else if (info.Current.Aqi.HasValue)
        {
            panel.Children.Add(new TextBlock
            {
                Text = $"AQI {info.Current.Aqi}",
                FontSize = 32,
                FontWeight = FontWeight.Bold,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            });
            panel.Children.Add(new TextBlock
            {
                Text = info.Current.AqiLevel ?? "",
                FontSize = 14,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            });
        }
        else
        {
            panel.Children.Add(new TextBlock
            {
                Text = "无数据",
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            });
        }

        return CreateCard(panel, background);
    }

    private static Control CreatePlaceholder(string message)
    {
        return new Border
        {
            Background = CreateGradientBrush("#FF4A90E2", "#FF2D5A9E"),
            CornerRadius = new CornerRadius(20),
            Padding = new Thickness(14),
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            Child = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            }
        };
    }

    private static string BuildCurrentTip(CurrentWeather current)
    {
        var tips = new List<string>();

        if (current.Humidity > 0)
            tips.Add($"湿度 {current.Humidity}%");

        if (current.WindSpeed.HasValue && !string.IsNullOrEmpty(current.WindDirectionText))
            tips.Add($"{current.WindDirectionText} {current.WindSpeed.Value:F0} km/h");

        if (current.Pressure.HasValue)
            tips.Add($"气压 {current.Pressure.Value:F0} hPa");

        if (current.ApparentTemperature.HasValue)
            tips.Add($"体感 {current.ApparentTemperature.Value:F0}°");

        if (tips.Count == 0)
            tips.Add(current.WeatherText);

        return string.Join(" · ", tips);
    }

    private static IBrush CreateGradientBrush(string start, string end)
    {
        return new LinearGradientBrush
        {
            StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
            EndPoint = new RelativePoint(1, 1, RelativeUnit.Relative),
            GradientStops =
            {
                new GradientStop(Color.Parse(start), 0),
                new GradientStop(Color.Parse(end), 1)
            }
        };
    }
}
