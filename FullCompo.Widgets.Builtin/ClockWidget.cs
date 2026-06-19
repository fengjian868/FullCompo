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
        var isLarge = context.CurrentSize.Rows >= 2;
        var fontSize = isLarge ? 36 : 20;

        var textBlock = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = fontSize,
            FontWeight = FontWeight.Bold,
            Text = DateTime.Now.ToString("HH:mm:ss")
        };

        var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        timer.Tick += (_, _) => textBlock.Text = DateTime.Now.ToString("HH:mm:ss");
        timer.Start();

        return textBlock;
    }
}
