using System;
using System.Runtime.InteropServices;
using ZombieSimulation.Entities;

public static class ColorLogger
{
    static ColorLogger()
    {
        // Enable colors in Windows console
        if (OperatingSystem.IsWindows())
        {
            var handle = GetStdHandle(-11);  // STD_OUTPUT_HANDLE = -11
            GetConsoleMode(handle, out uint mode);
            SetConsoleMode(handle, mode | 0x4);  // ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x4
        }
    }

    // Add these P/Invoke declarations at the top of the class
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll")]
    private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

    [DllImport("kernel32.dll")]
    private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    private static readonly ConsoleColor CYAN_COLOR = ConsoleColor.Cyan;
    private static readonly ConsoleColor GREEN_COLOR = ConsoleColor.Green;
    private static readonly ConsoleColor RED_COLOR = ConsoleColor.Red;
    private static readonly ConsoleColor MAGENTA_COLOR = ConsoleColor.Magenta;
    private static readonly ConsoleColor YELLOW_COLOR = ConsoleColor.Yellow;

    private static class AnsiCodes
    {
        public const string Reset = "\u001b[0m";
        public const string Cyan = "\u001b[36m";
        public const string Green = "\u001b[32m";
        public const string Red = "\u001b[31m";
        public const string Magenta = "\u001b[35m";
        public const string Yellow = "\u001b[33m";
    }

    public static void GreenLog(string message)
    {
        WriteColoredLine(message, GREEN_COLOR);
    }

    public static void RedLog(string message)
    {
        WriteColoredLine(message, RED_COLOR);
    }

    public static void CyanLog(string message)
    {
        WriteColoredLine(message, CYAN_COLOR);
    }

    public static void YellowLog(string message)
    {
        WriteColoredLine(message, YELLOW_COLOR);
    }

    public static void LogMetric(string label, double value, string unit = "")
    {
        Console.Write($"{AnsiCodes.Cyan}{label}: ");
        Console.Write($"{AnsiCodes.Magenta}{value:F2}");
        if (!string.IsNullOrEmpty(unit))
        {
            Console.Write($" {unit}");
        }
        Console.WriteLine(AnsiCodes.Reset);
    }

    private static void WriteColoredLine(string message, ConsoleColor color)
    {
        string ansiCode = color switch
        {
            ConsoleColor.Cyan => AnsiCodes.Cyan,
            ConsoleColor.Green => AnsiCodes.Green,
            ConsoleColor.Red => AnsiCodes.Red,
            ConsoleColor.Yellow => AnsiCodes.Yellow,
            _ => ""
        };
        
        Console.WriteLine($"{ansiCode}{message}{AnsiCodes.Reset}");
    }
}