using Avalonia.Media;
using FullCompo.Core.Models;

namespace FullCompo.Core.Abstractions.Services;

public interface IThemeService
{
    ThemeConfig CurrentTheme { get; }
    IReadOnlyList<ThemeConfig> AvailableThemes { get; }

    void LoadThemes();
    void ApplyTheme(string themeId);
    event EventHandler<ThemeConfig>? ThemeChanged;
}
