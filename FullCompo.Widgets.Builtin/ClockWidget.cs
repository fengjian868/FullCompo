using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompo.Widgets.Builtin;

public class ClockWidget : WidgetBase
{
    public override string Id => "builtin.clock";
    public override string Name => "时钟";
    public override string Description => "显示当前时间";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "clock-medium", Name = "中", Type = WidgetSizeType.Medium, Columns = 2, Rows = 2 }
    };

    public override Control CreateView(WidgetContext context)
    {
        var textBlock = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 24,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(Colors.White),
            Text = DateTime.Now.ToString("HH:mm:ss")
        };

        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        timer.Tick += (_, _) => textBlock.Text = DateTime.Now.ToString("HH:mm:ss");
        timer.Start();

        return new Border
        {
            Background = new SolidColorBrush(Color.Parse("#55FFFFFF")),
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(12),
            Child = textBlock
        };
    }
}
