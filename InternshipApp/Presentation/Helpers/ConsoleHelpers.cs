using System;

namespace Presentation.Helpers
{
    public class ConsoleHelpers
    {
        public static void ClearNumberOfLinesFromConsole(int numberOfLinesToDelete)
        {
            for (var i = 0; i < numberOfLinesToDelete; i++)
            {
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - i);
            }
        }
        public static void WriteInColor(string input, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(input);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
