using Domain.Enums;
using Domain.Models;
using Presentation.Actions.ActionHelpers;
using Presentation.Enums;
using System;
using System.Collections.Generic;

namespace Presentation.Actions.Dashboard
{
    public static class DashboardAction
    {
        public static void Resources()
        {
            var domain = ResourceType.Get(ResourceActions.ChooseDomain());
            List<Template> InputsList = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Ispiši resurse", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Uđi u komentare resursa", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Reagiraj na resurs", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Izlaz", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.DashboardActions) }
            };

            ActionsHelper.GenericMenu(InputsList);
        }
        public static void Users() { }
        public static void ResourcesWithNoComments()
        {
        }
        public static void MyProfile() { }
        public static void LogOut()
        {
            CurrentUser.User = new();
            ActionsCaller.PrintMenuAndDoAction(ActionsCaller.AppStartActions);
        }
    }
}
