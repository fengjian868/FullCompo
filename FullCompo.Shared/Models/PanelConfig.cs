using FullCompo.Shared.Enums;

namespace FullCompo.Shared.Models;

public class PanelConfig
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = "默认面板";
    public PanelDockMode DockMode { get; set; } = PanelDockMode.TopRightCorner;
    public double MarginLeft { get; set; }
    public double MarginTop { get; set; }
    public double MarginRight { get; set; } = 16;
    public double MarginBottom { get; set; }
    public int Columns { get; set; } = 4;
    public double CellWidth { get; set; } = 80;
    public double CellHeight { get; set; } = 80;
    public double Spacing { get; set; } = 8;
    public List<WidgetInstanceConfig> Widgets { get; set; } = new();
}
