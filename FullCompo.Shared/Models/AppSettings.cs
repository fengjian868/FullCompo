namespace FullCompo.Shared.Models;

public class AppSettings
{
    public string Language { get; set; } = "zh-CN";
    public string ThemeId { get; set; } = "dark";
    public bool RunOnStartup { get; set; }
    public bool ShowTrayIcon { get; set; } = true;
    public bool ClickThrough { get; set; }
    public string EditModeShortcut { get; set; } = "Ctrl+Shift+E";
}
