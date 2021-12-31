using System;
using System.Collections.Generic;
using Presentation.Actions.AppStart;
using Presentation.Actions.Dashboard;
using Presentation.Helpers;

namespace Presentation.Actions
{
   public static class ActionsCaller
    {
        public static readonly List<(string, Action)> AppStartActions = new()
        {
            { ("Login", () => AppStartAction.Login() )},
            { ("Register", () => AppStartAction.Register() )},
            { ("Exit", () => AppStartAction.Exit()) }
        };
        public static readonly List<(string, Action)> DashboardActions = new()
        {
            { ("Resursi", () => DashboardAction.Resources()) },
            { ("Korisnici", () => DashboardAction.Users()) },
            { ("Neodgovoreno", () => DashboardAction.NoComment()) },
            { ("Moj profil", () => DashboardAction.MyProfile()) },
            { ("Logout", () => DashboardAction.LogOut())},
        };
        /*public static Dictionary<string, Action> ResourceActions = new()
        {
            { "Resursi", Login() },
            { "Korisnici", Register() },
            { "Neodgovoreno", Exit() },
            { "Moj profil", Exit() },s
            { "Logout", Exit() },
        };*/
        public static void PrintMenuAndDoAction(List<(string name, Action action)> actions)
        {
            var i = 1;
            foreach (var action in actions)
            {
                Console.WriteLine($"{i} - {action.name}");
                i++;
            }
            Console.WriteLine();
            var choice = Reader.UserNumberInput("vaš izbor", 1, actions.Count);
            actions[choice - 1].action();
        }
    }
}
