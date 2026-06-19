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
        new WidgetSize { Id = "medium-square", Name = "中方形", Type = WidgetSizeType.Medium, Columns = 2, Rows = 2, Width = 140, Height = 140 },
        new WidgetSize { Id = "large-square", Name = "大方形", Type = WidgetSizeType.Large, Columns = 3, Rows = 3, Width = 220, Height = 220 }
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
            FontSize = 14
        };

        return textBlock;
    }
}
