using TrafficController.Enums;

namespace TrafficController.Classes;

public static class Logger
{
    public static void Log(string message)
    {
        Console.Write(message);
        Console.ResetColor();
    }

    public static void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Log(message);
    }

    public static void LogWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Log(message);
    }

    public static void LogSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Log(message);
    }

    public static void LogBGColor(ConsoleColor color, string message)
    {
        Console.BackgroundColor = color;
        Console.ForegroundColor = color;
        Log(message);
    }

    public static void LogColor(ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;
        Log(message);
    }

    public static void LogPlane()
    {
        Console.BackgroundColor = (ConsoleColor) Color.BackgroundSky;
        Console.ForegroundColor = (ConsoleColor) Color.PlaneForeground;
        Log("V");
    }

    public static void Reset()
    {
        Console.ResetColor();
        Console.Clear();
    }
}