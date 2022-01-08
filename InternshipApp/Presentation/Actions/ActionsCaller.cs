using System;
using System.Collections.Generic;
using Presentation.Helpers;

namespace Presentation.Actions
{
   public static class ActionsCaller
    {
        public static void PrintMenuAndDoAction(List<(string name, Action action)> actions)
        {
            Console.Clear();
            for(var index = 0; index < actions.Count; index++)
            {
                Console.WriteLine($"{index+1} - {actions[index].name}");
            }
            Console.WriteLine();
            var choice = Reader.UserNumberInput(" vaš izbor", 1, actions.Count);
            Console.Clear();
            actions[choice - 1].action();
        }
    }
}
