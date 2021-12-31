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
        public static void AddPlaceholder(string placeholderString)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(placeholderString);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(Console.CursorLeft - placeholderString.Length, Console.CursorTop);
        }
        public static string WriteXForDecimals(int maxValue)
        {
            var stringToReturn = "";
            while (maxValue > 0)
            {
                stringToReturn += "X";
                maxValue = (maxValue - (maxValue % 10)) / 10;
            }
            return stringToReturn;
        }
    }
}
