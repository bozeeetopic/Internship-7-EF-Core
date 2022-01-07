using Domain.Models;
using Presentation.Actions.ActionHelpers;
using Presentation.Enums;
using System.Collections.Generic;

namespace Presentation.Actions.Dashboard
{
    public static class DashboardActions
    {
        public static void ChooseDomainAndListResourceAction(bool listingAll)
        {
            ResourceActions.ChooseDomain();
            CurrentResource.ListAll = listingAll;

            ListResourceActions();
        }
        public static void ListResourceActions()
        {

            List<Template> actions = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Dodaj resurs", Function = () => ResourceActions.AddResource() },
                new() { Status = InputStatus.WaitingForInput, Name = "Uđi u resurs", Function = () => ResourceActions.EnterResource() },
                new() { Status = InputStatus.WaitingForInput, Name = "Povratak u meni", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.DashboardActions) }
            };
            ResourceActions.GetResourcesFromDB();
            ResourceActions.SetActionCallabilityStatus(actions);

            ActionsHelper.GenericMenuAndMessage(actions,"");
        }
        /*public static void Users()
        {
            List<Template> InputsList = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Unos korisničkog imena", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Unos šifre", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Unos imena", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Unos prezimena", Function = null },
                new() { Status = InputStatus.Error, Name = "Register", Function = () => RegisterActions.Register() },
                new() { Status = InputStatus.WaitingForInput, Name = "Izlaz", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.AppStartActions) }
            };
            InputsList[0].Function = () => RegisterActions.UsernameInput(InputsList);
            InputsList[1].Function = () => RegisterActions.SetPassword(InputsList);
            InputsList[2].Function = () => RegisterActions.SetName(InputsList);
            InputsList[3].Function = () => RegisterActions.SetSurname(InputsList);

            CurrentUser.User = new();
            ActionsHelper.GenericMenu(InputsList,"");
        }*/
        public static void MyProfile() { }
        public static void LogOut()
        {
            CurrentUser.User = new();
            ActionsCaller.PrintMenuAndDoAction(ActionsCaller.AppStartActions);
        }
    }
}
