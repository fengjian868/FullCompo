using System.Text.Json;
using System.Text.Json.Serialization;

namespace FullCompo.Shared.Models;

[JsonConverter(typeof(WidgetSettingsConverter))]
public class WidgetSettings
{
    private readonly Dictionary<string, JsonElement> _values = new();

    public T? GetValue<T>(string key, T? defaultValue = default)
    {
        if (!_values.TryGetValue(key, out var element))
            return defaultValue;

        try
        {
            return element.Deserialize<T>();
        }
        catch
        {
            return defaultValue;
        }
    }

    public void SetValue<T>(string key, T value)
    {
        _values[key] = JsonSerializer.SerializeToElement(value);
    }

    public IReadOnlyDictionary<string, JsonElement> Values => _values;

    internal void SetValues(Dictionary<string, JsonElement> values)
    {
        _values.Clear();
        foreach (var item in values)
        {
            _values[item.Key] = item.Value;
        }
    }
}
