using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using FullCompo.Core.Abstractions;
using FullCompo.Core.Models;
using FullCompo.PluginSdk;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompo.ExamplePlugin;

public class GreetingWidget : IWidget
{
    public string Id => "example.greeting";
    public string Name => "问候语";
    public string Description => "显示自定义问候语";
    public IImage? Icon => null;
    public bool HasCustomBackground => false;

    public IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "greeting-small", Name = "小", Type = WidgetSizeType.Small, Columns = 1, Rows = 1 },
        new WidgetSize { Id = "greeting-medium", Name = "中", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1 },
        new WidgetSize { Id = "greeting-large", Name = "大", Type = WidgetSizeType.Large, Columns = 2, Rows = 2 }
    };

    public WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("message", "你好，全面组件！");
        settings.SetValue("fontSize", 18.0);
        return settings;
    }

    public Control CreateView(WidgetContext context)
    {
        var message = context.Settings.GetValue("message", "你好，全面组件！");
        var fontSize = context.Settings.GetValue("fontSize", 18.0);

        return new Border
        {
            Background = new SolidColorBrush(Color.Parse("#4466CCFF")),
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(12),
            Child = new TextBlock
            {
                Text = message,
                FontSize = fontSize,
                FontWeight = FontWeight.Bold,
                Foreground = new SolidColorBrush(Colors.White),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center
            }
        };
    }

    public Control? CreateSettingsView(WidgetSettings settings)
    {
        var stack = new StackPanel { Spacing = 8 };

        var textBox = new TextBox
        {
            Text = settings.GetValue("message", "你好，全面组件！") ?? "你好，全面组件！",
            Watermark = "问候语内容"
        };
        textBox.TextChanged += (_, _) => settings.SetValue("message", textBox.Text ?? "");

        var fontSizeBox = new NumericUpDown
        {
            Value = (decimal?)settings.GetValue("fontSize", 18.0),
            Minimum = 8,
            Maximum = 72,
            Increment = 1
        };
        fontSizeBox.ValueChanged += (_, _) => settings.SetValue("fontSize", (double?)fontSizeBox.Value ?? 18.0);

        stack.Children.Add(new TextBlock { Text = "问候语" });
        stack.Children.Add(textBox);
        stack.Children.Add(new TextBlock { Text = "字号" });
        stack.Children.Add(fontSizeBox);

        return stack;
    }

    public void OnActivated(WidgetContext context)
    {
    }

    public void OnDeactivated()
    {
    }
}
