using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;
using Microsoft.Extensions.Logging;

namespace FullCompo.Widgets.Builtin;

public class LauncherWidget : WidgetBase
{
    public override string Id => "builtin.launcher";
    public override string Name => "快捷启动";
    public override string Description => "快捷启动应用或网址";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小方形", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中横条", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 200, Height = 100 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("target", "https://www.bing.com");
        settings.SetValue("label", "必应");
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var target = context.Settings.GetValue("target", "https://www.bing.com") ?? "https://www.bing.com";
        var label = context.Settings.GetValue("label", "必应") ?? "必应";

        var stack = new StackPanel
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Spacing = 8
        };

        var iconText = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            FontSize = 28,
            Text = "🚀"
        };

        var labelText = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            FontSize = 13,
            Text = label
        };

        stack.Children.Add(iconText);
        stack.Children.Add(labelText);

        var border = new Border
        {
            Child = stack,
            CornerRadius = new CornerRadius(16)
        };

        border.PointerPressed += (_, e) =>
        {
            try
            {
                Process.Start(new ProcessStartInfo(target) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                context.Logger.LogError(ex, "Failed to launch {Target}", target);
            }
        };

        return border;
    }
}
