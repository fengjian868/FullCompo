using FullCompo.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FullCompo.PluginSdk;

public interface IPluginContext
{
    string PluginId { get; }
    string PluginDirectory { get; }
    IServiceProvider Services { get; }
    ILogger Logger { get; }
    IWidgetRegistry WidgetRegistry { get; }
}
