using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompo.Widgets.Builtin;

public class DateWidget : WidgetBase
{
    public override string Id => "builtin.date";
    public override string Name => "日期";
    public override string Description => "显示当前日期和星期";

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

        var dateText = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            FontSize = 16,
            FontWeight = FontWeight.SemiBold,
            Text = DateTime.Now.ToString("MM/dd")
        };

        var weekText = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            FontSize = 13,
            Text = DateTime.Now.ToString("ddd")
        };

        stack.Children.Add(dateText);
        stack.Children.Add(weekText);

        var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        timer.Tick += (_, _) =>
        {
            dateText.Text = DateTime.Now.ToString("MM/dd");
            weekText.Text = DateTime.Now.ToString("ddd");
        };
        timer.Start();

        return stack;
    }
}
