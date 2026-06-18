using Avalonia;
using Avalonia.Controls;
using FullCompo.App.Services;
using FullCompo.Core.Abstractions;
using FullCompo.Core.Abstractions.Services;
using FullCompo.Core.Services;
using FullCompo.Widgets.Builtin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FullCompo.App;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        IHost? host = null;
        try
        {
            host = CreateHostBuilder(args).Build();

            // Pre-load configuration and themes before starting Avalonia
            var configService = host.Services.GetRequiredService<IConfigService>();
            configService.Load();

            var themeService = host.Services.GetRequiredService<IThemeService>();
            themeService.LoadThemes();
            themeService.ApplyTheme(configService.AppSettings.ThemeId);

            // Load plugins from config directory
            var pluginService = host.Services.GetRequiredService<PluginService>();
            var pluginsDirectory = Path.Combine(configService.GetConfigDirectory(), "plugins");
            pluginService.LoadPlugins(pluginsDirectory);

            BuildAvaloniaApp(host.Services)
                .StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            try
            {
                host?.Services.GetService<ILogger<Program>>()?.LogCritical(ex, "Application crashed");
            }
            catch { }
            try
            {
                var logPath = Path.Combine(Path.GetTempPath(), "FullCompo_Crash.log");
                File.WriteAllText(logPath, ex.ToString());
            }
            catch { }
            throw;
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddSingleton<IConfigService, ConfigService>();
                services.AddSingleton<IThemeService, ThemeService>();
                services.AddSingleton<IWidgetRegistry, WidgetRegistry>();
                services.AddSingleton<IPanelService, PanelService>();
                services.AddSingleton<PluginService>();
                services.AddSingleton<BuiltinWidgetProvider>();

                services.AddLogging(builder =>
                {
                    builder.AddSimpleConsole(options =>
                    {
                        options.SingleLine = true;
                        options.TimestampFormat = "[HH:mm:ss] ";
                    });
                    builder.SetMinimumLevel(LogLevel.Information);
                });
            });
    }

    public static AppBuilder BuildAvaloniaApp(IServiceProvider services)
    {
        return AppBuilder.Configure(() => new App(services))
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }
}
