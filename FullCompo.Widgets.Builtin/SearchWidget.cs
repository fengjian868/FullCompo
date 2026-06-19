using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompo.Widgets.Builtin;

public class SearchWidget : WidgetBase
{
    public override string Id => "builtin.search";
    public override string Name => "搜索框";
    public override string Description => "快速搜索";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "2x1", Name = "横条", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 248, Height = 120 },
        new WidgetSize { Id = "4x1", Name = "宽条", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 504, Height = 120 }
    };

    public override Control CreateView(WidgetContext context)
    {
        var stack = new StackPanel
        {
            VerticalAlignment = VerticalAlignment.Center,
            Spacing = 8
        };

        var label = new TextBlock
        {
            FontSize = 13,
            Text = "🔍 搜索"
        };

        var textBox = new TextBox
        {
            Watermark = "输入关键词回车搜索...",
            VerticalContentAlignment = VerticalAlignment.Center,
            CornerRadius = new CornerRadius(8),
            FontSize = 14
        };

        textBox.KeyDown += (_, e) =>
        {
            if (e.Key == Key.Enter && !string.IsNullOrWhiteSpace(textBox.Text))
            {
                var query = Uri.EscapeDataString(textBox.Text);
                Process.Start(new ProcessStartInfo($"https://www.bing.com/search?q={query}") { UseShellExecute = true });
                textBox.Text = string.Empty;
            }
        };

        stack.Children.Add(label);
        stack.Children.Add(textBox);

        return stack;
    }
}
