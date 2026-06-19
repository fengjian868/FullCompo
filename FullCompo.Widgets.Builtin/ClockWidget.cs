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
        new WidgetSize { Id = "small-square", Name = "小方", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中横条", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 200, Height = 100 },
        new WidgetSize { Id = "medium-square", Name = "中方", Type = WidgetSizeType.Medium, Columns = 2, Rows = 2, Width = 140, Height = 140 },
        new WidgetSize { Id = "large-square", Name = "大方", Type = WidgetSizeType.Large, Columns = 3, Rows = 3, Width = 220, Height = 220 }
    };

    public override Control CreateView(WidgetContext context)
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
            Text = DateTime.Now.ToString("HH:mm")
        };

        var secondsText = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = isLarge ? 18 : 12,
            FontWeight = FontWeight.Normal,
            Foreground = accent,
            Text = DateTime.Now.ToString("ss"),
            Opacity = 0.9
        };

        var dateText = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = dateFontSize,
            Foreground = secondaryForeground,
            Text = DateTime.Now.ToString("MM月dd日 ddd")
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
            panel.Children.Add(secondsText);
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

        var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        timer.Tick += (_, _) =>
        {
            var now = DateTime.Now;
            timeText.Text = now.ToString("HH:mm");
            secondsText.Text = now.ToString("ss");
            dateText.Text = now.ToString("MM月dd日 ddd");
        };
        timer.Start();

        return root;
    }
}
