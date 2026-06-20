using System;
using System.IO;

namespace FullCompo.App.Helpers;

public static class AppLog
{
    private static readonly string LogDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "FullCompo", "Logs");

    private static readonly string LogFilePath = Path.Combine(LogDirectory, "app.log");

    private static readonly object Lock = new();

    public static void WriteException(string context, Exception exception)
    {
        try
        {
            Directory.CreateDirectory(LogDirectory);
            var message = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [ERROR] {context}{Environment.NewLine}{exception}{Environment.NewLine}";
            lock (Lock)
            {
                File.AppendAllText(LogFilePath, message);
            }
        }
        catch
        {
            // Logging failures must not crash the application.
        }
    }
}
