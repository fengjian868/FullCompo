namespace FullCompo.Core.Abstractions;

public interface IWidgetProvider
{
    IEnumerable<IWidget> GetWidgets();
}
