namespace FullCompo.Shared.Models;

public class WidgetInstanceConfig
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string WidgetId { get; set; } = string.Empty;
    public string PanelId { get; set; } = string.Empty;
    /// <summary>
    /// 组件尺寸类型: "1x1", "2x1", "1x2", "2x2"
    /// </summary>
    public string SizeId { get; set; } = "1x1";
    /// <summary>
    /// 桌面位置 X
    /// </summary>
    public double PosX { get; set; }
    /// <summary>
    /// 桌面位置 Y
    /// </summary>
    public double PosY { get; set; }
    public WidgetSettings Settings { get; set; } = new();
}
