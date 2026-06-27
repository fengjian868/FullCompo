using System.Text.Json;
using FullCompo.Core.Abstractions.Services;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Helpers;
using FullCompo.Shared.Models;

namespace FullCompo.Core.Services;

public class ConfigService : IConfigService
{
    public AppSettings AppSettings { get; private set; } = new();
    public List<PanelConfig> Panels { get; private set; } = new();

    public void Load()
    {
        var configDir = GetConfigDirectory();
        Directory.CreateDirectory(configDir);

        var settingsPath = Path.Combine(configDir, "settings.json");
        var panelsPath = Path.Combine(configDir, "panels.json");

        var isFirstRun = !File.Exists(settingsPath) && !File.Exists(panelsPath);

        try
        {
            AppSettings = File.Exists(settingsPath)
                ? JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(settingsPath), JsonHelper.Options) ?? new AppSettings()
                : new AppSettings();
        }
        catch
        {
            AppSettings = new AppSettings();
        }

        try
        {
            Panels = File.Exists(panelsPath)
                ? JsonSerializer.Deserialize<List<PanelConfig>>(File.ReadAllText(panelsPath), JsonHelper.Options) ?? new List<PanelConfig>()
                : new List<PanelConfig>();
        }
        catch
        {
            Panels = new List<PanelConfig>();
        }

        if (Panels.Count == 0)
        {
            ResetToDefault();
        }

        AppSettings.IsFirstRun = isFirstRun;
    }

    public void Save()
    {
        var configDir = GetConfigDirectory();
        Directory.CreateDirectory(configDir);

        var settingsPath = Path.Combine(configDir, "settings.json");
        var panelsPath = Path.Combine(configDir, "panels.json");

        File.WriteAllText(settingsPath, JsonSerializer.Serialize(AppSettings, JsonHelper.Options));
        File.WriteAllText(panelsPath, JsonSerializer.Serialize(Panels, JsonHelper.Options));
    }

    public void ResetToDefault()
    {
        AppSettings = new AppSettings();

        // 默认布局：单栏放在屏幕右上角（以 1920×1080 为参考，留出 16px 边距）。
        // 组件尺寸统一使用 medium-square(140x140)，在加载时会被自动适配到当前屏幕右下角/右上角。
        const double screenRight = 1920 - 16;
        const double widgetWidth = 140; // medium-square
        const double gap = 8;
        const double top = 16;

        var colX = screenRight - widgetWidth;

        Panels = new List<PanelConfig>
        {
            new()
            {
                Name = "默认面板",
                Widgets = new List<WidgetInstanceConfig>
                {
                    new() { WidgetId = "builtin.clock", SizeId = "medium-square", PosX = colX, PosY = top },
                    new() { WidgetId = "builtin.date", SizeId = "medium-square", PosX = colX, PosY = top + widgetWidth + gap },
                    new() { WidgetId = "builtin.weather", SizeId = "medium-square", PosX = colX, PosY = top + (widgetWidth + gap) * 2 }
                }
            }
        };
        Save();
    }

    public string GetConfigDirectory()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return Path.Combine(appData, "FullCompo", "data");
    }
}
