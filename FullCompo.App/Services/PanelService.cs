using Avalonia.Controls;
using FullCompo.App.Views;
using FullCompo.Core.Abstractions;
using FullCompo.Core.Abstractions.Services;
using FullCompo.Shared.Models;

namespace FullCompo.App.Services;

public class PanelService : IPanelService
{
    private readonly IServiceProvider _services;
    private readonly IConfigService _configService;
    private readonly IWidgetRegistry _widgetRegistry;
    private readonly List<WidgetWindow> _widgetWindows = new();

    public IReadOnlyList<Window> WidgetWindows => _widgetWindows.Cast<Window>().ToList().AsReadOnly();
    public bool IsEditMode { get; private set; }

    public event EventHandler<bool>? EditModeChanged;

    public PanelService(IServiceProvider services, IConfigService configService, IWidgetRegistry widgetRegistry)
    {
        _services = services;
        _configService = configService;
        _widgetRegistry = widgetRegistry;
    }

    public void CreateOrUpdateWidgets()
    {
        // Close existing windows
        foreach (var window in _widgetWindows)
        {
            window.Close();
        }
        _widgetWindows.Clear();

        // Create a window for each widget across all panels
        var gridX = 16.0;
        var gridY = 16.0;
        var rowHeight = 0.0;
        var screenWidth = 1920.0; // default, will be overridden by actual position

        foreach (var panel in _configService.Panels)
        {
            foreach (var widgetConfig in panel.Widgets)
            {
                var widget = _widgetRegistry.GetWidget(widgetConfig.WidgetId);
                if (widget == null) continue;

                // Auto-position if not set
                if (widgetConfig.PosX == 0 && widgetConfig.PosY == 0)
                {
                    var size = widget.SupportedSizes.FirstOrDefault(s => s.Id == widgetConfig.SizeId)
                        ?? widget.SupportedSizes.First();

                    widgetConfig.PosX = gridX;
                    widgetConfig.PosY = gridY;

                    rowHeight = Math.Max(rowHeight, size.Height);
                    gridX += size.Width + 8;

                    // Wrap to next row if exceeds screen width
                    if (gridX > screenWidth - 140)
                    {
                        gridX = 16;
                        gridY += rowHeight + 8;
                        rowHeight = 0;
                    }
                }

                var window = new WidgetWindow(widgetConfig, widget, _services);
                _widgetWindows.Add(window);
                window.Show();
            }
        }

        _configService.Save();
    }

    public void EnterEditMode()
    {
        IsEditMode = true;
        foreach (var window in _widgetWindows)
        {
            window.SetEditMode(true);
        }
        EditModeChanged?.Invoke(this, true);
    }

    public void ExitEditMode()
    {
        IsEditMode = false;
        foreach (var window in _widgetWindows)
        {
            window.SetEditMode(false);
        }
        _configService.Save();
        EditModeChanged?.Invoke(this, false);
    }

    public void RemoveWidget(WidgetInstanceConfig config)
    {
        foreach (var panel in _configService.Panels)
        {
            if (panel.Widgets.Remove(config))
                break;
        }
        _configService.Save();
    }
}
