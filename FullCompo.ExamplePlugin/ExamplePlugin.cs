using FullCompo.PluginSdk;
using Microsoft.Extensions.Logging;

namespace FullCompo.ExamplePlugin;

public class ExamplePlugin : IPlugin
{
    public string Id => "fullcompo.example";
    public string Name => "示例插件";
    public string Version => "1.0.0";
    public string? Description => "演示如何为全面组件编写插件";

    public void Initialize(IPluginContext context)
    {
        context.Logger.LogInformation("示例插件已加载");
        context.WidgetRegistry.Register(new GreetingWidget());
    }

    public void Shutdown()
    {
    }
}
