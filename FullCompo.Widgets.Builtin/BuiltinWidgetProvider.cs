using FullCompo.Core.Abstractions;

namespace FullCompo.Widgets.Builtin;

public class BuiltinWidgetProvider : IWidgetProvider
{
    public IEnumerable<IWidget> GetWidgets()
    {
        return new IWidget[]
        {
            new DateWidget(),
            new WeatherWidget(),
            new ClockWidget()
        };
    }
}
