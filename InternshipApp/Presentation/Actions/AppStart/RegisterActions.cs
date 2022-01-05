using Domain.Factories;
using Domain.Models;
using Domain.Repositories;
using Presentation.Actions.ActionHelpers;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Actions.AppStart
{
    public static class RegisterActions
    {
        public static void UsernameInput(List<Template> actions)
        {
            CurrentUser.User.Username = Reader.UserStringInput("username", "", 1);
            var UsernameChecker = RepositoryFactory.Create<MemberBase>().GetAll()
                .FirstOrDefault(p => p.Username == CurrentUser.User.Username);
            if (UsernameChecker is not null)
            {
                Console.WriteLine("Postoji korisnik sa unesenim korisničkim imenom!");
                return;
            }
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
            CurrentUser.User.ReputationPoints = 1;
            RepositoryFactory.Create<MemberBase>().Add(CurrentUser.User);
            ActionsCaller.PrintMenuAndDoAction(ActionsCaller.DashboardActions);
        }

    }
}
