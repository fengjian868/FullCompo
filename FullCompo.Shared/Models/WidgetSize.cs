using FullCompo.Shared.Enums;

namespace FullCompo.Shared.Models;

public record WidgetSize
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public WidgetSizeType Type { get; init; }
    public int Columns { get; init; } = 1;
    public int Rows { get; init; } = 1;
    /// <summary>
    /// 组件宽度（像素）
    /// </summary>
    public double Width { get; init; } = 120;
    /// <summary>
    /// 组件高度（像素）
    /// </summary>
    public double Height { get; init; } = 120;
    public bool IsCircular { get; init; }
}
