using FullCompo.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FullCompo.PluginSdk.Models;

public class PluginContext : IPluginContext
{
    public string PluginId { get; }
    public string PluginDirectory { get; }
    public IServiceProvider Services { get; }
    public ILogger Logger { get; }
    public IWidgetRegistry WidgetRegistry { get; }

    public PluginContext(
        string pluginId,
        string pluginDirectory,
        IServiceProvider services,
        ILogger logger,
        IWidgetRegistry widgetRegistry)
    {
        PluginId = pluginId;
        PluginDirectory = pluginDirectory;
        Services = services;
        Logger = logger;
        WidgetRegistry = widgetRegistry;
    }
}
