using FullCompo.Shared.Models;

namespace FullCompo.Core.Abstractions.Services;

public interface IConfigService
{
    AppSettings AppSettings { get; }
    List<PanelConfig> Panels { get; }

    void Load();
    void Save();
    void ResetToDefault();
    string GetConfigDirectory();
}
