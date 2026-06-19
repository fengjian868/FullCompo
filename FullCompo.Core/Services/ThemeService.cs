using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Styling;
using FullCompo.Core.Abstractions.Services;
using FullCompo.Core.Models;

namespace FullCompo.Core.Services;

public class ThemeService : IThemeService
{
    private readonly List<ThemeConfig> _themes = new();

    public ThemeConfig CurrentTheme { get; private set; } = new();
    public IReadOnlyList<ThemeConfig> AvailableThemes => _themes;

    public event EventHandler<ThemeConfig>? ThemeChanged;

    public void LoadThemes()
    {
        _themes.Clear();
        _themes.AddRange(GetBuiltinThemes());

        if (_themes.Count > 0 && string.IsNullOrEmpty(CurrentTheme.Id))
        {
            CurrentTheme = _themes[0];
        }
    }

    public void ApplyTheme(string themeId)
    {
        var theme = _themes.FirstOrDefault(t => t.Id == themeId);
        if (theme == null) return;

        if (theme.FollowSystem)
        {
            theme = GetSystemActualTheme(theme);
        }

        CurrentTheme = theme;
        ApplyToApplicationResources(theme);
        ThemeChanged?.Invoke(this, theme);
    }

    private static ThemeConfig GetSystemActualTheme(ThemeConfig followSystemTheme)
    {
        var variant = Application.Current?.ActualThemeVariant;
        bool isDark = variant == ThemeVariant.Dark;

        var baseTheme = isDark ? CreateDarkTheme() : CreateLightTheme();
        baseTheme.Id = followSystemTheme.Id;
        baseTheme.Name = followSystemTheme.Name;
        baseTheme.FollowSystem = true;
        return baseTheme;
    }

    private static IEnumerable<ThemeConfig> GetBuiltinThemes()
    {
        return new[]
        {
            CreateDarkTheme(),
            CreateLightTheme(),
            CreateGlassTheme(),
            new ThemeConfig
            {
                Id = "system",
                Name = "跟随系统",
                FollowSystem = true,
                UseAcrylic = true,
                Opacity = 0.92
            }
        };
    }

    private static ThemeConfig CreateDarkTheme()
    {
        return new ThemeConfig
        {
            Id = "dark",
            Name = "深色",
            BackgroundColor = Color.Parse("#B31E1E1E"),
            ForegroundColor = Colors.White,
            AccentColor = Color.Parse("#FF4A90E2"),
            BorderColor = Color.Parse("#55FFFFFF"),
            SurfaceColor = Color.Parse("#CC2D2D2D"),
            SidebarColor = Color.Parse("#CC252525"),
            SecondaryForegroundColor = Color.Parse("#FFAAAAAA"),
            DisabledForegroundColor = Color.Parse("#FF666666"),
            DividerColor = Color.Parse("#33FFFFFF"),
            HoverColor = Color.Parse("#1AFFFFFF"),
            SelectionColor = Color.Parse("#334A90E2"),
            ErrorColor = Color.Parse("#FFFF4444"),
            SuccessColor = Color.Parse("#FF44CC88"),
            BorderThickness = 1,
            CornerRadius = 20,
            Opacity = 0.92,
            UseAcrylic = true
        };
    }

    private static ThemeConfig CreateLightTheme()
    {
        return new ThemeConfig
        {
            Id = "light",
            Name = "浅色",
            BackgroundColor = Color.Parse("#D9F5F5F5"),
            ForegroundColor = Color.Parse("#FF1F1F1F"),
            AccentColor = Color.Parse("#FF0078D4"),
            BorderColor = Color.Parse("#33000000"),
            SurfaceColor = Color.Parse("#E6FFFFFF"),
            SidebarColor = Color.Parse("#F2F8F8F8"),
            SecondaryForegroundColor = Color.Parse("#FF5F5F5F"),
            DisabledForegroundColor = Color.Parse("#FFC4C4C4"),
            DividerColor = Color.Parse("#FFE5E5E5"),
            HoverColor = Color.Parse("#0A000000"),
            SelectionColor = Color.Parse("#1A0078D4"),
            ErrorColor = Color.Parse("#FFD13438"),
            SuccessColor = Color.Parse("#FF0F7B0F"),
            BorderThickness = 1,
            CornerRadius = 20,
            Opacity = 0.95,
            UseAcrylic = true
        };
    }

    private static ThemeConfig CreateGlassTheme()
    {
        return new ThemeConfig
        {
            Id = "glass",
            Name = "毛玻璃",
            BackgroundColor = Color.Parse("#99232323"),
            ForegroundColor = Colors.White,
            AccentColor = Color.Parse("#FF00D4AA"),
            BorderColor = Color.Parse("#77FFFFFF"),
            SurfaceColor = Color.Parse("#B32D2D2D"),
            SidebarColor = Color.Parse("#B31A1A1A"),
            SecondaryForegroundColor = Color.Parse("#FFBBBBBB"),
            DisabledForegroundColor = Color.Parse("#FF777777"),
            DividerColor = Color.Parse("#44FFFFFF"),
            HoverColor = Color.Parse("#22FFFFFF"),
            SelectionColor = Color.Parse("#3300D4AA"),
            ErrorColor = Color.Parse("#FFFF6B6B"),
            SuccessColor = Color.Parse("#FF00D4AA"),
            BorderThickness = 1,
            CornerRadius = 24,
            Opacity = 0.88,
            UseAcrylic = true
        };
    }

    private static void ApplyToApplicationResources(ThemeConfig theme)
    {
        var resources = Application.Current?.Resources;
        if (resources == null) return;

        resources["ThemeBackgroundBrush"] = new SolidColorBrush(theme.BackgroundColor);
        resources["ThemeForegroundBrush"] = new SolidColorBrush(theme.ForegroundColor);
        resources["ThemeAccentBrush"] = new SolidColorBrush(theme.AccentColor);
        resources["ThemeBorderBrush"] = new SolidColorBrush(theme.BorderColor);
        resources["ThemeSurfaceBrush"] = new SolidColorBrush(theme.SurfaceColor);
        resources["ThemeSidebarBrush"] = new SolidColorBrush(theme.SidebarColor);
        resources["ThemeSecondaryForegroundBrush"] = new SolidColorBrush(theme.SecondaryForegroundColor);
        resources["ThemeDisabledForegroundBrush"] = new SolidColorBrush(theme.DisabledForegroundColor);
        resources["ThemeDividerBrush"] = new SolidColorBrush(theme.DividerColor);
        resources["ThemeHoverBrush"] = new SolidColorBrush(theme.HoverColor);
        resources["ThemeSelectionBrush"] = new SolidColorBrush(theme.SelectionColor);
        resources["ThemeErrorBrush"] = new SolidColorBrush(theme.ErrorColor);
        resources["ThemeSuccessBrush"] = new SolidColorBrush(theme.SuccessColor);
        resources["ThemeBorderThickness"] = theme.BorderThickness;
        resources["ThemeCornerRadius"] = new CornerRadius(theme.CornerRadius);
        resources["ThemeFontSizeScale"] = theme.FontSizeScale;
        resources["ThemeOpacity"] = theme.Opacity;
        resources["ThemeUseAcrylic"] = theme.UseAcrylic;

        var fontFamily = string.IsNullOrWhiteSpace(theme.FontFamily)
            ? GetDefaultFontFamily()
            : new FontFamily(theme.FontFamily);
        resources["ThemeFontFamily"] = fontFamily;
    }

    private static FontFamily GetDefaultFontFamily()
    {
        // Prefer HarmonyOS Sans, fall back to system Chinese/UI fonts
        return new FontFamily("HarmonyOS Sans, HarmonyOS Sans SC, Microsoft YaHei UI, Microsoft YaHei, PingFang SC, Segoe UI, sans-serif");
    }
}
