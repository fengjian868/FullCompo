using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using FullCompo.Core.Abstractions;
using FullCompo.Core.Abstractions.Services;
using FullCompo.Widgets.Builtin;
using Microsoft.Extensions.DependencyInjection;

namespace FullCompo.App;

public partial class App : Application
{
    private readonly IServiceProvider _services;
    private IThemeService _themeService = null!;
    private IPanelService _panelService = null!;

    public App(IServiceProvider services)
    {
        _services = services;
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        RegisterBuiltinWidgets();

        _themeService = _services.GetRequiredService<IThemeService>();
        _panelService = _services.GetRequiredService<IPanelService>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new Window { IsVisible = false };
            _panelService.CreateOrUpdatePanels();

            var hotKey = new KeyGesture(Key.E, KeyModifiers.Control | KeyModifiers.Shift);
            RegisterGlobalHotKey(desktop, hotKey);
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void RegisterBuiltinWidgets()
    {
        var registry = _services.GetRequiredService<IWidgetRegistry>();
        var provider = _services.GetRequiredService<BuiltinWidgetProvider>();
        registry.RegisterRange(provider.GetWidgets());
    }

    private void RegisterGlobalHotKey(IClassicDesktopStyleApplicationLifetime desktop, KeyGesture gesture)
    {
        // Global hotkey is platform-specific; for now we use a simple timer to check keyboard state
        // or rely on tray menu. This is a placeholder for future implementation.
    }
}
