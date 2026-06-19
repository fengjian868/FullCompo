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
        new WidgetSize { Id = "small-square", Name = "小方", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "mini-bar", Name = "迷你条", Type = WidgetSizeType.Small, Columns = 2, Rows = 1, Width = 100, Height = 40 },
        new WidgetSize { Id = "small-hbar", Name = "小长条", Type = WidgetSizeType.Small, Columns = 2, Rows = 1, Width = 180, Height = 40 },
        new WidgetSize { Id = "medium-hbar", Name = "中横条", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 200, Height = 100 },
        new WidgetSize { Id = "medium-square", Name = "中方", Type = WidgetSizeType.Medium, Columns = 2, Rows = 2, Width = 140, Height = 140 }
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
            FontSize = context.CurrentSize.Width >= 140 ? 20 : 14,
            FontWeight = FontWeight.SemiBold,
            Text = DateTime.Now.ToString("MM/dd")
        };

        var weekText = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            FontSize = context.CurrentSize.Width >= 140 ? 14 : 11,
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
