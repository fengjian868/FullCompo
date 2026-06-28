using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompo.Widgets.Builtin.Widgets.Clock;

public class ClockWidget : WidgetBase
{
    public override string Id => "builtin.clock";
    public override string Name => "时钟";
    public override string Description => "显示当前时间，支持模拟、数字和世界时钟";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小方", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中横条", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 200, Height = 100 },
        new WidgetSize { Id = "medium-square", Name = "中方", Type = WidgetSizeType.Medium, Columns = 2, Rows = 2, Width = 140, Height = 140 },
        new WidgetSize { Id = "large-square", Name = "大方", Type = WidgetSizeType.Large, Columns = 3, Rows = 3, Width = 220, Height = 220 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("style", ClockStyle.Digital.ToString());
        settings.SetValue("showSeconds", true);
        settings.SetValue("timeZoneId", "");
        settings.SetValue("label", "");
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var style = ParseStyle(context.Settings.GetValue("style", ClockStyle.Digital.ToString()));
        var showSeconds = context.Settings.GetValue("showSeconds", true);
        var timeZoneId = context.Settings.GetValue("timeZoneId", "") ?? "";
        var label = context.Settings.GetValue("label", "") ?? "";

        var timeZone = GetTimeZone(timeZoneId);

        return style switch
        {
            ClockStyle.AnalogI => CreateAnalogClock(context, timeZone, showSeconds, false),
            ClockStyle.AnalogII => CreateAnalogClock(context, timeZone, showSeconds, true),
            ClockStyle.AnalogIII => CreateMinimalAnalogClock(context, timeZone, showSeconds),
            ClockStyle.World => CreateWorldClock(context, timeZone, label),
            _ => CreateDigitalClock(context, timeZone, showSeconds)
        };
    }

    public override Control? CreateSettingsView(WidgetSettings settings)
    {
        var style = ParseStyle(settings.GetValue("style", ClockStyle.Digital.ToString()));
        var showSeconds = settings.GetValue("showSeconds", true);
        var timeZoneId = settings.GetValue("timeZoneId", "") ?? "";
        var label = settings.GetValue("label", "") ?? "";

        var panel = new StackPanel { Spacing = 8 };

        panel.Children.Add(new TextBlock { Text = "样式" });
        var styleBox = new ComboBox();
        foreach (ClockStyle s in Enum.GetValues<ClockStyle>())
        {
            styleBox.Items.Add(s.ToString());
        }
        styleBox.SelectedItem = style.ToString();
        styleBox.SelectionChanged += (_, _) =>
        {
            if (styleBox.SelectedItem is string selected)
                settings.SetValue("style", selected);
        };
        panel.Children.Add(styleBox);

        var secondsCheck = new CheckBox
        {
            Content = "显示秒数",
            IsChecked = showSeconds
        };
        secondsCheck.IsCheckedChanged += (_, _) => settings.SetValue("showSeconds", secondsCheck.IsChecked == true);
        panel.Children.Add(secondsCheck);

        panel.Children.Add(new TextBlock { Text = "时区 ID（如 Asia/Shanghai、UTC，留空使用本地）" });
        var timeZoneBox = new TextBox
        {
            Text = timeZoneId,
            Watermark = "时区 ID"
        };
        timeZoneBox.TextChanged += (_, _) => settings.SetValue("timeZoneId", timeZoneBox.Text ?? "");
        panel.Children.Add(timeZoneBox);

        panel.Children.Add(new TextBlock { Text = "标签（世界时钟用）" });
        var labelBox = new TextBox
        {
            Text = label,
            Watermark = "城市名"
        };
        labelBox.TextChanged += (_, _) => settings.SetValue("label", labelBox.Text ?? "");
        panel.Children.Add(labelBox);

        return panel;
    }

    private static ClockStyle ParseStyle(string? value)
    {
        if (Enum.TryParse<ClockStyle>(value, out var style))
            return style;
        return ClockStyle.Digital;
    }

    private static TimeZoneInfo GetTimeZone(string timeZoneId)
    {
        if (string.IsNullOrWhiteSpace(timeZoneId))
            return TimeZoneInfo.Local;

        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }
        catch
        {
            return TimeZoneInfo.Local;
        }
    }

    private static DateTime GetTime(TimeZoneInfo timeZone) =>
        TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);

    private static Control CreateDigitalClock(WidgetContext context, TimeZoneInfo timeZone, bool showSeconds)
    {
        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isLarge = context.CurrentSize.Rows >= 2;
        var isWide = context.CurrentSize.Columns >= 2 && context.CurrentSize.Rows == 1;

        var timeFontSize = isLarge ? 42 : isWide ? 32 : 20;
        var dateFontSize = isLarge ? 14 : isWide ? 12 : 10;

        var timeText = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = timeFontSize,
            FontWeight = FontWeight.SemiBold,
            Foreground = foreground,
            Text = FormatTime(timeZone, showSeconds)
        };

        var dateText = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = dateFontSize,
            Foreground = secondaryForeground,
            Text = FormatDate(timeZone)
        };

        var root = new Grid
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch
        };

        if (isWide)
        {
            var panel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Spacing = 6
            };
            panel.Children.Add(timeText);
            root.Children.Add(panel);
        }
        else
        {
            var panel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Spacing = 2
            };
            panel.Children.Add(timeText);
            panel.Children.Add(dateText);
            root.Children.Add(panel);
        }

        var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(showSeconds ? 1 : 30) };
        timer.Tick += (_, _) =>
        {
            timeText.Text = FormatTime(timeZone, showSeconds);
            dateText.Text = FormatDate(timeZone);
        };
        timer.Start();

        return root;
    }

    private static Control CreateWorldClock(WidgetContext context, TimeZoneInfo timeZone, string label)
    {
        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isLarge = context.CurrentSize.Rows >= 2;

        var labelText = new TextBlock
        {
            Text = string.IsNullOrWhiteSpace(label) ? timeZone.DisplayName : label,
            FontSize = isLarge ? 16 : 12,
            FontWeight = FontWeight.SemiBold,
            Foreground = accent,
            HorizontalAlignment = HorizontalAlignment.Center,
            TextTrimming = TextTrimming.CharacterEllipsis
        };

        var timeText = new TextBlock
        {
            FontSize = isLarge ? 36 : 22,
            FontWeight = FontWeight.Bold,
            Foreground = foreground,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        var offsetText = new TextBlock
        {
            FontSize = isLarge ? 12 : 10,
            Foreground = secondaryForeground,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        void Update()
        {
            var now = GetTime(timeZone);
            timeText.Text = now.ToString("HH:mm");
            offsetText.Text = $"UTC{(timeZone.BaseUtcOffset >= TimeSpan.Zero ? "+" : "")}{timeZone.BaseUtcOffset.Hours:00}:{timeZone.BaseUtcOffset.Minutes:00}";
        }

        Update();

        var panel = new StackPanel
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Spacing = 2
        };
        panel.Children.Add(labelText);
        panel.Children.Add(timeText);
        panel.Children.Add(offsetText);

        var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(30) };
        timer.Tick += (_, _) => Update();
        timer.Start();

        return panel;
    }

    private static Control CreateAnalogClock(WidgetContext context, TimeZoneInfo timeZone, bool showSeconds, bool minimal)
    {
        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var size = Math.Min(context.CurrentSize.Width, context.CurrentSize.Height);
        var padding = 8;
        var radius = (size - padding * 2) / 2.0;
        var center = size / 2.0;

        var canvas = new Canvas
        {
            Width = size,
            Height = size,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        if (!minimal)
        {
            canvas.Children.Add(new Ellipse
            {
                Width = radius * 2,
                Height = radius * 2,
                Stroke = foreground,
                StrokeThickness = 2,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            });
            Canvas.SetLeft(canvas.Children[^1], center - radius);
            Canvas.SetTop(canvas.Children[^1], center - radius);
        }

        // Ticks
        for (int i = 0; i < 60; i++)
        {
            if (minimal && i % 5 != 0) continue;

            var angle = i * 6 * Math.PI / 180.0 - Math.PI / 2.0;
            var isHour = i % 5 == 0;
            var innerRadius = radius - (isHour ? 8 : 4);
            var outerRadius = radius - 2;

            var tick = new Line
            {
                StartPoint = new Point(center + Math.Cos(angle) * innerRadius, center + Math.Sin(angle) * innerRadius),
                EndPoint = new Point(center + Math.Cos(angle) * outerRadius, center + Math.Sin(angle) * outerRadius),
                Stroke = isHour ? accent : foreground,
                StrokeThickness = isHour ? 2 : 1
            };
            canvas.Children.Add(tick);
        }

        // Numbers for AnalogI
        if (!minimal)
        {
            for (int i = 1; i <= 12; i++)
            {
                var angle = i * 30 * Math.PI / 180.0 - Math.PI / 2.0;
                var numRadius = radius - 18;
                var num = new TextBlock
                {
                    Text = i.ToString(),
                    FontSize = Math.Max(8, radius / 7),
                    Foreground = foreground,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                canvas.Children.Add(num);
                Canvas.SetLeft(num, center + Math.Cos(angle) * numRadius - radius / 12);
                Canvas.SetTop(num, center + Math.Sin(angle) * numRadius - radius / 12);
            }
        }

        var hourHand = CreateHand(foreground, radius * 0.5, 4, center);
        var minuteHand = CreateHand(foreground, radius * 0.75, 3, center);
        var secondHand = showSeconds ? CreateHand(accent, radius * 0.85, 2, center) : null;

        canvas.Children.Add(hourHand);
        canvas.Children.Add(minuteHand);
        if (secondHand != null)
            canvas.Children.Add(secondHand);

        void Update()
        {
            var now = GetTime(timeZone);
            var second = now.Second + now.Millisecond / 1000.0;
            var minute = now.Minute + second / 60.0;
            var hour = now.Hour % 12 + minute / 60.0;

            SetHandAngle(hourHand, hour * 30 - 90, center);
            SetHandAngle(minuteHand, minute * 6 - 90, center);
            if (secondHand != null)
                SetHandAngle(secondHand, second * 6 - 90, center);
        }

        Update();

        var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(showSeconds ? 100 : 1000) };
        timer.Tick += (_, _) => Update();
        timer.Start();

        return canvas;
    }

    private static Control CreateMinimalAnalogClock(WidgetContext context, TimeZoneInfo timeZone, bool showSeconds)
    {
        return CreateAnalogClock(context, timeZone, showSeconds, true);
    }

    private static Line CreateHand(IBrush stroke, double length, double thickness, double center)
    {
        return new Line
        {
            StartPoint = new Point(center, center),
            EndPoint = new Point(center, center - length),
            Stroke = stroke,
            StrokeThickness = thickness,
            StrokeLineCap = PenLineCap.Round
        };
    }

    private static void SetHandAngle(Line hand, double angleDegrees, double center)
    {
        var angle = angleDegrees * Math.PI / 180.0;
        var length = Math.Sqrt(Math.Pow(hand.EndPoint.X - center, 2) + Math.Pow(hand.EndPoint.Y - center, 2));
        hand.EndPoint = new Point(center + Math.Cos(angle) * length, center + Math.Sin(angle) * length);
    }

    private static string FormatTime(TimeZoneInfo timeZone, bool showSeconds)
    {
        var now = GetTime(timeZone);
        return showSeconds ? now.ToString("HH:mm:ss") : now.ToString("HH:mm");
    }

    private static string FormatDate(TimeZoneInfo timeZone)
    {
        var now = GetTime(timeZone);
        return now.ToString("MM月dd日 ddd");
    }
}
