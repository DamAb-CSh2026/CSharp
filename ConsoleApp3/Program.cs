using System;
public enum LogLevel
{
    Error,
    Warning,
    Info
}
public class Logger
{
    public void Log(LogLevel level, string message)
    {
        Console.WriteLine($"{level.ToString().ToUpper()}: {message}");
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        var logger = new Logger();

        logger.Log(LogLevel.Error, "Произошла ошибка");
        logger.Log(LogLevel.Warning, "Недостаточно памяти");
        logger.Log(LogLevel.Info, "Приложение запущено");
    }
}
