using FullCompo.Core.Abstractions;

namespace FullCompo.Core.Services;

public class WidgetRegistry : IWidgetRegistry
{
    private readonly Dictionary<string, IWidget> _widgets = new();

    public void Register(IWidget widget)
    {
        _widgets[widget.Id] = widget;
    }

    public void RegisterRange(IEnumerable<IWidget> widgets)
    {
        foreach (var widget in widgets)
        {
            Register(widget);
        }
    }

    public IWidget? GetWidget(string widgetId)
    {
        _widgets.TryGetValue(widgetId, out var widget);
        return widget;
    }

    public IEnumerable<IWidget> GetAllWidgets() => _widgets.Values;
}
