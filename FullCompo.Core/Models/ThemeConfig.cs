using Avalonia.Media;

namespace FullCompo.Core.Models;

public class ThemeConfig
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    // 基础色
    public Color BackgroundColor { get; set; } = Colors.Black;
    public Color ForegroundColor { get; set; } = Colors.White;
    public Color AccentColor { get; set; } = Colors.DodgerBlue;
    public Color BorderColor { get; set; } = Colors.Gray;

    // 语义色
    public Color SurfaceColor { get; set; } = Colors.DarkGray;
    public Color SidebarColor { get; set; } = Colors.DimGray;
    public Color SecondaryForegroundColor { get; set; } = Colors.LightGray;
    public Color DisabledForegroundColor { get; set; } = Colors.Gray;
    public Color DividerColor { get; set; } = Colors.Gray;
    public Color HoverColor { get; set; } = Colors.Transparent;
    public Color SelectionColor { get; set; } = Colors.DodgerBlue;
    public Color ErrorColor { get; set; } = Colors.Red;
    public Color SuccessColor { get; set; } = Colors.LimeGreen;

    // 数值
    public double BorderThickness { get; set; } = 1;
    public double CornerRadius { get; set; } = 8;
    public double FontSizeScale { get; set; } = 1.0;
    public string? FontFamily { get; set; }
    public double Opacity { get; set; } = 0.9;
}
