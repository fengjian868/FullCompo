namespace FullCompo.Core.Abstractions;

public interface IWidgetRegistry
{
    void Register(IWidget widget);
    void RegisterRange(IEnumerable<IWidget> widgets);
    IWidget? GetWidget(string widgetId);
    IEnumerable<IWidget> GetAllWidgets();
}
