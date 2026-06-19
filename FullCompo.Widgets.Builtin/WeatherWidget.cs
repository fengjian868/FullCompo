using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompo.Widgets.Builtin;

public class WeatherWidget : WidgetBase
{
    public override string Id => "builtin.weather";
    public override string Name => "天气";
    public override string Description => "显示当前天气";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小方形", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-square", Name = "中方形", Type = WidgetSizeType.Medium, Columns = 2, Rows = 2, Width = 140, Height = 140 },
        new WidgetSize { Id = "medium-hbar", Name = "中横条", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 200, Height = 100 }
    };

    public override Control CreateView(WidgetContext context)
    {
        var stack = new StackPanel
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Spacing = 4
        };

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");

        var iconText = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            FontSize = 28,
            Foreground = secondaryForeground,
            Text = "☁️"
        };

        var tempText = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            FontSize = 16,
            FontWeight = FontWeight.SemiBold,
            Foreground = foreground,
            Text = "27°C"
        };

        stack.Children.Add(iconText);
        stack.Children.Add(tempText);

        return stack;
    }
}
