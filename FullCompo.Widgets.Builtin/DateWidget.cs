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
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
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

        var dateText = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            FontSize = context.CurrentSize.Width >= 140 ? 20 : 14,
            FontWeight = FontWeight.SemiBold,
            Foreground = foreground,
            Text = DateTime.Now.ToString("MM/dd")
        };

        var weekText = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            FontSize = context.CurrentSize.Width >= 140 ? 14 : 11,
            Foreground = secondaryForeground,
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
