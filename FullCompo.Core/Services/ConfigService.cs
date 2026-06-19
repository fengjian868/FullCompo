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
        Panels = new List<PanelConfig>
        {
            new()
            {
                Name = "默认面板",
                Widgets = new List<WidgetInstanceConfig>
                {
                    new() { WidgetId = "builtin.date", SizeId = "1x1", PosX = 16, PosY = 16 },
                    new() { WidgetId = "builtin.clock", SizeId = "2x1", PosX = 144, PosY = 16 },
                    new() { WidgetId = "builtin.weather", SizeId = "1x1", PosX = 400, PosY = 16 },
                    new() { WidgetId = "builtin.search", SizeId = "2x1", PosX = 16, PosY = 144 },
                    new() { WidgetId = "builtin.launcher", SizeId = "1x1", PosX = 272, PosY = 144 }
                }
            }
        };
        Save();
    }

    public string GetConfigDirectory()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return Path.Combine(appData, "FullCompo");
    }
}
