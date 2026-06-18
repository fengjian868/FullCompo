using System.Text.Json;
using System.Text.Json.Serialization;

namespace FullCompo.Shared.Models;

public class WidgetSettingsConverter : JsonConverter<WidgetSettings>
{
    public override WidgetSettings? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            return new WidgetSettings();

        var values = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(ref reader, options);
        var settings = new WidgetSettings();
        if (values != null)
        {
            settings.SetValues(values);
        }
        return settings;
    }

    public override void Write(Utf8JsonWriter writer, WidgetSettings value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        foreach (var item in value.Values)
        {
            writer.WritePropertyName(item.Key);
            item.Value.WriteTo(writer);
        }
        writer.WriteEndObject();
    }
}
