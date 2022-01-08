using Domain.Models;
using Domain.Services;
using Presentation.Actions.ActionHelpers;
using Presentation.Actions.AppStart;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;

namespace Presentation.Actions.Dashboard
{
    public static class DashboardActions
    {
        public static readonly List<(string, Action)> DashboardActionsList = new()
        {
            { ("Resursi", () => ChooseDomainAndListResourceAction(true)) },
            { ("Korisnici", () => Users()) },
            { ("Neodgovoreno", () => ChooseDomainAndListResourceAction(false)) },
            { ("Popularno", () => Popular()) },
            { ("Moj profil", () => MyProfile()) },
            { ("Logout", () => LogOut()) },
        };
        public static void Popular()
        {
            ResourceServices.SetPopularResources();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Redni broj - Naslov\t\tAutor\t\tBroj komentara");
                var i = 1;
                foreach (var resource in Resources.ResourcesList)
                {
                    Console.WriteLine($"{i} - {resource.Header}\t\t{UserServices.ReturnUserById(resource.AuthorId).Username}\t\t{CommentServices.GetCommentsCount(resource.Id)}");
                }
                Console.WriteLine("\nOdaberite između sljedećih akcija: ");

                List<Template> actions = new()
                {
                    new() { Status = InputStatus.WaitingForInput, Name = "Uđi u resurs", Function = () => ResourceActions.EnterResource() },
                    new() { Status = InputStatus.WaitingForInput, Name = "Povratak u meni", Function = () => ActionsCaller.PrintMenuAndDoAction(DashboardActionsList) }
                };

                ActionsHelper.GenericMenu(actions);
            }
        }
        public static void ChooseDomainAndListResourceAction(bool listingAll)
        {
            ResourceActions.ChooseDomain();
            Resources.ListAll = listingAll;

            ListResourceActions();
        }
        public static void ListResourceActions()
        {
            Console.Clear();
            List<Template> actions = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Dodaj resurs", Function = () => ResourceActions.AddResource() },
                new() { Status = InputStatus.WaitingForInput, Name = "Uđi u resurs", Function = () => ResourceActions.EnterResource() },
                new() { Status = InputStatus.WaitingForInput, Name = "Povratak u odabir područja", Function = () => ChooseDomainAndListResourceAction(Resources.ListAll) },
                new() { Status = InputStatus.WaitingForInput, Name = "Povratak u meni", Function = () => ActionsCaller.PrintMenuAndDoAction(DashboardActionsList) }
            };
            ResourceActions.GetResourcesFromDB();
            ResourceActions.SetActionCallabilityStatus(actions);

            ActionsHelper.GenericMenuAndMessage(actions,"");
        }
        public static void Users()
        {
            UserServices.SetUsers();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Redni broj - Korisničko ime");
                var i = 1;
                foreach (var user in Domain.Models.Users.UsersList)
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
                    new() { Status = InputStatus.WaitingForInput, Name = "Povratak u meni", Function = () => ActionsCaller.PrintMenuAndDoAction(DashboardActionsList) }
                };
                SetActionCallabilityStatus(actions);
                ActionsHelper.GenericMenu(actions);
            }
        }
        public static void SetActionCallabilityStatus(List<Template> actions)
        {
            switch (Domain.Models.Users.UsersList.Count)
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
            Domain.Models.Users.SearchedUser = Domain.Models.Users.CurrentUser;

            List<Template> actions = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Edit username", Function = () => UserActions.EditUsername() },
                new() { Status = InputStatus.WaitingForInput, Name = "Edit name", Function = () => UserActions.EditName() },
                new() { Status = InputStatus.WaitingForInput, Name = "Edit surname", Function = () => UserActions.EditSurname() },
                new() { Status = InputStatus.WaitingForInput, Name = "Edit password", Function = () => UserActions.EditPassword() },
                new() { Status = InputStatus.WaitingForInput, Name = "Povratak u meni", Function = () => ActionsCaller.PrintMenuAndDoAction(DashboardActionsList) }
            };
            var userString = Domain.Models.Users.UserToStringWithPassword(Domain.Models.Users.SearchedUser);
            ActionsHelper.GenericMenuAndMessage(actions, userString);
        }
        public static void LogOut()
        {
            Domain.Models.Users.CurrentUser = new();
            ActionsCaller.PrintMenuAndDoAction(AppStartActions.AppStartActionsList);
        }
    }
}
