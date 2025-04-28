using Core.Entities.Concrete.Log;
using Newtonsoft.Json;

namespace Core.Utilities.Helpers;

public class FileLogHelper
{
    public static async void WriteLog(CustomLog log)
    {
        await WriteLogToFile(log, "customs");
    }

    public static async void WriteLog(ErrorLog log)
    {
        await WriteLogToFile(log, "errors");
    }

    public static async void WriteLog(TransactionLog log)
    {
        await WriteLogToFile(log, "audits");
    }

    private static async Task WriteLogToFile(object log, string logType)
    {
        try
        {
            var folderPath = $"./logs/{logType}/";
            var fileName = $"{DateTime.Now:yyyy-MM-dd}.log";
            var path = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (!File.Exists(path))
            {
                await using var sw = File.CreateText(path);
                await sw.WriteLineAsync(JsonConvert.SerializeObject(log, Formatting.Indented));
            }
            else
            {
                await File.AppendAllTextAsync(path, JsonConvert.SerializeObject(log, Formatting.Indented));
            }
        }
        catch
        {
            // TODO: Ignored Exceptions
        }
    }
}