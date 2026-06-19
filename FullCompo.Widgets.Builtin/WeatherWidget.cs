using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using FullCompo.Core.Abstractions.Services;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;
using FullCompo.Shared.Models.Weather;

namespace FullCompo.Widgets.Builtin;

public class WeatherWidget : WidgetBase
{
    public override string Id => "builtin.weather";
    public override string Name => "天气";
    public override string Description => "显示当前天气";
    public override bool HasCustomBackground => true;

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小方", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中横条", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 200, Height = 100 },
        new WidgetSize { Id = "medium-square", Name = "中方", Type = WidgetSizeType.Medium, Columns = 2, Rows = 2, Width = 140, Height = 140 },
        new WidgetSize { Id = "large-square", Name = "大方", Type = WidgetSizeType.Large, Columns = 3, Rows = 3, Width = 220, Height = 220 }
    };

    public override Control CreateView(WidgetContext context)
    {
        var weatherService = context.GetService<IWeatherService>();
        var isLarge = context.CurrentSize.Rows >= 2;
        var isWide = context.CurrentSize.Columns >= 2 && context.CurrentSize.Rows == 1;

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
            Text = "☁️"
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

        var aqiBadge = new Border
        {
            CornerRadius = new CornerRadius(10),
            Padding = new Thickness(8, 2, 8, 2),
            Background = new SolidColorBrush(Colors.White, 0.2),
            Child = new TextBlock
            {
                FontSize = 10,
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center
            },
            VerticalAlignment = VerticalAlignment.Center
        };

        var rootCard = new Border
        {
            Background = GetDefaultBackground(),
            CornerRadius = new CornerRadius(20),
            Padding = new Thickness(14),
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch
        };

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
            infoPanel.Children.Add(aqiBadge);
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

        rootCard.Child = contentGrid;

        void UpdateView(WeatherInfo? info)
        {
            if (info == null)
            {
                cityText.Text = context.Settings.GetValue<string>("cityName", "") ?? "";
                tempText.Text = "--";
                unitText.Text = "°";
                conditionText.Text = "未获取";
                tipText.Text = "检查网络或城市设置";
                aqiBadge.IsVisible = false;
                return;
            }

            var code = info.Current.WeatherCode;
            cityText.Text = _configService?.AppSettings.WeatherCityName ?? context.Settings.GetValue<string>("cityName", "") ?? "";
            iconText.Text = WeatherCodeToIcon(code);
            tempText.Text = ((int)info.Current.Temperature.Value).ToString();
            unitText.Text = info.Current.Temperature.Unit;
            conditionText.Text = info.Current.WeatherText;
            tipText.Text = BuildTip(code, info.Current);
            rootCard.Background = GetWeatherBackground(code);

            if (info.Current.Aqi.HasValue)
            {
                ((TextBlock)aqiBadge.Child!).Text = $"AQI {info.Current.Aqi} {info.Current.AqiLevel}";
                aqiBadge.IsVisible = true;
            }
            else if (info.Aqi != null)
            {
                ((TextBlock)aqiBadge.Child!).Text = $"AQI {info.Aqi.Aqi} {info.Aqi.Description}";
                aqiBadge.IsVisible = true;
            }
            else
            {
                aqiBadge.IsVisible = false;
            }
        }

        UpdateView(weatherService?.LastWeatherInfo);

        if (weatherService != null)
        {
            weatherService.WeatherUpdated += (_, info) =>
            {
                Dispatcher.UIThread.Post(() => UpdateView(info));
            };
        }

        return rootCard;
    }

    private IConfigService? _configService;

    public override void OnActivated(WidgetContext context)
    {
        _configService = context.GetService<IConfigService>();
        base.OnActivated(context);
    }

    private static string WeatherCodeToIcon(int code)
    {
        return code switch
        {
            0 => "☀️",
            1 => "⛅",
            2 => "☁️",
            >= 3 and <= 5 => "⛈️",
            6 => "🌨️",
            >= 7 and <= 12 => "🌧️",
            >= 13 and <= 17 => "❄️",
            18 => "🌫️",
            19 => "🌧️",
            >= 20 and <= 31 => "🌪️",
            >= 53 => "😷",
            _ => "🌤️"
        };
    }

    private static string BuildTip(int code, CurrentWeather current)
    {
        var tips = new List<string>();

        if (current.Humidity > 0)
            tips.Add($"湿度 {current.Humidity}%");

        if (current.PrecipitationDistanceKm.HasValue)
        {
            var d = current.PrecipitationDistanceKm.Value;
            tips.Add(d <= 0 ? "正在下雨" : $"最近的雨在 {d} 千米之外");
        }

        if (!string.IsNullOrWhiteSpace(current.WindDirection) && current.WindLevel.HasValue)
            tips.Add($"{current.WindDirection} {current.WindLevel}级");

        tips.Add(code switch
        {
            0 => "注意防晒",
            1 => "适合出行",
            2 => "天气阴沉",
            >= 3 and <= 5 => "注意防雷",
            >= 7 and <= 12 => "记得带伞",
            >= 13 and <= 17 => "注意保暖",
            18 => "能见度低",
            >= 53 => "减少外出",
            _ => "出行愉快"
        });

        return string.Join(" · ", tips);
    }

    private static IBrush GetDefaultBackground()
    {
        return new LinearGradientBrush
        {
            StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
            EndPoint = new RelativePoint(1, 1, RelativeUnit.Relative),
            GradientStops =
            {
                new GradientStop(Color.Parse("#FF4A90E2"), 0),
                new GradientStop(Color.Parse("#FF2D5A9E"), 1)
            }
        };
    }

    private static IBrush GetWeatherBackground(int code)
    {
        var (start, end) = code switch
        {
            0 => ("#FF5B8BD4", "#FFE8A838"),   // 晴：蓝天到暖黄
            1 => ("#FF4A90E2", "#FF7A9BCB"),   // 多云：蓝
            2 => ("#FF5F6D7A", "#FF3A4B5C"),   // 阴：灰蓝
            >= 3 and <= 5 => ("#FF3A4B5C", "#FF6B5B7A"), // 雷阵雨：深灰紫
            >= 7 and <= 12 => ("#FF2C3E50", "#FF4A5F7A"), // 雨：深蓝灰
            >= 13 and <= 17 => ("#FF7DAFD6", "#FFE0E8F0"), // 雪：浅蓝白
            18 => ("#FF6E7F91", "#FF9AA5B1"), // 雾：灰
            >= 20 and <= 31 => ("#FF7D6B54", "#FF9E8A6A"), // 沙尘：土黄
            >= 53 => ("#FF6B6B6B", "#FF8E8E8E"), // 霾：灰
            _ => ("#FF4A90E2", "#FF2D5A9E")
        };

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
