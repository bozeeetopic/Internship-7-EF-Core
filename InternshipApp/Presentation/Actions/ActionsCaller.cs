using System;
using System.Collections.Generic;
using Presentation.Actions.AppStart;
using Presentation.Actions.Dashboard;
using Presentation.Helpers;
using Data.Entities.Enums;

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
            { ("Resursi", () => Dashboard.DashboardActions.ChooseDomainAndListResourceAction(true)) },
            { ("Korisnici", () => Dashboard.DashboardActions.Users() )},
            { ("Neodgovoreno", () => Dashboard.DashboardActions.ChooseDomainAndListResourceAction(false)) },
            { ("Moj profil", () => Dashboard.DashboardActions.MyProfile()) },
            { ("Logout", () => Dashboard.DashboardActions.LogOut())},
        };

        public static void PrintMenuAndDoAction(List<(string name, Action action)> actions)
        {
            Console.Clear();
            for(var index = 0; index < actions.Count; index++)
            {
                Console.WriteLine($"{index+1} - {actions[index].name}");
            }
            Console.WriteLine();
            var choice = Reader.UserNumberInput("vaš izbor", 1, actions.Count);
            Console.Clear();
            actions[choice - 1].action();
        }
    }
}
