using Avalonia.Controls;
using Avalonia.Media;
using FullCompo.Core.Models;
using FullCompo.Shared.Models;

namespace FullCompo.Core.Abstractions;

public interface IWidget
{
    string Id { get; }
    string Name { get; }
    string Description { get; }
    IImage? Icon { get; }
    IEnumerable<WidgetSize> SupportedSizes { get; }
    bool HasCustomBackground { get; }

    Control CreateView(WidgetContext context);
    Control? CreateSettingsView(WidgetSettings settings);
    WidgetSettings CreateDefaultSettings();
    void OnActivated(WidgetContext context);
    void OnDeactivated();
}
