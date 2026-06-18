using System.Reflection;
using System.Runtime.Loader;
using FullCompo.Core.Abstractions;
using FullCompo.PluginSdk;
using FullCompo.PluginSdk.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FullCompo.App.Services;

public class PluginService : IDisposable
{
    private readonly IServiceProvider _services;
    private readonly IWidgetRegistry _widgetRegistry;
    private readonly ILogger<PluginService> _logger;
    private readonly List<PluginLoadContext> _loadContexts = new();
    private readonly List<IPlugin> _plugins = new();

    public PluginService(IServiceProvider services, IWidgetRegistry widgetRegistry, ILogger<PluginService> logger)
    {
        _services = services;
        _widgetRegistry = widgetRegistry;
        _logger = logger;
    }

    public void LoadPlugins(string pluginsDirectory)
    {
        if (!Directory.Exists(pluginsDirectory))
        {
            Directory.CreateDirectory(pluginsDirectory);
            return;
        }

        var pluginFiles = Directory.GetFiles(pluginsDirectory, "*.dll", SearchOption.AllDirectories);

        foreach (var file in pluginFiles)
        {
            try
            {
                LoadPlugin(file);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load plugin from {File}", file);
            }
        }
    }

    private void LoadPlugin(string pluginPath)
    {
        var pluginDirectory = Path.GetDirectoryName(pluginPath)!;
        var loadContext = new PluginLoadContext(pluginDirectory);
        _loadContexts.Add(loadContext);

        var assembly = loadContext.LoadFromAssemblyPath(pluginPath);
        var pluginTypes = assembly.GetTypes()
            .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

        foreach (var type in pluginTypes)
        {
            if (Activator.CreateInstance(type) is not IPlugin plugin)
            {
                continue;
            }

            var logger = _services.GetRequiredService<ILoggerFactory>().CreateLogger($"Plugin.{plugin.Id}");
            var context = new PluginContext(
                plugin.Id,
                pluginDirectory,
                _services,
                logger,
                _widgetRegistry);

            plugin.Initialize(context);
            _plugins.Add(plugin);

            _logger.LogInformation("Loaded plugin {PluginName} v{Version} from {File}", plugin.Name, plugin.Version, pluginPath);
        }
    }

    public void UnloadAllPlugins()
    {
        foreach (var plugin in _plugins)
        {
            try
            {
                plugin.Shutdown();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error shutting down plugin {PluginId}", plugin.Id);
            }
        }

        _plugins.Clear();
        _loadContexts.Clear();
    }

    public void Dispose()
    {
        UnloadAllPlugins();
        GC.SuppressFinalize(this);
    }
}

public class PluginLoadContext : AssemblyLoadContext
{
    private readonly string _pluginDirectory;

    public PluginLoadContext(string pluginDirectory) : base(isCollectible: true)
    {
        _pluginDirectory = pluginDirectory;
    }

    protected override Assembly? Load(AssemblyName assemblyName)
    {
        // Don't reload framework assemblies or host assemblies
        var existing = Default.Assemblies.FirstOrDefault(a => a.FullName == assemblyName.FullName);
        if (existing != null)
        {
            return existing;
        }

        var assemblyPath = Path.Combine(_pluginDirectory, $"{assemblyName.Name}.dll");
        if (File.Exists(assemblyPath))
        {
            return LoadFromAssemblyPath(assemblyPath);
        }

        return null;
    }
}
