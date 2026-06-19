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
        new WidgetSize { Id = "1x1", Name = "方形", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 120, Height = 120 },
        new WidgetSize { Id = "2x1", Name = "横条", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 248, Height = 120 }
    };

    public override Control CreateView(WidgetContext context)
    {
        var stack = new StackPanel
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Spacing = 4
        };

        var iconText = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            FontSize = 28,
            Text = "☁️"
        };

        var tempText = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            FontSize = 16,
            FontWeight = FontWeight.SemiBold,
            Text = "27°C"
        };

        stack.Children.Add(iconText);
        stack.Children.Add(tempText);

        return stack;
    }
}
