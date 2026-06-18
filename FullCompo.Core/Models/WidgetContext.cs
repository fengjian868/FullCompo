using FullCompo.Shared.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FullCompo.Core.Models;

public class WidgetContext
{
    public string InstanceId { get; }
    public string PanelId { get; }
    public WidgetSize CurrentSize { get; }
    public IServiceProvider Services { get; }
    public ILogger Logger { get; }
    public WidgetSettings Settings { get; }

    public WidgetContext(
        string instanceId,
        string panelId,
        WidgetSize currentSize,
        IServiceProvider services,
        ILogger logger,
        WidgetSettings settings)
    {
        InstanceId = instanceId;
        PanelId = panelId;
        CurrentSize = currentSize;
        Services = services;
        Logger = logger;
        Settings = settings;
    }

    public T? GetService<T>() where T : notnull => Services.GetService<T>();
}
