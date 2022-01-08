using Domain.Factories;
using Domain.Models;
using Domain.Repositories;
using Presentation.Actions.ActionHelpers;
using Presentation.Actions.User;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Console.Clear();
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
        public static void Users()
        {
            CurrentUser.Users = RepositoryFactory
                        .Create<MemberBase>().GetAll().ToList();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Redni broj - Korisničko ime");
                var i = 1;
                foreach (var user in CurrentUser.Users)
                {
                    if (user.ReputationPoints >= 1000)
                    {
                        ConsoleHelpers.WriteInColor($"{i} - {user.Username}", ConsoleColor.Cyan);
                    }
                    else
                    {
                        Console.WriteLine($"{i} - {user.Username}");
                    }
                    i++;
                }
                Console.WriteLine("\nOdaberite između sljedećih akcija: ");

                List<Template> actions = new()
                {
                    new() { Status = InputStatus.WaitingForInput, Name = "Pretraži po imenu", Function = () => UserActions.SearchName() },
                    new() { Status = InputStatus.WaitingForInput, Name = "Pretraži po prezimenu", Function = () => UserActions.SearchSurname() },
                    new() { Status = InputStatus.WaitingForInput, Name = "Pretraži po korisničkom imenu", Function = () => UserActions.SearchUsername() },
                    new() { Status = InputStatus.WaitingForInput, Name = "Odaberi korisnika", Function = () => UserActions.ChooseUser() },
                    new() { Status = InputStatus.WaitingForInput, Name = "Povratak u meni", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.DashboardActions) }
                };
                SetActionCallabilityStatus(actions);
                ActionsHelper.GenericMenu(actions);
            }
        }
        public static void SetActionCallabilityStatus(List<Template> actions)
        {
            switch (CurrentUser.Users.Count)
            {
                case > 1:
                    break;
                case 1:
                    actions.RemoveAt(0);
                    actions.RemoveAt(0);
                    actions.RemoveAt(0);
                    break;
                default:
                    actions.RemoveAt(0);
                    actions.RemoveAt(0);
                    actions.RemoveAt(0);
                    actions.RemoveAt(0);
                    break;
            }
        }
        public static void MyProfile()
        {
            CurrentUser.SearchedUser = CurrentUser.User;

            List<Template> actions = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Edit username", Function = () => UserActions.EditUsername() },
                new() { Status = InputStatus.WaitingForInput, Name = "Edit name", Function = () => UserActions.EditName() },
                new() { Status = InputStatus.WaitingForInput, Name = "Edit surname", Function = () => UserActions.EditSurname() },
                new() { Status = InputStatus.WaitingForInput, Name = "Edit password", Function = () => UserActions.EditPassword() },
                new() { Status = InputStatus.WaitingForInput, Name = "Povratak u meni", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.DashboardActions) }
            };
            var userString = CurrentUser.UserToStringWithPassword(CurrentUser.SearchedUser);
            ActionsHelper.GenericMenuAndMessage(actions, userString);
        }
        public static void LogOut()
        {
            CurrentUser.User = new();
            ActionsCaller.PrintMenuAndDoAction(ActionsCaller.AppStartActions);
        }
    }
}
