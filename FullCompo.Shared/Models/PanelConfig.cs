using FullCompo.Shared.Enums;

namespace FullCompo.Shared.Models;

public class PanelConfig
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = "默认面板";
    public List<WidgetInstanceConfig> Widgets { get; set; } = new();
}
