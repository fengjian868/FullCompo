namespace FullCompo.Shared.Models.Weather;

public static class OpenMeteoWeatherCode
{
    public static string GetText(int code) => code switch
    {
        0 => "晴",
        1 => "大部晴朗",
        2 => "多云",
        3 => "阴",
        45 => "雾",
        48 => "雾凇",
        51 => "毛毛雨",
        53 => "小雨",
        55 => "中雨",
        56 => "冻雨",
        57 => "强冻雨",
        61 => "小雨",
        63 => "中雨",
        65 => "大雨",
        66 => "冻雨",
        67 => "强冻雨",
        71 => "小雪",
        73 => "中雪",
        75 => "大雪",
        77 => "雪粒",
        80 => "阵雨",
        81 => "强阵雨",
        82 => "暴雨",
        85 => "阵雪",
        86 => "强阵雪",
        95 => "雷雨",
        96 => "雷伴冰雹",
        99 => "强雷伴冰雹",
        _ => "未知"
    };

    public static string GetIcon(int code, bool isDay = true) => code switch
    {
        0 => isDay ? "☀️" : "🌙",
        1 => isDay ? "🌤️" : "☁️",
        2 => "⛅",
        3 => "☁️",
        45 or 48 => "🌫️",
        51 or 53 or 55 or 56 or 57 => "🌦️",
        61 or 63 or 65 or 66 or 67 => "🌧️",
        71 or 73 or 75 or 77 or 85 or 86 => "🌨️",
        80 or 81 or 82 => "🌦️",
        95 or 96 or 99 => "⛈️",
        _ => "🌡️"
    };

    public static (string Start, string End) GetGradient(int code, bool isDay = true) => code switch
    {
        0 => isDay ? ("#FF5B8BD4", "#FFE8A838") : ("#FF1A2A4A", "#FF3A3A6A"),
        1 => isDay ? ("#FF4A90E2", "#FF7A9BCB") : ("#FF2A3A5A", "#FF4A4A7A"),
        2 or 3 => ("#FF5F6D7A", "#FF3A4B5C"),
        45 or 48 => ("#FF6E7F91", "#FF9AA5B1"),
        >= 51 and <= 67 => ("#FF2C3E50", "#FF4A5F7A"),
        >= 71 and <= 77 or 85 or 86 => ("#FF7DAFD6", "#FFE0E8F0"),
        >= 80 and <= 82 => ("#FF2C3E50", "#FF4A5F7A"),
        >= 95 and <= 99 => ("#FF3A4B5C", "#FF6B5B7A"),
        _ => ("#FF4A90E2", "#FF2D5A9E")
    };
}
