using Domain.Models;
using Domain.Services;
using Presentation.Actions.ActionHelpers;
using Presentation.Actions.Dashboard;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;

namespace Presentation.Actions.AppStart
{
    public static class RegisterActions
    {
        public static void UsernameInput(List<Template> actions)
        {
            var username = Reader.UserStringInput("username", "", 1);
            var userExists = UserServices.UserExists(username);
            if (userExists)
            {
                Console.WriteLine("Postoji korisnik sa unesenim korisničkim imenom!");
                return;
            }
            Users.CurrentUser.Username = username;

            actions[0].Status = InputStatus.Done;
            actions[5].Status = InputStatus.Warning;
            AllInputsDone(actions);
            return;
        }
        public static string UserPropertiesInput(List<Template> actions, int index)
        {
            var userInput = Reader.UserStringInput(actions[index].Name, "", 1);
            actions[index].Status = InputStatus.Done;
            actions[5].Status = InputStatus.Warning;
            AllInputsDone(actions);
            return userInput;
        }
        private static void AllInputsDone(List<Template> actions)
        {
            for (var i = 0; i < 4; i++)
            {
                if (actions[i].Status != InputStatus.Done)
                {
                    return;
                }
            }
            actions[4].Status = InputStatus.WaitingForInput;
            return;
        }
        public static void Register()
        {
            UserServices.RegisterUser();
            ActionsCaller.PrintMenuAndDoAction(DashboardActions.DashboardActionsList);
        }
        public static void SetPassword(List<Template> actions)
        {
            Users.CurrentUser.Password = UserPropertiesInput(actions, 1);
        }
        public static void SetName(List<Template> actions)
        {
            Users.CurrentUser.Name = UserPropertiesInput(actions, 2);
        }
        public static void SetSurname(List<Template> actions)
        {
            Users.CurrentUser.Surname = UserPropertiesInput(actions, 3);
        }

    }
}
