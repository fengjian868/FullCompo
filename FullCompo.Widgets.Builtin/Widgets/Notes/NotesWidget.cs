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
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
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
usingusing System;
using Avalonia;
using Avalusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompousing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompo.Widgets.Builtin.Widgets.Notes;
using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompo.Widgets.Builtin.Widgets.Notes;

public class NotesWidget : WidgetBase
{
    public override string Id => "builtin.nusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompo.Widgets.Builtin.Widgets.Notes;

public class NotesWidget : WidgetBase
{
    public override string Id => "builtin.notes";
    public override string Name =>using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override stringusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件",using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns =using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width =using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-husing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type =using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        newusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rowsusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Heightusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettingsusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.Setusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public overrideusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValueusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValueusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3Busing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.Getusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("Themeusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground =using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("Themeusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;
using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 :using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thicknessusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thicknessusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foregroundusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrushusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment =using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("headerusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            Updateusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadiususing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12,using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child =using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignmentusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        varusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Datausing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Pointusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
            Stroke = foreground,
            StrokeThickness =using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
            Stroke = foreground,
            StrokeThickness = 1,
            StrokeDashArray = newusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
            Stroke = foreground,
            StrokeThickness = 1,
            StrokeDashArray = new AvaloniaList<double> { 4,using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
            Stroke = foreground,
            StrokeThickness = 1,
            StrokeDashArray = new AvaloniaList<double> { 4, 4 },
            Opacity = 0using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
            Stroke = foreground,
            StrokeThickness = 1,
            StrokeDashArray = new AvaloniaList<double> { 4, 4 },
            Opacity = 0.2,
            Stretch = Stretch.Fill,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Height = 1,
            Marginusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
            Stroke = foreground,
            StrokeThickness = 1,
            StrokeDashArray = new AvaloniaList<double> { 4, 4 },
            Opacity = 0.2,
            Stretch = Stretch.Fill,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Height = 1,
            Margin = new Thickness(8, 0)
        };

        var contentBoxusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
            Stroke = foreground,
            StrokeThickness = 1,
            StrokeDashArray = new AvaloniaList<double> { 4, 4 },
            Opacity = 0.2,
            Stretch = Stretch.Fill,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Height = 1,
            Margin = new Thickness(8, 0)
        };

        var contentBox = new TextBox
        {
            Textusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
            Stroke = foreground,
            StrokeThickness = 1,
            StrokeDashArray = new AvaloniaList<double> { 4, 4 },
            Opacity = 0.2,
            Stretch = Stretch.Fill,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Height = 1,
            Margin = new Thickness(8, 0)
        };

        var contentBox = new TextBox
        {
            Text = content,
            FontSize = isSmallusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
            Stroke = foreground,
            StrokeThickness = 1,
            StrokeDashArray = new AvaloniaList<double> { 4, 4 },
            Opacity = 0.2,
            Stretch = Stretch.Fill,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Height = 1,
            Margin = new Thickness(8, 0)
        };

        var contentBox = new TextBox
        {
            Text = content,
            FontSize = isSmall ? 10 : 12,
            Backgroundusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
            Stroke = foreground,
            StrokeThickness = 1,
            StrokeDashArray = new AvaloniaList<double> { 4, 4 },
            Opacity = 0.2,
            Stretch = Stretch.Fill,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Height = 1,
            Margin = new Thickness(8, 0)
        };

        var contentBox = new TextBox
        {
            Text = content,
            FontSize = isSmall ? 10 : 12,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Paddingusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
            Stroke = foreground,
            StrokeThickness = 1,
            StrokeDashArray = new AvaloniaList<double> { 4, 4 },
            Opacity = 0.2,
            Stretch = Stretch.Fill,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Height = 1,
            Margin = new Thickness(8, 0)
        };

        var contentBox = new TextBox
        {
            Text = content,
            FontSize = isSmall ? 10 : 12,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = foreground,
            Careusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
            Stroke = foreground,
            StrokeThickness = 1,
            StrokeDashArray = new AvaloniaList<double> { 4, 4 },
            Opacity = 0.2,
            Stretch = Stretch.Fill,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Height = 1,
            Margin = new Thickness(8, 0)
        };

        var contentBox = new TextBox
        {
            Text = content,
            FontSize = isSmall ? 10 : 12,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = foreground,
            CaretBrush = foreground,
            TextWrappingusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
            Stroke = foreground,
            StrokeThickness = 1,
            StrokeDashArray = new AvaloniaList<double> { 4, 4 },
            Opacity = 0.2,
            Stretch = Stretch.Fill,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Height = 1,
            Margin = new Thickness(8, 0)
        };

        var contentBox = new TextBox
        {
            Text = content,
            FontSize = isSmall ? 10 : 12,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = foreground,
            CaretBrush = foreground,
            TextWrapping = TextWrapping.Wrap,
            Acceptsusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
            Stroke = foreground,
            StrokeThickness = 1,
            StrokeDashArray = new AvaloniaList<double> { 4, 4 },
            Opacity = 0.2,
            Stretch = Stretch.Fill,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Height = 1,
            Margin = new Thickness(8, 0)
        };

        var contentBox = new TextBox
        {
            Text = content,
            FontSize = isSmall ? 10 : 12,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = foreground,
            CaretBrush = foreground,
            TextWrapping = TextWrapping.Wrap,
            AcceptsReturn = true,
            VerticalAlignment = Verticalusing System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
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
    public override string Description => "显示可编辑标题和内容的便签";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小组件", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中组件", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 160, Height = 80 },
        new WidgetSize { Id = "large-hbar", Name = "大组件", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 80 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "便签");
        settings.SetValue("content", "点击编辑内容");
        settings.SetValue("backgroundColor", "#FFFEF3B7");
        settings.SetValue("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "便签") ?? "便签";
        var content = context.Settings.GetValue("content", "") ?? "";
        var backgroundColor = context.Settings.GetValue("backgroundColor", "#FFFEF3B7") ?? "#FFFEF3B7";
        var updated = context.Settings.GetValue("updated", "") ?? "";

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var headerBox = new TextBox
        {
            Text = header,
            FontSize = isSmall ? 11 : 13,
            FontWeight = FontWeight.SemiBold,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = Brushes.White,
            CaretBrush = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        headerBox.LostFocus += (_, _) =>
        {
            context.Settings.SetValue("header", headerBox.Text ?? "");
            UpdateTimestamp(context.Settings);
        };

        var headerBorder = new Border
        {
            Background = accent,
            CornerRadius = new CornerRadius(12, 12, 0, 0),
            Child = headerBox,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        var separator = new Path
        {
            Data = new LineGeometry { StartPoint = new Point(0, 0), EndPoint = new Point(1, 0) },
            Stroke = foreground,
            StrokeThickness = 1,
            StrokeDashArray = new AvaloniaList<double> { 4, 4 },
            Opacity = 0.2,
            Stretch = Stretch.Fill,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Height = 1,
            Margin = new Thickness(8, 0)
        };

        var contentBox = new TextBox
        {
            Text = content,
            FontSize = isSmall ? 10 : 12,
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Padding = new Thickness(8, 4),
            Foreground = foreground,
            CaretBrush = foreground,
            TextWrapping = TextWrapping.Wrap,
            AcceptsReturn = true,
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = Horizontal