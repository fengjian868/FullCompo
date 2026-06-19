using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompo.Widgets.Builtin;

public class PlaceholderWidget : WidgetBase
{
    public override string Id => "builtin.placeholder";
    public override string Name => "空组件";
    public override string Description => "点击选择功能";

    private readonly WidgetSize _size;

    public PlaceholderWidget(WidgetSize size)
    {
        _size = size;
    }

    public override IEnumerable<WidgetSize> SupportedSizes => new[] { _size };

    public override Control CreateView(WidgetContext context)
    {
        return new TextBlock
        {
            Text = "+",
            FontSize = 32,
            Foreground = new SolidColorBrush(Colors.Gray),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
    }
}
