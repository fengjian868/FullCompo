using Avalonia.Controls;
using FullCompo.Shared.Models;

namespace FullCompo.Core.Abstractions.Services;

public interface IPanelService
{
    IReadOnlyList<Window> WidgetWindows { get; }
    void CreateOrUpdateWidgets();
    void EnterEditMode();
    void ExitEditMode();
    void RemoveWidget(WidgetInstanceConfig config);
    bool IsEditMode { get; }
    event EventHandler<bool>? EditModeChanged;
}
