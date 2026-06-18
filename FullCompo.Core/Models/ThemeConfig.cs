using Avalonia.Media;

namespace FullCompo.Core.Models;

public class ThemeConfig
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Color BackgroundColor { get; set; } = Colors.Black;
    public Color ForegroundColor { get; set; } = Colors.White;
    public Color AccentColor { get; set; } = Colors.DodgerBlue;
    public Color BorderColor { get; set; } = Colors.Gray;
    public double BorderThickness { get; set; } = 1;
    public double CornerRadius { get; set; } = 8;
    public double FontSizeScale { get; set; } = 1.0;
    public string? FontFamily { get; set; }
    public double Opacity { get; set; } = 0.9;
}
