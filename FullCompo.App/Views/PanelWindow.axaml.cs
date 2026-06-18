using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FullCompo.Core.Abstractions;
using FullCompo.Core.Abstractions.Services;
using FullCompo.Core.Models;
using FullCompo.Shared.Enums;
using FullCompo.Shared.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FullCompo.App.Views;

public partial class PanelWindow : Window
{
    private readonly PanelConfig _config;
    private readonly IWidgetRegistry _widgetRegistry;
    private readonly IServiceProvider _services;
    private readonly ILogger<PanelWindow> _logger;
    private readonly List<Control> _widgetViews = new();

    public PanelConfig Config => _config;

    public PanelWindow(PanelConfig config, IServiceProvider services)
    {
        _config = config;
        _services = services;
        _widgetRegistry = services.GetRequiredService<IWidgetRegistry>();
        _logger = services.GetRequiredService<ILogger<PanelWindow>>();

        InitializeComponent();
        SetupGrid();
        LoadWidgets();
        UpdatePosition();
    }

    private void SetupGrid()
    {
        WidgetGrid.Children.Clear();
        WidgetGrid.ColumnDefinitions.Clear();
        WidgetGrid.RowDefinitions.Clear();

        for (var i = 0; i < _config.Columns; i++)
        {
            WidgetGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }

        var maxRow = _config.Widgets.Any() ? _config.Widgets.Max(w => w.Row + w.RowSpan) : 1;
        for (var i = 0; i < maxRow; i++)
        {
            WidgetGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
        }
    }

    private void LoadWidgets()
    {
        foreach (var widgetConfig in _config.Widgets)
        {
            var widget = _widgetRegistry.GetWidget(widgetConfig.WidgetId);
            if (widget == null)
            {
                _logger.LogWarning("Widget {WidgetId} not found", widgetConfig.WidgetId);
                continue;
            }

            var size = widget.SupportedSizes.FirstOrDefault(s => s.Columns == widgetConfig.ColumnSpan && s.Rows == widgetConfig.RowSpan)
                ?? widget.SupportedSizes.First();

            var context = new WidgetContext(
                widgetConfig.Id,
                _config.Id,
                size,
                _services,
                _services.GetRequiredService<ILoggerFactory>().CreateLogger(widget.GetType().FullName ?? widget.Id),
                widgetConfig.Settings);

            try
            {
                widget.OnActivated(context);
                var view = widget.CreateView(context);
                view.Margin = new Thickness(_config.Spacing / 2);

                Grid.SetColumn(view, widgetConfig.Column);
                Grid.SetRow(view, widgetConfig.Row);
                Grid.SetColumnSpan(view, widgetConfig.ColumnSpan);
                Grid.SetRowSpan(view, widgetConfig.RowSpan);

                WidgetGrid.Children.Add(view);
                _widgetViews.Add(view);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load widget {WidgetId}", widgetConfig.WidgetId);
            }
        }
    }

    public void UpdatePosition()
    {
        var screens = Screens.ScreenFromWindow(this);
        var bounds = screens?.Bounds ?? new PixelRect(0, 0, 1920, 1080);

        var totalWidth = _config.Columns * _config.CellWidth + (_config.Columns - 1) * _config.Spacing + _config.MarginLeft + _config.MarginRight + 16;
        var maxRow = _config.Widgets.Any() ? _config.Widgets.Max(w => w.Row + w.RowSpan) : 1;
        var totalHeight = maxRow * _config.CellHeight + (maxRow - 1) * _config.Spacing + _config.MarginTop + _config.MarginBottom + 16;

        Width = totalWidth;
        Height = totalHeight;

        var position = _config.DockMode switch
        {
            PanelDockMode.TopRightCorner => new PixelPoint(
                bounds.X + bounds.Width - (int)totalWidth - (int)_config.MarginRight,
                bounds.Y + (int)_config.MarginTop),
            PanelDockMode.TopLeftCorner => new PixelPoint(
                bounds.X + (int)_config.MarginLeft,
                bounds.Y + (int)_config.MarginTop),
            PanelDockMode.BottomRightCorner => new PixelPoint(
                bounds.X + bounds.Width - (int)totalWidth - (int)_config.MarginRight,
                bounds.Y + bounds.Height - (int)totalHeight - (int)_config.MarginBottom),
            PanelDockMode.BottomLeftCorner => new PixelPoint(
                bounds.X + (int)_config.MarginLeft,
                bounds.Y + bounds.Height - (int)totalHeight - (int)_config.MarginBottom),
            _ => new PixelPoint(
                bounds.X + bounds.Width - (int)totalWidth - (int)_config.MarginRight,
                bounds.Y + (int)_config.MarginTop)
        };

        Position = position;
    }

    public void SetEditMode(bool isEditMode)
    {
        PanelBorder.IsHitTestVisible = isEditMode;
        PanelBorder.Opacity = isEditMode ? 1.0 : _services.GetRequiredService<IThemeService>().CurrentTheme.Opacity;
    }
}
