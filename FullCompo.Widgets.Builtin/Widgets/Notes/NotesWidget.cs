using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompo.Widgets.Builtin.Widgets.Notes;

public class NotesWidget : WidgetBase
{
    public override string Id => "builtin.notes";
    public override string Name => "便签";
    public override string Description => "显示带有标题的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小方", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中横条", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 200, Height = 100 },
        new WidgetSize { Id = "large-hbar", Name = "大横条", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 160 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击设置编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerText = new TextBlock
        {
            Text = header,
            FontSize = isSmall ? 12 : 14,
            FontWeight = FontWeight.SemiBold,
            Foreground = foreground,
            TextTrimming = TextTrimming.CharacterEllipsis
        };

        var contentText = new TextBlock
        {
            Text = content,
            FontSize = isSmall ? 11 : 13,
            Foreground = secondaryForeground,
            TextWrapping = TextWrapping.Wrap,
            TextTrimming = TextTrimming.CharacterEllipsis
        };

        var stack = new StackPanel
        {
            Spacing = 4,
            Margin = new Thickness(10)
        };
        stack.Children.Add(headerText);
        stack.Children.Add(contentText);

        var border = new Border
        {
            Background = ParseBrush(backgroundColor),
            CornerRadius = new CornerRadius(12),
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            Child = stack
        };

        return border;
    }

    public override Control? CreateSettingsView(WidgetSettings settings)
    {
        var header = settings.GetValue("header", "便签") ?? "便签";
        var content = settings.GetValue("content", "") ?? "";
        var backgroundColor = settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";

        var panel = new StackPanel
        {
            Spacing = 8
        };

        var headerBox = new TextBox
        {
            Watermark = "标题",
            Text = header
        };
        headerBox.TextChanged += (_, _) => settings.SetValue("header", headerBox.Text ?? "");

        var contentBox = new TextBox
        {
            Watermark = "内容",
            Text = content,
            AcceptsReturn = true,
            Height = 80
        };
        contentBox.TextChanged += (_, _) => settings.SetValue("content", contentBox.Text ?? "");

        var colorBox = new TextBox
        {
            Watermark = "背景色 (#RRGGBBAA)",
            Text = backgroundColor
        };
        colorBox.TextChanged += (_, _) => settings.SetValue("backgroundColor", colorBox.Text ?? "#FFFEF3B7");

        panel.Children.Add(new TextBlock { Text = "标题" });
        panel.Children.Add(headerBox);
        panel.Children.Add(new TextBlock { Text = "内容" });
        panel.Children.Add(contentBox);
        panel.Children.Add(new TextBlock { Text = "背景色" });
        panel.Children.Add(colorBox);

        return panel;
    }

    private static IBrush ParseBrush(string color)
    {
        try
        {
            if (Color.TryParse(color, out var parsed))
                return new SolidColorBrush(parsed);
        }
        catch
        {
            // ignored
        }

        return new SolidColorBrush(Colors.LightYellow);
    }
}
