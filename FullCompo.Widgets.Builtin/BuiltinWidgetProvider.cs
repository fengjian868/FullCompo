using FullCompo.Core.Abstractions;
using FullCompo.Widgets.Builtin.Widgets.Calendar;
using FullCompo.Widgets.Builtin.Widgets.Clock;
using FullCompo.Widgets.Builtin.Widgets.Monitor;
using FullCompo.Widgets.Builtin.Widgets.Notes;
using FullCompo.Widgets.Builtin.Widgets.Reminders;
using FullCompo.Widgets.Builtin.Widgets.Weather;

namespace FullCompo.Widgets.Builtin;

public class BuiltinWidgetProvider : IWidgetProvider
{
    public IEnumerable<IWidget> GetWidgets()
    {
        return new IWidget[]
        {
            new DateWidget(),
            new WeatherWidget(),
            new ClockWidget(),
            new NoteWidget(),
            new NotesWidget(),
            new LauncherWidget(),
            new SearchWidget(),
            new CustomTextWidget(),
            new CalendarWidget(),
            new RemindersWidget(),
            new MonitorWidget()
        };
    }
}
