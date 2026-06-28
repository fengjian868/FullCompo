using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompo.Widgets.Builtin;

public class NoteWidget : WidgetBase
{
    public override string Id => "builtin.note";
    public override string Name => "便签";
    public override string Description => "显示可编辑便签内容";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小方", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中横条", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 200, Height = 100 },
        new WidgetSize { Id = "large-hbar", Name = "大横条", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 160 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("text", "点击编辑");
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var text = context.Settings.GetValue("text", "点击编辑");

        var textBlock = new TextBlock
        {
            Text = text,
            TextWrapping = TextWrapping.Wrap,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            FontSize = 14,
            Foreground = GetThemeBrush("ThemeForegroundBrush")
        };

        return textBlock;
    }
}
