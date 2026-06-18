using FullCompo.PluginSdk.Models;

namespace FullCompo.PluginSdk;

public interface IPlugin
{
    string Id { get; }
    string Name { get; }
    string Version { get; }
    string? Description { get; }

    void Initialize(IPluginContext context);
    void Shutdown();
}
