using System.Text.Encodings.Web;
using System.Text.Json;

namespace FullCompo.Shared.Helpers;

public static class JsonHelper
{
    public static JsonSerializerOptions Options { get; } = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        PropertyNameCaseInsensitive = true
    };
}
