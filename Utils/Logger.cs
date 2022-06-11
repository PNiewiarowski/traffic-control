using TrafficController.Map;
using TrafficController.Ui;

namespace TrafficController.Utils;

public static class Logger
{
    public static void Log(string message)
    {
        Console.Write(message);
        Console.ResetColor();
    }

    public static void Log(ConsoleColor color, string message)
    {
        Console.BackgroundColor = color;
        Console.ForegroundColor = color;
        Log(message);
    }

    public static void LogRed(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Log(message);
    }

    public static void LogYellow(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Log(message);
    }

    public static void LogGreen(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Log(message);
    }

    public static void LogMapItem(MapItem item)
    {
        Console.BackgroundColor = (ConsoleColor) Color.BackgroundSky;
        Console.ForegroundColor = (ConsoleColor) Color.ForegroundPlane;
        Log(item.ToString());
    }

    public static void Reset()
    {
        Console.ResetColor();
        Console.Clear();
    }
}