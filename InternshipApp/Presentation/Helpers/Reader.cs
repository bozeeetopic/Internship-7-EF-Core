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
                        if(minValue != maxValue)
                        {
                            Console.Write($" između {minValue} i {maxValue}!");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write("Morate unjeti broj ");
                        if (minValue != maxValue)
                        {
                            Console.Write(" između ");
                            ConsoleHelpers.WriteInColor($"{minValue}", ConsoleColor.Red);
                            Console.Write(" i ");
                            ConsoleHelpers.WriteInColor($"{maxValue}", ConsoleColor.Red);
                        }
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
        public static string UserStringInput(string stringName, string forbiddenString, int minLength)
        {
            var repeatedInput = false;
            var input = "";
            int linesToDelete = 0;
            do
            {
                if (linesToDelete == 3)
                {
                    Console.SetCursorPosition(0, Console.GetCursorPosition().Top + 1);
                }
                if (linesToDelete != 0)
                {
                    ConsoleHelpers.ClearNumberOfLinesFromConsole(linesToDelete);
                    Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                }
                linesToDelete = 1;
                if (repeatedInput && (input.Trim().Length < minLength))
                {
                    Console.Write("Duljina unosa mora biti bar ");
                    ConsoleHelpers.WriteInColor($"{minLength}", ConsoleColor.Red);
                    Console.WriteLine("!");
                    linesToDelete++;
                }
                if (repeatedInput && ConsoleHelpers.ForbiddenStringChecker(input.Trim().ToLower(), forbiddenString))
                {
                    Console.Write("Unos sadrži ");
                    ConsoleHelpers.WriteInColor("zabranjeni", ConsoleColor.Red);
                    Console.WriteLine(" znak! ("+forbiddenString+")");
                    linesToDelete++;
                }

                Console.Write(stringName + ": ");
                ConsoleHelpers.AddPlaceholder(ConsoleHelpers.WriteXForDecimals(minLength));
                input = Console.ReadLine();
                repeatedInput = true;
            }
            while ((input.Trim().Length < minLength) || ConsoleHelpers.ForbiddenStringChecker(input.Trim().ToLower(), forbiddenString));
            return input;
        }
        public static bool UserConfirmation(string message)
        {
            Console.Write(message);
            ConsoleHelpers.AddPlaceholder("da");
            var eraseConfirm = Console.ReadLine();
            if (eraseConfirm.Trim().ToLower() is "da")
            {
                return true;
            }
            return false;
        }
        public static int DaysInMonth(int month, int year)
        {
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) return 31;
            else if (month == 4 || month == 6 || month == 9 || month == 11) return 30;
            else
            {
                if (year % 4 == 0)
                {
                    if (year % 100 == 0)
                    {
                        if (year % 400 == 0) return 29;
                        return 28;
                    }
                    return 29;
                }
                return 28;
            }
        }
        public static DateTime UserDateInput()
        {
            var year = UserNumberInput("godinu", 1, 2100);
            var month = UserNumberInput("mjesec", 1, 12);
            var day = UserNumberInput("dan", 1, DaysInMonth(month, year));
            return new DateTime(year, month, day, 0, 0, 0);
        }
    }
}
