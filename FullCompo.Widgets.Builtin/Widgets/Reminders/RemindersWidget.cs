using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompo.Widgets.Builtin.Widgets.Reminders;

public class RemindersWidget : WidgetBase
{
    public override string Id => "builtin.reminders";
    public override string Name => "待办";
    public override string Description => "显示待办事项列表";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小方", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-square", Name = "中方", Type = WidgetSizeType.Medium, Columns = 2, Rows = 2, Width = 140, Height = 140 },
        new WidgetSize { Id = "large-square", Name = "大方", Type = WidgetSizeType.Large, Columns = 3, Rows = 3, Width = 220, Height = 220 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("header", "待办");
        settings.SetValue("items", new List<ReminderItem>());
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var header = context.Settings.GetValue("header", "待办") ?? "待办";
        var items = GetItems(context.Settings);

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isSmall = context.CurrentSize.Width < 140;

        var panel = new StackPanel
        {
            Spacing = 4,
            Margin = new Thickness(8)
        };

        var headerText = new TextBlock
        {
            Text = header,
            FontSize = isSmall ? 12 : 14,
            FontWeight = FontWeight.SemiBold,
            Foreground = foreground,
            TextTrimming = TextTrimming.CharacterEllipsis
        };
        panel.Children.Add(headerText);

        var listPanel = new StackPanel
        {
            Spacing = 2
        };

        foreach (var item in items)
        {
            var check = new CheckBox
            {
                Content = item.Title,
                IsChecked = item.IsDone,
                FontSize = isSmall ? 10 : 12,
                Foreground = item.IsDone ? secondaryForeground : foreground
            };
            check.IsCheckedChanged += (_, _) =>
            {
                item.IsDone = check.IsChecked == true;
                check.Foreground = item.IsDone ? secondaryForeground : foreground;
                SaveItems(context.Settings, items);
            };
            listPanel.Children.Add(check);
        }

        if (items.Count == 0)
        {
            listPanel.Children.Add(new TextBlock
            {
                Text = "暂无待办",
                FontSize = isSmall ? 10 : 12,
                Foreground = secondaryForeground,
                Opacity = 0.7
            });
        }

        panel.Children.Add(listPanel);

        var border = new Border
        {
            Background = GetThemeBrush("ThemeBackgroundBrush"),
            CornerRadius = new CornerRadius(12),
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            Child = panel
        };

        return border;
    }

    public override Control? CreateSettingsView(WidgetSettings settings)
    {
        var header = settings.GetValue("header", "待办") ?? "待办";
        var items = GetItems(settings);

        var panel = new StackPanel
        {
            Spacing = 8
        };

        var headerBox = new TextBox
        {
            Watermark = "标题",
            Text = header
        };
        headerBox.TextChanged += (_, _) => settings.SetValue("header", headerBox.Text ?? "待办");
        panel.Children.Add(new TextBlock { Text = "标题" });
        panel.Children.Add(headerBox);

        var addPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 6
        };
        var newItemBox = new TextBox
        {
            Watermark = "新待办",
            Width = 160
        };
        var addButton = new Button
        {
            Content = "添加"
        };

        var itemsPanel = new StackPanel
        {
            Spacing = 4
        };

        void RefreshItems()
        {
            itemsPanel.Children.Clear();
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var row = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Spacing = 6
                };
                var doneCheck = new CheckBox
                {
                    IsChecked = item.IsDone
                };
                doneCheck.IsCheckedChanged += (_, _) =>
                {
                    item.IsDone = doneCheck.IsChecked == true;
                    SaveItems(settings, items);
                };
                var titleBox = new TextBox
                {
                    Text = item.Title,
                    Width = 140
                };
                titleBox.TextChanged += (_, _) =>
                {
                    item.Title = titleBox.Text ?? "";
                    SaveItems(settings, items);
                };
                var removeButton = new Button
                {
                    Content = "删除"
                };
                removeButton.Click += (_, _) =>
                {
                    items.Remove(item);
                    SaveItems(settings, items);
                    RefreshItems();
                };
                row.Children.Add(doneCheck);
                row.Children.Add(titleBox);
                row.Children.Add(removeButton);
                itemsPanel.Children.Add(row);
            }
        }

        addButton.Click += (_, _) =>
        {
            var title = newItemBox.Text?.Trim();
            if (string.IsNullOrWhiteSpace(title)) return;
            items.Add(new ReminderItem { Title = title, IsDone = false });
            SaveItems(settings, items);
            newItemBox.Text = "";
            RefreshItems();
        };

        addPanel.Children.Add(newItemBox);
        addPanel.Children.Add(addButton);

        panel.Children.Add(new TextBlock { Text = "待办列表" });
        panel.Children.Add(addPanel);
        panel.Children.Add(itemsPanel);

        RefreshItems();
        return panel;
    }

    private static List<ReminderItem> GetItems(WidgetSettings settings)
    {
        var items = settings.GetValue<List<ReminderItem>>("items", null);
        return items ?? new List<ReminderItem>();
    }

    private static void SaveItems(WidgetSettings settings, List<ReminderItem> items)
    {
        settings.SetValue("items", items);
    }
}
