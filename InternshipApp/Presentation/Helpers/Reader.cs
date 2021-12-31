using System;

namespace Presentation.Helpers
{
    public static class Reader
    {
        public static int UserNumberInput(string message, int minValue, int maxValue)
        {
            var repeatedInput = false;
            int number = 0;
            int linesToDelete = 3;
            do
            {
                if (repeatedInput)
                {
                    ConsoleHelpers.ClearNumberOfLinesFromConsole(linesToDelete);
                    Console.SetCursorPosition(0, Console.GetCursorPosition().Top + 1);
                    if ((int)number == -1)
                    {
                        Console.Write("Morate unjeti ");
                        ConsoleHelpers.WriteInColor("BROJ", ConsoleColor.Red);
                        Console.Write($" između {minValue} i {maxValue}!\n");
                    }
                    else
                    {
                        Console.Write("Morate unjeti broj između ");
                        ConsoleHelpers.WriteInColor($"{minValue}", ConsoleColor.Red);
                        Console.Write(" i ");
                        ConsoleHelpers.WriteInColor($"{maxValue}", ConsoleColor.Red);
                        Console.Write("!\n");
                    }
                    linesToDelete = 3;
                }
                Console.Write("Unesite " + message + ":   ");
                ConsoleHelpers.AddPlaceholder(ConsoleHelpers.WriteXForDecimals(maxValue));

                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    number = -1;
                }
                repeatedInput = true;
            }
            while (number > maxValue || number < minValue);
            return (int)number;
        }
    }
}
