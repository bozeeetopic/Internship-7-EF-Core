using Presentation.Enums;
using System;
using System.Collections.Generic;
using Presentation.Helpers;
using System.Threading;

namespace Presentation.Actions.ActionHelpers
{
    public static class ActionsHelper
    {
        private static readonly Dictionary<InputStatus, ConsoleColor> _colors = new()
        {
            { InputStatus.WaitingForInput, ConsoleColor.Gray },
            { InputStatus.Done, ConsoleColor.Green },
            { InputStatus.Error, ConsoleColor.Red },
            { InputStatus.Warning, ConsoleColor.Yellow },
        };
        public static void PrintActionsColored( List<Template> actions)
        {
            var i = 1;
            foreach(var action in actions)
            {
                ConsoleHelpers.WriteInColor($"{i}. - {action.Name}\n", _colors[action.Status]);
                i++;
            }
        }
        public static  void GenericMenu(List<Template> InputsList, Action<List<Template>, int> action)
        {
            while (true)
            {
                PrintActionsColored(InputsList);
                Console.WriteLine();
                var choiceActionIndex = Reader.UserNumberInput("vaš izbor", 1, InputsList.Count) - 1;
                switch (InputsList[choiceActionIndex].Status)
                {
                    case InputStatus.Warning:
                        if (!Reader.UserConfirmation("Jeste li sigurni da želite izvršiti odabranu akciju? "))
                        {
                            break;
                        }
                        action(InputsList, choiceActionIndex);
                        break;
                    case InputStatus.Done:
                    case InputStatus.WaitingForInput:
                        action(InputsList, choiceActionIndex);
                        break;
                    case InputStatus.Error:
                        Console.WriteLine("Odabrana akcija trenutno nije moguća!");
                        break;
                }
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }
}
