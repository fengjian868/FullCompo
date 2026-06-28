using System.Management;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;

namespace FullCompo.Widgets.Builtin.Widgets.Monitor;

public class MonitorWidget : WidgetBase
{
    public override string Id => "builtin.monitor";
    public override string Name => "监控";
    public override string Description => "显示 CPU、内存、磁盘、网络或电池状态";

    public override IEnumerable<WidgetSize> SupportedSizes => new[]
    {
        new WidgetSize { Id = "small-square", Name = "小方", Type = WidgetSizeType.Small, Columns = 1, Rows = 1, Width = 80, Height = 80 },
        new WidgetSize { Id = "medium-hbar", Name = "中横条", Type = WidgetSizeType.Medium, Columns = 2, Rows = 1, Width = 200, Height = 100 },
        new WidgetSize { Id = "large-hbar", Name = "大横条", Type = WidgetSizeType.Large, Columns = 4, Rows = 1, Width = 320, Height = 160 }
    };

    public override WidgetSettings CreateDefaultSettings()
    {
        var settings = new WidgetSettings();
        settings.SetValue("metricType", MetricType.CpuUsage.ToString());
        return settings;
    }

    public override Control CreateView(WidgetContext context)
    {
        var metricTypeString = context.Settings.GetValue("metricType", MetricType.CpuUsage.ToString()) ?? MetricType.CpuUsage.ToString();
        if (!Enum.TryParse<MetricType>(metricTypeString, out var metricType))
            metricType = MetricType.CpuUsage;

        var foreground = GetThemeBrush("ThemeForegroundBrush");
        var secondaryForeground = GetThemeBrush("ThemeSecondaryForegroundBrush");
        var accent = GetThemeBrush("ThemeAccentBrush");

        var isWide = context.CurrentSize.Width >= context.CurrentSize.Height * 2;
        var isLarge = context.CurrentSize.Width >= 140;

        var iconText = new TextBlock
        {
            Text = GetIcon(metricType),
            FontSize = isLarge ? 32 : 20,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        var labelText = new TextBlock
        {
            Text = GetLabel(metricType),
            FontSize = isLarge ? 14 : 11,
            Foreground = secondaryForeground,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        var valueText = new TextBlock
        {
            FontSize = isLarge ? 28 : (isWide ? 20 : 16),
            FontWeight = FontWeight.SemiBold,
            Foreground = accent,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Text = "--"
        };

        var root = new Grid
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch
        };

        if (isWide)
        {
            var panel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Spacing = 10
            };
            var left = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            left.Children.Add(iconText);
            left.Children.Add(labelText);
            panel.Children.Add(left);
            panel.Children.Add(valueText);
            root.Children.Add(panel);
        }
        else
        {
            var panel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Spacing = 4
            };
            panel.Children.Add(iconText);
            panel.Children.Add(valueText);
            panel.Children.Add(labelText);
            root.Children.Add(panel);
        }

        async void Update()
        {
            var value = await GetMetricValueAsync(metricType);
            valueText.Text = FormatValue(metricType, value);
        }

        Update();

        var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
        timer.Tick += (_, _) => Update();
        timer.Start();

        return root;
    }

    public override Control? CreateSettingsView(WidgetSettings settings)
    {
        var current = settings.GetValue("metricType", MetricType.CpuUsage.ToString()) ?? MetricType.CpuUsage.ToString();

        var panel = new StackPanel
        {
            Spacing = 8
        };
        panel.Children.Add(new TextBlock { Text = "监控指标" });

        foreach (MetricType metric in Enum.GetValues<MetricType>())
        {
            var radio = new RadioButton
            {
                Content = GetLabel(metric),
                IsChecked = metric.ToString() == current
            };
            radio.IsCheckedChanged += (_, _) =>
            {
                if (radio.IsChecked == true)
                    settings.SetValue("metricType", metric.ToString());
            };
            panel.Children.Add(radio);
        }

        return panel;
    }

    private static string GetIcon(MetricType type) => type switch
    {
        MetricType.CpuUsage => "🖥️",
        MetricType.RamUsage => "🧠",
        MetricType.DiskUsage => "💾",
        MetricType.NetworkUsage => "🌐",
        MetricType.BatteryLevel => "🔋",
        _ => "📊"
    };

    private static string GetLabel(MetricType type) => type switch
    {
        MetricType.CpuUsage => "CPU",
        MetricType.RamUsage => "内存",
        MetricType.DiskUsage => "磁盘",
        MetricType.NetworkUsage => "网络",
        MetricType.BatteryLevel => "电池",
        _ => "未知"
    };

    private static string FormatValue(MetricType type, double? value)
    {
        if (value == null) return "--";
        return type switch
        {
            MetricType.NetworkUsage => $"{value:F1} Mbps",
            MetricType.BatteryLevel => $"{value:P0}",
            _ => $"{value:P0}"
        };
    }

    private static Task<double?> GetMetricValueAsync(MetricType type)
    {
        return Task.Run(() =>
        {
            try
            {
                if (!OperatingSystem.IsWindows()) return (double?)null;

                return type switch
                {
                    MetricType.CpuUsage => GetCpuUsage(),
                    MetricType.RamUsage => GetRamUsage(),
                    MetricType.DiskUsage => GetDiskUsage(),
                    MetricType.NetworkUsage => GetNetworkUsage(),
                    MetricType.BatteryLevel => GetBatteryLevel(),
                    _ => null
                };
            }
            catch
            {
                return (double?)null;
            }
        });
    }

    private static double? GetCpuUsage()
    {
        using var searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor where Name='_Total'");
        foreach (var o in searcher.Get())
        {
            var obj = (ManagementObject)o;
            return Convert.ToDouble(obj["PercentProcessorTime"]) / 100.0;
        }
        return null;
    }

    private static double? GetRamUsage()
    {
        using var searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
        foreach (var o in searcher.Get())
        {
            var obj = (ManagementObject)o;
            var total = Convert.ToDouble(obj["TotalVisibleMemorySize"]);
            var free = Convert.ToDouble(obj["FreePhysicalMemory"]);
            if (total > 0)
                return (total - free) / total;
        }
        return null;
    }

    private static double? GetDiskUsage()
    {
        using var searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfDisk_LogicalDisk where Name='_Total'");
        foreach (var o in searcher.Get())
        {
            var obj = (ManagementObject)o;
            return Convert.ToDouble(obj["PercentDiskTime"]) / 100.0;
        }
        return null;
    }

    private static double? GetNetworkUsage()
    {
        double totalBytesPerSec = 0;

        using (var searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_Tcpip_NetworkInterface"))
        {
            foreach (var o in searcher.Get())
            {
                var obj = (ManagementObject)o;
                totalBytesPerSec += Convert.ToDouble(obj["BytesSentPerSec"]);
                totalBytesPerSec += Convert.ToDouble(obj["BytesReceivedPerSec"]);
            }
        }

        // Mbps
        return totalBytesPerSec * 8 / 1_000_000.0;
    }

    private static double? GetBatteryLevel()
    {
        using var searcher = new ManagementObjectSearcher("select * from Win32_Battery");
        foreach (var o in searcher.Get())
        {
            var obj = (ManagementObject)o;
            return Convert.ToInt32(obj["EstimatedChargeRemaining"]) / 100.0;
        }
        return null;
    }
}
