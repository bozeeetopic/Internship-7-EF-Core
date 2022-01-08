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
        public static  void GenericMenuAndMessage(List<Template> actions, string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                GenericMenu(actions);
            }
        }
        public static void GenericMenu(List<Template> actions)
        {
                PrintActionsColored(actions);
                Console.WriteLine();
                if(actions.Count == 1)
                {
                Console.WriteLine("Postoji samo jedan izbor!");
                Thread.Sleep(1500);
                actions[0].Function();
            }
                var choiceActionIndex = Reader.UserNumberInput(" vaš izbor", 1, actions.Count) - 1;
                switch (actions[choiceActionIndex].Status)
                {
                    case InputStatus.Warning:
                        if (!Reader.UserConfirmation("Jeste li sigurni da želite izvršiti odabranu akciju? "))
                        {
                            break;
                        }
                        Console.Clear();
                        actions[choiceActionIndex].Function();
                        break;
                    case InputStatus.Done:
                    case InputStatus.WaitingForInput:
                        actions[choiceActionIndex].Function();
                        break;
                    case InputStatus.Error:
                        Console.WriteLine("Odabrana akcija trenutno nije moguća!");
                        Thread.Sleep(1000);
                        break;
                }
                Console.Clear();
        }

    }
}
