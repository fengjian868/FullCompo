using Avalonia.Controls;
using FullCompo.Shared.Models;

namespace FullCompo.Core.Abstractions.Services;

public interface IPanelService
{
    IReadOnlyList<Window> Panels { get; }

    void CreateOrUpdatePanels();
    void EnterEditMode();
    void ExitEditMode();
    bool IsEditMode { get; }
    event EventHandler<bool>? EditModeChanged;
}
